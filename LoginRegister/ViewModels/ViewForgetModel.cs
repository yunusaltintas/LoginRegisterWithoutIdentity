﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegister.ViewModels
{
    public class ViewForgetModel
    {
        public string Email { get; set; }
        public int ForgetCode { get; set; }
        public string Password1 { get; set; }
        public string Password2 { get; set; }

    }
}
