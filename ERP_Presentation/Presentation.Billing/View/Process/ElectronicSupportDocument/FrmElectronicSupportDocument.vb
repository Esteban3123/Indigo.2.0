Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing.MVP
Imports Presentation.Controls

Public Class FrmElectronicSupportDocument
    Implements IElectronicSupportDocument, ICustomizableForm

#Region "Properties"
    ''' <summary>
    ''' Código de la razón
    ''' </summary>
    Public Property Code As String Implements IElectronicSupportDocument.Code
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
    ''' Gets or sets the supplier
    ''' </summary>
    Public Property SupplierThirdPartyId As Integer? Implements IElectronicSupportDocument.SupplierThirdPartyId
        Get
            Return INDSleSupplierThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDSleSupplierThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' fecha del documento
    ''' </summary>
    Public Property DocumentDate As DateTime Implements IElectronicSupportDocument.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As DateTime)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de radicacion
    ''' </summary>
    Public Property RadicationDate As DateTime Implements IElectronicSupportDocument.RadicationDate
        Get
            Return INDdtRadicationDate.EditValue
        End Get
        Set(value As DateTime)
            INDdtRadicationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de radicacion
    ''' </summary>
    Public Property DueDate As DateTime Implements IElectronicSupportDocument.DueDate
        Get
            Return INDdtDueDate.EditValue
        End Get
        Set(value As DateTime)
            INDdtDueDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción del documento soporte
    ''' </summary>
    Public Property Description As String Implements IElectronicSupportDocument.Description
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
    Public Property Status As Boolean Implements IElectronicSupportDocument.Status
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
    ''' sub total
    ''' </summary>
    ''' <returns></returns>
    Public Property SubTotalValue As Decimal Implements IElectronicSupportDocument.SubTotal
        Get
            Return IIf(INDTeSubTotalValue.EditValue Is Nothing, 0, INDTeSubTotalValue.EditValue)
        End Get
        Set(value As Decimal)
            INDTeSubTotalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' IVA
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxValue As Decimal Implements IElectronicSupportDocument.TaxValue
        Get
            Return IIf(INDTeTaxValue.EditValue Is Nothing, 0, INDTeTaxValue.EditValue)
        End Get
        Set(value As Decimal)
            INDTeTaxValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' total
    ''' </summary>
    ''' <returns></returns>
    Public Property TotalValue As Decimal Implements IElectronicSupportDocument.TotalValue
        Get
            Return IIf(INDTeTotal.EditValue Is Nothing, 0, INDTeTotal.EditValue)
        End Get
        Set(value As Decimal)
            INDTeTotal.EditValue = value
        End Set
    End Property

    Public Property BillingAuthorizationId As Integer? Implements IElectronicSupportDocument.BillingAuthorizationId
        Get
            Return INDSleBillingAuthorization.EditValue
        End Get
        Set(value As Integer?)
            INDSleBillingAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Layout principal para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IElectronicSupportDocument.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IElectronicSupportDocument.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Secuencia de facturación
    ''' </summary>
    Public Property Sequence As BillingSequence Implements IElectronicSupportDocument.Sequense
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
    Private _sequence As BillingSequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad de Documento Soporte
    ''' </summary>
    Private _ElectronicSupportDocument As ElectronicSupportDocument

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private presenter As PElectronicSupportDocument

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
        _ElectronicSupportDocument = Nothing
        _searchMode = Nothing
        presenter = Nothing
        _record = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmElectronicSupportDocument control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmElectronicSupportDocument_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        presenter = New PElectronicSupportDocument(Me)
        presenter.GetSequence()
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
                    Await Me.NewElectronicSupportDocument()
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
        Me.ViewModeEditHold = True
        If Me._ElectronicSupportDocument IsNot Nothing AndAlso Me._ElectronicSupportDocument.Id > 0 Then
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
    Private Async Function NewElectronicSupportDocument() As Task
        _ElectronicSupportDocument = New ElectronicSupportDocument() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

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

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlcgRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDSleSupplierThirdParty.Enabled = value
            INDmeDescription.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDdtRadicationDate.Enabled = value
            INDdtDueDate.Enabled = value
            INDTeTaxValue.Enabled = False
            INDTeSubTotalValue.Enabled = value
            INDTeTotal.Enabled = value
            INDSleBillingAuthorization.Enabled = value
            INDlcgRoot.EndUpdate()
            If value Then
                INDSleSupplierThirdParty.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MElectronicSupportDocument(CStr(Me.Tag))
                    AsyncLoader(True)

                    Dim resultOperation = Await Model.GetElectronicSupportDocumentByCode(Me.Code)
                    _ElectronicSupportDocument = resultOperation.ObjectEmbbeded
                    INDlcgRoot.BeginUpdate()
                    If resultOperation.StateResult = True AndAlso _ElectronicSupportDocument IsNot Nothing AndAlso _ElectronicSupportDocument.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_ElectronicSupportDocument.Id))
                            With _ElectronicSupportDocument
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                Me.SupplierThirdPartyId = .SupplierThirdPartyId
                                Me.INDSleSupplierThirdParty.Properties.NullText = .SupplierCodeName
                                Me.INDSleBillingAuthorization.Properties.NullText = .BillingAuthorizationCodeName
                                Me.BillingAuthorizationId = .BillingAuthorizationId
                                Me.DocumentDate = .DocumentDate
                                Me.RadicationDate = .RadicationDate
                                Me.DueDate = .DueDate
                                Me.Description = .Description
                                Me.SubTotalValue = .SubTotalValue
                                Me.TaxValue = .TaxValue
                                Me.TotalValue = .TotalValue
                                Me.Status = .Status
                                Me.BarraBotones.OperatingUnit.Id = .OperativeUnitId
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._ElectronicSupportDocument.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _ElectronicSupportDocument.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            AsyncLoader(False)

                            If Not Me.Status Then
                                ActionsOnControls = True
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                            Else
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
                                INDbteCode.Enabled = True
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewElectronicSupportDocument()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = resultOperation?.Message
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
    Public Sub AssigningValues(Optional ToConfirm As Boolean = False)
        With _ElectronicSupportDocument
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .SupplierThirdPartyId = SupplierThirdPartyId
            .DocumentDate = DocumentDate
            .RadicationDate = RadicationDate
            .DueDate = DueDate
            .BillingAuthorizationId = BillingAuthorizationId
            .SubTotalValue = SubTotalValue
            .TaxValue = TaxValue
            .TotalValue = TotalValue
            .Description = Description
            .IndigoCompanyNit = indigo.IndigoCompanyNit
            .Status = False
            .ToConfirm = ToConfirm
            .OperativeUnitId = BarraBotones.OperatingUnit.Id
        End With
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub


    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        INDlcgRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = False
        Code = String.Empty
        INDSleSupplierThirdParty.Properties.NullText = String.Empty
        Me.DocumentDate = GetDateServer()
        Me.RadicationDate = Me.DocumentDate
        Me.DueDate = Me.DocumentDate
        Me.SupplierThirdPartyId = Nothing
        Me.Description = String.Empty
        Me.SubTotalValue = 0
        Me.TaxValue = 0
        Me.TotalValue = 0
        Me.BillingAuthorizationId = Nothing
        Me.INDSleBillingAuthorization.Properties.NullText = String.Empty

        'Limpiar controles
        _ElectronicSupportDocument = Nothing

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
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub SaveElectronicSupportDocument(Optional ToConfirm As Boolean = False)
        If Not ValidateControls() Then
            Exit Sub
        End If

        If BarraBotones.OperatingUnit Is Nothing OrElse BarraBotones.OperatingUnit.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
            Exit Sub
        End If

        AssigningValues(ToConfirm)
        Try
            Using Model As New MElectronicSupportDocument(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult = Await Model.SaveElectronicSupportDocument(Me._ElectronicSupportDocument, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewElectronicSupportDocument()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Proveedor", .FieldName = "SupplierThirdParty.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Nit Tercero", .FieldName = "SupplierThirdParty.Nit", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "TotalValue", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListElectronicSupportDocument
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
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub
#End Region

#Region "Events"
#Region "QueryPopup"
    Private Sub INDSleSupplierThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSupplierThirdParty.QueryPopUp
        If INDSleSupplierThirdParty.Properties.DataSource Is Nothing Then
            Dim DataSource = presenter.ListSuppliersDistributionLines()
            INDSleSupplierThirdParty.Properties.DataSource = DataSource
        End If
    End Sub

    Private Sub INDSleBillingAuthorization_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBillingAuthorization.QueryPopUp
        If INDSleBillingAuthorization.Properties.DataSource Is Nothing Then
            Dim DataSource = presenter.ListAllBillingAuthorizationByUserCode(indigo.AuditMessageWcf.CodeUser)
            INDSleBillingAuthorization.Properties.DataSource = DataSource
        End If
    End Sub
#End Region
#Region "EditValueChanging"
    Private Sub INDTeSubTotalValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDTeSubTotalValue.EditValueChanging
        TotalValue = Convert.ToDecimal(Replace(e.NewValue, ".", ",")) + TaxValue
    End Sub
#End Region
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
        SaveElectronicSupportDocument()
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
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        SaveElectronicSupportDocument()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickSaveConfirm() Handles BarraBotones.Click_GuardarConfirmar
        SaveElectronicSupportDocument(True)
    End Sub

    Private Sub BarraBotones_ClickConfirm() Handles BarraBotones.ClickConfirmar
        SaveElectronicSupportDocument(True)
    End Sub

    Private Sub BarraBotones_ClickUpdateAndConfirm() Handles BarraBotones.Click_ActualizarConfirmar
        SaveElectronicSupportDocument(True)
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