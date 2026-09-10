'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class AgePortfolioRepository
    Inherits GenericRepository(Of AgesPortfolio)
    Implements IAgePortfolioRepository



    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todos las edades de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPortfolio() As List(Of AgesPortfolio) Implements IAgePortfolioRepository.ListAgesPortfolio
        Dim query = (From ap As AgesPortfolio In _context.AgesPortfolio Select ap).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' lista todas las edaddes de cartera de una unidad operativa
    ''' </summary>
    ''' <param name="idSettingPortfolio"></param>
    ''' <returns></returns>
    Public Function ListAgesPortfolioByIdSettingPortfolio(idSettingPortfolio As Integer) As List(Of AgesPortfolio) Implements IAgePortfolioRepository.ListAgesPortfolioByIdSettingPortfolio
        Dim query = (From ap As AgesPortfolio In _context.AgesPortfolio Where ap.SettingPortfolioId = idSettingPortfolio Select ap).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            Return query
        Else
            Return New List(Of AgesPortfolio)
        End If
    End Function
End Class