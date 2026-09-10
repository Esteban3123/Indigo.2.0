Imports Presentation.Glosas.MVP

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GeneralConciliation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralConciliation))
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDTxtValuePending = New DevExpress.XtraEditors.TextEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDSleResponseHierarchyId = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDMeComment = New DevExpress.XtraEditors.MemoEdit()
        Me.INDTxtValueAcceptedIPS = New DevExpress.XtraEditors.TextEdit()
        Me.INDTxtValueAcceptedEAPB = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLciValueAcceptedIPS = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciComment = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciResponseHierarchyId = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValueAcceptedEAPB = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLciValuePending = New DevExpress.XtraLayout.LayoutControlItem()
        Me.PasteItem1 = New DevExpress.XtraRichEdit.UI.PasteItem()
        Me.CutItem1 = New DevExpress.XtraRichEdit.UI.CutItem()
        Me.CopyItem1 = New DevExpress.XtraRichEdit.UI.CopyItem()
        Me.PasteSpecialItem1 = New DevExpress.XtraRichEdit.UI.PasteSpecialItem()
        Me.ChangeFontNameItem1 = New DevExpress.XtraRichEdit.UI.ChangeFontNameItem()
        Me.RepositoryItemFontEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemFontEdit()
        Me.ChangeFontSizeItem1 = New DevExpress.XtraRichEdit.UI.ChangeFontSizeItem()
        Me.RepositoryItemRichEditFontSizeEdit1 = New DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit()
        Me.FontSizeIncreaseItem1 = New DevExpress.XtraRichEdit.UI.FontSizeIncreaseItem()
        Me.FontSizeDecreaseItem1 = New DevExpress.XtraRichEdit.UI.FontSizeDecreaseItem()
        Me.ToggleFontBoldItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontBoldItem()
        Me.ToggleFontItalicItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontItalicItem()
        Me.ToggleFontUnderlineItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem()
        Me.ToggleFontDoubleUnderlineItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem()
        Me.ToggleFontStrikeoutItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem()
        Me.ToggleFontDoubleStrikeoutItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontDoubleStrikeoutItem()
        Me.ToggleFontSuperscriptItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem()
        Me.ToggleFontSubscriptItem1 = New DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem()
        Me.ChangeFontColorItem1 = New DevExpress.XtraRichEdit.UI.ChangeFontColorItem()
        Me.ChangeFontBackColorItem1 = New DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem()
        Me.ChangeTextCaseItem1 = New DevExpress.XtraRichEdit.UI.ChangeTextCaseItem()
        Me.MakeTextUpperCaseItem1 = New DevExpress.XtraRichEdit.UI.MakeTextUpperCaseItem()
        Me.MakeTextLowerCaseItem1 = New DevExpress.XtraRichEdit.UI.MakeTextLowerCaseItem()
        Me.ToggleTextCaseItem1 = New DevExpress.XtraRichEdit.UI.ToggleTextCaseItem()
        Me.ClearFormattingItem1 = New DevExpress.XtraRichEdit.UI.ClearFormattingItem()
        Me.ShowFontFormItem1 = New DevExpress.XtraRichEdit.UI.ShowFontFormItem()
        Me.ToggleBulletedListItem1 = New DevExpress.XtraRichEdit.UI.ToggleBulletedListItem()
        Me.ToggleNumberingListItem1 = New DevExpress.XtraRichEdit.UI.ToggleNumberingListItem()
        Me.ToggleMultiLevelListItem1 = New DevExpress.XtraRichEdit.UI.ToggleMultiLevelListItem()
        Me.DecreaseIndentItem1 = New DevExpress.XtraRichEdit.UI.DecreaseIndentItem()
        Me.IncreaseIndentItem1 = New DevExpress.XtraRichEdit.UI.IncreaseIndentItem()
        Me.ToggleParagraphAlignmentLeftItem1 = New DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem()
        Me.ToggleParagraphAlignmentCenterItem1 = New DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem()
        Me.ToggleParagraphAlignmentRightItem1 = New DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem()
        Me.ToggleParagraphAlignmentJustifyItem1 = New DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem()
        Me.ToggleShowWhitespaceItem1 = New DevExpress.XtraRichEdit.UI.ToggleShowWhitespaceItem()
        Me.ChangeParagraphLineSpacingItem1 = New DevExpress.XtraRichEdit.UI.ChangeParagraphLineSpacingItem()
        Me.SetSingleParagraphSpacingItem1 = New DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem()
        Me.SetSesquialteralParagraphSpacingItem1 = New DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem()
        Me.SetDoubleParagraphSpacingItem1 = New DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem()
        Me.ShowLineSpacingFormItem1 = New DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem()
        Me.AddSpacingBeforeParagraphItem1 = New DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem()
        Me.RemoveSpacingBeforeParagraphItem1 = New DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem()
        Me.AddSpacingAfterParagraphItem1 = New DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem()
        Me.RemoveSpacingAfterParagraphItem1 = New DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem()
        Me.ChangeParagraphBackColorItem1 = New DevExpress.XtraRichEdit.UI.ChangeParagraphBackColorItem()
        Me.ShowParagraphFormItem1 = New DevExpress.XtraRichEdit.UI.ShowParagraphFormItem()
        Me.ChangeStyleItem1 = New DevExpress.XtraRichEdit.UI.ChangeStyleItem()
        Me.RepositoryItemRichEditStyleEdit1 = New DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit()
        Me.ShowEditStyleFormItem1 = New DevExpress.XtraRichEdit.UI.ShowEditStyleFormItem()
        Me.FindItem1 = New DevExpress.XtraRichEdit.UI.FindItem()
        Me.ReplaceItem1 = New DevExpress.XtraRichEdit.UI.ReplaceItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.RichEditBarController1 = New DevExpress.XtraRichEdit.UI.RichEditBarController(Me.components)
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDTxtValuePending.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleResponseHierarchyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDMeComment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValueAcceptedIPS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDTxtValueAcceptedEAPB.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValueAcceptedIPS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciComment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciResponseHierarchyId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValueAcceptedEAPB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLciValuePending, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFontEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRichEditFontSizeEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRichEditStyleEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RichEditBarController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
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
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDTxtValuePending)
        Me.LayoutControl1.Controls.Add(Me.INDSleResponseHierarchyId)
        Me.LayoutControl1.Controls.Add(Me.INDMeComment)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValueAcceptedIPS)
        Me.LayoutControl1.Controls.Add(Me.INDTxtValueAcceptedEAPB)
        resources.ApplyResources(Me.LayoutControl1, "LayoutControl1")
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        '
        'INDTxtValuePending
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValuePending, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValuePending, True)
        resources.ApplyResources(Me.INDTxtValuePending, "INDTxtValuePending")
        Me.INDTxtValuePending.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValuePending, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtValuePending.MenuManager = Me.BarManager1
        Me.INDTxtValuePending.Name = "INDTxtValuePending"
        Me.INDTxtValuePending.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValuePending.Properties.Appearance.Font = CType(resources.GetObject("INDTxtValuePending.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtValuePending.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValuePending.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValuePending.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValuePending.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValuePending.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtValuePending.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtValuePending.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtValuePending.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtValuePending.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtValuePending.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtValuePending.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtValuePending.Properties.DisplayFormat.FormatString = "c"
        Me.INDTxtValuePending.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDTxtValuePending.Properties.Mask.EditMask = resources.GetString("INDTxtValuePending.Properties.Mask.EditMask")
        Me.INDTxtValuePending.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtValuePending.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtValuePending.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtValuePending.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtValuePending.Properties.MaxLength = 15
        Me.INDTxtValuePending.Properties.ReadOnly = True
        Me.INDTxtValuePending.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValuePending, 0)
        '
        'BarManager1
        '
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.Form = Me
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        resources.ApplyResources(Me.barDockControlTop, "barDockControlTop")
        Me.barDockControlTop.Manager = Me.BarManager1
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        resources.ApplyResources(Me.barDockControlBottom, "barDockControlBottom")
        Me.barDockControlBottom.Manager = Me.BarManager1
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        resources.ApplyResources(Me.barDockControlLeft, "barDockControlLeft")
        Me.barDockControlLeft.Manager = Me.BarManager1
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        resources.ApplyResources(Me.barDockControlRight, "barDockControlRight")
        Me.barDockControlRight.Manager = Me.BarManager1
        '
        'INDSleResponseHierarchyId
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleResponseHierarchyId, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleResponseHierarchyId, True)
        Me.INDSleResponseHierarchyId.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDSleResponseHierarchyId, "INDSleResponseHierarchyId")
        Me.IndigoTextEdit1.SetMascara(Me.INDSleResponseHierarchyId, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleResponseHierarchyId.MenuManager = Me.BarManager1
        Me.INDSleResponseHierarchyId.Name = "INDSleResponseHierarchyId"
        Me.INDSleResponseHierarchyId.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDSleResponseHierarchyId.Properties.Appearance.Font = CType(resources.GetObject("INDSleResponseHierarchyId.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDSleResponseHierarchyId.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleResponseHierarchyId.Properties.Appearance.Options.UseFont = True
        Me.INDSleResponseHierarchyId.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDSleResponseHierarchyId.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDSleResponseHierarchyId.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSleResponseHierarchyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("INDSleResponseHierarchyId.Properties.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.INDSleResponseHierarchyId.Properties.DisplayMember = "CodeName"
        Me.INDSleResponseHierarchyId.Properties.NullText = resources.GetString("INDSleResponseHierarchyId.Properties.NullText")
        Me.INDSleResponseHierarchyId.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleResponseHierarchyId.Properties.ValueMember = "Id"
        Me.INDSleResponseHierarchyId.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleResponseHierarchyId, 0)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.FocusedRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.GroupRow.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.HeaderPanel.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = CType(resources.GetObject("SearchLookUpEdit1View.Appearance.Row.Font"), System.Drawing.Font)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
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
        Me.GridColumn3.FieldName = "Code"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowFocus = False
        '
        'GridColumn4
        '
        resources.ApplyResources(Me.GridColumn4, "GridColumn4")
        Me.GridColumn4.FieldName = "Name"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        '
        'INDMeComment
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDMeComment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDMeComment, True)
        Me.INDMeComment.EnterMoveNextControl = True
        resources.ApplyResources(Me.INDMeComment, "INDMeComment")
        Me.IndigoTextEdit1.SetMascara(Me.INDMeComment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDMeComment.MenuManager = Me.BarManager1
        Me.INDMeComment.Name = "INDMeComment"
        Me.INDMeComment.Properties.Appearance.BackColor = System.Drawing.Color.MistyRose
        Me.INDMeComment.Properties.Appearance.Font = CType(resources.GetObject("INDMeComment.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDMeComment.Properties.Appearance.Options.UseBackColor = True
        Me.INDMeComment.Properties.Appearance.Options.UseFont = True
        Me.INDMeComment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDMeComment.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDMeComment.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDMeComment.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDMeComment.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDMeComment.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDMeComment.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDMeComment.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDMeComment, 0)
        '
        'INDTxtValueAcceptedIPS
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValueAcceptedIPS, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValueAcceptedIPS, True)
        resources.ApplyResources(Me.INDTxtValueAcceptedIPS, "INDTxtValueAcceptedIPS")
        Me.INDTxtValueAcceptedIPS.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValueAcceptedIPS, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtValueAcceptedIPS.MenuManager = Me.BarManager1
        Me.INDTxtValueAcceptedIPS.Name = "INDTxtValueAcceptedIPS"
        Me.INDTxtValueAcceptedIPS.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValueAcceptedIPS.Properties.Appearance.Font = CType(resources.GetObject("INDTxtValueAcceptedIPS.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtValueAcceptedIPS.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValueAcceptedIPS.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValueAcceptedIPS.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValueAcceptedIPS.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValueAcceptedIPS.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtValueAcceptedIPS.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtValueAcceptedIPS.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtValueAcceptedIPS.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtValueAcceptedIPS.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtValueAcceptedIPS.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtValueAcceptedIPS.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtValueAcceptedIPS.Properties.DisplayFormat.FormatString = "c"
        Me.INDTxtValueAcceptedIPS.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDTxtValueAcceptedIPS.Properties.Mask.EditMask = resources.GetString("INDTxtValueAcceptedIPS.Properties.Mask.EditMask")
        Me.INDTxtValueAcceptedIPS.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtValueAcceptedIPS.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtValueAcceptedIPS.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtValueAcceptedIPS.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtValueAcceptedIPS.Properties.MaxLength = 15
        Me.INDTxtValueAcceptedIPS.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValueAcceptedIPS, 1)
        '
        'INDTxtValueAcceptedEAPB
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDTxtValueAcceptedEAPB, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDTxtValueAcceptedEAPB, True)
        resources.ApplyResources(Me.INDTxtValueAcceptedEAPB, "INDTxtValueAcceptedEAPB")
        Me.INDTxtValueAcceptedEAPB.EnterMoveNextControl = True
        Me.IndigoTextEdit1.SetMascara(Me.INDTxtValueAcceptedEAPB, Presentation.Controls.IndigoTextEdit.EMask.MonedaDecimales)
        Me.INDTxtValueAcceptedEAPB.MenuManager = Me.BarManager1
        Me.INDTxtValueAcceptedEAPB.Name = "INDTxtValueAcceptedEAPB"
        Me.INDTxtValueAcceptedEAPB.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDTxtValueAcceptedEAPB.Properties.Appearance.Font = CType(resources.GetObject("INDTxtValueAcceptedEAPB.Properties.Appearance.Font"), System.Drawing.Font)
        Me.INDTxtValueAcceptedEAPB.Properties.Appearance.Options.UseBackColor = True
        Me.INDTxtValueAcceptedEAPB.Properties.Appearance.Options.UseFont = True
        Me.INDTxtValueAcceptedEAPB.Properties.Appearance.Options.UseTextOptions = True
        Me.INDTxtValueAcceptedEAPB.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.Font = CType(resources.GetObject("INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.Font"), System.Drawing.Font)
        Me.INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDTxtValueAcceptedEAPB.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDTxtValueAcceptedEAPB.Properties.DisplayFormat.FormatString = "c"
        Me.INDTxtValueAcceptedEAPB.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDTxtValueAcceptedEAPB.Properties.Mask.EditMask = resources.GetString("INDTxtValueAcceptedEAPB.Properties.Mask.EditMask")
        Me.INDTxtValueAcceptedEAPB.Properties.Mask.MaskType = CType(resources.GetObject("INDTxtValueAcceptedEAPB.Properties.Mask.MaskType"), DevExpress.XtraEditors.Mask.MaskType)
        Me.INDTxtValueAcceptedEAPB.Properties.Mask.UseMaskAsDisplayFormat = CType(resources.GetObject("INDTxtValueAcceptedEAPB.Properties.Mask.UseMaskAsDisplayFormat"), Boolean)
        Me.INDTxtValueAcceptedEAPB.Properties.MaxLength = 15
        Me.INDTxtValueAcceptedEAPB.StyleController = Me.LayoutControl1
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDTxtValueAcceptedEAPB, 0)
        '
        'LayoutControlGroup1
        '
        resources.ApplyResources(Me.LayoutControlGroup1, "LayoutControlGroup1")
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup2})
        Me.LayoutControlGroup1.Name = "Root"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(497, 415)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceGroup.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = CType(resources.GetObject("LayoutControlGroup2.AppearanceItemCaption.Font"), System.Drawing.Font)
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        resources.ApplyResources(Me.LayoutControlGroup2, "LayoutControlGroup2")
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLciValueAcceptedIPS, Me.INDLciComment, Me.INDLciResponseHierarchyId, Me.INDLciValueAcceptedEAPB, Me.INDLciValuePending})
        Me.LayoutControlGroup2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup2.Name = "LayoutControlGroup2"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(477, 395)
        '
        'INDLciValueAcceptedIPS
        '
        Me.INDLciValueAcceptedIPS.Control = Me.INDTxtValueAcceptedIPS
        resources.ApplyResources(Me.INDLciValueAcceptedIPS, "INDLciValueAcceptedIPS")
        Me.INDLciValueAcceptedIPS.Location = New System.Drawing.Point(0, 0)
        Me.INDLciValueAcceptedIPS.MaxSize = New System.Drawing.Size(453, 60)
        Me.INDLciValueAcceptedIPS.MinSize = New System.Drawing.Size(453, 60)
        Me.INDLciValueAcceptedIPS.Name = "INDLciValueAcceptedIPS"
        Me.INDLciValueAcceptedIPS.Size = New System.Drawing.Size(453, 60)
        Me.INDLciValueAcceptedIPS.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValueAcceptedIPS.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValueAcceptedIPS.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValueAcceptedIPS.TextSize = New System.Drawing.Size(131, 21)
        Me.INDLciValueAcceptedIPS.TextToControlDistance = 5
        '
        'INDLciComment
        '
        Me.INDLciComment.Control = Me.INDMeComment
        resources.ApplyResources(Me.INDLciComment, "INDLciComment")
        Me.INDLciComment.Location = New System.Drawing.Point(0, 240)
        Me.INDLciComment.MaxSize = New System.Drawing.Size(453, 120)
        Me.INDLciComment.MinSize = New System.Drawing.Size(453, 36)
        Me.INDLciComment.Name = "INDLciComment"
        Me.INDLciComment.Size = New System.Drawing.Size(453, 102)
        Me.INDLciComment.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciComment.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciComment.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciComment.TextSize = New System.Drawing.Size(131, 21)
        Me.INDLciComment.TextToControlDistance = 5
        '
        'INDLciResponseHierarchyId
        '
        Me.INDLciResponseHierarchyId.Control = Me.INDSleResponseHierarchyId
        Me.INDLciResponseHierarchyId.Location = New System.Drawing.Point(0, 180)
        Me.INDLciResponseHierarchyId.MaxSize = New System.Drawing.Size(453, 60)
        Me.INDLciResponseHierarchyId.MinSize = New System.Drawing.Size(453, 60)
        Me.INDLciResponseHierarchyId.Name = "INDLciResponseHierarchyId"
        Me.INDLciResponseHierarchyId.Size = New System.Drawing.Size(453, 60)
        Me.INDLciResponseHierarchyId.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciResponseHierarchyId, "INDLciResponseHierarchyId")
        Me.INDLciResponseHierarchyId.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciResponseHierarchyId.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciResponseHierarchyId.TextSize = New System.Drawing.Size(131, 21)
        Me.INDLciResponseHierarchyId.TextToControlDistance = 5
        Me.INDLciResponseHierarchyId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDLciValueAcceptedEAPB
        '
        Me.INDLciValueAcceptedEAPB.Control = Me.INDTxtValueAcceptedEAPB
        resources.ApplyResources(Me.INDLciValueAcceptedEAPB, "INDLciValueAcceptedEAPB")
        Me.INDLciValueAcceptedEAPB.Location = New System.Drawing.Point(0, 60)
        Me.INDLciValueAcceptedEAPB.MaxSize = New System.Drawing.Size(453, 60)
        Me.INDLciValueAcceptedEAPB.MinSize = New System.Drawing.Size(453, 60)
        Me.INDLciValueAcceptedEAPB.Name = "INDLciValueAcceptedEAPB"
        Me.INDLciValueAcceptedEAPB.Size = New System.Drawing.Size(453, 60)
        Me.INDLciValueAcceptedEAPB.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLciValueAcceptedEAPB.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValueAcceptedEAPB.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValueAcceptedEAPB.TextSize = New System.Drawing.Size(131, 21)
        Me.INDLciValueAcceptedEAPB.TextToControlDistance = 5
        '
        'INDLciValuePending
        '
        Me.INDLciValuePending.Control = Me.INDTxtValuePending
        Me.INDLciValuePending.Location = New System.Drawing.Point(0, 120)
        Me.INDLciValuePending.MaxSize = New System.Drawing.Size(453, 60)
        Me.INDLciValuePending.MinSize = New System.Drawing.Size(453, 60)
        Me.INDLciValuePending.Name = "INDLciValuePending"
        Me.INDLciValuePending.Size = New System.Drawing.Size(453, 60)
        Me.INDLciValuePending.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        resources.ApplyResources(Me.INDLciValuePending, "INDLciValuePending")
        Me.INDLciValuePending.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDLciValuePending.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDLciValuePending.TextSize = New System.Drawing.Size(131, 21)
        Me.INDLciValuePending.TextToControlDistance = 5
        '
        'PasteItem1
        '
        Me.PasteItem1.Enabled = False
        Me.PasteItem1.Id = 0
        Me.PasteItem1.Name = "PasteItem1"
        '
        'CutItem1
        '
        Me.CutItem1.Enabled = False
        Me.CutItem1.Id = 1
        Me.CutItem1.Name = "CutItem1"
        '
        'CopyItem1
        '
        Me.CopyItem1.Enabled = False
        Me.CopyItem1.Id = 2
        Me.CopyItem1.Name = "CopyItem1"
        '
        'PasteSpecialItem1
        '
        Me.PasteSpecialItem1.Enabled = False
        Me.PasteSpecialItem1.Id = 3
        Me.PasteSpecialItem1.Name = "PasteSpecialItem1"
        '
        'ChangeFontNameItem1
        '
        Me.ChangeFontNameItem1.Edit = Me.RepositoryItemFontEdit1
        Me.ChangeFontNameItem1.Enabled = False
        Me.ChangeFontNameItem1.Id = 4
        Me.ChangeFontNameItem1.Name = "ChangeFontNameItem1"
        '
        'RepositoryItemFontEdit1
        '
        resources.ApplyResources(Me.RepositoryItemFontEdit1, "RepositoryItemFontEdit1")
        Me.RepositoryItemFontEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemFontEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemFontEdit1.Name = "RepositoryItemFontEdit1"
        '
        'ChangeFontSizeItem1
        '
        Me.ChangeFontSizeItem1.Edit = Me.RepositoryItemRichEditFontSizeEdit1
        Me.ChangeFontSizeItem1.Enabled = False
        Me.ChangeFontSizeItem1.Id = 5
        Me.ChangeFontSizeItem1.Name = "ChangeFontSizeItem1"
        '
        'RepositoryItemRichEditFontSizeEdit1
        '
        resources.ApplyResources(Me.RepositoryItemRichEditFontSizeEdit1, "RepositoryItemRichEditFontSizeEdit1")
        Me.RepositoryItemRichEditFontSizeEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemRichEditFontSizeEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemRichEditFontSizeEdit1.Control = Nothing
        Me.RepositoryItemRichEditFontSizeEdit1.Name = "RepositoryItemRichEditFontSizeEdit1"
        '
        'FontSizeIncreaseItem1
        '
        Me.FontSizeIncreaseItem1.Enabled = False
        Me.FontSizeIncreaseItem1.Id = 6
        Me.FontSizeIncreaseItem1.Name = "FontSizeIncreaseItem1"
        '
        'FontSizeDecreaseItem1
        '
        Me.FontSizeDecreaseItem1.Enabled = False
        Me.FontSizeDecreaseItem1.Id = 7
        Me.FontSizeDecreaseItem1.Name = "FontSizeDecreaseItem1"
        '
        'ToggleFontBoldItem1
        '
        Me.ToggleFontBoldItem1.Enabled = False
        Me.ToggleFontBoldItem1.Id = 8
        Me.ToggleFontBoldItem1.Name = "ToggleFontBoldItem1"
        '
        'ToggleFontItalicItem1
        '
        Me.ToggleFontItalicItem1.Enabled = False
        Me.ToggleFontItalicItem1.Id = 9
        Me.ToggleFontItalicItem1.Name = "ToggleFontItalicItem1"
        '
        'ToggleFontUnderlineItem1
        '
        Me.ToggleFontUnderlineItem1.Enabled = False
        Me.ToggleFontUnderlineItem1.Id = 10
        Me.ToggleFontUnderlineItem1.Name = "ToggleFontUnderlineItem1"
        '
        'ToggleFontDoubleUnderlineItem1
        '
        Me.ToggleFontDoubleUnderlineItem1.Enabled = False
        Me.ToggleFontDoubleUnderlineItem1.Id = 11
        Me.ToggleFontDoubleUnderlineItem1.Name = "ToggleFontDoubleUnderlineItem1"
        '
        'ToggleFontStrikeoutItem1
        '
        Me.ToggleFontStrikeoutItem1.Enabled = False
        Me.ToggleFontStrikeoutItem1.Id = 12
        Me.ToggleFontStrikeoutItem1.Name = "ToggleFontStrikeoutItem1"
        '
        'ToggleFontDoubleStrikeoutItem1
        '
        Me.ToggleFontDoubleStrikeoutItem1.Enabled = False
        Me.ToggleFontDoubleStrikeoutItem1.Id = 13
        Me.ToggleFontDoubleStrikeoutItem1.Name = "ToggleFontDoubleStrikeoutItem1"
        '
        'ToggleFontSuperscriptItem1
        '
        Me.ToggleFontSuperscriptItem1.Enabled = False
        Me.ToggleFontSuperscriptItem1.Id = 14
        Me.ToggleFontSuperscriptItem1.Name = "ToggleFontSuperscriptItem1"
        '
        'ToggleFontSubscriptItem1
        '
        Me.ToggleFontSubscriptItem1.Enabled = False
        Me.ToggleFontSubscriptItem1.Id = 15
        Me.ToggleFontSubscriptItem1.Name = "ToggleFontSubscriptItem1"
        '
        'ChangeFontColorItem1
        '
        Me.ChangeFontColorItem1.Enabled = False
        Me.ChangeFontColorItem1.Id = 16
        Me.ChangeFontColorItem1.Name = "ChangeFontColorItem1"
        '
        'ChangeFontBackColorItem1
        '
        Me.ChangeFontBackColorItem1.Enabled = False
        Me.ChangeFontBackColorItem1.Id = 17
        Me.ChangeFontBackColorItem1.Name = "ChangeFontBackColorItem1"
        '
        'ChangeTextCaseItem1
        '
        Me.ChangeTextCaseItem1.Enabled = False
        Me.ChangeTextCaseItem1.Id = 18
        Me.ChangeTextCaseItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MakeTextUpperCaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.MakeTextLowerCaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTextCaseItem1)})
        Me.ChangeTextCaseItem1.Name = "ChangeTextCaseItem1"
        '
        'MakeTextUpperCaseItem1
        '
        Me.MakeTextUpperCaseItem1.Enabled = False
        Me.MakeTextUpperCaseItem1.Id = 19
        Me.MakeTextUpperCaseItem1.Name = "MakeTextUpperCaseItem1"
        '
        'MakeTextLowerCaseItem1
        '
        Me.MakeTextLowerCaseItem1.Enabled = False
        Me.MakeTextLowerCaseItem1.Id = 20
        Me.MakeTextLowerCaseItem1.Name = "MakeTextLowerCaseItem1"
        '
        'ToggleTextCaseItem1
        '
        Me.ToggleTextCaseItem1.Enabled = False
        Me.ToggleTextCaseItem1.Id = 21
        Me.ToggleTextCaseItem1.Name = "ToggleTextCaseItem1"
        '
        'ClearFormattingItem1
        '
        Me.ClearFormattingItem1.Enabled = False
        Me.ClearFormattingItem1.Id = 22
        Me.ClearFormattingItem1.Name = "ClearFormattingItem1"
        '
        'ShowFontFormItem1
        '
        Me.ShowFontFormItem1.Enabled = False
        Me.ShowFontFormItem1.Id = 23
        Me.ShowFontFormItem1.Name = "ShowFontFormItem1"
        '
        'ToggleBulletedListItem1
        '
        Me.ToggleBulletedListItem1.Enabled = False
        Me.ToggleBulletedListItem1.Id = 24
        Me.ToggleBulletedListItem1.Name = "ToggleBulletedListItem1"
        '
        'ToggleNumberingListItem1
        '
        Me.ToggleNumberingListItem1.Enabled = False
        Me.ToggleNumberingListItem1.Id = 25
        Me.ToggleNumberingListItem1.Name = "ToggleNumberingListItem1"
        '
        'ToggleMultiLevelListItem1
        '
        Me.ToggleMultiLevelListItem1.Enabled = False
        Me.ToggleMultiLevelListItem1.Id = 26
        Me.ToggleMultiLevelListItem1.Name = "ToggleMultiLevelListItem1"
        '
        'DecreaseIndentItem1
        '
        Me.DecreaseIndentItem1.Enabled = False
        Me.DecreaseIndentItem1.Id = 27
        Me.DecreaseIndentItem1.Name = "DecreaseIndentItem1"
        '
        'IncreaseIndentItem1
        '
        Me.IncreaseIndentItem1.Enabled = False
        Me.IncreaseIndentItem1.Id = 28
        Me.IncreaseIndentItem1.Name = "IncreaseIndentItem1"
        '
        'ToggleParagraphAlignmentLeftItem1
        '
        Me.ToggleParagraphAlignmentLeftItem1.Enabled = False
        Me.ToggleParagraphAlignmentLeftItem1.Id = 29
        Me.ToggleParagraphAlignmentLeftItem1.Name = "ToggleParagraphAlignmentLeftItem1"
        '
        'ToggleParagraphAlignmentCenterItem1
        '
        Me.ToggleParagraphAlignmentCenterItem1.Enabled = False
        Me.ToggleParagraphAlignmentCenterItem1.Id = 30
        Me.ToggleParagraphAlignmentCenterItem1.Name = "ToggleParagraphAlignmentCenterItem1"
        '
        'ToggleParagraphAlignmentRightItem1
        '
        Me.ToggleParagraphAlignmentRightItem1.Enabled = False
        Me.ToggleParagraphAlignmentRightItem1.Id = 31
        Me.ToggleParagraphAlignmentRightItem1.Name = "ToggleParagraphAlignmentRightItem1"
        '
        'ToggleParagraphAlignmentJustifyItem1
        '
        Me.ToggleParagraphAlignmentJustifyItem1.Enabled = False
        Me.ToggleParagraphAlignmentJustifyItem1.Id = 32
        Me.ToggleParagraphAlignmentJustifyItem1.Name = "ToggleParagraphAlignmentJustifyItem1"
        '
        'ToggleShowWhitespaceItem1
        '
        Me.ToggleShowWhitespaceItem1.Enabled = False
        Me.ToggleShowWhitespaceItem1.Id = 33
        Me.ToggleShowWhitespaceItem1.Name = "ToggleShowWhitespaceItem1"
        '
        'ChangeParagraphLineSpacingItem1
        '
        Me.ChangeParagraphLineSpacingItem1.Enabled = False
        Me.ChangeParagraphLineSpacingItem1.Id = 34
        Me.ChangeParagraphLineSpacingItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetSingleParagraphSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSesquialteralParagraphSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetDoubleParagraphSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowLineSpacingFormItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.AddSpacingBeforeParagraphItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.RemoveSpacingBeforeParagraphItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.AddSpacingAfterParagraphItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.RemoveSpacingAfterParagraphItem1)})
        Me.ChangeParagraphLineSpacingItem1.Name = "ChangeParagraphLineSpacingItem1"
        '
        'SetSingleParagraphSpacingItem1
        '
        Me.SetSingleParagraphSpacingItem1.Enabled = False
        Me.SetSingleParagraphSpacingItem1.Id = 35
        Me.SetSingleParagraphSpacingItem1.Name = "SetSingleParagraphSpacingItem1"
        '
        'SetSesquialteralParagraphSpacingItem1
        '
        Me.SetSesquialteralParagraphSpacingItem1.Enabled = False
        Me.SetSesquialteralParagraphSpacingItem1.Id = 36
        Me.SetSesquialteralParagraphSpacingItem1.Name = "SetSesquialteralParagraphSpacingItem1"
        '
        'SetDoubleParagraphSpacingItem1
        '
        Me.SetDoubleParagraphSpacingItem1.Enabled = False
        Me.SetDoubleParagraphSpacingItem1.Id = 37
        Me.SetDoubleParagraphSpacingItem1.Name = "SetDoubleParagraphSpacingItem1"
        '
        'ShowLineSpacingFormItem1
        '
        Me.ShowLineSpacingFormItem1.Enabled = False
        Me.ShowLineSpacingFormItem1.Id = 38
        Me.ShowLineSpacingFormItem1.Name = "ShowLineSpacingFormItem1"
        '
        'AddSpacingBeforeParagraphItem1
        '
        Me.AddSpacingBeforeParagraphItem1.Enabled = False
        Me.AddSpacingBeforeParagraphItem1.Id = 39
        Me.AddSpacingBeforeParagraphItem1.Name = "AddSpacingBeforeParagraphItem1"
        '
        'RemoveSpacingBeforeParagraphItem1
        '
        Me.RemoveSpacingBeforeParagraphItem1.Enabled = False
        Me.RemoveSpacingBeforeParagraphItem1.Id = 40
        Me.RemoveSpacingBeforeParagraphItem1.Name = "RemoveSpacingBeforeParagraphItem1"
        '
        'AddSpacingAfterParagraphItem1
        '
        Me.AddSpacingAfterParagraphItem1.Enabled = False
        Me.AddSpacingAfterParagraphItem1.Id = 41
        Me.AddSpacingAfterParagraphItem1.Name = "AddSpacingAfterParagraphItem1"
        '
        'RemoveSpacingAfterParagraphItem1
        '
        Me.RemoveSpacingAfterParagraphItem1.Enabled = False
        Me.RemoveSpacingAfterParagraphItem1.Id = 42
        Me.RemoveSpacingAfterParagraphItem1.Name = "RemoveSpacingAfterParagraphItem1"
        '
        'ChangeParagraphBackColorItem1
        '
        Me.ChangeParagraphBackColorItem1.Enabled = False
        Me.ChangeParagraphBackColorItem1.Id = 43
        Me.ChangeParagraphBackColorItem1.Name = "ChangeParagraphBackColorItem1"
        '
        'ShowParagraphFormItem1
        '
        Me.ShowParagraphFormItem1.Enabled = False
        Me.ShowParagraphFormItem1.Id = 44
        Me.ShowParagraphFormItem1.Name = "ShowParagraphFormItem1"
        '
        'ChangeStyleItem1
        '
        Me.ChangeStyleItem1.Edit = Me.RepositoryItemRichEditStyleEdit1
        Me.ChangeStyleItem1.Enabled = False
        Me.ChangeStyleItem1.Id = 45
        Me.ChangeStyleItem1.Name = "ChangeStyleItem1"
        '
        'RepositoryItemRichEditStyleEdit1
        '
        resources.ApplyResources(Me.RepositoryItemRichEditStyleEdit1, "RepositoryItemRichEditStyleEdit1")
        Me.RepositoryItemRichEditStyleEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(CType(resources.GetObject("RepositoryItemRichEditStyleEdit1.Buttons"), DevExpress.XtraEditors.Controls.ButtonPredefines))})
        Me.RepositoryItemRichEditStyleEdit1.Control = Nothing
        Me.RepositoryItemRichEditStyleEdit1.Name = "RepositoryItemRichEditStyleEdit1"
        '
        'ShowEditStyleFormItem1
        '
        Me.ShowEditStyleFormItem1.Enabled = False
        Me.ShowEditStyleFormItem1.Id = 46
        Me.ShowEditStyleFormItem1.Name = "ShowEditStyleFormItem1"
        '
        'FindItem1
        '
        Me.FindItem1.Enabled = False
        Me.FindItem1.Id = 47
        Me.FindItem1.Name = "FindItem1"
        '
        'ReplaceItem1
        '
        Me.ReplaceItem1.Enabled = False
        Me.ReplaceItem1.Id = 48
        Me.ReplaceItem1.Name = "ReplaceItem1"
        '
        'RichEditBarController1
        '
        Me.RichEditBarController1.BarItems.Add(Me.PasteItem1)
        Me.RichEditBarController1.BarItems.Add(Me.CutItem1)
        Me.RichEditBarController1.BarItems.Add(Me.CopyItem1)
        Me.RichEditBarController1.BarItems.Add(Me.PasteSpecialItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFontNameItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFontSizeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FontSizeIncreaseItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FontSizeDecreaseItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontBoldItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontItalicItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontUnderlineItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontDoubleUnderlineItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontStrikeoutItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontDoubleStrikeoutItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontSuperscriptItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFontSubscriptItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFontColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFontBackColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeTextCaseItem1)
        Me.RichEditBarController1.BarItems.Add(Me.MakeTextUpperCaseItem1)
        Me.RichEditBarController1.BarItems.Add(Me.MakeTextLowerCaseItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTextCaseItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ClearFormattingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowFontFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleBulletedListItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleNumberingListItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleMultiLevelListItem1)
        Me.RichEditBarController1.BarItems.Add(Me.DecreaseIndentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.IncreaseIndentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleParagraphAlignmentLeftItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleParagraphAlignmentCenterItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleParagraphAlignmentRightItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleParagraphAlignmentJustifyItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleShowWhitespaceItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeParagraphLineSpacingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSingleParagraphSpacingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSesquialteralParagraphSpacingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetDoubleParagraphSpacingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowLineSpacingFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.AddSpacingBeforeParagraphItem1)
        Me.RichEditBarController1.BarItems.Add(Me.RemoveSpacingBeforeParagraphItem1)
        Me.RichEditBarController1.BarItems.Add(Me.AddSpacingAfterParagraphItem1)
        Me.RichEditBarController1.BarItems.Add(Me.RemoveSpacingAfterParagraphItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeParagraphBackColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowParagraphFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeStyleItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowEditStyleFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FindItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ReplaceItem1)
        '
        'GridColumn1
        '
        resources.ApplyResources(Me.GridColumn1, "GridColumn1")
        Me.GridColumn1.FieldName = "NameConceptCodeGlosa"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        '
        'GridColumn2
        '
        resources.ApplyResources(Me.GridColumn2, "GridColumn2")
        Me.GridColumn2.FieldName = "CodeGlosa"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'GeneralConciliation
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.Icon = CType(resources.GetObject("GeneralConciliation.IconOptions.Icon"), System.Drawing.Icon)
        Me.IconOptions.ShowIcon = False
        Me.MinimizeBox = False
        Me.Name = "GeneralConciliation"
        Me.Opacity = 1.0R
        Me.ViewModeEditHold = True
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        Me.Controls.SetChildIndex(Me.LayoutControl1, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDTxtValuePending.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleResponseHierarchyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDMeComment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValueAcceptedIPS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDTxtValueAcceptedEAPB.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValueAcceptedIPS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciComment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciResponseHierarchyId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValueAcceptedEAPB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLciValuePending, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFontEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRichEditFontSizeEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRichEditStyleEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RichEditBarController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDTxtValueAcceptedIPS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciValueAcceptedIPS As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents PasteItem1 As DevExpress.XtraRichEdit.UI.PasteItem
    Friend WithEvents CutItem1 As DevExpress.XtraRichEdit.UI.CutItem
    Friend WithEvents CopyItem1 As DevExpress.XtraRichEdit.UI.CopyItem
    Friend WithEvents PasteSpecialItem1 As DevExpress.XtraRichEdit.UI.PasteSpecialItem
    Friend WithEvents ChangeFontNameItem1 As DevExpress.XtraRichEdit.UI.ChangeFontNameItem
    Friend WithEvents RepositoryItemFontEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemFontEdit
    Friend WithEvents ChangeFontSizeItem1 As DevExpress.XtraRichEdit.UI.ChangeFontSizeItem
    Friend WithEvents RepositoryItemRichEditFontSizeEdit1 As DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit
    Friend WithEvents FontSizeIncreaseItem1 As DevExpress.XtraRichEdit.UI.FontSizeIncreaseItem
    Friend WithEvents FontSizeDecreaseItem1 As DevExpress.XtraRichEdit.UI.FontSizeDecreaseItem
    Friend WithEvents ToggleFontBoldItem1 As DevExpress.XtraRichEdit.UI.ToggleFontBoldItem
    Friend WithEvents ToggleFontItalicItem1 As DevExpress.XtraRichEdit.UI.ToggleFontItalicItem
    Friend WithEvents ToggleFontUnderlineItem1 As DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem
    Friend WithEvents ToggleFontDoubleUnderlineItem1 As DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem
    Friend WithEvents ToggleFontStrikeoutItem1 As DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem
    Friend WithEvents ToggleFontDoubleStrikeoutItem1 As DevExpress.XtraRichEdit.UI.ToggleFontDoubleStrikeoutItem
    Friend WithEvents ToggleFontSuperscriptItem1 As DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem
    Friend WithEvents ToggleFontSubscriptItem1 As DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem
    Friend WithEvents ChangeFontColorItem1 As DevExpress.XtraRichEdit.UI.ChangeFontColorItem
    Friend WithEvents ChangeFontBackColorItem1 As DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem
    Friend WithEvents ChangeTextCaseItem1 As DevExpress.XtraRichEdit.UI.ChangeTextCaseItem
    Friend WithEvents MakeTextUpperCaseItem1 As DevExpress.XtraRichEdit.UI.MakeTextUpperCaseItem
    Friend WithEvents MakeTextLowerCaseItem1 As DevExpress.XtraRichEdit.UI.MakeTextLowerCaseItem
    Friend WithEvents ToggleTextCaseItem1 As DevExpress.XtraRichEdit.UI.ToggleTextCaseItem
    Friend WithEvents ClearFormattingItem1 As DevExpress.XtraRichEdit.UI.ClearFormattingItem
    Friend WithEvents ShowFontFormItem1 As DevExpress.XtraRichEdit.UI.ShowFontFormItem
    Friend WithEvents ToggleBulletedListItem1 As DevExpress.XtraRichEdit.UI.ToggleBulletedListItem
    Friend WithEvents ToggleNumberingListItem1 As DevExpress.XtraRichEdit.UI.ToggleNumberingListItem
    Friend WithEvents ToggleMultiLevelListItem1 As DevExpress.XtraRichEdit.UI.ToggleMultiLevelListItem
    Friend WithEvents DecreaseIndentItem1 As DevExpress.XtraRichEdit.UI.DecreaseIndentItem
    Friend WithEvents IncreaseIndentItem1 As DevExpress.XtraRichEdit.UI.IncreaseIndentItem
    Friend WithEvents ToggleParagraphAlignmentLeftItem1 As DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem
    Friend WithEvents ToggleParagraphAlignmentCenterItem1 As DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem
    Friend WithEvents ToggleParagraphAlignmentRightItem1 As DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem
    Friend WithEvents ToggleParagraphAlignmentJustifyItem1 As DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem
    Friend WithEvents ToggleShowWhitespaceItem1 As DevExpress.XtraRichEdit.UI.ToggleShowWhitespaceItem
    Friend WithEvents ChangeParagraphLineSpacingItem1 As DevExpress.XtraRichEdit.UI.ChangeParagraphLineSpacingItem
    Friend WithEvents SetSingleParagraphSpacingItem1 As DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem
    Friend WithEvents SetSesquialteralParagraphSpacingItem1 As DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem
    Friend WithEvents SetDoubleParagraphSpacingItem1 As DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem
    Friend WithEvents ShowLineSpacingFormItem1 As DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem
    Friend WithEvents AddSpacingBeforeParagraphItem1 As DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem
    Friend WithEvents RemoveSpacingBeforeParagraphItem1 As DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem
    Friend WithEvents AddSpacingAfterParagraphItem1 As DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem
    Friend WithEvents RemoveSpacingAfterParagraphItem1 As DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem
    Friend WithEvents ChangeParagraphBackColorItem1 As DevExpress.XtraRichEdit.UI.ChangeParagraphBackColorItem
    Friend WithEvents ShowParagraphFormItem1 As DevExpress.XtraRichEdit.UI.ShowParagraphFormItem
    Friend WithEvents ChangeStyleItem1 As DevExpress.XtraRichEdit.UI.ChangeStyleItem
    Friend WithEvents RepositoryItemRichEditStyleEdit1 As DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit
    Friend WithEvents ShowEditStyleFormItem1 As DevExpress.XtraRichEdit.UI.ShowEditStyleFormItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents FindItem1 As DevExpress.XtraRichEdit.UI.FindItem
    Friend WithEvents ReplaceItem1 As DevExpress.XtraRichEdit.UI.ReplaceItem
    Friend WithEvents RichEditBarController1 As DevExpress.XtraRichEdit.UI.RichEditBarController
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents INDMeComment As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDLciComment As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDSleResponseHierarchyId As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDLciResponseHierarchyId As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDTxtValuePending As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDTxtValueAcceptedEAPB As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDLciValueAcceptedEAPB As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDLciValuePending As DevExpress.XtraLayout.LayoutControlItem
End Class
