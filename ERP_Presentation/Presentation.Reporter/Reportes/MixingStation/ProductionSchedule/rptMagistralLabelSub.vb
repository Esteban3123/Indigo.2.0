#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

Public Class rptMagistralLabelSub
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

    Dim INDList As List(Of Domain.Entities.CampaignDetail)
#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            INDList = New List(Of Domain.Entities.CampaignDetail)
            Dim param = ParametrosReporte(0)
            If param IsNot Nothing AndAlso param > 0 Then
                INDList.Add(New Domain.Entities.CampaignDetail With
                             {
                             .Id = param
                             }
                )
            End If

            Me.DataSource = INDList
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

#Region "Events"

    Private Sub rptTabletStickerSub_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDCampaignDetailId").Value}
            CargarDataSource()
        End If
    End Sub

#End Region

End Class