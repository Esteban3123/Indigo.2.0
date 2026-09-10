'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19/05/2016
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
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Inventory.MVP
Imports Presentation.Payroll
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports System.Windows.Forms
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmPurchaseOrderDevolution
    Implements IPurchaseOrderDevolution, ICustomizableForm

#Region "GLOBALS"
    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PPurchaseOrderDevolution

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Representa la entidad de devolucion de ordenes de compra
    ''' </summary>
    ''' <remarks></remarks>
    Private _purchaseOrderDevolution As PurchaseOrderDevolution
    ''' <summary>
    ''' Listado de los detalles agregados
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPurchaseOrderDevolutionDetail As List(Of PurchaseOrderDevolutionDetail)
    
    ''' <summary>
    ''' The list purchase order devolution detail delete
    ''' </summary>
    Dim listPurchaseOrderDevolutionDetailDelete As List(Of PurchaseOrderDevolutionDetail)
#End Region

#Region "PROPERTIES"
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IPurchaseOrderDevolution.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDSleSupplier.Enabled = value
            INDDteDocumentDate.Enabled = value
            INDMeDetail.Enabled = value
            INDBtnAddDevolution.Enabled = False
            INDGcDevolutions.Enabled = value
            If value Then
                INDSleSupplier.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    Public Property Code As String Implements IPurchaseOrderDevolution.Code
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

    Public Property Description As String Implements IPurchaseOrderDevolution.Description
        Get
            Return INDMeDetail.EditValue
        End Get
        Set(value As String)
            INDMeDetail.EditValue = value
        End Set
    End Property

    Public Property DocumentDate As Date? Implements IPurchaseOrderDevolution.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPurchaseOrderDevolution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IPurchaseOrderDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As InventorySequence Implements IPurchaseOrderDevolution.Sequense
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

            'Me.DicSequense.Clear()
            'For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
            '    Me.DicSequense.Add(seq.Id, New List(Of String)())
            'Next
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Establece el Id del Proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierId As Integer Implements IPurchaseOrderDevolution.SupplierId
        Get
            Return INDSleSupplier.EditValue
        End Get
        Set(value As Integer)
            INDSleSupplier.EditValue = value
        End Set
    End Property
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        If listPurchaseOrderDevolutionDetail Is Nothing OrElse listPurchaseOrderDevolutionDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se deve agregar minimo un detalle para la devolución"
            Exit Sub
        End If
        If _purchaseOrderDevolution.Status < 3 Then
            AssigningValues()
        End If
        Using model As New MPurchaseOrderDevolution(MyTag)
            AsyncLoader(True)
            Dim result = Await model.SavePurchaseOrderDevolution(_purchaseOrderDevolution, _idCurrentSequence)
            Select Case result.StatusCode
                Case eStatusResult.SUCCESS
                    If _purchaseOrderDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            If _purchaseOrderDevolution.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf _purchaseOrderDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If _purchaseOrderDevolution.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf _purchaseOrderDevolution.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                        ElseIf _purchaseOrderDevolution.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me._purchaseOrderDevolution = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Case eStatusResult.WARNING
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    If _purchaseOrderDevolution.Id > 0 Then
                        _purchaseOrderDevolution = Await model.GetPurchaseOrderDevolutionById(_purchaseOrderDevolution.Id)
                    Else
                        _purchaseOrderDevolution = New PurchaseOrderDevolution
                    End If
                Case eStatusResult.EXCEPTION
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    AsyncLoader(False)
                    Me.Deshacer()
            End Select
           
        End Using
        
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPurchaseOrderDevolution()
        End If
    End Sub
#End Region

#Region "METHODS"
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo() With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus}}.ToList()
            .ValorSolicitado = "Code"

            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllInventoryPurchaserOrderDevolution
            '.FiltroBusqueda = BarraBotones.OperatingUnitValue.ToString()
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPurchaseOrderDevolution() As Task
        Me._purchaseOrderDevolution = New PurchaseOrderDevolution
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


        'If Sequense IsNot Nothing AndAlso Sequense.Id > 0 Then
        '    Me._purchaseOrderDevolution = New PurchaseOrderDevolution
        '    If Me._sequence.IsManual Then
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    Else
        '        If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '            Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
        '        ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '            If Me._sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                Exit Sub
        '            End If
        '        End If
        '        If Not Me._sequence.Sequential Then
        '            If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                        Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                    End Using
        '                    If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                        Me.ActionsOnControls = True
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                    Else
        '                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                    End If
        '                End If
        '            Else
        '                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '        BarraBotones.StatusRecordVisible = True
        '        BarraBotones.StatusRecord = "1"
        '        'INDBtnAddService.Enabled = True
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        'End If
    End Function

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    Private Sub CleanControls()
        INDLcgMainData.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        _purchaseOrderDevolution = Nothing
        Code = String.Empty
        DocumentDate = Nothing
        'SupplierId = Nothing
        INDSleSupplier.EditValue = Nothing
        INDSleSupplier.Properties.ReadOnly = False
        Description = String.Empty
        INDSleSupplier.Properties.NullText = String.Empty
        Dim search = CType(INDSleSupplier, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim view = search.Properties.View
        view.ActiveFilterString = Nothing
        INDMeDetail.Text = String.Empty
        INDGcDevolutions.DataSource = Nothing
        listPurchaseOrderDevolutionDetail = Nothing
        listPurchaseOrderDevolutionDetailDelete = Nothing
        INDLcgMainData.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
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
        Dim content = String.Format(ResourceManager.GetString("FrmPurchaseOrderDevolution_IndexContent", MODULE_NAME), _purchaseOrderDevolution.Code, If(INDSleSupplier.Text Is String.Empty, INDSleSupplier.Properties.NullText, INDSleSupplier.Text), _purchaseOrderDevolution.DocumentDate.ToString())
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = content, _
                .CreationDate = dateServer, _
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName, _
                .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me._purchaseOrderDevolution.Code & "#$", _
                .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._purchaseOrderDevolution.Code), _
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._purchaseOrderDevolution.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Método que borra el registro de auditoria
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

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

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPurchaseOrderDevolution(CStr(Me.Tag))
                    AsyncLoader(True)
                    _purchaseOrderDevolution = Await Model.GetPurchaseOrderDevolutionByCode(Code)
                    INDLcMain.BeginUpdate()
                    If _purchaseOrderDevolution IsNot Nothing AndAlso _purchaseOrderDevolution.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_purchaseOrderDevolution.Id))
                            With _purchaseOrderDevolution
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

                                Code = .Code
                                DocumentDate = .DocumentDate
                                SupplierId = .SupplierId
                                INDSleSupplier.Properties.ReadOnly = True
                                Description = .Observation

                                Me.BarraBotones.StatusRecord = .Status.ToString()

                                listPurchaseOrderDevolutionDetail = Await Model.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(.Id)
                                INDGcDevolutions.DataSource = listPurchaseOrderDevolutionDetail
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._purchaseOrderDevolution.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _purchaseOrderDevolution.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_purchaseOrderDevolution.Id, Me.Tag.ToString(), Nothing, GetType(PurchaseOrderDevolution).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If _purchaseOrderDevolution.Status = 2 Then 'estado confirmado
                                ReadOnlyControls(True)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                            ElseIf _purchaseOrderDevolution.Status = 3 Then
                                ReadOnlyControls(True)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                INDBtnAddDevolution.Enabled = True
                            End If
                            INDbtnCode.Enabled = False
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _purchaseOrderDevolution.Id, 0, _purchaseOrderDevolution.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPurchaseOrderDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDLcMain.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If



        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If
        'Using model As New MPurchaseOrderDevolution(Me.MyTag)
        '    INDLcMain.BeginUpdate()
        '    AsyncLoader(True)
        '    _purchaseOrderDevolution = Await model.GetPurchaseOrderDevolutionByCode(Code)
        '    If _purchaseOrderDevolution IsNot Nothing AndAlso _purchaseOrderDevolution.Id > 0 Then
        '        With _purchaseOrderDevolution
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
        '            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '            Me.BarraBotones.StatusRecordVisible = True

        '            Code = .Code
        '            DocumentDate = .DocumentDate
        '            SupplierId = .SupplierId
        '            INDSleSupplier.Properties.ReadOnly = True
        '            Description = .Observation

        '            Me.BarraBotones.StatusRecord = .Status.ToString()

        '            listPurchaseOrderDevolutionDetail = Await model.GetPurchaseOrderDevolutionDetailByPurchaseOrderDevolutionId(.Id)
        '            INDGcDevolutions.DataSource = listPurchaseOrderDevolutionDetail
        '        End With
        '        Me.GetDocumentIndexed(MyTag & "_" & _purchaseOrderDevolution.Code)
        '        GenerateBlockRecord()
        '        BarraBotones.SetDocuments(_purchaseOrderDevolution.Id)
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '        If _purchaseOrderDevolution.Status = 2 Then 'estado confirmado
        '            ReadOnlyControls(True)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
        '        ElseIf _purchaseOrderDevolution.Status = 3 Then
        '            ReadOnlyControls(True)
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
        '        Else
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        '            INDBtnAddDevolution.Enabled = True
        '        End If
        '        INDbtnCode.Enabled = False

        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '        Me.BarraBotones.PrintReport(PrintReportAction.None, _purchaseOrderDevolution.Id, 0, _purchaseOrderDevolution.Id)
        '    Else
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '        If Me._sequence.IsManual Then
        '            Me.NewPurchaseOrderDevolution()
        '        Else
        '            AsyncLoader(False)
        '            Deshacer()
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
        '            Code = String.Empty
        '            INDbtnCode.Focus()
        '        End If
        '    End If
        '    INDLcMain.EndUpdate()
        'End Using
    End Function

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me._purchaseOrderDevolution.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me._purchaseOrderDevolution.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    Private Sub ReturnPopupAddDevolutions(sender As Object, e As AddDevolutionsEventArgs)
        If listPurchaseOrderDevolutionDetail Is Nothing OrElse listPurchaseOrderDevolutionDetail.Count = 0 Then
            listPurchaseOrderDevolutionDetail = e.ListPurchaseOrderDevolutionDetail
        Else
            For Each item In e.ListPurchaseOrderDevolutionDetail
                Dim detail = listPurchaseOrderDevolutionDetail.Find(Function(x) x.PurchaseOrderDetailId = item.PurchaseOrderDetailId)
                If detail IsNot Nothing Then
                    detail.Quantity = item.Quantity
                Else
                    listPurchaseOrderDevolutionDetail.Add(item)
                End If
            Next
        End If
        INDGcDevolutions.DataSource = listPurchaseOrderDevolutionDetail
        If listPurchaseOrderDevolutionDetail.Count > 0 Then
            INDSleSupplier.Properties.ReadOnly = True
        End If
        INDGcDevolutions.RefreshDataSource()
    End Sub

    Private Sub AssigningValues()
        With _purchaseOrderDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .SupplierId = SupplierId
            .Observation = Description
            For Each item In listPurchaseOrderDevolutionDetail
                .PurchaseOrderDevolutionDetail.Add(item)
            Next
            If listPurchaseOrderDevolutionDetailDelete IsNot Nothing Then
                For Each item In listPurchaseOrderDevolutionDetailDelete
                    .PurchaseOrderDevolutionDetail.Add(item)
                Next
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        _purchaseOrderDevolution = Nothing
        listPurchaseOrderDevolutionDetail = Nothing
        listPurchaseOrderDevolutionDetailDelete = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmPurchaseOrderDevolution control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPurchaseOrderDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMain, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Using model As New MPurchaseOrderDevolution(MyTag)
            INDSleSupplier.Properties.DataSource = model.ListSupplier()
        End Using
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _indigoSession = SessionValues.Instance
        _presenter = New PPurchaseOrderDevolution(Me)
        _presenter.GetSequense()
        ' Agrega a la rejilla la columna de Acciones
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvDevolution, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDevolution.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDGcDevolutions)
        Deshacer()
        LoadStatus()
    End Sub

    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
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
                    Await Me.NewPurchaseOrderDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
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

#Region "ButtonClick"
    Private Sub INDSleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("558", Nothing, True)
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDSleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplier.EditValueChanged
        If INDSleSupplier.EditValue IsNot Nothing Then
            INDBtnAddDevolution.Enabled = True
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAddDevolution_Click(sender As Object, e As EventArgs) Handles INDBtnAddDevolution.Click
        Using formulario As New FrmPopupPurchaseOrderDevolution()
            AddHandler formulario.AddDevolutionsEventArgs, AddressOf ReturnPopupAddDevolutions

            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle

            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Size = New Size(800, 700)
            formulario.SupplierId = INDSleSupplier.EditValue
            formulario.ListPurchaseOrderDevolutionDetail = listPurchaseOrderDevolutionDetail
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If _purchaseOrderDevolution IsNot Nothing AndAlso _purchaseOrderDevolution.Status > 1 Then
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim detail = DirectCast(INDGvDevolution.GetFocusedRow, PurchaseOrderDevolutionDetail)
            If detail.Id > 0 Then
                If listPurchaseOrderDevolutionDetailDelete Is Nothing Then
                    listPurchaseOrderDevolutionDetailDelete = New List(Of PurchaseOrderDevolutionDetail)
                End If
                listPurchaseOrderDevolutionDetailDelete.Add(detail.MarkAsDeleted())
            End If
            listPurchaseOrderDevolutionDetail.Remove(detail)
            INDGcDevolutions.DataSource = listPurchaseOrderDevolutionDetail
            If listPurchaseOrderDevolutionDetail.Count = 0 Then
                INDSleSupplier.Properties.ReadOnly = False
            End If
            INDGcDevolutions.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "IdEntityLoaded"
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._purchaseOrderDevolution IsNot Nothing AndAlso Me._purchaseOrderDevolution.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity =  String.Empty
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
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
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _purchaseOrderDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _purchaseOrderDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        _purchaseOrderDevolution.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        _purchaseOrderDevolution.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        _purchaseOrderDevolution.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir

        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _purchaseOrderDevolution.Id, 0, _purchaseOrderDevolution.Id)
    End Sub
#End Region

End Class