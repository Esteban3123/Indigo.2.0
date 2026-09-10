'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Jorge Leonardo Vernaza
' Created          : 29-10-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Media.Imaging

#End Region
''' <summary>
''' Clase que contiene el diseño wpf del control para mostrar cuando se recibe un mensaje
''' </summary>
Public Class WPFMessageConversation
#Region "Variables Globales - Propiedades - Eventos"
    ''' <summary>
    ''' Evento que ocurre cuando se da clic sobre el boton responder.
    ''' </summary>
    Public Event Answer()
    ''' <summary>
    ''' Evento que ocurre cuando se da clic en el boton ignorar.
    ''' </summary>
    Public Event Declined()

    ''' <summary>
    ''' Propiedad que obtiene o establece el nombre del usuario.
    ''' </summary>
    Public Property UserName As String
        Get
            Return INDtxtUserName.Text
        End Get
        Set(value As String)
            INDtxtUserName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la photo del usuario.
    ''' </summary>
    Public WriteOnly Property UserPhoto As Bitmap
        Set(value As Bitmap)
            If value IsNot Nothing Then
                INDpeUserPhoto.Source = loadBitmap(value)
            Else
                INDpeUserPhoto.Source = loadBitmap(My.Resources.User175x200)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el mensaje enviado
    ''' </summary>
    Public Property UserMessage As String
#End Region

#Region "Metodos"
    <DllImport("gdi32")> _
    Private Shared Function DeleteObject(o As IntPtr) As Integer
    End Function

    Private Shared _loadBitmap As BitmapSource
    Public Shared Property loadBitmap(source As System.Drawing.Bitmap) As BitmapSource
        Get
            Dim ip As IntPtr = source.GetHbitmap()
            Try
                _loadBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions())
            Finally
                DeleteObject(ip)
            End Try
            Return _loadBitmap
        End Get
        Set(value As BitmapSource)
            _loadBitmap = value
        End Set
    End Property

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        RaiseEvent Answer()
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        RaiseEvent Declined()
    End Sub
#End Region


End Class
