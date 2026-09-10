'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/10/2015
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
Imports System.Text

#End Region

Public Class FrmSelectHomologations

#Region "PublicEvents"

    Public Event SetSelectHomologations(sender As Object, e As SetSelectHomologationsEventArgs)

#End Region

#Region "Properties"

    Private _ListNoQx As List(Of NoQxEntity)
    ''' <summary>
    ''' Obtiene el listado NoQx que viene del form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListNoQx As List(Of NoQxEntity)
        Get
            Return _ListNoQx
        End Get
        Set(value As List(Of NoQxEntity))
            _ListNoQx = value
        End Set
    End Property

    Private _ListQx As List(Of QxEntity)
    ''' <summary>
    ''' Obtiene el listado Qx que viene del form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListQx As List(Of QxEntity)
        Get
            Return _ListQx
        End Get
        Set(value As List(Of QxEntity))
            _ListQx = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al item que se esta recorriendo en ese momento
    ''' </summary>
    ''' <remarks></remarks>
    Dim Record As Object

#End Region

#Region "Methods"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Metodo que carga el dataSource de la rejilla con las homologaciones de cada item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControl(Record As Object)
        'Se asigna el item que se va recorriendo a la variable global
        Me.Record = Record
        'Se asigna el item que va recorriendo dependiendo del listado NoQx o Qx
        INDgcHomologations.DataSource = Nothing
        INDgcHomologations.DataSource = Me.Record.ListCupsHomologation
    End Sub

    ''' <summary>
    ''' Metodo que valida si escogieron las homologaciones y acepta para enviarlas al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AcceptHomologations()
        'Se valida que hayan escogido al menos una homologación de cada item
        Dim ListErrors As New StringBuilder
        If ListNoQx IsNot Nothing AndAlso ListNoQx.Count > 0 Then
            Dim cont = 1
            For Each item In ListNoQx
                Dim quantityHomologations = (From l In item.ListCupsHomologation Where l.Activated = True Select l).Count
                If quantityHomologations = 0 Then
                    ListErrors.AppendLine("Debe elegir al menos una homologación del item " + cont.ToString)
                End If
                cont += 1
            Next
        ElseIf ListQx IsNot Nothing AndAlso ListQx.Count > 0 Then
            Dim cont = 1
            For Each item In ListQx
                Dim quantityHomologations = (From l In item.ListCupsHomologation Where l.Activated = True Select l).Count
                If quantityHomologations = 0 Then
                    ListErrors.AppendLine("Debe elegir al menos una homologación del item " + cont.ToString)
                End If
                cont += 1
            Next
        End If
        If ListErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ListErrors.ToString
            Exit Sub
        End If

        'Se eliminan las homologaciones que se necesitan para poder enviar solo una al form principal
        If ListNoQx IsNot Nothing AndAlso ListNoQx.Count > 0 Then
            For Each item In ListNoQx
                Dim cont = 0
                While item.ListCupsHomologation.Count > 1
                    If item.ListCupsHomologation(cont).Activated = False Then
                        item.ListCupsHomologation.Remove(item.ListCupsHomologation(cont))
                        cont = 0
                    Else
                        cont += 1
                    End If
                End While
            Next
        ElseIf ListQx IsNot Nothing AndAlso ListQx.Count > 0 Then
            For Each item In ListQx
                Dim cont = 0
                While item.ListCupsHomologation.Count > 1
                    If item.ListCupsHomologation(cont).Activated = False Then
                        item.ListCupsHomologation.Remove(item.ListCupsHomologation(cont))
                        cont = 0
                    Else
                        cont += 1
                    End If
                End While
            Next
        End If

        Dim args As New SetSelectHomologationsEventArgs
        args.ListNoQx = ListNoQx
        args.ListQx = ListQx
        RaiseEvent SetSelectHomologations(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Record = Nothing
        _ListNoQx = Nothing
        _ListQx = Nothing
    End Sub
    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSelectHomologations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "IPSServiceDescription"}}.ToList()
        If ListNoQx IsNot Nothing AndAlso ListNoQx.Count > 0 Then
            Me.BarraBotones.FilterDataSource = ListNoQx
            If ListNoQx.Count = 1 Then
                LoadControl(ListNoQx(0))
            End If
        ElseIf ListQx IsNot Nothing AndAlso ListQx.Count > 0 Then
            Me.BarraBotones.FilterDataSource = ListQx
            If ListQx.Count = 1 Then
                LoadControl(ListQx(0))
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar clic sobre el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddHomologations_Click(sender As Object, e As EventArgs) Handles INDbtnAddHomologations.Click
        AcceptHomologations()
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"

    ''' <summary>
    ''' Evento del siguiente siguiente de la barra botones
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        LoadControl(Record)
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control del check
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelectHomologations_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectHomologations.EditValueChanging
        Dim ListCupsHomologations As List(Of CupsHomologation) = Record.ListCupsHomologation
        If (From l In ListCupsHomologations Where l.Activated = True Select l).Count > 0 Then
            CType((From l In ListCupsHomologations Where l.Activated = True Select l).FirstOrDefault, CupsHomologation).Activated = False
        End If
        CType(viewHomologations.GetFocusedRow(), CupsHomologation).Activated = e.NewValue
        INDgcHomologations.RefreshDataSource()
    End Sub

#End Region

#End Region

End Class

Public Class SetSelectHomologationsEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Listado de NoQx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListNoQx As List(Of NoQxEntity)

    ''' <summary>
    ''' Listado de Qx
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListQx As List(Of QxEntity)

End Class