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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDMeObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtDebitNote = New DevExpress.XtraEditors.TextEdit()
        Me.INDSleVoucherTransaction = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDGvVoucherTransaction = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDGleDataUpdate = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDLcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLcgMainData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciVoucherTransaction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciDebitNote = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtDebitNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleVoucherTransaction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvVoucherTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleDataUpdate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciVoucherTransaction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciDebitNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLcRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(857, 398)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(857, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(857, 98)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDLcRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 389)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDMeObservation)
        Me.INDLcRoot.Controls.Add(Me.INDTxtDebitNote)
        Me.INDLcRoot.Controls.Add(Me.INDSleVoucherTransaction)
        Me.INDLcRoot.Controls.Add(Me.INDGleDataUpdate)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(660, 320, 250, 350)
        Me.INDLcRoot.Root = Me.INDLcgRoot
        Me.INDLcRoot.Size = New System.Drawing.Size(653, 389)
        Me.INDLcRoot.TabIndex = 1
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDMeObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeObservation, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeObservation, False)
        Me.INDMeObservation.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDMeObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeObservation.Name = "INDMeObservation"
        Me.INDMeObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDMeObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeObservation.Properties.Appearance.Options.UseFont = True
        Me.INDMeObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeObservation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeObservation.Size = New System.Drawing.Size(386, 90)
        Me.INDMeObservation.StyleController = Me.INDLcRoot
        Me.INDMeObservation.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeObservation, 0)
        '
        'INDTxtDebitNote
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtDebitNote, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtDebitNote, False)
        Me.INDTxtDebitNote.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtDebitNote, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtDebitNote.Name = "INDTxtDebitNote"
        Me.INDTxtDebitNote.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
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
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleVoucherTransaction, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleVoucherTransaction, AppearanceObject2)
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
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleVoucherTransaction, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.INDSleVoucherTransaction.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleVoucherTransaction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucherTransaction.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleVoucherTransaction.Properties.Appearance.Options.UseFont = True
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleVoucherTransaction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleVoucherTransaction.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleVoucherTransaction.Properties.DisplayMember = "CodeClass"
        Me.INDSleVoucherTransaction.Properties.NullText = ""
        Me.INDSleVoucherTransaction.Properties.PopupSizeable = False
        Me.INDSleVoucherTransaction.Properties.ShowFooter = False
        Me.INDSleVoucherTransaction.Properties.ValueMember = "Code"
        Me.INDSleVoucherTransaction.Properties.View = Me.INDGvVoucherTransaction
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleVoucherTransaction, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleVoucherTransaction, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleVoucherTransaction, True)
        Me.INDSleVoucherTransaction.Size = New System.Drawing.Size(386, 28)
        Me.INDSleVoucherTransaction.StyleController = Me.INDLcRoot
        Me.INDSleVoucherTransaction.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleVoucherTransaction, "636")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleVoucherTransaction, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleVoucherTransaction, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleVoucherTransaction, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleVoucherTransaction, False)
        '
        'INDGvVoucherTransaction
        '
        Me.INDGvVoucherTransaction.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDGvVoucherTransaction.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDGvVoucherTransaction.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDGvVoucherTransaction.Appearance.FocusedRow.Options.UseFont = True
        Me.INDGvVoucherTransaction.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvVoucherTransaction.Appearance.GroupRow.Options.UseFont = True
        Me.INDGvVoucherTransaction.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGvVoucherTransaction.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGvVoucherTransaction.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDGvVoucherTransaction.Appearance.Row.Options.UseFont = True
        Me.INDGvVoucherTransaction.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn5, Me.GridColumn4})
        Me.INDGvVoucherTransaction.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvVoucherTransaction.Name = "INDGvVoucherTransaction"
        Me.INDGvVoucherTransaction.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvVoucherTransaction.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGvVoucherTransaction.OptionsView.EnableAppearanceOddRow = True
        Me.INDGvVoucherTransaction.OptionsView.ShowAutoFilterRow = True
        Me.INDGvVoucherTransaction.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDGvVoucherTransaction, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 359
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Clase"
        Me.GridColumn3.FieldName = "VoucherClassName"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 483
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Valor"
        Me.GridColumn5.DisplayFormat.FormatString = "C0"
        Me.GridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn5.FieldName = "Value"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 256
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Estado"
        Me.GridColumn4.FieldName = "StatusName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 294
        '
        'INDGleDataUpdate
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleDataUpdate, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleDataUpdate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleDataUpdate, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleDataUpdate, False)
        Me.INDGleDataUpdate.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleDataUpdate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleDataUpdate.Name = "INDGleDataUpdate"
        Me.INDGleDataUpdate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDGleDataUpdate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleDataUpdate.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleDataUpdate.Properties.Appearance.Options.UseFont = True
        Me.INDGleDataUpdate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDGleDataUpdate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDGleDataUpdate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleDataUpdate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleDataUpdate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleDataUpdate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleDataUpdate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleDataUpdate.Properties.DisplayMember = "Item2"
        Me.INDGleDataUpdate.Properties.ImmediatePopup = True
        Me.INDGleDataUpdate.Properties.NullText = ""
        Me.INDGleDataUpdate.Properties.ValueMember = "Item1"
        Me.INDGleDataUpdate.Properties.View = Me.GridLookUpEdit1View
        Me.INDGleDataUpdate.Size = New System.Drawing.Size(386, 28)
        Me.INDGleDataUpdate.StyleController = Me.INDLcRoot
        Me.INDGleDataUpdate.TabIndex = 0
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleDataUpdate, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleDataUpdate, 0)
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
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
        Me.GridColumn1.Caption = "Campos"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDLcgRoot
        '
        Me.INDLcgRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDLcgRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgRoot, False)
        Me.INDLcgRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgRoot.GroupBordersVisible = False
        Me.INDLcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLcgMainData})
        Me.INDLcgRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgRoot.Name = "Root"
        Me.INDLcgRoot.Size = New System.Drawing.Size(653, 389)
        Me.INDLcgRoot.TextVisible = False
        '
        'INDLcgMainData
        '
        Me.INDLcgMainData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceGroup.Options.UseFont = True
        Me.INDLcgMainData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLcgMainData.AppearanceItemCaption.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLcgMainData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLcgMainData, False)
        Me.INDLcgMainData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciType, Me.INDLciVoucherTransaction, Me.INDLciDebitNote, Me.INDLciObservation})
        Me.INDLcgMainData.Location = New System.Drawing.Point(0, 0)
        Me.INDLcgMainData.Name = "INDLcgMainData"
        Me.INDLcgMainData.Size = New System.Drawing.Size(633, 369)
        Me.INDLcgMainData.Text = "Datos Principales"
        '
        'INDLciType
        '
        Me.INDLciType.Control = Me.INDGleDataUpdate
        Me.INDLciType.Location = New System.Drawing.Point(0, 0)
        Me.INDLciType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciType.Name = "INDLciType"
        Me.INDLciType.ShowInCustomizationForm = False
        Me.INDLciType.Size = New System.Drawing.Size(609, 60)
        Me.INDLciType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciType.Text = "Campos"
        Me.INDLciType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciType.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciType.TextToControlDistance = 5
        '
        'INDLciVoucherTransaction
        '
        Me.INDLciVoucherTransaction.Control = Me.INDSleVoucherTransaction
        Me.INDLciVoucherTransaction.Location = New System.Drawing.Point(0, 60)
        Me.INDLciVoucherTransaction.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciVoucherTransaction.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciVoucherTransaction.Name = "INDLciVoucherTransaction"
        Me.INDLciVoucherTransaction.ShowInCustomizationForm = False
        Me.INDLciVoucherTransaction.Size = New System.Drawing.Size(609, 60)
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
        Me.INDLciDebitNote.Size = New System.Drawing.Size(609, 60)
        Me.INDLciDebitNote.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciDebitNote.Text = "# Nota Debito"
        Me.INDLciDebitNote.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciDebitNote.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciDebitNote.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciDebitNote.TextToControlDistance = 5
        '
        'INDLciObservation
        '
        Me.INDLciObservation.Control = Me.INDMeObservation
        Me.INDLciObservation.Location = New System.Drawing.Point(0, 180)
        Me.INDLciObservation.MaxSize = New System.Drawing.Size(390, 120)
        Me.INDLciObservation.MinSize = New System.Drawing.Size(390, 120)
        Me.INDLciObservation.Name = "INDLciObservation"
        Me.INDLciObservation.Size = New System.Drawing.Size(609, 130)
        Me.INDLciObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciObservation.Text = "Observación"
        Me.INDLciObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciObservation.TextSize = New System.Drawing.Size(143, 21)
        Me.INDLciObservation.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmUpdateFieldsVoucherTransaction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(857, 520)
        Me.Name = "FrmUpdateFieldsVoucherTransaction"
        Me.Opacity = 1.0R
        Me.Tag = "1679"
        Me.Text = "Comprobante de Egreso - Actualización de Datos"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDMeObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtDebitNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleVoucherTransaction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvVoucherTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleDataUpdate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgMainData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciVoucherTransaction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciDebitNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSleVoucherTransaction As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvVoucherTransaction As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDGleDataUpdate As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLcgMainData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciVoucherTransaction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDMeObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDTxtDebitNote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciDebitNote As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
