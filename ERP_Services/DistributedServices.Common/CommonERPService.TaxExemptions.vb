'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Alarcon
' Created          : 26-08-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService

    Public Function GetTaxExemptions(code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TaxExemptions) Implements ICommonERPTaxExemptions.GetTaxExemptions
        Using TaxExemptionsAdminService As ITaxExemptionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITaxExemptionsAdminService)()
            Return TaxExemptionsAdminService.GetTaxExemptions(code, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ChangeStateTaxExemptions(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TaxExemptions) Implements ICommonERPTaxExemptions.ChangeStateTaxExemptions
        Using TaxExemptionsAdminService As ITaxExemptionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITaxExemptionsAdminService)()
            Return TaxExemptionsAdminService.ChangeStateTaxExemptions(code, state, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetTaxExemptionsById(id As Integer, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TaxExemptions) Implements ICommonERPTaxExemptions.GetTaxExemptionsById
        Using TaxExemptionsAdminService As ITaxExemptionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITaxExemptionsAdminService)()
            Return TaxExemptionsAdminService.GetTaxExemptionsyById(id, session.AuditMessageWcf)
        End Using
    End Function

    Public Function SaveTaxExemptions(TaxExemptions As Domain.Entities.TaxExemptions, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.TaxExemptions) Implements ICommonERPTaxExemptions.SaveTaxExemptions
        Using TaxExemptionsAdminService As ITaxExemptionsAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITaxExemptionsAdminService)()
            Return TaxExemptionsAdminService.SaveTaxExemptions(TaxExemptions, session.AuditMessageWcf)
        End Using
    End Function

End Class
