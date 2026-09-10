Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccountingVoucher
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
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccountingVoucher))
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup6 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemLegalBook = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDSleLegalBook = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyJournalVoucher = New DevExpress.XtraLayout.LayoutControl()
        Me.INDBtnImportFile = New DevExpress.XtraEditors.SimpleButton()
        Me.INDEsbDetails = New Presentation.Controls.ExportStructureButton()
        Me.INDHleDocument = New DevExpress.XtraEditors.HyperLinkEdit()
        Me.indglTypeDocument = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.btnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.indMemoObservation = New DevExpress.XtraEditors.MemoEdit()
        Me.indGcDetail = New DevExpress.XtraGrid.GridControl()
        Me.viewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColAccount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColThird = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColCostCenter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColDebits = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCreditValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.repositoryDebit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.repositoryCredit = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.indDtDateDocument = New DevExpress.XtraEditors.DateEdit()
        Me.indGlConsecutive = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDtxtDocument = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlyItemConsecutive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemTypeDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDateDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocument = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemDocumentLink = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservation = New DevExpress.XtraLayout.LayoutControlItem()
        Me.groupDetail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem22 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.itemadd = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.IndigoGridLookUpControl1 = New Presentation.Controls.IndigoGridLookUpControl(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemLegalBook, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSleLegalBook.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyJournalVoucher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyJournalVoucher.SuspendLayout()
        CType(Me.INDHleDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indglTypeDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indMemoObservation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indGcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.viewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryDebit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repositoryCredit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indDtDateDocument.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indDtDateDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.indGlConsecutive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemConsecutive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemTypeDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDateDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemDocumentLink, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.groupDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.itemadd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyJournalVoucher)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1537, 627)
        Me.INDPanelControlBase.TabIndex = 0
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Size = New System.Drawing.Size(1537, 130)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1537, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'LayoutControlGroup1
        '
        Me.LayoutControlGroup1.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup1.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup1.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup1, False)
        Me.LayoutControlGroup1.CustomizationFormText = "Comprobante Contable"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup6, Me.groupDetail})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(1333, 618)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'LayoutControlGroup6
        '
        Me.LayoutControlGroup6.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup6.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup6, False)
        Me.LayoutControlGroup6.CustomizationFormText = "Comprobante Diario"
        Me.LayoutControlGroup6.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemLegalBook, Me.INDlyItemConsecutive, Me.INDlyItemTypeDocument, Me.INDlyItemDateDocument, Me.INDlyItemDocument, Me.INDlyItemDocumentLink, Me.INDlyItemObservation})
        Me.LayoutControlGroup6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup6.Name = "LayoutControlGroup6"
        Me.LayoutControlGroup6.Size = New System.Drawing.Size(414, 598)
        Me.LayoutControlGroup6.Text = "Comprobante Diario"
        '
        'INDlyItemLegalBook
        '
        Me.INDlyItemLegalBook.AllowHide = False
        Me.INDlyItemLegalBook.Control = Me.INDSleLegalBook
        Me.INDlyItemLegalBook.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemLegalBook.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemLegalBook.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemLegalBook.Name = "INDlyItemLegalBook"
        Me.INDlyItemLegalBook.ShowInCustomizationForm = False
        Me.INDlyItemLegalBook.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemLegalBook.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemLegalBook.Text = "Libro Oficial"
        Me.INDlyItemLegalBook.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemLegalBook.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemLegalBook.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemLegalBook.TextToControlDistance = 5
        '
        'INDSleLegalBook
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDSleLegalBook, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDSleLegalBook, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.INDSleLegalBook, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDSleLegalBook, False)
        Me.INDSleLegalBook.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDSleLegalBook, False)
        Me.INDSleLegalBook.Location = New System.Drawing.Point(24, 78)
        Me.IndigoTextEdit1.SetMascara(Me.INDSleLegalBook, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSleLegalBook.Name = "INDSleLegalBook"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDSleLegalBook, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDSleLegalBook, False)
        Me.INDSleLegalBook.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSleLegalBook.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDSleLegalBook.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.INDSleLegalBook.Properties.Appearance.Options.UseBackColor = True
        Me.INDSleLegalBook.Properties.Appearance.Options.UseFont = True
        Me.INDSleLegalBook.Properties.Appearance.Options.UseForeColor = True
        Me.INDSleLegalBook.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSleLegalBook.Properties.DisplayMember = "CodeName"
        Me.INDSleLegalBook.Properties.NullText = ""
        Me.INDSleLegalBook.Properties.PopupSizeable = False
        Me.INDSleLegalBook.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDSleLegalBook.Properties.ShowFooter = False
        Me.INDSleLegalBook.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDSleLegalBook, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDSleLegalBook, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDSleLegalBook, True)
        Me.INDSleLegalBook.Size = New System.Drawing.Size(386, 28)
        Me.INDSleLegalBook.StyleController = Me.INDlyJournalVoucher
        Me.INDSleLegalBook.TabIndex = 19
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDSleLegalBook, "1682")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSleLegalBook, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDSleLegalBook, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDSleLegalBook, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDSleLegalBook, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3, Me.GridColumn4})
        Me.SearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit1View.Name = "SearchLookUpEdit1View"
        Me.SearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit1View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit1View, False)
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Código"
        Me.GridColumn2.FieldName = "Code"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 0
        Me.GridColumn2.Width = 264
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 564
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Libro Oficial"
        Me.GridColumn4.FieldName = "OfficialBook"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 564
        '
        'INDlyJournalVoucher
        '
        Me.INDlyJournalVoucher.AllowCustomization = False
        Me.INDlyJournalVoucher.Controls.Add(Me.INDSleLegalBook)
        Me.INDlyJournalVoucher.Controls.Add(Me.INDBtnImportFile)
        Me.INDlyJournalVoucher.Controls.Add(Me.INDEsbDetails)
        Me.INDlyJournalVoucher.Controls.Add(Me.INDHleDocument)
        Me.INDlyJournalVoucher.Controls.Add(Me.indglTypeDocument)
        Me.INDlyJournalVoucher.Controls.Add(Me.btnAdd)
        Me.INDlyJournalVoucher.Controls.Add(Me.indMemoObservation)
        Me.INDlyJournalVoucher.Controls.Add(Me.indGcDetail)
        Me.INDlyJournalVoucher.Controls.Add(Me.indDtDateDocument)
        Me.INDlyJournalVoucher.Controls.Add(Me.indGlConsecutive)
        Me.INDlyJournalVoucher.Controls.Add(Me.INDtxtDocument)
        Me.INDlyJournalVoucher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControls.SetIsCustomizable(Me.INDlyJournalVoucher, False)
        Me.INDlyJournalVoucher.Location = New System.Drawing.Point(202, 7)
        Me.INDlyJournalVoucher.Name = "INDlyJournalVoucher"
        Me.INDlyJournalVoucher.Root = Me.LayoutControlGroup1
        Me.INDlyJournalVoucher.Size = New System.Drawing.Size(1333, 618)
        Me.INDlyJournalVoucher.TabIndex = 0
        Me.INDlyJournalVoucher.Text = "LayoutControl1"
        '
        'INDBtnImportFile
        '
        Me.INDBtnImportFile.ImageOptions.Image = CType(resources.GetObject("INDBtnImportFile.ImageOptions.Image"), System.Drawing.Image)
        Me.INDBtnImportFile.Location = New System.Drawing.Point(1273, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDBtnImportFile, False)
        Me.INDBtnImportFile.Name = "INDBtnImportFile"
        Me.INDBtnImportFile.Size = New System.Drawing.Size(34, 32)
        Me.INDBtnImportFile.StyleController = Me.INDlyJournalVoucher
        Me.INDBtnImportFile.TabIndex = 18
        Me.INDBtnImportFile.ToolTip = "Importar Archivo"
        '
        'INDEsbDetails
        '
        Me.INDEsbDetails.ImageOptions.Image = CType(resources.GetObject("INDEsbDetails.ImageOptions.Image"), System.Drawing.Image)
        Me.INDEsbDetails.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter
        Me.INDEsbDetails.Location = New System.Drawing.Point(1235, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.INDEsbDetails, False)
        Me.INDEsbDetails.Name = "INDEsbDetails"
        Me.INDEsbDetails.Size = New System.Drawing.Size(34, 32)
        Me.INDEsbDetails.StyleController = Me.INDlyJournalVoucher
        Me.INDEsbDetails.TabIndex = 15
        Me.INDEsbDetails.Text = "ExportStructureButton3"
        Me.INDEsbDetails.ToolTip = "Exportar Estructura"
        '
        'INDHleDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDHleDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDHleDocument, False)
        Me.INDHleDocument.EditValue = "Código - Tipo Documento"
        Me.INDHleDocument.EnterMoveNextControl = True
        Me.INDHleDocument.Location = New System.Drawing.Point(24, 372)
        Me.IndigoTextEdit1.SetMascara(Me.INDHleDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDHleDocument.Name = "INDHleDocument"
        Me.INDHleDocument.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDHleDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Underline)
        Me.INDHleDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDHleDocument.Properties.Appearance.Options.UseFont = True
        Me.INDHleDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDHleDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDHleDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDHleDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDHleDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDHleDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDHleDocument.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDHleDocument.Size = New System.Drawing.Size(386, 26)
        Me.INDHleDocument.StyleController = Me.INDlyJournalVoucher
        Me.INDHleDocument.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDHleDocument, 0)
        '
        'indglTypeDocument
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.indglTypeDocument, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.indglTypeDocument, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetAutomaticOpenForm(Me.indglTypeDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.indglTypeDocument, False)
        Me.indglTypeDocument.EditValue = ""
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.indglTypeDocument, False)
        Me.indglTypeDocument.Location = New System.Drawing.Point(24, 195)
        Me.IndigoTextEdit1.SetMascara(Me.indglTypeDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.indglTypeDocument.Name = "indglTypeDocument"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.indglTypeDocument, True)
        Me.IndigoSearchLookUpControl1.SetPopupBestFitHeight(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetPopupSizeable(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.indglTypeDocument, False)
        Me.indglTypeDocument.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.indglTypeDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.indglTypeDocument.Properties.Appearance.Options.UseBackColor = True
        Me.indglTypeDocument.Properties.Appearance.Options.UseFont = True
        Me.indglTypeDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.indglTypeDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.indglTypeDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indglTypeDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.indglTypeDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.indglTypeDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.indglTypeDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.indglTypeDocument.Properties.DisplayMember = "CodeName"
        Me.indglTypeDocument.Properties.NullText = ""
        Me.indglTypeDocument.Properties.PopupSizeable = False
        Me.indglTypeDocument.Properties.PopupView = Me.GridView1
        Me.indglTypeDocument.Properties.ShowClearButton = False
        Me.indglTypeDocument.Properties.ShowFooter = False
        Me.indglTypeDocument.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.indglTypeDocument, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.indglTypeDocument, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.indglTypeDocument, True)
        Me.indglTypeDocument.Size = New System.Drawing.Size(386, 28)
        Me.indglTypeDocument.StyleController = Me.INDlyJournalVoucher
        Me.indglTypeDocument.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.indglTypeDocument, "607")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.indglTypeDocument, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.indglTypeDocument, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.indglTypeDocument, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.indglTypeDocument, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn11, Me.GridColumn12, Me.GridColumn15, Me.GridColumn16})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "id"
        Me.GridColumn11.FieldName = "Id"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Código"
        Me.GridColumn12.FieldName = "Code"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Visible = True
        Me.GridColumn12.VisibleIndex = 0
        Me.GridColumn12.Width = 215
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Descripción"
        Me.GridColumn15.FieldName = "Name"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 1
        Me.GridColumn15.Width = 1143
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "CodName"
        Me.GridColumn16.FieldName = "CodeName"
        Me.GridColumn16.Name = "GridColumn16"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(438, 53)
        Me.IndigoSimpleButton1.SetModernUiIndigo(Me.btnAdd, False)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(793, 32)
        Me.btnAdd.StyleController = Me.INDlyJournalVoucher
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "Agregar"
        '
        'indMemoObservation
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.indMemoObservation, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.indMemoObservation, False)
        Me.indMemoObservation.Location = New System.Drawing.Point(24, 432)
        Me.IndigoTextEdit1.SetMascara(Me.indMemoObservation, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.indMemoObservation.Name = "indMemoObservation"
        Me.indMemoObservation.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.indMemoObservation.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indMemoObservation.Properties.Appearance.Options.UseBackColor = True
        Me.indMemoObservation.Properties.Appearance.Options.UseFont = True
        Me.indMemoObservation.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.indMemoObservation.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.indMemoObservation.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indMemoObservation.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.indMemoObservation.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.indMemoObservation.Properties.AppearanceFocused.Options.UseFont = True
        Me.indMemoObservation.Size = New System.Drawing.Size(386, 151)
        Me.indMemoObservation.StyleController = Me.INDlyJournalVoucher
        Me.indMemoObservation.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.indMemoObservation, 0)
        '
        'indGcDetail
        '
        Me.IndigoGridControl1.SetAddActions(Me.indGcDetail, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.indGcDetail, Nothing)
        Me.indGcDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.indGcDetail, True)
        Me.IndigoGridControl1.SetGuardarXml(Me.indGcDetail, True)
        Me.IndigoGridControl1.SetHoldSize(Me.indGcDetail, False)
        Me.IndigoGridControl1.SetHotTrack(Me.indGcDetail, False)
        Me.indGcDetail.Location = New System.Drawing.Point(440, 91)
        Me.indGcDetail.MainView = Me.viewDetail
        Me.indGcDetail.Name = "indGcDetail"
        Me.indGcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repositoryDebit, Me.repositoryCredit})
        Me.indGcDetail.Size = New System.Drawing.Size(865, 489)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.indGcDetail, DevExpress.XtraLayout.SizeConstraintsType.Custom)
        Me.indGcDetail.TabIndex = 6
        Me.indGcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.viewDetail})
        '
        'viewDetail
        '
        Me.viewDetail.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.viewDetail.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.viewDetail.Appearance.FocusedRow.Options.UseBackColor = True
        Me.viewDetail.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.viewDetail.Appearance.FocusedRow.Options.UseFont = True
        Me.viewDetail.Appearance.FocusedRow.Options.UseForeColor = True
        Me.viewDetail.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewDetail.Appearance.GroupRow.Options.UseFont = True
        Me.viewDetail.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.viewDetail.Appearance.HeaderPanel.Options.UseFont = True
        Me.viewDetail.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.viewDetail.Appearance.Row.Options.UseFont = True
        Me.viewDetail.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.viewDetail.Appearance.ViewCaption.Options.UseFont = True
        Me.viewDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColAccount, Me.ColThird, Me.ColCostCenter, Me.GridColumn7, Me.INDColDebits, Me.INDColCreditValue, Me.GridColumn1, Me.GridColumn13, Me.GridColumn14})
        Me.viewDetail.GridControl = Me.indGcDetail
        Me.viewDetail.Name = "viewDetail"
        Me.viewDetail.OptionsView.EnableAppearanceEvenRow = True
        Me.viewDetail.OptionsView.EnableAppearanceOddRow = True
        Me.viewDetail.OptionsView.ShowAutoFilterRow = True
        Me.viewDetail.OptionsView.ShowDetailButtons = False
        Me.viewDetail.OptionsView.ShowFooter = True
        Me.viewDetail.OptionsView.ShowGroupPanel = False
        Me.viewDetail.Tag = 291
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.viewDetail, False)
        '
        'ColAccount
        '
        Me.ColAccount.Caption = "Cuenta Contable"
        Me.ColAccount.FieldName = "CodeNameMainAccount"
        Me.ColAccount.Name = "ColAccount"
        Me.ColAccount.OptionsColumn.AllowEdit = False
        Me.ColAccount.OptionsColumn.AllowFocus = False
        Me.ColAccount.Visible = True
        Me.ColAccount.VisibleIndex = 0
        Me.ColAccount.Width = 385
        '
        'ColThird
        '
        Me.ColThird.Caption = "Tercero"
        Me.ColThird.FieldName = "CodeNameThirdParty"
        Me.ColThird.Name = "ColThird"
        Me.ColThird.OptionsColumn.AllowEdit = False
        Me.ColThird.OptionsColumn.AllowFocus = False
        Me.ColThird.Visible = True
        Me.ColThird.VisibleIndex = 1
        Me.ColThird.Width = 258
        '
        'ColCostCenter
        '
        Me.ColCostCenter.Caption = "Centro de Costo"
        Me.ColCostCenter.FieldName = "CodeNameCostCenter"
        Me.ColCostCenter.Name = "ColCostCenter"
        Me.ColCostCenter.OptionsColumn.AllowEdit = False
        Me.ColCostCenter.OptionsColumn.AllowFocus = False
        Me.ColCostCenter.Visible = True
        Me.ColCostCenter.VisibleIndex = 2
        Me.ColCostCenter.Width = 247
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Observación"
        Me.GridColumn7.FieldName = "Detail"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.AllowFocus = False
        Me.GridColumn7.Visible = True
        Me.GridColumn7.VisibleIndex = 3
        Me.GridColumn7.Width = 203
        '
        'INDColDebits
        '
        Me.INDColDebits.Caption = "Débitos"
        Me.INDColDebits.FieldName = "DebitValue"
        Me.INDColDebits.Name = "INDColDebits"
        Me.INDColDebits.OptionsColumn.AllowEdit = False
        Me.INDColDebits.OptionsColumn.AllowFocus = False
        Me.INDColDebits.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DebitValue", "{0:c2}")})
        Me.INDColDebits.Visible = True
        Me.INDColDebits.VisibleIndex = 4
        Me.INDColDebits.Width = 143
        '
        'INDColCreditValue
        '
        Me.INDColCreditValue.Caption = "Créditos"
        Me.INDColCreditValue.FieldName = "CreditValue"
        Me.INDColCreditValue.Name = "INDColCreditValue"
        Me.INDColCreditValue.OptionsColumn.AllowEdit = False
        Me.INDColCreditValue.OptionsColumn.AllowFocus = False
        Me.INDColCreditValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CreditValue", "{0:c2}")})
        Me.INDColCreditValue.Visible = True
        Me.INDColCreditValue.VisibleIndex = 5
        Me.INDColCreditValue.Width = 156
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Id Cuenta Contable"
        Me.GridColumn1.FieldName = "IdMainAccount"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Width = 20
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Id Tercero"
        Me.GridColumn13.FieldName = "IdThirdParty"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Width = 20
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Id Centro Costo"
        Me.GridColumn14.FieldName = "IdCostCenter"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Width = 20
        '
        'repositoryDebit
        '
        Me.repositoryDebit.AutoHeight = False
        Me.repositoryDebit.Mask.EditMask = "C2"
        Me.repositoryDebit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.repositoryDebit.Mask.UseMaskAsDisplayFormat = True
        Me.repositoryDebit.Name = "repositoryDebit"
        '
        'repositoryCredit
        '
        Me.repositoryCredit.AutoHeight = False
        Me.repositoryCredit.Mask.EditMask = "C2"
        Me.repositoryCredit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        Me.repositoryCredit.Mask.UseMaskAsDisplayFormat = True
        Me.repositoryCredit.Name = "repositoryCredit"
        '
        'indDtDateDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.indDtDateDocument, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.indDtDateDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.indDtDateDocument, False)
        Me.indDtDateDocument.EditValue = Nothing
        Me.indDtDateDocument.Location = New System.Drawing.Point(24, 255)
        Me.IndigoTextEdit1.SetMascara(Me.indDtDateDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.indDtDateDocument, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.indDtDateDocument.Name = "indDtDateDocument"
        Me.indDtDateDocument.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.indDtDateDocument.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.indDtDateDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indDtDateDocument.Properties.Appearance.Options.UseBackColor = True
        Me.indDtDateDocument.Properties.Appearance.Options.UseFont = True
        Me.indDtDateDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.indDtDateDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.indDtDateDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indDtDateDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.indDtDateDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.indDtDateDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.indDtDateDocument.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.indDtDateDocument.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.indDtDateDocument.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.indDtDateDocument.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.indDtDateDocument.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.indDtDateDocument.Size = New System.Drawing.Size(386, 28)
        Me.indDtDateDocument.StyleController = Me.INDlyJournalVoucher
        Me.indDtDateDocument.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.indDtDateDocument, 0)
        '
        'indGlConsecutive
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.indGlConsecutive, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.indGlConsecutive, False)
        Me.indGlConsecutive.EditValue = ""
        Me.indGlConsecutive.Location = New System.Drawing.Point(24, 138)
        Me.IndigoTextEdit1.SetMascara(Me.indGlConsecutive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.indGlConsecutive.Name = "indGlConsecutive"
        Me.indGlConsecutive.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.indGlConsecutive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indGlConsecutive.Properties.Appearance.Options.UseBackColor = True
        Me.indGlConsecutive.Properties.Appearance.Options.UseFont = True
        Me.indGlConsecutive.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.indGlConsecutive.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.indGlConsecutive.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.indGlConsecutive.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.indGlConsecutive.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.indGlConsecutive.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions1.Image = Global.Presentation.Accounting.My.Resources.Resources.BuscarMetro
        Me.indGlConsecutive.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.indGlConsecutive.Properties.Mask.EditMask = "\d{0,15}"
        Me.indGlConsecutive.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.indGlConsecutive.Properties.NullText = "[EditValue is null]"
        Me.indGlConsecutive.Size = New System.Drawing.Size(386, 28)
        Me.indGlConsecutive.StyleController = Me.INDlyJournalVoucher
        Me.indGlConsecutive.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.indGlConsecutive, 0)
        '
        'INDtxtDocument
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtDocument, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtDocument, False)
        Me.INDtxtDocument.EditValue = "Código - Tipo Documento"
        Me.INDtxtDocument.EnterMoveNextControl = True
        Me.INDtxtDocument.Location = New System.Drawing.Point(24, 312)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtDocument, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtDocument.Name = "INDtxtDocument"
        Me.INDtxtDocument.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtDocument.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDocument.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtDocument.Properties.Appearance.Options.UseFont = True
        Me.INDtxtDocument.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDtxtDocument.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDtxtDocument.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDtxtDocument.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtDocument.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.INDtxtDocument.Properties.ReadOnly = True
        Me.INDtxtDocument.Size = New System.Drawing.Size(386, 26)
        Me.INDtxtDocument.StyleController = Me.INDlyJournalVoucher
        Me.INDtxtDocument.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtDocument, 0)
        '
        'INDlyItemConsecutive
        '
        Me.INDlyItemConsecutive.AllowHide = False
        Me.INDlyItemConsecutive.Control = Me.indGlConsecutive
        Me.INDlyItemConsecutive.CustomizationFormText = "Consecutivo"
        Me.INDlyItemConsecutive.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemConsecutive.Name = "INDlyItemConsecutive"
        Me.INDlyItemConsecutive.ShowInCustomizationForm = False
        Me.INDlyItemConsecutive.Size = New System.Drawing.Size(390, 57)
        Me.INDlyItemConsecutive.Text = "Consecutivo"
        Me.INDlyItemConsecutive.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemConsecutive.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemConsecutive.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemConsecutive.TextToControlDistance = 5
        '
        'INDlyItemTypeDocument
        '
        Me.INDlyItemTypeDocument.Control = Me.indglTypeDocument
        Me.INDlyItemTypeDocument.CustomizationFormText = "Tipo De Comprobante Contable"
        Me.INDlyItemTypeDocument.Location = New System.Drawing.Point(0, 117)
        Me.INDlyItemTypeDocument.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTypeDocument.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemTypeDocument.Name = "INDlyItemTypeDocument"
        Me.INDlyItemTypeDocument.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemTypeDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemTypeDocument.Text = "Tipo De Comprobante Contable"
        Me.INDlyItemTypeDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemTypeDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemTypeDocument.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemTypeDocument.TextToControlDistance = 5
        '
        'INDlyItemDateDocument
        '
        Me.INDlyItemDateDocument.Control = Me.indDtDateDocument
        Me.INDlyItemDateDocument.CustomizationFormText = "Fecha"
        Me.INDlyItemDateDocument.Location = New System.Drawing.Point(0, 177)
        Me.INDlyItemDateDocument.Name = "INDlyItemDateDocument"
        Me.INDlyItemDateDocument.ShowInCustomizationForm = False
        Me.INDlyItemDateDocument.Size = New System.Drawing.Size(390, 57)
        Me.INDlyItemDateDocument.Text = "Fecha"
        Me.INDlyItemDateDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDateDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDateDocument.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemDateDocument.TextToControlDistance = 5
        '
        'INDlyItemDocument
        '
        Me.INDlyItemDocument.Control = Me.INDtxtDocument
        Me.INDlyItemDocument.CustomizationFormText = "Documento Origen"
        Me.INDlyItemDocument.Location = New System.Drawing.Point(0, 234)
        Me.INDlyItemDocument.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocument.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocument.Name = "INDlyItemDocument"
        Me.INDlyItemDocument.ShowInCustomizationForm = False
        Me.INDlyItemDocument.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocument.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocument.Text = "Documento Origen"
        Me.INDlyItemDocument.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDocument.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocument.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemDocument.TextToControlDistance = 5
        '
        'INDlyItemDocumentLink
        '
        Me.INDlyItemDocumentLink.AllowHide = False
        Me.INDlyItemDocumentLink.Control = Me.INDHleDocument
        Me.INDlyItemDocumentLink.CustomizationFormText = "Documento Origen"
        Me.INDlyItemDocumentLink.Location = New System.Drawing.Point(0, 294)
        Me.INDlyItemDocumentLink.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentLink.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentLink.Name = "INDlyItemDocumentLink"
        Me.INDlyItemDocumentLink.ShowInCustomizationForm = False
        Me.INDlyItemDocumentLink.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemDocumentLink.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemDocumentLink.Text = "Documento Origen"
        Me.INDlyItemDocumentLink.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemDocumentLink.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemDocumentLink.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemDocumentLink.TextToControlDistance = 5
        '
        'INDlyItemObservation
        '
        Me.INDlyItemObservation.Control = Me.indMemoObservation
        Me.INDlyItemObservation.CustomizationFormText = "Observaciones"
        Me.INDlyItemObservation.Location = New System.Drawing.Point(0, 354)
        Me.INDlyItemObservation.MaxSize = New System.Drawing.Size(390, 180)
        Me.INDlyItemObservation.MinSize = New System.Drawing.Size(390, 180)
        Me.INDlyItemObservation.Name = "INDlyItemObservation"
        Me.INDlyItemObservation.ShowInCustomizationForm = False
        Me.INDlyItemObservation.Size = New System.Drawing.Size(390, 191)
        Me.INDlyItemObservation.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservation.Text = "Observaciones"
        Me.INDlyItemObservation.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservation.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservation.TextSize = New System.Drawing.Size(50, 20)
        Me.INDlyItemObservation.TextToControlDistance = 5
        '
        'groupDetail
        '
        Me.groupDetail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupDetail.AppearanceGroup.Options.UseFont = True
        Me.groupDetail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.groupDetail.AppearanceItemCaption.Options.UseFont = True
        Me.groupDetail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.groupDetail.AppearanceTabPage.Header.Options.UseFont = True
        Me.groupDetail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.groupDetail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.groupDetail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.groupDetail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.groupDetail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.groupDetail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.groupDetail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.groupDetail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.groupDetail, False)
        Me.groupDetail.CustomizationFormText = "Listado de Movimientos"
        Me.groupDetail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem22, Me.itemadd, Me.LayoutControlItem5, Me.LayoutControlItem6})
        Me.groupDetail.Location = New System.Drawing.Point(414, 0)
        Me.groupDetail.Name = "groupDetail"
        Me.groupDetail.Size = New System.Drawing.Size(899, 598)
        Me.groupDetail.Text = "Listado de Movimientos"
        '
        'LayoutControlItem22
        '
        Me.LayoutControlItem22.Control = Me.indGcDetail
        Me.LayoutControlItem22.CustomizationFormText = "Movimientos"
        Me.LayoutControlItem22.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem22.MaxSize = New System.Drawing.Size(873, 497)
        Me.LayoutControlItem22.MinSize = New System.Drawing.Size(873, 497)
        Me.LayoutControlItem22.Name = "LayoutControlItem22"
        Me.LayoutControlItem22.Padding = New DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4)
        Me.LayoutControlItem22.Size = New System.Drawing.Size(875, 509)
        Me.LayoutControlItem22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem22.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem22.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem22.TextToControlDistance = 0
        Me.LayoutControlItem22.TextVisible = False
        '
        'itemadd
        '
        Me.itemadd.Control = Me.btnAdd
        Me.itemadd.CustomizationFormText = "Agregar"
        Me.itemadd.Location = New System.Drawing.Point(0, 0)
        Me.itemadd.MaxSize = New System.Drawing.Size(797, 36)
        Me.itemadd.MinSize = New System.Drawing.Size(797, 36)
        Me.itemadd.Name = "itemadd"
        Me.itemadd.Size = New System.Drawing.Size(797, 36)
        Me.itemadd.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.itemadd.TextSize = New System.Drawing.Size(0, 0)
        Me.itemadd.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDEsbDetails
        Me.LayoutControlItem5.Location = New System.Drawing.Point(797, 0)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.Control = Me.INDBtnImportFile
        Me.LayoutControlItem6.Location = New System.Drawing.Point(835, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(38, 36)
        Me.LayoutControlItem6.Name = "LayoutControlItem6"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(40, 36)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem6.TextVisible = False
        '
        'IndigoGridControl1
        '
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        Me.IndigoGridView1.RepositoryItemPopupContainerEdit = Me.RepositoryItemPopupContainerEdit1
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyJournalVoucher
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 618)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmAccountingVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1537, 762)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmAccountingVoucher"
        Me.Opacity = 1.0R
        Me.Tag = "610"
        Me.Text = "Comprobante Contable"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemLegalBook, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSleLegalBook.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyJournalVoucher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyJournalVoucher.ResumeLayout(False)
        CType(Me.INDHleDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indglTypeDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indMemoObservation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indGcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.viewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryDebit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repositoryCredit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indDtDateDocument.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indDtDateDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.indGlConsecutive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtDocument.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemConsecutive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemTypeDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDateDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemDocumentLink, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.groupDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.itemadd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents INDlyJournalVoucher As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemConsecutive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents indDtDateDocument As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemDateDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents indGcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents viewDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoGridLookUpControl1 As Presentation.Controls.IndigoGridLookUpControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents LayoutControlItem22 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents indGlConsecutive As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ColAccount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColThird As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColCostCenter As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColDebits As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDColCreditValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents LayoutControlGroup6 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents groupDetail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents indMemoObservation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObservation As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents btnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents itemadd As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents repositoryDebit As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents repositoryCredit As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents IndigoSimpleButton1 As Presentation.Controls.IndigoSimpleButton
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoSearchLookUpControl1 As Presentation.Controls.IndigoSearchLookUpControl
    Friend WithEvents indglTypeDocument As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemTypeDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemDocument As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtDocument As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDHleDocument As DevExpress.XtraEditors.HyperLinkEdit
    Friend WithEvents INDlyItemDocumentLink As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDEsbDetails As Presentation.Controls.ExportStructureButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtnImportFile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents INDSleLegalBook As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlyItemLegalBook As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
End Class
