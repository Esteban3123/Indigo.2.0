'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Andres Alarcón
' Created          : 13/03/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Reporter

Public Class PopUpCampaignBatchRecord

    Private WriteOnly Property Title As String
        Set(value As String)
            Text = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los documentos
    ''' </summary>
    Private _listDocuments As List(Of Tuple(Of Byte, String, List(Of (String, Object()))))

    ''' <summary>
    ''' Datasource de los documentos
    ''' </summary>
    Public CampaignSelected As ViewHistoricCampaingXpo

    ''' <summary>
    ''' Entidad que almacéna los movimientos de reportes
    ''' </summary>
    Dim campaingReports As CampaignReports

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>S
    Private Sub PopUpCampaignBatchRecord_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetDocuments()
    End Sub

    Private Sub SetDocuments()
        _listDocuments = New List(Of Tuple(Of Byte, String, List(Of (String, Object()))))()
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(0, "Orden de alistamiento de materiales", {(NameOf(rptProductionOrder), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(1, "Plan de adecuación", {(NameOf(rptAdecuationPlan), {CampaignSelected.CampaignDetailId, CampaignSelected.MateriaPrimaStock, CampaignSelected.Almacen, 2, CampaignSelected.CampaignNumber, CampaignSelected.CampaignStatusName, CampaignSelected.NameUnitDoseType, CampaignSelected.MSClass})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(2, "Picking de materia prima (Orden de traslado)", {(NameOf(rptSubTransferOrderByCampaign), {CType(CampaignSelected.CampaignDetailId, Object), "TransferOrder"})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(3, "Etiquetas de la campaña", {(NameOf(rptAllLabelsByCampaign), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))

        If CampaignSelected.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(4, "Ficha técnica", {(NameOf(rptNptLabelSub), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))
        End If

        If CampaignSelected.MSClass <> EUnitDoseTypeClass.Refilling And CampaignSelected.MSClass <> EUnitDoseTypeClass.Repackaging Then
            _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(5, "Rotulo de preparación medicamentosas", {(NameOf(rptReportDoseAdjustmentLabel), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))
            _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(6, "Acta de entrega de medicamentos", {(NameOf(rptReportRemissions), {CType(CampaignSelected.CampaignDetailId, Object), CampaignSelected.MSClass})}.ToList()))
        End If

        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(7, "Liberación de línea", {(NameOf(rptReleaseLine), {Nothing, CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(8, "Clasificación de defectos", {(NameOf(rptDefectClassification), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(9, "Orden de traslado Producto terminado", {(NameOf(rptSubTransferOrderByCampaign), {CType(CampaignSelected.CampaignDetailId, Object), "TransferOrderFinishedProduct"})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(13, "Liberación de Campaña", {(NameOf(rptGeneralReportByCampaign), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))
        _listDocuments.Add(New Tuple(Of Byte, String, List(Of (String, Object())))(14, "Baja de remanentes", {(NameOf(rptRemainingLow), {CType(CampaignSelected.CampaignDetailId, Object)})}.ToList()))

        INDgcBatch.DataSource = _listDocuments.ToList

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de visualizar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBPrint_Click(sender As Object, e As EventArgs) Handles INDBPrint.Click
        PrintDocuments()
    End Sub

    Private Sub PrintDocuments()
        Dim reportePrincipal As New XtraReport

        For Each i In INDviewBatchRecord.GetSelectedRows()
            For Each rpt In _listDocuments(i).Item3
                ' Manejo especial para rptAdecuationPlan - Generar múltiples páginas de lotes
                If rpt.Item1 = NameOf(rptAdecuationPlan) Then
                    GeneratePaginatedReportByBatches(reportePrincipal, rpt.Item2)
                Else
                    ' Manejo normal para otros reportes
                    Dim report As IReport = Activator.CreateInstance("Presentation.Reporter", "Presentation.Reporter." & rpt.Item1).Unwrap()
                    report.ParametrosReporte = rpt.Item2
                    report.CargarDataSource()

                    Dim xtraReport As XtraReport = CType(report, XtraReport)

                    ' Validar si el reporte tiene DataSource y datos antes de crear el documento
                    If HasDataSource(xtraReport) Then
                        xtraReport.CreateDocument()

                        ' Verificar si el documento se creó con páginas antes de agregarlo
                        If xtraReport.Pages.Count > 0 Then
                            reportePrincipal.Pages.AddRange(xtraReport.Pages)
                        End If
                    End If
                End If
            Next
        Next

        If reportePrincipal.Pages.Count > 0 Then
            reportePrincipal.PrintingSystem.ContinuousPageNumbering = True

            reportePrincipal.ShowPreviewDialog()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No hay datos disponibles para el reporte"
        End If
    End Sub

    ''' <summary>
    ''' Genera el reporte de adecuación con paginación por lotes (8 lotes por página)
    ''' </summary>
    ''' <param name="mainReport">Reporte principal donde se agregarán las páginas</param>
    ''' <param name="baseParameters">Parámetros base del reporte</param>
    Public Sub GeneratePaginatedReportByBatches(ByRef mainReport As XtraReport, baseParameters As Object())
        ' Crear un reporte temporal para obtener el número total de páginas de lotes
        Dim initialReport As IReport = Activator.CreateInstance("Presentation.Reporter", "Presentation.Reporter." & NameOf(rptAdecuationPlan)).Unwrap()

        ' Crear array de parámetros con índice de página = 0
        Dim initialParameters(baseParameters.Length) As Object
        Array.Copy(baseParameters, initialParameters, baseParameters.Length)
        initialParameters(baseParameters.Length) = 0  ' Índice de página inicial

        initialReport.ParametrosReporte = initialParameters
        initialReport.CargarDataSource()

        ' Obtener el total de páginas de lotes
        Dim totalBatchPages As Integer = 1
        If TypeOf initialReport Is rptAdecuationPlan Then
            totalBatchPages = CType(initialReport, rptAdecuationPlan).TotalPaginasLotes
        End If

        ' Generar un reporte por cada página de lotes
        For batchPage As Integer = 0 To totalBatchPages - 1
            ' Crear nueva instancia del reporte
            Dim batchReport As IReport = Activator.CreateInstance("Presentation.Reporter", "Presentation.Reporter." & NameOf(rptAdecuationPlan)).Unwrap()

            ' Crear array de parámetros con el índice de página actual
            Dim pageParameters(baseParameters.Length) As Object
            Array.Copy(baseParameters, pageParameters, baseParameters.Length)
            pageParameters(baseParameters.Length) = batchPage  ' Índice de página actual

            batchReport.ParametrosReporte = pageParameters
            batchReport.CargarDataSource()

            Dim xtraReport As XtraReport = CType(batchReport, XtraReport)

            ' Validar si el reporte tiene DataSource y datos
            If HasDataSource(xtraReport) Then
                xtraReport.CreateDocument()

                ' Agregar las páginas al reporte principal
                If xtraReport.Pages.Count > 0 Then
                    mainReport.Pages.AddRange(xtraReport.Pages)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Valida si el reporte tiene DataSource y datos disponibles
    ''' </summary>
    ''' <param name="report">Reporte a validar</param>
    ''' <returns>True si el reporte tiene DataSource y datos, False en caso contrario</returns>
    Private Function HasDataSource(report As XtraReport) As Boolean
        If report Is Nothing Then
            Return False
        End If

        ' Verificar si el reporte tiene DataSource asignado
        If report.DataSource Is Nothing Then
            Return False
        End If

        Dim prueba = report.DataSource.GetType
        If TypeOf (report.DataSource) Is IEnumerable Then
            For Each item In report.DataSource
                Return True
            Next

            Return False
        ElseIf TypeOf (report.DataSource) Is DataTable Then
            For Each item In CType(report.DataSource, DataTable).Rows
                Return True
            Next

            Return False
        End If

        Return True
    End Function
End Class