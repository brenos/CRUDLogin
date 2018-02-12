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

        public WebConfigBO(ParametroTO parametroTO)
        {
            _ParametroTO = parametroTO;
        }
        
        public void AddTagsNoWebConfig()
        {
            //Verificar se ja adicionou o nuget (Verifica se tem a tag Membership)
            //Caso tenha adicionado, é só alterar os application name e colocar outro atributos nas tags
            XElement webConfig = XElement.Load(_ParametroTO.Pasta + "\\Web.config");

            XElement systemWeb = webConfig.Element(XName.Get("system.web"));
            XElement profile = systemWeb.Element(XName.Get("profile"));
            XElement membership = systemWeb.Element(XName.Get("membership"));
            XElement role = systemWeb.Element(XName.Get("profile"));

            XElement connectionString = webConfig.Element(XName.Get("connectionStrings"));

            Set_SystemWebProfileElements(profile);
            Set_SystemWebMembershipElements(membership);
            Set_SystemWebRoleElements(role);
            systemWeb.Add(XElement.Parse(Get_SystemWebAutenticationElements()));
            systemWeb.Add(XElement.Parse(Get_SystemWebAuthorizationElements()));
            //Verifica o SessionState
            systemWeb.AddAfterSelf(XElement.Parse(Get_LocationsForgotElements()));
            systemWeb.AddAfterSelf(XElement.Parse(Get_LocationInstalarSistemaElements()));
            Set_ConnectionString(connectionString);
            
            webConfig.Save(_ParametroTO.Pasta + "\\Web.config");
        }

        private XElement Get_AddFromProvidersElement(XElement node)
        {
            XElement providers = node.Element(XName.Get("providers"));
            XElement add = providers.Element(XName.Get("add"));
            return add;
        }

        private void Set_SystemWebProfileElements(XElement profile)
        {
            Get_AddFromProvidersElement(profile).SetAttributeValue(XName.Get("applicationName"), _ParametroTO.NmProjeto);
            /**StringBuilder sb = new StringBuilder();
            sb.AppendLine("<profile defaultProvider=\"DefaultProfileProvider\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultProfileProvider\" type=\"System.Web.Providers.DefaultProfileProvider, System.Web.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"DefaultConnection\" applicationName=\"" + _ParametroTO.NmProjeto + "\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</profile>");
            _SystemWebProfileElements = sb;**/
        }

        private void Set_SystemWebMembershipElements(XElement membership)
        {
            XElement add = Get_AddFromProvidersElement(membership);
            add.SetAttributeValue(XName.Get("applicationName"), _ParametroTO.NmProjeto);
            /**StringBuilder sb = new StringBuilder();
            sb.AppendLine("<membership defaultProvider=\"DefaultMembershipProvider\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultMembershipProvider\" type=\"System.Web.Providers.DefaultMembershipProvider, System.Web.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"DefaultConnection\" enablePasswordRetrieval=\"false\" enablePasswordReset=\"true\" requiresQuestionAndAnswer=\"false\" requiresUniqueEmail=\"false\" maxInvalidPasswordAttempts=\"5\" minRequiredPasswordLength=\"6\" minRequiredNonalphanumericCharacters=\"0\" passwordAttemptWindow=\"10\" applicationName=\"" + _ParametroTO.NmProjeto + "\" passwordFormat=\"Hashed\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</membership>");
            _SystemWebMembershipElements = sb;**/
        }

        private void Set_SystemWebRoleElements(XElement role)
        {
            Get_AddFromProvidersElement(role).SetAttributeValue(XName.Get("applicationName"), _ParametroTO.NmProjeto);
            /*StringBuilder sb = new StringBuilder();
            sb.AppendLine("<roleManager defaultProvider=\"DefaultRoleProvider\" enabled=\"true\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultRoleProvider\" type=\"System.Web.Providers.DefaultRoleProvider, System.Web.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"DefaultConnection\" applicationName=\"" + _ParametroTO.NmProjeto + "\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</roleManager>");
            _SystemWebRoleElements = sb;**/
        }

        private string Get_SystemWebAutenticationElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<authentication mode=\"Forms\">");
            sb.AppendLine("<forms loginUrl=\"~/Account/Login\" name=\"OCCookie\" timeout=\"30\" slidingExpiration=\"true\" />");
            sb.AppendLine("</authentication>");
            return sb.ToString();
        }

        private string Get_SystemWebAuthorizationElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<authorization>");
            sb.AppendLine("<deny users=\"?\" />");
            sb.AppendLine("<!--<allow users=\"*\" />-->");
            sb.AppendLine("<!--<allow users=\"?\" />-->");
            sb.AppendLine("</authorization>");
            return sb.ToString();
        }

       /** Verificar o mode="InProc"
        * private void Set_SystemWebSessionStateElements(XElement sessionState)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<sessionState mode=\"InProc\" customProvider=\"DefaultSessionProvider\">");
            sb.AppendLine("<providers>");
            sb.AppendLine("<add name=\"DefaultSessionProvider\" type=\"System.Web.Providers.DefaultSessionStateProvider, System.Web.Providers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" connectionStringName=\"DefaultConnection\" />");
            sb.AppendLine("</providers>");
            sb.AppendLine("</sessionState>");
            _SystemWebSessionStateElements = sb;
        }**/

        private string Get_LocationsForgotElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<location path=\"Account/Forgot\" allowOverride=\"true\">");
            sb.AppendLine("<system.web>");
            sb.AppendLine("<authorization>");
            sb.AppendLine("<allow users=\"*\" />");
            sb.AppendLine("</authorization>");
            sb.AppendLine("</system.web>");
            sb.AppendLine("</location>");
            return sb.ToString();
        }

        private string Get_LocationInstalarSistemaElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<location path=\"Account/InstalarSistema\" allowOverride=\"true\">");
            sb.AppendLine("<system.web>");
            sb.AppendLine("<authorization>");
            sb.AppendLine("<allow users=\"*\" />");
            sb.AppendLine("</authorization>");
            sb.AppendLine("</system.web>");
            sb.AppendLine("</location>");
            return sb.ToString();
        }

        private void Set_ConnectionString(XElement connectionString)
        {
            XElement add = connectionString.Element(XName.Get("add"));
            
            StringBuilder sb = new StringBuilder("\"Data Source=");
            sb.Append(_ParametroTO.Conexao);
            sb.Append(";Initial Catalog=");
            sb.Append(_ParametroTO.Base);
            sb.Append(";Integrated Security=SSPI;user id=");
            sb.Append(_ParametroTO.Usuario);
            sb.Append(";password=");
            sb.Append(_ParametroTO.Senha);
            sb.Append(";MultipleActiveResultSets=true\"");

            add.SetAttributeValue(XName.Get("connectionString"), sb.ToString());
        }
    }
}
