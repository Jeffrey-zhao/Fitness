using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Common
{
  public class EmailHelper
  {
    public static void Send(EmailDTO dto)
    {
      using (MailMessage mailMessage = new MailMessage())
      using (SmtpClient smtpClient = new SmtpClient(dto.SmtpServer))
      {
        foreach (var address in dto.Addresses.Split(Consts.SPLITER))
        {
          mailMessage.To.Add(address);
        }
        mailMessage.Subject = dto.Subject;
        mailMessage.From = new MailAddress(dto.From);
        mailMessage.Body = dto.Body;
        smtpClient.Credentials = new System.Net.NetworkCredential(dto.SmtpUserName,dto.SmtpPassword);
        smtpClient.Send(mailMessage);
      }
    }
  }

  public class EmailDTO
  {
    public string Body { get; set; }
    public string From { get; set; }
    public string Addresses { get; set; }
    public string Subject { get; set; }
    public string SmtpServer { get; set; }
    public string SmtpUserName { get; set; }
    public string SmtpPassword { get; set; }
  }
}
