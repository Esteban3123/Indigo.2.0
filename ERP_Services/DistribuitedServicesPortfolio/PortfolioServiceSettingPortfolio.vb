'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
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
    ''' <summary>
    ''' obtiene la configuracion por id de la unidad operativa
    ''' </summary>
    ''' <param name="idOperatingUnit"></param>
    ''' <returns></returns>
    Public Function GetSettingPortfolioByIdOperatingUnit(idOperatingUnit As Integer, audit As AuditMessage) As Domain.Entities.SettingPortfolio Implements IPortfolioServiceSettingPortfolio.GetSettingPortfolioByIdOperatingUnit
        Using service As ISettingPortfolioAdminService = Container.Current.Resolve(Of ISettingPortfolioAdminService)()
            Return service.GetSettinPortfolioByIdOperatingUnit(idOperatingUnit, audit)
        End Using
        'Return _settingPortfolioAdminService.GetSettinPortfolioByIdOperatingUnit(idOperatingUnit, audit)
    End Function

    ''' <summary>
    ''' guarda la configuracion de cartera
    ''' </summary>
    ''' <param name="settingPortfolio"></param>
    ''' <returns></returns>
    Public Function SaveSettingPortfolio(settingPortfolio As Domain.Entities.SettingPortfolio, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingPortfolio) Implements IPortfolioServiceSettingPortfolio.SaveSettingPortfolio
        Using service As ISettingPortfolioAdminService = Container.Current.Resolve(Of ISettingPortfolioAdminService)()
            Return service.SaveSettingPortfolio(settingPortfolio, audit)
        End Using
        'Return _settingPortfolioAdminService.SaveSettingPortfolio(settingPortfolio, audit)
    End Function
End Class
