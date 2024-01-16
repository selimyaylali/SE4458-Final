using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using se4458_api.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace se4458_api.Services
{
    public class PaymentReportingService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;
        
        public PaymentReportingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromDays(1)); // Runs every 24 hours

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SelimContext>();
                GenerateAndSendReports(context).Wait();
            }
        }

        private async Task GenerateAndSendReports(SelimContext context)
        {
            var currentDate = DateTime.Now.Date;
            var prescriptions = await context.Prescriptions
                                             .Include(p => p.PrescriptionId)
                                             .Where(p => p.Date == currentDate)
                                             .ToListAsync();

            var groupedPrescriptions = prescriptions
                                       .GroupBy(p => p.PharmacyId)
                                       .Select(group => new
                                       {
                                           PharmacyId = group.Key,
                                           TotalAmount = group.Sum(p => p.TotalCost),
                                           PrescriptionCount = group.Count()
                                       });

            foreach (var group in groupedPrescriptions)
            {
                var pharmacy = await context.Pharmacies.FindAsync(group.PharmacyId);
                if (pharmacy != null)
                {
                    string report = $"To: {pharmacy.Name}\n" +
                                    $"You have submitted {group.PrescriptionCount} prescriptions today.\n" +
                                    $"Total amount is {group.TotalAmount} TL.";
                    // Send the email report
                    SendEmailReport(pharmacy.Email, "Daily Payment Report", report);
                }
            }
        }

        private void SendEmailReport(string toEmail, string subject, string body)
        {
            // Implement email sending logic here
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
