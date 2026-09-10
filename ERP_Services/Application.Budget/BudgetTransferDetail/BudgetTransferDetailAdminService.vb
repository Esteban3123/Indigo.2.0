'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Transactions

#End Region


Public Class BudgetTransferDetailAdminService
    Implements IBudgetTransferDetailAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetTransferDetailRepository As IBudgetTransferDetailRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal budgetTransferDetailRepository As IBudgetTransferDetailRepository)
        If budgetTransferDetailRepository Is Nothing Then
            Throw New ArgumentNullException("budgetTransferDetailRepository Vacio")
        End If
        _budgetTransferDetailRepository = budgetTransferDetailRepository
    End Sub

    ''' <summary>
    ''' Elimina el detalle del traslado
    ''' </summary>
    ''' <param name="BudgetTransferDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteBudgetTransferDetail(BudgetTransferDetail As BudgetTransferDetail, audit As AuditMessage) As ActionResult Implements IBudgetTransferDetailAdminService.DeleteBudgetTransferDetail
        If BudgetTransferDetail Is Nothing Then
            Throw New ArgumentNullException("BudgetTransferDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._budgetTransferDetailRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetTransferDetail)
            auditProcess = New IndigoAuditSimpleEntity(Of BudgetTransferDetail)(BudgetTransferDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._budgetTransferDetailRepository.DeleteEntity(BudgetTransferDetail)
            unitOfWork.Commit()
            auditProcess.Execute()
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
    ''' Obtiene el detalle del traslado por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBudgetTransferDetailById(Id As Integer, audit As AuditMessage) As ActionResult(Of BudgetTransferDetail) Implements IBudgetTransferDetailAdminService.GetBudgetTransferDetailById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim BudgetTransferDetail As BudgetTransferDetail = Me._budgetTransferDetailRepository.GetBudgetTransferDetailById(Id)
            If BudgetTransferDetail IsNot Nothing AndAlso BudgetTransferDetail.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of BudgetTransferDetail)(BudgetTransferDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of BudgetTransferDetail) With {.StateResult = True, .ObjectEmbbeded = BudgetTransferDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetTransferDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza el detalle del traslado
    ''' </summary>
    ''' <param name="BudgetTransferDetail"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveBudgetTransferDetail(BudgetTransferDetail As BudgetTransferDetail, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BudgetTransferDetail) Implements IBudgetTransferDetailAdminService.SaveBudgetTransferDetail
        If BudgetTransferDetail Is Nothing Then
            Throw New ArgumentNullException("BudgetTransferDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._budgetTransferDetailRepository.UnitWork
        Try
            Dim auxBudget As BudgetTransferDetail = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of BudgetTransferDetail)
            Dim status As Integer

            Me._budgetTransferDetailRepository.SaveEntity(BudgetTransferDetail)
            unitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of BudgetTransferDetail)(BudgetTransferDetail, audit, status, auxBudget)
            auditProcess.Execute()

            'Se marca la entidad como sin cambios
            BudgetTransferDetail.MarkAsUnchanged()

            Return New ActionResult(Of BudgetTransferDetail) With {.StateResult = True, .ObjectEmbbeded = BudgetTransferDetail}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of BudgetTransferDetail) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of BudgetTransferDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _budgetTransferDetailRepository = Nothing
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
