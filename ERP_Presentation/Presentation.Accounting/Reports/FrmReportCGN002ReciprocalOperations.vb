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
Imports System.Text
Imports Presentation.Accounting.MVP
Imports System.IO

#End Region

Public Class FrmReportCGN002ReciprocalOperations

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
    ''' Variable que almacena la fecha de inicio
    ''' </summary>
    Dim INDDateStart As Date

    ''' <summary>
    ''' Variable que almacena la fecha de final
    ''' </summary>
    Dim INDDateEnd As Date

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
    ''' Lsita de tuplas que contiene tipos de reporte
    ''' </summary>
    Private _FillingtypeReport As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite acceder a una lista de tipos de reportes predefinidos (provisional y definitivo) 
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property FillingtypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingtypeReport Is Nothing Then
                _FillingtypeReport = New List(Of Tuple(Of Integer, String))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(1, "Provisional"))
                _FillingtypeReport.Add(New Tuple(Of Integer, String)(2, "Definitivo"))
            End If
            Return _FillingtypeReport
        End Get
    End Property

    ''' <summary>
    ''' Almacena una lista de tuplas que representan diferentes periodos de tiempo
    ''' </summary>
    Private _FillingPeriod As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingPeriod As List(Of Tuple(Of Integer, String))
        Get
            If _FillingPeriod Is Nothing Then
                _FillingPeriod = New List(Of Tuple(Of Integer, String))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(1, "1 Trimestre"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(2, "2 Trimestre"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(3, "3 Trimestre"))
                _FillingPeriod.Add(New Tuple(Of Integer, String)(4, "4 Trimestre"))
            End If
            Return _FillingPeriod
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
    ''' Calcula las fechas de inicio y fin del período según las selecciones realizadas
    ''' </summary>
    Public Sub ChargueDate()
        'fechas
        If INDGlePeriod.EditValue = 1 Then
            INDDateStart = "01/01/" & INDSpnYear.EditValue
            INDDateEnd = "01/03/" & INDSpnYear.EditValue
        ElseIf INDGlePeriod.EditValue = 2 Then
            INDDateStart = "01/04/" & INDSpnYear.EditValue
            INDDateEnd = "01/06/" & INDSpnYear.EditValue
        ElseIf INDGlePeriod.EditValue = 3 Then
            INDDateStart = "01/07/" & INDSpnYear.EditValue
            INDDateEnd = "01/09/" & INDSpnYear.EditValue
        ElseIf INDGlePeriod.EditValue = 4 Then
            INDDateStart = "01/10/" & INDSpnYear.EditValue
            INDDateEnd = "01/12/" & INDSpnYear.EditValue
        End If

    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
    End Sub


    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBaseHome.Visible = True
        INDCtcNavigation.Visible = True
        INDPcViewReport.Visible = False
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

    ''' <summary>
    ''' Si el tipo de reporte es definitivo deshabilita los filtros por cuentas, si es provisional habilita los filtros por cuenta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeReport.EditValueChanged
        If INDGleTypeReport.EditValue = 1 Then
            INDSleAccountStart.Enabled = True
            INDSleAccountEnd.Enabled = True
        Else
            INDSleAccountStart.Enabled = False
            INDSleAccountStart.EditValue = Nothing
            INDSleAccountEnd.Enabled = False
            INDSleAccountEnd.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Se inicializa los miembros
    ''' </summary>
    Private Sub FrmReportAuxiliary_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleAccountStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode
        Me.INDSleAccountEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetAccountByCode

        INDGleTypeReport.Properties.DataSource = FillingtypeReport
        INDGlePeriod.Properties.DataSource = FillingPeriod
        INDSleLevelSubAccount.EditValue = False

        INDSpnYear.EditValue = Year(Me.GetDateServer())
        INDGlePeriod.EditValue = 1
        INDGleTypeReport.EditValue = 2
        INDsleBook.Properties.Buttons(1).Visible = False
        INDSleAccountStart.View.OptionsView.ShowGroupPanel = False
        INDSleAccountEnd.View.OptionsView.ShowGroupPanel = False
        LoadXpoBook()
        SetOfficialBook()
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New rptCGN002ReciprocalOperations()
            'fechas
            ChargueDate()
            reporte.ParametrosReporte = New Object() {INDDateStart,
                                                          INDDateEnd,
                                                          INDSleAccountStart.EditValue,
                                                          INDSleAccountEnd.EditValue,
                                                          INDSleLevelSubAccount.EditValue,
                                                          INDsleBook.EditValue,
                                                          BarraBotones.OperatingUnitValue}
            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBaseHome.Visible = False
                Me.INDCtcNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSpnYear.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click al btn INDSbGenerateFlatFile; Llama la método DialogGenerateFile para generar un archivo plano
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks
    Private Async Sub INDSbGenerateFlatFile_Click(sender As Object, e As EventArgs) Handles INDSbGenerateFlatFile.Click
        If Me.ValidateControlsReports = True Then
            Dim DataArchive As StringBuilder
            ChargueDate()
            Using model As New MSettingsAccount(Me.Tag.ToString())
                DataArchive = Await model.GenerateFileCGN002(INDDateStart, INDDateEnd, INDSleAccountStart.EditValue, INDSleAccountEnd.EditValue, INDSleLevelSubAccount.EditValue, INDsleBook.EditValue)
            End Using
            DialogGenerateFile(DataArchive)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As StringBuilder)
        Try
            Dim save As System.Windows.Forms.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
            save.Filter = "Texto|*.txt"
            save.Title = "CGN2005_002_OPERACIONES_RECIPROCAS_CONVERGENCIAS - REPORTE TRIMESTRE " & INDGlePeriod.EditValue & " Del " & Year(CDate(INDDateEnd))
            save.FileName = "CGN2005_002_OPERACIONES_RECIPROCAS_CONVERGENCIAS - REPORTE TRIMESTRE " & INDGlePeriod.EditValue & " Del " & Year(CDate(INDDateEnd))
            If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim file = save.OpenFile()
                Dim streamWrite As New StreamWriter(file)
                streamWrite.Write(content)
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