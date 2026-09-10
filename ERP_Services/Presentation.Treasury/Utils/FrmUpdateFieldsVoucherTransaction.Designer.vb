Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUpdateFieldsVoucherTransaction
    Inherits FormBase

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
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControl()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtDebitNote = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleVoucherTransaction = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDGleData = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDLcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciData = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciVoucherTransaction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDebitNote = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControl1 = New Presentation.Controls.IndigoLayoutControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDebitNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleVoucherTransaction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciVoucherTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 117)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(857, 403)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(857, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(857, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(26, 394)
        Me.CtrNavigationControl1.TabIndex = 0
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.MemoEdit1)
        Me.INDLcRoot.Controls.Add(Me.INDTxtDebitNote)
        Me.INDLcRoot.Controls.Add(Me.INDSleVoucherTransaction)
        Me.INDLcRoot.Controls.Add(Me.INDGleData)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(28, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.INDLcgRoot
        Me.INDLcRoot.Size = New System.Drawing.Size(827, 394)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'MemoEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.MemoEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.MemoEdit1, False)
        Me.MemoEdit1.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.MemoEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.MemoEdit1.Name = "MemoEdit1"
        Me.MemoEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.MemoEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.MemoEdit1.Size = New System.Drawing.Size(386, 90)
        Me.MemoEdit1.StyleController = Me.INDLcRoot
        Me.MemoEdit1.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.MemoEdit1, 0)
        '
        'INDTxtDebitNote
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDebitNote, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDebitNote, False)
        Me.INDTxtDebitNote.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDebitNote, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtDebitNote.Name = "INDTxtDebitNote"
        Me.INDTxtDebitNote.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDTxtDebitNote.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDebitNote.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtDebitNote.Properties.Appearance.Options.UseFont = True
        Me.INDTxtDebitNote.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtDebitNote.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtDebitNote.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtDebitNote.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtDebitNote.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtDebitNote.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtDebitNote.Size = New System.Drawing.Size(386, 28)
        Me.INDTxtDebitNote.StyleController = Me.INDLcRoot
        Me.INDTxtDebitNote.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtDebitNote, 0)
        '
        'INDSleVoucherTransaction
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleVoucherTransaction, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleVoucherTransaction, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleVoucherTransaction, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.INDSleVoucherTransaction.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleVoucherTransaction, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleVoucherTransaction.Name = "INDSleVoucherTransaction"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.INDSleVoucherTransaction.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDSleVoucherTransaction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucherTransaction.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleVoucherTransaction.Properties.Appearance.Options.UseFont = True
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleVoucherTransaction.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "Limpiar selección (Supr)", Nothing, Nothing, True)})
        Me.INDSleVoucherTransaction.Properties.NullText = ""
        Me.INDSleVoucherTransaction.Properties.PopupSizeable = False
        Me.INDSleVoucherTransaction.Properties.ShowFooter = False
        Me.INDSleVoucherTransaction.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleVoucherTransaction, True)
        Me.INDSleVoucherTransaction.Size = New System.Drawing.Size(386, 28)
        Me.INDSleVoucherTransaction.StyleController = Me.INDLcRoot
        Me.INDSleVoucherTransaction.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleVoucherTransaction, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleVoucherTransaction, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleVoucherTransaction, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleVoucherTransaction, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'INDGleData
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleData, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleData, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleData, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleData, False)
        Me.INDGleData.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleData, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleData.Name = "INDGleData"
        Me.INDGleData.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDGleData.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleData.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleData.Properties.Appearance.Options.UseFont = True
        Me.INDGleData.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleData.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleData.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleData.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleData.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleData.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleData.Properties.ImmediatePopup = True
        Me.INDGleData.Properties.NullText = ""
        Me.INDGleData.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleData.Size = New System.Drawing.Size(386, 28)
        Me.INDGleData.StyleController = Me.INDLcRoot
        Me.INDGleData.TabIndex = 0
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleData, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleData, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.GridLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridLookUpEdit1View, False)
        '
        'INDLcgRoot
        '
        Me.INDLcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRoot.AppearanceGroup.Options.UseForeColor = True
        Me.INDLcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Options.UseForeColor = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseForeColor = True
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRoot, False)
        Me.INDLcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgRoot.GroupBordersVisible = False
        Me.INDLcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMainData})
        Me.INDLcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgRoot.Name = "INDLcgRoot"
        Me.INDLcgRoot.Size = New System.Drawing.Size(827, 394)
        Me.INDLcgRoot.TextVisible = False
        '
        'INDLcgMainData
        '
        Me.INDLcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMainData.AppearanceGroup.Options.UseForeColor = True
        Me.INDLcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Options.UseForeColor = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseForeColor = True
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMainData, False)
        Me.INDLcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciData, Me.INDLciVoucherTransaction, Me.INDLciDebitNote, Me.INDLciObservation})
        Me.INDLcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMainData.Name = "INDLcgMainData"
        Me.INDLcgMainData.Size = New System.Drawing.Size(807, 374)
        Me.INDLcgMainData.Text = "Datos Principales"
        '
        'INDLciData
        '
        Me.INDLciData.Control = Me.INDGleData
        Me.INDLciData.Location = New System.Drawing.Point(0, 0)
        Me.INDLciData.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciData.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciData.Name = "INDLciData"
        Me.INDLciData.ShowInCustomizationForm = False
        Me.INDLciData.Size = New System.Drawing.Size(783, 60)
        Me.INDLciData.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciData.Text = "Dato"
        Me.INDLciData.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciData.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciData.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciData.TextToControlDistance = 5
        '
        'INDLciVoucherTransaction
        '
        Me.INDLciVoucherTransaction.Control = Me.INDSleVoucherTransaction
        Me.INDLciVoucherTransaction.Location = New System.Drawing.Point(0, 60)
        Me.INDLciVoucherTransaction.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciVoucherTransaction.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciVoucherTransaction.Name = "INDLciVoucherTransaction"
        Me.INDLciVoucherTransaction.ShowInCustomizationForm = False
        Me.INDLciVoucherTransaction.Size = New System.Drawing.Size(783, 60)
        Me.INDLciVoucherTransaction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciVoucherTransaction.Text = "Comprobante Egreso"
        Me.INDLciVoucherTransaction.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciVoucherTransaction.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciVoucherTransaction.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciVoucherTransaction.TextToControlDistance = 5
        '
        'INDLciDebitNote
        '
        Me.INDLciDebitNote.Control = Me.INDTxtDebitNote
        Me.INDLciDebitNote.Location = New System.Drawing.Point(0, 120)
        Me.INDLciDebitNote.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciDebitNote.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciDebitNote.Name = "INDLciDebitNote"
        Me.INDLciDebitNote.Size = New System.Drawing.Size(783, 60)
        Me.INDLciDebitNote.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDebitNote.Text = "# Nota Debito"
        Me.INDLciDebitNote.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDebitNote.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDebitNote.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciDebitNote.TextToControlDistance = 5
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.MemoEdit1
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 180)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(390, 120)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(783, 135)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observación"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciObservation.TextToControlDistance = 5
        '
        'IndigoLayoutControl1
        '
        Me.IndigoLayoutControl1.CompanyName = ""
        Me.IndigoLayoutControl1.FormName = "FormBase20151029"
        Me.IndigoLayoutControl1.ProductName = "InfrastructureCrossCuttingBase"
        '
        'FrmUpdateFieldsVoucherTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 520)
        Me.Name = "FrmUpdateFieldsVoucherTransaction"
        Me.Opacity = 1.0R
        Me.Text = "Comprobante de Egreso - Actualización de Datos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDebitNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleVoucherTransaction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciVoucherTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControl
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleVoucherTransaction As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleData As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciData As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciVoucherTransaction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDTxtDebitNote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciDebitNote As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
