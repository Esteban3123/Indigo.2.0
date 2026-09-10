#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class CtrCostActivityStepPayroll

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

    Public Property CostActivityStepPayroll As CostActivityStepPayroll

    Public Property ListCostActivityStepPayroll As List(Of CostActivityStepPayroll)

    Private CostActivityStepUUID As String

    Private Property CostActivityStepId As Integer

    Private Property CostActivityStepOrderDescription As String

    Private Property PayrollPositionId As Integer
        Get
            Return INDsleCostActivityStepPayroll.EditValue
        End Get
        Set(value As Integer)
            INDsleCostActivityStepPayroll.EditValue = value
        End Set
    End Property

    Private Property PositionCodeName As String
        Get
            Return If(String.IsNullOrEmpty(INDsleCostActivityStepPayroll.Text), INDsleCostActivityStepPayroll.Properties.NullText, INDsleCostActivityStepPayroll.Text)
        End Get
        Set(value As String)
            INDsleCostActivityStepPayroll.Properties.NullText = value
        End Set
    End Property

    Private Property Hours As Decimal
        Get
            Return INDseHours.EditValue
        End Get
        Set(value As Decimal)
            INDseHours.EditValue = value
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

    Private Property PayrollPositionXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostActivityStepPayroll.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostActivityStepPayroll.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostActivityStepPayroll(ByVal CostActivityStepPayroll As CostActivityStepPayroll)

    Private Sub CtrCostActivityStepPayroll_Load(sender As Object, e As EventArgs) Handles Me.Load
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

    Private Sub INDsleCostActivityStepPayroll_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostActivityStepPayroll.QueryPopUp
        If PayrollPositionXpo Is Nothing Then
            Using model As New MBusqueda
                PayrollPositionXpo = model.ConsultarEntidades(eDataSource.Position)
            End Using
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            If _CostActivityStepPayroll Is Nothing Then

            End If
            CostActivityStepPayroll.ParentUUID = CostActivityStepUUID
            CostActivityStepPayroll.CostActivityStepId = CostActivityStepId
            CostActivityStepPayroll.CostActivityStepOrderDescription = CostActivityStepOrderDescription
            CostActivityStepPayroll.PayrollPositionId = PayrollPositionId
            CostActivityStepPayroll.PositionCodeName = PositionCodeName
            CostActivityStepPayroll.Hours = Hours
            RaiseEvent AddCostActivityStepPayroll(_CostActivityStepPayroll)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        _CostActivityStepPayroll = New CostActivityStepPayroll()
        CostActivityStepUUID = Nothing
        CostActivityStepId = Nothing
        INDsleCostActivityStep.EditValue = Nothing
        INDsleCostActivityStep.Properties.NullText = String.Empty
        INDsleCostActivityStepPayroll.EditValue = Nothing
        INDsleCostActivityStepPayroll.Properties.NullText = String.Empty
        Hours = 0
        INDsleCostActivityStep.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True
        Me.CostActivityStepUUID = Me.CostActivityStepPayroll.ParentUUID
        Me.CostActivityStepId = Me.CostActivityStepPayroll.CostActivityStepId
        Me.CostActivityStepOrderDescription = Me.CostActivityStepPayroll.CostActivityStepOrderDescription
        INDsleCostActivityStep.Properties.NullText = Me.CostActivityStepOrderDescription
        Me.PayrollPositionId = Me.CostActivityStepPayroll.PayrollPositionId
        Me.PositionCodeName = Me.CostActivityStepPayroll.PositionCodeName
        Me.Hours = Me.CostActivityStepPayroll.Hours
        INDsleCostActivityStep.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If String.IsNullOrEmpty(Me.CostActivityStepUUID) AndAlso Me.CostActivityStepId = 0 Then
            errorList.AppendLine("Paso")
        End If

        If INDsleCostActivityStepPayroll.EditValue Is Nothing Then
            errorList.AppendLine("Activo Fijo")
        End If

        If INDseHours.EditValue <= 0 Then
            errorList.AppendLine("Horas")
        End If

        If errorList.Length > 0 Then
            errorList.Insert(0, "Hay Campos sin diligenciar: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        If Not Me.EditMode OrElse CostActivityStepPayroll.ParentUUID <> CostActivityStepUUID OrElse CostActivityStepPayroll.CostActivityStepId <> CostActivityStepId OrElse CostActivityStepPayroll.PayrollPositionId <> PayrollPositionId Then
            If ListCostActivityStepPayroll.Any(Function(d) d.ParentUUID = Me.CostActivityStepUUID AndAlso d.CostActivityStepId = Me.CostActivityStepId AndAlso d.PayrollPositionId = PayrollPositionId) Then
                Mensaje(EeventViewerImages.Advertencia) = "El Cargo " + CostActivityStepPayroll.PositionCodeName + " ya se encuentra asociado al paso."
                Return False
            End If
        End If

        Return True
    End Function

#End Region

End Class