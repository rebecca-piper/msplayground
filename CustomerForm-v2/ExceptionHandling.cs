using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public class ExceptionHandling
    {
        string sName;
        string sEmail;
        int iNum;
        string sPostcode;

        public ExceptionHandling()
        {
           
        }
        public void NameException(string pName)
        {
            sName = pName;
            try
            {
                throw new Exception();
            }
            catch (Exception exName)
            {

                if (sName == "")
                {
                    throw new ExceptionMessage("Please enter a name", exName);
                }

            }
        }
        public void EmailException(string pEmail)
        {
            sEmail = pEmail;
            try
            {
                throw new Exception();
            }
            catch (Exception exName)
            {

                if (sEmail == "")
                {
                    throw new ExceptionMessage("Please enter an email", exName);
                }

            }
        }

        public void FormException(string pName, string pEmail, int pNum, string pPostcode)
        {
            sName = pName;
            sEmail = pEmail;
            iNum = pNum;
            sPostcode = pPostcode;
           
            
            
        }

    }
}
