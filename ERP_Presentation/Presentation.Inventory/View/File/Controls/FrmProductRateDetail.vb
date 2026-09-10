
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP

Public Class FrmProductRateDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el datasource del producto
    ''' </summary>
    Private Property ProductDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleProduct.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProduct.Properties.DataSource = value
        End Set
    End Property

    Private Property ComponentList As List(Of ProductRateDetailPackage)
        Get
            Return INDGcComponents.DataSource
        End Get
        Set(value As List(Of ProductRateDetailPackage))
            INDGcComponents.DataSource = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Representa al formulari padre
    ''' </summary>
    Public FrmProductCoverage As FrmProductCoverage

    ''' <summary>
    ''' Representa a la entidad de detalle de tarifa de productos
    ''' </summary>
    Public ProductRateDetail As ProductRateDetail

    ''' <summary>
    ''' Permite saber si se esta editando un registro
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' lista de eliminados de los detalles del paquete seleccionado
    ''' </summary>
    Private ListDeleteProductRateDetailPackageTmp As List(Of ProductRateDetailPackage)

    Private _isEditing As Boolean = False

    ''' <summary>
    ''' Obtiene o establece el rango del producto
    ''' </summary>
    Public Property ProductRate As ProductRate

    ''' <summary>
    ''' Occurs when [save product rate].
    ''' </summary>
    Public Event SaveProductRate(ByVal sender As Object, ByVal e As AddProductRateEventArgs)

    Dim _presenter As PProductCoverage

    ''' <summary>
    ''' control de producto
    ''' </summary>
    Dim ProductControl As Boolean

    ''' <summary>
    ''' Variable para determinar si contiene impuestos
    ''' </summary>
    ''' <remarks></remarks>
    Public flagIncludeTax As Boolean

    ''' <summary>
    ''' Variable para obetener el valor del porcentaje del iva
    ''' </summary>
    ''' <remarks></remarks>
    Public percentage As Decimal

    ''' <summary>
    ''' Variable para obtener la informacion de el formato de numero
    ''' </summary>
    Private FormatNumber As Globalization.NumberFormatInfo

    Private Property PackageId As Integer
        Get
            Return INDSlePackage.EditValue
        End Get
        Set(value As Integer)
            INDSlePackage.EditValue = value
        End Set
    End Property

    Private Property RateClass As Byte
        Get
            Return INDGleRateClass.EditValue
        End Get
        Set(value As Byte)
            INDGleRateClass.EditValue = value
        End Set
    End Property

    Private Property DoseType As Byte
        Get
            Return INDGleDoseType.EditValue
        End Get
        Set(value As Byte)
            INDGleDoseType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de redondeo de la moneda parametrizada
    ''' </summary>
    Private _roundingType As Integer

#End Region

#Region "DataSource"

    Private _liquidationType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property LiquidationType As List(Of Tuple(Of Byte, String))
        Get
            If _liquidationType Is Nothing Then
                _liquidationType = New List(Of Tuple(Of Byte, String))
                _liquidationType.Add(New Tuple(Of Byte, String)(1, "Tarifa"))
                _liquidationType.Add(New Tuple(Of Byte, String)(2, "Servicio"))
                _liquidationType.Add(New Tuple(Of Byte, String)(3, "Tarifa - Servicio"))
            End If
            Return _liquidationType
        End Get
    End Property

    Private _rateType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property RateType As List(Of Tuple(Of Byte, String))
        Get
            If _rateType Is Nothing Then
                _rateType = New List(Of Tuple(Of Byte, String))
                _rateType.Add(New Tuple(Of Byte, String)(1, "Tarifa Fija"))
                _rateType.Add(New Tuple(Of Byte, String)(2, "Porcentaje"))
            End If
            Return _rateType
        End Get
    End Property

    Private _percentageType As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property PercentageType As List(Of Tuple(Of Byte, String))
        Get
            If _percentageType Is Nothing Then
                _percentageType = New List(Of Tuple(Of Byte, String))
                _percentageType.Add(New Tuple(Of Byte, String)(1, "Costo Promedio Ponderado"))
                _percentageType.Add(New Tuple(Of Byte, String)(2, "Ultimo Costo"))
            End If
            Return _percentageType
        End Get
    End Property

    Private _rateClasses As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property RateClasses As List(Of Tuple(Of Byte, String))
        Get
            If _rateClasses Is Nothing Then
                _rateClasses = New List(Of Tuple(Of Byte, String))
                _rateClasses.Add(New Tuple(Of Byte, String)(1, "Producto"))
                _rateClasses.Add(New Tuple(Of Byte, String)(2, "Paquete"))
            End If
            Return _rateClasses
        End Get
    End Property

    Private _doseTypes As List(Of Tuple(Of Byte, String))
    Private ReadOnly Property DoseTypes As List(Of Tuple(Of Byte, String))
        Get
            If _doseTypes Is Nothing Then
                _doseTypes = New List(Of Tuple(Of Byte, String))
                _doseTypes.Add(New Tuple(Of Byte, String)(2, "Estándar"))
                _doseTypes.Add(New Tuple(Of Byte, String)(3, "Personalizada"))
            End If

            Return _doseTypes
        End Get
    End Property
#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmProductRateDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductRateDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PProductCoverage(Nothing, False)
        InitializeTuples()

        If EditMode Then
            LoadControls()
        Else
            CleanControls()
        End If
        Me.indigo = SessionValues.Instance 'obtenemos la varibale de seccion
        FormatNumber = Me.indigo.CurrencyNumbertFormat
        GetCurrencyByCompanySettings(FormatNumber)
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddProduct()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the SearchLookUpEdit1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmProducts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                InitializeProducts()
            End Using
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the FrmProductRateDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductRateDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private oldFilter As String

    Private Sub INDRiProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRiProduct.QueryPopUp
        Dim row = INDGvComponents.GetFocusedObject(Of ProductRateDetailPackage)
        Dim filter As String = "ProductTypeId.Class <> 5"

        If row.ItemType = 1 Then
            filter &= $" And ATCId.Id = {row.ItemId}"
        ElseIf row.ItemType = 2 Then
            filter &= $" And SupplieId.Id = {row.ItemId}"
        ElseIf row.ItemType = 3 Then
            filter &= $" And Id = {row.ItemId}"
        End If

        If INDRiProduct.DataSource Is Nothing OrElse oldFilter <> filter Then
            INDRiProduct.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of InventoryProductXpo)(filter)
            oldFilter = filter
        End If
    End Sub

    ''' <summary>
    ''' Se cargan los datos de los paquetes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePackage_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePackage.QueryPopUp
        If INDSlePackage.Properties.DataSource Is Nothing Then
            INDSlePackage.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of MixinStationPackageXpo)()
        End If
    End Sub
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If ProductDatasource Is Nothing Then
            InitializeProducts()
        End If
    End Sub

    ''' <summary>
    ''' QueryPopUp para cargar datasource de Cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCUPS.QueryPopUp
        If INDsleCUPS.Properties.DataSource Is Nothing Then
            Using model As New MBusqueda
                INDsleCUPS.Properties.DataSource = model.ConsultarEntidades(eDataSource.ListCupsEntityByStatus, True)
            End Using
        End If
    End Sub
#End Region

#Region "Validated"

    ''' <summary>
    ''' Redondea el valor del precio de venta  despues de ser validado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtSalesValue_Validated(sender As Object, e As EventArgs) Handles INDtxtSalesValue.Validated
        INDtxtSalesValue.EditValue = RoundValue(sender.EditValue, _roundingType)
    End Sub

    ''' <summary>
    ''' Redondea el valor del precio con recargo  despues de ser validado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtPriceWithSurcharge_Validated(sender As Object, e As EventArgs) Handles INDtxtPriceWithSurcharge.Validated
        INDtxtPriceWithSurcharge.EditValue = RoundValue(sender.EditValue, _roundingType)
    End Sub

#End Region

#Region "EditValueChanged"
    Private Sub INDSlePackage_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePackage.EditValueChanged
        LoadPackageDetails()
    End Sub

    Private Sub INDGleRateClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRateClass.EditValueChanged
        EnableControlsRateClass()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeInitialDate.EditValueChanged
        If INDdeInitialDate.EditValue IsNot Nothing Then
            INDdeFinalDate.Properties.MinValue = INDdeInitialDate.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Acciones para ocultar elemento o mostrar cuando se cambia el tipo de liquidacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleLiquidationType.EditValueChanged
        If String.IsNullOrEmpty(INDGleLiquidationType.EditValue) OrElse INDGleLiquidationType.EditValue = 0 Then
            HideItems()
            Exit Sub
        End If

        If INDGleLiquidationType.EditValue <> 2 Then
            INDLciIRateType.ShowLayout()
            If INDGleLiquidationType.EditValue = 3 Then
                INDliCUPS.ShowLayout()
                Exit Sub
            End If
            INDliCUPS.HideLayout()
            INDsleCUPS.EditValue = Nothing
            INDsleRelatedDescription.EditValue = Nothing
            Exit Sub
        End If

        INDLciIRateType.HideLayout()
        INDliCUPS.ShowLayout()
        INDGleRateType.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Acciones para ocultar elemento o mostrar cuando se cambia el tipo de tarifa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDGleRateType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRateType.EditValueChanged
        If String.IsNullOrEmpty(INDGleRateType.EditValue) OrElse INDGleRateType.EditValue = 0 Then
            ShowOrHidePercentage(DevExpress.XtraLayout.Utils.LayoutVisibility.Never, True)
            ShowOrHideSalesValue(DevExpress.XtraLayout.Utils.LayoutVisibility.Never, True)
            HideItems()
            Exit Sub
        End If

        If INDGleRateType.EditValue = 1 Then
            ShowOrHideSalesValue(DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            ShowOrHidePercentage(DevExpress.XtraLayout.Utils.LayoutVisibility.Never, True)
            HideItems()
            Using model As New MInventoryProduct(Me.Tag)
                Dim resultProduct = Await model.GetProductByIdXpo(INDsleProduct.EditValue)
                If resultProduct IsNot Nothing Then
                    If resultProduct.TaxedProduct Then
                        If Me.flagIncludeTax Then
                            INDliIvaValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDliSubtotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Else
                            INDliIvaValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDliTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        End If
                    End If
                End If
            End Using
        Else
            ShowOrHideSalesValue(DevExpress.XtraLayout.Utils.LayoutVisibility.Never, True)
            ShowOrHidePercentage(DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        End If
    End Sub

    ''' <summary>
    ''' Acciones para ocultar elemento o mostrar cuando se cambia el CUPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCUPS.EditValueChanged
        If INDsleCUPS.EditValue Is Nothing Then
            INDliCUPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            HideContractDescription()
            Exit Sub
        End If
        Dim ContractDescription = _presenter.InitializeContractDescription(INDsleCUPS.EditValue).ToList()

        If ContractDescription Is Nothing OrElse ContractDescription.Count = 0 Then
            HideContractDescription()
            Exit Sub
        End If

        INDsleRelatedDescription.Properties.DataSource = ContractDescription
        INDliRelatedDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    Private Sub INDGleDoseType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDoseType.EditValueChanged
        EnableControlsDoseType()
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form por primera vez
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductRateDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDGleRateClass.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Funcion para mostrar u ocultar controles valor de venta y recargo
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="EditValue"></param>
    Private Sub ShowOrHideSalesValue(value As DevExpress.XtraLayout.Utils.LayoutVisibility?, Optional EditValue As Boolean = False)
        If value IsNot Nothing Then
            INDliSalesValue.Visibility = value
            INDliSalesValueWithSurcharge.Visibility = value
        End If
        If EditValue Then
            INDtxtSalesValue.EditValue = Nothing
            INDtxtPriceWithSurcharge.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' funcion para mostrar u ocultar controles de porcentaje
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="EditValue"></param>
    Private Sub ShowOrHidePercentage(value As DevExpress.XtraLayout.Utils.LayoutVisibility?, Optional EditValue As Boolean = False)
        If value IsNot Nothing Then
            INDLciIPercentageType.Visibility = value
            INDliPercentage.Visibility = value
        End If
        If EditValue Then
            INDGlePercentageType.EditValue = Nothing
            INDtxtPercentage.EditValue = Nothing
        End If
    End Sub

    Private Sub HideContractDescription()
        INDliRelatedDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleRelatedDescription.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Carga los controles cuando se esta editando
    ''' </summary>
    Private Sub LoadControls()
        If EditMode Then
            INDbtnAdd.Text = ResourceManager.GetString("Edit")
        End If

        _isEditing = True
        With ProductRateDetail
            INDGleRateClass.EditValue = .RateClass
            INDGleRateClass.ReadOnly = True
            INDSlePackage.EditValue = .PackageId
            INDSlePackage.Properties.NullText = $"{ .PackageCode} - { .PackageName}"
            INDSlePackage.ReadOnly = True
            INDGleDoseType.EditValue = .DoseType
            INDsleProduct.EditValue = .ProductId
            INDsleProduct.Properties.NullText = .ProductCode + " - " + .ProductName
            INDsleProduct.ReadOnly = True
            INDGleLiquidationType.EditValue = .LiquidationType
            INDGleRateType.EditValue = .RateType
            INDGlePercentageType.EditValue = .PercentageBasedOn
            INDtxtPercentage.EditValue = .Percentage
            INDsleCUPS.EditValue = .CupsId
            INDsleCUPS.Properties.NullText = .CupsCodeName
            INDsleRelatedDescription.EditValue = .ContractDescriptionId
            INDsleRelatedDescription.Properties.NullText = .ContractDesCodeName
            INDdeInitialDate.EditValue = .InitialDate
            INDdeFinalDate.EditValue = .EndDate
            INDtxtSalesValue.EditValue = .SalesValue
            INDtxtPriceWithSurcharge.EditValue = .SalesValueWithSurcharge
            INDsleContracted.EditValue = .Contracted
            INDsleQuoted.EditValue = .Quoted
            INDmemoObservations.EditValue = .Observations
            ComponentList = .ProductRateDetailPackage.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
        End With
        _isEditing = False
    End Sub

    ''' <summary>
    ''' Inicializa los controles con valores quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim ListYesNot As New List(Of Tuple(Of Boolean, String))
        ListYesNot = New List(Of Tuple(Of Boolean, String))
        ListYesNot.Add(New Tuple(Of Boolean, String)(True, "Sí"))
        ListYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))

        INDsleContracted.Properties.DataSource = ListYesNot.ToList
        INDsleQuoted.Properties.DataSource = ListYesNot.ToList
        INDGleLiquidationType.Properties.DataSource = LiquidationType
        INDGleRateType.Properties.DataSource = RateType
        INDGlePercentageType.Properties.DataSource = PercentageType
        INDGleRateClass.Properties.DataSource = RateClasses
        INDGleDoseType.Properties.DataSource = DoseTypes

        INDsleContracted.EditValue = True
        INDsleQuoted.EditValue = False
    End Sub

    ''' <summary>
    ''' Valida que el producto no exista en el rango de fechas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateProductWithDates(_initialDate As DateTime, _endDate As DateTime, _productId As Integer?, _packageId As Integer?, listCompare As List(Of ProductRateDetail)) As Boolean
        Return listCompare.Any(Function(item) (
                (_initialDate >= item.InitialDate AndAlso _initialDate <= item.EndDate) _
                OrElse (_endDate >= item.InitialDate AndAlso _endDate <= item.EndDate) _
                OrElse (_initialDate < item.InitialDate) AndAlso (_endDate > item.EndDate)
            ) _
            AndAlso
            (
                Not _productId.HasValue OrElse (item.ProductId.HasValue AndAlso _productId.Value = item.ProductId.Value)
            ) _
            AndAlso (
                Not _packageId.HasValue OrElse (item.PackageId.HasValue AndAlso _packageId.Value = item.PackageId.Value)
            )
        )
    End Function

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
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

    ''' <summary>
    ''' Initializes the products.
    ''' </summary>
    Private Sub InitializeProducts()
        Dim criteria = "ProductTypeId.Class <> 1 And ProductTypeId.Class <> 5"
        ProductDatasource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of InventoryProductXpo)(criteria)
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDsleProduct.EditValue = Nothing
        INDsleProduct.Properties.NullText = String.Empty
        INDdeInitialDate.EditValue = Nothing
        INDdeFinalDate.EditValue = Nothing
        INDtxtSalesValue.EditValue = Nothing
        INDtxtPriceWithSurcharge.EditValue = Nothing
        INDsleContracted.EditValue = Nothing
        INDsleQuoted.EditValue = Nothing
        INDmemoObservations.EditValue = Nothing
        _liquidationType = Nothing
        _rateType = Nothing
        _percentageType = Nothing
        INDGleLiquidationType.EditValue = Nothing
        INDsleContracted.EditValue = True
        INDsleQuoted.EditValue = False
        INDGleRateType.EditValue = Nothing
        INDsleRelatedDescription.EditValue = Nothing
        INDsleCUPS.EditValue = Nothing
        INDSlePackage.EditValue = Nothing
        INDGleDoseType.EditValue = Nothing
        INDGleRateClass.EditValue = Nothing
        INDtxtIvaValue.EditValue = Nothing
        INDtxtIvaValue2.EditValue = Nothing
        INDtxtSubtotalValue.EditValue = Nothing
        INDtxtITotalValue.EditValue = Nothing
        HideItems()
        INDGleRateClass.EditValue = CByte(1)
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForms() As String
        Dim errorList As New StringBuilder()

        If CByte(INDGleRateClass.EditValue) = 1 AndAlso (INDsleProduct.EditValue Is Nothing OrElse INDsleProduct.EditValue = 0) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Producto"))
        End If

        If CByte(INDGleRateClass.EditValue) = 2 AndAlso (INDSlePackage.EditValue Is Nothing OrElse INDSlePackage.EditValue = 0) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Paquete"))
        End If

        If INDdeInitialDate.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha Inicial"))
        End If
        If INDdeFinalDate.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha Final"))
        End If

        If INDGleRateType.EditValue IsNot Nothing AndAlso INDGleRateType.EditValue = 1 Then
            If INDtxtSalesValue.EditValue Is Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Precio de Venta"))
            End If
            If INDtxtPriceWithSurcharge.EditValue Is Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Precio con Recargo"))
            End If
        End If

        If INDGleRateType.EditValue IsNot Nothing AndAlso INDGleRateType.EditValue = 2 Then
            If INDGlePercentageType.EditValue Is Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Tipo de Porcentaje"))
            End If
            If INDtxtPercentage.EditValue Is Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Porcentaje"))
            End If
        End If

        If {2, 3}.Contains(INDGleLiquidationType.EditValue) Then
            If INDsleCUPS.EditValue Is Nothing Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "CUPS"))
            End If
        End If

        If INDdeInitialDate.EditValue > INDdeFinalDate.EditValue Then
            errorList.AppendLine("La fecha inicial debe ser menor a la fecha final")
        End If

        If INDGleRateType.EditValue = 1 Then

            If INDtxtSalesValue.EditValue <= 0 Then
                errorList.AppendLine("El valor del campo Precio de Venta debe ser mayor a cero y no permite valores negativos")
            End If

            If INDtxtPriceWithSurcharge.EditValue < 0 Then
                errorList.AppendLine("El valor del campo Precio con Recargo no permite valores negativos")
            End If
        End If

        Return errorList.ToString()
    End Function

    ''' <summary>
    ''' Metodo que agrega el producto al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddProduct()
        'Se valida los controles del form
        Dim errors = ValidateControlsForms()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

		If Not EditMode Then
			Dim listValidate As List(Of ProductRateDetail) = New List(Of ProductRateDetail)
			If FrmProductCoverage?.ListProductTemplate?.Any() Then
				listValidate = FrmProductCoverage.ListProductTemplate
				listValidate = (From x In listValidate Where Not x.Equals(ProductRateDetail) Select x).ToList()
			End If

			If listValidate IsNot Nothing AndAlso listValidate.Any() Then
				Dim resultValidate = ValidateProductWithDates(INDdeInitialDate.EditValue, INDdeFinalDate.EditValue, INDsleProduct.EditValue, INDSlePackage.EditValue, listValidate)
				If resultValidate Then
					Mensaje(EeventViewerImages.Advertencia) = "El producto " + INDsleProduct.Text + " ya existe en el listado con las fechas " + INDdeInitialDate.EditValue.ToString + " y " + INDdeFinalDate.EditValue.ToString
					Exit Sub
				End If
			End If
		End If

		If Not ValidateComponents() Then
            Return
        End If

        If Not EditMode Then
            ProductRateDetail = New ProductRateDetail
        Else
            If ProductRateDetail.Id > 0 Then
                ProductRateDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End If

        With ProductRateDetail
            .RateClass = RateClass
            If RateClass = 1 Then
                Dim Product = _presenter.ProductbyId(INDsleProduct.EditValue)
                .RateClassName = "Producto"
                .ProductId = INDsleProduct.EditValue
                .ProductCodeName = INDsleProduct.Text
                .ProductName = Product(0).Name
                If Product IsNot Nothing Then
                    .ProductControl = Product(0).ProductControl
                    .ProductWithPriceControl = Product(0).ProductWithPriceControl
                End If
                .ProductCode = If(EditMode AndAlso Not String.IsNullOrEmpty(.ProductCode), .ProductCode, Product(0).Code)

            Else
                .RateClassName = "Paquete"
                .PackageId = PackageId
                .ProductName = INDSlePackage.Text
                .DoseType = DoseType
                .PackageCode = INDSlePackage.Text.Split("-")(0).Trim()
                .PackageName = INDSlePackage.Text.Split("-")(1).Trim()

                If Not EditMode Then
                    For Each c In ComponentList
                        ProductRateDetail.ProductRateDetailPackage.Add(c)
                    Next
                End If
            End If

            .LiquidationType = INDGleLiquidationType.EditValue
            .RateType = IIf(INDGleRateType.EditValue Is Nothing, 0, INDGleRateType.EditValue)
            .PercentageBasedOn = IIf(INDGlePercentageType.EditValue Is Nothing, 0, INDGlePercentageType.EditValue)
            .Percentage = INDtxtPercentage.EditValue
            .CupsId = INDsleCUPS.EditValue
            .CupsCodeName = INDsleCUPS.Text
            .ContractDescriptionId = INDsleRelatedDescription.EditValue
            .ContractDesCodeName = INDsleRelatedDescription.Text
            .Status = 1
            .InitialDate = INDdeInitialDate.EditValue
            .EndDate = INDdeFinalDate.EditValue
            .SalesValue = INDtxtSalesValue.EditValue
            .SalesValueWithSurcharge = INDtxtPriceWithSurcharge.EditValue
            .Contracted = INDsleContracted.EditValue
            .Quoted = INDsleQuoted.EditValue
            .Observations = INDmemoObservations.EditValue
        End With

        Dim e As New AddProductRateEventArgs()
        e.ProductRateDetail = ProductRateDetail
        e.EditMode = EditMode
        RaiseEvent SaveProductRate(Nothing, e)
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString(IIf(EditMode, "ProductRangeEdit", "ProductRangeAdd"), "Inventory")
        CleanControls()
        INDGleRateClass.Focus()

        If EditMode Then
            Me.Close()
        End If
    End Sub

    Private Function ValidateComponents() As Boolean
        If INDGleRateClass.EditValue = 2 AndAlso INDGleDoseType.EditValue = 2 Then
            Dim items As List(Of ProductRateDetailPackage) = ComponentList

            If items Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró detalle del paquete"
                Return False
            End If

            Dim sb As New StringBuilder()

            For Each i In items
                If i.ProductId <= 0 Then
                    sb.AppendLine($"El ítem ({i.ItemCodeName}) no tiene parametrizado el CUM de referencia")
                    Continue For
                End If
            Next

            If sb.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = sb.ToString()
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Habilita o deshabilita los controles
    ''' </summary>
    Private Sub EnableControlsRateClass()
        If INDGleRateClass.EditValue Is Nothing Then Return

        Dim rateClass As Byte = INDGleRateClass.EditValue

        If rateClass = 1 Then
            PrepareControlsProduct()
        ElseIf rateClass = 2 Then
            PrepareControlsPackage()
        End If
    End Sub

    ''' <summary>
    ''' Muestra u oculta los controles para la configuracion de paquetes
    ''' </summary>
    Private Sub PrepareControlsPackage()
        INDLciPakcage.ShowLayout()
        INDLciDoseType.ShowLayout()
        INDliProduct.HideLayout()
        INDLciILiquidationType.HideLayout()
        INDLciIRateType.HideLayout()

        INDGleLiquidationType.EditValue = Nothing
        INDsleProduct.EditValue = Nothing
        INDGleDoseType.EditValue = CByte(2) 'Nothing
        INDGleRateType.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Muestra u oculta los controles para la configuracion de productos
    ''' </summary>
    Private Sub PrepareControlsProduct()
        INDliProduct.ShowLayout()
        INDLciILiquidationType.HideLayout()
        INDLciPakcage.HideLayout()
        INDLciDoseType.HideLayout()

        INDSlePackage.EditValue = Nothing
        INDGleLiquidationType.EditValue = CByte(1)
        INDGleDoseType.EditValue = CByte(1)
    End Sub

    Private Sub EnableControlsDoseType()
        Dim doseType As Byte? = INDGleDoseType.EditValue

        If doseType = 2 Then
            INDLcgComponents.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciILiquidationType.HideLayout()
            INDLciIRateType.HideLayout()
            INDLciIPercentageType.HideLayout()

            INDGleLiquidationType.EditValue = Nothing
            INDGleRateType.EditValue = Nothing
            LoadPackageDetails()
        ElseIf doseType = 3 Then
            INDLciILiquidationType.ShowLayout()
            INDLciIRateType.ShowLayout()
            INDLcgComponents.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ComponentList = Nothing
        Else
            INDLcgComponents.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ComponentList = Nothing
        End If
    End Sub

    Private lastPackageId As Integer

    Private Async Function LoadPackageDetails() As Task
        If _isEditing OrElse Not PackageId > 0 Then Return

        If lastPackageId <> PackageId AndAlso (INDSlePackage.EditValue Is Nothing OrElse INDGleDoseType.EditValue <> 2) Then Return

        lastPackageId = PackageId
        INDGvComponents.ShowLoadingPanel()
        Using model As New MPackage(Tag)
            Dim packageDetails = Await model.GetProductRateDetailPackageByPackageIdAsync(PackageId)
            ComponentList = packageDetails
        End Using
        INDGvComponents.HideLoadingPanel()
    End Function

    Private Sub INDGvComponents_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvComponents.CustomDrawCell
        Dim row As ProductRateDetailPackage = INDGvComponents.GetRow(e.RowHandle)
        If row Is Nothing Then Return

        If e.Column.Name = INDColReferenceCUM.Name Then
            If row.ProductId = 0 Then
                e.DisplayText = ""
            Else
                e.DisplayText = row.ProductCodeName
            End If
        ElseIf e.Column.Name = INDColQuantity.Name Then
            If row.MainMedicine Then
                e.DisplayText = String.Empty
            End If
        ElseIf e.Column.Name = INDColDoseNumber.Name Then
            If row.MainMedicine Then
                e.DisplayText = String.Empty
            End If
        End If
    End Sub

    Private Sub INDRiProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiProduct.EditValueChanged
        Dim product = DirectCast(sender, DevExpress.XtraEditors.SearchLookUpEdit).Properties.View.GetFocusedObject(Of InventoryProductXpo)()
        Dim row = INDGvComponents.GetFocusedObject(Of ProductRateDetailPackage)()

        If row IsNot Nothing Then
            If product IsNot Nothing Then
                row.ProductCodeName = product.CodeName
            Else
                row.ProductCodeName = ""
            End If
        End If
    End Sub

    Private Sub INDGvComponents_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDGvComponents.ShowingEditor
        Dim row = INDGvComponents.GetFocusedObject(Of ProductRateDetailPackage)()

        If row IsNot Nothing AndAlso (INDGvComponents.FocusedColumn.Name = INDColDoseNumber.Name OrElse INDGvComponents.FocusedColumn.Name = INDColQuantity.Name) AndAlso row.MainMedicine Then
            e.Cancel = True
        End If
    End Sub

    Private Async Sub INDsleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProduct.EditValueChanged
        If INDsleProduct.EditValue IsNot Nothing Then
            Using model As New MInventoryProduct(Me.Tag)
                Dim resultProduct = Await model.GetProductByIdXpo(INDsleProduct.EditValue)

                If resultProduct.TaxedProduct Then
                    percentage = resultProduct?.IVAId?.Percentage
                End If

                If resultProduct IsNot Nothing Then
                    If Not resultProduct.TaxedProduct Then
                        HideItems()
                    Else
                        If Me.flagIncludeTax Then
                            INDliIvaValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDliSubtotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Else
                            INDliIvaValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDliTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        End If
                        CalculateTotals()
                    End If
                End If
            End Using
        End If

    End Sub

    Private Sub HideItems()
        INDliIvaValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliIvaValue2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliSubtotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Sub INDtxtSalesValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtSalesValue.EditValueChanged
        CalculateTotals()
    End Sub


    Private Sub CalculateTotals()
        If flagIncludeTax Then
            INDtxtSubtotalValue.EditValue = (INDtxtSalesValue.EditValue / ((percentage / 100.0F) + 1))
            INDtxtIvaValue.EditValue = If(INDtxtSubtotalValue.EditValue = 0, 0, (INDtxtSalesValue.EditValue - INDtxtSubtotalValue.EditValue))
        Else
            INDtxtIvaValue2.EditValue = (INDtxtSalesValue.EditValue * (percentage / 100))
            INDtxtITotalValue.EditValue = (INDtxtSalesValue.EditValue + INDtxtIvaValue2.EditValue)
        End If
    End Sub

    ''' <summary>
    '''Configura las propiedades de un campo numérico dependiendo de la moneda  de la compania segun el tipo de redondeo
    ''' </summary>
    Private Sub GetCurrencyByCompanySettings(currencyNumbertFormat As Globalization.NumberFormatInfo)
        Dim companySettings = _presenter.GetOfficialCurrencyFromCompanySettings()
        If companySettings IsNot Nothing Then
            Dim round = companySettings?.OfficialCurrency?.RoundingType
            Dim objMaskDisplay As Object = Nothing
            If round IsNot Nothing Then
                _roundingType = CInt(round)
                currencyNumbertFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundingType)
                If _roundingType <> ECurrencyRoundingType.OneDecimal AndAlso _roundingType <> ECurrencyRoundingType.TwoDecimals Then
                    objMaskDisplay = New With {.useMaskAsDisplayFormat = False, .decimals = 2}
                End If
                Me.changeNumericFormatByCurrency(currencyNumbertFormat, Nothing, objMaskDisplay)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Redondea el valor por el tipo de redondeo
    ''' </summary>
    ''' <param name="value"></param>
    ''' <param name="RoundingType"></param>
    ''' <returns></returns>
    Function RoundValue(value As Decimal, RoundingType As Integer) As Decimal
        Return Utils.RoundValueByTypeCurrency(value, RoundingType)
    End Function
#End Region

End Class