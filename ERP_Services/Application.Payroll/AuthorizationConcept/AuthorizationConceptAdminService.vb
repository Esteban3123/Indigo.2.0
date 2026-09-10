'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 09-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

Public Class AuthorizationConceptAdminService
    Implements IAuthorizationConceptAdminService

    ''' <summary>
    ''' Repositorio de autorizacion de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _authorizationConceptRepository As IAuthorizationConceptRepository

    ''' <summary>
    ''' contructor el cual creo una instancia del repositorio
    ''' </summary>
    ''' <param name="authorizationConceptRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal authorizationConceptRepository As IAuthorizationConceptRepository)
        If authorizationConceptRepository Is Nothing Then
            Throw New ArgumentNullException("authorizationConceptRepository Vacio")
        End If
        _authorizationConceptRepository = authorizationConceptRepository
    End Sub

    ''' <summary>
    ''' Elimina una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Autorizacion del concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    Public Function DeleteAuthorizationConcept(authorizationConcept As AuthorizationConcept, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of AuthorizationConcept) Implements IAuthorizationConceptAdminService.DeleteAuthorizationConcept
        Dim result As New ActionMessageResult(Of AuthorizationConcept)
        result.StateResult = True
        If authorizationConcept Is Nothing Then
            Throw New ArgumentNullException("Autorizacion de concepto es vacio")
        End If
        Dim unitWork As IUnitWork = _authorizationConceptRepository.UnitWork
        Try
            _authorizationConceptRepository.DeleteEntity(authorizationConcept)
            unitWork.Commit()
            'IndigoAuditSimpleEntity(Of AuthorizationConcept).Execute(authorizationConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, authorizationConcept)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", authorizationConcept.ConceptId))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un empleado
    ''' </summary>
    ''' <param name="employeeId">Codigo del empleado</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Public Function GetAuthorizationConceptByEmployeeId(employeeId As Integer) As List(Of AuthorizationConcept) Implements IAuthorizationConceptAdminService.GetAuthorizationConceptByEmployeeId
        Try
            Return _authorizationConceptRepository.GetAuthorizationConceptByEmployeeId(employeeId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene las autorizaciones de concepto que tenga un grupo
    ''' </summary>
    ''' <param name="groupId">Codigo del Grupo</param>
    ''' <returns>Lista de AuthorizationConcept</returns>
    ''' <remarks></remarks>
    Public Function GetAuthorizationConceptByGroupId(groupId As Integer) As List(Of AuthorizationConcept) Implements IAuthorizationConceptAdminService.GetAuthorizationConceptByGroupId
        Try
            Return _authorizationConceptRepository.GetAuthorizationConceptByGroupId(groupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todos las autorizaciones de concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAuthorizationConcept() As List(Of AuthorizationConcept) Implements IAuthorizationConceptAdminService.ListAllAuthorizationConcept
        Try
            Return _authorizationConceptRepository.ListAllAuthorizationConcept()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza una autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">authorizationConcept</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAuthorizationConcept(authorizationConcept As AuthorizationConcept, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IAuthorizationConceptAdminService.SaveAuthorizationConcept
        If authorizationConcept Is Nothing Then
            Throw New ArgumentNullException("Autorizacion de concepto vacio")
        End If
        Dim unitWork As IUnitWork = _authorizationConceptRepository.UnitWork
        Try
            _authorizationConceptRepository.SaveEntity(authorizationConcept)
            unitWork.Commit()
            'If authorizationConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            'IndigoAuditSimpleEntity(Of AuthorizationConcept).Execute(authorizationConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, authorizationConcept)
            'ElseIf authorizationConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            'IndigoAuditSimpleEntity(Of AuthorizationConcept).Execute(authorizationConcept, audit, Infrastructure.CrossCutting.Audit.Actions.Update, authorizationConcept)
            'End If
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza una lista de autorizacion de concepto
    ''' </summary>
    ''' <param name="authorizationConcept">Lista de autorizacion de concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveListAuthorizationConcept(authorizationConcept As List(Of AuthorizationConcept), audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IAuthorizationConceptAdminService.SaveListAuthorizationConcept
        If authorizationConcept Is Nothing Then
            Throw New ArgumentNullException("Lista de autorizaciones vacia")
        End If
        Dim uniWork As IUnitWork = _authorizationConceptRepository.UnitWork
        Try
            For Each item As AuthorizationConcept In authorizationConcept
                _authorizationConceptRepository.SaveEntity(item)
            Next
            uniWork.Commit()
            Return True
        Catch ex As Exception
            uniWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _authorizationConceptRepository = Nothing
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
