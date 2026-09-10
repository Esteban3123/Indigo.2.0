<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPatientBonus
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPatientBonus))
        Me.INDLcRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleShareType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDSpAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSpnBonusValue = New DevExpress.XtraEditors.SpinEdit()
        Me.INDLcgRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciValue = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciAdd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLcRoot.SuspendLayout()
        CType(Me.INDSleShareType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSpnBonusValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDLcRoot
        '
        Me.INDLcRoot.Controls.Add(Me.INDSleShareType)
        Me.INDLcRoot.Controls.Add(Me.INDSpAdd)
        Me.INDLcRoot.Controls.Add(Me.INDSpnBonusValue)
        Me.INDLcRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDLcRoot.Location = New System.Drawing.Point(0, 0)
        Me.INDLcRoot.Name = "INDLcRoot"
        Me.INDLcRoot.Root = Me.INDLcgRoot
        Me.INDLcRoot.Size = New System.Drawing.Size(410, 194)
        Me.INDLcRoot.TabIndex = 0
        Me.INDLcRoot.Text = "LayoutControl1"
        '
        'INDSleShareType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleShareType, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleShareType, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleShareType, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleShareType, False)
        Me.INDSleShareType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleShareType, False)
        Me.INDSleShareType.Location = New System.Drawing.Point(12, 38)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleShareType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleShareType.Name = "INDSleShareType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleShareType, False)
        Me.INDSleShareType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleShareType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleShareType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDSleShareType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleShareType.Properties.Appearance.Options.UseFont = True
        Me.INDSleShareType.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleShareType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleShareType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleShareType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleShareType.Properties.DisplayMember = "Item2"
        Me.INDSleShareType.Properties.NullText = ""
        Me.INDSleShareType.Properties.PopupSizeable = False
        Me.INDSleShareType.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleShareType.Properties.ShowFooter = False
        Me.INDSleShareType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleShareType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleShareType, True)
        Me.INDSleShareType.Size = New System.Drawing.Size(386, 28)
        Me.INDSleShareType.StyleController = Me.INDLcRoot
        Me.INDSleShareType.TabIndex = 4
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleShareType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleShareType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleShareType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleShareType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleShareType, False)
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
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
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
        Me.GridColumn1.Caption = "Tipo"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDSpAdd
        '
        Me.INDSpAdd.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 10.0!)
        Me.INDSpAdd.Appearance.Options.UseFont = True
        Me.INDSpAdd.Location = New System.Drawing.Point(12, 132)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDSpAdd, False)
        Me.INDSpAdd.Name = "INDSpAdd"
        Me.INDSpAdd.Size = New System.Drawing.Size(386, 28)
        Me.INDSpAdd.StyleController = Me.INDLcRoot
        Me.INDSpAdd.TabIndex = 1
        Me.INDSpAdd.Text = "Aceptar"
        '
        'INDSpnBonusValue
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSpnBonusValue, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSpnBonusValue, False)
        Me.INDSpnBonusValue.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDSpnBonusValue.EnterMoveNextControl = True
        Me.INDSpnBonusValue.Location = New System.Drawing.Point(12, 97)
        Me.IndigoTextEdit1.SetMascara(Me.INDSpnBonusValue, Presentation.Controls.IndigoTextEdit.EMask.Moneda)
        Me.INDSpnBonusValue.Name = "INDSpnBonusValue"
        Me.INDSpnBonusValue.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnBonusValue.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnBonusValue.Properties.Appearance.Options.UseBackColor = True
        Me.INDSpnBonusValue.Properties.Appearance.Options.UseFont = True
        Me.INDSpnBonusValue.Properties.Appearance.Options.UseTextOptions = True
        Me.INDSpnBonusValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDSpnBonusValue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSpnBonusValue.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSpnBonusValue.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSpnBonusValue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSpnBonusValue.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSpnBonusValue.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSpnBonusValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSpnBonusValue.Properties.Mask.EditMask = "c0"
        Me.INDSpnBonusValue.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSpnBonusValue.Properties.MaxLength = 15
        Me.INDSpnBonusValue.Size = New System.Drawing.Size(386, 28)
        Me.INDSpnBonusValue.StyleController = Me.INDLcRoot
        Me.INDSpnBonusValue.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSpnBonusValue, 0)
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
        Me.INDLcgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciValue, Me.INDLciAdd, Me.LayoutControlItem1})
        Me.INDLcgRoot.Name = "INDLcgRoot"
        Me.INDLcgRoot.Size = New System.Drawing.Size(410, 194)
        Me.INDLcgRoot.TextVisible = False
        '
        'INDLciValue
        '
        Me.INDLciValue.Control = Me.INDSpnBonusValue
        Me.INDLciValue.Location = New System.Drawing.Point(0, 60)
        Me.INDLciValue.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLciValue.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLciValue.Name = "INDLciValue"
        Me.INDLciValue.Size = New System.Drawing.Size(390, 60)
        Me.INDLciValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValue.Text = "Valor"
        Me.INDLciValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValue.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValue.TextSize = New System.Drawing.Size(50, 20)
        Me.INDLciValue.TextToControlDistance = 5
        '
        'INDLciAdd
        '
        Me.INDLciAdd.Control = Me.INDSpAdd
        Me.INDLciAdd.Location = New System.Drawing.Point(0, 120)
        Me.INDLciAdd.MaxSize = New System.Drawing.Size(390, 32)
        Me.INDLciAdd.MinSize = New System.Drawing.Size(390, 32)
        Me.INDLciAdd.Name = "INDLciAdd"
        Me.INDLciAdd.Size = New System.Drawing.Size(390, 54)
        Me.INDLciAdd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciAdd.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciAdd.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLciAdd.TextToControlDistance = 0
        Me.INDLciAdd.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDSleShareType
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem1.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem1.Text = "Tipo de Cuota"
        Me.LayoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem1.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'FrmPatientBonus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 194)
        Me.Controls.Add(Me.INDLcRoot)
        Me.IconOptions.SvgImage = CType(resources.GetObject("FrmPatientBonus.IconOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.IconOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPatientBonus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bono a Paciente"
        CType(Me.INDLcRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLcRoot.ResumeLayout(False)
        CType(Me.INDSleShareType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSpnBonusValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLcRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDLcgRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDSpnBonusValue As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLciValue As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDSpAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLciAdd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleShareType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
End Class
