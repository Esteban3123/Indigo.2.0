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
End Class
