'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/03/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Dynamic
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraSplashScreen
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports System.Drawing
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class FrmRawMaterial

#Region "Variables"

    ''' <summary>
    ''' Configuracion contiene resultados de Inventarios
    ''' </summary>
    Private _settingInventory As SettingInventory

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PCampaigns

    ''' <summary>
    ''' Obtiene los permisos del usuario para el formulario principal
    ''' </summary>
    Public PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' Entidad xpo que representa el tipo Almacén: Stock Materia Prima  
    ''' </summary>
    Private StockXpo As ViewListWarehouseTypeXpo

    ''' <summary>
    ''' Entidad xpo que representa el Almacén 
    ''' </summary>
    Private WarenhouseXpo As ViewListWarehouseTypeXpo

    ''' <summary>
    ''' Entidad xpo que representa el Almacén Maquila
    ''' </summary>
    Private WarehouseMaquilaXpo As ViewWarehouseMaquilaXpo

    ''' <summary>
    ''' Entidad xpo que representa el Almacén enProceso 
    ''' </summary>
    Private ProductionXpo As ViewListWarehouseTypeXpo

    ''' <summary>
    ''' Utlizado para saber si existe un Almacen tipo : Stock 
    ''' </summary>
    Public StockExist As Boolean = False

    ''' <summary>
    ''' Utlizado para saber si existe un Almacen tipo : Almacen Secuandario  
    ''' </summary>
    Public WarehouseExist As Boolean = False

    ''' <summary>
    ''' Utlizado para saber si existe un Almacen tipo : Maquila
    ''' </summary>
    Public WarehouseMaquilaExist As Boolean = False

    ''' <summary>
    ''' Variable que Almacena la Informacion de Materias Primas 
    ''' </summary>
    Private _listProductMixingStation As List(Of ProductMixingStation)

    ''' <summary>
    ''' Representa la entidad Orden de Traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private _transferOrder As New TransferOrder

    ''' <summary>
    ''' Representa la entidad Solicitud de Inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private inventoryRequest As New InventoryRequest

    ''' <summary>
    ''' Tabla de detalle 
    ''' </summary>
    Private addProductsCampaignDetail As New CampaignDetailItems

    ''' <summary>
    ''' 
    ''' </summary>
    Private listPhysicalInventoryTmp As PhysicalInventory

    ''' <summary>
    ''' Entidad que almacéna los movimientos de reportes
    ''' </summary>
    Private campaingReports As CampaignReports

    Dim Start1 As Integer
    Dim s As Integer
    Dim duration1 As Integer
    Dim flag As Boolean = True

    ''' <summary>
    ''' Asigna el estado
    ''' </summary>
    Public WriteOnly Property Status() As Byte
        Set(value As Byte)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Entidad xpo que representa el Almacén tipo remanente 
    ''' </summary>
    Private RemnantWarenhouseXpo As ViewListWarehouseTypeXpo
#End Region

#Region "Enumerations"
    ''' <summary>
    ''' Enum Reports 
    ''' </summary>
    Enum eReports
        inventoryRequest = 1
        transferOrder = 2
    End Enum
#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para cargar nuevamente la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ReloadPrincipalGridArgs(sender As Object, e As EventArgs)

#End Region

#Region "Properties"
    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)

    Private _objCampaignDetailXpo As CampaignDetailXpo
    ''' <summary>
    ''' Entidad xpo del Detalle de la campaña
    ''' </summary>
    Public Property objCampaignDetailXpo As CampaignDetailXpo
        Get
            Return _objCampaignDetailXpo
        End Get
        Set(value As CampaignDetailXpo)
            _objCampaignDetailXpo = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve el almacén seleccionado
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property WareHouseId() As Integer
        Get
            Return WarenhouseXpo.Id_Warehouse
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene los Productos en Estado Picking 
    ''' </summary>
    Public Property ListProductDetailItems As List(Of CampaignDetailItems)
        Get
            Return INDgcDetail.DataSource
        End Get
        Set(value As List(Of CampaignDetailItems))
            INDgcDetail.DataSource = value
            INDgcDetail.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el almacén de maquila seleccionado
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property MaquilaWareHouseId As Integer?
        Get
            Return INDsleItemWarehouseMaquila.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

    ''' <summary>
    ''' async loader
    ''' </summary>
    ''' <param name="State"></param>
    Public Overrides Sub AsyncLoader(State As Boolean)
        MyBase.AsyncLoader(State)
        If State Then
            INDviewDetail.ShowLoadingPanel()
        Else
            INDviewDetail.HideLoadingPanel()
        End If
    End Sub
#End Region

#Region "Methods and Functions"
    ''' <summary>
    ''' Carga el datasource de la rejilla
    ''' </summary>
    Public Async Function SetDatasourceAsync() As Task
        Select Case objCampaignDetailXpo.Status
            Case 1 'Calculado
                Await GetDetails()
            Case 2 'Picking, Validación, Confirmado
                Await GetDetailPicking()
                Await GetCampaignReports(objCampaignDetailXpo.Id, eReports.inventoryRequest)
                'Actualizar la visibilidad del botón "Procesar Solicitud" después de cargar los reportes
                UpdateConfirmButtonVisibility()
            Case 3, 4 'Validación, Confirmado
                Await GetDetailValidation()
                Await GetCampaignReports(objCampaignDetailXpo.Id, eReports.transferOrder)
                UpdateConfirmButtonVisibility()
        End Select
    End Function

    ''' <summary>
    ''' Listar canastas que esten asociadas a la linea de distribución de la campaña.
    ''' </summary>
    Private Sub ListBaskets()
        Try
            Using model As New MCampaign(Me.Tag)
                Dim result = model.ListBaskets(objCampaignDetailXpo.UnitDoseTypeId.Id)
                If result IsNot Nothing Then
                    INDsleBakets.Properties.DataSource = result
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
        INDsleBakets.EditValue = objCampaignDetailXpo.ProductionBasketId
    End Sub

    ''' <summary>
    ''' Guarda el Producto Adicional a la Campaña 
    ''' </summary>
    ''' <param name="productsCampaignDetail"></param>
    Private Async Sub SaveProductCampaignDetail(productsCampaignDetail As CampaignDetailItems)
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.SaveProductItemCampaignDetail(productsCampaignDetail)
                If result.StateResult Then
                    Await SetDatasourceAsync()
                    Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Detalle no pudo ser agregado"
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
        INDsleBakets.EditValue = objCampaignDetailXpo.ProductionBasketId
    End Sub

    ''' <summary>
    ''' Elimina el Producto de la solicitud manual de la Campaña 
    ''' </summary>
    ''' <param name="productsCampaignDetail"></param>
    Private Async Sub DeleteProductCampaignDetail(productsCampaignDetail As CampaignDetailItems)
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.DeleteProductItemCampaignDetail(productsCampaignDetail)

                If result.StateResult Then
                    Await SetDatasourceAsync()
                End If

                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que abre el from para agregar los Productos
    ''' </summary>
    Private Sub OpenFormAddProductsCampaignDetail(EditMode As Boolean)
        Using formulario As New FrmRawMaterialProductsDetail()
            Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddProductsCampaignDetailArgs, AddressOf ReturnAddEventArgs
            formulario.CampaignId = objCampaignDetailXpo.Id
            formulario.EditModeDetail = EditMode
            formulario.ProductsCampaignDetail = addProductsCampaignDetail
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(700, 600)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega y guarda el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddProductsCampaignDetail)
        If e IsNot Nothing Then
            'Save Produduct 
            SaveProductCampaignDetail(e.ProductsCampaignDetail)
        End If
    End Sub

    ''' <summary>
    ''' Consulta la materia prima en estado: Calculado
    ''' </summary>
    Private Async Function GetDetails() As Task
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)

                Dim result = Await model.ListProductRawMaterial(objCampaignDetailXpo.Id, StockXpo.Id_Warehouse, If(WarenhouseXpo Is Nothing, 0, WarenhouseXpo.Id_Warehouse))

                If result IsNot Nothing Then
                    INDgcDetail.DataSource = result.ToList()
                    _listProductMixingStation = result.ToList()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para poder continuar"
                End If

                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' Consulta la materia prima en estado: Picking 
    ''' </summary>
    Private Async Function GetDetailPicking() As Task
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.ListRawMaterialItems(objCampaignDetailXpo.Id)
                If result IsNot Nothing Then
                    ListProductDetailItems = result.ToList()
                    ShowColumWithQuantities(ListProductDetailItems)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para poder continuar"
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function


    ''' <summary>
    ''' Actualiza la visibilidad del botón "Procesar Solicitud" basándose en las reglas de negocio
    ''' </summary>
    Private Sub UpdateConfirmButtonVisibility()
        'Por defecto, ocultar el botón
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True

        'Verificar si hay datos cargados
        If ListProductDetailItems Is Nothing OrElse Not ListProductDetailItems.Any() Then
            Exit Sub
        End If

        'Calcular las cantidades totales
        Dim RequestQuestities = ListProductDetailItems.Sum(Function(x) x.RequestQuantity)
        Dim StockQuestities = ListProductDetailItems.Sum(Function(x) x.QuantityStock)

        'Si no hay diferencias en cantidades, no mostrar el botón
        If RequestQuestities = StockQuestities Then
            Exit Sub
        End If

        'Verificar si ya existe una solicitud de inventario procesada
        If campaingReports?.InventoryRequest?.Code IsNot Nothing Then
            'Ya existe solicitud previa
            'Solo mostrar el botón si hay productos agregados manualmente (ItemType = 4)
            Dim hasManualProducts = ListProductDetailItems.Any(Function(x) x.ItemType = 4)

            If hasManualProducts Then
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Procesar Solicitud")
            End If
        Else
            'No existe solicitud previa, mostrar el botón si hay diferencias
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Procesar Solicitud")
        End If
    End Sub

    ''' <summary>
    ''' segun el datasource muestra  u oculta las columnas que este en cero
    ''' </summary>
    ''' <param name="ListProductDetailItems"></param>
    Private Sub ShowColumWithQuantities(ListProductDetailItems As List(Of CampaignDetailItems))
        'si no hay cantidades en almacenes oculta la columna
        If Not ListProductDetailItems.Any(Function(x) x.QuantityWarehouse > 0) Then
            INDColPreferenceWarehouse.HideColumn()
        End If
        ' si no hay cantidades en maquila oculta la columna
        If Not ListProductDetailItems.Any(Function(x) x.QuantityMaquila > 0) Then
            INDColWarehouseMaquila.HideColumn()
        End If
        'se oculta el la columna de stock si no vienen ninguna de la cantidades
        If Not ListProductDetailItems.Any(Function(x) x.QuantityStock > 0) Then
            INDColStock.HideColumn()
        End If
        'se oculta la columna de remanente si no hay cantidades
        If Not ListProductDetailItems.Any(Function(x) x.QuantityRemnant > 0) Then
            INDColRemnantWareHouse.HideColumn()
        End If
    End Sub

    ''' <summary>
    ''' Consulta la materia prima en estado: Validacion 
    ''' </summary>
    Private Async Function GetDetailValidation() As Task
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.ListRawMaterialItemsValidation(objCampaignDetailXpo.Id)
                If result IsNot Nothing Then
                    ListProductDetailItems = result.ToList()
                    ShowColumWithQuantities(ListProductDetailItems)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para poder continuar"
                End If
                AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' Consulta los reportes
    ''' </summary>
    Private Async Function GetCampaignReports(campaignDetailId As Integer, action As Integer) As Task
        Using model As New MCampaign(Me.Tag)
            Dim data As New Tuple(Of Integer, Integer)(campaignDetailId, action)
            Dim result = Await model.GetListCampaignReports(data)
            If result.Any() Then
                campaingReports = result.FirstOrDefault()
                If action = 1 Then Me.BarraBotones.PrintReportByIdReport(PrintReportAction.None, campaingReports.InventoryRequest.Id, 2228, 52, campaingReports.InventoryRequest.Id)
                If action = 2 Then Me.BarraBotones.PrintReportByIdReport(PrintReportAction.None, campaingReports.TransferOrder.Id, 2228, 49, campaingReports.TransferOrder.Id)
            End If
        End Using
    End Function

    ''' <summary>
    ''' Método que asigna la columna de acciones a las rejillas
    ''' </summary>
    Private Sub SetActionsColumns()
        If PermissionsForm IsNot Nothing AndAlso PermissionsForm.Count > 0 Then
            If PermissionsForm.ContainsKey(75) Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = False
                INDcolCUM.OptionsColumn.AllowEdit = True
                INDcolCUM.OptionsColumn.AllowFocus = True
            Else
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = True
                INDcolCUM.OptionsColumn.AllowEdit = False
                INDcolCUM.OptionsColumn.AllowFocus = False
            End If

            If _objCampaignDetailXpo.Status = 1 AndAlso {2, 5}.Contains(_objCampaignDetailXpo.CampaignStatus) Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = False
            Else
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
            End If

            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.ActualizarConfirmar, "Imprimir plan de adecuación")

            IndigoGridView1.SetListAcction(INDviewDetail, {eAcciones.Remove}.ToList())
            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewDetail.Columns
                If col.Name = "colactions" Then
                    col.Width = 100
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = "Calculada", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = "Picking", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = "Validación", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = "Confirmado", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
        Me.BarraBotones.StatusRecordVisible = True
    End Sub

    ''' <summary>
    ''' Método que carga la información de el almacén y el Stock  
    ''' </summary>
    Public Sub SetInformationControls()
        Dim ListMSWareHouse = Presenter.ListViewMSWarehouse(objCampaignDetailXpo.CampaignId.CMConfigurationId)
        StockXpo = ListMSWareHouse.Where(Function(a) a.WarehouseType = eWarehouseMSType.MateriaPrimaStock).FirstOrDefault
        WarenhouseXpo = ListMSWareHouse.Where(Function(a) a.WarehouseType = eWarehouseMSType.Almacen).FirstOrDefault
        RemnantWarenhouseXpo = ListMSWareHouse.Where(Function(a) a.WarehouseType = eWarehouseMSType.Remanente).FirstOrDefault
        WarehouseMaquilaXpo = Presenter.ViewWarehouseMaquilaId(objCampaignDetailXpo.Id)
    End Sub

    ''' <summary>
    ''' Método que asigna el almacén y el Stock a la Rejilla, Si estos Existen , y oculto columnas. 
    ''' </summary>
    Public Sub SetWarehouseInformation()

        Dim ButtonsToHide = {
        EbuttonsWithoutPermission.Actualizar,
        EbuttonsWithoutPermission.Deshacer,
        EbuttonsWithoutPermission.Guardar,
        EbuttonsWithoutPermission.GuardarConfirmar,
        EbuttonsWithoutPermission.Auditoria,
        EbuttonsWithoutPermission.Imprimir,
        EbuttonsWithoutPermission.Confirmar,
        EbuttonsWithoutPermission.Procesar,
        EbuttonsWithoutPermission.EntregaManual,
        EbuttonsWithoutPermission.ActiveInactive,
        EbuttonsWithoutPermission.LoadPurcharseOrder
    }

        For Each Button In ButtonsToHide
            Me.BarraBotones.OcultarBotonesSinPermisos(Button) = True
        Next

        Me.BarraBotones.LoadReportsAndDefinitions()
        INDsleRowMaterialsStock.EditValue = StockXpo.Id
        INDsleRowMaterialsStock.Properties.NullText = StockXpo.CodeName
        INDsleRemnantWarenhouse.EditValue = RemnantWarenhouseXpo?.Id
        INDsleRemnantWarenhouse.Properties.NullText = RemnantWarenhouseXpo?.CodeName
        StockExist = True

        INDsleBakets.Properties.ReadOnly = True
        INDLciBasket.Enabled = False

        Select Case objCampaignDetailXpo.Status
            Case 1 ' Calculado
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Actualizar, "Picking de Dosis Unitarias")
                INDLciBasket.Enabled = True
                INDsleBakets.Properties.ReadOnly = False

                ConfigureColumns({INDColType, INDColItem, INDColRequiredQuantity})
                INDviewDetail.Columns.ColumnByName("colActions").ShowColumn(3)

                If WarenhouseXpo IsNot Nothing Then
                    INDlyItemWarehouse.ShowLayout()
                    INDsleWarehouse.EditValue = WarenhouseXpo.Id
                    INDsleWarehouse.Properties.NullText = WarenhouseXpo.CodeName
                    WarehouseExist = True
                Else
                    Me.INDlyItemWarehouse.HideLayout()
                End If

                If WarehouseMaquilaXpo IsNot Nothing Then
                    INDlyItemWarehouseMaquila.ShowLayout()
                    INDsleItemWarehouseMaquila.EditValue = WarehouseMaquilaXpo.WarehouseId
                    INDsleItemWarehouseMaquila.Properties.NullText = WarehouseMaquilaXpo.WarehouseMaquilaCodeName
                    WarehouseMaquilaExist = True
                Else
                    INDlyItemWarehouseMaquila.HideLayout()
                End If

            Case 2 ' Picking de dosis unitarias
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Imprimir, "Imprimir Picking")
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Actualizar, "Picking de Dosis Unitarias")
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Procesar) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Procesar, "Validar Lotes")
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

                INDLciBasket.Enabled = True
                ConfigureColumns({INDColType, INDColItem, INDColRequiredQuantity, INDColRawMaterial,
                                 INDColRemnantWareHouse, INDColStock})

                If WarenhouseXpo IsNot Nothing OrElse WarehouseMaquilaXpo IsNot Nothing Then
                    INDColPreferenceWarehouse.ShowColumn(6)
                End If

                INDColWarehouseMaquila.ShowColumn(7)
                INDColAmountPending.ShowColumn(8)
                INDcolCUM.ShowColumn(9)
                INDviewDetail.Columns.ColumnByName("colActions").ShowColumn(10)

                If WarenhouseXpo IsNot Nothing Then
                    INDlyItemWarehouse.ShowLayout()
                    INDsleWarehouse.EditValue = WarenhouseXpo.Id
                    INDsleWarehouse.Properties.NullText = WarenhouseXpo.CodeName
                    WarehouseExist = True
                Else
                    Me.INDlyItemWarehouse.HideLayout()
                End If

                If WarehouseMaquilaXpo IsNot Nothing Then
                    INDlyItemWarehouseMaquila.ShowLayout()
                    INDsleItemWarehouseMaquila.EditValue = WarehouseMaquilaXpo.WarehouseId
                    INDsleItemWarehouseMaquila.Properties.NullText = WarehouseMaquilaXpo.WarehouseMaquilaCodeName
                    WarehouseMaquilaExist = True
                Else
                    INDlyItemWarehouseMaquila.HideLayout()
                End If

            Case 3, 4  'Validación de lotes y Confirmado
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.EntregaManual) = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Imprimir, "Imprimir Picking")

                'Botón Plan de Adecuación solo en estado 4
                If objCampaignDetailXpo.Status = 4 Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.LoadPurcharseOrder) = False
                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.LoadPurcharseOrder, "Imprimir Plan de Adecuación")
                    Me.BarraBotones.CopySvgImageFromButton(EbuttonsWithoutPermission.Imprimir, EbuttonsWithoutPermission.LoadPurcharseOrder)
                End If

                INDlyEntregaManual.ShowLayout()
                INDlyItemWarehouse.HideLayout()

                If WarehouseMaquilaXpo IsNot Nothing Then
                    INDlyItemWarehouseMaquila.ShowLayout()
                    INDsleItemWarehouseMaquila.EditValue = WarehouseMaquilaXpo.WarehouseId
                    INDsleItemWarehouseMaquila.Properties.NullText = WarehouseMaquilaXpo.WarehouseMaquilaCodeName
                    WarehouseMaquilaExist = True
                Else
                    INDlyItemWarehouseMaquila.HideLayout()
                End If

                INDLciBasket.HideControl()
                ConfigureColumns({INDColType, INDColItem, INDColRequiredQuantity, INDColCantEntregada, INDColAmountPending, INDcolCUM})
                INDviewDetail.Columns.ColumnByName("colActions").ShowColumn(6)
        End Select

        If (objCampaignDetailXpo.Status <> 1) AndAlso (objCampaignDetailXpo.CampaignStatus = 5 OrElse objCampaignDetailXpo.CampaignStatus = 2) Then
            INDlyItemAddComponents.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de mostrar las columnas necesarias dependiendo del estado de la campaña
    ''' </summary>
    ''' <param name="Columns"></param>
    Private Sub ConfigureColumns(Columns As IEnumerable(Of Columns.GridColumn), Optional _LastIndex As Integer? = Nothing)
        Me.INDgcDetail.BeginUpdate()

        INDviewDetail.Columns.ToList().ForEach(Sub(i) i.HideColumn())

        Dim index As Integer = If(_LastIndex, 0)
        For Each column In Columns
            column.ShowColumn(index)
            index += 1
        Next

        Me.INDgcDetail.EndUpdate()
    End Sub

    ''' <summary>
    ''' Procesa y Guarda la Materia Prima
    ''' </summary>
    Private Async Function ProcessRawMaterial(_listProductMixingStationTmp As List(Of ProductMixingStation)) As Task
        Me.AsyncLoader(True)
        Try
            Dim warehouseStockId = StockXpo.Id_Warehouse
            Dim warehouseId = WarenhouseXpo?.Id_Warehouse
            Dim RemnantWarehouseId = Me.RemnantWarenhouseXpo.Id_Warehouse
            Dim maquilaId As Integer?

            If WarehouseMaquilaExist Then
                maquilaId = WarehouseMaquilaXpo.WarehouseId
            End If

            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.CalculatePicking(_listProductMixingStationTmp, objCampaignDetailXpo.Id, warehouseStockId, warehouseId, maquilaId, RemnantWarehouseId)
                If result.StateResult = True Then
                    Dim sb As New StringBuilder()
                    sb.AppendLine("* Picking de dosis unitaria procesada correctamente")
                    If Not String.IsNullOrEmpty(result.Message) Then
                        sb.AppendLine()
                        sb.AppendLine(result.Message)
                    End If
                    Mensaje(EeventViewerImages.Informacion) = sb.ToString()
                    _listProductMixingStation = Nothing
                    Await ReloadCampaigRawMaterial()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Picking de dosis unitaria No pudo ser procesada correctamente"
                End If

                Me.AsyncLoader(False)
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' Recarga los datos de la ventana
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ReloadCampaigRawMaterial() As Task
        INDgcDetail.DataSource = Nothing
        Dim xpo = Await Task.Factory.StartNew(Function()
                                                  Using model As New MCampaign(Tag)
                                                      Return model.GetCampaignDetailXpoById(objCampaignDetailXpo.Id)
                                                  End Using
                                              End Function)
        objCampaignDetailXpo.Status = xpo.Status
        Status = objCampaignDetailXpo.Status
        SetWarehouseInformation()
        Await SetDatasourceAsync()
    End Function

    ''' <summary>
    ''' Objeto para la Validacion
    ''' </summary>
    ''' <returns></returns>
    Private Function WarehouseValues() As Object
        Dim args As Object = New ExpandoObject()
        args.SourceWarehouseId = _transferOrder.SourceWarehouseId
        args.SourceTypeName = StockXpo.WarehouseTypeName
        args.TargetWarehouseId = _transferOrder.TargetWarehouseId
        args.TargetTypeName = ProductionXpo.WarehouseTypeName
        args.FormModule = "FrmRawMaterial"
        Return args
    End Function

    ''' <summary>
    ''' Asigan los Valores a la Entidad: TransferOrder
    ''' </summary>
    Private Sub AssigningValues()
        With _transferOrder
            Dim OperatingUnitId As Integer = Me.BarraBotones.OperatingUnitValue
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = OperatingUnitId
            .OrderType = 1
            .SourceWarehouseId = StockXpo.Id_Warehouse
            .DispatchTo = 1
            .TargetWarehouseId = ProductionXpo.Id_Warehouse
            .Status = 2
        End With
    End Sub

    ''' <summary>
    ''' Asigan los Valores a la Entidad: inventoryRequest
    ''' </summary>
    Private Sub AssigningValuesInventory()
        With inventoryRequest
            Dim OperatingUnitId As Integer = Me.BarraBotones.OperatingUnitValue
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = OperatingUnitId
            .RequestType = 2 'Almacen
            .MovementType = 2 ' Traslado
            .SourceWarehouseId = WarenhouseXpo.Id_Warehouse
            .TargetWarehouseId = StockXpo.Id_Warehouse
            .Status = 2 ' Confirmado 
            .CMConfigurationId = objCampaignDetailXpo.CampaignId.CMConfigurationId
            .CampaignDetailId = objCampaignDetailXpo.Id
            .CampaignNumber = objCampaignDetailXpo.CampaignNumber
        End With
    End Sub

    ''' <summary>
    ''' Picking Dosis de Dosis Unitarias
    ''' </summary>
    Public Async Function Picking() As Task
        If Not objCampaignDetailXpo.ProductionBasketId.HasValue Then
            If MessageIndigo.Show("No ha asociado una canasta de producción a la campaña, ¿desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Return
            End If
        End If

        Me.AsyncLoader(True)

        If _listProductMixingStation Is Nothing OrElse Not _listProductMixingStation.Any() Then
            Using model As New MCampaign(Me.Tag)
                Dim calculationResult = Await model.ListProductRawMaterial(objCampaignDetailXpo.Id, StockXpo.Id_Warehouse, If(WarenhouseXpo Is Nothing, 0, WarenhouseXpo.Id_Warehouse))
                If calculationResult IsNot Nothing Then
                    _listProductMixingStation = calculationResult
                End If
            End Using
        End If
        Me.AsyncLoader(False)

        If _listProductMixingStation Is Nothing OrElse INDviewDetail.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles para generar Picking"
            Exit Function
        End If

        Await ProcessRawMaterial(_listProductMixingStation)
    End Function

    ''' <summary>
    ''' Valida Lotes
    ''' </summary>
    Public Async Sub ValidateBatches()
        If ListProductDetailItems Is Nothing OrElse Not ListProductDetailItems.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay productos agregados en la rejilla."
            Exit Sub
        End If

        Dim pendingToValidate = (From t In ListProductDetailItems Where t.RequestQuantity - t.QuantityStock - t.QuantityWarehouse - t.QuantityMaquila > 0 Select t)?.ToList()

        If pendingToValidate IsNot Nothing AndAlso pendingToValidate.Any() Then
            Dim products = pendingToValidate.Select(Function(m) If(m.AtcCodeName, If(m.SupplyCodeName, m.ProductCodeName))).ToArray()

            Mensaje(EeventViewerImages.Advertencia) = $"Los siguientes items  tienen cantidades pendientes: {vbCrLf}{String.Join(vbCrLf, products)}"
        End If

        Try
            Using model As New MCampaign(Me.Tag)
                Me.AsyncLoader(True)
                Dim result = Await model.UpdateStatusCampaignDetail(objCampaignDetailXpo.Id, 3)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = "Se ha iniciado el proceso de validación de lotes correctamente"
                    objCampaignDetailXpo.Status = 3
                    Status = objCampaignDetailXpo.Status
                    SetWarehouseInformation()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo validar el lote"
                End If
                Me.AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar y Confirmar del control de usuarios
    ''' </summary>
    Public Async Sub OrdenTraslado()
        If ListProductDetailItems Is Nothing OrElse ListProductDetailItems.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay productos agregados en la rejilla."
            Exit Sub
        End If

        If objCampaignDetailXpo.Status = 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe realizar picking de dosis unitarias"
            Exit Sub
        End If

        'Valido Cantidades
        If (From x In ListProductDetailItems Select x.DeliveredQuantity).Sum() > (From x In ListProductDetailItems Select x.RequestQuantity).Sum() Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar por todos los productos supera la cantidad Solicitada"
            Exit Sub
        End If

        If MessageIndigo.Show("¿Está seguro que desea generar la orden de traslado?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Return
        End If

        Try
            ProductionXpo = Presenter.ListViewMixingStationWarehouse(objCampaignDetailXpo.CampaignId.CMConfigurationId, eWarehouseMSType.EnProceso)
            AssigningValues()
            Dim args = WarehouseValues()
            Using model As New MCampaign(Me.Tag)
                Me.AsyncLoader(True)
                Dim resultValidation As ActionResult = Await model.ValidateWarehouses(args)
                If resultValidation.StateResult Then
                    Dim result = Await model.OrderTranfer(_transferOrder, objCampaignDetailXpo.CampaignId.CMConfigurationId, objCampaignDetailXpo.Id, objCampaignDetailXpo.CampaignNumber)
                    If result.StateResult Then
                        objCampaignDetailXpo.Status = 4
                        Status = objCampaignDetailXpo.Status
                        SetWarehouseInformation()
                        Await ReloadCampaigRawMaterial()
                        If result.MessageResult.Count = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = "Se confirmó correctamente la orden de traslado con código " & String.Join(" ", result.MessageResult)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = $"Se confirmaron correctamente las Órdenes de traslado con códigos: ({String.Join(" ", result.MessageResult)})"
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message.ToString
                    End If
                Else
                    ShowMessage(EeventViewerImages.Advertencia) = resultValidation.Message
                End If
                Me.AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Procesar Solicitud del control de usuarios.
    ''' </summary>
    Private Sub TrasladoInventario()
        If ValidateControls() = False Then
            Exit Sub
        End If

        If INDviewDetail.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay productos agregados en la rejilla."
            Exit Sub
        End If

        'Valido que hayan Cantidades encontradas en los otros almacenes diferente a Stock 
        If (From t In ListProductDetailItems Select t.QuantityWarehouse).Sum() = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay productos de preferencia en almacenes diferente a Stock"
        End If

        If campaingReports?.InventoryRequest?.Code Is Nothing Then
            SaveInventoryRequest()
        Else
            'Permite saber si se escogió YES en la pregunta siempre y cuando salga
            Dim selectedYesInQuestion As Boolean = False
            If MessageIndigo.Show("Ya existe solicitud No." & campaingReports.InventoryRequest.Code & " ¿Desea crear una nueva solicitud?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                selectedYesInQuestion = True
            End If
            If selectedYesInQuestion Then
                SaveInventoryRequest()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Procesar Solicitud de Inventario.
    ''' </summary>
    Private Async Sub SaveInventoryRequest()
        Try
            AssigningValuesInventory()
            Using model As New MCampaign(Me.Tag)
                Me.AsyncLoader(True)
                Dim result = Await model.SaveInventoryRequest(inventoryRequest)
                If result.StateResult = True Then
                    Dim inventoryRequestCode = String.Join(" ", result.MessageResult)
                    Mensaje(EeventViewerImages.Informacion) = "Se generó solicitud de orden de traslado con código " & inventoryRequestCode
                    Await ReloadCampaigRawMaterial()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Me.AsyncLoader(False)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' metodo que asigna valores cuando se han seleccionado cantidades a entregar desde el popup de CUM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnGetCUM(sender As Object, e As GetCUMEventArgs)
        Dim rowDetail = INDviewDetail.GetFocusedObject(Of CampaignDetailItems)
        INDviewDetail.ShowLoadingPanel()

        rowDetail.QuantityWarehouse = e.ListItemDetails.FindAll(Function(m) m.WareHouseId = WareHouseId).Sum(Function(m) m.DeliveredQuantity)
        rowDetail.QuantityStock = e.ListItemDetails.FindAll(Function(m) m.WareHouseId = StockXpo.Id_Warehouse).Sum(Function(m) m.DeliveredQuantity)
        If MaquilaWareHouseId.HasValue Then
            rowDetail.QuantityMaquila = e.ListItemDetails.FindAll(Function(m) m.WareHouseId = MaquilaWareHouseId).Sum(Function(m) m.DeliveredQuantity)
        End If

        If objCampaignDetailXpo.Status = 2 Then
            ' Falta implementar el almacen de maquila
            Try
                Using model As New MCampaign(Me.Tag)
                    Me.AsyncLoader(True)
                    Dim result = Await model.SaveCampaignDetailItem(rowDetail, e.ListItemDetails?.Select(Of CampaignDetailPicking)(Function(m) m).ToList(), StockXpo.Id_Warehouse)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = "Se guardó correctamente"
                        SetWarehouseInformation()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    Me.AsyncLoader(False)
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try

        ElseIf objCampaignDetailXpo.Status = 3 OrElse objCampaignDetailXpo.Status = 4 Then
            Try
                Using model As New MCampaign(Me.Tag)
                    Me.AsyncLoader(True)
                    Dim result = Await model.SaveCampaignDetailValidation(rowDetail, e.ListItemDetails?.Select(Of CampaignDetailValidation)(Function(m) m).ToList(), StockXpo.Id_Warehouse)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = "Se guardó correctamente"
                        SetWarehouseInformation()
                        GetDetailValidation()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    Me.AsyncLoader(False)
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If

        INDviewDetail.HideLoadingPanel()
        INDgcDetail.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' metodo para cargar manualmente las cantidades que se van a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ManualDelivery() As Task
        Using model As New MCampaign(Tag)
            Dim result = Await model.ManualDeliveryAsync(ListProductDetailItems)
            If result.StateResult Then
                GetDetailValidation()
                Mensaje(EeventViewerImages.Informacion) = result.Message
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        End Using
    End Function

    ''' <summary>
    ''' Consulta los parámetros de inventarios
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetSettingsInventory() As Task
        Using model As New MSettingInventory(Me.Tag)
            Dim settingInventoryRes = Await model.GetInventorySettingsRegister(Me.BarraBotones.OperatingUnitValue)
            If settingInventoryRes.IsNotNull() Then
                _settingInventory = settingInventoryRes.ObjectEmbbeded
            End If
        End Using
    End Function

    Private Async Sub INDMeCUM_KeyDownPhysicalInventory(e As KeyEventArgs)
        Dim warehouseId As Integer? = StockXpo.Id_Warehouse
        Dim resultPhysical As ActionResult(Of List(Of PhysicalInventory)) = Nothing
        Dim productATC = INDMeCUM.EditValue.ToString.ToUpper().Trim().Split("*IND*")

        Using model As New MCampaign(Me.Tag)
            If productATC.Length > 1 Then
                resultPhysical = model.GetPhysicalInventoryBarCode(productATC.ElementAt(0), productATC.ElementAt(2))
            Else
                resultPhysical = model.GetPhysicalInventoryBarCode(productATC.ElementAt(0), "")
            End If

            If resultPhysical.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = resultPhysical.Message
                INDMeCUM.EditValue = Nothing
                INDMeCUM.Focus()
                e.SuppressKeyPress = True
                Exit Sub
            End If

            listPhysicalInventoryTmp = (From t In resultPhysical.ObjectEmbbeded.ToList() Where t.WarehouseId = warehouseId Select t).FirstOrDefault
        End Using

        Dim finded = ListProductDetailItems.Find(Function(m) ((m.ProductId.HasValue AndAlso m.ProductId = listPhysicalInventoryTmp.ProductId) _
                                                OrElse (m.SupplyId.HasValue AndAlso m.SupplyId = listPhysicalInventoryTmp.InventoryProduct.SupplieId) _
                                                OrElse (m.AtcId.HasValue AndAlso m.AtcId = listPhysicalInventoryTmp.InventoryProduct.ATCId)) _
                                                AndAlso m.DeliveredQuantity < m.RequestQuantity)

        If finded IsNot Nothing Then
            Using model As New MCampaign(Me.Tag)
                Me.AsyncLoader(True)
                Dim campaignDetailValidation = Await model.GetCampaignDetailValidationByCampaignDetailId(finded.CampaignDetailId)
                Dim listCampaignValidation As New List(Of CampaignDetailValidation)
                Dim ResultValidation = campaignDetailValidation.ObjectEmbbeded.FirstOrDefault(Function(m) m.ProductId = listPhysicalInventoryTmp.ProductId AndAlso m.BatchSerialId = listPhysicalInventoryTmp.BatchSerialId AndAlso m.WarehouseId = listPhysicalInventoryTmp.WarehouseId)
                If ResultValidation Is Nothing Then
                    ResultValidation = New CampaignDetailValidation() With {
                        .CampaignDetailId = finded.CampaignDetailId,
                        .ProductId = listPhysicalInventoryTmp.ProductId,
                        .WarehouseId = listPhysicalInventoryTmp.WarehouseId,
                        .BatchSerialId = listPhysicalInventoryTmp.BatchSerialId
                    }
                Else
                    If ResultValidation.DeliveredQuantity >= listPhysicalInventoryTmp.Quantity Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar supera la cantidad en el inventario del producto"
                        Me.AsyncLoader(False)
                        Exit Sub
                    End If
                End If

                ResultValidation.DeliveredQuantity += 1
                ResultValidation.TypeProcess = 3
                listCampaignValidation.Add(ResultValidation)

                Dim result = Await model.SaveCampaignDetailValidation(finded, listCampaignValidation, StockXpo.Id_Warehouse)
                If result.StateResult = True Then
                    GetDetailValidation()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo realizar la entrega correctamente"
                End If
                Me.AsyncLoader(False)
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró el producto en la rejilla o ya fue entregado"
        End If
    End Sub

    ''' <summary>
    ''' Init data
    ''' </summary>
    ''' <returns></returns>
    Private Async Function InitData() As Task
        Status = objCampaignDetailXpo.Status
        Await SetDatasourceAsync()
        If objCampaignDetailXpo.Status > 1 Then
            INDsleBakets.Properties.ReadOnly = True
        End If
        ListBaskets()
    End Function
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmRawMaterial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        SetActionsColumns()
        'Instancia del presentador campañas.
        Presenter = New PCampaigns()
        'Listar Stock - Almacenes
        SetInformationControls()
        'Listar los componentes ò medicamentos que se van a procesar.
        'Carga el estado 
        LoadStatus()
        AsyncLoader(True)
        Await GetSettingsInventory()
        Await InitData()
    End Sub
#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRawMaterial_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRawMaterial_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDMeCUM_KeyDown(sender As Object, e As KeyEventArgs) Handles INDMeCUM.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDMeCUM.EditValue Is Nothing Then
                Exit Sub
            End If
            Dim rowDetail = INDviewDetail.GetFocusedObject(Of CampaignDetailItems)
            INDMeCUM_KeyDownPhysicalInventory(e)
        End If

        Start1 = DateTime.Now.Millisecond
    End Sub

    Private Sub INDMeCUM_KeyUp(sender As Object, e As KeyEventArgs) Handles INDMeCUM.KeyUp

        s = DateTime.Now.Millisecond
        duration1 = s - Start1
        If flag = True Then
            flag = False
        End If
        If duration1 > 15 Then

            If INDMeCUM.Text <> String.Empty Then
                INDMeCUM.Text = String.Empty
            End If

            MessageIndigo.Show("No se permite Digitar Manualmente", MessageType.Warning, Me.Text, Botones.Aceptar)
        End If
    End Sub
#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction

        Dim detail As Object = Nothing
        If objCampaignDetailXpo.Status = 1 Then
            detail = TryCast(INDviewDetail.GetFocusedRow(), ProductMixingStation)
        Else
            detail = TryCast(INDviewDetail.GetFocusedRow(), CampaignDetailItems)
        End If

        INDviewDetail.BeginUpdate()

        Select Case detail?.ItemType
            Case 2 'Canasta
                If objCampaignDetailXpo.Status = 1 Then
                    _listProductMixingStation.Remove(detail)

                    INDgcDetail.DataSource = Nothing
                    INDgcDetail.DataSource = _listProductMixingStation
                    INDgcDetail.RefreshDataSource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Solo puede eliminarse un detalle de canasta cuando el proceso de materia prima de la campaña se encuentre en estado 'Calculada'"
                End If
            Case 4 'Solicitudes manuales
                If detail.DeliveredQuantity = 0 Then
                    DeleteProductCampaignDetail(detail)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = $"El detalle {detail.SupplyCodeName} no puede ser eliminado puesto que tiene cantidades entregadas"
                End If
            Case Else
                Mensaje(EeventViewerImages.Advertencia) = $"La opcion eliminar no esta disponible para los items tipo '{If(detail?.GroupName, String.Empty)}'"
        End Select

        INDviewDetail.EndUpdate()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRawMaterial_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleWarehouse.Focus()
        SetWarehouseInformation()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Click botón CUM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepBeCUM_Click(sender As Object, e As EventArgs) Handles INDrepBeCUM.Click
        If objCampaignDetailXpo.Status = 2 OrElse objCampaignDetailXpo.Status = 3 OrElse objCampaignDetailXpo.Status = 4 Then
            Me.Cursor = ChangeCursorIndigo()
            INDviewDetail.ShowLoadingPanel()
            Dim rowDetail = INDviewDetail.GetFocusedObject(Of CampaignDetailItems)

            'se agrega validacion para no permitir la accion CUM en elementos tipo canasta cuando la campaña esta en un estado diferente a Validacion
            If rowDetail IsNot Nothing AndAlso rowDetail.ItemType = 2 AndAlso objCampaignDetailXpo.Status <> 3 AndAlso objCampaignDetailXpo.Status <> 4 Then
                Mensaje(EeventViewerImages.Advertencia) = "Acción no permitida para los componentes de la canasta"
                Me.Cursor = Cursors.Default
                INDviewDetail.HideLoadingPanel()
                Exit Sub
            End If

            Dim frm As New PopupCUMateriaRaw(
                settingInventory:=Me._settingInventory,
                currentDate:=GetDateServer(),
                warehouseId:=If(INDsleWarehouse.EditValue Is Nothing, 0, INDsleWarehouse.EditValue),
                StockId:=StockXpo.Id_Warehouse
            )

            frm.StartPosition = FormStartPosition.CenterParent
            frm.UnitDoseTypeClass = objCampaignDetailXpo.UnitDoseTypeId.MSClass

            If rowDetail.AtcId IsNot Nothing Then
                frm.Product = rowDetail.AtcId
                frm.ProductType = 1
            ElseIf rowDetail.SupplyId IsNot Nothing Then
                frm.Product = rowDetail.SupplyId
                frm.ProductType = 2
            Else
                frm.Product = rowDetail.ProductId
                frm.ProductType = 3
            End If

            frm.QuantityDeliver = rowDetail.RequestQuantity
            AddHandler frm.GetItemsResult, AddressOf ReturnGetCUM
            AddHandler frm.LoadItemsCUM, AddressOf LoadItemsCUM
            frm.LoadData()
        End If
    End Sub

    Private ReadOnly Property ProductSelectedId(item As CampaignDetailItems)
        Get
            If item.AtcId IsNot Nothing Then
                Return item.AtcId
            ElseIf item.SupplyId IsNot Nothing Then
                Return item.SupplyId
            Else
                Return item.ProductId
            End If
        End Get
    End Property

    ''' <summary>
    ''' Cargamos los ítems para mostrar en la rejilla del popup CUM
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub LoadItemsCUM(sender As Object, e As EventArgs)
        Dim WareHouse As Integer = 0
        Dim Maquila As Integer? = 0
        Dim rowDetail = INDviewDetail.GetFocusedObject(Of CampaignDetailItems)
        Dim form = DirectCast(sender, PopupCUMateriaRaw)
        form.ShowLoadingGrid()

        If objCampaignDetailXpo.Status = 3 OrElse objCampaignDetailXpo.Status = 4 Then
            Maquila = MaquilaWareHouseId
        Else
            WareHouse = WareHouseId
            Maquila = MaquilaWareHouseId
        End If

        Using model As New MCampaign(Me.Tag)
            Dim listPhysicalInventoryCrystalProduct = Await model.ListPhysicalInventoryByATCSupplyProductAsync(rowDetail.CampaignDetailId, rowDetail.AtcId, rowDetail.SupplyId, rowDetail.ProductId, WareHouse, StockXpo.Id_Warehouse, Maquila)

            If listPhysicalInventoryCrystalProduct.IsNotNullAndAny() Then
                form.Title = listPhysicalInventoryCrystalProduct.Item(0).CodeNameProduct
                'Se realiza el ordenamiento de acuerdo al almacen y los productos con la fecha de vencimiento mas próxima
                listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
                    OrderBy(Function(d) If(WareHouseId = d.WarehouseId, 0, 1)).
                    ThenBy(Function(d) If(d.Covered, 0, 1)).
                    ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()

                Dim itemList As List(Of ICUMCampaign) = Nothing

                If objCampaignDetailXpo.Status = 2 Then
                    Dim campaignDetailPicking = Await model.GetCampaignDetailPickingByCampaignDetailId(rowDetail.CampaignDetailId)

                    If objCampaignDetailXpo.UnitDoseTypeId.MSClass = EUnitDoseTypeClass.Repackaging Then
                        listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct _
                            .FindAll(Function(i)
                                         Dim picking As CampaignDetailPicking = campaignDetailPicking? _
                                        .ObjectEmbbeded? _
                                        .FirstOrDefault(Function(m) m.ProductId = i.ProductId AndAlso m.WarehouseId = i.WarehouseId AndAlso m.BatchSerialId = i.BatchSerialId AndAlso m.ItemType = rowDetail.ItemType)
                                         Return picking IsNot Nothing AndAlso i.Quantity >= picking.AvailableQuantity
                                     End Function)
                    End If

                    itemList = listPhysicalInventoryCrystalProduct? _
                        .Select(Of ICUMCampaign)(Function(i)
                                                     Dim picking As CampaignDetailPicking = campaignDetailPicking? _
                                                     .ObjectEmbbeded? _
                                                     .FirstOrDefault(Function(m) m.ProductId = i.ProductId AndAlso m.WarehouseId = i.WarehouseId AndAlso m.BatchSerialId = i.BatchSerialId AndAlso m.ItemType = rowDetail.ItemType)

                                                     If picking.IsNull() Then
                                                         picking = New CampaignDetailPicking With {
                                                             .Id = 0,
                                                             .CampaignDetailId = rowDetail.CampaignDetailId,
                                                             .ProductId = i.ProductId,
                                                             .WarehouseId = i.WarehouseId,
                                                             .BatchSerialId = i.BatchSerialId,
                                                             .DeliveredQuantity = 0
                                                             }
                                                     End If

                                                     With picking
                                                         .ItemType = rowDetail.ItemType
                                                         .ProductFullName = i.CodeNameProduct
                                                         .WareHouseFullName = i.CodeNameWarehouse
                                                         .BatchSerialCode = i.CodeNameBatchSerial
                                                         .Covered = i.Covered
                                                         .ExpirationDate = i.BatchSerialExpiredDate
                                                         .AvailableQuantity = i.Quantity
                                                     End With

                                                     Return picking
                                                 End Function).ToList()
                ElseIf objCampaignDetailXpo.Status = 3 OrElse objCampaignDetailXpo.Status = 4 Then
                    Dim campaignDetailValidation = Await model.GetCampaignDetailValidationByCampaignDetailId(rowDetail.CampaignDetailId)

                    If objCampaignDetailXpo.UnitDoseTypeId.MSClass = EUnitDoseTypeClass.Repackaging Then
                        Dim outstandingQuantity = rowDetail.RequestQuantity - rowDetail.DeliveredQuantity

                        listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct _
                            .FindAll(Function(m) listPhysicalInventoryCrystalProduct _
                                            .Where(Function(o) o.BatchSerialId = m.BatchSerialId AndAlso o.ProductId = m.ProductId).Sum(Function(o) o.Quantity) >= outstandingQuantity)
                    End If

                    itemList = listPhysicalInventoryCrystalProduct? _
                        .Select(Of ICUMCampaign)(Function(i)
                                                     Dim validation As CampaignDetailValidation = campaignDetailValidation? _
                                                         .ObjectEmbbeded? _
                                                         .FirstOrDefault(Function(m) m.ProductId = i.ProductId AndAlso m.WarehouseId = i.WarehouseId AndAlso m.BatchSerialCode = i.BatchSerial?.BatchCode AndAlso m.ItemType = rowDetail.ItemType)

                                                     If validation.IsNull() Then
                                                         validation = New CampaignDetailValidation With {
                                                             .Id = 0,
                                                             .CampaignDetailId = rowDetail.CampaignDetailId,
                                                             .ProductId = i.ProductId,
                                                             .WarehouseId = i.WarehouseId,
                                                             .BatchSerialId = i.BatchSerialId,
                                                             .DeliveredQuantity = 0,
                                                             .TransferOrderQuantityTmp = 0
                                                    }
                                                     End If

                                                     With validation
                                                         .ItemType = rowDetail.ItemType
                                                         .ProductFullName = i.CodeNameProduct
                                                         .WareHouseFullName = i.CodeNameWarehouse
                                                         .BatchSerialCode = i.CodeNameBatchSerial
                                                         .Covered = i.Covered
                                                         .ExpirationDate = i.BatchSerialExpiredDate
                                                         .AvailableQuantity = i.Quantity
                                                         .TransferOrderQuantityTmp = validation.TransferOrderQuantity
                                                         .DeliveredQuantity -= validation.TransferOrderQuantity
                                                     End With
                                                     Return validation
                                                 End Function).ToList()
                End If
                form.DataSource = itemList
                Dim trasparent As New FrmTransparent(form, False)
                Me.Cursor = Cursors.Default
                INDviewDetail.HideLoadingPanel()
                trasparent.ShowDialog(Me)
                INDMeCUM.Focus()
                form.Dispose()
            Else
                If objCampaignDetailXpo.Status = 2 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente no tiene existencias en los almacenes de preferencia"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "El componente no tiene existencias en el almacén Stock"
                End If
                Me.Cursor = Cursors.Default
                form.HideLoadingGrid()
                INDviewDetail.HideLoadingPanel()
                form.Close()
                form.Dispose()
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Evento click del boton agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnComponents_Click(sender As Object, e As EventArgs) Handles INDbtnComponents.Click
        addProductsCampaignDetail = Nothing
        OpenFormAddProductsCampaignDetail(False)
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' EditValueChanged para el cambio de canasta.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleBakets_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBakets.EditValueChanged
        If INDgviewBaskets.RowCount = 0 OrElse objCampaignDetailXpo.Status <> 1 Then
            'Mensaje(EeventViewerImages.Advertencia) = "No se encuentran canastas para poder asociar."
            Exit Sub
        End If

        If INDsleBakets.EditValue IsNot Nothing Then
            If Not objCampaignDetailXpo.ProductionBasketId.HasValue Then
                If MessageIndigo.Show("¿Desea asociar la canasta seleccionada?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    INDsleBakets.EditValue = Nothing
                    Return
                End If
            End If

            If objCampaignDetailXpo.ProductionBasketId.HasValue AndAlso INDsleBakets.EditValue <> objCampaignDetailXpo.ProductionBasketId Then
                If MessageIndigo.Show("¿Desea reemplazar la canasta asociada?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    INDsleBakets.EditValue = objCampaignDetailXpo.ProductionBasketId
                    Return
                End If
            End If

            Try
                Using model As New MCampaign(Me.Tag)
                    AsyncLoader(True)
                    Dim result = Await model.SaveBasketsMateriaRaw(objCampaignDetailXpo.Id, objCampaignDetailXpo.CampaignId.Id, INDsleBakets.EditValue)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = "Asociación de la canasta correctamente"
                        objCampaignDetailXpo.ProductionBasketId = INDsleBakets.EditValue
                        Await SetDatasourceAsync()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = "No se guardo la asociación de la canasta"
                    End If

                    Me.AsyncLoader(False)
                End Using
            Catch ex As Exception
                Me.AsyncLoader(False)
                Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Sub
#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para concatenar el Code - Name Materia Prima 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <returns></returns>
    Private Function getItemCode(row As CampaignDetailItems) As String
        If row.AtcId.HasValue Then
            Return row.AtcCodeName
        End If
        If row.ProductId.HasValue Then
            Return row.ProductCodeName
        End If
        If row.SupplyId.HasValue Then
            Return row.SupplyCodeName
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Grid Event -Asignar valores Campaign Detail Status 2 - 4 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewDetail_CustomUnboundColumnData(sender As Object, e As Views.Base.CustomColumnDataEventArgs) Handles INDviewDetail.CustomUnboundColumnData
        If objCampaignDetailXpo.Status = 2 OrElse objCampaignDetailXpo.Status = 3 OrElse objCampaignDetailXpo.Status = 4 Then
            Dim row = DirectCast(e.Row, CampaignDetailItems)
            Select Case e.Column.Name
                Case INDColItem.Name
                    e.Value = getItemCode(e.Row)
                Case INDColAmountPending.Name
                    If objCampaignDetailXpo.Status = 2 Then
                        e.Value = row.RequestQuantity - row.QuantityRemnant - row.QuantityStock - row.QuantityWarehouse - row.QuantityMaquila
                    ElseIf objCampaignDetailXpo.Status = 3 OrElse objCampaignDetailXpo.Status = 4 Then
                        e.Value = row.RequestQuantity - row.DeliveredQuantity
                    End If
                Case INDColRequiredQuantity.Name
                    e.Value = row.RequestQuantity
                Case INDColRawMaterial.Name
                    e.Value = row.QuantityStock + row.QuantityWarehouse + row.QuantityMaquila
                Case INDColWarehouseMaquila.Name
                    e.Value = row.QuantityMaquila
                Case INDColPreferenceWarehouse.Name
                    e.Value = row.QuantityWarehouse
            End Select
        End If
    End Sub
#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Click Picking Dosis Unitarias.
    ''' </summary>
    Private Async Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Await Picking()
    End Sub

    ''' <summary>
    ''' Click Guardar y Confirmar.
    ''' </summary>
    Private Sub BarraBotones_clickGuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        OrdenTraslado()
    End Sub

    ''' <summary>
    ''' Click Procesar Solicitud.
    ''' </summary>
    Private Sub BarraBotones_clickConfirmar() Handles BarraBotones.ClickConfirmar
        TrasladoInventario()
    End Sub

    Private Async Sub BarraBotones_Click_EntregaManual() Handles BarraBotones.Click_EntregaManual
        Me.Cursor = ChangeCursorIndigo()
        AsyncLoader(True)
        Await ManualDelivery()
        AsyncLoader(False)
        Me.Cursor = Cursors.Default
        INDMeCUM.Focus()
    End Sub

    ''' <summary>
    ''' Click Valida Lotes.
    ''' </summary>
    Private Sub BarraBotones_ClickValidar() Handles BarraBotones.ClickProcesar
        ValidateBatches()
    End Sub

    Private Async Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim reportDefTirilla As New Reporter.rptAdecuationPlan()
        If objCampaignDetailXpo.UnitDoseTypeId.MSClass <> EUnitDoseTypeClass.ParenteralNutrition Then
            ' Agregar False al final para NO usar paginación (modo Campaña)
            reportDefTirilla.ParametrosReporte = {_objCampaignDetailXpo.Id, StockXpo.Id_Warehouse, If(WarenhouseXpo Is Nothing, 0, WarenhouseXpo.Id_Warehouse), 2, _objCampaignDetailXpo.CampaignNumber, _objCampaignDetailXpo.CampaignStatusName, _objCampaignDetailXpo.UnitDoseTypeId.Description, _objCampaignDetailXpo.UnitDoseTypeId.MSClass, False}
            reportDefTirilla.Parameters("TypeUnit").Value = 2

            Await reportDefTirilla.CargarDataSourceAsync()
        End If

        Dim reportMl As New Reporter.rptAdecuationPlan()
        ' Agregar False al final para NO usar paginación (modo Campaña)
        reportMl.ParametrosReporte = {_objCampaignDetailXpo.Id, StockXpo.Id_Warehouse, If(WarenhouseXpo Is Nothing, 0, WarenhouseXpo.Id_Warehouse), 1, _objCampaignDetailXpo.CampaignNumber, _objCampaignDetailXpo.CampaignStatusName, _objCampaignDetailXpo.UnitDoseTypeId.Description, _objCampaignDetailXpo.UnitDoseTypeId.MSClass, False}
        reportMl.Parameters("TypeUnit").Value = 1

        Await reportMl.CargarDataSourceAsync()

        If waitForm.IsSplashFormVisible Then
            waitForm.CloseWaitForm()
        End If

        INDSvFile.FileName = $"Plan_Adecuacion_{GetTimeStamp()}.xlsx"
        INDSvFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

        If INDSvFile.ShowDialog(Me) = DialogResult.OK Then

            Dim combinedReport As New XtraReport()
            combinedReport.PaperKind = System.Drawing.Printing.PaperKind.Custom
            combinedReport.Bands.Add(New DetailBand())

            ' Configurar subreporte para unidades de peso
            Dim isFirstPageLoaded = False
            If reportDefTirilla.DataSource IsNot Nothing AndAlso reportDefTirilla.DataSource.Count > 0 Then
                combinedReport.PageWidth = reportDefTirilla.PageWidth
                combinedReport.PageHeight = reportDefTirilla.PageHeight

                isFirstPageLoaded = True
                Dim subreport1 As New XRSubreport()
                subreport1.ReportSource = reportDefTirilla
                subreport1.LocationF = New PointF(20, 0)
                subreport1.SizeF = New SizeF(reportDefTirilla.PageWidth, reportDefTirilla.PageHeight)
                combinedReport.Bands(0).Controls.Add(subreport1)
            End If

            '' Configurar subreporte para unidades de volumen
            If reportMl.DataSource IsNot Nothing AndAlso reportMl.DataSource.Count > 0 Then
                combinedReport.PageWidth = reportMl.PageWidth
                combinedReport.PageHeight = reportMl.PageHeight

                Dim subreport2 As New XRSubreport()
                subreport2.ReportSource = reportMl
                subreport2.LocationF = IIf(isFirstPageLoaded, New PointF(20, reportDefTirilla.PageHeight + 10), New PointF(20, 0))
                subreport2.SizeF = New SizeF(reportMl.PageWidth, reportMl.PageHeight)
                combinedReport.Bands(0).Controls.Add(subreport2)
            End If

            Dim options As New XlsxExportOptions()
            options.ExportMode = XlsxExportMode.SingleFilePageByPage

            If objCampaignDetailXpo.UnitDoseTypeId.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                options.SheetName = "Nutrición parenteral "
            Else
                options.SheetName = "Unidad de medida "
            End If

            combinedReport.ExportToXlsx(INDSvFile.FileName, options)

            Process.Start(INDSvFile.FileName)
        End If
    End Sub

    Private Sub BarraBotones_Click_LoadPurcharseOrder() Handles BarraBotones.Click_LoadPurcharseOrder

        If objCampaignDetailXpo.Status <> 4 Then
            Return
        End If

        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Try
            Dim reportePrincipal As New XtraReport

            ' Generar el reporte paginado
            Using popup As New PopUpCampaignBatchRecord()
                popup.GeneratePaginatedReportByBatches(reportePrincipal, {
                objCampaignDetailXpo.Id,
                StockXpo.Id_Warehouse,
                If(WarenhouseXpo IsNot Nothing, WarenhouseXpo.Id_Warehouse, 0),
                2,
                objCampaignDetailXpo.CampaignNumber,
                objCampaignDetailXpo.CampaignStatusName,
                objCampaignDetailXpo.UnitDoseTypeId.Description,
                objCampaignDetailXpo.UnitDoseTypeId.MSClass
            })
            End Using

            If waitForm.IsSplashFormVisible Then
                waitForm.CloseWaitForm()
            End If

            ' Mostrar el reporte
            If reportePrincipal.Pages.Count > 0 Then
                reportePrincipal.PrintingSystem.ContinuousPageNumbering = True
                reportePrincipal.ShowPreviewDialog()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No hay datos disponibles para el reporte"
            End If

        Catch ex As Exception
            If waitForm.IsSplashFormVisible Then
                waitForm.CloseWaitForm()
            End If
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Function GetTimeStamp() As Int64
        Return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() 'DirectCast((DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds, Int64)
    End Function
#End Region

#End Region

End Class