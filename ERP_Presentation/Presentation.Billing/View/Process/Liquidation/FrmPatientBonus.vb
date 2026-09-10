Imports Presentation.Contract.MVP

Public Class FrmPatientBonus

#Region "Properties"
    Public Property BonusValue As Decimal
        Get
            Return CType(INDSpnBonusValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDSpnBonusValue.EditValue = value
        End Set
    End Property
#End Region

#Region "Methods"

#End Region

#Region "Handlers"

    Public WriteOnly Property TotalEntity As Decimal
        Set(value As Decimal)
            If value > 0 Then
                INDSpnBonusValue.Properties.MaxValue = value
            End If
        End Set
    End Property

    Public ReadOnly Property ShareType As Integer
        Get
            Return INDSleShareType.EditValue
        End Get
    End Property

    Private _careGroupId As Integer
    Public WriteOnly Property CareGroupId
        Set(value)
            _careGroupId = value
        End Set
    End Property


    Private Sub FrmPatientBonus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDSpnBonusValue.Focus()
        Dim listShareType As New List(Of Tuple(Of Integer, String))
        listShareType.Add(New Tuple(Of Integer, String)(2, "Cuota Moderadora"))
        listShareType.Add(New Tuple(Of Integer, String)(3, "Copago"))
        Using model As New MCareGroup(Me.Tag)
            Dim careGroup = model.GetCareGroupByIdSimple(_careGroupId).ObjectEmbbeded
            If careGroup.EntityType > 4 Then
                listShareType.Add(New Tuple(Of Integer, String)(4, "Bono"))
            End If
        End Using

        INDSleShareType.Properties.DataSource = listShareType
    End Sub

    Private Sub INDSpAdd_Click(sender As Object, e As EventArgs) Handles INDSpAdd.Click
        If BonusValue <> 0 AndAlso INDSleShareType.EditValue IsNot Nothing Then
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub
#End Region

End Class