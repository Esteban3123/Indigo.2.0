#Region "Imports"

Imports Domain.Integration.POCO
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Inventory.MVP
Imports Newtonsoft.Json
Imports System.ComponentModel
Imports System.Timers
Imports System.IO

#End Region

Public Class FrmIntegrationErrorLogs
    Implements IIntegrationErrorLogs

    Private ReadOnly _presenter As PIntegrationErrorLogs
    Public Sub New()
        InitializeComponent()
        _presenter = New PIntegrationErrorLogs(SessionValues.Instance.GetEndpointByCode("integration-api").UrlBase)
    End Sub
#Region "Variables"

    Private _isPopupMenuShowing As Boolean

    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)

    Private _currentPage As Integer = 1
    Private _pageSize As Integer = 100
    Private _isLoading As Boolean = False
    Private _allInvalidDataLoaded As Boolean = False
    Private _allValidDataLoaded As Boolean = False

    ''' <summary>
    ''' propiedad privada que establece timer que refrescará el dashboard
    ''' </summary>
    Private _timer As System.Timers.Timer
    Private _flagTimer As Boolean = False
    Private Const PATH_ORIGIN = "{0}\FrmIntegrationErrorLogs\{1}\{2}"

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IIntegrationErrorLogs.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' propiedad que obtiene la pestaña actual del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrentTabName
        Get
            Return INDTcgTransactionMessages.SelectedTabPageName
        End Get
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna el valor que espicifica si se activa 
    ''' o no el refrescado automatico sobre la rejilla
    ''' </summary>
    ''' <returns></returns>
    Public Property AutomaticReload As Boolean
        Get
            Return INDBtsAutomaticRefresh.Checked
        End Get
        Set(value As Boolean)
            INDBtsAutomaticRefresh.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AutomaticReloadPath As String
        Get
            Return String.Format(PATH_ORIGIN, Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDBtsAutomaticRefresh), $"{NameOf(INDBtsAutomaticRefresh)}.IndigoUser_{SessionValues.Instance.UserIndigo}.fav")
        End Get
    End Property

#End Region

#Region "Datasource"


#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los mensajes invalidos, lo deserializa y asigna al datasource.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadErrorLogsAsync() As Task
        Try
            INDGvIntegrationTransactionsInvalid.ShowLoadingPanel()
            Dim jsonData As String = Await _presenter.GetIntegrationLogsPagedAsync(1, _pageSize, 1)
            ' Deserializamos el JSON en una lista
            Dim errorLogs As List(Of IntegrationLog) = JsonConvert.DeserializeObject(Of List(Of IntegrationLog))(jsonData)
            INDGcIntegrationInvalid.DataSource = errorLogs
            INDGcIntegrationInvalid.RefreshDataSource()
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Ocurrió un error al consultar los mensajes, por favor intente de nuevo."
        Finally
            INDGvIntegrationTransactionsInvalid.HideLoadingPanel()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los mensajes validos, lo deserializa y asigna al datasource.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadValidLogsAsync() As Task
        Try
            INDGvIntegrationTransactionsValid.ShowLoadingPanel()
            Dim jsonData As String = Await _presenter.GetIntegrationLogsPagedAsync(_currentPage, _pageSize, 0)
            Dim validlogs As List(Of IntegrationLog) = JsonConvert.DeserializeObject(Of List(Of IntegrationLog))(jsonData)
            INDGcIntegrationValid.DataSource = validlogs
            INDGcIntegrationValid.RefreshDataSource()
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Ocurrió un error al consultar los mensajes, por favor intente de nuevo."
        Finally
            INDGvIntegrationTransactionsValid.HideLoadingPanel()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function RefreshDataSource(Optional selectedPage As String = Nothing) As Task

        Dim page As String = String.Empty

        If String.IsNullOrEmpty(selectedPage) Then
            page = Me.CurrentTabName
        Else
            page = selectedPage
        End If

        Select Case page
            Case INDLcgInvalidTransactions.Name
                _currentPage = 1
                _allInvalidDataLoaded = False
                Await LoadErrorLogsAsync()
            Case INDLcgValidTransactions.Name
                _currentPage = 1
                _allValidDataLoaded = False
                Await LoadValidLogsAsync()
        End Select
    End Function

    Private Function GetSavedAutomaticReload(automaticReloadPath As String) As Boolean
        If File.Exists(automaticReloadPath) AndAlso Not String.IsNullOrEmpty(File.ReadAllText(automaticReloadPath)) Then
            Dim body As String = File.ReadAllText(automaticReloadPath).Trim()

            Using ms As MemoryStream = New MemoryStream(System.Text.Encoding.UTF8.GetBytes(body))
                Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(AutomaticReload))
                Dim dictionaryAutomaticReload = CType(xs.Deserialize(ms), AutomaticReload)
                Return dictionaryAutomaticReload.Value
            End Using
        Else
            Return False
        End If
    End Function

#End Region

#Region "Handlers"

#Region "Load"
    Private Async Sub FrmIntegratrionErrorLogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ExecuteTimer()
        Me.AutomaticReload = Me.GetSavedAutomaticReload(Me.AutomaticReloadPath)
        Me.ToolBar.Hide()
        Await RefreshDataSource()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Resend)
        IndigoGridView1.SetListAcction(INDGvIntegrationTransactionsInvalid, ListActions)
        INDGcIntegrationInvalid.RefreshDataSource()

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvIntegrationTransactionsInvalid.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub
#End Region

#Region "Scrolling"
    Private Async Sub INDGvIntegrationTransactionsInvalid_GridScrollFinish(sender As Object, e As EventArgs) Handles INDGvIntegrationTransactionsInvalid.TopRowChanged
        Dim isVisible = INDGvIntegrationTransactionsInvalid.IsRowVisible(INDGvIntegrationTransactionsInvalid.DataRowCount - 1).ToString()

        If (isVisible.Equals("Visible") Or isVisible.Equals("Partially")) AndAlso Not _isLoading AndAlso Not _allInvalidDataLoaded Then
            _isLoading = True ' Bloquea nuevas solicitudes mientras esta está en curso

            Dim topRowIndex As Integer = INDGvIntegrationTransactionsInvalid.TopRowIndex

            Try
                ' Llama a la API para obtener el siguiente lote de registros
                Dim jsonData As String = Await _presenter.GetIntegrationLogsPagedAsync(_currentPage + 1, _pageSize, 1)
                Dim newLogs As List(Of IntegrationLog) = JsonConvert.DeserializeObject(Of List(Of IntegrationLog))(jsonData)

                ' Verifica si hay datos nuevos
                If newLogs.Count = 0 Then
                    _allInvalidDataLoaded = True
                Else
                    ' Agrega los nuevos datos a la fuente actual
                    Dim currentData = CType(INDGcIntegrationInvalid.DataSource, List(Of IntegrationLog))
                    currentData.AddRange(newLogs)
                    INDGcIntegrationInvalid.DataSource = Nothing
                    INDGcIntegrationInvalid.DataSource = currentData

                    ' Incrementa el contador de página
                    _currentPage += 1
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.MensajeError) = "Ocurrió un error al cargar los datos: " & ex.Message
            Finally
                INDGvIntegrationTransactionsInvalid.TopRowIndex = topRowIndex
                _isLoading = False
            End Try
        End If
    End Sub

    Private Async Sub INDGvIntegrationTransactionsValid_GridScrollFinish(sender As Object, e As EventArgs) Handles INDGvIntegrationTransactionsValid.TopRowChanged
        Dim isVisible = INDGvIntegrationTransactionsValid.IsRowVisible(INDGvIntegrationTransactionsValid.DataRowCount - 1).ToString()

        If (isVisible.Equals("Visible") Or isVisible.Equals("Partially")) AndAlso Not _isLoading AndAlso Not _allValidDataLoaded Then
            _isLoading = True ' Bloquea nuevas solicitudes mientras esta está en curso

            Dim topRowIndex As Integer = INDGvIntegrationTransactionsValid.TopRowIndex

            Try
                ' Llama a la API para obtener el siguiente lote de registros
                Dim jsonData As String = Await _presenter.GetIntegrationLogsPagedAsync(_currentPage + 1, _pageSize, 0)
                Dim newLogs As List(Of IntegrationLog) = JsonConvert.DeserializeObject(Of List(Of IntegrationLog))(jsonData)

                ' Verifica si hay datos nuevos
                If newLogs.Count = 0 Then
                    _allValidDataLoaded = True
                Else
                    ' Agrega los nuevos datos a la fuente actual
                    Dim currentData = CType(INDGcIntegrationValid.DataSource, List(Of IntegrationLog))
                    currentData.AddRange(newLogs)
                    INDGcIntegrationValid.DataSource = Nothing
                    INDGcIntegrationValid.DataSource = currentData

                    ' Incrementa el contador de página
                    _currentPage += 1
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.MensajeError) = "Ocurrió un error al cargar los datos: " & ex.Message
            Finally
                INDGvIntegrationTransactionsValid.TopRowIndex = topRowIndex
                _isLoading = False
            End Try
        End If
    End Sub

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Obtiene los detalles del mensaje seleccionado en la grilla de Mensajes Validos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDPceTransactionDetailValid_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceTransactionDetailValid.QueryPopUp
        INDPceTransactionDetailsInvalid.PopupControl = Nothing
        INDPceTransactionDetailValid.PopupControl = INDPccIntegrationTransactionDetails
        INDGcIntegrationTransactionDetails.DataSource = Nothing
        Try
            Dim IntegrationLog = TryCast(INDGvIntegrationTransactionsValid.GetFocusedRow, IntegrationLog)
            If IntegrationLog Is Nothing Then
                Throw New InvalidOperationException("No se pudo obtener la fila seleccionada.")
            End If

            Dim result = Await _presenter.GetIntegrationLogDetailsAsync(IntegrationLog.Id)
            If Not result.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Sub
            End If

            Dim jsondata = result.ObjectEmbbeded
            Dim IntegrationLogDetailsList As List(Of IntegrationLogDetail) = JsonConvert.DeserializeObject(Of List(Of IntegrationLogDetail))(jsondata)
            Me.SafeInvoke(Sub()
                              INDGcIntegrationTransactionDetails.DataSource = IntegrationLogDetailsList
                              INDGcIntegrationTransactionDetails.RefreshDataSource()
                          End Sub)
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Ocurrió un error al consultar los detalles del mensaje, por favor intente de nuevo. " & ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los detalles del mensaje seleccionado en la grilla de Mensajes Invalidos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDPceTransactionDetailsInvalid_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPceTransactionDetailsInvalid.QueryPopUp
        INDGcIntegrationTransactionDetails.DataSource = Nothing
        INDPceTransactionDetailValid.PopupControl = Nothing
        INDPceTransactionDetailsInvalid.PopupControl = INDPccIntegrationTransactionDetails

        Try
            Dim IntegrationLog = TryCast(INDGvIntegrationTransactionsInvalid.GetFocusedRow, IntegrationLog)
            If IntegrationLog Is Nothing Then
                Throw New InvalidOperationException("Ocurrió un error al obtener el mensaje seleccionado, por favor intente de nuevo.")
            End If

            Dim result = Await _presenter.GetIntegrationLogDetailsAsync(IntegrationLog.Id)

            If Not result.StateResult Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Sub
            End If

            Dim jsondata = result.ObjectEmbbeded
            Dim IntegrationLogDetailsList As List(Of IntegrationLogDetail) = JsonConvert.DeserializeObject(Of List(Of IntegrationLogDetail))(jsondata)
            Me.SafeInvoke(Sub()
                              INDGcIntegrationTransactionDetails.DataSource = IntegrationLogDetailsList
                              INDGcIntegrationTransactionDetails.RefreshDataSource()
                          End Sub)
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Ocurrió un error al consultar los detalles del mensaje, por favor intente de nuevo. " + ex.Message
        End Try

    End Sub
#End Region

#Region "ContextMenuActions"
    ''' <summary>
    ''' Evento que captura el click en el menu contextual de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions

        Try
            Dim IntegrationLog = TryCast(INDGvIntegrationTransactionsInvalid.GetFocusedRow, IntegrationLog)
            If IntegrationLog Is Nothing Then
                Throw New InvalidOperationException("Ocurrió un error al obtener el mensaje seleccionado, por favor intente de nuevo.")
            End If

            Dim jsonResponse = Await _presenter.ResendMessageAsync(IntegrationLog.Id)
            Dim response = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(jsonResponse)

            Dim status = response("status")
            Dim message = response("message")

            If status = "Success" Then
                Mensaje(EeventViewerImages.Informacion) = message
            Else
                Mensaje(EeventViewerImages.MensajeError) = message
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub



#End Region

#Region "FrmClosing"
    Private Sub FrmIntegrationErrorLogs_FormClosed(sender As Object, e As Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        INDGcIntegrationTransactionDetails.DataSource = Nothing
        INDGcIntegrationValid.DataSource = Nothing
        INDGcIntegrationInvalid.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(PATH_ORIGIN, Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDBtsAutomaticRefresh), "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(PATH_ORIGIN, Path.Combine(Window.Utils.GetPathUserFiles(), "Preferences"), NameOf(INDBtsAutomaticRefresh), ""))
        End If

        Dim automaticReloadPath As String = Me.AutomaticReloadPath
        If File.Exists(automaticReloadPath) Then
            File.Delete(automaticReloadPath)
        End If

        Using ms As New MemoryStream()
            Dim xs = New System.Xml.Serialization.XmlSerializer(GetType(AutomaticReload))
            Dim dictionaryCustom = New AutomaticReload
            dictionaryCustom.Name = NameOf(AutomaticReload)
            dictionaryCustom.Value = Me.AutomaticReload
            xs.Serialize(ms, dictionaryCustom)
            File.WriteAllText(automaticReloadPath, System.Text.Encoding.UTF8.GetString(ms.ToArray()).Trim())
        End Using
    End Sub
#End Region



#Region "ItemClick"
    Private Async Sub INDBbiRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        Await Me.RefreshDataSource()
    End Sub

#End Region
#Region "SelectedPageChanged"
    Private Async Sub INDTcgTransactionMessages_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgTransactionMessages.SelectedPageChanged
        Await Me.RefreshDataSource(e.Page.Name)
    End Sub


#End Region

#Region "Toggle"
    ''' <summary>
    ''' evento del boton toggle cuando cambia de estado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtsAutomaticRefresh_Toggled(sender As Object, e As EventArgs) Handles INDBtsAutomaticRefresh.CheckedChanged
        If Not Me.AutomaticReload Then
            _timer.Stop()
            _timer.Enabled = False
        Else
            _timer.AutoReset = True
            _timer.Enabled = True
        End If
    End Sub


    ''' <summary>
    ''' creacion del timer, programado a 2 minutos
    ''' </summary>
    Private Sub ExecuteTimer()
        If _timer Is Nothing OrElse Not _timer.Enabled Then
            _timer = New System.Timers.Timer(300000)
            AddHandler _timer.Elapsed, AddressOf OnTimedEvent
            _timer.SynchronizingObject = Me
            _timer.AutoReset = False
            _timer.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' evento timer que llama a refrescar la rejilla
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    Private Async Sub OnTimedEvent(source As Object, e As ElapsedEventArgs)
        Me._flagTimer = True
        Await Me.RefreshDataSource()
        Me._flagTimer = False
    End Sub
#End Region

#End Region

End Class