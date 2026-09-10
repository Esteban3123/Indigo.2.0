'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IPortfolioInitialBalanceAccountReceivableRepository
    Inherits IRepository(Of PortfolioInitialBalanceAccountReceivable)
    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="PortfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioInitialBalanceAccountReceivableById(PortfolioInitialBalanceAccountReceivableId As Integer) As PortfolioInitialBalanceAccountReceivable
    ''' <summary>
    ''' busca si existen facturas sin asiganar prsupuesto
    ''' </summary>
    ''' <param name="initialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortgolioInitialBalanceWithOutBudget(initialBalanceId As Integer) As List(Of PortfolioInitialBalanceAccountReceivable)

    ''' <summary>
    ''' Obtiene la staging row (PortfolioInitialBalanceAccountReceivable) de una factura
    ''' identificada por (PortfolioInitialBalanceId, InvoiceNumber). Utilizado para localizar
    ''' los datos de la factura antes del confirm, cuando aún no existen shadow Invoice/ED.
    ''' </summary>
    Function GetByInvoiceNumberAndInitialBalanceId(initialBalanceId As Integer, invoiceNumber As String) As PortfolioInitialBalanceAccountReceivable
End Interface
