'***********************************************************************
' Assembly         : Presentacion.Contract.Utils
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/02/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Contract.MVP
Imports Presentation.Controls

#End Region

Public Class FrmSettingsContract
    Implements ISettingsContract

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

#End Region

#Region "Variables"

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PSettingsContract

    ''' <summary>
    ''' Representa la entidad de parametros de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsContract As SettingsContract

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As BlockRecordContract

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ISettingsContract.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor MyLayoutControl
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ISettingsContract.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingsContract.ActionsOnControls
        Set(value As Boolean)
            INDlySettings.BeginUpdate()

            INDsleCUPSWithRelatedDescription.Enabled = value
            INDsleRequestQuoteOutpatientServices.Enabled = value
            INDsleRequestQuoteIntrahospitalServices.Enabled = value
            INDsleRequestQuoteOutpatientProducts.Enabled = value
            INDsleRequestQuoteIntrahospitalProducts.Enabled = value

            BarraBotones.StatusRecordVisible = value

            INDlySettings.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si se pide la descripción relacionada
    ''' </summary>
    ''' <returns></returns>
    Public Property CUPSWithRelatedDescription As Boolean Implements ISettingsContract.CUPSWithRelatedDescription
        Get
            Return INDsleCUPSWithRelatedDescription.EditValue
        End Get
        Set(value As Boolean)
            INDsleCUPSWithRelatedDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Servicios Ambulatorios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestQuoteOutpatientServices As Boolean Implements ISettingsContract.RequestQuoteOutpatientServices
        Get
            Return INDsleRequestQuoteOutpatientServices.EditValue
        End Get
        Set(value As Boolean)
            INDsleRequestQuoteOutpatientServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Servicios Intrahospitalarios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestQuoteIntrahospitalServices As Boolean Implements ISettingsContract.RequestQuoteIntrahospitalServices
        Get
            Return INDsleRequestQuoteIntrahospitalServices.EditValue
        End Get
        Set(value As Boolean)
            INDsleRequestQuoteIntrahospitalServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Productos Ambulatorios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestQuoteOutpatientProducts As Boolean Implements ISettingsContract.RequestQuoteOutpatientProducts
        Get
            Return INDsleRequestQuoteOutpatientProducts.EditValue
        End Get
        Set(value As Boolean)
            INDsleRequestQuoteOutpatientProducts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite identifica si se Solicitan Cotización Productos Intrahospitalarios de manera restrictiva
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestQuoteIntrahospitalProducts As Boolean Implements ISettingsContract.RequestQuoteIntrahospitalProducts
        Get
            Return INDsleRequestQuoteIntrahospitalProducts.EditValue
        End Get
        Set(value As Boolean)
            INDsleRequestQuoteIntrahospitalProducts.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"

    Private _FillingYesOrNot As List(Of Tuple(Of Boolean, String))
    Private ReadOnly Property FillingYesOrNot As List(Of Tuple(Of Boolean, String))
        Get
            If _FillingYesOrNot Is Nothing Then
                _FillingYesOrNot = New List(Of Tuple(Of Boolean, String))
                _FillingYesOrNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _FillingYesOrNot.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _FillingYesOrNot
        End Get
    End Property

    Private _attentionGroupsDictionary As Dictionary(Of String, List(Of Tuple(Of Integer, String)))
    Private ReadOnly Property AttentionGroupsDictionary As Dictionary(Of String, List(Of Tuple(Of Integer, String)))
        Get
            If _attentionGroupsDictionary Is Nothing Then
                _attentionGroupsDictionary = New Dictionary(Of String, List(Of Tuple(Of Integer, String)))()
                _attentionGroupsDictionary.Add("es-CO", {
                    New Tuple(Of Integer, String)(1, "EAPB con contrato"),
                    New Tuple(Of Integer, String)(2, "EAPB sin contrato"),
                    New Tuple(Of Integer, String)(3, "Particulares"),
                    New Tuple(Of Integer, String)(4, "Aseguradoras")
                }.ToList())
                _attentionGroupsDictionary.Add("es-CR", {
                    New Tuple(Of Integer, String)(1, "Clientes con Contrato"),
                    New Tuple(Of Integer, String)(2, "Clientes sin Contrato"),
                    New Tuple(Of Integer, String)(3, "Particulares"),
                    New Tuple(Of Integer, String)(4, "Aseguradoras")
                }.ToList())
            End If

            Return _attentionGroupsDictionary
        End Get
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Método : Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MSettingsContract(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveSettingsContract(_settingsContract)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _settingsContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf _settingsContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If

                    Await LoadControls()
                Else
                    AsyncLoader(False)
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Carga el datasource del search que maneja tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        INDsleCUPSWithRelatedDescription.Properties.DataSource = FillingYesOrNot
        INDsleRequestQuoteOutpatientServices.Properties.DataSource = FillingYesOrNot
        INDsleRequestQuoteIntrahospitalServices.Properties.DataSource = FillingYesOrNot
        INDsleRequestQuoteOutpatientProducts.Properties.DataSource = FillingYesOrNot
        INDSleRequestQuoteIntrahospitalProducts.Properties.DataSource = FillingYesOrNot
        INDsleTypeGroupsAttention.Properties.DataSource = AttentionGroupsDictionary(indigo.LanguageCulture)
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlySettings.BeginUpdate()
        Await DeleteBlockedRecord()

        CUPSWithRelatedDescription = False
        RequestQuoteOutpatientServices = False
        RequestQuoteIntrahospitalServices = False
        RequestQuoteOutpatientProducts = False
        RequestQuoteIntrahospitalProducts = False

        _doc = Nothing
        _settingsContract = New SettingsContract

        ReadOnlyControls(False)
        ActionsOnControls = False

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

        INDlySettings.EndUpdate()
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await (Model.DeleteBlockRecord(_blockRecord))
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        Try
            Using Model As New MSettingsContract(CStr(Me.Tag))
                AsyncLoader(True)
                INDlySettings.BeginUpdate()
                Dim resulOperation = Await (Model.GetSettingsContractByOperatingUnitId(Me._idOperativeUnit))
                If resulOperation.StateResult Then
                    _settingsContract = resulOperation.ObjectEmbbeded
                    If _settingsContract IsNot Nothing AndAlso _settingsContract.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_settingsContract.Id))
                            With _settingsContract
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                CUPSWithRelatedDescription = .CUPSWithRelatedDescription
                                RequestQuoteOutpatientServices = .RequestQuoteOutpatientServices
                                RequestQuoteIntrahospitalServices = .RequestQuoteIntrahospitalServices
                                RequestQuoteOutpatientProducts = .RequestQuoteOutpatientProducts
                                RequestQuoteIntrahospitalProducts = .RequestQuoteIntrahospitalProducts
                                INDgcTypeGroupsAttention.DataSource = _settingsContract.SettingContractCareGroupType.ToList()
                                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._settingsContract.Id)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _settingsContract.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                            Me.BarraBotones.SetDocuments(_settingsContract.Id)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True

                            ActionsOnControls = True
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Deshacer()
                        Me.ActionsOnControls = True
                    End If
                Else
                    AsyncLoader(False)
                    Me.Mensaje(EeventViewerImages.Advertencia) = resulOperation.Message
                    Me.Deshacer()
                    Me.ActionsOnControls = True
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _settingsContract
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = Me._idOperativeUnit
            .CUPSWithRelatedDescription = CUPSWithRelatedDescription
            .RequestQuoteOutpatientServices = RequestQuoteOutpatientServices
            .RequestQuoteIntrahospitalServices = RequestQuoteIntrahospitalServices
            .RequestQuoteOutpatientProducts = RequestQuoteOutpatientProducts
            .RequestQuoteIntrahospitalProducts = RequestQuoteIntrahospitalProducts

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina un tipo de grupo de atención
    ''' </summary>
    Private Sub DeleteTypeGroupsAttention()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim entity = CType(INDviewTypeGroupsAttention.GetFocusedRow, SettingContractCareGroupType)
        entity.MarkAsDeleted()
        INDgcTypeGroupsAttention.DataSource = _settingsContract.SettingContractCareGroupType.ToList()
        INDgcTypeGroupsAttention.RefreshDataSource()
    End Sub
#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Rejilla tipo grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteTypeGroupsAttention()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Se ejecuta al dar click sobre agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If INDsleTypeGroupsAttention.EditValue Is Nothing OrElse INDsleTypeGroupsAttention.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe selecionar un tipo"
            Exit Sub
        End If

        If _settingsContract.SettingContractCareGroupType.Any(Function(m) m.CareGroupType = INDsleTypeGroupsAttention.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "El grupo atención ya se encuetra agregado"
            INDsleTypeGroupsAttention.EditValue = Nothing
            Exit Sub
        End If

        _settingsContract.SettingContractCareGroupType.Add(New SettingContractCareGroupType With {.CareGroupType = INDsleTypeGroupsAttention.EditValue})
        INDgcTypeGroupsAttention.DataSource = _settingsContract.SettingContractCareGroupType.ToList()
        INDsleTypeGroupsAttention.EditValue = Nothing
    End Sub

#End Region



#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingsContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlySettings, True)
        '****Inicializar variables*****'
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        _presenter = New PSettingsContract(Me)
        '******************************

        IndigoGridView1.SetListAcction(INDviewTypeGroupsAttention, {
                                       eAcciones.Remove
                                       }.ToList())

        InitializeTuple()
        Deshacer()
        LoadStatus()

        Await LoadControls()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _presenter = Nothing
        _settingsContract = Nothing
        _blockRecord = Nothing

        _FillingYesOrNot = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' se dispara cuando se activa el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSettingsAuthorization_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDsleCUPSWithRelatedDescription.Focus()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingsContract_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barra botones: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: cambia la unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            BarraBotones.StatusRecordVisible = False
            Await DeleteBlockedRecord()
            CleanControls()
            Await LoadControls()
        End If
    End Sub

    Private Sub INDviewUnitDosesType_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDviewTypeGroupsAttention.CustomUnboundColumnData
        If e.IsGetData Then
            e.Value = AttentionGroupsDictionary(indigo.LanguageCulture).Find(Function(m) m.Item1 = e.Row.CareGroupType).Item2
        End If
    End Sub

#End Region

End Class