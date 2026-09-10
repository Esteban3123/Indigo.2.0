'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Ernesto Cordoba
' Created          : 02/10/2014
'
' Last Modified By : Carlos Mario Arias Rubiano
' Last Modified On : 20/11/2014
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Contract.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.Windows.Forms

#End Region

Public Class FrmIPSService
    Implements IIPSService

#Region "TUPLES"
    ''' <summary>
    ''' tipos de manual
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceManual As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListServiceManual As List(Of Tuple(Of Integer, String))
        Get
            If _listServiceManual Is Nothing Then
                _listServiceManual = New List(Of Tuple(Of Integer, String))
                _listServiceManual.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ISS2001", NAME_MODULE)))
                _listServiceManual.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("ISS2004", NAME_MODULE)))
                _listServiceManual.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("SOAT", NAME_MODULE)))
                _listServiceManual.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("Institucional", NAME_MODULE)))
            End If
            Return _listServiceManual
        End Get
    End Property

    ''' <summary>
    ''' Clases de Servicios
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceClass As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListServiceClass As List(Of Tuple(Of Integer, String))
        Get
            If _listServiceClass Is Nothing Then
                _listServiceClass = New List(Of Tuple(Of Integer, String))
                _listServiceClass.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("None", NAME_MODULE)))
                _listServiceClass.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Surgeon", NAME_MODULE)))
                _listServiceClass.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Anesthesiologist", NAME_MODULE)))
                _listServiceClass.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("Assistant", NAME_MODULE)))
                _listServiceClass.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("RightRoom", NAME_MODULE)))
                _listServiceClass.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("SutureMaterials", NAME_MODULE)))
                _listServiceClass.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("SurgicalInstrumentation", NAME_MODULE)))
            End If
            Return _listServiceClass
        End Get
    End Property

    ''' <summary>
    ''' tipos de Servicios
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListServiceType As List(Of Tuple(Of Integer, String))
        Get
            If _listServiceType Is Nothing Then
                _listServiceType = New List(Of Tuple(Of Integer, String))
                _listServiceType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("None", NAME_MODULE)))
                _listServiceType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Diagnosis", NAME_MODULE)))
                _listServiceType.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Therapeutic", NAME_MODULE)))
                _listServiceType.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("SpecifiesProtection", NAME_MODULE)))
                _listServiceType.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("EarlyDetectionSystemicDisease", NAME_MODULE)))
                _listServiceType.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("EarlyDetectionOccupationalDisease", NAME_MODULE)))
            End If
            Return _listServiceType
        End Get
    End Property

    ''' <summary>
    ''' presentacion de servcios
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPresentationProduct As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListPresentationProduct As List(Of Tuple(Of Integer, String))
        Get
            If _listPresentationProduct Is Nothing Then
                _listPresentationProduct = New List(Of Tuple(Of Integer, String))
                _listPresentationProduct.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("NoSurgical", NAME_MODULE)))
                _listPresentationProduct.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Surgical", NAME_MODULE)))
                _listPresentationProduct.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Package", NAME_MODULE)))
            End If
            Return _listPresentationProduct
        End Get
    End Property

    ''' <summary>
    ''' Procedimientos
    ''' </summary>
    ''' <remarks></remarks>
    Private _listProcedure As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListProcedure As List(Of Tuple(Of Integer, String))
        Get
            If _listProcedure Is Nothing Then
                _listProcedure = New List(Of Tuple(Of Integer, String))
                _listProcedure.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Diagnosis", NAME_MODULE)))
                _listProcedure.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Laboratory", NAME_MODULE)))
                _listProcedure.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Dentistry", NAME_MODULE)))
                _listProcedure.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("ConsultationEmergencies", NAME_MODULE)))
                _listProcedure.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("Hospitalization", NAME_MODULE)))
            End If
            Return _listProcedure
        End Get
    End Property

    ''' <summary>
    ''' Nivel de complejidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _listComplexityLevel As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListComplexityLevel As List(Of Tuple(Of Integer, String))
        Get
            If _listComplexityLevel Is Nothing Then
                _listComplexityLevel = New List(Of Tuple(Of Integer, String))
                _listComplexityLevel.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Under", NAME_MODULE)))
                _listComplexityLevel.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Middle", NAME_MODULE)))
                _listComplexityLevel.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("High", NAME_MODULE)))
            End If
            Return _listComplexityLevel
        End Get
    End Property

    ''' <summary>
    ''' Edades
    ''' </summary>
    ''' <remarks></remarks>
    Private _listAge As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListAge As List(Of Tuple(Of Integer, String))
        Get
            If _listAge Is Nothing Then
                _listAge = New List(Of Tuple(Of Integer, String))
                _listAge.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Years", NAME_MODULE)))
                _listAge.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Months", NAME_MODULE)))
                _listAge.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Days", NAME_MODULE)))
            End If
            Return _listAge
        End Get
    End Property

    ''' <summary>
    ''' codigo subatencion
    ''' </summary>
    ''' <remarks></remarks>
    Private _listSubattentionCode As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListSubattentionCode As List(Of Tuple(Of Integer, String))
        Get
            If _listSubattentionCode Is Nothing Then
                _listSubattentionCode = New List(Of Tuple(Of Integer, String))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("None", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("StaySingle", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("SharedRoom", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("AdultUCI", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("NeonatalUCI", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("MediumUCICare", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("Incubator", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(8, ResourceManager.GetString("GeneralMedicalConsultation", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(9, ResourceManager.GetString("ConsultationSpecialist", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(10, ResourceManager.GetString("InterConsultation", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(11, ResourceManager.GetString("HospitalVisits", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(12, ResourceManager.GetString("FeesSurgeons", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(13, ResourceManager.GetString("AnesthesiaFees", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(14, ResourceManager.GetString("FeesAssistantship", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(15, ResourceManager.GetString("FeesInstrumentation", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(16, ResourceManager.GetString("RoomRights", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(17, ResourceManager.GetString("AnesthesiaRight", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(18, ResourceManager.GetString("RightTeam", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(19, ResourceManager.GetString("HospitalSupplies", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(20, ResourceManager.GetString("materials", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(21, ResourceManager.GetString("Drugs", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(22, ResourceManager.GetString("Oxygen", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(23, ResourceManager.GetString("Laboratory", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(24, ResourceManager.GetString("Radiology", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(25, ResourceManager.GetString("Scans", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(26, ResourceManager.GetString("NuclearMedicine", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(27, ResourceManager.GetString("MagneticResonance", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(28, ResourceManager.GetString("ComplementaryTests", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(29, ResourceManager.GetString("VascularTesting", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(30, ResourceManager.GetString("Hemodynamics", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(31, ResourceManager.GetString("BloodBank", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(32, ResourceManager.GetString("Therapies", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(33, ResourceManager.GetString("Ambulance", NAME_MODULE)))
                _listSubattentionCode.Add(New Tuple(Of Integer, String)(34, ResourceManager.GetString("IntegralInvoice", NAME_MODULE)))
            End If
            Return _listSubattentionCode
        End Get
    End Property
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PIPSService
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract
    ''' <summary>
    ''' represnta la entidad de servicios IPS
    ''' </summary>
    ''' <remarks></remarks>
    Private iPSService As IPSService
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private SearchMode As Boolean
    ''' <summary>
    ''' listado de las homologaciones de las entidades CUPS
    ''' </summary>
    ''' <remarks></remarks>
    Private listCupsHomologation As New List(Of CupsHomologation)
    ''' <summary>
    ''' listado de las homologaciones de las entidades CUPS para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private listCupsHomologationDelete As List(Of CupsHomologation)

    ''' <summary>
    ''' representa la entidad de hologacion CUPS
    ''' </summary>
    ''' <remarks></remarks>
    Private cupsHomologation As CupsHomologation

    Private _generalLedgerIva As GeneralLedgerIVA

    Private _mainAccount As MainAccounts

    ''' <summary>
    ''' representa la entidad de procedimientos quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Private surgicalProcedureService As SurgicalProcedureService
    ''' <summary>
    ''' listado de los procedimientos quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Private listSurgicalProcedureService As List(Of SurgicalProcedureService)
    ''' <summary>
    ''' listado de los procedimientos quirurgicos a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Private listSurgicalProcedureServiceDelete As List(Of SurgicalProcedureService)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListOutPatientRecoveryFeeType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Private ListInPatientRecoveryFeeType As New List(Of Tuple(Of Integer, String))

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Obtiene o establece el id del material asociado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociatedMaterialIPSServiceId As Integer? Implements IIPSService.AssociatedMaterialIPSServiceId
        Get
            Return INDsleAssociatedMaterialIPSServiceId.EditValue
        End Get
        Set(value As Integer?)
            INDsleAssociatedMaterialIPSServiceId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del material asociado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AssociatedMaterialIPSServiceIdXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSService.AssociatedMaterialIPSServiceIdXpo
        Get
            Return INDsleAssociatedMaterialIPSServiceId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAssociatedMaterialIPSServiceId.Properties.DataSource = value
        End Set
    End Property

    Public Property ListGeneralLedgerIva As XPInstantFeedbackSource Implements IIPSService.ListGeneralLedgerIva
        Get
            Return INDsleIVACode.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIVACode.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si aplica para > 450 UVR
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ApplyChangeScore As Boolean? Implements IIPSService.ApplyChangeScore
        Get
            Return INDsleApplyChangeScore.EditValue
        End Get
        Set(value As Boolean?)
            INDsleApplyChangeScore.EditValue = value
        End Set
    End Property

    Public Property TaxedProduct As Boolean
        Get
            Return INDrgProduct.EditValue
        End Get
        Set(value As Boolean)
            INDrgProduct.EditValue = value
        End Set
    End Property

    Public Property IdAccountSale As Integer?

    ''' <summary>
    ''' Obtiene o establece el puntaje > 450 UVR
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NewScore As Integer Implements IIPSService.NewScore
        Get
            Return INDtxtNewScore.EditValue
        End Get
        Set(value As Integer)
            INDtxtNewScore.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the billing concept identifier.
    ''' </summary>
    ''' <value>
    ''' The billing concept identifier.
    ''' </value>
    Public Property BillingConceptId As Integer?
        Get
            Return INDSleBillingConcept.EditValue
        End Get
        Set(value As Integer?)
            INDSleBillingConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la liquidacion ambulatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InPatientRecoveryFeeType As Integer? Implements IIPSService.InPatientRecoveryFeeType
        Get
            Return INDsleInPatientRecoveryFeeTypeSurgicalServices.EditValue
        End Get
        Set(value As Integer?)
            INDsleInPatientRecoveryFeeTypeSurgicalServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la liquidacion hospitalaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OutPatientRecoveryFeeType As Integer? Implements IIPSService.OutPatientRecoveryFeeType
        Get
            Return INDsleOutPatientRecoveryFeeTypeSurgicalServices.EditValue
        End Get
        Set(value As Integer?)
            INDsleOutPatientRecoveryFeeTypeSurgicalServices.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el puntaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Score As Integer Implements IIPSService.Score
        Get
            Return INDtxtScore.EditValue
        End Get
        Set(value As Integer)
            INDtxtScore.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor uvr
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UVRNumber As Integer Implements IIPSService.UVRNumber
        Get
            Return INDtxtUVR.EditValue
        End Get
        Set(value As Integer)
            INDtxtUVR.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo quirurgico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalGroupId As Integer? Implements IIPSService.SurgicalGroupId
        Get
            Return INDsleSurgicalGroup.EditValue
        End Get
        Set(value As Integer?)
            INDsleSurgicalGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del grupo quirurgico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSService.SurgicalGroupXpo
        Get
            Return INDsleSurgicalGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSurgicalGroup.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public WriteOnly Property ActionsOnControls As Boolean Implements IIPSService.ActionsOnControls
        Set(value As Boolean)
            INDLcIPSService.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDBtnCode.Enabled = Not value
            INDTxtServiceName.Enabled = value
            INDGleServiceManual.Enabled = value
            INDGlePresentation.Enabled = value
            INDrgProduct.Enabled = value
            INDsleIVACode.Enabled = value
            INDsleSalesAccount.Enabled = value
            INDGleServiceClass.Enabled = value
            INDGleServiceType.Enabled = value
            INDSeAuthorizationLevel.Enabled = value
            INDGleSubattentionCode.Enabled = value
            INDGleProcedure.Enabled = value
            INDSeContributionsWeeks.Enabled = value
            INDSeContributionsWeeks.Enabled = value
            INDsleInPatientRecoveryFeeTypeSurgicalServices.Enabled = value
            INDsleOutPatientRecoveryFeeTypeSurgicalServices.Enabled = value
            INDGleSurgeryArtroscopica.Enabled = value
            INDGlePathologyService.Enabled = value
            INDGlePromotionAndPrevention.Enabled = value
            INDPceSurgicalProcedure.Enabled = value
            INDGcSurgicalProcedure.Enabled = value
            INDGleMinimunAgeUnit.Enabled = value
            INDSeMinimunAge.Enabled = value
            INDGleMaximumAgeUnit.Enabled = value
            INDSeMaximumAge.Enabled = value
            INDGleInMale.Enabled = value
            INDGleInFemale.Enabled = value
            INDGlePOS.Enabled = value
            INDGleComplexityLevel.Enabled = value
            INDGleComplexityLevel.Enabled = value
            INDGleChildbirthAbortion.Enabled = value
            INDMePromotionAndPreventionActivities.Enabled = value
            INDMePromotionAndPreventionActivities.Enabled = value
            INDPceHomologation.Enabled = value
            INDGcHomologation.Enabled = value
            INDGcHomologation.Enabled = value
            INDLcIPSService.EndUpdate()
            If INDBtnCode.Enabled = True Then
                INDBtnCode.Focus()
            Else
                INDTxtServiceName.Focus()
            End If
        End Set
    End Property
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IIPSService.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IIPSService.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IIPSService.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Codigo del servicio IPS
    ''' </summary>
    Public Property Code As String Implements IIPSService.Code
        Get
            Return INDBtnCode.Text
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del servicio IPS
    ''' </summary>
    Public Property ServiceName As String Implements IIPSService.ServiceName
        Get
            Return INDTxtServiceName.Text
        End Get
        Set(value As String)
            INDTxtServiceName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de manual 1 - ISS 2001 , 2 - ISS 2004 , 3 - SOAT
    ''' </summary>
    Public Property ServiceManual As Integer? Implements IIPSService.ServiceManual
        Get
            Return INDGleServiceManual.EditValue
        End Get
        Set(value As Integer?)
            INDGleServiceManual.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Presentacion del producto 1 - No quirurgico , 2 - Quirurgico , 3 - Paquete
    ''' </summary>
    Public Property PresentationProduct As Integer? Implements IIPSService.PresentationProduct
        Get
            Return INDGlePresentation.EditValue
        End Get
        Set(value As Integer?)
            INDGlePresentation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Clase de servicio 1 - Ninguno , 2 - Cirujano , 3 - Anesteciologo , 4 - Ayudante , 5 - Derecho Sala , 6 - Materiales Sutura , 7 - Instrumentacion Quirurgica
    ''' </summary>
    ''' 
    Public Property ServiceClass As Integer? Implements IIPSService.ServiceClass
        Get
            Return INDGleServiceClass.EditValue
        End Get
        Set(value As Integer?)
            INDGleServiceClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de servicio 1 - Ninguno , 2 - Diagnostico , 3  -Terapeutico , 4 - Proteccion Especifica , 5 - Deteccion temprana enfermedad general , 6 - Deteccion temprana enfermedad profesional
    ''' </summary>
    Public Property ServiceType As Integer? Implements IIPSService.ServiceType
        Get
            Return INDGleServiceType.EditValue
        End Get
        Set(value As Integer?)
            INDGleServiceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el nivel de autorizacion en rango es de uno a nueve
    ''' </summary>
    Public Property AuthorizationLevel As Integer Implements IIPSService.AuthorizationLevel
        Get
            Return INDSeAuthorizationLevel.EditValue
        End Get
        Set(value As Integer)
            INDSeAuthorizationLevel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Codigo subatencion 1 - Ninguno,2 - Estancia Individual,3 - Habitacion compartida,4 - UCI Adultos,5 - UCI Neonatal,6 - UCI Cuidados Medianos,7 - Incubadora,8 - Consulta Medica General,9 - Consulta pecialista,10 - Interconsulta,11 - Visitas Hospitalarias,12 - Honorarios Cirujanos,13 - Honorarios Anestesia,14 - Honorarios Ayudantia,15 - Honorarios Instrumentacion,16 - Derechos Sala,17 - Derecho Anestesia,18 - Derecho Equipo,19 - Insumos Hospitalarios,20 - Material Quirurgico,21 - Medicamentos,22 - Oxigeno,23 - Laboratorio,24 - Radiologia,25 - Tomografias,26 - Medicina Nuclear,27 - Resonancia Magnetica,28 - Examenes Complementarios,29 - Examenes Vasculares,30 - Hemodinamia,31 - Banco Sangre,32 - Terapias,33 - Ambulancia,34 - Factura Integral
    ''' </summary>
    Public Property SubattentionCode As Integer? Implements IIPSService.SubattentionCode
        Get
            Return INDGleSubattentionCode.EditValue
        End Get
        Set(value As Integer?)
            INDGleSubattentionCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Procedimiento del servicio 1 - Diagnostico , 2 - Laboratorio , 3 - Odontologia , 4 - Consulta - Urgencias , 5 - Hospitalizacion
    ''' </summary>
    Public Property Procedure As Integer? Implements IIPSService.Procedure
        Get
            Return INDGleProcedure.EditValue
        End Get
        Set(value As Integer?)
            INDGleProcedure.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Semanas cotizadas para acceder al servicio
    ''' </summary>
    Public Property ContributionsWeeks As Integer Implements IIPSService.ContributionsWeeks
        Get
            Return INDSeContributionsWeeks.EditValue
        End Get
        Set(value As Integer)
            INDSeContributionsWeeks.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece que es una cirugia artroscopica, solo puede ser artroscopica si la presentacion es quirurgica o paquete
    ''' </summary>
    Public Property SurgeryArtroscopica As Boolean? Implements IIPSService.SurgeryArtroscopica
        Get
            Return INDGleSurgeryArtroscopica.EditValue
        End Get
        Set(value As Boolean?)
            INDGleSurgeryArtroscopica.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece si es un servicio de patologia, solo puese ser patologica si el procedimiento es Laboratorio
    ''' </summary>
    Public Property PathologyService As Boolean? Implements IIPSService.PathologyService
        Get
            Return INDGlePathologyService.EditValue
        End Get
        Set(value As Boolean?)
            INDGlePathologyService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece si es de promocion y prevención
    ''' </summary>
    Public Property PromotionAndPrevention As Boolean? Implements IIPSService.PromotionAndPrevention
        Get
            Return INDGlePromotionAndPrevention.EditValue
        End Get
        Set(value As Boolean?)
            INDGlePromotionAndPrevention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de medida para la edad minima 1 - Años,2 - Meses,3 - Dias
    ''' </summary> 
    Public Property MinimunAgeUnit As Integer? Implements IIPSService.MinimunAgeUnit
        Get
            Return INDGleMinimunAgeUnit.EditValue
        End Get
        Set(value As Integer?)
            INDGleMinimunAgeUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Edad minima para este servicio
    ''' </summary>
    Public Property MinimunAge As Integer Implements IIPSService.MinimunAge
        Get
            Return INDSeMinimunAge.EditValue
        End Get
        Set(value As Integer)
            INDSeMinimunAge.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de medida para la edad maxima 1 - Años,2 - Meses,3 - Dias
    ''' </summary>
    Public Property MaximumAgeUnit As Integer? Implements IIPSService.MaximumAgeUnit
        Get
            Return INDGleMaximumAgeUnit.EditValue
        End Get
        Set(value As Integer?)
            INDGleMaximumAgeUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Edad Maxima para acceder al servicio
    ''' </summary>
    Public Property MaximumAge As Integer Implements IIPSService.MaximumAge
        Get
            Return INDSeMaximumAge.EditValue
        End Get
        Set(value As Integer)
            INDSeMaximumAge.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' campo para identificar si se aplica al sexo masculino
    ''' </summary>
    Public Property InMale As Boolean? Implements IIPSService.InMale
        Get
            Return INDGleInMale.EditValue
        End Get
        Set(value As Boolean?)
            INDGleInMale.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' campo para identificar si se aplica al sexo femenino
    ''' </summary>
    Public Property InFemale As Boolean? Implements IIPSService.InFemale
        Get
            Return INDGleInFemale.EditValue
        End Get
        Set(value As Boolean?)
            INDGleInFemale.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si el servicio petenece al POS
    ''' </summary>
    Public Property POS As Boolean? Implements IIPSService.POS
        Get
            Return INDGlePOS.EditValue
        End Get
        Set(value As Boolean?)
            INDGlePOS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el nivel de complejidad 1 - Baja,2 - Media,3 - Alta
    ''' </summary>
    Public Property ComplexityLevel As Integer? Implements IIPSService.ComplexityLevel
        Get
            Return INDGleComplexityLevel.EditValue
        End Get
        Set(value As Integer?)
            INDGleComplexityLevel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si el servicio corresponde a un parto o aun aborto
    ''' </summary>
    Public Property ChildbirthAbortion As Boolean? Implements IIPSService.ChildbirthAbortion
        Get
            Return INDGleChildbirthAbortion.EditValue
        End Get
        Set(value As Boolean?)
            INDGleChildbirthAbortion.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Actividades del servicio de promocion y prevencion
    ''' </summary>
    Public Property PromotionAndPreventionActivities As String Implements IIPSService.PromotionAndPreventionActivities
        Get
            Return INDMePromotionAndPreventionActivities.Text
        End Get
        Set(value As String)
            INDMePromotionAndPreventionActivities.Text = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de entidades CUPS
    ''' </summary>
    Public Property CupsEntityXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSService.CupsEntityXPO
        Get
            Return CType(INDSleCodeHomologation.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCodeHomologation.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de servicios IPS
    ''' </summary>
    Public Property ServicesIPSXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements IIPSService.ServicesIPSXPO
        Get
            Return CType(INDSleSurgicalProcedureCode.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleSurgicalProcedureCode.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "CRUD"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControlsPopupHomologation()
        CleanControlsPopupSurgicalProcedure()
        CleanControls()

        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If iPSService IsNot Nothing AndAlso iPSService.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MIPSService(MyTag)
                    AsyncLoader(True)
                    AssigningValuesDelete()
                    Dim result = Await Model.DeleteIPSService(iPSService)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        SearchMode = False
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Dim errors = ValidateFields()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If listSurgicalProcedureService IsNot Nothing AndAlso listSurgicalProcedureService.Count > 0 Then
            Dim errorsList = ValidateDefaultServiceList()
            If errorsList.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsList
                Exit Sub
            End If
        End If
        AssigningValues()
        Using model As New MIPSService(MyTag)
            AsyncLoader(True)
            Dim Result = Await model.SaveIPSService(iPSService, 0)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If iPSService.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                Else
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.iPSService = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
                Me.Deshacer()
            Else
                Dim message = Result?.MessageResult?.FirstOrDefault()
                If (String.IsNullOrEmpty(message)) Then
                    message = Result?.Message
                ElseIf (message = ErrorConcurrencia) Then
                    message = ResourceManager.GetString("ErrorConcurrence")
                End If
                Mensaje(EeventViewerImages.MensajeError) = message
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        ValidateCode()
    End Sub

#End Region

#Region "METHODS"

    ''' <summary>
    ''' Metodo que muestra u oculta los controles de Liquidacion Ingresos Hospitalarios y Liquidacion Ingresos Ambulatorios segun corresponda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsLiquidation()
        If PresentationProduct IsNot Nothing AndAlso ServiceClass IsNot Nothing Then
            If PresentationProduct = 1 AndAlso ServiceClass > 1 Then
                INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.HideControl()
                INDsleOutPatientRecoveryFeeTypeSurgicalServices.EditValue = Nothing
                INDlyItemInPatientRecoveryFeeTypeSurgicalServices.HideControl()
                INDsleInPatientRecoveryFeeTypeSurgicalServices.EditValue = Nothing
            Else
                INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.HideControl(False)
                INDlyItemInPatientRecoveryFeeTypeSurgicalServices.HideControl(False)
            End If
        Else
            If PresentationProduct IsNot Nothing Then
                INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.HideControl(False)
                INDlyItemInPatientRecoveryFeeTypeSurgicalServices.HideControl(False)
            Else
                INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.HideControl()
                INDlyItemInPatientRecoveryFeeTypeSurgicalServices.HideControl()
                INDsleOutPatientRecoveryFeeTypeSurgicalServices.EditValue = Nothing
                INDsleInPatientRecoveryFeeTypeSurgicalServices.EditValue = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListOutPatientRecoveryFeeType = New List(Of Tuple(Of Integer, String))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(2, "Cuota Moderadora"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(3, "Copago"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(4, "Bono"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(5, "Franquicia"))
        ListOutPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(6, "Otra"))
        INDsleOutPatientRecoveryFeeTypeSurgicalServices.Properties.DataSource = ListOutPatientRecoveryFeeType.ToList

        ListInPatientRecoveryFeeType = New List(Of Tuple(Of Integer, String))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(1, "Ninguna"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(2, "Cuota Moderadora"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(3, "Copago"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(4, "Bono"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(5, "Franquicia"))
        ListInPatientRecoveryFeeType.Add(New Tuple(Of Integer, String)(6, "Otra"))
        INDsleInPatientRecoveryFeeTypeSurgicalServices.Properties.DataSource = ListInPatientRecoveryFeeType.ToList
    End Sub

    ''' <summary>
    ''' Muestra u oculta el control de puntaje
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlScore()
        If ServiceManual IsNot Nothing AndAlso ServiceClass IsNot Nothing AndAlso PresentationProduct IsNot Nothing Then
            If (ServiceManual = 1 OrElse ServiceManual = 2) AndAlso (ServiceClass = 2 OrElse ServiceClass = 3 OrElse ServiceClass = 4) AndAlso PresentationProduct = 1 Then
                INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemScore.AllowHide = False
                INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemApplyChangeScore.AllowHide = False
            ElseIf (ServiceManual = 1 OrElse ServiceManual = 2) AndAlso ServiceClass = 5 AndAlso PresentationProduct = 1 Then
                INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemScore.AllowHide = True
                INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemApplyChangeScore.AllowHide = False
            ElseIf ServiceManual = 4 AndAlso {1, 3}.Contains(PresentationProduct) AndAlso ServiceClass = 1 Then
                INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemScore.AllowHide = True
                INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemApplyChangeScore.AllowHide = True
                ApplyChangeScore = False
            ElseIf ServiceManual = 4 AndAlso {1, 3}.Contains(PresentationProduct) AndAlso ServiceClass = 5 Then
                INDlyItemAssociatedMaterialIPSServiceId.HideControl()
                INDlyItemScore.HideControl()
                INDlyItemScore.AllowHide = True
                INDlyItemApplyChangeScore.HideControl()
                INDlyItemApplyChangeScore.AllowHide = True
                ApplyChangeScore = False
                INDlyItemNewScore.HideControl()
                INDlyItemNewScore.AllowHide = True
            ElseIf ServiceManual = 4 AndAlso {1, 3}.Contains(PresentationProduct) AndAlso {2, 3, 4, 6, 7}.Contains(ServiceClass) Then
                INDlyItemScore.HideControl()
                INDlyItemScore.AllowHide = True
                INDlyItemApplyChangeScore.HideControl()
                INDlyItemApplyChangeScore.AllowHide = True
                ApplyChangeScore = False
                INDlyItemNewScore.HideControl()
                INDlyItemNewScore.AllowHide = True
            Else
                INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemScore.AllowHide = True
                INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemApplyChangeScore.AllowHide = True
                ApplyChangeScore = False
            End If
        Else
            INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemScore.AllowHide = True
            INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemApplyChangeScore.AllowHide = True
            ApplyChangeScore = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que valida si el listado de tipo quirurgico tiene el valor por defecto
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDefaultServiceList() As String
        Dim errors As New StringBuilder
        Dim listCompare = (From item In listSurgicalProcedureService Select item.ClassService).Distinct
        For i = 0 To listCompare.Count - 1
            Dim cont As Integer = listSurgicalProcedureService.FindAll(Function(item) item.ClassService = listCompare.ElementAt(i) AndAlso item.DefaultService = True).ToList().Count
            If cont = 0 And ServiceManual <> 4 Then
                errors.AppendLine("La clase " + listCompare.ElementAt(i) + " del Servicio de Procedimiento Quirurgico no tiene valor por defecto.")
            End If
        Next
        Return errors.ToString
    End Function

    ''' <summary>
    ''' metodo para limpiar controles del popup de procedimiento quirurgico
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupSurgicalProcedure()
        INDSleSurgicalProcedureCode.EditValue = Nothing
        INDTxtSurgicalProcedureName.Text = String.Empty
        INDTxtClass.Text = String.Empty
        INDtxtAssociated.Text = String.Empty
        INDSeAmount.EditValue = 1
        INDBtnAddSurgicalProcedure.Enabled = False
        INDsleDefaultService.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpioar los controles del popup de homologacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopupHomologation()
        INDSleCodeHomologation.EditValue = Nothing
        INDTxtHomologationName.Text = String.Empty
        INDBtnAddHomologation.Enabled = False
    End Sub

    ''' <summary>
    ''' metodo para limpiar controels del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.CleanAuditBasic()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        ActionsOnControls = False
        Code = String.Empty
        ServiceName = String.Empty
        ServiceManual = Nothing
        ServiceClass = Nothing
        AssociatedMaterialIPSServiceId = Nothing
        INDsleAssociatedMaterialIPSServiceId.Properties.NullText = String.Empty
        ServiceType = Nothing
        PresentationProduct = Nothing
        ApplyChangeScore = Nothing
        NewScore = Nothing
        SurgicalGroupId = Nothing
        InPatientRecoveryFeeType = Nothing
        OutPatientRecoveryFeeType = Nothing
        INDsleSurgicalGroup.Properties.NullText = String.Empty
        UVRNumber = Nothing
        INDGlePresentation.Properties.ReadOnly = False
        INDGlePresentation.Properties.Buttons(0).Enabled = True
        TaxedProduct = 0
        IdAccountSale = Nothing
        INDsleSalesAccount.EditValue = Nothing
        INDsleIVACode.EditValue = Nothing
        INDsleSalesAccount.Properties.NullText = String.Empty
        INDsleIVACode.Properties.NullText = String.Empty
        AuthorizationLevel = 1
        ContributionsWeeks = 0
        Procedure = Nothing
        SubattentionCode = Nothing
        MinimunAgeUnit = Nothing
        MinimunAge = 0
        MaximumAgeUnit = Nothing
        MaximumAge = 0
        InMale = Nothing
        InFemale = Nothing
        ChildbirthAbortion = Nothing
        POS = Nothing
        ComplexityLevel = Nothing
        PromotionAndPrevention = Nothing
        PromotionAndPreventionActivities = String.Empty
        SurgeryArtroscopica = Nothing
        PathologyService = Nothing
        iPSService = Nothing
        INDLcgSurgicalProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciServiceClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciServiceClass.AllowHide = True
        INDLciSurgeryArtroscopica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciSurgeryArtroscopica.AllowHide = True
        INDLciPathologyService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciPathologyService.AllowHide = True
        INDLciPromotionAndPreventionActivities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciPromotionAndPreventionActivities.AllowHide = True
        INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSurgicalGroup.AllowHide = True
        INDlyItemUVR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemUVR.AllowHide = True
        INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemScore.AllowHide = True
        INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemApplyChangeScore.AllowHide = True
        INDlyItemNewScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemNewScore.AllowHide = True
        INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.AllowHide = True
        INDlyItemInPatientRecoveryFeeTypeSurgicalServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemInPatientRecoveryFeeTypeSurgicalServices.AllowHide = True
        INDlyItemAssociatedMaterialIPSServiceId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDLciBillingConcept.HideControl()
        INDSleBillingConcept.EditValue = Nothing
        INDSleBillingConcept.Properties.NullText = String.Empty

        INDGcHomologation.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcHomologation)
        INDGcSurgicalProcedure.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcSurgicalProcedure)
        listCupsHomologation = New List(Of CupsHomologation)()
        listSurgicalProcedureService = Nothing
        listCupsHomologationDelete = Nothing
        listSurgicalProcedureServiceDelete = Nothing
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ChangeState()
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MIPSService(MyTag)
                AsyncLoader(True)
                Dim state As Boolean = Not iPSService.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    iPSService = Result.ObjectEmbbeded
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        SearchMode = True
        DeleteBlockedRecord()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listItemsColumnEditServiceManual As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditServiceManual.Add(New Tuple(Of String, Byte)("ISS 2001", 1))
        listItemsColumnEditServiceManual.Add(New Tuple(Of String, Byte)("ISS 2004", 2))
        listItemsColumnEditServiceManual.Add(New Tuple(Of String, Byte)("SOAT", 3))

        Dim listItemsColumnEditPresentation As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditPresentation.Add(New Tuple(Of String, Byte)("No Quirúrgico", 1))
        listItemsColumnEditPresentation.Add(New Tuple(Of String, Byte)("Quirúrgico", 2))
        listItemsColumnEditPresentation.Add(New Tuple(Of String, Byte)("Paquete", 3))

        Dim listItemsColumnEditServiceClass As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Ninguno", 1))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Cirujano", 2))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Anestesiologo", 3))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Ayudante", 4))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Derecho Sala", 5))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Materiales Sutura", 6))
        listItemsColumnEditServiceClass.Add(New Tuple(Of String, Byte)("Instrumentacion Quirurgica", 7))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100}, _
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = 300}, _
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "ServiceManual", .ColumnWidth = 100, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditServiceManual}, _
                              New ColumnInfo() With {.Caption = "Presentación", .FieldName = "Presentation", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditPresentation}, _
                              New ColumnInfo() With {.Caption = "Clase", .FieldName = "ServiceClass", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditServiceClass}, _
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 100}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListIPSServices
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
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequense(MyTag)
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.iPSService.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordContract With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.iPSService.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.iPSService.Code, Me.iPSService.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.iPSService.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.iPSService.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.iPSService.Code, Me.iPSService.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.iPSService.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' metodo para establecer los datasources de los combos 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSources()
        INDGleServiceManual.Properties.DataSource = ListServiceManual
        INDGlePresentation.Properties.DataSource = ListPresentationProduct
        INDGleServiceClass.Properties.DataSource = ListServiceClass
        INDGleServiceType.Properties.DataSource = ListServiceType
        INDGleSubattentionCode.Properties.DataSource = ListSubattentionCode
        INDGleProcedure.Properties.DataSource = ListProcedure
        INDGleMinimunAgeUnit.Properties.DataSource = ListAge
        INDGleMaximumAgeUnit.Properties.DataSource = ListAge
        INDGleComplexityLevel.Properties.DataSource = ListComplexityLevel
    End Sub

    ''' <summary>
    ''' Valida el codigo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ValidateCode()
        If INDBtnCode.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty", NAME_MODULE)
            INDBtnCode.Focus()
        Else
            Await Me.LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' metodfo para cargar controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MIPSService(MyTag)
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetIPSService(INDBtnCode.Text.Trim)
            AsyncLoader(False)
            iPSService = resultOperation.ObjectEmbbeded
            If iPSService IsNot Nothing AndAlso iPSService.Id > 0 Then
                listCupsHomologation = Model.GetCupsHomologationByIPSServiceId(iPSService.Id)
                INDGcHomologation.DataSource = Nothing
                INDGcHomologation.DataSource = listCupsHomologation
                listSurgicalProcedureService = Model.GetSurgicalProcedureServiceByIPSServiceId(iPSService.Id)
                INDGcSurgicalProcedure.DataSource = Nothing
                INDGcSurgicalProcedure.DataSource = listSurgicalProcedureService
                INDGlePresentation.Properties.ReadOnly = True
                INDGlePresentation.Properties.Buttons(0).Enabled = False
                With iPSService
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Code = .Code
                    ServiceName = .Name
                    ServiceManual = .ServiceManual
                    ServiceClass = .ServiceClass
                    TaxedProduct = .TaxedProduct
                    If TaxedProduct Then
                        INDsleIVACode.EditValue = .IVAId
                        IdAccountSale = .SalesLedgerAccountId
                    End If
                    AssociatedMaterialIPSServiceId = .AssociatedMaterialIPSServiceId
                    INDsleAssociatedMaterialIPSServiceId.Properties.NullText = .AssociatedMaterialIPSServiceDescription

                    ServiceType = .ServiceType
                    PresentationProduct = .Presentation
                    SurgicalGroupId = .SurgicalGroupId
                    INDsleSurgicalGroup.Properties.NullText = .CodeNameSurgicalGroup
                    If .UVRNumber = Nothing Then
                        UVRNumber = 0
                    Else
                        UVRNumber = .UVRNumber
                    End If
                    Score = .Score
                    ApplyChangeScore = .ApplyChangeScore
                    NewScore = .NewScore
                    AuthorizationLevel = .AuthorizationLevel
                    ContributionsWeeks = .ContributionsWeeks
                    Procedure = .Procedure
                    SubattentionCode = .SubattentionCode
                    MinimunAgeUnit = .MinimunAgeUnit
                    MinimunAge = .MinimunAge
                    MaximumAgeUnit = .MaximumAgeUnit
                    MaximumAge = .MaximumAge
                    InMale = .InMale
                    InFemale = .InFemale
                    ChildbirthAbortion = .ChildbirthAbortion
                    POS = .POS
                    ComplexityLevel = .ComplexityLevel
                    PromotionAndPrevention = .PromotionAndPrevention
                    PromotionAndPreventionActivities = .PromotionAndPreventionActivities
                    SurgeryArtroscopica = .SurgeryArtroscopica
                    PathologyService = .PathologyService
                    InPatientRecoveryFeeType = .InPatientRecoveryFeeType
                    OutPatientRecoveryFeeType = .OutPatientRecoveryFeeType
                    BillingConceptId = .BillingConceptId
                    INDSleBillingConcept.Properties.NullText = .CodeNameBillingConcept
                    Status = .Status
                End With
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = False
                BarraBotones.SetDocuments(iPSService.Id)
                ActionsOnControls = True
                INDTxtServiceName.Focus()
                Me.GetDocumentIndexed(MyTag & "_" & iPSService.Code)
                GenerateBlockRecord()
            Else
                CreateNew()
            End If
        End Using
    End Function

    ''' <summary>
    ''' prepara el formulario para crear un servicio IPS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateNew()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'Status = True
        iPSService = New IPSService With {.Status = True}
        iPSService.Status = True
        ActionsOnControls = True
        INDTxtServiceName.Focus()
    End Sub

    ''' <summary>
    ''' metodo para validar controles antes de gurardad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As String
        Dim result As New StringBuilder

        If ServiceName = String.Empty Then
            result.AppendLine(INDLciServiceName.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If ServiceManual Is Nothing Then
            result.AppendLine(INDLciServiceManual.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If PresentationProduct Is Nothing Then
            result.AppendLine(INDLciPresentation.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDLciServiceClass.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ServiceClass Is Nothing Then
                result.AppendLine(INDLciServiceClass.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If ServiceType Is Nothing Then
            result.AppendLine(INDLciServiceType.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If SubattentionCode Is Nothing Then
            result.AppendLine(INDLciSubattentionCode.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If TaxedProduct And INDsleIVACode.EditValue Is Nothing Then
            result.AppendLine("Código IVA vacío")
        End If
        If Procedure Is Nothing Then
            result.AppendLine(INDLciProcedure.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If Procedure = 2 And PathologyService Is Nothing Then
            result.AppendLine("Patología vacío")
        End If
        If PromotionAndPrevention Is Nothing Then
            result.AppendLine(INDLciPromotionAndPrevention.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDlyItemInPatientRecoveryFeeTypeSurgicalServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If InPatientRecoveryFeeType Is Nothing Then
                result.AppendLine(INDlyItemInPatientRecoveryFeeTypeSurgicalServices.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If OutPatientRecoveryFeeType Is Nothing Then
                result.AppendLine(INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDLcgSurgicalProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDGvSurgicalProcedure.RowCount = 0 Then
                result.AppendLine(ResourceManager.GetString("AddService", NAME_MODULE))
            End If
        End If
        If MinimunAgeUnit Is Nothing Then
            result.AppendLine(INDLciMinimunAgeUnit.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If MaximumAgeUnit Is Nothing Then
            result.AppendLine(INDLciMaximumAgeUnit.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        Dim age = ValidateAge()
        If age.Length > 0 Then
            result.AppendLine(age)
        End If
        If InMale Is Nothing Then
            result.AppendLine(INDLciInMale.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If InFemale Is Nothing Then
            result.AppendLine(INDLciInFemale.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If POS Is Nothing Then
            result.AppendLine(INDLciPOS.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If ComplexityLevel Is Nothing Then
            result.AppendLine(INDLciComplexityLevel.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If ChildbirthAbortion Is Nothing Then
            result.AppendLine(INDLciChildbirthAbortion.CustomizationFormText + ResourceManager.GetString("Empty"))
        End If
        If INDLciPromotionAndPreventionActivities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If PromotionAndPreventionActivities Is String.Empty Then
                result.AppendLine(INDLciPromotionAndPreventionActivities.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ApplyChangeScore Is Nothing Then
                result.AppendLine(INDlyItemApplyChangeScore.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If
        If INDlyItemNewScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If NewScore = Nothing OrElse NewScore = 0 Then
                result.AppendLine(INDlyItemNewScore.CustomizationFormText + ResourceManager.GetString("Empty"))
            End If
        End If

        If INDLcgHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDGvHomologation.RowCount = 0 Then
                result.AppendLine(ResourceManager.GetString("AddHomologation", NAME_MODULE))
            End If
        End If
        Return result.ToString()
    End Function

    ''' <summary>
    ''' metodo para validar las edades minimas y maximas convirtiendolas a dias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateAge() As String
        Dim errors As New StringBuilder
        Dim minimunAgeTmp As Integer = 0
        Dim maximunAgeTmp As Integer = 0
        Select Case MinimunAgeUnit
            Case 1
                minimunAgeTmp = MinimunAge * 360
            Case 2
                minimunAgeTmp = MinimunAge * 30
            Case 3
                minimunAgeTmp = MinimunAge
        End Select
        Select Case MaximumAgeUnit
            Case 1
                maximunAgeTmp = MaximumAge * 360
            Case 2
                maximunAgeTmp = MaximumAge * 30
            Case 3
                maximunAgeTmp = MaximumAge
        End Select
        If minimunAgeTmp >= maximunAgeTmp Then
            errors.AppendLine(ResourceManager.GetString("Age", NAME_MODULE))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para signar valores que se van a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With iPSService
            .Code = Code
            .Name = ServiceName
            .ServiceManual = ServiceManual
            .ServiceClass = ServiceClass
            .TaxedProduct = TaxedProduct
            .IVAId = INDsleIVACode.EditValue
            .SalesLedgerAccountId = IdAccountSale

            If INDlyItemAssociatedMaterialIPSServiceId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AssociatedMaterialIPSServiceId = AssociatedMaterialIPSServiceId
            Else
                .AssociatedMaterialIPSServiceId = Nothing
            End If

            .ServiceType = ServiceType
            .Presentation = PresentationProduct

            If INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SurgicalGroupId = SurgicalGroupId
            Else
                .SurgicalGroupId = Nothing
            End If

            If INDlyItemUVR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .UVRNumber = UVRNumber
            Else
                .UVRNumber = Nothing
            End If

            If INDlyItemScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Score = Score
            Else
                .Score = 0
            End If

            If INDlyItemApplyChangeScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ApplyChangeScore = ApplyChangeScore
            Else
                .ApplyChangeScore = False
            End If

            If INDlyItemNewScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .NewScore = NewScore
            Else
                .NewScore = 0
            End If

            .AuthorizationLevel = AuthorizationLevel
            If ContributionsWeeks < 0 Then
                ContributionsWeeks *= -1
            End If
            .ContributionsWeeks = ContributionsWeeks
            .Procedure = Procedure
            .SubattentionCode = SubattentionCode
            .MinimunAgeUnit = MinimunAgeUnit
            If MinimunAge < 0 Then
                MinimunAge *= -1
            End If
            .MinimunAge = MinimunAge
            .MaximumAgeUnit = MaximumAgeUnit
            If MaximumAge < 0 Then
                MaximumAge *= -1
            End If
            .MaximumAge = MaximumAge
            .InMale = InMale
            .InFemale = InFemale
            .ChildbirthAbortion = ChildbirthAbortion
            .POS = POS
            .ComplexityLevel = ComplexityLevel
            .PromotionAndPrevention = PromotionAndPrevention
            .PromotionAndPreventionActivities = PromotionAndPreventionActivities
            .BillingConceptId = BillingConceptId
            If INDLciSurgeryArtroscopica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SurgeryArtroscopica = SurgeryArtroscopica
            Else
                .SurgeryArtroscopica = False
            End If
            If INDLciPathologyService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PathologyService = PathologyService
            Else
                .PathologyService = False
            End If

            If INDlyItemInPatientRecoveryFeeTypeSurgicalServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .InPatientRecoveryFeeType = InPatientRecoveryFeeType
            Else
                .InPatientRecoveryFeeType = 1
            End If
            If INDlyItemOutPatientRecoveryFeeTypeSurgicalServices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .OutPatientRecoveryFeeType = OutPatientRecoveryFeeType
            Else
                .OutPatientRecoveryFeeType = 1
            End If

            If INDLcgHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                For Each item In listCupsHomologation
                    .CupsHomologation.Add(item)
                Next
                If listCupsHomologationDelete IsNot Nothing AndAlso listCupsHomologationDelete.Count > 0 Then
                    For Each item In listCupsHomologationDelete
                        .CupsHomologation.Add(item.MarkAsDeleted())
                    Next
                End If
            Else
                If iPSService.Id > 0 Then
                    If listCupsHomologation IsNot Nothing AndAlso listCupsHomologation.Count > 0 Then
                        For Each item In listCupsHomologation
                            .CupsHomologation.Add(item.MarkAsDeleted())
                        Next
                    End If
                    If listCupsHomologationDelete IsNot Nothing AndAlso listCupsHomologationDelete.Count > 0 Then
                        For Each item In listCupsHomologationDelete
                            .CupsHomologation.Add(item.MarkAsDeleted())
                        Next
                    End If
                End If
            End If

            If INDLcgSurgicalProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                For Each item In listSurgicalProcedureService
                    .SurgicalProcedureService.Add(item)
                Next
                If listSurgicalProcedureServiceDelete IsNot Nothing AndAlso listSurgicalProcedureServiceDelete.Count > 0 Then
                    For Each item In listSurgicalProcedureServiceDelete
                        .SurgicalProcedureService.Add(item.MarkAsDeleted())
                    Next
                    .listSurgicalProcedureServiceDelete = listSurgicalProcedureServiceDelete
                End If
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodos para asignar valores cuando se va a eliminar el servicio IPS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValuesDelete()
        With iPSService
            .MarkAsDeleted()
            For Each item In listCupsHomologation
                .CupsHomologation.Add(item.MarkAsDeleted())
            Next
            If listCupsHomologationDelete IsNot Nothing AndAlso listCupsHomologationDelete.Count > 0 Then
                For Each item In listCupsHomologationDelete
                    .CupsHomologation.Add(item.MarkAsDeleted())
                Next
            End If
            If INDLcgSurgicalProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                For Each item In listSurgicalProcedureService
                    .SurgicalProcedureService.Add(item.MarkAsDeleted())
                Next
                If listSurgicalProcedureServiceDelete IsNot Nothing AndAlso listSurgicalProcedureServiceDelete.Count > 0 Then
                    For Each item In listSurgicalProcedureService
                        .SurgicalProcedureService.Add(item.MarkAsDeleted())
                    Next
                End If
            End If
        End With
    End Sub
#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        record = Nothing
        iPSService = Nothing
        SearchMode = Nothing
        listCupsHomologation = Nothing
        listCupsHomologationDelete = Nothing
        cupsHomologation = Nothing
        surgicalProcedureService = Nothing
        listSurgicalProcedureService = Nothing
        listSurgicalProcedureServiceDelete = Nothing
        ListOutPatientRecoveryFeeType = Nothing
        ListInPatientRecoveryFeeType = Nothing
    End Sub

    Private Sub FrmIPSService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridView1.SetListAcction(INDGvHomologation, {eAcciones.Remove}.ToList)
        IndigoGridView2.SetListAcction(INDGvSurgicalProcedure, {eAcciones.Remove}.ToList)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvHomologation.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvSurgicalProcedure.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Me.LayoutControls.SetIsCustomizable(Me.INDLcIPSService, True)
        IndigoGridControl1.RefreshGrid(INDGcSurgicalProcedure)
        SetDataSources()

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PIPSService(Me)
        LoadStatus()
        Deshacer()
        InitializeTuples()
        INDsleAssociatedMaterialIPSServiceId.Properties.Buttons.Item(1).Visible = False
        SearchMode = False
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmIPSService_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmIPSService_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBtnCode.Enabled = True Then
            INDBtnCode.Focus()
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            ValidateCode()
        End If
    End Sub


    Private Sub INDGlePromotionAndPrevention_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGlePromotionAndPrevention.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDLciPromotionAndPreventionActivities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDMePromotionAndPreventionActivities.Focus()
            ElseIf INDLcgSurgicalProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDPceSurgicalProcedure.Focus()
                INDPceSurgicalProcedure.ShowPopup()
                INDSleSurgicalProcedureCode.Focus()
            Else
                INDGleMinimunAgeUnit.Focus()
            End If
        End If
    End Sub

    Private Sub INDGleChildbirthAbortion_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDGleChildbirthAbortion.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter AndAlso INDLcgHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDPceHomologation.Focus()
            INDPceHomologation.ShowPopup()
            INDSleCodeHomologation.Focus()
        End If
    End Sub

    Private Sub INDMePromotionAndPreventionActivities_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDMePromotionAndPreventionActivities.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDLcgSurgicalProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDPceSurgicalProcedure.Focus()
                INDPceSurgicalProcedure.ShowPopup()
                INDSleSurgicalProcedureCode.Focus()
            Else
                INDGleMinimunAgeUnit.Focus()
            End If
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleApplyChangeScore_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyChangeScore.EditValueChanged
        If ApplyChangeScore IsNot Nothing Then
            If ApplyChangeScore Then
                INDlyItemNewScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemNewScore.AllowHide = False
            Else
                INDlyItemNewScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemNewScore.AllowHide = True
            End If
        Else
            INDlyItemNewScore.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemNewScore.AllowHide = True
        End If
    End Sub

    Private Sub INDsleIvaCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIVACode.EditValueChanged

        If INDsleIVACode.EditValue IsNot Nothing Then
            Using modelIva As New MIPSService(Me.Tag)
                Dim iva = modelIva.GetGeneralLedgerIVAById(INDsleIVACode.EditValue)
                IdAccountSale = iva.ObjectEmbbeded.IdAccountSale
                If IdAccountSale IsNot Nothing Then
                    Dim account = modelIva.GetMainAccountById(IdAccountSale)
                    INDsleSalesAccount.EditValue = account.NumberName
                Else
                    INDsleSalesAccount.EditValue = Nothing
                    INDsleSalesAccount.Properties.NullText = String.Empty
                End If
            End Using
        End If
    End Sub

    Private Sub INDrgProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgProduct.EditValueChanged
        If Not TaxedProduct Then
            INDLciIvaCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSalesAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleIVACode.EditValue = Nothing
            IdAccountSale = Nothing
            INDsleSalesAccount.EditValue = Nothing
            INDsleSalesAccount.Properties.NullText = String.Empty
        Else
            INDLciIvaCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSalesAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    Private Sub INDGlePresentation_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePresentation.EditValueChanged
        If PresentationProduct IsNot Nothing Then
            Select Case PresentationProduct
                Case 1
                    INDLcgSurgicalProcedure.HideControl()
                    INDLciServiceClass.HideControl(False)
                    INDLciSurgeryArtroscopica.HideControl()
                Case 2
                    INDLcgSurgicalProcedure.HideControl(False)
                    INDLciServiceClass.HideControl()
                    INDLciSurgeryArtroscopica.HideControl(False)
                    ServiceClass = Nothing
                Case 3
                    INDLcgSurgicalProcedure.HideControl()
                    INDLciServiceClass.HideControl()
                    INDLciSurgeryArtroscopica.HideControl(False)
                    ServiceClass = Nothing
            End Select
        End If
        CheckBillingConcept()
        ValidateGroupHomologation()
        HideControlsByServiceManualAndPresentation()
        HideControlScore()
        HideControlsLiquidation()
    End Sub

    Private Sub CheckBillingConcept()
        If PresentationProduct IsNot Nothing AndAlso PresentationProduct <> 0 AndAlso ServiceClass IsNot Nothing AndAlso ServiceClass <> 0 Then
            If PresentationProduct = 1 AndAlso ServiceClass <> 1 Then
                INDLciBillingConcept.HideControl(False)
            Else
                INDLciBillingConcept.HideControl()
                INDSleBillingConcept.EditValue = Nothing
                INDSleBillingConcept.Properties.NullText = String.Empty
            End If
        Else
            INDLciBillingConcept.HideControl()
            INDSleBillingConcept.EditValue = Nothing
            INDSleBillingConcept.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Metodo que oculta o muestra el grupo de homologacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateGroupHomologation()
        If PresentationProduct IsNot Nothing Then
            Select Case PresentationProduct
                Case 1
                    If ServiceClass IsNot Nothing Then
                        If ServiceClass = 1 Then
                            INDLcgHomologation.HideControl(False) '.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Else
                            INDLcgHomologation.HideControl() '.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        End If
                    Else
                        INDLcgHomologation.HideControl() '.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                Case 2, 3

                    INDLcgHomologation.HideControl(False) '.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End Select
        Else
            INDLcgHomologation.HideControl() '.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Metodo que oculta los controles dependiento del tipo de manual y la presentacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsByServiceManualAndPresentation()
        If ServiceManual IsNot Nothing AndAlso PresentationProduct IsNot Nothing Then
            If (ServiceManual = 1 OrElse ServiceManual = 2) AndAlso PresentationProduct = 2 Then
                INDlyItemUVR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemUVR.AllowHide = False
                INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSurgicalGroup.AllowHide = True
            ElseIf ServiceManual = 3 AndAlso PresentationProduct = 2 Then
                INDlyItemUVR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemUVR.AllowHide = True
                INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSurgicalGroup.AllowHide = False
            ElseIf ServiceManual = 4 AndAlso {1, 3}.Contains(PresentationProduct) Then
                INDlyItemUVR.HideControl()
                INDlyItemUVR.AllowHide = True
                INDlyItemSurgicalGroup.HideControl()
                INDlyItemSurgicalGroup.AllowHide = True
            Else
                INDlyItemUVR.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemUVR.AllowHide = True
                INDlyItemSurgicalGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSurgicalGroup.AllowHide = True
            End If
        End If
    End Sub

    Private Sub INDGlePromotionAndPrevention_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePromotionAndPrevention.EditValueChanged
        If PromotionAndPrevention IsNot Nothing Then
            If PromotionAndPrevention Then
                INDLciPromotionAndPreventionActivities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPromotionAndPreventionActivities.AllowHide = False
            Else
                INDLciPromotionAndPreventionActivities.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPromotionAndPreventionActivities.AllowHide = True
            End If
        End If
    End Sub

    Private Sub INDGleProcedure_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleProcedure.EditValueChanged
        If Procedure IsNot Nothing Then
            If Procedure = 2 Then
                INDLciPathologyService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPathologyService.AllowHide = False
            Else
                INDLciPathologyService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPathologyService.AllowHide = True
            End If
        End If
    End Sub

    Private Sub INDSleCodeHomologation_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCodeHomologation.EditValueChanged
        If INDSleCodeHomologation.EditValue IsNot Nothing Then
            If CupsEntityXPO IsNot Nothing Then
                Dim cupsEntity = DirectCast(DirectCast(INDGvHomologationCode.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CupsEntityXpo)
                INDTxtHomologationName.Text = cupsEntity.Description
                INDBtnAddHomologation.Enabled = True
            End If
        Else
            CleanControlsPopupHomologation()
        End If
    End Sub

    Private Sub INDSleSurgicalProcedureCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSurgicalProcedureCode.EditValueChanged
        If INDSleSurgicalProcedureCode.EditValue IsNot Nothing Then

            Dim service = DirectCast(DirectCast(INDGvSleSurgicalProcedureCode.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ContractIPSServiceXPO)
            INDTxtSurgicalProcedureName.Text = service.Name
            INDTxtClass.Text = service.ServiceClassName

            If service.ServiceClass = 5 Then
                ReasizablePopup(True)

                If service.AssociatedMaterialIPSServiceId IsNot Nothing Then
                    INDtxtAssociated.Text = service.AssociatedMaterialIPSServiceId.CodeName
                Else
                    INDtxtAssociated.Text = "Ninguno"
                End If
            Else
                ReasizablePopup(False)
            End If

            INDBtnAddSurgicalProcedure.Enabled = True
        Else
            CleanControlsPopupSurgicalProcedure()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que redimensiona el popup de quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReasizablePopup(optionResize As Boolean)
        Dim size As System.Drawing.Size
        If optionResize Then
            size.Width = 416
            size.Height = 276
            INDlyItemAssociated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            size.Width = 416
            size.Height = 244
            INDlyItemAssociated.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        INDPceSurgicalProcedure.Properties.PopupSizeable = True
        INDPccSurgicalProcedure.Size = size
        INDPceSurgicalProcedure.Properties.PopupSizeable = False

        INDPceSurgicalProcedure.ShowPopup()
    End Sub

    Private Sub INDGleMinimunAgeUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleMinimunAgeUnit.EditValueChanged
        Select Case MinimunAgeUnit
            Case 1
                INDSeMinimunAge.Properties.MaxLength = 3
                INDSeMinimunAge.Properties.MaxValue = 150
            Case 2
                INDSeMinimunAge.Properties.MaxLength = 4
                INDSeMinimunAge.Properties.MaxValue = 1800
            Case 3
                INDSeMinimunAge.Properties.MaxLength = 5
                INDSeMinimunAge.Properties.MaxValue = 54000
            Case Else
                INDSeMinimunAge.Properties.MaxLength = 0
                INDSeMinimunAge.Properties.MaxValue = 0
        End Select

    End Sub

    Private Sub INDGleMaximumAgeUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleMaximumAgeUnit.EditValueChanged
        Select Case MaximumAgeUnit
            Case 1
                INDSeMaximumAge.Properties.MaxLength = 3
                INDSeMaximumAge.Properties.MaxValue = 150
            Case 2
                INDSeMaximumAge.Properties.MaxLength = 4
                INDSeMaximumAge.Properties.MaxValue = 1800
            Case 3
                INDSeMaximumAge.Properties.MaxLength = 5
                INDSeMaximumAge.Properties.MaxValue = 54000
            Case Else
                INDSeMaximumAge.Properties.MaxLength = 0
                INDSeMaximumAge.Properties.MaxValue = 0
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de manual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleServiceManual_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleServiceManual.EditValueChanged
        HideControlsByServiceManualAndPresentation()

        If ServicesIPSXPO IsNot Nothing AndAlso ServiceManual IsNot Nothing Then
            Presenter.InitializeIPSServiceXPO(ServiceManual)
        End If

        HideControlScore()
        AssociatedMaterialIPSServiceId = Nothing
        INDsleAssociatedMaterialIPSServiceId.Properties.NullText = String.Empty
        AssociatedMaterialIPSServiceIdXpo = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la clase
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleServiceClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleServiceClass.EditValueChanged
        ValidateGroupHomologation()
        HideControlScore()
        HideControlsLiquidation()

        If ServiceClass = 5 And ServiceManual <> 4 Then
            INDlyItemAssociatedMaterialIPSServiceId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemAssociatedMaterialIPSServiceId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        CheckBillingConcept()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor a vacio del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAssociatedMaterialIPSServiceId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAssociatedMaterialIPSServiceId.EditValueChanged
        If INDsleAssociatedMaterialIPSServiceId.EditValue Is Nothing Then
            INDsleAssociatedMaterialIPSServiceId.Properties.NullText = String.Empty
        End If
    End Sub

#End Region

#Region "Click"
    Private Sub INDBtnAddHomologation_Click(sender As Object, e As EventArgs) Handles INDBtnAddHomologation.Click
        cupsHomologation = New CupsHomologation
        With cupsHomologation
            .CupsEntityId = INDSleCodeHomologation.EditValue
            .CodeNameCupsEntity = INDSleCodeHomologation.Text
        End With

        If listCupsHomologation Is Nothing Then
            listCupsHomologation = New List(Of CupsHomologation)
        End If

        If listCupsHomologation.Any(Function(x) x.CupsEntityId = cupsHomologation.CupsEntityId) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CUPSAdded", NAME_MODULE)
            Exit Sub
        End If

        listCupsHomologation.Add(cupsHomologation)
        INDGcHomologation.DataSource = Nothing
        INDGcHomologation.DataSource = listCupsHomologation
        CleanControlsPopupHomologation()
        INDSleCodeHomologation.Focus()
    End Sub
    ''' <summary>
    ''' agregar un procedimiento quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddSurgicalProcedure_Click(sender As Object, e As EventArgs) Handles INDBtnAddSurgicalProcedure.Click
        surgicalProcedureService = New SurgicalProcedureService
        Dim ServiceSelected = DirectCast(DirectCast(INDGvSleSurgicalProcedureCode.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ContractIPSServiceXPO)
        With surgicalProcedureService
            .IPSService = New IPSService
            If ServiceSelected.AssociatedMaterialIPSServiceId IsNot Nothing Then
                .IPSService.AssociatedMaterialIPSServiceId = ServiceSelected.AssociatedMaterialIPSServiceId?.Id
            End If
            .IPSService.Presentation = ServiceSelected.Presentation
            .IPSService.ServiceClass = ServiceSelected.ServiceClass
            .IPSServiceId = INDSleSurgicalProcedureCode.EditValue
            .CodeNameService = INDSleSurgicalProcedureCode.Text
            .ClassService = INDTxtClass.Text
            .DefaultService = INDsleDefaultService.EditValue
            If INDSeAmount.EditValue < 0 Then
                .ServiceAmount = CInt(INDSeAmount.EditValue * -1)
            Else
                .ServiceAmount = CInt(INDSeAmount.EditValue)
            End If
        End With

        If listSurgicalProcedureService Is Nothing Then
            listSurgicalProcedureService = New List(Of SurgicalProcedureService)
        End If

        If listSurgicalProcedureService.Any(Function(x) x.IPSServiceId = surgicalProcedureService.IPSServiceId) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ServiceAdded", NAME_MODULE)
            Exit Sub
        End If

        If listSurgicalProcedureService.Any(Function(x) x.ClassService = INDTxtClass.Text AndAlso x.DefaultService = True AndAlso INDsleDefaultService.EditValue = True) AndAlso ServiceManual <> 4 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ExistService", NAME_MODULE)
            Exit Sub
        End If

        If listSurgicalProcedureService.Any(Function(x) x.IPSService.AssociatedMaterialIPSServiceId IsNot Nothing) And
            surgicalProcedureService.IPSService.ServiceClass = 6 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar el material seleccionado porque hay un derecho de sala agregado que ya tiene un material asociado"
            Exit Sub
        End If

        If listSurgicalProcedureService.Any(Function(x) x.IPSService.ServiceClass = 6) And
            surgicalProcedureService.IPSService.ServiceClass = 5 And surgicalProcedureService.IPSService.AssociatedMaterialIPSServiceId IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar el derecho de sala porque tiene un material asociado y ya hay un material agregado"
            Exit Sub
        End If

        If listSurgicalProcedureService.Any(Function(x) x.IPSService.AssociatedMaterialIPSServiceId IsNot Nothing) And
            surgicalProcedureService.IPSService.AssociatedMaterialIPSServiceId IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Hay un derecho de sala que ya tiene asociado un material y solo puede haber uno agregado"
            Exit Sub
        End If

        If listSurgicalProcedureService.Any(Function(x) x.IPSService.ServiceClass = 6) And surgicalProcedureService.IPSService.ServiceClass = 6 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya hay un material agregado"
            Exit Sub
        End If

        listSurgicalProcedureService.Add(surgicalProcedureService)
        INDGcSurgicalProcedure.DataSource = Nothing
        INDGcSurgicalProcedure.DataSource = listSurgicalProcedureService
        ReasizablePopup(False)
        CleanControlsPopupSurgicalProcedure()
        INDSleSurgicalProcedureCode.Focus()
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceHomologation_Popup(sender As Object, e As EventArgs) Handles INDPceHomologation.Popup
        IndigoGridControl1.SetExportButton(INDGcHomologation, False)
        INDSleCodeHomologation.Focus()
    End Sub

    Private Sub INDPceSurgicalProcedure_Popup(sender As Object, e As EventArgs) Handles INDPceSurgicalProcedure.Popup
        IndigoGridControl1.SetExportButton(INDGcSurgicalProcedure, False)
        INDSleSurgicalProcedureCode.Focus()
    End Sub
#End Region

#Region "CloseUp"
    Private Sub INDPceHomologation_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceHomologation.CloseUp
        If listCupsHomologation IsNot Nothing AndAlso listCupsHomologation.Count > 0 Then
            IndigoGridControl1.SetExportButton(INDGcHomologation, True)
        Else
            IndigoGridControl1.SetExportButton(INDGcHomologation, False)
        End If
    End Sub

    Private Sub INDPceSurgicalProcedure_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceSurgicalProcedure.CloseUp
        If listSurgicalProcedureService IsNot Nothing AndAlso listSurgicalProcedureService.Count > 0 Then
            IndigoGridControl1.SetExportButton(INDGcSurgicalProcedure, True)
        Else
            IndigoGridControl1.SetExportButton(INDGcSurgicalProcedure, False)
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            cupsHomologation = DirectCast(INDGvHomologation.GetFocusedRow, CupsHomologation)

            If cupsHomologation.Id > 0 Then
                If listCupsHomologationDelete Is Nothing Then
                    listCupsHomologationDelete = New List(Of CupsHomologation)
                End If
                listCupsHomologationDelete.Add(cupsHomologation)
            End If

            listCupsHomologation.Remove(cupsHomologation)
            INDGcHomologation.DataSource = Nothing
            INDGcHomologation.DataSource = listCupsHomologation

            If listCupsHomologation.Count > 0 Then
                IndigoGridControl1.SetExportButton(INDGcHomologation, True)
            Else
                IndigoGridControl1.SetExportButton(INDGcHomologation, False)
                IndigoGridControl1.RefreshGrid(INDGcHomologation)
            End If

        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            surgicalProcedureService = DirectCast(INDGvSurgicalProcedure.GetFocusedRow, SurgicalProcedureService)

            If surgicalProcedureService.Id > 0 Then
                If listSurgicalProcedureServiceDelete Is Nothing Then
                    listSurgicalProcedureServiceDelete = New List(Of SurgicalProcedureService)
                End If
                listSurgicalProcedureServiceDelete.Add(surgicalProcedureService)
            End If

            listSurgicalProcedureService.Remove(surgicalProcedureService)
            INDGcSurgicalProcedure.DataSource = Nothing
            INDGcSurgicalProcedure.DataSource = listSurgicalProcedureService

            If listSurgicalProcedureService.Count > 0 Then
                IndigoGridControl1.SetExportButton(INDGcSurgicalProcedure, True)
            Else
                IndigoGridControl1.SetExportButton(INDGcSurgicalProcedure, False)
                IndigoGridControl1.RefreshGrid(INDGcSurgicalProcedure)
            End If

        End If
    End Sub
#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.iPSService IsNot Nothing AndAlso Me.iPSService.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                Await LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            Await LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDBtnCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleCodeHomologation_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCodeHomologation.QueryPopUp
        If CupsEntityXPO Is Nothing Then
            Presenter.InitializeCupsEntityXPO()
        End If
    End Sub

    Private Sub INDsleIVACode__QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleIVACode.QueryPopUp
        If INDsleIVACode.Properties.DataSource Is Nothing Then
            Presenter.InitializeIva()
        End If
    End Sub

    Private Sub INDSleSurgicalProcedureCode_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSurgicalProcedureCode.QueryPopUp
        If ServicesIPSXPO Is Nothing AndAlso ServiceManual IsNot Nothing Then
            Presenter.InitializeIPSServiceXPO(ServiceManual)
        End If
    End Sub

    Private Sub INDSleBillingConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBillingConcept.QueryPopUp
        If INDSleBillingConcept.Properties.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                Me.INDSleBillingConcept.Properties.DataSource = model.GetBillinConceptByType(2) 'Servicios de Salud
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo quirurgico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSurgicalGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSurgicalGroup.QueryPopUp
        If SurgicalGroupXpo Is Nothing Then
            Presenter.InitializeSurgicalGroupXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de material asociado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAssociatedMaterialIPSServiceId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleAssociatedMaterialIPSServiceId.QueryPopUp
        If AssociatedMaterialIPSServiceIdXpo Is Nothing AndAlso ServiceManual IsNot Nothing Then
            Presenter.InitializeAssociatedMaterialIPSService(ServiceManual)
        End If
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para abrir el formulario correspondiente (Grupo Quirurgico)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSurgicalGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSurgicalGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSurgicalGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSurgicalGroupXpo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCodeHomologation_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCodeHomologation.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsEntity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCupsEntityXPO()
        End If
    End Sub

    Private Sub INDSleBillingConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleBillingConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("749", Nothing, True)
        End If
    End Sub
#End Region

#Region "AsyncCompleted"
    Private Sub INDGvSleSurgicalProcedureCode_AsyncCompleted(sender As Object, e As EventArgs) Handles INDGvSleSurgicalProcedureCode.AsyncCompleted
        INDGvSleSurgicalProcedureCode.ExpandAllGroups()
    End Sub
#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If ServiceManual Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir un tipo manual para poder copiar y pegar"
            Exit Sub
        End If
        Await PasteToGrid(sender, e.Rows)
    End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDGcSurgicalProcedure.Name Then
            INDGvSurgicalProcedure.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MIPSService(MyTag)
                Dim result = Await model.CopyAndPasteIPSService(ListInfo, ServiceManual)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDGvSurgicalProcedure.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If listSurgicalProcedureService IsNot Nothing AndAlso listSurgicalProcedureService.Count > 0 Then
                        Dim listBillsTmp = (From s In result.ObjectEmbbeded Select s.IPSServiceId).ToList.Distinct.ToList()
                        Dim listBillsNotExist As New List(Of String)
                        For i As Integer = 0 To listBillsTmp.Count - 1 Step 1
                            Dim share = listSurgicalProcedureService.Where(Function(x) x.IPSServiceId = listBillsTmp.Item(i)).FirstOrDefault()
                            If share IsNot Nothing Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El servicio IPS " + share.CodeNameService + " ya existe en la lista", 2))
                            Else
                                listBillsNotExist.Add(listBillsTmp.Item((i)))
                            End If
                        Next
                        For i As Integer = 0 To listBillsNotExist.Count - 1 Step 1
                            listSurgicalProcedureService.AddRange(result.ObjectEmbbeded.FindAll(Function(x) x.IPSServiceId = listBillsNotExist.Item(i)))
                        Next

                    Else
                        listSurgicalProcedureService = result.ObjectEmbbeded
                    End If
                End If
                If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                    Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            End Using
            INDGcSurgicalProcedure.DataSource = Nothing
            INDGcSurgicalProcedure.DataSource = listSurgicalProcedureService
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDGvSurgicalProcedure.HideLoadingPanel()
        End If
    End Function

#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
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
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

#End Region

End Class