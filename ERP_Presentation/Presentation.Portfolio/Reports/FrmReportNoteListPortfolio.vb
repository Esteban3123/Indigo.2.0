#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP





#End Region

Public Class FrmReportNoteListPortfolio

#Region "Globals"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Properties"

    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoNotes As XPInstantFeedbackSource
    Public Property ProoftCloseXpoConceptNote() As XPInstantFeedbackSource

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Listado Notas"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Documento Notas"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingNatureReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingNatureReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingNatureReport Is Nothing Then
                _FillingNatureReport = New List(Of Tuple(Of Integer, String))
                _FillingNatureReport.Add(New Tuple(Of Integer, String)(1, "Débito"))
                _FillingNatureReport.Add(New Tuple(Of Integer, String)(2, "Crédito"))
                _FillingNatureReport.Add(New Tuple(Of Integer, String)(3, "Todas"))
            End If
            Return _FillingNatureReport
        End Get
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

#Region "XPO"

    ''' <summary>
    ''' metodo para Cargar todo el data Concepto de Notas INDSleConceptStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoConceptNote = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.PortfolioNoteConcept)
            INDSleConceptStart.Properties.DataSource = ProoftCloseXpoConceptNote
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar todo el data Concepto de Notas INDSleConceptEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoConceptEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoConceptNote = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.PortfolioNoteConcept)
            INDSleConceptEnd.Properties.DataSource = ProoftCloseXpoConceptNote
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportPortfolio)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReportPortfolio)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentStart()
        Dim criteria As String = Nothing
        'filtro por estado
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If

        'filtro por naturaleza
        If (INDGleNatureReport.EditValue IsNot Nothing And INDGleNatureReport.EditValue <> 3) Then
            If (criteria Is Nothing) Then
                criteria = "Nature = " & INDGleNatureReport.EditValue
            Else
                criteria &= " AND Nature = " & INDGleNatureReport.EditValue
            End If
        End If


        ' filtro por tercero
        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "CustomerId.ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND CustomerId.ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND CustomerId.ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND CustomerId.ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoNotes = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListNoteReportPortfolioByFilter, criteria)
            INDSleDocumentStart.Datasource = ProoftCloseXpoNotes
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleDocumentEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoDocumentEnd()
        Dim criteria As String = Nothing
        'filtro por estado
        If (INDGleStatus.EditValue IsNot Nothing And INDGleStatus.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatus.EditValue
        End If

        'filtro por naturaleza
        If (INDGleNatureReport.EditValue IsNot Nothing And INDGleNatureReport.EditValue <> 3) Then
            If (criteria Is Nothing) Then
                criteria = "Nature = " & INDGleNatureReport.EditValue
            Else
                criteria &= " AND Nature = " & INDGleNatureReport.EditValue
            End If
        End If


        ' filtro por tercero
        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "CustomerId.ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND CustomerId.ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND CustomerId.ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND CustomerId.ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If
        Using msearch As New MBusqueda
            ProoftCloseXpoNotes = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListNoteReportPortfolioByFilter, criteria)
            INDSleDocumentEnd.Datasource = ProoftCloseXpoNotes
        End Using
    End Sub

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Events"

#Region "Form Events"

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportNoteListPortfolio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatus.Properties.DataSource = FillingStatus
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleNatureReport.Properties.DataSource = FillingNatureReport

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatus.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleNatureReport.EditValue = 3

        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleDocumentStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPortfolioNoteByCode
        Me.INDSleDocumentEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPortfolioNoteByCode
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
        ProoftCloseXpoNotes = Nothing
        ProoftCloseXpoThirdParty = Nothing
        ProoftCloseXpoConceptNote = Nothing
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del Concept Note INDSleConceptStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConceptStart.QueryPopUp
        If INDSleConceptStart.Properties.DataSource Is Nothing Then
            LoadXpoConceptStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del Concept Note INDSleConceptEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConceptEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleConceptEnd.QueryPopUp

        If INDSleConceptEnd.Properties.DataSource Is Nothing Then
            LoadXpoConceptEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocumentStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentStart_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocumentStart.QueryPopUp
        If INDSleDocumentStart.Datasource Is Nothing Then
            LoadXpoDocumentStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleDocumentEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleDocumentEnd_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleDocumentEnd.QueryPopUp
        If INDSleDocumentEnd.Datasource Is Nothing Then
            LoadXpoDocumentEnd()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' vuelve a cargar el datasource de las notas filtrado por naturaleza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleNatureReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleNatureReport.EditValueChanged
        LoadXpoDocumentStart()
        LoadXpoDocumentEnd()
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de las notas filtrado por estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatus.EditValueChanged
        LoadXpoDocumentStart()
        LoadXpoDocumentEnd()
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de las notas filtrado por terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyStart.EditValueChanged
        LoadXpoDocumentStart()
        LoadXpoDocumentEnd()
    End Sub

    ''' <summary>
    ''' vuelve a cargar el datasource de las notas filtrado por terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyEnd.EditValueChanged
        LoadXpoDocumentStart()
        LoadXpoDocumentEnd()
    End Sub

    ''' <summary>
    ''' Oculta o muestra el boton de exportar a excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 2 Then
            INDLciGenerateExcell.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDSbGenerateReport.MaximumSize = New System.Drawing.Size(0, 20)
            Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(376, 0)
        Else
            INDLciGenerateExcell.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDSbGenerateReport.MaximumSize = New System.Drawing.Size(300, 20)
            Me.INDLciGenerateReport.MaxSize = New System.Drawing.Size(330, 0)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports Then
            If INDGleTypeReport.EditValue = 1 Then
                AsyncLoader(True)
                Dim reporte As New rptReportListNotePortfolio
                reporte.ParametrosReporte = {INDDeDateStart.EditValue, INDDeDateEnd.EditValue,
                                             INDGleStatus.EditValue, INDGleNatureReport.EditValue,
                                             INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleDocumentStart.EditValue, INDSleDocumentEnd.EditValue,
                                             INDSleConceptStart.EditValue, INDSleConceptEnd.EditValue}
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If
            Else
                AsyncLoader(True)
                Dim reporte As New rptSubReportDocumentNotePortfolio
                reporte.ParametrosReporte = {INDDeDateStart.EditValue, INDDeDateEnd.EditValue,
                                            INDGleStatus.EditValue, INDGleNatureReport.EditValue,
                                            INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                            INDSleDocumentStart.EditValue, INDSleDocumentEnd.EditValue,
                                            INDSleConceptStart.EditValue, INDSleConceptEnd.EditValue}
                INDDvDocumentViewer.DocumentSource = reporte
                Await reporte.CargarDataSourceAsync()
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    reporte.CreateDocument(True)
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcDocumentViewer.Visible = True
                    INDDvDocumentViewer.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDeDateStart.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta cuando den click en el boton generar reporte en excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            Me.INDGcExportExcell.DataSource = chargueDatasource()
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Validations = False
        End If

        'validaciones controles de Concepto de Notas
        If INDSleConceptStart.EditValue IsNot Nothing And INDSleConceptEnd.EditValue Is Nothing Or INDSleConceptStart.EditValue Is Nothing And INDSleConceptEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblConceptNote.Text)
            Me.INDSleConceptStart.Focus()
            Validations = False
        ElseIf INDSleConceptStart.EditValue > INDSleConceptEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblConceptNote.Text)
            Me.INDSleConceptStart.Focus()
            Validations = False
        End If

        'validaciones controles de tercero
        If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue Is Nothing Or INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyStart.EditValue > INDSleThirdPartyEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        'validaciones controles de documento
        If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue Is Nothing Or INDSleDocumentStart.EditValue Is Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblDocuments.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        ElseIf INDSleDocumentStart.EditValue > INDSleDocumentEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblDocuments.Text)
            Me.INDSleDocumentStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' metodo para cargar los datos al gridview
    ''' </summary>
    ''' <remarks></remarks>
    Private Function chargueDatasource() As DataTable
        Try
            Dim filtroConsulta As String = "GetDate(PortfolioNoteId.NoteDate) >= #" & Format(INDDeDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(PortfolioNoteId.NoteDate) <= #" & Format(INDDeDateEnd.EditValue, "yyyy-MM-dd") & "#"

            'filtro por terceros
            If INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Then
                filtroConsulta &= "AND PortfolioNoteId.CustomerId.ThirdPartyId.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND PortfolioNoteId.CustomerId.ThirdPartyId.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
            ' si filtra por Notas
            If INDSleDocumentStart.EditValue IsNot Nothing And INDSleDocumentEnd.EditValue IsNot Nothing Then
                filtroConsulta &= "AND PortfolioNoteId.Code >= '" & INDSleDocumentStart.EditValue & "' AND PortfolioNoteId.Code <= '" & INDSleDocumentEnd.EditValue & "'"
            End If
            ' si filtra por Concepto de Notas
            If INDSleConceptStart.EditValue IsNot Nothing And INDSleConceptEnd.EditValue IsNot Nothing Then
                filtroConsulta &= "AND PortfolioNoteId.Portfolio_PortfolioNoteDetails[PortfolioNoteConceptId.Code >= '" & INDSleConceptStart.EditValue & "' AND PortfolioNoteConceptId.Code <= '" & INDSleConceptEnd.EditValue & "']"
            End If
            'si filtra por estado
            If INDGleStatus.EditValue <> 4 Then
                filtroConsulta &= " AND PortfolioNoteId.Status = " & INDGleStatus.EditValue
            End If
            'Si filtra por naturaleza
            If INDGleNatureReport.EditValue <> 3 Then
                filtroConsulta &= " AND PortfolioNoteId.Nature = " & INDGleNatureReport.EditValue
            End If

            Dim IndList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of PortfolioNoteAccountReceivableAdvanceReportXpo)(Nothing, filtroConsulta)

            Dim dt As New DataTable
            dt.Columns.Add("Nota")
            dt.Columns.Add("Fecha Nota", GetType(DateTime))
            dt.Columns.Add("Naturaleza")
            dt.Columns.Add("Estado")
            dt.Columns.Add("CxC")
            dt.Columns.Add("Factura")
            dt.Columns.Add("Anticipo")
            dt.Columns.Add("Valor Ajuste", GetType(Decimal))
            dt.Columns.Add("Saldo", GetType(Decimal))
            dt.Columns.Add("Cliente")

            For Each itemView In IndList
                Dim row As DataRow = dt.NewRow()
                row.Item("Nota") = itemView.PortfolioNoteId.Code
                row.Item("Fecha Nota") = itemView.PortfolioNoteId.NoteDate.AsDate
                row.Item("Naturaleza") = If(itemView.PortfolioNoteId.Nature = 1, "Debito", "Credito")
                row.Item("Estado") = If(itemView.PortfolioNoteId.Status = 1, "Registrado", If(itemView.PortfolioNoteId.Status = 2, "Confirmado", "Anulado"))
                row.Item("CxC") = If(itemView.PortfolioNoteId.NoteType = 3, "", itemView.AccountReceivableId.Code)
                row.Item("Factura") = If(itemView.PortfolioNoteId.NoteType = 3, "", itemView.AccountReceivableId.InvoiceNumber)
                row.Item("Anticipo") = If(itemView.PortfolioNoteId.NoteType = 3, itemView.PortfolioAdvanceId.Code, "")
                row.Item("Valor Ajuste") = itemView.AdjusmentValue
                row.Item("Saldo") = If(itemView.PortfolioNoteId.NoteType = 3, itemView.PortfolioAdvanceId.Balance, itemView.AccountReceivableId.Balance)
                row.Item("Cliente") = itemView.PortfolioNoteId.CustomerId.NitName
                dt.Rows.Add(row)
            Next

            AsyncLoader(False)

            Return dt
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
        _gridView.MainView.PopulateColumns()
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

End Class