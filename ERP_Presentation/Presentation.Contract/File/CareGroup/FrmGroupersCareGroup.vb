'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
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
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.ContractRepository
Imports DevExpress.Xpo

#End Region

Public Class FrmGroupersCareGroup
    Implements IGroupersCareGroup

#Region "Event"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        SearchMode = Nothing
        GroupersCareGroup = Nothing
        GroupersCareGroupCups = Nothing
        GroupersCareGroupActivities = Nothing
        ListGroupersCareGroupCups = Nothing
        ListGroupersCareGroupActivities = Nothing
        ListDeleteGroupersCareGroupCups = Nothing
        ListDeleteGroupersCareGroupActivities = Nothing
        ListMeasurementUnit = Nothing
        ICareGroup = Nothing
        View = Nothing
        IdCareGroup = Nothing
    End Sub


    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddAddGroupersCareGroupEventArgs(sender As Object, e As AddGroupersCareGroup)

#End Region

#Region "Properties"



    Public Property GroupersId As Integer Implements IGroupersCareGroup.GroupersId
        Get
            Return INDSlGroupers.EditValue
        End Get
        Set(value As Integer)
            INDSlGroupers.EditValue = value
        End Set
    End Property

    Public Property MinimunRange As Integer Implements IGroupersCareGroup.MinimunRange
        Get
            Return INDTxtMinimunRange.EditValue
        End Get
        Set(value As Integer)
            INDTxtMinimunRange.EditValue = value
        End Set
    End Property

    Public Property MaximunRange As Integer Implements IGroupersCareGroup.MaximunRange
        Get
            Return INDtxtMaximunRange.EditValue
        End Get
        Set(value As Integer)
            INDtxtMaximunRange.EditValue = value
        End Set
    End Property

    Public Property MeasurementUnit As Integer Implements IGroupersCareGroup.MeasurementUnit
        Get
            Return INDSlMetricUnit.EditValue
        End Get
        Set(value As Integer)
            INDSlMetricUnit.EditValue = value
        End Set
    End Property


    Public Property WarningFor As Integer Implements IGroupersCareGroup.WarningFor
        Get
            Return INDtxtWarningFor.EditValue
        End Get
        Set(value As Integer)
            INDtxtWarningFor.EditValue = value
        End Set
    End Property

    Public Property WarningMessage As String Implements IGroupersCareGroup.WarningMessage
        Get
            Return INDMeWarningMessage.EditValue
        End Get
        Set(value As String)
            INDMeWarningMessage.EditValue = value
        End Set
    End Property

    Public Property MaximunRangeRestrict As Boolean Implements IGroupersCareGroup.MaximunRangeRestrict
        Get
            Return INDslMaximunRangeRestrict.EditValue
        End Get
        Set(value As Boolean)
            INDslMaximunRangeRestrict.EditValue = value
        End Set
    End Property

    Public Property RestrictMessage As String Implements IGroupersCareGroup.RestrictMessage
        Get
            Return INDMeRestrictMessage.EditValue
        End Get
        Set(value As String)
            INDMeRestrictMessage.EditValue = value
        End Set
    End Property

    Public Property CupsXpo As XPInstantFeedbackSource Implements IGroupersCareGroup.CUPSXpo
        Get
            Return INDslCups.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslCups.Properties.DataSource = value
        End Set
    End Property

    Public Property GroupersXpo As XPInstantFeedbackSource Implements IGroupersCareGroup.GroupersXpo
        Get
            Return INDSlGroupers.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlGroupers.Properties.DataSource = value
        End Set
    End Property

    Public Property ActivitiesXpo As XPInstantFeedbackSource Implements IGroupersCareGroup.ActivitiesXpo
        Get
            Return INDSlActivities.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlActivities.Properties.DataSource = value
        End Set
    End Property



#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PGroupersCareGroup

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean


    ''' <summary>
    ''' Entidad de definición de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupersCareGroup As GroupersCareGroup

    ''' <summary>
    ''' Representa el detalle de la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupersCareGroupCups As GroupersCareGroupCups


    Dim GroupersCareGroupActivities As GroupersCareGroupActivities


    ''' <summary>
    ''' Representa el listado del detalle de la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListGroupersCareGroupCups As List(Of GroupersCareGroupCups)

    Dim ListGroupersCareGroupActivities As List(Of GroupersCareGroupActivities)

    ''' <summary>
    ''' Listado de eliminados de condiciones de los detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteGroupersCareGroupCups As List(Of GroupersCareGroupCups)

    Dim ListDeleteGroupersCareGroupActivities As List(Of GroupersCareGroupActivities)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListMeasurementUnit As New List(Of Tuple(Of Integer, String))

    Private _listCompare As List(Of GroupersCareGroup)
    Public Property ListCompare As List(Of GroupersCareGroup)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of GroupersCareGroup))
            _listCompare = value
        End Set
    End Property

    Dim ICareGroup As ICareGroup

    Dim View As IGroupersCareGroup

    Public IdCareGroup As Integer


#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

#End Region

#Region "Methods"





    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.GroupersCareGroup IsNot Nothing AndAlso Me.GroupersCareGroup.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDSlGroupers.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDSlGroupers.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyDefinitionRate.BeginUpdate()
            INDSlGroupers.Enabled = Not value
            INDTxtMinimunRange.Enabled = value
            INDtxtMaximunRange.Enabled = value
            INDSlMetricUnit.Enabled = value
            INDtxtWarningFor.Enabled = value
            INDMeWarningMessage.Enabled = value
            INDslMaximunRangeRestrict.Enabled = value
            INDMeRestrictMessage.Enabled = value
            INDGcCups.Enabled = value
            INDBtnAddCups.Enabled = value
            INDgcActivities.Enabled = value
            INDBtnAddActivities.Enabled = value
            INDslCups.Enabled = value
            INDSlActivities.Enabled = value

            INDlyDefinitionRate.EndUpdate()
            If value Then
                INDTxtMinimunRange.Focus()
            Else
                INDSlGroupers.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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



    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch


    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDSlGroupers.Text = ReturnValue
        If INDSlGroupers.Text <> String.Empty Then
            LoadControls()
            If INDSlGroupers.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDSlGroupers.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2

    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyDefinitionRate.BeginUpdate()
        ActionsOnControls = False

        GroupersId = 0
        MinimunRange = 0
        MaximunRange = 0
        MeasurementUnit = 0
        WarningFor = 0
        WarningMessage = String.Empty
        MaximunRangeRestrict = False
        RestrictMessage = String.Empty

        ListGroupersCareGroupCups = Nothing
        ListGroupersCareGroupActivities = Nothing
        ListDeleteGroupersCareGroupCups = Nothing
        ListDeleteGroupersCareGroupActivities = Nothing
        INDGcCups.DataSource = Nothing
        INDgcActivities.DataSource = Nothing
        GroupersCareGroup = Nothing
        BarraBotones.CleanAuditBasic()
        INDlyDefinitionRate.EndUpdate()
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub



    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task

    End Function

    ''' <summary>
    ''' Metodo que carga el listado del detalle de la definicion de tarifa
    ''' se le envia el id de la definicion y el 
    ''' optionImportInfo(True=Viene desde el form ImportInfo, False=Viene desde el LoadControls)
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadListDetail(DefinitionRateId As Integer, optionImportInfo As Boolean) As Task

    End Function



    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Function LoadControls() As Task
        ActionsOnControls = True
        With GroupersCareGroup

            GroupersId = .GroupersId
            'MinimunRange = .MinimunRange
            'MaximunRange = .MaximunRange
            'MeasurementUnit = .MeasurementUnit
            'WarningFor = .WarningFor
            'WarningMessage = .WarningMessage
            'MaximunRangeRestrict = .MaximunRangeRestrict
            'RestrictMessage = .RestrictMessage

            If .GroupersCareGroupCups IsNot Nothing And .GroupersCareGroupCups.Count > 0 Then
                ListGroupersCareGroupCups = .GroupersCareGroupCups.ToList()
                INDGcCups.DataSource = ListGroupersCareGroupCups

            End If

            If .GroupersCareGroupActivities IsNot Nothing And .GroupersCareGroupActivities.Count > 0 Then
                ListGroupersCareGroupActivities = .GroupersCareGroupActivities.ToList()
                INDgcActivities.DataSource = ListGroupersCareGroupActivities

            End If


        End With
    End Function



#End Region



#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDefinitionRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyDefinitionRate, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        InitializeTuple()

        Presenter = New PGroupersCareGroup(Me)

        ActionsOnControls = False

        IndigoGridControl1.RefreshGrid(INDGcCups)
        IndigoGridControl1.RefreshGrid(INDgcActivities)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(ViewCups, ListActions)
        IndigoGridView2.SetListAcction(ViewActivities, ListActions)

        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)

        LoadStatus()

        SearchMode = False
    End Sub

    Private Sub InitializeTuple()
        'Tipo de adquisición
        ListMeasurementUnit = New List(Of Tuple(Of Integer, String))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(1, "Diario"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(2, "Semanal"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(3, "Mensual"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(4, "Bimensual"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(5, "Trimestral"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(6, "Semestral"))
        ListMeasurementUnit.Add(New Tuple(Of Integer, String)(7, "Anual"))
        INDSlMetricUnit.Properties.DataSource = ListMeasurementUnit.ToList


    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDefinitionRate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"



#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDefinitionRate_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Presenter.InitilizeCups()
        Presenter.InitilizeGroupers()
        Presenter.GetAllAGACTIMED()
    End Sub

#End Region



#Region "ContextMenu"

    ''' <summary>
    ''' Evento que se dispara al presionar sobre el menu alguna accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                DeleteCups()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al escoger sobre el menu alguna accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                DeleteCups()
        End Select
    End Sub


    ''' <summary>
    ''' Evento que se dispara al presionar sobre el menu alguna accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.ButtonEdit
        btn = DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
        Select Case btn.Text.ToString
            Case "Eliminar"
                DeleteActivities()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al escoger sobre el menu alguna accion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                DeleteActivities()
        End Select
    End Sub
#End Region

#Region "Delete"
    Private Sub DeleteCups()

        GroupersCareGroupCups = CType(ViewCups.GetFocusedRow, GroupersCareGroupCups)


        ListGroupersCareGroupCups.Remove(GroupersCareGroupCups)

        If GroupersCareGroupCups.Id > 0 Then
            If ListDeleteGroupersCareGroupCups Is Nothing Then
                ListDeleteGroupersCareGroupCups = New List(Of GroupersCareGroupCups)
            End If
            ListDeleteGroupersCareGroupCups.Add(GroupersCareGroupCups)

        End If

        INDGcCups.DataSource = Nothing
        INDGcCups.DataSource = ListGroupersCareGroupCups

    End Sub

    Private Sub DeleteActivities()

        GroupersCareGroupActivities = CType(ViewActivities.GetFocusedRow, GroupersCareGroupActivities)

        ListGroupersCareGroupActivities.Remove(GroupersCareGroupActivities)

        If GroupersCareGroupActivities.Id > 0 Then
            If ListDeleteGroupersCareGroupActivities Is Nothing Then
                ListDeleteGroupersCareGroupActivities = New List(Of GroupersCareGroupActivities)
            End If
            ListDeleteGroupersCareGroupActivities.Add(GroupersCareGroupActivities)

        End If

        INDgcActivities.DataSource = Nothing
        INDgcActivities.DataSource = ListGroupersCareGroupActivities

    End Sub



#Region "DataSourceChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcRules_DataSourceChanged(sender As Object, e As EventArgs)
        'viewRules.ExpandAllGroups()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
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
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit

    End Sub

    ''' <summary>
    ''' Barra Botones: ImportarInformación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion

    End Sub



    Private Sub INDbtnCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlGroupers.EditValueChanged
        'Bsucamos por agrupador
        Try

            If IdCareGroup > 0 AndAlso GroupersId > 0 Then
                ActionsOnControls = True
                AsyncLoader(True)
                GroupersCareGroup = Presenter.GetGroupersCareGroup(IdCareGroup, GroupersId)
                AsyncLoader(False)

                If GroupersCareGroup.Id > 0 Then
                    LoadControls()
                End If
            Else

                ActionsOnControls = True
                GroupersCareGroup = New GroupersCareGroup()
            End If


        Catch ex As Exception
            AsyncLoader(False)
            GroupersCareGroup = New GroupersCareGroup()

        End Try

    End Sub


    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AddItem()
    End Sub

    Private Sub AddItem()

        'If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then 'Se valida que el articulo no se encuentre en el form principal
        '    Dim cont = (From l In ListCompare Where l.GroupersId = GroupersId Select l).Count
        '    If cont > 0 Then
        '        Mensaje(EeventViewerImages.Advertencia) = "Esta configuración ya se encuentra en el listado."
        '        Exit Sub
        '    End If
        'End If

        'Se asigna los valores

        If ValidateControl() = False Then
            Exit Sub
        End If

        AssigningValues()

        'Se crea el objeto que se va a devolver
        Dim args As New AddGroupersCareGroup
        args.GrouperCareGroup = GroupersCareGroup
        args.ListDeleteGroupersCareGroupActivities = ListDeleteGroupersCareGroupActivities
        args.ListDeleteGroupersCareGroupCups = ListDeleteGroupersCareGroupCups

        RaiseEvent AddAddGroupersCareGroupEventArgs(Nothing, args)
        CleanControls()
        Me.Close()

    End Sub

#Region "Click"
    Private Sub INDBtnAddCups_Click(sender As Object, e As EventArgs) Handles INDBtnAddCups.Click

        AssigningValuesCups()

    End Sub

    Private Sub INDBtnAddActivities_Click(sender As Object, e As EventArgs) Handles INDBtnAddActivities.Click
        AssigningValuesActivities()
    End Sub
#End Region

#Region "AssignValues"
    Private Sub AssigningValuesCups()

        GroupersCareGroupCups = New GroupersCareGroupCups()

        With GroupersCareGroupCups
            .CUPSEntityId = INDslCups.EditValue
            .DescriptionCups = INDslCups.Text
        End With

        If ListGroupersCareGroupCups Is Nothing Then
            ListGroupersCareGroupCups = New List(Of GroupersCareGroupCups)
        End If


        If ListGroupersCareGroupCups.Where(Function(x) x.CUPSEntityId = INDslCups.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Este CUPS ya se encuentra en el listado."
            Exit Sub
        End If

        ListGroupersCareGroupCups.Add(GroupersCareGroupCups)

        INDGcCups.DataSource = Nothing
        INDGcCups.DataSource = ListGroupersCareGroupCups

        INDslCups.EditValue = Nothing

    End Sub

    Private Sub AssigningValuesActivities()

        GroupersCareGroupActivities = New GroupersCareGroupActivities()

        With GroupersCareGroupActivities
            .AGACTIMEDCode = INDSlActivities.EditValue
            .AGACTIMEDName = INDSlActivities.Text
        End With

        If ListGroupersCareGroupActivities Is Nothing Then
            ListGroupersCareGroupActivities = New List(Of GroupersCareGroupActivities)
        End If

        If ListGroupersCareGroupActivities.Where(Function(x) x.AGACTIMEDCode = INDSlActivities.EditValue).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Esta Actividad ya se encuentra en el listado."
            Exit Sub
        End If

        ListGroupersCareGroupActivities.Add(GroupersCareGroupActivities)



        INDgcActivities.DataSource = Nothing
        INDgcActivities.DataSource = ListGroupersCareGroupActivities

        INDSlActivities.EditValue = Nothing

    End Sub


    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()

        With GroupersCareGroup
            .GroupersId = GroupersId
            '.MinimunRange = MinimunRange
            '.MaximunRange = MaximunRange
            '.MeasurementUnit = MeasurementUnit
            '.WarningFor = WarningFor
            '.WarningMessage = WarningMessage
            '.MaximunRangeRestrict = MaximunRangeRestrict
            '.RestrictMessage = RestrictMessage

            .GroupersCareGroupActivities.Clear()
            .GroupersCareGroupCups.Clear()
            If ListGroupersCareGroupCups IsNot Nothing AndAlso ListGroupersCareGroupCups.Count > 0 Then
                For Each item In ListGroupersCareGroupCups
                    .GroupersCareGroupCups.Add(item)
                Next
            End If

            If ListGroupersCareGroupActivities IsNot Nothing AndAlso ListGroupersCareGroupActivities.Count > 0 Then
                For Each item In ListGroupersCareGroupActivities
                    .GroupersCareGroupActivities.Add(item)
                Next
            End If


            If ListDeleteGroupersCareGroupCups IsNot Nothing AndAlso ListDeleteGroupersCareGroupCups.Count > 0 Then
                For Each item In ListDeleteGroupersCareGroupCups
                    item.MarkAsDeleted()
                    .GroupersCareGroupCups.Add(item)
                Next
            End If

            If ListDeleteGroupersCareGroupActivities IsNot Nothing AndAlso ListDeleteGroupersCareGroupActivities.Count > 0 Then
                For Each item In ListDeleteGroupersCareGroupActivities
                    item.MarkAsDeleted()
                    .GroupersCareGroupActivities.Add(item)
                Next
            End If

        End With
    End Sub

    Private Function ValidateControl() As Boolean
        If INDSlGroupers.EditValue Is Nothing Or INDSlGroupers.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado Agrupadores"
            INDSlGroupers.Focus()
            Return False
        End If

        If INDTxtMinimunRange.EditValue Is Nothing Or INDTxtMinimunRange.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado Rango Mínimo"
            INDTxtMinimunRange.Focus()
            Return False
        End If

        If INDtxtMaximunRange.EditValue Is Nothing Or INDtxtMaximunRange.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado Rango Máximo"
            INDtxtMaximunRange.Focus()
            Return False
        End If

        If INDSlMetricUnit.EditValue Is Nothing Or INDSlMetricUnit.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado Unidad de Medida"
            INDSlMetricUnit.Focus()
            Return False
        End If


        If INDtxtWarningFor.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado el Advertir a partir de"
            INDtxtWarningFor.Focus()
            Return False
        End If

        If INDMeWarningMessage.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha Escrito el Mensaje de la Advertencia"
            INDMeWarningMessage.Focus()
            Return False
        End If

        If INDslMaximunRangeRestrict.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado el Restringir Máximo"
            INDslMaximunRangeRestrict.Focus()
            Return False
        End If

        Return True

    End Function
#End Region


    Private Sub INDslMaximunRangeRestrict_EditValueChanged(sender As Object, e As EventArgs) Handles INDslMaximunRangeRestrict.EditValueChanged
        If MaximunRangeRestrict Then
            INDLciMessageMaximunRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciMessageMaximunRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub




#End Region

End Class