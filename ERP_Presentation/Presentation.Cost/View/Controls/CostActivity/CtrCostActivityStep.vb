#Region "Imports"

Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

#End Region

Public Class CtrCostActivityStep

#Region "Variables"

    Private WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#Region "Properties"

    Public Property EditMode As Boolean

    Public Property CostActivityStep As CostActivityStep

    Public Property ListCostActivityStep As List(Of CostActivityStep)

    Private Property Order As Integer
        Get
            Return INDseOrder.EditValue
        End Get
        Set(value As Integer)
            INDseOrder.EditValue = value
        End Set
    End Property

    Private Property Description As String
        Get
            Return INDmeDescription.EditValue
        End Get
        Set(value As String)
            INDmeDescription.EditValue = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostActivityStep(ByVal CostActivityStep As CostActivityStep, ByVal PreviousOrder As Integer)

    Private Sub CtrCostActivityStep_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            CostActivityStep.UUID = If(EditMode, CostActivityStep.UUID, Guid.NewGuid().ToString())
            Dim PreviousOrder = CostActivityStep.Order
            CostActivityStep.Order = Order
            CostActivityStep.Description = Description
            CostActivityStep.OrderDescription = String.Format("{0} - {1}", Order, Description)
            RaiseEvent AddCostActivityStep(CostActivityStep, PreviousOrder)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        Dim TotalSteps = If(ListCostActivityStep Is Nothing, 0, ListCostActivityStep.Count)
        CostActivityStep = New CostActivityStep() With { .Order = TotalSteps + 1 }
        INDseOrder.Properties.MaxValue = CostActivityStep.Order
        INDseOrder.EditValue = CostActivityStep.Order
        INDmeDescription.EditValue = String.Empty
        INDmeDescription.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True        
        Me.Order = Me.CostActivityStep.Order
        Me.Description = Me.CostActivityStep.Description
        INDseOrder.Properties.MaxValue = ListCostActivityStep.Count
        INDmeDescription.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If String.IsNullOrEmpty(INDmeDescription.EditValue) Then
            errorList.AppendLine("Descripción")
        End If

        If errorList.Length > 0 Then
            errorList.Insert(0, "Hay Campos sin diligenciar: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        Return True
    End Function

#End Region

End Class
