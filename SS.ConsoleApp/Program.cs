using MailKit.Security;
using MimeKit;
using System.Net.Mail;

internal class Program
{
    public static async Task SendEmailAsync(string email, string subject, string messageBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("SpaceSurfers", "spacesurfers5@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = messageBody };

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("spacesurfers5@gmail.com", "ewnu jgif xtjj maks");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
    public static async Task Main(string[] args)
    {
        string targetEmail = "sarahphan915@gmail.com";
        string subject = "Testing";
        string msg = "Hello Sarah,\n\nThis is a test email sent from SpaceSurfers!\n\nBest,\nPixelPals";

        try
        {
            await SendEmailAsync(targetEmail, subject, msg);
            Console.WriteLine("Successfully sent email.");
        }
        catch (SmtpException ex)
        {
            Console.WriteLine("SMTP error: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("IO error: " + ex.Message);
        }
        catch (AuthenticationException ex)
        {
            Console.WriteLine("Authentication error: " + ex.Message);
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }

    //Response result = new Response();
    //var builder = new CustomSqlCommandBuilder();
    ////string configFilePath = Path.Combine(AppContext.BaseDirectory, "config.local.txt");
    //string configFilePath = "C:\\Users\\epik1\\source\\repos\\SS.Backend\\config.local.txt";
    //ConfigService configService = new ConfigService(configFilePath);
    //SqlDAO dao = new SqlDAO(configService);

    //var selectCommand = builder
    //            .BeginSelectAll()
    //            .From("userProfile")
    //            .Where($"hashedUsername = 'helloworld'")
    //            .Build();
    //result = await dao.ReadSqlResult(selectCommand);

    //result.PrintDataTable();

    //Response result = new Response();
    //var builder = new CustomSqlCommandBuilder();
    //string configFilePath = Path.Combine(AppContext.BaseDirectory, "config.local.txt");
    //ConfigService configService = new ConfigService(configFilePath);
    //GenOTP genotp = new GenOTP();
    //Hashing hasher = new Hashing();
    //SqlDAO dao = new SqlDAO(configService);
    //SqlLogTarget target = new SqlLogTarget(dao);
    //Logger log = new Logger(target);
    //SSAuthService auth = new SSAuthService(genotp, hasher, dao, log);
    //AuthenticationRequest request = new AuthenticationRequest();
    //SSPrincipal principal = new SSPrincipal();

    //Console.WriteLine("Enter username: ");
    //string username = Console.ReadLine();

    //request.UserIdentity = username;
    //request.Proof = null;

    //(string otp, result) = await auth.SendOTP_and_SaveToDB(request);

    //if (result.HasError == false)
    //{
    //    Console.WriteLine($"You have received OTP: {otp}");
    //    Console.WriteLine("Enter password: ");
    //    string password = Console.ReadLine();
    //    request.Proof = password;

    //    (principal, result) = await auth.Authenticate(request);

    //    if (result.HasError == false)
    //    {
    //        Console.WriteLine("Successful authentication!");
    //        var requiredClaims = new Dictionary<string, string>
    //        {
    //            {"Role", "Admin"}
    //        };
    //        bool isAuthZ = await auth.IsAuthorize(principal, requiredClaims);

    //        if (isAuthZ)
    //        {
    //            Console.WriteLine("Successful authorization!");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Failed to authorize.");
    //            Console.WriteLine(result.ErrorMessage);
    //        }
    //    }
    //    else
    //    {
    //        Console.WriteLine("Failed to authenticate.");
    //        Console.WriteLine(result.ErrorMessage);
    //    }
    //}
    //else
    //{
    //    Console.WriteLine("Send and save failed");
    //    Console.WriteLine(result.ErrorMessage);
    //}


}