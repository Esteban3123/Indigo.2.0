'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base

Partial Class PayrollService
    ''' <summary>
    ''' Funcion para eliminar una clase de convenios
    ''' </summary>
    ''' <param name="AgreementsC">Obj. convenio a eliminar</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    Public Function DeleteAgreementsC(AgreementsC As Domain.Payroll.Entities.AgreementsC, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IPayrollAgreements.DeleteAgreementsC
        Using AgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsAdminService)()
            Return AgreementsAdminService.DeleteAgreementsC(AgreementsC, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="audit">Objeto Inf. Auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsC(consecutive As String, session As SessionValues) As Domain.Payroll.Entities.AgreementsC Implements IPayrollAgreements.GetAgreementsC
        Using AgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsAdminService)()
            Return AgreementsAdminService.GetAgreementsC(consecutive, session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Lista de convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAgreementsC(session As SessionValues) As List(Of Domain.Payroll.Entities.AgreementsC) Implements IPayrollAgreements.ListAgreementsC
        Using AgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsAdminService)()
            Return AgreementsAdminService.ListAgreementsC(session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Lista de empleados
    ''' </summary>
    ''' <param name="audit">Objeto Inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEmployeeAgreements(session As SessionValues) As List(Of Domain.Payroll.Entities.Employee) Implements IPayrollAgreements.ListEmployeeAgreements
        Using AgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsAdminService)()
            Return AgreementsAdminService.ListEmployee(session.AuditMessageWcf)
        End Using
    End Function
    ''' <summary>
    ''' Funcion para guardar un convenio
    ''' </summary>
    ''' <param name="AgreementsC">Objeto convenio</param>
    ''' <param name="audit">Objeto inf. auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveAgreementsC(AgreementsC As Domain.Payroll.Entities.AgreementsC, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.AgreementsC) Implements IPayrollAgreements.SaveAgreementsC
        Using AgreementsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsAdminService)()
            Return AgreementsAdminService.SaveAgreementsC(AgreementsC, session.AuditMessageWcf)
        End Using
    End Function
End Class
