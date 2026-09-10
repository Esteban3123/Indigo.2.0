<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCambiarContrasena
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCambiarContrasena))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDTxtCoAnterior = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtUserName = New DevExpress.XtraEditors.LabelControl()
        Me.INDtxtConfContra = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtNuContraseña = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteUserCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiPasswordBack = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiPasswordNew = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiConfirmPassword = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiUserCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiUserName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtCoAnterior.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDtxtConfContra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtNuContraseña.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteUserCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiPasswordBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiPasswordNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiConfirmPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiUserCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        '
        'BarraBotones
        '
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        '
        'INDTxtCoAnterior
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtCoAnterior, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtCoAnterior, False)
        Me.INDTxtCoAnterior.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtCoAnterior, "INDTxtCoAnterior")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtCoAnterior, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtCoAnterior.Name = "INDTxtCoAnterior"
        Me.INDTxtCoAnterior.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtCoAnterior.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtCoAnterior.Properties.Appearance.Font = CType(resources.GetObject("INDTxtCoAnterior.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtCoAnterior.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtCoAnterior.Properties.Appearance.Options.UseFont = True
        Me.INDTxtCoAnterior.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtCoAnterior.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtCoAnterior.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtCoAnterior.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtCoAnterior.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtCoAnterior.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtCoAnterior.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtCoAnterior.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtCoAnterior.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtCoAnterior.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(108)
        Me.INDTxtCoAnterior.Properties.UseSystemPasswordChar = True
        Me.INDTxtCoAnterior.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtCoAnterior, 0)
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDtxtUserName)
        Me.LayoutControl1.Controls.Add(Me.INDtxtConfContra)
        Me.LayoutControl1.Controls.Add(Me.INDTxtNuContraseña)
        Me.LayoutControl1.Controls.Add(Me.INDTxtCoAnterior)
        Me.LayoutControl1.Controls.Add(Me.INDbteUserCode)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(512, 485, 250, 350)
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'INDtxtUserName
        '
        Me.INDtxtUserName.Appearance.Font = CType(resources.GetObject("INDtxtUserName.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtUserName.Appearance.ForeColor = CType(resources.GetObject("INDtxtUserName.Appearance.ForeColor"), System.Drawing.Color)
        resources.ApplyResources(Me.INDtxtUserName, "INDtxtUserName")
        Me.INDtxtUserName.Name = "INDtxtUserName"
        Me.INDtxtUserName.StyleController = Me.LayoutControl1
        '
        'INDtxtConfContra
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtConfContra, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtConfContra, False)
        Me.INDtxtConfContra.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDtxtConfContra, "INDtxtConfContra")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtConfContra, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtConfContra.Name = "INDtxtConfContra"
        Me.INDtxtConfContra.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtConfContra.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtConfContra.Properties.Appearance.Font = CType(resources.GetObject("INDtxtConfContra.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtConfContra.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtConfContra.Properties.Appearance.Options.UseFont = True
        Me.INDtxtConfContra.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtConfContra.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtConfContra.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtConfContra.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtConfContra.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtConfContra.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtConfContra.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtConfContra.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtConfContra.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtConfContra.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(108)
        Me.INDtxtConfContra.Properties.UseSystemPasswordChar = True
        Me.INDtxtConfContra.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtConfContra, 0)
        '
        'INDTxtNuContraseña
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtNuContraseña, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtNuContraseña, False)
        Me.INDTxtNuContraseña.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDTxtNuContraseña, "INDTxtNuContraseña")
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtNuContraseña, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDTxtNuContraseña.Name = "INDTxtNuContraseña"
        Me.INDTxtNuContraseña.Properties.Appearance.BackColor = CType(resources.GetObject("INDTxtNuContraseña.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDTxtNuContraseña.Properties.Appearance.Font = CType(resources.GetObject("INDTxtNuContraseña.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtNuContraseña.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtNuContraseña.Properties.Appearance.Options.UseFont = True
        Me.INDTxtNuContraseña.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDTxtNuContraseña.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDTxtNuContraseña.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDTxtNuContraseña.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDTxtNuContraseña.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtNuContraseña.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtNuContraseña.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtNuContraseña.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtNuContraseña.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtNuContraseña.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(108)
        Me.INDTxtNuContraseña.Properties.UseSystemPasswordChar = True
        Me.INDTxtNuContraseña.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtNuContraseña, 0)
        '
        'INDbteUserCode
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteUserCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteUserCode, False)
        Me.INDbteUserCode.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDbteUserCode, "INDbteUserCode")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteUserCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteUserCode.Name = "INDbteUserCode"
        Me.INDbteUserCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteUserCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteUserCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteUserCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteUserCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteUserCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteUserCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbteUserCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbteUserCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbteUserCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbteUserCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteUserCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteUserCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteUserCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteUserCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteUserCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteUserCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteUserCode.Properties.Buttons1"), CType(resources.GetObject("INDbteUserCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteUserCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteUserCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteUserCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteUserCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Security.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteUserCode.Properties.Buttons7"), CType(resources.GetObject("INDbteUserCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteUserCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteUserCode.Properties.Buttons10"), Boolean))})
        Me.INDbteUserCode.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteUserCode, 0)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1168, 558)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiPasswordBack, Me.INDlyiPasswordNew, Me.INDlyiConfirmPassword, Me.INDlyiUserCode, Me.INDlyiUserName})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(1148, 538)
        '
        'INDlyiPasswordBack
        '
        Me.INDlyiPasswordBack.Control = Me.INDTxtCoAnterior
        Me.INDlyiPasswordBack.Location = New System.Drawing.Point(0, 72)
        Me.INDlyiPasswordBack.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiPasswordBack.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiPasswordBack.Name = "INDlyiPasswordBack"
        Me.INDlyiPasswordBack.Size = New System.Drawing.Size(1124, 36)
        Me.INDlyiPasswordBack.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDlyiPasswordBack, "INDlyiPasswordBack")
        Me.INDlyiPasswordBack.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiPasswordBack.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyiPasswordBack.TextToControlDistance = 12
        '
        'INDlyiPasswordNew
        '
        Me.INDlyiPasswordNew.Control = Me.INDTxtNuContraseña
        Me.INDlyiPasswordNew.Location = New System.Drawing.Point(0, 108)
        Me.INDlyiPasswordNew.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiPasswordNew.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiPasswordNew.Name = "INDlyiPasswordNew"
        Me.INDlyiPasswordNew.Size = New System.Drawing.Size(1124, 36)
        Me.INDlyiPasswordNew.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDlyiPasswordNew, "INDlyiPasswordNew")
        Me.INDlyiPasswordNew.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiPasswordNew.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyiPasswordNew.TextToControlDistance = 12
        '
        'INDlyiConfirmPassword
        '
        Me.INDlyiConfirmPassword.Control = Me.INDtxtConfContra
        Me.INDlyiConfirmPassword.Location = New System.Drawing.Point(0, 144)
        Me.INDlyiConfirmPassword.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiConfirmPassword.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiConfirmPassword.Name = "INDlyiConfirmPassword"
        Me.INDlyiConfirmPassword.Size = New System.Drawing.Size(1124, 335)
        Me.INDlyiConfirmPassword.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDlyiConfirmPassword, "INDlyiConfirmPassword")
        Me.INDlyiConfirmPassword.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiConfirmPassword.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyiConfirmPassword.TextToControlDistance = 12
        '
        'INDlyiUserCode
        '
        Me.INDlyiUserCode.Control = Me.INDbteUserCode
        resources.ApplyResources(Me.INDlyiUserCode, "INDlyiUserCode")
        Me.INDlyiUserCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiUserCode.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiUserCode.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiUserCode.Name = "INDlyiUserCode"
        Me.INDlyiUserCode.Size = New System.Drawing.Size(1124, 36)
        Me.INDlyiUserCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiUserCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiUserCode.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyiUserCode.TextToControlDistance = 12
        Me.INDlyiUserCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyiUserName
        '
        Me.INDlyiUserName.Control = Me.INDtxtUserName
        resources.ApplyResources(Me.INDlyiUserName, "INDlyiUserName")
        Me.INDlyiUserName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiUserName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiUserName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiUserName.Name = "INDlyiUserName"
        Me.INDlyiUserName.Size = New System.Drawing.Size(1124, 36)
        Me.INDlyiUserName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiUserName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiUserName.TextSize = New System.Drawing.Size(150, 20)
        Me.INDlyiUserName.TextToControlDistance = 12
        Me.INDlyiUserName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmCambiarContrasena
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmCambiarContrasena"
        Me.Opacity = 1.0R
        Me.Tag = "104"
        Me.ViewModeEditHold = True
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtCoAnterior.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDtxtConfContra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtNuContraseña.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteUserCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiPasswordBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiPasswordNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiConfirmPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiUserCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtConfContra As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtNuContraseña As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtCoAnterior As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiPasswordBack As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiPasswordNew As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiConfirmPassword As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDbteUserCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyiUserCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtUserName As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlyiUserName As DevExpress.XtraLayout.LayoutControlItem
End Class
