'***********************************************************************'
' Assembly         : Presentation.Client                                '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-10-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'
''' <summary>
''' Clase con toda la funcioanlidad del control de usuario para mostrar las noticias de los rss
''' </summary>
Public Class CtrRssItem

#Region "Propiedades"
    ''' <summary>
    ''' Titulo de la noticia
    ''' </summary>
    Public Property INDTitle As String
        Get
            Return INDlblTitle.Text
        End Get
        Set(value As String)
            INDlblTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Resumen de la noticia
    ''' </summary>
    Public Property INDResume As String
        Get
            Return INDlblResume.Text
        End Get
        Set(value As String)
            INDlblResume.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Imagen de la noticia
    ''' </summary>
    Public Property INDImage As String
        Get
            Return INDpeImage.Tag
        End Get
        Set(value As String)
            INDpeImage.Tag = value
        End Set
    End Property

    ''' <summary>
    ''' Noticia completa
    ''' </summary>
    Public Property INDNews As String
        Get
            Return INDtxtNews.Text
        End Get
        Set(value As String)
            INDtxtNews.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Noticia completa
    ''' </summary>
    Private _INDLinkNews As String
    Public Property INDLinkNews As String
        Get
            Return _INDLinkNews
        End Get
        Set(value As String)
            _INDLinkNews = value
        End Set
    End Property
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Metodo load del control.
    ''' </summary>
    Private Sub CtrRssItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        If INDImage = String.Empty Then
            INDpeImage.Visible = True
            Me.Size = New Size(400, 200)
        Else
            INDpeImage.Visible = False
            Me.Size = New Size(400, 90)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para abrir el popup con la noticia completa cuando da clic en el resumen de la noticia
    ''' </summary>
    Private Sub INDlblResume_Click(sender As Object, e As EventArgs) Handles INDlblResume.Click
        INDtxtNews.ShowPopup()
    End Sub

    ''' <summary>
    ''' Click en el boton abrir link
    ''' </summary>
    Private Sub INDbtnLink_Click(sender As Object, e As EventArgs) Handles INDbtnLink.Click
        Using proceso As New System.Diagnostics.Process
            proceso.StartInfo.FileName = INDLinkNews
            proceso.Start()
        End Using
    End Sub
#End Region

End Class
