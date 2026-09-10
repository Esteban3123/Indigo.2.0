#Region "Imports"
Imports Domain.Base
Imports Infrastructure.CrossCutting.Base
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Data.Entity.Core
Imports System.Text
Imports System.Transactions

#End Region

Public Class FixedAssetPurchaseOrderAdminService

    Implements IFixedAssetPurchaseOrderAdminService

    'Repositorio de la aseguradora
    Private _PurchaseOrderRepository As IFixedAssetPurchaseOrderRepository

    'repositorio de la secuencia
    Private _sequenceRepository As IFixedAssetSequenceDetailRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="PurchaseOrderRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal PurchaseOrderRepository As IFixedAssetPurchaseOrderRepository, sequenceRepository As IFixedAssetSequenceDetailRepository)
        If (PurchaseOrderRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio PurchaseOrderRepository vacio")
        End If
        _sequenceRepository = sequenceRepository
        _PurchaseOrderRepository = PurchaseOrderRepository
    End Sub

    Public Function DeleteFixedAssetPurchaseOrder(FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage) As Boolean Implements IFixedAssetPurchaseOrderAdminService.DeleteFixedAssetPurchaseOrder
        If FixedAssetPurchaseOrder Is Nothing Then
            Throw New ArgumentNullException("InputRemission vacio")
        End If
        Dim unitWork As IUnitWork = _PurchaseOrderRepository.UnitWork
        'Dim unitWorkAdministrative As IUnitWork = _AdministrativeRepository.UnitWork
        'Dim unitWorkAsistential As IUnitWork = _AsistentialRepository.UnitWork
        Try

            FixedAssetPurchaseOrder.MarkAsDeleted()

            _PurchaseOrderRepository.DeleteEntity(FixedAssetPurchaseOrder)
            unitWork.Commit()
            Dim auditObject As New IndigoAuditSimpleEntity(Of FixedAssetPurchaseOrder)(FixedAssetPurchaseOrder, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            'unitWorkAdministrative.RollbackChanges()
            'unitWorkAsistential.RollbackChanges()
            IndigoManagementExceptions.HandleExceptionUI(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    Public Function GetFixedAssetPurchaseOrderByCode(code As String) As ActionResult(Of FixedAssetPurchaseOrder) Implements IFixedAssetPurchaseOrderAdminService.GetFixedAssetPurchaseOrderByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Try
            Dim FixedAssetPurchaseOrder As FixedAssetPurchaseOrder = Me._PurchaseOrderRepository.GetFixedAssetPurchaseOrderByCode(code.Trim())
            Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = True, .ObjectEmbbeded = FixedAssetPurchaseOrder}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    Public Function SaveFixedAssetPurchaseOrder(FixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetPurchaseOrder) Implements IFixedAssetPurchaseOrderAdminService.SaveFixedAssetPurchaseOrder
        If FixedAssetPurchaseOrder Is Nothing Then
            Throw New ArgumentNullException("FixedAssetPurchaseOrder")
        End If
        Dim unitOfWork As IUnitWork = Me._PurchaseOrderRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._sequenceRepository.UnitWork
        Dim parameter As New StringBuilder()

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim seq As FixedAssetSequenceDetail = Nothing
                If FixedAssetPurchaseOrder.Code Is Nothing OrElse FixedAssetPurchaseOrder.Code.Trim().Equals(String.Empty) Then
                    seq = Me._sequenceRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.FixedAssetSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            FixedAssetPurchaseOrder.Code = res
                            seq.Next += 1
                            Me._sequenceRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                        End If
                    Else
                        Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                    End If
                End If

                Dim auxFixedAssetPurchaseOrder As FixedAssetPurchaseOrder = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of FixedAssetPurchaseOrder)
                Dim status As Integer

                If FixedAssetPurchaseOrder.ChangeTracker.State = ObjectState.Added Then
                    FixedAssetPurchaseOrder.CreationUser = audit.CodeUser
                    FixedAssetPurchaseOrder.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                ElseIf FixedAssetPurchaseOrder.ChangeTracker.State = ObjectState.Modified Then
                    auxFixedAssetPurchaseOrder = FixedAssetPurchaseOrder.OriginalValue
                    FixedAssetPurchaseOrder.ModificationUser = audit.CodeUser
                    FixedAssetPurchaseOrder.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                If FixedAssetPurchaseOrder.Status = 2 Then
                    FixedAssetPurchaseOrder.ModificationUser = audit.CodeUser
                    FixedAssetPurchaseOrder.ModificationDate = DateTime.Now
                    FixedAssetPurchaseOrder.ConfirmationUser = audit.CodeUser
                    FixedAssetPurchaseOrder.ConfirmationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                End If
                If FixedAssetPurchaseOrder.Status = 3 Then
                    FixedAssetPurchaseOrder.ModificationUser = audit.CodeUser
                    FixedAssetPurchaseOrder.ModificationDate = DateTime.Now
                    FixedAssetPurchaseOrder.AnnulmentUser = audit.CodeUser
                    FixedAssetPurchaseOrder.AnnulmentDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Annular
                End If

                Me._PurchaseOrderRepository.SaveEntity(FixedAssetPurchaseOrder)
                unitOfWork.Commit()

                Dim messageReturn As String = ""

                If FixedAssetPurchaseOrder.Status = 2 Or FixedAssetPurchaseOrder.IsDesconfirmed Then

                    If FixedAssetPurchaseOrder.Status = 2 Then
                        parameter.Append("<PurchaseOrden>")
                        parameter.Append(String.Format("<{0}>{1}</{0}>", "Id", FixedAssetPurchaseOrder.Id))
                        parameter.Append(String.Format("<{0}>{1}</{0}>", "Status", FixedAssetPurchaseOrder.Status))
                        parameter.Append(String.Format("<{0}>{1}</{0}>", "BudgetaryValidityId", FixedAssetPurchaseOrder.BudgetaryValidityId))
                        parameter.Append("</PurchaseOrden>")
                    ElseIf FixedAssetPurchaseOrder.IsDesconfirmed Then
                        parameter.Append("<PurchaseOrden>")
                        parameter.Append(String.Format("<{0}>{1}</{0}>", "Id", FixedAssetPurchaseOrder.Id))
                        parameter.Append(String.Format("<{0}>{1}</{0}>", "Status", 3))
                        parameter.Append("</PurchaseOrden>")
                    End If


                    Dim resultConfirmPurchaseOrder As SP_ConfirmPurchaseOrderFixedAsset_Result = _PurchaseOrderRepository.ConfirmPurchaseOrder(parameter.ToString)
                        If resultConfirmPurchaseOrder.Status <> 1 Then
                            Transaction.Dispose()
                            unitOfWork.RollbackChanges()
                            Dim messages As List(Of String) = New List(Of String)
                            messages.Add(resultConfirmPurchaseOrder.Message)
                            Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .StateResultAux = False, .MessageResult = messages}
                        End If
                        messageReturn = resultConfirmPurchaseOrder.Message
                    End If

                    sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of FixedAssetPurchaseOrder)(FixedAssetPurchaseOrder, audit, status, auxFixedAssetPurchaseOrder)
                auditProcess.Execute()

                Transaction.Complete()

                'Se marca la entidad como sin cambios
                FixedAssetPurchaseOrder.MarkAsUnchanged()
                Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = True, .ObjectEmbbeded = FixedAssetPurchaseOrder, .Message = messageReturn}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                unitOfWork.RollbackChanges()
                Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As Exception
                Transaction.Dispose()
                unitOfWork.RollbackChanges()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks><code>HRR PBI4547</code></remarks>
    Public Function ListPurchaseRequestToOrder() As List(Of Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result) Implements IFixedAssetPurchaseOrderAdminService.ListPurchaseRequestToOrder
        Try
            Return _PurchaseOrderRepository.ListPurchaseRequestToOrder
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Domain.Entities.SP_PurchaseRequestToOrderFixedAsset_Result)
        End Try
    End Function

    ''' <summary>
    ''' Desconfirma la orden de compra
    ''' </summary>
    ''' <param name="FixedAssetPurchaseOrder"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UnconfirmPurchaseOrder(fixedAssetPurchaseOrder As FixedAssetPurchaseOrder, audit As AuditMessage) As ActionResult(Of FixedAssetPurchaseOrder) Implements IFixedAssetPurchaseOrderAdminService.UnconfirmPurchaseOrder
        If fixedAssetPurchaseOrder Is Nothing Then
            Throw New ArgumentNullException("FixedAssetPurchaseOrder")
        End If
        Try
            fixedAssetPurchaseOrder.Status = 1
            fixedAssetPurchaseOrder.IsDesconfirmed = True
            Dim unconfirmResult As ActionResult(Of FixedAssetPurchaseOrder) = SaveFixedAssetPurchaseOrder(fixedAssetPurchaseOrder, audit)

            If unconfirmResult.StateResult = True Then
                Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = True, .Message = "El documento se desconfirmó con éxito", .ObjectEmbbeded = unconfirmResult.ObjectEmbbeded}
            Else
                Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = True, .Message = If(unconfirmResult.MessageResult IsNot Nothing AndAlso unconfirmResult.MessageResult.Any(), unconfirmResult.MessageResult(0), unconfirmResult.Message)}
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of FixedAssetPurchaseOrder) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _sequenceRepository = Nothing
            _PurchaseOrderRepository = Nothing
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
