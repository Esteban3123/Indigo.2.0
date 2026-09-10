'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/06/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.Base
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.CrossCutting.Base
#End Region

Public Class FrmMassiveReplication
    Implements IMassiveReplication

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PMassiveReplication

#End Region

#Region "Properties"

    ''' <summary>
    ''' Libro destino
    ''' </summary>
    ''' <returns></returns>
    Public Property BookDestinationId As Integer? Implements IMassiveReplication.BookDestinationId
        Get
            Return INDsleBookDestination.EditValue
        End Get
        Set(value As Integer?)
            INDsleBookDestination.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource libro destino
    ''' </summary>
    ''' <returns></returns>
    Public Property BookDestinationXpo As XPInstantFeedbackSource Implements IMassiveReplication.BookDestinationXpo
        Get
            Return INDsleBookDestination.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBookDestination.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Libro origen
    ''' </summary>
    ''' <returns></returns>
    Public Property BookOriginId As Integer? Implements IMassiveReplication.BookOriginId
        Get
            Return INDsleBookOrigin.EditValue
        End Get
        Set(value As Integer?)
            INDsleBookOrigin.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource libro origen
    ''' </summary>
    ''' <returns></returns>
    Public Property BookOriginXpo As XPCollection Implements IMassiveReplication.BookOriginXpo
        Get
            Return INDsleBookOrigin.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleBookOrigin.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha final
    ''' </summary>
    ''' <returns></returns>
    Public Property EndDate As Date? Implements IMassiveReplication.EndDate
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha inicial
    ''' </summary>
    ''' <returns></returns>
    Public Property InitialDate As Date? Implements IMassiveReplication.InitialDate
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo comprobante final
    ''' </summary>
    ''' <returns></returns>
    Public Property CodeEndJournalVoucherType As String Implements IMassiveReplication.CodeEndJournalVoucherType
        Get
            Return INDsleCodeEndJournalVoucherType.EditValue
        End Get
        Set(value As String)
            INDsleCodeEndJournalVoucherType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource tipo comprobante final
    ''' </summary>
    ''' <returns></returns>
    Public Property JournalVoucherTypeEndXpo As XPInstantFeedbackSource Implements IMassiveReplication.JournalVoucherTypeEndXpo
        Get
            Return INDsleCodeEndJournalVoucherType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCodeEndJournalVoucherType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo comprobante inicial
    ''' </summary>
    ''' <returns></returns>
    Public Property CodeInitialJournalVoucherType As String Implements IMassiveReplication.CodeInitialJournalVoucherType
        Get
            Return INDsleCodeInitialJournalVoucherType.EditValue
        End Get
        Set(value As String)
            INDsleCodeInitialJournalVoucherType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource tipo comprobante inicial
    ''' </summary>
    ''' <returns></returns>
    Public Property JournalVoucherTypeInitialXpo As XPInstantFeedbackSource Implements IMassiveReplication.JournalVoucherTypeInitialXpo
        Get
            Return INDsleCodeInitialJournalVoucherType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCodeInitialJournalVoucherType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMassiveReplication.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IMassiveReplication.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Item buscar del control de usuarios
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    ''' <summary>
    ''' Limpia los controles del formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyConfirmAnnular)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        INDdteInitialDate.Focus()
    End Sub

    ''' <summary>
    ''' Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Item Guardar del control de usuarios
    ''' </summary>
    Public Sub Guardar() Implements ICrudBase.Guardar
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -> Muestra Guardar | False -> Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Item Nuevo del control de usuario
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Confirma la replicación masiva
    ''' </summary>
    Private Async Function ConfirmMassiveReplication() As Task
        If ValidateControls() = False Then
            Exit Function
        End If
        Try
            Using model As New MMassiveReplication(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SP_MassiveReplication(InitialDate, EndDate, BookOriginId, BookDestinationId, CodeInitialJournalVoucherType, CodeEndJournalVoucherType)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    CleanControls()
                    INDdteInitialDate.Focus()
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw (ex)
        End Try
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        InitialDate = Nothing
        EndDate = Nothing
        BookDestinationId = Nothing
        CodeInitialJournalVoucherType = Nothing
        CodeEndJournalVoucherType = Nothing
        JournalVoucherTypeEndXpo = Nothing
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que vacía las propiedades que están asociadas a la instancia del formulario cuando este se cierra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMassiveReplication_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PMassiveReplication(Me)
        Deshacer()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged
        If InitialDate IsNot Nothing Then
            INDdteEndDate.Properties.MinValue = InitialDate
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de libro origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBookOrigin_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBookOrigin.EditValueChanged
        If BookOriginId IsNot Nothing Then
            BookDestinationId = Nothing
            BookDestinationXpo = Nothing
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBookOrigin_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBookOrigin.QueryPopUp
        If BookOriginXpo Is Nothing Then
            Presenter.InitializeBookOrigin()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de libro destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBookDestination_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBookDestination.QueryPopUp
        If BookDestinationXpo Is Nothing AndAlso BookOriginId IsNot Nothing Then
            Presenter.InitializeBookDestination(BookOriginId)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo comprobante inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleJournalVoucherTypeInitial_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCodeInitialJournalVoucherType.QueryPopUp
        If JournalVoucherTypeInitialXpo Is Nothing Then
            JournalVoucherTypeInitialXpo = Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo comprobante final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleJournalVoucherTypeEnd_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCodeEndJournalVoucherType.QueryPopUp
        If JournalVoucherTypeEndXpo Is Nothing Then
            JournalVoucherTypeEndXpo = Presenter.InitializeJournalVoucherType()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de libros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBookOrigin_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBookOrigin.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeBookOrigin()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de libros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBookDestination_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBookDestination.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            If BookOriginId IsNot Nothing Then
                Presenter.InitializeBookDestination(BookOriginId)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de tipo comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleJournalVoucherTypeInitial_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCodeInitialJournalVoucherType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            JournalVoucherTypeInitialXpo = Presenter.InitializeJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de tipo comprobante contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleJournalVoucherTypeEnd_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCodeEndJournalVoucherType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, Nothing, True)
            JournalVoucherTypeEndXpo = Presenter.InitializeJournalVoucherType()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMassiveReplication_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Presenter.InitializeBookOrigin()
        If BookOriginXpo IsNot Nothing AndAlso BookOriginXpo.Count > 0 Then
            Dim info As BookXpo = (From b In BookOriginXpo Where b.OfficialBook = True Select b).FirstOrDefault
            If info IsNot Nothing Then
                BookOriginId = info.Id
            Else
                Mensaje(EeventViewerImages.Advertencia) = "No existe un libro oficial."
                BookOriginId = Nothing
            End If
        End If
        INDdteInitialDate.Focus()
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Click confirmar
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await ConfirmMassiveReplication()
    End Sub

    ''' <summary>
    ''' Click deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

#End Region

End Class