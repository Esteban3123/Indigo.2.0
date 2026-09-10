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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de consumibles
''' </summary>
Public Class FrmConsumables
    Implements IConsumables, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

#End Region

#Region "Variable Globales Propiedades Intefaz y Load"

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Esta propiedad contiene el estado del consumible
    ''' </summary>
    Public Property Status As Boolean Implements IConsumables.StateConsumable
        Get
            Return Me.BarraBotones.StatusRecord
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
    ''' Esta propiedad contiene el nombre del consumible
    ''' </summary>
    Public Property NameConsumable As String Implements IConsumables.NameConsumable
        Get
            Return INDTxtName.Text
        End Get
        Set(value As String)
            INDTxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el codigo del consumible
    ''' </summary>
    Public Property CodeConsumable As String Implements IConsumables.CodeConsumable
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
    Public Property EquipmentTypeDataSource As XPCollection Implements IConsumables.EquipmentTypeDataSource
        Get
            Return INDglEquipmentType.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDglEquipmentType.Properties.DataSource = value
            INDglEquipmentType.Properties.PopupFormWidth = INDglEquipmentType.Width
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el id del tipo de equipo
    ''' </summary>
    Public Property IdEquipmentType As Integer Implements IConsumables.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer)
            INDglEquipmentType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene el consumible
    ''' </summary>
    Dim Consumable As Consumable
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MConsumables
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PConsumables
    ''' <summary>
    ''' variable que contiene el listado  registros eliminados
    ''' </summary>
    Dim DeletedList As New List(Of ConsumableDetail)

    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

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

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PConsumables(Me)
        GetSequense()
        LoadStatus()
        Deshacer()
        AddActionColumns()
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

        If DeletedList.Count > 0 Then
            Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeletedList.Count - 1
                If DeletedList.Item(i).Id <> 0 Then
                    DeletedList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    Consumable.ConsumableDetail.Add(DeletedList.Item(i))
                End If
            Next
        End If

        Try
            Using Model As New MConsumables
                AsyncLoader(True)
                Dim result = Await Model.SaveconsumableAsync(Consumable, _idCurrentSequence)
                AsyncLoader(False)
                If result.StateResult = True Then
                    If Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(_idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.Consumable = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    SearchMode = False
                    Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Consumable IsNot Nothing And INDBteCode.Enabled = False Then
            If Consumable.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        AsyncLoader(True)
                        Using Model As New MConsumables
                            If Await Model.DeleteconsumableAsync(Consumable) = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                                AsyncLoader(False)
                                SearchMode = False
                                Deshacer()
                                Me.DeleteDocumentIndexed()
                            Else
                                AsyncLoader(False)
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End Using
                    Catch ex As Exception
                        AsyncLoader(False)
                        Throw ex
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneConsumibles, Consumibles)
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneConsumibles, Consumibles)
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
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        'CleanControls()
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewEntity()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Consumables
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
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
        'If SearchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'INDBteCode.Focus()
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

#Region "Metodos Funciones"

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
                .Content = String.Format(ResourceManager.GetString("FrmConsumables_IndexContent", NAME_MODULE), Me.Consumable.Code, Me.Consumable.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Consumable.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString("FrmConsumables_IndexTitle", NAME_MODULE), Me.Consumable.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString("FrmConsumables_IndexContent", NAME_MODULE), Me.Consumable.Code, Me.Consumable.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString("FrmConsumables_IndexTitle", NAME_MODULE), Me.Consumable.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Function NewEntity() As Task
        'Consumable = New Consumable()
        'If Me._sequense.Scope IsNot Nothing Then
        '    If Me._sequense.IsManual Then
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    Else
        '        If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '            Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail(0).Id
        '        ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '            If Me._sequense.MaintenanceSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '                Me._idCurrentSequense = Me._sequense.MaintenanceSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '            Else
        '                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                Exit Sub
        '            End If
        '        End If
        '        If Not Me._sequense.Sequential Then
        '            If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '                If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                    PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '                Else
        '                    Using model As New MAccessories()
        '                        Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
        '                    End Using
        '                    If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                        PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '                    Else
        '                        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                    End If
        '                End If
        '            Else
        '                PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '            End If
        '        Else
        '            PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '        End If
        '    End If
        'Else
        '    Me.ActionsOnControls = False
        '    Mensaje(EeventViewerImages.Advertencia) = "Revise la Secuencia Numérica por favor. Comunicar con el Administrador."
        'End If


        Consumable = New Consumable() With {.State = True}
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
                Me.CodeConsumable = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeConsumable = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MAccessories
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeConsumable = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeConsumable = ResourceManager.GetString("LabelOrTextboxNew")
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
        Using model As New MConsumables
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
        'ActionsOnControls = False
        'INDTxtName.Text = String.Empty
        'INDBteCode.Text = String.Empty
        'INDglEquipmentType.EditValue = Nothing
        'INDgcListEquipmentType.DataSource = Nothing
        'DeletedList.Clear()
        'EquipmentTypeList.Clear()
        'INDlyItemListEquipmentType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.StatusRecordVisible = False
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.Consumable = Nothing



        INDLyCtrConsumable.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDTxtName.Text = String.Empty
        INDBteCode.Text = String.Empty
        INDglEquipmentType.EditValue = Nothing
        INDgcListEquipmentType.DataSource = Nothing
        DeletedList.Clear()
        'Limpiar controles
        Consumable = Nothing
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLyCtrConsumable.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConsumables.ActionsOnControls
        Set(value As Boolean)
            INDLyCtrConsumable.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDbtnAgregar.Enabled = value
            INDglEquipmentType.Enabled = value
            INDgcListEquipmentType.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDLyCtrConsumable.EndUpdate()
            If value = True Then
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
        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If
        'Using Model As New MConsumables
        '    Consumable = Await Model.GetconsumableAsync(INDBteCode.Text)
        '    If Consumable.Id > 0 Then
        '        ActionsOnControls = True
        '        Me.BarraBotones.StatusRecordVisible = True
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Consumable.Id)
        '        With Consumable
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '            CodeConsumable = .Code
        '            NameConsumable = .Name
        '            For i As Integer = 0 To .ConsumableDetail.ToList.Count - 1
        '                EquipmentTypeList.Add(.ConsumableDetail.ToList.Item(i).EquipmentType)
        '            Next
        '            If EquipmentTypeList.Count > 0 Then
        '                INDlyItemListEquipmentType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '            End If
        '            ListEquipmentTypeDataSource = EquipmentTypeList
        '            StateConsumable = .State

        '            Me.GetDocumentIndexed(Me.Tag & "_" & Me.Consumable.Code)
        '            If result.Id = 0 Then
        '                Me.BarraBotones.SetDocuments(Consumable.Id, Me.Tag)
        '                Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                state.State = Domain.Base.Entities.ObjectState.Added
        '                blockRecord = New Domain.Entities.BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Consumable.Id}
        '                Dim operation = Await Model.SaveBlockRecord(blockRecord)
        '                blockRecord = operation.ObjectEmbbeded
        '            Else
        '                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '                blockRecord = result
        '                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '            End If
        '        End With
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("RecordNotExist")
        '        'INDBteCode.Text = String.Empty
        '        'INDBteCode.Focus()
        '        If Me._sequense.IsManual Then
        '            Me.NewEntity()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            INDBteCode.Text = String.Empty
        '            INDBteCode.Focus()
        '        End If
        '    End If
        'End Using


        If Not String.IsNullOrEmpty(CodeConsumable) AndAlso Not String.IsNullOrWhiteSpace(CodeConsumable) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MConsumables
                    AsyncLoader(True)
                    Consumable = Await Model.GetconsumableAsync(INDBteCode.Text)
                    INDLyCtrConsumable.BeginUpdate()
                    If Consumable IsNot Nothing AndAlso Consumable.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        blockRecord = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Consumable.Id))
                        With Consumable
                            'LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            'Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            'Llenar Entidad
                            CodeConsumable = .Code
                            NameConsumable = .Name
                            INDgcListEquipmentType.DataSource = .ConsumableDetail.ToList()
                            Status = .State
                        End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Consumable.Code)
                        If blockRecord.Id = 0 Then
                            blockRecord = (Await Model.SaveBlockRecord(
                                    New Domain.Entities.BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Consumable.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), blockRecord.CodUser, blockRecord.NameUser, blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, blockRecord.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(Consumable.Id, Me.Tag.ToString(), Nothing, GetType(Consumable).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        'End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEntity()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeConsumable = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLyCtrConsumable.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDTxtName.Text = String.Empty Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Consumable
            .Code = CodeConsumable
            .Name = NameConsumable
        End With
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para agregar un tipo de equipo al listado
    ''' </summary>
    Private Sub AddEquipmentType()
        If Object.Equals(INDglEquipmentType.EditValue, Nothing) = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Tipo de Equipo"
            Exit Sub
        End If
        If Consumable.ConsumableDetail.Where(Function(x) x.IdEquipmentType = IdEquipmentType).Count = 0 Then
            Consumable.ConsumableDetail.Add(New ConsumableDetail With {
                                            .IdConsumable = Consumable.Id,
                                            .IdEquipmentType = IdEquipmentType,
                                            .EquipmentTypeCode = INDglEquipmentType.Text.Split("-")(0).Trim(),
                                            .EquipmentTypeName = INDglEquipmentType.Text.Split("-")(1).Trim()
                                            })
            If Consumable.Id = 0 Then
                Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
            Else
                Consumable.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDglEquipmentType.EditValue = Nothing
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El Tipo de Equipo ya existe en la lista"
            INDglEquipmentType.EditValue = Nothing
        End If
        INDglEquipmentType.Focus()
        INDgcListEquipmentType.DataSource = Consumable.ConsumableDetail.ToList()
        INDgcListEquipmentType.RefreshDataSource()
    End Sub
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        ''If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        ''    If String.IsNullOrEmpty(INDBteCode.Text) Then
        ''        Me.NewEntity()
        ''    Else
        ''        Await Me.LoadControls()
        ''    End If
        ''End If
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDBteCode.Text) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDBteCode.Text) Then
        '            Me.NewEntity()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If


        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeConsumable.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeConsumable) Then
                    Await Me.NewEntity()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Consumable = Nothing
        Model = Nothing
        Presenter = Nothing
        DeletedList = Nothing
        SearchMode = Nothing
        blockRecord = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' Evento para ejecutar el metodo AddEquipmentType y agregar registros de tipos de equipo
    ''' </summary>
    Private Sub INDbtnAgregar_Click(sender As Object, e As EventArgs) Handles INDbtnAgregar.Click
        AddEquipmentType()
    End Sub


    ''' <summary>
    ''' Evento para eliminar registros del listado de equipos de equipos
    ''' </summary>
    Private Sub INDgcListEquipmentType_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles INDgcListEquipmentType.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            Dim Row = INDgcListEquipmentTypeView.FocusedRowHandle
            DeletedList.Add(Consumable.ConsumableDetail.Item(Row))
            Consumable.ConsumableDetail.RemoveAt(Row)
        End If
    End Sub

    Private Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
        If INDglEquipmentType.EditValue IsNot Nothing Then
            ValidateEquipmentType()
        End If
    End Sub

    ''' <summary>
    ''' Valida que el el tipo de proveedor sea el ultimo item
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateEquipmentType()
        If INDglEquipmentType.EditValue IsNot Nothing AndAlso INDglEquipmentType.Properties.DataSource IsNot Nothing Then
            Dim item As FixedAssetEquipmentTypeXpo = (From fu In EquipmentTypeDataSource Where fu.Id = INDglEquipmentType.EditValue Select fu).FirstOrDefault
            If item IsNot Nothing Then
                Dim count = (From fu As FixedAssetEquipmentTypeXpo In EquipmentTypeDataSource Where fu.ParentId IsNot Nothing AndAlso fu.ParentId.Id = INDglEquipmentType.EditValue Select fu).Count
                If count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedItemLastLevel")
                    INDglEquipmentType.Properties.NullText = String.Empty
                    INDglEquipmentType.EditValue = Nothing
                    Exit Sub
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo que se dispara cuando el form esta activo y si el codigo esta habilitado le da el foco
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        Me.ViewModeEditHold = True
        If Me.Consumable IsNot Nothing AndAlso Me.Consumable.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
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
        SearchMode = False
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
        If Not String.IsNullOrEmpty(Me.Consumable.Code) Then
            Try
                Using model As New MConsumables
                    AsyncLoader(True)
                    Dim state As Boolean = Not Consumable.State
                    Dim result As ActionResult(Of Consumable) = Await model.ChangeState(Me.Consumable.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Consumable = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglEquipmentType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglEquipmentType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            'Using pop As New FrmTransparent(New Presentation.Maintenance.FrmEquipmentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent}, False)
            '    pop.Show()
            'End Using
            Presenter.Initializes()
        End If
    End Sub

    Private Sub INDglEquipmentType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglEquipmentType.QueryPopUp
        If INDglEquipmentType.EditValue Is Nothing Then
            Presenter.Initializes()
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            CType(INDgcListEquipmentTypeView.GetFocusedRow(), ConsumableDetail).MarkAsDeleted()
            INDgcListEquipmentType.DataSource = Consumable.ConsumableDetail.ToList()
            INDgcListEquipmentType.RefreshDataSource()
            If Consumable.Id > 0 Then
                Consumable.MarkAsModified()
            End If
        End If
    End Sub

End Class