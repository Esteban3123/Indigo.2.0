Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base
Imports Presentation.Contract
Imports Presentation.Contract.MVP

Public Class FrmGrouperCareGroup

    Public Sub New()
        InitializeComponent()
        IndigoGridControl1.SetHideNoRecords(INDGcGroupers, True)
    End Sub

#Region "Properties"
    Public Event AddAddGroupersCareGroupEventArgs(sender As Object, e As AddGroupersCareGroup)

    Public Property Groupers As List(Of GroupersCareGroup)
        Get
            Return CType(INDGcGroupers.DataSource, List(Of GroupersCareGroup))
        End Get
        Set(value As List(Of GroupersCareGroup))
            Dim nList As New List(Of GroupersCareGroup)()
            If value IsNot Nothing AndAlso value.Count > 0 Then
                For Each i In value
                    nList.Add(i.Clone())
                Next
            End If
            INDGcGroupers.DataSource = nList
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    Public Property IdCareGroup As Integer
#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        IdCareGroup = Nothing
    End Sub

    Private Sub FrmGrouperCareGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.Minimizar(True)
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        Me.ToolBar.Hide()
        INDSleGrouper.Focus()
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If INDSleGrouper Is Nothing Then
            Exit Sub
        End If
        Dim obj As GroupersXpo = CType(INDsgvGroupers.GetFocusedRow(), GroupersXpo)

        If Groupers IsNot Nothing AndAlso Groupers.Any(Function(o) o.GroupersId = obj.Id) Then
            Mensaje(EeventViewerImages.Advertencia) = "El Agrupador seleccionado ya se encuentra en el listado."
            Exit Sub
        End If

        Dim GroupersCareGroup As New GroupersCareGroup()
        With GroupersCareGroup
            .GroupersId = obj.Id
            .GrouperName = obj.Description
            .CareGroupId = IdCareGroup
            .DocumentDate = DateTime.Now
        End With
        If Groupers Is Nothing Then
            Groupers = New List(Of GroupersCareGroup)()
        End If
        Groupers.Add(GroupersCareGroup)

        INDGcGroupers.RefreshDataSource()
        INDSleGrouper.EditValue = Nothing
        INDSleGrouper.Focus()
    End Sub

    Private Sub INDSleGrouper_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleGrouper.EditValueChanged

    End Sub

    Private Sub INDSleGrouper_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleGrouper.QueryPopUp
        If INDSleGrouper.Properties.DataSource Is Nothing Then
            Using model As New MGroupers(Me.Tag.ToString())
                Dim _structure As XPCollection(Of GroupersXpo) = model.ListAllGroupersCollection()
                INDSleGrouper.Properties.DataSource = _structure
            End Using
        End If
    End Sub



    Private Sub INDSbAddGroupers_Click(sender As Object, e As EventArgs) Handles INDSbAddGroupers.Click
        If Groupers Is Nothing OrElse Groupers.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione algún Agrupador"
            Exit Sub
        End If

        Dim args As New AddGroupersCareGroup
        'args.GrouperCareGroup = GroupersCareGroup
        args.GrouperCareGroupList = Groupers
        'args.ListDeleteGroupersCareGroupActivities = ListDeleteGroupersCareGroupActivities
        'args.ListDeleteGroupersCareGroupCups = ListDeleteGroupersCareGroupCups
        RaiseEvent AddAddGroupersCareGroupEventArgs(Nothing, args)
        Me.Close()
    End Sub
#End Region

End Class