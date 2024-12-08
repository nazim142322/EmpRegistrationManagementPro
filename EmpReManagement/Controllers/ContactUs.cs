﻿using EmpReManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmpReManagement.Controllers
{
    public class ContactUs : Controller
    {
        private readonly EmailService emailservice;
        public ContactUs(EmailService emailservice)
        {
            this.emailservice = emailservice;
        }

        // GET: ContactUs/Contact
        public async Task<IActionResult> Contact()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("UserSession")))
            {
                return RedirectToAction("Login", "UserLoginRegistration");
            }
            await Task.Delay(100);
            return View();
        }

        //[HttpGet, ActionName("Contact")]
        [HttpPost]
        public async Task<IActionResult> Contact(string Name, string Email, string Phone, string Message)
        {
            string name = Name.Trim();
            var email = Email.Trim();
            var phoneNo = Phone.Trim();
            var message = Message.Trim();

            bool isEmailSent = await emailservice.SendEmailAsync(name, email, phoneNo, message);

            if (isEmailSent)
            {
                ViewBag.Message = "Your message has been sent successfully!";
                //ViewBag.MessageType = "success";
            }
            else
            {
                ViewBag.Message = "There was an error sending your message. Please try again.";
                //ViewBag.MessageType = "error";
            }
            return View("Contact");
        }
    }
}
