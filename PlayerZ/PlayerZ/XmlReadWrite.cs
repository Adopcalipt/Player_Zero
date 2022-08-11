using PlayerZero.Classes;
using System.IO;
using System.Xml.Serialization;

namespace PlayerZero
{
    public class XmlReadWrite
    {
        public static RandomPlusList LoadRando(string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.LoadRando == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(RandomPlusList));
                using (StreamReader sr = new StreamReader(fileName))
                {
                    return (RandomPlusList)xml.Deserialize(sr);
                }
            }
            catch
            {
                return new RandomPlusList();
            }

        }
        public static void SaveRando(RandomPlusList config, string fileName)
        {
            LoggerLight.GetLogging("XmlReadWrite.SaveRando == " + fileName);
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(RandomPlusList));
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    xml.Serialize(sw, config);
                }
            }
            catch
            {
                LoggerLight.GetLogging("XmlReadWrite.SaveRando failed");
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
