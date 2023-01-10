using System.Xml.Serialization;

namespace Dal;


/// <summary>
/// A helper class with useful functions for xml files
/// </summary>
public class XmlTools
{

    public static string dir = @"xml\";
    static XmlTools()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    /// <summary>
    /// function used to save data from list to an xml file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="filePath"></param>
    /// <exception cref="Exception"></exception>
    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception(ex.Message);
            // DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
        }
    }

    /// <summary>
    /// function used to load data from xml file to a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>));
                FileStream file = new FileStream(filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file)!;
                file.Close();
                return list;
            }
            else
                return new List<T>();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
        }
    }
    #endregion
}
