Imports System.Runtime.Serialization

<DataContract(IsReference:=True), Serializable(), KnownType(GetType(FolioDataDetail))>
Partial Public Class FolioDataHeader

    Public Sub New()
        ListRevenueControlDetails = New List(Of FolioDataDetail)()
    End Sub

    <DataMember()>
    Public Property Id As Integer
    <DataMember()>
    Public Property FolioQuantity As Byte
    <DataMember()>
    Public Property LiquidationType As Byte
    <DataMember()>
    Public Property LiquidationTypeName As String
    <DataMember()>
    Public Property ContractCodeName As String
    <DataMember()>
    Public Property ListRevenueControlDetails As List(Of FolioDataDetail)

End Class