#Region "Imports"

Imports System.Drawing.Printing
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

Public Class rptSubRemissions
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

    Dim INDList As List(Of ViewReportRemissionsXpo)
#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            INDList = New List(Of ViewReportRemissionsXpo)

            If ParametrosReporte(0) IsNot Nothing AndAlso ParametrosReporte(0) > 0 Then
                Dim filter As String = String.Format("CampaignDetailId = {0}", ParametrosReporte(0))

                Me.INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewReportRemissionsXpo)(Nothing, filter)?.ToList()
            Else
                MessageIndigo.Show("No hay parametros para imprimir el reporte", MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If

            If Me.INDList.Any() Then
                Dim QueryGroup = INDList.GroupBy(Function(x) New With {Key x.CampaignDetailId, Key x.UnitDoseTypeMsClass}).Select(Function(d) New ViewReportRemissionsXpo With {.CampaignDetailId = d.Key.CampaignDetailId, .UnitDoseTypeMsClass = d.Key.UnitDoseTypeMsClass}).ToList()
                Me.DataSource = QueryGroup
            Else
                MessageIndigo.Show("No se encontraron datos", MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If

        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"

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

#End Region

End Class