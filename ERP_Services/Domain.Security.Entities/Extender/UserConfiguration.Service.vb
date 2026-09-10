Imports System.Runtime.Serialization

Partial Public Class UserConfiguration
    <DataMember()>
    Property BorrarConfig As Boolean

    <DataMember()>
    Property TimezoneName As String

    <DataMember()>
    Property AccessToken As String

    <DataMember()>
    Property EncryptedKey As String
End Class
