using System;
using System.Text;
using System.Security.Cryptography;

namespace LiveTogether.Utils.Security
{
    static class AuthSecurity
    {
        public static void CreatePasswordHash(string pwd, out byte[] pwdHash, out byte[] pwdSalt)
        {
            if(pwd == null) {
                throw new ArgumentNullException("pwd");
            }

            if(string.IsNullOrWhiteSpace(pwd)) {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "pwd");
            }

            using(var hmac = new HMACSHA512())
            {
                pwdSalt = hmac.Key;
                pwdHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(pwd));
            }
        }

        public static bool VerifyPasswordHash(string pwd, byte[] pwdHash, byte[] pwdSalt)
        {
            if(pwd == null) {
                throw new ArgumentNullException("pwd");
            }

            if(string.IsNullOrWhiteSpace(pwd)) {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "pwd");
            }

            if(pwdHash.Length != 64) {
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "pwdHash");
            }
            
            if(pwdSalt.Length != 128) {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "pwdSalt");
            }

            var valid = true;

            using(var hmac = new HMACSHA512(pwdSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.ASCII.GetBytes(pwd));

                for(int i = 0; valid && i < computedHash.Length; i++) {
                    if(computedHash[i] != pwdHash[i]) {
                        valid = false;
                    }
                }
            }

            return valid;
        }
    }
}