Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography.X509Certificates
Imports Org.BouncyCastle.Security

Namespace Utils

    Friend Module X509Certificate2Extensions

#Region "Methods"

        <Extension()>
        Public Function GetSerialNumberAsDecimalString(certificate As X509Certificate2) As String
            Dim list As List(Of Integer) = New List(Of Integer)()
            list.Add(0)
            Dim serialNumber As String = certificate.SerialNumber
            For i As Integer = 0 To serialNumber.Length - 1
                Dim num As Integer = Convert.ToInt32(serialNumber(i).ToString(), 16)
                For j As Integer = 0 To list.Count - 1
                    Dim num2 As Integer = list(j) * 16 + num
                    list(j) = num2 Mod 10
                    num = Math.Floor(num2 / 10)
                Next
                While num > 0
                    list.Add(num Mod 10)
                    num = Math.Floor(num / 10)
                End While
            Next
            Dim source As IEnumerable(Of Char) = From d In list Select Convert.ToChar(48 + d)
            Dim value As Char() = source.Reverse().ToArray()
            Return New String(value)
        End Function

        Public Function ToBouncyX509Certificate(certificate As X509Certificate2) As Org.BouncyCastle.X509.X509Certificate
            Return DotNetUtilities.FromX509Certificate(certificate)
        End Function

#End Region

    End Module

End Namespace