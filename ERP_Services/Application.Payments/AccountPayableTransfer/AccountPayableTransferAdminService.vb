'***********************************************************************
' Assembly         : Application.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
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
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports System.Text

Public Class AccountPayableTransferAdminService
    Implements IAccountPayableTransferAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableTransferRepository As IAccountPayableTransferRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequensePaymentsDRepository
    ''' <summary>
    ''' repositorio de detalle de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _AccountPayableTransferDetailRepository As IAccountPayableTransferDetailRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal accountPayableTransferRepository As IAccountPayableTransferRepository, ByVal secuenseDRepository As ISequensePaymentsDRepository, ByVal AccountPayableTransferDetailRepository As IAccountPayableTransferDetailRepository)
        If accountPayableTransferRepository Is Nothing Then
            Throw New ArgumentNullException("accountPayableTransferRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        If AccountPayableTransferDetailRepository Is Nothing Then
            Throw New ArgumentNullException("AccountPayableTransferDetailRepository Vacio")
        End If
        _accountPayableTransferRepository = accountPayableTransferRepository
        _AccountPayableTransferDetailRepository = AccountPayableTransferDetailRepository
        Me._secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountPayableTransfer(AccountPayableTransfer As AccountPayableTransfer, audit As AuditMessage) As ActionResult Implements IAccountPayableTransferAdminService.DeleteAccountPayableTransfer
        If AccountPayableTransfer Is Nothing Then
            Throw New ArgumentNullException("AccountPayableTransfer")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableTransferRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableTransfer)
            auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableTransfer)(AccountPayableTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._accountPayableTransferRepository.DeleteEntity(AccountPayableTransfer)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorDependence")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransfer(code As String, audit As AuditMessage) As ActionResult(Of AccountPayableTransfer) Implements IAccountPayableTransferAdminService.GetAccountPayableTransfer
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AccountPayableTransfer As AccountPayableTransfer = Me._accountPayableTransferRepository.GetAccountPayableTransfer(code.Trim())
            If AccountPayableTransfer IsNot Nothing AndAlso AccountPayableTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableTransfer)(AccountPayableTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = True, .ObjectEmbbeded = AccountPayableTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un trasado de factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountPayableTransferById(id As String, audit As AuditMessage) As ActionResult(Of AccountPayableTransfer) Implements IAccountPayableTransferAdminService.GetAccountPayableTransferById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim AccountPayableTransfer As AccountPayableTransfer = Me._accountPayableTransferRepository.GetAccountPayableTransferById(id)
            If AccountPayableTransfer IsNot Nothing AndAlso AccountPayableTransfer.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of AccountPayableTransfer)(AccountPayableTransfer, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = True, .ObjectEmbbeded = AccountPayableTransfer}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAccountPayableTransfer(AccountPayableTransfer As AccountPayableTransfer, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of AccountPayableTransfer) Implements IAccountPayableTransferAdminService.SaveAccountPayableTransfer
        If AccountPayableTransfer Is Nothing Then
            Throw New ArgumentNullException("AccountPayableTransfer")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableTransferRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork

        'Se valida que las facturas o los reembolsos ingresados en el detalle no existan previamente en la BD
        If AccountPayableTransfer.AccountPayableTransferDetail IsNot Nothing AndAlso AccountPayableTransfer.AccountPayableTransferDetail.Count > 0 Then
            Dim listErrors As New StringBuilder
            For Each itemDetail As AccountPayableTransferDetail In AccountPayableTransfer.AccountPayableTransferDetail
                If itemDetail.ChangeTracker.State = ObjectState.Added Then
                    If AccountPayableTransfer.TranferType = 1 Then 'Cuenta por pagar

                        Dim AccountPayableTmp = _accountPayableTransferRepository.GetAccountPayableFilingUnitSourceIdAccountPayableId(itemDetail.AccountPayableId, AccountPayableTransfer.FilingUnitSourceId)
                        If AccountPayableTmp Is Nothing Then
                            listErrors.AppendLine("La Cuenta por Pagar " & itemDetail.AccountPayableConsecutive & " con No. Factura " & itemDetail.AccountPayableBillNumber & ", no se encuentra en la unidad de origen seleccionada")
                            Continue For
                        End If

                        Dim _accountPayableTransferDetail = _accountPayableTransferRepository.GetAccountPayableTransferDetailByAccountPayableId(itemDetail.AccountPayableId)
                        If _accountPayableTransferDetail IsNot Nothing Then
                            listErrors.AppendLine("Ya existe un registro pendiente por aceptación con la Cuenta por Pagar " & itemDetail.AccountPayableConsecutive & " con No. Factura " & itemDetail.AccountPayableBillNumber)
                        End If
                    Else 'reembolso

                        Dim refundsTmp = _accountPayableTransferRepository.GetAccountPayableFilingUnitSourceIdRefundId(itemDetail.RefundId, AccountPayableTransfer.FilingUnitSourceId)
                        If refundsTmp Is Nothing Then
                            listErrors.AppendLine("El reembolso " & itemDetail.AccountPayableConsecutive & ", no se encuentra en la unidad de origen seleccionada")
                            Continue For
                        End If

                        Dim _accountPayableTransferDetail = _accountPayableTransferRepository.GetAccountPayableTransferDetailByRefundId(itemDetail.RefundId)
                        If _accountPayableTransferDetail IsNot Nothing Then
                            listErrors.AppendLine("Ya existe un registro pendiente por aceptación con el reembolso " & itemDetail.AccountPayableConsecutive)
                        End If
                    End If
                End If
            Next
            If listErrors.Length > 0 Then
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .Message = listErrors.ToString, .MessageResult = Nothing}
            End If
        End If

        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim seq As PaymentsSecuenceDetail = Nothing
                If AccountPayableTransfer.Code Is Nothing OrElse AccountPayableTransfer.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.PaymentsSecuence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            AccountPayableTransfer.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim auxAccountPayableTransfer As AccountPayableTransfer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableTransfer)
                Dim status As Integer

                If AccountPayableTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    AccountPayableTransfer.CreationUser = audit.CodeUser
                    AccountPayableTransfer.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert

                    If AccountPayableTransfer.Status = 2 Then
                        AccountPayableTransfer.ConfirmationUser = audit.CodeUser
                        AccountPayableTransfer.ConfirmationDate = DateTime.Now
                    End If
                Else
                    auxAccountPayableTransfer = AccountPayableTransfer.OriginalValue
                    Select Case AccountPayableTransfer.Status
                        Case 1
                            AccountPayableTransfer.ModificationUser = audit.CodeUser
                            AccountPayableTransfer.ModificationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Update
                        Case 2
                            AccountPayableTransfer.ModificationUser = audit.CodeUser
                            AccountPayableTransfer.ModificationDate = DateTime.Now
                            AccountPayableTransfer.ConfirmationUser = audit.CodeUser
                            AccountPayableTransfer.ConfirmationDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                        Case 3
                            AccountPayableTransfer.ModificationUser = audit.CodeUser
                            AccountPayableTransfer.ModificationDate = DateTime.Now
                            AccountPayableTransfer.AnnulmentUser = audit.CodeUser
                            AccountPayableTransfer.AnnulmentDate = DateTime.Now
                            status = Infrastructure.CrossCutting.Audit.Actions.Annular
                    End Select
                End If

                'Se recorre el detalle para llenar los campos de auditoria respectivos
                If AccountPayableTransfer.AccountPayableTransferDetail IsNot Nothing AndAlso AccountPayableTransfer.AccountPayableTransferDetail.Count > 0 Then
                    For Each itemDetail As AccountPayableTransferDetail In AccountPayableTransfer.AccountPayableTransferDetail
                        If itemDetail.ChangeTracker.State = ObjectState.Added Then
                            itemDetail.CreationUser = audit.CodeUser
                            itemDetail.CreationDate = DateTime.Now
                        Else
                            itemDetail.ModificationUser = audit.CodeUser
                            itemDetail.ModificationDate = DateTime.Now
                        End If
                    Next
                End If

                Me._accountPayableTransferRepository.SaveEntity(AccountPayableTransfer)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableTransfer)(AccountPayableTransfer, audit, status, auxAccountPayableTransfer)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                AccountPayableTransfer.MarkAsUnchanged()
                Transaction.Complete()
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = True, .ObjectEmbbeded = AccountPayableTransfer}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Anula un traslado de factura
    ''' </summary>
    ''' <param name="AccountPayableTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AnnularAccountPayableTransfer(AccountPayableTransfer As AccountPayableTransfer, audit As AuditMessage) As ActionResult(Of AccountPayableTransfer) Implements IAccountPayableTransferAdminService.AnnularAccountPayableTransfer
        If AccountPayableTransfer Is Nothing Then
            Throw New ArgumentNullException("AccountPayableTransfer")
        End If

        Dim unitOfWork As IUnitWork = Me._accountPayableTransferRepository.UnitWork
        Dim _accountPayableTransferAnnular As AccountPayableTransfer = _accountPayableTransferRepository.GetAccountPayableTransfer(AccountPayableTransfer.Code)
        If _accountPayableTransferAnnular.Id = 0 Then
            Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {"No existe el traslado de factura."}.ToList}
        End If

        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                If _accountPayableTransferAnnular.AccountPayableTransferDetail Is Nothing OrElse _accountPayableTransferAnnular.AccountPayableTransferDetail.Count = 0 Then
                    unitOfWork.RollbackChanges()
                    Transaction.Dispose()
                    Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {"El traslado de factura no tiene detalles para anular."}.ToList}
                End If

                Dim auxAccountPayableTransfer As AccountPayableTransfer = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableTransfer)
                Dim status As Integer

                auxAccountPayableTransfer = _accountPayableTransferAnnular.OriginalValue
                _accountPayableTransferAnnular.Status = 3
                _accountPayableTransferAnnular.ModificationUser = audit.CodeUser
                _accountPayableTransferAnnular.ModificationDate = DateTime.Now
                _accountPayableTransferAnnular.AnnulmentUser = audit.CodeUser
                _accountPayableTransferAnnular.AnnulmentDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Annular
                For Each itemDetail As AccountPayableTransferDetail In _accountPayableTransferAnnular.AccountPayableTransferDetail
                    itemDetail.Status = 4
                Next

                Me._accountPayableTransferRepository.SaveEntity(_accountPayableTransferAnnular)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableTransfer)(_accountPayableTransferAnnular, audit, status, auxAccountPayableTransfer)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                _accountPayableTransferAnnular.MarkAsUnchanged()

                Transaction.Complete()
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = True, .ObjectEmbbeded = _accountPayableTransferAnnular}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AccountPayableTransfer) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Funcion para Aceptar o rechazar los traslados
    ''' </summary>
    ''' <param name="ListIDDetailTranfer">Lista de Id de detalle de traslado a actualizar</param>
    ''' <param name="IDTarget">Id de unidad de radicacion de Destino</param>
    ''' <param name="RejectionReasonID">Id del motivo de rechazo</param>
    ''' <param name="RejectionDescription">Descipcion del rechazo</param>
    ''' <param name="audit">auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAcceptanceTranfer(ListIDDetailTranfer As List(Of Integer), IDTarget As Integer, RejectionReasonID As Integer?, RejectionDescription As String, audit As AuditMessage) As ActionResult Implements IAccountPayableTransferAdminService.SaveAcceptanceTranfer
        If ListIDDetailTranfer.Count <= 0 Then
            Throw New ArgumentNullException("ListIDDetailTranfer Lista vacia")
        End If
        Dim unitOfWork As IUnitWork = Me._accountPayableTransferRepository.UnitWork
        Dim unitOfWorkDetail As IUnitWork = Me._AccountPayableTransferDetailRepository.UnitWork
        Dim auxAccountPayableTransferDetail As AccountPayableTransferDetail = Nothing
        Dim auditProcess As IndigoAuditSimpleEntity(Of AccountPayableTransferDetail)
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim ListAccountPayableTransferDetail As List(Of AccountPayableTransferDetail) = _AccountPayableTransferDetailRepository.GetListAccountPayableTransferDetailByAccountPayableId(ListIDDetailTranfer)
                Dim datetmpNow As Date = Date.Now
                If RejectionReasonID IsNot Nothing Then
                    For Each item As AccountPayableTransferDetail In ListAccountPayableTransferDetail
                        Dim auxAccountPayableTransfer As AccountPayableTransferDetail = item
                        item.Status = 3 ' rechazada
                        item.RejectionDate = datetmpNow
                        item.RejectionDescription = RejectionDescription
                        item.RejectionReasonId = RejectionReasonID
                        item.RejectionUser = audit.CodeUser
                        item.ModificationDate = datetmpNow
                        item.ModificationUser = audit.CodeUser
                        'item.AccountPayable.FilingUnitId = IDTarget
                        _AccountPayableTransferDetailRepository.SaveEntity(item)
                        auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableTransferDetail)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxAccountPayableTransfer)
                        auditProcess.Execute()
                    Next
                Else
                    For Each item As AccountPayableTransferDetail In ListAccountPayableTransferDetail
                        Dim auxAccountPayableTransfer As AccountPayableTransferDetail = item
                        item.Status = 2 ' Aceptada
                        item.AcceptanceDate = datetmpNow
                        item.AcceptanceUser = audit.CodeUser
                        item.ModificationDate = datetmpNow
                        item.ModificationUser = audit.CodeUser
                        If item.AccountPayable IsNot Nothing Then
                            item.AccountPayable.FilingUnitId = IDTarget
                            item.AccountPayable.MarkAsModified()
                        Else
                            item.Refunds.FilingUnitId = IDTarget
                            item.Refunds.MarkAsModified()
                        End If
                        _AccountPayableTransferDetailRepository.SaveEntity(item)
                        auditProcess = New IndigoAuditSimpleEntity(Of AccountPayableTransferDetail)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Update, auxAccountPayableTransfer)
                        auditProcess.Execute()
                    Next
                End If
                unitOfWorkDetail.Commit()
                Dim Confirmtmp As Boolean = _AccountPayableTransferDetailRepository.ValidateConfirmEvaluation(ListAccountPayableTransferDetail(0).AccountPayableTransferId)
                If Confirmtmp = True Then
                    Dim ObjAccountPayableTransfer As AccountPayableTransfer = _accountPayableTransferRepository.GetAccountPayableTransferById(ListAccountPayableTransferDetail(0).AccountPayableTransferId, True)
                    ObjAccountPayableTransfer.Status = 4 ' 4 - Evaluado el oficio de traslado si ya no tiene item de detalle pendientes de aceptar
                    _accountPayableTransferRepository.SaveEntity(ObjAccountPayableTransfer)
                    unitOfWork.Commit()
                End If
                Transaction.Complete()


                Return New ActionResult With {.StateResult = True}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _secuenseDRepository = Nothing
            _accountPayableTransferRepository = Nothing
            _AccountPayableTransferDetailRepository = Nothing
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
