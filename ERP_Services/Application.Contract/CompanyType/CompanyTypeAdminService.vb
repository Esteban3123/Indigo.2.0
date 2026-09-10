'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Transactions
Imports Application.Security
Imports Application.Contract
Imports System.Data.Entity.Core

Public Class CompanyTypeAdminService
    Implements ICompanyTypeAdminService, Inject

#Region "Variables"
    'CompanyType
    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _companyTypeRepository As ICompanyTypeRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    Public Const FORM_NAME As String = "Entidad"

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal CompanyTypeRepository As ICompanyTypeRepository,
                   ByVal secuenseDRepository As ISequenseContractDRepository)
        If CompanyTypeRepository Is Nothing Then
            Throw New ArgumentNullException("CompanyTypeRepository Vacio")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _companyTypeRepository = CompanyTypeRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChangeStateCompanyType(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of CompanyType) Implements ICompanyTypeAdminService.ChangeStateCompanyType
        Dim CompanyType As CompanyType = _companyTypeRepository.GetCompanyTypeByCode(code)
        CompanyType.Status = state
        Return SaveCompanyType(CompanyType, audit)
    End Function

    ''' <summary>
    ''' Trae todas las entidades
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllCompanyType(audit As AuditMessage) As List(Of CompanyType) Implements ICompanyTypeAdminService.GetAllCompanyType
        Try
            Dim companyType = Me._companyTypeRepository.GetAll()
            For Each item As CompanyType In companyType
                Dim auditObject As New IndigoAuditSimpleEntity(Of CompanyType)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return companyType
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Function SaveCompanyType(CompanyType As CompanyType, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of CompanyType) Implements ICompanyTypeAdminService.SaveCompanyType
        If CompanyType Is Nothing Then
            Throw New ArgumentNullException("CompanyType")
        End If
        Dim unitOfWork As IUnitWork = Me._companyTypeRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim MessageResult As String = String.Empty
                Dim seq As ContractSequenceDetail = Nothing
                If CompanyType.Code Is Nothing OrElse CompanyType.Code.Trim().Equals(String.Empty) Then
                    seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                        Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                        If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            CompanyType.Code = res
                            seq.Next += 1
                            Me._secuenseDRepository.SaveEntity(seq)
                        Else
                            scope.Dispose()
                            Return New ActionResult(Of CompanyType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                        End If
                        MessageResult = If(seq.ContractSequence.Sequential, String.Format(ResourceManager.GetString("SavedWithCode"), CompanyType.Code), ResourceManager.GetString("SaveMessage"))
                    Else
                        scope.Dispose()
                        Return New ActionResult(Of CompanyType) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME)}
                    End If
                Else
                    MessageResult = ResourceManager.GetString("SaveMessage")
                End If

                Dim auxCompanyType As CompanyType = Nothing
                Dim auditProcess As IndigoAuditSimpleEntity(Of CompanyType)
                Dim status As Integer

                If CompanyType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    CompanyType.CreationUser = audit.CodeUser
                    CompanyType.CreationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Insert
                Else
                    auxCompanyType = CompanyType.OriginalValue
                    CompanyType.ModificationUser = audit.CodeUser
                    CompanyType.ModificationDate = DateTime.Now
                    status = Infrastructure.CrossCutting.Audit.Actions.Update
                End If

                Me._companyTypeRepository.SaveEntity(CompanyType)
                unitOfWork.Commit()
                sequenseUnitOfWork.Commit()
                auditProcess = New IndigoAuditSimpleEntity(Of CompanyType)(CompanyType, audit, status, auxCompanyType)
                auditProcess.Execute()

                'Se marca la entidad como sin cambios
                CompanyType.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of CompanyType) With {.StateResult = True, .ObjectEmbbeded = CompanyType}
            End Using
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of CompanyType) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CompanyType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Elimina una entidad
    ''' </summary>
    ''' <param name="CompanyType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteCompanyType(CompanyType As CompanyType, audit As AuditMessage) As ActionResult Implements ICompanyTypeAdminService.DeleteCompanyType
        If CompanyType Is Nothing Then
            Throw New ArgumentNullException("CompanyType")
        End If
        Dim unitOfWork As IUnitWork = Me._companyTypeRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of CompanyType)
            auditProcess = New IndigoAuditSimpleEntity(Of CompanyType)(CompanyType, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._companyTypeRepository.DeleteEntity(CompanyType)
            unitOfWork.Commit()
            auditProcess.Execute()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As UpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-000"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompanyTypeByCode(code As String, audit As AuditMessage) As ActionResult(Of CompanyType) Implements ICompanyTypeAdminService.GetCompanyTypeByCode
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CompanyType As CompanyType = Me._companyTypeRepository.GetCompanyTypeByCode(code.Trim())
            If CompanyType IsNot Nothing AndAlso CompanyType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CompanyType)(CompanyType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CompanyType) With {.StateResult = True, .ObjectEmbbeded = CompanyType}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CompanyType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCompanyTypeById(id As Integer, audit As AuditMessage) As ActionResult(Of CompanyType) Implements ICompanyTypeAdminService.GetCompanyTypeById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim CompanyType As CompanyType = Me._companyTypeRepository.GetCompanyTypeById(id)
            If CompanyType IsNot Nothing AndAlso CompanyType.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of CompanyType)(CompanyType, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return New ActionResult(Of CompanyType) With {.StateResult = True, .ObjectEmbbeded = CompanyType}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CompanyType) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _secuenseDRepository = Nothing
            _CompanyTypeRepository = Nothing
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