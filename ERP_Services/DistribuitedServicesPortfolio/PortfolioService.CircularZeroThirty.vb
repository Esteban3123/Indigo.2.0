'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Faiber Julian Mora D.
' Created          : 07-10-2016
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

    Public Function ListTrimesters() As Dictionary(Of Integer, String) Implements IPortfolioServiceCircularZeroThirty.ListTrimesters
        Using service As ICircularZeroThirtyAdminService = Container.Current.Resolve(Of ICircularZeroThirtyAdminService)()
            Return service.ListTrimesters()
        End Using
        'Return _circularZeroThirtyAdminService.ListTrimesters()
    End Function

    Public Function GenerateDocument030(year As Integer, trimester As Integer, audit As AuditMessage) As ActionResult(Of List(Of GenerateDocument030_Result)) Implements IPortfolioServiceCircularZeroThirty.GenerateDocument030
        Using service As ICircularZeroThirtyAdminService = Container.Current.Resolve(Of ICircularZeroThirtyAdminService)()
            Return service.GenerateDocument030(year, trimester, audit)
        End Using
        'Return _circularZeroThirtyAdminService.GenerateDocument030(year, trimester, audit)
    End Function

End Class
