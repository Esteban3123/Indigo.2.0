#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base

#End Region

Public Class rptSales
    Implements IReport
    Implements IReportAsync

#Region "Properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
#End Region

#Region "Load Data"
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = "GetDate(CreationDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(CreationDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
            'si filtra por documento
            If ParametrosReporte(2) IsNot Nothing Then
                filtroConsulta &= "AND UserCodeNameAux = '" & ParametrosReporte(2) & "'"
            End If
            'si filtra por Almacén
            If Not String.IsNullOrEmpty(ParametrosReporte(3)) Then
                filtroConsulta &= String.Format(" AND IdAlmacen In ({0}) ", ParametrosReporte(3))
            End If
            'si filtra por Producto
            If Not String.IsNullOrEmpty(ParametrosReporte(4)) Then
                filtroConsulta &= String.Format(" AND IdProduct In ({0}) ", ParametrosReporte(4))
            End If
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of InventoryViewSalesReportXpo)(Nothing, filtroConsulta)
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
        Dim fields = properties.[Select](Function([property]) New With {
            Key .Name = [property].Name,
            Key .Value = [property].GetValue(exception, Nothing)
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

    Private Sub rptDispensation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit: " & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Format(ParametrosReporte(0), "yyyy-MM-dd")).ToString("dd De MMMM Del yyyy") & " " & CDate(Format(ParametrosReporte(1), "yyyy-MM-dd")).ToString(" al   dd De MMMM Del yyyy")
    End Sub
#End Region

End Class