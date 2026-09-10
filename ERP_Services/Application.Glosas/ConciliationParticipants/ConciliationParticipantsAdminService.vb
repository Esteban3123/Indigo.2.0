'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz
' Created          : 23-05-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Common.Entities
Imports Application.Base
Imports System.Data.Entity.Core

#End Region

''' <summary>
''' Servicio de Participantes Conciliación.
''' </summary>
''' <remarks></remarks>
Public Class ConciliationParticipantsAdminService
    Implements IConciliationParticipantsAdminService


    Private _ConciliationParticipantsRepository As IConciliationParticipantsRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="ConciliationParticipantsAdminService" />.
    ''' </summary>
    ''' <param name="ConciliationParticipantsRepository">El repositorio para el manejo de las compañias.</param>
    Public Sub New(ByVal ConciliationParticipantsRepository As IConciliationParticipantsRepository)
        If ConciliationParticipantsRepository Is Nothing Then
            Throw New ArgumentNullException("Participantes Conciliación Vacío")
        End If
        _ConciliationParticipantsRepository = ConciliationParticipantsRepository
    End Sub

    ''' <summary>
    ''' Borrar un participante de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliación</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function DeleteConciliationParticipants(ConciliacionParticipante As ConciliationParticipants, audit As AuditMessage) As ActionResult Implements IConciliationParticipantsAdminService.DeleteConciliationParticipants
        If ConciliacionParticipante Is Nothing Then
            Throw New ArgumentNullException("Participante Conciliación Vacío")
        End If
        Dim unitOfWork As IUnitWork = _ConciliationParticipantsRepository.UnitWork
        Try
            'Elimino el participante de conciliación.
            _ConciliationParticipantsRepository.DeleteEntity(ConciliacionParticipante)
            unitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ConciliationParticipants", audit.Functional, ConciliacionParticipante.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un participante de conciliación especifico.
    ''' </summary>
    ''' <param name="Id">Id Participante Conciliación</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Public Function GetConciliationParticipantsById(Id As String) As ConciliationParticipants Implements IConciliationParticipantsAdminService.GetConciliationParticipantsById
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _ConciliationParticipantsRepository.GetConciliationParticipantsById(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Listar todas los participantes de conciliaciones.
    ''' </summary>
    ''' <returns>Lista de Participantes de Conciliacion</returns>
    Public Function ListAllConciliationParticipants() As List(Of ConciliationParticipants) Implements IConciliationParticipantsAdminService.ListAllConciliationParticipants
        Try
            Return _ConciliationParticipantsRepository.ListAllConciliationParticipants()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Obtiene participantes de conciliación especificos.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Lista Participantes Conciliación</returns>
    Public Function ListConciliationParticipantsByIdConciliationC(Id As String) As List(Of ConciliationParticipants) Implements IConciliationParticipantsAdminService.ListConciliationParticipantsByIdConciliationC
        If String.IsNullOrEmpty(Id) = True Then
            Throw New ArgumentNullException("Id vacío")
        End If
        Try
            Return _ConciliationParticipantsRepository.ListConciliationParticipantsByIdConciliationC(Id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guardar un participante de conciliación.
    ''' </summary>
    ''' <param name="ConciliacionParticipante">Objeto Participante Conciliacion</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Action Result</returns>
    Public Function SaveConciliationParticipants(ConciliacionParticipante As List(Of ConciliationParticipants), audit As AuditMessage) As ActionResult Implements IConciliationParticipantsAdminService.SaveConciliationParticipants
        If ConciliacionParticipante Is Nothing Then
            Throw New ArgumentNullException("Participante conciliación vacio")
        End If
        Dim unitOfWork As IUnitWork = _ConciliationParticipantsRepository.UnitWork
        Try
            Dim j As Integer = 0
            Do
                If ConciliacionParticipante(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    _ConciliationParticipantsRepository.UpdateEntity(ConciliacionParticipante(j))
                ElseIf ConciliacionParticipante(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    _ConciliationParticipantsRepository.AddEntity(ConciliacionParticipante(j))
                ElseIf ConciliacionParticipante(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    _ConciliationParticipantsRepository.DeleteEntity(ConciliacionParticipante(j))
                    j = j - 1
                End If
                j = j + 1
            Loop While j < ConciliacionParticipante.Count
            'Confirmo la unidad de trabajo
            unitOfWork.Commit()
            j = 0
            Do
                If ConciliacionParticipante(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("ConciliationParticipants", audit.Functional, ConciliacionParticipante(j).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
                ElseIf ConciliacionParticipante(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("ConciliationParticipants", audit.Functional, ConciliacionParticipante(j).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
                ElseIf ConciliacionParticipante(j).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted Then
                    '/***** Auditoria Basica ********/
                    IndigoAuditBasic.Execute("ConciliationParticipants", audit.Functional, ConciliacionParticipante(j).Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
                End If
                j = j + 1
            Loop While j < ConciliacionParticipante.Count
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = True, .MessageResult = {ex.Message}.ToList()}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ConciliationParticipantsRepository = Nothing
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
