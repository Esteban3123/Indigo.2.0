Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDashboardAlertsPGP
    Inherits FormBase

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.INDLcDashboardAlertsPGP = New DevExpress.XtraLayout.LayoutControl()
        Me.INDLblServiceControlsValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblServiceControlsText = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblServiceOrdersValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblServiceOrdersText = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblRequestsValue = New DevExpress.XtraEditors.LabelControl()
        Me.INDLblRequestsText = New DevExpress.XtraEditors.LabelControl()
        Me.INDGcServiceControls = New DevExpress.XtraGrid.GridControl()
        Me.INDGvServiceControls = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvServiceControls_GroupDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_Patient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_AdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_CareCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_FunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_Professional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_Diagnostic = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_Observations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_CUPSEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_CUPSEntityDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_CMEValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_TotalCME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_TotalServiceValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_ServiceOrderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_ServiceControlNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceControls_InvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDGcServiceOrders = New DevExpress.XtraGrid.GridControl()
        Me.INDGvServiceOrders = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvServiceOrders_GroupDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_Patient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_AdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_CareCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_FunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_Professional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_Diagnostic = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_Observations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_CUPSEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_CUPSEntityDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_CMEValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_TotalCME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_TotalServiceValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvServiceOrders_ServiceOrderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPccFilter = New DevExpress.XtraEditors.PopupContainerControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDDeDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.INDBtnAplicar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleCareGroup = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvCareGroup = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvCareGroup_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvCareGroup_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciCareGroup = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDPceFilters = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDGcRequests = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRequests = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvRequests_GroupDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Date = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Patient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_AdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Folio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_CareCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_FunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Professional = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Diagnostic = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Observations = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_CUPSEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_CUPSEntityDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_CMEValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvRequests_TotalCME = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgDashboardAlertsPGP = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgFilter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDTcgDashboardAlertsPGP = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgRequests = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciRequests = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRequestsText = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRequestsValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgServiceOrders = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciServiceOrders = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciServiceOrdersText = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciServiceOrdersValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgServiceControls = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciServiceControls = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciServiceControlsText = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciServiceControlsValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.INDPopMenuActions1 = New DevExpress.XtraBars.PopupMenu(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcDashboardAlertsPGP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcDashboardAlertsPGP.SuspendLayout()
        CType(Me.INDGcServiceControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvServiceControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcServiceOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvServiceOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccFilter.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDDeDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleCareGroup.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCareGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPceFilters.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgDashboardAlertsPGP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgDashboardAlertsPGP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequestsText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequestsValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgServiceOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceOrdersText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceOrdersValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgServiceControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceControlsText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciServiceControlsValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcDashboardAlertsPGP)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1252, 583)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1252, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1252, 130)
        '
        'INDLcDashboardAlertsPGP
        '
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDLblServiceControlsValue)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDLblServiceControlsText)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDLblServiceOrdersValue)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDLblServiceOrdersText)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDLblRequestsValue)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDLblRequestsText)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDGcServiceControls)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDGcServiceOrders)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDPccFilter)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDPceFilters)
        Me.INDLcDashboardAlertsPGP.Controls.Add(Me.INDGcRequests)
        Me.INDLcDashboardAlertsPGP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcDashboardAlertsPGP.Location = New System.Drawing.Point(2, 7)
        Me.INDLcDashboardAlertsPGP.Name = "INDLcDashboardAlertsPGP"
        Me.INDLcDashboardAlertsPGP.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2352, 399, 574, 569)
        Me.INDLcDashboardAlertsPGP.Root = Me.INDLcgDashboardAlertsPGP
        Me.INDLcDashboardAlertsPGP.Size = New System.Drawing.Size(1248, 574)
        Me.INDLcDashboardAlertsPGP.TabIndex = 5
        Me.INDLcDashboardAlertsPGP.Text = "LayoutControl2"
        '
        'INDLblServiceControlsValue
        '
        Me.INDLblServiceControlsValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLblServiceControlsValue.Appearance.Options.UseFont = True
        Me.INDLblServiceControlsValue.Location = New System.Drawing.Point(84, 143)
        Me.INDLblServiceControlsValue.Name = "INDLblServiceControlsValue"
        Me.INDLblServiceControlsValue.Size = New System.Drawing.Size(27, 28)
        Me.INDLblServiceControlsValue.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDLblServiceControlsValue.TabIndex = 11
        Me.INDLblServiceControlsValue.Text = "$ 0"
        '
        'INDLblServiceControlsText
        '
        Me.INDLblServiceControlsText.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLblServiceControlsText.Appearance.Options.UseFont = True
        Me.INDLblServiceControlsText.Location = New System.Drawing.Point(14, 143)
        Me.INDLblServiceControlsText.Name = "INDLblServiceControlsText"
        Me.INDLblServiceControlsText.Size = New System.Drawing.Size(66, 28)
        Me.INDLblServiceControlsText.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDLblServiceControlsText.TabIndex = 10
        Me.INDLblServiceControlsText.Text = "TOTAL: "
        '
        'INDLblServiceOrdersValue
        '
        Me.INDLblServiceOrdersValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLblServiceOrdersValue.Appearance.Options.UseFont = True
        Me.INDLblServiceOrdersValue.Location = New System.Drawing.Point(84, 143)
        Me.INDLblServiceOrdersValue.Name = "INDLblServiceOrdersValue"
        Me.INDLblServiceOrdersValue.Size = New System.Drawing.Size(1150, 28)
        Me.INDLblServiceOrdersValue.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDLblServiceOrdersValue.TabIndex = 9
        Me.INDLblServiceOrdersValue.Text = "$ 0"
        '
        'INDLblServiceOrdersText
        '
        Me.INDLblServiceOrdersText.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLblServiceOrdersText.Appearance.Options.UseFont = True
        Me.INDLblServiceOrdersText.Location = New System.Drawing.Point(14, 143)
        Me.INDLblServiceOrdersText.Name = "INDLblServiceOrdersText"
        Me.INDLblServiceOrdersText.Size = New System.Drawing.Size(66, 28)
        Me.INDLblServiceOrdersText.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDLblServiceOrdersText.TabIndex = 8
        Me.INDLblServiceOrdersText.Text = "TOTAL: "
        '
        'INDLblRequestsValue
        '
        Me.INDLblRequestsValue.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLblRequestsValue.Appearance.Options.UseFont = True
        Me.INDLblRequestsValue.Location = New System.Drawing.Point(84, 143)
        Me.INDLblRequestsValue.Name = "INDLblRequestsValue"
        Me.INDLblRequestsValue.Size = New System.Drawing.Size(1150, 28)
        Me.INDLblRequestsValue.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDLblRequestsValue.TabIndex = 7
        Me.INDLblRequestsValue.Text = "$ 0"
        '
        'INDLblRequestsText
        '
        Me.INDLblRequestsText.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.INDLblRequestsText.Appearance.Options.UseFont = True
        Me.INDLblRequestsText.Location = New System.Drawing.Point(14, 143)
        Me.INDLblRequestsText.Name = "INDLblRequestsText"
        Me.INDLblRequestsText.Size = New System.Drawing.Size(66, 28)
        Me.INDLblRequestsText.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDLblRequestsText.TabIndex = 6
        Me.INDLblRequestsText.Text = "TOTAL: "
        '
        'INDGcServiceControls
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcServiceControls, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcServiceControls, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcServiceControls, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcServiceControls, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcServiceControls, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcServiceControls, False)
        Me.INDGcServiceControls.Location = New System.Drawing.Point(14, 175)
        Me.INDGcServiceControls.MainView = Me.INDGvServiceControls
        Me.INDGcServiceControls.MenuManager = Me.BarManager1
        Me.INDGcServiceControls.Name = "INDGcServiceControls"
        Me.INDGcServiceControls.Size = New System.Drawing.Size(1220, 385)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcServiceControls, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcServiceControls.TabIndex = 5
        Me.INDGcServiceControls.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvServiceControls})
        '
        'INDGvServiceControls
        '
        Me.INDGvServiceControls.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvServiceControls.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvServiceControls.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvServiceControls.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvServiceControls.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvServiceControls.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvServiceControls.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvServiceControls.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvServiceControls.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvServiceControls.Appearance.Row.Options.UseFont = True
        Me.INDGvServiceControls.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvServiceControls.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvServiceControls.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvServiceControls_GroupDescription, Me.INDGvServiceControls_Date, Me.INDGvServiceControls_Patient, Me.INDGvServiceControls_AdmissionNumber, Me.INDGvServiceControls_CareCenter, Me.INDGvServiceControls_FunctionalUnit, Me.INDGvServiceControls_Professional, Me.INDGvServiceControls_Diagnostic, Me.INDGvServiceControls_Observations, Me.INDGvServiceControls_CUPSEntity, Me.INDGvServiceControls_CUPSEntityDescription, Me.INDGvServiceControls_Quantity, Me.INDGvServiceControls_CMEValue, Me.INDGvServiceControls_TotalCME, Me.INDGvServiceControls_TotalServiceValue, Me.INDGvServiceControls_ServiceOrderCode, Me.INDGvServiceControls_ServiceControlNumber, Me.INDGvServiceControls_InvoiceNumber})
        Me.INDGvServiceControls.GridControl = Me.INDGcServiceControls
        Me.INDGvServiceControls.GroupCount = 1
        Me.INDGvServiceControls.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", Me.INDGvServiceControls_Quantity, "{0:N0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCME", Me.INDGvServiceControls_TotalCME, "{0:C0}")})
        Me.INDGvServiceControls.Name = "INDGvServiceControls"
        Me.INDGvServiceControls.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvServiceControls.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvServiceControls.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.INDGvServiceControls.OptionsView.ShowAutoFilterRow = True
        Me.INDGvServiceControls.OptionsView.ShowFooter = True
        Me.INDGvServiceControls.OptionsView.ShowGroupPanel = False
        Me.INDGvServiceControls.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvServiceControls_GroupDescription, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvServiceControls, False)
        '
        'INDGvServiceControls_GroupDescription
        '
        Me.INDGvServiceControls_GroupDescription.Caption = "Agrupador"
        Me.INDGvServiceControls_GroupDescription.FieldName = "GroupDescription"
        Me.INDGvServiceControls_GroupDescription.Name = "INDGvServiceControls_GroupDescription"
        Me.INDGvServiceControls_GroupDescription.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_GroupDescription.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_GroupDescription.Visible = True
        Me.INDGvServiceControls_GroupDescription.VisibleIndex = 0
        Me.INDGvServiceControls_GroupDescription.Width = 73
        '
        'INDGvServiceControls_Date
        '
        Me.INDGvServiceControls_Date.Caption = "Fecha"
        Me.INDGvServiceControls_Date.FieldName = "RequestDate"
        Me.INDGvServiceControls_Date.Name = "INDGvServiceControls_Date"
        Me.INDGvServiceControls_Date.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_Date.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_Date.Visible = True
        Me.INDGvServiceControls_Date.VisibleIndex = 0
        Me.INDGvServiceControls_Date.Width = 100
        '
        'INDGvServiceControls_Patient
        '
        Me.INDGvServiceControls_Patient.Caption = "Paciente"
        Me.INDGvServiceControls_Patient.FieldName = "Patient"
        Me.INDGvServiceControls_Patient.Name = "INDGvServiceControls_Patient"
        Me.INDGvServiceControls_Patient.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_Patient.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_Patient.Visible = True
        Me.INDGvServiceControls_Patient.VisibleIndex = 1
        Me.INDGvServiceControls_Patient.Width = 265
        '
        'INDGvServiceControls_AdmissionNumber
        '
        Me.INDGvServiceControls_AdmissionNumber.Caption = "Ingreso"
        Me.INDGvServiceControls_AdmissionNumber.FieldName = "AdmissionNumber"
        Me.INDGvServiceControls_AdmissionNumber.Name = "INDGvServiceControls_AdmissionNumber"
        Me.INDGvServiceControls_AdmissionNumber.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_AdmissionNumber.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_AdmissionNumber.Visible = True
        Me.INDGvServiceControls_AdmissionNumber.VisibleIndex = 2
        Me.INDGvServiceControls_AdmissionNumber.Width = 100
        '
        'INDGvServiceControls_CareCenter
        '
        Me.INDGvServiceControls_CareCenter.Caption = "Centro Atención"
        Me.INDGvServiceControls_CareCenter.FieldName = "CareCenter"
        Me.INDGvServiceControls_CareCenter.Name = "INDGvServiceControls_CareCenter"
        Me.INDGvServiceControls_CareCenter.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_CareCenter.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_CareCenter.Width = 51
        '
        'INDGvServiceControls_FunctionalUnit
        '
        Me.INDGvServiceControls_FunctionalUnit.Caption = "Unidad Funcional"
        Me.INDGvServiceControls_FunctionalUnit.FieldName = "FunctionalUnit"
        Me.INDGvServiceControls_FunctionalUnit.Name = "INDGvServiceControls_FunctionalUnit"
        Me.INDGvServiceControls_FunctionalUnit.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_FunctionalUnit.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_FunctionalUnit.Visible = True
        Me.INDGvServiceControls_FunctionalUnit.VisibleIndex = 3
        Me.INDGvServiceControls_FunctionalUnit.Width = 200
        '
        'INDGvServiceControls_Professional
        '
        Me.INDGvServiceControls_Professional.Caption = "Medico Solicitante"
        Me.INDGvServiceControls_Professional.FieldName = "Professional"
        Me.INDGvServiceControls_Professional.Name = "INDGvServiceControls_Professional"
        Me.INDGvServiceControls_Professional.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_Professional.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_Professional.Width = 42
        '
        'INDGvServiceControls_Diagnostic
        '
        Me.INDGvServiceControls_Diagnostic.Caption = "Diagnostico"
        Me.INDGvServiceControls_Diagnostic.FieldName = "Diagnostic"
        Me.INDGvServiceControls_Diagnostic.Name = "INDGvServiceControls_Diagnostic"
        Me.INDGvServiceControls_Diagnostic.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_Diagnostic.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_Diagnostic.Width = 42
        '
        'INDGvServiceControls_Observations
        '
        Me.INDGvServiceControls_Observations.Caption = "Observaciones"
        Me.INDGvServiceControls_Observations.FieldName = "Observations"
        Me.INDGvServiceControls_Observations.Name = "INDGvServiceControls_Observations"
        Me.INDGvServiceControls_Observations.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_Observations.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_Observations.Width = 42
        '
        'INDGvServiceControls_CUPSEntity
        '
        Me.INDGvServiceControls_CUPSEntity.Caption = "Servicio"
        Me.INDGvServiceControls_CUPSEntity.FieldName = "CUPSEntityCodeName"
        Me.INDGvServiceControls_CUPSEntity.Name = "INDGvServiceControls_CUPSEntity"
        Me.INDGvServiceControls_CUPSEntity.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_CUPSEntity.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_CUPSEntity.Visible = True
        Me.INDGvServiceControls_CUPSEntity.VisibleIndex = 4
        Me.INDGvServiceControls_CUPSEntity.Width = 260
        '
        'INDGvServiceControls_CUPSEntityDescription
        '
        Me.INDGvServiceControls_CUPSEntityDescription.Caption = "Descripción Relacionada"
        Me.INDGvServiceControls_CUPSEntityDescription.FieldName = "DescriptionCodeName"
        Me.INDGvServiceControls_CUPSEntityDescription.Name = "INDGvServiceControls_CUPSEntityDescription"
        Me.INDGvServiceControls_CUPSEntityDescription.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_CUPSEntityDescription.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_CUPSEntityDescription.Width = 26
        '
        'INDGvServiceControls_Quantity
        '
        Me.INDGvServiceControls_Quantity.Caption = "Cantidad"
        Me.INDGvServiceControls_Quantity.DisplayFormat.FormatString = "N0"
        Me.INDGvServiceControls_Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceControls_Quantity.FieldName = "Quantity"
        Me.INDGvServiceControls_Quantity.Name = "INDGvServiceControls_Quantity"
        Me.INDGvServiceControls_Quantity.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_Quantity.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_Quantity.Visible = True
        Me.INDGvServiceControls_Quantity.VisibleIndex = 5
        Me.INDGvServiceControls_Quantity.Width = 90
        '
        'INDGvServiceControls_CMEValue
        '
        Me.INDGvServiceControls_CMEValue.Caption = "Valor CME"
        Me.INDGvServiceControls_CMEValue.DisplayFormat.FormatString = "C0"
        Me.INDGvServiceControls_CMEValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceControls_CMEValue.FieldName = "CMEValue"
        Me.INDGvServiceControls_CMEValue.Name = "INDGvServiceControls_CMEValue"
        Me.INDGvServiceControls_CMEValue.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_CMEValue.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_CMEValue.Visible = True
        Me.INDGvServiceControls_CMEValue.VisibleIndex = 6
        Me.INDGvServiceControls_CMEValue.Width = 90
        '
        'INDGvServiceControls_TotalCME
        '
        Me.INDGvServiceControls_TotalCME.Caption = "Total CME"
        Me.INDGvServiceControls_TotalCME.DisplayFormat.FormatString = "C0"
        Me.INDGvServiceControls_TotalCME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceControls_TotalCME.FieldName = "TotalCME"
        Me.INDGvServiceControls_TotalCME.Name = "INDGvServiceControls_TotalCME"
        Me.INDGvServiceControls_TotalCME.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_TotalCME.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_TotalCME.Visible = True
        Me.INDGvServiceControls_TotalCME.VisibleIndex = 7
        Me.INDGvServiceControls_TotalCME.Width = 90
        '
        'INDGvServiceControls_TotalServiceValue
        '
        Me.INDGvServiceControls_TotalServiceValue.Caption = "Total Servicio"
        Me.INDGvServiceControls_TotalServiceValue.DisplayFormat.FormatString = "C0"
        Me.INDGvServiceControls_TotalServiceValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceControls_TotalServiceValue.FieldName = "TotalServiceValue"
        Me.INDGvServiceControls_TotalServiceValue.Name = "INDGvServiceControls_TotalServiceValue"
        Me.INDGvServiceControls_TotalServiceValue.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_TotalServiceValue.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_TotalServiceValue.Width = 20
        '
        'INDGvServiceControls_ServiceOrderCode
        '
        Me.INDGvServiceControls_ServiceOrderCode.Caption = "Orden de Servicio"
        Me.INDGvServiceControls_ServiceOrderCode.FieldName = "ServiceOrderCode"
        Me.INDGvServiceControls_ServiceOrderCode.Name = "INDGvServiceControls_ServiceOrderCode"
        Me.INDGvServiceControls_ServiceOrderCode.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_ServiceOrderCode.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_ServiceOrderCode.Width = 20
        '
        'INDGvServiceControls_ServiceControlNumber
        '
        Me.INDGvServiceControls_ServiceControlNumber.Caption = "Control de Servicio"
        Me.INDGvServiceControls_ServiceControlNumber.FieldName = "ServiceControlNumber"
        Me.INDGvServiceControls_ServiceControlNumber.Name = "INDGvServiceControls_ServiceControlNumber"
        Me.INDGvServiceControls_ServiceControlNumber.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_ServiceControlNumber.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_ServiceControlNumber.Width = 20
        '
        'INDGvServiceControls_InvoiceNumber
        '
        Me.INDGvServiceControls_InvoiceNumber.Caption = "Factura"
        Me.INDGvServiceControls_InvoiceNumber.FieldName = "InvoiceNumber"
        Me.INDGvServiceControls_InvoiceNumber.Name = "INDGvServiceControls_InvoiceNumber"
        Me.INDGvServiceControls_InvoiceNumber.OptionsColumn.AllowEdit = False
        Me.INDGvServiceControls_InvoiceNumber.OptionsColumn.AllowFocus = False
        Me.INDGvServiceControls_InvoiceNumber.Width = 20
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.BarDockControl5)
        Me.BarManager1.DockControls.Add(Me.BarDockControl6)
        Me.BarManager1.DockControls.Add(Me.BarDockControl7)
        Me.BarManager1.DockControls.Add(Me.BarDockControl8)
        Me.BarManager1.Form = Me
        Me.BarManager1.MaxItemId = 34
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        Me.BarDockControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl5.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl5.Manager = Me.BarManager1
        Me.BarDockControl5.Size = New System.Drawing.Size(1252, 0)
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        Me.BarDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl6.Location = New System.Drawing.Point(0, 718)
        Me.BarDockControl6.Manager = Me.BarManager1
        Me.BarDockControl6.Size = New System.Drawing.Size(1252, 0)
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        Me.BarDockControl7.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl7.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl7.Manager = Me.BarManager1
        Me.BarDockControl7.Size = New System.Drawing.Size(0, 713)
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        Me.BarDockControl8.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl8.Location = New System.Drawing.Point(1252, 5)
        Me.BarDockControl8.Manager = Me.BarManager1
        Me.BarDockControl8.Size = New System.Drawing.Size(0, 713)
        '
        'INDGcServiceOrders
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcServiceOrders, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcServiceOrders, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcServiceOrders, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcServiceOrders, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcServiceOrders, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcServiceOrders, False)
        Me.INDGcServiceOrders.Location = New System.Drawing.Point(14, 175)
        Me.INDGcServiceOrders.MainView = Me.INDGvServiceOrders
        Me.INDGcServiceOrders.MenuManager = Me.BarManager1
        Me.INDGcServiceOrders.Name = "INDGcServiceOrders"
        Me.INDGcServiceOrders.Size = New System.Drawing.Size(1220, 385)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcServiceOrders, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcServiceOrders.TabIndex = 4
        Me.INDGcServiceOrders.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvServiceOrders})
        '
        'INDGvServiceOrders
        '
        Me.INDGvServiceOrders.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvServiceOrders.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvServiceOrders.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvServiceOrders.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvServiceOrders.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvServiceOrders.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvServiceOrders.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvServiceOrders.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvServiceOrders.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvServiceOrders.Appearance.Row.Options.UseFont = True
        Me.INDGvServiceOrders.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvServiceOrders.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvServiceOrders.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvServiceOrders_GroupDescription, Me.INDGvServiceOrders_Date, Me.INDGvServiceOrders_Patient, Me.INDGvServiceOrders_AdmissionNumber, Me.INDGvServiceOrders_CareCenter, Me.INDGvServiceOrders_FunctionalUnit, Me.INDGvServiceOrders_Professional, Me.INDGvServiceOrders_Diagnostic, Me.INDGvServiceOrders_Observations, Me.INDGvServiceOrders_CUPSEntity, Me.INDGvServiceOrders_CUPSEntityDescription, Me.INDGvServiceOrders_Quantity, Me.INDGvServiceOrders_CMEValue, Me.INDGvServiceOrders_TotalCME, Me.INDGvServiceOrders_TotalServiceValue, Me.INDGvServiceOrders_ServiceOrderCode})
        Me.INDGvServiceOrders.GridControl = Me.INDGcServiceOrders
        Me.INDGvServiceOrders.GroupCount = 1
        Me.INDGvServiceOrders.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", Me.INDGvServiceOrders_Quantity, "{0:N0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCME", Me.INDGvServiceOrders_TotalCME, "{0:C0}")})
        Me.INDGvServiceOrders.Name = "INDGvServiceOrders"
        Me.INDGvServiceOrders.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvServiceOrders.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvServiceOrders.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.INDGvServiceOrders.OptionsView.ShowAutoFilterRow = True
        Me.INDGvServiceOrders.OptionsView.ShowFooter = True
        Me.INDGvServiceOrders.OptionsView.ShowGroupPanel = False
        Me.INDGvServiceOrders.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvServiceOrders_GroupDescription, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvServiceOrders, False)
        '
        'INDGvServiceOrders_GroupDescription
        '
        Me.INDGvServiceOrders_GroupDescription.Caption = "Agrupador"
        Me.INDGvServiceOrders_GroupDescription.FieldName = "GroupDescription"
        Me.INDGvServiceOrders_GroupDescription.Name = "INDGvServiceOrders_GroupDescription"
        Me.INDGvServiceOrders_GroupDescription.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_GroupDescription.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_GroupDescription.Visible = True
        Me.INDGvServiceOrders_GroupDescription.VisibleIndex = 0
        '
        'INDGvServiceOrders_Date
        '
        Me.INDGvServiceOrders_Date.Caption = "Fecha"
        Me.INDGvServiceOrders_Date.FieldName = "RequestDate"
        Me.INDGvServiceOrders_Date.Name = "INDGvServiceOrders_Date"
        Me.INDGvServiceOrders_Date.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_Date.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_Date.Visible = True
        Me.INDGvServiceOrders_Date.VisibleIndex = 0
        Me.INDGvServiceOrders_Date.Width = 100
        '
        'INDGvServiceOrders_Patient
        '
        Me.INDGvServiceOrders_Patient.Caption = "Paciente"
        Me.INDGvServiceOrders_Patient.FieldName = "Patient"
        Me.INDGvServiceOrders_Patient.Name = "INDGvServiceOrders_Patient"
        Me.INDGvServiceOrders_Patient.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_Patient.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_Patient.Visible = True
        Me.INDGvServiceOrders_Patient.VisibleIndex = 1
        Me.INDGvServiceOrders_Patient.Width = 265
        '
        'INDGvServiceOrders_AdmissionNumber
        '
        Me.INDGvServiceOrders_AdmissionNumber.Caption = "Ingreso"
        Me.INDGvServiceOrders_AdmissionNumber.FieldName = "AdmissionNumber"
        Me.INDGvServiceOrders_AdmissionNumber.Name = "INDGvServiceOrders_AdmissionNumber"
        Me.INDGvServiceOrders_AdmissionNumber.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_AdmissionNumber.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_AdmissionNumber.Visible = True
        Me.INDGvServiceOrders_AdmissionNumber.VisibleIndex = 2
        Me.INDGvServiceOrders_AdmissionNumber.Width = 100
        '
        'INDGvServiceOrders_CareCenter
        '
        Me.INDGvServiceOrders_CareCenter.Caption = "Centro Atención"
        Me.INDGvServiceOrders_CareCenter.FieldName = "CareCenter"
        Me.INDGvServiceOrders_CareCenter.Name = "INDGvServiceOrders_CareCenter"
        Me.INDGvServiceOrders_CareCenter.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_CareCenter.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_CareCenter.Width = 62
        '
        'INDGvServiceOrders_FunctionalUnit
        '
        Me.INDGvServiceOrders_FunctionalUnit.Caption = "Unidad Funcional"
        Me.INDGvServiceOrders_FunctionalUnit.FieldName = "FunctionalUnit"
        Me.INDGvServiceOrders_FunctionalUnit.Name = "INDGvServiceOrders_FunctionalUnit"
        Me.INDGvServiceOrders_FunctionalUnit.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_FunctionalUnit.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_FunctionalUnit.Visible = True
        Me.INDGvServiceOrders_FunctionalUnit.VisibleIndex = 3
        Me.INDGvServiceOrders_FunctionalUnit.Width = 200
        '
        'INDGvServiceOrders_Professional
        '
        Me.INDGvServiceOrders_Professional.Caption = "Medico Solicitante"
        Me.INDGvServiceOrders_Professional.FieldName = "Professional"
        Me.INDGvServiceOrders_Professional.Name = "INDGvServiceOrders_Professional"
        Me.INDGvServiceOrders_Professional.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_Professional.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_Professional.Width = 53
        '
        'INDGvServiceOrders_Diagnostic
        '
        Me.INDGvServiceOrders_Diagnostic.Caption = "Diagnostico"
        Me.INDGvServiceOrders_Diagnostic.FieldName = "Diagnostic"
        Me.INDGvServiceOrders_Diagnostic.Name = "INDGvServiceOrders_Diagnostic"
        Me.INDGvServiceOrders_Diagnostic.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_Diagnostic.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_Diagnostic.Width = 53
        '
        'INDGvServiceOrders_Observations
        '
        Me.INDGvServiceOrders_Observations.Caption = "Observaciones"
        Me.INDGvServiceOrders_Observations.FieldName = "Observations"
        Me.INDGvServiceOrders_Observations.Name = "INDGvServiceOrders_Observations"
        Me.INDGvServiceOrders_Observations.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_Observations.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_Observations.Width = 53
        '
        'INDGvServiceOrders_CUPSEntity
        '
        Me.INDGvServiceOrders_CUPSEntity.Caption = "Servicio"
        Me.INDGvServiceOrders_CUPSEntity.FieldName = "CUPSEntityCodeName"
        Me.INDGvServiceOrders_CUPSEntity.Name = "INDGvServiceOrders_CUPSEntity"
        Me.INDGvServiceOrders_CUPSEntity.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_CUPSEntity.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_CUPSEntity.Visible = True
        Me.INDGvServiceOrders_CUPSEntity.VisibleIndex = 4
        Me.INDGvServiceOrders_CUPSEntity.Width = 260
        '
        'INDGvServiceOrders_CUPSEntityDescription
        '
        Me.INDGvServiceOrders_CUPSEntityDescription.Caption = "Descripción Relacionada"
        Me.INDGvServiceOrders_CUPSEntityDescription.FieldName = "DescriptionCodeName"
        Me.INDGvServiceOrders_CUPSEntityDescription.Name = "INDGvServiceOrders_CUPSEntityDescription"
        Me.INDGvServiceOrders_CUPSEntityDescription.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_CUPSEntityDescription.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_CUPSEntityDescription.Width = 34
        '
        'INDGvServiceOrders_Quantity
        '
        Me.INDGvServiceOrders_Quantity.Caption = "Cantidad"
        Me.INDGvServiceOrders_Quantity.DisplayFormat.FormatString = "N0"
        Me.INDGvServiceOrders_Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceOrders_Quantity.FieldName = "Quantity"
        Me.INDGvServiceOrders_Quantity.Name = "INDGvServiceOrders_Quantity"
        Me.INDGvServiceOrders_Quantity.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_Quantity.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_Quantity.Visible = True
        Me.INDGvServiceOrders_Quantity.VisibleIndex = 5
        Me.INDGvServiceOrders_Quantity.Width = 90
        '
        'INDGvServiceOrders_CMEValue
        '
        Me.INDGvServiceOrders_CMEValue.Caption = "Valor CME"
        Me.INDGvServiceOrders_CMEValue.DisplayFormat.FormatString = "C0"
        Me.INDGvServiceOrders_CMEValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceOrders_CMEValue.FieldName = "CMEValue"
        Me.INDGvServiceOrders_CMEValue.Name = "INDGvServiceOrders_CMEValue"
        Me.INDGvServiceOrders_CMEValue.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_CMEValue.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_CMEValue.Visible = True
        Me.INDGvServiceOrders_CMEValue.VisibleIndex = 6
        Me.INDGvServiceOrders_CMEValue.Width = 90
        '
        'INDGvServiceOrders_TotalCME
        '
        Me.INDGvServiceOrders_TotalCME.Caption = "Total CME"
        Me.INDGvServiceOrders_TotalCME.DisplayFormat.FormatString = "C0"
        Me.INDGvServiceOrders_TotalCME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceOrders_TotalCME.FieldName = "TotalCME"
        Me.INDGvServiceOrders_TotalCME.Name = "INDGvServiceOrders_TotalCME"
        Me.INDGvServiceOrders_TotalCME.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_TotalCME.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_TotalCME.Visible = True
        Me.INDGvServiceOrders_TotalCME.VisibleIndex = 7
        Me.INDGvServiceOrders_TotalCME.Width = 90
        '
        'INDGvServiceOrders_TotalServiceValue
        '
        Me.INDGvServiceOrders_TotalServiceValue.Caption = "Total Servicio"
        Me.INDGvServiceOrders_TotalServiceValue.DisplayFormat.FormatString = "C0"
        Me.INDGvServiceOrders_TotalServiceValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvServiceOrders_TotalServiceValue.FieldName = "TotalServiceValue"
        Me.INDGvServiceOrders_TotalServiceValue.Name = "INDGvServiceOrders_TotalServiceValue"
        Me.INDGvServiceOrders_TotalServiceValue.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_TotalServiceValue.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_TotalServiceValue.Width = 20
        '
        'INDGvServiceOrders_ServiceOrderCode
        '
        Me.INDGvServiceOrders_ServiceOrderCode.Caption = "Orden de Servicio"
        Me.INDGvServiceOrders_ServiceOrderCode.FieldName = "ServiceOrderCode"
        Me.INDGvServiceOrders_ServiceOrderCode.Name = "INDGvServiceOrders_ServiceOrderCode"
        Me.INDGvServiceOrders_ServiceOrderCode.OptionsColumn.AllowEdit = False
        Me.INDGvServiceOrders_ServiceOrderCode.OptionsColumn.AllowFocus = False
        Me.INDGvServiceOrders_ServiceOrderCode.Width = 20
        '
        'INDPccFilter
        '
        Me.INDPccFilter.Controls.Add(Me.PanelControl1)
        Me.INDPccFilter.Location = New System.Drawing.Point(28, 340)
        Me.INDPccFilter.Name = "INDPccFilter"
        Me.INDPccFilter.Size = New System.Drawing.Size(804, 155)
        Me.INDPccFilter.TabIndex = 3
        '
        'PanelControl1
        '
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.Controls.Add(Me.LayoutControl1)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(804, 155)
        Me.PanelControl1.TabIndex = 0
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDDeDateEnd)
        Me.LayoutControl1.Controls.Add(Me.INDDeDateStart)
        Me.LayoutControl1.Controls.Add(Me.INDBtnAplicar)
        Me.LayoutControl1.Controls.Add(Me.INDSleCareGroup)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2352, 399, 574, 569)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(804, 155)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDDeDateEnd
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeDateEnd, False)
        Me.INDDeDateEnd.EditValue = Nothing
        Me.INDDeDateEnd.EnterMoveNextControl = True
        Me.INDDeDateEnd.Location = New System.Drawing.Point(503, 75)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeDateEnd, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeDateEnd.MenuManager = Me.BarManager1
        Me.INDDeDateEnd.Name = "INDDeDateEnd"
        Me.INDDeDateEnd.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDDeDateEnd.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateEnd.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeDateEnd.Properties.Appearance.Options.UseFont = True
        Me.INDDeDateEnd.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateEnd.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateEnd.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeDateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeDateEnd.Size = New System.Drawing.Size(287, 28)
        Me.INDDeDateEnd.StyleController = Me.LayoutControl1
        Me.INDDeDateEnd.TabIndex = 10
        '
        'INDDeDateStart
        '
        Me.IndigoDate1.SetCampoObligatorio(Me.INDDeDateStart, False)
        Me.INDDeDateStart.EditValue = Nothing
        Me.INDDeDateStart.EnterMoveNextControl = True
        Me.INDDeDateStart.Location = New System.Drawing.Point(113, 75)
        Me.IndigoDate1.SetMascaraDate(Me.INDDeDateStart, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDDeDateStart.MenuManager = Me.BarManager1
        Me.INDDeDateStart.Name = "INDDeDateStart"
        Me.INDDeDateStart.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDDeDateStart.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateStart.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeDateStart.Properties.Appearance.Options.UseFont = True
        Me.INDDeDateStart.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeDateStart.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDDeDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeDateStart.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDDeDateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDDeDateStart.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDDeDateStart.Size = New System.Drawing.Size(287, 28)
        Me.INDDeDateStart.StyleController = Me.LayoutControl1
        Me.INDDeDateStart.TabIndex = 9
        '
        'INDBtnAplicar
        '
        Me.INDBtnAplicar.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDBtnAplicar.Appearance.Options.UseFont = True
        Me.INDBtnAplicar.Location = New System.Drawing.Point(723, 119)
        Me.INDBtnAplicar.Name = "INDBtnAplicar"
        Me.INDBtnAplicar.Size = New System.Drawing.Size(79, 28)
        Me.INDBtnAplicar.StyleController = Me.LayoutControl1
        Me.INDBtnAplicar.TabIndex = 8
        Me.INDBtnAplicar.Text = "Aplicar"
        '
        'INDSleCareGroup
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleCareGroup, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleCareGroup, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleCareGroup, False)
        Me.INDSleCareGroup.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleCareGroup, False)
        Me.INDSleCareGroup.Location = New System.Drawing.Point(113, 43)
        Me.INDSleCareGroup.Name = "INDSleCareGroup"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleCareGroup, False)
        Me.INDSleCareGroup.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleCareGroup.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleCareGroup.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleCareGroup.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleCareGroup.Properties.Appearance.Options.UseFont = True
        Me.INDSleCareGroup.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleCareGroup.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleCareGroup.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleCareGroup.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleCareGroup.Properties.DisplayMember = "CodeName"
        Me.INDSleCareGroup.Properties.NullText = ""
        Me.INDSleCareGroup.Properties.PopupSizeable = False
        Me.INDSleCareGroup.Properties.PopupView = Me.INDGvCareGroup
        Me.INDSleCareGroup.Properties.ShowFooter = False
        Me.INDSleCareGroup.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleCareGroup, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleCareGroup, True)
        Me.INDSleCareGroup.Size = New System.Drawing.Size(287, 28)
        Me.INDSleCareGroup.StyleController = Me.LayoutControl1
        Me.INDSleCareGroup.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleCareGroup, Nothing)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleCareGroup, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleCareGroup, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleCareGroup, False)
        '
        'INDGvCareGroup
        '
        Me.INDGvCareGroup.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvCareGroup.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvCareGroup.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvCareGroup.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvCareGroup.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareGroup.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvCareGroup.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvCareGroup.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvCareGroup.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvCareGroup.Appearance.Row.Options.UseFont = True
        Me.INDGvCareGroup.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvCareGroup_Code, Me.INDGvCareGroup_Name})
        Me.INDGvCareGroup.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvCareGroup.Name = "INDGvCareGroup"
        Me.INDGvCareGroup.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvCareGroup.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvCareGroup.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvCareGroup.OptionsView.ShowAutoFilterRow = True
        Me.INDGvCareGroup.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvCareGroup, False)
        '
        'INDGvCareGroup_Code
        '
        Me.INDGvCareGroup_Code.Caption = "Código"
        Me.INDGvCareGroup_Code.FieldName = "Code"
        Me.INDGvCareGroup_Code.Name = "INDGvCareGroup_Code"
        Me.INDGvCareGroup_Code.Visible = True
        Me.INDGvCareGroup_Code.VisibleIndex = 0
        Me.INDGvCareGroup_Code.Width = 358
        '
        'INDGvCareGroup_Name
        '
        Me.INDGvCareGroup_Name.Caption = "Nombre"
        Me.INDGvCareGroup_Name.FieldName = "Name"
        Me.INDGvCareGroup_Name.Name = "INDGvCareGroup_Name"
        Me.INDGvCareGroup_Name.Visible = True
        Me.INDGvCareGroup_Name.VisibleIndex = 1
        Me.INDGvCareGroup_Name.Width = 863
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.LayoutControlGroup2, Me.EmptySpaceItem1})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 155)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDBtnAplicar
        Me.LayoutControlItem9.Location = New System.Drawing.Point(721, 117)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(83, 32)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(83, 32)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(83, 38)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem9.TextToControlDistance = 0
        Me.LayoutControlItem9.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.EmptySpaceItem2, Me.LayoutControlItem1, Me.LayoutControlItem2, Me.INDLciCareGroup})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(804, 117)
        Me.LayoutControlGroup2.Text = "Filtros"
        '
        'EmptySpaceItem2
        '
        Me.EmptySpaceItem2.AllowHotTrack = False
        Me.EmptySpaceItem2.Location = New System.Drawing.Point(390, 0)
        Me.EmptySpaceItem2.Name = "EmptySpaceItem2"
        Me.EmptySpaceItem2.Size = New System.Drawing.Size(390, 32)
        Me.EmptySpaceItem2.TextSize = New System.Drawing.Size(0, 0)
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem1.Control = Me.INDDeDateStart
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 32)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Fecha Inicial"
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(96, 17)
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LayoutControlItem2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem2.Control = Me.INDDeDateEnd
        Me.LayoutControlItem2.Location = New System.Drawing.Point(390, 32)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 32)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.Text = "Fecha Final"
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(96, 17)
        '
        'INDLciCareGroup
        '
        Me.INDLciCareGroup.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLciCareGroup.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciCareGroup.Control = Me.INDSleCareGroup
        Me.INDLciCareGroup.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCareGroup.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciCareGroup.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciCareGroup.Name = "INDLciCareGroup"
        Me.INDLciCareGroup.Size = New System.Drawing.Size(390, 32)
        Me.INDLciCareGroup.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCareGroup.Text = "Grupo Atención"
        Me.INDLciCareGroup.TextSize = New System.Drawing.Size(96, 17)
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 117)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(721, 38)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDPceFilters
        '
        Me.INDPceFilters.Location = New System.Drawing.Point(14, 43)
        Me.INDPceFilters.Name = "INDPceFilters"
        Me.INDPceFilters.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.INDPceFilters.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDPceFilters.Properties.Appearance.Options.UseFont = True
        Me.INDPceFilters.Properties.Appearance.Options.UseForeColor = True
        Me.INDPceFilters.Properties.AutoHeight = False
        Me.INDPceFilters.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDPceFilters.Properties.PopupControl = Me.INDPccFilter
        Me.INDPceFilters.Size = New System.Drawing.Size(1220, 41)
        Me.INDPceFilters.StyleController = Me.INDLcDashboardAlertsPGP
        Me.INDPceFilters.TabIndex = 2
        '
        'INDGcRequests
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRequests, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRequests, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRequests, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRequests, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRequests, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRequests, False)
        Me.INDGcRequests.Location = New System.Drawing.Point(14, 175)
        Me.INDGcRequests.MainView = Me.INDGvRequests
        Me.INDGcRequests.MenuManager = Me.BarManager1
        Me.INDGcRequests.Name = "INDGcRequests"
        Me.INDGcRequests.Size = New System.Drawing.Size(1220, 385)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRequests, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcRequests.TabIndex = 1
        Me.INDGcRequests.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRequests})
        '
        'INDGvRequests
        '
        Me.INDGvRequests.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRequests.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRequests.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRequests.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRequests.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequests.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRequests.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequests.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRequests.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRequests.Appearance.Row.Options.UseFont = True
        Me.INDGvRequests.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRequests.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRequests.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvRequests_GroupDescription, Me.INDGvRequests_Date, Me.INDGvRequests_Patient, Me.INDGvRequests_AdmissionNumber, Me.INDGvRequests_Folio, Me.INDGvRequests_CareCenter, Me.INDGvRequests_FunctionalUnit, Me.INDGvRequests_Professional, Me.INDGvRequests_Diagnostic, Me.INDGvRequests_Observations, Me.INDGvRequests_CUPSEntity, Me.INDGvRequests_CUPSEntityDescription, Me.INDGvRequests_Quantity, Me.INDGvRequests_CMEValue, Me.INDGvRequests_TotalCME})
        Me.INDGvRequests.GridControl = Me.INDGcRequests
        Me.INDGvRequests.GroupCount = 1
        Me.INDGvRequests.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", Me.INDGvRequests_Quantity, "{0:N0}"), New DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCME", Me.INDGvRequests_TotalCME, "{0:C0}")})
        Me.INDGvRequests.Name = "INDGvRequests"
        Me.INDGvRequests.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRequests.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRequests.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.INDGvRequests.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRequests.OptionsView.ShowFooter = True
        Me.INDGvRequests.OptionsView.ShowGroupPanel = False
        Me.INDGvRequests.SortInfo.AddRange(New DevExpress.XtraGrid.Columns.GridColumnSortInfo() {New DevExpress.XtraGrid.Columns.GridColumnSortInfo(Me.INDGvRequests_GroupDescription, DevExpress.Data.ColumnSortOrder.Ascending)})
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRequests, False)
        '
        'INDGvRequests_GroupDescription
        '
        Me.INDGvRequests_GroupDescription.Caption = "Agrupador"
        Me.INDGvRequests_GroupDescription.FieldName = "GroupDescription"
        Me.INDGvRequests_GroupDescription.Name = "INDGvRequests_GroupDescription"
        Me.INDGvRequests_GroupDescription.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_GroupDescription.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_GroupDescription.Visible = True
        Me.INDGvRequests_GroupDescription.VisibleIndex = 0
        '
        'INDGvRequests_Date
        '
        Me.INDGvRequests_Date.Caption = "Fecha"
        Me.INDGvRequests_Date.FieldName = "RequestDate"
        Me.INDGvRequests_Date.Name = "INDGvRequests_Date"
        Me.INDGvRequests_Date.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Date.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Date.Visible = True
        Me.INDGvRequests_Date.VisibleIndex = 0
        Me.INDGvRequests_Date.Width = 100
        '
        'INDGvRequests_Patient
        '
        Me.INDGvRequests_Patient.Caption = "Paciente"
        Me.INDGvRequests_Patient.FieldName = "Patient"
        Me.INDGvRequests_Patient.Name = "INDGvRequests_Patient"
        Me.INDGvRequests_Patient.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Patient.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Patient.Visible = True
        Me.INDGvRequests_Patient.VisibleIndex = 1
        Me.INDGvRequests_Patient.Width = 265
        '
        'INDGvRequests_AdmissionNumber
        '
        Me.INDGvRequests_AdmissionNumber.Caption = "Ingreso"
        Me.INDGvRequests_AdmissionNumber.FieldName = "AdmissionNumber"
        Me.INDGvRequests_AdmissionNumber.Name = "INDGvRequests_AdmissionNumber"
        Me.INDGvRequests_AdmissionNumber.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_AdmissionNumber.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_AdmissionNumber.Visible = True
        Me.INDGvRequests_AdmissionNumber.VisibleIndex = 2
        Me.INDGvRequests_AdmissionNumber.Width = 100
        '
        'INDGvRequests_Folio
        '
        Me.INDGvRequests_Folio.Caption = "Folio"
        Me.INDGvRequests_Folio.FieldName = "Folio"
        Me.INDGvRequests_Folio.Name = "INDGvRequests_Folio"
        Me.INDGvRequests_Folio.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Folio.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Folio.Width = 124
        '
        'INDGvRequests_CareCenter
        '
        Me.INDGvRequests_CareCenter.Caption = "Centro Atención"
        Me.INDGvRequests_CareCenter.FieldName = "CareCenter"
        Me.INDGvRequests_CareCenter.Name = "INDGvRequests_CareCenter"
        Me.INDGvRequests_CareCenter.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_CareCenter.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_CareCenter.Width = 54
        '
        'INDGvRequests_FunctionalUnit
        '
        Me.INDGvRequests_FunctionalUnit.Caption = "Unidad Funcional"
        Me.INDGvRequests_FunctionalUnit.FieldName = "FunctionalUnit"
        Me.INDGvRequests_FunctionalUnit.Name = "INDGvRequests_FunctionalUnit"
        Me.INDGvRequests_FunctionalUnit.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_FunctionalUnit.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_FunctionalUnit.Visible = True
        Me.INDGvRequests_FunctionalUnit.VisibleIndex = 3
        Me.INDGvRequests_FunctionalUnit.Width = 200
        '
        'INDGvRequests_Professional
        '
        Me.INDGvRequests_Professional.Caption = "Medico Solicitante"
        Me.INDGvRequests_Professional.FieldName = "Professional"
        Me.INDGvRequests_Professional.Name = "INDGvRequests_Professional"
        Me.INDGvRequests_Professional.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Professional.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Professional.Width = 243
        '
        'INDGvRequests_Diagnostic
        '
        Me.INDGvRequests_Diagnostic.Caption = "Diagnostico"
        Me.INDGvRequests_Diagnostic.FieldName = "Diagnostic"
        Me.INDGvRequests_Diagnostic.Name = "INDGvRequests_Diagnostic"
        Me.INDGvRequests_Diagnostic.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Diagnostic.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Diagnostic.Width = 127
        '
        'INDGvRequests_Observations
        '
        Me.INDGvRequests_Observations.Caption = "Observaciones"
        Me.INDGvRequests_Observations.FieldName = "Observations"
        Me.INDGvRequests_Observations.Name = "INDGvRequests_Observations"
        Me.INDGvRequests_Observations.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Observations.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Observations.Width = 118
        '
        'INDGvRequests_CUPSEntity
        '
        Me.INDGvRequests_CUPSEntity.Caption = "Servicio"
        Me.INDGvRequests_CUPSEntity.FieldName = "CUPSEntityCodeName"
        Me.INDGvRequests_CUPSEntity.Name = "INDGvRequests_CUPSEntity"
        Me.INDGvRequests_CUPSEntity.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_CUPSEntity.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_CUPSEntity.Visible = True
        Me.INDGvRequests_CUPSEntity.VisibleIndex = 4
        Me.INDGvRequests_CUPSEntity.Width = 260
        '
        'INDGvRequests_CUPSEntityDescription
        '
        Me.INDGvRequests_CUPSEntityDescription.Caption = "Descripción Relacionada"
        Me.INDGvRequests_CUPSEntityDescription.FieldName = "DescriptionCodeName"
        Me.INDGvRequests_CUPSEntityDescription.Name = "INDGvRequests_CUPSEntityDescription"
        Me.INDGvRequests_CUPSEntityDescription.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_CUPSEntityDescription.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_CUPSEntityDescription.Width = 143
        '
        'INDGvRequests_Quantity
        '
        Me.INDGvRequests_Quantity.Caption = "Cantidad"
        Me.INDGvRequests_Quantity.DisplayFormat.FormatString = "N0"
        Me.INDGvRequests_Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvRequests_Quantity.FieldName = "Quantity"
        Me.INDGvRequests_Quantity.Name = "INDGvRequests_Quantity"
        Me.INDGvRequests_Quantity.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_Quantity.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_Quantity.Visible = True
        Me.INDGvRequests_Quantity.VisibleIndex = 5
        Me.INDGvRequests_Quantity.Width = 90
        '
        'INDGvRequests_CMEValue
        '
        Me.INDGvRequests_CMEValue.Caption = "Valor CME"
        Me.INDGvRequests_CMEValue.DisplayFormat.FormatString = "C0"
        Me.INDGvRequests_CMEValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvRequests_CMEValue.FieldName = "CMEValue"
        Me.INDGvRequests_CMEValue.Name = "INDGvRequests_CMEValue"
        Me.INDGvRequests_CMEValue.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_CMEValue.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_CMEValue.Visible = True
        Me.INDGvRequests_CMEValue.VisibleIndex = 6
        Me.INDGvRequests_CMEValue.Width = 90
        '
        'INDGvRequests_TotalCME
        '
        Me.INDGvRequests_TotalCME.Caption = "Total CME"
        Me.INDGvRequests_TotalCME.DisplayFormat.FormatString = "C0"
        Me.INDGvRequests_TotalCME.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvRequests_TotalCME.FieldName = "TotalCME"
        Me.INDGvRequests_TotalCME.Name = "INDGvRequests_TotalCME"
        Me.INDGvRequests_TotalCME.OptionsColumn.AllowEdit = False
        Me.INDGvRequests_TotalCME.OptionsColumn.AllowFocus = False
        Me.INDGvRequests_TotalCME.Visible = True
        Me.INDGvRequests_TotalCME.VisibleIndex = 7
        Me.INDGvRequests_TotalCME.Width = 90
        '
        'INDLcgDashboardAlertsPGP
        '
        Me.INDLcgDashboardAlertsPGP.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDashboardAlertsPGP.AppearanceGroup.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgDashboardAlertsPGP.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgDashboardAlertsPGP.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgDashboardAlertsPGP.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgDashboardAlertsPGP.GroupBordersVisible = False
        Me.INDLcgDashboardAlertsPGP.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgFilter, Me.INDTcgDashboardAlertsPGP})
        Me.INDLcgDashboardAlertsPGP.Name = "INDLcgDashboardAlertsPGP"
        Me.INDLcgDashboardAlertsPGP.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLcgDashboardAlertsPGP.Size = New System.Drawing.Size(1248, 574)
        Me.INDLcgDashboardAlertsPGP.TextVisible = False
        '
        'INDLcgFilter
        '
        Me.INDLcgFilter.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilter.AppearanceGroup.Options.UseFont = True
        Me.INDLcgFilter.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgFilter.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgFilter.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgFilter.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgFilter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem19})
        Me.INDLcgFilter.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilter.Name = "INDLcgFilter"
        Me.INDLcgFilter.Size = New System.Drawing.Size(1248, 98)
        Me.INDLcgFilter.Text = "Filtros"
        '
        'LayoutControlItem19
        '
        Me.LayoutControlItem19.Control = Me.INDPceFilters
        Me.LayoutControlItem19.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem19.MaxSize = New System.Drawing.Size(0, 45)
        Me.LayoutControlItem19.MinSize = New System.Drawing.Size(1, 45)
        Me.LayoutControlItem19.Name = "LayoutControlItem19"
        Me.LayoutControlItem19.Size = New System.Drawing.Size(1224, 45)
        Me.LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem19.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem19.TextVisible = False
        '
        'INDTcgDashboardAlertsPGP
        '
        Me.INDTcgDashboardAlertsPGP.Location = New System.Drawing.Point(0, 98)
        Me.INDTcgDashboardAlertsPGP.Name = "INDTcgDashboardAlertsPGP"
        Me.INDTcgDashboardAlertsPGP.SelectedTabPage = Me.INDLcgServiceControls
        Me.INDTcgDashboardAlertsPGP.Size = New System.Drawing.Size(1248, 476)
        Me.INDTcgDashboardAlertsPGP.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgRequests, Me.INDLcgServiceOrders, Me.INDLcgServiceControls})
        '
        'INDLcgRequests
        '
        Me.INDLcgRequests.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRequests, Me.INDLciRequestsText, Me.INDLciRequestsValue})
        Me.INDLcgRequests.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgRequests.Name = "INDLcgRequests"
        Me.INDLcgRequests.Size = New System.Drawing.Size(1224, 421)
        Me.INDLcgRequests.Text = "Solicitudes"
        '
        'INDLciRequests
        '
        Me.INDLciRequests.Control = Me.INDGcRequests
        Me.INDLciRequests.Location = New System.Drawing.Point(0, 32)
        Me.INDLciRequests.Name = "INDLciRequests"
        Me.INDLciRequests.Size = New System.Drawing.Size(1224, 389)
        Me.INDLciRequests.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciRequests.TextVisible = False
        '
        'INDLciRequestsText
        '
        Me.INDLciRequestsText.Control = Me.INDLblRequestsText
        Me.INDLciRequestsText.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRequestsText.Name = "INDLciRequestsText"
        Me.INDLciRequestsText.Size = New System.Drawing.Size(70, 32)
        Me.INDLciRequestsText.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciRequestsText.TextVisible = False
        '
        'INDLciRequestsValue
        '
        Me.INDLciRequestsValue.Control = Me.INDLblRequestsValue
        Me.INDLciRequestsValue.Location = New System.Drawing.Point(70, 0)
        Me.INDLciRequestsValue.Name = "INDLciRequestsValue"
        Me.INDLciRequestsValue.Size = New System.Drawing.Size(1154, 32)
        Me.INDLciRequestsValue.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciRequestsValue.TextVisible = False
        '
        'INDLcgServiceOrders
        '
        Me.INDLcgServiceOrders.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciServiceOrders, Me.INDLciServiceOrdersText, Me.INDLciServiceOrdersValue})
        Me.INDLcgServiceOrders.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgServiceOrders.Name = "INDLcgServiceOrders"
        Me.INDLcgServiceOrders.Size = New System.Drawing.Size(1224, 421)
        Me.INDLcgServiceOrders.Text = "Ordenes de Servicio"
        '
        'INDLciServiceOrders
        '
        Me.INDLciServiceOrders.Control = Me.INDGcServiceOrders
        Me.INDLciServiceOrders.Location = New System.Drawing.Point(0, 32)
        Me.INDLciServiceOrders.Name = "INDLciServiceOrders"
        Me.INDLciServiceOrders.Size = New System.Drawing.Size(1224, 389)
        Me.INDLciServiceOrders.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciServiceOrders.TextVisible = False
        '
        'INDLciServiceOrdersText
        '
        Me.INDLciServiceOrdersText.Control = Me.INDLblServiceOrdersText
        Me.INDLciServiceOrdersText.Location = New System.Drawing.Point(0, 0)
        Me.INDLciServiceOrdersText.Name = "INDLciServiceOrdersText"
        Me.INDLciServiceOrdersText.Size = New System.Drawing.Size(70, 32)
        Me.INDLciServiceOrdersText.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciServiceOrdersText.TextVisible = False
        '
        'INDLciServiceOrdersValue
        '
        Me.INDLciServiceOrdersValue.Control = Me.INDLblServiceOrdersValue
        Me.INDLciServiceOrdersValue.Location = New System.Drawing.Point(70, 0)
        Me.INDLciServiceOrdersValue.Name = "INDLciServiceOrdersValue"
        Me.INDLciServiceOrdersValue.Size = New System.Drawing.Size(1154, 32)
        Me.INDLciServiceOrdersValue.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciServiceOrdersValue.TextVisible = False
        '
        'INDLcgServiceControls
        '
        Me.INDLcgServiceControls.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciServiceControls, Me.INDLciServiceControlsText, Me.INDLciServiceControlsValue})
        Me.INDLcgServiceControls.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgServiceControls.Name = "INDLcgServiceControls"
        Me.INDLcgServiceControls.Size = New System.Drawing.Size(1224, 421)
        Me.INDLcgServiceControls.Text = "Controles de Servicio"
        '
        'INDLciServiceControls
        '
        Me.INDLciServiceControls.Control = Me.INDGcServiceControls
        Me.INDLciServiceControls.Location = New System.Drawing.Point(0, 32)
        Me.INDLciServiceControls.Name = "INDLciServiceControls"
        Me.INDLciServiceControls.Size = New System.Drawing.Size(1224, 389)
        Me.INDLciServiceControls.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciServiceControls.TextVisible = False
        '
        'INDLciServiceControlsText
        '
        Me.INDLciServiceControlsText.Control = Me.INDLblServiceControlsText
        Me.INDLciServiceControlsText.Location = New System.Drawing.Point(0, 0)
        Me.INDLciServiceControlsText.Name = "INDLciServiceControlsText"
        Me.INDLciServiceControlsText.Size = New System.Drawing.Size(70, 32)
        Me.INDLciServiceControlsText.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciServiceControlsText.TextVisible = False
        '
        'INDLciServiceControlsValue
        '
        Me.INDLciServiceControlsValue.Control = Me.INDLblServiceControlsValue
        Me.INDLciServiceControlsValue.Location = New System.Drawing.Point(70, 0)
        Me.INDLciServiceControlsValue.Name = "INDLciServiceControlsValue"
        Me.INDLciServiceControlsValue.Size = New System.Drawing.Size(1154, 32)
        Me.INDLciServiceControlsValue.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciServiceControlsValue.TextVisible = False
        '
        'TreeListColumn1
        '
        Me.TreeListColumn1.Caption = "Descripción"
        Me.TreeListColumn1.FieldName = "CodeName"
        Me.TreeListColumn1.Name = "TreeListColumn1"
        Me.TreeListColumn1.Visible = True
        Me.TreeListColumn1.VisibleIndex = 0
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDPopMenuActions1
        '
        Me.INDPopMenuActions1.Manager = Me.BarManager1
        Me.INDPopMenuActions1.Name = "INDPopMenuActions1"
        '
        'FrmDashboardAlertsPGP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1252, 718)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmDashboardAlertsPGP"
        Me.Opacity = 1.0R
        Me.Tag = "2710"
        Me.Text = "Dashboard Alertas PGP"
        Me.Controls.SetChildIndex(Me.BarDockControl5, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl6, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl8, 0)
        Me.Controls.SetChildIndex(Me.BarDockControl7, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcDashboardAlertsPGP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcDashboardAlertsPGP.ResumeLayout(False)
        CType(Me.INDGcServiceControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvServiceControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcServiceOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvServiceOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccFilter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccFilter.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDDeDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleCareGroup.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCareGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPceFilters.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgDashboardAlertsPGP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgDashboardAlertsPGP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequests, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequestsText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequestsValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgServiceOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceOrders, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceOrdersText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceOrdersValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgServiceControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceControlsText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciServiceControlsValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLcDashboardAlertsPGP As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDPccFilter As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents TreeListColumn1 As DevExpress.XtraTreeList.Columns.TreeListColumn
    Friend WithEvents INDBtnAplicar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSleCareGroup As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvCareGroup As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvCareGroup_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvCareGroup_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCareGroup As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDPceFilters As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDGcRequests As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRequests As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvRequests_Patient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_AdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_FunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_CareCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_Folio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_Professional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_Observations As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_CUPSEntity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_CUPSEntityDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgDashboardAlertsPGP As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgFilter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDTcgDashboardAlertsPGP As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgRequests As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciRequests As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvRequests_GroupDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvRequests_Diagnostic As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDPopMenuActions1 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDGvRequests_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDDeDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDGvRequests_CMEValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgServiceControls As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgServiceOrders As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcServiceOrders As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvServiceOrders As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciServiceOrders As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcServiceControls As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvServiceControls As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciServiceControls As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvRequests_TotalCME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_GroupDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_Patient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_AdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_CareCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_FunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_Professional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_Diagnostic As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_Observations As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_CUPSEntity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_CUPSEntityDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_CMEValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_TotalCME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_TotalServiceValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceOrders_ServiceOrderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_GroupDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_Date As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_Patient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_AdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_CareCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_FunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_Professional As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_Diagnostic As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_Observations As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_CUPSEntity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_CUPSEntityDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_CMEValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_TotalCME As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_TotalServiceValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_ServiceOrderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_ServiceControlNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvServiceControls_InvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLblRequestsValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLblRequestsText As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciRequestsText As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciRequestsValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLblServiceControlsValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLblServiceControlsText As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLblServiceOrdersValue As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLblServiceOrdersText As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDLciServiceControlsText As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciServiceControlsValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciServiceOrdersText As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciServiceOrdersValue As DevExpress.XtraLayout.LayoutControlItem
End Class
