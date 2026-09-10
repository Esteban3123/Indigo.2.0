Imports Presentation.Glosas.MVP


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class JustificationEvaluation
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim GalleryItemGroup1 As DevExpress.XtraBars.Ribbon.GalleryItemGroup = New DevExpress.XtraBars.Ribbon.GalleryItemGroup()
        Dim RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDgleResponseHierarchy = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.StandaloneBarDockControl1 = New DevExpress.XtraBars.StandaloneBarDockControl()
        Me.INDrecComment = New DevExpress.XtraRichEdit.RichEditControl()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.ClipboardBar1 = New DevExpress.XtraRichEdit.UI.ClipboardBar()
        Me.PasteItem1 = New DevExpress.XtraRichEdit.UI.PasteItem()
        Me.CutItem1 = New DevExpress.XtraRichEdit.UI.CutItem()
        Me.CopyItem1 = New DevExpress.XtraRichEdit.UI.CopyItem()
        Me.PasteSpecialItem1 = New DevExpress.XtraRichEdit.UI.PasteSpecialItem()
        Me.FontBar1 = New DevExpress.XtraRichEdit.UI.FontBar()
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
        Me.ParagraphBar1 = New DevExpress.XtraRichEdit.UI.ParagraphBar()
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
        Me.StylesBar1 = New DevExpress.XtraRichEdit.UI.StylesBar()
        Me.ChangeStyleItem1 = New DevExpress.XtraRichEdit.UI.ChangeStyleItem()
        Me.RepositoryItemRichEditStyleEdit1 = New DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit()
        Me.ShowEditStyleFormItem1 = New DevExpress.XtraRichEdit.UI.ShowEditStyleFormItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.FileNewItem1 = New DevExpress.XtraRichEdit.UI.FileNewItem()
        Me.FileOpenItem1 = New DevExpress.XtraRichEdit.UI.FileOpenItem()
        Me.FileSaveItem1 = New DevExpress.XtraRichEdit.UI.FileSaveItem()
        Me.FileSaveAsItem1 = New DevExpress.XtraRichEdit.UI.FileSaveAsItem()
        Me.QuickPrintItem1 = New DevExpress.XtraRichEdit.UI.QuickPrintItem()
        Me.PrintItem1 = New DevExpress.XtraRichEdit.UI.PrintItem()
        Me.PrintPreviewItem1 = New DevExpress.XtraRichEdit.UI.PrintPreviewItem()
        Me.UndoItem1 = New DevExpress.XtraRichEdit.UI.UndoItem()
        Me.RedoItem1 = New DevExpress.XtraRichEdit.UI.RedoItem()
        Me.FindItem1 = New DevExpress.XtraRichEdit.UI.FindItem()
        Me.ReplaceItem1 = New DevExpress.XtraRichEdit.UI.ReplaceItem()
        Me.InsertPageBreakItem1 = New DevExpress.XtraRichEdit.UI.InsertPageBreakItem()
        Me.InsertTableItem1 = New DevExpress.XtraRichEdit.UI.InsertTableItem()
        Me.InsertPictureItem1 = New DevExpress.XtraRichEdit.UI.InsertPictureItem()
        Me.InsertFloatingPictureItem1 = New DevExpress.XtraRichEdit.UI.InsertFloatingPictureItem()
        Me.InsertBookmarkItem1 = New DevExpress.XtraRichEdit.UI.InsertBookmarkItem()
        Me.InsertHyperlinkItem1 = New DevExpress.XtraRichEdit.UI.InsertHyperlinkItem()
        Me.EditPageHeaderItem1 = New DevExpress.XtraRichEdit.UI.EditPageHeaderItem()
        Me.EditPageFooterItem1 = New DevExpress.XtraRichEdit.UI.EditPageFooterItem()
        Me.InsertPageNumberItem1 = New DevExpress.XtraRichEdit.UI.InsertPageNumberItem()
        Me.InsertPageCountItem1 = New DevExpress.XtraRichEdit.UI.InsertPageCountItem()
        Me.InsertTextBoxItem1 = New DevExpress.XtraRichEdit.UI.InsertTextBoxItem()
        Me.InsertSymbolItem1 = New DevExpress.XtraRichEdit.UI.InsertSymbolItem()
        Me.ChangeSectionPageMarginsItem1 = New DevExpress.XtraRichEdit.UI.ChangeSectionPageMarginsItem()
        Me.SetNormalSectionPageMarginsItem1 = New DevExpress.XtraRichEdit.UI.SetNormalSectionPageMarginsItem()
        Me.SetNarrowSectionPageMarginsItem1 = New DevExpress.XtraRichEdit.UI.SetNarrowSectionPageMarginsItem()
        Me.SetModerateSectionPageMarginsItem1 = New DevExpress.XtraRichEdit.UI.SetModerateSectionPageMarginsItem()
        Me.SetWideSectionPageMarginsItem1 = New DevExpress.XtraRichEdit.UI.SetWideSectionPageMarginsItem()
        Me.ShowPageMarginsSetupFormItem1 = New DevExpress.XtraRichEdit.UI.ShowPageMarginsSetupFormItem()
        Me.ChangeSectionPageOrientationItem1 = New DevExpress.XtraRichEdit.UI.ChangeSectionPageOrientationItem()
        Me.SetPortraitPageOrientationItem1 = New DevExpress.XtraRichEdit.UI.SetPortraitPageOrientationItem()
        Me.SetLandscapePageOrientationItem1 = New DevExpress.XtraRichEdit.UI.SetLandscapePageOrientationItem()
        Me.ChangeSectionPaperKindItem1 = New DevExpress.XtraRichEdit.UI.ChangeSectionPaperKindItem()
        Me.ChangeSectionColumnsItem1 = New DevExpress.XtraRichEdit.UI.ChangeSectionColumnsItem()
        Me.SetSectionOneColumnItem1 = New DevExpress.XtraRichEdit.UI.SetSectionOneColumnItem()
        Me.SetSectionTwoColumnsItem1 = New DevExpress.XtraRichEdit.UI.SetSectionTwoColumnsItem()
        Me.SetSectionThreeColumnsItem1 = New DevExpress.XtraRichEdit.UI.SetSectionThreeColumnsItem()
        Me.ShowColumnsSetupFormItem1 = New DevExpress.XtraRichEdit.UI.ShowColumnsSetupFormItem()
        Me.InsertBreakItem1 = New DevExpress.XtraRichEdit.UI.InsertBreakItem()
        Me.InsertColumnBreakItem1 = New DevExpress.XtraRichEdit.UI.InsertColumnBreakItem()
        Me.InsertSectionBreakNextPageItem1 = New DevExpress.XtraRichEdit.UI.InsertSectionBreakNextPageItem()
        Me.InsertSectionBreakEvenPageItem1 = New DevExpress.XtraRichEdit.UI.InsertSectionBreakEvenPageItem()
        Me.InsertSectionBreakOddPageItem1 = New DevExpress.XtraRichEdit.UI.InsertSectionBreakOddPageItem()
        Me.ChangeSectionLineNumberingItem1 = New DevExpress.XtraRichEdit.UI.ChangeSectionLineNumberingItem()
        Me.SetSectionLineNumberingNoneItem1 = New DevExpress.XtraRichEdit.UI.SetSectionLineNumberingNoneItem()
        Me.SetSectionLineNumberingContinuousItem1 = New DevExpress.XtraRichEdit.UI.SetSectionLineNumberingContinuousItem()
        Me.SetSectionLineNumberingRestartNewPageItem1 = New DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewPageItem()
        Me.SetSectionLineNumberingRestartNewSectionItem1 = New DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewSectionItem()
        Me.ToggleParagraphSuppressLineNumbersItem1 = New DevExpress.XtraRichEdit.UI.ToggleParagraphSuppressLineNumbersItem()
        Me.ShowLineNumberingFormItem1 = New DevExpress.XtraRichEdit.UI.ShowLineNumberingFormItem()
        Me.ChangePageColorItem1 = New DevExpress.XtraRichEdit.UI.ChangePageColorItem()
        Me.InsertTableOfContentsItem1 = New DevExpress.XtraRichEdit.UI.InsertTableOfContentsItem()
        Me.UpdateTableOfContentsItem1 = New DevExpress.XtraRichEdit.UI.UpdateTableOfContentsItem()
        Me.AddParagraphsToTableOfContentItem1 = New DevExpress.XtraRichEdit.UI.AddParagraphsToTableOfContentItem()
        Me.SetParagraphHeadingLevelItem1 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem2 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem3 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem4 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem5 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem6 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem7 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem8 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem9 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.SetParagraphHeadingLevelItem10 = New DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem()
        Me.InsertCaptionPlaceholderItem1 = New DevExpress.XtraRichEdit.UI.InsertCaptionPlaceholderItem()
        Me.InsertFiguresCaptionItems1 = New DevExpress.XtraRichEdit.UI.InsertFiguresCaptionItems()
        Me.InsertTablesCaptionItems1 = New DevExpress.XtraRichEdit.UI.InsertTablesCaptionItems()
        Me.InsertEquationsCaptionItems1 = New DevExpress.XtraRichEdit.UI.InsertEquationsCaptionItems()
        Me.InsertTableOfFiguresPlaceholderItem1 = New DevExpress.XtraRichEdit.UI.InsertTableOfFiguresPlaceholderItem()
        Me.InsertTableOfFiguresItems1 = New DevExpress.XtraRichEdit.UI.InsertTableOfFiguresItems()
        Me.InsertTableOfTablesItems1 = New DevExpress.XtraRichEdit.UI.InsertTableOfTablesItems()
        Me.InsertTableOfEquationsItems1 = New DevExpress.XtraRichEdit.UI.InsertTableOfEquationsItems()
        Me.UpdateTableOfFiguresItem1 = New DevExpress.XtraRichEdit.UI.UpdateTableOfFiguresItem()
        Me.InsertMergeFieldItem1 = New DevExpress.XtraRichEdit.UI.InsertMergeFieldItem()
        Me.ShowAllFieldCodesItem1 = New DevExpress.XtraRichEdit.UI.ShowAllFieldCodesItem()
        Me.ShowAllFieldResultsItem1 = New DevExpress.XtraRichEdit.UI.ShowAllFieldResultsItem()
        Me.ToggleViewMergedDataItem1 = New DevExpress.XtraRichEdit.UI.ToggleViewMergedDataItem()
        Me.CheckSpellingItem1 = New DevExpress.XtraRichEdit.UI.CheckSpellingItem()
        Me.ProtectDocumentItem1 = New DevExpress.XtraRichEdit.UI.ProtectDocumentItem()
        Me.ChangeRangeEditingPermissionsItem1 = New DevExpress.XtraRichEdit.UI.ChangeRangeEditingPermissionsItem()
        Me.UnprotectDocumentItem1 = New DevExpress.XtraRichEdit.UI.UnprotectDocumentItem()
        Me.SwitchToSimpleViewItem1 = New DevExpress.XtraRichEdit.UI.SwitchToSimpleViewItem()
        Me.SwitchToDraftViewItem1 = New DevExpress.XtraRichEdit.UI.SwitchToDraftViewItem()
        Me.SwitchToPrintLayoutViewItem1 = New DevExpress.XtraRichEdit.UI.SwitchToPrintLayoutViewItem()
        Me.ToggleShowHorizontalRulerItem1 = New DevExpress.XtraRichEdit.UI.ToggleShowHorizontalRulerItem()
        Me.ToggleShowVerticalRulerItem1 = New DevExpress.XtraRichEdit.UI.ToggleShowVerticalRulerItem()
        Me.ZoomOutItem1 = New DevExpress.XtraRichEdit.UI.ZoomOutItem()
        Me.ZoomInItem1 = New DevExpress.XtraRichEdit.UI.ZoomInItem()
        Me.GoToPageHeaderItem1 = New DevExpress.XtraRichEdit.UI.GoToPageHeaderItem()
        Me.GoToPageFooterItem1 = New DevExpress.XtraRichEdit.UI.GoToPageFooterItem()
        Me.GoToNextHeaderFooterItem1 = New DevExpress.XtraRichEdit.UI.GoToNextHeaderFooterItem()
        Me.GoToPreviousHeaderFooterItem1 = New DevExpress.XtraRichEdit.UI.GoToPreviousHeaderFooterItem()
        Me.ToggleLinkToPreviousItem1 = New DevExpress.XtraRichEdit.UI.ToggleLinkToPreviousItem()
        Me.ToggleDifferentFirstPageItem1 = New DevExpress.XtraRichEdit.UI.ToggleDifferentFirstPageItem()
        Me.ToggleDifferentOddAndEvenPagesItem1 = New DevExpress.XtraRichEdit.UI.ToggleDifferentOddAndEvenPagesItem()
        Me.ClosePageHeaderFooterItem1 = New DevExpress.XtraRichEdit.UI.ClosePageHeaderFooterItem()
        Me.ToggleFirstRowItem1 = New DevExpress.XtraRichEdit.UI.ToggleFirstRowItem()
        Me.ToggleLastRowItem1 = New DevExpress.XtraRichEdit.UI.ToggleLastRowItem()
        Me.ToggleBandedRowsItem1 = New DevExpress.XtraRichEdit.UI.ToggleBandedRowsItem()
        Me.ToggleFirstColumnItem1 = New DevExpress.XtraRichEdit.UI.ToggleFirstColumnItem()
        Me.ToggleLastColumnItem1 = New DevExpress.XtraRichEdit.UI.ToggleLastColumnItem()
        Me.ToggleBandedColumnItem1 = New DevExpress.XtraRichEdit.UI.ToggleBandedColumnItem()
        Me.GalleryChangeTableStyleItem1 = New DevExpress.XtraRichEdit.UI.GalleryChangeTableStyleItem()
        Me.ChangeTableBorderLineStyleItem1 = New DevExpress.XtraRichEdit.UI.ChangeTableBorderLineStyleItem()
        Me.RepositoryItemBorderLineStyle1 = New DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle()
        Me.ChangeTableBorderLineWeightItem1 = New DevExpress.XtraRichEdit.UI.ChangeTableBorderLineWeightItem()
        Me.RepositoryItemBorderLineWeight1 = New DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight()
        Me.ChangeTableBorderColorItem1 = New DevExpress.XtraRichEdit.UI.ChangeTableBorderColorItem()
        Me.ChangeTableBordersItem1 = New DevExpress.XtraRichEdit.UI.ChangeTableBordersItem()
        Me.ToggleTableCellsBottomBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomBorderItem()
        Me.ToggleTableCellsTopBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsTopBorderItem()
        Me.ToggleTableCellsLeftBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsLeftBorderItem()
        Me.ToggleTableCellsRightBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsRightBorderItem()
        Me.ResetTableCellsAllBordersItem1 = New DevExpress.XtraRichEdit.UI.ResetTableCellsAllBordersItem()
        Me.ToggleTableCellsAllBordersItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsAllBordersItem()
        Me.ToggleTableCellsOutsideBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsOutsideBorderItem()
        Me.ToggleTableCellsInsideBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideBorderItem()
        Me.ToggleTableCellsInsideHorizontalBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideHorizontalBorderItem()
        Me.ToggleTableCellsInsideVerticalBorderItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideVerticalBorderItem()
        Me.ToggleShowTableGridLinesItem1 = New DevExpress.XtraRichEdit.UI.ToggleShowTableGridLinesItem()
        Me.ChangeTableCellsShadingItem1 = New DevExpress.XtraRichEdit.UI.ChangeTableCellsShadingItem()
        Me.SelectTableElementsItem1 = New DevExpress.XtraRichEdit.UI.SelectTableElementsItem()
        Me.SelectTableCellItem1 = New DevExpress.XtraRichEdit.UI.SelectTableCellItem()
        Me.SelectTableColumnItem1 = New DevExpress.XtraRichEdit.UI.SelectTableColumnItem()
        Me.SelectTableRowItem1 = New DevExpress.XtraRichEdit.UI.SelectTableRowItem()
        Me.SelectTableItem1 = New DevExpress.XtraRichEdit.UI.SelectTableItem()
        Me.ShowTablePropertiesFormItem1 = New DevExpress.XtraRichEdit.UI.ShowTablePropertiesFormItem()
        Me.DeleteTableElementsItem1 = New DevExpress.XtraRichEdit.UI.DeleteTableElementsItem()
        Me.ShowDeleteTableCellsFormItem1 = New DevExpress.XtraRichEdit.UI.ShowDeleteTableCellsFormItem()
        Me.DeleteTableColumnsItem1 = New DevExpress.XtraRichEdit.UI.DeleteTableColumnsItem()
        Me.DeleteTableRowsItem1 = New DevExpress.XtraRichEdit.UI.DeleteTableRowsItem()
        Me.DeleteTableItem1 = New DevExpress.XtraRichEdit.UI.DeleteTableItem()
        Me.InsertTableRowAboveItem1 = New DevExpress.XtraRichEdit.UI.InsertTableRowAboveItem()
        Me.InsertTableRowBelowItem1 = New DevExpress.XtraRichEdit.UI.InsertTableRowBelowItem()
        Me.InsertTableColumnToLeftItem1 = New DevExpress.XtraRichEdit.UI.InsertTableColumnToLeftItem()
        Me.InsertTableColumnToRightItem1 = New DevExpress.XtraRichEdit.UI.InsertTableColumnToRightItem()
        Me.ShowInsertTableCellsFormItem1 = New DevExpress.XtraRichEdit.UI.ShowInsertTableCellsFormItem()
        Me.MergeTableCellsItem1 = New DevExpress.XtraRichEdit.UI.MergeTableCellsItem()
        Me.ShowSplitTableCellsForm1 = New DevExpress.XtraRichEdit.UI.ShowSplitTableCellsForm()
        Me.SplitTableItem1 = New DevExpress.XtraRichEdit.UI.SplitTableItem()
        Me.ToggleTableAutoFitItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableAutoFitItem()
        Me.ToggleTableAutoFitContentsItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableAutoFitContentsItem()
        Me.ToggleTableAutoFitWindowItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableAutoFitWindowItem()
        Me.ToggleTableFixedColumnWidthItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableFixedColumnWidthItem()
        Me.ToggleTableCellsTopLeftAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsTopLeftAlignmentItem()
        Me.ToggleTableCellsMiddleLeftAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleLeftAlignmentItem()
        Me.ToggleTableCellsBottomLeftAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomLeftAlignmentItem()
        Me.ToggleTableCellsTopCenterAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsTopCenterAlignmentItem()
        Me.ToggleTableCellsMiddleCenterAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleCenterAlignmentItem()
        Me.ToggleTableCellsBottomCenterAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomCenterAlignmentItem()
        Me.ToggleTableCellsTopRightAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsTopRightAlignmentItem()
        Me.ToggleTableCellsMiddleRightAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleRightAlignmentItem()
        Me.ToggleTableCellsBottomRightAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomRightAlignmentItem()
        Me.ShowTableOptionsFormItem1 = New DevExpress.XtraRichEdit.UI.ShowTableOptionsFormItem()
        Me.ChangeFloatingObjectFillColorItem1 = New DevExpress.XtraRichEdit.UI.ChangeFloatingObjectFillColorItem()
        Me.ChangeFloatingObjectOutlineColorItem1 = New DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineColorItem()
        Me.ChangeFloatingObjectOutlineWeightItem1 = New DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineWeightItem()
        Me.RepositoryItemFloatingObjectOutlineWeight1 = New DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight()
        Me.ChangeFloatingObjectTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.ChangeFloatingObjectTextWrapTypeItem()
        Me.SetFloatingObjectSquareTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectSquareTextWrapTypeItem()
        Me.SetFloatingObjectTightTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectTightTextWrapTypeItem()
        Me.SetFloatingObjectThroughTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectThroughTextWrapTypeItem()
        Me.SetFloatingObjectTopAndBottomTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectTopAndBottomTextWrapTypeItem()
        Me.SetFloatingObjectBehindTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectBehindTextWrapTypeItem()
        Me.SetFloatingObjectInFrontOfTextWrapTypeItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectInFrontOfTextWrapTypeItem()
        Me.ChangeFloatingObjectAlignmentItem1 = New DevExpress.XtraRichEdit.UI.ChangeFloatingObjectAlignmentItem()
        Me.SetFloatingObjectTopLeftAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectTopLeftAlignmentItem()
        Me.SetFloatingObjectTopCenterAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectTopCenterAlignmentItem()
        Me.SetFloatingObjectTopRightAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectTopRightAlignmentItem()
        Me.SetFloatingObjectMiddleLeftAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleLeftAlignmentItem()
        Me.SetFloatingObjectMiddleCenterAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleCenterAlignmentItem()
        Me.SetFloatingObjectMiddleRightAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleRightAlignmentItem()
        Me.SetFloatingObjectBottomLeftAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomLeftAlignmentItem()
        Me.SetFloatingObjectBottomCenterAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomCenterAlignmentItem()
        Me.SetFloatingObjectBottomRightAlignmentItem1 = New DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomRightAlignmentItem()
        Me.FloatingObjectBringForwardSubItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardSubItem()
        Me.FloatingObjectBringForwardItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardItem()
        Me.FloatingObjectBringToFrontItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectBringToFrontItem()
        Me.FloatingObjectBringInFrontOfTextItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectBringInFrontOfTextItem()
        Me.FloatingObjectSendBackwardSubItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardSubItem()
        Me.FloatingObjectSendBackwardItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardItem()
        Me.FloatingObjectSendToBackItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectSendToBackItem()
        Me.FloatingObjectSendBehindTextItem1 = New DevExpress.XtraRichEdit.UI.FloatingObjectSendBehindTextItem()
        Me.INDgleTemplates = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SimpleButton3 = New DevExpress.XtraEditors.SimpleButton()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
        Me.INDlyiResponseHierarchy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.RichEditBarController1 = New DevExpress.XtraRichEdit.UI.RichEditBarController(Me.components)
        Me.InsertPageBreakItem2 = New DevExpress.XtraRichEdit.UI.InsertPageBreakItem()
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDgleResponseHierarchy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFontEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRichEditFontSizeEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemRichEditStyleEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemBorderLineStyle1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemBorderLineWeight1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFloatingObjectOutlineWeight1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgleTemplates.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiResponseHierarchy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RichEditBarController1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDgleResponseHierarchy)
        Me.LayoutControl1.Controls.Add(Me.StandaloneBarDockControl1)
        Me.LayoutControl1.Controls.Add(Me.INDrecComment)
        Me.LayoutControl1.Controls.Add(Me.INDgleTemplates)
        Me.LayoutControl1.Controls.Add(Me.SimpleButton3)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.Root = Me.LayoutControlGroup1
        Me.LayoutControl1.Size = New System.Drawing.Size(1386, 757)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDgleResponseHierarchy
        '
        Me.INDgleResponseHierarchy.AllowQueryOne = True
        Me.INDgleResponseHierarchy.Datasource = Nothing
        Me.INDgleResponseHierarchy.DisplayMember = "{Code} - {Name}"
        Me.INDgleResponseHierarchy.DisplayNullText = ""
        Me.INDgleResponseHierarchy.EditValue = Nothing
        Me.INDgleResponseHierarchy.EnterMoveNextControl = False
        Me.INDgleResponseHierarchy.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDgleResponseHierarchy.IdOpenForm = 0
        Me.INDgleResponseHierarchy.Location = New System.Drawing.Point(792, 40)
        Me.INDgleResponseHierarchy.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDgleResponseHierarchy.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDgleResponseHierarchy.Name = "INDgleResponseHierarchy"
        Me.INDgleResponseHierarchy.PopUpFormSize = New System.Drawing.Size(200, 300)
        Me.INDgleResponseHierarchy.Size = New System.Drawing.Size(364, 28)
        Me.INDgleResponseHierarchy.TabIndex = 14
        Me.INDgleResponseHierarchy.ValueMember = "Id"
        Me.INDgleResponseHierarchy.View = Me.SearchLookUpEditExView3
        '
        'SearchLookUpEditExView3
        '
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView3.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView3.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView3.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEditExView3.Name = "SearchLookUpEditExView3"
        Me.SearchLookUpEditExView3.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView3.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView3.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView3, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Codigo"
        Me.GridColumn1.FieldName = "Code"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'StandaloneBarDockControl1
        '
        Me.StandaloneBarDockControl1.CausesValidation = False
        Me.StandaloneBarDockControl1.Location = New System.Drawing.Point(12, 12)
        Me.StandaloneBarDockControl1.Manager = Me.BarManager1
        Me.StandaloneBarDockControl1.Name = "StandaloneBarDockControl1"
        Me.StandaloneBarDockControl1.Size = New System.Drawing.Size(1554, 24)
        Me.StandaloneBarDockControl1.Text = "StandaloneBarDockControl1"
        '
        'INDrecComment
        '
        Me.INDrecComment.Location = New System.Drawing.Point(12, 76)
        Me.INDrecComment.MenuManager = Me.BarManager1
        Me.INDrecComment.Name = "INDrecComment"
        Me.INDrecComment.Size = New System.Drawing.Size(1554, 652)
        Me.INDrecComment.TabIndex = 11
        '
        'BarManager1
        '
        Me.BarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.ClipboardBar1, Me.FontBar1, Me.ParagraphBar1, Me.StylesBar1})
        Me.BarManager1.DockControls.Add(Me.barDockControlTop)
        Me.BarManager1.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager1.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager1.DockControls.Add(Me.barDockControlRight)
        Me.BarManager1.DockControls.Add(Me.StandaloneBarDockControl1)
        Me.BarManager1.Form = Me
        Me.BarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.FileNewItem1, Me.FileOpenItem1, Me.FileSaveItem1, Me.FileSaveAsItem1, Me.QuickPrintItem1, Me.PrintItem1, Me.PrintPreviewItem1, Me.UndoItem1, Me.RedoItem1, Me.PasteItem1, Me.CutItem1, Me.CopyItem1, Me.PasteSpecialItem1, Me.ChangeFontNameItem1, Me.ChangeFontSizeItem1, Me.FontSizeIncreaseItem1, Me.FontSizeDecreaseItem1, Me.ToggleFontBoldItem1, Me.ToggleFontItalicItem1, Me.ToggleFontUnderlineItem1, Me.ToggleFontDoubleUnderlineItem1, Me.ToggleFontStrikeoutItem1, Me.ToggleFontDoubleStrikeoutItem1, Me.ToggleFontSuperscriptItem1, Me.ToggleFontSubscriptItem1, Me.ChangeFontColorItem1, Me.ChangeFontBackColorItem1, Me.ChangeTextCaseItem1, Me.MakeTextUpperCaseItem1, Me.MakeTextLowerCaseItem1, Me.ToggleTextCaseItem1, Me.ClearFormattingItem1, Me.ShowFontFormItem1, Me.ToggleBulletedListItem1, Me.ToggleNumberingListItem1, Me.ToggleMultiLevelListItem1, Me.DecreaseIndentItem1, Me.IncreaseIndentItem1, Me.ToggleParagraphAlignmentLeftItem1, Me.ToggleParagraphAlignmentCenterItem1, Me.ToggleParagraphAlignmentRightItem1, Me.ToggleParagraphAlignmentJustifyItem1, Me.ToggleShowWhitespaceItem1, Me.ChangeParagraphLineSpacingItem1, Me.SetSingleParagraphSpacingItem1, Me.SetSesquialteralParagraphSpacingItem1, Me.SetDoubleParagraphSpacingItem1, Me.ShowLineSpacingFormItem1, Me.AddSpacingBeforeParagraphItem1, Me.RemoveSpacingBeforeParagraphItem1, Me.AddSpacingAfterParagraphItem1, Me.RemoveSpacingAfterParagraphItem1, Me.ChangeParagraphBackColorItem1, Me.ShowParagraphFormItem1, Me.ChangeStyleItem1, Me.ShowEditStyleFormItem1, Me.FindItem1, Me.ReplaceItem1, Me.InsertPageBreakItem1, Me.InsertTableItem1, Me.InsertPictureItem1, Me.InsertFloatingPictureItem1, Me.InsertBookmarkItem1, Me.InsertHyperlinkItem1, Me.EditPageHeaderItem1, Me.EditPageFooterItem1, Me.InsertPageNumberItem1, Me.InsertPageCountItem1, Me.InsertTextBoxItem1, Me.InsertSymbolItem1, Me.ChangeSectionPageMarginsItem1, Me.SetNormalSectionPageMarginsItem1, Me.SetNarrowSectionPageMarginsItem1, Me.SetModerateSectionPageMarginsItem1, Me.SetWideSectionPageMarginsItem1, Me.ShowPageMarginsSetupFormItem1, Me.ChangeSectionPageOrientationItem1, Me.SetPortraitPageOrientationItem1, Me.SetLandscapePageOrientationItem1, Me.ChangeSectionPaperKindItem1, Me.ChangeSectionColumnsItem1, Me.SetSectionOneColumnItem1, Me.SetSectionTwoColumnsItem1, Me.SetSectionThreeColumnsItem1, Me.ShowColumnsSetupFormItem1, Me.InsertBreakItem1, Me.InsertColumnBreakItem1, Me.InsertSectionBreakNextPageItem1, Me.InsertSectionBreakEvenPageItem1, Me.InsertSectionBreakOddPageItem1, Me.ChangeSectionLineNumberingItem1, Me.SetSectionLineNumberingNoneItem1, Me.SetSectionLineNumberingContinuousItem1, Me.SetSectionLineNumberingRestartNewPageItem1, Me.SetSectionLineNumberingRestartNewSectionItem1, Me.ToggleParagraphSuppressLineNumbersItem1, Me.ShowLineNumberingFormItem1, Me.ChangePageColorItem1, Me.InsertTableOfContentsItem1, Me.UpdateTableOfContentsItem1, Me.AddParagraphsToTableOfContentItem1, Me.SetParagraphHeadingLevelItem1, Me.SetParagraphHeadingLevelItem2, Me.SetParagraphHeadingLevelItem3, Me.SetParagraphHeadingLevelItem4, Me.SetParagraphHeadingLevelItem5, Me.SetParagraphHeadingLevelItem6, Me.SetParagraphHeadingLevelItem7, Me.SetParagraphHeadingLevelItem8, Me.SetParagraphHeadingLevelItem9, Me.SetParagraphHeadingLevelItem10, Me.InsertCaptionPlaceholderItem1, Me.InsertFiguresCaptionItems1, Me.InsertTablesCaptionItems1, Me.InsertEquationsCaptionItems1, Me.InsertTableOfFiguresPlaceholderItem1, Me.InsertTableOfFiguresItems1, Me.InsertTableOfTablesItems1, Me.InsertTableOfEquationsItems1, Me.UpdateTableOfFiguresItem1, Me.InsertMergeFieldItem1, Me.ShowAllFieldCodesItem1, Me.ShowAllFieldResultsItem1, Me.ToggleViewMergedDataItem1, Me.CheckSpellingItem1, Me.ProtectDocumentItem1, Me.ChangeRangeEditingPermissionsItem1, Me.UnprotectDocumentItem1, Me.SwitchToSimpleViewItem1, Me.SwitchToDraftViewItem1, Me.SwitchToPrintLayoutViewItem1, Me.ToggleShowHorizontalRulerItem1, Me.ToggleShowVerticalRulerItem1, Me.ZoomOutItem1, Me.ZoomInItem1, Me.GoToPageHeaderItem1, Me.GoToPageFooterItem1, Me.GoToNextHeaderFooterItem1, Me.GoToPreviousHeaderFooterItem1, Me.ToggleLinkToPreviousItem1, Me.ToggleDifferentFirstPageItem1, Me.ToggleDifferentOddAndEvenPagesItem1, Me.ClosePageHeaderFooterItem1, Me.ToggleFirstRowItem1, Me.ToggleLastRowItem1, Me.ToggleBandedRowsItem1, Me.ToggleFirstColumnItem1, Me.ToggleLastColumnItem1, Me.ToggleBandedColumnItem1, Me.GalleryChangeTableStyleItem1, Me.ChangeTableBorderLineStyleItem1, Me.ChangeTableBorderLineWeightItem1, Me.ChangeTableBorderColorItem1, Me.ChangeTableBordersItem1, Me.ToggleTableCellsBottomBorderItem1, Me.ToggleTableCellsTopBorderItem1, Me.ToggleTableCellsLeftBorderItem1, Me.ToggleTableCellsRightBorderItem1, Me.ResetTableCellsAllBordersItem1, Me.ToggleTableCellsAllBordersItem1, Me.ToggleTableCellsOutsideBorderItem1, Me.ToggleTableCellsInsideBorderItem1, Me.ToggleTableCellsInsideHorizontalBorderItem1, Me.ToggleTableCellsInsideVerticalBorderItem1, Me.ToggleShowTableGridLinesItem1, Me.ChangeTableCellsShadingItem1, Me.SelectTableElementsItem1, Me.SelectTableCellItem1, Me.SelectTableColumnItem1, Me.SelectTableRowItem1, Me.SelectTableItem1, Me.ShowTablePropertiesFormItem1, Me.DeleteTableElementsItem1, Me.ShowDeleteTableCellsFormItem1, Me.DeleteTableColumnsItem1, Me.DeleteTableRowsItem1, Me.DeleteTableItem1, Me.InsertTableRowAboveItem1, Me.InsertTableRowBelowItem1, Me.InsertTableColumnToLeftItem1, Me.InsertTableColumnToRightItem1, Me.ShowInsertTableCellsFormItem1, Me.MergeTableCellsItem1, Me.ShowSplitTableCellsForm1, Me.SplitTableItem1, Me.ToggleTableAutoFitItem1, Me.ToggleTableAutoFitContentsItem1, Me.ToggleTableAutoFitWindowItem1, Me.ToggleTableFixedColumnWidthItem1, Me.ToggleTableCellsTopLeftAlignmentItem1, Me.ToggleTableCellsMiddleLeftAlignmentItem1, Me.ToggleTableCellsBottomLeftAlignmentItem1, Me.ToggleTableCellsTopCenterAlignmentItem1, Me.ToggleTableCellsMiddleCenterAlignmentItem1, Me.ToggleTableCellsBottomCenterAlignmentItem1, Me.ToggleTableCellsTopRightAlignmentItem1, Me.ToggleTableCellsMiddleRightAlignmentItem1, Me.ToggleTableCellsBottomRightAlignmentItem1, Me.ShowTableOptionsFormItem1, Me.ChangeFloatingObjectFillColorItem1, Me.ChangeFloatingObjectOutlineColorItem1, Me.ChangeFloatingObjectOutlineWeightItem1, Me.ChangeFloatingObjectTextWrapTypeItem1, Me.SetFloatingObjectSquareTextWrapTypeItem1, Me.SetFloatingObjectTightTextWrapTypeItem1, Me.SetFloatingObjectThroughTextWrapTypeItem1, Me.SetFloatingObjectTopAndBottomTextWrapTypeItem1, Me.SetFloatingObjectBehindTextWrapTypeItem1, Me.SetFloatingObjectInFrontOfTextWrapTypeItem1, Me.ChangeFloatingObjectAlignmentItem1, Me.SetFloatingObjectTopLeftAlignmentItem1, Me.SetFloatingObjectTopCenterAlignmentItem1, Me.SetFloatingObjectTopRightAlignmentItem1, Me.SetFloatingObjectMiddleLeftAlignmentItem1, Me.SetFloatingObjectMiddleCenterAlignmentItem1, Me.SetFloatingObjectMiddleRightAlignmentItem1, Me.SetFloatingObjectBottomLeftAlignmentItem1, Me.SetFloatingObjectBottomCenterAlignmentItem1, Me.SetFloatingObjectBottomRightAlignmentItem1, Me.FloatingObjectBringForwardSubItem1, Me.FloatingObjectBringForwardItem1, Me.FloatingObjectBringToFrontItem1, Me.FloatingObjectBringInFrontOfTextItem1, Me.FloatingObjectSendBackwardSubItem1, Me.FloatingObjectSendBackwardItem1, Me.FloatingObjectSendToBackItem1, Me.FloatingObjectSendBehindTextItem1})
        Me.BarManager1.MaxItemId = 227
        Me.BarManager1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFontEdit1, Me.RepositoryItemRichEditFontSizeEdit1, Me.RepositoryItemRichEditStyleEdit1, Me.RepositoryItemBorderLineStyle1, Me.RepositoryItemBorderLineWeight1, Me.RepositoryItemFloatingObjectOutlineWeight1})
        '
        'ClipboardBar1
        '
        Me.ClipboardBar1.Control = Me.INDrecComment
        Me.ClipboardBar1.DockCol = 2
        Me.ClipboardBar1.DockRow = 0
        Me.ClipboardBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone
        Me.ClipboardBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.PasteItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.CutItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.CopyItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.PasteSpecialItem1)})
        Me.ClipboardBar1.Offset = 509
        Me.ClipboardBar1.StandaloneBarDockControl = Me.StandaloneBarDockControl1
        '
        'PasteItem1
        '
        Me.PasteItem1.Id = 9
        Me.PasteItem1.Name = "PasteItem1"
        '
        'CutItem1
        '
        Me.CutItem1.Id = 10
        Me.CutItem1.Name = "CutItem1"
        '
        'CopyItem1
        '
        Me.CopyItem1.Id = 11
        Me.CopyItem1.Name = "CopyItem1"
        '
        'PasteSpecialItem1
        '
        Me.PasteSpecialItem1.Id = 12
        Me.PasteSpecialItem1.Name = "PasteSpecialItem1"
        '
        'FontBar1
        '
        Me.FontBar1.Control = Me.INDrecComment
        Me.FontBar1.DockCol = 3
        Me.FontBar1.DockRow = 0
        Me.FontBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone
        Me.FontBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeFontNameItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeFontSizeItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.FontSizeIncreaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.FontSizeDecreaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontBoldItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontItalicItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontUnderlineItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontDoubleUnderlineItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontStrikeoutItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontDoubleStrikeoutItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontSuperscriptItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleFontSubscriptItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeFontColorItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeFontBackColorItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeTextCaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ClearFormattingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowFontFormItem1)})
        Me.FontBar1.Offset = 668
        Me.FontBar1.StandaloneBarDockControl = Me.StandaloneBarDockControl1
        '
        'ChangeFontNameItem1
        '
        Me.ChangeFontNameItem1.Edit = Me.RepositoryItemFontEdit1
        Me.ChangeFontNameItem1.EditWidth = 129
        Me.ChangeFontNameItem1.Id = 13
        Me.ChangeFontNameItem1.Name = "ChangeFontNameItem1"
        '
        'RepositoryItemFontEdit1
        '
        Me.RepositoryItemFontEdit1.AutoHeight = False
        Me.RepositoryItemFontEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemFontEdit1.Name = "RepositoryItemFontEdit1"
        '
        'ChangeFontSizeItem1
        '
        Me.ChangeFontSizeItem1.Edit = Me.RepositoryItemRichEditFontSizeEdit1
        Me.ChangeFontSizeItem1.Id = 14
        Me.ChangeFontSizeItem1.Name = "ChangeFontSizeItem1"
        '
        'RepositoryItemRichEditFontSizeEdit1
        '
        Me.RepositoryItemRichEditFontSizeEdit1.AutoHeight = False
        Me.RepositoryItemRichEditFontSizeEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemRichEditFontSizeEdit1.Control = Me.INDrecComment
        Me.RepositoryItemRichEditFontSizeEdit1.Name = "RepositoryItemRichEditFontSizeEdit1"
        '
        'FontSizeIncreaseItem1
        '
        Me.FontSizeIncreaseItem1.Id = 15
        Me.FontSizeIncreaseItem1.Name = "FontSizeIncreaseItem1"
        '
        'FontSizeDecreaseItem1
        '
        Me.FontSizeDecreaseItem1.Id = 16
        Me.FontSizeDecreaseItem1.Name = "FontSizeDecreaseItem1"
        '
        'ToggleFontBoldItem1
        '
        Me.ToggleFontBoldItem1.Id = 17
        Me.ToggleFontBoldItem1.Name = "ToggleFontBoldItem1"
        '
        'ToggleFontItalicItem1
        '
        Me.ToggleFontItalicItem1.Id = 18
        Me.ToggleFontItalicItem1.Name = "ToggleFontItalicItem1"
        '
        'ToggleFontUnderlineItem1
        '
        Me.ToggleFontUnderlineItem1.Id = 19
        Me.ToggleFontUnderlineItem1.Name = "ToggleFontUnderlineItem1"
        '
        'ToggleFontDoubleUnderlineItem1
        '
        Me.ToggleFontDoubleUnderlineItem1.Id = 20
        Me.ToggleFontDoubleUnderlineItem1.Name = "ToggleFontDoubleUnderlineItem1"
        '
        'ToggleFontStrikeoutItem1
        '
        Me.ToggleFontStrikeoutItem1.Id = 21
        Me.ToggleFontStrikeoutItem1.Name = "ToggleFontStrikeoutItem1"
        '
        'ToggleFontDoubleStrikeoutItem1
        '
        Me.ToggleFontDoubleStrikeoutItem1.Id = 22
        Me.ToggleFontDoubleStrikeoutItem1.Name = "ToggleFontDoubleStrikeoutItem1"
        '
        'ToggleFontSuperscriptItem1
        '
        Me.ToggleFontSuperscriptItem1.Id = 23
        Me.ToggleFontSuperscriptItem1.Name = "ToggleFontSuperscriptItem1"
        '
        'ToggleFontSubscriptItem1
        '
        Me.ToggleFontSubscriptItem1.Id = 24
        Me.ToggleFontSubscriptItem1.Name = "ToggleFontSubscriptItem1"
        '
        'ChangeFontColorItem1
        '
        Me.ChangeFontColorItem1.Id = 25
        Me.ChangeFontColorItem1.Name = "ChangeFontColorItem1"
        '
        'ChangeFontBackColorItem1
        '
        Me.ChangeFontBackColorItem1.Id = 26
        Me.ChangeFontBackColorItem1.Name = "ChangeFontBackColorItem1"
        '
        'ChangeTextCaseItem1
        '
        Me.ChangeTextCaseItem1.Id = 27
        Me.ChangeTextCaseItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.MakeTextUpperCaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.MakeTextLowerCaseItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTextCaseItem1)})
        Me.ChangeTextCaseItem1.Name = "ChangeTextCaseItem1"
        '
        'MakeTextUpperCaseItem1
        '
        Me.MakeTextUpperCaseItem1.Id = 28
        Me.MakeTextUpperCaseItem1.Name = "MakeTextUpperCaseItem1"
        '
        'MakeTextLowerCaseItem1
        '
        Me.MakeTextLowerCaseItem1.Id = 29
        Me.MakeTextLowerCaseItem1.Name = "MakeTextLowerCaseItem1"
        '
        'ToggleTextCaseItem1
        '
        Me.ToggleTextCaseItem1.Id = 30
        Me.ToggleTextCaseItem1.Name = "ToggleTextCaseItem1"
        '
        'ClearFormattingItem1
        '
        Me.ClearFormattingItem1.Id = 31
        Me.ClearFormattingItem1.Name = "ClearFormattingItem1"
        '
        'ShowFontFormItem1
        '
        Me.ShowFontFormItem1.Id = 32
        Me.ShowFontFormItem1.Name = "ShowFontFormItem1"
        '
        'ParagraphBar1
        '
        Me.ParagraphBar1.Control = Me.INDrecComment
        Me.ParagraphBar1.DockCol = 0
        Me.ParagraphBar1.DockRow = 0
        Me.ParagraphBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone
        Me.ParagraphBar1.FloatLocation = New System.Drawing.Point(45, 262)
        Me.ParagraphBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleBulletedListItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleNumberingListItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleMultiLevelListItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.DecreaseIndentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.IncreaseIndentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleParagraphAlignmentLeftItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleParagraphAlignmentCenterItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleParagraphAlignmentRightItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleParagraphAlignmentJustifyItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleShowWhitespaceItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeParagraphLineSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeParagraphBackColorItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowParagraphFormItem1)})
        Me.ParagraphBar1.Offset = 1
        Me.ParagraphBar1.StandaloneBarDockControl = Me.StandaloneBarDockControl1
        '
        'ToggleBulletedListItem1
        '
        Me.ToggleBulletedListItem1.Id = 33
        Me.ToggleBulletedListItem1.Name = "ToggleBulletedListItem1"
        '
        'ToggleNumberingListItem1
        '
        Me.ToggleNumberingListItem1.Id = 34
        Me.ToggleNumberingListItem1.Name = "ToggleNumberingListItem1"
        '
        'ToggleMultiLevelListItem1
        '
        Me.ToggleMultiLevelListItem1.Id = 35
        Me.ToggleMultiLevelListItem1.Name = "ToggleMultiLevelListItem1"
        '
        'DecreaseIndentItem1
        '
        Me.DecreaseIndentItem1.Id = 36
        Me.DecreaseIndentItem1.Name = "DecreaseIndentItem1"
        '
        'IncreaseIndentItem1
        '
        Me.IncreaseIndentItem1.Id = 37
        Me.IncreaseIndentItem1.Name = "IncreaseIndentItem1"
        '
        'ToggleParagraphAlignmentLeftItem1
        '
        Me.ToggleParagraphAlignmentLeftItem1.Id = 38
        Me.ToggleParagraphAlignmentLeftItem1.Name = "ToggleParagraphAlignmentLeftItem1"
        '
        'ToggleParagraphAlignmentCenterItem1
        '
        Me.ToggleParagraphAlignmentCenterItem1.Id = 39
        Me.ToggleParagraphAlignmentCenterItem1.Name = "ToggleParagraphAlignmentCenterItem1"
        '
        'ToggleParagraphAlignmentRightItem1
        '
        Me.ToggleParagraphAlignmentRightItem1.Id = 40
        Me.ToggleParagraphAlignmentRightItem1.Name = "ToggleParagraphAlignmentRightItem1"
        '
        'ToggleParagraphAlignmentJustifyItem1
        '
        Me.ToggleParagraphAlignmentJustifyItem1.Id = 41
        Me.ToggleParagraphAlignmentJustifyItem1.Name = "ToggleParagraphAlignmentJustifyItem1"
        '
        'ToggleShowWhitespaceItem1
        '
        Me.ToggleShowWhitespaceItem1.Id = 42
        Me.ToggleShowWhitespaceItem1.Name = "ToggleShowWhitespaceItem1"
        '
        'ChangeParagraphLineSpacingItem1
        '
        Me.ChangeParagraphLineSpacingItem1.Id = 43
        Me.ChangeParagraphLineSpacingItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetSingleParagraphSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSesquialteralParagraphSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetDoubleParagraphSpacingItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowLineSpacingFormItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.AddSpacingBeforeParagraphItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.RemoveSpacingBeforeParagraphItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.AddSpacingAfterParagraphItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.RemoveSpacingAfterParagraphItem1)})
        Me.ChangeParagraphLineSpacingItem1.Name = "ChangeParagraphLineSpacingItem1"
        '
        'SetSingleParagraphSpacingItem1
        '
        Me.SetSingleParagraphSpacingItem1.Id = 44
        Me.SetSingleParagraphSpacingItem1.Name = "SetSingleParagraphSpacingItem1"
        '
        'SetSesquialteralParagraphSpacingItem1
        '
        Me.SetSesquialteralParagraphSpacingItem1.Id = 45
        Me.SetSesquialteralParagraphSpacingItem1.Name = "SetSesquialteralParagraphSpacingItem1"
        '
        'SetDoubleParagraphSpacingItem1
        '
        Me.SetDoubleParagraphSpacingItem1.Id = 46
        Me.SetDoubleParagraphSpacingItem1.Name = "SetDoubleParagraphSpacingItem1"
        '
        'ShowLineSpacingFormItem1
        '
        Me.ShowLineSpacingFormItem1.Id = 47
        Me.ShowLineSpacingFormItem1.Name = "ShowLineSpacingFormItem1"
        '
        'AddSpacingBeforeParagraphItem1
        '
        Me.AddSpacingBeforeParagraphItem1.Id = 48
        Me.AddSpacingBeforeParagraphItem1.Name = "AddSpacingBeforeParagraphItem1"
        '
        'RemoveSpacingBeforeParagraphItem1
        '
        Me.RemoveSpacingBeforeParagraphItem1.Id = 49
        Me.RemoveSpacingBeforeParagraphItem1.Name = "RemoveSpacingBeforeParagraphItem1"
        '
        'AddSpacingAfterParagraphItem1
        '
        Me.AddSpacingAfterParagraphItem1.Id = 50
        Me.AddSpacingAfterParagraphItem1.Name = "AddSpacingAfterParagraphItem1"
        '
        'RemoveSpacingAfterParagraphItem1
        '
        Me.RemoveSpacingAfterParagraphItem1.Id = 51
        Me.RemoveSpacingAfterParagraphItem1.Name = "RemoveSpacingAfterParagraphItem1"
        '
        'ChangeParagraphBackColorItem1
        '
        Me.ChangeParagraphBackColorItem1.Id = 52
        Me.ChangeParagraphBackColorItem1.Name = "ChangeParagraphBackColorItem1"
        '
        'ShowParagraphFormItem1
        '
        Me.ShowParagraphFormItem1.Id = 53
        Me.ShowParagraphFormItem1.Name = "ShowParagraphFormItem1"
        '
        'StylesBar1
        '
        Me.StylesBar1.Control = Me.INDrecComment
        Me.StylesBar1.DockCol = 1
        Me.StylesBar1.DockRow = 0
        Me.StylesBar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone
        Me.StylesBar1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ChangeStyleItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowEditStyleFormItem1)})
        Me.StylesBar1.Offset = 432
        Me.StylesBar1.StandaloneBarDockControl = Me.StandaloneBarDockControl1
        '
        'ChangeStyleItem1
        '
        Me.ChangeStyleItem1.Edit = Me.RepositoryItemRichEditStyleEdit1
        Me.ChangeStyleItem1.Id = 54
        Me.ChangeStyleItem1.Name = "ChangeStyleItem1"
        '
        'RepositoryItemRichEditStyleEdit1
        '
        Me.RepositoryItemRichEditStyleEdit1.AutoHeight = False
        Me.RepositoryItemRichEditStyleEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemRichEditStyleEdit1.Control = Me.INDrecComment
        Me.RepositoryItemRichEditStyleEdit1.Name = "RepositoryItemRichEditStyleEdit1"
        '
        'ShowEditStyleFormItem1
        '
        Me.ShowEditStyleFormItem1.Id = 55
        Me.ShowEditStyleFormItem1.Name = "ShowEditStyleFormItem1"
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1386, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 757)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1386, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 757)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1386, 0)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 757)
        '
        'FileNewItem1
        '
        Me.FileNewItem1.Id = 0
        Me.FileNewItem1.Name = "FileNewItem1"
        '
        'FileOpenItem1
        '
        Me.FileOpenItem1.Id = 1
        Me.FileOpenItem1.Name = "FileOpenItem1"
        '
        'FileSaveItem1
        '
        Me.FileSaveItem1.Id = 2
        Me.FileSaveItem1.Name = "FileSaveItem1"
        '
        'FileSaveAsItem1
        '
        Me.FileSaveAsItem1.Id = 3
        Me.FileSaveAsItem1.Name = "FileSaveAsItem1"
        '
        'QuickPrintItem1
        '
        Me.QuickPrintItem1.Id = 4
        Me.QuickPrintItem1.Name = "QuickPrintItem1"
        '
        'PrintItem1
        '
        Me.PrintItem1.Id = 5
        Me.PrintItem1.Name = "PrintItem1"
        '
        'PrintPreviewItem1
        '
        Me.PrintPreviewItem1.Id = 6
        Me.PrintPreviewItem1.Name = "PrintPreviewItem1"
        '
        'UndoItem1
        '
        Me.UndoItem1.Id = 7
        Me.UndoItem1.Name = "UndoItem1"
        '
        'RedoItem1
        '
        Me.RedoItem1.Id = 8
        Me.RedoItem1.Name = "RedoItem1"
        '
        'FindItem1
        '
        Me.FindItem1.Id = 56
        Me.FindItem1.Name = "FindItem1"
        '
        'ReplaceItem1
        '
        Me.ReplaceItem1.Id = 57
        Me.ReplaceItem1.Name = "ReplaceItem1"
        '
        'InsertPageBreakItem1
        '
        Me.InsertPageBreakItem1.Id = 58
        Me.InsertPageBreakItem1.Name = "InsertPageBreakItem1"
        '
        'InsertTableItem1
        '
        Me.InsertTableItem1.Id = 59
        Me.InsertTableItem1.Name = "InsertTableItem1"
        '
        'InsertPictureItem1
        '
        Me.InsertPictureItem1.Id = 60
        Me.InsertPictureItem1.Name = "InsertPictureItem1"
        '
        'InsertFloatingPictureItem1
        '
        Me.InsertFloatingPictureItem1.Id = 61
        Me.InsertFloatingPictureItem1.Name = "InsertFloatingPictureItem1"
        '
        'InsertBookmarkItem1
        '
        Me.InsertBookmarkItem1.Id = 62
        Me.InsertBookmarkItem1.Name = "InsertBookmarkItem1"
        '
        'InsertHyperlinkItem1
        '
        Me.InsertHyperlinkItem1.Id = 63
        Me.InsertHyperlinkItem1.Name = "InsertHyperlinkItem1"
        '
        'EditPageHeaderItem1
        '
        Me.EditPageHeaderItem1.Id = 64
        Me.EditPageHeaderItem1.Name = "EditPageHeaderItem1"
        '
        'EditPageFooterItem1
        '
        Me.EditPageFooterItem1.Id = 65
        Me.EditPageFooterItem1.Name = "EditPageFooterItem1"
        '
        'InsertPageNumberItem1
        '
        Me.InsertPageNumberItem1.Id = 66
        Me.InsertPageNumberItem1.Name = "InsertPageNumberItem1"
        '
        'InsertPageCountItem1
        '
        Me.InsertPageCountItem1.Id = 67
        Me.InsertPageCountItem1.Name = "InsertPageCountItem1"
        '
        'InsertTextBoxItem1
        '
        Me.InsertTextBoxItem1.Id = 68
        Me.InsertTextBoxItem1.Name = "InsertTextBoxItem1"
        '
        'InsertSymbolItem1
        '
        Me.InsertSymbolItem1.Id = 69
        Me.InsertSymbolItem1.Name = "InsertSymbolItem1"
        '
        'ChangeSectionPageMarginsItem1
        '
        Me.ChangeSectionPageMarginsItem1.Id = 70
        Me.ChangeSectionPageMarginsItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetNormalSectionPageMarginsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetNarrowSectionPageMarginsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetModerateSectionPageMarginsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetWideSectionPageMarginsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowPageMarginsSetupFormItem1)})
        Me.ChangeSectionPageMarginsItem1.Name = "ChangeSectionPageMarginsItem1"
        '
        'SetNormalSectionPageMarginsItem1
        '
        Me.SetNormalSectionPageMarginsItem1.Id = 71
        Me.SetNormalSectionPageMarginsItem1.Name = "SetNormalSectionPageMarginsItem1"
        '
        'SetNarrowSectionPageMarginsItem1
        '
        Me.SetNarrowSectionPageMarginsItem1.Id = 72
        Me.SetNarrowSectionPageMarginsItem1.Name = "SetNarrowSectionPageMarginsItem1"
        '
        'SetModerateSectionPageMarginsItem1
        '
        Me.SetModerateSectionPageMarginsItem1.Id = 73
        Me.SetModerateSectionPageMarginsItem1.Name = "SetModerateSectionPageMarginsItem1"
        '
        'SetWideSectionPageMarginsItem1
        '
        Me.SetWideSectionPageMarginsItem1.Id = 74
        Me.SetWideSectionPageMarginsItem1.Name = "SetWideSectionPageMarginsItem1"
        '
        'ShowPageMarginsSetupFormItem1
        '
        Me.ShowPageMarginsSetupFormItem1.Id = 75
        Me.ShowPageMarginsSetupFormItem1.Name = "ShowPageMarginsSetupFormItem1"
        '
        'ChangeSectionPageOrientationItem1
        '
        Me.ChangeSectionPageOrientationItem1.Id = 76
        Me.ChangeSectionPageOrientationItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetPortraitPageOrientationItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetLandscapePageOrientationItem1)})
        Me.ChangeSectionPageOrientationItem1.Name = "ChangeSectionPageOrientationItem1"
        '
        'SetPortraitPageOrientationItem1
        '
        Me.SetPortraitPageOrientationItem1.Id = 77
        Me.SetPortraitPageOrientationItem1.Name = "SetPortraitPageOrientationItem1"
        '
        'SetLandscapePageOrientationItem1
        '
        Me.SetLandscapePageOrientationItem1.Id = 78
        Me.SetLandscapePageOrientationItem1.Name = "SetLandscapePageOrientationItem1"
        '
        'ChangeSectionPaperKindItem1
        '
        Me.ChangeSectionPaperKindItem1.Id = 79
        Me.ChangeSectionPaperKindItem1.Name = "ChangeSectionPaperKindItem1"
        '
        'ChangeSectionColumnsItem1
        '
        Me.ChangeSectionColumnsItem1.Id = 80
        Me.ChangeSectionColumnsItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionOneColumnItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionTwoColumnsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionThreeColumnsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowColumnsSetupFormItem1)})
        Me.ChangeSectionColumnsItem1.Name = "ChangeSectionColumnsItem1"
        '
        'SetSectionOneColumnItem1
        '
        Me.SetSectionOneColumnItem1.Id = 81
        Me.SetSectionOneColumnItem1.Name = "SetSectionOneColumnItem1"
        '
        'SetSectionTwoColumnsItem1
        '
        Me.SetSectionTwoColumnsItem1.Id = 82
        Me.SetSectionTwoColumnsItem1.Name = "SetSectionTwoColumnsItem1"
        '
        'SetSectionThreeColumnsItem1
        '
        Me.SetSectionThreeColumnsItem1.Id = 83
        Me.SetSectionThreeColumnsItem1.Name = "SetSectionThreeColumnsItem1"
        '
        'ShowColumnsSetupFormItem1
        '
        Me.ShowColumnsSetupFormItem1.Id = 84
        Me.ShowColumnsSetupFormItem1.Name = "ShowColumnsSetupFormItem1"
        '
        'InsertBreakItem1
        '
        Me.InsertBreakItem1.Id = 85
        Me.InsertBreakItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.InsertPageBreakItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertColumnBreakItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertSectionBreakNextPageItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertSectionBreakEvenPageItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertSectionBreakOddPageItem1)})
        Me.InsertBreakItem1.Name = "InsertBreakItem1"
        '
        'InsertColumnBreakItem1
        '
        Me.InsertColumnBreakItem1.Id = 86
        Me.InsertColumnBreakItem1.Name = "InsertColumnBreakItem1"
        '
        'InsertSectionBreakNextPageItem1
        '
        Me.InsertSectionBreakNextPageItem1.Id = 87
        Me.InsertSectionBreakNextPageItem1.Name = "InsertSectionBreakNextPageItem1"
        '
        'InsertSectionBreakEvenPageItem1
        '
        Me.InsertSectionBreakEvenPageItem1.Id = 88
        Me.InsertSectionBreakEvenPageItem1.Name = "InsertSectionBreakEvenPageItem1"
        '
        'InsertSectionBreakOddPageItem1
        '
        Me.InsertSectionBreakOddPageItem1.Id = 89
        Me.InsertSectionBreakOddPageItem1.Name = "InsertSectionBreakOddPageItem1"
        '
        'ChangeSectionLineNumberingItem1
        '
        Me.ChangeSectionLineNumberingItem1.Id = 90
        Me.ChangeSectionLineNumberingItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionLineNumberingNoneItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionLineNumberingContinuousItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionLineNumberingRestartNewPageItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetSectionLineNumberingRestartNewSectionItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleParagraphSuppressLineNumbersItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ShowLineNumberingFormItem1)})
        Me.ChangeSectionLineNumberingItem1.Name = "ChangeSectionLineNumberingItem1"
        '
        'SetSectionLineNumberingNoneItem1
        '
        Me.SetSectionLineNumberingNoneItem1.Id = 91
        Me.SetSectionLineNumberingNoneItem1.Name = "SetSectionLineNumberingNoneItem1"
        '
        'SetSectionLineNumberingContinuousItem1
        '
        Me.SetSectionLineNumberingContinuousItem1.Id = 92
        Me.SetSectionLineNumberingContinuousItem1.Name = "SetSectionLineNumberingContinuousItem1"
        '
        'SetSectionLineNumberingRestartNewPageItem1
        '
        Me.SetSectionLineNumberingRestartNewPageItem1.Id = 93
        Me.SetSectionLineNumberingRestartNewPageItem1.Name = "SetSectionLineNumberingRestartNewPageItem1"
        '
        'SetSectionLineNumberingRestartNewSectionItem1
        '
        Me.SetSectionLineNumberingRestartNewSectionItem1.Id = 94
        Me.SetSectionLineNumberingRestartNewSectionItem1.Name = "SetSectionLineNumberingRestartNewSectionItem1"
        '
        'ToggleParagraphSuppressLineNumbersItem1
        '
        Me.ToggleParagraphSuppressLineNumbersItem1.Id = 95
        Me.ToggleParagraphSuppressLineNumbersItem1.Name = "ToggleParagraphSuppressLineNumbersItem1"
        '
        'ShowLineNumberingFormItem1
        '
        Me.ShowLineNumberingFormItem1.Id = 96
        Me.ShowLineNumberingFormItem1.Name = "ShowLineNumberingFormItem1"
        '
        'ChangePageColorItem1
        '
        Me.ChangePageColorItem1.Id = 97
        Me.ChangePageColorItem1.Name = "ChangePageColorItem1"
        '
        'InsertTableOfContentsItem1
        '
        Me.InsertTableOfContentsItem1.Id = 98
        Me.InsertTableOfContentsItem1.Name = "InsertTableOfContentsItem1"
        '
        'UpdateTableOfContentsItem1
        '
        Me.UpdateTableOfContentsItem1.Id = 99
        Me.UpdateTableOfContentsItem1.Name = "UpdateTableOfContentsItem1"
        '
        'AddParagraphsToTableOfContentItem1
        '
        Me.AddParagraphsToTableOfContentItem1.Id = 100
        Me.AddParagraphsToTableOfContentItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem2), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem3), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem4), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem5), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem6), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem7), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem8), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem9), New DevExpress.XtraBars.LinkPersistInfo(Me.SetParagraphHeadingLevelItem10)})
        Me.AddParagraphsToTableOfContentItem1.Name = "AddParagraphsToTableOfContentItem1"
        '
        'SetParagraphHeadingLevelItem1
        '
        Me.SetParagraphHeadingLevelItem1.Id = 101
        Me.SetParagraphHeadingLevelItem1.Name = "SetParagraphHeadingLevelItem1"
        Me.SetParagraphHeadingLevelItem1.OutlineLevel = 0
        '
        'SetParagraphHeadingLevelItem2
        '
        Me.SetParagraphHeadingLevelItem2.Id = 102
        Me.SetParagraphHeadingLevelItem2.Name = "SetParagraphHeadingLevelItem2"
        Me.SetParagraphHeadingLevelItem2.OutlineLevel = 1
        '
        'SetParagraphHeadingLevelItem3
        '
        Me.SetParagraphHeadingLevelItem3.Id = 103
        Me.SetParagraphHeadingLevelItem3.Name = "SetParagraphHeadingLevelItem3"
        Me.SetParagraphHeadingLevelItem3.OutlineLevel = 2
        '
        'SetParagraphHeadingLevelItem4
        '
        Me.SetParagraphHeadingLevelItem4.Id = 104
        Me.SetParagraphHeadingLevelItem4.Name = "SetParagraphHeadingLevelItem4"
        Me.SetParagraphHeadingLevelItem4.OutlineLevel = 3
        '
        'SetParagraphHeadingLevelItem5
        '
        Me.SetParagraphHeadingLevelItem5.Id = 105
        Me.SetParagraphHeadingLevelItem5.Name = "SetParagraphHeadingLevelItem5"
        Me.SetParagraphHeadingLevelItem5.OutlineLevel = 4
        '
        'SetParagraphHeadingLevelItem6
        '
        Me.SetParagraphHeadingLevelItem6.Id = 106
        Me.SetParagraphHeadingLevelItem6.Name = "SetParagraphHeadingLevelItem6"
        Me.SetParagraphHeadingLevelItem6.OutlineLevel = 5
        '
        'SetParagraphHeadingLevelItem7
        '
        Me.SetParagraphHeadingLevelItem7.Id = 107
        Me.SetParagraphHeadingLevelItem7.Name = "SetParagraphHeadingLevelItem7"
        Me.SetParagraphHeadingLevelItem7.OutlineLevel = 6
        '
        'SetParagraphHeadingLevelItem8
        '
        Me.SetParagraphHeadingLevelItem8.Id = 108
        Me.SetParagraphHeadingLevelItem8.Name = "SetParagraphHeadingLevelItem8"
        Me.SetParagraphHeadingLevelItem8.OutlineLevel = 7
        '
        'SetParagraphHeadingLevelItem9
        '
        Me.SetParagraphHeadingLevelItem9.Id = 109
        Me.SetParagraphHeadingLevelItem9.Name = "SetParagraphHeadingLevelItem9"
        Me.SetParagraphHeadingLevelItem9.OutlineLevel = 8
        '
        'SetParagraphHeadingLevelItem10
        '
        Me.SetParagraphHeadingLevelItem10.Id = 110
        Me.SetParagraphHeadingLevelItem10.Name = "SetParagraphHeadingLevelItem10"
        Me.SetParagraphHeadingLevelItem10.OutlineLevel = 9
        '
        'InsertCaptionPlaceholderItem1
        '
        Me.InsertCaptionPlaceholderItem1.Id = 111
        Me.InsertCaptionPlaceholderItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.InsertFiguresCaptionItems1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertTablesCaptionItems1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertEquationsCaptionItems1)})
        Me.InsertCaptionPlaceholderItem1.Name = "InsertCaptionPlaceholderItem1"
        '
        'InsertFiguresCaptionItems1
        '
        Me.InsertFiguresCaptionItems1.Id = 112
        Me.InsertFiguresCaptionItems1.Name = "InsertFiguresCaptionItems1"
        '
        'InsertTablesCaptionItems1
        '
        Me.InsertTablesCaptionItems1.Id = 113
        Me.InsertTablesCaptionItems1.Name = "InsertTablesCaptionItems1"
        '
        'InsertEquationsCaptionItems1
        '
        Me.InsertEquationsCaptionItems1.Id = 114
        Me.InsertEquationsCaptionItems1.Name = "InsertEquationsCaptionItems1"
        '
        'InsertTableOfFiguresPlaceholderItem1
        '
        Me.InsertTableOfFiguresPlaceholderItem1.Id = 115
        Me.InsertTableOfFiguresPlaceholderItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.InsertTableOfFiguresItems1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertTableOfTablesItems1), New DevExpress.XtraBars.LinkPersistInfo(Me.InsertTableOfEquationsItems1)})
        Me.InsertTableOfFiguresPlaceholderItem1.Name = "InsertTableOfFiguresPlaceholderItem1"
        '
        'InsertTableOfFiguresItems1
        '
        Me.InsertTableOfFiguresItems1.Id = 116
        Me.InsertTableOfFiguresItems1.Name = "InsertTableOfFiguresItems1"
        '
        'InsertTableOfTablesItems1
        '
        Me.InsertTableOfTablesItems1.Id = 117
        Me.InsertTableOfTablesItems1.Name = "InsertTableOfTablesItems1"
        '
        'InsertTableOfEquationsItems1
        '
        Me.InsertTableOfEquationsItems1.Id = 118
        Me.InsertTableOfEquationsItems1.Name = "InsertTableOfEquationsItems1"
        '
        'UpdateTableOfFiguresItem1
        '
        Me.UpdateTableOfFiguresItem1.Id = 119
        Me.UpdateTableOfFiguresItem1.Name = "UpdateTableOfFiguresItem1"
        '
        'InsertMergeFieldItem1
        '
        Me.InsertMergeFieldItem1.Id = 120
        Me.InsertMergeFieldItem1.Name = "InsertMergeFieldItem1"
        '
        'ShowAllFieldCodesItem1
        '
        Me.ShowAllFieldCodesItem1.Id = 121
        Me.ShowAllFieldCodesItem1.Name = "ShowAllFieldCodesItem1"
        '
        'ShowAllFieldResultsItem1
        '
        Me.ShowAllFieldResultsItem1.Id = 122
        Me.ShowAllFieldResultsItem1.Name = "ShowAllFieldResultsItem1"
        '
        'ToggleViewMergedDataItem1
        '
        Me.ToggleViewMergedDataItem1.Id = 123
        Me.ToggleViewMergedDataItem1.Name = "ToggleViewMergedDataItem1"
        '
        'CheckSpellingItem1
        '
        Me.CheckSpellingItem1.Id = 124
        Me.CheckSpellingItem1.Name = "CheckSpellingItem1"
        '
        'ProtectDocumentItem1
        '
        Me.ProtectDocumentItem1.Id = 125
        Me.ProtectDocumentItem1.Name = "ProtectDocumentItem1"
        '
        'ChangeRangeEditingPermissionsItem1
        '
        Me.ChangeRangeEditingPermissionsItem1.Id = 126
        Me.ChangeRangeEditingPermissionsItem1.Name = "ChangeRangeEditingPermissionsItem1"
        '
        'UnprotectDocumentItem1
        '
        Me.UnprotectDocumentItem1.Id = 127
        Me.UnprotectDocumentItem1.Name = "UnprotectDocumentItem1"
        '
        'SwitchToSimpleViewItem1
        '
        Me.SwitchToSimpleViewItem1.Id = 128
        Me.SwitchToSimpleViewItem1.Name = "SwitchToSimpleViewItem1"
        '
        'SwitchToDraftViewItem1
        '
        Me.SwitchToDraftViewItem1.Id = 129
        Me.SwitchToDraftViewItem1.Name = "SwitchToDraftViewItem1"
        '
        'SwitchToPrintLayoutViewItem1
        '
        Me.SwitchToPrintLayoutViewItem1.Id = 130
        Me.SwitchToPrintLayoutViewItem1.Name = "SwitchToPrintLayoutViewItem1"
        '
        'ToggleShowHorizontalRulerItem1
        '
        Me.ToggleShowHorizontalRulerItem1.Id = 131
        Me.ToggleShowHorizontalRulerItem1.Name = "ToggleShowHorizontalRulerItem1"
        '
        'ToggleShowVerticalRulerItem1
        '
        Me.ToggleShowVerticalRulerItem1.Id = 132
        Me.ToggleShowVerticalRulerItem1.Name = "ToggleShowVerticalRulerItem1"
        '
        'ZoomOutItem1
        '
        Me.ZoomOutItem1.Id = 133
        Me.ZoomOutItem1.Name = "ZoomOutItem1"
        '
        'ZoomInItem1
        '
        Me.ZoomInItem1.Id = 134
        Me.ZoomInItem1.Name = "ZoomInItem1"
        '
        'GoToPageHeaderItem1
        '
        Me.GoToPageHeaderItem1.Id = 135
        Me.GoToPageHeaderItem1.Name = "GoToPageHeaderItem1"
        '
        'GoToPageFooterItem1
        '
        Me.GoToPageFooterItem1.Id = 136
        Me.GoToPageFooterItem1.Name = "GoToPageFooterItem1"
        '
        'GoToNextHeaderFooterItem1
        '
        Me.GoToNextHeaderFooterItem1.Id = 137
        Me.GoToNextHeaderFooterItem1.Name = "GoToNextHeaderFooterItem1"
        '
        'GoToPreviousHeaderFooterItem1
        '
        Me.GoToPreviousHeaderFooterItem1.Id = 138
        Me.GoToPreviousHeaderFooterItem1.Name = "GoToPreviousHeaderFooterItem1"
        '
        'ToggleLinkToPreviousItem1
        '
        Me.ToggleLinkToPreviousItem1.Id = 139
        Me.ToggleLinkToPreviousItem1.Name = "ToggleLinkToPreviousItem1"
        '
        'ToggleDifferentFirstPageItem1
        '
        Me.ToggleDifferentFirstPageItem1.Id = 140
        Me.ToggleDifferentFirstPageItem1.Name = "ToggleDifferentFirstPageItem1"
        '
        'ToggleDifferentOddAndEvenPagesItem1
        '
        Me.ToggleDifferentOddAndEvenPagesItem1.Id = 141
        Me.ToggleDifferentOddAndEvenPagesItem1.Name = "ToggleDifferentOddAndEvenPagesItem1"
        '
        'ClosePageHeaderFooterItem1
        '
        Me.ClosePageHeaderFooterItem1.Id = 142
        Me.ClosePageHeaderFooterItem1.Name = "ClosePageHeaderFooterItem1"
        '
        'ToggleFirstRowItem1
        '
        Me.ToggleFirstRowItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.ToggleFirstRowItem1.Id = 143
        Me.ToggleFirstRowItem1.Name = "ToggleFirstRowItem1"
        '
        'ToggleLastRowItem1
        '
        Me.ToggleLastRowItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.ToggleLastRowItem1.Id = 144
        Me.ToggleLastRowItem1.Name = "ToggleLastRowItem1"
        '
        'ToggleBandedRowsItem1
        '
        Me.ToggleBandedRowsItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.ToggleBandedRowsItem1.Id = 145
        Me.ToggleBandedRowsItem1.Name = "ToggleBandedRowsItem1"
        '
        'ToggleFirstColumnItem1
        '
        Me.ToggleFirstColumnItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.ToggleFirstColumnItem1.Id = 146
        Me.ToggleFirstColumnItem1.Name = "ToggleFirstColumnItem1"
        '
        'ToggleLastColumnItem1
        '
        Me.ToggleLastColumnItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.ToggleLastColumnItem1.Id = 147
        Me.ToggleLastColumnItem1.Name = "ToggleLastColumnItem1"
        '
        'ToggleBandedColumnItem1
        '
        Me.ToggleBandedColumnItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText
        Me.ToggleBandedColumnItem1.Id = 148
        Me.ToggleBandedColumnItem1.Name = "ToggleBandedColumnItem1"
        '
        'GalleryChangeTableStyleItem1
        '
        Me.GalleryChangeTableStyleItem1.CurrentItem = Nothing
        Me.GalleryChangeTableStyleItem1.DeleteItemLink = Nothing
        '
        '
        '
        Me.GalleryChangeTableStyleItem1.Gallery.ColumnCount = 3
        Me.GalleryChangeTableStyleItem1.Gallery.Groups.AddRange(New DevExpress.XtraBars.Ribbon.GalleryItemGroup() {GalleryItemGroup1})
        Me.GalleryChangeTableStyleItem1.Gallery.ImageSize = New System.Drawing.Size(65, 46)
        Me.GalleryChangeTableStyleItem1.Id = 149
        Me.GalleryChangeTableStyleItem1.ModifyItemLink = Nothing
        Me.GalleryChangeTableStyleItem1.Name = "GalleryChangeTableStyleItem1"
        Me.GalleryChangeTableStyleItem1.NewItemLink = Nothing
        Me.GalleryChangeTableStyleItem1.PopupGallery = Nothing
        '
        'ChangeTableBorderLineStyleItem1
        '
        Me.ChangeTableBorderLineStyleItem1.Edit = Me.RepositoryItemBorderLineStyle1
        Me.ChangeTableBorderLineStyleItem1.EditWidth = 130
        Me.ChangeTableBorderLineStyleItem1.Id = 150
        Me.ChangeTableBorderLineStyleItem1.Name = "ChangeTableBorderLineStyleItem1"
        '
        'RepositoryItemBorderLineStyle1
        '
        Me.RepositoryItemBorderLineStyle1.AutoHeight = False
        Me.RepositoryItemBorderLineStyle1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemBorderLineStyle1.Control = Me.INDrecComment
        Me.RepositoryItemBorderLineStyle1.Name = "RepositoryItemBorderLineStyle1"
        '
        'ChangeTableBorderLineWeightItem1
        '
        Me.ChangeTableBorderLineWeightItem1.Edit = Me.RepositoryItemBorderLineWeight1
        Me.ChangeTableBorderLineWeightItem1.EditValue = 20
        Me.ChangeTableBorderLineWeightItem1.EditWidth = 130
        Me.ChangeTableBorderLineWeightItem1.Id = 151
        Me.ChangeTableBorderLineWeightItem1.Name = "ChangeTableBorderLineWeightItem1"
        '
        'RepositoryItemBorderLineWeight1
        '
        Me.RepositoryItemBorderLineWeight1.AutoHeight = False
        Me.RepositoryItemBorderLineWeight1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemBorderLineWeight1.Control = Me.INDrecComment
        Me.RepositoryItemBorderLineWeight1.Name = "RepositoryItemBorderLineWeight1"
        '
        'ChangeTableBorderColorItem1
        '
        Me.ChangeTableBorderColorItem1.Id = 152
        Me.ChangeTableBorderColorItem1.Name = "ChangeTableBorderColorItem1"
        '
        'ChangeTableBordersItem1
        '
        Me.ChangeTableBordersItem1.Id = 153
        Me.ChangeTableBordersItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsBottomBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsTopBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsLeftBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsRightBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ResetTableCellsAllBordersItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsAllBordersItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsOutsideBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsInsideBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsInsideHorizontalBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableCellsInsideVerticalBorderItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleShowTableGridLinesItem1)})
        Me.ChangeTableBordersItem1.Name = "ChangeTableBordersItem1"
        '
        'ToggleTableCellsBottomBorderItem1
        '
        Me.ToggleTableCellsBottomBorderItem1.Id = 154
        Me.ToggleTableCellsBottomBorderItem1.Name = "ToggleTableCellsBottomBorderItem1"
        '
        'ToggleTableCellsTopBorderItem1
        '
        Me.ToggleTableCellsTopBorderItem1.Id = 155
        Me.ToggleTableCellsTopBorderItem1.Name = "ToggleTableCellsTopBorderItem1"
        '
        'ToggleTableCellsLeftBorderItem1
        '
        Me.ToggleTableCellsLeftBorderItem1.Id = 156
        Me.ToggleTableCellsLeftBorderItem1.Name = "ToggleTableCellsLeftBorderItem1"
        '
        'ToggleTableCellsRightBorderItem1
        '
        Me.ToggleTableCellsRightBorderItem1.Id = 157
        Me.ToggleTableCellsRightBorderItem1.Name = "ToggleTableCellsRightBorderItem1"
        '
        'ResetTableCellsAllBordersItem1
        '
        Me.ResetTableCellsAllBordersItem1.Id = 158
        Me.ResetTableCellsAllBordersItem1.Name = "ResetTableCellsAllBordersItem1"
        '
        'ToggleTableCellsAllBordersItem1
        '
        Me.ToggleTableCellsAllBordersItem1.Id = 159
        Me.ToggleTableCellsAllBordersItem1.Name = "ToggleTableCellsAllBordersItem1"
        '
        'ToggleTableCellsOutsideBorderItem1
        '
        Me.ToggleTableCellsOutsideBorderItem1.Id = 160
        Me.ToggleTableCellsOutsideBorderItem1.Name = "ToggleTableCellsOutsideBorderItem1"
        '
        'ToggleTableCellsInsideBorderItem1
        '
        Me.ToggleTableCellsInsideBorderItem1.Id = 161
        Me.ToggleTableCellsInsideBorderItem1.Name = "ToggleTableCellsInsideBorderItem1"
        '
        'ToggleTableCellsInsideHorizontalBorderItem1
        '
        Me.ToggleTableCellsInsideHorizontalBorderItem1.Id = 162
        Me.ToggleTableCellsInsideHorizontalBorderItem1.Name = "ToggleTableCellsInsideHorizontalBorderItem1"
        '
        'ToggleTableCellsInsideVerticalBorderItem1
        '
        Me.ToggleTableCellsInsideVerticalBorderItem1.Id = 163
        Me.ToggleTableCellsInsideVerticalBorderItem1.Name = "ToggleTableCellsInsideVerticalBorderItem1"
        '
        'ToggleShowTableGridLinesItem1
        '
        Me.ToggleShowTableGridLinesItem1.Id = 164
        Me.ToggleShowTableGridLinesItem1.Name = "ToggleShowTableGridLinesItem1"
        '
        'ChangeTableCellsShadingItem1
        '
        Me.ChangeTableCellsShadingItem1.Id = 165
        Me.ChangeTableCellsShadingItem1.Name = "ChangeTableCellsShadingItem1"
        '
        'SelectTableElementsItem1
        '
        Me.SelectTableElementsItem1.Id = 166
        Me.SelectTableElementsItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SelectTableCellItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SelectTableColumnItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SelectTableRowItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SelectTableItem1)})
        Me.SelectTableElementsItem1.Name = "SelectTableElementsItem1"
        '
        'SelectTableCellItem1
        '
        Me.SelectTableCellItem1.Id = 167
        Me.SelectTableCellItem1.Name = "SelectTableCellItem1"
        '
        'SelectTableColumnItem1
        '
        Me.SelectTableColumnItem1.Id = 168
        Me.SelectTableColumnItem1.Name = "SelectTableColumnItem1"
        '
        'SelectTableRowItem1
        '
        Me.SelectTableRowItem1.Id = 169
        Me.SelectTableRowItem1.Name = "SelectTableRowItem1"
        '
        'SelectTableItem1
        '
        Me.SelectTableItem1.Id = 170
        Me.SelectTableItem1.Name = "SelectTableItem1"
        '
        'ShowTablePropertiesFormItem1
        '
        Me.ShowTablePropertiesFormItem1.Id = 171
        Me.ShowTablePropertiesFormItem1.Name = "ShowTablePropertiesFormItem1"
        '
        'DeleteTableElementsItem1
        '
        Me.DeleteTableElementsItem1.Id = 172
        Me.DeleteTableElementsItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ShowDeleteTableCellsFormItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.DeleteTableColumnsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.DeleteTableRowsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.DeleteTableItem1)})
        Me.DeleteTableElementsItem1.Name = "DeleteTableElementsItem1"
        '
        'ShowDeleteTableCellsFormItem1
        '
        Me.ShowDeleteTableCellsFormItem1.Id = 173
        Me.ShowDeleteTableCellsFormItem1.Name = "ShowDeleteTableCellsFormItem1"
        '
        'DeleteTableColumnsItem1
        '
        Me.DeleteTableColumnsItem1.Id = 174
        Me.DeleteTableColumnsItem1.Name = "DeleteTableColumnsItem1"
        '
        'DeleteTableRowsItem1
        '
        Me.DeleteTableRowsItem1.Id = 175
        Me.DeleteTableRowsItem1.Name = "DeleteTableRowsItem1"
        '
        'DeleteTableItem1
        '
        Me.DeleteTableItem1.Id = 176
        Me.DeleteTableItem1.Name = "DeleteTableItem1"
        '
        'InsertTableRowAboveItem1
        '
        Me.InsertTableRowAboveItem1.Id = 177
        Me.InsertTableRowAboveItem1.Name = "InsertTableRowAboveItem1"
        '
        'InsertTableRowBelowItem1
        '
        Me.InsertTableRowBelowItem1.Id = 178
        Me.InsertTableRowBelowItem1.Name = "InsertTableRowBelowItem1"
        '
        'InsertTableColumnToLeftItem1
        '
        Me.InsertTableColumnToLeftItem1.Id = 179
        Me.InsertTableColumnToLeftItem1.Name = "InsertTableColumnToLeftItem1"
        '
        'InsertTableColumnToRightItem1
        '
        Me.InsertTableColumnToRightItem1.Id = 180
        Me.InsertTableColumnToRightItem1.Name = "InsertTableColumnToRightItem1"
        '
        'ShowInsertTableCellsFormItem1
        '
        Me.ShowInsertTableCellsFormItem1.Id = 181
        Me.ShowInsertTableCellsFormItem1.Name = "ShowInsertTableCellsFormItem1"
        '
        'MergeTableCellsItem1
        '
        Me.MergeTableCellsItem1.Id = 182
        Me.MergeTableCellsItem1.Name = "MergeTableCellsItem1"
        '
        'ShowSplitTableCellsForm1
        '
        Me.ShowSplitTableCellsForm1.Id = 183
        Me.ShowSplitTableCellsForm1.Name = "ShowSplitTableCellsForm1"
        '
        'SplitTableItem1
        '
        Me.SplitTableItem1.Id = 184
        Me.SplitTableItem1.Name = "SplitTableItem1"
        '
        'ToggleTableAutoFitItem1
        '
        Me.ToggleTableAutoFitItem1.Id = 185
        Me.ToggleTableAutoFitItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableAutoFitContentsItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableAutoFitWindowItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.ToggleTableFixedColumnWidthItem1)})
        Me.ToggleTableAutoFitItem1.Name = "ToggleTableAutoFitItem1"
        '
        'ToggleTableAutoFitContentsItem1
        '
        Me.ToggleTableAutoFitContentsItem1.Id = 186
        Me.ToggleTableAutoFitContentsItem1.Name = "ToggleTableAutoFitContentsItem1"
        '
        'ToggleTableAutoFitWindowItem1
        '
        Me.ToggleTableAutoFitWindowItem1.Id = 187
        Me.ToggleTableAutoFitWindowItem1.Name = "ToggleTableAutoFitWindowItem1"
        '
        'ToggleTableFixedColumnWidthItem1
        '
        Me.ToggleTableFixedColumnWidthItem1.Id = 188
        Me.ToggleTableFixedColumnWidthItem1.Name = "ToggleTableFixedColumnWidthItem1"
        '
        'ToggleTableCellsTopLeftAlignmentItem1
        '
        Me.ToggleTableCellsTopLeftAlignmentItem1.Id = 189
        Me.ToggleTableCellsTopLeftAlignmentItem1.Name = "ToggleTableCellsTopLeftAlignmentItem1"
        '
        'ToggleTableCellsMiddleLeftAlignmentItem1
        '
        Me.ToggleTableCellsMiddleLeftAlignmentItem1.Id = 190
        Me.ToggleTableCellsMiddleLeftAlignmentItem1.Name = "ToggleTableCellsMiddleLeftAlignmentItem1"
        '
        'ToggleTableCellsBottomLeftAlignmentItem1
        '
        Me.ToggleTableCellsBottomLeftAlignmentItem1.Id = 191
        Me.ToggleTableCellsBottomLeftAlignmentItem1.Name = "ToggleTableCellsBottomLeftAlignmentItem1"
        '
        'ToggleTableCellsTopCenterAlignmentItem1
        '
        Me.ToggleTableCellsTopCenterAlignmentItem1.Id = 192
        Me.ToggleTableCellsTopCenterAlignmentItem1.Name = "ToggleTableCellsTopCenterAlignmentItem1"
        '
        'ToggleTableCellsMiddleCenterAlignmentItem1
        '
        Me.ToggleTableCellsMiddleCenterAlignmentItem1.Id = 193
        Me.ToggleTableCellsMiddleCenterAlignmentItem1.Name = "ToggleTableCellsMiddleCenterAlignmentItem1"
        '
        'ToggleTableCellsBottomCenterAlignmentItem1
        '
        Me.ToggleTableCellsBottomCenterAlignmentItem1.Id = 194
        Me.ToggleTableCellsBottomCenterAlignmentItem1.Name = "ToggleTableCellsBottomCenterAlignmentItem1"
        '
        'ToggleTableCellsTopRightAlignmentItem1
        '
        Me.ToggleTableCellsTopRightAlignmentItem1.Id = 195
        Me.ToggleTableCellsTopRightAlignmentItem1.Name = "ToggleTableCellsTopRightAlignmentItem1"
        '
        'ToggleTableCellsMiddleRightAlignmentItem1
        '
        Me.ToggleTableCellsMiddleRightAlignmentItem1.Id = 196
        Me.ToggleTableCellsMiddleRightAlignmentItem1.Name = "ToggleTableCellsMiddleRightAlignmentItem1"
        '
        'ToggleTableCellsBottomRightAlignmentItem1
        '
        Me.ToggleTableCellsBottomRightAlignmentItem1.Id = 197
        Me.ToggleTableCellsBottomRightAlignmentItem1.Name = "ToggleTableCellsBottomRightAlignmentItem1"
        '
        'ShowTableOptionsFormItem1
        '
        Me.ShowTableOptionsFormItem1.Id = 198
        Me.ShowTableOptionsFormItem1.Name = "ShowTableOptionsFormItem1"
        '
        'ChangeFloatingObjectFillColorItem1
        '
        Me.ChangeFloatingObjectFillColorItem1.Id = 199
        Me.ChangeFloatingObjectFillColorItem1.Name = "ChangeFloatingObjectFillColorItem1"
        '
        'ChangeFloatingObjectOutlineColorItem1
        '
        Me.ChangeFloatingObjectOutlineColorItem1.Id = 200
        Me.ChangeFloatingObjectOutlineColorItem1.Name = "ChangeFloatingObjectOutlineColorItem1"
        '
        'ChangeFloatingObjectOutlineWeightItem1
        '
        Me.ChangeFloatingObjectOutlineWeightItem1.Edit = Me.RepositoryItemFloatingObjectOutlineWeight1
        Me.ChangeFloatingObjectOutlineWeightItem1.EditValue = 20
        Me.ChangeFloatingObjectOutlineWeightItem1.Id = 201
        Me.ChangeFloatingObjectOutlineWeightItem1.Name = "ChangeFloatingObjectOutlineWeightItem1"
        '
        'RepositoryItemFloatingObjectOutlineWeight1
        '
        Me.RepositoryItemFloatingObjectOutlineWeight1.AutoHeight = False
        Me.RepositoryItemFloatingObjectOutlineWeight1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemFloatingObjectOutlineWeight1.Control = Me.INDrecComment
        Me.RepositoryItemFloatingObjectOutlineWeight1.Name = "RepositoryItemFloatingObjectOutlineWeight1"
        '
        'ChangeFloatingObjectTextWrapTypeItem1
        '
        Me.ChangeFloatingObjectTextWrapTypeItem1.Id = 202
        Me.ChangeFloatingObjectTextWrapTypeItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectSquareTextWrapTypeItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectTightTextWrapTypeItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectThroughTextWrapTypeItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectTopAndBottomTextWrapTypeItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectBehindTextWrapTypeItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectInFrontOfTextWrapTypeItem1)})
        Me.ChangeFloatingObjectTextWrapTypeItem1.Name = "ChangeFloatingObjectTextWrapTypeItem1"
        '
        'SetFloatingObjectSquareTextWrapTypeItem1
        '
        Me.SetFloatingObjectSquareTextWrapTypeItem1.Id = 203
        Me.SetFloatingObjectSquareTextWrapTypeItem1.Name = "SetFloatingObjectSquareTextWrapTypeItem1"
        '
        'SetFloatingObjectTightTextWrapTypeItem1
        '
        Me.SetFloatingObjectTightTextWrapTypeItem1.Id = 204
        Me.SetFloatingObjectTightTextWrapTypeItem1.Name = "SetFloatingObjectTightTextWrapTypeItem1"
        '
        'SetFloatingObjectThroughTextWrapTypeItem1
        '
        Me.SetFloatingObjectThroughTextWrapTypeItem1.Id = 205
        Me.SetFloatingObjectThroughTextWrapTypeItem1.Name = "SetFloatingObjectThroughTextWrapTypeItem1"
        '
        'SetFloatingObjectTopAndBottomTextWrapTypeItem1
        '
        Me.SetFloatingObjectTopAndBottomTextWrapTypeItem1.Id = 206
        Me.SetFloatingObjectTopAndBottomTextWrapTypeItem1.Name = "SetFloatingObjectTopAndBottomTextWrapTypeItem1"
        '
        'SetFloatingObjectBehindTextWrapTypeItem1
        '
        Me.SetFloatingObjectBehindTextWrapTypeItem1.Id = 207
        Me.SetFloatingObjectBehindTextWrapTypeItem1.Name = "SetFloatingObjectBehindTextWrapTypeItem1"
        '
        'SetFloatingObjectInFrontOfTextWrapTypeItem1
        '
        Me.SetFloatingObjectInFrontOfTextWrapTypeItem1.Id = 208
        Me.SetFloatingObjectInFrontOfTextWrapTypeItem1.Name = "SetFloatingObjectInFrontOfTextWrapTypeItem1"
        '
        'ChangeFloatingObjectAlignmentItem1
        '
        Me.ChangeFloatingObjectAlignmentItem1.Id = 209
        Me.ChangeFloatingObjectAlignmentItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectTopLeftAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectTopCenterAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectTopRightAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectMiddleLeftAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectMiddleCenterAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectMiddleRightAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectBottomLeftAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectBottomCenterAlignmentItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.SetFloatingObjectBottomRightAlignmentItem1)})
        Me.ChangeFloatingObjectAlignmentItem1.Name = "ChangeFloatingObjectAlignmentItem1"
        '
        'SetFloatingObjectTopLeftAlignmentItem1
        '
        Me.SetFloatingObjectTopLeftAlignmentItem1.Id = 210
        Me.SetFloatingObjectTopLeftAlignmentItem1.Name = "SetFloatingObjectTopLeftAlignmentItem1"
        '
        'SetFloatingObjectTopCenterAlignmentItem1
        '
        Me.SetFloatingObjectTopCenterAlignmentItem1.Id = 211
        Me.SetFloatingObjectTopCenterAlignmentItem1.Name = "SetFloatingObjectTopCenterAlignmentItem1"
        '
        'SetFloatingObjectTopRightAlignmentItem1
        '
        Me.SetFloatingObjectTopRightAlignmentItem1.Id = 212
        Me.SetFloatingObjectTopRightAlignmentItem1.Name = "SetFloatingObjectTopRightAlignmentItem1"
        '
        'SetFloatingObjectMiddleLeftAlignmentItem1
        '
        Me.SetFloatingObjectMiddleLeftAlignmentItem1.Id = 213
        Me.SetFloatingObjectMiddleLeftAlignmentItem1.Name = "SetFloatingObjectMiddleLeftAlignmentItem1"
        '
        'SetFloatingObjectMiddleCenterAlignmentItem1
        '
        Me.SetFloatingObjectMiddleCenterAlignmentItem1.Id = 214
        Me.SetFloatingObjectMiddleCenterAlignmentItem1.Name = "SetFloatingObjectMiddleCenterAlignmentItem1"
        '
        'SetFloatingObjectMiddleRightAlignmentItem1
        '
        Me.SetFloatingObjectMiddleRightAlignmentItem1.Id = 215
        Me.SetFloatingObjectMiddleRightAlignmentItem1.Name = "SetFloatingObjectMiddleRightAlignmentItem1"
        '
        'SetFloatingObjectBottomLeftAlignmentItem1
        '
        Me.SetFloatingObjectBottomLeftAlignmentItem1.Id = 216
        Me.SetFloatingObjectBottomLeftAlignmentItem1.Name = "SetFloatingObjectBottomLeftAlignmentItem1"
        '
        'SetFloatingObjectBottomCenterAlignmentItem1
        '
        Me.SetFloatingObjectBottomCenterAlignmentItem1.Id = 217
        Me.SetFloatingObjectBottomCenterAlignmentItem1.Name = "SetFloatingObjectBottomCenterAlignmentItem1"
        '
        'SetFloatingObjectBottomRightAlignmentItem1
        '
        Me.SetFloatingObjectBottomRightAlignmentItem1.Id = 218
        Me.SetFloatingObjectBottomRightAlignmentItem1.Name = "SetFloatingObjectBottomRightAlignmentItem1"
        '
        'FloatingObjectBringForwardSubItem1
        '
        Me.FloatingObjectBringForwardSubItem1.Id = 219
        Me.FloatingObjectBringForwardSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.FloatingObjectBringForwardItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.FloatingObjectBringToFrontItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.FloatingObjectBringInFrontOfTextItem1)})
        Me.FloatingObjectBringForwardSubItem1.Name = "FloatingObjectBringForwardSubItem1"
        '
        'FloatingObjectBringForwardItem1
        '
        Me.FloatingObjectBringForwardItem1.Id = 220
        Me.FloatingObjectBringForwardItem1.Name = "FloatingObjectBringForwardItem1"
        '
        'FloatingObjectBringToFrontItem1
        '
        Me.FloatingObjectBringToFrontItem1.Id = 221
        Me.FloatingObjectBringToFrontItem1.Name = "FloatingObjectBringToFrontItem1"
        '
        'FloatingObjectBringInFrontOfTextItem1
        '
        Me.FloatingObjectBringInFrontOfTextItem1.Id = 222
        Me.FloatingObjectBringInFrontOfTextItem1.Name = "FloatingObjectBringInFrontOfTextItem1"
        '
        'FloatingObjectSendBackwardSubItem1
        '
        Me.FloatingObjectSendBackwardSubItem1.Id = 223
        Me.FloatingObjectSendBackwardSubItem1.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.FloatingObjectSendBackwardItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.FloatingObjectSendToBackItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.FloatingObjectSendBehindTextItem1)})
        Me.FloatingObjectSendBackwardSubItem1.Name = "FloatingObjectSendBackwardSubItem1"
        '
        'FloatingObjectSendBackwardItem1
        '
        Me.FloatingObjectSendBackwardItem1.Id = 224
        Me.FloatingObjectSendBackwardItem1.Name = "FloatingObjectSendBackwardItem1"
        '
        'FloatingObjectSendToBackItem1
        '
        Me.FloatingObjectSendToBackItem1.Id = 225
        Me.FloatingObjectSendToBackItem1.Name = "FloatingObjectSendToBackItem1"
        '
        'FloatingObjectSendBehindTextItem1
        '
        Me.FloatingObjectSendBehindTextItem1.Id = 226
        Me.FloatingObjectSendBehindTextItem1.Name = "FloatingObjectSendBehindTextItem1"
        '
        'INDgleTemplates
        '
        Me.IndigoGridLookUpControl1.SetAbrirFormularioArchivo(Me.INDgleTemplates, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDgleTemplates, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDgleTemplates, False)
        Me.IndigoGridLookUpControl1.SetGuardarXmlGrid(Me.INDgleTemplates, False)
        Me.INDgleTemplates.Location = New System.Drawing.Point(12, 40)
        Me.IndigoTextEdit1.SetMascara(Me.INDgleTemplates, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDgleTemplates.Name = "INDgleTemplates"
        Me.INDgleTemplates.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleTemplates.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgleTemplates.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDgleTemplates.Properties.Appearance.Options.UseBackColor = True
        Me.INDgleTemplates.Properties.Appearance.Options.UseFont = True
        Me.INDgleTemplates.Properties.Appearance.Options.UseForeColor = True
        Me.INDgleTemplates.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDgleTemplates.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.INDgleTemplates.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDgleTemplates.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDgleTemplates.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDgleTemplates.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDgleTemplates.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDgleTemplates.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.INDgleTemplates.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDgleTemplates.Properties.DisplayMember = "Name"
        Me.INDgleTemplates.Properties.ImmediatePopup = True
        Me.INDgleTemplates.Properties.NullText = "Seleccione un concepto"
        Me.INDgleTemplates.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDgleTemplates.Properties.PopupFormSize = New System.Drawing.Size(600, 0)
        Me.INDgleTemplates.Properties.PopupView = Me.GridView3
        Me.INDgleTemplates.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDgleTemplates.Properties.ValueMember = "Id"
        Me.INDgleTemplates.Size = New System.Drawing.Size(381, 28)
        Me.INDgleTemplates.StyleController = Me.LayoutControl1
        Me.INDgleTemplates.TabIndex = 10
        Me.IndigoGridLookUpControl1.SetTagFormularioAbrir(Me.INDgleTemplates, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDgleTemplates, 0)
        '
        'GridView3
        '
        Me.GridView3.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView3.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView3.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView3.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView3.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.GroupRow.Options.UseFont = True
        Me.GridView3.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView3.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView3.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView3.Appearance.Row.Options.UseFont = True
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn19, Me.GridColumn24, Me.GridColumn25})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Codigo"
        Me.GridColumn19.FieldName = "Code"
        Me.GridColumn19.MaxWidth = 120
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Visible = True
        Me.GridColumn19.VisibleIndex = 0
        Me.GridColumn19.Width = 120
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Planilla"
        Me.GridColumn24.FieldName = "Name"
        Me.GridColumn24.MaxWidth = 100
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Visible = True
        Me.GridColumn24.VisibleIndex = 1
        Me.GridColumn24.Width = 96
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Concepto"
        Me.GridColumn25.FieldName = "ConceptGlosas.ConceptCodeName"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Visible = True
        Me.GridColumn25.VisibleIndex = 2
        Me.GridColumn25.Width = 168
        '
        'SimpleButton3
        '
        Me.SimpleButton3.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SimpleButton3.Appearance.Options.UseFont = True
        Me.SimpleButton3.Location = New System.Drawing.Point(415, 40)
        Me.SimpleButton3.Name = "SimpleButton3"
        Me.SimpleButton3.Size = New System.Drawing.Size(173, 26)
        Me.SimpleButton3.StyleController = Me.LayoutControl1
        Me.SimpleButton3.TabIndex = 9
        Me.SimpleButton3.Text = "Aplicar"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem2, Me.LayoutControlItem1, Me.LayoutControlItem4, Me.LayoutControlItem3, Me.EmptySpaceItem1, Me.INDlyiResponseHierarchy})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1578, 740)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDgleTemplates
        Me.LayoutControlItem2.CustomizationFormText = "LayoutControlItem2"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 28)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 20, 2, 2)
        Me.LayoutControlItem2.Size = New System.Drawing.Size(403, 36)
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDrecComment
        Me.LayoutControlItem1.CustomizationFormText = "LayoutControlItem1"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 64)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(1558, 656)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.StandaloneBarDockControl1
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem4"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem4.Name = "LayoutControlItem4"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(1558, 28)
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem4.TextVisible = False
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.SimpleButton3
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem3"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(403, 28)
        Me.LayoutControlItem3.Name = "LayoutControlItem3"
        Me.LayoutControlItem3.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 20, 2, 2)
        Me.LayoutControlItem3.Size = New System.Drawing.Size(195, 36)
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem3.TextVisible = False
        '
        'EmptySpaceItem1
        '
        Me.EmptySpaceItem1.AllowHotTrack = False
        Me.EmptySpaceItem1.CustomizationFormText = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Location = New System.Drawing.Point(1148, 28)
        Me.EmptySpaceItem1.Name = "EmptySpaceItem1"
        Me.EmptySpaceItem1.Size = New System.Drawing.Size(410, 36)
        Me.EmptySpaceItem1.TextSize = New System.Drawing.Size(0, 0)
        '
        'INDlyiResponseHierarchy
        '
        Me.INDlyiResponseHierarchy.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlyiResponseHierarchy.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyiResponseHierarchy.Control = Me.INDgleResponseHierarchy
        Me.INDlyiResponseHierarchy.CustomizationFormText = "Concepto De Aceptación"
        Me.INDlyiResponseHierarchy.Location = New System.Drawing.Point(598, 28)
        Me.INDlyiResponseHierarchy.MaxSize = New System.Drawing.Size(550, 36)
        Me.INDlyiResponseHierarchy.MinSize = New System.Drawing.Size(550, 36)
        Me.INDlyiResponseHierarchy.Name = "INDlyiResponseHierarchy"
        Me.INDlyiResponseHierarchy.Size = New System.Drawing.Size(550, 36)
        Me.INDlyiResponseHierarchy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiResponseHierarchy.Text = "Concepto De Aceptación"
        Me.INDlyiResponseHierarchy.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiResponseHierarchy.TextSize = New System.Drawing.Size(170, 21)
        Me.INDlyiResponseHierarchy.TextToControlDistance = 12
        '
        'RichEditBarController1
        '
        Me.RichEditBarController1.BarItems.Add(Me.FileNewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FileOpenItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FileSaveItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FileSaveAsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.QuickPrintItem1)
        Me.RichEditBarController1.BarItems.Add(Me.PrintItem1)
        Me.RichEditBarController1.BarItems.Add(Me.PrintPreviewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.UndoItem1)
        Me.RichEditBarController1.BarItems.Add(Me.RedoItem1)
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
        Me.RichEditBarController1.BarItems.Add(Me.InsertPageBreakItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertPictureItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertFloatingPictureItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertBookmarkItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertHyperlinkItem1)
        Me.RichEditBarController1.BarItems.Add(Me.EditPageHeaderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.EditPageFooterItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertPageNumberItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertPageCountItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTextBoxItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertSymbolItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeSectionPageMarginsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetNormalSectionPageMarginsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetNarrowSectionPageMarginsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetModerateSectionPageMarginsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetWideSectionPageMarginsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowPageMarginsSetupFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeSectionPageOrientationItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetPortraitPageOrientationItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetLandscapePageOrientationItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeSectionPaperKindItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeSectionColumnsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionOneColumnItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionTwoColumnsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionThreeColumnsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowColumnsSetupFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertBreakItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertPageBreakItem2)
        Me.RichEditBarController1.BarItems.Add(Me.InsertColumnBreakItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertSectionBreakNextPageItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertSectionBreakEvenPageItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertSectionBreakOddPageItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeSectionLineNumberingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionLineNumberingNoneItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionLineNumberingContinuousItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionLineNumberingRestartNewPageItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetSectionLineNumberingRestartNewSectionItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleParagraphSuppressLineNumbersItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowLineNumberingFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangePageColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableOfContentsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.UpdateTableOfContentsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.AddParagraphsToTableOfContentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem2)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem3)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem4)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem5)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem6)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem7)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem8)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem9)
        Me.RichEditBarController1.BarItems.Add(Me.SetParagraphHeadingLevelItem10)
        Me.RichEditBarController1.BarItems.Add(Me.InsertCaptionPlaceholderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertFiguresCaptionItems1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTablesCaptionItems1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertEquationsCaptionItems1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableOfFiguresPlaceholderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableOfFiguresItems1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableOfTablesItems1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableOfEquationsItems1)
        Me.RichEditBarController1.BarItems.Add(Me.UpdateTableOfFiguresItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertMergeFieldItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowAllFieldCodesItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowAllFieldResultsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleViewMergedDataItem1)
        Me.RichEditBarController1.BarItems.Add(Me.CheckSpellingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ProtectDocumentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeRangeEditingPermissionsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.UnprotectDocumentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SwitchToSimpleViewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SwitchToDraftViewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SwitchToPrintLayoutViewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleShowHorizontalRulerItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleShowVerticalRulerItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ZoomOutItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ZoomInItem1)
        Me.RichEditBarController1.BarItems.Add(Me.GoToPageHeaderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.GoToPageFooterItem1)
        Me.RichEditBarController1.BarItems.Add(Me.GoToNextHeaderFooterItem1)
        Me.RichEditBarController1.BarItems.Add(Me.GoToPreviousHeaderFooterItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleLinkToPreviousItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleDifferentFirstPageItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleDifferentOddAndEvenPagesItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ClosePageHeaderFooterItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFirstRowItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleLastRowItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleBandedRowsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleFirstColumnItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleLastColumnItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleBandedColumnItem1)
        Me.RichEditBarController1.BarItems.Add(Me.GalleryChangeTableStyleItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeTableBorderLineStyleItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeTableBorderLineWeightItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeTableBorderColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeTableBordersItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsBottomBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsTopBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsLeftBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsRightBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ResetTableCellsAllBordersItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsAllBordersItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsOutsideBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsInsideBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsInsideHorizontalBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsInsideVerticalBorderItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleShowTableGridLinesItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeTableCellsShadingItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SelectTableElementsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SelectTableCellItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SelectTableColumnItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SelectTableRowItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SelectTableItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowTablePropertiesFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.DeleteTableElementsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowDeleteTableCellsFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.DeleteTableColumnsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.DeleteTableRowsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.DeleteTableItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableRowAboveItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableRowBelowItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableColumnToLeftItem1)
        Me.RichEditBarController1.BarItems.Add(Me.InsertTableColumnToRightItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowInsertTableCellsFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.MergeTableCellsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowSplitTableCellsForm1)
        Me.RichEditBarController1.BarItems.Add(Me.SplitTableItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableAutoFitItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableAutoFitContentsItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableAutoFitWindowItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableFixedColumnWidthItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsTopLeftAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsMiddleLeftAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsBottomLeftAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsTopCenterAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsMiddleCenterAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsBottomCenterAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsTopRightAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsMiddleRightAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleTableCellsBottomRightAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ShowTableOptionsFormItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFloatingObjectFillColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFloatingObjectOutlineColorItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFloatingObjectOutlineWeightItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFloatingObjectTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectSquareTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectTightTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectThroughTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectTopAndBottomTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectBehindTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectInFrontOfTextWrapTypeItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ChangeFloatingObjectAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectTopLeftAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectTopCenterAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectTopRightAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectMiddleLeftAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectMiddleCenterAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectMiddleRightAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectBottomLeftAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectBottomCenterAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SetFloatingObjectBottomRightAlignmentItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectBringForwardSubItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectBringForwardItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectBringToFrontItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectBringInFrontOfTextItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectSendBackwardSubItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectSendBackwardItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectSendToBackItem1)
        Me.RichEditBarController1.BarItems.Add(Me.FloatingObjectSendBehindTextItem1)
        Me.RichEditBarController1.Control = Me.INDrecComment
        '
        'InsertPageBreakItem2
        '
        Me.InsertPageBreakItem2.Name = "InsertPageBreakItem2"
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = RepositoryItemPopupContainerEdit1
        '
        'JustificationEvaluation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1386, 757)
        Me.Controls.Add(Me.LayoutControl1)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "JustificationEvaluation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Justificación Evaluación"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDgleResponseHierarchy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFontEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRichEditFontSizeEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemRichEditStyleEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemBorderLineStyle1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemBorderLineWeight1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFloatingObjectOutlineWeight1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgleTemplates.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmptySpaceItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiResponseHierarchy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RichEditBarController1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDgleTemplates As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SimpleButton3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents StandaloneBarDockControl1 As DevExpress.XtraBars.StandaloneBarDockControl
    Friend WithEvents INDrecComment As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents FileNewItem1 As DevExpress.XtraRichEdit.UI.FileNewItem
    Friend WithEvents FileOpenItem1 As DevExpress.XtraRichEdit.UI.FileOpenItem
    Friend WithEvents FileSaveItem1 As DevExpress.XtraRichEdit.UI.FileSaveItem
    Friend WithEvents FileSaveAsItem1 As DevExpress.XtraRichEdit.UI.FileSaveAsItem
    Friend WithEvents QuickPrintItem1 As DevExpress.XtraRichEdit.UI.QuickPrintItem
    Friend WithEvents PrintItem1 As DevExpress.XtraRichEdit.UI.PrintItem
    Friend WithEvents PrintPreviewItem1 As DevExpress.XtraRichEdit.UI.PrintPreviewItem
    Friend WithEvents UndoItem1 As DevExpress.XtraRichEdit.UI.UndoItem
    Friend WithEvents RedoItem1 As DevExpress.XtraRichEdit.UI.RedoItem
    Friend WithEvents ClipboardBar1 As DevExpress.XtraRichEdit.UI.ClipboardBar
    Friend WithEvents PasteItem1 As DevExpress.XtraRichEdit.UI.PasteItem
    Friend WithEvents CutItem1 As DevExpress.XtraRichEdit.UI.CutItem
    Friend WithEvents CopyItem1 As DevExpress.XtraRichEdit.UI.CopyItem
    Friend WithEvents PasteSpecialItem1 As DevExpress.XtraRichEdit.UI.PasteSpecialItem
    Friend WithEvents FontBar1 As DevExpress.XtraRichEdit.UI.FontBar
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
    Friend WithEvents ParagraphBar1 As DevExpress.XtraRichEdit.UI.ParagraphBar
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
    Friend WithEvents StylesBar1 As DevExpress.XtraRichEdit.UI.StylesBar
    Friend WithEvents ChangeStyleItem1 As DevExpress.XtraRichEdit.UI.ChangeStyleItem
    Friend WithEvents RepositoryItemRichEditStyleEdit1 As DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit
    Friend WithEvents ShowEditStyleFormItem1 As DevExpress.XtraRichEdit.UI.ShowEditStyleFormItem
    Friend WithEvents FindItem1 As DevExpress.XtraRichEdit.UI.FindItem
    Friend WithEvents ReplaceItem1 As DevExpress.XtraRichEdit.UI.ReplaceItem
    Friend WithEvents InsertPageBreakItem1 As DevExpress.XtraRichEdit.UI.InsertPageBreakItem
    Friend WithEvents InsertTableItem1 As DevExpress.XtraRichEdit.UI.InsertTableItem
    Friend WithEvents InsertPictureItem1 As DevExpress.XtraRichEdit.UI.InsertPictureItem
    Friend WithEvents InsertFloatingPictureItem1 As DevExpress.XtraRichEdit.UI.InsertFloatingPictureItem
    Friend WithEvents InsertBookmarkItem1 As DevExpress.XtraRichEdit.UI.InsertBookmarkItem
    Friend WithEvents InsertHyperlinkItem1 As DevExpress.XtraRichEdit.UI.InsertHyperlinkItem
    Friend WithEvents EditPageHeaderItem1 As DevExpress.XtraRichEdit.UI.EditPageHeaderItem
    Friend WithEvents EditPageFooterItem1 As DevExpress.XtraRichEdit.UI.EditPageFooterItem
    Friend WithEvents InsertPageNumberItem1 As DevExpress.XtraRichEdit.UI.InsertPageNumberItem
    Friend WithEvents InsertPageCountItem1 As DevExpress.XtraRichEdit.UI.InsertPageCountItem
    Friend WithEvents InsertTextBoxItem1 As DevExpress.XtraRichEdit.UI.InsertTextBoxItem
    Friend WithEvents InsertSymbolItem1 As DevExpress.XtraRichEdit.UI.InsertSymbolItem
    Friend WithEvents ChangeSectionPageMarginsItem1 As DevExpress.XtraRichEdit.UI.ChangeSectionPageMarginsItem
    Friend WithEvents SetNormalSectionPageMarginsItem1 As DevExpress.XtraRichEdit.UI.SetNormalSectionPageMarginsItem
    Friend WithEvents SetNarrowSectionPageMarginsItem1 As DevExpress.XtraRichEdit.UI.SetNarrowSectionPageMarginsItem
    Friend WithEvents SetModerateSectionPageMarginsItem1 As DevExpress.XtraRichEdit.UI.SetModerateSectionPageMarginsItem
    Friend WithEvents SetWideSectionPageMarginsItem1 As DevExpress.XtraRichEdit.UI.SetWideSectionPageMarginsItem
    Friend WithEvents ShowPageMarginsSetupFormItem1 As DevExpress.XtraRichEdit.UI.ShowPageMarginsSetupFormItem
    Friend WithEvents ChangeSectionPageOrientationItem1 As DevExpress.XtraRichEdit.UI.ChangeSectionPageOrientationItem
    Friend WithEvents SetPortraitPageOrientationItem1 As DevExpress.XtraRichEdit.UI.SetPortraitPageOrientationItem
    Friend WithEvents SetLandscapePageOrientationItem1 As DevExpress.XtraRichEdit.UI.SetLandscapePageOrientationItem
    Friend WithEvents ChangeSectionPaperKindItem1 As DevExpress.XtraRichEdit.UI.ChangeSectionPaperKindItem
    Friend WithEvents ChangeSectionColumnsItem1 As DevExpress.XtraRichEdit.UI.ChangeSectionColumnsItem
    Friend WithEvents SetSectionOneColumnItem1 As DevExpress.XtraRichEdit.UI.SetSectionOneColumnItem
    Friend WithEvents SetSectionTwoColumnsItem1 As DevExpress.XtraRichEdit.UI.SetSectionTwoColumnsItem
    Friend WithEvents SetSectionThreeColumnsItem1 As DevExpress.XtraRichEdit.UI.SetSectionThreeColumnsItem
    Friend WithEvents ShowColumnsSetupFormItem1 As DevExpress.XtraRichEdit.UI.ShowColumnsSetupFormItem
    Friend WithEvents InsertBreakItem1 As DevExpress.XtraRichEdit.UI.InsertBreakItem
    Friend WithEvents InsertColumnBreakItem1 As DevExpress.XtraRichEdit.UI.InsertColumnBreakItem
    Friend WithEvents InsertSectionBreakNextPageItem1 As DevExpress.XtraRichEdit.UI.InsertSectionBreakNextPageItem
    Friend WithEvents InsertSectionBreakEvenPageItem1 As DevExpress.XtraRichEdit.UI.InsertSectionBreakEvenPageItem
    Friend WithEvents InsertSectionBreakOddPageItem1 As DevExpress.XtraRichEdit.UI.InsertSectionBreakOddPageItem
    Friend WithEvents ChangeSectionLineNumberingItem1 As DevExpress.XtraRichEdit.UI.ChangeSectionLineNumberingItem
    Friend WithEvents SetSectionLineNumberingNoneItem1 As DevExpress.XtraRichEdit.UI.SetSectionLineNumberingNoneItem
    Friend WithEvents SetSectionLineNumberingContinuousItem1 As DevExpress.XtraRichEdit.UI.SetSectionLineNumberingContinuousItem
    Friend WithEvents SetSectionLineNumberingRestartNewPageItem1 As DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewPageItem
    Friend WithEvents SetSectionLineNumberingRestartNewSectionItem1 As DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewSectionItem
    Friend WithEvents ToggleParagraphSuppressLineNumbersItem1 As DevExpress.XtraRichEdit.UI.ToggleParagraphSuppressLineNumbersItem
    Friend WithEvents ShowLineNumberingFormItem1 As DevExpress.XtraRichEdit.UI.ShowLineNumberingFormItem
    Friend WithEvents ChangePageColorItem1 As DevExpress.XtraRichEdit.UI.ChangePageColorItem
    Friend WithEvents InsertTableOfContentsItem1 As DevExpress.XtraRichEdit.UI.InsertTableOfContentsItem
    Friend WithEvents UpdateTableOfContentsItem1 As DevExpress.XtraRichEdit.UI.UpdateTableOfContentsItem
    Friend WithEvents AddParagraphsToTableOfContentItem1 As DevExpress.XtraRichEdit.UI.AddParagraphsToTableOfContentItem
    Friend WithEvents SetParagraphHeadingLevelItem1 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem2 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem3 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem4 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem5 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem6 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem7 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem8 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem9 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents SetParagraphHeadingLevelItem10 As DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem
    Friend WithEvents InsertCaptionPlaceholderItem1 As DevExpress.XtraRichEdit.UI.InsertCaptionPlaceholderItem
    Friend WithEvents InsertFiguresCaptionItems1 As DevExpress.XtraRichEdit.UI.InsertFiguresCaptionItems
    Friend WithEvents InsertTablesCaptionItems1 As DevExpress.XtraRichEdit.UI.InsertTablesCaptionItems
    Friend WithEvents InsertEquationsCaptionItems1 As DevExpress.XtraRichEdit.UI.InsertEquationsCaptionItems
    Friend WithEvents InsertTableOfFiguresPlaceholderItem1 As DevExpress.XtraRichEdit.UI.InsertTableOfFiguresPlaceholderItem
    Friend WithEvents InsertTableOfFiguresItems1 As DevExpress.XtraRichEdit.UI.InsertTableOfFiguresItems
    Friend WithEvents InsertTableOfTablesItems1 As DevExpress.XtraRichEdit.UI.InsertTableOfTablesItems
    Friend WithEvents InsertTableOfEquationsItems1 As DevExpress.XtraRichEdit.UI.InsertTableOfEquationsItems
    Friend WithEvents UpdateTableOfFiguresItem1 As DevExpress.XtraRichEdit.UI.UpdateTableOfFiguresItem
    Friend WithEvents InsertMergeFieldItem1 As DevExpress.XtraRichEdit.UI.InsertMergeFieldItem
    Friend WithEvents ShowAllFieldCodesItem1 As DevExpress.XtraRichEdit.UI.ShowAllFieldCodesItem
    Friend WithEvents ShowAllFieldResultsItem1 As DevExpress.XtraRichEdit.UI.ShowAllFieldResultsItem
    Friend WithEvents ToggleViewMergedDataItem1 As DevExpress.XtraRichEdit.UI.ToggleViewMergedDataItem
    Friend WithEvents CheckSpellingItem1 As DevExpress.XtraRichEdit.UI.CheckSpellingItem
    Friend WithEvents ProtectDocumentItem1 As DevExpress.XtraRichEdit.UI.ProtectDocumentItem
    Friend WithEvents ChangeRangeEditingPermissionsItem1 As DevExpress.XtraRichEdit.UI.ChangeRangeEditingPermissionsItem
    Friend WithEvents UnprotectDocumentItem1 As DevExpress.XtraRichEdit.UI.UnprotectDocumentItem
    Friend WithEvents SwitchToSimpleViewItem1 As DevExpress.XtraRichEdit.UI.SwitchToSimpleViewItem
    Friend WithEvents SwitchToDraftViewItem1 As DevExpress.XtraRichEdit.UI.SwitchToDraftViewItem
    Friend WithEvents SwitchToPrintLayoutViewItem1 As DevExpress.XtraRichEdit.UI.SwitchToPrintLayoutViewItem
    Friend WithEvents ToggleShowHorizontalRulerItem1 As DevExpress.XtraRichEdit.UI.ToggleShowHorizontalRulerItem
    Friend WithEvents ToggleShowVerticalRulerItem1 As DevExpress.XtraRichEdit.UI.ToggleShowVerticalRulerItem
    Friend WithEvents ZoomOutItem1 As DevExpress.XtraRichEdit.UI.ZoomOutItem
    Friend WithEvents ZoomInItem1 As DevExpress.XtraRichEdit.UI.ZoomInItem
    Friend WithEvents GoToPageHeaderItem1 As DevExpress.XtraRichEdit.UI.GoToPageHeaderItem
    Friend WithEvents GoToPageFooterItem1 As DevExpress.XtraRichEdit.UI.GoToPageFooterItem
    Friend WithEvents GoToNextHeaderFooterItem1 As DevExpress.XtraRichEdit.UI.GoToNextHeaderFooterItem
    Friend WithEvents GoToPreviousHeaderFooterItem1 As DevExpress.XtraRichEdit.UI.GoToPreviousHeaderFooterItem
    Friend WithEvents ToggleLinkToPreviousItem1 As DevExpress.XtraRichEdit.UI.ToggleLinkToPreviousItem
    Friend WithEvents ToggleDifferentFirstPageItem1 As DevExpress.XtraRichEdit.UI.ToggleDifferentFirstPageItem
    Friend WithEvents ToggleDifferentOddAndEvenPagesItem1 As DevExpress.XtraRichEdit.UI.ToggleDifferentOddAndEvenPagesItem
    Friend WithEvents ClosePageHeaderFooterItem1 As DevExpress.XtraRichEdit.UI.ClosePageHeaderFooterItem
    Friend WithEvents ToggleFirstRowItem1 As DevExpress.XtraRichEdit.UI.ToggleFirstRowItem
    Friend WithEvents ToggleLastRowItem1 As DevExpress.XtraRichEdit.UI.ToggleLastRowItem
    Friend WithEvents ToggleBandedRowsItem1 As DevExpress.XtraRichEdit.UI.ToggleBandedRowsItem
    Friend WithEvents ToggleFirstColumnItem1 As DevExpress.XtraRichEdit.UI.ToggleFirstColumnItem
    Friend WithEvents ToggleLastColumnItem1 As DevExpress.XtraRichEdit.UI.ToggleLastColumnItem
    Friend WithEvents ToggleBandedColumnItem1 As DevExpress.XtraRichEdit.UI.ToggleBandedColumnItem
    Friend WithEvents GalleryChangeTableStyleItem1 As DevExpress.XtraRichEdit.UI.GalleryChangeTableStyleItem
    Friend WithEvents ChangeTableBorderLineStyleItem1 As DevExpress.XtraRichEdit.UI.ChangeTableBorderLineStyleItem
    Friend WithEvents RepositoryItemBorderLineStyle1 As DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle
    Friend WithEvents ChangeTableBorderLineWeightItem1 As DevExpress.XtraRichEdit.UI.ChangeTableBorderLineWeightItem
    Friend WithEvents RepositoryItemBorderLineWeight1 As DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight
    Friend WithEvents ChangeTableBorderColorItem1 As DevExpress.XtraRichEdit.UI.ChangeTableBorderColorItem
    Friend WithEvents ChangeTableBordersItem1 As DevExpress.XtraRichEdit.UI.ChangeTableBordersItem
    Friend WithEvents ToggleTableCellsBottomBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomBorderItem
    Friend WithEvents ToggleTableCellsTopBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsTopBorderItem
    Friend WithEvents ToggleTableCellsLeftBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsLeftBorderItem
    Friend WithEvents ToggleTableCellsRightBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsRightBorderItem
    Friend WithEvents ResetTableCellsAllBordersItem1 As DevExpress.XtraRichEdit.UI.ResetTableCellsAllBordersItem
    Friend WithEvents ToggleTableCellsAllBordersItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsAllBordersItem
    Friend WithEvents ToggleTableCellsOutsideBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsOutsideBorderItem
    Friend WithEvents ToggleTableCellsInsideBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideBorderItem
    Friend WithEvents ToggleTableCellsInsideHorizontalBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideHorizontalBorderItem
    Friend WithEvents ToggleTableCellsInsideVerticalBorderItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideVerticalBorderItem
    Friend WithEvents ToggleShowTableGridLinesItem1 As DevExpress.XtraRichEdit.UI.ToggleShowTableGridLinesItem
    Friend WithEvents ChangeTableCellsShadingItem1 As DevExpress.XtraRichEdit.UI.ChangeTableCellsShadingItem
    Friend WithEvents SelectTableElementsItem1 As DevExpress.XtraRichEdit.UI.SelectTableElementsItem
    Friend WithEvents SelectTableCellItem1 As DevExpress.XtraRichEdit.UI.SelectTableCellItem
    Friend WithEvents SelectTableColumnItem1 As DevExpress.XtraRichEdit.UI.SelectTableColumnItem
    Friend WithEvents SelectTableRowItem1 As DevExpress.XtraRichEdit.UI.SelectTableRowItem
    Friend WithEvents SelectTableItem1 As DevExpress.XtraRichEdit.UI.SelectTableItem
    Friend WithEvents ShowTablePropertiesFormItem1 As DevExpress.XtraRichEdit.UI.ShowTablePropertiesFormItem
    Friend WithEvents DeleteTableElementsItem1 As DevExpress.XtraRichEdit.UI.DeleteTableElementsItem
    Friend WithEvents ShowDeleteTableCellsFormItem1 As DevExpress.XtraRichEdit.UI.ShowDeleteTableCellsFormItem
    Friend WithEvents DeleteTableColumnsItem1 As DevExpress.XtraRichEdit.UI.DeleteTableColumnsItem
    Friend WithEvents DeleteTableRowsItem1 As DevExpress.XtraRichEdit.UI.DeleteTableRowsItem
    Friend WithEvents DeleteTableItem1 As DevExpress.XtraRichEdit.UI.DeleteTableItem
    Friend WithEvents InsertTableRowAboveItem1 As DevExpress.XtraRichEdit.UI.InsertTableRowAboveItem
    Friend WithEvents InsertTableRowBelowItem1 As DevExpress.XtraRichEdit.UI.InsertTableRowBelowItem
    Friend WithEvents InsertTableColumnToLeftItem1 As DevExpress.XtraRichEdit.UI.InsertTableColumnToLeftItem
    Friend WithEvents InsertTableColumnToRightItem1 As DevExpress.XtraRichEdit.UI.InsertTableColumnToRightItem
    Friend WithEvents ShowInsertTableCellsFormItem1 As DevExpress.XtraRichEdit.UI.ShowInsertTableCellsFormItem
    Friend WithEvents MergeTableCellsItem1 As DevExpress.XtraRichEdit.UI.MergeTableCellsItem
    Friend WithEvents ShowSplitTableCellsForm1 As DevExpress.XtraRichEdit.UI.ShowSplitTableCellsForm
    Friend WithEvents SplitTableItem1 As DevExpress.XtraRichEdit.UI.SplitTableItem
    Friend WithEvents ToggleTableAutoFitItem1 As DevExpress.XtraRichEdit.UI.ToggleTableAutoFitItem
    Friend WithEvents ToggleTableAutoFitContentsItem1 As DevExpress.XtraRichEdit.UI.ToggleTableAutoFitContentsItem
    Friend WithEvents ToggleTableAutoFitWindowItem1 As DevExpress.XtraRichEdit.UI.ToggleTableAutoFitWindowItem
    Friend WithEvents ToggleTableFixedColumnWidthItem1 As DevExpress.XtraRichEdit.UI.ToggleTableFixedColumnWidthItem
    Friend WithEvents ToggleTableCellsTopLeftAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsTopLeftAlignmentItem
    Friend WithEvents ToggleTableCellsMiddleLeftAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleLeftAlignmentItem
    Friend WithEvents ToggleTableCellsBottomLeftAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomLeftAlignmentItem
    Friend WithEvents ToggleTableCellsTopCenterAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsTopCenterAlignmentItem
    Friend WithEvents ToggleTableCellsMiddleCenterAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleCenterAlignmentItem
    Friend WithEvents ToggleTableCellsBottomCenterAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomCenterAlignmentItem
    Friend WithEvents ToggleTableCellsTopRightAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsTopRightAlignmentItem
    Friend WithEvents ToggleTableCellsMiddleRightAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleRightAlignmentItem
    Friend WithEvents ToggleTableCellsBottomRightAlignmentItem1 As DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomRightAlignmentItem
    Friend WithEvents ShowTableOptionsFormItem1 As DevExpress.XtraRichEdit.UI.ShowTableOptionsFormItem
    Friend WithEvents ChangeFloatingObjectFillColorItem1 As DevExpress.XtraRichEdit.UI.ChangeFloatingObjectFillColorItem
    Friend WithEvents ChangeFloatingObjectOutlineColorItem1 As DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineColorItem
    Friend WithEvents ChangeFloatingObjectOutlineWeightItem1 As DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineWeightItem
    Friend WithEvents RepositoryItemFloatingObjectOutlineWeight1 As DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight
    Friend WithEvents ChangeFloatingObjectTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.ChangeFloatingObjectTextWrapTypeItem
    Friend WithEvents SetFloatingObjectSquareTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectSquareTextWrapTypeItem
    Friend WithEvents SetFloatingObjectTightTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectTightTextWrapTypeItem
    Friend WithEvents SetFloatingObjectThroughTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectThroughTextWrapTypeItem
    Friend WithEvents SetFloatingObjectTopAndBottomTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectTopAndBottomTextWrapTypeItem
    Friend WithEvents SetFloatingObjectBehindTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectBehindTextWrapTypeItem
    Friend WithEvents SetFloatingObjectInFrontOfTextWrapTypeItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectInFrontOfTextWrapTypeItem
    Friend WithEvents ChangeFloatingObjectAlignmentItem1 As DevExpress.XtraRichEdit.UI.ChangeFloatingObjectAlignmentItem
    Friend WithEvents SetFloatingObjectTopLeftAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectTopLeftAlignmentItem
    Friend WithEvents SetFloatingObjectTopCenterAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectTopCenterAlignmentItem
    Friend WithEvents SetFloatingObjectTopRightAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectTopRightAlignmentItem
    Friend WithEvents SetFloatingObjectMiddleLeftAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleLeftAlignmentItem
    Friend WithEvents SetFloatingObjectMiddleCenterAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleCenterAlignmentItem
    Friend WithEvents SetFloatingObjectMiddleRightAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleRightAlignmentItem
    Friend WithEvents SetFloatingObjectBottomLeftAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomLeftAlignmentItem
    Friend WithEvents SetFloatingObjectBottomCenterAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomCenterAlignmentItem
    Friend WithEvents SetFloatingObjectBottomRightAlignmentItem1 As DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomRightAlignmentItem
    Friend WithEvents FloatingObjectBringForwardSubItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardSubItem
    Friend WithEvents FloatingObjectBringForwardItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardItem
    Friend WithEvents FloatingObjectBringToFrontItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectBringToFrontItem
    Friend WithEvents FloatingObjectBringInFrontOfTextItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectBringInFrontOfTextItem
    Friend WithEvents FloatingObjectSendBackwardSubItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardSubItem
    Friend WithEvents FloatingObjectSendBackwardItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardItem
    Friend WithEvents FloatingObjectSendToBackItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectSendToBackItem
    Friend WithEvents FloatingObjectSendBehindTextItem1 As DevExpress.XtraRichEdit.UI.FloatingObjectSendBehindTextItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RichEditBarController1 As DevExpress.XtraRichEdit.UI.RichEditBarController
    Friend WithEvents InsertPageBreakItem2 As DevExpress.XtraRichEdit.UI.InsertPageBreakItem
    Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents INDgleResponseHierarchy As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyiResponseHierarchy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
End Class
