'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 04-09-2013
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
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Base.Entities
Imports System.Windows.Forms

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de registro tecnico
''' </summary>
Public Class FrmEquipmentTypePartsAccesoriesConsumables
    Implements IEquipmentTypePartsAccesoriesConsumibles


#Region "Constantes"

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

    Public Property IdEquipmentType As Integer Implements IEquipmentTypePartsAccesoriesConsumibles.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer)
            INDglEquipmentType.EditValue = value
        End Set
    End Property

    Public Property IdPartsAccesoriesConsumibles As Integer Implements IEquipmentTypePartsAccesoriesConsumibles.IdPartsAccesoriesConsumibles
        Get
            Return INDglTechnicalLog.EditValue
        End Get
        Set(value As Integer)
            INDglTechnicalLog.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ListEquipmentType As List(Of EquipmentType) Implements IEquipmentTypePartsAccesoriesConsumibles.ListEquipmentType
        Set(value As List(Of EquipmentType))
            INDglEquipmentType.Properties.DataSource = value
            'INDglEquipmentType.Properties.PopupFormWidth = INDglEquipmentType.Width
        End Set
    End Property

    Public WriteOnly Property ListPartsAccesoriesConsumables As List(Of PartsAccesoriesConsumables) Implements IEquipmentTypePartsAccesoriesConsumibles.ListPartsAccesoriesConsumibles
        Set(value As List(Of PartsAccesoriesConsumables))
            INDglTechnicalLog.Properties.DataSource = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements IEquipmentTypePartsAccesoriesConsumibles.MyTag
        Get
            Return Me.Tag
        End Get
    End Property


    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene el consumible
    ''' </summary>
    Dim ListEquipmentTypePartsAccesoriesConsumibles As New List(Of Domain.Maintenance.Entities.EquipmentTypePartsAccesoriesConsumibles)


    Dim EquipmentTypeTechnicalLog As EquipmentTypeTechnicalLog
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MEquipmentTypeTechnicalLog(Me.Tag)
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PEquipmentTypePartsAccesoriesConsumables
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    Dim EquipmentType As EquipmentType

    Dim ListadoPartesAccesoriosConsumibles As New List(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' variable que contiene el listado de los registros eliminados
    ''' </summary>
    Dim DeleteList As New List(Of EquipmentTypePartsAccesoriesConsumibles)
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Dim ListActions As New List(Of eAcciones)

        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgcListUnitMeasureView, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgcListUnitMeasureView.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        Me.LoadStatus()
        Presenter = New PEquipmentTypePartsAccesoriesConsumables(Me)
        Presenter.Initializes()
        Deshacer()
        _searchMode = False
    End Sub

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecordMaintenance

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean
#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        'AssigningValues()
        If DeleteList.Count > 0 Then
            'EquipmentTypeTechnicalLog.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeleteList.Count - 1
                If DeleteList.Item(i).Id <> 0 Then
                    DeleteList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    ListEquipmentTypePartsAccesoriesConsumibles.Add(DeleteList.Item(i))
                End If
            Next
        End If

        Try
            Using Model As New MEquipmentTypePartsAccesoriesConsumables(Me.MyTag)
                AsyncLoader(True)
                Dim result = Await Model.SaveEquipmentTypePartsAccesoriesConsumablesAsync(ListEquipmentTypePartsAccesoriesConsumibles)
                AsyncLoader(False)
                If result = True Then
                    Mensaje(EeventViewerImages.Informacion) = "Se Almacenó Correctamente"
                    'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    _searchMode = False
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If ListEquipmentTypePartsAccesoriesConsumibles IsNot Nothing Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEquipmentTypePartsAccesoriesConsumables(Me.MyTag)
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEquipmentTypePartsAccesoriesConsumablesAsync(ListEquipmentTypePartsAccesoriesConsumibles)
                        If result = True Then
                            If Me._doc IsNot Nothing Then
                                Await Me.DeleteDocumentIndexed()
                            End If
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            _searchMode = False
                            Deshacer()
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            AsyncLoader(False)
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
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
        CleanControls()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = 100}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion", .ColumnWidth = 400}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.TechnicalLog
            .ValorSolicitado = "Codigo"
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
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        'INDBteCode.Text = ReturnValue
        'If INDBteCode.Text <> String.Empty Then
        '    LoadControls()
        '    If INDBteCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    End If
        '    INDBteCode.Enabled = False
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If _searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
        'INDBteCode.Focus()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

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

#Region "Metodos Funciones Propiedades"

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
        ListadoPartesAccesoriosConsumibles = New List(Of PartsAccesoriesConsumables)
        ListEquipmentTypePartsAccesoriesConsumibles = New List(Of EquipmentTypePartsAccesoriesConsumibles)
        EquipmentTypeTechnicalLog = New EquipmentTypeTechnicalLog()
        INDglEquipmentType.EditValue = Nothing
        INDgcListUnitMeasure.DataSource = Nothing
        INDglTechnicalLog.EditValue = Nothing
        INDlyItemListUnitMeasure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        DeleteList.Clear()
        ListadoPartesAccesoriosConsumibles.Clear()
        ListEquipmentTypePartsAccesoriesConsumibles.Clear()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
    End Sub



    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        'If INDBteCode.Text = String.Empty Then
        '    ValidateControls = False
        'End If
        'If INDTxtName.Text = String.Empty Then
        '    ValidateControls = False
        'End If
    End Function

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        'Dim dateServer = Me.GetDateServer()
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.TechnicalLog.Code, Me.TechnicalLog.Name), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.TechnicalLog.Code & "#$", .IdForm = CStr(Me.Tag), _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.TechnicalLog.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        '    Return Me._doc
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.TechnicalLog.Code, Me.TechnicalLog.Name)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.TechnicalLog.Code)
        '    Return Me._doc
        'End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub


    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteCodeKindship_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        OpenSearch()
    End Sub

    Private Sub INDbtnAgregar_Click(sender As Object, e As EventArgs) Handles INDbtnAgregar.Click
        If Object.Equals(INDglTechnicalLog.EditValue, Nothing) = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Registro Técnico"
            Exit Sub
        End If
        If ListadoPartesAccesoriosConsumibles.Where(Function(x) x.Code = INDglTechnicalLog.Properties.View.GetFocusedRowCellValue("Code")).Count = 0 Then
            ListEquipmentTypePartsAccesoriesConsumibles.Add(New Domain.Maintenance.Entities.EquipmentTypePartsAccesoriesConsumibles With {.EquipmentTypeId = IdEquipmentType, .PartsAccesoriesConsumiblesId = IdPartsAccesoriesConsumibles})

            ListadoPartesAccesoriosConsumibles.Add(New PartsAccesoriesConsumables With {.Code = INDglTechnicalLog.Properties.View.GetFocusedRowCellValue("Code").ToString.Trim, .Name = INDglTechnicalLog.Properties.View.GetFocusedRowCellValue("Name").ToString.Trim})
            INDglTechnicalLog.EditValue = Nothing
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El registro técnico ya existe en la lista"
            INDglTechnicalLog.EditValue = Nothing
        End If
        If ListadoPartesAccesoriosConsumibles.Count > 0 Then
            INDlyItemListUnitMeasure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
        INDgcListUnitMeasure.DataSource = Nothing
        INDgcListUnitMeasure.DataSource = ListadoPartesAccesoriosConsumibles
        INDgcListUnitMeasure.RefreshDataSource()
    End Sub

    Private Sub INDgcListUnitMeasure_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles INDgcListUnitMeasure.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = DevExpress.XtraEditors.NavigatorButtonType.Remove Then
            Dim Row = INDgcListUnitMeasureView.FocusedRowHandle
            DeleteList.Add(ListEquipmentTypePartsAccesoriesConsumibles.Item(Row))
            ListEquipmentTypePartsAccesoriesConsumibles.RemoveAt(Row)
        End If
    End Sub

    Private Sub FrmTechnicalLog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        ListEquipmentTypePartsAccesoriesConsumibles = Nothing
        EquipmentTypeTechnicalLog = Nothing
        Model = Nothing
        Presenter = Nothing
        EquipmentType = Nothing
        ListadoPartesAccesoriosConsumibles = Nothing
        DeleteList = Nothing
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim Row = INDgcListUnitMeasureView.FocusedRowHandle
        DeleteList.Add(ListEquipmentTypePartsAccesoriesConsumibles.Item(Row))
        ListEquipmentTypePartsAccesoriesConsumibles.RemoveAt(Row)
        ListadoPartesAccesoriosConsumibles.RemoveAt(Row)
        INDgcListUnitMeasure.DataSource = Nothing
        INDgcListUnitMeasure.DataSource = ListadoPartesAccesoriosConsumibles
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo

    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub


#End Region

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDglUnitMeasure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglEquipmentType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New Presentation.Maintenance.FrmUnitMeasure With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            Await Presenter.Initializes()
        End If
    End Sub

    Private Async Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
        Dim VarEquipmentType = INDglEquipmentType.Properties.GetRowByKeyValue(INDglEquipmentType.EditValue)

        'Dim gridView = INDglEquipmentType.Properties.View
        'Dim rowHandle = gridView.FocusedRowHandle
        Dim ListPartsAccesoriesConsumables As New List(Of PartsAccesoriesConsumables)

        Using Model As New MEquipamentType
            If VarEquipmentType IsNot Nothing Then
                AsyncLoader(True)
                EquipmentType = Await Model.GetEquipamentTypeAsync(VarEquipmentType.Code)
                AsyncLoader(False)
            End If
        End Using

        If EquipmentType IsNot Nothing Then
            If EquipmentType.EquipmentTypePartsAccesoriesConsumibles IsNot Nothing AndAlso EquipmentType.EquipmentTypePartsAccesoriesConsumibles.Count() > 0 Then

                For Each varEquipmentTypeTechnicalLog As EquipmentTypePartsAccesoriesConsumibles In EquipmentType.EquipmentTypePartsAccesoriesConsumibles
                    ListPartsAccesoriesConsumables.Add(varEquipmentTypeTechnicalLog.PartsAccesoriesConsumables)
                Next
                ListEquipmentTypePartsAccesoriesConsumibles = EquipmentType.EquipmentTypePartsAccesoriesConsumibles.ToList()
                INDgcListUnitMeasure.Visible = True
                INDlyItemListUnitMeasure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ListadoPartesAccesoriosConsumibles = ListPartsAccesoriesConsumables
                INDgcListUnitMeasure.DataSource = ListPartsAccesoriesConsumables

                Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                LogicaBotonActualizar(True)
            Else
                ListadoPartesAccesoriosConsumibles = New List(Of PartsAccesoriesConsumables)
                EquipmentTypeTechnicalLog = New EquipmentTypeTechnicalLog()
                INDlyItemListUnitMeasure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDgcListUnitMeasure.Visible = False
                Me.BarraBotones.PrepareToolbar(eAction.Save)
            End If
        End If
    End Sub
End Class