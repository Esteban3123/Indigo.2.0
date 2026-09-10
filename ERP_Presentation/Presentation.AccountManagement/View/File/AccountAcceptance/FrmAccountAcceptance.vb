Imports Infrastructure.CrossCutting.Base
Imports Presentation.AccountManagement.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid
Imports DevExpress.Data.Linq
Imports Domain.AccountManagement.Model
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class FrmAccountAcceptance
    Implements IAccountAcceptance

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Private _presenter As PAccountAcceptance

    Public Sub New()
        InitializeComponent()

    End Sub

#Region "Variables"

    ''' <summary>
    ''' Código del usuario logeado
    ''' </summary>
    Dim userCode = SessionValues.Instance.UserIndigo

    ''' <summary>
    ''' Flag que determina si se está mostrando un popup
    ''' </summary>
    Private _isPopupMenuShowing As Boolean

    ''' <summary>
    ''' Variable que guarda el traslado seleccionado en el grid
    ''' </summary>
    Private _selectedFolioTransfer As VDashboardProperties


#End Region

#Region "Properties"

    ''' <summary>
    ''' Tag identificador del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IAccountAcceptance.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propieda para los mensajes de eventos.
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

    ''' <summary>
    ''' Propiedad que obtiene el ID del centro de atención seleccionado.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AttentionCenterCode As String
        Get
            Return INDSleAttentionCenterFilter.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el area de gestión seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property SelectedManagementArea As String
        Get
            Return INDSleManagementAreaFilter.EditValue
        End Get
        Set(value As String)
            INDSleManagementAreaFilter.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasource"

    ''' <summary>
    ''' Datasource de las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    Public Property AttentionCenterXpo As XPInstantFeedbackSource Implements IAccountAcceptance.AttentionCenterXpo
        Get
            Return INDSleAttentionCenterFilter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAttentionCenterFilter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los usuarios a filtrar
    ''' </summary>
    ''' <returns></returns>
    Public Property UsersXpo As LinqInstantFeedbackSource Implements IAccountAcceptance.UsersXpo
        Get
            Return INDSleManagementAreaFilter.Properties.DataSource
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDSleManagementAreaFilter.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Datasource de las areas de gestión de cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Property ManagementAreasDatasource As List(Of ManagementAreas) Implements IAccountAcceptance.ManagementAreasDatasource
        Get
            Return INDSleManagementAreaFilter.Properties.DataSource
        End Get
        Set(value As List(Of ManagementAreas))
            INDSleManagementAreaFilter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la lista de razones de rechazo en el popup de rechazo
    ''' </summary>
    ''' <returns></returns>
    Public Property RejectionReasonDatasource As List(Of RejectionReason) Implements IAccountAcceptance.RejectionReasonDatasource

    ''' <summary>
    ''' Datasource del control de traslados
    ''' </summary>
    ''' <returns></returns>
    Public Property TransfersXpo As List(Of VDashboardProperties)
        Get
            Return INDGcAcceptance.DataSource
        End Get
        Set(value As List(Of VDashboardProperties))
            INDGcAcceptance.DataSource = value
        End Set
    End Property



#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Evento de carga del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub FrmDashboardAccountManagement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()
        AddActionsColumns()
        _presenter = New PAccountAcceptance(Me)
        _presenter.FillAttentionCenter()
        _presenter.FillRejectionReasons()

        Using model As New MAccountAcceptance(MyTag)
            Dim managementAreas = Await model.GetManagementAreasPerUserCode(userCode)

            If managementAreas IsNot Nothing AndAlso managementAreas.Count = 1 Then
                ManagementAreasDatasource = Nothing
                ManagementAreasDatasource = managementAreas
                SelectedManagementArea = managementAreas(0).Code
                Await LoadDatasourceAsync()
            End If

            If managementAreas IsNot Nothing AndAlso managementAreas.Count > 1 Then
                ManagementAreasDatasource = Nothing
                ManagementAreasDatasource = managementAreas
            End If
        End Using
    End Sub
#End Region

#Region "Shown"
    ''' <summary>
    ''' Evento de visualización del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDashboardAccountManagement_Shown(sender As Object, e As EventArgs) Handles Me.Shown

    End Sub
#End Region

    ''' <summary>
    ''' Evento click del botón Cargar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbLoad_Click(sender As Object, e As EventArgs) Handles INDSbLoad.Click
        If String.IsNullOrEmpty(SelectedManagementArea) OrElse String.IsNullOrEmpty(AttentionCenterCode) Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione un centro de atención y un area de gestión."
            Exit Sub
        End If

        Await LoadDatasourceAsync()
    End Sub
#End Region

#Region "ContextMenu"
    ''' <summary>
    ''' Evento click en los botones del contextmenu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridView_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView.Click_ButtonAction, IndigoGridView.ContexMenuActions
        Dim selectedFolioTransfer = TryCast(INDGvAcceptance.GetFocusedRow, VDashboardProperties)
        _selectedFolioTransfer = selectedFolioTransfer

        Select Case sender.Tag
            Case "ConfirmItem"
                Dim folioTransfer = New FolioTransfer With {
                        .Id = selectedFolioTransfer.FolioTransferId
                }
                Await ProcessRequest(folioTransfer, 3, "aceptar", indigo.AuditMessageWcf)
            Case "Reject"
                ' Crear y mostrar el formulario de rechazo
                Dim frmRejection As New FrmRejectionPopup()
                frmRejection.RejectionReasonDatasource = RejectionReasonDatasource
                frmRejection.StartPosition = FormStartPosition.Manual

                ' Posicionar el formulario
                Dim mousePosition As Point = Me.MousePosition
                mousePosition.Offset(-300, 10)
                frmRejection.Location = mousePosition

                ' Mostrar el formulario y procesar si se aceptó
                frmRejection.ShowDialog()

                If frmRejection.Accepted Then
                    Dim folioTransfer = New FolioTransfer With {
                        .Id = selectedFolioTransfer.FolioTransferId,
                        .RejectionObservation = frmRejection.RejectionObservation,
                        .RejectionReasonId = frmRejection.SelectedRejectionReason
                    }
                    Await ProcessRequest(folioTransfer, 4, "rechazar", indigo.AuditMessageWcf)
                End If
        End Select

    End Sub
#End Region

#Region "CustomDrawCell"
    ''' <summary>
    ''' Evento que escucha el renderizado de una celda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvAcceptance_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvAcceptance.CustomDrawCell
        ' Early return: solo procesar columna TimeStatus
        If e.Column.FieldName <> "TimeStatus" Then Exit Sub

        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim cellValue = view.GetRowCellValue(e.RowHandle, e.Column)

        ' Validar que el valor esté cargado y no sea nulo
        If cellValue Is Nothing OrElse TypeOf cellValue Is DevExpress.Data.NotLoadedObject Then Exit Sub

        ' Conversión segura del valor
        Dim value As Integer
        If Not Integer.TryParse(cellValue.ToString(), value) Then Exit Sub

        ' Aplicar colores según el valor del semáforo
        Select Case value
            Case 1 ' Verde - A tiempo
                e.Appearance.BackColor = Color.FromArgb(198, 239, 206)
                e.Appearance.BackColor2 = Color.FromArgb(155, 217, 155)
            Case 2 ' Amarillo - Advertencia
                e.Appearance.BackColor = Color.FromArgb(255, 235, 156)
                e.Appearance.BackColor2 = Color.FromArgb(255, 217, 102)
            Case 3 ' Rojo - Crítico
                e.Appearance.BackColor = Color.FromArgb(255, 199, 206)
                e.Appearance.BackColor2 = Color.FromArgb(255, 102, 102)
            Case Else
                Exit Sub ' No colorear si el valor no es 1, 2 o 3
        End Select

        ' Configurar gradiente y ocultar texto
        e.Appearance.GradientMode = LinearGradientMode.Vertical
        e.DisplayText = String.Empty
    End Sub
#End Region


#Region "Methods"

    ''' <summary>
    ''' Crea la columna de acciones
    ''' </summary>
    Private Sub AddActionsColumns()
        IndigoGridView.SetListAcction(INDGvAcceptance, {eAcciones.ConfirmItem, eAcciones.Reject}.ToList())

        For Each col As Columns.GridColumn In INDGvAcceptance.Columns
            If col.Name = "colAction" Then
                col.Width = 150
            End If
        Next
    End Sub

    ''' <summary>
    ''' Procesa la operación de aceptación o rechazo de la solicitud de traslado
    ''' </summary>
    ''' <param name="folioTransfer"></param>
    ''' <param name="newState"></param>
    ''' <param name="operation"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Private Async Function ProcessRequest(folioTransfer As FolioTransfer, newState As Byte, operation As String, audit As AuditMessage) As Task
        Try
            Using model As New MAccountAcceptance(MyTag)
                Dim res = Await model.ProcessTransferRequest(folioTransfer, newState, operation, audit)
                If res.StateResult Then
                    Await LoadDatasourceAsync()
                    Mensaje(EeventViewerImages.Informacion) = res.Message
                Else
                    Mensaje(EeventViewerImages.MensajeError) = res.Message
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Recarga el datasource
    ''' </summary>
    Private Async Function LoadDatasourceAsync() As Task
        Try
            Using model As New MAccountAcceptance(MyTag)

                Dim res = Await model.ListFolioTransferRequests(AttentionCenterCode, SelectedManagementArea, userCode)

                If res.StateResult Then
                    TransfersXpo = res.ObjectEmbbeded
                Else
                    TransfersXpo = Nothing
                    Mensaje(EeventViewerImages.MensajeError) = res.Message
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Function
#End Region





End Class