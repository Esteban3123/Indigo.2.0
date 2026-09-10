'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 22-05-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Budget.MVP
Imports Presentation.Controls
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Domain.Base.Entities
#End Region

Public Class FrmSettingBudget
    Implements ISettingBudget

#Region "Const"
    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Private Const NAME_MODULE = "Budget"
#End Region

#Region "Properties"
    ''' <summary>
    ''' Crear cuentas por pagar
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [create payable accounts]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public Property CreatePayableAccounts As Boolean? Implements ISettingBudget.CreatePayableAccounts
        Get
            Return INDRgCreatePayableAccounts.EditValue
        End Get
        Set(value As Boolean?)
            INDRgCreatePayableAccounts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Crear cuentas por cobrar
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [create receivable accounts]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public Property CreateReceivableAccounts As Boolean? Implements ISettingBudget.CreateReceivableAccounts
        Get
            Return INDRgCreateReceivableAccounts.EditValue
        End Get
        Set(value As Boolean?)
            INDRgCreateReceivableAccounts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Crear reservas
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [create reserves]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public Property CreateReserves As Boolean? Implements ISettingBudget.CreateReserves
        Get
            Return INDRgCreateReserves.EditValue
        End Get
        Set(value As Boolean?)
            INDRgCreateReserves.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Agrupar documentos por tercero
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [documents groupping]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public Property DocumentsGroupping As Boolean? Implements ISettingBudget.DocumentsGroupping
        Get
            Return INDRgDocumentsGroupping.EditValue
        End Get
        Set(value As Boolean?)
            INDRgDocumentsGroupping.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Habilitar interface con los otros modulos
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [enabled interface]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public Property EnabledInterface As Boolean? Implements ISettingBudget.EnabledInterface
        Get
            Return INDRgEnabledInterface.EditValue
        End Get
        Set(value As Boolean?)
            INDRgEnabledInterface.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As String Implements ISettingBudget.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' id de la Unidad Operativa
    ''' </summary>
    ''' <value>
    ''' The operating unit.
    ''' </value>
    Public Property idOperatingUnit As Integer Implements ISettingBudget.idOperatingUnit
        Get
            Return _idOperativeUnit
        End Get
        Set(value As Integer)
            _idOperativeUnit = value
        End Set
    End Property
#End Region

#Region "Globals Variables"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad de parametros de presupuesto
    ''' </summary>
    Dim settingBudget As SettingsBudget

    ''' <summary>
    ''' presentador
    ''' </summary>
    Dim presenter As PSettingBudget

    ''' <summary>
    ''' registro bloquedo
    ''' </summary>
    Dim record As BlockRecordBudget

#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssingValues()
        Try
            Using model As New MSettingBudget(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveSettingBudget(settingBudget)
                AsyncLoader(False)
                settingBudget = result.ObjectEmbbeded
                If result.StateResult = True Then
                    If settingBudget.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        Me.BarraBotones.PrepareToolbar(eAction.Update)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        SearchSettingBudget()
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                Else
                    If result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        AsyncLoader(False)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        AsyncLoader(False)
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para asignar valores a la entidad
    ''' </summary>
    Private Sub AssingValues()
        With settingBudget
            .CreatePayableAccounts = CreatePayableAccounts
            .CreateReceivableAccounts = CreateReceivableAccounts
            .CreateReserves = CreateReserves
            .DocumentsGroupping = DocumentsGroupping
            .EnabledInterface = EnabledInterface
            .OperatingUnitId = BarraBotones.OperatingUnitValue
        End With

    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del funcional
    ''' </summary>
    Private Sub CleanControls()
        DeleteBlockedRecord()
        INDRgEnabledInterface.SelectedIndex = -1
        INDRgMovementAccounting.SelectedIndex = -1
        INDRgCreateReserves.SelectedIndex = -1
        INDRgCreatePayableAccounts.SelectedIndex = -1
        INDRgCreateReceivableAccounts.SelectedIndex = -1
        INDRgDocumentsGroupping.SelectedIndex = -1
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New ModelBaseBudget(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para validar controles
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If EnabledInterface Is Nothing Then
            Return False
        End If

        If CreatePayableAccounts Is Nothing Then
            Return False
        End If
        If CreateReceivableAccounts Is Nothing Then
            Return False
        End If
        If CreateReserves Is Nothing Then
            Return False
        End If
        If DocumentsGroupping Is Nothing Then
            Return False
        End If
        If EnabledInterface Is Nothing Then
            Return False
        End If
        'If idOperatingUnit = Nothing Then
        '    Return False
        'End If
        Return True
    End Function

    ''' <summary>
    ''' metodo para cargar los controles del funcional
    ''' </summary>
    Private Async Sub LoadControls()
        Using model As New ModelBaseBudget(MyTag)
            AsyncLoader(True)
            Dim result = Await model.GetBlockRecord(Me.Tag, settingBudget.Id)
            Me.BarraBotones.PrepareToolbar(eAction.Update)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            With settingBudget
                CreatePayableAccounts = .CreatePayableAccounts
                CreateReceivableAccounts = .CreateReceivableAccounts
                CreateReserves = .CreateReserves
                DocumentsGroupping = .DocumentsGroupping
                EnabledInterface = .EnabledInterface
                BarraBotones.OperatingUnitValue = .OperatingUnitId
            End With
            AsyncLoader(False)
            BarraBotones.SetDocuments(settingBudget.Id)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordBudget With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = settingBudget.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para buscar la configuracion 
    ''' </summary>
    Private Async Sub SearchSettingBudget()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        Using model As New MSettingBudget(MyTag)
            'settingBudget = Await model.GetSettingBudgetById(_idOperativeUnit)
        End Using
        If settingBudget IsNot Nothing AndAlso settingBudget.Id > 0 Then
            DeleteBlockedRecord()
            LoadControls()
            settingBudget.MarkAsModified()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
            CleanControls()
        End If
        INDRgEnabledInterface.Focus()
    End Sub
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Libera la memoria del frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        settingBudget = Nothing
        presenter = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' evento load del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSettingBudget_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.StatusRecordVisible = False
        presenter = New PSettingBudget(Me)
        idOperatingUnit = BarraBotones.OperatingUnitValue
        SearchSettingBudget()
    End Sub

    ''' <summary>
    ''' evento que se dispara al cerrar el form y elimina el regisro bloqueado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmSettingBudget_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Bar Buttons"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            idOperatingUnit = operatingUnit.Id
            SearchSettingBudget()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub
#End Region

End Class