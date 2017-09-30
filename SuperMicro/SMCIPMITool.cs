///-----------------------------------------------------------------
///   Namespace:      SuperMicro
///   Class:          IPMITool
///   Description:    The Class Control the IPMITool
///   Author:         Alex Shoyhit                    Date: 22/2/17
///   Notes:          
///   Revision History:
///   Name:Alex Shoyhit           Date:22/2/17        Description:Init
///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMicro
{
    class SMCIPMITool
    {
        public enum Power
        {
            up,
            down
        }

        public enum FanMode
        {
            StandartSpeed,
            FullSpead,
            OptimalSpeed,
            HeavyIOSpeed
        }

        private string sExeFilePath = "";//File IPMI tool Path
        private string sIP = "";//IPMI IP
        private string sUser = "";//IPMI User
        private string sPassword = "";//IPMI Password
        private string sBaseString = "";//IP USER PASSWORD
        Process proc = null;

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="sExeFilePath"></param>
        /// <param name="sIP"></param>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        public SMCIPMITool(string sExeFilePath,string sIP,string sUser,string sPassword)
        {
            this.sExeFilePath = sExeFilePath;
            this.sIP = sIP;
            this.sUser = sUser;
            this.sPassword = sPassword;
            this.sBaseString = sIP + ' ' + sUser + ' ' + sPassword + ' ';
            
        }

        /// <summary>
        /// Connect to IPMI tool
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                proc = new Process();
                if (!File.Exists(sExeFilePath))
                {
                    return false;
                }
                proc.StartInfo.FileName = sExeFilePath;
                proc.StartInfo.Arguments =this.sBaseString+"ipmi ver";
                proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.RedirectStandardInput = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();
                string sLine = proc.StandardOutput.ReadToEnd().Trim();
                if (!sLine.Equals("Can't connect to "+this.sIP))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Set Power UP/Down
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public string SetPower(Power power)
        {
            proc.StartInfo.Arguments = this.sBaseString + "ipmi power "+power;
            proc.Start();
            proc.WaitForExit();
            string sLine = proc.StandardOutput.ReadToEnd().Trim();
            return sLine;
        }

        /// <summary>
        /// Set Fan Speed StandartSpeed/FullSpead/OptimalSpeed/HeavyIOSpeed
        /// </summary>
        /// <param name="fanmode"></param>
        /// <returns></returns>
        public string SetFanMode(FanMode fanmode)
        {
            proc.StartInfo.Arguments = this.sBaseString + "ipmi fan " + Convert.ToInt32(fanmode);
            proc.Start();
            proc.WaitForExit();
            string sLine = proc.StandardOutput.ReadToEnd().Trim();
            return sLine;
        }


    }
}
