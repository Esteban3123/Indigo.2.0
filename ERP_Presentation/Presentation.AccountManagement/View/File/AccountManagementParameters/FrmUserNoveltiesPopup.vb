#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.AccountManagement.MVP
Imports Presentation.Base
Imports Presentation.Controls

#End Region
Public Class FrmUserNoveltiesPopup

#Region "Builder"
    Public Sub New(_User As UsersAssignment, Optional _Novelties As UserNovelties = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _Novelties IsNot Nothing Then
            INDSbAddNovelty.Text = ResourceManager.GetString("Edit")
            _editMode = True
            Novelties = _Novelties
        End If
        SelectedUser = _User
    End Sub
#End Region

#Region "Globals"
    ''' <summary>
    ''' Novedades del usuario
    ''' </summary>
    Dim Novelties As UserNovelties

    ''' <summary>
    ''' Usuario seleccionado anteriormente y a quien está ligada la novedad
    ''' </summary>
    Dim SelectedUser As UsersAssignment
#End Region

#Region "Properties"

    ''' <summary>
    ''' Bandera que permite conocer si el popup se abrió en modo edición o creación de novedad
    ''' </summary>
    Dim _editMode As Boolean = False
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    ''' <summary>
    ''' Instancia de mensaje tipo notificación
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la novedad
    ''' </summary>
    ''' <returns></returns>
    Public Property NoveltyDate As Date
        Get
            Return INDDeNoveltyDate.EditValue
        End Get
        Set(value As Date)
            INDDeNoveltyDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Estado de la novedad, ligada al estado del usuario en la asignación.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsUserActive As Integer
        Get
            Return INDLeNoveltyStatus.EditValue
        End Get
        Set(value As Integer)
            INDLeNoveltyStatus.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción de la novedad.
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String
        Get
            Return INDTeNoveltyDescription.Text
        End Get
        Set(value As String)
            INDTeNoveltyDescription.Text = value
        End Set
    End Property
#End Region

#Region "Events"
    ''' <summary>
    ''' Evento para creación-modificación de la novedad.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="novelty"></param>
    ''' <param name="_editMode"></param>
    Public Event AddUserNovelty(sender As Object, novelty As UserNovelties, _editMode As Boolean)
#End Region

#Region "Handlers"

#Region "Disposed"

    ''' <summary>
    ''' Limpieza de recursos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Novelties = Nothing
        NoveltyDate = Nothing
        IsUserActive = Nothing
        Description = Nothing
    End Sub
#End Region

#Region "Load"
    ''' <summary>
    ''' Evento de carga inicial del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmUserNoveltiesPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True

        LoadLookupEdit()
        If _editMode = True Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub
#End Region

#Region "Activated"
    ''' <summary>
    ''' Establece el focus en el primer editcontrol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmUserNoveltiesPopup_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDDeNoveltyDate.Focus()
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento click en el botón de agregar o modificar novedad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbAddNovelty_Click(sender As Object, e As EventArgs) Handles INDSbAddNovelty.Click

        Using model As New MAccountManagementParameters(Me.Tag)
            Dim userWithDistribuitedAccounts = Await model.GetUsersWithDistributedAccounts()
            If userWithDistribuitedAccounts.StateResult = True Then
                Dim userIds = userWithDistribuitedAccounts.ObjectEmbbeded.Select(Function(x) x.Id).ToList()

                If userIds.Contains(SelectedUser.Id) And IsUserActive = 0 Then
                    Mensaje(EeventViewerImages.MensajeError) = "No se puede crear la novedad de inactivación. El usuario posee ingresos o folios distribuidos."
                    Me.Close()

                    Return
                End If
            End If
        End Using

        INDSbAddNovelty.Enabled = False

        Dim _novelty As UserNovelties
        Dim _editFlag As Boolean
        If Novelties Is Nothing Then
            Novelties = New UserNovelties()
        End If

        Novelties.AssignedUserId = SelectedUser.Id
        Novelties.NoveltyDate = NoveltyDate
        Novelties.IsUserActive = IsUserActive
        Novelties.Description = Description

        _editFlag = _editMode
        _novelty = Novelties

        'Se dispara el evento de creación o modificación de la novedad, para que se le de manejo desde el formulario que lo escucha
        RaiseEvent AddUserNovelty(Me, _novelty, _editFlag)
        INDSbAddNovelty.Enabled = True

        Novelties = Nothing
        Me.Close()

        Mensaje(EeventViewerImages.Informacion) = "Se agregó la novedad"

    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpieza de los controles y propiedades
    ''' </summary>
    Private Sub CleanControls()
        INDlyNoveltyPopup.BeginUpdate()
        Novelties = Nothing
        INDDeNoveltyDate.Text = String.Empty
        INDDeNoveltyDate.EditValue = Nothing
        INDLeNoveltyStatus.Text = String.Empty
        INDLeNoveltyStatus.EditValue = Nothing
        INDTeNoveltyDescription.Text = String.Empty
        INDTeNoveltyDescription.EditValue = Nothing
        _editMode = False
        INDlyNoveltyPopup.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los controles y sus valores
    ''' </summary>
    Private Sub LoadControls()
        INDSbAddNovelty.Text = ResourceManager.GetString("Edit")

        With Novelties
            INDlyNoveltyPopup.BeginUpdate()

            NoveltyDate = Novelties.NoveltyDate
            INDDeNoveltyDate.Enabled = False

            IsUserActive = If(Novelties.IsUserActive, 1, 0)
            INDLeNoveltyStatus.Enabled = False

            Description = Novelties.Description
            INDTeNoveltyDescription.Enabled = True

            INDlyNoveltyPopup.EndUpdate()
        End With
    End Sub

    ''' <summary>
    ''' Carga el LookupEdit de IsUserActive con valores predeterminados
    ''' </summary>
    Private Sub LoadLookupEdit()

        Dim estados = New List(Of Object) From {
            New With {.Valor = 1, .Descripcion = "Activo"},
            New With {.Valor = 0, .Descripcion = "Inactivo"}
        }

        With INDLeNoveltyStatus.Properties
            .DataSource = estados
            .DisplayMember = "Descripcion"
            .ValueMember = "Valor"
            .NullText = String.Empty
        End With

    End Sub
#End Region

#Region "BarraBotones"
    ''' <summary>
    ''' Evento click del botón deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region
End Class