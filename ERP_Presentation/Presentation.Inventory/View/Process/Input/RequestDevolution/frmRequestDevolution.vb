'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 26-09-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Inventory.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports Presentation.Accounting
Imports Presentation.Payments
Imports Presentation.Common
Imports Presentation.Payroll
Imports System.Windows
Imports System.Windows.Forms
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class frmRequestDevolution
    Implements IInventoryRequestDevolution, ICustomizableForm

#Region "Properties"
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory

    ''' <summary>
    ''' Secuencia numerica del fomulario
    ''' </summary>
    Private Property _sequence As Domain.Entities.InventorySequence
    Public Property Sequense As InventorySequence Implements IInventoryRequestDevolution.Sequense
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
    ''' Obitne o establece el consecutivo de la devolución de la solicitud
    ''' </summary>
    ''' <value>Consecutivo de la devolución de la solicitud</value>
    ''' <returns>El consecutivo de la decolución de la solicitud</returns>
    Public Property Code As String Implements IInventoryRequestDevolution.Code
        Get
            If INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Obtiene o establece la fecha de la devolución de la solicitud
    ''' </summary>
    ''' <value>Fecha de la devolución de la solicitud</value>
    ''' <returns>La Fecha de la devolución de la solicitud</returns>
    Public Property DocumentDate As Date Implements IInventoryRequestDevolution.DocumentDate
        Get
            Return INDdtDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdtDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estable la descripción de la devolución de solicitud
    ''' </summary>
    ''' <value>Descripción de solicitud</value>
    ''' <returns>La descripción de la devolución de solicitud</returns>
    Public Property Description As String Implements IInventoryRequestDevolution.Description
        Get
            Return INDMeDescription.EditValue
        End Get
        Set(value As String)
            INDMeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de solicitud a devolver
    ''' </summary>
    ''' <value>Tipo de solicitud</value>
    ''' <returns>El tipo de solicitud</returns>
    Public Property RequestDevolutionType As Integer Implements IInventoryRequestDevolution.RequestDevolutionType
        Get
            Return INDslRequestType.EditValue
        End Get
        Set(value As Integer)
            INDslRequestType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryRequestDevolution.MyLayoutControl
        Get
            Return MyLayoutControl
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As String Implements IInventoryRequestDevolution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IInventoryRequestDevolution.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

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

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene la lista de tipos de devolución de solicitudes
    ''' </summary>
    Dim ListRequestDevolutionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PInventoryRequestDevolution

    ''' <summary>
    ''' Representa el Modelo
    ''' </summary>
    Dim Model As MInventoryRequestdevolution

    ''' <summary>
    ''' Representa la entidad cabecera
    ''' </summary>
    Dim inventoryRequestDevolution As InventoryRequestDevolution

    ''' <summary>
    ''' Reprecenta la entidad detalle
    ''' </summary>
    Dim inventoryRequestdevolutionDetail As InventoryRequestDevolutionDetail

    ''' <summary>
    ''' Listado de eliminados de los detalles de devolución de solicitudes
    ''' </summary>
    Dim ListDeleteInventoryRequestDevolutionDetail As List(Of InventoryRequestDevolutionDetail)

    ''' <summary>
    ''' Flag para solo lectura
    ''' </summary>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' Indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    Dim indexEditRecord As Integer

    ''' <summary>
    ''' Variable para saber si confirma [True:Si confirma - False:No confirma]
    ''' </summary>
    Dim banConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si guardan y confirman, o si actualizan y confirman [True:GuardarConfirmar - False:ActualizarConfirmar]
    ''' </summary>
    Dim banSaveAndConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si anulan [True:Si anula - False:no confirma]
    ''' </summary>
    Dim banAnular As Boolean

    ''' <summary>
    ''' Variable para saber si el formulario ya cargo
    ''' </summary>
    ''' <remarks></remarks>
    Dim banActivateForm As Boolean = False

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' variable que contiene la lista de solicitudes para devolver
    ''' </summary>
    ''' <remarks></remarks>
    Dim SelectRequestToDevolution As List(Of InventoryRequestDetail)

    ''' <summary>
    ''' TASK: Listado de los detalles de devolución de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRequestDevolutionDetail As List(Of InventoryRequestDevolutionDetail)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO BUSCAR: Item buscar del control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO GUARDAR: Item Guardar del cotrol de usuario
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If inventoryRequestDevolution.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            Else
                If _listRequestDevolutionDetail Is Nothing AndAlso _listRequestDevolutionDetail.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No hay solicitudes agregadas a la rejilla."
                    Exit Sub
                End If
            End If
            AssigningValues()
        End If
        Try
            Using model As New MInventoryRequestdevolution(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveInventoryRequestdevolution(inventoryRequestDevolution, _idCurrentSequence, Me._sequence)
                If Result.StatusCode = eStatusResult.SUCCESS Then
                    If inventoryRequestDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        End If
                    ElseIf inventoryRequestDevolution.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If banConfirm Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                            End If
                        ElseIf banAnular Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If

                    End If
                    Me.inventoryRequestDevolution = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, inventoryRequestDevolution.Id, 0, inventoryRequestDevolution.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, inventoryRequestDevolution.Id, 0, inventoryRequestDevolution.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, inventoryRequestDevolution.Id, 0, inventoryRequestDevolution.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, inventoryRequestDevolution.Id, 0, inventoryRequestDevolution.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO NUEVO: Item nuevo del control de usuario
    ''' </summary>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewInventoryRequestDevolution()
        End If
    End Sub

    ''' <summary>
    ''' METODO OPEN SEARCH: Este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRequestDevolution
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        _prefixSelected = Nothing
        _idCurrentSequence = Nothing
        ListRequestDevolutionType = Nothing
        Presenter = Nothing
        Model = Nothing
        inventoryRequestDevolution = Nothing
        inventoryRequestdevolutionDetail = Nothing
        ListDeleteInventoryRequestDevolutionDetail = Nothing
        OnlyRead = Nothing
        indexEditRecord = Nothing
        banConfirm = Nothing
        banSaveAndConfirm = Nothing
        banAnular = Nothing
        banActivateForm = Nothing
        varImp = Nothing
        SelectRequestToDevolution = Nothing
        _listRequestDevolutionDetail = Nothing
    End Sub

    ''' <summary>
    ''' LOAD: Evento Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub frmRequestDevolution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcMainGroup, True)

        '***** Inicializar Variables *****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PInventoryRequestDevolution(Me)
        Presenter.GetSequense()
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvRequestDevolutionView, ListActions)
        InitializeTuples()
        Deshacer()

        IndigoGridControl1.RefreshGrid(INDgcRequestDevolution)
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            banActivateForm = True
            INDbtnCode.Focus()
        End If
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

    ''' <summary>
    ''' FORM_CLOSING: Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmRequestDevolution_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' EDIT_VALUE_CHANGED: Evento que se dispara cuando cambio el value del searchlookupEdit [1:Unidad Funcional - 2:almacen]
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslRequestType_EditValueChanged(sender As Object, e As EventArgs) Handles INDslRequestType.EditValueChanged
        Using Model As New MInventoryRequestdevolution(MyTag)
            INDgcRequestList.DataSource = Model.ListRequestconfirmedByType(RequestDevolutionType)
        End Using
        If RequestDevolutionType = 1 Then
            If INDGvRequestList.Columns.ColumnByName("INDGclRequestSourceWarehouse").Visible = True Then
                INDGvRequestList.Columns.ColumnByName("INDGclRequestSourceWarehouse").Visible = False
            End If
            If INDGvRequestList.Columns.ColumnByName("INDGclRequestTargetWarehouse").Visible = True Then
                INDGvRequestList.Columns.ColumnByName("INDGclRequestTargetWarehouse").Visible = False
            End If
            INDGvRequestList.Columns.ColumnByName("INDGclRequestFunctionalUnit").Visible = True
            INDGvRequestList.Columns.ColumnByName("INDGclRequestFunctionalUnit").VisibleIndex = 2
        ElseIf RequestDevolutionType = 2 Then
            INDGvRequestList.Columns.ColumnByName("INDGclRequestFunctionalUnit").Visible = False
            INDGvRequestList.Columns.ColumnByName("INDGclRequestSourceWarehouse").Visible = True
            INDGvRequestList.Columns.ColumnByName("INDGclRequestTargetWarehouse").Visible = True
            INDGvRequestList.Columns.ColumnByName("INDGclRequestSourceWarehouse").VisibleIndex = 2
            INDGvRequestList.Columns.ColumnByName("INDGclRequestTargetWarehouse").VisibleIndex = 3
        End If
    End Sub

    ''' <summary>
    ''' KEY_DOWN: Evento que se dispara al presionar enter en el control de codigo para cargar los controles
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
                    Await Me.NewInventoryRequestDevolution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' EDIT_VALUE_CHANGING: Evento que controla el cambio en la columna de Cantidad Devolución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSpeOutstandingQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDSpeOutstandingQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim RequestDevolutionDetail = DirectCast(INDGvRequestDevolutionView.GetFocusedRow, InventoryRequestDevolutionDetail)

        If e.NewValue < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser negativa"
            e.Cancel = True
            Exit Sub
        End If

        If e.NewValue > RequestDevolutionDetail.OutstandingQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver no puede ser mayor que la cantidad pendiente"
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' CLICK: Evento click que agrega la solicitud al detalle de la devolución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddRequest_Click(sender As Object, e As EventArgs) Handles INDBtnAddRequest.Click
        If _listRequestDevolutionDetail Is Nothing Then
            _listRequestDevolutionDetail = New List(Of InventoryRequestDevolutionDetail)()
        End If

        If INDGvRequestList.GetSelectedRows.Length <> 0 Then
            For Each indexRow In INDGvRequestList.GetSelectedRows
                Dim objRequestDevolutionDetailSelected = DirectCast(INDGvRequestList.GetRow(indexRow), InventoryRequestDetailXpo)
                Dim detailAdded = _listRequestDevolutionDetail.Find(Function(x) x.InventoryRequestDetailId = objRequestDevolutionDetailSelected.Id)
                If detailAdded Is Nothing Then
                    Dim _requestDevolutionDetail As New InventoryRequestDevolutionDetail
                    With _requestDevolutionDetail
                        .InventoryRequestDetailId = objRequestDevolutionDetailSelected.Id
                        .Quantity = objRequestDevolutionDetailSelected.OutstandingQuantity
                        .ProductoCodeName = objRequestDevolutionDetailSelected.InventoryProductId.CodeName
                        .RequestCode = objRequestDevolutionDetailSelected.InventoryRequestId.Code
                        .RequestType = INDslRequestType.EditValue
                        .OutstandingQuantity = objRequestDevolutionDetailSelected.OutstandingQuantity
                    End With
                    _listRequestDevolutionDetail.Add(_requestDevolutionDetail)
                End If
            Next
            INDgcRequestDevolution.DataSource = _listRequestDevolutionDetail
            INDgcRequestDevolution.RefreshDataSource()
            INDGvRequestList.ClearSelection()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar mínimo una solicitud para agregar a la devolución"
        End If
    End Sub

    ''' <summary>
    ''' CLICK_BUTTON_ACTION: Evento que ejecuta el metodo para eliminar el detalle de la devolución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

    ''' <summary>
    ''' CLICK_CONTEXMENU_ACTION: Evento que ejecuta el metodo para eliminar el detalle de la devolución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub


#End Region

#Region "Methods"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
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
        INDdtDocumentDate.Properties.MinValue = dateMin
        INDdtDocumentDate.Properties.MaxValue = GetDateServer()
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

    ''' <summary>
    ''' Inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()

        ListRequestDevolutionType = New List(Of Tuple(Of Integer, String))
        ListRequestDevolutionType.Add(New Tuple(Of Integer, String)(1, "Unidad Funcional"))
        ListRequestDevolutionType.Add(New Tuple(Of Integer, String)(2, "Almacen"))
        INDslRequestType.Properties.DataSource = ListRequestDevolutionType.ToList

    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryRequestDevolution.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDbtnPopupContainer.Enabled = value
            INDLcMainGroup.EndUpdate()
            INDMeDescription.Enabled = value
            INDdtDocumentDate.Enabled = value
            INDgcRequestDevolution.Enabled = value
            If value Then
                INDdtDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Limpias los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()

        inventoryRequestDevolution = Nothing
        Code = String.Empty
        DocumentDate = Me.GetDateServer()
        Description = String.Empty
        RequestDevolutionType = 1
        Status = 0
        ReadOnlyControls(False)
        ActionsOnControls = False
        _listRequestDevolutionDetail = Nothing
        ListDeleteInventoryRequestDevolutionDetail = Nothing
        INDgcRequestDevolution.DataSource = Nothing

        _listRequestDevolutionDetail = New List(Of InventoryRequestDevolutionDetail)

        Using Model As New MInventoryRequestdevolution(MyTag)
            INDgcRequestList.DataSource = Model.ListRequestconfirmedByType(RequestDevolutionType)
        End Using

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
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
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryRequestDevolution.Code, Me.inventoryRequestDevolution.DocumentDate, Me.inventoryRequestDevolution.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.inventoryRequestDevolution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryRequestDevolution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.inventoryRequestDevolution.Code, Me.inventoryRequestDevolution.DocumentDate, Me.inventoryRequestDevolution.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.inventoryRequestDevolution.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que carga los controles de la devolucion de solicitudes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MInventoryRequestdevolution(CStr(Me.Tag))
                    AsyncLoader(True)
                    inventoryRequestDevolution = (Await Model.GetInventoryRequestDevolutionbyCode(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDLcMainGroup.BeginUpdate()
                    If inventoryRequestDevolution IsNot Nothing AndAlso inventoryRequestDevolution.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(inventoryRequestDevolution.Id))
                            With inventoryRequestDevolution
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                If .Status <> 1 Then
                                    INDdtDocumentDate.Properties.MinValue = .DocumentDate
                                End If

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Description = .Description
                                Me.BarraBotones.StatusRecord = .Status.ToString

                                _listRequestDevolutionDetail = .InventoryRequestDevolutionDetail.ToList
                                INDgcRequestDevolution.DataSource = Nothing
                                INDgcRequestDevolution.DataSource = _listRequestDevolutionDetail
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.inventoryRequestDevolution.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = inventoryRequestDevolution.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                                'INDslRequestType.Enabled = True
                                INDbtnPopupContainer.Enabled = True
                            Else
                                'INDslRequestType.Enabled = False
                                INDbtnPopupContainer.Enabled = False
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                            End If

                            Me.BarraBotones.SetDocuments(inventoryRequestDevolution.Id, Me.Tag.ToString(), Nothing, GetType(InventoryRequestDevolution).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, inventoryRequestDevolution.Id, 0, inventoryRequestDevolution.Code, inventoryRequestDevolution.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInventoryRequestDevolution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDLcMainGroup.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
        If Status = 2 Then
            'INDslRequestType.Enabled = False
            INDbtnPopupContainer.Enabled = False
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.inventoryRequestDevolution IsNot Nothing AndAlso Me.inventoryRequestDevolution.Id > 0 Then
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
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Carga los estados de la devolucion de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewInventoryRequestDevolution() As Task
        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
            Exit Function
        End If

        Me.inventoryRequestDevolution = New Domain.Entities.InventoryRequestDevolution
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
    End Function

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With inventoryRequestDevolution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .Description = Description
            If _listRequestDevolutionDetail IsNot Nothing AndAlso _listRequestDevolutionDetail.Count > 0 Then
                For Each itemRequest As InventoryRequestDevolutionDetail In _listRequestDevolutionDetail
                    .InventoryRequestDevolutionDetail.Add(itemRequest)
                Next
            End If
            If ListDeleteInventoryRequestDevolutionDetail IsNot Nothing AndAlso ListDeleteInventoryRequestDevolutionDetail.Count > 0 Then
                For Each itemRequest As InventoryRequestDevolutionDetail In ListDeleteInventoryRequestDevolutionDetail
                    .InventoryRequestDevolutionDetail.Add(itemRequest)
                Next
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Guarda o actualiza y confirma la devolución de inventario
    ''' </summary>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Private Async Sub SaveOrUpdateAndConfirm(action As Integer)
        If ValidateControls() Then
            If Me.inventoryRequestDevolution.Id = 0 Then
                Dim queryQuantities = _listRequestDevolutionDetail.Find(Function(x) x.Quantity > 0)
                If _listRequestDevolutionDetail Is Nothing OrElse _listRequestDevolutionDetail.Count = 0 OrElse queryQuantities IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar una solicitud para devolver"
                    Exit Sub
                End If

            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MInventoryRequestdevolution
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmRequestDevolution(inventoryRequestDevolution, _idCurrentSequence, action, Me._sequence)
                AsyncLoader(False)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                ElseIf result.StatusCode = eStatusResult.WARNING Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Me.Deshacer()
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Eliminar el detalle de la devolución
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim detail As InventoryRequestDevolutionDetail = INDGvRequestDevolutionView.GetFocusedRow()
        If detail.Id > 0 Then
            If ListDeleteInventoryRequestDevolutionDetail Is Nothing Then
                ListDeleteInventoryRequestDevolutionDetail = New List(Of InventoryRequestDevolutionDetail)
            End If
            detail.MarkAsDeleted()
            ListDeleteInventoryRequestDevolutionDetail.Add(detail)
        End If
        _listRequestDevolutionDetail.Remove(detail)
        INDgcRequestDevolution.DataSource = Nothing
        INDgcRequestDevolution.DataSource = _listRequestDevolutionDetail
    End Sub

    Private Function ValidateControls() As Boolean
        Dim ControlsValidate As Boolean = True

        If INDbtnCode.Text = String.Empty Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), INDlyCode.Text.ToString())
            ControlsValidate = False
        End If

        If INDdtDocumentDate.Text = String.Empty Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), INDlyDocumentDate.Text.ToString())
            ControlsValidate = False
        End If

        If INDMeDescription.Text = String.Empty Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), INDLyDescription.Text.ToString())
            ControlsValidate = False
        End If

        If INDgcRequestDevolution.DataSource Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), LayoutControlGroup3.Text.ToString())
            ControlsValidate = False
        End If

        Return ControlsValidate

    End Function

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, inventoryRequestDevolution.Id, 0, inventoryRequestDevolution.Code, inventoryRequestDevolution.Id)

    End Sub

    ''' <summary>
    ''' LOAD: Carga los permisos de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' CLICK_BUSCAR: Evento ClickBuscar de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' CLICK_DESHACER
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' CLICK_GUARDAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        inventoryRequestDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ACTUALIZAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        inventoryRequestDevolution.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ANULAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        inventoryRequestDevolution.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_GUARDAR_CONFIRMAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        inventoryRequestDevolution.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ACTUALIZAR_COFIRMAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        inventoryRequestDevolution.Status = 2
        Guardar()
    End Sub

#End Region

End Class