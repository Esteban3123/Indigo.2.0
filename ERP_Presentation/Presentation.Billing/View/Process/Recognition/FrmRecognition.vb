'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 2017-07-24
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Text
Imports DevExpress.Data.PLinq
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
#End Region

''' <summary>
''' 
''' </summary>
''' <seealso cref="Presentation.Controls.FormBase" />
''' <seealso cref="Presentation.Billing.MVP.IRecognition" />
Public Class FrmRecognition
    Implements IRecognition

#Region "Builder"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._ctrRangeDate = New CtrlSelectRangeDate()
        AdditionalControlPanel.Controls.Add(_ctrRangeDate)
        Me._ctrRangeDate.Dock = System.Windows.Forms.DockStyle.Fill
        AddHandler Me._ctrRangeDate.OnChangingDate, AddressOf OnChangeDate
        Me._presenter = New PRecognition(Me)
        IndigoGridControl1.SetHideNoRecords(INDGcRecognition, True)
        _IsProcesar = True
    End Sub

    Private Async Sub OnChangeDate(newDate As Date)

    End Sub
#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' Presentador de reconocimientos
    ''' </summary>
    Dim _presenter As PRecognition
    '''' <summary>
    '''' Control de seleccion de rango de fechas
    '''' </summary>
    Dim _ctrRangeDate As CtrlSelectRangeDate
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    Private _IsProcesar As Boolean

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As String Implements IRecognition.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property IdOperativeUnit As Integer Implements IRecognition.IdOperativeUnit
        Get
            Return _idOperativeUnit
        End Get
        Set(value As Integer)
            _idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the recognition datasource.
    ''' </summary>
    ''' <value>
    ''' The recognition datasource.
    ''' </value>
    Public Property RecognitionDatasource As PLinqServerModeSource Implements IRecognition.RecognitionDatasource
        Get
            Return CType(INDGcRecognition.DataSource, PLinqServerModeSource)
        End Get
        Set(value As PLinqServerModeSource)
            INDGcRecognition.SafeInvoke(Sub()
                                            INDGcRecognition.DataSource = value
                                            INDGcRecognition.RefreshDataSource()
                                            INDGcRecognition.Refresh()
                                            INDGvRecognition.HideLoadingPanel()
                                        End Sub)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the date initialize.
    ''' </summary>
    ''' <value>
    ''' The date initialize.
    ''' </value>
    Public Property DateInit As Date Implements IRecognition.DateInit
        Get
            Return Me._ctrRangeDate.InitialDate
        End Get
        Set(value As Date)
            Me._ctrRangeDate.InitialDate = value
        End Set
    End Property

    '''' <summary>
    '''' Gets or sets the date end.
    '''' </summary>
    '''' <value>
    '''' The date end.
    '''' </value>
    'Public Property DateEnd As Date Implements IRecognition.DateEnd
    '    Get
    '        Return Me._ctrRangeDate.EndDate
    '    End Get
    '    Set(value As Date)
    '        Me._ctrRangeDate.EndDate = value
    '    End Set
    'End Property
#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _ctrRangeDate = Nothing
        _idOperativeUnit = Nothing
        _IsProcesar = Nothing
    End Sub

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRecognition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.ControlHideStatus = False
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        IndigoGridView1.SetListAcction(INDGvRecognition, {eAcciones.View}.ToList())
        Me.Deshacer()
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ShowRecognitionDetail()
    End Sub
#End Region

#Region "Methods"
    Private Sub ShowRecognitionDetail()
        Dim itemRecognition = Nothing
        If _IsProcesar Then
            itemRecognition = CType(INDGvRecognition.GetFocusedRow(), ViewListRecognitionEntrance)
        Else
            itemRecognition = CType(INDGvRecognition.GetFocusedRow(), ViewListRecognitionReverse)
        End If
        Using frm As New FrmPopUpShowRecognitionDetail()
            frm.Title = String.Format("{0} - {1} - {2} Folios", itemRecognition.CareGroupCodeName, CType(itemRecognition.TotalCareGroup, Decimal).ToString("C0"), itemRecognition.FolioQuantity)
            frm.IsProcesar = Me._IsProcesar
            'frm.DateRange = Me._ctrRangeDate.GetDateValues()
            frm.OperativeUnitId = Me._idOperativeUnit
            frm.CareGroupId = itemRecognition.CareGroupId
            Dim transparent As New FrmTransparent(frm, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub
    Private Sub ProcesarReversar()
        INDGvRecognition.ShowLoadingPanel()
        Me._presenter.ProcesarReversar(_idOperativeUnit)
    End Sub
    ''' <summary>
    ''' Procesars this instance.
    ''' </summary>
    Public Sub ProcesarConfirmar()
        INDGvRecognition.ShowLoadingPanel()
        Me._presenter.ProcesarConfirmar(_idOperativeUnit)
    End Sub

    Private Function convertToXml() As String
        Dim cxml As New StringBuilder()

        cxml.Append("<RecognitionEntrance>")
        cxml.AppendFormat("<OperatingUnitId>{0}</OperatingUnitId>", Me._idOperativeUnit)
        CType(CType(INDGcRecognition.DataSource, PLinqServerModeSource).Source,
            List(Of ViewListRecognitionEntrance)).ForEach(Sub(o)

                                                              cxml.Append("<RecognitionEntranceDetail>")
                                                              cxml.AppendFormat("<CareGroupId>{0}</CareGroupId>", o.CareGroupId)
                                                              cxml.AppendFormat("<TotalCareGroup>{0}</TotalCareGroup>", o.TotalCareGroup)
                                                              cxml.AppendFormat("<FolioQuantity>{0}</FolioQuantity>", o.FolioQuantity)
                                                              cxml.Append("</RecognitionEntranceDetail>")

                                                          End Sub)
        cxml.Append("</RecognitionEntrance>")

        Return cxml.ToString()
    End Function
#End Region

#Region "IRecognition"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

        Me.BarraBotones.BarBtnConfirmar.Caption = "Liquidar"
        Me.BarraBotones.BarBtnDesconfirmar.Caption = "Reversar"


        Me.BarraBotones.BarbtnRefreshGrid.Caption = "Reconocidos"
        Me.BarraBotones.BarBtnRejillaEliminar.Caption = "No Reconocidos"

        Me._presenter.CleanControls()

        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True


        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Procesar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = False
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.RefreshGrid) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeleteGrid) = True
        End If
        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        End If
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
#End Region

#Region "BarButtonEvents"
    ''' <summary>
    ''' Barras the botones load.
    ''' </summary>
    Private Sub BarraBotones_Load() Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub
    ''' <summary>
    ''' Barras the botones click procesar.
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If INDGcRecognition.DataSource Is Nothing Then
            Exit Sub
        End If

        Dim listRecognition = CType(CType(INDGcRecognition.DataSource, PLinqServerModeSource).Source, List(Of ViewListRecognitionEntrance))
        If listRecognition.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para Liquidar"
            Exit Sub
        End If

        If MessageIndigo.Show("¿Esta seguro que desea Liquidar el Reconocimiento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.AsyncLoaderOnlyBar(True)

            Dim msgError As New StringBuilder()
            Dim msgSuccess As New StringBuilder()
            Dim msgRecognition As New List(Of Integer)()

            For Each recognition In listRecognition
                recognition.StateOperation = 1
                INDGcRecognition.RefreshDataSource()

                Dim result As ActionResult = Await Me._presenter.LiquidarByCareGroup(recognition.CareGroupId, recognition.TotalCareGroup, Me._idOperativeUnit, Me._ctrRangeDate.InitialDate)
                If result.StateResult Then
                    recognition.StateOperation = 2
                    recognition.MessageInfo = result.Message
                    msgSuccess.AppendLine(result.Message)
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        msgRecognition.Add(Convert.ToInt32(result.MessageResult(0)))
                    End If
                Else
                    recognition.StateOperation = 3
                    recognition.MessageInfo = result.Message
                    msgError.AppendLine(recognition.MessageInfo)
                End If
                INDGcRecognition.RefreshDataSource()
            Next

            Me.AsyncLoader(False)

            If msgSuccess.Length > 0 Then
                Me.Mensaje(Base.EeventViewerImages.Informacion) = msgSuccess.ToString()
            End If

            If msgError.Length > 0 Then
                Me.Mensaje(Base.EeventViewerImages.Advertencia) = msgError.ToString()
            End If

            If msgRecognition.Count > 0 Then
                Dim parameter = String.Join(",", msgRecognition)
                Dim reportDef As New Reporter.rptRevenueRecognitionReverse
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, parameter)
            End If
        End If
    End Sub

    Private Async Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        If INDGcRecognition.DataSource Is Nothing Then
            Exit Sub
        End If

        Dim listRecognition = CType(CType(INDGcRecognition.DataSource, PLinqServerModeSource).Source, List(Of ViewListRecognitionReverse))
        If listRecognition.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para Reversar"
            Exit Sub
        End If

        If MessageIndigo.Show("¿Esta seguro que desea Reversar el Reconocimiento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.AsyncLoaderOnlyBar(True)

            Dim msgError As New StringBuilder()
            Dim msgSuccess As New StringBuilder()
            Dim msgRecognition As New List(Of Integer)()

            For Each recognition In listRecognition
                recognition.StateOperation = 1
                INDGcRecognition.RefreshDataSource()

                Dim result As ActionResult = Await Me._presenter.LiquidarReversar(recognition.RecognitionId)
                If result.StateResult Then
                    recognition.StateOperation = 2
                    recognition.MessageInfo = result.Message
                    msgSuccess.AppendLine(result.Message)
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        msgRecognition.Add(Convert.ToInt32(result.MessageResult(0)))
                    End If
                Else
                    recognition.StateOperation = 3
                    recognition.MessageInfo = result.Message
                    msgError.AppendLine(recognition.MessageInfo)
                End If
                INDGcRecognition.RefreshDataSource()
            Next

            Me.AsyncLoader(False)

            If msgSuccess.Length > 0 Then
                Me.Mensaje(Base.EeventViewerImages.Informacion) = msgSuccess.ToString()
            End If

            If msgError.Length > 0 Then
                Me.Mensaje(Base.EeventViewerImages.Advertencia) = msgError.ToString()
            End If

            If msgRecognition.Count > 0 Then
                Dim parameter = String.Join(",", msgRecognition)
                Dim reportDef As New Reporter.rptRevenueRecognitionEntrance
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, parameter)
            End If
        End If
    End Sub


    ''' <summary>
    ''' Barras the botones click refresh grid.
    ''' </summary>
    Private Sub BarraBotones_Click_RefreshGrid() Handles BarraBotones.Click_RefreshGrid
        _IsProcesar = False

        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        End If

        Me.ProcesarReversar()
    End Sub
    ''' <summary>
    ''' Barras the botones click eliminar rejilla.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminarRejilla() Handles BarraBotones.ClickEliminarRejilla
        _IsProcesar = True

        If Me.BarraBotones.PermissionsForm IsNot Nothing AndAlso Me.BarraBotones.PermissionsForm.Count > 0 AndAlso Me.BarraBotones.PermissionsForm.ContainsKey(Int32.Parse(PermissionsActionsForm.Liquidar)) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = True
        End If

        Me.ProcesarConfirmar()
    End Sub
    ''' <summary>
    ''' Barras the botones click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id > 0 Then
            Me._idOperativeUnit = operatingUnit.Id
            If _IsProcesar Then
                Me.ProcesarConfirmar()
            Else
                Me.ProcesarReversar()
            End If
        End If
    End Sub

    Private Sub INDGvRecognition_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDGvRecognition.RowClick
        Dim hitInfo As GridHitInfo = INDGvRecognition.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))
        If hitInfo.InRow AndAlso hitInfo.InRowCell AndAlso hitInfo.Column.Name.Equals("ColState") Then
            Dim frmNotificationItem As New FrmNotificationItemDetail()
            If _IsProcesar Then
                frmNotificationItem.TxtMessage.Text = CType(INDGvRecognition.GetFocusedRow(), ViewListRecognitionEntrance).MessageInfo
            Else
                frmNotificationItem.TxtMessage.Text = CType(INDGvRecognition.GetFocusedRow(), ViewListRecognitionReverse).MessageInfo
            End If
            If String.IsNullOrEmpty(frmNotificationItem.TxtMessage.Text) Then
                Exit Sub
            End If
            Using transparent As New FrmTransparent(frmNotificationItem, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub
#End Region

End Class