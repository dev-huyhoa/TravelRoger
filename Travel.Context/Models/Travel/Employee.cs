﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Shared.Models
{
    public class Employee
    {       
        private Guid id;
        private string name;
        private string email;
        private string password;
        private long birthday ;
        private string image;
        private string phone;

        private int roleId;

        private int status;
        private long createDate;    
        private string accessToken;
      
        private string modifyBy;
        private long modifyDate; 
    
        private bool isDelete;
        private bool isActive;

        public Guid Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public long Birthday { get => birthday; set => birthday = value; }
        public string Image { get => image; set => image = value; }
        public string Phone { get => phone; set => phone = value; }
        public int RoleId { get => roleId; set => roleId = value; } 
        public int Status { get => status; set => status = value; }
        public long CreateDate { get => createDate; set => createDate = value; } 
        public string AccessToken { get => accessToken; set => accessToken = value; }
        public string ModifyBy { get => modifyBy; set => modifyBy = value; }
        public long ModifyDate { get => modifyDate; set => modifyDate = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
    }

}
