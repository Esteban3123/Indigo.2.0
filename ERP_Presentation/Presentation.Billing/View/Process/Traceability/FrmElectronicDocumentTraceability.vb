#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraReports.Parameters
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

Public Class FrmElectronicDocumentTraceability
    Implements IElectronicDocumentTraceability

#Region "Variables"

    ''' <summary>
    ''' presenter del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PElectronicDocumentTraceability

    Private _isPopupMenuShowing As Boolean

    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, DevExpress.XtraSplashScreen.ParentType.UserControl)

    ''' <summary>
    ''' Parrametros de Facturacion
    ''' </summary>
    Private SettingBilling As SettingsBilling

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object Implements IElectronicDocumentTraceability.MyTag
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

    Public ReadOnly Property OperatingUnitId As Integer Implements IElectronicDocumentTraceability.OperatingUnitId
        Get
            Return INDSleOperatingUnit.EditValue
        End Get
    End Property

    Public ReadOnly Property Status As String Implements IElectronicDocumentTraceability.Status
        Get
            Return INDCcbeStatus.EditValue
        End Get
    End Property

#End Region

#Region "Datasource"

    Public Property OperatingUnitXpo As List(Of Domain.Entities.OperatingUnit) Implements IElectronicDocumentTraceability.OperatingUnitXpo
        Get
            Return CType(INDSleOperatingUnit.Properties.DataSource, List(Of Domain.Entities.OperatingUnit))
        End Get
        Set(value As List(Of Domain.Entities.OperatingUnit))
            INDSleOperatingUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property InvoiceXpo As XPInstantFeedbackSource Implements IElectronicDocumentTraceability.InvoiceXpo
        Get
            Return CType(INDGcInvoice.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcInvoice.DataSource = value
        End Set
    End Property

    Public Property DebitNoteXpo As XPInstantFeedbackSource Implements IElectronicDocumentTraceability.DebitNoteXpo
        Get
            Return CType(INDGcDebitNote.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcDebitNote.DataSource = value
        End Set
    End Property

    Public Property CreditNoteXpo As XPInstantFeedbackSource Implements IElectronicDocumentTraceability.CreditNoteXpo
        Get
            Return CType(INDGcCreditNote.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGcCreditNote.DataSource = value
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
        InvoiceXpo = Nothing
        DebitNoteXpo = Nothing
        CreditNoteXpo = Nothing

        _selectorInvoice.Clear()
        _selectorDebitNote.Clear()
        _selectorCreditNote.Clear()
    End Sub

    Private Sub BeginReloadDatasource(selectedPage As String)
        If OperatingUnitId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione una Unidad Operativa"
            Exit Sub
        End If

        Select Case selectedPage
            Case INDLcgInvoice.Name 'Facturas
                If Me.InvoiceXpo Is Nothing Then
                    Me._presenter.GetInvoices(Me.OperatingUnitId, Me.Status)
                End If
            Case INDLcgDebitNote.Name 'Notas Debito
                If Me.DebitNoteXpo Is Nothing Then
                    Me._presenter.GetDebitNotes(Me.OperatingUnitId, Me.Status)
                End If
            Case INDLcgCreditNote.Name 'Notas Credito
                If Me.CreditNoteXpo Is Nothing Then
                    Me._presenter.GetCreditNotes(Me.OperatingUnitId, Me.Status)
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
            Case INDLcgInvoice.Name 'Facturas
                INDGvInvoice.RefreshData()
            Case INDLcgDebitNote.Name 'Notas Debito
                INDGvDebitNote.RefreshData()
            Case INDLcgCreditNote.Name 'Notas Credito
                INDGvCreditNote.RefreshData()
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

    Private _selectorInvoice As SelectorCache = New SelectorCache("Id", "Status", "EntityId", "ThirdPartyId", "CurrencyAbbreviation")
    Private _selectorDebitNote As SelectorCache = New SelectorCache("Id", "Status", "EntityId", "ThirdPartyId", "CurrencyAbbreviation", "NoteTypeDetail")
    Private _selectorCreditNote As SelectorCache = New SelectorCache("Id", "Status", "EntityId", "ThirdPartyId", "CurrencyAbbreviation", "NoteTypeDetail")

    Private Sub INDGv_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvInvoice.CustomUnboundColumnData, INDGvDebitNote.CustomUnboundColumnData, INDGvCreditNote.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvInvoice" Then
                e.Value = _selectorInvoice.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvDebitNote" Then
                e.Value = _selectorDebitNote.ValidateExistsRow(e.Row)
            ElseIf view.Name = "INDGvCreditNote" Then
                e.Value = _selectorCreditNote.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvInvoice.RowCellClick, INDGvDebitNote.RowCellClick, INDGvCreditNote.RowCellClick
        If Not Me._isPopupMenuShowing Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)

            If view.Name = "INDGvInvoice" Then
                selector = _selectorInvoice
            ElseIf view.Name = "INDGvDebitNote" Then
                selector = _selectorDebitNote
            ElseIf view.Name = "INDGvCreditNote" Then
                selector = _selectorCreditNote
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

    Private Sub FrmElectronicDocumentTraceability_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        Me._presenter = New PElectronicDocumentTraceability(Me)
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

    Private Async Sub INDSleOperatingUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleOperatingUnit.EditValueChanged
        Me.CleanControls()
        Me.BeginReloadDatasource(INDTcgElectronicDocument.SelectedTabPageName)
        Using model As New MBillingSetting(Me.Tag)
            SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(OperatingUnitId, False)
        End Using
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
    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvInvoice.PopupMenuShowing, INDGvDebitNote.PopupMenuShowing, INDGvCreditNote.PopupMenuShowing
        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        End If

        INDBbiPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        INDBbiSendNotification.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        Dim selector = GetSelector(True)

        If selector.Count > 0 Then
            INDBbiPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            'Ocultar el botón procesar para Costa Rica en el tab de Facturas
            Dim isCostaRica = SessionValues.Instance.Culture.Name = "es-CR"

            'Solo mostrar INDBbiProcess si NO es Costa Rica
            If BarraBotones.PermissionsForm.ContainsKey(81) AndAlso Not isCostaRica Then 'si tiene permiso de procesar
                For Each key In selector.GetKeysToArray()
                    If selector.GetValueByKey(key, "Status") <> "3" Then
                        INDBbiProcess.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                        Exit For
                    End If
                Next
            End If

            For Each key In selector.GetKeysToArray()
                If selector.GetValueByKey(key, "Status") = "3" Then
                    INDBbiSendNotification.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    Exit For
                End If
            Next
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
            Case INDLcgInvoice.Name 'Facturas
                If selection Then
                    _selectorInvoice.SetValue(INDGvInvoice.GetFocusedRow, True)
                End If
                selector = _selectorInvoice
            Case INDLcgDebitNote.Name 'Notas Debito
                If selection Then
                    _selectorDebitNote.SetValue(INDGvDebitNote.GetFocusedRow, True)
                End If
                selector = _selectorDebitNote
            Case INDLcgCreditNote.Name 'Notas Credito
                If selection Then
                    _selectorCreditNote.SetValue(INDGvCreditNote.GetFocusedRow, True)
                End If
                selector = _selectorCreditNote
        End Select
        Return selector
    End Function

#End Region

#Region "ItemClick"

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
            Case INDLcgInvoice.Name 'Facturas

                Dim CurrencyAbbreviation = selector.GetValueByKey(selector.GetKeysToArray().FirstOrDefault, "CurrencyAbbreviation")
                If selector.GetKeysToArray().Any(Function(key) CurrencyAbbreviation <> selector.GetValueByKey(key, "CurrencyAbbreviation")) Then
                    Mensaje(EeventViewerImages.Advertencia) = "Puede acceder a las facturas correspondientes seleccionando aquellas que compartan la misma denominación de moneda"
                    waitForm.CloseWaitForm()
                    Exit Sub
                End If

                Dim listInvoices As New List(Of Domain.Entities.Invoice)
                For Each key In selector.GetKeysToArray()
                    listInvoices.Add(New Domain.Entities.Invoice With {.Id = selector.GetValueByKey(key, "EntityId")})
                Next

                Dim reportDef As New Reporter.rptSubSaleInvoiceAll
                reportDef.Currency = New Currency With {.Abbreviation = CurrencyAbbreviation}
                AddHandler reportDef.AfterPrint, Sub()
                                                     If waitForm.IsSplashFormVisible Then
                                                         waitForm.CloseWaitForm()
                                                     End If
                                                 End Sub

                reportDef.Parameters("LiquidateMasterAccount").Value = SettingBilling?.LiquidateMasterAccount
                reportDef.Parameters("requiresConditionsSale").Value = SettingBilling?.requiresConditionsSale

                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listInvoices)

            Case INDLcgDebitNote.Name, INDLcgCreditNote.Name 'Notas

                Dim CurrencyAbbreviation = selector.GetValueByKey(selector.GetKeysToArray().FirstOrDefault, "CurrencyAbbreviation")
                If selector.GetKeysToArray().Any(Function(key) CurrencyAbbreviation <> selector.GetValueByKey(key, "CurrencyAbbreviation")) Then
                    Mensaje(EeventViewerImages.Advertencia) = "Puede acceder a las facturas correspondientes seleccionando aquellas que compartan la misma denominación de moneda"
                    waitForm.CloseWaitForm()
                    Exit Sub
                End If

                Dim listBillingNotes As New List(Of Domain.Entities.BillingNote)
                For Each key In selector.GetKeysToArray()
                    listBillingNotes.Add(New Domain.Entities.BillingNote With {.Id = selector.GetValueByKey(key, "EntityId"), .NoteTypeDetail = selector.GetValueByKey(key, "NoteTypeDetail")})
                Next

                Dim reportDef As New Reporter.rptSubBillingNoteAll
                reportDef.Currency = New Currency With {.Abbreviation = CurrencyAbbreviation}
                AddHandler reportDef.AfterPrint, Sub()
                                                     If waitForm.IsSplashFormVisible Then
                                                         waitForm.CloseWaitForm()
                                                     End If
                                                 End Sub
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, listBillingNotes)
        End Select
    End Sub

    Private Async Sub INDBbiProcess_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcess.ItemClick
        Dim selector = GetSelector(False)
        Dim listElectronicDocuments As New List(Of Domain.Entities.ElectronicDocument)

        For Each key In selector.GetKeysToArray()
            If selector.GetValueByKey(key, "Status") <> "3" Then
                listElectronicDocuments.Add(New Domain.Entities.ElectronicDocument With {.Id = key, .Status = 66})
            End If
        Next

        Using Model As New MElectronicDocumentTraceability(Me.Tag)
            Dim result = Await Model.UpdateStateElectronicDocuments(listElectronicDocuments)

            If Not String.IsNullOrEmpty(result.Message) Then
                Mensaje(EeventViewerImages.Informacion) = result.Message
            End If

            If Not String.IsNullOrEmpty(result.MessageAux) Then
                Mensaje(EeventViewerImages.Advertencia) = result.MessageAux
            End If
        End Using
    End Sub

    Private Sub INDBbiSendNotification_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiSendNotification.ItemClick
        Dim selector = GetSelector(False)
        Dim listElectronicDocuments As New List(Of Domain.Entities.ElectronicDocument)

        For Each key In selector.GetKeysToArray()
            If selector.GetValueByKey(key, "Status") = "3" Then
                listElectronicDocuments.Add(New Domain.Entities.ElectronicDocument With {.Id = key, .CustomerPartyId = selector.GetValueByKey(key, "ThirdPartyId")})
            End If
        Next

        If listElectronicDocuments.GroupBy(Function(ed) ed.CustomerPartyId).Count > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Los registros seleccionados no corresponden al mismo cliente"
            Exit Sub
        End If

        OpenFormSendElectronicDocumentNotification(listElectronicDocuments)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al reenviar la factura, en el proceso de facturacion electronica Costa Rica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBbiReSend_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBbiReSend.ItemClick
        Dim row As Object
        Dim selectedTabPageName As String = INDTcgElectronicDocument.SelectedTabPageName

        Select Case selectedTabPageName
            Case "INDLcgInvoice"
                row = CType(INDGvInvoice.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow
            Case "INDLcgDebitNote"
                row = CType(INDGvDebitNote.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow
            Case "INDLcgCreditNote"
                row = CType(INDGvCreditNote.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow
        End Select

        Dim electronicDocumentXPO = DirectCast(row, Infrastructure.Data.Xpo.BillingRepository.ElectronicDocumentXpo)
        If electronicDocumentXPO Is Nothing Then
            Exit Sub
        End If

        resendElectronicDocuments(New List(Of ElectronicDocument) From {
                    New ElectronicDocument With {
                        .Id = electronicDocumentXPO.Id,
                        .Status = electronicDocumentXPO.Status,
                        .DocumentType = electronicDocumentXPO.DocumentType,
                        .Prefix = electronicDocumentXPO.Prefix,
                        .DocumentNumber = electronicDocumentXPO.DocumentNumber,
                        .EntityId = electronicDocumentXPO.EntityId,
                        .EntityName = electronicDocumentXPO.EntityName
                    }
                })
    End Sub

    Async Sub resendElectronicDocuments(listElectronicDocuments As List(Of ElectronicDocument))
        If Not listElectronicDocuments.Any() Then
            Exit Sub
        End If

        If MessageIndigo.Show("¿Desea reenviar el documento electrónico?", MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Using Model As New MElectronicDocumentTraceability(Me.Tag)
            Dim result = Await Model.ReSendElectronicDocuments(listElectronicDocuments)
            If result?.StateResult Then
                MessageIndigo.Show(result.Message, MessageType.Information, Me.Text)
                Me.CleanControls()
                Me.BeginReloadDatasource(INDTcgElectronicDocument.SelectedTabPageName)
            Else
                Mensaje(EeventViewerImages.Advertencia) = result?.Message
            End If
        End Using
    End Sub

#End Region

#Region "ShowingEditor"

    Private Sub INDGvInvoice_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvInvoice.ShowingEditor
        INDGvInvoice_PceDetails.PopupControl = Nothing
        INDGvDebitNote_PceDetails.PopupControl = Nothing
        INDGvCreditNote_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentDetails.DataSource = Nothing

        INDGvInvoice_PceNotifications.PopupControl = Nothing
        INDGvDebitNote_PceNotifications.PopupControl = Nothing
        INDGvCreditNote_PceNotifications.PopupControl = Nothing
        INDGvSupportDocument_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentNotifications.DataSource = Nothing

        Dim electronicDocument = CType(INDGvInvoice.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicDocumentDetails.DataSource = Me._presenter.GetDetails(electronicDocument.Id)
        INDGvInvoice_PceDetails.PopupControl = INDPccElectronicDocumentDetails
        INDGcElectronicDocumentNotifications.DataSource = Me._presenter.GetNotifications(electronicDocument.Id)
        INDGvInvoice_PceNotifications.PopupControl = INDPccElectronicDocumentNotifications
    End Sub

    Private Sub INDGvDebitNote_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvDebitNote.ShowingEditor
        INDGvInvoice_PceDetails.PopupControl = Nothing
        INDGvDebitNote_PceDetails.PopupControl = Nothing
        INDGvCreditNote_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentDetails.DataSource = Nothing

        INDGvInvoice_PceNotifications.PopupControl = Nothing
        INDGvDebitNote_PceNotifications.PopupControl = Nothing
        INDGvCreditNote_PceNotifications.PopupControl = Nothing
        INDGvSupportDocument_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentNotifications.DataSource = Nothing

        Dim electronicDocument = CType(INDGvDebitNote.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicDocumentDetails.DataSource = Me._presenter.GetDetails(electronicDocument.Id)
        INDGvDebitNote_PceDetails.PopupControl = INDPccElectronicDocumentDetails
        INDGcElectronicDocumentNotifications.DataSource = Me._presenter.GetNotifications(electronicDocument.Id)
        INDGvDebitNote_PceNotifications.PopupControl = INDPccElectronicDocumentNotifications
    End Sub

    Private Sub INDGvCreditNote_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvCreditNote.ShowingEditor
        INDGvInvoice_PceDetails.PopupControl = Nothing
        INDGvDebitNote_PceDetails.PopupControl = Nothing
        INDGvCreditNote_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentDetails.DataSource = Nothing

        INDGvInvoice_PceNotifications.PopupControl = Nothing
        INDGvDebitNote_PceNotifications.PopupControl = Nothing
        INDGvCreditNote_PceNotifications.PopupControl = Nothing
        INDGvSupportDocument_PceDetails.PopupControl = Nothing
        INDGcElectronicDocumentNotifications.DataSource = Nothing

        Dim electronicDocument = CType(INDGvCreditNote.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcElectronicDocumentDetails.DataSource = Me._presenter.GetDetails(electronicDocument.Id)
        INDGvCreditNote_PceDetails.PopupControl = INDPccElectronicDocumentDetails
        INDGcElectronicDocumentNotifications.DataSource = Me._presenter.GetNotifications(electronicDocument.Id)
        INDGvCreditNote_PceNotifications.PopupControl = INDPccElectronicDocumentNotifications
    End Sub

#End Region

#End Region

End Class