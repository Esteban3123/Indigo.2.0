'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/03/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Drawing
Imports System.Dynamic
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraSplashScreen
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP
Imports Presentation.Reporter

#End Region

Public Class FrmDashboardProductionSchedule

#Region "Variables"

    Private waitForm As New SplashScreenManager(Me, GetType(wfMain), False, True, ParentType.UserControl)
    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardProductionSchedule

    ''' <summary>
    ''' Id de la central de mezcla
    ''' </summary>
    Private CMConfigurationId As Integer

    ''' <summary>
    ''' Id de la linea de producción
    ''' </summary>
    Private ProductionLineId As Integer

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProductsCampaignDetailArgs(sender As Boolean, e As AddProductsCampaignDetail)

    ''' <summary>
    ''' Objeto de campaña detalle
    ''' </summary>
    Private _campaignDetail As New CampaignDetail()
#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Vuelve a cargar el datasource de la rejilla activa
    ''' </summary>
    Private Sub BeginReloadDatasource()
        Select Case INDtcgInformation.SelectedTabPageName
            Case INDlcgSchedule.Name 'Cronograma
                INDgcSchedule.DataSource = Nothing
                LoadSchedule()
            Case INDlcgProductionOrder.Name 'Orden de producción
                INDgcProductionOrder.DataSource = Nothing
                LoadProductionOrder()
            Case INDlcgHistoricCampaing.Name 'Historico Campañas
                INDgcHistoricCampaing.DataSource = Nothing
                LoadHistoricCampaign()
        End Select
    End Sub

    ''' <summary>
    ''' Carga el cronograma
    ''' </summary>
    Private Sub LoadSchedule()
        If INDgcSchedule.DataSource IsNot Nothing Then
            Exit Sub
        End If

        INDgcSchedule.DataSource = Nothing
        INDgcSchedule.DataSource = Presenter.ListViewListDashboardProductionScheduleXPInstantFeedbackSource(CMConfigurationId, ProductionLineId)
    End Sub

    ''' <summary>
    ''' Carga las ordenes de producción
    ''' </summary>
    Private Sub LoadProductionOrder()
        If INDgcProductionOrder.DataSource IsNot Nothing Then
            Exit Sub
        End If

        INDgcProductionOrder.DataSource = Nothing
        INDgcProductionOrder.DataSource = Presenter.ListViewProductionOrder(CMConfigurationId, ProductionLineId)
    End Sub

    ''' <summary>
    ''' Carga las ordenes de Historico 
    ''' </summary>
    Private Sub LoadHistoricCampaign()
        If INDgcHistoricCampaing.DataSource IsNot Nothing Then
            Exit Sub
        End If

        INDgcHistoricCampaing.DataSource = Nothing
        INDgcHistoricCampaing.DataSource = Presenter.ListViewHistoricCampaign(CMConfigurationId, ProductionLineId)
    End Sub

    ''' <summary>
    ''' Abre el formulario para ver los detalles de la campaña
    ''' </summary>
    Private Sub OpenFrmViewDetails()
        Dim focusedRowHandled = INDviewProductionOrder.FocusedRowHandle
        Dim item = DirectCast(DirectCast(INDviewProductionOrder.GetRow(focusedRowHandled), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)

        Using formulario As New FrmViewDetails()
            formulario.SetTitleWindow = item.CampaignDescription
            formulario.campaignDetailId = item.CampaignDetailId
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Abre el formulario para ver los detalles de la campaña
    ''' </summary>
    Private Sub OpenFrmViewDetailsHistoric()
        Dim item = INDviewHistoricCampaign.GetFocusedObject(Of ViewHistoricCampaingXpo)()

        Using formulario As New FrmViewDetails()
            formulario.SetTitleWindow = item.CampaignDescription
            formulario.campaignDetailId = item.CampaignDetailId
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Abre el formulario para agregar usuarios autorizados
    ''' </summary>
    Private Sub OpenFrmAuthorizeUsers()
        Dim item = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)

        Using formulario As New FrmAuthorizeUsers()
            formulario.campaignDetailId = item.CampaignDetailId
            formulario.CampaignCreationDate = item.CampaignCreationDate
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.CampaignStatus = item.CampaignStatus
            formulario.Height = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.8
            formulario._MSclass = item.UnitDoseTypeClass
            AddHandler formulario.OnSaveAuthorizeUsers, AddressOf AuthorizeUserSave
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Async Sub AuthorizeUserSave(sender As Object, e As AuthorizedUserEventArgs)
        Try
            Using model As New MDashboardProductionSchedule(Me.Tag.ToString())
                CType(sender, FrmAuthorizeUsers).AsyncLoader(True)
                AsyncLoader(True)
                If e.CampaignStatus = 2 Then
                    Dim resultValidation = Await model.ValidateProductionOrder({e.CampaignDetailId}.ToList())

                    If Not resultValidation.StateResult Then
                        If MessageIndigo.Show($"{resultValidation.Message}{vbCrLf}¿Desea continuar con el proceso?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                            AsyncLoader(False)
                            CType(sender, FrmAuthorizeUsers).AsyncLoader(False)
                            Exit Sub
                        End If
                    End If
                End If

                Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
                Dim args = AssigningValues(listItems)

                With _campaignDetail
                    .Id = e.CampaignDetailId
                    .ProcessingDate = e.ProcessDate
                    .PreparationTime = e.PreparationTime
                    .CampaignStatus = e.CampaignStatus
                    .MSClass = listItems(0).UnitDoseTypeClass
                End With

                Dim result = Await model.SaveProductionSchedule(args, e.AuthorizeUsers, _campaignDetail)
                AsyncLoader(False)
                CType(sender, FrmAuthorizeUsers).AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    If Not String.IsNullOrWhiteSpace(result.MessageAux) Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageAux
                    End If

                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using

            CType(sender, FrmAuthorizeUsers).Close()
        Catch ex As Exception
            AsyncLoader(False)
            CType(sender, FrmAuthorizeUsers).AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Procesa los registros para generar la orden de producción
    ''' </summary>
    Private Sub Process()
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
        Dim objCampaignDetail = listItems(0)
        If objCampaignDetail.CampaignStatus = 2 Then
            If MessageIndigo.Show("Desea generar la orden de producción?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            If (From x In listItems Where Not String.IsNullOrEmpty(x.ProductionScheduleCode) Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Hay items seleccionados que ya tienen una orden de producción"
                Exit Sub
            End If
        End If
        OpenFrmAuthorizeUsers()
    End Sub

    Private Sub ManageLabel()
        Dim obj = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)
        Using frm As New FrmPopUpManageLabel(obj.CampaignDetailId, obj.UnitDoseTypeClass)
            frm.StartPosition = Windows.Forms.FormStartPosition.CenterParent
            AddHandler frm.ConfirmLabel, AddressOf ConfirmLabel
            Dim trns As New FrmTransparent(frm, False)
            trns.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Imprime la ficha técnica de las nutriciones parenterales
    ''' </summary>
    Private Sub TechnicalDataSheet()
        Dim item = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)
        Dim itemCampaignDetailId = item.CampaignDetailId

        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim rpt As New rptNptLabelSub()
        AddHandler rpt.AfterPrint, Sub()
                                       If waitForm.IsSplashFormVisible Then
                                           waitForm.CloseWaitForm()
                                       End If
                                   End Sub
        ReportHelper.ExecuteReport(rpt, Me, Nothing, {itemCampaignDetailId})
    End Sub

    Private Sub CancelCampaign()
        If MessageIndigo.Show("¿Está seguro que desea anular las campañas seleccionadas?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
        Dim CampaingDetailIds = (From x In listItems Select x.CampaignDetailId).ToList()
        AcctionsMenuCampaingStatus(CampaingDetailIds, 4)
    End Sub

    Private Sub BlockCampaign()
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
        Dim CampaingDetailIds = (From x In listItems Select x.CampaignDetailId).ToList()
        AcctionsMenuCampaingStatus(CampaingDetailIds, 3)
    End Sub

    Private Sub OpenCampaign()
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
        Dim CampaingDetailIds = (From x In listItems Select x.CampaignDetailId).ToList()
        AcctionsMenuCampaingStatus(CampaingDetailIds, 1)
    End Sub

    Private Sub CloseCampaign()
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
        Dim CampaingDetailIds = (From x In listItems Select x.CampaignDetailId).ToList()
        AcctionsMenuCampaingStatus(CampaingDetailIds, 2)
    End Sub

    Private Sub PrintProductionOrder()
        Try
            Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
            Dim objCampaignDetail = listItems(0)
            If objCampaignDetail.Status < 3 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("La campaña No. {0} no ha superado la validación de lotes.", objCampaignDetail.CampaignNumber)
                Exit Sub
            End If
            If Not waitForm.IsSplashFormVisible Then waitForm.ShowWaitForm()

            Dim reportDef As New rptProductionOrder
            AddHandler reportDef.AfterPrint, Sub()
                                                 If waitForm.IsSplashFormVisible Then waitForm.CloseWaitForm()
                                             End Sub
            ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, objCampaignDetail.CampaignDetailId)
        Catch ex As Exception
            MessageIndigo.Show(IndigoManagementExceptions.GetExceptionDetails(ex), MessageType.Errores, Me.Text, Botones.Aceptar, "")
            If waitForm.IsSplashFormVisible Then waitForm.CloseWaitForm()
        End Try
    End Sub

    Private Sub ViewDetailCampaing()
        Dim item = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)

        Using formulario As New FrmViewDetailCampaing()
            formulario.campaignDetailId = item.CampaignDetailId
            formulario.CampaingNumber = item.CampaignNumber
            formulario.HourProcessed = item.ProcessingDate
            formulario.State = item.CampaignStatusName
            formulario.QuantityAd = item.Quantity
            formulario.WorkingArea = item.WorkingAreaName
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ViewDetailCampaingHistoric()
        Dim item = INDviewHistoricCampaign.GetFocusedObject(Of ViewHistoricCampaingXpo)

        Using formulario As New FrmViewDetailCampaing()
            formulario.campaignDetailId = item.CampaignDetailId
            formulario.CampaingNumber = item.CampaignNumber
            formulario.HourProcessed = item.ProcessingDate
            formulario.State = item.CampaignStatusName
            formulario.QuantityAd = item.Quantity
            formulario.WorkingArea = item.WorkingAreaName
            formulario.WindowState = Windows.Forms.FormWindowState.Maximized
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.MinimizeBox = True
            formulario.MaximizeBox = True
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub CampaignBatchRecord()
        Dim item = INDviewHistoricCampaign.GetFocusedObject(Of ViewHistoricCampaingXpo)

        Using formulario As New PopUpCampaignBatchRecord()
            formulario.CampaignSelected = item
            formulario.Text = $"Batch Record Campaña No {item.CampaignNumber}"
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub AttachWitnesses()
        Dim item = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)

        Using frm As New FrmAttachWitnesses()
            frm.CampaignDetailId = item.CampaignDetailId
            Dim transparent = New FrmTransparent(frm, False)
            Me.Cursor = Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Imprime las etiquetas con orden de producción
    ''' </summary>
    Private Sub PrintLabels()
        Dim item = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)

        AsyncLoader(True)
        Dim reporte As New rptReportDoseAdjustmentLabel
        ReportHelper.ExecuteReport(reporte, Me, Nothing, item.CampaignDetailId)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Function AssigningValues(listItems As List(Of ViewProductionOrderXpo)) As Object
        Dim args As Object = New ExpandoObject()
        Dim myListDetail As New ConcurrentBag(Of Object)()

        If listItems IsNot Nothing AndAlso listItems.Count > 0 Then
            For Each item In listItems
                Dim detail As Object = New ExpandoObject()
                detail.CampaignDetailId = item.CampaignDetailId
                myListDetail.Add(detail)
            Next
        End If

        args.CMConfigurationId = CMConfigurationId
        args.ProductionLineId = ProductionLineId
        args.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
        args.Details = myListDetail
        Return args
    End Function

    Public Overrides Sub AsyncLoader(State As Boolean)
        MyBase.AsyncLoader(State)
        If State Then
            INDviewProductionOrder.ShowLoadingPanel()
            INDviewSchedule.ShowLoadingPanel()
        Else
            INDviewProductionOrder.HideLoadingPanel()
            INDviewSchedule.HideLoadingPanel()
        End If
    End Sub

    Private Async Sub AcctionsMenuCampaingStatus(CampaingDetailId As List(Of Integer), Action As Byte)
        Try
            AsyncLoader(True)
            Using model As New MDashboardProductionSchedule(Me.Tag.ToString())
                Dim result = Await model.CampaingDetailStatusChange(CampaingDetailId, Action)

                If Not result.StateResult Then
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count = 1 Then
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageResult(0)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("No se pudo cambiar el estado de ninguna campaña. info #:{0}", result.Message)
                    End If
                    AsyncLoader(False)
                ElseIf result.Message IsNot String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El proceso se completo parcialmente, el estado de las campañas número {0} no fue cambiado", result.Message)
                Else
                    Mensaje(EeventViewerImages.Informacion) = "El Proceso se completo correctamente"
                End If

                BeginReloadDatasource()
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            AsyncLoader(False)
        End Try
    End Sub

    Private Async Sub AdecuationLabels()
        Try
            ' Consultamos los labelTypes
            INDviewProductionOrder.ShowLoadingPanel()

            Dim row = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)()
            Dim labelTypes = Await Task.Factory.StartNew(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService _
                .GetCollection(Of RequestMixingStationDetailXpo)(Function(m) m.CampaignDetailId = row.CampaignDetailId)? _
                .Where(Function(m) m.LabelType.HasValue) _
                .Select(Function(m) m.LabelType.Value).Distinct().ToList())

            Using frm As New FrmAdecuationLabelDialog()
                frm.LabelTypes = labelTypes
                AddHandler frm.Shown, Sub() INDviewProductionOrder.HideLoadingPanel()
                AddHandler frm.ClickJeringa, Sub()
                                                 ShowJeringaReport(row.CampaignDetailId)
                                             End Sub
                AddHandler frm.ClickTablet, Sub()
                                                ShowTabletReport(row.CampaignDetailId)
                                            End Sub
                AddHandler frm.ClickNptLabel, Sub()
                                                  ShowNptLabelReport(row.CampaignDetailId)
                                              End Sub
                AddHandler frm.ClickBolsa, Sub()
                                               ShowBolsaReport(row.CampaignDetailId)
                                           End Sub
                AddHandler frm.ClickMagistral, Sub()
                                                   ShowMagistralReport(row.CampaignDetailId)
                                               End Sub
                Dim trn As New FrmTransparent(frm, False)
                trn.ShowDialog(Me)
            End Using
        Catch ex As Exception
            INDviewProductionOrder.HideLoadingPanel()
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta el reporte indicado desde el boton 'Jeringa'
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    Private Sub ShowJeringaReport(campaignDetailId As Integer)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Dim rpt As New rptStickerLabel()
        AddHandler rpt.AfterPrint, Sub()
                                       If waitForm.IsSplashFormVisible Then
                                           waitForm.CloseWaitForm()
                                       End If
                                   End Sub
        ReportHelper.ExecuteReport(rpt, Me, Nothing, {campaignDetailId})
    End Sub

    ''' <summary>
    ''' Ejecuta el reporte indicado desde el boton 'Tableta 4 x 4'
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    Private Sub ShowTabletReport(campaignDetailId As Integer)
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()
        Dim objCampaignDetail = listItems(0)
        If objCampaignDetail.Status > 3 Then

            If Not waitForm.IsSplashFormVisible Then
                waitForm.ShowWaitForm()
            End If

            Dim rpt As New rptTabletSticker()
            AddHandler rpt.AfterPrint, Sub()
                                           If waitForm.IsSplashFormVisible Then
                                               waitForm.CloseWaitForm()
                                           End If
                                       End Sub
            ReportHelper.ExecuteReport(rpt, Me, Nothing, {campaignDetailId})
        Else
            Mensaje(EeventViewerImages.Informacion) = String.Format("La campaña No. {0} no ha superado la validación de lotes.", objCampaignDetail.CampaignNumber)
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el reporte indicado desde el boton 'Nutricion Parenteral'
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    Private Sub ShowNptLabelReport(campaignDetailId As Integer)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Dim rpt As New rptParenteralNutritionLabel()
        AddHandler rpt.AfterPrint, Sub()
                                       If waitForm.IsSplashFormVisible Then
                                           waitForm.CloseWaitForm()
                                       End If
                                   End Sub
        ReportHelper.ExecuteReport(rpt, Me, Nothing, {campaignDetailId})
    End Sub


    ''' <summary>
    ''' Ejecuta el reporte indicado desde el boton "Magistral"
    ''' </summary>
    Private Sub ShowMagistralReport(campaignDetailId As Integer)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim rpt As New rptMagistralLabel()
        AddHandler rpt.AfterPrint, Sub()
                                       If waitForm.IsSplashFormVisible Then
                                           waitForm.CloseWaitForm()
                                       End If
                                   End Sub

        ReportHelper.ExecuteReport(rpt, Me, Nothing, {campaignDetailId})
    End Sub

    ''' <summary>
    ''' Ejecuta el reporte indicado desde el boton "Bolsa"
    ''' </summary>
    Private Sub ShowBolsaReport(campaignDetailId As Integer)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If

        Dim rpt As New rptReportDoseAdjustmentLabel()
        AddHandler rpt.AfterPrint, Sub()
                                       If waitForm.IsSplashFormVisible Then
                                           waitForm.CloseWaitForm()
                                       End If
                                   End Sub

        ReportHelper.ExecuteReport(rpt, Me, Nothing, {campaignDetailId})
    End Sub

    Private Async Sub PrintRemission()
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()

        AsyncLoader(True)
        Dim reporte As New rptReportRemissions

        AddHandler reporte.AfterPrint, Sub()
                                           If waitForm.IsSplashFormVisible Then
                                               waitForm.CloseWaitForm()
                                           End If
                                       End Sub
        ReportHelper.ExecuteReport(reporte, Me, Nothing, listItems.FirstOrDefault.CampaignDetailId, listItems.FirstOrDefault.UnitDoseTypeClass)
        AsyncLoader(False)
    End Sub
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardProductionSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        'Se valida si el usuario tiene permiso para mostrar los check del search de centro de atención
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        'Inicializamos la referencia
        Presenter = New PDashboardProductionSchedule()
        setActions()
        INDsleCMProductionLine.Font = New Font("Segoe UI", 28, FontStyle.Regular)
    End Sub

    Private Sub setActions()
        IndigoGridView1.SetListAcction(INDviewProductionOrder, {
            eAcciones.View,
            eAcciones.AdaptationsLabel,
            eAcciones.PrintLabels,
            eAcciones.PrintRemission,
            eAcciones.Process,
            eAcciones.OpenCampagin,
            eAcciones.CloseCampaign,
            eAcciones.BlockCampaign,
            eAcciones.AnnulateCampaign,
            eAcciones.ManageLabels,
            eAcciones.PrintProductionOrder,
            eAcciones.ViewDetailCampaing,
            eAcciones.AttachWitnesses,
            eAcciones.TechnicalDataSheet
        }.ToList())

        IndigoGridView1.RepositoryItemPopupContainerEdit.PopupControl.ResetAutoSizeMode()
        IndigoGridView2.SetListAcction(INDviewHistoricCampaign, {eAcciones.View, eAcciones.ViewDetailCampaing, eAcciones.CampaignBatchRecord}.ToList())

        Dim col = INDviewProductionOrder.Columns.FirstOrDefault(Function(m) m.Name.Equals("colActions"))
        If col IsNot Nothing Then col.Width = 90

        Dim colu = INDviewHistoricCampaign.Columns.FirstOrDefault(Function(m) m.Name.Equals("colActions"))
        If colu IsNot Nothing Then colu.Width = 90
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardProductionSchedule_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCMProductionLine.Properties.PopupFormMinSize = New System.Drawing.Size(INDsleCMProductionLine.Size.Width - 11, 0)
        INDsleCMProductionLine.Properties.PopupFormSize = New System.Drawing.Size(INDsleCMProductionLine.Size.Width - 11, 0)

        INDtcgInformation.SelectedTabPageIndex = 0
        INDsleCMProductionLine.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMProductionLine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCMProductionLine.QueryPopUp
        If INDsleCMProductionLine.Properties.DataSource Is Nothing Then
            INDsleCMProductionLine.Properties.DataSource = Presenter.InitializeCMProductionLine()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara la cambiar el valor del control de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMProductionLine_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCMProductionLine.EditValueChanged
        If INDsleCMProductionLine.EditValue IsNot Nothing Then
            Dim viewXpo = INDviewSearchCMProductionLine.GetFocusedObject(Of ViewListCMProductionLineXpo)
            CMConfigurationId = viewXpo.CMConfigurationId
            ProductionLineId = viewXpo.ProductionLineId
            BeginReloadDatasource()
        End If
    End Sub

#End Region

#Region "SelectPageChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la pagina de los tabs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtcgInformation_SelectedPageChanged(sender As Object, e As DevExpress.XtraLayout.LayoutTabPageChangedEventArgs) Handles INDtcgInformation.SelectedPageChanged
        If INDsleCMProductionLine.EditValue Is Nothing Then
            Exit Sub
        End If

        Select Case e.Page.Name
            Case INDlcgSchedule.Name 'Cronograma
                LoadSchedule()
            Case INDlcgProductionOrder.Name 'Orden de producción
                LoadProductionOrder()
            Case INDlcgHistoricCampaing.Name 'Historico campañas
                LoadHistoricCampaign()
        End Select
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual para la rejilla de solicitudes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDview_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDviewProductionOrder.PopupMenuShowing
        IndigoGridView1.BarManagerActions.Items.ToList().ForEach(Sub(i) i.Visibility = DevExpress.XtraBars.BarItemVisibility.Never)

        If e.HitInfo.RowHandle < 0 Then
            Exit Sub
        Else
            Select Case INDtcgInformation.SelectedTabPageName
                Case INDlcgProductionOrder.Name 'Orden de Produccion
                    PopupMenuProductionOrder()
                Case INDlcgHistoricCampaing.Name 'Historico Campaña
                    PopupMenuHistoricCampaing()
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Menu contextual para ordenes de produccion
    ''' </summary>
    Private Sub PopupMenuProductionOrder()
        Dim listItems = (From x In INDviewProductionOrder.GetSelectedRows() Select DirectCast(DirectCast(INDviewProductionOrder.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewProductionOrderXpo)).ToList()

        If (From i In listItems Select i.CampaignStatus).Distinct.Count > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccionó Campañas con estados diferentes, para ejercer una acción asegurece que todas tengan el mismo estado"
            Exit Sub
        End If

        Dim status = listItems.FirstOrDefault().CampaignStatus
        Dim UnitDoseTypeClass = listItems.FirstOrDefault().UnitDoseTypeClass
        'Si tiene permiso de ver paquetes
        If listItems.Count = 1 AndAlso Me.HasPermission(127) Then IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.View))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Select Case status
            Case 1
                'Si tiene permiso de procesar
                If Me.HasPermission(81) AndAlso listItems.FirstOrDefault.CampaignStatus = 2 Then _
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CloseCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.BlockCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Case 2
                'Si tiene permiso de procesar
                If Me.HasPermission(81) AndAlso listItems.FirstOrDefault.CampaignStatus = 2 Then _
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Caption = "Procesar"
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.OpenCampagin))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.BlockCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always


            Case 3
                'Si tiene permiso de procesar
                If Me.HasPermission(81) AndAlso listItems.FirstOrDefault.CampaignStatus = 2 Then _
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.OpenCampagin))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Case 5
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                If Me.HasPermission(PermissionsActionsForm.GestionarEtiqueta) AndAlso listItems.Count = 1 _
                    AndAlso listItems(0).LabelConfirmationDate Is Nothing Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ManageLabels))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If Me.HasPermission(81) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Caption = "Ajustar programación"
                End If

                If UnitDoseTypeClass <> EUnitDoseTypeClass.Repackaging AndAlso UnitDoseTypeClass <> EUnitDoseTypeClass.Refilling Then
                    'Si se selecciona al menos un item que tenga orden de produccion
                    If Me.HasPermission(PermissionsActionsForm.ImprimirEtiqueta) AndAlso listItems.Count = 1 _
                        AndAlso listItems(0).LabelConfirmationDate IsNot Nothing And UnitDoseTypeClass <> EUnitDoseTypeClass.Magistral Then
                        IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintLabels))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    End If

                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintRemission))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If UnitDoseTypeClass = EUnitDoseTypeClass.ParenteralNutrition AndAlso listItems(0).LabelConfirmationDate IsNot Nothing Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.TechnicalDataSheet))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If listItems.All(Function(m) m.LabelConfirmationDate.HasValue) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AdaptationsLabel))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ViewDetailCampaing))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                If listItems.Count = 1 AndAlso Not String.IsNullOrEmpty(listItems(0).ProductionScheduleCode) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintProductionOrder))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If listItems.Count = 1 AndAlso listItems(0).IsManagedLabel AndAlso listItems(0).Status >= 3 Then
                    'adjuntar testigos para todos los tipos de dosis unitaria 
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AttachWitnesses))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

            Case 6
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ViewDetailCampaing))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CampaignBatchRecord))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                If listItems.Count = 1 AndAlso Not String.IsNullOrEmpty(listItems(0).ProductionScheduleCode) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintProductionOrder))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                End If
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual para ordenes de produccion
    ''' </summary>
    Private Sub PopupMenuHistoricCampaing()
        Dim listItems = (From x In INDviewHistoricCampaign.GetSelectedRows() Select DirectCast(DirectCast(INDviewHistoricCampaign.GetRow(x), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ViewHistoricCampaingXpo)).ToList()

        If (From i In listItems Select i.CampaignStatus).Distinct.Count > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccionó Campañas con estados diferentes, para ejercer una acción asegurece que todas tengan el mismo estado"
            Exit Sub
        End If

        Dim status = listItems.FirstOrDefault().CampaignStatus
        'Si tiene permiso de ver paquetes
        If listItems.Count = 1 AndAlso Me.HasPermission(127) Then IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.View))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

        Select Case status
            Case 1
                'Si tiene permiso de procesar
                If Me.HasPermission(81) AndAlso listItems.FirstOrDefault.CampaignStatus = 2 Then _
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CloseCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.BlockCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Case 2
                'Si tiene permiso de procesar
                If Me.HasPermission(81) AndAlso listItems.FirstOrDefault.CampaignStatus = 2 Then _
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Caption = "Procesar"
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.OpenCampagin))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.BlockCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Case 3
                'Si tiene permiso de procesar
                If Me.HasPermission(81) AndAlso listItems.FirstOrDefault.CampaignStatus = 2 Then _
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.OpenCampagin))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CloseCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Case 5
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Never

                If Me.HasPermission(PermissionsActionsForm.GestionarEtiqueta) AndAlso listItems.Count = 1 _
                    AndAlso listItems(0).LabelConfirmationDate Is Nothing Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ManageLabels))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If Me.HasPermission(81) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Caption = "Ajustar programación"
                End If
                'Si se selecciona al menos un item que tenga orden de produccion
                If Me.HasPermission(PermissionsActionsForm.ImprimirEtiqueta) AndAlso listItems.Count = 1 _
                    AndAlso listItems(0).LabelConfirmationDate IsNot Nothing Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintLabels))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                If listItems.All(Function(m) m.LabelConfirmationDate.HasValue) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AdaptationsLabel))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintRemission))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always

                If listItems.Count = 1 AndAlso Not String.IsNullOrEmpty(listItems(0).ProductionScheduleCode) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintProductionOrder))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If

            Case 6
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ViewDetailCampaing))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CampaignBatchRecord))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                If listItems.Count = 1 AndAlso Not String.IsNullOrEmpty(listItems(0).ProductionScheduleCode) Then
                    IndigoGridView1.BarManagerActions.Items.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintProductionOrder))).Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
        End Select
    End Sub

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        Dim row = INDviewProductionOrder.GetFocusedObject(Of ViewProductionOrderXpo)()
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        'Si tiene permiso de ver paquetes
        Dim count As Integer = 0
        If Me.HasPermission(127) Then
            e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.View))).Visible = True
            count += 1
        End If

        Select Case row.CampaignStatus
            Case 1
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CloseCampaign))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.BlockCampaign))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visible = True

                count += 3
            Case 2
                'Si tiene permiso de procesar
                If Me.HasPermission(81) Then
                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visible = True
                    count += 1
                End If

                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.OpenCampagin))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.BlockCampaign))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Text = "Procesar"
                count += 3
            Case 3
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.OpenCampagin))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AnnulateCampaign))).Visible = True

                count += 2
            Case 5
                If Me.HasPermission(PermissionsActionsForm.GestionarEtiqueta) AndAlso row.LabelConfirmationDate Is Nothing Then
                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ManageLabels))).Visible = True
                    count += 1
                End If

                If row.UnitDoseTypeClass <> EUnitDoseTypeClass.Refilling AndAlso row.UnitDoseTypeClass <> EUnitDoseTypeClass.Repackaging Then
                    'Si se selecciona al menos un item que tenga orden de produccion
                    If Me.HasPermission(PermissionsActionsForm.ImprimirEtiqueta) AndAlso row.LabelConfirmationDate IsNot Nothing And row.UnitDoseTypeClass <> EUnitDoseTypeClass.Magistral Then
                        e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintLabels))).Visible = True
                        count += 1
                    End If

                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintRemission))).Visible = True
                    count += 1
                End If

                If row.UnitDoseTypeClass = EUnitDoseTypeClass.ParenteralNutrition AndAlso row.LabelConfirmationDate IsNot Nothing Then
                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.TechnicalDataSheet))).Visible = True
                    count += 1
                End If

                If Not String.IsNullOrEmpty(row.ProductionScheduleCode) Then
                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintProductionOrder))).Visible = True
                    count += 1
                End If

                If row.LabelConfirmationDate.HasValue Then
                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AdaptationsLabel))).Visible = True
                    count += 1
                End If

                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ViewDetailCampaing))).Visible = True
                count += 1

                If row.IsManagedLabel AndAlso row.Status >= 3 Then
                    'adjuntar testigos para todos los tipos de dosis unitaria 
                    e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.AttachWitnesses))).Visible = True
                    count += 1
                End If

                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.Process))).Text = "Ajustar programación"
                count += 1

            Case 6
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ViewDetailCampaing))).Visible = True
                e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CampaignBatchRecord))).Visible = True
                count += 1
                If Not String.IsNullOrEmpty(row.ProductionScheduleCode) Then
                    Dim btn = e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.PrintProductionOrder)))
                    btn.Visible = True
                    count += 2
                End If
        End Select

        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * count)
    End Sub

    Private Sub IndigoGridView2_QueryPopUpActionButtons(sender As Object, e As Controls.QueryPopUpActionButtonsEventArgs) Handles IndigoGridView2.QueryPopUpActionButtons
        Dim row = INDviewHistoricCampaign.GetFocusedObject(Of ViewHistoricCampaingXpo)()
        Dim popUp = CType(sender, RepositoryItemPopupContainerEdit)
        e.Buttons.ToList().ForEach(Sub(i) i.Visible = False)

        e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.View))).Visible = True
        e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.ViewDetailCampaing))).Visible = True
        e.Buttons.FirstOrDefault(Function(m) m.Tag.Equals(NameOf(eAcciones.CampaignBatchRecord))).Visible = True

        popUp.PopupControl.Size = New System.Drawing.Size(200, 36 * 3)
    End Sub


#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Evento que se dispara al presionar refrescar del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBarRefresh_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBarRefresh.ItemClick
        BeginReloadDatasource()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar ver del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiView_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiView.ItemClick
        OpenFrmViewDetails()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar ver detalle de la campaña del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBViewDetailCampaing_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBViewDetailCampaing.ItemClick
        ViewDetailCampaing()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar procesar del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiProcess_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiProcess.ItemClick
        Process()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar imprimir etiquetas del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiPrintLabels_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintLabels.ItemClick
        PrintLabels()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar Abrir Campaña en el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiOpenCampaing_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiOpenCampaing.ItemClick
        OpenCampaign()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar Cerrar Campaña en el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiCloseCampaing_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCloseCampaing.ItemClick
        CloseCampaign()
    End Sub

    ''' <summary>
    ''' Imprime la órden de producción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiPrintProductionOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiPrintProductionOrder.ItemClick
        PrintProductionOrder()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar Bloquear Campaña en el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiBlockCampaing_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiBlockCampaing.ItemClick
        BlockCampaign()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar Anular Campaña en el menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiCancelCampaing_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiCancelCampaing.ItemClick
        CancelCampaign()
    End Sub

    ''' <summary>
    ''' Abre el popup para gestionas las etiquetas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbiManageLabel_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBbiManageLabel.ItemClick
        ManageLabel()
    End Sub

    ''' <summary>
    ''' Abre el popup para gestionas las etiquetas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBTechnicalDataSheet_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDBTechnicalDataSheet.ItemClick
        TechnicalDataSheet()
    End Sub

    ''' <summary>
    ''' Confirma el etiquetado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub ConfirmLabel(sender As Object, e As FrmPopUpManageLabel.ConfirmLabelEventArgs)
        Dim popUp = CType(sender, FrmPopUpManageLabel)
        Try
            Using model As New MCampaign(Me.Tag)
                AsyncLoader(True)
                popUp.AsyncLoader(True)
                Dim result = Await model.ConfirmLabelItems(e.CampaignDetailId, e.LabelData)
                AsyncLoader(False)
                popUp.AsyncLoader(False)
                If result.StateResult Then
                    CType(sender, FrmPopUpManageLabel).Close()
                    Mensaje(EeventViewerImages.Informacion) = "Los datos han sido confirmados exitosamente"
                    BeginReloadDatasource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            popUp.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "ClickBack"

    ''' <summary>
    ''' se ejecuta al dar click en el control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        Me.INDlyRoot.Visible = True
        Me.INDPcDocumentViewer.Visible = False
    End Sub

    Private Sub INDSbRefresh_Click(sender As Object, e As EventArgs) Handles INDSbRefresh.Click
        BeginReloadDatasource()
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Select Case sender.Tag
            Case "View"
                OpenFrmViewDetailsHistoric()
            Case "ViewDetailCampaing"
                ViewDetailCampaingHistoric()
            Case "CampaignBatchRecord"
                CampaignBatchRecord()
        End Select
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag
            Case "View"
                OpenFrmViewDetails()
            Case "Process"
                Process()
            Case "OpenCampagin"
                OpenCampaign()
            Case "CloseCampaign"
                CloseCampaign()
            Case "BlockCampaign"
                BlockCampaign()
            Case "AnnulateCampaign"
                CancelCampaign()
            Case "ManageLabels"
                ManageLabel()
            Case "PrintLabels"
                PrintLabels()
            Case "PrintProductionOrder"
                PrintProductionOrder()
            Case "AdaptationsLabel"
                AdecuationLabels()
            Case "PrintRemission"
                PrintRemission()
            Case "ViewDetailCampaing"
                ViewDetailCampaing()
            Case "AttachWitnesses"
                AttachWitnesses()
            Case "TechnicalDataSheet"
                TechnicalDataSheet()
        End Select
    End Sub

    Private Sub INDTeBatchCodeFilter_EditValueChanged(sender As Object, e As EventArgs) Handles INDTeBatchCodeFilter.EditValueChanged
        FilterDataByBatchCode()
    End Sub

    Private _campaignDetailIds As List(Of Integer)

    Private Async Sub FilterDataByBatchCode()
        INDviewHistoricCampaign.ShowLoadingPanel()
        Using model As New MDashboardProductionSchedule(Me.Tag.ToString())
            If String.IsNullOrEmpty(INDTeBatchCodeFilter.EditValue) Then
                _campaignDetailIds = Nothing
                RefreshFilterGrid()
                INDviewHistoricCampaign.HideLoadingPanel()
                Return
            End If

            Dim campaignDetailIds = Await model.GetCampaignDetailIdsByBatchCode(CMConfigurationId, ProductionLineId, INDTeBatchCodeFilter.EditValue)

            If campaignDetailIds IsNot Nothing Then
                _campaignDetailIds = campaignDetailIds
            End If
            RefreshFilterGrid()
        End Using
        INDviewHistoricCampaign.HideLoadingPanel()
    End Sub

    Private Sub RefreshFilterGrid()
        If _campaignDetailIds IsNot Nothing Then
            INDviewHistoricCampaign.ActiveFilterString = $"[CampaignDetailId] In ({String.Join(",", _campaignDetailIds)})"
        Else
            INDviewHistoricCampaign.ActiveFilterString = Nothing
        End If
    End Sub
#End Region

#End Region

End Class