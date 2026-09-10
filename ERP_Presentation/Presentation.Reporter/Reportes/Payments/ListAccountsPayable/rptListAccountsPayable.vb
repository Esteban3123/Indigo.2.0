#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI

#End Region

Public Class rptListAccountsPayable
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Objeto para almacenar los datos
    ''' </summary>
    Dim listReport As List(Of PaymentsAccountPayable)

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

        Dim status = ParametrosReporte(2)
        Dim distributionLines As String = ParametrosReporte(3)
        Dim suppliers As String = ParametrosReporte(4)
        Dim accountPayables As String = ParametrosReporte(5)

        If status IsNot Nothing Then
            filtroConsulta &= String.Format(" AND Status IN ({0})", status)
        End If

        If Not String.IsNullOrEmpty(distributionLines) Then
            filtroConsulta &= String.Format(" AND IdSuppliersDistributionLines.IdDistributionLine.Id IN ({0})", distributionLines)
        End If

        If Not String.IsNullOrEmpty(suppliers) Then
            filtroConsulta &= String.Format(" AND IdSupplier.Id IN ({0})", suppliers)
        End If

        If Not String.IsNullOrEmpty(accountPayables) Then
            filtroConsulta &= String.Format(" AND Id IN ({0})", accountPayables)
        End If

        Me.listReport = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsAccountPayable)(Nothing, filtroConsulta)
        Me.DataSource = Me.listReport

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptListAccountsPayable_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        'Totalizo dependiendo de las monedas que vengan
        Me.CreateRowsByCurrencies(Me.listReport?.Select(Function(f) f.CurrencyAbbreviation).Distinct.ToList())
    End Sub

    Private Sub XrTableCell15_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell15.BeforePrint
        Dim row = GetCurrentRow()
        If row IsNot Nothing AndAlso String.IsNullOrEmpty(row.CurrencyAbbreviation) Then
            XrTableCell15.Text = Utils.GetMoneyWithISO4217(row.Value, row.CurrencyAbbreviation)
        End If
    End Sub

    Private Sub XrTableCell18_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell18.BeforePrint
        Dim row = GetCurrentRow()
        If row IsNot Nothing AndAlso String.IsNullOrEmpty(row.CurrencyAbbreviation) Then
            XrTableCell18.Text = Utils.GetMoneyWithISO4217(row.Balance, row.CurrencyAbbreviation)
        End If
    End Sub

    Private Sub CreateRowsByCurrencies(listCurrenciesAbbreviation As List(Of String))
        If listCurrenciesAbbreviation?.Any Then
            ' Suspend the table's layout.
            Me.XrtableTotals.SuspendLayout()

            For Each currencyAbbreviation In listCurrenciesAbbreviation
                Dim xrRow As New XRTableRow()
                Dim totalValue = Me.listReport?.Where(Function(w) w.CurrencyAbbreviation = currencyAbbreviation).Sum(Function(s) s.Value)
                Me.AddCellIntoTable(xrRow, New XRTableCell With {.Text = currencyAbbreviation})
                Me.AddCellIntoTable(xrRow, New XRTableCell With {.Text = Utils.GetMoneyWithISO4217(totalValue, currencyAbbreviation)})
                Me.XrtableTotals.Rows.Add(xrRow)
            Next

            ' Perform the table's layout.  
            Me.XrtableTotals.PerformLayout()
        End If
    End Sub

    ''' <summary>
    ''' Crea una nueva celda dentro de un objeto XRTable
    ''' </summary>
    ''' <param name="cell"></param>
    Private Sub AddCellIntoTable(row As XRTableRow, cell As XRTableCell)
        If cell IsNot Nothing Then
            ' Create a new table cell and set its text and width. 
            cell.Width = 200
            cell.Font = New System.Drawing.Font("Arial", 9.0, System.Drawing.FontStyle.Regular)
            cell.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)

            ' Change the table.  
            row.Cells.Add(cell)
            row.Width = 200
        End If
    End Sub
End Class