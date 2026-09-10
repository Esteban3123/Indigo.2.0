'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 10-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias importadas"
Imports System.Runtime.InteropServices
Imports Presentation.Base.BaseClass
Imports Presentation.Base

#End Region

''' <summary>
''' Clase con toda la funcionalidad del control para cabiar de aplicacion abierta
''' </summary>
Public Class CtrAplicationOpen
    Public Event ClicAplication()

    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function SetForegroundWindow(handle As IntPtr) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function ShowWindow(handle As IntPtr, nCmdShow As Integer) As Boolean
    End Function
    <System.Runtime.InteropServices.DllImport("User32.dll")> _
    Private Shared Function IsIconic(handle As IntPtr) As Boolean
    End Function

    Const SW_RESTORE As Integer = 9





    Private _sysImages As SystemImageList = New SystemImageList()
    Dim ListadoProcesos As New List(Of Procesos)
    Public Function UpdateImageList() As Boolean
        Try
            ListadoProcesos = New List(Of Procesos)
            Dim fileName As String
            INDlvwAplications.Items.Clear()
            INDlvwAplications.View = View.LargeIcon
            INDlvwAplications.LargeImageList = _sysImages.LargeImages
            For Each p As Process In Process.GetProcesses()
                If p.MainWindowHandle.ToInt32 <> 0 AndAlso Not String.IsNullOrEmpty(p.MainWindowTitle) Then
                    If p.MainModule.FileName <> "D:\Vie HealtTech\Genesis ERP\Presentacion.Cliente\bin\Debug\Indigo Crystal.vshost.exe" Then
                        fileName = p.MainModule.FileName
                        ListadoProcesos.Add(New Procesos With {.WindowHandle = p.MainWindowHandle, .NombreProceso = p.MainModule.FileName, .ImagenProceso = _sysImages.LargeImages.Images(_sysImages.GetImageIndex(fileName)), .TextoProceso = p.MainWindowTitle})
                        INDlvwAplications.Items.Add(p.MainWindowHandle.ToString, p.MainWindowTitle, _sysImages.GetImageIndex(fileName))
                    End If
                End If
            Next
            INDlvwAplications.Focus()
            Return True
        Catch ex As Exception
            MessageIndigo.Show(obtenerRecurso(Eresources.ComunesError, Eform.Comunes), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
            Return False
        End Try
    End Function


    Private Sub ListView1_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles INDlvwAplications.ItemSelectionChanged
        Try
            Dim handle As IntPtr = ListadoProcesos.Where(Function(x) x.WindowHandle.ToString = e.Item.Name).SingleOrDefault.WindowHandle
            If IsIconic(handle) Then
                ShowWindow(handle, SW_RESTORE)
            End If
            SetForegroundWindow(handle)
            RaiseEvent ClicAplication()
        Catch ex As Exception
            MessageIndigo.Show(obtenerRecurso(Eresources.ComunesError, Eform.Comunes), Infrastructure.CrossCutting.Base.MessageType.Warning, Me.Text)
        End Try
    End Sub

    Private Sub INDlvwAplications_LostFocus(sender As Object, e As EventArgs) Handles INDlvwAplications.LostFocus
        RaiseEvent ClicAplication()
    End Sub

    Public Sub SetFocus()
        INDlvwAplications.Focus()
    End Sub
End Class

Class Procesos

    Property NombreProceso As String
    Property ImagenProceso As Image
    Property TextoProceso As String
    Property WindowHandle As IntPtr
End Class