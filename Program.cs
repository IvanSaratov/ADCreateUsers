using System;
using System.DirectoryServices;

namespace ADCreateUsers
{
    class CreateUser
    {
        [STAThread]
        static void Main(string[] args)
        {
            DirectoryEntry objADAM;
            DirectoryEntry objUser;

            string ldapPath = "LDAP://main.sgu.ru/DC=main,DC=sgu,DC=ru";

            Console.WriteLine("Bind to {0}", ldapPath);

            try
            {
                objADAM = new DirectoryEntry(ldapPath);
                objADAM.RefreshCache();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: Bind failed");
                Console.WriteLine("{0}",e.Message);
                return;
            }

            string strUser = "CN=TestMatest";
            string strDisplayName = "Test Matest Display";
            string strUserPrincipalName = "TestMatest@main.sgu.ru";
            Console.WriteLine("Create: {0}", strUser);

            try
            {
                objUser = objADAM.Children.Add(strUser, "user");
                objUser.Properties["displayName"].Add(strDisplayName);
                objUser.Properties["userPrincipalName"].Add(strUserPrincipalName);
                objUser.CommitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Create failed");
                Console.WriteLine("{0}", e.Message);
                return;
            }

            // Output User attributes.
            Console.WriteLine("Success: Create succeeded.");
            Console.WriteLine("Name:    {0}", objUser.Name);
            Console.WriteLine("         {0}",
                objUser.Properties["displayName"].Value);
            Console.WriteLine("         {0}",
                objUser.Properties["userPrincipalName"].Value);
            return;
        }
    }
}
