'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
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

Public Class AccountingStructureAdminService

    Implements IAccountingStructureAdminService

    Private _accountingStructureRepository As IAccountingStructureRepository

    Public Sub New(ByVal accountingStructureRepository As IAccountingStructureRepository)
        If accountingStructureRepository Is Nothing Then
            Throw New ArgumentNullException("accountingStructureRepository Vacio")
        End If
        _accountingStructureRepository = accountingStructureRepository
    End Sub

    ''' <summary>
    ''' Elimina una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteAccountingStructure(accountingStructure As AccountingStructure, audit As AuditMessage) As ActionMessageResult(Of AccountingStructure) Implements IAccountingStructureAdminService.DeleteAccountingStructure
        Dim result As New ActionMessageResult(Of AccountingStructure)
        result.StateResult = True
        If accountingStructure Is Nothing Then
            Throw New ArgumentNullException("accountingStructure Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _accountingStructureRepository.UnitWork
        Try
            _accountingStructureRepository.DeleteEntity(accountingStructure)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(accountingStructure.GetType.Name, audit.Functional, accountingStructure.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of AccountingStructure)(accountingStructure, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", accountingStructure.Code))
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Código
    ''' </summary>
    ''' <param name="code">Código Accounting Structure</param>
    ''' <param name="desatach"></param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructure(code As String, Optional desatach As Boolean = True) As AccountingStructure Implements IAccountingStructureAdminService.GetAccountingStructure
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _accountingStructureRepository.GetAccountingStructure(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AccountingStructure()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Id
    ''' </summary>
    ''' <param name="accountingStructureId">Id Accounting Structure</param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructureById(accountingStructureId As Integer, Optional desatach As Boolean = True) As AccountingStructure Implements IAccountingStructureAdminService.GetAccountingStructureById
        If String.IsNullOrEmpty(accountingStructureId) Then
            Throw New ArgumentNullException("accountingStructureId Vacio")
        End If
        Try
            Return _accountingStructureRepository.GetAccountingStructureById(accountingStructureId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New AccountingStructure()
        End Try
    End Function

    ''' <summary>
    ''' Almacena una Estructura Contable de Nómina
    ''' </summary>
    ''' <param name="accountingStructure">accountingStructure</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveAccountingStructure(accountingStructure As AccountingStructure, audit As AuditMessage) As Boolean Implements IAccountingStructureAdminService.SaveAccountingStructure
        If accountingStructure Is Nothing Then
            Throw New ArgumentNullException("accountingStructure Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _accountingStructureRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of AccountingStructure)
            Dim auxAccountingStructure As AccountingStructure = Nothing
            Dim status As Integer

            If accountingStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                accountingStructure.ModificationUser = audit.CodeUser
                accountingStructure.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxAccountingStructure = _accountingStructureRepository.GetAccountingStructure(accountingStructure.Code, False)
            Else
                accountingStructure.CreationUser = audit.CodeUser
                accountingStructure.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _accountingStructureRepository.SaveEntity(accountingStructure)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of AccountingStructure)(accountingStructure, audit, status, auxAccountingStructure)
            auditProcess.Execute()
            Return True

        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lista Toda las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAccountingStructure() As List(Of AccountingStructure) Implements IAccountingStructureAdminService.ListAllAccountingStructure
        Try
            Return _accountingStructureRepository.ListAllAccountingStructure()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _accountingStructureRepository = Nothing
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
