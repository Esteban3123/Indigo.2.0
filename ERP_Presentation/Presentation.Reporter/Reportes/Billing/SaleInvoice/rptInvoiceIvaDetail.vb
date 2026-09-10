
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports DevExpress.XtraReports.UI


Public Class rptInvoiceIvaDetail
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property CustomDatasource As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "Id = " & Me.Parameters(0).Value
            Dim filterData As Object
            ''En caso de que la info venga desde el reporte parcial

            Select Case Me.Parameters(1).Value
                Case 0
                    Dim DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingVReportInvoicePartial)(Nothing, filtroConsulta)
                    Dim dataa = TryCast(DataSource, List(Of BillingVReportInvoicePartial)).FirstOrDefault()
                    Dim detail = TryCast(dataa.BillingVReportInvoicePartialDetail.ToList(), List(Of BillingVReportInvoicePartialDetail))
                    filterData = (From item In detail
                                  Group By item.IvaPercentage Into Group
                                  Select IvaPercentage, IvaTotalValue = Group.Sum(Function(y) y.IvaTotalValue),
                                 GrossValue = Group.Sum(Function(y) y.NetWorth), BaseTypeName = NameBaseType(IvaPercentage)) _
                               .Where(Function(y) y.IvaPercentage IsNot Nothing)
                Case 1, 2
                    '' en caso de que venga del reporte de factura
                    Dim DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of BillingVReportInvoice)(Nothing, filtroConsulta)
                    Dim dataa As BillingVReportInvoice = TryCast(DataSource, List(Of BillingVReportInvoice)).FirstOrDefault()
                    Dim detail = TryCast(dataa.BillingVReportInvoiceDetail.ToList(), List(Of BillingVReportInvoiceDetail))
                    filterData = (From item In detail
                                  Group By item.IvaPercentage Into Group
                                  Select IvaPercentage, IvaTotalValue = Group.Sum(Function(y) y.IvaTotalValue),
                                 GrossValue = Group.Sum(Function(y) y.NetWorth), BaseTypeName = NameBaseType(IvaPercentage)) _
                    .Where(Function(y) y.IvaPercentage IsNot Nothing)
                Case 3
                    filterData = CustomDatasource
            End Select

            DetailReport.DataSource = filterData
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public Function GetExceptionDetails(exception As Exception) As String
        Dim properties = exception.[GetType]().GetProperties()
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
        }).[Select](Function(x) [String].Format("{0} : {1}", x.Name, If(x.Value IsNot Nothing, x.Value.ToString(), [String].Empty)))
        Return [String].Join(vbLf, fields)
    End Function

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte


    Private Sub rptInvoicePay_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 Then
            CargarDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Retorna el nombre del tipo de la base
    ''' </summary>
    ''' <returns></returns>
    Public Function NameBaseType(item As String) As String
        If String.IsNullOrEmpty(item) Then
            Return "BASE NO GRAVADA"
        End If

        Select Case item
            Case "0.00%"
                Return "BASE EXENTA"
            Case Else
                Return "BASE GRAVADA"
        End Select
    End Function
End Class