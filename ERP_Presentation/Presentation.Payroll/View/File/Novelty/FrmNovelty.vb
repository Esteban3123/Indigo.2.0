'***********************************************************************
' Assembly         : Presentacion.Corporation.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 21-08-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ComponentModel
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base.Extension
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP

Public Class FrmNovelty
    Implements IInability

#Region "Fields"

    ''' <summary>
    ''' Enumeracion para los estados de la novedad
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum statusNovelty As Integer
        normal = 0
        liquidado = 1
        liquidadoParcial = 2
    End Enum
    ''' <summary>
    ''' Enumeración para la clase de licencia
    ''' </summary>
    Private Enum eLicenseClass As Byte
        Remunerada = 1
        NoRemunerada = 2
        Permiso = 3
        ConCargoVacaciones = 4
        CalamidadDomestica = 5
        LicenciaLuto = 6
        DiaFamilia = 7
    End Enum

    ''' <summary>
    ''' Parámetros de nómina
    ''' </summary>
    Private _payrollSettings As PayrollSettingsXpo
    ''' <summary>
    ''' variable para almacenar una novedad
    ''' </summary>
    ''' <remarks></remarks>
    Dim noveltyEmployee As Novelty = New Novelty()
    ''' <summary>
    ''' Variable para almacenar los dias que paga la EPS
    ''' </summary>
    ''' <remarks></remarks>
    Dim diasEPS As Nullable(Of Integer)
    ''' <summary>
    ''' variable para almacenar los dias que debe pagar el patrono
    ''' </summary>
    ''' <remarks></remarks>
    Dim diasPatrono As Nullable(Of Integer)
    ''' <summary>
    ''' Variable para almacenar la base ya sea el sueldo o el ibc
    ''' </summary>
    ''' <remarks></remarks>
    Dim valorBase As Decimal
    ''' <summary>
    ''' Variable para almacenar el valor de la incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim valorIncapacidad As Nullable(Of Decimal)
    ''' <summary>
    ''' variable para almacenas el valor que paga la eps por la incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim valorEPS As Nullable(Of Decimal)
    ''' <summary>
    ''' Variable para almacenar el valor de la nomina
    ''' </summary>
    ''' <remarks></remarks>
    Dim valorNomina As Nullable(Of Decimal)
    ''' <summary>
    ''' Variable para almacenar el calor que debe pagar el patrono por la incapacidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim valorPatrono As Nullable(Of Decimal)
    ''' <summary>
    ''' Contrato vigente del empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim contrato As Domain.Payroll.Entities.Contract
    ''' <summary>
    ''' Lista de posibles incapacidades a mostrar en el combo
    ''' </summary>           
    ''' <remarks></remarks>
    Dim listIncapacidad As List(Of ItemOpcionNovelty) = New List(Of ItemOpcionNovelty)()
    ''' <summary>
    ''' Lista de las posibles acciones que se pueden mostar en el combo "Postular valor de:"
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPostularValor As List(Of ItemOpcionNovelty) = New List(Of ItemOpcionNovelty)()
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PInability
    ''' <summary>
    ''' Variable para almacenar los datos del empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim employee As Domain.Payroll.Entities.Employee
    ''' <summary>
    ''' Variable para almacenar los parametros del grupo donde esta el empleado
    ''' </summary>
    ''' <remarks></remarks>
    Dim parameterGroup As PayrollParameter
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Constante de salario Integral
    ''' </summary>
    Const IntegralSalary = 2
    ''' <summary>
    ''' Permite reconocer si la fecha de la nueva novedad es el inciio de un ciclo para el descuento en liquidación
    ''' </summary>
    Dim IsCycleDate As Boolean = False
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Datatable donde se almacenan los campos nulos
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    Dim FlagProrroga As Boolean = False

    Dim FlagExtensionEdit As Boolean = False

    Dim diasNovedad As Integer = 0

    Dim ListHolidays As List(Of Domain.Entities.Holiday)

    Dim EditMode As Boolean = False

    Dim DiasIncapacidadTotal As Integer = 0

    Dim TmpNovelty As New Novelty

    Dim LicenseClassCBG As ImageComboBoxEdit

    Private ReadOnly Property HandleBaseLiquidationPatrono As Boolean
        Get
            Return {1, 2}.Contains(INDcmbInabilityClass.EditValue) AndAlso _payrollSettings?.HandlesLiquidationFirstTwoDays
        End Get
    End Property


    ''' <summary>
    ''' Propiedad para obtener el valor de la clase de licencia
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property LicenseClassValue As Byte
        Get
            Return LicenseClassCBG.EditValue
        End Get
    End Property

    Private Property CalculationType As Byte? Implements IInability.CalculationType
        Get
            Return INDcmbCalculationType.EditValue
        End Get
        Set(value As Byte?)
            INDcmbCalculationType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o asigna el valor de la novedad que maneje concepto de salario integral
    ''' </summary>
    ''' <returns></returns>
    Private Property IntegralSalaryNoveltyValue As Decimal?
        Get
            Return INDteSIntegralValue.EditValue
        End Get
        Set(value As Decimal?)
            INDteSIntegralValue.EditValue = value
        End Set

    End Property
#End Region


#Region "Implementacion IInability MVP"
    ''' <summary>
    ''' Funcion que se encarga de abriri el formulario de busqueda del empleado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Employee
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Cedula", .FieldName = "Nit"}, New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name"}, New ColumnInfo() With {.Caption = "Grupo", .FieldName = "Descripcion"}}.ToList()
            'BarraBotonesPopup.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteEmployeeId.Text = ReturnValue
        If INDbteEmployeeId.Text <> String.Empty Then
            LoadControls()
            If INDbteEmployeeId.Enabled = False Then
                BarraBotonesPopup.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            'INDbteEmployeeId.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Funcion la cual llama a la funcion de AbrirBusqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' limpia los controles del popup y del formulario
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        CleanPopupNovedades()
    End Sub

    ''' <summary>
    ''' Funcion que se encarga de Eliminar una novedad
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        Try
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                AsyncLoader(True)
                Me.BarraBotonesPopup.Enabled = False
                Using model As New MNovelty(MyBase.Tag)
                    If Await model.DeleteNoveltyAsync(noveltyEmployee) = True Then
                        ' Await Me.DeleteDocumentIndexed()
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        CleanPopupNovedades()
                        CargarRegillasEmpleado(employee.Id)
                        PopupContainerMain.HidePopup()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End Using
                Me.BarraBotonesPopup.Enabled = True
                AsyncLoader(False)
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
            AsyncLoader(False)
        End Try

    End Sub

    ''' <summary>
    ''' Funcion que se encarga de guardar una novedad
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        Try
            AsyncLoader(True)
            Me.BarraBotonesPopup.Enabled = False

            'Valido que todos lo datos esten bien
            If validatedData() = False Then
                AsyncLoader(False)
                Me.BarraBotonesPopup.Enabled = True
                Return
            End If
            If noveltyEmployee.VacationInitialDateNovelty IsNot Nothing And noveltyEmployee.VacationEndDateNovelty IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NovedadReajusteVacacionesNoEditar, Incapacidades)

                AsyncLoader(False)
                Me.BarraBotonesPopup.Enabled = True
                Return
            End If
            Dim realDate As Date = INDdeRealDateNovelty.DateTime
            Dim endDate As Date
            If NoveltyLicensingType = 6 Or INDcmbInabilityClass.EditValue = 5 Then
                'LICENCIA POR LUTO O LICENCIA DE PATERNIDAD
                endDate = CType(INDdeRealDateNovelty.EditValue, Date).AddDays(diasNovedad - 1)
            Else
                endDate = CType(INDdeRealDateNovelty.EditValue, Date).AddDays(CType(INDteNroDaysNovelty.Text, Integer) - 1)
            End If

            ' Cargo los calendarios que existen entre la fecha inicial y la fecha final
            Dim detalleCalendarios As List(Of ScheduleDetail) = New List(Of ScheduleDetail)
            Dim listNovelties As List(Of Novelty) = New List(Of Novelty)
            Dim listVacation As List(Of Vacation) = New List(Of Vacation)

            Using model As New MNovelty(MyBase.Tag)
                listNovelties = Await model.GetNoveltyDistinctNoveltyBetweenDate(noveltyEmployee.Id, employee.Id, realDate, endDate)
                detalleCalendarios = Await model.GetScheduleDetailByEmployeeBetweenDateFromNovelty(employee.Id, realDate, endDate)
            End Using
            Using modelVacation As New MVacation(MyBase.Tag)
                listVacation = Await modelVacation.GetVacationBetweenDate(employee.Id, realDate, endDate)
                If listVacation IsNot Nothing AndAlso listVacation.Count > 0 Then

                    listVacation = listVacation.Where(Function(x) x.TypeVacation = 2).ToList()
                    If listVacation IsNot Nothing AndAlso listVacation.Count > 0 Then


                        If realDate = listVacation.Item(0).IncorporationDateReal Then
                            listVacation = Nothing
                        End If
                    End If

                End If
            End Using
            If listNovelties IsNot Nothing AndAlso listNovelties.Count > 0 Then
                PopupContainerMain.HidePopup()
                Dim formDetail As New FrmNoveltyDetail(listNovelties)
                formDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(formDetail, False)
                transparent.ShowDialog()

                AsyncLoader(False)
                Me.BarraBotonesPopup.Enabled = True
                Return

                Return
            End If
            If detalleCalendarios IsNot Nothing Then
                detalleCalendarios = detalleCalendarios.FindAll(Function(x) x.ScheduleTemplate IsNot Nothing)
                If detalleCalendarios.Count > 0 Then 'Si hay detalle en el rango de fecha de la novedad disparo el popup
                    PopupContainerMain.HidePopup()
                    Dim formDetail As New FrmNoveltyScheduleDetail(detalleCalendarios)
                    formDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    Dim transparent As New FrmTransparent(formDetail, False)
                    transparent.ShowDialog()
                    If formDetail.ActionOk = False Then
                        AsyncLoader(False)
                        Me.BarraBotonesPopup.Enabled = True
                        Return
                    End If
                End If
            End If
            If listVacation IsNot Nothing AndAlso listVacation.Count > 0 Then
                If INDcmbTypeNovelty.EditValue = 1 Or INDcmbTypeNovelty.EditValue = 3 Then 'Si es una incapacidad
                    PopupContainerMain.HidePopup()
                    If contrato.LastLiquidationDate IsNot Nothing AndAlso contrato.LastLiquidationDate <= realDate Then

                        If MessageIndigo.Show(obtenerRecurso(EmpleadoVacacionesReajuste, Incapacidades), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                            AsyncLoader(False)
                            Me.BarraBotonesPopup.Enabled = True
                            Return
                        End If
                    Else

                        If MessageIndigo.Show("El Empleado tiene vacaciones, pero la Incapacidad viene de un Mes con Nómina ya confirmada. Si se aprueba el reajuste, el cuadro de turnos de la Nómina del Mes ya confirmado NO será cambiado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                            AsyncLoader(False)
                            Me.BarraBotonesPopup.Enabled = True
                            Return
                        End If

                    End If

                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(EmpleadoVacacionesNoPuedeNovedad, Incapacidades)
                    AsyncLoader(False)
                    Me.BarraBotonesPopup.Enabled = True
                    Return
                End If
            End If
            Dim claseLicencia As Nullable(Of Byte) = Nothing
            Dim claseIncapacidad As Nullable(Of Byte) = Nothing
            Dim tipoRiesgo As Nullable(Of Byte) = Nothing
            Dim tipoCalculo As Nullable(Of Byte) = Nothing
            Select Case INDcmbTypeNovelty.EditValue
                Case 1 'Incapacidad
                    'Realizo las validaciones pertinenetes para las incapacidades
                    If CalculationType.IsNull Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyCalculationType.Text)
                        AsyncLoader(False)
                        Me.BarraBotonesPopup.Enabled = True
                        Return
                    ElseIf INDcmbInabilityClass.EditValue Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyInabilityClass.Text)
                        AsyncLoader(False)
                        Me.BarraBotonesPopup.Enabled = True
                        Return
                    ElseIf INDcmbInabilityClass.EditValue = 4 And INDcmbRiskType.EditValue Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyRiskType.Text)
                        AsyncLoader(False)
                        Me.BarraBotonesPopup.Enabled = True
                        Return
                    End If
                    tipoCalculo = CalculationType
                    claseIncapacidad = INDcmbInabilityClass.EditValue
                    If INDcmbInabilityClass.EditValue = 4 Then
                        tipoRiesgo = INDcmbRiskType.EditValue
                    End If
                Case 3 'Licencia
                    If NoveltyLicensingType = Nothing Then ' Valido que tenga seleccionado un tipo de licencia
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyLicenseClass.Text)
                        AsyncLoader(False)
                        Me.BarraBotonesPopup.Enabled = True
                        Return
                    End If
                    claseLicencia = CType(NoveltyLicensingType, Byte)
            End Select
            noveltyEmployee.GroupId = contrato.GroupId
            noveltyEmployee.EmployeeId = employee.Id
            noveltyEmployee.TypeNovelty = INDcmbTypeNovelty.EditValue
            noveltyEmployee.RealDate = realDate
            noveltyEmployee.Days = diasNovedad
            noveltyEmployee.EndDate = endDate
            noveltyEmployee.Reason = INDteReason.Text
            noveltyEmployee.LiquidatedIBCorSalary = INDteLiquidatedIBCorSalary.EditValue
            If INDteLiquidatedIBCorSalary.EditValue = 1 Then
                noveltyEmployee.IBC = valorBase
            Else
                noveltyEmployee.EmployeeBaseSalary = valorBase
            End If
            If valorBase < parameterGroup.LegalSalaryMinimum Then
                noveltyEmployee.LiquidationBase = parameterGroup.LegalSalaryMinimum
            Else
                noveltyEmployee.LiquidationBase = valorBase
            End If
            noveltyEmployee.LiquidationBasePatrono = INDTeBaseLiquidationPatrono.EditValue
            noveltyEmployee.LicenseClass = claseLicencia
            noveltyEmployee.InabilityClass = claseIncapacidad
            noveltyEmployee.RiskType = tipoRiesgo
            noveltyEmployee.CalculationType = tipoCalculo
            noveltyEmployee.EPSDays = diasEPS
            noveltyEmployee.EPSRecognizeValue = retornaNothingDecimal(valorEPS)
            noveltyEmployee.EmployerDays = diasPatrono
            noveltyEmployee.PaidEmployerValue = retornaNothingDecimal(valorPatrono)
            noveltyEmployee.PaidPayrollValue = retornaNothingDecimal(valorNomina)
            noveltyEmployee.Value = retornaNothingDecimal(valorIncapacidad)
            noveltyEmployee.AutorizationNumber = INDteAutorizationNumber.Text
            noveltyEmployee.NoveltyLiquidate = Me.BarraBotonesPopup.StatusRecord
            noveltyEmployee.IsCycleDate = IsCycleDate
            noveltyEmployee.IntegralSalaryDisabilityValue = IntegralSalaryNoveltyValue

            If indigo.IndigoCompanyType = "2" Then
                noveltyEmployee.ResolutionDate = INDDeResolutionDate.EditValue
                noveltyEmployee.ResolutionNumber = INDTxtResolutionNumber.EditValue
            End If


            Using model As New MNovelty(MyBase.Tag)
                Dim result = Await model.SaveInabilityAsync(noveltyEmployee)
                If result.StateResult = True Then
                    noveltyEmployee = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotonesPopup._listDocuments)
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    CleanPopupNovedades()
                    CargarRegillasEmpleado(employee.Id)
                    PopupContainerMain.HidePopup()
                    EditMode = False
                Else
                    If result.MessageResult.Count > 0 Then
                        Dim strMessage As String = ""
                        For Each message As MessageResult In result.MessageResult
                            Select Case message.CodeMessage
                                Case "V001"
                                    strMessage &= String.Format(obtenerRecurso(EmpleadoNoPuedeDias, Eform.Vacaciones), message.Parameters) & vbCrLf
                                Case "V002"
                                    strMessage &= String.Format(obtenerRecurso(EmpleadoNotieneDiasPendientes, Eform.Vacaciones), message.Parameters) & vbCrLf
                                Case "V003"
                                    strMessage &= String.Format(obtenerRecurso(EmpleadoEnVacaciones, Eform.Vacaciones), message.Parameters) & vbCrLf
                                Case Else
                                    strMessage &= message.CodeMessage
                            End Select
                        Next
                        Mensaje(EeventViewerImages.MensajeError) = strMessage
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesError)
                    End If
                End If
            End Using
            Me.BarraBotonesPopup.Enabled = True
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Sub

    Private Function validatedData() As Boolean
        If INDcmbTypeNovelty.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyTypeNovelty.Text)
            Return False
        End If
        'Realizo las validaciones que son generales para cualquieras de las tres opciones Incapacidad, 
        If INDdeRealDateNovelty.EditValue Is Nothing Then ' Fecha novedad
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyInitialDateNovelty.Text)
            Return False
        ElseIf INDteNroDaysNovelty.EditValue Is Nothing Or CType(INDteNroDaysNovelty.EditValue, Integer) = 0 Then 'Dias novedad
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyNumberDays.Text)
            Return False
        ElseIf INDteReason.EditValue Is Nothing Then 'Descripcion Novedad
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyReason.Text)
            Return False
        ElseIf INDteLiquidatedIBCorSalary.EditValue Is Nothing And NoveltyLicensingType <> 5 Then 'Liquidado con IBC o Sueldo
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyLiquidatedIBCorSalary.Text)
            Return False
        ElseIf INDteLiquidatedIBCorSalary.EditValue = 1 And (INDteMonthIBC.EditValue Is Nothing OrElse CType(INDteMonthIBC.Text, Integer) = 0) And FlagProrroga = False Then 'Si selecciono la opcion 1 de IBC
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyMonthIBC.Text)
            Return False
        ElseIf Me.BarraBotonesPopup.StatusRecord = True And INDcmbTypeNovelty.EditValue = 1 And (INDteAutorizationNumber.EditValue Is Nothing OrElse INDteAutorizationNumber.EditValue.ToString().Length = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyAutorizationNumber.Text)
            Return False
        End If
        Return True
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Funcion que dispara los mensaje y escribe en el visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Funcion que se ejecuta al crear una nueva novedad
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanPopupNovedades()
    End Sub

    ''' <summary>
    ''' Acciones del control
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInability.ActionsOnControls
        Set(value As Boolean)
            INDgcNoveltyPending.Enabled = value
            INDgcInability.Enabled = value
            INDgcSanctions.Enabled = value
            INDgcLicence.Enabled = value
            INDbtnAddNovelty.Enabled = value
        End Set
    End Property
    ''' <summary>
    ''' Porpiedad del data Source del concepto de licencias 
    ''' </summary>
    ''' <returns></returns>
    Public Property ListLicensingConceptsNoveltyXpo As XPInstantFeedbackSource Implements IInability.ListLicensingConceptsNoveltyXpo
        Get
            Return CType(INDSlConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlConcept.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad del campo Conceptos con su set y get
    ''' </summary>
    ''' <returns></returns>
    Private Property NoveltyLicensingType As Integer Implements IInability.NoveltyLicensingType
        Get
            Return INDcmbLicenseClass.EditValue
        End Get
        Set(value As Integer)
            INDcmbLicenseClass.EditValue = value
        End Set
    End Property
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmNoveltyMetaData, Eform.InfoMetaData), INDbteEmployeeId.Text, INDteEmployeeName.Text, INDcmbTypeNovelty.SelectedItem.ToString(), INDteReason.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & noveltyEmployee.Id & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmNoveltyMetaDataTitle, Eform.InfoMetaData), INDbteEmployeeId.Text),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmNoveltyMetaData, Eform.InfoMetaData), INDbteEmployeeId.Text, INDteEmployeeName.Text, INDcmbTypeNovelty.SelectedText, INDteReason.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmNoveltyMetaDataTitle, Eform.InfoMetaData), INDbteEmployeeId.Text)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(Liquidar, Incapacidades), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(NoLiquidar, Incapacidades), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotonesPopup.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MNovelty(MyBase.Tag)
                Await model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Try
            AsyncLoader(True)
            Using Model As New MEmployee(MEmployee.TAG)
                employee = Await Model.GetEmployeeAsync(INDbteEmployeeId.Text)
            End Using

            Await CalculateTwoFirstDaysAsync()

            If employee IsNot Nothing AndAlso employee.Id > 0 Then
                INDbtnAddNovelty.ShowDropDown()
                INDbtnAddNovelty.HideDropDown()
                ActionsOnControls = True
                CleanPopupNovedades()
                CargarRegillasEmpleado(employee.Id)
                Me.BarraBotones.PrintReport(PrintReportAction.None, employee.Id, 0, employee.Id, Me.BarraBotones.OperatingUnit)
                With employee
                    contrato = .Contract.Where(Function(x) x.Valid = True).ToList().Item(0)
                    LogicaBotonActualizar(True)
                    INDbteEmployeeId.Text = employee.ThirdParty.Nit
                    INDteEmployeeName.Text = employee.ThirdParty.Name
                    INDteSalary.Text = contrato.BasicSalary
                    INDLblPayrollNextDate.Text = contrato.Group.NextDateLiquidation
                    '---------------------------------------------------------------

                    Using model As New MNovelty(MyBase.Tag)
                        valorBase = Await model.GetAverageIBCLiquidationLastMonth(contrato.InitialContractNumber, 1)
                    End Using
                    INDteIBCLastMonth.Text = valorBase
                    '---------------------------------------------------------------                    
                    INDdeRealDateNovelty.Properties.MaxValue = contrato.ContractEndingDate
                    INDdeRealDateNovelty.Properties.MinValue = contrato.JobBondingDate
                End With
                Using modelParameter As New MPayrollParameter 'Obtenemos los parametros de nomina
                    parameterGroup = Await modelParameter.GetPayrollParameterAsync(contrato.GroupId)
                    If parameterGroup Is Nothing Or parameterGroup.Id = 0 Then
                        Using modelGroup As New MGroups(MGroups.TAG)
                            Dim grupo = Await modelGroup.GetGroupByIdAsync(contrato.GroupId)
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(GrupoSinParametros, Incapacidades), "[" & grupo.Code & " " & grupo.Name & "]")
                            Deshacer()
                        End Using
                    End If
                End Using
            Else
                LogicaBotonActualizar(False)
                MessageIndigo.Show(String.Format(obtenerRecurso(EmpleadoNoExiste, Incapacidades), INDbteEmployeeId.Text), MessageType.Warning, Me.Text)
                Deshacer()
            End If
            AsyncLoader(False)

        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' carga las regillas de novedades pendientes y demas novedades del empleado
    ''' </summary>
    ''' <param name="idEmpleado"></param>
    ''' <remarks></remarks>
    Private Sub CargarRegillasEmpleado(idEmpleado As Integer)
        Try
            Using modelInability As New MNovelty(MyBase.Tag)

                IndigoGridControl1.AcceptXPO = True
                INDgcNoveltyPending.DataSource = modelInability.ListInabilityLiquidate(idEmpleado, statusNovelty.normal)

                INDgcNoveltyPending.RefreshDataSource()
                INDcolNoveltyCode.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                INDcolNoveltyPendingRealDate.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                INDgcNoveltyPending.Visible = True
                INDgcInability.DataSource = modelInability.ListNoveltyByType(idEmpleado, 1) ' Cargo las incapacidades
                INDgcInability.RefreshDataSource()
                INDcolInabilityCode.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                INDcolInabilityRealDate.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                INDgcSanctions.DataSource = modelInability.ListNoveltyByType(idEmpleado, 2) ' Cargo las sanciones
                INDgcSanctions.RefreshDataSource()
                INDcolSactionsCode.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                INDcolSactionsRealDate.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

                INDgcLicence.DataSource = modelInability.ListNoveltyByType(idEmpleado, 3) ' Cargo las Licencias
                INDgcLicence.RefreshDataSource()
                INDcolLicenseCode.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
                INDcolRealDateLicense.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            End Using

            For i As Integer = 0 To INDgvNoveltyPending.Columns.Count - 1
                INDgvNoveltyPending.Columns.Item(i).BestFit()
            Next
            For i As Integer = 0 To INDgvSactions.Columns.Count - 1
                INDgvSactions.Columns.Item(i).BestFit()
            Next

        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Sub

    ''' <summary>
    ''' Limpia todos los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotonesPopup.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDbteEmployeeId.Text = String.Empty
        INDteEmployeeName.Text = String.Empty
        INDteSalary.Text = String.Empty
        INDteIBCLastMonth.Text = String.Empty
        INDgcInability.DataSource = Nothing
        INDgcLicence.DataSource = Nothing
        INDgcNoveltyPending.DataSource = Nothing
        INDgcSanctions.DataSource = Nothing
        INDTeBaseLiquidationPatrono.EditValue = 0
        employee = Nothing
        Me._doc = Nothing
        Me.BarraBotonesPopup.EnableBarItems()
        Me.BarraBotonesPopup.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup y los vuelve a dejar en su estado inicial es decir con algunos controles ocultos y desabilidatos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanPopupNovedades()

        INDbtnAddNovelty.Text = obtenerRecurso(CrearNovedad, Incapacidades)
        CleanControlsPopupNovedades()
        INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyCalculationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyInabilityClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyLicenseClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyLiquidatedIBCorSalary.HideLayout
        INDlyIBCValue.HideLayout
        INDSlConcept.Text = Nothing
        INDcmbTypeNovelty.Enabled = True
        INDdeRealDateNovelty.Enabled = False
        INDteNroDaysNovelty.Enabled = False
        INDteLiquidatedIBCorSalary.Enabled = False
        INDdeRealDateNovelty.EditValue = Nothing
        INDteReason.Enabled = False
        INDSlConcept.Enabled = False
        INDcmbTypeNovelty.Focus()

        INDteEmployerDays.EditValue = 0
        INDteEPSDays.EditValue = 0
        INDteLiquidationBase.EditValue = 0
        INDtePaidEmployerValue.EditValue = 0
        INDteEPSRecognizeValue.EditValue = 0
        INDtePaidPayrollValue.EditValue = 0
        INDteValueNovelty.EditValue = 0
        IntegralSalaryNoveltyValue = 0
        INDlciIntegralSalaryInabilityValue.HideLayout()
        FlagProrroga = False
        IsCycleDate = False
        Me._doc = Nothing
        Me.BarraBotonesPopup.StatusRecordVisible = True
        Me.BarraBotonesPopup.StatusRecord = True
        Me.BarraBotonesPopup.EnableBarItems()
        Me.BarraBotonesPopup.DisableBarDocument()
        BarraBotonesPopup.PrepareToolbar(eAction.OnlySave)
    End Sub

    ''' <summary>
    ''' Limpia el contenido de los controles del popup 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupNovedades(Optional limpiarTipoNovedad As Boolean = True)
        Try
            If limpiarTipoNovedad = True Then
                INDcmbTypeNovelty.EditValue = Nothing
                noveltyEmployee = New Novelty()
            End If

            INDteNroDaysNovelty.EditValue = Nothing
            If EditMode = False Then
                INDetEndDate.Text = Nothing
            End If
            INDteReason.Text = Nothing
            INDteLiquidatedIBCorSalary.EditValue = Nothing
            INDteMonthIBC.Text = Nothing
            INDcmbCalculationType.EditValue = Nothing
            INDcmbInabilityClass.EditValue = Nothing
            INDcmbRiskType.EditValue = Nothing
            NoveltyLicensingType = Nothing
            INDteEmployerDays.EditValue = Nothing
            INDteEPSDays.EditValue = Nothing
            INDteLiquidationBase.EditValue = Nothing
            INDteEPSRecognizeValue.EditValue = Nothing
            INDtePaidPayrollValue.EditValue = Nothing
            IntegralSalaryNoveltyValue = Nothing
            INDtePaidEmployerValue.EditValue = Nothing
            INDteValueNovelty.EditValue = Nothing
            INDteAutorizationNumber.Text = Nothing
            INDlciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSlConcept.Text = Nothing
            diasEPS = Nothing
            diasPatrono = Nothing
            valorBase = Nothing
            valorEPS = Nothing
            valorIncapacidad = Nothing
            valorNomina = Nothing
            valorPatrono = Nothing
            IsCycleDate = False
            INDTxtResolutionNumber.EditValue = Nothing
            INDDeResolutionDate.Text = Nothing
            INDcmbCalculationType.Properties.Items.Clear()
            INDcmbInabilityClass.Properties.Items.Clear()

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' carga las lista con las que se va a cargar los combos de incapacidades y tipo de calculo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cargarListasCombo()
        '-----------------------------------------------------------------------------------------------------------------------------------------------------
        listPostularValor.Add(New ItemOpcionNovelty(menorTres:=False, mayor2Menor91:=True, mayor90:=False, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(Postular23, Incapacidades), CType(1, Byte), -1)))

        listPostularValor.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=False, mayor90:=False, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(PostularPatrono, Incapacidades), CType(2, Byte), -1)))

        listPostularValor.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=True, maternidad:=True, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(PostularNomina, Incapacidades), CType(3, Byte), -1)))

        listPostularValor.Add(New ItemOpcionNovelty(menorTres:=False, mayor2Menor91:=False, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(Postular90Dias, Incapacidades), CType(4, Byte), -1)))

        listPostularValor.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=False, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(Postular23Todos, Incapacidades), CType(5, Byte), -1)))

        listPostularValor.Add(New ItemOpcionNovelty(menorTres:=False, mayor2Menor91:=False, mayor90:=False, riesgoProfesional:=False, maternidad:=False, mayor180:=True, ImageComboItem:=New ImageComboBoxItem("180 Días, no se pagará más por Nómina", CType(7, Byte), -1)))

        'Cargo las incapacidades
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------
        If indigo.Culture.Name <> "es-CO" Then
            listPostularValor.Add(New ItemOpcionNovelty(maternidad:=True, losTresPrimerosDias:=True, ImageComboItem:=New ImageComboBoxItem("50% todos los dias", CType(9, Byte), -1)))
        End If

        If indigo.Culture.Name <> "es-CO" Then
            listPostularValor.Add(New ItemOpcionNovelty(riesgoProfesional:=True, noLiquida:=True, ImageComboItem:=New ImageComboBoxItem("No Liquida", CType(10, Byte), -1), culture:=indigo.Culture))
            listPostularValor.Add(New ItemOpcionNovelty(maternidad:=True, todosLosDias:=True, ImageComboItem:=New ImageComboBoxItem("50% Los primeros 3 dias", CType(8, Byte), -1), culture:=indigo.Culture))
        End If
        '-----------------------------------------------------------------------------------------------------------------------------------------------------------

        If indigo.Culture.Name <> "es-CO" Then
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadGeneral, Incapacidades), CType(6, Byte), -1), culture:=indigo.Culture))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=False, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadMaternidad, Incapacidades), CType(3, Byte), -1), culture:=indigo.Culture))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadRiesgos, Incapacidades), CType(4, Byte), -1), culture:=indigo.Culture))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=False, riesgoProfesional:=False, maternidad:=True, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadPaternidad, Incapacidades), CType(5, Byte), -1), culture:=indigo.Culture))
        Else
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=False, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadMaternidad, Incapacidades), CType(3, Byte), -1)))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadRiesgos, Incapacidades), CType(4, Byte), -1)))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=False, riesgoProfesional:=False, maternidad:=True, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadPaternidad, Incapacidades), CType(5, Byte), -1)))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadAmbulatoria, Incapacidades), CType(1, Byte), -1)))
            listIncapacidad.Add(New ItemOpcionNovelty(menorTres:=True, mayor2Menor91:=True, mayor90:=True, riesgoProfesional:=False, maternidad:=False, mayor180:=False, ImageComboItem:=New ImageComboBoxItem(obtenerRecurso(IncapacidadHospitalaria, Incapacidades), CType(2, Byte), -1)))
        End If
    End Sub

    ''' <summary>
    ''' subrutina para asignar los items a un source de un imagecombo que me envien como parametro
    ''' </summary>
    ''' <param name="list">Lista que se va a agregar</param>
    ''' <param name="source">Source al que se le va agregar la lista</param>
    ''' <remarks></remarks>
    Public Sub asignarItemsCombo(ByVal list As List(Of ItemOpcionNovelty), ByRef source As ImageComboBoxItemCollection)
        For Each item As ItemOpcionNovelty In list
            source.Add(item.ImageComboItem)
        Next
    End Sub

    ''' <summary>
    ''' calcula el valor de la incapacidad y los demas detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub calcularValorIncapacidad()

        Dim valorBaseDia As Decimal
        If INDteNroDaysNovelty.Text.Length > 0 AndAlso CType(INDteNroDaysNovelty.Text, Integer) > 0 Then
            'diasNovedad = CType(INDteNroDaysNovelty.Text, Integer)
        Else
            'MessageIndigo.Show(obtenerRecurso(CantidadDiasNovedad, Incapacidades), MessageType.Information, Me.Text)
            INDteNroDaysNovelty.Focus()
            Return
        End If
        INDteValueNovelty.EditValue = Nothing
        INDteEPSRecognizeValue.EditValue = Nothing
        INDtePaidPayrollValue.EditValue = Nothing
        INDtePaidEmployerValue.EditValue = Nothing
        INDteEmployerDays.EditValue = 0
        INDteEPSDays.EditValue = 0

        If FlagProrroga = True Then
            INDteLiquidationBase.EditValue = valorBase
            INDteIBCValue.EditValue = valorBase
            INDteIBCLastMonth.EditValue = valorBase
            Me.valorBase = noveltyEmployee.LiquidationBase
        End If

        Dim valorBasePatrono As Decimal = INDTeBaseLiquidationPatrono.EditValue
        valorBaseDia = Me.valorBase / 30

        If diasPatrono Is Nothing Then
            diasPatrono = 0
        End If

        If diasEPS Is Nothing Then
            diasEPS = 0
        End If

        'Evalue el tipo de incapacidad que haya seleccionado el usuario
        Select Case INDcmbInabilityClass.EditValue
            Case 4 'Riesgos Profesionales
                Select Case CalculationType
                    Case 1 'EPS(2/3) apartir del 3 dia
                        diasEPS = diasNovedad - 1
                        diasPatrono = 1
                        valorPatrono = valorBaseDia * 1
                        valorEPS = valorBaseDia * diasEPS
                        valorIncapacidad = valorBaseDia * diasNovedad
                        valorNomina = valorIncapacidad
                    Case 3 'NOMINA 100%
                        valorIncapacidad = valorBaseDia * diasNovedad
                        valorEPS = valorIncapacidad
                        valorNomina = valorIncapacidad
                        valorPatrono = 0
                        diasPatrono = 0
                        diasEPS = diasNovedad
                End Select
        End Select

        Dim FactDiv As Decimal = (2 / 3) * 100

        Dim VarRateAproximation = parameterGroup.AproximationValue

        FactDiv = Math.Round(FactDiv, 2)


        Select Case CalculationType

            Case 1 ' EPS(2/3) apartir del 3 dia
                If FlagProrroga = False Then
                    If diasNovedad >= 3 Then
                        'Se modifica de acuerdo Según decreto 2943 de 17 de diciembre 2013
                        diasPatrono = 2
                    Else
                        diasPatrono = diasNovedad
                    End If
                    diasEPS = diasNovedad - diasPatrono.Value

                    'Valido que el dia de la eps no sea menor aun dia normal de salario minimo
                    Dim basePatrono = IIf(HandleBaseLiquidationPatrono, INDTeBaseLiquidationPatrono.EditValue / 30, valorBaseDia)
                    Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                    valorPatrono = basePatrono * diasPatrono.Value

                    If (valorBaseDia / 3 * 2) < valorDiaMinimo Then
                        valorEPS = valorDiaMinimo * diasEPS.Value
                        valorNomina = valorDiaMinimo * diasNovedad
                    Else
                        valorEPS = (valorBaseDia * (FactDiv / 100)) * diasEPS.Value
                        valorNomina = valorBaseDia * diasNovedad
                    End If

                    diasEPS = diasNovedad - diasPatrono.Value
                    valorIncapacidad = Utils.RoundedValuesByRate(valorEPS.Value + valorPatrono.Value, VarRateAproximation)
                Else
                    Dim DiasAuxiliares As Integer = 0
                    Dim ValorAuxiliar As Decimal = 0
                    INDteEmployerDays.EditValue = 0
                    diasEPS = diasNovedad
                    diasPatrono = 0

                    diasEPS = diasNovedad
                    'Valido que el dia de la eps no sea menor aun dia normal de salario minimo
                    Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                    If (valorBaseDia / 3 * 2) < valorDiaMinimo Then
                        INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                        valorEPS = valorDiaMinimo * diasEPS.Value
                    Else

                        valorEPS = (valorBaseDia * (FactDiv / 100)) * diasEPS
                    End If
                    ValorAuxiliar = valorBaseDia * DiasAuxiliares
                    valorIncapacidad = Utils.RoundedValuesByRate(valorEPS.Value + ValorAuxiliar, VarRateAproximation)

                    valorNomina = valorBaseDia * diasNovedad
                End If

            Case 2 ' Patrono

                Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                If valorBaseDia < valorDiaMinimo Then
                    INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                    valorBaseDia = valorDiaMinimo
                End If

                valorIncapacidad = Utils.RoundedValuesByRate(valorBaseDia * diasNovedad, VarRateAproximation)
                valorPatrono = valorIncapacidad
                valorEPS = 0
                valorNomina = valorIncapacidad
                diasPatrono = diasNovedad
                diasEPS = 0
            Case 3 'Nomina 100% todos los dias
                Dim basePatrono = IIf(HandleBaseLiquidationPatrono, INDTeBaseLiquidationPatrono.EditValue, parameterGroup.LegalSalaryMinimum) / 30
                Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30

                If valorBaseDia < valorDiaMinimo Then
                    INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                    valorBaseDia = valorDiaMinimo
                    valorPatrono = basePatrono * diasPatrono.Value
                    valorEPS = valorDiaMinimo * diasEPS.Value
                End If

                valorIncapacidad = Utils.RoundedValuesByRate(valorBaseDia * diasNovedad, VarRateAproximation)

                valorEPS = valorIncapacidad
                valorNomina = valorIncapacidad
                valorPatrono = 0
                diasPatrono = 0
                diasEPS = diasNovedad
            Case 4 '90 dias o mas

                valorPatrono = 0
                valorEPS = (valorBaseDia / 3 * 2) * 90

                If FlagProrroga = True Then

                    Dim FlagDivido = 1
                    Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                    If Math.Round(valorBaseDia) <= Math.Round(valorDiaMinimo) Then
                        FlagDivido = 1
                    Else
                        FlagDivido = 2
                    End If

                    valorNomina = valorBaseDia * (diasNovedad + DiasIncapacidadTotal)

                    Dim DiasAl50 As Integer = 0
                    Dim DiasAlTercero As Integer = 0
                    If (diasNovedad + DiasIncapacidadTotal) > 90 Then
                        DiasAl50 = (diasNovedad + DiasIncapacidadTotal) - 90
                    End If

                    DiasAlTercero = diasNovedad - DiasAl50

                    If DiasAlTercero < 0 Then
                        DiasAlTercero = 0

                    End If

                    If DiasAl50 > diasNovedad Then
                        DiasAl50 = diasNovedad
                    End If

                    valorIncapacidad = Utils.RoundedValuesByRate((valorBaseDia * (2 / 3) * DiasAlTercero), VarRateAproximation)

                    valorIncapacidad = valorIncapacidad + Utils.RoundedValuesByRate(((valorBaseDia / FlagDivido) * DiasAl50), VarRateAproximation)

                    valorEPS = valorIncapacidad

                    If DiasAl50 + DiasAlTercero >= 30 AndAlso valorIncapacidad < parameterGroup.LegalSalaryMinimum Then
                        valorEPS = parameterGroup.LegalSalaryMinimum
                        valorIncapacidad = valorEPS
                    End If
                Else
                    valorNomina = valorBaseDia * diasNovedad
                    valorIncapacidad = Utils.RoundedValuesByRate(valorEPS + ((valorBaseDia / 2) * (diasNovedad - 90)), VarRateAproximation)
                End If
                valorNomina = valorBaseDia * diasNovedad

                diasEPS = diasNovedad
                diasPatrono = 0
            Case 5 '2/3 todos los dias
                If FlagProrroga = False Then
                    If diasNovedad >= 3 Then
                        'Se modifica de acuerdo Según decreto 2943 de 17 de diciembre 2013
                        diasPatrono = 2
                    Else
                        diasPatrono = diasNovedad
                    End If
                Else
                    diasEPS = diasNovedad
                    diasPatrono = 0
                End If

                'diasPatrono = 0
                diasEPS = diasNovedad - diasPatrono.Value
                'Valido que el dia de la eps no sea menor aun dia normal de salario minimo

                Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                Dim valorDiaMinimoPatrono = If(HandleBaseLiquidationPatrono, INDTeBaseLiquidationPatrono.EditValue / 30, valorDiaMinimo)
                Dim valorBaseDiaPatrono = If(HandleBaseLiquidationPatrono, INDTeBaseLiquidationPatrono.EditValue / 30, valorBaseDia)

                If (valorBaseDia / 3 * 2) < valorDiaMinimo Then
                    INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                    valorEPS = (valorDiaMinimo * diasEPS.Value)
                    valorPatrono = Utils.ConvertToDecimal(valorDiaMinimoPatrono) * Utils.ConvertToInt(diasPatrono.Value)
                Else
                    valorEPS = (valorBaseDia / 3 * 2) * diasEPS.Value
                    valorPatrono = (valorBaseDiaPatrono / 3 * 2) * diasPatrono.Value
                End If
                'End If

                valorIncapacidad = Utils.RoundedValuesByRate(valorEPS.Value + valorPatrono.Value, VarRateAproximation)
                valorNomina = valorBaseDia * diasNovedad
            Case 7 '180 días de Incapacidad, no se paga nada

                valorPatrono = 0
                valorEPS = (valorBaseDia / 3 * 2) * 90

                If FlagProrroga = True Then

                    Dim FlagDivido = 1
                    Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                    If Math.Round(valorBaseDia) <= Math.Round(valorDiaMinimo) Then
                        FlagDivido = 1
                    Else
                        FlagDivido = 2
                    End If

                    valorNomina = valorBaseDia * (diasNovedad + DiasIncapacidadTotal)

                    Dim DiasAl50 As Integer = 0
                    Dim DiasAlTercero As Integer = 0
                    If (diasNovedad + DiasIncapacidadTotal) >= 180 Then
                        DiasAl50 = (diasNovedad + DiasIncapacidadTotal) - 180
                    End If

                    DiasAlTercero = diasNovedad - DiasAl50

                    If DiasAlTercero < 0 Then
                        DiasAlTercero = 0
                    End If

                    If DiasAl50 > diasNovedad Then
                        DiasAl50 = diasNovedad
                    End If

                    valorIncapacidad = Utils.RoundedValuesByRate((valorBaseDia * (1 / 2) * DiasAlTercero), VarRateAproximation)

                    diasEPS = diasNovedad
                    valorEPS = valorIncapacidad
                Else
                    valorNomina = valorBaseDia * diasNovedad
                    valorIncapacidad = Utils.RoundedValuesByRate(valorEPS + ((valorBaseDia / 2) * (diasNovedad - 90)), VarRateAproximation)
                    diasEPS = diasNovedad
                End If
                valorNomina = valorBaseDia * diasNovedad



                diasPatrono = 0

            Case 8 ' 50% los primeros 3 días
                CalculateCyclePercentage(valorBaseDia)
                valorPatrono = valorIncapacidad
                valorEPS = 0
                valorNomina = valorIncapacidad
                diasPatrono = diasNovedad
                diasEPS = 0

            Case 9 ' 50% Todos los días

                Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                If valorBaseDia < valorDiaMinimo Then
                    INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                    valorBaseDia = valorDiaMinimo
                    valorPatrono = valorDiaMinimo * diasPatrono.Value
                    valorEPS = valorDiaMinimo * diasEPS.Value
                End If

                valorIncapacidad = Utils.RoundedValuesByRate((valorBaseDia * diasNovedad) / 2, VarRateAproximation)
                valorEPS = valorIncapacidad
                valorNomina = valorIncapacidad
                valorPatrono = 0
                diasPatrono = 0
                diasEPS = diasNovedad

            Case 10 ' No liquida

                Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                If valorBaseDia < valorDiaMinimo Then
                    INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                    valorBaseDia = valorDiaMinimo
                    valorPatrono = valorDiaMinimo * diasPatrono.Value
                    valorEPS = valorDiaMinimo * diasEPS.Value
                End If

                valorIncapacidad = Utils.RoundedValuesByRate(0, VarRateAproximation)
                valorNomina = valorIncapacidad
                valorEPS = valorIncapacidad
                valorPatrono = 0
                diasPatrono = 0
                diasEPS = diasNovedad

        End Select

        If INDcmbTypeNovelty.EditValue = 3 Then
            If NoveltyLicensingType = 6 Then ' Licencia por Luto
                diasPatrono = diasNovedad
                diasEPS = 0
                valorIncapacidad = valorBaseDia * diasNovedad
                valorNomina = valorIncapacidad
                valorPatrono = valorIncapacidad
            End If

            If NoveltyLicensingType = 2 Then ' Licencia No Remunerada
                diasPatrono = diasNovedad
                diasEPS = 0
                valorIncapacidad = 0
                valorNomina = 0
                valorPatrono = valorIncapacidad
            End If

            If NoveltyLicensingType = 5 Then ' Calamidad Doméstica
                diasPatrono = diasNovedad
                diasEPS = 0
                valorIncapacidad = valorBaseDia * diasNovedad
                valorNomina = valorIncapacidad
                valorPatrono = valorIncapacidad
                valorEPS = 0
            End If
        End If

        If INDcmbTypeNovelty.EditValue = 1 Then
            If INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then ' Enfermedad Profesional
                If CalculationType = 10 Then
                    Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30
                    If valorBaseDia < valorDiaMinimo Then
                        INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
                        valorBaseDia = valorDiaMinimo
                        valorPatrono = valorDiaMinimo * diasPatrono.Value
                        valorEPS = valorDiaMinimo * diasEPS.Value
                    End If

                    valorIncapacidad = Utils.RoundedValuesByRate(0, VarRateAproximation)
                    valorNomina = valorIncapacidad
                    valorEPS = valorIncapacidad
                    valorPatrono = 0
                    diasPatrono = 0
                    diasEPS = diasNovedad
                Else

                    If diasNovedad = 1 Then
                        diasPatrono = diasNovedad
                        diasEPS = 0
                    Else
                        diasPatrono = 1
                        diasEPS = diasNovedad - 1
                    End If

                    If FlagProrroga = True Then
                        diasPatrono = 0
                        diasEPS = diasNovedad
                    End If

                    valorIncapacidad = valorBaseDia * diasNovedad
                    valorNomina = valorIncapacidad
                    valorPatrono = valorBaseDia * diasPatrono
                    valorEPS = valorBaseDia * diasEPS

                End If

            End If
        End If
        If contrato.ContractType.SalaryType = IntegralSalary Then
            Await CalculateNoveltyConcept(contrato, diasNovedad)
        End If

        INDteValueNovelty.EditValue = valorIncapacidad
        INDteEPSRecognizeValue.EditValue = valorEPS
        INDtePaidPayrollValue.EditValue = valorNomina
        INDtePaidEmployerValue.EditValue = valorPatrono
        INDteEmployerDays.EditValue = diasPatrono
        INDteEPSDays.EditValue = diasEPS
    End Sub

    ''' <summary>
    ''' Metodo que realiza el calculo cuando se requiere liquidar al 50% 3 dias de incapacidad
    ''' </summary>
    ''' <param name="valorBaseDia"></param>
    Private Sub CalculateCyclePercentage(valorBaseDia As Decimal)
        Dim LastStartDate As Date?
        Dim EndDateCycle As Date
        Dim daysLiquidated As Integer = 0
        Dim valorDiaMinimo As Double = parameterGroup.LegalSalaryMinimum / 30

        If valorBaseDia < valorDiaMinimo Then
            INDteLiquidationBase.EditValue = parameterGroup.LegalSalaryMinimum
            valorBaseDia = valorDiaMinimo
        End If

        Using model As New MNovelty(MyBase.Tag)
            LastStartDate = model.GetLastCycleStartDate(employee.Id, INDdeRealDateNovelty.EditValue)
            If LastStartDate.HasValue Then
                EndDateCycle = LastStartDate.Value.AddDays(30)
            End If
            'Comparamos ya que si el dia es el inicio de un nuevo ciclo hay que guardarlo
            If EndDateCycle <= INDdeRealDateNovelty.EditValue Then
                If diasNovedad > 3 Then
                    valorIncapacidad = Utils.RoundedValuesByRate((valorBaseDia * 3) / 2, parameterGroup.AproximationValue)
                Else
                    valorIncapacidad = Utils.RoundedValuesByRate((valorBaseDia * diasNovedad) / 2, parameterGroup.AproximationValue)
                End If
                IsCycleDate = True
            Else
                ' Cálculo en memoria si no se ha llegado al límite de 3 días liquidados
                Dim inabilities = model.GetInabilitiesByDate(employee.Id, LastStartDate.Value, EndDateCycle)
                If inabilities?.Any() Then
                    daysLiquidated = inabilities.Sum(Function(x) x.Days)
                End If

                ' Si los días liquidados son menores a 3, realizar el cálculo
                If daysLiquidated < 3 Then
                    Dim diasPorLiquidar As Integer = 3 - daysLiquidated
                    Dim diasALiquidar As Integer = Math.Min(diasPorLiquidar, diasNovedad)
                    valorIncapacidad = Utils.RoundedValuesByRate((valorBaseDia * diasALiquidar) / 2, parameterGroup.AproximationValue)
                Else
                    valorIncapacidad = Utils.RoundedValuesByRate(0, parameterGroup.AproximationValue)
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Filtra los datos correspondientes para cada combo dependiendo de la cantidad de dias que le envie
    ''' </summary>
    ''' <param name="dias"></param>
    ''' <param name="lista"></param>
    ''' <param name="source"></param>
    ''' <remarks></remarks>
    Private Sub filtrarDatosCombo(dias As Integer, lista As List(Of ItemOpcionNovelty), ByRef source As ImageComboBoxItemCollection)
        source.Clear()

        If indigo.Culture.Name = "es-CO" Then
            If dias < 3 Then
                ' Días 1-2: MenorTres
                asignarItemsCombo(lista.FindAll(Function(x) x.MenorTres = True AndAlso x.Culture.Name = indigo.Culture.Name), source)
            ElseIf dias >= 180 Then
                ' Días >= 180: Mayor180
                asignarItemsCombo(lista.FindAll(Function(x) x.Mayor180 = True AndAlso x.Culture.Name = indigo.Culture.Name), source)
            ElseIf dias >= 90 Then
                ' Días 90-179: Mayor90 (Aquí se muestra Postular90Dias)
                asignarItemsCombo(lista.FindAll(Function(x) x.Mayor90 = True AndAlso x.Culture.Name = indigo.Culture.Name), source)
            ElseIf dias > 2 Then
                ' Días 3-89: Mayor2Menor91
                asignarItemsCombo(lista.FindAll(Function(x) x.Mayor2Menor91 = True AndAlso x.Culture.Name = indigo.Culture.Name), source)
            End If
        Else
            asignarItemsCombo(lista.FindAll(Function(x) x.Culture.Name = indigo.Culture.Name), source)
        End If

    End Sub

    ''' <summary>
    ''' Muestra u Oculta los controles que tiene que ver con la opcion de incapacidad
    ''' </summary>
    ''' <param name="value">Visibilidad del los controles</param>
    ''' <remarks></remarks>
    Private Sub mostrarControlesIncapacidad(value As Boolean)
        Dim visibilidad As Integer
        If (value = True) Then
            visibilidad = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            visibilidad = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        INDlyEmployerDays.Visibility = visibilidad
        INDlyEPSDays.Visibility = visibilidad
        INDlyEPSRecognizeValue.Visibility = visibilidad
        INDlyPaidPayrollValue.Visibility = visibilidad
        INDlyPaidEmployerValue.Visibility = visibilidad
        INDlyValueNovelty.Visibility = visibilidad
        INDlyAutorizationNumber.Visibility = visibilidad
    End Sub

    ''' <summary>
    ''' Funcion que retorna nothing si el objeto esta vacio o retorna un decimal redondeado con 2 fracciones
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function retornaNothingDecimal(valor As Nullable(Of Decimal)) As Nullable(Of Decimal)
        If valor Is Nothing Then
            Return valor
        Else
            Return Decimal.Round(valor.Value, 2)
        End If
    End Function

    ''' <summary>
    ''' Edita una novedad, esta funcion espande el popup y carga los datos
    ''' </summary>
    ''' <param name="novelty">Novedad a editar</param>
    ''' <remarks></remarks>
    Private Sub editarNovedad(novelty As Novelty)
        FlagProrroga = False
        CleanPopupNovedades()
        BarraBotonesPopup.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        INDbtnAddNovelty.Text = obtenerRecurso(Eresources.EditarNovedad, Incapacidades)
        Dim objetoSeleccion = CType(INDgcNoveltyPending.DefaultView.GetRow(CType(INDgcNoveltyPending.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Dim novedad As PayrollNoveltyXpo = CType(objetoSeleccion.OriginalRow, PayrollNoveltyXpo)
        If novedad.Extension = 1 Then
            FlagExtensionEdit = True
            prorrogaNovedad(novelty)
        End If
        noveltyEmployee = novelty
        Me.GetDocumentIndexed(Me.Tag & "_" & novelty.Id)
        Me.BarraBotonesPopup.SetDocuments(novelty.Id, MyBase.Tag, Me)
        asignarDatosNovedad(novelty)
        EditMode = True
    End Sub

    ''' <summary>
    ''' Agrega prorroga a una novedad, esta funcion espande el popup y carga los datos
    ''' </summary>
    ''' <param name="novelty"></param>
    ''' <remarks></remarks>
    Private Async Function prorrogaNovedad(novelty As Novelty) As Task
        Try
            CleanPopupNovedades()
            DiasIncapacidadTotal = 0
            FlagProrroga = True

            CalcularTotalDiasProrroga(novelty)

            If FlagExtensionEdit = True AndAlso novelty.Extension = 1 Then
                Dim objetoSeleccion = CType(INDgcNoveltyPending.DefaultView.GetRow(CType(INDgcNoveltyPending.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                Dim novedad As PayrollNoveltyXpo = CType(objetoSeleccion.OriginalRow, PayrollNoveltyXpo)
                Me.valorBase = novedad.LiquidationBase
                INDteEmployerDays.EditValue = 0
                INDteIBCValue.EditValue = novedad.LiquidationBase
                INDteMonthIBC.EditValue = novedad.LiquidationBase
            Else
                Me.valorBase = novelty.LiquidationBase
                noveltyEmployee = cloneNoveltyExtension(novelty)
            End If

            BarraBotonesPopup.PrepareToolbar(eAction.OnlyUpdateOrDelete)
            diasEPS = TmpNovelty.EPSDays
            diasPatrono = TmpNovelty.EmployerDays
            INDbtnAddNovelty.Text = obtenerRecurso(Eresources.ProrrogaNovedad, Incapacidades)
            noveltyEmployee.LiquidationBase = valorBase
            asignarDatosNovedad(noveltyEmployee)
            INDcmbTypeNovelty.Enabled = False
            INDteLiquidationBase.Enabled = False
            INDteLiquidatedIBCorSalary.Enabled = False
            INDteMonthIBC.Enabled = False
            INDteIBCLastMonth.Enabled = False

            INDdeRealDateNovelty.Properties.MinValue = noveltyEmployee.EndDate.AddDays(1)
            INDdeRealDateNovelty.Properties.MaxValue = noveltyEmployee.EndDate.AddDays(30)
            INDdeRealDateNovelty.DateTime = noveltyEmployee.EndDate.AddDays(1)
            INDteEmployerDays.EditValue = 0
        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Function

    ''' <summary>
    ''' Funcion la cual se encarga de cargar los datos de una novedad en los controles correspondientes
    ''' </summary>
    ''' <param name="novelty"></param>
    ''' <remarks></remarks>
    Private Sub asignarDatosNovedad(novelty As Novelty)
        EditMode = True
        INDbtnAddNovelty.Focus()
        INDbtnAddNovelty.ShowDropDown()
        INDcmbTypeNovelty.EditValue = novelty.TypeNovelty
        INDdeRealDateNovelty.DateTime = novelty.RealDate
        If novelty.LicenseClass Is Nothing Or novelty.LicenseClass <> 6 Then
            INDteNroDaysNovelty.EditValue = novelty.Days
        End If

        INDetEndDate.EditValue = novelty.EndDate.ToLongDateString()
        INDteReason.EditValue = novelty.Reason
        INDteLiquidatedIBCorSalary.EditValue = novelty.LiquidatedIBCorSalary
        CalculationType = novelty.CalculationType
        INDcmbInabilityClass.EditValue = novelty.InabilityClass
        INDcmbRiskType.EditValue = novelty.RiskType
        'Se asigna y se muestra la clase de licencia al cargarse
        If novelty.LicenseClass IsNot Nothing Then
            NoveltyLicensingType = novelty.LicenseClass
            INDcmbLicenseClass.SelectedIndex = novelty.LicenseClass - 1
        End If
        INDteEmployerDays.EditValue = novelty.EmployerDays
        INDteEPSDays.EditValue = novelty.EPSDays

        If novelty.EPSRecognizeValue IsNot Nothing Then
            INDteEPSRecognizeValue.EditValue = novelty.EPSRecognizeValue.Value
        Else
            INDteEPSRecognizeValue.EditValue = novelty.EPSRecognizeValue
        End If
        If novelty.PaidPayrollValue IsNot Nothing Then
            INDtePaidPayrollValue.EditValue = novelty.PaidPayrollValue.Value
        Else
            INDtePaidPayrollValue.EditValue = novelty.PaidPayrollValue
        End If
        If novelty.PaidEmployerValue IsNot Nothing Then
            INDtePaidEmployerValue.EditValue = novelty.PaidEmployerValue.Value
        Else
            INDtePaidEmployerValue.EditValue = novelty.PaidEmployerValue
        End If
        If novelty.Value IsNot Nothing Then
            INDteValueNovelty.EditValue = novelty.Value.Value
        Else
            INDteValueNovelty.EditValue = novelty.Value
        End If
        INDteAutorizationNumber.EditValue = novelty.AutorizationNumber
        Me.BarraBotonesPopup.StatusRecord = novelty.NoveltyLiquidate

        If indigo.IndigoCompanyType = "2" Then
            INDTxtResolutionNumber.EditValue = novelty.ResolutionNumber
            INDDeResolutionDate.EditValue = novelty.ResolutionDate
        End If

        INDteLiquidationBase.EditValue = novelty.LiquidationBase
        INDTeBaseLiquidationPatrono.EditValue = novelty.LiquidationBasePatrono
    End Sub

    Private Function cloneNoveltyExtension(novelty As Novelty) As Novelty
        Dim newNovelty As New Novelty()
        newNovelty.Consecutive = novelty.Consecutive
        newNovelty.GroupId = novelty.GroupId
        newNovelty.EmployeeId = employee.Id
        newNovelty.TypeNovelty = novelty.TypeNovelty
        newNovelty.Extension = 1
        newNovelty.IBC = novelty.IBC
        newNovelty.EmployeeBaseSalary = novelty.EmployeeBaseSalary
        newNovelty.RealDate = novelty.RealDate
        newNovelty.Days = novelty.Days
        newNovelty.EndDate = novelty.EndDate
        newNovelty.LiquidatedIBCorSalary = novelty.LiquidatedIBCorSalary
        newNovelty.Reason = novelty.Reason
        newNovelty.LiquidationBase = novelty.LiquidationBase
        newNovelty.LiquidationBasePatrono = novelty.LiquidationBasePatrono
        newNovelty.EPSDays = novelty.EPSDays
        newNovelty.EmployerDays = 0
        newNovelty.LicenseClass = novelty.LicenseClass
        newNovelty.InabilityClass = novelty.InabilityClass
        newNovelty.RiskType = novelty.RiskType
        newNovelty.NoveltyLiquidate = novelty.NoveltyLiquidate
        newNovelty.CalculationType = novelty.CalculationType
        newNovelty.EPSRecognizeValue = novelty.EPSRecognizeValue
        newNovelty.PaidPayrollValue = novelty.PaidPayrollValue
        newNovelty.PaidEmployerValue = novelty.PaidEmployerValue
        newNovelty.Value = novelty.Value
        newNovelty.AutorizationNumber = novelty.AutorizationNumber
        newNovelty.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
        Return newNovelty
    End Function

    ''' <summary>
    ''' habilita o desabilita los controles para cuando se edita una novedad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub habilitarControlesEditarNovedad(habilita As Boolean)
        INDcmbTypeNovelty.Enabled = habilita
        INDdeRealDateNovelty.Enabled = habilita
        INDteNroDaysNovelty.Enabled = habilita
        If contrato.ContractType.SalaryType = IntegralSalary Then
            IntegralSalaryNoveltyValue = 0
            INDlciIntegralSalaryInabilityValue.ShowLayout()
        Else
            INDlciIntegralSalaryInabilityValue.HideLayout()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene un la descripcion del codigo de la novedad
    ''' </summary>
    ''' <param name="typeNovelty">tipo de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetStringTypeNovelty(typeNovelty As Integer) As String
        Dim StrTypeNovelty As String = ""
        Select Case typeNovelty
            Case 1
                StrTypeNovelty = "Incapacidad"
            Case 2
                StrTypeNovelty = "Sanción"
            Case 3
                StrTypeNovelty = "Licencia"
        End Select
        Return StrTypeNovelty
    End Function

    ''' <summary>
    ''' Funcion la cual se encarga de agergar una prorroga
    ''' </summary>
    ''' <param name="control"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadExtensionNovelty(control As GridControl) As Task
        'Dim novedad As PayrollNoveltyXpo = CType(control.DefaultView.GetRow(CType(control.DefaultView, GridView).FocusedRowHandle()), PayrollNoveltyXpo)
        Dim objetoSeleccion = CType(control.DefaultView.GetRow(CType(control.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Dim novedad As PayrollNoveltyXpo = CType(objetoSeleccion.OriginalRow, PayrollNoveltyXpo)
        Dim novedadEntity As Novelty
        Using model As New MNovelty(MyBase.Tag)
            novedadEntity = Await model.GetUltimateNoveltyConsecutiveAsync(novedad.Consecutive)
        End Using
        prorrogaNovedad(novedadEntity)
    End Function

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        changeNumericFormatByCurrency(numberFormat, PopupContainerMain.Controls)

        INDColBaseSalary = Window.Utils.FormatGrid(INDColBaseSalary, _currencyAbbreviation)
        INDcolValue = Window.Utils.FormatGrid(INDcolValue, _currencyAbbreviation)
        INDColBaseLiquidated = Window.Utils.FormatGrid(INDColBaseLiquidated, _currencyAbbreviation)
        INDcolInabilityValue = Window.Utils.FormatGrid(INDcolInabilityValue, _currencyAbbreviation)
        INDcolSactionsLiquidationBase = Window.Utils.FormatGrid(INDcolSactionsLiquidationBase, _currencyAbbreviation)
        INDcolLicenseLiquidationBase = Window.Utils.FormatGrid(INDcolLicenseLiquidationBase, _currencyAbbreviation)
    End Sub

#End Region

#Region "Events"

#Region "Shown"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        noveltyEmployee = Nothing
        diasEPS = Nothing
        diasPatrono = Nothing
        valorBase = Nothing
        valorIncapacidad = Nothing
        valorEPS = Nothing
        valorNomina = Nothing
        valorPatrono = Nothing
        contrato = Nothing
        listIncapacidad = Nothing
        listPostularValor = Nothing
        Presenter = Nothing
        employee = Nothing
        parameterGroup = Nothing
        PathFunctionalDefinitions = Nothing
        ExistDefinitionFront = Nothing
        dtFieldsCustomizables = Nothing
        record = Nothing
        FlagProrroga = Nothing
        diasNovedad = Nothing
        ListHolidays = Nothing
        EditMode = Nothing
        IsCycleDate = False
        DiasIncapacidadTotal = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al pintar el form por primera vez
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmNovelty_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteEmployeeId.Focus()
    End Sub

#End Region

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Dim noveltyTmp As Novelty
        Using model As New MNovelty(MyBase.Tag)
            noveltyTmp = Await model.GetNoveltyById(Me.IdEntity.Trim())
        End Using
        If Me.employee IsNot Nothing AndAlso Me.employee.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), Botones.SiNo, MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes)) = System.Windows.Forms.DialogResult.Yes Then
                Me.INDbteEmployeeId.Text = noveltyTmp.Employee.ThirdParty.Nit
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteEmployeeId.Text = noveltyTmp.Employee.ThirdParty.Nit
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta cuando presionan click en el boton de buscar empleado
    ''' </summary>
    ''' <param name="sender">Objeto que ejecuta el evento</param>
    ''' <param name="e">argumentos</param>
    ''' <remarks></remarks>
    Private Sub INDbteEmployeeId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteEmployeeId.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se carga por completo la barra botones del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_Load(sender As Object, e As EventArgs)
        BarraBotonesPopup.ActualizarPermisosBarra(Str(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando presionan una tecla sobre el control de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteEmployeeId_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteEmployeeId.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then

            'Se valida que el usuario haya llenado el campo de identificación
            If String.IsNullOrEmpty(INDbteEmployeeId.Text) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe escribir una identificación"
                INDbteEmployeeId.Focus()
                Exit Sub
            End If

            LoadControls()
            BarraBotonesPopup.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            BarraBotonesPopup.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
        'End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar el formulario de incapacidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInability_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'
        Me._funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollBank.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PInability(Me)
        _payrollSettings = Presenter.LoadPayrollSettings()
        SetCurrencyFormat(_payrollSettings.CurrencyId.Abbreviation)
        LoadStatus()
        Me.BarraBotonesPopup.StatusRecordVisible = True
        Deshacer()
        IndigoGridControl1.SetHoldSize(INDgcNoveltyPending, False)
        IndigoGridControl1.SetHoldSize(INDgcInability, False)
        IndigoGridControl1.SetHoldSize(INDgcSanctions, False)
        IndigoGridControl1.SetHoldSize(INDgcLicence, False)

        cargarListasCombo()

        BarraBotones.Minimizar(True)
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True

        If indigo.IndigoCompanyType = "2" Then
            INDlyResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyResolutionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDColResolutionDatePreview.Visible = True
            INDColResolutionNumberPreview.Visible = True
            INDColInabilityResolutionDate.Visible = True
            INDColInabilityResolutionNumber.Visible = True
            INDColSanctionResolutionDate.Visible = True
            INDColSanctionResolutionNumber.Visible = True
            INDColLicensesResolutionDate.Visible = True
            INDColLicensesResolutionNumber.Visible = True
        Else
            INDColResolutionDatePreview.Visible = False
            INDColResolutionNumberPreview.Visible = False
            INDColInabilityResolutionDate.Visible = False
            INDColInabilityResolutionNumber.Visible = False
            INDColSanctionResolutionDate.Visible = False
            INDColSanctionResolutionNumber.Visible = False
            INDColLicensesResolutionDate.Visible = False
            INDColLicensesResolutionNumber.Visible = False
        End If
    End Sub

    'Private Sub SetInitialValues()
    '    If _payrollSettings?.HandlesLiquidationFirstTwoDays Then
    '        INDLciBaseLiquidationPatrono.ShowLayout()
    '    End If
    'End Sub

    ''' <summary>
    ''' Evento el cual se dispara cuando el popup obtiene el foco
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerMain_GotFocus(sender As Object, args As EventArgs) Handles PopupContainerMain.GotFocus
        INDcmbTypeNovelty.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_ClickDeshacer() Handles BarraBotonesPopup.ClickDeshacer
        INDTeBaseLiquidationPatrono.EditValue = 0
        CleanPopupNovedades()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando dan click sobre el boton nuevo0
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_ClickNuevo() Handles BarraBotonesPopup.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, employee.Id, 0, employee.Id, Me.BarraBotones.OperatingUnit)
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se va a visualizar el texto de una columna
    ''' se utiliza para remplazar el codigo que viene en la base de datos por el texto que debe ser
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEdit1_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles RepositoryItemTextEdit1.CustomDisplayText
        e.DisplayText = FrmNovelty.GetStringTypeNovelty(e.Value)
    End Sub


    ''' <summary>
    ''' Evento que se dispara cuando el tipo de la novedad cambia su valor
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos</param>
    ''' <remarks></remarks>
    Private Sub INDcmbTypeNovelty_EditValueChanged(sender As Object, e As EventArgs) Handles INDcmbTypeNovelty.EditValueChanged
        Dim typeNovelty As ImageComboBoxEdit = CType(sender, ImageComboBoxEdit)
        If typeNovelty.EditValue IsNot Nothing Then
            INDdeRealDateNovelty.Enabled = True
            INDteNroDaysNovelty.Enabled = True
            INDteReason.Enabled = True
            INDteLiquidatedIBCorSalary.Enabled = True
        End If
        'Limpio los controles a excepcion de el tipo de novedad
        CleanControlsPopupNovedades(False)
        'Oculto las opciones que solo funcionan para la opcion de incapacidad
        mostrarControlesIncapacidad(False)
        'Evaluo la opcion que a elegido el usuario
        If typeNovelty.EditValue = 1 Then ' Incapacidad
            'Muestro las opciones que solo funcionan para la opcion de incapacidad
            mostrarControlesIncapacidad(True)
            INDlyCalculationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyInabilityClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyLicenseClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDcmbRiskType.EditValue = Nothing
            NoveltyLicensingType = Nothing
            'Se Muestra siempre y cuando el salario es integral
            If contrato.ContractType.SalaryType = IntegralSalary Then
                IntegralSalaryNoveltyValue = 0
                INDlciIntegralSalaryInabilityValue.ShowLayout()
            Else
                INDlciIntegralSalaryInabilityValue.HideLayout()
            End If
        ElseIf typeNovelty.EditValue = 2 Then 'Sancion
            INDlyCalculationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyInabilityClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyLicenseClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDcmbCalculationType.EditValue = Nothing
            INDcmbInabilityClass.EditValue = Nothing
            INDcmbRiskType.EditValue = Nothing
            NoveltyLicensingType = Nothing
        Else ' 3 Licencia
            INDlyLicenseClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyCalculationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyInabilityClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDcmbCalculationType.EditValue = Nothing
            INDcmbRiskType.EditValue = Nothing
            NoveltyLicensingType = Nothing

            'Ocultamos el campo si es Colombia
            If Me.indigo.Culture.Name <> "es-CO" Then
                INDlciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            Else
                INDlciConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            INDSlConcept.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuano la cambia la cantidad de dias de la novedad
    ''' </summary>
    ''' <param name="sender">Objeto que dispara el evento</param>
    ''' <param name="e">Argumentos</param>
    ''' <remarks></remarks>
    Private Async Sub INDteNroDaysNovelty_EditValueChanged(sender As Object, e As EventArgs) Handles INDteNroDaysNovelty.EditValueChanged, INDdeRealDateNovelty.EditValueChanged

        If Not String.IsNullOrEmpty(INDdeRealDateNovelty.EditValue?.ToString()) Then
            INDlyLiquidatedIBCorSalary.ShowLayout
        End If

        If INDteNroDaysNovelty.Text.Length > 0 AndAlso CType(INDteNroDaysNovelty.Text, Integer) > 0 Then

            If TmpNovelty IsNot Nothing AndAlso TmpNovelty.Id > 0 Then
                AsyncLoader(True)
                Await CalcularTotalDiasProrroga(TmpNovelty)
                AsyncLoader(False)
            End If

            If String.IsNullOrEmpty(INDteNroDaysNovelty.EditValue) Then
                INDteNroDaysNovelty.EditValue = 0
            End If

            Dim endDateTmp = CType(INDdeRealDateNovelty.EditValue, Date).AddDays(CType(INDteNroDaysNovelty.EditValue, Integer) - 1)
            If endDateTmp > contrato.ContractEndingDate Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeFechaFinSuperiorContrato, Incapacidades)
                INDteNroDaysNovelty.EditValue = 0
                INDetEndDate.Text = ""
                Exit Sub
            End If
            INDetEndDate.Text = endDateTmp.ToLongDateString()
            Dim numero As Integer = If(String.IsNullOrEmpty(INDteNroDaysNovelty.Text), 0, CType(INDteNroDaysNovelty.Text, Integer))

            filtrarDatosCombo(numero, listIncapacidad, INDcmbInabilityClass.Properties.Items)

            If FlagProrroga = True Then
                filtrarDatosCombo((DiasIncapacidadTotal + numero), listPostularValor, INDcmbCalculationType.Properties.Items)
            Else
                filtrarDatosCombo(numero, listPostularValor, INDcmbCalculationType.Properties.Items)
            End If

            CalcularDiasIncapacidad()

        Else
            INDetEndDate.Text = String.Empty
        End If
        INDcmbCalculationType.EditValue = Nothing
        INDcmbInabilityClass.EditValue = Nothing
        INDcmbRiskType.EditValue = Nothing
        'El día de la familia se asigna automaticamente como 1, en los demás casos se limpia la variable
        If NoveltyLicensingType <> eLicenseClass.DiaFamilia Then
            NoveltyLicensingType = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia la clase de incapacidad seleccionada
    ''' </summary>
    ''' <param name="sender">Objeto que ejecuta la accion</param>
    ''' <param name="e">Argumentos</param>
    ''' <remarks></remarks>
    Private Sub INDcmbInabilityClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDcmbInabilityClass.EditValueChanged
        INDcmbCalculationType.Properties.Items.Clear()
        INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Select Case INDcmbInabilityClass.EditValue
            Case 4 ' Opcion Riesgos Profesionales
                asignarItemsCombo(listPostularValor.FindAll(Function(x) (x.RiesgosProfesionales = True OrElse x.NoLiquida) AndAlso x.Culture.Name = indigo.Culture.Name), INDcmbCalculationType.Properties.Items)

                INDlyRiskType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Case 3, 5 'Licencia Maternidad. 'Licencia Paternidad
                asignarItemsCombo(listPostularValor.FindAll(Function(x) (x.LicenciaMaternidad And x.Culture.Name = "es-CO") OrElse (x.LicenciaMaternidad AndAlso x.Culture.Name = "es-CR" And x.LosTresPrimerosDias)), INDcmbCalculationType.Properties.Items)

            Case Else
                If INDteNroDaysNovelty.Text.Length AndAlso CType(INDteNroDaysNovelty.Text.Length, Integer) Then
                    If FlagProrroga = True Then
                        filtrarDatosCombo(CType(INDteNroDaysNovelty.Text, Integer) + DiasIncapacidadTotal, listPostularValor, INDcmbCalculationType.Properties.Items)
                    Else
                        filtrarDatosCombo(CType(INDteNroDaysNovelty.Text, Integer), listPostularValor, INDcmbCalculationType.Properties.Items)
                    End If
                End If
        End Select
        CalcularDiasIncapacidad()
        calcularValorIncapacidad()
        ShowHideBaseLiquidationPatrono()
    End Sub

    ''' <summary>
    ''' Muestra u oculta el control de base liquidacion patrono
    ''' </summary>
    Private Sub ShowHideBaseLiquidationPatrono()
        If HandleBaseLiquidationPatrono Then
            INDLciBaseLiquidationPatrono.ShowLayout()
        Else
            INDLciBaseLiquidationPatrono.HideLayout()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga la barra botones
    ''' </summary>
    ''' <param name="sender">Objeto que ejecuto el evento</param>
    ''' <param name="e">Argumentos del objeto</param>
    ''' <remarks></remarks>
    Private Sub CtrBarraBotones1_Load(sender As Object, e As EventArgs) Handles BarraBotonesPopup.Load
        BarraBotonesPopup.ActualizarPermisosBarra(CType(PopupContainerMain.Tag, String))
        BarraBotonesPopup.PrepareToolbar(eAction.OnlySave)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia el valor del control tipo de calculo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDcmbCalculationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDcmbCalculationType.EditValueChanged
        calcularValorIncapacidad()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotonesPopup_ClickGuardar() Handles BarraBotonesPopup.ClickGuardar, BarraBotonesPopup.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' evento el cual se ejecuta al disparar el caption de la columna acciones, el caption lo dejamos como editar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemButtonEdit1_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs)
        e.DisplayText = obtenerRecurso(Eresources.EditarRegistro)
    End Sub

    ''' <summary>
    ''' Se ejecuta al tratar de dibujar el text del repositorio de liquidado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemTextEditLiquidate_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles RepositoryItemTextEditLiquidate.CustomDisplayText
        If e.Value = True Then
            e.DisplayText = obtenerRecurso(Si)
        Else
            e.DisplayText = obtenerRecurso(No)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta dan click sobre el boton de editar una novedad que esta pendiente por liquidar
    ''' </summary>
    ''' <param name="sender">Objeto que ejecuta el evento</param>
    ''' <param name="e">Evento</param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnEdit_Click(sender As Object, e As EventArgs) Handles INDbtnEdit.Click
        Dim objetoSeleccion = CType(INDgcNoveltyPending.DefaultView.GetRow(CType(INDgcNoveltyPending.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Dim novedad As PayrollNoveltyXpo = CType(objetoSeleccion.OriginalRow, PayrollNoveltyXpo)
        'Dim novedad As PayrollNoveltyXpo = CType(INDgcNoveltyPending.DefaultView.GetRow(CType(INDgcNoveltyPending.DefaultView, GridView).FocusedRowHandle()), PayrollNoveltyXpo)

        Dim novedadEntity As Novelty
        Using model As New MNovelty(MyBase.Tag)
            novedadEntity = Await model.GetUltimateNoveltyConsecutiveAsync(novedad.Consecutive)
        End Using

        If novedadEntity.Id <> novedad.Id Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(NoPuedeAccionPorUltimaProrroga, Incapacidades), INDbtnDelete.Text)
            Return
        End If
        editarNovedad(novedadEntity)

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al presionar click sobre el boton eliminar
    ''' </summary>
    ''' <param name="sender">objeto que ejecuta el evento</param>
    ''' <param name="e">Evento</param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnDelete_Click(sender As Object, e As EventArgs) Handles INDbtnDelete.Click
        Dim objetoSeleccion = CType(INDgcNoveltyPending.DefaultView.GetRow(CType(INDgcNoveltyPending.DefaultView, GridView).FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Dim novedad As PayrollNoveltyXpo = CType(objetoSeleccion.OriginalRow, PayrollNoveltyXpo)
        Dim novedadEntity As Novelty
        Using model As New MNovelty(MyBase.Tag)
            novedadEntity = Await model.GetUltimateNoveltyConsecutiveAsync(novedad.Consecutive)
        End Using
        If novedadEntity.Id <> novedad.Id Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(NoPuedeAccionPorUltimaProrroga, Incapacidades), INDbtnDelete.Text)
            Return
        End If
        noveltyEmployee = novedadEntity


        Eliminar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando quieren agregarle una prorroga una novedad que falta liquidar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnExtensionNovelty_Click(sender As Object, e As EventArgs) Handles INDbtnExtensionNovelty.Click
        Await LoadExtensionNovelty(INDgcNoveltyPending)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al mostrar el texto de repositorio de prorroga
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepositoryTextEditExtension_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles INDRepositoryTextEditExtension.CustomDisplayText, INDRepositoryTextEditInabilityClase.CustomDisplayText
        If e.Value = 0 Then
            e.DisplayText = obtenerRecurso(Eresources.Inicial, Incapacidades)
        Else
            e.DisplayText = obtenerRecurso(Eresources.Prorroga, Incapacidades)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al editar una sancion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub RepositoryItemButtonEdit2_Click(sender As Object, e As EventArgs) Handles INDRepositoryButtonEditSactions.Click
        Await LoadExtensionNovelty(INDgcSanctions)
    End Sub

    ''' <summary>
    ''' Evento para escribir un texto especifico dependiendo de la columna que lo use
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvSactions_CustomDrawCell(sender As Object, e As Views.Base.RowCellCustomDrawEventArgs) Handles INDgvSactions.CustomDrawCell, INDgvInability.CustomDrawCell, INDgvLicense.CustomDrawCell
        If e.Column.Name = INDcolSactionsAction.Name Or e.Column.Name = INDcolInabilityAction.Name Or e.Column.Name = INDcolActionLicense.Name Then
            e.DisplayText = obtenerRecurso(Eresources.Prorroga, Incapacidades)
        ElseIf e.Column.Name = INDcolInabilityClass.Name Then
            If e.CellValue IsNot Nothing Then
                If String.IsNullOrEmpty(e.CellValue.ToString()) Then
                    Return
                End If
                Select Case CType(e.CellValue, Byte)
                    Case 1
                        e.DisplayText = obtenerRecurso(IncapacidadAmbulatoria, Incapacidades)
                    Case 2
                        e.DisplayText = obtenerRecurso(IncapacidadHospitalaria, Incapacidades)
                    Case 3
                        e.DisplayText = obtenerRecurso(IncapacidadMaternidad, Incapacidades)
                    Case 4
                        e.DisplayText = obtenerRecurso(IncapacidadRiesgos, Incapacidades)
                End Select
            End If
        ElseIf e.Column.Name = INDcolLicenseClass.Name Then
            If e.CellValue IsNot Nothing Then
                If String.IsNullOrEmpty(e.CellValue.ToString()) Then
                    Return
                End If
                e.DisplayText = INDcmbLicenseClass.Properties.Items(e.CellValue - 1).Description
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al editar una incapacidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDRepositoryButtonEditInability_Click(sender As Object, e As EventArgs) Handles INDRepositoryButtonEditInability.Click
        Await LoadExtensionNovelty(INDgcInability)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al editar una licencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDRepositoryButtonEditLicense_Click(sender As Object, e As EventArgs) Handles INDRepositoryButtonEditLicense.Click
        Await LoadExtensionNovelty(INDgcLicence)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor de laxaja de texto de meses ibc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDteMonthIBC_EditValueChanged(sender As Object, e As EventArgs) Handles INDteMonthIBC.EditValueChanged
        If INDteMonthIBC.Text.Length = 0 Then
            Return
        End If
        Dim calculatedValue As Decimal = 0
        If FlagProrroga = False Then
            Using model As New MNovelty(MyBase.Tag)
                calculatedValue = Await model.GetAverageIBCLiquidationLastMonth(contrato.InitialContractNumber, CType(INDteMonthIBC.Text, Integer))
            End Using
        End If
        valorBase = ValidateIBCsalaryIntegral(calculatedValue)
        INDteLiquidationBase.EditValue = valorBase

        ' Recalcular para Licencia por Luto
        If NoveltyLicensingType = 6 Then
            calcularValorIncapacidad()
        End If
    End Sub



    ''' <summary>
    ''' Evento que se ejecuta al perder el foco la fecha real de la novedad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeRealDateNovelty_Leave(sender As Object, e As EventArgs) Handles INDdeRealDateNovelty.Leave
        If INDdeRealDateNovelty.EditValue IsNot Nothing AndAlso INDdeRealDateNovelty.EditValue < contrato.JobBondingDate Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(NoPuedeFechaInferiorContrato, Incapacidades), CType(INDdeRealDateNovelty.EditValue, Date).ToShortDateString(), contrato.JobBondingDate.ToShortDateString())
        ElseIf INDdeRealDateNovelty.EditValue IsNot Nothing AndAlso INDdeRealDateNovelty.EditValue > contrato.ContractEndingDate Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(NoPuedeFechaSuperiorContrato, Incapacidades), CType(INDdeRealDateNovelty.EditValue, Date).ToShortDateString(), contrato.ContractEndingDate.ToShortDateString())
        End If
    End Sub

    ''' <summary>
    ''' Funcion la cual se encarga de escribir si la novedad esta como liquidada o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepositoryTextEditNoveltyLiquidate_CustomDisplayText(sender As Object, e As CustomDisplayTextEventArgs) Handles INDRepositoryTextEditNoveltyLiquidate.CustomDisplayText
        If e.Value = True Then
            e.DisplayText = obtenerRecurso(Si)
        Else
            e.DisplayText = obtenerRecurso(No)
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotonesPopup_ClickEliminar() Handles BarraBotonesPopup.ClickEliminar
        Eliminar()
    End Sub

#End Region

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(Me.Tag)

    End Sub

    Private Sub INDcmbLicenseClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDcmbLicenseClass.EditValueChanged
        LicenseClassCBG = CType(sender, ImageComboBoxEdit)

        'Definimos la constante del día de la familia
        Dim FamilyDay As Byte = 1


        INDlyLiquidatedIBCorSalary.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always


        If LicenseClassValue = eLicenseClass.LicenciaLuto Then
            INDlyValueNovelty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyValueNovelty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        'Si la novedad es día de la familia, el Nro Días es 1 y no se puede editar
        If LicenseClassValue = eLicenseClass.DiaFamilia Then
            INDteNroDaysNovelty.Properties.ReadOnly = True
            INDteNroDaysNovelty.EditValue = FamilyDay
        Else
            INDteNroDaysNovelty.Properties.ReadOnly = False
        End If

        Using msearch As New MBusqueda()
            ListLicensingConceptsNoveltyXpo = msearch.ConsultarEntidades(eDataSource.ListLicensingConceptsNovelty, NoveltyLicensingType)
        End Using
        INDSlConcept.Enabled = True

        CalcularDiasIncapacidad()

    End Sub

    Private Sub INDteIBCValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDteIBCValue.EditValueChanged
        If INDteIBCValue.Text.Length = 0 Then
            Return
        End If
        valorBase = INDteIBCValue.EditValue
        INDteLiquidationBase.EditValue = valorBase
    End Sub

    Private Async Sub INDteLiquidatedIBCorSalary_EditValueChanged(sender As Object, e As EventArgs) Handles INDteLiquidatedIBCorSalary.EditValueChanged
        Dim ConsultValorBase As Decimal

        INDteMonthIBC.Enabled = True
        INDteIBCValue.Enabled = True
        INDteLiquidatedIBCorSalary.Enabled = True

        If FlagProrroga = False Then

            If INDteLiquidatedIBCorSalary.EditValue = 1 Then 'IBC
                INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyIBCValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf INDteLiquidatedIBCorSalary.EditValue = 2 Then 'Sueldo
                valorBase = contrato.BasicSalary
                INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyIBCValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDteMonthIBC.Text = Nothing
                INDteLiquidationBase.EditValue = valorBase
            ElseIf INDteLiquidatedIBCorSalary.EditValue = 3 Then ' IBC Promedio
                INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDteMonthIBC.Text = Nothing

                Using model As New MNovelty(MyBase.Tag)
                    Dim Year As Integer
                    Dim Month As Integer

                    Dim DateSearch = DateAdd(DateInterval.Month, -1, INDdeRealDateNovelty.EditValue)

                    Year = DateSearch.Year
                    Month = DateSearch.Month

                    ConsultValorBase = Await Presenter.ValueIBCMonth(employee.Id, Year, Month)
                    valorBase = ConsultValorBase
                End Using

                If ConsultValorBase <= 0 Then
                    INDlyIBCValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyIBCValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                INDteLiquidationBase.EditValue = valorBase

            End If
        Else

            INDteIBCValue.Enabled = False
            INDteMonthIBC.Enabled = False
            INDteLiquidatedIBCorSalary.Enabled = False
            INDteLiquidatedIBCorSalary.EditValue = noveltyEmployee.LiquidatedIBCorSalary
            INDteMonthIBC.EditValue = noveltyEmployee.LiquidationBase
            INDteIBCValue.EditValue = noveltyEmployee.LiquidationBase

            If INDteLiquidatedIBCorSalary.EditValue = 1 Then 'IBC
                INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyIBCValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf INDteLiquidatedIBCorSalary.EditValue = 2 Then 'Sueldo
                INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyIBCValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf INDteLiquidatedIBCorSalary.EditValue = 3 Then ' IBC Promedio
                INDlyMonthIBC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            End If
        End If
        If INDcmbTypeNovelty.EditValue IsNot Nothing AndAlso INDcmbTypeNovelty.EditValue = 1 Then
            calcularValorIncapacidad()
        End If

        If NoveltyLicensingType = 6 Then
            calcularValorIncapacidad()
        End If
    End Sub

    Private Async Function CalcularDiasIncapacidad() As Task
        Try

            ' Solo Licencia Maternidad (3) y Licencia por Luto (6) usan días hábiles
            ' Licencia de Paternidad (INDcmbInabilityClass = 5) usa días calendario
            If NoveltyLicensingType = 3 Or NoveltyLicensingType = 6 Then
                Dim endDate As Date
                Dim index As Integer = 1
                Dim enjoyDays As Integer = 0 ' Variable local para contar días calendario
                endDate = INDdeRealDateNovelty.EditValue.AddDays(-1)
                Dim RealDate As Date = INDdeRealDateNovelty.EditValue
                Using model As New MNovelty(MyBase.Tag)

                    ListHolidays = Await model.ListHolidayBetweenDate(INDdeRealDateNovelty.EditValue, RealDate.AddMonths(6))

                    While index <= INDteNroDaysNovelty.EditValue 'Calculo la fecha final de vacaciones
                        endDate = endDate.AddDays(1)
                        If Await model.IsValidDay(ListHolidays, contrato.Group.PayrollParameter.SaturdayBusinessDay, contrato.Group.PayrollParameter.SundayBusinessDay, endDate) Then ' Si no es un festivo ni un domingo aumenta el indice
                            index += 1
                        Else
                            If INDcmbInabilityClass.EditValue IsNot Nothing AndAlso INDcmbInabilityClass.EditValue = 5 Then ''Licencia paternidad
                                index += 1
                            End If
                        End If
                        enjoyDays += 1

                    End While
                End Using
                diasNovedad = enjoyDays

                If diasNovedad > 0 Then
                    INDetEndDate.EditValue = CDate(DateAdd(DateInterval.Day, enjoyDays - 1, INDdeRealDateNovelty.EditValue))
                End If

            Else
                If INDdeRealDateNovelty.EditValue IsNot Nothing Then
                    diasNovedad = INDteNroDaysNovelty.EditValue
                End If

                If diasNovedad > 0 Then
                    INDetEndDate.EditValue = CDate(DateAdd(DateInterval.Day, diasNovedad - 1, INDdeRealDateNovelty.EditValue))
                End If

            End If

            If noveltyEmployee.Id > 0 And noveltyEmployee.LicenseClass = 6 And NoveltyLicensingType = noveltyEmployee.LicenseClass Then
                INDetEndDate.EditValue = noveltyEmployee.EndDate
            End If


            calcularValorIncapacidad()

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message.ToString()
        End Try


    End Function

    Private Sub INDcmbRiskType_EditValueChanged(sender As Object, e As EventArgs) Handles INDcmbRiskType.EditValueChanged
        CalcularDiasIncapacidad()
    End Sub

    Private Async Function CalcularTotalDiasProrroga(novelty As Novelty) As Task
        Using model As New MNovelty(MyBase.Tag)
            AsyncLoader(True)
            Dim ListNoveltyConsecutive = Await model.GetListNoveltyByConsecutive(novelty.Consecutive)
            AsyncLoader(False)
            If ListNoveltyConsecutive IsNot Nothing And ListNoveltyConsecutive.Count > 0 Then
                DiasIncapacidadTotal = ListNoveltyConsecutive.Sum(Function(x) x.Days)
                TmpNovelty = ListNoveltyConsecutive.Where(Function(x) x.Extension = 0).FirstOrDefault()
            End If
        End Using
    End Function

    Private Async Function CalculateTwoFirstDaysAsync() As Task
        If _payrollSettings IsNot Nothing AndAlso _payrollSettings.HandlesLiquidationFirstTwoDays Then
            Using model As New MNovelty(Me.Tag)
                Dim res = Await model.CalculateTwoFirstDaysAsync(employee.Id)
                If res?.StateResult = True Then
                    Dim conceptValue = res.ObjectEmbbeded
                    INDTeBaseLiquidationPatrono.EditValue = conceptValue
                Else
                    Mensaje(EeventViewerImages.Advertencia) = res.Message
                End If
            End Using
        End If
    End Function
    ''' <summary>
    ''' Funcion que trae el valor del concepto de incapacidad para salarios integrales 
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="noveltyDays"></param>
    ''' <returns></returns>
    Private Async Function CalculateNoveltyConcept(contract As Domain.Payroll.Entities.Contract, noveltyDays As Integer) As Task

        Using model As New MNovelty(Me.Tag)
            Dim res = Await model.CalculateNoveltyConcept(contract, noveltyDays)
            If res?.StateResult = True Then
                Dim conceptValue = res.ObjectEmbbeded
                IntegralSalaryNoveltyValue = conceptValue
            Else
                IntegralSalaryNoveltyValue = 0
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Function


    ''' <summary>
    ''' Valida que el salario no supere el tope de 25 smmlv
    ''' </summary>
    ''' <param name="ibcInicial"></param>
    ''' <returns></returns>
    Private Function ValidateIBCsalaryIntegral(ibcInicial As Decimal)
        Dim smmlv As Decimal = parameterGroup.LegalSalaryMinimum
        Dim ibcMaximo As Decimal = 25 * smmlv
        Dim ibcReal As Decimal = 0
        ' Verificar si el IBC inicial supera el tope
        If ibcInicial > ibcMaximo Then
            ibcReal = ibcMaximo
        Else
            ibcReal = ibcInicial
        End If
        Return ibcReal
    End Function

End Class