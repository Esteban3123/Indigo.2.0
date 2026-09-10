Imports System.Runtime.Serialization

Public Class ViewPharmaDoseMixingStation
    <DataMember()>
    Public Property Id As Guid
    <DataMember()>
    Public Property PackageId As Integer
    <DataMember()>
    Public Property QuantityDelivered As Integer
    <DataMember()>
    Public Property AppliedDose As Integer
    <DataMember()>
    Public Property QuantityReceivable As Integer
    <DataMember()>
    Public Property DeliveryStatus As Byte
    <DataMember()>
    Public Property BatchCode As String
    <DataMember()>
    Public Property DispensingDate As Date?
    <DataMember()>
    Public Property FunctionalUnitId As Integer
    <DataMember()>
    Public Property SurchargeApply As Boolean
    <DataMember()>
    Public Property OrderedHealthProfessionalCode As String
    <DataMember()>
    Public Property OrderedHealthProfessionalThirdPartyId As Integer
    <DataMember()>
    Public Property HealthAdministratorId As Integer?
    <DataMember()>
    Public Property PerformsProfessionalSpecialty As String
    <DataMember()>
    Public Property WarehouseId As Integer
    ''' <summary>
    ''' Id del paquete personalizado utilizado para producir el producto terminado.
    ''' Cuando está presente, los componentes no-principales deben calcular sus
    ''' cantidades desde PackagePersonalizedDetail (Quantity mL / Concentration)
    ''' en lugar de usar la tarifa estándar de PackageDetail (Quantity / DoseNumber).
    ''' </summary>
    <DataMember()>
    Public Property PackagePersonalizedId As Integer?
    ''' <summary>
    ''' Origen del producto en la solicitud de central de mezclas (RequestMixingStationDetail.Source).
    ''' Source = 4 indica dosis estandar: las cantidades se calculan desde PackageDetail.
    ''' </summary>
    <DataMember()>
    Public Property Source As Byte
End Class
