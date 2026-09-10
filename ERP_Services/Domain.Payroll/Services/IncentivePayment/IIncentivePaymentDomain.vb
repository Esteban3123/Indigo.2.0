'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 14-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Public Interface IIncentivePaymentDomain
    Inherits IDisposable

    ''' <summary>
    ''' función que se utiliza para Calcular Primas
    ''' </summary>
    ''' <param name="employeeLiquidation">Las de Liquidaciones de Empleados (NOTHING SI ES PARA LIQUIDACIÓN DE CONTRATO)</param>
    ''' <param name="NumberOfIncentivePayment">Número de Primas Al Año (Grupo)</param>
    ''' <param name="PaymentType">Tipo de Pago</param>
    ''' <param name="Period">Periodo ( 1 - 2 )</param>
    ''' <param name="IncentiveStarDate">Fecha Inicio Prima</param>
    ''' <param name="IncentiveEndDate">Fecha Fin Prima</param>
    ''' <param name="PayrollNextDate">Fecha Pago Próxima Nómina</param>
    ''' <param name="contract">Contrato (PARA LIQUIDACIÓN DE CONTRATO)</param>
    ''' <param name="employeeList">Lista opcional de empleados a liquidar (patrón nómina con lotes desde Presentation)</param>
    ''' <returns>Action Result Primas</returns>
    ''' <remarks></remarks>
    Function IncentivePaymentCalculate(Group As Group, PaymentType As Char, Period As Char, IncentiveStarDate As Date, IncentiveEndDate As Date, SessionValues As SessionValues, Optional ByVal contract As Contract = Nothing, Optional ExtraLiquidation As Liquidation = Nothing, Optional FlagContractLiquidation As Boolean = False, Optional ByRef ReplaceFormulate As String = "", Optional ByRef ConceptFormulate As String = "", Optional ByVal BasePrimasCesantias As Decimal = 0, Optional employeeList As List(Of Employee) = Nothing) As ActionMessageResult(Of List(Of IncentivePayment))

    ''' <summary>
    ''' Función que se utiliza para Hallas las FEchas Iniciales y Finales de Acuerdo al Periodo y el número de Primas al año
    ''' </summary>
    ''' <param name="NumberIncentivePayment">Número de Primas al año</param>
    ''' <param name="Period">Periodo</param>
    ''' <param name="Year">Año</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInitialEndDatePeriod(ByVal NumberIncentivePayment As Byte, Period As Char, Year As Integer, CompanyType As Integer) As Dictionary(Of String, Date)

    ''' <summary>
    ''' Función que escoge el Banco y arma el archivo correspondiente
    ''' </summary>
    ''' <param name="ListIncentivePayment">Lista de Primas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SelectBankEmployee(ListIncentivePayment As List(Of IncentivePayment), Bank As Bank, BankAccount As String, AccountType As Integer, Company As Company) As ActionMessageResult(Of StringBuilder)


End Interface
