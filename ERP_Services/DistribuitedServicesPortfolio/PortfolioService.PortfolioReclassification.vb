'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : CJuan Carlos Bermudez
' Created          : 29-05-2015
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
    ''' Obtiene una reclasificación de documento por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetReclassificationByCode(code As String, audit As AuditMessage) As Domain.Entities.PortfolioReclassification Implements IPortfolioServicePortfolioReclassification.GetReclassificationByCode
        Using service As IPortfolioReclassificationAdminService = Container.Current.Resolve(Of IPortfolioReclassificationAdminService)()
            Return service.GetReclassificationByCode(code, audit)
        End Using
        'Return _PortfolioReclassificationAdminService.GetReclassificationByCode(code, audit)
    End Function

End Class
