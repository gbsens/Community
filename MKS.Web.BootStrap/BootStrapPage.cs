using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MKS.Core;
using MKS.Web.MVP;
using System.Text;
using System.Web.UI;

namespace MKS.Web
{ 

    /// <summary>
    /// Page de base pour les applications MVP avec Bootstrap.
    /// </summary>
    /// <remarks>La page Master doit contenir un control pour recevoirun LiteralControl</remarks>
    public class BootStrapPage:MKS.Web.MVP.Page
    {
        string CRTLMSG = null;
        bool IsInMasterPage = false;
        public BootStrapPage(string ctrlMsgName, bool isInMasterPage=false)
        {
            CRTLMSG = ctrlMsgName;
            IsInMasterPage = isInMasterPage;
        }

        public string GetSeverity(MKS.Core.Severity severity)
        {
            switch (severity)
            {
                case MKS.Core.Severity.Error:
                    //msg.ForeColor = System.Drawing.Color.Red;
                    return "danger";


                case MKS.Core.Severity.Warning:
                    //msg.ForeColor = System.Drawing.Color.OrangeRed;
                    return "warning";

                case MKS.Core.Severity.Information:
                    //msg.ForeColor = System.Drawing.Color.Blue;
                    return "info fade in";

                case MKS.Core.Severity.Success:
                    //msg.ForeColor = System.Drawing.Color.Green;
                    return "success fade in";
            }
            return null;
        }


        public override void ShowReservation(string title, string message, ProcessResults processResults)
        {
            StringBuilder sb = new StringBuilder();
            Control crtlMSG = null;
            if (IsInMasterPage)
                crtlMSG= Master.FindControl(CRTLMSG);
            else
                crtlMSG = this.FindControl(CRTLMSG);

            foreach (var item in processResults.MessagesList)
            {

                sb.AppendLine("<br><div id = 'MKSMSG" + item.CodeMessage + "'  class='alert alert-" + GetSeverity(item.Severity) + "'>");
                sb.AppendLine("<a href = '#' class='close' data-dismiss='alert'>&times;</a>");
                sb.AppendLine("<strong>" + item.CodeMessage + "</strong><br>" + item.Description);
                sb.AppendLine("</div>");
            }
            crtlMSG.Controls.Add(new LiteralControl(sb.ToString()));
        }

        public override void ShowMessage(string title, string message, Severity severity)
        {
            StringBuilder sb = new StringBuilder();
            Control crtlMSG = null;
            if (IsInMasterPage)
                crtlMSG = Master.FindControl(CRTLMSG);
            else
                crtlMSG = this.FindControl(CRTLMSG);

            sb.AppendLine("<br><div id = 'MKSMSG' class='alert alert-" + GetSeverity(severity) + "'>");
            sb.AppendLine("<a href = '#' class='close' data-dismiss='alert'>&times;</a>");
            sb.AppendLine("<strong>" + title + "</strong><br>" + message);
            sb.AppendLine("</div>");

            crtlMSG.Controls.Add(new LiteralControl(sb.ToString()));

        }

        public override void ShowBusinessValidation(string title, string message, ProcessResults processResults)
        {
            StringBuilder sb = new StringBuilder();
            Control crtlMSG = null;
            if (IsInMasterPage)
                crtlMSG = Master.FindControl(CRTLMSG);
            else
                crtlMSG = this.FindControl(CRTLMSG);

            foreach (var item in processResults.MessagesList)
            {

                sb.AppendLine("<br><div id = 'MKSMSG" + item.CodeMessage+"'  class='alert alert-" + GetSeverity(item.Severity) + "'>");
                sb.AppendLine("<a href = '#' class='close' data-dismiss='alert'>&times;</a>");
                sb.AppendLine("<strong>" + item.CodeMessage + "</strong><br>" + item.Description);
                sb.AppendLine("</div>");
            }
            crtlMSG.Controls.Add(new LiteralControl(sb.ToString()));

        }
        public override void ShowContextValidation(string title, string message, List<ReturnMessage> result)
        {
            StringBuilder sb = new StringBuilder();
            Control crtlMSG = null;
            if (IsInMasterPage)
                crtlMSG = Master.FindControl(CRTLMSG);
            else
                crtlMSG = this.FindControl(CRTLMSG);

            foreach (var item in result)
            {

                sb.AppendLine("<br><div id = 'MKSMSG" + item.CodeMessage + "'  class='alert alert-" + GetSeverity(item.Severity) + "'>");
                sb.AppendLine("<a href = '#' class='close' data-dismiss='alert'>&times;</a>");
                sb.AppendLine("<strong>" + item.CodeMessage + "</strong><br>" + item.Description);
                sb.AppendLine("</div>");
            }
            crtlMSG.Controls.Add(new LiteralControl(sb.ToString()));
        }
        public override void ShowSecurity(string title, string message, ProcessResults processResults)
        {
            StringBuilder sb = new StringBuilder();
            Control crtlMSG = null;
            if (IsInMasterPage)
                crtlMSG = Master.FindControl(CRTLMSG);
            else
                crtlMSG = this.FindControl(CRTLMSG);

            foreach (var item in processResults.MessagesList)
            {

                sb.AppendLine("<br><div id = 'MKSMSG" + item.CodeMessage + "'  class='alert alert-" + GetSeverity(item.Severity) + "'>");
                sb.AppendLine("<a href = '#' class='close' data-dismiss='alert'>&times;</a>");
                sb.AppendLine("<strong>" + item.CodeMessage + "</strong><br>" + item.Description);
                sb.AppendLine("</div>");
            }
            crtlMSG.Controls.Add(new LiteralControl(sb.ToString()));
        }
    }
}