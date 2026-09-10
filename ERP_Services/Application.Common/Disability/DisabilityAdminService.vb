'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 03-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Data.Entity.Infrastructure

Public Class DisabilityAdminService
    Implements IDisabilityAdminService


    ' Repositoria de Discapacidades
    Private _DisabilityRepository As IDisabilityRepository

    ''' <summary>
    ''' Costructor el cual inicia el repositorio de discapacidades
    ''' </summary>
    ''' <param name="repository">Repositorio de discapacidades</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal repository As IDisabilityRepository)
        If (repository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de discapacidades Vacio")
        End If
        _DisabilityRepository = repository
    End Sub

    ''' <summary>
    ''' Elimina una discapacidad
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function DeleteDisability(disability As Disability, audit As AuditMessage) As ActionMessageResult(Of Disability) Implements IDisabilityAdminService.DeleteDisability
        Dim result As New ActionMessageResult(Of Disability)
        result.StateResult = True
        If disability Is Nothing Then
            Throw New ArgumentNullException("Discapacidades vacio")
        End If
        Dim unitWork As IUnitWork = _DisabilityRepository.UnitWork
        Try
            _DisabilityRepository.DeleteEntity(disability)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(disability.GetType.Name, audit.Functional, disability.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Domain.Entities.Disability)(disability, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result

        Catch ex As DbUpdateException
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", disability.Code))
            unitWork.RollbackChanges()
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Discapacidad especifica
    ''' </summary>
    ''' <param name="code">Codigo Discapacidad</param>
    ''' <returns>Discapacidad</returns>
    ''' <remarks></remarks>
    Public Function GetDisability(code As String) As Domain.Entities.Disability Implements IDisabilityAdminService.GetDisability
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try
            Return _DisabilityRepository.GetDisability(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Domain.Entities.Disability()
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las Discapacidades
    ''' </summary>
    ''' <returns>Lista de Discapacidades</returns>
    ''' <remarks></remarks>
    Public Function ListAllDisability() As List(Of Domain.Entities.Disability) Implements IDisabilityAdminService.ListAllDisability
        Try
            Return _DisabilityRepository.ListAllDisability()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Graba o Actualiza una Discapacidad
    ''' </summary>
    ''' <param name="disability">Discapacidad</param>
    ''' <param name="audit">Objeto de Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Public Function SaveDisability(disability As Domain.Entities.Disability, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IDisabilityAdminService.SaveDisability
        If disability Is Nothing Then
            Throw New ArgumentNullException("Discapacidad vacia")
        End If
        Dim unitWork As IUnitWork = _DisabilityRepository.UnitWork
        Try
            'Dim auxDisabilidy As Disability = Nothing
            'If disability.ChangeTracker.State = ObjectState.Modified Then
            '    auxDisabilidy = _DisabilityRepository.GetDisability(disability.Code, False)
            'End If

            '_DisabilityRepository.SaveEntity(disability)
            'unitWork.Commit()
            'If disability.ChangeTracker.State = ObjectState.Added Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute(disability.GetType.Name, audit.Functional, disability.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '    '/*****Auditoria Avanzada ******/
            '    IndigoAuditSimpleEntity(Of Disability).Execute(disability, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, audit.Company)
            'ElseIf disability.ChangeTracker.State = ObjectState.Modified Then
            '    IndigoAuditSimpleEntity(Of Disability).Execute(disability, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, auxDisabilidy)
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute(disability.GetType.Name, audit.Functional, disability.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            'End If
            'Return True

            Dim auditProcess As IndigoAuditSimpleEntity(Of Disability)
            Dim auxDisabilidy As Disability = Nothing
            Dim status As Integer

            If disability.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                disability.ModificationUser = audit.CodeUser
                disability.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxDisabilidy = _DisabilityRepository.GetDisability(disability.Code, False)
            Else
                disability.CreationUser = audit.CodeUser
                disability.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _DisabilityRepository.SaveEntity(disability)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Disability)(disability, audit, status, auxDisabilidy)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
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
            _DisabilityRepository = Nothing
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
