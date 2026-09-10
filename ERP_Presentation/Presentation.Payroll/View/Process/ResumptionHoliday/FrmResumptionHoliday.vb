'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/06/2016
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports DevExpress.Data.Linq
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.Payroll.MVP

#End Region

Public Class FrmResumptionHoliday
    Implements IResumptionHoliday

#Region "Properties"

    ''' <summary>
    ''' Id del empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeId As Integer? Implements IResumptionHoliday.EmployeeId
        Get
            Return INDsleEmployee.EditValue
        End Get
        Set(value As Integer?)
            INDsleEmployee.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeXpo As LinqInstantFeedbackSource Implements IResumptionHoliday.EmployeeXpo
        Get
            Return INDsleEmployee.Properties.DataSource
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEmployee.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PResumptionHoliday

    ''' <summary>
    ''' Listado xpo de reanudación de vacaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListResumptionHolidayXpo As List(Of ResumptionHolidayXpo)

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que abre el form para reanudar las vacaciones del empleado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenFormResumption()
        'Se valida que hayan elegido un empleado
        If EmployeeId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un empleado"
            INDsleEmployee.Focus()
            Exit Sub
        End If
        Using formulario As New FrmPopupResumptionHoliday
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddResumptionHolidayEventArgs, AddressOf LoadDatasource
            formulario.EmployeeId = EmployeeId
            formulario.Size = New Size(681, 527)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que carga el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDatasource()
        If EmployeeId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un empleado"
            Exit Sub
        End If
        Try
            AsyncLoader(True)
            ListResumptionHolidayXpo = Presenter.ListResumptionHoliday(EmployeeId)
            INDgcResumption.DataSource = Nothing
            If ListResumptionHolidayXpo IsNot Nothing AndAlso ListResumptionHolidayXpo.Count > 0 Then
                INDgcResumption.DataSource = ListResumptionHolidayXpo
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListResumptionHolidayXpo = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmResumptionHoliday_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyResumptionHoliday, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PResumptionHoliday(Me)
        IndigoGridControl1.RefreshGrid(INDgcResumption)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.Minimizar(True)
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(529, Nothing, True)

        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleEmployee.QueryPopUp
        If EmployeeXpo Is Nothing Then
            Presenter.InitializeEmployee()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmResumptionHoliday_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEmployee.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de reanudar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnResumption_Click(sender As Object, e As EventArgs) Handles INDbtnResumption.Click
        OpenFormResumption()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEmployee.EditValueChanged
        If EmployeeId IsNot Nothing Then
            LoadDatasource()
        End If
    End Sub

#End Region

#End Region

End Class