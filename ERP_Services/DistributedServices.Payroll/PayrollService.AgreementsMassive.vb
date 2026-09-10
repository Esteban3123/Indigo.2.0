'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08/09/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Domain.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Importa el archivo excel y valida la info
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_ImportFileAgreementsMassive(data As List(Of ImportFileRow), session As SessionValues) As ActionResult(Of List(Of SP_ImportFileAgreementsC_Result)) Implements IPayrollIAgreementsMassive.SP_ImportFileAgreementsMassive
        Using adminService As IAgreementsMassiveAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsMassiveAdminService)()
            Return adminService.SP_ImportFileAgreementsMassive(data)
        End Using
    End Function

    Public Function SP_SaveAgreementsMassive(ListInfo As List(Of SP_ImportFileAgreementsC_Result), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IPayrollIAgreementsMassive.SP_SaveAgreementsMassive
        Using adminService As IAgreementsMassiveAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IAgreementsMassiveAdminService)()
            Return adminService.SP_SaveAgreementsMassive(ListInfo, session.AuditMessageWcf)
        End Using
    End Function

End Class
