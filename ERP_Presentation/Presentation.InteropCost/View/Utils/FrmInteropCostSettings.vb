Imports Infrastructure.CrossCutting.Base
Imports Presentation.InteropCost.MVP
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform

Public Class FrmInteropCostSettings
    Implements IInteropCostSetting

    Public Sub New()
        InitializeComponent()
        _presenter = New PInteropCostSettings(Me)
        model = New MInteropCostSetting(Me.Tag)
    End Sub

#Region "Properties"
    Private _date As DateTime
    Private _presenter As PInteropCostSettings
    Private model As MInteropCostSetting

    Public Const NAME_MODULE As String = "InteropCost"

    ReadOnly Property GetEstimationLaborType As List(Of Tuple(Of Byte, String))
        Get
            Dim _listEstimationLabor As New List(Of Tuple(Of Byte, String))
            _listEstimationLabor.Add(New Tuple(Of Byte, String)(1, "Total Devengados"))
            _listEstimationLabor.Add(New Tuple(Of Byte, String)(2, "Total Devengados Mas Carga patronal"))
            Return _listEstimationLabor
        End Get
    End Property

    Private _idOperativeUnit As Int32
    Private _interopCostSetting As InteropCostSetting
    Property JournalVoucherTypeCode As String
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
    Dim _record As BlockRecordInteropCost

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IInteropCostSetting.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IInteropCostSetting.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property AccountingCosts As Boolean Implements IInteropCostSetting.AccountingCosts
        Get
            Return INDsleAccountingCosts.EditValue
        End Get
        Set(value As Boolean)
            INDsleAccountingCosts.EditValue = value
        End Set
    End Property

#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _date = Nothing
        _presenter = Nothing
        model = Nothing
        _idOperativeUnit = Nothing
        _interopCostSetting = Nothing
        _record = Nothing
    End Sub

    Private Sub FrmInteropCostSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PInteropCostSettings(Me)
        _presenter.LoadDefinitionLayout()
        INDGleEstimationLaborType.Properties.DataSource = GetEstimationLaborType
        CleanControls()
        LoadControls()
    End Sub

    Private Sub FrmInteropCostSettings_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub

    Private Sub INDSleJournalVoucherType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleJournalVoucherType.QueryPopUp
        If INDSleJournalVoucherType.Properties.DataSource Is Nothing Then
            INDSleJournalVoucherType.Properties.DataSource = model.ListDocumentTypes(True)
        End If
    End Sub

    Private Sub FrmInteropCostSettings_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
    End Sub

    Private Sub INDSleJournalVoucherType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleJournalVoucherType.EditValueChanged
        If SearchLookUpEdit1View.GetFocusedRow() IsNot Nothing Then
            JournalVoucherTypeCode = CType(CType(SearchLookUpEdit1View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InteropCostRepository.CTNTIPCOMXpo).TCCODIGO
        End If
    End Sub

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si contabiliza costos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountingCosts_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountingCosts.EditValueChanged
        If AccountingCosts Then 'Si permite contabilizar costos visualiza el campo de tipo de comprobante
            INDLciJournalVoucherType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciJournalVoucherType.AllowHide = False
        Else 'Si no oculta el campo
            INDLciJournalVoucherType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciJournalVoucherType.AllowHide = True
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
            Using mCommon As New MCommonInteropCost(Me.Tag)
                Await mCommon.DeleteBlockRecordInteropCost(_record)
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
                .IdEntity =  "$#" & Me.Tag & "_" & Me._idOperativeUnit & "#$", .IdForm = Me.Tag, _
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

    Private Sub CleanControls() Implements IInteropCostSetting.CleanControls

        INDLcRoot.BeginUpdate()
        'ActionsOnControls = False
        _date = Me.GetDateServer()
        CtrDateNavigator1.SetYear = _date.Year
        CtrDateNavigator1.SetMonth = _date.Month
        INDSleJournalVoucherType.EditValue = Nothing
        INDGleEstimationLaborType.EditValue = Nothing
        Me._interopCostSetting = Nothing
        JournalVoucherTypeCode = String.Empty
        INDLcRoot.EndUpdate()

        INDSleJournalVoucherType.Properties.NullText = String.Empty

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()


        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    Public Sub AssigningValues() Implements IInteropCostSetting.AssigningValues
        With _interopCostSetting
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .AccountingCosts = AccountingCosts
            If INDLciJournalVoucherType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .JournalVoucherTypeId = INDSleJournalVoucherType.EditValue
                .JournalVoucherTypeCode = JournalVoucherTypeCode
            Else
                .JournalVoucherTypeId = Nothing
                .JournalVoucherTypeCode = String.Empty
            End If
            .Month = CtrDateNavigator1.GetMonth
            .Year = CtrDateNavigator1.GetYear
            .CostEstimateLabor = INDGleEstimationLaborType.EditValue
        End With
    End Sub

    Public Async Sub LoadControls() Implements IInteropCostSetting.LoadControls
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        AsyncLoader(True)
        _interopCostSetting = Await model.GetInteropCostSettingAsync()
        AsyncLoader(False)
        Me.CtrDateNavigator1.Enabled = Not model.HasMonthClosed()
        If _interopCostSetting IsNot Nothing AndAlso _interopCostSetting.Id > 0 Then
            Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, _interopCostSetting.Id)

                With Me._interopCostSetting
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    '.IdOperatingUnit = Me._idOperativeUnit
                    AccountingCosts = .AccountingCosts
                    INDSleJournalVoucherType.EditValue = .JournalVoucherTypeId
                    JournalVoucherTypeCode = .JournalVoucherTypeCode
                    CtrDateNavigator1.SetMonth = .Month
                    CtrDateNavigator1.SetYear = .Year
                    INDGleEstimationLaborType.EditValue = .CostEstimateLabor
                End With
                INDSleJournalVoucherType.Properties.NullText = _interopCostSetting.FullNameJournalVoucherType

                Me.GetDocumentIndexed(Me.Tag & "_" & Me._interopCostSetting.Id)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _interopCostSetting.Id}
                    Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                    _record = operation.ObjectEmbbeded
                Else
                    _record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                Me.BarraBotones.SetDocuments(_interopCostSetting.Id, Me.Tag.ToString(), Nothing, GetType(InteropCostSetting).Name)
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
        If Me._interopCostSetting IsNot Nothing AndAlso Me._interopCostSetting.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Me._interopCostSetting.MarkAsDeleted()
                    AsyncLoader(True)
                    Dim result = Await model.DeleteInteropCostSetting(Me._interopCostSetting)
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
            Dim Result = Await model.SaveInteropCostSetting(Me._interopCostSetting)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If _interopCostSetting.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf _interopCostSetting.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me._interopCostSetting = Result.ObjectEmbbeded
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