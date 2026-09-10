#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmInventoryContractAssignment
    Implements IInventoryContractAssignment

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const MODULE_NAME As String = "Inventory"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PInventoryContractAssignment

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numérica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As BlockRecordInventory

    ''' <summary>
    ''' Representa la entidad de ordenes de compra
    ''' </summary>
    ''' <remarks></remarks>
    Private _InventoryContractAssignment As InventoryContractAssignment

    ''' <summary>
    ''' Variable para identificar si se esta cargando un registro
    ''' </summary>
    Dim _isLoading As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IInventoryContractAssignment.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Layout del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryContractAssignment.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numérica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As InventorySequence Implements IInventoryContractAssignment.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el código del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IInventoryContractAssignment.Code
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
    ''' Obtiene o Establece la Fecha del Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements IInventoryContractAssignment.DocumentDate
        Get
            Return INDdeDateDocument.EditValue
        End Get
        Set(value As DateTime?)
            INDdeDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de Contrato de Inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractId As Integer Implements IInventoryContractAssignment.ContractId
        Get
            Return INDsleContractId.EditValue
        End Get
        Set(value As Integer)
            INDsleContractId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id del Proveedor Cedente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierTransferorId As Integer Implements IInventoryContractAssignment.SupplierTransferorId

    ''' <summary>
    ''' Obtiene o Establece el Id de la Línea de Distribución del Proveedor Cedente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineTransferorId As Integer? Implements IInventoryContractAssignment.SupplierDistributionLineTransferorId
        Get
            Return INDsleSupplierDistributionLineTransferorId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierDistributionLineTransferorId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id del Proveedor Cesionario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierAssigneeId As Integer Implements IInventoryContractAssignment.SupplierAssigneeId

    ''' <summary>
    ''' Obtiene o Establece el Id de la Línea de Distribución del Proveedor Cesionario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineAssigneeId As Integer? Implements IInventoryContractAssignment.SupplierDistributionLineAssigneeId
        Get
            Return INDsleSupplierDistributionLineAssigneeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplierDistributionLineAssigneeId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece la Descripción de la Orden de la Compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IInventoryContractAssignment.Description
        Get
            Return INDmeDetail.EditValue
        End Get
        Set(value As String)
            INDmeDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece los Estados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As String Implements IInventoryContractAssignment.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

#End Region

#Region "Datasources"

    Public Property ListContractInventory As XPInstantFeedbackSource Implements IInventoryContractAssignment.ListContractInventory
        Get
            Return INDsleContractId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleContractId.Properties.DataSource = value
        End Set
    End Property

    Public Property SuppliersDistributionLinesAssigneeXpo As XPInstantFeedbackSource Implements IInventoryContractAssignment.SuppliersDistributionLinesAssigneeXpo
        Get
            Return INDsleSupplierDistributionLineAssigneeId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplierDistributionLineAssigneeId.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Metodo que elimina la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _InventoryContractAssignment.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MInventoryContractAssignment(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveInventoryContractAssignment(_InventoryContractAssignment)
                AsyncLoader(False)
                If Result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message

                    _InventoryContractAssignment = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _InventoryContractAssignment.Id, 0, _InventoryContractAssignment.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _InventoryContractAssignment.Id, 0, _InventoryContractAssignment.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _InventoryContractAssignment.Id, 0, _InventoryContractAssignment.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _InventoryContractAssignment.Id, 0, _InventoryContractAssignment.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewInventoryContractAssignment()
        End If
    End Sub

    ''' <summary>
    ''' este método abre el frontal de búsqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate"},
                              New ColumnInfo With {.Caption = "Contrato", .FieldName = "ContractId.ContractNumber"},
                              New ColumnInfo With {.Caption = "Proveedor Cedente", .FieldName = "SupplierTransferorId.CodeName"},
                              New ColumnInfo With {.Caption = "Proveedor Cesionario", .FieldName = "SupplierAssigneeId.CodeName"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventoryContractAssignment
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor de la búsqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryContractAssignment.ActionsOnControls
        Set(value As Boolean)
            INDlyInventoryContractAssignment.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDdeDateDocument.Enabled = value
            INDsleContractId.Enabled = value
            INDsleSupplierDistributionLineTransferorId.Enabled = value
            INDsleSupplierDistributionLineAssigneeId.Enabled = value
            INDmeDetail.Enabled = value

            INDlyInventoryContractAssignment.EndUpdate()
            If value Then
                INDdeDateDocument.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyInventoryContractAssignment.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbtnCode.Text = String.Empty
        INDdeDateDocument.EditValue = Nothing
        INDsleContractId.EditValue = Nothing
        INDsleContractId.Properties.NullText = Nothing
        INDsleSupplierDistributionLineTransferorId.EditValue = Nothing
        INDsleSupplierDistributionLineTransferorId.Properties.NullText = String.Empty
        INDsleSupplierDistributionLineAssigneeId.EditValue = Nothing
        INDsleSupplierDistributionLineAssigneeId.Properties.NullText = String.Empty
        INDmeDetail.EditValue = Nothing

        _doc = Nothing
        _InventoryContractAssignment = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False
        INDbtnCode.Focus()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        INDlyInventoryContractAssignment.EndUpdate()
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la lógica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewInventoryContractAssignment() As Task
        INDsleSupplierDistributionLineTransferorId.Properties.ReadOnly = True
        _InventoryContractAssignment = New InventoryContractAssignment() With {.Status = 1}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._InventoryContractAssignment.Code, Me._InventoryContractAssignment.DescriptionSupplierTransferor, Me._InventoryContractAssignment.DescriptionSupplierAssignee),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._InventoryContractAssignment.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._InventoryContractAssignment.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._InventoryContractAssignment.Code, Me._InventoryContractAssignment.DescriptionSupplierTransferor, Me._InventoryContractAssignment.DescriptionSupplierAssignee)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._InventoryContractAssignment.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Método que borra el registro de auditoria
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituel
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._InventoryContractAssignment IsNot Nothing AndAlso Me._InventoryContractAssignment.Id > 0 Then
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
    ''' Método que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MInventoryContractAssignment(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetInventoryContractAssignment(INDbtnCode.Text.Trim)
                    If Not resultOperation.StateResult Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                        Deshacer()
                        Exit Function
                    End If

                    _InventoryContractAssignment = resultOperation.ObjectEmbbeded
                    INDlyInventoryContractAssignment.BeginUpdate()
                    If _InventoryContractAssignment IsNot Nothing AndAlso _InventoryContractAssignment.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            With _InventoryContractAssignment
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                _isLoading = True
                                Me._idOperativeUnit = .OperatingUnitId
                                Me.BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Code = .Code
                                DocumentDate = .DocumentDate
                                ContractId = .ContractId
                                INDsleContractId.Properties.NullText = .ContractNumber
                                SupplierTransferorId = .SupplierTransferorId
                                SupplierDistributionLineTransferorId = .SupplierDistributionLineTransferorId
                                INDsleSupplierDistributionLineTransferorId.Properties.NullText = .DescriptionSupplierTransferor
                                SupplierAssigneeId = .SupplierAssigneeId
                                SupplierDistributionLineAssigneeId = .SupplierDistributionLineAssigneeId
                                INDsleSupplierDistributionLineAssigneeId.Properties.NullText = .DescriptionSupplierAssignee
                                Description = .Description
                                Status = .Status.ToString()
                                Me.BarraBotones.StatusRecordVisible = True
                                _isLoading = False
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._InventoryContractAssignment.Code)

                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_InventoryContractAssignment.Id))
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _InventoryContractAssignment.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            If _InventoryContractAssignment.Status = 1 Then 'Registrado
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                INDsleSupplierDistributionLineTransferorId.Properties.ReadOnly = True
                            Else 'Confirmado o Anulado
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                ReadOnlyControls(True)
                            End If

                            Me.BarraBotones.SetDocuments(_InventoryContractAssignment.Id, Me.Tag.ToString(), Nothing, GetType(InventoryContractAssignment).Name)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _InventoryContractAssignment.Id, 0, _InventoryContractAssignment.Id)

                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInventoryContractAssignment()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyInventoryContractAssignment.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _InventoryContractAssignment
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .Code = Code
            .DocumentDate = DocumentDate
            .ContractId = ContractId
            .SupplierTransferorId = SupplierTransferorId
            .SupplierDistributionLineTransferorId = SupplierDistributionLineTransferorId
            .SupplierAssigneeId = SupplierAssigneeId
            .SupplierDistributionLineAssigneeId = SupplierDistributionLineAssigneeId
            .Description = Description
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmInventoryContractAssignment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyInventoryContractAssignment, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        _presenter = New PInventoryContractAssignment(Me)
        '******************************
        AsyncLoader(True)
        Await _presenter.GetSequense()
        AsyncLoader(False)
        '******************************

        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _blockRecord = Nothing
        _InventoryContractAssignment = Nothing
        _isLoading = False
        _varImp = Nothing
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewInventoryContractAssignment()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContractId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleContractId.EditValueChanged
        If INDsleContractId.EditValue IsNot Nothing AndAlso INDsleContractId.EditValue <> 0 AndAlso Not _isLoading Then
            Dim contract = DirectCast(INDsleContractId.GetSelectedObject(), Infrastructure.Data.Xpo.InventoryRepository.InventoryContractXpo)
            If contract Is Nothing Then
                contract = _presenter.GetContractById(ContractId)
            End If

            SupplierTransferorId = contract.SupplierId.Id
            SupplierDistributionLineTransferorId = contract.SupplierDistributionLineId
            INDsleSupplierDistributionLineTransferorId.Properties.NullText = contract.SupplierId.CodeName
        End If
    End Sub

    Private Sub INDsleSupplierDistributionLineAssigneeId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplierDistributionLineAssigneeId.EditValueChanged
        If INDsleSupplierDistributionLineAssigneeId.EditValue IsNot Nothing AndAlso INDsleSupplierDistributionLineAssigneeId.EditValue <> 0 AndAlso Not _isLoading Then
            Dim supplierDistributionLineAssignee = DirectCast(INDsleSupplierDistributionLineAssigneeId.GetSelectedObject(), Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
            If supplierDistributionLineAssignee Is Nothing Then
                supplierDistributionLineAssignee = _presenter.GetSuppliersDistributionLineIdById(SupplierDistributionLineAssigneeId)
            End If

            SupplierAssigneeId = supplierDistributionLineAssignee.IdSupplier.Id
        End If
    End Sub

#End Region

#Region "FromClosing"

    ''' <summary>
    ''' Se dispara cuando se cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmInventoryContractAssignment_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    Private Sub FrmInventoryContractAssignment_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Realiza la consulta del combo de Contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSeLookEdContract_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractId.QueryPopUp
        If INDsleContractId.Properties.DataSource Is Nothing Then
            _presenter.LoadContractInventory()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta del combo de Proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplierDistributionLineAssigneeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplierDistributionLineAssigneeId.QueryPopUp
        If INDsleSupplierDistributionLineAssigneeId.Properties.DataSource Is Nothing Then
            _presenter.InitializeSupplierAssignee()
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
    Private Sub FrmInventoryContractAssignment_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
        INDdeDateDocument.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _InventoryContractAssignment.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _InventoryContractAssignment.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _InventoryContractAssignment.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _InventoryContractAssignment.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _InventoryContractAssignment.Status = 3
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el botón imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _InventoryContractAssignment.Id, 0, _InventoryContractAssignment.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class