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

#End Region

Partial Class BillingService
    Implements IBillingControlOutPatientServices

    Public Function GenerateDocuments(controlOutPatientServices As Domain.Entities.ControlOutPatientServices, companyNit As String, audit As AuditMessage) As ActionResult(Of String) Implements IBillingControlOutPatientServices.GenerateDocuments
        Using service As IControlOutPatientServicesAdminService = Container.Current.Resolve(Of IControlOutPatientServicesAdminService)()
            Return service.GenerateDocuments(controlOutPatientServices, companyNit, audit)
        End Using
        'Return _controlOutPatientServiceAdminService.GenerateDocuments(controlOutPatientServices, audit)
    End Function

    Public Function GetProductsByActmedicaAndCups(listActmedicaCups As List(Of Domain.Crystal.Entities.ACTMEDCUPS), IPFECNACI As Date) As ActionResult(Of List(Of Domain.Entities.ProductCita)) Implements IBillingControlOutPatientServices.GetProductsByActmedicaAndCups
        Using service As IControlOutPatientServicesAdminService = Container.Current.Resolve(Of IControlOutPatientServicesAdminService)()
            Return service.GetProductsByActmedicaAndCups(listActmedicaCups, IPFECNACI)
        End Using
        'Return _controlOutPatientServiceAdminService.GetProductsByActmedicaAndCups(listActmedicaCups, IPFECNACI)
    End Function

    Public Function GenerateServiceOrderProducts(params As String, audit As AuditMessage) As ActionResult(Of String) Implements IBillingControlOutPatientServices.GenerateServiceOrderProducts
        Using service As IControlOutPatientServicesAdminService = Container.Current.Resolve(Of IControlOutPatientServicesAdminService)()
            Return service.GenerateServiceOrderProducts(params, audit)
        End Using
        'Return _controlOutPatientServiceAdminService.GenerateServiceOrderProducts(params, audit)
    End Function
End Class
