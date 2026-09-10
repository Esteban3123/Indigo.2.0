Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public NotInheritable Class IndigoEncriptar

    Private Shared _key As Byte()
    Private Shared _iv As Byte()

    Shared Sub New()
        _key = Encoding.ASCII.GetBytes("Sist3masAs0ciads")
        _iv = Encoding.ASCII.GetBytes("9Ind1g0(rystaL-2")
    End Sub

    Public Shared Function EncriptaCadena(ByVal inputText As String) As String
        Dim inputBytes As Byte() = Encoding.ASCII.GetBytes(inputText)
        Dim encripted As Byte()
        Dim cripto As New RijndaelManaged()
        Using ms As New MemoryStream(inputBytes.Length)
            Using objCryptoStream As New CryptoStream(ms, cripto.CreateEncryptor(_key, _iv), CryptoStreamMode.Write)
                objCryptoStream.Write(inputBytes, 0, inputBytes.Length)
                objCryptoStream.FlushFinalBlock()
                objCryptoStream.Close()
            End Using
            encripted = ms.ToArray()
        End Using
        Return Convert.ToBase64String(encripted)
    End Function

    Public Shared Function DesencritaCadena(ByVal inputText As String) As String
        Dim inputBytes As Byte() = Convert.FromBase64String(inputText)
        Dim resultBytes As Byte() = New Byte(inputBytes.Length - 1) {}
        Dim textoLimpio As String = [String].Empty
        Dim cripto As New RijndaelManaged()
        Using ms As New MemoryStream(inputBytes)
            Using objCryptoStream As New CryptoStream(ms, cripto.CreateDecryptor(_key, _iv), CryptoStreamMode.Read)
                Using sr As New StreamReader(objCryptoStream, True)
                    textoLimpio = sr.ReadToEnd()
                End Using
            End Using
        End Using
        Return textoLimpio
    End Function

    Public Shared Function EncriptarPassword(ByVal strTexto As String) As String
        Dim _objHMACSHA As New HMACSHA1(Encoding.UTF8.GetBytes("ASE093431-+ADFERDKZ.DFE"))
        Dim _objTemporal As Byte() = Nothing
        Try

            For I As Integer = 0 To 1000
                _objTemporal = _objHMACSHA.ComputeHash(Encoding.UTF8.GetBytes(strTexto))
            Next
            Return Convert.ToBase64String(_objTemporal)
        Finally
            _objHMACSHA.Clear()
        End Try
    End Function

#Region "Funciones encriptar para integración con healtvault"
    Public Shared Function EncriptarString_MD5(texto As String) As String
        Using md5Hash As MD5 = MD5.Create()
            EncriptarString_MD5 = GetMd5Hash(md5Hash, texto)
        End Using
    End Function


    Public Shared Function GetMd5Hash(ByVal md5Hash As MD5, ByVal input As String) As String

        ' Convert the input string to a byte array and compute the hash. 
        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes 
        ' and create a string. 
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data  
        ' and format each one as a hexadecimal string. 
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string. 
        Return sBuilder.ToString()

    End Function


#End Region

End Class