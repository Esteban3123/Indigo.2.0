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
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Repository
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP


#End Region

Public Class PopUpFixedAssetPurchaseOrderImport
    Implements IFixedAssetPurchaseOrderImport

#Region "EVENTS"
    ''' <summary>
    ''' evento para obtener el listado de detalles seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event GetListFixedAssetPurchaseOrderItem(sender As Object, e As AddEquipmentPurchaseOrderEventArgs)
#End Region

#Region "TUPLE"

#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' listado de detalles de orden de compra para validar que los FixedAssetos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listFixedAssetPurchaseOrderItemValidation As List(Of FixedAssetPurchaseOrderItem)

#End Region

#Region "Variables"
    Private _listPurchaseRequestDetail As List(Of SP_PurchaseRequestToOrderFixedAsset_Result)
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' propiedad para para pasar el listado de detalles de la orden de compra
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ListFixedAssetPurchaseOrderItemValidation As List(Of FixedAssetPurchaseOrderItem)
        Set(value As List(Of FixedAssetPurchaseOrderItem))
            If value IsNot Nothing Then
                _listFixedAssetPurchaseOrderItemValidation = New List(Of FixedAssetPurchaseOrderItem)(value.ToArray())
            End If
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ListPurchaseRequestDetail As List(Of SP_PurchaseRequestToOrderFixedAsset_Result) Implements IFixedAssetPurchaseOrderImport.ListPurchaseRequestDetail
        Get
            Return _listPurchaseRequestDetail
        End Get
        Set(value As List(Of SP_PurchaseRequestToOrderFixedAsset_Result))
            _listPurchaseRequestDetail = value
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
        ListPurchaseRequestDetail = Nothing
        INDGcFixedAsset.DataSource = ListPurchaseRequestDetail
        _listFixedAssetPurchaseOrderItemValidation = Nothing
    End Sub

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportFixedAssets_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Using presenter As New PFixedAssetPurchaseOrderImport(Me)
            presenter.LoadPurchaseRequest()
        End Using
        INDGcFixedAsset.DataSource = ListPurchaseRequestDetail
        IndigoGridControl1.RefreshGrid(INDGcFixedAsset)
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
        Dim args As New AddEquipmentPurchaseOrderEventArgs
        args.ListFixedAssetPurchaseOrderEquipment = GenerateFixedAssetPurchaseOrderItem()
        If args.ListFixedAssetPurchaseOrderEquipment.Count > 0 Then
            Me.Close()
            RaiseEvent GetListFixedAssetPurchaseOrderItem(Nothing, args)
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
    Private Sub INDGcFixedAsset_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcFixedAsset.MouseDoubleClick
        Dim hitPoint = Me.INDGvFixedAsset.CalcHitInfo(e.Location)
        If hitPoint.Column IsNot Nothing Then
            If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
                If INDGvFixedAsset.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of SP_PurchaseRequestToOrderFixedAsset_Result).Where(Function(s) s.Activated).ToList().Count = INDGvFixedAsset.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of SP_PurchaseRequestToOrderFixedAsset_Result).Count Then
                    INDGvFixedAsset.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of SP_PurchaseRequestToOrderFixedAsset_Result).ForEach(Sub(x) x.Activated = False)
                Else
                    INDGvFixedAsset.DataController.GetAllFilteredAndSortedRows.ToEntityList(Of SP_PurchaseRequestToOrderFixedAsset_Result).ForEach(Sub(x) x.Activated = True)
                End If
                Me.INDGcFixedAsset.RefreshDataSource()
            End If
            Me.INDGcFixedAsset.Invalidate()
        End If
    End Sub
#End Region

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
        INDGcFixedAsset.RepositoryItems.Add(riSpinEdit)
        INDGvFixedAsset.Columns(4).ColumnEdit = riSpinEdit
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvFixedAsset_ShownEditor(ByVal sender As Object, ByVal e As EventArgs) Handles INDGvFixedAsset.ShownEditor
        Dim spin As SpinEdit = TryCast(INDGvFixedAsset.ActiveEditor, SpinEdit)
        If spin IsNot Nothing Then
            Dim row As SP_PurchaseRequestToOrderFixedAsset_Result = TryCast(INDGvFixedAsset.GetFocusedRow(), SP_PurchaseRequestToOrderFixedAsset_Result)
            spin.Properties.MaxValue = row.MaxValue
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptCheActivate_EditValueChanged(sender As Object, e As EventArgs) Handles INDRptCheActivate.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim purcharse = DirectCast(INDGvFixedAsset.GetFocusedRow(), SP_PurchaseRequestToOrderFixedAsset_Result)
        purcharse.Activated = checkControl.EditValue
        SetImageActivateColumn()
        Me.INDGcFixedAsset.RefreshDataSource()
        Me.INDGcFixedAsset.Invalidate()
    End Sub
#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para generar los detalles de la orden de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateFixedAssetPurchaseOrderItem() As List(Of FixedAssetPurchaseOrderItem)
        Dim listFixedAssetPurchaseOrderItemTmp As New List(Of FixedAssetPurchaseOrderItem)
        Dim FixedAssetPurchaseOrderItemTmp As FixedAssetPurchaseOrderItem
        'genero los detalles por orden de compra
        For Each item In ListPurchaseRequestDetail.FindAll(Function(x) x.Activated = True And x.ItemInvalid = False)
            FixedAssetPurchaseOrderItemTmp = New FixedAssetPurchaseOrderItem
            With FixedAssetPurchaseOrderItemTmp
                .Quantity = item.OutstandingQuantity
                .SourceCode = item.Code
                .OrderSource = 1
                .PurchaseRequestDetailId = item.Id
                .BranchOfficeId = item.BranchOfficeId
                .Code = item.Code
                .FunctionalUnitId = item.FunctionalUnitId
                .ItemId = item.FixedAssetItemId
                .Model = item.Model
                .NameBranchOffice = item.CodeNameBranchOffice
                .NameEquipment = item.CodeNameFixedAsset
                .NameFunctionalUnit = item.CodeNameFunctionalUnit
                .NameTrademark = item.CodeNameTrademark
                .OrderSource = 1
                .TrademarkId = item.TrademarkId
            End With
            listFixedAssetPurchaseOrderItemTmp.Add(FixedAssetPurchaseOrderItemTmp)
        Next
        Return listFixedAssetPurchaseOrderItemTmp
    End Function

    ''' <summary>
    ''' valida los datos para poder importarlos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRecords() As List(Of String)
        Dim errors As New List(Of String)

        Dim listPurchaseRequestDetailActivated = ListPurchaseRequestDetail.FindAll(Function(x) x.Activated = True)


        If (listPurchaseRequestDetailActivated.Count = 0) Then
            errors.Add(ResourceManager.GetString("NoSelectPurchaseRequest", MODULE_NAME))
        End If
        If errors.Count = 0 Then
            If _listFixedAssetPurchaseOrderItemValidation IsNot Nothing AndAlso _listFixedAssetPurchaseOrderItemValidation.Count > 0 Then
                'valido que el FixedAsseto no este con la misma orden de compra solo en los items que estan seleccionados
                Dim _listFixedAssetPurchaseOrderItemValidationTmp = _listFixedAssetPurchaseOrderItemValidation.Where(Function(x) x.PurchaseRequestDetailId IsNot Nothing).ToList()
                For Each item In listPurchaseRequestDetailActivated
                    Dim FixedAssetPurchaseOrderItemTmp = _listFixedAssetPurchaseOrderItemValidationTmp.Find(Function(x) x.PurchaseRequestDetailId = item.Id)
                    If FixedAssetPurchaseOrderItemTmp IsNot Nothing Then
                        errors.Add(String.Format(ResourceManager.GetString("FixedAssetAdded", MODULE_NAME), FixedAssetPurchaseOrderItemTmp.NameEquipment, ResourceManager.GetString("PurchaseRequest", MODULE_NAME)))
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
        Dim listActivated = ListPurchaseRequestDetail.FindAll(Function(x) x.Activated = True)
        If listActivated.Count = ListPurchaseRequestDetail.Count Then
            Me.INDGclState.Image = Global.Presentation.FixedAsset.My.Resources.Resources.check
        Else
            Me.INDGclState.Image = Global.Presentation.FixedAsset.My.Resources.Resources.undcheck
        End If
    End Sub

#End Region
End Class