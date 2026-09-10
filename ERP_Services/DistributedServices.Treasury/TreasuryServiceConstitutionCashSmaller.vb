'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class TreasuryService
    Implements ITreasuryServiceConstitutionCashSmaller

    Public Function ConfirmConstitutionCashSmaller(ConstitutionCashSmaller As ConstitutionCashSmaller, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller) Implements ITreasuryServiceConstitutionCashSmaller.ConfirmConstitutionCashSmaller
        Using service As IConstitutionCashSmallerAdminService = Container.Current.Resolve(Of IConstitutionCashSmallerAdminService)()
            Return service.ConfirmConstitutionCashSmaller(ConstitutionCashSmaller, audit, idSequence)
        End Using
        'Return Me._constitutionCashSmallerAdminService.ConfirmConstitutionCashSmaller(ConstitutionCashSmaller, audit, idSequence)
    End Function

    Public Function GetConstitutionCashSmaller(code As String, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller) Implements ITreasuryServiceConstitutionCashSmaller.GetConstitutionCashSmaller
        Using service As IConstitutionCashSmallerAdminService = Container.Current.Resolve(Of IConstitutionCashSmallerAdminService)()
            Return service.GetConstitutionCashSmaller(code, audit)
        End Using
        'Return Me._constitutionCashSmallerAdminService.GetConstitutionCashSmaller(code, audit)
    End Function

    Public Function GetConstitutionCashSmallerById(id As Integer, tracking As Boolean, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller) Implements ITreasuryServiceConstitutionCashSmaller.GetConstitutionCashSmallerById
        Using service As IConstitutionCashSmallerAdminService = Container.Current.Resolve(Of IConstitutionCashSmallerAdminService)()
            Return service.GetConstitutionCashSmallerById(id, tracking, audit)
        End Using
        'Return Me._constitutionCashSmallerAdminService.GetConstitutionCashSmallerById(id, tracking, audit)
    End Function

    Public Function SaveConstitutionCashSmaller(ConstitutionCashSmaller As ConstitutionCashSmaller, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ConstitutionCashSmaller) Implements ITreasuryServiceConstitutionCashSmaller.SaveConstitutionCashSmaller
        Using service As IConstitutionCashSmallerAdminService = Container.Current.Resolve(Of IConstitutionCashSmallerAdminService)()
            Return service.SaveConstitutionCashSmaller(ConstitutionCashSmaller, audit, idSequence)
        End Using
        'Return Me._constitutionCashSmallerAdminService.SaveConstitutionCashSmaller(ConstitutionCashSmaller, audit, idSequence)
    End Function

End Class