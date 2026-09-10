'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
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

Public Class ContractTemplateAdminService

    Implements IContractTemplateAdminService

    'Repositorio de Plantillas de contratos
    Private _ContractTemplateRepository As IContractTemplateRepository

    ''' <summary>
    ''' inicia el repositorio de Plantillas de Contratos
    ''' </summary>
    ''' <param name="contractTemplateRepository">Repositorio de Plantillas de Contratos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contractTemplateRepository As IContractTemplateRepository)
        If (contractTemplateRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Plantillas de contratos vacio")
        End If
        _ContractTemplateRepository = contractTemplateRepository
    End Sub

    ''' <summary>
    ''' Elimina una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteContractTemplate(contractTemplate As ContractTemplate, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of ContractTemplate) Implements IContractTemplateAdminService.DeleteContractTemplate
        Dim result As New ActionMessageResult(Of ContractTemplate)
        result.StateResult = True
        If contractTemplate Is Nothing Then
            Throw New ArgumentNullException("Plantillas de Contratos vacio")
        End If
        Dim unitWork As IUnitWork = _ContractTemplateRepository.UnitWork
        Try
            _ContractTemplateRepository.DeleteEntity(contractTemplate)
            unitWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ContractTemplate", audit.Functional, contractTemplate.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ContractTemplate)(contractTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            'IndigoAuditSimpleEntity(Of ContractTemplate).Execute(contractTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, contractTemplate)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", contractTemplate.Code))
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            unitWork.RollbackChanges()
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una Plantilla de Contrato
    ''' </summary>
    ''' <param name="code">Código de la Plantilla de Contrato</param>
    ''' <returns>Plantilla de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractTemplate(code As String) As ContractTemplate Implements IContractTemplateAdminService.GetContractTemplate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _ContractTemplateRepository.GetContractTemplate(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContractTemplate()
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las Plantillas de Contrato
    ''' </summary>
    ''' <returns>Plantillas de Contrato</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractTemplate() As List(Of ContractTemplate) Implements IContractTemplateAdminService.ListAllContractTemplate
        Try
            Return _ContractTemplateRepository.ListAllContractTemplate()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Actualiza o Almacena una Plantilla de Contrato
    ''' </summary>
    ''' <param name="contractTemplate">Plantilla de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveContractTemplate(contractTemplate As ContractTemplate, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IContractTemplateAdminService.SaveContractTemplate
        If contractTemplate Is Nothing Then
            Throw New ArgumentNullException("Plantilla de contrato vacio")
        End If
        Dim unitWork As IUnitWork = _ContractTemplateRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractTemplate)
            Dim AuxContractTemplate As ContractTemplate = Nothing
            Dim status As Integer

            If contractTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                contractTemplate.ModificationUser = audit.CodeUser
                contractTemplate.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxContractTemplate = _ContractTemplateRepository.GetContractTemplate(contractTemplate.Code, False)
            Else
                contractTemplate.CreationUser = audit.CodeUser
                contractTemplate.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _ContractTemplateRepository.SaveEntity(contractTemplate)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractTemplate)(contractTemplate, audit, status, AuxContractTemplate)
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
            _ContractTemplateRepository = Nothing
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
