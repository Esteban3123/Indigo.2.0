Imports System.Runtime.Serialization

Public Class ConsignmentCostListDetail
    <DataMember>
    Property ProductCodeName As String

    <DataMember()>
    Public Property SelectOption As Boolean

    <DataMember()>
    Public Property ProductType As String

End Class
