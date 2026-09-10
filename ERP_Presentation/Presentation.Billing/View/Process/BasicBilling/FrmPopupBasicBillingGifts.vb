'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 2023-03-22
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Billing.MVP
Imports System.Text

#End Region

Public Class FrmPopupBasicBillingGifts

#Region "Events"

    Public Event AddBasicBillingDetail(sender As Object, e As AddBasicBillingGiftsEventArgs)

#End Region

#Region "Globals"

    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    Private _presenter As PBasicBillingGifts

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' Variable para establecer si se está editando o no
    ''' </summary>
    Public _editMode As Boolean

    ''' <summary>
    ''' Objeto principal
    ''' </summary>
    Public _BasicBillingGifts As BasicBillingGifts

    ''' <summary>
    ''' Variable para alojar el producto seleccionado
    ''' </summary>
    Private _product As InventoryProduct

    ''' <summary>
    ''' Listado para mostrar los inventarios que tienen stock del producto seleccionado
    ''' </summary>
    Private _listPhysicalInventory As List(Of PhysicalInventory)

    ''' <summary>
    ''' Variable que obtiene el valor de la sesion
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Listado para mostrar los almacenes parametrizados para el producto seleccionado
    ''' </summary>
    Private listWarehouseIds As List(Of Integer)

#End Region

#Region "Fields"

    WriteOnly Property ActionOnControls As Boolean
        Set(value As Boolean)
            INDSeQuantity.Enabled = value
            INDSleProduct.Enabled = value
            INDSleWarehouse.Enabled = value
            INDSleBatchSerial.Enabled = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.CleanControls()
    End Sub

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Async Sub FrmPopupBasicBillingDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False
        Me._indigoSession = SessionValues.Instance
        _presenter = New PBasicBillingGifts

        If Me._editMode Then
            Await LoadControls()
        Else
            Me.CleanControls()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me._indigoSession = Nothing
        Me._editMode = Nothing
        Me._product = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPopupBasicBillingDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleProduct.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDSleProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Me.AssignProductWithCode()
        End If
    End Sub

    Private Sub FrmPopupBasicBillingDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDTxtTotal_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            If listWarehouseIds IsNot Nothing Then
                INDSleWarehouse.Properties.DataSource = _presenter.InitializeWarehouseXPO(listWarehouseIds)
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un producto porfavor"
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub INDPceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        CtrProductsGifts.SetDataSourceProduct()
    End Sub

    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleBatchSerial.QueryPopUp
        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
        CtrPhysicalInventory1.Product = Me._product
        CtrPhysicalInventory1.WareHouseId = INDSleWarehouse.EditValue
        CtrPhysicalInventory1.FormOwner = Me
        CtrPhysicalInventory1.SetListPhysicalInventory()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        INDSeQuantity.EditValue = 0
        INDSleBatchSerial.EditValue = String.Empty
        CtrPhysicalInventory1.CleanControls()
    End Sub

#End Region

#Region "Closed"

    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleBatchSerial.Closed
        Dim quantity As Integer = 0
        Me._listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()

        If Me._listPhysicalInventory.Count > 0 Then
            quantity = Me._listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
            INDSleBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), Me._listPhysicalInventory.Count.ToString(), quantity.ToString())
        Else
            INDSleBatchSerial.EditValue = Nothing
        End If

        INDSeQuantity.EditValue = quantity
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AsyncLoader(True)

        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            AsyncLoader(False)
            Exit Sub
        End If

        AssigningValues()

        RaiseEvent AddBasicBillingDetail(Nothing, New AddBasicBillingGiftsEventArgs With
            {
                .EditMode = _editMode,
                .BasicBillingGifts = _BasicBillingGifts
            }
        )

        AsyncLoader(False)
        If _editMode Then
            Me.Close()
        End If
        Me.CleanControls()
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub CleanControls()
        INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        INDSleProduct.EditValue = Nothing
        INDSleProduct.Text = String.Empty
        INDSleProduct.Properties.ReadOnly = False

        INDSleBatchSerial.EditValue = Nothing
        INDSleBatchSerial.Text = String.Empty

        INDSeQuantity.EditValue = 0
        INDSeQuantity.Properties.ReadOnly = True
        Me._editMode = False
        Me._BasicBillingGifts = New BasicBillingGifts
        Me._product = Nothing
        CtrPhysicalInventory1.CleanControls()
        Me._listPhysicalInventory = Nothing

        INDSleWarehouse.EditValue = Nothing

        ActionOnControls = True
        INDBtnAdd.Enabled = True
        INDBtnAdd.Text = ResourceManager.GetString("Add")

        INDSleProduct.Focus()
    End Sub

    Private Async Function LoadControls() As Task
        Try
            AsyncLoader(True)

            INDBtnAdd.Text = ResourceManager.GetString("Edit")

            With _BasicBillingGifts
                INDSleProduct.Properties.ReadOnly = True
                INDSleProduct.Text = .ProductName
                INDSleWarehouse.EditValue = .WarehouseId
                INDSleWarehouse.Properties.NullText = .CodeNameWarehouse
                INDSeQuantity.EditValue = .Quantity

                Using model As New MBasicBillingDetail(Me.Tag)
                    _product = Await model.GetInventoryProductByIdWithoutAggregates(.ProductId)

                    If _product IsNot Nothing Then
                        CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                        CtrPhysicalInventory1.Product = _product
                        CtrPhysicalInventory1.WareHouseId = .WarehouseId
                        CtrPhysicalInventory1.FormOwner = Me
                        CtrPhysicalInventory1.SetListPhysicalInventory()
                        CtrPhysicalInventory1.SetQuantityPhysicalInventory(.BasicBillingGiftsItem.ToList())
                        _listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
                        INDSleBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME),
                                                                Me._listPhysicalInventory.Count.ToString(), .Quantity.ToString())
                        INDSeQuantity.Properties.ReadOnly = True

                        Dim physicalInventory = model.GetPhysicalInventoryByProductId(.ProductId)
                        listWarehouseIds = (From item In physicalInventory
                                            Where item.WarehouseId.WareHouseType = 5 Or item.WarehouseId.WareHouseType = 0
                                            Group By item.WarehouseId.Id Into Group
                                            From i In Group
                                            Select i.WarehouseId.Id).ToList()
                    End If
                End Using
            End With

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnAdd.Enabled = False
            Throw ex
        End Try
    End Function

    Private Sub AssignProductWithCode()
        If INDSleProduct.Text.Trim <> String.Empty Then
            Me.AsyncLoader(True)

            INDSleProduct.Focus()
            INDSleProduct.ClosePopup()
            CtrPhysicalInventory1.CleanControls()

            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct = INDSleProduct.Text.Split(" - ")
            Dim codeProduct = arrayCodeProduct(0).Trim

            Using model As New MBasicBillingDetail(Me.Tag)
                Me._product = model.GetInventoryProductByCodeWithoutAggregates(codeProduct)
                If Me._product IsNot Nothing AndAlso Me._product.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    Me.CleanControls()
                    Exit Sub
                End If

                INDSleProduct.Text = Me._product.Code + " - " + Me._product.Name

                Dim physicalInventory = model.GetPhysicalInventoryByProductId(_product.Id)
                listWarehouseIds = (From item In physicalInventory
                                    Where item.WarehouseId.WareHouseType = 5 Or item.WarehouseId.WareHouseType = 0
                                    Group By item.WarehouseId.Id Into Group
                                    From i In Group
                                    Select i.WarehouseId.Id).ToList()
            End Using

            Me.AsyncLoader(False)
            Me.ActionOnControls = True

        ElseIf INDSleProduct.Text.Trim = String.Empty Then
            If Me._product IsNot Nothing Then
                INDSleProduct.Text = Me._product.Code + " - " + Me._product.Name
            End If
        End If
    End Sub

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder

        If Not _editMode Then
            If INDSleProduct.EditValue Is Nothing Then
                errors.AppendLine("Debe seleccionar un producto")
            End If
        End If

        If INDSeQuantity.EditValue = 0 Then
            errors.AppendLine("Cantidad en cero")
        End If

        If INDSleWarehouse.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un almacen")
        End If

        Return errors.ToString()
    End Function

    Private Sub AssigningValues()
        With Me._BasicBillingGifts
            .ProductId = _product.Id
            .ProductName = INDSleProduct.Text
            .Quantity = INDSeQuantity.EditValue
            .WarehouseId = INDSleWarehouse.EditValue
            .CodeNameWarehouse = INDSleWarehouse.Text

            .BasicBillingGiftsItem.Clear()
            For Each item In Me._listPhysicalInventory
                Dim basicBillingGiftsItem As New BasicBillingGiftsItem
                With basicBillingGiftsItem
                    .PhysicalInventoryId = item.Id
                    .Quantity = item.QuantityDeliver
                End With
                .BasicBillingGiftsItem.Add(basicBillingGiftsItem)
            Next

        End With
    End Sub

    Private Async Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProductsGifts.SelectProduct
        INDSleProduct.Text = e.CodeNameProduct
        INDSleProduct.Focus()
        INDSleProduct.ClosePopup()
        INDSleBatchSerial.EditValue = String.Empty
        INDSleWarehouse.EditValue = Nothing
        INDSleWarehouse.Properties.DataSource = Nothing
        INDSeQuantity.EditValue = 0
        CtrPhysicalInventory1.CleanControls()

        Me.AsyncLoader(True)

        Using model As New MBasicBillingDetail(Me.Tag)
            Me._product = Await model.GetInventoryProductByIdWithoutAggregates(e.ProductId)

            ''Consulta para sacar los ids de los almacenes que tienen el producto
            Dim physicalInventory = model.GetPhysicalInventoryByProductId(e.ProductId)
            listWarehouseIds = (From item In physicalInventory
                                Where {5, 0}.Contains(item.WarehouseId.WareHouseType)
                                Group By item.WarehouseId.Id Into Group
                                From i In Group
                                Select i.WarehouseId.Id).ToList()

            Me.AsyncLoader(False)
            Me.ActionOnControls = True
        End Using
    End Sub

#End Region

End Class

