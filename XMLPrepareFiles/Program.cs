﻿// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using XMLPrepareFiles;
///Initializing the values ​​in the xml files
XmlTools.SaveListToXMLSerializer(DataSource.Products, "Products.xml");
XmlTools.SaveListToXMLSerializer(DataSource.Orders, "Orders.xml");
XmlTools.SaveListToXMLSerializer(DataSource.OrderItems, "OrderItems.xml");

///Initializing the config ​​in the xml files
XElement configRoot = new ("config");
configRoot.Add(new XElement("orderSequenceID"), DataSource.Config.GetOrderSequenceID());
configRoot.Add(new XElement("orderItemSequenceID"), DataSource.Config.GetOrderItemSequenceID());
configRoot.Save("config.xml");
