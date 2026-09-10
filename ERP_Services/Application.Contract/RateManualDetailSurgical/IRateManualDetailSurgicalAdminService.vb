'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 24/11/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IRateManualDetailSurgicalAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para obtener un detalla del manual de tarifas quirurgico
    ''' </summary>
    Function GetSurgicalDetailServiceOrder(rateManualId As Integer, ipsService As Integer, surgicalGrouopId As Integer?, UVRNumber As Integer?, serviceManual As Integer) As RateManualDetailSurgical
End Interface
