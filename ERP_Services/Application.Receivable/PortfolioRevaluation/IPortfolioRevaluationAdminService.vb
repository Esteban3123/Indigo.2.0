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

Public Interface IPortfolioRevaluationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' metodo para pegar en la rejilla de traslado
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult)

End Interface