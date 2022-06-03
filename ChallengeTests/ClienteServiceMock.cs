using Challenge.Interfaces;

namespace ChallengeTests
{
    public class ClienteServiceMock : IClienteService
    {
        public int Create(Cliente cliente)
        {
            return 4;
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente() { CPF = "12345678900", DataNasc = DateTime.Now, Nome = "Teste1"});
            clientes.Add(new Cliente() { CPF = "87654321900", DataNasc = DateTime.Now, Nome = "Teste1"});
            return clientes;
        }

        public Cliente GetByCpf(string cpf)
        {
            return new Cliente() { CPF = cpf, DataNasc = DateTime.Now, Nome = "Teste1", Id=1};
        }

        public Cliente GetById(int id)
        {
            return new Cliente() { CPF = "12345678900", DataNasc = DateTime.Now, Nome = "Teste1", Id=id };
        }
    }
}
