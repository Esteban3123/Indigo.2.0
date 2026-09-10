#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

Public Class rptReportRemissions
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
            Dim filter As String = String.Format("CampaignDetailId = {0}", ParametrosReporte(0))

            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewReportRemissionsXpo)(Nothing, filter).ToList().OrderBy(Function(x) x.BatchCode)
            CompanyName(IndigoSessionValues)
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

    Public Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Return Task.Factory.StartNew(AddressOf CargarDataSource)
    End Function

#End Region

#Region "Methods"


    Private Sub rpt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If ParametrosReporte(1) IsNot Nothing Then
            If ParametrosReporte(1) > 0 Then
                Select Case ParametrosReporte(1)
                    Case EUnitDoseTypeClass.ParenteralNutrition
                        XrTable2.DeleteColumn(XrTableCell54)
                        XrTable2.DeleteColumn(XrTableCell7)
                        XrTable5.DeleteColumn(XrTableCell64)
                        XrTable5.DeleteColumn(XrTableCell63)
                    Case EUnitDoseTypeClass.Magistral
                        XrTable5.DeleteColumn(XrTableCell64)
                        XrTable2.DeleteColumn(XrTableCell54)
                        XrTable5.DeleteColumn(XrTableCell65)
                        XrTable2.DeleteColumn(XrTableCell8)
                End Select

                CargarDataSource()
            End If
        End If
    End Sub

    Public Sub CompanyName(Sessions As SessionValues)
        INDXcCompanyName.Text = Sessions.IndigoCompanyName.ToString()
        INDCellCity.Text = Sessions.City
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

#End Region

End Class