<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRequestsToAutomaticAllocation
    Inherits Presentation.Controls.FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDPccRequestDetail = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDLcRequestDetail = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcRequestDetail = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRequestDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColRequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgRequestDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciRequestDetail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDBtnOk = New DevExpress.XtraEditors.SimpleButton()
        Me.INDGcRequest = New DevExpress.XtraGrid.GridControl()
        Me.INDGvRequest = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColIdentification = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColAge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDetails = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDRptPceDetail = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.BehaviorManager1 = New DevExpress.Utils.Behaviors.BehaviorManager(Me.components)
        Me.DragDropEvents1 = New DevExpress.Utils.DragDrop.DragDropEvents(Me.components)
        Me.INDColOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDPccRequestDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPccRequestDetail.SuspendLayout()
        CType(Me.INDLcRequestDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRequestDetail.SuspendLayout()
        CType(Me.INDGcRequestDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRequestDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRequestDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequestDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDRptPceDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1221, 560)
        Me.INDPanelControlBase.Visible = True
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1221, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1221, 98)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.AllowCustomization = False
        Me.LayoutControl1.Controls.Add(Me.INDPccRequestDetail)
        Me.LayoutControl1.Controls.Add(Me.INDBtnOk)
        Me.LayoutControl1.Controls.Add(Me.INDGcRequest)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LayoutControl1, False)
        Me.LayoutControl1.Location = New System.Drawing.Point(2, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(370, 75, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1217, 551)
        Me.LayoutControl1.TabIndex = 1
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDPccRequestDetail
        '
        Me.INDPccRequestDetail.Controls.Add(Me.INDLcRequestDetail)
        Me.INDPccRequestDetail.Location = New System.Drawing.Point(178, 201)
        Me.INDPccRequestDetail.Name = "INDPccRequestDetail"
        Me.INDPccRequestDetail.Size = New System.Drawing.Size(861, 149)
        Me.INDPccRequestDetail.TabIndex = 25
        '
        'INDLcRequestDetail
        '
        Me.INDLcRequestDetail.Controls.Add(Me.INDGcRequestDetail)
        Me.INDLcRequestDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRequestDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDLcRequestDetail.Name = "INDLcRequestDetail"
        Me.INDLcRequestDetail.Root = Me.INDLcgRequestDetail
        Me.INDLcRequestDetail.Size = New System.Drawing.Size(861, 149)
        Me.INDLcRequestDetail.TabIndex = 0
        Me.INDLcRequestDetail.Text = "LayoutControl1"
        '
        'INDGcRequestDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRequestDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRequestDetail, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRequestDetail, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRequestDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRequestDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRequestDetail, False)
        Me.INDGcRequestDetail.Location = New System.Drawing.Point(12, 12)
        Me.INDGcRequestDetail.MainView = Me.INDGvRequestDetail
        Me.INDGcRequestDetail.Name = "INDGcRequestDetail"
        Me.INDGcRequestDetail.Size = New System.Drawing.Size(837, 125)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRequestDetail, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcRequestDetail.TabIndex = 4
        Me.INDGcRequestDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRequestDetail})
        '
        'INDGvRequestDetail
        '
        Me.INDGvRequestDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRequestDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRequestDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRequestDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRequestDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequestDetail.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRequestDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequestDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRequestDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRequestDetail.Appearance.Row.Options.UseFont = True
        Me.INDGvRequestDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRequestDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvRequestDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColRequestDate, Me.INDColAdmissionNumber, Me.INDColItem, Me.INDColQuantity})
        Me.INDGvRequestDetail.GridControl = Me.INDGcRequestDetail
        Me.INDGvRequestDetail.Name = "INDGvRequestDetail"
        Me.INDGvRequestDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRequestDetail.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRequestDetail.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRequestDetail.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRequestDetail, False)
        '
        'INDColRequestDate
        '
        Me.INDColRequestDate.Caption = "Fecha"
        Me.INDColRequestDate.FieldName = "RequestDate"
        Me.INDColRequestDate.Name = "INDColRequestDate"
        Me.INDColRequestDate.OptionsColumn.AllowEdit = False
        Me.INDColRequestDate.OptionsColumn.AllowFocus = False
        Me.INDColRequestDate.Visible = True
        Me.INDColRequestDate.VisibleIndex = 0
        Me.INDColRequestDate.Width = 80
        '
        'INDColAdmissionNumber
        '
        Me.INDColAdmissionNumber.Caption = "Ingreso"
        Me.INDColAdmissionNumber.FieldName = "AdmissionNumber"
        Me.INDColAdmissionNumber.Name = "INDColAdmissionNumber"
        Me.INDColAdmissionNumber.OptionsColumn.AllowEdit = False
        Me.INDColAdmissionNumber.OptionsColumn.AllowFocus = False
        Me.INDColAdmissionNumber.Visible = True
        Me.INDColAdmissionNumber.VisibleIndex = 1
        Me.INDColAdmissionNumber.Width = 80
        '
        'INDColItem
        '
        Me.INDColItem.Caption = "Servicio"
        Me.INDColItem.FieldName = "ServiceDescription"
        Me.INDColItem.Name = "INDColItem"
        Me.INDColItem.OptionsColumn.AllowEdit = False
        Me.INDColItem.OptionsColumn.AllowFocus = False
        Me.INDColItem.Visible = True
        Me.INDColItem.VisibleIndex = 2
        Me.INDColItem.Width = 581
        '
        'INDColQuantity
        '
        Me.INDColQuantity.Caption = "Cantidad"
        Me.INDColQuantity.FieldName = "Quantity"
        Me.INDColQuantity.Name = "INDColQuantity"
        Me.INDColQuantity.OptionsColumn.AllowEdit = False
        Me.INDColQuantity.OptionsColumn.AllowFocus = False
        Me.INDColQuantity.Visible = True
        Me.INDColQuantity.VisibleIndex = 3
        Me.INDColQuantity.Width = 78
        '
        'INDLcgRequestDetail
        '
        Me.INDLcgRequestDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRequestDetail.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRequestDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRequestDetail.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRequestDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRequestDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRequestDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgRequestDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRequestDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRequestDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRequestDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRequestDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRequestDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRequestDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRequestDetail, False)
        Me.INDLcgRequestDetail.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgRequestDetail.GroupBordersVisible = False
        Me.INDLcgRequestDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciRequestDetail})
        Me.INDLcgRequestDetail.Name = "INDLcgRequestDetail"
        Me.INDLcgRequestDetail.Size = New System.Drawing.Size(861, 149)
        Me.INDLcgRequestDetail.TextVisible = False
        '
        'INDLciRequestDetail
        '
        Me.INDLciRequestDetail.Control = Me.INDGcRequestDetail
        Me.INDLciRequestDetail.Location = New System.Drawing.Point(0, 0)
        Me.INDLciRequestDetail.Name = "INDLciRequestDetail"
        Me.INDLciRequestDetail.Size = New System.Drawing.Size(841, 129)
        Me.INDLciRequestDetail.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciRequestDetail.TextVisible = False
        '
        'INDBtnOk
        '
        Me.INDBtnOk.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnOk.Appearance.Options.UseFont = True
        Me.INDBtnOk.Location = New System.Drawing.Point(14, 505)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnOk, True)
        Me.INDBtnOk.Name = "INDBtnOk"
        Me.INDBtnOk.Size = New System.Drawing.Size(1189, 32)
        Me.INDBtnOk.StyleController = Me.LayoutControl1
        Me.INDBtnOk.TabIndex = 5
        Me.INDBtnOk.Text = "Aceptar"
        '
        'INDGcRequest
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcRequest, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcRequest, Nothing)
        Me.INDGcRequest.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDGcRequest, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcRequest, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcRequest, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcRequest, False)
        Me.INDGcRequest.Location = New System.Drawing.Point(14, 49)
        Me.INDGcRequest.MainView = Me.INDGvRequest
        Me.INDGcRequest.Name = "INDGcRequest"
        Me.INDGcRequest.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDRptPceDetail})
        Me.INDGcRequest.Size = New System.Drawing.Size(1189, 452)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcRequest, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcRequest.TabIndex = 4
        Me.INDGcRequest.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvRequest})
        '
        'INDGvRequest
        '
        Me.INDGvRequest.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvRequest.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDGvRequest.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequest.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvRequest.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvRequest.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvRequest.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvRequest.Appearance.Row.Options.UseFont = True
        Me.INDGvRequest.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvRequest.Appearance.ViewCaption.Options.UseFont = True
        Me.BehaviorManager1.SetBehaviors(Me.INDGvRequest, New DevExpress.Utils.Behaviors.Behavior() {CType(DevExpress.Utils.DragDrop.DragDropBehavior.Create(GetType(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), True, True, True, True, Me.DragDropEvents1), DevExpress.Utils.Behaviors.Behavior)})
        Me.INDGvRequest.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColOrder, Me.INDColIdentification, Me.INDColName, Me.INDColAge, Me.INDColDetails})
        Me.INDGvRequest.GridControl = Me.INDGcRequest
        Me.INDGvRequest.Name = "INDGvRequest"
        Me.INDGvRequest.OptionsSelection.MultiSelect = True
        Me.INDGvRequest.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvRequest.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvRequest.OptionsView.ShowAutoFilterRow = True
        Me.INDGvRequest.OptionsView.ShowDetailButtons = False
        Me.INDGvRequest.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvRequest, False)
        '
        'INDColIdentification
        '
        Me.INDColIdentification.Caption = "Identificación"
        Me.INDColIdentification.FieldName = "PatientCode"
        Me.INDColIdentification.Name = "INDColIdentification"
        Me.INDColIdentification.OptionsColumn.AllowEdit = False
        Me.INDColIdentification.OptionsColumn.AllowFocus = False
        Me.INDColIdentification.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColIdentification.Visible = True
        Me.INDColIdentification.VisibleIndex = 1
        Me.INDColIdentification.Width = 114
        '
        'INDColName
        '
        Me.INDColName.Caption = "Nombre"
        Me.INDColName.FieldName = "PatientName"
        Me.INDColName.Name = "INDColName"
        Me.INDColName.OptionsColumn.AllowEdit = False
        Me.INDColName.OptionsColumn.AllowFocus = False
        Me.INDColName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColName.Visible = True
        Me.INDColName.VisibleIndex = 2
        Me.INDColName.Width = 715
        '
        'INDColAge
        '
        Me.INDColAge.Caption = "Edad"
        Me.INDColAge.FieldName = "PatientAge"
        Me.INDColAge.Name = "INDColAge"
        Me.INDColAge.OptionsColumn.AllowEdit = False
        Me.INDColAge.OptionsColumn.AllowFocus = False
        Me.INDColAge.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColAge.Visible = True
        Me.INDColAge.VisibleIndex = 3
        Me.INDColAge.Width = 196
        '
        'INDColDetails
        '
        Me.INDColDetails.Caption = "Detalle"
        Me.INDColDetails.ColumnEdit = Me.INDRptPceDetail
        Me.INDColDetails.Name = "INDColDetails"
        Me.INDColDetails.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColDetails.Visible = True
        Me.INDColDetails.VisibleIndex = 4
        Me.INDColDetails.Width = 76
        '
        'INDRptPceDetail
        '
        Me.INDRptPceDetail.AllowDropDownWhenReadOnly = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDRptPceDetail.AutoHeight = False
        Me.INDRptPceDetail.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDRptPceDetail.Name = "INDRptPceDetail"
        Me.INDRptPceDetail.PopupControl = Me.INDPccRequestDetail
        Me.INDRptPceDetail.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
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
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Homologaciones"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1217, 551)
        Me.LayoutControlGroup1.TextVisible = False
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
        Me.LayoutControlGroup2.CustomizationFormText = "Solicitudes"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem1, Me.LayoutControlItem2})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.ShowInCustomizationForm = False
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1217, 551)
        Me.LayoutControlGroup2.Text = "Solicitudes"
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcRequest
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1193, 456)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDBtnOk
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 456)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(0, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(83, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(1193, 36)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDColOrder
        '
        Me.INDColOrder.Caption = "Orden"
        Me.INDColOrder.FieldName = "Order"
        Me.INDColOrder.Name = "INDColOrder"
        Me.INDColOrder.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColOrder.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDColOrder.Visible = True
        Me.INDColOrder.VisibleIndex = 0
        Me.INDColOrder.Width = 70
        '
        'FrmRequestsToAutomaticAllocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1221, 682)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmRequestsToAutomaticAllocation"
        Me.Opacity = 1.0R
        Me.Text = "Solicitudes a Asignar Automáticamente"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDPccRequestDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPccRequestDetail.ResumeLayout(False)
        CType(Me.INDLcRequestDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRequestDetail.ResumeLayout(False)
        CType(Me.INDGcRequestDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRequestDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRequestDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequestDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvRequest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDRptPceDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BehaviorManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDBtnOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDGcRequest As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRequest As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColIdentification As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDColAge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDPccRequestDetail As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDLcRequestDetail As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGcRequestDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvRequestDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDColAdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColRequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcgRequestDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciRequestDetail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColDetails As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDRptPceDetail As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents BehaviorManager1 As DevExpress.Utils.Behaviors.BehaviorManager
    Friend WithEvents DragDropEvents1 As DevExpress.Utils.DragDrop.DragDropEvents
    Friend WithEvents INDColOrder As DevExpress.XtraGrid.Columns.GridColumn
End Class
