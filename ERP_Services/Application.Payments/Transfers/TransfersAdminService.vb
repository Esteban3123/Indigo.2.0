'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 31-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core
Imports System.Transactions
Imports Domain.Entities.Service
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

Public Class TransfersAdminService
    Implements ITransfersAdminService

#Region "Properties"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _transferRepository As ITransfersRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository
    ''' <summary>
    ''' Repositorio de documento contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountingRepository As IAccountingDocumentAdminService
    ''' <summary>
    ''' Repositorio de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentsAdminServie As IMoneyAdvanceAdminService
    ''' <summary>
    ''' Repositorio de cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableAdminService As IAccountPayableAdminService
    ''' <summary>
    ''' Servicios de aplicacion de control en pagos
    ''' </summary>
    ''' <remarks></remarks>
    Private _paymentControlAdminService As IPaymentControlAdminService

    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsPaymentsRepository As ISettingPaymentsRepository
    ''' <summary>
    ''' Variable tipo repositorio para cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository
    ''' <summary>
    ''' Variable tipo repositorio para anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentsRepository As IMoneyAdvanceRepository
    Private _repositoryCloseMont As ICloseMonthRepository
    Private _repositoryMainAccounts As IPUCRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal transferRepository As ITransfersRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository, ByVal accountingRepository As IAccountingDocumentAdminService,
                   ByVal advancePaymentsAdminServie As IMoneyAdvanceAdminService, ByVal accountPayableAdminService As IAccountPayableAdminService, ByVal settingsPaymentsRepository As ISettingPaymentsRepository,
                   ByVal accountPayableRepository As IAccountPayableRepository, ByVal advancePaymentsRepository As IMoneyAdvanceRepository, ByVal paymentControlAdminService As IPaymentControlAdminService,
                   ByVal repositoryCloseMont As ICloseMonthRepository, ByVal repositoryMainAccounts As IPUCRepository)
        If transferRepository Is Nothing Then
            Throw New ArgumentNullException("transferRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If accountingRepository Is Nothing Then
            Throw New ArgumentNullException("accountingRepository")
        End If
        If advancePaymentsAdminServie Is Nothing Then
            Throw New ArgumentNullException("advancePaymentsAdminServie")
        End If
        If accountPayableAdminService Is Nothing Then
            Throw New ArgumentNullException("accountPayableAdminService")
        End If
        If settingsPaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("settingsPaymentsRepository")
        End If
        If accountPayableRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableRepository")
        End If
        If advancePaymentsRepository Is Nothing Then
            Throw New ArgumentNullException("advancePaymentsRepository")
        End If
        If paymentControlAdminService Is Nothing Then
            Throw New ArgumentNullException("paymentControlAdminService Vacio")
        End If
        _transferRepository = transferRepository
        _secuenseDRepository = secuenseDRepository
        _accountingRepository = accountingRepository
        _advancePaymentsAdminServie = advancePaymentsAdminServie
        _accountPayableAdminService = accountPayableAdminService
        _settingsPaymentsRepository = settingsPaymentsRepository
        _accountPayableRepository = accountPayableRepository
        _advancePaymentsRepository = advancePaymentsRepository
        _paymentControlAdminService = paymentControlAdminService
        _repositoryCloseMont = repositoryCloseMont
        _repositoryMainAccounts = repositoryMainAccounts
    End Sub

#End Region

#Region "Methods"

    Public Function ImportBillsToPortfolioNote(data As List(Of List(Of String)), ParamArray parameters() As Object) As ActionResult(Of List(Of PaymentTransferDetail)) Implements ITransfersAdminService.ImportBillsToPaymentTransfer
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("Data")
        End If

        'Listado de errores
        Dim listErrors As New List(Of String)

        'Listado que se devuelve para pegar a la rejilla del form
        Dim ListPaymentTransferDetails As New List(Of PaymentTransferDetail)

        Try
            Dim xmlParameters = ConvertParametersToXml(parameters)
            Dim xmlObject = ConvertBillsToXml(parameters, data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _transferRepository.SP_ImportBillsToPaymentTransfer(xmlObject, xmlParameters)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 1 Then 'Si el estado del item es True y pasó todas las validaciones
                        'Se crea el nuevo objeto para agregarlo al listado
                        Dim accountPayable As PaymentTransferDetail = ListPaymentTransferDetails.Where(Function(x) x.AccountPayableShareId = itemXml.AccountPayableShareId).FirstOrDefault()
                        If accountPayable Is Nothing Then
                            accountPayable = New PaymentTransferDetail() With
                            {
                                .SupplierId = itemXml.SupplierId,
                                .SupplierDescription = itemXml.SupplierCodeName,
                                .AccountPayableId = itemXml.AccountPayableId,
                                .NumberBill = itemXml.BillNumber,
                                .BalanceBill = itemXml.Balance,
                                .AccountPayableShareId = itemXml.AccountPayableShareId,
                                .Share = itemXml.Share,
                                .BalanceShare = itemXml.BalanceShare,
                                .MainAccountId = itemXml.MainAccountId,
                                .NumberNameMainAccount = itemXml.MainAccountNumberName,
                                .ThirdPartyId = itemXml.ThirdPartyId,
                                .CostCenterId = itemXml.CostCenterId,
                                .Value = itemXml.CrossShareValue,
                                .TRMValue = itemXml.TRMValue
                            }

                            ListPaymentTransferDetails.Add(accountPayable)
                        End If
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            Return New ActionResult(Of List(Of PaymentTransferDetail)) With {.StateResult = True, .ObjectEmbbeded = ListPaymentTransferDetails, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of PaymentTransferDetail)) With {.StateResult = False, .MessageResult = {"Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}.ToList()}
            Else
                Return New ActionResult(Of List(Of PaymentTransferDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of PaymentTransferDetail)) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Elimina un traslado
    ''' </summary>
    ''' <param name="trasnfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePaymentsTransfer(trasnfer As PaymentTransfer, audit As AuditMessage) As ActionResult Implements ITransfersAdminService.DeletePaymentsTransfer
        If trasnfer Is Nothing Then
            Throw New ArgumentNullException("trasnfer")
        End If
        Dim unitOfWork As IUnitWork = Me._transferRepository.UnitWork
        Try
            trasnfer.StartTracking()
            While trasnfer.PaymentTransferDetail.Count > 0
                trasnfer.PaymentTransferDetail.Item(0).MarkAsDeleted()
            End While
            trasnfer.MarkAsDeleted()
            Me._transferRepository.SaveEntity(trasnfer)
            unitOfWork.Commit()
            'Auditoria básica
            IndigoAuditBasic.Execute(GetType(PaymentTransfer).Name, audit.Functional, trasnfer.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, Infrastructure.CrossCutting.Base.ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            'Ausitoria avanzada
            Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentTransfer)(trasnfer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTransfer(code As String, audit As AuditMessage) As ActionResult(Of PaymentTransfer) Implements ITransfersAdminService.GetPaymentsTransfer
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim trasnfer As PaymentTransfer = Me._transferRepository.GetPaymentsTransfer(code.Trim())
            If trasnfer IsNot Nothing AndAlso trasnfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentTransfer)(trasnfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PaymentTransfer) With {.StateResult = True, .ObjectEmbbeded = trasnfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsTransferById(id As Integer, audit As AuditMessage) As ActionResult(Of PaymentTransfer) Implements ITransfersAdminService.GetPaymentsTransferById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim trasnfer As PaymentTransfer = Me._transferRepository.GetPaymentsTransferById(id)
            If trasnfer IsNot Nothing AndAlso trasnfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of PaymentTransfer)(trasnfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of PaymentTransfer) With {.StateResult = True, .ObjectEmbbeded = trasnfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado
    ''' </summary>
    ''' <param name="trasnfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePaymentsTransfer(trasnfer As PaymentTransfer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PaymentTransfer) Implements ITransfersAdminService.SavePaymentsTransfer
        If trasnfer Is Nothing Then
            Throw New ArgumentNullException("trasnfer")
        End If
        Dim unitOfWork As IUnitWork = Me._transferRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As PaymentsSecuenceDetail = Nothing
                If trasnfer.Code Is Nothing OrElse trasnfer.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            trasnfer.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                'Valido estado del registro
                If trasnfer.Id > 0 Then
                    Dim trasnferTmp = _transferRepository.GetPaymentsTransferById(trasnfer.Id, False)
                    If trasnferTmp IsNot Nothing AndAlso Not trasnferTmp.Status = 1 Then
                        Transaction.Dispose()
                        Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {String.Format("El cruce de anticipo vs CxP se encuentra en estado: {0}", If(trasnferTmp.Status = 2, "Confirmado", "Anulado"))}.ToList()}
                    End If
                End If

                'Pregunto si el periodo esta abierto
                Dim accountingServices As New AccountingServices(_repositoryMainAccounts, _repositoryCloseMont)
                Dim validatePeriod = accountingServices.ValidatePeriodAndReturnOpendMonths(trasnfer.DocumentDate)
                If validatePeriod.StateResult = False Then
                    Transaction.Dispose()
                    Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {validatePeriod.Message}.ToList()}
                End If

                Dim auxTransfer As PaymentTransfer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of PaymentTransfer)
                Dim status As Integer

                If trasnfer.ChangeTracker.State = ObjectState.Added Then
                    trasnfer.CreationUser = audit.CodeUser
                    trasnfer.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf trasnfer.ChangeTracker.State = ObjectState.Modified Then
                    auxTransfer = trasnfer.OriginalValue
                    If trasnfer.Status = 1 Then
                        trasnfer.ModificationUser = audit.CodeUser
                        trasnfer.ModificationDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Update
                    ElseIf trasnfer.Status = 2 Then
                        trasnfer.Status = 1
                        status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                    ElseIf trasnfer.Status = 3 Then
                        trasnfer.ModificationUser = audit.CodeUser
                        trasnfer.ModificationDate = DateTime.Now
                        trasnfer.AnnulmentUser = audit.CodeUser
                        trasnfer.AnnulmentDate = DateTime.Now
                        status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End If
                End If

                If trasnfer.Status = 1 Then
                    Dim paymentControl = _paymentControlAdminService.GetPaymentControlByDocumentNumber(trasnfer.Code, 4)
                    paymentControl.DocumentNumber = trasnfer.Code
                    paymentControl.DocumentType = 4
                    paymentControl.DocumentUser = audit.CodeUser
                    paymentControl.DocumentDate = trasnfer.DocumentDate
                    If paymentControl.Id > 0 Then
                        paymentControl.MarkAsModified()
                    End If
                    Dim resultSaveControl = _paymentControlAdminService.SavePaymentControl(paymentControl, audit)
                    If resultSaveControl.StateResult = False Then
                        Transaction.Dispose()
                        Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {ResourceManager.GetString("SavePaymentControlError", "Payments")}.ToList}
                    End If
                Else
                    Dim paymentControl = _paymentControlAdminService.GetPaymentControlByDocumentNumber(trasnfer.Code, 4)
                    If paymentControl IsNot Nothing AndAlso paymentControl.Id > 0 Then
                        paymentControl.MarkAsDeleted()
                        _paymentControlAdminService.DeletePaymentControl(paymentControl, audit)
                    End If
                End If

                Me._transferRepository.SaveEntity(trasnfer)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of PaymentTransfer)(trasnfer, audit, status, auxTransfer)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                trasnfer.MarkAsUnchanged()
                Transaction.Complete()
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = True, .ObjectEmbbeded = trasnfer}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Confirma el traslado
    ''' </summary>
    ''' <param name="paymentTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConfirmTransfer(paymentTransfer As PaymentTransfer, audit As AuditMessage) As ActionResult(Of PaymentTransfer) Implements ITransfersAdminService.ConfirmTransfer
        If paymentTransfer Is Nothing Then
            Throw New ArgumentNullException("paymentTransfer")
        End If

        Dim unitOfWork As IUnitWork = Me._transferRepository.UnitWork

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim resultStore = Me._transferRepository.SP_ConfirmPaymentTransfer(paymentTransfer.Id, audit.CodeUser)
                If resultStore Is Nothing OrElse resultStore.CodeMessage <> 0 Then
                    unitOfWork.RollbackChanges()
                    transaction.Dispose()
                    Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {resultStore.Message}.ToList()}
                End If

                Dim auditProcess As IndigoAuditSimpleEntity(Of PaymentTransfer)
                auditProcess = New IndigoAuditSimpleEntity(Of PaymentTransfer)(paymentTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Confirm, Nothing)
                auditProcess.Execute()

                transaction.Complete()
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = True, .ObjectEmbbeded = paymentTransfer, .MessageResult = {resultStore.Consecutive, resultStore.Message}.ToList()}
            Catch ex As Exception
                transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .MessageResult = {Utils.GetInnerExceptionMessageToString(ex)}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Guarda y confirma el traslado
    ''' </summary>
    ''' <param name="paymentTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAndConfirmTransfer(paymentTransfer As PaymentTransfer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of PaymentTransfer) Implements ITransfersAdminService.SaveAndConfirmTransfer
        Dim result = SavePaymentsTransfer(paymentTransfer, audit, idSequense)
        If result.StateResult Then
            Dim resultComfirm = ConfirmTransfer(result.ObjectEmbbeded, audit)
            If resultComfirm.StateResult Then
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = True, .StateResultAux = True, .ObjectEmbbeded = resultComfirm.ObjectEmbbeded, .MessageResult = resultComfirm.MessageResult}
            Else
                Return New ActionResult(Of PaymentTransfer) With {.StateResult = True, .StateResultAux = False, .ObjectEmbbeded = result.ObjectEmbbeded, .MessageResult = resultComfirm.MessageResult}
            End If
        Else
            Return New ActionResult(Of PaymentTransfer) With {.StateResult = False, .StateResultAux = False, .MessageResult = result.MessageResult}
        End If
    End Function

#End Region

#Region "Methods Privates"

    Private Function ConvertParametersToXml(parameters() As Object)
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        builder.Append("<TransferType>" & parameters.ElementAt(0) & "</TransferType>")
        builder.Append("<SupplierId>" & parameters.ElementAt(1) & "</SupplierId>")
        builder.Append("<CurrencyId>" & parameters.ElementAt(2) & "</CurrencyId>")

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertBillsToXml(parameters() As Object, data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim position As Integer = 0
        Dim SupplierCode As String = String.Empty
        Dim BillNumber As String = String.Empty
        Dim CrossShare As String = String.Empty

        For Each item In data
            position = position + 1

            If parameters.ElementAt(0) = 1 Then
                If item.Count <> 2 Then
                    builder.Append("<Row>")

                    builder.Append("<StatusField>" & 0 & "</StatusField>")
                    builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                    builder.Append("</Row>")
                    Continue For
                End If

                BillNumber = item(0)
                CrossShare = item(1)
            ElseIf parameters.ElementAt(0) = 2 Then
                If item.Count <> 3 Then
                    builder.Append("<Row>")

                    builder.Append("<StatusField>" & 0 & "</StatusField>")
                    builder.Append("<MessageField>" & String.Format("El registro {0} no tiene la estructura requerida", position) & "</MessageField>")

                    builder.Append("</Row>")
                    Continue For
                End If

                SupplierCode = item(0)
                BillNumber = item(1)
                CrossShare = item(2)
            Else
                builder.Append("<Row>")

                builder.Append("<StatusField>" & 0 & "</StatusField>")
                builder.Append("<MessageField>" & String.Format("El registro {0} tiene un tipo incorrecto", position) & "</MessageField>")

                builder.Append("</Row>")
                Continue For
            End If

            builder.Append("<Row>")

            builder.Append("<CountFields>" & item.Count & "</CountFields>")
            builder.Append("<StatusField>" & 1 & "</StatusField>")
            builder.Append("<MessageField>" & "Ok" & "</MessageField>")
            builder.Append("<SupplierCode>" & SupplierCode & "</SupplierCode>")
            builder.Append("<BillNumber>" & BillNumber & "</BillNumber>")
            builder.Append("<CrossShare>" & CrossShare.Replace(",", ".") & "</CrossShare>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _advancePaymentsAdminServie.Dispose()
                _accountPayableAdminService.Dispose()
                _paymentControlAdminService.Dispose()
            End If
            _transferRepository = Nothing
            _secuenseDRepository = Nothing
            _accountingRepository = Nothing
            _advancePaymentsAdminServie = Nothing
            _accountPayableAdminService = Nothing
            _settingsPaymentsRepository = Nothing
            _accountPayableRepository = Nothing
            _advancePaymentsRepository = Nothing
            _paymentControlAdminService = Nothing
            _repositoryCloseMont = Nothing
            _repositoryMainAccounts = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
