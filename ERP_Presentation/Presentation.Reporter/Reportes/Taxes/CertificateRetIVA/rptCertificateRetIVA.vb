#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TaxesRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.XtraReports.UI
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.CloudAgent
Imports Presentation.Base

#End Region

Public Class rptCertificateRetIVA
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim listReport As List(Of GeneralLedgerJournalVoucherDeatilsXpo)
    Dim listReport2 As List(Of CommonOperatingUnitXpo)
    Dim thirdParty As List(Of Infrastructure.Data.Xpo.TaxesRepository.CommonThirdPartyXpo)
    Dim address As List(Of CommonAddressXpo)

    ''' <summary>
    ''' Variable par obtener la tabla de Taxes IVA
    ''' </summary>
    Dim dtReportTaxesIVA As DataTable

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            thirdParty = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.TaxesRepository.CommonThirdPartyXpo)(Nothing, "Nit ='" & IndigoSessionValues.IndigoCompanyNit & "'")
            If thirdParty IsNot Nothing OrElse thirdParty.Count > 0 Then
                address = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of CommonAddressXpo)(Nothing, "IdPerson =" & thirdParty(0).PersonId)
            End If
            Dim ds As DataSet = IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCertificateRetIVA(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), 2, ParametrosReporte(10), Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportTaxesIVA = ds.Tables("ReportCertificateRetIVA")
                For Each row As DataRow In dtReportTaxesIVA.Rows
                    row(8) = Utils.Num2Text(Convert.ToDouble(Convert.ToInt64(Math.Abs(row(7))))).ToString & " PESOS M/Cte."
                    'MessageBox.Show(row(7).ToString())
                Next
                Me.DataSource = dtReportTaxesIVA
                Me.DataMember = "ReportCertificateRetIVA"
            Else
                Me.DataSource = Nothing
            End If
            Application.DoEvents()
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    ''' <summary>
    ''' Carga el datasource de manera asyncrona para generar el reporte
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CargarDataSourceAsync() As Task
        Try
            thirdParty = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.TaxesRepository.CommonThirdPartyXpo)(Nothing, "Nit ='" & IndigoSessionValues.IndigoCompanyNit & "'")
            If thirdParty IsNot Nothing OrElse thirdParty.Count > 0 Then
                address = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.GetCollection(Of CommonAddressXpo)(Nothing, "IdPerson =" & thirdParty(0).PersonId)
            End If
            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetListReportCertificateRetIVAAsync(ParametrosReporte(0), ParametrosReporte(1), ParametrosReporte(2), ParametrosReporte(3), ParametrosReporte(4), ParametrosReporte(5), 2, ParametrosReporte(10), Me.IndigoSessionValues)
            If ds.Tables(0).Rows.Count > 0 Then
                dtReportTaxesIVA = ds.Tables("ReportCertificateRetIVA")
                For Each row As DataRow In dtReportTaxesIVA.Rows
                    row(8) = Utils.Num2Text(Convert.ToDouble(Convert.ToInt64(Math.Abs(row(7))))).ToString & " PESOS M/Cte."
                Next
                Me.DataSource = dtReportTaxesIVA
                Me.DataMember = "ReportCertificateRetIVA"
            Else
                Me.DataSource = Nothing
            End If
            Application.DoEvents()
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Function

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With { _
            Key .Name = [property].Name, _
            Key .Value = [property].GetValue(exception, Nothing) _
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptCertificateRetIVA_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName

        Me.XrTableCell19.Text = IndigoSessionValues.IndigoCompanyName
        If thirdParty.Count > 0 Then
            Me.XrTableCell4.Text = IndigoSessionValues.IndigoCompanyNit + thirdParty(0).DigitVerification
        Else
            Me.XrTableCell4.Text = IndigoSessionValues.IndigoCompanyNit
        End If

        If address.Count > 0 Then
            Me.XrTableCell8.Text = address(0).Addresss
        Else
            Me.XrTableCell8.Text = "No Asignada"
        End If

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
        Me.XrTableCell12.Text = CType(listReport2(0), CommonOperatingUnitXpo).IdCity.Name.ToString
    End Sub
End Class