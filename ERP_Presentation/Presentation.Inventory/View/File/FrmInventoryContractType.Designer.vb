Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInventoryContractType
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
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.LyContractType = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDGleType = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDGleTypeView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDBtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyGpContractType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLyBtnCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyTxtName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLyGleType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LyContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LyContractType.SuspendLayout()
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGleTypeView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGpContractType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyBtnCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyTxtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyGleType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LyContractType)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 134)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(768, 379)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 4)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ToolBars.Size = New System.Drawing.Size(768, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.BarraBotones.Size = New System.Drawing.Size(768, 130)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.LyContractType
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 6)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 371)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'LyContractType
        '
        Me.LyContractType.AllowCustomization = False
        Me.LyContractType.Controls.Add(Me.INDTxtName)
        Me.LyContractType.Controls.Add(Me.INDGleType)
        Me.LyContractType.Controls.Add(Me.INDBtnCode)
        Me.LyContractType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.LyContractType, False)
        Me.LyContractType.Location = New System.Drawing.Point(202, 6)
        Me.LyContractType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.LyContractType.Name = "LyContractType"
        Me.LyContractType.Root = Me.LayoutControlGroup1
        Me.LyContractType.Size = New System.Drawing.Size(564, 371)
        Me.LyContractType.TabIndex = 1
        Me.LyContractType.Text = "LayoutControl1"
        '
        'INDTxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtName, True)
        Me.INDTxtName.EnterMoveNextControl = True
        Me.INDTxtName.Location = New System.Drawing.Point(78, 91)
        Me.INDTxtName.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtName.Name = "INDTxtName"
        Me.INDTxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDTxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDTxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtName.Properties.Appearance.Options.UseFont = True
        Me.INDTxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDTxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtName.Properties.MaxLength = 100
        Me.INDTxtName.Size = New System.Drawing.Size(332, 28)
        Me.INDTxtName.StyleController = Me.LyContractType
        Me.INDTxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtName, 0)
        Me.INDTxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDGleType
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDGleType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDGleType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDGleType, True)
        Me.INDGleType.EnterMoveNextControl = True
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDGleType, False)
        Me.INDGleType.Location = New System.Drawing.Point(78, 127)
        Me.INDGleType.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDGleType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDGleType.Name = "INDGleType"
        Me.INDGleType.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDGleType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleType.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDGleType.Properties.Appearance.Options.UseBackColor = True
        Me.INDGleType.Properties.Appearance.Options.UseFont = True
        Me.INDGleType.Properties.Appearance.Options.UseForeColor = True
        Me.INDGleType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDGleType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDGleType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDGleType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDGleType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDGleType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDGleType.Properties.DisplayMember = "Item2"
        Me.INDGleType.Properties.ImmediatePopup = True
        Me.INDGleType.Properties.NullText = ""
        Me.INDGleType.Properties.PopupView = Me.INDGleTypeView
        Me.INDGleType.Properties.ValueMember = "Item1"
        Me.INDGleType.Size = New System.Drawing.Size(332, 28)
        Me.INDGleType.StyleController = Me.LyContractType
        Me.INDGleType.TabIndex = 2
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDGleType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDGleType, 0)
        Me.INDGleType.ToolTip = "Este Campo es Necesario"
        '
        'INDGleTypeView
        '
        Me.INDGleTypeView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeView.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDGleTypeView.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDGleTypeView.Appearance.Row.Options.UseFont = True
        Me.INDGleTypeView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.INDGleTypeView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGleTypeView.Name = "INDGleTypeView"
        Me.INDGleTypeView.OptionsBehavior.Editable = False
        Me.INDGleTypeView.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGleTypeView.OptionsView.EnableAppearanceEvenRow = True
        Me.INDGleTypeView.OptionsView.EnableAppearanceOddRow = True
        Me.INDGleTypeView.OptionsView.ShowDetailButtons = False
        Me.INDGleTypeView.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'INDBtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBtnCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBtnCode, True)
        Me.INDBtnCode.Location = New System.Drawing.Point(78, 53)
        Me.INDBtnCode.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.IndigoTextEdit1.SetMascara(Me.INDBtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDBtnCode.Name = "INDBtnCode"
        Me.INDBtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDBtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDBtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions2.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDBtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDBtnCode.Properties.MaxLength = 20
        Me.INDBtnCode.Size = New System.Drawing.Size(332, 28)
        Me.INDBtnCode.StyleController = Me.LyContractType
        Me.INDBtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBtnCode, 0)
        Me.INDBtnCode.ToolTip = "Este Campo es Necesario"
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
        Me.LayoutControlGroup1.CustomizationFormText = "Tipos de Contrato"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyGpContractType})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(564, 371)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLyGpContractType
        '
        Me.INDLyGpContractType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGpContractType.AppearanceGroup.Options.UseFont = True
        Me.INDLyGpContractType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDLyGpContractType.AppearanceItemCaption.Options.UseFont = True
        Me.INDLyGpContractType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpContractType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDLyGpContractType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDLyGpContractType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDLyGpContractType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpContractType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDLyGpContractType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpContractType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDLyGpContractType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDLyGpContractType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDLyGpContractType, False)
        Me.INDLyGpContractType.CustomizationFormText = "Datos Generales"
        Me.INDLyGpContractType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLyBtnCode, Me.INDLyTxtName, Me.INDLyGleType})
        Me.INDLyGpContractType.Location = New System.Drawing.Point(0, 0)
        Me.INDLyGpContractType.Name = "INDLyGpContractType"
        Me.INDLyGpContractType.Size = New System.Drawing.Size(544, 351)
        Me.INDLyGpContractType.Text = "Datos Generales"
        '
        'INDLyBtnCode
        '
        Me.INDLyBtnCode.Control = Me.INDBtnCode
        Me.INDLyBtnCode.CustomizationFormText = "LayoutControlItem1"
        Me.INDLyBtnCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLyBtnCode.MaxSize = New System.Drawing.Size(390, 38)
        Me.INDLyBtnCode.MinSize = New System.Drawing.Size(390, 38)
        Me.INDLyBtnCode.Name = "INDLyBtnCode"
        Me.INDLyBtnCode.ShowInCustomizationForm = False
        Me.INDLyBtnCode.Size = New System.Drawing.Size(520, 38)
        Me.INDLyBtnCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyBtnCode.Text = "Código"
        Me.INDLyBtnCode.TextSize = New System.Drawing.Size(51, 17)
        '
        'INDLyTxtName
        '
        Me.INDLyTxtName.Control = Me.INDTxtName
        Me.INDLyTxtName.CustomizationFormText = "LayoutControlItem2"
        Me.INDLyTxtName.Location = New System.Drawing.Point(0, 38)
        Me.INDLyTxtName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyTxtName.Name = "INDLyTxtName"
        Me.INDLyTxtName.ShowInCustomizationForm = False
        Me.INDLyTxtName.Size = New System.Drawing.Size(520, 36)
        Me.INDLyTxtName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyTxtName.Text = "Nombre"
        Me.INDLyTxtName.TextSize = New System.Drawing.Size(51, 17)
        '
        'INDLyGleType
        '
        Me.INDLyGleType.Control = Me.INDGleType
        Me.INDLyGleType.CustomizationFormText = "LayoutControlItem3"
        Me.INDLyGleType.Location = New System.Drawing.Point(0, 74)
        Me.INDLyGleType.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDLyGleType.MinSize = New System.Drawing.Size(390, 36)
        Me.INDLyGleType.Name = "INDLyGleType"
        Me.INDLyGleType.ShowInCustomizationForm = False
        Me.INDLyGleType.Size = New System.Drawing.Size(520, 224)
        Me.INDLyGleType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLyGleType.Text = "Tipo"
        Me.INDLyGleType.TextSize = New System.Drawing.Size(51, 17)
        '
        'FrmInventoryContractType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(768, 513)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "FrmInventoryContractType"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.Tag = "1400"
        Me.Text = "Tipos de Contrato"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LyContractType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LyContractType.ResumeLayout(False)
        CType(Me.INDTxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGleTypeView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGpContractType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyBtnCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyTxtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyGleType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LyContractType As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDLyGpContractType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents INDTxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLyBtnCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyTxtName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLyGleType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDGleType As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDGleTypeView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDBtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
End Class
