using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
   public class ShareMembers
    {
        private Int64 _MemberID;
        private DateTime _RegistrationDate;
        private byte [] _MemberPic;
        private string _MemberName;
        private string _FatherName;
        private string _VillageName;
        private string _CityName;
        private string _ContactNo;
        private string _CNINCNo;
        private string _Occupation;
        private string _PropertDetail;
        private string _PostalAddress;
        private string _Remarks;
        private string _AccountNo;

        public Int64 MemberID { get { return _MemberID; } set { _MemberID = value; } }
        public DateTime RegistrationDate { get { return _RegistrationDate; } set { _RegistrationDate = value; } }
        public byte[] MemberPic { get { return _MemberPic; } set { _MemberPic = value; } }
        public string MemberName { get { return _MemberName; } set { _MemberName = value; } }
        public string FatherName { get { return _FatherName; } set { _FatherName = value; } }
        public string VillageName { get { return _VillageName; } set { _VillageName = value; } }
        public string CityName { get { return _CityName; } set { _CityName = value; } }
        public string ContactNo { get { return _ContactNo; } set { _ContactNo = value; } }
        public string CNINCNo { get { return _CNINCNo; } set { _CNINCNo = value; } }
        public string Occupation { get { return _Occupation; } set { _Occupation = value; } }
        public string PropertDetail { get { return _PropertDetail; } set { _PropertDetail = value; } }
        public string PostalAddress { get { return _PostalAddress; } set { _PostalAddress = value; } }
        public string Remarks { get { return _Remarks; } set { _Remarks = value; } }
        public string AccountNo { get { return _AccountNo; } set { _AccountNo = value; } }
    }
}
