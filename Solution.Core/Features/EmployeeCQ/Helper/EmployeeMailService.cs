using System.Net.Mail;

namespace Solution.Core.Features.EmployeeCQ.Helper
{
    public class EmployeeMailService
    {
        private int id { get; set; }
        private string name { get; set; }
        private string email { get; set; }
        private string user_Name { get; set; }
        private string password { get; set; }

        public EmployeeMailService(string name, string email, string user_Name, string password)
        {
            this.email = email;
            this.user_Name = user_Name;
            this.password = password;
            this.name = name;
        }

        public void SendAddEmployeeMail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("patelshubham652000@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Your Login Information for [Project_Name]";

                mail.Body = "<h1>Dear " + name + ",</h1><br><h3>We are delighted to welcome you as a valued member of our [Project_Name]. To facilitate your access and ensure a seamless login experience, we are providing you with your login credentials. <br><br>Below you will find your login information: <h2><br>Username: " + user_Name + "<br> Password: " + password + "</h3><br><br> <h3> Visit Application for login ..... <a style='color:blue' href='http://localhost:4200/#/login'> [Project_Name] </a>. </h3><br><br><h4> Thank you for choosing [Project_Name]. We look forward to providing you with a rewarding and enjoyable experience on our platform!</h4>";
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("patelshubham652000@gmail.com", "qzxmvrfebaaafumy");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
