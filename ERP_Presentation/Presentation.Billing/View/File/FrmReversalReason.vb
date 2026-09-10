Imports Domain.Entities
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform

Public Class FrmReversalReason
    Implements IReversalReason, ICustomizableForm

#Region "Properties"
    Private Const NAME_MODULE As String = "Billing"
    ''' <summary>
    ''' Código de la razón
    ''' </summary>
    Public Property Code As String Implements IReversalReason.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Gets or sets the name1.
    ''' </summary>
    Public Property NameReason As String Implements IReversalReason.Name
        Get
            Return INDteName.Text
        End Get
        Set(value As String)
            INDteName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción de la razón
    ''' </summary>
    Public Property Description As String Implements IReversalReason.Description
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Estado de la razón
    ''' </summary>
    Public Property Status As Boolean Implements IReversalReason.Status
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

    ''' <summary>
    ''' Layout principal para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IReversalReason.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IReversalReason.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia de facturación
    ''' </summary>
    Public Property Sequence As BillingSequence Implements IReversalReason.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Nombre del módulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Billing"

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad de razones de anulacion
    ''' </summary>
    Private _reversalReason As BillingReversalReason

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private presenter As PReversalReason

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

#End Region

#Region "Handlers"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _reversalReason = Nothing
        _searchMode = Nothing
        presenter = Nothing
        _record = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmReversalReason control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmReversalReason_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        ' Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        ' Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        'Me.indigo = SessionValues.Instance
        '_presenter = New PReversalReason(Me)
        '_presenter.GetSequence()
        '_presenter.LoadDefinitionLayout()

        'If FormSearchObjects Is Nothing Then
        '    _searchMode = False
        'End If

        'LoadStatus()
        ' Deshacer()


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        presenter = New PReversalReason(Me)
        presenter.GetSequence()
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewReversalReason()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        'If Me._reversalReason IsNot Nothing AndAlso Me._reversalReason.Id > 0 Then
        '    If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
        '        DeleteBlockedRecord()
        '        Me.INDbteCode.Text = Me.IdEntity.Trim()
        '        Me.LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Me.INDbteCode.Text = Me.IdEntity.Trim()
        '    Me.LoadControls()
        '    If FormSearchObjects IsNot Nothing Then
        '        FormSearchObjects.Close()
        '    End If
        'End If
        'Me.IdEntity = String.Empty



        Me.ViewModeEditHold = True
        If Me._reversalReason IsNot Nothing AndAlso Me._reversalReason.Id > 0 Then
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

    ''' <summary>
    ''' Evento cerrar del formulario que elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCash_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' News the reversal reason.
    ''' </summary>
    Private Async Function NewReversalReason() As Task
        'If Sequense IsNot Nothing AndAlso Sequense.Id > 0 Then
        '    Me._reversalReason = New BillingReversalReason()
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        Dim res = (From ou As BillingSequenceDetail In Me._sequence.BillingSequenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
        '        If res IsNot Nothing AndAlso res.Count > 0 Then
        '            Me._idCurrentSequence = res(0).Id
        '        Else
        '            Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
        '                Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(Me.Tag)
        '                    Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
        '                End Using
        '                If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
        '                    Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        'End If



        _reversalReason = New BillingReversalReason() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
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
    End Function

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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IReversalReason.ActionsOnControls
        Set(value As Boolean)
            INDlcgRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDteName.Enabled = value
            INDmeDescription.Enabled = value
            INDlcgRoot.EndUpdate()
            If value Then
                INDteName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Function LoadControls() As Task Implements IReversalReason.LoadControls
        'If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
        '    If Me.BarraBotones.PermiteConsultar = False Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '        Exit Function
        '    End If

        '    Me.BarraBotones.StatusRecordVisible = True
        '    AsyncLoader(True)
        '    Using Model As New MReversalReason(Me.Tag)
        '        Dim resultOperation = Await Model.GetReversalReasonAsync(Me.Code, True)
        '        AsyncLoader(False)
        '        _reversalReason = resultOperation.ObjectEmbbeded
        '        If Not _reversalReason Is Nothing AndAlso _reversalReason.Id > 0 Then
        '            Using modelBlock As New MBlockRecordAndSequense(Me.Tag)
        '                Dim result = Await modelBlock.GetBlockRecord(Me.Tag, _reversalReason.Id)
        '                With _reversalReason
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
        '                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Code = .Code
        '                    NameReason = .Name
        '                    Description = .Description
        '                    Status = .Status
        '                End With
        '                Me.GetDocumentIndexed(Me.Tag & "_" & Me._reversalReason.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    _record = New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _reversalReason.Id}
        '                    Dim operation = Await modelBlock.SaveBlockRecord(_record)
        '                    _record = operation.ObjectEmbbeded
        '                Else
        '                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '                    _record = result
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                End If
        '                Me.BarraBotones.SetDocuments(_reversalReason.Id, Me.Tag.ToString(), Nothing, GetType(BillingReversalReason).Name)
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                ActionsOnControls = True
        '                INDbteCode.Enabled = False
        '            End Using
        '        Else
        '            If Me._sequence.IsManual Then
        '                Me.NewReversalReason()
        '                If Sequense IsNot Nothing AndAlso Sequense.Id > 0 Then
        '                    ActionsOnControls = True
        '                    INDbteCode.Enabled = False
        '                End If
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
        '                Me.Code = String.Empty
        '                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        '                INDbteCode.Focus()
        '            End If
        '        End If
        '    End Using
        'End If



        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MReversalReason(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetReversalReasonAsync(Me.Code, True)
                    _reversalReason = resultOperation.ObjectEmbbeded
                    INDlcgRoot.BeginUpdate()
                    If _reversalReason IsNot Nothing AndAlso _reversalReason.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_reversalReason.Id))
                            With _reversalReason
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                NameReason = .Name
                                Description = .Description
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._reversalReason.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _reversalReason.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(_reversalReason.Id, Me.Tag.ToString(), Nothing, GetType(BillingReversalReason).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewReversalReason()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlcgRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IReversalReason.AssigningValues
        With _reversalReason
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameReason
            .Description = Description
        End With
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._reversalReason.Code, Me._reversalReason.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & Me.Tag & "_" & Me._reversalReason.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._reversalReason.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._reversalReason.Code, Me._reversalReason.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._reversalReason.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IReversalReason.CleanControls
        'INDlcgRoot.BeginUpdate()
        'ActionsOnControls = False
        'Code = String.Empty
        'NameReason = String.Empty
        'Description = String.Empty
        'INDlcgRoot.EndUpdate()

        '_reversalReason = Nothing
        'Me.BarraBotones.StatusRecordVisible = False
        ' Me.BarraBotones.StatusRecord = Nothing
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        ' Me.BarraBotones.CleanAuditBasic()



        INDlcgRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        Code = String.Empty
        NameReason = String.Empty
        Description = String.Empty
        'Limpiar controles
        _reversalReason = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlcgRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Crud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        'If Not _searchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        'Try
        '    AsyncLoader(True)
        '    If Me._reversalReason IsNot Nothing AndAlso Me._reversalReason.Id > 0 Then
        '        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Using Model As New MReversalReason(Me.Tag.ToString())
        '                _reversalReason.MarkAsDeleted()
        '                Dim result = Await Model.DeleteReversalReasonAsync(Me._reversalReason)
        '                If result.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    AsyncLoader(False)
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                    _searchMode = False
        '                    Me.Deshacer()
        '                Else
        '                    AsyncLoader(False)
        '                    If result.Message IsNot Nothing Then
        '                        GenerateListError(result.Message)
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                    End If
        '                End If
        '            End Using
        '        End If
        '    End If
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try



        If Me._reversalReason IsNot Nothing AndAlso Me._reversalReason.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MReversalReason(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteReversalReasonAsync(Me._reversalReason)
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
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        'If Not String.IsNullOrEmpty(Me._reversalReason.Code) Then
        '    Try
        '        AsyncLoader(True)
        '        Using model As New MReversalReason(Me.Tag)
        '            Dim state As Boolean
        '            Select Case Status
        '                Case eActionsStatusRecords.Active
        '                    state = True
        '                Case eActionsStatusRecords.Inactive
        '                    state = False
        '            End Select
        '            Dim Result = Await model.UpdateStateReversalReasonAsync(Me._reversalReason.Code, state)
        '            AsyncLoader(False)
        '            If Result.StateResult = True Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '                Me._reversalReason = Result.ObjectEmbbeded
        '                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            Else
        '                If Result.MessageResult(0) = ErrorConcurrencia Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                End If
        '            End If
        '        End Using
        '    Catch ex As Exception
        '        Throw ex
        '        AsyncLoader(False)
        '    End Try
        'Else
        '    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        'End If
        If Not String.IsNullOrEmpty(Me._reversalReason.Code) Then
            Try
                Using model As New MReversalReason(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not _reversalReason.Status
                    Dim result As ActionResult(Of BillingReversalReason) = Await model.UpdateStateReversalReasonAsync(Me._reversalReason.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._reversalReason = result.ObjectEmbbeded
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
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        'Try
        '    AsyncLoader(True)
        '    If ValidateControls() = False Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
        '        Exit Sub
        '    End If
        '    AssigningValues()
        '    Using Model As New MReversalReason(Me.Tag.ToString())
        '        Dim Result = Await Model.SaveReversalReasonAsync(Me._reversalReason, Me._idCurrentSequence)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            If _reversalReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                If Not Me._sequence.Sequential Then
        '                    Me.DicSequense(Me._sequence.BillingSequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '                If Me._sequence.Sequential Then
        '                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
        '                Else
        '                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
        '                End If
        '            ElseIf _reversalReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            Me._reversalReason = Result.ObjectEmbbeded
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            _searchMode = False
        '            Me.Deshacer()
        '        Else
        '            If Result.Message IsNot Nothing Then
        '                GenerateListError(Result.Message)
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try




        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MReversalReason(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of BillingReversalReason) = Await Model.SaveReversalReasonAsync(Me._reversalReason, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _reversalReason.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._reversalReason = result.ObjectEmbbeded
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

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub GenerateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewReversalReason()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListReversalReason
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
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
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        'If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using ModelCommonTreasury As New MBlockRecordAndSequense(Me.Tag)
        '        Await ModelCommonTreasury.DeleteBlockRecord(_record)
        '        _record = Nothing
        '    End Using
        'End If

        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub
#End Region

#Region "Bar Button Events"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class