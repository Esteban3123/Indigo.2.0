#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Authorization.MVP
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

Public Class FrmSettingsAuthorization
    Implements ISettingsAuthorization

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Authorization"

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
    Dim _presenter As PSettingsAuthorization

    ''' <summary>
    ''' Representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsAuthorization As SettingsAuthorization

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Dim _blockRecord As BlockRecordAuthorization

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ISettingsAuthorization.MyTag
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ISettingsAuthorization.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISettingsAuthorization.ActionsOnControls
        Set(value As Boolean)
            INDLcBase.BeginUpdate()

            INDGleAutomaticAllocation.Enabled = value
            INDSeDaysToAssign.Enabled = value

            BarraBotones.StatusRecordVisible = value

            INDLcBase.EndUpdate()
        End Set
    End Property

    Public Property AutomaticAllocation As Boolean Implements ISettingsAuthorization.AutomaticAllocation
        Get
            Return INDGleAutomaticAllocation.EditValue
        End Get
        Set(value As Boolean)
            INDGleAutomaticAllocation.EditValue = value
        End Set
    End Property

    Public Property DaysToAssign As Integer Implements ISettingsAuthorization.DaysToAssign
        Get
            Return INDSeDaysToAssign.EditValue
        End Get
        Set(value As Integer)
            INDSeDaysToAssign.EditValue = value
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
    ''' Método : Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MSettingsAuthorization(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveSettingsAuthorization(_settingsAuthorization)
                If Result.StateResult = True Then
                    If _settingsAuthorization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    ElseIf _settingsAuthorization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
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
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        INDGleAutomaticAllocation.Properties.DataSource = FillingYesOrNot
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
        INDLcBase.BeginUpdate()
        Await DeleteBlockedRecord()

        AutomaticAllocation = False
        DaysToAssign = 1

        _doc = Nothing
        _settingsAuthorization = New SettingsAuthorization

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

        INDLcBase.EndUpdate()
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        Try
            Using Model As New MSettingsAuthorization(CStr(Me.Tag))
                AsyncLoader(True)
                INDLcBase.BeginUpdate()
                Dim resulOperation = Await Model.GetSettingsAuthorization()
                If resulOperation.StateResult Then
                    _settingsAuthorization = resulOperation.ObjectEmbbeded
                    If _settingsAuthorization IsNot Nothing AndAlso _settingsAuthorization.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_settingsAuthorization.Id))
                            With _settingsAuthorization
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                AutomaticAllocation = .AutomaticAllocation
                                DaysToAssign = .DaysToAssign
                                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._settingsAuthorization.Id)
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordAuthorization With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _settingsAuthorization.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                            Me.BarraBotones.SetDocuments(_settingsAuthorization.Id)
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
        With _settingsAuthorization
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .AutomaticAllocation = AutomaticAllocation
            .DaysToAssign = DaysToAssign

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingsAuthorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcBase, True)
        '****Inicializar variables*****'
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        _presenter = New PSettingsAuthorization(Me)
        '******************************

        InitializeTuples()
        Deshacer()
        LoadStatus()

        Await LoadControls()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _presenter = Nothing
        _settingsAuthorization = Nothing
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
        INDGleAutomaticAllocation.Focus()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmSettingsAuthorization_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#End Region

#Region "Bar Buttons"

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
        End If
    End Sub

#End Region

End Class