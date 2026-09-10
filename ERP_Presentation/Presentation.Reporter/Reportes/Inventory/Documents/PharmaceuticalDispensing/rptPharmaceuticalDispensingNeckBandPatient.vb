#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base

#End Region

Public Class rptPharmaceuticalDispensingNeckBandPatient
    Implements IReport

#Region "Properties"

    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Private DataList As List(Of ViewReportPharmaceuticalDispensingNeckBandPatientXpo)

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String
            If ParametrosReporte(0).GetType().Equals(GetType(Domain.Entities.PharmaceuticalDispensing)) Then
                filtroConsulta = String.Format("CodePharmaceuticalDispensing = '{0}'", ParametrosReporte(0).Code)
            Else
                filtroConsulta = "PharmaceuticalDispensingId = " & ParametrosReporte(0)
            End If

            DataList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).InventoryService.ListReportPharmaceuticalDispensingNeckBandPatient(filtroConsulta).ToList()
            Me.DataSource = DataList
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

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

    Private Sub rptPharmaceuticalDispensingNeckBandProcess_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Dim firsRow = DataList.FirstOrDefault()
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdPharmaceutical").Value}
            CargarDataSource()
        End If
        INDLblCompany.Text = String.Format("Farmacia {0}", IndigoSessionValues.IndigoCompanyName)
        Dim reporte As XtraReport = (CType(sender, XtraReport))
        reporte.Watermark.PageRange = "1"
    End Sub

    Private Sub INDTrBatchCode_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If String.IsNullOrEmpty(Me.GetCurrentColumnValue("BatchCode")) Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

End Class