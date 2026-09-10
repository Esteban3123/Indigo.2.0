'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Diego Andrés Roldán
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService

#Region "Methods"
    ''' <summary>
    ''' Elimina una edad de cartera
    ''' </summary>
    ''' <param name="agesPortfolio"></param>
    ''' <returns></returns>
    Public Function DeleteAgesPortfolio(agesPortfolio As AgesPortfolio, audit As AuditMessage) As ActionResult Implements IPortfolioServiceAgePortfolio.DeleteAgesPortfolio
        Using service As IAgePortfolioAdminService = Container.Current.Resolve(Of IAgePortfolioAdminService)()
            Return service.DeleteAgesPortfolio(agesPortfolio, audit)
        End Using
        'Return Me._agesPortfolioAdminService.DeleteAgesPortfolio(agesPortfolio, audit)
    End Function

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPortfolio() As ActionResult(Of List(Of AgesPortfolio)) Implements IPortfolioServiceAgePortfolio.ListAgesPortfolio
        Using service As IAgePortfolioAdminService = Container.Current.Resolve(Of IAgePortfolioAdminService)()
            Return service.ListAgesPortfolio()
        End Using
        'Return Me._agesPortfolioAdminService.ListAgesPortfolio()
    End Function

    ''' <summary>
    ''' Guarda una edad de cartera
    ''' </summary>
    ''' <param name="agesPortfolio"></param>
    ''' <returns></returns>
    Public Function SaveAgesPortfolio(agesPortfolio As AgesPortfolio, audit As AuditMessage) As ActionResult(Of AgesPortfolio) Implements IPortfolioServiceAgePortfolio.SaveAgesPortfolio
        Using service As IAgePortfolioAdminService = Container.Current.Resolve(Of IAgePortfolioAdminService)()
            Return service.SaveAgesPortfolio(agesPortfolio, audit)
        End Using
        'Return Me._agesPortfolioAdminService.SaveAgesPortfolio(agesPortfolio, audit)
    End Function

    ''' <summary>
    ''' Lista todos las edades de cartera por unidad operativa
    ''' </summary>
    ''' <param name="idOperatingUnit"></param>
    ''' <returns></returns>
    Public Function ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio As Integer) As List(Of Domain.Entities.AgesPortfolio) Implements IPortfolioServiceAgePortfolio.ListAgesPortfolioByIdSettingPortfolio
        Using service As IAgePortfolioAdminService = Container.Current.Resolve(Of IAgePortfolioAdminService)()
            Return service.ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio)
        End Using
        'Return Me._agesPortfolioAdminService.ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio)
    End Function
#End Region

End Class
