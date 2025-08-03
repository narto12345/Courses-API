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

		public SecurityController(IDataProtectionProvider dataProtectionProvider)
		{
			_protector = dataProtectionProvider.CreateProtector("SecurityController");
			_timeLimitProtector = _protector.ToTimeLimitedDataProtector();
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
