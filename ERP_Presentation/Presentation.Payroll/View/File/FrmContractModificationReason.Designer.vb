Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmContractModificationReason
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmContractModificationReason))
        Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
        Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyCtlContractModificationReason = New DevExpress.XtraLayout.LayoutControl()
        Me.INDSleNoveltyType = New Presentation.Controls.CtrYesNo()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDmemDescription = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlcgContractModificationReason = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlcgNewContractModificationReason = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlciDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlciNoveltyType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit11 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtlContractModificationReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtlContractModificationReason.SuspendLayout()
        CType(Me.INDSleNoveltyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleNoveltyType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgContractModificationReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlcgNewContractModificationReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlciNoveltyType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtlContractModificationReason)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 140)
        Me.INDPanelControlBase.Margin = New System.Windows.Forms.Padding(6)
        Me.INDPanelControlBase.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1176, 680)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Location = New System.Drawing.Point(0, 10)
        Me.ToolBars.Margin = New System.Windows.Forms.Padding(6)
        Me.ToolBars.Size = New System.Drawing.Size(1176, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Margin = New System.Windows.Forms.Padding(9)
        Me.BarraBotones.Size = New System.Drawing.Size(1176, 130)
        '
        'INDlyCtlContractModificationReason
        '
        Me.INDlyCtlContractModificationReason.AllowCustomization = False
        Me.INDlyCtlContractModificationReason.Controls.Add(Me.INDSleNoveltyType)
        Me.INDlyCtlContractModificationReason.Controls.Add(Me.INDmemDescription)
        Me.INDlyCtlContractModificationReason.Controls.Add(Me.INDtxtName)
        Me.INDlyCtlContractModificationReason.Controls.Add(Me.INDbteCode)
        Me.INDlyCtlContractModificationReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCtlContractModificationReason, False)
        Me.INDlyCtlContractModificationReason.Location = New System.Drawing.Point(2, 12)
        Me.INDlyCtlContractModificationReason.Margin = New System.Windows.Forms.Padding(4)
        Me.INDlyCtlContractModificationReason.Name = "INDlyCtlContractModificationReason"
        Me.INDlyCtlContractModificationReason.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(586, 362, 808, 522)
        Me.INDlyCtlContractModificationReason.Root = Me.INDlcgContractModificationReason
        Me.INDlyCtlContractModificationReason.Size = New System.Drawing.Size(1172, 666)
        Me.INDlyCtlContractModificationReason.TabIndex = 0
        Me.INDlyCtlContractModificationReason.Text = "Razones de Modificación de Contrato"
        '
        'INDSleNoveltyType
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleNoveltyType, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDSleNoveltyType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleNoveltyType, False)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDSleNoveltyType, False)
        Me.INDSleNoveltyType.EnterMoveNextControl = True
        Me.INDSleNoveltyType.Location = New System.Drawing.Point(306, 346)
        Me.INDSleNoveltyType.Margin = New System.Windows.Forms.Padding(7, 20, 7, 20)
        Me.IndigoTextEdit11.SetMascara(Me.INDSleNoveltyType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleNoveltyType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleNoveltyType.MaximumSize = New System.Drawing.Size(387, 35)
        Me.INDSleNoveltyType.MinimumSize = New System.Drawing.Size(387, 35)
        Me.INDSleNoveltyType.Name = "INDSleNoveltyType"
        Me.INDSleNoveltyType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleNoveltyType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleNoveltyType.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleNoveltyType.Properties.Appearance.Options.UseFont = True
        Me.INDSleNoveltyType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSleNoveltyType.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSleNoveltyType.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSleNoveltyType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSleNoveltyType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSleNoveltyType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleNoveltyType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleNoveltyType.Properties.DataSource = CType(resources.GetObject("INDSleNoveltyType.Properties.DataSource"), Object)
        Me.INDSleNoveltyType.Properties.DisplayMember = "Item2"
        Me.INDSleNoveltyType.Properties.ImmediatePopup = True
        Me.INDSleNoveltyType.Properties.NullText = ""
        Me.INDSleNoveltyType.Properties.PopupView = Me.GridView5
        Me.INDSleNoveltyType.Properties.ValueMember = "Item1"
        Me.INDSleNoveltyType.Size = New System.Drawing.Size(387, 35)
        Me.INDSleNoveltyType.StyleController = Me.INDlyCtlContractModificationReason
        SuperToolTip1.DistanceBetweenItems = 20
        ToolTipItem1.Text = "Impuesto del Cree"
        SuperToolTip1.Items.Add(ToolTipItem1)
        SuperToolTip1.Padding = New System.Windows.Forms.Padding(0, 20, 0, 20)
        Me.INDSleNoveltyType.SuperTip = SuperToolTip1
        Me.INDSleNoveltyType.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleNoveltyType, 0)
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDSleNoveltyType, 0)
        '
        'GridView5
        '
        Me.GridView5.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView5.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView5.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView5.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView5.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView5.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.GridView5.Appearance.GroupRow.Options.UseFont = True
        Me.GridView5.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.GridView5.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView5.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView5.Appearance.Row.Options.UseFont = True
        Me.GridView5.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.GridView5.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView5.Name = "GridView5"
        Me.GridView5.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView5.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView5.OptionsView.EnableAppearanceOddRow = True
        Me.GridView5.OptionsView.ShowAutoFilterRow = True
        Me.GridView5.OptionsView.ShowDetailButtons = False
        Me.GridView5.OptionsView.ShowGroupPanel = False
        '
        'GridColumn4
        '
        Me.GridColumn4.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn4.Caption = "Selección"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        '
        'GridColumn5
        '
        Me.GridColumn5.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn5.Caption = "Selección"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDmemDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemDescription, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDmemDescription, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemDescription, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDmemDescription, False)
        Me.INDmemDescription.Location = New System.Drawing.Point(306, 174)
        Me.INDmemDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.IndigoTextEdit11.SetMascara(Me.INDmemDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemDescription.MaximumSize = New System.Drawing.Size(0, 145)
        Me.INDmemDescription.MinimumSize = New System.Drawing.Size(0, 145)
        Me.INDmemDescription.Name = "INDmemDescription"
        Me.INDmemDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDmemDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemDescription.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDmemDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemDescription.Properties.Appearance.Options.UseFont = True
        Me.INDmemDescription.Properties.Appearance.Options.UseForeColor = True
        Me.INDmemDescription.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmemDescription.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmemDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemDescription.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmemDescription.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmemDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemDescription.Properties.MaxLength = 1000
        Me.INDmemDescription.Size = New System.Drawing.Size(389, 145)
        Me.INDmemDescription.StyleController = Me.INDlyCtlContractModificationReason
        Me.INDmemDescription.TabIndex = 6
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDmemDescription, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemDescription, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDtxtName, False)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(306, 121)
        Me.INDtxtName.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit11.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.Appearance.Options.UseForeColor = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtName.Properties.MaxLength = 50
        Me.INDtxtName.Size = New System.Drawing.Size(389, 38)
        Me.INDtxtName.StyleController = Me.INDlyCtlContractModificationReason
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDbteCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit11.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.IndigoTextEdit11.SetCampoObligatorio(Me.INDbteCode, False)
        Me.INDbteCode.Location = New System.Drawing.Point(306, 68)
        Me.INDbteCode.Margin = New System.Windows.Forms.Padding(4)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.IndigoTextEdit11.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteCode.MinimumSize = New System.Drawing.Size(276, 0)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbteCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteCode.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDbteCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Size = New System.Drawing.Size(276, 38)
        Me.INDbteCode.StyleController = Me.INDlyCtlContractModificationReason
        Me.INDbteCode.TabIndex = 4
        Me.IndigoTextEdit11.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        Me.INDbteCode.ToolTip = "Este Campo es Necesario"
        '
        'INDlcgContractModificationReason
        '
        Me.INDlcgContractModificationReason.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractModificationReason.AppearanceGroup.Options.UseFont = True
        Me.INDlcgContractModificationReason.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgContractModificationReason.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgContractModificationReason.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractModificationReason.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgContractModificationReason.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgContractModificationReason.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgContractModificationReason.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractModificationReason.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgContractModificationReason.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractModificationReason.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgContractModificationReason.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgContractModificationReason.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgContractModificationReason, False)
        Me.INDlcgContractModificationReason.CustomizationFormText = "Razones de Modificación de Contrato"
        Me.INDlcgContractModificationReason.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlcgContractModificationReason.GroupBordersVisible = False
        Me.INDlcgContractModificationReason.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlcgNewContractModificationReason})
        Me.INDlcgContractModificationReason.Name = "Root"
        Me.INDlcgContractModificationReason.Size = New System.Drawing.Size(1172, 666)
        Me.INDlcgContractModificationReason.Text = "Razones de Modificación de Contrato"
        Me.INDlcgContractModificationReason.TextVisible = False
        '
        'INDlcgNewContractModificationReason
        '
        Me.INDlcgNewContractModificationReason.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgNewContractModificationReason.AppearanceGroup.Options.UseFont = True
        Me.INDlcgNewContractModificationReason.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlcgNewContractModificationReason.AppearanceItemCaption.Options.UseFont = True
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlcgNewContractModificationReason.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlcgNewContractModificationReason, False)
        Me.INDlcgNewContractModificationReason.CustomizationFormText = "Nueva Razón de Modificación de Contrato"
        Me.INDlcgNewContractModificationReason.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlciDescription, Me.INDlciName, Me.INDlciCode, Me.EmptySpaceItem1, Me.INDlciNoveltyType})
        Me.INDlcgNewContractModificationReason.Location = New System.Drawing.Point(0, 0)
        Me.INDlcgNewContractModificationReason.Name = "INDlcgNewContractModificationReason"
        Me.INDlcgNewContractModificationReason.Size = New System.Drawing.Size(1152, 646)
        Me.INDlcgNewContractModificationReason.Text = "Razones de Modificación de Contrato"
        '
        'INDlciDescription
        '
        Me.INDlciDescription.Control = Me.INDmemDescription
        Me.INDlciDescription.CustomizationFormText = "Descripción"
        Me.INDlciDescription.Location = New System.Drawing.Point(0, 106)
        Me.INDlciDescription.MaxSize = New System.Drawing.Size(675, 172)
        Me.INDlciDescription.MinSize = New System.Drawing.Size(585, 172)
        Me.INDlciDescription.Name = "INDlciDescription"
        Me.INDlciDescription.Size = New System.Drawing.Size(1128, 172)
        Me.INDlciDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciDescription.Tag = "Description"
        Me.INDlciDescription.Text = "Descripción"
        Me.INDlciDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciDescription.TextSize = New System.Drawing.Size(270, 29)
        Me.INDlciDescription.TextToControlDistance = 12
        '
        'INDlciName
        '
        Me.INDlciName.Control = Me.INDtxtName
        Me.INDlciName.CustomizationFormText = "Nombre"
        Me.INDlciName.Location = New System.Drawing.Point(0, 53)
        Me.INDlciName.MaxSize = New System.Drawing.Size(675, 53)
        Me.INDlciName.MinSize = New System.Drawing.Size(585, 53)
        Me.INDlciName.Name = "INDlciName"
        Me.INDlciName.Size = New System.Drawing.Size(1128, 53)
        Me.INDlciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciName.Tag = "Name"
        Me.INDlciName.Text = "Nombre"
        Me.INDlciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciName.TextSize = New System.Drawing.Size(270, 29)
        Me.INDlciName.TextToControlDistance = 12
        '
        'INDlciCode
        '
        Me.INDlciCode.Control = Me.INDbteCode
        Me.INDlciCode.CustomizationFormText = "Código"
        Me.INDlciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlciCode.MaxSize = New System.Drawing.Size(505, 53)
        Me.INDlciCode.MinSize = New System.Drawing.Size(505, 53)
        Me.INDlciCode.Name = "INDlciCode"
        Me.INDlciCode.Size = New System.Drawing.Size(1128, 53)
        Me.INDlciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciCode.Tag = "Code"
        Me.INDlciCode.Text = "Código"
        Me.INDlciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciCode.TextSize = New System.Drawing.Size(270, 29)
        Me.INDlciCode.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 331)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(1128, 247)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlciNoveltyType
        '
        Me.INDlciNoveltyType.Control = Me.INDSleNoveltyType
        Me.INDlciNoveltyType.Location = New System.Drawing.Point(0, 278)
        Me.INDlciNoveltyType.MaxSize = New System.Drawing.Size(1012, 53)
        Me.INDlciNoveltyType.MinSize = New System.Drawing.Size(675, 53)
        Me.INDlciNoveltyType.Name = "INDlciNoveltyType"
        Me.INDlciNoveltyType.Size = New System.Drawing.Size(1128, 53)
        Me.INDlciNoveltyType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlciNoveltyType.Text = "Tipo de Novedad"
        Me.INDlciNoveltyType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlciNoveltyType.TextSize = New System.Drawing.Size(270, 29)
        Me.INDlciNoveltyType.TextToControlDistance = 12
        '
        'GridColumn3
        '
        Me.GridColumn3.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn3.Caption = "Selección"
        Me.GridColumn3.FieldName = "Item2"
        Me.GridColumn3.Name = "GridColumn3"
        '
        'GridColumn2
        '
        Me.GridColumn2.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn2.Caption = "Selección"
        Me.GridColumn2.FieldName = "Item2"
        Me.GridColumn2.Name = "GridColumn2"
        '
        'GridColumn1
        '
        Me.GridColumn1.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn1.Caption = "Selección"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'GridColumn6
        '
        Me.GridColumn6.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn6.Caption = "Selección"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        '
        'FrmContractModificationReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1176, 820)
        Me.IconOptions.ShowIcon = False
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.Name = "FrmContractModificationReason"
        Me.Opacity = 1.0R
        Me.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.Tag = "580"
        Me.Text = "Razones de Modificación de Contrato"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtlContractModificationReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtlContractModificationReason.ResumeLayout(False)
        CType(Me.INDSleNoveltyType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleNoveltyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgContractModificationReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlcgNewContractModificationReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlciNoveltyType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCtlContractModificationReason As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlcgContractModificationReason As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDmemDescription As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlciDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDlcgNewContractModificationReason As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoTextEdit11 As IndigoTextEdit
    Friend WithEvents INDSleNoveltyType As CtrYesNo
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlciNoveltyType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
End Class
