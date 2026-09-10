'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Juan Diego Diaz M.
' Created          : 01-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Transactions

Imports Domain.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Application.Base
Imports Domain.Common.Entities
Imports System.Data.Entity.Core

#End Region
Public Class JustificationTemplateAdminService
    Implements IJustificationTemplateAdminService

    Private _JustificationTemplateRepository As IJustificationTemplateRepository

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase <see cref="JustificationTemplateAdminService" />.
    ''' </summary>
    ''' <param name="JustificationTemplateRepository">El repositorio para el manejo de las plantillas de justificación.</param>
    Public Sub New(ByVal JustificationTemplateRepository As IJustificationTemplateRepository)
        If JustificationTemplateRepository Is Nothing Then
            Throw New ArgumentNullException("JustificationTemplateRepository Vacio")
        End If
        _JustificationTemplateRepository = JustificationTemplateRepository
    End Sub

    ''' <summary>
    ''' Elimina una plantilla de justificación
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto Justification Template</param>
    ''' <param name="audit">Objeto de auditoria</param>
    ''' <returns></returns>
    Public Function DeleteJustificationTemplate(JustificationTemplate As JustificationTemplate, audit As AuditMessage) As ActionResult Implements IJustificationTemplateAdminService.DeleteJustificationTemplate
        If JustificationTemplate Is Nothing Then
            Throw New ArgumentNullException("Plantilla de Justificación vacía")
        End If
        Dim unitOfWork As IUnitWork = _JustificationTemplateRepository.UnitWork
        Try
            'elimino la plantilla
            _JustificationTemplateRepository.DeleteEntity(JustificationTemplate)
            unitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("JustificationTemplate", audit.Functional, JustificationTemplate.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/***** Auditoria Avanzada *****/
            Dim auditObject As New IndigoAuditSimpleEntity(Of JustificationTemplate)(JustificationTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
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
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

    ''' <summary>
    ''' consulta una plantilla de justificación especifica
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns></returns>
    Public Function getJustificationTemplate(code As String, audit As AuditMessage) As JustificationTemplate Implements IJustificationTemplateAdminService.getJustificationTemplate
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("Código vacío")
        End If
        Try
            Dim Justification = _JustificationTemplateRepository.getJustificationTemplate(code)
            If Justification.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of JustificationTemplate)(Justification, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If
            Return Justification
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Lista todas las plantillas de justificación.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllJustificationTemplate(audit As AuditMessage) As List(Of JustificationTemplate) Implements IJustificationTemplateAdminService.ListAllJustificationTemplate
        Try
            Dim Justifications = _JustificationTemplateRepository.ListAllJustificationTemplate
            For Each item As JustificationTemplate In Justifications
                Dim auditObject As New IndigoAuditSimpleEntity(Of JustificationTemplate)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return Justifications
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda una plantilla de justificacion
    ''' </summary>
    ''' <param name="JustificationTemplate">Objeto Justification Template</param>
    ''' <param name="audit">Objeto de auditoria</param>
    ''' <returns></returns>
    Public Function SaveJustificationTemplate(JustificationTemplate As JustificationTemplate, audit As AuditMessage) As ActionResult(Of JustificationTemplate) Implements IJustificationTemplateAdminService.SaveJustificationTemplate
        If JustificationTemplate Is Nothing Then
            Throw New ArgumentNullException("Plantilla Justificación vacía")
        End If
        Dim unitOfWork As IUnitWork = _JustificationTemplateRepository.UnitWork
        Try
            Dim auditProcess As IndigoAuditSimpleEntity(Of JustificationTemplate)
            Dim AuxResponsible As JustificationTemplate = Nothing
            Dim status As Integer
            Dim AUXJustification As JustificationTemplate = Nothing
            If JustificationTemplate.ChangeTracker.State = ObjectState.Modified Then
                AUXJustification = JustificationTemplate.OriginalValue
                JustificationTemplate.ModificationUser = audit.CodeUser
                JustificationTemplate.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
            Else
                JustificationTemplate.CreationUser = audit.CodeUser
                JustificationTemplate.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If
            _JustificationTemplateRepository.SaveEntity(JustificationTemplate)
            'confirmo la unidad de trabajo
            unitOfWork.Commit()
            'Audito
            auditProcess = New IndigoAuditSimpleEntity(Of JustificationTemplate)(JustificationTemplate, audit, status, AUXJustification)
            auditProcess.Execute()


            'If (JustificationTemplate.ChangeTracker.State = ObjectState.Added) Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute("JustificationTemplate", audit.Functional, JustificationTemplate.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Crear, audit.Company, audit.ContainerSecurity)
            '    '/***** Auditoria Avanzada *****/
            '    IndigoAuditSimpleEntity(Of JustificationTemplate).Execute(JustificationTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Insert, audit.Company)
            'ElseIf JustificationTemplate.ChangeTracker.State = ObjectState.Modified Then
            '    '/***** Auditoria Basica ********/
            '    IndigoAuditBasic.Execute("JustificationTemplate", audit.Functional, JustificationTemplate.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Modificar, audit.Company, audit.ContainerSecurity)
            '    '/***** Auditoria Avanzada *****/
            '    IndigoAuditSimpleEntity(Of JustificationTemplate).Execute(JustificationTemplate, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, AUXJustification)
            'End If
            Return New ActionResult(Of JustificationTemplate) With {.StateResult = True, .ObjectEmbbeded = JustificationTemplate}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of JustificationTemplate) With {.StateResult = False, .MessageResult = {"-999"}.ToList()}
        Catch ex As System.Data.Entity.Infrastructure.DbUpdateException
            unitOfWork.RollbackChanges()
            Return New ActionResult(Of JustificationTemplate) With {.StateResult = False, .MessageResult = {"-888"}.ToList()}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of JustificationTemplate) With {.StateResult = False, .MessageResult = {ex.Message}.ToList}
        End Try
    End Function

    ''' <summary>
    ''' consulta una plantilla de justificación por código
    ''' </summary>
    ''' <param name="code">El codigo de la plantilla</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Public Function listJustificationTemplateByCode(code As String, audit As AuditMessage) As List(Of JustificationTemplate) Implements IJustificationTemplateAdminService.listJustificationTemplateByCode
        If String.IsNullOrEmpty(code) = True Then
            Throw New ArgumentNullException("Código vacío")
        End If
        Try
            Dim Justifications = _JustificationTemplateRepository.listJustificationTemplateByCode(code)
            For Each item As JustificationTemplate In Justifications
                Dim auditObject As New IndigoAuditSimpleEntity(Of JustificationTemplate)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return Justifications
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' consulta una plantilla de justificación por concepto
    ''' </summary>
    ''' <param name="Concept">El codigo de la plantilla</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Lista JustificationTemplate</returns>
    Public Function listJustificationTemplateByConcept(Concept As String, audit As AuditMessage) As List(Of JustificationTemplate) Implements IJustificationTemplateAdminService.listJustificationTemplateByConcept
        If String.IsNullOrEmpty(Concept) = True Then
            Throw New ArgumentNullException("Concepto vacío")
        End If
        Try
            Dim Justifications = _JustificationTemplateRepository.listJustificationTemplateByConcept(Concept)
            For Each item As JustificationTemplate In Justifications
                Dim auditObject As New IndigoAuditSimpleEntity(Of JustificationTemplate)(item, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            Next
            Return Justifications
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
            _JustificationTemplateRepository = Nothing
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
