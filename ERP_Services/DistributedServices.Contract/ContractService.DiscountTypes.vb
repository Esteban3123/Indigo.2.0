'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Angi Camila Duran Vargas
' Created          : 15/03/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class ContractService


    Public Function DeleteDiscountTypes(DiscountTypes As Domain.Entities.DiscountTypes, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractDiscountTypes.DeleteDiscountTypes
        Using service As IDiscountTypesAdminService = Container.Current.Resolve(Of IDiscountTypesAdminService)()
            Return service.DeleteDiscountTypes(DiscountTypes, audit)
        End Using
    End Function

    Public Function GetDiscountTypes(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DiscountTypes) Implements IContractDiscountTypes.GetDiscountTypes
        Using service As IDiscountTypesAdminService = Container.Current.Resolve(Of IDiscountTypesAdminService)()
            Return service.GetDiscountTypes(code, audit)
        End Using
    End Function

    Public Function GetDiscountTypesById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DiscountTypes) Implements IContractDiscountTypes.GetDiscountTypesById
        Using service As IDiscountTypesAdminService = Container.Current.Resolve(Of IDiscountTypesAdminService)()
            Return service.GetDiscountTypesById(id, audit)
        End Using
    End Function

    Public Function SaveDiscountTypes(DiscountTypes As Domain.Entities.DiscountTypes, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DiscountTypes) Implements IContractDiscountTypes.SaveDiscountTypes
        Using service As IDiscountTypesAdminService = Container.Current.Resolve(Of IDiscountTypesAdminService)()
            Return service.SaveDiscountTypes(DiscountTypes, audit, idSequense)
        End Using
    End Function
End Class
