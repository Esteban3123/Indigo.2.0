#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
#End Region

Public Class rptRemissionsEntrance
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = Nothing

        'filtro por fechas
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            filtroConsulta = "GetDate(RemissionDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(RemissionDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
        End If

        ' si filtra por documento
        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta = "Code >= '" & ParametrosReporte(2) & "' AND Code <= '" & ParametrosReporte(3) & "'"
            Else
                filtroConsulta &= " AND Code >= '" & ParametrosReporte(2) & "' AND Code <= '" & ParametrosReporte(3) & "'"
            End If
        End If

        'si filtra por proveedor
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            If filtroConsulta Is Nothing Then
                filtroConsulta = "SupplierId.IdThirdParty.Nit >= '" & ParametrosReporte(4) & "' AND SupplierId.IdThirdParty.Nit <= '" & ParametrosReporte(5) & "'"
            Else
                filtroConsulta &= " AND SupplierId.IdThirdParty.Nit >= '" & ParametrosReporte(4) & "' AND SupplierId.IdThirdParty.Nit <= '" & ParametrosReporte(5) & "'"
            End If
        End If

        'si filtra por almacenes
        If ParametrosReporte(7) <> String.Empty Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND WarehouseId.Id In (" & ParametrosReporte(7) & ")"
            Else
                filtroConsulta &= "WarehouseId.Id In (" & ParametrosReporte(7) & ")"
            End If
        End If

        'si filtra por Estado
        If ParametrosReporte(9) <> String.Empty Then
            If filtroConsulta IsNot Nothing Then
                filtroConsulta &= " AND Status In (" & ParametrosReporte(9) & ")"
            Else
                filtroConsulta &= "Status In (" & ParametrosReporte(9) & ")"
            End If
        End If

        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryRemissionEntranceReportXpo)(Nothing, filtroConsulta)
    End Sub

    ''' <summary>
    ''' Carga Asincrono para no bloquear la interfaz de usuario
    ''' </summary>
    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptRemissionsEntrance_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        Me.INDParameterGroup.Value = ParametrosReporte(6)
        'Muestra Los almacenes seleccionados
        If ParametrosReporte(8) <> String.Empty Then
            INDLblSubTitleWarehouse.Text = "Informe Comprendido de los Almacenes :" & ParametrosReporte(8)
        End If
        If ParametrosReporte(0) IsNot Nothing And ParametrosReporte(1) IsNot Nothing Then
            Me.INDLblSubTitle.Text = "Informe comprendido entre " & CDate(ParametrosReporte(0)).ToString("yyyy-MM-dd") & " y " & CDate(ParametrosReporte(1)).ToString("yyyy-MM-dd")
        End If
    End Sub

    Private Sub XrTableCell14_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell14.BeforePrint
        Dim row = GetCurrentRow()
        Dim Value = DirectCast(row, InventoryRemissionEntranceReportXpo)?.Value
        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation

        If Value > 0 AndAlso Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            XrTableCell14.Text = Utils.GetMoneyWithISO4217(Value, CurrencyAbbreviation)
        End If
    End Sub

    Private Sub XrTableCell15_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell15.BeforePrint
        Dim row = GetCurrentRow()
        Dim Value = DirectCast(row, InventoryRemissionEntranceReportXpo)?.IvaValue
        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation

        If Value > 0 AndAlso Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            XrTableCell15.Text = Utils.GetMoneyWithISO4217(Value, CurrencyAbbreviation)
        End If
    End Sub

    Private Sub XrTableCell16_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell16.BeforePrint
        Dim row = GetCurrentRow()
        Dim Value = DirectCast(row, InventoryRemissionEntranceReportXpo)?.TotalValue
        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation

        If Value > 0 AndAlso Not String.IsNullOrEmpty(CurrencyAbbreviation) Then
            XrTableCell16.Text = Utils.GetMoneyWithISO4217(Value, CurrencyAbbreviation)
        End If
    End Sub

    Private Sub XrTableCell26_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell26.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell27_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell27.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell28_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell28.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell20_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell20.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell21_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell21.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub XrTableCell22_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs) Handles XrTableCell22.SummaryGetResult
        Dim row = GetCurrentRow()

        Dim CurrencyAbbreviation = DirectCast(row, InventoryRemissionEntranceReportXpo)?.CurrencyAbbreviation
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, CurrencyAbbreviation))
        e.Handled = True
    End Sub
End Class