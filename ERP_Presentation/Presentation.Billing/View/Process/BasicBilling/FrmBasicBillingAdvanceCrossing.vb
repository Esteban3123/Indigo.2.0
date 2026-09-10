Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Treasury

Public Class FrmBasicBillingAdvanceCrossing
    Implements IAdvanceCrossing


#Region "Properties & Variables"
    ''' <summary>
    ''' private property to Currency Document
    ''' </summary>
    Private _documentCurrencyId As Integer

    ''' <summary>
    ''' Private property of document balance
    ''' </summary>
    Private _balanceDocument As Decimal

    ''' <summary>
    ''' Private property of MainAccount
    ''' </summary>
    Private _mainAccountId As Integer

    ''' <summary>
    ''' Thirparty id of source document
    ''' </summary>
    Private _thirdPartyId As Integer

    ''' <summary>
    ''' Currency Abbreviation of Source Document
    ''' </summary>
    Private _currencyAbbreviation As String

    ''' <summary>
    ''' Portfolio Advances Sum 
    ''' </summary>
    Private _sumPortfolioAdvanced As Decimal

    ''' <summary>
    ''' Presenter class to query datasources
    ''' </summary>
    Private _presenter As PAdvanceCrossing

    ''' <summary>
    ''' List of Portfolio Advances
    ''' </summary>
    Private _listPortfolioAdvance As List(Of Domain.Entities.PortfolioAdvance)

    ''' <summary>
    ''' Portfolio advance searchLookUpEdit
    ''' </summary>
    Private _portfolioAdvance As Domain.Entities.PortfolioAdvance

    ''' <summary>
    ''' Source Of document, like BasicBilling
    ''' </summary>
    Private _sourceDocument As eSourceDocument

    ''' <summary>
    ''' Const to set Portfolio Name to obtain the string resource
    ''' </summary>
    Private Const MODULE_NAME_PORTFOLIO As String = "Portfolio"

    ''' <summary>
    ''' Occurs when liqudate the Invoice
    ''' </summary>
    Public Event RunLiquidateBasicBilling(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Public property to get source currencyId 
    ''' </summary>
    Public ReadOnly Property GetDocumentCurrencyId As Integer Implements IAdvanceCrossing.DocumentCurrencyId
        Get
            Return _documentCurrencyId
        End Get
    End Property

    ''' <summary>
    ''' Public property to get source balance document
    ''' </summary>
    Public ReadOnly Property GetBalanceDocument As Decimal Implements IAdvanceCrossing.BalanceDocument
        Get
            Return _balanceDocument
        End Get
    End Property

    ''' <summary>
    ''' Public property to get source thirdpartyId
    ''' </summary>
    Public ReadOnly Property GetThirdPartyId As Integer Implements IAdvanceCrossing.ThirdPartyId
        Get
            Return _thirdPartyId
        End Get
    End Property

    Public ReadOnly Property GetCurrentBalance As Decimal Implements IAdvanceCrossing.GetCurrentBalance
        Get
            Return (Me.GetBalanceDocument - Me.SumPortfolioAdvanced)
        End Get
    End Property

    ''' <summary>
    ''' Public property to get sourceDocument enum
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetSourceDocument As eSourceDocument
        Get
            Return _sourceDocument
        End Get
    End Property

    ''' <summary>
    ''' Private Property to Set Source Currency Document
    ''' </summary>
    ''' <param name="abbreviation"></param>
    Private WriteOnly Property SetDocumentCurrency(abbreviation As String) As Integer
        Set(value As Integer)
            _documentCurrencyId = value
            _currencyAbbreviation = abbreviation
            Me.SetCurrencyUI(abbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set thirdParty
    ''' </summary>
    Private WriteOnly Property SetThirdPartyId As Integer
        Set(value As Integer)
            _thirdPartyId = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set Source Balance Document
    ''' </summary>
    Private WriteOnly Property SetBalanceDocument As Decimal
        Set(value As Decimal)
            _balanceDocument = value
            Me.CurrentBalanceLabel = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set Source MainAccountId
    ''' </summary>
    Private WriteOnly Property SetMainAccountId As Integer
        Set(value As Integer)
            _mainAccountId = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set Current Balance
    ''' </summary>
    Private WriteOnly Property CurrentBalanceLabel As Decimal
        Set(value As Decimal)
            Me.INDlbBalance.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.AbbreviationPopup)
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set Source Document Enum
    ''' </summary>
    Private WriteOnly Property SetSourceDocument As eSourceDocument
        Set(value As eSourceDocument)
            _sourceDocument = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Get Abbreviation Currency source document
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property AbbreviationPopup As String
        Get
            Return _currencyAbbreviation
        End Get
    End Property

    ''' <summary>
    ''' Private Property to Get/set Sum of portfolio advances added in the gridview
    ''' </summary>
    ''' <returns></returns>
    Private Property SumPortfolioAdvanced As Decimal
        Get
            Return _sumPortfolioAdvanced
        End Get
        Set(value As Decimal)
            _sumPortfolioAdvanced = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Get/set DataSource of PortfolioAdvance
    ''' </summary>
    ''' <returns></returns>
    Private Property DataSourceAdvanced As XPInstantFeedbackSource Implements IAdvanceCrossing.DataSourceAdvanced
        Get
            Return INDsleAdvance.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdvance.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Get/set Advance Id
    ''' </summary>
    ''' <returns></returns>
    Private Property AdvanceId As Integer?
        Get
            Return INDsleAdvance.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set Balance of selected Advance
    ''' </summary>
    Private WriteOnly Property BalanceAdvance As Decimal
        Set(value As Decimal)
            INDteBalance.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.AbbreviationPopup)
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Set TRM value of Selected Advance
    ''' </summary>
    Private WriteOnly Property TRMLabelValue As String
        Set(value As String)
            INDtxtTRM.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Get List of Portfolio Advances added in gridview
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListPortfolioAdvance As List(Of Domain.Entities.PortfolioAdvance) Implements IAdvanceCrossing.ListPortfolioAdvance
        Get
            Return _listPortfolioAdvance
        End Get
    End Property

    ''' <summary>
    ''' Private Property to Get/set Value to cross of selected advance
    ''' </summary>
    ''' <returns></returns>
    Private Property ValueToCross As Decimal
        Get
            Return INDspnCossingValue.EditValue
        End Get
        Set(value As Decimal)
            INDspnCossingValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Private Property to Get/set selected  Portfolio Advance
    ''' </summary>
    ''' <returns></returns>
    Private Property PortfolioAdvance As Domain.Entities.PortfolioAdvance
        Get
            Return Me._portfolioAdvance
        End Get
        Set(value As Domain.Entities.PortfolioAdvance)
            Me._portfolioAdvance = value
        End Set
    End Property

#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Class builder
    ''' </summary>
    ''' <param name="sourceDocument"></param>
    ''' <param name="documentCurrency"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="balanceDocument"></param>
    Public Sub New(sourceDocument As eSourceDocument,
                    documentCurrency As Domain.Entities.Currency,
                    thirdPartyId As Integer?,
                    balanceDocument As Decimal,
                    MainAccountId As Integer)

        ' This call is required by the designer.
        InitializeComponent()
        Try
            If documentCurrency Is Nothing OrElse documentCurrency.Id = 0 OrElse String.IsNullOrEmpty(documentCurrency.Abbreviation) Then
                Throw New ArgumentNullException(NameOf(documentCurrency), "Parámetro Obligatorio")
            End If

            If thirdPartyId Is Nothing OrElse thirdPartyId = 0 Then
                Throw New ArgumentNullException(NameOf(thirdPartyId), "Parámetro Obligatorio")
            End If

            If balanceDocument = 0 Then
                Throw New ArgumentNullException(NameOf(balanceDocument), "Sin saldo para cruzar")
            End If

            If MainAccountId = 0 Then
                Throw New ArgumentNullException(NameOf(balanceDocument), "Parámetro Obligatorio")
            End If

            Me.SetSourceDocument = sourceDocument
            Me.SetDocumentCurrency(documentCurrency.Abbreviation) = documentCurrency.Id
            Me.SetThirdPartyId = thirdPartyId
            Me.SetBalanceDocument = balanceDocument
            Me.SetMainAccountId = MainAccountId

        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Me.Close()
        End Try
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _documentCurrencyId = Nothing
        _balanceDocument = Nothing
        _thirdPartyId = Nothing
        _listPortfolioAdvance = Nothing
        _portfolioAdvance = Nothing
        _sumPortfolioAdvanced = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmBasicBillingAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmBasicBillingAdvanceCrossing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._presenter = New PAdvanceCrossing(Me)
        Me.CleanPopupControls()
        Me.SumCrossValue()
        Me.IndigoGridView1.SetListAcction(INDgvAdvance, {eAcciones.Remove}.ToList)
    End Sub

#End Region

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmBasicBillingAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmBasicBillingAdvanceCrossing_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.BarraBotones.Enabled Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the Closed event of the INDpceAddAdvancePortfolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddAdvancePortfolio_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceAddAdvancePortfolio.Closed
        INDsbAcept.Enabled = True
        INDsbAcept.Focus()
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' event when the edit value in Searchlookup changes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvance.EditValueChanged
        If Me.AdvanceId Is Nothing Then
            Exit Sub
        End If
        Await Me.PopupLoadAdvance(Me.AdvanceId)
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAccountAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAccountAccounting_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdvance.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using frm As New FrmCashReceipts
                frm.subCashReceipts = True
                frm.IdThirdParty = _thirdPartyId
                frm.SourceDocument = eSourceDocument.BasicBilling
                frm.IdMainAccount = _mainAccountId
                frm.IdThirdPartyMainAccount = _mainAccountId
                frm.CashDefaultValue = _balanceDocument
                frm.ValueProductInvoice = _balanceDocument
                frm.ParentCurrencyId(Me._currencyAbbreviation) = Me._documentCurrencyId
                Dim transparent As New FrmTransparent(frm, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' event to add the advance to gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbAddAdvance_Click(sender As Object, e As EventArgs) Handles INDsbAddAdvance.Click
        Try
            If Me.ListPortfolioAdvance Is Nothing Then
                _listPortfolioAdvance = New List(Of Domain.Entities.PortfolioAdvance)
            End If

            If PortfolioAdvance Is Nothing Then
                ShowMessage(EeventViewerImages.Advertencia) = "Es necesario establecer al menos un anticipo."
                Exit Sub
            End If

            If Me.ValueToCross <= 0 Then
                ShowMessage(EeventViewerImages.Advertencia) = "Es necesario establecer un valor a cruzar"
                Exit Sub
            End If

            Dim balance = If(PortfolioAdvance.BalanceInCurrencyConverted Is Nothing, PortfolioAdvance.Balance, PortfolioAdvance.BalanceInCurrencyConverted.Value)

            If Me.ValueToCross > balance Then
                ShowMessage(EeventViewerImages.Advertencia) = "El valor a cruzar no puede ser mayor al saldo del anticipo"
                Exit Sub
            End If

            If (Me.ValueToCross + Me.SumPortfolioAdvanced) > Me.GetBalanceDocument Then
                ShowMessage(EeventViewerImages.Advertencia) = "El valor a cruzar no puede ser mayor al saldo de la factura"
                Exit Sub
            End If

            If Me.ListPortfolioAdvance.Exists(Function(x) x.Id = Me.PortfolioAdvance.Id) Then
                ShowMessage(EeventViewerImages.Advertencia) = "El anticipo ya se encuentra añadido"
                Exit Sub
            End If

            Me.PortfolioAdvance.CrossingValue = Me.ValueToCross

            _listPortfolioAdvance.Add(Me.PortfolioAdvance)
        Finally
            Me.SumCrossValue()
            Me.INDgcPortfolioAdvance.DataSource = _listPortfolioAdvance
            Me.INDgcPortfolioAdvance.RefreshDataSource()
            Me.CleanPopupControls()
        End Try
    End Sub

    ''' <summary>
    ''' event to make the online crossing process
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click

        If Not _listPortfolioAdvance?.Any() Then
            If MessageIndigo.Show("¿Desea seguir sin agregar anticipos? Deberá crear uno nuevo", MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                RaiseEvent RunLiquidateBasicBilling(Me, Nothing)
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
                Exit Sub
            Else
                Exit Sub
            End If
        End If

        If Me.SumPortfolioAdvanced > Me.GetBalanceDocument Then
            ShowMessage(EeventViewerImages.Advertencia) = "El total de los anticipos excede el saldo de la factura."
            Exit Sub
        End If

        Dim resultDialog = MessageIndigo.Show(ResourceManager.GetString("QuestionToConfirmTheCross", MODULE_NAME_PORTFOLIO), MessageType.Question, Me.Text, Botones.SiNo)

        If resultDialog <> System.Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Dim eventAdvance As New RunCrossingProcessEventArgs()
        Dim crossing As New PortfolioAdvanceInvoicePayment
        With crossing
            .CurrencyId = GetDocumentCurrencyId
            .EntityName = GetSourceDocument.ToString
            .ListPortfolioAdvance = ListPortfolioAdvance
            .TotalCrossingValue = Me.SumPortfolioAdvanced
        End With

        eventAdvance.ListPortfolioAdvanceInvoicePaymentAs = crossing
        RaiseEvent RunLiquidateBasicBilling(Me, eventAdvance)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
#End Region

#Region "KeyDown"

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdvance_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAdvance.QueryPopUp
        If Me.DataSourceAdvanced Is Nothing Then
            Me._presenter.DataSourceAdvanced(Me.GetThirdPartyId)
        End If
    End Sub

#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim pFolioAdvance = CType(INDgvAdvance.GetFocusedRow(), Domain.Entities.PortfolioAdvance)
            _listPortfolioAdvance.Remove(pFolioAdvance)
            INDgcPortfolioAdvance.DataSource = _listPortfolioAdvance
            INDgcPortfolioAdvance.RefreshDataSource()
            Me.SumCrossValue()
        End If
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' method to set numeric culture in this popup
    ''' </summary>
    ''' <param name="abbreviation"></param>
    Private Sub SetCurrencyUI(abbreviation As String)

        If String.IsNullOrEmpty(abbreviation) Then
            ShowMessage(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = abbreviation.GetNumberFormat

        INDspnCossingValue.Properties.Mask.Culture = _culture
        INDteBalance.Properties.Mask.Culture = _culture
        ColCrossValue = Window.Utils.FormatGrid(ColCrossValue, abbreviation)
    End Sub

    ''' <summary>
    ''' Async Function to Load the portfolio advance
    ''' </summary>
    ''' <param name="advanceId"></param>
    ''' <returns></returns>
    Private Async Function PopupLoadAdvance(advanceId As Integer?) As Task
        Try
            Me.AsyncLoaderPopUp(True)
            If advanceId Is Nothing Then
                Return
            End If

            Dim advanceXpo = INDsleAdvance.GetFocusedObject(Of PortfolioRepository.Portfolio_PortfolioAdvance)

            Dim advance = Me.DTOAdvance(advanceXpo)

            If advance Is Nothing Then
                Return
            End If

            Me.HideControlTRM(GetDocumentCurrencyId = advance.CurrencyId)
            Me.BalanceAdvance = advance.Balance

            If _balanceDocument < advance.Balance Then
                ValueToCross = _balanceDocument
            Else
                ValueToCross = advance.Balance
            End If

            If GetDocumentCurrencyId <> advance.CurrencyId Then
                Using model As New MBasicBilling("")
                    Dim result = Await model.GetBalanceAdvanceByCurrency(advance.Id, GetDocumentCurrencyId)
                    If result Is Nothing OrElse Not result.StateResult Then
                        ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                        CleanPopupControls()
                        Return
                    End If

                    advance = result.ObjectEmbbeded
                    Me.TRMLabelValue = Utils.VisibleTRM(advance.BalanceInCurrencyConverted.TRMValue)
                    Me.BalanceAdvance = advance.BalanceInCurrencyConverted.Value
                End Using
            End If

            Me.PortfolioAdvance = advance
        Finally
            Me.AsyncLoaderPopUp(False)
        End Try
    End Function

    ''' <summary>
    ''' method to clean portfolioAdvance Popup
    ''' </summary>
    Private Sub CleanPopupControls()
        Me.AdvanceId = Nothing
        Me.TRMLabelValue = String.Empty
        Me.INDlciTRM.HideControl()
        Me.BalanceAdvance = 0D
        Me.ValueToCross = 0D
        Me.PortfolioAdvance = Nothing
    End Sub

    ''' <summary>
    ''' DTO function to convert portfolioAdvanceXpo Entity in PortfolioAdvance entity
    ''' </summary>
    ''' <param name="advance"></param>
    ''' <returns></returns>
    Private Function DTOAdvance(advance As PortfolioRepository.Portfolio_PortfolioAdvance) As Domain.Entities.PortfolioAdvance
        Dim portfolioAdvance = New Domain.Entities.PortfolioAdvance

        With portfolioAdvance
            .Id = advance.Id
            .AdmissionNumber = advance.AdmissionNumber
            .Code = advance.Code
            .DocumentDate = advance.DocumentDate
            .CurrencyId = advance.CurrencyId
            .CurrencyAbbreviation = advance.CurrencyAbbreviation
            .Value = advance.Value
            .Balance = advance.Balance
            .CrossingValue = 0
        End With

        Return portfolioAdvance
    End Function

    ''' <summary>
    ''' function to sum every balance in the gridview
    ''' </summary>
    Private Sub SumCrossValue()
        If _listPortfolioAdvance Is Nothing Then
            _listPortfolioAdvance = New List(Of PortfolioAdvance)
        End If

        Me.SumPortfolioAdvanced = Me._listPortfolioAdvance.Sum(Function(x) x.CrossingValue)

        If Me.GetBalanceDocument <> Me.SumPortfolioAdvanced Then
            INDLCBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(0, Byte), Integer))
        Else
            INDLCBalance.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
        End If

        Me.CurrentBalanceLabel = Me.GetBalanceDocument - Me.SumPortfolioAdvanced
    End Sub

    ''' <summary>
    ''' method to block the popup when the popup is in async mode
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub AsyncLoaderPopUp(value As Boolean)
        If value Then
            Me.Cursor = BaseClass.ChangeCursorIndigo()
        Else
            Me.Cursor = DefaultCursor()
        End If
        Me.INDPanelControlBase.Enabled = Not value
    End Sub

    ''' <summary>
    ''' method to hide or show the TRM control, maybe is obsolete
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub HideControlTRM(value As Boolean)

        If Me.INDlciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never AndAlso value Then
            Exit Sub
        End If

        If Me.INDlciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Not value Then
            Exit Sub
        End If

        Me.INDlciTRM.HideControl(value)
    End Sub
#End Region

#Region "BarButton"
    ''' <summary>
    ''' event to load bar buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.ToolBar.Visible = False
    End Sub


    ''' <summary>
    ''' show the messages
    ''' </summary>
    ''' <param name="Icon">Icono del tipo de mensaje</param>
    ''' <value>Mensaje a mostrar</value>
    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

End Class

