using Microsoft.AspNetCore.Mvc;
using se4458_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;

namespace se4458_api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly SelimContext _context;
        private const int MaxRows = 50; // Maximum number of rows to parse
        private Random _random = new Random();

        public MedicinesController(SelimContext context)
        {
            _context = context;
        }


        // POST: api/Medicines/UploadExcel
        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty or not provided.");
            }

            var medicineNames = ParseMedicineNames(file);
            await AddMedicinesToDatabase(medicineNames);

            return Ok("Medicines added successfully.");
        }

        private List<string> ParseMedicineNames(IFormFile file)
        {
            var medicineNames = new List<string>();
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    for (int row = 4; row <= MaxRows + 3; row++) // Start from the 4th row
                    {
                        var cellValue = worksheet.Cell(row, 1).Value.ToString();
                        if (string.IsNullOrEmpty(cellValue))
                            break;

                        medicineNames.Add(cellValue);
                    }
                }
            }
            return medicineNames;
        }

        private async Task AddMedicinesToDatabase(List<string> medicineNames)
        {
            foreach (var name in medicineNames)
            {
                var medicine = new Medicine
                {
                    Name = name,
                    Price = GetRandomPrice()
                };
                _context.Medicines.Add(medicine);
            }
            await _context.SaveChangesAsync();
        }

        // GET: api/Medicines/5
        [HttpGet("Get by ID")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);

            if (medicine == null)
            {
                return NotFound($"Medicine with ID {id} not found.");
            }
            return medicine;
        }

        private decimal GetRandomPrice()
        {
            return (decimal)(_random.NextDouble() * (100 - 50) + 50); // Generate random price between 50 and 100
        }
    }
}
