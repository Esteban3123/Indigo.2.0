Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAccountAcceptance
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ColumnDefinition1 As DevExpress.XtraLayout.ColumnDefinition = New DevExpress.XtraLayout.ColumnDefinition()
        Dim ColumnDefinition2 As DevExpress.XtraLayout.ColumnDefinition = New DevExpress.XtraLayout.ColumnDefinition()
        Dim ColumnDefinition3 As DevExpress.XtraLayout.ColumnDefinition = New DevExpress.XtraLayout.ColumnDefinition()
        Dim ColumnDefinition4 As DevExpress.XtraLayout.ColumnDefinition = New DevExpress.XtraLayout.ColumnDefinition()
        Dim ColumnDefinition5 As DevExpress.XtraLayout.ColumnDefinition = New DevExpress.XtraLayout.ColumnDefinition()
        Dim ColumnDefinition6 As DevExpress.XtraLayout.ColumnDefinition = New DevExpress.XtraLayout.ColumnDefinition()
        Dim RowDefinition1 As DevExpress.XtraLayout.RowDefinition = New DevExpress.XtraLayout.RowDefinition()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSbLoad = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSleAttentionCenterFilter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvAttentionCenter = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GcCenterCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcCenterName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcAcceptance = New DevExpress.XtraGrid.GridControl()
        Me.INDGvAcceptance = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GcAdmissionNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPatientName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcAdmissionDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPatientCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFunctionalUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcCareGroup = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcBed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcDiagnosis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolios = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolioTotalValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolioType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcFolioStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcAssignedUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcCreationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcModificationUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcTransferStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcTimeStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPreviousUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcPreviousManagementArea = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcComments = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcInvoiceNumber = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcManagementArea = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcTransferDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcInvoiceUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleManagementAreaFilter = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvAttentionCenter1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GcManagementAreaCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GcManagementAreaName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgAcceptance = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciGrid = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcgFilters = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciAttentionCenter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciManagementAreaFilter = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciButton = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcMainData = New DevExpress.XtraLayout.LayoutControl()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GcFilter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgTransfers = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.IndigoGridView = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridView2 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.RepositoryItemPopupContainerEdit11 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDSleAttentionCenterFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAttentionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleManagementAreaFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvAttentionCenter1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgAcceptance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgFilters, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAttentionCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciManagementAreaFilter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciButton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgTransfers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 137)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1687, 711)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 7)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.ToolBars.Size = New System.Drawing.Size(1687, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.BarraBotones.Size = New System.Drawing.Size(1687, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDSbLoad)
        Me.INDLcRoot.Controls.Add(Me.INDSleAttentionCenterFilter)
        Me.INDLcRoot.Controls.Add(Me.INDGcAcceptance)
        Me.INDLcRoot.Controls.Add(Me.INDSleManagementAreaFilter)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(2, 9)
        Me.INDLcRoot.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(998, 418, 748, 569)
        Me.INDLcRoot.Root = Me.INDLcgAcceptance
        Me.INDLcRoot.Size = New System.Drawing.Size(1683, 700)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDSbLoad
        '
        Me.INDSbLoad.Location = New System.Drawing.Point(715, 45)
        Me.INDSbLoad.Margin = New System.Windows.Forms.Padding(0)
        Me.INDSbLoad.Name = "INDSbLoad"
        Me.INDSbLoad.Size = New System.Drawing.Size(193, 27)
        Me.INDSbLoad.StyleController = Me.INDLcRoot
        Me.INDSbLoad.TabIndex = 30
        Me.INDSbLoad.Text = "Cargar"
        '
        'INDSleAttentionCenterFilter
        '
        Me.INDSleAttentionCenterFilter.Location = New System.Drawing.Point(183, 47)
        Me.INDSleAttentionCenterFilter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleAttentionCenterFilter.Name = "INDSleAttentionCenterFilter"
        Me.INDSleAttentionCenterFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleAttentionCenterFilter.Properties.DisplayMember = "CodeName"
        Me.INDSleAttentionCenterFilter.Properties.NullText = ""
        Me.INDSleAttentionCenterFilter.Properties.PopupView = Me.INDGvAttentionCenter
        Me.INDSleAttentionCenterFilter.Properties.ValueMember = "CODCENATE"
        Me.INDSleAttentionCenterFilter.Size = New System.Drawing.Size(158, 22)
        Me.INDSleAttentionCenterFilter.StyleController = Me.INDLcRoot
        Me.INDSleAttentionCenterFilter.TabIndex = 28
        '
        'INDGvAttentionCenter
        '
        Me.INDGvAttentionCenter.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAttentionCenter.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAttentionCenter.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAttentionCenter.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAttentionCenter.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAttentionCenter.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAttentionCenter.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAttentionCenter.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAttentionCenter.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAttentionCenter.Appearance.Row.Options.UseFont = True
        Me.INDGvAttentionCenter.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GcCenterCode, Me.GcCenterName})
        Me.INDGvAttentionCenter.DetailHeight = 431
        Me.INDGvAttentionCenter.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvAttentionCenter.Name = "INDGvAttentionCenter"
        Me.INDGvAttentionCenter.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAttentionCenter.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAttentionCenter.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAttentionCenter.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAttentionCenter.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvAttentionCenter, False)
        Me.IndigoGridView.SetTemaIndigoMetro(Me.INDGvAttentionCenter, False)
        '
        'GcCenterCode
        '
        Me.GcCenterCode.Caption = "Código"
        Me.GcCenterCode.FieldName = "CODCENATE"
        Me.GcCenterCode.MinWidth = 23
        Me.GcCenterCode.Name = "GcCenterCode"
        Me.GcCenterCode.Visible = True
        Me.GcCenterCode.VisibleIndex = 0
        Me.GcCenterCode.Width = 87
        '
        'GcCenterName
        '
        Me.GcCenterName.Caption = "Nombre"
        Me.GcCenterName.FieldName = "NOMCENATE"
        Me.GcCenterName.MinWidth = 23
        Me.GcCenterName.Name = "GcCenterName"
        Me.GcCenterName.Visible = True
        Me.GcCenterName.VisibleIndex = 1
        Me.GcCenterName.Width = 87
        '
        'INDGcAcceptance
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcAcceptance, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcAcceptance, Nothing)
        Me.INDGcAcceptance.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcAcceptance, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcAcceptance, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcAcceptance, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcAcceptance, False)
        Me.INDGcAcceptance.Location = New System.Drawing.Point(12, 74)
        Me.INDGcAcceptance.MainView = Me.INDGvAcceptance
        Me.INDGcAcceptance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDGcAcceptance.Name = "INDGcAcceptance"
        Me.INDGcAcceptance.Size = New System.Drawing.Size(1659, 614)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcAcceptance, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDGcAcceptance.TabIndex = 27
        Me.INDGcAcceptance.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvAcceptance})
        '
        'INDGvAcceptance
        '
        Me.INDGvAcceptance.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAcceptance.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAcceptance.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAcceptance.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAcceptance.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAcceptance.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAcceptance.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAcceptance.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAcceptance.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAcceptance.Appearance.Row.Options.UseFont = True
        Me.INDGvAcceptance.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDGvAcceptance.Appearance.ViewCaption.Options.UseFont = True
        Me.INDGvAcceptance.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GcAdmissionNumber, Me.GcPatientName, Me.GcAdmissionDate, Me.GcPatientCode, Me.GcFunctionalUnit, Me.GcCareGroup, Me.GcBed, Me.GcDiagnosis, Me.GcFolios, Me.GcFolioTotalValue, Me.GcFolioType, Me.GcFolioStatus, Me.GcAssignedUser, Me.GcCreationUser, Me.GcModificationUser, Me.GcTransferStatus, Me.GcTimeStatus, Me.GcPreviousUser, Me.GcPreviousManagementArea, Me.GcComments, Me.GcInvoiceNumber, Me.GcManagementArea, Me.GcTransferDate, Me.GcInvoiceUser})
        Me.INDGvAcceptance.CustomizationFormBounds = New System.Drawing.Rectangle(903, 516, 294, 377)
        Me.INDGvAcceptance.DetailHeight = 431
        Me.INDGvAcceptance.GridControl = Me.INDGcAcceptance
        Me.INDGvAcceptance.Name = "INDGvAcceptance"
        Me.INDGvAcceptance.OptionsSelection.MultiSelect = True
        Me.INDGvAcceptance.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAcceptance.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAcceptance.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAcceptance.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvAcceptance, False)
        Me.IndigoGridView.SetTemaIndigoMetro(Me.INDGvAcceptance, False)
        '
        'GcAdmissionNumber
        '
        Me.GcAdmissionNumber.Caption = "Ingreso"
        Me.GcAdmissionNumber.FieldName = "AdmissionNumber"
        Me.GcAdmissionNumber.MinWidth = 23
        Me.GcAdmissionNumber.Name = "GcAdmissionNumber"
        Me.GcAdmissionNumber.OptionsColumn.AllowEdit = False
        Me.GcAdmissionNumber.Visible = True
        Me.GcAdmissionNumber.VisibleIndex = 0
        Me.GcAdmissionNumber.Width = 90
        '
        'GcPatientName
        '
        Me.GcPatientName.Caption = "Paciente"
        Me.GcPatientName.FieldName = "PatientFullName"
        Me.GcPatientName.MinWidth = 23
        Me.GcPatientName.Name = "GcPatientName"
        Me.GcPatientName.OptionsColumn.AllowEdit = False
        Me.GcPatientName.Visible = True
        Me.GcPatientName.VisibleIndex = 1
        Me.GcPatientName.Width = 89
        '
        'GcAdmissionDate
        '
        Me.GcAdmissionDate.Caption = "Fecha Ingreso"
        Me.GcAdmissionDate.FieldName = "AdmissionDate"
        Me.GcAdmissionDate.MinWidth = 23
        Me.GcAdmissionDate.Name = "GcAdmissionDate"
        Me.GcAdmissionDate.OptionsColumn.AllowEdit = False
        Me.GcAdmissionDate.Width = 125
        '
        'GcPatientCode
        '
        Me.GcPatientCode.Caption = "Identificación"
        Me.GcPatientCode.FieldName = "PatientCode"
        Me.GcPatientCode.MinWidth = 23
        Me.GcPatientCode.Name = "GcPatientCode"
        Me.GcPatientCode.OptionsColumn.AllowEdit = False
        Me.GcPatientCode.Width = 34
        '
        'GcFunctionalUnit
        '
        Me.GcFunctionalUnit.Caption = "Unidad Funcional"
        Me.GcFunctionalUnit.FieldName = "FunctionalUnitName"
        Me.GcFunctionalUnit.MinWidth = 23
        Me.GcFunctionalUnit.Name = "GcFunctionalUnit"
        Me.GcFunctionalUnit.OptionsColumn.AllowEdit = False
        Me.GcFunctionalUnit.Width = 24
        '
        'GcCareGroup
        '
        Me.GcCareGroup.AppearanceCell.Options.UseTextOptions = True
        Me.GcCareGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcCareGroup.Caption = "Grupo de Atención"
        Me.GcCareGroup.FieldName = "CareGroupName"
        Me.GcCareGroup.MinWidth = 23
        Me.GcCareGroup.Name = "GcCareGroup"
        Me.GcCareGroup.OptionsColumn.AllowEdit = False
        Me.GcCareGroup.Width = 57
        '
        'GcBed
        '
        Me.GcBed.Caption = "Cama"
        Me.GcBed.FieldName = "BedNumber"
        Me.GcBed.MinWidth = 23
        Me.GcBed.Name = "GcBed"
        Me.GcBed.OptionsColumn.AllowEdit = False
        Me.GcBed.Width = 31
        '
        'GcDiagnosis
        '
        Me.GcDiagnosis.Caption = "Diagnóstico"
        Me.GcDiagnosis.FieldName = "Diagnosis"
        Me.GcDiagnosis.MinWidth = 23
        Me.GcDiagnosis.Name = "GcDiagnosis"
        Me.GcDiagnosis.OptionsColumn.AllowEdit = False
        Me.GcDiagnosis.Width = 29
        '
        'GcFolios
        '
        Me.GcFolios.AppearanceCell.Options.UseTextOptions = True
        Me.GcFolios.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcFolios.Caption = "Folio"
        Me.GcFolios.FieldName = "FolioNumber"
        Me.GcFolios.MinWidth = 23
        Me.GcFolios.Name = "GcFolios"
        Me.GcFolios.OptionsColumn.AllowEdit = False
        Me.GcFolios.Width = 48
        '
        'GcFolioTotalValue
        '
        Me.GcFolioTotalValue.Caption = "Valor Folio"
        Me.GcFolioTotalValue.FieldName = "FolioTotalValue"
        Me.GcFolioTotalValue.MinWidth = 23
        Me.GcFolioTotalValue.Name = "GcFolioTotalValue"
        Me.GcFolioTotalValue.OptionsColumn.AllowEdit = False
        Me.GcFolioTotalValue.Width = 34
        '
        'GcFolioType
        '
        Me.GcFolioType.Caption = "Categoría Folio"
        Me.GcFolioType.FieldName = "FolioType"
        Me.GcFolioType.MinWidth = 23
        Me.GcFolioType.Name = "GcFolioType"
        Me.GcFolioType.OptionsColumn.AllowEdit = False
        Me.GcFolioType.Width = 34
        '
        'GcFolioStatus
        '
        Me.GcFolioStatus.Caption = "Estado Folio"
        Me.GcFolioStatus.FieldName = "FolioStatusDescription"
        Me.GcFolioStatus.MinWidth = 23
        Me.GcFolioStatus.Name = "GcFolioStatus"
        Me.GcFolioStatus.OptionsColumn.AllowEdit = False
        Me.GcFolioStatus.Width = 50
        '
        'GcAssignedUser
        '
        Me.GcAssignedUser.Caption = "Asignado"
        Me.GcAssignedUser.FieldName = "ReceivingUserCodeName"
        Me.GcAssignedUser.MinWidth = 23
        Me.GcAssignedUser.Name = "GcAssignedUser"
        Me.GcAssignedUser.OptionsColumn.AllowEdit = False
        Me.GcAssignedUser.Width = 85
        '
        'GcCreationUser
        '
        Me.GcCreationUser.Caption = "Usuario de Creación"
        Me.GcCreationUser.FieldName = "AdmissionCreationUser"
        Me.GcCreationUser.MinWidth = 23
        Me.GcCreationUser.Name = "GcCreationUser"
        Me.GcCreationUser.OptionsColumn.AllowEdit = False
        Me.GcCreationUser.Width = 72
        '
        'GcModificationUser
        '
        Me.GcModificationUser.Caption = "Usuario de Modificación"
        Me.GcModificationUser.FieldName = "AdmissionModificationUser"
        Me.GcModificationUser.MinWidth = 23
        Me.GcModificationUser.Name = "GcModificationUser"
        Me.GcModificationUser.OptionsColumn.AllowEdit = False
        Me.GcModificationUser.Width = 48
        '
        'GcTransferStatus
        '
        Me.GcTransferStatus.AppearanceCell.Options.UseTextOptions = True
        Me.GcTransferStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcTransferStatus.Caption = "Estado de Traslado"
        Me.GcTransferStatus.FieldName = "TransferStatus"
        Me.GcTransferStatus.MinWidth = 23
        Me.GcTransferStatus.Name = "GcTransferStatus"
        Me.GcTransferStatus.OptionsColumn.AllowEdit = False
        Me.GcTransferStatus.Visible = True
        Me.GcTransferStatus.VisibleIndex = 3
        Me.GcTransferStatus.Width = 83
        '
        'GcTimeStatus
        '
        Me.GcTimeStatus.Caption = "Tiempo"
        Me.GcTimeStatus.FieldName = "TimeStatus"
        Me.GcTimeStatus.MinWidth = 23
        Me.GcTimeStatus.Name = "GcTimeStatus"
        Me.GcTimeStatus.OptionsColumn.AllowEdit = False
        Me.GcTimeStatus.Visible = True
        Me.GcTimeStatus.VisibleIndex = 6
        Me.GcTimeStatus.Width = 87
        '
        'GcPreviousUser
        '
        Me.GcPreviousUser.AppearanceCell.Options.UseTextOptions = True
        Me.GcPreviousUser.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcPreviousUser.Caption = "Usuario que Traslada"
        Me.GcPreviousUser.FieldName = "PreviousUserCodeName"
        Me.GcPreviousUser.MinWidth = 23
        Me.GcPreviousUser.Name = "GcPreviousUser"
        Me.GcPreviousUser.OptionsColumn.AllowEdit = False
        Me.GcPreviousUser.Visible = True
        Me.GcPreviousUser.VisibleIndex = 5
        Me.GcPreviousUser.Width = 99
        '
        'GcPreviousManagementArea
        '
        Me.GcPreviousManagementArea.AppearanceCell.Options.UseTextOptions = True
        Me.GcPreviousManagementArea.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GcPreviousManagementArea.Caption = "Área de Gestión Origen"
        Me.GcPreviousManagementArea.FieldName = "PreviousManagementArea"
        Me.GcPreviousManagementArea.MinWidth = 23
        Me.GcPreviousManagementArea.Name = "GcPreviousManagementArea"
        Me.GcPreviousManagementArea.OptionsColumn.AllowEdit = False
        Me.GcPreviousManagementArea.Visible = True
        Me.GcPreviousManagementArea.VisibleIndex = 4
        Me.GcPreviousManagementArea.Width = 91
        '
        'GcComments
        '
        Me.GcComments.Caption = "Comentario"
        Me.GcComments.FieldName = "Comments"
        Me.GcComments.MinWidth = 23
        Me.GcComments.Name = "GcComments"
        Me.GcComments.OptionsColumn.AllowEdit = False
        Me.GcComments.Width = 66
        '
        'GcInvoiceNumber
        '
        Me.GcInvoiceNumber.Caption = "Número de Factura"
        Me.GcInvoiceNumber.FieldName = "InvoiceNumber"
        Me.GcInvoiceNumber.MinWidth = 23
        Me.GcInvoiceNumber.Name = "GcInvoiceNumber"
        Me.GcInvoiceNumber.OptionsColumn.AllowEdit = False
        Me.GcInvoiceNumber.Width = 50
        '
        'GcManagementArea
        '
        Me.GcManagementArea.Caption = "Area de Gestión"
        Me.GcManagementArea.FieldName = "AdmissionManagementArea"
        Me.GcManagementArea.MinWidth = 23
        Me.GcManagementArea.Name = "GcManagementArea"
        Me.GcManagementArea.Width = 59
        '
        'GcTransferDate
        '
        Me.GcTransferDate.Caption = "Fecha de Traslado"
        Me.GcTransferDate.FieldName = "TransferDate"
        Me.GcTransferDate.MinWidth = 23
        Me.GcTransferDate.Name = "GcTransferDate"
        Me.GcTransferDate.Visible = True
        Me.GcTransferDate.VisibleIndex = 2
        Me.GcTransferDate.Width = 152
        '
        'GcInvoiceUser
        '
        Me.GcInvoiceUser.Caption = "Usuario que Factura"
        Me.GcInvoiceUser.FieldName = "InvoiceUser"
        Me.GcInvoiceUser.MinWidth = 23
        Me.GcInvoiceUser.Name = "GcInvoiceUser"
        Me.GcInvoiceUser.Width = 114
        '
        'INDSleManagementAreaFilter
        '
        Me.INDSleManagementAreaFilter.Location = New System.Drawing.Point(506, 47)
        Me.INDSleManagementAreaFilter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.INDSleManagementAreaFilter.Name = "INDSleManagementAreaFilter"
        Me.INDSleManagementAreaFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleManagementAreaFilter.Properties.DisplayMember = "Name"
        Me.INDSleManagementAreaFilter.Properties.NullText = ""
        Me.INDSleManagementAreaFilter.Properties.PopupView = Me.INDGvAttentionCenter1
        Me.INDSleManagementAreaFilter.Properties.ValueMember = "Code"
        Me.INDSleManagementAreaFilter.Size = New System.Drawing.Size(187, 22)
        Me.INDSleManagementAreaFilter.StyleController = Me.INDLcRoot
        Me.INDSleManagementAreaFilter.TabIndex = 28
        '
        'INDGvAttentionCenter1
        '
        Me.INDGvAttentionCenter1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvAttentionCenter1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvAttentionCenter1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvAttentionCenter1.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvAttentionCenter1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAttentionCenter1.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvAttentionCenter1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvAttentionCenter1.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvAttentionCenter1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvAttentionCenter1.Appearance.Row.Options.UseFont = True
        Me.INDGvAttentionCenter1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GcManagementAreaCode, Me.GcManagementAreaName})
        Me.INDGvAttentionCenter1.DetailHeight = 431
        Me.INDGvAttentionCenter1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvAttentionCenter1.Name = "INDGvAttentionCenter1"
        Me.INDGvAttentionCenter1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvAttentionCenter1.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvAttentionCenter1.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvAttentionCenter1.OptionsView.ShowAutoFilterRow = True
        Me.INDGvAttentionCenter1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView2.SetTemaIndigoMetro(Me.INDGvAttentionCenter1, False)
        Me.IndigoGridView.SetTemaIndigoMetro(Me.INDGvAttentionCenter1, False)
        '
        'GcManagementAreaCode
        '
        Me.GcManagementAreaCode.Caption = "Código"
        Me.GcManagementAreaCode.FieldName = "Code"
        Me.GcManagementAreaCode.MinWidth = 23
        Me.GcManagementAreaCode.Name = "GcManagementAreaCode"
        Me.GcManagementAreaCode.Visible = True
        Me.GcManagementAreaCode.VisibleIndex = 0
        Me.GcManagementAreaCode.Width = 87
        '
        'GcManagementAreaName
        '
        Me.GcManagementAreaName.Caption = "Nombre"
        Me.GcManagementAreaName.FieldName = "Name"
        Me.GcManagementAreaName.MinWidth = 23
        Me.GcManagementAreaName.Name = "GcManagementAreaName"
        Me.GcManagementAreaName.Visible = True
        Me.GcManagementAreaName.VisibleIndex = 1
        Me.GcManagementAreaName.Width = 87
        '
        'INDLcgAcceptance
        '
        Me.INDLcgAcceptance.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAcceptance.AppearanceGroup.Options.UseFont = True
        Me.INDLcgAcceptance.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgAcceptance.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgAcceptance.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAcceptance.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgAcceptance.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgAcceptance.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgAcceptance.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAcceptance.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgAcceptance.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAcceptance.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgAcceptance.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgAcceptance.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDLcgAcceptance.CustomizationFormText = "Aceptación"
        Me.INDLcgAcceptance.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgAcceptance.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciGrid, Me.INDLcgFilters})
        Me.INDLcgAcceptance.Name = "Root"
        Me.INDLcgAcceptance.Size = New System.Drawing.Size(1683, 700)
        Me.INDLcgAcceptance.Text = "Aceptación"
        '
        'INDLciGrid
        '
        Me.INDLciGrid.Control = Me.INDGcAcceptance
        Me.INDLciGrid.Location = New System.Drawing.Point(0, 27)
        Me.INDLciGrid.Name = "INDLciGrid"
        Me.INDLciGrid.Size = New System.Drawing.Size(1663, 618)
        Me.INDLciGrid.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGrid.TextVisible = False
        '
        'INDLcgFilters
        '
        Me.INDLcgFilters.AllowHide = False
        Me.INDLcgFilters.DefaultLayoutType = DevExpress.XtraLayout.Utils.LayoutType.Horizontal
        Me.INDLcgFilters.GroupBordersVisible = False
        Me.INDLcgFilters.GroupStyle = DevExpress.Utils.GroupStyle.Title
        Me.INDLcgFilters.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciAttentionCenter, Me.INDLciManagementAreaFilter, Me.INDLciButton})
        Me.INDLcgFilters.LayoutMode = DevExpress.XtraLayout.Utils.LayoutMode.Table
        Me.INDLcgFilters.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgFilters.Name = "INDLcgFilters"
        ColumnDefinition1.SizeType = System.Windows.Forms.SizeType.AutoSize
        ColumnDefinition1.Width = 333.0R
        ColumnDefinition2.SizeType = System.Windows.Forms.SizeType.AutoSize
        ColumnDefinition2.Width = 20.0R
        ColumnDefinition3.SizeType = System.Windows.Forms.SizeType.AutoSize
        ColumnDefinition3.Width = 332.0R
        ColumnDefinition4.SizeType = System.Windows.Forms.SizeType.AutoSize
        ColumnDefinition4.Width = 20.0R
        ColumnDefinition5.SizeType = System.Windows.Forms.SizeType.AutoSize
        ColumnDefinition5.Width = 193.0R
        ColumnDefinition6.SizeType = System.Windows.Forms.SizeType.AutoSize
        ColumnDefinition6.Width = 765.0R
        Me.INDLcgFilters.OptionsTableLayoutGroup.ColumnDefinitions.AddRange(New DevExpress.XtraLayout.ColumnDefinition() {ColumnDefinition1, ColumnDefinition2, ColumnDefinition3, ColumnDefinition4, ColumnDefinition5, ColumnDefinition6})
        RowDefinition1.Height = 27.0R
        RowDefinition1.SizeType = System.Windows.Forms.SizeType.AutoSize
        Me.INDLcgFilters.OptionsTableLayoutGroup.RowDefinitions.AddRange(New DevExpress.XtraLayout.RowDefinition() {RowDefinition1})
        Me.INDLcgFilters.OptionsTableLayoutGroup.ShrinkEmptyAutoSizeDefinition = DevExpress.Utils.DefaultBoolean.[False]
        Me.INDLcgFilters.Size = New System.Drawing.Size(1663, 27)
        Me.INDLcgFilters.Text = "Filtros"
        Me.INDLcgFilters.TextVisible = False
        '
        'INDLciAttentionCenter
        '
        Me.INDLciAttentionCenter.Control = Me.INDSleAttentionCenterFilter
        Me.INDLciAttentionCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDLciAttentionCenter.Name = "INDLciAttentionCenter"
        Me.INDLciAttentionCenter.Size = New System.Drawing.Size(333, 27)
        Me.INDLciAttentionCenter.Text = "Centro de Atención:"
        Me.INDLciAttentionCenter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciAttentionCenter.TextSize = New System.Drawing.Size(166, 23)
        Me.INDLciAttentionCenter.TextToControlDistance = 5
        '
        'INDLciManagementAreaFilter
        '
        Me.INDLciManagementAreaFilter.Control = Me.INDSleManagementAreaFilter
        Me.INDLciManagementAreaFilter.ControlAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.INDLciManagementAreaFilter.CustomizationFormText = "Area de gestión:"
        Me.INDLciManagementAreaFilter.Location = New System.Drawing.Point(353, 0)
        Me.INDLciManagementAreaFilter.Name = "INDLciManagementAreaFilter"
        Me.INDLciManagementAreaFilter.OptionsTableLayoutItem.ColumnIndex = 2
        Me.INDLciManagementAreaFilter.Size = New System.Drawing.Size(332, 27)
        Me.INDLciManagementAreaFilter.Text = "Area de gestión:"
        Me.INDLciManagementAreaFilter.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize
        Me.INDLciManagementAreaFilter.TextSize = New System.Drawing.Size(136, 23)
        Me.INDLciManagementAreaFilter.TextToControlDistance = 5
        '
        'INDLciButton
        '
        Me.INDLciButton.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDLciButton.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center
        Me.INDLciButton.Control = Me.INDSbLoad
        Me.INDLciButton.Location = New System.Drawing.Point(705, 0)
        Me.INDLciButton.Name = "INDLciButton"
        Me.INDLciButton.OptionsTableLayoutItem.ColumnIndex = 4
        Me.INDLciButton.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0)
        Me.INDLciButton.Size = New System.Drawing.Size(193, 27)
        Me.INDLciButton.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciButton.TextVisible = False
        '
        'INDLcMainData
        '
        Me.INDLcMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcMainData.Name = "INDLcMainData"
        Me.INDLcMainData.Root = Me.LayoutControlGroup1
        Me.INDLcMainData.Size = New System.Drawing.Size(180, 120)
        Me.INDLcMainData.TabIndex = 0
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(180, 120)
        '
        'GcFilter
        '
        Me.GcFilter.Caption = "Tipo Consulta"
        Me.GcFilter.FieldName = "Item2"
        Me.GcFilter.Name = "GcFilter"
        Me.GcFilter.Visible = True
        Me.GcFilter.VisibleIndex = 0
        '
        'INDLcgTransfers
        '
        Me.INDLcgTransfers.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgTransfers.Name = "INDLcgTransfers"
        Me.INDLcgTransfers.Size = New System.Drawing.Size(50, 25)
        '
        'IndigoGridView
        '
        Me.IndigoGridView.RaiseMenuPopUp = True
        Me.IndigoGridView.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'IndigoGridView2
        '
        Me.IndigoGridView2.RaiseMenuPopUp = True
        Me.IndigoGridView2.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit11
        '
        'RepositoryItemPopupContainerEdit11
        '
        Me.RepositoryItemPopupContainerEdit11.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit11.Name = "RepositoryItemPopupContainerEdit11"
        '
        'FrmAccountAcceptance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1687, 848)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "FrmAccountAcceptance"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Tag = "2856"
        Me.Text = "Dashboard Gestión de Cuentas"
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDSleAttentionCenterFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAttentionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleManagementAreaFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvAttentionCenter1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgAcceptance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgFilters, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAttentionCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciManagementAreaFilter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciButton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgTransfers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgAcceptance As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents PanelControl11 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents GCAttentionCenterCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCAttentionCenterName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLcMainData As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents PanelControl3 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents INDLciGrid As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcMain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgMain As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDGcPreviousAlerts As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvPreviousAlerts As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSbAddAlert As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciNewAlert_Comments As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgTraceability As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGcAcceptance As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvAcceptance As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GcAdmissionNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPatientName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcAdmissionDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPatientCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFunctionalUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcCareGroup As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcBed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcDiagnosis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolios As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolioTotalValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolioType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcFolioStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcAssignedUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcCreationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcModificationUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GcTransferStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPreviousUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcPreviousManagementArea As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcComments As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcInvoiceNumber As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDGvFilter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSbCleanFilter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciFilterType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciFilter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GcFilter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCColumn01 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GCColumn02 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents INDSleAttentionCenterFilter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvAttentionCenter As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgFilters As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciAttentionCenter As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLcgTransfers As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSbLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciButton As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents GcManagementArea As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcTransferDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcInvoiceUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcCenterCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcCenterName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcTimeStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView2 As IndigoGridView
    Friend WithEvents RepositoryItemPopupContainerEdit11 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDSleManagementAreaFilter As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvAttentionCenter1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GcManagementAreaCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GcManagementAreaName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciManagementAreaFilter As DevExpress.XtraLayout.LayoutControlItem
End Class
