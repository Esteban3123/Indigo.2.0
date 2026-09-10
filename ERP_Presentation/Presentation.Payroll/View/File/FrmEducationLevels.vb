'***********************************************************************
' Assembly         : Presentacion.Payroll.File
' Author           : Kevin Garay Rodriguez
' Created          : 12-04-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Clase que contiene el comportamiento de la vista en el frontal Niveles de educacion
''' </summary>
Public Class FrmEducationLevels
    Implements IEducationLevels

#Region "Variables Globales y Propiedades"


    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' The dt fields customizables
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IEducationLevels.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    Public ReadOnly Property MyTag As Object Implements IEducationLevels.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Propiedad que contiene en codigo del nivel de educacion
    ''' </summary>
    Public Property CodeEducationLevels As String Implements IEducationLevels.CodeEducationLevels
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el nombre del nivel de educacion
    ''' </summary>
    Public Property NameEducationLevels As String Implements IEducationLevels.NameEducationLevels
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IEducationLevels.StateEducationLevels
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property

    Public Property Sequence As Domain.Entities.PayrollSequence Implements IEducationLevels.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.PayrollSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequence.PayrollSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    ''' <summary>
    ''' Varaible que contiene el Nivel de educacion
    ''' </summary> 
    Dim EducationLevels As Domain.Payroll.Entities.EducationLevel
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MEducationLevels(Me.Tag)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PEducationLevels

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para saber si se inicia el formulario en modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean = False

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PayrollSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ' EducationLevels = Nothing
        'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        'INDbteCode.Text = String.Empty
        'INDtxtName.Text = String.Empty
        ' Me.BarraBotones.StatusRecordVisible = False
        '  ActionsOnControls = False
        '  Me.BarraBotones.StatusRecordVisible = False
        ' DeleteBlockedRecord()
        ' Me._doc = Nothing
        ' Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.CleanAuditBasic()


        INDlyEducationLevels.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        'Limpiar controles
        EducationLevels = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyEducationLevels.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEducationLevels.ActionsOnControls
        Set(value As Boolean)
            INDlyEducationLevels.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDlyEducationLevels.EndUpdate()
            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'INDbteCode.Enabled = False
        'StateEducationLevels = True
        'Using Model As New MEducationLevels(Me.Tag)
        '    AsyncLoader(True)
        '    EducationLevels = Await Model.GetEducationLevelsAsync(INDbteCode.Text)
        '    If Not EducationLevels Is Nothing Then
        '        If EducationLevels.Id > 0 Then
        '            Dim result = Await Model.GetBlockRecord(Me.Tag, EducationLevels.Id)
        '            With EducationLevels
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), EducationLevels.CreationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), EducationLevels.CreationDate)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), EducationLevels.ModificationUser)
        '                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(EducationLevels.ModificationDate Is Nothing, Nothing, EducationLevels.ModificationDate))
        '                CodeEducationLevels = .Code
        '                NameEducationLevels = .Name
        '                StateEducationLevels = .State
        '            End With
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '            Me.GetDocumentIndexed(Me.Tag & "_" & Me.EducationLevels.Code)
        '            If result.Id = 0 Then
        '                Me.BarraBotones.SetDocuments(EducationLevels.Id)
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = EducationLevels.Id}
        '                Dim operation = Await Model.SaveBlockRecord(record)
        '                record = operation.ObjectEmbbeded
        '            Else
        '                record = result
        '                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '            End If
        '        Else
        '            'LogicaBotonActualizar(False)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        End If
        '    Else
        '        EducationLevels = New EducationLevel
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        '    AsyncLoader(False)
        '    ActionsOnControls = True
        'End Using



        If Not String.IsNullOrEmpty(CodeEducationLevels) AndAlso Not String.IsNullOrWhiteSpace(CodeEducationLevels) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MEducationLevels(CStr(Me.Tag))
                    AsyncLoader(True)
                    EducationLevels = Await Model.GetEducationLevelsAsync(INDbteCode.Text)
                    INDlyEducationLevels.BeginUpdate()
                    If EducationLevels IsNot Nothing AndAlso EducationLevels.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(EducationLevels.Id))
                        With EducationLevels
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad
                            CodeEducationLevels = .Code
                            NameEducationLevels = .Name
                            Status = .State
                        End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.EducationLevels.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = EducationLevels.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(EducationLevels.Id, Me.Tag.ToString(), Nothing, GetType(EducationLevel).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        ' End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEducationLevels()
                        Else
                            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Me.Mensaje(EeventViewerImages.Advertencia) = "Código de Niveles de Estudio no Existe."
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyEducationLevels.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function
    ''' <summary>
    ''' Valida que los campos esten diligenciados, verifica que el codigo no este ya ingresado en la base de datos
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteCode.Text = String.Empty Then
            ValidateControls = False
            INDbteCode.Focus()
            Exit Function
        End If
        If INDtxtName.Text = String.Empty Then
            ValidateControls = False
            INDtxtName.Focus()
            Exit Function
        End If
    End Function
    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With EducationLevels
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeEducationLevels
            .Name = NameEducationLevels
        End With
    End Sub
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar el nivel de cargo en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        'If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) Then
        '    If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '        Await LoadControls()
        '        If INDbteCode.Enabled = False Then
        '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
        '        End If
        '        INDbteCode.Enabled = False
        '    End If
        'End If

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeEducationLevels.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeEducationLevels) Then
                    Await Me.NewEducationLevels()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Async Function NewEducationLevels() As Task
        EducationLevels = New Domain.Payroll.Entities.EducationLevel With {.State = True}
        If _sequence.Id > 0 Then

            If Me._sequence.IsManual Then
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail(0).Id
                ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    If Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                        Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Exit Function
                    End If
                End If
                If Me._sequence.Sequential Then
                    Me.CodeEducationLevels = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Else
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeEducationLevels = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            AsyncLoader(True)
                            Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                                Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                            End Using
                            AsyncLoader(False)
                            If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                                Me.CodeEducationLevels = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                                Me.ActionsOnControls = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        Me.CodeEducationLevels = ResourceManager.GetString("LabelOrTextboxNew")
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    End If
                End If
            End If
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = "No ha parametrizado la Secuencia Numérica"
        End If
    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        EducationLevels = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        SearchMode = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub


    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmEducationLevels_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub FrmEducationLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '****Inicializar variables*****'
        'Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        'Me.indigo = SessionValues.Instance

        'Cargamos de manera asincrona definiciones del funcional
        'PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollEducationlevels.", Me.Name, ".xml")
        'LoadhronousDefinitions = New BackgroundWorker
        'If LoadhronousDefinitions.IsBusy = False Then
        '    LoadhronousDefinitions.RunWorkerAsync()
        'End If
        ' Presenter = New PEducationLevels(Me)
        'LoadStatus()
        'Deshacer()


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PEducationLevels(Me)
        Presenter.GetSequence()

        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollEducationlevels.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MEducationLevels(Me.Tag)
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.EducationLevels IsNot Nothing AndAlso Me.EducationLevels.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Niveles de educacion.
    ''' </summary>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEducationLevels()
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Niveles de educacion.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Niveles de educacion.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'If EducationLevels IsNot Nothing Then
        '    If EducationLevels.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            AsyncLoader(True)
        '            Using Model As New MEducationLevels(Me.Tag)
        '                Dim result As ActionMessageResult(Of EducationLevel)
        '                result = Await Model.DeleteEducationLevelsAsync(EducationLevels)
        '                If result.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                    AsyncLoader(False)
        '                    SearchMode = False
        '                    Deshacer()
        '                Else
        '                    AsyncLoader(False)
        '                    If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
        '                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                    End If
        '                End If
        '            End Using
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        'End If


        If Me.EducationLevels IsNot Nothing AndAlso Me.EducationLevels.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEducationLevels(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEducationLevelsAsync(Me.EducationLevels)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de Niveles de educacion.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        '    Exit Sub
        'End If
        'AsyncLoader(True)
        'AssigningValues()
        'Using Model As New MEducationLevels(Me.Tag)
        '    If Await Model.SaveEducationLevelsAsync(EducationLevels) = True Then
        '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '        If EducationLevels.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
        '        ElseIf EducationLevels.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '        End If
        '        AsyncLoader(False)
        '        SearchMode = False
        '        Deshacer()
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '        AsyncLoader(False)
        '    End If
        'End Using


        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MEducationLevels(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of EducationLevel) = Await Model.SaveEducationLevelsAsync(Me.EducationLevels, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If EducationLevels.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.EducationLevels = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub



    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.EducationLevels.Code) Then
            Try
                Using model As New MEducationLevels(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not EducationLevels.State
                    Dim result As ActionResult(Of EducationLevel) = Await model.ChangeState(Me.EducationLevels.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.EducationLevels = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.EducationLevels
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Niveles de educacion.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        'If Not SearchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'End If
    End Sub
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)

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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub


    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmEducationLevelsMetaData, Eform.InfoMetaData), Me.EducationLevels.Code, Me.EducationLevels.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.EducationLevels.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmEducationLevelsMetaDataTitle, Eform.InfoMetaData), Me.EducationLevels.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEducationLevelsMetaData, Eform.InfoMetaData), Me.EducationLevels.Code, Me.EducationLevels.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEducationLevelsMetaDataTitle, Eform.InfoMetaData), Me.EducationLevels.Code)
            Return Me._doc
        End If
    End Function
#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de Niveles de educacion.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PayrollSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyEducationLevels.ShowCustomizationForm()
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
            INDlyEducationLevels.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyEducationLevels_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyEducationLevels.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MEducationLevels(Me.Tag)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyEducationLevels.Items.Count - 1
                        INDlyEducationLevels.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyEducationLevels.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyEducationLevels.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyEducationLevels.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyEducationLevels.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyEducationLevels.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyEducationLevels.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyEducationLevels.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

End Class