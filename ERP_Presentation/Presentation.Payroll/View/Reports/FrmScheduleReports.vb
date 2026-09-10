'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 03-03-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls.MVP
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Payroll.Entities
Imports DevExpress.XtraEditors
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.CrossCutting.Resources
Imports System.Drawing

Imports Presentation.Reporter
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Common.MVP
#End Region

''' <summary>
''' Form de reportes
''' </summary>
''' <remarks></remarks>
Public Class FrmScheduleReports

#Region "Globals"
    ''' <summary>
    ''' Lista de id's de los empleados a consultar el reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListEmployesToDo As List(Of Integer)

    Dim varSelect As ImageComboBoxEdit
#End Region

#Region "Properties"

    Public validateConcept As Integer = 0

    Public Property ProoftCloseXpoConcept As XPInstantFeedbackSource

    Public Property ProoftCloseXpoEmployee As LinqInstantFeedbackSource

    Private _FillingSearchBy As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingSearchBy As List(Of Tuple(Of Integer, String))
        Get
            If _FillingSearchBy Is Nothing Then
                _FillingSearchBy = New List(Of Tuple(Of Integer, String))
                _FillingSearchBy.Add(New Tuple(Of Integer, String)(1, "Empleado"))
                _FillingSearchBy.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
                _FillingSearchBy.Add(New Tuple(Of Integer, String)(3, "Grupo"))
            End If
            Return _FillingSearchBy
        End Get
    End Property

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
#End Region


    ''' <summary>
    ''' Método para cargar el DataSource Del Control INDSleConceptos
    ''' </summary>
    ''' <remarks></remarks> 
    Private Sub LoadXpoConcepts()
        Using msearch As New MBusqueda

            ProoftCloseXpoConcept = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsPayrollAll)
            INDSleConcepts.Datasource = ProoftCloseXpoConcept            

            'ProoftCloseXpoEmployee = msearch.ConsultarEntidades(eDataSource.AllEmployees)
            'INDSleEmployee.Properties.DataSource = ProoftCloseXpoEmployee

        End Using
    End Sub




#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListEmployesToDo = Nothing
        varSelect = Nothing
        validateConcept = Nothing
        ProoftCloseXpoConcept = Nothing
        ProoftCloseXpoEmployee = Nothing
    End Sub

    ''' <summary>
    ''' Load Del Formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmScheduleReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '******************* Ajustar tamaños de controles *********************'
        INDsleFunctionalUnit.Properties.PopupFormSize = New System.Drawing.Size(INDsleFunctionalUnit.Width, 300)
        INDsleGroup.Properties.PopupFormSize = New System.Drawing.Size(INDsleGroup.Width, 300)
        INDGleSearchBy.Properties.DataSource = FillTuple(0)

        INDGleSearchBy.EditValue = 1

        '****************** Inicializar ******************'
        Inizialite()

        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.Minimizar(True)
    End Sub
#End Region

#Region "Events"

    ''' <summary>
    ''' Cargar los empleados al datasource del control INDSleEmployee
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleEmployee_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleEmployee.QueryPopUp
        If INDSleEmployee.Properties.DataSource Is Nothing Then
            LoadXpoEmployee()
        End If
    End Sub

    Private Sub INDGleSearchBy_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSearchBy.EditValueChanged

        If Not INDGleSearchBy.EditValue = Nothing Then
            If INDGleSearchBy.EditValue = 1 Then 'flitrar por empleado
                INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDsleFunctionalUnit.EditValue = Nothing
                INDsleGroup.EditValue = Nothing
            ElseIf INDGleSearchBy.EditValue = 2 Then 'filtrar por unidad funcional
                INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDsleGroup.EditValue = Nothing
                INDSleEmployee.EditValue = Nothing
            ElseIf INDGleSearchBy.EditValue = 3 Then 'filtrar por grupo
                INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleFunctionalUnit.EditValue = Nothing
                INDSleEmployee.EditValue = Nothing
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' propiedad para Cargar los valores del segundo criteria
    ''' </summary>
    ''' ''' <param name="ValueCriteria1"></param>
    ''' <remarks></remarks>
    Public Function FillTuple(ByVal ValueCriteria1 As Integer)
        Dim ListTuple As New List(Of Tuple(Of Integer, String))
        If ValueCriteria1 = 0 Then
            ListTuple.Add(New Tuple(Of Integer, String)(1, "Empleado"))
            ListTuple.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
            ListTuple.Add(New Tuple(Of Integer, String)(3, "Grupo"))
        ElseIf ValueCriteria1 = 1 Then
            ListTuple.Add(New Tuple(Of Integer, String)(1, "Empleado"))
            ListTuple.Add(New Tuple(Of Integer, String)(2, "Unidad Funcional"))
        ElseIf ValueCriteria1 = 2 Or ValueCriteria1 = 3 Or ValueCriteria1 = 7 Or ValueCriteria1 = 8 Or ValueCriteria1 = 9 Or ValueCriteria1 = 10 Or ValueCriteria1 = 11 Or ValueCriteria1 = 12 Or ValueCriteria1 = 13 Then
            ListTuple.Add(New Tuple(Of Integer, String)(1, "Empleado"))
            ListTuple.Add(New Tuple(Of Integer, String)(3, "Grupo"))
        End If
        Return ListTuple
    End Function

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleEmployee
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoEmployee()
        Using msearch As New MBusqueda
            ProoftCloseXpoEmployee = msearch.ConsultarEntidades(eDataSource.ListAllEmployee)
            INDSleEmployee.Properties.DataSource = ProoftCloseXpoEmployee
        End Using
    End Sub


    ''' <summary>
    ''' Inicializa datos
    ''' </summary>
    ''' <remarks></remarks>
    Sub Inizialite()
        Using model As New MBusqueda
            INDsleFunctionalUnit.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit)
            INDsleGroup.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GroupsPayroll)
        End Using
    End Sub

    ''' <summary>
    ''' Valida controles para el reporte de Cuadro de Turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateControlsCuadroTurno() As Boolean
        ValidateControlsCuadroTurno = True
        If INDGleSearchBy.EditValue = 1 Then
            If INDSleEmployee.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciEmployee.Text)
                INDSleEmployee.Focus()
                ValidateControlsCuadroTurno = False
            End If
        Else
            If INDsleFunctionalUnit.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemFunctionalUnit.Text)
                INDsleFunctionalUnit.Focus()
                ValidateControlsCuadroTurno = False
            End If
        End If
    End Function

    ''' <summary>
    ''' Valida Controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateControls() As Boolean
        ValidateControls = True
        If INDdeInitialDate.EditValue Is Nothing Or INDdeEndingDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDdeInitialDate.Focus()
            ValidateControls = False
        ElseIf Me.INDdeInitialDate.EditValue > INDdeEndingDate.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDdeInitialDate.Focus()
            ValidateControls = False
        End If

        If INDGleSearchBy.EditValue = 1 Then
            If INDSleEmployee.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciEmployee.Text)
                INDSleEmployee.Focus()
                ValidateControls = False
            End If
        Else
            If INDsleGroup.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemGroup.Text)
                INDsleGroup.Focus()
                ValidateControls = False
            End If
        End If
        If validateConcept = 0 Then
            If INDCmbStatusPayroll.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemPayrollLiquidationConfirm.Text)
                INDsleGroup.Focus()
                ValidateControls = False
            End If
        End If        
        If validateConcept = 1 Then
            If INDSleConcepts.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciConcept.Text)
                INDSleConcepts.Focus()
                ValidateControls = False
            End If
            validateConcept = 0
        End If
        

    End Function

    ''' <summary>
    ''' Valida controles para el reporte de Cuadro de Turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateControlsKardex() As Boolean
        ValidateControlsKardex = True

        If INDSleEmployee.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDLciEmployee.Text)
            INDSleEmployee.Focus()
            ValidateControlsKardex = False
        End If
    End Function

    Private Sub CleanControls()
        INDCmbReports.EditValue = Nothing
        INDdeInitialDate.EditValue = Nothing
        INDdeEndingDate.EditValue = Nothing
        INDGleSearchBy.Enabled = True
        INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDProcessButton.Enabled = False
    End Sub

#End Region


    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()
        Dim Validations As Boolean = True
        If Me.INDdeInitialDate.EditValue > INDdeEndingDate.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmReportAuxiliary_CompareDate", "Accounting"))
            Me.INDdeInitialDate.Focus()
            Validations = False
        End If
        If INDdeInitialDate.EditValue Is Nothing Or INDdeEndingDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDdeInitialDate.Focus()
            Validations = False
        End If
        Return Validations
    End Function



  

#Region "Bar buttons events"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
    '    Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
    '    'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
    '    'Me.BarraBotones.PrepareToolbar(eAction.OnlyGenerateFile)
    'End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
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

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
    End Sub

    ''' <summary>
    ''' Barra de botones Click Generar archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GenerateFile() Handles BarraBotones.Click_GenerateFile
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
    End Sub



#End Region

    Private Sub INDCmbReports_EditValueChanged(sender As Object, e As EventArgs) Handles INDCmbReports.EditValueChanged
        varSelect = sender

        If varSelect.EditValue = 1 Then 'Cuadro de Turno
            INDlyItemDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.Both
            INDlyItemDateNavigator.MinSize = New Size(360, 72)
            INDlyItemDateNavigator.MaxSize = New Size(360, 72)
            INDlyItemInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPayrollLiquidationConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleSearchBy.Properties.DataSource = FillTuple(varSelect.EditValue)
            INDGleSearchBy.EditValue = 2
        ElseIf varSelect.EditValue = 2 Or varSelect.EditValue = 3 Or varSelect.EditValue = 7 Or varSelect.EditValue = 8 Or varSelect.EditValue = 9 Or varSelect.EditValue = 10 Or varSelect.EditValue = 11 Or varSelect.EditValue = 12 Or varSelect.EditValue = 13 Then 'Control de Nómina y Desprendible de Pago
            INDlyItemPayrollLiquidationConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If varSelect.EditValue = 12 Then
                INDLciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPayrollLiquidationConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDLciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            INDlyItemInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDGleSearchBy.Properties.DataSource = FillTuple(varSelect.EditValue)
            INDGleSearchBy.EditValue = 3
        ElseIf varSelect.EditValue = 4 Then
            INDlyItemDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.Both
            INDlyItemDateNavigator.MinSize = New Size(360, 72)
            INDlyItemDateNavigator.MaxSize = New Size(360, 72)
            INDlyItemInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPayrollLiquidationConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSearchBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf varSelect.EditValue = 5 Then
            INDlyItemDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear
            INDlyItemDateNavigator.MinSize = New Size(360, 36)
            INDlyItemDateNavigator.MaxSize = New Size(360, 36)
            INDlyItemInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPayrollLiquidationConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSearchBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf varSelect.EditValue = 6 Then
            INDlyItemDateNavigator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDCtrDateNavigator.HowShowControl = CtrDateNavigator.EHowShowControl.Both
            INDlyItemInitialDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemEndingDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPayrollLiquidationConfirm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSearchBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        End If

        INDCmbReports.ClosePopup()

        If varSelect.EditValue = Nothing Then
            INDProcessButton.Enabled = False
        Else
            INDProcessButton.Enabled = True
        End If
        'Me.BarraBotones.PrintReport(PrintReportAction.None, INDsleFunctionalUnit.EditValue, 0, INDsleFunctionalUnit.EditValue, INDCtrDateNavigator.GetMonth, INDCtrDateNavigator.GetYear, Me.BarraBotones.OperatingUnit)

    End Sub



    ''' <summary>
    ''' Se ejecuta al darle clic al Botón INDProcessButton para generar el reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDProcessButton_Click(sender As Object, e As EventArgs) Handles INDProcessButton.Click



        If ValidateControlsReports() = True Then
            AsyncLoader(True)
            Dim reporte As New rptPayrollRelationEmployee
            reporte.ParametrosReporte = New Object() {INDsleGroup.EditValue,
                                                      INDdeInitialDate.EditValue,
                                                      INDdeEndingDate.EditValue,
                                                      INDCmbStatusPayroll.EditValue,
                                                      INDSleEmployee.EditValue,
                                                      INDSleConcepts.EditValue}

            INDDvReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()

            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                reporte.CreateDocument(True)
                Me.INDlyScheduleReports.Visible = False
                Me.INDCtrNavigation.Visible = False
                Me.INDPcReport.Visible = True
                INDDvReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDdeInitialDate.Focus()
            End If


        End If



    End Sub

    Private Sub INDCmbReports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDCmbReports.SelectedIndexChanged

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleConcepts
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleConcepts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleConcepts.QueryPopUp

        If INDSleConcepts.Datasource Is Nothing Then
            LoadXpoConcepts()
        End If

    End Sub

    Private Sub INDCnReport_ClickBack() Handles INDCnReport.ClickBack
        Me.INDlyScheduleReports.Visible = True
        Me.INDCtrNavigation.Visible = True
        Me.INDPcReport.Visible = False
        Me.INDdeInitialDate.Focus()
    End Sub
End Class