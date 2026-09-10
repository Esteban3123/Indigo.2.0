'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
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

Public Class ContributorTypeAdminService
    Implements IContributorTypeAdminService

    'Repositorio de contratos
    Private _ContributorTypeRepository As IContributorTypeRepository

    ''' <summary>
    ''' inicia el repositorio de tipos contribuyentes
    ''' </summary>
    ''' <param name="contributorTypeRepository">Repositorio de Tipo contribuyente</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contributorTypeRepository As IContributorTypeRepository)
        If (contributorTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de grupos de contratos vacio")
        End If
        _ContributorTypeRepository = contributorTypeRepository
    End Sub

    ''' <summary>
    ''' Elimina un tipo de contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo contribuyente</param>
    ''' <returns></returns>
    Public Function DeleteContributorType(contributorType As ContributorType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of ContributorType) Implements IContributorTypeAdminService.DeleteContributorType
        Dim result As New ActionMessageResult(Of ContributorType)
        result.StateResult = True
        If contributorType Is Nothing Then
            Throw New ArgumentNullException("Tipo contribuyente Vacio")
        End If
        Dim unitWork As IUnitWork = _ContributorTypeRepository.UnitWork
        Try
            _ContributorTypeRepository.DeleteEntity(contributorType)
            unitWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ContributorType", audit.Functional, contributorType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ContributorType)(contributorType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            'IndigoAuditSimpleEntity(Of ContributorType).Execute(contributorType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, contributorType)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", contributorType.Code))
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            unitWork.RollbackChanges()
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un tipo contribuyente
    ''' </summary>
    ''' <param name="code">Código del tipo contribuyente</param>
    ''' <returns>Tipo Contribuyente</returns>
    Public Function GetContributorType(code As String) As ContributorType Implements IContributorTypeAdminService.GetContributorType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo Vacio")
        End If
        Try

            Return _ContributorTypeRepository.GetContributorType(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContributorType()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los tipos de contribuyentes
    ''' </summary>
    ''' <returns>Lista de tipos de contribuyentes</returns>
    Public Function ListAllContributorType() As List(Of ContributorType) Implements IContributorTypeAdminService.ListAllContributorType
        Try

            Return _ContributorTypeRepository.ListAllContributorType()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un tipo contribuyente
    ''' </summary>
    ''' <param name="contributorType">Tipo Contribuyente</param>
    ''' <returns></returns>
    Public Function SaveContributorType(contributorType As ContributorType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IContributorTypeAdminService.SaveContributorType
        If contributorType Is Nothing Then
            Throw New ArgumentNullException("Tipo contribuyente vacio")
        End If
        Dim unitwork As IUnitWork = _ContributorTypeRepository.UnitWork
        Try


            Dim auditProcess As IndigoAuditSimpleEntity(Of ContributorType)
            Dim AuxContributorType As ContributorType = Nothing
            Dim status As Integer

            If contributorType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                contributorType.ModificationUser = audit.CodeUser
                contributorType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxContributorType = _ContributorTypeRepository.GetContributorType(contributorType.Code, False)
            Else
                contributorType.CreationUser = audit.CodeUser
                contributorType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _ContributorTypeRepository.SaveEntity(contributorType)
            unitwork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContributorType)(contributorType, audit, status, AuxContributorType)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitwork.RollbackChanges()
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
            _ContributorTypeRepository = Nothing
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
