#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraReports.UI
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports System.Globalization

#End Region

Public Class rptVoucherTransaction006
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion.
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private INDUser As List(Of UserXpo)
    Private INDSettingTreasury As List(Of TreasurySettingsTreasuryXpo)
    Dim INDList As List(Of TreasuryVoucherTransactionXpo)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), TreasuryVoucherTransactionXpo).CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")

        'Ocultar el Beneficiario si llega vacia
        'fila de beneficiario tercero
        Dim table1 As XRTable = CType(XrTable3, XRTable)
        'fila de beneficiario
        Dim table2 As XRTable = CType(XrTable26, XRTable)
        'fila de tercero del cheque

        If (CType(INDList(0), TreasuryVoucherTransactionXpo).VoucherClass = 3) Then
            table1.Rows.Remove(XrTableRow3)
            table2.Rows.Remove(XrTableRow27)
        Else
            If (CType(INDList(0), TreasuryVoucherTransactionXpo).Beneficiary Is Nothing OrElse CType(INDList(0), TreasuryVoucherTransactionXpo).Beneficiary.Trim() = "") Then
                XrTable26.Visible = False
                'oculta el beneficiario si viene vacio
                XrTable25.Visible = False
            Else
                XrTable3.Visible = False
                'oculta el tercero si beneficiario tiene datos
                XrTable27.Visible = False
            End If
        End If

        'Tercero del movimiento según la parametrización en Tesoreria
        If (CType(INDList(0), TreasuryVoucherTransactionXpo).VoucherClass = 1) Then
            INDSettingTreasury = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySettingsTreasuryXpo)(Nothing, "IdOperatingUnit = " & IndigoSessionValues.IndigoOperatingUnitId)

            If (CType(INDList(0), TreasuryVoucherTransactionXpo).IdCashRegister IsNot Nothing) Then
                If (CType(INDSettingTreasury(0), TreasurySettingsTreasuryXpo).GetThirdPartyCashRegister = 2) Then
                    XrTableCell31.Visible = False
                    XrLabel3.Visible = True
                Else
                    XrTableCell31.Visible = True
                    XrLabel3.Visible = False
                End If
            Else
                If (CType(INDSettingTreasury(0), TreasurySettingsTreasuryXpo).GetThirdPartyBank = 2) Then
                    XrTableCell66.Visible = False
                    XrLabel3.Visible = True
                Else
                    XrTableCell66.Visible = True
                    XrLabel3.Visible = False
                End If
            End If
        End If

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

    Private Sub rptVoucherTransaction_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint

        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        If INDUser.Count() > 0 Then
            INDLblCreationUser.Text = INDUser(0).CodeName
        End If
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation = INDList?.FirstOrDefault?.CurrencyAbbreviation
        Dim CurrencyName = INDList?.FirstOrDefault?.CommonCurrency.CurrencyNameISO
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        ApplyLocalization(_culture)
        Dim CurrentValue As Decimal = Convert.ToDecimal(GetCurrentColumnValue("Value"))
        Dim _integerPart As Int64 = Int(CurrentValue)
        Dim _decimalPart As Integer = Strings.Right(Format(CurrentValue - _integerPart, "0.00"), 2)
        INDLblNumLetters.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString,
                                                  CurrencyName.ToUpper,
                                                  If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))

        XrLabel1.Text = String.Format("{0} {1}{2}",
                                                  Utils.Num2Text(_integerPart).ToString,
                                                  CurrencyName.ToUpper,
                                                  If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString } {Utils.ListDecimalCurrency(CurrencyAbbreviation)}", ""))
        XrTableCell33.Text = Utils.GetMoneyWithISO4217(CDec(0.00), CurrencyAbbreviation)
        XrTableCell68.Text = Utils.GetMoneyWithISO4217(CDec(0.00), CurrencyAbbreviation)
        XrTableCell78.Text = Utils.GetMoneyWithISO4217(CDec(CurrentValue), CurrencyAbbreviation)
        Dim lengthValue = XrLabel1.Text.Length

        If lengthValue <= 74 Then
            'XrLabel1.Text += Environment.NewLine & "****************************************************************************************************" & Environment.NewLine & "****************************************************************************************************"
        ElseIf lengthValue >= 75 AndAlso lengthValue <= 149 Then
            'XrLabel1.Text += Environment.NewLine & "****************************************************************************************************"
        End If

        Dim treasurySequence = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasurySequenceXpo)(Nothing, "IdForm = '636'")
        If treasurySequence IsNot Nothing Then
            INDPrScope.Value = treasurySequence(0).Scope
        End If
    End Sub

End Class