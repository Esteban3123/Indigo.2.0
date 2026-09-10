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

#Region "Methods"
    ''' <summary>
    ''' metodo para eliminar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <returns></returns>
    Public Function DeleteEconomicIndicator(economicIndicator As EconomicIndicator, audit As AuditMessage) As ActionResult Implements IPortfolioEconomicIndicator.DeleteEconomicIndicator
        Using service As IEconomicIndicatorAdminService = Container.Current.Resolve(Of IEconomicIndicatorAdminService)()
            Return service.DeleteEconomicIndicator(economicIndicator, audit)
        End Using
        'Return Me._economicIndicatorAdminService.DeleteEconomicIndicator(economicIndicator, audit)
    End Function
    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetEconomicIndicatorByCode(code As String, audit As AuditMessage) As EconomicIndicator Implements IPortfolioEconomicIndicator.GetEconomicIndicatorByCode
        Using service As IEconomicIndicatorAdminService = Container.Current.Resolve(Of IEconomicIndicatorAdminService)()
            Return service.GetEconomicIndicatorByCode(code, audit)
        End Using
        'Return Me._economicIndicatorAdminService.GetEconomicIndicatorByCode(code, audit)
    End Function
    ''' <summary>
    ''' Metodo para listar todos los
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllEconomicIndicator(audit As AuditMessage) As List(Of EconomicIndicator) Implements IPortfolioEconomicIndicator.GetAllEconomicIndicator
        Using service As IEconomicIndicatorAdminService = Container.Current.Resolve(Of IEconomicIndicatorAdminService)()
            Return service.GetAllEconomicIndicator(audit)
        End Using
        'Return Me._economicIndicatorAdminService.GetAllEconomicIndicator(audit)
    End Function

    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function GetEconomicIndicator(year As String, month As String, audit As AuditMessage) As EconomicIndicator Implements IPortfolioEconomicIndicator.GetEconomicIndicator
        Using service As IEconomicIndicatorAdminService = Container.Current.Resolve(Of IEconomicIndicatorAdminService)()
            Return service.GetEconomicIndicator(year, month, audit)
        End Using
        'Return Me._economicIndicatorAdminService.GetEconomicIndicator(year, month, audit)
    End Function

    ''' <summary>
    ''' metodo para guardar un indicador economico
    ''' </summary>
    ''' <param name="economicIndicator">The economic indicator.</param>
    ''' <returns></returns>
    Public Function SaveEconomicIndicator(economicIndicator As EconomicIndicator, idSequense As Int64, audit As AuditMessage) As ActionResult(Of EconomicIndicator) Implements IPortfolioEconomicIndicator.SaveEconomicIndicator
        Using service As IEconomicIndicatorAdminService = Container.Current.Resolve(Of IEconomicIndicatorAdminService)()
            Return service.SaveEconomicIndicator(economicIndicator, audit, idSequense)
        End Using
        'Return Me._economicIndicatorAdminService.SaveEconomicIndicator(economicIndicator, audit, idSequense)
    End Function

    ''' <summary>
    ''' Changes the state1.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateEconomicIndicator(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.EconomicIndicator) Implements IPortfolioEconomicIndicator.ChangeStateEconomicIndicator
        Using service As IEconomicIndicatorAdminService = Container.Current.Resolve(Of IEconomicIndicatorAdminService)()
            Return service.ChangeState(code, state, audit)
        End Using
        'Return Me._economicIndicatorAdminService.ChangeState(code, state, audit)
    End Function
#End Region

End Class
