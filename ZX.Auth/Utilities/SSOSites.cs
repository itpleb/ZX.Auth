using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using ZX.Auth.SSO.Interfaces;

namespace ZX.Auth.Utilities
{
    [Serializable]
    internal class SSOSites
    {
        string cfgPath = "";
        XmlDocument doc = new XmlDocument();
        public SSOSites()
        {
            cfgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sites.config");
            doc.Load(cfgPath);
        }
        public static SSOSites Instance { get; private set; } = new SSOSites();

        public ISSOSite GetSite(string siteId)
        {

            XmlElement documentElement = this.doc.DocumentElement;
            string xpath = string.Format("site[@siteid='{0}']", siteId);
            XmlNode node = documentElement.SelectSingleNode(xpath);
            if (node == null)
            {
                return null;
            }
            XmlAttribute attribute = node.Attributes["homepage"];
            var homepage = attribute.Value;
            var site = new SSO.Server.SSOSite()
            {
                SiteId = siteId,
                HomePage = homepage,
            };
            return site;
        }
    }
}
