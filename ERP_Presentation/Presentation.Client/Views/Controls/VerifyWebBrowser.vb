Imports System.Runtime.InteropServices
Imports System.Threading
Imports Microsoft.Win32

Public Class VerifyWebBrowser
    <DllImport("advapi32.dll", EntryPoint:="RegOpenKeyEx")>
    Public Shared Function RegOpenKeyEx(ByVal hKey As Integer, ByVal lpSubKey As String, ByVal ulOptions As Integer, ByVal samDesired As Integer, ByRef phkResult As Integer) As Integer
    End Function
    Private rk As RegistryKey = Registry.LocalMachine

    Public Function GetInstallPath() As String
        Dim installPath = GetInstallPathFromRegistry()
        Return installPath
    End Function

    Public Function GetInstallPathFromRegistry() As String
        Dim version As Version = Nothing

        Try

            If Environment.Is64BitProcess Then

                Using key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}")

                    If key IsNot Nothing Then
                        Dim o As Object = key.GetValue("pv")

                        If o IsNot Nothing Then
                            version = New Version(TryCast(o, String))
                        End If
                    End If
                End Using
            Else

                Using key As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}")

                    If key IsNot Nothing Then
                        Dim o As Object = key.GetValue("pv")

                        If o IsNot Nothing Then
                            version = New Version(TryCast(o, String))
                        End If
                    End If
                End Using
            End If

            If version IsNot Nothing Then
                Return version.ToString()
            Else
                instalarRuntime()
                Return String.Empty
            End If

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    Private Sub instalarRuntime()
        Try
            Dim nombreArchivo As String = String.Format("MicrosoftEdgeWebView2RuntimeInstaller{0}.exe", If(Environment.Is64BitProcess, "X64", "X86"))
            Dim p1 As Process
            Dim ps1 As ProcessStartInfo = New ProcessStartInfo(String.Format("{0}\{1}\{2}", Environment.CurrentDirectory, If(Environment.Is64BitProcess, "x64", "x86"), nombreArchivo), " /install")
            ps1.WindowStyle = ProcessWindowStyle.Normal
            ps1.UseShellExecute = True
            ps1.Verb = "runas"
            p1 = Process.Start(ps1)
            p1.WaitForInputIdle()

            While (p1.MainWindowHandle = IntPtr.Zero)
                p1.Refresh()
                Thread.Sleep(3)
            End While

            'Application.[Exit]()
        Catch ex As Exception
        End Try
    End Sub
End Class
