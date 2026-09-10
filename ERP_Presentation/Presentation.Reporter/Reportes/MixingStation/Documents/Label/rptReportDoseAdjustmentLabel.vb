#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base

#End Region

Public Class rptReportDoseAdjustmentLabel
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

            If ParametrosReporte.Length > 1 AndAlso ParametrosReporte(1) IsNot Nothing AndAlso ParametrosReporte(1) = 1 Then
                filter += String.Format(" AND LabelType = 1")
            End If

            Dim data = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListPrintLabelsXpo)(Nothing, filter)
            If Not data.Any() Then
                Exit Sub
            End If

            data = data.OrderBy(Function(p) p.MainMedicines).ThenBy(Function(p) p.BatchCode).ToList()
            Me.DataSource = data
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
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("IdC").Value, ParametrosFilter("Origin").Value}
            CargarDataSource()

        End If

        Dim originParam = Parameters("Origin")
        If originParam IsNot Nothing AndAlso originParam.Value Is Nothing Then
            XrLabel1.Visible = False
        End If
        ApplyInitialLabelFormatting()
    End Sub

    Public Sub CompanyName(Sessions As SessionValues)
        INDXcCompanyName.Text = Sessions.IndigoCompanyName.ToString()
    End Sub


    ''' <summary>
    '''  Aplica configuración inicial de márgenes y comportamiento visual de celdas para evitar desbordamientos.
    ''' </summary>
    Private Sub ApplyInitialLabelFormatting()
        CompanyName(IndigoSessionValues)
        ''Margins = New Margins(left:=20, right:=0, top:=20, bottom:=0)
        For Each row As XRTableRow In XrTableAdjustmentLabel.Rows
            For Each cell As XRTableCell In row.Cells
                With cell
                    .CanGrow = False    ' No permitir que crezca con texto largo
                    .CanShrink = False  ' No permitir que se reduzca
                    .WordWrap = True    ' Permitir salto de línea automático
                End With
            Next
        Next
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