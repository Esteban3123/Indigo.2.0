Imports System.Runtime.Serialization

<DataContract(IsReference:=True), Serializable(), KnownType(GetType(ControlOutPatientServices))> _
<KnownType(GetType(ServiceOrder))> _
<KnownType(GetType(IngresoOrdenesServicio))> _
Public Class ControlOutPatientServices

    '<DataMember()> _
    'Property ListCitaFactura As List(Of Tuple(Of Integer, Integer))

    <DataMember()> _
    Property ListCitasMedicas As List(Of CitasMedicas)
    <DataMember()> _
    Property ServiceOrderDetail As List(Of ServiceOrderDetail)
    <DataMember()> _
    Property Ingreso As IngresoOrdenesServicio
    <DataMember()> _
    Property OperatingUnitId As Integer
    <DataMember()> _
    Property CareGroupId As Integer?
    <DataMember()>
    Property IngresoExistente As String

    <DataMember()>
    Property UnitTypeFunctionalUnit As Integer

    <DataMember()> _
    Property ListHemocomponent As List(Of Hemocomponent)
End Class