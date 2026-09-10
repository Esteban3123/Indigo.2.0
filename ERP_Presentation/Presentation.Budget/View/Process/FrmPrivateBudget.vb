'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Daniel Eduardo Arévalo Bonilla
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
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Columns
Imports System.Collections.CollectionBase

#End Region

Public Class FrmPrivateBudget

    Implements IcrudBase

#Region "Properties"


#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de unidades de radicacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListPrivateBudget As List(Of SP_ListPrivateBudget_Result)
        Get
            Return INDTreeList.DataSource
        End Get
        Set(value As List(Of SP_ListPrivateBudget_Result))
            INDTreeList.DataSource = value
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
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        'Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        LoadStructure()
        LoadStatus()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True

    End Sub


    ''' <summary>
    ''' Loads the structure.
    ''' </summary>
    Public Async Sub LoadStructure()
        Try
            Using Model As New MPrivateBudget
                AsyncLoader(True)
                Dim _structure = Await Model.GetBudgetPrivate()
                AsyncLoader(False)
                If _structure IsNot Nothing Then
                    ListPrivateBudget = _structure.ObjectEmbbeded
                End If

                INDTreeList.RefreshDataSource()
                INDTreeList.ExpandAll()
            End Using
        Catch ex As Exception
            AsyncLoader(False)
        End Try

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
                MonthNameCopy = DirectCast(DirectCast(sender, DevExpress.Utils.Menu.DXMenuItem).Collection.Owner, DevExpress.XtraTreeList.Menu.TreeListColumnMenu).Column.Caption
            Case "02"
                MonthNamePaste = DirectCast(DirectCast(sender, DevExpress.Utils.Menu.DXMenuItem).Collection.Owner, DevExpress.XtraTreeList.Menu.TreeListColumnMenu).Column.Caption
                PasteValues()
        End Select
    End Sub

#End Region


#Region "PopupMenuShowing"

    ''' <summary>
    ''' Evento para agregar el menu al treeList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlStruct_PopupMenuShowing(sender As Object, e As DevExpress.XtraTreeList.PopupMenuShowingEventArgs) Handles INDTreeList.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()

        Dim addLevelText As String = "Copiar"
        Dim ItemMenuAddLevel As DXMenuItem = New DXMenuItem(addLevelText, AddressOf ContexMenuActions_Click)
        ItemMenuAddLevel.Tag = "01"

        Dim consultModifyText As String = "Pegar"
        Dim ItemMenuConsultModify As DXMenuItem = New DXMenuItem(consultModifyText, AddressOf ContexMenuActions_Click)
        ItemMenuConsultModify.Tag = "02"

        e.Menu.Items.Add(ItemMenuAddLevel)
        e.Menu.Items.Add(ItemMenuConsultModify)
    End Sub

#End Region

#End Region

#Region "Methods"
    Dim MonthNameCopy As String
    Dim MonthNamePaste As String
    ''' <summary>
    ''' Metodo que abre el showPopup del formulario FrmAddSupplierType
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PasteValues(Optional ByVal optionForm As Boolean = False)

        For Each item In ListPrivateBudget
            Dim d = item.GetType().GetProperties().Where(Function(o) o.Name.Equals(MonthNameCopy)).FirstOrDefault()
            Select Case MonthNamePaste
                Case "Enero"
                    item.Enero = d.GetValue(item)
                Case "Febrero"
                    item.Febrero = d.GetValue(item)
                Case "Marzo"
                    item.Marzo = d.GetValue(item)
                Case "Abril"
                    item.Abril = d.GetValue(item)
                Case "Mayo"
                    item.Mayo = d.GetValue(item)
                Case "Junio"
                    item.Junio = d.GetValue(item)
                Case "Julio"
                    item.Julio = d.GetValue(item)
                Case "Agosto"
                    item.Agosto = d.GetValue(item)
                Case "Septiembre"
                    item.Septiembre = d.GetValue(item)
                Case "Octubre"
                    item.Octubre = d.GetValue(item)
                Case "Noviembre"
                    item.Noviembre = d.GetValue(item)
                Case "Diciembre"
                    item.Diciembre = d.GetValue(item)
            End Select
        Next

        INDTreeList.RefreshDataSource()

    End Sub


    Private Sub SelectNode(ByVal node As TreeListNode)
        node.Selected = True
    End Sub
    '


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
        Guardar()
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

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        ' If ValidateControls() = True Then
        'AssigningValues()
        Using model As New MPrivateBudget()
            AsyncLoader(True)
            Dim Result = Await model.SavePrivateBudget(ListPrivateBudget, 0)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Me.ListPrivateBudget = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
                INDTreeList.RefreshDataSource()
                INDTreeList.ExpandAll()
                'RaiseEvent RefreshDatasourceStruct()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
        'End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub



    Private Sub UpdateValues(id As Integer, monthName As String)
        'Obtengo el registro en el cual estoy parado
        Dim category = ListPrivateBudget.FindAll(Function(item) item.IdVista = id).FirstOrDefault
        If category.IdParent IsNot Nothing Then
            'Obtengo el padre del registro en el cual estoy parado
            Dim categoryParent = ListPrivateBudget.FindAll(Function(item) item.IdVista = category.IdParent).FirstOrDefault
            'Obtengo todos los hijos del padre obtenido
            Dim ListCategoryChields = (From l In ListPrivateBudget Where l.IdParent = categoryParent.IdVista Select l).ToList
            'Sumo los valores
            Dim sum As Decimal = 0
            Select Case monthName
                Case "Enero"
                    sum = ListCategoryChields.Sum(Function(item) item.Enero)
                    categoryParent.Enero = sum
                Case "Febrero"
                    sum = ListCategoryChields.Sum(Function(item) item.Febrero)
                    categoryParent.Febrero = sum
                Case "Marzo"
                    sum = ListCategoryChields.Sum(Function(item) item.Marzo)
                    categoryParent.Marzo = sum
                Case "Abril"
                    sum = ListCategoryChields.Sum(Function(item) item.Abril)
                    categoryParent.Abril = sum
                Case "Mayo"
                    sum = ListCategoryChields.Sum(Function(item) item.Mayo)
                    categoryParent.Mayo = sum
                Case "Junio"
                    sum = ListCategoryChields.Sum(Function(item) item.Junio)
                    categoryParent.Junio = sum
                Case "Julio"
                    sum = ListCategoryChields.Sum(Function(item) item.Julio)
                    categoryParent.Julio = sum
                Case "Agosto"
                    sum = ListCategoryChields.Sum(Function(item) item.Agosto)
                    categoryParent.Agosto = sum
                Case "Septiembre"
                    sum = ListCategoryChields.Sum(Function(item) item.Septiembre)
                    categoryParent.Septiembre = sum
                Case "Octubre"
                    sum = ListCategoryChields.Sum(Function(item) item.Octubre)
                    categoryParent.Octubre = sum
                Case "Noviembre"
                    sum = ListCategoryChields.Sum(Function(item) item.Noviembre)
                    categoryParent.Noviembre = sum
                Case "Diciembre"
                    sum = ListCategoryChields.Sum(Function(item) item.Diciembre)
                    categoryParent.Diciembre = sum
            End Select
            'Repito el proceso con el padre
            UpdateValues(categoryParent.IdVista, monthName)
        End If
    End Sub

    Private Sub INDrepValueEnero_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDrepValueEnero.KeyDown, INDrepValueFebrero.KeyDown, INDrepValueMarzo.KeyDown, INDrepValueAbril.KeyDown, INDrepValueMayo.KeyDown, INDrepValueJunio.KeyDown, INDrepValueJulio.KeyDown, INDrepValueAgosto.KeyDown, INDrepValueSeptiembre.KeyDown, INDrepValueOctubre.KeyDown, INDrepValueNoviembre.KeyDown, INDrepValueDiciembre.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Dim MonthName = DirectCast(DirectCast(sender, System.Windows.Forms.Control).Parent, DevExpress.XtraTreeList.TreeList).FocusedColumn.Caption
            Dim control = CType(sender, DevExpress.XtraEditors.SpinEdit)
            Dim entity As SP_ListPrivateBudget_Result = CType(INDTreeList.GetDataRecordByNode(INDTreeList.FocusedNode), SP_ListPrivateBudget_Result)
            Select Case MonthName
                Case "Enero"
                    entity.Enero = control.EditValue
                Case "Febrero"
                    entity.Febrero = control.EditValue
                Case "Marzo"
                    entity.Marzo = control.EditValue
                Case "Abril"
                    entity.Abril = control.EditValue
                Case "Mayo"
                    entity.Mayo = control.EditValue
                Case "Junio"
                    entity.Junio = control.EditValue
                Case "Julio"
                    entity.Julio = control.EditValue
                Case "Agosto"
                    entity.Agosto = control.EditValue
                Case "Septiembre"
                    entity.Septiembre = control.EditValue
                Case "Octubre"
                    entity.Octubre = control.EditValue
                Case "Noviembre"
                    entity.Noviembre = control.EditValue
                Case "Diciembre"
                    entity.Diciembre = control.EditValue
            End Select
            UpdateValues(entity.IdVista, MonthName)
            INDTreeList.RefreshDataSource()
        End If
    End Sub


#End Region

End Class