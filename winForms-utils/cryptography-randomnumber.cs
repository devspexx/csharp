byte[] random = new Byte[100];

//RNGCryptoServiceProvider is an implementation of a random number generator.
RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
rng.GetBytes(random); // The array is now filled with cryptographically strong random bytes.

// Gives a random number for the integer range.
// You can simply update the parameters as your needs.
RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue);

// https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator?view=net-6.0

byte[] rngBytes = new byte[4];
RandomNumberGenerator.Create().GetBytes(rngBytes);
uint myInt = BitConverter.ToUInt32(rngBytes, 0); // uint to prevent negative numbers
