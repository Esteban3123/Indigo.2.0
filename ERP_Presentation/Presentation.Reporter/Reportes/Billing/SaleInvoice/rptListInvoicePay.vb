#Region "Imports"

Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base

#End Region

Public Class rptListInvoicePay
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

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

            filterParameters.Add("AccountReceivableType = 4")

            If ParametrosReporte(0) IsNot Nothing AndAlso ParametrosReporte(1) IsNot Nothing Then
                filterParameters.Add("GetDate(AccountReceivableDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(AccountReceivableDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#")
            End If

            If ParametrosReporte(2) IsNot Nothing Then
                filterParameters.Add(String.Format("InvoiceId.PatientCode = '{0}'", ParametrosReporte(2)))
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of PortfolioAccountReceivableXpo)(Nothing, String.Join(" AND ", filterParameters))
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

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

#End Region

End Class