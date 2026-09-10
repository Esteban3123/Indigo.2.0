'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Payments.MVP
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

Public Class FrmSupplierType
    Implements IcrudBase

#Region "Properties"


#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de unidades de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListSupplierType As List(Of SupplierType)
        Get
            Return INDtlStruct.DataSource
        End Get
        Set(value As List(Of SupplierType))
            INDtlStruct.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Handles the Load event of the FrmOrganizationalStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSupplierType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlySupplierType, True)
        'Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        LoadStructure()
        HideButtons()
        LoadStatus()
    End Sub

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
        Using Model As New MBusqueda
            Dim _structure As XPCollection = Model.ConsultarEntidades(eDataSource.ListSupplierTypeData)
            If _structure IsNot Nothing Then
                ListSupplierType = New List(Of SupplierType)()
                For Each itemXpo As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_SupplierType In _structure
                    Dim _item As New SupplierType()
                    With _item
                        .Id = itemXpo.Id
                        .Code = itemXpo.Code
                        .Name = itemXpo.Name
                        .SupplierTypeDescription = itemXpo.CodeName
                        If itemXpo.ParentId IsNot Nothing Then
                            .ParentId = itemXpo.ParentId.Id
                        End If
                        .Status = itemXpo.Status
                        .MarkAsModified()
                    End With
                    ListSupplierType.Add(_item)
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
            Case "01"
                OpenFormAdd()
            Case "02"
                OpenFormAdd(True)
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

        Dim addLevelText As String = "Agregar Nivel"
        Dim ItemMenuAddLevel As DXMenuItem = New DXMenuItem(addLevelText, AddressOf ContexMenuActions_Click)
        ItemMenuAddLevel.Tag = "01"

        Dim consultModifyText As String = "Modificar"
        Dim ItemMenuConsultModify As DXMenuItem = New DXMenuItem(consultModifyText, AddressOf ContexMenuActions_Click)
        ItemMenuConsultModify.Tag = "02"

        e.Menu.Items.Add(ItemMenuAddLevel)
        e.Menu.Items.Add(ItemMenuConsultModify)
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que abre el showPopup del formulario FrmAddSupplierType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormAdd(Optional ByVal optionForm As Boolean = False)
        Dim _supplierType As SupplierType = CType(INDtlStruct.GetDataRecordByNode(INDtlStruct.FocusedNode), SupplierType)
        If _supplierType IsNot Nothing OrElse ListSupplierType Is Nothing OrElse ListSupplierType.Count = 0 Then
            Using Formulario As New FrmAddSupplierType
                Formulario.ViewModeEditHold = True
                If _supplierType IsNot Nothing Then
                    If optionForm Then
                        Formulario.CodeST = _supplierType.Code
                    Else
                        Formulario.ParentId = _supplierType.Id
                        Formulario.TextSearchStruct = _supplierType.SupplierTypeDescription
                    End If
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