'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Budget.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Controls.MVP
Imports DevExpress.Utils.Menu
Imports Infrastructure.Data.Xpo.BudgetRepository

#End Region

Public Class FrmPrivateBudgetItemsStructure
    Implements IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Listado de unidades de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListPrivateBudgetItemsStructure As List(Of PrivateBudgetItemsStructure)
        Get
            Return INDtlStruct.DataSource
        End Get
        Set(value As List(Of PrivateBudgetItemsStructure))
            INDtlStruct.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PPrivateBudgetItemsStructure

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Libera la memoria al cerrar el Frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
    End Sub

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPrivateBudgetItemsStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        'Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPrivateBudgetItemsStructure()
        LoadStructure()
        HideButtons()
        LoadStatus()
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Handles the Click event of the ContexMenuActions control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub ContexMenuActions_Click(sender As Object, e As EventArgs)
        Select Case (sender.Tag)
            Case "01" 'Agrega un nivel
                OpenFormAdd(1)
            Case "02" 'Modifica
                OpenFormAdd(2)
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddStruct_Click(sender As Object, e As EventArgs) Handles INDbtnAddStruct.Click
        OpenFormAdd()
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento para agregar el menu al treeList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlStruct_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDtlStruct.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()

        'Se obtiene el registro xpo
        Dim PrivateBudgetItemsStructureXpo As PrivateBudgetItemsStructureXpo = CType(INDtlStruct.GetDataRecordByNode(INDtlStruct.FocusedNode), PrivateBudgetItemsStructureXpo)

        'Se valida si es de ultimo nivel para no dejar agregar mas hijos
        If PrivateBudgetItemsStructureXpo.Type = 1 Then
            Dim addLevelText As String = "Agregar Nivel"
            Dim ItemMenuAddLevel As DXMenuItem = New DXMenuItem(addLevelText, AddressOf ContexMenuActions_Click)
            ItemMenuAddLevel.Tag = "01"
            e.Menu.Items.Add(ItemMenuAddLevel)
        End If

        Dim consultModifyText As String = "Modificar"
        Dim ItemMenuConsultModify As DXMenuItem = New DXMenuItem(consultModifyText, AddressOf ContexMenuActions_Click)
        ItemMenuConsultModify.Tag = "02"
        e.Menu.Items.Add(ItemMenuConsultModify)
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Oculta los botones de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideButtons()
        Me.BarraBotones.Minimizar(True)
        Me.BarraBotones.OperatingUnitVisible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
    End Sub

    ''' <summary>
    ''' Loads the structure.
    ''' </summary>
    Public Sub LoadStructure()
        INDtlStruct.DataSource = Presenter.ListPrivateBudgetItemsStructureXpo()
        INDtlStruct.RefreshDataSource()
        INDtlStruct.ExpandAll()
    End Sub

    ''' <summary>
    ''' Metodo que abre el showPopup del formulario FrmAddSupplierType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAdd(Optional ByVal EditMode As Integer = 0)
        Dim PrivateBudgetItemsStructureXpo As PrivateBudgetItemsStructureXpo = CType(INDtlStruct.GetDataRecordByNode(INDtlStruct.FocusedNode), PrivateBudgetItemsStructureXpo)
        Using Formulario As New FrmAddPrivateBudgetItemsStructure
            Formulario.ViewModeEditHold = True
            AddHandler Formulario.RefreshDatasourceStruct, AddressOf LoadStructure

            If EditMode = 2 Then 'Modifica
                Formulario.Code = PrivateBudgetItemsStructureXpo.Code
            ElseIf EditMode = 1 Then 'Agrega nivel
                Formulario.PivotParentId = PrivateBudgetItemsStructureXpo.Id
                Formulario.PivotParentDescription = PrivateBudgetItemsStructureXpo.CodeDescription
            End If
            Formulario.EditMode = EditMode
            Formulario.Owner = Me.MdiParent
            Formulario.Size = New Size(970, 750)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(Formulario, False)
            transparent.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>    
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        'If _record IsNot Nothing AndAlso _record.Id > 0 Then
        '    Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
        '        Await RunAsyncOperation(ModelCommonTreasury.DeleteBlockRecordTreasury(record))
        '        record = Nothing
        '    End Using
        'End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)

        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        'INDlcRoot.BeginUpdate()
        'ActionsOnControls = False

        'INDlcRoot.EndUpdate()

        'Me.BarraBotones.StatusRecordVisible = False
        'Me.BarraBotones.StatusRecord = Nothing
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'Me.BarraBotones.CleanAuditBasic()
    End Sub

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
        'If Me._doc Is Nothing Then
        '    Me._doc = New IndexedDocument2 With { _
        '        .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashRegister.Code, Me.CashRegister.Name, Me.CashRegister.InitialDate), _
        '        .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
        '        .IdEntity =  "$#" & Me.Tag & "_" & Me.CashRegister.Code & "#$", .IdForm = Me.Tag, _
        '        .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashRegister.Code), _
        '        .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        'Else
        '    Me._doc.Update = dateServer
        '    Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        '    Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.CashRegister.Code, Me.CashRegister.Name, Me.CashRegister.InitialDate)
        '    Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.CashRegister.Code)
        'End If
        Return Me._doc
    End Function

#End Region

#Region "BarButton Events"
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing Then
        '    Me._idOperativeUnit = operatingUnit.Id
        'End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer

    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo

    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub
#End Region

#Region "ICrud"
    ''' <summary>
    ''' Metodos del tiop CRUD implementados desde la interfaz IcrudBase 
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

End Class