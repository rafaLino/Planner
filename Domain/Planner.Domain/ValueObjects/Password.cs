namespace Planner.Domain.ValueObjects
{
    public sealed class Password
    {
        public byte[] _hash;

        public byte[] _salt;

        private Password()
        {

        }

        public bool Verify(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(_salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return SameHashes(computedHash, _hash);
            }
        }


        public static Password Load(byte[] hash, byte[] salt)
        {
            Password password = new Password();
            password._hash = hash;
            password._salt = salt;
            return password;
        }

        public static Password Create(string word)
        {
            Password password = new Password();
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                password._salt = hmac.Key;
                password._hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(word));
            }
            return password;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return SameHashes(((Password)obj)._hash, _hash);
        }

        public override string ToString()
        {
            return _hash.ToString() + _salt.ToString();
        }

        public override int GetHashCode()
        {
            return _hash.GetHashCode();
        }

        private bool SameHashes(byte[] hash, byte[] hash2)
        {
            for (int i = 0; i < hash.Length; i++)
            {
                if (hash[i] != hash2[i]) return false;
            }
            return true;
        }
    }
}
