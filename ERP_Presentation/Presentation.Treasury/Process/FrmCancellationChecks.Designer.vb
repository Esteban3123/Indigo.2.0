<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCancellationChecks
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDmeDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDdeCancellationDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleEntityAccount = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtCheckNumber = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDliEntityAccount = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCancellationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliCheckNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDliDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRoot.SuspendLayout()
        CType(Me.INDdeCancellationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeCancellationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleEntityAccount.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCheckNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliEntityAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCancellationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliCheckNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(890, 529)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(890, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(890, 98)
        '
        'INDmeDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeDescription, True)
        Me.INDmeDescription.Location = New System.Drawing.Point(24, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeDescription.Name = "INDmeDescription"
        Me.INDmeDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmeDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmeDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeDescription.Size = New System.Drawing.Size(386, 70)
        Me.INDmeDescription.StyleController = Me.INDlycRoot
        Me.INDmeDescription.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeDescription, 0)
        Me.INDmeDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomization = False
        Me.INDlycRoot.Controls.Add(Me.INDmeDescription)
        Me.INDlycRoot.Controls.Add(Me.INDdeCancellationDate)
        Me.INDlycRoot.Controls.Add(Me.INDsleEntityAccount)
        Me.INDlycRoot.Controls.Add(Me.INDtxtCheckNumber)
        Me.INDlycRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, False)
        Me.INDlycRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlycRoot.Name = "INDlycRoot"
        Me.INDlycRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(526, 262, 561, 456)
        Me.INDlycRoot.Root = Me.LayoutControlGroup1
        Me.INDlycRoot.Size = New System.Drawing.Size(686, 520)
        Me.INDlycRoot.TabIndex = 1
        Me.INDlycRoot.Text = "LayoutControl1"
        '
        'INDdeCancellationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeCancellationDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeCancellationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeCancellationDate, True)
        Me.INDdeCancellationDate.EditValue = Nothing
        Me.INDdeCancellationDate.EnterMoveNextControl = True
        Me.INDdeCancellationDate.Location = New System.Drawing.Point(24, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeCancellationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeCancellationDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeCancellationDate.Name = "INDdeCancellationDate"
        Me.INDdeCancellationDate.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDdeCancellationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeCancellationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeCancellationDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeCancellationDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeCancellationDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeCancellationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeCancellationDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeCancellationDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeCancellationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeCancellationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeCancellationDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeCancellationDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdeCancellationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeCancellationDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeCancellationDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeCancellationDate.StyleController = Me.INDlycRoot
        Me.INDdeCancellationDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeCancellationDate, 0)
        Me.INDdeCancellationDate.ToolTip = "Este Campo es Necesario"
        '
        'INDsleEntityAccount
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleEntityAccount, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleEntityAccount, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleEntityAccount, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleEntityAccount, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleEntityAccount, False)
        Me.INDsleEntityAccount.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleEntityAccount, False)
        Me.INDsleEntityAccount.Location = New System.Drawing.Point(24, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleEntityAccount, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleEntityAccount.Name = "INDsleEntityAccount"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleEntityAccount, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleEntityAccount, False)
        Me.INDsleEntityAccount.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleEntityAccount.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleEntityAccount.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleEntityAccount.Properties.Appearance.Options.UseFont = True
        Me.INDsleEntityAccount.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDsleEntityAccount.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDsleEntityAccount.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDsleEntityAccount.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Treasury.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDsleEntityAccount.Properties.DisplayMember = "CodeName"
        Me.INDsleEntityAccount.Properties.NullText = ""
        Me.INDsleEntityAccount.Properties.PopupFormMinSize = New System.Drawing.Size(700, 0)
        Me.INDsleEntityAccount.Properties.PopupFormSize = New System.Drawing.Size(700, 0)
        Me.INDsleEntityAccount.Properties.PopupSizeable = False
        Me.INDsleEntityAccount.Properties.ShowFooter = False
        Me.INDsleEntityAccount.Properties.ValueMember = "Id"
        Me.INDsleEntityAccount.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleEntityAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleEntityAccount, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleEntityAccount, True)
        Me.INDsleEntityAccount.Size = New System.Drawing.Size(386, 28)
        Me.INDsleEntityAccount.StyleController = Me.INDlycRoot
        Me.INDsleEntityAccount.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleEntityAccount, "628")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleEntityAccount, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleEntityAccount, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleEntityAccount, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleEntityAccount, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
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
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 252
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Banco"
        Me.GridColumn2.FieldName = "IdBank.Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 422
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Num Cuenta"
        Me.GridColumn3.FieldName = "Number"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 242
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cuenta Contable"
        Me.GridColumn4.FieldName = "IdMainAccount.NumberName"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 254
        '
        'INDtxtCheckNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCheckNumber, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCheckNumber, False)
        Me.INDtxtCheckNumber.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDtxtCheckNumber.Location = New System.Drawing.Point(24, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCheckNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCheckNumber.Name = "INDtxtCheckNumber"
        Me.INDtxtCheckNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtCheckNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCheckNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCheckNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCheckNumber.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtCheckNumber.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtCheckNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCheckNumber.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtCheckNumber.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtCheckNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCheckNumber.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDtxtCheckNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.[Default]
        Me.INDtxtCheckNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.INDtxtCheckNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtCheckNumber.StyleController = Me.INDlycRoot
        Me.INDtxtCheckNumber.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCheckNumber, 0)
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
        Me.LayoutControlGroup1.CustomizationFormText = "Cancelación de cheques"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(686, 520)
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
        Me.LayoutControlGroup2.CustomizationFormText = "Datos Principales"
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDliEntityAccount, Me.INDliCancellationDate, Me.INDliCheckNumber, Me.INDliDescription})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(666, 500)
        Me.LayoutControlGroup2.Text = "Datos Principales"
        '
        'INDliEntityAccount
        '
        Me.INDliEntityAccount.AllowHide = False
        Me.INDliEntityAccount.Control = Me.INDsleEntityAccount
        Me.INDliEntityAccount.CustomizationFormText = "Cuenta Bancaria"
        Me.INDliEntityAccount.Location = New System.Drawing.Point(0, 0)
        Me.INDliEntityAccount.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliEntityAccount.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliEntityAccount.Name = "INDliEntityAccount"
        Me.INDliEntityAccount.ShowInCustomizationForm = False
        Me.INDliEntityAccount.Size = New System.Drawing.Size(642, 60)
        Me.INDliEntityAccount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliEntityAccount.Text = "Cuenta Bancaria"
        Me.INDliEntityAccount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliEntityAccount.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliEntityAccount.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliEntityAccount.TextToControlDistance = 5
        '
        'INDliCancellationDate
        '
        Me.INDliCancellationDate.AllowHide = False
        Me.INDliCancellationDate.Control = Me.INDdeCancellationDate
        Me.INDliCancellationDate.CustomizationFormText = "Fecha"
        Me.INDliCancellationDate.Location = New System.Drawing.Point(0, 60)
        Me.INDliCancellationDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCancellationDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCancellationDate.Name = "INDliCancellationDate"
        Me.INDliCancellationDate.ShowInCustomizationForm = False
        Me.INDliCancellationDate.Size = New System.Drawing.Size(642, 60)
        Me.INDliCancellationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCancellationDate.Text = "Fecha"
        Me.INDliCancellationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCancellationDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCancellationDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliCancellationDate.TextToControlDistance = 5
        '
        'INDliCheckNumber
        '
        Me.INDliCheckNumber.AllowHide = False
        Me.INDliCheckNumber.Control = Me.INDtxtCheckNumber
        Me.INDliCheckNumber.CustomizationFormText = "Cheque"
        Me.INDliCheckNumber.Location = New System.Drawing.Point(0, 120)
        Me.INDliCheckNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDliCheckNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDliCheckNumber.Name = "INDliCheckNumber"
        Me.INDliCheckNumber.ShowInCustomizationForm = False
        Me.INDliCheckNumber.Size = New System.Drawing.Size(642, 60)
        Me.INDliCheckNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliCheckNumber.Text = "Cheque"
        Me.INDliCheckNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliCheckNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliCheckNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliCheckNumber.TextToControlDistance = 5
        '
        'INDliDescription
        '
        Me.INDliDescription.AllowHide = False
        Me.INDliDescription.Control = Me.INDmeDescription
        Me.INDliDescription.CustomizationFormText = "Detalle"
        Me.INDliDescription.Location = New System.Drawing.Point(0, 180)
        Me.INDliDescription.MaxSize = New System.Drawing.Size(390, 100)
        Me.INDliDescription.MinSize = New System.Drawing.Size(390, 100)
        Me.INDliDescription.Name = "INDliDescription"
        Me.INDliDescription.ShowInCustomizationForm = False
        Me.INDliDescription.Size = New System.Drawing.Size(642, 261)
        Me.INDliDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDliDescription.Text = "Detalle"
        Me.INDliDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDliDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDliDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDliDescription.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlycRoot
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 520)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmCancellationChecks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 651)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmCancellationChecks"
        Me.Opacity = 1.0R
        Me.Tag = "641"
        Me.Text = "Anulación de Cheques"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmeDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRoot.ResumeLayout(False)
        CType(Me.INDdeCancellationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeCancellationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleEntityAccount.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCheckNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliEntityAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCancellationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliCheckNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDliDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoRadioGroup1 As Presentation.Controls.IndigoRadioGroup
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDmeDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDdeCancellationDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDliEntityAccount As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliCancellationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliCheckNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDliDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDsleEntityAccount As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDtxtCheckNumber As DevExpress.XtraEditors.SpinEdit
End Class
