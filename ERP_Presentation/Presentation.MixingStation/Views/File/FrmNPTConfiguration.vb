'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Giovanny Plazas
' Created          : 20/12/2021
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Threading
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP
#End Region

Public Class FrmNPTConfiguration
    Implements INPTConfiguration

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim NPTConfiguration As NPTConfiguration

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PNPTConfiguration

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64


#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements INPTConfiguration.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements INPTConfiguration.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDGcNPTConfiguration.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements INPTConfiguration.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property ListNPTConfiguration As List(Of NPTConfiguration) Implements INPTConfiguration.ListNPTConfiguration
        Get
            Return CType(INDGcNPTConfiguration.DataSource, List(Of NPTConfiguration))
        End Get
        Set(value As List(Of NPTConfiguration))
            INDGcNPTConfiguration.DataSource = value
        End Set
    End Property



#End Region

#Region "ICrudBase"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Me.AsyncLoader(True)
        Try
            'AssigningValues()
            Using model As New MNPTConfiguration(Me.Tag.ToString())
                Dim result = Await model.SaveNPTConfiguration(NPTConfiguration)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "Se agregó exitosamente"
                    ListNPTConfiguration = result.ObjectEmbbeded
                    INDGcNPTConfiguration.RefreshDataSource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Await LoadControls()
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' elimina registros de la rejilla
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        Dim SelectedRows = INDGvNPTConfiguration.GetSelectedRows().Select(Function(x) CType(INDGvNPTConfiguration.GetRow(x), NPTConfiguration)).ToList()

        If SelectedRows IsNot Nothing AndAlso SelectedRows.Count > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MNPTConfiguration(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteNPTConfiguration(SelectedRows)
                    AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = "Se ejecutó la acción con exito"
                        ListNPTConfiguration = result.ObjectEmbbeded
                        INDGcNPTConfiguration.RefreshDataSource()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Await LoadControls()
                    End If
                End Using
            End If
        End If
    End Sub


#End Region

#Region "Methods"
    ''' <summary>
    ''' Carga la información
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        Using Model As New MNPTConfiguration(CStr(Me.Tag))
            AsyncLoader(True)
            ListNPTConfiguration = Await Model.ListAllNPTConfiguration()
            INDGcNPTConfiguration.RefreshDataSource()
            AsyncLoader(False)
        End Using
    End Function

    ''' <summary>
    ''' crea la columna de acciones
    ''' </summary>
    Private Sub ActionsColumn()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvNPTConfiguration, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvNPTConfiguration.Columns
            If col.Name = "colActions" Then
                col.Width = 100
                col.Visible = True
            End If
        Next
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmNPTConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PNPTConfiguration(Me)
        ActionsColumn()
        LoadControls()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmNPTConfiguration_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Se ejecuta al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmNPTConfiguration_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    End Sub

#End Region

#End Region

#Region "BarButton Events"


    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.None)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "ItemCLick"
    ''' <summary>
    ''' ejecuta el menu de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim tagGrid As String = ""
        If sender.GetType() Is GetType(DevExpress.XtraEditors.SimpleButton) Or sender.GetType() Is GetType(DevExpress.XtraBars.BarButtonItem) Then
            tagGrid = sender.Tag.ToString()
        Else
            tagGrid = sender.Text
        End If
        Eliminar()
    End Sub

    ''' <summary>
    ''' Recarga la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDlygPrincipalInformation_CustomButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.BaseButtonEventArgs) Handles INDlygPrincipalInformation.CustomButtonClick
        If e.Button.Properties.VisibleIndex = 1 Then
            OpenFormAddNTP()
        Else
            LoadControls()
        End If
    End Sub

    Private Sub OpenFormAddNTP()
        Using frm As New FrmPopupAddNPT()
            AddHandler frm.AddNPTConfiguration, Sub(sender, e)
                                                    NPTConfiguration = e
                                                    NPTConfiguration.MarkAsAdded()
                                                    Guardar()
                                                End Sub
            Dim t As New FrmTransparent(frm, False)
            t.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDlygPrincipalInformation_Click(sender As Object, e As EventArgs) Handles INDlygPrincipalInformation.Click

    End Sub
#End Region

End Class