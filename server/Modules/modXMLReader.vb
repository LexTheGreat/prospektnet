Imports System.Xml
Imports System.IO
Module modXMLReader
    Sub loadoptions()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        m_xmld = New XmlDocument
        m_xmld.Load(pathContent & "config.xml")

        m_nodelist = m_xmld.SelectNodes("configuration/network")
        For Each m_node In m_nodelist
            ServerConfig.Port = m_node.Item("port").InnerText
        Next
    End Sub
End Module

