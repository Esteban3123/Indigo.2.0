Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmWorkOrderModal
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWorkOrderModal))
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleMaintenanceResponsible = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvMaintenanceResponsible = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvMaintenanceResponsible_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvMaintenanceResponsible_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDSleProtocol = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvProtocol = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvProtocol_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvProtocol_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleBranchOffice = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvBranchOffice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvBranchOffice_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvBranchOffice_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDDeProgramDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDDeRequestDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDGcSupplies = New DevExpress.XtraGrid.GridControl()
        Me.INDGvSupplies = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvSupplies_Selected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDChkSuppliesSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDGvSupplies_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvSupplies_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcTools = New DevExpress.XtraGrid.GridControl()
        Me.INDGvTools = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvTools_Selected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDChkToolsSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDGvTools_Plate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvTools_Item = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcConsumables = New DevExpress.XtraGrid.GridControl()
        Me.INDGvConsumables = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvConsumables_Selected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDChkConsumablesSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDGvConsumables_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvConsumables_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGcActivities = New DevExpress.XtraGrid.GridControl()
        Me.INDGvActivities = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvActivities_Selected = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDChkActivitiesSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDGvActivities_Activity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGvActivities_Time = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSeActivitiesTime = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.INDGvActivities_Unit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSleActivitiesUnit = New DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit()
        Me.INDGvActivitiesUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGvActivitiesUnit_Description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDTxtCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLygGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciEquipment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciRequestDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProgramDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciBranchOffice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciProtocol = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciMaintenanceResponsible = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLygActivities = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciActivities = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLygConsumables = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciConsumables = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLygTools = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciTools = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLygSupplies = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciSupplies = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.INDSlePhysicalAsset = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDLciPhysicalAsset = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGvPhysicalAsset = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDSleMaintenanceResponsible.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvMaintenanceResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleProtocol.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvProtocol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleBranchOffice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeProgramDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeProgramDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeRequestDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDDeRequestDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcSupplies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvSupplies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkSuppliesSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcTools, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvTools, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkToolsSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcConsumables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvConsumables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkConsumablesSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGcActivities, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvActivities, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDChkActivitiesSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSeActivitiesTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleActivitiesUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvActivitiesUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciEquipment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciRequestDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProgramDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciProtocol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciMaintenanceResponsible, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygActivities, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciActivities, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygConsumables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciConsumables, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygTools, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciTools, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLygSupplies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciSupplies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSlePhysicalAsset.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciPhysicalAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvPhysicalAsset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1584, 590)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1584, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1584, 98)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 581)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDSlePhysicalAsset)
        Me.INDLcRoot.Controls.Add(Me.INDSleMaintenanceResponsible)
        Me.INDLcRoot.Controls.Add(Me.INDMeDescription)
        Me.INDLcRoot.Controls.Add(Me.INDSleProtocol)
        Me.INDLcRoot.Controls.Add(Me.INDSleBranchOffice)
        Me.INDLcRoot.Controls.Add(Me.INDDeProgramDate)
        Me.INDLcRoot.Controls.Add(Me.INDDeRequestDate)
        Me.INDLcRoot.Controls.Add(Me.INDGcSupplies)
        Me.INDLcRoot.Controls.Add(Me.INDGcTools)
        Me.INDLcRoot.Controls.Add(Me.INDGcConsumables)
        Me.INDLcRoot.Controls.Add(Me.INDGcActivities)
        Me.INDLcRoot.Controls.Add(Me.INDTxtCode)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.LayoutControlGroup1
        Me.INDLcRoot.Size = New System.Drawing.Size(1380, 581)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDSleMaintenanceResponsible
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleMaintenanceResponsible, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleMaintenanceResponsible, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleMaintenanceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.INDSleMaintenanceResponsible.Location = New System.Drawing.Point(24, 439)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleMaintenanceResponsible, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleMaintenanceResponsible.Name = "INDSleMaintenanceResponsible"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.INDSleMaintenanceResponsible.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleMaintenanceResponsible.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleMaintenanceResponsible.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleMaintenanceResponsible.Properties.Appearance.Options.UseFont = True
        Me.INDSleMaintenanceResponsible.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleMaintenanceResponsible.Properties.DisplayMember = "CodeName"
        Me.INDSleMaintenanceResponsible.Properties.NullText = ""
        Me.INDSleMaintenanceResponsible.Properties.PopupSizeable = False
        Me.INDSleMaintenanceResponsible.Properties.PopupView = Me.INDGvMaintenanceResponsible
        Me.INDSleMaintenanceResponsible.Properties.ShowFooter = False
        Me.INDSleMaintenanceResponsible.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleMaintenanceResponsible, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleMaintenanceResponsible, True)
        Me.INDSleMaintenanceResponsible.Size = New System.Drawing.Size(356, 28)
        Me.INDSleMaintenanceResponsible.StyleController = Me.INDLcRoot
        Me.INDSleMaintenanceResponsible.TabIndex = 20
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleMaintenanceResponsible, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleMaintenanceResponsible, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleMaintenanceResponsible, "{0} - {1}")
        Me.INDSleMaintenanceResponsible.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleMaintenanceResponsible, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleMaintenanceResponsible, False)
        '
        'INDGvMaintenanceResponsible
        '
        Me.INDGvMaintenanceResponsible.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvMaintenanceResponsible.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvMaintenanceResponsible.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvMaintenanceResponsible.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvMaintenanceResponsible.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMaintenanceResponsible.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvMaintenanceResponsible.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvMaintenanceResponsible.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvMaintenanceResponsible.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvMaintenanceResponsible.Appearance.Row.Options.UseFont = True
        Me.INDGvMaintenanceResponsible.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvMaintenanceResponsible_Code, Me.INDGvMaintenanceResponsible_Name})
        Me.INDGvMaintenanceResponsible.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvMaintenanceResponsible.Name = "INDGvMaintenanceResponsible"
        Me.INDGvMaintenanceResponsible.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvMaintenanceResponsible.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvMaintenanceResponsible.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvMaintenanceResponsible.OptionsView.ShowAutoFilterRow = True
        Me.INDGvMaintenanceResponsible.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvMaintenanceResponsible, False)
        '
        'INDGvMaintenanceResponsible_Code
        '
        Me.INDGvMaintenanceResponsible_Code.Caption = "Código"
        Me.INDGvMaintenanceResponsible_Code.FieldName = "Code"
        Me.INDGvMaintenanceResponsible_Code.Name = "INDGvMaintenanceResponsible_Code"
        Me.INDGvMaintenanceResponsible_Code.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceResponsible_Code.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceResponsible_Code.Visible = True
        Me.INDGvMaintenanceResponsible_Code.VisibleIndex = 0
        '
        'INDGvMaintenanceResponsible_Name
        '
        Me.INDGvMaintenanceResponsible_Name.Caption = "Nombre"
        Me.INDGvMaintenanceResponsible_Name.FieldName = "CodeNitName"
        Me.INDGvMaintenanceResponsible_Name.Name = "INDGvMaintenanceResponsible_Name"
        Me.INDGvMaintenanceResponsible_Name.OptionsColumn.AllowEdit = False
        Me.INDGvMaintenanceResponsible_Name.OptionsColumn.AllowFocus = False
        Me.INDGvMaintenanceResponsible_Name.Visible = True
        Me.INDGvMaintenanceResponsible_Name.VisibleIndex = 1
        '
        'INDMeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeDescription, True)
        Me.INDMeDescription.Location = New System.Drawing.Point(24, 499)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeDescription.Name = "INDMeDescription"
        Me.INDMeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDMeDescription.Size = New System.Drawing.Size(356, 70)
        Me.INDMeDescription.StyleController = Me.INDLcRoot
        Me.INDMeDescription.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeDescription, 0)
        Me.INDMeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDSleProtocol
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleProtocol, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleProtocol, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleProtocol, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleProtocol, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleProtocol, False)
        Me.INDSleProtocol.Location = New System.Drawing.Point(24, 319)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleProtocol, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleProtocol.Name = "INDSleProtocol"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleProtocol, False)
        Me.INDSleProtocol.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleProtocol.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleProtocol.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleProtocol.Properties.Appearance.Options.UseFont = True
        Me.INDSleProtocol.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleProtocol.Properties.DisplayMember = "CodeName"
        Me.INDSleProtocol.Properties.NullText = ""
        Me.INDSleProtocol.Properties.PopupSizeable = False
        Me.INDSleProtocol.Properties.PopupView = Me.INDGvProtocol
        Me.INDSleProtocol.Properties.ShowFooter = False
        Me.INDSleProtocol.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleProtocol, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleProtocol, True)
        Me.INDSleProtocol.Size = New System.Drawing.Size(356, 28)
        Me.INDSleProtocol.StyleController = Me.INDLcRoot
        Me.INDSleProtocol.TabIndex = 18
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleProtocol, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleProtocol, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleProtocol, "{0} - {1}")
        Me.INDSleProtocol.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleProtocol, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleProtocol, False)
        '
        'INDGvProtocol
        '
        Me.INDGvProtocol.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvProtocol.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvProtocol.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvProtocol.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvProtocol.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProtocol.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvProtocol.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvProtocol.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvProtocol.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvProtocol.Appearance.Row.Options.UseFont = True
        Me.INDGvProtocol.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvProtocol_Code, Me.INDGvProtocol_Name})
        Me.INDGvProtocol.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvProtocol.Name = "INDGvProtocol"
        Me.INDGvProtocol.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvProtocol.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvProtocol.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvProtocol.OptionsView.ShowAutoFilterRow = True
        Me.INDGvProtocol.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvProtocol, False)
        '
        'INDGvProtocol_Code
        '
        Me.INDGvProtocol_Code.Caption = "Código"
        Me.INDGvProtocol_Code.FieldName = "Code"
        Me.INDGvProtocol_Code.Name = "INDGvProtocol_Code"
        Me.INDGvProtocol_Code.OptionsColumn.AllowEdit = False
        Me.INDGvProtocol_Code.OptionsColumn.AllowFocus = False
        Me.INDGvProtocol_Code.Visible = True
        Me.INDGvProtocol_Code.VisibleIndex = 0
        '
        'INDGvProtocol_Name
        '
        Me.INDGvProtocol_Name.Caption = "Nombre"
        Me.INDGvProtocol_Name.FieldName = "Name"
        Me.INDGvProtocol_Name.Name = "INDGvProtocol_Name"
        Me.INDGvProtocol_Name.OptionsColumn.AllowEdit = False
        Me.INDGvProtocol_Name.OptionsColumn.AllowFocus = False
        Me.INDGvProtocol_Name.Visible = True
        Me.INDGvProtocol_Name.VisibleIndex = 1
        '
        'INDSleBranchOffice
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleBranchOffice, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleBranchOffice, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleBranchOffice, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleBranchOffice, False)
        Me.INDSleBranchOffice.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleBranchOffice, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleBranchOffice.Name = "INDSleBranchOffice"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleBranchOffice, False)
        Me.INDSleBranchOffice.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleBranchOffice.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleBranchOffice.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBranchOffice.Properties.Appearance.Options.UseFont = True
        Me.INDSleBranchOffice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleBranchOffice.Properties.DisplayMember = "CodeName"
        Me.INDSleBranchOffice.Properties.NullText = ""
        Me.INDSleBranchOffice.Properties.PopupSizeable = False
        Me.INDSleBranchOffice.Properties.PopupView = Me.INDGvBranchOffice
        Me.INDSleBranchOffice.Properties.ShowFooter = False
        Me.INDSleBranchOffice.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleBranchOffice, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleBranchOffice, True)
        Me.INDSleBranchOffice.Size = New System.Drawing.Size(356, 28)
        Me.INDSleBranchOffice.StyleController = Me.INDLcRoot
        Me.INDSleBranchOffice.TabIndex = 17
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleBranchOffice, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleBranchOffice, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleBranchOffice, "{0} - {1}")
        Me.INDSleBranchOffice.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleBranchOffice, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleBranchOffice, False)
        '
        'INDGvBranchOffice
        '
        Me.INDGvBranchOffice.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvBranchOffice.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvBranchOffice.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvBranchOffice.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvBranchOffice.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBranchOffice.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvBranchOffice.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvBranchOffice.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvBranchOffice.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvBranchOffice.Appearance.Row.Options.UseFont = True
        Me.INDGvBranchOffice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvBranchOffice_Code, Me.INDGvBranchOffice_Name})
        Me.INDGvBranchOffice.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvBranchOffice.Name = "INDGvBranchOffice"
        Me.INDGvBranchOffice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvBranchOffice.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvBranchOffice.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvBranchOffice.OptionsView.ShowAutoFilterRow = True
        Me.INDGvBranchOffice.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvBranchOffice, False)
        '
        'INDGvBranchOffice_Code
        '
        Me.INDGvBranchOffice_Code.Caption = "Código"
        Me.INDGvBranchOffice_Code.FieldName = "Codigo"
        Me.INDGvBranchOffice_Code.Name = "INDGvBranchOffice_Code"
        Me.INDGvBranchOffice_Code.OptionsColumn.AllowEdit = False
        Me.INDGvBranchOffice_Code.OptionsColumn.AllowFocus = False
        Me.INDGvBranchOffice_Code.Visible = True
        Me.INDGvBranchOffice_Code.VisibleIndex = 0
        '
        'INDGvBranchOffice_Name
        '
        Me.INDGvBranchOffice_Name.Caption = "Nombre"
        Me.INDGvBranchOffice_Name.FieldName = "Descripcion"
        Me.INDGvBranchOffice_Name.Name = "INDGvBranchOffice_Name"
        Me.INDGvBranchOffice_Name.OptionsColumn.AllowEdit = False
        Me.INDGvBranchOffice_Name.OptionsColumn.AllowFocus = False
        Me.INDGvBranchOffice_Name.Visible = True
        Me.INDGvBranchOffice_Name.VisibleIndex = 1
        '
        'INDDeProgramDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeProgramDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeProgramDate, True)
        Me.INDDeProgramDate.EditValue = Nothing
        Me.INDDeProgramDate.Location = New System.Drawing.Point(24, 199)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeProgramDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDDeProgramDate.Name = "INDDeProgramDate"
        Me.INDDeProgramDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDeProgramDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeProgramDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeProgramDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeProgramDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeProgramDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeProgramDate.Size = New System.Drawing.Size(356, 28)
        Me.INDDeProgramDate.StyleController = Me.INDLcRoot
        Me.INDDeProgramDate.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeProgramDate, 0)
        Me.INDDeProgramDate.ToolTip = "Este Campo es Necesario"
        '
        'INDDeRequestDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDDeRequestDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDDeRequestDate, True)
        Me.INDDeRequestDate.EditValue = Nothing
        Me.INDDeRequestDate.Location = New System.Drawing.Point(24, 139)
        Me.IndigoTextEdit1.SetMascara(Me.INDDeRequestDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDDeRequestDate.Name = "INDDeRequestDate"
        Me.INDDeRequestDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDDeRequestDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDDeRequestDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDDeRequestDate.Properties.Appearance.Options.UseFont = True
        Me.INDDeRequestDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeRequestDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDDeRequestDate.Size = New System.Drawing.Size(356, 28)
        Me.INDDeRequestDate.StyleController = Me.INDLcRoot
        Me.INDDeRequestDate.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDDeRequestDate, 0)
        Me.INDDeRequestDate.ToolTip = "Este Campo es Necesario"
        '
        'INDGcSupplies
        '
        Me.INDGcSupplies.Location = New System.Drawing.Point(1932, 53)
        Me.INDGcSupplies.MainView = Me.INDGvSupplies
        Me.INDGcSupplies.MinimumSize = New System.Drawing.Size(480, 0)
        Me.INDGcSupplies.Name = "INDGcSupplies"
        Me.INDGcSupplies.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDChkSuppliesSelect})
        Me.INDGcSupplies.Size = New System.Drawing.Size(480, 516)
        Me.INDGcSupplies.TabIndex = 14
        Me.INDGcSupplies.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvSupplies})
        '
        'INDGvSupplies
        '
        Me.INDGvSupplies.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvSupplies.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvSupplies.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvSupplies.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvSupplies.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSupplies.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvSupplies.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvSupplies.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvSupplies.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvSupplies.Appearance.Row.Options.UseFont = True
        Me.INDGvSupplies.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvSupplies_Selected, Me.INDGvSupplies_Code, Me.INDGvSupplies_Name})
        Me.INDGvSupplies.GridControl = Me.INDGcSupplies
        Me.INDGvSupplies.Name = "INDGvSupplies"
        Me.INDGvSupplies.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvSupplies.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvSupplies.OptionsView.ShowAutoFilterRow = True
        Me.INDGvSupplies.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvSupplies, False)
        '
        'INDGvSupplies_Selected
        '
        Me.INDGvSupplies_Selected.Caption = " "
        Me.INDGvSupplies_Selected.ColumnEdit = Me.INDChkSuppliesSelect
        Me.INDGvSupplies_Selected.FieldName = "Selected"
        Me.INDGvSupplies_Selected.Name = "INDGvSupplies_Selected"
        Me.INDGvSupplies_Selected.Visible = True
        Me.INDGvSupplies_Selected.VisibleIndex = 0
        Me.INDGvSupplies_Selected.Width = 40
        '
        'INDChkSuppliesSelect
        '
        Me.INDChkSuppliesSelect.AutoHeight = False
        Me.INDChkSuppliesSelect.Name = "INDChkSuppliesSelect"
        '
        'INDGvSupplies_Code
        '
        Me.INDGvSupplies_Code.Caption = "Código"
        Me.INDGvSupplies_Code.FieldName = "ProductCode"
        Me.INDGvSupplies_Code.Name = "INDGvSupplies_Code"
        Me.INDGvSupplies_Code.OptionsColumn.AllowEdit = False
        Me.INDGvSupplies_Code.OptionsColumn.AllowFocus = False
        Me.INDGvSupplies_Code.Visible = True
        Me.INDGvSupplies_Code.VisibleIndex = 1
        Me.INDGvSupplies_Code.Width = 125
        '
        'INDGvSupplies_Name
        '
        Me.INDGvSupplies_Name.Caption = "Nombre"
        Me.INDGvSupplies_Name.FieldName = "ProductName"
        Me.INDGvSupplies_Name.Name = "INDGvSupplies_Name"
        Me.INDGvSupplies_Name.OptionsColumn.AllowEdit = False
        Me.INDGvSupplies_Name.OptionsColumn.AllowFocus = False
        Me.INDGvSupplies_Name.Visible = True
        Me.INDGvSupplies_Name.VisibleIndex = 2
        Me.INDGvSupplies_Name.Width = 290
        '
        'INDGcTools
        '
        Me.INDGcTools.Location = New System.Drawing.Point(1424, 53)
        Me.INDGcTools.MainView = Me.INDGvTools
        Me.INDGcTools.MinimumSize = New System.Drawing.Size(480, 0)
        Me.INDGcTools.Name = "INDGcTools"
        Me.INDGcTools.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDChkToolsSelect})
        Me.INDGcTools.Size = New System.Drawing.Size(480, 516)
        Me.INDGcTools.TabIndex = 11
        Me.INDGcTools.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvTools})
        '
        'INDGvTools
        '
        Me.INDGvTools.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvTools.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvTools.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvTools.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvTools.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTools.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvTools.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvTools.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvTools.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvTools.Appearance.Row.Options.UseFont = True
        Me.INDGvTools.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvTools_Selected, Me.INDGvTools_Plate, Me.INDGvTools_Item})
        Me.INDGvTools.GridControl = Me.INDGcTools
        Me.INDGvTools.Name = "INDGvTools"
        Me.INDGvTools.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvTools.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvTools.OptionsView.ShowAutoFilterRow = True
        Me.INDGvTools.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvTools, False)
        '
        'INDGvTools_Selected
        '
        Me.INDGvTools_Selected.Caption = " "
        Me.INDGvTools_Selected.ColumnEdit = Me.INDChkToolsSelect
        Me.INDGvTools_Selected.FieldName = "Selected"
        Me.INDGvTools_Selected.Name = "INDGvTools_Selected"
        Me.INDGvTools_Selected.Visible = True
        Me.INDGvTools_Selected.VisibleIndex = 0
        Me.INDGvTools_Selected.Width = 40
        '
        'INDChkToolsSelect
        '
        Me.INDChkToolsSelect.AutoHeight = False
        Me.INDChkToolsSelect.Name = "INDChkToolsSelect"
        '
        'INDGvTools_Plate
        '
        Me.INDGvTools_Plate.Caption = "Placa"
        Me.INDGvTools_Plate.FieldName = "PhysicalAssetPlate"
        Me.INDGvTools_Plate.Name = "INDGvTools_Plate"
        Me.INDGvTools_Plate.OptionsColumn.AllowEdit = False
        Me.INDGvTools_Plate.OptionsColumn.AllowFocus = False
        Me.INDGvTools_Plate.Visible = True
        Me.INDGvTools_Plate.VisibleIndex = 1
        Me.INDGvTools_Plate.Width = 130
        '
        'INDGvTools_Item
        '
        Me.INDGvTools_Item.Caption = "Artículo"
        Me.INDGvTools_Item.FieldName = "PhysicalAssetItemName"
        Me.INDGvTools_Item.Name = "INDGvTools_Item"
        Me.INDGvTools_Item.OptionsColumn.AllowEdit = False
        Me.INDGvTools_Item.OptionsColumn.AllowFocus = False
        Me.INDGvTools_Item.Visible = True
        Me.INDGvTools_Item.VisibleIndex = 2
        Me.INDGvTools_Item.Width = 297
        '
        'INDGcConsumables
        '
        Me.INDGcConsumables.Location = New System.Drawing.Point(916, 53)
        Me.INDGcConsumables.MainView = Me.INDGvConsumables
        Me.INDGcConsumables.MinimumSize = New System.Drawing.Size(480, 0)
        Me.INDGcConsumables.Name = "INDGcConsumables"
        Me.INDGcConsumables.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDChkConsumablesSelect})
        Me.INDGcConsumables.Size = New System.Drawing.Size(480, 516)
        Me.INDGcConsumables.TabIndex = 8
        Me.INDGcConsumables.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvConsumables})
        '
        'INDGvConsumables
        '
        Me.INDGvConsumables.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvConsumables.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvConsumables.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvConsumables.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvConsumables.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvConsumables.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvConsumables.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvConsumables.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvConsumables.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvConsumables.Appearance.Row.Options.UseFont = True
        Me.INDGvConsumables.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvConsumables_Selected, Me.INDGvConsumables_Code, Me.INDGvConsumables_Description})
        Me.INDGvConsumables.GridControl = Me.INDGcConsumables
        Me.INDGvConsumables.Name = "INDGvConsumables"
        Me.INDGvConsumables.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvConsumables.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvConsumables.OptionsView.ShowAutoFilterRow = True
        Me.INDGvConsumables.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvConsumables, False)
        '
        'INDGvConsumables_Selected
        '
        Me.INDGvConsumables_Selected.Caption = " "
        Me.INDGvConsumables_Selected.ColumnEdit = Me.INDChkConsumablesSelect
        Me.INDGvConsumables_Selected.FieldName = "Selected"
        Me.INDGvConsumables_Selected.Name = "INDGvConsumables_Selected"
        Me.INDGvConsumables_Selected.Visible = True
        Me.INDGvConsumables_Selected.VisibleIndex = 0
        Me.INDGvConsumables_Selected.Width = 40
        '
        'INDChkConsumablesSelect
        '
        Me.INDChkConsumablesSelect.AutoHeight = False
        Me.INDChkConsumablesSelect.Name = "INDChkConsumablesSelect"
        '
        'INDGvConsumables_Code
        '
        Me.INDGvConsumables_Code.Caption = "Código"
        Me.INDGvConsumables_Code.FieldName = "ConsumableCode"
        Me.INDGvConsumables_Code.Name = "INDGvConsumables_Code"
        Me.INDGvConsumables_Code.OptionsColumn.AllowEdit = False
        Me.INDGvConsumables_Code.OptionsColumn.AllowFocus = False
        Me.INDGvConsumables_Code.Visible = True
        Me.INDGvConsumables_Code.VisibleIndex = 1
        Me.INDGvConsumables_Code.Width = 109
        '
        'INDGvConsumables_Description
        '
        Me.INDGvConsumables_Description.Caption = "Descripción"
        Me.INDGvConsumables_Description.FieldName = "ConsumableName"
        Me.INDGvConsumables_Description.Name = "INDGvConsumables_Description"
        Me.INDGvConsumables_Description.OptionsColumn.AllowEdit = False
        Me.INDGvConsumables_Description.OptionsColumn.AllowFocus = False
        Me.INDGvConsumables_Description.Visible = True
        Me.INDGvConsumables_Description.VisibleIndex = 2
        Me.INDGvConsumables_Description.Width = 306
        '
        'INDGcActivities
        '
        Me.INDGcActivities.Location = New System.Drawing.Point(408, 53)
        Me.INDGcActivities.MainView = Me.INDGvActivities
        Me.INDGcActivities.MinimumSize = New System.Drawing.Size(480, 0)
        Me.INDGcActivities.Name = "INDGcActivities"
        Me.INDGcActivities.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDChkActivitiesSelect, Me.INDSleActivitiesUnit, Me.INDSeActivitiesTime})
        Me.INDGcActivities.Size = New System.Drawing.Size(480, 516)
        Me.INDGcActivities.TabIndex = 8
        Me.INDGcActivities.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDGvActivities})
        '
        'INDGvActivities
        '
        Me.INDGvActivities.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvActivities.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvActivities.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvActivities.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvActivities.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvActivities.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvActivities.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvActivities.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvActivities.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvActivities.Appearance.Row.Options.UseFont = True
        Me.INDGvActivities.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvActivities_Selected, Me.INDGvActivities_Activity, Me.INDGvActivities_Time, Me.INDGvActivities_Unit})
        Me.INDGvActivities.GridControl = Me.INDGcActivities
        Me.INDGvActivities.Name = "INDGvActivities"
        Me.INDGvActivities.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvActivities.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvActivities.OptionsView.ShowAutoFilterRow = True
        Me.INDGvActivities.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvActivities, False)
        '
        'INDGvActivities_Selected
        '
        Me.INDGvActivities_Selected.Caption = " "
        Me.INDGvActivities_Selected.ColumnEdit = Me.INDChkActivitiesSelect
        Me.INDGvActivities_Selected.FieldName = "Selected"
        Me.INDGvActivities_Selected.Name = "INDGvActivities_Selected"
        Me.INDGvActivities_Selected.Visible = True
        Me.INDGvActivities_Selected.VisibleIndex = 0
        Me.INDGvActivities_Selected.Width = 40
        '
        'INDChkActivitiesSelect
        '
        Me.INDChkActivitiesSelect.AutoHeight = False
        Me.INDChkActivitiesSelect.Name = "INDChkActivitiesSelect"
        '
        'INDGvActivities_Activity
        '
        Me.INDGvActivities_Activity.Caption = "Actividad"
        Me.INDGvActivities_Activity.FieldName = "Activity"
        Me.INDGvActivities_Activity.Name = "INDGvActivities_Activity"
        Me.INDGvActivities_Activity.OptionsColumn.AllowEdit = False
        Me.INDGvActivities_Activity.OptionsColumn.AllowFocus = False
        Me.INDGvActivities_Activity.Visible = True
        Me.INDGvActivities_Activity.VisibleIndex = 1
        Me.INDGvActivities_Activity.Width = 221
        '
        'INDGvActivities_Time
        '
        Me.INDGvActivities_Time.Caption = "Tiempo"
        Me.INDGvActivities_Time.ColumnEdit = Me.INDSeActivitiesTime
        Me.INDGvActivities_Time.DisplayFormat.FormatString = "n0"
        Me.INDGvActivities_Time.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDGvActivities_Time.FieldName = "Time"
        Me.INDGvActivities_Time.Name = "INDGvActivities_Time"
        Me.INDGvActivities_Time.Visible = True
        Me.INDGvActivities_Time.VisibleIndex = 2
        Me.INDGvActivities_Time.Width = 81
        '
        'INDSeActivitiesTime
        '
        Me.INDSeActivitiesTime.AutoHeight = False
        Me.INDSeActivitiesTime.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSeActivitiesTime.Mask.EditMask = "n0"
        Me.INDSeActivitiesTime.Mask.UseMaskAsDisplayFormat = True
        Me.INDSeActivitiesTime.Name = "INDSeActivitiesTime"
        '
        'INDGvActivities_Unit
        '
        Me.INDGvActivities_Unit.Caption = "Unidad"
        Me.INDGvActivities_Unit.ColumnEdit = Me.INDSleActivitiesUnit
        Me.INDGvActivities_Unit.FieldName = "Unit"
        Me.INDGvActivities_Unit.Name = "INDGvActivities_Unit"
        Me.INDGvActivities_Unit.Visible = True
        Me.INDGvActivities_Unit.VisibleIndex = 3
        Me.INDGvActivities_Unit.Width = 113
        '
        'INDSleActivitiesUnit
        '
        Me.INDSleActivitiesUnit.AutoHeight = False
        Me.INDSleActivitiesUnit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleActivitiesUnit.DisplayMember = "Item2"
        Me.INDSleActivitiesUnit.Name = "INDSleActivitiesUnit"
        Me.INDSleActivitiesUnit.NullText = ""
        Me.INDSleActivitiesUnit.PopupView = Me.INDGvActivitiesUnit
        Me.INDSleActivitiesUnit.ValueMember = "Item1"
        '
        'INDGvActivitiesUnit
        '
        Me.INDGvActivitiesUnit.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvActivitiesUnit.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvActivitiesUnit.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvActivitiesUnit.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvActivitiesUnit.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvActivitiesUnit.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvActivitiesUnit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvActivitiesUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvActivitiesUnit.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvActivitiesUnit.Appearance.Row.Options.UseFont = True
        Me.INDGvActivitiesUnit.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDGvActivitiesUnit_Description})
        Me.INDGvActivitiesUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvActivitiesUnit.Name = "INDGvActivitiesUnit"
        Me.INDGvActivitiesUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvActivitiesUnit.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvActivitiesUnit.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvActivitiesUnit.OptionsView.ShowAutoFilterRow = True
        Me.INDGvActivitiesUnit.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvActivitiesUnit, False)
        '
        'INDGvActivitiesUnit_Description
        '
        Me.INDGvActivitiesUnit_Description.Caption = "Descripción"
        Me.INDGvActivitiesUnit_Description.FieldName = "Item2"
        Me.INDGvActivitiesUnit_Description.Name = "INDGvActivitiesUnit_Description"
        Me.INDGvActivitiesUnit_Description.OptionsColumn.AllowEdit = False
        Me.INDGvActivitiesUnit_Description.OptionsColumn.AllowFocus = False
        Me.INDGvActivitiesUnit_Description.Visible = True
        Me.INDGvActivitiesUnit_Description.VisibleIndex = 0
        '
        'INDTxtCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtCode, False)
        Me.INDTxtCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtCode.Name = "INDTxtCode"
        Me.INDTxtCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtCode.Properties.Appearance.Options.UseFont = True
        Me.INDTxtCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro
        Me.INDTxtCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDTxtCode.Size = New System.Drawing.Size(356, 28)
        Me.INDTxtCode.StyleController = Me.INDLcRoot
        Me.INDTxtCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtCode, 0)
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
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLygGeneralData, Me.INDLygActivities, Me.INDLygConsumables, Me.INDLygTools, Me.INDLygSupplies})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(2436, 593)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLygGeneralData
        '
        Me.INDLygGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDLygGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygGeneralData, False)
        Me.INDLygGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciEquipment, Me.INDLciRequestDate, Me.INDLciProgramDate, Me.INDLciBranchOffice, Me.INDLciProtocol, Me.INDLciDescription, Me.INDLciMaintenanceResponsible, Me.INDLciPhysicalAsset})
        Me.INDLygGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDLygGeneralData.Name = "INDLygGeneralData"
        Me.INDLygGeneralData.Size = New System.Drawing.Size(384, 573)
        Me.INDLygGeneralData.Text = "Datos Principales"
        '
        'INDLciEquipment
        '
        Me.INDLciEquipment.Control = Me.INDTxtCode
        Me.INDLciEquipment.Location = New System.Drawing.Point(0, 0)
        Me.INDLciEquipment.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciEquipment.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciEquipment.Name = "INDLciEquipment"
        Me.INDLciEquipment.Size = New System.Drawing.Size(360, 60)
        Me.INDLciEquipment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciEquipment.Text = "Código"
        Me.INDLciEquipment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciEquipment.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciEquipment.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciEquipment.TextToControlDistance = 5
        '
        'INDLciRequestDate
        '
        Me.INDLciRequestDate.AllowHide = False
        Me.INDLciRequestDate.Control = Me.INDDeRequestDate
        Me.INDLciRequestDate.Location = New System.Drawing.Point(0, 60)
        Me.INDLciRequestDate.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciRequestDate.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciRequestDate.Name = "INDLciRequestDate"
        Me.INDLciRequestDate.ShowInCustomizationForm = False
        Me.INDLciRequestDate.Size = New System.Drawing.Size(360, 60)
        Me.INDLciRequestDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciRequestDate.Text = "Fecha Solicitud"
        Me.INDLciRequestDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciRequestDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciRequestDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciRequestDate.TextToControlDistance = 5
        '
        'INDLciProgramDate
        '
        Me.INDLciProgramDate.AllowHide = False
        Me.INDLciProgramDate.Control = Me.INDDeProgramDate
        Me.INDLciProgramDate.Location = New System.Drawing.Point(0, 120)
        Me.INDLciProgramDate.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciProgramDate.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciProgramDate.Name = "INDLciProgramDate"
        Me.INDLciProgramDate.ShowInCustomizationForm = False
        Me.INDLciProgramDate.Size = New System.Drawing.Size(360, 60)
        Me.INDLciProgramDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProgramDate.Text = "Fecha Programada"
        Me.INDLciProgramDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProgramDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProgramDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciProgramDate.TextToControlDistance = 5
        '
        'INDLciBranchOffice
        '
        Me.INDLciBranchOffice.AllowHide = False
        Me.INDLciBranchOffice.Control = Me.INDSleBranchOffice
        Me.INDLciBranchOffice.Location = New System.Drawing.Point(0, 180)
        Me.INDLciBranchOffice.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciBranchOffice.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciBranchOffice.Name = "INDLciBranchOffice"
        Me.INDLciBranchOffice.ShowInCustomizationForm = False
        Me.INDLciBranchOffice.Size = New System.Drawing.Size(360, 60)
        Me.INDLciBranchOffice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBranchOffice.Text = "Sucursal"
        Me.INDLciBranchOffice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBranchOffice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciBranchOffice.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciBranchOffice.TextToControlDistance = 5
        '
        'INDLciProtocol
        '
        Me.INDLciProtocol.AllowHide = False
        Me.INDLciProtocol.Control = Me.INDSleProtocol
        Me.INDLciProtocol.Location = New System.Drawing.Point(0, 240)
        Me.INDLciProtocol.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciProtocol.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciProtocol.Name = "INDLciProtocol"
        Me.INDLciProtocol.ShowInCustomizationForm = False
        Me.INDLciProtocol.Size = New System.Drawing.Size(360, 60)
        Me.INDLciProtocol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciProtocol.Text = "Protocolo"
        Me.INDLciProtocol.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciProtocol.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciProtocol.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciProtocol.TextToControlDistance = 5
        '
        'INDLciDescription
        '
        Me.INDLciDescription.AllowHide = False
        Me.INDLciDescription.Control = Me.INDMeDescription
        Me.INDLciDescription.Location = New System.Drawing.Point(0, 420)
        Me.INDLciDescription.MaxSize = New System.Drawing.Size(360, 100)
        Me.INDLciDescription.MinSize = New System.Drawing.Size(360, 100)
        Me.INDLciDescription.Name = "INDLciDescription"
        Me.INDLciDescription.ShowInCustomizationForm = False
        Me.INDLciDescription.Size = New System.Drawing.Size(360, 100)
        Me.INDLciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDescription.Text = "Observaciones"
        Me.INDLciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciDescription.TextToControlDistance = 5
        '
        'INDLciMaintenanceResponsible
        '
        Me.INDLciMaintenanceResponsible.AllowHide = False
        Me.INDLciMaintenanceResponsible.Control = Me.INDSleMaintenanceResponsible
        Me.INDLciMaintenanceResponsible.Location = New System.Drawing.Point(0, 360)
        Me.INDLciMaintenanceResponsible.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciMaintenanceResponsible.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciMaintenanceResponsible.Name = "INDLciMaintenanceResponsible"
        Me.INDLciMaintenanceResponsible.ShowInCustomizationForm = False
        Me.INDLciMaintenanceResponsible.Size = New System.Drawing.Size(360, 60)
        Me.INDLciMaintenanceResponsible.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciMaintenanceResponsible.Text = "Responsable"
        Me.INDLciMaintenanceResponsible.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciMaintenanceResponsible.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciMaintenanceResponsible.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciMaintenanceResponsible.TextToControlDistance = 5
        '
        'INDLygActivities
        '
        Me.INDLygActivities.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygActivities.AppearanceGroup.Options.UseFont = True
        Me.INDLygActivities.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygActivities.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygActivities.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygActivities.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygActivities.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygActivities.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygActivities.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygActivities.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygActivities.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygActivities.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygActivities.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygActivities.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygActivities, False)
        Me.INDLygActivities.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciActivities})
        Me.INDLygActivities.Location = New System.Drawing.Point(384, 0)
        Me.INDLygActivities.Name = "INDLygActivities"
        Me.INDLygActivities.Size = New System.Drawing.Size(508, 573)
        Me.INDLygActivities.Text = "Actividades"
        '
        'INDLciActivities
        '
        Me.INDLciActivities.Control = Me.INDGcActivities
        Me.INDLciActivities.Location = New System.Drawing.Point(0, 0)
        Me.INDLciActivities.Name = "INDLciActivities"
        Me.INDLciActivities.Size = New System.Drawing.Size(484, 520)
        Me.INDLciActivities.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciActivities.TextVisible = False
        '
        'INDLygConsumables
        '
        Me.INDLygConsumables.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygConsumables.AppearanceGroup.Options.UseFont = True
        Me.INDLygConsumables.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygConsumables.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygConsumables.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygConsumables.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygConsumables.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygConsumables.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygConsumables.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygConsumables.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygConsumables.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygConsumables.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygConsumables.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygConsumables.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygConsumables, False)
        Me.INDLygConsumables.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciConsumables})
        Me.INDLygConsumables.Location = New System.Drawing.Point(892, 0)
        Me.INDLygConsumables.Name = "INDLygConsumables"
        Me.INDLygConsumables.Size = New System.Drawing.Size(508, 573)
        Me.INDLygConsumables.Text = "Consumibles"
        '
        'INDLciConsumables
        '
        Me.INDLciConsumables.Control = Me.INDGcConsumables
        Me.INDLciConsumables.Location = New System.Drawing.Point(0, 0)
        Me.INDLciConsumables.Name = "INDLciConsumables"
        Me.INDLciConsumables.Size = New System.Drawing.Size(484, 520)
        Me.INDLciConsumables.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciConsumables.TextVisible = False
        '
        'INDLygTools
        '
        Me.INDLygTools.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygTools.AppearanceGroup.Options.UseFont = True
        Me.INDLygTools.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygTools.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygTools.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygTools.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygTools.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygTools.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygTools.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygTools.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygTools.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygTools.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygTools.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygTools.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygTools, False)
        Me.INDLygTools.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciTools})
        Me.INDLygTools.Location = New System.Drawing.Point(1400, 0)
        Me.INDLygTools.Name = "INDLygTools"
        Me.INDLygTools.Size = New System.Drawing.Size(508, 573)
        Me.INDLygTools.Text = "Herramientas y Equipos"
        '
        'INDLciTools
        '
        Me.INDLciTools.Control = Me.INDGcTools
        Me.INDLciTools.Location = New System.Drawing.Point(0, 0)
        Me.INDLciTools.Name = "INDLciTools"
        Me.INDLciTools.Size = New System.Drawing.Size(484, 520)
        Me.INDLciTools.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciTools.TextVisible = False
        '
        'INDLygSupplies
        '
        Me.INDLygSupplies.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygSupplies.AppearanceGroup.Options.UseFont = True
        Me.INDLygSupplies.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLygSupplies.AppearanceItemCaption.Options.UseFont = True
        Me.INDLygSupplies.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygSupplies.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLygSupplies.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLygSupplies.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLygSupplies.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygSupplies.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLygSupplies.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygSupplies.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLygSupplies.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLygSupplies.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLygSupplies, False)
        Me.INDLygSupplies.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciSupplies})
        Me.INDLygSupplies.Location = New System.Drawing.Point(1908, 0)
        Me.INDLygSupplies.Name = "INDLygSupplies"
        Me.INDLygSupplies.Size = New System.Drawing.Size(508, 573)
        Me.INDLygSupplies.Text = "Insumos"
        '
        'INDLciSupplies
        '
        Me.INDLciSupplies.Control = Me.INDGcSupplies
        Me.INDLciSupplies.Location = New System.Drawing.Point(0, 0)
        Me.INDLciSupplies.Name = "INDLciSupplies"
        Me.INDLciSupplies.Size = New System.Drawing.Size(484, 520)
        Me.INDLciSupplies.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciSupplies.TextVisible = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDSlePhysicalAsset
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlePhysicalAsset, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlePhysicalAsset, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlePhysicalAsset, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.INDSlePhysicalAsset.Location = New System.Drawing.Point(24, 379)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlePhysicalAsset, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlePhysicalAsset.Name = "INDSlePhysicalAsset"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlePhysicalAsset, True)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.INDSlePhysicalAsset.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSlePhysicalAsset.Properties.Appearance.Options.UseFont = True
        Me.INDSlePhysicalAsset.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSlePhysicalAsset.Properties.NullText = ""
        Me.INDSlePhysicalAsset.Properties.PopupView = Me.INDGvPhysicalAsset
        Me.INDSlePhysicalAsset.Properties.ReadOnly = True
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlePhysicalAsset, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlePhysicalAsset, True)
        Me.INDSlePhysicalAsset.Size = New System.Drawing.Size(356, 28)
        Me.INDSlePhysicalAsset.StyleController = Me.INDLcRoot
        Me.INDSlePhysicalAsset.TabIndex = 21
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlePhysicalAsset, "574")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlePhysicalAsset, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlePhysicalAsset, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlePhysicalAsset, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlePhysicalAsset, False)
        '
        'INDLciPhysicalAsset
        '
        Me.INDLciPhysicalAsset.Control = Me.INDSlePhysicalAsset
        Me.INDLciPhysicalAsset.Location = New System.Drawing.Point(0, 300)
        Me.INDLciPhysicalAsset.MaxSize = New System.Drawing.Size(360, 60)
        Me.INDLciPhysicalAsset.MinSize = New System.Drawing.Size(360, 60)
        Me.INDLciPhysicalAsset.Name = "INDLciPhysicalAsset"
        Me.INDLciPhysicalAsset.Size = New System.Drawing.Size(360, 60)
        Me.INDLciPhysicalAsset.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciPhysicalAsset.Text = "Activo"
        Me.INDLciPhysicalAsset.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciPhysicalAsset.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciPhysicalAsset.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLciPhysicalAsset.TextToControlDistance = 5
        '
        'INDGvPhysicalAsset
        '
        Me.INDGvPhysicalAsset.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvPhysicalAsset.Name = "INDGvPhysicalAsset"
        Me.INDGvPhysicalAsset.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvPhysicalAsset.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvPhysicalAsset, False)
        '
        'FrmWorkOrderModal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1584, 712)
        Me.IconOptions.Icon = CType(resources.GetObject("FrmWorkOrderModal.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmWorkOrderModal"
        Me.Opacity = 1.0R
        Me.Tag = "2134"
        Me.Text = "Orden de Trabajo"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDSleMaintenanceResponsible.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvMaintenanceResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleProtocol.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvProtocol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleBranchOffice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeProgramDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeProgramDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeRequestDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDDeRequestDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcSupplies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvSupplies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkSuppliesSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcTools, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvTools, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkToolsSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcConsumables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvConsumables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkConsumablesSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGcActivities, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvActivities, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDChkActivitiesSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSeActivitiesTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleActivitiesUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvActivitiesUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygGeneralData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciEquipment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciRequestDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProgramDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciProtocol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciMaintenanceResponsible, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygActivities, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciActivities, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygConsumables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciConsumables, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygTools, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciTools, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLygSupplies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciSupplies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSlePhysicalAsset.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciPhysicalAsset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvPhysicalAsset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLciEquipment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents INDGcActivities As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvActivities As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciActivities As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcSupplies As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvSupplies As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcTools As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvTools As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGcConsumables As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDGvConsumables As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciConsumables As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciTools As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciSupplies As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLygGeneralData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLygActivities As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLygConsumables As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLygTools As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLygSupplies As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents INDGvConsumables_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvConsumables_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvTools_Plate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvTools_Item As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvSupplies_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvSupplies_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvActivities_Activity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvActivities_Time As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDDeProgramDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDDeRequestDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDLciRequestDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciProgramDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSleProtocol As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvProtocol As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSleBranchOffice As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvBranchOffice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciBranchOffice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciProtocol As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvBranchOffice_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvBranchOffice_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvProtocol_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvProtocol_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSleMaintenanceResponsible As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvMaintenanceResponsible As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvMaintenanceResponsible_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvMaintenanceResponsible_Name As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLciMaintenanceResponsible As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGvActivities_Unit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvActivities_Selected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvConsumables_Selected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvTools_Selected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDGvSupplies_Selected As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDChkActivitiesSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSleActivitiesUnit As DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit
    Friend WithEvents INDGvActivitiesUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGvActivitiesUnit_Description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSeActivitiesTime As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents INDChkSuppliesSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDChkToolsSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDChkConsumablesSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents INDSlePhysicalAsset As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvPhysicalAsset As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciPhysicalAsset As DevExpress.XtraLayout.LayoutControlItem
End Class
