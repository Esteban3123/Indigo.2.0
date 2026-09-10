'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 18-05-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing.MVP
Imports Presentation.Controls

#End Region

Public Class FrmProductAndServiceFee
    Implements IProductAndServiceFee, ICustomizableForm

#Region "Constants"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Billing"

#End Region

#Region "Properties and Variables"

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private listErrosImportFile As List(Of String())

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing

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
    ''' variable que contiene la entidad
    ''' </summary>
    Private _productAndServiceFee As ProductAndServiceFee

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PProductAndServiceFee

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    ''' <summary>
    ''' Listado de eliminados de los productos
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteProductFee As List(Of ProductFeeDetail)

    ''' <summary>
    ''' Listado de eliminados de los productos
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteServiceFee As List(Of ServiceFeeDetail)

    ''' <summary>
    ''' Listado de eliminados de los usuarios autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteProductAndServiceFeeUser As List(Of ProductAndServiceFeeUser)

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersXpo As UserXpo

    ''' <summary>
    ''' Posición del detalle que se va a editar
    ''' </summary>
    Private indexOfDetail As Integer

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Private totalProcessedItems As Integer = 0

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 300

    ''' <summary>
    ''' Listado de productos importados
    ''' </summary>
    Public Property ListServices As List(Of ServiceFeeDetail)

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IProductAndServiceFee.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IProductAndServiceFee.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Public Property Sequence As BillingSequence Implements IProductAndServiceFee.Sequense
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

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    Public Property Code As String Implements IProductAndServiceFee.Code
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
    ''' Obtiene o establece el nombre del cubrimiento
    ''' </summary>
    Public Property Name As String Implements IProductAndServiceFee.Name
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IProductAndServiceFee.Status
        Get
            Return BarraBotones.StatusRecord
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
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IProductAndServiceFee.UserXpo
        Get
            Return CType(INDsleUsers.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUsers.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Methods"

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(14)})
                                              End SyncLock
                                          End Sub)
    End Sub

    Private Sub ReturnAddProductFeeDetail(sender As Object, e As AddProductFeeEventArgs)
        If e.EditMode Then
            Me._productAndServiceFee.ProductFeeDetail.RemoveAt(indexOfDetail)
            Me._productAndServiceFee.ProductFeeDetail.Insert(indexOfDetail, e.ProductFeeDetail)
        Else
            Me._productAndServiceFee.ProductFeeDetail.Add(e.ProductFeeDetail)
        End If

        INDgcProduct.DataSource = Nothing
        INDgcProduct.DataSource = _productAndServiceFee.ProductFeeDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
    End Sub

    Private Sub ReturnAddServicesFeeDetail(sender As Object, e As AddServiceFeeEventArgs)
        If e.EditMode Then
            Me._productAndServiceFee.ServiceFeeDetail.RemoveAt(indexOfDetail)
            Me._productAndServiceFee.ServiceFeeDetail.Insert(indexOfDetail, e.ServiceFeeDetail)
        Else
            Me._productAndServiceFee.ServiceFeeDetail.Add(e.ServiceFeeDetail)
        End If

        INDgcServices.DataSource = Nothing
        INDgcServices.DataSource = _productAndServiceFee.ServiceFeeDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
    End Sub

    ''' <summary>
    ''' News the ProductAndServiceFee.
    ''' </summary>
    Private Async Function NewProductAndServiceFee() As Task
        _productAndServiceFee = New ProductAndServiceFee() With {.Status = True}
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

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProductAndServicesFee
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
        Await DeleteBlockedRecord()
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
    Public Async Function DeleteBlockedRecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

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
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productAndServiceFee.Code, Me._productAndServiceFee.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._productAndServiceFee.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productAndServiceFee.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productAndServiceFee.Code, Me._productAndServiceFee.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productAndServiceFee.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using Model As New MProductAndServiceFee(CStr(Me.Tag))
                    AsyncLoader(True)
                    INDlcRoot.BeginUpdate()

                    _productAndServiceFee = Await Model.GetProductAndServiceFeeByCode(Me.Code)
                    If _productAndServiceFee IsNot Nothing AndAlso _productAndServiceFee.Id > 0 Then

                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_productAndServiceFee.Id))

                            With _productAndServiceFee
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Name = .Name
                                Status = .Status

                                INDgcProduct.DataSource = Nothing
                                INDgcProduct.DataSource = _productAndServiceFee.ProductFeeDetail.ToList()

                                INDgcServices.DataSource = Nothing
                                INDgcServices.DataSource = _productAndServiceFee.ServiceFeeDetail.ToList()

                                INDgcUsers.DataSource = Nothing
                                INDgcUsers.DataSource = _productAndServiceFee.ProductAndServiceFeeUser.ToList()
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._productAndServiceFee.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _productAndServiceFee.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_productAndServiceFee.Id, Me.Tag.ToString(), Nothing, GetType(ProductRate).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await NewProductAndServiceFee()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProductAndServiceFee.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDbtnAddProduct.Enabled = value
            INDbtnAddServices.Enabled = value
            INDbtnAddUser.Enabled = value
            INDgcProduct.Enabled = value
            INDgcServices.Enabled = value
            INDgcUsers.Enabled = value
            INDsleUsers.Enabled = value
            INDEsbProductRate.Enabled = value
            INDBtnImportFileProducts.Enabled = value
            INDEsbServiceRate.Enabled = value
            INDBtnImportFileServices.Enabled = value

            INDlcRoot.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property


    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Async Sub CleanControls()
        INDlcgRoot.BeginUpdate()

        ActionsOnControls = False
        Await DeleteBlockedRecord()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        Code = String.Empty
        Name = Nothing
        INDgcProduct.DataSource = Nothing
        INDgcServices.DataSource = Nothing
        INDgcUsers.DataSource = Nothing
        ListDeleteProductFee = Nothing
        ListDeleteProductAndServiceFeeUser = Nothing
        INDsleUsers.Enabled = Nothing
        _productAndServiceFee = Nothing

        INDlcgRoot.EndUpdate()

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
        With _productAndServiceFee

            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name

            If ListDeleteProductFee IsNot Nothing AndAlso ListDeleteProductFee.Count > 0 Then
                ListDeleteProductFee.ForEach(Sub(item)
                                                 .ProductFeeDetail.Add(item)
                                             End Sub)
            End If

            If ListDeleteServiceFee IsNot Nothing AndAlso ListDeleteServiceFee.Count > 0 Then
                ListDeleteServiceFee.ForEach(Sub(item)
                                                 .ServiceFeeDetail.Add(item)
                                             End Sub)
            End If

            If ListDeleteProductAndServiceFeeUser IsNot Nothing AndAlso ListDeleteProductAndServiceFeeUser.Count > 0 Then
                ListDeleteProductAndServiceFeeUser.ForEach(Sub(item)
                                                               .ProductAndServiceFeeUser.Add(item)
                                                           End Sub)
            End If

        End With
    End Sub

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

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        If INDgvProducts.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo una tarifa de producto"
            Exit Sub
        End If

        If INDgvServices.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo una tarifa de servicios"
            Exit Sub
        End If

        If viewUsersGrid.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo un usuario autorizado"
            Exit Sub
        End If

        AssigningValues()

        Try
            Using Model As New MProductAndServiceFee(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ProductAndServiceFee) = Await Model.SaveProductAndServiceFee(Me._productAndServiceFee)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _productAndServiceFee.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._productAndServiceFee = result.ObjectEmbbeded
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
    ''' cambia el estado del registro
    ''' </summary>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me._productAndServiceFee.Code) Then
            Try
                Using model As New MProductAndServiceFee(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me._productAndServiceFee.Status
                    Dim result As ActionResult(Of ProductAndServiceFee) = Await model.UpdateStateProductAndServiceFee(Me._productAndServiceFee.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._productAndServiceFee = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
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
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewProductAndServiceFee()
        End If
    End Sub
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _presenter = Nothing
        _record = Nothing
        ListDeleteProductFee = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmProductAndServiceFee control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductAndServiceFee_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDEsbProductRate.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Código Producto"},
                            New ExcelColumn With {.Name = "Tipo Tarifa", .Comment = "1.Tarifa Fija , 2.Porcentaje"},
                            New ExcelColumn With {.Name = "Tipo Porcentaje", .Comment = "1.Costo Promedio Ponderado , 2.Ultimo Costo"},
                            New ExcelColumn With {.Name = "Fecha Inicial"},
                            New ExcelColumn With {.Name = "Fecha Final"},
                            New ExcelColumn With {.Name = "Porcentaje", .Comment = "Se debe diligenciar sin el signo %"},
                            New ExcelColumn With {.Name = "Precio de Venta"},
                            New ExcelColumn With {.Name = "Observaciones"}
                        }
                    })

        INDEsbServiceRate.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Código Servicio"},
                            New ExcelColumn With {.Name = "Fecha Inicial"},
                            New ExcelColumn With {.Name = "Fecha Final"},
                            New ExcelColumn With {.Name = "Precio de Venta"},
                            New ExcelColumn With {.Name = "Observaciones"}
                        }
                    })

        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        _presenter = New PProductAndServiceFee(Me)
        _presenter.GetSequence()

        SetListActions()
        LoadStatus()
        Deshacer()
    End Sub

    Private Sub SetListActions()

        Dim listActions As New List(Of eAcciones)
        listActions.Add(eAcciones.Remove)
        listActions.Add(eAcciones.Edit)

        IndigoGridView1.SetListAcction(INDgvProducts, listActions)
        IndigoGridView2.SetListAcction(INDgvServices, listActions)
        IndigoGridView3.SetListAcction(viewUsersGrid, {eAcciones.Remove}.ToList)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvServices.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewUsersGrid.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub
#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._productAndServiceFee IsNot Nothing AndAlso Me._productAndServiceFee.Id > 0 Then

            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal

            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If

        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmProductCoverage control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmProductAndServiceFee_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
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
                    Await LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewProductAndServiceFee()
                Else
                    Await LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions

        Dim ProductFeeDetail = DirectCast(INDgvProducts.GetFocusedRow(), ProductFeeDetail)

        Select Case (sender.tag)
            Case "Edit"
                indexOfDetail = _productAndServiceFee.ProductFeeDetail.IndexOf(ProductFeeDetail)
                Using formulario As New FrmPopUpProductFeeDetail
                    Me.Cursor = BaseClass.ChangeCursorIndigo()
                    formulario.Size = New System.Drawing.Size(1200, 730)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    formulario.EditMode = True
                    formulario._productFeeDetail = ProductFeeDetail
                    formulario.ListProductFeeDetails = _productAndServiceFee.ProductFeeDetail.ToList()
                    AddHandler formulario.AddProductFeeDetail, AddressOf ReturnAddProductFeeDetail
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using

            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If ListDeleteProductFee Is Nothing Then
                        ListDeleteProductFee = New List(Of ProductFeeDetail)
                    End If

                    ProductFeeDetail.MarkAsDeleted
                    ListDeleteProductFee.Add(ProductFeeDetail)

                    INDgcProduct.DataSource = Nothing
                    INDgcProduct.DataSource = _productAndServiceFee.ProductFeeDetail.ToList()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions

        Dim ServiceFeeDetail = DirectCast(INDgvServices.GetFocusedRow(), ServiceFeeDetail)

        Select Case (sender.tag)
            Case "Edit"
                indexOfDetail = _productAndServiceFee.ServiceFeeDetail.IndexOf(ServiceFeeDetail)
                Using formulario As New FrmPopUpServicesFeeDetail
                    Me.Cursor = BaseClass.ChangeCursorIndigo()
                    formulario.Size = New System.Drawing.Size(1200, 730)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    formulario.EditMode = True
                    formulario._serviceFeeDetail = ServiceFeeDetail
                    formulario.ListServiceFeeDetail = _productAndServiceFee.ServiceFeeDetail.ToList()
                    AddHandler formulario.AddServicesFeeDetail, AddressOf ReturnAddServicesFeeDetail
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using

            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If ListDeleteServiceFee Is Nothing Then
                        ListDeleteServiceFee = New List(Of ServiceFeeDetail)
                    End If

                    ServiceFeeDetail.MarkAsDeleted
                    ListDeleteServiceFee.Add(ServiceFeeDetail)

                    INDgcServices.DataSource = Nothing
                    INDgcServices.DataSource = _productAndServiceFee.ServiceFeeDetail.ToList()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        Dim ProductAndServiceFeeUser = CType(viewUsersGrid.GetFocusedRow, ProductAndServiceFeeUser)
        If ListDeleteProductAndServiceFeeUser Is Nothing Then
            ListDeleteProductAndServiceFeeUser = New List(Of ProductAndServiceFeeUser)
        End If

        ProductAndServiceFeeUser.MarkAsDeleted()
        ListDeleteProductAndServiceFeeUser.Add(ProductAndServiceFeeUser)

        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = _productAndServiceFee.ProductAndServiceFeeUser.ToList()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the INDsbAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnAddProduct_Click(sender As Object, e As EventArgs) Handles INDbtnAddProduct.Click
        Using formulario As New FrmPopUpProductFeeDetail
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(1200, 730)
            formulario.ListProductFeeDetails = _productAndServiceFee.ProductFeeDetail.ToList()
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            AddHandler formulario.AddProductFeeDetail, AddressOf ReturnAddProductFeeDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnAddServices_Click(sender As Object, e As EventArgs) Handles INDbtnAddServices.Click
        Using formulario As New FrmPopUpServicesFeeDetail
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(1200, 730)
            formulario.ListServiceFeeDetail = _productAndServiceFee.ServiceFeeDetail.ToList()
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            AddHandler formulario.AddServicesFeeDetail, AddressOf ReturnAddServicesFeeDetail
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAddUser.Click
        If INDsleUsers.EditValue IsNot Nothing Then
            If _productAndServiceFee.ProductAndServiceFeeUser.Any(Function(x) x.UserId = INDsleUsers.EditValue) Then
                Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra agregado"
                Exit Sub
            End If

            _productAndServiceFee.ProductAndServiceFeeUser.Add(New ProductAndServiceFeeUser _
                With {
                        .UserId = _usersXpo.Id,
                        .UserCode = _usersXpo.UserCode,
                        .FullNameUser = _usersXpo.IdPerson.Fullname
                     })

            INDgcUsers.DataSource = Nothing
            INDgcUsers.DataSource = _productAndServiceFee.ProductAndServiceFeeUser.ToList()
            Mensaje(EeventViewerImages.Informacion) = "Usuario agregado exitosamente"
            INDsleUsers.EditValue = Nothing
            INDsleUsers.Focus()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un usuario"
            INDsleUsers.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsers.QueryPopUp
        If INDsleUsers.Properties.DataSource Is Nothing Then
            _presenter.InitializeUsers()
        End If
    End Sub

    Private Sub INDsleUsers_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUsers.EditValueChanged
        If INDsleUsers.EditValue IsNot Nothing Then
            _usersXpo = DirectCast(DirectCast(viewUserSearch.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, UserXpo)
        End If
    End Sub

    Private Async Sub INDbtnImportFileServices_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileServices.Click
        Await ImportFileServices()
    End Sub

    Private Async Function ImportFileServices() As Task

        If _productAndServiceFee.Id > 0 AndAlso _productAndServiceFee.Status = False Then
            Mensaje(EeventViewerImages.Advertencia) = "La tarifa esta inactiva"
            Exit Function
        End If

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If

        AsyncLoader(True)
        Try
            'obtengo la rura del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            AsyncLoader(True)
            Await LoadImportFileServices()

            If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"El archivo presento error en {listErrosImportFile.Count} registros "

                Dim ErrorsExcel As New SpreadsheetControl
                ErrorsExcel.CreateNewDocument()
                ErrorsExcel.Document.Worksheets.ActiveWorksheet = ErrorsExcel.Document.Worksheets(0)
                Dim worksheet As Worksheet = ErrorsExcel.Document.Worksheets.ActiveWorksheet

                worksheet.Cells(0, 0).Value = "Codigo Servicio"
                worksheet.Cells(0, 3).Value = "Fecha Inicial"
                worksheet.Cells(0, 4).Value = "Fecha Final"
                worksheet.Cells(0, 6).Value = "Precio de Venta"
                worksheet.Cells(0, 7).Value = "Observaciones"
                worksheet.DefaultColumnWidth = 250

                Dim rows = 1
                For Each dato As String() In listErrosImportFile
                    Dim Columns = 0
                    For Each item In dato
                        worksheet.Cells(rows, Columns).Value = item
                        Columns += 1
                    Next
                    rows += 1
                Next

                Dim fileName As String = System.IO.Path.GetTempPath() & INDtxtName.EditValue & ".xlsx"
                ErrorsExcel.SaveDocument(fileName)
                System.Diagnostics.Process.Start(fileName)
            Else
                INDgcServices.DataSource = Nothing
                INDgcServices.DataSource = _productAndServiceFee.ServiceFeeDetail.ToList()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
            AsyncLoader(False)
        End Try
        AsyncLoader(False)
    End Function

    Private Function LoadImportFileServices() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String())
                                         Dim ssc = New SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If

                                         Dim indexSend = 0
                                         totalProcessedItems = 0
                                         Dim totalItems = rows.LastUsedIndex
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)

                                             Using model As New MProductAndServiceFee(Me.Tag.ToString())
                                                 Dim result = model.SetServicesFeeDetailFromFile(listRows.ToList())
                                                 If result.ListMessageResult IsNot Nothing AndAlso result.ListMessageResult.Any() Then
                                                     listErrosImportFile.AddRange(result.ListMessageResult)
                                                 End If
                                                 If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                                                     For Each item In result.ObjectEmbbeded
                                                         _productAndServiceFee.ServiceFeeDetail.Add(item)
                                                     Next
                                                 End If
                                             End Using
                                         End While
                                     End Sub)
    End Function

    Private Async Sub INDBtnImportFileProducts_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileProducts.Click
        Await ImportFile()
    End Sub

    Private Async Function ImportFile() As Task

        If _productAndServiceFee.Id > 0 AndAlso _productAndServiceFee.Status = False Then
            Mensaje(EeventViewerImages.Advertencia) = "La tarifa esta inactiva"
            Exit Function
        End If

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If

        AsyncLoader(True)
        Try
            'obtengo la rura del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            AsyncLoader(True)
            Await LoadImportFileProducts()

            If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"El archivo presento error en {listErrosImportFile.Count} registros"

                Dim ErrorsExcel As New SpreadsheetControl
                ErrorsExcel.CreateNewDocument()
                ErrorsExcel.Document.Worksheets.ActiveWorksheet = ErrorsExcel.Document.Worksheets(0)
                Dim worksheet As Worksheet = ErrorsExcel.Document.Worksheets.ActiveWorksheet

                worksheet.Cells(0, 0).Value = "Codigo Producto"
                worksheet.Cells(0, 1).Value = "Tipo de tarifa"
                worksheet.Cells(0, 2).Value = "Tipo de Promedio"
                worksheet.Cells(0, 3).Value = "Fecha Inicial"
                worksheet.Cells(0, 4).Value = "Fecha Final"
                worksheet.Cells(0, 5).Value = "Porcentaje"
                worksheet.Cells(0, 6).Value = "Precio de Venta"
                worksheet.Cells(0, 7).Value = "Observaciones"
                worksheet.DefaultColumnWidth = 250

                Dim rows = 1
                For Each dato As String() In listErrosImportFile
                    Dim Columns = 0
                    For Each item In dato
                        worksheet.Cells(rows, Columns).Value = item
                        Columns += 1
                    Next
                    rows += 1
                Next

                Dim fileName As String = System.IO.Path.GetTempPath() & INDtxtName.EditValue & ".xlsx"
                ErrorsExcel.SaveDocument(fileName)
                System.Diagnostics.Process.Start(fileName)
            Else
                INDgcProduct.DataSource = Nothing
                INDgcProduct.DataSource = _productAndServiceFee.ProductFeeDetail.ToList()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
            AsyncLoader(False)
        End Try
        AsyncLoader(False)
    End Function

    Private Function LoadImportFileProducts() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String())
                                         Dim ssc = New SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If

                                         Dim indexSend = 0
                                         Dim totalItems = rows.LastUsedIndex
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)

                                             Using model As New MProductAndServiceFee(Me.Tag.ToString())
                                                 Dim result = model.SetProductFeeDetailFromFile(listRows.ToList())
                                                 If result.ListMessageResult IsNot Nothing AndAlso result.ListMessageResult.Any() Then
                                                     listErrosImportFile.AddRange(result.ListMessageResult)
                                                 End If
                                                 If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                                                     For Each item In result.ObjectEmbbeded
                                                         _productAndServiceFee.ProductFeeDetail.Add(item)
                                                     Next
                                                 End If
                                             End Using
                                         End While
                                     End Sub)
    End Function

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductCoverage_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteCode.Focus()
    End Sub

#End Region

#Region "CopyPaste"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If sender.Equals(INDgcProduct) Then
            If _productAndServiceFee.Id > 0 AndAlso Not _productAndServiceFee.Status Then
                Mensaje(EeventViewerImages.Advertencia) = "La tarifa esta inactiva"
                Exit Sub
            End If

            If e.Rows(0).Item(0).Contains("Código Producto") Then
                e.Rows.Remove(e.Rows.ElementAt(0))
            End If

            AsyncLoader(True)
            Using Model As New MProductAndServiceFee(Me.Tag.ToString())
                Dim result = Await Model.SetProductFeeDetailFromCopyandPaste(e.Rows)

                'si ocurrio un error
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each item In result.ObjectEmbbeded
                        _productAndServiceFee.ProductFeeDetail.Add(item)
                    Next
                End If

                INDgcProduct.DataSource = Nothing
                INDgcProduct.DataSource = _productAndServiceFee.ProductFeeDetail.ToList()
            End Using
            AsyncLoader(False)
        End If
    End Sub

    Private Async Sub IndigoGridControl2_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl2.PasteToGrid
        If sender.Equals(INDgcServices) Then
            If _productAndServiceFee.Id > 0 AndAlso Not _productAndServiceFee.Status Then
                Mensaje(EeventViewerImages.Advertencia) = "La tarifa esta inactiva"
                Exit Sub
            End If

            If e.Rows(0).Item(0).Contains("Código Servicio") Then
                e.Rows.Remove(e.Rows.ElementAt(0))
            End If

            AsyncLoader(True)
            Using Model As New MProductAndServiceFee(Me.Tag.ToString())
                Dim result = Await Model.SetServicesFeeDetailFromCopyandPaste(e.Rows)

                'si ocurrio un error
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each item In result.ObjectEmbbeded
                        _productAndServiceFee.ServiceFeeDetail.Add(item)
                    Next
                End If

                INDgcServices.DataSource = Nothing
                INDgcServices.DataSource = _productAndServiceFee.ServiceFeeDetail.ToList()
            End Using
            AsyncLoader(False)
        End If
    End Sub
#End Region

#End Region

#Region "BarButtonEvents"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
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

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub
#End Region

End Class

Public Class AddProductFeeEventArgs
    Inherits EventArgs

    Property ProductFeeDetail As ProductFeeDetail

    Property EditMode As Boolean

End Class

Public Class AddServiceFeeEventArgs
    Inherits EventArgs

    Property ServiceFeeDetail As ServiceFeeDetail

    Property EditMode As Boolean

End Class