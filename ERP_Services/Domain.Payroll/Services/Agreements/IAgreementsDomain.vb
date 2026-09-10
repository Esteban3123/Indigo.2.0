'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Common.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Public Interface IAgreementsDomain
    Inherits IDisposable

    ''' <summary>
    ''' Calcula las Cuotas de los Convenios
    ''' </summary>
    ''' <param name="AgreementsEmployee">Objeto Conciliaciones Empleado</param>
    ''' <param name="payrollDateLiquidated">Fecha Nómina</param>
    ''' <param name="conceptValue">Valor del Concepto</param>
    ''' <returns>Lista de Convenios</returns>
    ''' <remarks></remarks>
    Function AgreementsCalculate(AgreementsEmployee As List(Of AgreementsC), payrollDateLiquidated As Date, conceptValue As Double) As List(Of AgreementsC)

End Interface
