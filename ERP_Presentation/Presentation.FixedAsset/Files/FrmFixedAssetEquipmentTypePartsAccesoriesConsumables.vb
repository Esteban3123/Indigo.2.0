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

Imports System.ComponentModel
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de registro tecnico
''' </summary>
Public Class FrmFixedAssetEquipmentTypePartsAccesoriesConsumables
    Implements IFixedAssetEquipmentTypePartsAccesoriesConsumibles

#Region "Constantes"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"
#End Region

#Region "Variable Globales Propiedades Intefaz y Load"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Private Presenter As PFixedAssetEquipmentTypePartsAccesoriesConsumables

    ''' <summary>
    ''' Variable que contiene el consumible
    ''' </summary>
    Private ListEquipmentTypePartsAccesoriesConsumibles As New List(Of FixedAssetItemTypePartsAccesories)

    ''' <summary>
    ''' variable que contiene el listado de los registros eliminados
    ''' </summary>
    Private DeleteList As New List(Of FixedAssetItemTypePartsAccesories)


    Public Property IdEquipmentType As Integer Implements IFixedAssetEquipmentTypePartsAccesoriesConsumibles.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer)
            INDglEquipmentType.EditValue = value
        End Set
    End Property

    Public Property IdPartsAccesoriesConsumibles As Integer Implements IFixedAssetEquipmentTypePartsAccesoriesConsumibles.IdPartsAccesoriesConsumibles
        Get
            Return INDglPartsAccesories.EditValue
        End Get
        Set(value As Integer)
            INDglPartsAccesories.EditValue = value
        End Set
    End Property

    Public WriteOnly Property ListEquipmentType As List(Of FixedAssetItemType) Implements IFixedAssetEquipmentTypePartsAccesoriesConsumibles.ListEquipmentType
        Set(value As List(Of FixedAssetItemType))
            INDglEquipmentType.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property ListPartsAccesoriesConsumables As List(Of FixedAssetPartsAccesoriesConsumables) Implements IFixedAssetEquipmentTypePartsAccesoriesConsumibles.ListPartsAccesoriesConsumibles
        Set(value As List(Of FixedAssetPartsAccesoriesConsumables))
            INDglPartsAccesories.Properties.DataSource = value
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements IFixedAssetEquipmentTypePartsAccesoriesConsumibles.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListEquipmentTypePartsAccesoriesConsumibles = Nothing
        Presenter = Nothing
        DeleteList = Nothing
        record = Nothing
    End Sub


    Private Async Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            AsyncLoader(True)
            Me._doc = Nothing

            IndigoGridView1.SetListAcction(INDgcListPartsAccesoriesConsumablesView, {eAcciones.Remove}.ToList)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgcListPartsAccesoriesConsumablesView.Columns
                If col.Name = "colActions" Then
                    col.Width = 100
                End If
            Next

            Me.LoadStatus()
            Presenter = New PFixedAssetEquipmentTypePartsAccesoriesConsumables(Me)
            Await Presenter.Initializes()
            Deshacer()
            AsyncLoader(False)

        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If DeleteList.Count > 0 Then
            For Each Item In DeleteList
                If Item.Id <> 0 Then
                    ListEquipmentTypePartsAccesoriesConsumibles.Add(Item)
                End If
            Next
        End If

        Try
            Using Model As New MFixedAssetEquipmentTypePartsAccesoriesConsumables(Me.MyTag)
                AsyncLoader(True)
                Dim result = Await Model.SaveEquipmentTypePartsAccesoriesConsumablesAsync(ListEquipmentTypePartsAccesoriesConsumibles)
                AsyncLoader(False)
                If result = True Then
                    Mensaje(EeventViewerImages.Informacion) = "Se Almacenó Correctamente"
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
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If ListEquipmentTypePartsAccesoriesConsumibles IsNot Nothing Then
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFixedAssetEquipmentTypePartsAccesoriesConsumables(Me.MyTag)
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEquipmentTypePartsAccesoriesConsumablesAsync(ListEquipmentTypePartsAccesoriesConsumibles)
                        If result = True Then
                            If Me._doc IsNot Nothing Then
                                Await Me.DeleteDocumentIndexed()
                            End If
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
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
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
        ListEquipmentTypePartsAccesoriesConsumibles = New List(Of FixedAssetItemTypePartsAccesories)
        INDglEquipmentType.EditValue = Nothing
        INDgcListPartsAccesoriesConsumables.DataSource = Nothing
        INDglPartsAccesories.EditValue = Nothing
        INDlyItemListPartsAccesoriesConsumables.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        DeleteList.Clear()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    Private Sub INDbtnAgregar_Click(sender As Object, e As EventArgs) Handles INDbtnAgregar.Click
        If IdPartsAccesoriesConsumibles = Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Parte"
            Exit Sub
        End If

        If ListEquipmentTypePartsAccesoriesConsumibles.Any(Function(x) x.PartsAccesoriesConsumiblesId = IdPartsAccesoriesConsumibles) Then
            Mensaje(EeventViewerImages.Advertencia) = "La parte, Accesorio o Consumible ya existe en la lista"
            INDglPartsAccesories.EditValue = Nothing
            Exit Sub
        End If

        ListEquipmentTypePartsAccesoriesConsumibles.Add(New FixedAssetItemTypePartsAccesories With {.EquipmentTypeId = IdEquipmentType, .PartsAccesoriesConsumiblesId = IdPartsAccesoriesConsumibles,
                                                        .FixedAssetPartsAccesoriesConsumablesCode = INDglPartsAccesories.Properties.View.GetFocusedRowCellValue("Code").ToString.Trim,
                                                        .FixedAssetPartsAccesoriesConsumablesName = INDglPartsAccesories.Properties.View.GetFocusedRowCellValue("Name").ToString.Trim
                                                        })

        If ListEquipmentTypePartsAccesoriesConsumibles.Count > 0 Then
            INDlyItemListPartsAccesoriesConsumables.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If

        INDglPartsAccesories.EditValue = Nothing
        INDgcListPartsAccesoriesConsumables.DataSource = Nothing
        INDgcListPartsAccesoriesConsumables.DataSource = ListEquipmentTypePartsAccesoriesConsumibles
        INDgcListPartsAccesoriesConsumables.RefreshDataSource()
    End Sub

    Private Sub FrmTechnicalLog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim _FixedAssetItemTypePartsAccesories = DirectCast(INDgcListPartsAccesoriesConsumablesView.GetFocusedRow, FixedAssetItemTypePartsAccesories)

        _FixedAssetItemTypePartsAccesories.MarkAsDeleted()
        DeleteList.Add(_FixedAssetItemTypePartsAccesories)
        ListEquipmentTypePartsAccesoriesConsumibles.Remove(_FixedAssetItemTypePartsAccesories)

        INDgcListPartsAccesoriesConsumables.DataSource = Nothing
        INDgcListPartsAccesoriesConsumables.DataSource = ListEquipmentTypePartsAccesoriesConsumibles
    End Sub

    Private Async Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
        Dim VarEquipmentType = INDglEquipmentType.Properties.GetRowByKeyValue(INDglEquipmentType.EditValue)
        Dim FixedAssetEquipmentType As New FixedAssetItemType

        Using Model As New MEquipamentType
            If VarEquipmentType IsNot Nothing Then
                AsyncLoader(True)
                FixedAssetEquipmentType = Await Model.GetEquipamentTypeAsync(VarEquipmentType.Code)
                AsyncLoader(False)
            End If
        End Using

        If FixedAssetEquipmentType IsNot Nothing Then
            If FixedAssetEquipmentType.FixedAssetItemTypePartsAccesories IsNot Nothing AndAlso FixedAssetEquipmentType.FixedAssetItemTypePartsAccesories.Count() > 0 Then

                ListEquipmentTypePartsAccesoriesConsumibles = FixedAssetEquipmentType.FixedAssetItemTypePartsAccesories.ToList()
                INDgcListPartsAccesoriesConsumables.Visible = True
                INDlyItemListPartsAccesoriesConsumables.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDgcListPartsAccesoriesConsumables.DataSource = ListEquipmentTypePartsAccesoriesConsumibles
                Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
            Else
                ListEquipmentTypePartsAccesoriesConsumibles = New List(Of FixedAssetItemTypePartsAccesories)
                INDlyItemListPartsAccesoriesConsumables.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDgcListPartsAccesoriesConsumables.Visible = False
                Me.BarraBotones.PrepareToolbar(eAction.Save)
            End If
        End If
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

    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub
#End Region
End Class