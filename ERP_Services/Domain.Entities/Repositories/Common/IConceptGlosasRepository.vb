'************************************************************
' Assembly         : Domain.Common
' Author           : Rafael Eduardo Patiño
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Common.Entities
Imports Domain.Base

#End Region

Public Interface IConceptGlosasRepository
    Inherits IRepository(Of ConceptGlosas)

    ''' <summary>
    ''' Funcion para cargar los conceptos de glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConceptGlosas() As List(Of ConceptGlosas)
    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según tipo
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Function ListConceptGlosaByType(type As String) As List(Of ConceptGlosas)
    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según lista de tipos
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    Function ListConceptGlosaByListTypes(types As List(Of String)) As List(Of ConceptGlosas)
    ''' <summary>
    ''' Funcion para retornar lista de Id conceptos que aplican para descuentos de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConceptGlosasWithMedicalFees() As List(Of Integer)

End Interface
