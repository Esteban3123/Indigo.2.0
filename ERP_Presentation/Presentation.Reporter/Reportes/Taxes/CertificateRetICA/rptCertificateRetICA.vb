#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TaxesRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI
Imports Presentation.CloudAgent
Imports Presentation.Base

#End Region

Public Class rptCertificateRetICA
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim listReport As List(Of GeneralLedgerJournalVoucherDeatilsXpo)
    Dim listReport2 As List(Of CommonOperatingUnitXpo)

    ''' <summary>
    ''' Variable par obtener la tabla de Taxes IVA
    ''' </summary>
    Dim dtReportTaxesICA As DataTable

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

        Dim filtroConsulta As String = "GetDate(IdAccounting.VoucherDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") &
            "# AND GetDate(IdAccounting.VoucherDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "# AND IdMainAccount.RetencionType = 3 and IdAccounting.Status = 2 and IdAccounting.IsClosedYear = 0"

        'Se filtra por Cuentas
        If ParametrosReporte(2) IsNot Nothing And ParametrosReporte(3) IsNot Nothing Then
            filtroConsulta &= " AND IdMainAccount.Number >= '" & ParametrosReporte(2) & "' AND IdMainAccount.Number <= '" & ParametrosReporte(3) & "'"
        End If

        'Se filtra por Terceros
        If ParametrosReporte(4) IsNot Nothing And ParametrosReporte(5) IsNot Nothing Then
            filtroConsulta &= " AND IdThirdParty.Nit >= '" & ParametrosReporte(4) & "' AND IdThirdParty.Nit <= '" & ParametrosReporte(5) & "'"
        End If

        'Se filtra por Libro
        If ParametrosReporte(10) IsNot Nothing Then
            filtroConsulta &= " AND IdAccounting.LegalBookId = " & ParametrosReporte(10)
        End If

        listReport = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of GeneralLedgerJournalVoucherDeatilsXpo)(Nothing, filtroConsulta)
        Me.DataSource = listReport

        If listReport.Count > 0 Then
            If (CType(listReport(0), GeneralLedgerJournalVoucherDeatilsXpo)).IdMainAccount.IdAccountClass.Nature = 1 Then
                'Dim itemResult = (From x In listReport Group x By x.IdThirdParty Into resulValue = Sum(CType(x.BaseValue * (x.RetentionRate / 100), Decimal)) Select New With {.resulValue = resulValue, .IdThirdParty = IdThirdParty.Id}).ToList
                Dim itemResult = (From x In listReport Group x By x.IdThirdParty Into resulValue = Sum(CType(x.DebitValue - x.CreditValue, Decimal)) Select New With {.resulValue = resulValue, .IdThirdParty = IdThirdParty.Id}).ToList
                For Each item In itemResult
                    For Each itemlist In (From x In listReport Where x.IdThirdParty.Id = item.IdThirdParty Select x).ToList
                        itemlist.ValueLetters = Utils.Num2Text(Convert.ToDouble(Convert.ToInt64(Math.Abs(item.resulValue)))).ToString & " PESOS M/Cte."
                    Next
                Next
            Else
                Dim itemResult2 = (From x In listReport Group x By x.IdThirdParty Into resulValue = Sum(CType(x.CreditValue - x.DebitValue, Decimal)) Select New With {.resulValue = resulValue, .IdThirdParty = IdThirdParty.Id}).ToList
                For Each item2 In itemResult2
                    For Each itemlist In (From x In listReport Where x.IdThirdParty.Id = item2.IdThirdParty Select x).ToList
                        itemlist.ValueLetters = Utils.Num2Text(Convert.ToDouble(Convert.ToInt64(Math.Abs(item2.resulValue)))).ToString & " PESOS M/Cte."
                    Next
                Next
            End If
        End If

    End Sub

    ''' <summary>
    ''' Carga el datasource de manera asyncrona para generar el reporte
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CargarDataSourceAsync() As Task
        Try
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCertificateRetICAAsync(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), 3, ParametrosReporte(10), Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportTaxesICA = ds.Tables("ReportCertificateRetICA")
                Me.DataSource = dtReportTaxesICA
                Me.DataMember = "ReportCertificateRetICA"
            Else
                Me.DataSource = Nothing
            End If
            Application.DoEvents()
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Private Sub rptCertificateRetICA_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Me.XrTableCell19.Text = IndigoSessionValues.IndigoCompanyName
        Me.XrTableCell4.Text = IndigoSessionValues.IndigoCompanyNit
        'Me.XrTableCell8.Text = "Calle 5W # A-21"

        Dim Mes As String = ""
        Dim Mes2 As String = ""
        Select Case (ParametrosReporte(7))
            Case 1
                Mes = "Enero"
            Case 2
                Mes = "Febrero"
            Case 3
                Mes = "Marzo"
            Case 4
                Mes = "Abril"
            Case 5
                Mes = "Mayo"
            Case 6
                Mes = "Junio"
            Case 7
                Mes = "Julio"
            Case 8
                Mes = "Agosto"
            Case 9
                Mes = "Septiembre"
            Case 10
                Mes = "Octubre"
            Case 11
                Mes = "Noviembre"
            Case 12
                Mes = "Diciembre"
        End Select

        Select Case (ParametrosReporte(9))
            Case 1
                Mes2 = "Enero"
            Case 2
                Mes2 = "Febrero"
            Case 3
                Mes2 = "Marzo"
            Case 4
                Mes2 = "Abril"
            Case 5
                Mes2 = "Mayo"
            Case 6
                Mes2 = "Junio"
            Case 7
                Mes2 = "Julio"
            Case 8
                Mes2 = "Agosto"
            Case 9
                Mes2 = "Septiembre"
            Case 10
                Mes2 = "Octubre"
            Case 11
                Mes2 = "Noviembre"
            Case 12
                Mes2 = "Diciembre"
        End Select

        If ParametrosReporte(6) = ParametrosReporte(8) Then
            If (ParametrosReporte(7) = ParametrosReporte(9)) Then
                Me.LblSubtitle.Text = "Para el Mes de " & Mes & " de " & ParametrosReporte(6)
            Else
                Me.LblSubtitle.Text = "Comprendido entre " & Mes & " y " & Mes2 & " de " & ParametrosReporte(6)
            End If
        Else
            Me.LblSubtitle.Text = "Comprendido entre " & Mes & " de " & ParametrosReporte(6) & " y " & Mes2 & " de " & ParametrosReporte(8)
        End If


        Dim filtroConsulta2 = "Id = " & IndigoSessionValues.IndigoOperatingUnitId
        listReport2 = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of CommonOperatingUnitXpo)(Nothing, filtroConsulta2)

        If listReport2 IsNot Nothing AndAlso listReport2.Count > 0 Then
            Dim commonOperatingUnit = CType(listReport2(0), CommonOperatingUnitXpo)

            If commonOperatingUnit.IdCity IsNot Nothing AndAlso commonOperatingUnit.IdCity.Name IsNot Nothing Then
                Me.XrTableCell12.Text = commonOperatingUnit.IdCity.Name.ToString
            End If
            If commonOperatingUnit.Address IsNot Nothing Then
                Me.XrTableCell8.Text = commonOperatingUnit.Address.ToString
            End If
        End If
    End Sub

    Dim RetentionValue As Double = 0
    Private Sub XrTableCell25_SummaryRowChanged(sender As Object, e As EventArgs) Handles XrTableCell25.SummaryRowChanged
        RetentionValue += Convert.ToDouble(GetCurrentColumnValue("ValorRetenido"))
    End Sub

    Private Sub XrTableCell45_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell45.BeforePrint
        sender.Text = Utils.Num2Text(Math.Abs(RetentionValue)).ToString & " PESOS M/Cte."
        RetentionValue = 0
    End Sub
End Class