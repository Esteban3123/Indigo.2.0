'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Implements IPayrollRetention

    ''' <summary>
    ''' Eliminar una Retención
    ''' </summary>
    ''' <param name="retention">Retención</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteRetention(retention As Domain.Payroll.Entities.Retention, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.Retention) Implements IPayrollRetention.DeleteRetention
        Using retentionAdminService As IRetentionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetentionAdminService)()
            Return retentionAdminService.DeleteRetention(retention, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtener una Retención
    ''' </summary>
    ''' <param name="code">Código de la Retención</param>
    ''' <returns>Retención</returns>
    ''' <remarks></remarks>
    Public Function GetRetention(code As String, session As SessionValues) As Domain.Payroll.Entities.Retention Implements IPayrollRetention.GetRetention
        Using retentionAdminService As IRetentionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetentionAdminService)()
            Return retentionAdminService.GetRetention(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista Todas las Retenciones
    ''' </summary>
    ''' <returns>Retenciones</returns>
    ''' <remarks></remarks>
    Public Function ListAllRetention(session As SessionValues) As List(Of Domain.Payroll.Entities.Retention) Implements IPayrollRetention.ListAllRetention
        Using retentionAdminService As IRetentionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetentionAdminService)()
            Return retentionAdminService.ListAllRetention()
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza Retenciones
    ''' </summary>
    ''' <param name="retention">Retención</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveRetention(retention As Domain.Payroll.Entities.Retention, session As SessionValues) As Boolean Implements IPayrollRetention.SaveRetention
        Using retentionAdminService As IRetentionAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IRetentionAdminService)()
            Return retentionAdminService.SaveRetention(retention, session.AuditMessageWcf)
        End Using
    End Function
End Class
