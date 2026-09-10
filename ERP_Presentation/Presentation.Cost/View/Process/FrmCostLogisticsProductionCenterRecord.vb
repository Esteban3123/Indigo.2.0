'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/12/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Cost.MVP
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Windows.Forms
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class FrmCostLogisticsProductionCenterRecord
    Implements ICostLogisticsProductionCenterRecord

#Region "Fields"

    Private _logisticsProductionCenterRecord As CostLogisticsProductionCenterRecord

    Private _idOperativeUnit As Int32

    Private _sequence As Domain.Entities.CostSecuence

    Private _idCurrentSequence As Int64

    Private _presenter As PCostLogisticsProductionCenterRecord

    Private __model As MCostLogisticsProductionCenterRecord

    Private _record As BlockRecordCost

    Public Const MODULE_NAME As String = "Cost"

#End Region

#Region "Properties"

#Region "LogisticsProductionCenterRecord Entity"

    Public Property Code As String Implements ICostLogisticsProductionCenterRecord.Code
        Get
            Return Me.INDBteCode.Text
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                value = value.Trim()
            End If
            Me.INDBteCode.Text = value
        End Set
    End Property

    Public Property ProductionCenterId As Integer Implements ICostLogisticsProductionCenterRecord.ProductionCenterId
        Get
            If INDSleProductionCenterLogistics.EditValue IsNot Nothing Then
                Return CType(Me.INDSleProductionCenterLogistics.EditValue, Integer)
            End If
            Return 0
        End Get
        Set(value As Integer)
            Me.INDSleProductionCenterLogistics.EditValue = value
        End Set
    End Property

    Public Property RecordDate As DateTime Implements ICostLogisticsProductionCenterRecord.RecordDate
        Get
            If Me.INDDteDateRecord.EditValue IsNot Nothing Then
                Return CType(Me.INDDteDateRecord.EditValue, DateTime)
            End If
            Return DateTime.Now
        End Get
        Set(value As DateTime)
            Me.INDDteDateRecord.EditValue = value
        End Set
    End Property

    Public Property Description As String Implements ICostLogisticsProductionCenterRecord.Description
        Get
            If Me.INDTxtDescription.EditValue IsNot Nothing Then
                Return Me.INDTxtDescription.EditValue.ToString().Trim()
            End If
            Return String.Empty
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                value = value.Trim()
            End If
            Me.INDTxtDescription.EditValue = value
        End Set
    End Property

    Public Property Status As String Implements ICostLogisticsProductionCenterRecord.Status
        Get
            Return BarraBotones.StatusRecord.ToString()
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value
        End Set
    End Property

    Public Property ListLogisticsProductionCenterRecordDetail As List(Of CostLogisticsProductionCenterRecordDetail) Implements ICostLogisticsProductionCenterRecord.ListLogisticsProductionCenterRecordDetail
        Get
            If Me.INDGdcDetails.DataSource IsNot Nothing Then
                Return CType(Me.INDGdcDetails.DataSource, List(Of CostLogisticsProductionCenterRecordDetail))
            End If
            Return Me.INDGdcDetails.DataSource
        End Get
        Set(value As List(Of CostLogisticsProductionCenterRecordDetail))
            Me.INDGdcDetails.DataSource = value
            Me.INDGdcDetails.RefreshDataSource()
        End Set
    End Property

#End Region

#Region "Others"

    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostLogisticsProductionCenterRecord.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDSleProductionCenterLogistics.Enabled = value
            INDDteDateRecord.Enabled = value
            INDTxtDescription.Enabled = value

            INDPceAddDetail.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            INDlycRoot.EndUpdate()
            If value Then
                INDSleProductionCenterLogistics.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICostLogisticsProductionCenterRecord.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As String Implements ICostLogisticsProductionCenterRecord.MyTag
        Get
            Return IIf(Me.Tag IsNot Nothing, Me.Tag.ToString(), String.Empty)
        End Get
    End Property

    Public Property Sequence As CostSecuence Implements ICostLogisticsProductionCenterRecord.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As CostSecuence)
            Me._sequence = value
        End Set
    End Property

    Public Property ProductionCenterLogisticXpo As XPInstantFeedbackSource Implements ICostLogisticsProductionCenterRecord.ProductionCenterLogisticXpo
        Get
            Return Me.INDSleProductionCenterLogistics.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDSleProductionCenterLogistics.Properties.DataSource = value
        End Set
    End Property

    Public Property ProductionCenterTargetXpo As XPInstantFeedbackSource Implements ICostLogisticsProductionCenterRecord.ProductionCenterTargetXpo
        Get
            Return Me.INDSleProductionCenterTarget.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDSleProductionCenterTarget.Properties.DataSource = value
        End Set
    End Property

    Public Property MeasurementUnitXpo As XPInstantFeedbackSource Implements ICostLogisticsProductionCenterRecord.MeasurementUnitXpo
        Get
            Return Me.INDSleMeasurementUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDSleMeasurementUnit.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Builders"

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _logisticsProductionCenterRecord = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        __model = Nothing
        _record = Nothing
    End Sub

    Private Sub FrmLogisticsProductionCenterRecord_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        __model = New MCostLogisticsProductionCenterRecord(Me.Tag)

        IndigoGridControl1.RefreshGrid(INDGdcDetails)
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGdvDetails, _listActions)

        INDGdvDetails.Columns.ColumnByName("colActions").Width = 80

        _presenter = New PCostLogisticsProductionCenterRecord(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()

        Me.INDDteDateRecord.Properties.MaxValue = DateTime.Now
        'INDebtDistribution.AddRangeColumns("Centro Produccion", "Unidad Medida", "Cantidad", "Puntos de Valor")
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDBteCode.Text) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(INDBteCode.Text) Then
                    Me.NewLogisticsProductionCenterRecord()
                Else
                    LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDPceAddDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDPceAddDetail.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDPceAddDetail.ShowPopup()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDBtnAddDetail.Click
        If ValidateAddDetailControls() Then
            AddDetail(Me.INDSleProductionCenterTarget.EditValue, Me.INDSleMeasurementUnit.EditValue, Me.INDTxtMaximumAmount.EditValue)
            Me.INDSleProductionCenterTarget.Focus()
            Me.INDPceAddDetail.ShowPopup()
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _detail As CostLogisticsProductionCenterRecordDetail = CType(INDGdvDetails.GetFocusedRow(), CostLogisticsProductionCenterRecordDetail)
            _detail.MarkAsDeleted()
            If _logisticsProductionCenterRecord.ChangeTracker.State <> ObjectState.Added Then
                _logisticsProductionCenterRecord.MarkAsModified()
            End If
            ListLogisticsProductionCenterRecordDetail = _logisticsProductionCenterRecord.CostLogisticsProductionCenterRecordDetail.ToList()
        End If
    End Sub

#End Region

#Region "BarButton Events"

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _logisticsProductionCenterRecord.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _logisticsProductionCenterRecord.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _logisticsProductionCenterRecord.Id, 0, _logisticsProductionCenterRecord.Id, _idOperativeUnit)
    End Sub


    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If _logisticsProductionCenterRecord IsNot Nothing Then
            _logisticsProductionCenterRecord.Status = 2
        End If
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If _logisticsProductionCenterRecord IsNot Nothing Then
            _logisticsProductionCenterRecord.Status = 2
        End If
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If _logisticsProductionCenterRecord IsNot Nothing Then
            _logisticsProductionCenterRecord.Status = 3
        End If
        Guardar()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleProductionCenterLogistics_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleProductionCenterLogistics.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1729, Nothing, True)
            _presenter.InitializeProductionCenter()
        End If
    End Sub

    Private Sub INDSleProductionCenterTarget_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleProductionCenterTarget.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1729, Nothing, True)
            _presenter.InitializeProductionCenterTarget()
        End If
    End Sub

    Private Sub INDSleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(300, Nothing, True)
            _presenter.InitializeMeasurementUnit()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleProductionCenterLogistics_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleProductionCenterLogistics.QueryPopUp
        If ProductionCenterLogisticXpo Is Nothing Then
            _presenter.InitializeProductionCenter()
        End If
    End Sub

    Private Sub INDSleProductionCenterTarget_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleProductionCenterTarget.QueryPopUp
        If ProductionCenterTargetXpo Is Nothing Then
            _presenter.InitializeProductionCenterTarget()
        End If
    End Sub

    Private Sub INDSleMeasurementUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleMeasurementUnit.QueryPopUp
        If MeasurementUnitXpo Is Nothing Then
            _presenter.InitializeMeasurementUnit()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmCostLogisticsProductionCenterRecord_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
    End Sub

#End Region

#Region "Popup"

    Private Sub INDPceAddDetail_Popup(sender As Object, e As EventArgs) Handles INDPceAddDetail.Popup
        INDSleProductionCenterTarget.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

#Region "Others"

    Private Async Sub NewLogisticsProductionCenterRecord()
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        Me._logisticsProductionCenterRecord = New CostLogisticsProductionCenterRecord()
        Me._logisticsProductionCenterRecord.Status = 1
        ListLogisticsProductionCenterRecordDetail = Me._logisticsProductionCenterRecord.CostLogisticsProductionCenterRecordDetail.ToList()
        RecordDate = DateTime.Now
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense.ContainsKey(CInt(Me._idCurrentSequence)) AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                    Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Else
                    Using model As New MCommonCost(Me.Tag)
                        Dim res = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        If Not Me.DicSequense.ContainsKey(Me._idCurrentSequence) Then
                            Me.DicSequense.Add(Me._idCurrentSequence, res)
                        Else
                            Me.DicSequense(Me._idCurrentSequence) = res
                        End If
                    End Using
                    If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        Exit Sub
                    End If
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If
        Me.ActionsOnControls = True
        Me.INDDteDateRecord.Focus()
    End Sub

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._logisticsProductionCenterRecord.Code, INDSleProductionCenterLogistics.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._logisticsProductionCenterRecord.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._logisticsProductionCenterRecord.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._logisticsProductionCenterRecord.Code, INDSleProductionCenterLogistics.Text)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._logisticsProductionCenterRecord.Code)
        End If
        Return Me._doc
    End Function

    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonInteropCost As New MCommonCost(Me.Tag)
                Await ModelCommonInteropCost.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Public Sub AssigningValues() Implements ICostLogisticsProductionCenterRecord.AssigningValues
        With _logisticsProductionCenterRecord
            .Code = Code
            .RecordDate = RecordDate
            .ProductionCenterId = ProductionCenterId
            .Description = Description
        End With
    End Sub

    Public Sub CleanPopUp() Implements ICostLogisticsProductionCenterRecord.CleanPopUp
        INDlycRoot.BeginUpdate()
        Me.INDSleProductionCenterTarget.EditValue = Nothing
        Me.INDSleMeasurementUnit.EditValue = Nothing
        Me.INDTxtMaximumAmount.EditValue = Nothing
        INDlycRoot.EndUpdate()
    End Sub

    Public Sub CleanControls() Implements ICostLogisticsProductionCenterRecord.CleanControls
        INDlycRoot.BeginUpdate()

        ReadOnlyControls(False, INDlycRoot)
        Code = Nothing
        RecordDate = DateTime.Now
        ProductionCenterId = Nothing
        INDSleProductionCenterLogistics.Properties.NullText = String.Empty
        Description = Nothing
        ListLogisticsProductionCenterRecordDetail = Nothing

        _logisticsProductionCenterRecord = Nothing

        ActionsOnControls = False

        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If

        INDlycRoot.EndUpdate()
        CleanPopUp()
    End Sub

    Public Async Sub LoadControls() Implements ICostLogisticsProductionCenterRecord.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            'INDlycRoot.BeginUpdate()
            AsyncLoader(True)
            Dim res = Await __model.GetCostLogisticsProductionCenterRecordByCodeAsync(Me.Code)
            If res.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(res.StatusCode) = res.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _logisticsProductionCenterRecord = res.ObjectEmbbeded
            If _logisticsProductionCenterRecord IsNot Nothing AndAlso _logisticsProductionCenterRecord.Id > 0 Then
                Using ModelInteropCost As New MCommonCost(Me.Tag)
                    Dim result = Await ModelInteropCost.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _logisticsProductionCenterRecord.Id)

                    With _logisticsProductionCenterRecord
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        RecordDate = .RecordDate
                        ProductionCenterId = .ProductionCenterId
                        INDSleProductionCenterLogistics.Properties.NullText = .ProductionCenterCodeName
                        Description = .Description
                        Dim list = .CostLogisticsProductionCenterRecordDetail.ToList()
                        ListLogisticsProductionCenterRecordDetail = list
                        Status = .Status.ToString()
                    End With

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._logisticsProductionCenterRecord.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _logisticsProductionCenterRecord.Id}
                        Dim operation = Await ModelInteropCost.SaveBlockRecordCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_logisticsProductionCenterRecord.Id, MyTag, Nothing, GetType(LogisticsProductionCenterRecord).Name)
                    If _logisticsProductionCenterRecord.Status = 1 Then
                        IndigoGridView1.RaiseMenuPopUp = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                    Else
                        IndigoGridView1.RaiseMenuPopUp = False
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        ReadOnlyControls(True)
                    End If
                    AsyncLoader(False)
                    ActionsOnControls = True
                    INDBteCode.Enabled = False
                End Using
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _logisticsProductionCenterRecord.Id, 0, _logisticsProductionCenterRecord.Id, _idOperativeUnit)
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewLogisticsProductionCenterRecord()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            'INDlycRoot.EndUpdate()
        End If
    End Sub

    Private Function ValidateAddDetailControls() As Boolean
        Dim errorList As New StringBuilder()
        If CType(INDSleMeasurementUnit.EditValue, Decimal) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Unidad de Medida"))
        End If
        If INDTxtMaximumAmount.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cantidad"))
        End If
        If CType(INDSleProductionCenterTarget.EditValue, Integer) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Centro de Producción"))
        End If
        If CType(INDSleProductionCenterLogistics.EditValue, Integer) = CType(INDSleProductionCenterTarget.EditValue, Integer) Then
            errorList.AppendLine("El centro de producción " + INDSleProductionCenterTarget.Text + " no puede ser el mismo de la cabecera")
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
        Return True
    End Function

    Private Sub AddDetail(ByVal productionCenterId As Integer, ByVal measurementeUnitId As Integer, ByVal count As Decimal)
        If Me._logisticsProductionCenterRecord.CostLogisticsProductionCenterRecordDetail.Any(Function(d) d.ProductionCenterId = productionCenterId And d.InventoryMeasurementUnitId = measurementeUnitId) Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya existe el Centro de Producción con la misma Unidad de Medida"
            Exit Sub
        End If
        Dim detail As New CostLogisticsProductionCenterRecordDetail()
        Dim pCenter = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.GetProductionCenterById(productionCenterId)
        Dim mUnit = XpoServiceEx.Instance(indigo.TransactionalContainer).CostService.GetMeasurementUnitById(measurementeUnitId)
        detail.ProductionCenterId = productionCenterId
        detail.ProductionCenterCodeName = pCenter.Code & " - " & pCenter.Name
        detail.InventoryMeasurementUnitId = measurementeUnitId
        detail.MeasurementUnitCodeName = mUnit.Code & " - " & mUnit.Name
        detail.Count = count
        Me._logisticsProductionCenterRecord.CostLogisticsProductionCenterRecordDetail.Add(detail)
        ListLogisticsProductionCenterRecordDetail = Me._logisticsProductionCenterRecord.CostLogisticsProductionCenterRecordDetail.ToList()
        Me.CleanPopUp()
    End Sub

#End Region

#Region "ICrudBase"

    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me._logisticsProductionCenterRecord IsNot Nothing AndAlso Me._logisticsProductionCenterRecord.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await __model.DeleteCostLogisticsProductionCenterRecordAsync(Me._logisticsProductionCenterRecord, _idCurrentSequence)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                    End If
                    Mensaje(result.StatusCode) = result.Message
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Me.BarraBotones.Focus()
        If ValidateControls() = True Then
            If INDGdvDetails.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una información detallada"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result = Await __model.SaveCostLogisticsProductionCenterRecordAsync(Me._logisticsProductionCenterRecord, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _logisticsProductionCenterRecord.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                    End If
                End If
                Me._logisticsProductionCenterRecord = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
                Mensaje(EeventViewerImages.Informacion) = result.Message
            Else
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewLogisticsProductionCenterRecord()
        End If
    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda

        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))


        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Centro de Producción", .FieldName = "ProductionCenterId.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Fecha Registro", .FieldName = "RecordDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostLogisticsProductionCenterRecord
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#End Region

End Class