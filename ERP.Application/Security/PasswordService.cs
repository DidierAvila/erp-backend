using BC = BCrypt.Net.BCrypt;

namespace ERP.Application.Security
{
    public class PasswordService : IPasswordService
    {
        private readonly int _workFactor;

        public PasswordService()
        {
            // WorkFactor de 12 es un buen balance entre seguridad y performance
            _workFactor = 12;
        }

        public string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            return BC.HashPassword(password, _workFactor);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            try
            {
                return BC.Verify(password, hashedPassword);
            }
            catch (Exception)
            {
                // Si hay error al verificar (hash inválido), devolver false
                return false;
            }
        }
    }
}
