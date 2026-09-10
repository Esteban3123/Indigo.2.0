#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmPopupWareHouse

#Region "DataSource"

    Private _FillingWareHouseType As List(Of Tuple(Of Byte, String))

    Private ReadOnly Property FillingWareHouseType As List(Of Tuple(Of Byte, String))
        Get
            If _FillingWareHouseType Is Nothing Then
                _FillingWareHouseType = New List(Of Tuple(Of Byte, String))
                _FillingWareHouseType.Add(New Tuple(Of Byte, String)(1, "Materia Prima Stock"))
                _FillingWareHouseType.Add(New Tuple(Of Byte, String)(2, "Almacén"))
                _FillingWareHouseType.Add(New Tuple(Of Byte, String)(3, "En Proceso"))
                _FillingWareHouseType.Add(New Tuple(Of Byte, String)(4, "Producto Terminado"))
                _FillingWareHouseType.Add(New Tuple(Of Byte, String)(5, "Inventario de Control"))
                _FillingWareHouseType.Add(New Tuple(Of Byte, String)(6, "Inventario de Remanente"))
            End If
            Return _FillingWareHouseType
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "MixingStation"

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PCMConfig

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Private _CMWarehouseEntity As CMWarehouse

    ''' <summary>
    ''' Enum Source para Saber desde que Dasboard se envia al Abrir Popup de Almacén
    ''' </summary>
    Public INDSource As eSource

    Dim WareHouseId As Integer
    Dim WareHouseDescription As String
    Dim listWareHouse As List(Of WarehouseXpo)

    Public CMWarehouse As New TrackableCollection(Of CMWarehouse)


#End Region

#Region "Enumerations"
    ''' <summary>
    ''' Enum Source para Saber desde que Dashboard se envia al Abrir Popup de Almacén 
    ''' </summary>
    Enum eSource
        CMConfig = 0
        DashboardQualityControl = 1
    End Enum
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece código y el nombre del producto para el detalle del paquete
    ''' </summary>
    Public Property CodeWareHouse As String
        Get
            Return INDsleItemCode.EditValue
        End Get
        Set(value As String)
            INDsleItemCode.EditValue = value
        End Set
    End Property

    Private _MixingStationId As Integer

    Public Property MixingStationId() As Integer
        Get
            Return _MixingStationId
        End Get
        Set(ByVal value As Integer)
            _MixingStationId = value
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
        form.Size = New Size(800, 730)
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
        CodeWareHouse = Nothing
        INDsleDescription.Properties.NullText = Nothing
        INDGleType.EditValue = Nothing
        Me.WareHouseId = Nothing
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupWareHouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        If _presenter Is Nothing Then _presenter = New PCMConfig()
        If INDSource = eSource.DashboardQualityControl Then
            HideControl(INDlciType, True)
        End If
        INDGleType.Properties.DataSource = FillingWareHouseType
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _FillingWareHouseType = Nothing
        Me.WareHouseId = Nothing
    End Sub

#End Region

#Region "Click"

    Private Sub INDsleItemCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleItemCode.EditValueChanged
        If Not String.IsNullOrEmpty(INDsleItemCode.EditValue) Then
            listWareHouse = _presenter.GetWareHouseByCode(CodeWareHouse)

            If listWareHouse IsNot Nothing Then
                For Each i As WarehouseXpo In listWareHouse
                    WareHouseId = i.Id
                    WareHouseDescription = i.Name
                    INDsleDescription.Properties.NullText = i.Name
                Next
            Else
                INDsleDescription.Properties.NullText = String.Empty
            End If
        Else
            INDsleDescription.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Agrega los datos  al detalle de paquete
    ''' </summary>
    Private Sub AddCenterAttention()

        _CMWarehouseEntity = New CMWarehouse

        With _CMWarehouseEntity
            .IdWarehouse = WareHouseId
            If Not INDSource = eSource.DashboardQualityControl Then
                .WarehouseType = INDGleType.EditValue
                Select Case INDGleType.EditValue
                    Case 1
                        .WarehouseTypeName = "Materia Prima Stock"
                    Case 2
                        .WarehouseTypeName = "Almacén"
                    Case 3
                        .WarehouseTypeName = "En Proceso"
                    Case 4
                        .WarehouseTypeName = "Terminado"
                    Case 5
                        .WarehouseTypeName = "Control"
                    Case 6
                        .WarehouseTypeName = "Remanente"
                End Select
            End If
            .StateWH = True
            .StatusName = "Activo"
            .WareHouseCode = CodeWareHouse
            .Description = WareHouseDescription
            .Agregado = True
            .ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
        End With
    End Sub

    ''' <summary>
    ''' Agrega el centro de atención a la central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Try
            If CodeWareHouse Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un codigo de almacen"
                Exit Sub
            End If
            If Not INDSource = eSource.DashboardQualityControl Then
                If INDGleType.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de almacén"
                    Exit Sub
                End If
            End If
            AddCenterAttention()
            CMWarehouse.Add(_CMWarehouseEntity)

            Me.Close()
        Catch ex As Exception
            INDbtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        If INDSource = eSource.DashboardQualityControl Then
            HideControl(INDlciType)
        End If
    End Sub

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' QueryPopup al Agregar un nuevo Almacén
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleItemCode_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleItemCode.QueryPopUp
        If INDsleItemCode.Properties.DataSource Is Nothing Then
            If INDSource = eSource.DashboardQualityControl Then
                INDsleItemCode.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListAssignWarehouseDashboardQualityControl()
            Else

                If INDGleType.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de almacén"
                    Exit Sub
                End If

                If INDGleType.EditValue = 5 Then
                    INDsleItemCode.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListControlWarehouse()
                ElseIf INDGleType.EditValue = 6 Then
                    INDsleItemCode.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehousebyType(INDGleType.EditValue)
                Else
                    INDsleItemCode.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListWarehouseNoCustody()
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Se activa cuando se Selecciona un tipo de Almacén
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        INDsleItemCode.Properties.DataSource = Nothing
        INDsleDescription.Properties.NullText = String.Empty
    End Sub
#End Region
End Class