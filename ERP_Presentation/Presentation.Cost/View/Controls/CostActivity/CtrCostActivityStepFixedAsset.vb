#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class CtrCostActivityStepFixedAsset

#Region "Variables"

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

#End Region

#Region "Properties"

    Public Property EditMode As Boolean

    Public Property CostActivityStepFixedAsset As CostActivityStepFixedAsset

    Public Property ListCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset)

    Private CostActivityStepUUID As String

    Private Property CostActivityStepId As Integer

    Private Property CostActivityStepOrderDescription As String

    Private Property FixedAssetItemId As Integer
        Get
            Return INDsleCostActivityStepFixedAsset.EditValue
        End Get
        Set(value As Integer)
            INDsleCostActivityStepFixedAsset.EditValue = value
        End Set
    End Property

    Private Property FixedAssetItemCodeName As String
        Get
            Return If(String.IsNullOrEmpty(INDsleCostActivityStepFixedAsset.Text), INDsleCostActivityStepFixedAsset.Properties.NullText, INDsleCostActivityStepFixedAsset.Text)
        End Get
        Set(value As String)
            INDsleCostActivityStepFixedAsset.Properties.NullText = value
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

    Private Property FixedAssetPhysicalAssetXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostActivityStepFixedAsset.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostActivityStepFixedAsset.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Events"

    Public Event AddCostActivityStepFixedAsset(ByVal CostActivityStepFixedAsset As CostActivityStepFixedAsset)

    Private Sub CtrCostActivityStepFixedAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
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

    Private Sub INDsleCostActivityStepFixedAsset_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCostActivityStepFixedAsset.QueryPopUp
        If FixedAssetPhysicalAssetXpo Is Nothing Then
            Using model As New MBusqueda
                FixedAssetPhysicalAssetXpo = model.ConsultarEntidades(eDataSource.ListFixedAssetEquipmentByStatus, "True")
            End Using
        End If
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then            
            CostActivityStepFixedAsset.ParentUUID = CostActivityStepUUID
            CostActivityStepFixedAsset.CostActivityStepId = CostActivityStepId
            CostActivityStepFixedAsset.CostActivityStepOrderDescription = CostActivityStepOrderDescription
            CostActivityStepFixedAsset.FixedAssetItemId = FixedAssetItemId
            CostActivityStepFixedAsset.FixedAssetItemCodeName = FixedAssetItemCodeName
            CostActivityStepFixedAsset.Hours = Hours
            RaiseEvent AddCostActivityStepFixedAsset(_CostActivityStepFixedAsset)
            CleanControls()
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub CleanControls()
        EditMode = False
        _CostActivityStepFixedAsset = New CostActivityStepFixedAsset()
        CostActivityStepUUID = Nothing
        CostActivityStepId = Nothing
        INDsleCostActivityStep.EditValue = Nothing
        INDsleCostActivityStep.Properties.NullText = String.Empty
        INDsleCostActivityStepFixedAsset.EditValue = Nothing
        INDsleCostActivityStepFixedAsset.Properties.NullText = String.Empty
        Hours = 0
        INDsleCostActivityStep.Focus()
    End Sub

    Public Sub LoadControls()
        Me.EditMode = True
        Me.CostActivityStepUUID = Me.CostActivityStepFixedAsset.ParentUUID
        Me.CostActivityStepId = Me.CostActivityStepFixedAsset.CostActivityStepId
        Me.CostActivityStepOrderDescription = Me.CostActivityStepFixedAsset.CostActivityStepOrderDescription
        INDsleCostActivityStep.Properties.NullText = Me.CostActivityStepOrderDescription
        Me.FixedAssetItemId = Me.CostActivityStepFixedAsset.FixedAssetItemId
        Me.FixedAssetItemCodeName = Me.CostActivityStepFixedAsset.FixedAssetItemCodeName
        Me.Hours = Me.CostActivityStepFixedAsset.Hours
        INDsleCostActivityStep.Focus()
    End Sub

    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()

        If String.IsNullOrEmpty(Me.CostActivityStepUUID) AndAlso Me.CostActivityStepId = 0 Then
            errorList.AppendLine("Paso")
        End If

        If INDsleCostActivityStepFixedAsset.EditValue Is Nothing Then
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

        If CostActivityStepFixedAsset.FixedAssetItemId <> FixedAssetItemId AndAlso ListCostActivityStepFixedAsset.Any(Function(d) d.FixedAssetItemId = FixedAssetItemId) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Artículo " + CostActivityStepFixedAsset.FixedAssetItemCodeName + " ya existe en la lista."
            Return False
        End If

        Return True
    End Function

#End Region

End Class