'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 27-10-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Maintenance.MVP
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region


Public Class FrmMaintenanceCostCenter
    Implements ICostCenter, ICustomizableForm

#Region "Variables, propiedades y load"

    Private Const NAME_MODULE As String = "Maintenance"
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMaintenance

    Private _idCurrentSequence As Long

    Private _idOperativeUnit As Integer

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PCostCenter
    ''' <summary>
    ''' Variable donde se almacena el centro de costo
    ''' </summary>
    Dim costCenter As Domain.Maintenance.Entities.CostCenter

    ''' <summary>
    ''' Variable para poder acceder al modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As New MCostCenter(Me.Tag)

    ''' <summary>
    ''' Metodo para definir la accion de los controles en el formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostCenter.ActionsOnControls
        Set(value As Boolean)
            INDBteCode.Enabled = Not value
            INDNameCostCenter.Enabled = value

            If value Then
                INDNameCostCenter.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Metodo que define la lógica si se actualiza o se guarda nuevo registro
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Boton Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        'CleanControls()
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Propiedad que contiene el código del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeCostCenter As String Implements ICostCenter.CodeCostCenter
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements ICostCenter.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Private _sequence As Domain.Entities.MaintenanceSequence
    Public Property Sequence As Domain.Entities.MaintenanceSequence Implements ICostCenter.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameCostCenter As String Implements ICostCenter.NameCostCenter
        Get
            Return INDNameCostCenter.Text
        End Get
        Set(value As String)
            INDNameCostCenter.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el estado del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatusCostCenter As Boolean Implements ICostCenter.StatusCostCenter
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Presenter = Nothing
        costCenter = Nothing
        Model = Nothing
    End Sub

    Private Sub FrmCostCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollCostCenter.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PCostCenter(Me)
        Presenter.GetSequence()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenseMaintenance(Me.MyTag.ToString())
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.costCenter IsNot Nothing AndAlso Me.costCenter.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), Botones.SiNo, MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes)) = Windows.Forms.DialogResult.Yes Then
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
    End Sub


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDBteCode.Text = String.Empty
        INDNameCostCenter.Text = String.Empty
        Me.BarraBotones.StatusRecordVisible = False
        costCenter = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Load del barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(Me.Tag))
    End Sub

    ''' <summary>
    ''' Boton buscar del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Boton Deshacer del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' borra el registro bloqueado antes de cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCostCenter_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento key down para capturar enter en el codigo y buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBECodigo_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        'If e.KeyCode = Windows.Forms.Keys.Enter Then
        '    If INDBteCode.Text <> String.Empty Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
        '        Await LoadControls()
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontCode")
        '        INDBteCode.Focus()
        '    End If
        'End If

        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeCostCenter.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeCostCenter) Then
                    Await Me.NewCostCenter()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario de unidades funcionales en un pop up
    ''' </summary>
    Private Sub INDGleFunctionalUnit_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            'Using Formulario As New FrmFunctionalUnit
            '    Formulario.ShowDialog()
            'End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeCostCenter) AndAlso Not String.IsNullOrWhiteSpace(CodeCostCenter) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Me.BarraBotones.StatusRecordVisible = True
                StatusCostCenter = True
                Using Model As New MCostCenter(CStr(Me.Tag))
                    AsyncLoader(True)
                    costCenter = Await Model.GetCostCenterAsync(INDBteCode.Text.Trim)
                    INDLyCostCenter.BeginUpdate()
                    If costCenter IsNot Nothing AndAlso costCenter.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(costCenter.Id))
                            With costCenter
                                'LogicaBotonActualizar(True)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), costCenter.CreadionUserId)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), costCenter.CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), costCenter.ModificationUserId)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(costCenter.ModificationDate Is Nothing, Nothing, costCenter.ModificationDate))
                                CodeCostCenter = .Code
                                NameCostCenter = .Name
                                'FunctionalUnitId = .FunctionalUnitId
                                StatusCostCenter = .State
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.costCenter.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = costCenter.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(costCenter.Id, Me.Tag.ToString(), Nothing, GetType(Domain.Maintenance.Entities.CostCenter).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCostCenter()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = "El registro no existe"
                            CodeCostCenter = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLyCostCenter.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If


        'Me.BarraBotones.StatusRecordVisible = True
        'StatusCostCenter = True
        'AsyncLoader(True)
        'Using Model As New MCostCenter(Me.Tag)
        '    costCenter = Await Model.GetCostCenterAsync(INDBteCode.Text)
        'End Using
        'AsyncLoader(False)
        'ActionsOnControls = True
        'If Not costCenter Is Nothing Then
        '    If costCenter.Id > 0 Then
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, costCenter.Id)
        '        With costCenter
        '            'LogicaBotonActualizar(True)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), costCenter.CreadionUserId)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), costCenter.CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), costCenter.ModificationUserId)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(costCenter.ModificationDate Is Nothing, Nothing, costCenter.ModificationDate))
        '            CodeCostCenter = .Code
        '            NameCostCenter = .Name
        '            'FunctionalUnitId = .FunctionalUnitId
        '            StatusCostCenter = .State
        '        End With
        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.costCenter.Code)
        '        If result.Id = 0 Then
        '            Me.BarraBotones.SetDocuments(costCenter.Id)
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = costCenter.Id}
        '            Dim operation = Await Model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            record = result
        '            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    Else
        '        'LogicaBotonActualizar(False) 
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        'Else
        '    costCenter = New Domain.Maintenance.Entities.CostCenter()
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'INDNameCostCenter.Focus()
    End Function

    ''' <summary>
    ''' Reestablecer layout principal de customizacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCode.Text = String.Empty Then
            INDBteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDNameCostCenter.Text = String.Empty Then
            INDNameCostCenter.Focus()
            ValidateControls = False
            Exit Function
        End If
        Return ValidateControls
    End Function

    ''' <summary>
    ''' Metodo para asignara valores en la entidad principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With costCenter
            .Code = INDBteCode.Text
            .Name = INDNameCostCenter.Text
            Select Case StatusCostCenter
                Case CBool(eActionsStatusRecords.Active)
                    .State = True
                Case CBool(eActionsStatusRecords.Inactive)
                    .State = False
            End Select
        End With
    End Sub

    Private Async Function NewCostCenter() As Task
        costCenter = New Domain.Maintenance.Entities.CostCenter()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeCostCenter = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeCostCenter = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeCostCenter = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeCostCenter = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function
#End Region

#Region "Methods Icrud"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.costCenter.Code) Then
            Try
                Using model As New MCostCenter(Me.Tag)
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of Domain.Maintenance.Entities.CostCenter) = Await model.ChangeState(Me.costCenter.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.costCenter = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
        'If Not String.IsNullOrEmpty(CodeCostCenter) Then
        '    Dim state As Boolean
        '    Select Case StatusCostCenter
        '        Case CBool(eActionsStatusRecords.Active)
        '            state = True
        '        Case CBool(eActionsStatusRecords.Inactive)
        '            state = False
        '    End Select
        '    Using model As New MCostCenter(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await model.ChangeState(CodeCostCenter, state)
        '        AsyncLoader(False)
        '        If Result = True Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '        End If
        '    End Using
        'Else
        '    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        'End If
    End Function

    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Boton Actualizar del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Boton Nuevo del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Boton Nuevo del barra botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Boton del button edit de codigo para buscar en el frm busqueda con XPO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Metodo para Abrir el Frm de busqueda que se aplica con xpo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostCenterMaintenance
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDBteCode.Text = ReturnValue
        DeleteBlockedRecord()
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo que deshace el ultimol proceso realizado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Metodo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me.costCenter IsNot Nothing AndAlso Me.costCenter.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MCostCenter(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteCostCenterAsync(Me.costCenter)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
        'Dim actionResult As ActionMessageResult(Of Domain.Maintenance.Entities.CostCenter)
        'If costCenter IsNot Nothing Then
        '    If costCenter.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
        '            Try
        '                Using Model As New MCostCenter(Me.Tag)
        '                    AsyncLoader(True)
        '                    actionResult = Await Model.DeleteCostCenterAsync(costCenter)
        '                    If actionResult.StateResult = True Then
        '                        Me.DeleteDocumentIndexed()
        '                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                        AsyncLoader(False)
        '                        Deshacer()
        '                    Else
        '                        AsyncLoader(False)
        '                        INDBteCode.Enabled = False
        '                        For Each action As MessageResult In actionResult.MessageResult
        '                            If action.CodeMessage = "c-0000" Then
        '                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
        '                                AsyncLoader(False)
        '                            Else
        '                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                            End If
        '                        Next
        '                    End If
        '                End Using
        '            Catch ex As Exception
        '                AsyncLoader(False)
        '                INDBteCode.Enabled = False
        '                Throw ex
        '            End Try
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        'End If
    End Sub

    ''' <summary>
    ''' Metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCostCenter(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of Domain.Maintenance.Entities.CostCenter) = Await Model.SaveCostCenterAsync(Me.costCenter, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.costCenter = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        '    Exit Sub
        'End If
        'AsyncLoader(True)
        'Try
        '    AssigningValues()
        '    Using Model As New MCostCenter(Me.Tag)
        '        If Await Model.SaveCostCenterAsync(costCenter) = True Then
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            If costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
        '            ElseIf costCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '            End If
        '            AsyncLoader(False)
        '        Else
        '            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '        End If
        '    End Using
        '    Deshacer()
        'Catch ex As Exception

        'End Try
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaData, Eform.InfoMetaData), Me.costCenter.Code, Me.costCenter.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.costCenter.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaDataTitle, Eform.InfoMetaData), Me.costCenter.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaData, Eform.InfoMetaData), Me.costCenter.Code, Me.costCenter.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmCostCenterMetaDataTitle, Eform.InfoMetaData), Me.costCenter.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region

#Region "Customize"
    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDLyCostCenter.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDLyCostCenter.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDLyCostCenter_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyCostCenter.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MCostCenter(Me.Tag)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDLyCostCenter.Items.Count - 1
                        INDLyCostCenter.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDLyCostCenter.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDLyCostCenter.Items.Item(j).Tag.ToString.Trim Then
                                    INDLyCostCenter.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDLyCostCenter.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDLyCostCenter.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDLyCostCenter.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLyCostCenter.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub
End Class