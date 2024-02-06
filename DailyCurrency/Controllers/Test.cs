using DailyCurrency.Data;
using DailyCurrency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DailyCurrency.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _xmlUrl = "https://cbar.az/currencies/06.02.2024.xml";

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> InsertCurrencyData()
        {
            try
            {
                // Retrieve XML data
                XDocument xmlData = await GetXmlData();

                // Parse and insert currency data
                List<Currency> currencies = ParseXmlData(xmlData);
                await InsertCurrenciesToDatabase(currencies);

                return Ok("Currency data inserted successfully!");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IActionResult> DeleteAllData()
        {
            try
            {
                // Remove all existing data from the Currency table
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM Currencies");

                return Ok("All data deleted successfully!");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private async Task<XDocument> GetXmlData()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(_xmlUrl);
                if (response.IsSuccessStatusCode)
                {
                    string xmlString = await response.Content.ReadAsStringAsync();
                    return XDocument.Parse(xmlString);
                }
                else
                {
                    throw new Exception($"Failed to retrieve XML data. Status code: {response.StatusCode}");
                }
            }
        }

        private List<Currency> ParseXmlData(XDocument xmlData)
        {
            List<Currency> currencies = new List<Currency>();

            // Extract currency data from XML
            foreach (XElement valute in xmlData.Descendants("Valute"))
            {
                string code = valute.Attribute("Code").Value;
                string nominal = valute.Element("Nominal").Value;
                string name = valute.Element("Name").Value;
                decimal value = Convert.ToDecimal(valute.Element("Value").Value);

                currencies.Add(new Currency
                {
                    Code = code,
                    Nominal = nominal,
                    Name = name,
                    Value = value
                });
            }

            return currencies;
        }

        private async Task InsertCurrenciesToDatabase(List<Currency> currencies)
        {
            // Insert currencies into the database
            _context.Currencies.AddRange(currencies);
            await _context.SaveChangesAsync();
        }
    }
}
