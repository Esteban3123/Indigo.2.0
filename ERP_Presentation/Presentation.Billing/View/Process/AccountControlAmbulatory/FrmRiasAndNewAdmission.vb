'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/02/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Extension

Imports Domain.Entities
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Security.MVP
Imports Domain.Security.Entities
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports Presentation.Controls.MVP
Imports Domain.Entities.Service
Imports DevExpress.Utils.Menu
Imports System.Globalization
Imports DevExpress.Data.Linq
Imports System.ComponentModel
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.Utils
Imports DevExpress.XtraLayout
Imports Presentation.Common.MVP
Imports Domain.Crystal.Entities
Imports Presentation.Billing.MVP
Imports Presentation.Contract.MVP
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Dynamic
Imports System.Collections.Concurrent
Imports System.Drawing
Imports Presentation.Resources
Imports System.Threading
Imports Infrastructure.Data.Xpo.BillingRepository
Imports DevExpress.XtraSplashScreen
Imports Presentation.Billing

#End Region

Public Class FrmRiasAndNewAdmission

#Region "Properties"

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    Dim _requestQuoteServices As Boolean
    Public WriteOnly Property RequestQuoteServices As Boolean
        Set(value As Boolean)
            _requestQuoteServices = value
        End Set
    End Property

    Private _careCenterCode As String
    ''' <summary>
    ''' Código del centro de atención
    ''' </summary>
    ''' <returns></returns>
    Public Property CareCenterCode As String
        Get
            Return _careCenterCode
        End Get
        Set(value As String)
            _careCenterCode = value
        End Set
    End Property

    Private _patientCode As String
    ''' <summary>
    ''' Código del paciente
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

    Private _admissionRecord As ViewLiquidationGetAdmissionAll
    ''' <summary>
    ''' Obtiene el ingreso actual
    ''' </summary>
    ''' <returns></returns>
    Public Property AdmissionRecord As ViewLiquidationGetAdmissionAll
        Get
            Return _admissionRecord
        End Get
        Set(value As ViewLiquidationGetAdmissionAll)
            _admissionRecord = value
        End Set
    End Property

    Private _isCurrentAdmission As Boolean
    ''' <summary>
    ''' Permite saber si se esta trabajando con el ingreso actual
    ''' </summary>
    ''' <returns></returns>
    Public Property IsCurrentAdmission As Boolean
        Get
            Return _isCurrentAdmission
        End Get
        Set(value As Boolean)
            _isCurrentAdmission = value
        End Set
    End Property

    ''' <summary>
    ''' Asigna los controles como readOnly
    ''' </summary>
    Private WriteOnly Property SetReadOnlyControls() As Boolean
        Set(value As Boolean)
            INDsleContract.ReadOnly = value
            INDsleHealthAdministrator.ReadOnly = value
            If value Then 'Se oculta el layout del nuevo ingreso porque se esta trabajando con el ingreso actual
                INDlyItemNewAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else 'Se muestra el layout del nuevo ingreso
                INDlyItemNewAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End Set
    End Property

    Private _listDetail As ConcurrentBag(Of Object)
    ''' <summary>
    ''' Listado de detalles que vienen desde el form principal, en este modal se filtra para los que apliquen a rias
    ''' </summary>
    ''' <returns></returns>
    Public Property ListDetail As ConcurrentBag(Of Object)
        Get
            Return _listDetail
        End Get
        Set(value As ConcurrentBag(Of Object))
            _listDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' propiedad que obtiene o establece la Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <returns></returns>
    Property EntryRoutesHealthServicesId As Integer?
        Get
            Return INDsleEntryRoutesHealthServices.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntryRoutesHealthServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la Finalidades tecnologías de la salud
    ''' </summary>
    ''' <returns></returns>
    Property HealthPurposesId As Integer?
        Get
            Return INDsleHealthPurposes.EditValue
        End Get
        Set(value As Integer?)
            INDsleHealthPurposes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la Modalidades de Atención
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionModalitiesId As Integer?
        Get
            Return INDsleAdmissionModalities.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdmissionModalities.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de Ingresa por
    ''' </summary>
    ''' <returns></returns>
    Property EntryRoutesHealthServicesDatasource As XPInstantFeedbackSource
        Get
            Return INDsleEntryRoutesHealthServices.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntryRoutesHealthServices.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de finalidad
    ''' </summary>
    ''' <returns></returns>
    Property HealthPurposesDatasource As XPInstantFeedbackSource
        Get
            Return INDsleHealthPurposes.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthPurposes.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de Modalidad de atención
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionModalitiesDatasource As XPInstantFeedbackSource
        Get
            Return INDsleAdmissionModalities.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdmissionModalities.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "PublicEvents"

    ''' <summary>
    ''' Evento para controlar el retorno al proceso principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event ReturnToProcess(sender As Object, e As ReturnToProcessEventArgs)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PAccountControlAmbulatory

    ''' <summary>
    ''' Lista de tupla si o no
    ''' </summary>
    Dim ListYesNo As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Lista de tupla de tipo de riesgo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRiskType As List(Of Tuple(Of String, String))

    ''' <summary>
    ''' Lista de tupla de causas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCause As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que me establece si controlo el cambio de valor en el search
    ''' </summary>
    Dim ControlEditValueChanged As Boolean = False

    ''' <summary>
    ''' Código de la unidad funcional, se asigna al momento de cambiar el valor del control de unidad funcional
    ''' </summary>
    Dim FunctionalUnitCode As String

    ''' <summary>
    ''' Variable que define si el grupo de atención aplica a RIAS
    ''' </summary>
    Dim ApplyRIASCaregroup As Boolean

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga el datasource del search que maneja tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListYesNo = New List(Of Tuple(Of Boolean, String))
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleApplyRias.Properties.DataSource = ListYesNo.ToList()

        ListRiskType = New List(Of Tuple(Of String, String))
        ListRiskType.Add(New Tuple(Of String, String)("1", "Enfermedad General y Maternidad"))
        ListRiskType.Add(New Tuple(Of String, String)("2", "Accidente de Tránsito"))
        ListRiskType.Add(New Tuple(Of String, String)("3", "Catástrofe"))
        ListRiskType.Add(New Tuple(Of String, String)("5", "Accidente de Trabajo"))
        ListRiskType.Add(New Tuple(Of String, String)("6", "Enfermedad Profesional"))
        ListRiskType.Add(New Tuple(Of String, String)("7", "Atención Inicial de Urgencias"))
        ListRiskType.Add(New Tuple(Of String, String)("8", "Otro Tipo de Accidente"))
        ListRiskType.Add(New Tuple(Of String, String)("9", "Lesión Por Agresión"))
        ListRiskType.Add(New Tuple(Of String, String)("10", "Lesión AutoInfligida"))
        ListRiskType.Add(New Tuple(Of String, String)("11", "Maltrato Fisico"))
        ListRiskType.Add(New Tuple(Of String, String)("12", "Promoción y Prevención"))
        ListRiskType.Add(New Tuple(Of String, String)("13", "Otro"))
        ListRiskType.Add(New Tuple(Of String, String)("14", "Accidente Rábico"))
        ListRiskType.Add(New Tuple(Of String, String)("15", "Accidente Ofídico"))
        ListRiskType.Add(New Tuple(Of String, String)("16", "Sopecha de Abuso Sexual"))
        ListRiskType.Add(New Tuple(Of String, String)("17", "Sopecha de Violencia Sexual"))
        ListRiskType.Add(New Tuple(Of String, String)("18", "Sopecha de Maltrato Emocional"))
        INDsleRiskType.Properties.DataSource = ListRiskType.ToList()

        ListCause = New List(Of Tuple(Of Integer, String))
        ListCause.Add(New Tuple(Of Integer, String)(1, "Heridos en Combate"))
        ListCause.Add(New Tuple(Of Integer, String)(2, "Enfermedad Profesional"))
        ListCause.Add(New Tuple(Of Integer, String)(3, "Enfermedad General Adulto"))
        ListCause.Add(New Tuple(Of Integer, String)(4, "Enfermedad General Pediatría"))
        ListCause.Add(New Tuple(Of Integer, String)(5, "Odontología"))
        ListCause.Add(New Tuple(Of Integer, String)(6, "Accidente de Tránsito"))
        ListCause.Add(New Tuple(Of Integer, String)(7, "Evento Catastrófico"))
        ListCause.Add(New Tuple(Of Integer, String)(8, "Quemados"))
        ListCause.Add(New Tuple(Of Integer, String)(9, "Maternidad"))
        ListCause.Add(New Tuple(Of Integer, String)(10, "Accidente Laboral"))
        ListCause.Add(New Tuple(Of Integer, String)(11, "Cirugía Programada"))
        INDsleCause.Properties.DataSource = ListCause.ToList()
    End Sub

    ''' <summary>
    ''' Metodo que asigna los valores a los controles
    ''' </summary>
    Private Sub SetValuesControls()
        'Se asignan los valores en los controles del ingreso actual, si se esta trabajando con el ingreso actual los controles van bloqueados y si es con un ingreso nuevo se pueden modificar
        INDtxtPatient.EditValue = _admissionRecord.PatientCode.ToString().Trim() + " - " + _admissionRecord.PatientName.ToString().Trim()
        INDtxtAdmission.EditValue = _admissionRecord.AdmissionCode.ToString().Trim()

        'Se asigna el centro de atención del ingreso
        INDsleCareCenter.EditValue = _admissionRecord.AdmissionCentAtencCodeName.Split(" - ")(0).Trim()
        INDsleCareCenter.Properties.NullText = _admissionRecord.AdmissionCentAtencCodeName

        'Se asigna la unidad funcional del ingreso
        Dim infoFunctionalUnitXpo = Presenter.GetFunctionalUnitByCode(_admissionRecord.AdmissionUniFuncCodeName.Split(" - ")(0).Trim())
        If infoFunctionalUnitXpo IsNot Nothing Then
            INDsleFunctionalUnit.EditValue = infoFunctionalUnitXpo.Id
            INDsleFunctionalUnit.Properties.NullText = _admissionRecord.AdmissionUniFuncCodeName
        End If

        ApplyRIASCaregroup = _admissionRecord.AdmissionCareGroupApplyRIAS
        ControlEditValueChanged = True
        INDsleCareGroup.EditValue = _admissionRecord.AdmissionCaregroupId
        INDsleCareGroup.Properties.NullText = _admissionRecord.AdmissionCareGroupCodeName
        ControlEditValueChanged = False

        'Si el grupo de atención que tiene el ingreso tiene asociado un contrato
        If _admissionRecord.ContractId IsNot Nothing AndAlso CInt(_admissionRecord.ContractId) > 0 Then
            INDlyItemContract.HideControl(False)
            ControlEditValueChanged = True
            INDsleContract.EditValue = _admissionRecord.ContractId
            INDsleContract.Properties.NullText = _admissionRecord.CareGroupContractCodeName
            ControlEditValueChanged = False
        End If

        'Si el ingreso tiene asociado una entidad administradora de salud
        If _admissionRecord.HealthAdministratorId IsNot Nothing AndAlso CInt(_admissionRecord.HealthAdministratorId) > 0 Then
            INDlyItemHealthAdministrator.HideControl(False)
            INDsleHealthAdministrator.EditValue = _admissionRecord.HealthAdministratorId
            INDsleHealthAdministrator.Properties.NullText = _admissionRecord.AdmissionHealthAdministratorCodeName
        End If

        'Si el listado de detalles principal tiene cups que apliquen a rias se asigna a la rejilla siempre y cuando el grupo de atención del ingreso aplique a rias
        INDgcCupsRias.DataSource = Nothing
        INDlygRIAS.HideControl()
        If _listDetail IsNot Nothing AndAlso _listDetail.Count > 0 AndAlso _admissionRecord.AdmissionCareGroupApplyRIAS Then
            'Se filtra primero por los cups que apliquen a rias, despues se agrupa por cups(porque pueden venir repetidos con diferente procedimiento en el mismo folio)
            Dim listGroup = (From x In _listDetail Where x.ApplyRIAS = True
                             Group x By CupsEntityCode = x.CupsEntityCode, CupsDescription = x.CupsDescription, FolioNumber = x.FolioNumber, RegisterDate = x.Date,
                                        RiasCupsId = x.RiasCupsId, RiasDescription = x.RiasDescription
                                     Into Quantity = Sum(CDec(x.Quantity))
                             Select New InfoEntity(CupsEntityCode, CupsDescription, Quantity, FolioNumber, RegisterDate, RiasCupsId, RiasDescription)).ToList()

            If listGroup IsNot Nothing AndAlso listGroup.Count > 0 Then
                INDlygRIAS.HideControl(False)
                INDgcCupsRias.DataSource = listGroup
            End If
        End If

        'Si se esta trabajando con el ingreso actual
        If _isCurrentAdmission Then
            'Se asigna readOnly = true a los controles ya que se esta trabajando con el ingreso actual y no se modifica nd de la información
            SetReadOnlyControls = True

            'Si se esta generando la orden de servicio con el ingreso actual, se bloquea el control de centro de atención
            INDsleCareCenter.Properties.ReadOnly = True
        Else 'Si se esta trabajando con un ingreso nuevo
            'Se asigna readOnly = false a los controles ya que se esta trabajando con un ingreso nuevo y si se puede modificar la información
            SetReadOnlyControls = False

            'Si se esta generando la orden de servicio con un ingreso nuevo, se permite cambiar el centro de atención
            INDsleCareCenter.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidationControls() As String
        Dim errors As New StringBuilder

        If INDsleCareCenter.EditValue Is Nothing OrElse INDsleCareCenter.EditValue = 0 Then
            errors.AppendLine("Debe seleccionar un centro de atención")
        End If

        If INDsleFunctionalUnit.EditValue Is Nothing OrElse INDsleFunctionalUnit.EditValue = 0 Then
            errors.AppendLine("Debe seleccionar una unidad funcional")
        End If

        If INDsleCareGroup.EditValue Is Nothing OrElse INDsleCareGroup.EditValue = 0 Then
            errors.AppendLine("Debe seleccionar un grupo de atención")
        End If

        If INDlyItemContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleContract.EditValue Is Nothing OrElse INDsleContract.EditValue = 0 Then
                errors.AppendLine("Debe seleccionar un contrato")
            End If
        End If

        If INDlyItemHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleHealthAdministrator.EditValue Is Nothing OrElse INDsleHealthAdministrator.EditValue = 0 Then
                errors.AppendLine("Debe seleccionar una entidad administradora de salud")
            End If
        End If

        If INDlyItemNewAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleRiskType.EditValue Is Nothing OrElse INDsleRiskType.EditValue = 0 Then
                errors.AppendLine("Debe seleccionar un tipo de riesgo")
            End If

            If INDlyItemValueSent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDtxtValueSent.EditValue Is Nothing OrElse INDtxtValueSent.EditValue = 0 Then
                    errors.AppendLine("Debe ingresar un valor remitido")
                End If
            End If

            If INDlyItemSMLV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleSMLV.EditValue Is Nothing OrElse INDsleSMLV.EditValue = String.Empty Then
                    errors.AppendLine("Debe seleccionar un SMLV")
                End If
            End If

            If INDsleCause.EditValue Is Nothing OrElse INDsleCause.EditValue = 0 Then
                errors.AppendLine("Debe seleccionar una causa")
            End If

            If String.IsNullOrEmpty(RTrim(LTrim(INDtxtAuthorizationNumberAdmission.EditValue))) AndAlso INDlyItemAuthorization.AllowHide = False Then
                errors.AppendLine("Debe ingresar un No. de autorización")
            End If

            If indigo.LanguageCulture = "es-CO" Then
                If EntryRoutesHealthServicesId Is Nothing Then
                    errors.AppendLine("Debe seleccionar Ingresa por")
                End If
                If HealthPurposesId Is Nothing Then
                    errors.AppendLine("Debe seleccionar una Finalidad")
                End If
                If AdmissionModalitiesId Is Nothing Then
                    errors.AppendLine("Debe seleccionar una Modalidad de Atención")
                End If

            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Configura la visibilidad de los controles relacionados con las rutas de ingreso, finalidades de salud, y modalidades de atención 
    ''' en función de la cultura del sistema.
    ''' Muestra estos controles si la cultura es "es-CO" (Colombia) y los oculta para otras culturas.
    ''' </summary>
    Private Sub ConfigureControlsVisibilityBasedOnCulture()
        If indigo.LanguageCulture = "es-CO" Then
            INDlyItemEntryRoutesHealthServices.HideControl(False)
            INDlyItemHealthPurposes.HideControl(False)
            INDlyItemAdmissionModalities.HideControl(False)
        Else
            INDlyItemEntryRoutesHealthServices.HideControl()
            INDlyItemHealthPurposes.HideControl()
            INDlyItemAdmissionModalities.HideControl()
        End If
    End Sub
#End Region

#Region "Hanlders"

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que abre el form de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(985, Nothing, True)
            INDsleCareGroup.Properties.DataSource = Presenter.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContract.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(978, Nothing, True)
            INDsleContract.Properties.DataSource = Presenter.ListContractXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de entidad administradora de salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleHealthAdministrator.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(972, Nothing, True)
            INDsleHealthAdministrator.Properties.DataSource = Presenter.ListHealthAdministratorByStatus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCareCenter.Properties.DataSource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                INDsleCareCenter.Properties.DataSource = model.ListCentersHIS()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFunctionalUnit.QueryPopUp
        If INDsleFunctionalUnit.Properties.DataSource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                INDsleFunctionalUnit.Properties.DataSource = model.ListFunctionalUnitCareCenter(INDsleCareCenter.EditValue)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareGroup.QueryPopUp
        If INDsleCareGroup.Properties.DataSource Is Nothing Then
            INDsleCareGroup.Properties.DataSource = Presenter.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de contratos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContract.QueryPopUp
        If INDsleContract.Properties.DataSource Is Nothing Then
            INDsleContract.Properties.DataSource = Presenter.ListContractXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad administradora de salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.DataSource Is Nothing Then
            INDsleHealthAdministrator.Properties.DataSource = Presenter.ListHealthAdministratorByStatus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIAS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIAS.QueryPopUp
        If INDsleRIAS.Properties.DataSource Is Nothing Then
            Dim CupsCodeFocus As String = CType(INDviewCupsRias.GetFocusedRow, InfoEntity).CupsEntityCode
            INDsleRIAS.Properties.DataSource = Presenter.ListRIASCups(CupsCodeFocus)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el repositorio de la rejilla para rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPceRIAS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceRIAS.QueryPopUp
        Dim info As InfoEntity = INDviewCupsRias.GetFocusedRow()
        If info IsNot Nothing Then
            ControlEditValueChanged = True
            INDsleApplyRias.EditValue = info.ApplyRIAS
            INDsleRIAS.Properties.DataSource = Nothing
            INDsleRIAS.EditValue = info.RiasCupsId
            INDsleRIAS.Properties.NullText = info.RiasDescription
            ControlEditValueChanged = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de salario minimo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSMLV_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSMLV.QueryPopUp
        If INDsleSMLV.Properties.DataSource Is Nothing Then
            INDsleSMLV.Properties.DataSource = Presenter.ListAllMinWage()
        End If
    End Sub

    ''' <summary>
    ''' evento encargado de cargar el datasource de los Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdEntryRoutesHealthServices_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEntryRoutesHealthServices.QueryPopUp
        If Me.EntryRoutesHealthServicesDatasource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                Me.EntryRoutesHealthServicesDatasource = model.GetEntryRoutesHealthServices()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento encargado de cargar el datasource de los Finalidades tecnologías de la salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdHealthPurposes_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthPurposes.QueryPopUp
        If Me.HealthPurposesDatasource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                Me.HealthPurposesDatasource = model.GetHealthPurposes()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento encargado de cargar el datasource de los Modalidades de Atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdAdmissionModalities_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdmissionModalities.QueryPopUp
        If Me.AdmissionModalitiesDatasource Is Nothing Then
            Using model As New MControlOutpatientServices("")
                Me.AdmissionModalitiesDatasource = model.GetAdmissionModalities()
            End Using
        End If
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' Evento que se ejecuta al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRiasAndNewAdmission_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PAccountControlAmbulatory()
        ConfigureControlsVisibilityBasedOnCulture()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se ejecuta al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRiasAndNewAdmission_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        InitializeTuple()
        SetValuesControls()
        INDtxtPatient.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta al presionar aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        'Objeto que representa al nuevo ingreso
        Dim argsNewAdmission As Object = Nothing

        AsyncLoader(True)

        'Se valida los controles del form siempre y cuando se este trabajando con un ingreso nuevo
        If _isCurrentAdmission = False AndAlso INDlyItemNewAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Se valida los controles
            Dim validateControls = ValidationControls()
            If validateControls.Length > 0 Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = validateControls
                Exit Sub
            End If

            argsNewAdmission = New ExpandoObject()
            argsNewAdmission.IPCODPACI = _admissionRecord.PatientCode.Trim()
            argsNewAdmission.ITIPORIES = INDsleRiskType.EditValue
            argsNewAdmission.ICAUSAING = INDsleCause.EditValue
            argsNewAdmission.CODENTIDA = INDsleHealthAdministrator.EditValue 'Se asigna a esta variable el id de la entidad pero en el sp se realiza la logica para obtener el codigo
            argsNewAdmission.CODCENATE = INDsleCareCenter.EditValue
            argsNewAdmission.GENCAREGROUP = INDsleCareGroup.EditValue
            argsNewAdmission.GENCONENTITY = INDsleHealthAdministrator.EditValue
            argsNewAdmission.CODUSUCRE = indigo.UserIndigo
            argsNewAdmission.IOBSERVAC = INDmeObservations.EditValue
            argsNewAdmission.UFUCODIGO = FunctionalUnitCode
            argsNewAdmission.IAUTORIZA = INDtxtAuthorizationNumberAdmission.EditValue
            argsNewAdmission.IdEntryRoutesHealthServices = EntryRoutesHealthServicesId
            argsNewAdmission.IdHealthPurposes = HealthPurposesId
            argsNewAdmission.IdAdmissionModalities = AdmissionModalitiesId

            If INDlyItemValueSent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    argsNewAdmission.ISOATVALO = INDtxtValueSent.EditValue
                Else
                    argsNewAdmission.ISOATVALO = 0
                End If

                If INDlyItemSMLV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    argsNewAdmission.ISALCODIG = INDsleSMLV.EditValue
                Else
                    argsNewAdmission.ISALCODIG = String.Empty
                End If
            End If

            'Se valida si esta llena la información necesaria en la rejilla y que el grupo de RIAS este visible, siempre y cuando hayan cups que manejen rias
            If CType(INDgcCupsRias.DataSource, List(Of InfoEntity)) IsNot Nothing AndAlso CType(INDgcCupsRias.DataSource, List(Of InfoEntity)).Count > 0 _
            AndAlso INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Errores que se presentan en la rejilla
            Dim errors As New StringBuilder

            'Listado de parámetros para enviar a validar las rias
            Dim listParameters As New List(Of Tuple(Of String, Integer, String, Integer, DateTime))

            'Se recorre el listado de la rejilla para verificar si cada cups aplica a rias y si aplica que hayan seleccionado para cada una una rias
            Parallel.ForEach(CType(INDgcCupsRias.DataSource, List(Of InfoEntity)),
                             Sub(obj As InfoEntity)
                                 If obj.ApplyRIAS = True AndAlso obj.RiasCupsId = 0 Then
                                     errors.AppendLine("El cups " + obj.CupsEntityCode.ToString().Trim() + " aplica a RIAS pero no ha seleccionado una")
                                 End If

                                 'Se establece el listado que se va a enviar al método de validación de rias
                                 If obj.ApplyRIAS = True AndAlso obj.RiasCupsId > 0 Then
                                     listParameters.Add(New Tuple(Of String, Integer, String, Integer, DateTime)(_admissionRecord.PatientCode.Trim(), obj.RiasCupsId, obj.CupsEntityCode.Trim(), obj.Quantity, obj.RegisterDate))
                                 End If
                             End Sub)

            'Si hay errores se retorna y no se deja avanzar
            If errors.ToString().Length > 0 Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If

            'Se envia el listado de parámetros para poder validar las rias
            If listParameters IsNot Nothing AndAlso listParameters.Count > 0 Then
                Using srvOrderModel As New MServiceOrder("")
                    'Se envia las rias al sp para validar si se pueden agregar
                    Dim resultValidateRIAS As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) = Await srvOrderModel.SP_RIAS_ValidacionCUPSRIAS(listParameters)
                    If resultValidateRIAS.StateResult = False Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = resultValidateRIAS.Message
                        Exit Sub
                    End If
                End Using
            End If
        End If

        'Diccionario para las rias
        Dim _dictionaryViewRiasCups As Dictionary(Of Integer, ViewRIASCupsXpo) = New Dictionary(Of Integer, ViewRIASCupsXpo)

        'Se asigna la unidad funcional a los detalles
        If _listDetail IsNot Nothing AndAlso _listDetail.Count > 0 Then
            For Each item In _listDetail
                item.FunctionalUnitCode = FunctionalUnitCode
                item.CareCenterCode = INDsleCareCenter.EditValue

                'Se asignan los valores de RIAS
                item.ApplyRIAS = False
                item.RiasCupsId = 0
                item.RiasId = 0

                'Si el grupo de RIAS esta visible
                If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    'Se obtiene el registro al cual se le aplico la RIAS
                    Dim info = (From x In CType(INDgcCupsRias.DataSource, List(Of InfoEntity)) Where x.CupsEntityCode = item.CupsEntityCode AndAlso x.FolioNumber = item.FolioNumber).FirstOrDefault()

                    'Si encontro registro se asignan los valores
                    If info IsNot Nothing Then
                        item.ApplyRIAS = info.ApplyRIAS
                        If info.ApplyRIAS Then
                            item.RiasCupsId = info.RiasCupsId

                            Dim infoViewRiasCups As ViewRIASCupsXpo = Nothing
                            If _dictionaryViewRiasCups.ContainsKey(item.RiasCupsId) Then
                                infoViewRiasCups = _dictionaryViewRiasCups(item.RiasCupsId)
                            Else
                                infoViewRiasCups = Presenter.GetViewRIASCupsByRiasCupsId(item.RiasCupsId)
                                If infoViewRiasCups IsNot Nothing Then
                                    _dictionaryViewRiasCups.Add(item.RiasCupsId, infoViewRiasCups)
                                End If
                            End If

                            If infoViewRiasCups IsNot Nothing Then
                                item.RiasId = infoViewRiasCups.RiasId
                            End If
                        End If
                    End If
                End If
            Next
        End If

        'Se crea el nuevo objeto que se envia al form principal y que sirve para asignar la rias en el listado que se envia al servicio de orden de servicio
        Dim args As New ReturnToProcessEventArgs
        args.Details = _listDetail
        args.NewAdmission = argsNewAdmission
        args.CareGroupId = INDsleCareGroup.EditValue

        'Se valida si los cups manejan cotización
        Using model As New MAccountControl(Me.Tag)
            Dim messageQuoted = model.GetCUPSWithQuoted(args, args.CareGroupId)
            If Not String.IsNullOrEmpty(messageQuoted.Item1) Then
                If MessageIndigo.Show(messageQuoted.Item1, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    If Me._requestQuoteServices Then
                        AsyncLoader(False)
                        Exit Sub
                    End If
                Else
                    Me.Cursor = ChangeCursorIndigo()
                    Using Formulario As New FrmImportQuotation()
                        AddHandler Formulario.ImportEvent, AddressOf ImportInfo
                        Formulario.PatientCode = PatientCode.Trim()
                        Formulario.ListCupsEntityId = messageQuoted.Item2
                        Formulario.arg = args
                        Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                        Formulario.ViewModeEditHold = True
                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Formulario.Width = 1054
                        Formulario.Height = 500
                        Dim frm As New FrmTransparent(Formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        frm.ShowDialog(Me)

                        If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                            AsyncLoader(False)
                            Exit Sub
                        End If
                    End Using
                End If
            End If
        End Using

        AsyncLoader(False)
        RaiseEvent ReturnToProcess(Nothing, args)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al seleccionar la cotización
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddImportQuotationServiceOrderDetail)
        If e IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo.Count > 0 Then
            For Each itemArgs In e.arg.Details
                Dim quotationServiceOrderDetailXpo = (From x In e.ListQuotationServiceOrderDetailXpo Where x.CUPSEntityId.Code = itemArgs.CupsEntityCode.ToString().Trim()).FirstOrDefault()
                If quotationServiceOrderDetailXpo IsNot Nothing Then
                    itemArgs.QuotationServiceOrderDetailId = quotationServiceOrderDetailXpo.Id
                End If
            Next
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de centro de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareCenter.EditValueChanged
        If INDsleCareCenter.EditValue IsNot Nothing Then
            INDsleFunctionalUnit.Properties.DataSource = Nothing
            INDsleFunctionalUnit.EditValue = Nothing
            INDsleFunctionalUnit.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad funcional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFunctionalUnit.EditValueChanged
        If INDsleFunctionalUnit.EditValue IsNot Nothing Then
            Dim infoFunctionalUnitXpo = Presenter.GetFunctionalUnitById(INDsleFunctionalUnit.EditValue)
            If infoFunctionalUnitXpo IsNot Nothing Then
                FunctionalUnitCode = infoFunctionalUnitXpo.Code
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor de si aplica a RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleApplyRias_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyRias.EditValueChanged
        If INDsleApplyRias.EditValue Then
            INDlyItemRias.HideControl(False)
        Else
            INDlyItemRias.HideControl()
        End If
        Dim info As InfoEntity = INDviewCupsRias.GetFocusedRow()
        If info IsNot Nothing Then
            info.ApplyRIAS = INDsleApplyRias.EditValue
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIAS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRIAS.EditValueChanged
        If INDsleRIAS.EditValue IsNot Nothing AndAlso ControlEditValueChanged = False Then
            Dim info As InfoEntity = INDviewCupsRias.GetFocusedRow()
            If info IsNot Nothing Then
                info.RiasCupsId = INDsleRIAS.EditValue
                info.RiasDescription = INDsleRIAS.Text
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de tipo de riesgo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRiskType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRiskType.EditValueChanged
        If INDsleRiskType.EditValue IsNot Nothing Then
            INDlyItemValueSent.HideControl()
            INDlyItemSMLV.HideControl()

            Select Case INDsleRiskType.EditValue
                Case Is = "2" 'Accidente de Transito
                    INDlyItemValueSent.HideControl(False)
                    INDlyItemSMLV.HideControl(False)
                    INDtxtValueSent.EditValue = Nothing
                    INDsleSMLV.EditValue = Nothing
                    INDsleCause.EditValue = "6"
                Case Is = "3" 'Catastrofe
                    INDsleCause.EditValue = "7"
                Case Is = "4" 'Enfermedad General y Maternidad
                    INDsleCause.EditValue = "3"
                Case Is = "5" 'Accidente de trabajo
                    INDsleCause.EditValue = "10"
                Case Is = "6" 'Enfermedad Profesional
                    INDsleCause.EditValue = "2"
                Case Else
                    INDsleCause.EditValue = Nothing
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de grupo de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareGroup.EditValueChanged
        Dim careGroupXpo As Infrastructure.Data.Xpo.ContractRepository.ContractCareGroupReportXpo = Nothing
        If INDsleCareGroup.EditValue IsNot Nothing Then
            careGroupXpo = Presenter.GetCareGroupById(INDsleCareGroup.EditValue)

            INDlyItemAuthorization.AllowHide = True
            If careGroupXpo IsNot Nothing AndAlso careGroupXpo.AuthorizationRequired Then
                INDlyItemAuthorization.AllowHide = False
            End If
        End If
        If ControlEditValueChanged = False Then
            INDlyItemContract.HideControl(True)
            INDlyItemHealthAdministrator.HideControl(True)
            INDsleContract.EditValue = Nothing
            INDsleContract.Properties.NullText = String.Empty
            INDsleHealthAdministrator.EditValue = Nothing
            INDsleHealthAdministrator.Properties.NullText = String.Empty
            INDsleHealthAdministrator.Properties.ReadOnly = False

            If INDsleCareGroup.EditValue IsNot Nothing Then
                'Se asigna la variable para saber si el grupo de atención aplica a RIAS
                ApplyRIASCaregroup = careGroupXpo.ApplyRIAS

                'Si el grupo de atención aplica a RIAS y hay registros en la rejilla
                If ApplyRIASCaregroup AndAlso INDviewCupsRias.RowCount > 0 Then
                    INDlygRIAS.HideControl(False)
                Else
                    INDlygRIAS.HideControl()
                End If

                INDsleHealthAdministrator.Properties.DataSource = Nothing
                INDsleHealthAdministrator.Properties.NullText = String.Empty
                Select Case careGroupXpo.CareGroupType
                    Case Is = 1 'Con contrato (mostrat EAPB)
                        INDlyItemContract.HideControl(False)
                        INDsleContract.Properties.NullText = careGroupXpo.ContractId.CodeContractName
                        INDsleContract.EditValue = careGroupXpo.ContractId.Id
                        INDlyItemHealthAdministrator.HideControl(False)

                        INDsleHealthAdministrator.EditValue = careGroupXpo.ContractId.HealthAdministratorId.Id
                        INDsleHealthAdministrator.Properties.NullText = careGroupXpo.ContractId.HealthAdministratorId.CodeName
                        INDsleHealthAdministrator.Properties.ReadOnly = True
                    Case Is = 2 'Sin contrato (preguntar por EAPB diferente a aseguradoras)
                        INDlyItemHealthAdministrator.HideControl(False)
                    Case Is = 4 'Aseguradoras (preguntar por EAPB = aseguradoras)
                        INDlyItemHealthAdministrator.HideControl(False)
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleContract.EditValueChanged
        If INDsleContract.EditValue IsNot Nothing AndAlso ControlEditValueChanged = False Then
            Dim contractXpo = Presenter.GetContractById(INDsleContract.EditValue)
            INDsleHealthAdministrator.EditValue = contractXpo.HealthAdministratorId.Id
            INDsleHealthAdministrator.Properties.NullText = contractXpo.HealthAdministratorId.CodeName
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 en el control de datos de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAdmissionData_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceNewAdmissionData.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceNewAdmissionData.ShowPopup()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al realizar popup sobre el control de datos del ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAdmissionData_Popup(sender As Object, e As EventArgs) Handles INDpceNewAdmissionData.Popup
        INDsleRiskType.Focus()
    End Sub

#End Region

#End Region

End Class

Public Class ReturnToProcessEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Listado de detalles que viene desde el form principal y sirve para que cuando haya rias al momento de retornar se asigne el id de la rias al respectivo cups
    ''' </summary>
    ''' <returns></returns>
    Property Details As ConcurrentBag(Of Object)

    ''' <summary>
    ''' Objeto que representa a los campos que se van a enviar para poder realizar el ingreso nuevo
    ''' </summary>
    ''' <returns></returns>
    Property NewAdmission As Object

    ''' <summary>
    ''' Grupo de atención que es seleccionado en el search para poder enviar a generar la orden y el ingreso
    ''' </summary>
    ''' <returns></returns>
    Property CareGroupId As Integer

    ''' <summary>
    ''' No. de autorización
    ''' </summary>
    ''' <returns></returns>
    Property AuthorizationNumber As String

End Class

Public Class InfoEntity

    Sub New(_cupsCode As String, _cupsDescription As String, _quantity As Integer, _folioNumber As String, _registerDate As Date, _riasCupsId As Integer, _riasDescription As String)
        CupsEntityCode = _cupsCode
        CupsDescription = _cupsDescription
        Quantity = _quantity
        FolioNumber = _folioNumber
        RegisterDate = _registerDate
        RiasCupsId = _riasCupsId
        RiasDescription = _riasDescription

        ApplyRIAS = False
        If RiasCupsId > 0 Then
            ApplyRIAS = True
        End If
    End Sub

    ''' <summary>
    ''' Código del cups
    ''' </summary>
    ''' <returns></returns>
    Property CupsEntityCode As String

    ''' <summary>
    ''' Código y nombre del cups
    ''' </summary>
    ''' <returns></returns>
    Property CupsDescription As String

    ''' <summary>
    ''' Aplica a rias
    ''' </summary>
    ''' <returns></returns>
    Property ApplyRIAS As Boolean

    ''' <summary>
    ''' Id de la rias
    ''' </summary>
    ''' <returns></returns>
    Property RiasCupsId As Integer

    ''' <summary>
    ''' Código y nombre de la rias
    ''' </summary>
    ''' <returns></returns>
    Property RiasDescription As String

    ''' <summary>
    ''' Representa a la sumatoria de la cantidad de cada cups
    ''' </summary>
    ''' <returns></returns>
    Property Quantity As Integer

    ''' <summary>
    ''' Establece el numero del folio
    ''' </summary>
    ''' <returns></returns>
    Property FolioNumber As String

    ''' <summary>
    ''' Establece la fecha del servicio
    ''' </summary>
    ''' <returns></returns>
    Property RegisterDate As Date

End Class