'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 01-03-2024
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"

Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls

#End Region

Public Class FrmElectronicRIPSTraceability
    Implements IElectronicRIPSTraceability

#Region "Variables"

    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PElectronicRIPSTraceability

    Private _isPopupMenuShowing As Boolean

    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)

    ''' <summary>
    ''' Parrametros de Facturacion
    ''' </summary>
    Private SettingBilling As SettingsBilling

    Private _toogleFlag As Boolean
    Private _loadingCount As Integer = 0
    Private _isLoadingCompleted As Boolean = False
    Public Event LoadingCompleted()

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IElectronicRIPSTraceability.MyTag
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

    Public ReadOnly Property OperatingUnitId As Integer Implements IElectronicRIPSTraceability.OperatingUnitId
        Get
            Return INDSleOperatingUnit.EditValue
        End Get
    End Property

    Public ReadOnly Property Status As String Implements IElectronicRIPSTraceability.Status
        Get
            Return INDCcbeStatus.EditValue
        End Get
    End Property

#End Region

#Region "Datasource"

    ''' <summary>
    ''' Obtiene las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    Public Property OperatingUnitXpo As List(Of Domain.Entities.OperatingUnit) Implements IElectronicRIPSTraceability.OperatingUnitXpo
        Get
            Return CType(INDSleOperatingUnit.Properties.DataSource, List(Of Domain.Entities.OperatingUnit))
        End Get
        Set(value As List(Of Domain.Entities.OperatingUnit))
            INDSleOperatingUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene los erips que ya se generaron
    ''' </summary>
    ''' <returns></returns>
    Public Property GeneratedRIPSXpo As XPInstantFeedbackSource Implements IElectronicRIPSTraceability.GeneratedRIPSXpo
        Get
            Return CType(INDGcGeneratedRIPS.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcGeneratedRIPS.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene las notas debito/credito de los erips
    ''' </summary>
    ''' <returns></returns>
    Public Property DebitCreditNoteRIPSXpo As XPInstantFeedbackSource Implements IElectronicRIPSTraceability.DebitCreditNoteRIPSXpo
        Get
            Return CType(INDGcDebitCreditNoteRIPS.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcDebitCreditNoteRIPS.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene las notas de ajuste de los erips
    ''' </summary>
    ''' <returns></returns>
    Public Property AdjustmentNoteRIPSXpo As XPInstantFeedbackSource Implements IElectronicRIPSTraceability.AdjustmentNoteRIPSXpo
        Get
            Return CType(INDGcAdjustmentNoteRIPS.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcAdjustmentNoteRIPS.DataSource = value
        End Set
    End Property


#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        GeneratedRIPSXpo = Nothing
        DebitCreditNoteRIPSXpo = Nothing
        AdjustmentNoteRIPSXpo = Nothing
        _toogleFlag = False
        _isLoadingCompleted = True
        _loadingCount = 0
        _selectorGeneratedRIPS.Clear()
        _selectorDebitCreditNoteRIPS.Clear()
        _selectorAdjustmentNoteRIPS.Clear()
    End Sub

    ''' <summary>
    ''' Recarga la informacion de la seccion seleccionada
    ''' </summary>
    ''' <param name="selectedPage"></param>
    Private Sub BeginReloadDatasource(selectedPage As String)
        If OperatingUnitId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione una Unidad Operativa"
            Exit Sub
        End If

        Select Case selectedPage
            Case INDLcgGeneratedRIPS.Name 'Facturas rips 
                If Me.GeneratedRIPSXpo Is Nothing Then
                    Me._presenter.GetGeneratedRIPSInvoiceTap(Me.OperatingUnitId)
                End If
            Case INDLcgDebitCreditNoteRIPS.Name 'Notas Debito/ Credito RIPS
                If Me.DebitCreditNoteRIPSXpo Is Nothing Then
                    Me._presenter.GetDebitCreditNoteRIPS(Me.OperatingUnitId, "BillingNote")
                End If
            Case INDLcgAdjustmentNoteRIPS.Name 'Notas de ajuste RIPS
                If Me.AdjustmentNoteRIPSXpo Is Nothing Then
                    Me._presenter.GetAdjustmentNoteRIPS(Me.OperatingUnitId, "BillingNoteAdjustment")
                End If
        End Select
    End Sub

#Region "RefreshFRM"
    ''' <summary>
    ''' Refresca los datos del Formulario cada 5 minutos
    ''' </summary>
    Private Sub SetupAutoRefresh()
        Dim refreshTimer As New Timer()
        refreshTimer.Interval = 5 * 60 * 1000
        AddHandler refreshTimer.Tick, Sub(sender, e)
                                          If INDGvGeneratedRIPS IsNot Nothing Then
                                              INDGvGeneratedRIPS.RefreshData()
                                          End If

                                          If INDGvDebitCreditNoteRIPS IsNot Nothing Then
                                              INDGvDebitCreditNoteRIPS.RefreshData()
                                          End If

                                          If INDGvAdjustmentNoteRIPS IsNot Nothing Then
                                              INDGvAdjustmentNoteRIPS.RefreshData()
                                          End If
                                      End Sub

        refreshTimer.Start()

        AddHandler Me.FormClosing, Sub(sender, e)
                                       refreshTimer.Stop()
                                   End Sub
    End Sub

#End Region
#Region "Selector"

    Private _selectorGeneratedRIPS As SelectorCache = New SelectorCache("Id", "StatusRIPS", "DocumentNumber", "CosmoDBId", "DocumentDate", "PatientCodeName", "AdmissionNumber", "CareGroupCodeName", "ThirdPartyNitName", "EntityName")
    Private _selectorDebitCreditNoteRIPS As SelectorCache = New SelectorCache("Id", "StatusRIPS", "DocumentNumber", "CosmoDBId", "DocumentDate", "PatientCodeName", "AdmissionNumber", "CareGroupCodeName", "ThirdPartyNitName", "EntityName")
    Private _selectorAdjustmentNoteRIPS As SelectorCache = New SelectorCache("Id", "StatusRIPS", "DocumentNumber", "CosmoDBId", "DocumentDate", "PatientCodeName", "AdmissionNumber", "CareGroupCodeName", "ThirdPartyNitName", "EntityName")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvGeneratedRIPS.CustomUnboundColumnData, INDGvDebitCreditNoteRIPS.CustomUnboundColumnData, INDGvAdjustmentNoteRIPS.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvGeneratedRIPS" Then
                e.Value = _selectorGeneratedRIPS.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvDebitCreditNoteRIPS" Then
                e.Value = _selectorDebitCreditNoteRIPS.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvAdjustmentNoteRIPS" Then
                e.Value = _selectorAdjustmentNoteRIPS.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvGeneratedRIPS.RowCellClick, INDGvDebitCreditNoteRIPS.RowCellClick, INDGvAdjustmentNoteRIPS.RowCellClick
        If Not Me._isPopupMenuShowing Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvGeneratedRIPS" Then
                selector = _selectorGeneratedRIPS
            ElseIf view.Name = "INDGvDebitCreditNoteRIPS" Then
                selector = _selectorDebitCreditNoteRIPS
            ElseIf view.Name = "INDGvAdjustmentNoteRIPS" Then
                selector = _selectorAdjustmentNoteRIPS
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)
                view.RefreshRow(e.RowHandle)
            Else
                selector.Clear()
                view.RefreshData()
            End If
        End If

        Me._isPopupMenuShowing = False
    End Sub

#End Region

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Sub FrmElectronicRIPSTraceability_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        Me._presenter = New PElectronicRIPSTraceability(Me)
        Me._presenter.InitializeOperatingUnitXpo()

        SetupAutoRefresh()
        AddHandler Me.RowLoaded, AddressOf OnRowLoaded
        AddHandler Me.LoadingCompleted, AddressOf Me.CompleteLoadingCheck
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmElectronicRIPSTraceability_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        INDBbiRefresh.Visibility = IIf(BarraBotones.PermissionsForm.ContainsKey(40), BarItemVisibility.Always, BarItemVisibility.Never)

        INDSleOperatingUnit.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDSleOperatingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleOperatingUnit.EditValueChanged
        Me.CleanControls()
        Me.BeginReloadDatasource(INDTcgElectronicRIPS.SelectedTabPageName)
        Using model As New MBillingSetting(Me.Tag)
            SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(OperatingUnitId, False)
        End Using
    End Sub

#End Region

#Region "SelectedPageChanged"

    Private Sub INDTcgElectronicDocument_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgElectronicRIPS.SelectedPageChanged
        BeginReloadDatasource(e.Page.Name)
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para las rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvGeneratedRIPS.PopupMenuShowing, INDGvDebitCreditNoteRIPS.PopupMenuShowing, INDGvAdjustmentNoteRIPS.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        INDBbiPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Dim selector = GetSelector(True)

        If selector.Count > 0 Then
            INDBbiPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            If BarraBotones.PermissionsForm.ContainsKey(81) Then 'si tiene permiso de procesar
                For Each key In selector.GetKeysToArray()
                    If selector.GetValueByKey(key, "Status") <> "3" Then
                        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        Exit For
                    End If
                Next
            End If

        End If

        Me._isPopupMenuShowing = True
        Dim view = CType(sender, GridView)
        view.RefreshRow(e.HitInfo.RowHandle)
        INDPopMenuActions2.Manager = BarManager2
        INDPopMenuActions2.ShowPopup(view.GridControl.PointToScreen(e.Point))
    End Sub

    Private Function GetSelector(selection As Boolean) As SelectorCache
        Dim selector As New SelectorCache("", "")
        Select Case INDTcgElectronicRIPS.SelectedTabPageName
            Case INDLcgGeneratedRIPS.Name 'Facturas
                If selection Then
                    _selectorGeneratedRIPS.SetValue(INDGvGeneratedRIPS.GetFocusedRow, True)
                End If
                selector = _selectorGeneratedRIPS
            Case INDLcgDebitCreditNoteRIPS.Name 'Notas Debito / Credito
                If selection Then
                    _selectorDebitCreditNoteRIPS.SetValue(INDGvDebitCreditNoteRIPS.GetFocusedRow, True)
                End If
                selector = _selectorDebitCreditNoteRIPS
            Case INDLcgAdjustmentNoteRIPS.Name 'Notas Ajuste
                If selection Then
                    _selectorAdjustmentNoteRIPS.SetValue(INDGvAdjustmentNoteRIPS.GetFocusedRow, True)
                End If
                selector = _selectorAdjustmentNoteRIPS
        End Select
        Return selector
    End Function

#End Region

#Region "ItemClick"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiDownloadJson_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiDownloadJson.ItemClick, INDBbiDownloadJsonWithXml.ItemClick

        Dim button = TryCast(e.Item, BarButtonItem)
        Dim isNative = button.Name = NameOf(INDBbiDownloadJson)
        ' se obtiene en qué tab está el usuario
        Dim selectedTabPageName As String = INDTcgElectronicRIPS.SelectedTabPageName

        ' dependiendo del tipo de documento se crea su evento de reenvío
        Select Case selectedTabPageName
            Case "INDLcgGeneratedRIPS"
                Await DownLoadFevRIPSJson(Me._selectorGeneratedRIPS, isNative)
            Case "INDLcgDebitCreditNoteRIPS"
                Await DownLoadFevRIPSJson(Me._selectorDebitCreditNoteRIPS, isNative)
            Case "INDLcgAdjustmentNoteRIPS"
                Await DownLoadFevRIPSJson(Me._selectorAdjustmentNoteRIPS, isNative)
        End Select
    End Sub

    Private Async Function DownLoadFevRIPSJson(selector As SelectorCache, isNative As Boolean) As Task
        Dim formProgress As FrmProgressPanel = Nothing
        Try
            AsyncLoader(True)

            If Not _isLoadingCompleted Then
                Mensaje(EeventViewerImages.Advertencia) = "Aún hay elementos pendientes de marcar en la rejilla. Espere a que finalice el proceso."
                Return
            End If

            Using Model As New MElectronicRIPSTraceability(Me.Tag)
                ' se envían los datos para ejecutar el método de publicar
                Dim stringBuilder = New StringBuilder()
                Dim listDocuments = New Dictionary(Of String, String)

                If selector.ValuesCache.Count > 3 Then
                    formProgress = New FrmProgressPanel(selector.ValuesCache.Count)
                    formProgress.LayoutControlName = "Descarga Json RIPS"
                    formProgress.WindowState = FormWindowState.Normal
                    formProgress.TopMost = False
                    formProgress.BringToFront()
                    formProgress.Show(Me)
                End If

                Dim j As Integer = 1
                For Each item As KeyValuePair(Of Object, Dictionary(Of String, Object)) In selector.ValuesCache
                    Dim cosmosId As String = If(item.Value.ToList().Item(2).Value?.ToString(), "").Trim()
                    Dim docNumber As String = If(item.Value.ToList().Item(1).Value?.ToString(), "").Trim()

                    If _selectorGeneratedRIPS.ValuesCache.Count > 3 AndAlso (formProgress Is Nothing OrElse formProgress.IsDisposed) Then
                        listDocuments.Clear()
                        Return
                    End If

                    If formProgress IsNot Nothing AndAlso Not formProgress.IsDisposed Then
                        formProgress.SafeInvoke(Sub()
                                                    formProgress.UpdateProgressBar(j, docNumber)
                                                End Sub)
                        formProgress.Update()
                    End If
                    j += 1

                    Dim result As Domain.Base.Entities.ActionResult(Of String)

                    If Not String.IsNullOrEmpty(cosmosId) Then
                        result = Await Model.GetJsonRIPSById(cosmosId)
                    ElseIf Not String.IsNullOrEmpty(docNumber) Then
                        result = Await Model.GetJsonRIPSByDocNumber(docNumber)
                    Else
                        stringBuilder.AppendLine($"No se encontró identificador ni número de documento para el registro.")
                        Continue For
                    End If

                    If Not result.StateResult Then
                        Dim displayRef = If(Not String.IsNullOrEmpty(docNumber), docNumber, "Documento")
                        stringBuilder.AppendLine($"Documento {displayRef}: {result.Message}")
                        Continue For
                    End If

                    If String.IsNullOrEmpty(result.ObjectEmbbeded) Then
                        Dim displayRef = If(Not String.IsNullOrEmpty(docNumber), docNumber, "Documento")
                        stringBuilder.AppendLine($"Documento {displayRef}: No se obtuvo contenido JSON del servicio.")
                        Continue For
                    End If

                    listDocuments.Add(docNumber, result.ObjectEmbbeded)
                Next

                If stringBuilder.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = stringBuilder.ToString()
                End If

                If listDocuments?.Any() Then
                    Dim Folder As New FolderBrowserDialog
                    Dim fileNameBuilder = New StringBuilder()
                    If Folder.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                        ' se recorre cada documento para procesar el JSON y el XML
                        For Each i In listDocuments
                            ' parsea el JSON usando Newtonsoft.Json.Linq
                            Dim fileNameResult = String.Empty
                            Dim attachmentValue As String = String.Empty

                            Dim jsonObj As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.Linq.JObject.Parse(i.Value)
                            Dim jsonRIPS As Newtonsoft.Json.Linq.JObject = jsonObj("JsonRIPS")
                            Dim jsonCUVResponse As String = jsonObj.SelectToken("DocumentsAssociatedRIPS.Data")?.ToString()

                            ' extrae la propiedad "attachment" si existe y luego la elimina
                            If jsonRIPS("xmlFevFile") IsNot Nothing AndAlso Not isNative Then
                                Dim jsonAlternative = jsonRIPS("rips")
                                attachmentValue = CStr(jsonRIPS("xmlFevFile"))
                                jsonRIPS.Remove("xmlFevFile")
                                jsonRIPS.Remove("rips")
                                jsonRIPS = If(Not (jsonAlternative).HasValues, New Newtonsoft.Json.Linq.JObject(), jsonAlternative)
                            End If

                            ' escribe el archivo JSON sin la etiqueta "attachment"
                            Dim jsonFilePath As String = Path.Combine(Folder.SelectedPath, i.Key & ".json")
                            File.WriteAllText(jsonFilePath, jsonRIPS.ToString())
                            fileNameResult = $"{i.Key}.json"

                            If Not String.IsNullOrEmpty(jsonCUVResponse) AndAlso Not isNative Then
                                Dim cuvFilePath As String = Path.Combine(Folder.SelectedPath, i.Key & "_CUV.json")
                                File.WriteAllText(cuvFilePath, jsonCUVResponse)
                                fileNameResult &= $", {i.Key}_CUV.json"
                            End If

                            ' si existe contenido en "attachment", decodifícalo y escribe el XML
                            If Not String.IsNullOrEmpty(attachmentValue) Then
                                Try
                                    Dim xmlBytes() As Byte = Convert.FromBase64String(attachmentValue)
                                    Dim xmlString As String = System.Text.Encoding.UTF8.GetString(xmlBytes)
                                    Dim xmlFilePath As String = Path.Combine(Folder.SelectedPath, i.Key & ".xml")
                                    File.WriteAllText(xmlFilePath, xmlString)
                                    fileNameResult &= $" y {i.Key}.xml"
                                Catch exBase64 As FormatException
                                    ' en caso de error en la conversión de Base64, se registra el error y continúa
                                    fileNameBuilder.AppendLine($"{i.Key}.Json (error en conversión de XML desde Base64)")
                                End Try
                            End If

                            fileNameBuilder.AppendLine(fileNameResult)
                        Next
                        Mensaje(EeventViewerImages.Informacion) = $"Se han creado los Archivos {fileNameBuilder.ToString()} correctamente"
                    End If
                End If
            End Using

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            If formProgress IsNot Nothing AndAlso Not formProgress.IsDisposed Then
                formProgress.Close()
            End If
            AsyncLoader(False)
        End Try
    End Function

    ''' <summary>
    ''' evento para marcar o desmarcar lo visible en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGcGeneratedRIPS_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDGcGeneratedRIPS.MouseDoubleClick, INDGcDebitCreditNoteRIPS.MouseDoubleClick, INDGcAdjustmentNoteRIPS.MouseDoubleClick

        Dim control = TryCast(sender, GridControl)
        Dim view = TryCast(control.DefaultView, GridView)
        Dim hitPoint = view.CalcHitInfo(e.Location)

        If hitPoint.Column IsNot Nothing Then

            Dim flagColumn As Boolean = False
            flagColumn = hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals(GetGridViewInfo(view).Item2)

            If flagColumn Then
                If view.RowCount > 0 Then
                    Me._toogleFlag = Not _toogleFlag
                End If

                ' Inicializar el contador antes de empezar a cargar
                _loadingCount = view.DataRowCount
                _isLoadingCompleted = False

                For i As Integer = 0 To view.DataRowCount - 1
                    Dim rowHandle As Integer = view.GetRowHandle(i) ' Obtiene el índice visible
                    If view.IsDataRow(rowHandle) Then
                        LoadRowAsync(view, rowHandle)
                    End If
                Next
            End If
        End If
    End Sub


    Public Event RowLoaded(ByVal values As Object, gridView As GridView)

    ' Método que carga la fila y dispara el evento
    Public Sub LoadRowAsync(ByVal view As GridView, ByVal rowHandle As Integer)
        If view IsNot Nothing Then
            view.EnsureRowLoaded(rowHandle, Sub(values)
                                                ' Cuando la fila se cargue, dispara el evento
                                                RaiseEvent RowLoaded(values, view)
                                                view.RefreshRow(rowHandle)
                                                CheckIfLoadingCompleted() ' Verifica si ya se cargaron todas las filas
                                            End Sub)

        End If
    End Sub


    Private Sub OnRowLoaded(ByVal values As Object, gridView As GridView)
        Dim selector As SelectorCache = GetGridViewInfo(gridView).Item1
        If values IsNot Nothing Then
            selector.SetValue(values, Me._toogleFlag)
        End If
    End Sub

    Function GetGridViewInfo(gridView As GridView) As (SelectorCache, String)
        Dim selector As SelectorCache = Nothing
        Dim columnName As String = String.Empty
        Select Case gridView.Name
            Case "INDGvGeneratedRIPS"
                selector = _selectorGeneratedRIPS
                columnName = INDGvValidatedRIPS_UnboundSelection.Name
            Case "INDGvDebitCreditNoteRIPS"
                selector = _selectorDebitCreditNoteRIPS
                columnName = INDGvDebitNote_UnboundSelection.Name
            Case "INDGvAdjustmentNoteRIPS"
                selector = _selectorAdjustmentNoteRIPS
                columnName = INDGvAdjustmentNoteRIPS_UnboundSelection.Name
        End Select
        Return (selector, columnName)
    End Function


    Private Sub CheckIfLoadingCompleted()
        SyncLock Me
            _loadingCount -= 1
            If _loadingCount <= 0 Then
                _isLoadingCompleted = True
                RaiseEvent LoadingCompleted() ' Dispara un evento cuando todo se haya cargado
            End If
        End SyncLock
    End Sub

    Private Sub CompleteLoadingCheck()
        Mensaje(EeventViewerImages.Informacion) = $"Se terminó de {If(Not _toogleFlag, "desmarcar", "seleccionar")} el conjunto de datos de la rejilla"
    End Sub

    Private Sub INDBbiRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        Me.CleanControls()
        Me.BeginReloadDatasource(INDTcgElectronicRIPS.SelectedTabPageName)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al reenviar la el rips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiReSend_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiReSend.ItemClick
        ''se obtiene en que tab esta el usuario
        Dim selectedTabPageName As String = INDTcgElectronicRIPS.SelectedTabPageName

        ''dependiendo del tipo de documento se crea su evento de reenvio
        Select Case selectedTabPageName
            Case "INDLcgGeneratedRIPS"
                If MessageIndigo.Show("¿Desea reenviar la factura?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MElectronicRIPSTraceability(Me.Tag)
                        ''se envian los datos para ejecutar el metodo de publicar
                        Dim invoiceList As New List(Of String)

                        Dim itemFirst = GetSelectedRipsItems.FirstOrDefault()
                        If Not GetSelectedRipsItems.All(Function(x) x.EntityName = itemFirst.EntityName) Then
                            Mensaje(EeventViewerImages.Advertencia) = "Solo se puede re enviar Elemento de un mismo Tipo"
                        End If

                        ''se obtienen los numeros del documento del selector
                        For Each item As KeyValuePair(Of Object, Dictionary(Of String, Object)) In _selectorGeneratedRIPS.ValuesCache
                            Dim propertyValue = item.Value.ToList().Item(1).Value
                            Dim statusRIPS = item.Value.ToList().Item(0).Value

                            If statusRIPS <> EStatusERIPS.ValidateSuccess Then
                                invoiceList.Add(propertyValue)
                            End If
                        Next

                        Dim result = Await Model.ReSendElectronicRIPS($"Resend{itemFirst.EntityName}", invoiceList)
                        If result?.StateResult Then
                            MessageIndigo.Show(result.Message, MessageType.Information, Me.Text)
                            Me.CleanControls()
                            Me.BeginReloadDatasource(INDTcgElectronicRIPS.SelectedTabPageName)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result?.Message
                        End If
                    End Using
                End If
            Case "INDLcgDebitCreditNoteRIPS"
                If MessageIndigo.Show("¿Desea reenviar la nota?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MElectronicRIPSTraceability(Me.Tag)
                        ''se envian los datos para ejecutar el metodo de publicar
                        Dim DebitCreditNoteList As New List(Of String)

                        ''se obtienen los numeros del documento del selector
                        For Each item As KeyValuePair(Of Object, Dictionary(Of String, Object)) In _selectorDebitCreditNoteRIPS.ValuesCache
                            Dim propertyValue = item.Value.ToList().Item(1).Value
                            Dim statusRIPS = item.Value.ToList().Item(0).Value

                            If statusRIPS <> EStatusERIPS.ValidateSuccess Then
                                DebitCreditNoteList.Add(propertyValue)
                            End If
                        Next

                        Dim result = Await Model.ReSendElectronicRIPS("ResendBillingNote", DebitCreditNoteList)
                        If result?.StateResult Then
                            MessageIndigo.Show(result.Message, MessageType.Information, Me.Text)
                            Me.CleanControls()
                            Me.BeginReloadDatasource(INDTcgElectronicRIPS.SelectedTabPageName)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result?.Message
                        End If
                    End Using
                End If

            Case "INDLcgAdjustmentNoteRIPS"
                If MessageIndigo.Show("¿Desea reenviar la nota de ajuste?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MElectronicRIPSTraceability(Me.Tag)
                        ''se envian los datos para ejecutar el metodo de publicar
                        Dim AdjustmentNoteList As New List(Of String)

                        ''se obtienen los numeros del documento del selector
                        For Each item As KeyValuePair(Of Object, Dictionary(Of String, Object)) In _selectorAdjustmentNoteRIPS.ValuesCache
                            Dim propertyValue = item.Value.ToList().Item(1).Value
                            Dim statusRIPS = item.Value.ToList().Item(0).Value

                            If statusRIPS <> EStatusERIPS.ValidateSuccess Then
                                AdjustmentNoteList.Add(propertyValue)
                            End If
                        Next

                        Dim result = Await Model.ReSendElectronicRIPS("ResendBillingNoteAdjustment", AdjustmentNoteList)
                        If result?.StateResult Then
                            MessageIndigo.Show(result.Message, MessageType.Information, Me.Text)
                            Me.CleanControls()
                            Me.BeginReloadDatasource(INDTcgElectronicRIPS.SelectedTabPageName)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result?.Message
                        End If
                    End Using
                End If

        End Select
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al generar nota de ajuste al RIPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiReGenerateNote_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiReGenerateNote.ItemClick
        Using formulario As New FrmPopUpAdjustmentNoteRIPS
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
            formulario.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            formulario.ripsTraceabilityList = GetSelectedRipsItems()
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub


    ''' <summary>
    ''' Prepara los items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private Function GetSelectedRipsItems() As List(Of Object)
        Dim ripsTraceabilityList As New List(Of Object)
        Select Case INDTcgElectronicRIPS.SelectedTabPageName
            Case "INDLcgGeneratedRIPS"
                For Each item As KeyValuePair(Of Object, Dictionary(Of String, Object)) In _selectorGeneratedRIPS.ValuesCache
                    Dim ripsRecordObj As New With {
                    Key .Id = item.Key,
                    Key .DocumentNumber = item.Value.ToList().Item(1).Value,
                    Key .CosmoDBId = item.Value.ToList().Item(2).Value,
                    Key .DocumentDate = item.Value.ToList().Item(3).Value,
                    Key .PatientCodeName = item.Value.ToList().Item(4).Value,
                    Key .AdmissionNumber = item.Value.ToList().Item(5).Value,
                    Key .CareGroupCodeName = item.Value.ToList().Item(6).Value,
                    Key .ThirdPartyNitName = item.Value.ToList().Item(7).Value,
                    Key .EntityName = item.Value.ToList().Item(8).Value
                    }
                    ripsTraceabilityList.Add(ripsRecordObj)
                Next
        End Select
        Return ripsTraceabilityList
    End Function

#End Region

#Region "ShowingEditor"

    Private Sub INDGvGeneratedRIPS_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvGeneratedRIPS.ShowingEditor
        INDGvInvoice_PceDetails.PopupControl = Nothing
        INDGvDebitNote_PceDetails.PopupControl = Nothing
        INDGvCreditNote_PceDetails.PopupControl = Nothing
        INDGcElectronicRIPSDetails.DataSource = Nothing

        Dim electronicRIPS = CType(INDGvGeneratedRIPS.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicRIPSDetails.DataSource = Me._presenter.GetDetails(electronicRIPS.Id)
        INDGvInvoice_PceDetails.PopupControl = INDPccElectronicRIPSDetails
    End Sub

    Private Sub INDGvDebitCreditNoteRIPS_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvDebitCreditNoteRIPS.ShowingEditor
        INDGvInvoice_PceDetails.PopupControl = Nothing
        INDGvDebitNote_PceDetails.PopupControl = Nothing
        INDGvCreditNote_PceDetails.PopupControl = Nothing
        INDGcElectronicRIPSDetails.DataSource = Nothing

        Dim electronicRIPS = CType(INDGvDebitCreditNoteRIPS.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicRIPSDetails.DataSource = Me._presenter.GetDetails(electronicRIPS.Id)
        INDGvDebitNote_PceDetails.PopupControl = INDPccElectronicRIPSDetails
    End Sub

    Private Sub INDGvAdjustmentNoteRIPS_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvAdjustmentNoteRIPS.ShowingEditor
        INDGvInvoice_PceDetails.PopupControl = Nothing
        INDGvDebitNote_PceDetails.PopupControl = Nothing
        INDGvCreditNote_PceDetails.PopupControl = Nothing
        INDGcElectronicRIPSDetails.DataSource = Nothing

        Dim electronicRIPS = CType(INDGvAdjustmentNoteRIPS.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicRIPSDetails.DataSource = Me._presenter.GetDetails(electronicRIPS.Id)
        INDGvCreditNote_PceDetails.PopupControl = INDPccElectronicRIPSDetails
    End Sub

#End Region

#End Region

End Class