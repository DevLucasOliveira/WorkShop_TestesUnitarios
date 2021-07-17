namespace WorkShop.ValueObjects
{
    public class Password
    {
        public Password(string password)
        {
            PasswordUser = password;
            CreatePasswordHash(password);
        }

        public string PasswordUser { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public void CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            ValidatePasswordHash(storedHash, storedSalt);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        public static bool ValidatePasswordHash(byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64)
                return false;

            if (storedSalt.Length != 128)
                return false;

            return true;
        }
    }
}
