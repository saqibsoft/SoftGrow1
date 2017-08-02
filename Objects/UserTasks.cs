using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class UserTasks
    {
        private int _SerialNo;
        private int _UserID;
        private string _TaskKey;

        public int SerialNo
        {
            get { return _SerialNo; }
            set { _SerialNo = value; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string TaskKey
        {
            get { return _TaskKey; }
            set { _TaskKey = value; }
        }
    }
}
