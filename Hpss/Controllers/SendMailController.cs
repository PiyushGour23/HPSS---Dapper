using Hpss.Data;
using Hpss.Interface;
using Hpss.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hpss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SendMailController : ControllerBase
    {
        private DapperDbContext _dapperdbContext;
        private readonly IEmailService _emailService;

        public SendMailController(IEmailService emailService, DapperDbContext dapperDbContext)
        {
            _emailService = emailService;
            _dapperdbContext = dapperDbContext;
        }
    

    [HttpPost("SendMail")]
    public async Task<IActionResult> SendMail()
    {
            try
            {
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToEmail = "bobogour@gmail.com";
                mailRequest.Subject = "Welcome to HPSS";
                mailRequest.Body = "Thank you for using application";
                await _emailService.SendEmail(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
