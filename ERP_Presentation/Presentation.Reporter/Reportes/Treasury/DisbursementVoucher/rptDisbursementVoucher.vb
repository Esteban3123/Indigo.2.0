#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports Domain.Entities
Imports System.Globalization
#End Region

Public Class rptDisbursementVoucher
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance



    ''' <summary>
    ''' abreviacion de la moneda 
    ''' </summary>
    Dim CurrencyAbbreviation As String

    Dim listReport As List(Of TreasuryVoucherTransactionXpo)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = Nothing
        'Se filtra por Fechas
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            filtroConsulta &= "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
        End If

        'Se filtra por Terceros
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdThirdParty.Nit >= '" & ParametrosReporte(4) & "' AND IdThirdParty.Nit <= '" & ParametrosReporte(5) & "'"
            Else
                filtroConsulta &= "IdThirdParty.Nit >= '" & ParametrosReporte(4) & "' AND IdThirdParty.Nit <= '" & ParametrosReporte(5) & "'"
            End If
        End If

        'Se filtra por Comprobantes
        If ParametrosReporte(6) IsNot Nothing And ParametrosReporte(7) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND Code >= '" & ParametrosReporte(6) & "' AND Code <= '" & ParametrosReporte(7) & "'"
            Else
                filtroConsulta &= "Code >= '" & ParametrosReporte(6) & "' AND Code <= '" & ParametrosReporte(7) & "'"
            End If
        End If

        'Se filtra por Usuarios
        If ParametrosReporte(8) IsNot Nothing And ParametrosReporte(9) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND CreationUser >= '" & ParametrosReporte(8) & "' AND CreationUser <= '" & ParametrosReporte(9) & "'"
            Else
                filtroConsulta &= "CreationUser >= '" & ParametrosReporte(8) & "' AND CreationUser <= '" & ParametrosReporte(9) & "'"
            End If
        End If

        'Se filtra por Plantilla
        If ParametrosReporte(10) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND SchedulePaymentId.Id = " & ParametrosReporte(10)
            Else
                filtroConsulta &= "SchedulePaymentId.Id = " & ParametrosReporte(10)
            End If
        End If

        'Se filtra por Bancos
        If ParametrosReporte(11) IsNot Nothing And ParametrosReporte(12) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdEntityBankAccount.Code >= '" & ParametrosReporte(11) & "' AND IdEntityBankAccount.Code <= '" & ParametrosReporte(12) & "'"
            Else
                filtroConsulta &= "IdEntityBankAccount.Code >= '" & ParametrosReporte(11) & "' AND IdEntityBankAccount.Code <= '" & ParametrosReporte(12) & "'"
            End If
        End If

        'Se filtra por Cuenta
        If ParametrosReporte(13) IsNot Nothing And ParametrosReporte(14) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND IdCashRegister.Code >= '" & ParametrosReporte(13) & "' AND IdCashRegister.Code <= '" & ParametrosReporte(14) & "'"
            Else
                filtroConsulta &= "IdCashRegister.Code >= '" & ParametrosReporte(13) & "' AND IdCashRegister.Code <= '" & ParametrosReporte(14) & "'"
            End If
        End If

        'INDGleGroupingReport

        If ParametrosReporte(15) IsNot Nothing Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND VoucherClass = " & ParametrosReporte(15)
            Else
                filtroConsulta &= "VoucherClass = " & ParametrosReporte(15)
            End If
        End If
        'Se filtra por moneda
        If ParametrosReporte(16) > 0 Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND CurrencyId = " & ParametrosReporte(16)
            Else
                filtroConsulta &= "CurrencyId = " & ParametrosReporte(16)
            End If
        End If

        If ParametrosReporte(3) <> 5 Then 'Le agrego la condicion de "ParametrosReporte(3) <> 5" porque el 5 representa a Todos en el estado pero en la BD no existe lo cual antes no me traia nada cuando le daba el estado de Todos=5.
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND Status = " & ParametrosReporte(3)
            Else
                filtroConsulta &= "Status = " & ParametrosReporte(3)
            End If
        End If

        listReport = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryVoucherTransactionXpo)(Nothing, filtroConsulta)
        'listReport(0).ExpenseType = 1 And (SessionValues.Instance.IndigoCompanyType = 2 Or SessionValues.Instance.IndigoCompanyType = 3)

        
        'For Each item In listReport
        '    If item.IdCashRegister Is Nothing Then
        '        item.GroupByAccountBankThirdPartyUser = item.IdEntityBankAccount.Number & " - " & item.IdEntityBankAccount.IdBank.Name
        '    Else
        '        item.GroupByAccountBankThirdPartyUser = item.IdCashRegister.CodeName
        '    End If
        'Next
        If ParametrosReporte(2) = 5 Then
            Dim queryContratos2 = (From e In listReport Where e.IdEntityBankAccount IsNot Nothing Select e).ToList()
            listReport = queryContratos2
        End If

        If ParametrosReporte(2) = 2 Then
            Dim queryContratos3 = (From e In listReport Where e.IdCashRegister IsNot Nothing Select e).ToList()
            listReport = queryContratos3
        End If
        If ParametrosReporte(2) = 1 Then
            Dim queryContratos4 = (From e In listReport Where e.IdEntityBankAccount IsNot Nothing Select e).ToList()
            listReport = queryContratos4
        End If




        Me.DataSource = listReport

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptDisbursementVoucher_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.GroupByParameter.Value = ParametrosReporte(2)
        INDLBlSubtitle.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("dd De MMMM Del yyyy") & " " & CDate(ParametrosReporte(1)).ToString("A dd De MMMM Del yyyy")
        If SessionValues.Instance.IndigoCompanyType <> 2 AndAlso SessionValues.Instance.IndigoCompanyType <> 3 Then
            XrLabel3.Visible = False
            XrLabel4.Visible = False
        End If
        '---Se establece el number format al reporte dependiendo de la moneda
        Dim _culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
        Dim CurrencyAbbreviation As String = TryCast(Me.DataSource, List(Of TreasuryVoucherTransactionXpo))?.FirstOrDefault?.CurrencyAbbreviation
        Dim currencyName As String = TryCast(Me.DataSource, List(Of TreasuryVoucherTransactionXpo))?.FirstOrDefault?.CommonCurrency?.ISO4217Xpo?.CurrencyName
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        ApplyLocalization(_culture)
    End Sub
End Class