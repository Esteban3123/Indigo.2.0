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

Public Class FrmReportListFixedAssetActiveOutput
#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon
#End Region

#Region "Properties"
    Public Property ProoftCloseXpoDocument As XPInstantFeedbackSource
    Public Property ProoftCloseXpoItem As XPInstantFeedbackSource
    Public Property ProoftCloseXpoStatusAsset As XPInstantFeedbackSource
    Public Property ProoftCloseXpoPlate As XPInstantFeedbackSource

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private a As List(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)

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

    Private _ActiveType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property ActiveType As List(Of Tuple(Of Integer, String))
        Get
            If _ActiveType Is Nothing Then
                _ActiveType = New List(Of Tuple(Of Integer, String))
                _ActiveType.Add(New Tuple(Of Integer, String)(1, "Activo"))
                _ActiveType.Add(New Tuple(Of Integer, String)(2, "Parte"))
                _ActiveType.Add(New Tuple(Of Integer, String)(3, "Todos"))
            End If
            Return _ActiveType
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

        'Valida Documento
        If INDSleDocumentStart.EditValue Is Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Or INDSleDocumentEnd.EditValue Is Nothing And INDSleDocumentStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblDocument.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        ElseIf INDSleDocumentEnd.EditValue < INDSleDocumentStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblDocument.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        End If


        'Valida Responsable
        If INDSlePlateStart.EditValue Is Nothing And INDSlePlateEnd.EditValue IsNot Nothing Or INDSlePlateEnd.EditValue Is Nothing And INDSlePlateStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        ElseIf INDSlePlateEnd.EditValue < INDSlePlateStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblPlate.Text)
            Me.INDSlePlateStart.Focus()
            Validations = False
        End If

        'Valida Artículo
        If INDSleItemStart.EditValue Is Nothing And INDSleItemEnd.EditValue IsNot Nothing Or INDSleItemEnd.EditValue Is Nothing And INDSleItemStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
            Validations = False
        ElseIf INDSleItemEnd.EditValue < INDSleItemStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblItem.Text)
            Me.INDSleItemStart.Focus()
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
    ''' cargamos el datasource de Documento Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentStart.QueryPopUp
        If INDSleDocumentStart.Datasource Is Nothing Then
            LoadXpoDocumentStart()
        End If
    End Sub

    ''' <summary>
    ''' cargamos el datasource de Documento Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleDocumentEnd.QueryPopUp
        If INDSleDocumentEnd.Datasource Is Nothing Then
            LoadXpoDocumentEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateStart.QueryPopUp
        If INDSlePlateStart.Datasource Is Nothing Then
            LoadXpoPlateStart()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Placa Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSlePlateEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlePlateEnd.QueryPopUp
        If INDSlePlateEnd.Datasource Is Nothing Then
            LoadXpoPlateEnd()
        End If
    End Sub


    ''' <summary>
    ''' cargamos el datasource de Artículo Inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>    ''' 
    Private Sub INDSleItemStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemStart.QueryPopUp
        If INDSleItemStart.Datasource Is Nothing Then
            LoadXpoItemStart()
        End If
    End Sub
   

    ''' <summary>
    ''' cargamos el datasource de Artículo Final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleItemEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleItemEnd.QueryPopUp
        If INDSleItemEnd.Datasource Is Nothing Then
            LoadXpoItemEnd()
        End If
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoDocument = Nothing
        ProoftCloseXpoItem = Nothing
        ProoftCloseXpoStatusAsset = Nothing
        ProoftCloseXpoPlate = Nothing
        a = Nothing
        _FillingState = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReports_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReports.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Me.INDGcExportExcel.DataSource = chargueDatasource()
            If Me.INDGcExportExcel.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable

        Dim filtroConsulta As String = Nothing

        'filtro por fechas
        If INDDateStart.EditValue IsNot Nothing And INDDateEnd.EditValue IsNot Nothing Then
            filtroConsulta &= "FixedAssetActiveOutputId.DocumentDate >= #" & Format(INDDateStart.EditValue, "yyyy-MM-dd") & "# AND FixedAssetActiveOutputId.DocumentDate <= #" & Format(INDDateEnd.EditValue, "yyyy-MM-dd") & "#"
        End If

        'filtro por Tipo de Activo
        If INDGleTypeActive.EditValue <> 3 Then
            filtroConsulta &= " AND ActiveType = " & INDGleTypeActive.EditValue
        End If

        'filtro por Estado
        If INDGleStatus.EditValue <> 4 Then
            filtroConsulta &= " AND FixedAssetActiveOutputId.Status = " & INDGleStatus.EditValue
        End If

        'filtro por Documento
        If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND FixedAssetActiveOutputId.Code >= '" & INDSleDocumentStart.EditValue & "' AND FixedAssetActiveOutputId.Code <= '" & INDSleDocumentEnd.EditValue & "'"
        End If

        'filtro por Placa
        If INDSlePlateStart.EditValue IsNot Nothing And INDSlePlateEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND PhysicalAssetId.Plate >= '" & INDSlePlateStart.EditValue & "' AND PhysicalAssetId.Plate <= '" & INDSlePlateEnd.EditValue & "'"
        End If

        'filtro por Artículo
        If INDSleItemStart.EditValue IsNot Nothing And INDSleItemEnd.EditValue IsNot Nothing Then
            filtroConsulta &= " AND PhysicalAssetId.ItemId.Code >= '" & INDSleItemStart.EditValue & "' AND PhysicalAssetId.ItemId.Code <= '" & INDSleItemEnd.EditValue & "'"
        End If

        a = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetFixedAssetActiveOutputDetailReportXpo)(Nothing, filtroConsulta)


        Dim dt As New DataTable
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("Artículo")
        dt.Columns.Add("Tipo Activo")
        dt.Columns.Add("Placa")
        dt.Columns.Add("Parte Activo")
        dt.Columns.Add("Tipo Salida")
        dt.Columns.Add("Tipo Baja")
        dt.Columns.Add("Cuenta")
        dt.Columns.Add("Responsable")
        dt.Columns.Add("Estado")
        dt.Columns.Add("Observaciones")


        Dim columValueInvoice As DataColumn = New DataColumn
        columValueInvoice.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice.AllowDBNull = False
        columValueInvoice.Caption = "Valor Venta"
        columValueInvoice.ColumnName = "Valor Venta"
        dt.Columns.Add(columValueInvoice)

        Dim columValueInvoice2 As DataColumn = New DataColumn
        columValueInvoice2.DataType = System.Type.GetType("System.Decimal")
        columValueInvoice2.AllowDBNull = False
        columValueInvoice2.Caption = "Valor Historico"
        columValueInvoice2.ColumnName = "Valor Historico"
        dt.Columns.Add(columValueInvoice2)

        For Each itemView In a
            Dim ActiveTypeValue As String
            If itemView.ActiveType = 1 Then
                ActiveTypeValue = "Activo"
            Else
                ActiveTypeValue = "Parte"
            End If

            Dim OutputTypeValue As String
            If itemView.OutputType = 1 Then
                OutputTypeValue = "Baja"
            Else
                OutputTypeValue = "Venta"
            End If

            Dim LowTypeValue As String
            LowTypeValue = ""
            If itemView.LowType = 0 Then
                LowTypeValue = "No Aplica"
            ElseIf itemView.LowType = 1 Then
                LowTypeValue = "Perdida"
            ElseIf itemView.LowType = 2 Then
                LowTypeValue = "Siniestro"
            ElseIf itemView.LowType = 3 Then
                LowTypeValue = "Perdida Reposicion (Habilita Precio de venta)"
            ElseIf itemView.LowType = 4 Then
                LowTypeValue = "Bienes Inservibles"
            End If

            Dim StatusValue As String
            StatusValue = ""
            If itemView.FixedAssetActiveOutputId.Status = 1 Then
                StatusValue = "Registrado"
            ElseIf itemView.FixedAssetActiveOutputId.Status = 2 Then
                StatusValue = "Confirmado"
            ElseIf itemView.FixedAssetActiveOutputId.Status = 3 Then
                StatusValue = "Anulado"
            End If

            Dim Item As String
            If itemView.PhysicalAssetId Is Nothing Then
                Item = ""
            Else
                Item = itemView.PhysicalAssetId.ItemId.Code & " - " & itemView.PhysicalAssetId.ItemId.Description
            End If

            Dim Part As String
            If itemView.PhysicalAssetPartsId Is Nothing Then
                Part = ""
            Else
                Part = itemView.PhysicalAssetPartsId.PhysicalAssetId.ItemId.Code & " - " & itemView.PhysicalAssetPartsId.PhysicalAssetId.ItemId.Description
            End If

            Dim Responsable As String
            If itemView.ThirdPartyId Is Nothing Then
                Responsable = ""
            Else
                Responsable = itemView.ThirdPartyId.NitName
            End If

            Dim HistoricalValue As String
            If itemView.PhysicalAssetId Is Nothing Then
                HistoricalValue = itemView.PhysicalAssetPartsId.HistoricalValue
            Else
                HistoricalValue = itemView.PhysicalAssetId.HistoricalValue
            End If

            Dim row As DataRow = dt.NewRow()
            row.Item("Código") = itemView.FixedAssetActiveOutputId.Code
            row.Item("Fecha") = itemView.FixedAssetActiveOutputId.DocumentDate
            row.Item("Artículo") = Item
            row.Item("Tipo Activo") = ActiveTypeValue
            row.Item("Placa") = itemView.PhysicalAssetId.Plate
            row.Item("Parte Activo") = Part
            row.Item("Tipo Salida") = OutputTypeValue
            row.Item("Tipo Baja") = LowTypeValue
            row.Item("Cuenta") = itemView.MainAccountId.Number
            row.Item("Responsable") = Responsable
            row.Item("Estado") = StatusValue
            row.Item("Valor Venta") = itemView.SalesValue
            row.Item("Valor Historico") = HistoricalValue
            row.Item("Observaciones") = itemView.FixedAssetActiveOutputId.Observation

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
                Dim reporte As New rptListFixedAssetActiveOutput
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleTypeActive.EditValue,
                                                          INDGleStatus.EditValue,
                                                          INDSleDocumentStart.EditValue,
                                                          INDSleDocumentEnd.EditValue,
                                                          INDSlePlateStart.EditValue,
                                                          INDSlePlateEnd.EditValue,
                                                          INDSleItemStart.EditValue,
                                                          INDSleItemEnd.EditValue}

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
                Dim reporte As New rptSubActiveOutput
                reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                          INDDateEnd.EditValue,
                                                          INDGleTypeActive.EditValue,
                                                          INDGleStatus.EditValue,
                                                          INDSleDocumentStart.EditValue,
                                                          INDSleDocumentEnd.EditValue,
                                                          INDSlePlateStart.EditValue,
                                                          INDSlePlateEnd.EditValue,
                                                          INDSleItemStart.EditValue,
                                                          INDSleItemEnd.EditValue,
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
    Private Sub FrmReportListFixedAssetActiveOutput_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        'Me.INDSleSubAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleSubAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        'Me.INDSleGroupStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode
        'Me.INDSleGroupEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetFunctionalUnitByCode


        'Cargar GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingState
        Me.INDGleTypeReport.Properties.DataSource = TypeReport
        Me.INDGleTypeActive.Properties.DataSource = ActiveType

        'Dar un valor por defecto a los GridLookEdit
        INDGleStatus.EditValue = 4
        INDGleTypeReport.EditValue = 1
        INDGleTypeActive.EditValue = 3

        INDSlePlateStart.View.OptionsView.ShowGroupPanel = False
        INDSlePlateEnd.View.OptionsView.ShowGroupPanel = False
        INDSleItemStart.View.OptionsView.ShowGroupPanel = False
        INDSleItemEnd.View.OptionsView.ShowGroupPanel = False
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoDocument = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetActiveOutput)
            INDSleDocumentStart.Datasource = ProoftCloseXpoDocument
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoDocument = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetActiveOutput)
            INDSleDocumentEnd.Datasource = ProoftCloseXpoDocument
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateStart.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSlePlateEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoPlateEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoPlate = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset)
            INDSlePlateEnd.Datasource = ProoftCloseXpoPlate
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemStart.Datasource = ProoftCloseXpoItem
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleItemEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoItemEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoItem = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetEquipment)
            INDSleItemEnd.Datasource = ProoftCloseXpoItem
        End Using
    End Sub
#End Region
End Class