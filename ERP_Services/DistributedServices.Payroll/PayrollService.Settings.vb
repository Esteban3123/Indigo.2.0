'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina los Parámetros de Nómina
    ''' </summary>
    ''' <param name="PayrollSettings">PayrollSettings</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteSettingsPayroll(PayrollSettings As PayrollSettings, session As SessionValues) As Boolean Implements IPayrollSettings.DeleteSettingsPayroll
        Using settingsAdminService As IPayrollSettingsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSettingsAdminService)()
            Return settingsAdminService.DeletePayrollSettings(PayrollSettings, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Obtiene los Parámetros de Nómina
    ''' </summary>
    ''' <param name="session">session</param>
    ''' <returns>PayrollSettings</returns>
    ''' <remarks></remarks>
    Public Function GetSettingsPayroll(session As SessionValues) As PayrollSettings Implements IPayrollSettings.GetSettingsPayroll
        Using settingsAdminService As IPayrollSettingsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSettingsAdminService)()
            Return settingsAdminService.GetPayrollSettings()
        End Using
    End Function

    ''' <summary>
    ''' Almacena los Parámetros de Nómina
    ''' </summary>
    ''' <param name="PayrollSettings">PayrollSettings</param>
    ''' <param name="session"></param>
    ''' <returns>ActionResult(Of PayrollSettings)</returns>
    ''' <remarks></remarks>
    Public Function SaveSettingsPayroll(PayrollSettings As PayrollSettings, session As SessionValues) As ActionResult(Of PayrollSettings) Implements IPayrollSettings.SaveSettingsPayroll
        Using settingsAdminService As IPayrollSettingsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollSettingsAdminService)()
            Return settingsAdminService.SavePayrollSettings(PayrollSettings, session.AuditMessageWcf)
        End Using
    End Function
End Class
