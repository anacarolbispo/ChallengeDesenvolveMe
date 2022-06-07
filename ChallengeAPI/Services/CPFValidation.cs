using ChallengeAPI.Interfaces;

namespace ChallengeAPI.Services
{
    public class CPFValidation : IValidation
    {
        public bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            return ValidateFormat(cpf);
        }

        private bool ValidateFormat(string cpf)
        {
            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            string cpfSub = cpf.Substring(0, 9);
            int[] mult = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string digito = Calculate(mult, mult.Length, cpfSub);

            mult = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            digito += Calculate(mult, mult.Length, cpfSub + digito);

            return cpf.EndsWith(digito);
        }

        private string Calculate(int[] mult, int qtdCaract, string cpfSub)
        {
            int soma = 0;

            for (int i = 0; i < qtdCaract; i++)
            {
                int x = int.Parse(cpfSub[i].ToString()) * mult[i];
                soma += x;
            }

            int mod = soma % 11;
            int digito = mod < 2 ? 0 : 11 - mod;

            return digito.ToString();
        }
    }
}
