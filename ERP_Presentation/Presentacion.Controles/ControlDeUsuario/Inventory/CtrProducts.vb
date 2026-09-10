'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 26-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Controls.MVP

#End Region

Public Class CtrProducts

#Region "EVENTS"
    ''' <summary>
    ''' evento para retornar el valor seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event SelectProduct(sender As Object, e As SelectProductEventArgs)

    Public Property DataSource As XPInstantFeedbackSource
        Get
            Return INDGcProducts.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcProducts.DataSource = value
        End Set
    End Property


#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub CtrProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridView1.MoreInfoColunmns(INDGvProducts)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProducts.Columns
            If col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next
        ''se actualiza el formato de los campos numericos en el formuario
        Dim indigo = SessionValues.Instance
        Me.colCost = Window.Utils.FormatGrid(Me.colCost, indigo.CurrencyISO4217)
    End Sub
#End Region

#Region "MouseDoubleClick"
    Private Sub INDGcProducts_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcProducts.MouseDoubleClick
        Dim hitPoint = Me.INDGvProducts.CalcHitInfo(e.Location)
        If hitPoint.InRow = True Then
            Dim product = DirectCast(DirectCast(INDGvProducts.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, InventoryProductXpo)
            Dim args As New SelectProductEventArgs
            args.ProductId = product.Id
            args.CodeNameProduct = product.CodeName
            RaiseEvent SelectProduct(Nothing, args)
        End If
    End Sub
#End Region

#Region "FocusedRowChanged"
    Private Sub INDGvProducts_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles INDGvProducts.FocusedRowChanged
        If e.FocusedRowHandle >= 0 Then
            If Not INDGvProducts.GetRow(e.FocusedRowHandle).GetType().Equals((GetType(DevExpress.Data.NotLoadedObject))) Then
                Dim product = DirectCast(DirectCast(INDGvProducts.GetRow(e.FocusedRowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, InventoryProductXpo)
                SearchPhysicalInventory(product.Id)
            End If
        End If
    End Sub

    Private Sub INDGvPhysicalInventory_CustomDrawEmptyForeground(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs) Handles INDGvPhysicalInventory.CustomDrawEmptyForeground
        If INDGvPhysicalInventory.RowCount <> 0 Then
            Return
        End If
        Dim drawFormat As New StringFormat()
        drawFormat.LineAlignment = StringAlignment.Center
        drawFormat.Alignment = drawFormat.LineAlignment
        e.Graphics.DrawString("No hay Inventario",
                              e.Appearance.Font, SystemBrushes.ControlDark, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), drawFormat)
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDGvProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGvProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDGvProducts.GetFocusedRow() Is Nothing Then
                Exit Sub
            End If
            Dim product = DirectCast(DirectCast(INDGvProducts.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, InventoryProductXpo)
            Dim args As New SelectProductEventArgs
            args.ProductId = product.Id
            args.CodeNameProduct = product.CodeName
            RaiseEvent SelectProduct(Nothing, args)
        End If
    End Sub
#End Region

#Region "AsyncCompleted"
    Private oldProductIdAsync As Integer = 0
    Private Sub INDGvProducts_AsyncCompleted(sender As Object, e As EventArgs) Handles INDGvProducts.AsyncCompleted
        If DirectCast(DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView).DataSource, DevExpress.Data.Helpers.AsyncListWrapper).Count > 0 Then
            If INDGvProducts.GetFocusedRow() IsNot Nothing AndAlso INDGvProducts.GetFocusedRow().GetType() <> GetType(DevExpress.Data.NotLoadedObject) Then
                Dim product = DirectCast(DirectCast(INDGvProducts.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, InventoryProductXpo)
                If oldProductIdAsync = 0 OrElse oldProductIdAsync <> product.Id Then
                    SearchPhysicalInventory(product.Id)
                    oldProductIdAsync = product.Id
                End If
            End If
        End If
    End Sub
#End Region

#End Region

#Region "METHODS"
    Private Sub SearchPhysicalInventory(productId As Integer)
        INDGvPhysicalInventory.ShowLoadingPanel()
        Threading.Tasks.Task.Factory.StartNew(Sub()
                                                  Using model As New MCtrProducts(Me.Tag)
                                                      Dim listPhysicalInventory = model.ListPhysicalInventoryByProductId(productId)
                                                      If listPhysicalInventory.Count > 0 Then

                                                          If INDGcPhysicalInventory.InvokeRequired Then
                                                              INDGcPhysicalInventory.BeginInvoke(Sub()
                                                                                                     INDGcPhysicalInventory.DataSource = Nothing
                                                                                                     INDGcPhysicalInventory.DataSource = listPhysicalInventory
                                                                                                 End Sub)
                                                          Else
                                                              INDGcPhysicalInventory.DataSource = Nothing
                                                              INDGcPhysicalInventory.DataSource = listPhysicalInventory
                                                          End If
                                                          INDGvPhysicalInventory.HideLoadingPanel()
                                                      Else
                                                          If INDGcPhysicalInventory.InvokeRequired Then
                                                              INDGcPhysicalInventory.BeginInvoke(Sub()
                                                                                                     INDGcPhysicalInventory.DataSource = Nothing
                                                                                                 End Sub)
                                                          Else
                                                              INDGcPhysicalInventory.DataSource = Nothing
                                                          End If
                                                          INDGvPhysicalInventory.HideLoadingPanel()
                                                      End If
                                                  End Using
                                              End Sub)
    End Sub

    Public Sub SetDataSourceProduct(Optional ByVal IdAlmacen As Integer? = Nothing, Optional VirtualStore As Boolean = False, Optional ATCId As Integer? = Nothing, Optional SupplyId As Integer? = Nothing, Optional ProductAndServiceFeeId As Integer? = Nothing)
        If Not DesignMode Then
            Using model As New MCtrProducts(Me.Tag)
                INDGcProducts.DataSource = Nothing
                INDGcPhysicalInventory.DataSource = Nothing
                If ATCId IsNot Nothing Then
                    INDGcProducts.DataSource = model.ListInventoryProductByATC(ATCId)
                ElseIf SupplyId IsNot Nothing Then
                    INDGcProducts.DataSource = model.ListInventoryProductBySupply(SupplyId)
                ElseIf IdAlmacen IsNot Nothing Then
                    INDGcProducts.DataSource = model.ListInventoryProductByStatusByNoClassTypeByAlmacen(IdAlmacen, VirtualStore)
                ElseIf ProductAndServiceFeeId IsNot Nothing Then
                    Dim ListInventoryProduct = model.GeProductsByProductAndServiceId(ProductAndServiceFeeId)
                    Dim listProductsIds = (From item In ListInventoryProduct
                                           Select item.ProductId.Id).ToList()
                    INDGcProducts.DataSource = model.ListInventoryProductsById(listProductsIds)
                Else
                    INDGcProducts.DataSource = model.ListInventoryProductByStatusByNoClassType()
                End If
            End Using
        End If
    End Sub
    Public Sub SetDataSourceProductNoAffectedInventory(Optional ByVal IdAlmacen As Integer? = Nothing, Optional VirtualStore As Boolean = False, Optional ListProductsIds As List(Of Integer) = Nothing)
        If Not DesignMode Then
            Using model As New MCtrProducts(Me.Tag)
                INDGcProducts.DataSource = Nothing
                INDGcPhysicalInventory.DataSource = Nothing
                INDGcProducts.DataSource = model.ListInventoryProductByStatusByNoClassTypeByAlmacenNoAffectedInventory(IdAlmacen, VirtualStore, ListProductsIds)
            End Using
        End If
    End Sub
#End Region

End Class
