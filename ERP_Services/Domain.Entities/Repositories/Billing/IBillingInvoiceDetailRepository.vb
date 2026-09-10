'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-09-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IBillingInvoiceDetailRepository
    Inherits IRepository(Of InvoiceDetail)

    Function SaveListInvoiceDetail(detailList As List(Of InvoiceDetail)) As Boolean

End Interface
