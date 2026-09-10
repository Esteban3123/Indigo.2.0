Imports System.Runtime.Serialization
Public Class SP_ListHCPRESCRDByCODCONCEC_Result

    <DataMember()>
    Public Property IsDeferred As Boolean

    <DataMember()>
    Public Property IPSCode As String

    ''' <summary>
    ''' Id del producto, este campo se asigna cuando se realiza una dispensación manual
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property ProductId As Integer?

    <DataMember>
    Public Property PerformsHealthProfessionalThirdPartyId As Integer?
    <DataMember>
    Public Property DateFormulation As DateTime?

    <DataMember()>
    Public Property DiagnosticCode As String
    <DataMember()>
    Public Property TreatmentDays As Integer
    <DataMember()>
    Public Property IDMipres As String
    <DataMember()>
    Public Property AuthorizationNumber As String

End Class
