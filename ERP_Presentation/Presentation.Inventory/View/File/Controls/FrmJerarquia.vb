#Region "Imports"
Imports Domain.Entities
Imports Presentation.Controls
Imports DevExpress.Utils.Menu
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Inventory.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class FrmJerarquia

#Region "Properties and Variables"
    ''' <summary>
    ''' Contiene la entidad principal del ultimo nivel
    ''' </summary>
    Public Property InventoryProduct As InventoryProduct

    ''' <summary>
    ''' The product type class
    ''' </summary>
    Private ProductTypeClass As Integer

    ''' <summary>
    ''' Obtiene o establece la unidad de conversion
    ''' </summary>
    Public Property ConversionUnit As Long
        Get
            Return INDtxtConversionValue.EditValue
        End Get
        Set(value As Long)
            INDtxtConversionValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del producto
    ''' </summary>
    Public Property ProductId As Integer?
        Get
            Return INDsleProduct.EditValue
        End Get
        Set(value As Integer?)
            INDsleProduct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the list product hierarchy.
    ''' </summary>
    ''' <value>
    ''' The list product hierarchy.
    ''' </value>
    Property ListProductHierarchy As List(Of ProductHierarchy)
#End Region

#Region "Events"

    ''' <summary>
    ''' Handles the Load event of the FrmJerarquia control.
    ''' </summary>
    Private Async Sub FrmJerarquia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.InventoryProduct?.Id Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("ErrorToLoadPopUp"), "Cargando el producto")
            Me.Close()
        End If
        AsyncLoader(True)
        Me.BarraBotones.Minimizar(True)
        Me.BarraBotones.OperatingUnitVisible = False
        INDtlJerarquia.KeyFieldName = "ProductId"
        Await LoadTreeLevel()
        InitializeProduct()
        CleanControls()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' evento del click para añadir el detalle a la rejilla.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsbAddLevel_Click(sender As Object, e As EventArgs) Handles INDsbAddLevel.Click
        If ValidateFields() Then
            Mensaje(EeventViewerImages.Advertencia) = $"Faltan campos por diligenciar"
            Exit Sub
        End If
        Dim _product As InventoryProduct
        Using Model As New MInventoryProduct(Me.Tag)
            AsyncLoader(True)
            _product = Await Model.GetInventoryProductById(ProductId)
            AsyncLoader(False)
        End Using
        If _product IsNot Nothing AndAlso _product.Id > 0 Then
            Dim eventSend As New AddProductLevelEventArgs()
            eventSend.InventoryProductLevel = _product
            eventSend.ConversionUnit = ConversionUnit
            AddLevel(sender, eventSend)
        End If
        CleanControls()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        Try
            Me.ListProductHierarchy.RemoveAll(Function(x) x.ProductId = 0)
            For Each item In Me.ListProductHierarchy
                Me.InventoryProduct.ProductHierarchy2.Add(item)
            Next
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SuccessfulProcess")
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    Private Async Sub INDsleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProduct.EditValueChanged
        If ProductId <> 0 Then
            If ListProductHierarchy.Where(Function(x) (x.ProductId = ProductId OrElse x.ParentProductId = ProductId) AndAlso x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).Count() = 0 Then
                Using Model As New MInventoryProduct(Me.Tag)
                    Dim product = Await Model.GetInventoryProductById(ProductId)
                    INDtxtPackingUnit.Properties.NullText = product.PackingUnitDescription
                End Using
            Else
                ProductId = Nothing
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductAgregated", "Inventory")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sets the mensaje.
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#Region "GridviewEvents"
    ''' <summary>
    ''' Handles the PopupMenuShowing event of the INDtlJerarquia control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraTreeList.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDtlJerarquia_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDtlJerarquia.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If

        If TryCast(INDtlJerarquia.GetDataRecordByNode(INDtlJerarquia.FocusedNode), ProductHierarchy)?.ProductId = 0 _
            OrElse (e.HitInfo?.Node?.Level <> 0) Then
            Exit Sub
        End If

        e.Menu.Items.Clear()
        Dim nombreTexto As String = ResourceManager.GetString("LevelDelete", "Inventory")
        Dim ItemMenu As DXMenuItem = New DXMenuItem(nombreTexto, AddressOf ContexMenuActions_Click)
        ItemMenu.Tag = "01"
        e.Menu.Items.Add(ItemMenu)
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' valida que los campos del popup este llenos para poder añadirlos a la rejilla
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If ProductId Is Nothing OrElse Me.INDtxtConversionValue.EditValue Is Nothing Then
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' Loads the tree level.
    ''' </summary>
    Private Async Function LoadTreeLevel() As Task
        Dim _productType As ProductType
        Using Model As New MProductType(Me.Tag)
            _productType = (Await Model.GetProductTypeById(InventoryProduct.ProductTypeId)).ObjectEmbbeded
        End Using
        If _productType IsNot Nothing AndAlso _productType.Id > 0 Then
            ProductTypeClass = _productType.Class

            If ListProductHierarchy Is Nothing Then
                ListProductHierarchy = New List(Of ProductHierarchy)()
            End If

            If Not InventoryProduct?.ProductHierarchy2?.Any() Then
                ListProductHierarchy.Add(FinalLevelProduct(InventoryProduct))
            Else
                Me.ListProductHierarchy.Add(FinalLevelProduct(InventoryProduct))
                For Each item In InventoryProduct?.ProductHierarchy2.ToList()
                    Me.ListProductHierarchy.Add(item)
                Next
            End If
            INDtlJerarquia.DataSource = ListProductHierarchy
            INDtlJerarquia.RefreshDataSource()
        End If
    End Function

    ''' <summary>
    ''' Establece siempre la jerarquia de ultimo nivel
    ''' </summary>
    ''' <param name="_inventoryProduct"></param>
    ''' <returns></returns>
    Private Function FinalLevelProduct(_inventoryProduct As InventoryProduct) As ProductHierarchy
        Return New ProductHierarchy With {.HierarchyProductFinalId = _inventoryProduct.Id,
                                            .ProductId = 0,
                                            .ParentProductId = _inventoryProduct.Id,
                                            .ConversionUnit = 1,
                                            .InventoryProduct1 = New InventoryProduct With {.Code = _inventoryProduct.Code,
                                                                                            .Name = InventoryProduct.Name,
                                                                                            .Id = _inventoryProduct.Id}}
    End Function

    ''' <summary>
    ''' Initializes the product.
    ''' </summary>
    Private Sub InitializeProduct()
        Using Model As New MBusqueda
            Dim filter() As Object = {1}
            INDsleProduct.Properties.DataSource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryProductByProductType, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        INDsleProduct.EditValue = Nothing
        INDtxtPackingUnit.EditValue = Nothing
        ConversionUnit = 0
        INDsleProduct.Focus()
    End Sub

    ''' <summary>
    ''' AÑADE UN NIVEL A LA REJILLA
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AddLevel(sender As Object, e As AddProductLevelEventArgs)
        Try
            Dim _productHierarchy = TryCast(INDtlJerarquia.GetDataRecordByNode(INDtlJerarquia.Nodes.FirstNode), ProductHierarchy)

            If _productHierarchy Is Nothing OrElse e.InventoryProductLevel Is Nothing OrElse Me.ListProductHierarchy Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ErrorToLoadPopUp"), "Cargando Producto")
                Exit Sub
            End If

            Dim _ParentHierarchy = New ProductHierarchy With {.HierarchyProductFinalId = Me.InventoryProduct.Id,
                                                               .ProductId = _productHierarchy.ParentProductId,
                                                               .ParentProductId = e.InventoryProductLevel.Id,
                                                               .ConversionUnit = e.ConversionUnit,
                                                               .InventoryProduct1 = e.InventoryProductLevel}

            Me.ListProductHierarchy.Add(_ParentHierarchy)
            Me.INDtlJerarquia.DataSource = Nothing
            Me.INDtlJerarquia.DataSource = Me.ListProductHierarchy.FindAll(Function(s) s.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted)
            Me.INDtlJerarquia.RefreshDataSource()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ContexMenuActions control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ContexMenuActions_Click(sender As Object, e As EventArgs)
        Select Case (sender.Tag)
            Case "01"
                Dim _productHierarchy = TryCast(INDtlJerarquia.GetDataRecordByNode(INDtlJerarquia.FocusedNode), ProductHierarchy)

                If _productHierarchy Is Nothing Then
                    Exit Sub
                End If

                If _productHierarchy.Id > 0 Then
                    _productHierarchy.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                Else
                    Me.ListProductHierarchy.Remove(_productHierarchy)
                End If
                INDtlJerarquia.DataSource = Nothing
                INDtlJerarquia.DataSource = Me.ListProductHierarchy.FindAll(Function(s) s.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted)
                INDtlJerarquia.RefreshDataSource()
        End Select
    End Sub
#End Region

#Region "Enums"
    Public Enum eProductTypeClass
        Grupo = 1
        ItemMedicamento = 2
        ItemInsumo = 3
        ItemOtro = 4
    End Enum
#End Region

End Class