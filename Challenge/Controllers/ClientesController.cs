using Microsoft.AspNetCore.Mvc;
using Challenge.Models;
using Challenge.Interfaces;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IValidation _validation;

        public ClientesController(IClienteService clienteService, IValidation validation)
        {
            _clienteService = clienteService;
            _validation = validation;
        }

        /// <summary>
        /// Buscar todos os clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clienteService.GetAll());
        }

        /// <summary>
        /// Buscar cliente pelo CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso. Retorna o cliente</response>
        /// <response code="404">Erro. Caso o CPF informado não seja encontrado.</response>
        [HttpGet("{cpf}")]
        public IActionResult GetByCPF(string cpf)
        {
            var cliente = _clienteService.GetByCpf(cpf);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }


        /// <summary>
        /// Buscar cliente pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso. Retorna o cliente</response>
        /// <response code="404">Erro. Caso o id informado não seja encontrado.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = _clienteService.GetById(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        /// <summary>
        /// Realizar o cadastro de um cliente
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso. Retorna o cliente cadastrado</response>
        /// <response code="422">Erro. Caso o CPF informado seja inválido.</response>
        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {
            if(!_validation.IsValid(cliente.CPF))
                return UnprocessableEntity();

            _clienteService.Create(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
    }
}
