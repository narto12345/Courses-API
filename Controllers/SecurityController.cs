using Courses_API.Dtos;
using Courses_API.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Courses_API.Controllers
{
	[Route("api/security")]
	public class SecurityController : ControllerBase
	{
		private IDataProtector _protector;
		private ITimeLimitedDataProtector _timeLimitProtector;
		private IHashService _hashService;

		public SecurityController(IDataProtectionProvider dataProtectionProvider, IHashService hashService)
		{
			_protector = dataProtectionProvider.CreateProtector("SecurityController");
			_timeLimitProtector = _protector.ToTimeLimitedDataProtector();
			_hashService = hashService;
		}

		[HttpGet("hash")]
		public ActionResult Hash(string plainText)
		{
			HashResultDto hash1 = _hashService.Hash(plainText);
			HashResultDto hash2 = _hashService.Hash(plainText);
			HashResultDto hash3 = _hashService.Hash(plainText, hash2.Sal);
			HashResultDto hash4 = _hashService.Hash(plainText,
			[
				12, 212, 34, 65, 34, 32, 56
			]);
			HashResultDto hash5 = _hashService.Hash(plainText,
			[
				12, 212, 34, 65, 34, 32, 56
			]);

			return Ok(new { hash1, hash2, hash3, hash4, hash5 });
		}

		[HttpGet("encrypt")]
		public ActionResult Encrypt(string plainText)
		{
			string ciphertext = _protector.Protect(plainText);
			return Ok(new { ciphertext });
		}

		[HttpGet("decipher")]
		public ActionResult Decipher(string ciphertext)
		{
			string plainText = _protector.Unprotect(ciphertext);

			return Ok(new { plainText });
		}

		[HttpGet("time-limit-encrypt")]
		public ActionResult TimeLimitEncrypt(string plainText)
		{
			string ciphertext = _timeLimitProtector.Protect(plainText, TimeSpan.FromSeconds(30));
			return Ok(new { ciphertext });
		}

		[HttpGet("time-limit-decipher")]
		public ActionResult TimeDecipher(string ciphertext)
		{
			string plainText = _timeLimitProtector.Unprotect(ciphertext);

			return Ok(new { plainText });
		}
	}
}
