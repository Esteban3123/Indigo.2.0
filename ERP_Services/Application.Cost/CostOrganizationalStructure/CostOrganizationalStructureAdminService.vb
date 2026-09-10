'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries"
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

#End Region

Public Class CostOrganizationalStructureAdminService
    Implements ICostOrganizationalStructureAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de estructura organizacional
    ''' </summary>
    Private _organizationalStructureRepository As ICostOrganizationalStructureRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal organizationalStructureRepository As ICostOrganizationalStructureRepository)
        If organizationalStructureRepository Is Nothing Then
            Throw New ArgumentNullException("organizationalStructureRepository")
        End If
        _organizationalStructureRepository = organizationalStructureRepository
    End Sub

#End Region
    ''' <summary>
    ''' Elimina una estructura Organizacional
    ''' </summary>
    ''' <param name="organizationalStructure"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">organizationalStructure</exception>
    Public Function DeleteCostOrganizationalStructure(organizationalStructure As CostOrganizationalStructureOfCosts, audit As AuditMessage) As ActionResult Implements ICostOrganizationalStructureAdminService.DeleteCostOrganizationalStructure
        If organizationalStructure Is Nothing Then
            Throw New ArgumentNullException("organizationalStructure")
        End If
        Dim unitOfWork As IUnitWork = Me._organizationalStructureRepository.UnitWork
        Try
            organizationalStructure.ModificationUser = audit.CodeUser
            organizationalStructure.ModificationDate = Date.Now
            Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Delete
            Dim auditProcess As New IndigoAuditSimpleEntity(Of CostOrganizationalStructureOfCosts)(organizationalStructure, audit, status)

            Me._organizationalStructureRepository.DeleteEntity(organizationalStructure)
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
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    Public Function GetCostOrganizationalStructure(code As String, audit As AuditMessage) As ActionResult(Of CostOrganizationalStructureOfCosts) Implements ICostOrganizationalStructureAdminService.GetCostOrganizationalStructure
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim refund As CostOrganizationalStructureOfCosts = Me._organizationalStructureRepository.GetCostOrganizationalStructure(code.Trim())
            If refund IsNot Nothing AndAlso refund.Id > 0 Then
                Dim status As Integer = Infrastructure.CrossCutting.Audit.Actions.Print
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostOrganizationalStructureOfCosts)(refund, audit, status)
                auditProcess.Execute()
            End If
            Return New ActionResult(Of CostOrganizationalStructureOfCosts) With {.StateResult = True, .ObjectEmbbeded = refund}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostOrganizationalStructureOfCosts) With {.StateResult = False}
        End Try
    End Function
    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCostOrganizationalStructureById(id As Integer) As CostOrganizationalStructureOfCosts Implements ICostOrganizationalStructureAdminService.GetCostOrganizationalStructureById
        Try
            Return _organizationalStructureRepository.GetCostOrganizationalStructureById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Guarda una estructura Organizacional
    ''' </summary>
    ''' <param name="costOrganizationalStructure"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">organizationalStructure</exception>
    Public Function SaveCostOrganizationalStructure(costOrganizationalStructure As CostOrganizationalStructureOfCosts, audit As AuditMessage) As ActionResult(Of CostOrganizationalStructureOfCosts) Implements ICostOrganizationalStructureAdminService.SaveCostOrganizationalStructure
        If costOrganizationalStructure Is Nothing Then
            Throw New ArgumentNullException("organizationalStructure")
        End If
        Using scope As New TransactionScope()
            Dim unitOfWork As IUnitWork = Me._organizationalStructureRepository.UnitWork

            Try

                Dim auxRefund As CostOrganizationalStructureOfCosts = Nothing
                Dim status As Integer
                If costOrganizationalStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    costOrganizationalStructure.CreationDate = Date.Now
                    costOrganizationalStructure.CreationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    costOrganizationalStructure.ModificationDate = Date.Now
                    costOrganizationalStructure.ModificationUser = audit.CodeUser
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                    auxRefund = costOrganizationalStructure.OriginalValue
                End If

                Me._organizationalStructureRepository.SaveEntity(costOrganizationalStructure)
                unitOfWork.Commit()

                scope.Complete()
                Dim auditProcess As New IndigoAuditSimpleEntity(Of CostOrganizationalStructureOfCosts)(costOrganizationalStructure, audit, status, auxRefund)
                auditProcess.Execute()
                Return New ActionResult(Of CostOrganizationalStructureOfCosts) With {.StateResult = True, .ObjectEmbbeded = costOrganizationalStructure}

            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of CostOrganizationalStructureOfCosts) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorConcurrence")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of CostOrganizationalStructureOfCosts) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
            End Try
        End Using
    End Function
    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    Public Function UpdateState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CostOrganizationalStructureOfCosts) Implements ICostOrganizationalStructureAdminService.UpdateState
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If String.IsNullOrEmpty(state) Then
            Throw New ArgumentNullException("state")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If

        Try
            Dim organizationalStructureOfCosts As CostOrganizationalStructureOfCosts = Me._organizationalStructureRepository.GetCostOrganizationalStructure(code.Trim())
            If organizationalStructureOfCosts IsNot Nothing AndAlso organizationalStructureOfCosts.Id > 0 Then
                organizationalStructureOfCosts.Status = state
            End If
            Return Me.SaveCostOrganizationalStructure(organizationalStructureOfCosts, audit)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CostOrganizationalStructureOfCosts) With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _organizationalStructureRepository = Nothing
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