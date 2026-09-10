'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure

#End Region

Public Class PortfolioRevaluationRepository
    Inherits BaseRepository
    Implements IPortfolioRevaluationRepository

#Region "Builder"

    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"

    Public Function CalculatePortfolioRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements IPortfolioRevaluationRepository.CalculatePortfolioRevaluation
        Dim parameters As IEnumerable(Of (String, Object)) = {
                    ("@month", Month), ("@year", Year), ("@status", Status), ("@userCode", UserCode)
                }

        Dim result = ExecuteStoredProcedure(Of RevaluationResult)("[Portfolio].[SPCurrencyRevaluation]", parameters)

        Return result
    End Function

#End Region

End Class
