#Region "Imports"

Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region

Public Class CtrCostActivityStepAddictionalCost

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

    Public Property CostActivityStepAddictionalCost As CostActivityStepAddictionalCost

    Public Property ListCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost)

    Private CostActivityStepUUID As String

    Private Property CostActivityStepId As Integer

    Private Property CostActivityStepOrderDescription As String

    Private Property Description As String
        Get
            Return INDmeDescription.EditValue
        End Get
        Set(value As String)
            INDmeDescription.EditValue = value
        End Set
    End Property

    Private Property Value As Decimal
        Get
            Return INDseValue.EditValue
        End Get
        Set(value As Decimal)
            INDseValue.EditValue = value
        End Set
    End Property

#End Region

#Region "XPO"

    Public Property ListCostActivityStep As List(Of CostActivityStep)
        Get
            Return CType(INDsleCostActivityStep.Properties.DataSource, List(Of CostActivityStep))
        End Get
        Set(value As List(Of CostActivityStep))
            INDsleCostActivityStep.Properties.DataSource = Nothing
            INDsleCostActivityStep.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostActivityStepAddictionalCost(ByVal CostActivityStepAddictionalCost As CostActivityStepAddictionalCost)

    Private Sub CtrCostActivityStepAddictionalCost_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
    End Sub

    Private Sub INDsleCostActivityStep_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCostActivityStep.EditValueChanged
        If Not String.IsNullOrEmpty(INDsleCostActivityStep.EditValue) Then
            If INDgvCostActivityStep.GetFocusedRow IsNot Nothing
                Dim activityStep = DirectCast(INDgvCostActivityStep.GetFocusedRow, CostActivityStep)
                CostActivityStepUUID = activityStep.UUID
                CostActivityStepId = activityStep.Id
                CostActivityStepOrderDescription = activityStep.OrderDescription
            End If
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            CostActivityStepAddictionalCost.ParentUUID = CostActivityStepUUID
            CostActivityStepAddictionalCost.CostActivityStepId = CostActivityStepId
            CostActivityStepAddictionalCost.CostActivityStepOrderDescription = CostActivityStepOrderDescription
            CostActivityStepAddictionalCost.Description = Description
            CostActivityStepAddictionalCost.Value = Value
            RaiseEvent AddCostActivityStepAddictionalCost(_CostActivityStepAddictionalCost)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        _CostActivityStepAddictionalCost = New CostActivityStepAddictionalCost()
        CostActivityStepUUID = Nothing
        CostActivityStepId = Nothing
        INDsleCostActivityStep.EditValue = Nothing
        INDsleCostActivityStep.Properties.NullText = String.Empty
        INDmeDescription.EditValue = String.Empty
        Value = 0
        INDsleCostActivityStep.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True
        Me.CostActivityStepUUID = Me.CostActivityStepAddictionalCost.ParentUUID
        Me.CostActivityStepId = Me.CostActivityStepAddictionalCost.CostActivityStepId
        Me.CostActivityStepOrderDescription = Me.CostActivityStepAddictionalCost.CostActivityStepOrderDescription
        INDsleCostActivityStep.Properties.NullText = Me.CostActivityStepOrderDescription
        Me.Description = Me.CostActivityStepAddictionalCost.Description
        Me.Value = Me.CostActivityStepAddictionalCost.Value
        INDsleCostActivityStep.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If String.IsNullOrEmpty(Me.CostActivityStepUUID) AndAlso Me.CostActivityStepId = 0 Then
            errorList.AppendLine("Paso")
        End If

        If String.IsNullOrEmpty(Description) Then
            errorList.AppendLine("Descripción")
        End If

        If INDseValue.EditValue <= 0 Then
            errorList.AppendLine("Horas")
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