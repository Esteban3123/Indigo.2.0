'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IRateManualDetailRepository
    Inherits IRepository(Of RateManualDetail)

    ''' <summary>
    ''' Obtiene un detalle de manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualDetailById(id As Integer) As RateManualDetail
    ''' <summary>
    ''' metodo para obtener un detalla del reta
    ''' </summary>
    ''' <param name="rateManualId"></param>
    ''' <param name="ipsServiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualDetailServiceOrder(rateManualId As Integer, ipsServiceId As Integer) As RateManualDetail
End Interface
