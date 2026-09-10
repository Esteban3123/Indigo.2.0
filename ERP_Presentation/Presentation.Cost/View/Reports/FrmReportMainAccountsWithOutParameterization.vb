' Assembly         : Presentation.Portfolio
' Author           : Dayan Mauricio Sabi
' Created          : 09-03-2020
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Cost.MVP
#End Region


Public Class FrmReportMainAccountsWithOutParameterization
    Implements IReportMainAccountsWithOutParameterization

#Region "Variables"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PReportMainAccountsWithOutParameterization
#End Region

#Region "Properties"


    Public ReadOnly Property GetYearPeriod As Integer Implements IReportMainAccountsWithOutParameterization.GetYearPeriod
        Get
            Return INDDnPeriod.GetYear
        End Get
    End Property


    Public ReadOnly Property GetMonthPeriod As Integer Implements IReportMainAccountsWithOutParameterization.GetMonthPeriod
        Get
            Return INDDnPeriod.GetMonth
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#Region "ToExcel"

    Private Sub chargueDatasourceMainAccountsWithOutParameterization()
        INDGcExportExcell.DataSource = _presenter.LoadDatasourceMainAccountWithOutParameterization(GetMonthPeriod, GetYearPeriod)
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        '_gridView.MainView.PopulateColumns()
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcell.DataSource = Nothing
        Me.INDGcExportExcell.RefreshDataSource()
    End Sub

#End Region
#End Region

#Region "Load"
    ''' <summary>
    ''' se ejecuta al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportMainAccountsWithOutParameterization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia
        _presenter = New PReportMainAccountsWithOutParameterization()
        _presenter.LoadDatasourceMainAccountWithOutParameterization(GetMonthPeriod, GetYearPeriod)
    End Sub

#End Region

#Region "Report"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Try
            AsyncLoader(True)
            Await Task.Factory.StartNew(AddressOf chargueDatasourceMainAccountsWithOutParameterization)

            If Me.INDGcExportExcell.DataSource IsNot Nothing AndAlso INDGcExportExcell.DataSource.Count > 0 Then
                generateExcel()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        Try
            AsyncLoader(True)
            Await Task.Factory.StartNew(AddressOf chargueDatasourceMainAccountsWithOutParameterization)

            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub
#End Region

End Class