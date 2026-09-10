'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 14-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities

Public Interface IIncentivePaymentDomain

    ''' <summary>
    ''' Función para Calcular las Primas
    ''' </summary>
    ''' <param name="employeeLiquidation">Objeto Liquidación Empleados</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IncentivePaymentCalculate(employeeLiquidation As List(Of Liquidation), NumberOfIncentivePayment As Byte, PaymentType As Char, Period As Char, IncentiveStarDate As Date, IncentiveEndDate As Date, PayrollNextDate As Date) As ActionMessageResult(Of List(Of IncentivePayment))


End Interface
