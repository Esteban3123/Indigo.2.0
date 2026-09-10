#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
#End Region

Public Class FrmPopupObservations
    Implements IDashBoardPharmacyDetail

#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PDashBoardPharmacyDetail

    ''' <summary>
    ''' filtra el tipo de motivo general
    ''' </summary>
    Private _filterType As Integer?
#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece la observacion del paquete
    ''' </summary>
    Public Property Description As String
        Get
            Return INDMeDescription.EditValue
        End Get
        Set(value As String)
            INDMeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del motivo general
    ''' </summary>
    ''' <returns></returns>
    Public Property IdHCMOANULB As String
        Get
            Return INDsleReasonType.EditValue
        End Get
        Set(value As String)
            INDsleReasonType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que asinga el valor a la variable privada para el tipo del motivo general
    ''' </summary>
    Public WriteOnly Property FilterType As Integer?
        Set(value As Integer?)
            _filterType = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
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

    Public ReadOnly Property MyTag As Object Implements IDashBoardPharmacyDetail.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDashBoardPharmacyDetail.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public Property Sequence As InventorySequence Implements IDashBoardPharmacyDetail.Sequence
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As InventorySequence)
            Throw New NotImplementedException()
        End Set
    End Property

    Private WriteOnly Property ICrudBase_Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el formulario para Establecer
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 730)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDMeDescription.EditValue = Nothing
        INDMeDescription.Properties.NullText = Nothing
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupWareHouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        _presenter = New PDashBoardPharmacyDetail(Me)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        Me.Description = Nothing
    End Sub

#End Region

#Region "QueryPopUp"
    Private Sub INDsleReasonType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReasonType.QueryPopUp
        INDsleReasonType.Properties.DataSource = _presenter.ListHCMOANULBXPInstant(_filterType)
    End Sub
#End Region
#Region "KeyDown"
    Private Sub FrmPopupObservations_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region
#Region "Click"

    ''' <summary>
    ''' Click en Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickGuardar
        Dim Str = New StringBuilder()

        If String.IsNullOrEmpty(IdHCMOANULB) Then
            Str.AppendLine("Razón de Cambio")
            INDsleReasonType.Focus()
        End If
        If String.IsNullOrEmpty(Description) Then
            Str.AppendLine("Descripción")
        End If
        If Str.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = $"Falta llenar los siguientes campos: {Str.ToString()}"
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region
#Region "IcrudBase"
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
    End Sub
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub
#End Region

End Class