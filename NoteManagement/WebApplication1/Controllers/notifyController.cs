using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace NoteManagement.Services.NotifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class notifyController : ControllerBase
    {
        [HttpPost("{email}")]
        public async Task<IActionResult> Sendnewassignmentalertpost(string email)
        {
            if (email == null)
            {
                return BadRequest("emailid not passed");
            }
            string fromMail = "arjun98999899@gmail.com";
            string fromPassword = "jhha oqnu zcvp rdlv ";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "new assignment created";
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> New assignment Created </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return Ok();
        }
    }
}
