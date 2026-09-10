Imports System.Runtime.Serialization
Imports System.Xml.Serialization

<DataContract()>
<XmlRoot(ElementName:="AccountPayable")>
Public Class CxPDifferentialAdjustment

    <DataMember()>
    Property Id As Integer
    <DataMember()>
    Property ValuePaid As Decimal
    <DataMember()>
    Property EntityName As String
    <DataMember()>
    Property EntityId As String
End Class

