'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
#End Region


Public Interface IServiceOrderDetailSurgicalRepository
    Inherits IRepository(Of ServiceOrderDetailSurgical)
    ''' <summary>
    ''' lista los detalles quirurgicos de la ordern de servicio
    ''' </summary>
    ''' <param name="idServiceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListServiceOrderDetailSurgicalByIdDerviceOrderDetail(idServiceOrderDetail As Integer) As List(Of ServiceOrderDetailSurgical)

    ''' <summary>
    ''' Valida que la causacion no exista en alguna liquidacion
    ''' </summary>
    ''' <param name="MedicalFeesCausationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateDeleteServiceOrderDetailSurgical(MedicalFeesCausationId As Integer) As Boolean

    ''' <summary>
    ''' Obtiene el detalle quirurgico por id
    ''' </summary>
    ''' <param name="ServiceOrderDetailSurgicalId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderDetailSurgicalById(ServiceOrderDetailSurgicalId As Integer, Optional tracking As Boolean = True) As ServiceOrderDetailSurgical

End Interface
