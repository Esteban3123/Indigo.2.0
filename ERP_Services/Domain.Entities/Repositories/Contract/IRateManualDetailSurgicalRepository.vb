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

Public Interface IRateManualDetailSurgicalRepository
    Inherits IRepository(Of RateManualDetailSurgical)

    ''' <summary>
    ''' metodo para obtener un detalla del manual de tarifas quirurgico
    ''' </summary>
    Function GetSurgicalDetailServiceOrder(rateManualId As Integer, ipsService As Integer, surgicalGrouopId As Integer?, UVRNumber As Integer?, serviceManual As Integer) As RateManualDetailSurgical

    ''' <summary>
    ''' obtiene una tarifa del detalle quirurgico por id
    ''' </summary>
    ''' <param name="rateManualDetailSurgicalId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualDetailSurgicalById(rateManualDetailSurgicalId As Integer) As RateManualDetailSurgical


End Interface
