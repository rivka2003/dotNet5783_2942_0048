// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using XMLPrapareFiles;
using XMLPrepareFiles;

XmlTools.SaveListToXMLSerializer(DataSource.Products, "product.xml");
XmlTools.SaveListToXMLSerializer(DataSource.Orders, "order.xml");
XmlTools.SaveListToXMLSerializer(DataSource.OrderItems, "orderItem.xml");

XElement configRoot = new XElement("config");
configRoot.Add(new XElement("orderSequenceID"), DataSource.getOrderSequenceID());
configRoot.Add(new XElement("orderItemSequenceID"), DataSource.getOrderItemSequenceID());
configRoot.Save("config.xml");
