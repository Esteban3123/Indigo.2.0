Imports System.Runtime.Serialization

<DataContract(IsReference:=True), Serializable(), KnownType(GetType(ProductCita))> _
<KnownType(GetType(InventoryProduct))> _
<KnownType(GetType(ATC))> _
Public Class ProductCita

    <DataMember()> _
    Public Property Product As InventoryProduct

    <DataMember()> _
    Public Property CANTRECUR As Integer?

    <DataMember()> _
    Public Property REACALAUT As Boolean?

    <DataMember()> _
    Public Property MLPRODUCT As Decimal?

    <DataMember()> _
    Public Property KGPRODUCT As Decimal?

    <DataMember()> _
    Public Property EDADESDE As Integer?

    <DataMember()> _
    Public Property EDAHASTA As Integer?
End Class
