'***********************************************************************
' Assembly         : Presentacion.Payments.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.CloudAgent

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporacion
''' </summary>
Public Class MAccountPayable
    Implements IDisposable

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPaymentConceptById(ByVal id As Integer) As Task(Of ActionResult(Of AccountPayableConcepts))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentConceptByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayable(ByVal code As String) As Task(Of AccountPayable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una cxp por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableById(ByVal Id As Integer) As AccountPayable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableById(Id, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function GetAccountPayableByIdAsync(ByVal Id As Integer) As Task(Of AccountPayable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByIdAsync(Id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una cxp por Id para notas
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetAccountPayableByIdForNotes(ByVal Id As Integer) As AccountPayable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByIdForNotes(Id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtiene las cuotas de una factura por el id de la factura, id del tercero y el estado
    ''' </summary>
    Public Async Function GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short) As Task(Of List(Of AccountPayableShares))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndStateAsync(IdThird, IdAccount, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the state of the count account payable share by account pay identifier third identifier account and.
    ''' </summary>
    ''' <param name="IdThird">The identifier third.</param>
    ''' <param name="IdAccount">The identifier account.</param>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Public Function GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(ByVal IdThird As Integer, ByVal IdAccount As Integer, ByVal state As Short) As Integer
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetCountAccountPayableShareByAccountPayIdThirdIdAccountAndState(IdThird, IdAccount, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una couta de factura por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetAccountPayableShareById(ByVal Id As Integer) As Task(Of AccountPayableShares)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableShareByIdAsync(Id, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function GetAccountPayableShareByIdSimple(ByVal Id As Integer) As AccountPayableShares
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableShareById(Id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un cuenta por pagar por su numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableByBillNumber(ByVal code As String, ByVal idSupplier As Integer) As Task(Of AccountPayable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByBillNumberAsync(code, idSupplier)
    End Function

    ''' <summary>
    ''' Obtiene un cuenta por pagar por su numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableByBillNumberAndMainAccount(ByVal code As String, ByVal idSupplier As Integer, mainAccountId As Integer) As Task(Of AccountPayable)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByBillNumberAndMainAccountAsync(code, idSupplier, mainAccountId)
    End Function

    ''' <summary>
    ''' Obtiene un cuenta por pagar por su numero de factura y id del proveedor
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableByBillNumberSimple(ByVal code As String, ByVal idSupplier As Integer) As AccountPayable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByBillNumber(code, idSupplier)
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableByCode(ByVal code As String) As Task(Of List(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableSharesByIdAccountPayable(ByVal id As Integer) As List(Of AccountPayableShares)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableSharesByIdAccountPayable(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas por id proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableByIdSupplier(ByVal id As Integer) As Task(Of List(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByIdSupplierAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un listado de facturas por id proveedor y el estado: confirmado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayableByIdSupplierAndState(ByVal id As Integer, ByVal status As Integer) As Task(Of List(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByIdSupplierAndStateAsync(id, status, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene las facturas por tercero y estado
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Public Async Function GetAccountPayableByIdThirdIdAccountAndState(ByVal id As Integer, ByVal IdAccount As Integer, ByVal state As Short) As Task(Of List(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByIdThirdIdAccountAndStateAsync(id, IdAccount, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function GetAccountPayableByIdThirdIdAccountAndStateSimple(ByVal id As Integer, ByVal IdAccount As Integer, ByVal state As Short) As List(Of AccountPayable)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableByIdThirdIdAccountAndState(id, IdAccount, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCostCenterById(ByVal id As Integer) As Task(Of CostCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetCostCenterByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterId(ByVal id As Integer) As CostCenter
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetCostCenterById(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAccountPayable(ByVal record As AccountPayable, ByVal idSequense As Int64) As Task(Of ActionResult(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveAccountPayableAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una lista de cuentas por pagar que tiene asociado la unidad de radicacion
    ''' </summary>
    ''' <param name="FilingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetListAccountPayableByFilingUnitId(ByVal FilingUnitId As Integer) As Task(Of ActionResult(Of List(Of AccountPayable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetListAccountPayableByFilingUnitIdAsync(FilingUnitId)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function AnnularAccountPayable(ListAccountPayable As List(Of AccountPayable), ByVal idSequense As Int64) As Task(Of ActionResult(Of List(Of AccountPayable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.AnnularAccountPayableAsync(ListAccountPayable, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza las facturas de pago
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveListAccountPayable(ByVal record As List(Of AccountPayable), ByVal listDeferredCausation As List(Of DeferredCausation), modeSaveAndConfirm As Boolean, ByVal idSequense As Int64) As Task(Of ActionResult(Of List(Of String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SaveListAccountPayableAsync(record, listDeferredCausation, modeSaveAndConfirm, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ChangeStateAccountPayableAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAccountPayable(ByVal ListAccountPayable As List(Of AccountPayable)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.DeleteAccountPayableAsync(ListAccountPayable, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Valida que la cxp tenga causacion diferida
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Async Function GetValidationDeferredCausation(accountPayableId As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetValidationDeferredCausationAsync(accountPayableId)
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <returns>La factura de cartera</returns>
    Public Async Function GetServerDate() As Task(Of DateTime)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDateAsync()
    End Function

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCheckExistAccountPayable() As Task(Of Domain.Base.Entities.ActionResult(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetCheckExistAccountPayableAsync()
    End Function

    ''' <summary>
    ''' Consulta si hay registros de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetUltimateRegisterConsecutiveFiling() As Task(Of Domain.Base.Entities.ActionResult(Of AccountPayable))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetUltimateRegisterConsecutiveFilingAsync
    End Function

    Public Function GetElectronicSupportDocumentByDocumentOrigin(EntityId As Integer, EntityName As String) As XPCollection(Of ElectronicSupportDocumentXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetElectronicSupportDocumentByDocumentOrigin(EntityId, EntityName)
    End Function

    ''' <summary>
    ''' Lista los detalles de un folio o una factura
    ''' </summary>
    Public Function ListAccountPayableByFilingUnitId(ByVal filingUnitId As Integer) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableByFilingUnitId(filingUnitId)
    End Function

    Public Function ListRefundByTransfer(ByVal filingUnitId As Integer) As XPCollection(Of RefundXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListRefundByTransfer(filingUnitId)
    End Function

    Public Function InitializeAccountPayableCOncepts(ByVal status As Boolean, ByVal handlesRetention As Boolean, Optional session As DevExpress.Xpo.Session = Nothing) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListConceptsAccountsPayableByStatusAndHandlesRetention(status, handlesRetention, session)
    End Function

    Public Async Function ImportBillsToAccountPayable(dataCopyPaste As List(Of List(Of String)), dataImport As List(Of ImportFileRow), parameters As List(Of Object)) As Task(Of ActionResult(Of List(Of AccountPayable)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.ImportBillsToAccountPayableAsync(dataCopyPaste, dataImport, parameters)
    End Function

    ''' <summary>
    ''' Funcion para obtener el fabricante 
    ''' </summary>
    ''' <param name="id">El id del fabricante</param>
    ''' <returns></returns>
    Public Function GetThirdPartyById(ByVal id As Integer) As Domain.Entities.ThirdParty
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetThirdPartyById(id, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el centro de costo por Id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenterInfoById(ByVal id As Integer) As Task(Of CostCenter)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetCostCenterByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Funcion para obtener los datos de la cuenta contable
    ''' </summary>
    ''' <param name="id">El id del fabricante</param>
    ''' <returns></returns>
    Public Function GetMainAccountById(Id As Integer) As Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.GeneralLedgerMainAccountsXpo)(Nothing, filtroConsulta).FirstOrDefault
    End Function


    ''' <summary>
    ''' metodo para copiar y pegar o importar un archivo de excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetCopyPasteOrImportFileDeferredCausation(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), valueShare As Decimal) As ActionResult(Of List(Of DeferredCausationDetails))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.SetCopyPasteOrImportFileDeferredCausation(dataImportFile, dataCopyPaste, valueShare)
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