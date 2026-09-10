Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity


Partial Class TreasuryService
    Implements ITreasuryServiceCheckCashingControl

    Public Function SaveCheckCashingControl(ByVal checkCashingControl As Domain.Entities.CheckCashingControl, ByVal audit As AuditMessage) As ActionResult(Of Domain.Entities.CheckCashingControl) Implements ITreasuryServiceCheckCashingControl.SaveCheckCashingControl
        Using service As ICheckCashingControlAdminService = Container.Current.Resolve(Of ICheckCashingControlAdminService)()
            Return service.SaveCheckCashingControl(checkCashingControl, audit)
        End Using
    End Function

    Public Function GetCheckCashingControlById(ByVal id As Integer) As Domain.Entities.CheckCashingControl Implements ITreasuryServiceCheckCashingControl.GetCheckCashingControlById
        Using service As ICheckCashingControlAdminService = Container.Current.Resolve(Of ICheckCashingControlAdminService)()
            Return service.GetCheckCashingControlById(id)
        End Using
    End Function

    Public Function ListCheckCashingControl(ByVal parameters As String, ByVal session As SessionValues) As DataSet Implements ITreasuryServiceCheckCashingControl.ListCheckCashingControl
        Using service As ICheckCashingControlAdminService = Container.Current.Resolve(Of ICheckCashingControlAdminService)()
            Return service.ListCheckCashingControl(parameters, session)
        End Using
    End Function

End Class

