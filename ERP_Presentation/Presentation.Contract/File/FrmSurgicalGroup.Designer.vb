Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSurgicalGroup
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDlySurgicalGroup = New DevExpress.XtraLayout.LayoutControl()
        Me.INDseMaterialsService = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseRoomService = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseAssistantService = New DevExpress.XtraEditors.SpinEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDseAnesthesiologistService = New DevExpress.XtraEditors.SpinEdit()
        Me.INDbtnCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDseSurgeonService = New DevExpress.XtraEditors.SpinEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygInformationGeneral = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSurgeonService = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAnesthesiologistService = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAssistantService = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRoomService = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemMaterialsService = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl()
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit()
        Me.IndigoSimpleButton2 = New Presentation.Controls.IndigoSimpleButton()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlySurgicalGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlySurgicalGroup.SuspendLayout()
        CType(Me.INDseMaterialsService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseRoomService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseAssistantService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseAnesthesiologistService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseSurgeonService.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygInformationGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSurgeonService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAnesthesiologistService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAssistantService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRoomService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemMaterialsService, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlySurgicalGroup)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 118)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(484, 353)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(484, 94)
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        Me.BarraBotones.Size = New System.Drawing.Size(484, 94)
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControl1.LayoutControl = Me.INDlySurgicalGroup
        Me.CtrNavigationControl1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControl1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.Size = New System.Drawing.Size(200, 344)
        Me.CtrNavigationControl1.TabIndex = 0
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'INDlySurgicalGroup
        '
        Me.INDlySurgicalGroup.AllowCustomization = False
        Me.INDlySurgicalGroup.Controls.Add(Me.INDseMaterialsService)
        Me.INDlySurgicalGroup.Controls.Add(Me.INDseRoomService)
        Me.INDlySurgicalGroup.Controls.Add(Me.INDseAssistantService)
        Me.INDlySurgicalGroup.Controls.Add(Me.INDtxtName)
        Me.INDlySurgicalGroup.Controls.Add(Me.INDseAnesthesiologistService)
        Me.INDlySurgicalGroup.Controls.Add(Me.INDbtnCode)
        Me.INDlySurgicalGroup.Controls.Add(Me.INDseSurgeonService)
        Me.INDlySurgicalGroup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlySurgicalGroup, False)
        Me.INDlySurgicalGroup.Location = New System.Drawing.Point(202, 7)
        Me.INDlySurgicalGroup.Name = "INDlySurgicalGroup"
        Me.INDlySurgicalGroup.Root = Me.LayoutControlGroup1
        Me.INDlySurgicalGroup.Size = New System.Drawing.Size(280, 344)
        Me.INDlySurgicalGroup.TabIndex = 1
        Me.INDlySurgicalGroup.Text = "LayoutControl1"
        '
        'INDseMaterialsService
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseMaterialsService, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseMaterialsService, True)
        Me.INDseMaterialsService.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseMaterialsService.EnterMoveNextControl = True
        Me.INDseMaterialsService.Location = New System.Drawing.Point(164, 275)
        Me.IndigoTextEdit1.SetMascara(Me.INDseMaterialsService, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseMaterialsService.Name = "INDseMaterialsService"
        Me.INDseMaterialsService.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDseMaterialsService.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseMaterialsService.Properties.Appearance.Options.UseBackColor = True
        Me.INDseMaterialsService.Properties.Appearance.Options.UseFont = True
        Me.INDseMaterialsService.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseMaterialsService.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseMaterialsService.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseMaterialsService.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseMaterialsService.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseMaterialsService.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseMaterialsService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseMaterialsService.Properties.Mask.EditMask = "P"
        Me.INDseMaterialsService.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseMaterialsService.Size = New System.Drawing.Size(246, 28)
        Me.INDseMaterialsService.StyleController = Me.INDlySurgicalGroup
        Me.INDseMaterialsService.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseMaterialsService, 0)
        '
        'INDseRoomService
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseRoomService, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseRoomService, True)
        Me.INDseRoomService.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseRoomService.EnterMoveNextControl = True
        Me.INDseRoomService.Location = New System.Drawing.Point(164, 239)
        Me.IndigoTextEdit1.SetMascara(Me.INDseRoomService, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseRoomService.Name = "INDseRoomService"
        Me.INDseRoomService.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDseRoomService.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseRoomService.Properties.Appearance.Options.UseBackColor = True
        Me.INDseRoomService.Properties.Appearance.Options.UseFont = True
        Me.INDseRoomService.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseRoomService.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseRoomService.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseRoomService.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseRoomService.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseRoomService.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseRoomService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseRoomService.Properties.Mask.EditMask = "P"
        Me.INDseRoomService.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseRoomService.Size = New System.Drawing.Size(246, 28)
        Me.INDseRoomService.StyleController = Me.INDlySurgicalGroup
        Me.INDseRoomService.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseRoomService, 0)
        '
        'INDseAssistantService
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseAssistantService, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseAssistantService, True)
        Me.INDseAssistantService.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseAssistantService.EnterMoveNextControl = True
        Me.INDseAssistantService.Location = New System.Drawing.Point(164, 203)
        Me.IndigoTextEdit1.SetMascara(Me.INDseAssistantService, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseAssistantService.Name = "INDseAssistantService"
        Me.INDseAssistantService.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDseAssistantService.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseAssistantService.Properties.Appearance.Options.UseBackColor = True
        Me.INDseAssistantService.Properties.Appearance.Options.UseFont = True
        Me.INDseAssistantService.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseAssistantService.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseAssistantService.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseAssistantService.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseAssistantService.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseAssistantService.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseAssistantService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseAssistantService.Properties.Mask.EditMask = "P"
        Me.INDseAssistantService.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseAssistantService.Size = New System.Drawing.Size(246, 28)
        Me.INDseAssistantService.StyleController = Me.INDlySurgicalGroup
        Me.INDseAssistantService.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseAssistantService, 0)
        '
        'INDtxtName
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.INDtxtName.Location = New System.Drawing.Point(164, 95)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDtxtName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtName.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.MaxLength = 100
        Me.INDtxtName.Size = New System.Drawing.Size(246, 28)
        Me.INDtxtName.StyleController = Me.INDlySurgicalGroup
        Me.INDtxtName.TabIndex = 1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        Me.INDtxtName.ToolTip = "Este Campo es Necesario"
        '
        'INDseAnesthesiologistService
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseAnesthesiologistService, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseAnesthesiologistService, True)
        Me.INDseAnesthesiologistService.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseAnesthesiologistService.EnterMoveNextControl = True
        Me.INDseAnesthesiologistService.Location = New System.Drawing.Point(164, 167)
        Me.IndigoTextEdit1.SetMascara(Me.INDseAnesthesiologistService, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseAnesthesiologistService.Name = "INDseAnesthesiologistService"
        Me.INDseAnesthesiologistService.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDseAnesthesiologistService.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseAnesthesiologistService.Properties.Appearance.Options.UseBackColor = True
        Me.INDseAnesthesiologistService.Properties.Appearance.Options.UseFont = True
        Me.INDseAnesthesiologistService.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseAnesthesiologistService.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseAnesthesiologistService.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseAnesthesiologistService.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseAnesthesiologistService.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseAnesthesiologistService.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseAnesthesiologistService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseAnesthesiologistService.Properties.Mask.EditMask = "P"
        Me.INDseAnesthesiologistService.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseAnesthesiologistService.Size = New System.Drawing.Size(246, 28)
        Me.INDseAnesthesiologistService.StyleController = Me.INDlySurgicalGroup
        Me.INDseAnesthesiologistService.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseAnesthesiologistService, 0)
        '
        'INDbtnCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbtnCode, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbtnCode, True)
        Me.INDbtnCode.Location = New System.Drawing.Point(164, 59)
        Me.IndigoTextEdit1.SetMascara(Me.INDbtnCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbtnCode.Name = "INDbtnCode"
        Me.INDbtnCode.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDbtnCode.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbtnCode.Properties.Appearance.Options.UseFont = True
        Me.INDbtnCode.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbtnCode.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbtnCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbtnCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Contract.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.INDbtnCode.Properties.MaxLength = 20
        Me.INDbtnCode.Size = New System.Drawing.Size(246, 28)
        Me.INDbtnCode.StyleController = Me.INDlySurgicalGroup
        Me.INDbtnCode.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbtnCode, 0)
        Me.INDbtnCode.ToolTip = "Este Campo es Necesario"
        '
        'INDseSurgeonService
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseSurgeonService, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseSurgeonService, True)
        Me.INDseSurgeonService.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseSurgeonService.EnterMoveNextControl = True
        Me.INDseSurgeonService.Location = New System.Drawing.Point(164, 131)
        Me.IndigoTextEdit1.SetMascara(Me.INDseSurgeonService, Presentation.Controls.IndigoTextEdit.EMask.Porcentaje)
        Me.INDseSurgeonService.Name = "INDseSurgeonService"
        Me.INDseSurgeonService.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDseSurgeonService.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseSurgeonService.Properties.Appearance.Options.UseBackColor = True
        Me.INDseSurgeonService.Properties.Appearance.Options.UseFont = True
        Me.INDseSurgeonService.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDseSurgeonService.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDseSurgeonService.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseSurgeonService.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDseSurgeonService.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDseSurgeonService.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseSurgeonService.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseSurgeonService.Properties.Mask.EditMask = "P"
        Me.INDseSurgeonService.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDseSurgeonService.Size = New System.Drawing.Size(246, 28)
        Me.INDseSurgeonService.StyleController = Me.INDlySurgicalGroup
        Me.INDseSurgeonService.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseSurgeonService, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Grupos Quirúrgicos"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygInformationGeneral})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(434, 331)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlygInformationGeneral
        '
        Me.INDlygInformationGeneral.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygInformationGeneral.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygInformationGeneral.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInformationGeneral.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygInformationGeneral.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformationGeneral.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygInformationGeneral.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygInformationGeneral.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygInformationGeneral.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformationGeneral.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygInformationGeneral.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        'Cadena reemplazada... System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDlygInformationGeneral.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlygInformationGeneral.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInformationGeneral.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygInformationGeneral, False)
        Me.INDlygInformationGeneral.CustomizationFormText = "Información General"
        Me.INDlygInformationGeneral.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemSurgeonService, Me.INDlyItemAnesthesiologistService, Me.INDlyItemAssistantService, Me.INDlyItemRoomService, Me.INDlyItemMaterialsService})
        Me.INDlygInformationGeneral.Location = New System.Drawing.Point(0, 0)
        Me.INDlygInformationGeneral.Name = "INDlygInformationGeneral"
        Me.INDlygInformationGeneral.Size = New System.Drawing.Size(414, 311)
        Me.INDlygInformationGeneral.Text = "Información General"
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbtnCode
        Me.INDlyItemCode.CustomizationFormText = "Código"
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Text = "Código"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCode.TextToControlDistance = 5
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.CustomizationFormText = "Nombre"
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Text = "Nombre"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemName.TextToControlDistance = 5
        '
        'INDlyItemSurgeonService
        '
        Me.INDlyItemSurgeonService.Control = Me.INDseSurgeonService
        Me.INDlyItemSurgeonService.CustomizationFormText = "Cirujano"
        Me.INDlyItemSurgeonService.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemSurgeonService.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSurgeonService.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemSurgeonService.Name = "INDlyItemSurgeonService"
        Me.INDlyItemSurgeonService.ShowInCustomizationForm = False
        Me.INDlyItemSurgeonService.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemSurgeonService.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSurgeonService.Text = "Cirujano"
        Me.INDlyItemSurgeonService.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSurgeonService.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSurgeonService.TextToControlDistance = 5
        '
        'INDlyItemAnesthesiologistService
        '
        Me.INDlyItemAnesthesiologistService.Control = Me.INDseAnesthesiologistService
        Me.INDlyItemAnesthesiologistService.CustomizationFormText = "Anestesiólogo"
        Me.INDlyItemAnesthesiologistService.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemAnesthesiologistService.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemAnesthesiologistService.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemAnesthesiologistService.Name = "INDlyItemAnesthesiologistService"
        Me.INDlyItemAnesthesiologistService.ShowInCustomizationForm = False
        Me.INDlyItemAnesthesiologistService.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemAnesthesiologistService.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAnesthesiologistService.Text = "Anestesiólogo"
        Me.INDlyItemAnesthesiologistService.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAnesthesiologistService.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAnesthesiologistService.TextToControlDistance = 5
        '
        'INDlyItemAssistantService
        '
        Me.INDlyItemAssistantService.Control = Me.INDseAssistantService
        Me.INDlyItemAssistantService.CustomizationFormText = "Ayudante"
        Me.INDlyItemAssistantService.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemAssistantService.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemAssistantService.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemAssistantService.Name = "INDlyItemAssistantService"
        Me.INDlyItemAssistantService.ShowInCustomizationForm = False
        Me.INDlyItemAssistantService.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemAssistantService.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAssistantService.Text = "Ayudante"
        Me.INDlyItemAssistantService.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAssistantService.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAssistantService.TextToControlDistance = 5
        '
        'INDlyItemRoomService
        '
        Me.INDlyItemRoomService.Control = Me.INDseRoomService
        Me.INDlyItemRoomService.CustomizationFormText = "Sala"
        Me.INDlyItemRoomService.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemRoomService.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemRoomService.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemRoomService.Name = "INDlyItemRoomService"
        Me.INDlyItemRoomService.ShowInCustomizationForm = False
        Me.INDlyItemRoomService.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemRoomService.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRoomService.Text = "Sala"
        Me.INDlyItemRoomService.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRoomService.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRoomService.TextToControlDistance = 5
        '
        'INDlyItemMaterialsService
        '
        Me.INDlyItemMaterialsService.Control = Me.INDseMaterialsService
        Me.INDlyItemMaterialsService.CustomizationFormText = "Materiales"
        Me.INDlyItemMaterialsService.Location = New System.Drawing.Point(0, 216)
        Me.INDlyItemMaterialsService.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemMaterialsService.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemMaterialsService.Name = "INDlyItemMaterialsService"
        Me.INDlyItemMaterialsService.ShowInCustomizationForm = False
        Me.INDlyItemMaterialsService.Size = New System.Drawing.Size(390, 36)
        Me.INDlyItemMaterialsService.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemMaterialsService.Text = "Materiales"
        Me.INDlyItemMaterialsService.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemMaterialsService.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemMaterialsService.TextToControlDistance = 5
        '
        'FrmSurgicalGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 471)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSurgicalGroup"
        Me.Opacity = 1.0R
        Me.Tag = "983"
        Me.Text = "Grupos Quirúrgicos"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlySurgicalGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlySurgicalGroup.ResumeLayout(False)
        CType(Me.INDseMaterialsService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseRoomService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseAssistantService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseAnesthesiologistService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbtnCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseSurgeonService.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygInformationGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSurgeonService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAnesthesiologistService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAssistantService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRoomService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemMaterialsService, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDlySurgicalGroup As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbtnCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygInformationGeneral As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    'Friend WithEvents IndigoLayoutControl1 As Presentation.Controls.IndigoLayoutControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents INDseMaterialsService As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseRoomService As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseAssistantService As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseAnesthesiologistService As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDseSurgeonService As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents IndigoSimpleButton2 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents INDlyItemSurgeonService As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAnesthesiologistService As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAssistantService As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemRoomService As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemMaterialsService As DevExpress.XtraLayout.LayoutControlItem
End Class
