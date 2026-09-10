'***********************************************************************
' Assembly         : MixingStation
' Author           : Juan David Patiño Cabrera
' Created          : 21-07-2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

#End Region

Public Class PopupCUMateriaRaw

#Region "BUILDER"

    ''' <summary>
    ''' Constructor del modal
    ''' </summary>
    ''' <param name="settingInventory"></param>
    ''' <param name="currentDate">Fecha</param>
    ''' <param name="warehouseId">Id Almacen</param>
    ''' <param name="StockId">Id Stock</param>
    Public Sub New(settingInventory As SettingInventory, currentDate As Date, warehouseId As Integer?, StockId As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me._settingInventory = settingInventory
        Me._currentDate = currentDate
        Me._warehouseId = warehouseId
        Me._StockId = StockId
    End Sub

#End Region

#Region "EVENTS"
    Public Event GetItemsResult(senser As Object, e As GetCUMEventArgs)
    Public Event LoadItemsCUM(senser As Object, e As EventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' codigo del producto de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _productCode As String
    ''' <summary>
    ''' cantidad que se va a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _quantityDeliver As Integer

    Private _dataSource As List(Of ICUMCampaign)
    ''' <summary>
    ''' Inyecta los datos al datasource de la rejilla
    ''' </summary>
    Public WriteOnly Property DataSource() As List(Of ICUMCampaign)
        Set(value As List(Of ICUMCampaign))
            _dataSource = value
            INDGcCUM.DataSource = value
            INDGvCUM.HideLoadingPanel()

            If value.IsNotNullAndAny() Then
                IndigoGridControl1.RefreshGrid(INDGcCUM)
                INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End Set
    End Property

    '''' <summary>
    '''' listado del inventario fisico por el codigo atc
    '''' </summary>
    '''' <remarks></remarks>
    'Dim listPhysicalInventoryCrystalProduct As List(Of PhysicalInventory)

    ''' <summary>
    ''' Fecha actual 
    ''' </summary>
    Dim _currentDate As Date
    ''' <summary>
    ''' parámetros de inventario
    ''' </summary>
    Dim _settingInventory As SettingInventory
    ''' <summary>
    ''' Fecha actual 
    ''' </summary>
    Dim _warehouseId As Integer?
    ''' <summary>
    ''' Id stock
    ''' </summary>
    Dim _StockId As Integer?

    Public RequestedQuantity As Integer
#End Region

#Region "PROPERTIES"

    Dim _careGroupId As Integer
    Public WriteOnly Property CareGroupId As Integer
        Set(value As Integer)
            _careGroupId = value
        End Set
    End Property

    Dim _productType As Integer
    Public WriteOnly Property ProductType As Integer
        Set(value As Integer)
            _productType = value
        End Set
    End Property

    Public WriteOnly Property Product As String
        Set(value As String)
            Dim values = value.Split(" - ")
            _productCode = values(0)
        End Set
    End Property

    Public WriteOnly Property QuantityDeliver As Integer
        Set(value As Integer)
            _quantityDeliver = value
            INDLblQuantity.Text = "Cantidad Solicitada: " + value.ToString()
        End Set
    End Property

    Public WriteOnly Property Title As String
        Set(value As String)
            INDLcgMain.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Validar productos próximos a vencer
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValidateBatchSerialExpiredDate As Boolean
        Get
            Return If(Me._settingInventory Is Nothing, False, Me._settingInventory.ValidateBatchSerialExpiredDate)
        End Get
    End Property

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

    Public Property UnitDoseTypeClass As Integer?

#End Region

#Region "HANDLES"

#Region "Load"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupCUM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvCUM.OptionsView.ShowAutoFilterRow = False
        verifyInventorySettings()
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Dim args As New GetCUMEventArgs()

        Dim itemsToValidate = _dataSource.FindAll(Function(x) x.DeliveredQuantity > 0 OrElse x.Id > 0)
        If itemsToValidate.IsNull() Or itemsToValidate.Count = 0 Then
            Mensaje(EeventViewerImages.Informacion) = "No se entrego ningún producto"
            Me.Close()
            Exit Sub
        End If

        If itemsToValidate.IsNotNullAndAny() AndAlso Me._warehouseId IsNot Nothing AndAlso Me.ValidateBatchSerialExpiredDate Then
            Dim validateExpired As Boolean = False
            Dim oustandingQuantityDeliver = itemsToValidate.Sum(Function(d) d.DeliveredQuantity)
            Dim transferOrderQuantity = itemsToValidate.Sum(Function(d) d.TransferOrderQuantityTmp)
            If (oustandingQuantityDeliver + transferOrderQuantity) > _quantityDeliver Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar por todos los productos supera la cantidad solicitada"
                Exit Sub
            End If

            For Each physical In _dataSource
                Dim itemDeliver = itemsToValidate.FirstOrDefault(Function(d) d.Id = physical.Id AndAlso d.WareHouseId = physical.WareHouseId AndAlso d.ProductId = physical.ProductId AndAlso d.BatchSerialId.Equals(physical.BatchSerialId))

                If itemDeliver Is Nothing Then
                    'Si no se encuentra el item dentro de los productos a entregar
                    If physical.BatchSerialId IsNot Nothing Then
                        'Y este maneja lote, no se esta entregando el próximo a vencer
                        validateExpired = True
                    End If
                Else
                    'Si se encuentra el item dentro de los productos a entregar
                    oustandingQuantityDeliver = _quantityDeliver - oustandingQuantityDeliver
                    If physical.BatchSerialId IsNot Nothing Then
                        'Y este maneja lote
                        If oustandingQuantityDeliver > 0 AndAlso physical.AvailableQuantity > itemDeliver.DeliveredQuantity Then
                            'Y aún queda cantidades por entregar en el item y del total entregao, no se esta entregando el próximo a vencer
                            validateExpired = True
                        End If
                    End If
                End If

                If validateExpired OrElse Not oustandingQuantityDeliver > 0 Then
                    Exit For
                End If
            Next

            If _dataSource.Count > 1 AndAlso validateExpired AndAlso MessageIndigo.Show("El producto seleccionado no es el próximo a vencer, ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            args.ListItemDetails = itemsToValidate
            RaiseEvent GetItemsResult(Nothing, args)

        End If
        Me.Close()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub PopupCUM_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    Private Function ValidateRepack(quantity As Decimal) As Boolean
        If UnitDoseTypeClass = 5 Then
            Dim items = _dataSource
            Dim itemSelected = INDGvCUM.GetFocusedObject(Of ICUMCampaign)()
            Dim count = items.Where(Function(m) m.DeliveredQuantity > 0 OrElse (m.GetHashCode() = itemSelected.GetHashCode() AndAlso m.DeliveredQuantity + quantity > 0)) _
                .GroupBy(Function(m) m.BatchSerialCode).Count()

            If count > 1 Then
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then Exit Sub

        Dim oldValue = e.OldValue

        If oldValue Is String.Empty Then oldValue = 0

        Dim physicalInventoryTmp = INDGvCUM.GetFocusedObject(Of ICUMCampaign)

        If physicalInventoryTmp.DeliveredQuantityTmp Is Nothing Then
            physicalInventoryTmp.DeliveredQuantityTmp = CInt(oldValue) + physicalInventoryTmp.TransferOrderQuantityTmp
        End If

        Dim listTmp = _dataSource
        If e.NewValue > physicalInventoryTmp.AvailableQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad en el inventario del producto " + physicalInventoryTmp.ProductFullName + " es menor a la cantidad a entregar "
            e.Cancel = True
            Exit Sub
        End If

        If Not ValidateRepack(e.NewValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "El producto seleccionado deber ser del mismo lote del producto anterior"
            e.Cancel = True
            Return
        End If

        Dim quantity = listTmp _
            .Where(Function(x) Not x.Equals(physicalInventoryTmp)) _
            .Sum(Function(x) x.DeliveredQuantity) + e.NewValue

        If quantity > _quantityDeliver Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar por todos los productos supera la cantidad solicitada"
            e.Cancel = True
            Return
        End If

        'TransferOrder
        Dim quantityTranferOrder As Integer = listTmp _
        .Sum(Function(x) x.TransferOrderQuantityTmp)

        quantityTranferOrder += quantity
        If quantityTranferOrder > _quantityDeliver Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar del producto " + physicalInventoryTmp.ProductFullName + " supera la cantidad solicitada"
            e.Cancel = True
            Exit Sub
        End If


        Dim quantityToDeliver As Integer = _quantityDeliver
        '''Se valida la cantidad de productos que aún están disponibles para entregar 
        If physicalInventoryTmp.TransferOrderQuantityTmp IsNot Nothing Then
            quantityToDeliver = _quantityDeliver - physicalInventoryTmp.TransferOrderQuantityTmp '''cantidad productos transferidos
        End If

        '''Si la cantidad a entregar es mayor que la cantidad de productos que ya se han transferido tmp
        If quantity > quantityToDeliver Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar del producto " + physicalInventoryTmp.ProductFullName + " supera la cantidad solicitada"
            e.Cancel = True
            Exit Sub
        End If

        physicalInventoryTmp.DeliveredQuantity = e.NewValue
        physicalInventoryTmp.TypeProcess = 1
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cantidad solicitada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseRequestQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDseRequestQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If

        'Se valida que si la cantidad digitada es menor a la cantidad entregada en la rejilla
        If _dataSource IsNot Nothing AndAlso _dataSource.Any() Then
            If e.NewValue < _dataSource.Sum(Function(x) x.DeliveredQuantity) Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad solicitada no puede ser menor a la sumatoria de la cantidad entregada en la rejilla"
                e.Cancel = True
                Exit Sub
            End If
        End If

        _quantityDeliver = e.NewValue
    End Sub

#End Region

#Region "RowStyle"
    ''' <summary>
    ''' RowStyle utilizado para pintar las filas dependiendo de la fecha de expiración
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCUM_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles INDGvCUM.RowStyle
        Dim view As GridView = TryCast(sender, GridView)
        Dim _expirationDate As DateTime? = view.GetRowCellValue(e.RowHandle, "ExpirationDate")
        If _expirationDate IsNot Nothing Then
            If Me._settingInventory IsNot Nothing Then
                If Me._settingInventory.BatchSerialRange IsNot Nothing AndAlso Me._settingInventory.BatchSerialRange.Any Then
                    Dim diff = DateDiff(DateInterval.Day, Me._currentDate, _expirationDate.Value)
                    Dim batchSerialRange As BatchSerialRange = Nothing
                    If diff <= 0 Then
                        batchSerialRange = Me._settingInventory.BatchSerialRange.FirstOrDefault()
                    Else
                        batchSerialRange = Me._settingInventory.BatchSerialRange.FirstOrDefault(Function(r) r.InitialRange <= diff AndAlso r.EndRange >= diff)
                    End If
                    If batchSerialRange IsNot Nothing Then
                        'e.Appearance.ForeColor = ForeColor
                        e.Appearance.BackColor = System.Drawing.Color.FromArgb(batchSerialRange.Color)
                        e.HighPriority = True
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Muestra el icono de cargando de la rejilla
    ''' </summary>
    Public Sub ShowLoadingGrid()
        INDGvCUM.ShowLoadingPanel()
    End Sub

    ''' <summary>
    ''' Oculta el cargando de la rejilla
    ''' </summary>
    Public Sub HideLoadingGrid()
        INDGvCUM.HideLoadingPanel()
    End Sub

#End Region

#End Region

#Region "Privated methods"
    ''' <summary>
    ''' Valida si los parámetros de inventario se enviaron o no
    ''' </summary>
    Private Sub verifyInventorySettings()
        If Me._settingInventory.IsNull() Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de inventario"
            Me.Close()
        End If
    End Sub

    Friend Sub LoadData()
        RaiseEvent LoadItemsCUM(Me, New EventArgs())
    End Sub
#End Region

End Class

''' <summary>
''' clase para retornar en el evento de seleccionar productos
''' </summary>
''' <remarks></remarks>
Public Class GetCUMEventArgs
    Inherits EventArgs

    Property ListItemDetails As List(Of ICUMCampaign)

    Property ListItemDetailvalidation As List(Of CampaignDetailValidation)


End Class