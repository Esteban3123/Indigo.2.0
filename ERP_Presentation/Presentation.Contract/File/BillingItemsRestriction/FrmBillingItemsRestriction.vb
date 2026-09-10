'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Giovanny Plazas L
' Created          : 15/12/2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class FrmBillingItemsRestriction
    Implements IBillingItemsRestriction, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IBillingItemsRestriction.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IBillingItemsRestriction.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBillingItemsRestriction.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IBillingItemsRestriction.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameRestriction As String Implements IBillingItemsRestriction.NameRestriction
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Integer Implements IBillingItemsRestriction.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Integer)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As ContractSequence Implements IBillingItemsRestriction.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As ContractSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequence.ContractSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la lista de deatlles de las restricciones
    ''' </summary>
    ''' <returns></returns>
    Public Property ListBillingItemsRestriction As List(Of BillingItemsRestrictionDetail)
        Get
            Return _listBillingItemsRestrictionDetail
        End Get
        Set(value As List(Of BillingItemsRestrictionDetail))
            _listBillingItemsRestrictionDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna la entidad cabecera de restriccion
    ''' </summary>
    ''' <returns></returns>
    Public Property BillingItemsRestriction As BillingItemsRestriction
        Get
            Return _billingItemsRestriction
        End Get
        Set(value As BillingItemsRestriction)
            _billingItemsRestriction = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBillingItemsRestriction.ActionsOnControls
        Set(value As Boolean)
            INDlyBillingItemsRestriction.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDmemoDescription.Enabled = value
            INDbtnAddRules.Enabled = value
            INDgcRules.Enabled = value
            INDlyBillingItemsRestriction.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#Region "Variables"
    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PBillingItemsRestriction

    ''' <summary>
    ''' Entidad servicios no facturables
    ''' </summary>
    ''' <remarks></remarks>
    Dim _billingItemsRestriction As BillingItemsRestriction

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' lista de los detalles de las restricciones
    ''' </summary>
    Private _listBillingItemsRestrictionDetail As List(Of BillingItemsRestrictionDetail)

#End Region

#Region "ICrud"

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
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If BillingItemsRestriction IsNot Nothing AndAlso BillingItemsRestriction.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MBillingItemsRestriction(Me.Tag.ToString())
                        AsyncLoader(True)
                        BillingItemsRestriction.Company = indigo.TransactionalContainer()
                        Dim result = Await Model.DeleteBillingItemsRestriction(BillingItemsRestriction)
                        If result.StateResult = True Then
                            AsyncLoader(False)
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        Try
            AssigningValues()
            Using model As New MBillingItemsRestriction(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveBillingItemsRestriction(Me.BillingItemsRestriction, Me.ListBillingItemsRestriction, _idCurrentSequence)

                If Result Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ErrorUnknown")
                    Exit Sub
                End If

                If Not Result.StateResult Then

                    If Result?.MessageResult.Any(Function(x) x = ErrorConcurrencia) Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ErrorConcurrence")
                        Exit Sub
                    End If

                    If Result?.MessageResult.Any() Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Join(",", Result?.MessageResult)
                        Exit Sub
                    End If

                    Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                    Exit Sub
                End If

                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                Me.Deshacer()
            End Using
        Catch ex As Exception

            INDbtnCode.Enabled = False
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewBillingItemsRestriction()
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que abre el form de agregar reglas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenAddRule(modeEdit As Boolean, Optional BillingItemsRestrictionDetail As BillingItemsRestrictionDetail = Nothing)
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmAddRuleRestriction()
            AddHandler Formulario.AddInfoToGridBillingItemsRestriction, AddressOf AddInfoToGridBillingItemsRestriction
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1090
            Formulario.Height = 768
            Formulario.ModeEdit = modeEdit
            If modeEdit Then
                Formulario.ListBillingItemsRestrictionDetail = {BillingItemsRestrictionDetail}.ToList()
            End If
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega la regla que viene del form
    ''' que se lanza de forma modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddInfoToGridBillingItemsRestriction(sender As Object, e As AddInfoToGridBillingItemsRestriction)

        If ListBillingItemsRestriction Is Nothing Then
            ListBillingItemsRestriction = New List(Of BillingItemsRestrictionDetail)
        End If

        Dim errors As String = ValidateListDetail(e.ListBillingItemsRestrictionDetail, e.ModeEdit)
        If errors.Length > 0 Then
            e.ReturnValueOk = False
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If Not e.ModeEdit Then 'Cuando se va a agregar
            Me.ListBillingItemsRestriction.AddRange(e.ListBillingItemsRestrictionDetail)
            Me.INDgcRules.DataSource = Me.ListBillingItemsRestriction.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
            Me.INDgcRules.RefreshDataSource()
            Mensaje(EeventViewerImages.Informacion) = "Reglas agregadas correctamente."
        Else 'Cuando se va a modificar
            Mensaje(EeventViewerImages.Informacion) = "Regla editada correctamente."
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida el listado que viene desde el form modal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListDetail(ListDetail As List(Of BillingItemsRestrictionDetail), editMode As Boolean) As String
        Dim listErrors As New StringBuilder

        If ListDetail Is Nothing OrElse Not ListDetail.Any() Then
            Return "El Objeto a Validar esta vacio"
        End If

        'validar que la restriccion no este ya en la rejilla
        ListBillingItemsRestriction.ForEach(Function(x) ListDetail _
                                                    .FindAll(Function(e) e.RuleType = x.RuleType) _
                                                    .Any(Function(s)
                                                             Dim validate As Boolean
                                                             Select Case x.RuleType
                                                                 Case Utils.EItemsRestrictionRuleType.CUPS
                                                                     validate = s.CUPSEntityId = x.CUPSEntityId
                                                                 Case Utils.EItemsRestrictionRuleType.General
                                                                     validate = True
                                                                 Case Utils.EItemsRestrictionRuleType.GroupCUPS
                                                                     validate = s.CUPSGroupId = x.CUPSGroupId
                                                                 Case Utils.EItemsRestrictionRuleType.GroupProduct
                                                                     validate = s.ProductGroupId = x.ProductGroupId
                                                                 Case Utils.EItemsRestrictionRuleType.Product
                                                                     validate = s.ProductId = x.ProductId
                                                                 Case Utils.EItemsRestrictionRuleType.SubGroupCUPS
                                                                     validate = s.CUPSSubgroupId = x.CUPSSubgroupId
                                                                 Case Utils.EItemsRestrictionRuleType.SubGroupProduct
                                                                     validate = s.ProductSubGroupId = x.ProductSubGroupId
                                                             End Select

                                                             Dim flag = validate _
                                                                        AndAlso s.ConditionType = x.ConditionType _
                                                                        AndAlso s.LogicalOperator = x.LogicalOperator _
                                                                        AndAlso s.ConditionType2 = x.ConditionType2

                                                             If Not editMode AndAlso flag Then
                                                                 listErrors.AppendLine($"La regla {s.RuleTypeName}: {s.RuleDescription} ya se encuentra agregada a la rejilla")
                                                             End If
                                                             Return flag
                                                         End Function))

        Return listErrors.ToString()
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.BillingItemsRestriction IsNot Nothing AndAlso Me.BillingItemsRestriction.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listItemsColumnEdit As New List(Of Tuple(Of String, Boolean))
        listItemsColumnEdit.Add(New Tuple(Of String, Boolean)("Activo", True))
        listItemsColumnEdit.Add(New Tuple(Of String, Boolean)("Inactivo", False))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15), .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEdit}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBillingItemsRestriction
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
        Await DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.BillingItemsRestriction.Code, Me.BillingItemsRestriction.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.BillingItemsRestriction.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.BillingItemsRestriction.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.BillingItemsRestriction.Code, Me.BillingItemsRestriction.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.BillingItemsRestriction.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyBillingItemsRestriction.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameRestriction = String.Empty
        Description = String.Empty
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyBillingItemsRestriction.EndUpdate()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BillingItemsRestriction = Nothing
        Me.ListBillingItemsRestriction = Nothing
        Me.INDgcRules.DataSource = Nothing
        Me.INDgcRules.RefreshDataSource()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With BillingItemsRestriction
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameRestriction
            .Description = Description
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
        Me.ListBillingItemsRestriction = Me.ListBillingItemsRestriction
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="BillingItemsRestrictionId"></param>
    ''' <returns></returns>
    Private Async Function LoadListDetail(BillingItemsRestrictionId As Integer) As Task

        Me.viewRules.ShowLoadingPanel()

        Dim result = Await Presenter.ListItemsRestrictionDetailByIdHeader(BillingItemsRestrictionId)

        Me.viewRules.HideLoadingPanel()

        If result Is Nothing OrElse Not result?.StateResult Then
            Throw New Exception(result?.Message)
        End If

        Me.ListBillingItemsRestriction = result.ObjectEmbbeded
        Me.INDgcRules.DataSource = Me.ListBillingItemsRestriction
        Me.INDgcRules.RefreshDataSource()

    End Function

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
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MBillingItemsRestriction(CStr(Me.Tag))
                    AsyncLoader(True)
                    BillingItemsRestriction = (Await Model.GetBillingItemsRestriction(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyBillingItemsRestriction.BeginUpdate()
                    If BillingItemsRestriction IsNot Nothing AndAlso BillingItemsRestriction.Id > 0 Then

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(BillingItemsRestriction.Id))
                            With BillingItemsRestriction
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                NameRestriction = .Name
                                Description = .Description
                                Status = .Status

                                Await LoadListDetail(.Id)
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.BillingItemsRestriction.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = BillingItemsRestriction.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            AsyncLoader(False)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(BillingItemsRestriction.Id, Me.Tag.ToString(), Nothing, GetType(BillingItemsRestriction).Name)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBillingItemsRestriction()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyBillingItemsRestriction.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Deshacer()
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewBillingItemsRestriction() As Task
        BillingItemsRestriction = New BillingItemsRestriction With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.ContractSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.ContractSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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

        Status = True
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MBillingItemsRestriction(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not BillingItemsRestriction.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.BillingItemsRestriction = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorSaveState", NAME_MODULE)
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = $"{ResourceManager.GetString("ErrorSaveState", NAME_MODULE)} Debe guardar el registro"
        End If
    End Function

    ''' <summary>
    ''' Edita una regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditRule()
        Dim BillingItemsRestrictionDetail = CType(viewRules.GetFocusedRow, BillingItemsRestrictionDetail)
        OpenAddRule(True, BillingItemsRestrictionDetail)
    End Sub

    ''' <summary>
    ''' Elimina una regla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteRule()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim obj As BillingItemsRestrictionDetail = viewRules.GetFocusedRow()
        If obj IsNot Nothing AndAlso obj.Id > 0 Then
            obj.MarkAsDeleted()
        Else
            Me.ListBillingItemsRestriction.Remove(obj)
        End If
        Me.INDgcRules.DataSource = Me.ListBillingItemsRestriction.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
        Me.INDgcRules.RefreshDataSource()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        BillingItemsRestriction = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _listBillingItemsRestrictionDetail = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBillingItemsRestriction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyBillingItemsRestriction, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PBillingItemsRestriction(Me)
        Presenter.GetSequense()
        IndigoGridControl1.RefreshGrid(INDgcRules)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewRules, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRules.Columns
            If col.Name = "colActions" Then
                col.Width = 80
            End If
        Next

        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBillingItemsRestriction_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewBillingItemsRestriction()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBillingItemsRestriction_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddRules_Click(sender As Object, e As EventArgs) Handles INDbtnAddRules.Click
        OpenAddRule(False)
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Evento que se dispara al presionar sobre el menu alguna accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag.ToString()
            Case "Edit"
                EditRule()
            Case "Remove"
                DeleteRule()
        End Select
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcRules_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcRules.DataSourceChanged
        viewRules.ExpandAllGroups()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
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
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.ContractSequenceDetail IsNot Nothing Then
                If Not Me._sequence.ContractSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class