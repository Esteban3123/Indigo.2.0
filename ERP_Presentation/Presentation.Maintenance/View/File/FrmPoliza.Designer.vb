Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPoliza
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPoliza))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDbteCode = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyPoliza = New DevExpress.XtraLayout.LayoutControl()
        Me.INDmeObjeto = New DevExpress.XtraEditors.MemoEdit()
        Me.INDdtFechaFinal = New DevExpress.XtraEditors.DateEdit()
        Me.INDdtFechaInicio = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtName = New DevExpress.XtraEditors.TextEdit()
        Me.INDglAseguradora = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDglPolizaType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyGrPoliza = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemCode = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemName = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPolizaType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAseguradora = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFechaInicio = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObjeto = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemFechaFinal = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.CtrNavigationControl1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyPoliza.SuspendLayout()
        CType(Me.INDmeObjeto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdtFechaFinal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdtFechaFinal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdtFechaInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdtFechaInicio.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglAseguradora.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglPolizaType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyGrPoliza, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPolizaType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAseguradora, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFechaInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObjeto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemFechaFinal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        resources.ApplyResources(Me.INDPanelControlBase, "INDPanelControlBase")
        Me.INDPanelControlBase.Controls.Add(Me.INDlyPoliza)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControl1)
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
        'INDbteCode
        '
        resources.ApplyResources(Me.INDbteCode, "INDbteCode")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteCode, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteCode, True)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteCode, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDbteCode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDbteCode.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines), resources.GetString("INDbteCode.Properties.Buttons1"), CType(resources.GetObject("INDbteCode.Properties.Buttons2"), Integer), CType(resources.GetObject("INDbteCode.Properties.Buttons3"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons4"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons5"), Boolean), CType(resources.GetObject("INDbteCode.Properties.Buttons6"), DevExpress.XtraEditors.ImageLocation), Global.Presentation.Maintenance.My.Resources.Resources.BuscarMetro, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, resources.GetString("INDbteCode.Properties.Buttons7"), CType(resources.GetObject("INDbteCode.Properties.Buttons8"), Object), CType(resources.GetObject("INDbteCode.Properties.Buttons9"), DevExpress.Utils.SuperToolTip), CType(resources.GetObject("INDbteCode.Properties.Buttons10"), Boolean))})
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
        Me.INDbteCode.StyleController = Me.INDlyPoliza
        Me.INDbteCode.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteCode, 0)
        '
        'INDlyPoliza
        '
        resources.ApplyResources(Me.INDlyPoliza, "INDlyPoliza")
        Me.INDlyPoliza.AllowCustomization = False
        Me.INDlyPoliza.Appearance.DisabledLayoutGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyPoliza.Appearance.DisabledLayoutGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyPoliza.Appearance.DisabledLayoutGroupCaption.GradientMode = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyPoliza.Appearance.DisabledLayoutGroupCaption.Image = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyPoliza.Appearance.DisabledLayoutItem.FontSizeDelta = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutItem.FontSizeDelta"), Integer)
        Me.INDlyPoliza.Appearance.DisabledLayoutItem.FontStyleDelta = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutItem.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyPoliza.Appearance.DisabledLayoutItem.GradientMode = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutItem.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyPoliza.Appearance.DisabledLayoutItem.Image = CType(resources.GetObject("INDlyPoliza.Appearance.DisabledLayoutItem.Image"), System.Drawing.Image)
        Me.INDlyPoliza.Controls.Add(Me.INDmeObjeto)
        Me.INDlyPoliza.Controls.Add(Me.INDdtFechaFinal)
        Me.INDlyPoliza.Controls.Add(Me.INDdtFechaInicio)
        Me.INDlyPoliza.Controls.Add(Me.INDtxtName)
        Me.INDlyPoliza.Controls.Add(Me.INDbteCode)
        Me.INDlyPoliza.Controls.Add(Me.INDglAseguradora)
        Me.INDlyPoliza.Controls.Add(Me.INDglPolizaType)
        Me.LayoutControls.SetIsCustomizable(Me.INDlyPoliza, False)
        Me.INDlyPoliza.Name = "INDlyPoliza"
        Me.INDlyPoliza.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(741, 239, 250, 350)
        Me.INDlyPoliza.OptionsPrint.AppearanceGroupCaption.FontSizeDelta = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceGroupCaption.FontSizeDelta"), Integer)
        Me.INDlyPoliza.OptionsPrint.AppearanceGroupCaption.FontStyleDelta = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceGroupCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyPoliza.OptionsPrint.AppearanceGroupCaption.GradientMode = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceGroupCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyPoliza.OptionsPrint.AppearanceGroupCaption.Image = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceGroupCaption.Image"), System.Drawing.Image)
        Me.INDlyPoliza.OptionsPrint.AppearanceItemCaption.FontSizeDelta = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceItemCaption.FontSizeDelta"), Integer)
        Me.INDlyPoliza.OptionsPrint.AppearanceItemCaption.FontStyleDelta = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceItemCaption.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDlyPoliza.OptionsPrint.AppearanceItemCaption.GradientMode = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceItemCaption.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDlyPoliza.OptionsPrint.AppearanceItemCaption.Image = CType(resources.GetObject("INDlyPoliza.OptionsPrint.AppearanceItemCaption.Image"), System.Drawing.Image)
        Me.INDlyPoliza.Root = Me.LayoutControlGroup1
        '
        'INDmeObjeto
        '
        resources.ApplyResources(Me.INDmeObjeto, "INDmeObjeto")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeObjeto, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeObjeto, True)
        Me.INDmeObjeto.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDmeObjeto, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeObjeto.Name = "INDmeObjeto"
        Me.INDmeObjeto.Properties.AccessibleDescription = resources.GetString("INDmeObjeto.Properties.AccessibleDescription")
        Me.INDmeObjeto.Properties.AccessibleName = resources.GetString("INDmeObjeto.Properties.AccessibleName")
        Me.INDmeObjeto.Properties.Appearance.BackColor = CType(resources.GetObject("INDmeObjeto.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDmeObjeto.Properties.Appearance.Font = CType(resources.GetObject("INDmeObjeto.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDmeObjeto.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDmeObjeto.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDmeObjeto.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDmeObjeto.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDmeObjeto.Properties.Appearance.GradientMode = CType(resources.GetObject("INDmeObjeto.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDmeObjeto.Properties.Appearance.Image = CType(resources.GetObject("INDmeObjeto.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDmeObjeto.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeObjeto.Properties.Appearance.Options.UseFont = True
        Me.INDmeObjeto.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDmeObjeto.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDmeObjeto.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDmeObjeto.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDmeObjeto.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDmeObjeto.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDmeObjeto.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDmeObjeto.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDmeObjeto.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeObjeto.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeObjeto.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeObjeto.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDmeObjeto.Properties.NullValuePrompt = resources.GetString("INDmeObjeto.Properties.NullValuePrompt")
        Me.INDmeObjeto.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDmeObjeto.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDmeObjeto.StyleController = Me.INDlyPoliza
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeObjeto, 0)
        '
        'INDdtFechaFinal
        '
        resources.ApplyResources(Me.INDdtFechaFinal, "INDdtFechaFinal")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdtFechaFinal, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdtFechaFinal, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdtFechaFinal, False)
        Me.INDdtFechaFinal.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDdtFechaFinal, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdtFechaFinal, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdtFechaFinal.Name = "INDdtFechaFinal"
        Me.INDdtFechaFinal.Properties.AccessibleDescription = resources.GetString("INDdtFechaFinal.Properties.AccessibleDescription")
        Me.INDdtFechaFinal.Properties.AccessibleName = resources.GetString("INDdtFechaFinal.Properties.AccessibleName")
        Me.INDdtFechaFinal.Properties.Appearance.BackColor = CType(resources.GetObject("INDdtFechaFinal.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDdtFechaFinal.Properties.Appearance.Font = CType(resources.GetObject("INDdtFechaFinal.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDdtFechaFinal.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDdtFechaFinal.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDdtFechaFinal.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDdtFechaFinal.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDdtFechaFinal.Properties.Appearance.GradientMode = CType(resources.GetObject("INDdtFechaFinal.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDdtFechaFinal.Properties.Appearance.Image = CType(resources.GetObject("INDdtFechaFinal.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDdtFechaFinal.Properties.Appearance.Options.UseBackColor = True
        Me.INDdtFechaFinal.Properties.Appearance.Options.UseFont = True
        Me.INDdtFechaFinal.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDdtFechaFinal.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDdtFechaFinal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdtFechaFinal.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdtFechaFinal.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdtFechaFinal.Properties.AutoHeight = CType(resources.GetObject("INDdtFechaFinal.Properties.AutoHeight"), Boolean)
        Me.INDdtFechaFinal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDdtFechaFinal.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.AccessibleDescription = resources.GetString("INDdtFechaFinal.Properties.CalendarTimeProperties.AccessibleDescription")
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.AccessibleName = resources.GetString("INDdtFechaFinal.Properties.CalendarTimeProperties.AccessibleName")
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.AutoHeight = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.AutoHeight"), Boolean)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.AutoComplete = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.BeepOnError = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.BeepOnError"), Boolean)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.EditMask")
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.MaskType = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.PlaceHolder = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.PlaceHolder"), Char)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.SaveLiteral = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.SaveLiteral"), Boolean)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.NullValuePrompt = resources.GetString("INDdtFechaFinal.Properties.CalendarTimeProperties.NullValuePrompt")
        Me.INDdtFechaFinal.Properties.CalendarTimeProperties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDdtFechaFinal.Properties.CalendarTimeProperties.NullValuePromptShowForEmptyValu" & _
        "e"), Boolean)
        Me.INDdtFechaFinal.Properties.Mask.AutoComplete = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDdtFechaFinal.Properties.Mask.BeepOnError = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.BeepOnError"), Boolean)
        Me.INDdtFechaFinal.Properties.Mask.EditMask = resources.GetString("INDdtFechaFinal.Properties.Mask.EditMask")
        Me.INDdtFechaFinal.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDdtFechaFinal.Properties.Mask.MaskType = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDdtFechaFinal.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.PlaceHolder"), Char)
        Me.INDdtFechaFinal.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDdtFechaFinal.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDdtFechaFinal.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDdtFechaFinal.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDdtFechaFinal.Properties.NullValuePrompt = resources.GetString("INDdtFechaFinal.Properties.NullValuePrompt")
        Me.INDdtFechaFinal.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDdtFechaFinal.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDdtFechaFinal.StyleController = Me.INDlyPoliza
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdtFechaFinal, 0)
        '
        'INDdtFechaInicio
        '
        resources.ApplyResources(Me.INDdtFechaInicio, "INDdtFechaInicio")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdtFechaInicio, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdtFechaInicio, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdtFechaInicio, False)
        Me.INDdtFechaInicio.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDdtFechaInicio, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdtFechaInicio, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdtFechaInicio.Name = "INDdtFechaInicio"
        Me.INDdtFechaInicio.Properties.AccessibleDescription = resources.GetString("INDdtFechaInicio.Properties.AccessibleDescription")
        Me.INDdtFechaInicio.Properties.AccessibleName = resources.GetString("INDdtFechaInicio.Properties.AccessibleName")
        Me.INDdtFechaInicio.Properties.Appearance.BackColor = CType(resources.GetObject("INDdtFechaInicio.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDdtFechaInicio.Properties.Appearance.Font = CType(resources.GetObject("INDdtFechaInicio.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDdtFechaInicio.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDdtFechaInicio.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDdtFechaInicio.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDdtFechaInicio.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDdtFechaInicio.Properties.Appearance.GradientMode = CType(resources.GetObject("INDdtFechaInicio.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDdtFechaInicio.Properties.Appearance.Image = CType(resources.GetObject("INDdtFechaInicio.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDdtFechaInicio.Properties.Appearance.Options.UseBackColor = True
        Me.INDdtFechaInicio.Properties.Appearance.Options.UseFont = True
        Me.INDdtFechaInicio.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDdtFechaInicio.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDdtFechaInicio.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdtFechaInicio.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdtFechaInicio.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdtFechaInicio.Properties.AutoHeight = CType(resources.GetObject("INDdtFechaInicio.Properties.AutoHeight"), Boolean)
        Me.INDdtFechaInicio.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDdtFechaInicio.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.AccessibleDescription = resources.GetString("INDdtFechaInicio.Properties.CalendarTimeProperties.AccessibleDescription")
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.AccessibleName = resources.GetString("INDdtFechaInicio.Properties.CalendarTimeProperties.AccessibleName")
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.AutoHeight = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.AutoHeight"), Boolean)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.AutoComplete = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.BeepOnError = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.BeepOnError"), Boolean)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.EditMask = resources.GetString("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.EditMask")
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.MaskType = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.PlaceHolder = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.PlaceHolder"), Char)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.SaveLiteral = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.SaveLiteral"), Boolean)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.NullValuePrompt = resources.GetString("INDdtFechaInicio.Properties.CalendarTimeProperties.NullValuePrompt")
        Me.INDdtFechaInicio.Properties.CalendarTimeProperties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDdtFechaInicio.Properties.CalendarTimeProperties.NullValuePromptShowForEmptyVal" & _
        "ue"), Boolean)
        Me.INDdtFechaInicio.Properties.Mask.AutoComplete = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDdtFechaInicio.Properties.Mask.BeepOnError = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.BeepOnError"), Boolean)
        Me.INDdtFechaInicio.Properties.Mask.EditMask = resources.GetString("INDdtFechaInicio.Properties.Mask.EditMask")
        Me.INDdtFechaInicio.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDdtFechaInicio.Properties.Mask.MaskType = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDdtFechaInicio.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.PlaceHolder"), Char)
        Me.INDdtFechaInicio.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDdtFechaInicio.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDdtFechaInicio.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDdtFechaInicio.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDdtFechaInicio.Properties.NullValuePrompt = resources.GetString("INDdtFechaInicio.Properties.NullValuePrompt")
        Me.INDdtFechaInicio.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDdtFechaInicio.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDdtFechaInicio.StyleController = Me.INDlyPoliza
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdtFechaInicio, 0)
        '
        'INDtxtName
        '
        resources.ApplyResources(Me.INDtxtName, "INDtxtName")
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtName, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtName, True)
        Me.INDtxtName.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtName, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
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
        Me.INDtxtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.INDtxtName.Properties.Mask.AutoComplete = CType(resources.GetObject("INDtxtName.Properties.Mask.AutoComplete"), DevExpress.XtraEditors.Mask.AutoCompleteType)
        Me.INDtxtName.Properties.Mask.BeepOnError = CType(resources.GetObject("INDtxtName.Properties.Mask.BeepOnError"), Boolean)
        Me.INDtxtName.Properties.Mask.EditMask = resources.GetString("INDtxtName.Properties.Mask.EditMask")
        Me.INDtxtName.Properties.Mask.IgnoreMaskBlank = CType(resources.GetObject("INDtxtName.Properties.Mask.IgnoreMaskBlank"), Boolean)
        Me.INDtxtName.Properties.Mask.MaskType = CType(resources.GetObject("INDtxtName.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDtxtName.Properties.Mask.PlaceHolder = CType(resources.GetObject("INDtxtName.Properties.Mask.PlaceHolder"), Char)
        Me.INDtxtName.Properties.Mask.SaveLiteral = CType(resources.GetObject("INDtxtName.Properties.Mask.SaveLiteral"), Boolean)
        Me.INDtxtName.Properties.Mask.ShowPlaceHolders = CType(resources.GetObject("INDtxtName.Properties.Mask.ShowPlaceHolders"), Boolean)
        Me.INDtxtName.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDtxtName.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDtxtName.Properties.MaxLength = 60
        Me.INDtxtName.Properties.NullValuePrompt = resources.GetString("INDtxtName.Properties.NullValuePrompt")
        Me.INDtxtName.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDtxtName.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDtxtName.StyleController = Me.INDlyPoliza
        Me.INDtxtName.Tag = ""
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtName, 0)
        '
        'INDglAseguradora
        '
        resources.ApplyResources(Me.INDglAseguradora, "INDglAseguradora")
        resources.ApplyResources(AppearanceObject1, "AppearanceObject1")
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDglAseguradora, AppearanceObject1)
        resources.ApplyResources(AppearanceObject2, "AppearanceObject2")
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDglAseguradora, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDglAseguradora, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglAseguradora, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglAseguradora, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDglAseguradora, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDglAseguradora, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDglAseguradora, False)
        Me.INDglAseguradora.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDglAseguradora, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDglAseguradora, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDglAseguradora, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDglAseguradora, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglAseguradora.Name = "INDglAseguradora"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDglAseguradora, True)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDglAseguradora, True)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDglAseguradora, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDglAseguradora, True)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDglAseguradora, True)
        Me.INDglAseguradora.Properties.AccessibleDescription = resources.GetString("INDglAseguradora.Properties.AccessibleDescription")
        Me.INDglAseguradora.Properties.AccessibleName = resources.GetString("INDglAseguradora.Properties.AccessibleName")
        Me.INDglAseguradora.Properties.Appearance.BackColor = CType(resources.GetObject("INDglAseguradora.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglAseguradora.Properties.Appearance.Font = CType(resources.GetObject("INDglAseguradora.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglAseguradora.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDglAseguradora.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDglAseguradora.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDglAseguradora.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglAseguradora.Properties.Appearance.GradientMode = CType(resources.GetObject("INDglAseguradora.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglAseguradora.Properties.Appearance.Image = CType(resources.GetObject("INDglAseguradora.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDglAseguradora.Properties.Appearance.Options.UseBackColor = True
        Me.INDglAseguradora.Properties.Appearance.Options.UseFont = True
        Me.INDglAseguradora.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglAseguradora.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDglAseguradora.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglAseguradora.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDglAseguradora.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglAseguradora.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglAseguradora.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDglAseguradora.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDglAseguradora.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglAseguradora.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglAseguradora.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglAseguradora.Properties.AutoHeight = CType(resources.GetObject("INDglAseguradora.Properties.AutoHeight"), Boolean)
        Me.INDglAseguradora.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglAseguradora.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglAseguradora.Properties.DisplayMember = "Name"
        Me.INDglAseguradora.Properties.NullText = resources.GetString("INDglAseguradora.Properties.NullText")
        Me.INDglAseguradora.Properties.NullValuePrompt = resources.GetString("INDglAseguradora.Properties.NullValuePrompt")
        Me.INDglAseguradora.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDglAseguradora.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDglAseguradora.Properties.PopupSizeable = False
        Me.INDglAseguradora.Properties.ShowFooter = False
        Me.INDglAseguradora.Properties.ValueMember = "Id"
        Me.INDglAseguradora.Properties.View = Me.SearchLookUpEdit1View
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDglAseguradora, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDglAseguradora, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDglAseguradora, False)
        Me.INDglAseguradora.StyleController = Me.INDlyPoliza
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDglAseguradora, "557")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglAseguradora, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDglAseguradora, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDglAseguradora, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDglAseguradora, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.GradientMode = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Image = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Image = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.Row.FontSizeDelta = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.FontSizeDelta"), Integer)
        Me.SearchLookUpEdit1View.Appearance.Row.FontStyleDelta = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.SearchLookUpEdit1View.Appearance.Row.GradientMode = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.SearchLookUpEdit1View.Appearance.Row.Image = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Image"), System.Drawing.Image)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.SearchLookUpEdit1View, "SearchLookUpEdit1View")
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn3, Me.GridColumn4})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn3
        '
        resources.ApplyResources(Me.GridColumn3, "GridColumn3")
        Me.GridColumn3.FieldName = "Nit"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'INDglPolizaType
        '
        resources.ApplyResources(Me.INDglPolizaType, "INDglPolizaType")
        resources.ApplyResources(AppearanceObject3, "AppearanceObject3")
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDglPolizaType, AppearanceObject3)
        resources.ApplyResources(AppearanceObject4, "AppearanceObject4")
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDglPolizaType, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDglPolizaType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglPolizaType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglPolizaType, True)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDglPolizaType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDglPolizaType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDglPolizaType, False)
        Me.INDglPolizaType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDglPolizaType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDglPolizaType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDglPolizaType, False)
        Me.IndigoTextEdit1.SetMascara(Me.INDglPolizaType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglPolizaType.Name = "INDglPolizaType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDglPolizaType, True)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDglPolizaType, True)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDglPolizaType, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDglPolizaType, True)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDglPolizaType, True)
        Me.INDglPolizaType.Properties.AccessibleDescription = resources.GetString("INDglPolizaType.Properties.AccessibleDescription")
        Me.INDglPolizaType.Properties.AccessibleName = resources.GetString("INDglPolizaType.Properties.AccessibleName")
        Me.INDglPolizaType.Properties.Appearance.BackColor = CType(resources.GetObject("INDglPolizaType.Properties.Appearance.BackColor"), System.Drawing.Color)
        Me.INDglPolizaType.Properties.Appearance.Font = CType(resources.GetObject("INDglPolizaType.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDglPolizaType.Properties.Appearance.FontSizeDelta = CType(resources.GetObject("INDglPolizaType.Properties.Appearance.FontSizeDelta"), Integer)
        Me.INDglPolizaType.Properties.Appearance.FontStyleDelta = CType(resources.GetObject("INDglPolizaType.Properties.Appearance.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglPolizaType.Properties.Appearance.GradientMode = CType(resources.GetObject("INDglPolizaType.Properties.Appearance.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglPolizaType.Properties.Appearance.Image = CType(resources.GetObject("INDglPolizaType.Properties.Appearance.Image"), System.Drawing.Image)
        Me.INDglPolizaType.Properties.Appearance.Options.UseBackColor = True
        Me.INDglPolizaType.Properties.Appearance.Options.UseFont = True
        Me.INDglPolizaType.Properties.AppearanceFocused.BackColor = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.BackColor"), System.Drawing.Color)
        Me.INDglPolizaType.Properties.AppearanceFocused.BorderColor = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.BorderColor"), System.Drawing.Color)
        Me.INDglPolizaType.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDglPolizaType.Properties.AppearanceFocused.FontSizeDelta = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.FontSizeDelta"), Integer)
        Me.INDglPolizaType.Properties.AppearanceFocused.FontStyleDelta = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.FontStyleDelta"), System.Drawing.FontStyle)
        Me.INDglPolizaType.Properties.AppearanceFocused.GradientMode = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.INDglPolizaType.Properties.AppearanceFocused.Image = CType(resources.GetObject("INDglPolizaType.Properties.AppearanceFocused.Image"), System.Drawing.Image)
        Me.INDglPolizaType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglPolizaType.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglPolizaType.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglPolizaType.Properties.AutoHeight = CType(resources.GetObject("INDglPolizaType.Properties.AutoHeight"), Boolean)
        Me.INDglPolizaType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDglPolizaType.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDglPolizaType.Properties.DisplayMember = "Name"
        Me.INDglPolizaType.Properties.NullText = resources.GetString("INDglPolizaType.Properties.NullText")
        Me.INDglPolizaType.Properties.NullValuePrompt = resources.GetString("INDglPolizaType.Properties.NullValuePrompt")
        Me.INDglPolizaType.Properties.NullValuePromptShowForEmptyValue = CType(resources.GetObject("INDglPolizaType.Properties.NullValuePromptShowForEmptyValue"), Boolean)
        Me.INDglPolizaType.Properties.PopupSizeable = False
        Me.INDglPolizaType.Properties.ShowFooter = False
        Me.INDglPolizaType.Properties.ValueMember = "Id"
        Me.INDglPolizaType.Properties.View = Me.GridView1
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDglPolizaType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDglPolizaType, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDglPolizaType, False)
        Me.INDglPolizaType.StyleController = Me.INDlyPoliza
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDglPolizaType, "560")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglPolizaType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDglPolizaType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDglPolizaType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDglPolizaType, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.GroupRow.Font = CType(resources.GetObject("GridView1.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.GroupRow.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.GroupRow.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.GroupRow.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.GroupRow.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.GroupRow.ForeColor"), System.Drawing.Color)
        Me.GridView1.Appearance.GroupRow.GradientMode = CType(resources.GetObject("GridView1.Appearance.GroupRow.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.GroupRow.Image = CType(resources.GetObject("GridView1.Appearance.GroupRow.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.HeaderPanel.Font = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.HeaderPanel.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.HeaderPanel.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.FontStyleDelta"), System.Drawing.FontStyle)
        'Cadena reemplazada... CType(resources.GetObject("GridView1.Appearance.HeaderPanel.ForeColor"), System.Drawing.Color)
        Me.GridView1.Appearance.HeaderPanel.GradientMode = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.HeaderPanel.Image = CType(resources.GetObject("GridView1.Appearance.HeaderPanel.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        'Cadena reemplazada... True
        Me.GridView1.Appearance.Row.Font = CType(resources.GetObject("GridView1.Appearance.Row.Font"), System.Drawing.Font)
        Me.GridView1.Appearance.Row.FontSizeDelta = CType(resources.GetObject("GridView1.Appearance.Row.FontSizeDelta"), Integer)
        Me.GridView1.Appearance.Row.FontStyleDelta = CType(resources.GetObject("GridView1.Appearance.Row.FontStyleDelta"), System.Drawing.FontStyle)
        Me.GridView1.Appearance.Row.GradientMode = CType(resources.GetObject("GridView1.Appearance.Row.GradientMode"), System.Drawing.Drawing2D.LinearGradientMode)
        Me.GridView1.Appearance.Row.Image = CType(resources.GetObject("GridView1.Appearance.Row.Image"), System.Drawing.Image)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        resources.ApplyResources(Me.GridView1, "GridView1")
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem()})
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
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyGrPoliza})
        Me.LayoutControlGroup1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(804, 602)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDlyGrPoliza
        '
        resources.ApplyResources(Me.INDlyGrPoliza, "INDlyGrPoliza")
        Me.INDlyGrPoliza.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemCode, Me.INDlyItemName, Me.INDlyItemPolizaType, Me.INDlyItemAseguradora, Me.INDlyItemFechaInicio, Me.INDlyItemObjeto, Me.INDlyItemFechaFinal})
        Me.INDlyGrPoliza.Location = New System.Drawing.Point(0, 0)
        Me.INDlyGrPoliza.Name = "INDlyGrPoliza"
        Me.INDlyGrPoliza.Size = New System.Drawing.Size(804, 602)
        '
        'INDlyItemCode
        '
        Me.INDlyItemCode.AllowHide = False
        resources.ApplyResources(Me.INDlyItemCode, "INDlyItemCode")
        Me.INDlyItemCode.Control = Me.INDbteCode
        Me.INDlyItemCode.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemCode.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemCode.Name = "INDlyItemCode"
        Me.INDlyItemCode.ShowInCustomizationForm = False
        Me.INDlyItemCode.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemCode.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCode.Tag = "Code"
        Me.INDlyItemCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCode.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemCode.TextToControlDistance = 12
        '
        'INDlyItemName
        '
        Me.INDlyItemName.AllowHide = False
        resources.ApplyResources(Me.INDlyItemName, "INDlyItemName")
        Me.INDlyItemName.Control = Me.INDtxtName
        Me.INDlyItemName.Location = New System.Drawing.Point(0, 36)
        Me.INDlyItemName.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemName.Name = "INDlyItemName"
        Me.INDlyItemName.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 3, 2, 2)
        Me.INDlyItemName.ShowInCustomizationForm = False
        Me.INDlyItemName.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemName.Tag = "Name"
        Me.INDlyItemName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemName.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemName.TextToControlDistance = 12
        '
        'INDlyItemPolizaType
        '
        resources.ApplyResources(Me.INDlyItemPolizaType, "INDlyItemPolizaType")
        Me.INDlyItemPolizaType.Control = Me.INDglPolizaType
        Me.INDlyItemPolizaType.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemPolizaType.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemPolizaType.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemPolizaType.Name = "INDlyItemPolizaType"
        Me.INDlyItemPolizaType.ShowInCustomizationForm = False
        Me.INDlyItemPolizaType.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemPolizaType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPolizaType.Tag = "IdPolizaType"
        Me.INDlyItemPolizaType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPolizaType.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemPolizaType.TextToControlDistance = 12
        '
        'INDlyItemAseguradora
        '
        resources.ApplyResources(Me.INDlyItemAseguradora, "INDlyItemAseguradora")
        Me.INDlyItemAseguradora.Control = Me.INDglAseguradora
        Me.INDlyItemAseguradora.Location = New System.Drawing.Point(0, 72)
        Me.INDlyItemAseguradora.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemAseguradora.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemAseguradora.Name = "INDlyItemAseguradora"
        Me.INDlyItemAseguradora.ShowInCustomizationForm = False
        Me.INDlyItemAseguradora.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemAseguradora.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAseguradora.Tag = "InsuranceId"
        Me.INDlyItemAseguradora.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAseguradora.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemAseguradora.TextToControlDistance = 12
        '
        'INDlyItemFechaInicio
        '
        resources.ApplyResources(Me.INDlyItemFechaInicio, "INDlyItemFechaInicio")
        Me.INDlyItemFechaInicio.Control = Me.INDdtFechaInicio
        Me.INDlyItemFechaInicio.Location = New System.Drawing.Point(0, 108)
        Me.INDlyItemFechaInicio.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemFechaInicio.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemFechaInicio.Name = "INDlyItemFechaInicio"
        Me.INDlyItemFechaInicio.ShowInCustomizationForm = False
        Me.INDlyItemFechaInicio.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemFechaInicio.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFechaInicio.Tag = "InitialDate"
        Me.INDlyItemFechaInicio.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFechaInicio.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemFechaInicio.TextToControlDistance = 12
        '
        'INDlyItemObjeto
        '
        resources.ApplyResources(Me.INDlyItemObjeto, "INDlyItemObjeto")
        Me.INDlyItemObjeto.Control = Me.INDmeObjeto
        Me.INDlyItemObjeto.Location = New System.Drawing.Point(0, 216)
        Me.INDlyItemObjeto.MaxSize = New System.Drawing.Size(420, 100)
        Me.INDlyItemObjeto.MinSize = New System.Drawing.Size(420, 100)
        Me.INDlyItemObjeto.Name = "INDlyItemObjeto"
        Me.INDlyItemObjeto.ShowInCustomizationForm = False
        Me.INDlyItemObjeto.Size = New System.Drawing.Size(780, 327)
        Me.INDlyItemObjeto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObjeto.Tag = "Object"
        Me.INDlyItemObjeto.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObjeto.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemObjeto.TextToControlDistance = 12
        '
        'INDlyItemFechaFinal
        '
        resources.ApplyResources(Me.INDlyItemFechaFinal, "INDlyItemFechaFinal")
        Me.INDlyItemFechaFinal.Control = Me.INDdtFechaFinal
        Me.INDlyItemFechaFinal.Location = New System.Drawing.Point(0, 144)
        Me.INDlyItemFechaFinal.MaxSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemFechaFinal.MinSize = New System.Drawing.Size(420, 36)
        Me.INDlyItemFechaFinal.Name = "INDlyItemFechaFinal"
        Me.INDlyItemFechaFinal.ShowInCustomizationForm = False
        Me.INDlyItemFechaFinal.Size = New System.Drawing.Size(780, 36)
        Me.INDlyItemFechaFinal.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemFechaFinal.Tag = "EndDate"
        Me.INDlyItemFechaFinal.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemFechaFinal.TextSize = New System.Drawing.Size(160, 21)
        Me.INDlyItemFechaFinal.TextToControlDistance = 12
        '
        'CtrNavigationControl1
        '
        resources.ApplyResources(Me.CtrNavigationControl1, "CtrNavigationControl1")
        Me.CtrNavigationControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControl1.LayoutControl = Me.INDlyPoliza
        Me.CtrNavigationControl1.Name = "CtrNavigationControl1"
        Me.CtrNavigationControl1.UseDisabledStatePainter = False
        '
        'FrmPoliza
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Name = "FrmPoliza"
        Me.Opacity = 1.0R
        Me.Tag = "559"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyPoliza.ResumeLayout(False)
        CType(Me.INDmeObjeto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdtFechaFinal.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdtFechaFinal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdtFechaInicio.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdtFechaInicio.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglAseguradora.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglPolizaType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyGrPoliza, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPolizaType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAseguradora, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFechaInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObjeto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemFechaFinal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents INDlyPoliza As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDtxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDbteCode As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemCode As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemName As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyGrPoliza As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDlyItemPolizaType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemAseguradora As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents INDdtFechaFinal As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDdtFechaInicio As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemFechaInicio As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemFechaFinal As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmeObjeto As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObjeto As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControl1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDglAseguradora As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDglPolizaType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
End Class
