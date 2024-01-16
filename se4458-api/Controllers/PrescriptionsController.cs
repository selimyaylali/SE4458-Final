using Microsoft.AspNetCore.Mvc;
using se4458_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace se4458_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly SelimContext _context;

        public PrescriptionController(SelimContext context)
        {
            _context = context;
        }

        // POST: /Prescription
        [HttpPost("Create Prescription")]
        public async Task<IActionResult> CreatePrescription([FromBody] Prescription prescription)
        {
            if (prescription == null)
            {
                return BadRequest("Prescription is null.");
            }


            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrescription), new { id = prescription.PrescriptionId }, prescription);
        }

        // GET: /Prescription/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescription(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(p => p.Pharmacy) 
                .FirstOrDefaultAsync(p => p.PrescriptionId == id
        );


            if (prescription == null)
            {
                return NotFound($"Prescription with ID {id} not found.");
            }

            return prescription;
        }
    }
}