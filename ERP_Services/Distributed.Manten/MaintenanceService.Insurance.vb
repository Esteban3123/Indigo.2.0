Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities

Partial Class MaintanceService

    Public Function DeleteInsurance(Empresa As String, Insurance As Domain.Maintenance.Entities.Insurance, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Boolean Implements IMaintenanceInsurance.DeleteInsurance
        Using InsuranceAdmin As IInsuranceAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInsuranceAdminService)()
            Return InsuranceAdmin.DeleteInsurance(Insurance, audit)
        End Using
    End Function

    Public Function GetInsurance(Empresa As String, codeInsurance As String) As Domain.Maintenance.Entities.Insurance Implements IMaintenanceInsurance.GetInsurance
        Using InsuranceAdmin As IInsuranceAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInsuranceAdminService)()
            Return InsuranceAdmin.GetInsurance(codeInsurance)
        End Using
    End Function

    Public Function ListAllInsurance(Empresa As String) As List(Of Domain.Maintenance.Entities.Insurance) Implements IMaintenanceInsurance.ListAllInsurance
        Using InsuranceAdmin As IInsuranceAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInsuranceAdminService)()
            Return InsuranceAdmin.ListAllInsurance()
        End Using
    End Function
    Public Function SaveInsurance(Empresa As String, Insurance As Domain.Maintenance.Entities.Insurance, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.Insurance) Implements IMaintenanceInsurance.SaveInsurance
        Using InsuranceAdmin As IInsuranceAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IInsuranceAdminService)()
            Return InsuranceAdmin.SaveInsurance(Insurance, audit)
        End Using
    End Function

    Public Function Change_StateInsurance(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Domain.Maintenance.Entities.Insurance) Implements IMaintenanceInsurance.Change_StateInsurance
        Using InsuranceAdmin As IInsuranceAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IInsuranceAdminService)()
            Return InsuranceAdmin.ChangeState(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
