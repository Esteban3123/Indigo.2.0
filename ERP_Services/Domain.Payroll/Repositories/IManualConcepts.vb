'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IManualConcepts
    Inherits IRepository(Of ManualConcepts)

    ''' <summary>
    ''' Funcion para obtener un concepto manual
    ''' </summary>
    ''' <param name="Consecutive">Numero de consecutivo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConcepts(ByVal Consecutive As Integer) As ManualConcepts

    ''' <summary>
    ''' Función que obtiene Los conceptos manuales por Número de Contrato, Fecha del Pago de la Nómina y que su estado sea ACTIVO
    ''' </summary>
    ''' <param name="ContractNumber">Número del Contrato</param>
    ''' <param name="PayrollInitialDate">Fecha Inicio Nómina</param>
    ''' <param name="PayrollEndDate">Fecha Fin Nómina</param>
    ''' <returns>Manual Concept</returns>
    ''' <remarks></remarks>
    Function GetManualConceptsByContractNumberPayrollDate(ByVal ContractNumber As Integer, ByVal PayrollInitialDate As Date, PayrollEndDate As Date, State As Byte, ProcessType As Byte, Optional FlagSave As Boolean = False) As List(Of ManualConcepts)

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_listDates">fecha que se quiere iniciar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDates As List(Of Date), ProcessType As Byte, Optional _otherDate As Date = Nothing) As ManualConcepts

    ''' <summary>
    ''' Devuelve si ya hay un concepto manual ya registrado hasta fin de contrato, por concepto y por numero de contraro
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConceptsByConceptAndEndContractTrue(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte) As Boolean

    ''' <summary>
    ''' Función que devuelve un listado de conceptos manuales por Empleado y por Fecha Inicial. Creada inicialmente para RETROACTIVOS
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="InitialDate">Fecha Inicial</param>
    ''' <param name="State">Estado</param>
    ''' <param name="ProcessType">Tipo de Proceso (1 - Nómina, 2 - Retroactivo)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetManualConceptsByEmployeeIdInitialDate(ByVal EmployeeId As Integer, ByVal InitialDate As Date, State As Byte, ProcessType As Byte) As List(Of ManualConcepts)

    ''' <summary>
    ''' Función que obtiene el listado de Conceptos Manuales por Id de Contrato
    ''' </summary>
    ''' <param name="IdContract">IdContract</param>
    ''' <returns> List(Of ManualConcepts)</returns>
    Function GetManualConceptsContractId(ByVal IdContract As Integer) As List(Of ManualConcepts)

    ''' <summary>
    ''' Función para los Conceptos Manuales en Liquidación de Contrato
    ''' </summary>
    ''' <param name="IdEmployee"></param>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    Function GetManualConceptsByLiquidationContract(IdEmployee As Integer, Status As Byte) As List(Of ManualConcepts)

    Function ValidateManualConceptsMassive(pXMLObj As String) As List(Of SP_ValidateMassiveManualConcepts_Result)

    Function GetManualConceptsMassive(pXMLObj As String) As List(Of SP_GetMassiveManualConcepts_Result)

    Sub SaveManualConceptsMassive(pXMLObj As String)

End Interface