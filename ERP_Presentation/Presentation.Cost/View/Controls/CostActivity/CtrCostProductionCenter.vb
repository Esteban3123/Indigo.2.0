#Region "Imports"

Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Cost.MVP

#End Region

Public Class CtrCostProductionCenter

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

    Public Property CostActivityProductionCenter As CostActivityProductionCenter

    Public Property ListCostActivityProductionCenter As List(Of CostActivityProductionCenter)

    Private Property CostProductionCenterId As Integer
        Get
            Return INDsleCostProductionCenter.EditValue
        End Get
        Set(value As Integer)
            INDsleCostProductionCenter.EditValue = value
        End Set
    End Property

    Private Property CostProductionCenterCodeName As String
        Get
            Return INDsleCostProductionCenter.Properties.NullText
        End Get
        Set(value As String)
            INDsleCostProductionCenter.Properties.NullText = value
        End Set
    End Property

#End Region

#Region "XPO"

    Private Property CostProductionCenterXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostProductionCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostProductionCenter.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostActivityProductionCenter(ByVal costActivityProductionCenter As CostActivityProductionCenter)

    Private Sub CtrCostProductionCenter_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
    End Sub

    Private Sub INDsleCostProductionCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostProductionCenter.QueryPopUp
        If CostProductionCenterXpo Is Nothing Then
            Using model As New MCostProductionCenter("")
                CostProductionCenterXpo = model.ListCostProductionCenterByStatusAndCenterType(True, 1)
            End Using
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            CostActivityProductionCenter.CostProductionCenterId = CostProductionCenterId
            CostActivityProductionCenter.CostProductionCenterCodeName = If(INDsleCostProductionCenter.Text Is Nothing, CostProductionCenterCodeName, INDsleCostProductionCenter.Text)
            RaiseEvent AddCostActivityProductionCenter(CostActivityProductionCenter)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        CostActivityProductionCenter = New CostActivityProductionCenter()
        INDsleCostProductionCenter.EditValue = Nothing
        INDsleCostProductionCenter.Properties.NullText = String.Empty
        INDsleCostProductionCenter.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True        
        Me.CostProductionCenterId = Me.CostActivityProductionCenter.CostProductionCenterId
        Me.CostProductionCenterCodeName = Me.CostActivityProductionCenter.CostProductionCenterCodeName
        INDsleCostProductionCenter.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If INDsleCostProductionCenter.EditValue Is Nothing Then
            errorList.AppendLine("Centro de Producción")
        End If 

        If errorList.Length > 0 Then
            errorList.Insert(0, "Hay Campos sin diligenciar: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        If CostActivityProductionCenter.CostProductionCenterId <> CostProductionCenterId AndAlso ListCostActivityProductionCenter.Any(Function(d) d.CostProductionCenterId = CostProductionCenterId) Then
            Mensaje(EeventViewerImages.Advertencia) ="El Centro de Producción " + CostActivityProductionCenter.CostProductionCenterCodeName + " ya existe en la lista."
            Return False
        End If

        Return True
    End Function

#End Region

End Class
