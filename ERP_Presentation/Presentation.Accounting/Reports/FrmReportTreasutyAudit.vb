#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP

#End Region
Public Class FrmReportTreasutyAudit

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Property ProoftCloseXpoAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que almacena el origen de datos para libros contables
    ''' </summary>
    ''' <returns></returns>
    Public Property bookXpcollection As XPCollection

    ''' <summary>
    ''' Lista para acceder y manipular objetos de la clase GeneralLedgerVTreasuryAuditReportXpo
    ''' </summary>
    Private lista As List(Of GeneralLedgerVTreasuryAuditReportXpo)

    ''' <summary>
    ''' Propiedad que se usa para cargar la tupla de opciones de clasificación
    ''' </summary>
    Private _FillingSort As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingSort As List(Of Tuple(Of Integer, String))
        Get
            If _FillingSort Is Nothing Then
                _FillingSort = New List(Of Tuple(Of Integer, String))
                _FillingSort.Add(New Tuple(Of Integer, String)(1, "Consecutivo"))
                _FillingSort.Add(New Tuple(Of Integer, String)(2, "Fecha"))
            End If
            Return _FillingSort
        End Get
    End Property

    Dim dt As New DataTable

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

#Region "Methods"

    ''' <summary>
    ''' Método para cargar el DataSource Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountStart()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            'ProoftCloseXpoAccount.Sorting.Add(New SortProperty("Number", DB.SortingDirection.Ascending))
            INDSleAccountStart.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoAccountEnd()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True, INDsleBook.EditValue}
            ProoftCloseXpoAccount = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListMainAccountsByStatusAndBookId, filter)
            'ProoftCloseXpoAccount.Sorting.Add(New SortProperty("Number", DB.SortingDirection.Ascending))
            INDSleAccountEnd.Datasource = ProoftCloseXpoAccount
        End Using
    End Sub


    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenterEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBook()
        Using msearch As New MBusqueda
            Dim filter() As Object = {True}
            bookXpcollection = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBookByStatusXpCollection, filter)
            INDsleBook.Properties.DataSource = bookXpcollection
        End Using
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True

        'validaciones controles de fecha
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDDateStart.Focus()
            Validations = False
        End If

        'validaciones controles de cuenta
        If INDSleAccountStart.EditValue IsNot Nothing And INDSleAccountEnd.EditValue Is Nothing Or INDSleAccountStart.EditValue Is Nothing And INDSleAccountEnd.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblAccount.Text)
            Me.INDSleAccountStart.Focus()
            Validations = False
        ElseIf INDSleAccountStart.EditValue > INDSleAccountEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblAccount.Text)
            Me.INDSleAccountStart.Focus()
            Validations = False
        End If

        Return Validations
    End Function


    ''' <summary>
    ''' se ejecuta al dar click en el boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptTreasuryAudit()
            reporte.ParametrosReporte = New Object() {INDDateStart.EditValue,
                                                      INDDateEnd.EditValue,
                                                      INDsleBook.EditValue,
                                                      INDGleSort.EditValue,
                                                      INDSleAccountStart.TextEditValue,
                                                      INDSleAccountEnd.TextEditValue}
            INDDvReportPrint.DocumentSource = reporte
            Await reporte.CargarDataSourceAsync()
            AsyncLoader(False)
            If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                reporte.CreateDocument(True)
                Me.INDLcBaseHome.Visible = False
                Me.INDCtcNavigation.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReportPrint.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDDateStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBaseHome.Visible = True
        INDCtcNavigation.Visible = True
        INDPcReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountStart.QueryPopUp
        If INDSleAccountStart.Datasource Is Nothing Then
            LoadXpoAccountStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccountEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAccountEnd.QueryPopUp
        If INDSleAccountEnd.Datasource Is Nothing Then
            LoadXpoAccountEnd()
        End If
    End Sub

    ''' <summary>
    ''' Este evento se activa cuando el formulario es eliminado y sus recursos deben ser liberados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
        dt = Nothing
    End Sub


    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    Private Sub FrmReportTreasutyAudit_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode

        INDsleBook.Properties.Buttons(1).Visible = False
        INDSleAccountStart.View.OptionsView.ShowGroupPanel = False
        INDSleAccountEnd.View.OptionsView.ShowGroupPanel = False
        LoadXpoBook()
        SetOfficialBook()
    End Sub

    ''' <summary>
    ''' se ejecuta al mostrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTreasutyAudit_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        'Cargar GridLookUpEdit
        Me.INDGleSort.Properties.DataSource = FillingSort

        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleSort.EditValue = 1
    End Sub

    ''' <summary>
    ''' Carga datos en un Datatable para luego exportarlos a un archivo excel
    ''' </summary>
    ''' <returns></returns>
    Private Function chargueDatasource() As DataTable ' EXCEL

        'Se obtiene el listado por medio de un Xpo
        Dim filtroConsulta As String = "GetDate(DocumentDate) >= #" & Format(Me.INDDateStart.EditValue, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(Me.INDDateEnd.EditValue, "yyyy-MM-dd") & "#"

        'si filtra por Almacén
        If Me.INDSleAccountStart.EditValue <> String.Empty And Me.INDSleAccountEnd.EditValue <> String.Empty Then
            filtroConsulta &= " AND Number >= '" & INDSleAccountStart.EditValue & "' AND Number <= '" & INDSleAccountEnd.EditValue & "'"
        End If

        'Filtra por el libro 
        If Me.INDsleBook.EditValue IsNot Nothing Then
            filtroConsulta &= " AND LegalBookId = " & INDsleBook.EditValue
        End If

        lista = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).AccountingService.GetCollection(Of GeneralLedgerVTreasuryAuditReportXpo)(Nothing, filtroConsulta)

        'Dim dt As New DataTable
        dt.Columns.Add("Entidad")
        dt.Columns.Add("Código")
        dt.Columns.Add("Fecha")
        dt.Columns.Add("U. Creación")
        dt.Columns.Add("Detalle")
        dt.Columns.Add("Caja")
        dt.Columns.Add("Banco")
        dt.Columns.Add("Cuenta Contable")

        Dim columValue As DataColumn = New DataColumn
        columValue.DataType = System.Type.GetType("System.Decimal")
        columValue.AllowDBNull = False
        columValue.Caption = "Valor Tesorería"
        columValue.ColumnName = "Valor Tesorería"
        dt.Columns.Add(columValue)

        Dim columValue2 As DataColumn = New DataColumn
        columValue2.DataType = System.Type.GetType("System.Decimal")
        columValue2.AllowDBNull = False
        columValue2.Caption = "Valor General"
        columValue2.ColumnName = "Valor General"
        dt.Columns.Add(columValue2)

        For Each itemView In lista

            Dim row As DataRow = dt.NewRow()
            row.Item("Entidad") = itemView.Entity
            row.Item("Código") = itemView.Code
            row.Item("Fecha") = itemView.DocumentDate
            row.Item("U. Creación") = itemView.CreationUser
            row.Item("Detalle") = itemView.Detail
            row.Item("Caja") = itemView.Cash
            row.Item("Banco") = itemView.EntityBank
            row.Item("Cuenta Contable") = itemView.Number & " - " & itemView.MainAccount
            row.Item("Valor Tesorería") = itemView.TreasuryValue
            row.Item("Valor General") = itemView.GeneralValue

            dt.Rows.Add(row)
        Next

        AsyncLoader(False)
        'If dt IsNot Nothing Then
        If dt.Rows.Count > 0 Then
            Return dt
        Else
            Return New DataTable
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
            Me.INDDateStart.Focus()
        End If
    End Function

    ''' <summary>
    ''' se ejecuta para exportar a excel el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateExcell_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcell.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Me.INDGcExportExcell.DataSource = chargueDatasource()
            If Me.INDGcExportExcell.DataSource IsNot Nothing Then
                generateExcel()
            End If
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub generateExcel()
        Dim _gridView = Me.INDGcExportExcell
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

    ''' <summary>
    '''  Se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetOfficialBook()
        If bookXpcollection IsNot Nothing AndAlso bookXpcollection.Count > 0 Then
            Dim item = (From l In bookXpcollection Where l.OfficialBook = True Select l).FirstOrDefault
            If item IsNot Nothing Then
                INDsleBook.EditValue = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo que carga los datasource de la cuenta inicial y final al momento de cambiar el libro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBook.EditValueChanged
        If INDsleBook.EditValue IsNot Nothing Then
            LoadXpoAccountStart()
            LoadXpoAccountEnd()
        End If
    End Sub
#End Region
End Class