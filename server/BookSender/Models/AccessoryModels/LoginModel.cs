﻿namespace BookSender.Models.AccessoryModels
{
	public class LoginModel
    {
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong adress")]
        public string userLogInfo { get; set; }

        public string Password { get; set; }
        
    }
}
