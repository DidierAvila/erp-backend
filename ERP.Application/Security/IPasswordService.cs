namespace ERP.Application.Security
{
    public interface IPasswordService
    {
        /// <summary>
        /// Encripta una contraseña usando BCrypt
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <returns>Contraseña encriptada</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verifica si una contraseña coincide con el hash almacenado
        /// </summary>
        /// <param name="password">Contraseña en texto plano</param>
        /// <param name="hashedPassword">Hash de la contraseña almacenado</param>
        /// <returns>True si la contraseña coincide, false en caso contrario</returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}
