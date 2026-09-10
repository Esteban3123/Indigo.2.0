#Region "Imports"

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Portfolio.MVP
Imports System.IO
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

#End Region

Public Class FrmReportCircularAccountsReceivable
    Implements IReportCircularAccountsReceivable

#Region "Builder"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PReportCircularAccountsReceivable

    Private ListHealthAdministratorId As New List(Of Integer)()

    Private _healthAdministratorIds As String

    Dim filters As Dictionary(Of String, String)

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

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

    Public ReadOnly Property Year As Integer
        Get
            Return INDCdnDateStart.GetYear
        End Get
    End Property

    Public ReadOnly Property Month As Integer
        Get
            Return INDCdnDateStart.GetMonth
        End Get
    End Property

    Public Property HealthAdministratorIds As String Implements IReportCircularAccountsReceivable.HealthAdministratorIds
        Get
            Return _healthAdministratorIds
        End Get
        Set(value As String)
            _healthAdministratorIds = value
        End Set
    End Property

    Public ReadOnly Property Status As String
        Get
            Return INDchkStatus.EditValue
        End Get
    End Property

#End Region

#Region "XPO"

    ''' <summary>
    ''' Establece el datasource de la entidad administradora de salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthAdministratorXpo As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.ContractRepository.HealthAdministratorXpo) Implements IReportCircularAccountsReceivable.HealthAdministratorXpo
        Get
            Return INDsleHealthAdministrator.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection(Of Infrastructure.Data.Xpo.ContractRepository.HealthAdministratorXpo))
            INDsleHealthAdministrator.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

#End Region

#Region "Methods"

    Private Function ValidateControlsReports() As Boolean
        Dim Validations As Boolean = True

        If String.IsNullOrEmpty(Me.HealthAdministratorIds) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos una entidad administradora"
            Validations = False
        End If

        If String.IsNullOrEmpty(Me.INDchkStatus.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un estado"
            Validations = False
        End If

        If Validations Then
            filters = New Dictionary(Of String, String)
            filters.Add("Year", Year)
            filters.Add("Month", Month)
            filters.Add("HealthAdministratorIds", HealthAdministratorIds)
            filters.Add("Status", Status)
        End If

        Return Validations
    End Function

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

#End Region

#Region "Events"

#Region "Load And Disposed"

    Private Sub FrmReportCircularAccountsReceivable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PReportCircularAccountsReceivable(Me)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _healthAdministratorIds = Nothing
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.DataSource Is Nothing Then
            Presenter.InitializeHealtAdministrator()
        End If
    End Sub

#End Region

#Region "MouseDown"

    ''' <summary>
    ''' Evento que se dispara al checkear los items de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvHealthAdministrator_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgvHealthAdministrator.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column.FieldName = "DX$CheckboxSelectorColumn" Then
            If Not hi.InRow Then
                Dim allSelected As Boolean = view.DataController.Selection.Count = view.DataRowCount
                If Not allSelected Then
                    For i As Integer = 0 To view.RowCount - 1
                        Dim sourceHandle As Integer = view.GetDataSourceRowIndex(i)
                        If Not ListHealthAdministratorId.Contains(sourceHandle) Then
                            ListHealthAdministratorId.Add(sourceHandle)
                        End If
                    Next i
                Else
                    ListHealthAdministratorId.Clear()
                End If
            Else
                Dim sourceHandle As Integer = view.GetDataSourceRowIndex(hi.RowHandle)
                If Not ListHealthAdministratorId.Contains(sourceHandle) Then
                    ListHealthAdministratorId.Add(sourceHandle)
                Else
                    ListHealthAdministratorId.Remove(sourceHandle)
                End If
            End If
        End If
    End Sub

#End Region

#Region "ColumnFilterChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del filtro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvHealthAdministrator_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDgvHealthAdministrator.ColumnFilterChanged
        RestoreSelection(TryCast(sender, GridView))
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDsleHealthAdministratorCloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleHealthAdministrator.CloseUp
        Me.HealthAdministratorIds = RecuperarSeleccionados(sender, "Id", "CodeName")
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        Try
            If Me.ValidateControlsReports = True Then
                AsyncLoader(True)
                Using model As New MReports(Me.Tag)
                    Dim dataArchive As ActionResult(Of StringBuilder) = Await model.GenerateFileCircularAccountsReceivable(filters)
                    If String.IsNullOrEmpty(dataArchive.ObjectEmbbeded.ToString()) Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para generar el archivo"
                    Else
                        DialogGenerateFile(dataArchive)
                    End If
                End Using
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Sub INDSbGenerateExcel_Click(sender As Object, e As EventArgs) Handles INDSbGenerateExcel.Click
        Try
            If Me.ValidateControlsReports = True Then
                AsyncLoader(True)
                Using model As New MReports(Me.Tag)
                    Dim ds As DataSet = Await model.GetListReportCircularAccountsReceivable(filters)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim dt As DataTable = ds.Tables("ReportCircularAccountsReceivable")
                        INDGcExportExcel.DataSource = dt
                        If INDGcExportExcel.DataSource IsNot Nothing Then
                            generateExcel()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para generar el excel"
                    End If
                End Using
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

#End Region

#Region "ClickBack"

    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que reestablece el check de los items de la rejilla
    ''' </summary>
    ''' <param name="view"></param>
    Private Sub RestoreSelection(ByVal view As GridView)
        BeginInvoke(New Action(Sub()
                                   Dim i As Integer = 0
                                   Do While i < ListHealthAdministratorId.Count
                                       view.SelectRow(view.GetRowHandle(ListHealthAdministratorId(i)))
                                       i += 1
                                   Loop
                               End Sub))
    End Sub

    Private Function RecuperarSeleccionados(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim ListHealthAdministratorId As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In ListHealthAdministratorId
            'identificadores += separador & edit.Properties.View.GetRow(selectionRow).Row(keyField).ToString()
            'descripciones += separador & edit.Properties.View.GetRow(selectionRow).Row(descripcionField).ToString()
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ", "
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function

    Public Sub DialogGenerateFile(dataArchive As ActionResult(Of StringBuilder))
        Try
            Dim save As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            save.Filter = "Texto|*.txt"
            save.FileName = dataArchive.Message
            If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim file = save.OpenFile()
                Dim streamWrite As New StreamWriter(file)
                streamWrite.Write(dataArchive.ObjectEmbbeded)
                streamWrite.Flush()
                streamWrite.Close()
                If MessageIndigo.Show("Desea Abrir el Archivo", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Process.Start(save.FileName)
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un Error Creando el Archivo"
        End Try
    End Sub

#End Region

End Class