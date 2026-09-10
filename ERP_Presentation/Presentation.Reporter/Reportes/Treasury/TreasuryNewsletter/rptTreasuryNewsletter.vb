#Region "Librerias Improtadas"
Imports System.Drawing.Printing
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
#End Region

Public Class rptTreasuryNewsletter
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private PreviousBalanceCash As Decimal
    Private PreviousBalanceBank As Decimal

    ''' <summary>
    ''' obtiene la informacion de la moneda seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property Currency As Currency

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#Region "Public Methods"

    ''' <summary>
    ''' se ejecuta para cargar los movimientos en un rango de fechas por cuentas bancarias
    ''' </summary>
    ''' <param name="INDTypeVoucher">1 = ingresos, 2 = egresos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FillListTreasuryNewsletterEntityBank(ByVal INDTypeVoucher As Integer, Optional ByVal INDCurrency As Object = Nothing) As Task(Of List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount))
        If INDCurrency Is Nothing Then
            INDCurrency = IndigoSessionValues.CurrencyISO4217
        End If
        Return Task.FromResult(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollectionTreasuryNewsletterEntityBank(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(6), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(7), ParametrosReporte(8), INDTypeVoucher, ParametrosReporte(9), INDCurrency.ToString()))
    End Function

    ''' <summary>
    ''' se ejecuta para cargar los movimientos en un rango de fechas por cajas
    ''' </summary>
    ''' <param name="INDTypeVoucher">1= ingresos, 2= cajas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FillListTreasuryNewsletterEntityCash(ByVal INDTypeVoucher As Integer, Optional ByVal INDCurrency As Object = Nothing) As Task(Of List(Of TreasuryVReportTreasuryNewsletterCash))
        If INDCurrency Is Nothing Then
            INDCurrency = IndigoSessionValues.CurrencyISO4217
        End If
        Return Task.FromResult(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollectionTreasuryNewsletterEntityCash(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(6), ParametrosReporte(4), ParametrosReporte(5), ParametrosReporte(7), ParametrosReporte(8), INDTypeVoucher, ParametrosReporte(9), INDCurrency.ToString()))
    End Function

    ''' <summary>
    ''' se ejecuta para cargar el datasource cuando seleccionan reporte resumido
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FillListSummaryNewsletter() As Task(Of List(Of TreasuryVReportTreasuryNewsletterSummary))
        Return Task.FromResult(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.ListCollectionSummaryNewsletter(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(6), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), ParametrosReporte(7), ParametrosReporte(8), ParametrosReporte(9)))
    End Function

    ''' <summary>
    ''' se ejecuta para consultar el saldo anterior por cajas o cuentas bancarias
    ''' </summary>
    ''' <param name="INDPreviousBalanceType">1 = sacar el saldo por cuentas bancarias, 2 = sacar el saldo anterior por cajas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValuePreviousBalance(ByVal INDPreviousBalanceType As Integer) As Task(Of Decimal)
        Return Task.FromResult(XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.ValuePreviousBalance(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), INDPreviousBalanceType, ParametrosReporte(6)))
    End Function

#End Region

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Async Sub rptTreasuryNewsletter_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        'Cargar los valores del titulo
        INDLblDate.Text = "Informe comprendido entre el " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy - HH:mm") & " " & CDate(ParametrosReporte(1)).ToString(" Al dd De MMMM Del yyyy - HH:mm")
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        'Titulo para reporte si tiene parametros de Cuentas Bancarias
        If ParametrosReporte(2) <> "" Then
            INDLblNumberAccounts.Visible = True
            INDLblNumberAccounts.Text = "Informe comprendido desde la cuenta " & ParametrosReporte(2) & " a la cuenta " & ParametrosReporte(3)
        End If

        'Titulo para reporte si tiene parametros de Cajas
        If ParametrosReporte(4) <> "" Then
            INDLblCodeCash.Visible = True
            INDLblCodeCash.Text = "Informe comprendido desde la caja " & ParametrosReporte(4) & " a la caja " & ParametrosReporte(5)
        End If

        'cargar subreport segun el tipo de reporte
        If ParametrosReporte(7) = 1 And ParametrosReporte(8) = 1 Then
            INDLblTitle.Text = "BOLETIN DE TESORERIA"
            'SI en el filtro van cuentas bancarias
            If ParametrosReporte(2) <> "" Then
                GhBanks.Visible = True
                GFBanks.Visible = True
            End If

            'si en el filtro van cajas
            If ParametrosReporte(4) <> "" Then
                GfHeaderCash.Visible = True
                INDLblDescriptionDebitCash.Visible = True
                INDLblValueDebitCash.Visible = True
                INDLblDescriptionCreditCash.Visible = True
                INDLblValueCreditCash.Visible = True
                XrTable4.Visible = True
            Else
                GfHeaderCash.Visible = False
                INDLblDescriptionDebitCash.Visible = False
                INDLblValueDebitCash.Visible = False
                INDLblDescriptionCreditCash.Visible = False
                INDLblValueCreditCash.Visible = False
                XrTable4.Visible = False
            End If

            'si en el filtro van bancos
            If ParametrosReporte(2) <> "" Then
                GhBanks.Visible = True
                INDLblDescriptionDebitBank.Visible = True
                INDLblValueDebitBank.Visible = True
                INDLblDescriptionCreditBank.Visible = True
                INDLblValueCreditBank.Visible = True
                XrTable2.Visible = True
            Else
                GhBanks.Visible = False
                INDLblDescriptionDebitBank.Visible = False
                INDLblValueDebitBank.Visible = False
                INDLblDescriptionCreditBank.Visible = False
                INDLblValueCreditBank.Visible = False
                XrTable2.Visible = False
            End If

            'imprimir el saldo anterior de las cuentas bancarias
            'If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            PreviousBalanceBank = Await ValuePreviousBalance(1)
            'End If

            'imprimir el saldo anterior de las cajas
            'If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            PreviousBalanceCash = Await ValuePreviousBalance(2)
            'End If

        ElseIf ParametrosReporte(7) = 1 And ParametrosReporte(8) = 2 Then
            INDLblTitle.Text = "BOLETIN DE TESORERIA RESUMIDO"
            XrSubreport3.Visible = True
        End If

        'Moneda para el reporte padre
        Dim _culture As CultureInfo
        If Currency IsNot Nothing Then
            _culture = CultureInfo.CurrentCulture.Clone()
            _culture.NumberFormat = Currency.Abbreviation.GetNumberFormat()
            ApplyLocalization(_culture)
        End If
    End Sub

    'Total Debitos Banco
    Private Sub INDLblValueDebitBank_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblValueDebitBank.BeforePrint

        'imprimir total debito, total credito de las cuentas bancarias
        Me.INDLblValueDebitBank.Text = Format(CDec(rptTreasuryNewsletterReceipts.ValueDebit), "c2")

    End Sub

    'Total Creditos Banco
    Private Sub INDLblValueCreditBank_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblValueCreditBank.BeforePrint

        Me.INDLblValueCreditBank.Text = Format(CDec(rptTreasuryNewsletterExpenditures.ValueCredit), "c2")

    End Sub

    'Saldo Final de Bank
    Private Sub INDLblValueBalanceEnd_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblValueBalanceEnd.BeforePrint

        Dim ValueBalanceEnd = (CDec(PreviousBalanceBank) + rptTreasuryNewsletterReceipts.ValueDebit) - rptTreasuryNewsletterExpenditures.ValueCredit
        Me.INDLblValueBalanceEnd.Text = Format(ValueBalanceEnd, "c2")

    End Sub

    'Total Debitos Caja
    Private Sub INDLblValueDebitCash_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblValueDebitCash.BeforePrint

        'imprimir total debito, total credito de las cajas 
        Me.INDLblValueDebitCash.Text = Format(rptTreasuryNewsletterReceiptsCash.ValueDebitCashDecimal, "c2")

    End Sub

    'Total Creditos Caja
    Private Sub INDLblValueCreditCash_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblValueCreditCash.BeforePrint

        'imprimir total total credito de las cajas 
        Me.INDLblValueCreditCash.Text = Format(rptTreasuryNewsletterExpendituresCash.ValueCreditCashDecimal, "c2")

    End Sub

    'Saldo Final de Caja
    Private Sub INDLblValueBalanceEndCash_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblValueBalanceEndCash.BeforePrint

        Dim ValueBalanceEndCash = (PreviousBalanceCash + rptTreasuryNewsletterReceiptsCash.ValueDebitCashDecimal) - rptTreasuryNewsletterExpendituresCash.ValueCreditCashDecimal
        Me.INDLblValueBalanceEndCash.Text = Format(ValueBalanceEndCash, "c2")

    End Sub

    Private Sub INDLblPreviousBalance_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblPreviousBalance.BeforePrint
        'Se establece la abreviatura
        INDLblPreviousBalance.Text = Utils.GetMoneyWithISO4217(PreviousBalanceBank, If(String.IsNullOrEmpty(Currency.Abbreviation),
                                             IndigoSessionValues.CurrencyISO4217, Currency.Abbreviation))
    End Sub

    Private Sub INDLblPreviousBalanceCash_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblPreviousBalanceCash.BeforePrint
        'Se establece la abreviatura
        INDLblPreviousBalanceCash.Text = Utils.GetMoneyWithISO4217(PreviousBalanceCash, If(String.IsNullOrEmpty(Currency.Abbreviation),
                                             IndigoSessionValues.CurrencyISO4217, Currency.Abbreviation))
    End Sub

End Class