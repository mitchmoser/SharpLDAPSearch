using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

namespace SharpLDAPSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryEntry entry = new DirectoryEntry();
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            List<string> searchProperties = new List<string>();
            //get LDAP search filter
            if (args.Length > 0)
            {
                //example LDAP filter
                //mySearcher.Filter = ("(&(objectCategory=computer)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))");
                mySearcher.Filter = args[0];
                if (args.Length > 1)
                {
                    searchProperties = args[1].Split(',').ToList();
                    foreach (string myKey in searchProperties)
                    {
                        mySearcher.PropertiesToLoad.Add(myKey);
                    }
                }
            }
            else
            {
                Console.WriteLine("[!] No LDAP search provided");
                Environment.Exit(0);
            }

            //string[] searchProperties = { "name" };

            mySearcher.SizeLimit = int.MaxValue;
            mySearcher.PageSize = int.MaxValue;

            foreach (SearchResult mySearchResult in mySearcher.FindAll())
            {
                // Get the 'DirectoryEntry' that corresponds to 'mySearchResult'.  
                DirectoryEntry myDirectoryEntry = mySearchResult.GetDirectoryEntry();
                
                // Get the properties of the 'mySearchResult'.  
                ResultPropertyCollection myResultPropColl;
                myResultPropColl = mySearchResult.Properties;
                foreach (string myKey in myResultPropColl.PropertyNames)
                {
                    if (searchProperties.Count > 0)
                    {
                        if (searchProperties.Contains(myKey.ToString().ToLower()))
                        {
                            foreach (Object myCollection in myResultPropColl[myKey])
                            {
                                Console.WriteLine("{0} - {1}", myKey, myCollection);
                            }
                        }
                    }
                    else
                    {
                        foreach (Object myCollection in myResultPropColl[myKey])
                        {
                            Console.WriteLine("{0} - {1}", myKey, myCollection);
                        }
                    }
                }
                /*
                foreach (string myKey in myResultPropColl.PropertyNames)
                {
                    if (searchProperties.Contains(myKey.ToString().ToLower()))
                    {
                        foreach (Object myCollection in myResultPropColl[myKey])
                        {
                            Console.WriteLine("{0} - {1}", myKey, myCollection);
                        }
                    }
                }
                */
                mySearcher.Dispose();
                entry.Dispose(); ;
            }

        }
    }
}
