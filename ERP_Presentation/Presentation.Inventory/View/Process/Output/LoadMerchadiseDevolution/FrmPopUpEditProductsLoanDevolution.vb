'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 23/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

Public Class FrmPopUpEditProductsLoanDevolution

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event EditProductLoanMerchandiseDevolutionDetail(sender As Object, e As EditProductLoanMerchandiseDevolutionDetailEventArgs)

#End Region

#Region "Constantes"

    Const MODULE_NAME As String = "Inventory"

#End Region

#Region "variables"

    ''' <summary>
    ''' variable para saber si el prestamo  es una entrada o salida
    ''' </summary>
    ''' <remarks></remarks>
    Dim _loanType As ELeanType

    ''' <summary>
    ''' id del almacen 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _wareHouseId As Integer

    ''' <summary>
    ''' Objeto detalle de un prestamo de mercancia
    ''' </summary>
    ''' <remarks></remarks>
    Dim LoanMerchandiseDevolutionDetail As LoanMerchandiseDevolutionDetail

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
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventory As List(Of PhysicalInventory)

    ''' <summary>
    ''' Propiedad para asignar la cantidad que se va editar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _outstandingQuantity As Integer

#End Region

#Region "propiedades"

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            INDSeQuantity.Enabled = value
            INDPceBatchSerialInput.Enabled = value
            INDPceBatchSerialOutput.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Mensajes
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
    ''' propiedad para asignar el tipo de solicitud de prestamo, entrada o salida
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property LoanType As ELeanType
        Set(value As ELeanType)
            _loanType = value
            If value = ELeanType.Input Then
                CtrPhysicalInventory1.MovementType = InventoryStaticServices.MovementType.OutPut
                CtrPhysicalInventory1.WareHouseId = _wareHouseId
            Else
                CtrBatchSerial1.RemissionType = ELeanType.Input
                CtrBatchSerial1.WarehouseId = _wareHouseId
            End If
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
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property LoanMerchandiseDetailEdit As LoanMerchandiseDevolutionDetail
        Set(value As LoanMerchandiseDevolutionDetail)
            LoanMerchandiseDevolutionDetail = value
            _outstandingQuantity = LoanMerchandiseDevolutionDetail.OutstandingQuantity + LoanMerchandiseDevolutionDetail.Quantity
            LoanMerchandiseDevolutionDetail.OutstandingQuantity = _outstandingQuantity
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar la presentación del producto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property PresentationProduct As String
        Set(value As String)
            INDtxtPresentation.Text = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpiar Controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ActionsControls = False

        INDPceProduct.Text = String.Empty
        INDPceProduct.Properties.ReadOnly = False
        INDtxtPresentation.Text = String.Empty
        INDSeQuantity.EditValue = 1
        INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDPceBatchSerialInput.EditValue = Nothing
        INDPceBatchSerialOutput.EditValue = Nothing
        CtrBatchSerial1.CleanControls()
        CtrPhysicalInventory1.CleanControls()

        product = Nothing
        listBatchSerial = Nothing
        listPhysicalInventory = Nothing

        INDSeQuantity.Focus()
    End Sub

    ''' <summary>
    ''' Carga de datos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControls()
        With LoanMerchandiseDevolutionDetail
            Using model As New MInventoryProduct(Me.Tag)
                product = model.GetInventoryProductByIdSimple(.ProductId)
                INDPceProduct.Text = product.Code + " - " + product.Name
                INDPceProduct.Properties.ReadOnly = True
                INDtxtPresentation.Text = product.Presentation
                INDSeQuantity.EditValue = .Quantity
            End Using
            Using model As New MSubGroup(Me.Tag)
                Dim subGroup = model.GetProductSubGroupByIdSimple(product.ProductSubGroupId)
                If subGroup.ObjectEmbbeded.HandlesBatch = True Then
                    INDSeQuantity.Properties.ReadOnly = True
                    If _loanType = ELeanType.Input Then
                        INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        If .LoanMerchandiseDevolutionDetailBatchSerial.Count = 0 Then
                            INDPceBatchSerialOutput.EditValue = String.Empty
                        Else
                            INDPceBatchSerialOutput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), .LoanMerchandiseDevolutionDetailBatchSerial.Count.ToString())
                        End If
                        CtrPhysicalInventory1.Product = product
                        CtrPhysicalInventory1.FormOwner = Me
                        CtrPhysicalInventory1.SetListPhysicalInventory()
                        CtrPhysicalInventory1.SetQuantityPhysicalInventory(.LoanMerchandiseDevolutionDetailBatchSerial.ToList())
                        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
                    ElseIf INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If .LoanMerchandiseDevolutionDetailBatchSerial.Count = 0 Then
                            INDPceBatchSerialInput.EditValue = String.Empty
                        Else
                            INDPceBatchSerialInput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), .LoanMerchandiseDevolutionDetailBatchSerial.Count.ToString())
                        End If
                        CtrBatchSerial1.Product = product
                        CtrBatchSerial1.FormOwner = Me
                        CtrBatchSerial1.SetListBatchSerial()
                        CtrBatchSerial1.SetQuantityBatchSerial(.LoanMerchandiseDevolutionDetailBatchSerial.ToList())
                        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
                    End If
                Else
                    INDSeQuantity.Properties.ReadOnly = False
                    INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using
        End With
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If product Is Nothing Then
            errors.AppendLine(INDLciProduct.Text + ResourceManager.GetString("Empty"))
        End If
        If INDSeQuantity.EditValue Is Nothing OrElse INDSeQuantity.EditValue = 0 Then
            errors.AppendLine(INDLciQuantity.Text + ResourceManager.GetString("Empty"))
        End If
        If INDSeQuantity.EditValue > _outstandingQuantity Then
            errors.AppendLine(String.Format(ResourceManager.GetString("InventoryAnmountDevolutionMayLoan", MODULE_NAME), INDSeQuantity.EditValue.ToString(), _outstandingQuantity.ToString()))
        End If
        If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listPhysicalInventory Is Nothing OrElse listPhysicalInventory.Count = 0 Then
                errors.AppendLine(INDLciBatchSerialOutput.Text + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If listBatchSerial Is Nothing OrElse listBatchSerial.Count = 0 Then
                errors.AppendLine(INDLciBatchSerialInput.Text + ResourceManager.GetString("Empty"))
            End If
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        With LoanMerchandiseDevolutionDetail
            .ProductId = product.Id
            .CodeNameProduct = product.Code + " - " + product.Name
            .Quantity = INDSeQuantity.EditValue
            .OutstandingQuantity = .OutstandingQuantity - .Quantity

            'Eliminamos los detalles previos
            While .LoanMerchandiseDevolutionDetailBatchSerial.Count > 0
                If .LoanMerchandiseDevolutionDetailBatchSerial(0).Id > 0 Then
                    .LoanMerchandiseDevolutionDetailBatchSerial(0).MarkAsDeleted()
                Else
                    .LoanMerchandiseDevolutionDetailBatchSerial.Remove(.LoanMerchandiseDevolutionDetailBatchSerial(0))
                End If
            End While

            'Agregamos el lote
            If INDLciBatchSerialOutput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If listPhysicalInventory IsNot Nothing Then
                    For Each item In listPhysicalInventory
                        Dim LoanDevolutionDetailBatchSerial As New LoanMerchandiseDevolutionDetailBatchSerial
                        LoanDevolutionDetailBatchSerial.PhysicalInventoryId = item.Id
                        LoanDevolutionDetailBatchSerial.BatchSerialId = item.BatchSerialId
                        LoanDevolutionDetailBatchSerial.CodeBatchSerial = item.CodeNameBatchSerial
                        LoanDevolutionDetailBatchSerial.Quantity = item.QuantityDeliver
                        .LoanMerchandiseDevolutionDetailBatchSerial.Add(LoanDevolutionDetailBatchSerial)
                    Next
                End If
            ElseIf INDLciBatchSerialInput.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If listBatchSerial IsNot Nothing Then
                    For Each item In listBatchSerial
                        Dim LoanDevolutionDetailBatchSerial As New LoanMerchandiseDevolutionDetailBatchSerial
                        LoanDevolutionDetailBatchSerial.PhysicalInventoryId = Nothing
                        LoanDevolutionDetailBatchSerial.BatchSerialId = item.Id
                        LoanDevolutionDetailBatchSerial.CodeBatchSerial = item.BatchCode
                        LoanDevolutionDetailBatchSerial.Quantity = item.Quantity
                        .LoanMerchandiseDevolutionDetailBatchSerial.Add(LoanDevolutionDetailBatchSerial)
                    Next
                End If
            Else
                Dim LoanDevolutionDetailBatchSerial As New LoanMerchandiseDevolutionDetailBatchSerial
                LoanDevolutionDetailBatchSerial.PhysicalInventoryId = Nothing
                LoanDevolutionDetailBatchSerial.BatchSerialId = Nothing
                LoanDevolutionDetailBatchSerial.Quantity = INDSeQuantity.EditValue
                .LoanMerchandiseDevolutionDetailBatchSerial.Add(LoanDevolutionDetailBatchSerial)
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmPopupProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.None)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = False

        LoadControls()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _loanType = Nothing
        _wareHouseId = Nothing
        _outstandingQuantity = Nothing
        LoanMerchandiseDevolutionDetail = Nothing
        product = Nothing
        listBatchSerial = Nothing
        listPhysicalInventory = Nothing
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmPopupProduct_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Activated"

    Private Sub FrmPopupProduct_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDSeQuantity.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmPopUpAddProductsLoanDevolution_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDSeQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDSeQuantity.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Sub INDPceQuantity_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceBatchSerialOutput.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Sub INDPceBatchSerial_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceBatchSerialInput.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDPceBatchSerialInput_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerialInput.Popup
        CtrBatchSerial1.SetFocusGrid()
    End Sub

    Private Sub INDPceBatchSerialOutput_Popup(sender As Object, e As EventArgs) Handles INDPceBatchSerialOutput.Popup
        CtrPhysicalInventory1.SetFocusGrid()
    End Sub

#End Region

#Region "ChangeQuantity"

    Private Sub CtrBatchSerial1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrBatchSerial1.ChangeQuantity
        INDSeQuantity.EditValue = e.Quantity
    End Sub

    Private Sub CtrPhysicalInventory1_ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs) Handles CtrPhysicalInventory1.ChangeQuantity
        INDSeQuantity.EditValue = e.Quantity
    End Sub

#End Region

#Region "Closed"

    ''' <summary>
    ''' Obtiene la lista de lotes y cantidades  - cuando es una entrada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceBatchSerial_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerialInput.Closed
        listBatchSerial = CtrBatchSerial1.GetListBatchSerial()
        If listBatchSerial.Count = 0 Then
            INDPceBatchSerialInput.EditValue = String.Empty
        Else
            INDPceBatchSerialInput.EditValue = String.Format(ResourceManager.GetString("SelectedBatch", MODULE_NAME), listBatchSerial.Count.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la lista del control del inventario físico  - cuando es una salida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceQuantity_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceBatchSerialOutput.Closed
        listPhysicalInventory = CtrPhysicalInventory1.GetListPhysicalInventory()
        If listPhysicalInventory.Count > 0 Then
            INDPceBatchSerialOutput.EditValue = String.Format(ResourceManager.GetString("SelectedTotalQuantity", MODULE_NAME), listPhysicalInventory.Count.ToString(), listPhysicalInventory.Sum(Function(x) x.QuantityDeliver).ToString())
        Else
            INDPceBatchSerialOutput.EditValue = Nothing
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Try
            INDBtnAdd.Enabled = False
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then
                INDBtnAdd.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            SetValues()
            Dim args As New EditProductLoanMerchandiseDevolutionDetailEventArgs
            args.LoanMerchandiseDevolutionDetail = LoanMerchandiseDevolutionDetail
            RaiseEvent EditProductLoanMerchandiseDevolutionDetail(Nothing, args)
            CleanControls()
            INDBtnAdd.Enabled = True
            Me.Close()
        Catch ex As Exception
            INDBtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub
#End Region

#End Region

#Region "Eventos Barra botones"

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

#Region "Enumeraciones"

    Public Enum ELeanType As Integer
        Input = 1
        Output = 2
    End Enum

#End Region

End Class