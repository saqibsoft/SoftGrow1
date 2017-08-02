using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class Users
    {
        private int _UserID;
        private string _UserName;
        private string _Password;
        private bool _IsAdmin;
        private bool _IsSuperAdmin;
        private bool _IsActive;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }
        public bool IsSuperAdmin
        {
            get { return _IsSuperAdmin; }
            set { _IsSuperAdmin = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

    }
}
