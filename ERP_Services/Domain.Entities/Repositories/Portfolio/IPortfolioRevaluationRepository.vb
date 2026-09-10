'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IPortfolioRevaluationRepository

    Function CalculatePortfolioRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult)

End Interface
