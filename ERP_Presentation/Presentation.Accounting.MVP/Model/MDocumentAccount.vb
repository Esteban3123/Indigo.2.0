'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 23-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.GlosasRepository

#End Region

Public Class MDocumentAccount
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    ''' Obtiene la secuencia numerica asignada al formulario
    ''' </summary>
    ''' <returns>Secuencia numerica</returns>
    Public Async Function GetSequense() As Task(Of Domain.Entities.GeneralLedgerSequence)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetSequenseByIdFormAsync(Me._tagForm)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Validates the period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Async Function ValidatePeriod(ByVal month As Integer, ByVal year As Integer) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ValidatePeriodOpenAsync(month, year)
    End Function

    ''' <summary>
    ''' Obtiene el periodo abierto
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOpenPeriod() As List(Of Domain.Entities.ClosedMonth)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetOpenPeriod()
    End Function

    ''' <summary>
    ''' Metodo pafa guardar un documento contable
    ''' </summary>
    ''' <param name="doc">el documento contable</param>
    ''' <returns>Resultado</returns>
    Public Async Function SaveDocumentAccounting(ByVal doc As Domain.Entities.JournalVouchers) As Task(Of ActionMessageResult(Of Domain.Entities.JournalVouchers))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess2 As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header2)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveAccountingDocumentAsync(doc)
        End Using
    End Function

    ''' <summary>
    ''' Gets the accounting documente by consecutive.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <returns></returns>
    Public Async Function GetAccountingDocumenteByConsecutive(ByVal consecutive As String, ByVal Tracking As Boolean) As Task(Of Domain.Entities.JournalVouchers)

        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountingDocumenteByConsecutiveAsync(consecutive, Tracking)

    End Function

    ''' <summary>
    ''' Gets the accounting documente by consecutive.
    ''' </summary>
    ''' <param name="idJournalVourchers">The consecutive.</param>
    ''' <returns></returns>
    Public Async Function GetJournalVouchersbyIdAsync(ByVal idJournalVourchers) As Task(Of Domain.Entities.JournalVouchers)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetJournalVouchersByIdAsync(idJournalVourchers)
    End Function

    ''' <summary>
    ''' Gets the details documente by journalVoucherId.
    ''' </summary>
    ''' <param name="journalVoucherId">The JournalVoucher Id.</param>
    ''' <returns></returns>
    Public Async Function GetJournalVoucherDetailsbyJournalVoucherId(ByVal journalVoucherId As Integer) As Task(Of ActionResult(Of List(Of JournalVoucherDetails)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetJournalVoucherDetailsAsync(journalVoucherId)
    End Function


    ''' <summary>
    ''' Gets the accounting documente by consecutive.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <returns></returns>
    Public Async Function GetAccountingDocumenteByConsecutiveIdJournalVoucherType(ByVal consecutive As String, ByVal idDocuemnt As Integer) As Task(Of Domain.Entities.JournalVouchers)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountingDocumentByConsecutiveIdDocumentAsync(consecutive, idDocuemnt)
    End Function

    ''' <summary>
    ''' Gets the accounting documente by consecutive.
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <returns></returns>
    Public Async Function GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(legalbookId As Integer, journalVoucherTypeId As Integer, consecutive As Integer) As Task(Of Domain.Entities.JournalVouchers)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookIdAsync(legalbookId, journalVoucherTypeId, consecutive)
    End Function

    ''' <summary>
    ''' Crea el bloqueo de un registro
    ''' </summary>
    ''' <param name="record">Registro a bloquear</param>
    ''' <returns>Resultados de la acción</returns>
    Public Async Function SaveBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult(Of BlockRecordGeneralLedger))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveBlockRecordAccountingAsync(record)
        End Using
    End Function

    ''' <summary>
    ''' Elimina el bloqueo del registro
    ''' </summary>
    ''' <param name="record">Registro a desbloquear</param>
    ''' <returns>Resultado de la acción</returns>
    Public Async Function DeleteBlockRecord(ByVal record As BlockRecordGeneralLedger) As Task(Of ActionResult)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.DeleteBlockRecordAccountingAsync(record)
        End Using
    End Function

    ''' <summary>
    ''' Consulta si el registro se encuentra bloqueado
    ''' </summary>
    ''' <param name="IdForm">Id del frontal</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' la información de bloqueo del registro</returns>
    Public Async Function GetBlockRecord(ByVal idForm As String, ByVal idRecord As String) As Task(Of BlockRecordGeneralLedger)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetBlockRecordAccountingByIdformAndIdRecordAsync(idForm, idRecord)
        End Using
    End Function

    ''' <summary>
    ''' Gets the journal vourchers by status.
    ''' </summary>
    ''' <param name="Status">The status.</param>
    ''' <param name="_month">The _month.</param>
    ''' <returns></returns>
    Public Async Function GetJournalVourchersByStatus(Status As Integer, ByVal _month As Integer, _year As Integer) As Task(Of List(Of SP_GetJournalVouchersByStatus_Result))

        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetJournalVourchersByStatusAsync(Status, _month, _year)
    End Function

    ''' <summary>
    ''' funcion para confirmar varios documentos contables y realizar las operaciones en la tabla de saldos de contabilidad
    ''' </summary>
    ''' <param name="Status">el estado confirmado del documento (2).</param>
    ''' <param name="ListIdJournalVouchers">los ids de los documentos que se quieren confirmar.</param>
    ''' <param name="month">el mes  del que se quieren confirmar los documentos|.</param>
    ''' <returns></returns>
    Public Async Function SaveListJournalVouchers(Status As Integer, ListIdJournalVouchers As String, month As Integer) As Task(Of List(Of Domain.Entities.SP_ChangeStatusJournalVouchers_Result))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SaveListJournalVouchersAsync(Status, ListIdJournalVouchers, month)
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante
    ''' </summary>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherDetailsByJournalVoucherId(journalVoucherId As Integer) As XPCollection(Of JournalVoucherDetailsXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVourcherDetailsByJournalVoucherId(journalVoucherId)
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante
    ''' </summary>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherDetailsImportedByJournalVoucherId(journalVoucherId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVourcherDetailsImportedByJournalVoucherId(journalVoucherId)
    End Function


    Public Function ListBook() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListBook()
    End Function

    Public Function ListBookByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Function

    Public Function ListBookJournalVoucherHomologations(bookId) As XPCollection
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListBookJournalVoucherHomologations(bookId)
    End Function


    Public Function GetGlosasObjectionDByInvoiceNumber(invoiceNumber As String) As GlosasObjectionDXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.GetGlosasObjectionDByInvoiceNumber(invoiceNumber)
    End Function

    Public Function GetGlosasObjectionCById(id As Integer) As GlosasObjectionCXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.GetGlosasObjectionCById(id)
    End Function


    Public Function GetCountVouchers(id As Integer) As Integer
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetCountVouchers(id)
    End Function

    Public Function GetDebitCreditVouchers(id As Integer, Optional debit As Boolean = True) As Decimal
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetDebitCreditVouchers(id, debit)
    End Function
    ''' <summary>
    ''' metodo para copiar y pegar en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetJournalVoucherDetailsCopyPaste(LegalBookId As Integer, data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of Domain.Entities.JournalVoucherDetails)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.SetJournalVoucherDetailsCopyPasteAsync(LegalBookId, data)
    End Function
    ''' <summary>
    ''' metodo para validar el archivo para importar detalles del comprobante
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateFileJournalVoucherDetails(LegalBookId As Integer, data As List(Of ImportFileRow)) As ActionResult(Of List(Of JournalVoucherDetails))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.ValidateFileJournalVoucherDetails(LegalBookId, data)
    End Function

    ''' <summary>
    ''' Calcula la retención de tipo variable
    ''' </summary>
    ''' <param name="BaseValue"></param>
    ''' <param name="MinBase"></param>
    ''' <param name="Percentaje"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    Public Function CalculateRetention(BaseValue As Decimal, MinBase As Decimal, Percentaje As Decimal, ThirdPartyId As Integer, MainAccountId As Integer) As ActionResult(Of Object)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.CalculateRetention(BaseValue, MinBase, Percentaje, ThirdPartyId, MainAccountId)
    End Function

    Public Function GetJournalVouchersByIdOnlyHead(id As Integer) As JournalVouchers
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetJournalVouchersByIdOnlyHead(id)
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherHomologations(AccountingMovementId As Integer, journalVoucherId As Integer) As XPCollection(Of JournalVouchersXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVourcherHomologations(AccountingMovementId, journalVoucherId)
    End Function
    ''' <summary>
    ''' metodo para generar las homologaciones del comprobante contable
    ''' </summary>
    Public Function GenerateHomologationsJournalVoucher(journalVourcher As Domain.Entities.JournalVouchers, legalBookId As Integer) As ActionResult(Of List(Of JournalVoucherDetails))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GenerateHomologationsJournalVoucher(journalVourcher, legalBookId)
    End Function

    ''' <summary>
    ''' Lista los comprobantes contables por libro y tipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListJournalVourchersByLegalBookAndJournalVoucherType(legalBookId As Integer, journalVoucherTypeId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.ListJournalVourchersByLegalBookAndJournalVoucherType(legalBookId, journalVoucherTypeId)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
