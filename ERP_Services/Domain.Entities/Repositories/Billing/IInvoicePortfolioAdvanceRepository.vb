'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 05-10-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IInvoicePortfolioAdvanceRepository
    Inherits IRepository(Of InvoicePortfolioAdvance)

    Sub AddRageEntity(listInvoicePortfolioAdvance As List(Of InvoicePortfolioAdvance))

End Interface