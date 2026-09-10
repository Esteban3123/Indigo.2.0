'***********************************************************************
' Assembly         : Application.Common
' Author           : Rafael Eduardo patiño
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IConceptGlosasAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para cargar los conceptos de glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConceptGlosas() As List(Of ConceptGlosas)
    ''' <summary>
    ''' Funcion para cargar los conceptos glosas por tipo
    ''' </summary>
    Function ListConceptGlosasByType(type As String) As List(Of ConceptGlosas)
    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según lista de tipos
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Function ListConceptGlosaByListTypes(types As List(Of String)) As List(Of ConceptGlosas)
    ''' <summary>
    ''' Guarda la configuracion de concepto para honorarios medicos
    ''' </summary>
    ''' <param name="ListSave"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveConceptXFeesMedical(ListSave As List(Of ConceptGlosas), audit As AuditMessage) As ActionResult

End Interface
