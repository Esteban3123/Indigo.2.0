'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
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

Public Class ContractGroupAdminService
    Implements IContractGroupAdminService


    'Repositorio de contratos
    Private _ContractRepository As IContractGroupRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="contractRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contractRepository As IContractGroupRepository)
        If (contractRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de grupos de contratos vacio")
        End If
        _ContractRepository = contractRepository
    End Sub

    ''' <summary>
    ''' Obtiene un grupo de contrato
    ''' </summary>
    ''' <param name="code">Código de grupo de contrato</param>
    ''' <returns> Grupo de contrato</returns>
    Public Function GetContractGroup(code As String) As Domain.Payroll.Entities.ContractGroup Implements IContractGroupAdminService.GetContractGroup
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _ContractRepository.GetContractGroup(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContractGroup()
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los Grupos de contratos
    ''' </summary>
    ''' <returns>Lista los grupos de contratos</returns>
    Public Function ListAllContractGroup() As List(Of Domain.Payroll.Entities.ContractGroup) Implements IContractGroupAdminService.ListAllContractGroup
        Try

            Return _ContractRepository.ListAllContractGroup()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda o edita un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    Public Function SaveContractGroup(contractGroup As Domain.Payroll.Entities.ContractGroup, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IContractGroupAdminService.SaveContractGroup
        If contractGroup Is Nothing Then
            Throw New ArgumentNullException("Grupo de contrato vacio")
        End If
        Dim unitWork As IUnitWork = _ContractRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractGroup)
            Dim AuxContractGroup As ContractGroup = Nothing
            Dim status As Integer

            If contractGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                contractGroup.ModificationUser = audit.CodeUser
                contractGroup.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxContractGroup = _ContractRepository.GetContractGroup(contractGroup.Code, False)
            Else
                contractGroup.CreationUser = audit.CodeUser
                contractGroup.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _ContractRepository.SaveEntity(contractGroup)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractGroup)(contractGroup, audit, status, AuxContractGroup)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina un grupo de contrato
    ''' </summary>
    ''' <param name="contractGroup">Grupo de contrato</param>
    ''' <returns></returns>
    Public Function DeleteContractGroup(contractGroup As ContractGroup, audit As AuditMessage) As ActionMessageResult(Of ContractGroup) Implements IContractGroupAdminService.DeleteContractGroup
        Dim result As New ActionMessageResult(Of ContractGroup)
        result.StateResult = True
        If contractGroup Is Nothing Then
            Throw New ArgumentNullException("Grupos de contratos vacio")
        End If
        Dim unitWork As IUnitWork = _ContractRepository.UnitWork
        Try
            _ContractRepository.DeleteEntity(contractGroup)
            unitWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ContractGroup", audit.Functional, contractGroup.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ContractGroup)(contractGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            'IndigoAuditSimpleEntity(Of ContractGroup).Execute(contractGroup, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, contractGroup)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", contractGroup.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ContractRepository = Nothing
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
