'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
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
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class ObligationModificationDetailAdminService
    Implements IObligationModificationDetailAdminService

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _obligationModificationDetailRepository As IObligationModificationDetailRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseBudgetDRepository

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal obligationModificationDetailRepository As IObligationModificationDetailRepository, ByVal secuenseDRepository As ISequenseBudgetDRepository)
        If obligationModificationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("obligationModificationDetailRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _obligationModificationDetailRepository = obligationModificationDetailRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Elimina un detalle de una modificacion de obligacion
    ''' </summary>
    ''' <param name="ObligationModificationDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteObligationModificationDetail(ObligationModificationDetail As ObligationModificationDetail, audit As AuditMessage) As ActionResult Implements IObligationModificationDetailAdminService.DeleteObligationModificationDetail
        If ObligationModificationDetail Is Nothing Then
            Throw New ArgumentNullException("ObligationModificationDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._obligationModificationDetailRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of ObligationModificationDetail)
            auditProcess = New IndigoAuditSimpleEntity(Of ObligationModificationDetail)(ObligationModificationDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._obligationModificationDetailRepository.DeleteEntity(ObligationModificationDetail)
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
    ''' Obtiene un detalle de una modificacion de obligacion por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetObligationModificationDetailById(Id As Integer, audit As AuditMessage) As ActionResult(Of ObligationModificationDetail) Implements IObligationModificationDetailAdminService.GetObligationModificationDetailById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ObligationModificationDetail As ObligationModificationDetail = Me._obligationModificationDetailRepository.GetObligationModificationDetailById(Id)
            If ObligationModificationDetail IsNot Nothing AndAlso ObligationModificationDetail.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ObligationModificationDetail)(ObligationModificationDetail, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of ObligationModificationDetail) With {.StateResult = True, .ObjectEmbbeded = ObligationModificationDetail}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ObligationModificationDetail) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Guarda o actualiza un detalle de una modificacion de obligacion
    ''' </summary>
    ''' <param name="ObligationModificationDetail"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveObligationModificationDetail(ObligationModificationDetail As ObligationModificationDetail, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ObligationModificationDetail) Implements IObligationModificationDetailAdminService.SaveObligationModificationDetail
        If ObligationModificationDetail Is Nothing Then
            Throw New ArgumentNullException("ObligationModificationDetail")
        End If
        Dim unitOfWork As IUnitWork = Me._obligationModificationDetailRepository.UnitWork
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim auxObligationModificationDetail As ObligationModificationDetail = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of ObligationModificationDetail)
                Dim status As Integer

                Me._obligationModificationDetailRepository.SaveEntity(ObligationModificationDetail)
                unitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of ObligationModificationDetail)(ObligationModificationDetail, audit, status, auxObligationModificationDetail)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                ObligationModificationDetail.MarkAsUnchanged()

                Transaction.Complete()
                Return New ActionResult(Of ObligationModificationDetail) With {.StateResult = True, .ObjectEmbbeded = ObligationModificationDetail}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                Return New ActionResult(Of ObligationModificationDetail) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of ObligationModificationDetail) With {.StateResult = False, .StateResultAux = False, .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _obligationModificationDetailRepository = Nothing
            _secuenseDRepository = Nothing
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
