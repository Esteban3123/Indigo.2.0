Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports System.Drawing
Imports Presentation.InteropCost.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository

Public Class PopUpDistributionBase

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="PopUpDistributionBase"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        CreateButtonCenter()
    End Sub
#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' Occurs when [add distribution base].
    ''' </summary>
    Public Event AddDistributionBase(ByVal distributionBase As DistributionBase, ByVal listDelete As List(Of DistributionBaseDetail))
    ''' <summary>
    ''' Occurs when [add distribution base secondary].
    ''' </summary>
    Public Event AddDistributionBaseSecondary(ByVal distributionBase As DistributionSecondaryBase, ByVal listDelete As List(Of DistributionSecondaryBaseDetail))
    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"

    ''' <summary>
    ''' Obtiene la base multiple seleccioanda en el frontal de gastos generales
    ''' </summary>
    Property MultipleBase As Byte

    ''' <summary>
    ''' boton que limpia los centros de producción
    ''' </summary>
    Private INDsbRemoveCenter As SimpleButton
    ''' <summary>
    ''' boton que agrega todos los centros de producción
    ''' </summary>
    Private INDsbLoadCenter As SimpleButton


    Private listProductionCenters As XPCollection(Of ProductionCenterXpo)

    ''' <summary>
    ''' Obtiene o establece el listado de centros de produccion (Solo se modifican si son tipo A)
    ''' </summary>
    Property ListDistributionBaseDetail As List(Of Domain.Entities.DistributionBaseDetail)
        Get
            Return CType(INDgcProductionCenter.DataSource, List(Of Domain.Entities.DistributionBaseDetail))
        End Get
        Set(value As List(Of Domain.Entities.DistributionBaseDetail))
            INDgcProductionCenter.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de centros de produccion (Solo se modifican si son tipo A)
    ''' </summary>
    Property ListDistributionSecondaryBaseDetail As List(Of Domain.Entities.DistributionSecondaryBaseDetail)
        Get
            Return CType(INDgcProductionCenter.DataSource, List(Of Domain.Entities.DistributionSecondaryBaseDetail))
        End Get
        Set(value As List(Of Domain.Entities.DistributionSecondaryBaseDetail))
            INDgcProductionCenter.DataSource = value
        End Set
    End Property

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
    ''' Gets or sets a value indicating whether [area].
    ''' </summary>
    Property Area As Boolean
        Get
            Return INDclbCosteoOptions.Items(0).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(0).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(0).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [official hours].
    ''' </summary>
    Property OfficialHours As Boolean
        Get
            Return INDclbCosteoOptions.Items(1).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(1).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(1).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [supply value].
    ''' </summary>
    Property SupplyValue As Boolean
        Get
            Return INDclbCosteoOptions.Items(2).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(2).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(2).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [workmanship value].
    ''' </summary>
    Property WorkmanshipValue As Boolean
        Get
            Return INDclbCosteoOptions.Items(3).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(3).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(3).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [asset value].
    ''' </summary>
    Property AssetValue As Boolean
        Get
            Return INDclbCosteoOptions.Items(4).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(4).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(4).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [cousin value].
    ''' </summary>
    Property CousinValue As Boolean
        Get
            Return INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [invoice value].
    ''' </summary>
    Property InvoiceValue As Boolean
        Get
            Return INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    Property Sales As Boolean
        Get
            Return INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Checked
        End Get
        Set(value As Boolean)
            If value Then
                INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Checked
            Else
                INDclbCosteoOptions.Items(5).CheckState = System.Windows.Forms.CheckState.Unchecked
            End If
        End Set
    End Property

    ''' <summary>
    ''' variable de distributionBase
    ''' </summary>
    Private _distributionBase As DistributionBase

    ''' <summary>
    ''' Variable para conocer que frontal lo está instanciando
    ''' </summary>
    Property FormParentName As eParent

    ''' <summary>
    ''' distribucion secundaria
    ''' </summary>
    Private _distributionSecondaryBase As DistributionSecondaryBase

    ''' <summary>
    ''' The _state open pop up production center
    ''' </summary>
    Private _stateOpenPopUpProductionCenter As Boolean
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

    Dim _costClass As Byte
    Public WriteOnly Property CostClass As Byte
        Set(value As Byte)
            _costClass = value
        End Set
    End Property


    Public WriteOnly Property ListDistributionBaseMeasurementUnit As List(Of DistributionBaseMeasurementUnit)
        Set(value As List(Of DistributionBaseMeasurementUnit))
            _listDistributionBaseMeasurementUnit = value
        End Set
    End Property


    Public WriteOnly Property ListDistributionSecondaryMeasurementUnit As List(Of DistributionSecondaryMeasurementUnit)
        Set(value As List(Of DistributionSecondaryMeasurementUnit))
            _listDistributionSecondaryMeasurementUnit = value
        End Set
    End Property


#End Region

#Region "Events"

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la rejilla del % distribuir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub RepositoryItemSpinEdit1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrpDistributionAmount.EditValueChanging
        If e IsNot Nothing AndAlso Not e.NewValue.ToString().Trim().Equals("") Then
            Dim info As DistributionBaseDetail = INDgvProductionCenterList.GetFocusedRow()
            If info IsNot Nothing Then
                info.Quantity = CDec(e.NewValue)
                INDgcProductionCenter.RefreshDataSource()
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
            Dim info As DistributionBaseDetail = INDgvProductionCenterList.GetFocusedRow()
            If info IsNot Nothing Then
                info.ProductionCenterId = CInt(e.NewValue)
            End If
        End If
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the PopUpDistributionBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PopUpDistributionBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
#End Region

#Region "KeyDonw"
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

#Region "DatasourceChanged"
    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDirectCostDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcProductionCenter.DataSourceChanged
        Select Case FormParentName
            Case eParent.GeneralExpenses
                If (ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Count > 0) Then
                    INDsbLoadCenter.Location = New Point((INDgcProductionCenter.Width - INDsbLoadCenter.Size.Width - 20), 25)
                    INDsbRemoveCenter.Location = New Point((INDgcProductionCenter.Width - INDsbRemoveCenter.Size.Width - INDsbLoadCenter.Width - 30), 25)
                Else
                    INDsbLoadCenter.Location = New Point((INDgcProductionCenter.Width - INDsbLoadCenter.Size.Width - 20), 5)
                    INDsbRemoveCenter.Location = New Point((INDgcProductionCenter.Width - INDsbRemoveCenter.Size.Width - INDsbLoadCenter.Width - 30), 5)
                End If
            Case eParent.DistributionSecondary
                If (ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Count > 0) Then
                    INDsbLoadCenter.Location = New Point((INDgcProductionCenter.Width - INDsbLoadCenter.Size.Width - 20), 25)
                    INDsbRemoveCenter.Location = New Point((INDgcProductionCenter.Width - INDsbRemoveCenter.Size.Width - INDsbLoadCenter.Width - 30), 25)
                Else
                    INDsbLoadCenter.Location = New Point((INDgcProductionCenter.Width - INDsbLoadCenter.Size.Width - 20), 5)
                    INDsbRemoveCenter.Location = New Point((INDgcProductionCenter.Width - INDsbRemoveCenter.Size.Width - INDsbLoadCenter.Width - 30), 5)
                End If
        End Select
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
                    'INDgleMeasureUnit.EditValue = Nothing
                    'INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'INDclbCosteoOptions.Refresh()
                    'INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    'ListDistributionBaseDetail = Nothing
                    'INDlcgInformationCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    MeasurementUnit = 0
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDcolAmount.Visible = False
                Case eDistributionType.Calculated
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDcolAmount.Visible = True
                Case eDistributionType.Searched
                    Select Case FormParentName
                        Case eParent.GeneralExpenses
                            If ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Count > 0 Then
                                For Each distrbBase As DistributionBaseDetail In ListDistributionBaseDetail
                                    distrbBase.Quantity = 0
                                Next
                            End If
                        Case eParent.DistributionSecondary
                            If ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Count > 0 Then
                                For Each distrbBase As DistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                                    distrbBase.Quantity = 0
                                Next
                            End If
                    End Select
                    INDlyciAddAllProductionCenters.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDcolAmount.Visible = False
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
                Me.INDColAmount.Caption = "% Distribuir"
                INDColAmount.SummaryItem.DisplayFormat = "{0:0.####}%"
                INDspnDistributAmount.Properties.Mask.EditMask = "P4"
                INDrpDistributionAmount.Mask.EditMask = "P4"
                INDgleDistributionOption.Enabled = False
            Else
                Me.INDColAmount.Caption = "V. Distribuir"
                INDColAmount.SummaryItem.DisplayFormat = "{0:C0}"
                INDspnDistributAmount.Properties.Mask.EditMask = "C0"
                INDrpDistributionAmount.Mask.EditMask = "C0"
                INDgleDistributionOption.Enabled = True
            End If
            INDgcProductionCenter.EndUpdate()
            INDgcProductionCenter.RefreshDataSource()
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
        If Not _stateOpenPopUpProductionCenter Then
            InitializeProductionCenter()
            _stateOpenPopUpProductionCenter = True
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddDistributionBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddDistributionBase_Click(sender As Object, e As EventArgs) Handles INDsbAddDistributionBase.Click
        Dim totalPercentajeDistribution As Decimal
        If ValidateDistributionBase() Then
            If FormParentName = eParent.GeneralExpenses Then
                If _listDeletedDistributionBaseDetail IsNot Nothing AndAlso _listDeletedDistributionBaseDetail.Any() Then
                    While _listDeletedDistributionBaseDetail.Count > 0
                        _listDeletedDistributionBaseDetail(0).MarkAsDeleted()
                        _listDeletedDistributionBaseDetail.RemoveAt(0)
                    End While
                End If
                If _listDistributionBaseMeasurementUnitDelete IsNot Nothing AndAlso _listDistributionBaseMeasurementUnitDelete.Any() Then
                    While _listDistributionBaseMeasurementUnitDelete.Count > 0
                        _listDistributionBaseMeasurementUnitDelete(0).MarkAsDeleted()
                        _listDistributionBaseMeasurementUnitDelete.RemoveAt(0)
                    End While
                End If
                With _distributionBase
                    .DistributionType = DistributionType
                    .ImpactPoints = ImpactPoints
                    .MeasurementUnit = MeasurementUnit
                    .Description = Description
                    .Area = Area
                    .OfficialHours = OfficialHours
                    .SupplyValue = SupplyValue
                    .WorkmanshipValue = WorkmanshipValue
                    .AssetValue = AssetValue
                    .Sales = Sales
                    .MultipleBase = MultipleBase
                    .DistributionTypeName = INDgleDistributionType.Text
                    .MeasureUnitName = INDgleMeasureUnit.Text
                    If ListDistributionBaseDetail IsNot Nothing Then
                        For Each item As DistributionBaseDetail In ListDistributionBaseDetail
                            totalPercentajeDistribution = totalPercentajeDistribution + item.Quantity
                            Dim distributionDetail As DistributionBaseDetail = _distributionBase.DistributionBaseDetail.Where(Function(x) x.ProductionCenterId = item.ProductionCenterId).FirstOrDefault()
                            If distributionDetail IsNot Nothing Then
                                If item.Quantity <= 0 AndAlso DistributionType = 2 Then 'Validamos que solo permita si el tipo es calculada y la cantidad es mayor a cero
                                    Me.Mensaje(EeventViewerImages.Advertencia) = "Se eliminarán los centros de producción con valores en cero"
                                    distributionDetail.MarkAsDeleted()
                                Else
                                    distributionDetail.Quantity = item.Quantity
                                End If
                            Else
                                If item.Quantity <= 0 AndAlso DistributionType = 2 Then 'Validamos que solo permita si el tipo es calculada y la cantidad es mayor a cero
                                    Me.Mensaje(EeventViewerImages.Advertencia) = "Existen centros de producción con valores en cero, por favor verifique para poder continuar"
                                    Exit Sub
                                Else
                                    distributionDetail = New DistributionBaseDetail()
                                    With distributionDetail
                                        .ProductionCenterId = item.ProductionCenterId
                                        .Quantity = item.Quantity
                                        .MainAccountId = item.MainAccountId
                                        .CostCenterId = item.CostCenterId
                                        .CodeNameMainAccount = item.CodeNameMainAccount
                                        .CodeNameCostCenter = item.CodeNameCostCenter
                                    End With
                                    .DistributionBaseDetail.Add(distributionDetail)
                                End If
                            End If
                        Next
                    End If
                    If MeasurementUnit = 1 AndAlso totalPercentajeDistribution <> 100.0 AndAlso INDColAmount.Visible Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = "El total de porcentaje a distribuir debe estar al 100%"
                        Exit Sub
                    End If
                    If INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        For Each item In _listDistributionBaseMeasurementUnit
                            If item.Id > 0 Then
                                If .DistributionBaseMeasurementUnit.Where(Function(x) x.Id = item.Id).FirstOrDefault Is Nothing Then
                                    .DistributionBaseMeasurementUnit.Add(item)
                                End If
                            Else
                                If .DistributionBaseMeasurementUnit.Where(Function(x) x.MeasurementUnitId = item.MeasurementUnitId).FirstOrDefault Is Nothing Then
                                    .DistributionBaseMeasurementUnit.Add(item)
                                End If
                            End If
                        Next
                    End If
                End With
            ElseIf FormParentName = eParent.DistributionSecondary Then
                If _listDeletedDistributionSecondaryBaseDetail IsNot Nothing AndAlso _listDeletedDistributionSecondaryBaseDetail.Any() Then
                    While _listDeletedDistributionSecondaryBaseDetail.Count > 0
                        _listDeletedDistributionSecondaryBaseDetail(0).MarkAsDeleted()
                        _listDeletedDistributionSecondaryBaseDetail.RemoveAt(0)
                    End While
                End If
                If _listDistributionSecondaryMeasurementUnitDelete IsNot Nothing AndAlso _listDistributionSecondaryMeasurementUnitDelete.Any() Then
                    While _listDistributionSecondaryMeasurementUnitDelete.Count > 0
                        _listDistributionSecondaryMeasurementUnitDelete(0).MarkAsDeleted()
                        _listDistributionSecondaryMeasurementUnitDelete.RemoveAt(0)
                    End While
                End If
                With _distributionSecondaryBase
                    .DistributionType = DistributionType
                    .ImpactPoints = ImpactPoints
                    .MeasurementUnit = MeasurementUnit
                    .Description = Description
                    .Area = Area
                    .OfficialHours = OfficialHours
                    .SupplyValue = SupplyValue
                    .WorkmanshipValue = WorkmanshipValue
                    .AssetValue = AssetValue
                    .CousinValue = CousinValue
                    .InvoiceValue = InvoiceValue
                    .MultipleBase = MultipleBase
                    .DistributionTypeName = INDgleDistributionType.Text
                    .MeasureUnitName = INDgleMeasureUnit.Text
                    If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                        For Each item As DistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                            Dim distributionDetail As DistributionSecondaryBaseDetail = _distributionSecondaryBase.DistributionSecondaryBaseDetail.Where(Function(x) x.ProductionCenterId = item.ProductionCenterId).FirstOrDefault()
                            If distributionDetail IsNot Nothing Then
                                If item.Quantity <= 0 AndAlso DistributionType = 2 Then 'Validamos que solo permita si el tipo es calculada y la cantidad es mayor a cero
                                    Me.Mensaje(EeventViewerImages.Advertencia) = "Se eliminarán los centros de producción con valores en cero"
                                    distributionDetail.MarkAsDeleted()
                                Else
                                    distributionDetail.Quantity = item.Quantity
                                End If
                            Else
                                If item.Quantity <= 0 AndAlso DistributionType = 2 Then 'Validamos que solo permita si el tipo es calculada y la cantidad es mayor a cero
                                    Me.Mensaje(EeventViewerImages.Advertencia) = "Existen centros de producción con valores en cero, por favor verifique para poder continuar"
                                    Exit Sub
                                Else
                                    distributionDetail = New DistributionSecondaryBaseDetail()
                                    With distributionDetail
                                        .ProductionCenterId = item.ProductionCenterId
                                        .Quantity = item.Quantity
                                    End With
                                    .DistributionSecondaryBaseDetail.Add(distributionDetail)
                                End If
                            End If
                        Next
                    End If
                    If INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        For Each item In _listDistributionSecondaryMeasurementUnit
                            If item.Id > 0 Then
                                If .DistributionSecondaryMeasurementUnit.Where(Function(x) x.Id = item.Id).FirstOrDefault Is Nothing Then
                                    .DistributionSecondaryMeasurementUnit.Add(item)
                                End If
                            Else
                                If .DistributionSecondaryMeasurementUnit.Where(Function(x) x.MeasurementUnitId = item.MeasurementUnitId).FirstOrDefault Is Nothing Then
                                    .DistributionSecondaryMeasurementUnit.Add(item)
                                End If
                            End If
                        Next
                    End If
                End With
            End If
            If FormParentName = eParent.GeneralExpenses Then
                RaiseEvent AddDistributionBase(_distributionBase, _listDeletedDistributionBaseDetail)
            ElseIf FormParentName = eParent.DistributionSecondary Then
                RaiseEvent AddDistributionBaseSecondary(_distributionSecondaryBase, _listDeletedDistributionSecondaryBaseDetail)
            End If
            INDgleDistributionType.EditValue = Nothing
            Me.Close()
        End If
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
                            For Each item As DistributionBaseDetail In ListDistributionBaseDetail
                                item.Quantity = proportionValue
                            Next
                        End If
                    ElseIf FormParentName = eParent.DistributionSecondary Then
                        If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                            For Each item As DistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
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

    ''' <summary>
    ''' Handles the Click event of the INDsbAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateProductionCenter() Then
            If FormParentName = eParent.GeneralExpenses Then
                Dim _productCenter As New DistributionBaseDetail()
                With _productCenter
                    .ProductionCenterId = INDsleProductionCenter.EditValue
                    .Quantity = INDspnDistributAmount.EditValue
                    .MainAccountId = INDsleAccountId.EditValue
                    .CodeNameMainAccount = INDsleAccountId.Text
                    .CostCenterId = INDSLeCostCenterConcept.EditValue
                    .CodeNameCostCenter = INDSLeCostCenterConcept.Text
                End With
                If ListDistributionBaseDetail Is Nothing Then
                    ListDistributionBaseDetail = New List(Of DistributionBaseDetail)()
                End If
                ListDistributionBaseDetail.Add(_productCenter)
            ElseIf FormParentName = eParent.DistributionSecondary Then
                Dim _productCenter As New DistributionSecondaryBaseDetail()
                With _productCenter
                    .ProductionCenterId = INDsleProductionCenter.EditValue
                    .Quantity = INDspnDistributAmount.EditValue
                End With
                If ListDistributionSecondaryBaseDetail Is Nothing Then
                    ListDistributionSecondaryBaseDetail = New List(Of DistributionSecondaryBaseDetail)()
                End If
                ListDistributionSecondaryBaseDetail.Add(_productCenter)
            End If
            INDgcProductionCenter.RefreshDataSource()
            CleanControlsPopup()
            INDsleProductionCenter.Focus()
        End If
        Me.INDgvProductionCenterList.OptionsView.ShowFooter = True
    End Sub

    Private Sub CleanControlsPopup()
        INDsleProductionCenter.EditValue = Nothing
        INDspnDistributAmount.EditValue = 0
        INDsleAccountId.EditValue = Nothing
        INDSLeCostCenterConcept.EditValue = Nothing
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub



    ''' <summary>
    ''' Handles the Click event of the INDsbLoadCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsbLoadCenter_Click(sender As Object, e As EventArgs)
        If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using Model As New MProductionCenter(Me.Tag)
                Dim listProductionCenter As List(Of ProductionCenter) = Await Model.ListProductionCenter()
                If listProductionCenter IsNot Nothing AndAlso listProductionCenter.Count > 0 Then

                    Select Case FormParentName
                        Case eParent.GeneralExpenses

                            If ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Count > 0 Then
                                For Each item As DistributionBaseDetail In ListDistributionBaseDetail
                                    listProductionCenter.Remove(listProductionCenter.Where(Function(x) x.Id = item.ProductionCenterId).FirstOrDefault())
                                Next
                            End If
                            For Each item As ProductionCenter In listProductionCenter
                                Dim _distribFixedAssetDetail As New DistributionBaseDetail()
                                With _distribFixedAssetDetail
                                    .ProductionCenterId = item.Id
                                    .Quantity = 0
                                End With
                                _distributionBase.DistributionBaseDetail.Add(_distribFixedAssetDetail)
                            Next
                            ListDistributionBaseDetail = _distributionBase.DistributionBaseDetail.ToList()

                        Case eParent.DistributionSecondary

                            If ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Count > 0 Then
                                For Each item As DistributionSecondaryBaseDetail In ListDistributionSecondaryBaseDetail
                                    listProductionCenter.Remove(listProductionCenter.Where(Function(x) x.Id = item.ProductionCenterId).FirstOrDefault())
                                Next
                            End If
                            For Each item As ProductionCenter In listProductionCenter
                                Dim _distribFixedAssetDetail As New DistributionSecondaryBaseDetail()
                                With _distribFixedAssetDetail
                                    .ProductionCenterId = item.Id
                                    .Quantity = 0
                                End With
                                _distributionSecondaryBase.DistributionSecondaryBaseDetail.Add(_distribFixedAssetDetail)
                            Next
                            ListDistributionSecondaryBaseDetail = _distributionSecondaryBase.DistributionSecondaryBaseDetail.ToList()

                    End Select
                    INDgcProductionCenter.RefreshDataSource()
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbRemoveCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbRemoveCenter_Click(sender As Object, e As EventArgs)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Select Case FormParentName
                Case eParent.GeneralExpenses
                    If _distributionBase.DistributionBaseDetail IsNot Nothing AndAlso _distributionBase.DistributionBaseDetail.Count > 0 Then
                        While _distributionBase.DistributionBaseDetail.Count > 0
                            _distributionBase.DistributionBaseDetail(_distributionBase.DistributionBaseDetail.Count - 1).MarkAsDeleted()
                        End While
                        ListDistributionBaseDetail = _distributionBase.DistributionBaseDetail.ToList()
                    End If
                Case eParent.DistributionSecondary
                    If _distributionSecondaryBase.DistributionSecondaryBaseDetail IsNot Nothing AndAlso _distributionSecondaryBase.DistributionSecondaryBaseDetail.Count > 0 Then
                        While _distributionSecondaryBase.DistributionSecondaryBaseDetail.Count > 0
                            _distributionSecondaryBase.DistributionSecondaryBaseDetail(_distributionSecondaryBase.DistributionSecondaryBaseDetail.Count - 1).MarkAsDeleted()
                        End While
                        ListDistributionSecondaryBaseDetail = _distributionSecondaryBase.DistributionSecondaryBaseDetail.ToList()
                    End If
            End Select
            INDgcProductionCenter.RefreshDataSource()
        End If
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

#Region "Actions"

    Dim _listDeletedDistributionBaseDetail As List(Of DistributionBaseDetail)

    Dim _listDeletedDistributionSecondaryBaseDetail As List(Of DistributionSecondaryBaseDetail)

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Select Case FormParentName
                Case eParent.GeneralExpenses
                    Dim _distrBaseDetail As DistributionBaseDetail = CType(INDgvProductionCenterList.GetFocusedRow(), DistributionBaseDetail)
                    If _listDeletedDistributionBaseDetail Is Nothing Then
                        _listDeletedDistributionBaseDetail = New List(Of DistributionBaseDetail)()
                    End If
                    _listDeletedDistributionBaseDetail.Add(_distrBaseDetail)
                    '_distrBaseDetail.MarkAsDeleted()
                    ListDistributionBaseDetail.Remove(_distrBaseDetail)
                Case eParent.DistributionSecondary
                    Dim _distrBaseDetail As DistributionSecondaryBaseDetail = CType(INDgvProductionCenterList.GetFocusedRow(), DistributionSecondaryBaseDetail)
                    If _listDeletedDistributionSecondaryBaseDetail Is Nothing Then
                        _listDeletedDistributionSecondaryBaseDetail = New List(Of DistributionSecondaryBaseDetail)()
                    End If
                    _listDeletedDistributionSecondaryBaseDetail.Add(_distrBaseDetail)
                    '_distrBaseDetail.MarkAsDeleted()
                    ListDistributionSecondaryBaseDetail.Remove(_distrBaseDetail)
            End Select
            INDgcProductionCenter.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1201", Nothing, True)
        End If
    End Sub
#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliCosteoOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlcgInformationCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliDistributionOption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlibtnDistribute.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        If FormParentName = eParent.DistributionSecondary Then
            INDLciMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ColMainAccount.Visible = False
            ColCostCenter.Visible = False
        End If
        INDcolAmount.Visible = False
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Creates the button center.
    ''' </summary>
    Private Sub CreateButtonCenter()
        INDsbLoadCenter = New SimpleButton()
        INDsbLoadCenter.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        INDsbLoadCenter.Appearance.Options.UseFont = True
        INDsbLoadCenter.MaximumSize = New System.Drawing.Size(100, 20)
        INDsbLoadCenter.MinimumSize = New System.Drawing.Size(100, 20)
        INDsbLoadCenter.Name = "INDsbLoadCenter"
        INDsbLoadCenter.Size = New System.Drawing.Size(100, 20)
        INDsbLoadCenter.Text = "Agregar Centros"
        AddHandler INDsbLoadCenter.Click, AddressOf INDsbLoadCenter_Click
        If INDgcProductionCenter IsNot Nothing Then
            Me.INDgcProductionCenter.Controls.Add(INDsbLoadCenter)
            INDsbLoadCenter.Location = New Point((INDgcProductionCenter.Width - INDsbLoadCenter.Size.Width - 5), 5)
            INDsbLoadCenter.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            INDsbLoadCenter.BringToFront()
        End If

        INDsbRemoveCenter = New SimpleButton()
        INDsbRemoveCenter.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        INDsbRemoveCenter.Appearance.Options.UseFont = True
        INDsbRemoveCenter.MaximumSize = New System.Drawing.Size(100, 20)
        INDsbRemoveCenter.MinimumSize = New System.Drawing.Size(100, 20)
        INDsbRemoveCenter.Name = "INDsbRemoveCenter"
        INDsbRemoveCenter.Size = New System.Drawing.Size(100, 20)
        INDsbRemoveCenter.Text = "Remover Centros"
        AddHandler INDsbRemoveCenter.Click, AddressOf INDsbRemoveCenter_Click
        If INDgcProductionCenter IsNot Nothing Then
            Me.INDgcProductionCenter.Controls.Add(INDsbRemoveCenter)
            INDsbRemoveCenter.Location = New Point((INDgcProductionCenter.Width - INDsbRemoveCenter.Size.Width - INDsbLoadCenter.Width - 10), 5)
            INDsbRemoveCenter.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            INDsbRemoveCenter.BringToFront()
        End If

        INDsbRemoveCenter.Visible = False
        INDsbLoadCenter.Visible = False
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
            If MultipleBase = Presentation.InteropCost.FrmGeneralExpenses.eMultipleBase.DistributionA AndAlso _distributionBase Is Nothing Then
                If FormParentName = eParent.GeneralExpenses Then
                    INDgleDistributionType.EditValue = 1
                Else
                    INDgleDistributionType.EditValue = 2
                End If

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
                _distributionBase = New DistributionBase()
            End If
        ElseIf FormParentName = eParent.DistributionSecondary Then
            If _distributionSecondaryBase Is Nothing Then
                _distributionSecondaryBase = New DistributionSecondaryBase()
            End If
            If INDclbCosteoOptions.Items.Count < 6 Then
                INDclbCosteoOptions.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("6", "Valor Primo"))
                INDclbCosteoOptions.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("7", "Valor Facturado"))
            End If
        End If

        INDgleDistributionOption.EditValue = 2
        _stateOpenPopUpProductionCenter = False
        InitializeProductionCenter()
        INDlcRoot.EndUpdate()
        Me.ResumeLayout()
    End Sub

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
    ''' Crea los tipos de distribución
    ''' </summary>
    Private Sub CreateDistributionType()
        _distributionType = New List(Of Tuple(Of Integer, String))()
        _distributionType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("DistributionTypeDirect", MODULE_NAME)))
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
        If _costClass = 1 Then ' si es fijo el tipo de costo
            _measureUnit.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("MeasureUnitValue", MODULE_NAME)))
        End If
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
    ''' Carga los datos en los controles
    ''' </summary>
    ''' <param name="distribBase">The distrib base.</param>
    Public Sub LoadDistributionBase(Optional distribBase As DistributionBase = Nothing, Optional distribSecondaryBase As DistributionSecondaryBase = Nothing)
        _distributionBase = distribBase
        _distributionSecondaryBase = distribSecondaryBase

        Dim distribution As Object
        If distribBase Is Nothing Then
            distribution = distribSecondaryBase
            If INDclbCosteoOptions.Items.Count < 6 Then
                INDclbCosteoOptions.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("6", "Valor Primo"))
                INDclbCosteoOptions.Items.Add(New DevExpress.XtraEditors.Controls.CheckedListBoxItem("7", "Valor Facturado"))
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
        If distribBase IsNot Nothing Then
            'If distribBase.DistributionBaseDetail IsNot Nothing Then
            '    For Each item As DistributionBaseDetail In distribBase.DistributionBaseDetail.ToList()
            '        ListDistributionBaseDetail.Where(Function(x) x.ProductionCenterId = item.ProductionCenterId).Cast(Of DistributionBaseDetail).FirstOrDefault().Quantity = item.Quantity
            '    Next
            'End If
        Else
            If distribSecondaryBase.DistributionSecondaryBaseDetail IsNot Nothing Then
                For Each item As DistributionSecondaryBaseDetail In distribSecondaryBase.DistributionSecondaryBaseDetail.ToList()
                    ListDistributionSecondaryBaseDetail.Where(Function(x) x.ProductionCenterId = item.ProductionCenterId).Cast(Of DistributionSecondaryBaseDetail).FirstOrDefault().Quantity = item.Quantity
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
    End Sub

    ''' <summary>
    ''' Validates the production center list.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateProductionCenterList() As ActionResult
        If FormParentName = eParent.GeneralExpenses Then
            If ListDistributionBaseDetail IsNot Nothing Then
                If MeasurementUnit = eMeasureUnit.Proporcion AndAlso ListDistributionBaseDetail.Sum(Function(x) x.Quantity) > 100 Then
                    Return New ActionResult With {.Message = ResourceManager.GetString("MaximumDistributionErrorSum", MODULE_NAME), .StateResult = False}
                End If
            End If
        ElseIf FormParentName = eParent.DistributionSecondary Then
            If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                If MeasurementUnit = eMeasureUnit.Proporcion AndAlso ListDistributionSecondaryBaseDetail.Sum(Function(x) x.Quantity) > 100 Then
                    Return New ActionResult With {.Message = ResourceManager.GetString("MaximumDistributionErrorSum", MODULE_NAME), .StateResult = False}
                End If
            End If
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Initializes the production center.
    ''' </summary>
    Private Sub InitializeProductionCenter()
        If FormParentName = eParent.GeneralExpenses Then
            INDrpProductionCenter.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
            INDsleProductionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatus(True)
        Else
            INDrpProductionCenter.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatusAndCenterType(True, 1)
            INDsleProductionCenter.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatusAndCenterType(True, 1)
            listProductionCenters = XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListProductionCenterByStatusAndCenterTypeXpCollection(True, 1)
        End If
    End Sub

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
        Select Case DistributionType
            Case eDistributionType.Calculated
                If INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If MeasurementUnit = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), INDliMeasureUnit.Text))
                    End If

                End If

                If FormParentName = eParent.GeneralExpenses Then
                    If ListDistributionBaseDetail Is Nothing OrElse ListDistributionBaseDetail.Where(Function(x) x.Quantity <> 0).Count() = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centros de Producción"))
                    End If
                ElseIf FormParentName = eParent.DistributionSecondary Then
                    If ListDistributionSecondaryBaseDetail Is Nothing OrElse ListDistributionSecondaryBaseDetail.Where(Function(x) x.Quantity <> 0).Count() = 0 Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centros de Producción"))
                    End If
                End If

                If Not ValidateProductionCenterList().StateResult Then
                    errorList.AppendLine(ValidateProductionCenterList().Message)
                End If
            Case eDistributionType.Searched
                'If MeasurementUnit = 0 Then
                '    errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), INDliMeasureUnit.Text))
                'End If
                If FormParentName = eParent.GeneralExpenses Then
                    If ListDistributionBaseDetail Is Nothing Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centros de Producción"))
                    End If
                ElseIf FormParentName = eParent.DistributionSecondary Then
                    If ListDistributionSecondaryBaseDetail Is Nothing Then
                        errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centros de Producción"))
                    End If
                End If
        End Select
        If INDLcgMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDGvCostDistributionBaseMeasurementUnit.RowCount = 0 Then
                errorList.AppendLine("Se debe agregar minimo una unidad de medida")
            End If
        End If
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
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ProductionCenter", MODULE_NAME)))
        Else
            If FormParentName = eParent.GeneralExpenses Then
                Dim errors As New StringBuilder
                If INDsleAccountId.EditValue Is Nothing Then
                    errors.AppendLine("Seleccione una cuenta")
                End If
                If INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If INDSLeCostCenterConcept.EditValue Is Nothing Then
                        errors.AppendLine("Seleccione un centro de costo")
                    End If
                End If
                If ListDistributionBaseDetail IsNot Nothing AndAlso ListDistributionBaseDetail.Where(Function(x) x.ProductionCenterId = CInt(INDsleProductionCenter.EditValue)).Count() > 0 Then
                    errors.AppendLine("El centro de produccion ya se encuentra agregado")
                End If
                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    Return False
                End If
            ElseIf FormParentName = eParent.DistributionSecondary Then
                If ListDistributionSecondaryBaseDetail IsNot Nothing AndAlso ListDistributionSecondaryBaseDetail.Where(Function(x) x.ProductionCenterId = CType(INDsleProductionCenter.EditValue, Integer)).Count() > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ProductionCenterRepeat", MODULE_NAME)
                    Return False
                End If
            End If
        End If
        If INDliDistributedAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If DistributionType <> eDistributionType.Searched AndAlso (INDspnDistributAmount.EditValue Is Nothing OrElse INDspnDistributAmount.EditValue = 0) Then
                errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("Quantity", MODULE_NAME)))
            End If
        End If
        If FormParentName = eParent.GeneralExpenses Then
            If ListDistributionBaseDetail IsNot Nothing Then
                If MeasurementUnit = eMeasureUnit.Proporcion AndAlso (ListDistributionBaseDetail.Sum(Function(x) x.Quantity) + CType(INDspnDistributAmount.EditValue, Decimal)) > 100 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MaximumDistributionErrorSum", MODULE_NAME)
                    Return False
                End If
            End If
        ElseIf FormParentName = eParent.DistributionSecondary Then
            If ListDistributionSecondaryBaseDetail IsNot Nothing Then
                If MeasurementUnit = eMeasureUnit.Proporcion AndAlso (ListDistributionSecondaryBaseDetail.Sum(Function(x) x.Quantity) + CType(INDspnDistributAmount.EditValue, Decimal)) > 100 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MaximumDistributionErrorSum", MODULE_NAME)
                    Return False
                End If
            End If
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("EmptyFields", "Treasury"), errorList.ToString())
            Return False
        Else
            Return True
        End If
    End Function
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

    ''' <summary>
    ''' listado de las unidades de medida
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listDistributionBaseMeasurementUnit As List(Of DistributionBaseMeasurementUnit)
    Dim _listDistributionSecondaryMeasurementUnit As List(Of DistributionSecondaryMeasurementUnit)

    Private Sub INDsleCancellationCostMainAccountId_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountId.QueryPopUp
        If INDsleAccountId.Properties.DataSource Is Nothing Then
            Using model As New MGeneralExpenses(Me.Tag)
                INDsleAccountId.Properties.DataSource = model.ListAccounts()
            End Using
        End If
    End Sub

    Private Sub INDsleCancellationCostMainAccountId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountId.EditValueChanged
        If INDsleAccountId.EditValue IsNot Nothing Then
            Using model As New MGeneralExpenses(Me.Tag)
                Dim account = model.GetCTNCUENTAById(INDsleAccountId.EditValue)
                If account.CUEMANCEN Then
                    INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDSLeCostCenterConcept.EditValue = Nothing
                    INDSLeCostCenterConcept.Properties.DataSource = Nothing
                    INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using
        End If
    End Sub

    Private Sub INDsleProductionCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProductionCenter.EditValueChanged
        INDSLeCostCenterConcept.EditValue = Nothing
        INDSLeCostCenterConcept.Properties.DataSource = Nothing
    End Sub

    Private Sub INDSLeCostCenterConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSLeCostCenterConcept.QueryPopUp
        If INDSLeCostCenterConcept.Properties.DataSource Is Nothing Then
            Using model As New MGeneralExpenses(Me.Tag)
                Dim listCostCenter = model.ListProductionCenterCostCenterByProductionCenterId(INDsleProductionCenter.EditValue)
                Dim listId = (From i In listCostCenter Select i.CostCenterId).Distinct().ToList()
                INDSLeCostCenterConcept.Properties.DataSource = model.ListCostCenterDinamicByListId(listId)
            End Using
        End If
    End Sub

    Private Sub INDsleMeasureUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleMeasureUnit.QueryPopUp
        If INDsleMeasureUnit.Properties.DataSource Is Nothing Then
            Using model As New MGeneralExpenses(Me.Tag)
                INDsleMeasureUnit.Properties.DataSource = model.ListMeasureUnitByType()
            End Using
        End If
    End Sub


    Private Sub INDBtnAddMeasureUnit_Click(sender As Object, e As EventArgs) Handles INDBtnAddMeasureUnit.Click
        If INDsleMeasureUnit.EditValue IsNot Nothing Then

            If FormParentName = eParent.GeneralExpenses Then

                If _listDistributionBaseMeasurementUnit Is Nothing Then
                    _listDistributionBaseMeasurementUnit = New List(Of DistributionBaseMeasurementUnit)
                End If
                Dim measureUnitAdded = _listDistributionBaseMeasurementUnit.Find(Function(x) x.MeasurementUnitId = INDsleMeasureUnit.EditValue)
                If measureUnitAdded IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya esta agregada la unidad de medida"
                    Exit Sub
                End If
                Dim DistributionBaseMeasurementUnit As New DistributionBaseMeasurementUnit
                DistributionBaseMeasurementUnit.MeasurementUnitId = INDsleMeasureUnit.EditValue
                DistributionBaseMeasurementUnit.CodeNameMeasureUnit = INDsleMeasureUnit.Text
                _listDistributionBaseMeasurementUnit.Add(DistributionBaseMeasurementUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionBaseMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
                INDsleMeasureUnit.EditValue = Nothing

            Else

                If _listDistributionSecondaryMeasurementUnit Is Nothing Then
                    _listDistributionSecondaryMeasurementUnit = New List(Of DistributionSecondaryMeasurementUnit)
                End If
                Dim measureUnitAdded = _listDistributionSecondaryMeasurementUnit.Find(Function(x) x.MeasurementUnitId = INDsleMeasureUnit.EditValue)
                If measureUnitAdded IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya esta agregada la unidad de medida"
                    Exit Sub
                End If
                Dim DistributionSecondaryMeasurementUnit As New DistributionSecondaryMeasurementUnit
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

    Private Sub INDsleMeasureUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasureUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("300", Nothing, True)
        End If
    End Sub


    Dim _listDistributionBaseMeasurementUnitDelete As List(Of DistributionBaseMeasurementUnit)
    Dim _listDistributionSecondaryMeasurementUnitDelete As List(Of DistributionSecondaryMeasurementUnit)
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If FormParentName = eParent.GeneralExpenses Then
                Dim measureUnit = DirectCast(INDGvCostDistributionBaseMeasurementUnit.GetFocusedRow, DistributionBaseMeasurementUnit)
                'If measureUnit.Id > 0 Then
                '    _distributionBase.DistributionBaseMeasurementUnit.Where(Function(x) x.Id = measureUnit.Id).FirstOrDefault.MarkAsDeleted()
                'End If
                If _listDistributionBaseMeasurementUnitDelete Is Nothing Then
                    _listDistributionBaseMeasurementUnitDelete = New List(Of DistributionBaseMeasurementUnit)()
                End If
                _listDistributionBaseMeasurementUnit.Remove(measureUnit)
                _listDistributionBaseMeasurementUnitDelete.Add(measureUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionBaseMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
            Else
                Dim measureUnit = DirectCast(INDGvCostDistributionBaseMeasurementUnit.GetFocusedRow, DistributionSecondaryMeasurementUnit)
                'If measureUnit.Id > 0 Then
                '    _distributionSecondaryBase.DistributionSecondaryMeasurementUnit.Where(Function(x) x.Id = measureUnit.Id).FirstOrDefault.MarkAsDeleted()
                'End If
                If _listDistributionSecondaryMeasurementUnitDelete Is Nothing Then
                    _listDistributionSecondaryMeasurementUnitDelete = New List(Of DistributionSecondaryMeasurementUnit)()
                End If
                _listDistributionSecondaryMeasurementUnit.Remove(measureUnit)
                _listDistributionSecondaryMeasurementUnitDelete.Add(measureUnit)
                INDGcCostDistributionBaseMeasurementUnit.DataSource = _listDistributionSecondaryMeasurementUnit
                INDGcCostDistributionBaseMeasurementUnit.RefreshDataSource()
            End If
        End If
    End Sub

    Private Sub PopUpDistributionBase_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If INDgleDistributionType.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub INDsbAddAllProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddAllProductionCenter.Click
        If FormParentName = eParent.DistributionSecondary Then
            InitializeProductionCenter()
            If ListDistributionSecondaryBaseDetail Is Nothing Then
                ListDistributionSecondaryBaseDetail = New List(Of DistributionSecondaryBaseDetail)()
            End If
            For Each p As ProductionCenterXpo In listProductionCenters
                If Not ListDistributionSecondaryBaseDetail.Any(Function(x) x.ProductionCenterId = p.Id) Then
                    Dim _productCenter As New DistributionSecondaryBaseDetail()
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

End Class