'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 05-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de ubicaciones
''' </summary>
Public Class FrmEquipmentType

    Implements IEquipmentType


#Region "Variable Globales Propiedades Intefaz y Load"

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    Dim Counter As Integer

    Dim TmpListEquipmentType As New List(Of FixedAssetItemType)

    ' ''' <summary>
    ' ''' Esta propiedad contiene el estado del tipo de poliza
    ' ''' </summary>
    'Public Property StateEquipmentType As Boolean Implements IEquipmentType.StateEquipmentType
    '    Get
    '        Return BarraBotones.StatusRecord
    '    End Get
    '    Set(value As Boolean)
    '        BarraBotones.StatusRecord = value
    '    End Set
    'End Property

    Public WriteOnly Property EquipmentTypeDatasource As List(Of FixedAssetItemType) Implements IEquipmentType.EquipmentTypeDataSource
        Set(value As List(Of FixedAssetItemType))
            INDTreeListEquipmentType.DataSource = value
            'INDTreeListEquipmentType.ExpandAll()

        End Set
    End Property
    ''' <summary>
    ''' Variable que contiene el listado de tipos de equipos
    ''' </summary>
    Dim EquipmentTypeList As List(Of FixedAssetItemType)
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MEquipamentType
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PEquipamentType

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        Counter = Nothing
        TmpListEquipmentType = Nothing
        EquipmentTypeList = Nothing
        Model = Nothing
        Presenter = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Cargamos de manera asincrona definiciones del funcional
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.LoadStatus()
        LoadControls()
        Presenter = New PEquipamentType(Me)
        Deshacer()
    End Sub


#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Save() Implements IcrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
                Exit Sub
            End If
            'If Not EquipmentTypeList.Where(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Or _
            '                               x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified _
            '                               Or x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted).Count = 0 Then
            If DeletedList.Count > 0 Then
                For i = 0 To EquipmentTypeList.Count - 1
                    If EquipmentTypeList.Item(i).ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added Then
                        EquipmentTypeList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                    End If
                Next
                For i = 0 To DeletedList.Count - 1
                    If DeletedList.Item(i).ChangeTracker.State <> Domain.Base.Entities.ObjectState.Added Then
                        DeletedList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        EquipmentTypeList.Add(DeletedList.Item(i))
                    End If
                Next
            End If
            Using Model As New MEquipamentType
                AsyncLoader(True)
                Dim resultSave = Await Model.SaveEquipamentTypeAsync(EquipmentTypeList)
                AsyncLoader(False)
                If resultSave = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
            INDTreeListEquipmentType.RefreshDataSource()
            Deshacer()
        Catch ex As Exception
            AsyncLoader(False)
        End Try

        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If EquipmentType IsNot Nothing And INDBteCode.Enabled = False Then
        '    If EquipmentType.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), Botones.SiNo, MessageType.Question, Me.Text) = System.Windows.Forms.DialogResult.Yes Then
        '            Using Model As New MEquipamentType
        '                If Await Model.DeleteEquipmentTypeAsync(EquipmentType) = True Then
        '                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                    CleanControls()
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                End If
        '            End Using
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTipoPoliza, TiposPoliza)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTipoPoliza, TiposPoliza)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
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
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
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
                MessageIndigo.Show(value, Botones.Aceptar, MessageType.Errores, Me.Text)
            End If
        End Set
    End Property
#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Activo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "Inactivo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        EquipmentTypeDatasource = Nothing
        TmpListEquipmentType = New List(Of FixedAssetItemType)
        EquipmentTypeList = New List(Of FixedAssetItemType)
        DeletedList = New List(Of FixedAssetItemType)
        LoadControls()
        DeletedList = New List(Of FixedAssetItemType)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEquipmentType.ActionsOnControls
        Set(value As Boolean)
        End Set
    End Property


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Sub LoadControls()
        AsyncLoader(True)
        Dim _structure As XPCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).FixedAsset.ListAllEquipmentType()
        If _structure IsNot Nothing Then
            EquipmentTypeList = New List(Of Domain.Entities.FixedAssetItemType)()
            For Each itemXpo As Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetEquipmentTypeXpo In _structure

                Dim _item As New FixedAssetItemType()
                With _item
                    .Id = itemXpo.Id
                    .Code = itemXpo.Code
                    .Name = itemXpo.Name
                    .InventoryTypeId = itemXpo.InventoryTypeId.Id
                    .NameInventoryType = itemXpo.InventoryTypeId.Name
                    If itemXpo.ParentId IsNot Nothing Then
                        .ParentId = itemXpo.ParentId.Id
                    End If
                    .Status = itemXpo.Status
                    .MarkAsModified()
                End With
                EquipmentTypeList.Add(_item)
            Next
        End If
        INDTreeListEquipmentType.DataSource = EquipmentTypeList
        INDTreeListEquipmentType.RefreshDataSource()
        INDTreeListEquipmentType.ExpandAll()
        AsyncLoader(False)
        'Using Model As New MEquipamentType
        '    AsyncLoader(True)
        '    EquipmentTypeList = Await Model.ListAllEquipamentType
        '    If EquipmentTypeList IsNot Nothing AndAlso EquipmentTypeList.Count > 0 Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
        '        EquipmentTypeDatasource = EquipmentTypeList
        '        Counter = EquipmentTypeList.Item(EquipmentTypeList.Count - 1).Id
        '    Else
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        '    AsyncLoader(False)
        'End Using
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
    End Function


    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDBteCodeKindship_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        AbrirBusqueda()
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
        CleanControls()
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
        Save()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        'Save()
        LoadControls()
    End Sub
#End Region

    Dim DeletedList As List(Of FixedAssetItemType)


    Private Sub INDRepositoryItemButtonEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDRepositoryItemButtonEdit.ButtonClick

        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenFormAddStructure(False)
        ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
            OpenFormAddStructure(True)
        Else
            OpenFormAddStructure(True)
        End If

        'Dim EquipmentTypeParent = DirectCast(INDTreeListEquipmentType.GetDataRecordByNode(INDTreeListEquipmentType.FocusedNode), Domain.Entities.FixedAssetItemType)
        'If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
        '    Dim AddEquipmentType As New FrmAddEquipmentType
        '    Dim frmTransparent As New FrmTransparent(AddEquipmentType, False)
        '    AddEquipmentType.INDLciInventoryType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '    If frmTransparent.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

        '        If TmpListEquipmentType IsNot Nothing Then
        '            If TmpListEquipmentType.Where(Function(x) x.Code = AddEquipmentType.INDtxtCode.Text.Trim).Count > 0 Then
        '                Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el mismo codigo"
        '                Return
        '            End If
        '        End If


        '        If EquipmentTypeList.Where(Function(x) x.Code = AddEquipmentType.INDtxtCode.Text.Trim).Count = 0 Then
        '            'If EquipmentTypeList.Where(Function(x) x.FixedAssetItemType1.Where(Function(y) y.Code = AddEquipmentType.INDtxtCode.Text.Trim).Count = 0).Count = 0 Then

        '            EquipmentTypeParent.FixedAssetItemType1.Add(New FixedAssetItemType With {.Code = AddEquipmentType.INDtxtCode.Text.Trim, .Name = AddEquipmentType.INDtxtDescription.Text.Trim, .InventoryTypeId = EquipmentTypeParent.InventoryTypeId, .CreationUser = indigo.AuditMessageWcf.CodeUser, .CreationDate = Date.Now(), .Status = True})
        '            Counter += 1
        '            'End If
        '            TmpListEquipmentType.Add(New FixedAssetItemType With {.Code = AddEquipmentType.INDtxtCode.Text.Trim, .Name = AddEquipmentType.INDtxtDescription.Text.Trim, .InventoryTypeId = EquipmentTypeParent.InventoryTypeId, .CreationUser = indigo.AuditMessageWcf.CodeUser, .CreationDate = Date.Now(), .Status = True})
        '        Else
        '            Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el mismo codigo"
        '        End If
        '    End If
        '    EquipmentTypeList.Remove(EquipmentTypeParent)
        '    EquipmentTypeList.Add(EquipmentTypeParent)
        'ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
        '    'Editar
        '    Dim ModifyLocation As FixedAssetItemType = EquipmentTypeParent
        '    Dim AddEquipmentType As New FrmAddEquipmentType

        '    AddEquipmentType.INDtxtCode.EditValue = ModifyLocation.Code
        '    AddEquipmentType.INDtxtDescription.EditValue = ModifyLocation.Name

        '    AddEquipmentType.INDLciInventoryType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


        '    Dim frmTransparent As New FrmTransparent(AddEquipmentType, False)
        '    frmTransparent.ShowDialog()

        '    If AddEquipmentType.INDAceptar = True Then

        '        ModifyLocation.Code = AddEquipmentType.INDtxtCode.Text.Trim

        '        ModifyLocation.Name = AddEquipmentType.INDtxtDescription.Text

        '        ModifyLocation.ModificationDate = Date.Now
        '        ModifyLocation.ModificationUser = indigo.AuditMessageWcf.CodeUser

        '        EquipmentTypeParent = ModifyLocation

        '    End If
        'Else
        '    If MessageIndigo.Show("Seguro Desea Eliminar el Registro y sus Hijos?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Dim NumeroEliminados As Integer = 0
        '        ' y campturamos el valor del nodo seleccionado
        '        Dim ID As String = INDTreeListEquipmentType.FocusedNode.GetValue("Id").ToString.Trim
        '        'Iteramos sobre el Listado completo
        '        For i = 0 To EquipmentTypeList.Count - 1
        '            'Preguntamos si el valor del nodo seleccionado es igual a la posicion del objeto
        '            If ID = EquipmentTypeList.Item(i - NumeroEliminados).ParentId.ToString Then
        '                ID = EquipmentTypeList.Item(i - NumeroEliminados).Id
        '                DeletedList.Add(EquipmentTypeList.Item(i - NumeroEliminados))
        '                EquipmentTypeList.RemoveAt(i - NumeroEliminados)
        '                NumeroEliminados = NumeroEliminados + 1
        '            End If
        '        Next
        '        For i As Integer = 0 To EquipmentTypeList.Count - 1
        '            ID = INDTreeListEquipmentType.FocusedNode.GetValue("Id").ToString.Trim
        '            'Eliminamos La unidad de Mercadeo Seleccionada
        '            If EquipmentTypeList.Item(i).Id.ToString.Trim = ID.ToString.Trim Then
        '                DeletedList.Add(EquipmentTypeList.Item(i))
        '                EquipmentTypeList.RemoveAt(i)
        '                Exit For
        '            End If
        '        Next

        '    End If
        'End If
        'EquipmentTypeDatasource = Nothing
        'EquipmentTypeDatasource = EquipmentTypeList
        'INDTreeListEquipmentType.RefreshDataSource()
        'INDTreeListEquipmentType.ExpandAll()
    End Sub

    ''' <summary>
    ''' Handles the PopupMenuShowing event of the INDtlStruct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraTreeList.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDtlStruct_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDTreeListEquipmentType.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()

        Dim addItem As String = "Agregar Nivel"
        Dim ItemMenuAdd As DXMenuItem = New DXMenuItem(addItem, AddressOf ContexMenuActions_Click)
        ItemMenuAdd.Tag = "01"

        Dim modifyItem As String = "Modificar Nivel"
        Dim ItemMenuModify As DXMenuItem = New DXMenuItem(modifyItem, AddressOf ContexMenuActions_Click)
        ItemMenuModify.Tag = "02"

        Dim deleteItem As String = "Eliminar Nivel"
        Dim ItemMenuDelete As DXMenuItem = New DXMenuItem(deleteItem, AddressOf ContexMenuActions_Click)
        ItemMenuDelete.Tag = "03"

        e.Menu.Items.Add(ItemMenuAdd)
        e.Menu.Items.Add(ItemMenuModify)
        e.Menu.Items.Add(ItemMenuDelete)
    End Sub

    Private Sub ContexMenuActions_Click(sender As Object, e As EventArgs)
        Select Case (sender.Tag)
            Case "01" 'Agregar nivel
                OpenFormAddStructure(False)
            Case "02" 'Modificar nivel
                OpenFormAddStructure(True)
            Case "03" 'Eliminar nivel
                OpenFormAddStructure(True)
        End Select
    End Sub


    ''' <summary>
    ''' Método que abre el form de la estructura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAddStructure(modeEdit As Boolean)
        Dim _structOrganiz As FixedAssetItemType = CType(INDTreeListEquipmentType.GetDataRecordByNode(INDTreeListEquipmentType.FocusedNode), Domain.Entities.FixedAssetItemType)
        Using Formulario As New FrmAddEquipmentType
            Formulario.ViewModeEditHold = True
            Formulario.MinimizeBox = False
            Formulario.MaximizeBox = False
            Formulario.ParentId = _structOrganiz.Id
            Formulario.ModeEdit = modeEdit
            Formulario.Code = _structOrganiz.Code
            AddHandler Formulario.RefreshDatasourceEquipmentType, AddressOf LoadControls
            Dim transparent As New FrmTransparent(Formulario, False)
            transparent.ShowDialog()
        End Using
    End Sub

    Private Sub INDbtnAddRoot_Click(sender As Object, e As EventArgs) Handles INDbtnAddRoot.Click
        Dim _typeSelected As FixedAssetItemType = CType(INDTreeListEquipmentType.GetDataRecordByNode(INDTreeListEquipmentType.FocusedNode), FixedAssetItemType)
        Dim AddEquipmentType As New FrmAddEquipmentType
        AddEquipmentType.ViewModeEditHold = True
        If _typeSelected IsNot Nothing Then
            AddEquipmentType.ParentId = _typeSelected.Id
        End If
        AddHandler AddEquipmentType.RefreshDatasourceEquipmentType, AddressOf LoadControls
        Dim frmTransparent As New FrmTransparent(AddEquipmentType, False)
        frmTransparent.ShowDialog()
        'If frmTransparent.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        '    'If EquipmentTypeList IsNot Nothing AndAlso EquipmentTypeList.Count() > 0 Then
        '    '    If EquipmentTypeList.Where(Function(x) x.Code = AddEquipmentType.INDtxtCode.Text.Trim).Count = 0 Then
        '    '        EquipmentTypeList.Add(New FixedAssetItemType With {.Code = AddEquipmentType.INDtxtCode.Text.Trim, .Name = AddEquipmentType.INDtxtDescription.Text.Trim, .InventoryTypeId = AddEquipmentType.INDSleInventoryType.EditValue, .NameInventoryType = AddEquipmentType.INDSleInventoryType.Text, .CreationUser = indigo.AuditMessageWcf.CodeUser, .CreationDate = Date.Now(), .Status = True})
        '    '        Counter += 1
        '    '    Else
        '    '        Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el mismo codigo"
        '    '    End If
        '    'Else
        '    '    EquipmentTypeList = New List(Of FixedAssetItemType)
        '    '    EquipmentTypeList.Add(New FixedAssetItemType With {.Code = AddEquipmentType.INDtxtCode.Text.Trim, .Name = AddEquipmentType.INDtxtDescription.Text.Trim, .InventoryTypeId = AddEquipmentType.INDSleInventoryType.EditValue, .NameInventoryType = AddEquipmentType.INDSleInventoryType.Text, .CreationUser = indigo.AuditMessageWcf.CodeUser, .CreationDate = Date.Now(), .Status = True})
        '    '    Counter += 1
        '    'End If
        'End If
        'EquipmentTypeDatasource = Nothing
        'EquipmentTypeDatasource = EquipmentTypeList

        ''Save()

        'INDTreeListEquipmentType.RefreshDataSource()
        'INDTreeListEquipmentType.ExpandAll()
    End Sub

    Private Sub INDTreeListEquipmentType_InvalidNodeException(sender As Object, e As DevExpress.XtraTreeList.InvalidNodeExceptionEventArgs) Handles INDTreeListEquipmentType.InvalidNodeException
        Mensaje(EeventViewerImages.Advertencia) = "Por favor actualice antes de ingresar otro nodo principal"
    End Sub
End Class