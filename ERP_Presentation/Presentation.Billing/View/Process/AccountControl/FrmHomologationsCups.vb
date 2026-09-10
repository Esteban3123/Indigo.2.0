'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 04-07-2015
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
Imports Presentation.Controls
Imports System.Collections.Concurrent

#End Region

Public Class FrmHomologationsCups

    Public Event SetHomologation(sender As Object, e As HomologationCupsEventArgs)

    Dim _listHomologation As List(Of List(Of CupsHomologation))

    Public Property CurrentHomologation As List(Of CupsHomologation)

    Public Property CanClose As Boolean = True

    ''' <summary>
    ''' Bandera para validar que solo se seleccione un homologo
    ''' </summary>
    Property selectedItem As Boolean

    Public Property ListHomologation As List(Of List(Of CupsHomologation))
        Get
            Return _listHomologation
        End Get
        Set(value As List(Of List(Of CupsHomologation)))
            _listHomologation = value
        End Set
    End Property

    Property IsQx As Boolean

    ''' <summary>
    ''' Propiedad que es utilizada en control cuentas ambulatorio
    ''' </summary>
    ''' <returns></returns>
    Property NewAdmission As Object

    ''' <summary>
    ''' Propiedad que es utilizada en control cuentas ambulatorio
    ''' </summary>
    ''' <returns></returns>
    Property CareCenterCode As String

    ''' <summary>
    ''' Propiedad que es utilizada en control cuentas ambulatorio
    ''' </summary>
    ''' <returns></returns>
    Property listDetail As ConcurrentBag(Of Object)

    ''' <summary>
    ''' Propiedad que es utilizada en control cuentas ambulatorio
    ''' </summary>
    ''' <returns></returns>
    Property CareGroupId As Integer

    ''' <summary>
    ''' Propiedad que es utilizada en control cuentas ambulatorio
    ''' </summary>
    ''' <returns></returns>
    Property AuthorizationNumber As String

	Property arguments As Object

	''' <summary>
	''' Propiedad que es utilizada para llevar el control de las homologaciones
	''' </summary>
	''' <returns></returns>
	Private Property CurrentIndex As Integer = 0

	Private sourceList As List(Of List(Of CupsHomologation))

	Private Sub PopupHomologation_Load(sender As Object, e As EventArgs) Handles Me.Load
		If ListHomologation.Any(Function(o) o.Count > 1) Then
			sourceList = ListHomologation.Where(Function(o) o.Count > 1).ToList()

			Me.ToolBar.Visible = True
			Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
			Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
			Me.BarraBotones.FilterDataSource = sourceList
		Else
			sourceList = ListHomologation
			Me.ToolBar.Visible = False
			Me.BarraBotones.FilterDataSource = sourceList
		End If
		CurrentIndex = 0
		CurrentHomologation = sourceList(CurrentIndex)
		INDGcHomologation.DataSource = CurrentHomologation
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
        'Dim activate = ListHomologation.Find(Function(x) x.Activated = True)
        'If activate Is Nothing Then
        '    Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar minimo una homologación"
        '    Exit Sub
        'End If
        'Validar que se halla seleccionado al menos uno por pestaña
        Dim homologacionSinValidar As Boolean = False
        If ListHomologation.Count > 1 Then
            For Each homolog In ListHomologation
                If homolog.Count > 1 AndAlso homolog.FindAll(Function(o) o.Activated = True).Count = 0 Then
                    homologacionSinValidar = True
                    BarraBotones.FilterNavigationPosition(ListHomologation.IndexOf(homolog))
                    Exit For
                End If
            Next
        Else
            If ListHomologation(0).FindAll(Function(o) o.Activated = True).Count = 0 Then
                homologacionSinValidar = True
            End If
        End If

        If homologacionSinValidar Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar minimo una homologación"
            Exit Sub
        End If

        Dim args As New HomologationCupsEventArgs
        args.IsProcedureQx = IsQx
        args.listDetail = listDetail
        args.NewAdmission = NewAdmission
        args.CareCenterCode = CareCenterCode
        args.CareGroupId = CareGroupId
        args.AuthorizationNumber = AuthorizationNumber
        Dim listHomologationReturn As New List(Of List(Of CupsHomologation))
        ListHomologation.ForEach(Sub(o)
                                     If o.Count = 1 Then
                                         listHomologationReturn.Add(o)
                                     Else
                                         listHomologationReturn.Add(o.Where(Function(x) x.Activated = True).ToList())
                                     End If
                                 End Sub)
        args.ListHomologations = listHomologationReturn
        args.arguments = arguments
        RaiseEvent SetHomologation(Me, args)
        If CanClose Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ record navigation change event.
    ''' </summary>
    ''' <param name="Record">The record.</param>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
		Dim group = TryCast(Record, List(Of CupsHomologation))
		If group IsNot Nothing Then
			INDGcHomologation.DataSource = group
			CurrentHomologation = group

			Dim dataSourceList = BarraBotones.FilterDataSource.ToList()
			CurrentIndex = dataSourceList.FindIndex(Function(x) x Is group)
		End If
	End Sub

    Private Sub INDGcHomologation_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDGcHomologation.MouseDoubleClick
        'Dim hitPoint = Me.INDGvHomologation.CalcHitInfo(e.Location)
        'If hitPoint.Column IsNot Nothing Then
        '    If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then
        '        If DirectCast(Me.INDGvHomologation.DataSource, List(Of CupsHomologation)).Where(Function(s) s.Activated).ToList().Count = _listHomologation.Count Then
        '            _listHomologation.ForEach(Sub(x) x.Activated = False)
        '        Else
        '            _listHomologation.ForEach(Sub(x) x.Activated = True)
        '        End If
        '        Me.INDGcHomologation.RefreshDataSource()
        '        Me.INDGcHomologation.Invalidate()
        '    End If
        'End If
    End Sub

    Private Sub FrmHomologationsCups_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the CheckedChanged event of the INDrptChkSelect control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDrptChkSelect_CheckedChanged(sender As Object, e As EventArgs) Handles INDrptChkSelect.CheckedChanged
        Dim _control = CType(sender, DevExpress.XtraEditors.CheckEdit)
        If _control.Checked Then
            If CurrentHomologation.Find(Function(o) o.Activated = True) IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar solo una homologación"
                _control.Checked = False
            End If
        End If
    End Sub

	Private Sub INDGvHomologation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGvHomologation.KeyDown
		If Me.BarraBotones.FilterDataSource Is Nothing Then Exit Sub

		If e.KeyCode = System.Windows.Forms.Keys.Right Then
			If INDGvHomologation.IsEditing Then
				INDGvHomologation.PostEditor()
			End If
			If CurrentIndex < BarraBotones.FilterDataSource.Count - 1 Then
				CurrentIndex += 1
				BarraBotones.FilterNavigationPosition(CurrentIndex)
			End If

		ElseIf e.KeyCode = System.Windows.Forms.Keys.Left Then
			If INDGvHomologation.IsEditing Then
				INDGvHomologation.PostEditor()
			End If
			If CurrentIndex > 0 Then
				CurrentIndex -= 1
				BarraBotones.FilterNavigationPosition(CurrentIndex)
			End If

		ElseIf e.KeyCode = System.Windows.Forms.Keys.Tab Then
			INDBtnOk.Focus()
		End If
	End Sub

	Private Sub INDGvHomologation_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGvHomologation.KeyUp
        If (e.KeyCode = System.Windows.Forms.Keys.Right AndAlso Me.BarraBotones.FilterDataSource IsNot Nothing) OrElse e.KeyCode = System.Windows.Forms.Keys.Left Then
            INDGvHomologation.FocusedRowHandle = 0
        End If
    End Sub

End Class