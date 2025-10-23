﻿using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.ExternalServices;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string emailTo, string subject, string? body = null, IList<IFormFile> attachments = null)
        {
            var email = CreateEmailMessage(emailTo, subject); // Create the email message

            // Build the email body
            body = BuildEmailBody(body!, subject);

            // Set the HTML body to the email message
            email.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

            // Handle attachments if they exist
            if (attachments != null)
            {
                AddAttachments(email, attachments);
            }

            // Send the email using SMTP
            var result = await SendEmailAsync(email);
            return result;
        }

        private MimeMessage CreateEmailMessage(string emailTo, string subject)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_configuration["Smtp:Username"]),
                Subject = subject
            };

            email.To.Add(MailboxAddress.Parse(emailTo));
            return email;
        }

        private string BuildEmailBody(string body, string? subject = "Confirm your email address ")
        {
            return $@"
    <html>
        <head>
            <style>
                h1 {{
                    color: #535154;
                    text-align: center;
                    font-family: Arial, Helvetica, sans-serif;
                    font-size: 30px;
                }}
                .p-1 {{
                    font-size: 15px;
                    color: rgb(36, 80, 82);
                }}
                .otp {{
                    padding-top: 6px;
                    padding-bottom: 6px;
                    padding-inline: 10px;
                    background-color: #fffff;
                    color: black;
                    font-weight: bold;
                    width:100%;
                    text-align: center;        
                    font-size: 16px;
                }}
                li {{
                    color: #555;
                    line-height: 1.5;
                }}
                ul {{
                    margin-inline: 15px;
                    padding-inline: 10px;
                }}
                .container {{
                    background-color: #f9f9f9;
                    border-radius: 5px;
                    padding: 20px;
                }}
                a {{
                    color: #007bff;
                    text-decoration: none;
                }}
                a:hover {{
                    text-decoration: underline;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>{subject} to get started on<span style='color: rgb(6, 130, 213)'>Split</span></h1>
                <p class='p-1'>
                    Once you've confirmed that <span style='color: rgb(6, 130, 213)'>{{kaloyan.yankulov@gmail.com}}</span> 
                    is your email address, we'll help you find your Split workspaces or create a new one.
                </p>
                <div class='otp'>
                    {body}
                </div>
               
            </div>
        </body>
    </html>
    ";
        }


        private void AddAttachments(MimeMessage email, IList<IFormFile> attachments)
        {
            var builder = new BodyBuilder();

            foreach (var file in attachments)
            {
                if (file.Length > 0)
                {
                    using var ms = new MemoryStream();
                    file.CopyTo(ms);
                    builder.Attachments.Add(file.FileName, ms.ToArray(), ContentType.Parse(file.ContentType)); // Add attachment
                }
            }

            email.Body = builder.ToMessageBody();
        }

        private async Task<bool> SendEmailAsync(MimeMessage email)
        {
            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_configuration["Smtp:Host"], _configuration.GetValue<int>("Smtp:Port"), SecureSocketOptions.StartTls); // Connect to SMTP server
                smtp.Authenticate(_configuration["Smtp:Username"], _configuration["Smtp:Password"]); // Authenticate using username and password
                await smtp.SendAsync(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }
        }
    }
}
