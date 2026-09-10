'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
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

Public Class ContractTypeAdminService
    Implements IContractTypeAdminService

    'Repositorio de Tipos de contratos
    Private _ContractTypeRepository As IContractTypeRepository

    ''' <summary>
    ''' inicia el repositorio de Tipos de Contratos
    ''' </summary>
    ''' <param name="contractTypeRepository">Repositorio de Tipos de Contratos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contractTypeRepository As IContractTypeRepository)
        If (contractTypeRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Tipos de contratos vacio")
        End If
        _ContractTypeRepository = contractTypeRepository
    End Sub

    ''' <summary>
    ''' Elimina un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteContractType(contractType As ContractType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IContractTypeAdminService.DeleteContractType
        If contractType Is Nothing Then
            Throw New ArgumentNullException("Tipos de Contratos vacio")
        End If
        Dim unitWork As IUnitWork = _ContractTypeRepository.UnitWork
        Try
            _ContractTypeRepository.DeleteEntity(contractType)
            unitWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("ContractType", audit.Functional, contractType.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ContractType)(contractType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            'IndigoAuditSimpleEntity(Of ContractType).Execute(contractType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, contractType)
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="code">Código del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractType(ByVal code As String) As ContractType Implements IContractTypeAdminService.GetContractType
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _ContractTypeRepository.GetContractType(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContractType()
        End Try
    End Function

    ''' <summary>
    ''' Lista de Tipos de Contratos
    ''' </summary>
    ''' <returns>Tipos de Contratos</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractType() As List(Of ContractType) Implements IContractTypeAdminService.ListAllContractType
        Try

            Return _ContractTypeRepository.ListAllContractType()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actuliza un Tipo de Contrato
    ''' </summary>
    ''' <param name="contractType">Tipo de Contrato</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveContractType(contractType As ContractType, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IContractTypeAdminService.SaveContractType
        If contractType Is Nothing Then
            Throw New ArgumentNullException("Tipo de contrato vacio")
        End If
        Dim unitWork As IUnitWork = _ContractTypeRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of ContractType)
            Dim AuxContractType As ContractType = Nothing
            Dim status As Integer

            If contractType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                contractType.ModificationUser = audit.CodeUser
                contractType.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxContractType = _ContractTypeRepository.GetContractType(contractType.Code, False)
            Else
                contractType.CreationUser = audit.CodeUser
                contractType.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _ContractTypeRepository.SaveEntity(contractType)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ContractType)(contractType, audit, status, AuxContractType)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Contrato por ID
    ''' </summary>
    ''' <param name="code">Id del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractTypeById(ID As String) As ContractType Implements IContractTypeAdminService.GetContractTypeById
        If String.IsNullOrEmpty(ID) Then
            Throw New ArgumentNullException("ID vacio")
        End If
        Try
            Return _ContractTypeRepository.GetContractTypeById(ID)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ContractType()
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ContractTypeRepository = Nothing
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
