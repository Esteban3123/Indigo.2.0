'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
Imports Application.Contract

Partial Class ContractService

    Public Function ChangeStateCareGroup(id As Integer, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup) Implements IContractCareGroup.ChangeStateCareGroup
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.ChangeStateCareGroup(id, state, audit)
        End Using
        'Return Me._careGroupAdminService.ChangeStateCareGroup(id, state, audit)
    End Function

    Public Function DeleteCareGroup(id As Integer, Company As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractCareGroup.DeleteCareGroup
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.DeleteCareGroup(id, Company, audit)
        End Using
        'Return Me._careGroupAdminService.DeleteCareGroup(id, Company, audit)
    End Function

    Public Function GetCareGroup(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup) Implements IContractCareGroup.GetCareGroup
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.GetCareGroup(code, audit)
        End Using
        'Return Me._careGroupAdminService.GetCareGroup(code, audit)
    End Function

    Public Function GetCareGroupById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup) Implements IContractCareGroup.GetCareGroupById
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.GetCareGroupById(id, audit)
        End Using
        'Return Me._careGroupAdminService.GetCareGroupById(id, audit)
    End Function

    Public Function SaveCareGroup(CareGroup As Domain.Entities.CareGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CareGroup) Implements IContractCareGroup.SaveCareGroup
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.SaveCareGroup(CareGroup, audit, idSequense)
        End Using
        'Return Me._careGroupAdminService.SaveCareGroup(CareGroup, audit, idSequense)
    End Function

    Public Function ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId As Integer) As List(Of Domain.Entities.CareGroupInvoiceCategories) Implements IContractCareGroup.ListCareGroupInvoiceCategoriesByCareGroupId
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId)
        End Using
        'Return Me._careGroupAdminService.ListCareGroupInvoiceCategoriesByCareGroupId(careGroupId)
    End Function

    Public Function GetGroupersCareGroup(CareGroupId As Integer, grouperId As Integer, audit As AuditMessage) As GroupersCareGroup Implements IContractCareGroup.GetGroupersCareGroup
        Using service As ICareGroupAdminService = Container.Current.Resolve(Of ICareGroupAdminService)()
            Return service.GetGroupersCareGroup(CareGroupId, grouperId, audit)
        End Using
        'Return Me._careGroupAdminService.GetGroupersCareGroup(CareGroupId, grouperId, audit)
    End Function
End Class
