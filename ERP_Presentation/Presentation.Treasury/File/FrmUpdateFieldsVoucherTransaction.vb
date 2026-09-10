Imports Domain.Entities
Imports Presentation.Treasury.MVP
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Extension
Imports Presentation.Common.MVP

Public Class FrmUpdateFieldsVoucherTransaction
    Implements Presentation.Base.IcrudBase, ICustomizableForm

#Region "Builder"

#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' Nombre del módulo
    ''' </summary>
    Private Const MODULE_NAME As String = "Treasury"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad Comprobante de Egresos
    ''' </summary>
    Private _voucherTransaction As Domain.Entities.VoucherTransaction

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordTreasury

    ''' <summary>
    ''' The model
    ''' </summary>
    Dim model As MVoucherTransaction

    Public Enum eActionsControls
        Undo
        All
        Voucher
    End Enum

    ''' <summary>
    ''' Habilita o deshabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As eActionsControls
        Set(value As eActionsControls)
            INDLcRoot.BeginUpdate()
            If value = eActionsControls.Undo Then
                INDGleDataUpdate.Enabled = True
                INDSleVoucherTransaction.Enabled = False
                INDTxtDebitNote.Enabled = False
                INDMeObservation.Enabled = False
                INDGleDataUpdate.Focus()
            ElseIf value = eActionsControls.Voucher Then
                INDGleDataUpdate.Enabled = True
                INDSleVoucherTransaction.Enabled = True
                INDTxtDebitNote.Enabled = False
                INDMeObservation.Enabled = False
                INDSleVoucherTransaction.Focus()
            ElseIf value = eActionsControls.All Then
                INDGleDataUpdate.Enabled = False
                INDSleVoucherTransaction.Enabled = False
                INDTxtDebitNote.Enabled = True
                INDMeObservation.Enabled = True
                INDTxtDebitNote.Focus()
            End If
            INDLcRoot.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

#Region "Properties Entity"
    ''' <summary>
    ''' Gets the code.
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public ReadOnly Property Code As String
        Get
            Return INDSleVoucherTransaction.EditValue
        End Get
    End Property
    ''' <summary>
    ''' Gets or sets the fields update.
    ''' </summary>
    ''' <value>
    ''' The fields update.
    ''' </value>
    Public Property FieldsUpdate As Byte
        Get
            Return CType(INDGleDataUpdate.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDGleDataUpdate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the note debit.
    ''' </summary>
    ''' <value>
    ''' The note debit.
    ''' </value>
    Public Property NoteDebit As String
        Get
            Return INDTxtDebitNote.Text
        End Get
        Set(value As String)
            INDTxtDebitNote.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the observation.
    ''' </summary>
    ''' <value>
    ''' The observation.
    ''' </value>
    Public Property Observation As String
        Get
            Return INDMeObservation.Text
        End Get
        Set(value As String)
            INDMeObservation.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the status.
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Public Property Status As String
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property
#End Region

#End Region

#Region "Handlers"
#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _voucherTransaction = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        model = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmUpdateFieldsVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmUpdateFieldsVoucherTransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        LoadDefinitionLayout()
        model = New MVoucherTransaction(Me.Tag)

        InitializeUpdateType()
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "Actived"
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDGleDataUpdate.Enabled Then
            INDGleDataUpdate.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDSleVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleVoucherTransaction_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleVoucherTransaction.QueryPopUp
        If INDSleVoucherTransaction.Properties.DataSource Is Nothing Then
            If FieldsUpdate <> 0 Then
                Select Case FieldsUpdate
                    Case 1
                        INDSleVoucherTransaction.Properties.DataSource = model.ListVoucherTransactionByPaymentMethod(2)
                    Case 2
                        INDSleVoucherTransaction.Properties.DataSource = model.ListVoucherTransactionXpo()
                    Case 3
                        INDSleVoucherTransaction.Properties.DataSource = model.ListVoucherTransactionByPaymentMethod(2)
                End Select
            End If
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSleVoucherTransaction_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleVoucherTransaction.ButtonClick

    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDGleDataUpdate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDGleDataUpdate_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDataUpdate.EditValueChanged
        If FieldsUpdate <> 0 Then
            INDSleVoucherTransaction.Properties.DataSource = Nothing
            ActionsOnControls = eActionsControls.Voucher
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDSleVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleVoucherTransaction_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleVoucherTransaction.EditValueChanged
        If INDSleVoucherTransaction.EditValue IsNot Nothing Then
            _voucherTransaction = New VoucherTransaction()
            Dim voucher As VoucherTransactionXpo = CType(CType(INDGvVoucherTransaction.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, VoucherTransactionXpo)
            _voucherTransaction.Id = voucher.Id
            _voucherTransaction.NoteNumber = voucher.NoteNumber
            _voucherTransaction.Detail = voucher.Detail

            If FieldsUpdate = 1 Then
                INDLciDebitNote.HideControl(False)
                INDLciObservation.HideControl()
                INDMeObservation.Text = String.Empty

            ElseIf FieldsUpdate = 2 Then
                INDLciDebitNote.HideControl()
                INDLciObservation.HideControl(False)
                INDTxtDebitNote.Text = String.Empty
            ElseIf FieldsUpdate = 3 Then
                INDLciDebitNote.HideControl(False)
                INDLciObservation.HideControl(False)
            End If
            LoadControls()
        End If
    End Sub
#End Region

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmUpdateFieldsVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmUpdateFieldsVoucherTransaction_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
    End Sub
#End Region
#End Region

#Region "Methods"
    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._voucherTransaction.Code, _voucherTransaction.Detail),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._voucherTransaction.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._voucherTransaction.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._voucherTransaction.Code, Me._voucherTransaction.Detail)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._voucherTransaction.Code)
            Return Me._doc
        End If
    End Function
    ''' <summary>
    ''' Loads the status.
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

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        'If _voucherTransaction IsNot Nothing Then
        '    If FieldsUpdate = 1 Then
        '        INDTxtDebitNote.Text = _voucherTransaction.NoteNumber
        '    ElseIf FieldsUpdate = 2 Then
        '        INDMeObservation.Text = _voucherTransaction.Detail
        '    ElseIf FieldsUpdate = 3 Then
        '        INDTxtDebitNote.Text = _voucherTransaction.NoteNumber
        '        INDMeObservation.Text = _voucherTransaction.Detail
        '    End If
        '    ActionsOnControls = eActionsControls.All
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        'End If

        Me.BarraBotones.StatusRecordVisible = True
        AsyncLoader(True)
        Dim resultObject = Await model.GetVoucherTransaction(Me.Code)
        _voucherTransaction = resultObject.ObjectEmbbeded
        INDLcRoot.BeginUpdate()
        If _voucherTransaction IsNot Nothing AndAlso _voucherTransaction.Id > 0 Then
            Using modelBlock As New MCommonTreasury(Me.Tag)
                Dim result = Await modelBlock.GetBlockRecordTreasury(Me.Tag, _voucherTransaction.Id)
                With _voucherTransaction
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                    If FieldsUpdate = 1 Then
                        INDTxtDebitNote.Text = _voucherTransaction.NoteNumber
                    ElseIf FieldsUpdate = 2 Then
                        INDMeObservation.Text = _voucherTransaction.Detail
                    ElseIf FieldsUpdate = 3 Then
                        INDTxtDebitNote.Text = _voucherTransaction.NoteNumber
                        INDMeObservation.Text = _voucherTransaction.Detail
                    End If
                    Status = .Status
                End With

                Me.GetDocumentIndexed(Me.Tag & "_" & Me._voucherTransaction.Code)
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    _record = New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _voucherTransaction.Id}
                    Dim operation = Await modelBlock.SaveBlockRecordTreasury(_record)
                    _record = operation.ObjectEmbbeded
                Else
                    _record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
                Me.BarraBotones.SetDocuments(_voucherTransaction.Id, Me.Tag.ToString(), Nothing, GetType(VoucherTransaction).Name)

                AsyncLoader(False)
                ActionsOnControls = eActionsControls.All
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            End Using
        Else
            AsyncLoader(False)
            Deshacer()
        End If
        INDLcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDLcRoot.BeginUpdate()

        INDGleDataUpdate.EditValue = Nothing
        INDSleVoucherTransaction.EditValue = Nothing
        INDTxtDebitNote.Text = String.Empty
        INDMeObservation.Text = String.Empty
        ActionsOnControls = eActionsControls.Undo

        _voucherTransaction = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()

        INDLciDebitNote.HideControl()
        INDLciObservation.HideControl()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        INDLcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _voucherTransaction
            .UpdateFields = True
            If INDLciDebitNote.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NoteNumber = NoteDebit
            End If
            If INDLciObservation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Detail = Observation
            End If
        End With
    End Sub

    ''' <summary>
    ''' Initializes the type of the update.
    ''' </summary>
    Private Sub InitializeUpdateType()
        Dim _listUpdateFields As New List(Of Tuple(Of Byte, String))()
        _listUpdateFields.Add(New Tuple(Of Byte, String)(1, "# Nota Debito"))
        _listUpdateFields.Add(New Tuple(Of Byte, String)(2, "Detalle"))
        _listUpdateFields.Add(New Tuple(Of Byte, String)(3, "# Nota Debito / Detalle"))
        INDGleDataUpdate.Properties.DataSource = _listUpdateFields
    End Sub
#End Region

#Region "Crud"
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

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of VoucherTransaction) = Await model.SaveVoucherTransaction(Me._voucherTransaction, False, 0, Nothing)
            If result.StatusCode = eStatusResult.SUCCESS Then
                Me._voucherTransaction = result.ObjectEmbbeded
                Mensaje(EeventViewerImages.Informacion) = result.Message

                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Deshacer()
            Else
                AsyncLoader(False)
                ActionsOnControls = eActionsControls.All
                Mensaje(EeventViewerImages.Advertencia) = result.Message
            End If
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
            ActionsOnControls = eActionsControls.All
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

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
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(status As eStatusResult) As String
        Set(value As String)
            If status = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf status = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf status = eStatusResult.EXCEPTION Then
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

#Region "BarButtons"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
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
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
#End Region

End Class