'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 19-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities.ObjectChangeTracker
Imports Domain.Entities
Imports System.Text
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports System.Windows.Forms

#End Region

Public Class FrmMontClose
    Implements ICloseMonth

#Region "Builder"

    ''' <summary>
    '''  En este constructor se inicializan los componentes del formulario y se crea un temporizador con un intervalo de 5 segundos
    ''' </summary>
    Sub New()
        InitializeComponent()
        timer = New Timers.Timer(5000)
    End Sub
#End Region

#Region "Fields"

    ''' <summary>
    ''' La variable timer se convertirá en un objeto que puede responder a eventos específicos generados por el objeto Timers.Timer
    ''' </summary>
    Private WithEvents timer As Timers.Timer

    ''' <summary>
    ''' Propiead que contiene  el datasource de los meses
    ''' </summary>
    Private ListMont As List(Of CloseMonthComplex)

    ''' <summary>
    ''' Se declara la propiedad Month que implementa la interfaz ICloseMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property Month As Integer Implements ICloseMonth.Month

    ''' <summary>
    ''' Se declara la propiedad Status que implementa la interfaz ICloseMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements ICloseMonth.Status

    ''' <summary>
    ''' Se declara la propiedad Year que implementa la interfaz ICloseMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property Year As Integer Implements ICloseMonth.Year

    ''' <summary>
    ''' Propiedad que almacenará una instancia de la clase ClosedMonth
    ''' </summary>
    ''' <returns></returns>
    Public Property _CloseMonth As ClosedMonth

    ''' <summary>
    ''' Propiedad que contendrá registros de comprobantes contables obtenidos a través de la ejecución de un SP
    ''' </summary>
    Private ListConfirmJournalVouchers As List(Of SP_GetJournalVouchersByStatus_Result)

    ''' <summary>
    ''' Propiedad que contendrá registros de comprobantes contables obtenidos a través de la ejecución de un SP
    ''' </summary>
    Private ListJournalVouchersConfirm As List(Of SP_ChangeStatusJournalVouchers_Result)
#End Region

#Region "Constant"
    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Accounting"
#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se hace click en el control "Seleccione Mes"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub glMonth_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles glMonth.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            LoadMonths()
            timer.Start()
            glMonth.Properties.Buttons(1).Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' metodo para cargar el datasource
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadMonths()
        Using mSearch As New MCloseMonth(Me.Tag)
            Dim year As Integer
            year = INDCtrDateNavigator.GetYear
            ListMont = Await mSearch.GetAllMonth(year, False)
            glMonth.Properties.DataSource = ListMont
            glMonth.EditValue = Nothing
        End Using
    End Sub

    ''' <summary>
    ''' evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMontClose_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear
        'glMonth.Properties.Buttons(2).Visible = False
        BarraBotones.Minimizar(True)
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Copiar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Pegar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Cortar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        BarraBotones.RibbonPagEform.Visible = False
        LoadMonths()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the BtnProcess control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BtnProcess_Click(sender As Object, e As EventArgs) Handles BtnProcess.Click
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el objeto BarraBotones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        BarraBotones.PrepareToolbar(eAction.OnlyNew)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el valor seleccionado en el control "Seleccione Mes" cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub glMonth_EditValueChanged(sender As Object, e As EventArgs) Handles glMonth.EditValueChanged
        If glMonth.EditValue IsNot Nothing Then
            Using model As New MCloseMonth(Me.Tag)
                _CloseMonth = Await model.GetMonth(glMonth.EditValue, INDCtrDateNavigator.GetYear)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = False
                BarraBotones.SetDocuments(_CloseMonth.Id, Me.Tag, Nothing, GetType(ClosedMonth).Name)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                BarraBotones.CleanAuditBasic()
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), _CloseMonth.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), _CloseMonth.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), _CloseMonth.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), _CloseMonth.ModificationDate)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el temporizador "timer" ha transcurrido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub timer_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles timer.Elapsed
        timer.Stop()
        If glMonth.InvokeRequired Then
            glMonth.BeginInvoke(Sub()
                                    glMonth.Properties.Buttons(1).Enabled = True
                                End Sub)
        Else
            glMonth.Properties.Buttons(1).Enabled = True
        End If
    End Sub
#End Region

#Region "Metodos de la interfaz"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        BarraBotones.CleanAuditBasic()
        glMonth.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            AsyncLoader(True)
            If Await ValidateMonth() = False Then
                AsyncLoader(False)
                Exit Sub
            End If

            AssingValues()
            Using Model As New MCloseMonth(Me.Tag.ToString())
                ListConfirmJournalVouchers = Nothing
                Dim modelJournalVouchers As New MDocumentAccount("")
                Dim listJournal As List(Of SP_GetJournalVouchersByStatus_Result)
                listJournal = Await modelJournalVouchers.GetJournalVourchersByStatus(1, glMonth.EditValue, INDCtrDateNavigator.GetYear)
                Dim formulario As New frmPopupJournalVouchers()
                If listJournal IsNot Nothing Then
                    Me.Cursor = ChangeCursorIndigo()
                    formulario = New frmPopupJournalVouchers
                    formulario.ListJournalVouchers = listJournal
                    formulario.ViewModeEditHold = True
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    formulario.Month = CInt(glMonth.EditValue)
                    formulario.MonthName = glMonth.Text
                    transparent.ShowDialog(Me)
                Else
                    formulario.Confirm = True
                    formulario.DialogResult = System.Windows.Forms.DialogResult.OK
                End If
                If formulario.DialogResult = System.Windows.Forms.DialogResult.OK Then
                    If formulario.Confirm = True Then

                        'valido que los saldos esten balanceados en el mes a cerrar
                        Dim resultBalance = Model.ValidateBalanceCloseMonth(glMonth.EditValue, INDCtrDateNavigator.GetYear)
                        If resultBalance.StatusCode <> eStatusResult.SUCCESS Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se puede cerrar el mes seleccionado"
                            Using formularioError As New FrmListErrors(resultBalance.ObjectEmbbeded)
                                formularioError.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formularioError, False)
                                transparent.ShowDialog(Me)
                            End Using
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        Dim result = Await Model.SaveCloseMonth(_CloseMonth, Nothing, Nothing, Nothing)
                        BarraBotones.SetDocuments(_CloseMonth.Id)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveCloseSingle", "Accounting"), glMonth.Text)
                            AsyncLoader(False)
                            _CloseMonth = Nothing
                            Deshacer()
                            LoadMonths()
                        Else
                            If result.MessageResult(0) = ErrorConcurrencia Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                            AsyncLoader(False)
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("PeriodCannotClosed", "Accounting"), glMonth.Text)
                        AsyncLoader(False)
                    End If
                Else
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region

#Region "Metodos"
    ''' <summary>
    ''' funcion para validar el mes que se desea cerrar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ValidateMonth() As Task(Of Boolean)
        Using modelo As New MCloseMonth(Me.Tag)
            _CloseMonth = Await modelo.GetMonth(CInt(glMonth.EditValue), INDCtrDateNavigator.GetYear)
            If _CloseMonth Is Nothing Then
                _CloseMonth = New ClosedMonth
            Else
                'valido que el mes que se quiere cerrar no este ya cerrado
                If _CloseMonth.Status = False Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("CloseMonth")
                    Return False
                Else
                    If (glMonth.EditValue = 1) Then
                        'validacion para enero
                        If Await modelo.ValidateOpenMonth(12, INDCtrDateNavigator.GetYear - 1, False) = True Then
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ClosePreviousMonth")
                            Return False
                        Else
                            Return True
                        End If
                    Else
                        'valido que el mes anterior este cerrado
                        If Await modelo.ValidateOpenMonth(glMonth.EditValue - 1, INDCtrDateNavigator.GetYear, False) = True Then
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ClosePreviousMonth")
                            Return False
                        Else
                            Return True
                        End If
                    End If
                End If
            End If
            Return False
        End Using
    End Function

    ''' <summary>
    ''' Assings the values.
    ''' </summary>
    Private Sub AssingValues()
        With _CloseMonth
            .Month = CInt(glMonth.EditValue)
            .Year = INDCtrDateNavigator.GetYear
            .Status = False
        End With
    End Sub


#End Region
    ''' <summary>
    ''' Evento que se activa cuando hay un cambio de fecha en el control de fechas y llama el método LoadMonths
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCtrDateNavigator_OnChangeDate(sender As Object, e As EventArgs) Handles INDCtrDateNavigator.OnChangeDate
        LoadMonths()
    End Sub
End Class
