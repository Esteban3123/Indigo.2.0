Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.XtraBars
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq
Imports DevExpress.XtraEditors.Controls

Public Class FrmAlertsPopup

#Region "Fields"

    ''' <summary>
    ''' Referencia al popup de agregar alerta del formulario padre
    ''' </summary>
    Private _addAlertPopup As PopupControlContainer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Datasource de las alertas actuales
    ''' </summary>
    Public Property CurrentAlertsDatasource As List(Of FolioAlert)
        Get
            Return CType(INDGcCurrentAlerts.DataSource, List(Of FolioAlert))
        End Get
        Set(value As List(Of FolioAlert))
            INDGcCurrentAlerts.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de las alertas pasadas
    ''' </summary>
    Public Property PreviousAlertsDatasource As List(Of FolioAlert)
        Get
            Return CType(INDGcPreviousAlerts.DataSource, List(Of FolioAlert))
        End Get
        Set(value As List(Of FolioAlert))
            INDGcPreviousAlerts.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Referencia al popup para agregar alertas
    ''' </summary>
    Public Property AddAlertPopup As PopupControlContainer
        Get
            Return _addAlertPopup
        End Get
        Set(value As PopupControlContainer)
            _addAlertPopup = value
        End Set
    End Property


#End Region

#Region "Events"

    ''' <summary>
    ''' Evento que se dispara cuando se solicita suspender una alerta
    ''' </summary>
    Public Event SuspendAlertRequested As EventHandler(Of FolioAlertEventArgs)

#End Region

#Region "Constructor"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento click del botón + para agregar una alerta
    ''' </summary>
    Private Sub INDSbAddAlert_Click(sender As Object, e As EventArgs) Handles INDSbAddAlert.Click
        If _addAlertPopup IsNot Nothing Then
            Dim mousePosition As Point = Me.MousePosition
            mousePosition.Offset(10, 10)
            _addAlertPopup.ShowPopup(mousePosition)
        End If
    End Sub


    ''' <summary>
    ''' Maneja el click del botón de suspender alerta
    ''' </summary>
    Private Sub SuspendButtonEdit_Click(sender As Object, e As ButtonPressedEventArgs) Handles SuspendButtonEdit.ButtonPressed
        Dim folioAlert = TryCast(INDGvCurrentAlerts.GetFocusedRow(), FolioAlert)
        If folioAlert IsNot Nothing Then
            RaiseEvent SuspendAlertRequested(Me, New FolioAlertEventArgs(folioAlert))
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Método público para refrescar los datasources de los grids
    ''' </summary>
    Public Sub RefreshGrids()
        INDGcCurrentAlerts.RefreshDataSource()
        INDGcPreviousAlerts.RefreshDataSource()
    End Sub

#End Region

End Class

''' <summary>
''' Clase para los argumentos del evento de suspensión de alerta
''' </summary>
Public Class FolioAlertEventArgs
    Inherits EventArgs

    Public Property FolioAlert As FolioAlert

    Public Sub New(folioAlert As FolioAlert)
        Me.FolioAlert = folioAlert
    End Sub
End Class

