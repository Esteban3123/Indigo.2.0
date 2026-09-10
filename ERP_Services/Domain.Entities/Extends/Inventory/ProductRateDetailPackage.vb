Imports System.Runtime.Serialization

Partial Public Class ProductRateDetailPackage
    <DataMember()>
    Public Property ItemCodeName As String
    <DataMember()>
    Public Property ProductCodeName As String
    <DataMember()>
    Public Property ItemType As Byte
    <DataMember()>
    Public Property TotalQuantities As Integer
    <DataMember()>
    Public Property UnitValue As Decimal
    <DataMember()>
    Public Property TotalValue As Decimal
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
    Public Property WarehouseId As Integer

    <DataMember()>
    Public Property ItemId As Integer?
    <DataMember>
    Public Property MainMedicine As Boolean
    <DataMember>
    Public Property Thinner As Boolean
    <DataMember>
    Public Property Vehicle As Boolean
End Class
