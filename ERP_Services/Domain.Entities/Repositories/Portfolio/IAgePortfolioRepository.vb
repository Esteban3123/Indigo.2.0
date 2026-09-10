'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IAgePortfolioRepository
    Inherits IRepository(Of AgesPortfolio)

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    Function ListAgesPortfolio() As List(Of AgesPortfolio)
    ''' <summary>
    ''' lista todas las edaddes de cartera de una unidad operativa
    ''' </summary>
    ''' <param name="idSettingPortfolio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio As Integer) As List(Of AgesPortfolio)
End Interface