#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.Base

#End Region

Public Class rptReportCertifiedResponsibleForFixedAssets
    Implements IReport
    Implements IReportAsync

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Dim criterias As Dictionary(Of String, String)

    Dim filters As Dictionary(Of String, String)

    Dim dtReportResponsibleForFixedAssets As DataTable

    ''' <summary>
    ''' Variable que almacena el tipo de identificacion del tercero
    ''' </summary>
    Private IdentificationType As String

    ''' <summary>
    ''' Variable para almacena el NIT del tercero
    ''' </summary>
    Private ThirdPartyNit As String

    ''' <summary>
    ''' Variable que almacena el nombre del terceo
    ''' </summary>
    Private ThirdPartyName As String

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

#End Region

#Region "Load Data"

    Public Sub CargarDataSource() Implements IReport.CargarDataSource

    End Sub

    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync
        Try
            criterias = ParametrosReporte(0)
            filters = ParametrosReporte(1)

            Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetReportResponsibleForFixedAssetsAsync(criterias, filters, Me.IndigoSessionValues)
            If ds IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                dtReportResponsibleForFixedAssets = ds.Tables("ReportResponsibleForFixedAssets")
                IdentificationType = dtReportResponsibleForFixedAssets.Rows.Item(1).ItemArray.GetValue(0)
                ThirdPartyNit = dtReportResponsibleForFixedAssets.Rows.Item(1).ItemArray.GetValue(1)
                ThirdPartyName = dtReportResponsibleForFixedAssets.Rows.Item(1).ItemArray.GetValue(2)
                Me.DataSource = dtReportResponsibleForFixedAssets
                Me.DataMember = "ReportResponsibleForFixedAssets"
            Else
                Me.DataSource = Nothing
            End If
        Catch ex As Exception
            MessageIndigo.Show(GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            Me.DataSource = Nothing
        End Try
    End Function

#End Region

#Region "Methods"

    Private Sub rptListRadicatedInvoice_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        Me.INDLblCompany.Text = " EL AREA DE ACTIVOS FIJOS DE " & IndigoSessionValues.IndigoCompanyName.ToUpper
        Me.INDLblText.Text = "Que con corte de " & filters.Item("CutOffDate") & " el (la) señor(a) " & ThirdPartyName.ToUpper & " , identificado con " & IdentificationType & "  No " & ThirdPartyNit & ", tiene asignados los siguientes activos fijos que se encuentran bajo su responsabilidad: "
        Me.INDLblExpiditionDate.Text = INDLblExpiditionDate.Text & filters.Item("DocumentDate")
        Me.INDLblUserPrint.Text = INDLblUserPrint.Text & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
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