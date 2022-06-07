using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ChallengeTests
{
    public class ClientesControllerTest
    {
        ClientesController _clienteController;
        IClienteService _clienteService;
        IValidation _validation;

        public ClientesControllerTest()
        {
            _validation = new CPFValidation();
            _clienteService = new ClienteServiceMock();
            _clienteController = new ClientesController(_clienteService, _validation);
        }

        [Fact]
        void Get_QuandoChamado_DeveRetornarTodosOsItens()
        {
            var response = _clienteController.Get();

            Assert.NotNull(response);

            var objectResult = response as ObjectResult;
            var clientes = objectResult.Value as IEnumerable<Cliente>;

            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.Equal(2, clientes.Count());
        }

        [Fact]
        void GetByCpf_QuandoChamado_DeveRetornarObjetoComCpfInformado()
        {
            string cpf = "45678912366";
            var response = _clienteController.GetByCPF(cpf);

            Assert.NotNull(response);

            var objectResult = response as ObjectResult;
            var cliente = objectResult.Value as Cliente;

            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.NotNull(cliente);
            Assert.Equal(cpf, cliente.CPF);
        }

        [Fact]
        void GetById_QuandoChamado_DeveRetornarObjetoComIdInformado()
        {
            int id = 1;
            var response = _clienteController.GetById(id);

            Assert.NotNull(response);

            var objectResult = response as ObjectResult;
            var cliente = objectResult.Value as Cliente;

            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);
            Assert.NotNull(cliente);
            Assert.Equal(id, cliente.Id);
        }

        [Fact]
        void Post_QuandoChamadoComCpfInvalido_DeveRetornarErrodeProcessamento()
        {
            var cliente = new Cliente() { CPF = "11144477705", DataNasc = DateTime.Now, Nome = "Teste1" };
            var response = _clienteController.Post(cliente);

            Assert.NotNull(response);
            Assert.IsType<UnprocessableEntityResult>(response);
        }

        [Theory]
        [InlineData("asfasf", "te")]
        [InlineData("123456789102", "Joana")]
        [InlineData("11144477710", "232")]
        [InlineData("asfasf", "232")]
        public void Model_QuandoInseridoDadosInvalidos_NaoEnviarParaApi(string cpf, string nome)
        {
            var cliente = new Cliente() { CPF = cpf, DataNasc = DateTime.Now, Nome = nome };
            var errors = ValidateModel(cliente);

            Assert.True(errors.Count > 0);
        }


        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
