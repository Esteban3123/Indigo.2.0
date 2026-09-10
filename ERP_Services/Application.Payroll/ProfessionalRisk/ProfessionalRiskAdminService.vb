'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 02-07-2013
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


Public Class ProfessionalRiskAdminService
    Implements IProfessionalRiskAdminService

    Private _professionalRiskRepository As IProfessionalRiskRepository

    Public Sub New(ByVal professionalRiskRepository As IProfessionalRiskRepository)
        If professionalRiskRepository Is Nothing Then
            Throw New ArgumentNullException("professionalRiskRepository Vacio")
        End If
        _professionalRiskRepository = professionalRiskRepository
    End Sub

    ''' <summary>
    ''' Obtiene un Riesgo Profesional
    ''' </summary>
    ''' <param name="code">Código del Riesgo Profesional</param>
    ''' <returns>Riesgo Profesional</returns>
    ''' <remarks></remarks>
    Public Function GetProfessionalRisk(code As String) As ProfessionalRisk Implements IProfessionalRiskAdminService.GetProfessionalRisk
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code Vacio")
        End If
        Try
            Return _professionalRiskRepository.GetProfessionalRisk(code)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ProfessionalRisk()
        End Try
    End Function

    ''' <summary>
    ''' Lista un Riesgo Profesional
    ''' </summary>
    ''' <returns>Lista de Riesgos Profesionales</returns>
    ''' <remarks></remarks>
    Public Function ListAllProfessionalRisk() As List(Of ProfessionalRisk) Implements IProfessionalRiskAdminService.ListAllProfessionalRisk
        Try
            Return _professionalRiskRepository.ListAllProfessionalRisk()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Riesgo Profesional</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveProfessionalRisk(professionalRisk As ProfessionalRisk, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IProfessionalRiskAdminService.SaveProfessionalRisk
        If professionalRisk Is Nothing Then
            Throw New ArgumentNullException("professionalRisk Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _professionalRiskRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of ProfessionalRisk)
            Dim auxProfessionalRisk As ProfessionalRisk = Nothing
            Dim status As Integer

            If professionalRisk.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                professionalRisk.ModificationUser = audit.CodeUser
                professionalRisk.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                auxProfessionalRisk = _professionalRiskRepository.GetProfessionalRisk(professionalRisk.Code, False)
            Else
                professionalRisk.CreationUser = audit.CodeUser
                professionalRisk.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            'Valido si se va a guardar o a eliminar
            _professionalRiskRepository.SaveEntity(professionalRisk)
            UnitOfWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of ProfessionalRisk)(professionalRisk, audit, status, auxProfessionalRisk)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Elimina un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Riesgo Profesional</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteProfessionalRisk(professionalRisk As ProfessionalRisk, audit As AuditMessage) As Domain.Base.Entities.ActionMessageResult(Of ProfessionalRisk) Implements IProfessionalRiskAdminService.DeleteProfessionalRisk
        Dim result As New ActionMessageResult(Of ProfessionalRisk)
        result.StateResult = True
        If professionalRisk Is Nothing Then
            Throw New ArgumentNullException("professionalRisk Vacio")
        End If
        Dim UnitOfWork As IUnitWork = _professionalRiskRepository.UnitWork
        Try
            _professionalRiskRepository.DeleteEntity(professionalRisk)
            UnitOfWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute(professionalRisk.GetType.Name, audit.Functional, professionalRisk.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of ProfessionalRisk)(professionalRisk, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            Return result
        Catch ex As DbUpdateException
            UnitOfWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", professionalRisk.Code))
            Return result
        Catch ex As Exception
            UnitOfWork.RollbackChanges()
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
            _professionalRiskRepository = Nothing
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
