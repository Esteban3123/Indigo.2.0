'***********************************************************************
' Assembly         : Application.Common
' Author           : Rafael Eduardo Patiño
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base
Imports Application.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Core


Public Class ConceptGlosasAdminService
    Implements IConceptGlosasAdminService

    ' Repositorio de concepto glosa
    Private _ConceptGlosaRepository As IConceptGlosasRepository

    Public Sub New(ByVal ConceptGlosaRepository As IConceptGlosasRepository)
        If ConceptGlosaRepository Is Nothing Then
            Throw New ArgumentNullException("ConceptglosasRepository vacio")
        End If
        _ConceptGlosaRepository = ConceptGlosaRepository
    End Sub

    ''' <summary>
    ''' Funcion para cargar los conceptos glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptGlosas() As List(Of ConceptGlosas) Implements IConceptGlosasAdminService.ListConceptGlosas
        Try
            Return _ConceptGlosaRepository.ListConceptGlosas()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para cargar los conceptos glosas por tipo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConceptGlosasByType(type As String) As List(Of ConceptGlosas) Implements IConceptGlosasAdminService.ListConceptGlosasByType
        If String.IsNullOrEmpty(type) Then
            Throw New ArgumentNullException("Tipo vacío")
        End If
        Try
            Return _ConceptGlosaRepository.ListConceptGlosaByType(type)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según lista de tipos
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Public Function ListConceptGlosaByListTypes(types As List(Of String)) As List(Of ConceptGlosas) Implements IConceptGlosasAdminService.ListConceptGlosaByListTypes
        If types.Count = 0 Then
            Throw New ArgumentNullException("Lista de tipos vacía")
        End If
        Try
            Return _ConceptGlosaRepository.ListConceptGlosaByListTypes(types)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function
    ''' <summary>
    ''' Guarda la configuracion de concepto para honorarios medicos
    ''' </summary>
    ''' <param name="ListSave"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveConceptXFeesMedical(ListSave As List(Of ConceptGlosas), audit As AuditMessage) As ActionResult Implements IConceptGlosasAdminService.SaveConceptXFeesMedical
        If ListSave.Count = 0 Then
            Throw New ArgumentNullException("Lista de conceptos Vacio")
        End If
        Dim unitOfWork As IUnitWork = _ConceptGlosaRepository.UnitWork
        Try
            Dim AuxConcept As ConceptGlosas = Nothing
            For Each item As ConceptGlosas In ListSave
                If item.ChangeTracker.State = ObjectState.Modified Then
                    _ConceptGlosaRepository.SaveEntity(item)
                End If
            Next
            'Confirmo la unidad de trabajo
            unitOfWork.Commit()
            Return New ActionResult With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            unitOfWork.RollbackChanges()
            Return New ActionResult With {.StateResult = False, .MessageResult = New List(Of String)({"-999"})}
        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False}
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ConceptGlosaRepository = Nothing
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
