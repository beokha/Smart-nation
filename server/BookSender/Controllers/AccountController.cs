﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookSender.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using GmailSender.Model;
using GmailSender;
using BookSender.Data;
using BookSender.Data.Models;
using BookSender.Data.Models.AccessoryModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using LoginData = BookSender.Data.Models.AccessoryModels.LoginModel;
using System.Text.RegularExpressions;

namespace BookSender.Controllers
{
    [EnableCors("CorsPolicy")]
    public class AccountController : Controller
    {
        private ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Register(string request)
        {
            try
            {
                dynamic requestDyn = JsonConvert.DeserializeObject(request);

                RegisterModel model = new RegisterModel { Phone = requestDyn.Phone, Password = requestDyn.Password };

                BookSender.Data.Models.User user = await _context.Users.FirstOrDefaultAsync(u => u.Number == model.Phone);
                if (user == null)
                {
                    user = new BookSender.Data.Models.User { Number = model.Phone, Password = model.Password };
                    BookSender.Data.Models.Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");

                    if (userRole != null)
                        user.Role = userRole;

                    _context.Users.Add(user);

                    await _context.SaveChangesAsync();

                    //string key = GmailSender.SmtpClientLibrary.SendKey("", "", "");
                    //await Authenticate(user);

                    return Json($" 'Answer' : 'Successful user creation'");
                }
                else
                    return Json(" 'Answer' : 'Unsuccessful user creation' ");
            }
            catch (Exception ex)
            {
                return Json($" 'Answer' : ' Error = {ex.Message}' ");
            }
        }

        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginData model)
        {
            //string request = null;
            try
            {
                if (model != null)
                {
                    Regex regexEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Regex regexPhone = new Regex(@"^+(\d{3}+)(\d{9}+)$");
                    Match match = regexEmail.Match(model.Email);


                    if (match.Success)
                    {
                        BookSender.Data.Models.User user = await _context.Users
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                        AccountLoginResponce acc = new AccountLoginResponce
                        {
                            Name = user.FirstName,
                            Surname = user.LastName,
                            Role = user.Role.Name,
                          // StatusCode = StatusCode(500).ToString()
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(acc);

                        return Json(json);
                    }
                    else if (String.IsNullOrEmpty(model.Phone) == false)
                    {
                        BookSender.Data.Models.User user = await _context.Users
                            .Include(u => u.Role)
                            .FirstOrDefaultAsync(u => u.Number == model.Phone && u.Password == model.Password);

                        AccountLoginResponce acc = new AccountLoginResponce
                        {
                            Name = user.FirstName,
                            Surname = user.LastName,
                            Role = user.Role.Name,
                            // StatusCode = StatusCode(500).ToString()
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(acc);

                        return Json(json);
                    }
                    else
                    {
                        return Json("'Answer': 'Wrong user credetials'");
                    }
                }
                else
                    return Json(new LoginData());
            }
            catch (Exception ex)
            {
                return Json($" 'Answer' : 'Error = {ex.Message}' ");
            }
        }

        [HttpPut]
        public async Task<JsonResult> NewPassword(string request)
        {
            try
            {
                dynamic requestDyn = JsonConvert.DeserializeObject(request);

                BookSender.Data.Models.AccessoryModels.LoginModel model = 
                         new BookSender.Data.Models.AccessoryModels.LoginModel
                         { Email = requestDyn.Email, Password = requestDyn.Password, Phone = requestDyn.Phone };

                BookSender.Data.Models.User user = await _context.Users
                         .Include(u => u.Role)
                         .FirstOrDefaultAsync(u => u.Email == model.Email || u.Number == model.Phone);

                if (String.IsNullOrEmpty(model.Password) == false)
                {
                    user.Password = model.Password;
                    await _context.SaveChangesAsync();

                    return Json(" 'Answer' : 'Password was successfully updated' ");
                }
                else
                    throw new Exception("Empty password string");
            }
            catch (Exception ex)
            {
                return Json($" 'Answer' ; 'Something goes wrong', 'Error' : '{ex.Message}' ");
            }
        }
    }
}