Imports Presentation.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInvoiceRadicate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInvoiceRadicate))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions4 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject13 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject14 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject15 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject16 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepositoryItemPopupContainerEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDpccInvoice = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl2 = New DevExpress.XtraLayout.LayoutControl()
        Me.INDchkSelectFiltered = New DevExpress.XtraEditors.CheckEdit()
        Me.BarManager1 = New DevExpress.XtraBars.BarManager(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.INDchkSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.INDbtnAgregarFactura = New DevExpress.XtraEditors.SimpleButton()
        Me.INDBtndisplayAdvancedSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.INDgcInvoices = New DevExpress.XtraGrid.GridControl()
        Me.INDgcvInvoices = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Selection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrchSeleccion = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColDevolution = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageDevolution = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.INDimcInvoiceState = New DevExpress.Utils.ImageCollection(Me.components)
        Me.InvoiceNumberToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.InvoiceDateToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ContractCodeToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RadicatedNumberToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RadicatedDateToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PatientCodeToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PatientNameToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.IngressNumberToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ContractNameToPersist = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDgridclAccountantAccountCustomers = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDrcbiState = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.CtrAdvancedFilter = New Presentation.Controls.CtrAdvancedFilter()
        Me.LayoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDLytFiltrosAvanzada = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLytRejillaFacturas = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDLytAddInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiSelectAll = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDbteNit = New Presentation.Controls.SearchLookUpEditEx()
        Me.SearchLookUpEditExView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PopupContainerControlMoreInfo = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl6 = New DevExpress.XtraLayout.LayoutControl()
        Me.CtrXtraInfoConceptDevolution = New Presentation.Controls.CtrXtraInfo()
        Me.LayoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDpccMore = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.INDmeComment = New DevExpress.XtraEditors.MemoEdit()
        Me.INDpceMore = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDgcRadicateD = New DevExpress.XtraGrid.GridControl()
        Me.INDgvRadicateD = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Clselection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.INDclDevolution = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemImageDevolution2 = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclInvoice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclDateInvoice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclValue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdCurrencyAbbreviation = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDConceptDevolution = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEditMoreInfo = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDclActions = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonEditActions = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.INDclInvoiceValuePacient = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclIngress = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclAccountantAccountCustomers = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclCodeContract = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclValueEntity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDColCUV = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPopupContainerEditActions = New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
        Me.INDpccAcciones = New DevExpress.XtraEditors.PopupContainerControl()
        Me.INDbtnEliminar = New DevExpress.XtraEditors.SimpleButton()
        Me.INDMoreColumaPcc = New DevExpress.XtraEditors.PopupContainerControl()
        Me.LayoutControl3 = New DevExpress.XtraLayout.LayoutControl()
        Me.CtrXtraInfoInvoiceDetail = New Presentation.Controls.CtrXtraInfo()
        Me.LayoutControlGroup5 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.RepositoryItemMemoEditObservation = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.INDdeDocumentDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDlblDateRadicate = New DevExpress.XtraEditors.LabelControl()
        Me.INDpceBuscaFactura = New DevExpress.XtraEditors.PopupContainerEdit()
        Me.INDglCompany = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.INDgvSucursales = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.INDclNameContainers = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDclAccountingMethod = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDbteConsecutive = New DevExpress.XtraEditors.ButtonEdit()
        Me.INDlycRoot = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygGeneralData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiConsecutive = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiDateRadication = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiDateOfice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygInvoice = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyiContainer = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiInvoice = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDSendDocumentCommentMte = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControl4 = New DevExpress.XtraLayout.LayoutControl()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.INDSendDocumentDateDte = New DevExpress.XtraEditors.DateEdit()
        Me.INDUserRadicateTxt = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup12 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup10 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiUserConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyiDateConfirm = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.MemoEdit1 = New DevExpress.XtraEditors.MemoEdit()
        Me.LayoutControl5 = New DevExpress.XtraLayout.LayoutControl()
        Me.SimpleButton3 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton4 = New DevExpress.XtraEditors.SimpleButton()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.LayoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoDate2 = New Presentation.Controls.IndigoDate(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl1.SuspendLayout()
        CType(Me.INDpccInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccInvoice.SuspendLayout()
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl2.SuspendLayout()
        CType(Me.INDchkSelectFiltered.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDchkSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrchSeleccion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageDevolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDimcInvoiceState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDrcbiState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLytFiltrosAvanzada, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLytRejillaFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDLytAddInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteNit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PopupContainerControlMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PopupContainerControlMoreInfo.SuspendLayout()
        CType(Me.LayoutControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl6.SuspendLayout()
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccMore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccMore.SuspendLayout()
        CType(Me.INDmeComment.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceMore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgcRadicateD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvRadicateD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemImageDevolution2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEditMoreInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonEditActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPopupContainerEditActions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpccAcciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDpccAcciones.SuspendLayout()
        CType(Me.INDMoreColumaPcc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDMoreColumaPcc.SuspendLayout()
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl3.SuspendLayout()
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemMemoEditObservation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDpceBuscaFactura.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDglCompany.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDgvSucursales, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDbteConsecutive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneralData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiConsecutive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiDateRadication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiDateOfice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSendDocumentCommentMte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl4.SuspendLayout()
        CType(Me.INDSendDocumentDateDte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDSendDocumentDateDte.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDUserRadicateTxt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiUserConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyiDateConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.LayoutControl5.SuspendLayout()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.LayoutControl1)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        Me.INDPanelControlBase.Location = New System.Drawing.Point(0, 135)
        Me.INDPanelControlBase.Size = New System.Drawing.Size(1495, 548)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        Me.ToolBars.Controls.Add(Me.INDpccAcciones)
        Me.ToolBars.Controls.Add(Me.LayoutControl4)
        Me.ToolBars.Size = New System.Drawing.Size(1495, 130)
        Me.ToolBars.Controls.SetChildIndex(Me.LayoutControl4, 0)
        Me.ToolBars.Controls.SetChildIndex(Me.INDpccAcciones, 0)
        Me.ToolBars.Controls.SetChildIndex(Me.BarraBotones, 0)
        '
        'BarraBotones
        '
        Me.BarraBotones.Size = New System.Drawing.Size(1495, 130)
        '
        'RepositoryItemPopupContainerEdit1
        '
        Me.RepositoryItemPopupContainerEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEdit1.Name = "RepositoryItemPopupContainerEdit1"
        '
        'LayoutControl1
        '
        Me.LayoutControl1.Controls.Add(Me.INDpccInvoice)
        Me.LayoutControl1.Controls.Add(Me.INDbteNit)
        Me.LayoutControl1.Controls.Add(Me.PopupContainerControlMoreInfo)
        Me.LayoutControl1.Controls.Add(Me.INDpccMore)
        Me.LayoutControl1.Controls.Add(Me.INDpceMore)
        Me.LayoutControl1.Controls.Add(Me.INDgcRadicateD)
        Me.LayoutControl1.Controls.Add(Me.INDdeDocumentDate)
        Me.LayoutControl1.Controls.Add(Me.INDlblDateRadicate)
        Me.LayoutControl1.Controls.Add(Me.INDpceBuscaFactura)
        Me.LayoutControl1.Controls.Add(Me.INDglCompany)
        Me.LayoutControl1.Controls.Add(Me.INDbteConsecutive)
        Me.LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl1.Location = New System.Drawing.Point(202, 7)
        Me.LayoutControl1.Name = "LayoutControl1"
        Me.LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(629, 483, 250, 350)
        Me.LayoutControl1.Root = Me.INDlycRoot
        Me.LayoutControl1.Size = New System.Drawing.Size(1291, 539)
        Me.LayoutControl1.TabIndex = 0
        Me.LayoutControl1.Text = "LayoutControl1"
        '
        'INDpccInvoice
        '
        Me.INDpccInvoice.Controls.Add(Me.LayoutControl2)
        Me.INDpccInvoice.Location = New System.Drawing.Point(541, 144)
        Me.INDpccInvoice.Name = "INDpccInvoice"
        Me.INDpccInvoice.Size = New System.Drawing.Size(783, 340)
        Me.INDpccInvoice.TabIndex = 15
        '
        'LayoutControl2
        '
        Me.LayoutControl2.Controls.Add(Me.INDchkSelectFiltered)
        Me.LayoutControl2.Controls.Add(Me.INDchkSelectAll)
        Me.LayoutControl2.Controls.Add(Me.INDbtnAgregarFactura)
        Me.LayoutControl2.Controls.Add(Me.INDBtndisplayAdvancedSearch)
        Me.LayoutControl2.Controls.Add(Me.INDgcInvoices)
        Me.LayoutControl2.Controls.Add(Me.CtrAdvancedFilter)
        Me.LayoutControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl2.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl2.Name = "LayoutControl2"
        Me.LayoutControl2.Root = Me.LayoutControlGroup1
        Me.LayoutControl2.Size = New System.Drawing.Size(783, 340)
        Me.LayoutControl2.TabIndex = 0
        Me.LayoutControl2.Text = "LayoutControl2"
        '
        'INDchkSelectFiltered
        '
        Me.INDchkSelectFiltered.Location = New System.Drawing.Point(212, 218)
        Me.INDchkSelectFiltered.MenuManager = Me.BarManager1
        Me.INDchkSelectFiltered.Name = "INDchkSelectFiltered"
        Me.INDchkSelectFiltered.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.INDchkSelectFiltered.Properties.Appearance.Options.UseFont = True
        Me.INDchkSelectFiltered.Properties.Caption = "Seleccionar Filtrado"
        Me.INDchkSelectFiltered.Size = New System.Drawing.Size(196, 21)
        Me.INDchkSelectFiltered.StyleController = Me.LayoutControl2
        Me.INDchkSelectFiltered.TabIndex = 14
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
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlTop.Manager = Me.BarManager1
        Me.barDockControlTop.Size = New System.Drawing.Size(1495, 0)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 683)
        Me.barDockControlBottom.Manager = Me.BarManager1
        Me.barDockControlBottom.Size = New System.Drawing.Size(1495, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 5)
        Me.barDockControlLeft.Manager = Me.BarManager1
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 678)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1495, 5)
        Me.barDockControlRight.Manager = Me.BarManager1
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 678)
        '
        'INDchkSelectAll
        '
        Me.INDchkSelectAll.Location = New System.Drawing.Point(12, 218)
        Me.INDchkSelectAll.MenuManager = Me.BarManager1
        Me.INDchkSelectAll.Name = "INDchkSelectAll"
        Me.INDchkSelectAll.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDchkSelectAll.Properties.Appearance.Options.UseFont = True
        Me.INDchkSelectAll.Properties.Caption = "Seleccionar Todos"
        Me.INDchkSelectAll.Size = New System.Drawing.Size(196, 21)
        Me.INDchkSelectAll.StyleController = Me.LayoutControl2
        Me.INDchkSelectAll.TabIndex = 13
        '
        'INDbtnAgregarFactura
        '
        Me.INDbtnAgregarFactura.Location = New System.Drawing.Point(679, 296)
        Me.INDbtnAgregarFactura.Name = "INDbtnAgregarFactura"
        Me.INDbtnAgregarFactura.Size = New System.Drawing.Size(92, 32)
        Me.INDbtnAgregarFactura.StyleController = Me.LayoutControl2
        Me.INDbtnAgregarFactura.TabIndex = 11
        Me.INDbtnAgregarFactura.Text = "Agregar"
        '
        'INDBtndisplayAdvancedSearch
        '
        Me.INDBtndisplayAdvancedSearch.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDBtndisplayAdvancedSearch.Appearance.Options.UseFont = True
        Me.INDBtndisplayAdvancedSearch.Location = New System.Drawing.Point(12, 296)
        Me.INDBtndisplayAdvancedSearch.Name = "INDBtndisplayAdvancedSearch"
        Me.INDBtndisplayAdvancedSearch.Size = New System.Drawing.Size(196, 32)
        Me.INDBtndisplayAdvancedSearch.StyleController = Me.LayoutControl2
        Me.INDBtndisplayAdvancedSearch.TabIndex = 9
        Me.INDBtndisplayAdvancedSearch.Text = "Ocultar Filtros"
        '
        'INDgcInvoices
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcInvoices, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcInvoices, Nothing)
        Me.INDgcInvoices.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcInvoices, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcInvoices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcInvoices, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcInvoices, False)
        Me.INDgcInvoices.Location = New System.Drawing.Point(12, 243)
        Me.INDgcInvoices.MainView = Me.INDgcvInvoices
        Me.INDgcInvoices.Name = "INDgcInvoices"
        Me.INDgcInvoices.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.INDrcbiState, Me.INDrchSeleccion, Me.RepositoryItemImageDevolution})
        Me.INDgcInvoices.Size = New System.Drawing.Size(759, 49)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcInvoices, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcInvoices.TabIndex = 5
        Me.INDgcInvoices.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgcvInvoices})
        '
        'INDgcvInvoices
        '
        Me.INDgcvInvoices.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgcvInvoices.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgcvInvoices.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgcvInvoices.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgcvInvoices.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgcvInvoices.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgcvInvoices.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgcvInvoices.Appearance.GroupRow.Options.UseFont = True
        Me.INDgcvInvoices.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgcvInvoices.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgcvInvoices.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgcvInvoices.Appearance.Row.Options.UseFont = True
        Me.INDgcvInvoices.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgcvInvoices.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgcvInvoices.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Selection, Me.ColDevolution, Me.InvoiceNumberToPersist, Me.InvoiceDateToPersist, Me.GridColumn4, Me.ContractCodeToPersist, Me.GridColumn6, Me.RadicatedNumberToPersist, Me.RadicatedDateToPersist, Me.PatientCodeToPersist, Me.PatientNameToPersist, Me.IngressNumberToPersist, Me.ContractNameToPersist, Me.INDgridclAccountantAccountCustomers})
        Me.INDgcvInvoices.GridControl = Me.INDgcInvoices
        Me.INDgcvInvoices.Name = "INDgcvInvoices"
        Me.INDgcvInvoices.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgcvInvoices.OptionsView.EnableAppearanceOddRow = True
        Me.INDgcvInvoices.OptionsView.ShowAutoFilterRow = True
        Me.INDgcvInvoices.OptionsView.ShowGroupPanel = False
        Me.INDgcvInvoices.Tag = 95
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgcvInvoices, False)
        '
        'Selection
        '
        Me.Selection.Caption = "Selección"
        Me.Selection.ColumnEdit = Me.INDrchSeleccion
        Me.Selection.FieldName = "Selection"
        Me.Selection.Name = "Selection"
        Me.Selection.Visible = True
        Me.Selection.VisibleIndex = 0
        Me.Selection.Width = 68
        '
        'INDrchSeleccion
        '
        Me.INDrchSeleccion.Caption = "Check"
        Me.INDrchSeleccion.Name = "INDrchSeleccion"
        '
        'ColDevolution
        '
        Me.ColDevolution.Caption = "Devolución"
        Me.ColDevolution.ColumnEdit = Me.RepositoryItemImageDevolution
        Me.ColDevolution.FieldName = "Devolution"
        Me.ColDevolution.MaxWidth = 30
        Me.ColDevolution.MinWidth = 30
        Me.ColDevolution.Name = "ColDevolution"
        Me.ColDevolution.OptionsColumn.AllowEdit = False
        Me.ColDevolution.Visible = True
        Me.ColDevolution.VisibleIndex = 1
        Me.ColDevolution.Width = 30
        '
        'RepositoryItemImageDevolution
        '
        Me.RepositoryItemImageDevolution.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageDevolution.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Devolucion", 1, 0)})
        Me.RepositoryItemImageDevolution.LargeImages = Me.INDimcInvoiceState
        Me.RepositoryItemImageDevolution.Name = "RepositoryItemImageDevolution"
        '
        'INDimcInvoiceState
        '
        Me.INDimcInvoiceState.ImageStream = CType(resources.GetObject("INDimcInvoiceState.ImageStream"), DevExpress.Utils.ImageCollectionStreamer)
        Me.INDimcInvoiceState.InsertImage(Global.Presentation.Glosas.My.Resources.Resources.ConfirmedState16, "ConfirmedState16", GetType(Global.Presentation.Glosas.My.Resources.Resources), 0)
        Me.INDimcInvoiceState.Images.SetKeyName(0, "ConfirmedState16")
        Me.INDimcInvoiceState.InsertImage(Global.Presentation.Glosas.My.Resources.Resources.UnconfirmedState16, "UnconfirmedState16", GetType(Global.Presentation.Glosas.My.Resources.Resources), 1)
        Me.INDimcInvoiceState.Images.SetKeyName(1, "UnconfirmedState16")
        '
        'InvoiceNumberToPersist
        '
        Me.InvoiceNumberToPersist.Caption = "N° factura"
        Me.InvoiceNumberToPersist.FieldName = "InvoiceNumber"
        Me.InvoiceNumberToPersist.Name = "InvoiceNumberToPersist"
        Me.InvoiceNumberToPersist.OptionsColumn.AllowEdit = False
        Me.InvoiceNumberToPersist.OptionsColumn.AllowFocus = False
        Me.InvoiceNumberToPersist.OptionsColumn.ReadOnly = True
        Me.InvoiceNumberToPersist.SortMode = DevExpress.XtraGrid.ColumnSortMode.Custom
        Me.InvoiceNumberToPersist.Visible = True
        Me.InvoiceNumberToPersist.VisibleIndex = 2
        Me.InvoiceNumberToPersist.Width = 78
        '
        'InvoiceDateToPersist
        '
        Me.InvoiceDateToPersist.Caption = "Fecha Factura"
        Me.InvoiceDateToPersist.DisplayFormat.FormatString = "d"
        Me.InvoiceDateToPersist.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.InvoiceDateToPersist.FieldName = "InvoiceDate"
        Me.InvoiceDateToPersist.Name = "InvoiceDateToPersist"
        Me.InvoiceDateToPersist.OptionsColumn.AllowEdit = False
        Me.InvoiceDateToPersist.OptionsColumn.AllowFocus = False
        Me.InvoiceDateToPersist.OptionsColumn.ReadOnly = True
        Me.InvoiceDateToPersist.Visible = True
        Me.InvoiceDateToPersist.VisibleIndex = 3
        Me.InvoiceDateToPersist.Width = 81
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Categoría"
        Me.GridColumn4.FieldName = "InvoiceCategory"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowFocus = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 4
        '
        'ContractCodeToPersist
        '
        Me.ContractCodeToPersist.Caption = "Contrato"
        Me.ContractCodeToPersist.FieldName = "ContractCode"
        Me.ContractCodeToPersist.Name = "ContractCodeToPersist"
        Me.ContractCodeToPersist.OptionsColumn.AllowEdit = False
        Me.ContractCodeToPersist.OptionsColumn.AllowFocus = False
        Me.ContractCodeToPersist.OptionsColumn.ReadOnly = True
        Me.ContractCodeToPersist.Visible = True
        Me.ContractCodeToPersist.VisibleIndex = 5
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Grupo Atención"
        Me.GridColumn6.FieldName = "CodePlan"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 6
        '
        'RadicatedNumberToPersist
        '
        Me.RadicatedNumberToPersist.Caption = "Numero Radicado"
        Me.RadicatedNumberToPersist.FieldName = "RadicatedNumber"
        Me.RadicatedNumberToPersist.Name = "RadicatedNumberToPersist"
        Me.RadicatedNumberToPersist.OptionsColumn.AllowEdit = False
        Me.RadicatedNumberToPersist.OptionsColumn.AllowFocus = False
        Me.RadicatedNumberToPersist.OptionsColumn.ReadOnly = True
        '
        'RadicatedDateToPersist
        '
        Me.RadicatedDateToPersist.Caption = "Fecha Radicado"
        Me.RadicatedDateToPersist.DisplayFormat.FormatString = "d"
        Me.RadicatedDateToPersist.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RadicatedDateToPersist.FieldName = "RadicatedDate"
        Me.RadicatedDateToPersist.Name = "RadicatedDateToPersist"
        Me.RadicatedDateToPersist.OptionsColumn.AllowEdit = False
        Me.RadicatedDateToPersist.OptionsColumn.AllowFocus = False
        Me.RadicatedDateToPersist.OptionsColumn.ReadOnly = True
        '
        'PatientCodeToPersist
        '
        Me.PatientCodeToPersist.Caption = "Identificación"
        Me.PatientCodeToPersist.FieldName = "PatientCode"
        Me.PatientCodeToPersist.Name = "PatientCodeToPersist"
        Me.PatientCodeToPersist.OptionsColumn.AllowEdit = False
        Me.PatientCodeToPersist.OptionsColumn.AllowFocus = False
        Me.PatientCodeToPersist.OptionsColumn.ReadOnly = True
        Me.PatientCodeToPersist.Visible = True
        Me.PatientCodeToPersist.VisibleIndex = 7
        Me.PatientCodeToPersist.Width = 120
        '
        'PatientNameToPersist
        '
        Me.PatientNameToPersist.Caption = "Paciente"
        Me.PatientNameToPersist.FieldName = "PatientName"
        Me.PatientNameToPersist.Name = "PatientNameToPersist"
        Me.PatientNameToPersist.OptionsColumn.AllowEdit = False
        Me.PatientNameToPersist.OptionsColumn.AllowFocus = False
        Me.PatientNameToPersist.OptionsColumn.ReadOnly = True
        Me.PatientNameToPersist.Visible = True
        Me.PatientNameToPersist.VisibleIndex = 8
        Me.PatientNameToPersist.Width = 176
        '
        'IngressNumberToPersist
        '
        Me.IngressNumberToPersist.Caption = "Ingreso"
        Me.IngressNumberToPersist.FieldName = "IngressNumber"
        Me.IngressNumberToPersist.Name = "IngressNumberToPersist"
        Me.IngressNumberToPersist.OptionsColumn.AllowEdit = False
        Me.IngressNumberToPersist.OptionsColumn.AllowFocus = False
        Me.IngressNumberToPersist.OptionsColumn.ReadOnly = True
        '
        'ContractNameToPersist
        '
        Me.ContractNameToPersist.Caption = "Nombre Contrato"
        Me.ContractNameToPersist.FieldName = "ContractName"
        Me.ContractNameToPersist.Name = "ContractNameToPersist"
        Me.ContractNameToPersist.OptionsColumn.AllowEdit = False
        Me.ContractNameToPersist.OptionsColumn.AllowFocus = False
        Me.ContractNameToPersist.OptionsColumn.ReadOnly = True
        '
        'INDgridclAccountantAccountCustomers
        '
        Me.INDgridclAccountantAccountCustomers.Caption = "Cuenta "
        Me.INDgridclAccountantAccountCustomers.FieldName = "AccountantAccountCustomers"
        Me.INDgridclAccountantAccountCustomers.Name = "INDgridclAccountantAccountCustomers"
        Me.INDgridclAccountantAccountCustomers.OptionsColumn.AllowEdit = False
        Me.INDgridclAccountantAccountCustomers.OptionsColumn.AllowFocus = False
        Me.INDgridclAccountantAccountCustomers.OptionsColumn.ReadOnly = True
        '
        'INDrcbiState
        '
        Me.INDrcbiState.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDrcbiState.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Factura Glosada", "7", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Factura Sin Confirmar", "1", 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Factura Radicada Sin Confirmada", "T", 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Factura Anulada", "6", 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Factura Radicada Confirmada", "2", 4)})
        Me.INDrcbiState.Name = "INDrcbiState"
        '
        'CtrAdvancedFilter
        '
        Me.CtrAdvancedFilter.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.CtrAdvancedFilter.Appearance.Options.UseBackColor = True
        Me.CtrAdvancedFilter.CriteriaLabel = "Criterios"
        Me.CtrAdvancedFilter.DataLabel = "Valor"
        Me.CtrAdvancedFilter.DisplayStringFormatDateTime = "dd MMM yyyy - HH:mm:ss"
        Me.CtrAdvancedFilter.DsplayStringFormatDate = "dd MMM yyyy"
        Me.CtrAdvancedFilter.FieldsLabel = "Campos"
        Me.CtrAdvancedFilter.ListFields = Nothing
        Me.CtrAdvancedFilter.ListTopResults = CType(resources.GetObject("CtrAdvancedFilter.ListTopResults"), System.Collections.ObjectModel.ObservableCollection(Of String))
        Me.CtrAdvancedFilter.Location = New System.Drawing.Point(12, 12)
        Me.CtrAdvancedFilter.Name = "CtrAdvancedFilter"
        Me.CtrAdvancedFilter.OperatorLabel = "Operador"
        Me.CtrAdvancedFilter.Size = New System.Drawing.Size(759, 202)
        Me.CtrAdvancedFilter.StringFormatDate = "yyyyMMdd"
        Me.CtrAdvancedFilter.StringFormatDateTime = "yyyyMMdd HH:mm:ss"
        Me.CtrAdvancedFilter.TabIndex = 4
        Me.CtrAdvancedFilter.Title = "Búsqueda Avanzada"
        Me.CtrAdvancedFilter.TitleFilterlistLabel = "Lista de Filtros"
        Me.CtrAdvancedFilter.TopResultsLabel = "No. Resultados"
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
        Me.LayoutControlGroup1.CustomizationFormText = "LayoutControlGroup1"
        Me.LayoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup1.GroupBordersVisible = False
        Me.LayoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDLytFiltrosAvanzada, Me.INDLytRejillaFacturas, Me.LayoutControlItem5, Me.INDLytAddInvoice, Me.INDlyiSelectAll, Me.LayoutControlItem16})
        Me.LayoutControlGroup1.Name = "LayoutControlGroup1"
        Me.LayoutControlGroup1.Size = New System.Drawing.Size(783, 340)
        Me.LayoutControlGroup1.TextVisible = False
        '
        'INDLytFiltrosAvanzada
        '
        Me.INDLytFiltrosAvanzada.Control = Me.CtrAdvancedFilter
        Me.INDLytFiltrosAvanzada.CustomizationFormText = "INDLytFiltrosAvanzada"
        Me.INDLytFiltrosAvanzada.Location = New System.Drawing.Point(0, 0)
        Me.INDLytFiltrosAvanzada.Name = "INDLytFiltrosAvanzada"
        Me.INDLytFiltrosAvanzada.Size = New System.Drawing.Size(763, 206)
        Me.INDLytFiltrosAvanzada.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLytFiltrosAvanzada.TextVisible = False
        '
        'INDLytRejillaFacturas
        '
        Me.INDLytRejillaFacturas.Control = Me.INDgcInvoices
        Me.INDLytRejillaFacturas.CustomizationFormText = "INDLytRejillaFacturas"
        Me.INDLytRejillaFacturas.Location = New System.Drawing.Point(0, 231)
        Me.INDLytRejillaFacturas.Name = "INDLytRejillaFacturas"
        Me.INDLytRejillaFacturas.Size = New System.Drawing.Size(763, 53)
        Me.INDLytRejillaFacturas.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLytRejillaFacturas.TextVisible = False
        '
        'LayoutControlItem5
        '
        Me.LayoutControlItem5.Control = Me.INDBtndisplayAdvancedSearch
        Me.LayoutControlItem5.CustomizationFormText = "LayoutControlItem5"
        Me.LayoutControlItem5.Location = New System.Drawing.Point(0, 284)
        Me.LayoutControlItem5.MaxSize = New System.Drawing.Size(200, 36)
        Me.LayoutControlItem5.MinSize = New System.Drawing.Size(200, 36)
        Me.LayoutControlItem5.Name = "LayoutControlItem5"
        Me.LayoutControlItem5.Size = New System.Drawing.Size(200, 36)
        Me.LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem5.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem5.TextVisible = False
        '
        'INDLytAddInvoice
        '
        Me.INDLytAddInvoice.Control = Me.INDbtnAgregarFactura
        Me.INDLytAddInvoice.ControlAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.INDLytAddInvoice.CustomizationFormText = "INDLytAddInvoice"
        Me.INDLytAddInvoice.Location = New System.Drawing.Point(200, 284)
        Me.INDLytAddInvoice.MaxSize = New System.Drawing.Size(96, 36)
        Me.INDLytAddInvoice.MinSize = New System.Drawing.Size(96, 36)
        Me.INDLytAddInvoice.Name = "INDLytAddInvoice"
        Me.INDLytAddInvoice.Size = New System.Drawing.Size(563, 36)
        Me.INDLytAddInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDLytAddInvoice.TextSize = New System.Drawing.Size(0, 0)
        Me.INDLytAddInvoice.TextVisible = False
        '
        'INDlyiSelectAll
        '
        Me.INDlyiSelectAll.Control = Me.INDchkSelectAll
        Me.INDlyiSelectAll.CustomizationFormText = "INDlyiSelectAll"
        Me.INDlyiSelectAll.Location = New System.Drawing.Point(0, 206)
        Me.INDlyiSelectAll.MaxSize = New System.Drawing.Size(200, 25)
        Me.INDlyiSelectAll.MinSize = New System.Drawing.Size(200, 25)
        Me.INDlyiSelectAll.Name = "INDlyiSelectAll"
        Me.INDlyiSelectAll.Size = New System.Drawing.Size(200, 25)
        Me.INDlyiSelectAll.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiSelectAll.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiSelectAll.TextSize = New System.Drawing.Size(0, 0)
        Me.INDlyiSelectAll.TextToControlDistance = 0
        Me.INDlyiSelectAll.TextVisible = False
        '
        'LayoutControlItem16
        '
        Me.LayoutControlItem16.Control = Me.INDchkSelectFiltered
        Me.LayoutControlItem16.Location = New System.Drawing.Point(200, 206)
        Me.LayoutControlItem16.MaxSize = New System.Drawing.Size(200, 25)
        Me.LayoutControlItem16.MinSize = New System.Drawing.Size(200, 25)
        Me.LayoutControlItem16.Name = "LayoutControlItem16"
        Me.LayoutControlItem16.Size = New System.Drawing.Size(563, 25)
        Me.LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem16.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem16.TextToControlDistance = 0
        Me.LayoutControlItem16.TextVisible = False
        '
        'INDbteNit
        '
        Me.INDbteNit.AllowQueryOne = True
        Me.INDbteNit.Datasource = Nothing
        Me.INDbteNit.DisplayMember = "{Nit} - {Name}"
        Me.INDbteNit.DisplayNullText = ""
        Me.INDbteNit.EditValue = Nothing
        Me.INDbteNit.EnterMoveNextControl = True
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = Nothing
        Me.INDbteNit.IdOpenForm = 503
        Me.INDbteNit.IsReadOnly = False
        Me.INDbteNit.Location = New System.Drawing.Point(24, 138)
        Me.INDbteNit.MaximumSize = New System.Drawing.Size(5000, 28)
        Me.INDbteNit.MinimumSize = New System.Drawing.Size(100, 28)
        Me.INDbteNit.Name = "INDbteNit"
        Me.INDbteNit.PopUpFormSize = New System.Drawing.Size(700, 400)
        Me.INDbteNit.Size = New System.Drawing.Size(386, 28)
        Me.INDbteNit.TabIndex = 1
        Me.INDbteNit.ValueMember = "Id"
        Me.INDbteNit.View = Me.SearchLookUpEditExView4
        '
        'SearchLookUpEditExView4
        '
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.FocusedRow.Options.UseForeColor = True
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEditExView4.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEditExView4.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEditExView4.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEditExView4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.SearchLookUpEditExView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2})
        Me.SearchLookUpEditExView4.Name = "SearchLookUpEditExView4"
        Me.SearchLookUpEditExView4.OptionsFind.AlwaysVisible = True
        Me.SearchLookUpEditExView4.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEditExView4.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEditExView4.OptionsView.ShowAutoFilterRow = True
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEditExView4, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Nit"
        Me.GridColumn1.FieldName = "Nit"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowFocus = False
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Nombre"
        Me.GridColumn2.FieldName = "Name"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.AllowFocus = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        '
        'PopupContainerControlMoreInfo
        '
        Me.PopupContainerControlMoreInfo.Controls.Add(Me.LayoutControl6)
        Me.PopupContainerControlMoreInfo.Location = New System.Drawing.Point(953, 414)
        Me.PopupContainerControlMoreInfo.Name = "PopupContainerControlMoreInfo"
        Me.PopupContainerControlMoreInfo.Size = New System.Drawing.Size(473, 191)
        Me.PopupContainerControlMoreInfo.TabIndex = 10
        '
        'LayoutControl6
        '
        Me.LayoutControl6.Controls.Add(Me.CtrXtraInfoConceptDevolution)
        Me.LayoutControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl6.Name = "LayoutControl6"
        Me.LayoutControl6.Root = Me.LayoutControlGroup4
        Me.LayoutControl6.Size = New System.Drawing.Size(473, 191)
        Me.LayoutControl6.TabIndex = 0
        Me.LayoutControl6.Text = "LayoutControl6"
        '
        'CtrXtraInfoConceptDevolution
        '
        Me.CtrXtraInfoConceptDevolution.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.CtrXtraInfoConceptDevolution.Appearance.Options.UseBackColor = True
        Me.CtrXtraInfoConceptDevolution.Location = New System.Drawing.Point(12, 12)
        Me.CtrXtraInfoConceptDevolution.Name = "CtrXtraInfoConceptDevolution"
        Me.CtrXtraInfoConceptDevolution.Size = New System.Drawing.Size(449, 167)
        Me.CtrXtraInfoConceptDevolution.TabIndex = 4
        Me.CtrXtraInfoConceptDevolution.Title = "Concepto Devolución"
        Me.CtrXtraInfoConceptDevolution.WidthTextLabel = 135
        '
        'LayoutControlGroup4
        '
        Me.LayoutControlGroup4.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup4.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup4.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup4, False)
        Me.LayoutControlGroup4.CustomizationFormText = "LayoutControlGroup4"
        Me.LayoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup4.GroupBordersVisible = False
        Me.LayoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem14})
        Me.LayoutControlGroup4.Name = "LayoutControlGroup4"
        Me.LayoutControlGroup4.Size = New System.Drawing.Size(473, 191)
        Me.LayoutControlGroup4.TextVisible = False
        '
        'LayoutControlItem14
        '
        Me.LayoutControlItem14.Control = Me.CtrXtraInfoConceptDevolution
        Me.LayoutControlItem14.CustomizationFormText = "LayoutControlItem14"
        Me.LayoutControlItem14.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem14.Name = "LayoutControlItem14"
        Me.LayoutControlItem14.Size = New System.Drawing.Size(453, 171)
        Me.LayoutControlItem14.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem14.TextVisible = False
        '
        'INDpccMore
        '
        Me.INDpccMore.Controls.Add(Me.LabelControl1)
        Me.INDpccMore.Controls.Add(Me.INDmeComment)
        Me.INDpccMore.Location = New System.Drawing.Point(101, 402)
        Me.INDpccMore.Name = "INDpccMore"
        Me.INDpccMore.Size = New System.Drawing.Size(419, 100)
        Me.INDpccMore.TabIndex = 21
        '
        'LabelControl1
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.LabelControl1, True)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.LabelControl1, False)
        Me.LabelControl1.Location = New System.Drawing.Point(34, 33)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(79, 21)
        Me.LabelControl1.TabIndex = 13
        Me.LabelControl1.Text = "Comentario"
        '
        'INDmeComment
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmeComment, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmeComment, False)
        Me.INDmeComment.Location = New System.Drawing.Point(131, 16)
        Me.IndigoTextEdit1.SetMascara(Me.INDmeComment, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmeComment.MaximumSize = New System.Drawing.Size(274, 60)
        Me.INDmeComment.MinimumSize = New System.Drawing.Size(274, 60)
        Me.INDmeComment.Name = "INDmeComment"
        Me.INDmeComment.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmeComment.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeComment.Properties.Appearance.Options.UseBackColor = True
        Me.INDmeComment.Properties.Appearance.Options.UseFont = True
        Me.INDmeComment.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDmeComment.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDmeComment.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmeComment.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDmeComment.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDmeComment.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmeComment.Size = New System.Drawing.Size(274, 60)
        Me.INDmeComment.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmeComment, 0)
        '
        'INDpceMore
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceMore, False)
        Me.INDpceMore.EnterMoveNextControl = True
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceMore, Nothing)
        Me.INDpceMore.Location = New System.Drawing.Point(24, 293)
        Me.INDpceMore.MinimumSize = New System.Drawing.Size(386, 32)
        Me.INDpceMore.Name = "INDpceMore"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceMore, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceMore, False)
        Me.INDpceMore.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpceMore.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceMore.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceMore.Properties.Appearance.Options.UseFont = True
        Me.INDpceMore.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDpceMore.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpceMore.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceMore.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpceMore.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpceMore.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDpceMore.Properties.AutoHeight = False
        Me.INDpceMore.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        Me.INDpceMore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceMore.Properties.PopupControl = Me.INDpccMore
        Me.INDpceMore.Properties.PopupSizeable = False
        Me.INDpceMore.Properties.ShowPopupCloseButton = False
        Me.INDpceMore.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        Me.INDpceMore.Size = New System.Drawing.Size(386, 32)
        Me.INDpceMore.StyleController = Me.LayoutControl1
        Me.INDpceMore.TabIndex = 5
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceMore, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceMore, Nothing)
        '
        'INDgcRadicateD
        '
        Me.IndigoGridControl1.SetAddActions(Me.INDgcRadicateD, Nothing)
        Me.IndigoGridControl1.SetControlNextFocus(Me.INDgcRadicateD, Nothing)
        Me.INDgcRadicateD.Cursor = System.Windows.Forms.Cursors.Default
        Me.IndigoGridControl1.SetExportButton(Me.INDgcRadicateD, False)
        Me.IndigoGridControl1.SetGuardarXml(Me.INDgcRadicateD, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgcRadicateD, False)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgcRadicateD, False)
        Me.INDgcRadicateD.Location = New System.Drawing.Point(438, 89)
        Me.INDgcRadicateD.MainView = Me.INDgvRadicateD
        Me.INDgcRadicateD.Name = "INDgcRadicateD"
        Me.INDgcRadicateD.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPopupContainerEditActions, Me.RepositoryItemMemoEditObservation, Me.RepositoryItemImageDevolution2, Me.RepositoryItemCheckEdit1, Me.RepositoryItemPopupContainerEditMoreInfo, Me.RepositoryItemButtonEditActions})
        Me.INDgcRadicateD.Size = New System.Drawing.Size(868, 409)
        Me.IndigoGridControl1.SetSizeConstraintsType(Me.INDgcRadicateD, DevExpress.XtraLayout.SizeConstraintsType.[Default])
        Me.INDgcRadicateD.TabIndex = 14
        Me.INDgcRadicateD.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.INDgvRadicateD})
        '
        'INDgvRadicateD
        '
        Me.INDgvRadicateD.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvRadicateD.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvRadicateD.Appearance.FocusedRow.Options.UseBackColor = True
        Me.INDgvRadicateD.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvRadicateD.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvRadicateD.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvRadicateD.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvRadicateD.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvRadicateD.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvRadicateD.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvRadicateD.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvRadicateD.Appearance.Row.Options.UseFont = True
        Me.INDgvRadicateD.Appearance.ViewCaption.Font = New System.Drawing.Font("Segoe UI Light", 13.0!)
        Me.INDgvRadicateD.Appearance.ViewCaption.Options.UseFont = True
        Me.INDgvRadicateD.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Clselection, Me.INDclDevolution, Me.GridColumn5, Me.INDclInvoice, Me.INDclDateInvoice, Me.GridColumn3, Me.INDclValue, Me.INDdCurrencyAbbreviation, Me.INDConceptDevolution, Me.INDColCUV, Me.INDclActions, Me.INDclInvoiceValuePacient, Me.INDclIngress, Me.INDclAccountantAccountCustomers, Me.INDclCodeContract, Me.INDclValueEntity})
        Me.INDgvRadicateD.GridControl = Me.INDgcRadicateD
        Me.INDgvRadicateD.Name = "INDgvRadicateD"
        Me.INDgvRadicateD.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        Me.INDgvRadicateD.OptionsMenu.EnableColumnMenu = False
        Me.INDgvRadicateD.OptionsSelection.MultiSelect = True
        Me.INDgvRadicateD.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.INDgvRadicateD.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.INDgvRadicateD.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvRadicateD.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvRadicateD.OptionsView.GroupDrawMode = DevExpress.XtraGrid.Views.Grid.GroupDrawMode.Standard
        Me.INDgvRadicateD.OptionsView.ShowAutoFilterRow = True
        Me.INDgvRadicateD.OptionsView.ShowFooter = True
        Me.INDgvRadicateD.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvRadicateD, False)
        '
        'Clselection
        '
        Me.Clselection.Caption = "Seleccion"
        Me.Clselection.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.Clselection.FieldName = "Selection"
        Me.Clselection.MaxWidth = 50
        Me.Clselection.Name = "Clselection"
        Me.Clselection.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.Clselection.OptionsColumn.AllowMove = False
        Me.Clselection.OptionsColumn.AllowShowHide = False
        Me.Clselection.OptionsColumn.AllowSize = False
        Me.Clselection.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.Clselection.Visible = True
        Me.Clselection.VisibleIndex = 0
        Me.Clselection.Width = 20
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'INDclDevolution
        '
        Me.INDclDevolution.Caption = "Devolución"
        Me.INDclDevolution.ColumnEdit = Me.RepositoryItemImageDevolution2
        Me.INDclDevolution.FieldName = "Devolution"
        Me.INDclDevolution.MinWidth = 75
        Me.INDclDevolution.Name = "INDclDevolution"
        Me.INDclDevolution.OptionsColumn.AllowEdit = False
        Me.INDclDevolution.OptionsColumn.AllowFocus = False
        Me.INDclDevolution.OptionsFilter.AllowFilter = False
        Me.INDclDevolution.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Devolution", "{0:n0} Registros")})
        Me.INDclDevolution.Visible = True
        Me.INDclDevolution.VisibleIndex = 1
        '
        'RepositoryItemImageDevolution2
        '
        Me.RepositoryItemImageDevolution2.AutoHeight = False
        Me.RepositoryItemImageDevolution2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemImageDevolution2.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("Devolucion", True, 0)})
        Me.RepositoryItemImageDevolution2.LargeImages = Me.INDimcInvoiceState
        Me.RepositoryItemImageDevolution2.Name = "RepositoryItemImageDevolution2"
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Categoría"
        Me.GridColumn5.FieldName = "CodeNameInvoiceCategory"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowFocus = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 2
        Me.GridColumn5.Width = 70
        '
        'INDclInvoice
        '
        Me.INDclInvoice.Caption = "N° Factura"
        Me.INDclInvoice.FieldName = "InvoiceNumber"
        Me.INDclInvoice.MinWidth = 100
        Me.INDclInvoice.Name = "INDclInvoice"
        Me.INDclInvoice.OptionsColumn.AllowEdit = False
        Me.INDclInvoice.Visible = True
        Me.INDclInvoice.VisibleIndex = 3
        Me.INDclInvoice.Width = 100
        '
        'INDclDateInvoice
        '
        Me.INDclDateInvoice.Caption = "Fecha Factura"
        Me.INDclDateInvoice.DisplayFormat.FormatString = "d"
        Me.INDclDateInvoice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.INDclDateInvoice.FieldName = "InvoiceDate"
        Me.INDclDateInvoice.Name = "INDclDateInvoice"
        Me.INDclDateInvoice.OptionsColumn.AllowEdit = False
        Me.INDclDateInvoice.Visible = True
        Me.INDclDateInvoice.VisibleIndex = 4
        Me.INDclDateInvoice.Width = 80
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Valor Factura"
        Me.GridColumn3.DisplayFormat.FormatString = "n2"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "InvoiceValueFacade"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InvoiceValueFacade", "Total: {0:c2}")})
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 5
        Me.GridColumn3.Width = 72
        '
        'INDclValue
        '
        Me.INDclValue.Caption = "Saldo"
        Me.INDclValue.DisplayFormat.FormatString = "n2"
        Me.INDclValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDclValue.FieldName = "BalanceInvoice"
        Me.INDclValue.Name = "INDclValue"
        Me.INDclValue.OptionsColumn.AllowEdit = False
        Me.INDclValue.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BalanceInvoice", "Total: {0:c2}")})
        Me.INDclValue.Visible = True
        Me.INDclValue.VisibleIndex = 6
        Me.INDclValue.Width = 58
        '
        'INDdCurrencyAbbreviation
        '
        Me.INDdCurrencyAbbreviation.Caption = "Moneda"
        Me.INDdCurrencyAbbreviation.FieldName = "CurrencyAbbreviation"
        Me.INDdCurrencyAbbreviation.Name = "INDdCurrencyAbbreviation"
        Me.INDdCurrencyAbbreviation.OptionsColumn.AllowEdit = False
        Me.INDdCurrencyAbbreviation.Visible = True
        Me.INDdCurrencyAbbreviation.VisibleIndex = 7
        Me.INDdCurrencyAbbreviation.Width = 68
        '
        'INDConceptDevolution
        '
        Me.INDConceptDevolution.Caption = "Concepto Devolución"
        Me.INDConceptDevolution.ColumnEdit = Me.RepositoryItemPopupContainerEditMoreInfo
        Me.INDConceptDevolution.MinWidth = 25
        Me.INDConceptDevolution.Name = "INDConceptDevolution"
        Me.INDConceptDevolution.Visible = True
        Me.INDConceptDevolution.VisibleIndex = 8
        Me.INDConceptDevolution.Width = 49
        '
        'RepositoryItemPopupContainerEditMoreInfo
        '
        Me.RepositoryItemPopupContainerEditMoreInfo.AutoHeight = False
        EditorButtonImageOptions2.Image = Global.Presentation.Glosas.My.Resources.Resources.mas
        Me.RepositoryItemPopupContainerEditMoreInfo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "Mas información", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.RepositoryItemPopupContainerEditMoreInfo.Name = "RepositoryItemPopupContainerEditMoreInfo"
        Me.RepositoryItemPopupContainerEditMoreInfo.PopupControl = Me.PopupContainerControlMoreInfo
        Me.RepositoryItemPopupContainerEditMoreInfo.PopupSizeable = False
        Me.RepositoryItemPopupContainerEditMoreInfo.ShowPopupCloseButton = False
        Me.RepositoryItemPopupContainerEditMoreInfo.ShowPopupShadow = False
        Me.RepositoryItemPopupContainerEditMoreInfo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDclActions
        '
        Me.INDclActions.AppearanceCell.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDclActions.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDclActions.AppearanceCell.Options.UseFont = True
        Me.INDclActions.AppearanceCell.Options.UseForeColor = True
        Me.INDclActions.AppearanceCell.Options.UseTextOptions = True
        Me.INDclActions.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.INDclActions.Caption = "Acciones"
        Me.INDclActions.ColumnEdit = Me.RepositoryItemButtonEditActions
        Me.INDclActions.MinWidth = 80
        Me.INDclActions.Name = "INDclActions"
        Me.INDclActions.Visible = True
        Me.INDclActions.VisibleIndex = 10
        Me.INDclActions.Width = 93
        '
        'RepositoryItemButtonEditActions
        '
        Me.RepositoryItemButtonEditActions.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemButtonEditActions.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.RepositoryItemButtonEditActions.Appearance.Options.UseFont = True
        Me.RepositoryItemButtonEditActions.Appearance.Options.UseForeColor = True
        Me.RepositoryItemButtonEditActions.Appearance.Options.UseTextOptions = True
        Me.RepositoryItemButtonEditActions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEditActions.AppearanceDisabled.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemButtonEditActions.AppearanceDisabled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.RepositoryItemButtonEditActions.AppearanceDisabled.Options.UseFont = True
        Me.RepositoryItemButtonEditActions.AppearanceDisabled.Options.UseForeColor = True
        Me.RepositoryItemButtonEditActions.AppearanceDisabled.Options.UseTextOptions = True
        Me.RepositoryItemButtonEditActions.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEditActions.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemButtonEditActions.AppearanceFocused.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.RepositoryItemButtonEditActions.AppearanceFocused.Options.UseFont = True
        Me.RepositoryItemButtonEditActions.AppearanceFocused.Options.UseForeColor = True
        Me.RepositoryItemButtonEditActions.AppearanceFocused.Options.UseTextOptions = True
        Me.RepositoryItemButtonEditActions.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEditActions.AppearanceReadOnly.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.RepositoryItemButtonEditActions.AppearanceReadOnly.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.RepositoryItemButtonEditActions.AppearanceReadOnly.Options.UseFont = True
        Me.RepositoryItemButtonEditActions.AppearanceReadOnly.Options.UseForeColor = True
        Me.RepositoryItemButtonEditActions.AppearanceReadOnly.Options.UseTextOptions = True
        Me.RepositoryItemButtonEditActions.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.RepositoryItemButtonEditActions.AutoHeight = False
        Me.RepositoryItemButtonEditActions.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.RepositoryItemButtonEditActions.Name = "RepositoryItemButtonEditActions"
        Me.RepositoryItemButtonEditActions.NullText = "Eliminar"
        Me.RepositoryItemButtonEditActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'INDclInvoiceValuePacient
        '
        Me.INDclInvoiceValuePacient.Caption = "Valor Paciente"
        Me.INDclInvoiceValuePacient.DisplayFormat.FormatString = "c2"
        Me.INDclInvoiceValuePacient.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDclInvoiceValuePacient.FieldName = "InvoiceValuePacient"
        Me.INDclInvoiceValuePacient.Name = "INDclInvoiceValuePacient"
        Me.INDclInvoiceValuePacient.OptionsColumn.AllowEdit = False
        Me.INDclInvoiceValuePacient.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InvoiceValuePacient", "Total: {0:c2}")})
        Me.INDclInvoiceValuePacient.Width = 85
        '
        'INDclIngress
        '
        Me.INDclIngress.Caption = "Ingreso"
        Me.INDclIngress.FieldName = "IngressNumber"
        Me.INDclIngress.Name = "INDclIngress"
        Me.INDclIngress.OptionsColumn.AllowEdit = False
        Me.INDclIngress.Width = 103
        '
        'INDclAccountantAccountCustomers
        '
        Me.INDclAccountantAccountCustomers.Caption = "Cuenta cartera"
        Me.INDclAccountantAccountCustomers.FieldName = "AccountantAccountCustomers"
        Me.INDclAccountantAccountCustomers.Name = "INDclAccountantAccountCustomers"
        Me.INDclAccountantAccountCustomers.OptionsColumn.AllowEdit = False
        Me.INDclAccountantAccountCustomers.Width = 190
        '
        'INDclCodeContract
        '
        Me.INDclCodeContract.Caption = "N° Contrato"
        Me.INDclCodeContract.FieldName = "ContractCode"
        Me.INDclCodeContract.Name = "INDclCodeContract"
        Me.INDclCodeContract.OptionsColumn.AllowEdit = False
        Me.INDclCodeContract.Width = 85
        '
        'INDclValueEntity
        '
        Me.INDclValueEntity.Caption = "Valor Entidad"
        Me.INDclValueEntity.DisplayFormat.FormatString = "c2"
        Me.INDclValueEntity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.INDclValueEntity.FieldName = "InvoiceValueEntity"
        Me.INDclValueEntity.Name = "INDclValueEntity"
        Me.INDclValueEntity.OptionsColumn.AllowEdit = False
        Me.INDclValueEntity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "InvoiceValueEntity", "Total: {0:c2}")})
        Me.INDclValueEntity.Width = 85
        '
        'INDColCUV
        '
        Me.INDColCUV.Caption = "CUV"
        Me.INDColCUV.FieldName = "CUV"
        Me.INDColCUV.Name = "INDColCUV"
        Me.INDColCUV.OptionsColumn.AllowEdit = False
        Me.INDColCUV.OptionsColumn.AllowFocus = False
        Me.INDColCUV.Visible = True
        Me.INDColCUV.VisibleIndex = 9
        '
        'RepositoryItemPopupContainerEditActions
        '
        Me.RepositoryItemPopupContainerEditActions.AutoHeight = False
        Me.RepositoryItemPopupContainerEditActions.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemPopupContainerEditActions.Name = "RepositoryItemPopupContainerEditActions"
        Me.RepositoryItemPopupContainerEditActions.PopupControl = Me.INDpccAcciones
        Me.RepositoryItemPopupContainerEditActions.PopupSizeable = False
        Me.RepositoryItemPopupContainerEditActions.ShowPopupCloseButton = False
        Me.RepositoryItemPopupContainerEditActions.ShowPopupShadow = False
        Me.RepositoryItemPopupContainerEditActions.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'INDpccAcciones
        '
        Me.INDpccAcciones.Controls.Add(Me.INDbtnEliminar)
        Me.INDpccAcciones.Controls.Add(Me.INDMoreColumaPcc)
        Me.INDpccAcciones.Location = New System.Drawing.Point(773, 16)
        Me.INDpccAcciones.Name = "INDpccAcciones"
        Me.INDpccAcciones.Size = New System.Drawing.Size(121, 38)
        Me.INDpccAcciones.TabIndex = 19
        '
        'INDbtnEliminar
        '
        Me.INDbtnEliminar.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbtnEliminar.Appearance.Options.UseFont = True
        Me.INDbtnEliminar.Appearance.Options.UseTextOptions = True
        Me.INDbtnEliminar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.INDbtnEliminar.Dock = System.Windows.Forms.DockStyle.Top
        Me.INDbtnEliminar.ImageOptions.Image = Global.Presentation.Glosas.My.Resources.Resources.eliminarLineaAzul
        Me.INDbtnEliminar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft
        Me.INDbtnEliminar.Location = New System.Drawing.Point(0, 0)
        Me.INDbtnEliminar.MaximumSize = New System.Drawing.Size(0, 38)
        Me.INDbtnEliminar.MinimumSize = New System.Drawing.Size(117, 38)
        Me.INDbtnEliminar.Name = "INDbtnEliminar"
        Me.INDbtnEliminar.Size = New System.Drawing.Size(121, 38)
        Me.INDbtnEliminar.TabIndex = 0
        Me.INDbtnEliminar.Text = "Eliminar"
        '
        'INDMoreColumaPcc
        '
        Me.INDMoreColumaPcc.Controls.Add(Me.LayoutControl3)
        Me.INDMoreColumaPcc.Location = New System.Drawing.Point(193, 62)
        Me.INDMoreColumaPcc.Name = "INDMoreColumaPcc"
        Me.INDMoreColumaPcc.Size = New System.Drawing.Size(627, 253)
        Me.INDMoreColumaPcc.TabIndex = 23
        '
        'LayoutControl3
        '
        Me.LayoutControl3.Controls.Add(Me.CtrXtraInfoInvoiceDetail)
        Me.LayoutControl3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl3.Name = "LayoutControl3"
        Me.LayoutControl3.Root = Me.LayoutControlGroup5
        Me.LayoutControl3.Size = New System.Drawing.Size(627, 253)
        Me.LayoutControl3.TabIndex = 0
        Me.LayoutControl3.Text = "LayoutControl3"
        '
        'CtrXtraInfoInvoiceDetail
        '
        Me.CtrXtraInfoInvoiceDetail.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.CtrXtraInfoInvoiceDetail.Appearance.Options.UseBackColor = True
        Me.CtrXtraInfoInvoiceDetail.Location = New System.Drawing.Point(12, 12)
        Me.CtrXtraInfoInvoiceDetail.Name = "CtrXtraInfoInvoiceDetail"
        Me.CtrXtraInfoInvoiceDetail.Size = New System.Drawing.Size(603, 229)
        Me.CtrXtraInfoInvoiceDetail.TabIndex = 29
        Me.CtrXtraInfoInvoiceDetail.Title = "Mas Información"
        Me.CtrXtraInfoInvoiceDetail.WidthTextLabel = 135
        '
        'LayoutControlGroup5
        '
        Me.LayoutControlGroup5.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup5.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup5.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup5, False)
        Me.LayoutControlGroup5.CustomizationFormText = "LayoutControlGroup5"
        Me.LayoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup5.GroupBordersVisible = False
        Me.LayoutControlGroup5.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem13})
        Me.LayoutControlGroup5.Name = "LayoutControlGroup5"
        Me.LayoutControlGroup5.Size = New System.Drawing.Size(627, 253)
        Me.LayoutControlGroup5.TextVisible = False
        '
        'LayoutControlItem13
        '
        Me.LayoutControlItem13.Control = Me.CtrXtraInfoInvoiceDetail
        Me.LayoutControlItem13.CustomizationFormText = "LayoutControlItem13"
        Me.LayoutControlItem13.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem13.Name = "LayoutControlItem13"
        Me.LayoutControlItem13.Size = New System.Drawing.Size(607, 233)
        Me.LayoutControlItem13.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem13.TextVisible = False
        '
        'RepositoryItemMemoEditObservation
        '
        Me.RepositoryItemMemoEditObservation.Name = "RepositoryItemMemoEditObservation"
        '
        'INDdeDocumentDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdeDocumentDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.IndigoDate2.SetCampoObligatorio(Me.INDdeDocumentDate, False)
        Me.INDdeDocumentDate.EditValue = Nothing
        Me.INDdeDocumentDate.EnterMoveNextControl = True
        Me.INDdeDocumentDate.Location = New System.Drawing.Point(24, 259)
        Me.IndigoTextEdit1.SetMascara(Me.INDdeDocumentDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate2.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.IndigoDate1.SetMascaraDate(Me.INDdeDocumentDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdeDocumentDate.Name = "INDdeDocumentDate"
        Me.INDdeDocumentDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdeDocumentDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.Appearance.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDdeDocumentDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdeDocumentDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdeDocumentDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDdeDocumentDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdeDocumentDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdeDocumentDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdeDocumentDate.StyleController = Me.LayoutControl1
        Me.INDdeDocumentDate.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdeDocumentDate, 0)
        '
        'INDlblDateRadicate
        '
        Me.IndigoLabelControl1.SetAplicaEstilo(Me.INDlblDateRadicate, True)
        Me.INDlblDateRadicate.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlblDateRadicate.Appearance.Options.UseFont = True
        Me.IndigoLabelControl1.SetCampoObligatorio(Me.INDlblDateRadicate, False)
        Me.INDlblDateRadicate.Location = New System.Drawing.Point(24, 199)
        Me.INDlblDateRadicate.Name = "INDlblDateRadicate"
        Me.INDlblDateRadicate.Size = New System.Drawing.Size(386, 30)
        Me.INDlblDateRadicate.StyleController = Me.LayoutControl1
        Me.INDlblDateRadicate.TabIndex = 9
        '
        'INDpceBuscaFactura
        '
        Me.IndigoPopUpContainerEdit1.SetButtonMoreOptions(Me.INDpceBuscaFactura, False)
        Me.IndigoPopUpContainerEdit1.SetHostControl(Me.INDpceBuscaFactura, Nothing)
        Me.INDpceBuscaFactura.Location = New System.Drawing.Point(975, 53)
        Me.INDpceBuscaFactura.MinimumSize = New System.Drawing.Size(313, 32)
        Me.INDpceBuscaFactura.Name = "INDpceBuscaFactura"
        Me.IndigoPopUpContainerEdit1.SetOpenForm(Me.INDpceBuscaFactura, False)
        Me.IndigoPopUpContainerEdit1.SetPopUpAnimation(Me.INDpceBuscaFactura, False)
        Me.INDpceBuscaFactura.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.INDpceBuscaFactura.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceBuscaFactura.Properties.Appearance.Options.UseBackColor = True
        Me.INDpceBuscaFactura.Properties.Appearance.Options.UseFont = True
        Me.INDpceBuscaFactura.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDpceBuscaFactura.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDpceBuscaFactura.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDpceBuscaFactura.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDpceBuscaFactura.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDpceBuscaFactura.Properties.AppearanceFocused.Options.UseFont = True
        SerializableAppearanceObject9.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        SerializableAppearanceObject9.Options.UseFont = True
        SerializableAppearanceObject10.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        SerializableAppearanceObject10.Options.UseFont = True
        SerializableAppearanceObject11.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        SerializableAppearanceObject11.Options.UseFont = True
        SerializableAppearanceObject12.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        SerializableAppearanceObject12.Options.UseFont = True
        Me.INDpceBuscaFactura.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDpceBuscaFactura.Properties.Mask.EditMask = "[-a-zA-Z0-9|°¬!""#$%&/()=?¡'¿\@¨´_.:,; ]+"
        Me.INDpceBuscaFactura.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDpceBuscaFactura.Properties.PopupControl = Me.INDpccInvoice
        Me.INDpceBuscaFactura.Properties.PopupFormMinSize = New System.Drawing.Size(981, 436)
        Me.INDpceBuscaFactura.Properties.PopupFormSize = New System.Drawing.Size(981, 436)
        Me.INDpceBuscaFactura.Properties.PopupSizeable = False
        Me.INDpceBuscaFactura.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        Me.INDpceBuscaFactura.Properties.ShowPopupShadow = False
        Me.INDpceBuscaFactura.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.INDpceBuscaFactura.Size = New System.Drawing.Size(313, 32)
        Me.INDpceBuscaFactura.StyleController = Me.LayoutControl1
        Me.INDpceBuscaFactura.TabIndex = 4
        Me.IndigoPopUpContainerEdit1.SetTagForm(Me.INDpceBuscaFactura, Nothing)
        Me.IndigoPopUpContainerEdit1.SetWpfControl(Me.INDpceBuscaFactura, Nothing)
        '
        'INDglCompany
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDglCompany, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDglCompany, False)
        Me.INDglCompany.EnterMoveNextControl = True
        Me.INDglCompany.Location = New System.Drawing.Point(585, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDglCompany, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDglCompany.Name = "INDglCompany"
        Me.INDglCompany.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDglCompany.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDglCompany.Properties.Appearance.Options.UseBackColor = True
        Me.INDglCompany.Properties.Appearance.Options.UseFont = True
        Me.INDglCompany.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDglCompany.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDglCompany.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDglCompany.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDglCompany.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDglCompany.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDglCompany.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDglCompany.Properties.DisplayMember = "ParametersInterfaceCodeName"
        Me.INDglCompany.Properties.ImmediatePopup = True
        Me.INDglCompany.Properties.NullText = ""
        Me.INDglCompany.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.INDglCompany.Properties.PopupView = Me.INDgvSucursales
        Me.INDglCompany.Properties.ValueMember = "ContainerName"
        Me.INDglCompany.Size = New System.Drawing.Size(221, 28)
        Me.INDglCompany.StyleController = Me.LayoutControl1
        Me.INDglCompany.TabIndex = 3
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDglCompany, 0)
        '
        'INDgvSucursales
        '
        Me.INDgvSucursales.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.INDgvSucursales.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.INDgvSucursales.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.INDgvSucursales.Appearance.FocusedRow.Options.UseFont = True
        Me.INDgvSucursales.Appearance.FocusedRow.Options.UseForeColor = True
        Me.INDgvSucursales.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvSucursales.Appearance.GroupRow.Options.UseFont = True
        Me.INDgvSucursales.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDgvSucursales.Appearance.HeaderPanel.Options.UseFont = True
        Me.INDgvSucursales.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.INDgvSucursales.Appearance.Row.Options.UseFont = True
        Me.INDgvSucursales.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.INDclNameContainers, Me.INDclAccountingMethod})
        Me.INDgvSucursales.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.INDgvSucursales.Name = "INDgvSucursales"
        Me.INDgvSucursales.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.INDgvSucursales.OptionsView.EnableAppearanceEvenRow = True
        Me.INDgvSucursales.OptionsView.EnableAppearanceOddRow = True
        Me.INDgvSucursales.OptionsView.ShowAutoFilterRow = True
        Me.INDgvSucursales.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.INDgvSucursales, False)
        '
        'INDclNameContainers
        '
        Me.INDclNameContainers.Caption = "Nombre"
        Me.INDclNameContainers.FieldName = "ParametersInterfaceCodeName"
        Me.INDclNameContainers.Name = "INDclNameContainers"
        Me.INDclNameContainers.Visible = True
        Me.INDclNameContainers.VisibleIndex = 0
        '
        'INDclAccountingMethod
        '
        Me.INDclAccountingMethod.Caption = "AccountingMethod"
        Me.INDclAccountingMethod.FieldName = "AccountingMethod"
        Me.INDclAccountingMethod.Name = "INDclAccountingMethod"
        '
        'INDbteConsecutive
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDbteConsecutive, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDbteConsecutive, False)
        Me.INDbteConsecutive.Location = New System.Drawing.Point(24, 79)
        Me.IndigoTextEdit1.SetMascara(Me.INDbteConsecutive, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDbteConsecutive.Name = "INDbteConsecutive"
        Me.INDbteConsecutive.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDbteConsecutive.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteConsecutive.Properties.Appearance.Options.UseBackColor = True
        Me.INDbteConsecutive.Properties.Appearance.Options.UseFont = True
        Me.INDbteConsecutive.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDbteConsecutive.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDbteConsecutive.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDbteConsecutive.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDbteConsecutive.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDbteConsecutive.Properties.AppearanceFocused.Options.UseFont = True
        EditorButtonImageOptions4.Image = Global.Presentation.Glosas.My.Resources.Resources.BuscarMetro
        Me.INDbteConsecutive.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, EditorButtonImageOptions4, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject13, SerializableAppearanceObject14, SerializableAppearanceObject15, SerializableAppearanceObject16, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.INDbteConsecutive.Size = New System.Drawing.Size(386, 28)
        Me.INDbteConsecutive.StyleController = Me.LayoutControl1
        Me.INDbteConsecutive.TabIndex = 0
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDbteConsecutive, 0)
        '
        'INDlycRoot
        '
        Me.INDlycRoot.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycRoot.AppearanceGroup.Options.UseFont = True
        Me.INDlycRoot.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlycRoot.AppearanceItemCaption.Options.UseFont = True
        Me.INDlycRoot.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycRoot.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlycRoot.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlycRoot.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlycRoot.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycRoot.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlycRoot.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycRoot.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlycRoot.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlycRoot.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlycRoot, False)
        Me.INDlycRoot.CustomizationFormText = "Root"
        Me.INDlycRoot.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.INDlycRoot.GroupBordersVisible = False
        Me.INDlycRoot.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygGeneralData, Me.INDlygInvoice})
        Me.INDlycRoot.Name = "Root"
        Me.INDlycRoot.Size = New System.Drawing.Size(1330, 522)
        Me.INDlycRoot.TextVisible = False
        '
        'INDlygGeneralData
        '
        Me.INDlygGeneralData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralData.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneralData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneralData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneralData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneralData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneralData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneralData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneralData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneralData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneralData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneralData, False)
        Me.INDlygGeneralData.CustomizationFormText = "Radicación de Cuentas"
        Me.INDlygGeneralData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiConsecutive, Me.LayoutControlItem2, Me.LayoutControlItem8, Me.INDlyiDateRadication, Me.INDlyiDateOfice})
        Me.INDlygGeneralData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygGeneralData.Name = "INDlygGeneralData"
        Me.INDlygGeneralData.Size = New System.Drawing.Size(414, 502)
        Me.INDlygGeneralData.Text = "Radicación de Cuentas"
        '
        'INDlyiConsecutive
        '
        Me.INDlyiConsecutive.Control = Me.INDbteConsecutive
        Me.INDlyiConsecutive.CustomizationFormText = "Consecutivo"
        Me.INDlyiConsecutive.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiConsecutive.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyiConsecutive.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyiConsecutive.Name = "INDlyiConsecutive"
        Me.INDlyiConsecutive.Size = New System.Drawing.Size(390, 60)
        Me.INDlyiConsecutive.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiConsecutive.Text = "Consecutivo"
        Me.INDlyiConsecutive.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiConsecutive.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyiConsecutive.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiConsecutive.TextToControlDistance = 5
        '
        'LayoutControlItem2
        '
        Me.LayoutControlItem2.Control = Me.INDpceMore
        Me.LayoutControlItem2.CustomizationFormText = "Comentario"
        Me.LayoutControlItem2.Location = New System.Drawing.Point(0, 240)
        Me.LayoutControlItem2.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem2.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem2.Name = "LayoutControlItem2"
        Me.LayoutControlItem2.Size = New System.Drawing.Size(390, 209)
        Me.LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem2.TextToControlDistance = 0
        Me.LayoutControlItem2.TextVisible = False
        '
        'LayoutControlItem8
        '
        Me.LayoutControlItem8.Control = Me.INDbteNit
        Me.LayoutControlItem8.CustomizationFormText = "Nit"
        Me.LayoutControlItem8.Location = New System.Drawing.Point(0, 60)
        Me.LayoutControlItem8.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem8.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem8.Name = "LayoutControlItem8"
        Me.LayoutControlItem8.Size = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem8.Text = "Nit"
        Me.LayoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top
        Me.LayoutControlItem8.TextSize = New System.Drawing.Size(135, 20)
        Me.LayoutControlItem8.TextToControlDistance = 5
        '
        'INDlyiDateRadication
        '
        Me.INDlyiDateRadication.Control = Me.INDlblDateRadicate
        Me.INDlyiDateRadication.CustomizationFormText = "Fecha Radicación"
        Me.INDlyiDateRadication.Location = New System.Drawing.Point(0, 120)
        Me.INDlyiDateRadication.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyiDateRadication.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyiDateRadication.Name = "INDlyiDateRadication"
        Me.INDlyiDateRadication.Size = New System.Drawing.Size(390, 60)
        Me.INDlyiDateRadication.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiDateRadication.Text = "Fecha Radicación"
        Me.INDlyiDateRadication.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiDateRadication.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyiDateRadication.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiDateRadication.TextToControlDistance = 5
        '
        'INDlyiDateOfice
        '
        Me.INDlyiDateOfice.Control = Me.INDdeDocumentDate
        Me.INDlyiDateOfice.CustomizationFormText = "Fecha Oficio"
        Me.INDlyiDateOfice.Location = New System.Drawing.Point(0, 180)
        Me.INDlyiDateOfice.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyiDateOfice.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyiDateOfice.Name = "INDlyiDateOfice"
        Me.INDlyiDateOfice.Size = New System.Drawing.Size(390, 60)
        Me.INDlyiDateOfice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiDateOfice.Text = "Fecha Oficio "
        Me.INDlyiDateOfice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiDateOfice.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyiDateOfice.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiDateOfice.TextToControlDistance = 5
        '
        'INDlygInvoice
        '
        Me.INDlygInvoice.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInvoice.AppearanceGroup.Options.UseFont = True
        Me.INDlygInvoice.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygInvoice.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygInvoice.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInvoice.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygInvoice.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygInvoice.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygInvoice.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInvoice.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygInvoice.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInvoice.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygInvoice.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygInvoice.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygInvoice, False)
        Me.INDlygInvoice.CustomizationFormText = "Factura"
        Me.INDlygInvoice.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyiContainer, Me.INDlyiInvoice, Me.LayoutControlItem1})
        Me.INDlygInvoice.Location = New System.Drawing.Point(414, 0)
        Me.INDlygInvoice.Name = "INDlygInvoice"
        Me.INDlygInvoice.Size = New System.Drawing.Size(896, 502)
        Me.INDlygInvoice.Text = "Factura"
        '
        'INDlyiContainer
        '
        Me.INDlyiContainer.Control = Me.INDglCompany
        Me.INDlyiContainer.CustomizationFormText = "Sede o Sucursal"
        Me.INDlyiContainer.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiContainer.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiContainer.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiContainer.Name = "INDlyiContainer"
        Me.INDlyiContainer.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 20, 2, 2)
        Me.INDlyiContainer.Size = New System.Drawing.Size(390, 36)
        Me.INDlyiContainer.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiContainer.Text = "Sede o Sucursal"
        Me.INDlyiContainer.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiContainer.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiContainer.TextToControlDistance = 12
        '
        'INDlyiInvoice
        '
        Me.INDlyiInvoice.Control = Me.INDpceBuscaFactura
        Me.INDlyiInvoice.CustomizationFormText = "N° Factura"
        Me.INDlyiInvoice.Location = New System.Drawing.Point(390, 0)
        Me.INDlyiInvoice.MaxSize = New System.Drawing.Size(482, 36)
        Me.INDlyiInvoice.MinSize = New System.Drawing.Size(482, 36)
        Me.INDlyiInvoice.Name = "INDlyiInvoice"
        Me.INDlyiInvoice.Padding = New DevExpress.XtraLayout.Utils.Padding(2, 20, 2, 2)
        Me.INDlyiInvoice.Size = New System.Drawing.Size(482, 36)
        Me.INDlyiInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiInvoice.Text = "N° Factura"
        Me.INDlyiInvoice.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiInvoice.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiInvoice.TextToControlDistance = 12
        '
        'LayoutControlItem1
        '
        Me.LayoutControlItem1.Control = Me.INDgcRadicateD
        Me.LayoutControlItem1.CustomizationFormText = "Lista de Facturas"
        Me.LayoutControlItem1.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem1.Name = "LayoutControlItem1"
        Me.LayoutControlItem1.Size = New System.Drawing.Size(872, 413)
        Me.LayoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem1.TextVisible = False
        '
        'INDSendDocumentCommentMte
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSendDocumentCommentMte, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSendDocumentCommentMte, False)
        Me.INDSendDocumentCommentMte.Location = New System.Drawing.Point(171, 125)
        Me.IndigoTextEdit1.SetMascara(Me.INDSendDocumentCommentMte, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDSendDocumentCommentMte.Name = "INDSendDocumentCommentMte"
        Me.INDSendDocumentCommentMte.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSendDocumentCommentMte.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSendDocumentCommentMte.Properties.Appearance.Options.UseBackColor = True
        Me.INDSendDocumentCommentMte.Properties.Appearance.Options.UseFont = True
        Me.INDSendDocumentCommentMte.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSendDocumentCommentMte.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSendDocumentCommentMte.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSendDocumentCommentMte.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSendDocumentCommentMte.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSendDocumentCommentMte.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSendDocumentCommentMte.Properties.MaxLength = 500
        Me.INDSendDocumentCommentMte.Size = New System.Drawing.Size(239, 56)
        Me.INDSendDocumentCommentMte.StyleController = Me.LayoutControl4
        Me.INDSendDocumentCommentMte.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSendDocumentCommentMte, 0)
        '
        'LayoutControl4
        '
        Me.LayoutControl4.Controls.Add(Me.SimpleButton2)
        Me.LayoutControl4.Controls.Add(Me.SimpleButton1)
        Me.LayoutControl4.Controls.Add(Me.INDSendDocumentCommentMte)
        Me.LayoutControl4.Controls.Add(Me.INDSendDocumentDateDte)
        Me.LayoutControl4.Controls.Add(Me.INDUserRadicateTxt)
        Me.LayoutControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl4.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl4.Name = "LayoutControl4"
        Me.LayoutControl4.Root = Me.LayoutControlGroup12
        Me.LayoutControl4.Size = New System.Drawing.Size(1495, 130)
        Me.LayoutControl4.TabIndex = 20
        Me.LayoutControl4.Text = "LayoutControl4"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.Location = New System.Drawing.Point(24, 185)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(198, 29)
        Me.SimpleButton2.StyleController = Me.LayoutControl4
        Me.SimpleButton2.TabIndex = 9
        Me.SimpleButton2.Text = "Aceptar"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Location = New System.Drawing.Point(226, 185)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(198, 29)
        Me.SimpleButton1.StyleController = Me.LayoutControl4
        Me.SimpleButton1.TabIndex = 8
        Me.SimpleButton1.Text = "Cancelar"
        '
        'INDSendDocumentDateDte
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDSendDocumentDateDte, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDSendDocumentDateDte, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDSendDocumentDateDte, False)
        Me.IndigoDate2.SetCampoObligatorio(Me.INDSendDocumentDateDte, False)
        Me.INDSendDocumentDateDte.EditValue = Nothing
        Me.INDSendDocumentDateDte.Location = New System.Drawing.Point(171, 53)
        Me.IndigoTextEdit1.SetMascara(Me.INDSendDocumentDateDte, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate2.SetMascaraDate(Me.INDSendDocumentDateDte, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.IndigoDate1.SetMascaraDate(Me.INDSendDocumentDateDte, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDSendDocumentDateDte.Name = "INDSendDocumentDateDte"
        Me.INDSendDocumentDateDte.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDSendDocumentDateDte.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSendDocumentDateDte.Properties.Appearance.Options.UseBackColor = True
        Me.INDSendDocumentDateDte.Properties.Appearance.Options.UseFont = True
        Me.INDSendDocumentDateDte.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDSendDocumentDateDte.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDSendDocumentDateDte.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDSendDocumentDateDte.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDSendDocumentDateDte.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDSendDocumentDateDte.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDSendDocumentDateDte.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDSendDocumentDateDte.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.INDSendDocumentDateDte.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.INDSendDocumentDateDte.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDSendDocumentDateDte.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDSendDocumentDateDte.Size = New System.Drawing.Size(239, 28)
        Me.INDSendDocumentDateDte.StyleController = Me.LayoutControl4
        Me.INDSendDocumentDateDte.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDSendDocumentDateDte, 0)
        '
        'INDUserRadicateTxt
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDUserRadicateTxt, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDUserRadicateTxt, False)
        Me.INDUserRadicateTxt.Location = New System.Drawing.Point(171, 89)
        Me.IndigoTextEdit1.SetMascara(Me.INDUserRadicateTxt, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDUserRadicateTxt.Name = "INDUserRadicateTxt"
        Me.INDUserRadicateTxt.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDUserRadicateTxt.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDUserRadicateTxt.Properties.Appearance.Options.UseBackColor = True
        Me.INDUserRadicateTxt.Properties.Appearance.Options.UseFont = True
        Me.INDUserRadicateTxt.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.INDUserRadicateTxt.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.INDUserRadicateTxt.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDUserRadicateTxt.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.INDUserRadicateTxt.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.INDUserRadicateTxt.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDUserRadicateTxt.Properties.MaxLength = 50
        Me.INDUserRadicateTxt.Size = New System.Drawing.Size(239, 28)
        Me.INDUserRadicateTxt.StyleController = Me.LayoutControl4
        Me.INDUserRadicateTxt.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDUserRadicateTxt, 0)
        '
        'LayoutControlGroup12
        '
        Me.LayoutControlGroup12.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup12.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup12.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup12.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup12.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup12, False)
        Me.LayoutControlGroup12.CustomizationFormText = "LayoutControlGroup12"
        Me.LayoutControlGroup12.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup12.GroupBordersVisible = False
        Me.LayoutControlGroup12.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup10})
        Me.LayoutControlGroup12.Name = "LayoutControlGroup12"
        Me.LayoutControlGroup12.Size = New System.Drawing.Size(1478, 238)
        Me.LayoutControlGroup12.TextVisible = False
        '
        'LayoutControlGroup10
        '
        Me.LayoutControlGroup10.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup10.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup10.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup10.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup10.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup10, False)
        Me.LayoutControlGroup10.CustomizationFormText = "LayoutControlGroup10"
        Me.LayoutControlGroup10.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem9, Me.INDlyiUserConfirm, Me.INDlyiDateConfirm, Me.LayoutControlItem10, Me.LayoutControlItem15})
        Me.LayoutControlGroup10.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup10.Name = "LayoutControlGroup10"
        Me.LayoutControlGroup10.Size = New System.Drawing.Size(1458, 218)
        Me.LayoutControlGroup10.Text = "Datos de confirmación"
        '
        'LayoutControlItem9
        '
        Me.LayoutControlItem9.Control = Me.INDSendDocumentCommentMte
        Me.LayoutControlItem9.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem9.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem9.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem9.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem9.Name = "LayoutControlItem9"
        Me.LayoutControlItem9.Size = New System.Drawing.Size(1434, 60)
        Me.LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem9.Text = "Comentario"
        Me.LayoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem9.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem9.TextToControlDistance = 12
        '
        'INDlyiUserConfirm
        '
        Me.INDlyiUserConfirm.Control = Me.INDUserRadicateTxt
        Me.INDlyiUserConfirm.CustomizationFormText = "LayoutControlItem18"
        Me.INDlyiUserConfirm.Location = New System.Drawing.Point(0, 36)
        Me.INDlyiUserConfirm.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiUserConfirm.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiUserConfirm.Name = "INDlyiUserConfirm"
        Me.INDlyiUserConfirm.Size = New System.Drawing.Size(1434, 36)
        Me.INDlyiUserConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiUserConfirm.Text = "Dirigido a"
        Me.INDlyiUserConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiUserConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiUserConfirm.TextToControlDistance = 12
        '
        'INDlyiDateConfirm
        '
        Me.INDlyiDateConfirm.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlyiDateConfirm.AppearanceItemCaption.Options.UseFont = True
        Me.INDlyiDateConfirm.Control = Me.INDSendDocumentDateDte
        Me.INDlyiDateConfirm.CustomizationFormText = "LayoutControlItem6"
        Me.INDlyiDateConfirm.Location = New System.Drawing.Point(0, 0)
        Me.INDlyiDateConfirm.MaxSize = New System.Drawing.Size(390, 36)
        Me.INDlyiDateConfirm.MinSize = New System.Drawing.Size(390, 36)
        Me.INDlyiDateConfirm.Name = "INDlyiDateConfirm"
        Me.INDlyiDateConfirm.Size = New System.Drawing.Size(1434, 36)
        Me.INDlyiDateConfirm.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyiDateConfirm.Text = "Fecha"
        Me.INDlyiDateConfirm.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyiDateConfirm.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyiDateConfirm.TextToControlDistance = 12
        '
        'LayoutControlItem10
        '
        Me.LayoutControlItem10.Control = Me.SimpleButton1
        Me.LayoutControlItem10.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem10.Location = New System.Drawing.Point(202, 132)
        Me.LayoutControlItem10.MaxSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem10.MinSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem10.Name = "LayoutControlItem10"
        Me.LayoutControlItem10.Size = New System.Drawing.Size(1232, 33)
        Me.LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem10.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem10.TextVisible = False
        '
        'LayoutControlItem15
        '
        Me.LayoutControlItem15.Control = Me.SimpleButton2
        Me.LayoutControlItem15.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem15.Location = New System.Drawing.Point(0, 132)
        Me.LayoutControlItem15.MaxSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem15.MinSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem15.Name = "LayoutControlItem15"
        Me.LayoutControlItem15.Size = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem15.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem15.TextVisible = False
        '
        'MemoEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.MemoEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.MemoEdit1, False)
        Me.MemoEdit1.Location = New System.Drawing.Point(171, 125)
        Me.IndigoTextEdit1.SetMascara(Me.MemoEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.MemoEdit1.Name = "MemoEdit1"
        Me.MemoEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.MemoEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.MemoEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.MemoEdit1.Properties.Appearance.Options.UseFont = True
        Me.MemoEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MemoEdit1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.MemoEdit1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.MemoEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.MemoEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.MemoEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.MemoEdit1.Properties.MaxLength = 500
        Me.MemoEdit1.Size = New System.Drawing.Size(239, 56)
        Me.MemoEdit1.StyleController = Me.LayoutControl5
        Me.MemoEdit1.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.MemoEdit1, 0)
        '
        'LayoutControl5
        '
        Me.LayoutControl5.Controls.Add(Me.SimpleButton3)
        Me.LayoutControl5.Controls.Add(Me.SimpleButton4)
        Me.LayoutControl5.Controls.Add(Me.MemoEdit1)
        Me.LayoutControl5.Controls.Add(Me.DateEdit1)
        Me.LayoutControl5.Controls.Add(Me.TextEdit1)
        Me.LayoutControl5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutControl5.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControl5.Name = "LayoutControl5"
        Me.LayoutControl5.Root = Me.LayoutControlGroup2
        Me.LayoutControl5.Size = New System.Drawing.Size(448, 245)
        Me.LayoutControl5.TabIndex = 0
        Me.LayoutControl5.Text = "LayoutControl4"
        '
        'SimpleButton3
        '
        Me.SimpleButton3.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SimpleButton3.Appearance.Options.UseFont = True
        Me.SimpleButton3.Location = New System.Drawing.Point(24, 185)
        Me.SimpleButton3.Name = "SimpleButton3"
        Me.SimpleButton3.Size = New System.Drawing.Size(198, 29)
        Me.SimpleButton3.StyleController = Me.LayoutControl5
        Me.SimpleButton3.TabIndex = 9
        Me.SimpleButton3.Text = "Aceptar"
        '
        'SimpleButton4
        '
        Me.SimpleButton4.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.SimpleButton4.Appearance.Options.UseFont = True
        Me.SimpleButton4.Location = New System.Drawing.Point(226, 185)
        Me.SimpleButton4.Name = "SimpleButton4"
        Me.SimpleButton4.Size = New System.Drawing.Size(198, 29)
        Me.SimpleButton4.StyleController = Me.LayoutControl5
        Me.SimpleButton4.TabIndex = 8
        Me.SimpleButton4.Text = "Cancelar"
        '
        'DateEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.DateEdit1, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.DateEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.DateEdit1, False)
        Me.IndigoDate2.SetCampoObligatorio(Me.DateEdit1, False)
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.Location = New System.Drawing.Point(171, 53)
        Me.IndigoTextEdit1.SetMascara(Me.DateEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate2.SetMascaraDate(Me.DateEdit1, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.IndigoDate1.SetMascaraDate(Me.DateEdit1, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.DateEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.DateEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.DateEdit1.Properties.Appearance.Options.UseFont = True
        Me.DateEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.DateEdit1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.DateEdit1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.DateEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.DateEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.DateEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit1.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.DateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.DateEdit1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DateEdit1.Size = New System.Drawing.Size(239, 28)
        Me.DateEdit1.StyleController = Me.LayoutControl5
        Me.DateEdit1.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.DateEdit1, 0)
        '
        'TextEdit1
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.TextEdit1, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.TextEdit1, False)
        Me.TextEdit1.Location = New System.Drawing.Point(171, 89)
        Me.IndigoTextEdit1.SetMascara(Me.TextEdit1, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.TextEdit1.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TextEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.TextEdit1.Properties.Appearance.Options.UseFont = True
        Me.TextEdit1.Properties.AppearanceFocused.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TextEdit1.Properties.AppearanceFocused.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.TextEdit1.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseBorderColor = True
        Me.TextEdit1.Properties.AppearanceFocused.Options.UseFont = True
        Me.TextEdit1.Properties.MaxLength = 50
        Me.TextEdit1.Size = New System.Drawing.Size(239, 28)
        Me.TextEdit1.StyleController = Me.LayoutControl5
        Me.TextEdit1.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.TextEdit1, 0)
        '
        'LayoutControlGroup2
        '
        Me.LayoutControlGroup2.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup2.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup2.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup2, False)
        Me.LayoutControlGroup2.CustomizationFormText = "LayoutControlGroup12"
        Me.LayoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.LayoutControlGroup2.GroupBordersVisible = False
        Me.LayoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlGroup3})
        Me.LayoutControlGroup2.Name = "LayoutControlGroup12"
        Me.LayoutControlGroup2.Size = New System.Drawing.Size(448, 245)
        Me.LayoutControlGroup2.TextVisible = False
        '
        'LayoutControlGroup3
        '
        Me.LayoutControlGroup3.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceGroup.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LayoutControlGroup3.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.Header.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlGroup3.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.LayoutControlGroup3, False)
        Me.LayoutControlGroup3.CustomizationFormText = "LayoutControlGroup10"
        Me.LayoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.LayoutControlItem3, Me.LayoutControlItem4, Me.LayoutControlItem6, Me.LayoutControlItem7, Me.LayoutControlItem11})
        Me.LayoutControlGroup3.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlGroup3.Name = "LayoutControlGroup10"
        Me.LayoutControlGroup3.Size = New System.Drawing.Size(428, 225)
        Me.LayoutControlGroup3.Text = "Datos de confirmación"
        '
        'LayoutControlItem3
        '
        Me.LayoutControlItem3.Control = Me.MemoEdit1
        Me.LayoutControlItem3.CustomizationFormText = "LayoutControlItem9"
        Me.LayoutControlItem3.Location = New System.Drawing.Point(0, 72)
        Me.LayoutControlItem3.MaxSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.MinSize = New System.Drawing.Size(390, 60)
        Me.LayoutControlItem3.Name = "LayoutControlItem9"
        Me.LayoutControlItem3.Size = New System.Drawing.Size(404, 60)
        Me.LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem3.Text = "Comentario"
        Me.LayoutControlItem3.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem3.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem3.TextToControlDistance = 12
        '
        'LayoutControlItem4
        '
        Me.LayoutControlItem4.Control = Me.TextEdit1
        Me.LayoutControlItem4.CustomizationFormText = "LayoutControlItem18"
        Me.LayoutControlItem4.Location = New System.Drawing.Point(0, 36)
        Me.LayoutControlItem4.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem4.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem4.Name = "INDlyiUserConfirm"
        Me.LayoutControlItem4.Size = New System.Drawing.Size(404, 36)
        Me.LayoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem4.Text = "Dirigido a"
        Me.LayoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem4.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem4.TextToControlDistance = 12
        '
        'LayoutControlItem6
        '
        Me.LayoutControlItem6.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.LayoutControlItem6.AppearanceItemCaption.Options.UseFont = True
        Me.LayoutControlItem6.Control = Me.DateEdit1
        Me.LayoutControlItem6.CustomizationFormText = "LayoutControlItem6"
        Me.LayoutControlItem6.Location = New System.Drawing.Point(0, 0)
        Me.LayoutControlItem6.MaxSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem6.MinSize = New System.Drawing.Size(390, 36)
        Me.LayoutControlItem6.Name = "INDlyiDateConfirm"
        Me.LayoutControlItem6.Size = New System.Drawing.Size(404, 36)
        Me.LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem6.Text = "Fecha"
        Me.LayoutControlItem6.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.LayoutControlItem6.TextSize = New System.Drawing.Size(135, 21)
        Me.LayoutControlItem6.TextToControlDistance = 12
        '
        'LayoutControlItem7
        '
        Me.LayoutControlItem7.Control = Me.SimpleButton4
        Me.LayoutControlItem7.CustomizationFormText = "LayoutControlItem10"
        Me.LayoutControlItem7.Location = New System.Drawing.Point(202, 132)
        Me.LayoutControlItem7.MaxSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem7.MinSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem7.Name = "LayoutControlItem10"
        Me.LayoutControlItem7.Size = New System.Drawing.Size(202, 40)
        Me.LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem7.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem7.TextVisible = False
        '
        'LayoutControlItem11
        '
        Me.LayoutControlItem11.Control = Me.SimpleButton3
        Me.LayoutControlItem11.CustomizationFormText = "LayoutControlItem15"
        Me.LayoutControlItem11.Location = New System.Drawing.Point(0, 132)
        Me.LayoutControlItem11.MaxSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem11.MinSize = New System.Drawing.Size(202, 33)
        Me.LayoutControlItem11.Name = "LayoutControlItem15"
        Me.LayoutControlItem11.Size = New System.Drawing.Size(202, 40)
        Me.LayoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.LayoutControlItem11.TextSize = New System.Drawing.Size(0, 0)
        Me.LayoutControlItem11.TextVisible = False
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
        Me.CtrNavigationControlPanel1.LayoutControl = Me.LayoutControl1
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 539)
        Me.CtrNavigationControlPanel1.TabIndex = 1
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'FrmInvoiceRadicate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1495, 683)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.IconOptions.ShowIcon = False
        Me.Name = "FrmInvoiceRadicate"
        Me.Opacity = 1.0R
        Me.Tag = "509"
        Me.Text = "Radicación de Cuentas"
        Me.Controls.SetChildIndex(Me.barDockControlTop, 0)
        Me.Controls.SetChildIndex(Me.barDockControlBottom, 0)
        Me.Controls.SetChildIndex(Me.barDockControlRight, 0)
        Me.Controls.SetChildIndex(Me.barDockControlLeft, 0)
        Me.Controls.SetChildIndex(Me.ToolBars, 0)
        Me.Controls.SetChildIndex(Me.INDPanelControlBase, 0)
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl1.ResumeLayout(False)
        CType(Me.INDpccInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccInvoice.ResumeLayout(False)
        CType(Me.LayoutControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl2.ResumeLayout(False)
        CType(Me.INDchkSelectFiltered.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDchkSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrchSeleccion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageDevolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDimcInvoiceState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDrcbiState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLytFiltrosAvanzada, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLytRejillaFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDLytAddInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteNit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEditExView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PopupContainerControlMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PopupContainerControlMoreInfo.ResumeLayout(False)
        CType(Me.LayoutControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl6.ResumeLayout(False)
        CType(Me.LayoutControlGroup4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccMore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccMore.ResumeLayout(False)
        Me.INDpccMore.PerformLayout()
        CType(Me.INDmeComment.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceMore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgcRadicateD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvRadicateD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemImageDevolution2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEditMoreInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonEditActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPopupContainerEditActions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpccAcciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDpccAcciones.ResumeLayout(False)
        CType(Me.INDMoreColumaPcc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDMoreColumaPcc.ResumeLayout(False)
        CType(Me.LayoutControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl3.ResumeLayout(False)
        CType(Me.LayoutControlGroup5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemMemoEditObservation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdeDocumentDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDpceBuscaFactura.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDglCompany.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDgvSucursales, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDbteConsecutive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlycRoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneralData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiConsecutive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiDateRadication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiDateOfice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiContainer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSendDocumentCommentMte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl4.ResumeLayout(False)
        CType(Me.INDSendDocumentDateDte.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDSendDocumentDateDte.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDUserRadicateTxt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiUserConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyiDateConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MemoEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.LayoutControl5.ResumeLayout(False)
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlGroup3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutControlItem11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents INDbteConsecutive As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents INDlycRoot As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiConsecutive As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlygGeneralData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDglCompany As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents INDgvSucursales As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygInvoice As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyiContainer As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDpceBuscaFactura As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents INDlyiInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlblDateRadicate As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDlyiDateRadication As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdeDocumentDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyiDateOfice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcRadicateD As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgvRadicateD As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDclInvoice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclDateInvoice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclCodeContract As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclValueEntity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclValue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclIngress As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclActions As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IndigoGridView1 As Presentation.Controls.IndigoGridView
    Friend WithEvents IndigoDate1 As Presentation.Controls.IndigoDate
    Friend WithEvents IndigoTextEdit1 As Presentation.Controls.IndigoTextEdit
    Friend WithEvents IndigoPopUpContainerEdit1 As Presentation.Controls.IndigoPopUpContainerEdit
    Friend WithEvents IndigoGroupControl1 As Presentation.Controls.IndigoGroupControl
    Friend WithEvents IndigoLayoutControlGroup1 As Presentation.Controls.IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As Presentation.Controls.IndigoLabelControl
    Friend WithEvents IndigoDate2 As Presentation.Controls.IndigoDate
    Friend WithEvents RepositoryItemPopupContainerEditActions As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents RepositoryItemMemoEditObservation As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents INDpccAcciones As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents INDbtnEliminar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDMoreColumaPcc As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl3 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents CtrXtraInfoInvoiceDetail As Presentation.Controls.CtrXtraInfo
    Friend WithEvents LayoutControlGroup5 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents IndigoGridControl1 As Presentation.Controls.IndigoGridControl
    Friend WithEvents INDclNameContainers As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccInvoice As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LayoutControl2 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents CtrAdvancedFilter As Presentation.Controls.CtrAdvancedFilter
    Friend WithEvents LayoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDLytFiltrosAvanzada As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDgcInvoices As DevExpress.XtraGrid.GridControl
    Friend WithEvents INDgcvInvoices As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Selection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrchSeleccion As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColDevolution As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDrcbiState As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents InvoiceNumberToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents InvoiceDateToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RadicatedNumberToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RadicatedDateToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PatientCodeToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PatientNameToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents IngressNumberToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContractCodeToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContractNameToPersist As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDLytRejillaFacturas As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDBtndisplayAdvancedSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbtnAgregarFactura As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDLytAddInvoice As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDclInvoiceValuePacient As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclAccountantAccountCustomers As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDclDevolution As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemImageDevolution2 As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDimcInvoiceState As DevExpress.Utils.ImageCollection
    Friend WithEvents INDclAccountingMethod As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDgridclAccountantAccountCustomers As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDpccMore As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents INDmeComment As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDpceMore As DevExpress.XtraEditors.PopupContainerEdit
    Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl4 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents INDSendDocumentCommentMte As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDSendDocumentDateDte As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDUserRadicateTxt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup12 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup10 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiUserConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyiDateConfirm As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControl5 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents SimpleButton3 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton4 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents MemoEdit1 As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LayoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents BarManager1 As DevExpress.XtraBars.BarManager
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents INDchkSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents INDlyiSelectAll As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents Clselection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemImageDevolution As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents INDConceptDevolution As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PopupContainerControlMoreInfo As DevExpress.XtraEditors.PopupContainerControl
    Friend WithEvents RepositoryItemPopupContainerEditMoreInfo As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents LayoutControl6 As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents CtrXtraInfoConceptDevolution As Presentation.Controls.CtrXtraInfo
    Friend WithEvents LayoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDbteNit As Presentation.Controls.SearchLookUpEditEx
    Friend WithEvents SearchLookUpEditExView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDchkSelectFiltered As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents RepositoryItemButtonEditActions As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CtrNavigationControlPanel1 As Presentation.Controls.CtrNavigationControlPanel
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDdCurrencyAbbreviation As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPopupContainerEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit
    Friend WithEvents INDColCUV As DevExpress.XtraGrid.Columns.GridColumn
End Class
