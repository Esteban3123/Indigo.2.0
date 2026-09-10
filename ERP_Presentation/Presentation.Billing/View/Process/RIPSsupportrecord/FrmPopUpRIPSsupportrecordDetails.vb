'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 21-11-2025
'
' Last Modified By : 
' Last Modified On : 
' Description      : Formulario Popup Detalle Registro Soporte RIPS
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Controls
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Billing.MVP
Imports DevExpress.Data.Async.Helpers
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Domain.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Formulario popup para capturar los detalles de estancia hospitalaria
''' que serán agregados al registro de soporte RIPS.
''' </summary>
Public Class FrmPopUpRIPSsupportrecordDetails
    Inherits FormBase
    Implements IRIPSSupportRecordDetailsView

#Region "CONSTANTS"

    ''' <summary>Nombre del módulo.</summary>
    Private Const MODULE_NAME As String = "Billing"

    ''' <summary>Código de condición de egreso: Morgue (requiere diagnóstico causa de muerte).</summary>
    Private Const DISCHARGE_CONDITION_MORGUE As Integer = 11

#End Region

#Region "BUILDER"

    ''' <summary>
    ''' Inicializa una nueva instancia del formulario popup de detalles RIPS.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        InitializeControls()
    End Sub

#End Region

#Region "EVENTS DELEGATES"

    ''' <summary>
    ''' Delegado para el evento cuando se agrega un detalle de estancia hospitalaria.
    ''' </summary>
    ''' <param name="detail">El detalle que fue agregado.</param>
    Public Delegate Sub DetailAddedEventHandler(detail As RIPSSupportRecordDetail)

    ''' <summary>
    ''' Evento que se dispara cuando se agrega un nuevo detalle de estancia hospitalaria.
    ''' El formulario padre puede suscribirse para actualizar su grid en tiempo real.
    ''' </summary>
    Public Event DetailAdded As DetailAddedEventHandler

#End Region

#Region "GLOBALS"

    ''' <summary>Fecha de ingreso del paciente para validaciones de rango.</summary>
    Private _admissionDate As DateTime?

    ''' <summary>Código del ingreso asociado para cálculos de estancia.</summary>
    Private _admissionCode As String

    ''' <summary>Código del paciente del ingreso.</summary>
    Private _patientCode As String

    ''' <summary>Presentador del formulario (patrón MVP).</summary>
    Private _presenter As PRIPSSupportRecord

    ''' <summary>Profesional de la salud seleccionado actualmente.</summary>
    Private _healthProfessional As HealthCareProfessionalXpo

    ''' <summary>Lista de detalles agregados para ser consumidos por el formulario padre.</summary>
    Private _addedDetailRecord As List(Of RIPSSupportRecordDetail)

    ''' <summary>Entidad CUPS seleccionada actualmente.</summary>
    Private _cupsEntity As CupsEntityXpo

    ''' <summary>Id de la descripción relacionada para persistirla globalmente </summary>
    Private _descriptionId As Integer

    ''' <summary>Indica si el popup está en modo edición.</summary>
    Private _isEditMode As Boolean = False

    ''' <summary>Detalle a editar cuando está en modo edición.</summary>
    Private _detailToEdit As RIPSSupportRecordDetail

    ''' <summary>Índice del detalle que se está editando en la lista del formulario padre.</summary>
    Private _editIndex As Integer = -1

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Obtiene los datos del registro de estancia hospitalaria
    ''' </summary>
    Public ReadOnly Property GetRecordData() As Object
        Get
            Return New With {
                .StartDate = RIPSDteStartDate.DateTime,
                .EndDate = RIPSDteEndDate.DateTime,
                .FunctionalUnitId = If(RIPSSleFunctionalUnit.EditValue IsNot Nothing, RIPSSleFunctionalUnit.EditValue, Nothing),
                .StayTypeId = If(RIPSSleStayType.EditValue IsNot Nothing, RIPSSleStayType.EditValue, Nothing),
                .CUPSId = If(RIPSTxtCUPSStay.EditValue IsNot Nothing, RIPSTxtCUPSStay.EditValue, Nothing),
                .StayDays = RIPSTxtStayDays.Text,
                .HealthProfessionalId = If(RIPSSleHealthProfessional.EditValue IsNot Nothing, RIPSSleHealthProfessional.EditValue, Nothing),
                .SpecialtyId = If(RIPSSleSpecialty.EditValue IsNot Nothing, RIPSSleSpecialty.EditValue, Nothing),
                .PrincipalDiagnosisId = If(RIPSSlePrincipalDiagnosis.EditValue IsNot Nothing, RIPSSlePrincipalDiagnosis.EditValue, Nothing),
                .RelatedDiagnosisId = If(RIPSSleRelatedDiagnosis.EditValue IsNot Nothing, RIPSSleRelatedDiagnosis.EditValue, Nothing),
                .DischargeConditionId = If(RIPSSleDischargeCondition.EditValue IsNot Nothing, RIPSSleDischargeCondition.EditValue, Nothing),
                .DeathCauseDiagnosisId = If(RIPSSleDeathCauseDiagnosis.EditValue IsNot Nothing, RIPSSleDeathCauseDiagnosis.EditValue, Nothing)
            }
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el código de ingreso para cálculos de estancia
    ''' </summary>
    Public Property AdmissionCode As String
        Get
            Return _admissionCode
        End Get
        Set(value As String)
            _admissionCode = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el código del paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientCode As String
        Get
            Return _patientCode
        End Get
        Set(value As String)
            _patientCode = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la lista de detalles agregados desde el popup (para ser consumido por el formulario padre)
    ''' </summary>
    Public ReadOnly Property AddedDetailRecord As List(Of RIPSSupportRecordDetail)
        Get
            Return _addedDetailRecord
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el índice del detalle que se está editando (para identificar detalles nuevos sin guardar).
    ''' </summary>
    Public ReadOnly Property EditIndex As Integer
        Get
            Return _editIndex
        End Get
    End Property

    ''' <summary>
    ''' Indica si el popup está en modo edición.
    ''' </summary>
    Public ReadOnly Property IsEditMode As Boolean
        Get
            Return _isEditMode
        End Get
    End Property

    ''' <summary>
    ''' Establece el detalle a editar y activa el modo edición.
    ''' </summary>
    ''' <param name="detail">Detalle a editar.</param>
    ''' <param name="index">Índice del detalle en la lista del formulario padre.</param>
    Public Sub SetDetailToEdit(detail As RIPSSupportRecordDetail, index As Integer)
        _isEditMode = True
        _detailToEdit = detail
        _editIndex = index
    End Sub

    ' Implementación de IRIPSSupportRecordDetailsView
    Public Property StartDate As DateTime? Implements IRIPSSupportRecordDetailsView.StartDate
        Get
            Return RIPSDteStartDate.EditValue
        End Get
        Set(value As DateTime?)
            RIPSDteStartDate.EditValue = value
        End Set
    End Property

    Public Property EndDate As DateTime? Implements IRIPSSupportRecordDetailsView.EndDate
        Get
            Return RIPSDteEndDate.EditValue
        End Get
        Set(value As DateTime?)
            RIPSDteEndDate.EditValue = value
        End Set
    End Property

    Public WriteOnly Property AdmissionDate As DateTime? Implements IRIPSSupportRecordDetailsView.AdmissionDate
        Set(value As DateTime?)
            _admissionDate = value
            If value.HasValue Then
                RIPSDteStartDate.Properties.MinValue = value.Value
                RIPSDteEndDate.Properties.MinValue = value.Value
            End If
        End Set
    End Property

    Public Property FunctionalUnitsDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.FunctionalUnitsDataSource
        Get
            Return RIPSSleFunctionalUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    Public Property StayTypesDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.StayTypesDataSource
        Get
            Return RIPSSleStayType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleStayType.Properties.DataSource = value
        End Set
    End Property

    Public Property HealthProfessionalsDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.HealthProfessionalsDataSource
        Get
            Return RIPSSleHealthProfessional.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleHealthProfessional.Properties.DataSource = value
        End Set
    End Property

    Public Property SpecialtiesDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.SpecialtiesDataSource
        Get
            Return RIPSSleSpecialty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleSpecialty.Properties.DataSource = value
        End Set
    End Property

    Public Property PrincipalDiagnosisDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.PrincipalDiagnosisDataSource
        Get
            Return RIPSSlePrincipalDiagnosis.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSlePrincipalDiagnosis.Properties.DataSource = value
        End Set
    End Property

    Public Property RelatedDiagnosisDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.RelatedDiagnosisDataSource
        Get
            Return RIPSSleRelatedDiagnosis.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleRelatedDiagnosis.Properties.DataSource = value
        End Set
    End Property

    Public Property DischargeConditionsDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.DischargeConditionsDataSource
        Get
            Return RIPSSleDischargeCondition.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleDischargeCondition.Properties.DataSource = value
        End Set
    End Property

    Public Property DeathCauseDiagnosisDataSource As XPInstantFeedbackSource Implements IRIPSSupportRecordDetailsView.DeathCauseDiagnosisDataSource
        Get
            Return RIPSSleDeathCauseDiagnosis.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            RIPSSleDeathCauseDiagnosis.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "METHODS"
    ''' <summary>
    ''' Inicializa los controles del formulario
    ''' </summary>
    Private Sub InitializeControls()
        ' Configurar campos de fecha con hora
        RIPSDteStartDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True
        RIPSDteEndDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True

        ' Configurar formato de visualización para mostrar fecha y hora
        RIPSDteStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        RIPSDteStartDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        RIPSDteStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        RIPSDteStartDate.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"

        RIPSDteEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        RIPSDteEndDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm"
        RIPSDteEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        RIPSDteEndDate.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm"

        ' Inicializar campos deshabilitados
        RIPSTxtStayDays.Properties.ReadOnly = True
        RIPSSleDeathCauseDiagnosis.Enabled = False

        ' Inicializar presentador y cargar datos iniciales
        _presenter = New PRIPSSupportRecord(Me, Me.Tag.ToString)
        _presenter.LoadInitialData()
        LoadStaticDischargeConditions()
    End Sub

    ''' <summary>
    ''' Establece el datasource del SLE de condición de egreso con la lista quemada requerida por negocio.
    ''' </summary>
    Private Sub LoadStaticDischargeConditions()
        Dim dischargeConditions = New List(Of Tuple(Of Integer, String)) From {
            Tuple.Create(1, "Trasladar a Urgencias: Solo consulta externa"),
            Tuple.Create(2, "Trasladar a Observacion Urgencias: Solo urgencias"),
            Tuple.Create(3, "Trasladar a Hospitalizacion: Dif Misma Unidad"),
            Tuple.Create(4, "Trasladar a UCI Adulto: Dif Misma Unidad"),
            Tuple.Create(5, "Trasladar a UCI Pediatrica: Dif Misma Unidad"),
            Tuple.Create(6, "Trasladar a UCI Neonatal: Dif Misma Unidad"),
            Tuple.Create(7, "Trasladar a Consulta Externa: Dif Misma Unidad"),
            Tuple.Create(8, "Trasladar a Cirugia: Dif Misma Unidad"),
            Tuple.Create(9, "Hospitalizacion en Casa"),
            Tuple.Create(10, "Referencia"),
            Tuple.Create(11, "Morgue"),
            Tuple.Create(12, "Salida"),
            Tuple.Create(13, "Continua en la Unidad"),
            Tuple.Create(14, "Paciente en Tratamiento"),
            Tuple.Create(15, "Retiro Voluntario"),
            Tuple.Create(16, "Fuga"),
            Tuple.Create(17, "Salida Parcial"),
            Tuple.Create(18, "Estancia con la Madre"),
            Tuple.Create(19, "U. Cuidado Intermedio"),
            Tuple.Create(20, "U. Basica"),
            Tuple.Create(21, "Hospitalización Pediatría")
        }

        RIPSSleDischargeCondition.Properties.DataSource = dischargeConditions
        RIPSSleDischargeCondition.Properties.DisplayMember = "Item2"
        RIPSSleDischargeCondition.Properties.ValueMember = "Item1"

        GridColumn11.FieldName = "Item1"
        GridColumn12.FieldName = "Item2"
    End Sub

    ''' <summary>
    ''' Establece las especialidades del médico en el combo
    ''' </summary>
    ''' <param name="healthProfessional">Profesional de la salud</param>
    Private Sub SetSpecialties(healthProfessional As HealthCareProfessionalXpo)
        Dim listSpecialty As New List(Of Tuple(Of String, String))

        If healthProfessional IsNot Nothing Then
            If healthProfessional.CODESPEC1 IsNot Nothing Then
                listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC1.CODESPECI, healthProfessional.CODESPEC1.CodeName))
            End If
            If healthProfessional.CODESPEC2 IsNot Nothing Then
                listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC2.CODESPECI, healthProfessional.CODESPEC2.CodeName))
            End If
            If healthProfessional.CODESPEC3 IsNot Nothing Then
                listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC3.CODESPECI, healthProfessional.CODESPEC3.CodeName))
            End If
        End If

        RIPSSleSpecialty.Properties.DataSource = listSpecialty
        RIPSSleSpecialty.Properties.DisplayMember = "Item2"
        RIPSSleSpecialty.Properties.ValueMember = "Item1"

        If listSpecialty.Count = 1 Then
            RIPSSleSpecialty.EditValue = listSpecialty(0).Item1
        Else
            RIPSSleSpecialty.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la fila seleccionada de un SearchLookUpEdit manejando los proxys generados por XPInstantFeedbackSource.
    ''' </summary>
    Private Function GetSelectedLookupRow(Of T As Class)(lookup As SearchLookUpEdit) As T
        If lookup Is Nothing Then Return Nothing
        Return ConvertProxyRow(Of T)(lookup.GetSelectedDataRow())
    End Function

    ''' <summary>
    ''' Convierte el objeto proxy devuelto por DevExpress al tipo solicitado.
    ''' </summary>
    Private Function ConvertProxyRow(Of T As Class)(rowObject As Object) As T
        If rowObject Is Nothing Then Return Nothing

        Dim proxy = TryCast(rowObject, ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If proxy IsNot Nothing AndAlso proxy.OriginalRow IsNot Nothing Then
            Return TryCast(proxy.OriginalRow, T)
        End If

        Return TryCast(rowObject, T)
    End Function

    ''' <summary>
    ''' Calcula los días de estancia usando el servicio de estancias del sistema.
    ''' </summary>
    ''' <remarks>
    ''' Utiliza la misma lógica de cálculo que el resto del sistema para garantizar
    ''' consistencia. En caso de error, hace fallback a un cálculo simple por diferencia de días.
    ''' </remarks>
    Private Async Sub CalculateStayDays()
        ' Validar que existan las fechas y el código de ingreso
        If RIPSDteStartDate.DateTime = Nothing OrElse
           RIPSDteEndDate.DateTime = Nothing OrElse
           String.IsNullOrEmpty(_admissionCode) Then
            RIPSTxtStayDays.Text = "0"
            Return
        End If

        Dim startDate As DateTime = RIPSDteStartDate.DateTime
        Dim endDate As DateTime = RIPSDteEndDate.DateTime

        ' Validar que la fecha final sea mayor o igual a la inicial
        If endDate < startDate Then
            RIPSTxtStayDays.Text = "0"
            Return
        End If

        Try
            ' Calcular días de estancia usando el servicio
            Await Task.Run(
                Sub()
                    Using model As New MRIPSSupportRecord(Tag)
                        Dim result = model.CalculateUnitStayForRange(_admissionCode, startDate, endDate)

                        RIPSTxtStayDays.SafeInvoke(
                            Sub()
                                If result IsNot Nothing Then
                                    RIPSTxtStayDays.Text = result.Days.ToString()
                                Else
                                    ' Fallback: cálculo simple
                                    RIPSTxtStayDays.Text = CalculateSimpleDays(startDate, endDate).ToString()
                                End If
                            End Sub)
                    End Using
                End Sub)

        Catch ex As Exception
            ' En caso de error, usar cálculo simple por diferencia de días
            System.Diagnostics.Debug.WriteLine($"CalculateStayDays: Error en cálculo: {ex.Message}")
            RIPSTxtStayDays.Text = CalculateSimpleDays(startDate, endDate).ToString()
        End Try
    End Sub

    ''' <summary>
    ''' Calcula los días de estancia de forma simple (diferencia de días).
    ''' </summary>
    ''' <param name="startDate">Fecha de inicio.</param>
    ''' <param name="endDate">Fecha de fin.</param>
    ''' <returns>Número de días entre las fechas.</returns>
    Private Function CalculateSimpleDays(startDate As DateTime, endDate As DateTime) As Integer
        If endDate < startDate Then Return 0
        Return CInt((endDate.Date - startDate.Date).TotalDays)
    End Function

    ''' <summary>
    ''' Valida todos los controles requeridos del formulario antes de agregar un detalle.
    ''' </summary>
    ''' <returns>True si todas las validaciones pasan; False en caso contrario.</returns>
    Private Function ValidateControls() As Boolean
        ' ═══════════════════════════════════════════════════════════════
        ' VALIDACIONES DE FECHAS
        ' ═══════════════════════════════════════════════════════════════

        If RIPSDteStartDate.DateTime = Nothing Then
            ShowWarning("Debe seleccionar la Fecha Inicio")
            RIPSDteStartDate.Focus()
            Return False
        End If

        If _admissionDate.HasValue AndAlso RIPSDteStartDate.DateTime < _admissionDate.Value Then
            ShowWarning("La Fecha Inicio debe ser posterior a la fecha de ingreso")
            RIPSDteStartDate.Focus()
            Return False
        End If

        If RIPSDteEndDate.DateTime = Nothing Then
            ShowWarning("Debe seleccionar la Fecha Final")
            RIPSDteEndDate.Focus()
            Return False
        End If

        If RIPSDteEndDate.DateTime < RIPSDteStartDate.DateTime Then
            ShowWarning("La Fecha Final no puede ser menor a la Fecha Inicial")
            RIPSDteEndDate.Focus()
            Return False
        End If

        ' Validar que la fecha final no sea futura
        Dim serverDate = GetDateServer()
        If RIPSDteEndDate.DateTime.Date > serverDate.Date Then
            ShowWarning("La Fecha Final no puede ser posterior a la fecha actual")
            RIPSDteEndDate.Focus()
            Return False
        End If

        ' ═══════════════════════════════════════════════════════════════
        ' VALIDACIONES DE UNIDAD FUNCIONAL Y TIPO DE ESTANCIA
        ' ═══════════════════════════════════════════════════════════════

        If RIPSSleFunctionalUnit.EditValue Is Nothing Then
            ShowWarning("Debe seleccionar la Unidad Funcional")
            RIPSSleFunctionalUnit.Focus()
            Return False
        End If

        If RIPSSleStayType.EditValue Is Nothing Then
            ShowWarning("Debe seleccionar el Tipo de Estancia")
            RIPSSleStayType.Focus()
            Return False
        End If

        If RIPSTxtCUPSStay.EditValue Is Nothing OrElse _cupsEntity Is Nothing Then
            ShowWarning("Debe seleccionar el CUPS Estancia")
            RIPSTxtCUPSStay.Focus()
            Return False
        End If

        ' ═══════════════════════════════════════════════════════════════
        ' VALIDACIONES DE PROFESIONAL DE SALUD
        ' ═══════════════════════════════════════════════════════════════

        If RIPSSleHealthProfessional.EditValue Is Nothing OrElse _healthProfessional Is Nothing Then
            ShowWarning("Debe seleccionar el Profesional de la Salud")
            RIPSSleHealthProfessional.Focus()
            Return False
        End If

        If RIPSSleSpecialty.EditValue Is Nothing Then
            ShowWarning("Debe seleccionar la Especialidad del Profesional")
            RIPSSleSpecialty.Focus()
            Return False
        End If

        ' ═══════════════════════════════════════════════════════════════
        ' VALIDACIONES DE DIAGNÓSTICOS
        ' ═══════════════════════════════════════════════════════════════

        If RIPSSlePrincipalDiagnosis.EditValue Is Nothing Then
            ShowWarning("Debe seleccionar el Diagnóstico Principal")
            RIPSSlePrincipalDiagnosis.Focus()
            Return False
        End If

        ' ═══════════════════════════════════════════════════════════════
        ' VALIDACIONES DE CONDICIÓN DE EGRESO
        ' ═══════════════════════════════════════════════════════════════

        If RIPSSleDischargeCondition.EditValue Is Nothing Then
            ShowWarning("Debe seleccionar la Condición de Egreso")
            RIPSSleDischargeCondition.Focus()
            Return False
        End If

        ' Validar diagnóstico causa de muerte si condición de egreso = Morgue
        If IsDischargeConditionMorgue() Then
            If RIPSSleDeathCauseDiagnosis.EditValue Is Nothing Then
                ShowWarning("Debe seleccionar el Diagnóstico Causa de Muerte cuando la condición de egreso es Morgue")
                RIPSSleDeathCauseDiagnosis.Focus()
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Verifica si la condición de egreso seleccionada es Morgue.
    ''' </summary>
    ''' <returns>True si la condición es Morgue (11); False en caso contrario.</returns>
    Private Function IsDischargeConditionMorgue() As Boolean
        If RIPSSleDischargeCondition.EditValue Is Nothing Then Return False

        Dim dischargeValue As Integer
        If Integer.TryParse(RIPSSleDischargeCondition.EditValue.ToString(), dischargeValue) Then
            Return dischargeValue = DISCHARGE_CONDITION_MORGUE
        End If

        Return False
    End Function

    ''' <summary>
    ''' Muestra un mensaje de advertencia al usuario.
    ''' </summary>
    ''' <param name="message">Mensaje a mostrar.</param>
    Private Sub ShowWarning(message As String)
        MessageIndigo.Show(message, MessageType.Warning, Me.Text, Me)
    End Sub

    ''' <summary>
    ''' Muestra un mensaje de error al usuario.
    ''' </summary>
    ''' <param name="message">Mensaje a mostrar.</param>
    Private Sub ShowError(message As String)
        MessageIndigo.Show(message, MessageType.Errores, Me.Text, Botones.Aceptar, "")
    End Sub

    ''' <summary>
    ''' Limpia todos los controles del formulario y restablece las entidades en memoria
    ''' para permitir agregar un nuevo registro de estancia.
    ''' </summary>
    Private Sub CleanControls()
        ' ═══════════════════════════════════════════════════════════════
        ' LIMPIAR CONTROLES DE FECHA
        ' ═══════════════════════════════════════════════════════════════
        RIPSDteStartDate.EditValue = Nothing
        RIPSDteEndDate.EditValue = Nothing
        RIPSTxtStayDays.Text = "0"

        ' ═══════════════════════════════════════════════════════════════
        ' LIMPIAR CONTROLES DE UNIDAD Y TIPO DE ESTANCIA
        ' ═══════════════════════════════════════════════════════════════
        RIPSSleFunctionalUnit.EditValue = Nothing
        RIPSSleStayType.EditValue = Nothing
        RIPSTxtCUPSStay.EditValue = Nothing
        RIPSTxtCUPSStay.Text = String.Empty
        RIPSTxtCUPSStayDescription.Text = String.Empty

        ' ═══════════════════════════════════════════════════════════════
        ' LIMPIAR CONTROLES DE PROFESIONAL DE SALUD
        ' ═══════════════════════════════════════════════════════════════
        RIPSSleHealthProfessional.EditValue = Nothing
        RIPSSleSpecialty.EditValue = Nothing
        RIPSSleSpecialty.Properties.DataSource = Nothing

        ' ═══════════════════════════════════════════════════════════════
        ' LIMPIAR CONTROLES DE DIAGNÓSTICOS
        ' ═══════════════════════════════════════════════════════════════
        RIPSSlePrincipalDiagnosis.EditValue = Nothing
        RIPSSleRelatedDiagnosis.EditValue = Nothing

        ' ═══════════════════════════════════════════════════════════════
        ' LIMPIAR CONTROLES DE CONDICIÓN DE EGRESO
        ' ═══════════════════════════════════════════════════════════════
        RIPSSleDischargeCondition.EditValue = Nothing
        RIPSSleDeathCauseDiagnosis.EditValue = Nothing
        RIPSSleDeathCauseDiagnosis.Enabled = False

        ' ═══════════════════════════════════════════════════════════════
        ' LIMPIAR ENTIDADES EN MEMORIA
        ' ═══════════════════════════════════════════════════════════════
        _healthProfessional = Nothing
        _cupsEntity = Nothing

        ' Restaurar texto del botón si estaba en modo edición
        If _isEditMode Then
            RIPSBtnAdd.Text = "Agregar"
            _isEditMode = False
            _detailToEdit = Nothing
            _editIndex = -1
        End If

        ' Establecer el foco en el primer campo para continuar agregando
        RIPSDteStartDate.Focus()
    End Sub
#End Region

#Region "EVENTS"
    ''' <summary>
    ''' Evento Load del formulario.
    ''' Inicializa la lista de detalles y configura la interfaz.
    ''' Si está en modo edición, carga los datos del detalle.
    ''' </summary>
    Private Sub FrmPopUpRIPSsupportrecordDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicializar la lista de detalles
        _addedDetailRecord = New List(Of RIPSSupportRecordDetail)()

        ' Ocultar la barra de herramientas (no se usa en popup)
        Me.ToolBar.Hide()

        ' Si está en modo edición, cargar los datos del detalle
        If _isEditMode AndAlso _detailToEdit IsNot Nothing Then
            LoadDetailForEdit()
        Else
            ' Establecer el foco en el primer campo
            RIPSDteStartDate.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento FormClosing del formulario.
    ''' Establece DialogResult.OK si hay registros agregados para que el formulario padre
    ''' pueda obtener los detalles incluso si se cierra con el botón X.
    ''' </summary>
    Private Sub FrmPopUpRIPSsupportrecordDetails_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Si hay registros agregados, establecer resultado OK
        If _addedDetailRecord IsNot Nothing AndAlso _addedDetailRecord.Count > 0 Then
            Me.DialogResult = DialogResult.OK
        End If

        ' Limpiar referencias
        _presenter = Nothing
        _healthProfessional = Nothing
        _cupsEntity = Nothing
        CleanControls()
    End Sub

    ''' <summary>
    ''' Evento click del botón Agregar.
    ''' Valida los datos, construye el detalle de estancia y lo agrega o actualiza en la lista.
    ''' </summary>
    Private Sub RIPSBtnAdd_Click(sender As Object, e As EventArgs) Handles RIPSBtnAdd.Click
        ' Validar todos los campos requeridos
        If Not ValidateControls() Then
            Return
        End If

        Try
            ' Crear o actualizar el detalle de estancia hospitalaria
            Dim detail = BuildStayDetail()

            If detail IsNot Nothing Then
                If _isEditMode Then
                    ' Modo edición: actualizar el detalle existente
                    ' Preservar el Id y ChangeTracker si existe
                    If _detailToEdit IsNot Nothing Then
                        detail.Id = _detailToEdit.Id
                        If _detailToEdit.ChangeTracker IsNot Nothing Then
                            detail.ChangeTracker = _detailToEdit.ChangeTracker
                        End If
                    End If
                    _addedDetailRecord.Add(detail)
                    ' Cerrar el popup después de actualizar
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                Else
                    ' Modo nuevo: agregar a la lista
                    _addedDetailRecord.Add(detail)
                    ' Disparar evento para que el padre actualice su grid inmediatamente
                    RaiseEvent DetailAdded(detail)
                    ' Limpiar controles para permitir agregar más registros
                    CleanControls()
                End If
            End If

        Catch ex As Exception
            ShowError($"Error al agregar el registro de estancia: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Construye el objeto de detalle de estancia hospitalaria con los datos del formulario.
    ''' </summary>
    ''' <returns>Objeto RIPSSupportRecordDetail con los datos capturados, o Nothing si hay error.</returns>
    Private Function BuildStayDetail() As RIPSSupportRecordDetail
        ' Obtener el tercero del profesional de salud
        Dim thirdPartyInfo As ThirdParty = Nothing
        Using model As New MThirdParty(Tag)
            thirdPartyInfo = model.GetThirdParty(_healthProfessional.CODIGONIT)
        End Using

        If thirdPartyInfo Is Nothing Then
            ShowWarning("No se encontró información del tercero asociado al profesional de salud")
            RIPSSleHealthProfessional.Focus()
            Return Nothing
        End If

        ' Obtener entidades seleccionadas
        Dim principalDiagnosis = GetSelectedLookupRow(Of INDIAGNOS)(RIPSSlePrincipalDiagnosis)
        Dim relatedDiagnosis = GetSelectedLookupRow(Of INDIAGNOS)(RIPSSleRelatedDiagnosis)
        Dim functionalUnit = GetSelectedLookupRow(Of PayrollFunctionalUnit)(RIPSSleFunctionalUnit)

        ' Validar diagnóstico principal
        If principalDiagnosis Is Nothing Then
            ShowWarning("No se pudo obtener la información del diagnóstico principal")
            RIPSSlePrincipalDiagnosis.Focus()
            Return Nothing
        End If

        ' Validar unidad funcional
        If functionalUnit Is Nothing Then
            ShowWarning("No se pudo obtener la información de la unidad funcional")
            RIPSSleFunctionalUnit.Focus()
            Return Nothing
        End If

        ' Obtener descripción del contrato CUPS (opcional)
        Dim selectedContractDescription = _cupsEntity?.CUPSEntityContractDescriptionsXpo?.FirstOrDefault(Function(x) x.Id = _descriptionId)

        ' Obtener la especialidad del profesional (usar la seleccionada o la primera disponible)
        Dim specialtyCode As String = GetSelectedSpecialtyCode()

        ' Construir el detalle
        Return New RIPSSupportRecordDetail With {
            .InitialDate = RIPSDteStartDate.DateTime,
            .FinalDate = RIPSDteEndDate.DateTime,
            .FunctionalUnitId = functionalUnit.Id,
            .FunctionalUnitCode = functionalUnit.Codigo,
            .TypeStay = RIPSSleStayType.EditValue,
            .CalculatedDaysStay = RIPSTxtStayDays.Text,
            .StayCUPSEntityId = _cupsEntity.Id,
            .CUPSCode = _cupsEntity.Code,
            .SpecialtyCode = RIPSSleSpecialty.EditValue?.ToString(),
            .PerformsHealthProfessionalThirdPartyId = thirdPartyInfo.Id,
            .ProfessionalCode = _healthProfessional.CODPROSAL,
            .PerformsProfessionalSpecialty = specialtyCode,
            .NitMedico = _healthProfessional.CODIGONIT,
            .MainDiagnosis = principalDiagnosis.CODDIAGNO,
            .RelatedDiagnosis = relatedDiagnosis?.CODDIAGNO,
            .DischargeCondition = RIPSSleDischargeCondition.EditValue,
            .DiagnosisCauseDeath = If(IsDischargeConditionMorgue(), RIPSSleDeathCauseDiagnosis.EditValue, Nothing),
            .AdmissionNumber = AdmissionCode,
            .PatientCode = PatientCode,
            .Quantity = RIPSTxtStayDays.Text,
            .CreationDate = GetDateServer(),
            .CreationUser = indigo.UserIndigo,
            .ContractDescriptionId = selectedContractDescription?.ContractDescriptionId?.Id,
            .StayCUPSEntityContractDescriptionId = selectedContractDescription?.Id
        }
    End Function

    ''' <summary>
    ''' Obtiene el código de especialidad seleccionado o la primera del profesional.
    ''' </summary>
    ''' <returns>Código de especialidad o cadena vacía si no hay.</returns>
    Private Function GetSelectedSpecialtyCode() As String
        ' Primero intentar con la especialidad seleccionada
        If RIPSSleSpecialty.EditValue IsNot Nothing Then
            Return RIPSSleSpecialty.EditValue.ToString()
        End If

        ' Fallback: usar la primera especialidad del profesional
        If _healthProfessional?.CODESPEC1 IsNot Nothing Then
            Return _healthProfessional.CODESPEC1.CODESPECI
        End If

        Return String.Empty
    End Function

    ''' <summary>
    ''' Evento cuando cambia la fecha inicial
    ''' </summary>
    Private Sub RIPSDteStartDate_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSDteStartDate.EditValueChanged
        If RIPSDteStartDate.DateTime <> Nothing Then
            RIPSDteEndDate.Properties.MinValue = RIPSDteStartDate.DateTime
            CalculateStayDays()
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando cambia la fecha final
    ''' </summary>
    Private Sub RIPSDteEndDate_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSDteEndDate.EditValueChanged
        CalculateStayDays()
    End Sub

    ''' <summary>
    ''' Evento cuando cambia la unidad funcional - carga los tipos de estancia asociados
    ''' </summary>
    Private Sub RIPSSleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSSleFunctionalUnit.EditValueChanged
        ' Limpiar el tipo de estancia cuando cambie la unidad funcional
        RIPSSleStayType.EditValue = Nothing
        RIPSTxtCUPSStay.EditValue = Nothing
        RIPSTxtCUPSStayDescription.Text = String.Empty

        If RIPSSleFunctionalUnit.EditValue IsNot Nothing Then
            ' Llamar al presentador para cargar los tipos de estancia
            _presenter?.OnFunctionalUnitChanged(RIPSSleFunctionalUnit.EditValue.ToString())
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando cambia el tipo de estancia.
    ''' Carga automáticamente el CUPS asociado según la configuración de tarifas.
    ''' </summary>
    ''' <remarks>
    ''' Lógica de negocio para selección de CUPS según tipo de liquidación:
    ''' - TIPLIQEST = 1: Usa GENCUPS / IDDESCRIPCIONRELACIONADA_CUPS
    ''' - TIPLIQEST = 2 o 3: Usa GENCUPS2 / IDDESCRIPCIONRELACIONADA_CUPS2
    ''' </remarks>
    Private Sub RIPSSleStayType_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSSleStayType.EditValueChanged
        ' Limpiar siempre los campos de CUPS al cambiar el tipo de estancia
        ClearCUPSData()

        ' Si no hay tipo de estancia seleccionado, terminar
        If RIPSSleStayType.EditValue Is Nothing Then
            Return
        End If

        Try
            ' Obtener el tipo de estancia seleccionado
            Dim stayType = GetSelectedStayType()
            If stayType Is Nothing Then
                Return
            End If

            ' Validar que tenga tarifas configuradas
            If stayType.CHGENTARIs Is Nothing OrElse stayType.CHGENTARIs.Count = 0 Then
                ShowWarning("El tipo de estancia seleccionado no tiene tarifas configuradas")
                Return
            End If

            ' Obtener los IDs del CUPS según el tipo de liquidación
            Dim tariff As CHGENTARI = stayType.CHGENTARIs(0)
            Dim cupsIds = GetCUPSIdsFromTariff(tariff)
            _descriptionId = cupsIds.DescriptionId

            ' Cargar el CUPS si existe
            If cupsIds.CupsId.HasValue Then
                LoadCUPSData(cupsIds.CupsId.Value, cupsIds.DescriptionId)
            End If

        Catch ex As Exception
            ShowError($"Error al cargar el CUPS del tipo de estancia: {ex.Message}")
            ClearCUPSData()
        End Try
    End Sub

    ''' <summary>
    ''' Limpia los datos del CUPS y sus controles relacionados.
    ''' </summary>
    Private Sub ClearCUPSData()
        _cupsEntity = Nothing
        RIPSTxtCUPSStay.EditValue = Nothing
        RIPSTxtCUPSStay.Text = String.Empty
        RIPSTxtCUPSStayDescription.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Carga los datos del detalle en los controles del formulario para modo edición.
    ''' </summary>
    Private Sub LoadDetailForEdit()
        If _detailToEdit Is Nothing Then Return

        Try
            ' Cargar fechas
            RIPSDteStartDate.EditValue = _detailToEdit.InitialDate
            RIPSDteEndDate.EditValue = _detailToEdit.FinalDate

            ' Cargar unidad funcional y tipo de estancia
            If _detailToEdit.FunctionalUnitId > 0 Then
                ' Obtener el código de la unidad funcional (necesario porque el lookup usa Codigo como ValueMember)
                Dim functionalUnitCode As String = _detailToEdit.FunctionalUnitCode

                ' Si no hay código almacenado, obtenerlo desde el ID
                If String.IsNullOrEmpty(functionalUnitCode) Then
                    Using model As New MRIPSSupportRecord(Tag)
                        Dim functionalUnit = model.GetFunctionalUnitById(_detailToEdit.FunctionalUnitId)
                        If functionalUnit IsNot Nothing Then
                            functionalUnitCode = functionalUnit.Codigo
                        End If
                    End Using
                End If

                ' Deshabilitar temporalmente el evento para evitar que se dispare con un valor incorrecto
                RemoveHandler RIPSSleFunctionalUnit.EditValueChanged, AddressOf RIPSSleFunctionalUnit_EditValueChanged
                Try
                    ' Establecer el código (el lookup usa Codigo como ValueMember)
                    If Not String.IsNullOrEmpty(functionalUnitCode) Then
                        RIPSSleFunctionalUnit.EditValue = functionalUnitCode
                    End If
                Finally
                    ' Rehabilitar el evento
                    AddHandler RIPSSleFunctionalUnit.EditValueChanged, AddressOf RIPSSleFunctionalUnit_EditValueChanged
                End Try

                ' Cargar tipos de estancia para la unidad funcional usando el código correcto
                If Not String.IsNullOrEmpty(functionalUnitCode) Then
                    _presenter?.OnFunctionalUnitChanged(functionalUnitCode)
                End If
            End If

            If Not String.IsNullOrEmpty(_detailToEdit.TypeStay) Then
                RIPSSleStayType.EditValue = _detailToEdit.TypeStay
            End If

            ' Cargar CUPS si existe
            If _detailToEdit.StayCUPSEntityId > 0 Then
                Using model As New MRIPSSupportRecord(Tag)
                    Dim cupsEntity = model.GetCupsEntityById(_detailToEdit.StayCUPSEntityId)
                    If cupsEntity IsNot Nothing Then
                        _cupsEntity = cupsEntity
                        RIPSTxtCUPSStay.EditValue = cupsEntity.Id
                        RIPSTxtCUPSStay.Text = cupsEntity.CodeDescription

                        ' Cargar descripción del contrato si existe
                        If _detailToEdit.StayCUPSEntityContractDescriptionId > 0 AndAlso
                           cupsEntity.CUPSEntityContractDescriptionsXpo IsNot Nothing Then
                            Dim cupsDescription = cupsEntity.CUPSEntityContractDescriptionsXpo.
                                FirstOrDefault(Function(x) x.Id = _detailToEdit.StayCUPSEntityContractDescriptionId)
                            If cupsDescription IsNot Nothing Then
                                RIPSTxtCUPSStayDescription.Text = cupsDescription.ContractDescriptionId?.CodeName
                            End If
                        End If
                    End If
                End Using
            End If

            ' Cargar profesional de salud
            If _detailToEdit.PerformsHealthProfessionalThirdPartyId > 0 Then
                Using model As New MRIPSSupportRecord(Tag)
                    Dim professional = model.GetHealthProfessionalByThirdPartyId(_detailToEdit.PerformsHealthProfessionalThirdPartyId)
                    If professional IsNot Nothing Then
                        _healthProfessional = professional
                        RIPSSleHealthProfessional.EditValue = professional.CODPROSAL
                        SetSpecialties(professional)
                    End If
                End Using
            End If

            ' Cargar especialidad
            If Not String.IsNullOrEmpty(_detailToEdit.SpecialtyCode) Then
                RIPSSleSpecialty.EditValue = _detailToEdit.SpecialtyCode
            End If

            ' Cargar diagnósticos
            If Not String.IsNullOrEmpty(_detailToEdit.MainDiagnosis) Then
                RIPSSlePrincipalDiagnosis.EditValue = _detailToEdit.MainDiagnosis
            End If

            If Not String.IsNullOrEmpty(_detailToEdit.RelatedDiagnosis) Then
                RIPSSleRelatedDiagnosis.EditValue = _detailToEdit.RelatedDiagnosis
            End If

            ' Cargar condición de egreso
            If _detailToEdit.DischargeCondition IsNot Nothing Then
                RIPSSleDischargeCondition.EditValue = _detailToEdit.DischargeCondition
            End If

            ' Cargar diagnóstico causa de muerte si aplica
            If IsDischargeConditionMorgue() AndAlso Not String.IsNullOrEmpty(_detailToEdit.DiagnosisCauseDeath) Then
                RIPSSleDeathCauseDiagnosis.EditValue = _detailToEdit.DiagnosisCauseDeath
            End If

            ' Cambiar texto del botón a "Actualizar"
            RIPSBtnAdd.Text = "Actualizar"

            ' Establecer foco en el primer campo
            RIPSDteStartDate.Focus()

        Catch ex As Exception
            ShowError($"Error al cargar el detalle para edición: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el objeto CHTIPESTA (tipo de estancia) desde el control de selección.
    ''' </summary>
    ''' <returns>Objeto CHTIPESTA o Nothing si no se puede obtener.</returns>
    Private Function GetSelectedStayType() As CHTIPESTA
        Dim view = RIPSSleStayType.Properties.View
        If view Is Nothing Then Return Nothing

        Dim rowObject = view.GetFocusedRow()
        If rowObject Is Nothing Then Return Nothing

        ' Para XPInstantFeedbackSource, la fila puede venir envuelta en un proxy
        Return ConvertProxyRow(Of CHTIPESTA)(rowObject)
    End Function

    ''' <summary>
    ''' Obtiene los IDs del CUPS y su descripción según el tipo de liquidación de la tarifa.
    ''' </summary>
    ''' <param name="tariff">Tarifa de estancia.</param>
    ''' <returns>Tupla con CupsId y DescriptionId.</returns>
    Private Function GetCUPSIdsFromTariff(tariff As CHGENTARI) As (CupsId As Integer?, DescriptionId As Integer?)
        Select Case tariff.TIPLIQEST
            Case 1
                Return (tariff.GENCUPS, tariff.IDDESCRIPCIONRELACIONADA_CUPS)
            Case 2, 3
                Return (tariff.GENCUPS2, tariff.IDDESCRIPCIONRELACIONADA_CUPS2)
            Case Else
                Return (Nothing, Nothing)
        End Select
    End Function

    ''' <summary>
    ''' Carga los datos del CUPS en los controles del formulario.
    ''' </summary>
    ''' <param name="cupsId">ID del CUPS a cargar.</param>
    ''' <param name="descriptionId">ID de la descripción del CUPS (opcional).</param>
    Private Sub LoadCUPSData(cupsId As Integer, descriptionId As Integer?)
        Using model As New MRIPSSupportRecord(Tag)
            Dim cupsEntity = model.GetCupsEntityById(cupsId)

            If cupsEntity Is Nothing Then
                ShowWarning("No se encontró el CUPS asociado al tipo de estancia")
                Return
            End If

            _cupsEntity = cupsEntity
            RIPSTxtCUPSStay.EditValue = cupsEntity.Id
            RIPSTxtCUPSStay.Text = cupsEntity.CodeDescription

            ' Cargar descripción del contrato si existe
            If descriptionId.HasValue AndAlso
               cupsEntity.CUPSEntityContractDescriptionsXpo IsNot Nothing Then

                Dim cupsDescription = cupsEntity.CUPSEntityContractDescriptionsXpo.
                    FirstOrDefault(Function(x) x.Id = descriptionId.Value)

                If cupsDescription IsNot Nothing Then
                    RIPSTxtCUPSStayDescription.Text = cupsDescription.ContractDescriptionId?.CodeName
                Else
                    RIPSTxtCUPSStayDescription.Text = String.Empty
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Evento cuando cambia el profesional de la salud.
    ''' Carga las especialidades asociadas al profesional seleccionado.
    ''' </summary>
    Private Sub RIPSSleHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSSleHealthProfessional.EditValueChanged
        ' Si no hay selección, limpiar datos relacionados
        If RIPSSleHealthProfessional.EditValue Is Nothing Then
            ClearHealthProfessionalData()
            Return
        End If

        Try
            Dim codeProfessional = RIPSSleHealthProfessional.EditValue.ToString().Trim()

            ' Intentar obtener el profesional desde la fila seleccionada (más eficiente)
            Dim selectedProfessional = GetSelectedLookupRow(Of HealthCareProfessionalXpo)(RIPSSleHealthProfessional)

            If selectedProfessional IsNot Nothing Then
                _healthProfessional = selectedProfessional
            Else
                ' Fallback: buscar en base de datos por código
                Using model As New MServiceOrder(Me.Tag)
                    _healthProfessional = model.GetCareProfessionalByCode(codeProfessional)?.FirstOrDefault()
                End Using
            End If

            ' Validar que se encontró el profesional
            If _healthProfessional Is Nothing Then
                ShowWarning($"No se encontró un profesional de la salud con código {codeProfessional}")
                ClearHealthProfessionalData()
                Return
            End If

            ' Cargar las especialidades del profesional
            SetSpecialties(_healthProfessional)

        Catch ex As Exception
            ShowError($"Error al cargar el profesional de salud: {ex.Message}")
            ClearHealthProfessionalData()
        End Try
    End Sub

    ''' <summary>
    ''' Limpia los datos del profesional de salud y sus controles relacionados.
    ''' </summary>
    Private Sub ClearHealthProfessionalData()
        _healthProfessional = Nothing
        RIPSSleHealthProfessional.EditValue = Nothing
        RIPSSleSpecialty.EditValue = Nothing
        RIPSSleSpecialty.Properties.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Evento cuando cambia el diagnóstico principal - postila el diagnóstico relacionado
    ''' </summary>
    Private Sub RIPSSlePrincipalDiagnosis_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSSlePrincipalDiagnosis.EditValueChanged
        If RIPSSlePrincipalDiagnosis.EditValue IsNot Nothing Then
            ' Postular por defecto el diagnóstico seleccionado
            RIPSSleRelatedDiagnosis.EditValue = RIPSSlePrincipalDiagnosis.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando cambia la condición de egreso.
    ''' Habilita/deshabilita el campo de diagnóstico causa de muerte según la condición.
    ''' </summary>
    Private Sub RIPSSleDischargeCondition_EditValueChanged(sender As Object, e As EventArgs) Handles RIPSSleDischargeCondition.EditValueChanged
        If IsDischargeConditionMorgue() Then
            ' Condición de egreso = Morgue - habilitar campo de causa de muerte
            RIPSSleDeathCauseDiagnosis.Enabled = True
        Else
            ' Deshabilitar y limpiar campo
            RIPSSleDeathCauseDiagnosis.Enabled = False
            RIPSSleDeathCauseDiagnosis.EditValue = Nothing
        End If
    End Sub
#End Region

End Class


