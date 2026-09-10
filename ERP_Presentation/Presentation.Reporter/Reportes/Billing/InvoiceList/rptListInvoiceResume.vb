#Region "Imports"

Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports System.Collections.ObjectModel

#End Region

Public Class rptListInvoiceResume
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Private IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filterParameters As New List(Of String)()

            If ParametrosReporte(0) IsNot Nothing AndAlso ParametrosReporte(1) IsNot Nothing Then
                filterParameters.Add("InvoiceDate >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd HH:mm:ss") & "# AND InvoiceDate <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd HH:mm:ss") & "#")
            End If

            If ParametrosReporte(2) <> 0 Then
                filterParameters.Add(String.Format("DocumentType = {0}", ParametrosReporte(2)))
            End If

            If ParametrosReporte(3) <> 3 Then
                filterParameters.Add(String.Format("Status = {0}", ParametrosReporte(3)))
            End If

            If ParametrosReporte(4) IsNot Nothing AndAlso ParametrosReporte(5) IsNot Nothing Then
                filterParameters.Add(String.Format("InvoiceNumber >= '{0}' AND InvoiceNumber <= '{1}'", ParametrosReporte(4), ParametrosReporte(5)))
            ElseIf ParametrosReporte(4) IsNot Nothing Then
                filterParameters.Add(String.Format("InvoiceNumber IN ({0})", ParametrosReporte(4)))
            End If

            If ParametrosReporte(6) IsNot Nothing Then
                filterParameters.Add(String.Format("HealthAdministratorId = {0}", ParametrosReporte(6)))
            End If

            If ParametrosReporte(7) IsNot Nothing Then
                filterParameters.Add(String.Format("PatientCode = '{0}'", ParametrosReporte(7)))
            End If

            If ParametrosReporte(8) IsNot Nothing Then
                filterParameters.Add(String.Format("CareGroupId = {0}", ParametrosReporte(8)))
            End If

            If ParametrosReporte(9) IsNot Nothing Then
                filterParameters.Add(String.Format("InvoiceCategoryId = {0}", ParametrosReporte(9)))
            End If

            If ParametrosReporte(10) IsNot Nothing Then
                filterParameters.Add(String.Format("ThirdPartyId = {0}", ParametrosReporte(10)))
            End If

            If ParametrosReporte(12) IsNot Nothing Then
                filterParameters.Add(String.Format("AdmissionNumber IN ({0})", ParametrosReporte(12)))
            End If

            If ParametrosReporte(13) IsNot Nothing Then
                filterParameters.Add(String.Format("RadicateInvoiceIdC = {0}", ParametrosReporte(13)))
            End If

            If ParametrosReporte(14) IsNot Nothing Then
                filterParameters.Add(String.Format("InvoicedUser = '{0}'", ParametrosReporte(14)))
            End If

            Dim DataSourceView = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingViewRadicatedInvoicReportXpo)(Nothing, String.Join(" AND ", filterParameters))

            Select Case ParametrosReporte(15)
                Case 1
                    Me.DataSource = New Collection(Of BillingViewRadicatedInvoicReportXpo)(DataSourceView.OrderBy(Function(x) x.AdmissionNumber).ToList())
                Case 2
                    Me.DataSource = New Collection(Of BillingViewRadicatedInvoicReportXpo)(DataSourceView.OrderBy(Function(x) x.InvoiceDate).ToList())
                Case 3
                    Me.DataSource = New Collection(Of BillingViewRadicatedInvoicReportXpo)(DataSourceView.OrderBy(Function(x) x.Nit).ToList())
            End Select

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

#End Region

#Region "Methods"

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Sub Fiters()
        'Entidad Administradora
        If ParametrosReporte(6) IsNot Nothing Then
            GroupHeader9.Visible = True
            XrTableCell71.Text = ParametrosReporte(16)
        End If
        'Paciente
        If ParametrosReporte(7) IsNot Nothing Then
            GroupHeader8.Visible = True
            XrTableCell69.Text = ParametrosReporte(17).ToString.Trim
        End If
        'Grupo de Atención
        If ParametrosReporte(8) IsNot Nothing Then
            GroupHeader7.Visible = True
            XrTableCell67.Text = ParametrosReporte(18)
        End If
        'Categoria
        If ParametrosReporte(9) IsNot Nothing Then
            GroupHeader6.Visible = True
            XrTableCell65.Text = ParametrosReporte(19)
        End If
        'Tercero
        If ParametrosReporte(10) IsNot Nothing Then
            GroupHeader5.Visible = True
            XrTableCell63.Text = ParametrosReporte(20)
        End If
        'Ingreso
        If ParametrosReporte(12) IsNot Nothing Then
            GroupHeader4.Visible = True
            XrTableCell61.Text = ParametrosReporte(21)
        End If
        'Radicado
        If ParametrosReporte(13) IsNot Nothing Then
            GroupHeader3.Visible = True
            XrTableCell59.Text = ParametrosReporte(22)
        End If
        'Usuario
        If ParametrosReporte(14) IsNot Nothing Then
            GroupHeader2.Visible = True
            XrTableCell57.Text = ParametrosReporte(23)
        End If
    End Sub

#End Region

#Region "Events"

    Private Sub rptListInvoiceResume_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim table As XRTable = CType(XrTable1, XRTable)
        Dim table2 As XRTable = CType(XrTable2, XRTable)
        Dim table3 As XRTable = CType(XrTable5, XRTable)
        Dim table4 As XRTable = CType(XrTable6, XRTable)
        Dim table5 As XRTable = CType(XrTable3, XRTable)
        Dim table6 As XRTable = CType(XrTable7, XRTable)
        Dim table7 As XRTable = CType(XrTable9, XRTable)
        Dim table8 As XRTable = CType(XrTable10, XRTable)
        Dim table9 As XRTable = CType(XrTable11, XRTable)
        Dim table10 As XRTable = CType(XrTable12, XRTable)

        If ParametrosReporte(10) <> 0 Then
            If ParametrosReporte(10) = 5 Then
                table3.Visible = False ''arriba
                table.Visible = True ''arriba
                table8.Visible = False ''arriba todos
                table4.Visible = False ''detalle
                table2.Visible = True ''detalle
                table9.Visible = False ''detalle todos
                table5.Visible = True ''total
                table6.Visible = True ''descuento
                table7.Visible = False ''total nuevo
                table10.Visible = False ''total todos
            Else
                table3.Visible = True ''arriba
                table.Visible = False ''arriba
                table8.Visible = False ''arriba todos
                table4.Visible = True ''detalle
                table2.Visible = False ''detalle
                table9.Visible = False ''detalle todos
                table5.Visible = False ''total
                table6.Visible = False ''descuento
                table7.Visible = True ''total nuevo
                table10.Visible = False ''total todos
            End If
        Else
            table3.Visible = False ''arriba
            table.Visible = False ''arriba
            table8.Visible = True ''arriba todos
            table4.Visible = False ''detalle
            table2.Visible = False ''detalle
            table9.Visible = True ''detalle todos
            table5.Visible = False ''total
            table6.Visible = False ''descuento
            table7.Visible = False ''total nuevo
            table10.Visible = True ''total todos
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión: " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDOrderBy.Value = ParametrosReporte(15)
        Fiters()
    End Sub
    Private Sub XrTable2_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell33.BeforePrint, XrTableCell12.BeforePrint, XrTableCell51.BeforePrint
        Dim t = TryCast(GetCurrentRow(), BillingViewRadicatedInvoicReportXpo)
        Select Case sender?.Name
            Case NameOf(XrTableCell33)
                XrTableCell33.Text = Utils.GetMoneyWithISO4217(t.TotalInvoice, t.CurrencyAbbreviation)
            Case NameOf(XrTableCell12)
                XrTableCell12.Text = Utils.GetMoneyWithISO4217(t.TotalInvoice, t.CurrencyAbbreviation)
            Case NameOf(XrTableCell51)
                XrTableCell51.Text = Utils.GetMoneyWithISO4217(t.TotalInvoice, t.CurrencyAbbreviation)
        End Select

    End Sub

    Private Sub XrTableCell36_SummaryGetResult(sender As Object, e As SummaryGetResultEventArgs) Handles XrTableCell15.SummaryGetResult, XrTableCell36.SummaryGetResult, XrTableCell37.SummaryGetResult
        Dim row = GetCurrentRow()
        e.Result = Utils.GetMoneyWithISO4217(e.CalculatedValues.ToEntityList(Of Decimal).Sum(), If(String.IsNullOrEmpty(row?.CurrencyAbbreviation),
                                             IndigoSessionValues.CurrencyISO4217, row?.CurrencyAbbreviation))
        e.Handled = True
    End Sub

    Private Sub xrtablecell53_beforeprint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTableCell53.BeforePrint

        Dim D = TryCast(GetCurrentRow(), BillingViewRadicatedInvoicReportXpo)
        Select Case D.DocumentType

            Case 1
                XrTableCell53.Text = ResourceManager.GetString("InvoiceWithContract", "Billing")
            Case 2
                XrTableCell53.Text = ResourceManager.GetString("InvoiceWithoutContract", "Billing")
            Case 3
                XrTableCell53.Text = ResourceManager.GetString("ParticularBill", "Billing")
            Case 4
                XrTableCell53.Text = ResourceManager.GetString("CapitatedBill", "Billing")
            Case 5
                XrTableCell53.Text = ResourceManager.GetString("CapitationControl", "Billing")
            Case 6
                XrTableCell53.Text = ResourceManager.GetString("BasicBill", "Billing")
            Case 8
                XrTableCell53.Text = ResourceManager.GetString("HealthBillParentAccount", "Billing")
            Case Else
                XrTableCell53.Text = ResourceManager.GetString("ProductSalesInvoice", "Billing")
        End Select
    End Sub

#End Region

End Class