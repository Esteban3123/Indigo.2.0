'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 25-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Inventory.MVP

#End Region

Public Class PopupCUM

#Region "BUILDER"

    ''' <summary>
    ''' Constructor del modal
    ''' </summary>
    ''' <param name="dispensingIntegration">0 = No hay integración, 1 = Hay integración con Heon, 2 = Hay integración entre Medilaser y FarmaQx</param>
    Public Sub New(Optional settingInventory As SettingInventory = Nothing, Optional currentDate As Date = Nothing, Optional warehouseId As Integer? = Nothing, Optional dispensingIntegration As Integer = 0)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._settingInventory = settingInventory
        Me._currentDate = currentDate
        Me._warehouseId = warehouseId
        Me.DispensingIntegration = dispensingIntegration
    End Sub

    ''' <summary>
    ''' Constructor del modal
    ''' </summary>
    ''' <param name="dispensingIntegration">0 = No hay integración, 1 = Hay integración con Heon, 2 = Hay integración entre Medilaser y FarmaQx</param>
    ''' <param name="custody"></param>
    ''' <param name="admissionNumber"></param>
    Public Sub New(settingInventory As SettingInventory, currentDate As Date, warehouseId As Integer?, dispensingIntegration As Integer, custody As Boolean, admissionNumber As String)
        ' This call is required by the designer.
        InitializeComponent()

        Me._settingInventory = settingInventory
        Me._currentDate = currentDate
        Me._warehouseId = warehouseId
        Me.DispensingIntegration = dispensingIntegration
        Me.Custody = custody
        Me._admissionNumber = admissionNumber
        If custody Then
            Me.AdmissionNumber = admissionNumber
        End If
    End Sub

#End Region

#Region "EVENTS"
    Public Event GetCUM(senser As Object, e As GetCUMEventArgs)
#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' Consecutivo de la solicitud de farmacia
    ''' </summary>
    ''' <remarks></remarks>
    Private _consecutive As Integer

    ''' <summary>
    ''' Dosis solicitada del producto de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Private _DoseDeliver As Decimal?

    ''' <summary>
    ''' Dosis solicitada del producto de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Private _DoseDeliverMeasurement As String

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
    ''' <summary>
    ''' Código susceptible para productos procesados por central de mezclas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _codeSusceptibleMixingStation As String
    ''' <summary>
    ''' listado del inventario fisico por el codigo atc
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPhysicalInventoryCrystalProduct As List(Of PhysicalInventory)

    ''' <summary>
    ''' listado del inventario fisico por el codigo atc
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Dim listPhysicalInventoryCustodyProduct As List(Of PhysicalInventoryCustody)

    ''' <summary>
    ''' 0 = No hay integración, 1 = Hay integración con Heon, 2 = Hay integración entre Medilaser y FarmaQx
    ''' </summary>
    Private DispensingIntegration As Integer

    ''' <summary>
    ''' Cantidad que viene desde el request de HEON
    ''' </summary>
    Public QuantityAsignedByHEON As Integer = 0

    ''' <summary>
    ''' Detalle de la solicitud de HEON para asignar los valores del tableLayout
    ''' </summary>
    Public solicitudDetalle As SolicitudDetalleObject

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Public Custody As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Public AdmissionNumber As String

    ''' <summary>
    ''' numero de ingreso para enviar como parametro para consulta cum central de mezclas
    ''' </summary>
    Private _admissionNumber As String

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
            INDLcgMain.Text = value
        End Set
    End Property

    Public WriteOnly Property ProductCode As String
        Set(value As String)
            _productCode = value
        End Set
    End Property

    Public WriteOnly Property QuantityDeliver As Integer
        Set(value As Integer)
            _quantityDeliver = value
            INDlyItemLabel.Text = "Cantidad Solicitada: " + value.ToString()
        End Set
    End Property

    Public WriteOnly Property Consecutive As Integer
        Set(value As Integer)
            _consecutive = value
        End Set
    End Property

    Public WriteOnly Property DoseDeliver As Decimal?
        Set(value As Decimal?)
            _DoseDeliver = value
            SimpleLabelItem9.Text = "Dosis total preescrita: " + value.ToString()
        End Set
    End Property

    Public WriteOnly Property DoseDeliverMeasurement As String
        Set(value As String)
            _DoseDeliverMeasurement = value
            SimpleLabelItem9.Text = String.Concat(SimpleLabelItem9.Text, " ", RTrim(value)).ToString()
        End Set
    End Property

    Public WriteOnly Property CodeSusceptibleMixingStation As String
        Set(value As String)
            _codeSusceptibleMixingStation = value
        End Set
    End Property

    ''' <summary>
    ''' vandera que permite sabe si necesitamos filtrar por productos tipo item produccion
    ''' </summary>
    Dim _itemProduction As Boolean = False
    Public WriteOnly Property ItemProduction As Boolean
        Set(value As Boolean)
            _itemProduction = value
        End Set
    End Property

    ''' <summary>
    ''' listado de el inventario fisico que se pasa desde el formulario principal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPhysicalInventory As List(Of PhysicalInventory)
    Public WriteOnly Property ListPhysicalInventory As List(Of PhysicalInventory)
        Set(value As List(Of PhysicalInventory))
            _listPhysicalInventory = value
        End Set
    End Property

    ''' <summary>
    ''' listado de el inventario fisico que se pasa desde el formulario principal
    ''' </summary>
    ''' <remarks>HRR PBI3410</remarks>
    Dim _listPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)
    Public WriteOnly Property ListPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)
        Set(value As List(Of PhysicalInventoryCustody))
            _listPhysicalInventoryCustody = value
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

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub PopupCUM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvCUM.OptionsView.ShowAutoFilterRow = False

        If _DoseDeliver Is Nothing Then
            SimpleLabelItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgDeliveriesTypeProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
#End Region

#Region "Shown"
    Private Async Sub PopupCUM_ShownPhysicalInventory()
        Using model As New MDashBoardPharmacy(Me.Tag)
            listPhysicalInventoryCrystalProduct = New List(Of PhysicalInventory)
            If _itemProduction Then
                Dim Result = Await model.ListPhysicalInventoryByATCCodeToMS(_productCode, _admissionNumber, _codeSusceptibleMixingStation)
                If Result?.Message?.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
                If Result IsNot Nothing AndAlso Result.StateResult = True Then
                    listPhysicalInventoryCrystalProduct = Result.ObjectEmbbeded
                End If
            Else
                listPhysicalInventoryCrystalProduct = model.ListPhysicalInventoryByATCNumberWithAdditionalInformation(_productCode, _productType, _careGroupId, _DoseDeliver)
            End If
            If listPhysicalInventoryCrystalProduct.Count > 0 Then
                'Se realiza el ordenamiento de acuerdo al almacen y los productos con la fecha de vencimiento mas próxima
                listPhysicalInventoryCrystalProduct = listPhysicalInventoryCrystalProduct.
                    OrderBy(Function(d) If(Me._warehouseId IsNot Nothing AndAlso Me._warehouseId = d.WarehouseId, 0, 1)).
                    ThenBy(Function(d) If(d.Covered, 0, 1)).
                    ThenBy(Function(d) If(d.BatchSerialExpiredDate Is Nothing, DateTime.MaxValue, d.BatchSerialExpiredDate)).ToList()

                If _listPhysicalInventory IsNot Nothing Then
                    For Each item In _listPhysicalInventory
                        Dim physicalInventoryTmp As PhysicalInventory = Nothing

                        ' Primero buscar por Id del PhysicalInventory (es único)
                        If item.Id > 0 Then
                            physicalInventoryTmp = listPhysicalInventoryCrystalProduct.FirstOrDefault(Function(x) x.Id = item.Id)
                        End If

                        ' Si no se encontró por Id, buscar por BatchSerialId (fallback para compatibilidad)
                        If physicalInventoryTmp Is Nothing AndAlso item.BatchSerialId IsNot Nothing Then
                            physicalInventoryTmp = listPhysicalInventoryCrystalProduct.FirstOrDefault(Function(x) _
                                x.BatchSerialId = item.BatchSerialId AndAlso
                                x.ProductId = item.ProductId AndAlso
                                x.WarehouseId = item.WarehouseId)
                        End If

                        ' Si aún no se encontró y no tiene BatchSerialId, buscar por ProductId y WarehouseId
                        If physicalInventoryTmp Is Nothing AndAlso item.BatchSerialId Is Nothing Then
                            physicalInventoryTmp = listPhysicalInventoryCrystalProduct.FirstOrDefault(Function(x) _
                                x.ProductId = item.ProductId AndAlso
                                x.WarehouseId = item.WarehouseId)
                        End If

                        If physicalInventoryTmp IsNot Nothing Then
                            physicalInventoryTmp.QuantityDeliver = item.QuantityDeliver
                        End If
                    Next
                End If
                INDGcCUM.DataSource = Nothing

                INDGcCUM.DataSource = listPhysicalInventoryCrystalProduct
            Else
                IndigoGridControl1.RefreshGrid(INDGcCUM)
                INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Using
    End Sub

    Private Sub PopupCUM_ShownPhysicalInventoryCustody()
        Using model As New MDashBoardPharmacy(Me.Tag)
            listPhysicalInventoryCustodyProduct = model.ListPhysicalInventoryCustodyByATCNumber(_productCode, _productType, AdmissionNumber)
            If listPhysicalInventoryCustodyProduct.Count > 0 Then
                If _listPhysicalInventoryCustody IsNot Nothing Then
                    For Each item In _listPhysicalInventoryCustody
                        Dim physicalInventoryTmp As PhysicalInventoryCustody = Nothing
                        If item.BatchSerialId IsNot Nothing Then
                            physicalInventoryTmp = listPhysicalInventoryCustodyProduct.Find(Function(x) x.BatchSerialId = item.BatchSerialId And x.ProductId = item.ProductId And x.WarehouseId = item.WarehouseId)
                        Else
                            physicalInventoryTmp = listPhysicalInventoryCustodyProduct.Find(Function(x) x.ProductId = item.ProductId And x.WarehouseId = item.WarehouseId)
                        End If

                        If physicalInventoryTmp IsNot Nothing Then
                            physicalInventoryTmp.QuantityDeliver = item.QuantityDeliver
                        End If
                    Next
                End If
                INDGcCUM.DataSource = Nothing
                INDGcCUM.DataSource = listPhysicalInventoryCustodyProduct
            Else
                IndigoGridControl1.RefreshGrid(INDGcCUM)
                INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Using
    End Sub
    Private Sub PopupCUM_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        If Custody Then
            PopupCUM_ShownPhysicalInventoryCustody()
        Else
            PopupCUM_ShownPhysicalInventory()
        End If
        'Si viene con integración a HEON se cambia el label de Medicamento y se coloca visible el tableLayout
        If DispensingIntegration = 1 Then
            LabelControl1.Text = "Producto: "
            INDlyItemInfoProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            If solicitudDetalle IsNot Nothing Then
                INDlblPosologia.Text = "Posología: " + solicitudDetalle.unidosis + " " + solicitudDetalle.unidadMedidaDosis
                INDlblDx.Text = "Dx: " + solicitudDetalle.diagnosticoCIE10 + " - " + solicitudDetalle.descripcionDiagnosticoCIE10
                INDlblPeriodicidad.Text = "Periodicidad: " + solicitudDetalle.periodicidad
                INDlblAltoCosto.Text = "Alto Costo: " + IIf(solicitudDetalle.altoCosto, "Si", "No")
                INDlblPOS.Text = "POS: " + IIf(solicitudDetalle.pos, "Si", "No")
            End If
        End If

        'Se valida que si la cantidad solicitada es cero y además viene con integración HEON me muestre el control para digitar cantidad
        If DispensingIntegration = 1 AndAlso QuantityAsignedByHEON = 0 Then
            INDlyItemLabel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemTableView.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDseRequestQuantity.EditValue = _quantityDeliver
            INDseRequestQuantity.Focus()
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Dim args As New GetCUMEventArgs

        If Custody Then
            args.ListPhysicalInventoryCustody = listPhysicalInventoryCustodyProduct.FindAll(Function(x) x.QuantityDeliver > 0)
        Else
            If INDLcgDeliveriesTypeProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Dim summary = CalculateDeliveriesSummary(listPhysicalInventoryCrystalProduct)

                Dim ListResult = New List(Of DeliveriesByPharmacyProductTypeModel)
                If summary.QuantityPT > 0 Then
                    Dim QuantityDeliverPT = If(summary.DosePS > 0, CInt(Math.Ceiling(summary.TotalWeightPT / summary.DosePS)), 0)
                    Dim ItemsPT = New DeliveriesByPharmacyProductTypeModel With {
                        .ProductType = False,
                        .Quantity = summary.QuantityPT,
                        .ConcentrationByUnit = summary.ConcentrationPT,
                        .TotalWeight = $"{summary.TotalWeightPT} {summary.DoseMeasurementPT}",
                        .QuantityDeliveryPT = QuantityDeliverPT,
                        .ProductId = summary.ProductIdPT
                    }

                    ListResult.Add(ItemsPT)
                End If

                If summary.QuantityPS > 0 Then
                    Dim ItemsPS = New DeliveriesByPharmacyProductTypeModel With {
                        .ProductType = True,
                        .Quantity = summary.QuantityPS,
                        .ConcentrationByUnit = summary.ConcentrationPS,
                        .TotalWeight = $"{summary.TotalWeightPS} {summary.DoseMeasurementPS}",
                        .ProductId = summary.ProductIdPS
                    }

                    ListResult.Add(ItemsPS)
                End If

                args.ListDeliveriesByPharmacyProductType = ListResult
            End If

            args.ListPhysicalInventory = listPhysicalInventoryCrystalProduct.FindAll(Function(x) x.QuantityDeliver > 0)
                If args.ListPhysicalInventory.Any AndAlso Me._warehouseId IsNot Nothing AndAlso Me.ValidateBatchSerialExpiredDate Then
                    Dim validateExpired As Boolean = False
                    Dim oustandingQuantityDeliver = args.ListPhysicalInventory.Sum(Function(d) d.QuantityDeliver)
                    For Each physical In listPhysicalInventoryCrystalProduct
                        Dim itemDeliver = args.ListPhysicalInventory.FirstOrDefault(Function(d) d.Id = physical.Id AndAlso d.WarehouseId = physical.WarehouseId AndAlso d.ProductId = physical.ProductId AndAlso d.BatchSerialId.Equals(physical.BatchSerialId))

                        If itemDeliver Is Nothing Then
                            'Si no se encuentra el item dentro de los productos a entregar
                            If physical.BatchSerialId IsNot Nothing Then
                                'Y este maneja lote, no se esta entregando el próximo a vencer
                                validateExpired = True
                            End If
                        Else
                            'Si se encuentra el item dentro de los productos a entregar
                            oustandingQuantityDeliver = oustandingQuantityDeliver - itemDeliver.QuantityDeliver
                            If physical.BatchSerialId IsNot Nothing Then
                                'Y este maneja lote
                                If oustandingQuantityDeliver > 0 AndAlso physical.Quantity > itemDeliver.QuantityDeliver Then
                                    'Y aún queda cantidades por entregar en el item y del total entregao, no se esta entregando el próximo a vencer
                                    validateExpired = True
                                End If
                            End If
                        End If

                        If validateExpired OrElse Not oustandingQuantityDeliver > 0 Then
                            Exit For
                        End If
                    Next

                    If validateExpired AndAlso MessageIndigo.Show("El producto seleccionado no es el proximo a vencer, ¿Desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
            End If

            If DispensingIntegration = 1 AndAlso QuantityAsignedByHEON = 0 Then
            args.RequestQuantity = INDseRequestQuantity.EditValue
        End If
        RaiseEvent GetCUM(Nothing, args)
        'End If
        Me.Close()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub PopupCUM_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            'If listPhysicalInventoryCrystalProduct.Count > 0 Then
            '    e.SuppressKeyPress = True
            'Else
            Me.Close()
            'End If
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then Exit Sub

        Dim newValue As Decimal = Convert.ToDecimal(e.NewValue)

        If Custody Then
            ' Custodia: Usa PhysicalInventoryCustody
            Dim physicalInventoryTmp = DirectCast(INDGvCUM.GetFocusedRow, PhysicalInventoryCustody)
            Dim listTmp = New List(Of PhysicalInventoryCustody)(listPhysicalInventoryCustodyProduct)
            listTmp.Remove(physicalInventoryTmp)

            ' Validación inventario
            If newValue > physicalInventoryTmp.Quantity Then
                Mensaje(EeventViewerImages.Advertencia) = $"La cantidad en el inventario del producto {physicalInventoryTmp.CodeNameProduct} es menor a la cantidad a entregar."
                e.Cancel = True
                Exit Sub
            End If

            ' Validación total
            Dim totalQuantity = listTmp.Sum(Function(x) x.QuantityDeliver) + newValue
            If totalQuantity > _quantityDeliver Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar por todos los productos supera la cantidad solicitada."
                e.Cancel = True
                Exit Sub
            End If

            ' Asignación y actualización
            physicalInventoryTmp.QuantityDeliver = newValue
        Else
            Dim physicalInventoryTmp = TryCast(INDGvCUM.GetFocusedRow, PhysicalInventory)
            Dim listTmp = New List(Of PhysicalInventory)(listPhysicalInventoryCrystalProduct)
            listTmp.Remove(physicalInventoryTmp)

            If newValue > physicalInventoryTmp.Quantity Then
                Mensaje(EeventViewerImages.Advertencia) = $"La cantidad en el inventario del producto {physicalInventoryTmp.CodeNameProduct} es menor a la cantidad a entregar."
                e.Cancel = True
                Exit Sub
            End If

            Dim summary = CalculateDeliveriesSummary(listTmp)
            Dim totalQuantity = summary.QuantityPT + summary.QuantityPS + newValue
            If totalQuantity > _quantityDeliver Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad a entregar por todos los productos supera la cantidad solicitada."
                e.Cancel = True
                Exit Sub
            End If

            ' --------- VALIDACIÓN AVANZADA DE DOSIS TRANSFORMADOS/COMERCIALES ---------
            If _DoseDeliver IsNot Nothing Then
                ' Simular cantidades ajustando solo el registro editado
                Dim cantidadPTActual = summary.QuantityPT
                Dim cantidadPSActual = summary.QuantityPS
                If physicalInventoryTmp.ClassType = EProductTypeClass.ItemProduccion Then
                    cantidadPTActual += newValue
                ElseIf physicalInventoryTmp.ClassType = EProductTypeClass.ItemMedicamento Then
                    cantidadPSActual += newValue
                End If

                Dim dosisPT = summary.DosePT
                Dim dosisPS = summary.DosePS

                Dim DoseDeliveryPT As Decimal = cantidadPTActual * dosisPT
                Dim DoseDeliveryPS As Decimal = cantidadPSActual * dosisPS

                Dim hayPT As Boolean = cantidadPTActual > 0

                If hayPT AndAlso (DoseDeliveryPT + DoseDeliveryPS) > _DoseDeliver Then
                    Mensaje(EeventViewerImages.Advertencia) = $"La suma total de dosis a entregar excede la dosis total prescrita ({_DoseDeliver}{RTrim(_DoseDeliverMeasurement)})"
                    e.Cancel = True
                    Exit Sub
                End If

                If Not hayPT AndAlso (DoseDeliveryPS > _DoseDeliver) Then
                    Mensaje(EeventViewerImages.Advertencia) = $"Advertencia: la dosis total de productos comerciales seleccionados excede la dosis prescrita."
                End If
            End If

            physicalInventoryTmp.QuantityDeliver = newValue
            Dim item = listPhysicalInventoryCrystalProduct.FirstOrDefault(Function(x) x.Id = physicalInventoryTmp.Id)
            If item IsNot Nothing Then
                item.QuantityDeliver = physicalInventoryTmp.QuantityDeliver
            End If

            SetValuesForTabDeliveries(listPhysicalInventoryCrystalProduct.Where(Function(y) y.QuantityDeliver > 0).ToList())
        End If
    End Sub

    ''' <summary>
    ''' Actualiza los datos de la tabla 'Unidades entregadas por tipo de producto'
    ''' </summary>
    ''' <param name="_listPhysicalInventory"></param>
    Private Sub SetValuesForTabDeliveries(_listPhysicalInventory As List(Of PhysicalInventory))
        If INDLcgDeliveriesTypeProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Dim summary = CalculateDeliveriesSummary(_listPhysicalInventory)

            INDSePT.Value = summary.QuantityPT
            INDConcentrationPT.Text = summary.ConcentrationPT
            INDWeightPT.Text = $"{summary.TotalWeightPT} {summary.DoseMeasurementPT}"

            INDSePS.Value = summary.QuantityPS
            INDConcentrationPS.Text = summary.ConcentrationPS
            INDWeightPS.Text = $"{summary.TotalWeightPS} {summary.DoseMeasurementPS}"

            INDSeTotalQuantity.Value = summary.QuantityPT + summary.QuantityPS
            INDWeightTotal.Text = $"{summary.TotalWeightAll} {If(summary.DoseMeasurementPT <> "", summary.DoseMeasurementPT, summary.DoseMeasurementPS)}"
        End If
    End Sub

    ''' <summary>
    ''' Calcula y resume las cantidades, dosis, concentraciones y pesos totales de productos transformados y comerciales
    ''' para un listado de inventarios físicos (PhysicalInventory o PhysicalInventoryCustody).
    ''' </summary>
    ''' <param name="list">Lista de objetos de inventario a evaluar</param>
    ''' <returns>Un objeto PharmacyDeliveriesSummary con los totales y detalles agrupados por tipo de producto</returns>
    Public Function CalculateDeliveriesSummary(list As List(Of PhysicalInventory)) As PharmacyDeliveriesSummary
        Dim ptList = list.Where(Function(x) x.ClassType = EProductTypeClass.ItemProduccion).ToList()
        Dim psList = list.Where(Function(x) x.ClassType = EProductTypeClass.ItemMedicamento).ToList()

        Dim quantityPT As Integer = ptList.Sum(Function(x) x.QuantityDeliver)
        Dim quantityPS As Integer = psList.Sum(Function(x) x.QuantityDeliver)

        Dim ProductIdPT As Integer? = ptList.Where(Function(x) x.QuantityDeliver > 0).FirstOrDefault()?.ProductId
        Dim ProductIdPS As Integer? = psList.Where(Function(x) x.QuantityDeliver > 0).FirstOrDefault()?.ProductId

        Dim dosePT As Decimal = If(ptList.Any() AndAlso ptList(0)?.Dose.HasValue, ptList(0).Dose.Value, 0D)
        Dim dosePS As Decimal = If(psList.Any() AndAlso psList(0)?.Dose.HasValue, psList(0).Dose.Value, 0D)
        Dim doseMeasurementPT As String = If(ptList.Any() AndAlso ptList(0)?.DoseMeasurement IsNot Nothing, ptList(0).DoseMeasurement, "")
        Dim doseMeasurementPS As String = If(psList.Any() AndAlso psList(0)?.DoseMeasurement IsNot Nothing, psList(0).DoseMeasurement, "")

        Dim totalWeightPT As Decimal = quantityPT * dosePT
        Dim totalWeightPS As Decimal = quantityPS * dosePS

        Return New PharmacyDeliveriesSummary With {
        .QuantityPT = quantityPT,
        .QuantityPS = quantityPS,
        .DosePT = dosePT,
        .DosePS = dosePS,
        .DoseMeasurementPT = doseMeasurementPT,
        .DoseMeasurementPS = doseMeasurementPS,
        .TotalWeightPT = totalWeightPT,
        .TotalWeightPS = totalWeightPS,
        .TotalWeightAll = totalWeightPT + totalWeightPS,
        .ConcentrationPT = If(ptList.Any(), $"{dosePT} {doseMeasurementPT}", ""),
        .ConcentrationPS = If(psList.Any(), $"{dosePS} {doseMeasurementPS}", ""),
        .ProductIdPS = ProductIdPS,
        .ProductIdPT = ProductIdPT
    }
    End Function

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cantidad solicitada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseRequestQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDseRequestQuantity.EditValueChanging
        If e IsNot Nothing Then

            'Se valida que si la cantidad digitada es menor a la cantidad entregada en la rejilla
            If Custody Then
                If listPhysicalInventoryCustodyProduct IsNot Nothing AndAlso listPhysicalInventoryCustodyProduct.Count > 0 Then
                    If e.NewValue < listPhysicalInventoryCustodyProduct.Sum(Function(x) x.QuantityDeliver) Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad solicitada no puede ser menor a la sumatoria de la cantidad entregada en la rejilla"
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            Else
                If listPhysicalInventoryCrystalProduct IsNot Nothing AndAlso listPhysicalInventoryCrystalProduct.Count > 0 Then
                    If e.NewValue < listPhysicalInventoryCrystalProduct.Sum(Function(x) x.QuantityDeliver) Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad solicitada no puede ser menor a la sumatoria de la cantidad entregada en la rejilla"
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If

            _quantityDeliver = e.NewValue
        End If
    End Sub

#End Region

#Region "CustomDrawCell"

    ''' <summary>
    ''' Evento que se encarga de pintar el color de la columna de 'vencimiento'
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCUM_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles INDGvCUM.CustomDrawCell
        If e.Column.FieldName = "BatchSerialExpiredDate" Then
            Dim view As GridView = TryCast(sender, GridView)
            Dim _expirationDate As DateTime? = view.GetRowCellValue(e.RowHandle, "BatchSerialExpiredDate")
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
                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(batchSerialRange.Color)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#End Region

End Class

''' <summary>
''' clase para retornar en el evento de seleccionar productos
''' </summary>
''' <remarks></remarks>
Public Class GetCUMEventArgs
    Inherits EventArgs

    Property ListPhysicalInventory As List(Of PhysicalInventory)

    Property ListPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)

    Property ListDeliveriesByPharmacyProductType As List(Of DeliveriesByPharmacyProductTypeModel)

    Property RequestQuantity As Integer

    Property DeliveryQuantity As Integer

    Property PendingQuantity As Integer

    Property IsDeferred As Boolean

    Property ViewDashboardPharmacyDetail As Domain.Crystal.Entities.ViewDashboardPharmacyDetail

    Property EditMode As Boolean

    Property ListDeferred As List(Of Domain.Crystal.Entities.ViewDashboardPharmacyDetailDeferred)

End Class

Public Class PharmacyDeliveriesSummary
    Public Property ProductIdPS As Integer?
    Public Property ProductIdPT As Integer?
    Public Property QuantityPT As Integer
    Public Property QuantityPS As Integer
    Public Property DosePT As Decimal
    Public Property DosePS As Decimal
    Public Property DoseMeasurementPT As String
    Public Property DoseMeasurementPS As String
    Public Property TotalWeightPT As Decimal
    Public Property TotalWeightPS As Decimal
    Public Property TotalWeightAll As Decimal
    Public Property ConcentrationPT As String
    Public Property ConcentrationPS As String
End Class