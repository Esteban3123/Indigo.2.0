#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IElectronicDocumentsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Actualiza el estado de un documento electrónico
    ''' </summary>
    ''' <returns></returns>
    Function UpdateStateElectronicDocument(id As Integer, status As Byte, audit As AuditMessage) As ActionResult(Of ElectronicDocument)

    ''' <summary>
    ''' Actualiza el estado de uno o más documentos electrónicos
    ''' </summary>
    ''' <returns></returns>
    Function UpdateStateElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument), audit As AuditMessage) As ActionResult(Of ElectronicDocument)

    ''' <summary>
    ''' Lista todas las facturas electronicas
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Function ListElectronicDocumentsTypeInvoices(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument)

    ''' <summary>
    ''' Lista todas las notas debitos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Function ListElectronicDocumentsTypeDebitNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument)

    ''' <summary>
    ''' Lista todas las notas creditos
    ''' por código de usuario
    ''' </summary>
    ''' <returns></returns>
    Function ListElectronicDocumentsTypeCreditNotes(OperatingUnitId As Integer, StatusId As Integer?) As List(Of ElectronicDocument)

    Function ReSendElectronicDocument(InvoiceNumber As String, InvoiceId As Integer, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Reenvia documentos electronicos al proceso de facturacion electronica en Costa Rica
    ''' </summary>
    ''' <param name="listElectronicDocuments"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Function ReSendElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument), session As SessionValues) As ActionResult


End Interface