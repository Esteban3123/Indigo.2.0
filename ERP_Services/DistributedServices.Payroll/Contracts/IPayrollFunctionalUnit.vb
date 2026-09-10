Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 19-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

<ServiceContract()> _
Public Interface IPayrollFunctionalUnit

    ''' <summary>
    ''' Lista todos las unidades funcionales
    ''' </summary>
    ''' <returns>Lista las unidades funcionales</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllFunctionalUnit(session As SessionValues) As List(Of FunctionalUnit)

    ''' <summary>
    ''' Obtiene una unidad funcional
    ''' </summary>
    ''' <param name="code">Codigo de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFunctionalUnit(ByVal code As String, session As SessionValues) As FunctionalUnit

    ''' <summary>
    ''' Obtiene una unidad funcional por id
    ''' </summary>
    ''' <param name="id">Id de la unidad funcional</param>
    ''' <returns>Unidad funcional</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFunctionalUnitById(ByVal id As String, session As SessionValues) As FunctionalUnit

    ''' <summary>
    ''' Graba o actualiza una unidad funcional
    ''' </summary>
    ''' <param name="functionalUnit">unidad funcional a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFunctionalUnit(ByVal functionalUnit As FunctionalUnit, session As SessionValues, idSequence As Long) As ActionResult(Of Domain.Payroll.Entities.FunctionalUnit)

    ''' <summary>
    ''' Elimina una unidad funcional
    ''' </summary>
    ''' <param name="FunctionalUnit">Unidad Funcional</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteFunctionalUnit(ByVal FunctionalUnit As FunctionalUnit, session As SessionValues) As ActionMessageResult(Of FunctionalUnit)
    <OperationContract()>
    Function UpdateStateFunctionalUnit(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Payroll.Entities.FunctionalUnit)

End Interface
