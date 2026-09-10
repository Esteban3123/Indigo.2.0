#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports DevExpress.XtraEditors
#End Region

Public Class FrmElectronicPayrollConcepts
    Implements IElectronicPayrollConcepts

#Region "Globals & Properties"

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PElectronicPayrollConcepts

    ''' <summary>
    ''' Varaible que contiene el conceptos de nómina electrónica
    ''' </summary> 
    Dim ElectronicPayrollConcepts As Domain.Payroll.Entities.ElectronicPayrollConcepts

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MElectronicPayrollConcepts(MyBase.Tag)
    ''' <summary>
    ''' Variable para saber si se inicia el formulario en modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean = False

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PayrollSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    Private _listConceptType As List(Of Tuple(Of Byte, String))

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IElectronicPayrollConcepts.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que contiene en codigo del conceptos de nómina electrónica
    ''' </summary>
    Public Property Code As String Implements IElectronicPayrollConcepts.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre del conceptos de nómina electrónica
    ''' </summary>
    Private Property NameElectronicPayrollConcept As String Implements IElectronicPayrollConcepts.Name
        Get
            Return INDTeName.Text
        End Get
        Set(value As String)
            INDTeName.Text = value
        End Set
    End Property



    ''' <summary>
    ''' Propiedad que contiene en tipo concepto del conceptos de nómina electrónica
    ''' </summary>
    Public Property ConceptType As Integer Implements IElectronicPayrollConcepts.ConceptType
        Get
            Return INDsleConceptType.EditValue
        End Get
        Set(value As Integer)
            INDsleConceptType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el estado del conceptos de nómina electrónica
    ''' </summary>
    Public Property Status As Boolean Implements IElectronicPayrollConcepts.StateElectronicPayrollConcept
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

    Public Property Sequence As Domain.Entities.PayrollSequence Implements IElectronicPayrollConcepts.Sequence
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
    ''' Obtiene o establece la Tupla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ListConceptType As List(Of Tuple(Of Byte, String))
        Get
            If _listConceptType Is Nothing Then
                _listConceptType = New List(Of Tuple(Of Byte, String))
                _listConceptType.Add(New Tuple(Of Byte, String)(1, "Devengados"))
                _listConceptType.Add(New Tuple(Of Byte, String)(2, "Deducciones"))
                _listConceptType.Add(New Tuple(Of Byte, String)(3, "No Aplica"))
            End If
            Return _listConceptType
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IElectronicPayrollConcepts.MyTag
        Get
            Return Me.Tag
        End Get
    End Property


#End Region

#Region "Methods and Event"

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MElectronicPayrollConcepts(Me.Tag)
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With ElectronicPayrollConcepts
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Me.Code
            .Name = Me.NameElectronicPayrollConcept
            .ConceptType = Me.ConceptType
            .State = Status
        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDLycRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDBtnCode.Text = String.Empty
        INDTeName.Text = String.Empty
        INDsleConceptType.Text = String.Empty
        'Limpiar controles
        ElectronicPayrollConcepts = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        INDLycRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IElectronicPayrollConcepts.ActionsOnControls
        Set(value As Boolean)
            INDLycRoot.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDsleConceptType.Enabled = value
            INDLycRoot.EndUpdate()
            If value = True Then
                INDTeName.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Evento para consultar el nivel de cargo en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                Await Me.LoadControls()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If

    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MElectronicPayrollConcepts(CStr(Me.Tag))
                    AsyncLoader(True)
                    ElectronicPayrollConcepts = Await Model.GetElectronicPayrollConceptsByCode(INDBtnCode.Text)
                    INDLycRoot.BeginUpdate()
                    If ElectronicPayrollConcepts IsNot Nothing AndAlso ElectronicPayrollConcepts.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(ElectronicPayrollConcepts.Id))
                        With ElectronicPayrollConcepts
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            'Llenar Entidad
                            Code = .Code
                            NameElectronicPayrollConcept = .Name
                            ConceptType = .ConceptType
                            Status = .State
                        End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ElectronicPayrollConcepts.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ElectronicPayrollConcepts.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(ElectronicPayrollConcepts.Id, Me.Tag.ToString(), Nothing, GetType(ElectronicPayrollConcepts).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        ' End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewElectronicPayrollConcepts()
                        Else
                            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Me.Mensaje(EeventViewerImages.Advertencia) = "Código de Niveles de Estudio no Existe."
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados, verifica que el codigo no este ya ingresado en la base de datos
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBtnCode.Text = String.Empty Then
            ValidateControls = False
            INDBtnCode.Focus()
            Exit Function
        End If
        If INDTeName.Text = String.Empty Then
            ValidateControls = False
            INDTeName.Focus()
            Exit Function
        End If
        If INDsleConceptType.Text = String.Empty Then
            ValidateControls = False
            INDsleConceptType.Focus()
            Exit Function
        End If
    End Function

    Private Async Function NewElectronicPayrollConcepts() As Task
        ElectronicPayrollConcepts = New Domain.Payroll.Entities.ElectronicPayrollConcepts With {.State = True}
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
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Else
                    If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                        If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
                                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                                Me.ActionsOnControls = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            End If
                        End If
                    Else
                        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
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
        ElectronicPayrollConcepts = Nothing
        Model = Nothing
        Presenter = Nothing
        SearchMode = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub

    Private Sub FrmElectronicPayrollConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '*****Inicializa la Tupla*****'
        INDsleConceptType.Properties.DataSource = ListConceptType
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PElectronicPayrollConcepts(Me)
        Presenter.GetSequence()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.ElectronicPayrollConcepts IsNot Nothing AndAlso Me.ElectronicPayrollConcepts.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Crud"

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.ElectronicPayrollConcepts.Code) Then
            Try
                Using model As New MElectronicPayrollConcepts(Me.Tag)
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of ElectronicPayrollConcepts) = Await model.ChangeState(Me.ElectronicPayrollConcepts.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.ElectronicPayrollConcepts = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDBtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de conceptos de nómina electrónica.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewElectronicPayrollConcepts()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Buscar del control de conceptos de nómina electrónica.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de conceptos de nómina electrónica.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de conceptos de nómina electrónica.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MElectronicPayrollConcepts(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ElectronicPayrollConcepts) = Await Model.SaveElectronicPayrollConcepts(Me.ElectronicPayrollConcepts, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If ElectronicPayrollConcepts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.ElectronicPayrollConcepts = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de conceptos de nómina electrónica.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If ElectronicPayrollConcepts IsNot Nothing Then
            If ElectronicPayrollConcepts.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Using model As New MElectronicPayrollConcepts(Me.Tag)
                        Dim result = Await model.DeleteElectronicPayrollConcepts(Me.ElectronicPayrollConcepts)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            AsyncLoader(False)
                            SearchMode = False
                            Deshacer()
                        Else
                            AsyncLoader(False)
                            If result.MessageResult.ElementAt(0).AsString = "c-0000" Then
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End If
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Tipo de concepto", .FieldName = "ConceptTypeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StateName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ElectronicPayrollConcepts
            .FormParent = Me
            .ShowSearch()
        End With
        ' _searchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBtnCode.Text = ReturnValue
        If INDBtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub


#End Region

#Region "bar buttons and events"

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmElectronicPayrollConcepts_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

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
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
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
        INDLycRoot.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDLycRoot.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region


End Class