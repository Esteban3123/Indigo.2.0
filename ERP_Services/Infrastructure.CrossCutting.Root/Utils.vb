Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports System.ServiceModel
Imports System.Text
Imports System.Xml.Serialization

Public Class Utils

#Region "Methods"

    ''' <summary>
    ''' Valida si el error presentado corresponde a un código de los enviados
    ''' </summary>
    ''' <param name="ex">Excepcion a validar</param>
    ''' <param name="status">Codigo de los estados a validar</param>
    ''' <returns>Verdadero si encuentra alguno de los códigos relacionados</returns>
    Public Shared Function ValidateSqlCodeExceptions(ByVal ex As SqlException, ByVal status As Integer()) As Boolean
        Dim found = False

        If ex.Errors IsNot Nothing Then
            For Each Err As SqlError In ex.Errors
                If status.Contains(Err.Number) Then
                    found = True
                End If
            Next
        Else
            found = status.Contains(Err.Number)
        End If

        Return found
    End Function

    ''' <summary>
    ''' Obtiene la recursivamente la secuencia de excepciones heredadas
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <returns>Message Error</returns>
    Public Shared Function GetInnerExceptionMessageToString(ByVal ex As Exception) As String
        Dim message = ex.Message
        If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
            message = GetInnerExceptionMessageToString(ex.InnerException)
        ElseIf ex.GetType().Name = GetType(FaultException(Of ExceptionDetail)).Name Then
            Dim exFaultException = DirectCast(ex, FaultException(Of ExceptionDetail))
            If exFaultException.Detail IsNot Nothing AndAlso exFaultException.Detail.InnerException IsNot Nothing Then
                message = GetInnerExceptionMessageToString(exFaultException.Detail.InnerException)
            End If
        ElseIf ex.GetType().Name = GetType(DbEntityValidationException).Name Then
            Dim exDbEntity = CType(ex, DbEntityValidationException)
            If exDbEntity.EntityValidationErrors IsNot Nothing AndAlso exDbEntity.EntityValidationErrors.Count() > 0 Then
                message = String.Empty
                For Each eve In exDbEntity.EntityValidationErrors
                    message = message + String.Format("La entidad de tipo {0} tiene los siguientes errores: ", eve.Entry.Entity.GetType().Name) + Environment.NewLine
                    For Each ve In eve.ValidationErrors
                        message = message + String.Format("- Propiedad: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage) + Environment.NewLine
                    Next
                Next
            End If
        End If
        Return message
    End Function

    ''' <summary>
    ''' Obtiene la recursivamente la secuencia de excepciones heredadas si son de tipo exceptionDetail
    ''' </summary>
    ''' <param name="ex">Excepcion a obtener</param>
    ''' <returns>Message Error</returns>
    Public Shared Function GetInnerExceptionMessageToString(ByVal ex As ExceptionDetail) As String
        Dim message = ex.Message
        If ex.InnerException IsNot Nothing AndAlso Not String.IsNullOrEmpty(ex.InnerException.Message) Then
            message = GetInnerExceptionMessageToString(ex.InnerException)
        End If
        Return message
    End Function

    Public Shared Function GetFormatNumberWithTwoDecimals(value As Decimal?) As String
        If value Is Nothing Then
            Return Nothing
        End If
        Dim str1 = value.Value.ToString("n2")
        Dim numberFormat As NumberFormatInfo = CultureInfo.CurrentCulture.NumberFormat
        Dim decimalSeparator = numberFormat.NumberDecimalSeparator
        Dim numberGroupSeparator = numberFormat.NumberGroupSeparator
        Dim strResult = str1.Replace(numberGroupSeparator, "").
                Replace(decimalSeparator, ".")
        Return strResult
    End Function

    ''' <summary>
    ''' Asegura que la ruta a directorio exista. Si no existe la crea
    ''' </summary>
    ''' <param name="filePath">Ruta a directorio</param>
    ''' <param name="fileName">Nombre del Archivo</param>
    Public Shared Function ValidateFileExists(ByVal filePath As String, ByVal fileName As String) As Boolean
        Try
            Dim path = System.IO.Path.Combine(filePath, fileName)
            If File.Exists(path) Then
                Return False
            Else
                If Not Directory.Exists(filePath) Then
                    Directory.CreateDirectory(filePath)
                End If
                Return True
            End If
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Asegura que la ruta a directorio exista. Si no existe la crea
    ''' </summary>
    ''' <param name="path">Ruta a directorio</param>
    Public Shared Sub EnsurePathExists(ByVal path As String)
        Try
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
        Catch
        End Try
    End Sub

    Public Shared Function Sha1Encode(strToHash As String) As String
        Dim sha1Obj As New SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte = sha1Obj.ComputeHash(Encoding.UTF8.GetBytes(strToHash))
        Return GetStringFromHash(bytesToHash)
    End Function

    Public Shared Function Sha1Base64Encode(nb As Byte(), DateSend As String, Key As String) As String
        Dim sha1Obj As New SHA1CryptoServiceProvider
        Dim bytes1 As Byte() = Encoding.UTF8.GetBytes(DateSend)
        Dim bytes2 As Byte() = Encoding.UTF8.GetBytes(Key)
        Dim length1 As Integer = nb.Length
        Dim length2 As Integer = bytes1.Length
        Dim length3 As Integer = bytes2.Length
        Array.Resize(nb, length1 + length2 + length3)
        Array.Copy(bytes1, 0, nb, length1, length2)
        Array.Copy(bytes2, 0, nb, length1 + length2, length3)
        Return Convert.ToBase64String(sha1Obj.ComputeHash(nb))
    End Function

    Public Shared Function Sha256Encode(strToHash As String) As String
        Dim sha256Obj As New SHA256CryptoServiceProvider
        Dim bytesToHash() As Byte = sha256Obj.ComputeHash(Encoding.UTF8.GetBytes(strToHash))
        Return GetStringFromHash(bytesToHash)
    End Function

    Public Shared Function Sha384Encode(strToHash As String) As String
        Dim sha384Obj As New SHA384CryptoServiceProvider
        Dim bytesToHash() As Byte = sha384Obj.ComputeHash(Encoding.UTF8.GetBytes(strToHash))
        Return GetStringFromHash(bytesToHash)
    End Function

    Private Shared Function GetStringFromHash(bytesToHash As Byte()) As String
        Dim strResult As String = ""
        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next
        Return strResult
    End Function

    Public Shared Function SerializeToXmlElement(o As Object) As Xml.XmlElement
        Dim doc As New Xml.XmlDocument
        Using writer As Xml.XmlWriter = doc.CreateNavigator().AppendChild()
            Dim xs As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(o.GetType())
            xs.Serialize(writer, o)
        End Using
        Return doc.DocumentElement
    End Function

    Public Shared Function SerializeToXmlString(o As Object) As String
        Dim xmlString As String
        Using writer As New StringWriter()
            Dim xs As Xml.Serialization.XmlSerializer = New Xml.Serialization.XmlSerializer(o.GetType())
            xs.Serialize(writer, o)
            xmlString = writer.ToString()
        End Using
        Return xmlString
    End Function

    Public Shared Function Deserialize(Of T)(xmlStr As String) As T
        Dim serializer As New XmlSerializer(GetType(T))
        Dim result As T
        Using reader As TextReader = New StringReader(xmlStr)
            result = CType(serializer.Deserialize(reader), T)
        End Using
        Return result
    End Function

    Public Shared Function Deserialize(Of T)(doc As XDocument) As T
        Dim serializer As New XmlSerializer(GetType(T))
        Dim result As T
        Using reader = doc.Root.CreateReader()
            result = CType(serializer.Deserialize(reader), T)
        End Using
        Return result
    End Function

    Public Shared Function GetXmlEnumToString(Of TEnum)(ByVal value As TEnum)
        Dim enumType = GetType(TEnum)
        If Not enumType.IsEnum Then
            Return Nothing
        End If

        Dim member = enumType.GetMember(value.ToString).FirstOrDefault()
        If member Is Nothing Then
            Return Nothing
        End If

        Dim attribute = member.GetCustomAttributes(False).OfType(Of XmlEnumAttribute)().FirstOrDefault()
        If attribute Is Nothing Then
            Return Nothing
        End If

        Return attribute.Name
    End Function

    Public Shared Function CalculateAdjustedValue(Difference As Decimal, ValueIfDifferenceNegative As Decimal, ValueIfDifferencePositive As Decimal) As Decimal
        Dim AdjustedValue As Decimal = 0

        If Difference < 0 Then
            AdjustedValue = IIf(Math.Abs(Difference) > ValueIfDifferenceNegative, ValueIfDifferenceNegative * -1, Difference)
        ElseIf Difference > 0 Then
            AdjustedValue = IIf(Difference > ValueIfDifferencePositive, ValueIfDifferencePositive, Difference)
        End If

        Return AdjustedValue
    End Function

    Public Shared Function TryParseXml(value As String) As Boolean
        Try
            XElement.Parse(value)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

End Class
