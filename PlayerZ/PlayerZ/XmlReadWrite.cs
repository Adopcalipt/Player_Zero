using PlayerZero.Classes;
using System.IO;
using System.Xml.Serialization;

namespace PlayerZero
{
    public class XmlReadWrite : DataStore
    {
        public static ContactList LoadContact(string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.LoadContact == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(ContactList));
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return (ContactList)xml.Deserialize(sr);
                }
            }
            catch
            {
                return new ContactList();
            }

        }
        public static void SaveMyContacts(ContactList config, string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.SaveThisContact == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(ContactList));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    xml.Serialize(sw, config);
                }
            }
            catch
            {
                LoggerLight.GetLogging("XmlReadWrite.SaveThisContact failed");
            }
        }
        public static ClothBankXML LoadOutfitXML(string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.LoadOutfitXML == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(ClothBankXML));
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return (ClothBankXML)xml.Deserialize(sr);
                }
            }
            catch
            {
                return new ClothBankXML();
            }

        }
        public static void SavePlayerBrain(BackUpBrain config, string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.SavePlayerBrain == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(BackUpBrain));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    xml.Serialize(sw, config);
                }
            }
            catch
            {
                LoggerLight.GetLogging("XmlReadWrite.SavePlayerBrain failed");
            }

        }
        public static BackUpBrain LoadPlayerBrain(string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.LoadPlayerBrain == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(BackUpBrain));
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return (BackUpBrain)xml.Deserialize(sr);
                }
            }
            catch
            {
                return new BackUpBrain();
            }
        }
    }
}
