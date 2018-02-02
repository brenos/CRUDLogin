using CRUDLogin.ADO.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Configuration;
using System.Xml.Linq;

namespace CRUDLogin.Bussiness.Gerador.Config
{
    class WebConfigBO
    {
        private ParametroTO _ParametroTO { get; set; }
        private StringBuilder _SystemWebProfileElements { get; set; }
        private StringBuilder _SystemWebMembershipElements { get; set; }
        private StringBuilder _SystemWebRoleElements { get; set; }
        private StringBuilder _SystemWebAutenticationElements { get; set; }
        private StringBuilder _SystemWebAuthorizationElements { get; set; }
        private StringBuilder _SystemWebSessionStateElements { get; set; }
        private StringBuilder _LocationsForgotElements { get; set; }
        private StringBuilder _LocationsHomeElements { get; set; }
        private StringBuilder _ConnectionStringElement { get; set; }

        public WebConfigBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
            Set_SystemWebProfileElements();
            Set_SystemWebMembershipElements();
            Set_SystemWebRoleElements();
            Set_SystemWebAutenticationElements();
            Set_SystemWebAuthorizationElements();
            Set_SystemWebSessionStateElements();
            Set_LocationsForgotElements();
            Set_LocationHomeElements();
            Set_ConnectionString();
        }

        public void AddTagsNoWebConfig()
        {
            //Configuration config = WebConfigurationManager.OpenWebConfiguration(_ParametroTO.Pasta);
            //config.SectionGroups.Get("system.web");
            XElement webConfig = XElement.Load(_ParametroTO.Pasta + "\\Web.config");
            
            foreach (var node in webConfig.Elements())
            {
                if (node.Name.LocalName.Equals("system.web"))
                {
                    node.Add(XElement.Parse(_SystemWebProfileElements.ToString()));
                    node.Add(XElement.Parse(_SystemWebMembershipElements.ToString()));
                    node.Add(XElement.Parse(_SystemWebRoleElements.ToString()));
                    node.Add(XElement.Parse(_SystemWebAutenticationElements.ToString()));
                    node.Add(XElement.Parse(_SystemWebAuthorizationElements.ToString()));
                    node.Add(XElement.Parse(_SystemWebSessionStateElements.ToString()));
                    node.AddAfterSelf(XElement.Parse(_LocationsForgotElements.ToString()));
                    node.AddAfterSelf(XElement.Parse(_LocationsHomeElements.ToString()));
                }

                if (node.Name.LocalName.Equals("connectionStrings"))
                {
                    node.Add(XElement.Parse(_ConnectionStringElement.ToString()));
                }
            }

            webConfig.Save(_ParametroTO.Pasta + "\\Web.config");
        }

        private void Set_SystemWebProfileElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<profile defaultProvider=\"DefaultProfileProvider\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultProfileProvider\" type=\"System.Web.Providers.DefaultProfileProvider, System.Web.Providers, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"MembershipConnection\" applicationName=\"" + _ParametroTO.NmProjeto + "\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</profile>");
            _SystemWebProfileElements = sb;
        }

        private void Set_SystemWebMembershipElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<membership defaultProvider=\"DefaultMembershipProvider\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultMembershipProvider\" type=\"System.Web.Providers.DefaultMembershipProvider, System.Web.Providers, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"MembershipConnection\" enablePasswordRetrieval=\"false\" enablePasswordReset=\"true\" requiresQuestionAndAnswer=\"false\" requiresUniqueEmail=\"false\" maxInvalidPasswordAttempts=\"5\" minRequiredPasswordLength=\"6\" minRequiredNonalphanumericCharacters=\"0\" passwordAttemptWindow=\"10\" applicationName=\"" + _ParametroTO.NmProjeto + "\" passwordFormat=\"Hashed\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</membership>");
            _SystemWebMembershipElements = sb;
        }

        private void Set_SystemWebRoleElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<roleManager defaultProvider=\"DefaultRoleProvider\" enabled=\"true\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultRoleProvider\" type=\"System.Web.Providers.DefaultRoleProvider, System.Web.Providers, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"MembershipConnection\" applicationName=\"" + _ParametroTO.NmProjeto + "\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</roleManager>");
            _SystemWebRoleElements = sb;
        }

        private void Set_SystemWebAutenticationElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<authentication mode=\"Forms\">");
            sb.AppendLine("<forms loginUrl=\"~/Account/Login\" name=\"OCCookie\" timeout=\"30\" slidingExpiration=\"true\" />");
            sb.AppendLine("</authentication>");
            _SystemWebAutenticationElements = sb;
        }

        private void Set_SystemWebAuthorizationElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<authorization>");
            sb.AppendLine("<deny users=\"?\" />");
            sb.AppendLine("<!--<allow users=\"*\" />-->");
            sb.AppendLine("<!--<allow users=\"?\" />-->");
            sb.AppendLine("</authorization>");
            _SystemWebAuthorizationElements = sb;
        }

        private void Set_SystemWebSessionStateElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<sessionState mode=\"InProc\" customProvider=\"DefaultSessionProvider\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultSessionProvider\" type=\"System.Web.Providers.DefaultSessionStateProvider, System.Web.Providers, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"MembershipConnection\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</sessionState>");
            _SystemWebSessionStateElements = sb;
        }

        private void Set_LocationsForgotElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<location path=\"Account/Forgot\" allowOverride=\"true\">");
            sb.AppendLine("<system.web>");
            sb.AppendLine("<authorization>");
            sb.AppendLine("<allow users=\"*\" />");
            sb.AppendLine("</authorization>");
            sb.AppendLine("</system.web>");
            sb.AppendLine("</location>");
            _LocationsForgotElements = sb;
        }

        private void Set_LocationHomeElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<location path=\"Home\" allowOverride=\"true\">");
            sb.AppendLine("<system.web>");
            sb.AppendLine("<authorization>");
            sb.AppendLine("<allow users=\"*\" />");
            sb.AppendLine("</authorization>");
            sb.AppendLine("</system.web>");
            sb.AppendLine("</location>");
            _LocationsHomeElements = sb;
        }

        private void Set_ConnectionString()
        {
            StringBuilder sb = new StringBuilder("<add name=\"MembershipConnection\" providerName=\"System.Data.SqlClient\" connectionString=\"Data Source=");
            sb.Append(_ParametroTO.Conexao);
            sb.Append(";Initial Catalog=");
            sb.Append(_ParametroTO.Base);
            sb.Append("Integrated Security=SSPI;MultipleActiveResultSets=true\"/>");
            _ConnectionStringElement = sb;
        }
    }
}
