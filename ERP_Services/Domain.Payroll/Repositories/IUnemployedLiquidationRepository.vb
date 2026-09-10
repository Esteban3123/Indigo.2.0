'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IUnemployedLiquidationRepository
    Inherits IRepository(Of UnemployedLiquidation)

    ''' <summary>
    ''' Devuelve las liquidaciones de cesantías que tenga determinado empleado en determinado periodo de tiempo
    ''' </summary>
    ''' <param name="EmployeeId">id del empleado</param>
    ''' <param name="InitialDate">fecha inicio</param>
    ''' <param name="EndingDate">fecha fin</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUnemployedLiquidationEmployeeByDate(ByVal EmployeeId As Integer, ByVal InitialDate As Date, ByVal EndingDate As Date) As List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Función que devuelve la liquidación de Cesantia de un Contrato con la Fecha de Pago del Interés de Cesantía
    ''' </summary>
    ''' <param name="ContractId">Id del Contrato</param>
    ''' <param name="InterestPayDay">Fecha Pago Interés</param>
    ''' <returns>Objeto Cesantia</returns>
    ''' <remarks></remarks>
    Function GetUnemployedLiquidationByContractIdInterestPayDay(ByVal ContractId As String, InterestPayDay As Date) As UnemployedLiquidation

    Function GetUnemploymentLiquidationByContractId(ByVal ContractId As String) As List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación por Clase de Concepto y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="conceptClass">Clase del Concepto</param>
    ''' <returns></returns>
    Function GetLiquidationDetailByContractIdConceptClass(ByVal Year As Integer, InitialContractNumber As Integer, conceptClass As String) As List(Of LiquidationDetail)

    ''' <summary>
    ''' Función para cargar los detalles de Liquidación si Afecta para Cesantias y Año
    ''' </summary>
    ''' <param name="Year">Año</param>
    ''' <param name="InitialContractNumber">Contrato Inicial</param>
    ''' <param name="AffectUnemployement">Afecta Cesantías</param>
    ''' <returns></returns>
    Function GetLiquidationDetailByContractIdConceptAffectUnemployement(ByVal Year As Integer, InitialContractNumber As Integer, AffectUnemployement As Boolean) As List(Of LiquidationDetail)

    Function SP_GenerateJournalVouchersUnemployment(GroupId As Integer, PeriodInitialDate As Date, PeriodEndDate As Date, codeUser As String) As List(Of SP_GenerateJournalVouchersUnemployment_Result)

    Function GetunemploymentLiquidationEmployee(EmployeeId As Integer, PeriodEndDate As Date, Status As Boolean) As List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Función que devuelve las liquidaciones de cesantías por grupo, fecha y estado
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <param name="PeriodEndDate">Fecha de fin del periodo</param>
    ''' <param name="StatusValue">Estado de la liquidación (0=False, 1=True)</param>
    ''' <returns>Lista de liquidaciones de cesantías del grupo</returns>
    ''' <remarks></remarks>
    Function GetUnemployedLiquidationByGroup(GroupId As Integer, PeriodEndDate As Date, StatusValue As Boolean) As List(Of UnemployedLiquidation)

End Interface