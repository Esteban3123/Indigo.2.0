#Region "Imports"
Imports Presentation.Controls
#End Region

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmTurn
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDcncTurn = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlycBase = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTimeEditDeliveryHasta = New DevExpress.XtraEditors.TimeEdit()
        Me.INDTimeEditDeliveryDesde = New DevExpress.XtraEditors.TimeEdit()
        Me.INDTimeEditWorkHasta = New DevExpress.XtraEditors.TimeEdit()
        Me.INDTimeEditWorkDesde = New DevExpress.XtraEditors.TimeEdit()
        Me.INDtxtDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDrgWeekFraction = New DevExpress.XtraEditors.RadioGroup()
        Me.INDlycBaseTurn = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrTurn = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrTimeTable = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrTimeTableWork = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemWorkTimeDesde = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemWorkTimeHasta = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyGrTimeTableDelivery = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemDeliveryTimeDesde = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDeliveryTimeHasta = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemWeekFraction = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoRadioGroup1 = New Presentation.Controls.IndigoRadioGroup(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDcncTurn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycBase.SuspendLayout()
        CType(Me.INDTimeEditDeliveryHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTimeEditDeliveryDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTimeEditWorkHasta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTimeEditWorkDesde.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrgWeekFraction.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycBaseTurn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrTurn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrTimeTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrTimeTableWork, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWorkTimeDesde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWorkTimeHasta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrTimeTableDelivery, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDeliveryTimeDesde, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDeliveryTimeHasta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemWeekFraction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlycBase)
        Me.INDPanelControlBase.Controls.Add(Me.INDcncTurn)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1008, 607)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1008, 98)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(1008, 98)
        '
        'INDcncTurn
        '
        Me.INDcncTurn.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDcncTurn.Dock = System.Windows.Forms.DockStyle.Left
        Me.INDcncTurn.LayoutControl = Me.INDlycBase
        Me.INDcncTurn.Location = New System.Drawing.Point(2, 7)
        Me.INDcncTurn.Margin = New System.Windows.Forms.Padding(0)
        Me.INDcncTurn.Name = "INDcncTurn"
        Me.INDcncTurn.Size = New System.Drawing.Size(200, 598)
        Me.INDcncTurn.TabIndex = 2
        Me.INDcncTurn.UseDisabledStatePainter = False
        '
        'INDlycBase
        '
        Me.INDlycBase.Controls.Add(Me.INDTimeEditDeliveryHasta)
        Me.INDlycBase.Controls.Add(Me.INDTimeEditDeliveryDesde)
        Me.INDlycBase.Controls.Add(Me.INDTimeEditWorkHasta)
        Me.INDlycBase.Controls.Add(Me.INDTimeEditWorkDesde)
        Me.INDlycBase.Controls.Add(Me.INDtxtDescription)
        Me.INDlycBase.Controls.Add(Me.INDtxtName)
        Me.INDlycBase.Controls.Add(Me.INDbtnCode)
        Me.INDlycBase.Controls.Add(Me.INDrgWeekFraction)
        Me.INDlycBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlycBase.Location = New System.Drawing.Point(202, 7)
        Me.INDlycBase.Name = "INDlycBase"
        Me.INDlycBase.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(434, 310, 1201, 569)
        Me.INDlycBase.Root = Me.INDlycBaseTurn
        Me.INDlycBase.Size = New System.Drawing.Size(804, 598)
        Me.INDlycBase.TabIndex = 3
        Me.INDlycBase.Text = "LayoutControl1"
        '
        'INDTimeEditDeliveryHasta
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTimeEditDeliveryHasta, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTimeEditDeliveryHasta, True)
        Me.INDTimeEditDeliveryHasta.EditValue = New Date(2019, 5, 6, 0, 0, 0, 0)
        Me.INDTimeEditDeliveryHasta.EnterMoveNextControl = True
        Me.INDTimeEditDeliveryHasta.Location = New System.Drawing.Point(682, 263)
        Me.IndigoTextEdit1.SetMascara(Me.INDTimeEditDeliveryHasta, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTimeEditDeliveryHasta.Name = "INDTimeEditDeliveryHasta"
        Me.INDTimeEditDeliveryHasta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTimeEditDeliveryHasta.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditDeliveryHasta.Properties.Appearance.Options.UseBackColor = True
        Me.INDTimeEditDeliveryHasta.Properties.Appearance.Options.UseFont = True
        Me.INDTimeEditDeliveryHasta.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditDeliveryHasta.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTimeEditDeliveryHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTimeEditDeliveryHasta.Properties.Mask.EditMask = "HH:mm"
        Me.INDTimeEditDeliveryHasta.Size = New System.Drawing.Size(135, 28)
        Me.INDTimeEditDeliveryHasta.StyleController = Me.INDlycBase
        Me.INDTimeEditDeliveryHasta.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTimeEditDeliveryHasta, 0)
        '
        'INDTimeEditDeliveryDesde
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTimeEditDeliveryDesde, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTimeEditDeliveryDesde, True)
        Me.INDTimeEditDeliveryDesde.EditValue = New Date(2019, 5, 6, 0, 0, 0, 0)
        Me.INDTimeEditDeliveryDesde.EnterMoveNextControl = True
        Me.INDTimeEditDeliveryDesde.Location = New System.Drawing.Point(499, 263)
        Me.IndigoTextEdit1.SetMascara(Me.INDTimeEditDeliveryDesde, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTimeEditDeliveryDesde.Name = "INDTimeEditDeliveryDesde"
        Me.INDTimeEditDeliveryDesde.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTimeEditDeliveryDesde.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditDeliveryDesde.Properties.Appearance.Options.UseBackColor = True
        Me.INDTimeEditDeliveryDesde.Properties.Appearance.Options.UseFont = True
        Me.INDTimeEditDeliveryDesde.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditDeliveryDesde.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTimeEditDeliveryDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTimeEditDeliveryDesde.Properties.Mask.EditMask = "HH:mm"
        Me.INDTimeEditDeliveryDesde.Size = New System.Drawing.Size(135, 28)
        Me.INDTimeEditDeliveryDesde.StyleController = Me.INDlycBase
        Me.INDTimeEditDeliveryDesde.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTimeEditDeliveryDesde, 0)
        '
        'INDTimeEditWorkHasta
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTimeEditWorkHasta, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTimeEditWorkHasta, True)
        Me.INDTimeEditWorkHasta.EditValue = New Date(2019, 5, 6, 0, 0, 0, 0)
        Me.INDTimeEditWorkHasta.EnterMoveNextControl = True
        Me.INDTimeEditWorkHasta.Location = New System.Drawing.Point(682, 170)
        Me.IndigoTextEdit1.SetMascara(Me.INDTimeEditWorkHasta, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTimeEditWorkHasta.Name = "INDTimeEditWorkHasta"
        Me.INDTimeEditWorkHasta.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTimeEditWorkHasta.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditWorkHasta.Properties.Appearance.Options.UseBackColor = True
        Me.INDTimeEditWorkHasta.Properties.Appearance.Options.UseFont = True
        Me.INDTimeEditWorkHasta.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditWorkHasta.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTimeEditWorkHasta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTimeEditWorkHasta.Properties.Mask.EditMask = "HH:mm"
        Me.INDTimeEditWorkHasta.Size = New System.Drawing.Size(135, 28)
        Me.INDTimeEditWorkHasta.StyleController = Me.INDlycBase
        Me.INDTimeEditWorkHasta.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTimeEditWorkHasta, 0)
        '
        'INDTimeEditWorkDesde
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTimeEditWorkDesde, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTimeEditWorkDesde, True)
        Me.INDTimeEditWorkDesde.EditValue = New Date(2019, 5, 6, 0, 0, 0, 0)
        Me.INDTimeEditWorkDesde.EnterMoveNextControl = True
        Me.INDTimeEditWorkDesde.Location = New System.Drawing.Point(499, 170)
        Me.IndigoTextEdit1.SetMascara(Me.INDTimeEditWorkDesde, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTimeEditWorkDesde.Name = "INDTimeEditWorkDesde"
        Me.INDTimeEditWorkDesde.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTimeEditWorkDesde.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditWorkDesde.Properties.Appearance.Options.UseBackColor = True
        Me.INDTimeEditWorkDesde.Properties.Appearance.Options.UseFont = True
        Me.INDTimeEditWorkDesde.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDTimeEditWorkDesde.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTimeEditWorkDesde.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDTimeEditWorkDesde.Properties.Mask.EditMask = "HH:mm"
        Me.INDTimeEditWorkDesde.Size = New System.Drawing.Size(135, 28)
        Me.INDTimeEditWorkDesde.StyleController = Me.INDlycBase
        Me.INDTimeEditWorkDesde.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTimeEditWorkDesde, 0)
        '
        'INDtxtDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDescription, True)
        Me.INDtxtDescription.EnterMoveNextControl = True
        Me.INDtxtDescription.Location = New System.Drawing.Point(24, 209)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDescription, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtDescription.Name = "INDtxtDescription"
        Me.INDtxtDescription.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDescription.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtDescription.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtDescription.StyleController = Me.INDlycBase
        Me.INDtxtDescription.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDescription, 0)
        Me.INDtxtDescription.ToolTip = "Este Campo es Necesario"
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(24, 143)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!ñÑ""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDtxtName.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtName.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtName.StyleController = Me.INDlycBase
        Me.INDtxtName.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.MixingStation.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.INDbtnCode.Properties.Mask.EditMask = "[0-9]+"
        Me.INDbtnCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDbtnCode.Size = New System.Drawing.Size(386, 28)
        Me.INDbtnCode.StyleController = Me.INDlycBase
        Me.INDbtnCode.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDrgWeekFraction
        '
        Me.IndigoRadioGroup1.SetCampoObligatorio(Me.INDrgWeekFraction, True)
        Me.INDrgWeekFraction.EnterMoveNextControl = True
        Me.INDrgWeekFraction.Location = New System.Drawing.Point(438, 85)
        Me.INDrgWeekFraction.Name = "INDrgWeekFraction"
        Me.INDrgWeekFraction.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDrgWeekFraction.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgWeekFraction.Properties.Appearance.Options.UseBackColor = True
        Me.INDrgWeekFraction.Properties.Appearance.Options.UseFont = True
        Me.INDrgWeekFraction.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDrgWeekFraction.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDrgWeekFraction.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDrgWeekFraction.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDrgWeekFraction.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDrgWeekFraction.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDrgWeekFraction.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Lunes a Viernes"), New DevExpress.XtraEditors.Controls.RadioGroupItem(2, "Sabados y Domingos")})
        Me.INDrgWeekFraction.Size = New System.Drawing.Size(386, 34)
        Me.INDrgWeekFraction.StyleController = Me.INDlycBase
        Me.INDrgWeekFraction.TabIndex = 12
        Me.INDrgWeekFraction.ToolTip = "Este Campo es Necesario"
        '
        'INDlycBaseTurn
        '
        Me.INDlycBaseTurn.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseTurn.AppearanceGroup.Options.UseFont = True
        Me.INDlycBaseTurn.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycBaseTurn.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycBaseTurn.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseTurn.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycBaseTurn.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycBaseTurn.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycBaseTurn.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseTurn.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycBaseTurn.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseTurn.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycBaseTurn.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycBaseTurn.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycBaseTurn, False)
        Me.INDlycBaseTurn.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycBaseTurn.GroupBordersVisible = False
        Me.INDlycBaseTurn.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrTurn, Me.INDlyGrTimeTable})
        Me.INDlycBaseTurn.Location = New System.Drawing.Point(0, 0)
        Me.INDlycBaseTurn.Name = "Root"
        Me.INDlycBaseTurn.Size = New System.Drawing.Size(853, 581)
        Me.INDlycBaseTurn.TextVisible = False
        '
        'INDlyGrTurn
        '
        Me.INDlyGrTurn.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTurn.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrTurn.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTurn.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrTurn.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTurn.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrTurn.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrTurn.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrTurn.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTurn.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrTurn.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTurn.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrTurn.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTurn.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrTurn, False)
        Me.INDlyGrTurn.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemDescription})
        Me.INDlyGrTurn.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrTurn.Name = "INDlyGrTurn"
        Me.INDlyGrTurn.Size = New System.Drawing.Size(414, 561)
        Me.INDlyGrTurn.Text = "Turno"
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
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(51, 17)
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 64)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(390, 64)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemName.TextSize = New System.Drawing.Size(51, 17)
        '
        'INDlyItemDescription
        '
        Me.INDlyItemDescription.AllowHide = False
        Me.INDlyItemDescription.Control = Me.INDtxtDescription
        Me.INDlyItemDescription.Location = New System.Drawing.Point(0, 128)
        Me.INDlyItemDescription.MaxSize = New System.Drawing.Size(390, 104)
        Me.INDlyItemDescription.MinSize = New System.Drawing.Size(390, 104)
        Me.INDlyItemDescription.Name = "INDlyItemDescription"
        Me.INDlyItemDescription.ShowInCustomizationForm = False
        Me.INDlyItemDescription.Size = New System.Drawing.Size(390, 374)
        Me.INDlyItemDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDescription.Text = "Descripción"
        Me.INDlyItemDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDescription.TextSize = New System.Drawing.Size(75, 17)
        Me.INDlyItemDescription.TextToControlDistance = 5
        '
        'INDlyGrTimeTable
        '
        Me.INDlyGrTimeTable.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTimeTable.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrTimeTable.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTimeTable.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrTimeTable.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTable.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrTimeTable.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrTimeTable.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrTimeTable.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTable.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrTimeTable.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTable.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrTimeTable.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTable.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrTimeTable, False)
        Me.INDlyGrTimeTable.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrTimeTableWork, Me.INDlyGrTimeTableDelivery, Me.INDlyItemWeekFraction})
        Me.INDlyGrTimeTable.Location = New System.Drawing.Point(414, 0)
        Me.INDlyGrTimeTable.Name = "INDlyGrTimeTable"
        Me.INDlyGrTimeTable.Size = New System.Drawing.Size(419, 561)
        Me.INDlyGrTimeTable.Text = "Horarios"
        '
        'INDlyGrTimeTableWork
        '
        Me.INDlyGrTimeTableWork.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTimeTableWork.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrTimeTableWork.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTimeTableWork.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrTimeTableWork.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableWork.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrTimeTableWork.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrTimeTableWork.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrTimeTableWork.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableWork.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrTimeTableWork.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableWork.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrTimeTableWork.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableWork.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrTimeTableWork, False)
        Me.INDlyGrTimeTableWork.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemWorkTimeDesde, Me.INDlyItemWorkTimeHasta})
        Me.INDlyGrTimeTableWork.Location = New System.Drawing.Point(0, 64)
        Me.INDlyGrTimeTableWork.Name = "INDlyGrTimeTableWork"
        Me.INDlyGrTimeTableWork.Size = New System.Drawing.Size(395, 93)
        Me.INDlyGrTimeTableWork.Text = "Horario de Trabajo"
        '
        'INDlyItemWorkTimeDesde
        '
        Me.INDlyItemWorkTimeDesde.AllowHide = False
        Me.INDlyItemWorkTimeDesde.Control = Me.INDTimeEditWorkDesde
        Me.INDlyItemWorkTimeDesde.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemWorkTimeDesde.MaxSize = New System.Drawing.Size(188, 34)
        Me.INDlyItemWorkTimeDesde.MinSize = New System.Drawing.Size(188, 34)
        Me.INDlyItemWorkTimeDesde.Name = "INDlyItemWorkTimeDesde"
        Me.INDlyItemWorkTimeDesde.ShowInCustomizationForm = False
        Me.INDlyItemWorkTimeDesde.Size = New System.Drawing.Size(188, 34)
        Me.INDlyItemWorkTimeDesde.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWorkTimeDesde.Text = "Desde:"
        Me.INDlyItemWorkTimeDesde.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemWorkTimeDesde.TextSize = New System.Drawing.Size(44, 17)
        Me.INDlyItemWorkTimeDesde.TextToControlDistance = 5
        '
        'INDlyItemWorkTimeHasta
        '
        Me.INDlyItemWorkTimeHasta.AllowHide = False
        Me.INDlyItemWorkTimeHasta.Control = Me.INDTimeEditWorkHasta
        Me.INDlyItemWorkTimeHasta.Location = New System.Drawing.Point(188, 0)
        Me.INDlyItemWorkTimeHasta.MaxSize = New System.Drawing.Size(183, 34)
        Me.INDlyItemWorkTimeHasta.MinSize = New System.Drawing.Size(183, 34)
        Me.INDlyItemWorkTimeHasta.Name = "INDlyItemWorkTimeHasta"
        Me.INDlyItemWorkTimeHasta.ShowInCustomizationForm = False
        Me.INDlyItemWorkTimeHasta.Size = New System.Drawing.Size(183, 34)
        Me.INDlyItemWorkTimeHasta.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWorkTimeHasta.Text = "Hasta:"
        Me.INDlyItemWorkTimeHasta.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemWorkTimeHasta.TextSize = New System.Drawing.Size(39, 17)
        Me.INDlyItemWorkTimeHasta.TextToControlDistance = 5
        '
        'INDlyGrTimeTableDelivery
        '
        Me.INDlyGrTimeTableDelivery.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTimeTableDelivery.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrTimeTableDelivery.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyGrTimeTableDelivery.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyGrTimeTableDelivery.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrTimeTableDelivery, False)
        Me.INDlyGrTimeTableDelivery.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemDeliveryTimeDesde, Me.INDlyItemDeliveryTimeHasta})
        Me.INDlyGrTimeTableDelivery.Location = New System.Drawing.Point(0, 157)
        Me.INDlyGrTimeTableDelivery.Name = "INDlyGrTimeTableDelivery"
        Me.INDlyGrTimeTableDelivery.Size = New System.Drawing.Size(395, 345)
        Me.INDlyGrTimeTableDelivery.Text = "Horario de Entrega"
        '
        'INDlyItemDeliveryTimeDesde
        '
        Me.INDlyItemDeliveryTimeDesde.AllowHide = False
        Me.INDlyItemDeliveryTimeDesde.Control = Me.INDTimeEditDeliveryDesde
        Me.INDlyItemDeliveryTimeDesde.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemDeliveryTimeDesde.MaxSize = New System.Drawing.Size(188, 34)
        Me.INDlyItemDeliveryTimeDesde.MinSize = New System.Drawing.Size(188, 34)
        Me.INDlyItemDeliveryTimeDesde.Name = "INDlyItemDeliveryTimeDesde"
        Me.INDlyItemDeliveryTimeDesde.ShowInCustomizationForm = False
        Me.INDlyItemDeliveryTimeDesde.Size = New System.Drawing.Size(188, 286)
        Me.INDlyItemDeliveryTimeDesde.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDeliveryTimeDesde.Text = "Desde:"
        Me.INDlyItemDeliveryTimeDesde.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDeliveryTimeDesde.TextSize = New System.Drawing.Size(44, 17)
        Me.INDlyItemDeliveryTimeDesde.TextToControlDistance = 5
        '
        'INDlyItemDeliveryTimeHasta
        '
        Me.INDlyItemDeliveryTimeHasta.AllowHide = False
        Me.INDlyItemDeliveryTimeHasta.Control = Me.INDTimeEditDeliveryHasta
        Me.INDlyItemDeliveryTimeHasta.Location = New System.Drawing.Point(188, 0)
        Me.INDlyItemDeliveryTimeHasta.MaxSize = New System.Drawing.Size(183, 34)
        Me.INDlyItemDeliveryTimeHasta.MinSize = New System.Drawing.Size(183, 34)
        Me.INDlyItemDeliveryTimeHasta.Name = "INDlyItemDeliveryTimeHasta"
        Me.INDlyItemDeliveryTimeHasta.ShowInCustomizationForm = False
        Me.INDlyItemDeliveryTimeHasta.Size = New System.Drawing.Size(183, 286)
        Me.INDlyItemDeliveryTimeHasta.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDeliveryTimeHasta.Text = "Hasta:"
        Me.INDlyItemDeliveryTimeHasta.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDeliveryTimeHasta.TextSize = New System.Drawing.Size(39, 17)
        Me.INDlyItemDeliveryTimeHasta.TextToControlDistance = 5
        '
        'INDlyItemWeekFraction
        '
        Me.INDlyItemWeekFraction.AllowHide = False
        Me.INDlyItemWeekFraction.Control = Me.INDrgWeekFraction
        Me.INDlyItemWeekFraction.CustomizationFormText = "Días de Semana"
        Me.INDlyItemWeekFraction.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemWeekFraction.MaxSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemWeekFraction.MinSize = New System.Drawing.Size(390, 64)
        Me.INDlyItemWeekFraction.Name = "INDlyItemWeekFraction"
        Me.INDlyItemWeekFraction.ShowInCustomizationForm = False
        Me.INDlyItemWeekFraction.Size = New System.Drawing.Size(395, 64)
        Me.INDlyItemWeekFraction.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemWeekFraction.Text = "Días de Semana"
        Me.INDlyItemWeekFraction.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemWeekFraction.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemWeekFraction.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemWeekFraction.TextToControlDistance = 5
        '
        'FrmTurn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Name = "FrmTurn"
        Me.Opacity = 1.0R
        Me.Tag = "2061"
        Me.Text = "Turnos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDcncTurn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycBase.ResumeLayout(False)
        CType(Me.INDTimeEditDeliveryHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTimeEditDeliveryDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTimeEditWorkHasta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTimeEditWorkDesde.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrgWeekFraction.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycBaseTurn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrTurn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrTimeTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrTimeTableWork, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWorkTimeDesde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWorkTimeHasta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrTimeTableDelivery, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDeliveryTimeDesde, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDeliveryTimeHasta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemWeekFraction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoRadioGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDcncTurn As CtrNavigationControlPanel
    Friend WithEvents INDlycBase As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDlycBaseTurn As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyGrTurn As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As IndigoGridLookUpControl
    Friend WithEvents INDrgWeekFraction As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents IndigoRadioGroup1 As IndigoRadioGroup
    Friend WithEvents INDlyItemWeekFraction As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrTimeTable As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGrTimeTableWork As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyGrTimeTableDelivery As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTimeEditDeliveryHasta As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDTimeEditDeliveryDesde As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDTimeEditWorkHasta As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDTimeEditWorkDesde As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDlyItemWorkTimeDesde As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemWorkTimeHasta As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDeliveryTimeDesde As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemDeliveryTimeHasta As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As IndigoDate
End Class
