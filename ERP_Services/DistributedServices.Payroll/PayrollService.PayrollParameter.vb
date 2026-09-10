'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 31-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService

    Implements IPayrollParameter

    ''' <summary>
    ''' Elimina una Parametrización de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Parametrización de Nómina</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePayrollParameter(payrollParameter As Domain.Payroll.Entities.PayrollParameter, session As SessionValues) As Boolean Implements IPayrollParameter.DeletePayrollParameter
        Using payrollParameterAdmin As IPayrollParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollParameterAdminService)()
            Return payrollParameterAdmin.DeletePayrollParameter(payrollParameter, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una Parametrización de Nómina
    ''' </summary>
    ''' <param name="groupId">Group ID</param>
    ''' <returns>Parametrización de Nómina</returns>
    ''' <remarks></remarks>
    Public Function GetPayrollParameter(groupId As String, session As SessionValues) As Domain.Payroll.Entities.PayrollParameter Implements IPayrollParameter.GetPayrollParameter
        Using payrollParameterAdmin As IPayrollParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollParameterAdminService)()
            Return payrollParameterAdmin.GetPayrollParameter(groupId)
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las parametrizaciones de Nómina
    ''' </summary>
    ''' <returns>Parametrizaciones de Nómina</returns>
    ''' <remarks></remarks>
    Public Function ListAllPayrollParameter(session As SessionValues) As List(Of Domain.Payroll.Entities.PayrollParameter) Implements IPayrollParameter.ListAllPayrollParameter
        Using payrollParameterAdmin As IPayrollParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollParameterAdminService)()
            Return payrollParameterAdmin.ListAllPayrollParameter()
        End Using
    End Function

    ''' <summary>
    ''' Almacena una Parametrización de Nómina
    ''' </summary>
    ''' <param name="payrollParameter">Parametrización de Nómina</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePayrollParameter(payrollParameter As Domain.Payroll.Entities.PayrollParameter, session As SessionValues) As Boolean Implements IPayrollParameter.SavePayrollParameter
        Using payrollParameterAdmin As IPayrollParameterAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPayrollParameterAdminService)()
            Return payrollParameterAdmin.SavePayrollParameter(payrollParameter, session.AuditMessageWcf)
        End Using
    End Function
End Class
