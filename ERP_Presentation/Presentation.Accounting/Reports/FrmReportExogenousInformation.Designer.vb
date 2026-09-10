Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmReportExogenousInformation
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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDGcExportExcell = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDSbGenerateExcell = New DevExpress.XtraEditors.SimpleButton()
        Me.INDbtnGenerate = New DevExpress.XtraEditors.SimpleButton()
        Me.INDtxtSendingNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDcdnYear = New Presentation.Controls.CtrDateNavigator()
        Me.INDgleConcept = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDsleFormat = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralInformation = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemConcept = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSendingNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemYear = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemGenerate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFormat = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciGenerateExcell = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.INDtxtVersion = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyItemVersion = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtSendingNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleConcept.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleFormat.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConcept, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSendingNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemYear, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemGenerate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciGenerateExcell, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtVersion.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 24)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1465, 677)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Dock = System.Windows.Forms.DockStyle.None
        '
        'BarraBotones
        '
        Me.BarraBotones.Dock = System.Windows.Forms.DockStyle.None
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1465, 75)
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 668)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDtxtVersion)
        Me.INDlyRoot.Controls.Add(Me.INDGcExportExcell)
        Me.INDlyRoot.Controls.Add(Me.INDSbGenerateExcell)
        Me.INDlyRoot.Controls.Add(Me.INDbtnGenerate)
        Me.INDlyRoot.Controls.Add(Me.INDtxtSendingNumber)
        Me.INDlyRoot.Controls.Add(Me.INDcdnYear)
        Me.INDlyRoot.Controls.Add(Me.INDgleConcept)
        Me.INDlyRoot.Controls.Add(Me.INDsleFormat)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.LayoutControlGroup1
        Me.INDlyRoot.Size = New System.Drawing.Size(1261, 668)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDGcExportExcell
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDGcExportExcell, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDGcExportExcell, Nothing)
        Me.IndigoGridControl1.SetExportButton(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDGcExportExcell, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDGcExportExcell, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDGcExportExcell, False)
        Me.INDGcExportExcell.Location = New System.Drawing.Point(24, 375)
        Me.INDGcExportExcell.MainView = Me.GridView1
        Me.INDGcExportExcell.Name = "INDGcExportExcell"
        Me.INDGcExportExcell.Size = New System.Drawing.Size(386, 269)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDGcExportExcell, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.INDGcExportExcell.TabIndex = 9
        Me.INDGcExportExcell.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.GridView1.Appearance.ViewCaption.Options.UseFont = True
        Me.GridView1.GridControl = Me.INDGcExportExcell
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'INDSbGenerateExcell
        '
        Me.INDSbGenerateExcell.ImageOptions.Image = Global.Presentation.Accounting.My.Resources.Resources.ICONO_EXCEL_02
        Me.INDSbGenerateExcell.Location = New System.Drawing.Point(370, 339)
        Me.INDSbGenerateExcell.MaximumSize = New System.Drawing.Size(40, 32)
        Me.INDSbGenerateExcell.MinimumSize = New System.Drawing.Size(40, 32)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSbGenerateExcell, False)
        Me.INDSbGenerateExcell.Name = "INDSbGenerateExcell"
        Me.INDSbGenerateExcell.Size = New System.Drawing.Size(40, 32)
        Me.INDSbGenerateExcell.StyleController = Me.INDlyRoot
        Me.INDSbGenerateExcell.TabIndex = 8
        '
        'INDbtnGenerate
        '
        Me.INDbtnGenerate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnGenerate.Appearance.Options.UseFont = True
        Me.INDbtnGenerate.Location = New System.Drawing.Point(24, 339)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDbtnGenerate, True)
        Me.INDbtnGenerate.Name = "INDbtnGenerate"
        Me.INDbtnGenerate.Size = New System.Drawing.Size(346, 32)
        Me.INDbtnGenerate.StyleController = Me.INDlyRoot
        Me.INDbtnGenerate.TabIndex = 7
        Me.INDbtnGenerate.Text = "Generar"
        '
        'INDtxtSendingNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtSendingNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtSendingNumber, False)
        Me.INDtxtSendingNumber.EnterMoveNextControl = True
        Me.INDtxtSendingNumber.Location = New System.Drawing.Point(24, 305)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtSendingNumber, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDtxtSendingNumber.Name = "INDtxtSendingNumber"
        Me.INDtxtSendingNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtSendingNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSendingNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtSendingNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtSendingNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtSendingNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtSendingNumber.Properties.Mask.EditMask = "[0-9]+"
        Me.INDtxtSendingNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtSendingNumber.Properties.MaxLength = 8
        Me.INDtxtSendingNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtSendingNumber.StyleController = Me.INDlyRoot
        Me.INDtxtSendingNumber.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtSendingNumber, 0)
        '
        'INDcdnYear
        '
        Me.INDcdnYear.CtrCalendar = Nothing
        Me.INDcdnYear.HowShowControl = Presentation.Controls.CtrDateNavigator.EHowShowControl.OnlyYear
        Me.INDcdnYear.Location = New System.Drawing.Point(24, 59)
        Me.INDcdnYear.Name = "INDcdnYear"
        Me.INDcdnYear.Size = New System.Drawing.Size(386, 36)
        Me.INDcdnYear.TabIndex = 0
        Me.INDcdnYear.WithEvent = True
        '
        'INDgleConcept
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleConcept, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleConcept, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleConcept, False)
        Me.INDgleConcept.EditValue = ""
        Me.INDgleConcept.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleConcept, True)
        Me.INDgleConcept.Location = New System.Drawing.Point(24, 245)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleConcept, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleConcept.Name = "INDgleConcept"
        Me.INDgleConcept.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDgleConcept.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleConcept.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleConcept.Properties.Appearance.Options.UseFont = True
        Me.INDgleConcept.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleConcept.Properties.DisplayMember = "Item2"
        Me.INDgleConcept.Properties.ImmediatePopup = True
        Me.INDgleConcept.Properties.NullText = ""
        Me.INDgleConcept.Properties.PopupView = Me.GridLookUpEdit1View
        Me.INDgleConcept.Properties.ValueMember = "Item1"
        Me.INDgleConcept.Size = New System.Drawing.Size(386, 28)
        Me.INDgleConcept.StyleController = Me.INDlyRoot
        Me.INDgleConcept.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleConcept, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleConcept, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Descripción"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDsleFormat
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleFormat, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleFormat, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleFormat, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleFormat, False)
        Me.INDsleFormat.Location = New System.Drawing.Point(24, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleFormat, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleFormat.Name = "INDsleFormat"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleFormat, False)
        Me.INDsleFormat.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleFormat.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleFormat.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleFormat.Properties.Appearance.Options.UseFont = True
        Me.INDsleFormat.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleFormat.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleFormat.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleFormat.Properties.DisplayMember = "Format"
        Me.INDsleFormat.Properties.NullText = ""
        Me.INDsleFormat.Properties.PopupSizeable = False
        Me.INDsleFormat.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleFormat.Properties.ShowFooter = False
        Me.INDsleFormat.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleFormat, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleFormat, True)
        Me.INDsleFormat.Size = New System.Drawing.Size(386, 28)
        Me.INDsleFormat.StyleController = Me.INDlyRoot
        Me.INDsleFormat.TabIndex = 6
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleFormat, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleFormat, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleFormat, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleFormat, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleFormat, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Formato"
        Me.GridColumn3.FieldName = "Format"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Versión"
        Me.GridColumn4.FieldName = "Version"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
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
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralInformation})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1261, 668)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygGeneralInformation
        '
        Me.INDlygGeneralInformation.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralInformation.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralInformation.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralInformation, False)
        Me.INDlygGeneralInformation.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemConcept, Me.INDlyItemSendingNumber, Me.INDlyItemYear, Me.INDlyItemGenerate, Me.INDlyItemFormat, Me.INDLciGenerateExcell, Me.LayoutControlItem1, Me.INDlyItemVersion})
        Me.INDlygGeneralInformation.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralInformation.Name = "INDlygGeneralInformation"
        Me.INDlygGeneralInformation.Size = New System.Drawing.Size(1241, 648)
        Me.INDlygGeneralInformation.Text = "Información Principal"
        '
        'INDlyItemConcept
        '
        Me.INDlyItemConcept.Control = Me.INDgleConcept
        Me.INDlyItemConcept.Location = New System.Drawing.Point(0, 160)
        Me.INDlyItemConcept.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemConcept.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemConcept.Name = "INDlyItemConcept"
        Me.INDlyItemConcept.Size = New System.Drawing.Size(1217, 60)
        Me.INDlyItemConcept.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemConcept.Text = "Concepto"
        Me.INDlyItemConcept.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemConcept.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemConcept.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemConcept.TextToControlDistance = 5
        '
        'INDlyItemSendingNumber
        '
        Me.INDlyItemSendingNumber.Control = Me.INDtxtSendingNumber
        Me.INDlyItemSendingNumber.Location = New System.Drawing.Point(0, 220)
        Me.INDlyItemSendingNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendingNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendingNumber.Name = "INDlyItemSendingNumber"
        Me.INDlyItemSendingNumber.Size = New System.Drawing.Size(1217, 60)
        Me.INDlyItemSendingNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSendingNumber.Text = "No. Envío"
        Me.INDlyItemSendingNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSendingNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSendingNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSendingNumber.TextToControlDistance = 5
        '
        'INDlyItemYear
        '
        Me.INDlyItemYear.Control = Me.INDcdnYear
        Me.INDlyItemYear.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemYear.MaxSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemYear.MinSize = New System.Drawing.Size(390, 40)
        Me.INDlyItemYear.Name = "INDlyItemYear"
        Me.INDlyItemYear.Size = New System.Drawing.Size(1217, 40)
        Me.INDlyItemYear.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemYear.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemYear.TextVisible = False
        '
        'INDlyItemGenerate
        '
        Me.INDlyItemGenerate.Control = Me.INDbtnGenerate
        Me.INDlyItemGenerate.Location = New System.Drawing.Point(0, 280)
        Me.INDlyItemGenerate.MaxSize = New System.Drawing.Size(348, 36)
        Me.INDlyItemGenerate.MinSize = New System.Drawing.Size(348, 36)
        Me.INDlyItemGenerate.Name = "INDlyItemGenerate"
        Me.INDlyItemGenerate.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2)
        Me.INDlyItemGenerate.Size = New System.Drawing.Size(348, 36)
        Me.INDlyItemGenerate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemGenerate.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyItemGenerate.TextVisible = False
        '
        'INDlyItemFormat
        '
        Me.INDlyItemFormat.Control = Me.INDsleFormat
        Me.INDlyItemFormat.Location = New System.Drawing.Point(0, 40)
        Me.INDlyItemFormat.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFormat.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemFormat.Name = "INDlyItemFormat"
        Me.INDlyItemFormat.Size = New System.Drawing.Size(1217, 60)
        Me.INDlyItemFormat.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFormat.Text = "Formato"
        Me.INDlyItemFormat.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFormat.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemFormat.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemFormat.TextToControlDistance = 5
        '
        'INDLciGenerateExcell
        '
        Me.INDLciGenerateExcell.Control = Me.INDSbGenerateExcell
        Me.INDLciGenerateExcell.Location = New System.Drawing.Point(348, 280)
        Me.INDLciGenerateExcell.MaxSize = New System.Drawing.Size(42, 32)
        Me.INDLciGenerateExcell.MinSize = New System.Drawing.Size(42, 32)
        Me.INDLciGenerateExcell.Name = "INDLciGenerateExcell"
        Me.INDLciGenerateExcell.Padding = New DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2)
        Me.INDLciGenerateExcell.Size = New System.Drawing.Size(869, 36)
        Me.INDLciGenerateExcell.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciGenerateExcell.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciGenerateExcell.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDGcExportExcell
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 316)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 0)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(104, 24)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1217, 273)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'INDtxtVersion
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtVersion, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtVersion, False)
        Me.INDtxtVersion.Location = New System.Drawing.Point(24, 185)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtVersion, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtVersion.Name = "INDtxtVersion"
        Me.INDtxtVersion.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtVersion.Properties.Appearance.Options.UseFont = True
        Me.INDtxtVersion.Properties.ReadOnly = True
        Me.INDtxtVersion.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtVersion.StyleController = Me.INDlyRoot
        Me.INDtxtVersion.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtVersion, 0)
        '
        'INDlyItemVersion
        '
        Me.INDlyItemVersion.Control = Me.INDtxtVersion
        Me.INDlyItemVersion.Location = New System.Drawing.Point(0, 100)
        Me.INDlyItemVersion.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemVersion.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemVersion.Name = "INDlyItemVersion"
        Me.INDlyItemVersion.Size = New System.Drawing.Size(1217, 60)
        Me.INDlyItemVersion.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemVersion.Text = "Versión"
        Me.INDlyItemVersion.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemVersion.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemVersion.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemVersion.TextToControlDistance = 5
        '
        'FrmReportExogenousInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmReportExogenousInformation"
        Me.Opacity = 1.0R
        Me.Tag = "1953"
        Me.Text = "Generación Información Exogena DIAN"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDGcExportExcell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtSendingNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleConcept.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleFormat.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralInformation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConcept, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSendingNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemYear, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemGenerate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFormat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciGenerateExcell, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtVersion.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemVersion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlygGeneralInformation As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgleConcept As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemConcept As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDcdnYear As CtrDateNavigator
    Friend WithEvents INDlyItemYear As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtSendingNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemSendingNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemFormat As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnGenerate As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDlyItemGenerate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleFormat As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDSbGenerateExcell As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciGenerateExcell As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGcExportExcell As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtVersion As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemVersion As DevExpress.XtraLayout.LayoutControlItem
End Class
