#Region "Imports"

Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.PaymentsRepository
#End Region

Public Class rptSubAccountPayableAndVoucherTransactionSupportDocument
    Implements IReport

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Obtiene el listado de los datos del reporte
    ''' </summary>
    Dim data As List(Of ViewElectronicDocumentSupportRptXpo)

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
            If ParametrosReporte.Length = 2 Then

                Dim IdParentDocumentlist As New List(Of String)()
                IdParentDocumentlist = ParametrosReporte(0)

                Dim EntityNamelist As New List(Of String)()
                EntityNamelist = ParametrosReporte(1)

                Dim IdParentDocumentlistConcat As String = String.Join(",", IdParentDocumentlist)

                Dim groupedEntityNameList = EntityNamelist.GroupBy(Function(item) item).Select(Function(group) group.Key)
                Dim EntityNameConcat As String = String.Join(",", groupedEntityNameList.Select(Function(item) $"'{item}'"))

                filterParameters.Add($"IdParentDocument IN ({IdParentDocumentlistConcat}) And EntityName IN ({EntityNameConcat})")
            End If

            data = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ViewElectronicDocumentSupportRptXpo)(Nothing, String.Join(" AND ", filterParameters))

            If data IsNot Nothing AndAlso data.Any() Then
                DataSource = data.GroupBy(Function(m) m.IdParentDocument).Select(Function(m) m.FirstOrDefault()).ToList()
            End If
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

#End Region

#Region "Events"

    ''' <summary>
    ''' funcion para cargar la definicion customizada de los subreportes
    ''' </summary>
    ''' <param name="_tag"></param>
    ''' <param name="Name"></param>
    ''' <returns></returns>
    Public Function LoadCustomLayout(_tag As Object, Name As String) As String
        Dim nameRepDefault As String = If(File.Exists(Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default")), File.ReadAllText(Path.Combine(ConfigurationFile.Instance.ReportsPath, _tag & "Repx.Default"))?.Trim(), "*")
        Dim pattern As String = $"{_tag}.{Name}.{nameRepDefault}.repx"
        Return Directory.GetFiles(ConfigurationFile.Instance.ReportsPath, pattern, SearchOption.TopDirectoryOnly)?.FirstOrDefault
    End Function

#End Region

End Class