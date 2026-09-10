Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmContributorType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmContributorType))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.INDlyCtrContributorType = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrContributionType = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyCtrContributorType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyCtrContributorType.SuspendLayout()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrContributionType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDlyCtrContributorType)
        '
        'ToolBars
        '
        resources.ApplyResources(Me.ToolBars, "ToolBars")
        Me.ToolBars.Appearance.BackColor = CType(resources.GetObject("ToolBars.Appearance.BackColor"), System.Drawing.Color)
        Me.ToolBars.Appearance.FontSizeDelta = CType(resources.GetObject("ToolBars.Appearance.FontSizeDelta"), Integer)
        Me.ToolBars.Appearance.FontStyleDelta = CType(resources.GetObject("ToolBars.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.ToolBars.Appearance.GradientMode = CType(resources.GetObject("ToolBars.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.ToolBars.Appearance.Image = CType(resources.GetObject("ToolBars.Appearance.Image"), System.Drawing.Image)
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        resources.ApplyResources(Me.BarraBotones, "BarraBotones")
        Me.BarraBotones.LookAndFeel.SkinName = "Blue"
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDlyCtrContributorType
        '
        resources.ApplyResources(Me.INDlyCtrContributorType, "INDlyCtrContributorType")
        Me.INDlyCtrContributorType.AllowCustomizationMenu = False
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyCtrContributorType.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDlyCtrContributorType.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDlyCtrContributorType.Controls.Add(Me.INDtxtName)
        Me.INDlyCtrContributorType.Controls.Add(Me.INDbteCode)
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCtrContributorType, False)
        Me.INDlyCtrContributorType.Name = "INDlyCtrContributorType"
        Me.INDlyCtrContributorType.Root = Me.LayoutControlGroup1
        '
        'INDtxtName
        '
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDtxtName.Name = "INDtxtName"
        Me.INDtxtName.Properties.AccessibleDescription = resources.GetString("INDtxtName.Properties.AccessibleDescription")
        Me.INDtxtName.Properties.AccessibleName = resources.GetString("INDtxtName.Properties.AccessibleName")
        Me.INDtxtName.Properties.Appearance.BackColor = CType(resources.GetObject("INDtxtName.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.Appearance.Font = CType(resources.GetObject("INDtxtName.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDtxtName.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDtxtName.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDtxtName.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtName.Properties.Appearance.GradientMode = CType(resources.GetObject("INDtxtName.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtName.Properties.Appearance.Image = CType(resources.GetObject("INDtxtName.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDtxtName.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtName.Properties.Appearance.Options.UseFont = True
        Me.INDtxtName.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDtxtName.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDtxtName.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDtxtName.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDtxtName.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDtxtName.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDtxtName.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtName.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtName.Properties.AutoHeight = CType(resources.GetObject("INDtxtName.Properties.AutoHeight"), Boolean)
        Me.INDtxtName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDtxtName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDtxtName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtName.Properties.Mask.EditMask = resources.GetString("INDtxtName.Properties.Mask.EditMask")
        Me.INDtxtName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDtxtName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDtxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDtxtName.Properties.Mask.PlaceHolder"), Char)
        Me.INDtxtName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDtxtName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDtxtName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDtxtName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDtxtName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtName.Properties.MaxLength = 80
        Me.INDtxtName.Properties.NullValuePrompt = resources.GetString("INDtxtName.Properties.NullValuePrompt")
        Me.INDtxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtName.StyleController = Me.INDlyCtrContributorType
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 5)
        '
        'INDbteCode
        '
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.AlfaNumerico)
        Me.INDbteCode.Name = "INDbteCode"
        Me.INDbteCode.Properties.AccessibleDescription = resources.GetString("INDbteCode.Properties.AccessibleDescription")
        Me.INDbteCode.Properties.AccessibleName = resources.GetString("INDbteCode.Properties.AccessibleName")
        Me.INDbteCode.Properties.Appearance.BackColor = CType(resources.GetObject("INDbteCode.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.Appearance.Font = CType(resources.GetObject("INDbteCode.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDbteCode.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDbteCode.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDbteCode.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDbteCode.Properties.Appearance.GradientMode = CType(resources.GetObject("INDbteCode.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDbteCode.Properties.Appearance.Image = CType(resources.GetObject("INDbteCode.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDbteCode.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteCode.Properties.Appearance.Options.UseFont = True
        Me.INDbteCode.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDbteCode.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDbteCode.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDbteCode.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDbteCode.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDbteCode.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDbteCode.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteCode.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDbteCode.Properties.AutoHeight = CType(resources.GetObject("INDbteCode.Properties.AutoHeight"), Boolean)
        resources.ApplyResources(SerializableAppearanceObject1, "SerializableAppearanceObject1")
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Payroll.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
        Me.INDbteCode.Properties.Mask.AutoComplete = CType(resources.GetObject("INDbteCode.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDbteCode.Properties.Mask.BeepOnError = CType(resources.GetObject("INDbteCode.Properties.Mask.BeepOnError"), Boolean)
        Me.INDbteCode.Properties.Mask.EditMask = resources.GetString("INDbteCode.Properties.Mask.EditMask")
        Me.INDbteCode.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDbteCode.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDbteCode.Properties.Mask.MaskType = CType(resources.GetObject("INDbteCode.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDbteCode.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDbteCode.Properties.Mask.PlaceHolder"), Char)
        Me.INDbteCode.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDbteCode.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDbteCode.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDbteCode.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDbteCode.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDbteCode.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDbteCode.Properties.MaxLength = 20
        Me.INDbteCode.Properties.NullValuePrompt = resources.GetString("INDbteCode.Properties.NullValuePrompt")
        Me.INDbteCode.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDbteCode.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDbteCode.StyleController = Me.INDlyCtrContributorType
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 1)
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceGroup.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceGroup.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceGroup.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceItemCaption.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceItemCaption.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("LayoutControlGroup1.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrContributionType})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(680, 334)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrContributionType
        '
        Me.INDlyGrContributionType.AppearanceGroup.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceGroup.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceGroup.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceGroup.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceGroup.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceGroup.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyGrContributionType.AppearanceGroup.ForeColor"), System.Drawing.Color)
        Me.INDlyGrContributionType.AppearanceGroup.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceGroup.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceGroup.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceGroup.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceGroup.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrContributionType.AppearanceItemCaption.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrContributionType.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceItemCaption.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyGrContributionType.AppearanceTabPage.Header.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.Header.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceTabPage.Header.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.Header.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceTabPage.Header.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.Header.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrContributionType.AppearanceTabPage.Header.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.Header.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceTabPage.Header.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.Header.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderActive.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderActive.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderActive.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderActive.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderActive.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderActive.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderActive.ForeColor"), System.Drawing.Color)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderActive.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderActive.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderActive.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderActive.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderActive.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.ForeColor"), System.Drawing.Color)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        'Cadena reemplazada... True
        Me.INDlyGrContributionType.AppearanceTabPage.PageClient.Font = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.PageClient.Font"), System.Drawing.Font)
        Me.INDlyGrContributionType.AppearanceTabPage.PageClient.FontSizeDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.PageClient.FontSizeDelta"), Integer)
        Me.INDlyGrContributionType.AppearanceTabPage.PageClient.FontStyleDelta = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.PageClient.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyGrContributionType.AppearanceTabPage.PageClient.GradientMode = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.PageClient.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyGrContributionType.AppearanceTabPage.PageClient.Image = CType(resources.GetObject("INDlyGrContributionType.AppearanceTabPage.PageClient.Image"), System.Drawing.Image)
        Me.INDlyGrContributionType.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlyGrContributionType, False)
        resources.ApplyResources(Me.INDlyGrContributionType, "INDlyGrContributionType")
        Me.INDlyGrContributionType.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.EmptySpaceItem1})
        Me.INDlyGrContributionType.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrContributionType.Name = "INDlyGrContributionType"
        Me.INDlyGrContributionType.Size = New System.Drawing.Size(660, 314)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.Control = Me.INDbteCode
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(300, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.Control = Me.INDtxtName
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Size = New System.Drawing.Size(636, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(100, 20)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        resources.ApplyResources(Me.EmptySpaceItem1, "EmptySpaceItem1")
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(0, 72)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(636, 182)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'FrmContributorType
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmContributorType"
        Me.Opacity = 1.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Tag = "537"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyCtrContributorType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyCtrContributorType.ResumeLayout(False)
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrContributionType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyCtrContributorType As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyGrContributionType As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
End Class
