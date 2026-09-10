#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TaxesRepository
Imports DevExpress.XtraReports.UI
Imports Presentation.Base



Imports Domain.Entities
Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Resources
Imports System.Threading
#End Region

Public Class rptSubMapListTaxes
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        'Try
        '    'XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of TaxesViewGenerateInvoiceReportXpo)(Nothing, "Code In ('" & String.Join("','", CType(ParametrosReporte(0), List(Of String))) & "')")
        '     await  Task.Run(  Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TaxesService.ListViewTaxes("Code In ('" & String.Join("','", CType(ParametrosReporte(0), List(Of String))) & "')")
        'Catch ex As Exception
        '    MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        'End Try
    End Sub

    Public Async Function CargarDataSource1() As task
        Try
            Await Task.Run(Sub() Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TaxesService.ListViewTaxes("Code In ('" & String.Join("','", CType(ParametrosReporte(0), List(Of String))) & "')"))
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
End Class