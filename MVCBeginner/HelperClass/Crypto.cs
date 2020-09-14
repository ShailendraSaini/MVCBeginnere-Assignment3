namespace MVCBeginner
{
    using System;
    using System.Text;

    /// <summary>
    ///     Crypto class
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        ///     Hash method returns SHA256 encrypted string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }
    }
}