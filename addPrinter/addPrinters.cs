using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.ComponentModel;
using System.Collections;

namespace addPrinters
{
    class PrintPort
    {

        public ManagementScope scope { get; private set; }

        public PrintPort(string name,string ip)
        {
            string computer=".";
            ConnectionOptions connOptions = new ConnectionOptions();
            connOptions.Impersonation = ImpersonationLevel.Impersonate;
            
            scope = new ManagementScope(@"\\" + computer + @"\root\cimv2", connOptions);
            scope.Connect();
            ObjectGetOptions portGetOptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            ManagementPath portPath = new ManagementPath("Win32_TCPIPPrinterPort");
            ManagementClass portClass = new ManagementClass(scope, portPath, portGetOptions);
            ManagementObject newPort = portClass.CreateInstance();
            
            newPort["Name"] =  ip;
            newPort["Protocol"] =2 ;//for lpr
            newPort["HostAddress"] = ip;
            //newPort["PortNumber"] = 9100;//for lpr 9100 for RAW
            newPort["SNMPEnabled"] = false;
           // newPort["SNMPCommunity"]="public";
            newPort["Queue"] = "public";
            newPort["ByteCount"] = true;
            PutOptions portPutOps = new PutOptions();
            portPutOps.UseAmendedQualifiers = true;
            portPutOps.Type = PutType.UpdateOrCreate;

            newPort.Put(portPutOps);

      





        }

    }



    class Printer
    {



        public Printer(string name,string ip, string driver,string location, PrintPort port)
        {
            
            ObjectGetOptions printerGetOptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            ManagementPath printerPath = new ManagementPath("Win32_Printer");
            ManagementClass printerClass = new ManagementClass(port.scope, printerPath, printerGetOptions);
            ManagementObject printer = printerClass.CreateInstance();
            printer["DriverName"] = driver;
            printer["Portname"] = ip;
            printer["Name"] = name;
            printer["Shared"] = true;
            printer["Local"] = true;
            printer["Network"] = true;
            printer["Location"] = location;
            PutOptions printerPutOptions = new PutOptions();
            printerPutOptions.UseAmendedQualifiers = true;
            printerPutOptions.Type = PutType.UpdateOrCreate;
            printer.Put(printerPutOptions);
            


        }



    }
}