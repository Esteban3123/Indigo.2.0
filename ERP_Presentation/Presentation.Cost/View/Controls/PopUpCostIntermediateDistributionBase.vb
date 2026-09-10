Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CostRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Cost.MVP

Public Class PopUpCostIntermediateDistributionBase

#Region "Variables and Properties"

#Region "Variables"

    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PCostGeneralExpenses

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
    ''' Variable para conocer que frontal lo está instanciando
    ''' </summary>
    Property FormParentName As eParent

    ''' <summary>
    ''' Permite identificar si se esta editando un registro
    ''' </summary>
    Private _editMode As Boolean

    ''' <summary>
    ''' Permite identificar si se esta editando un registro
    ''' </summary>
    Property EditMode As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' Parametrizacion de la moneda 
    ''' </summary>
    Private _currencyAbbreviation As String
    Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que indica la cantidad de detalles
    ''' </summary>
    Private _countDistributionBase As Integer

    ''' <summary>
    ''' Cantidad de detalles
    ''' </summary>
    Property CountDistributionBase As Integer
        Get
            Return _countDistributionBase
        End Get
        Set(value As Integer)
            _countDistributionBase = value
        End Set
    End Property

    ''' <summary>
    ''' Permite identificar si el formulario esta cargandose
    ''' </summary>
    Private _isLoad As Boolean

    ''' <summary>
    ''' variable de distributionBase
    ''' </summary>
    Private _distributionBase As CostDistributionBase

    ''' <summary>
    ''' Obtiene o establece el listado de centros de produccion (Solo se modifican si son tipo A)
    ''' </summary>
    Property ListDistributionBaseDetail As List(Of CostDistributionBaseDetail)
        Get
            Return CType(INDgcProductionCenter.DataSource, List(Of CostDistributionBaseDetail))
        End Get
        Set(value As List(Of CostDistributionBaseDetail))
            INDgcProductionCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' listado de las unidades de medida
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDistributionBaseMeasurementUnit As List(Of CostDistributionBaseMeasurementUnit)

    ''' <summary>
    ''' listado de las unidades de medida
    ''' </summary>
    ''' <remarks></remarks>
    Public WriteOnly Property ListDistributionBaseMeasurementUnit As List(Of CostDistributionBaseMeasurementUnit)
        Set(value As List(Of CostDistributionBaseMeasurementUnit))
            _listDistributionBaseMeasurementUnit = value
        End Set
    End Property

    ''' <summary>
    ''' distribucion secundaria
    ''' </summary>
    Private _distributionSecondaryBase As CostDistributionSecondaryBase

    ''' <summary>
    ''' Obtiene o establece el listado de centros de produccion (Solo se modifican si son tipo A)
    ''' </summary>
    Property ListDistributionSecondaryBaseDetail As List(Of CostDistributionSecondaryBaseDetail)
        Get
            Return CType(INDgcProductionCenter.DataSource, List(Of CostDistributionSecondaryBaseDetail))
        End Get
        Set(ByVal value As List(Of CostDistributionSecondaryBaseDetail))
            INDgcProductionCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' listado de las unidades de medida (Distribución secundaria)
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDistributionSecondaryMeasurementUnit As List(Of CostDistributionSecondaryMeasurementUnit)

    ''' <summary>
    ''' listado de las unidades de medida (Distribución secundaria)
    ''' </summary>
    ''' <remarks></remarks>
    Public WriteOnly Property ListDistributionSecondaryMeasurementUnit As List(Of CostDistributionSecondaryMeasurementUnit)
        Set(value As List(Of CostDistributionSecondaryMeasurementUnit))
            _listDistributionSecondaryMeasurementUnit = value
        End Set
    End Property

#End Region

#Region "Properties Entity"

    ''' <summary>
    ''' Obtiene o establece los tipos de distribución
    ''' </summary>
    Public Property DistributionType As Byte
        Get
            Return CType(INDgleDistributionType.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleDistributionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the impact points.
    ''' </summary>
    Property ImpactPoints As Byte
        Get
            Return CType(INDspnIncidencePoint.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDspnIncidencePoint.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the description.
    ''' </summary>
    Property Description As String
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the measurement unit.
    ''' </summary>
    Property MeasurementUnit As Byte
        Get
            Return CType(INDgleMeasureUnit.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDgleMeasureUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Ventas
    ''' </summary>
    ''' <returns></returns>
    Property Sales As Boolean
        Get
            Return INDRgCosteoOptions.EditValue = CByte(1)
        End Get
        Set(value As Boolean)
            If value Then
                INDRgCosteoOptions.EditValue = CByte(1)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [invoice value].
    ''' </summary>
    Property QuantitiesProduced As Boolean
        Get
            Return INDRgCosteoOptions.EditValue = CByte(2)
        End Get
        Set(value As Boolean)
            If value Then
                INDRgCosteoOptions.EditValue = CByte(2)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la base multiple seleccioanda en el frontal de gastos generales
    ''' </summary>
    Property MultipleBase As Byte

#End Region

#Region "Events"

    ''' <summary>
    ''' Occurs when [add distribution base].
    ''' </summary>
    Public Event AddDistributionBase(ByVal edit As Boolean, ByVal distributionBase As CostDistributionBase)

    ''' <summary>
    ''' Occurs when [add distribution base secondary].
    ''' </summary>
    Public Event AddDistributionBaseSecondary(ByVal edit As Boolean, ByVal distributionBase As CostDistributionSecondaryBase)

#End Region

#Region "Datasource Entity"

#Region "Tuples"

    ''' <summary>
    ''' tipos de distribucion
    ''' </summary>
    Private _distributionType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' unidad de medida
    ''' </summary>
    Private _measureUnit As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' The _distribution option
    ''' </summary>
    Private _distributionOption As List(Of Tuple(Of Integer, String))

#End Region

    Private listProductionCenters As XPCollection(Of CostProductionCenterXpo)

#End Region

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the PopUpDistributionBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PopUpDistributionBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PCostGeneralExpenses
        'IndigoGridControl1.RefreshGrid(INDgcProductionCenter)
        If CountDistributionBase < 2 Then
            Dim _listActions As New List(Of eAcciones)()
            _listActions.Add(eAcciones.Remove)
            IndigoGridView1.SetListAcction(INDgvProductionCenterList, _listActions)
            INDgvProductionCenterList.Columns.ColumnByName("colActions").Width = 70

            IndigoGridView2.SetListAcction(INDGvCostDistributionBaseMeasurementUnit, _listActions)
            INDGvCostDistributionBaseMeasurementUnit.Columns.ColumnByName("colActions").Width = 70
        End If

        CreateDistributionType()
        CreateMeasureUnit()
        CreateDistributionOptions()
        SetInitialValues()

    End Sub

    Private Sub PopUpDistributionBase_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If INDgleDistributionType.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the PopupContainerEdit1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddProductionCenter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddProductionCenter.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceAddProductionCenter.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the PopUpDistributionBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub PopUpDistributionBase_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleProductionCenter.QueryPopUp
        If INDsleProductionCenter.Properties.DataSource Is Nothing Then
            InitializeProductionCenter()
        End If
    End Sub

    Private Sub INDsleCancellationCostMainAccountId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountId.QueryPopUp
        If INDsleAccountId.Properties.DataSource Is Nothing Then
            INDsleAccountId.Properties.DataSource = Presenter.ListAccounts()
        End If
    End Sub

    Private Sub INDSLeCostCenterConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSLeCostCenterConcept.QueryPopUp
        If INDSLeCostCenterConcept.Properties.DataSource Is Nothing Then
            Dim ListCostCenter = Presenter.ListProductionCenterCostCenterByProductionCenterId(INDsleProductionCenter.EditValue)
            Dim listId = (From i In ListCostCenter Select i.CostCenterId.Id).Distinct().ToList()
            INDSLeCostCenterConcept.Properties.DataSource = Presenter.ListCostCenterDinamicByListId(listId)
        End If
    End Sub

    Private Sub INDsleMeasureUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleMeasureUnit.QueryPopUp
        If INDsleMeasureUnit.Properties.DataSource Is Nothing Then
            INDsleMeasureUnit.Properties.DataSource = Presenter.ListMeasureUnitByType()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleDistributionType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleDistributionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleDistributionType.EditValueChanged
        If DistributionType <> 0 Then
            MeasurementUnit = Nothing
            CleanControls()
            Select Case DistributionType
                Case eDistributionType.Direct
                    MeasurementUnit = 0
                    INDColAmount.Visible = False
                    'General Information
                    INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'PopUp
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'Production Center Information
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'Measure Unit
                    INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'Campos para el copy & paste
                    Select Case FormParentName
                        Case eParent.GeneralExpenses
                            INDEsbBill.AddRangeColumns("Centro de Producción", "Cuenta Contable", "Centro de Costo")
                        Case eParent.DistributionSecondary
                            INDEsbBill.AddRangeColumns("Centro de Producción")
                    End Select
                Case eDistributionType.Calculated
                    MeasurementUnit = 1
                    INDColAmount.Visible = True
                    INDColAmount.VisibleIndex = IIf(FormParentName = eParent.GeneralExpenses, 3, 1)
                    'General Information
                    INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'PopUp
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'Production Center Information
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'Measure Unit
                    INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'Campos para el copy & paste
                    Select Case FormParentName
                        Case eParent.GeneralExpenses
                            INDEsbBill.AddRangeColumns("Centro de Producción", "Cuenta Contable", "Centro de Costo", "% Distribuir")
                        Case eParent.DistributionSecondary
                            INDEsbBill.AddRangeColumns("Centro de Producción", "% Distribuir")
                    End Select
                Case eDistributionType.Searched
                    MeasurementUnit = 0
                    INDColAmount.Visible = False
                    'General Information
                    INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'PopUp
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'Production Center Information
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'Measure Unit
                    INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'Campos para el copy & paste
                    Select Case FormParentName
                        Case eParent.GeneralExpenses
                            INDEsbBill.AddRangeColumns("Centro de Producción", "Cuenta Contable", "Centro de Costo")
                        Case eParent.DistributionSecondary
                            INDEsbBill.AddRangeColumns("Centro de Producción")
                    End Select
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleMeasureUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleMeasureUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleMeasureUnit.EditValueChanged
        If MeasurementUnit <> 0 Then
            INDgleDistributionOption.EditValue = 2
            INDgcProductionCenter.BeginUpdate()
            If MeasurementUnit = 1 Then
                INDspnDistributAmount.Properties.MaxValue = 100
                INDspnDistributAmount.Properties.Mask.EditMask = "P4"
                Me.INDColAmount.Caption = "% Distribuir"
                INDColAmount.SummaryItem.DisplayFormat = "{0:0.####}%"
                INDrpDistributionAmount.MaxValue = 100
                INDrpDistributionAmount.Mask.EditMask = "P4"
                INDgleDistributionOption.Enabled = False
            Else
                INDspnDistributAmount.Properties.MaxValue = 0
                INDspnDistributAmount.Properties.Mask.EditMask = "C0"
                Me.INDColAmount.Caption = "V. Distribuir"
                INDColAmount.SummaryItem.DisplayFormat = "{0:C0}"
                INDrpDistributionAmount.MaxValue = 0
                INDrpDistributionAmount.Mask.EditMask = "C0"
                INDgleDistributionOption.Enabled = True
            End If

            Select Case FormParentName
                Case eParent.GeneralExpenses
                    INDEsbBill.AddRangeColumns("Centro de Producción", "Cuenta Contable", "Centro de Costo", Me.INDColAmount.Caption)
                    If Not _isLoad AndAlso ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Count > 0 Then
                        For Each distrbBase As CostDistributionBaseDetail In ListDistributionBaseDetail.Where(Function(d) d.Quantity > 100)
                            distrbBase.Quantity = 0
                        Next
                    End If
                Case eParent.DistributionSecondary
                    INDEsbBill.AddRangeColumns("Centro de Producción", Me.INDColAmount.Caption)
                    If ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Count > 0 Then
                        For Each distrbBase As CostDistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail.Where(Function(d) d.Quantity > 100)
                            distrbBase.Quantity = 0
                        Next
                    End If
            End Select
            INDgcProductionCenter.EndUpdate()
            INDgcProductionCenter.RefreshDataSource()
        End If
        changeNumericFormatByCurrency(CurrencyAbbreviation.GetNumberFormat, INDpccProductionCenter.Controls)
        INDColAmount = Window.Utils.FormatGrid(INDColAmount, CurrencyAbbreviation)
    End Sub

    Private Sub INDsleProductionCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProductionCenter.EditValueChanged
        INDSLeCostCenterConcept.EditValue = Nothing
        INDSLeCostCenterConcept.Properties.DataSource = Nothing
    End Sub

    Private Sub INDsleCancellationCostMainAccountId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountId.EditValueChanged
        If INDsleAccountId.EditValue IsNot Nothing Then
            Dim accountXpo = DirectCast(DirectCast(viewSearchMainAccount.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.PUCServiceXpo)
            If accountXpo.HandlesCostCenter Then
                INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDSLeCostCenterConcept.EditValue = Nothing
                INDSLeCostCenterConcept.Properties.DataSource = Nothing
                INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the INDsbAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateProductionCenter() Then
            If FormParentName = eParent.GeneralExpenses Then
                Dim _productCenter As New CostDistributionBaseDetail()
                With _productCenter
                    .ProductionCenterId = INDsleProductionCenter.EditValue
                    .MainAccountId = INDsleAccountId.EditValue
                    .CodeNameMainAccount = INDsleAccountId.Text
                    .CostCenterId = INDSLeCostCenterConcept.EditValue
                    .CodeNameCostCenter = INDSLeCostCenterConcept.Text
                    .Quantity = INDspnDistributAmount.EditValue
                End With
                If ListDistributionBaseDetail Is Nothing Then
                    ListDistributionBaseDetail = New List(Of CostDistributionBaseDetail)()
                End If
                ListDistributionBaseDetail.Add(_productCenter)
            ElseIf FormParentName = eParent.DistributionSecondary Then
                Dim _productCenter As New CostDistributionSecondaryBaseDetail()
                With _productCenter
                    .ProductionCenterId = INDsleProductionCenter.EditValue
                    .Quantity = INDspnDistributAmount.EditValue
                End With
                If ListDistributionSecondaryBaseDetail Is Nothing Then
                    ListDistributionSecondaryBaseDetail = New List(Of CostDistributionSecondaryBaseDetail)()
                End If
                ListDistributionSecondaryBaseDetail.Add(_productCenter)
            End If
            INDgcProductionCenter.RefreshDataSource()
            CleanControlsPopup()
            INDsleProductionCenter.Focus()
        End If
        Me.INDgvProductionCenterList.OptionsView.ShowFooter = True
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbDistribute control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbDistribute_Click(sender As Object, e As EventArgs) Handles INDsbDistribute.Click
        If INDspnDistributedValue.EditValue <> 0 Then
            If MeasurementUnit <> 0 Then
                If ValidateValueDistribute() Then
                    If Not ValidateProductionCenterList().StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = ValidateProductionCenterList().Message
                    End If
                    Dim proportionValue As Decimal = 0
                    If INDgleDistributionOption.EditValue = 2 Then
                        If FormParentName = eParent.GeneralExpenses Then
                            proportionValue = InteropCostStaticService.CalculateRateValue(ListDistributionBaseDetail.Count, CType(INDspnDistributedValue.EditValue, Decimal))
                        ElseIf FormParentName = eParent.DistributionSecondary Then
                            proportionValue = InteropCostStaticService.CalculateRateValue(ListDistributionSecondaryBaseDetail.Count, CType(INDspnDistributedValue.EditValue, Decimal))
                        End If
                    Else
                        proportionValue = CType(INDspnDistributedValue.EditValue, Decimal)
                    End If

                    If FormParentName = eParent.GeneralExpenses Then
                        If ListDistributionBaseDetail IsNot Nothing Then
                            For Each item As CostDistributionBaseDetail In ListDistributionBaseDetail
                                item.Quantity = proportionValue
                            Next
                        End If
                    ElseIf FormParentName = eParent.DistributionSecondary Then
                        If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                            For Each item As CostDistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                                item.Quantity = proportionValue
                            Next
                        End If
                    End If
                    INDgcProductionCenter.RefreshDataSource()
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MeasureUnitSelectPlease", MODULE_NAME)
            End If
        End If
    End Sub

    Private Sub INDsbAddAllProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddAllProductionCenter.Click
        If FormParentName = eParent.DistributionSecondary Then
            InitializeProductionCenter()
            If ListDistributionSecondaryBaseDetail Is Nothing Then
                ListDistributionSecondaryBaseDetail = New List(Of CostDistributionSecondaryBaseDetail)()
            End If
            For Each p As CostProductionCenterXpo In listProductionCenters
                If Not ListDistributionSecondaryBaseDetail.Any(Function(x) x.ProductionCenterId = p.Id) Then
                    Dim _productCenter As New CostDistributionSecondaryBaseDetail()
                    With _productCenter
                        .ProductionCenterId = p.Id
                    End With
                    ListDistributionSecondaryBaseDetail.Add(_productCenter)
                End If
            Next
            INDgcProductionCenter.RefreshDataSource()
            CleanControlsPopup()
            INDsleProductionCenter.Focus()
        End If
    End Sub

    Private Sub INDBtnAddMeasureUnit_Click(sender As Object, e As EventArgs) Handles INDBtnAddMeasureUnit.Click
        If INDsleMeasureUnit.EditValue IsNot Nothing Then

            If FormParentName = eParent.GeneralExpenses Then

                If _listDistributionBaseMeasurementUnit Is Nothing Then
                    _listDistributionBaseMeasurementUnit = New List(Of CostDistributionBaseMeasurementUnit)
                End If
                Dim measureUnitAdded = _listDistributionBaseMeasurementUnit.Find(Function(x) x.MeasurementUnitId = INDsleMeasureUnit.EditValue)
                If measureUnitAdded IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya esta agregada la unidad de medida"
                    Exit Sub
                End If
                Dim DistributionBaseMeasurementUnit As New CostDistributionBaseMeasurementUnit
                DistributionBaseMeasurementUnit.MeasurementUnitId = INDsleMeasureUnit.EditValue
                DistributionBaseMeasurementUnit.CodeNameMeasureUnit = INDsleMeasureUnit.Text
                _listDistributionBaseMeasurementUnit.Add(DistributionBaseMeasurementUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionBaseMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
                INDsleMeasureUnit.EditValue = Nothing
            Else

                If _listDistributionSecondaryMeasurementUnit Is Nothing Then
                    _listDistributionSecondaryMeasurementUnit = New List(Of CostDistributionSecondaryMeasurementUnit)
                End If
                Dim measureUnitAdded = _listDistributionSecondaryMeasurementUnit.Find(Function(x) x.MeasurementUnitId = INDsleMeasureUnit.EditValue)
                If measureUnitAdded IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya esta agregada la unidad de medida"
                    Exit Sub
                End If
                Dim DistributionSecondaryMeasurementUnit As New CostDistributionSecondaryMeasurementUnit
                DistributionSecondaryMeasurementUnit.MeasurementUnitId = INDsleMeasureUnit.EditValue
                DistributionSecondaryMeasurementUnit.CodeNameMeasureUnit = INDsleMeasureUnit.Text
                _listDistributionSecondaryMeasurementUnit.Add(DistributionSecondaryMeasurementUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionSecondaryMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
                INDsleMeasureUnit.EditValue = Nothing
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una unidad de medida"
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddDistributionBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddDistributionBase_Click(sender As Object, e As EventArgs) Handles INDsbAddDistributionBase.Click
        If Not ValidateDistributionBase() Then
            Exit Sub
        End If

        If FormParentName = eParent.GeneralExpenses Then
            With _distributionBase
                .DistributionType = DistributionType
                .DistributionTypeName = INDgleDistributionType.Text
                .ImpactPoints = ImpactPoints
                .MeasurementUnit = MeasurementUnit
                .MeasureUnitName = INDgleMeasureUnit.Text
                .Description = Description
                .Sales = Sales
                .QuantitiesProduced = QuantitiesProduced
                .MultipleBase = MultipleBase
                .CostDistributionBaseDetail.Clear()
                If ListDistributionBaseDetail IsNot Nothing Then
                    For Each item As CostDistributionBaseDetail In ListDistributionBaseDetail
                        If DistributionType = 2 AndAlso item.Quantity = 0 Then
                            Continue For
                        End If

                        .CostDistributionBaseDetail.Add(item)
                    Next
                End If
                If DistributionType = 1 Then
                    For Each item In _listDistributionBaseMeasurementUnit
                        If item.Id > 0 Then
                            If .CostDistributionBaseMeasurementUnit.Where(Function(x) x.Id = item.Id).FirstOrDefault Is Nothing Then
                                .CostDistributionBaseMeasurementUnit.Add(item)
                            End If
                        Else
                            If .CostDistributionBaseMeasurementUnit.Where(Function(x) x.MeasurementUnitId = item.MeasurementUnitId).FirstOrDefault Is Nothing Then
                                .CostDistributionBaseMeasurementUnit.Add(item)
                            End If
                        End If
                    Next
                End If
            End With

            RaiseEvent AddDistributionBase(_editMode, _distributionBase)
        ElseIf FormParentName = eParent.DistributionSecondary Then
            With _distributionSecondaryBase
                .DistributionType = DistributionType
                .DistributionTypeName = INDgleDistributionType.Text
                .ImpactPoints = ImpactPoints
                .MeasurementUnit = MeasurementUnit
                .MeasureUnitName = INDgleMeasureUnit.Text
                .Description = Description
                .Sales = Sales
                .QuantitiesProduced = QuantitiesProduced
                .MultipleBase = MultipleBase
                If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                    For Each item As CostDistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                        If DistributionType = 2 AndAlso item.Quantity = 0 Then
                            Continue For
                        End If

                        .CostDistributionSecondaryBaseDetail.Add(item)
                    Next
                End If
                If DistributionType = 1 Then
                    For Each item In _listDistributionSecondaryMeasurementUnit
                        If item.Id > 0 Then
                            If .CostDistributionSecondaryMeasurementUnit.Where(Function(x) x.Id = item.Id).FirstOrDefault Is Nothing Then
                                .CostDistributionSecondaryMeasurementUnit.Add(item)
                            End If
                        Else
                            If .CostDistributionSecondaryMeasurementUnit.Where(Function(x) x.MeasurementUnitId = item.MeasurementUnitId).FirstOrDefault Is Nothing Then
                                .CostDistributionSecondaryMeasurementUnit.Add(item)
                            End If
                        End If
                    Next
                End If
            End With

            RaiseEvent AddDistributionBaseSecondary(_editMode, _distributionSecondaryBase)
        End If

        INDgleDistributionType.EditValue = Nothing
        Me.Close()
    End Sub

#End Region

#Region "Closed"

    ''' <summary>
    ''' Handles the Closed event of the INDpceAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddProductionCenter_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceAddProductionCenter.Closed
        INDsbAddDistributionBase.Focus()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la rejilla del % distribuir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrpDistributionAmount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpDistributionAmount.EditValueChanging
        If e IsNot Nothing AndAlso Not e.NewValue.ToString().Trim().Equals("") Then
            If FormParentName = eParent.GeneralExpenses Then
                Dim info As CostDistributionBaseDetail = INDgvProductionCenterList.GetFocusedRow()
                If info IsNot Nothing Then
                    info.Quantity = CDec(e.NewValue)
                    INDgcProductionCenter.RefreshDataSource()
                End If
            Else
                Dim info As CostDistributionSecondaryBaseDetail = INDgvProductionCenterList.GetFocusedRow()
                If info IsNot Nothing Then
                    info.Quantity = CDec(e.NewValue)
                    INDgcProductionCenter.RefreshDataSource()
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio de centro de produccion de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrpProductionCenter_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpProductionCenter.EditValueChanging
        If e IsNot Nothing Then
            If ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Where(Function(x) x.ProductionCenterId = CInt(e.NewValue)).Count() > 0 Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "El centro de produccion ya se encuentra agregado"
                Exit Sub
            End If
            Dim info As CostDistributionBaseDetail = INDgvProductionCenterList.GetFocusedRow()
            If info IsNot Nothing Then
                info.ProductionCenterId = CInt(e.NewValue)
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1201", Nothing, True)
        End If
    End Sub

    Private Sub INDsleMeasureUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasureUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("300", Nothing, True)
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Select Case FormParentName
                Case eParent.GeneralExpenses
                    Dim _distrBaseDetail As CostDistributionBaseDetail = CType(INDgvProductionCenterList.GetFocusedRow(), CostDistributionBaseDetail)
                    _distrBaseDetail.MarkAsDeleted()
                    ListDistributionBaseDetail.Remove(_distrBaseDetail)
                Case eParent.DistributionSecondary
                    Dim _distrBaseDetail As CostDistributionSecondaryBaseDetail = CType(INDgvProductionCenterList.GetFocusedRow(), CostDistributionSecondaryBaseDetail)
                    _distrBaseDetail.MarkAsDeleted()
                    ListDistributionSecondaryBaseDetail.Remove(_distrBaseDetail)
            End Select
            INDgcProductionCenter.RefreshDataSource()
        End If
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If FormParentName = eParent.GeneralExpenses Then
                Dim measureUnit = DirectCast(INDGvCostDistributionBaseMeasurementUnit.GetFocusedRow, CostDistributionBaseMeasurementUnit)
                measureUnit.MarkAsDeleted()
                _listDistributionBaseMeasurementUnit.Remove(measureUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionBaseMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
            Else
                Dim measureUnit = DirectCast(INDGvCostDistributionBaseMeasurementUnit.GetFocusedRow, CostDistributionSecondaryMeasurementUnit)
                measureUnit.MarkAsDeleted()
                _listDistributionSecondaryMeasurementUnit.Remove(measureUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionSecondaryMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, Data As List(Of List(Of String))) As Task
        Try
            If sender.Name = INDgcProductionCenter.Name Then
                AsyncLoader(True)
                Dim listErrors As New List(Of String)

                If FormParentName = eParent.GeneralExpenses Then
                    Using model As New MCostGeneralExpenses(Me.Tag)
                        Dim listToSend As New List(Of CostDistributionBaseDetail)
                        If ListDistributionBaseDetail IsNot Nothing Then
                            ListDistributionBaseDetail.ForEach(Sub(d)
                                                                   listToSend.Add(New CostDistributionBaseDetail With {
                                                                                    .ProductionCenterId = d.ProductionCenterId,
                                                                                    .MainAccountId = d.MainAccountId,
                                                                                    .CostCenterId = d.CostCenterId,
                                                                                    .Quantity = d.Quantity
                                                                                  })
                                                               End Sub)
                        End If
                        Dim result = Await model.ImportDetailsToCostDistributionBase(DistributionType, MeasurementUnit, listToSend, Data)
                        listErrors = result.MessageResult
                        If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                            If ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Count > 0 Then
                                ListDistributionBaseDetail.AddRange(result.ObjectEmbbeded)
                            Else
                                ListDistributionBaseDetail = result.ObjectEmbbeded
                            End If
                        End If
                    End Using
                ElseIf FormParentName = eParent.DistributionSecondary Then
                    Using model As New MCostDistributionSecondaryElements(Me.Tag)
                        Dim listToSend As New List(Of CostDistributionSecondaryBaseDetail)
                        If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                            ListDistributionSecondaryBaseDetail.ForEach(Sub(d)
                                                                            listToSend.Add(New CostDistributionSecondaryBaseDetail With {
                                                                                    .ProductionCenterId = d.ProductionCenterId,
                                                                                    .Quantity = d.Quantity
                                                                                  })
                                                                        End Sub)
                        End If
                        Dim result = Await model.ImportDetailsToCostDistributionSecondaryBase(DistributionType, MeasurementUnit, listToSend, Data)
                        listErrors = result.MessageResult
                        If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                            If ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Count > 0 Then
                                ListDistributionSecondaryBaseDetail.AddRange(result.ObjectEmbbeded)
                            Else
                                ListDistributionSecondaryBaseDetail = result.ObjectEmbbeded
                            End If
                        End If
                    End Using
                End If

                If listErrors.Count > 0 Then
                    Using formulario As New FrmListErrors(listErrors)
                        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If

                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDgcProductionCenter.RefreshDataSource()
                INDgvProductionCenterList.OptionsView.ShowFooter = True
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Private Sub InitializeProductionCenter()
        If FormParentName = eParent.GeneralExpenses Then
            INDrpProductionCenter.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatu(True)
            INDsleProductionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatu(True)
        Else
            INDrpProductionCenter.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatusAndCenterType(True, 1)
            INDsleProductionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListCostProductionCenterByStatusAndCenterType(True, 1)
            listProductionCenters = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterTypeXpCollection(True, 1)
        End If
    End Sub

    ''' <summary>
    ''' Crea los tipos de distribución
    ''' </summary>
    Private Sub CreateDistributionType()
        _distributionType = New List(Of Tuple(Of Integer, String))()
        _distributionType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("DistributionTypeCalculated", MODULE_NAME)))
        _distributionType.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("DistributionTypeSearch", MODULE_NAME)))
        INDgleDistributionType.Properties.DataSource = _distributionType
    End Sub

    ''' <summary>
    ''' Crea las unidades de medida
    ''' </summary>
    Private Sub CreateMeasureUnit()
        _measureUnit = New List(Of Tuple(Of Integer, String))()
        _measureUnit.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("MeasureUnitProportion", MODULE_NAME)))
        _measureUnit.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("MeasureUnitValue", MODULE_NAME)))
        INDgleMeasureUnit.Properties.DataSource = _measureUnit
    End Sub

    ''' <summary>
    ''' Creates the distribution options.
    ''' </summary>
    Private Sub CreateDistributionOptions()
        _distributionOption = New List(Of Tuple(Of Integer, String))()
        _distributionOption.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("DistributionOptionForEach", MODULE_NAME)))
        _distributionOption.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("DistributionOptionBetween", MODULE_NAME)))
        INDgleDistributionOption.Properties.DataSource = _distributionOption
    End Sub

    ''' <summary>
    ''' Carga los valores iniciales
    ''' </summary>
    Private Sub SetInitialValues()
        INDlcRoot.BeginUpdate()
        Me.SuspendLayout()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.StatusRecordVisible = False
        BarraBotones.Minimizar(True)
        BarraBotones.OperatingUnitVisible = False

        If _distributionBase Is Nothing AndAlso _distributionSecondaryBase Is Nothing Then
            CleanControls()
            If MultipleBase = Presentation.Cost.FrmCostGeneralExpenses.eMultipleBase.DistributionA AndAlso _distributionBase Is Nothing Then
                INDgleDistributionType.EditValue = 1
                INDcolProductionCenter.OptionsColumn.AllowEdit = True
                INDcolProductionCenter.OptionsColumn.AllowFocus = True
            Else
                INDgleDistributionType.EditValue = 2
                INDcolProductionCenter.OptionsColumn.AllowEdit = False
                INDcolProductionCenter.OptionsColumn.AllowFocus = False
                If CountDistributionBase < 2 Then
                    INDgvProductionCenterList.Columns.Where(Function(x) x.Name = "colActions").FirstOrDefault().Visible = False
                End If
                INDliAddProductionCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        Else
            If (_distributionBase IsNot Nothing AndAlso _distributionBase.MultipleBase <> 1) OrElse (_distributionSecondaryBase IsNot Nothing AndAlso _distributionSecondaryBase.MultipleBase <> 1) Then
                If CountDistributionBase < 2 Then
                    INDgvProductionCenterList.Columns.Where(Function(x) x.Name = "colActions").FirstOrDefault().Visible = False
                End If
                INDcolProductionCenter.OptionsColumn.AllowEdit = False
                INDcolProductionCenter.OptionsColumn.AllowFocus = False
                INDliAddProductionCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If

        If FormParentName = eParent.GeneralExpenses Then
            If _distributionBase Is Nothing Then
                _distributionBase = New CostDistributionBase()
            End If

            INDclbCosteoOptions.Items.RemoveAt(5)
            INDclbCosteoOptions.Items.Insert(5, New DevExpress.XtraEditors.Controls.CheckedListBoxItem("5", "Ventas"))
        ElseIf FormParentName = eParent.DistributionSecondary Then
            If _distributionSecondaryBase Is Nothing Then
                _distributionSecondaryBase = New CostDistributionSecondaryBase()
            End If
            If INDclbCosteoOptions.Items.Count < 7 Then
                INDclbCosteoOptions.Items.RemoveAt(5)
                INDclbCosteoOptions.Items.Insert(5, New DevExpress.XtraEditors.Controls.CheckedListBoxItem("5", "Ingresos facturados"))
                INDclbCosteoOptions.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("6", "Valor Primo"))
            End If
        End If

        INDgleDistributionOption.EditValue = 2
        INDsleProductionCenter.Properties.DataSource = Nothing
        InitializeProductionCenter()
        INDlcRoot.EndUpdate()
        Me.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    ''' <param name="distribBase">The distrib base.</param>
    Public Sub LoadDistributionBase(Optional distribBase As CostDistributionBase = Nothing, Optional distribSecondaryBase As CostDistributionSecondaryBase = Nothing)
        _isLoad = True
        _distributionBase = distribBase
        _distributionSecondaryBase = distribSecondaryBase

        Dim distribution As Object
        If distribBase Is Nothing Then
            distribution = distribSecondaryBase
            If INDclbCosteoOptions.Items.Count < 7 Then
                INDclbCosteoOptions.Items.RemoveAt(5)
                INDclbCosteoOptions.Items.Insert(5, New DevExpress.XtraEditors.Controls.CheckedListBoxItem("5", "Ingresos facturados"))
                INDclbCosteoOptions.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("6", "Valor Primo"))
            End If
            CousinValue = distribSecondaryBase.CousinValue
            InvoiceValue = distribSecondaryBase.InvoiceValue
        Else
            distribution = distribBase
        End If

        MultipleBase = distribution.MultipleBase
        INDgleDistributionType.EditValue = distribution.DistributionType
        INDspnIncidencePoint.EditValue = distribution.ImpactPoints
        INDtxtDescription.Text = distribution.Description
        INDgleMeasureUnit.EditValue = distribution.MeasurementUnit
        Area = distribution.Area
        OfficialHours = distribution.OfficialHours
        SupplyValue = distribution.SupplyValue
        WorkmanshipValue = distribution.WorkmanshipValue
        AssetValue = distribution.AssetValue
        If distribution.GetType().Name = "DistributionBase" Then
            Sales = distribution.Sales
        End If
        If distribBase Is Nothing Then
            If distribSecondaryBase.CostDistributionSecondaryBaseDetail IsNot Nothing Then
                For Each item As CostDistributionSecondaryBaseDetail In distribSecondaryBase.CostDistributionSecondaryBaseDetail.ToList()
                    ListDistributionSecondaryBaseDetail.Where(Function(x) x.ProductionCenterId = item.ProductionCenterId).Cast(Of CostDistributionSecondaryBaseDetail).FirstOrDefault().Quantity = item.Quantity
                Next
            End If
        End If
        If _listDistributionBaseMeasurementUnit IsNot Nothing AndAlso _listDistributionBaseMeasurementUnit.Count > 0 Then
            INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionBaseMeasurementUnit
            INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
        End If
        If _listDistributionSecondaryMeasurementUnit IsNot Nothing AndAlso _listDistributionSecondaryMeasurementUnit.Count > 0 Then
            INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionSecondaryMeasurementUnit
            INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
        End If
        INDgcProductionCenter.RefreshDataSource()
        _isLoad = False
    End Sub

    Private Sub CleanControlsPopup()
        INDsleProductionCenter.EditValue = Nothing
        INDspnDistributAmount.EditValue = 0
        INDsleAccountId.EditValue = Nothing
        INDSLeCostCenterConcept.EditValue = Nothing
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        If _isLoad Then
            Exit Sub
        End If

        'General Information
        INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'PopUp
        INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Production Center Information
        INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Measure Unit
        INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        'Columnas de la grilla
        INDcolProductionCenter.VisibleIndex = 0

        Select Case FormParentName
            Case eParent.GeneralExpenses
                ColMainAccount.VisibleIndex = 1
                ColCostCenter.VisibleIndex = 2
                If ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Count > 0 Then
                    For Each distrbBase As CostDistributionBaseDetail In ListDistributionBaseDetail
                        distrbBase.Quantity = 0
                    Next
                End If
                If _listDistributionBaseMeasurementUnit IsNot Nothing AndAlso _listDistributionBaseMeasurementUnit.Count > 0 Then
                    For Each item In _listDistributionBaseMeasurementUnit
                        item.MarkAsDeleted()
                    Next
                    _listDistributionBaseMeasurementUnit.Clear()
                    INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionBaseMeasurementUnit
                    INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
                End If
            Case eParent.DistributionSecondary
                INDLciMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ColMainAccount.Visible = False
                ColCostCenter.Visible = False
                If ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Count > 0 Then
                    For Each distrbBase As CostDistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                        distrbBase.Quantity = 0
                    Next
                End If
                If _listDistributionSecondaryMeasurementUnit IsNot Nothing AndAlso _listDistributionSecondaryMeasurementUnit.Count > 0 Then
                    For Each item In _listDistributionSecondaryMeasurementUnit
                        item.MarkAsDeleted()
                    Next
                    _listDistributionSecondaryMeasurementUnit.Clear()
                    INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionSecondaryMeasurementUnit
                    INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
                End If
        End Select

        Area = False
        OfficialHours = False
        SupplyValue = False
        WorkmanshipValue = False
        AssetValue = False
        If INDclbCosteoOptions.Items.Count = 6 Then
            Sales = False
        Else
            CousinValue = False
            InvoiceValue = False
        End If
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Validates the production center list.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateProductionCenterList() As ActionResult
        If FormParentName = eParent.GeneralExpenses Then
            If ListDistributionBaseDetail IsNot Nothing Then
                If DistributionType = eDistributionType.Calculated Then
                    If ListDistributionBaseDetail.Any(Function(x) x.Quantity <= 0) Then
                        ListDistributionBaseDetail.Where(Function(x) x.Quantity < 0).ToList().ForEach(Sub(d) d.Quantity = 0)
                        Me.Mensaje(EeventViewerImages.Advertencia) = "Se eliminarán los detalles con valores iguales o menores a cero"
                    End If
                    If MeasurementUnit = eMeasureUnit.Proporcion AndAlso ListDistributionBaseDetail.Sum(Function(x) x.Quantity) <> 100 Then
                        Return New ActionResult With {.Message = "La suma de los valores distribuidos por proporción debe ser igual a 100%", .StateResult = False}
                    End If
                End If
            End If
        ElseIf FormParentName = eParent.DistributionSecondary Then
            If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                If DistributionType = eDistributionType.Calculated Then
                    If ListDistributionSecondaryBaseDetail.Any(Function(x) x.Quantity <= 0) Then
                        ListDistributionSecondaryBaseDetail.Where(Function(x) x.Quantity < 0).ToList().ForEach(Sub(d) d.Quantity = 0)
                        Me.Mensaje(EeventViewerImages.Advertencia) = "Se eliminarán los detalles con valores iguales o menores a cero"
                    End If
                    If MeasurementUnit = eMeasureUnit.Proporcion AndAlso ListDistributionSecondaryBaseDetail.Sum(Function(x) x.Quantity) <> 100 Then
                        Return New ActionResult With {.Message = "La suma de los valores distribuidos por proporción debe ser igual a 100%", .StateResult = False}
                    End If
                End If
            End If
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Validates the distribution base.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDistributionBase() As Boolean
        Dim errorList As New StringBuilder()

        If DistributionType = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), INDliDistributionType.Text))
        End If
        If Description = String.Empty Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("Description", MODULE_NAME)))
        End If
        If FormParentName = eParent.GeneralExpenses Then
            If ListDistributionBaseDetail Is Nothing OrElse ListDistributionBaseDetail.Where(Function(x) DistributionType <> eDistributionType.Calculated OrElse x.Quantity <> 0).Count() = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centros de Producción"))
            End If
        ElseIf FormParentName = eParent.DistributionSecondary Then
            If ListDistributionSecondaryBaseDetail Is Nothing OrElse ListDistributionSecondaryBaseDetail.Where(Function(x) DistributionType <> eDistributionType.Calculated OrElse x.Quantity <> 0).Count() = 0 Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centros de Producción"))
            End If
        End If

        Select Case DistributionType
            Case eDistributionType.Direct
                If INDGvCostDistributionBaseMeasurementUnit.RowCount = 0 Then
                    errorList.AppendLine("Se debe agregar minimo una unidad de medida")
                End If
            Case eDistributionType.Calculated
                If MeasurementUnit = 0 Then
                    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), INDliMeasureUnit.Text))
                End If

                If Not ValidateProductionCenterList().StateResult Then
                    errorList.AppendLine(ValidateProductionCenterList().Message)
                End If
            Case eDistributionType.Searched
                If Not (Area OrElse OfficialHours OrElse SupplyValue OrElse WorkmanshipValue OrElse AssetValue OrElse Sales) Then
                    If INDclbCosteoOptions.Items.Count < 7 OrElse Not (CousinValue OrElse InvoiceValue) Then
                        errorList.AppendLine("Debe seleccionar al menos una opcion de costeo")
                    End If
                End If
        End Select

        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Validates the value distribute.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateValueDistribute() As Boolean
        Select Case FormParentName
            Case eParent.GeneralExpenses
                If ListDistributionBaseDetail Is Nothing OrElse ListDistributionBaseDetail.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ListProductionCenterNothing", MODULE_NAME)
                    Return False
                End If
            Case eParent.DistributionSecondary
                If ListDistributionSecondaryBaseDetail Is Nothing OrElse ListDistributionSecondaryBaseDetail.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ListProductionCenterNothing", MODULE_NAME)
                    Return False
                End If
        End Select

        If MeasurementUnit = eMeasureUnit.Proporcion AndAlso INDspnDistributedValue.EditValue > 100 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MaximumDistributionError", MODULE_NAME)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Validates the production center.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateProductionCenter() As Boolean
        Dim errorList As New StringBuilder()
        If CountDistributionBase > 1 Then
            errorList.AppendLine("No se puede agregar mas centros de produccion ya que se han creado otros detalles")
        End If
        If INDsleProductionCenter.EditValue Is Nothing Then
            errorList.AppendLine("Seleccione un Centro de Producción")
        Else
            If FormParentName = eParent.GeneralExpenses Then
                If INDsleAccountId.EditValue Is Nothing Then
                    errorList.AppendLine("Seleccione una Cuenta Contable")
                End If
                If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If INDSLeCostCenterConcept.EditValue Is Nothing Then
                        errorList.AppendLine("Seleccione un Centro de Costo")
                    End If
                End If
                If ListDistributionBaseDetail IsNot Nothing Then
                    If ListDistributionBaseDetail.Where(Function(x) x.ProductionCenterId = CInt(INDsleProductionCenter.EditValue) AndAlso x.MainAccountId = CInt(INDsleAccountId.EditValue) AndAlso x.CostCenterId = INDSLeCostCenterConcept.EditValue).Count() > 0 Then
                        errorList.AppendLine("El centro de producción con la cuenta contable y el centro de costo ya se encuentra en el listado")
                    End If
                    If MeasurementUnit = eMeasureUnit.Proporcion AndAlso (ListDistributionBaseDetail.Sum(Function(x) x.Quantity) + CType(INDspnDistributAmount.EditValue, Decimal)) > 100 Then
                        errorList.AppendLine(ResourceManager.GetString("MaximumDistributionErrorSum", MODULE_NAME))
                    End If
                End If
            ElseIf FormParentName = eParent.DistributionSecondary Then
                If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                    If ListDistributionSecondaryBaseDetail.Where(Function(x) x.ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer)).Count() > 0 Then
                        errorList.AppendLine(ResourceManager.GetString("ProductionCenterRepeat", MODULE_NAME))
                    End If
                    If MeasurementUnit = eMeasureUnit.Proporcion AndAlso (ListDistributionSecondaryBaseDetail.Sum(Function(x) x.Quantity) + CType(INDspnDistributAmount.EditValue, Decimal)) > 100 Then
                        errorList.AppendLine(ResourceManager.GetString("MaximumDistributionErrorSum", MODULE_NAME))
                    End If
                End If
            End If
        End If

        If INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If DistributionType <> eDistributionType.Searched AndAlso (INDspnDistributAmount.EditValue Is Nothing OrElse INDspnDistributAmount.EditValue = 0) Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("Quantity", MODULE_NAME)))
            End If
        End If

        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Bar Button"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

#End Region

#Region "Enums"

    Public Enum eDistributionType
        Direct = 1
        Calculated = 2
        Searched = 3
    End Enum

    Public Enum eMeasureUnit
        Proporcion = 1
        Valor = 2
    End Enum

    Public Enum eDistributionOption
        CadaUno = 1
        EntreTodos = 2
    End Enum

    Public Enum eParent
        GeneralExpenses
        DistributionSecondary
    End Enum

#End Region

End Class

