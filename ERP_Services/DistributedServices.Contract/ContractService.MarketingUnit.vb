'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateMarketingUnit(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit) Implements IContractMarketingUnit.ChangeStateMarketingUnit
        Using service As IMarketingUnitAdminService = Container.Current.Resolve(Of IMarketingUnitAdminService)()
            Return service.ChangeStateMarketingUnit(code, state, audit)
        End Using
        'Return Me._marketingUnitAdminService.ChangeStateMarketingUnit(code, state, audit)
    End Function

    Public Function DeleteMarketingUnit(MarketingUnit As Domain.Entities.MarketingUnit, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractMarketingUnit.DeleteMarketingUnit
        Using service As IMarketingUnitAdminService = Container.Current.Resolve(Of IMarketingUnitAdminService)()
            Return service.DeleteMarketingUnit(MarketingUnit, audit)
        End Using
        'Return Me._marketingUnitAdminService.DeleteMarketingUnit(MarketingUnit, audit)
    End Function

    Public Function GetMarketingUnit(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit) Implements IContractMarketingUnit.GetMarketingUnit
        Using service As IMarketingUnitAdminService = Container.Current.Resolve(Of IMarketingUnitAdminService)()
            Return service.GetMarketingUnit(code, audit)
        End Using
        'Return Me._marketingUnitAdminService.GetMarketingUnit(code, audit)
    End Function

    Public Function GetMarketingUnitById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit) Implements IContractMarketingUnit.GetMarketingUnitById
        Using service As IMarketingUnitAdminService = Container.Current.Resolve(Of IMarketingUnitAdminService)()
            Return service.GetMarketingUnitById(id, audit)
        End Using
        'Return Me._marketingUnitAdminService.GetMarketingUnitById(id, audit)
    End Function

    Public Function SaveMarketingUnit(MarketingUnit As Domain.Entities.MarketingUnit, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MarketingUnit) Implements IContractMarketingUnit.SaveMarketingUnit
        Using service As IMarketingUnitAdminService = Container.Current.Resolve(Of IMarketingUnitAdminService)()
            Return service.SaveMarketingUnit(MarketingUnit, audit, idSequense)
        End Using
        'Return Me._marketingUnitAdminService.SaveMarketingUnit(MarketingUnit, audit, idSequense)
    End Function

End Class
