using Capstone.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using Google.Apis.Gmail.v1;
using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Capstone.Security;
using System.ComponentModel;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Google.Apis.Util;
using Nest;
using Org.BouncyCastle.Ocsp;
using static System.Net.WebRequestMethods;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Capstone.Email_Stuff
{
    public class GmailClient
    {
        public static void TestMessage(string recepientName, string recepientEmail, string senderName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, "agaveatlas@hotmail.com"));
            message.To.Add(new MailboxAddress(recepientName, recepientEmail));
            message.Subject = "Testing Email";

            message.Body = new TextPart("plain")
            {
                Text = @"This message is a test!"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, false);

                client.Authenticate("agaveatlas@hotmail.com", "KaitlinLovesMargs23!");
                try
                {
                    client.Send(message);
                    client.Disconnect(true);
                    message.Dispose();
                }
                catch (SmtpException e)
                {
                    Console.WriteLine(e.StatusCode);
                    client.Disconnect(true);
                    message.Dispose();
                }
            }
        }
        public static void SendInvite(string recepientName, string recepientEmail, string senderName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, "agaveatlas@hotmail.com"));
            message.To.Add(new MailboxAddress(recepientName, recepientEmail));
            message.Subject = "Welcome to Agave Atlas!";

            var builder = new BodyBuilder();
            builder.TextBody = @$"You've been invited!

Hello, {recepientName}
Your friend, {senderName}, would love to share a margarita with you on Agave Atlas!

https://twitter.com/AgaveAtlas
            ";

            builder.HtmlBody = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css"" integrity=""sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2"" crossorigin=""anonymous"">
    <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
<link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
<link href=""https://fonts.googleapis.com/css2?family=Oleo+Script&display=swap"" rel=""stylesheet"">
    
    <title>Invite Email</title>
</head>
<body style=""background-color: #F3FAEF;"">
    <header style=""display:flex; flex-direction:row; "">
        <div id=""header-background"" class=""background container"" >
            <div id=""header-text"" style=""text-align:center; margin:auto;"">
                <h1 style=""text-align: center; font-family: 'Oleo Script', Arial, Helvetica, sans-serif; font-size: 3em; margin:auto;"">You've been invited!</h1>
            </div>
        </div>
    </header>
    <hr style=""border-width: 3px; border-color: black; width: 50vw;""/>
    <div class=""container"" id=""body-block"">
        <div id=""body-background"" class=""background container"">
            <div id=""body-text"" style=""display: flex; flex-direction: column; flex-wrap: wrap; text-align: center;"">
                <p style=""display: block; font-size: 1.5em; font-family: 'Oleo', Arial, Helvetica, sans-serif;"">Your friend {senderName} would love to share a margarita with you on Agave Atlas!</p>
                <a type=""button"" style="" display: block; background-color: #7bc950; color: white; margin: auto; padding: 5px 15px; border: none; border-radius: 20px; cursor: pointer; width: 50%;"" href=""https://twitter.com/AgaveAtlas""> Click here to join the party!</a>
            </div>
        </div>
    </div>
</body>
</html>";

            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, false);

                client.Authenticate("agaveatlas@hotmail.com", "KaitlinLovesMargs23!");
                try
                {
                    client.Send(message);
                    client.Disconnect(true);
                    message.Dispose();
                }
                catch (SmtpException e)
                {
                    Console.WriteLine(e.StatusCode);
                    client.Disconnect(true);
                    message.Dispose();
                }
            }
        }
    }
}
