'***********************************************************************
' Assembly         : Presentacion.Budget.Utils
' Author           : Jeisson Herrera Peña
' Created          : 21/09/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Presentation.Budget.MVP
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Presentation.Controls.MVP
Imports Presentation.Common
Imports Domain.Base.Entities

#End Region

Public Class FrmParametersBudget
    Implements ISettingBudget

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la creación de cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreatePayableAccounts As Boolean? Implements ISettingBudget.CreatePayableAccounts
        Get
            Return INDSleCreateAccountsPayable.EditValue
        End Get
        Set(value As Boolean?)
            INDSleCreateAccountsPayable.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la creación de cuentas por cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreateReceivableAccounts As Boolean? Implements ISettingBudget.CreateReceivableAccounts
        Get
            Return INDSleCreateAccountReceivable.EditValue
        End Get
        Set(value As Boolean?)
            INDSleCreateAccountReceivable.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la creación de reservas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CreateReserves As Boolean? Implements ISettingBudget.CreateReserves
        Get
            Return INDSleCreateReservation.EditValue
        End Get
        Set(value As Boolean?)
            INDSleCreateReservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la agrupación de documentos por tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentsGroupping As Boolean? Implements ISettingBudget.DocumentsGroupping
        Get
            Return INDSleGroupDocuments.EditValue
        End Get
        Set(value As Boolean?)
            INDSleGroupDocuments.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la activación de interface con todos los módulos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EnabledInterface As Boolean? Implements ISettingBudget.EnabledInterface
        Get
            Return INDSleEnableInterface.EditValue
        End Get
        Set(value As Boolean?)
            INDSleEnableInterface.EditValue = value
        End Set
    End Property

    Public Property idOperatingUnit As Integer Implements ISettingBudget.idOperatingUnit

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As String Implements ISettingBudget.MyTag
        Get
            Return Me.Tag
        End Get
    End Property



#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

    Dim filter() As Object = {5, True}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBudget

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PSettingBudget

    ''' <summary>
    ''' Representa la entidad de parametros de Presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _settingBudget As SettingsBudget

    ''' <summary>
    ''' bandera para saber si se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Dim editPopup As Boolean

#End Region

#Region "ICrud"
    ''' <summary>
    ''' Metodo Buscar sin uso
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub
    ''' <summary>
    ''' Eliminar sin uso
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Using model As New MSettingBudget(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveSettingBudget(_settingBudget)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If _settingBudget.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf _settingBudget.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me._settingBudget = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                DeleteBlockedRecord()
                CleanControls()
                LoadControls()
                INDSleEnableInterface.Focus()

                Me.BarraBotones.CleanAuditBasic()
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Result.ObjectEmbbeded.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Result.ObjectEmbbeded.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Result.ObjectEmbbeded.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Result.ObjectEmbbeded.ModificationDate)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub
    ''' <summary>
    ''' Metodo actualizar sin uso
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' Metodo para generar un nuevo parametro de preuspuesto, sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
    ''' <summary>
    ''' Abre la busqueda en el visor, sin usarse
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento Load del Formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmParametersBudget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcParameters, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        Me.indigo = SessionValues.Instance
        Presenter = New PSettingBudget(Me)
        LoadStatus()
        Deshacer()
        LoadControls()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento closing del Formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmParametersBudget_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#End Region

#Region "Methods"

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
    Private Sub CleanControls()
        CreatePayableAccounts = Nothing
        CreateReceivableAccounts = Nothing
        CreateReserves = Nothing
        EnabledInterface = Nothing
        DocumentsGroupping = Nothing
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MSettingBudget(MyTag)
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MSettingBudget(MyTag)
            AsyncLoader(True)
            Dim resulOperation = Await Model.GetSettingBudgetById(_idOperativeUnit)
            AsyncLoader(False)
            _settingBudget = resulOperation.ObjectEmbbeded
            If Not _settingBudget Is Nothing Then
                If _settingBudget.Id > 0 Then
                    Using ModelRecord As New MSettingBudget(MyTag)
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_settingBudget.Id))
                        With _settingBudget
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.CleanAuditBasic()
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            CreatePayableAccounts = .CreatePayableAccounts
                            CreateReceivableAccounts = .CreateReceivableAccounts
                            CreateReserves = .CreateReserves
                            EnabledInterface = .EnabledInterface
                            DocumentsGroupping = .DocumentsGroupping
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._settingBudget.Id)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = _settingBudget.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                            _record = result
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        Me.BarraBotones.SetDocuments(_settingBudget.Id)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                    End Using
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    CleanControls()
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                CleanControls()
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
            End If
        End Using
        INDSleEnableInterface.Focus()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _settingBudget
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .CreatePayableAccounts = CreatePayableAccounts
            .CreateReceivableAccounts = CreateReceivableAccounts
            .CreateReserves = CreateReserves
            .EnabledInterface = EnabledInterface
            .DocumentsGroupping = DocumentsGroupping
            .OperatingUnitId = _idOperativeUnit

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Buttons Bar"

    ''' <summary>
    ''' Barra botones: cambia la unidad operativa
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            BarraBotones.StatusRecordVisible = False
            DeleteBlockedRecord()
            CleanControls()
            LoadControls()
            If _settingBudget IsNot Nothing AndAlso _settingBudget.Id > 0 Then
                _settingBudget.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
        End If
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
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

#End Region

End Class