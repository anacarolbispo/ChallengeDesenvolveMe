using Challenge.Models;

namespace Challenge.Interfaces
{
    public interface IClienteService
    {
        int Create(Cliente cliente);

        List<Cliente> GetAll();

        Cliente GetById(int id);

        Cliente GetByCpf(string cpf);
    }
}