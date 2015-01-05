using System.Management;
using System.IO;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

using addPrinters;


class ninjaPrinters
{
    public static void Main()
    {

        installPrinterDb();

      
      
      
      

       Console.ReadKey();
    }


    public static void installPrinterDb()
    {
       // Dictionary<string, string> dic = new Dictionary<string, string>(); //hash table for fast query
        String[] links = {"http://www.columbia.edu/acis/facilities/printers/ninja/acis/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/barnard/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/cait/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/lso/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/meche/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/old_pcd/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/soa/js/printers.js",
"http://www.columbia.edu/acis/facilities/printers/ninja/ssw/js/printers.js"};

      //  foreach (string link in links)
      //  {
        string link = links[0];
            StreamReader sr = openURL(link);
            int count = 0;
            while (!sr.EndOfStream)
            {
                string anEntry=sr.ReadLine();
                if (anEntry.TrimStart().StartsWith("//")|| anEntry.Trim()=="") //i can't believe this has a startsWith method!
                {
                    continue; //skip comments and empty lines
                }
                else {
                    try { 
                
               Dictionary<string,string> printDic= buildStr(anEntry);
               PrintPort port = new PrintPort(printDic["name"], printDic["addr"]);
               Printer printer = new Printer(printDic["name"], printDic["driver"], printDic["location"], port);
               Console.WriteLine(printDic["name"]);
               Console.WriteLine(printDic["addr"]);
               Console.WriteLine(printDic["driver"]);
               Console.WriteLine(printDic["location"]);
               count++;

                        }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        count++;
                    }
                }

                if (count == 2) break;
            }
            
            


      //  }
        
        
        
        

    }

    public static StreamReader openURL(String str)
    {
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(str);
        StreamReader reader = new StreamReader(stream);
        return reader;
    }
 
    public static Dictionary<string,string> buildStr(string lin){
         String[] strArr = lin.Split(new char[] { '[', ']' });
       string[] print = strArr[3].Split(',');
       Dictionary<string, string> printDic = new Dictionary<string, string>();
        //scrub data
          printDic["name"]= print[0].Replace("\"", "").Trim();
          printDic["addr"] = print[1].Replace("\"", "").Trim();
          printDic["driver"] = print[2].Replace("\"", "").Trim();
          printDic["location"] = print[3].Replace("\"", "").Trim();
          

          return printDic;
    }
   
}