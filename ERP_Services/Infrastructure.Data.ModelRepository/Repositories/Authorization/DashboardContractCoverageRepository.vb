'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/05/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class DashboardContractCoverageRepository
    Inherits GenericRepository(Of ServiceOrderDetail)
    Implements IDashboardContractCoverageRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function SP_SaveContractCoverage(xml As String) As SP_SaveContractCoverage_Result Implements IDashboardContractCoverageRepository.SP_SaveContractCoverage
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveContractCoverage(xml).SingleOrDefault()
    End Function

End Class
