'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 27-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
#End Region

Public Class FrmAddProductInventoryAdjustments

#Region "Builder"
    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_Detaill"></param>
    ''' <param name="_listInventoryAdjustmentDetailValidation"></param>
    ''' <param name="_AdjustmentType"></param> 1 = Entrada, 2 = Salida
    ''' <param name="_admissionNumber"></param>
    Public Sub New(_Detaill As InventoryAdjustmentDetail, _listInventoryAdjustmentDetailValidation As List(Of InventoryAdjustmentDetail), _AdjustmentType As Byte, _admissionNumber As String)
        InitializeComponent()

        'si es almacen de custodia no se necesita agregar un concepto
        If _AdjustmentType = 4 Then
            Me.INDLyPceProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLySleConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            Me.INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSleProduct.Enabled = True
        Else
            Me.INDLyPceProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDLciProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If _Detaill IsNot Nothing Then
            INDBtnAddProduct.Text = ResourceManager.GetString("Edit")
            product = _Detaill.InventoryProduct
            _editMode = True
            Detail = _Detaill
        End If
        If _AdjustmentType = 1 Then
            CtrBatchSerial1.RemissionType = ERemissionType.Input
        Else
            CtrBatchSerial1.RemissionType = ERemissionType.Output
        End If
        ListDetailValidation = _listInventoryAdjustmentDetailValidation
        Me.AdmissionNumber = _admissionNumber
    End Sub
#End Region

#Region "Globals"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Obtiene o establece el producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct

    ''' <summary>
    ''' listado de los lotes
    ''' </summary>
    ''' <remarks></remarks>
    Dim listBatchSerial As List(Of BatchSerial)

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim Detail As InventoryAdjustmentDetail

    ''' <summary>
    ''' Objeto que representa el listado de detalles utilziado para valdiar los existentes
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDetailValidation As List(Of InventoryAdjustmentDetail)

    ''' <summary>
    ''' Objeto que representa el listado que hay en el inventario fisico del producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim physical As List(Of PhysicalInventory)

    Public AdjustmentConcept As AdjustmentConcept
    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer
    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _operatingUnitId As Integer
    ''' <summary>
    ''' tipo de ajuste de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _typeAdjustment As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    Dim AdmissionNumber As String
#End Region

#Region "Properties"
    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Dim _editMode As Boolean = False
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el mensaje que se va a mostrar
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            LayoutControl1.BeginUpdate()
            INDTxtQuantity.Enabled = value
            INDPceBatchSerial.Enabled = value
            INDTxtUnid.Enabled = value
            INDTxtUnitValue.Enabled = value
            If _typeAdjustment <> 4 Then
                INDSleProduct.Enabled = value
            End If
            INDPceProduct.Enabled = value
            LayoutControl1.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <remarks></remarks>
    Dim inventoryAdjustmentDetail As InventoryAdjustmentDetail
    Public WriteOnly Property InventoryControlDetailEdit As InventoryAdjustmentDetail
        Set(value As InventoryAdjustmentDetail)
            inventoryAdjustmentDetail = value
        End Set
    End Property
    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WareHouseId As Integer
        Set(value As Integer)
            _wareHouseId = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para establecer el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property operatingUnitId As Integer
        Set(value As Integer)
            _operatingUnitId = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para establecer el tipo de ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property typeAdjustment As Integer
        Set(value As Integer)
            _typeAdjustment = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los conceptos de ajuste de inventario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentConceptId As Integer?
        Get
            Return INDSleConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleConcept.EditValue = value
        End Set
    End Property

#End Region

#Region "Events"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInventoryAdjustmentDetail(sender As Object, e As AddProductInventoryAdjustmentsEventArg)
#End Region

#Region "Handless"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        product = Nothing
        listBatchSerial = Nothing
        Detail = Nothing
        ListDetailValidation = Nothing
        physical = Nothing
        AdjustmentConcept = Nothing
        _wareHouseId = Nothing
        _operatingUnitId = Nothing
        _typeAdjustment = Nothing
    End Sub

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddProductInventoryAdjustments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        If _editMode = True Then
            'si esta editando un registro
            Me.LoadControls()
        Else
            'si es un nuevo registro
            CleanControls()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmAddProductInventoryAdjustments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddProductInventoryAdjustments_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDPceProduct.Focus()
    End Sub
#End Region

#Region "SelectProduct"
    ''' <summary>
    ''' Evento que selecciona el producto en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProduct.Text = e.CodeNameProduct
        INDPceProduct.Focus()
        INDPceProduct.ClosePopup()
        CtrBatchSerial1.CleanControls()
        INDTxtQuantity.EditValue = 0
        INDPceBatchSerial.EditValue = Nothing
        ActionsControls = True
        Using model As New MInventoryProduct(Me.Tag)
            product = model.GetInventoryProductByIdSimple(e.ProductId)
            INDTxtUnid.Text = product.PackingUnitDescription
            INDTxtUnitValue.EditValue = product.ProductCost
        End Using
        Using model As New MSubGroup(Me.Tag)
            If product.ProductSubGroup IsNot Nothing Then
                If product.ProductSubGroup.HandlesBatch = True Then
                    INDTxtQuantity.Properties.ReadOnly = True
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDPceBatchSerial.Focus()
                    If _editMode = True Then
                        'instancio el listado para poder hacer la validacion de los lotes cuando se esta editando y saber si esta sin registros
                        listBatchSerial = New List(Of BatchSerial)
                    End If
                Else
                    INDTxtQuantity.Properties.ReadOnly = False
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTxtQuantity.Focus()
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El producto " + product.Code + " - " + product.Name + " no tiene un subgrupo asociado"
                CleanControls()
            End If
        End Using
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Consulta y asigna los produuctos al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProduct.QueryPopUp
        If CtrProducts1.DataSource Is Nothing Then
            CtrProducts1.SetDataSourceProduct()
        End If
    End Sub

    ''' <summary>
    ''' Consulta y asigna los lotes del producto seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceBatchSerial.QueryPopUp
        If product IsNot Nothing Then
            CtrBatchSerial1.Product = product
            CtrBatchSerial1.WarehouseId = _wareHouseId
            CtrBatchSerial1.AdmissionNumber = AdmissionNumber
            CtrBatchSerial1.Custody = (Not String.IsNullOrEmpty(AdmissionNumber))
            CtrBatchSerial1.FormOwner = Me
            If AdjustmentConcept IsNot Nothing Then
                If AdjustmentConcept.ShowExpiredProduct Then
                    CtrBatchSerial1.SetListBatchSerial()
                Else
                    CtrBatchSerial1.SetListBatchSerial(False)
                End If
            ElseIf (Not String.IsNullOrEmpty(AdmissionNumber)) Then
                CtrBatchSerial1.SetListBatchSerial()
            Else
                CtrBatchSerial1.SetListBatchSerial(False)
            End If
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUP(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            Using model As New MDashBoardPharmacy(Me.Tag)
                INDSleProduct.Properties.DataSource = model.GetPhysicalInventoryCustodyByWareHouse(Me.AdmissionNumber, Me._wareHouseId)
            End Using
        End If
    End Sub

    Private Sub INDSleConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleConcept.QueryPopUp
        If INDSleConcept.Properties.DataSource Is Nothing Then
            Using Model As New MInventoryAdjustments("")
                INDSleConcept.Properties.DataSource = Model.ListAdjustmentConceptsByTypeXpo(Me._typeAdjustment)
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Despliega el formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click para abrir el formulario y agregar o consultar nuevos Conceptos de ajuste de invtentario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmConceptsInventorySettings()
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "Closed"
    ''' <summary>
    ''' Evento que se dispara cuando el control del batch se cierra y agrega los datos al control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerial.Closed
        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
        If listBatchSerial.Count = 0 Then
            INDPceBatchSerial.EditValue = String.Empty
        Else
            INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", NAME_MODULE), listBatchSerial.Count.ToString())
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Evento para cerrar el frontal con el boton escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpProductsEntranceVoucher_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento qeu coloca el foco sobre el boton de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceBatchSerial.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDPceBatchSerial.Text <> String.Empty Then
                INDBtnAddProduct.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento qeu coloca el foco sobre el boton de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDTxtQuantity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Exit Sub
            End If
            If INDTxtQuantity.Text <> String.Empty Then
                INDBtnAddProduct.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cierra el frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupAddProductsInventoryControl_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Async Sub INDPceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#Region "Click"
    Private Async Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        Try
            INDBtnAddProduct.Enabled = False
            Dim errors = ValidateControlsPopUp()
            If errors.Length > 0 Then
                INDBtnAddProduct.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            '********************* Defino el evento de retorno ****************'
            Dim args As New AddProductInventoryAdjustmentsEventArg
            '********************* Verifico si el item es para actualziar o agregar ****************'
            Detail.InventoryProduct = product
            Detail.Quantity = INDTxtQuantity.EditValue
            Detail.ProductCodeName = INDPceProduct.Text
            Detail.consumptionUnit = product.PackingUnitDescription
            Detail.UnitValue = INDTxtUnitValue.EditValue
            Detail.ProductUnid = product.PackingUnitDescription

            ''si es almacen de custodia no agrego la informacion del concepto
            If _typeAdjustment <> 4 Then
                Detail.AdjustmentConceptId = INDSleConcept.EditValue
                Detail.CodeNameAdjustmentConcept = INDSleConcept.Text
                Detail.CodeNameCostCenter = AdjustmentConcept.CostCenterCodeName
            End If

            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If _editMode = True Then
                    If Detail.Id > 0 Then
                        Detail.ChangeTracker.State = ObjectState.Modified
                    End If
                    If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count > 0 Then
                        While Detail.InventoryAdjustmentDetailBatchSerial.Count > 0
                            If Detail.InventoryAdjustmentDetailBatchSerial(0).Id > 0 Then
                                Detail.InventoryAdjustmentDetailBatchSerial(0).MarkAsDeleted()
                            Else
                                Detail.InventoryAdjustmentDetailBatchSerial.Remove(Detail.InventoryAdjustmentDetailBatchSerial(0))
                            End If
                        End While
                    End If
                End If

                If listBatchSerial IsNot Nothing Then
                    For Each item In listBatchSerial
                        Dim detailBatchSerial As New InventoryAdjustmentDetailBatchSerial
                        detailBatchSerial.BatchSerialId = item.Id
                        detailBatchSerial.Quantity = item.Quantity
                        'detailBatchSerial.CodeBatchSerial = item.BatchCode
                        Detail.InventoryAdjustmentDetailBatchSerial.Add(detailBatchSerial)
                    Next
                End If
            Else
                ' elimino todos los registros de RemissionEntranceDetailBatchSerial
                While Detail.InventoryAdjustmentDetailBatchSerial.Count > 0
                    If Detail.InventoryAdjustmentDetailBatchSerial(0).Id > 0 Then
                        Detail.InventoryAdjustmentDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        Detail.InventoryAdjustmentDetailBatchSerial.Remove(Detail.InventoryAdjustmentDetailBatchSerial(0))
                    End If
                End While
                Dim detailBatchSerial As New InventoryAdjustmentDetailBatchSerial
                detailBatchSerial.Quantity = INDTxtQuantity.EditValue
                Detail.InventoryAdjustmentDetailBatchSerial.Add(detailBatchSerial)
            End If

            If String.IsNullOrEmpty(Me.AdmissionNumber) Then
                'validacion de stock del producto
                Dim movementType As InventoryStaticServices.MovementType
                If _typeAdjustment = 1 Then
                    movementType = InventoryStaticServices.MovementType.Input
                ElseIf _typeAdjustment = 2 Then
                    movementType = InventoryStaticServices.MovementType.OutPut
                End If

                Using modelSettings As New MSettingInventory(Me.Tag)
                    Dim resulValidateStock = Await modelSettings.ValidateStock(Detail.ProductId, _operatingUnitId, _wareHouseId, Detail.Quantity, movementType)
                    If resulValidateStock.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = resulValidateStock.Message
                    End If
                End Using
            End If

            If _editMode Then
                args.EditMode = True
                args.inventoryAdjustmentDetail = Detail
            Else
                args.inventoryAdjustmentDetail = Detail
                ListDetailValidation.Add(Detail)
            End If

            RaiseEvent AddInventoryAdjustmentDetail(Nothing, args)
            INDBtnAddProduct.Enabled = True
            If _editMode Then
                product = Nothing
                Me.Close()
            Else
                CleanControls()
                If INDSleConcept.Properties.NullText IsNot Nothing Then
                    INDSleProduct.Enabled = True
                    INDPceProduct.Enabled = True
                End If
            End If
        Catch ex As Exception
            INDBtnAddProduct.Enabled = True
            Throw ex
        End Try
    End Sub
#End Region

#Region "ChangeQuantity"
    ''' <summary>
    ''' Modifica el valor de la cantidad cuando se asigna el lote
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        INDTxtQuantity.EditValue = e.Quantity
    End Sub
#End Region

#Region "Popup"
    ''' <summary>
    ''' Evento que se utiliza para dirigir el foco en la regilla de los lotes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerial.Popup
        CtrBatchSerial1.SetFocusGrid()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que selecciona el producto en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProduct.EditValueChanged
        'Separo el string escrito en el control de producto
        Dim arrayCodeProduct As String() = INDSleProduct.Text.Split(" - ")
        'Capturo el codigo del producto
        Dim codeProduct As String = arrayCodeProduct(0).Trim

        ''se consulta solo si hay selecionado un producto
        If codeProduct <> "" AndAlso codeProduct IsNot Nothing Then
            Using model As New MInventoryProduct(Me.Tag)
                Dim resultProduct = Await model.GetInventoryProduct(codeProduct)
                If resultProduct.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Sub
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    CleanControls()
                    Exit Sub
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls()
                    Exit Sub
                End If
                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que carga los conceptos de ajuste de inventario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleConcept.EditValueChanged
        INDSleProduct.Enabled = True
        INDPceProduct.Enabled = True
        INDTxtUnitValue.Properties.ReadOnly = True

        If AdjustmentConceptId Is Nothing Then
            INDSleConcept.EditValue = Nothing
            INDSleConcept.Properties.NullText = String.Empty
            Exit Sub
        End If

        Using Model As New MAdjustmentConcept("")
            Dim ac = Await Model.GetAdjustmentConceptById(INDSleConcept.EditValue)
            AdjustmentConcept = ac?.ObjectEmbbeded
        End Using

        If AdjustmentConcept IsNot Nothing Then
            INDSleConcept.Properties.NullText = String.Format("{0} - {1}", AdjustmentConcept.Code, AdjustmentConcept.Name)
        End If
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Abre el formulario para adicion de productos
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
        product = Nothing
        Detail = New InventoryAdjustmentDetail

        INDPceProduct.Text = String.Empty
        INDTxtUnid.Text = String.Empty
        INDTxtQuantity.EditValue = 0
        INDTxtUnitValue.EditValue = 0
        INDPceBatchSerial.Text = String.Empty
        INDSleProduct.Text = String.Empty

        CtrBatchSerial1.CleanControls()
        ActionsControls = False
        BarraBotones.FilterDataSource = Nothing
        _editMode = False

        INDPceProduct.Focus()
        INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        INDBtnAddProduct.Text = ResourceManager.GetString("Edit")

        With Detail
            Using model As New MInventoryProduct(Me.Tag)
                LayoutControl1.BeginUpdate()
                product = model.GetInventoryProductByIdSimple(.ProductId)
                If Not String.IsNullOrEmpty(AdmissionNumber) Then
                    INDSleProduct_QueryPopUP(Nothing, Nothing)
                    INDSleProduct.EditValue = product.Id
                    INDSleProduct.Properties.ReadOnly = True
                End If
                INDPceProduct.Text = product.Code + " - " + product.Name
                INDPceProduct.Properties.ReadOnly = True
                INDTxtUnid.Text = product.PackingUnitDescription
                INDTxtUnitValue.EditValue = product.ProductCost
            End Using
            If product.ProductSubGroupId = 0 Then
                Mensaje(EeventViewerImages.Informacion) = "El producto no tiene Subgrupo, no puede continuar."
                Me.Close()
                Exit Sub
            End If

            Using model As New MSubGroup(Me.Tag)
                Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                    INDTxtQuantity.Properties.ReadOnly = True
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDTxtQuantity.Properties.ReadOnly = False
                    INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using

            AdjustmentConceptId = If(.AdjustmentConceptId IsNot Nothing, .AdjustmentConceptId, If(AdjustmentConcept IsNot Nothing, AdjustmentConcept.Id, Nothing))

            INDTxtQuantity.EditValue = .Quantity
            If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If .InventoryAdjustmentDetailBatchSerial.Count = 0 Then
                    INDPceBatchSerial.EditValue = String.Empty
                Else
                    INDPceBatchSerial.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", NAME_MODULE), .InventoryAdjustmentDetailBatchSerial.Count.ToString())
                End If

                CtrBatchSerial1.Product = product
                CtrBatchSerial1.WarehouseId = _wareHouseId
                CtrBatchSerial1.FormOwner = Me

                If AdjustmentConcept IsNot Nothing Then
                    If AdjustmentConcept.ShowExpiredProduct Then
                        CtrBatchSerial1.SetListBatchSerial()
                    Else
                        CtrBatchSerial1.SetListBatchSerial(False)
                    End If
                ElseIf (Not String.IsNullOrEmpty(Me.AdmissionNumber)) Then
                    CtrBatchSerial1.SetListBatchSerial()
                Else
                    CtrBatchSerial1.SetListBatchSerial(False)
                End If

                CtrBatchSerial1.SetQuantityBatchSerial(.InventoryAdjustmentDetailBatchSerial.ToList())
            End If
        End With
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' valida los controles del formualario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopUp() As String
        Dim errors As New StringBuilder

        If product Is Nothing Then
            errors.AppendLine(INDLyPceProduct.Text + ResourceManager.GetString("Empty"))
        Else
            If Not _editMode Then
                If ListDetailValidation IsNot Nothing AndAlso ListDetailValidation.Any() Then
                    Dim detailTmp = ListDetailValidation.FirstOrDefault(Function(x) x.ProductId = product.Id)
                    If detailTmp IsNot Nothing Then
                        errors.AppendLine(String.Format("El producto {0} ya se encuetra agregado en el listado.", product.Code + " - " + product.Name))
                    End If
                End If
            End If
        End If

        If INDTxtQuantity.EditValue = 0 Then
            errors.AppendLine(INDLyTxtQuantity.Text + ResourceManager.GetString("Empty"))
        End If

        If INDLyPceBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If _editMode = False Then
                If listBatchSerial Is Nothing Then
                    errors.AppendLine(INDLyPceBatchSerial.Text + ResourceManager.GetString("Empty"))
                End If
                If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                    errors.AppendLine(INDLyPceBatchSerial.Text + ResourceManager.GetString("Empty"))
                End If
            Else
                If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count = 0 Then
                    errors.AppendLine(INDLyPceBatchSerial.Text + ResourceManager.GetString("Empty"))
                End If
            End If
        End If

        If _typeAdjustment <> 4 Then
            If AdjustmentConceptId = 0 OrElse INDSleConcept.EditValue Is Nothing Then
                errors.AppendLine(INDLySleConcept.Text + ResourceManager.GetString("Empty"))
            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProduct.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProduct.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim


            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(codeProduct)
                If resultProduct.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto con código {0} no existe.", codeProduct)
                    CleanControls()
                    Exit Function
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls()
                    Exit Function
                End If
                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        ElseIf INDPceProduct.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProduct.Text = product.Code + " - " + product.Name
            End If
        End If
    End Function
#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Limpia los controles, deshace los cambios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

End Class