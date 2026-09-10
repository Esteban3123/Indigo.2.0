Imports Infrastructure.CrossCutting.Base
Imports Presentation.AccountManagement.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports Domain.AccountManagement.Model
Imports Infrastructure.Data.Xpo.AccountManagementRespository

Public Class FrmFolioTransferPopup

#Region "Properties"

    ''' <summary>
    ''' Propiedad que obtiene el codigo del area de atención seleccionada
    ''' </summary>
    Public Property ManagementAreaSelected As Integer
        Get
            Return INDSleManagementArea_Popup.EditValue
        End Get
        Set(value As Integer)
            INDSleManagementArea_Popup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene el Id del usuario seleccionado para el traslado
    ''' </summary>
    Public Property UserSelected As String
        Get
            Return INDSleTransferUser_Popup.EditValue
        End Get
        Set(value As String)
            INDSleTransferUser_Popup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de las areas de gestión
    ''' </summary>
    Public Property ManagementAreasXpo As List(Of ManagementAreasXpo)
        Get
            Return CType(INDSleManagementArea_Popup.Properties.DataSource, List(Of ManagementAreasXpo))
        End Get
        Set(value As List(Of ManagementAreasXpo))
            INDSleManagementArea_Popup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los usuarios por area de gestión
    ''' </summary>
    Public Property ManagementAreasUsers As List(Of ManagementAreasUser)
        Get
            Return CType(INDSleTransferUser_Popup.Properties.DataSource, List(Of ManagementAreasUser))
        End Get
        Set(value As List(Of ManagementAreasUser))
            INDSleTransferUser_Popup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si el usuario aceptó el traslado
    ''' </summary>
    Public Property Accepted As Boolean = False

    ''' <summary>
    ''' Referencia al tag del formulario padre para operaciones async
    ''' </summary>
    Public Property ParentTag As Object

#End Region

#Region "Constructor"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento click del botón Trasladar
    ''' </summary>
    Private Sub INDSbTransfer_Click(sender As Object, e As EventArgs) Handles INDSbTransfer.Click
        If ManagementAreaSelected = 0 Then
            MessageIndigo.Show("Por favor seleccione un área de gestión.", MessageType.Warning, Me.Text)
            Exit Sub
        End If

        If String.IsNullOrEmpty(UserSelected) Then
            MessageIndigo.Show("Por favor seleccione un usuario.", MessageType.Warning, Me.Text)
            Exit Sub
        End If

        Accepted = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento de selección del area de gestión en el traslado de folios
    ''' </summary>
    Private Async Sub INDLeManagementArea_Popup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleManagementArea_Popup.EditValueChanged
        If ManagementAreaSelected = 0 OrElse ManagementAreasXpo Is Nothing Then
            Exit Sub
        End If

        Try
            Using model As New MDashboardAccountManagement()
                Dim result = Await model.GetManagementAreasWithUsers(ManagementAreasXpo.First(Function(x) x.Id = ManagementAreaSelected).Code, SessionValues.Instance.AuditMessageWcf)
                Dim managementArea = result.ObjectEmbbeded
                INDSleTransferUser_Popup.Properties.DataSource = managementArea.ManagementAreasUser.ToList()
            End Using
        Catch ex As Exception
            MessageIndigo.Show($"Error al cargar usuarios: {ex.Message}", MessageType.Errores, Me.Text)
        End Try
    End Sub

    ''' <summary>
    ''' Evento de carga del formulario
    ''' </summary>
    Private Sub FrmFolioTransferPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Limpiar valores previos
        ManagementAreaSelected = 0
        UserSelected = Nothing
        Accepted = False
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando el formulario pierde el foco
    ''' </summary>
    Private Sub FrmFolioTransferPopup_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        ' Cerrar el formulario cuando se hace clic fuera
        ' Esto proporciona una mejor experiencia de usuario
        Me.Close()
    End Sub

#End Region

End Class

