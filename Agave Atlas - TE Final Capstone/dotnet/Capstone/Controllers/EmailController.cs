using Capstone.Email_Stuff;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using System.Net.Mail;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class EmailController : ControllerBase
    {
        private GmailClient gmailClient;

        public EmailController(GmailClient gmailClient)
        {
            this.gmailClient = gmailClient;
        }

        [HttpGet]
        public ActionResult TestEndpoint()
        {
            return Ok();
        }

        [HttpPost("testEmail")]
        public ActionResult<bool> SendTestEmail([FromBody] Email details)
        {

            GmailClient.TestMessage(details.recepient, details.email, details.sender);
            
            return true;
        }
        [HttpPost("invite")]
        public ActionResult<bool> InviteUser([FromBody] Email details)
        {
            GmailClient.SendInvite(details.recepient, details.email, details.sender);
            return true;
        }
    }
}
