#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
#End Region

Public Class FrmReportListFixedAssetTransfer
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoResponsible As XPInstantFeedbackSource
    Public Property ProoftCloseXpoClassification As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStatusAsset As XPInstantFeedbackSource
    Public Property ProoftCloseXpoEquipmentType As XPInstantFeedbackSource
    Public Property ProoftCloseXpoLocation As XPCollection

    Private List As List(Of FixedAssetTransferDetailReportXpo)
    Private tipTrasl As String
    Private Transfer As String
    Private SourceResponsiblId As String
    Private SourceLocatioId As String
    Private TargetLocatioId As String
    Private TargetResponsiblId As String

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private _FillingState As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingState As List(Of Tuple(Of Integer, String))
        Get
            If _FillingState Is Nothing Then
                _FillingState = New List(Of Tuple(Of Integer, String))
                _FillingState.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingState.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingState.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingState.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingState
        End Get
    End Property

    Private _TypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property TypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _TypeReport Is Nothing Then
                _TypeReport = New List(Of Tuple(Of Integer, String))
                _TypeReport.Add(New Tuple(Of Integer, String)(1, "Resumido"))
                _TypeReport.Add(New Tuple(Of Integer, String)(2, "Detallado"))
            End If
            Return _TypeReport
        End Get
    End Property

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'Valida Fecha
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If


        'Valida Responsable
        If INDSleResponsibleStart.EditValue Is Nothing And INDSleResponsibleEnd.EditValue IsNot Nothing Or INDSleResponsibleEnd.EditValue Is Nothing And INDSleResponsibleStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblPlate.Text)
            Me.INDSleResponsibleStart.Focus()
            Validations = False
        ElseIf INDSleResponsibleEnd.EditValue < INDSleResponsibleStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPlate.Text)
            Me.INDSleResponsibleStart.Focus()
            Validations = False
        End If

        'Valida Location
        If INDSleLocationStart.EditValue Is Nothing And INDSleLocationEnd.EditValue IsNot Nothing Or INDSleLocationEnd.EditValue Is Nothing And INDSleLocationStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblLocation.Text)
            Me.INDSleLocationStart.Focus()
            Validations = False
        ElseIf INDSleLocationEnd.EditValue < INDSleLocationStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblLocation.Text)
            Me.INDSleLocationStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
#End Region

#Region "Eventos"
    ''' <summary>
    ''' cargamos el datasource de Placa Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleResponsibleStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsibleStart.QueryPopUp
        If INDSleResponsibleStart.Datasource Is Nothing Then
            LoadXpoResponsibleStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleResponsibleEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsibleEnd.QueryPopUp
        If INDSleResponsibleEnd.Datasource Is Nothing Then
            LoadXpoResponsibleEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Ubicación Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocationStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLocationStart.QueryPopUp
        If INDSleLocationStart.Datasource Is Nothing Then
            LoadXpoLocationStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Ubicación Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleLocationEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleLocationEnd.QueryPopUp
        If INDSleLocationEnd.Datasource Is Nothing Then
            LoadXpoLocationEnd()
        End If
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoResponsible = Nothing
        ProoftCloseXpoClassification = Nothing
        ProoftCloseXpoStatusAsset = Nothing
        ProoftCloseXpoEquipmentType = Nothing
        ProoftCloseXpoLocation = Nothing
        List = Nothing
        tipTrasl = Nothing
        Transfer = Nothing
        SourceResponsiblId = Nothing
        SourceLocatioId = Nothing
        TargetLocatioId = Nothing
        TargetResponsiblId = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Me.INDGcExportExcel.DataSource = chargeDataSource()
            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

    Private Function chargeDataSource() As DataTable

        Dim filtroConsulta As String = Nothing

        'filtro por fechas
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "FixedAssetTransferId.DocumentDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND FixedAssetTransferId.DocumentDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"
        End If

        'filtro por Estado
        If INDGleStatus.EditValue <> 4 Then
            filtroConsulta &= " AND FixedAssetTransferId.Status = " & INDGleStatus.EditValue
        End If

        'filtro por Responsable
        If INDSleResponsibleStart.EditValue IsNot Nothing And INDSleResponsibleEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND FixedAssetTransferId.SourceResponsibleId.ThirdPartyId.Nit >= '" & INDSleResponsibleStart.EditValue & "' AND FixedAssetTransferId.TargetResponsibleId.ThirdPartyId.Nit <= '" & INDSleResponsibleEnd.EditValue & "'"
        End If

        'filtro por Ubicación
        If INDSleLocationStart.EditValue IsNot Nothing And INDSleLocationEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND FixedAssetTransferId.SourceLocationId.Code >= '" & INDSleLocationStart.EditValue & "' AND FixedAssetTransferId.TargetLocationId.Code <= '" & INDSleLocationEnd.EditValue & "'"
        End If

        List = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetTransferDetailReportXpo)(Nothing, filtroConsulta)


        Dim dt As New DataTable
        With dt.Columns
            .Add("Código")
            .Add("Fecha")
            .Add("Placa")
            .Add("Artículo")
            .Add("Tipo Traslado")
            .Add("Ubicación Origen")
            .Add("Ubicación Destino")
            .Add("Responsable Origen")
            .Add("Responsable Destino")
            .Add("Estado")
            .Add("Usuario")

            Dim columValue As DataColumn = New DataColumn
            columValue.DataType = System.Type.GetType("System.Decimal")
            columValue.AllowDBNull = False
            columValue.Caption = "Total"
            columValue.ColumnName = "Total"
            .Add(columValue)
        End With

        For Each ItemView In List

            Select Case ItemView.FixedAssetTransferId.TransferType
                Case 1
                    tipTrasl = "Ubicación"
                Case 2
                    tipTrasl = "Responsable"
                Case Else
                    tipTrasl = "Ubicación y Responsable"
            End Select

            Select Case ItemView.FixedAssetTransferId.Status
                Case 1
                    Transfer = "Registrado"
                Case 2
                    Transfer = "Confirmado"
                Case Else
                    Transfer = "Anulado"
            End Select


            If ItemView.FixedAssetTransferId.SourceResponsibleId Is Nothing Then
                SourceResponsiblId = ""
            Else
                SourceResponsiblId = ItemView.FixedAssetTransferId.SourceResponsibleId.ThirdPartyId.NitName
            End If

            If ItemView.FixedAssetTransferId.TargetResponsibleId Is Nothing Then
                TargetResponsiblId = ""
            Else
                TargetResponsiblId = ItemView.FixedAssetTransferId.TargetResponsibleId.ThirdPartyId.NitName
            End If

            If ItemView.FixedAssetTransferId.SourceLocationId Is Nothing Then
                SourceLocatioId = ""
            Else
                SourceLocatioId = ItemView.FixedAssetTransferId.SourceLocationId.Code & " - " & ItemView.FixedAssetTransferId.SourceLocationId.Name
            End If

            If ItemView.FixedAssetTransferId.TargetLocationId Is Nothing Then
                TargetLocatioId = ""
            Else
                TargetLocatioId = ItemView.FixedAssetTransferId.TargetLocationId.Code & " - " & ItemView.FixedAssetTransferId.TargetLocationId.Name
            End If


            Dim row As DataRow = dt.NewRow()
            With row
                .Item("Código") = ItemView.FixedAssetTransferId.Code
                .Item("Fecha") = ItemView.FixedAssetTransferId.DocumentDate
                .Item("Placa") = ItemView.PhysicalAssetId.Plate
                .Item("Artículo") = ItemView.PhysicalAssetId.ItemId.Code & " - " & ItemView.PhysicalAssetId.ItemId.Description
                .Item("Tipo Traslado") = tipTrasl
                .Item("Ubicación Origen") = SourceLocatioId
                .Item("Ubicación Destino") = TargetLocatioId
                .Item("Responsable Origen") = SourceResponsiblId
                .Item("Responsable Destino") = TargetResponsiblId
                .Item("Estado") = Transfer
                .Item("Usuario") = ItemView.FixedAssetTransferId.CreationUser
                .Item("Total") = ItemView.PhysicalAssetId.HistoricalValue
            End With

            dt.Rows.Add(row)
        Next
        AsyncLoader(False)
        If dt IsNot Nothing Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcel
        If _gridView IsNot Nothing Then
            Dim fileName As String = System.IO.Path.GetTempFileName() & ".xlsx"
            Dim param As New DevExpress.XtraPrinting.XlsxExportOptions(DevExpress.XtraPrinting.TextExportMode.Value, True, False)
            _gridView.ExportToXlsx(fileName, param)
            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        End If
        Me.INDGcExportExcel.DataSource = Nothing
        Me.INDGcExportExcel.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptListFixedAssetTransfer
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleStatus.EditValue,
                                                          INDSleResponsibleStart.EditValue,
                                                          INDSleResponsibleEnd.EditValue,
                                                          INDSleLocationStart.EditValue,
                                                          INDSleLocationEnd.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
                AsyncLoader(False)
            Else
                AsyncLoader(True)
                Dim reporte As New rptSubTransfer
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleStatus.EditValue,
                                                          INDSleResponsibleStart.EditValue,
                                                          INDSleResponsibleEnd.EditValue,
                                                          INDSleLocationStart.EditValue,
                                                          INDSleLocationEnd.EditValue,
                                                          BarraBotones.OperatingUnit.Id}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
                AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
        Me.INDDateStart.Focus()
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportListFixedAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleSubAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleSubAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleGroupEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode


        'Cargar GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingState
        Me.INDGleTypeReport.Properties.DataSource = TypeReport

        'Dar un valor por defecto a los GridLookEdit
        INDGleStatus.EditValue = 4
        INDGleTypeReport.EditValue = 1

        INDSleResponsibleStart.View.OptionsView.ShowGroupPanel = False
        INDSleResponsibleEnd.View.OptionsView.ShowGroupPanel = False
        INDSleLocationStart.View.OptionsView.ShowGroupPanel = False
        INDSleLocationEnd.View.OptionsView.ShowGroupPanel = False
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleResponsibleStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoResponsibleStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoResponsible = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            INDSleResponsibleStart.Datasource = ProoftCloseXpoResponsible
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleResponsibleEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoResponsibleEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoResponsible = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetResponsible)
            INDSleResponsibleEnd.Datasource = ProoftCloseXpoResponsible
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLocationStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLocationStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleLocationStart.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleLocationEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoLocationEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoLocation = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetLocation)
            INDSleLocationEnd.Datasource = ProoftCloseXpoLocation
        End Using
    End Sub
#End Region
End Class