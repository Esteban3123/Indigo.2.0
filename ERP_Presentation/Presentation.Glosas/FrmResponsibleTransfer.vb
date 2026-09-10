'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Glosas.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports DevExpress.Utils.Menu
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

''' <summary>
''' Formulario de Reasignación de Responsables
''' </summary>
Public Class FrmResponsibleTransfer
    Implements IResponsibleTransfer

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene el Responsable
    ''' </summary>
    Dim Responsible As Responsible
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MResponsibleTransfer
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PResponsibleTransfer
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigoSessionValues As SessionValues

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Lista de movimientos por responsables
    ''' </summary>
    Private _listResposibleMovements As List(Of ResponsibleMovements)
    ''' <summary>
    ''' Objeto Movimiento Responsable
    ''' </summary>
    Private _responsibleMovement As ResponsibleMovements

#End Region

#Region "Properties"

    ''' <summary>
    ''' propiedad que contiene los datos para listar responsables
    ''' en el control gridLookUpEdit
    ''' </summary>
    Public Property DataSourceResponsibleGle As Object Implements IResponsibleTransfer.DataSourceResponsibleGle
        Get
            Return Me.INDResponsibleGle.Properties.DataSource
        End Get
        Set(value As Object)
            Me.INDResponsibleGle.Properties.DataSource = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub FrmResponsibleTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MResponsibleTransfer(Me.Tag)
        Me.indigoSessionValues = SessionValues.Instance
        '******************************'
        'Me.Funct = AddressOf GenerateDoc
        Presenter = New PResponsibleTransfer(Me)
        Presenter.Initialize(Me.Tag)
        Deshacer()
    End Sub

    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch

    End Sub

    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        Me.INDResponsibleGle.EditValue = Nothing
        Me.INDResponsiblesGc.DataSource = Nothing
        Me.IndigoGridControl1.RefreshGrid(INDResponsiblesGc)
        If Me._listResposibleMovements IsNot Nothing Then
            Me._listResposibleMovements.Clear()
        End If
        Me._responsibleMovement = Nothing
    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

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

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Private Async Sub INDLoadSmb_Click(sender As Object, e As EventArgs) Handles INDLoadSmb.Click
        If INDResponsibleGle.EditValue IsNot Nothing Then
            Me.INDResponsiblesGc.DataSource = Await Me.Model.ListAllResponsibleTransfer(INDResponsibleGle.EditValue)
        End If
    End Sub

    Private Sub INDResponsiblesGv_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDResponsiblesGv.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            e.Menu.Items.Add(New DXMenuItem("Reasignación Responsable", AddressOf ShowTransferPopupResponsible, My.Resources.GlosaBorde))
            e.Menu.Items.Add(New DXMenuItem("Reasignación Concepto", AddressOf ShowTransferPopupConcept, My.Resources.GlosaBorde))
            e.Menu.Items.Add(New DXMenuItem("Reasignación Causante", AddressOf ShowResponsibleThirdParty, My.Resources.GlosaBorde))
        End If
    End Sub

    Sub ShowTransferPopupResponsible()
        Me._listResposibleMovements = New List(Of ResponsibleMovements)
        For Each item As Integer In INDResponsiblesGv.GetSelectedRows()
            If item > -1 Then
                Dim data As ResponsibleMovements = TryCast(INDResponsiblesGv.GetRow(item), Domain.Entities.ResponsibleMovements)
                Me._listResposibleMovements.Add(data)
            End If
        Next
        Dim frmResponsibleTransferPopup As New FrmResponsibleTransferPopup(Me.Tag, Me.INDResponsibleGle.EditValue, 1)
        frmResponsibleTransferPopup.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTransparent As New FrmTransparent(frmResponsibleTransferPopup, False)
        frmTransparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTransparent.ShowDialog
        If result = System.Windows.Forms.DialogResult.OK Then
            For Each item As ResponsibleMovements In Me._listResposibleMovements
                item.IdResponsible = frmResponsibleTransferPopup._responsibleId
            Next
            TransferProcess(1)
        End If
    End Sub

    Sub ShowTransferPopupConcept()
        Me._listResposibleMovements = New List(Of ResponsibleMovements)
        For Each item As Integer In INDResponsiblesGv.GetSelectedRows()
            If item > -1 Then
                Dim data As ResponsibleMovements = TryCast(INDResponsiblesGv.GetRow(item), Domain.Entities.ResponsibleMovements)
                Me._listResposibleMovements.Add(data)
            End If
        Next
        Dim frmResponsibleTransferPopup As New FrmResponsibleTransferPopup(Me.Tag, Me.INDResponsibleGle.EditValue, 2)
        frmResponsibleTransferPopup.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTransparent As New FrmTransparent(frmResponsibleTransferPopup, False)
        frmTransparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTransparent.ShowDialog
        If result = System.Windows.Forms.DialogResult.OK Then
            For Each item As ResponsibleMovements In Me._listResposibleMovements
                item.IdConceptGlosa = frmResponsibleTransferPopup._ConceptId
            Next
            TransferProcess(2)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para disparar el formulario de causante de glosa.
    ''' </summary>
    Sub ShowResponsibleThirdParty()
        Me._listResposibleMovements = New List(Of ResponsibleMovements)
        For Each item As Integer In INDResponsiblesGv.GetSelectedRows()
            If item > -1 Then
                Dim data As ResponsibleMovements = TryCast(INDResponsiblesGv.GetRow(item), Domain.Entities.ResponsibleMovements)
                Me._listResposibleMovements.Add(data)
            End If
        Next
        If Me._listResposibleMovements.Count > 0 Then
            Dim ResponsibleThirdParty As FrmResponsibleThirdPartyPopUp = New FrmResponsibleThirdPartyPopUp()
            ResponsibleThirdParty.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim result = ResponsibleThirdParty.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                For Each item As ResponsibleMovements In Me._listResposibleMovements
                    item.ResponsibleThirdPartyId = ResponsibleThirdParty.ResponsibleThirdPartyId
                Next
                TransferProcess(3)
            End If
        End If
    End Sub


    Private Sub INDTransferRepositoryBte_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDTransferRepositoryBte.ButtonClick
        Me._responsibleMovement = CType(Me.INDResponsiblesGv.GetRow(Me.INDResponsiblesGv.FocusedRowHandle), Domain.Entities.ResponsibleMovements)
        Dim frmResponsibleTransferPopup As New FrmResponsibleTransferPopup(Me.Tag, Me.INDResponsibleGle.EditValue, 1)
        frmResponsibleTransferPopup.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTransparent As New FrmTransparent(frmResponsibleTransferPopup, False)
        frmTransparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTransparent.ShowDialog
        If result = System.Windows.Forms.DialogResult.OK Then
            If Me._listResposibleMovements Is Nothing Then
                Me._listResposibleMovements = New List(Of ResponsibleMovements)
            Else
                Me._listResposibleMovements.Clear()
            End If
            Me._responsibleMovement.IdResponsible = frmResponsibleTransferPopup._responsibleId
            Me._listResposibleMovements.Add(Me._responsibleMovement)
            'TransferProcess()
        End If
    End Sub

    Async Sub TransferProcess(opt As Integer)
        Dim response = Await Me.Model.TransferResponsibleMovement(Me._listResposibleMovements, opt)
        If response.StateResult Then
            If response.MessageResult.Count > 0 Then
                Dim strMensaje As String = String.Empty
                For i As Integer = 0 To response.MessageResult.Count - 1
                    strMensaje += response.MessageResult(i).ToString + Environment.NewLine
                Next
                Mensaje(EeventViewerImages.Informacion) = strMensaje
                Me.INDResponsiblesGc.DataSource = Await Me.Model.ListAllResponsibleTransfer(INDResponsibleGle.EditValue)
            Else
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado, Comunes)
                Me.INDResponsiblesGc.DataSource = Await Me.Model.ListAllResponsibleTransfer(INDResponsibleGle.EditValue)
            End If
        End If
    End Sub


    Private Sub INDResponsiblesGv_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDResponsiblesGv.CustomColumnDisplayText
        If e.Column.FieldName = "Proceso" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "1"
                        e.DisplayText = "Objeción"
                    Case "2"
                        e.DisplayText = "Reiteracion"
                End Select
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Responsible = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        _listResposibleMovements = Nothing
        _responsibleMovement = Nothing
    End Sub


    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.BarraBotones.RibbonPagEform.Visible = False
    End Sub

    Private Sub INDTransferResponsibleSmb_Click(sender As Object, e As EventArgs) Handles INDTransferResponsibleSmb.Click
        Me._responsibleMovement = CType(Me.INDResponsiblesGv.GetRow(Me.INDResponsiblesGv.FocusedRowHandle), Domain.Entities.ResponsibleMovements)
        Dim frmResponsibleTransferPopup As New FrmResponsibleTransferPopup(Me.Tag, Me.INDResponsibleGle.EditValue, 1)
        frmResponsibleTransferPopup.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTransparent As New FrmTransparent(frmResponsibleTransferPopup, False)
        frmTransparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTransparent.ShowDialog
        If result = System.Windows.Forms.DialogResult.OK Then
            If Me._listResposibleMovements Is Nothing Then
                Me._listResposibleMovements = New List(Of ResponsibleMovements)
            Else
                Me._listResposibleMovements.Clear()
            End If
            Me._responsibleMovement.IdResponsible = frmResponsibleTransferPopup._responsibleId
            Me._listResposibleMovements.Add(Me._responsibleMovement)
            TransferProcess(1)
        End If
    End Sub

    Private Sub INDTransferConceptSmb_Click(sender As Object, e As EventArgs) Handles INDTransferConceptSmb.Click
        Me._responsibleMovement = CType(Me.INDResponsiblesGv.GetRow(Me.INDResponsiblesGv.FocusedRowHandle), Domain.Entities.ResponsibleMovements)
        Dim frmResponsibleTransferPopup As New FrmResponsibleTransferPopup(Me.Tag, Me.INDResponsibleGle.EditValue, 2)
        frmResponsibleTransferPopup.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTransparent As New FrmTransparent(frmResponsibleTransferPopup, False)
        frmTransparent.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTransparent.ShowDialog
        If result = System.Windows.Forms.DialogResult.OK Then
            If Me._listResposibleMovements Is Nothing Then
                Me._listResposibleMovements = New List(Of ResponsibleMovements)
            Else
                Me._listResposibleMovements.Clear()
            End If
            Me._responsibleMovement.IdConceptGlosa = frmResponsibleTransferPopup._ConceptId
            Me._listResposibleMovements.Add(Me._responsibleMovement)
            TransferProcess(2)
        End If
    End Sub
End Class