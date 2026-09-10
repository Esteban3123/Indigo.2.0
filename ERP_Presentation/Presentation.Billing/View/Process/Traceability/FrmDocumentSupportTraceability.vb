#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

Public Class FrmDocumentSupportTraceability
    Implements IDocumentSupportTraceability

#Region "Variables"

    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PDocumentSupportTraceability

    Private _isPopupMenuShowing As Boolean

    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)

#End Region

#Region "Enums"
    Public Enum eStatusElectronic
        invalid
        register
        send
        validated
        validatedFailed
    End Enum
#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IDocumentSupportTraceability.MyTag
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

    Public ReadOnly Property OperatingUnitId As Integer Implements IDocumentSupportTraceability.OperatingUnitId
        Get
            Return INDSleOperatingUnit.EditValue
        End Get
    End Property

    Public ReadOnly Property Status As String Implements IDocumentSupportTraceability.Status
        Get
            Return INDCcbeStatus.EditValue
        End Get
    End Property

#End Region

#Region "Datasource"

    Public Property OperatingUnitXpo As List(Of Domain.Entities.OperatingUnit) Implements IDocumentSupportTraceability.OperatingUnitXpo
        Get
            Return CType(INDSleOperatingUnit.Properties.DataSource, List(Of Domain.Entities.OperatingUnit))
        End Get
        Set(value As List(Of Domain.Entities.OperatingUnit))
            INDSleOperatingUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property ElectronicSupportDocumentXpo As XPInstantFeedbackSource Implements IDocumentSupportTraceability.ElectronicSupportDocumentXpo
        Get
            Return CType(INDGcDocumentSupport.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcDocumentSupport.DataSource = value
        End Set
    End Property

    Public Property AdjustmentNoteXpo As XPInstantFeedbackSource Implements IDocumentSupportTraceability.AdjustmentNoteXpo
        Get
            Return CType(INDGcNoteAdjustment.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcNoteAdjustment.DataSource = value
        End Set
    End Property

#End Region

#Region "BarraBotones"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub

#End Region

#Region "Methods"

    Private Sub CleanControls()

        _selectorSupportDocument.Clear()
        _selectorNoteAdjustment.Clear()
        Me.ElectronicSupportDocumentXpo = Nothing
        Me.AdjustmentNoteXpo = Nothing

    End Sub

    Private Sub BeginReloadDatasource(selectedPage As String)
        If OperatingUnitId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione una Unidad Operativa"
            Exit Sub
        End If
        Me.CleanControls()
        Select Case selectedPage
            Case INDLcgDocumentSupport.Name 'Documento Soporte
                If Me.ElectronicSupportDocumentXpo Is Nothing Then
                    Me._presenter.GetElectronicSupportDocument(Me.OperatingUnitId)
                End If
            Case INDLcgNoteAdjustment.Name 'Nota Ajuste
                If Me.AdjustmentNoteXpo Is Nothing Then
                    Me._presenter.GetElectronicSupportDocumentAdjustmentNote(Me.OperatingUnitId)
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al retornar el modal de eventos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnModalArgs(sender As Object, e As EventArgs)
        Dim selector = GetSelector(False)
        selector.Clear()

        Select Case INDTcgElectronicDocument.SelectedTabPageName
            Case INDLcgDocumentSupport.Name 'Documento Soporte
                INDGvDocumentSupport.RefreshData()
            Case INDLcgNoteAdjustment.Name 'Nota Ajuste
                INDGvNoteAdjustment.RefreshData()
        End Select
    End Sub

    Private Sub OpenFormSendElectronicDocumentNotification(listElectronicDocuments As List(Of Domain.Entities.ElectronicDocument))
        Using formulario As New FrmSendElectronicDocumentNotification()
            AddHandler formulario.ReturnModalArgs, AddressOf ReturnModalArgs
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.ListElectronicDocuments = listElectronicDocuments
            Dim frm As New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

#Region "Selector"

    Private _selectorSupportDocument As SelectorCache = New SelectorCache("Id", "StatusElectronic", "EntityId", "EntityName", "SupplierThirdPartyId")
    Private _selectorNoteAdjustment As SelectorCache = New SelectorCache("Id", "StatusElectronic", "EntityId", "EntityName", "SupplierThirdPartyId")


    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvDocumentSupport.CustomUnboundColumnData, INDGvNoteAdjustment.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = INDGvDocumentSupport.Name Then
                e.Value = _selectorSupportDocument.ValidateExistsRow(e.Row)
            ElseIf view.Name = INDGvNoteAdjustment.Name Then
                e.Value = _selectorNoteAdjustment.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la columna de seleccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles INDGvDocumentSupport.RowCellClick
        If e.Column.FieldName.Contains("INDGvDocumentSupport_UnboundSelection") Then

            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvDocumentSupport" Then
                selector = _selectorSupportDocument

                If e.RowHandle >= 0 Then
                    Dim row = view.GetRow(e.RowHandle)
                    selector.SetValue(row)
                Else
                    If _selectorSupportDocument.Count = 0 Then
                        view.SelectAll()

                        For Each item In view.GetSelectedRows()
                            Dim row = view.GetRow(item)
                            selector.SetValue(row)
                        Next
                    Else
                        selector.Clear()
                    End If
                End If

                view.RefreshData()
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Sub FrmElectronicDocumentTraceability_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        Me._presenter = New PDocumentSupportTraceability(Me)
        Me._presenter.InitializeOperatingUnitXpo()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmElectronicDocumentTraceability_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        INDBbiRefresh.Visibility = IIf(BarraBotones.PermissionsForm.ContainsKey(40), BarItemVisibility.Always, BarItemVisibility.Never)

        INDSleOperatingUnit.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleOperatingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleOperatingUnit.EditValueChanged
        Me.CleanControls()
        Me.BeginReloadDatasource(INDTcgElectronicDocument.SelectedTabPageName)
    End Sub

#End Region

#Region "SelectedPageChanged"

    Private Sub INDTcgElectronicDocument_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDTcgElectronicDocument.SelectedPageChanged
        BeginReloadDatasource(e.Page.Name)
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para las rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvDocumentSupport.PopupMenuShowing, INDGvNoteAdjustment.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Dim selector = _selectorSupportDocument
        If selector.Count > 0 Then
            If BarraBotones.PermissionsForm.ContainsKey(81) Then 'si tiene permiso de procesar
                For Each key In selector.GetKeysToArray()
                    If selector.GetValueByKey(key, "StatusElectronic") <> eStatusElectronic.validated Then
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
        Select Case INDTcgElectronicDocument.SelectedTabPageName
            Case INDLcgDocumentSupport.Name 'Documento Soporte
                If selection Then
                    _selectorSupportDocument.SetValue(INDGvDocumentSupport.GetFocusedRow, True)
                End If
                selector = _selectorSupportDocument
            Case INDLcgNoteAdjustment.Name 'Nota Ajuste
                If selection Then
                    _selectorNoteAdjustment.SetValue(INDGvNoteAdjustment.GetFocusedRow, True)
                End If
                selector = _selectorNoteAdjustment

        End Select
        Return selector
    End Function

    Private Function GetView(selectedPage As String) As GridView
        Dim view As New GridView
        Select Case selectedPage
            Case INDLcgDocumentSupport.Name 'Documento Soporte
                view = INDGvDocumentSupport
            Case INDLcgNoteAdjustment.Name 'Notas de ajuste
                view = INDGvNoteAdjustment
        End Select
        Return view
    End Function

#End Region

#Region "ItemClick"

    Private Sub INDBbiSelection_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiSelection.ItemClick
        Dim selector = GetSelector(False)
        Dim view = GetView(INDTcgElectronicDocument.SelectedTabPageName)

        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                selector.SetValue(view.GetRow(item), True)
                view.RefreshRow(item)
            End If
        Next
        view.RefreshData()
    End Sub

    Private Sub INDBbiUnSelection_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiSelection.ItemClick, INDBbiUnSelection.ItemClick
        Dim selector = GetSelector(False)
        Dim view = GetView(INDTcgElectronicDocument.SelectedTabPageName)
        For Each Item In view.GetSelectedRows()
            If Item > -1 Then
                selector.UnSetValue(view.GetRow(Item))
                view.RefreshRow(Item)
            End If
        Next
    End Sub

    Private Sub INDBbiRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiRefresh.ItemClick
        Me.CleanControls()
        Me.BeginReloadDatasource(INDTcgElectronicDocument.SelectedTabPageName)
    End Sub

    Private Sub INDBbiPrint_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiPrint.ItemClick
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim selector = GetSelector(False)
        Select Case INDTcgElectronicDocument.SelectedTabPageName
            Case INDLcgDocumentSupport.Name 'Documento soporte

                Dim key = selector.GetKeysToArray().FirstOrDefault
                Dim DocumentOrigin = selector.GetValueByKey(key, "EntityName")

                If DocumentOrigin IsNot Nothing Then

                    Dim listEntityIds As Object()
                    Dim EntityId As New List(Of String)
                    Dim EntityName As New List(Of String)
                    For Each Item In selector.GetKeysToArray()
                        EntityId.Add(selector.GetValueByKey(Item, "EntityId"))
                        EntityName.Add(selector.GetValueByKey(Item, "EntityName"))
                    Next
                    listEntityIds = {EntityId, EntityName}
                    Dim report As New Reporter.rptSubAccountPayableAndVoucherTransactionSupportDocument
                    AddHandler report.AfterPrint, Sub()
                                                      If waitForm.IsSplashFormVisible Then
                                                          waitForm.CloseWaitForm()
                                                      End If
                                                  End Sub
                    ReportHelper.ExecuteReport(report, Me, Me.BarraBotones.PermissionsForm, listEntityIds)

                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontro el documento soporte para imprimir"
                    Exit Sub
                End If
            Case INDLcgNoteAdjustment.Name 'Nota de ajuste
                Dim IdNote As Integer = selector.GetKeysToArray().FirstOrDefault
                If IdNote > 0 Then
                    Dim report As New Reporter.rptSupportDocumentAdjusmentNote
                    AddHandler report.AfterPrint, Sub()
                                                      If waitForm.IsSplashFormVisible Then
                                                          waitForm.CloseWaitForm()
                                                      End If
                                                  End Sub
                    ReportHelper.ExecuteReport(report, Me, Me.BarraBotones.PermissionsForm, IdNote)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontro la nota para imprimir"
                    Exit Sub
                End If
        End Select
    End Sub

    Private Sub INDBbiProcess_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcess.ItemClick
        ProcessDocument()
    End Sub


    Private Async Sub ProcessDocument()
        Dim selector = GetSelector(False)
        Dim listDocuments As New List(Of Integer)
        Select Case INDTcgElectronicDocument.SelectedTabPageName
            Case INDLcgDocumentSupport.Name
                For Each key In selector.GetKeysToArray
                    If selector.GetValueByKey(key, "StatusElectronic") <> eStatusElectronic.validated Then
                        listDocuments.Add(key)
                    End If
                Next
                If listDocuments.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione al menos un registro"
                    Exit Sub
                End If
                Using model As New MElectronicSupportDocument(Me.Tag)
                    Dim result = Await model.UpdateStateElectronicSupportDocumentsAsync(listDocuments)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using
            Case INDLcgNoteAdjustment.Name
                For Each key In selector.GetKeysToArray
                    If selector.GetValueByKey(key, "StatusElectronic") <> eStatusElectronic.validated Then
                        listDocuments.Add(key)
                    End If
                Next
                If listDocuments.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione al menos un registro"
                    Exit Sub
                End If
                Using model As New MElectronicSupportDocumentAdjustmentNote(Me.Tag)
                    Dim result = Await model.UpdateStateElectronicSupportDocumentsAdjusmentNoteAsync(listDocuments)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using
        End Select
    End Sub

    Private Sub INDBbiSendNotification_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiSendNotification.ItemClick
        Dim selector = GetSelector(False)
        Dim listElectronicDocuments As New List(Of Domain.Entities.ElectronicDocument)

        For Each key In selector.GetKeysToArray()
            If selector.GetValueByKey(key, "Status") = eStatusElectronic.validated Then
                listElectronicDocuments.Add(New Domain.Entities.ElectronicDocument With {.Id = key, .CustomerPartyId = selector.GetValueByKey(key, "ThirdPartyId")})
            End If
        Next

        If listElectronicDocuments.GroupBy(Function(ed) ed.CustomerPartyId).Count > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Los registros seleccionados no corresponden al mismo cliente"
            Exit Sub
        End If

        OpenFormSendElectronicDocumentNotification(listElectronicDocuments)
    End Sub

#End Region

#Region "ShowingEditor"


    Private Sub INDGvDocumentSupport_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvDocumentSupport.ShowingEditor
        INDGcElectronicDocumentDetails.DataSource = Nothing
        INDGvNoteAdjustment_PceDetails.PopupControl = Nothing

        INDGvSupportDocument_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentNotifications.DataSource = Nothing

        Dim electronicDocument = CType(INDGvDocumentSupport.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicDocumentDetails.DataSource = Me._presenter.GetDetails(electronicDocument.Id)
        INDGvSupportDocument_PceDetails.PopupControl = INDPccElectronicDocumentDetails
        INDGcElectronicDocumentNotifications.DataSource = Me._presenter.GetNotifications(electronicDocument.Id)
    End Sub

    Private Sub INDGvNoteAdjustment_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvNoteAdjustment.ShowingEditor
        INDGvNoteAdjustment_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentDetails.DataSource = Nothing

        INDGvSupportDocument_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentNotifications.DataSource = Nothing

        Dim NoteAdjustment = CType(INDGvNoteAdjustment.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicDocumentDetails.DataSource = Me._presenter.GetNoteDetails(NoteAdjustment.Id)
        INDGvNoteAdjustment_PceDetails.PopupControl = INDPccElectronicDocumentDetails
        INDGcElectronicDocumentNotifications.DataSource = Me._presenter.GetNotifications(NoteAdjustment.Id)
    End Sub

#End Region

#End Region

End Class