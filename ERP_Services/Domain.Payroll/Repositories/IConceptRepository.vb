'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IConceptRepository
    Inherits IRepository(Of Concept)

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Function ListAllConcept() As List(Of Concept)

    ''' <summary>
    ''' Obtiene un Concepto
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    Function GetConcept(ByVal code As String, Optional desatach As Boolean = True) As Concept

    ''' <summary>
    ''' Obtiene una lista de Concepto dependiendo de la lista de class Concept, para el formulario de ScheduleTemplate
    ''' </summary>
    ''' <param name="listClassConcept">Lista de codigos de clase de concepto</param>
    ''' <returns>Lista de Concepto</returns>
    ''' <remarks></remarks>
    Function GetConceptByConceptClass(ByVal listClassConcept As List(Of String)) As List(Of Concept)

    ''' <summary>
    ''' Obtiene el concepto por clase
    ''' </summary>
    ''' <param name="conceptClass"></param>
    ''' <returns></returns>
    Function GetConceptByClass(conceptClass As String) As Concept

    Function GetConceptId(ByVal Id As Integer) As Concept

    Function GetConceptIds(ByVal Ids As List(Of Integer)) As List(Of Concept)

    Function GetAdjustmentConceptId(Id As Integer) As Concept

End Interface
