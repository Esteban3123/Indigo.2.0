'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PortfolioInitialBalanceAccountReceivableRepository
    Inherits GenericRepository(Of PortfolioInitialBalanceAccountReceivable)
    Implements IPortfolioInitialBalanceAccountReceivableRepository


#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="PortfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    Public Function GetPortfolioInitialBalanceAccountReceivableById(PortfolioInitialBalanceAccountReceivableId As Integer) As PortfolioInitialBalanceAccountReceivable Implements IPortfolioInitialBalanceAccountReceivableRepository.GetPortfolioInitialBalanceAccountReceivableById
        Return (From pibar In _context.PortfolioInitialBalanceAccountReceivable Where pibar.Id = PortfolioInitialBalanceAccountReceivableId Select pibar).FirstOrDefault()
    End Function

    Public Function GetPortgolioInitialBalanceWithOutBudget(initialBalanceId As Integer) As List(Of PortfolioInitialBalanceAccountReceivable) Implements IPortfolioInitialBalanceAccountReceivableRepository.GetPortgolioInitialBalanceWithOutBudget
        Return (From pibar In _context.PortfolioInitialBalanceAccountReceivable.AsNoTracking() Where pibar.PortfolioInitialBalanceId = initialBalanceId And pibar.AffectBudget = False Select pibar).ToList()
    End Function
End Class
