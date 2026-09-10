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

Public Class ContractModificationReasonAdminService
    Implements IContractModificationReasonAdminService

    'Repositorio de Plantillas de contratos
    Private _ContractModificationRepository As IContractModificationReasonRepository

    ''' <summary>
    ''' inicia el repositorio de razones de otro si
    ''' </summary>
    ''' <param name="contractModificationRepository">Repositorio de razon de otro si</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contractModificationRepository As IContractModificationReasonRepository)
        If (contractModificationRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Plantillas de contratos vacio")
        End If
        _ContractModificationRepository = contractModificationRepository
    End Sub

    ''' <summary>
    ''' Elimina una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteContractModificationReason(contractModificationReason As ContractModificationReason, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionMessageResult(Of ContractModificationReason) Implements IContractModificationReasonAdminService.DeleteContractModificationReason
        Dim result As New ActionMessageResult(Of ContractModificationReason)
        result.StateResult = True
        If contractModificationReason Is Nothing Then
            Throw New ArgumentNullException("Razon de otro si Vacio")
        End If
        Dim unitWork As IUnitWork = _ContractModificationRepository.UnitWork
        Try
            _ContractModificationRepository.DeleteEntity(contractModificationReason)

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ContractModificationReason", audit.Functional, contractModificationReason.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ContractModificationReason)(contractModificationReason, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            unitWork.Commit()
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", contractModificationReason.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            result.StateResult = False
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una razon de otro si por codigo
    ''' </summary>
    ''' <returns>razones de otro si</returns>
    ''' <remarks></remarks>
    Public Function GetContractModificationReason(code As String) As ContractModificationReason Implements IContractModificationReasonAdminService.GetContractModificationReason
        Try

            Return _ContractModificationRepository.GetContractModificationReason(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveContractModificationReason(contractModificationReason As ContractModificationReason, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IContractModificationReasonAdminService.SaveContractModificationReason
        If contractModificationReason Is Nothing Then
            Throw New ArgumentNullException("Razon de otro si Vacio")
        End If
        Dim unitWork As IUnitWork = _ContractModificationRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractModificationReason)
            Dim AuxContractModificationReason As ContractModificationReason = Nothing
            Dim status As Integer

            If contractModificationReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                contractModificationReason.ModificationUser = audit.CodeUser
                contractModificationReason.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxContractModificationReason = _ContractModificationRepository.GetContractModificationReason(contractModificationReason.Code, False)
            Else
                contractModificationReason.CreationUser = audit.CodeUser
                contractModificationReason.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _ContractModificationRepository.SaveEntity(contractModificationReason)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractModificationReason)(contractModificationReason, audit, status, AuxContractModificationReason)
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
            _ContractModificationRepository = Nothing
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
