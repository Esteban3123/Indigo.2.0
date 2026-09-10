'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IServiceOrderDetailSurgicalAdminService
    Inherits IDisposable

    ''' <summary>
    ''' lista los detalles quirurgicos de la orden de servicio
    ''' </summary>
    ''' <param name="idServiceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail As Integer) As List(Of ServiceOrderDetailSurgical)

    ''' <summary>
    ''' Guarda el detalle quirurgico
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgical"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveServiceOrderDetailSurgical(ByVal ServiceOrderDetailSurgical As ServiceOrderDetailSurgical, ByVal audit As AuditMessage) As ActionResult(Of ServiceOrderDetailSurgical)

    ''' <summary>
    ''' Elimina un detalle quirurgico
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteServiceOrderDetailSurgical(ByVal ServiceOrderDetailSurgicalId As Integer, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Valida que la causacion no este en ninguna liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As ActionResult(Of String)

    ''' <summary>
    ''' Actualiza los campos de médico y tercero en el detalle quirurgico de la orden de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateFieldsServiceOrderDetailSurgical(ServiceOrderDetailSurgicalId As Integer, HealthProfessionalCode As String, ThirdPartyId As Integer) As ActionResult(Of ServiceOrderDetailSurgical)

End Interface
