#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing

Imports System.Drawing
Imports DevExpress.XtraPrinting.Drawing
Imports Presentation.Base
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptAccountPayableConceptLiquidation
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Try
            Dim filtroConsulta As String = ""

            If ParametrosReporte(0) IsNot Nothing Then
                filtroConsulta = "AccountPayableDetailConceptId.IdAccountPayable.Id = " & ParametrosReporte(0)
            End If

            'pasar el parametro
            Dim INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PaymentsService.ListAccountPayableDetailConceptLiquidationByAccountPayableDetailConceptId(filtroConsulta)
            If INDList.Count > 0 Then
                Me.DataSource = INDList

                Dim companySettings = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of Infrastructure.Data.Xpo.AccountingRepository.CompanySettingsXpo)
                Me.INDPrLimitMontabcd.Value = companySettings(0).UVT * 5040 / 12
            Else
                Me.Visible = False
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
        End Try
    End Sub

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

    'Private Sub Detail_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Detail.BeforePrint

    'End Sub

    Private Sub rptAccountPayableConceptLiquidation_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDIdAccountPayableSubreport").Value}
            CargarDataSource()
        End If
        Dim INDValue384 = (GetCurrentColumnValue("RetentionValue384"))

        Dim tableValueArt383 As XRTable = CType(INDCfArt383Last, XRTable)
        Dim table2 As XRTable = CType(XrTable24, XRTable)

        If INDValue384 = 0 Then
            table2.Visible = False
            table2.HeightF = 0

            tableValueArt383.Visible = False
            XrTableCell61.Visible = True
        Else
            table2.Visible = True
            table2.HeightF = 20

            tableValueArt383.Visible = True
            XrTableCell61.Visible = False
        End If
    End Sub
End Class