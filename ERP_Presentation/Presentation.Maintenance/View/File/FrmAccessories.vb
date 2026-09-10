'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 01-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Maintenance.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de accesorios...
''' </summary>
Public Class FrmAccessories
    Implements IAccessories

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

#End Region

#Region "Variable Globales Propiedades Intefaz y Load"
    ''' <summary>
    ''' propiedad que contiene el estado del registro
    ''' </summary>
    Public Property StateAccessory As Boolean Implements IAccessories.StateAccessory
        Get
            Return Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = If(value, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del accesorio
    ''' </summary>
    Public Property NameAccessories As String Implements IAccessories.NameAccessory
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property

    Public Property Code As String Implements IAccessories.CodeAccessory
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Propiedad que carga todas  tipos de equipo
    ''' </summary>
    Public WriteOnly Property EquipmentTypeDataSource As List(Of FixedAssetEquipmentType) Implements IAccessories.EquipmentTypeDataSource
        Set(value As List(Of FixedAssetEquipmentType))
            INDglEquipmentType.Properties.DataSource = value
            INDglEquipmentType.Properties.PopupFormWidth = 400
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que carga todos los tipos de equipos agregados 
    ''' </summary>
    Public WriteOnly Property ListEquipmentTypeDataSource As List(Of FixedAssetEquipmentType) Implements IAccessories.ListEquipmentTypeDataSource
        Set(value As List(Of FixedAssetEquipmentType))
            'INDgcListEquipmentType.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el id del tipo de equipo
    ''' </summary>
    Public Property IdEquipmentType As Integer? Implements IAccessories.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer?)
            INDglEquipmentType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Variable que contiene el accesorio
    ''' </summary>
    Dim Accessory As Accessory
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MAccessories
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PAccessories
    ''' <summary>
    ''' variable que contiene el listado de los registro eliminados
    ''' </summary>
    Dim DeleteList As New List(Of AccesoryDetail)
    ''' <summary>
    ''' variable que contiene el listado de los tipos de equipo agregados
    ''' </summary>
    'Dim EquipmentTypeList As New List(Of FixedAssetEquipmentType)

    ''' <summary>
    ''' variable que contiene el listado de los registro eliminados
    ''' </summary>
    Dim DeletedList As New List(Of Domain.Entities.Accessory)

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyBudgetConcept, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PAccessories(Me)
        GetSequense()
        AddActionColumns()
        Me.LoadStatus()
        Deshacer()
    End Sub

    Private Sub AddActionColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgcListEquipmentTypeView, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgcListEquipmentTypeView.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As Domain.Entities.BlockRecordMaintenance

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.MaintenanceSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Gets or sets the sequense.
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As Domain.Entities.MaintenanceSequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        If DeleteList.Count > 0 Then
            Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeleteList.Count - 1
                If DeleteList.Item(i).Id <> 0 Then
                    DeleteList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    Accessory.AccesoryDetail.Add(DeleteList.Item(i))
                End If
            Next
        End If
        Try
            Using Model As New MAccessories
                AsyncLoader(True)
                Dim result = Await Model.SaveAccessoryAsync(Accessory, _idCurrentSequence)
                If result.StateResult = True Then
                    Me.Accessory = result.ObjectEmbbeded
                    If Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = result.Message 'obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Accessory IsNot Nothing And INDBteCode.Enabled = False Then
            If Accessory.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        AsyncLoader(True)
                        Using Model As New MAccessories
                            If Await Model.DeleteAccessoryAsync(Accessory) = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                                Me.DeleteDocumentIndexed()
                                AsyncLoader(False)
                                Deshacer()
                            Else
                                AsyncLoader(False)
                                INDBteCode.Enabled = False
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End Using
                    Catch ex As Exception
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        Throw ex
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneAccesorios, Eform.Accesorios)
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneAccesorios, Eform.Accesorios)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEntity()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Accessories
            BarraBotones.PrepareToolbar(eAction.New)
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
        INDBteCode.Text = ReturnValue
        DeleteBlockedRecord()
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)

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

#Region "Handlers"
    Private Sub INDglEquipmentType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglEquipmentType.QueryPopUp
        If INDglEquipmentType.Properties.DataSource Is Nothing Then
            Presenter.Initializes()
            'Await Presenter.Initializes()


            'Using Model As New MEquipamentType
            '    EquipmentTypeList = Await Model.ListAllEquipamentType
            '    If EquipmentTypeList IsNot Nothing AndAlso EquipmentTypeList.Count > 0 Then
            '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            '        EquipmentTypeDataSource = EquipmentTypeList
            '        Counter = EquipmentTypeList.Item(EquipmentTypeList.Count - 1).Id
            '    Else
            '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            '    End If
            'End Using


        End If
    End Sub
#End Region

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Establece el datasource de tipo de equipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EquipmentTypeXpo As XPCollection Implements IAccessories.EquipmentTypeXpo
        Get
            Return INDglEquipmentType.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDglEquipmentType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de equipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
        If IdEquipmentType IsNot Nothing Then
            ValidateEquipmentType()
        End If
    End Sub

    ''' <summary>
    ''' Valida que el el tipo de proveedor sea el ultimo item
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateEquipmentType()
        If INDglEquipmentType.EditValue IsNot Nothing AndAlso INDglEquipmentType.Properties.DataSource IsNot Nothing Then
            Dim item As FixedAssetEquipmentTypeXpo = (From fu In EquipmentTypeXpo Where fu.Id = INDglEquipmentType.EditValue Select fu).FirstOrDefault
            If item IsNot Nothing Then
                Dim count = (From fu As FixedAssetEquipmentTypeXpo In EquipmentTypeXpo Where fu.ParentId IsNot Nothing AndAlso fu.ParentId.Id = INDglEquipmentType.EditValue Select fu).Count
                If count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedItemLastLevel")
                    INDglEquipmentType.Properties.NullText = String.Empty
                    IdEquipmentType = Nothing
                    Exit Sub
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MAccessories
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString("FrmAccessories_IndexContent", NAME_MODULE), Me.Accessory.Code, Me.Accessory.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Accessory.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString("FrmAccessories_IndexTitle", NAME_MODULE), Me.Accessory.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString("FrmAccessories_IndexContent", NAME_MODULE), Me.Accessory.Code, Me.Accessory.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString("FrmAccessories_IndexTitle", NAME_MODULE), Me.Accessory.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Function NewEntity() As Task
        Accessory = New Accessory() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MAccessories()
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
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        INDBteCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MAccessories
            Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        INDTxtName.Text = String.Empty
        INDBteCode.Text = String.Empty
        INDglEquipmentType.EditValue = Nothing
        INDgcListEquipmentType.DataSource = Nothing
        'EquipmentTypeList.Clear()
        DeleteList.Clear()
        'INDlyItemListEquipmentType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BarraBotones.ReassignOperatingUnit()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.Accesorios = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAccessories.ActionsOnControls
        Set(value As Boolean)
            INDLyCtrAccessories.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDglEquipmentType.Enabled = value
            INDgcListEquipmentType.Enabled = value
            INDbtnAgregar.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDLyCtrAccessories.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

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
                Using Model As New MAccessories()
                    AsyncLoader(True)
                    Accessory = Await Model.GetAccessoryAsync(INDBteCode.Text.Trim)
                    INDLyCtrAccessories.BeginUpdate()
                    If Accessory IsNot Nothing AndAlso Accessory.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        blockRecord = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Accessory.Id))
                        With Accessory
                            'Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Code = .Code
                            NameAccessories = .Name
                            'For i As Integer = 0 To .AccesoryDetail.ToList.Count - 1
                            '    EquipmentTypeList.Add(.AccesoryDetail.ToList.Item(i).FixedAssetEquipmentType)
                            'Next
                            'If EquipmentTypeList.Count > 0 Then
                            '    'ListEquipmentTypeDataSource = EquipmentTypeList
                            'End If
                            INDgcListEquipmentType.DataSource = .AccesoryDetail.ToList()
                            StateAccessory = .State
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Accessory.Code)
                        If blockRecord.Id = 0 Then
                            blockRecord = (Await Model.SaveBlockRecord(
                            New Domain.Entities.BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Accessory.Id})
                            ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), blockRecord.CodUser, blockRecord.NameUser, blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, blockRecord.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(Accessory.Id, Me.Tag.ToString(), Nothing, GetType(Accessory).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True

                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEntity()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLyCtrAccessories.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Accessory
            .Code = Code
            .Name = NameAccessories
        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para agregar un tipo de equipo al detalle
    ''' </summary>
    Private Sub AddEquipmentType()
        If IdEquipmentType Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Tipo de Equipo"
            Exit Sub
        End If
        If Not Accessory.AccesoryDetail.Any(Function(x) x.IdEquipmentType = IdEquipmentType.Value) Then
            Accessory.AccesoryDetail.Add(New AccesoryDetail With
                                         {.IdAccessory = Accessory.Id,
                                         .IdEquipmentType = IdEquipmentType.Value,
                                         .EquipmentTypeCode = INDglEquipmentType.Text.Split("-")(0).Trim,
                                         .EquipmentTypeName = INDglEquipmentType.Text.Split("-")(1).Trim
                                         })
            If Accessory.Id = 0 Then
                Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
            Else
                Accessory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            'EquipmentTypeList.Add(New FixedAssetEquipmentType With {.Code = INDglEquipmentType.EditValue.ToString.Trim, .Name = INDglEquipmentType.Text.ToString.Trim})
            INDglEquipmentType.EditValue = Nothing
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El Tipo de Equipo ya existe en la lista"
            INDglEquipmentType.EditValue = Nothing
        End If
        INDglEquipmentType.Focus()
        INDgcListEquipmentType.DataSource = Accessory.AccesoryDetail.ToList() 'EquipmentTypeList
        INDgcListEquipmentType.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewEntity()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
    ''' <summary>
    ''' Evento que llama el metodo AddEquipmentType
    ''' </summary>
    Private Sub INDbtnAgregar_Click(sender As Object, e As EventArgs) Handles INDbtnAgregar.Click
        AddEquipmentType()
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para eliminar un tipo de equipo del detalle
    ''' </summary>
    Private Sub INDgcListEquipmentType_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles INDgcListEquipmentType.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            Dim Row = INDgcListEquipmentTypeView.FocusedRowHandle
            DeleteList.Add(Accessory.AccesoryDetail.Item(Row))
            Accessory.AccesoryDetail.RemoveAt(Row)
        End If
    End Sub

    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Accessory = Nothing
        Model = Nothing
        Presenter = Nothing
        DeleteList = Nothing
        'EquipmentTypeList = Nothing
        DeleteList = Nothing
        blockRecord = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub

    Private Sub FrmBudgetConcept_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmBudgetConcept_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Accessory IsNot Nothing AndAlso Me.Accessory.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Eventos Barra Botones"

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
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Try
            Using model As New MAccessories()
                AsyncLoader(True)
                Dim state = Not Accessory.State
                Dim Result = Await model.ChangeState(INDBteCode.Text, True)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Accessory = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                Else
                    INDBteCode.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception

            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
            If Me._sequence.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequence.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        'If sender.Tag = "Remove" Then
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDgcListEquipmentTypeView.GetFocusedRow(), AccesoryDetail).MarkAsDeleted()
            INDgcListEquipmentType.DataSource = Accessory.AccesoryDetail.ToList()
            INDgcListEquipmentType.RefreshDataSource()
            If Accessory.Id > 0 Then
                Accessory.MarkAsModified()
            End If
        End If
        'End If
    End Sub

#End Region

End Class