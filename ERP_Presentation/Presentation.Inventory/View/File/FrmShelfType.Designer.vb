#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmShelfType
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDcncShelfType = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlycBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtLarge = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtWide = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPartitionXDeep = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPartitions = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtLocationXPartition = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtDeep = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtAvailableLocations = New DevExpress.XtraEditors.TextEdit()
        Me.INDlycBaseUnitDoseType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrShelfType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrDimensions = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyWide = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyLarge = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyPartitionXDeep = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyPartitions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyLocationXPartition = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyDeep = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyAvailableLocations = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcncShelfType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycBase.SuspendLayout()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtLarge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtWide.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPartitionXDeep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPartitions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtLocationXPartition.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDeep.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtAvailableLocations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrShelfType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrDimensions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyWide, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyLarge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPartitionXDeep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPartitions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyLocationXPartition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyDeep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyAvailableLocations, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDcncShelfType)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 594)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 130)
        '
        'INDcncShelfType
        '
        Me.INDcncShelfType.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDcncShelfType.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDcncShelfType.LayoutControl = Me.INDlycBase
        Me.INDcncShelfType.Location = New System.Drawing.Point(2, 7)
        Me.INDcncShelfType.Margin = New System.Windows.Forms.Padding(0)
        Me.INDcncShelfType.Name = "INDcncShelfType"
        Me.INDcncShelfType.Size = New System.Drawing.Size(200, 585)
        Me.INDcncShelfType.TabIndex = 2
        Me.INDcncShelfType.UseDisabledStatePainter = False
        '
        'INDlycBase
        '
        Me.INDlycBase.Controls.Add(Me.INDtxtDescription)
        Me.INDlycBase.Controls.Add(Me.INDbtnCode)
        Me.INDlycBase.Controls.Add(Me.INDtxtLarge)
        Me.INDlycBase.Controls.Add(Me.INDtxtWide)
        Me.INDlycBase.Controls.Add(Me.INDtxtPartitionXDeep)
        Me.INDlycBase.Controls.Add(Me.INDtxtPartitions)
        Me.INDlycBase.Controls.Add(Me.INDtxtLocationXPartition)
        Me.INDlycBase.Controls.Add(Me.INDtxtDeep)
        Me.INDlycBase.Controls.Add(Me.INDtxtAvailableLocations)
        Me.INDlycBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycBase.Location = New System.Drawing.Point(202, 7)
        Me.INDlycBase.Name = "INDlycBase"
        Me.INDlycBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(434, 310, 1201, 569)
        Me.INDlycBase.Root = Me.INDlycBaseUnitDoseType
        Me.INDlycBase.Size = New System.Drawing.Size(804, 585)
        Me.INDlycBase.TabIndex = 3
        Me.INDlycBase.Text = "LayoutControl1"
        '
        'INDtxtDescription
        '
        Me.INDtxtDescription.Location = New System.Drawing.Point(24, 137)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDescription.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtDescription.Properties.MaxLength = 100
        Me.INDtxtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDescription.StyleController = Me.INDlycBase
        Me.INDtxtDescription.TabIndex = 2
        '
        'INDbtnCode
        '
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 73)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Inventory.My.Resources.Resources.BuscarMetro
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbtnCode.Properties.Mask.EditMask = "[0-9]+"
        Me.INDbtnCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlycBase
        Me.INDbtnCode.TabIndex = 1
        '
        'INDtxtLarge
        '
        Me.INDtxtLarge.Location = New System.Drawing.Point(438, 73)
        Me.INDtxtLarge.Name = "INDtxtLarge"
        Me.INDtxtLarge.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtLarge.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtLarge.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLarge.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLarge.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtLarge.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtLarge.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtLarge.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLarge.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtLarge.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtLarge.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtLarge.Properties.MaxLength = 6
        Me.INDtxtLarge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtLarge.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtLarge.StyleController = Me.INDlycBase
        Me.INDtxtLarge.TabIndex = 3
        '
        'INDtxtWide
        '
        Me.INDtxtWide.Location = New System.Drawing.Point(438, 137)
        Me.INDtxtWide.Name = "INDtxtWide"
        Me.INDtxtWide.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtWide.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtWide.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtWide.Properties.Appearance.Options.UseFont = True
        Me.INDtxtWide.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtWide.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtWide.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtWide.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtWide.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtWide.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtWide.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtWide.Properties.MaxLength = 6
        Me.INDtxtWide.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtWide.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtWide.StyleController = Me.INDlycBase
        Me.INDtxtWide.TabIndex = 4
        '
        'INDtxtPartitionXDeep
        '
        Me.INDtxtPartitionXDeep.Location = New System.Drawing.Point(438, 265)
        Me.INDtxtPartitionXDeep.Name = "INDtxtPartitionXDeep"
        Me.INDtxtPartitionXDeep.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPartitionXDeep.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPartitionXDeep.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPartitionXDeep.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPartitionXDeep.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtPartitionXDeep.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtPartitionXDeep.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPartitionXDeep.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPartitionXDeep.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtPartitionXDeep.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtPartitionXDeep.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtPartitionXDeep.Properties.MaxLength = 6
        Me.INDtxtPartitionXDeep.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtPartitionXDeep.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPartitionXDeep.StyleController = Me.INDlycBase
        Me.INDtxtPartitionXDeep.TabIndex = 7
        '
        'INDtxtPartitions
        '
        Me.INDtxtPartitions.Location = New System.Drawing.Point(438, 329)
        Me.INDtxtPartitions.Name = "INDtxtPartitions"
        Me.INDtxtPartitions.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPartitions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPartitions.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPartitions.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPartitions.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtPartitions.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtPartitions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPartitions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPartitions.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtPartitions.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtPartitions.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtPartitions.Properties.MaxLength = 6
        Me.INDtxtPartitions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtPartitions.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPartitions.StyleController = Me.INDlycBase
        Me.INDtxtPartitions.TabIndex = 8
        '
        'INDtxtLocationXPartition
        '
        Me.INDtxtLocationXPartition.Location = New System.Drawing.Point(438, 393)
        Me.INDtxtLocationXPartition.Name = "INDtxtLocationXPartition"
        Me.INDtxtLocationXPartition.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtLocationXPartition.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtLocationXPartition.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtLocationXPartition.Properties.Appearance.Options.UseFont = True
        Me.INDtxtLocationXPartition.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtLocationXPartition.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtLocationXPartition.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtLocationXPartition.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtLocationXPartition.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtLocationXPartition.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtLocationXPartition.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtLocationXPartition.Properties.MaxLength = 6
        Me.INDtxtLocationXPartition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtLocationXPartition.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtLocationXPartition.StyleController = Me.INDlycBase
        Me.INDtxtLocationXPartition.TabIndex = 9
        '
        'INDtxtDeep
        '
        Me.INDtxtDeep.Location = New System.Drawing.Point(438, 201)
        Me.INDtxtDeep.Name = "INDtxtDeep"
        Me.INDtxtDeep.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtDeep.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDeep.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDeep.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDeep.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtDeep.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtDeep.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDeep.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDeep.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtDeep.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtDeep.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtDeep.Properties.MaxLength = 6
        Me.INDtxtDeep.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtDeep.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDeep.StyleController = Me.INDlycBase
        Me.INDtxtDeep.TabIndex = 4
        '
        'INDtxtAvailableLocations
        '
        Me.INDtxtAvailableLocations.Enabled = False
        Me.INDtxtAvailableLocations.Location = New System.Drawing.Point(438, 457)
        Me.INDtxtAvailableLocations.Name = "INDtxtAvailableLocations"
        Me.INDtxtAvailableLocations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtAvailableLocations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAvailableLocations.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAvailableLocations.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAvailableLocations.Properties.Appearance.Options.UseTextOptions = True
        Me.INDtxtAvailableLocations.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDtxtAvailableLocations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAvailableLocations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtAvailableLocations.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtAvailableLocations.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDtxtAvailableLocations.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.INDtxtAvailableLocations.Properties.MaxLength = 20
        Me.INDtxtAvailableLocations.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.INDtxtAvailableLocations.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtAvailableLocations.StyleController = Me.INDlycBase
        Me.INDtxtAvailableLocations.TabIndex = 10
        '
        'INDlycBaseUnitDoseType
        '
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceGroup.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseUnitDoseType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseUnitDoseType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlycBaseUnitDoseType.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycBaseUnitDoseType.GroupBordersVisible = False
        Me.INDlycBaseUnitDoseType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrShelfType, Me.INDlyGrDimensions})
        Me.INDlycBaseUnitDoseType.Name = "Root"
        Me.INDlycBaseUnitDoseType.Size = New System.Drawing.Size(848, 568)
        Me.INDlycBaseUnitDoseType.TextVisible = False
        '
        'INDlyGrShelfType
        '
        Me.INDlyGrShelfType.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrShelfType.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrShelfType.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrShelfType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrShelfType.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrShelfType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrShelfType.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrShelfType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrShelfType.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrShelfType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrShelfType.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrShelfType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrShelfType.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrShelfType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlyGrShelfType.CustomizationFormText = "Datos Principales"
        Me.INDlyGrShelfType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemDescription})
        Me.INDlyGrShelfType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrShelfType.Name = "INDlyGrShelfType"
        Me.INDlyGrShelfType.Size = New System.Drawing.Size(414, 548)
        Me.INDlyGrShelfType.Text = "Datos Principales"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.Control = Me.INDtxtDescription
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.Size = New System.Drawing.Size(390, 431)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Descripción"
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyGrDimensions
        '
        Me.INDlyGrDimensions.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrDimensions.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrDimensions.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrDimensions.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrDimensions.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDimensions.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrDimensions.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrDimensions.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrDimensions.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDimensions.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrDimensions.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDimensions.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrDimensions.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrDimensions.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.INDlyGrDimensions.CustomizationFormText = "Dimensiones"
        Me.INDlyGrDimensions.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyWide, Me.INDlyLarge, Me.INDlyPartitionXDeep, Me.INDlyPartitions, Me.INDlyLocationXPartition, Me.INDlyDeep, Me.INDlyAvailableLocations})
        Me.INDlyGrDimensions.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGrDimensions.Name = "INDlyGrDimensions"
        Me.INDlyGrDimensions.Size = New System.Drawing.Size(414, 548)
        Me.INDlyGrDimensions.Text = "Dimensiones"
        '
        'INDlyWide
        '
        Me.INDlyWide.Control = Me.INDtxtWide
        Me.INDlyWide.CustomizationFormText = "Ancho (cm)"
        Me.INDlyWide.Location = New System.Drawing.Point(0, 64)
        Me.INDlyWide.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyWide.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyWide.Name = "INDlyWide"
        Me.INDlyWide.Size = New System.Drawing.Size(390, 64)
        Me.INDlyWide.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyWide.Text = "Ancho (cm)"
        Me.INDlyWide.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyWide.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyLarge
        '
        Me.INDlyLarge.Control = Me.INDtxtLarge
        Me.INDlyLarge.CustomizationFormText = "Alto (cm)"
        Me.INDlyLarge.Location = New System.Drawing.Point(0, 0)
        Me.INDlyLarge.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyLarge.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyLarge.Name = "INDlyLarge"
        Me.INDlyLarge.Size = New System.Drawing.Size(390, 64)
        Me.INDlyLarge.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyLarge.Text = "Alto (cm)"
        Me.INDlyLarge.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyLarge.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyPartitionXDeep
        '
        Me.INDlyPartitionXDeep.Control = Me.INDtxtPartitionXDeep
        Me.INDlyPartitionXDeep.CustomizationFormText = "Cantidad de Divisiones por Profundidad"
        Me.INDlyPartitionXDeep.Location = New System.Drawing.Point(0, 192)
        Me.INDlyPartitionXDeep.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyPartitionXDeep.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyPartitionXDeep.Name = "INDlyPartitionXDeep"
        Me.INDlyPartitionXDeep.Size = New System.Drawing.Size(390, 64)
        Me.INDlyPartitionXDeep.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyPartitionXDeep.Text = "Cantidad de Divisiones por Profundidad"
        Me.INDlyPartitionXDeep.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyPartitionXDeep.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyPartitions
        '
        Me.INDlyPartitions.Control = Me.INDtxtPartitions
        Me.INDlyPartitions.CustomizationFormText = "Cantidad de Divisiones a lo Alto"
        Me.INDlyPartitions.Location = New System.Drawing.Point(0, 256)
        Me.INDlyPartitions.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyPartitions.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyPartitions.Name = "INDlyPartitions"
        Me.INDlyPartitions.Size = New System.Drawing.Size(390, 64)
        Me.INDlyPartitions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyPartitions.Text = "Cantidad de Divisiones a lo Alto"
        Me.INDlyPartitions.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyPartitions.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyLocationXPartition
        '
        Me.INDlyLocationXPartition.Control = Me.INDtxtLocationXPartition
        Me.INDlyLocationXPartition.CustomizationFormText = "Cantidad de Divisiones a lo Ancho"
        Me.INDlyLocationXPartition.Location = New System.Drawing.Point(0, 320)
        Me.INDlyLocationXPartition.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyLocationXPartition.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyLocationXPartition.Name = "INDlyLocationXPartition"
        Me.INDlyLocationXPartition.Size = New System.Drawing.Size(390, 64)
        Me.INDlyLocationXPartition.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyLocationXPartition.Text = "Cantidad de Divisiones a lo Ancho"
        Me.INDlyLocationXPartition.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyLocationXPartition.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyDeep
        '
        Me.INDlyDeep.Control = Me.INDtxtDeep
        Me.INDlyDeep.CustomizationFormText = "Profundidad (cm)"
        Me.INDlyDeep.Location = New System.Drawing.Point(0, 128)
        Me.INDlyDeep.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyDeep.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyDeep.Name = "INDlyDeep"
        Me.INDlyDeep.Size = New System.Drawing.Size(390, 64)
        Me.INDlyDeep.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyDeep.Text = "Profundidad (cm)"
        Me.INDlyDeep.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyDeep.TextSize = New System.Drawing.Size(255, 17)
        '
        'INDlyAvailableLocations
        '
        Me.INDlyAvailableLocations.Control = Me.INDtxtAvailableLocations
        Me.INDlyAvailableLocations.CustomizationFormText = "Localizaciones Disponibles"
        Me.INDlyAvailableLocations.Location = New System.Drawing.Point(0, 384)
        Me.INDlyAvailableLocations.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyAvailableLocations.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyAvailableLocations.Name = "INDlyAvailableLocations"
        Me.INDlyAvailableLocations.Size = New System.Drawing.Size(390, 111)
        Me.INDlyAvailableLocations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyAvailableLocations.Text = "Localizaciones Disponibles"
        Me.INDlyAvailableLocations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyAvailableLocations.TextSize = New System.Drawing.Size(255, 17)
        '
        'FrmShelfType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmShelfType"
        Me.Opacity = 1.0R
        Me.Tag = "2068"
        Me.Text = "Configuración Tipo de Estantes"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcncShelfType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycBase.ResumeLayout(False)
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtLarge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtWide.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPartitionXDeep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPartitions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtLocationXPartition.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDeep.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtAvailableLocations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBaseUnitDoseType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrShelfType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrDimensions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyWide, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyLarge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPartitionXDeep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPartitions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyLocationXPartition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyDeep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyAvailableLocations, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDcncShelfType As CtrNavigationControlPanel
	Friend WithEvents INDlycBase As DevExpress.XtraLayout.LayoutControl
	Friend WithEvents INDlycBaseUnitDoseType As DevExpress.XtraLayout.LayoutControlGroup
	Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
	Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyGrShelfType As DevExpress.XtraLayout.LayoutControlGroup
	Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDlyGrDimensions As DevExpress.XtraLayout.LayoutControlGroup
	Friend WithEvents INDtxtLarge As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyLarge As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDtxtWide As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyWide As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDtxtPartitions As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDtxtLocationXPartition As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyPartitions As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDlyLocationXPartition As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDtxtPartitionXDeep As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyPartitionXDeep As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDtxtDeep As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyDeep As DevExpress.XtraLayout.LayoutControlItem
	Friend WithEvents INDtxtAvailableLocations As DevExpress.XtraEditors.TextEdit
	Friend WithEvents INDlyAvailableLocations As DevExpress.XtraLayout.LayoutControlItem
End Class
