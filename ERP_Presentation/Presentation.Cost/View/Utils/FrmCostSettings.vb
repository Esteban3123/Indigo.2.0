Imports Infrastructure.CrossCutting.Base
Imports Presentation.Cost.MVP
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform

Public Class FrmCostSettings
    Implements ICostSetting

    Public Sub New()
        InitializeComponent()
        _presenter = New PCostSettings(Me)
        model = New MCostSetting(Me.Tag)
    End Sub

#Region "Properties"
    Private _date As DateTime
    Private _presenter As PCostSettings
    Private model As MCostSetting

    Public Const NAME_MODULE As String = "Cost"

    ReadOnly Property GetEstimationLaborType As List(Of Tuple(Of Byte, String))
        Get
            Dim _listEstimationLabor As New List(Of Tuple(Of Byte, String))
            _listEstimationLabor.Add(New Tuple(Of Byte, String)(1, "Total Devengados"))
            _listEstimationLabor.Add(New Tuple(Of Byte, String)(2, "Total Devengados Mas Carga patronal (Seguridad Social)"))
            _listEstimationLabor.Add(New Tuple(Of Byte, String)(3, "Total Devengados Mas Carga patronal (Seguridad Social + Parafiscales)"))
            _listEstimationLabor.Add(New Tuple(Of Byte, String)(4, "Total Devengados Mas Carga patronal (Seguridad Social + Parafiscales + Provisiones)"))
            Return _listEstimationLabor
        End Get
    End Property

    ReadOnly Property GetValidateActivities As List(Of Tuple(Of Boolean, String))
        Get
            Dim _listValidateActivities As New List(Of Tuple(Of Boolean, String))
            _listValidateActivities.Add(New Tuple(Of Boolean, String)(True, "Si"))
            _listValidateActivities.Add(New Tuple(Of Boolean, String)(False, "No"))
            Return _listValidateActivities
        End Get
    End Property

    ReadOnly Property GetActivityCalculationBy As List(Of Tuple(Of Byte, String))
        Get
            Dim _listActivityCalculationBy As New List(Of Tuple(Of Byte, String))
            _listActivityCalculationBy.Add(New Tuple(Of Byte, String)(1, "Organización"))
            _listActivityCalculationBy.Add(New Tuple(Of Byte, String)(2, "Centro de Producción"))
            Return _listActivityCalculationBy
        End Get
    End Property

    Private _idOperativeUnit As Int32
    Private _costSetting As CostSetting
    Public Property IdOperatingUnit As Integer
        Get
            Return Me._idOperativeUnit
        End Get
        Set(value As Integer)
            _idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que contiene el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordCost

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostSetting.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements ICostSetting.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Permite saber si contabiliza costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountingCosts As Boolean Implements ICostSetting.AccountingCosts
        Get
            Return INDsleAccountingCosts.EditValue
        End Get
        Set(value As Boolean)
            INDsleAccountingCosts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptId As Integer Implements ICostSetting.AccountPayableConceptId
        Get
            Return INDsleAccountPayableConcept.EditValue
        End Get
        Set(value As Integer)
            INDsleAccountPayableConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostSetting.AccountPayableConceptXpo
        Get
            Return INDsleAccountPayableConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los tipos de comprobante contable (campo: provisiones)
    ''' </summary>
    ''' <returns></returns>
    Public Property AverageStandardCostActivity As Boolean Implements ICostSetting.AverageStandardCostActivity
        Get
            Return INDgleAverageStandardCostActivity.EditValue
        End Get
        Set(value As Boolean)
            INDgleAverageStandardCostActivity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los tipos de comprobante contable (campo: provisiones)
    ''' </summary>
    ''' <returns></returns>
    Public Property ProvisionJournalVoucherTypeId As Integer Implements ICostSetting.ProvisionJournalVoucherTypeId
        Get
            Return INDSleProvisions.EditValue
        End Get
        Set(value As Integer)
            INDSleProvisions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los tipos de comprobante contable (campo: provisiones)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProvisionJournalVoucherTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostSetting.ProvisionJournalVoucherTypeXpo
        Get
            Return INDSleProvisions.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleProvisions.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Establece los tipos de comprobante contable (campo: reversion de provisiones)
    ''' </summary>
    ''' <returns></returns>
    Public Property ProvisionReversalJournalVoucherTypeId As Integer Implements ICostSetting.ProvisionReversalJournalVoucherTypeId
        Get
            Return INDSleProvisionReversalJournalVoucherType.EditValue
        End Get
        Set(value As Integer)
            INDSleProvisionReversalJournalVoucherType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los tipos de comprobante contable (campo: reversion de provisiones)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProvisionReversalJournalVoucherTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICostSetting.ProvisionReversalJournalVoucherTypeXpo
        Get
            Return INDSleProvisionReversalJournalVoucherType.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleProvisionReversalJournalVoucherType.Properties.DataSource = value
        End Set
    End Property


#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _date = Nothing
        _presenter = Nothing
        model = Nothing
        _record = Nothing
    End Sub

    Private Sub FrmInteropCostSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter.LoadDefinitionLayout()
        INDgleEstimationLaborType.Properties.DataSource = GetEstimationLaborType
        INDgleValidateActivities.Properties.DataSource = GetValidateActivities
        INDGleActivityCalculationBy.Properties.DataSource = GetActivityCalculationBy
        INDgleAverageStandardCostActivity.Properties.DataSource = GetValidateActivities
        CleanControls()
        LoadControls()
    End Sub

    Private Sub FrmCostSettings_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmInteropCostSettings_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDSleJournalVoucherType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleJournalVoucherType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("607", Nothing, True)
        End If
    End Sub

    Private Sub INDsleAccountPayableConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountPayableConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(725, Nothing, True)
            _presenter.InitializeAccountPayableConcept()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleJournalVoucherType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleJournalVoucherType.QueryPopUp
        If INDSleJournalVoucherType.Properties.DataSource Is Nothing Then
            INDSleJournalVoucherType.Properties.DataSource = model.ListDocumentTypes(True)
        End If
    End Sub

    Private Sub INDsleAccountPayableConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountPayableConcept.QueryPopUp
        If AccountPayableConceptXpo Is Nothing Then
            _presenter.InitializeAccountPayableConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento de consulta de los comprobantes contables (campo: provisión)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProvisions_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProvisions.QueryPopUp
        If ProvisionJournalVoucherTypeXpo Is Nothing Then
            _presenter.InitializeProvisionJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento de consulta de los comprobantes contables (campo:reversa de provisión)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProvisionReversalJournalVoucherType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProvisionReversalJournalVoucherType.QueryPopUp
        If ProvisionReversalJournalVoucherTypeXpo Is Nothing Then
            _presenter.InitializeProvisionReversalJournalVoucherType()
        End If
    End Sub


#End Region

#End Region

#Region "Methods"
    Private Sub LoadSettings()
        AsyncLoader(True)
        AsyncLoader(False)
    End Sub

    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using mCommon As New MCommonCost(Me.Tag)
                Await mCommon.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._date.Year, Me._date.Month), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._idOperativeUnit & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._idOperativeUnit), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._date.Year, Me._date.Month)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._idOperativeUnit)
            Return Me._doc
        End If
    End Function

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    Private Sub CleanControls() Implements ICostSetting.CleanControls
        INDLcRoot.BeginUpdate()
        'ActionsOnControls = False
        _date = Me.GetDateServer()
        CtrDateNavigator1.SetYear = _date.Year
        CtrDateNavigator1.SetMonth = _date.Month
        INDSleJournalVoucherType.EditValue = Nothing
        INDgleEstimationLaborType.EditValue = Nothing
        INDgleValidateActivities.EditValue = Nothing
        INDGleActivityCalculationBy.EditValue = Nothing
        AverageStandardCostActivity = False
        AccountPayableConceptId = Nothing
        INDsleAccountPayableConcept.Properties.NullText = String.Empty
        Me._costSetting = Nothing
        INDSleJournalVoucherType.Properties.NullText = String.Empty
        INDLcRoot.EndUpdate()

        INDSleJournalVoucherType.Properties.NullText = String.Empty

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()


        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    Public Sub AssigningValues() Implements ICostSetting.AssigningValues
        With _costSetting
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .AccountingCosts = AccountingCosts
            If INDLciJournalVoucherType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .JournalVoucherTypeId = INDSleJournalVoucherType.EditValue
            Else
                .JournalVoucherTypeId = Nothing
            End If
            .Month = CtrDateNavigator1.GetMonth
            .Year = CtrDateNavigator1.GetYear
            .CostEstimateLabor = INDgleEstimationLaborType.EditValue
            .ValidateActivities = INDgleValidateActivities.EditValue
            .ActivityCalculationBy = INDGleActivityCalculationBy.EditValue
            .AverageStandardCostActivity = AverageStandardCostActivity
            .AccountPayableConceptsId = AccountPayableConceptId
            .ProvisionJournalVoucherTypeId = Me.ProvisionJournalVoucherTypeId
            .ProvisionReversalJournalVoucherTypeId = Me.ProvisionReversalJournalVoucherTypeId

        End With
    End Sub

    Public Async Sub LoadControls() Implements ICostSetting.LoadControls
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        AsyncLoader(True)
        _costSetting = Await model.GetCostSettingAsync()
        AsyncLoader(False)
        If _costSetting IsNot Nothing AndAlso _costSetting.Id > 0 Then
            Using ModelCommon As New MCommonCost(Me.Tag)
                Dim result = Await ModelCommon.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _costSetting.Id)

                With Me._costSetting
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    AccountingCosts = .AccountingCosts
                    INDSleJournalVoucherType.EditValue = .JournalVoucherTypeId
                    CtrDateNavigator1.SetYear = .Year
                    CtrDateNavigator1.SetMonth = .Month
                    INDgleEstimationLaborType.EditValue = .CostEstimateLabor
                    INDgleValidateActivities.EditValue = .ValidateActivities
                    INDGleActivityCalculationBy.EditValue = .ActivityCalculationBy
                    AverageStandardCostActivity = .AverageStandardCostActivity
                    AccountPayableConceptId = .AccountPayableConceptsId
                    INDsleAccountPayableConcept.Properties.NullText = .AccountPayableConceptDescription
                    Me.ProvisionJournalVoucherTypeId = .ProvisionJournalVoucherTypeId
                    INDSleProvisions.Properties.NullText = .ProvisionJournalVoucherTypeDescription
                    Me.ProvisionReversalJournalVoucherTypeId = .ProvisionReversalJournalVoucherTypeId
                    INDSleProvisionReversalJournalVoucherType.Properties.NullText = .ProvisionReversalJournalVoucherTypeDescription
                End With
                INDSleJournalVoucherType.Properties.NullText = _costSetting.FullNameJournalVoucherType

                Me.GetDocumentIndexed(Me.Tag & "_" & Me._costSetting.Id)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _costSetting.Id}
                    Dim operation = Await ModelCommon.SaveBlockRecordCost(_record)
                    _record = operation.ObjectEmbbeded
                Else
                    _record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                Me.BarraBotones.SetDocuments(_costSetting.Id, Me.Tag.ToString(), Nothing, GetType(CostSetting).Name)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        End If
        INDSleJournalVoucherType.Focus()
    End Sub
#End Region

#Region "Crud"
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me._costSetting IsNot Nothing AndAlso Me._costSetting.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Me._costSetting.MarkAsDeleted()
                    AsyncLoader(True)
                    Dim result = Await model.DeleteCostSetting(Me._costSetting)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                Catch ex As Exception
                    AsyncLoader(False)
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim Result = Await model.SaveCostSetting(Me._costSetting)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If _costSetting.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                Else
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me._costSetting = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Deshacer()
                LoadControls()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
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
        Deshacer()
    End Sub
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            DeleteBlockedRecord()
            CleanControls()
            Me._idOperativeUnit = operatingUnit.Id
            LoadControls()
        End If
    End Sub

#End Region

End Class