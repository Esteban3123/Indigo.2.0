Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity


Partial Class TreasuryService
    Implements ITreasuryServiceCashFlowReclassification

    Public Function SaveCashFlowReclassification(ByVal CashFlowReclassification As Domain.Entities.CashFlowReclassification, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.CashFlowReclassification) Implements ITreasuryServiceCashFlowReclassification.SaveCashFlowReclassification
        Using service As ICashFlowReclassificationAdminService = Container.Current.Resolve(Of ICashFlowReclassificationAdminService)()
            Return service.SaveCashFlowReclassification(CashFlowReclassification, audit)
        End Using
    End Function

    Public Function GetCashFlowReclassificationById(ByVal id As Integer) As Domain.Entities.CashFlowReclassification Implements ITreasuryServiceCashFlowReclassification.GetCashFlowReclassificationById
        Using service As ICashFlowReclassificationAdminService = Container.Current.Resolve(Of ICashFlowReclassificationAdminService)()
            Return service.GetCashFlowReclassificationById(id)
        End Using
    End Function

End Class

