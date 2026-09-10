#Region "Imports"
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmReportCashFlow

#Region "properties"
    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Public Property XpoCashFlowConcept As XPInstantFeedbackSource

    Private _FillingReportType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingReportType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReportType Is Nothing Then
                _FillingReportType = New List(Of Tuple(Of Integer, String))
                _FillingReportType.Add(New Tuple(Of Integer, String)(1, "Flujo de caja"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(2, "Flujo de efectivo detallado"))
                _FillingReportType.Add(New Tuple(Of Integer, String)(3, "Flujo de efectivo resumido"))
            End If
            Return _FillingReportType
        End Get
    End Property

    Private _FillingType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingType As List(Of Tuple(Of Integer, String))
        Get
            If _FillingType Is Nothing Then
                _FillingType = New List(Of Tuple(Of Integer, String))
                _FillingType.Add(New Tuple(Of Integer, String)(1, "Ingreso"))
                _FillingType.Add(New Tuple(Of Integer, String)(2, "Egreso"))
            End If
            Return _FillingType
        End Get
    End Property

    Private _FillingActivity As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingActivity As List(Of Tuple(Of Integer, String))
        Get
            If _FillingActivity Is Nothing Then
                _FillingActivity = New List(Of Tuple(Of Integer, String))
                _FillingActivity.Add(New Tuple(Of Integer, String)(1, "Inversión"))
                _FillingActivity.Add(New Tuple(Of Integer, String)(2, "Operación"))
                _FillingActivity.Add(New Tuple(Of Integer, String)(3, "Financiación"))
            End If
            Return _FillingActivity
        End Get
    End Property

    Private _cashFlowConceptId As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CashFlowConceptId As String
        Get
            Return _cashFlowConceptId
        End Get
        Set(value As String)
            _cashFlowConceptId = value
        End Set
    End Property

    Private _typeId As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeId As String
        Get
            Return _typeId
        End Get
        Set(value As String)
            _typeId = value
        End Set
    End Property

    Private _activityId As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ActivityId As String
        Get
            Return _activityId
        End Get
        Set(value As String)
            _activityId = value
        End Set
    End Property


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
#End Region

#Region "events"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _FillingReportType = Nothing
        _FillingType = Nothing
        _FillingActivity = Nothing
        XpoCashFlowConcept = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportCashFlow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Cargar GridLookUpEdit
        Me.INDGleReportType.Properties.DataSource = FillingReportType
        Me.INDGleType.Properties.DataSource = FillingType
        Me.INDGleActivity.Properties.DataSource = FillingActivity
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleReportType.EditValue = 1

        INDSleCashFlowConcept.Properties.View.OptionsView.ShowGroupPanel = False
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del Control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptTreasuryCashFlow
            reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                        INDDateEnd.EditValue}
            INDDvViewReport.DocumentSource = reporte
            Dim DataSourceEmpty As Boolean = True

            reporte.XrSubreport3.ReportSource.DataSource = reporte.FillListCashFlow
            If DirectCast(reporte.XrSubreport3.ReportSource.DataSource, ICollection).Count <= 0 Then
                DataSourceEmpty = False
            End If

            reporte.CargarDataSource()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If DataSourceEmpty = True Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReportOther_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReportOther.Click
        If Me.ValidateControlsReports Then
            If INDGleReportType.EditValue = 2 Then
                AsyncLoader(True)
                Dim parametros As String = RecuperarParametros()

                Dim reporte As New rptCashFlowStatus
                reporte.ParametrosReporte = New Object() {parametros}
                INDDvViewReport.DocumentSource = reporte
                Await reporte.CargarDataSource()
                If reporte.EncontroInformacion Then
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
                Mensaje(EeventViewerImages.Advertencia) = "En construcción"
                Me.INDDateStart.Focus()

            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbExportReportOther_Click(sender As Object, e As EventArgs) Handles INDSbExportReportOther.Click
        If Me.ValidateControlsReports = True Then
            If INDGleReportType.EditValue = 2 Then
                AsyncLoader(True)
                Dim _gridControl As DevExpress.XtraGrid.GridControl
                _gridControl = Me.INDGcExportExcel
                Await CargueDatasource(_gridControl)
                If _gridControl.DataSource IsNot Nothing Then
                    GenerateExcel(_gridControl)
                End If
                AsyncLoader(False)
            Else
                Mensaje(EeventViewerImages.Advertencia) = "En construcción"
                Me.INDDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cargamos el datasource del search de conceptos de flujo de efectivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCashFlowConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCashFlowConcept.QueryPopUp
        If INDSleCashFlowConcept.Properties.DataSource Is Nothing Then
            LoadXpoCashFlowConcept()
        End If
    End Sub



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleType_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDGleType.CloseUp
        Me.TypeId = RecuperarSeleccionadosGle(sender, "Item1", "Item2")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleActivity_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDGleActivity.CloseUp
        Me.ActivityId = RecuperarSeleccionadosGle(sender, "Item1", "Item2")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConcept_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleCashFlowConcept.CloseUp
        Me.CashFlowConceptId = RecuperarSeleccionados(sender, "Id", "Code")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReportType.EditValueChanged
        If INDGleReportType.EditValue = 1 Then
            INDLcgFilterOptional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleType.EditValue = Nothing
            INDGleActivity.EditValue = Nothing
            INDSleCashFlowConcept.EditValue = Nothing
            INDLciGenerateReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Else
            INDLcgFilterOptional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciGenerateReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCashFlowConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCashFlowConcept.EditValueChanged
        If String.IsNullOrEmpty(INDSleCashFlowConcept.EditValue) Then
            CashFlowConceptId = String.Empty
            INDSleCashFlowConcept.Properties.NullText = String.Empty
            INDSleCashFlowConcept.ToolTip = String.Empty
            INDGvCashFlowConcept.ClearSelection()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleType_EditValueChanged(sender As Object, e As ButtonPressedEventArgs) Handles INDGleType.ButtonClick
        If e.Button.Kind = ButtonPredefines.Delete Then
            INDGleType.EditValue = Nothing
            Me.TypeId = String.Empty
            INDGleType.Properties.NullText = String.Empty
            INDGleType.ToolTip = String.Empty
            INDGvType.ClearSelection()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleActivity_EditValueChanged(sender As Object, e As ButtonPressedEventArgs) Handles INDGleActivity.ButtonClick
        If e.Button.Kind = ButtonPredefines.Delete Then
            INDGleActivity.EditValue = Nothing
            Me.ActivityId = String.Empty
            INDGleActivity.Properties.NullText = String.Empty
            INDGleActivity.ToolTip = String.Empty
            INDGvActivity.ClearSelection()
        End If
    End Sub
#End Region

#Region "Metodos, funciones"
    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAttentionCenter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCashFlowConcept()
        Using msearch As New MBusqueda
            XpoCashFlowConcept = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashFlowConcept)
            INDSleCashFlowConcept.Properties.DataSource = XpoCashFlowConcept
        End Using
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyField"></param>
    ''' <param name="descripcionField"></param>
    ''' <returns></returns>
    Private Function RecuperarSeleccionados(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim selectedRows As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In selectedRows
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ","
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyField"></param>
    ''' <param name="descripcionField"></param>
    ''' <returns></returns>
    Private Function RecuperarSeleccionadosGle(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim selectedRows As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In selectedRows
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ","
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function
    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDGleReportType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione tipo de reporte"
            Me.INDGleReportType.Focus()
            Validations = False
        End If
        Return Validations

    End Function

    ''' <summary>
    ''' creamos un datatable para generar el excel detallado
    ''' </summary>
    ''' <param name="_gridControl"></param>
    ''' <returns></returns>
    Private Async Function CargueDatasource(ByVal _gridControl As DevExpress.XtraGrid.GridControl) As Task


        Dim parametros As String = RecuperarParametros()
        Dim ds As DataSet = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListCashFlowStatusAsync(parametros, Me.IndigoSessionValues)
        Dim dt = ds.Tables("Treasury_SP_CashFlowStatus")

        _gridControl.DataSource = dt

        If dt Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <param name="_gridControl"></param>
    Private Sub GenerateExcel(ByVal _gridControl As DevExpress.XtraGrid.GridControl)
        Dim _gridView = _gridControl
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
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Function RecuperarParametros() As String
        Dim parametros As String = String.Format("<{0}>{1:dd/MM/yyyy hh:mm:ss}</{0}>", "InitialDate", INDDateStart.EditValue)
        parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "EndDate", INDDateEnd.EditValue)
        parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "ReportType", INDGleReportType.EditValue)
        If (Not String.IsNullOrEmpty(TypeId)) AndAlso Not INDGvType.SelectedRowsCount = INDGvType.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "CashFlowConceptType", TypeId)
        End If
        If (Not String.IsNullOrEmpty(ActivityId)) AndAlso Not INDGvActivity.SelectedRowsCount = INDGvActivity.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Activity", ActivityId)
        End If
        If (Not String.IsNullOrEmpty(CashFlowConceptId)) AndAlso Not INDGvCashFlowConcept.SelectedRowsCount = INDGvCashFlowConcept.RowCount Then
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "CashFlowConcept", CashFlowConceptId)
        End If

        parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)
        Return parametros
    End Function

#End Region

End Class