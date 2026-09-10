Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Public Function DeleteReligiousBeliefs(pReligiousBeliefs As Domain.Payroll.Entities.ReligiousBeliefs, pSession As SessionValues) As ActionResult Implements IPayrollReligiousBeliefs.DeleteReligiousBeliefs
        Using religiousBeliefsAdminService As IReligiousBeliefsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IReligiousBeliefsAdminService)()
            Return religiousBeliefsAdminService.DeleteReligiousBeliefs(pReligiousBeliefs, pSession.AuditMessageWcf)
        End Using
    End Function

    Public Function GetReligiousBeliefs(pCode As String, pTracking As Boolean, pSession As SessionValues) As ActionResult(Of Domain.Payroll.Entities.ReligiousBeliefs) Implements IPayrollReligiousBeliefs.GetReligiousBeliefs
        Using religiousBeliefsAdminService As IReligiousBeliefsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IReligiousBeliefsAdminService)()
            Return religiousBeliefsAdminService.GetReligiousBeliefs(pCode, pTracking, pSession.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveReligiousBeliefs(pReligiousBeliefs As Domain.Payroll.Entities.ReligiousBeliefs, pSession As SessionValues, pIDSequence As Int64) As ActionResult(Of Domain.Payroll.Entities.ReligiousBeliefs) Implements IPayrollReligiousBeliefs.SaveReligiousBeliefs
        Using religiousBeliefsAdminService As IReligiousBeliefsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IReligiousBeliefsAdminService)()
            Return religiousBeliefsAdminService.SaveReligiousBeliefs(pReligiousBeliefs, pSession.AuditMessageWcf, pIDSequence)
        End Using
    End Function

    Public Function GetReligiousBeliefsById(pID As String, pTracking As Boolean, pSession As SessionValues) As ActionResult(Of Domain.Payroll.Entities.ReligiousBeliefs) Implements IPayrollReligiousBeliefs.GetReligiousBeliefsById
        Using religiousBeliefsAdminService As IReligiousBeliefsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IReligiousBeliefsAdminService)()
            Return religiousBeliefsAdminService.GetReligiousBeliefsById(pID, pTracking, pSession.AuditMessageWcf)
        End Using
    End Function

    Public Function ChangeStateReligiousBeliefs(pCode As String, pStatus As Boolean, pSession As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Payroll.Entities.ReligiousBeliefs) Implements IPayrollReligiousBeliefs.ChangeStateReligiousBeliefs
        Using religiousBeliefsAdminService As IReligiousBeliefsAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IReligiousBeliefsAdminService)()
            Return religiousBeliefsAdminService.ChangeState(pCode, pStatus, pSession.AuditMessageWcf)
        End Using
    End Function

End Class
