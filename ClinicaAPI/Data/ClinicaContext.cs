using Microsoft.EntityFrameworkCore;
using ClinicaAPI.Models;

namespace ClinicaAPI.Data
{
    public class ClinicaContext : DbContext
    {
        public ClinicaContext(DbContextOptions<ClinicaContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
    }
}