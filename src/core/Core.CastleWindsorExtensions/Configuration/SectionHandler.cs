using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;

namespace Core.CastleWindsorExtensions.Configuration
{
    public class SectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("modules", IsRequired = false)]
        public ModulesElementCollection Modules
        {
            get
            {
                return (ModulesElementCollection)this["modules"];
            }
        }

        [ConfigurationProperty("components", IsRequired = false)]
        public ComponentElementCollection Components
        {
            get
            {
                return (ComponentElementCollection)this["components"];
            }
        }

        public static SectionHandler Deserialize(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            int content = (int)reader.MoveToContent();

            if (reader.EOF)
                throw new ConfigurationErrorsException("No XML in configuration.");

            SectionHandler sectionHandler = new SectionHandler();

            sectionHandler.DeserializeElement(reader, false);

            return sectionHandler;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The XmlTextReader disposal will automatically dispose the stream.")]
        public static SectionHandler Deserialize(string configurationFile, string configurationSection)
        {
            if (string.IsNullOrWhiteSpace(configurationSection))
                configurationSection = "autofac";

            configurationFile = SectionHandler.NormalizeConfigurationFilePath(configurationFile);

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configurationFile;
            System.Configuration.Configuration configuration;
            try
            {
                configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            }
            catch (ConfigurationErrorsException)
            {
                using (XmlTextReader xmlTextReader = new XmlTextReader((Stream)File.OpenRead(configurationFile)))
                    return SectionHandler.Deserialize((XmlReader)xmlTextReader);
            }
            SectionHandler section = (SectionHandler)configuration.GetSection(configurationSection);
            if (section == null)
                throw new ConfigurationErrorsException("Section '" + configurationSection + "' not found.");
            return section;
        }

        private static string NormalizeConfigurationFilePath(string configurationFile)
        {
            if (configurationFile == null)
                throw new ArgumentNullException("configurationFile");

            if (configurationFile.Length == 0)
                throw new ArgumentException("Argument 'configurationFile' may not be empty.");

            return configurationFile;
        }
    }
}