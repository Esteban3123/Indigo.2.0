'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Domain.Payroll.Entities

Partial Class PayrollService

    Implements IPayrollNationalSavingsFund

    ''' <summary>
    ''' Función en la cual se genera el archivo plano del Fondo Nacional del Ahorro por Fecha de Liquidación
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación</param>
    ''' <param name="CompanyId">Id Empresa</param>
    ''' <param name="session">Variable de Sesión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateNationalSavingsFundFile(PayrollDateLiquidated As Date, CompanyId As Integer, session As SessionValues) As ActionMessageResult(Of StringBuilder) Implements IPayrollNationalSavingsFund.GenerateNationalSavingsFundFile
        Using NationalSavingFundFile As INationalSavingsFundAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IFileForeclousureAdminService)()
            Return NationalSavingFundFile.GenerateNationalSavingsFundFile(PayrollDateLiquidated, CompanyId, session)
        End Using
    End Function
End Class
