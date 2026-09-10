'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 6-11-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns

#End Region

Public Class PopupHomologation

    Public Event SetHomologation(sender As Object, e As SetHomologationEventArgs)

    Dim _listHomologation As List(Of CupsHomologation)
    Public Property ListHomologation As List(Of CupsHomologation)
        Get
            Return _listHomologation
        End Get
        Set(value As List(Of CupsHomologation))
            _listHomologation = value
        End Set
    End Property

    Property OnlySelectedOne As Boolean

    Private Sub PopupHomologation_Load(sender As Object, e As EventArgs) Handles Me.Load
        INDGcHomologation.DataSource = ListHomologation
    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        If OnlySelectedOne AndAlso ListHomologation.FindAll(Function(x) x.Activated = True).Count > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar solo una homologación"
            Exit Sub
        End If

        Dim activate = ListHomologation.Find(Function(x) x.Activated = True)
        If activate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar minimo una homologación"
            Exit Sub
        End If
        Dim args As New SetHomologationEventArgs
        args.ListHomologations = ListHomologation.FindAll(Function(x) x.Activated = True)
        RaiseEvent SetHomologation(Nothing, args)
        Me.Close()
    End Sub

    Private Sub INDGcHomologation_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcHomologation.MouseDoubleClick
        If OnlySelectedOne = False Then
            Dim hitPoint = Me.INDGvHomologation.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
                    If DirectCast(Me.INDGvHomologation.DataSource, List(Of CupsHomologation)).Where(Function(s) s.Activated).ToList().Count = _listHomologation.Count Then
                        _listHomologation.ForEach(Sub(x) x.Activated = False)
                    Else
                        _listHomologation.ForEach(Sub(x) x.Activated = True)
                    End If
                    Me.INDGcHomologation.RefreshDataSource()
                    Me.INDGcHomologation.Invalidate()
                End If
            End If
        End If
    End Sub

    Private Sub INDGvHomologation_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGvHomologation.MouseDown
        If OnlySelectedOne AndAlso ListHomologation.Find(Function(x) x.Activated = True) IsNot Nothing Then
            Dim view As GridView = CType(sender, GridView)
            Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Location)
            If hitInfo.InRowCell Then
                If hitInfo.Column.Name = INDGclState.Name Then
                    If ListHomologation.Find(Function(x) x.Activated = True) IsNot Nothing Then
                        CType(INDGvHomologation.GetFocusedRow(), CupsHomologation).Activated = False
                        'CType(INDGvHomologation.GetRow(hitInfo.RowHandle), CupsHomologation).Activated = False
                        Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar solo una homologación"
                    End If
                End If
            End If
        End If
    End Sub

End Class