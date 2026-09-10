Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmupdateDateconfirm
    Inherits CustomizableFormBase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmupdateDateconfirm))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDbteNumberRadicate = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDdateConfirm = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtUserConfirm = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyGrRadicate = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyNumberRdicate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDdteDateConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLcUserConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlycRoot.SuspendLayout()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteNumberRadicate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdateConfirm.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdateConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtUserConfirm.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrRadicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyNumberRdicate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteDateConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLcUserConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDlycgRoot
        '
        Me.INDlycgRoot.AppearanceGroup.Font = CType(resources.GetObject("INDlycgRoot.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlycgRoot.AppearanceItemCaption.Font = CType(resources.GetObject("INDlycgRoot.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlycgRoot.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlycgRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycgRoot, False)
        resources.ApplyResources(Me.INDlycgRoot, "INDlycgRoot")
        Me.INDlycgRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrRadicate})
        Me.INDlycgRoot.Name = "Root"
        Me.INDlycgRoot.Size = New System.Drawing.Size(599, 380)
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AllowCustomization = False
        Me.INDlycRoot.Controls.Add(Me.INDtxtUserConfirm)
        Me.INDlycRoot.Controls.Add(Me.INDdateConfirm)
        Me.INDlycRoot.Controls.Add(Me.INDbteNumberRadicate)
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, False)
        resources.ApplyResources(Me.INDlycRoot, "INDlycRoot")
        Me.INDlycRoot.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(741, 239, 825, 404)
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.SetChildIndex(Me.CtrNavigationControl1, 0)
        Me.INDPanelControlBase.Controls.SetChildIndex(Me.INDlycRoot, 0)
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
        'INDbteNumberRadicate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteNumberRadicate, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteNumberRadicate, True)
        resources.ApplyResources(Me.INDbteNumberRadicate, "INDbteNumberRadicate")
        Me.IndigoTextEdit1.SetMascara(Me.INDbteNumberRadicate, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDbteNumberRadicate.Name = "INDbteNumberRadicate"
        Me.INDbteNumberRadicate.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteNumberRadicate.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteNumberRadicate.Properties.Appearance.Font = CType(resources.GetObject("INDbteNumberRadicate.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteNumberRadicate.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteNumberRadicate.Properties.Appearance.Options.UseFont = True
        Me.INDbteNumberRadicate.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbteNumberRadicate.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbteNumberRadicate.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbteNumberRadicate.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbteNumberRadicate.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteNumberRadicate.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteNumberRadicate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteNumberRadicate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteNumberRadicate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteNumberRadicate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteNumberRadicate.Properties.Buttons1"), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Glosas.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteNumberRadicate.Properties.Buttons7"), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteNumberRadicate.Properties.Buttons10"), Boolean))})
        Me.INDbteNumberRadicate.Properties.Mask.EditMask = resources.GetString("INDbteNumberRadicate.Properties.Mask.EditMask")
        Me.INDbteNumberRadicate.Properties.Mask.MaskType = CType(resources.GetObject("INDbteNumberRadicate.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteNumberRadicate.Properties.MaxLength = 15
        Me.INDbteNumberRadicate.StyleController = Me.INDlycRoot
        Me.INDbteNumberRadicate.Tag = "Nit"
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteNumberRadicate, 0)
        '
        'INDdateConfirm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdateConfirm, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdateConfirm, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdateConfirm, True)
        resources.ApplyResources(Me.INDdateConfirm, "INDdateConfirm")
        Me.IndigoTextEdit1.SetMascara(Me.INDdateConfirm, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdateConfirm, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdateConfirm.Name = "INDdateConfirm"
        Me.INDdateConfirm.Properties.Appearance.BackColor = CType(resources.GetObject("INDdateConfirm.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDdateConfirm.Properties.Appearance.Font = CType(resources.GetObject("INDdateConfirm.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDdateConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDdateConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDdateConfirm.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDdateConfirm.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDdateConfirm.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDdateConfirm.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDdateConfirm.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDdateConfirm.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDdateConfirm.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdateConfirm.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdateConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdateConfirm.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDdateConfirm.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDdateConfirm.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDdateConfirm.Properties.Mask.EditMask = resources.GetString("INDdateConfirm.Properties.Mask.EditMask")
        Me.INDdateConfirm.Properties.Mask.MaskType = CType(resources.GetObject("INDdateConfirm.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDdateConfirm.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDdateConfirm.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDdateConfirm.StyleController = Me.INDlycRoot
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdateConfirm, 0)
        '
        'INDtxtUserConfirm
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtUserConfirm, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtUserConfirm, False)
        resources.ApplyResources(Me.INDtxtUserConfirm, "INDtxtUserConfirm")
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtUserConfirm, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtUserConfirm.Name = "INDtxtUserConfirm"
        Me.INDtxtUserConfirm.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtUserConfirm.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtUserConfirm.Properties.Appearance.Font = CType(resources.GetObject("INDtxtUserConfirm.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtUserConfirm.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtUserConfirm.Properties.Appearance.Options.UseFont = True
        Me.INDtxtUserConfirm.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtUserConfirm.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtUserConfirm.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtUserConfirm.Properties.Mask.EditMask = resources.GetString("INDtxtUserConfirm.Properties.Mask.EditMask")
        Me.INDtxtUserConfirm.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtUserConfirm.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtUserConfirm.Properties.ReadOnly = True
        Me.INDtxtUserConfirm.StyleController = Me.INDlycRoot
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtUserConfirm, 0)
        '
        'INDlyGrRadicate
        '
        Me.INDlyGrRadicate.AppearanceGroup.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceGroup.Options.UseFont = True
        Me.INDlyGrRadicate.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrRadicate.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrRadicate.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlyGrRadicate.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrRadicate.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlyGrRadicate.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlyGrRadicate.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlyGrRadicate.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrRadicate, False)
        resources.ApplyResources(Me.INDlyGrRadicate, "INDlyGrRadicate")
        Me.INDlyGrRadicate.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyNumberRdicate, Me.INDdteDateConfirm, Me.INDLcUserConfirm})
        Me.INDlyGrRadicate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrRadicate.Name = "INDlyGrRadicate"
        Me.INDlyGrRadicate.Size = New System.Drawing.Size(579, 360)
        '
        'INDlyNumberRdicate
        '
        Me.INDlyNumberRdicate.AllowHide = False
        Me.INDlyNumberRdicate.Control = Me.INDbteNumberRadicate
        resources.ApplyResources(Me.INDlyNumberRdicate, "INDlyNumberRdicate")
        Me.INDlyNumberRdicate.Location = New System.Drawing.Point(0, 0)
        Me.INDlyNumberRdicate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyNumberRdicate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyNumberRdicate.Name = "INDlyNumberRdicate"
        Me.INDlyNumberRdicate.ShowInCustomizationForm = False
        Me.INDlyNumberRdicate.Size = New System.Drawing.Size(555, 60)
        Me.INDlyNumberRdicate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyNumberRdicate.Tag = "Nit"
        Me.INDlyNumberRdicate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyNumberRdicate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyNumberRdicate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyNumberRdicate.TextToControlDistance = 5
        '
        'INDdteDateConfirm
        '
        Me.INDdteDateConfirm.Control = Me.INDdateConfirm
        resources.ApplyResources(Me.INDdteDateConfirm, "INDdteDateConfirm")
        Me.INDdteDateConfirm.Location = New System.Drawing.Point(0, 120)
        Me.INDdteDateConfirm.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDdteDateConfirm.MinSize = New System.Drawing.Size(390, 60)
        Me.INDdteDateConfirm.Name = "INDdteDateConfirm"
        Me.INDdteDateConfirm.ShowInCustomizationForm = False
        Me.INDdteDateConfirm.Size = New System.Drawing.Size(555, 181)
        Me.INDdteDateConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDdteDateConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDdteDateConfirm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDdteDateConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDdteDateConfirm.TextToControlDistance = 5
        '
        'INDLcUserConfirm
        '
        Me.INDLcUserConfirm.Control = Me.INDtxtUserConfirm
        Me.INDLcUserConfirm.Location = New System.Drawing.Point(0, 60)
        Me.INDLcUserConfirm.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDLcUserConfirm.MinSize = New System.Drawing.Size(390, 60)
        Me.INDLcUserConfirm.Name = "INDLcUserConfirm"
        Me.INDLcUserConfirm.Size = New System.Drawing.Size(555, 60)
        Me.INDLcUserConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLcUserConfirm, "INDLcUserConfirm")
        Me.INDLcUserConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLcUserConfirm.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLcUserConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDLcUserConfirm.TextToControlDistance = 5
        Me.INDLcUserConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'CtrNavigationControl1
        '
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.LayoutControl = Me.INDlycRoot
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmupdateDateconfirm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmupdateDateconfirm"
        Me.OwnerModule = "ModuloGlosas"
        Me.Schema = ""
        Me.TableName = ""
        Me.Tag = "512"
        CType(Me.INDlycgRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlycRoot.ResumeLayout(False)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteNumberRadicate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdateConfirm.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdateConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtUserConfirm.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrRadicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyNumberRdicate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteDateConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLcUserConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDbteNumberRadicate As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlyGrRadicate As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyNumberRdicate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdateConfirm As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDdteDateConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents INDtxtUserConfirm As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLcUserConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
End Class
