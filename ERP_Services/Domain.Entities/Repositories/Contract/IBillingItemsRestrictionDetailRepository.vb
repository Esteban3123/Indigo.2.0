'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IBillingItemsRestrictionDetailRepository
    Inherits IRepository(Of BillingItemsRestrictionDetail)

    Function GetItemsRestrictionDetailByIdHeader(idHeader As Integer) As List(Of BillingItemsRestrictionDetail)

End Interface
