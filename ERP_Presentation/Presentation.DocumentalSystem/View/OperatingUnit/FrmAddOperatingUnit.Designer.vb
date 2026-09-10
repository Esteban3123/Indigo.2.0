Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddOperatingUnit
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDCnNavigation = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDsleStruct = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleIdCity = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtEmailAudit = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtEmail = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPhone = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtAddress = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtIPSCode = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtUnitName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnUnitCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgAddOperatingUnit = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciUnitCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciUnitName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciIPSCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciAddress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciPhone = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciEmail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciEmailAudit = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciIdCity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciStruct = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcBase.SuspendLayout()
        CType(Me.INDsleStruct.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleIdCity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtEmailAudit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtIPSCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtUnitName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnUnitCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgAddOperatingUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciUnitCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciUnitName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciIPSCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciEmailAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciIdCity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciStruct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDCnNavigation)
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
        'INDCnNavigation
        '
        Me.INDCnNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDCnNavigation.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDCnNavigation.LayoutControl = Me.INDLcBase
        Me.INDCnNavigation.Location = New System.Drawing.Point(2, 7)
        Me.INDCnNavigation.Margin = New System.Windows.Forms.Padding(0)
        Me.INDCnNavigation.Name = "INDCnNavigation"
        Me.INDCnNavigation.Size = New System.Drawing.Size(200, 602)
        Me.INDCnNavigation.TabIndex = 0
        Me.INDCnNavigation.UseDisabledStatePainter = False
        '
        'INDLcBase
        '
        Me.INDLcBase.AllowCustomization = False
        Me.INDLcBase.Controls.Add(Me.INDsleStruct)
        Me.INDLcBase.Controls.Add(Me.INDsleIdCity)
        Me.INDLcBase.Controls.Add(Me.INDtxtEmailAudit)
        Me.INDLcBase.Controls.Add(Me.INDtxtEmail)
        Me.INDLcBase.Controls.Add(Me.INDtxtPhone)
        Me.INDLcBase.Controls.Add(Me.INDtxtAddress)
        Me.INDLcBase.Controls.Add(Me.INDtxtIPSCode)
        Me.INDLcBase.Controls.Add(Me.INDtxtUnitName)
        Me.INDLcBase.Controls.Add(Me.INDbtnUnitCode)
        Me.INDLcBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, False)
        Me.INDLcBase.Location = New System.Drawing.Point(202, 7)
        Me.INDLcBase.Name = "INDLcBase"
        Me.INDLcBase.Root = Me.INDLcgBase
        Me.INDLcBase.Size = New System.Drawing.Size(804, 602)
        Me.INDLcBase.TabIndex = 1
        Me.INDLcBase.Text = "LayoutControl1"
        '
        'INDsleStruct
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleStruct, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleStruct, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleStruct, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleStruct, False)
        Me.INDsleStruct.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleStruct, False)
        Me.INDsleStruct.Location = New System.Drawing.Point(171, 347)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleStruct, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleStruct.Name = "INDsleStruct"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleStruct, False)
        Me.INDsleStruct.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleStruct.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleStruct.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleStruct.Properties.Appearance.Options.UseFont = True
        Me.INDsleStruct.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleStruct.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleStruct.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleStruct.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleStruct.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleStruct.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleStruct.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleStruct.Properties.DisplayMember = "CodeName"
        Me.INDsleStruct.Properties.NullText = ""
        Me.INDsleStruct.Properties.PopupSizeable = False
        Me.INDsleStruct.Properties.ShowFooter = False
        Me.INDsleStruct.Properties.ValueMember = "Id"
        Me.INDsleStruct.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleStruct, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleStruct, True)
        Me.INDsleStruct.Size = New System.Drawing.Size(239, 28)
        Me.INDsleStruct.StyleController = Me.INDLcBase
        Me.INDsleStruct.TabIndex = 9
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleStruct, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleStruct, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleStruct, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleStruct, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleStruct, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsFind.AlwaysVisible = True
        Me.GridView1.OptionsFind.FindFilterColumns = "UnitCode"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Código"
        Me.GridColumn3.FieldName = "UnitCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 0
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Nombre"
        Me.GridColumn4.FieldName = "UnitName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 1
        '
        'INDsleIdCity
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleIdCity, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleIdCity, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleIdCity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleIdCity, False)
        Me.INDsleIdCity.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleIdCity, False)
        Me.INDsleIdCity.Location = New System.Drawing.Point(171, 311)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleIdCity, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleIdCity.Name = "INDsleIdCity"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleIdCity, False)
        Me.INDsleIdCity.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDsleIdCity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleIdCity.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleIdCity.Properties.Appearance.Options.UseFont = True
        Me.INDsleIdCity.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleIdCity.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleIdCity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleIdCity.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleIdCity.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleIdCity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleIdCity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDsleIdCity.Properties.DisplayMember = "CodeName"
        Me.INDsleIdCity.Properties.NullText = ""
        Me.INDsleIdCity.Properties.PopupSizeable = False
        Me.INDsleIdCity.Properties.ShowFooter = False
        Me.INDsleIdCity.Properties.ValueMember = "Id"
        Me.INDsleIdCity.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleIdCity, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleIdCity, True)
        Me.INDsleIdCity.Size = New System.Drawing.Size(239, 28)
        Me.INDsleIdCity.StyleController = Me.INDLcBase
        Me.INDsleIdCity.TabIndex = 8
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleIdCity, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleIdCity, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleIdCity, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleIdCity, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleIdCity, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEdit1View.OptionsFind.FindFilterColumns = "Code"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Código"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'INDtxtEmailAudit
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtEmailAudit, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtEmailAudit, False)
        Me.INDtxtEmailAudit.EnterMoveNextControl = True
        Me.INDtxtEmailAudit.Location = New System.Drawing.Point(171, 275)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtEmailAudit, Presentation.Controls.IndigoTextEdit.EMask.CorreoElectronico)
        Me.INDtxtEmailAudit.Name = "INDtxtEmailAudit"
        Me.INDtxtEmailAudit.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtEmailAudit.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmailAudit.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtEmailAudit.Properties.Appearance.Options.UseFont = True
        Me.INDtxtEmailAudit.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtEmailAudit.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtEmailAudit.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmailAudit.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtEmailAudit.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtEmailAudit.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtEmailAudit.Properties.Mask.EditMask = "([a-zA-Z0-9_\-\.]{0,25})@([a-z]{0,15}\.)([a-z]{0,5})"
        Me.INDtxtEmailAudit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtEmailAudit.Properties.MaxLength = 100
        Me.INDtxtEmailAudit.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtEmailAudit.StyleController = Me.INDLcBase
        Me.INDtxtEmailAudit.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtEmailAudit, 0)
        '
        'INDtxtEmail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtEmail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtEmail, False)
        Me.INDtxtEmail.EnterMoveNextControl = True
        Me.INDtxtEmail.Location = New System.Drawing.Point(171, 239)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtEmail, Presentation.Controls.IndigoTextEdit.EMask.CorreoElectronico)
        Me.INDtxtEmail.Name = "INDtxtEmail"
        Me.INDtxtEmail.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtEmail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmail.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtEmail.Properties.Appearance.Options.UseFont = True
        Me.INDtxtEmail.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtEmail.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtEmail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmail.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtEmail.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtEmail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtEmail.Properties.Mask.EditMask = "([a-zA-Z0-9_\-\.]{0,25})@([a-z]{0,15}\.)([a-z]{0,5})"
        Me.INDtxtEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtEmail.Properties.MaxLength = 100
        Me.INDtxtEmail.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtEmail.StyleController = Me.INDLcBase
        Me.INDtxtEmail.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtEmail, 0)
        '
        'INDtxtPhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPhone, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPhone, False)
        Me.INDtxtPhone.EnterMoveNextControl = True
        Me.INDtxtPhone.Location = New System.Drawing.Point(171, 203)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPhone, Presentation.Controls.IndigoTextEdit.EMask.Telefono)
        Me.INDtxtPhone.Name = "INDtxtPhone"
        Me.INDtxtPhone.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtPhone.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPhone.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPhone.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPhone.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtPhone.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtPhone.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPhone.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtPhone.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtPhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPhone.Properties.Mask.EditMask = "(\d?\d?\d?)\d\d\d-\d\d\d\d"
        Me.INDtxtPhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular
        Me.INDtxtPhone.Properties.MaxLength = 20
        Me.INDtxtPhone.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtPhone.StyleController = Me.INDLcBase
        Me.INDtxtPhone.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPhone, 0)
        '
        'INDtxtAddress
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAddress, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAddress, False)
        Me.INDtxtAddress.EnterMoveNextControl = True
        Me.INDtxtAddress.Location = New System.Drawing.Point(171, 167)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAddress, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtAddress.Name = "INDtxtAddress"
        Me.INDtxtAddress.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtAddress.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAddress.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAddress.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAddress.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtAddress.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtAddress.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAddress.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtAddress.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtAddress.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtAddress.Properties.MaxLength = 100
        Me.INDtxtAddress.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtAddress.StyleController = Me.INDLcBase
        Me.INDtxtAddress.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAddress, 0)
        '
        'INDtxtIPSCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtIPSCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtIPSCode, False)
        Me.INDtxtIPSCode.EnterMoveNextControl = True
        Me.INDtxtIPSCode.Location = New System.Drawing.Point(171, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtIPSCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtIPSCode.Name = "INDtxtIPSCode"
        Me.INDtxtIPSCode.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDtxtIPSCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIPSCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtIPSCode.Properties.Appearance.Options.UseFont = True
        Me.INDtxtIPSCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtIPSCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtIPSCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtIPSCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtIPSCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtIPSCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtIPSCode.Properties.MaxLength = 20
        Me.INDtxtIPSCode.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtIPSCode.StyleController = Me.INDLcBase
        Me.INDtxtIPSCode.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtIPSCode, 0)
        '
        'INDtxtUnitName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtUnitName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtUnitName, True)
        Me.INDtxtUnitName.EnterMoveNextControl = True
        Me.INDtxtUnitName.Location = New System.Drawing.Point(171, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtUnitName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtUnitName.Name = "INDtxtUnitName"
        Me.INDtxtUnitName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtUnitName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtUnitName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtUnitName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtUnitName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtUnitName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtUnitName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtUnitName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtUnitName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtUnitName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtUnitName.Properties.MaxLength = 100
        Me.INDtxtUnitName.Size = New System.Drawing.Size(239, 28)
        Me.INDtxtUnitName.StyleController = Me.INDLcBase
        Me.INDtxtUnitName.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtUnitName, 0)
        Me.INDtxtUnitName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnUnitCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnUnitCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnUnitCode, True)
        Me.INDbtnUnitCode.Location = New System.Drawing.Point(171, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnUnitCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnUnitCode.Name = "INDbtnUnitCode"
        Me.INDbtnUnitCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnUnitCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnUnitCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnUnitCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnUnitCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnUnitCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnUnitCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnUnitCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnUnitCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnUnitCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnUnitCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.DocumentalSystem.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.INDbtnUnitCode.Properties.MaxLength = 5
        Me.INDbtnUnitCode.Size = New System.Drawing.Size(239, 28)
        Me.INDbtnUnitCode.StyleController = Me.INDLcBase
        Me.INDbtnUnitCode.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnUnitCode, 0)
        Me.INDbtnUnitCode.ToolTip = "Este Campo es Necesario"
        '
        'INDLcgBase
        '
        Me.INDLcgBase.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgBase.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgBase.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgBase.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgBase.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgBase.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDLcgBase.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgBase.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgBase, False)
        Me.INDLcgBase.CustomizationFormText = "Unidad Operativa"
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgAddOperatingUnit})
        Me.INDLcgBase.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgBase.Name = "INDLcgBase"
        Me.INDLcgBase.Size = New System.Drawing.Size(804, 602)
        Me.INDLcgBase.TextVisible = False
        '
        'INDlcgAddOperatingUnit
        '
        Me.INDlcgAddOperatingUnit.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgAddOperatingUnit.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgAddOperatingUnit.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgAddOperatingUnit.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgAddOperatingUnit.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgAddOperatingUnit, False)
        Me.INDlcgAddOperatingUnit.CustomizationFormText = "Información General"
        Me.INDlcgAddOperatingUnit.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciUnitCode, Me.INDlciUnitName, Me.INDlciIPSCode, Me.INDlciAddress, Me.INDlciPhone, Me.INDlciEmail, Me.INDlciEmailAudit, Me.INDlciIdCity, Me.INDlciStruct})
        Me.INDlcgAddOperatingUnit.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgAddOperatingUnit.Name = "INDlcgAddOperatingUnit"
        Me.INDlcgAddOperatingUnit.Size = New System.Drawing.Size(784, 582)
        Me.INDlcgAddOperatingUnit.Text = "Información General"
        '
        'INDlciUnitCode
        '
        Me.INDlciUnitCode.AllowHide = False
        Me.INDlciUnitCode.Control = Me.INDbtnUnitCode
        Me.INDlciUnitCode.CustomizationFormText = "Código"
        Me.INDlciUnitCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciUnitCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciUnitCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciUnitCode.Name = "INDlciUnitCode"
        Me.INDlciUnitCode.ShowInCustomizationForm = False
        Me.INDlciUnitCode.Size = New System.Drawing.Size(760, 36)
        Me.INDlciUnitCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciUnitCode.Text = "Código"
        Me.INDlciUnitCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciUnitCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciUnitCode.TextToControlDistance = 12
        '
        'INDlciUnitName
        '
        Me.INDlciUnitName.AllowHide = False
        Me.INDlciUnitName.Control = Me.INDtxtUnitName
        Me.INDlciUnitName.CustomizationFormText = "Nombre"
        Me.INDlciUnitName.Location = New System.Drawing.Point(0, 36)
        Me.INDlciUnitName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciUnitName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciUnitName.Name = "INDlciUnitName"
        Me.INDlciUnitName.ShowInCustomizationForm = False
        Me.INDlciUnitName.Size = New System.Drawing.Size(760, 36)
        Me.INDlciUnitName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciUnitName.Text = "Nombre"
        Me.INDlciUnitName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciUnitName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciUnitName.TextToControlDistance = 12
        '
        'INDlciIPSCode
        '
        Me.INDlciIPSCode.Control = Me.INDtxtIPSCode
        Me.INDlciIPSCode.CustomizationFormText = "Código IPS"
        Me.INDlciIPSCode.Location = New System.Drawing.Point(0, 72)
        Me.INDlciIPSCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciIPSCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciIPSCode.Name = "INDlciIPSCode"
        Me.INDlciIPSCode.Size = New System.Drawing.Size(760, 36)
        Me.INDlciIPSCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciIPSCode.Text = "Código IPS"
        Me.INDlciIPSCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciIPSCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciIPSCode.TextToControlDistance = 12
        '
        'INDlciAddress
        '
        Me.INDlciAddress.Control = Me.INDtxtAddress
        Me.INDlciAddress.CustomizationFormText = "Dirección"
        Me.INDlciAddress.Location = New System.Drawing.Point(0, 108)
        Me.INDlciAddress.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciAddress.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciAddress.Name = "INDlciAddress"
        Me.INDlciAddress.Size = New System.Drawing.Size(760, 36)
        Me.INDlciAddress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciAddress.Text = "Dirección"
        Me.INDlciAddress.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciAddress.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciAddress.TextToControlDistance = 12
        '
        'INDlciPhone
        '
        Me.INDlciPhone.Control = Me.INDtxtPhone
        Me.INDlciPhone.CustomizationFormText = "Teléfono"
        Me.INDlciPhone.Location = New System.Drawing.Point(0, 144)
        Me.INDlciPhone.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciPhone.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciPhone.Name = "INDlciPhone"
        Me.INDlciPhone.Size = New System.Drawing.Size(760, 36)
        Me.INDlciPhone.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciPhone.Text = "Teléfono"
        Me.INDlciPhone.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciPhone.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciPhone.TextToControlDistance = 12
        '
        'INDlciEmail
        '
        Me.INDlciEmail.Control = Me.INDtxtEmail
        Me.INDlciEmail.CustomizationFormText = "Email"
        Me.INDlciEmail.Location = New System.Drawing.Point(0, 180)
        Me.INDlciEmail.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciEmail.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciEmail.Name = "INDlciEmail"
        Me.INDlciEmail.Size = New System.Drawing.Size(760, 36)
        Me.INDlciEmail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEmail.Text = "Email"
        Me.INDlciEmail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciEmail.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciEmail.TextToControlDistance = 12
        '
        'INDlciEmailAudit
        '
        Me.INDlciEmailAudit.Control = Me.INDtxtEmailAudit
        Me.INDlciEmailAudit.CustomizationFormText = "Email Auditor"
        Me.INDlciEmailAudit.Location = New System.Drawing.Point(0, 216)
        Me.INDlciEmailAudit.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciEmailAudit.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciEmailAudit.Name = "INDlciEmailAudit"
        Me.INDlciEmailAudit.Size = New System.Drawing.Size(760, 36)
        Me.INDlciEmailAudit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciEmailAudit.Text = "Email Auditor"
        Me.INDlciEmailAudit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciEmailAudit.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciEmailAudit.TextToControlDistance = 12
        '
        'INDlciIdCity
        '
        Me.INDlciIdCity.Control = Me.INDsleIdCity
        Me.INDlciIdCity.CustomizationFormText = "Ciudad"
        Me.INDlciIdCity.Location = New System.Drawing.Point(0, 252)
        Me.INDlciIdCity.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciIdCity.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciIdCity.Name = "INDlciIdCity"
        Me.INDlciIdCity.Size = New System.Drawing.Size(760, 36)
        Me.INDlciIdCity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciIdCity.Text = "Ciudad"
        Me.INDlciIdCity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciIdCity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciIdCity.TextToControlDistance = 12
        '
        'INDlciStruct
        '
        Me.INDlciStruct.Control = Me.INDsleStruct
        Me.INDlciStruct.CustomizationFormText = "Estructura "
        Me.INDlciStruct.Location = New System.Drawing.Point(0, 288)
        Me.INDlciStruct.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlciStruct.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlciStruct.Name = "INDlciStruct"
        Me.INDlciStruct.Size = New System.Drawing.Size(760, 235)
        Me.INDlciStruct.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciStruct.Text = "Estructura "
        Me.INDlciStruct.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciStruct.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlciStruct.TextToControlDistance = 12
        '
        'FrmAddOperatingUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmAddOperatingUnit"
        Me.Opacity = 1.0R
        Me.Tag = "1511"
        Me.Text = "Unidad Operativa"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDCnNavigation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcBase.ResumeLayout(False)
        CType(Me.INDsleStruct.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleIdCity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtEmailAudit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtIPSCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtUnitName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnUnitCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgAddOperatingUnit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciUnitCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciUnitName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciIPSCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciEmailAudit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciIdCity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciStruct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDCnNavigation As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDbtnUnitCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlciUnitCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtUnitName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlciUnitName As DevExpress.XtraLayout.LayoutControlItem
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDtxtIPSCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciIPSCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtAddress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciAddress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtEmailAudit As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtPhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlciPhone As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciEmail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciEmailAudit As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleIdCity As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents INDlciIdCity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDsleStruct As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlciStruct As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlcgAddOperatingUnit As DevExpress.XtraLayout.LayoutControlGroup
End Class
