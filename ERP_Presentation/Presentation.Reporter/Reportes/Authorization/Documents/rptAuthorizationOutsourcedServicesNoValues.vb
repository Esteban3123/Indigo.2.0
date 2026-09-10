#Region "Imports"

Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Presentation.Base

#End Region

Public Class rptAuthorizationOutsourcedServicesNoValues
    Implements IReport

#Region "Properties"

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
            Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
            Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AuthorizationService.GetCollection(Of ViewReportAuthorizationOutsourcedServicesXpo)(Nothing, filtroConsulta)
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

    Private Sub rptAuthorizationOutsourcedServicesNoValues_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDAuthorizationOutsourcedServicesNoValuesId").Value}
            CargarDataSource()
        End If

        'Agrupar o detallar el reporte
        If INDPrTypeReport.Value = 2 Then
            ReportHeader1.Visible = False
            INDGHDetail.Visible = False
            Detail1.Visible = False
        Else
            ReportHeader1.Visible = True
            INDGHDetail.Visible = True
            Detail1.Visible = True

            If INDPrGroupServices.Value = 1 AndAlso INDPrGroupProducts.Value = 1 Then
                INDXrtHeaderDetail.Visible = False
                INDXrtHeaderDetailWithoutDate.Visible = True
                INDXrtDetail.Visible = False
                INDXrtDetailWithoutDate.Visible = True
            Else
                INDXrtHeaderDetail.Visible = True
                INDXrtHeaderDetailWithoutDate.Visible = False
                INDXrtDetail.Visible = True
                INDXrtDetailWithoutDate.Visible = False
            End If
        End If

        Dim operatingUnit = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).CommonService.ListOperatingUnitById(Convert.ToInt32(GetCurrentColumnValue("OperatingUnitId")))
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit & " - Dirección: " & operatingUnit(0).Address & " - Teléfono: " & operatingUnit(0).Phone & " - Código IPS: " & operatingUnit(0).IPSCode
        INDPrValueInLetters.Value = Utils.Num2Text(Convert.ToDecimal(GetCurrentColumnValue("ThirdPartySalesPrice"))).ToString & " PESOS M/Cte."
        INDUserImp.Text = IndigoSessionValues.UserIndigo
    End Sub

    Private Sub XrTable7_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles XrTable7.BeforePrint
        Dim table As XRTable = (CType(sender, XRTable))
        If DetailReport.GetCurrentColumnValue("SurgicalId") Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

#End Region

End Class