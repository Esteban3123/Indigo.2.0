'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 20/05/2015
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

Public Class GetListTransferOrderDetailEventArgs
    Inherits EventArgs

    Property ListTransferOrdeDetail As List(Of TransferOrderDetail)

    Property ListDetailMedicineAndSupplie As List(Of TransferOrderDetail)

End Class
