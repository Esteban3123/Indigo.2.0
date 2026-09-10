Imports System.Runtime.Serialization

<DataContract(IsReference:=True)>
Public Class CompanyPermission

    <DataMember()> _
    Public Property CompanyCode As String
    <DataMember()>
    Public Property IdOperatingUnitDefault As Integer
    <DataMember()>
    Public Property Administrator As Boolean

End Class
