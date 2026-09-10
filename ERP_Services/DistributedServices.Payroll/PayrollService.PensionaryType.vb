'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    Public Function DeletePensionaryType(pensionaryType As Domain.Payroll.Entities.PensionaryType, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.PensionaryType) Implements IPayrollPensionaryType.DeletePensionaryType
        Using pensionaryTypeAdmin As IPensionaryTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPensionaryTypeAdminService)()
            Return pensionaryTypeAdmin.DeletePensionaryType(pensionaryType, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetPensionaryType(code As String, session As SessionValues) As Domain.Payroll.Entities.PensionaryType Implements IPayrollPensionaryType.GetPensionaryType
        Using pensionaryTypeAdmin As IPensionaryTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPensionaryTypeAdminService)()
            Return pensionaryTypeAdmin.GetPensionaryType(code)
        End Using
    End Function

    Public Function ListAllPensionaryType(session As SessionValues) As List(Of Domain.Payroll.Entities.PensionaryType) Implements IPayrollPensionaryType.ListAllPensionaryType
        Using pensionaryTypeAdmin As IPensionaryTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPensionaryTypeAdminService)()
            Return pensionaryTypeAdmin.ListAllPensionaryType()
        End Using
    End Function

    Public Function SavePensionaryType(pensionaryType As Domain.Payroll.Entities.PensionaryType, session As SessionValues) As Boolean Implements IPayrollPensionaryType.SavePensionaryType
        Using pensionaryTypeAdmin As IPensionaryTypeAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPensionaryTypeAdminService)()
            Return pensionaryTypeAdmin.SavePensionaryType(pensionaryType, session.AuditMessageWcf)
        End Using
    End Function

End Class
