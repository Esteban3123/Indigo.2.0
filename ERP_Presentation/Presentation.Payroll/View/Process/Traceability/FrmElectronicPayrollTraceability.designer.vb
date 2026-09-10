Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmElectronicPayrollTraceability
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmElectronicPayrollTraceability))
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcExportExcel = New DevExpress.XtraGrid.GridControl()
        Me.INDGvExportExcel = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvExportExcel_Year = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_Month = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_EmployeePartyNit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_EmployeePartyName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_NatureDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_Detail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_DateStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_DateEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_Quantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_Percentage = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_Value = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvExportExcel_CUNE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BarManager2 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.BarDockControl5 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl6 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl7 = New DevExpress.XtraBars.BarDockControl()
        Me.BarDockControl8 = New DevExpress.XtraBars.BarDockControl()
        Me.INDBbiProcess = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiSendNotification = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiPrint = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiSelection = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiUnSelection = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiGenerateAdjustmentNote = New DevExpress.XtraBars.BarButtonItem()
        Me.INDBbiExport = New DevExpress.XtraBars.BarButtonItem()
        Me.INDPccElectronicPayrollNotifications = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcElectronicPayrollNotifications = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcElectronicPayrollNotifications = New DevExpress.XtraGrid.GridControl()
        Me.INDgvElectronicPayrollNotifications = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDcolElectronicPayrollNotifications_Email = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolElectronicPayrollNotifications_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolElectronicPayrollNotifications_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDcolElectronicPayrollNotifications_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciElectronicPayrollNotifications = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDPccElectronicPayrollDetails = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcElectronicPayrollDetails = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcElectronicPayrollDetails = New DevExpress.XtraGrid.GridControl()
        Me.INDGvElectronicPayrollDetails = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvElectronicPayrollDetails_Destination = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollDetails_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollDetails_Response = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollDetails_Comments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollDetails_ResponseData = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollDetails_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgElectronicPayrollDetails = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciElectronicPayrollDetails = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGcElectronicPayrollPaymentSupport = New DevExpress.XtraGrid.GridControl()
        Me.INDGvElectronicPayrollPaymentSupport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_Alert = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRepCbeRequestsAlert = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDicAlertas = New DevExpress.Utils.ImageCollection(Me.components)
        Me.INDGvElectronicPayrollPaymentSupport_Period = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_StatusName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_Download = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnDownload = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDGvElectronicPayrollPaymentSupport_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvElectronicPayrollPaymentSupport_Notifications = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvElectronicPayrollPaymentSupport_CUNE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcAdjustmentNote = New DevExpress.XtraGrid.GridControl()
        Me.INDGvAdjustmentNote = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvAdjustmentNote_UnboundSelection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_Period = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_DocumentNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_EmployeePartyNitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_Status = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_CreationDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_ShippingDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_Details = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_PceDetails = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvAdjustmentNote_Notifications = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvAdjustmentNote_PceNotifications = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDGvAdjustmentNote_CUNE = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDTcgElectronicPayroll = New DevExpress.XtraLayout.TabbedControlGroup()
        Me.INDLcgElectronicPayrollPaymentSupport = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciElectronicPayrollPaymentSupport = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgAdjustmentNote = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAdjustmentNote = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciExportExcel = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDPopMenuActions2 = New DevExpress.XtraBars.PopupMenu(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcMainData.SuspendLayout()
        CType(Me.INDGcExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccElectronicPayrollNotifications.SuspendLayout()
        CType(Me.INDLcElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcElectronicPayrollNotifications.SuspendLayout()
        CType(Me.INDGcElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPccElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccElectronicPayrollDetails.SuspendLayout()
        CType(Me.INDLcElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcElectronicPayrollDetails.SuspendLayout()
        CType(Me.INDGcElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRepCbeRequestsAlert, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDicAlertas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnDownload, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvElectronicPayrollPaymentSupport_PceDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvElectronicPayrollPaymentSupport_PceNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcAdjustmentNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAdjustmentNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAdjustmentNote_PceDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAdjustmentNote_PceNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTcgElectronicPayroll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAdjustmentNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdjustmentNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciExportExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcMainData)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1014, 623)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1014, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1014, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        Me.RepositoryItemPictureEdit1.NullText = " "
        '
        'INDLcMainData
        '
        Me.INDLcMainData.AllowCustomization = False
        Me.INDLcMainData.Controls.Add(Me.INDGcExportExcel)
        Me.INDLcMainData.Controls.Add(Me.INDPccElectronicPayrollNotifications)
        Me.INDLcMainData.Controls.Add(Me.INDPccElectronicPayrollDetails)
        Me.INDLcMainData.Controls.Add(Me.INDGcElectronicPayrollPaymentSupport)
        Me.INDLcMainData.Controls.Add(Me.INDGcAdjustmentNote)
        Me.INDLcMainData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainData, False)
        Me.INDLcMainData.Location = New System.Drawing.Point(2, 7)
        Me.INDLcMainData.Name = "INDLcMainData"
        Me.INDLcMainData.Root = Me.LayoutControlGroup2
        Me.INDLcMainData.Size = New System.Drawing.Size(1010, 614)
        Me.INDLcMainData.TabIndex = 10
        Me.INDLcMainData.Text = "LayoutControl2"
        '
        'INDGcExportExcel
        '
        Me.INDGcExportExcel.Location = New System.Drawing.Point(12, 578)
        Me.INDGcExportExcel.MainView = Me.INDGvExportExcel
        Me.INDGcExportExcel.MenuManager = Me.BarManager2
        Me.INDGcExportExcel.Name = "INDGcExportExcel"
        Me.INDGcExportExcel.Size = New System.Drawing.Size(986, 24)
        Me.INDGcExportExcel.TabIndex = 26
        Me.INDGcExportExcel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvExportExcel})
        '
        'INDGvExportExcel
        '
        Me.INDGvExportExcel.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvExportExcel.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvExportExcel.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvExportExcel.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvExportExcel.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvExportExcel.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvExportExcel.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvExportExcel.Appearance.Row.Options.UseFont = True
        Me.INDGvExportExcel.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvExportExcel_Year, Me.INDGvExportExcel_Month, Me.INDGvExportExcel_EmployeePartyNit, Me.INDGvExportExcel_EmployeePartyName, Me.INDGvExportExcel_NatureDescription, Me.INDGvExportExcel_Detail, Me.INDGvExportExcel_DateStart, Me.INDGvExportExcel_DateEnd, Me.INDGvExportExcel_Quantity, Me.INDGvExportExcel_Percentage, Me.INDGvExportExcel_Value, Me.INDGvExportExcel_CUNE})
        Me.INDGvExportExcel.GridControl = Me.INDGcExportExcel
        Me.INDGvExportExcel.Name = "INDGvExportExcel"
        Me.INDGvExportExcel.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvExportExcel.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvExportExcel.OptionsView.ShowAutoFilterRow = True
        Me.INDGvExportExcel.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvExportExcel, False)
        '
        'INDGvExportExcel_Year
        '
        Me.INDGvExportExcel_Year.Caption = "Año"
        Me.INDGvExportExcel_Year.FieldName = "Year"
        Me.INDGvExportExcel_Year.Name = "INDGvExportExcel_Year"
        Me.INDGvExportExcel_Year.Visible = True
        Me.INDGvExportExcel_Year.VisibleIndex = 0
        '
        'INDGvExportExcel_Month
        '
        Me.INDGvExportExcel_Month.Caption = "Mes"
        Me.INDGvExportExcel_Month.FieldName = "Month"
        Me.INDGvExportExcel_Month.Name = "INDGvExportExcel_Month"
        Me.INDGvExportExcel_Month.Visible = True
        Me.INDGvExportExcel_Month.VisibleIndex = 1
        '
        'INDGvExportExcel_EmployeePartyNit
        '
        Me.INDGvExportExcel_EmployeePartyNit.Caption = "Nit"
        Me.INDGvExportExcel_EmployeePartyNit.FieldName = "EmployeePartyNit"
        Me.INDGvExportExcel_EmployeePartyNit.Name = "INDGvExportExcel_EmployeePartyNit"
        Me.INDGvExportExcel_EmployeePartyNit.Visible = True
        Me.INDGvExportExcel_EmployeePartyNit.VisibleIndex = 2
        '
        'INDGvExportExcel_EmployeePartyName
        '
        Me.INDGvExportExcel_EmployeePartyName.Caption = "Nombre"
        Me.INDGvExportExcel_EmployeePartyName.FieldName = "EmployeePartyName"
        Me.INDGvExportExcel_EmployeePartyName.Name = "INDGvExportExcel_EmployeePartyName"
        Me.INDGvExportExcel_EmployeePartyName.Visible = True
        Me.INDGvExportExcel_EmployeePartyName.VisibleIndex = 3
        '
        'INDGvExportExcel_NatureDescription
        '
        Me.INDGvExportExcel_NatureDescription.Caption = "Naturaleza"
        Me.INDGvExportExcel_NatureDescription.FieldName = "NatureDescription"
        Me.INDGvExportExcel_NatureDescription.Name = "INDGvExportExcel_NatureDescription"
        Me.INDGvExportExcel_NatureDescription.Visible = True
        Me.INDGvExportExcel_NatureDescription.VisibleIndex = 4
        '
        'INDGvExportExcel_Detail
        '
        Me.INDGvExportExcel_Detail.Caption = "Detalle"
        Me.INDGvExportExcel_Detail.FieldName = "Detail"
        Me.INDGvExportExcel_Detail.Name = "INDGvExportExcel_Detail"
        Me.INDGvExportExcel_Detail.Visible = True
        Me.INDGvExportExcel_Detail.VisibleIndex = 5
        '
        'INDGvExportExcel_DateStart
        '
        Me.INDGvExportExcel_DateStart.Caption = "Fecha Inicio"
        Me.INDGvExportExcel_DateStart.FieldName = "DateStart"
        Me.INDGvExportExcel_DateStart.Name = "INDGvExportExcel_DateStart"
        Me.INDGvExportExcel_DateStart.Visible = True
        Me.INDGvExportExcel_DateStart.VisibleIndex = 6
        '
        'INDGvExportExcel_DateEnd
        '
        Me.INDGvExportExcel_DateEnd.Caption = "Fecha Fin"
        Me.INDGvExportExcel_DateEnd.FieldName = "DateEnd"
        Me.INDGvExportExcel_DateEnd.Name = "INDGvExportExcel_DateEnd"
        Me.INDGvExportExcel_DateEnd.Visible = True
        Me.INDGvExportExcel_DateEnd.VisibleIndex = 7
        '
        'INDGvExportExcel_Quantity
        '
        Me.INDGvExportExcel_Quantity.Caption = "Cantidad"
        Me.INDGvExportExcel_Quantity.DisplayFormat.FormatString = "N0"
        Me.INDGvExportExcel_Quantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvExportExcel_Quantity.FieldName = "Quantity"
        Me.INDGvExportExcel_Quantity.Name = "INDGvExportExcel_Quantity"
        Me.INDGvExportExcel_Quantity.Visible = True
        Me.INDGvExportExcel_Quantity.VisibleIndex = 8
        '
        'INDGvExportExcel_Percentage
        '
        Me.INDGvExportExcel_Percentage.Caption = "Porcentaje"
        Me.INDGvExportExcel_Percentage.DisplayFormat.FormatString = "N0"
        Me.INDGvExportExcel_Percentage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvExportExcel_Percentage.FieldName = "Percentage"
        Me.INDGvExportExcel_Percentage.Name = "INDGvExportExcel_Percentage"
        Me.INDGvExportExcel_Percentage.Visible = True
        Me.INDGvExportExcel_Percentage.VisibleIndex = 9
        '
        'INDGvExportExcel_Value
        '
        Me.INDGvExportExcel_Value.Caption = "Valor"
        Me.INDGvExportExcel_Value.DisplayFormat.FormatString = "N2"
        Me.INDGvExportExcel_Value.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvExportExcel_Value.FieldName = "Value"
        Me.INDGvExportExcel_Value.Name = "INDGvExportExcel_Value"
        Me.INDGvExportExcel_Value.Visible = True
        Me.INDGvExportExcel_Value.VisibleIndex = 10
        '
        'INDGvExportExcel_CUNE
        '
        Me.INDGvExportExcel_CUNE.Caption = "CUNE"
        Me.INDGvExportExcel_CUNE.FieldName = "CUNE"
        Me.INDGvExportExcel_CUNE.Name = "INDGvExportExcel_CUNE"
        Me.INDGvExportExcel_CUNE.Visible = True
        Me.INDGvExportExcel_CUNE.VisibleIndex = 11
        '
        'BarManager2
        '
        Me.BarManager2.DockControls.Add(Me.BarDockControl5)
        Me.BarManager2.DockControls.Add(Me.BarDockControl6)
        Me.BarManager2.DockControls.Add(Me.BarDockControl7)
        Me.BarManager2.DockControls.Add(Me.BarDockControl8)
        Me.BarManager2.Form = Me
        Me.BarManager2.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.INDBbiProcess, Me.INDBbiSendNotification, Me.INDBbiPrint, Me.INDBbiSelection, Me.INDBbiUnSelection, Me.INDBbiGenerateAdjustmentNote, Me.INDBbiExport})
        Me.BarManager2.MaxItemId = 35
        '
        'BarDockControl5
        '
        Me.BarDockControl5.CausesValidation = False
        Me.BarDockControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.BarDockControl5.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl5.Manager = Me.BarManager2
        Me.BarDockControl5.Size = New System.Drawing.Size(1014, 0)
        '
        'BarDockControl6
        '
        Me.BarDockControl6.CausesValidation = False
        Me.BarDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BarDockControl6.Location = New System.Drawing.Point(0, 758)
        Me.BarDockControl6.Manager = Me.BarManager2
        Me.BarDockControl6.Size = New System.Drawing.Size(1014, 0)
        '
        'BarDockControl7
        '
        Me.BarDockControl7.CausesValidation = False
        Me.BarDockControl7.Dock = System.Windows.Forms.DockStyle.Left
        Me.BarDockControl7.Location = New System.Drawing.Point(0, 5)
        Me.BarDockControl7.Manager = Me.BarManager2
        Me.BarDockControl7.Size = New System.Drawing.Size(0, 753)
        '
        'BarDockControl8
        '
        Me.BarDockControl8.CausesValidation = False
        Me.BarDockControl8.Dock = System.Windows.Forms.DockStyle.Right
        Me.BarDockControl8.Location = New System.Drawing.Point(1014, 5)
        Me.BarDockControl8.Manager = Me.BarManager2
        Me.BarDockControl8.Size = New System.Drawing.Size(0, 753)
        '
        'INDBbiProcess
        '
        Me.INDBbiProcess.Caption = "Procesar"
        Me.INDBbiProcess.Id = 17
        Me.INDBbiProcess.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.Refresh_16x16_blue
        Me.INDBbiProcess.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiProcess.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiProcess.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiProcess.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiProcess.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiProcess.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiProcess.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiProcess.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiProcess.Name = "INDBbiProcess"
        '
        'INDBbiSendNotification
        '
        Me.INDBbiSendNotification.Caption = "Enviar Notificación"
        Me.INDBbiSendNotification.Id = 29
        Me.INDBbiSendNotification.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.calificar16x16
        Me.INDBbiSendNotification.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSendNotification.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiSendNotification.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSendNotification.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiSendNotification.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSendNotification.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiSendNotification.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSendNotification.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiSendNotification.Name = "INDBbiSendNotification"
        '
        'INDBbiPrint
        '
        Me.INDBbiPrint.Caption = "Imprimir"
        Me.INDBbiPrint.Id = 30
        Me.INDBbiPrint.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.Print_16x16_blue
        Me.INDBbiPrint.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiPrint.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiPrint.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiPrint.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiPrint.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiPrint.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiPrint.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiPrint.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiPrint.Name = "INDBbiPrint"
        '
        'INDBbiSelection
        '
        Me.INDBbiSelection.Caption = "Seleccionar"
        Me.INDBbiSelection.Id = 31
        Me.INDBbiSelection.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.calificar16x16
        Me.INDBbiSelection.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSelection.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiSelection.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSelection.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiSelection.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSelection.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiSelection.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiSelection.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiSelection.Name = "INDBbiSelection"
        '
        'INDBbiUnSelection
        '
        Me.INDBbiUnSelection.Caption = "Deseleccionar"
        Me.INDBbiUnSelection.Id = 32
        Me.INDBbiUnSelection.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.Refresh_16x16_blue
        Me.INDBbiUnSelection.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiUnSelection.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiUnSelection.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiUnSelection.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiUnSelection.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiUnSelection.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiUnSelection.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiUnSelection.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiUnSelection.Name = "INDBbiUnSelection"
        '
        'INDBbiGenerateAdjustmentNote
        '
        Me.INDBbiGenerateAdjustmentNote.Caption = "Generar Nota de Ajuste"
        Me.INDBbiGenerateAdjustmentNote.Id = 33
        Me.INDBbiGenerateAdjustmentNote.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.calificar16x16
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiGenerateAdjustmentNote.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiGenerateAdjustmentNote.Name = "INDBbiGenerateAdjustmentNote"
        '
        'INDBbiExport
        '
        Me.INDBbiExport.Caption = "Exportar"
        Me.INDBbiExport.Id = 34
        Me.INDBbiExport.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.calificar16x16
        Me.INDBbiExport.ItemAppearance.Disabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiExport.ItemAppearance.Disabled.Options.UseFont = True
        Me.INDBbiExport.ItemAppearance.Hovered.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiExport.ItemAppearance.Hovered.Options.UseFont = True
        Me.INDBbiExport.ItemAppearance.Normal.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiExport.ItemAppearance.Normal.Options.UseFont = True
        Me.INDBbiExport.ItemAppearance.Pressed.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBbiExport.ItemAppearance.Pressed.Options.UseFont = True
        Me.INDBbiExport.Name = "INDBbiExport"
        '
        'INDPccElectronicPayrollNotifications
        '
        Me.INDPccElectronicPayrollNotifications.Controls.Add(Me.INDLcElectronicPayrollNotifications)
        Me.INDPccElectronicPayrollNotifications.Location = New System.Drawing.Point(57, 400)
        Me.INDPccElectronicPayrollNotifications.Name = "INDPccElectronicPayrollNotifications"
        Me.INDPccElectronicPayrollNotifications.Size = New System.Drawing.Size(858, 128)
        Me.INDPccElectronicPayrollNotifications.TabIndex = 25
        '
        'INDLcElectronicPayrollNotifications
        '
        Me.INDLcElectronicPayrollNotifications.Controls.Add(Me.INDGcElectronicPayrollNotifications)
        Me.INDLcElectronicPayrollNotifications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcElectronicPayrollNotifications.Location = New System.Drawing.Point(0, 0)
        Me.INDLcElectronicPayrollNotifications.Name = "INDLcElectronicPayrollNotifications"
        Me.INDLcElectronicPayrollNotifications.Root = Me.Root
        Me.INDLcElectronicPayrollNotifications.Size = New System.Drawing.Size(858, 128)
        Me.INDLcElectronicPayrollNotifications.TabIndex = 0
        Me.INDLcElectronicPayrollNotifications.Text = "LayoutControl1"
        '
        'INDGcElectronicPayrollNotifications
        '
        Me.INDGcElectronicPayrollNotifications.Location = New System.Drawing.Point(12, 12)
        Me.INDGcElectronicPayrollNotifications.MainView = Me.INDgvElectronicPayrollNotifications
        Me.INDGcElectronicPayrollNotifications.Name = "INDGcElectronicPayrollNotifications"
        Me.INDGcElectronicPayrollNotifications.Size = New System.Drawing.Size(834, 104)
        Me.INDGcElectronicPayrollNotifications.TabIndex = 4
        Me.INDGcElectronicPayrollNotifications.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvElectronicPayrollNotifications})
        '
        'INDgvElectronicPayrollNotifications
        '
        Me.INDgvElectronicPayrollNotifications.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvElectronicPayrollNotifications.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvElectronicPayrollNotifications.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvElectronicPayrollNotifications.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvElectronicPayrollNotifications.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvElectronicPayrollNotifications.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvElectronicPayrollNotifications.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvElectronicPayrollNotifications.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvElectronicPayrollNotifications.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvElectronicPayrollNotifications.Appearance.Row.Options.UseFont = True
        Me.INDgvElectronicPayrollNotifications.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDcolElectronicPayrollNotifications_Email, Me.INDcolElectronicPayrollNotifications_Status, Me.INDcolElectronicPayrollNotifications_ShippingDate, Me.INDcolElectronicPayrollNotifications_CreationDate})
        Me.INDgvElectronicPayrollNotifications.GridControl = Me.INDGcElectronicPayrollNotifications
        Me.INDgvElectronicPayrollNotifications.Name = "INDgvElectronicPayrollNotifications"
        Me.INDgvElectronicPayrollNotifications.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvElectronicPayrollNotifications.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvElectronicPayrollNotifications.OptionsView.ShowAutoFilterRow = True
        Me.INDgvElectronicPayrollNotifications.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvElectronicPayrollNotifications, False)
        '
        'INDcolElectronicPayrollNotifications_Email
        '
        Me.INDcolElectronicPayrollNotifications_Email.Caption = "Correo"
        Me.INDcolElectronicPayrollNotifications_Email.FieldName = "Email"
        Me.INDcolElectronicPayrollNotifications_Email.Name = "INDcolElectronicPayrollNotifications_Email"
        Me.INDcolElectronicPayrollNotifications_Email.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicPayrollNotifications_Email.OptionsColumn.AllowFocus = False
        Me.INDcolElectronicPayrollNotifications_Email.Visible = True
        Me.INDcolElectronicPayrollNotifications_Email.VisibleIndex = 0
        Me.INDcolElectronicPayrollNotifications_Email.Width = 457
        '
        'INDcolElectronicPayrollNotifications_Status
        '
        Me.INDcolElectronicPayrollNotifications_Status.Caption = "Estado"
        Me.INDcolElectronicPayrollNotifications_Status.FieldName = "StatusName"
        Me.INDcolElectronicPayrollNotifications_Status.Name = "INDcolElectronicPayrollNotifications_Status"
        Me.INDcolElectronicPayrollNotifications_Status.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicPayrollNotifications_Status.OptionsColumn.AllowFocus = False
        Me.INDcolElectronicPayrollNotifications_Status.Visible = True
        Me.INDcolElectronicPayrollNotifications_Status.VisibleIndex = 1
        Me.INDcolElectronicPayrollNotifications_Status.Width = 174
        '
        'INDcolElectronicPayrollNotifications_ShippingDate
        '
        Me.INDcolElectronicPayrollNotifications_ShippingDate.Caption = "Fecha de Envío"
        Me.INDcolElectronicPayrollNotifications_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDcolElectronicPayrollNotifications_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolElectronicPayrollNotifications_ShippingDate.FieldName = "ShippingDate"
        Me.INDcolElectronicPayrollNotifications_ShippingDate.Name = "INDcolElectronicPayrollNotifications_ShippingDate"
        Me.INDcolElectronicPayrollNotifications_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicPayrollNotifications_ShippingDate.OptionsColumn.AllowFocus = False
        Me.INDcolElectronicPayrollNotifications_ShippingDate.Visible = True
        Me.INDcolElectronicPayrollNotifications_ShippingDate.VisibleIndex = 2
        Me.INDcolElectronicPayrollNotifications_ShippingDate.Width = 185
        '
        'INDcolElectronicPayrollNotifications_CreationDate
        '
        Me.INDcolElectronicPayrollNotifications_CreationDate.Caption = "Fecha Creación"
        Me.INDcolElectronicPayrollNotifications_CreationDate.DisplayFormat.FormatString = "G"
        Me.INDcolElectronicPayrollNotifications_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDcolElectronicPayrollNotifications_CreationDate.FieldName = "CreationDate"
        Me.INDcolElectronicPayrollNotifications_CreationDate.Name = "INDcolElectronicPayrollNotifications_CreationDate"
        Me.INDcolElectronicPayrollNotifications_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDcolElectronicPayrollNotifications_CreationDate.OptionsColumn.AllowFocus = False
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciElectronicPayrollNotifications})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(858, 128)
        Me.Root.TextVisible = False
        '
        'INDLciElectronicPayrollNotifications
        '
        Me.INDLciElectronicPayrollNotifications.Control = Me.INDGcElectronicPayrollNotifications
        Me.INDLciElectronicPayrollNotifications.Location = New System.Drawing.Point(0, 0)
        Me.INDLciElectronicPayrollNotifications.Name = "INDLciElectronicPayrollNotifications"
        Me.INDLciElectronicPayrollNotifications.Size = New System.Drawing.Size(838, 108)
        Me.INDLciElectronicPayrollNotifications.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciElectronicPayrollNotifications.TextVisible = False
        '
        'INDPccElectronicPayrollDetails
        '
        Me.INDPccElectronicPayrollDetails.Controls.Add(Me.INDLcElectronicPayrollDetails)
        Me.INDPccElectronicPayrollDetails.Location = New System.Drawing.Point(54, 242)
        Me.INDPccElectronicPayrollDetails.Name = "INDPccElectronicPayrollDetails"
        Me.INDPccElectronicPayrollDetails.Size = New System.Drawing.Size(861, 149)
        Me.INDPccElectronicPayrollDetails.TabIndex = 24
        '
        'INDLcElectronicPayrollDetails
        '
        Me.INDLcElectronicPayrollDetails.Controls.Add(Me.INDGcElectronicPayrollDetails)
        Me.INDLcElectronicPayrollDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcElectronicPayrollDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDLcElectronicPayrollDetails.Name = "INDLcElectronicPayrollDetails"
        Me.INDLcElectronicPayrollDetails.Root = Me.INDLcgElectronicPayrollDetails
        Me.INDLcElectronicPayrollDetails.Size = New System.Drawing.Size(861, 149)
        Me.INDLcElectronicPayrollDetails.TabIndex = 0
        Me.INDLcElectronicPayrollDetails.Text = "LayoutControl1"
        '
        'INDGcElectronicPayrollDetails
        '
        Me.INDGcElectronicPayrollDetails.Location = New System.Drawing.Point(12, 12)
        Me.INDGcElectronicPayrollDetails.MainView = Me.INDGvElectronicPayrollDetails
        Me.INDGcElectronicPayrollDetails.Name = "INDGcElectronicPayrollDetails"
        Me.INDGcElectronicPayrollDetails.Size = New System.Drawing.Size(837, 125)
        Me.INDGcElectronicPayrollDetails.TabIndex = 4
        Me.INDGcElectronicPayrollDetails.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvElectronicPayrollDetails})
        '
        'INDGvElectronicPayrollDetails
        '
        Me.INDGvElectronicPayrollDetails.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvElectronicPayrollDetails.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvElectronicPayrollDetails.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvElectronicPayrollDetails.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvElectronicPayrollDetails.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvElectronicPayrollDetails.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvElectronicPayrollDetails.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvElectronicPayrollDetails.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvElectronicPayrollDetails.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvElectronicPayrollDetails.Appearance.Row.Options.UseFont = True
        Me.INDGvElectronicPayrollDetails.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvElectronicPayrollDetails_Destination, Me.INDGvElectronicPayrollDetails_Status, Me.INDGvElectronicPayrollDetails_Response, Me.INDGvElectronicPayrollDetails_Comments, Me.INDGvElectronicPayrollDetails_ResponseData, Me.INDGvElectronicPayrollDetails_CreationDate})
        Me.INDGvElectronicPayrollDetails.GridControl = Me.INDGcElectronicPayrollDetails
        Me.INDGvElectronicPayrollDetails.Name = "INDGvElectronicPayrollDetails"
        Me.INDGvElectronicPayrollDetails.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvElectronicPayrollDetails.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvElectronicPayrollDetails.OptionsView.ShowAutoFilterRow = True
        Me.INDGvElectronicPayrollDetails.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvElectronicPayrollDetails, False)
        '
        'INDGvElectronicPayrollDetails_Destination
        '
        Me.INDGvElectronicPayrollDetails_Destination.Caption = "Destino"
        Me.INDGvElectronicPayrollDetails_Destination.FieldName = "DestinationName"
        Me.INDGvElectronicPayrollDetails_Destination.Name = "INDGvElectronicPayrollDetails_Destination"
        Me.INDGvElectronicPayrollDetails_Destination.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollDetails_Destination.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollDetails_Destination.OptionsColumn.FixedWidth = True
        Me.INDGvElectronicPayrollDetails_Destination.Visible = True
        Me.INDGvElectronicPayrollDetails_Destination.VisibleIndex = 0
        '
        'INDGvElectronicPayrollDetails_Status
        '
        Me.INDGvElectronicPayrollDetails_Status.Caption = "Estado"
        Me.INDGvElectronicPayrollDetails_Status.FieldName = "StatusName"
        Me.INDGvElectronicPayrollDetails_Status.Name = "INDGvElectronicPayrollDetails_Status"
        Me.INDGvElectronicPayrollDetails_Status.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollDetails_Status.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollDetails_Status.OptionsColumn.FixedWidth = True
        Me.INDGvElectronicPayrollDetails_Status.Visible = True
        Me.INDGvElectronicPayrollDetails_Status.VisibleIndex = 1
        '
        'INDGvElectronicPayrollDetails_Response
        '
        Me.INDGvElectronicPayrollDetails_Response.Caption = "Código"
        Me.INDGvElectronicPayrollDetails_Response.FieldName = "Response"
        Me.INDGvElectronicPayrollDetails_Response.Name = "INDGvElectronicPayrollDetails_Response"
        Me.INDGvElectronicPayrollDetails_Response.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollDetails_Response.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollDetails_Response.OptionsColumn.FixedWidth = True
        Me.INDGvElectronicPayrollDetails_Response.Visible = True
        Me.INDGvElectronicPayrollDetails_Response.VisibleIndex = 2
        '
        'INDGvElectronicPayrollDetails_Comments
        '
        Me.INDGvElectronicPayrollDetails_Comments.Caption = "Comentario"
        Me.INDGvElectronicPayrollDetails_Comments.FieldName = "Comments"
        Me.INDGvElectronicPayrollDetails_Comments.Name = "INDGvElectronicPayrollDetails_Comments"
        Me.INDGvElectronicPayrollDetails_Comments.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollDetails_Comments.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollDetails_Comments.OptionsColumn.FixedWidth = True
        Me.INDGvElectronicPayrollDetails_Comments.Visible = True
        Me.INDGvElectronicPayrollDetails_Comments.VisibleIndex = 3
        '
        'INDGvElectronicPayrollDetails_ResponseData
        '
        Me.INDGvElectronicPayrollDetails_ResponseData.Caption = "Respuesta"
        Me.INDGvElectronicPayrollDetails_ResponseData.FieldName = "ResponseData"
        Me.INDGvElectronicPayrollDetails_ResponseData.Name = "INDGvElectronicPayrollDetails_ResponseData"
        Me.INDGvElectronicPayrollDetails_ResponseData.OptionsColumn.FixedWidth = True
        Me.INDGvElectronicPayrollDetails_ResponseData.Visible = True
        Me.INDGvElectronicPayrollDetails_ResponseData.VisibleIndex = 4
        '
        'INDGvElectronicPayrollDetails_CreationDate
        '
        Me.INDGvElectronicPayrollDetails_CreationDate.Caption = "Fecha Creación"
        Me.INDGvElectronicPayrollDetails_CreationDate.DisplayFormat.FormatString = "G"
        Me.INDGvElectronicPayrollDetails_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvElectronicPayrollDetails_CreationDate.FieldName = "CreationDate"
        Me.INDGvElectronicPayrollDetails_CreationDate.Name = "INDGvElectronicPayrollDetails_CreationDate"
        Me.INDGvElectronicPayrollDetails_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollDetails_CreationDate.OptionsColumn.AllowFocus = False
        '
        'INDLcgElectronicPayrollDetails
        '
        Me.INDLcgElectronicPayrollDetails.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgElectronicPayrollDetails.AppearanceGroup.Options.UseFont = True
        Me.INDLcgElectronicPayrollDetails.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgElectronicPayrollDetails.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollDetails.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgElectronicPayrollDetails, False)
        Me.INDLcgElectronicPayrollDetails.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgElectronicPayrollDetails.GroupBordersVisible = False
        Me.INDLcgElectronicPayrollDetails.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciElectronicPayrollDetails})
        Me.INDLcgElectronicPayrollDetails.Name = "INDLcgElectronicPayrollDetails"
        Me.INDLcgElectronicPayrollDetails.Size = New System.Drawing.Size(861, 149)
        Me.INDLcgElectronicPayrollDetails.TextVisible = False
        '
        'INDLciElectronicPayrollDetails
        '
        Me.INDLciElectronicPayrollDetails.Control = Me.INDGcElectronicPayrollDetails
        Me.INDLciElectronicPayrollDetails.Location = New System.Drawing.Point(0, 0)
        Me.INDLciElectronicPayrollDetails.Name = "INDLciElectronicPayrollDetails"
        Me.INDLciElectronicPayrollDetails.Size = New System.Drawing.Size(841, 129)
        Me.INDLciElectronicPayrollDetails.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciElectronicPayrollDetails.TextVisible = False
        '
        'INDGcElectronicPayrollPaymentSupport
        '
        Me.INDGcElectronicPayrollPaymentSupport.Location = New System.Drawing.Point(24, 55)
        Me.INDGcElectronicPayrollPaymentSupport.MainView = Me.INDGvElectronicPayrollPaymentSupport
        Me.INDGcElectronicPayrollPaymentSupport.Name = "INDGcElectronicPayrollPaymentSupport"
        Me.INDGcElectronicPayrollPaymentSupport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvElectronicPayrollPaymentSupport_PceDetails, Me.INDGvElectronicPayrollPaymentSupport_PceNotifications, Me.INDRepCbeRequestsAlert, Me.INDBtnDownload})
        Me.INDGcElectronicPayrollPaymentSupport.Size = New System.Drawing.Size(962, 507)
        Me.INDGcElectronicPayrollPaymentSupport.TabIndex = 23
        Me.INDGcElectronicPayrollPaymentSupport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvElectronicPayrollPaymentSupport})
        '
        'INDGvElectronicPayrollPaymentSupport
        '
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvElectronicPayrollPaymentSupport.Appearance.Row.Options.UseFont = True
        Me.INDGvElectronicPayrollPaymentSupport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection, Me.INDGvElectronicPayrollPaymentSupport_Alert, Me.INDGvElectronicPayrollPaymentSupport_Period, Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber, Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName, Me.INDGvElectronicPayrollPaymentSupport_StatusName, Me.INDGvElectronicPayrollPaymentSupport_CreationDate, Me.INDGvElectronicPayrollPaymentSupport_ShippingDate, Me.INDGvElectronicPayrollPaymentSupport_Download, Me.INDGvElectronicPayrollPaymentSupport_Details, Me.INDGvElectronicPayrollPaymentSupport_Notifications, Me.INDGvElectronicPayrollPaymentSupport_CUNE})
        Me.INDGvElectronicPayrollPaymentSupport.GridControl = Me.INDGcElectronicPayrollPaymentSupport
        Me.INDGvElectronicPayrollPaymentSupport.Name = "INDGvElectronicPayrollPaymentSupport"
        Me.INDGvElectronicPayrollPaymentSupport.OptionsSelection.MultiSelect = True
        Me.INDGvElectronicPayrollPaymentSupport.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvElectronicPayrollPaymentSupport.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvElectronicPayrollPaymentSupport.OptionsView.ShowAutoFilterRow = True
        Me.INDGvElectronicPayrollPaymentSupport.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvElectronicPayrollPaymentSupport, False)
        '
        'INDGvElectronicPayrollPaymentSupport_UnboundSelection
        '
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.Caption = " "
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.FieldName = "INDGvElectronicPayrollPaymentSupport_UnboundSelection"
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.Name = "INDGvElectronicPayrollPaymentSupport_UnboundSelection"
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.VisibleIndex = 0
        Me.INDGvElectronicPayrollPaymentSupport_UnboundSelection.Width = 40
        '
        'INDGvElectronicPayrollPaymentSupport_Alert
        '
        Me.INDGvElectronicPayrollPaymentSupport_Alert.Caption = " "
        Me.INDGvElectronicPayrollPaymentSupport_Alert.ColumnEdit = Me.INDRepCbeRequestsAlert
        Me.INDGvElectronicPayrollPaymentSupport_Alert.FieldName = "Alert"
        Me.INDGvElectronicPayrollPaymentSupport_Alert.ImageOptions.Alignment = System.Drawing.StringAlignment.Center
        Me.INDGvElectronicPayrollPaymentSupport_Alert.ImageOptions.Image = Global.Presentation.Payroll.My.Resources.Resources.Alerta
        Me.INDGvElectronicPayrollPaymentSupport_Alert.MaxWidth = 40
        Me.INDGvElectronicPayrollPaymentSupport_Alert.MinWidth = 40
        Me.INDGvElectronicPayrollPaymentSupport_Alert.Name = "INDGvElectronicPayrollPaymentSupport_Alert"
        Me.INDGvElectronicPayrollPaymentSupport_Alert.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_Alert.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_Alert.ToolTip = "Alertas"
        Me.INDGvElectronicPayrollPaymentSupport_Alert.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_Alert.VisibleIndex = 1
        Me.INDGvElectronicPayrollPaymentSupport_Alert.Width = 40
        '
        'INDRepCbeRequestsAlert
        '
        Me.INDRepCbeRequestsAlert.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDRepCbeRequestsAlert.AutoHeight = False
        Me.INDRepCbeRequestsAlert.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRepCbeRequestsAlert.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDRepCbeRequestsAlert.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Normal", False, -1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Alerta", True, 0)})
        Me.INDRepCbeRequestsAlert.Name = "INDRepCbeRequestsAlert"
        Me.INDRepCbeRequestsAlert.ReadOnly = True
        Me.INDRepCbeRequestsAlert.SmallImages = Me.INDicAlertas
        '
        'INDicAlertas
        '
        Me.INDicAlertas.ImageStream = CType(resources.GetObject("INDicAlertas.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDicAlertas.Images.SetKeyName(1, "filtra16x16.png")
        Me.INDicAlertas.Images.SetKeyName(2, "Escala Rass.png")
        Me.INDicAlertas.Images.SetKeyName(3, "Escalas Advertencia.png")
        Me.INDicAlertas.Images.SetKeyName(4, "riesgo-caida.png")
        Me.INDicAlertas.Images.SetKeyName(5, "RiesgoAgre.png")
        Me.INDicAlertas.Images.SetKeyName(6, "correto16.png")
        '
        'INDGvElectronicPayrollPaymentSupport_Period
        '
        Me.INDGvElectronicPayrollPaymentSupport_Period.Caption = "Periodo"
        Me.INDGvElectronicPayrollPaymentSupport_Period.FieldName = "Period"
        Me.INDGvElectronicPayrollPaymentSupport_Period.Name = "INDGvElectronicPayrollPaymentSupport_Period"
        Me.INDGvElectronicPayrollPaymentSupport_Period.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_Period.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_Period.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_Period.VisibleIndex = 2
        '
        'INDGvElectronicPayrollPaymentSupport_DocumentNumber
        '
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.Caption = "Número de Documento"
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.FieldName = "DocumentNumberWithPrefix"
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.Name = "INDGvElectronicPayrollPaymentSupport_DocumentNumber"
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.VisibleIndex = 3
        Me.INDGvElectronicPayrollPaymentSupport_DocumentNumber.Width = 101
        '
        'INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName
        '
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.Caption = "Empleado"
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.FieldName = "EmployeePartyNitName"
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.Name = "INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName"
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.VisibleIndex = 4
        Me.INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName.Width = 172
        '
        'INDGvElectronicPayrollPaymentSupport_StatusName
        '
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.Caption = "Estado"
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.FieldName = "StatusName"
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.Name = "INDGvElectronicPayrollPaymentSupport_StatusName"
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.VisibleIndex = 5
        Me.INDGvElectronicPayrollPaymentSupport_StatusName.Width = 101
        '
        'INDGvElectronicPayrollPaymentSupport_CreationDate
        '
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.Caption = "Fecha del Documento"
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.DisplayFormat.FormatString = "G"
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.FieldName = "CreationDate"
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.Name = "INDGvElectronicPayrollPaymentSupport_CreationDate"
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.VisibleIndex = 6
        Me.INDGvElectronicPayrollPaymentSupport_CreationDate.Width = 101
        '
        'INDGvElectronicPayrollPaymentSupport_ShippingDate
        '
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.Caption = "Fecha de Envío"
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.Name = "INDGvElectronicPayrollPaymentSupport_ShippingDate"
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.OptionsColumn.AllowFocus = False
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.VisibleIndex = 7
        Me.INDGvElectronicPayrollPaymentSupport_ShippingDate.Width = 101
        '
        'INDGvElectronicPayrollPaymentSupport_Download
        '
        Me.INDGvElectronicPayrollPaymentSupport_Download.Caption = "Descarga"
        Me.INDGvElectronicPayrollPaymentSupport_Download.ColumnEdit = Me.INDBtnDownload
        Me.INDGvElectronicPayrollPaymentSupport_Download.MaxWidth = 65
        Me.INDGvElectronicPayrollPaymentSupport_Download.MinWidth = 65
        Me.INDGvElectronicPayrollPaymentSupport_Download.Name = "INDGvElectronicPayrollPaymentSupport_Download"
        Me.INDGvElectronicPayrollPaymentSupport_Download.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_Download.VisibleIndex = 8
        Me.INDGvElectronicPayrollPaymentSupport_Download.Width = 65
        '
        'INDBtnDownload
        '
        Me.INDBtnDownload.AutoHeight = False
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDBtnDownload.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnDownload.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDBtnDownload.Name = "INDBtnDownload"
        Me.INDBtnDownload.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvElectronicPayrollPaymentSupport_Details
        '
        Me.INDGvElectronicPayrollPaymentSupport_Details.Caption = "Detalle"
        Me.INDGvElectronicPayrollPaymentSupport_Details.ColumnEdit = Me.INDGvElectronicPayrollPaymentSupport_PceDetails
        Me.INDGvElectronicPayrollPaymentSupport_Details.FieldName = "INDGvElectronicPayrollPaymentSupport_Details"
        Me.INDGvElectronicPayrollPaymentSupport_Details.Name = "INDGvElectronicPayrollPaymentSupport_Details"
        Me.INDGvElectronicPayrollPaymentSupport_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.INDGvElectronicPayrollPaymentSupport_Details.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_Details.VisibleIndex = 9
        Me.INDGvElectronicPayrollPaymentSupport_Details.Width = 50
        '
        'INDGvElectronicPayrollPaymentSupport_PceDetails
        '
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails.AutoHeight = False
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails.Name = "INDGvElectronicPayrollPaymentSupport_PceDetails"
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails.PopupControl = Me.INDPccElectronicPayrollDetails
        Me.INDGvElectronicPayrollPaymentSupport_PceDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvElectronicPayrollPaymentSupport_Notifications
        '
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.Caption = "Notificación"
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.ColumnEdit = Me.INDGvElectronicPayrollPaymentSupport_PceNotifications
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.FieldName = "INDGvElectronicPayrollPaymentSupport_Notifications"
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.Name = "INDGvElectronicPayrollPaymentSupport_Notifications"
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.Visible = True
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.VisibleIndex = 10
        Me.INDGvElectronicPayrollPaymentSupport_Notifications.Width = 67
        '
        'INDGvElectronicPayrollPaymentSupport_PceNotifications
        '
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications.AutoHeight = False
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications.Name = "INDGvElectronicPayrollPaymentSupport_PceNotifications"
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications.PopupControl = Me.INDPccElectronicPayrollNotifications
        Me.INDGvElectronicPayrollPaymentSupport_PceNotifications.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvElectronicPayrollPaymentSupport_CUNE
        '
        Me.INDGvElectronicPayrollPaymentSupport_CUNE.Caption = "CUNE"
        Me.INDGvElectronicPayrollPaymentSupport_CUNE.FieldName = "CUNE"
        Me.INDGvElectronicPayrollPaymentSupport_CUNE.Name = "INDGvElectronicPayrollPaymentSupport_CUNE"
        Me.INDGvElectronicPayrollPaymentSupport_CUNE.OptionsColumn.ReadOnly = True
        '
        'INDGcAdjustmentNote
        '
        Me.INDGcAdjustmentNote.Location = New System.Drawing.Point(24, 55)
        Me.INDGcAdjustmentNote.MainView = Me.INDGvAdjustmentNote
        Me.INDGcAdjustmentNote.Name = "INDGcAdjustmentNote"
        Me.INDGcAdjustmentNote.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDGvAdjustmentNote_PceDetails, Me.INDGvAdjustmentNote_PceNotifications})
        Me.INDGcAdjustmentNote.Size = New System.Drawing.Size(962, 507)
        Me.INDGcAdjustmentNote.TabIndex = 21
        Me.INDGcAdjustmentNote.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvAdjustmentNote})
        '
        'INDGvAdjustmentNote
        '
        Me.INDGvAdjustmentNote.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAdjustmentNote.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAdjustmentNote.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAdjustmentNote.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAdjustmentNote.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAdjustmentNote.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAdjustmentNote.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAdjustmentNote.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAdjustmentNote.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAdjustmentNote.Appearance.Row.Options.UseFont = True
        Me.INDGvAdjustmentNote.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvAdjustmentNote_UnboundSelection, Me.INDGvAdjustmentNote_Period, Me.INDGvAdjustmentNote_DocumentNumber, Me.INDGvAdjustmentNote_EmployeePartyNitName, Me.INDGvAdjustmentNote_Status, Me.INDGvAdjustmentNote_CreationDate, Me.INDGvAdjustmentNote_ShippingDate, Me.INDGvAdjustmentNote_Details, Me.INDGvAdjustmentNote_Notifications, Me.INDGvAdjustmentNote_CUNE})
        Me.INDGvAdjustmentNote.GridControl = Me.INDGcAdjustmentNote
        Me.INDGvAdjustmentNote.Name = "INDGvAdjustmentNote"
        Me.INDGvAdjustmentNote.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAdjustmentNote.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.INDGvAdjustmentNote.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAdjustmentNote.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAdjustmentNote.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAdjustmentNote.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvAdjustmentNote, False)
        '
        'INDGvAdjustmentNote_UnboundSelection
        '
        Me.INDGvAdjustmentNote_UnboundSelection.Caption = " "
        Me.INDGvAdjustmentNote_UnboundSelection.FieldName = "INDGvAdjustmentNote_UnboundSelection"
        Me.INDGvAdjustmentNote_UnboundSelection.Name = "INDGvAdjustmentNote_UnboundSelection"
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowMove = False
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowShowHide = False
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowSize = False
        Me.INDGvAdjustmentNote_UnboundSelection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDGvAdjustmentNote_UnboundSelection.UnboundType = DevExpress.Data.UnboundColumnType.[Boolean]
        Me.INDGvAdjustmentNote_UnboundSelection.Visible = True
        Me.INDGvAdjustmentNote_UnboundSelection.VisibleIndex = 0
        Me.INDGvAdjustmentNote_UnboundSelection.Width = 40
        '
        'INDGvAdjustmentNote_Period
        '
        Me.INDGvAdjustmentNote_Period.Caption = "Periodo"
        Me.INDGvAdjustmentNote_Period.FieldName = "Period"
        Me.INDGvAdjustmentNote_Period.Name = "INDGvAdjustmentNote_Period"
        Me.INDGvAdjustmentNote_Period.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_Period.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_Period.Visible = True
        Me.INDGvAdjustmentNote_Period.VisibleIndex = 1
        '
        'INDGvAdjustmentNote_DocumentNumber
        '
        Me.INDGvAdjustmentNote_DocumentNumber.Caption = "Número de Documento"
        Me.INDGvAdjustmentNote_DocumentNumber.FieldName = "DocumentNumberWithPrefix"
        Me.INDGvAdjustmentNote_DocumentNumber.Name = "INDGvAdjustmentNote_DocumentNumber"
        Me.INDGvAdjustmentNote_DocumentNumber.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_DocumentNumber.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_DocumentNumber.Visible = True
        Me.INDGvAdjustmentNote_DocumentNumber.VisibleIndex = 2
        Me.INDGvAdjustmentNote_DocumentNumber.Width = 113
        '
        'INDGvAdjustmentNote_EmployeePartyNitName
        '
        Me.INDGvAdjustmentNote_EmployeePartyNitName.Caption = "Empleado"
        Me.INDGvAdjustmentNote_EmployeePartyNitName.FieldName = "EmployeePartyNitName"
        Me.INDGvAdjustmentNote_EmployeePartyNitName.Name = "INDGvAdjustmentNote_EmployeePartyNitName"
        Me.INDGvAdjustmentNote_EmployeePartyNitName.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_EmployeePartyNitName.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_EmployeePartyNitName.Visible = True
        Me.INDGvAdjustmentNote_EmployeePartyNitName.VisibleIndex = 3
        Me.INDGvAdjustmentNote_EmployeePartyNitName.Width = 193
        '
        'INDGvAdjustmentNote_Status
        '
        Me.INDGvAdjustmentNote_Status.Caption = "Estado"
        Me.INDGvAdjustmentNote_Status.FieldName = "StatusName"
        Me.INDGvAdjustmentNote_Status.Name = "INDGvAdjustmentNote_Status"
        Me.INDGvAdjustmentNote_Status.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_Status.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_Status.Visible = True
        Me.INDGvAdjustmentNote_Status.VisibleIndex = 4
        Me.INDGvAdjustmentNote_Status.Width = 113
        '
        'INDGvAdjustmentNote_CreationDate
        '
        Me.INDGvAdjustmentNote_CreationDate.Caption = "Fecha del Documento"
        Me.INDGvAdjustmentNote_CreationDate.DisplayFormat.FormatString = "G"
        Me.INDGvAdjustmentNote_CreationDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvAdjustmentNote_CreationDate.FieldName = "CreationDate"
        Me.INDGvAdjustmentNote_CreationDate.Name = "INDGvAdjustmentNote_CreationDate"
        Me.INDGvAdjustmentNote_CreationDate.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_CreationDate.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_CreationDate.Visible = True
        Me.INDGvAdjustmentNote_CreationDate.VisibleIndex = 5
        Me.INDGvAdjustmentNote_CreationDate.Width = 113
        '
        'INDGvAdjustmentNote_ShippingDate
        '
        Me.INDGvAdjustmentNote_ShippingDate.Caption = "Fecha de Envío"
        Me.INDGvAdjustmentNote_ShippingDate.DisplayFormat.FormatString = "G"
        Me.INDGvAdjustmentNote_ShippingDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDGvAdjustmentNote_ShippingDate.FieldName = "ShippingDate"
        Me.INDGvAdjustmentNote_ShippingDate.Name = "INDGvAdjustmentNote_ShippingDate"
        Me.INDGvAdjustmentNote_ShippingDate.OptionsColumn.AllowEdit = False
        Me.INDGvAdjustmentNote_ShippingDate.OptionsColumn.AllowFocus = False
        Me.INDGvAdjustmentNote_ShippingDate.Visible = True
        Me.INDGvAdjustmentNote_ShippingDate.VisibleIndex = 6
        Me.INDGvAdjustmentNote_ShippingDate.Width = 113
        '
        'INDGvAdjustmentNote_Details
        '
        Me.INDGvAdjustmentNote_Details.Caption = "Detalle"
        Me.INDGvAdjustmentNote_Details.ColumnEdit = Me.INDGvAdjustmentNote_PceDetails
        Me.INDGvAdjustmentNote_Details.FieldName = "INDGvAdjustmentNote_Details"
        Me.INDGvAdjustmentNote_Details.Name = "INDGvAdjustmentNote_Details"
        Me.INDGvAdjustmentNote_Details.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.INDGvAdjustmentNote_Details.Visible = True
        Me.INDGvAdjustmentNote_Details.VisibleIndex = 7
        Me.INDGvAdjustmentNote_Details.Width = 56
        '
        'INDGvAdjustmentNote_PceDetails
        '
        Me.INDGvAdjustmentNote_PceDetails.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvAdjustmentNote_PceDetails.AutoHeight = False
        Me.INDGvAdjustmentNote_PceDetails.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvAdjustmentNote_PceDetails.Name = "INDGvAdjustmentNote_PceDetails"
        Me.INDGvAdjustmentNote_PceDetails.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvAdjustmentNote_Notifications
        '
        Me.INDGvAdjustmentNote_Notifications.Caption = "Notificación"
        Me.INDGvAdjustmentNote_Notifications.ColumnEdit = Me.INDGvAdjustmentNote_PceNotifications
        Me.INDGvAdjustmentNote_Notifications.FieldName = "INDGvAdjustmentNote_Notifications"
        Me.INDGvAdjustmentNote_Notifications.Name = "INDGvAdjustmentNote_Notifications"
        Me.INDGvAdjustmentNote_Notifications.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.INDGvAdjustmentNote_Notifications.Visible = True
        Me.INDGvAdjustmentNote_Notifications.VisibleIndex = 8
        Me.INDGvAdjustmentNote_Notifications.Width = 83
        '
        'INDGvAdjustmentNote_PceNotifications
        '
        Me.INDGvAdjustmentNote_PceNotifications.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDGvAdjustmentNote_PceNotifications.AutoHeight = False
        Me.INDGvAdjustmentNote_PceNotifications.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGvAdjustmentNote_PceNotifications.Name = "INDGvAdjustmentNote_PceNotifications"
        Me.INDGvAdjustmentNote_PceNotifications.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDGvAdjustmentNote_CUNE
        '
        Me.INDGvAdjustmentNote_CUNE.Caption = "CUDE"
        Me.INDGvAdjustmentNote_CUNE.FieldName = "CUNE"
        Me.INDGvAdjustmentNote_CUNE.Name = "INDGvAdjustmentNote_CUNE"
        Me.INDGvAdjustmentNote_CUNE.OptionsColumn.ReadOnly = True
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDTcgElectronicPayroll, Me.INDLciExportExcel})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1010, 614)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'INDTcgElectronicPayroll
        '
        Me.INDTcgElectronicPayroll.Location = New System.Drawing.Point(0, 0)
        Me.INDTcgElectronicPayroll.Name = "INDTcgElectronicPayroll"
        Me.INDTcgElectronicPayroll.SelectedTabPage = Me.INDLcgElectronicPayrollPaymentSupport
        Me.INDTcgElectronicPayroll.Size = New System.Drawing.Size(990, 566)
        Me.INDTcgElectronicPayroll.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgElectronicPayrollPaymentSupport, Me.INDLcgAdjustmentNote})
        '
        'INDLcgElectronicPayrollPaymentSupport
        '
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceGroup.Options.UseFont = True
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgElectronicPayrollPaymentSupport.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgElectronicPayrollPaymentSupport, False)
        Me.INDLcgElectronicPayrollPaymentSupport.CustomizationFormText = "Soporte Pago Nómina Electrónica"
        Me.INDLcgElectronicPayrollPaymentSupport.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciElectronicPayrollPaymentSupport})
        Me.INDLcgElectronicPayrollPaymentSupport.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgElectronicPayrollPaymentSupport.Name = "INDLcgElectronicPayrollPaymentSupport"
        Me.INDLcgElectronicPayrollPaymentSupport.Size = New System.Drawing.Size(966, 511)
        Me.INDLcgElectronicPayrollPaymentSupport.Text = "Soporte Pago Nómina Electrónica"
        '
        'INDLciElectronicPayrollPaymentSupport
        '
        Me.INDLciElectronicPayrollPaymentSupport.Control = Me.INDGcElectronicPayrollPaymentSupport
        Me.INDLciElectronicPayrollPaymentSupport.Location = New System.Drawing.Point(0, 0)
        Me.INDLciElectronicPayrollPaymentSupport.Name = "INDLciElectronicPayrollPaymentSupport"
        Me.INDLciElectronicPayrollPaymentSupport.Size = New System.Drawing.Size(966, 511)
        Me.INDLciElectronicPayrollPaymentSupport.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciElectronicPayrollPaymentSupport.TextVisible = False
        '
        'INDLcgAdjustmentNote
        '
        Me.INDLcgAdjustmentNote.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAdjustmentNote.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAdjustmentNote.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAdjustmentNote.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAdjustmentNote.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAdjustmentNote.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAdjustmentNote.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAdjustmentNote.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAdjustmentNote.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAdjustmentNote.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAdjustmentNote.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAdjustmentNote.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAdjustmentNote.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAdjustmentNote.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgAdjustmentNote, False)
        Me.INDLcgAdjustmentNote.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAdjustmentNote})
        Me.INDLcgAdjustmentNote.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgAdjustmentNote.Name = "INDLcgAdjustmentNote"
        Me.INDLcgAdjustmentNote.Size = New System.Drawing.Size(966, 511)
        Me.INDLcgAdjustmentNote.Text = "Nota de Ajuste"
        '
        'INDLciAdjustmentNote
        '
        Me.INDLciAdjustmentNote.Control = Me.INDGcAdjustmentNote
        Me.INDLciAdjustmentNote.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAdjustmentNote.Name = "INDLciAdjustmentNote"
        Me.INDLciAdjustmentNote.Size = New System.Drawing.Size(966, 511)
        Me.INDLciAdjustmentNote.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdjustmentNote.TextVisible = False
        '
        'INDLciExportExcel
        '
        Me.INDLciExportExcel.Control = Me.INDGcExportExcel
        Me.INDLciExportExcel.Location = New System.Drawing.Point(0, 566)
        Me.INDLciExportExcel.Name = "INDLciExportExcel"
        Me.INDLciExportExcel.Size = New System.Drawing.Size(990, 28)
        Me.INDLciExportExcel.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciExportExcel.TextVisible = False
        Me.INDLciExportExcel.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'GridColumn11
        '
        Me.GridColumn11.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.OptionsColumn.AllowEdit = False
        Me.GridColumn11.OptionsColumn.AllowFocus = False
        Me.GridColumn11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.AllowIncrementalSearch = False
        Me.GridColumn11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.AllowMove = False
        Me.GridColumn11.OptionsColumn.AllowShowHide = False
        Me.GridColumn11.OptionsColumn.AllowSize = False
        Me.GridColumn11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn11.OptionsColumn.ShowCaption = False
        Me.GridColumn11.UnboundType = DevExpress.Data.UnboundColumnType.[Object]
        Me.GridColumn11.Visible = True
        Me.GridColumn11.VisibleIndex = 1
        '
        'INDPopMenuActions2
        '
        Me.INDPopMenuActions2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiSelection), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiUnSelection), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiPrint), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiProcess), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiGenerateAdjustmentNote), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiSendNotification), New DevExpress.XtraBars.LinkPersistInfo(Me.INDBbiExport, True)})
        Me.INDPopMenuActions2.Manager = Me.BarManager2
        Me.INDPopMenuActions2.Name = "INDPopMenuActions2"
        '
        'FrmElectronicPayrollTraceability
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 758)
        Me.Controls.Add(Me.BarDockControl7)
        Me.Controls.Add(Me.BarDockControl8)
        Me.Controls.Add(Me.BarDockControl6)
        Me.Controls.Add(Me.BarDockControl5)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmElectronicPayrollTraceability.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmElectronicPayrollTraceability"
        Me.Opacity = 1.0R
        Me.Tag = "2245"
        Me.Text = "Trazabilidad Nómina Electrónica"
        Me.ViewModeEditHold = True
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
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcMainData.ResumeLayout(False)
        CType(Me.INDGcExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccElectronicPayrollNotifications.ResumeLayout(False)
        CType(Me.INDLcElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcElectronicPayrollNotifications.ResumeLayout(False)
        CType(Me.INDGcElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciElectronicPayrollNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPccElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccElectronicPayrollDetails.ResumeLayout(False)
        CType(Me.INDLcElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcElectronicPayrollDetails.ResumeLayout(False)
        CType(Me.INDGcElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciElectronicPayrollDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRepCbeRequestsAlert, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDicAlertas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnDownload, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvElectronicPayrollPaymentSupport_PceDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvElectronicPayrollPaymentSupport_PceNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcAdjustmentNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAdjustmentNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAdjustmentNote_PceDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAdjustmentNote_PceNotifications, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTcgElectronicPayroll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciElectronicPayrollPaymentSupport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAdjustmentNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdjustmentNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciExportExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDPopMenuActions2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents INDLcMainData As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTcgElectronicPayroll As DevExpress.XtraLayout.TabbedControlGroup
    Friend WithEvents INDLcgAdjustmentNote As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLcgElectronicPayrollPaymentSupport As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcAdjustmentNote As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvAdjustmentNote As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciAdjustmentNote As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcElectronicPayrollPaymentSupport As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvElectronicPayrollPaymentSupport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciElectronicPayrollPaymentSupport As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_EmployeePartyNitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_StatusName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_DocumentNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_EmployeePartyNitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents INDPccElectronicPayrollDetails As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcElectronicPayrollDetails As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgElectronicPayrollDetails As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcElectronicPayrollDetails As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvElectronicPayrollDetails As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciElectronicPayrollDetails As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvElectronicPayrollDetails_Destination As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollDetails_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollDetails_Response As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollDetails_Comments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollDetails_ResponseData As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_PceDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvAdjustmentNote_Details As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_PceDetails As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDPccElectronicPayrollNotifications As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcElectronicPayrollNotifications As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcElectronicPayrollNotifications As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvElectronicPayrollNotifications As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciElectronicPayrollNotifications As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDcolElectronicPayrollNotifications_Email As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolElectronicPayrollNotifications_Status As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolElectronicPayrollNotifications_ShippingDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_Notifications As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_PceNotifications As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_Notifications As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_PceNotifications As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_CUNE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_CUNE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BarDockControl7 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarManager2 As DevExpress.XtraBars.BarManager
    Friend WithEvents BarDockControl5 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl6 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents BarDockControl8 As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDBbiProcess As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDPopMenuActions2 As DevExpress.XtraBars.PopupMenu
    Friend WithEvents INDBbiSendNotification As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiPrint As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_UnboundSelection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcolElectronicPayrollNotifications_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollDetails_CreationDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_Period As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvAdjustmentNote_Period As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiSelection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiUnSelection As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDBbiGenerateAdjustmentNote As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDGcExportExcel As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvExportExcel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciExportExcel As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvExportExcel_Year As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_Month As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_EmployeePartyNit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_EmployeePartyName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_NatureDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_Detail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_DateStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_DateEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_Quantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_Percentage As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_Value As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvExportExcel_CUNE As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBbiExport As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents INDicAlertas As DevExpress.Utils.ImageCollection
    Friend WithEvents INDRepCbeRequestsAlert As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_Alert As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDGvElectronicPayrollPaymentSupport_Download As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDBtnDownload As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
End Class