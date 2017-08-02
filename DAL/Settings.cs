using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Specialized;


namespace DAL
{
  public  class Settings
    {
        public string connectionstring;

        public enum ProSettings
        {
            CashAccTitle=0,
            OpCashDebit=1,
            OpCashCredit=2,
            IsAutoPost=3,
            MsgSounds=4,
            MinToTry=5,
            SalaryExpAcc=6,
            SampleIssuance=7,
            ProductWastage = 8
        };

        public string GetSettingValue(ProSettings vKey)
        {
            string query = @"SELECT  KeyValue from ProjectSettings where ProjectKey='{0}'   ";
            DataSet ds = new DataSet();

            try
            {
                ds = new Database(connectionstring).ExecuteForDataSet(string.Format(query, vKey.ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else return string.Empty;
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public void SetSettingValue(ProSettings vKey,string vValue)
        {
            string query = @"Update  ProjectSettings  SET KeyValue='{1}' where ProjectKey='{0}'   ";            
            try
            {
                new Database(connectionstring).ExecuteNonQueryOnly(string.Format(query,vKey.ToString(),vValue));
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

    }
}
