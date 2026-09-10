'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceServiceOrderDetailSurgical

    ''' <summary>
    ''' lista los detalles quirurgicos de la orden de servicio
    ''' </summary>
    ''' <param name="idServiceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail As Integer) As List(Of ServiceOrderDetailSurgical)

    ''' <summary>
    ''' Guarda un detalle quirurgico
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveServiceOrderDetailSurgical(ServiceOrderDetailSurgical As Domain.Entities.ServiceOrderDetailSurgical, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ServiceOrderDetailSurgical)

    ''' <summary>
    ''' Elimina un detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Valida que la causacion no exista en una liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As ActionResult(Of String)

    ''' <summary>
    ''' Actualiza los campos de médico y tercero en el detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetailSurgical)

End Interface
