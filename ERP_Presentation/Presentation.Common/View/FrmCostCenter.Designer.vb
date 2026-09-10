<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCostCenter
    Inherits Presentation.Controls.FormBase

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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCostCenter))
        Me.INDLyCostCenter = New DevExpress.XtraLayout.LayoutControl()
        Me.INDNameCostCenter = New DevExpress.XtraEditors.TextEdit()
        Me.INDBteCodeCostCenter = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDLcgBase = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDGrCostCenter = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDSleBranchOffice = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.INDLciBranchOffice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDGvBranchOffice = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDColBranchOffice_Code = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColBranchOffice_Name = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLyCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDLyCostCenter.SuspendLayout()
        CType(Me.INDNameCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDBteCodeCostCenter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGrCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleBranchOffice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDGvBranchOffice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDLyCostCenter)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDLyCostCenter
        '
        Me.INDLyCostCenter.AllowCustomization = False
        Me.INDLyCostCenter.Controls.Add(Me.INDSleBranchOffice)
        Me.INDLyCostCenter.Controls.Add(Me.INDNameCostCenter)
        Me.INDLyCostCenter.Controls.Add(Me.INDBteCodeCostCenter)
        resources.ApplyResources(Me.INDLyCostCenter, "INDLyCostCenter")
        Me.LayoutControls.SetIsCustomizable(Me.INDLyCostCenter, False)
        Me.INDLyCostCenter.Name = "INDLyCostCenter"
        Me.INDLyCostCenter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(622, 269, 477, 350)
        Me.INDLyCostCenter.OptionsFocus.EnableAutoTabOrder = False
        Me.INDLyCostCenter.Root = Me.INDLcgBase
        '
        'INDNameCostCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDNameCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDNameCostCenter, True)
        Me.INDNameCostCenter.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDNameCostCenter, "INDNameCostCenter")
        Me.IndigoTextEdit1.SetMascara(Me.INDNameCostCenter, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDNameCostCenter.Name = "INDNameCostCenter"
        Me.INDNameCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDNameCostCenter.Properties.Appearance.Font = CType(resources.GetObject("INDNameCostCenter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDNameCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDNameCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDNameCostCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDNameCostCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDNameCostCenter.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDNameCostCenter.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDNameCostCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDNameCostCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDNameCostCenter.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDNameCostCenter.Properties.MaxLength = 50
        Me.INDNameCostCenter.StyleController = Me.INDLyCostCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDNameCostCenter, 0)
        '
        'INDBteCodeCostCenter
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDBteCodeCostCenter, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDBteCodeCostCenter, True)
        resources.ApplyResources(Me.INDBteCodeCostCenter, "INDBteCodeCostCenter")
        Me.IndigoTextEdit1.SetMascara(Me.INDBteCodeCostCenter, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDBteCodeCostCenter.Name = "INDBteCodeCostCenter"
        Me.INDBteCodeCostCenter.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDBteCodeCostCenter.Properties.Appearance.Font = CType(resources.GetObject("INDBteCodeCostCenter.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDBteCodeCostCenter.Properties.Appearance.Options.UseBackColor = True
        Me.INDBteCodeCostCenter.Properties.Appearance.Options.UseFont = True
        Me.INDBteCodeCostCenter.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDBteCodeCostCenter.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDBteCodeCostCenter.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDBteCodeCostCenter.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDBteCodeCostCenter.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDBteCodeCostCenter.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDBteCodeCostCenter.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Common.My.Resources.Resources.BuscarMetro
        Me.INDBteCodeCostCenter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDBteCodeCostCenter.Properties.Buttons1"), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons2"), Integer), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons5"), Boolean), EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, resources.GetString("INDBteCodeCostCenter.Properties.Buttons6"), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons7"), Object), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons8"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDBteCodeCostCenter.Properties.Buttons9"), DevExpress.Utils.ToolTipAnchor))})
        Me.INDBteCodeCostCenter.Properties.Mask.EditMask = resources.GetString("INDBteCodeCostCenter.Properties.Mask.EditMask")
        Me.INDBteCodeCostCenter.Properties.Mask.MaskType = CType(resources.GetObject("INDBteCodeCostCenter.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDBteCodeCostCenter.Properties.MaxLength = 20
        Me.INDBteCodeCostCenter.StyleController = Me.INDLyCostCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDBteCodeCostCenter, 0)
        '
        'INDLcgBase
        '
        resources.ApplyResources(Me.INDLcgBase, "INDLcgBase")
        Me.INDLcgBase.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDLcgBase.GroupBordersVisible = False
        Me.INDLcgBase.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDGrCostCenter})
        Me.INDLcgBase.Name = "Root"
        Me.INDLcgBase.Size = New System.Drawing.Size(616, 360)
        Me.INDLcgBase.TextVisible = False
        '
        'INDGrCostCenter
        '
        Me.INDGrCostCenter.AppearanceGroup.Font = CType(resources.GetObject("INDGrCostCenter.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDGrCostCenter.AppearanceGroup.Options.UseFont = True
        resources.ApplyResources(Me.INDGrCostCenter, "INDGrCostCenter")
        Me.INDGrCostCenter.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciCode, Me.INDLciName, Me.INDLciBranchOffice})
        Me.INDGrCostCenter.Location = New System.Drawing.Point(0, 0)
        Me.INDGrCostCenter.Name = "INDGrCostCenter"
        Me.INDGrCostCenter.Size = New System.Drawing.Size(596, 340)
        '
        'INDLciCode
        '
        Me.INDLciCode.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemCode.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLciCode.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciCode.Control = Me.INDBteCodeCostCenter
        resources.ApplyResources(Me.INDLciCode, "INDLciCode")
        Me.INDLciCode.Location = New System.Drawing.Point(0, 0)
        Me.INDLciCode.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLciCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLciCode.Name = "INDLciCode"
        Me.INDLciCode.ShowInCustomizationForm = False
        Me.INDLciCode.Size = New System.Drawing.Size(572, 36)
        Me.INDLciCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciCode.Tag = "Code"
        Me.INDLciCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciCode.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLciCode.TextToControlDistance = 12
        '
        'INDLciName
        '
        Me.INDLciName.AppearanceItemCaption.Font = CType(resources.GetObject("INDLyItemName.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLciName.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciName.Control = Me.INDNameCostCenter
        resources.ApplyResources(Me.INDLciName, "INDLciName")
        Me.INDLciName.Location = New System.Drawing.Point(0, 36)
        Me.INDLciName.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLciName.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLciName.Name = "INDLciName"
        Me.INDLciName.Size = New System.Drawing.Size(572, 36)
        Me.INDLciName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciName.Tag = "Name"
        Me.INDLciName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciName.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLciName.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.Appearance.BackColor = System.Drawing.Color.White
        Me.CtrNavigationControl1.Appearance.Image = CType(resources.GetObject("CtrNavigationControl1.Appearance.Image"), System.Drawing.Image)
        Me.CtrNavigationControl1.Appearance.Options.UseBackColor = True
        Me.CtrNavigationControl1.Appearance.Options.UseImage = True
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDLyCostCenter
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDSleBranchOffice
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleBranchOffice, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleBranchOffice, True)
        Me.INDSleBranchOffice.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDSleBranchOffice, "INDSleBranchOffice")
        Me.IndigoTextEdit1.SetMascara(Me.INDSleBranchOffice, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleBranchOffice.Name = "INDSleBranchOffice"
        Me.INDSleBranchOffice.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleBranchOffice.Properties.Appearance.Font = CType(resources.GetObject("INDSleBranchOffice.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleBranchOffice.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleBranchOffice.Properties.Appearance.Options.UseFont = True
        Me.INDSleBranchOffice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("SearchLookUpEdit1.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSleBranchOffice.Properties.DisplayMember = "CodeName"
        Me.INDSleBranchOffice.Properties.NullText = resources.GetString("SearchLookUpEdit1.Properties.NullText")
        Me.INDSleBranchOffice.Properties.PopupView = Me.INDGvBranchOffice
        Me.INDSleBranchOffice.Properties.ValueMember = "Id"
        Me.INDSleBranchOffice.StyleController = Me.INDLyCostCenter
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleBranchOffice, 0)
        '
        'INDLciBranchOffice
        '
        Me.INDLciBranchOffice.AppearanceItemCaption.Font = CType(resources.GetObject("INDLciBranchOffice.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDLciBranchOffice.AppearanceItemCaption.Options.UseFont = True
        Me.INDLciBranchOffice.Control = Me.INDSleBranchOffice
        Me.INDLciBranchOffice.Location = New System.Drawing.Point(0, 72)
        Me.INDLciBranchOffice.MaxSize = New System.Drawing.Size(410, 36)
        Me.INDLciBranchOffice.MinSize = New System.Drawing.Size(300, 36)
        Me.INDLciBranchOffice.Name = "INDLciBranchOffice"
        Me.INDLciBranchOffice.Size = New System.Drawing.Size(572, 209)
        Me.INDLciBranchOffice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciBranchOffice.Tag = "BranchOffice"
        resources.ApplyResources(Me.INDLciBranchOffice, "INDLciBranchOffice")
        Me.INDLciBranchOffice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciBranchOffice.TextSize = New System.Drawing.Size(100, 21)
        Me.INDLciBranchOffice.TextToControlDistance = 12
        '
        'INDGvBranchOffice
        '
        Me.INDGvBranchOffice.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDColBranchOffice_Code, Me.INDColBranchOffice_Name})
        Me.INDGvBranchOffice.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDGvBranchOffice.Name = "INDGvBranchOffice"
        Me.INDGvBranchOffice.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDGvBranchOffice.OptionsView.ShowGroupPanel = False
        '
        'INDColBranchOffice_Code
        '
        resources.ApplyResources(Me.INDColBranchOffice_Code, "INDColBranchOffice_Code")
        Me.INDColBranchOffice_Code.FieldName = "Codigo"
        Me.INDColBranchOffice_Code.Name = "INDColBranchOffice_Code"
        Me.INDColBranchOffice_Code.OptionsColumn.AllowEdit = False
        Me.INDColBranchOffice_Code.OptionsColumn.AllowFocus = False
        '
        'INDColBranchOffice_Name
        '
        resources.ApplyResources(Me.INDColBranchOffice_Name, "INDColBranchOffice_Name")
        Me.INDColBranchOffice_Name.FieldName = "Descripcion"
        Me.INDColBranchOffice_Name.Name = "INDColBranchOffice_Name"
        Me.INDColBranchOffice_Name.OptionsColumn.AllowEdit = False
        Me.INDColBranchOffice_Name.OptionsColumn.AllowFocus = False
        '
        'FrmCostCenter
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCostCenter"
        Me.Opacity = 1.0R
        Me.Tag = "517"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLyCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDLyCostCenter.ResumeLayout(False)
        CType(Me.INDNameCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDBteCodeCostCenter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcgBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGrCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleBranchOffice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDGvBranchOffice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDLyCostCenter As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDNameCostCenter As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDBteCodeCostCenter As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDLcgBase As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDGrCostCenter As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLciCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDSleBranchOffice As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents INDGvBranchOffice As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciBranchOffice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDColBranchOffice_Code As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColBranchOffice_Name As DevExpress.XtraGrid.Columns.GridColumn
End Class
