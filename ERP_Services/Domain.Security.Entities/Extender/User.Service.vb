Imports System.Linq
Imports System.Runtime.Serialization
Imports Domain.Base.Entities

Partial Public Class User
    Implements ICloneable

    <DataMember()>
    Public Property PersonFullName As String

    ''' <summary>
    ''' Clona el objeto para que obtenga una instancia distinta al actual
    ''' </summary>
    ''' <returns></returns>
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim _Clone As User = Nothing
        Dim obj = New System.Runtime.Serialization.DataContractSerializer(GetType(User))

        Using stream = New System.IO.MemoryStream()
            obj.WriteObject(stream, Me)
            stream.Seek(0, System.IO.SeekOrigin.Begin)
            _Clone = CType(obj.ReadObject(stream), User)
        End Using

        Return _Clone
    End Function

    ''' <summary>
    ''' Parsea el token a tipo de dato GUID.
    ''' </summary>
    ''' <returns>Token como GUID</returns>
    Public Function ParseToken() As Guid
        Dim _token As Guid = Guid.Parse(Me.ElectronicSignatureToken)
        Return _token
    End Function

    ''' <summary>
    ''' Valida si el token es un GUID válido
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateTokenFormat() As Boolean
        Dim guid As New Guid
        Dim res = Guid.TryParse(Me.ElectronicSignatureToken, guid)
        If res Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
