'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

Public Class PensionaryTypeAdminService
    Implements IPensionaryTypeAdminService

    Private _pensionaryTypeRepository As IPensionaryTypeRepository

    Public Sub New(ByVal pensionaryTypeRepository As IPensionaryTypeRepository)
        If pensionaryTypeRepository Is Nothing Then
            Throw New ArgumentNullException("pensionaryTypeRepository Vacio")
        End If
        _pensionaryTypeRepository = pensionaryTypeRepository
    End Sub

    ''' <summary>
    ''' Elimina un Tipo de Pensionados
    ''' </summary>
    ''' <param name="pensionaryType">Tipo de Pensionados</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePensionaryType(pensionaryType As PensionaryType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of PensionaryType) Implements IPensionaryTypeAdminService.DeletePensionaryType
        Dim result As New ActionMessageResult(Of PensionaryType)
        result.StateResult = True
        If pensionaryType Is Nothing Then
            Throw New ArgumentNullException("pensionaryType Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _pensionaryTypeRepository.UnitWork
        Try
            _pensionaryTypeRepository.DeleteEntity(pensionaryType)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(pensionaryType.GetType.Name, audit.Functional, pensionaryType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of PensionaryType)(pensionaryType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", pensionaryType.Code))
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            Return result
        End Try

    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Pensionados en Específico
    ''' </summary>
    ''' <param name="code">Código del Tipo de Pensionados</param>
    ''' <returns>Tipo de Pensionados</returns>
    ''' <remarks></remarks>
    Public Function GetPensionaryType(code As String) As PensionaryType Implements IPensionaryTypeAdminService.GetPensionaryType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _pensionaryTypeRepository.GetPensionaryType(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New PensionaryType()
        End Try
    End Function

    ''' <summary>
    ''' Lista de Tipos de Pensionados
    ''' </summary>
    ''' <returns>Lista de Tipos de Pensionados</returns>
    ''' <remarks></remarks>
    Public Function ListAllPensionaryType() As List(Of PensionaryType) Implements IPensionaryTypeAdminService.ListAllPensionaryType
        Try
            Return _pensionaryTypeRepository.ListAllPensionaryType()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un Tipo de Pensionados
    ''' </summary>
    ''' <param name="pensionaryType">Tipos de Pensionados</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePensionaryType(pensionaryType As PensionaryType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IPensionaryTypeAdminService.SavePensionaryType
        If pensionaryType Is Nothing Then
            Throw New ArgumentNullException("pensionaryType Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _pensionaryTypeRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of PensionaryType)
            Dim auxPensionaryType As PensionaryType = Nothing
            Dim status As Integer

            If pensionaryType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                pensionaryType.ModificationUser = audit.CodeUser
                pensionaryType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxPensionaryType = _pensionaryTypeRepository.GetPensionaryType(pensionaryType.Code, False)
            Else
                pensionaryType.CreationUser = audit.CodeUser
                pensionaryType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _pensionaryTypeRepository.SaveEntity(pensionaryType)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of PensionaryType)(pensionaryType, audit, status, auxPensionaryType)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
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
            _pensionaryTypeRepository = Nothing
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
