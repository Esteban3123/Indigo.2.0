'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 15-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.InteropCost.MVP
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

#End Region

Public Class FrmOrganizationalStructure
    Implements IcrudBase

#Region "Properties and Variables"

    Public Const MODULE_NAME As String = "InteropCost"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Listado de la estructura de la organizacion
    ''' </summary>
    Public Property ListOrganizationStruct As List(Of OrganizationalStructureOfCosts)
        Get
            Return CType(INDtlStruct.DataSource, List(Of OrganizationalStructureOfCosts))
        End Get
        Set(value As List(Of OrganizationalStructureOfCosts))
            INDtlStruct.DataSource = value
        End Set
    End Property
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmOrganizationalStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        LoadStructure()
        Me.BarraBotones.Minimizar(True)
        Me.BarraBotones.OperatingUnitVisible = False
        Dim _listActions As New List(Of eAcciones)()
        '_listActions.Add()
        LoadStatus()
    End Sub


    ''' <summary>
    ''' Loads the structure.
    ''' </summary>
    Public Sub LoadStructure()
        Using Model As New MBusqueda
            Dim _structure As XPCollection = Model.ConsultarEntidades(eDataSource.ListOrganizationalStructureData)
            If _structure IsNot Nothing Then
                ListOrganizationStruct = New List(Of Domain.Entities.OrganizationalStructureOfCosts)()
                For Each itemXpo As Infrastructure.Data.Xpo.InteropCostRepository.OrganizationalStructureOfCostsXpo In _structure
                    Dim _item As New OrganizationalStructureOfCosts()
                    With _item
                        .Id = itemXpo.Id
                        .Code = itemXpo.Code
                        .Name = itemXpo.Name
                        If itemXpo.ParentId IsNot Nothing Then
                            .ParentId = itemXpo.ParentId.Id
                        End If
                        .Status = itemXpo.Status
                        .MarkAsModified()
                    End With
                    ListOrganizationStruct.Add(_item)
                Next
            End If
            INDtlStruct.RefreshDataSource()
            INDtlStruct.ExpandAll()
        End Using
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmOrganizationalStructure_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
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
            Case "01" 'Agregar nivel
                OpenFormAddStructure(False)
            Case "02" 'Modificar nivel
                OpenFormAddStructure(True)
            Case "03" 'Eliminar nivel
                OpenFormAddStructure(True)
        End Select
    End Sub

#End Region

    ''' <summary>
    ''' Handles the Click event of the INDsbAddOrgStruct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddOrgStruct_Click(sender As Object, e As EventArgs) Handles INDsbAddOrgStruct.Click
        Dim _structOrganiz As OrganizationalStructureOfCosts = CType(INDtlStruct.GetDataRecordByNode(INDtlStruct.FocusedNode), Domain.Entities.OrganizationalStructureOfCosts)
        If _structOrganiz IsNot Nothing OrElse ListOrganizationStruct.Count = 0 Then
            Using Formulario As New FrmAddStructure
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                If _structOrganiz IsNot Nothing Then
                    Formulario.ParentId = _structOrganiz.Id
                End If
                AddHandler Formulario.RefreshDatasourceStruct, AddressOf LoadStructure
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione el registro padre"
        End If
    End Sub

    ''' <summary>
    ''' Handles the PopupMenuShowing event of the INDtlStruct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraTreeList.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
    Private Sub INDtlStruct_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDtlStruct.PopupMenuShowing
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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que abre el form de la estructura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAddStructure(modeEdit As Boolean)
        Dim _structOrganiz As OrganizationalStructureOfCosts = CType(INDtlStruct.GetDataRecordByNode(INDtlStruct.FocusedNode), Domain.Entities.OrganizationalStructureOfCosts)
        Using Formulario As New FrmAddStructure
            Formulario.ViewModeEditHold = True
            Formulario.MinimizeBox = False
            Formulario.MaximizeBox = False
            Formulario.ParentId = _structOrganiz.Id
            Formulario.ModeEdit = modeEdit
            Formulario.Code = _structOrganiz.Code
            AddHandler Formulario.RefreshDatasourceStruct, AddressOf LoadStructure
            Formulario.Size = New Size(780, 700)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(Formulario, False)
            transparent.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
        'If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
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
        INDlcRoot.BeginUpdate()
        ActionsOnControls = False

        INDlcRoot.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
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
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
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

    Public WriteOnly Property Mensaje1(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region

End Class