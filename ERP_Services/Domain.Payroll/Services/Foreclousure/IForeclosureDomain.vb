'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 10-10-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Common.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports System.Text

Public Interface IForeclosureDomain
    Inherits IDisposable

    ''' <summary>
    ''' Calcula las Cuotas de los Embargos
    ''' </summary>
    ''' <param name="ForeclousureEmployee">Objeto Embargos Empleado</param>
    ''' <param name="payrollDateLiquidated">Fecha Nómina</param>
    ''' <param name="conceptValue">Valor del Concepto</param>
    ''' <returns>Lista de Convenios</returns>
    ''' <remarks></remarks>
    Function ForeclousureCalculate(ForeclousureEmployee As List(Of Foreclousure), payrollDateLiquidated As Date, conceptValue As Double) As List(Of Foreclousure)

    ''' <summary>
    ''' Función para crear archivo plano de Embargos
    ''' </summary>
    ''' <param name="PayrollLiquidationDetail">Objeto Detalles de Liquidación</param>
    ''' <returns>StringBuilder</returns>
    Function GenerateArchive(PayrollLiquidationDetail As List(Of LiquidationDetail), Company As Company, ListForeclousure As List(Of Foreclousure)) As ActionMessageResult(Of StringBuilder)
End Interface
