'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 05-05-2014
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
Imports Presentation.CloudAgent

#End Region

Public Class PopUpProductsRequest

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

#End Region

#Region "Events"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddInventoryRequestDetail(sender As Object, e As AddProductInventoryRequestDetailEventArgs)
#End Region

#Region "Properties"
    ''' <summary>
    ''' propiedad para para pasar el listado del detalle de solicitud
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private Property _listinventoryRequestDetailValidation As List(Of InventoryRequestDetail)
    Public WriteOnly Property ListinventoryRequestDetailValidation As List(Of InventoryRequestDetail)
        Set(value As List(Of InventoryRequestDetail))
            If value IsNot Nothing Then
                _listinventoryRequestDetailValidation = New List(Of InventoryRequestDetail)(value.ToArray())
            End If
        End Set
    End Property

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
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Dim inventoryRequestDetail As InventoryRequestDetail
    Public WriteOnly Property inventoryRequestDetailEdit As InventoryRequestDetail
        Set(value As InventoryRequestDetail)
            inventoryRequestDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property ActionsControls As Boolean
        Set(value As Boolean)
            'INDTxtQuantity.Enabled = value
            'INDPceBatchSerial.Enabled = value
            'INDTxtUnitValue.Enabled = value
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
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        product = Nothing

        INDPceProducts.Text = String.Empty
        INDspnQuantity.EditValue = 1
        INDmeDescription.EditValue = String.Empty

        ActionsControls = False
        BarraBotones.FilterDataSource = Nothing
        _editMode = False

        INDPceProducts.Focus()
    End Sub

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControls() As String
        Dim errors As New StringBuilder
        If product Is Nothing Then
            errors.AppendLine(INDLyPceProducts.Text + ResourceManager.GetString("Empty"))
        End If
        If INDspnQuantity.EditValue = 0 Then
            errors.AppendLine(INDlciQuantity.Text + ResourceManager.GetString("Empty"))
        End If
        If _editMode = False AndAlso product IsNot Nothing Then
            If _listinventoryRequestDetailValidation IsNot Nothing AndAlso _listinventoryRequestDetailValidation.Count > 0 Then
                Dim inventoryRequestDetailTmp = _listinventoryRequestDetailValidation.Find(Function(x) x.InventoryProductId = product.Id)
                If inventoryRequestDetailTmp IsNot Nothing Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("SourceProductNone", NAME_MODULE), inventoryRequestDetailTmp.DescriptionProduct))
                End If
            End If
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' establece los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValues()
        If _editMode = False Then
            inventoryRequestDetail = New InventoryRequestDetail
        End If
        With inventoryRequestDetail
            .InventoryProductId = product.Id
            .DescriptionProduct = product.Code + " - " + product.Name
            .ManufacturerName = product.ManufacturerDescription
            .HealthRegistration = product.HealthRegistration
            .Presentation = product.Presentation
            .consumptionUnit = product.PackingUnitDescription
            .Quantity = INDspnQuantity.EditValue
            .OutstandingQuantity = INDspnQuantity.EditValue
            .Description = INDmeDescription.EditValue
        End With
    End Sub

    ''' <summary>
    ''' metodo para cargar los controles con la informacion requerida
    ''' </summary>
    ''' <param name="entranceVoucherDetailTmp"></param>
    ''' <remarks></remarks>
    Private Async Function LoadControls(Optional entranceVoucherDetailTmp As InventoryRequestDetail = Nothing) As Task
        If _editMode = True Then
            INDBtnAddProduct.Text = ResourceManager.GetString("Edit")
        End If
        With inventoryRequestDetail
            Using model As New MInventoryProduct(Me.Tag)
                Dim productDt = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDtAsync($"
select top 1 p.Id
	,  p.Code
	, p.Name
	, case when p.ManufacturerId is null then '' else concat(m.Code, ' - ', m.Name) end as ManufacturerDescription
	, p.HealthRegistration
	, p.Presentation
	, case when p.PackagingUnitId is null then '' else concat(pu.Code, ' - ', pu.Name) end as PackingUnitDescription
from Inventory.InventoryProduct p 
left join Inventory.Manufacturer m on p.ManufacturerId = m.Id
left join Inventory.PackagingUnit pu on p.PackagingUnitId = pu.Id
where p.Id = { .InventoryProductId}", indigo.TransactionalContainer)

                product = New InventoryProduct With {
                    .Id = Integer.Parse(productDt.Rows(0)("Id")),
                    .Code = productDt.Rows(0)("Code").ToString(),
                    .Name = productDt.Rows(0)("Name").ToString(),
                    .ManufacturerDescription = productDt.Rows(0)("ManufacturerDescription").ToString(),
                    .HealthRegistration = productDt.Rows(0)("HealthRegistration").ToString(),
                    .Presentation = productDt.Rows(0)("Presentation").ToString(),
                    .PackingUnitDescription = productDt.Rows(0)("PackingUnitDescription").ToString()
                }

                INDPceProducts.Text = product.Code + " - " + product.Name
                INDspnQuantity.Text = .Quantity
                INDPceProducts.Properties.ReadOnly = True
                INDmeDescription.Text = .Description
            End Using
        End With
    End Function

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPceProducts.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPceProducts.Text.Split(" - ")
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
        ElseIf INDPceProducts.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPceProducts.Text = product.Code + " - " + product.Description
            End If
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    '''  Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub PopUpProductsRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        If _editMode = True Then
            'si esta editando un registro
            Await Me.LoadControls()
        Else
            'si es un nuevo registro
            CleanControls()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpProductsRequest_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara cuando el formulario se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpProductsRequest_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing OrElse _editMode = True Then
            INDPceProducts.Focus()
        End If
    End Sub

#End Region

#Region "SelectProduct"

    ''' <summary>
    ''' se dispara al seleccionar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        INDPceProducts.Text = e.CodeNameProduct
        INDPceProducts.Focus()
        INDPceProducts.ClosePopup()
        INDspnQuantity.EditValue = 1
        INDmeDescription.EditValue = Nothing
        Try
            AsyncLoader(True)
            Using model As New MInventoryProduct(Me.Tag)
                Dim productDt = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ExecuteQueryDtAsync($"
select top 1 p.Id
	,  p.Code
	, p.Name
	, case when p.ManufacturerId is null then '' else concat(m.Code, ' - ', m.Name) end as ManufacturerDescription
	, p.HealthRegistration
	, p.Presentation
	, case when p.PackagingUnitId is null then '' else concat(pu.Code, ' - ', pu.Name) end as PackingUnitDescription
from Inventory.InventoryProduct p 
left join Inventory.Manufacturer m on p.ManufacturerId = m.Id
left join Inventory.PackagingUnit pu on p.PackagingUnitId = pu.Id
where p.Id = {e.ProductId}", indigo.TransactionalContainer)
                product = New InventoryProduct With {
                        .Id = Integer.Parse(productDt.Rows(0)("Id")),
                        .Code = productDt.Rows(0)("Code").ToString(),
                        .Name = productDt.Rows(0)("Name").ToString(),
                        .ManufacturerDescription = productDt.Rows(0)("ManufacturerDescription").ToString(),
                        .HealthRegistration = productDt.Rows(0)("HealthRegistration").ToString(),
                        .Presentation = productDt.Rows(0)("Presentation").ToString(),
                        .PackingUnitDescription = productDt.Rows(0)("PackingUnitDescription").ToString()
                    }
            End Using

        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
            ActionsControls = True
            INDspnQuantity.Focus()
        End Try
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Consulta y asigna los produuctos al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPceProducts.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Despliega el formulario de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceProducts_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceProducts.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' se dispara al darle click al boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDBtnAddProduct.Click
        Try
            INDBtnAddProduct.Enabled = False
            Dim errors = ValidateControls()
            If errors.Length > 0 Then
                INDBtnAddProduct.Enabled = True
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            SetValues()
            If _listinventoryRequestDetailValidation Is Nothing Then
                _listinventoryRequestDetailValidation = New List(Of InventoryRequestDetail)
            End If
            Dim args As New AddProductInventoryRequestDetailEventArgs
            If _editMode = True Then
                args.ItemInventoryRequestDetail = inventoryRequestDetail
                args.EditMode = True
            Else
                args.ItemInventoryRequestDetail = inventoryRequestDetail
                _listinventoryRequestDetailValidation.Add(inventoryRequestDetail)
            End If
            RaiseEvent AddInventoryRequestDetail(Nothing, args)
            Dim flagClose As Boolean = False
            INDBtnAddProduct.Enabled = True
            If _editMode = True Then
                product = Nothing
                Me.Close()
            Else
                CleanControls()
            End If
        Catch ex As Exception
            INDBtnAddProduct.Enabled = True
            Throw ex
        End Try

    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' se dispara al presionar la tecla escape
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpProductsRequest_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al darle enter en el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDPceProducts_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceProducts.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

#End Region

#Region "BarraBotones"
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region


End Class