'***********************************************************************'
' Assembly         : Presentation.Client                                '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-11-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'
#Region "Librerias Importadas"
Imports System.Threading.Tasks
Imports System.Data
#End Region
''' <summary>
''' Clase con toda la funcioanlidad del control de usuario para mostrar las noticias de los rss
''' </summary>
Public Class CtrRss
#Region "Variable Globales"
    ''' <summary>
    ''' Objeto que se utliza para obtener el dataset de los rss solicitados
    ''' </summary>
    Public objDataset As DataSet
    ''' <summary>
    ''' Variable que se utiliza para almacenar el listado de controles que contienen los twitters
    ''' </summary>
    Dim ControlsRss As List(Of CtrRssItem)
#End Region

#Region "Propiedades"

#End Region

#Region "Metodos - Funciones"
    Public Async Sub LoadRsssAsync()
        Await Task.Run(AddressOf LoadRsss)
        INDxscConainerRssItems.Controls.Clear()
        If ControlsRss IsNot Nothing Then
            If ControlsRss.Count > 0 Then
                For Each RssItem As CtrRssItem In ControlsRss
                    INDxscConainerRssItems.Controls.Add(RssItem)
                Next
            End If
        End If
    End Sub

    Private Sub LoadRsss()
        Try
            objDataset = New DataSet
            ControlsRss = New List(Of CtrRssItem)
            objDataset.ReadXml("http://www.nlm.nih.gov/medlineplus/feeds/news_en.xml", XmlReadMode.Auto)
            For i = 0 To objDataset.Tables("item").Rows.Count - 1
                ControlsRss.Add(New CtrRssItem With {.INDTitle = objDataset.Tables("item")(i)("title"), .INDResume = objDataset.Tables("item")(i)("description"), .INDNews = objDataset.Tables("item")(i)("description"), .INDLinkNews = objDataset.Tables("item")(i)("link"), .Dock = DockStyle.Top})
            Next
        Catch

        End Try
    End Sub

#End Region
End Class
