Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSyndicate
    Inherits FormBase

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlcSyndicate = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSlThirdParty = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDslePayrollConceptId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtNameSyndicate = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlciSyndicate = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciNameSyndicate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPayrollConceptId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciThirdParty = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcSyndicate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlcSyndicate.SuspendLayout()
        CType(Me.INDSlThirdParty.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePayrollConceptId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtNameSyndicate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciSyndicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciNameSyndicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPayrollConceptId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlcSyndicate)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 611)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlcSyndicate
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 602)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlcSyndicate
        '
        Me.INDlcSyndicate.Controls.Add(Me.INDSlThirdParty)
        Me.INDlcSyndicate.Controls.Add(Me.INDmeDescription)
        Me.INDlcSyndicate.Controls.Add(Me.INDslePayrollConceptId)
        Me.INDlcSyndicate.Controls.Add(Me.INDtxtNameSyndicate)
        Me.INDlcSyndicate.Controls.Add(Me.INDbtnCode)
        Me.INDlcSyndicate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlcSyndicate.Location = New System.Drawing.Point(202, 7)
        Me.INDlcSyndicate.Name = "INDlcSyndicate"
        Me.INDlcSyndicate.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(459, 283, 250, 350)
        Me.INDlcSyndicate.Root = Me.INDlciSyndicate
        Me.INDlcSyndicate.Size = New System.Drawing.Size(804, 602)
        Me.INDlcSyndicate.TabIndex = 1
        Me.INDlcSyndicate.Text = "Sindicato"
        '
        'INDSlThirdParty
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSlThirdParty, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSlThirdParty, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSlThirdParty, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSlThirdParty, False)
        Me.INDSlThirdParty.Location = New System.Drawing.Point(24, 211)
        Me.IndigoTextEdit1.SetMascara(Me.INDSlThirdParty, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSlThirdParty.Name = "INDSlThirdParty"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSlThirdParty, False)
        Me.INDSlThirdParty.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSlThirdParty.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSlThirdParty.Properties.Appearance.Options.UseBackColor = True
        Me.INDSlThirdParty.Properties.Appearance.Options.UseFont = True
        Me.INDSlThirdParty.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSlThirdParty.Properties.DisplayMember = "Name"
        Me.INDSlThirdParty.Properties.NullText = ""
        Me.INDSlThirdParty.Properties.PopupSizeable = False
        Me.INDSlThirdParty.Properties.ShowFooter = False
        Me.INDSlThirdParty.Properties.ValueMember = "Id"
        Me.INDSlThirdParty.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSlThirdParty, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSlThirdParty, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSlThirdParty, True)
        Me.INDSlThirdParty.Size = New System.Drawing.Size(386, 28)
        Me.INDSlThirdParty.StyleController = Me.INDlcSyndicate
        Me.INDSlThirdParty.TabIndex = 5
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSlThirdParty, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSlThirdParty, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSlThirdParty, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSlThirdParty, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSlThirdParty, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, False)
        Me.INDmeDescription.Location = New System.Drawing.Point(24, 340)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 71)
        Me.INDmeDescription.StyleController = Me.INDlcSyndicate
        Me.INDmeDescription.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        '
        'INDslePayrollConceptId
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePayrollConceptId, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePayrollConceptId, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePayrollConceptId, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePayrollConceptId, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.INDslePayrollConceptId.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.INDslePayrollConceptId.Location = New System.Drawing.Point(24, 149)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePayrollConceptId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePayrollConceptId.Name = "INDslePayrollConceptId"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePayrollConceptId, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.INDslePayrollConceptId.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDslePayrollConceptId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDslePayrollConceptId.Properties.Appearance.Options.UseBackColor = True
        Me.INDslePayrollConceptId.Properties.Appearance.Options.UseFont = True
        Me.INDslePayrollConceptId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDslePayrollConceptId.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDslePayrollConceptId.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDslePayrollConceptId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDslePayrollConceptId.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDslePayrollConceptId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDslePayrollConceptId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslePayrollConceptId.Properties.DisplayMember = "CodeName"
        Me.INDslePayrollConceptId.Properties.NullText = ""
        Me.INDslePayrollConceptId.Properties.PopupSizeable = False
        Me.INDslePayrollConceptId.Properties.ShowFooter = False
        Me.INDslePayrollConceptId.Properties.ValueMember = "Id"
        Me.INDslePayrollConceptId.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePayrollConceptId, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDslePayrollConceptId, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePayrollConceptId, True)
        Me.INDslePayrollConceptId.Size = New System.Drawing.Size(386, 28)
        Me.INDslePayrollConceptId.StyleController = Me.INDlcSyndicate
        Me.INDslePayrollConceptId.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePayrollConceptId, "549")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePayrollConceptId, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePayrollConceptId, "{0} - {1}")
        Me.INDslePayrollConceptId.ToolTip = "Este Campo es Necesario"
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePayrollConceptId, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePayrollConceptId, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsCustomization.AllowGroup = False
        Me.SearchLookUpEdit1View.OptionsDetail.EnableMasterViewMode = False
        Me.SearchLookUpEdit1View.OptionsDetail.ShowDetailTabs = False
        Me.SearchLookUpEdit1View.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit1View.OptionsFind.FindFilterColumns = "Codigo"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowDetailButtons = False
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Codigo"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Descripcion"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'INDtxtNameSyndicate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtNameSyndicate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtNameSyndicate, True)
        Me.INDtxtNameSyndicate.EnterMoveNextControl = True
        Me.INDtxtNameSyndicate.Location = New System.Drawing.Point(24, 277)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtNameSyndicate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtNameSyndicate.Name = "INDtxtNameSyndicate"
        Me.INDtxtNameSyndicate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtNameSyndicate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNameSyndicate.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtNameSyndicate.Properties.Appearance.Options.UseFont = True
        Me.INDtxtNameSyndicate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtNameSyndicate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtNameSyndicate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtNameSyndicate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtNameSyndicate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtNameSyndicate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtNameSyndicate.Properties.MaxLength = 200
        Me.INDtxtNameSyndicate.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtNameSyndicate.StyleController = Me.INDlcSyndicate
        Me.INDtxtNameSyndicate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtNameSyndicate, 0)
        Me.INDtxtNameSyndicate.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, False)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlcSyndicate
        Me.INDbtnCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        '
        'INDlciSyndicate
        '
        Me.INDlciSyndicate.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciSyndicate.AppearanceGroup.Options.UseFont = True
        Me.INDlciSyndicate.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlciSyndicate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlciSyndicate.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciSyndicate.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlciSyndicate.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlciSyndicate.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlciSyndicate.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciSyndicate.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlciSyndicate.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciSyndicate.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlciSyndicate.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlciSyndicate.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlciSyndicate, False)
        Me.INDlciSyndicate.CustomizationFormText = "Sindicato"
        Me.INDlciSyndicate.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlciSyndicate.GroupBordersVisible = False
        Me.INDlciSyndicate.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgMainData})
        Me.INDlciSyndicate.Location = New System.Drawing.Point(0, 0)
        Me.INDlciSyndicate.Name = "INDlciSyndicate"
        Me.INDlciSyndicate.Size = New System.Drawing.Size(804, 602)
        Me.INDlciSyndicate.Text = "Sindicato"
        Me.INDlciSyndicate.TextVisible = False
        '
        'INDlcgMainData
        '
        Me.INDlcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDlcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgMainData, False)
        Me.INDlcgMainData.CustomizationFormText = "Datos Principales"
        Me.INDlcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciNameSyndicate, Me.INDlciPayrollConceptId, Me.INDlciDescription, Me.INDLciThirdParty, Me.INDlciCode})
        Me.INDlcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgMainData.Name = "INDlcgMainData"
        Me.INDlcgMainData.Size = New System.Drawing.Size(784, 582)
        Me.INDlcgMainData.Text = "Datos Principales"
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbtnCode
        Me.INDlciCode.CustomizationFormText = "Código"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.ShowInCustomizationForm = False
        Me.INDlciCode.Size = New System.Drawing.Size(760, 64)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciCode.TextToControlDistance = 5
        '
        'INDlciNameSyndicate
        '
        Me.INDlciNameSyndicate.Control = Me.INDtxtNameSyndicate
        Me.INDlciNameSyndicate.CustomizationFormText = "Nombre"
        Me.INDlciNameSyndicate.Location = New System.Drawing.Point(0, 192)
        Me.INDlciNameSyndicate.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciNameSyndicate.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciNameSyndicate.Name = "INDlciNameSyndicate"
        Me.INDlciNameSyndicate.ShowInCustomizationForm = False
        Me.INDlciNameSyndicate.Size = New System.Drawing.Size(760, 64)
        Me.INDlciNameSyndicate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciNameSyndicate.Text = "Nombre"
        Me.INDlciNameSyndicate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciNameSyndicate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciNameSyndicate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciNameSyndicate.TextToControlDistance = 5
        '
        'INDlciPayrollConceptId
        '
        Me.INDlciPayrollConceptId.Control = Me.INDslePayrollConceptId
        Me.INDlciPayrollConceptId.CustomizationFormText = "Concepto"
        Me.INDlciPayrollConceptId.Location = New System.Drawing.Point(0, 64)
        Me.INDlciPayrollConceptId.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlciPayrollConceptId.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlciPayrollConceptId.Name = "INDlciPayrollConceptId"
        Me.INDlciPayrollConceptId.ShowInCustomizationForm = False
        Me.INDlciPayrollConceptId.Size = New System.Drawing.Size(760, 64)
        Me.INDlciPayrollConceptId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPayrollConceptId.Text = "Concepto"
        Me.INDlciPayrollConceptId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPayrollConceptId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciPayrollConceptId.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciPayrollConceptId.TextToControlDistance = 5
        '
        'INDlciDescription
        '
        Me.INDlciDescription.Control = Me.INDmeDescription
        Me.INDlciDescription.CustomizationFormText = "Descripción"
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 256)
        Me.INDlciDescription.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDlciDescription.MinSize = New System.Drawing.Size(390, 100)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.Size = New System.Drawing.Size(760, 267)
        Me.INDlciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlciDescription.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlciDescription.TextToControlDistance = 5
        '
        'INDLciThirdParty
        '
        Me.INDLciThirdParty.Control = Me.INDSlThirdParty
        Me.INDLciThirdParty.Location = New System.Drawing.Point(0, 128)
        Me.INDLciThirdParty.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDLciThirdParty.MinSize = New System.Drawing.Size(390, 64)
        Me.INDLciThirdParty.Name = "INDLciThirdParty"
        Me.INDLciThirdParty.Size = New System.Drawing.Size(760, 64)
        Me.INDLciThirdParty.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciThirdParty.Text = "Tercero"
        Me.INDLciThirdParty.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciThirdParty.TextSize = New System.Drawing.Size(50, 21)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nit"
        Me.GridColumn3.FieldName = "Nit"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción"
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'FrmSyndicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmSyndicate"
        Me.Opacity = 1.0R
        Me.Tag = "1527"
        Me.Text = "Sindicato"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcSyndicate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlcSyndicate.ResumeLayout(False)
        CType(Me.INDSlThirdParty.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePayrollConceptId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtNameSyndicate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciSyndicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciNameSyndicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPayrollConceptId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciThirdParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlcSyndicate As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlciSyndicate As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDlcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtNameSyndicate As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlciNameSyndicate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDslePayrollConceptId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciPayrollConceptId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDSlThirdParty As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciThirdParty As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
End Class
