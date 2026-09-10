'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IPayrollNationalSavingsFund

    ''' <summary>
    ''' Función en la cual se genera el archivo plano del Fondo Nacional del Ahorro por Fecha de Liquidación
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <param name="CompanyId">Id Empresa</param>
    ''' <param name="session">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GenerateNationalSavingsFundFile(PayrollDateLiquidated As Date, ByVal CompanyId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder)
End Interface
