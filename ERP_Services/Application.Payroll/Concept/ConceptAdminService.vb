'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
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

Public Class ConceptAdminService
    Implements IConceptAdminService


    'Repositorio de Tipos de contratos
    Private _ConceptRepository As IConceptRepository

    ''' <summary>
    ''' inicia el repositorio de Conceptos
    ''' </summary>
    ''' <param name="conceptRepository">Repositorio de Conceptos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal conceptRepository As IConceptRepository)
        If (conceptRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de Conceptos vacio")
        End If
        _ConceptRepository = conceptRepository
    End Sub

    ''' <summary>
    ''' Obtiene un Concepto
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    Public Function GetConcept(code As String) As Concept Implements IConceptAdminService.GetConcept
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Codigo vacio")
        End If
        Try
            Return _ConceptRepository.GetConcept(code)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New Concept()
        End Try
    End Function

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Public Function ListAllConcept() As List(Of Concept) Implements IConceptAdminService.ListAllConcept
        Try

            Return _ConceptRepository.ListAllConcept()

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Almacena o Actualiza un Concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveConcept(concept As Concept, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IConceptAdminService.SaveConcept
        If concept Is Nothing Then
            Throw New ArgumentNullException("Concepto vacio")
        End If
        Dim unitWork As IUnitWork = _ConceptRepository.UnitWork
        Try

            Dim auditProcess As IndigoAuditSimpleEntity(Of Concept)
            Dim AuxConcept As Concept = Nothing
            Dim status As Integer

            If concept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                concept.ModificationUser = audit.CodeUser
                concept.ModificationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Update
                AuxConcept = _ConceptRepository.GetConcept(concept.Code, False)
            Else
                concept.CreationUser = audit.CodeUser
                concept.CreationDate = Date.Now()
                status = Infrastructure.CrossCutting.Audit.Actions.Insert
            End If

            If concept.ConceptAccountingStructure IsNot Nothing And concept.ConceptAccountingStructure.Count > 0 Then
                For Each objConceptoAccounting As ConceptAccountingStructure In concept.ConceptAccountingStructure
                    If objConceptoAccounting.ChangeTracker.State = ObjectState.Modified Then
                        objConceptoAccounting.ModificationUserId = audit.IdUser
                        objConceptoAccounting.ModifiedDate = Date.Now
                    End If

                    If objConceptoAccounting.ChangeTracker.State = ObjectState.Added Then
                        objConceptoAccounting.CreationUserId = audit.IdUser
                        objConceptoAccounting.CreationDate = Date.Now
                    End If
                Next
            End If


            'Valido si se va a guardar o a eliminar
            _ConceptRepository.SaveEntity(concept)
            unitWork.Commit()
            auditProcess = New IndigoAuditSimpleEntity(Of Concept)(concept, audit, status, AuxConcept)
            'IndigoAuditSimpleEntity(Of Concept).Execute(concept, audit, Infrastructure.CrossCutting.Audit.Actions.Update, audit.Company, AuxConcept)
            auditProcess.Execute()
            Return True
        Catch ex As Exception
            unitWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene una lista de Concepto dependiendo de la lista de class Concept, para el formulario de ScheduleTemplate
    ''' </summary>
    ''' <param name="listClassConcept">Lista de codigos de clase de concepto</param>
    ''' <returns>Lista de Concepto</returns>
    ''' <remarks></remarks>
    Public Function GetConceptByConceptClass(listClassConcept As List(Of String)) As List(Of Concept) Implements IConceptAdminService.GetConceptByConceptClass
        Try
            Return _ConceptRepository.GetConceptByConceptClass(listClassConcept)

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Eliminar un Concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteConcept(concept As Concept, audit As AuditMessage) As ActionMessageResult(Of Concept) Implements IConceptAdminService.DeleteConcept
        Dim result As New ActionMessageResult(Of Concept)
        result.StateResult = True
        If concept Is Nothing Then
            Throw New ArgumentNullException("Concepto vacio")
        End If
        Dim unitWork As IUnitWork = _ConceptRepository.UnitWork
        Try
            _ConceptRepository.SaveEntity(concept)
            _ConceptRepository.DeleteEntity(concept)
            unitWork.Commit()

            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("Concept", audit.Functional, concept.Id, audit.NameUser, audit.CodeUser, audit.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, audit.Company, audit.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of Concept)(concept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()

            'IndigoAuditSimpleEntity(Of Concept).Execute(concept, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, concept)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.MessageResult.Add(New MessageResult("c-0000", concept.Code))
            Return result
        Catch ex As Exception
            unitWork.RollbackChanges()
            result.StateResult = False
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return result
        End Try
    End Function

    Public Function ChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IConceptAdminService.ChangeState
        Dim ConceptAccordingCode As Concept = _ConceptRepository.GetConcept(code)
        ConceptAccordingCode.State = state
        Return SaveConcept(ConceptAccordingCode, audit)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ConceptRepository = Nothing
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
