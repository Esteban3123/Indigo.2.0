#Region "Imports"

Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Entities
Imports DevExpress.Utils.Menu
Imports Domain.Base.Entities
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DevExpress.Data.PLinq
Imports System.Drawing
Imports System.Resources
Imports System.Text
Imports Presentation.Payments.MVP
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraBars

#End Region

Public Class FrmAcceptanceTransfer
    Implements IAcceptanceTransfer

#Region "Fields"

    ''' <summary>
    ''' Bandera para indicar que el frontal ya se cargo
    ''' </summary>
    Private _loaded As Boolean = False
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Variable bandera para el registro bloqueado
    ''' </summary>
    Private _recordFlag As Boolean
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PAcceptanceTransfer
    ''' <summary>
    ''' Grupo en el que se encuentra ubicado el proceso
    ''' </summary>
    Private _currentGroup As EGroups
    ''' <summary>
    ''' Modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Private Model As MAcceptanceTransfer
    ''' <summary>
    ''' Id de la unidad de radicacion de destino
    ''' </summary>
    ''' <remarks></remarks>
    Private IdTarget As Integer
    ''' <summary>
    ''' Id de la cabecera de traslado de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private IdTransfer As Integer
    ''' <summary>
    ''' Alamcena la lista de unidades de radicacion a la que el usuario tiene permiso
    ''' </summary>
    ''' <remarks></remarks>
    Private listFillingUnitIdPermisson As List(Of Integer)
#End Region

#Region "Enums"

    ''' <summary>
    ''' Enumeración que indica el nivel en el que me ecuentro en el frontal
    ''' </summary>
    Private Enum EGroups
        ''' <summary>
        ''' Representa el grupo raiz del formulario
        ''' </summary>
        Root
        ''' <summary>
        ''' Representa el grupo de oficio seleccionado, listando sus facturas
        ''' </summary>
        SelectedDocument
    End Enum

#End Region

#Region "Property"
    ''' <summary>
    ''' Asigna el estado a los controles de la barra de herramientas segun el grupo seleccionado
    ''' </summary>
    ''' <value>Grupo seleccionado</value>
    Private WriteOnly Property ControlsStatus() As EGroups
        Set(value As EGroups)
            Me._currentGroup = value
            Select Case value
                Case EGroups.Root
                    Me.INDlygOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.IndNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    ' Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
                Case EGroups.SelectedDocument
                    Me.INDlygOffice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlygDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.IndNavigate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    ' Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
            End Select
        End Set
    End Property
#End Region

#Region "Icrud"
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IAcceptanceTransfer.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAcceptanceTransfer.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IAcceptanceTransfer.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Ejecuta la accion de actualizar rejilla 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_RefreshGrid() Handles BarraBotones.Click_RefreshGrid
        ListDocument()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    End Sub

#End Region

#Region "methods"

    Private Sub ListDocument()
        AsyncLoader(True)
        If listFillingUnitIdPermisson Is Nothing AndAlso listFillingUnitIdPermisson.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El usuario no tiene permiso en ninguna unidad de radicación"
            Exit Sub
        End If
        Me.IndgcTranfersPayable.DataSource = Model.ListAccountPayableTransferAcceptence(listFillingUnitIdPermisson)
        Me.IndgcTranfersPayable.RefreshDataSource()
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo que controla el regresar dependiendo de la ubicación
    ''' </summary>
    Sub back()
        Select Case Me._currentGroup
            Case EGroups.SelectedDocument
                Me.ControlsStatus = EGroups.Root
                ListDocument()
        End Select
    End Sub

    ''' <summary>
    ''' Muestra el detalle del oficio seleccionado
    ''' </summary>
    Dim transferType As Integer
    Private Sub DetailDocument()
        If Me.IndgvTranfersPayable.SelectedRowsCount > 0 Then
            Dim aux = Me.IndgvTranfersPayable.GetRow(Me.IndgvTranfersPayable.FocusedRowHandle)
            If aux IsNot Nothing Then
                Me.AsyncLoader(True)
                Me.INDlygDetail.Text = "Detalle Oficio N° " & aux.Code
                Me.INDlblSource.Text = aux.FilingSource
                Me.INDlbltarget.Text = aux.FilingTarget
                Me.IdTarget = aux.IdTarget
                Me.IdTransfer = aux.Id
                transferType = aux.TranferType
                If aux.TranferType = 1 Then
                    LoadDetail()
                Else
                    LoadDetailRefunds()
                End If

                Me.ControlsStatus = EGroups.SelectedDocument
                Me.AsyncLoader(False)
            End If
        End If
    End Sub

    Private Sub LoadDetail()
        Me.IndgcDetailTransfer.DataSource = Nothing
        Me.IndgcDetailTransfer.DataSource = Model.ListPaymentsAccountPayableTransferDetail(Me.IdTransfer)
        Me.IndgcDetailTransfer.RefreshDataSource()
        ' Me.IndgvDetailTransfer.ExpandAllGroups()

    End Sub

    Private Sub LoadDetailRefunds()
        Me.IndgcDetailTransfer.DataSource = Nothing
        Me.IndgcDetailTransfer.DataSource = Model.ListRefundAccountPayableTransferDetail(Me.IdTransfer)
        Me.IndgcDetailTransfer.RefreshDataSource()
        'Me.IndgvDetailTransfer.ExpandAllGroups()

    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _loaded = Nothing
        record = Nothing
        _recordFlag = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        _currentGroup = Nothing
        Model = Nothing
        IdTarget = Nothing
        IdTransfer = Nothing
        listFillingUnitIdPermisson = Nothing
    End Sub
    ''' <summary>
    ''' Load de Frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAcceptanceTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
        Me.LayoutControls.SetIsCustomizable(Me.INDlycAcceptenceTransfer, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = Nothing
        indigo = SessionValues.Instance
        Me.Model = New MAcceptanceTransfer(Me.Tag)
        Me.Presenter = New PAcceptanceTransfer(Me)
        Presenter.LoadDefinitionLayout()
        Deshacer()
        IndigoGridControl1.RefreshGrid(IndgcTranfersPayable)
        Me.ControlsStatus = EGroups.Root
        Me.CtrNavigation1.Group = INDlygDetail
        listFillingUnitIdPermisson = New List(Of Integer)
        Dim actionresult As ActionResult(Of List(Of FilingUnitUser)) = Await Model.ListFillingPermisoUser(indigo.AuditMessageWcf.CodeUser)
        For Each item As FilingUnitUser In actionresult.ObjectEmbbeded
            listFillingUnitIdPermisson.Add(item.FillingUnitId)
        Next
        ListDocument()
        Dim ListAction As New List(Of eAcciones)
        ListAction.Add(eAcciones.Acceptance)
        ListAction.Add(eAcciones.Reject)
        Me.IndigoGridView1.SetListAcction(IndgvDetailTransfer, ListAction)
        Me.IndgvDetailTransfer.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown
        For Each col As GridColumn In IndgvDetailTransfer.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Aqui se controla el regreso al nivel superior del proceso dependiendo en
    ''' cual me encuentre
    ''' </summary>
    Private Sub CtrNavigation_ClickBack() Handles CtrNavigation1.ClickBack
        back()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la accion de mostrar el detalle del oficio seleccionado en la rejilla
    ''' </summary>
    Private Sub IndbteSelectionOffice_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles IndbteSelectionOffice.ButtonClick
        Me.DetailDocument()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndgvTranfersPayable_DoubleClick(sender As Object, e As EventArgs) Handles IndgvTranfersPayable.DoubleClick
        DetailDocument()
    End Sub

    Private Sub IndgvTranfersPayable_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles IndgvTranfersPayable.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            If Not _recordFlag Then
                e.Menu.Items.Add(New DXMenuItem("Ver Detalle", AddressOf DetailDocument, My.Resources.editrow16))
            End If
        End If
    End Sub

    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs)
        Acceptance()
    End Sub

    Private Sub INDbtnInvalidate_Click(sender As Object, e As EventArgs)
        Rejection()
    End Sub

    Private Async Sub Rejection()
        Dim ListIdUpdate As New List(Of Integer)
        For Each item As Integer In IndgvDetailTransfer.GetSelectedRows()
            If item > -1 Then
                Dim data As Object = IndgvDetailTransfer.GetRow(item)
                If data IsNot Nothing Then
                    ListIdUpdate.Add(data.Id)
                End If
            End If
        Next
        If ListIdUpdate.Count = 0 Then
            Exit Sub
        End If
        Dim frmPopUp As New FrmRejectionComment()
        Using tras As New FrmTransparent(frmPopUp, False)
            If tras.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                Dim result As ActionResult = Await Model.SaveAcceptanceTranfer(ListIdUpdate, IdTarget, frmPopUp.IdRejection, frmPopUp.CommentRejection)
                Resultguardar(result)
            End If
        End Using
    End Sub

    Private Async Sub Acceptance()
        Dim ListIdUpdate As New List(Of Integer)
        For Each item As Integer In IndgvDetailTransfer.GetSelectedRows()
            If item > -1 Then
                Dim data As Object = IndgvDetailTransfer.GetRow(item)
                If data IsNot Nothing Then
                    ListIdUpdate.Add(data.Id)
                End If
            End If
        Next
        If ListIdUpdate.Count = 0 Then
            Exit Sub
        End If
        Dim result As ActionResult = Await Model.SaveAcceptanceTranfer(ListIdUpdate, IdTarget, Nothing, String.Empty)
        Resultguardar(result)
    End Sub

    Private Sub Resultguardar(result As ActionResult)
        If result.StateResult = True Then
            If transferType = 1 Then 'cuenta por pagar
                LoadDetail()
            Else 'reembolso
                LoadDetailRefunds()
            End If

            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        Else
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                Dim strinbuilder As New StringBuilder
                For Each item As String In result.MessageResult
                    strinbuilder.AppendLine(item)
                Next
                Mensaje(EeventViewerImages.MensajeError) = strinbuilder.ToString()
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim action = TryCast(sender, SimpleButton)
        Select Case (action.Tag)
            Case eAcciones.Acceptance.ToString()
                Acceptance()
            Case eAcciones.Reject.ToString()
                Rejection()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim action As BarButtonItem = TryCast(sender, BarButtonItem)
        Select Case (action.Tag)
            Case eAcciones.Acceptance.ToString()
                Acceptance()
            Case eAcciones.Reject.ToString()
                Rejection()
        End Select
    End Sub

#End Region
    
End Class