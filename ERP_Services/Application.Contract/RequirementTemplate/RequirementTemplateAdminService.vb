'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core

Public Class RequirementTemplateAdminService
    Implements IRequirementTemplateAdminService


    Private _requirementTemplateRepository As IRequirementTemplateRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDRepository As ISequenseContractDRepository

    Public Sub New(requirementTemplateRepository As IRequirementTemplateRepository, secuenseDRepository As ISequenseContractDRepository)
        If requirementTemplateRepository Is Nothing Then
            Throw New ArgumentNullException("requirementTemplateRepository")
        End If
        If secuenseDRepository Is Nothing Then
            Throw New ArgumentNullException("secuenseDRepository")
        End If
        _requirementTemplateRepository = requirementTemplateRepository
        _secuenseDRepository = secuenseDRepository
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateRequirementTemplate(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of RequirementTemplate) Implements IRequirementTemplateAdminService.ChangeStateRequirementTemplate
        Dim RequirementTemplate As RequirementTemplate = _requirementTemplateRepository.GetRequirementTemplate(code)
        RequirementTemplate.Status = state
        RequirementTemplate.MarkAsModified()
        Return SaveRequirementTemplate(RequirementTemplate, audit)
    End Function

    ''' <summary>
    ''' Elimina una RequirementTemplate
    ''' </summary>
    ''' <param name="RequirementTemplate"></param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">RequirementTemplate</exception>
    Public Function DeleteRequirementTemplate(RequirementTemplate As RequirementTemplate, audit As AuditMessage) As ActionResult Implements IRequirementTemplateAdminService.DeleteRequirementTemplate
        If RequirementTemplate Is Nothing Then
            Throw New ArgumentNullException("RequirementTemplate")
        End If
        Dim unitOfWork As IUnitWork = Me._requirementTemplateRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of RequirementTemplate)
            auditProcess = New IndigoAuditSimpleEntity(Of RequirementTemplate)(RequirementTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            Me._requirementTemplateRepository.SaveEntity(RequirementTemplate)
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
    ''' Obtiene una RequirementTemplate por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">
    ''' code
    ''' or
    ''' audit
    ''' </exception>
    Public Function GetRequirementTemplate(code As String, audit As AuditMessage) As RequirementTemplate Implements IRequirementTemplateAdminService.GetRequirementTemplate
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RequirementTemplate As RequirementTemplate = Me._requirementTemplateRepository.GetRequirementTemplate(code.Trim())
            If RequirementTemplate IsNot Nothing AndAlso RequirementTemplate.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RequirementTemplate)(RequirementTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return RequirementTemplate
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RequirementTemplate
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una RequirementTemplate por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">id</exception>
    Public Function GetRequirementTemplateById(id As Integer) As RequirementTemplate Implements IRequirementTemplateAdminService.GetRequirementTemplateById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Return Me._requirementTemplateRepository.GetRequirementTemplateById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New RequirementTemplate
        End Try
    End Function

    ''' <summary>
    ''' Guarda o Actualiza una RequirementTemplate
    ''' </summary>
    ''' <param name="RequirementTemplate"></param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">RequirementTemplate</exception>
    Public Function SaveRequirementTemplate(RequirementTemplate As RequirementTemplate, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of RequirementTemplate) Implements IRequirementTemplateAdminService.SaveRequirementTemplate
        If RequirementTemplate Is Nothing Then
            Throw New ArgumentNullException("RequirementTemplate")
        End If
        Dim unitOfWork As IUnitWork = Me._requirementTemplateRepository.UnitWork
        Dim sequenseUnitOfWork As IUnitWork = Me._secuenseDRepository.UnitWork
        Try
            Dim seq As ContractSequenceDetail = Nothing
            If RequirementTemplate.Code Is Nothing OrElse RequirementTemplate.Code.Trim().Equals(String.Empty) Then
                seq = Me._secuenseDRepository.GetSequenseDById(idSequense)
                If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.ContractSequence.Sequential Then
                    Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
                    If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                        RequirementTemplate.Code = res
                        seq.Next += 1
                        Me._secuenseDRepository.SaveEntity(seq)
                    Else
                        Return New ActionResult(Of RequirementTemplate) With {.StateResult = False, .MessageResult = {"_Seq02_"}.ToList()}
                    End If
                Else
                    Return New ActionResult(Of RequirementTemplate) With {.StateResult = False, .MessageResult = {"_Seq01_"}.ToList()}
                End If
            End If

            Dim auxRequirementTemplate As RequirementTemplate = Nothing
            Dim auditProcess As IndigoAuditSimpleEntity(Of RequirementTemplate)
            Dim status As Integer

            If RequirementTemplate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                RequirementTemplate.CreationUser = audit.CodeUser
                RequirementTemplate.CreationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            Else
                auxRequirementTemplate = RequirementTemplate.OriginalValue
                RequirementTemplate.ModificationUser = audit.CodeUser
                RequirementTemplate.ModificationDate = DateTime.Now
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            End If

            Me._requirementTemplateRepository.SaveEntity(RequirementTemplate)
            unitOfWork.Commit()
            sequenseUnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of RequirementTemplate)(RequirementTemplate, audit, status, auxRequirementTemplate)
            auditProcess.Execute()

            Return New ActionResult(Of RequirementTemplate) With {.StateResult = True, .ObjectEmbbeded = RequirementTemplate}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of RequirementTemplate) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequirementTemplate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _requirementTemplateRepository = Nothing
            _secuenseDRepository = Nothing
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
