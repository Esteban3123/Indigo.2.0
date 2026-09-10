Imports System.Runtime.Serialization

<DataContract>
Public Class StayInfoModel
    <DataMember>
    Public Property Selected As Boolean
    <DataMember>
    Public Property AdmissionCode As String
    <DataMember>
    Public Property Bed As String
    <DataMember>
    Public Property FunctionalUnitCode As String
    <DataMember>
    Public Property FunctionalUnitName As String
    <DataMember>
    Public Property InitialDate As Date
    <DataMember>
    Public Property EndDate As Date
    <DataMember>
    Public Property LiquidationType As Byte
    <DataMember>
    Public Property StayInBed As String 'no creo que se deba devolver, la rejilla puede mostrar con demas datos
    <DataMember>
    Public Property Quantity As Integer
    <DataMember>
    Public Property LiquidationDate As Date?
    <DataMember>
    Public Property CUPSId As Integer
    <DataMember>
    Public Property CUPsCodeName As String
    <DataMember>
    Public Property StayTypeCode As String
    <DataMember>
    Public Property StayTypeName As String
    <DataMember>
    Public Property GENSERVICEORDER As Integer?
    <DataMember>
    Public Property Stay As CHREGESTA
    <DataMember>
    Public Property ACJustificationId As Integer?

    <DataMember>
    Public Property JustificationCodeName As String

    <DataMember>
    Public Property SkipLiquidation As Boolean

    <DataMember>
    Public Property AccountControlStaysId As Integer

    <DataMember>
    Public Property CUPSEntityContractDescriptionId As Integer?
    <DataMember>
    Public Property ContractDescriptionId As Integer?
    <DataMember>
    Public Property ContractDescriptionCodeName As String
End Class