#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base

#End Region

Public Class rptTreasuryAudit
    Implements IReport

    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            'Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"
            Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(ParametrosReporte(0), "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(ParametrosReporte(1), "yyyy-MM-dd") & "#"

            'si filtra por documento
            If ParametrosReporte(2) IsNot Nothing Then
                filtroConsulta &= "AND LegalBookId = " & ParametrosReporte(2)
            End If

            ''si filtra por Producto
            'If ParametrosReporte(3) IsNot Nothing Then
            '    filtroConsulta &= " AND CodeProduct >= '" & ParametrosReporte(3) & "'"
            'End If

            'si filtra por Almacén
            If ParametrosReporte(4) <> String.Empty And ParametrosReporte(5) <> String.Empty Then
                filtroConsulta &= " AND Number >= '" & ParametrosReporte(4) & "' AND Number <= '" & ParametrosReporte(5) & "'"
            End If

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of GeneralLedgerVTreasuryAuditReportXpo)(Nothing, filtroConsulta)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try

    End Sub

    ''' <summary>
    ''' Carga Asincrono para no bloquear la interfaz de usuario
    ''' </summary>
    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
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

    Private Sub rptTreasuryAudit_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit: " & IndigoSessionValues.IndigoCompanyNit
        INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        INDLblDate.Text = "Informe comprendido entre " & CDate(Format(ParametrosReporte(0), "yyyy-MM-dd")).ToString("dd De MMMM Del yyyy") & " " & CDate(Format(ParametrosReporte(1), "yyyy-MM-dd")).ToString(" al   dd De MMMM Del yyyy")
    End Sub
End Class