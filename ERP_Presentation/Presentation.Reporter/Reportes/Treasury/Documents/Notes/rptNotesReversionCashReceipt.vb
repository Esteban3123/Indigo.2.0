#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Presentation.CloudAgent
Imports System.Globalization
Imports System.Drawing
Imports DevExpress.XtraGrid

#End Region

Public Class rptNotesReversionCashReceipt
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim INDList As List(Of TreasuryNotesXpo)
    Private INDUser As Object

    ''' <summary>
    ''' abreviacion de la moneda 
    ''' </summary>
    Dim CurrencyAbbreviation As String = IndigoSessionValues.CurrencyISO4217

    ''' <summary>
    ''' Nombre de la moneda
    ''' </summary>
    Dim CurrencyName As String = IndigoSessionValues.CurrencyName

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryNotesXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), TreasuryNotesXpo).CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        Me.DataSource = INDList
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptNotesReversionCashReceipt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdNotesReceiptCash").Value}
            CargarDataSource()
        End If

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit

        'Inserta el Usuario de creacion en el reporte
        If INDUser.count() > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If

        If INDList?.Any() Then
            Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            CurrencyAbbreviation = INDList?.FirstOrDefault?.CashReceiptId?.CurrencyAbbreviation
            CurrencyName = INDList?.FirstOrDefault?.CashReceiptId?.CurrencyNameISO
            _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
            ApplyLocalization(_culture)
        End If

        Dim _integerPart As Integer = Int(Convert.ToDecimal(CType(INDList(0), TreasuryNotesXpo).CashReceiptId.Value))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(CType(INDList(0), TreasuryNotesXpo).CashReceiptId.Value) - _integerPart, "0.00"), 2)
        INDLblNumLetters.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString,
                                                  CurrencyName.ToUpper,
                                                  If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))


        Dim value As Decimal = 0
        For Each item In INDList?.FirstOrDefault?.CashReceiptId?.TreasuryPaymentMethodsXpo?.ToList().FindAll(Function(x) x.PaymentMethodTypes <> 5)
            value += item.ValueInCurrencyHeader
        Next
        INDLblTitle.Text = If(INDList?.FirstOrDefault?.NoteType = 7, "NOTA DE DEVOLUCIÓN DE R.C", "NOTA DE REVERSION R. C.")

        XrTableCell48.Text = Utils.GetMoneyWithISO4217(value, CurrencyAbbreviation)

        'Crear nuevas reglas y agregar al reporte
        Dim INDFrPaymentMethodPoints, INDFrPaymentMethodCard, INDFrPaymentMethodCheck, INDFrPaymentMethodCash, INDFrPaymentMethodConsignment As New FormattingRule()
        Me.FormattingRuleSheet.AddRange(New DevExpress.XtraReports.UI.FormattingRule() {INDFrPaymentMethodPoints, INDFrPaymentMethodCard, INDFrPaymentMethodCheck, INDFrPaymentMethodCash, INDFrPaymentMethodConsignment})

        'Especificar las propiedades de la regla
        INDFrPaymentMethodPoints.DataMember = "CashReceiptId.TreasuryPaymentMethodsXpo"
        INDFrPaymentMethodPoints.Condition = "[PaymentMethodTypes] != 5" 'Puntos
        INDFrPaymentMethodPoints.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        INDFrPaymentMethodPoints.Name = "INDFrPaymentMethodPoints"

        INDFrPaymentMethodCard.DataMember = "CashReceiptId.TreasuryPaymentMethodsXpo"
        INDFrPaymentMethodCard.Condition = "[PaymentMethodTypes] != 3"
        INDFrPaymentMethodCard.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        INDFrPaymentMethodCard.Name = "INDFrPaymentMethodCard"

        INDFrPaymentMethodCheck.DataMember = "CashReceiptId.TreasuryPaymentMethodsXpo"
        INDFrPaymentMethodCheck.Condition = "[PaymentMethodTypes] !=2"
        INDFrPaymentMethodCheck.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        INDFrPaymentMethodCheck.Name = "INDFrPaymentMethodCheck"

        INDFrPaymentMethodConsignment.DataMember = "CashReceiptId.TreasuryPaymentMethodsXpo"
        INDFrPaymentMethodConsignment.Condition = "[PaymentMethodTypes] != 4"
        INDFrPaymentMethodConsignment.Formatting.Visible = DevExpress.Utils.DefaultBoolean.[False]
        INDFrPaymentMethodConsignment.Name = "INDFrPaymentMethodConsignment"

        'Especificar a quienes va a aplicar la regla CHEQUE
        Me.XrTable11.FormattingRules.Add(INDFrPaymentMethodCheck)
        Me.XrTable7.FormattingRules.Add(INDFrPaymentMethodCheck)

        'Especificar regla TARJETA
        Me.XrTable16.FormattingRules.Add(INDFrPaymentMethodCard)
        Me.XrTable12.FormattingRules.Add(INDFrPaymentMethodCard)

        'Especificar regla CONSIGNACION
        Me.XrTable17.FormattingRules.Add(INDFrPaymentMethodConsignment)
        Me.XrTable13.FormattingRules.Add(INDFrPaymentMethodConsignment)

        'Especificar regla PUNTOS
        Me.XrTable10.FormattingRules.Add(INDFrPaymentMethodPoints)
        Me.XrTable19.FormattingRules.Add(INDFrPaymentMethodPoints)
    End Sub

    Private Sub XrTableCell47_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell47.BeforePrint
        XrTableCell47.Text = Utils.GetMoneyWithISO4217(0.00, CurrencyAbbreviation)
    End Sub

    Private Sub Detail3_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail3.BeforePrint
        Dim obj = TryCast(Me.DetailReport2.GetCurrentRow, TreasuryPaymentMethodsXpo)
        If obj Is Nothing Then
            e.Cancel = True
        End If
    End Sub
End Class