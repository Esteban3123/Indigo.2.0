'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 08-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmPurchaseOrderImport

#Region "EVENTS"

    ''' <summary>
    ''' evento para obtener el listado de detalles seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListPurchaseOrderDetail(sender As Object, e As AddProductPurchaseOrder)

#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' listado de la orden de compra
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPurchaseRequestDetail As New List(Of PurchaseRequestDetail)

    ''' <summary>
    ''' listado de detalles de orden de compra para validar que los productos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPurchaseOrderDetailValidation As List(Of PurchaseOrderDetail)

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' propiedad para para pasar el listado de detalles de la orden de compra
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListPurchaseOrderDetailValidation As List(Of PurchaseOrderDetail)
        Set(value As List(Of PurchaseOrderDetail))
            If value IsNot Nothing Then
                _listPurchaseOrderDetailValidation = New List(Of PurchaseOrderDetail)(value.ToArray())
            End If
        End Set
    End Property

#End Region

#Region "HANDLES"

#Region "Load"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listPurchaseRequestDetail = Nothing
        _listPurchaseOrderDetailValidation = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LoadPurchaseRequest()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPurchaseOrderImport_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim riSpinEdit As New RepositoryItemSpinEdit
        riSpinEdit.MaxLength = 9
        riSpinEdit.MinValue = 1
        riSpinEdit.MaxValue = 2147483646
        riSpinEdit.Increment = 1
        riSpinEdit.IsFloatValue = False
        INDGcProduct.RepositoryItems.Add(riSpinEdit)
        INDGvProduct.Columns(4).ColumnEdit = riSpinEdit
    End Sub

#End Region

#Region "KeyDown"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPurchaseOrderImport_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAccept_Click(sender As Object, e As EventArgs) Handles INDSmbAccept.Click
        Dim errors = ValidateRecords()
        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
        Dim args As New AddProductPurchaseOrder
        args.ListPurchaseOrderDetail = GeneratePurchaseOrderDetail()
        If args.ListPurchaseOrderDetail.Count > 0 Then
            Me.Close()
            RaiseEvent GetListPurchaseOrderDetail(Nothing, args)
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' activa o desactiva todos los items del listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGcProduct_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcProduct.MouseDoubleClick
        Dim hitPoint = Me.INDGvProduct.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
                If INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseRequestDetail).Where(Function(s) s.Activated).ToList().Count = INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseRequestDetail).Count Then
                    INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseRequestDetail).ForEach(Sub(x) x.Activated = False)
                Else
                    INDGvProduct.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of PurchaseRequestDetail).ForEach(Sub(x) x.Activated = True)
                End If
                Me.INDGcProduct.RefreshDataSource()
            End If
            Me.INDGcProduct.Invalidate()
        End If
    End Sub

#End Region

#Region ""

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvProduct_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles INDGvProduct.ShownEditor
        Dim spin As SpinEdit = TryCast(INDGvProduct.ActiveEditor, SpinEdit)
        If spin IsNot Nothing Then
            Dim row As PurchaseRequestDetail = TryCast(INDGvProduct.GetFocusedRow(), PurchaseRequestDetail)
            spin.Properties.MaxValue = row.OutstandingQuantity
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim purcharse = DirectCast(INDGvProduct.GetFocusedRow(), PurchaseRequestDetail)
        purcharse.Activated = checkControl.EditValue
        SetImageActivateColumn()
        Me.INDGcProduct.RefreshDataSource()
        Me.INDGcProduct.Invalidate()
    End Sub

#End Region

#End Region

#Region "METHODS"

    Private Sub LoadPurchaseRequest()
        Using model As New MPurchaseRequest(Me.Tag)
            listPurchaseRequestDetail = New List(Of PurchaseRequestDetail)
            Dim listPurchaseRequestDetailXpo As XPCollection = model.LoadPurchaseRequestDetailToOrder()
            If listPurchaseRequestDetailXpo IsNot Nothing AndAlso listPurchaseRequestDetailXpo.Count > 0 Then
                For Each itemXpo As ViewPurchaseRequestToOrderXpo In listPurchaseRequestDetailXpo
                    listPurchaseRequestDetail.Add(New PurchaseRequestDetail With
                    {
                        .Id = itemXpo.Id,
                        .PurchaseRequestId = itemXpo.PurchaseRequestId,
                        .Code = itemXpo.PurchaseRequestCode,
                        .DocumentDate = itemXpo.CreationDate,
                        .InventoryProductId = itemXpo.InventoryProductId,
                        .ProductCode = itemXpo.ProductCode,
                        .ProductName = itemXpo.ProductName,
                        .DescriptionProduct = String.Format("{0} - {1}", itemXpo.ProductCode, itemXpo.ProductName),
                        .OutstandingQuantity = itemXpo.OutstandingQuantity
                    })
                Next
            End If
            INDGcProduct.DataSource = Nothing
            INDGcProduct.DataSource = listPurchaseRequestDetail
            INDGcProduct.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para generar los detalles de la orden de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GeneratePurchaseOrderDetail() As TrackableCollection(Of PurchaseOrderDetail)
        Dim listPurchaseOrderDetailTmp As New TrackableCollection(Of PurchaseOrderDetail)
        Dim PurchaseOrderDetailTmp As PurchaseOrderDetail = Nothing

        'genero los detalles por orden de compra
        For Each item In listPurchaseRequestDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            PurchaseOrderDetailTmp = New PurchaseOrderDetail
            With PurchaseOrderDetailTmp
                .ProductCode = item.ProductCode
                .ProductName = item.ProductName
                .Quantity = item.OutstandingQuantity
                .SourceCode = item.Code
                .OrderSource = 1
                .PurchaseRequestDetailId = item.Id
                .ProductId = item.InventoryProductId
            End With
            listPurchaseOrderDetailTmp.Add(PurchaseOrderDetailTmp)
        Next

        Return listPurchaseOrderDetailTmp
    End Function

    ''' <summary>
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)

        Dim listPurchaseRequestDetailActivated = listPurchaseRequestDetail.FindAll(Function(x) x.Activated = True)
        If (listPurchaseRequestDetailActivated.Count = 0) Then
            errors.Add(ResourceManager.GetString("NoSelectPurchaseRequest", MODULE_NAME))
        End If
        If errors.Count = 0 Then
            If _listPurchaseOrderDetailValidation IsNot Nothing AndAlso _listPurchaseOrderDetailValidation.Count > 0 Then
                'valido que el producto no este con la misma orden de compra solo en los items que estan seleccionados
                Dim _listPurchaseOrderDetailValidationTmp = _listPurchaseOrderDetailValidation.Where(Function(x) x.PurchaseRequestDetailId IsNot Nothing).ToList()
                For Each item In listPurchaseRequestDetailActivated
                    Dim PurchaseOrderDetailTmp = _listPurchaseOrderDetailValidationTmp.Find(Function(x) x.PurchaseRequestDetailId = item.Id)
                    If PurchaseOrderDetailTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("ProductAdded", MODULE_NAME), PurchaseOrderDetailTmp.CodeNameProduct, ResourceManager.GetString("PurchaseRequest", MODULE_NAME)))
                        item.ItemInvalid = True
                    End If
                Next
            End If
        End If

        Return errors
    End Function
    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn()
        Dim listActivated = listPurchaseRequestDetail.FindAll(Function(x) x.Activated = True)
        If listActivated.Count = listPurchaseRequestDetail.Count Then
            Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.check
        Else
            Me.INDGclState.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
        End If
    End Sub

#End Region
End Class