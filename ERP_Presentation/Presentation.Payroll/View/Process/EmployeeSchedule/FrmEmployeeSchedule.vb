Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Design
Imports DevExpress.XtraEditors
Imports DevExpress.Data
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports System.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Spreadsheet
Imports System.Text
Imports Domain.Entities
Imports System.IO
Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Columns
Imports Presentation.CloudAgent

Public Class FrmEmployeeSchedule

    Private bwMassivePayrollDatas As BackgroundWorker = New BackgroundWorker
    ''' <summary>
    ''' Listado de notificaciones al importar.
    ''' </summary>
    Private ListMessages As List(Of Tuple(Of String, Integer))

    Private ListOutput As List(Of Tuple(Of String, Integer))

    Private validationResult As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Private validationResultMessage As List(Of SP_AnalisEmployeeSchedule_Result)

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AddHandler bwMassivePayrollDatas.DoWork, AddressOf bwListEmployeeSchedule_DoWork
        AddHandler bwMassivePayrollDatas.RunWorkerCompleted, AddressOf bwListEmployeeSchedule_RunWorkerCompleted
    End Sub

#End Region

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        bwMassivePayrollDatas = Nothing
        validationResult = Nothing
    End Sub


    Private Sub FrmMassiveManualConcepts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Using model As New MEmployeeSchedule(MyBase.Tag)
            INDSlFunctionalUnit.Properties.DataSource = model.ListAllFunctionalUnit()
            INDSlPosition.Properties.DataSource = model.ListAllPosition()
            INDSlEmployee.Properties.DataSource = model.ListEmployee()
        End Using
        INDSlFunctionalUnit.EditValue = Nothing
        INDSlPosition.EditValue = Nothing
        INDSlEmployee.EditValue = Nothing

        BarButtonsConfigure()
        CleanControls()
    End Sub

#Region "Backgroundworker"
    ''' <summary>
    ''' Inicia el backgroundWorker para consultar el listado de novedades masivo.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwListEmployeeSchedule_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        e.Cancel = False
        Using model As New MEmployeeSchedule(Me.Tag)

            Dim Year = INDCdnMonthYear.GetYear
            Dim Month = INDCdnMonthYear.GetMonth

            Dim InitialDate As New Date(Year, Month, 1)
            Dim EndDate As New Date(Year, Month, Date.DaysInMonth(Year, Month))

            Dim IdFunctionalUnit As Integer = 0
            Dim IdPosition As Integer = 0
            Dim IdEmployee As Integer = 0

            If INDSlFunctionalUnit.EditValue IsNot Nothing Then
                IdFunctionalUnit = INDSlFunctionalUnit.EditValue
            End If

            If INDSlPosition.EditValue IsNot Nothing Then
                IdPosition = INDSlPosition.EditValue
            End If

            If INDSlEmployee.EditValue IsNot Nothing Then
                IdEmployee = INDSlEmployee.EditValue
            End If

            validationResultMessage = model.AnalisisEmployeeSchedule(InitialDate, EndDate, IdFunctionalUnit, IdPosition, IdEmployee)

            If validationResultMessage.Any(Function(x) x.Warning = 2) Then
                Dim ListError As New List(Of SP_AnalisEmployeeSchedule_Result)

                ListError = validationResultMessage.Where(Function(x) x.Warning = 2).ToList()
                ListMessages = New List(Of Tuple(Of String, Integer))

                For Each linea As SP_AnalisEmployeeSchedule_Result In ListError
                    Dim mensaje As String = String.Format("{0}. {1}", linea.Cedula, linea.Observation)
                    ListMessages.Add(New Tuple(Of String, Integer)(mensaje, 2))
                Next
            End If


        End Using
    End Sub

    Private Sub bwListEmployeeSchedule_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then 'Si hubo alguna cancelación
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No hay datos en el listado para poder validar"
            INDgcInformation.DataSource = Nothing
            Exit Sub
        End If
        'Se muestran las notificaciones de la importacion.
        If ListMessages IsNot Nothing AndAlso ListMessages.Count > 0 Then
            Using formulario As New FrmListErrors(ListMessages)
                formulario.StartPosition = FormStartPosition.CenterScreen
                formulario.Title = "Mensajes de error."
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False

        INDgcInformation.DataSource = validationResultMessage

        AsyncLoader(False)

    End Sub
#End Region

    ''' <summary>
    ''' Deshace los cambios en el form
    ''' </summary>
    Private Sub Deshacer()
        BarButtonsConfigure()
        CleanControls()
    End Sub

    ''' <summary>
    ''' Configura los botones de la barra botones
    ''' </summary>
    Private Sub BarButtonsConfigure()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDgcInformation.DataSource = Nothing
        validationResult = Nothing
        validationResultMessage = Nothing
        INDSlEmployee.EditValue = Nothing
        INDSlFunctionalUnit.EditValue = Nothing
        INDSlPosition.EditValue = Nothing
    End Sub


#Region "MenuContext"

    ''' <summary>
    ''' Menu con botón en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteItem()
    End Sub

    ''' <summary>
    ''' Menu desplegable en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteItem()
    End Sub

#End Region

    Private Sub DeleteItem()
        If MessageIndigo.Show("Está seguro de eliminar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If INDviewInfo.GetSelectedRows IsNot Nothing AndAlso INDviewInfo.GetSelectedRows.Length > 0 Then

        End If
    End Sub

#Region "Click"
    Private Sub INDSbProcesar_Click(sender As Object, e As EventArgs) Handles INDSbProcesar.Click
        'Metodo para enviar las fechas al modelo como filtro.
        If ValidateControlsReports() = True Then
            'Se llama el asyncrono para validar el excel siempre y cuando no haya un asyncrono en ejecución

            If bwMassivePayrollDatas.IsBusy Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
                Exit Sub
            End If

            'Se ejecuta el asyncrono
            AsyncLoader(True)
            bwMassivePayrollDatas.RunWorkerAsync()
        End If

    End Sub
    ''' <summary>
    ''' Validar que las fechas se hayan diligenciado.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        'Aqui se incluyen las validaciones en caso de que se agreguen mas filtros.
        Return Validations
    End Function
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Deshacer de la barra botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Guarda los saldos iniciales
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        ConfirmNovelties()
    End Sub

    Private Sub ConfirmNovelties()

        If MessageIndigo.Show("Está seguro de confirmar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim ListHoursExtras = validationResultMessage.Where(Function(x) x.ExtraTime = True And x.HoursExtraTime > 0).ToList

        If ListHoursExtras IsNot Nothing AndAlso ListHoursExtras.Count > 0 Then
            Using formulario As New FrmPopuUpExtraTime()
                formulario.StartPosition = FormStartPosition.CenterScreen
                formulario.ListHoursExtras = ListHoursExtras
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No existen empleados con Horas Extras para cargar a nómina"
        End If

    End Sub

#End Region


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

#Region "ButtonClick"
    Private Sub INDSlFunctionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDSlFunctionalUnit.EditValue = Nothing
        End If
    End Sub

    Private Sub INDSlPosition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlPosition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDSlPosition.EditValue = Nothing
        End If
    End Sub

    Private Sub INDSlEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDSlEmployee.EditValue = Nothing
        End If
    End Sub

#End Region

End Class