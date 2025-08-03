using Courses_API.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Courses_API.Services
{
	public class HashService : IHashService
	{
		public HashResultDto Hash(string input)
		{
			byte[] sal = new byte[16];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(sal);
			}

			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: input,
				salt: sal,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8
				));

			return new HashResultDto
			{
				Hash = hashed,
				Sal = sal
			};
		}

		public HashResultDto Hash(string input, byte[] sal)
		{
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: input,
				salt: sal,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8
				));

			return new HashResultDto
			{
				Hash = hashed,
				Sal = sal
			};
		}
	}
}
