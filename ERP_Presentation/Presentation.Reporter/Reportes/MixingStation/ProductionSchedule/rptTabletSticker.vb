#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

Public Class rptTabletSticker
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Private _sessionValues As SessionValues = SessionValues.Instance

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
            DataSource = Nothing
            Dim campaignDetailId As String = ParametrosReporte(0)
            Dim data = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewTabletSticker)(Nothing, $"CampaignDetailId = {campaignDetailId}")

            If data IsNot Nothing AndAlso data.Any() Then
                Dim orderedData = data.OrderBy(Function(p) p.ProductName).ThenBy(Function(p) p.InternalBatchCode).ToList()
                For Each item In orderedData.Where(Function(d) {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(d.UnitDoseTypeClass)).GroupBy(Function(d) d.ProductId)
                    Dim row = orderedData.Where(Function(d) d.ProductId = item.Key).FirstOrDefault
                    Dim rowClone = row.Clone()
                    rowClone.RequestPackageDetailStatusId = -1
                    orderedData.Insert(orderedData.FindIndex(Function(x) x.ProductAbbreviation = rowClone.ProductAbbreviation), rowClone)
                Next
                DataSource = orderedData
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

    Private Sub rpt_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 AndAlso Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("Id").Value}
            CargarDataSource()
        End If
        ApplyInitialLabelFormatting()
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

    ''' <summary> 
    '''  Aplica configuración inicial de márgenes y comportamiento visual de celdas para evitar desbordamientos.
    ''' </summary>
    Private Sub ApplyInitialLabelFormatting()
        For Each row As XRTableRow In XrTableStiker.Rows
            For Each cell As XRTableCell In row.Cells
                With cell
                    .CanGrow = False
                    .CanShrink = False
                    .WordWrap = True
                End With
            Next
        Next
    End Sub

#End Region
End Class