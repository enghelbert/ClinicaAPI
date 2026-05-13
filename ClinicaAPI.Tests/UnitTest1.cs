using ClinicaAPI.Controllers;
using ClinicaAPI.Data;
using ClinicaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaAPI.Tests
{
    public class PacientesControllerTests
    {
        private ClinicaContext GetContextoEnMemoria()
        {
            var opciones = new DbContextOptionsBuilder<ClinicaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ClinicaContext(opciones);
        }

        [Fact]
        public void Get_DebeRetornarListaDePacientes()
        {
            // Arrange
            var contexto = GetContextoEnMemoria();
            contexto.Pacientes.Add(new Paciente
            {
                Id = 1,
                Nombre = "Juan Perez",
                Edad = 30,
                Diagnostico = "Gripe",
                Telefono = "999888777",
                Direccion = "Av. Test 123",
                CorreoElectronico = "juan@test.com",
                FechaRegistro = DateTime.Now
            });
            contexto.SaveChanges();

            var controlador = new PacientesController(contexto);

            // Act
            var resultado = controlador.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            var pacientes = Assert.IsType<List<Paciente>>(okResult.Value);
            Assert.Single(pacientes);
        }

        [Fact]
        public void Post_DebeAgregarPacienteCorrectamente()
        {
            // Arrange
            var contexto = GetContextoEnMemoria();
            var controlador = new PacientesController(contexto);
            var nuevoPaciente = new Paciente
            {
                Nombre = "Ana Lopez",
                Edad = 25,
                Diagnostico = "Anemia",
                Telefono = "911222333",
                Direccion = "Calle Test 456",
                CorreoElectronico = "ana@test.com",
                FechaRegistro = DateTime.Now
            };

            // Act
            var resultado = controlador.Post(nuevoPaciente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(resultado);
            Assert.NotNull(okResult.Value);
            Assert.Equal(1, contexto.Pacientes.Count());
        }
    }
}