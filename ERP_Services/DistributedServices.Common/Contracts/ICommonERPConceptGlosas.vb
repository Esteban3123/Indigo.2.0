'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Rafael Eduardo Patiño
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPConceptGlosas

    ''' <summary>
    ''' Funcion para cargar los conceptos de glosas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListConceptGlosas(session As SessionValues) As List(Of ConceptGlosas)

    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según tipo
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    <OperationContract>
    Function ListConceptGlosaByType(type As String, session As SessionValues) As List(Of ConceptGlosas)
    ''' <summary>
    ''' Funcion para cargar todos los conceptos glosa según lista de tipos
    ''' </summary>
    ''' <returns>Lista de todos los conceptos de glosa</returns>
    <OperationContract>
    Function ListConceptGlosaByListTypes(types As List(Of String), session As SessionValues) As List(Of ConceptGlosas)


End Interface
