Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository

Public Class FrmElectronicSupportDocumentAdjustmentNote
    Implements ICustomizableForm, IElectronicSupportDocumentAdjustmentNote

#Region "Properties"

    Private Const NAME_MODULE As String = "Billing"

    Private _electronicSuportDocumentSelectedValue As Decimal? = Nothing

    ''' <summary>
    ''' Tag del formularoop
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IElectronicSupportDocumentAdjustmentNote.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Layout principal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IElectronicSupportDocumentAdjustmentNote.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IElectronicSupportDocumentAdjustmentNote.Code
        Get
            If INDBeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBeCode.Text
            End If
        End Get
        Set(value As String)
            INDBeCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Soporte electrónico
    ''' </summary>
    ''' <returns></returns>
    Public Property ElectronicSupportDocumentId As Integer Implements IElectronicSupportDocumentAdjustmentNote.ElectronicSupportDocumentId
        Get
            Return INDSleSupportDocument.EditValue
        End Get
        Set(value As Integer)
            INDSleSupportDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentDate As Date Implements IElectronicSupportDocumentAdjustmentNote.DocumentDate
        Get
            Return INDDeDate.EditValue
        End Get
        Set(value As Date)
            INDDeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String Implements IElectronicSupportDocumentAdjustmentNote.Description
        Get
            Return INDMeDescription.EditValue
        End Get
        Set(value As String)
            INDMeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Status
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As String Implements IElectronicSupportDocumentAdjustmentNote.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Subtotal
    ''' </summary>
    ''' <returns></returns>
    Public Property SubTotal As Decimal Implements IElectronicSupportDocumentAdjustmentNote.SubTotal
        Get
            Return INDSpnSubtotal.EditValue
        End Get
        Set(value As Decimal)
            INDSpnSubtotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de IVA
    ''' </summary>
    ''' <returns></returns>
    Public Property TaxValue As Decimal Implements IElectronicSupportDocumentAdjustmentNote.TaxValue
        Get
            Return INDSpnIVA.EditValue
        End Get
        Set(value As Decimal)
            INDSpnIVA.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor total
    ''' </summary>
    ''' <returns></returns>
    Public Property TotalValue As Decimal Implements IElectronicSupportDocumentAdjustmentNote.TotalValue
        Get
            Return INDSpnTotal.EditValue
        End Get
        Set(value As Decimal)
            INDSpnTotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' sequence
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As BillingSequence Implements IElectronicSupportDocumentAdjustmentNote.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Actions on controls
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IElectronicSupportDocumentAdjustmentNote.ActionsOnControls
        Set(value As Boolean)
            INDLcgRoot.BeginUpdate()
            INDBeCode.Enabled = Not value
            INDDeDate.Enabled = value
            INDSleSupportDocument.Enabled = value
            INDTeSupportDocumentDate.Enabled = value
            INDteSupportDocumentValue.Enabled = value
            INDMeDescription.Enabled = value
            INDGleNature.Enabled = value
            INDGleType.Enabled = value
            INDSpnSubtotal.Enabled = value
            INDSpnIVA.Enabled = value
            INDSpnTotal.Enabled = value
            INDLcgRoot.EndUpdate()

            If value Then
                INDDeDate.Focus()
            Else
                INDBeCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tipo de nota
    ''' </summary>
    ''' <returns></returns>
    Public Property NoteType As Byte Implements IElectronicSupportDocumentAdjustmentNote.NoteType
        Get
            Return INDGleType.EditValue
        End Get
        Set(value As Byte)
            INDGleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Naturaleza
    ''' </summary>
    ''' <returns></returns>
    Public Property Nature As Byte Implements IElectronicSupportDocumentAdjustmentNote.Nature
        Get
            Return INDGleNature.EditValue
        End Get
        Set(value As Byte)
            INDGleNature.EditValue = value
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
    Private _electronicSupportDocument As ElectronicSupportDocumentAdjustmentNote

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private presenter As PElectronicSupportDocumentAdjustmentNote

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling
#End Region

#Region "handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _electronicSupportDocument = Nothing
        _searchMode = Nothing
        presenter = Nothing
        _record = Nothing
    End Sub

    Private Sub FrmElectronicSupportDocumentAdjustmentNote_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        _doc = Nothing
        indigo = SessionValues.Instance
        presenter = New PElectronicSupportDocumentAdjustmentNote(Me)
        presenter.GetSequence()
        LoadStatus()
        Deshacer()
        initTuples()
    End Sub

    ''' <summary>
    ''' iniciar tuplas
    ''' </summary>
    Private Sub initTuples()
        Dim types = {New Tuple(Of Byte, String)(1, "Ajuste documento soporte"), New Tuple(Of Byte, String)(2, "Reversión CxP")}.ToList()
        INDGleType.Properties.DataSource = types

        Dim natures = {New Tuple(Of Byte, String)(1, "Débito"), New Tuple(Of Byte, String)(2, "Crédito")}
        INDGleNature.Properties.DataSource = natures
    End Sub

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Shown(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBeCode.Enabled Then
            INDBeCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBeCode.KeyDown
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
                    Await Me.NewElectronicSupportDocumentAdjustmentNote()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "Methods"
    Public Async Function LoadControls() As Task Implements IElectronicSupportDocumentAdjustmentNote.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MElectronicSupportDocumentAdjustmentNote(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetElectronicSupportDocumentAdjustmentNoteByCodeAsync(INDBeCode.Text.Trim)
                    _electronicSupportDocument = resultOperation.ObjectEmbbeded

                    INDLcRoot.BeginUpdate()

                    If _electronicSupportDocument IsNot Nothing AndAlso _electronicSupportDocument.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_electronicSupportDocument.Id))
                            With _electronicSupportDocument
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                DocumentDate = .DocumentDate
                                ElectronicSupportDocumentId = .ElectronicSupportDocumentId
                                Description = .Description
                                NoteType = .NoteType
                                Nature = .Nature
                                SubTotal = .SubTotalValue
                                TaxValue = .TaxValue
                                TotalValue = .TotalValue
                                Status = .Status
                                _electronicSuportDocumentSelectedValue = .ElectronicSupportDocumentValue
                                BarraBotones.OperatingUnitValue = .OperativeUnitId
                            End With

                            INDSleSupportDocument.Properties.NullText = _electronicSupportDocument.ElectronicSupportDocumentFullName
                            INDTeSupportDocumentDate.Text = _electronicSupportDocument.ElectronicSupportDocumentDate
                            INDteSupportDocumentValue.Text = _electronicSupportDocument.ElectronicSupportDocumentValue

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._electronicSupportDocument.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _electronicSupportDocument.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(_electronicSupportDocument.Id, Me.Tag.ToString(), Nothing, GetType(BillingAuthorization).Name)

                            If _electronicSupportDocument.Status = 2 OrElse _electronicSupportDocument.Status = 3 Then
                                ReadOnlyControls(True)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            End If
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewElectronicSupportDocumentAdjustmentNote()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBeCode.Focus()
                        End If
                    End If
                    INDLcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBeCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Public Sub CleanControls() Implements IElectronicSupportDocumentAdjustmentNote.CleanControls
        INDLcRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Status = 1
        Code = String.Empty
        DocumentDate = GetDateServer()
        ElectronicSupportDocumentId = Nothing
        Description = ""
        INDGleNature.EditValue = CByte(1)
        INDGleNature.EditValue = CByte(1)
        SubTotal = 0
        TaxValue = 0
        TotalValue = 0
        _electronicSupportDocument = Nothing

        INDSleSupportDocument.Properties.NullText = String.Empty
        INDTeSupportDocumentDate.Text = String.Empty
        INDteSupportDocumentValue.Text = String.Empty

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        ReadOnlyControls(False)
        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Public Overrides Sub ReadOnlyControls(value As Boolean)
        INDLcgRoot.BeginUpdate()
        INDBeCode.ReadOnly = value
        INDDeDate.ReadOnly = value
        INDSleSupportDocument.ReadOnly = value
        INDTeSupportDocumentDate.ReadOnly = value
        INDteSupportDocumentValue.ReadOnly = value
        INDMeDescription.ReadOnly = value
        INDGleNature.ReadOnly = value
        INDGleType.ReadOnly = value
        INDSpnSubtotal.ReadOnly = value
        'INDSpnIVA.ReadOnly = value
        'INDSpnTotal.ReadOnly = value
        INDLcgRoot.EndUpdate()
    End Sub

    Public Sub AssigningValues(Optional withConfirm As Boolean = False) Implements IElectronicSupportDocumentAdjustmentNote.AssigningValues
        With _electronicSupportDocument
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .ElectronicSupportDocumentId = ElectronicSupportDocumentId
            .Description = Description
            .NoteType = NoteType
            .Nature = Nature
            .OperativeUnitId = _idOperativeUnit
            .SubTotalValue = SubTotal
            .TaxValue = TaxValue
            .TotalValue = TotalValue
            .Status = If(withConfirm, 2, Status)
        End With
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar(withConfirm As Boolean)
        If withConfirm Then
            If MessageIndigo.Show("¿Esta seguro que desea confirmar el documento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        If Not ValidateControls() Then
            Exit Sub
        End If

        AssigningValues(withConfirm)
        Try
            Using Model As New MElectronicSupportDocumentAdjustmentNote(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ElectronicSupportDocumentAdjustmentNote) = Await Model.SaveElectronicSupportDocumentAdjusmentNoteAsync(_electronicSupportDocument, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _electronicSupportDocument.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._electronicSupportDocument = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBeCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewElectronicSupportDocumentAdjustmentNote()
        End If
    End Sub

    Private Async Function NewElectronicSupportDocumentAdjustmentNote() As Task
        _electronicSupportDocument = New ElectronicSupportDocumentAdjustmentNote() With {.Status = 1}
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
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If

            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
    End Function

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Tipo nota", .FieldName = "NoteTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                New ColumnInfo() With {.Caption = "Naturaleza", .FieldName = "NatureName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                New ColumnInfo() With {.Caption = "Documento", .FieldName = "ElectronicSupportDocument.FullName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Valor", .FieldName = "TotalValue", .ColumnFormatType = DevExpress.Utils.FormatType.Numeric, .ColumnFormat = "c0", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}
            }.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListElectronicSupportDocumentAdjustmentNote
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBeCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub
#End Region


#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBeCode.ButtonClick
        OpenSearch()
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
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupportDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupportDocument.EditValueChanged
        SubTotal = 0
        If INDSleSupportDocument.EditValue Is Nothing Then
            INDLciSupportDocumentDate.HideLayout()
            INDLciSupportDocumentValue.HideLayout()

            INDTeSupportDocumentDate.EditValue = Nothing
            INDteSupportDocumentValue.EditValue = Nothing
        Else
            'INDLciSupportDocumentDate.ShowLayout()
            'INDLciSupportDocumentValue.ShowLayout()
            Dim row = INDSleSupportDocument.GetFocusedObject(Of ElectronicSupportDocumentXpo)()
            INDTeSupportDocumentDate.EditValue = row?.DocumentDate
            INDteSupportDocumentValue.EditValue = row?.TotalValue
            _electronicSuportDocumentSelectedValue = row?.TotalValue
        End If
    End Sub

    Private Sub INDSleSupportDocument_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSupportDocument.QueryPopUp
        If INDSleSupportDocument.Properties.DataSource Is Nothing Then
            INDSleSupportDocument.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListXPInstantFeedbackSource(Of ElectronicSupportDocumentXpo)("Status = 1")
        End If
    End Sub

    Private Sub INDSpnSubtotal_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSpnSubtotal.EditValueChanging
        If _electronicSuportDocumentSelectedValue Is Nothing Then
            e.Cancel = True
            Return
        End If

        Dim total = CDec(e.NewValue) + TaxValue

        If total > _electronicSuportDocumentSelectedValue.Value Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor de la nota no puede superar al del documento seleccionado"
            e.Cancel = True
            Return
        End If

        TotalValue = total
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Guardar(True)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Guardar(True)
    End Sub

#End Region

End Class