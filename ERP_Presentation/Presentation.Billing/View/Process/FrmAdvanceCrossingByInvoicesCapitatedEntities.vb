'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 12-08-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.Globalization
Imports System.Text
Imports DevExpress
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraLayout
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Extension
Imports Presentation.Portfolio.MVP

#End Region

Public Class FrmAdvanceCrossingByInvoicesCapitatedEntities

#Region "Properties & Variables"

    ''' <summary>
    ''' cursor indigo
    ''' </summary>
    Private _indigoCursor As System.Windows.Forms.Cursor

    ''' <summary>
    ''' The _total Entity
    ''' </summary>
    Public _TotalEntity As Decimal

    ''' <summary>
    ''' The _total Portfolio Advance
    ''' </summary>
    Private _totalPortolioAdvance As Decimal

    ''' <summary>
    ''' The _total Invoice
    ''' </summary>
    Private _totalInvoice As Decimal

    ''' <summary>
    ''' Asigna el valor del tercero (entidad)
    ''' </summary>
    ''' <value>
    ''' The third party identifier parent.
    ''' </value>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Asigna la moneda
    ''' </summary>
    Property CurrencyId As Integer

    ''' <summary>
    ''' propiedad que asigna y guarda el Id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyAbbreviation As String

    ''' <summary>
    ''' Moneda del anticipo
    ''' </summary>
    Private Property CurrencyAdvanceId As Integer?

    ''' <summary>
    ''' TRM usado para conversion de los anticipos y su saldo
    ''' </summary>
    ''' <returns></returns>
    Private Property _TRMValue As Decimal? = 1

    ''' <summary>
    ''' valor anterior del cruce por si la validacion sale falsa
    ''' </summary>
    Private _previusValue As Decimal

    ''' <summary>
    ''' Occurs when [run include portfolio advance].
    ''' </summary>
    Public Event RunIncludePortfolioAdvance(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Redondeo
    ''' </summary>
    Private _decimals As Integer = 2

    ''' <summary>
    ''' Lista de la tasa de cambio
    ''' </summary>
    Private _listTRM As List(Of TRM)

    ''' <summary>
    ''' bandera que identifica cuando se esta ejecutando el .load inicial
    ''' </summary>
    Private _flagInitLoad As Boolean = False

    ''' <summary>
    ''' Obtiene el id del anticipo
    ''' </summary>
    Private Property PortfolioAdvanceId As Integer
        Get
            Return INDsleAdvance.EditValue
        End Get
        Set(value As Integer)
            INDsleAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the list portfolio advance.
    ''' </summary>
    Property ListPortfolioAdvance As List(Of PortfolioAdvance)
        Get
            Return CType(INDgcPortfolioAdvance.DataSource, List(Of PortfolioAdvance))
        End Get
        Set(value As List(Of PortfolioAdvance))
            INDgcPortfolioAdvance.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el saldo de la factura
    ''' </summary>
    ''' <value>
    ''' The total patient.
    ''' </value>
    Public Property TotalInvoice As Decimal
        Get
            Return _totalInvoice
        End Get
        Set(value As Decimal)
            _totalInvoice = value
            INDlblTotalInvoice.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Establece el total de la entidad
    ''' </summary>
    ''' <value>
    ''' The total patient.
    ''' </value>
    Public Property TotalEntity As Decimal
        Get
            Return _TotalEntity
        End Get
        Set(value As Decimal)
            _TotalEntity = value
            INDlblTotalEntity.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Establece el total de los anticipos
    ''' </summary>
    ''' <value>
    ''' The total entity.
    ''' </value>
    Public Property TotalPortfolioAdvance As Decimal
        Get
            Return _totalPortolioAdvance
        End Get
        Set(value As Decimal)
            _totalPortolioAdvance = value
            INDlblTotalPortfolioAdvance.Text = Infrastructure.CrossCutting.Base.Utils.GetMoneyWithISO4217(value, Me.CurrencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' propiedad que Obtiene o establece el saldo del anticipo
    ''' </summary>
    ''' <returns></returns>
    Public Property BalanceAdvance As Decimal
        Get
            Return CType(INDteBalance.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDteBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el valor a cruzar
    ''' </summary>
    ''' <returns></returns>
    Public Property CrossingValue As Decimal
        Get
            Return CType(INDspnCossingValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDspnCossingValue.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Evento que se dispara al dar click en el boton que carga el form recibo de caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdvance_ButtonClick(sender As Object, e As XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdvance.ButtonClick
        If e.Button.Kind = XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("635", Nothing, True)
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me._indigoCursor = Nothing
        Me._totalPortolioAdvance = Nothing
        Me._TotalEntity = Nothing
        Me._totalInvoice = Nothing
        Me._previusValue = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdvanceCrossing_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._indigoCursor = ChangeCursorIndigo()
        IndigoGridView1.SetListAcction(INDgvAdvance, {eAcciones.Remove}.ToList)
        For Each col As GridColumn In INDgvAdvance.Columns
            If col.Name.Equals("colActions") Then
                col.Width = 150
            End If
        Next

        SetCurrencyFormatUI(CurrencyAbbreviation)
        LoadControls()
    End Sub

#End Region

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdvanceCrossing_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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

#Region "Leave"

    ''' <summary>
    ''' Handles the Leave event of the INDspnCossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDspnCossingValue_Leave(sender As Object, e As EventArgs) Handles INDspnCossingValue.Leave
        If INDspnCossingValue.EditValue IsNot Nothing AndAlso INDteBalance.EditValue IsNot Nothing Then
            If CrossingValue > INDspnCossingValue.Properties.MaxValue Then
                ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageCrossingValue2", GetType(CtrFolio).Name)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDrpCrossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDrpCrossingValue_Leave(sender As Object, e As EventArgs) Handles INDrpCrossingValue.Leave
        Dim _portfolioAdvance As PortfolioAdvance = CType(INDgvAdvance.GetFocusedRow(), PortfolioAdvance)
        If _portfolioAdvance IsNot Nothing Then
            If Not ValidateCrossingValue(_portfolioAdvance) Then
                _portfolioAdvance.CrossingValue = _previusValue
                INDgcPortfolioAdvance.RefreshDataSource()
            End If
            CrossingTotalRefresh()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvance.EditValueChanged
        BalanceAdvance = 0
        Me.INDlciTRM.HideControl()
        If INDsleAdvance.EditValue IsNot Nothing Then
            Dim portfolioAdv As Object
            portfolioAdv = TryCast(INDgvAdvancePortfolio.GetFocusedRow(), PortfolioRepository.Portfolio_PortfolioAdvance)
            Me.CurrencyAdvanceId = portfolioAdv?.CurrencyId
            Dim currencyAdvance As String = portfolioAdv?.CurrencyAbbreviation

            If Me.CurrencyId <> Me.CurrencyAdvanceId Then
                INDlciBalanceAdvance.Text = $"Saldo (expresado en {Me.CurrencyAbbreviation})"
                Me.INDlciTRM.HideControl(False)
                Await Me.loadListTRM(Me.CurrencyId, Me.CurrencyAdvanceId)


                If Me._listTRM Is Nothing OrElse Not Me._listTRM?.Any(Function(a) a.CurrencyId = CurrencyId _
                                                               AndAlso a.OfficialCurrencyId = Me.CurrencyAdvanceId _
                                                               AndAlso a.MeasurementDate = GetDateServer().Date) Then
                    CleanAdvancedControl()
                    ShowMessage(EeventViewerImages.Advertencia) = ResourceManager.GetString("TRMEmpty")
                    Exit Sub
                End If

                Me._TRMValue = Me._listTRM.FirstOrDefault(Function(a) a.CurrencyId = CurrencyId _
                                                               AndAlso a.OfficialCurrencyId = Me.CurrencyAdvanceId _
                                                               AndAlso a.MeasurementDate = GetDateServer().Date)?.Value

                BalanceAdvance = Math.Round(portfolioAdv.Balance / Me._TRMValue, Me._decimals)
                CrossingValue = IIf(BalanceAdvance > TotalInvoice, TotalInvoice, BalanceAdvance)
            Else
                Me._TRMValue = 1
                Me.INDlciBalanceAdvance.Text = $"Saldo"
                BalanceAdvance = portfolioAdv?.Balance
                CrossingValue = IIf(BalanceAdvance > TotalInvoice, TotalInvoice, BalanceAdvance)
            End If
        End If

        INDspnCossingValue.Properties.MaxValue = CrossingValue
        Dim _culture = Me.GetCultuteInfo(Me.CurrencyAbbreviation)
        Me.INDtxtTRM.Properties.NullText = $"{Me.CurrencyAbbreviation} - TRM {Utils.VisibleTRM(Me._TRMValue)}"
        INDspnCossingValue.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the INDsbAddAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddAdvance_Click(sender As Object, e As EventArgs) Handles INDsbAddAdvance.Click
        If PortfolioAdvanceId <> 0 Then
            If ListPortfolioAdvance Is Nothing Then
                ListPortfolioAdvance = New List(Of PortfolioAdvance)()
            End If

            Dim portfolioAdv = TryCast(INDgvAdvancePortfolio.GetFocusedRow(), PortfolioRepository.Portfolio_PortfolioAdvance)
            If Not AddAdvance(portfolioAdv, CrossingValue) Then
                Exit Sub
            End If

            CleanAdvancedControl()
            INDsleAdvance.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        Dim resultDialog As System.Windows.Forms.DialogResult = System.Windows.Forms.DialogResult.Yes
        If resultDialog = System.Windows.Forms.DialogResult.Yes Then
            INDsbAcept.Enabled = False

            Dim ipa As New RunIncludePortfolioAdvanceArgs()
            ipa.ListPortfolioAdvance = ListPortfolioAdvance
            RaiseEvent RunIncludePortfolioAdvance(Me, ipa)
            DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the FrmAdvanceCrossing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAdvanceCrossing_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddAdvancePortfolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddAdvancePortfolio_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddAdvancePortfolio.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceAddAdvancePortfolio.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDrpCrossingValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDrpCrossingValue_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDrpCrossingValue.KeyDown
        _previusValue = CType(INDgvAdvance.GetFocusedRow(), PortfolioAdvance).CrossingValue
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleAdvance control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdvance_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAdvance.QueryPopUp
        If INDsleAdvance.Properties.DataSource Is Nothing Then
            LoadAdvance()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Popup event of the INDpceAddAdvancePortfolio control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddAdvancePortfolio_Popup(sender As Object, e As EventArgs) Handles INDpceAddAdvancePortfolio.Popup
        INDsbAcept.Enabled = False
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
            Dim pFolioAdvance = CType(INDgvAdvance.GetFocusedRow(), PortfolioAdvance)
            ListPortfolioAdvance.Remove(pFolioAdvance)
            INDgcPortfolioAdvance.RefreshDataSource()
            CrossingTotalRefresh()
        End If
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Se asegura de precargar los anticipos que cumplan con las condiciones
    ''' </summary>
    Private Sub LoadControls()
        Try
            INDgcPortfolioAdvance.RefreshDataSource()
            CrossingTotalRefresh()
            INDteBalance.Properties.ReadOnly = True
            INDpceAddAdvancePortfolio.Focus()

            AutoLoadAdvances()
        Catch ex As Exception
            ShowMessage(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Validates the advance.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateAdvance(porfAdv As PortfolioRepository.Portfolio_PortfolioAdvance, crossingValue As Decimal) As Boolean
        Dim errorList As New StringBuilder()

        If ListPortfolioAdvance.FindAll(Function(o) o.Id = porfAdv.Id).Count > 0 Then
            errorList.AppendLine(ResourceManager.GetString("AdvanceExist", GetType(CtrFolio).Name))
        End If

        If (ListPortfolioAdvance.Sum(Function(o) o.CrossingValue) + crossingValue) > TotalEntity Then
            errorList.AppendLine("La suma de los anticipos no pueden sobrepasar el valor de la entidad")
        End If

        If crossingValue = 0 Then
            errorList.AppendLine(ResourceManager.GetString("MessageCrossingValue3", GetType(CtrFolio).Name))
        End If

        If errorList.Length > 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Loads the advance.
    ''' </summary>
    Private Sub LoadAdvance()
        If ThirdPartyId IsNot Nothing Then
            INDsleAdvance.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.GetPortfolioAdvanceByThirdPartyIdForFixedAmountInvoice(ThirdPartyId)
        End If
    End Sub

    ''' <summary>
    ''' Muestra un mensaje en el frontal
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

    ''' <summary>
    ''' Calcula el valor total a cruzar
    ''' </summary>
    Private Sub CrossingTotalRefresh()
        If ListPortfolioAdvance IsNot Nothing AndAlso ListPortfolioAdvance.Count > 0 Then
            TotalPortfolioAdvance = Math.Round(ListPortfolioAdvance.Sum(Function(x) x.CrossingValue), 2, MidpointRounding.AwayFromZero)
            TotalInvoice = Math.Round(TotalEntity - TotalPortfolioAdvance, 2, MidpointRounding.AwayFromZero)
        Else
            TotalPortfolioAdvance = Decimal.Zero
            TotalInvoice = TotalEntity
        End If
    End Sub

    ''' <summary>
    ''' Valida el valor a cruzar
    ''' </summary>
    ''' <param name="portfolioAdvance">The portfolio advance.</param>
    ''' <returns></returns>
    Private Function ValidateCrossingValue(portfolioAdvance As PortfolioAdvance) As Boolean
        Dim errorList As New StringBuilder()
        If (ListPortfolioAdvance.Sum(Function(o) o.CrossingValue)) > TotalInvoice Then
            errorList.AppendLine(ResourceManager.GetString("MessageValidatePatientValue", GetType(CtrFolio).Name))
        End If

        If portfolioAdvance.Balance < portfolioAdvance.CrossingValue Then
            errorList.AppendLine(ResourceManager.GetString("MessageCrossingValue2", GetType(CtrFolio).Name))
        End If
        If errorList.Length > 0 Then
            ShowMessage(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Verifica que si hay anticipos con el mismo tercero para que los postule en la rejilla
    ''' </summary>
    Private Sub AutoLoadAdvances()
        Dim listPortfolioAdv = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PortfolioService.GetPortfolioAdvanceByThirdPartyIdForFixedAmountInvoice(ThirdPartyId)

        If listPortfolioAdv?.ToEntityList(Of Object)?.Any() Then
            If ListPortfolioAdvance Is Nothing Then
                ListPortfolioAdvance = New List(Of PortfolioAdvance)()
            End If

            For Each portfolioAdv In listPortfolioAdv?.
                                ToEntityList(Of PortfolioRepository.Portfolio_PortfolioAdvance)?.FindAll(Function(x) x.CurrencyId = Me.CurrencyId).
                                 OrderByDescending(Function(x) x.Balance)

                If TotalEntity > 0 Then
                    Dim crossingValue As Decimal = IIf(portfolioAdv.Balance > TotalEntity, TotalEntity, portfolioAdv.Balance)

                    If ListPortfolioAdvance.Count > 0 Then

                        If (ListPortfolioAdvance.Sum(Function(o) o.CrossingValue) + crossingValue) > TotalEntity Then
                            crossingValue = TotalEntity - ListPortfolioAdvance.Sum(Function(o) o.CrossingValue)
                        End If
                    End If

                    AddAdvance(portfolioAdv, crossingValue)
                End If
            Next
        End If
    End Sub

    Private Function AddAdvance(portfolioAdv As PortfolioRepository.Portfolio_PortfolioAdvance, crossingValue As Decimal) As Boolean

        If Not ValidateAdvance(portfolioAdv, crossingValue) Then
            Return False
        End If

        If ListPortfolioAdvance Is Nothing Then
            ListPortfolioAdvance = New List(Of PortfolioAdvance)()
        End If

        ListPortfolioAdvance.Add(New PortfolioAdvance With
        {
            .Id = portfolioAdv.Id,
            .Code = portfolioAdv.Code,
            .AdmissionNumber = portfolioAdv.AdmissionNumber,
            .ThirdPartyId = portfolioAdv.ThirdPartyId.Id,
            .MainAccountId = portfolioAdv.MainAccountId.Id,
            .CostCenterId = If(portfolioAdv.CostCenterId IsNot Nothing, portfolioAdv.CostCenterId.Id, Nothing),
            .Value = portfolioAdv.Value / Me._TRMValue,
            .Balance = portfolioAdv.Balance / Me._TRMValue,
            .CrossingValue = crossingValue,
            .DocumentDate = portfolioAdv.DocumentDate,
            .CurrencyAbbreviation = Me.CurrencyAbbreviation,
            .PaymentMethodsType = If(portfolioAdv.CashReceiptId IsNot Nothing, portfolioAdv.CashReceiptId.PaymentMethod, 0),
            .PaymentMethodName = portfolioAdv.PaymentMethodName,
            .PortfolioAdvanceType = 1,
            .PortfolioAdvanceTypeDescription = ""
        })

        INDgcPortfolioAdvance.RefreshDataSource()
        CrossingTotalRefresh()
        Return True
    End Function

    ''' <summary>
    ''' funcion para establecer el formato de la moneda seleccionada
    ''' </summary>
    ''' <param name="abbreviation"></param>
    Private Sub SetCurrencyFormatUI(abbreviation As String)
        If String.IsNullOrEmpty(abbreviation) Then
            Exit Sub
        End If

        Me._CurrencyAbbreviation = abbreviation
        Dim _culture = Me.GetCultuteInfo(abbreviation)
        Me.INDteBalance.Properties.Mask.Culture = _culture
        Me.INDspnCossingValue.Properties.Mask.Culture = _culture

        ColCrossValue = Window.Utils.FormatGrid(ColCrossValue, abbreviation)
        ColBalance = Window.Utils.FormatGrid(ColBalance, abbreviation)
        ColValue = Window.Utils.FormatGrid(ColValue, abbreviation)
    End Sub

    ''' <summary>
    ''' limpia los controles del popup de anticipos cuando se agrega o cuando se cambia de moneda
    ''' </summary>
    Private Sub CleanAdvancedControl()
        INDsleAdvance.EditValue = Nothing
        BalanceAdvance = 0
        CrossingValue = 0
        INDlciTRM.HideControl()
        Me.INDlciBalanceAdvance.Text = $"Saldo"
    End Sub

    ''' <summary>
    ''' Carga una lista global del TRM
    ''' </summary>
    ''' <param name="fromCurrency">Moneda de la transacción</param>
    ''' <param name="toCurrency">Moneda a convertir</param>
    Private Async Function loadListTRM(toCurrency As Integer?, fromCurrency As Integer?) As Task
        Me._listTRM = If(Me._listTRM Is Nothing, New List(Of TRM), Me._listTRM)
        'Se busca que el TRM de la moneda a convertir no este en la lista
        If Not Me._listTRM?.Any(Function(a) a.CurrencyId = toCurrency AndAlso a.OfficialCurrencyId = fromCurrency AndAlso a.MeasurementDate = GetDateServer().Date) Then
            Using model As New MPortfolioTransfers(Me.Tag)
                Dim result = Await model.GetTRMbyCurrencyId(toCurrency, fromCurrency, Nothing, NameOf(Invoice))
                If Not result?.StateResult Then
                    ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                    Exit Function
                End If
                Me._listTRM.Add(result?.ObjectEmbbeded)
            End Using
        End If
    End Function

    Private Function GetCultuteInfo(CurrencyAbbreviation As String) As CultureInfo
        Dim culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        Return culture
    End Function

#End Region

#Region "BarButton"
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.ToolBar.Visible = False
    End Sub

#End Region

End Class