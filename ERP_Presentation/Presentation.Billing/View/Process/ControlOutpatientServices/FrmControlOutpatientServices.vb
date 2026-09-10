'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 25-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : Replica de FrmPopUpServices
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Collections.Concurrent
Imports System.Dynamic
Imports System.Text
Imports System.Threading
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports DevExpress.XtraSplashScreen
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Billing.MVP
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Contract.MVP
Imports Presentation.Controls
#End Region

Public Class FrmControlOutpatientServices
    Implements IControlOutpatientServices

#Region "BUILDER"
    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        ctrInfo = New CtrInfoControlOutpatientServices()
        ctrInfo.SetInfoFunction(AddressOf getInfo)
        ctrInfo.PrintInfo()
        ctrInfo.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrInfo)
        _isloading = False
    End Sub
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Valor anterior del ingreso
    ''' </summary>
    Private previusAdmissionOption As Byte
    ''' <summary>
    ''' Peso del paciente
    ''' </summary>
    Private _pesopaciente As Integer?
    Public Property PESOPACIENTE As Integer?
        Get
            Return _pesopaciente
        End Get
        Set(value As Integer?)
            If value Is Nothing Then
                value = 0
            End If
            Me.BarraBotones.WeightPatient(value, patient.IPCODPACI, patient.IPFECNACI)
            _pesopaciente = value
        End Set
    End Property

    Private _payrollFunctionalUnitXpo As Infrastructure.Data.Xpo.PayrollRepository.PayrollViewFunctionalUnitCareCenterUserXpo
    Private _centAtencionXpo As CentersXpo
    ''' <summary>
    ''' bandera usada para saber si se interrumpió el agregar una cita
    ''' </summary>
    Private _stopAggregateCita As Boolean
    Public ReadOnly Property AuthorizationNumber As String
        Get
            Return If(String.IsNullOrEmpty(INDAuthorizationNumberAdmission.Text), INDTxtAuthorizationNumber.Text, INDAuthorizationNumberAdmission.Text)
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IControlOutpatientServices.ActionsOnControls
        Set(value As Boolean)
            INDSleCareCenter.Enabled = Not value
            INDSleFunctionalUnit.Enabled = value

            If value Then
                INDSleFunctionalUnit.Focus()
            Else
                INDSleCareCenter.Focus()
            End If
        End Set
    End Property

    Public WriteOnly Property ActionsOnControlsIngress As Boolean
        Set(value As Boolean)
            INDLcControlOutpatientServices.BeginUpdate()
            INDBePatient.Properties.ReadOnly = value
            INDSleHealthAdministrator.Enabled = value
            INDSleCareGroup.Enabled = value
            INDPceAdmissionData.Enabled = value
            INDGleDispatched.Enabled = value
            INDPceMedicalAppointment.Enabled = value
            INDGcMedicalAppointment.Enabled = value
            INDBtnAddService.Enabled = value
            INDGcServiceOrderDetail.Enabled = value
            INDSleRequestHemoReserve.Enabled = value
            INDLcControlOutpatientServices.EndUpdate()
            If value Then
                INDSleCareGroup.Focus()
            Else
                INDBePatient.Focus()
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IControlOutpatientServices.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IControlOutpatientServices.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' id del centro de atencion
    ''' </summary>
    Public Property CareCenterCode As String Implements IControlOutpatientServices.CareCenterId
        Get
            Return INDSleCareCenter.EditValue
        End Get
        Set(value As String)
            INDSleCareCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' id de la unidad funcional
    ''' </summary>
    Public Property FunctionalUnitId As Integer? Implements IControlOutpatientServices.FunctionalUnitId
        Get
            Return INDSleFunctionalUnit.EditValue
        End Get
        Set(value As Integer?)
            INDSleFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de actividad
    ''' </summary>
    Public Property ActivityType As Integer Implements IControlOutpatientServices.ActivityType
        Get
            Return INDGleActivityType.EditValue
        End Get
        Set(value As Integer)
            INDGleActivityType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establcer el datasource de centros de atencion
    ''' </summary>
    Public Property CareCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IControlOutpatientServices.CareCenterXPO
        Get
            Return CType(INDSleCareCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCareCenter.Properties.DataSource = value
        End Set
    End Property

    Public Property ContractDatasourceXpo As XPInstantFeedbackSource
        Get
            Return CType(INDsleContract.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleContract.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer el data source de uniddades fucnionales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FunctionalUnitXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.FunctionalUnitXPO
        Get
            Return CType(INDSleFunctionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleFunctionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' id de la entidad administrdora de salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthAdministratorId As Integer? Implements IControlOutpatientServices.HealthAdministratorId
        Get
            Return INDSleHealthAdministrator.EditValue
        End Get
        Set(value As Integer?)
            INDSleHealthAdministrator.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource para la entidad aministradora de salud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HealtAdministratorXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.HealtAdministratorXPO
        Get
            Return CType(INDSleHealthAdministrator.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleHealthAdministrator.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' id del centro grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CareGroupId As Integer? Implements IControlOutpatientServices.CareGroupId
        Get
            Return INDSleCareGroup.EditValue
        End Get
        Set(value As Integer?)
            INDSleCareGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' datasource del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CareGroupXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.CareGroupXPO
        Get
            Return CType(INDSleCareGroup.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' remitido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Dispatched As Boolean Implements IControlOutpatientServices.Dispatched
        Get
            Return INDGleDispatched.EditValue
        End Get
        Set(value As Boolean)
            INDGleDispatched.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de riesgo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listRiskType As List(Of Tuple(Of String, String))
    ReadOnly Property ListRiskType As List(Of Tuple(Of String, String))
        Get
            If _listRiskType Is Nothing Then
                _listRiskType = New List(Of Tuple(Of String, String))
                _listRiskType.Add(New Tuple(Of String, String)("1", "Enfermedad General y Maternidad"))
                _listRiskType.Add(New Tuple(Of String, String)("2", "Accidente de Tránsito"))
                _listRiskType.Add(New Tuple(Of String, String)("3", "Catástrofe"))
                _listRiskType.Add(New Tuple(Of String, String)("5", "Accidente de Trabajo"))
                _listRiskType.Add(New Tuple(Of String, String)("6", "Enfermedad Profesional"))
                _listRiskType.Add(New Tuple(Of String, String)("7", "Atención Inicial de Urgencias"))
                _listRiskType.Add(New Tuple(Of String, String)("8", "Otro Tipo de Accidente"))
                _listRiskType.Add(New Tuple(Of String, String)("9", "Lesión Por Agresión"))
                _listRiskType.Add(New Tuple(Of String, String)("10", "Lesión AutoInfligida"))
                _listRiskType.Add(New Tuple(Of String, String)("11", "Maltrato Fisico"))
                _listRiskType.Add(New Tuple(Of String, String)("12", "Promoción y Prevención"))
                _listRiskType.Add(New Tuple(Of String, String)("13", "Otro"))
                _listRiskType.Add(New Tuple(Of String, String)("14", "Accidente Rábico"))
                _listRiskType.Add(New Tuple(Of String, String)("15", "Accidente Ofídico"))
                _listRiskType.Add(New Tuple(Of String, String)("16", "Sopecha de Abuso Sexual"))
                _listRiskType.Add(New Tuple(Of String, String)("17", "Sopecha de Violencia Sexual"))
                _listRiskType.Add(New Tuple(Of String, String)("18", "Sopecha de Maltrato Emocional"))
            End If
            Return _listRiskType
        End Get
    End Property

    ''' <summary>
    ''' tipo de riesgo
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listEspecial As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListEspecial As List(Of Tuple(Of Integer, String))
        Get
            If _listEspecial Is Nothing Then
                _listEspecial = New List(Of Tuple(Of Integer, String))
                _listEspecial.Add(New Tuple(Of Integer, String)(2, "Renal"))
                _listEspecial.Add(New Tuple(Of Integer, String)(3, "Oncología"))
            End If
            Return _listEspecial
        End Get
    End Property

    Dim _listAdmissionOption As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListAdmissionOption As List(Of Tuple(Of Byte, String))
        Get
            If _listAdmissionOption Is Nothing Then
                _listAdmissionOption = New List(Of Tuple(Of Byte, String))()
                _listAdmissionOption.Add(New Tuple(Of Byte, String)(1, "Nuevo Ingreso"))
                _listAdmissionOption.Add(New Tuple(Of Byte, String)(2, "Ingreso Existente"))
            End If
            Return _listAdmissionOption
        End Get
    End Property

    Dim _listYesNo As List(Of Tuple(Of Boolean, String))
    ReadOnly Property ListYesNo As List(Of Tuple(Of Boolean, String))
        Get
            If _listYesNo Is Nothing Then
                _listYesNo = New List(Of Tuple(Of Boolean, String))()
                _listYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _listYesNo
        End Get
    End Property

    ''' <summary>
    ''' datasource de tipos de citas
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listMedicalAppointmentType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListMedicalAppointmentType As List(Of Tuple(Of Integer, String))
        Get
            If _listMedicalAppointmentType Is Nothing Then
                _listMedicalAppointmentType = New List(Of Tuple(Of Integer, String))
                _listMedicalAppointmentType.Add(New Tuple(Of Integer, String)(1, "Primera Vez / Inicio Tratamiento"))
                _listMedicalAppointmentType.Add(New Tuple(Of Integer, String)(2, "Control / Continuidad Tratamiento"))
                _listMedicalAppointmentType.Add(New Tuple(Of Integer, String)(3, "Pos Operatorio"))
            End If
            Return _listMedicalAppointmentType
        End Get
    End Property

    ''' <summary>
    ''' datasource Tipo de Actividad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listActivityType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListActivityType As List(Of Tuple(Of Integer, String))
        Get
            If _listActivityType Is Nothing Then
                _listActivityType = New List(Of Tuple(Of Integer, String))
                _listActivityType.Add(New Tuple(Of Integer, String)(1, "Otras Citas"))
                _listActivityType.Add(New Tuple(Of Integer, String)(2, "Apoyo Diagnóstico y Terapéutico"))
            End If
            Return _listActivityType
        End Get
    End Property

    ''' <summary>
    ''' datasource Tipo de Actividad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listServiceType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListServiceType As List(Of Tuple(Of Integer, String))
        Get
            If _listServiceType Is Nothing Then
                _listServiceType = New List(Of Tuple(Of Integer, String))
                _listServiceType.Add(New Tuple(Of Integer, String)(1, "Laboratorios"))
                _listServiceType.Add(New Tuple(Of Integer, String)(2, "Imágenes Diagnósticas"))
                _listServiceType.Add(New Tuple(Of Integer, String)(3, "Otros Procedimientos"))
            End If
            Return _listServiceType
        End Get
    End Property

    ''' <summary>
    ''' datasource del salario minimo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SMLVXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.SMLVXPO
        Get
            Return CType(INDSleSMLV.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleSMLV.Properties.DataSource = value
        End Set
    End Property

    Private Property HealthProfessionalXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleHealthProfessional.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleHealthProfessional.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource para los CUPS de crystal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CUPSCrystalXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCUPSCrystal.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCUPSCrystal.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' datasource para las ips de crystal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property IPSCrystalXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleIPS.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleIPS.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' datasource para los municipios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property TownXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleTown.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleTown.Properties.DataSource = value
        End Set
    End Property

    Private _listaCitasMedicas As List(Of SP_AD_ListarCitasMedicasNativo_Result)
    Public Property ListCitasMedicas As List(Of SP_AD_ListarCitasMedicasNativo_Result)
        Get
            Return _listaCitasMedicas
        End Get
        Set(value As List(Of SP_AD_ListarCitasMedicasNativo_Result))
            _listaCitasMedicas = value
            INDGcMedicalAppointment.DataSource = value
            INDGcMedicalAppointment.RefreshDataSource()
        End Set
    End Property

    Private _listServiceOrderDetail As List(Of ServiceOrderDetail)
    Public Property ListServiceOrderDetail As List(Of ServiceOrderDetail)
        Get
            Return _listServiceOrderDetail
        End Get
        Set(value As List(Of ServiceOrderDetail))
            _listServiceOrderDetail = value
            INDGcServiceOrderDetail.DataSource = value
            INDGcServiceOrderDetail.RefreshDataSource()
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestHemoReserve As Integer? Implements IControlOutpatientServices.RequestHemoReserve
        Get
            Return INDSleRequestHemoReserve.EditValue
        End Get
        Set(value As Integer?)
            INDSleRequestHemoReserve.EditValue = value
        End Set
    End Property

    Dim _listRequestHemoReserve As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListRequestHemoReserve As List(Of Tuple(Of Integer, String))
        Get
            If _listRequestHemoReserve Is Nothing Then
                _listRequestHemoReserve = New List(Of Tuple(Of Integer, String))
                _listRequestHemoReserve.Add(New Tuple(Of Integer, String)(0, "No"))
                _listRequestHemoReserve.Add(New Tuple(Of Integer, String)(1, "Si"))
            End If
            Return _listRequestHemoReserve
        End Get
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property HemocomponentId As Integer? Implements IControlOutpatientServices.HemocomponentId
        Get
            Return INDSleHemo.EditValue
        End Get
        Set(value As Integer?)
            INDSleHemo.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ProfessionalId As String Implements IControlOutpatientServices.ProfessionalId
        Get
            Return INDSleProfessional.EditValue
        End Get
        Set(value As String)
            INDSleProfessional.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property HemoQuantity As Integer Implements IControlOutpatientServices.HemoQuantity
        Get
            Return INDSeHemoQuantity.EditValue
        End Get
        Set(value As Integer)
            INDSeHemoQuantity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Property HemocomponentXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.HemocomponentXPO
        Get
            Return CType(INDSleHemo.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleHemo.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Property ProfessionalXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.ProfessionalXPO
        Get
            Return CType(INDSleProfessional.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleProfessional.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Private _SpecialityCode As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property SpecialityCode As String
        Get
            Return _SpecialityCode
        End Get
        Set(value As String)
            _SpecialityCode = value
        End Set
    End Property

    Public Property ListReserveHemocomponentDetail As List(Of HCCOMSANDXpo)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ListHemocomponent As List(Of Hemocomponent)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Hemocomponente As Hemocomponent
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property ObjAdparametXpo As ADPARAMETXpo

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteOutpatientServices As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteOutpatientServices)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteIntrahospitalServices As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteIntrahospitalServices)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteServices As Boolean
        Get
            Dim result As Boolean
            If Me.admission IsNot Nothing Then
                If Me.admission.AdmissionType = 1 Then
                    result = Me.RequestQuoteOutpatientServices
                Else
                    result = Me.RequestQuoteIntrahospitalServices
                End If
            End If
            Return result
        End Get
    End Property

    ''' <summary>
    ''' Lista de citas de hemocomponentes
    ''' </summary>
    ''' <returns></returns>
    Public Property ListMedicalAppointmentHemo As List(Of AGASICITAXpo)
        Get
            Return CType(INDGcMedicalAppointmentHemo.DataSource, List(Of AGASICITAXpo))
        End Get
        Set(value As List(Of AGASICITAXpo))
            INDGcMedicalAppointmentHemo.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de ordenes xtramural de hemocomponentes
    ''' </summary>
    ''' <returns></returns>
    Public Property ListOrdersExtramuralHemo As List(Of HCORHEMCOXpo)
        Get
            Return CType(INDGcAmbulatoryOrders.DataSource, List(Of HCORHEMCOXpo))
        End Get
        Set(value As List(Of HCORHEMCOXpo))
            INDGcAmbulatoryOrders.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece u obtiene si el sistema es o no impuestos incluidos
    ''' </summary>
    Public Property TaxInclude As Boolean Implements IControlOutpatientServices.FlagTaxInclude
        Get
            Return Me._flagTaxInclude
        End Get
        Set(value As Boolean)
            Me._flagTaxInclude = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asgina el codigo del consultorio
    ''' </summary>
    ''' <returns></returns>
    Public Property ConsultingRoom As String Implements IControlOutpatientServices.ConsultingRoom
        Get
            Return INDSleConsultingRoom.EditValue
        End Get
        Set(value As String)
            INDSleConsultingRoom.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asgina el datasource del consultorio
    ''' </summary>
    ''' <returns></returns>
    Public Property ConsultingRoomXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.ConsultingRoomXPO
        Get
            Return INDSleConsultingRoom.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleConsultingRoom.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna el codigo de actividad de agendamiento
    ''' </summary>
    ''' <returns></returns>
    Public Property ScheduleActivityCode(Optional ByVal NullText As String = Nothing) As String Implements IControlOutpatientServices.ScheduleActivityCode
        Get
            Return If(ActivityType = 2, INDSleScheduleActivity.EditValue, INDSleScheduleActivityOther.EditValue)
        End Get
        Set(value As String)
            If ActivityType = 2 Then
                INDSleScheduleActivity.Properties.NullText = NullText
                INDSleScheduleActivity.EditValue = value
            Else
                INDSleScheduleActivityOther.Properties.NullText = NullText
                INDSleScheduleActivityOther.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o asigna el datasource de activida de agendamiento
    ''' </summary>
    ''' <returns></returns>
    Public Property ScheduleActivityOtherXPO As XPInstantFeedbackSource Implements IControlOutpatientServices.ScheduleActivityOtherXPO
        Get
            Return INDSleScheduleActivityOther.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)

            INDSleScheduleActivityOther.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo de la cita que viene desde el EHR (Se usa para el popup de crear cita)
    ''' </summary>
    Private Property _codeAppointment As String

    ''' <summary>
    ''' actividad de consulta medica que viene de la cita Agendada es null en cita manual
    ''' </summary>
    ''' <returns></returns>
    Private Property _consultationActivity As Byte?

    ''' <summary>
    ''' propiedad que obtiene o establece la bandera de carga del popup de citas cuando se edita
    ''' </summary>
    ''' <returns></returns>
    Public Property FlagLoadPopUpAppointment As Boolean
        Get
            Return _flagLoadPopUpAppointment
        End Get
        Set(value As Boolean)
            _flagLoadPopUpAppointment = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de servicio cuando la activad es de apoyo diagnostico
    ''' </summary>
    ''' <returns></returns>
    Public Property ServiceType As Integer?
        Get
            Return Me.INDGleServiceType.EditValue
        End Get
        Set(value As Integer?)
            Me.INDGleServiceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el tipo de ingreso
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionTypeId As Integer? Implements IControlOutpatientServices.AdmissionType
        Get
            Return INDSleAdmissionType.EditValue
        End Get
        Set(value As Integer?)
            INDSleAdmissionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <returns></returns>
    Property EntryRoutesHealthServicesId As Integer? Implements IControlOutpatientServices.EntryRoutesHealthServices
        Get
            Return INDSleIdEntryRoutesHealthServices.EditValue
        End Get
        Set(value As Integer?)
            INDSleIdEntryRoutesHealthServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la Finalidades tecnologías de la salud
    ''' </summary>
    ''' <returns></returns>
    Property HealthPurposesId As Integer? Implements IControlOutpatientServices.HealthPurposes
        Get
            Return INDSleIdHealthPurposes.EditValue
        End Get
        Set(value As Integer?)
            INDSleIdHealthPurposes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece la Modalidades de Atención
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionModalitiesId As Integer? Implements IControlOutpatientServices.AdmissionModalities
        Get
            Return INDSleIdAdmissionModalities.EditValue
        End Get
        Set(value As Integer?)
            INDSleIdAdmissionModalities.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de tipo de ingreso
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionTypeDatasource As XPInstantFeedbackSource Implements IControlOutpatientServices.AdmissionTypeDatasource
        Get
            Return INDSleAdmissionType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleAdmissionType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de Ingresa por
    ''' </summary>
    ''' <returns></returns>
    Property EntryRoutesHealthServicesDatasource As XPInstantFeedbackSource Implements IControlOutpatientServices.EntryRoutesHealthServicesDatasource
        Get
            Return INDSleIdEntryRoutesHealthServices.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleIdEntryRoutesHealthServices.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de finalidad
    ''' </summary>
    ''' <returns></returns>
    Property HealthPurposesDatasource As XPInstantFeedbackSource Implements IControlOutpatientServices.HealthPurposesDatasource
        Get
            Return INDSleIdHealthPurposes.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleIdHealthPurposes.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource del combo de Modalidad de atención
    ''' </summary>
    ''' <returns></returns>
    Property AdmissionModalitiesDatasource As XPInstantFeedbackSource Implements IControlOutpatientServices.AdmissionModalitiesDatasource
        Get
            Return INDSleIdAdmissionModalities.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleIdAdmissionModalities.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Diccionario que almacena en memoria la relacion de un CUPS con sus descripciones
    ''' </summary>
    ''' <returns></returns>
    Private Property _dictionaryCUPSDescription As Dictionary(Of String, Boolean) = New Dictionary(Of String, Boolean)

#End Region

#Region "GLOBALS"
    Private ThirdPartyPatientId As Integer?
    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' presenter de ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PControlOutpatientServices
    ''' <summary>
    ''' Variable que representa la entidad de parámetros de contratos
    ''' </summary>
    Dim _settingsContractXpo As Infrastructure.Data.Xpo.ContractRepository.SettingsContractXpo
    ''' <summary>
    ''' representa la entidad xpo de los profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Dim healthProfessional As HealthCareProfessionalXpo
    ''' <summary>
    ''' representa la entidad de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private thirdParty As Domain.Entities.ThirdParty
    Dim ctrInfo As CtrInfoControlOutpatientServices
    ''' <summary>
    ''' Variable que contiene el paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim patient As INPACIENT

    ''' <summary>
    ''' Id de la rias que se selecciona del control de RIASCups
    ''' </summary>
    Dim RiasId As Integer

    Private popupService As FrmPopupServicesControlOutPut

    ''' <summary>
    ''' almacena el valor del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceValue As Decimal = 0

    ''' <summary>
    ''' entodad que representa el detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private serviceOrderDetail As ServiceOrderDetail
    ''' <summary>
    ''' guarda la posicion del item que se esta editando para que luego el item nuevo se agregue en el lugar que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private indexEditItem As Integer
    ''' <summary>
    ''' variable para saber cual es el numero de la fila que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private rowEditing As Integer

    Private controlOutPatientServices As ControlOutPatientServices
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Variable que me permite saber si el grupo de atención aplica a RIAS
    ''' </summary>
    Dim ApplyRIASCareGroup As Boolean = False
    ''' <summary>
    ''' Parametro que indica si la autorizacion es obligatoria, por el tipo de unidad funcional (TUF)
    ''' </summary>
    Dim AuthoParameter As Boolean

    ''' <summary>
    ''' Permite saber si se valida el no. de autorización
    ''' </summary>
    Private ValidateAuthorizationNumber As Boolean = False
    Dim citasToAdd As List(Of SP_AD_ListarCitasMedicasNativo_Result)
    Dim isLoadingHomologation As Boolean
    Dim citasToAddWithHomologation As List(Of SP_AD_ListarCitasMedicasNativo_Result) = Nothing
    Private AllowsAddAuthorizationControl As Boolean

    ''' <summary>
    ''' Diccionario que contiene los permisos de l formulario
    ''' </summary>
    ''' <returns></returns>
    Public Property PermissionsForm As Dictionary(Of Integer, String) Implements IControlOutpatientServices.PermissionsForm

    ''' <summary>
    ''' Bandera de control para controlar cuando hay multiple homologacion
    ''' </summary>
    Private MultipleHomologation As Boolean = False

    ''' <summary>
    ''' guarda si el sistema es impuesto incluido o no
    ''' </summary>
    Private _flagTaxInclude As Boolean

    ''' <summary>
    ''' bandera de carga del popup de cita
    ''' </summary>
    Private _flagLoadPopUpAppointment As Boolean

    ''' <summary>
    ''' Bandera para validar cuando se edita o se crea una cita
    ''' </summary>
    Private editFlag As Boolean = False

#End Region

#Region "CRUD"
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True)

    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If admission Is Nothing Then
            INDliTipoRiesgo.ShowInCustomizationForm = False
            INDliCausa.ShowInCustomizationForm = False
            INDliAuthorization.ShowInCustomizationForm = False
            SetCustomizationFormBasedOnCulture()

            'Llenar datos de ingresos
            Dim resValidation = ValidateField(INDlcAdmission)
            INDliIdEntryRoutesHealthServices.ShowInCustomizationForm = True
            INDliIdHealthPurposes.ShowInCustomizationForm = True
            INDliIdAdmissionModalities.ShowInCustomizationForm = True
            INDliTipoRiesgo.ShowInCustomizationForm = True
            INDliCausa.ShowInCustomizationForm = True
            INDliAuthorization.ShowInCustomizationForm = True
            'SEGUIR ACA CON LA VALIDACION
            If Not resValidation.ResultStatus Then
                Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), resValidation.ToString())
                INDPceAdmissionData.Focus()
                INDPceAdmissionData.ShowPopup()
                controlValidate.Focus()
                Exit Sub
            End If
        End If
        Try
            If (ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0) OrElse (ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Where(Function(x) x.InvoiceId Is Nothing).Count() > 0) Then
                If INDGleAdmissionOption.EditValue Is Nothing Then
                    INDAdmissionOption.ShowInCustomizationForm = False
                ElseIf CType(INDGleAdmissionOption.EditValue, Byte) = 1 Then
                    INDliTipoRiesgo.ShowInCustomizationForm = False
                    INDliCausa.ShowInCustomizationForm = False
                    SetCustomizationFormBasedOnCulture()
                Else
                    INDLciAdmissionNumber.ShowInCustomizationForm = False
                    INDliTipoRiesgo.ShowInCustomizationForm = True
                    INDliCausa.ShowInCustomizationForm = True
                    INDliIdEntryRoutesHealthServices.ShowInCustomizationForm = True
                    INDliIdHealthPurposes.ShowInCustomizationForm = True
                    INDliIdAdmissionModalities.ShowInCustomizationForm = True
                End If
            End If
            If (ListCitasMedicas Is Nothing OrElse ListCitasMedicas.Count = 0) AndAlso (ListServiceOrderDetail Is Nothing OrElse ListServiceOrderDetail.Count = 0) _
                AndAlso (ListHemocomponent Is Nothing OrElse ListHemocomponent.Count = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar una cita o un servicio o un hemocomponente"
                Exit Sub
            End If

            If ((ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Any(Function(x) x.TipoCita Is Nothing OrElse x.TipoCita <> 3)) OrElse (ListHemocomponent IsNot Nothing AndAlso ListHemocomponent.Any(Function(x) x.Details.Any()))) AndAlso (ListServiceOrderDetail Is Nothing OrElse ListServiceOrderDetail.Count = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "No existe creada una Pre-liquidación para la cita o servicio o hemocomponente"
                Exit Sub
            End If

            If ValidateControles() = False Then
                Exit Sub
            End If
            If INDLciDispatched.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ValidateControlsRemision() = False Then
                    Exit Sub
                End If
            End If
            If _idOperativeUnit = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione una unidad operativa"
                Exit Sub
            End If
            If _listaCitasMedicas IsNot Nothing AndAlso _listaCitasMedicas.Any(Function(o) o.TipoCita IsNot Nothing AndAlso o.TipoCita = 3) AndAlso INDGleAdmissionOption.EditValue IsNot Nothing AndAlso CByte(INDGleAdmissionOption.EditValue) = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se debe agregar citas de tipo Pos Operatorio cuando se está relacionando un nuevo ingreso en Datos del Ingreso"
                Exit Sub
            End If
            If PESOPACIENTE Is Nothing AndAlso ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Where(Function(x) x.Tipo IsNot Nothing AndAlso x.Tipo = 3).Count > 0 Then
                If MessageIndigo.Show("El paciente no tiene configurado el peso, desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            'Si es una cita y si exige confirmación de cita
            If ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Any(Function(d) d.RequiresConfirmAppointment) Then
                For Each cita In ListCitasMedicas.Where(Function(d) d.RequiresConfirmAppointment)
                    'Se valida si tiene ingreso relacionado, el ingreso con el que se genere la orden de servicio sea con este ingreso
                    If String.IsNullOrEmpty(cita.Ingreso) Then
                        Mensaje(EeventViewerImages.Advertencia) = "La cita no se encuentra confirmada"
                        Exit Sub
                    Else
                        If admission Is Nothing OrElse admission.AdmissionCode.ToString().Trim() <> cita.Ingreso.ToString().Trim() Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format("El ingreso seleccionado no corresponde con el ingreso de la cita ({0})", cita.Ingreso.ToString().Trim())
                            Exit Sub
                        End If
                    End If
                Next
            End If
            'Validar el numero de atencion de acuerdo a como este parametrizado.
            Using ModeloControlPacientes As New MControlOutpatientServices(Me.Tag)
                AuthoParameter = ModeloControlPacientes.GetAuthorizationParameterByTUF(CareGroupId, _payrollFunctionalUnitXpo.UnitType.ToString())
            End Using
            If AuthoParameter = True AndAlso (INDAuthorizationNumberAdmission.EditValue Is Nothing OrElse INDAuthorizationNumberAdmission.EditValue.ToString() = String.Empty) Then
                Mensaje(EeventViewerImages.Advertencia) = "Ingrese # de autorización, su entidad lo exige."
                Exit Sub
            End If
            AssigningValues()
            Using model As New MControlOutpatientServices(Me.Tag)
                AsyncLoader(True)
                Dim result = Await model.GenerateDocuments(Me.controlOutPatientServices)
                If result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format("Se Generaron los siguientes documentos : {0}{1}", vbCrLf, result.Message)
                    If result.MessageResultAux IsNot Nothing AndAlso Not result.MessageResultAux(0).Equals(String.Empty) Then
                        Mensaje(EeventViewerImages.Informacion) = result.MessageResultAux(0)
                    End If
                    If _payrollFunctionalUnitXpo.UnitType <> 19 Then
                        If ListCitasMedicas IsNot Nothing AndAlso (ListCitasMedicas.Any(Function(x) x.TipoSolicitud <> 1) OrElse ListCitasMedicas.Any(Function(x) x.IsFalseId)) Then
                            'Existen citas de apoyo dx o de tratamiento especial
                            'Consultamos el parámetro de la tabla HCUNITHIS por unidad funcional
                            Dim param = Await model.GetHCUNITHISByUFUCODIGOWithFACMECONINS(_payrollFunctionalUnitXpo.Codigo)
                            If param IsNot Nothing AndAlso param.FACMECONINS IsNot Nothing Then
                                Using m As New MPatient("")
                                    Dim resultPatient As ActionResult(Of INPACIENT) = Await m.GetPacientByIdentification(patient.IPCODPACI.Trim())
                                    PESOPACIENTE = resultPatient.ObjectEmbbeded.PESO
                                End Using
                                'consultamos los productos,si no encuentra nada entonces continuamos
                                Dim ActMedCupsList = (From l In ListCitasMedicas Select New ACTMEDCUPS With {.CODACTMED = l.CodigioActividadMedica, .CODCUPS = l.CodigoServicio}).ToList()
                                Dim ResultProducts = Await model.GetProductsByActmedicaAndCups(ActMedCupsList, patient.IPFECNACI) '.Where(Function(x) x.IsFalseId).Select(Function(x) x.CodigoServicio).ToList(), patient.IPFECNACI)
                                'Consultamos los productos para las citas agregadas manualmente                            
                                If ResultProducts.StatusCode = eStatusResult.SUCCESS AndAlso ResultProducts.ObjectEmbbeded IsNot Nothing Then
                                    If Not waitForm.IsSplashFormVisible Then
                                        waitForm.ShowWaitForm()
                                    End If
                                    Using form As New FrmProductsAmb(Me.BarraBotones.PermissionsForm, param.FACMECONINS, param.CODBODEGA,
                                                                     param.CODCENCOS, patient, result.MessageResult(0), _idOperativeUnit,
                                                                     ResultProducts.ObjectEmbbeded, ThirdPartyPatientId, FunctionalUnitId,
                                                                    _centAtencionXpo.CODCENATE, _payrollFunctionalUnitXpo.Codigo)
                                        patient.PESO = PESOPACIENTE
                                        Dim trp As New FrmTransparent(form, False)
                                        AddHandler form.Shown, AddressOf HideLoaderForm
                                        trp.ShowDialog(Me)
                                    End Using
                                End If
                            End If
                        End If
                    End If
                    If ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0 Then
                        If Not waitForm.IsSplashFormVisible Then
                            waitForm.ShowWaitForm()
                        End If
                        Dim reportDef As New Reporter.rptControlOutpatientServices()
                        AddHandler reportDef.AfterPrint, AddressOf HideLoaderForm
                        ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, result.MessageResult(0))

                        'Se consulta el campo LiquidateSinceControlOutPatientService en parametros de facturacion para saber si se abre el formulario de liquidación
                        Dim settingsBillingXpo = presenter.GetLiquidateSinceControlOutPatientService(BarraBotones.OperatingUnitValue)
                        If settingsBillingXpo IsNot Nothing AndAlso settingsBillingXpo.LiquidateSinceControlOutPatientService Then
                            OpenFormLiquidation(result.MessageResult(0))
                        End If

                    End If
                    AsyncLoader(False)
                    Nuevo()
                Else
                    AsyncLoader(False)
                    If result.Message IsNot Nothing Then
                        GenerateListError(result.Message)
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Nuevo()
            Throw ex
        End Try
    End Sub

    Private Sub HideLoaderForm(sender As Object, e As EventArgs)
        waitForm.CloseWaitForm()
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub GenerateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls(eOptionClean.Nuevo)
        CleanControlsMedicalAppointment()
        CleanControlsPopupAdmissionData()
        CleanControlsDispatched()
        CleanControlHemo()
        CleanPopUpCitas()
        admission = Nothing
    End Sub

#End Region

#Region "METHODS"

    ''' <summary>
    ''' Metodo que consulta la admision y la asigna al control correspondiente, este metodo se utiliza al momento de seleccionar una cirugia en la rejilla
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    Private Sub ExecuteGetAdmission(admissionNumber As String)
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Of Boolean)(Function()

                                              Dim objTemp As ViewGetAdmissionXpo = presenter.GetAdmissionByAdmissionNumber(admissionNumber)
                                              Dim healthTmp As HealthAdministrator = Nothing
                                              Dim careGroupTmp As CareGroup = Nothing
                                              Using model As New Presentation.Contract.MVP.MHealthAdministrator(Me.Tag)
                                                  healthTmp = model.GetHealthAdministratorByIdSimple(objTemp.HealthAdministratorId).ObjectEmbbeded
                                              End Using
                                              Using model As New Presentation.Contract.MVP.MCareGroup(Me.Tag)
                                                  careGroupTmp = model.GetCareGroupByIdSimple(objTemp.CareGroupId).ObjectEmbbeded
                                              End Using
                                              admission = objTemp

                                              If Not tokenAsync.IsCancellationRequested Then
                                                  INDSleAdmissionNumber.SafeInvoke(Sub()
                                                                                       With admission
                                                                                           Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim(), admission.PatientName.ToString().Trim()))
                                                                                           INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
                                                                                           If .AdmissionDate IsNot Nothing Then
                                                                                               INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()
                                                                                           End If
                                                                                           If .AdmissionType IsNot Nothing Then
                                                                                               INDTxtAdmissionType.Text = ResourceManager.GetString(String.Concat("AdmissionType", .AdmissionType.ToString().Trim()))
                                                                                           End If
                                                                                           If .AuthorizationNumber IsNot Nothing Then
                                                                                               INDTxtAuthorizationNumber.Text = .AuthorizationNumber.ToString().Trim()
                                                                                           End If
                                                                                           If .AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", MODULE_NAME) Then
                                                                                               INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                                                                               If .BedStay IsNot Nothing Then
                                                                                                   INDTxtStay.Text = .BedStay.ToString().Trim()
                                                                                               End If
                                                                                           Else
                                                                                               INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                                                                           End If

                                                                                           If .CareGroupId IsNot Nothing AndAlso .CareGroupId > 0 Then
                                                                                               If careGroupTmp IsNot Nothing AndAlso careGroupTmp.Id > 0 Then
                                                                                                   INDTxtBenefitsPlan.Text = careGroupTmp.Code + " - " + careGroupTmp.Name
                                                                                               Else
                                                                                                   INDTxtBenefitsPlan.Text = String.Empty
                                                                                               End If
                                                                                           End If

                                                                                           If .HealthAdministratorId IsNot Nothing AndAlso .HealthAdministratorId > 0 Then
                                                                                               If healthTmp IsNot Nothing AndAlso healthTmp.Id > 0 Then
                                                                                                   INDTxtEntity.Text = healthTmp.Code + " - " + healthTmp.Name
                                                                                               Else
                                                                                                   INDTxtEntity.Text = String.Empty
                                                                                               End If
                                                                                           End If
                                                                                           If .LiquidationType IsNot Nothing Then
                                                                                               INDTxtLiquidationType.Text = ResourceManager.GetString(String.Concat("LiquidationType", .LiquidationType.ToString().Trim()))
                                                                                           End If
                                                                                           If .PatientCode IsNot Nothing And .PatientName IsNot Nothing Then
                                                                                               INDTxtPatient.Text = .PatientCode.ToString().Trim() + " - " + .PatientName.ToString().Trim()
                                                                                           End If
                                                                                           If .PlaceEntry IsNot Nothing Then
                                                                                               INDTxtAdmissionPlace.Text = ResourceManager.GetString(String.Concat("PlaceEntry", .PlaceEntry.ToString().Trim()))
                                                                                           End If
                                                                                           If .ResponsibleName IsNot Nothing Then
                                                                                               INDTxtResponsibleName.Text = .ResponsibleName.ToString().Trim()
                                                                                           End If
                                                                                           If .ResponsiblePhone IsNot Nothing Then
                                                                                               INDTxtResponsiblePhone.Text = .ResponsiblePhone.ToString().Trim()
                                                                                           End If
                                                                                           Select Case .TRATAESPECIA
                                                                                               Case 2
                                                                                                   INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Renal"
                                                                                               Case 3
                                                                                                   INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Oncológico"
                                                                                               Case Else
                                                                                                   INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Ninguno"
                                                                                           End Select

                                                                                           Mensaje(EeventViewerImages.Informacion) = INDTxtAdmissionPopup.Text
                                                                                       End With
                                                                                       INDSleAdmissionNumber.SetEditValue = admission.AdmissionCode
                                                                                   End Sub)
                                              End If

                                              Return True
                                          End Function, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si el tipo pasado tiene como base
    ''' un control BaseEdit
    ''' </summary>
    ''' <param name="type">Tipo pasado a consultar</param>
    Private Function GetIsBaseEditBaseType(ByVal type As Type) As Boolean
        Dim result As Boolean = False
        IsBaseEditBaseType(type, result)
        Return result
    End Function

    ''' <summary>
    ''' Cargar por defecto las opciones del campo "Ingresa por"
    ''' </summary>
    Private Sub LoadIngressData()
        If Me.EntryRoutesHealthServicesDatasource Is Nothing Then
            presenter.InitializateEntryRoutesHealthServices()
        End If
        If EntryRoutesHealthServicesId Is Nothing Then
            EntryRoutesHealthServicesId = 3
        End If
    End Sub

    ''' <summary>
    ''' Cargar por defecto las opciones del campo "Modalidad de ingreso"
    ''' </summary>
    Private Sub LoadModalityData()
        If Me.AdmissionModalitiesDatasource Is Nothing Then
            presenter.InitializateAdmissionModalities()
        End If
        If AdmissionModalitiesId Is Nothing Then
            AdmissionModalitiesId = 1
        End If
    End Sub

    Private Sub IsBaseEditBaseType(ByVal type As Type, ByRef result As Boolean)
        If type.BaseType.Equals(GetType(DevExpress.XtraEditors.BaseEdit)) Then
            result = True
        Else
            If Not type.BaseType.Equals(GetType(Object)) Then
                IsBaseEditBaseType(type.BaseType, result)
            Else
                result = False
            End If
        End If
    End Sub

    Dim controlValidate As System.Windows.Forms.Control
    Public Function ValidateField(layoutControl As LayoutControl) As ValidateResult
        Dim result As New ValidateResult()
        Dim refCtr As System.Windows.Forms.Control = Nothing
        'Recorremos los LayoutControlItem
        For Each item As BaseLayoutItem In (From i As BaseLayoutItem In layoutControl.Items Where i.GetType().Equals(GetType(LayoutControlItem)) Select i).ToList()
            Dim it As LayoutControlItem = DirectCast(item, LayoutControlItem)
            Dim nn As String = it.Name
            If TypeOf it.Control Is IValidable OrElse Me.GetIsBaseEditBaseType(it.Control.GetType()) Then
                Dim ctr As Object = it.Control
                If it.Name <> INDliAuthorization.Name AndAlso (Not it.ShowInCustomizationForm OrElse Not it.AllowHide) AndAlso it.ControlName <> "INDSleCUPSCrystal" Then
                    If ctr.EditValue IsNot Nothing Then
                        If ctr.EditValue.GetType().Equals(GetType(String)) AndAlso ctr.EditValue.ToString().Trim().Equals(String.Empty) Then
                            If result.EmptyFieldNames.Count = 0 Then
                                refCtr = ctr
                            End If
                            If it.Tag <> 50 Then
                                result.EmptyFieldNames.Add(it.Text)
                            End If
                        End If
                    Else
                        If result.EmptyFieldNames.Count = 0 Then
                            refCtr = ctr
                        End If
                        result.EmptyFieldNames.Add(it.Text)
                    End If
                ElseIf it.ControlName = "INDSleCUPSCrystal" Then
                    If String.IsNullOrEmpty(Me._selectorCUPS.GetKeys()) Then
                        If result.EmptyFieldNames.Count = 0 Then
                            refCtr = ctr
                        End If
                        result.EmptyFieldNames.Add(it.Text)
                    End If
                End If
            End If
        Next
        If refCtr IsNot Nothing Then
            controlValidate = refCtr
            refCtr.Focus()
        End If
        result.ResultStatus = (result.EmptyFieldNames.Count = 0)
        Return result
    End Function

    Public Function ValidateControles() As Boolean
        'Primero Validamos el layout principal
        Dim res = ValidateField(INDLcControlOutpatientServices)
        Dim res1 = ValidateField(INDlcAdmission)

        If INDGleAdmissionOption.EditValue IsNot Nothing AndAlso CType(INDGleAdmissionOption.EditValue, Byte) = 2 AndAlso INDLciAdmissionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDSleAdmissionNumber.Search.EditValue Is Nothing Then
            res1.EmptyFieldNames.Add(INDLciAdmissionNumber.Text)
            res1.ResultStatus = False
            controlValidate = INDSleAdmissionNumber
        End If

        'Si es un ingreso nuevo y el grupo de atención requiere el no. autorización y se encuentra vacío
        If INDGleAdmissionOption.EditValue IsNot Nothing AndAlso CType(INDGleAdmissionOption.EditValue, Byte) = 1 Then
            If ValidateAuthorizationNumber AndAlso String.IsNullOrEmpty(RTrim(LTrim(INDAuthorizationNumberAdmission.EditValue))) Then
                res1.EmptyFieldNames.Add(INDliAuthorization.Text)
                res1.ResultStatus = False
                controlValidate = INDAuthorizationNumberAdmission
            End If
        End If

        res.EmptyFieldNames.AddRange(res1.EmptyFieldNames)
        'SEGUIR ACA CON LA VALIDACION
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Return False
        ElseIf Not res1.ResultStatus AndAlso
            ((ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Where(Function(x) x.InvoiceId Is Nothing).Count() <> 0) OrElse (ListHemocomponent IsNot Nothing AndAlso ListHemocomponent.Any())) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res1.ToString())
            INDPceAdmissionData.Focus()
            INDPceAdmissionData.ShowPopup()
            controlValidate.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ValidateControlsRemision() As Boolean

        If INDTxtRemissionNumber.EditValue = String.Empty Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El Campo Número de Remisión está vacío"
            INDTxtRemissionNumber.Focus()
            Return False
        End If

        If INDSleIPS.EditValue = "" Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El Campo IPS está vacío"
            INDSleIPS.Focus()
            Return False
        End If

        If INDSleTown.EditValue = "" Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El Campo Municipio está vacío"
            INDSleTown.Focus()
            Return False
        End If

        If INDDteRemissionDate.Text = "" Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El Campo Fecha de Remisión está vacío"
            INDDteRemissionDate.Focus()
            Return False
        End If

        If INDTxtAuthorizationNumberRemission.EditValue = String.Empty Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El Campo Número de Autorización está vacío"
            INDTxtAuthorizationNumberRemission.Focus()
            Return False
        End If

        If INDMeObservationRemission.EditValue = String.Empty Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "El Campo Observaciones está vacío"
            INDTxtAuthorizationNumberRemission.Focus()
            Return False
        End If

        Return True
    End Function

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls(eOptionClean.Deshacer)
        CleanControlsPopupAdmissionData()
        CleanControlsDispatched()
        CleanControlsMedicalAppointment()
        CleanControlsAdminssion()
        CleanControlHemo()
        CleanPopUpCitas()
        FunctionalUnitXPO = Nothing
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        Using Model As New MAdmissions(Me.Tag)
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "IPCODPACI", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "IPNOMCOMP", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
                .ValorSolicitado = "IPCODPACI"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllPatients
                BarraBotones.PrepareToolbar(eAction.OnlyFind)
                .FormParent = Me
                .Text = "Paciente"
                .ShowSearch()
            End With
        End Using
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateInvoiced"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Sub SetAdmission(record As Object)
        admission = record
        With admission
            Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim(), admission.PatientName.ToString().Trim()))
            INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
            If .AdmissionDate IsNot Nothing Then
                INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()
            End If
            If .AdmissionType IsNot Nothing Then
                INDTxtAdmissionType.Text = ResourceManager.GetString(String.Concat("AdmissionType", .AdmissionType.ToString().Trim()))
            End If
            If .AuthorizationNumber IsNot Nothing Then
                INDTxtAuthorizationNumber.Text = .AuthorizationNumber.ToString().Trim()
            End If
            If .AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", MODULE_NAME) Then
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                If .BedStay IsNot Nothing Then
                    INDTxtStay.Text = .BedStay.ToString().Trim()
                End If
            Else
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If

            If .CareGroupId IsNot Nothing AndAlso .CareGroupId > 0 Then
                Using model As New Presentation.Contract.MVP.MCareGroup(Me.Tag)
                    Dim careGroupTmp = model.GetCareGroupByIdSimple(.CareGroupId).ObjectEmbbeded
                    If careGroupTmp IsNot Nothing AndAlso careGroupTmp.Id > 0 Then
                        INDTxtBenefitsPlan.Text = careGroupTmp.Code + " - " + careGroupTmp.Name
                    Else
                        INDTxtBenefitsPlan.Text = String.Empty
                    End If
                End Using
            End If

            If .HealthAdministratorId IsNot Nothing AndAlso .HealthAdministratorId > 0 Then
                Using model As New Presentation.Contract.MVP.MHealthAdministrator(Me.Tag)
                    Dim healthTmp = model.GetHealthAdministratorByIdSimple(.HealthAdministratorId).ObjectEmbbeded
                    If healthTmp IsNot Nothing AndAlso healthTmp.Id > 0 Then
                        INDTxtEntity.Text = healthTmp.Code + " - " + healthTmp.Name
                    Else
                        INDTxtEntity.Text = String.Empty
                    End If
                End Using
            End If
            If .LiquidationType IsNot Nothing Then
                INDTxtLiquidationType.Text = ResourceManager.GetString(String.Concat("LiquidationType", .LiquidationType.ToString().Trim()))
            End If
            If .PatientCode IsNot Nothing And .PatientName IsNot Nothing Then
                INDTxtPatient.Text = .PatientCode.ToString().Trim() + " - " + .PatientName.ToString().Trim()
            End If
            If .PlaceEntry IsNot Nothing Then
                INDTxtAdmissionPlace.Text = ResourceManager.GetString(String.Concat("PlaceEntry", .PlaceEntry.ToString().Trim()))
            End If
            If .ResponsibleName IsNot Nothing Then
                INDTxtResponsibleName.Text = .ResponsibleName.ToString().Trim()
            End If
            If .ResponsiblePhone IsNot Nothing Then
                INDTxtResponsiblePhone.Text = .ResponsiblePhone.ToString().Trim()
            End If
            Select Case .TRATAESPECIA
                Case 2
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Renal"
                Case 3
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Oncológico"
                Case Else
                    INDTxtAdmissionPopup.Text = "Tipo de Ingreso : Ninguno"
            End Select

            Mensaje(EeventViewerImages.Informacion) = INDTxtAdmissionPopup.Text
        End With
        INDSleAdmissionNumber.SetEditValue = admission.AdmissionCode
    End Sub

    Private Sub CleanControls(opt As eOptionClean)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        If opt = eOptionClean.Deshacer Then
            INDLcgMedicalAppointment.Text = "Citas"
            BarraBotones.StatusRecordVisible = False
            INDLcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgMedicalAppointment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgPreLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If INDSleCareCenter.Properties.Buttons.Count > 1 Then
                INDSleCareCenter.Properties.Buttons(1).Visible = False
            End If
            CareCenterCode = Nothing
            FunctionalUnitId = Nothing

            INDGleAdmissionOption.Properties.ReadOnly = False
            INDSleAdmissionNumber.IsReadOnly = False
            INDLcgAddCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            'Se organizan las columnas de la rejilla
            INDcolDateQx.Visible = False
            INDcolPrincipalQx.Visible = False
            INDcolSurgeonQx.Visible = False
            INDcolSpecialtyQx.Visible = False
            INDcolRoomQx.Visible = False

            INDcolCode.Visible = True
            INDcolProcedureQx.Visible = True
            INDcolDate.Visible = True
            INDcolType.Visible = True
            INDcolUbication.Visible = True
            INDcolActivity.Visible = True
            INDcolProfessional.Visible = True
            INDcolSpecialty.Visible = True
            INDcolCode.VisibleIndex = 1
            INDcolProcedureQx.VisibleIndex = 2
            INDcolDate.VisibleIndex = 3
            INDcolType.VisibleIndex = 4
            INDcolUbication.VisibleIndex = 5
            INDcolActivity.VisibleIndex = 6
            INDcolProfessional.VisibleIndex = 7
            INDcolSpecialty.VisibleIndex = 8
        End If
        previusAdmissionOption = 0
        INDGleAdmissionOption.EditValue = Nothing
        INDSleHealthProfessional.EditValue = Nothing
        INDGleSpecialty.EditValue = Nothing
        Me.SelectorClear()
        INDGleMedicalAppointmentType.EditValue = Nothing

        INDSleCareGroup.Properties.ReadOnly = False
        INDSleHealthAdministrator.Properties.ReadOnly = False

        INDGcMedicalAppointmentPatient.DataSource = Nothing
        INDBePatient.Text = String.Empty
        patient = Nothing
        HealthAdministratorId = Nothing
        CareGroupId = Nothing
        Dispatched = False
        IndigoGridControl1.RefreshGrid(INDGcMedicalAppointment)
        IndigoGridControl1.RefreshGrid(INDGcServiceOrderDetail)
        CleanGrids()
        Me.ActionsOnControls = False
        Me.ActionsOnControlsIngress = False
        INDGcServiceOrderDetail.Enabled = False
        INDBePatient.Properties.ReadOnly = True
        INDlciContract.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleContract.Properties.NullText = ""
        ListCitasMedicas = Nothing
        ListServiceOrderDetail = Nothing
        CUPSCrystalXPO = Nothing
        INDrptSleInvoice.DataSource = Nothing
        INDSleRequestHemoReserve.EditValue = Nothing

        TotalValue = 0
        ctrInfo.PrintInfo()

        If opt = eOptionClean.Nuevo Then
            INDBePatient.Properties.ReadOnly = False
            INDBePatient.Focus()
        End If
        Me.BarraBotones.WeightPatient(0, Nothing, Nothing)
    End Sub

    Property TotalValue As Decimal
    ''' <summary>
    ''' metodo para obtener la informacion para colocar en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String, String)

        TotalValue = 0
        If ListServiceOrderDetail IsNot Nothing Then
            For Each sod In ListServiceOrderDetail
                TotalValue += Math.Round(sod.GrandTotalSalesPrice, 2, MidpointRounding.AwayFromZero)
            Next
        End If

        Return New Tuple(Of String, String, String)(TotalValue, INDSleCareCenter.Text, INDSleFunctionalUnit.Text)
    End Function

    Property HealthAdministrator As HealthAdministrator
    Private _isloading As Boolean
    ''' <summary>
    ''' metodo para obtener el paciente seleccionado
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        Try
            INDPceMedicalAppointment.ClosePopup()
            Using m As New MPatient(Me.Tag)
                AsyncLoader(True)
                Dim result As ActionResult(Of INPACIENT) = Await m.GetPacientByIdentification(ReturnValue)
                If Not result.StateResult Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    AsyncLoader(False)
                    Exit Sub
                ElseIf result.ObjectEmbbeded Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No hay creado un paciente en Indigo Vie para el Nit digitado"
                    AsyncLoader(False)
                    Exit Sub
                End If
                patient = result.ObjectEmbbeded
                Dim errors As New StringBuilder

                If patient.GENCAREGROUP Is Nothing Then
                    errors.AppendLine("El paciente no tiene un grupo de atención asociado")
                Else
                    'Se consulta el grupo de atención del paciente, 
                    'se realiza la validación de la entidad siempre y cuando el grupo de atención asociado al paciente sea sin contrato
                    'ya que de acuerdo a la desciprción del campo GENCONENTITY solo se asigna cuando sea un grupo de atención sin contrato Bug 5885.
                    Dim careGroupPatientTemp = presenter.GetCareGroupPatientById(patient.GENCAREGROUP.Value)

                    If careGroupPatientTemp IsNot Nothing AndAlso careGroupPatientTemp.CareGroupType = 2 AndAlso patient.GENCONENTITY Is Nothing Then
                        errors.AppendLine("El paciente no tiene una entidad administradora de salud asociada")
                    End If
                End If

                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    AsyncLoader(False)
                    patient = Nothing
                    ActionsOnControlsIngress = False
                    Exit Sub
                End If
                PESOPACIENTE = patient.PESO
                INDBePatient.Text = patient.IPCODPACI.Trim() + " - " + patient.IPNOMCOMP.Trim()
                Using model As New MServiceOrder(MyTag)
                    HealtAdministratorXPO = model.ListHealthAdministratorByStatus()
                    CareGroupXPO = model.ListCareGroup()
                End Using
                CareGroupId = patient.GENCAREGROUP
                HealthAdministratorId = patient.GENCONENTITY
                _isloading = True
                Dim healthAdministStr As String = ""
                If HealthAdministratorId IsNot Nothing Then
                    Using md As New MHealthAdministrator(Me.Tag)
                        Dim healthAdm As ActionResult(Of HealthAdministrator) = Await md.GetHealthAdministratorById(HealthAdministratorId)
                        If healthAdm.StateResult AndAlso healthAdm.ObjectEmbbeded IsNot Nothing AndAlso healthAdm.ObjectEmbbeded.Id > 0 Then
                            HealthAdministrator = healthAdm.ObjectEmbbeded
                            healthAdministStr = String.Concat(healthAdm.ObjectEmbbeded.Code.Trim(), " - ", healthAdm.ObjectEmbbeded.Name.Trim())
                            ThirdPartyPatientId = HealthAdministrator.ThirdPartyId
                        End If
                    End Using
                End If
                _isloading = False
                Await ListarCitasMedicas()
                AsyncLoader(False)
                ActionsOnControlsIngress = True
                INDBePatient.Properties.ReadOnly = True
                If RequestHemoReserve Is Nothing Then RequestHemoReserve = 0
                INDSleHealthAdministrator.Properties.NullText = healthAdministStr
            End Using
            BarraBotones.PrepareToolbar(eAction.OnlySave)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False

            'Se valida si el tipo de unidad funcional es de cirugia = 19
            If _payrollFunctionalUnitXpo.UnitType = 19 Then
                'Se cambia el texto del grupo de citas
                INDLcgMedicalAppointment.Text = "Cirugía Programada"

                'Se asigna ingreso existente
                INDGleAdmissionOption.EditValue = 2

                'Se coloca los controles de solo lectura
                INDGleAdmissionOption.Properties.ReadOnly = True
                INDSleAdmissionNumber.IsReadOnly = True

                'Se oculta la pestaña de crear cita
                INDLcgAddCita.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                'Se organizan las columnas de la rejilla
                INDcolDate.Visible = False
                INDcolType.Visible = False
                INDcolUbication.Visible = False
                INDcolActivity.Visible = False
                INDcolProfessional.Visible = False
                INDcolSpecialty.Visible = False

                INDcolProcedureQx.Visible = True
                INDcolDateQx.Visible = True
                INDcolPrincipalQx.Visible = True
                INDcolSurgeonQx.Visible = True
                INDcolSpecialtyQx.Visible = True
                INDcolRoomQx.Visible = True
                INDcolCode.VisibleIndex = 1
                INDcolProcedureQx.VisibleIndex = 2
                INDcolDateQx.VisibleIndex = 3
                INDcolPrincipalQx.VisibleIndex = 4
                INDcolSurgeonQx.VisibleIndex = 5
                INDcolSpecialtyQx.VisibleIndex = 6
                INDcolRoomQx.VisibleIndex = 7
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles de los datos del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupAdmissionData()
        INDSleSMLV.Properties.Buttons(1).Visible = False
        INDGleRiskType.Properties.DataSource = ListRiskType
        INDGleAdmissionOption.Properties.DataSource = ListAdmissionOption
        INDLciAdmissionNumber.HideControl()
        INDLciValueSent.HideControl()
        INDLciSMLV.HideControl()
        INDGleRiskType.EditValue = Nothing
        INDSleAdmissionNumber.SetNullText(String.Empty)
        INDTxtValueSent.EditValue = 0
        INDGleCause.EditValue = Nothing
        INDAuthorizationNumberAdmission.EditValue = String.Empty
		INDMeObservationAdmission.EditValue = String.Empty
		INDTxtAuthorizationNumber.EditValue = String.Empty
		INDPccAdmissionData.Size = New System.Drawing.Size(INDlcAdmission.Size.Width, 80)

        INDliTipoRiesgo.HideControl()
        INDliCausa.HideControl()
        INDliAuthorization.HideControl()
        INDliObservaciones.HideControl()
        INDliIdEntryRoutesHealthServices.HideControl()
        INDliIdHealthPurposes.HideControl()
        INDliIdAdmissionModalities.HideControl()

    End Sub

    ''' <summary>
    ''' limpia el popup de cita
    ''' </summary>
    ''' <param name="edit"></param>
    Private Sub CleanPopUpCitas(Optional edit As Boolean = False)
        'ActivityType = 2
        editFlag = False
        Me.ServiceType = Nothing
        INDSleRoom.EditValue = Nothing
        INDSleRoom.Properties.DataSource = Nothing
        INDSleRoom.Properties.NullText = Nothing
        '------------------------------------------------------
        INDSleScheduleActivity.Properties.DataSource = Nothing
        Me.ScheduleActivityCode = Nothing
        '----------------------------------------
        Me.CUPSCrystalXPO = Nothing
        Me.INDSleCUPSCrystal.Properties.NullText = Nothing
        '-------------------------------
        Me.SelectorClear()
        '----------------------------------------
        Me.INDsleDescription.Properties.DataSource = Nothing
        Me.INDsleDescription.EditValue = Nothing
        Me.INDsleDescription.Properties.NullText = Nothing
        '----------------------------------------
        Me.ConsultingRoomXPO = Nothing
        Me.ConsultingRoom = Nothing

        INDDteDateMedicalAppointment.EditValue = GetDateServer()

        Me.INDLcgAddCita.Text = "Crear Cita"
        Me.INDlciSbClean.HideControl()

        If edit Then
            Me.INDLcgAddCita.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
            Me.INDSleScheduleActivityOther.Properties.DataSource = Nothing
            Me.INDlciSbClean.HideControl(False)
        Else
            Me._codeAppointment = Nothing
            Me._consultationActivity = Nothing
        End If
        '---------------------------------------------------
        '---------------------------------------------------
        Me.INDSleHealthProfessional.EditValue = Nothing
        Me.INDSleHealthProfessional.Properties.DataSource = Nothing
        Me.INDSleHealthProfessional.Properties.NullText = Nothing
        Me.INDGleSpecialty.Properties.DataSource = Nothing
        Me.INDGleSpecialty.EditValue = Nothing
        Me.INDGleSpecialty.Properties.NullText = Nothing
        '---------------------------------------------------
        Me.INDSleConsultingRoom.Properties.NullText = Nothing
    End Sub
    ''' <summary>
    ''' metodo para limpiar los controles del popup de citas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsMedicalAppointment()
        INDDteDateMedicalAppointment.EditValue = GetDateServer()
        INDGleMedicalAppointmentType.Properties.DataSource = ListMedicalAppointmentType
        INDGleMedicalAppointmentType.EditValue = Nothing
        INDSleHealthAdministrator.Properties.NullText = String.Empty
        INDGleSpecialty.Enabled = False
        INDSeQuantity.EditValue = 1
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles de remision
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsDispatched()
        INDTxtRemissionNumber.EditValue = String.Empty
        INDSleIPS.EditValue = Nothing
        INDSleIPS.Properties.Buttons(1).Visible = False
        INDSleTown.EditValue = Nothing
        INDSleTown.Properties.Buttons(1).Visible = False
        INDDteRemissionDate.EditValue = Nothing
        INDTxtAuthorizationNumberRemission.EditValue = String.Empty
        INDMeObservationRemission.EditValue = String.Empty
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub CleanControlHemo()
        Me.HemocomponentId = Nothing
        Me.ProfessionalId = Nothing
        Me.HemoQuantity = 0
        Me.SpecialityCode = String.Empty
        Me.Hemocomponente = Nothing
        Me.ProfessionalXPO = Nothing
        Me.HemocomponentXPO = Nothing
        Me.ListReserveHemocomponentDetail = Nothing
        Me.ListHemocomponent = Nothing
        INDGcHemoDetail.DataSource = Nothing
        INDGcHemoService.DataSource = Nothing
        ListMedicalAppointmentHemo = Nothing
        ListOrdersExtramuralHemo = Nothing
        INDLcgHemoDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Sub CleanGrids()
        INDGcMedicalAppointment.DataSource = Nothing
        INDGcMedicalAppointmentPatient.DataSource = Nothing
        INDGcServiceOrderDetail.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpioar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdminssion()
        INDTxtAdmissionCode.Text = String.Empty
        INDTxtStay.Text = String.Empty
        INDTxtPatient.Text = String.Empty
        INDTxtAdmissionDate.Text = String.Empty
        INDTxtAdmissionType.Text = String.Empty
        INDTxtAdmissionPlace.Text = String.Empty
        INDTxtLiquidationType.Text = String.Empty
        INDTxtEntity.Text = String.Empty
        INDTxtBenefitsPlan.Text = String.Empty
        INDTxtAuthorizationNumber.Text = String.Empty
        INDTxtResponsibleName.Text = String.Empty
        INDTxtResponsiblePhone.Text = String.Empty
        'INDGcMedicalAppointmentPatient.DataSource = Nothing
        admission = Nothing
    End Sub

    Private Async Function ListarCitasMedicas() As Task
        Using model As New MControlOutpatientServices(Me.Tag)
            Dim citasmedicas = Await model.ListarCitasMedicas(patient.IPCODPACI, CareCenterCode)

            If citasmedicas Is Nothing OrElse Not citasmedicas?.Any() Then
                Return
            End If

            'Si el tipo de la unidad funcional es cirugia = 19, se filtran del listado solo las de cirugia
            If _payrollFunctionalUnitXpo.UnitType = 19 Then
                INDGcMedicalAppointmentPatient.DataSource = (From x In citasmedicas Where x.OrigenCirugia = 1 Select x).ToList()
            Else 'Si no se muestran las demas que no sean cirugia
                INDGcMedicalAppointmentPatient.DataSource = (From x In citasmedicas Where x.OrigenCirugia = 0 Select x).ToList()
            End If

            Return
        End Using
    End Function

    ''' <summary>
    ''' metodo pra establecer las especialidades del medico en el combo
    ''' </summary>
    ''' <param name="healthProfessional"></param>
    ''' <remarks></remarks>
    Private Sub SetSpecialties(healthProfessional As HealthCareProfessionalXpo)
        Dim listSpecialty As New List(Of Tuple(Of String, String))
        If healthProfessional.CODESPEC1 IsNot Nothing Then
            listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC1.CODESPECI, healthProfessional.CODESPEC1.CodeName))
        End If
        If healthProfessional.CODESPEC2 IsNot Nothing Then
            listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC2.CODESPECI, healthProfessional.CODESPEC2.CodeName))
        End If
        If healthProfessional.CODESPEC3 IsNot Nothing Then
            listSpecialty.Add(New Tuple(Of String, String)(healthProfessional.CODESPEC3.CODESPECI, healthProfessional.CODESPEC3.CodeName))
        End If
        INDGleSpecialty.Properties.DataSource = listSpecialty

        If listSpecialty.Count = 1 Then
            INDGleSpecialty.EditValue = listSpecialty(0).Item1
        Else
            INDGleSpecialty.Properties.NullText = Nothing
            INDGleSpecialty.EditValue = Nothing
        End If
    End Sub

    Private Function ValidateControlsCita() As Boolean
        Dim res = ValidateField(INDlcCita)
        'SEGUIR ACA CON LA VALIDACION
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Return False
        Else
            Return True
        End If
    End Function

    Private Async Function CreateServiceOrderDetailItem(listCitas As List(Of SP_AD_ListarCitasMedicasNativo_Result), unSetInvoice As Boolean) As Task(Of Boolean)
        MultipleHomologation = False
        Using model As New MControlOutpatientServices(Me.Tag)
            AsyncLoader(True)
            INDsbAddCita.Enabled = False
            Me.Cursor = ChangeCursorIndigo()

            'Se obtienen los códigos cups para enviar a validar
            If listCitas Is Nothing OrElse listCitas.All(Function(x) String.IsNullOrEmpty(x?.CodigoServicio)) Then
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Mensaje(EeventViewerImages.Advertencia) = "No hay servicios dentro de las citas seleccionadas"
                AsyncLoader(False)
                Return False
            End If

            Dim listCodes = (From x In listCitas Select $"'{x?.CodigoServicio?.Trim()}'").ToList()

            'Listado que se asigna cuando se abre el modal para seleccionar las autorizaciones
            Dim ListViewTraceabilityPaperworkAuthorizedXpo As List(Of ViewTraceabilityPaperworkAuthorizedXpo) = Nothing

            'Se valida si los cups son suceptibles a autorización
            Dim listValidationSusceptible = presenter.ValidateCUPSSusceptible(listCodes, INDSleCareGroup.EditValue)
            If listValidationSusceptible IsNot Nothing AndAlso listValidationSusceptible.Count > 0 Then
                Dim codesString = String.Join(",", listValidationSusceptible.ToArray())
                If MessageIndigo.Show("Los siguientes CUPS " & codesString & " son suceptibles a autorización, Desea asociar una autorización?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Formulario As New FrmAsociateAuthorization()
                        Formulario.CareCenterCode = CareCenterCode
                        Formulario.FunctionalUnitCodes = "'" & _payrollFunctionalUnitXpo.Codigo & "'"
                        Formulario.PatientCode = patient.IPCODPACI.Trim()
                        Formulario.ListCupsCodes = listValidationSusceptible
                        Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
                        Formulario.ViewModeEditHold = True
                        Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Formulario.ToolBar.Visible = False
                        Formulario.Width = 900
                        Formulario.Height = 500
                        Dim frm As New FrmTransparent(Formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        frm.ShowDialog(Me)

                        If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                            AsyncLoader(False)
                            INDsbAddCita.Enabled = True
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            Return False
                        End If

                        ListViewTraceabilityPaperworkAuthorizedXpo = Formulario.ListViewTraceabilityPaperworkAuthorizedXpo
                    End Using
                End If
            End If

            Dim arg As Object = GenerateSendData(listCitas, ListViewTraceabilityPaperworkAuthorizedXpo)
            arg.unSetInvoice = unSetInvoice
            arg.EntityName = "ControlOutPatientServices"
            Dim result As ActionResult(Of List(Of List(Of CupsHomologation))) = Await model.GetHomologationsCups(arg, CInt(INDSleCareGroup.EditValue))
            If Not result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                MultipleHomologation = True
                'Guardar la cita antes de abrir el popup
                citasToAddWithHomologation = listCitas
                Dim formulario As New FrmHomologationsCups
                AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                formulario.CareCenterCode = CareCenterCode
                formulario.ListHomologation = result.ObjectEmbbeded
                formulario.arguments = arg
                formulario.CanClose = False
                Dim transParent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transParent.ShowDialog(Me)
                AsyncLoader(False)
                INDsbAddCita.Enabled = True
                Return False
            ElseIf result.StateResult AndAlso result.ObjectEmbbeded.Count > 0 Then
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Dim args As New HomologationCupsEventArgs
                args.IsProcedureQx = False
                Dim listHomologationReturn As New List(Of List(Of CupsHomologation))
                result.ObjectEmbbeded.ForEach(Sub(o)
                                                  o(0).Activated = True
                                                  If o.Count = 1 Then
                                                      listHomologationReturn.Add(o)
                                                  Else
                                                      listHomologationReturn.Add(o.Where(Function(x) x.Activated = True).ToList())
                                                  End If
                                              End Sub)
                args.ListHomologations = listHomologationReturn
                args.arguments = arg
                Await ReturnSetHomologation(Me, args)
                Return True
            Else
                AsyncLoader(False)
                INDsbAddCita.Enabled = True
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Return False
            End If
        End Using
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_hemocomponent"></param>
    ''' <param name="unSetInvoice"></param>
    ''' <returns></returns>
    Private Async Function CreateServiceOrderDetailItemHemocomponent(_hemocomponent As Hemocomponent, unSetInvoice As Boolean) As Task(Of Boolean)
        Using model As New MControlOutpatientServices(Me.Tag)
            AsyncLoader(True)
            INDSbHemoAdd.Enabled = False
            INDSbHemoServiceAdd.Enabled = False
            Me.Cursor = ChangeCursorIndigo()


            If ListHemocomponent.Any(Function(x) x.Details.Any()) Then
                Dim arg As Object = GenerateSendDataHemocomponent(_hemocomponent)
                arg.unSetInvoice = unSetInvoice
                arg.EntityName = "ControlOutPatientServices"
                Dim result As ActionResult(Of List(Of List(Of CupsHomologation))) = Await model.GetHomologationsCups(arg, CInt(INDSleCareGroup.EditValue))
                If Not result.StateResult AndAlso result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    Dim formulario As New FrmHomologationsCups
                    AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.CareCenterCode = CareCenterCode
                    formulario.ListHomologation = result.ObjectEmbbeded
                    formulario.arguments = arg
                    formulario.CanClose = False
                    Dim transParent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transParent.ShowDialog(Me)
                    AsyncLoader(False)
                    If formulario.DialogResult = System.Windows.Forms.DialogResult.OK Then
                        Return True
                    End If
                    INDSbHemoAdd.Enabled = True
                    INDSbHemoServiceAdd.Enabled = True
                    Return False
                ElseIf result.StateResult AndAlso result.ObjectEmbbeded.Count > 0 Then
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    Dim args As New HomologationCupsEventArgs
                    args.IsProcedureQx = False
                    Dim listHomologationReturn As New List(Of List(Of CupsHomologation))
                    result.ObjectEmbbeded.ForEach(Sub(o)
                                                      o(0).Activated = True
                                                      If o.Count = 1 Then
                                                          listHomologationReturn.Add(o)
                                                      Else
                                                          listHomologationReturn.Add(o.Where(Function(x) x.Activated = True).ToList())
                                                      End If
                                                  End Sub)
                    args.ListHomologations = listHomologationReturn
                    args.arguments = arg
                    Await ReturnSetHomologation(Me, args)
                    Return True
                Else
                    AsyncLoader(False)
                    INDSbHemoAdd.Enabled = True
                    INDSbHemoServiceAdd.Enabled = True
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Return False
                End If
            Else
                AsyncLoader(False)
                INDSbHemoAdd.Enabled = True
                INDSbHemoServiceAdd.Enabled = True
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Return False
            End If
        End Using
    End Function

    Private Async Function ReturnSetHomologation(sender As Object, e As HomologationCupsEventArgs) As Task
        isLoadingHomologation = MultipleHomologation
        If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
            _stopAggregateCita = False
            CType(sender, FrmHomologationsCups).AsyncLoader(True)
            Me.AsyncLoader(True)
        End If
        Dim arg As Object = e.arguments
        arg.AdmissionNumber = If(admission IsNot Nothing, admission.AdmissionCode.ToString().Trim(), String.Empty)
        arg.CareCenterCode = CareCenterCode
        arg.PatientCode = patient.IPCODPACI.Trim()
        arg.OperativeUnitId = 0
        arg.IsProcedureQx = False
        arg.AutorizationNumber = AuthorizationNumber
        Using m As New MAccountControl(Me.Tag)

            'Variable que permite saber si se va a importar un detalle de cotización
            Dim processImportQuotation As Boolean = False

            'Se valida si los cups necesitan asociar una cotización
            Dim messageQuoted = m.GetCUPSWithQuoted(arg, CInt(INDSleCareGroup.EditValue))
            If Not String.IsNullOrEmpty(messageQuoted.Item1) Then
                If MessageIndigo.Show(messageQuoted.Item1, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    If Me.RequestQuoteServices Then
                        If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                            CType(sender, FrmHomologationsCups).AsyncLoader(False)
                        Else
                            _stopAggregateCita = True
                        End If

                        AsyncLoader(False)
                        INDsbAddCita.Enabled = True
                        INDSbHemoAdd.Enabled = True
                        INDSbHemoServiceAdd.Enabled = True
                        isLoadingHomologation = False
                        'Limpiar también aquí en caso de cancelación
                        citasToAddWithHomologation = Nothing
                        Exit Function
                    End If
                Else
                    processImportQuotation = True
                End If
            End If

            If processImportQuotation Then 'Si se ejecuta el proceso de importación de cotización
                QuotationProcess(arg, sender, messageQuoted.Item2)
            Else 'Si se ejecuta el proceso normal para agregar un detalle de orden de servicio
                Await NormalProcessAsync(e.ListHomologations, arg, sender)
            End If
        End Using
        AsyncLoader(False)
        INDsbAddCita.Enabled = True
        INDSbHemoAdd.Enabled = True
        INDSbHemoServiceAdd.Enabled = True
        isLoadingHomologation = False
        citasToAddWithHomologation = Nothing
    End Function

    ''' <summary>
    ''' Método que genera la orden de servicio con el proceso normal
    ''' </summary>
    Private Async Function NormalProcessAsync(listHomologations As List(Of List(Of CupsHomologation)), arg As Object, sender As Object) As Task
        Using m As New MAccountControl(Me.Tag)
            Dim res As ActionResult(Of ServiceOrder) = Await m.GetServiceOrderDetailHomologation(CInt(INDSleCareGroup.EditValue), listHomologations, arg)
            If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                CType(sender, FrmHomologationsCups).AsyncLoader(False)
            End If
            If res.StateResult Then
                If res.ObjectEmbbeded.ServiceOrderDetail.Any(Function(x) x.Presentation = 2) Then
                    OpenFormServiceOrderDetailHomologation(res.ObjectEmbbeded)
                    AsyncLoader(False)
                    INDsbAddCita.Enabled = True
                    INDSbHemoAdd.Enabled = True
                    INDSbHemoServiceAdd.Enabled = True
                    If isLoadingHomologation AndAlso Not _stopAggregateCita Then
                        If citasToAddWithHomologation IsNot Nothing Then
                            AddCitas(citasToAddWithHomologation)
                            citasToAddWithHomologation = Nothing
                        Else
                            AddCitas(citasToAdd)
                        End If
                    End If
                    If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                        CType(sender, FrmHomologationsCups).DialogResult = System.Windows.Forms.DialogResult.OK
                    End If
                    Exit Function
                End If

                Dim listServiceOrderDetailPopup = res.ObjectEmbbeded.ServiceOrderDetail.ToList()
                For Each item In listServiceOrderDetailPopup
                    item.OperatingUnitId = BarraBotones.OperatingUnitValue
                    item.PatientCode = patient.IPCODPACI.Trim()
                    item.AdmissionNumber = If(admission IsNot Nothing, admission.AdmissionCode.ToString().Trim(), String.Empty)
                Next

                Using model As New MServiceOrder(Me.Tag)
                    Dim result = Await model.ValidateServiceOrderDetail(listServiceOrderDetailPopup)
                    If Not result.StateResult Then
                        If Not AllowsAddAuthorizationControl Then
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            AsyncLoader(False)
                            _stopAggregateCita = True
                            INDsbAddCita.Enabled = True
                            INDSbHemoAdd.Enabled = True
                            INDSbHemoServiceAdd.Enabled = True
                            If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                                CType(sender, FrmHomologationsCups).DialogResult = System.Windows.Forms.DialogResult.OK
                            End If
                            Exit Function
                        End If

                        Using popUp As New FrmPopupServiceOrderDetailControl()
                            Dim transparent As New FrmTransparent(popUp, False)
                            If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                AsyncLoader(False)
                                _stopAggregateCita = True
                                INDsbAddCita.Enabled = True
                                INDSbHemoAdd.Enabled = True
                                INDSbHemoServiceAdd.Enabled = True
                                If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                                    CType(sender, FrmHomologationsCups).DialogResult = System.Windows.Forms.DialogResult.OK
                                End If
                                Exit Function
                            End If

                            For Each item In listServiceOrderDetailPopup
                                item.IsServiceOrderDetailControlJustify = True
                                item.ServiceOrderDetailControlJustification = popUp.Justify
                            Next
                        End Using
                    End If
                End Using

                If _listServiceOrderDetail Is Nothing Then
                    _listServiceOrderDetail = New List(Of ServiceOrderDetail)()
                End If
                For Each sod In res.ObjectEmbbeded.ServiceOrderDetail
                    Using model As New Presentation.Contract.MVP.MCareGroup(Me.Tag)
                        Dim careGroupTmp = model.GetCareGroupByIdSimple(CareGroupId).ObjectEmbbeded
                        If careGroupTmp IsNot Nothing AndAlso careGroupTmp.Id > 0 Then
                            If careGroupTmp.CareGroupType = 3 Then
                                sod.HealthAdministratorId = Nothing
                                Using model1 As New MThirdParty(Me.Tag)
                                    Dim thirdParty1 = model1.GetThirdParty(patient.IPCODPACI.Trim())
                                    sod.ThirdPartyId = thirdParty1.Id
                                End Using

                            Else
                                sod.ThirdPartyId = HealthAdministrator.ThirdPartyId
                                sod.HealthAdministratorId = HealthAdministratorId
                            End If
                        Else
                            sod.ThirdPartyId = HealthAdministrator.ThirdPartyId
                            sod.HealthAdministratorId = HealthAdministratorId
                        End If
                    End Using
                Next
                _listServiceOrderDetail.AddRange(listServiceOrderDetailPopup)
                ListServiceOrderDetail = _listServiceOrderDetail

                INDSleCareGroup.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0
                INDSleHealthAdministrator.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0

                If CBool(arg.unSetInvoice) Then
                    CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).InvoiceId = Nothing
                    CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).InvoiceNumber = String.Empty
                    CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).AdmissionNumberInvoice = String.Empty
                    CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).CareGroupIdInvoice = Nothing
                    CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).HealthAdministratorIdInvoice = Nothing
                End If
                ctrInfo.PrintInfo()

                If isLoadingHomologation Then
                    'Usar la cita guardada en lugar de citasToAdd
                    If citasToAddWithHomologation IsNot Nothing Then
                        AddCitas(citasToAddWithHomologation)
                        citasToAddWithHomologation = Nothing  ' Limpiar después de usar
                    Else
                        AddCitas(citasToAdd)
                    End If
                End If

                If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                    CType(sender, FrmHomologationsCups).DialogResult = System.Windows.Forms.DialogResult.OK
                End If
            Else
                _stopAggregateCita = True
                Mensaje(EeventViewerImages.Advertencia) = res.Message
            End If
        End Using
    End Function

    ''' <summary>
    ''' metodo para agregar citas 
    ''' </summary>
    Private Sub AddCitas(_citasToAdd As List(Of SP_AD_ListarCitasMedicasNativo_Result))
        _listaCitasMedicas.AddRange(_citasToAdd)
        ListCitasMedicas = _listaCitasMedicas
        If CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)) IsNot Nothing Then
            CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).ForEach(Sub(x)
                                                                                                                         x.Sel = False
                                                                                                                     End Sub)
        End If

        INDGleMedicalAppointmentType.EditValue = Nothing
        INDlyItemApplyRIAS.HideControl()
        INDlyItemRIASCups.HideControl()
        INDlyItemDescription.HideControl()

        INDGcMedicalAppointmentPatient.RefreshDataSource()
        INDPceMedicalAppointment.ClosePopup()
        INDGvMedicalAppointment.FocusedRowHandle = 0
        INDGvMedicalAppointment.FocusedColumn = ColInvoice
        ctrInfo.PrintInfo()
    End Sub

    ''' <summary>
    ''' Método que genera la orden de servicio con la cotización seleccionada
    ''' </summary>
    Private Sub QuotationProcess(arg As Object, sender As Object, listCupsIds As List(Of Integer))
        If sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
            CType(sender, FrmHomologationsCups).AsyncLoader(False)
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportQuotation()
            AddHandler Formulario.ImportEvent, AddressOf ImportInfo
            Formulario.PatientCode = INDBePatient.EditValue.ToString().Split(" - ")(0).Trim()
            Formulario.ListCupsEntityId = listCupsIds
            Formulario.arg = arg
            Formulario.senderForm = sender
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1054
            Formulario.Height = 500
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)

            If Formulario.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                _stopAggregateCita = True
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Importa la información de la cotización
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddImportQuotationServiceOrderDetail)
        If e IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo.Count > 0 Then
            If _listServiceOrderDetail Is Nothing Then
                _listServiceOrderDetail = New List(Of ServiceOrderDetail)()
            End If

            For Each quotationServiceOrderDetailXpo In e.ListQuotationServiceOrderDetailXpo
                If serviceOrderDetail Is Nothing Then
                    serviceOrderDetail = New ServiceOrderDetail
                End If
                With serviceOrderDetail
                    .QuotationServiceOrderDetailId = quotationServiceOrderDetailXpo.Id
                    .QuotationCode = quotationServiceOrderDetailXpo.QuotationId.Code
                    .CareGroupId = quotationServiceOrderDetailXpo.CareGroupId.Id
                    .CodeNameCareGroup = quotationServiceOrderDetailXpo.CareGroupId.CodeName
                    If quotationServiceOrderDetailXpo.HealthAdministratorId IsNot Nothing Then
                        .HealthAdministratorId = quotationServiceOrderDetailXpo.HealthAdministratorId.Id
                        .CodeNameHealthAdministrator = quotationServiceOrderDetailXpo.HealthAdministratorId.Code + " - " + quotationServiceOrderDetailXpo.HealthAdministratorId.Name
                    End If
                    If quotationServiceOrderDetailXpo.ThirdPartyId IsNot Nothing Then
                        .ThirdPartyId = quotationServiceOrderDetailXpo.ThirdPartyId.Id
                        .NitNameThirdParty = quotationServiceOrderDetailXpo.ThirdPartyId.NitName
                    End If
                    .ServiceType = quotationServiceOrderDetailXpo.ServiceType
                    .RecordType = quotationServiceOrderDetailXpo.RecordType
                    If quotationServiceOrderDetailXpo.CUPSEntityId IsNot Nothing Then
                        .CUPSEntityId = quotationServiceOrderDetailXpo.CUPSEntityId.Id
                        .CodeNameCups = quotationServiceOrderDetailXpo.CUPSEntityId.CodeDescription
                    End If
                    If quotationServiceOrderDetailXpo.IPSServiceId IsNot Nothing Then
                        .IPSServiceId = quotationServiceOrderDetailXpo.IPSServiceId.Id
                        .CodeNameIpsService = quotationServiceOrderDetailXpo.IPSServiceId.CodeName
                    End If
                    If quotationServiceOrderDetailXpo.HospitalStayId > 0 Then
                        .HospitalStayId = quotationServiceOrderDetailXpo.HospitalStayId
                    End If
                    If quotationServiceOrderDetailXpo.HospitalStayDetailId > 0 Then
                        .HospitalStayDetailId = quotationServiceOrderDetailXpo.HospitalStayDetailId
                    End If
                    If quotationServiceOrderDetailXpo.ControlExternalConsultation > 0 Then
                        .ControlExternalConsultation = quotationServiceOrderDetailXpo.ControlExternalConsultation
                    End If
                    If quotationServiceOrderDetailXpo.ControlExternalConsultationCode > 0 Then
                        .ControlExternalConsultationCode = quotationServiceOrderDetailXpo.ControlExternalConsultationCode
                    End If
                    .CUPSAssociateService = quotationServiceOrderDetailXpo.CUPSAssociateService
                    If quotationServiceOrderDetailXpo.CodeAssociateService IsNot Nothing Then
                        .CodeAssociateService = quotationServiceOrderDetailXpo.CodeAssociateService
                    End If
                    .IsPackage = quotationServiceOrderDetailXpo.IsPackage
                    .Packaging = quotationServiceOrderDetailXpo.Packaging
                    If quotationServiceOrderDetailXpo.PackageServiceOrderDetailId <> Nothing Then
                        .PackageServiceOrderDetailId = quotationServiceOrderDetailXpo.PackageServiceOrderDetailId
                    End If
                    .LiquidationType = quotationServiceOrderDetailXpo.LiquidationType
                    .Presentation = quotationServiceOrderDetailXpo.Presentation
                    If quotationServiceOrderDetailXpo.ProductId IsNot Nothing Then
                        .ProductId = quotationServiceOrderDetailXpo.ProductId.Id
                        .CodeNameProduct = quotationServiceOrderDetailXpo.ProductId.CodeName
                    End If
                    .InvoicedQuantity = quotationServiceOrderDetailXpo.InvoicedQuantity
                    .SupplyQuantity = quotationServiceOrderDetailXpo.SupplyQuantity
                    .DevolutionQuantity = quotationServiceOrderDetailXpo.DevolutionQuantity
                    .RateManualSalePrice = quotationServiceOrderDetailXpo.RateManualSalePrice
                    .CostValue = quotationServiceOrderDetailXpo.CostValue
                    .ServiceDate = quotationServiceOrderDetailXpo.ServiceDate
                    If quotationServiceOrderDetailXpo.AuthorizationNumber IsNot Nothing Then
                        .AuthorizationNumber = quotationServiceOrderDetailXpo.AuthorizationNumber
                    End If
                    If quotationServiceOrderDetailXpo.PerformsFunctionalUnitId IsNot Nothing Then
                        .PerformsFunctionalUnitId = quotationServiceOrderDetailXpo.PerformsFunctionalUnitId.Id
                        .CodeNameFunctionalUnit = quotationServiceOrderDetailXpo.PerformsFunctionalUnitId.Code + " - " + quotationServiceOrderDetailXpo.PerformsFunctionalUnitId.Name
                    End If
                    If quotationServiceOrderDetailXpo.PerformsHealthProfessionalCode IsNot Nothing Then
                        .PerformsHealthProfessionalCode = quotationServiceOrderDetailXpo.PerformsHealthProfessionalCode
                    End If
                    If quotationServiceOrderDetailXpo.PerformsProfessionalSpecialty IsNot Nothing Then
                        .PerformsProfessionalSpecialty = quotationServiceOrderDetailXpo.PerformsProfessionalSpecialty
                    End If
                    If quotationServiceOrderDetailXpo.PerformsHealthProfessionalThirdPartyId <> Nothing Then
                        .PerformsHealthProfessionalThirdPartyId = quotationServiceOrderDetailXpo.PerformsHealthProfessionalThirdPartyId
                    End If
                    If quotationServiceOrderDetailXpo.BillingConceptId IsNot Nothing Then
                        .BillingConceptId = quotationServiceOrderDetailXpo.BillingConceptId.Id
                    End If

                    .CostCenterId = quotationServiceOrderDetailXpo.CostCenterId.Id
                    .CodeNameCostCenter = quotationServiceOrderDetailXpo.CostCenterId.Code + " - " + quotationServiceOrderDetailXpo.CostCenterId.Name
                    .SettlementType = quotationServiceOrderDetailXpo.SettlementType
                    If quotationServiceOrderDetailXpo.IncludeServiceOrderDetailId <> Nothing Then
                        .IncludeServiceOrderDetailId = quotationServiceOrderDetailXpo.IncludeServiceOrderDetailId
                    End If
                    .RecoveryRatio = quotationServiceOrderDetailXpo.RecoveryRatio

                    If quotationServiceOrderDetailXpo.RateManualId > 0 Then
                        .RateManualId = quotationServiceOrderDetailXpo.RateManualId
                    End If
                    If quotationServiceOrderDetailXpo.RateManualType > 0 Then
                        .RateManualType = quotationServiceOrderDetailXpo.RateManualType
                    End If
                    If quotationServiceOrderDetailXpo.RateManualDetailId IsNot Nothing Then
                        .RateManualDetailId = quotationServiceOrderDetailXpo.RateManualDetailId.Id
                    End If
                    If quotationServiceOrderDetailXpo.DefinitionRateDetailId > 0 Then
                        .DefinitionRateDetailId = quotationServiceOrderDetailXpo.DefinitionRateDetailId
                    End If
                    If quotationServiceOrderDetailXpo.DefinitionRateDetailConditionId > 0 Then
                        .DefinitionRateDetailConditionId = quotationServiceOrderDetailXpo.DefinitionRateDetailConditionId
                    End If
                    .SubTotalSalesPrice = quotationServiceOrderDetailXpo.SubTotalSalesPrice
                    .ThirdPartyDiscount = quotationServiceOrderDetailXpo.ThirdPartyDiscount
                    .ThirdPartyDiscountPercentage = quotationServiceOrderDetailXpo.ThirdPartyDiscountPercentage
                    .TotalSalesPrice = quotationServiceOrderDetailXpo.TotalSalesPrice
                    .GrandTotalSalesPrice = quotationServiceOrderDetailXpo.GrandTotalSalesPrice
                    .SurchargeApply = quotationServiceOrderDetailXpo.SurchargeApply
                    If quotationServiceOrderDetailXpo.SurgicalInterventionType > 0 Then
                        .SurgicalInterventionType = quotationServiceOrderDetailXpo.SurgicalInterventionType
                    End If
                    .SurgeryNumber = quotationServiceOrderDetailXpo.SurgeryNumber
                    .IsFirstEvent = quotationServiceOrderDetailXpo.IsFirstEvent
                    .IsAnnulled = quotationServiceOrderDetailXpo.IsAnnulled
                    .IsDelete = quotationServiceOrderDetailXpo.IsDelete
                    .IncomeMainAccountId = quotationServiceOrderDetailXpo.IncomeMainAccountId

                    .ApplyRIAS = Nothing
                    .RIASCupsId = Nothing
                    If quotationServiceOrderDetailXpo.ApplyRIAS IsNot Nothing Then
                        .ApplyRIAS = quotationServiceOrderDetailXpo.ApplyRIAS
                        If quotationServiceOrderDetailXpo.RIASCupsId IsNot Nothing AndAlso quotationServiceOrderDetailXpo.RIASCupsId > 0 Then
                            .RIASCupsId = quotationServiceOrderDetailXpo.RIASCupsId
                        End If
                    End If

                    .CUPSEntityContractDescriptionId = Nothing
                    If quotationServiceOrderDetailXpo.CUPSEntityContractDescriptionId IsNot Nothing Then
                        .CUPSEntityContractDescriptionId = quotationServiceOrderDetailXpo.CUPSEntityContractDescriptionId.Id
                        .ContractDescriptionCodeName = quotationServiceOrderDetailXpo.CUPSEntityContractDescriptionId.ContractDescriptionId.CodeName
                    End If
                End With
                For Each quotationServiceOrderDetailSurgicalXpo In quotationServiceOrderDetailXpo.QuotationServiceOrderDetailSurgicalXpo
                    Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                    With serviceOrderDetailSurgical
                        .ServiceOrderDetailId = 0
                        .IPSServiceId = quotationServiceOrderDetailSurgicalXpo.IPSServiceId.Id
                        .CodeNameIpsService = quotationServiceOrderDetailSurgicalXpo.IPSServiceId.CodeName
                        Select Case quotationServiceOrderDetailSurgicalXpo.IPSServiceId.ServiceClass
                            Case 1
                                .ClassServiceIps = "Ninguno"
                            Case 2
                                .ClassServiceIps = "Cirujano"
                            Case 3
                                .ClassServiceIps = "Anestesiólogo"
                            Case 4
                                .ClassServiceIps = "Ayudante"
                            Case 5
                                .ClassServiceIps = "Derecho Sala"
                            Case 6
                                .ClassServiceIps = "Materiales Sutura"
                            Case 7
                                .ClassServiceIps = "Instrumentación Quirúrgica"
                        End Select
                        .InvoicedQuantity = quotationServiceOrderDetailSurgicalXpo.InvoicedQuantity
                        .LiquidationPercentage = quotationServiceOrderDetailSurgicalXpo.LiquidationPercentage
                        .RateManualSalePrice = quotationServiceOrderDetailSurgicalXpo.RateManualSalePrice
                        .TotalSalesPrice = quotationServiceOrderDetailSurgicalXpo.TotalSalesPrice
                        If quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalCode IsNot Nothing Then
                            .PerformsHealthProfessionalCode = quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalCode
                        End If
                        If quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalThirdPartyId <> Nothing Then
                            .PerformsHealthProfessionalThirdPartyId = quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalThirdPartyId
                        End If
                        .CostValue = quotationServiceOrderDetailSurgicalXpo.CostValue
                        .BillingConceptId = quotationServiceOrderDetailSurgicalXpo.BillingConceptId.Id
                        .CostCenterId = quotationServiceOrderDetailSurgicalXpo.CostCenterId
                        If quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId IsNot Nothing Then
                            .RateManualDetailSurgicalId = quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId.Id
                        End If
                        .SurchargeApply = quotationServiceOrderDetailSurgicalXpo.SurchargeApply
                        .OnlyMedicalFees = quotationServiceOrderDetailSurgicalXpo.OnlyMedicalFees
                        .IncomeMainAccountId = quotationServiceOrderDetailSurgicalXpo.IncomeMainAccountId
                    End With
                    serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
                Next

                _listServiceOrderDetail.Add(serviceOrderDetail)
            Next

            If e.sender.GetType().Name = GetType(FrmHomologationsCups).Name Then
                CType(e.sender, FrmHomologationsCups).DialogResult = System.Windows.Forms.DialogResult.OK
            End If

            ListServiceOrderDetail = _listServiceOrderDetail
            INDSleCareGroup.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0
            INDSleHealthAdministrator.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0

            If CBool(e.arg.unSetInvoice) Then
                CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).InvoiceId = Nothing
                CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).InvoiceNumber = String.Empty
                CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).AdmissionNumberInvoice = String.Empty
                CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).CareGroupIdInvoice = Nothing
                CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result).HealthAdministratorIdInvoice = Nothing
            End If
            ctrInfo.PrintInfo()
        End If
    End Sub

    Private Sub OpenFormServiceOrderDetailHomologation(resultServiceOrder As ServiceOrder)
        Dim listServiceOrderDetailQx = resultServiceOrder.ServiceOrderDetail.ToList()
        Dim listServiceOrderDetailPopup = resultServiceOrder

        Me.Cursor = ChangeCursorIndigo()
        Me.AsyncLoader(True)
        Using formulario As New FrmPopUpServiceOrderDetailQx
            formulario.Size = New System.Drawing.Size(800, 700)
            'formulario.CloseBox = False
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.listServiceOrderDetailPopup = listServiceOrderDetailPopup.ServiceOrderDetail.ToList()
            formulario.Admission = If(admission IsNot Nothing, admission.AdmissionCode.ToString().Trim(), String.Empty)
            formulario.CenterAttentionCode = CareCenterCode
            formulario.Patient = INDTxtPatient.Text
            formulario.Stay = INDTxtStay.Text

            formulario.PatientDateBirth = patient.IPFECNACI
            formulario.PatientGenus = patient.IPSEXOPAC
            formulario.AdmissionDate = Date.Now
            formulario.EditMode = False
            formulario.Patient = String.Concat(patient.IPCODPACI.Trim(), " - ", patient.IPNOMCOMP.Trim())
            AddHandler formulario.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail
            If listServiceOrderDetailPopup.ServiceOrderDetail.ToList() IsNot Nothing Then
                formulario.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetailPopup.ServiceOrderDetail.ToList()
                formulario.ListServiceOrderDetailDatasourceIncludeService = listServiceOrderDetailPopup.ServiceOrderDetail.ToList()
            Else
                formulario.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
                formulario.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
            End If
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                _stopAggregateCita = True
            End If
        End Using
    End Sub

    Private Function GenerateSendData(listCitas As List(Of SP_AD_ListarCitasMedicasNativo_Result), listViewTraceabilityPaperworkAuthorizedXpo As List(Of ViewTraceabilityPaperworkAuthorizedXpo)) As Object
        Dim arg As Object = New ExpandoObject()
        'AsyncLoader(True)
        Dim myListDetail As New ConcurrentBag(Of Object)()
        Parallel.ForEach(listCitas, Sub(obj As SP_AD_ListarCitasMedicasNativo_Result)
                                        Dim detail As Object = New ExpandoObject()
                                        detail.CupsEntityCode = obj.CodigoServicio
                                        detail.TipoSolicitud = obj.TipoSolicitud '1 - Cita Medica; 2 - Apoyo Dx; 3 - Tratamiento Especial
                                        detail.FunctionalUnitCode = INDSleFunctionalUnit.Text.Split(" - ")(0).Trim() 'Me.FunctionalUnitCodeAdmission
                                        detail.ProfessionalCode = obj.CodigoProfesional
                                        detail.Date = obj.FechaCita
                                        detail.ProfessionalSpecialistCode = obj.CodigoEspecialidad
                                        detail.PatientCode = patient.IPCODPACI.Trim()
                                        detail.Quantity = If(obj.CantidadServicio = 0, 1, obj.CantidadServicio)
                                        detail.NitMedico = obj.NitMedico
                                        detail.IdCita = obj.Codigo
                                        detail.RiasCupsId = If(ApplyRIASCareGroup, obj.RiasCupsId, 0)
                                        detail.RiasId = If(ApplyRIASCareGroup, obj.RiasId, 0)

                                        If INDlyItemApplyRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                            detail.ApplyRIAS = INDsleApplyRIAS.EditValue
                                        Else
                                            If ApplyRIASCareGroup AndAlso obj.RiasCupsId > 0 Then
                                                detail.ApplyRIAS = 1
                                            End If
                                        End If

                                        detail.CUPSEntityContractDescriptionId = obj.CUPSEntityContractDescriptionId
                                        detail.ContractDescriptionId = obj.ContractDescriptionId

                                        'Si hay un proceso de autorización se busca el código cups dentro del listado para asignar el id del evento a la orden de servicio
                                        If listViewTraceabilityPaperworkAuthorizedXpo IsNot Nothing AndAlso listViewTraceabilityPaperworkAuthorizedXpo.Count > 0 Then
                                            Dim info = (From x In listViewTraceabilityPaperworkAuthorizedXpo Where x.ServiceCode.Trim() = obj.CodigoServicio.Trim() Select x).FirstOrDefault()
                                            If info IsNot Nothing Then
                                                detail.TraceabilityPaperworkEventsId = info.TraceabilityPaperworkEventsId
                                                detail.AuthorizationNumber = info.AuthorizationNumber
                                            End If
                                        Else
                                            detail.AuthorizationNumber = AuthorizationNumber
                                        End If

                                        'detail.ClaseCita = obj.TipoCita
                                        myListDetail.Add(detail)
                                    End Sub)
        arg.Details = myListDetail
        Return arg
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_hemocomponent"></param>
    ''' <returns></returns>
    Private Function GenerateSendDataHemocomponent(_hemocomponent As Hemocomponent) As Object
        Dim arg As Object = New ExpandoObject()
        Dim myListDetail As New ConcurrentBag(Of Object)()
        Parallel.ForEach(Of HemocomponentDetail)(_hemocomponent.Details, Sub(obj As HemocomponentDetail)

                                                                             Dim detail As Object = New ExpandoObject()
                                                                             detail.CupsEntityCode = obj.CodeServiceIPS
                                                                             detail.TipoSolicitud = 1 '1 - Cita Medica; 2 - Apoyo Dx; 3 - Tratamiento Especial
                                                                             detail.FunctionalUnitCode = INDSleFunctionalUnit.Text.Split(" - ")(0).Trim() 'Me.FunctionalUnitCodeAdmission
                                                                             detail.ProfessionalCode = _hemocomponent.ProfessionalId
                                                                             detail.Date = DateTime.Now
                                                                             detail.ProfessionalSpecialistCode = _hemocomponent.SpecialityCode
                                                                             detail.PatientCode = patient.IPCODPACI.Trim()
                                                                             detail.Quantity = _hemocomponent.Quantity
                                                                             detail.NitMedico = thirdParty.Nit
                                                                             detail.IdCita = 0
                                                                             detail.HemocomponentId = _hemocomponent.Id
                                                                             detail.RiasCupsId = 0
                                                                             detail.RiasId = 0
                                                                             detail.ApplyRIAS = 0
                                                                             detail.ContractDescriptionId = obj.IdRelatedDescription

                                                                             myListDetail.Add(detail)
                                                                         End Sub)

        arg.Details = myListDetail
        Return arg
    End Function

    Private Sub DeleteServiceOrderDetailByCitaId(citaId As Integer, Optional isEditingAppointment As Boolean = False)
        If _listServiceOrderDetail IsNot Nothing AndAlso _listServiceOrderDetail.Any(Function(o) o.IdCita = citaId) Then
            _listServiceOrderDetail.RemoveAll(Function(o) o.IdCita = citaId)
            ListServiceOrderDetail = _listServiceOrderDetail

            INDSleCareGroup.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0
            INDSleHealthAdministrator.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0
            ctrInfo.PrintInfo()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_hemocomponentId"></param>
    Private Sub DeleteServiceOrderDetailByHemocomponentId(_hemocomponentId As Integer)
        If _listServiceOrderDetail IsNot Nothing AndAlso _listServiceOrderDetail.Any(Function(o) o.HemocomponentId = _hemocomponentId) Then
            _listServiceOrderDetail.RemoveAll(Function(o) o.HemocomponentId = _hemocomponentId)
            ListServiceOrderDetail = _listServiceOrderDetail
            INDSleCareGroup.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0
            INDSleHealthAdministrator.Properties.ReadOnly = ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0
            ctrInfo.PrintInfo()
        End If
    End Sub

    Private Sub OpenPopUpServiceOrderDetail(edit As Boolean)
        If Not edit AndAlso admission Is Nothing Then
            INDliTipoRiesgo.ShowInCustomizationForm = False
            INDliCausa.ShowInCustomizationForm = False
            INDliAuthorization.ShowInCustomizationForm = False
            SetCustomizationFormBasedOnCulture()
            'Llenar datos de ingresos
            Dim resValidation = ValidateField(INDlcAdmission)
            INDliTipoRiesgo.ShowInCustomizationForm = True
            INDliCausa.ShowInCustomizationForm = True
            INDliAuthorization.ShowInCustomizationForm = True
            INDliIdEntryRoutesHealthServices.ShowInCustomizationForm = True
            INDliIdHealthPurposes.ShowInCustomizationForm = True
            INDliIdAdmissionModalities.ShowInCustomizationForm = True
            'SEGUIR ACA CON LA VALIDACION
            If Not resValidation.ResultStatus Then
                Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), resValidation.ToString())
                INDPceAdmissionData.Focus()
                INDPceAdmissionData.ShowPopup()
                controlValidate.Focus()
                Exit Sub
            End If
        End If
        Me.Cursor = ChangeCursorIndigo()
        popupService = New FrmPopupServicesControlOutPut
        popupService.Size = New System.Drawing.Size(800, 700)
        popupService.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        popupService.Admission = ""
        popupService.Patient = INDTxtPatient.Text
        popupService.Stay = INDTxtStay.Text
        popupService.PatientDateBirth = patient.IPFECNACI
        popupService.PatientGenus = patient.IPSEXOPAC
        popupService.AdmissionDate = Date.Now 'patient.IFECHAING
        popupService.Patient = String.Concat(patient.IPCODPACI.Trim(), " - ", patient.IPNOMCOMP.Trim())
        popupService.PermissionsForm = PermissionsForm
        popupService.TaxInclude = Me._flagTaxInclude
        'si tienen permiso de modifica preliquidacion y esta relacionado a  una cita activa la bandera
        If PermissionsForm.ContainsKey(139) AndAlso
            ((Not String.IsNullOrEmpty(serviceOrderDetail?.IdCita) AndAlso Not String.IsNullOrEmpty(serviceOrderDetail?.IdCita <> 0)) OrElse serviceOrderDetail?.HemocomponentId <> 0) Then
            popupService.FlagModPreCita = True
        End If
        If edit Then
            popupService.ServiceOrderDetailEdit = serviceOrderDetail.CloneEntity()
            popupService.ListCompare = ListServiceOrderDetail.Where(Function(item) Not item.Equals(serviceOrderDetail)).ToList()
        Else
            If ListServiceOrderDetail IsNot Nothing Then
                popupService.ListCompare = ListServiceOrderDetail.Select(Function(x) x.CloneEntity()).Cast(Of ServiceOrderDetail).ToList()
            End If
        End If
        popupService.EditMode = edit
        popupService.AutorizationNumber = AuthorizationNumber
        If CareGroupId > 0 Then
            popupService.CareGroupAdmission = CareGroupId.ToString() + "," + INDSleCareGroup.Text
        End If

        If INDlciEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            popupService.HealthAdministratorCrystal = 0
        Else
            If HealthAdministratorId Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una entidad administradora de salud"
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            popupService.HealthAdministratorCrystal = HealthAdministratorId
        End If

        AddHandler popupService.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail
        If ListServiceOrderDetail IsNot Nothing Then
            popupService.ListServiceOrderDetailSurgicalIntervention = ListServiceOrderDetail
            popupService.ListServiceOrderDetailDatasourceIncludeService = ListServiceOrderDetail
        Else
            popupService.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
            popupService.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
        End If
        Dim transparent = New Base.FrmTransparent(popupService, False)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        transparent.ShowDialog(Me)
    End Sub

    Private Sub ReturAddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
        If e.EditMode = False Then
            If ListServiceOrderDetail Is Nothing Then
                ListServiceOrderDetail = New List(Of ServiceOrderDetail)
            End If
            Dim errorsEvent As New StringBuilder
            'recalculamos para saber que item va coomo primer evento y asi aplicar o no los porcentajes
            For Each item In e.ListServiceOrderDetail
                If item.Presentation = 2 Then
                    If item.SurgicalInterventionType <> 1 Then
                        Dim detailFirstEvent = ListServiceOrderDetail.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.IsFirstEvent = True)
                        If detailFirstEvent IsNot Nothing Then
                            If item.SubTotalSalesPrice > detailFirstEvent.SubTotalSalesPrice Then
                                'valido que los items no esten bloqueados o facturados en los folios  
                                Dim listEventsTmp = ListServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.Id > 0)
                                For Each itemEvent In listEventsTmp
                                    If itemEvent.IsPackage Then
                                        errorsEvent.AppendLine($"El servicio {item.CodeNameIpsService} no se puede agregar porque el servicio {itemEvent.CodeNameIpsService} esta empaquetado")
                                        Exit For
                                    End If
                                    Using model As New MServiceOrder(MyTag)
                                        'valido que los items no esten distribuidos
                                        Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                                        If listDistribution.Count > 1 Then
                                            errorsEvent.AppendLine($"El servicio {item.CodeNameIpsService} no se puede agregar porque el servicio {itemEvent.CodeNameIpsService} esta distribuido")
                                            Exit For
                                        End If
                                        'valido que los folios no esten bloqueado o facturados
                                        Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                                        If revenueControlDetail.Status > 1 Then
                                            If revenueControlDetail.Status = 2 Then
                                                errorsEvent.AppendLine($"El servicio {item.CodeNameIpsService} no se puede agregar porque el folio #{(revenueControlDetail.FolioOrder).ToString()} esta facturado")
                                            Else
                                                errorsEvent.AppendLine($"El servicio {item.CodeNameIpsService} no se puede agregar porque el folio #{(revenueControlDetail.FolioOrder).ToString()} esta bloqueado")
                                            End If
                                            Exit For
                                        End If
                                    End Using
                                Next
                                If errorsEvent.Length > 0 Then
                                    Continue For
                                End If
                                item.IsFirstEvent = True
                                If item.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(item)
                                End If
                                detailFirstEvent.IsFirstEvent = False
                                If detailFirstEvent.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(detailFirstEvent)
                                End If
                            Else
                                item.IsFirstEvent = False
                                If item.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(item)
                                End If
                            End If
                        Else
                            item.IsFirstEvent = True
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item)
                            End If
                        End If
                    End If
                End If
                ListServiceOrderDetail.Add(item)
            Next
            ValidateMIVIE()
            If errorsEvent.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsEvent.ToString()
                Exit Sub
            End If
            If sender.GetType().Name.Equals("FrmPopUpServiceOrderDetailQx") Then
                Dim form As FrmPopUpServiceOrderDetailQx = CType(sender, FrmPopUpServiceOrderDetailQx)
                form.AsyncLoader(False)
                form.CanForceClose = True
                form.DialogResult = System.Windows.Forms.DialogResult.OK
            Else
                popupService.ListServiceOrderDetailSurgicalIntervention = ListServiceOrderDetail
                popupService.ListServiceOrderDetailDatasourceIncludeService.AddRange(ListServiceOrderDetail)
            End If
            _serviceValue = ListServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
            INDGcServiceOrderDetail.DataSource = Nothing
            INDGcServiceOrderDetail.DataSource = ListServiceOrderDetail
        Else
            ListServiceOrderDetail.Remove(serviceOrderDetail)
            ListServiceOrderDetail.Insert(indexEditItem, e.ServiceOrderDetail)
            INDGcServiceOrderDetail.DataSource = Nothing
            INDGcServiceOrderDetail.DataSource = ListServiceOrderDetail
            INDGvService.FocusedRowHandle = rowEditing
        End If
        ctrInfo.PrintInfo()
    End Sub

    Private Sub GetValueSurgicalEvenst(serviceOrdeDetailItem As ServiceOrderDetail)
        Using model As New MServiceOrder(Me.Tag)
            Dim SurgeriesPercentageManual = model.GetSurgeriesPercetageManualByRateManualIdInterventionType(serviceOrdeDetailItem.RateManualId, serviceOrdeDetailItem.SurgicalInterventionType)
            If serviceOrdeDetailItem.IsFirstEvent = True Then
                If SurgeriesPercentageManual.MainHundredPercent = False Then
                    For Each item In serviceOrdeDetailItem.ServiceOrderDetailSurgical
                        Select Case item.ClassServiceIps?.ToUpper()
                            Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                                item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.SurgeonPercentage / 100
                            Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                                item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100
                            Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                                item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.AssistantPercentage / 100
                            Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                                item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.RoomPercentage / 100
                            Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                                item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.MaterialsPercentage / 100
                        End Select
                    Next
                End If
            Else
                For Each item In serviceOrdeDetailItem.ServiceOrderDetailSurgical
                    Select Case item.ClassServiceIps?.ToUpper()
                        Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                            item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.SurgeonPercentage / 100
                        Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                            item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100
                        Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                            item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.AssistantPercentage / 100
                        Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                            item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.RoomPercentage / 100
                        Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                            item.TotalSalesPrice = item.TotalSalesPrice * SurgeriesPercentageManual.MaterialsPercentage / 100
                    End Select
                Next
            End If

            With serviceOrdeDetailItem
                '/**************--Segmento Impuestos--**********************/
                'se suman el detalle de los qx
                .SubTotalSalesPrice = .ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                .GrossValue = .SubTotalSalesPrice
                .TaxValue = 0
                'independientmente si viene de detalles qx o no al final en base a la parametrizacion del servicio y el sistema se establece el valor bruto, el iva y el subtotal
                Dim _dictionaryValues = Utils.SetValueSalesPrice(Me._flagTaxInclude,
                                                                  .SubTotalSalesPrice,
                                                                  ?.TaxPercent)
                If?.TaxedService AndAlso _dictionaryValues?.Any() Then
                    .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                    .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                    .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                    If .SubTotalSalesPrice > .RoundService Then
                        .SubTotalSalesPrice = Utils.RoundValue(.SubTotalSalesPrice, .RoundService)
                    End If
                End If
                '/***************************************************************/
                .TotalSalesPrice = .SubTotalSalesPrice - .ThirdPartyDiscount
                .GrandTotalSalesPrice = .TotalSalesPrice * .InvoicedQuantity
            End With
        End Using
    End Sub

    ''' <summary>
    ''' metodo para validar que los items que son MIVIE y sean mas de 2, los dos primeros se liquiden como dice el manual y los demas no se cobren
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateMIVIE()
        Dim listEvents = (From e In ListServiceOrderDetail Where e.SettlementType = 1 Select e.SurgeryNumber).Distinct().ToList()

        For item As Integer = 0 To listEvents.Count - 1 Step 1
            Dim firstEvent = ListServiceOrderDetail.Find(Function(x) x.IsFirstEvent = True And x.SurgeryNumber = listEvents(item))
            Dim index = 2
            Dim listMIVIE = ListServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = listEvents(item) AndAlso x.RateManualType < 3 AndAlso x.SurgicalInterventionType IsNot Nothing AndAlso x.SurgicalInterventionType = 3 And (x.LiquidateAllMIVIE Is Nothing OrElse Not x.LiquidateAllMIVIE))
            If listMIVIE.Count > 2 Then
                listMIVIE = (From l In listMIVIE Order By l.RateManualSalePrice Descending).ToList()
                If listMIVIE.Exists(Function(x) x.IsFirstEvent = True) Then
                    listMIVIE.Remove(firstEvent)
                    index = 1
                End If
                For i As Integer = index To listMIVIE.Count - 1 Step 1
                    listMIVIE.ElementAt(i).SubTotalSalesPrice = 0
                    listMIVIE.ElementAt(i).TotalSalesPrice = 0
                    listMIVIE.ElementAt(i).GrandTotalSalesPrice = 0
                    listMIVIE.ElementAt(i).GrossValue = 0
                    listMIVIE.ElementAt(i).TaxValue = 0
                    For Each itemSurgical In listMIVIE.ElementAt(i).ServiceOrderDetailSurgical
                        itemSurgical.TotalSalesPrice = 0
                    Next
                Next
            End If
        Next
    End Sub

    Public Function fncTipoLiquidacionTipoPaciente(ByVal TipoPaciente As String, ByVal TipoAfiliado As String, CapacidadPago As String) As Integer
        'marco la liquidacion segun aplique
        Select Case TipoPaciente
            Case "1" 'contributivo
                If TipoAfiliado = "2" Then 'beneficiario Then
                    Return 4
                End If
                If TipoAfiliado = "1" Then 'cotizante
                    Return 2
                End If
            Case "1", "6" 'contributivo y desplazado contributivo
                If TipoAfiliado = "1" Then 'cotizante
                    Return 2
                End If
                If TipoAfiliado = "2" Then 'beneficiario
                    Return 2
                End If
                If TipoAfiliado = "3" Then 'adicional
                    Return 2
                End If
                If TipoAfiliado = "4" Then 'jubilado
                    Return 3
                End If
                If TipoAfiliado = "5" Then 'pensionado
                    Return 3
                End If
            Case "2" 'subsidiado
                Return 1
            Case "3" 'vinculado
                If CapacidadPago = "1" OrElse CapacidadPago = "3" Then
                    Return 3
                Else
                    Return 1
                End If
            Case "8" 'desplazado no asegurado
                Return 1
        End Select
        'los demas tipos de pacientes/afiliados no aplica = N/A
        Return 3
    End Function

    Private Sub AssigningValues()
        controlOutPatientServices = New ControlOutPatientServices()
        With controlOutPatientServices
            If (ListServiceOrderDetail IsNot Nothing AndAlso ListServiceOrderDetail.Count > 0 OrElse ListHemocomponent IsNot Nothing AndAlso ListHemocomponent.Count > 0) OrElse (ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Any(Function(x) x.InvoiceId Is Nothing)) Then
                If CType(INDGleAdmissionOption.EditValue, Byte) = 1 Then
                    Dim ingress As New IngresoOrdenesServicio()
                    With ingress
                        .NUMINGRES = "" 'A calcular
                        .IPCODPACI = patient.IPCODPACI
                        .CODENTIDA = INDSleHealthAdministrator.EditValue 'patient.CODENTIDA -- se envia el id de la entidad en este campo y en el servidor se busca el tercero y luego la entidad de crystal
                        .TIPOINGRE = 1
                        .IINGREPOR = 2
                        If INDLciDispatched.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            'Si es por Remisión, el Tipo de Ingreso debe ir marcado así
                            .IINGREPOR = 4
                        End If
                        .ITIPORIES = CInt(INDGleRiskType.EditValue)
                        .ICAUSAING = CInt(INDGleCause.EditValue)
                        .IFECHAING = Date.Now 'Establecer en el servidor
                        Select Case fncTipoLiquidacionTipoPaciente(patient.IPTIPOPAC, patient.IPTIPOAFI, patient.CAPACIPAG)
                            Case 1
                                .ILIQUIDAC = 1
                            Case 2
                                .ILIQUIDAC = 2
                            Case 4
                                .ILIQUIDAC = 4
                            Case Else
                                .ILIQUIDAC = 3
                        End Select
                        .ICONTROLI = ""
                        .CODCENATE = CareCenterCode
                        .UFUCODIGO = INDSleFunctionalUnit.Text.Split("-")(0).Trim()
                        .IAUTORIZA = INDAuthorizationNumberAdmission.EditValue
                        .IESTADOIN = " "
                        If INDGleRiskType.EditValue = "2" Then
                            If admission IsNot Nothing Then
                                .IINGRESOA = admission.AdmissionCode.ToString()
                            End If
                            .ISOATVALO = CDec(INDTxtValueSent.EditValue)
                            .ISALCODIG = INDSleSMLV.EditValue
                        End If

                        If Dispatched Then
                            .INUMERORE = INDTxtRemissionNumber.EditValue
                            .IFECHAREM = INDDteRemissionDate.EditValue
                            .IAUTORREM = INDTxtAuthorizationNumberRemission.EditValue
                            .DEPMUNCOD = INDSleTown.EditValue
                            .AIPSREMIS = INDSleIPS.EditValue
                            .IDUBICACION = INDSleUbication.EditValue
                        End If

                        .IOBSERVAC = String.Concat(INDMeObservationAdmission.Text, Environment.NewLine, INDMeObservationRemission.Text)
                        .IREINGRES = 2
                        .TIPOPROFE = Nothing
                        .UFUACTPAC = .UFUCODIGO
                        .CODUSUCRE = indigo.UserIndigo
                        .FECREGCRE = Date.Now 'En el servidor
                        .INDAUDFOR = 0
                        .INGRECEXT = False
                        .PACATENDI = False
                        .GENCAREGROUP = CInt(INDSleCareGroup.EditValue)
                        .GENCONENTITY = CInt(INDSleHealthAdministrator.EditValue) 'Consultar en aplicacion
                        .SOLRESHEMO = CBool(IIf(INDSleRequestHemoReserve.EditValue Is Nothing, 0, INDSleRequestHemoReserve.EditValue))
                        .IdAdmissionType = Me.AdmissionTypeId
                        .IdEntryRoutesHealthServices = Me.EntryRoutesHealthServicesId
                        .IdHealthPurposes = Me.HealthPurposesId
                        .IdAdmissionModalities = Me.AdmissionModalitiesId

                    End With
                    .Ingreso = ingress
                Else
                    Dim ingress As New IngresoOrdenesServicio()
                    With ingress
                        .NUMINGRES = "" 'A calcular
                        .IPCODPACI = patient.IPCODPACI
                        .CODENTIDA = CInt(INDSleHealthAdministrator.EditValue) 'patient.CODENTIDA
                        .GENCAREGROUP = CInt(INDSleCareGroup.EditValue)
                        .GENCONENTITY = CInt(INDSleHealthAdministrator.EditValue) 'Consultar en aplicacion
                        .CODCENATE = CareCenterCode
                        .UFUCODIGO = INDSleFunctionalUnit.Text.Split("-")(0).Trim()
                        .SOLRESHEMO = CBool(IIf(INDSleRequestHemoReserve.EditValue Is Nothing, 0, INDSleRequestHemoReserve.EditValue))
                    End With
                    .Ingreso = ingress
                    .IngresoExistente = admission.AdmissionCode.ToString().Trim()
                End If
            End If
            .ServiceOrderDetail = ListServiceOrderDetail
        End With

        Dim listadoCitas As New List(Of CitasMedicas)()
        If ListCitasMedicas IsNot Nothing Then
            For Each item In ListCitasMedicas
                Dim cita As New CitasMedicas()
                If item.Tipo = 3 Then 'Se verifica que el tipo de CUP Sea Imagenes dx
                    For i = 1 To item.CantidadServicio
                        With cita
                            .OrigenCirugia = item.OrigenCirugia
                            .Codigo = item.CodeTmp
                            .CodeRelated = item.Codigo
                            .IsFalseId = item.IsFalseId
                            .FechaCita = item.FechaCita
                            .CodigoProfesional = item.CodigoProfesional
                            .Profesional = item.Profesional
                            .NitMedico = item.NitMedico
                            .Consultorio = item.Consultorio
                            .TipoCita = item.TipoCita
                            .ActividadMedica = item.ActividadMedica
                            .CodigoServicio = item.CodigoServicio
                            .Especialidad = item.Especialidad
                            .Servicio = item.Servicio
                            .CantidadServicio = 1
                            .CodigoEspecialidad = item.CodigoEspecialidad
                            .AreaServicio = item.AreaServicio
                            .CentroCosto = item.CentroCosto
                            .TipoSolicitud = item.TipoSolicitud
                            .RequiresConfirmAppointment = item.RequiresConfirmAppointment
                            .InvoiceId = item.InvoiceId
                            .InvoiceNumber = item.InvoiceNumber
                            .AdmissionNumberInvoice = item.AdmissionNumberInvoice
                            .ActivityType = item.ActivityType
                            .NombreCompletoPaciente = INDBePatient.Text.Split("-")(1).Trim()
                            .CodigoPaciente = patient.IPCODPACI
                            .CodigoCentroAtencion = CareCenterCode
                            .CodigoUnidadFuncional = INDSleFunctionalUnit.Text.Split("-")(0).Trim()
                            .CareGroupIdInvoice = item.CareGroupIdInvoice
                            .HealthAdministratorIdInvoice = item.HealthAdministratorIdInvoice
                            .CUPSEntityContractDescriptionId = If(item.CUPSEntityContractDescriptionId = 0, Nothing, item.CUPSEntityContractDescriptionId)
                            .IDSALA = item.IDSALA
                            .CODACTMED = item.CODACTMED
                            .TypeOfScheduleActivity = item.ACTIVICON
                        End With
                        listadoCitas.Add(cita)
                    Next
                Else
                    With cita
                        .OrigenCirugia = item.OrigenCirugia
                        .Codigo = item.CodeTmp
                        .CodeRelated = item.Codigo
                        .IsFalseId = item.IsFalseId
                        .FechaCita = item.FechaCita
                        .CodigoProfesional = item.CodigoProfesional
                        .Profesional = item.Profesional
                        .NitMedico = item.NitMedico
                        .Consultorio = item.Consultorio
                        .TipoCita = item.TipoCita
                        .ActividadMedica = item.ActividadMedica
                        .CodigoServicio = item.CodigoServicio
                        .Especialidad = item.Especialidad
                        .Servicio = item.Servicio
                        .CantidadServicio = item.CantidadServicio
                        .CodigoEspecialidad = item.CodigoEspecialidad
                        .AreaServicio = item.AreaServicio
                        .CentroCosto = item.CentroCosto
                        '.Tipo = item.Tipo
                        .TipoSolicitud = item.TipoSolicitud
                        .RequiresConfirmAppointment = item.RequiresConfirmAppointment
                        .InvoiceId = item.InvoiceId
                        .InvoiceNumber = item.InvoiceNumber
                        .AdmissionNumberInvoice = item.AdmissionNumberInvoice

                        .NombreCompletoPaciente = INDBePatient.Text.Split("-")(1).Trim()
                        .CodigoPaciente = patient.IPCODPACI
                        .CodigoCentroAtencion = CareCenterCode
                        .CodigoUnidadFuncional = INDSleFunctionalUnit.Text.Split("-")(0).Trim()
                        .CareGroupIdInvoice = item.CareGroupIdInvoice
                        .HealthAdministratorIdInvoice = item.HealthAdministratorIdInvoice
                        .CUPSEntityContractDescriptionId = If(item.CUPSEntityContractDescriptionId = 0, Nothing, item.CUPSEntityContractDescriptionId)
                        .ActivityType = item.ActivityType
                        .IDSALA = item.IDSALA
                        .CODACTMED = item.CODACTMED
                        .TypeOfScheduleActivity = item.ACTIVICON
                    End With
                    listadoCitas.Add(cita)
                End If
            Next
        End If
        controlOutPatientServices.ListCitasMedicas = listadoCitas
        controlOutPatientServices.OperatingUnitId = _idOperativeUnit
        controlOutPatientServices.CareGroupId = CareGroupId
        controlOutPatientServices.UnitTypeFunctionalUnit = _payrollFunctionalUnitXpo.UnitType
        controlOutPatientServices.ListHemocomponent = Me.ListHemocomponent
    End Sub

    Private Sub OpenFormLiquidation(admissionNumber As String)
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Using FrmLiquidation As New FrmLiquidation()
            FrmLiquidation.Size = New System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            FrmLiquidation.MinimizeBox = False
            FrmLiquidation.MaximizeBox = False
            Using m As New MControlOutpatientServices(Me.Tag)
                Dim admissionCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                    .CrystalService.Liquidation_GetAdmission(admissionNumber)
                FrmLiquidation.AuxAdmissionToReload = admissionCollection
            End Using
            FrmLiquidation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            AddHandler FrmLiquidation.Shown, AddressOf HideLoaderForm
            Dim transparent As New FrmTransparent(FrmLiquidation, False)
            transparent.ShowDialog(Me)
            Nuevo()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para establecer la imagen a la columna de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetImageActivateColumn()
        Dim listActivated = Me.Hemocomponente.Details.FindAll(Function(x) x.Activated = True)
        If listActivated.Count = Me.Hemocomponente.Details.Count Then
            Me.INDHemoServiceColSel.Image = Global.Presentation.Billing.My.Resources.Resources.check
        Else
            Me.INDHemoServiceColSel.Image = Global.Presentation.Billing.My.Resources.Resources.undcheck
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Private Async Sub AddHemoCreateServiceOrder(Optional _listHemocomponent As List(Of Hemocomponent) = Nothing)
        If _listHemocomponent Is Nothing Then
            Me.ListHemocomponent.Add(Me.Hemocomponente)
            Await CreateServiceOrderDetailItemHemocomponent(Me.Hemocomponente, False)
        Else
            For Each _hemocomponent In _listHemocomponent
                Me.ListHemocomponent.Add(_hemocomponent)
                Await CreateServiceOrderDetailItemHemocomponent(_hemocomponent, False)
            Next
        End If
        INDGcHemoDetail.DataSource = Me.ListHemocomponent
        INDGcHemoDetail.RefreshDataSource()
        INDGcHemoDetail.Focus()
    End Sub

    ''' <summary>
    ''' Metodo encargador de ejecutar la accion de editar en la rejilla de citas medicas
    ''' </summary>
    ''' <param name="patientAppointment"></param>
    Private Async Function EditPatientAppointment(patientAppointment As SP_AD_ListarCitasMedicasNativo_Result) As Task
        Try
            AsyncLoader(True)
            INDLciAddCita.Enabled = False
            editFlag = True

            Me._codeAppointment = Nothing
            Me._consultationActivity = Nothing

            If Not patientAppointment?.ConsultationActivity.HasValue Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede editar el detalle por que no viene de una cita agendada"
                Return
            End If

            Me.FlagLoadPopUpAppointment = True
            If patientAppointment Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo editar el detalle"
                Return
            End If

            With patientAppointment
                Me.ActivityType = If(.ConsultationActivity = 2, 2, 1)
                Me._codeAppointment = .Codigo
                Me._consultationActivity = .ConsultationActivity
                Me.INDDteDateMedicalAppointment.EditValue = .FechaCita
                '------profesional y su especialidad----------------
                Me.INDSleHealthProfessional.EditValue = .CodigoProfesional
                Me.INDSleHealthProfessional.Properties.NullText = .Profesional
                Me.INDGleSpecialty.EditValue = .CodigoEspecialidad
                Me.INDGleSpecialty.Properties.NullText = .Especialidad
                '---------------------------------------------------
                '------------Actividad medica----------------------
                Me.FlagLoadPopUpAppointment = False
                Dim medicalActivityCodeName = If(.ActividadMedica.Contains(" - "), .ActividadMedica, $"{ .CodigioActividadMedica} - { .ActividadMedica}")
                Me.ScheduleActivityCode(medicalActivityCodeName) = .CodigioActividadMedica
                Me.FlagLoadPopUpAppointment = True
                '-------------------------------------------------
                '-------servicio-----------------------------------
                Me.INDSeQuantity.EditValue = .CantidadServicio
                Me.INDSleCUPSCrystal.EditValue = Nothing
                INDSleCUPSCrystal.Properties.NullText = .CodigoServicio

                If Not String.IsNullOrEmpty(Me._selectorCUPS.GetKeys()) Then
                    Me.SelectorClear()
                End If

                Dim CUPS = Await Task.Factory.StartNew(Function()
                                                           Return XpoServiceEx.Instance(indigo.TransactionalContainer).ContractService.GetXPOObject(Of ViewListCupsByAGACTIMEDXpo)($"CODSERIPS = '{ .CodigoServicio}' AND CODACTMED = '{ .CodigioActividadMedica}'")
                                                       End Function)

                Me._selectorCUPS.SetValue(CUPS, True)



                If .CUPSEntityContractDescriptionId <> 0 Then
                    INDsleDescription.Properties.NullText = .DescriptionCodeName
                    INDsleDescription.EditValue = .CUPSEntityContractDescriptionId
                    INDlyItemDescription.HideControl(False)
                End If
                '---------RIAS----------------------------------------
                If .RiasCupsId <> 0 Then
                    INDsleRIASCups.EditValue = .RiasCupsId
                    INDsleRIASCups.Properties.NullText = .RiasCodeName
                    Me.RiasId = .RiasId
                    Me.INDlyItemRIASCups.HideControl(False)
                End If
                '-----------------------------------------------------
                INDGleMedicalAppointmentType.EditValue = .TipoCita

                If ActivityType = 2 Then
                    Select Case .Tipo
                        Case 1
                            Me.ServiceType = .Tipo
                        Case 3
                            Me.ServiceType = 2
                        Case Else
                            Me.ServiceType = 3
                    End Select
                    Me.INDSleRoom.EditValue = If(String.IsNullOrEmpty(.Consultorio), .IDSALA, .Consultorio)
                    Me.INDSleRoom.Properties.NullText = .ConsultorioName
                Else
                    Me.ConsultingRoom = .Consultorio
                    Me.INDSleConsultingRoom.Properties.NullText = .ConsultorioName
                End If

            End With


            Me.FlagLoadPopUpAppointment = False

            INDPceMedicalAppointment.ShowPopup()
            INDTcgAppoinment.SelectedTabPage = INDLcgAddCita
            INDSleCUPSCrystal.Focus()

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoader(False)
            INDLciAddCita.Enabled = True
        End Try
    End Function

    ''' <summary>
    ''' metodo encargado de eliminar un cita de la rejilla de citas
    ''' </summary>
    ''' <param name="flagEdit"></param>
    ''' <param name="cita"></param>
    ''' <param name="codeAppointment"></param>
    Private Sub DeletePatientAppointment(flagEdit As Boolean, Optional cita As SP_AD_ListarCitasMedicasNativo_Result = Nothing,
                                         Optional codeAppointment As String = Nothing)
        Try
            If cita Is Nothing AndAlso String.IsNullOrEmpty(codeAppointment) Then
                Mensaje(EeventViewerImages.Advertencia) = "Parámetros vacios"
                Exit Sub
            End If

            Dim code = Me._codeAppointment

            'If flagEdit Then
            '    _listaCitasMedicas.RemoveAll(Function(x) x.Codigo = code AndAlso x.IsEditing)

            'Else
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _listaCitasMedicas.Remove(cita)
                code = cita.Codigo
            Else
                Exit Sub
            End If

            ListCitasMedicas = _listaCitasMedicas
            'Eliminar Detalle de orden de servicio
            DeleteServiceOrderDetailByCitaId(code, flagEdit)

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Function LoadDatasourceINDSleCUPSCrystal() As Boolean

        If ScheduleActivityCode IsNot Nothing Then
            CUPSCrystalXPO = XpoServiceEx.Instance(indigo.TransactionalContainer).CrystalService.ListCUPSByCODACTMED(Me.ScheduleActivityCode)
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' funcion que valida si los cups checkeados tienen descripcion o es de consulta externa para no permitir mas de uno
    ''' </summary>
    ''' <param name="selector"></param>
    ''' <returns></returns>
    Private Async Function ValidateCUPSWithDescription(selector As SelectorCache) As Task(Of ActionResult)
        Try
            AsyncLoader(True)
            INDLciAddCita.Enabled = False

            CleanCUPSRelatedControls()

            If String.IsNullOrEmpty(selector.GetKeys()) Then
                Return New ActionResult With {.StateResult = False, .Message = String.Empty}
            End If

            Dim listSelectedCUPS = Split(selector.GetKeys(), ",").ToList()

            ' se valida si existen cups de consulta externa mezclados ya que se le debe habilitar otro segmento
            If listSelectedCUPS.Any(Function(item) selector.ValuesCache(item)("TIPSERIPS") = 8) Then
                If listSelectedCUPS.All(Function(item) selector.ValuesCache(item)("TIPSERIPS") = 8) Then
                    INDLciTypeConsultation.HideControl(False) 'Se hace visible solo cuando es consulta externa
                Else
                    INDLciTypeConsultation.HideControl(True)
                    Return New ActionResult With {.StateResult = True,
                                            .Message = $"Existen CUPS mezclados de consulta externa"}
                End If
            Else
                INDLciTypeConsultation.HideControl(True)
            End If

            'valida si un cups aplica RIAS
            If listSelectedCUPS.Any(
                Function(item) selector.ValuesCache(item)("APLICARIAS") _
                AndAlso ApplyRIASCareGroup) Then
                INDlyItemApplyRIAS.HideControl(False)
            Else
                INDlyItemApplyRIAS.HideControl()
            End If

            If listSelectedCUPS?.Any(Function(s) Not _dictionaryCUPSDescription.Select(Function(x) x.Key).Contains(s)) Then
                'obtiene la lista de CUPS que No existen en el diccionario
                Dim ListToAddDictionary = Await presenter.GetEntityCUPSByListCodes(listSelectedCUPS.FindAll(Function(s) Not _dictionaryCUPSDescription.Select(Function(x) x.Key).Contains(s)))

                For Each item In ListToAddDictionary
                    If Not _dictionaryCUPSDescription.ContainsKey(item.Code) Then
                        _dictionaryCUPSDescription.Add(item.Code, item.CUPSEntityContractDescriptionsXpo.Any(Function(f) f.IsDelete = 0))
                    End If
                Next

            End If

            ' se valida si existe descripcion relacionada y si seleciono mas de un CUPS
            If _dictionaryCUPSDescription.Any(Function(x) listSelectedCUPS.Contains(x.Key) AndAlso x.Value) Then

                If listSelectedCUPS.Count > 1 Then
                    INDlyItemDescription.HideControl(True)

                    Dim CUPSWithDescription = _dictionaryCUPSDescription.Where(Function(x) listSelectedCUPS.Contains(x.Key) AndAlso x.Value).Select(Function(s) s.Key).ToList()
                    Return New ActionResult With {.StateResult = True,
                                                .Message = $"Existen CUPS con descripción relacionada {String.Join(" , ", CUPSWithDescription)}"}
                Else
                    INDlyItemDescription.HideControl(False)
                End If
            End If

            Return New ActionResult With {.StateResult = False}

        Catch ex As Exception
            Return New ActionResult With {.StateResult = True, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        Finally
            AsyncLoader(False)
            INDLciAddCita.Enabled = True
        End Try
    End Function

    ''' <summary>
    ''' limpia y establece los valores de los combos relacionados a CUPS 
    ''' </summary>
    Private Sub CleanCUPSRelatedControls()
        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.DataSource = Nothing
        INDlyItemDescription.HideControl(True)

        INDsleRIASCups.EditValue = Nothing
        INDsleRIASCups.Properties.DataSource = Nothing
        INDsleApplyRIAS.EditValue = False
        INDlyItemApplyRIAS.HideControl()
    End Sub

    Private Sub SelectorClear()
        If Me._selectorCUPS IsNot Nothing Then
            Me._selectorCUPS.Clear()
            Me.INDSleCUPSCrystal.Text = String.Empty
            Me.INDSleCUPSCrystal.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    '''  se cambia el ShowInCustomizationForm a True cuando el idioma o cultura actual
    ''' es diferente a "es-CO" esto para que no me valide los control como obligatorio 
    ''' en la función ValidateField.
    ''' </summary>
    Private Sub SetCustomizationFormBasedOnCulture()
        If indigo.LanguageCulture <> "es-CO" Then
            INDliIdEntryRoutesHealthServices.ShowInCustomizationForm = True
            INDliIdHealthPurposes.ShowInCustomizationForm = True
            INDliIdAdmissionModalities.ShowInCustomizationForm = True
        Else
            INDliIdEntryRoutesHealthServices.ShowInCustomizationForm = False
            INDliIdHealthPurposes.ShowInCustomizationForm = False
            INDliIdAdmissionModalities.ShowInCustomizationForm = False
        End If
    End Sub
#End Region

#Region "HANDLERS"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ThirdPartyPatientId = Nothing
        admission = Nothing
        presenter = Nothing
        healthProfessional = Nothing
        thirdParty = Nothing
        ctrInfo = Nothing
        patient = Nothing
        popupService = Nothing
        _serviceValue = Nothing
        serviceOrderDetail = Nothing
        indexEditItem = Nothing
        rowEditing = Nothing
        controlOutPatientServices = Nothing
        _idOperativeUnit = Nothing
        _listRequestHemoReserve = Nothing
        HemocomponentXPO = Nothing
        ProfessionalXPO = Nothing
        _SpecialityCode = Nothing
        ListReserveHemocomponentDetail = Nothing
        ListHemocomponent = Nothing
        Hemocomponente = Nothing
        Me._codeAppointment = Nothing
        Me._flagLoadPopUpAppointment = Nothing
        Me._consultationActivity = Nothing
    End Sub


    Private Sub FrmControlOutpatientServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcControlOutpatientServices, True)
        Me.LayoutControls.SetIsCustomizable(Me.INDlcAdmission, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.INDPceMedicalAppointment.MinimumSize = New System.Drawing.Size(824, 28)
        Me.INDDteDateMedicalAppointment.EditValue = Me.GetDateServer()

        INDGleActivityType.Properties.DataSource = ListActivityType
        ActivityType = 2
        INDGleServiceType.Properties.DataSource = ListServiceType

        INDSleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission
        INDSleAdmissionNumber.Search.Properties.ValueMember = "AdmissionCode"
        INDSleAdmissionNumber.Search.Properties.DisplayMember = "FullNameAdmission"
        AddHandler INDSleAdmissionNumber.Search.QueryPopUp, AddressOf INDSleAdmissionNumber_QueryPopup
        _indigoSession = SessionValues.Instance
        presenter = New PControlOutpatientServices(Me)
        presenter.LoadDefinitionLayout()

        'parámetos de contratos
        _settingsContractXpo = presenter.GetSettingsContractByOperatingUnitId(_idOperativeUnit)
        presenter.LoadFlagTaxInclude()

        Task.Factory.StartNew(Sub()
                                  Using model As New MServiceOrder(Me.Tag)
                                      healthProfessional = model.GetCareProfessionalByCode("999")?.FirstOrDefault
                                  End Using

                                  If healthProfessional Is Nothing Then
                                      Exit Sub
                                  End If

                                  Using model As New MThirdParty(Me.Tag)
                                      thirdParty = model.GetThirdParty(healthProfessional?.CODIGONIT?.TrimStart("0"))
                                  End Using
                              End Sub)

        IndigoGridView2.MoreInfoColunmns(INDGvService)
        IndigoGridControl1.RefreshGrid(INDGcMedicalAppointment)
        IndigoGridControl1.RefreshGrid(INDGcMedicalAppointmentPatient)
        IndigoGridControl1.RefreshGrid(INDGcServiceOrderDetail)

        Dim listAction As New List(Of eAcciones)
        listAction.Add(eAcciones.Remove)
        IndigoGridView3.SetListAcction(INDGvHemoDetail, listAction)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvHemoDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next
        IndigoGridView1.SetListAcction(INDGvMedicalAppointment, listAction)

        listAction.Add(eAcciones.Edit)
        IndigoGridView4.SetListAcction(INDGvMedicalAppointmentPatient, {eAcciones.Edit}.ToList())
        IndigoGridView4.MoreInfoColunmns(INDGvMedicalAppointmentPatient)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvMedicalAppointment.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next

        IndigoGridView2.SetListAcction(INDGvService, listAction)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvService.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
            If col.Name = "MoreInfo" Then
                col.Width = 100
            End If
        Next

        INDPceMedicalAppointment.MinimumSize = New System.Drawing.Size(386, 28)
        Deshacer()
        LoadStatus()
        AddHandler Me.INDSleAdmissionNumber.Search.KeyDown, AddressOf INDSleAdmissionNumber_KeyDown
        INDSleRequestHemoReserve.Properties.DataSource = ListRequestHemoReserve
        INDSleRequestHemoReserve.EditValue = 0
        presenter.LoadPermissionsForm(Me.Tag)
    End Sub
#End Region

#Region "Shown"
    Private Sub FrmControlOutpatientServices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleApplyRIAS.Properties.DataSource = ListYesNo
        INDSleCareCenter.Focus()
    End Sub
#End Region

#Region "KeyDown"

    Private Sub INDGvMedicalAppointmentPatient_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGvMedicalAppointmentPatient.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Tab Then
            INDsbAddCitas.Focus()
        End If
    End Sub

    Private Sub INDSleHealthAdministrator_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDSleHealthAdministrator.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDPceAdmissionData.Focus()
            INDPceAdmissionData.ShowPopup()
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDTxtValueSent.Focus()
        End If
    End Sub

    Private Sub INDGleDispatched_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGleDispatched.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDLciDispatched.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDPceDispatched.Focus()
                INDPceDispatched.ShowPopup()
                INDTxtRemissionNumber.Focus()
            Else
                INDPceMedicalAppointment.Focus()
                INDPceMedicalAppointment.ShowPopup()
            End If
        End If
    End Sub

    Private Sub INDBePatient_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBePatient.KeyDown
        If Not INDBePatient.Properties.ReadOnly Then
            If e.KeyCode = System.Windows.Forms.Keys.F4 Then
                OpenSearch()
            ElseIf Not String.IsNullOrEmpty(INDBePatient.Text.ToString().Trim()) AndAlso e.KeyCode = System.Windows.Forms.Keys.Enter Then
                ReturnValue(INDBePatient.Text.ToString().Trim(), Nothing)
            End If
        End If
    End Sub

    Private Sub INDMeObservationRemission_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeObservationRemission.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDPceDispatched.ClosePopup()
            INDPceMedicalAppointment.Focus()
        End If
    End Sub

    Private Sub INDMeObservationAdmission_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMeObservationAdmission.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDPceAdmissionData.ClosePopup()
            INDGleDispatched.Focus()
        End If
    End Sub

    Private Sub INDPceMedicalAppointment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceMedicalAppointment.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDPceMedicalAppointment.ShowPopup()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDescription.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2096, Nothing, True)
            If Me._selectorCUPS IsNot Nothing AndAlso Me._selectorCUPS.Count = 1 Then
                INDsleDescription.Properties.DataSource = presenter.ListContractDescriptionsByCupsEntityCode(Me._selectorCUPS.GetKeys())
            End If
        End If
    End Sub

    Private Sub INDFpAdmission_ButtonClick(sender As Object, e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs) Handles INDFpAdmission.ButtonClick
        INDFpAdmission.HideBeakForm()
    End Sub

    Private Sub INDPceMedicalAppointment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceMedicalAppointment.ButtonClick
    End Sub

    Private Sub INDsleContract_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleContract.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("978", INDsleContract.EditValue, True)
        End If
    End Sub

    Private Sub INDSleFunctionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleFunctionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            ' Instanciar el formulario
            Using frmFunctionalUnit As New Payroll.FrmFunctionalUnit()
                frmFunctionalUnit.Tag = Me.Tag

                ' Mostrar el formulario de forma modal
                frmFunctionalUnit.ViewModeEditHold = True
                frmFunctionalUnit.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(frmFunctionalUnit, False)
                If transparent.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
                    FunctionalUnitXPO = Nothing
                    INDSleFunctionalUnit.Properties.DataSource = Nothing
                End If
            End Using
        End If
    End Sub

    Private Sub INDBePatient_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBePatient.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1500", Nothing, True)
        Else
            If Not INDBePatient.Properties.ReadOnly Then
                OpenSearch()
            End If
        End If
    End Sub

    Private Sub INDSleHealthAdministrator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleHealthAdministrator.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("972", Nothing, True)
        End If
    End Sub

    Private Sub INDSleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("985", Nothing, True)
        End If
    End Sub
#End Region

#Region "NewSelectedValue"
    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDSleAdmissionNumber.NewSelectedValue

        SetAdmission(e.AdmissionObject)

    End Sub
#End Region

#Region "CloseUp"
    Private Sub INDPceAdmissionData_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceAdmissionData.Closed
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' evento encargado de cargar el datasource de los tipo de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleAdmissionType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleAdmissionType.QueryPopUp
        If Me.AdmissionTypeDatasource Is Nothing Then
            presenter.InitializateAdmissionType()
        End If
    End Sub

    ''' <summary>
    ''' evento encargado de cargar el datasource de los Vías Ingreso Servicios de Salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdEntryRoutesHealthServices_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIdEntryRoutesHealthServices.QueryPopUp
        LoadIngressData()
    End Sub

    ''' <summary>
    ''' evento encargado de cargar el datasource de los Finalidades tecnologías de la salud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdHealthPurposes_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIdHealthPurposes.QueryPopUp
        If Me.HealthPurposesDatasource Is Nothing Then
            presenter.InitializateHealthPurposes()
        End If
    End Sub

    ''' <summary>
    ''' evento encargado de cargar el datasource de los Modalidades de Atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIdAdmissionModalities_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIdAdmissionModalities.QueryPopUp
        LoadModalityData()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al deslegar el control de descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleDescription.QueryPopUp
        If INDsleDescription.Properties.DataSource Is Nothing AndAlso Me._selectorCUPS IsNot Nothing AndAlso Me._selectorCUPS.Count = 1 Then
            INDsleDescription.Properties.DataSource = presenter.ListContractDescriptionsByCupsEntityCode(Me._selectorCUPS.GetKeys())
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el search de riasCups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASCups_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleRIASCups.QueryPopUp
        If INDsleRIASCups.Properties.DataSource Is Nothing AndAlso Not String.IsNullOrEmpty(Me._selectorCUPS.GetKeys()) Then
            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                INDsleRIASCups.Properties.DataSource = modelServiceOrder.ListRIASCups(Split(Me._selectorCUPS.GetKeys(), ",").ToList())
            End Using
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_QueryPopup(sender As Object, e As ComponentModel.CancelEventArgs)
        If previusAdmissionOption = 0 OrElse previusAdmissionOption <> CType(INDGleAdmissionOption.EditValue, Byte) Then
            Using model As New MServiceOrder(MyTag.ToString())
                If CType(INDGleAdmissionOption.EditValue, Byte) = 1 Then
                    INDSleAdmissionNumber.Datasource = model.GetListAdmissionsPatientCodeStatus(patient.IPCODPACI.Trim(), "F")
                Else
                    INDSleAdmissionNumber.Datasource = model.GetListAdmissionsByPatientCodeStatus(patient.IPCODPACI.Trim(), " ")
                End If
            End Using
            If admission IsNot Nothing AndAlso Not String.IsNullOrEmpty(admission.AdmissionCode) Then
                INDSleAdmissionNumber.SetEditValue = admission.AdmissionCode
            End If
            previusAdmissionOption = CType(INDGleAdmissionOption.EditValue, Byte)
        End If
    End Sub

    Private Sub INDsleContract_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleContract.QueryPopUp
        If ContractDatasourceXpo Is Nothing Then
            Using model As New MControlOutpatientServices(MyTag)
                ContractDatasourceXpo = model.ListContractXpo()
            End Using
        End If
    End Sub

    Private Sub INDrptSleInvoice_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDrptSleInvoice.QueryPopUp
        If INDrptSleInvoice.DataSource Is Nothing Then
            Using m As New MAccountControl(Me.Tag)
                INDrptSleInvoice.DataSource = m.ListInvoiceByStatusAndPatientCode(1, patient.IPCODPACI)
            End Using
        End If
    End Sub

    Private Sub INDSleCareCenter_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareCenter.QueryPopUp
        If CareCenterXPO Is Nothing Then
            Using model As New MControlOutpatientServices(MyTag)
                CareCenterXPO = model.ListCentersHIS()
            End Using
        End If
    End Sub

    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If FunctionalUnitXPO Is Nothing Then
            Using model As New MControlOutpatientServices(MyTag)
                FunctionalUnitXPO = model.ListFunctionalUnitCareCenter(INDSleCareCenter.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDSleHealthAdministrator_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHealthAdministrator.QueryPopUp
        If INDSleHealthAdministrator.Properties.DataSource Is Nothing Then
            Using Model As New MAdmissions(Me.Tag)
                INDSleHealthAdministrator.Properties.DataSource = Model.ListHealthAdministratorByType(careGroupPatientSelected.EntityType) ' entidades iguales a aseguradoras
            End Using
        End If
    End Sub

    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If CareGroupXPO Is Nothing Then
            Using model As New MServiceOrder(MyTag)
                CareGroupXPO = model.ListCareGroup()
            End Using
        End If
    End Sub

    Private Sub INDSleSMLV_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleSMLV.QueryPopUp
        If SMLVXPO Is Nothing Then
            Using model As New MAdmissions(MyTag)
                SMLVXPO = model.ListAllMinWage
            End Using
        End If
    End Sub

    Private Sub INDSleIPS_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleIPS.QueryPopUp
        If IPSCrystalXPO Is Nothing Then
            Using model As New MControlOutpatientServices(MyTag)
                IPSCrystalXPO = model.ListIPSCrystalByStatus(True)
            End Using
        End If
    End Sub

    Private Sub INDSleTown_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleTown.QueryPopUp
        If TownXPO Is Nothing Then
            Using model As New MControlOutpatientServices(MyTag)
                TownXPO = model.ListTown()
            End Using
        End If
    End Sub

    Private Sub INDSleHealthProfessional_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHealthProfessional.QueryPopUp
        If HealthProfessionalXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                HealthProfessionalXPO = model.ListHealthCareProfessional()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProfessional_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleProfessional.QueryPopUp
        If ProfessionalXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                ProfessionalXPO = model.ListHealthCareProfessional()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSle_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleHemo.QueryPopUp
        presenter.ListHemocomponent()
    End Sub

    ''' <summary>
    ''' Busca la Sala dependiendo del Servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleRoom_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleRoom.QueryPopUp
        If INDGleServiceType.EditValue IsNot Nothing AndAlso INDSleCareCenter.EditValue IsNot Nothing Then
            Dim DataSource = presenter.GetRoomWithFilters(Me.ServiceType, INDSleCareCenter.EditValue)
            INDSleRoom.Properties.DataSource = DataSource
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de servicio, para obtener la lista de las Salas"
        End If
    End Sub

    ''' <summary>
    ''' QUERYPOPUP DE ACTIVIDADES DE AGENDAMIENTO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleScheduleActivity_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleScheduleActivity.QueryPopUp
        If INDSleRoom.EditValue IsNot Nothing Then
            Dim DataSource = presenter.GetScheduleActivities(INDSleRoom.EditValue)
            INDSleScheduleActivity.Properties.DataSource = DataSource
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Sala primero para listar las actividades de agendamiento"
        End If
    End Sub

    Private Sub INDGleCause_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGleCause.QueryPopUp
        If INDGleCause.Properties.DataSource Is Nothing Then
            INDGleCause.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BillingService.ListXPInstantFeedbackSource(Of BillingRepository.CausesofattentionXpo)("Status=1")
        End If
    End Sub

    Private Sub INDSleUbication_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleUbication.QueryPopUp
        If INDSleUbication.Properties.DataSource Is Nothing Then
            INDSleUbication.Properties.DataSource = presenter.GetUbications()
        End If
    End Sub

    ''' <summary>
    ''' evento para establecer el datasource de las  actividades de agendamiento para tipo otros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleScheduleActivityOther_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleScheduleActivityOther.QueryPopUp

        If INDGleSpecialty.EditValue Is Nothing Then
            Me.ScheduleActivityOtherXPO = Nothing
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una especialidad primero para listar las actividades de agendamiento"
            Exit Sub
        End If
        Me.presenter.LoadScheduleActivityOther(INDGleSpecialty.EditValue)
    End Sub

    ''' <summary>
    ''' evento para establecer el datasource de consultorios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleConsultingRoom_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleConsultingRoom.QueryPopUp
        If Me.ConsultingRoomXPO Is Nothing Then
            Me.presenter.LoadConsultingRoom(_centAtencionXpo.CODCENATE)
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Dim EAPBType As ECareGroupType
    Dim careGroupPatientSelected As CareGroup

    ''' <summary>
    ''' evento editvaluechanged para cuando cambia la actividad de agendamiento limpie el cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleScheduleActivity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleScheduleActivity.EditValueChanged, INDSleScheduleActivityOther.EditValueChanged
        If Me.FlagLoadPopUpAppointment Then
            Exit Sub
        End If

        INDsleDescription.EditValue = Nothing
        INDSleCUPSCrystal.Properties.NullText = Nothing
        INDsleDescription.Properties.NullText = Nothing
        Me.SelectorClear()
        LoadDatasourceINDSleCUPSCrystal()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control aplica a rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleApplyRIAS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyRIAS.EditValueChanged
        If INDsleApplyRIAS.EditValue Then
            INDlyItemRIASCups.HideControl(False)
        Else
            INDlyItemRIASCups.HideControl()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRIASCups.EditValueChanged
        If Me.FlagLoadPopUpAppointment Then
            Exit Sub
        End If
        If INDsleRIASCups.EditValue IsNot Nothing Then
            Dim infoRias = CType(CType(INDviewSearchRiasCups.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BillingRepository.ViewRIASCupsXpo)
            If infoRias IsNot Nothing Then
                RiasId = infoRias.RiasId
            End If
        End If
    End Sub

    Private Async Sub INDSleHealthAdministrator_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHealthAdministrator.EditValueChanged
        If _isloading Then
            Exit Sub
        End If
        If INDSleHealthAdministrator.EditValue IsNot Nothing Then
            Using mHealthAdministrator As New MHealthAdministrator(Me.Tag)
                HealthAdministrator = (Await mHealthAdministrator.GetHealthAdministratorById(CInt(INDSleHealthAdministrator.EditValue))).ObjectEmbbeded
            End Using
        End If
    End Sub

    Private Sub INDGleAdmissionOption_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAdmissionOption.EditValueChanged
        INDPccAdmissionData.BeginInit()
        admission = Nothing
        If INDGleAdmissionOption.EditValue IsNot Nothing Then
            If CType(INDGleAdmissionOption.EditValue, Byte) = 1 Then
                INDSleAdmissionNumber.SetNullText(String.Empty)
                INDSleAdmissionNumber.SetEditValue = Nothing
                INDliTipoRiesgo.HideControl(False)
                INDliCausa.HideControl(False)
                INDliAuthorization.HideControl(False)
                INDliObservaciones.HideControl(False)
                INDLciAdmissionNumber.HideControl()
                INDLciAdmissionNumber.Text = "Ingreso Relacion"
                INDLciAdmissionType.HideControl(False)

                If indigo.LanguageCulture = "es-CO" Then
                    INDliIdEntryRoutesHealthServices.HideControl(False)
                    INDliIdHealthPurposes.HideControl(False)
                    INDliIdAdmissionModalities.HideControl(False)
                End If

            Else
                If _listaCitasMedicas IsNot Nothing AndAlso _listaCitasMedicas.Any(Function(o) o.TipoCita IsNot Nothing AndAlso o.TipoCita = 3) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede seleccionar Ingreso existente debido a que se ha agregado una cita de tipo Pos Operatorio"
                    INDGleAdmissionOption.EditValue = Nothing
                    Exit Sub
                End If
                INDSleAdmissionNumber.SetEditValue = Nothing
                INDLciAdmissionNumber.HideControl(False)
                INDLciValueSent.HideControl()
                INDLciSMLV.HideControl()
                INDGleRiskType.EditValue = Nothing
                INDSleAdmissionNumber.SetNullText(String.Empty)
                INDTxtValueSent.EditValue = 0
                INDGleCause.EditValue = Nothing
                INDAuthorizationNumberAdmission.EditValue = String.Empty
                INDMeObservationAdmission.EditValue = String.Empty
                Me.AdmissionTypeId = Nothing
                INDliTipoRiesgo.HideControl()
                INDliCausa.HideControl()
                INDliAuthorization.HideControl()
                INDliObservaciones.HideControl()
                INDLciAdmissionType.HideControl()
                INDLciAdmissionNumber.Text = "Ingreso"
                INDliIdEntryRoutesHealthServices.HideControl()
                INDliIdHealthPurposes.HideControl()
                INDliIdAdmissionModalities.HideControl()
            End If
        Else
            INDSleAdmissionNumber.SetEditValue = Nothing
            INDLciAdmissionNumber.HideControl()
            INDLciValueSent.HideControl()
            INDLciSMLV.HideControl()
            INDGleRiskType.EditValue = Nothing
            INDSleAdmissionNumber.SetNullText(String.Empty)
            INDTxtValueSent.EditValue = 0
            INDGleCause.EditValue = Nothing
            INDAuthorizationNumberAdmission.EditValue = String.Empty
            INDMeObservationAdmission.EditValue = String.Empty
            Me.AdmissionTypeId = Nothing
            Me.EntryRoutesHealthServicesId = Nothing
            Me.HealthPurposesId = Nothing
            Me.AdmissionModalitiesId = Nothing

            INDliTipoRiesgo.HideControl()
            INDliCausa.HideControl()
            INDliAuthorization.HideControl()
            INDliObservaciones.HideControl()
            INDLciAdmissionType.HideControl()
            INDliIdEntryRoutesHealthServices.HideControl()
            INDliIdHealthPurposes.HideControl()
            INDliIdAdmissionModalities.HideControl()
        End If

        INDPccAdmissionData.EndInit()
        INDPceAdmissionData.ClosePopup()
        INDPceAdmissionData.ShowPopup()
    End Sub


    Private Sub INDPceAdmissionData_QueryResultValue(sender As Object, e As QueryResultValueEventArgs) Handles INDPceAdmissionData.QueryResultValue
        If String.IsNullOrEmpty(INDAuthorizationNumberAdmission.Text) = False And INDGvMedicalAppointment.DataSource IsNot Nothing Then
            If ListServiceOrderDetail IsNot Nothing Then
                For Each item In ListServiceOrderDetail
                    If String.IsNullOrEmpty(item.AuthorizationNumber) Then
                        ' Verificar si el campo está vacío y llenarlo
                        item.AuthorizationNumber = AuthorizationNumber
                    End If
                Next
                INDGcServiceOrderDetail.DataSource = ListServiceOrderDetail
                INDGcServiceOrderDetail.RefreshDataSource()
            End If
        End If
    End Sub

    Private Sub INDPceAdmissionData_BeforePopup(sender As Object, e As EventArgs) Handles INDPceAdmissionData.BeforePopup
        Dim height = 100
        If INDGleAdmissionOption.EditValue IsNot Nothing AndAlso INDGleAdmissionOption.EditValue = 1 Then
            height = 360
        End If
        INDPceAdmissionData.Properties.PopupControl.Size = New System.Drawing.Size(INDlcAdmission.Size.Width, height)
        LoadIngressData()
        LoadModalityData()
    End Sub

    Private Async Sub INDSleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareGroup.EditValueChanged
        'limpo controles
        EAPBType = Nothing
        INDlciContract.HideControl(True)
        INDlciEntity.HideControl(True)
        INDsleContract.Properties.NullText = String.Empty
        INDSleHealthAdministrator.EditValue = Nothing
        INDsleContract.EditValue = Nothing
        INDSleHealthAdministrator.Properties.ReadOnly = False

        If INDSleCareGroup.EditValue IsNot Nothing Then
            Using Model As New MCareGroup(Me.Tag)
                Dim res = Await Model.GetCareGroupById(INDSleCareGroup.EditValue)
                careGroupPatientSelected = res.ObjectEmbbeded

                ValidateAuthorizationNumber = False
                If careGroupPatientSelected.AuthorizationRequired Then
                    ValidateAuthorizationNumber = True
                End If

                'Se asigna el valor de si el grupo de atención aplica a RIAS
                INDlyItemApplyRIAS.HideControl()
                INDlyItemRIASCups.HideControl()
                Me.SelectorClear()
                ApplyRIASCareGroup = If(res.ObjectEmbbeded.ApplyRIAS Is Nothing, False, res.ObjectEmbbeded.ApplyRIAS)

                INDSleHealthAdministrator.Properties.DataSource = Nothing
                INDSleHealthAdministrator.Properties.NullText = String.Empty
                Select Case res.ObjectEmbbeded.CareGroupType
                    Case Is = 1 'Con contrato (mostrat EAPB)
                        EAPBType = ECareGroupType.EAPBConContrato
                        INDlciContract.HideControl(False)
                        INDsleContract.Properties.NullText = res.ObjectEmbbeded.ContractDescription
                        INDsleContract.EditValue = res.ObjectEmbbeded.ContractId
                        INDlciEntity.HideControl(False)

                        INDSleHealthAdministrator.EditValue = res.ObjectEmbbeded.Contract.HealthAdministratorId
                        INDSleHealthAdministrator.Properties.NullText = res.ObjectEmbbeded.Contract.HealthAdministratorDescription
                        INDSleHealthAdministrator.Properties.ReadOnly = True

                    Case Is = 2 'Sin contrato (preguntar por EAPB diferente a aseguradoras)

                        EAPBType = ECareGroupType.EAPBSinContrato
                        INDlciEntity.HideControl(False)

                    Case Is = 3 'Particulares (Mostrar tercero del paciente)
                        EAPBType = ECareGroupType.EAPBParticular

                    Case Is = 4 'Aseguradoras (preguntar por EAPB = aseguradoras)
                        EAPBType = ECareGroupType.EAPBAseguradora
                        INDlciEntity.HideControl(False)
                End Select
            End Using
        End If
    End Sub

    Private Sub INDsleContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleContract.EditValueChanged
        If INDsleContract.EditValue IsNot Nothing Then
            If INDgvContract.GetFocusedRow() IsNot Nothing Then
                Dim contract As ContractXpo = CType(CType(INDgvContract.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ContractXpo)
                INDSleHealthAdministrator.EditValue = contract.HealthAdministratorId.Id
                INDSleHealthAdministrator.Properties.NullText = contract.HealthAdministratorId.CodeName
            End If
        End If
    End Sub

    Private Sub INDSleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareCenter.EditValueChanged
        If Not String.IsNullOrEmpty(CareCenterCode) Then
            _centAtencionXpo = CType(CType(INDGvCentAtencion.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CentersXpo)
            ActionsOnControls = True
        End If
    End Sub

    Private Sub INDSleFunctionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFunctionalUnit.EditValueChanged
        If INDSleFunctionalUnit.EditValue IsNot Nothing Then
            _payrollFunctionalUnitXpo = CType(CType(INDGvFunctionalUnit.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollViewFunctionalUnitCareCenterUserXpo)
            If FunctionalUnitId IsNot Nothing Then
                BarraBotones.StatusRecordVisible = True
                ctrInfo.PrintInfo()
                INDLcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLcgAdmissionData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcgMedicalAppointment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcgPreLiquidation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ActionsOnControlsIngress = False
            End If
        End If
    End Sub

    Private Sub INDGleDispatched_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDispatched.EditValueChanged
        If Dispatched Then
            INDLciDispatched.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciDispatched.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDGleRiskType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRiskType.EditValueChanged
        'valido tipos de riesgo con causa
        If INDGleRiskType.EditValue IsNot Nothing Then

            INDLciAdmissionNumber.HideControl()
            INDLciValueSent.HideControl()
            INDLciSMLV.HideControl()

            Select Case INDGleRiskType.EditValue
                Case Is = "2" 'Accidente de Transito

                    INDLciAdmissionNumber.HideControl(False)
                    INDLciValueSent.HideControl(False)
                    INDLciSMLV.HideControl(False)
                    INDSleAdmissionNumber.Search.EditValue = Nothing
                    INDSleAdmissionNumber.SetNullText("")
                    INDTxtValueSent.EditValue = Nothing
                    INDSleSMLV.EditValue = Nothing
                    admission = Nothing
                    CleanControlsAdminssion()
                Case Else
                    INDGleCause.EditValue = Nothing
            End Select
        End If
    End Sub

    Private Sub INDSleHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHealthProfessional.EditValueChanged
        If INDSleHealthProfessional.EditValue IsNot Nothing Then
            SetSpecialties(healthProfessional)
        Else
            INDGleSpecialty.Properties.DataSource = Nothing
            INDGleSpecialty.EditValue = Nothing
        End If
    End Sub

    Private Async Sub INDrptSleInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDrptSleInvoice.EditValueChanged
        Dim searchEdit As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim cita As SP_AD_ListarCitasMedicasNativo_Result = CType(INDGvMedicalAppointment.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result)
        If searchEdit.EditValue IsNot Nothing Then
            Dim invoiceXpo = CType(CType(searchEdit.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BillingRepository.InvoiceXpo)
            cita.InvoiceNumber = invoiceXpo.InvoiceNumber
            cita.AdmissionNumberInvoice = invoiceXpo.AdmissionNumber
            DeleteServiceOrderDetailByCitaId(cita.Codigo)
        Else
            Await CreateServiceOrderDetailItem({cita}.ToList(), True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que evalua si el tipo de actividad es apoyo diagnostico, u Otro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleActivityType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleActivityType.EditValueChanged
        CleanPopUpCitas(Me.INDLcgAddCita.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit"))
        Dim flag As Boolean = (ActivityType = 2)
        INDLciServiceType.HideControl(Not flag)
        INDLCiRoom.HideControl(Not flag)
        INDLCiScheduleActivity.HideControl(Not flag)
        INDLciConsultingRoom.HideControl(flag)
        INDLciScheduleActivityOther.HideControl(flag)
    End Sub

    Private Sub INDSleHealthProfessional_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleHealthProfessional.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                healthProfessional = model.GetCareProfessionalByCode(e.NewValue.ToString())(0)
            End Using
            Using model As New MThirdParty(Me.Tag)
                thirdParty = model.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                If thirdParty.Id = 0 Then
                    'si el medico no esta creado como tercero en la BD no continua el proceso
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", MODULE_NAME), healthProfessional.CodeName)
                    e.Cancel = True
                    Exit Sub
                End If
            End Using
            INDGleSpecialty.Enabled = True
        Else
            INDGleSpecialty.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleRequestHemoReserve_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRequestHemoReserve.EditValueChanged
        If INDSleRequestHemoReserve.EditValue IsNot Nothing AndAlso (Not IsNumeric(INDSleRequestHemoReserve.EditValue)) Then
            INDSleRequestHemoReserve.EditValue = Nothing
        End If
        If INDLcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            If INDSleRequestHemoReserve.EditValue Is Nothing OrElse INDSleRequestHemoReserve.EditValue = 0 Then
                INDLcgHemoDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLcgMedicalAppointment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                CleanControlHemo()
            Else
                INDLcgHemoDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcgMedicalAppointment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If ListMedicalAppointmentHemo Is Nothing Then
                    ListMedicalAppointmentHemo = presenter.GetMedicalAppointmentHemo(IPCODPACI:=patient.IPCODPACI)
                End If
                If ListOrdersExtramuralHemo Is Nothing Then
                    Dim _ListOrdersExtramuralHemo As List(Of HCORHEMCOXpo) = presenter.GetOrdersExtramuralHemo(IPCODPACI:=patient.IPCODPACI)
                    If _ListOrdersExtramuralHemo IsNot Nothing Then
                        For Each item In _ListOrdersExtramuralHemo
                            Dim Count = item.HCORHEMBOLXpo.Count
                            If Count > 0 Then
                                item.Quantitys = Count
                            End If
                        Next
                        ListOrdersExtramuralHemo = _ListOrdersExtramuralHemo
                    End If
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleHemo_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleHemo.EditValueChanged
        If INDSleHemo.EditValue IsNot Nothing AndAlso (Not IsNumeric(INDSleHemo.EditValue)) Then
            INDSleHemo.EditValue = Nothing
        End If
        If HemocomponentId IsNot Nothing Then
            Me.ListReserveHemocomponentDetail = presenter.ListReserveHemocomponentDetail(Me.HemocomponentId)
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProfessional.EditValueChanged
        If INDSleProfessional.EditValue IsNot Nothing AndAlso (Not IsNumeric(INDSleProfessional.EditValue)) Then
            INDSleProfessional.EditValue = Nothing
        End If
        If INDSleProfessional.EditValue IsNot Nothing Then
            SetSpecialties(healthProfessional)
            If INDGleSpecialty.Properties.DataSource IsNot Nothing Then
                Dim list = CType(INDGleSpecialty.Properties.DataSource, List(Of Tuple(Of String, String)))
                If list IsNot Nothing And list.Count > 0 Then
                    'INDGleSpecialty.EditValue = list(0).Item1
                    Me.SpecialityCode = list(0).Item1
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceHemoService_EditValueChanged(sender As Object, e As EventArgs) Handles INDRiceHemoService.EditValueChanged
        Dim checkControl = DirectCast(sender, DevExpress.XtraEditors.CheckEdit)
        Dim _hemocomponentService = DirectCast(INDGvHemoService.GetFocusedRow(), HemocomponentDetail)
        If _hemocomponentService IsNot Nothing Then
            _hemocomponentService.Activated = checkControl.EditValue
            SetImageActivateColumn()
            Me.INDGcHemoService.RefreshDataSource()
            Me.INDGcHemoService.Invalidate()
        End If
    End Sub

    Private Sub INDSleRoom_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleRoom.EditValueChanged
        If FlagLoadPopUpAppointment Then
            Exit Sub
        End If
        Me.ScheduleActivityCode = Nothing
        INDSleScheduleActivity.Properties.DataSource = Nothing
        If INDSleRoom.EditValue IsNot Nothing Then
            Dim AGDISPONSALA = presenter.GetCodProfesional(INDSleRoom.EditValue, INDDteDateMedicalAppointment.EditValue)
            If AGDISPONSALA Is Nothing Then
                Exit Sub
            End If
            If String.IsNullOrEmpty(AGDISPONSALA.CODPROSAL) Then
                Exit Sub
            End If

            INDSleHealthProfessional.EditValue = AGDISPONSALA.CODPROSAL
            INDSleHealthProfessional.Properties.NullText = AGDISPONSALA.INPROFSAL.CodeName
        End If
    End Sub

    Private Sub INDGleServiceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleServiceType.EditValueChanged
        INDSleRoom.EditValue = Nothing
        INDSleRoom.Properties.DataSource = Nothing
        INDSleRoom.Properties.NullText = Nothing
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProfessional_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleProfessional.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                healthProfessional = model.GetCareProfessionalByCode(e.NewValue.ToString())(0)
            End Using
            Using model As New MThirdParty(Me.Tag)
                thirdParty = model.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                If thirdParty.Id = 0 Then
                    'si el medico no esta creado como tercero en la BD no continua el proceso
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", MODULE_NAME), healthProfessional.CodeName)
                    e.Cancel = True
                    Exit Sub
                End If
            End Using
        End If
    End Sub
#End Region

#Region "Actions"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions

        Dim patientAppointment = TryCast(INDGvMedicalAppointment?.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result)

        If patientAppointment Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado una cita"
            Exit Sub
        End If

        ' "Remove"
        Me.CleanPopUpCitas()
        Me.DeletePatientAppointment(False, patientAppointment, Me._codeAppointment)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView4_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView4.Click_ButtonAction, IndigoGridView4.ContexMenuActions, INDGcMedicalAppointmentPatient.DoubleClick
        Dim patientAppointment = TryCast(INDGvMedicalAppointmentPatient?.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result)

        If patientAppointment Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha seleccionado una cita"
            Exit Sub
        End If

        If ValidateCitasMedidas(patientAppointment) Then
            Me.CleanPopUpCitas(True)
            Me.EditPatientAppointment(patientAppointment)
        End If
    End Sub

    '' <summary>
    '' 
    '' </summary>
    '' <param name="sender"></param>
    '' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _hemocomponent As Hemocomponent = CType(INDGvHemoDetail.GetFocusedRow(), Hemocomponent)
            If _hemocomponent IsNot Nothing Then
                'Eliminar Detalle de orden de servicio
                DeleteServiceOrderDetailByHemocomponentId(_hemocomponent.Id)
                ListHemocomponent.Remove(_hemocomponent)
                INDGcHemoDetail.DataSource = ListHemocomponent
                INDGcHemoDetail.RefreshDataSource()
            End If
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        serviceOrderDetail = CType(INDGvService.GetFocusedRow(), ServiceOrderDetail)
        If (serviceOrderDetail.IdCita <> 0 OrElse serviceOrderDetail.HemocomponentId <> 0) AndAlso Not PermissionsForm.ContainsKey(139) AndAlso sender.tag = "Edit" Then
            Mensaje(EeventViewerImages.Advertencia) = "Este item no se puede editar porque ha sido agregado por una cita o Hemocomponente"
            Exit Sub
        End If
        If (serviceOrderDetail.IdCita <> 0 OrElse serviceOrderDetail.HemocomponentId <> 0) AndAlso sender.tag <> "Edit" Then
            Mensaje(EeventViewerImages.Advertencia) = "Este item no se puede eliminar  porque ha sido agregado por una Cita o Hemocomponente"
            Exit Sub
        End If
        Select Case (sender.Tag)
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                indexEditItem = ListServiceOrderDetail.IndexOf(serviceOrderDetail)
                rowEditing = INDGvService.FocusedRowHandle
                OpenPopUpServiceOrderDetail(True)
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If serviceOrderDetail.Id > 0 Then
                        serviceOrderDetail.MarkAsDeleted()
                        ListServiceOrderDetail = _listServiceOrderDetail
                    Else
                        ListServiceOrderDetail.Remove(serviceOrderDetail)
                    End If
                    INDGcServiceOrderDetail.DataSource = ListServiceOrderDetail
                    INDGcServiceOrderDetail.RefreshDataSource()
                End If
        End Select
    End Sub
#End Region

#Region "CustomDrawCell"
    Private Sub INDGvMedicalAppointmentPatient_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvMedicalAppointmentPatient.CustomDrawCell
        Dim item = CType(Me.INDGvMedicalAppointmentPatient.GetRow(e.RowHandle), SP_AD_ListarCitasMedicasNativo_Result)
        If item?.Profesional Is Nothing AndAlso item?.Especialidad Is Nothing Then
            If item IsNot Nothing AndAlso item.TipoSolicitud <> 1 Then
                If e.Column.Name.Equals(INDcolProfessional.Name) OrElse e.Column.Name.Equals(INDcolSpecialty.Name) Then 'e.Column.Name.Equals(ColUbicacion.Name) OrElse
                    e.DisplayText = "No Aplica"
                End If
            End If
        End If
    End Sub

    Private Sub INDGvMedicalAppointment_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvMedicalAppointment.CustomDrawCell
        Dim item = CType(Me.INDGvMedicalAppointment.GetRow(e.RowHandle), SP_AD_ListarCitasMedicasNativo_Result)
        If item IsNot Nothing AndAlso item.TipoSolicitud <> 1 Then
            If item.TipoSolicitud = 2 Then
                'Apoyo Dx
                If item?.Profesional Is Nothing AndAlso e.Column.Name.Equals(ColProfesional1.Name) Then
                    e.DisplayText = "No Aplica"
                End If

                If item?.Especialidad Is Nothing AndAlso e.Column.Name.Equals(ColEspecialidad1.Name) Then
                    e.DisplayText = "No Aplica"
                End If

                If item?.TipoCita Is Nothing OrElse item.TipoCita <= 0 Then
                    If e.Column.Name.Equals(ColTipoConsulta.Name) Then
                        e.DisplayText = "No Aplica"
                    End If
                End If
            Else
                'Tratamiento Especial
                If e.Column.Name.Equals(ColProfesional1.Name) OrElse e.Column.Name.Equals(ColEspecialidad1.Name) Then
                    e.DisplayText = "No Aplica"
                End If
            End If
        ElseIf item IsNot Nothing AndAlso item.TipoSolicitud = 1 Then
            If e.Column.Name.Equals(ColTipoConsulta.Name) AndAlso (item.TipoCita Is Nothing AndAlso item.Tipo <> 8) Then
                e.DisplayText = "No Aplica"
            End If
        End If
    End Sub
#End Region

#Region "ShowinEditor"
    Private Sub INDGvMedicalAppointment_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvMedicalAppointment.ShowingEditor
    End Sub
#End Region

#Region "DatasourceChanged"
    Private Sub INDGcServiceOrderDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcServiceOrderDetail.DataSourceChanged
        INDSleCareGroup.Properties.ReadOnly = INDGcServiceOrderDetail.DataSource IsNot Nothing AndAlso CType(INDGcServiceOrderDetail.DataSource, IList).Count > 0
    End Sub
#End Region

#Region "MouseEnter"
    Private Sub INDsleAdmissionNumber_MouseEnterAdmission(sender As Object, e As EventArgs) Handles INDSleAdmissionNumber.MouseEnterAdmission
        If admission IsNot Nothing Then
            INDFpAdmission.ShowBeakForm()
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' evento que limpia el poup cuando el usuario esta editando
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbCleanPopup_Click(sender As Object, e As EventArgs) Handles INDsbCleanPopup.Click
        CleanPopUpCitas()
    End Sub

    Private Sub INDBtnAddService_Click(sender As Object, e As EventArgs) Handles INDBtnAddService.Click
        OpenPopUpServiceOrderDetail(False)
    End Sub

    Private Async Sub INDsbAddCitas_Click(sender As Object, e As EventArgs) Handles INDsbAddCitas.Click
        If INDGcMedicalAppointmentPatient.DataSource IsNot Nothing AndAlso CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).Count > 0 _
            AndAlso CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).Where(Function(o) o.Sel = True).ToList().Count > 0 Then
            citasToAdd = CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).Where(Function(o) o.Sel = True).ToList()
            Await AggregateCitas()
        End If
    End Sub

    Private Async Function AggregateCitas() As Task(Of Boolean)
        'Codigo para validar si es obligatorio Apl. a cons de paquetes de Ttos Especiales y Diálisis
        If citasToAdd.Where(Function(o) o.PaqDialisis IsNot Nothing AndAlso o.PaqDialisis Or o.TipoSolicitud = 3).ToList().Count > 0 Then
            If admission IsNot Nothing Then 'Verificamos que si se tenga un ingreso previo seleccionado. 
                If admission.TRATAESPECIA Is Nothing OrElse (admission.TRATAESPECIA <> 2 AndAlso admission.TRATAESPECIA <> 3) Then 'Se pregunta si es un ingreso de tratamiento especial (2-Renal y 3-Oncologico)
                    Dim ListActivities As String = ""
                    'Armamos una lista de actividades de la cita de dialisis que tengan el parametro PaqDialisis en true o '1'
                    Parallel.ForEach(citasToAdd.Where(Function(o) o.PaqDialisis IsNot Nothing AndAlso o.PaqDialisis Or o.TipoSolicitud = 3).ToList(),
                                     Sub(obj As SP_AD_ListarCitasMedicasNativo_Result)
                                         Dim ActivityTemp As String = ""
                                         If obj.ActividadMedica <> "" Then
                                             ActivityTemp = obj.ActividadMedica
                                         End If
                                         ListActivities = ListActivities & vbCrLf & ActivityTemp
                                     End Sub)
                    Mensaje(EeventViewerImages.Advertencia) = "Para cargar las citas con las siguientes actividades debe seleccionar un ingreso de tratamiento especial: " & vbCrLf & ListActivities
                    INDSleAdmissionNumber.Focus()
                    Return False
                End If
            Else
                Dim admissionType = TryCast(TryCast(GridView41.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, AdmissionTypeXpo)
                'se valida que el tipo de admision sea de tratamiento especial
                If admissionType Is Nothing OrElse Not admissionType?.IsSpecialTreatment Then
                    'Validamos que ya se ha seleccionado un ingreso para el paciente.
                    Mensaje(EeventViewerImages.Advertencia) = "Por favor seleccione un ingreso de tratamiento especial para continuar."
                    INDSleAdmissionNumber.Focus()
                    Return False
                End If
            End If
        End If

        'Si el grupo de atención aplica a RIAS y en la rejilla hay items seleccionados que también aplican se realiza la validación de RIAS
        If ApplyRIASCareGroup AndAlso citasToAdd.Where(Function(o) o.RiasCupsId > 0).ToList().Count > 0 Then
            'Listado de parámetros para enviar a validar las rias
            Dim ListParameters As New List(Of Tuple(Of String, Integer, String, Integer, DateTime))

            'Se recorren los items que estan checkiados y además que apliquen a RIAS para armar el listado para enviar a validar
            Parallel.ForEach(citasToAdd.Where(Function(o) o.RiasCupsId > 0).ToList(),
                                 Sub(obj As SP_AD_ListarCitasMedicasNativo_Result)
                                     Dim quantityTemp As Integer = 1
                                     If obj.CantidadServicio > 0 Then
                                         quantityTemp = obj.CantidadServicio
                                     End If
                                     ListParameters.Add(New Tuple(Of String, Integer, String, Integer, DateTime)(patient.IPCODPACI.Trim(), obj.RiasCupsId, obj.CodigoServicio, quantityTemp, obj.FechaCita))
                                 End Sub)

            'Si hay rias por validar
            If ListParameters IsNot Nothing AndAlso ListParameters.Count > 0 Then
                'Se envia las rias al sp para validar si se pueden agregar
                Dim resultValidateRIAS As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) = Nothing

                Await Task.Factory.StartNew(Sub()
                                                Using modelServiceOrder As New MServiceOrder(Me.Tag)
                                                    resultValidateRIAS = modelServiceOrder.SP_RIAS_ValidacionCUPSRIASDontAsync(ListParameters)
                                                End Using
                                            End Sub)

                'Se retorna el error si hubo
                If resultValidateRIAS.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = resultValidateRIAS.Message
                    Return False
                End If
            End If
        End If

        If _listaCitasMedicas IsNot Nothing AndAlso INDGcMedicalAppointmentPatient.DataSource IsNot Nothing AndAlso Not ValidateCitasMedidas() Then
            Return False
        End If
        If _listaCitasMedicas Is Nothing Then
            _listaCitasMedicas = New List(Of SP_AD_ListarCitasMedicasNativo_Result)()
        End If
        If citasToAdd.Any(Function(x) x.TipoCita IsNot Nothing AndAlso x.TipoCita = 3) AndAlso INDGleAdmissionOption.EditValue IsNot Nothing AndAlso CByte(INDGleAdmissionOption.EditValue) = 2 Then
            'Esta validacion se hace para cuando el tipo de la cita sea pos operatorio no deje relacionar un ingreso existente sino que obligatoriamente se cree un nuevo ingreso
            Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar una cita tipo Pos Operatorio debido a que en los datos de ingreso la opción de ingreso esta como ingreso existente"
            Return False
        End If

        Dim citasToGenerateServiceOrder = citasToAdd.Where(Function(x) x.TipoCita Is Nothing OrElse x.TipoCita <> 3).ToList()
        If citasToGenerateServiceOrder IsNot Nothing AndAlso citasToGenerateServiceOrder.Count > 0 Then
            If Not Await CreateServiceOrderDetailItem(citasToGenerateServiceOrder, False) Then
                _stopAggregateCita = False
                Return False
            End If
        End If

        If _stopAggregateCita Then
            _stopAggregateCita = False
            Return False
        End If

        _listaCitasMedicas.AddRange(citasToAdd)
        ListCitasMedicas = _listaCitasMedicas
        If CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)) IsNot Nothing Then
            CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).ForEach(Sub(x)
                                                                                                                         x.Sel = False
                                                                                                                     End Sub)
        End If

        INDGleMedicalAppointmentType.EditValue = Nothing
        INDlyItemApplyRIAS.HideControl()
        INDlyItemRIASCups.HideControl()
        INDlyItemDescription.HideControl()

        INDGcMedicalAppointmentPatient.RefreshDataSource()
        INDPceMedicalAppointment.ClosePopup()
        INDGvMedicalAppointment.FocusedRowHandle = 0
        INDGvMedicalAppointment.FocusedColumn = ColInvoice
        ctrInfo.PrintInfo()
        Return True
    End Function

    Private Function ValidateCitasMedidas(Optional appointment As SP_AD_ListarCitasMedicasNativo_Result = Nothing) As Boolean
        Dim errorValidation As New StringBuilder()
        Dim labelMessage As String = "cita"
        If _payrollFunctionalUnitXpo.UnitType = 19 Then
            labelMessage = "cirugía"
        End If

        Dim listAppointment As List(Of SP_AD_ListarCitasMedicasNativo_Result) = New List(Of SP_AD_ListarCitasMedicasNativo_Result)

        If appointment IsNot Nothing Then
            listAppointment.Add(appointment)
        ElseIf TryCast(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result))? _
                                                                                    .Any(Function(x) x.Sel) Then
            listAppointment = TryCast(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result))? _
                                                                                    .FindAll(Function(x) x.Sel)
        End If

        _listaCitasMedicas _
        ?.FindAll(Function(a) listAppointment _
                            .Any(Function(y) y.Equals(a) OrElse y.Codigo = a.Codigo)) _
        .ForEach(Sub(o)

                     Dim obj = listAppointment?.FirstOrDefault(Function(x) x.Equals(o) OrElse x.Codigo = o.Codigo)

                     If obj IsNot Nothing Then
                         If obj.TipoSolicitud = 1 Then
                             'Cita Médica
                             errorValidation.AppendLine(String.Format("La cita con fecha {0} profesional {1} y especialidad {2} ya está agregada", o.FechaCita, o.Profesional, o.Especialidad))
                         Else
                             'Apoyo Dx
                             errorValidation.AppendLine(String.Format("La {0} con fecha {1} ya está agregada", labelMessage, obj.FechaCita))
                         End If
                     End If
                 End Sub)
        If errorValidation.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorValidation.ToString()
            Return False
        End If
        Return True
    End Function

    Private Async Sub INDsbAddCita_Click(sender As Object, e As EventArgs) Handles INDsbAddCita.Click
        If Not ValidateControlsCita() Then
            Exit Sub
        End If

        For Each rowData In Me._selectorCUPS.ValuesCache
            Dim cita = New SP_AD_ListarCitasMedicasNativo_Result
            With cita

                .CodigoServicio = rowData.Key
                .Servicio = rowData.Value("DESSERIPS")
                .CantidadServicio = INDSeQuantity.EditValue
                .CodigoProfesional = INDSleHealthProfessional.EditValue

                'se envia la fecha del momento que se guarda, se cambia ya que anteriormente se enviaba la fehca de cuando se abria el formulario
                .FechaCita = INDDteDateMedicalAppointment.EditValue
                .CodigoEspecialidad = INDGleSpecialty.EditValue
                .NitMedico = thirdParty.Nit
                .TipoSolicitud = ActivityType
                .RiasCupsId = 0
                .RiasId = 0

                If INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .RiasCupsId = INDsleRIASCups.EditValue
                    .RiasId = RiasId
                End If

                .ContractDescriptionId = Nothing
                .CUPSEntityContractDescriptionId = Nothing
                If INDlyItemDescription.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    Dim info = presenter.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                    .CUPSEntityContractDescriptionId = INDsleDescription.EditValue
                    .ContractDescriptionId = info.ContractDescriptionId.Id
                    .DescriptionCodeName = info.ContractDescriptionId.CodeName
                End If

                Dim maxCitasCheck = 0
                Dim maxCitasAdd = 0
                If CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)) IsNot Nothing AndAlso CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).Count > 0 Then
                    maxCitasCheck = (From cm In CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)) Select cm.Codigo).Max(Function(o) o)
                End If
                If ListCitasMedicas IsNot Nothing AndAlso ListCitasMedicas.Count > 0 Then
                    maxCitasAdd = ListCitasMedicas.Max(Function(o) o.Codigo)
                End If

                .ActivityType = ActivityType
                Me._consultationActivity = If(.ActivityType = 2, CByte(.ActivityType), Me._consultationActivity)
                .ConsultationActivity = Me._consultationActivity
                .ServiceType = Me.ServiceType

                If Not String.IsNullOrEmpty(Me._codeAppointment) Then
                    .Codigo = Me._codeAppointment
                    .CodeTmp = .Codigo
                Else
                    .Codigo = IIf(maxCitasCheck > maxCitasAdd, maxCitasCheck + 1, maxCitasAdd + 1)
                End If

                .Profesional = If(INDSleHealthProfessional.Text.Contains("-"), INDSleHealthProfessional.Text.Split("-")(1).Trim(), INDSleHealthProfessional.Text)
                .Especialidad = INDGleSpecialty.Text.Split("-")(1).Trim()
                .TipoCita = INDGleMedicalAppointmentType.EditValue
                .Tipo = rowData.Value("TIPSERIPS")
                .IsFalseId = False
                .CODACTMED = Me.ScheduleActivityCode
                .CodigioActividadMedica = Me.ScheduleActivityCode
                .ActividadMedica = If(ActivityType = 2, INDSleScheduleActivity.Text, INDSleScheduleActivityOther.Text)

                If ActivityType = 2 Then
                    .IDSALA = INDSleRoom.EditValue
                    .ConsultorioName = INDSleRoom.Text
                Else
                    .Consultorio = Me.ConsultingRoom
                    .ConsultorioName = Me.INDSleConsultingRoom.Text
                End If

                .ACTIVICON = presenter.GetScheduleActivityType(ScheduleActivityCode)

            End With

            citasToAdd = {cita}.ToList()
            Dim addOrder = Await AggregateCitas()
        Next
        INDPceMedicalAppointment.ShowPopup()
        INDSleCUPSCrystal.Focus()
        CleanPopUpCitas()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbHemoAdd_Click(sender As Object, e As EventArgs) Handles INDSbHemoAdd.Click
        Dim validacion As String = String.Empty
        If HemocomponentId Is Nothing Then
            validacion = "Hemocomponente"
        End If
        If ProfessionalId Is Nothing Then
            validacion = String.Format("{0}{1}Profesional", validacion, IIf(String.IsNullOrEmpty(validacion), "", vbNewLine))
        End If
        If Not String.IsNullOrEmpty(validacion) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Los siguientes campos son obligatorios:{0}{1}", vbNewLine, validacion)
        ElseIf ListHemocomponent IsNot Nothing AndAlso ListHemocomponent.Count > 0 AndAlso ListHemocomponent.FirstOrDefault(Function(d) d.Id = Me.HemocomponentId) IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya se agregó el hemocomponente"
        ElseIf thirdParty.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", MODULE_NAME), healthProfessional.CodeName)
        ElseIf HemoQuantity = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad debe ser mayor a 0"
        ElseIf String.IsNullOrEmpty(Me.SpecialityCode) Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo determinar la especialidad del médico"
        Else
            If Me.ListHemocomponent Is Nothing Then Me.ListHemocomponent = New List(Of Hemocomponent)
            Me.Hemocomponente = New Hemocomponent
            Dim _hemocomponentDetail As New HemocomponentDetail
            Dim _hemocomponentXpo = presenter.GetHemocomponentById(Me.HemocomponentId)
            Me.Hemocomponente.Id = Me.HemocomponentId
            Me.Hemocomponente.Code = _hemocomponentXpo.CODCOMSAM
            Me.Hemocomponente.Description = _hemocomponentXpo.DESCOMSAM
            Me.Hemocomponente.ProfessionalId = Me.ProfessionalId
            Me.Hemocomponente.Professional = healthProfessional.CodeName
            Me.Hemocomponente.Quantity = Me.HemoQuantity
            Me.Hemocomponente.SpecialityCode = Me._SpecialityCode
            Me.Hemocomponente.Details = New List(Of HemocomponentDetail)
            For Each d In Me.ListReserveHemocomponentDetail
                Dim _cupsXPO = presenter.GetCUPSCrystalByCode(d.CODSERIPS)
                _hemocomponentDetail = New HemocomponentDetail
                _hemocomponentDetail.CodeServiceIPS = d.CODSERIPS
                _hemocomponentDetail.DescriptionServiceIPS = _cupsXPO.DESSERIPS
                _hemocomponentDetail.HemocomponentId = Me.HemocomponentId
                _hemocomponentDetail.Id = d.ID
                _hemocomponentDetail.kindLoad = d.TIPOCARGUE
                _hemocomponentDetail.TypeServiceIPS = d.TIPSERIPS
                If d.TIPOCARGUE = 1 Then _hemocomponentDetail.Activated = True
                Me.Hemocomponente.Details.Add(_hemocomponentDetail)
            Next
            INDGcHemoService.DataSource = Me.Hemocomponente.Details
            If Me.Hemocomponente.Details.Any(Function(d) Not d.Activated) Then
                INDLcgHemo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLcgHemoService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDTcgHemo.SelectedTabPageIndex = 1
            Else
                AddHemoCreateServiceOrder()
            End If
        End If

    End Sub

    ''' <summary>
    ''' evento para añadir las citas de hemocomponentes a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbaddAppointmentHemo_Click(sender As Object, e As EventArgs) Handles INDSbaddAppointmentHemo.Click
        If Not ListMedicalAppointmentHemo.Any(Function(x) x.Selc = True) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una cita para agregar"
            Exit Sub
        End If
        If Me.ListHemocomponent Is Nothing Then Me.ListHemocomponent = New List(Of Hemocomponent)

        If Me.ListHemocomponent.Any(Function(g) ListMedicalAppointmentHemo.Where(Function(j) j.Selc = True).Select(Function(d) d.CODAUTONU).ToList().Contains(g.IdAGASICITA)) Then
            Mensaje(EeventViewerImages.Advertencia) = "una cita(s) seleccionada ya se encuentra agregada"
            Exit Sub
        End If

        If Not ListMedicalAppointmentHemo.Any(Function(l) l.AppointmentHemocomponentsXpo.Any() And l.Selc = True) Then
            Mensaje(EeventViewerImages.Advertencia) = "La cita(s) seleccionada(s) no tiene una reserva de hemocomponente"
            Exit Sub
        End If

        Dim _listHemocomponent = New List(Of Hemocomponent)
        AsyncLoader(True)
        Await Task.Factory.StartNew(Sub()
                                        For Each Item In ListMedicalAppointmentHemo.Where(Function(j) j.Selc = True)

                                            For Each Appointment In Item.AppointmentHemocomponentsXpo
                                                Me.Hemocomponente = New Hemocomponent
                                                Me.Hemocomponente.Id = Appointment.IdBloodComponent
                                                Me.Hemocomponente.Code = Appointment.BloodComponent.CODCOMSAM
                                                Me.Hemocomponente.Description = Appointment.BloodComponent.DESCOMSAM
                                                Me.Hemocomponente.ProfessionalId = healthProfessional.CODPROSAL
                                                Me.Hemocomponente.Professional = healthProfessional.CodeName
                                                Me.Hemocomponente.Quantity = Appointment.Quantity
                                                Me.Hemocomponente.SpecialityCode = ""
                                                Me.Hemocomponente.IdAGASICITA = Appointment.IdAGASICITA
                                                Me.Hemocomponente.VolumenComponent = Appointment.VolumenComponent
                                                Me.Hemocomponente.Details = New List(Of HemocomponentDetail)
                                                For Each CupsHemo In Appointment.AppointmentHemocomponentsCUPSXpo
                                                    Dim _hemocomponentDetail = New HemocomponentDetail
                                                    Dim _cupsXPO = presenter.GetCUPSCrystalByCode(CupsHemo.CODSERIPS)
                                                    _hemocomponentDetail.CodeServiceIPS = CupsHemo.CODSERIPS
                                                    _hemocomponentDetail.DescriptionServiceIPS = _cupsXPO.DESSERIPS
                                                    _hemocomponentDetail.HemocomponentId = Appointment.IdBloodComponent
                                                    _hemocomponentDetail.Id = CupsHemo.Id
                                                    _hemocomponentDetail.kindLoad = CupsHemo.TypeLoad
                                                    _hemocomponentDetail.TypeServiceIPS = Appointment.RequestType
                                                    _hemocomponentDetail.IdRelatedDescription = CupsHemo.ContractDescriptionId
                                                    If CupsHemo.TypeLoad = 1 Then _hemocomponentDetail.Activated = True
                                                    Me.Hemocomponente.Details.Add(_hemocomponentDetail)
                                                Next
                                                _listHemocomponent.Add(Me.Hemocomponente)
                                            Next
                                        Next
                                    End Sub)
        AddHemoCreateServiceOrder(_listHemocomponent)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' evento para añadir las ordenes exramural de hemocomponentes a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbaddAppointmentHemo1_Click(sender As Object, e As EventArgs) Handles INDSbaddHemoAmbulatoryOrders.Click
        If Not ListOrdersExtramuralHemo.Any(Function(x) x.Selc = True) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Hemocomponente"
            Exit Sub
        End If
        If Me.ListHemocomponent Is Nothing Then Me.ListHemocomponent = New List(Of Hemocomponent)

        If Me.ListHemocomponent.Any(Function(g) ListOrdersExtramuralHemo.Where(Function(j) j.Selc = True).Select(Function(d) d.ID).ToList().Contains(g.IdHCORHEMCO)) Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra agregado el Hemocomponente"
            Exit Sub
        End If


        Dim _listHemocomponent = New List(Of Hemocomponent)
        AsyncLoader(True)
        Await Task.Factory.StartNew(Sub()
                                        For Each Item In ListOrdersExtramuralHemo.Where(Function(j) j.Selc = True)

                                            For Each Appointment In Item.HCORHEMBOLXpo
                                                If Me.Hemocomponente IsNot Nothing Then
                                                    Dim existingHemocomponent = _listHemocomponent.FirstOrDefault(Function(x) x.IdHCORHEMCO = Appointment.HCORHEMCOID)
                                                    If existingHemocomponent IsNot Nothing Then
                                                        existingHemocomponent.Quantity += 1
                                                        Continue For
                                                    End If
                                                End If
                                                Me.Hemocomponente = New Hemocomponent
                                                Me.Hemocomponente.Id = Appointment.HCCOMSAN.ID
                                                Me.Hemocomponente.Code = Appointment.HCCOMSAN.CODCOMSAM
                                                Me.Hemocomponente.Description = Appointment.HCCOMSAN.DESCOMSAM
                                                Me.Hemocomponente.ProfessionalId = healthProfessional.CODPROSAL
                                                Me.Hemocomponente.Professional = healthProfessional.CodeName
                                                Me.Hemocomponente.Quantity = 1
                                                Me.Hemocomponente.SpecialityCode = ""
                                                Me.Hemocomponente.IdHCORHEMCO = Appointment.HCORHEMCO.ID
                                                Me.Hemocomponente.VolumenComponent = Appointment.VolumeTransfuse
                                                Me.Hemocomponente.Details = New List(Of HemocomponentDetail)
                                                For Each CupsHemo In Appointment.HCCOMSAN.HCCOMSANDXpo
                                                    If CupsHemo.TIPSERIPS = 1 Then

                                                        Dim _hemocomponentDetail = New HemocomponentDetail
                                                        Dim _cupsXPO = presenter.GetCUPSCrystalByCode(CupsHemo.CODSERIPS)
                                                        _hemocomponentDetail.CodeServiceIPS = CupsHemo.CODSERIPS
                                                        _hemocomponentDetail.DescriptionServiceIPS = _cupsXPO.DESSERIPS
                                                        _hemocomponentDetail.HemocomponentId = Appointment.HCCOMSAN.ID
                                                        _hemocomponentDetail.Id = CupsHemo.ID
                                                        _hemocomponentDetail.kindLoad = CupsHemo.TIPOCARGUE
                                                        _hemocomponentDetail.TypeServiceIPS = CupsHemo.TIPSERIPS
                                                        _hemocomponentDetail.IdRelatedDescription = CupsHemo.ContractDescriptionId
                                                        If CupsHemo.TIPOCARGUE = 1 Then _hemocomponentDetail.Activated = True
                                                        Me.Hemocomponente.Details.Add(_hemocomponentDetail)
                                                    End If

                                                Next
                                                _listHemocomponent.Add(Me.Hemocomponente)
                                            Next
                                        Next
                                    End Sub)
        AddHemoCreateServiceOrder(_listHemocomponent)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbHemoServiceAdd_Click(sender As Object, e As EventArgs) Handles INDSbHemoServiceAdd.Click
        If Not Me.Hemocomponente.Details.Any(Function(d) d.Activated) Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione algun servicio"
        ElseIf Me.ListHemocomponent.FirstOrDefault(Function(h) h.Id = Me.Hemocomponente.Id) IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya se agregó el hemocomponente"
        Else
            For Each detail In Me.Hemocomponente.Details.Where(Function(d) Not d.Activated).ToList
                Me.Hemocomponente.Details.Remove(detail)
            Next
            AddHemoCreateServiceOrder()
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceHemoDetailAdd_Click(sender As Object, e As EventArgs) Handles INDPceHemoDetailAdd.Click
        INDTcgHemo.SelectedTabPageIndex = 0
        INDLcgHemo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLcgHemoService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        'Se obtiene el item de la rejilla
        Dim info = DirectCast(INDGvMedicalAppointmentPatient.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result)

        If e.NewValue Then
            'Si exige confirmación de cita
            If info.RequiresConfirmAppointment Then
                If info.Ingreso Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "La cita no se encuentra confirmada"
                    info.Sel = False
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

        'Se realizan validaciones siempre y cuando el tipo de unidad funcional sea cirugia = 19
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing AndAlso _payrollFunctionalUnitXpo.UnitType = 19 Then
            'Se valida si ya ha sido cargado un item en la rejilla
            If INDGvMedicalAppointment.RowCount > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede seleccionar el registro porque ya existe un item cargado en la rejilla principal"
                info.Sel = False
                e.Cancel = True
                Exit Sub
            End If

            If e.NewValue Then
                'Se valida si el item es principal
                If info.Principal = False Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede seleccionar el registro ya que no es principal"
                    info.Sel = False
                    e.Cancel = True
                    Exit Sub
                End If

                'Se valida que no hayan seleccionado anteriormente un principal
                If CType(INDGcMedicalAppointmentPatient.DataSource, List(Of SP_AD_ListarCitasMedicasNativo_Result)).Where(Function(o) o.Sel = True).ToList().Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede seleccionar el registro ya que ya fue seleccionado un registro principal"
                    info.Sel = False
                    e.Cancel = True
                    Exit Sub
                End If

                'Se asigna el ingreso
                ExecuteGetAdmission(info.Ingreso)
            End If

            'Se asigna el nuevo valor
            info.Sel = e.NewValue
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiceHemoService_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRiceHemoService.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing And (Not e.NewValue) Then
            Dim _hemocomponentDetail = DirectCast(INDGvHemoService.GetFocusedRow(), HemocomponentDetail)
            If _hemocomponentDetail IsNot Nothing AndAlso _hemocomponentDetail.kindLoad = 1 Then
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

#End Region

#Region "MasterRowExpanded"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvHemoDetail_MasterRowExpanded(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles INDGvHemoDetail.MasterRowExpanded
        Dim masterView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim detailView = CType(masterView?.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)
        If detailView Is Nothing Then Exit Sub
        For i = 0 To detailView.Columns.Count - 1
            If detailView.Columns(i).Name.Contains("CodeServiceIPS") Then
                detailView.Columns(i).Caption = "Código"
            ElseIf detailView.Columns(i).Name.Contains("DescriptionServiceIPS") Then
                detailView.Columns(i).Caption = "Descripción"
            Else
                detailView.Columns(i).Visible = False
            End If
            detailView.Columns(i).OptionsColumn.AllowEdit = False
        Next
    End Sub

#End Region

#Region "Custom Repository"

    Private Sub INDGvMedicalAppointmentPatient_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvMedicalAppointmentPatient.CustomColumnDisplayText
        If e.Column.FieldName = "CUPSEntityContractDescriptionId" Then
            If e.ListSourceRowIndex >= 0 Then
                Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                e.DisplayText = view.GetListSourceRowCellValue(e.ListSourceRowIndex, "DescriptionCodeName").ToString()
            End If
        End If

        If e.Column.FieldName = "ACTIVICON" AndAlso e.Value IsNot Nothing Then
            Select Case CInt(e.Value)
                Case 0
                    e.DisplayText = "Cita Médica"
                Case 1
                    e.DisplayText = "Procedimiento QX"
                Case 2
                    e.DisplayText = "Otros Procedimientos"
                Case 3
                    e.DisplayText = "Tratamientos Oncológicos"
                Case 4
                    e.DisplayText = "Cita Médica o Consulta Externa"
                Case 5
                    e.DisplayText = "Mixta"
                Case 6
                    e.DisplayText = "Transfusión Hemocomponentes"
                Case Else
                    Exit Sub
            End Select
        End If
    End Sub

    Private Sub INDgvProceduresNoQx_ShownEditor(sender As Object, e As EventArgs) Handles INDGvMedicalAppointmentPatient.ShownEditor
        Dim view = CType(sender, DevExpress.XtraGrid.Views.Base.ColumnView)
        If view.FocusedColumn.FieldName = "CUPSEntityContractDescriptionId" AndAlso TypeOf view.ActiveEditor Is DevExpress.XtraEditors.SearchLookUpEdit Then
            Dim edit = CType(view.ActiveEditor, DevExpress.XtraEditors.SearchLookUpEdit)
            Dim cupsEntityCode = view.GetFocusedRowCellValue("CodigoServicio")
            edit.Properties.DataSource = presenter.ListContractDescriptionsByCupsEntityCode(cupsEntityCode)
        End If
    End Sub

    Private Sub INDrptSleContractDescriptionProceduresNoQx_EditValueChanged(sender As Object, e As EventArgs) Handles INDrptSleContractDescriptionMedicalAppointmentPatient.EditValueChanged
        Dim searchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        If searchLookUpEdit Is Nothing Then
            Exit Sub
        End If

        Dim selectedhandle = INDGvMedicalAppointmentPatient.GetSelectedRows()
        If selectedhandle IsNot Nothing Then
            Dim headerRow = CType(INDGvMedicalAppointmentPatient.GetFocusedRow(), SP_AD_ListarCitasMedicasNativo_Result)
            If searchLookUpEdit.EditValue Is Nothing Then
                headerRow.ContractDescriptionId = Nothing
                headerRow.DescriptionCodeName = String.Empty
            Else
                Dim riSearchLookUpEdit = CType(searchLookUpEdit.Properties, DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit)
                Dim rowByKeyValue = DirectCast(DirectCast(riSearchLookUpEdit.GetRowByKeyValue(searchLookUpEdit.EditValue), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.ContractRepository.CUPSEntityContractDescriptionsXpo)
                If rowByKeyValue IsNot Nothing Then
                    headerRow.ContractDescriptionId = rowByKeyValue.ContractDescriptionId.Id
                    headerRow.DescriptionCodeName = rowByKeyValue.ContractDescriptionId.CodeName
                End If
            End If
        End If
    End Sub

#End Region

#Region "CustomDisplayText"
    ''' <summary>
    ''' evento para establecer el texto del combo de color en vacio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRIColor_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDRIColor.CustomDisplayText
        e.DisplayText = String.Empty
    End Sub


    ''' <summary>
    ''' Método para mostrar el texto según el tipo de actividad de agendamiento para tipo de cita apoyo diagnnóstico y terapéutico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SearchLookUpEdit10View11_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles SearchLookUpEdit10View11.CustomColumnDisplayText
        If e.Column.FieldName = "AGACTIMED.ACTIVICON" AndAlso e.Value IsNot Nothing Then
            Select Case CInt(e.Value)
                Case 0
                    e.DisplayText = "Cita Médica"
                Case 1
                    e.DisplayText = "Procedimiento QX"
                Case 2
                    e.DisplayText = "Otros Procedimientos"
                Case 3
                    e.DisplayText = "Tratamientos Oncológicos"
                Case 4
                    e.DisplayText = "Cita Médica o Consulta Externa"
                Case 5
                    e.DisplayText = "Mixta"
                Case 6
                    e.DisplayText = "Transfusión Hemocomponentes"
                Case Else
                    Exit Sub
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Método para mostrar el texto según el tipo de actividad de agendamiento para tipo de cita Otras Citas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SearchLookUpEdit10View111_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles SearchLookUpEdit10View111.CustomColumnDisplayText
        If e.Column.FieldName = "ActivityType" AndAlso e.Value IsNot Nothing Then
            Dim activityType As Integer

            If Integer.TryParse(Convert.ToString(e.Value), activityType) Then
                Select Case activityType
                    Case 0
                        e.DisplayText = "Cita Médica"
                    Case 1
                        e.DisplayText = "Procedimiento QX"
                    Case 2
                        e.DisplayText = "Otros Procedimientos"
                    Case 3
                        e.DisplayText = "Tratamientos Oncológicos"
                    Case 4
                        e.DisplayText = "Cita Médica o Consulta Externa"
                    Case 5
                        e.DisplayText = "Mixta"
                    Case 6
                        e.DisplayText = "Transfusión Hemocomponentes"
                    Case Else
                        Exit Sub
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método para mostrar el texto según el tipo de actividad de agendamiento en la sección de Citas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvMedicalAppointment_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvMedicalAppointment.CustomColumnDisplayText
        If e.Column.FieldName = "ACTIVICON" AndAlso e.Value IsNot Nothing Then

            Select Case CInt(e.Value)
                Case 0
                    e.DisplayText = "Cita Médica"
                Case 1
                    e.DisplayText = "Procedimiento QX"
                Case 2
                    e.DisplayText = "Otros Procedimientos"
                Case 3
                    e.DisplayText = "Tratamientos Oncológicos"
                Case 4
                    e.DisplayText = "Cita Médica o Consulta Externa"
                Case 5
                    e.DisplayText = "Mixta"
                Case 6
                    e.DisplayText = "Transfusión Hemocomponentes"
                Case Else
                    Exit Sub
            End Select
        End If
    End Sub
#End Region

#Region "Selector"

    ''' <summary>
    ''' 
    ''' </summary>
    Private _selectorCUPS As SelectorCache = New SelectorCache("CODSERIPS", "CODSERIPS", "CodeName", "TIPSERIPS", "APLICARIAS", "DESSERIPS")

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvCupsCrystal.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCupsCrystal" Then
                e.Value = _selectorCUPS.ValidateExistsRow(e.Row)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvSelector_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCupsCrystal.RowCellClick
        If e.Column.FieldName.Contains("UnboundSelection") Then
            Dim selector As SelectorCache = Nothing
            Dim view = TryCast(sender, GridView)
            If view.Name = "INDGvCupsCrystal" Then
                selector = _selectorCUPS
            End If

            If e.RowHandle >= 0 Then
                Dim row = view.GetRow(e.RowHandle)
                selector.SetValue(row)

                If selector.Count > 1 AndAlso (Me.editFlag OrElse Me.ActivityType = 1) Then
                    selector.UnSetValue(row)
                    Mensaje(EeventViewerImages.Advertencia) = $"{If(Me.editFlag, "Cuando se esta editando", "Cuando el tipo de actividad es igual a Otros")}, solo se puede seleccionar 1 CUPS"
                    Exit Sub
                End If
            Else
                selector.Clear()
            End If
            view.RefreshData()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCUPSCrystal.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing
        If searchLookupEdit.Name = "INDSleCUPSCrystal" Then
            searchLookupEdit.Properties.NullText = _selectorCUPS.ToString()
        End If
        Dim result = Await ValidateCUPSWithDescription(_selectorCUPS)
        If result Is Nothing OrElse result.StateResult Then
            Mensaje(EeventViewerImages.Advertencia) = result?.Message
            Me.SelectorClear()
        End If
        Me.INDSleCUPSCrystal.Focus()
    End Sub

#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra("755")
        AllowsAddAuthorizationControl = If(BarraBotones.PermissionsForm.ContainsKey(135), True, False)

        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id

            'parámetos de contratos
            _settingsContractXpo = presenter.GetSettingsContractByOperatingUnitId(_idOperativeUnit)
        End If
    End Sub

#End Region

#Region "Enums"
    Public Enum eOptionClean
        Deshacer
        Nuevo
    End Enum

    Public Enum eClassService
        ConsultaExterna = 1
        Laboratorios = 2
        Imagenes = 3
        Patologias = 4
    End Enum

#End Region

End Class