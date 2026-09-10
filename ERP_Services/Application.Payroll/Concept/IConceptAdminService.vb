'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IConceptAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    Function ListAllConcept() As List(Of Concept)

    ''' <summary>
    ''' Elimina un Concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteConcept(ByVal concept As Concept, ByVal audit As AuditMessage) As ActionMessageResult(Of Concept)

    ''' <summary>
    ''' Almacena o Actualiza un Concepto
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveConcept(ByVal concept As Concept, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un Concepto
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    Function GetConcept(ByVal code As String) As Concept

    ''' <summary>
    ''' Obtiene una lista de Concepto dependiendo de la lista de class Concept, para el formulario de ScheduleTemplate
    ''' </summary>
    ''' <param name="listClassConcept">Lista de codigos de clase de concepto</param>
    ''' <returns>Lista de Concepto</returns>
    ''' <remarks></remarks>
    Function GetConceptByConceptClass(ByVal listClassConcept As List(Of String)) As List(Of Concept)

    ''' <summary>
    ''' Cambia el estado  del concepto según el código
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    Function ChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean


End Interface
