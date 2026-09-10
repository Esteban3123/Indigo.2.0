'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 19-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Inventory.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports System.Text
Imports Presentation.Glosas
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmReferralOut
    Implements IRemissionOutput, ICustomizableForm

#Region "GLOBALS"
    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory
    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory
    ''' <summary>
    ''' presenter de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Private presenter As PRemissionOutput
    ''' <summary>
    ''' entidad de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim remissionOutput As RemissionOutput
    ''' <summary>
    ''' detalle de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim remissionOutputDetail As RemissionOutputDetail
    ''' <summary>
    ''' listado del detalla de la remision
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRemissionOutputDetail As List(Of RemissionOutputDetail)
    ''' <summary>
    ''' listado del detalla de la remision para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRemissionOutputDetailDelete As List(Of RemissionOutputDetail)
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRemissionOutput.ActionsOnControls
        Set(value As Boolean)
            INDLcRemissionOutput.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDDteDate.Enabled = value
            INDSleCustomer.Enabled = value
            INDSleWarehouse.Enabled = value
            INDMeDetail.Enabled = value
            INDMeDetail.Enabled = value
            INDBtnAdd.Enabled = False
            INDGcProduct.Enabled = value
            INDGcProduct.Enabled = value
            INDLcRemissionOutput.EndUpdate()
            If value Then
                INDDteDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' codigo de la remision
    ''' </summary>
    Public Property Code As String Implements IRemissionOutput.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' id del cliente
    ''' </summary>
    Public Property CustomerId As Integer? Implements IRemissionOutput.CustomerId
        Get
            Return INDSleCustomer.EditValue
        End Get
        Set(value As Integer?)
            INDSleCustomer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' descripcion de la remision
    ''' </summary>
    Public Property Description As String Implements IRemissionOutput.Description
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRemissionOutput.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IRemissionOutput.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    Public Property RemissionDate As DateTime? Implements IRemissionOutput.RemissionDate
        Get
            Return INDDteDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDteDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    Public Property Sequense As InventorySequence Implements IRemissionOutput.Sequense
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
    ''' id del almacen
    ''' </summary>
    Public Property WarehouseId As Integer? Implements IRemissionOutput.WarehouseId
        Get
            Return INDSleWarehouse.EditValue
        End Get
        Set(value As Integer?)
            INDSleWarehouse.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WarehouseXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleWarehouse.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleWarehouse.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' datasource de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCustomer.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCustomer.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If remissionOutput IsNot Nothing AndAlso remissionOutput.Status < 3 Then
            If ValidateControls() = True Then
                If INDGvProduct.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MRemissionOutput(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveRemissionOutput(remissionOutput, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If remissionOutput.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If remissionOutput.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    remissionOutput = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, remissionOutput.Id, 0, remissionOutput.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, remissionOutput.Id, 0, remissionOutput.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, remissionOutput.Id, 0, remissionOutput.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If remissionOutput.Id > 0 Then
                        remissionOutput = model.GetRemissionOutputById(remissionOutput.Id)
                    Else
                        remissionOutput = New RemissionOutput
                    End If

                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewRemissionOutput()
        End If
    End Sub

    Private Async Sub SaveOrUpdateAndConfirm(actions As Integer)
        If ValidateControls() = True Then
            If INDGvProduct.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddRemissionDetail", MODULE_NAME)
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MRemissionOutput(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmRemissionOutput(remissionOutput, _idCurrentSequence, actions, Me._sequence)
                If result.StateResult = True And result.StateResultAux = True Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                    End If
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    remissionOutput = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If result.MessageResultAux IsNot Nothing AndAlso result.MessageResultAux.Count > 0 Then
                        ViewMessageValidationStock(result.MessageResultAux)
                    End If
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, remissionOutput.Id, 0, remissionOutput.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InventorySequenceDetail(0).Id).RemoveAt(0)
                    End If
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    remissionOutput = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, remissionOutput.Id, 0, remissionOutput.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If result.MessageResult IsNot Nothing Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If remissionOutput.Id > 0 Then
                        remissionOutput = Await model.GetRemissionOutputByCode(Code)
                    Else
                        remissionOutput = New RemissionOutput
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If

            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDDteDate.Properties.MinValue = dateMin
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Cliente", .FieldName = "CustomerId.NitName", .ColumnWidth = 300},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "RemissionDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado Producto", .FieldName = "ProductStatusName", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllRemissionOutput
            .FiltroBusqueda = BarraBotones.OperatingUnitValue.ToString()
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
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Private Sub CleanControls()
        INDLcRemissionOutput.BeginUpdate()
        ReadOnlyControls(False)
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()
        Code = String.Empty
        RemissionDate = GetDateServer()
        CustomerId = Nothing
        INDSleCustomer.Properties.NullText = String.Empty
        WarehouseId = Nothing
        INDSleWarehouse.Properties.NullText = String.Empty
        INDSleWarehouse.Properties.ReadOnly = False
        Description = String.Empty
        listRemissionOutputDetail = Nothing
        listRemissionOutputDetailDelete = Nothing
        remissionOutput = Nothing
        remissionOutputDetail = Nothing
        INDGcProduct.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcProduct)
        INDLcRemissionOutput.EndUpdate()
        ActionsOnControls = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Private Sub AssigningValues()
        With remissionOutput
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .RemissionDate = RemissionDate
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .CustomerId = CustomerId
            .WarehouseId = WarehouseId
            .Description = Description
            .Status = 1
            .Prefix = Me._prefixSelected
            .ProductStatus = 1
            For Each item In listRemissionOutputDetail
                .RemissionOutputDetail.Add(item)
            Next
            If listRemissionOutputDetailDelete IsNot Nothing AndAlso listRemissionOutputDetailDelete.Count > 0 Then
                For Each item In listRemissionOutputDetailDelete
                    .RemissionOutputDetail.Add(item)
                Next
            End If
        End With
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MRemissionOutput(CStr(Me.Tag))
                    AsyncLoader(True)
                    remissionOutput = Await Model.GetRemissionOutputByCode(INDBteCode.Text.Trim)
                    INDLcRemissionOutput.BeginUpdate()
                    If remissionOutput IsNot Nothing AndAlso remissionOutput.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(remissionOutput.Id))
                            listRemissionOutputDetail = Model.GetRemissionOutputDetailByRemissionOutputId(remissionOutput.Id)
                            INDGcProduct.DataSource = Nothing
                            INDGcProduct.DataSource = listRemissionOutputDetail
                            With remissionOutput
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                If .Status <> 1 Then
                                    INDDteDate.Properties.MinValue = .RemissionDate
                                End If

                                Code = .Code
                                RemissionDate = .RemissionDate
                                CustomerId = .CustomerId
                                INDSleCustomer.Properties.NullText = .CodeNameCustomer
                                WarehouseId = .WarehouseId
                                INDSleWarehouse.Properties.NullText = .CodeNameWareHouse
                                Description = .Description
                                BarraBotones.StatusRecord = .Status.ToString()
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.remissionOutput.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = remissionOutput.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(remissionOutput.Id, Me.Tag.ToString(), Nothing, GetType(RemissionOutput).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, remissionOutput.Id, 0, remissionOutput.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If remissionOutput.Status = 1 Then
                                INDBtnAdd.Enabled = True
                            End If
                            INDDteDate.Focus()
                            Me.GetDocumentIndexed(MyTag & "_" & remissionOutput.Code)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRemissionOutput()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLcRemissionOutput.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If

        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Using model As New MRemissionOutput(MyTag)
            AsyncLoader(True)
            INDLcRemissionOutput.BeginUpdate()
            remissionOutput = Await model.GetRemissionOutputByCode(Code)
            If remissionOutput IsNot Nothing AndAlso remissionOutput.Id > 0 Then
                listRemissionOutputDetail = model.GetRemissionOutputDetailByRemissionOutputId(remissionOutput.Id)
                INDGcProduct.DataSource = Nothing
                INDGcProduct.DataSource = listRemissionOutputDetail
                With remissionOutput
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                    Me.BarraBotones.StatusRecordVisible = True
                    BarraBotones.OperatingUnitValue = .OperatingUnitId
                    Select Case .Status
                        Case 1
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                        Case Else
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            ReadOnlyControls(True)
                    End Select
                    Code = .Code
                    RemissionDate = .RemissionDate
                    CustomerId = .CustomerId
                    INDSleCustomer.Properties.NullText = .CodeNameCustomer
                    WarehouseId = .WarehouseId
                    INDSleWarehouse.Properties.NullText = .CodeNameWareHouse
                    Description = .Description
                    BarraBotones.StatusRecord = .Status.ToString()
                End With
                BarraBotones.SetDocuments(remissionOutput.Id)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, remissionOutput.Id, 0, remissionOutput.Id)
                AsyncLoader(False)
                ActionsOnControls = True
                If remissionOutput.Status = 1 Then
                    INDBtnAdd.Enabled = True
                End If
                INDDteDate.Focus()
                Me.GetDocumentIndexed(MyTag & "_" & remissionOutput.Code)
                GenerateBlockRecord()
            Else
                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                'Code = String.Empty
                'INDBteCode.Focus()
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewRemissionOutput()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Me.Code = String.Empty
                    Deshacer()
                    INDBteCode.Focus()
                End If
            End If
        End Using
        INDLcRemissionOutput.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvProduct, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvProduct.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.remissionOutput.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.remissionOutput.Id}
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
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewRemissionOutput() As Task
        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If

        remissionOutput = New RemissionOutput()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
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
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"

        'Me.remissionOutput = New RemissionOutput()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = 0
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        '    BarraBotones.StatusRecordVisible = True
        '    BarraBotones.StatusRecord = "1"
        'End If
    End Function

    Private Sub ReturnAddRemissionOutputDetail(sender As Object, e As AddRemissionOutputDetailEventArgs)
        If listRemissionOutputDetail Is Nothing Then
            INDSleWarehouse.Properties.ReadOnly = True
            listRemissionOutputDetail = New List(Of RemissionOutputDetail)
        End If
        If e.EditMode = True Then
            listRemissionOutputDetail.Remove(remissionOutputDetail)
            listRemissionOutputDetail.Insert(indexEditRecord, e.RemissionOutputDetail)
        Else
            listRemissionOutputDetail.Add(e.RemissionOutputDetail)
        End If
        INDGcProduct.DataSource = Nothing
        INDGcProduct.DataSource = listRemissionOutputDetail
    End Sub

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmReferralOut_IndexContent", MODULE_NAME), remissionOutput.Code, If(INDSleCustomer.Text = String.Empty, INDSleCustomer.Properties.NullText, INDSleCustomer.Text), RemissionDate, If(INDSleWarehouse.Text = String.Empty, INDSleWarehouse.Properties.NullText, INDSleWarehouse.Text))
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.remissionOutput.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.remissionOutput.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.remissionOutput.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog(Me)
    End Sub

    ''' <summary>
    ''' Metodo para mostar un pop up con los mensajes de validacion por stock
    ''' </summary>
    ''' <param name="messagesValidationStock"></param>
    ''' <remarks></remarks>
    Private Sub ViewMessageValidationStock(messagesValidationStock As List(Of String))
        Using formulario As New FrmPopUpValidateStock
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Datasource = messagesValidationStock
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "HANDLES"
#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        indexEditRecord = Nothing
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        _prefixSelected = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        presenter = Nothing
        remissionOutput = Nothing
        remissionOutputDetail = Nothing
        listRemissionOutputDetail = Nothing
        listRemissionOutputDetailDelete = Nothing
        varImp = Nothing
    End Sub

    Private Async Sub FrmReferralOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRemissionOutput, True)

        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PRemissionOutput(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        AddActionsColumns()
        Deshacer()
        LoadStatus()
        IndigoGridControl1.RefreshGrid(INDGcProduct)
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmReferralOut_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmReferralOut_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        Using formulario As New FrmPopupProductOutput
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddRemissionOutputDetail, AddressOf ReturnAddRemissionOutputDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.WareHouseId = WarehouseId
            formulario.operatingUnitId = BarraBotones.OperatingUnitValue
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDSleWarehouse_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWarehouse.EditValueChanged
        If WarehouseId IsNot Nothing Then
            INDBtnAdd.Enabled = True
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvWarehouse.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvWarehouse.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.remissionOutput.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.remissionOutput.Prefix)
            End If
        End If
    End Sub

#End Region

#Region "KeyDown"
    Private Sub INDMeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAdd.Focus()
        End If
    End Sub

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewRemissionOutput()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleWarehouse_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWarehouse.QueryPopUp
        If INDSleWarehouse.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If WarehouseXPO Is Nothing Then
            Using model As New MRemissionOutput(MyTag)
                WarehouseXPO = model.ListWarehouse()
            End Using
        End If
    End Sub

    Private Sub INDSleCustomer_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCustomer.QueryPopUp
        If CustomerXPO Is Nothing Then
            Using model As New MRemissionOutput(MyTag)
                CustomerXPO = model.ListCustomer()
            End Using
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        remissionOutputDetail = DirectCast(INDGvProduct.GetFocusedRow(), RemissionOutputDetail)
        Select Case button.Tag.ToString
            Case "Edit"
                indexEditRecord = listRemissionOutputDetail.IndexOf(remissionOutputDetail)
                Using formulario As New FrmPopupProductOutput
                    Me.Cursor = ChangeCursorIndigo()
                    AddHandler formulario.AddRemissionOutputDetail, AddressOf ReturnAddRemissionOutputDetail
                    formulario.Size = New Drawing.Size(800, 730)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.RemissionOutputDetailEdit = remissionOutputDetail
                    formulario.EditMode = True
                    formulario.WareHouseId = WarehouseId
                    formulario.operatingUnitId = BarraBotones.OperatingUnitValue
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If remissionOutputDetail.Id > 0 Then
                        If listRemissionOutputDetailDelete Is Nothing Then
                            listRemissionOutputDetailDelete = New List(Of RemissionOutputDetail)
                        End If
                        While remissionOutputDetail.RemissionOutputDetailPhysical.Count > 0
                            If remissionOutputDetail.RemissionOutputDetailPhysical(0).Id > 0 Then
                                remissionOutputDetail.RemissionOutputDetailPhysical(0).MarkAsDeleted()
                            Else
                                remissionOutputDetail.RemissionOutputDetailPhysical.Remove(remissionOutputDetail.RemissionOutputDetailPhysical(0))
                            End If
                        End While
                        remissionOutputDetail.MarkAsDeleted()
                        listRemissionOutputDetailDelete.Add(remissionOutputDetail)
                    End If
                    listRemissionOutputDetail.Remove(remissionOutputDetail)
                    If listRemissionOutputDetail.Count = 0 AndAlso remissionOutput.Id = 0 Then
                        INDSleWarehouse.Properties.ReadOnly = False
                    End If
                    INDGcProduct.DataSource = Nothing
                    INDGcProduct.DataSource = listRemissionOutputDetail

                End If
        End Select
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDSleCustomer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCustomer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCustomers
                OpenFormDialog(form)
                CustomerXPO = Nothing
                INDSleCustomer.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmStores
                OpenFormDialog(form)
                WarehouseXPO = Nothing
                INDSleWarehouse.Focus()
            End Using
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.remissionOutput IsNot Nothing AndAlso Me.remissionOutput.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReferralOut_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDBteCode.Focus()
        INDDteDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"

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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        varImp = 1
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, remissionOutput.Id, 0, remissionOutput.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            remissionOutput.Status = 3
            varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2)
    End Sub
#End Region

End Class