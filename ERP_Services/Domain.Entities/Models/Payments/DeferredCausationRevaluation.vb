Imports System.Runtime.Serialization
Imports System.Xml.Serialization

<DataContract()>
<XmlRoot(ElementName:="DeferredCausationRevaluation")>
Public Class DeferredCausationRevaluation

    <DataMember()>
    Property Id As Integer
    <DataMember()>
    Property ValueAdjustment As Decimal
    <DataMember()>
    Property EntityName As String
    <DataMember()>
    Property EntityId As String
    <DataMember()>
    Property DocumentDate As Date
End Class

<XmlRoot(ElementName:="Data")>
Public Class DataRevaluation
    <XmlElement(ElementName:="DeferredCausationRevaluation")>
    Property ListDeferredCausationRevaluation As List(Of DeferredCausationRevaluation)
End Class

