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

    Public Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements IPortfolioServiceRevaluation.CalculateRevaluation
        Using service As IPortfolioRevaluationAdminService = Container.Current.Resolve(Of IPortfolioRevaluationAdminService)()
            Return service.CalculateRevaluation(Month, Year, Status, UserCode)
        End Using
    End Function

#End Region

End Class
