﻿using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace XMLPrepareFiles
{
    /// <summary>
    /// A helper class with useful functions for xml files
    /// </summary>
    internal class XmlTools
    {
        static readonly string dir = @"xml\";
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SaveListToXMLSerializer<T>(List<T?> list, string filePath)
        {
            try
            {
                FileStream file = new(filePath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        /// <summary>
        /// function used to load data from xml file to a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<T?> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    List<T> list;
                    XmlSerializer x = new(typeof(List<T>));
                    FileStream file = new(filePath, FileMode.Open);
                    list = (List<T?>)x.Deserialize(file);
                    file.Close();
                    return list!;
                }
                else
                    return new List<T?>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); // DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
    }
}

