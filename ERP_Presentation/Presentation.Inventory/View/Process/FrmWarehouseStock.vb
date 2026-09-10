'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 04-06-2015
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
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Common
Imports System.Windows.Forms
#End Region

Public Class FrmWarehouseStock
    Implements IWarehouseStock

#Region "Builder"
    ''' <summary>
    ''' Se inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

#Region "Globals"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PWarehouseStock

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MWarehouseStock

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues

    ''' <summary>
    ''' Variable que obtiene un listado de sotck por almacen
    ''' </summary>
    ''' <remarks></remarks>
    Public ListWarehouseStock As List(Of WarehouseStock)

    ''' <summary>
    ''' Variable que obtiene un item de stock por almacen
    ''' </summary>
    ''' <remarks></remarks>
    Public itemWarehouseStock As WarehouseStock

#End Region

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IWarehouseStock.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IWarehouseStock.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IWarehouseStock.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del almacen seleccioando
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarehouseId As Integer Implements IWarehouseStock.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    Public Property ListWareHouse As XPInstantFeedbackSource Implements IWarehouseStock.ListWareHouse
        Get
            Return INDSleWarehouse.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property

    Public Property ListProducts As XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo) Implements IWarehouseStock.ListProducts

#End Region

#Region "ICrudBase"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls(True)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        AssigningValues()
        Try
            Using model As New MWarehouseStock(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveListWarehouseStock(ListWarehouseStock)
                AsyncLoader(False)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveMessage"))
                    Me.Deshacer()
                Else
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls(newWarehouseStock As Boolean)
        If newWarehouseStock Then
            WarehouseId = Nothing
        End If
        ListProducts = Nothing
        ListWarehouseStock = Nothing
        itemWarehouseStock = Nothing

        INDGcProductList.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcProductList)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para asignar los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        For Each ItemProduct In ListProducts.Where(Function(x) x.MaximumStockWarehouse > 0 Or x.MinimumStockWarehouse > 0 Or x.RepositionPointWarehouse > 0).ToList
            'verificamos que exista un registro de ese producto en el stock por almacen si existe procedemos a modificar el registro
            Dim warehouseStockUpdate = ListWarehouseStock.Where(Function(x) x.ProductId = ItemProduct.Id).FirstOrDefault
            If warehouseStockUpdate IsNot Nothing Then
                warehouseStockUpdate.MinimumStock = ItemProduct.MinimumStockWarehouse
                warehouseStockUpdate.MaximumStock = ItemProduct.MaximumStockWarehouse
                warehouseStockUpdate.RepositionPoint = ItemProduct.RepositionPointWarehouse

                'si no existe creamos el nuevo registro
            Else
                itemWarehouseStock = New WarehouseStock
                With itemWarehouseStock
                    .WarehouseId = WarehouseId
                    .ProductId = ItemProduct.Id
                    .MinimumStock = ItemProduct.MinimumStockWarehouse
                    .MaximumStock = ItemProduct.MaximumStockWarehouse
                    .RepositionPoint = ItemProduct.RepositionPointWarehouse
                End With
                ListWarehouseStock.Add(itemWarehouseStock)
            End If
        Next
    End Sub

#End Region

#Region "Handless"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        Presenter = Nothing
        Model = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        indexEditRecord = Nothing
        ListWarehouseStock = Nothing
        itemWarehouseStock = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmWarehouseStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.LyWarehouseStock, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        Presenter = New PWarehouseStock(Me)
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Deshacer()

        IndigoGridView1.MoreInfoColunmns(INDGvProductList)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProductList.Columns
            If col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next

        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmWarehouseStock_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        INDSleWarehouse.Focus()
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que carga los productos del almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If WarehouseId <> 0 Then
            CleanControls(False)
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
        End If
        'actualizamos la barra botones solo mostrando el boton de actualizar
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True

        Me.Cursor = ChangeCursorIndigo()
        'cargamos todos los productos
        Presenter.LoadProducts()
        INDGcProductList.DataSource = ListProducts
        Using model As New MWarehouseStock(MyTag)
            'consultamos los registros de stock almacen por el id del almacen y los agregamos a la rejilla
            ListWarehouseStock = Await model.ListWarehouseStocksByWarehouseId(WarehouseId)
            For Each item In ListWarehouseStock
                Dim ItemProduct = ListProducts.Where(Function(x) x.Id = item.ProductId).SingleOrDefault
                If ItemProduct IsNot Nothing Then
                    ItemProduct.MaximumStockWarehouse = item.MaximumStock
                    ItemProduct.MinimumStockWarehouse = item.MinimumStock
                    ItemProduct.RepositionPointWarehouse = item.RepositionPoint
                    ItemProduct.CreationUserWarehouseStock = item.CreationUser
                    ItemProduct.CreationDateWarehouseStock = item.CreationDate
                    If item.ModificationUser <> String.Empty Then
                        ItemProduct.ModificationUserWarehouseStock = item.ModificationUser
                        ItemProduct.ModificationDateWarehouseStock = item.ModificationDate
                    End If
                End If
            Next
        End Using
        Me.Cursor = Cursors.Default

    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.DataSource Is Nothing Then
            Presenter.LoadWarehouse()
        End If
    End Sub
#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' se dispara al cambiar la cantidad Maxima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDriseQuantityMax_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDriseQuantityMax.EditValueChanging
        Dim itemProduct = DirectCast(INDGvProductList.GetFocusedRow(), Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        If e.NewValue < itemProduct.MinimumStockWarehouse Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareQuantity", NAME_MODULE))
            e.Cancel = True
        End If

    End Sub

    ''' <summary>
    ''' se dispara al cambiar la cantidad minima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDriseQuantityMin_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDriseQuantityMin.EditValueChanging
        Dim itemProduct = DirectCast(INDGvProductList.GetFocusedRow(), Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        If e.NewValue > itemProduct.MaximumStockWarehouse Then
            itemProduct.MaximumStockWarehouse = e.NewValue
            INDGcProductList.RefreshDataSource()
        End If
    End Sub

#End Region

#End Region

#Region "Bar Buttons"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit

    End Sub

    ''' <summary>
    ''' Barra botones Click Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

#End Region

End Class