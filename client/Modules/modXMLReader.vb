Imports System.Xml
Imports System.IO
Module modXMLReader
    Sub loadoptions()
        Dim m_xmld As XmlDocument
        Dim m_nodelist As XmlNodeList
        Dim m_node As XmlNode
        m_xmld = New XmlDocument
        m_xmld.Load(pathContent & "config.xml")

        m_nodelist = m_xmld.SelectNodes("configuration/resolution")
        For Each m_node In m_nodelist
            ClientConfig.ScreenWidth = m_node.Item("width").InnerText
            ClientConfig.ScreenHeight = m_node.Item("height").InnerText
        Next

        m_nodelist = m_xmld.SelectNodes("configuration/music")
        For Each m_node In m_nodelist
            ClientConfig.MenuMusic = m_node.Item("menu").InnerText
            ClientConfig.GameMusic = m_node.Item("game").InnerText
        Next

        m_nodelist = m_xmld.SelectNodes("configuration/network")
        For Each m_node In m_nodelist
            ClientConfig.IP = m_node.Item("ip").InnerText
            ClientConfig.Port = m_node.Item("port").InnerText
        Next
    End Sub
End Module
