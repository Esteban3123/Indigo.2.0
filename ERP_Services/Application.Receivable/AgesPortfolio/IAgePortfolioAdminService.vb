'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAgePortfolioAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una edad de cartera
    ''' </summary>
    ''' <returns></returns>
    Function SaveAgesPortfolio(ByVal agesPortfolio As AgesPortfolio, ByVal audit As AuditMessage) As ActionResult(Of AgesPortfolio)

    ''' <summary>
    ''' Elimina una edad de cartera
    ''' </summary>
    ''' <returns></returns>
    Function DeleteAgesPortfolio(ByVal agesPortfolio As AgesPortfolio, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    Function ListAgesPortfolio() As ActionResult(Of List(Of AgesPortfolio))
    ''' <summary>
    ''' lista las edades de cartera por unidad operativa
    ''' </summary>
    ''' <param name="idSetingPortfolio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAgesPortfolioByIdSettingPortfolio(idSetingPortfolio As Integer) As List(Of AgesPortfolio)

End Interface
