'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions

#End Region

Public Class AnnualizedCashFlowAdminService
    Implements IAnnualizedCashFlowAdminService


#Region "Fields"

    ''' <summary>
    ''' Repositorio de las entidades de categorias
    ''' </summary>
    Private _AnnualizedCashFlowRepository As IAnnualizedCashFlowRepository

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal AnnualizedCashFlowRepository As IAnnualizedCashFlowRepository)
        If AnnualizedCashFlowRepository Is Nothing Then
            Throw New ArgumentNullException("AnnualizedCashFlowRepository")
        End If
        Me._AnnualizedCashFlowRepository = AnnualizedCashFlowRepository
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    Public Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As List(Of AnnualizedCashFlow) Implements IAnnualizedCashFlowAdminService.GetAnnualizedCashFlowByValidityId
        Try
            Return _AnnualizedCashFlowRepository.GetAnnualizedCashFlowByValidityId(ValidityId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of AnnualizedCashFlow)
        End Try
    End Function
    ''' <summary>
    ''' Obtener los rubros del PAC inicial 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, CodeCategory As String, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of AnnualizedCashFlow)) Implements IAnnualizedCashFlowAdminService.GetAnnualizedCashFlowByValidityIdAndByCodeCategory
        Try
            Return _AnnualizedCashFlowRepository.GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId, CodeCategory, type)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza el PAC inicial
    ''' </summary>
    ''' <param name="ListAnnualizedCashFlow"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveListAnnualizedCashFlowHeader(ListAnnualizedCashFlow As List(Of AnnualizedCashFlow), state As Integer, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of AnnualizedCashFlow)) Implements IAnnualizedCashFlowAdminService.SaveListAnnualizedCashFlowHeader
        If ListAnnualizedCashFlow Is Nothing Then
            Throw New ArgumentNullException("ListAnnualizedCashFlow")
        End If
        Dim unitOfWork As IUnitWork = Me._AnnualizedCashFlowRepository.UnitWork
        Dim txtSettings = New TransactionOptions()
        txtSettings.Timeout = TransactionManager.MaximumTimeout
        txtSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txtSettings)
            Try
                Dim status As Integer

                If ListAnnualizedCashFlow IsNot Nothing AndAlso ListAnnualizedCashFlow.Count > 0 Then
                    ListAnnualizedCashFlow.ToList.ForEach(Sub(item)
                                                              item.TotalScheduled = item.InitialValue - item.DebitModificationValue + item.CreditModificationValue - item.DebitTransferValue + item.CreditTransferValue
                                                              item.Balance = item.TotalScheduled - item.ExecutedValue
                                                              item.Status = state
                                                              item.DocumentSource = type
                                                              If item.ChangeTracker.State = ObjectState.Added Then
                                                                  If item.Status = 1 Then
                                                                      item.CreationUser = audit.CodeUser
                                                                      item.CreationDate = DateTime.Now
                                                                  ElseIf item.Status = 2 Then
                                                                      item.CreationUser = audit.CodeUser
                                                                      item.CreationDate = DateTime.Now
                                                                      item.ConfirmationUser = audit.CodeUser
                                                                      item.ConfirmationDate = DateTime.Now
                                                                      status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                                                                  ElseIf item.Status = 3 Then
                                                                      item.CreationUser = audit.CodeUser
                                                                      item.CreationDate = DateTime.Now
                                                                      item.AnnulmentUser = audit.CodeUser
                                                                      item.AnnulmentDate = DateTime.Now
                                                                      status = Infrastructure.CrossCutting.Audit.Actions.Annular
                                                                  End If
                                                              ElseIf item.ChangeTracker.State = ObjectState.Modified Then
                                                                  If item.Status = 1 Then
                                                                      item.ModificationUser = audit.CodeUser
                                                                      item.ModificationDate = DateTime.Now
                                                                      status = Infrastructure.CrossCutting.Audit.Actions.Update
                                                                  ElseIf item.Status = 2 Then
                                                                      item.ModificationUser = audit.CodeUser
                                                                      item.ModificationDate = DateTime.Now
                                                                      item.ConfirmationUser = audit.CodeUser
                                                                      item.ConfirmationDate = DateTime.Now
                                                                      status = Infrastructure.CrossCutting.Audit.Actions.Confirm
                                                                  ElseIf item.Status = 3 Then
                                                                      item.ModificationUser = audit.CodeUser
                                                                      item.ModificationDate = DateTime.Now
                                                                      item.AnnulmentUser = audit.CodeUser
                                                                      item.AnnulmentDate = DateTime.Now
                                                                      status = Infrastructure.CrossCutting.Audit.Actions.Annular
                                                                  End If
                                                              End If
                                                              Me._AnnualizedCashFlowRepository.SaveEntity(item)
                                                          End Sub)
                End If



                unitOfWork.Commit()
                If ListAnnualizedCashFlow IsNot Nothing AndAlso ListAnnualizedCashFlow.Count > 0 Then
                    ListAnnualizedCashFlow.ToList.ForEach(Sub(item)
                                                              If item.ChangeTracker.State = ObjectState.Modified Then
                                                                  Dim auxAnnualizedCashFlow As AnnualizedCashFlow = Nothing
                                                                  Dim auditProcess As IndigoAuditSimpleEntity(Of AnnualizedCashFlow)
                                                                  auxAnnualizedCashFlow = _AnnualizedCashFlowRepository.GetAnnualizedCashFlowById(item.Id)
                                                                  auditProcess = New IndigoAuditSimpleEntity(Of AnnualizedCashFlow)(item, audit, status, auxAnnualizedCashFlow)
                                                                  auditProcess.Execute()
                                                              End If
                                                          End Sub)
                End If

                Transaction.Complete()
                Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = True, .ObjectEmbbeded = ListAnnualizedCashFlow}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
            Catch ex As InvalidOperationException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = False}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of AnnualizedCashFlow)) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _AnnualizedCashFlowRepository = Nothing
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
