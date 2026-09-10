'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class BillingService
    ''' <summary>
    ''' lista los detalles quirurgicos de la orden de servicio
    ''' </summary>
    ''' <param name="idServiceOrderDetail"></param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail As Integer) As List(Of Domain.Entities.ServiceOrderDetailSurgical) Implements IBillingServiceServiceOrderDetailSurgical.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail
        Using service As IServiceOrderDetailSurgicalAdminService = Container.Current.Resolve(Of IServiceOrderDetailSurgicalAdminService)()
            Return service.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail)
        End Using
        'Return _serviceOrderDetailSurgicalAdminService.ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail)
    End Function

    ''' <summary>
    ''' Guarda el detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgical"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical As Domain.Entities.ServiceOrderDetailSurgical, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrderDetailSurgical) Implements IBillingServiceServiceOrderDetailSurgical.SaveServiceOrderDetailSurgical
        Using service As IServiceOrderDetailSurgicalAdminService = Container.Current.Resolve(Of IServiceOrderDetailSurgicalAdminService)()
            Return service.SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical, audit)
        End Using
        'Return Me._serviceOrderDetailSurgicalAdminService.SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical, audit)
    End Function

    ''' <summary>
    ''' Elimina un detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgical"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IBillingServiceServiceOrderDetailSurgical.DeleteServiceOrderDetailSurgical
        Using service As IServiceOrderDetailSurgicalAdminService = Container.Current.Resolve(Of IServiceOrderDetailSurgicalAdminService)()
            Return service.DeleteServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId, audit)
        End Using
        'Return Me._serviceOrderDetailSurgicalAdminService.DeleteServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId, audit)
    End Function

    ''' <summary>
    ''' Valida que la causacion no exista en alguna liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As Domain.Base.Entities.ActionResult(Of String) Implements IBillingServiceServiceOrderDetailSurgical.ValidateDeleteServiceOrderDetailSurgical
        Using service As IServiceOrderDetailSurgicalAdminService = Container.Current.Resolve(Of IServiceOrderDetailSurgicalAdminService)()
            Return service.ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId)
        End Using
        'Return Me._serviceOrderDetailSurgicalAdminService.ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId)
    End Function

    ''' <summary>
    ''' Actualiza los campos de tercero y médico del detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <param name="HealthProfessionalCode"></param>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrderDetailSurgical) Implements IBillingServiceServiceOrderDetailSurgical.UpdateFieldsServiceOrderDetailSurgical
        Using service As IServiceOrderDetailSurgicalAdminService = Container.Current.Resolve(Of IServiceOrderDetailSurgicalAdminService)()
            Return service.UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId, HealthProfessionalCode, ThirdPartyId)
        End Using
        'Return Me._serviceOrderDetailSurgicalAdminService.UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId, HealthProfessionalCode, ThirdPartyId)
    End Function

End Class
