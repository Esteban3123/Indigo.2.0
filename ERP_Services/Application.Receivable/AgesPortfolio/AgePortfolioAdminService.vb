'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
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

Public Class AgePortfolioAdminService
    Implements IAgePortfolioAdminService


#Region "Fields"

    ''' <summary>
    ''' Repositorio de edades de cartera
    ''' </summary>
    Private _agePortfolioRepository As IAgePortfolioRepository

    ''' <summary>
    ''' repositorio de las secuencias
    ''' </summary>
    Private _sequensePortfolioDRepository As ISequensePortfolioDRepository

#End Region

#Region "Methods"

    Public Sub New(ByVal agePortfolioRepository As IAgePortfolioRepository, ByVal sequensePortfolioDRepository As ISequensePortfolioDRepository)
        If agePortfolioRepository Is Nothing Then
            Throw New ArgumentNullException("agePortfolioRepository")
        End If
        If sequensePortfolioDRepository Is Nothing Then
            Throw New ArgumentNullException("sequensePortfolioDRepository")
        End If
        _agePortfolioRepository = agePortfolioRepository
        _sequensePortfolioDRepository = sequensePortfolioDRepository
    End Sub

    ''' <summary>
    ''' Elimina una edad de cartera
    ''' </summary>
    ''' <param name="agesPortfolio"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">agesPortfolio</exception>
    Public Function DeleteAgesPortfolio(agesPortfolio As AgesPortfolio, audit As AuditMessage) As ActionResult Implements IAgePortfolioAdminService.DeleteAgesPortfolio
        If agesPortfolio Is Nothing Then
            Throw New ArgumentNullException("agesPortfolio")
        End If
        Dim unitOfWork As IUnitWork = Me._agePortfolioRepository.UnitWork
        Try
            agesPortfolio.ModificationDate = DateTime.Now
            agesPortfolio.ModificationUser = audit.CodeUser
            Dim auditProcess As New IndigoAuditSimpleEntity(Of AgesPortfolio)(agesPortfolio, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._agePortfolioRepository.DeleteEntity(agesPortfolio)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}

        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"}), .Message = ResourceManager.GetString("ErrorConcurrence")}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"}), .Message = ResourceManager.GetString("ErrorUnknown")}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPortfolio() As ActionResult(Of List(Of AgesPortfolio)) Implements IAgePortfolioAdminService.ListAgesPortfolio
        Try
            Dim portfolio As List(Of AgesPortfolio) = _agePortfolioRepository.ListAgesPortfolio()
            Return New ActionResult(Of List(Of AgesPortfolio)) With {.StateResult = True, .ObjectEmbbeded = portfolio}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of AgesPortfolio)) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' lista las edades de cartera por unidad operativa
    ''' </summary>
    ''' <param name="idSetingPortfolio"></param>
    ''' <returns></returns>
    Public Function ListAgesPortfolioByIdSettingPortfolio(idSetingPortfolio As Integer) As List(Of AgesPortfolio) Implements IAgePortfolioAdminService.ListAgesPortfolioByIdSettingPortfolio
        Return _agePortfolioRepository.ListAgesPortfolioByIdSettingPortfolio(idSetingPortfolio)
    End Function

    ''' <summary>
    ''' Guarda una edad de cartera
    ''' </summary>
    ''' <param name="agesPortfolio"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">agesPortfolio</exception>
    Public Function SaveAgesPortfolio(agesPortfolio As AgesPortfolio, audit As AuditMessage) As ActionResult(Of AgesPortfolio) Implements IAgePortfolioAdminService.SaveAgesPortfolio
        If agesPortfolio Is Nothing Then
            Throw New ArgumentNullException("agesPortfolio")
        End If

        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Dim unitOfWork As IUnitWork = Me._agePortfolioRepository.UnitWork
            Dim unitWorkSequence As IUnitWork = Me._sequensePortfolioDRepository.UnitWork

            Try
                Dim auditProcess As IndigoAuditSimpleEntity(Of AgesPortfolio)
                Dim status As Integer
                Dim auxAgesPortfolio As AgesPortfolio = Nothing
                If agesPortfolio.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    agesPortfolio.CreationUser = audit.CodeUser
                    agesPortfolio.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    agesPortfolio.ModificationUser = audit.CodeUser
                    agesPortfolio.ModificationDate = DateTime.Now
                    auxAgesPortfolio = agesPortfolio.OriginalValue
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._agePortfolioRepository.SaveEntity(agesPortfolio)
                unitOfWork.Commit()
                unitWorkSequence.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of AgesPortfolio)(agesPortfolio, audit, status, auxAgesPortfolio)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                agesPortfolio.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of AgesPortfolio) With {.StateResult = True, .ObjectEmbbeded = agesPortfolio}
            Catch ex As OptimisticConcurrencyException
                unitOfWork.RollbackChanges()
                scope.Dispose()
                Return New ActionResult(Of AgesPortfolio) With {.StateResult = False, .MessageResult = {"-999"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of AgesPortfolio) With {.StateResult = False, .MessageResult = {"-000"}.ToList(), .Message = ResourceManager.GetString("ErrorUnknown")}
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
            _agePortfolioRepository = Nothing
            _sequensePortfolioDRepository = Nothing
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
