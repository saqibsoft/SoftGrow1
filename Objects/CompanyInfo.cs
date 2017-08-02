using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class CompanyInfo
    {       
       private string _CompanyName;
       private string _Address;
       private string _Phone;
       private string _Web;
       private byte[] _Logo;
     
       public string CompanyName
       {
           get { return _CompanyName; }
           set { _CompanyName = value; }
       }

       public string Address
       {
           get { return _Address; }
           set { _Address = value; }
       }

       public string Phone
       {
           get { return _Phone; }
           set { _Phone = value; }
       }

       public string Web
       {
           get { return _Web; }
           set { _Web = value; }
       }

       public byte[] Logo
       {
           get { return _Logo; }
           set { _Logo = value; }
       }

    }
}
