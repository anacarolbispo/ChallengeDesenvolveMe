using Challenge.Interfaces;
using Challenge.Models;

namespace Challenge.Services
{
    public class ClienteService : IClienteService
    {
        private readonly DataContext _context;

        public ClienteService(DataContext context)
        {
            _context = context;
        }

        public int Create(Cliente cliente)
        {
            if (_context.Clientes.Any(x => x.CPF == cliente.CPF))
                throw new Exception("Já existe um cliente cadastrado com o CPF " + cliente.CPF);

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return cliente.Id;
        }

        public List<Cliente> GetAll()
        {
            if (_context.Clientes == null)
            {
                return null;
            }

            return _context.Clientes.ToList();
        }

        public Cliente? GetByCpf(string cpf)
        {
            if (_context.Clientes == null)
            {
                return null;
            }

            return _context.Clientes.SingleOrDefault(x => x.CPF == cpf);
        }

        public Cliente? GetById(int id)
        {
            if (_context.Clientes == null)
            {
                return null;
            }
            var cliente = _context.Clientes.Find(id);

            return cliente;
        }
    }
}
