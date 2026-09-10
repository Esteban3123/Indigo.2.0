#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class FrmReportCostMeasurementUnit
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"
    Public Property ProoftCloseXpoGroup As XPCollection(Of InteropCostProductionCenterReportXpo)
    Public Property ProoftCloseXpoMeasurementUnit As XPInstantFeedbackSource


    'Private _FillingReportType As List(Of Tuple(Of Integer, String))
    'Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
    '    Get
    '        If _FillingReportType Is Nothing Then
    '            _FillingReportType = New List(Of Tuple(Of Integer, String))
    '            _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Resumido"))
    '            _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Detallado"))
    '        End If
    '        Return _FillingReportType
    '    End Get
    'End Property


    'Private _FillingDetailType As List(Of Tuple(Of Integer, String))
    'Private ReadOnly Property FillingDetailType As List(Of Tuple(Of Integer, String))
    '    Get
    '        If _FillingDetailType Is Nothing Then
    '            _FillingDetailType = New List(Of Tuple(Of Integer, String))
    '            _FillingDetailType.Add(New Tuple(Of Integer, String)(1, "Mano de Obra"))
    '            _FillingDetailType.Add(New Tuple(Of Integer, String)(2, "Ventas"))
    '            _FillingDetailType.Add(New Tuple(Of Integer, String)(3, "Suministros"))
    '            _FillingDetailType.Add(New Tuple(Of Integer, String)(4, "Consumo"))
    '            _FillingDetailType.Add(New Tuple(Of Integer, String)(5, "Gastos Generales"))
    '            _FillingDetailType.Add(New Tuple(Of Integer, String)(6, "Depreciación"))
    '        End If
    '        Return _FillingDetailType
    '    End Get
    'End Property
#End Region

#Region "Method"
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoGroupStart()
        Using msearch As New MBusqueda
            'Dim filter() As Object = {True}
            ProoftCloseXpoGroup = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).InteropCostService.ListReportCenterProductionStatusXpCollection(True)
            ProoftCloseXpoGroup.Load()
            'msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListProductionCenterByStatusCenterTypeReport, filter)
            INDSleGroupStart.Datasource = ProoftCloseXpoGroup
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitStart
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoMeasurementUnitStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoMeasurementUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMeasureUnit)
            INDSleMeasurementUnitStart.Datasource = ProoftCloseXpoMeasurementUnit
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleFunctionalUnitEnd
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadXpoMeasurementUnitEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoMeasurementUnit = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMeasureUnit)
            INDSleMeasurementUnitEnd.Datasource = ProoftCloseXpoMeasurementUnit
        End Using
    End Sub
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoGroup = Nothing
        ProoftCloseXpoMeasurementUnit = Nothing
    End Sub

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Dim GetCheckedValue = INDSleGroupStart.EditValue
        'Expresion para Obtener El Código de los Almacenes Sellecionados, para enviar y mostrarlos en el reporte
        Dim stringCenterProducction As String = String.Empty

        Dim queryCenterProduccion As String = ""
        If INDSleGroupStart.EditValue IsNot Nothing Then
            queryCenterProduccion = (From x In CType(INDSleGroupStart.Datasource, XPCollection(Of InteropCostProductionCenterReportXpo)) Where GetCheckedValue.Equals(x.Id) Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault.ToString
            'stringCenterProducction = String.Join(",", queryCenterProduccion)
        End If

        If ValidateControlsReports() = True Then

            AsyncLoader(True)
            Dim reporte As New rptListCostMeasurementUnit

            Dim DateStart As Date = "01/" & INDCdnMonthYearStart.GetMonth & "/" & INDCdnMonthYearStart.GetYear
            Dim DateEnd As Date = "01/" & INDCdnMonthYearEnd.GetMonth & "/" & INDCdnMonthYearEnd.GetYear

            reporte.ParametrosReporte = New Object() {INDCdnMonthYearStart.GetMonth,
                                                      INDCdnMonthYearStart.GetYear,
                                                      INDCdnMonthYearEnd.GetMonth,
                                                      INDCdnMonthYearEnd.GetYear,
                                                      INDSleGroupStart.EditValue,
                                                      INDSleMeasurementUnitStart.TextEditValue,
                                                      INDSleMeasurementUnitEnd.TextEditValue,
                                                      DateStart,
                                                      DateEnd,
                                                      queryCenterProduccion}

            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument()
            End If
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBase.Visible = False
                Me.INDCncReport.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDCdnMonthYearStart.Focus()
            End If
        End If
    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDPcReport.Visible = False
        Me.INDLcBase.Visible = True
        Me.INDCncReport.Visible = True
        Me.INDCdnMonthYearStart.Focus()
    End Sub


    Private Sub FrmReportCostMeasurementUnit_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Me.INDGleDetailType.Properties.DataSource = FillingDetailType
        'Me.INDGleDetailType.EditValue = 1

        'Me.INDGleReportType.Properties.DataSource = FillingReportType
        'Me.INDGleReportType.EditValue = 1
    End Sub
#End Region

#Region "Validate"
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Centro de Producción
        If INDSleGroupStart.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Centro de Producción"
            Me.INDSleGroupStart.Focus()
            Validations = False
        End If

        If INDCdnMonthYearStart.GetYear IsNot Nothing And INDCdnMonthYearEnd.GetYear IsNot Nothing Then
            If INDCdnMonthYearStart.GetYear = INDCdnMonthYearEnd.GetYear Then
                If Me.INDCdnMonthYearStart.GetMonth > INDCdnMonthYearEnd.GetMonth Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
                    Me.INDCdnMonthYearStart.Focus()
                    Validations = False
                End If
            End If
        End If

        'validaciones de rangos
        If INDSleMeasurementUnitStart.TextEditValue <> String.Empty And INDSleMeasurementUnitEnd.EditValue = String.Empty Or INDSleMeasurementUnitStart.EditValue = String.Empty And INDSleMeasurementUnitEnd.EditValue <> String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblMeasurementUnit.Text)
            Me.INDSleMeasurementUnitStart.Focus()
            Validations = False
        ElseIf INDSleMeasurementUnitStart.TextEditValue > INDSleMeasurementUnitEnd.TextEditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblMeasurementUnit.Text)
            Me.INDSleMeasurementUnitStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function


    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleGroupStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleGroupStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGroupStart.QueryPopUp
        If INDSleGroupStart.EditValue Is Nothing Then
            LoadXpoGroupStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleMeasurementUnitStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMeasurementUnitStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleMeasurementUnitStart.QueryPopUp
        If INDSleMeasurementUnitStart.EditValue Is Nothing Then
            LoadXpoMeasurementUnitStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleMeasurementUnitEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMeasurementUnitEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleMeasurementUnitEnd.QueryPopUp
        If INDSleMeasurementUnitEnd.EditValue Is Nothing Then
            LoadXpoMeasurementUnitEnd()
        End If
    End Sub
#End Region
End Class