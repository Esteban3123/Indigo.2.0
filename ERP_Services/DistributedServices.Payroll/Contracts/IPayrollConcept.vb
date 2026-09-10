'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollConcept

    ''' <summary>
    ''' Lista todos los Conceptos
    ''' </summary>
    ''' <returns>Conceptos</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllConcept(session As SessionValues) As List(Of Concept)

    ''' <summary>
    ''' Eliminar Conceptos
    ''' </summary>
    ''' <param name="concept">Conceptos</param>
    ''' <param name="audit">Objetos Auditorias</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteConcept(ByVal concept As Concept, session As SessionValues) As ActionMessageResult(Of Concept)

    ''' <summary>
    ''' Almacena o Actualiza Conceptos
    ''' </summary>
    ''' <param name="concept">Concepto</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveConcept(ByVal concept As Concept, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un Concept
    ''' </summary>
    ''' <param name="code">Código del Concepto</param>
    ''' <returns>Concepto</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetConcept(ByVal code As String, session As SessionValues) As Concept

    ''' <summary>
    ''' Obtiene una lista de Concepto dependiendo de la lista de class Concept, para el formulario de ScheduleTemplate
    ''' </summary>
    ''' <param name="listClassConcept">Lista de codigos de clase de concepto</param>
    ''' <returns>Lista de Concepto</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetConceptByConceptClass(ByVal listClassConcept As List(Of String), session As SessionValues) As List(Of Concept)

    ''' <summary>
    ''' Cambia el estado  del concepto según el código
    ''' </summary>
    ''' <param name="code">Código del concepto</param>
    ''' <returns>Cargo</returns>
    <OperationContract()> _
    Function ChangeStateConcept(Code As String, state As Boolean, session As SessionValues) As Boolean



End Interface
