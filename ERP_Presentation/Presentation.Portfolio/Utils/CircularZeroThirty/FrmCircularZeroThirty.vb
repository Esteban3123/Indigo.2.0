'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 06-10-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Portfolio.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System.Windows
Imports System.Windows.Forms
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Class FrmCircularZeroThirty
    Implements ICircularZeroThirty

#Region "Const"

    Private Const NAME_MODULE As String = "Portfolio"

#End Region

#Region "Fields"

    ''' <summary>
    ''' Instancia al presentador
    ''' </summary>
    Private _presenter As PCircularZeroThirty
    ''' <summary>
    ''' Instancia al modelo
    ''' </summary>
    Private _myModel As MCircularZeroThirty
    ''' <summary>
    ''' Diccionario de trimestres ya generados
    ''' </summary>
    Private _trimestersDic As Dictionary(Of Int32, List(Of Tuple(Of Integer, Integer)))
    ''' <summary>
    ''' Fecha actual del sistema
    ''' </summary>
    Private _currentDate As DateTime
    ''' <summary>
    ''' Último trimestre válido para generar
    ''' </summary>
    Private _lastCurrentTrimester As Byte
    ''' <summary>
    ''' Año del último trimestre válido para generar
    ''' </summary>
    Private _lastCurrentYear As Int32
    ''' <summary>
    ''' Lista de registros procesados para el trimestre
    ''' </summary>
    Private _listData As List(Of GenerateDocument030_Result)
    ''' <summary>
    ''' Constructor de cadena usado para almacenar el contenido del archivo generado
    ''' </summary>
    Private _stringResult As StringBuilder
    ''' <summary>
    ''' Control del estado del proceso
    ''' </summary>
    Private _ctrStatus As CtrStatusInfo

#End Region

#Region "Properties"

    ''' <summary>
    ''' Asigna una valor que indica si los controles de habilitan
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICircularZeroThirty.ActionsOnControls
        Set(value As Boolean)
            DteYear.Enabled = Not value
            INDSleTrimester.Enabled = value
            GdcResults.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el Tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As String Implements ICircularZeroThirty.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el trimestre seleccionado
    ''' </summary>
    ''' <returns>Trimestre seleccionado</returns>
    Public ReadOnly Property Trimester As Byte Implements ICircularZeroThirty.Trimester
        Get
            Return INDSleTrimester.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el año seleccionado
    ''' </summary>
    ''' <returns>Año seleccionado</returns>
    Public ReadOnly Property Year As Integer Implements ICircularZeroThirty.Year
        Get
            Return IIf(DteYear.EditValue IsNot Nothing, CType(DteYear.EditValue, DateTime).Year, 0)
        End Get
    End Property

    ''' <summary>
    ''' Asigna un mensaje al centro de notificaciones
    ''' </summary>
    ''' <param name="Icono">Tipo de mensaje a presentar</param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        _ctrStatus = New CtrStatusInfo()
        _ctrStatus.Status = String.Empty
        _ctrStatus.ToolTipStatus = String.Empty
        _ctrStatus.Dock = DockStyle.Fill
        _ctrStatus.Visible = True
        Me.AdditionalControlPanel.Controls.Add(_ctrStatus)
        _presenter = New PCircularZeroThirty(Me)
        _myModel = New MCircularZeroThirty(Me.Tag)
        _trimestersDic = New Dictionary(Of Int32, List(Of Tuple(Of Integer, Integer)))()
    End Sub

#End Region

#Region "Handlers"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _myModel = Nothing
        _trimestersDic = Nothing
        _currentDate = Nothing
        _lastCurrentTrimester = Nothing
        _lastCurrentYear = Nothing
        _listData = Nothing
        _stringResult = Nothing
        _ctrStatus = Nothing
    End Sub

    ''' <summary>
    ''' Aqui se carga los datos por defecto al iniciar el formulario
    ''' </summary>
    Private Sub FrmCircularZeroThirty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AsyncLoader(True)
        _presenter.LoadTrimesters()
        AsyncLoader(False)
        Deshacer()
        IndigoGridControl1.RefreshGrid(GdcResults)
        Me.AdditionalControlPanel.Visible = True
    End Sub

    ''' <summary>
    ''' Aqui se le da el foco al primer control
    ''' </summary>
    Private Sub FrmCircularZeroThirty_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If DteYear.Enabled Then
            DteYear.Focus()
        Else
            INDSleTrimester.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se consulta los trimestres del año seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DteYear_EditValueChanged(sender As Object, e As EventArgs) Handles DteYear.EditValueChanged
        If DteYear.EditValue IsNot Nothing Then
            If LoadTrimestersFronYear(Year) Then
                ActionsOnControls = True
                INDSleTrimester.Focus()
            Else
                Deshacer()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se habilita el botón de procesar
    ''' </summary>
    Private Sub INDSleTrimester_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleTrimester.EditValueChanged
        If INDSleTrimester.EditValue IsNot Nothing Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyButtonProcess)
            _stringResult = Nothing
            _ctrStatus.Status = String.Empty
            _ctrStatus.ToolTipStatus = String.Empty
            Me.GdcResults.DataSource = Nothing
            Me.GdcResults.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Realiza el calculo del último trimestre válido para generar
    ''' </summary>
    Private Sub CalculeLastCurrentTrimester()
        _currentDate = _myModel.GetDate()
        _lastCurrentYear = _currentDate.Year
        If _currentDate.Date >= New Date(_currentDate.Year, 1, 1) And _currentDate.Date <= New Date(_currentDate.Year, 3, 31) Then
            _lastCurrentTrimester = 4
            _lastCurrentYear = (_currentDate.Year - 1)
        End If
        If _currentDate.Date >= New Date(_currentDate.Year, 4, 1) And _currentDate.Date <= New Date(_currentDate.Year, 6, 30) Then
            _lastCurrentTrimester = 1
        End If
        If _currentDate.Date >= New Date(_currentDate.Year, 7, 1) And _currentDate.Date <= New Date(_currentDate.Year, 9, 30) Then
            _lastCurrentTrimester = 2
        End If
        If _currentDate.Date >= New Date(_currentDate.Year, 10, 1) And _currentDate.Date <= New Date(_currentDate.Year, 12, 31) Then
            _lastCurrentTrimester = 3
        End If
    End Sub

    ''' <summary>
    ''' Consulta y carga los trimestres ya generados en el diccionario interno de forma asincrona
    ''' </summary>
    Public Function LoadTrimestersAsync() As Task Implements ICircularZeroThirty.LoadTrimestersAsync
        Return Task.Factory.StartNew(AddressOf LoadTrimesters)
    End Function

    ''' <summary>
    ''' Consulta y carga los trimestres ya generados en el diccionario interno
    ''' </summary>
    Public Sub LoadTrimesters() Implements ICircularZeroThirty.LoadTrimesters
        Try
            If DteYear.InvokeRequired Then
                DteYear.BeginInvoke(Sub()
                                        DteYear.Properties.MinValue = Nothing
                                        DteYear.Properties.MaxValue = Nothing
                                    End Sub)
            Else
                DteYear.Properties.MinValue = Nothing
                DteYear.Properties.MaxValue = Nothing
            End If
            _trimestersDic = New Dictionary(Of Integer, List(Of Tuple(Of Integer, Integer)))()
            CalculeLastCurrentTrimester()
            Dim res = _myModel.ListTrimesters()
            If res.Count > 0 Then
                'Obtenemos los trimestres de cada año procesado
                For Each y In res
                    Dim trim = y.Value.Split(",").ToList().ConvertAll(Function(t) CInt(t))
                    Dim list As New List(Of Tuple(Of Integer, Integer))()
                    For Each e In trim
                        If y.Key = res.Max(Function(u) u.Key) And e = trim.Max() Then
                            list.Add(New Tuple(Of Integer, Integer)(e, 2))
                        Else
                            list.Add(New Tuple(Of Integer, Integer)(e, 1))
                        End If
                    Next
                    _trimestersDic.Add(y.Key, list)
                Next

                Dim lastTrimester = (_trimestersDic(_trimestersDic.Keys.Max()).Max(Function(t) t.Item1) + 1)
                Dim lastYear As Int32 = _trimestersDic.Keys.Max()
                If lastTrimester > 4 Then
                    lastYear += 1
                    lastTrimester = 1
                End If

                If lastYear < _lastCurrentYear OrElse (lastYear = _lastCurrentYear And lastTrimester <= _lastCurrentTrimester) Then
                    If _trimestersDic.ContainsKey(lastYear) Then
                        _trimestersDic(lastYear).Add(New Tuple(Of Integer, Integer)(lastTrimester, 3))
                    Else
                        _trimestersDic.Add(lastYear, New List(Of Tuple(Of Integer, Integer))({New Tuple(Of Integer, Integer)(lastTrimester, 3)}))
                    End If
                End If

                If DteYear.InvokeRequired Then
                    DteYear.BeginInvoke(Sub()
                                            DteYear.Properties.MinValue = New DateTime(_trimestersDic.Keys.Min(), 1, 1)
                                            DteYear.Properties.MaxValue = New DateTime(_trimestersDic.Keys.Max(), 12, 31)
                                        End Sub)
                Else
                    DteYear.Properties.MinValue = New DateTime(_trimestersDic.Keys.Min(), 1, 1)
                    DteYear.Properties.MaxValue = New DateTime(_trimestersDic.Keys.Max(), 12, 31)
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Limpia los controles de datos
    ''' </summary>
    Private Sub CleanControls()
        INDLycMain.BeginUpdate()
        _stringResult = Nothing
        DteYear.EditValue = Nothing
        INDSleTrimester.EditValue = Nothing
        GdcResults.DataSource = Nothing
        ActionsOnControls = False
        INDLycMain.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los trimestres disponibles en el año seleccionado
    ''' </summary>
    ''' <param name="year">Año seleccionado</param>
    Private Function LoadTrimestersFronYear(ByVal year As Int32) As Boolean
        Dim listTrimesters As New List(Of Tuple(Of Int32, String, String, Int32))()
        If _trimestersDic.Count = 0 Then 'No existe ningún trimestre procesado aún
            listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(1, "Primer trimestre", "1 - Primer trimestre", 3))
            listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(2, "Segundo trimestre", "2 - Segundo trimestre", 3))
            listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(3, "Tercer trimestre", "3 - Tercer trimestre", 3))
            listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(4, "Cuarto trimestre", "4 - Cuarto trimestre", 3))
        ElseIf _trimestersDic.ContainsKey(year) Then
            For Each i In _trimestersDic(year)
                Select Case i.Item1
                    Case 1
                        listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(1, "Primer trimestre", "1 - Primer trimestre", i.Item2))
                    Case 2
                        listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(2, "Segundo trimestre", "2 - Segundo trimestre", i.Item2))
                    Case 3
                        listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(3, "Tercer trimestre", "3 - Tercer trimestre", i.Item2))
                    Case 4
                        listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(4, "Cuarto trimestre", "4 - Cuarto trimestre", i.Item2))
                    Case Else
                        listTrimesters.Add(New Tuple(Of Int32, String, String, Int32)(1, "Primer trimestre", "1 - Primer trimestre", i.Item2))
                End Select
            Next
        Else 'El año seleccionado no es válido para procesar
            Mensaje(EeventViewerImages.Advertencia) = "El año seleccionado no se encuentra en el rango de años válidos (" & _trimestersDic.Keys.Min() & " - " & _trimestersDic.Keys.Max() & ")"
            Return False
        End If
        INDSleTrimester.Properties.DataSource = Nothing
        INDSleTrimester.Properties.DataSource = listTrimesters
        INDSleTrimester.Properties.View.RefreshData()
        Return True
    End Function

    ''' <summary>
    ''' Actualiza el estado del proceso en el control de la barra
    ''' </summary>
    ''' <param name="status">Numero del estado</param>
    Private Sub UpdateStatusProcess(ByVal status As Int32)
        Select Case status
            Case 1
                _ctrStatus.Status = "Consultado"
                _ctrStatus.ToolTipStatus = "Los datos del trimestre seleccionado han sido consultados"
            Case 2
                _ctrStatus.Status = "Reprocesado"
                _ctrStatus.ToolTipStatus = "Los datos del trimestre seleccionado se generaron y almacenaron nuevamente"
            Case 3
                _ctrStatus.Status = "Procesado"
                _ctrStatus.ToolTipStatus = "Los datos del trimestre seleccionado acaban de ser procesados y almacenados"
            Case Else
                _ctrStatus.Status = String.Empty
                _ctrStatus.ToolTipStatus = String.Empty
        End Select
    End Sub

    ''' <summary>
    ''' Carga los datos para el trimestre seleccionado
    ''' </summary>
    Private Async Sub LoadData()
        Try
            AsyncLoader(True)
            Dim result = Await _myModel.GenerateDocument030Async(Year, Trimester)
            If result?.StateResult Then
                Me._listData = result?.ObjectEmbbeded
                If Me._listData.Count > 0 Then
                    UpdateStatusProcess(_trimestersDic(Year).Where(Function(t) t.Item1 = Trimester).First().Item2)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Me._listData(0).CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Me._listData(0).CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Me._listData(0).ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Me._listData(0).ModificationDate)
                    GdcResults.DataSource = _listData
                    GdcResults.RefreshDataSource()
                    Me.BarraBotones.PrepareToolbar(eAction.GenerateFileWithAuditAndDocumental)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se obtuvo datos para el trimestre seleccionado y es posible que haya ocurrido un error en el proceso. Porfavor contacte al administrador del sistema."
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            If ex.InnerException IsNot Nothing Then
                Throw ex.InnerException
            Else
                Throw ex
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene una tupla con la fecha inicial y final del año y trimestre seleccionados
    ''' </summary>
    ''' <param name="year">Año a calcular</param>
    ''' <param name="trimester">Trimestre a calcular</param>
    ''' <returns>Fechas inicial y final del trimestre</returns>
    Private Function GetTrimesterDates(ByVal year As Int32, ByVal trimester As Byte) As Tuple(Of Date, Date)
        If trimester = 1 Then
            Return New Tuple(Of Date, Date)(New Date(year, 1, 1), New Date(year, 3, 31))
        End If
        If trimester = 2 Then
            Return New Tuple(Of Date, Date)(New Date(year, 4, 1), New Date(year, 6, 30))
        End If
        If trimester = 3 Then
            Return New Tuple(Of Date, Date)(New Date(year, 7, 1), New Date(year, 9, 30))
        End If
        If trimester = 4 Then
            Return New Tuple(Of Date, Date)(New Date(year, 10, 1), New Date(year, 12, 31))
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Genera el archivo
    ''' </summary>
    Private Sub GenerateFile()
        Try
            If _listData IsNot Nothing AndAlso _listData.Count > 0 Then
                AsyncLoader(True)
                Dim dates = GetTrimesterDates(Me.Year, Me.Trimester)
                If _stringResult Is Nothing Then
                    _stringResult = New StringBuilder()
                    'Creamos el registro de cabecera o tipo de registro 1
                    Dim reg1 As New List(Of String)()
                    reg1.Add("1")
                    reg1.Add("NI")
                    reg1.Add(indigo.IndigoCompanyNit.Trim())
                    reg1.Add(indigo.IndigoCompanyName.Trim().ToUpper())
                    reg1.Add(dates.Item1.ToString("yyyy-MM-dd"))
                    reg1.Add(dates.Item2.ToString("yyyy-MM-dd"))
                    reg1.Add(_listData.Count)
                    _stringResult.AppendLine(String.Join(",", reg1.ToArray()))
                    'Recorremos los datos dandole formato a cada registro tipo 2
                    Dim consecutive As Int64 = 0
                    For Each row As GenerateDocument030_Result In _listData
                        Dim reg2 As New List(Of String)()
                        consecutive += 1
                        reg2.Add("2")
                        reg2.Add(consecutive.ToString())
                        reg2.Add(If(row.IdentificationTypeERP IsNot Nothing, row.IdentificationTypeERP, String.Empty))
                        reg2.Add(If(row.IdentificationNumberERP IsNot Nothing, row.IdentificationNumberERP, String.Empty))
                        reg2.Add(If(row.NameERP IsNot Nothing, row.NameERP, String.Empty))
                        reg2.Add(If(row.IdentificationTypeIPS_EPSS IsNot Nothing, row.IdentificationTypeIPS_EPSS, String.Empty))
                        reg2.Add(If(row.IdentificationNumberIPS_EPSS IsNot Nothing, row.IdentificationNumberIPS_EPSS, String.Empty))
                        reg2.Add(If(row.PaymentType IsNot Nothing, row.PaymentType, String.Empty))
                        reg2.Add(If(row.InvoicePrefix IsNot Nothing, row.InvoicePrefix, String.Empty))
                        reg2.Add(If(row.InvoiceNumber IsNot Nothing, row.InvoiceNumber, String.Empty))
                        reg2.Add(If(row.UpdateIndicator IsNot Nothing, row.UpdateIndicator, String.Empty))
                        reg2.Add(row.InvoiceValue.ToString("0"))
                        reg2.Add(row.InvoiceDate.ToString("yyyy-MM-dd"))
                        reg2.Add(row.RadicateDate.ToString("yyyy-MM-dd"))
                        reg2.Add(If(row.DevolutionDate.HasValue, row.DevolutionDate.Value.ToString("yyyy-MM-dd"), String.Empty))
                        reg2.Add(row.TotalValuePayments.ToString("0"))
                        reg2.Add(row.ObjectionValue.ToString("0"))
                        reg2.Add(If(row.ObjectionWithAnswer, "SI", "NO"))
                        reg2.Add(row.InvoiceBalance.ToString("0"))
                        reg2.Add(If(row.InvoiceJudicialRecovery, "SI", "NO"))
                        reg2.Add(row.JudicialRecoveryStatus.ToString())
                        _stringResult.AppendLine(String.Join(",", reg2.ToArray()))
                    Next
                End If
                'Preguntamos la ruta donde se desea guardar el archivo
                Dim fbd As New FolderBrowserDialog()
                fbd.Description = "Seleccione la carpeta donde desea descargar el archivo"
                fbd.ShowNewFolderButton = True
                If fbd.ShowDialog() = DialogResult.OK AndAlso fbd.SelectedPath IsNot Nothing AndAlso Not fbd.SelectedPath.Trim().Equals(String.Empty) Then
                    Dim pathFile As String = IO.Path.Combine(fbd.SelectedPath, ("SAC165FIPS" & dates.Item2.ToString("yyyyMMdd") & "NI" & Utils.StringPad(indigo.IndigoCompanyNit.Trim(), 12, "0", Utils.PadType.STR_PAD_LEFT) & ".TXT"))
                    IO.File.WriteAllText(pathFile, _stringResult.ToString())
                    If MessageIndigo.Show("Desea abrir el archivo?", MessageType.Question, "Abrir archivo", Botones.SiNo) = DialogResult.Yes Then
                        Process.Start(pathFile)
                    End If
                End If
                AsyncLoader(False)
            End If
        Catch ex As Exception
            _stringResult = Nothing
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "BarButton Handlers"

    ''' <summary>
    ''' Aqui se cargan los permisos del formulario
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(Me.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' Aqui se procesa el trimestre seleccionado
    ''' </summary>
    Private Sub BarraBotones_ClickProcesar() Handles BarraBotones.ClickProcesar
        If Year = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe especificar un año"
            Return
        End If
        If Trimester = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar el trimestre a procesar"
            Return
        End If
        LoadData()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la generación del archivo
    ''' </summary>
    Private Sub BarraBotones_Click_GenerateFile() Handles BarraBotones.Click_GenerateFile
        GenerateFile()
    End Sub

    ''' <summary>
    ''' Se limpia el formulario
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

#End Region

End Class