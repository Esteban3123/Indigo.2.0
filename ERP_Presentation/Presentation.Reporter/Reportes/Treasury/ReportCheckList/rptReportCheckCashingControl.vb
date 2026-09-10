#Region "Librerias Importadas"
Imports System.Xml
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
#End Region

Public Class rptReportCheckCashingControl
    Implements IReport
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance


    Private _xmlDoc As XmlDocument
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property XmlDoc() As XmlDocument
        Get
            Return _xmlDoc
        End Get
        Set(ByVal value As XmlDocument)
            _xmlDoc = value
        End Set
    End Property

    Private _encontroInformacion As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property EncontroInformacion() As Boolean
        Get
            Return _encontroInformacion
        End Get
        Set(ByVal value As Boolean)
            _encontroInformacion = value
        End Set
    End Property

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte
    Public Sub CargarImagenes() Implements IReport.CargarImagenes
    End Sub
    Private Async Sub IReport_CargarDataSource() Implements IReport.CargarDataSource
        Await CargarDataSource()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Public Async Function CargarDataSource() As Task

        Dim parameters As String = ParametrosReporte(0)
        XmlDoc = New XmlDocument
        XmlDoc.LoadXml(parameters)
        Dim Resultado As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCheckCashingControlAsync(parameters, Me.IndigoSessionValues)
        If Resultado IsNot Nothing AndAlso Resultado.Tables.Count > 0 AndAlso Resultado.Tables(0).Rows.Count > 0 Then
            Me.EncontroInformacion = True
            Me.DataSource = Resultado
        End If
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub rptReporteAuditoriaHC_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Logica para mostrar reporte resumido.


        Dim esC As Globalization.CultureInfo = New Globalization.CultureInfo("es-ES")

        'Cargar los valores del titulo.
        Me.INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        Me.INDLblNitCompany.Text = "Nit : " & IndigoSessionValues.IndigoCompanyNit
        Me.INDLblUserPrint.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub
End Class