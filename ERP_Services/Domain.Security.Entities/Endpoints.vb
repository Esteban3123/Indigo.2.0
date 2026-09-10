
Imports System.Runtime.Serialization

<DataContract(IsReference:=True)>
Public Class Endpoints

    <DataMember()>
    Public Property Id As Int32

    <DataMember()>
    Public Property Code As String

    <DataMember()>
    Public Property IdContainer As Int32

    <DataMember()>
    Public Property UrlBase As String

End Class
