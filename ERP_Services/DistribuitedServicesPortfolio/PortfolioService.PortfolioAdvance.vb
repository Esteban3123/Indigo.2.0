'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Diego Andrés Roldán
' Created          : 29-07-2014
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
    ''' Deletes the portfolio advance.
    ''' </summary>
    ''' <param name="portfolioAdvance">The portfolio advance.</param>
    ''' <returns></returns>
    Public Function DeletePortfolioAdvance(portfolioAdvance As Domain.Entities.PortfolioAdvance, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IPortfolioServicePortfolioAdvance.DeletePortfolioAdvance
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.DeletePortfolioAdvance(portfolioAdvance, audit)
        End Using
        'Return Me._portfolioAdvanceAdminService.DeletePortfolioAdvance(portfolioAdvance, audit)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvance(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioAdvance) Implements IPortfolioServicePortfolioAdvance.GetPortfolioAdvance
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.GetPortfolioAdvance(code, audit)
        End Using
        'Return Me._portfolioAdvanceAdminService.GetPortfolioAdvance(code, audit)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceById(Id As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioAdvance) Implements IPortfolioServicePortfolioAdvance.GetPortfolioAdvanceById
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.GetPortfolioAdvanceById(Id)
        End Using
        'Return Me._portfolioAdvanceAdminService.GetPortfolioAdvanceById(Id)
    End Function

    ''' <summary>
    ''' Lista todos los anticipos por Id del tercero
    ''' </summary>
    ''' <param name="ThirdId"></param>
    ''' <returns></returns>
    Public Function ListPorfolioAdvanceByThirdId(ThirdId As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioAdvance)) Implements IPortfolioServicePortfolioAdvance.ListPorfolioAdvanceByThirdId
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.ListPorfolioAdvanceByThirdId(ThirdId)
        End Using
        'Return Me._portfolioAdvanceAdminService.ListPorfolioAdvanceByThirdId(ThirdId)
    End Function

    ''' <summary>
    ''' Guarda un anticipo
    ''' </summary>
    ''' <param name="portfolioAdvance"></param>
    ''' <returns></returns>
    Public Function SavePortfolioAdvance(portfolioAdvance As Domain.Entities.PortfolioAdvance, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.PortfolioAdvance) Implements IPortfolioServicePortfolioAdvance.SavePortfolioAdvance
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.SavePortfolioAdvance(portfolioAdvance, audit, idSequense)
        End Using
        'Return Me._portfolioAdvanceAdminService.SavePortfolioAdvance(portfolioAdvance, audit, idSequense)
    End Function

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Public Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId As Integer, admission As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.PortfolioAdvance)) Implements IPortfolioServicePortfolioAdvance.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId, admission)
        End Using
        'Return Me._portfolioAdvanceAdminService.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ThirdId, admission)
    End Function

    ''' <summary>
    ''' Get the Balance of advance in diferent Currency
    ''' </summary>
    ''' <param name="portfolioAdvanceId"></param>
    ''' <param name="toCurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <param name="moduleTRM"></param>
    ''' <returns></returns>
    Public Function GetBalanceAdvanceByCurrency(portfolioAdvanceId As Integer,
                                            Optional toCurrencyId As Integer? = Nothing,
                                            Optional dateTRM As Date? = Nothing,
                                            Optional moduleTRM As EModuleTRM = EModuleTRM.CommonTRM) As ActionResult(Of PortfolioAdvance) Implements IPortfolioServicePortfolioAdvance.GetBalanceAdvanceByCurrency
        Using service As IPortfolioAdvanceAdminService = Container.Current.Resolve(Of IPortfolioAdvanceAdminService)()
            Return service.GetBalanceAdvanceByCurrency(portfolioAdvanceId, toCurrencyId, dateTRM, moduleTRM)
        End Using
    End Function

#End Region

End Class
