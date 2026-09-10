#Region "Imports"
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmAuthorizeUsers

#Region "Event"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardProductionSchedule

    ''' <summary>
    ''' Id de la campaña
    ''' </summary>
    Public campaignDetailId As Integer

    ''' <summary>
    ''' Tipo de dosis unitaria
    ''' </summary>
    Public _MSclass As EUnitDoseTypeClass

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Dim Result As List(Of ViewListAuthorizeUsersXpo)

    ''' <summary>
    ''' Listado de usuarios eliminados
    ''' </summary>
    Private ListDeleteAuthorizeUsers As List(Of CampaignDetailUsers)

    ''' <summary>
    ''' Representa la entidad Usuarios autorizados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _AuthorizeUsers As New CampaignDetailUsers

    Public Event OnSaveAuthorizeUsers(sender As Object, e As AuthorizedUserEventArgs)

    Public SaveUsers As Boolean


#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
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
    ''' Propiedad que contiene los usuarios autorizados
    ''' </summary>
    Public Property ListAuthorizeUsersDetail As List(Of CampaignDetailUsers)
        Get
            Return INDgcUser.DataSource
        End Get
        Set(value As List(Of CampaignDetailUsers))
            INDgcUser.DataSource = value
            INDgcUser.RefreshDataSource()
        End Set
    End Property

    Private _campaignCreationDate As Date
    Public Property CampaignCreationDate As Date
        Get
            Return _campaignCreationDate
        End Get
        Set(value As Date)
            _campaignCreationDate = value
            INDsleFechaProceso.Properties.MinValue = value
        End Set
    End Property

    Private _preparationTime As TimeSpan
    Public Property PreparationTime As TimeSpan
        Get
            Return _preparationTime
        End Get
        Set(value As TimeSpan)
            _preparationTime = value
        End Set
    End Property

    Private _campaignStatus As Byte
    Public Property CampaignStatus As Byte
        Get
            Return _campaignStatus
        End Get
        Set(value As Byte)
            _campaignStatus = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        INDsleFechaProceso.EditValue = Nothing
        INDsleUser.EditValue = Nothing
        INDsleUser.Properties.NullText = Nothing
        INDsleUserRol.EditValue = Nothing
        INDsleUserRol.Properties.NullText = Nothing
        INDgcUser.DataSource = Nothing
        INDlyRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim ListComponentType = New List(Of Tuple(Of Byte, String))()
        ListComponentType.Add(New Tuple(Of Byte, String)(EUserCampaingRole.QualityControl, "QF Calidad"))
        ListComponentType.Add(New Tuple(Of Byte, String)(EUserCampaingRole.ProductionQuality, "QF Producción"))
        ListComponentType.Add(New Tuple(Of Byte, String)(EUserCampaingRole.Assistant, "Auxiliar de Central de Mezclas"))
        ListComponentType.Add(New Tuple(Of Byte, String)(EUserCampaingRole.TechnicalDirector, "Director técnico"))
        INDsleUserRol.Properties.DataSource = ListComponentType
    End Sub

    ''' <summary>
    ''' Guardar usuarios autorizados
    ''' </summary>
    Public Sub SaveUser()
        If INDsleFechaProceso.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una fecha de procesamiento"
            INDsleFechaProceso.Focus()
            Exit Sub
        End If

        If ListAuthorizeUsersDetail Is Nothing OrElse ListAuthorizeUsersDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay productos agregados en la rejilla."
            Exit Sub
        End If

        If Not ListAuthorizeUsersDetail.Any(Function(item) item.UserRole = EUserCampaingRole.QualityControl) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar por lo menos un usuario con rol Supervisor"
            Exit Sub
        End If

        If Not ListAuthorizeUsersDetail.Any(Function(item) item.UserRole = EUserCampaingRole.ProductionQuality) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar por lo menos un usuario con rol QF Producción"
            Exit Sub
        End If

        If {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging}.Contains(_MSclass) Then
            If Not ListAuthorizeUsersDetail.Any(Function(item) item.UserRole = EUserCampaingRole.Assistant) Then
                Mensaje(EeventViewerImages.Advertencia) = "El rol de Auxiliar de producción no tiene asignado usuario"
                Exit Sub
            End If
        End If

        If PreparationTime = TimeSpan.Zero Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una hora de preparación por lote"
            INDTePreparationTime.Focus()
            Exit Sub
        End If

        RaiseEvent OnSaveAuthorizeUsers(Me, New AuthorizedUserEventArgs With {
                                        .AuthorizeUsers = ListAuthorizeUsersDetail,
                                        .ProcessDate = INDsleFechaProceso.EditValue,
                                        .PreparationTime = PreparationTime,
                                        .CampaignDetailId = campaignDetailId,
                                        .CampaignStatus = CampaignStatus
                                        })
    End Sub

    ''' <summary>
    ''' Agrega los detalles a la rejilla
    ''' </summary>
    Public Sub AddDetail()

        If Not ValidateControls() Then
            Exit Sub
        End If

        If ListAuthorizeUsersDetail Is Nothing Then
            ListAuthorizeUsersDetail = New List(Of CampaignDetailUsers)
        Else
            If ListAuthorizeUsersDetail.Any(Function(item) item.UserId = INDsleUser.EditValue And item.UserRole <> EUserCampaingRole.TechnicalDirector) Then
                Mensaje(EeventViewerImages.Advertencia) = "El usuario seleccionado ya existe en la rejilla"
                INDsleUser.Focus()
                Exit Sub
            End If
            If ListAuthorizeUsersDetail.Any(Function(item) item.UserRole = INDsleUserRol.EditValue) Then
                Mensaje(EeventViewerImages.Advertencia) = "El rol seleccionado ya existe en la rejilla"
                INDsleUser.Focus()
                Exit Sub
            End If

            If ListAuthorizeUsersDetail.Where(Function(item) item.UserId = INDsleUser.EditValue).Count() > 1 AndAlso
                    ListAuthorizeUsersDetail.Any(Function(item) item.UserRole = EUserCampaingRole.TechnicalDirector) Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya existe un usuario como director técnico con mas de un rol asignado"
                INDsleUser.Focus()
                Exit Sub
            End If

        End If

        Dim CampaignDetailUsers As New CampaignDetailUsers()
        Dim user = Presenter.GetUserById(INDsleUser.EditValue)
        With CampaignDetailUsers
            .UserId = INDsleUser.EditValue
            .UserCode = user.UserCode
            .FullNameUser = INDsleUser.Text
            .UserRole = INDsleUserRol.EditValue
            .Rol = INDsleUserRol.Text
        End With

        If INDTePreparationTime.EditValue IsNot Nothing Then
            Dim ts As TimeSpan
            If TypeOf INDTePreparationTime.EditValue Is TimeSpan Then
                ts = CType(INDTePreparationTime.EditValue, TimeSpan)
            ElseIf TypeOf INDTePreparationTime.EditValue Is DateTime Then
                ts = CType(INDTePreparationTime.EditValue, DateTime).TimeOfDay
            End If
            If ts <> TimeSpan.Zero Then
                PreparationTime = ts
            End If
        End If

        ListAuthorizeUsersDetail.Add(CampaignDetailUsers)
        INDgcUser.RefreshDataSource()
        Mensaje(EeventViewerImages.Informacion) = "Se agrego usuario correctamente."
        INDsleUser.EditValue = Nothing
        INDsleUser.Properties.NullText = Nothing
        INDsleUserRol.EditValue = Nothing
        INDsleUser.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteUser()
        Dim CampaignDetailUsers As CampaignDetailUsers = CType(INDgvUser.GetFocusedRow, CampaignDetailUsers)
        If CampaignDetailUsers.UserCode <> 4 Then
            If ListDeleteAuthorizeUsers Is Nothing Then
                If CampaignDetailUsers.UserRole = EUserCampaingRole.TechnicalDirector Then
                    Mensaje(EeventViewerImages.Advertencia) = "El usuario se encuentra parametrizado como director técnico, este rol no puede ser eliminado."
                    Exit Sub
                End If
                ListDeleteAuthorizeUsers = New List(Of CampaignDetailUsers)
            End If
            CampaignDetailUsers.MarkAsDeleted()
            ListDeleteAuthorizeUsers.Add(CampaignDetailUsers)
        End If
        ListAuthorizeUsersDetail.Remove(CampaignDetailUsers)
        INDgcUser.DataSource = ListAuthorizeUsersDetail
        INDgcUser.RefreshDataSource()
    End Sub

    Private Sub SaveTechnicalDirector()
        Dim TechnicalDirector = Presenter.GetTechnicalDirectorByCampaign(campaignDetailId)
        Dim CampaignDetailUsers As New CampaignDetailUsers()

        With CampaignDetailUsers
            .UserId = TechnicalDirector.UserId
            .UserCode = TechnicalDirector.UserCode
            .FullNameUser = TechnicalDirector.UserCodeName
            .UserRole = EUserCampaingRole.TechnicalDirector
            .Rol = "Director técnico"
        End With

        ListAuthorizeUsersDetail = New List(Of CampaignDetailUsers)
        ListAuthorizeUsersDetail.Add(CampaignDetailUsers)
        INDgcUser.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Carga informacion de los usuarios 
    ''' </summary>
    Private Sub LoadDataUser()
        If CampaignStatus <> 2 Then
            Dim result = Presenter.GetCampaignDetailUsersByIdCampaign(campaignDetailId)
            If result?.Any Then
                ListAuthorizeUsersDetail = New List(Of CampaignDetailUsers)
                For Each item In result
                    Dim user = Presenter.GetUserById(item.UserId)
                    Dim campaignDetailUser As New CampaignDetailUsers()
                    With campaignDetailUser
                        .Id = item.Id
                        .UserId = item.UserId
                        .UserCode = item.UserCode
                        .FullNameUser = user.CodeName
                        .UserRole = item.UserRole
                        .Rol = item.UserRoleName
                    End With
                    campaignDetailUser.MarkAsModified
                    ListAuthorizeUsersDetail.Add(campaignDetailUser)
                Next
                INDsleFechaProceso.EditValue = result(0).CampaignDetailId.ProcessingDate
                INDTePreparationTime.EditValue = result(0).CampaignDetailId.PreparationTime
                PreparationTime = result(0).CampaignDetailId.PreparationTime
                INDgcUser.DataSource = ListAuthorizeUsersDetail
            End If
        End If
        If INDsleFechaProceso.EditValue Is Nothing Then
            INDsleFechaProceso.EditValue = GetDateServer()
        End If
    End Sub

    ''' <summary>
    ''' Carga datos del usuario para editar
    ''' </summary>
    Private Sub EditUser()
        Dim user As CampaignDetailUsers = CType(INDgvUser.GetFocusedRow, CampaignDetailUsers)
        INDsleUser.EditValue = user.UserId
        INDsleUserRol.EditValue = user.UserRole
        ListAuthorizeUsersDetail.Remove(user)
        INDgcUser.DataSource = ListAuthorizeUsersDetail
        INDgcUser.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Obtiene el listado de usuarios autorizados para la campaña
    ''' </summary>
    Private Sub LoadDataSourceUsers()
        Result = Presenter.ListViewListAuthorizeUsers(campaignDetailId)
        INDsleUser.Properties.DataSource = Result
    End Sub


#End Region




#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAuthorizeUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ListActions = {eAcciones.Remove, eAcciones.Edit}.ToList()
        IndigoGridView1.SetListAcction(INDgvUser, ListActions)
        INDgvUser.Columns.FirstOrDefault(Function(m) m.Name.Equals("colActions")).Width = 100
        Presenter = New PDashboardProductionSchedule()
        InitializeTuples()
        LoadDataSourceUsers()
        SetMinDate()
        INDTePreparationTime.EditValue = TimeSpan.Zero
        LoadDataUser()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmContractExternalClientsDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If CampaignStatus = 2 Then
            INDsleFechaProceso.Focus()
            SaveTechnicalDirector()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDsbAgregar_Click(sender As Object, e As EventArgs) Handles INDsbAgregar.Click
        AddDetail()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim tag = sender.Tag.ToString()
        Select Case tag
            Case NameOf(eAcciones.Edit)
                EditUser()
            Case NameOf(eAcciones.Remove)
                DeleteUser()
        End Select
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar 
    ''' </summary>
    Private Sub FrmAuthorizeUsers_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            SaveUsers = False
            Me.Close()
        End If
    End Sub

#End Region



#Region "EditValueChanged"

    ''' <summary>
    ''' Validaciones fecha superior actual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFechaProceso_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFechaProceso.EditValueChanged
        If INDsleFechaProceso.EditValue IsNot Nothing And INDsleFechaProceso.EditValue < CampaignCreationDate Then
            Mensaje(EeventViewerImages.Advertencia) = "La fecha y hora no puede ser menor a la fecha de la campaña"
            INDsleFechaProceso.EditValue = Nothing
            INDsleFechaProceso.Focus()
        Else
            If INDsleFechaProceso.EditValue IsNot Nothing Then
                Dim selectedDate As DateTime = CType(INDsleFechaProceso.EditValue, DateTime)
                Dim currentDate As DateTime = GetDateServer()
                Dim selectedDateTruncated As DateTime = selectedDate.AddSeconds(-selectedDate.Second).AddMilliseconds(-selectedDate.Millisecond)
                Dim currentDateTruncated As DateTime = currentDate.AddSeconds(-currentDate.Second).AddMilliseconds(-currentDate.Millisecond)

                If selectedDateTruncated.Date = currentDateTruncated.Date AndAlso selectedDateTruncated < currentDateTruncated Then
                    Mensaje(EeventViewerImages.Advertencia) = "La hora de la fecha de procesamiento seleccionada debe ser posterior a la hora actual."
                    INDsleFechaProceso.EditValue = Nothing
                    INDsleFechaProceso.Focus()
                End If
            End If
        End If
    End Sub
#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
    End Sub

    ''' <summary>
    ''' Click Guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        SaveUser()
    End Sub

    ''' <summary>
    ''' Click Deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#Region "Don't close popup date"
    Private allowClose As Boolean = False
    Private Sub INDsleFechaProceso_Popup(sender As Object, e As EventArgs) Handles INDsleFechaProceso.Popup
        Dim form As PopupDateEditForm = CType(sender, DateEdit).GetPopupEditForm()
        AddHandler form.Calendar.OkClick, AddressOf Calendar_OkClick
    End Sub

    Private Sub Calendar_OkClick(sender As Object, e As EventArgs)
        allowClose = True
        INDsleFechaProceso.ClosePopup()
        allowClose = False
    End Sub

    Private Sub INDsleFechaProceso_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles INDsleFechaProceso.QueryCloseUp
        If allowClose Then Return
        e.Cancel = True
    End Sub


    ''' <summary>
    ''' Validación de seleccion de Rol, para la visualización del control de tiempo de preparacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUserRol_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUserRol.EditValueChanged
        INDlciTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If INDsleUserRol.EditValue = EUserCampaingRole.ProductionQuality Then
            INDlciTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Establece la fecha minima a seleccionar
    ''' </summary>
    Private Sub SetMinDate()
        INDsleFechaProceso.Properties.MinValue = GetDateServer()
    End Sub

#End Region

#End Region

#End Region

End Class

Public Class AuthorizedUserEventArgs
    Inherits EventArgs

    Public AuthorizeUsers As List(Of CampaignDetailUsers)
    Public ProcessDate As Date
    Public CampaignDetailId As Integer
    Public PreparationTime As TimeSpan
    Public CampaignStatus As Byte

End Class