'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 05-07-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
#End Region

Public Class AddCashReceiptConceptEventArgs
    Inherits EventArgs

    Property CashReceiptDetails As CashReceiptDetails

    Property PortfolioAdvance As PortfolioAdvance

    Property ListAccountReceivable As List(Of AccountReceivable)

    Property ListAdvancePayment As List(Of CashReceiptAdvancePayment)

    Property ListCashReceiptDetailAccountPayable As List(Of CashReceiptDetailAccountPayable)
End Class
