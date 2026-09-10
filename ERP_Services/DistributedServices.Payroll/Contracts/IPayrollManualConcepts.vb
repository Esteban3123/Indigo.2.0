'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollManualConcepts

    ''' <summary>
    ''' Graba o Actualiza un concepto manual
    ''' </summary>
    ''' <param name="manualConcept">Concepto Manual</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveManualConcept(ByVal manualConcept As ManualConcepts, session As SessionValues) As ActionMessageResult

    ''' <summary>
    ''' Funcion para obtener un concepto manual
    ''' </summary>
    ''' <param name="Consecutive">Numero de consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetManualConcepts(ByVal Consecutive As Integer, session As SessionValues) As ManualConcepts

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_date">fecha que se quiere iniciar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDates As List(Of Date), ProcessType As Byte, session As SessionValues, Optional _otherDate As Date = Nothing) As ManualConcepts

    ''' <summary>
    ''' Devuelve si ya hay un concepto manual ya registrado hasta fin de contrato, por concepto y por numero de contraro
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetManualConceptsByConceptAndEndContractTrue(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte, session As SessionValues) As Boolean

    <OperationContract()> _
    Function ValidateManualConceptsMassive(data As List(Of ImportFileRow), session As SessionValues) As List(Of SP_ValidateMassiveManualConcepts_Result)

    <OperationContract()> _
    Function GetManualConceptsMassive(pData As List(Of ImportFileRow), session As SessionValues) As List(Of SP_GetMassiveManualConcepts_Result)

    <OperationContract()> _
    Sub SaveManualConceptsMassive(pData As List(Of ImportFileRow), session As SessionValues)

End Interface
