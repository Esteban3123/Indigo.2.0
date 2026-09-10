Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base

Public Class FrmPopupWorkingArea

    Public Event OnWorkingAreaAdded(sender As Object, workingArea As WorkingArea, edit As Boolean)

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Property Code As String
        Get
            Return INDTeCode.EditValue
        End Get
        Set(value As String)
            INDTeCode.EditValue = value
        End Set
    End Property

    Private Property Description As String
        Get
            Return INDTeDescription.EditValue
        End Get
        Set(value As String)
            INDTeDescription.EditValue = value
        End Set
    End Property

    Private Property Observation As String
        Get
            Return INDMeObservation.EditValue
        End Get
        Set(value As String)
            INDMeObservation.EditValue = value
        End Set
    End Property

    Private Property State As Boolean
        Get
            Return INDGleStatus.EditValue
        End Get
        Set(value As Boolean)
            INDGleStatus.EditValue = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    Public Property AllWorkingAreas As List(Of WorkingArea)
    Public WorkingArea As WorkingArea
    Public CMConfigurationId As Integer
    Private _edit As Boolean

    Private Sub FrmPopupWorkingArea_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitForm()
    End Sub

    Private Sub FrmPopupWorkingArea_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        Try
            ValidateFields()
            AssigningValues()

            RaiseEvent OnWorkingAreaAdded(Me, WorkingArea, _edit)
        Catch ex As IndigoValidationException
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    Private Sub ValidateFields()
        Dim sb As New StringBuilder()

        If String.IsNullOrEmpty(Code) Then
            sb.AppendLine("El código es obligatorio")
        End If

        If String.IsNullOrEmpty(Description) Then
            sb.AppendLine("La descripción es obligatoria")
        End If

        If String.IsNullOrEmpty(Observation) Then
            sb.AppendLine("La observación es obligatoria")
        End If

        If sb.Length > 0 Then
            Throw New IndigoValidationException(sb.ToString())
        End If
        If WorkingArea Is Nothing Then
            If AllWorkingAreas.Any(Function(m) m.Code = Code) Then
                Throw New IndigoValidationException("El área de trabajo ya se encuentra en el listado")
            End If
        ElseIf AllWorkingAreas.Any(Function(m) m.Code = Code AndAlso Not m.Equals(WorkingArea)) Then
            Throw New IndigoValidationException("El área de trabajo ya se encuentra en el listado")
        End If
    End Sub

    Private Sub InitForm()
        LoadControls(WorkingArea)
    End Sub

    Private Sub LoadControls(workingArea As WorkingArea)
        _edit = False
        If workingArea IsNot Nothing Then
            _edit = True
            Code = workingArea.Code
            Description = workingArea.Description
            Observation = workingArea.Observation
            State = workingArea.Status
        End If
    End Sub

    Private Sub AssigningValues()
        If WorkingArea Is Nothing Then
            WorkingArea = New WorkingArea() With {.CMConfigurationId = CMConfigurationId, .Status = True}
        End If
        With WorkingArea
            .Code = Code
            .Description = Description
            .Observation = Observation
            .Status = State
        End With
    End Sub
End Class