'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 10-Enero-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports System.Runtime.InteropServices
Imports System.Windows.Media.Imaging
Imports System.Windows
#End Region

''' <summary>
''' Clase con la funcionalida del control de notificaciones
''' </summary>
''' <remarks></remarks>
Public Class WpfSegmentControl

#Region "Eventos"
    ''' <summary>
    ''' Evento que indica que se cambio de seleccion
    ''' </summary>
    ''' <param name="Selection"></param>
    ''' <remarks></remarks>
    Public Event SelectionChange(ByVal Selection As eNotifications)
#End Region

#Region "Metodos"
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub WpfSegmentControl_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        INDicFirst.Source = loadBitmap(My.Resources.TaskIcon)
        INDicSecond.Source = loadBitmap(My.Resources.ProcessIcon)
        INDicThird.Source = loadBitmap(My.Resources.MessageIcon)
        INDtgbThird.IsChecked = True
    End Sub

    Private Sub INDtgbFirst_UnChecked(sender As Object, e As System.Windows.RoutedEventArgs) Handles INDtgbFirst.Unchecked
        If INDtgbSecond.IsChecked = False AndAlso INDtgbFirst.IsChecked = False AndAlso INDtgbThird.IsChecked = False Then
            INDtgbFirst.IsChecked = True
        End If
    End Sub

    Private Sub INDtgbSecond_UnChecked(sender As Object, e As System.Windows.RoutedEventArgs) Handles INDtgbSecond.Unchecked
        If INDtgbSecond.IsChecked = False AndAlso INDtgbFirst.IsChecked = False AndAlso INDtgbThird.IsChecked = False Then
            INDtgbSecond.IsChecked = True
        End If
    End Sub

    Private Sub INDtgbThird_UnChecked(sender As Object, e As System.Windows.RoutedEventArgs) Handles INDtgbThird.Unchecked
        If INDtgbSecond.IsChecked = False AndAlso INDtgbFirst.IsChecked = False AndAlso INDtgbThird.IsChecked = False Then
            INDtgbThird.IsChecked = True
        End If
    End Sub

    Private Sub INDtgbFirst_Checked(sender As Object, e As System.Windows.RoutedEventArgs) Handles INDtgbFirst.Checked
        INDtgbFirst.IsChecked = True
        INDtgbSecond.IsChecked = False
        INDtgbThird.IsChecked = False
        RaiseEvent SelectionChange(eNotifications.Task)
    End Sub

    Private Sub INDtgbSecond_Checked(sender As Object, e As System.Windows.RoutedEventArgs) Handles INDtgbSecond.Checked
        INDtgbFirst.IsChecked = False
        INDtgbSecond.IsChecked = True
        INDtgbThird.IsChecked = False
        RaiseEvent SelectionChange(eNotifications.Process)
    End Sub

    Private Sub INDtgbThird_Checked(sender As Object, e As System.Windows.RoutedEventArgs) Handles INDtgbThird.Checked
        INDtgbFirst.IsChecked = False
        INDtgbSecond.IsChecked = False
        INDtgbThird.IsChecked = True
        RaiseEvent SelectionChange(eNotifications.Messages)
    End Sub
#End Region

#Region "Metodos - Funciones para mostrar los iconos"
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
#End Region

End Class

''' <summary>
''' Enumeracion con el tipo de Notificaciones
''' </summary>
Public Enum eNotifications
    ''' <summary>
    ''' Procesos que se ejecutan en la aplicacion
    ''' </summary>
    ''' <remarks></remarks>
    Process
    ''' <summary>
    ''' Tareas que se tienen programadas en la aplicacion
    ''' </summary>
    ''' <remarks></remarks>
    Task
    ''' <summary>
    ''' Mensajes de la aplicacion
    ''' </summary>
    ''' <remarks></remarks>
    Messages
End Enum