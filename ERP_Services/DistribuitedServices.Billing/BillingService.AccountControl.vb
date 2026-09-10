'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Diego A. Roldán
' Created          : 04-07-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports DistribuitedServices.Billing
Imports Domain.Entities

#End Region

Partial Class BillingService
    Implements IBillingServiceAccountControl

    Public Function GenerateServiceOrderMassive(objParams As String, homologations As List(Of List(Of Domain.Entities.CupsHomologation)), audit As AuditMessage) As ActionResult(Of List(Of List(Of Domain.Entities.CupsHomologation))) Implements IBillingServiceAccountControl.GenerateServiceOrderMassive
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.GenerateServiceOrderMassive(objParams, homologations, audit)
        End Using
        'Return _accountControlAdminService.GenerateServiceOrderMassive(objParams, homologations, audit)
    End Function

    Public Function GetServiceOrderDetailHomologation(careGroupId As Integer, listHomologations As List(Of List(Of Domain.Entities.CupsHomologation)), args As String) As ActionResult(Of Domain.Entities.ServiceOrder) Implements IBillingServiceAccountControl.GetServiceOrderDetailHomologation
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.GetServiceOrderDetailHomologation(careGroupId, listHomologations, args)
        End Using
        'Return _accountControlAdminService.GetServiceOrderDetailHomologation(careGroupId, listHomologations, args)
    End Function

    Public Function GenerateServiceOrderMassiveWithListDetail(parameters As String, listServiceOrderDetail As List(Of Domain.Entities.ServiceOrderDetail), audit As AuditMessage) As ActionResult Implements IBillingServiceAccountControl.GenerateServiceOrderMassiveWithListDetail
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.GenerateServiceOrderMassiveWithListDetail(parameters, listServiceOrderDetail, audit)
        End Using
        'Return _accountControlAdminService.GenerateServiceOrderMassiveWithListDetail(parameters, listServiceOrderDetail, audit)
    End Function

    Public Function GetHomologationsCups(parameter As String, careGroupId As Integer) As ActionResult(Of List(Of List(Of Domain.Entities.CupsHomologation))) Implements IBillingServiceAccountControl.GetHomologationsCups
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.GetHomologationsCups(parameter, careGroupId)
        End Using
        'Return _accountControlAdminService.GetHomologationsCups(parameter, careGroupId)
    End Function

    Public Function SP_ListCareCenterHis(UserCode As String, GroupCode As String, CompanyContainer As String) As ActionResult(Of List(Of SP_ListCareCenterHis_Result)) Implements IBillingServiceAccountControl.SP_ListCareCenterHis
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.SP_ListCareCenterHis(UserCode, GroupCode, CompanyContainer)
        End Using
    End Function

    Public Function SP_ListFunctionalUnitHis(CareCenterCode As String, UserCode As String, GroupCode As String, CompanyContainer As String) As ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result)) Implements IBillingServiceAccountControl.SP_ListFunctionalUnitHis
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.SP_ListFunctionalUnitHis(CareCenterCode, UserCode, GroupCode, CompanyContainer)
        End Using
    End Function

    Public Function GetAuthorizationParameterByTUF(CareCenterCode As String, FunctionalUnit As String, EmpresaDGH As String) As ActionResult Implements IBillingServiceAccountControl.GetAuthorizationParameterByTUF
        Using service As IAccountControlAdminService = Container.Current.Resolve(Of IAccountControlAdminService)()
            Return service.GetAuthorizationParameterByTUF(CareCenterCode, FunctionalUnit, EmpresaDGH)
        End Using
    End Function

End Class
