Imports Infrastructure.CrossCutting.Base
Imports Presentation.AccountManagement.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports Domain.AccountManagement.Model

Public Class FrmRejectionPopup

#Region "Properties"

    ''' <summary>
    ''' Propiedad que obtiene o establece la razón de rechazo seleccionada
    ''' </summary>
    Public Property SelectedRejectionReason As Integer
        Get
            Return INDSleRejectionReason.EditValue
        End Get
        Set(value As Integer)
            INDSleRejectionReason.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la observación del rechazo
    ''' </summary>
    Public Property RejectionObservation As String
        Get
            Return INDTeRejectionObservation.EditValue
        End Get
        Set(value As String)
            INDTeRejectionObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de las razones de rechazo
    ''' </summary>
    Public Property RejectionReasonDatasource As List(Of RejectionReason)
        Get
            Return INDSleRejectionReason.Properties.DataSource
        End Get
        Set(value As List(Of RejectionReason))
            INDSleRejectionReason.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si el usuario aceptó el rechazo
    ''' </summary>
    Public Property Accepted As Boolean = False

#End Region

#Region "Constructor"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento click del botón Aceptar
    ''' </summary>
    Private Sub INDSbAcceptRejection_Click(sender As Object, e As EventArgs) Handles INDSbAcceptRejection.Click
        If SelectedRejectionReason = 0 Then
            MessageIndigo.Show("Por favor seleccione una razón de rechazo.", MessageType.Warning, Me.Text)
            Exit Sub
        End If

        Accepted = True
        Me.Close()
    End Sub

    ''' <summary>
    ''' Evento de carga del formulario
    ''' </summary>
    Private Sub FrmRejectionPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Limpiar valores previos
        SelectedRejectionReason = 0
        RejectionObservation = String.Empty
        Accepted = False
    End Sub

#End Region

End Class

