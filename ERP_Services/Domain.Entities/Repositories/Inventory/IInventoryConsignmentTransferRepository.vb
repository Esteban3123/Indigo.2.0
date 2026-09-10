'************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego A. Roldan
' Created          : 2023-05-08
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
#End Region


Public Interface IInventoryConsignmentTransferRepository
    Inherits IRepository(Of ConsignmentTransfer)

    Function GetByCode(code As String) As ConsignmentTransfer

End Interface
