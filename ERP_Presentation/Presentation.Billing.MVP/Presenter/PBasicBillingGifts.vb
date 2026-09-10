'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Andres Alarcon
' Created          : 2023-03-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo

#End Region

Public Class PBasicBillingGifts

#Region "Fields"

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Private Indigo As SessionValues

#End Region

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Lista los almacenes habilitadas para el tercero
    ''' </summary>
    ''' <remarks></remarks>
    Function InitializeWarehouseXPO(ListWareHouse As List(Of Integer))
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListWarehouseByIdAndUser(Indigo.UserIndigo, ListWareHouse)
    End Function

#End Region

End Class
