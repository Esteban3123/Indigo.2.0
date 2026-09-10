'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class PositionAdminService
    Implements IPositionAdminService

    'Repositorio de cargos
    Private _PositionRepository As IPositionRepository

    ''' <summary>
    ''' inicia el repositorio de cargos
    ''' </summary>
    ''' <param name="positionRepository">Repositorio de cargos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal positionRepository As IPositionRepository)
        If (positionRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de cargos vacio")
        End If
        _PositionRepository = positionRepository
    End Sub

    ''' <summary>
    ''' Elimina un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Public Function DeletePosition(position As Position, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of Position) Implements IPositionAdminService.DeletePosition
        Dim result As New ActionMessageResult(Of Position)
        result.StateResult = True
        If position Is Nothing Then
            Throw New ArgumentNullException("Cargo Vacio")
        End If
        Dim unitWork As IUnitWork = _PositionRepository.UnitWork
        Try
            _PositionRepository.DeleteEntity(position)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(position.GetType.Name, audit.Functional, position.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Position)(position, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", position.Code))
            Return result
            unitWork.RollbackChanges()
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un cargo
    ''' </summary>
    ''' <param name="code">Código del cargo</param>
    ''' <returns>Cargo</returns>
    Public Function GetPosition(code As String) As Position Implements IPositionAdminService.GetPosition
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try
            Return _PositionRepository.GetPosition(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Position()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los cargos
    ''' </summary>
    ''' <returns>Lista de cargos</returns>
    Public Function ListAllPosition() As List(Of Position) Implements IPositionAdminService.ListAllPosition
        Try
            Return _PositionRepository.ListAllPosition()
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un cargo
    ''' </summary>
    ''' <param name="position">Cargo</param>
    ''' <returns></returns>
    Public Function SavePosition(position As Position, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IPositionAdminService.SavePosition
        If position Is Nothing Then
            Throw New ArgumentNullException("Cargo Vacio")
        End If
        Dim unitWork As IUnitWork = _PositionRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of Position)
            Dim auxPosition As Position = Nothing
            Dim status As Integer

            If position.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                position.ModificationUser = audit.CodeUser
                position.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxPosition = _PositionRepository.GetPosition(position.Code, False)
            Else
                position.CreationUser = audit.CodeUser
                position.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _PositionRepository.SaveEntity(position)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Position)(position, audit, status, auxPosition)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As Boolean Implements IPositionAdminService.ChangeState
        Dim Position As Position = _PositionRepository.GetPosition(code)
        Position.State = state
        Position.ModificationDate = Date.Now
        Position.ModificationUser = audit.CodeUser
        Position.MarkAsModified()
        Return SavePosition(Position, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _PositionRepository = Nothing
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
