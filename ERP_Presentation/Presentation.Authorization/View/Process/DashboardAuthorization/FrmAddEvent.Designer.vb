Imports Presentation.Controls
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAddEvent
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
        Dim AppearanceObject9 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject10 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject1 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject2 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject3 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject4 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject5 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject6 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject7 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Dim AppearanceObject8 As DevExpress.Utils.AppearanceObject = New DevExpress.Utils.AppearanceObject()
        Me.IndigoTextEdit1 = New Presentation.Controls.IndigoTextEdit(Me.components)
        Me.INDsleHealthAdministrator = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDlyRoot = New DevExpress.XtraLayout.LayoutControl()
        Me.INDtxtRadicateNumberWebPage = New DevExpress.XtraEditors.TextEdit()
        Me.INDmemoInformationPatient = New DevExpress.XtraEditors.MemoEdit()
        Me.INDslePatientNotificated = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtPatientPhone = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPatientAddress = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPatientDescription = New DevExpress.XtraEditors.TextEdit()
        Me.INDmemoObservations = New DevExpress.XtraEditors.MemoEdit()
        Me.INDtxtAuthorizationNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDseAuthorizedQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDseRequestQuantity = New DevExpress.XtraEditors.SpinEdit()
        Me.INDsleStatus = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDdteSendDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtEmail = New DevExpress.XtraEditors.TextEdit()
        Me.INDdteRegistrationDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDtxtURL = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtCharge2 = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtReceivePerson = New DevExpress.XtraEditors.TextEdit()
        Me.INDdteReceivedDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDsleSendType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.INDtxtRadicateNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtCharge = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtContactPerson = New DevExpress.XtraEditors.TextEdit()
        Me.INDteEndTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDteInitialTime = New DevExpress.XtraEditors.TimeEdit()
        Me.INDtxtExtension = New DevExpress.XtraEditors.TextEdit()
        Me.INDtxtPhoneNumber = New DevExpress.XtraEditors.TextEdit()
        Me.INDmemoInstructions = New DevExpress.XtraEditors.MemoEdit()
        Me.INDsleReportType = New DevExpress.XtraEditors.SearchLookUpEdit()
        Me.SearchLookUpEdit2View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Root = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlygPrincipalData = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemHealthAdministrator = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemReportType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInstructions = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygGeneral = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemStatus = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRequestQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAuthorizedQuantity = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemAuthorizationNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemObservations = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygNotification = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPatientDescription = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPatientAddress = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPatientPhone = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemPatientNotificated = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInformationPatient = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygCall = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemPhoneNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemExtension = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemInitialTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemEndTime = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemContactPerson = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCharge = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygPhysicalSend = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemRadicateNumber = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSendType = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemReceivedDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemReceivePerson = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemCharge2 = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygWebPage = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemURL = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRegistrationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemRadicateNumberWebPage = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlygEmail = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.INDlyItemEmail = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDlyItemSendDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.IndigoSimpleButton1 = New Presentation.Controls.IndigoSimpleButton(Me.components)
        Me.IndigoSearchLookUpControl1 = New Presentation.Controls.IndigoSearchLookUpControl(Me.components)
        Me.IndigoPopUpContainerEdit1 = New Presentation.Controls.IndigoPopUpContainerEdit(Me.components)
        Me.IndigoLayoutControlGroup1 = New Presentation.Controls.IndigoLayoutControlGroup(Me.components)
        Me.IndigoLabelControl1 = New Presentation.Controls.IndigoLabelControl(Me.components)
        Me.IndigoGroupControl1 = New Presentation.Controls.IndigoGroupControl(Me.components)
        Me.IndigoGridView1 = New Presentation.Controls.IndigoGridView(Me.components)
        Me.IndigoGridControl1 = New Presentation.Controls.IndigoGridControl(Me.components)
        Me.IndigoDate1 = New Presentation.Controls.IndigoDate(Me.components)
        Me.CtrNavigationControlPanel1 = New Presentation.Controls.CtrNavigationControlPanel()
        Me.INDtxtAuthorizedBy = New DevExpress.XtraEditors.TextEdit()
        Me.INDlyItemAuthorizedBy = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDdteAuthorizationDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDlyItemAuthorizationDate = New DevExpress.XtraLayout.LayoutControlItem()
        Me.INDdteAuthorizationExpiredDate = New DevExpress.XtraEditors.DateEdit()
        Me.INDlyItemAuthorizationExpiredDate = New DevExpress.XtraLayout.LayoutControlItem()
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDPanelControlBase.SuspendLayout()
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolBars.SuspendLayout()
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleHealthAdministrator.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.INDlyRoot.SuspendLayout()
        CType(Me.INDtxtRadicateNumberWebPage.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoInformationPatient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDslePatientNotificated.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPatientPhone.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPatientAddress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPatientDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseAuthorizedQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDseRequestQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleStatus.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteSendDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteSendDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteRegistrationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteRegistrationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtURL.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCharge2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtReceivePerson.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteReceivedDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteReceivedDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleSendType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtRadicateNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtCharge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtContactPerson.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDteInitialTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtExtension.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtPhoneNumber.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDmemoInstructions.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDsleReportType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemHealthAdministrator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemReportType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInstructions, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygGeneral, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRequestQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuthorizedQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuthorizationNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygNotification, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPatientDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPatientAddress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPatientPhone, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPatientNotificated, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInformationPatient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygCall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemPhoneNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemExtension, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemInitialTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemContactPerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCharge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygPhysicalSend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRadicateNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSendType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemReceivedDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemReceivePerson, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemCharge2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygWebPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemURL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRegistrationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemRadicateNumberWebPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlygEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemEmail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemSendDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDtxtAuthorizedBy.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuthorizedBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteAuthorizationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteAuthorizationDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuthorizationDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteAuthorizationExpiredDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDdteAuthorizationExpiredDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.INDlyItemAuthorizationExpiredDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'INDPanelControlBase
        '
        Me.INDPanelControlBase.Controls.Add(Me.INDlyRoot)
        Me.INDPanelControlBase.Controls.Add(Me.CtrNavigationControlPanel1)
        '
        'ToolBars
        '
        Me.ToolBars.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.ToolBars.Appearance.Options.UseBackColor = True
        '
        'BarraBotones
        '
        Me.BarraBotones.OperatingUnitVisible = True
        '
        'INDsleHealthAdministrator
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleHealthAdministrator, AppearanceObject9)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleHealthAdministrator, AppearanceObject10)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleHealthAdministrator, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.INDsleHealthAdministrator.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.INDsleHealthAdministrator.Location = New System.Drawing.Point(-1650, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleHealthAdministrator, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleHealthAdministrator.Name = "INDsleHealthAdministrator"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleHealthAdministrator, True)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.INDsleHealthAdministrator.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleHealthAdministrator.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleHealthAdministrator.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleHealthAdministrator.Properties.Appearance.Options.UseFont = True
        Me.INDsleHealthAdministrator.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleHealthAdministrator.Properties.DisplayMember = "CodeName"
        Me.INDsleHealthAdministrator.Properties.NullText = ""
        Me.INDsleHealthAdministrator.Properties.PopupSizeable = False
        Me.INDsleHealthAdministrator.Properties.PopupView = Me.SearchLookUpEdit1View
        Me.INDsleHealthAdministrator.Properties.ShowFooter = False
        Me.INDsleHealthAdministrator.Properties.ValueMember = "Id"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleHealthAdministrator, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleHealthAdministrator, True)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleHealthAdministrator, True)
        Me.INDsleHealthAdministrator.Size = New System.Drawing.Size(386, 28)
        Me.INDsleHealthAdministrator.StyleController = Me.INDlyRoot
        Me.INDsleHealthAdministrator.TabIndex = 0
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleHealthAdministrator, "972")
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleHealthAdministrator, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleHealthAdministrator, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleHealthAdministrator, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleHealthAdministrator, False)
        '
        'SearchLookUpEdit1View
        '
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit1View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit1View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn2, Me.GridColumn3})
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
        Me.GridColumn2.Width = 269
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Nombre"
        Me.GridColumn3.FieldName = "Name"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 1113
        '
        'INDlyRoot
        '
        Me.INDlyRoot.Controls.Add(Me.INDdteAuthorizationExpiredDate)
        Me.INDlyRoot.Controls.Add(Me.INDdteAuthorizationDate)
        Me.INDlyRoot.Controls.Add(Me.INDtxtAuthorizedBy)
        Me.INDlyRoot.Controls.Add(Me.INDtxtRadicateNumberWebPage)
        Me.INDlyRoot.Controls.Add(Me.INDmemoInformationPatient)
        Me.INDlyRoot.Controls.Add(Me.INDslePatientNotificated)
        Me.INDlyRoot.Controls.Add(Me.INDtxtPatientPhone)
        Me.INDlyRoot.Controls.Add(Me.INDtxtPatientAddress)
        Me.INDlyRoot.Controls.Add(Me.INDtxtPatientDescription)
        Me.INDlyRoot.Controls.Add(Me.INDmemoObservations)
        Me.INDlyRoot.Controls.Add(Me.INDtxtAuthorizationNumber)
        Me.INDlyRoot.Controls.Add(Me.INDseAuthorizedQuantity)
        Me.INDlyRoot.Controls.Add(Me.INDseRequestQuantity)
        Me.INDlyRoot.Controls.Add(Me.INDsleStatus)
        Me.INDlyRoot.Controls.Add(Me.INDdteSendDate)
        Me.INDlyRoot.Controls.Add(Me.INDtxtEmail)
        Me.INDlyRoot.Controls.Add(Me.INDdteRegistrationDate)
        Me.INDlyRoot.Controls.Add(Me.INDtxtURL)
        Me.INDlyRoot.Controls.Add(Me.INDtxtCharge2)
        Me.INDlyRoot.Controls.Add(Me.INDtxtReceivePerson)
        Me.INDlyRoot.Controls.Add(Me.INDdteReceivedDate)
        Me.INDlyRoot.Controls.Add(Me.INDsleSendType)
        Me.INDlyRoot.Controls.Add(Me.INDtxtRadicateNumber)
        Me.INDlyRoot.Controls.Add(Me.INDtxtCharge)
        Me.INDlyRoot.Controls.Add(Me.INDtxtContactPerson)
        Me.INDlyRoot.Controls.Add(Me.INDteEndTime)
        Me.INDlyRoot.Controls.Add(Me.INDteInitialTime)
        Me.INDlyRoot.Controls.Add(Me.INDtxtExtension)
        Me.INDlyRoot.Controls.Add(Me.INDtxtPhoneNumber)
        Me.INDlyRoot.Controls.Add(Me.INDmemoInstructions)
        Me.INDlyRoot.Controls.Add(Me.INDsleReportType)
        Me.INDlyRoot.Controls.Add(Me.INDsleHealthAdministrator)
        Me.INDlyRoot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.INDlyRoot.Location = New System.Drawing.Point(202, 7)
        Me.INDlyRoot.Name = "INDlyRoot"
        Me.INDlyRoot.Root = Me.Root
        Me.INDlyRoot.Size = New System.Drawing.Size(1261, 570)
        Me.INDlyRoot.TabIndex = 1
        Me.INDlyRoot.Text = "LayoutControl1"
        '
        'INDtxtRadicateNumberWebPage
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRadicateNumberWebPage, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRadicateNumberWebPage, False)
        Me.INDtxtRadicateNumberWebPage.EnterMoveNextControl = True
        Me.INDtxtRadicateNumberWebPage.Location = New System.Drawing.Point(420, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRadicateNumberWebPage, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtRadicateNumberWebPage.Name = "INDtxtRadicateNumberWebPage"
        Me.INDtxtRadicateNumberWebPage.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtRadicateNumberWebPage.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRadicateNumberWebPage.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRadicateNumberWebPage.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRadicateNumberWebPage.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRadicateNumberWebPage.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtRadicateNumberWebPage.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtRadicateNumberWebPage.StyleController = Me.INDlyRoot
        Me.INDtxtRadicateNumberWebPage.TabIndex = 29
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRadicateNumberWebPage, 0)
        '
        'INDmemoInformationPatient
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoInformationPatient, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoInformationPatient, False)
        Me.INDmemoInformationPatient.EnterMoveNextControl = True
        Me.INDmemoInformationPatient.Location = New System.Drawing.Point(-822, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoInformationPatient, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoInformationPatient.Name = "INDmemoInformationPatient"
        Me.INDmemoInformationPatient.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoInformationPatient.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoInformationPatient.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoInformationPatient.Properties.Appearance.Options.UseFont = True
        Me.INDmemoInformationPatient.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoInformationPatient.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoInformationPatient.Size = New System.Drawing.Size(386, 120)
        Me.INDmemoInformationPatient.StyleController = Me.INDlyRoot
        Me.INDmemoInformationPatient.TabIndex = 15
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoInformationPatient, 0)
        '
        'INDslePatientNotificated
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDslePatientNotificated, AppearanceObject1)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDslePatientNotificated, AppearanceObject2)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDslePatientNotificated, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDslePatientNotificated, False)
        Me.INDslePatientNotificated.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDslePatientNotificated, False)
        Me.INDslePatientNotificated.Location = New System.Drawing.Point(-822, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDslePatientNotificated, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDslePatientNotificated.Name = "INDslePatientNotificated"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDslePatientNotificated, False)
        Me.INDslePatientNotificated.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDslePatientNotificated.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDslePatientNotificated.Properties.Appearance.Options.UseBackColor = True
        Me.INDslePatientNotificated.Properties.Appearance.Options.UseFont = True
        Me.INDslePatientNotificated.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDslePatientNotificated.Properties.DisplayMember = "Item2"
        Me.INDslePatientNotificated.Properties.NullText = ""
        Me.INDslePatientNotificated.Properties.PopupSizeable = False
        Me.INDslePatientNotificated.Properties.PopupView = Me.GridView3
        Me.INDslePatientNotificated.Properties.ShowFooter = False
        Me.INDslePatientNotificated.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDslePatientNotificated, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDslePatientNotificated, True)
        Me.INDslePatientNotificated.Size = New System.Drawing.Size(386, 28)
        Me.INDslePatientNotificated.StyleController = Me.INDlyRoot
        Me.INDslePatientNotificated.TabIndex = 14
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDslePatientNotificated, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDslePatientNotificated, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDslePatientNotificated, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDslePatientNotificated, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDslePatientNotificated, False)
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
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn5})
        Me.GridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView3.Name = "GridView3"
        Me.GridView3.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView3.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView3.OptionsView.EnableAppearanceOddRow = True
        Me.GridView3.OptionsView.ShowAutoFilterRow = True
        Me.GridView3.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView3, False)
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Descripción"
        Me.GridColumn5.FieldName = "Item2"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 0
        '
        'INDtxtPatientPhone
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPatientPhone, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPatientPhone, False)
        Me.INDtxtPatientPhone.EnterMoveNextControl = True
        Me.INDtxtPatientPhone.Location = New System.Drawing.Point(-822, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPatientPhone, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPatientPhone.Name = "INDtxtPatientPhone"
        Me.INDtxtPatientPhone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPatientPhone.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPatientPhone.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPatientPhone.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPatientPhone.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPatientPhone.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPatientPhone.Properties.ReadOnly = True
        Me.INDtxtPatientPhone.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPatientPhone.StyleController = Me.INDlyRoot
        Me.INDtxtPatientPhone.TabIndex = 13
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPatientPhone, 0)
        '
        'INDtxtPatientAddress
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPatientAddress, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPatientAddress, False)
        Me.INDtxtPatientAddress.EnterMoveNextControl = True
        Me.INDtxtPatientAddress.Location = New System.Drawing.Point(-822, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPatientAddress, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPatientAddress.Name = "INDtxtPatientAddress"
        Me.INDtxtPatientAddress.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPatientAddress.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPatientAddress.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPatientAddress.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPatientAddress.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPatientAddress.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPatientAddress.Properties.ReadOnly = True
        Me.INDtxtPatientAddress.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPatientAddress.StyleController = Me.INDlyRoot
        Me.INDtxtPatientAddress.TabIndex = 12
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPatientAddress, 0)
        '
        'INDtxtPatientDescription
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPatientDescription, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPatientDescription, False)
        Me.INDtxtPatientDescription.EnterMoveNextControl = True
        Me.INDtxtPatientDescription.Location = New System.Drawing.Point(-822, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPatientDescription, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPatientDescription.Name = "INDtxtPatientDescription"
        Me.INDtxtPatientDescription.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPatientDescription.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPatientDescription.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPatientDescription.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPatientDescription.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPatientDescription.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPatientDescription.Properties.ReadOnly = True
        Me.INDtxtPatientDescription.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPatientDescription.StyleController = Me.INDlyRoot
        Me.INDtxtPatientDescription.TabIndex = 11
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPatientDescription, 0)
        '
        'INDmemoObservations
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoObservations, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoObservations, False)
        Me.INDmemoObservations.EnterMoveNextControl = True
        Me.INDmemoObservations.Location = New System.Drawing.Point(-1236, 505)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoObservations, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoObservations.Name = "INDmemoObservations"
        Me.INDmemoObservations.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoObservations.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoObservations.Properties.Appearance.Options.UseFont = True
        Me.INDmemoObservations.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoObservations.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoObservations.Size = New System.Drawing.Size(386, 120)
        Me.INDmemoObservations.StyleController = Me.INDlyRoot
        Me.INDmemoObservations.TabIndex = 10
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoObservations, 0)
        '
        'INDtxtAuthorizationNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAuthorizationNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAuthorizationNumber, False)
        Me.INDtxtAuthorizationNumber.EnterMoveNextControl = True
        Me.INDtxtAuthorizationNumber.Location = New System.Drawing.Point(-1236, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAuthorizationNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtAuthorizationNumber.Name = "INDtxtAuthorizationNumber"
        Me.INDtxtAuthorizationNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtAuthorizationNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAuthorizationNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAuthorizationNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAuthorizationNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtAuthorizationNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtAuthorizationNumber.StyleController = Me.INDlyRoot
        Me.INDtxtAuthorizationNumber.TabIndex = 5
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAuthorizationNumber, 0)
        '
        'INDseAuthorizedQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseAuthorizedQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseAuthorizedQuantity, False)
        Me.INDseAuthorizedQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseAuthorizedQuantity.EnterMoveNextControl = True
        Me.INDseAuthorizedQuantity.Location = New System.Drawing.Point(-1236, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDseAuthorizedQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseAuthorizedQuantity.Name = "INDseAuthorizedQuantity"
        Me.INDseAuthorizedQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseAuthorizedQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseAuthorizedQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseAuthorizedQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseAuthorizedQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseAuthorizedQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseAuthorizedQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseAuthorizedQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseAuthorizedQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseAuthorizedQuantity.Properties.MaxLength = 5
        Me.INDseAuthorizedQuantity.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDseAuthorizedQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseAuthorizedQuantity.StyleController = Me.INDlyRoot
        Me.INDseAuthorizedQuantity.TabIndex = 6
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseAuthorizedQuantity, 0)
        '
        'INDseRequestQuantity
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDseRequestQuantity, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDseRequestQuantity, False)
        Me.INDseRequestQuantity.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
        Me.INDseRequestQuantity.EnterMoveNextControl = True
        Me.INDseRequestQuantity.Location = New System.Drawing.Point(-1236, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDseRequestQuantity, Presentation.Controls.IndigoTextEdit.EMask.Numerico)
        Me.INDseRequestQuantity.Name = "INDseRequestQuantity"
        Me.INDseRequestQuantity.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDseRequestQuantity.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseRequestQuantity.Properties.Appearance.Options.UseBackColor = True
        Me.INDseRequestQuantity.Properties.Appearance.Options.UseFont = True
        Me.INDseRequestQuantity.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDseRequestQuantity.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDseRequestQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDseRequestQuantity.Properties.Mask.EditMask = "[0-9]+"
        Me.INDseRequestQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDseRequestQuantity.Properties.MaxLength = 5
        Me.INDseRequestQuantity.Properties.MaxValue = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.INDseRequestQuantity.Properties.ReadOnly = True
        Me.INDseRequestQuantity.Size = New System.Drawing.Size(386, 28)
        Me.INDseRequestQuantity.StyleController = Me.INDlyRoot
        Me.INDseRequestQuantity.TabIndex = 4
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDseRequestQuantity, 0)
        '
        'INDsleStatus
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleStatus, AppearanceObject3)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleStatus, AppearanceObject4)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleStatus, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleStatus, False)
        Me.INDsleStatus.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleStatus, False)
        Me.INDsleStatus.Location = New System.Drawing.Point(-1236, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleStatus, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleStatus.Name = "INDsleStatus"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleStatus, False)
        Me.INDsleStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleStatus.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleStatus.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleStatus.Properties.Appearance.Options.UseFont = True
        Me.INDsleStatus.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleStatus.Properties.DisplayMember = "Item2"
        Me.INDsleStatus.Properties.NullText = ""
        Me.INDsleStatus.Properties.PopupSizeable = False
        Me.INDsleStatus.Properties.PopupView = Me.GridView2
        Me.INDsleStatus.Properties.ShowFooter = False
        Me.INDsleStatus.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleStatus, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleStatus, True)
        Me.INDsleStatus.Size = New System.Drawing.Size(386, 28)
        Me.INDsleStatus.StyleController = Me.INDlyRoot
        Me.INDsleStatus.TabIndex = 3
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleStatus, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleStatus, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleStatus, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleStatus, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleStatus, False)
        '
        'GridView2
        '
        Me.GridView2.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView2.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView2.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView2.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView2.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.GroupRow.Options.UseFont = True
        Me.GridView2.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView2.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView2.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView2.Appearance.Row.Options.UseFont = True
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn6})
        Me.GridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView2.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView2.OptionsView.EnableAppearanceOddRow = True
        Me.GridView2.OptionsView.ShowAutoFilterRow = True
        Me.GridView2.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView2, False)
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Descripción"
        Me.GridColumn6.FieldName = "Item2"
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Visible = True
        Me.GridColumn6.VisibleIndex = 0
        '
        'INDdteSendDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteSendDate, True)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteSendDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteSendDate, False)
        Me.INDdteSendDate.EditValue = Nothing
        Me.INDdteSendDate.EnterMoveNextControl = True
        Me.INDdteSendDate.Location = New System.Drawing.Point(834, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteSendDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteSendDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteSendDate.Name = "INDdteSendDate"
        Me.INDdteSendDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteSendDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteSendDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteSendDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteSendDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteSendDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteSendDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteSendDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteSendDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteSendDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteSendDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteSendDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteSendDate.StyleController = Me.INDlyRoot
        Me.INDdteSendDate.TabIndex = 31
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteSendDate, 0)
        '
        'INDtxtEmail
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtEmail, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtEmail, False)
        Me.INDtxtEmail.EnterMoveNextControl = True
        Me.INDtxtEmail.Location = New System.Drawing.Point(834, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtEmail, Presentation.Controls.IndigoTextEdit.EMask.CorreoElectronico)
        Me.INDtxtEmail.Name = "INDtxtEmail"
        Me.INDtxtEmail.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtEmail.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmail.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtEmail.Properties.Appearance.Options.UseFont = True
        Me.INDtxtEmail.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtEmail.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtEmail.Properties.Mask.EditMask = "([a-zA-Z0-9_\-\.]{0,25})@([a-z]{0,15}\.)([a-z]{0,5})"
        Me.INDtxtEmail.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx
        Me.INDtxtEmail.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtEmail.StyleController = Me.INDlyRoot
        Me.INDtxtEmail.TabIndex = 30
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtEmail, 0)
        '
        'INDdteRegistrationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteRegistrationDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteRegistrationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteRegistrationDate, False)
        Me.INDdteRegistrationDate.EditValue = Nothing
        Me.INDdteRegistrationDate.EnterMoveNextControl = True
        Me.INDdteRegistrationDate.Location = New System.Drawing.Point(420, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteRegistrationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteRegistrationDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteRegistrationDate.Name = "INDdteRegistrationDate"
        Me.INDdteRegistrationDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteRegistrationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteRegistrationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteRegistrationDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteRegistrationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteRegistrationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteRegistrationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteRegistrationDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteRegistrationDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteRegistrationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteRegistrationDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteRegistrationDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteRegistrationDate.StyleController = Me.INDlyRoot
        Me.INDdteRegistrationDate.TabIndex = 28
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteRegistrationDate, 0)
        '
        'INDtxtURL
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtURL, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtURL, False)
        Me.INDtxtURL.EnterMoveNextControl = True
        Me.INDtxtURL.Location = New System.Drawing.Point(420, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtURL, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtURL.Name = "INDtxtURL"
        Me.INDtxtURL.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtURL.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtURL.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtURL.Properties.Appearance.Options.UseFont = True
        Me.INDtxtURL.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtURL.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtURL.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtURL.StyleController = Me.INDlyRoot
        Me.INDtxtURL.TabIndex = 27
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtURL, 0)
        '
        'INDtxtCharge2
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCharge2, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCharge2, False)
        Me.INDtxtCharge2.EnterMoveNextControl = True
        Me.INDtxtCharge2.Location = New System.Drawing.Point(6, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCharge2, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCharge2.Name = "INDtxtCharge2"
        Me.INDtxtCharge2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtCharge2.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCharge2.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCharge2.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCharge2.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCharge2.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCharge2.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtCharge2.StyleController = Me.INDlyRoot
        Me.INDtxtCharge2.TabIndex = 26
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCharge2, 0)
        '
        'INDtxtReceivePerson
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtReceivePerson, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtReceivePerson, False)
        Me.INDtxtReceivePerson.EnterMoveNextControl = True
        Me.INDtxtReceivePerson.Location = New System.Drawing.Point(6, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtReceivePerson, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtReceivePerson.Name = "INDtxtReceivePerson"
        Me.INDtxtReceivePerson.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtReceivePerson.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtReceivePerson.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtReceivePerson.Properties.Appearance.Options.UseFont = True
        Me.INDtxtReceivePerson.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtReceivePerson.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtReceivePerson.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtReceivePerson.StyleController = Me.INDlyRoot
        Me.INDtxtReceivePerson.TabIndex = 25
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtReceivePerson, 0)
        '
        'INDdteReceivedDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteReceivedDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteReceivedDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteReceivedDate, False)
        Me.INDdteReceivedDate.EditValue = Nothing
        Me.INDdteReceivedDate.EnterMoveNextControl = True
        Me.INDdteReceivedDate.Location = New System.Drawing.Point(6, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteReceivedDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteReceivedDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteReceivedDate.Name = "INDdteReceivedDate"
        Me.INDdteReceivedDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteReceivedDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteReceivedDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteReceivedDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteReceivedDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteReceivedDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteReceivedDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteReceivedDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteReceivedDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteReceivedDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteReceivedDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteReceivedDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteReceivedDate.StyleController = Me.INDlyRoot
        Me.INDdteReceivedDate.TabIndex = 24
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteReceivedDate, 0)
        '
        'INDsleSendType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleSendType, AppearanceObject5)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleSendType, AppearanceObject6)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleSendType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleSendType, False)
        Me.INDsleSendType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleSendType, False)
        Me.INDsleSendType.Location = New System.Drawing.Point(6, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleSendType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleSendType.Name = "INDsleSendType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleSendType, False)
        Me.INDsleSendType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleSendType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleSendType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleSendType.Properties.Appearance.Options.UseFont = True
        Me.INDsleSendType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleSendType.Properties.DisplayMember = "Item2"
        Me.INDsleSendType.Properties.NullText = ""
        Me.INDsleSendType.Properties.PopupSizeable = False
        Me.INDsleSendType.Properties.PopupView = Me.GridView1
        Me.INDsleSendType.Properties.ShowFooter = False
        Me.INDsleSendType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleSendType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleSendType, True)
        Me.INDsleSendType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleSendType.StyleController = Me.INDlyRoot
        Me.INDsleSendType.TabIndex = 23
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleSendType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleSendType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleSendType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleSendType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleSendType, False)
        '
        'GridView1
        '
        Me.GridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.GridView1.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.GridView1.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.GridView1.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView1.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.GroupRow.Options.UseFont = True
        Me.GridView1.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.Appearance.HeaderPanel.Options.UseFont = True
        Me.GridView1.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.GridView1.Appearance.Row.Options.UseFont = True
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn4})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.EnableAppearanceOddRow = True
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.GridView1, False)
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Descripción"
        Me.GridColumn4.FieldName = "Item2"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 0
        '
        'INDtxtRadicateNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtRadicateNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtRadicateNumber, False)
        Me.INDtxtRadicateNumber.EnterMoveNextControl = True
        Me.INDtxtRadicateNumber.Location = New System.Drawing.Point(6, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtRadicateNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtRadicateNumber.Name = "INDtxtRadicateNumber"
        Me.INDtxtRadicateNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtRadicateNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRadicateNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtRadicateNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtRadicateNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtRadicateNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtRadicateNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtRadicateNumber.StyleController = Me.INDlyRoot
        Me.INDtxtRadicateNumber.TabIndex = 22
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtRadicateNumber, 0)
        '
        'INDtxtCharge
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtCharge, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtCharge, False)
        Me.INDtxtCharge.EnterMoveNextControl = True
        Me.INDtxtCharge.Location = New System.Drawing.Point(-408, 385)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtCharge, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtCharge.Name = "INDtxtCharge"
        Me.INDtxtCharge.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtCharge.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCharge.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtCharge.Properties.Appearance.Options.UseFont = True
        Me.INDtxtCharge.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtCharge.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtCharge.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtCharge.StyleController = Me.INDlyRoot
        Me.INDtxtCharge.TabIndex = 21
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtCharge, 0)
        '
        'INDtxtContactPerson
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtContactPerson, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtContactPerson, False)
        Me.INDtxtContactPerson.EnterMoveNextControl = True
        Me.INDtxtContactPerson.Location = New System.Drawing.Point(-408, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtContactPerson, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtContactPerson.Name = "INDtxtContactPerson"
        Me.INDtxtContactPerson.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtContactPerson.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtContactPerson.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtContactPerson.Properties.Appearance.Options.UseFont = True
        Me.INDtxtContactPerson.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtContactPerson.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtContactPerson.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtContactPerson.StyleController = Me.INDlyRoot
        Me.INDtxtContactPerson.TabIndex = 20
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtContactPerson, 0)
        '
        'INDteEndTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteEndTime, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteEndTime, False)
        Me.INDteEndTime.EditValue = New Date(2020, 6, 2, 0, 0, 0, 0)
        Me.INDteEndTime.EnterMoveNextControl = True
        Me.INDteEndTime.Location = New System.Drawing.Point(-408, 265)
        Me.IndigoTextEdit1.SetMascara(Me.INDteEndTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteEndTime.Name = "INDteEndTime"
        Me.INDteEndTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteEndTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteEndTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDteEndTime.Properties.Appearance.Options.UseFont = True
        Me.INDteEndTime.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteEndTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteEndTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDteEndTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteEndTime.Size = New System.Drawing.Size(386, 28)
        Me.INDteEndTime.StyleController = Me.INDlyRoot
        Me.INDteEndTime.TabIndex = 19
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteEndTime, 0)
        '
        'INDteInitialTime
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDteInitialTime, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDteInitialTime, False)
        Me.INDteInitialTime.EditValue = New Date(2020, 6, 2, 0, 0, 0, 0)
        Me.INDteInitialTime.EnterMoveNextControl = True
        Me.INDteInitialTime.Location = New System.Drawing.Point(-408, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDteInitialTime, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDteInitialTime.Name = "INDteInitialTime"
        Me.INDteInitialTime.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDteInitialTime.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteInitialTime.Properties.Appearance.Options.UseBackColor = True
        Me.INDteInitialTime.Properties.Appearance.Options.UseFont = True
        Me.INDteInitialTime.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDteInitialTime.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDteInitialTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDteInitialTime.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDteInitialTime.Size = New System.Drawing.Size(386, 28)
        Me.INDteInitialTime.StyleController = Me.INDlyRoot
        Me.INDteInitialTime.TabIndex = 18
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDteInitialTime, 0)
        '
        'INDtxtExtension
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtExtension, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtExtension, False)
        Me.INDtxtExtension.EnterMoveNextControl = True
        Me.INDtxtExtension.Location = New System.Drawing.Point(-408, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtExtension, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtExtension.Name = "INDtxtExtension"
        Me.INDtxtExtension.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtExtension.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExtension.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtExtension.Properties.Appearance.Options.UseFont = True
        Me.INDtxtExtension.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtExtension.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtExtension.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtExtension.StyleController = Me.INDlyRoot
        Me.INDtxtExtension.TabIndex = 17
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtExtension, 0)
        '
        'INDtxtPhoneNumber
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtPhoneNumber, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtPhoneNumber, False)
        Me.INDtxtPhoneNumber.EnterMoveNextControl = True
        Me.INDtxtPhoneNumber.Location = New System.Drawing.Point(-408, 85)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtPhoneNumber, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtPhoneNumber.Name = "INDtxtPhoneNumber"
        Me.INDtxtPhoneNumber.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtPhoneNumber.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPhoneNumber.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtPhoneNumber.Properties.Appearance.Options.UseFont = True
        Me.INDtxtPhoneNumber.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtPhoneNumber.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtPhoneNumber.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtPhoneNumber.StyleController = Me.INDlyRoot
        Me.INDtxtPhoneNumber.TabIndex = 16
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtPhoneNumber, 0)
        '
        'INDmemoInstructions
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDmemoInstructions, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDmemoInstructions, False)
        Me.INDmemoInstructions.EnterMoveNextControl = True
        Me.INDmemoInstructions.Location = New System.Drawing.Point(-1650, 205)
        Me.IndigoTextEdit1.SetMascara(Me.INDmemoInstructions, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDmemoInstructions.Name = "INDmemoInstructions"
        Me.INDmemoInstructions.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDmemoInstructions.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoInstructions.Properties.Appearance.Options.UseBackColor = True
        Me.INDmemoInstructions.Properties.Appearance.Options.UseFont = True
        Me.INDmemoInstructions.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDmemoInstructions.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDmemoInstructions.Properties.MaxLength = 2000
        Me.INDmemoInstructions.Size = New System.Drawing.Size(386, 120)
        Me.INDmemoInstructions.StyleController = Me.INDlyRoot
        Me.INDmemoInstructions.TabIndex = 2
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDmemoInstructions, 0)
        '
        'INDsleReportType
        '
        Me.IndigoSearchLookUpControl1.SetAppearanceEmbeddedNavigator(Me.INDsleReportType, AppearanceObject7)
        Me.IndigoSearchLookUpControl1.SetAppearanceTextFindControl(Me.INDsleReportType, AppearanceObject8)
        Me.IndigoSearchLookUpControl1.SetAppendButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDsleReportType, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetCancelEditButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetEditButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetEndEditButtonNavigator(Me.INDsleReportType, False)
        Me.INDsleReportType.EnterMoveNextControl = True
        Me.IndigoSearchLookUpControl1.SetExportButton(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetFirstButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetLastButtonNavigator(Me.INDsleReportType, False)
        Me.INDsleReportType.Location = New System.Drawing.Point(-1650, 145)
        Me.IndigoTextEdit1.SetMascara(Me.INDsleReportType, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDsleReportType.Name = "INDsleReportType"
        Me.IndigoSearchLookUpControl1.SetNextButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetNextPageButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetOpenForm(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetPrevButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetPrevPageButtonNavigator(Me.INDsleReportType, False)
        Me.INDsleReportType.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDsleReportType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDsleReportType.Properties.Appearance.Options.UseBackColor = True
        Me.INDsleReportType.Properties.Appearance.Options.UseFont = True
        Me.INDsleReportType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDsleReportType.Properties.DisplayMember = "Item2"
        Me.INDsleReportType.Properties.NullText = ""
        Me.INDsleReportType.Properties.PopupSizeable = False
        Me.INDsleReportType.Properties.PopupView = Me.SearchLookUpEdit2View
        Me.INDsleReportType.Properties.ShowFooter = False
        Me.INDsleReportType.Properties.ValueMember = "Item1"
        Me.IndigoSearchLookUpControl1.SetRemoveButtonNavigator(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetSaveXmlGrid(Me.INDsleReportType, True)
        Me.IndigoSearchLookUpControl1.SetShowDeleteButton(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetShowFindButton(Me.INDsleReportType, True)
        Me.INDsleReportType.Size = New System.Drawing.Size(386, 28)
        Me.INDsleReportType.StyleController = Me.INDlyRoot
        Me.INDsleReportType.TabIndex = 1
        Me.IndigoSearchLookUpControl1.SetTagForm(Me.INDsleReportType, Nothing)
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDsleReportType, 0)
        Me.IndigoSearchLookUpControl1.SetTextStringFormat(Me.INDsleReportType, "{0} - {1}")
        Me.IndigoSearchLookUpControl1.SetTxtFindEnterEnabled(Me.INDsleReportType, False)
        Me.IndigoSearchLookUpControl1.SetUseEmbeddedNavigator(Me.INDsleReportType, False)
        '
        'SearchLookUpEdit2View
        '
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.BorderColor = System.Drawing.Color.LightGray
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseBorderColor = True
        Me.SearchLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.GroupRow.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SearchLookUpEdit2View.Appearance.HeaderPanel.Options.UseFont = True
        Me.SearchLookUpEdit2View.Appearance.Row.Font = New System.Drawing.Font("Segoe UI Light", 9.75!)
        Me.SearchLookUpEdit2View.Appearance.Row.Options.UseFont = True
        Me.SearchLookUpEdit2View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1})
        Me.SearchLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.SearchLookUpEdit2View.Name = "SearchLookUpEdit2View"
        Me.SearchLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceEvenRow = True
        Me.SearchLookUpEdit2View.OptionsView.EnableAppearanceOddRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowAutoFilterRow = True
        Me.SearchLookUpEdit2View.OptionsView.ShowGroupPanel = False
        Me.IndigoGridView1.SetTemaIndigoMetro(Me.SearchLookUpEdit2View, False)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Descripción"
        Me.GridColumn1.FieldName = "Item2"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        '
        'Root
        '
        Me.Root.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceGroup.Options.UseFont = True
        Me.Root.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Root.AppearanceItemCaption.Options.UseFont = True
        Me.Root.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.Header.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Root.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.Root.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.Root.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.Root.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.Root, False)
        Me.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.Root.GroupBordersVisible = False
        Me.Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlygPrincipalData, Me.INDlygGeneral, Me.INDlygNotification, Me.INDlygCall, Me.INDlygPhysicalSend, Me.INDlygWebPage, Me.INDlygEmail})
        Me.Root.Name = "Root"
        Me.Root.Size = New System.Drawing.Size(2918, 649)
        Me.Root.TextVisible = False
        '
        'INDlygPrincipalData
        '
        Me.INDlygPrincipalData.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceGroup.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPrincipalData.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPrincipalData.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPrincipalData, False)
        Me.INDlygPrincipalData.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemHealthAdministrator, Me.INDlyItemReportType, Me.INDlyItemInstructions})
        Me.INDlygPrincipalData.Location = New System.Drawing.Point(0, 0)
        Me.INDlygPrincipalData.Name = "INDlygPrincipalData"
        Me.INDlygPrincipalData.Size = New System.Drawing.Size(414, 629)
        Me.INDlygPrincipalData.Text = "Datos Principales"
        '
        'INDlyItemHealthAdministrator
        '
        Me.INDlyItemHealthAdministrator.Control = Me.INDsleHealthAdministrator
        Me.INDlyItemHealthAdministrator.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemHealthAdministrator.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemHealthAdministrator.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemHealthAdministrator.Name = "INDlyItemHealthAdministrator"
        Me.INDlyItemHealthAdministrator.ShowInCustomizationForm = False
        Me.INDlyItemHealthAdministrator.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemHealthAdministrator.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemHealthAdministrator.Text = "Entidad"
        Me.INDlyItemHealthAdministrator.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemHealthAdministrator.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemHealthAdministrator.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemHealthAdministrator.TextToControlDistance = 5
        '
        'INDlyItemReportType
        '
        Me.INDlyItemReportType.Control = Me.INDsleReportType
        Me.INDlyItemReportType.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemReportType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReportType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReportType.Name = "INDlyItemReportType"
        Me.INDlyItemReportType.ShowInCustomizationForm = False
        Me.INDlyItemReportType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemReportType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemReportType.Text = "Tipo Reporte"
        Me.INDlyItemReportType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemReportType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemReportType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemReportType.TextToControlDistance = 5
        '
        'INDlyItemInstructions
        '
        Me.INDlyItemInstructions.Control = Me.INDmemoInstructions
        Me.INDlyItemInstructions.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemInstructions.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemInstructions.MinSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemInstructions.Name = "INDlyItemInstructions"
        Me.INDlyItemInstructions.Size = New System.Drawing.Size(390, 450)
        Me.INDlyItemInstructions.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInstructions.Text = "Instrucciones"
        Me.INDlyItemInstructions.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInstructions.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInstructions.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInstructions.TextToControlDistance = 5
        '
        'INDlygGeneral
        '
        Me.INDlygGeneral.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneral.AppearanceGroup.Options.UseFont = True
        Me.INDlygGeneral.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygGeneral.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygGeneral.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneral.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygGeneral.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygGeneral.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygGeneral.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneral.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygGeneral.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneral.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygGeneral.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygGeneral.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygGeneral, False)
        Me.INDlygGeneral.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemStatus, Me.INDlyItemRequestQuantity, Me.INDlyItemAuthorizedQuantity, Me.INDlyItemAuthorizationNumber, Me.INDlyItemObservations, Me.INDlyItemAuthorizedBy, Me.INDlyItemAuthorizationDate, Me.INDlyItemAuthorizationExpiredDate})
        Me.INDlygGeneral.Location = New System.Drawing.Point(414, 0)
        Me.INDlygGeneral.Name = "INDlygGeneral"
        Me.INDlygGeneral.Size = New System.Drawing.Size(414, 629)
        Me.INDlygGeneral.Text = "Generales"
        '
        'INDlyItemStatus
        '
        Me.INDlyItemStatus.Control = Me.INDsleStatus
        Me.INDlyItemStatus.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemStatus.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStatus.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemStatus.Name = "INDlyItemStatus"
        Me.INDlyItemStatus.ShowInCustomizationForm = False
        Me.INDlyItemStatus.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemStatus.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemStatus.Text = "Estado"
        Me.INDlyItemStatus.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemStatus.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemStatus.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemStatus.TextToControlDistance = 5
        '
        'INDlyItemRequestQuantity
        '
        Me.INDlyItemRequestQuantity.Control = Me.INDseRequestQuantity
        Me.INDlyItemRequestQuantity.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemRequestQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRequestQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRequestQuantity.Name = "INDlyItemRequestQuantity"
        Me.INDlyItemRequestQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemRequestQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRequestQuantity.Text = "Cantidad Solicitada"
        Me.INDlyItemRequestQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRequestQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRequestQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRequestQuantity.TextToControlDistance = 5
        '
        'INDlyItemAuthorizedQuantity
        '
        Me.INDlyItemAuthorizedQuantity.Control = Me.INDseAuthorizedQuantity
        Me.INDlyItemAuthorizedQuantity.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemAuthorizedQuantity.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizedQuantity.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizedQuantity.Name = "INDlyItemAuthorizedQuantity"
        Me.INDlyItemAuthorizedQuantity.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizedQuantity.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuthorizedQuantity.Text = "Cantidad Autorizada"
        Me.INDlyItemAuthorizedQuantity.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuthorizedQuantity.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAuthorizedQuantity.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAuthorizedQuantity.TextToControlDistance = 5
        Me.INDlyItemAuthorizedQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemAuthorizationNumber
        '
        Me.INDlyItemAuthorizationNumber.Control = Me.INDtxtAuthorizationNumber
        Me.INDlyItemAuthorizationNumber.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemAuthorizationNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationNumber.Name = "INDlyItemAuthorizationNumber"
        Me.INDlyItemAuthorizationNumber.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuthorizationNumber.Text = "No. Autorización"
        Me.INDlyItemAuthorizationNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuthorizationNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAuthorizationNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAuthorizationNumber.TextToControlDistance = 5
        Me.INDlyItemAuthorizationNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemObservations
        '
        Me.INDlyItemObservations.Control = Me.INDmemoObservations
        Me.INDlyItemObservations.Location = New System.Drawing.Point(0, 420)
        Me.INDlyItemObservations.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemObservations.MinSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemObservations.Name = "INDlyItemObservations"
        Me.INDlyItemObservations.Size = New System.Drawing.Size(390, 150)
        Me.INDlyItemObservations.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemObservations.Text = "Observaciones"
        Me.INDlyItemObservations.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemObservations.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemObservations.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemObservations.TextToControlDistance = 5
        '
        'INDlygNotification
        '
        Me.INDlygNotification.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygNotification.AppearanceGroup.Options.UseFont = True
        Me.INDlygNotification.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygNotification.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygNotification.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotification.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygNotification.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygNotification.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygNotification.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotification.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygNotification.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotification.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygNotification.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygNotification.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygNotification, False)
        Me.INDlygNotification.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPatientDescription, Me.INDlyItemPatientAddress, Me.INDlyItemPatientPhone, Me.INDlyItemPatientNotificated, Me.INDlyItemInformationPatient})
        Me.INDlygNotification.Location = New System.Drawing.Point(828, 0)
        Me.INDlygNotification.Name = "INDlygNotification"
        Me.INDlygNotification.Size = New System.Drawing.Size(414, 629)
        Me.INDlygNotification.Text = "Notificación Paciente"
        '
        'INDlyItemPatientDescription
        '
        Me.INDlyItemPatientDescription.Control = Me.INDtxtPatientDescription
        Me.INDlyItemPatientDescription.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPatientDescription.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientDescription.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientDescription.Name = "INDlyItemPatientDescription"
        Me.INDlyItemPatientDescription.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientDescription.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPatientDescription.Text = "Paciente"
        Me.INDlyItemPatientDescription.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPatientDescription.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPatientDescription.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPatientDescription.TextToControlDistance = 5
        '
        'INDlyItemPatientAddress
        '
        Me.INDlyItemPatientAddress.Control = Me.INDtxtPatientAddress
        Me.INDlyItemPatientAddress.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemPatientAddress.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientAddress.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientAddress.Name = "INDlyItemPatientAddress"
        Me.INDlyItemPatientAddress.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientAddress.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPatientAddress.Text = "Dirección"
        Me.INDlyItemPatientAddress.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPatientAddress.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPatientAddress.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPatientAddress.TextToControlDistance = 5
        '
        'INDlyItemPatientPhone
        '
        Me.INDlyItemPatientPhone.Control = Me.INDtxtPatientPhone
        Me.INDlyItemPatientPhone.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemPatientPhone.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientPhone.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientPhone.Name = "INDlyItemPatientPhone"
        Me.INDlyItemPatientPhone.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientPhone.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPatientPhone.Text = "Teléfono"
        Me.INDlyItemPatientPhone.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPatientPhone.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPatientPhone.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPatientPhone.TextToControlDistance = 5
        '
        'INDlyItemPatientNotificated
        '
        Me.INDlyItemPatientNotificated.Control = Me.INDslePatientNotificated
        Me.INDlyItemPatientNotificated.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemPatientNotificated.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientNotificated.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientNotificated.Name = "INDlyItemPatientNotificated"
        Me.INDlyItemPatientNotificated.ShowInCustomizationForm = False
        Me.INDlyItemPatientNotificated.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemPatientNotificated.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPatientNotificated.Text = "Paciente Notificado"
        Me.INDlyItemPatientNotificated.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPatientNotificated.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPatientNotificated.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPatientNotificated.TextToControlDistance = 5
        '
        'INDlyItemInformationPatient
        '
        Me.INDlyItemInformationPatient.Control = Me.INDmemoInformationPatient
        Me.INDlyItemInformationPatient.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemInformationPatient.MaxSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemInformationPatient.MinSize = New System.Drawing.Size(390, 150)
        Me.INDlyItemInformationPatient.Name = "INDlyItemInformationPatient"
        Me.INDlyItemInformationPatient.Size = New System.Drawing.Size(390, 330)
        Me.INDlyItemInformationPatient.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInformationPatient.Text = "Información Dada al Paciente"
        Me.INDlyItemInformationPatient.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInformationPatient.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInformationPatient.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInformationPatient.TextToControlDistance = 5
        '
        'INDlygCall
        '
        Me.INDlygCall.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCall.AppearanceGroup.Options.UseFont = True
        Me.INDlygCall.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygCall.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygCall.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCall.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygCall.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygCall.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygCall.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCall.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygCall.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCall.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygCall.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygCall.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygCall, False)
        Me.INDlygCall.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemPhoneNumber, Me.INDlyItemExtension, Me.INDlyItemInitialTime, Me.INDlyItemEndTime, Me.INDlyItemContactPerson, Me.INDlyItemCharge})
        Me.INDlygCall.Location = New System.Drawing.Point(1242, 0)
        Me.INDlygCall.Name = "INDlygCall"
        Me.INDlygCall.Size = New System.Drawing.Size(414, 629)
        Me.INDlygCall.Text = "Llamada Telefónica"
        Me.INDlygCall.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemPhoneNumber
        '
        Me.INDlyItemPhoneNumber.Control = Me.INDtxtPhoneNumber
        Me.INDlyItemPhoneNumber.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemPhoneNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPhoneNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemPhoneNumber.Name = "INDlyItemPhoneNumber"
        Me.INDlyItemPhoneNumber.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemPhoneNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemPhoneNumber.Text = "Número Telefónico"
        Me.INDlyItemPhoneNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemPhoneNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemPhoneNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemPhoneNumber.TextToControlDistance = 5
        '
        'INDlyItemExtension
        '
        Me.INDlyItemExtension.Control = Me.INDtxtExtension
        Me.INDlyItemExtension.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemExtension.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemExtension.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemExtension.Name = "INDlyItemExtension"
        Me.INDlyItemExtension.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemExtension.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemExtension.Text = "Extensión"
        Me.INDlyItemExtension.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemExtension.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemExtension.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemExtension.TextToControlDistance = 5
        '
        'INDlyItemInitialTime
        '
        Me.INDlyItemInitialTime.Control = Me.INDteInitialTime
        Me.INDlyItemInitialTime.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemInitialTime.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialTime.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialTime.Name = "INDlyItemInitialTime"
        Me.INDlyItemInitialTime.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemInitialTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemInitialTime.Text = "Hora Inicial"
        Me.INDlyItemInitialTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemInitialTime.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemInitialTime.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemInitialTime.TextToControlDistance = 5
        '
        'INDlyItemEndTime
        '
        Me.INDlyItemEndTime.Control = Me.INDteEndTime
        Me.INDlyItemEndTime.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemEndTime.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndTime.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndTime.Name = "INDlyItemEndTime"
        Me.INDlyItemEndTime.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemEndTime.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEndTime.Text = "Hora Final"
        Me.INDlyItemEndTime.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEndTime.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEndTime.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEndTime.TextToControlDistance = 5
        '
        'INDlyItemContactPerson
        '
        Me.INDlyItemContactPerson.Control = Me.INDtxtContactPerson
        Me.INDlyItemContactPerson.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemContactPerson.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemContactPerson.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemContactPerson.Name = "INDlyItemContactPerson"
        Me.INDlyItemContactPerson.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemContactPerson.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemContactPerson.Text = "Persona Contacto"
        Me.INDlyItemContactPerson.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemContactPerson.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemContactPerson.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemContactPerson.TextToControlDistance = 5
        '
        'INDlyItemCharge
        '
        Me.INDlyItemCharge.Control = Me.INDtxtCharge
        Me.INDlyItemCharge.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemCharge.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCharge.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCharge.Name = "INDlyItemCharge"
        Me.INDlyItemCharge.Size = New System.Drawing.Size(390, 270)
        Me.INDlyItemCharge.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCharge.Text = "Cargo"
        Me.INDlyItemCharge.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCharge.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCharge.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCharge.TextToControlDistance = 5
        '
        'INDlygPhysicalSend
        '
        Me.INDlygPhysicalSend.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPhysicalSend.AppearanceGroup.Options.UseFont = True
        Me.INDlygPhysicalSend.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygPhysicalSend.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygPhysicalSend.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPhysicalSend.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygPhysicalSend.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygPhysicalSend.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygPhysicalSend.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPhysicalSend.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygPhysicalSend.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPhysicalSend.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygPhysicalSend.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygPhysicalSend.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygPhysicalSend, False)
        Me.INDlygPhysicalSend.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemRadicateNumber, Me.INDlyItemSendType, Me.INDlyItemReceivedDate, Me.INDlyItemReceivePerson, Me.INDlyItemCharge2})
        Me.INDlygPhysicalSend.Location = New System.Drawing.Point(1656, 0)
        Me.INDlygPhysicalSend.Name = "INDlygPhysicalSend"
        Me.INDlygPhysicalSend.Size = New System.Drawing.Size(414, 629)
        Me.INDlygPhysicalSend.Text = "Envío Físico"
        Me.INDlygPhysicalSend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemRadicateNumber
        '
        Me.INDlyItemRadicateNumber.Control = Me.INDtxtRadicateNumber
        Me.INDlyItemRadicateNumber.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemRadicateNumber.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRadicateNumber.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRadicateNumber.Name = "INDlyItemRadicateNumber"
        Me.INDlyItemRadicateNumber.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemRadicateNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRadicateNumber.Text = "No. Radicado"
        Me.INDlyItemRadicateNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRadicateNumber.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRadicateNumber.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRadicateNumber.TextToControlDistance = 5
        '
        'INDlyItemSendType
        '
        Me.INDlyItemSendType.Control = Me.INDsleSendType
        Me.INDlyItemSendType.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemSendType.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendType.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendType.Name = "INDlyItemSendType"
        Me.INDlyItemSendType.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSendType.Text = "Tipo Envío"
        Me.INDlyItemSendType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSendType.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSendType.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSendType.TextToControlDistance = 5
        '
        'INDlyItemReceivedDate
        '
        Me.INDlyItemReceivedDate.Control = Me.INDdteReceivedDate
        Me.INDlyItemReceivedDate.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemReceivedDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReceivedDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReceivedDate.Name = "INDlyItemReceivedDate"
        Me.INDlyItemReceivedDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemReceivedDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemReceivedDate.Text = "Fecha Recibido"
        Me.INDlyItemReceivedDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemReceivedDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemReceivedDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemReceivedDate.TextToControlDistance = 5
        '
        'INDlyItemReceivePerson
        '
        Me.INDlyItemReceivePerson.Control = Me.INDtxtReceivePerson
        Me.INDlyItemReceivePerson.Location = New System.Drawing.Point(0, 180)
        Me.INDlyItemReceivePerson.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReceivePerson.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemReceivePerson.Name = "INDlyItemReceivePerson"
        Me.INDlyItemReceivePerson.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemReceivePerson.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemReceivePerson.Text = "Persona Recibe"
        Me.INDlyItemReceivePerson.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemReceivePerson.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemReceivePerson.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemReceivePerson.TextToControlDistance = 5
        '
        'INDlyItemCharge2
        '
        Me.INDlyItemCharge2.Control = Me.INDtxtCharge2
        Me.INDlyItemCharge2.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemCharge2.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCharge2.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemCharge2.Name = "INDlyItemCharge2"
        Me.INDlyItemCharge2.Size = New System.Drawing.Size(390, 330)
        Me.INDlyItemCharge2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemCharge2.Text = "Cargo"
        Me.INDlyItemCharge2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemCharge2.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemCharge2.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemCharge2.TextToControlDistance = 5
        '
        'INDlygWebPage
        '
        Me.INDlygWebPage.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygWebPage.AppearanceGroup.Options.UseFont = True
        Me.INDlygWebPage.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygWebPage.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygWebPage.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygWebPage.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygWebPage.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygWebPage.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygWebPage.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygWebPage.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygWebPage.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygWebPage.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygWebPage.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygWebPage.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygWebPage, False)
        Me.INDlygWebPage.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemURL, Me.INDlyItemRegistrationDate, Me.INDlyItemRadicateNumberWebPage})
        Me.INDlygWebPage.Location = New System.Drawing.Point(2070, 0)
        Me.INDlygWebPage.Name = "INDlygWebPage"
        Me.INDlygWebPage.Size = New System.Drawing.Size(414, 629)
        Me.INDlygWebPage.Text = "Página Web"
        Me.INDlygWebPage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemURL
        '
        Me.INDlyItemURL.Control = Me.INDtxtURL
        Me.INDlyItemURL.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemURL.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemURL.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemURL.Name = "INDlyItemURL"
        Me.INDlyItemURL.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemURL.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemURL.Text = "URL Página Web"
        Me.INDlyItemURL.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemURL.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemURL.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemURL.TextToControlDistance = 5
        '
        'INDlyItemRegistrationDate
        '
        Me.INDlyItemRegistrationDate.Control = Me.INDdteRegistrationDate
        Me.INDlyItemRegistrationDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemRegistrationDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRegistrationDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRegistrationDate.Name = "INDlyItemRegistrationDate"
        Me.INDlyItemRegistrationDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemRegistrationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRegistrationDate.Text = "Fecha Registro"
        Me.INDlyItemRegistrationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRegistrationDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRegistrationDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRegistrationDate.TextToControlDistance = 5
        '
        'INDlyItemRadicateNumberWebPage
        '
        Me.INDlyItemRadicateNumberWebPage.Control = Me.INDtxtRadicateNumberWebPage
        Me.INDlyItemRadicateNumberWebPage.Location = New System.Drawing.Point(0, 120)
        Me.INDlyItemRadicateNumberWebPage.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRadicateNumberWebPage.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemRadicateNumberWebPage.Name = "INDlyItemRadicateNumberWebPage"
        Me.INDlyItemRadicateNumberWebPage.Size = New System.Drawing.Size(390, 450)
        Me.INDlyItemRadicateNumberWebPage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemRadicateNumberWebPage.Text = "No. Radicado"
        Me.INDlyItemRadicateNumberWebPage.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemRadicateNumberWebPage.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemRadicateNumberWebPage.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemRadicateNumberWebPage.TextToControlDistance = 5
        '
        'INDlygEmail
        '
        Me.INDlygEmail.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygEmail.AppearanceGroup.Options.UseFont = True
        Me.INDlygEmail.AppearanceItemCaption.Font = New System.Drawing.Font("Segoe UI Light", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INDlygEmail.AppearanceItemCaption.Options.UseFont = True
        Me.INDlygEmail.AppearanceTabPage.Header.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEmail.AppearanceTabPage.Header.Options.UseFont = True
        Me.INDlygEmail.AppearanceTabPage.HeaderActive.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.INDlygEmail.AppearanceTabPage.HeaderActive.Options.UseFont = True
        Me.INDlygEmail.AppearanceTabPage.HeaderDisabled.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEmail.AppearanceTabPage.HeaderDisabled.Options.UseFont = True
        Me.INDlygEmail.AppearanceTabPage.HeaderHotTracked.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEmail.AppearanceTabPage.HeaderHotTracked.Options.UseFont = True
        Me.INDlygEmail.AppearanceTabPage.PageClient.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDlygEmail.AppearanceTabPage.PageClient.Options.UseFont = True
        Me.IndigoLayoutControlGroup1.SetCampoObligatorio(Me.INDlygEmail, False)
        Me.INDlygEmail.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.INDlyItemEmail, Me.INDlyItemSendDate})
        Me.INDlygEmail.Location = New System.Drawing.Point(2484, 0)
        Me.INDlygEmail.Name = "INDlygEmail"
        Me.INDlygEmail.Size = New System.Drawing.Size(414, 629)
        Me.INDlygEmail.Text = "Correo Electrónico"
        Me.INDlygEmail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDlyItemEmail
        '
        Me.INDlyItemEmail.Control = Me.INDtxtEmail
        Me.INDlyItemEmail.Location = New System.Drawing.Point(0, 0)
        Me.INDlyItemEmail.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEmail.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemEmail.Name = "INDlyItemEmail"
        Me.INDlyItemEmail.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemEmail.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemEmail.Text = "Email"
        Me.INDlyItemEmail.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemEmail.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemEmail.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemEmail.TextToControlDistance = 5
        '
        'INDlyItemSendDate
        '
        Me.INDlyItemSendDate.Control = Me.INDdteSendDate
        Me.INDlyItemSendDate.Location = New System.Drawing.Point(0, 60)
        Me.INDlyItemSendDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemSendDate.Name = "INDlyItemSendDate"
        Me.INDlyItemSendDate.Size = New System.Drawing.Size(390, 510)
        Me.INDlyItemSendDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemSendDate.Text = "Fecha Envío"
        Me.INDlyItemSendDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemSendDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemSendDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemSendDate.TextToControlDistance = 5
        '
        'IndigoGridView1
        '
        Me.IndigoGridView1.RaiseMenuPopUp = True
        '
        'CtrNavigationControlPanel1
        '
        Me.CtrNavigationControlPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.CtrNavigationControlPanel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.CtrNavigationControlPanel1.LayoutControl = Me.INDlyRoot
        Me.CtrNavigationControlPanel1.Location = New System.Drawing.Point(2, 7)
        Me.CtrNavigationControlPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.CtrNavigationControlPanel1.Name = "CtrNavigationControlPanel1"
        Me.CtrNavigationControlPanel1.Size = New System.Drawing.Size(200, 570)
        Me.CtrNavigationControlPanel1.TabIndex = 0
        Me.CtrNavigationControlPanel1.UseDisabledStatePainter = False
        '
        'INDtxtAuthorizedBy
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDtxtAuthorizedBy, True)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDtxtAuthorizedBy, False)
        Me.INDtxtAuthorizedBy.EnterMoveNextControl = True
        Me.INDtxtAuthorizedBy.Location = New System.Drawing.Point(-1236, 325)
        Me.IndigoTextEdit1.SetMascara(Me.INDtxtAuthorizedBy, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.INDtxtAuthorizedBy.Name = "INDtxtAuthorizedBy"
        Me.INDtxtAuthorizedBy.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDtxtAuthorizedBy.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAuthorizedBy.Properties.Appearance.Options.UseBackColor = True
        Me.INDtxtAuthorizedBy.Properties.Appearance.Options.UseFont = True
        Me.INDtxtAuthorizedBy.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDtxtAuthorizedBy.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDtxtAuthorizedBy.Size = New System.Drawing.Size(386, 28)
        Me.INDtxtAuthorizedBy.StyleController = Me.INDlyRoot
        Me.INDtxtAuthorizedBy.TabIndex = 7
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDtxtAuthorizedBy, 0)
        '
        'INDlyItemAuthorizedBy
        '
        Me.INDlyItemAuthorizedBy.Control = Me.INDtxtAuthorizedBy
        Me.INDlyItemAuthorizedBy.Location = New System.Drawing.Point(0, 240)
        Me.INDlyItemAuthorizedBy.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizedBy.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizedBy.Name = "INDlyItemAuthorizedBy"
        Me.INDlyItemAuthorizedBy.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizedBy.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuthorizedBy.Text = "Autorizado Por"
        Me.INDlyItemAuthorizedBy.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuthorizedBy.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAuthorizedBy.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAuthorizedBy.TextToControlDistance = 5
        Me.INDlyItemAuthorizedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDdteAuthorizationDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteAuthorizationDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteAuthorizationDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteAuthorizationDate, False)
        Me.INDdteAuthorizationDate.EditValue = Nothing
        Me.INDdteAuthorizationDate.EnterMoveNextControl = True
        Me.INDdteAuthorizationDate.Location = New System.Drawing.Point(-1236, 385)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteAuthorizationDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteAuthorizationDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteAuthorizationDate.Name = "INDdteAuthorizationDate"
        Me.INDdteAuthorizationDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteAuthorizationDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteAuthorizationDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteAuthorizationDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteAuthorizationDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteAuthorizationDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteAuthorizationDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteAuthorizationDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteAuthorizationDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteAuthorizationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteAuthorizationDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteAuthorizationDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteAuthorizationDate.StyleController = Me.INDlyRoot
        Me.INDdteAuthorizationDate.TabIndex = 8
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteAuthorizationDate, 0)
        '
        'INDlyItemAuthorizationDate
        '
        Me.INDlyItemAuthorizationDate.Control = Me.INDdteAuthorizationDate
        Me.INDlyItemAuthorizationDate.Location = New System.Drawing.Point(0, 300)
        Me.INDlyItemAuthorizationDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationDate.Name = "INDlyItemAuthorizationDate"
        Me.INDlyItemAuthorizationDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuthorizationDate.Text = "Fecha Autorización"
        Me.INDlyItemAuthorizationDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuthorizationDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAuthorizationDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAuthorizationDate.TextToControlDistance = 5
        Me.INDlyItemAuthorizationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'INDdteAuthorizationExpiredDate
        '
        Me.IndigoTextEdit1.SetApplyStyle(Me.INDdteAuthorizationExpiredDate, False)
        Me.IndigoDate1.SetCampoObligatorio(Me.INDdteAuthorizationExpiredDate, False)
        Me.IndigoTextEdit1.SetCampoObligatorio(Me.INDdteAuthorizationExpiredDate, False)
        Me.INDdteAuthorizationExpiredDate.EditValue = Nothing
        Me.INDdteAuthorizationExpiredDate.EnterMoveNextControl = True
        Me.INDdteAuthorizationExpiredDate.Location = New System.Drawing.Point(-1236, 445)
        Me.IndigoTextEdit1.SetMascara(Me.INDdteAuthorizationExpiredDate, Presentation.Controls.IndigoTextEdit.EMask.Ninguno)
        Me.IndigoDate1.SetMascaraDate(Me.INDdteAuthorizationExpiredDate, Presentation.Controls.IndigoDate.EMask.Fecha)
        Me.INDdteAuthorizationExpiredDate.Name = "INDdteAuthorizationExpiredDate"
        Me.INDdteAuthorizationExpiredDate.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.INDdteAuthorizationExpiredDate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteAuthorizationExpiredDate.Properties.Appearance.Options.UseBackColor = True
        Me.INDdteAuthorizationExpiredDate.Properties.Appearance.Options.UseFont = True
        Me.INDdteAuthorizationExpiredDate.Properties.AppearanceFocused.Font = New System.Drawing.Font("Segoe UI Light", 12.0!)
        Me.INDdteAuthorizationExpiredDate.Properties.AppearanceFocused.Options.UseFont = True
        Me.INDdteAuthorizationExpiredDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteAuthorizationExpiredDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.INDdteAuthorizationExpiredDate.Properties.Mask.EditMask = "dd \de MMMM \de yyyy"
        Me.INDdteAuthorizationExpiredDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
        Me.INDdteAuthorizationExpiredDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.INDdteAuthorizationExpiredDate.Size = New System.Drawing.Size(386, 28)
        Me.INDdteAuthorizationExpiredDate.StyleController = Me.INDlyRoot
        Me.INDdteAuthorizationExpiredDate.TabIndex = 9
        Me.IndigoTextEdit1.SetTamañoMinimoString(Me.INDdteAuthorizationExpiredDate, 0)
        '
        'INDlyItemAuthorizationExpiredDate
        '
        Me.INDlyItemAuthorizationExpiredDate.Control = Me.INDdteAuthorizationExpiredDate
        Me.INDlyItemAuthorizationExpiredDate.Location = New System.Drawing.Point(0, 360)
        Me.INDlyItemAuthorizationExpiredDate.MaxSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationExpiredDate.MinSize = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationExpiredDate.Name = "INDlyItemAuthorizationExpiredDate"
        Me.INDlyItemAuthorizationExpiredDate.Size = New System.Drawing.Size(390, 60)
        Me.INDlyItemAuthorizationExpiredDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
        Me.INDlyItemAuthorizationExpiredDate.Text = "Fecha Vencimiento Autorización"
        Me.INDlyItemAuthorizationExpiredDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize
        Me.INDlyItemAuthorizationExpiredDate.TextLocation = DevExpress.Utils.Locations.Top
        Me.INDlyItemAuthorizationExpiredDate.TextSize = New System.Drawing.Size(135, 21)
        Me.INDlyItemAuthorizationExpiredDate.TextToControlDistance = 5
        Me.INDlyItemAuthorizationExpiredDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '
        'FrmAddEvent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1465, 701)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddEvent"
        Me.Opacity = 1.0R
        Me.Tag = "2176"
        Me.Text = "Trámite"
        CType(Me.INDPanelControlBase, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDPanelControlBase.ResumeLayout(False)
        CType(Me.ToolBars, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolBars.ResumeLayout(False)
        CType(Me.LayoutControls, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleHealthAdministrator.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyRoot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.INDlyRoot.ResumeLayout(False)
        CType(Me.INDtxtRadicateNumberWebPage.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoInformationPatient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDslePatientNotificated.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPatientPhone.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPatientAddress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPatientDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoObservations.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtAuthorizationNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseAuthorizedQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDseRequestQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleStatus.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteSendDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteSendDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteRegistrationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteRegistrationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtURL.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCharge2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtReceivePerson.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteReceivedDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteReceivedDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleSendType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtRadicateNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtCharge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtContactPerson.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDteInitialTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtExtension.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtPhoneNumber.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDmemoInstructions.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDsleReportType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SearchLookUpEdit2View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Root, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPrincipalData, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemHealthAdministrator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemReportType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInstructions, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygGeneral, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRequestQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuthorizedQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuthorizationNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemObservations, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygNotification, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPatientDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPatientAddress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPatientPhone, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPatientNotificated, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInformationPatient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygCall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemPhoneNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemExtension, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemInitialTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemContactPerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCharge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygPhysicalSend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRadicateNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSendType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemReceivedDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemReceivePerson, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemCharge2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygWebPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemURL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRegistrationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemRadicateNumberWebPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlygEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemEmail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemSendDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSimpleButton1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoSearchLookUpControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoPopUpContainerEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLayoutControlGroup1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoLabelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.IndigoDate1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CtrNavigationControlPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDtxtAuthorizedBy.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuthorizedBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteAuthorizationDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteAuthorizationDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuthorizationDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteAuthorizationExpiredDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDdteAuthorizationExpiredDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.INDlyItemAuthorizationExpiredDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents IndigoTextEdit1 As IndigoTextEdit
    Friend WithEvents IndigoSimpleButton1 As IndigoSimpleButton
    Friend WithEvents IndigoSearchLookUpControl1 As IndigoSearchLookUpControl
    Friend WithEvents IndigoPopUpContainerEdit1 As IndigoPopUpContainerEdit
    Friend WithEvents IndigoLayoutControlGroup1 As IndigoLayoutControlGroup
    Friend WithEvents IndigoLabelControl1 As IndigoLabelControl
    Friend WithEvents IndigoGroupControl1 As IndigoGroupControl
    Friend WithEvents IndigoGridView1 As IndigoGridView
    Friend WithEvents IndigoGridControl1 As IndigoGridControl
    Friend WithEvents IndigoDate1 As IndigoDate
    Friend WithEvents INDlyRoot As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents CtrNavigationControlPanel1 As CtrNavigationControlPanel
    Friend WithEvents INDmemoInstructions As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDsleReportType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit2View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDsleHealthAdministrator As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents SearchLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygPrincipalData As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemHealthAdministrator As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemReportType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemInstructions As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtPhoneNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygCall As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemPhoneNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtExtension As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemExtension As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDteEndTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDteInitialTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents INDlyItemInitialTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemEndTime As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtCharge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtContactPerson As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemContactPerson As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCharge As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteReceivedDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDsleSendType As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDtxtRadicateNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygPhysicalSend As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemRadicateNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSendType As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemReceivedDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtCharge2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDtxtReceivePerson As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemReceivePerson As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemCharge2 As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtURL As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygWebPage As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemURL As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteRegistrationDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemRegistrationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteSendDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDtxtEmail As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygEmail As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemEmail As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDlyItemSendDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDsleStatus As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents INDlygGeneral As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemStatus As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseRequestQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemRequestQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDseAuthorizedQuantity As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents INDlyItemAuthorizedQuantity As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtAuthorizationNumber As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemAuthorizationNumber As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoObservations As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemObservations As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtPatientDescription As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlygNotification As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents INDlyItemPatientDescription As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtPatientAddress As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemPatientAddress As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtPatientPhone As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemPatientPhone As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDslePatientNotificated As DevExpress.XtraEditors.SearchLookUpEdit
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDlyItemPatientNotificated As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDmemoInformationPatient As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents INDlyItemInformationPatient As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents INDtxtRadicateNumberWebPage As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemRadicateNumberWebPage As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDtxtAuthorizedBy As DevExpress.XtraEditors.TextEdit
    Friend WithEvents INDlyItemAuthorizedBy As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteAuthorizationDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemAuthorizationDate As DevExpress.XtraLayout.LayoutControlItem
    Friend WithEvents INDdteAuthorizationExpiredDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents INDlyItemAuthorizationExpiredDate As DevExpress.XtraLayout.LayoutControlItem
End Class
