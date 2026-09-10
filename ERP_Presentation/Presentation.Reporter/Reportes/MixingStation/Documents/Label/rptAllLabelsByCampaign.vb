#Region "Imports"

Imports System.Drawing.Printing
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
'Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base

#End Region

Public Class rptAllLabelsByCampaign
    Implements IReport

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
            Dim filter As String = String.Format("CampaignDetailId = {0}", ParametrosReporte(0))

            Dim details = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MixingStationService.GetCollection(Of RequestMixingStationDetailXpo)(Nothing, filter)

            Dim newList As New List(Of RequestMixingStationDetailXpo)
            If details.Any(Function(m) m.LabelType.HasValue) Then
                Dim labelTypes = details.Where(Function(m) m.LabelType.HasValue).Select(Function(m) m.LabelType.Value).Distinct().ToList()

                For Each item In labelTypes
                    Dim requestMixingStationDetail = details.Where(Function(m) m.LabelType.HasValue AndAlso m.LabelType = item).FirstOrDefault()
                    newList.Add(requestMixingStationDetail)
                Next
            End If

            Me.DataSource = newList
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