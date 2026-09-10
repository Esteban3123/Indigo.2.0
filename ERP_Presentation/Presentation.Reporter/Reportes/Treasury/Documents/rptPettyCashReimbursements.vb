#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports System.Globalization
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptPettyCashReimbursements
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Obtiene el datasource del reporte
    ''' </summary>
    Dim INDList As List(Of TreasuryRefundsXpo)

    Const CNameReport = "Tesoreria.FrmPettyCashReimbursements"

    Private INDValue As Double
    Private INDInitialDate As Date
    Private INDFinalDate As Date
    Private INDUser As Object
    Dim filtroConsulta As String

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        If ParametrosReporte.Length > 1 Then
            filtroConsulta = "Id = " & ParametrosReporte(0)
            filtroConsulta &= " AND GetDate(InitialDate) >= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "# And GetDate(FinalDate) <= #" & Format(ParametrosReporte(2), "yyyy-MM-dd") & "#"
        Else
            filtroConsulta = "Id = " & ParametrosReporte(0)
        End If

        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryRefundsXpo)(Nothing, filtroConsulta)
        Dim INDCodeUser = CType(INDList(0), TreasuryRefundsXpo).CreationUser.Trim()
        INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
        INDValue = CType(INDList(0), TreasuryRefundsXpo).Value
        INDInitialDate = CType(INDList(0), TreasuryRefundsXpo).InitialDate
        INDFinalDate = CType(INDList(0), TreasuryRefundsXpo).FinalDate

        Me.DataSource = INDList

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptPettyCashReimbursements.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptPettyCashReimbursements_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdReimbursements").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If INDUser.count() > 0 Then
            INDUserCreate.Text = INDUser(0).CodeName
        End If
        If ParametrosReporte.Length > 1 Then
            XrTableCell6.Text = CDate(ParametrosReporte(1)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(2)).ToString("A dd De MMMM Del yyyy")
        Else
            XrTableCell6.Text = CDate(INDInitialDate).ToString("dd De MMMM Del yyyy") & " " & CDate(INDFinalDate).ToString("A dd De MMMM Del yyyy")
        End If

        Dim currencyAbbreviation As String = Me.DataSource?(0).IdCashRegister?.CurrencyAbbreviation
        Dim currencyName As String = Me.DataSource?(0).IdCashRegister?.CurrencyNameISO
        Dim CurrencyDecimal = Me.DataSource?(0).IdCashRegister?.CommonCurrency?.ISO4217Xpo?.CodeAbbreviation

        Dim _culture As CultureInfo
        If String.IsNullOrEmpty(currencyAbbreviation) Then
            currencyAbbreviation = SessionValues.Instance.CurrencyISO4217
            currencyName = currencyAbbreviation
        End If
        _culture = CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = currencyAbbreviation.GetNumberFormat
        ApplyLocalization(_culture)

        Dim _integerPart As Integer = Int(Convert.ToDecimal(GetCurrentColumnValue("Value")))
        Dim _decimalPart As Integer = Strings.Right(Format(Convert.ToDecimal(GetCurrentColumnValue("Value")) - _integerPart, "0.00"), 2)
        Me.XrTableCell16.Text = String.Format("{0}  {1}{2}",
                                                 Utils.Num2Text(_integerPart).ToString,
                                                 currencyName.ToUpper,
                                                 If(_decimalPart > 0, $", CON {Utils.Num2Text(_decimalPart).ToString} {Utils.ListDecimalCurrency(CurrencyDecimal)}", ""))


    End Sub


End Class