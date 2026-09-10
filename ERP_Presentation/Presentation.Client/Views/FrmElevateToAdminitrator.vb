Imports System.Runtime.InteropServices

Public Class FrmElevateToAdminitrator

    <DllImport("user32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Shared Function SendMessage( _
        ByVal hWnd As IntPtr, _
        ByVal Msg As UInt32, _
        ByVal wParam As Integer, _
        ByVal lParam As IntPtr) _
        As Integer
    End Function

    Const BCM_SETSHIELD As UInt32 = &H160C

    Private Sub FrmElevateToAdminitrator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SendMessage(Me.BtnElevateTo.Handle, BCM_SETSHIELD, 0, New IntPtr(1))
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub BtnElevateTo_Click(sender As Object, e As EventArgs) Handles BtnElevateTo.Click
        Dim proc As New ProcessStartInfo
        proc.UseShellExecute = True
        proc.WorkingDirectory = Environment.CurrentDirectory
        proc.FileName = Application.ExecutablePath
        proc.Verb = "runas"
        Try
            Process.Start(proc)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        End Try
    End Sub
End Class