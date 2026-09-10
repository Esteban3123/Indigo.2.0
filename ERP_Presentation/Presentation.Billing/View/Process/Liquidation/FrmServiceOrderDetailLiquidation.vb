'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Contract
Imports Presentation.Contract.MVP
Imports Presentation.Controls
Imports Presentation.Payroll

#End Region

Public Class FrmServiceOrderDetailLiquidation

#Region "EVENTS"
    ''' <summary>
    ''' evento que se dispara cuando se de click en el boton de agregar y pasa el o los registros al formulario principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
#End Region

#Region "BUILDER"
    Sub New(settingsBillign As SettingsBilling)
        Me.New()
        _settingBilling = settingsBillign
    End Sub


    Sub New()
        InitializeComponent()
        ctrTmp = New CtrServiceOrderInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        INDGvSurgery.OptionsView.ShowAutoFilterRow = False
    End Sub
#End Region

#Region "TUPLES"
    ''' <summary>
    ''' Parámetros de facturación
    ''' </summary>
    Private _settingBilling As SettingsBilling
    ''' <summary>
    ''' representa el datasource de tipo de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listServiceType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListServiceType As List(Of Tuple(Of Byte, String))
        Get
            If _listServiceType Is Nothing Then
                _listServiceType = New List(Of Tuple(Of Byte, String))
                _listServiceType.Add(New Tuple(Of Byte, String)(1, "SOAT"))
                _listServiceType.Add(New Tuple(Of Byte, String)(2, "ISS"))
                _listServiceType.Add(New Tuple(Of Byte, String)(3, "CUPS"))
            End If
            Return _listServiceType
        End Get
    End Property

    Public Property OperativeUnitId() As Integer

    ''' <summary>
    ''' representa el datasource de tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listLiquidationType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListLiquidationType As List(Of Tuple(Of Byte, String))
        Get
            If _listLiquidationType Is Nothing Then
                _listLiquidationType = New List(Of Tuple(Of Byte, String))
                _listLiquidationType.Add(New Tuple(Of Byte, String)(1, "Manual de Tarifas"))
                _listLiquidationType.Add(New Tuple(Of Byte, String)(2, "% de otro servicio cargado"))
                _listLiquidationType.Add(New Tuple(Of Byte, String)(4, "% del mismo servicio"))
                _listLiquidationType.Add(New Tuple(Of Byte, String)(3, "Incluido el 100%"))
            End If
            Return _listLiquidationType
        End Get
    End Property
    ''' <summary>
    ''' data source para los tipos de intervencion quirurgicos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listSurgicalInterventionType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListSurgicalInterventionType As List(Of Tuple(Of Byte, String))
        Get
            If _listSurgicalInterventionType Is Nothing Then
                _listSurgicalInterventionType = New List(Of Tuple(Of Byte, String))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(1, "Basico"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(2, "Bilateral"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(3, "MIVIE (Multiple Igual Via Igual Especialista)"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(4, "MDVIE (Multiple Diferente Via Igual Especialista)"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(5, "MIVDE (Multiple Igual Via Diferente Especialista)"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(6, "MDVDE (Multiple Diferente Via Diferente Especialista)"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(7, "PolitraumaIV (Politrauma Igual Via)"))
                _listSurgicalInterventionType.Add(New Tuple(Of Byte, String)(8, "PolitraumaDV (Politrauma Diferente Via)"))
            End If
            Return _listSurgicalInterventionType
        End Get
    End Property
    ''' <summary>
    ''' data source para los tipos de intervencion quirurgicos no cruentos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listSurgicalInterventionTypeNoBloody As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListSurgicalInterventionTypeNoBloody As List(Of Tuple(Of Byte, String))
        Get
            If _listSurgicalInterventionTypeNoBloody Is Nothing Then
                _listSurgicalInterventionTypeNoBloody = New List(Of Tuple(Of Byte, String))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(1, "Basico"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(2, "Bilateral"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(3, "MIVIE (Multiple Igual Via Igual Especialista)"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(4, "MDVIE (Multiple Diferente Via Igual Especialista)"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(5, "MIVDE (Multiple Igual Via Diferente Especialista)"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(6, "MDVDE (Multiple Diferente Via Diferente Especialista)"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(7, "PolitraumaIV (Politrauma Igual Via)"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(8, "PolitraumaDV (Politrauma Diferente Via)"))
                _listSurgicalInterventionTypeNoBloody.Add(New Tuple(Of Byte, String)(9, "No Cruento"))
            End If
            Return _listSurgicalInterventionTypeNoBloody
        End Get
    End Property
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"
    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrServiceOrderInfo
    ''' <summary>
    ''' representa la entidad de CUPS
    ''' </summary>
    ''' <remarks></remarks>
    Private cupsEntity As CUPSEntity
    ''' <summary>
    ''' listado de las homologaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private listHomologation As List(Of CupsHomologation)
    ''' <summary>
    ''' bandera que indica se se ejecuta el metodo para calcular el valor del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private runCalculationValue As Boolean
    ''' <summary>
    ''' listado de los detalles de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Public listServiceOrderDetailPopup As List(Of ServiceOrderDetail)
    ''' <summary>
    ''' representa la entidad del detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private serviceOrderDetail As ServiceOrderDetail
    ''' <summary>
    ''' almacena el valor del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceValue As Decimal = 0
    ''' <summary>
    ''' fecha de nacimiento del paciente para validar el servicio ips
    ''' </summary>
    ''' <remarks></remarks>
    Private _patientDate As DateTime
    ''' <summary>
    ''' genero del paciente para validar el servicio ips
    ''' </summary>
    ''' <remarks></remarks>
    Private _patienGenus As Integer
    ''' <summary>
    ''' listado de los detalles cuando el servicio ips es quirirgico
    ''' </summary>
    ''' <remarks></remarks>
    Private listSurgicalProcedureService As List(Of SurgicalProcedureService)
    ''' <summary>
    ''' bandera para controlar que el calculo se ejecute solo cuando se cambie el valor desde el control de valor
    ''' </summary>
    ''' <remarks></remarks>
    Private _changeValue As Boolean
    ''' <summary>
    ''' bandera para saber si se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private _editMode As Boolean
    ''' <summary>
    ''' variable para almacenar el mensaje de error cuando se esta obteniendo el valor del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private errorsGetValue As String
    ''' <summary>
    ''' representa la entidad de tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private thirdParty As Domain.Entities.ThirdParty
    ''' <summary>
    ''' bandera para saber que el valor de los controles se cambio desde el metodo de cargar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private editValueChangeLoadControl As Boolean
    ''' <summary>
    ''' bandera para saber si se limpian los controles 
    ''' </summary>
    ''' <remarks></remarks>
    Private cleaningControls As Boolean = True
    ''' <summary>
    ''' bandera para saber cuando el usuario selecciono mas de una homologacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _flagHomologation As Boolean
    ''' <summary>
    ''' representa la entidad xpo de los profesionales de la salud
    ''' </summary>
    ''' <remarks></remarks>
    Dim healthProfessional As HealthCareProfessionalXpo
#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Gets or sets the care group parent identifier.
    ''' </summary>
    ''' <value>
    ''' The care group parent identifier.
    ''' </value>
    Public Property CareGroupParentId As Integer
        Get
            Return INDSleCareGroup.EditValue
        End Get
        Set(value As Integer)
            INDSleCareGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' listado del detalle de la orden de servicio para usar como datasource cuando se va a incluir en otro servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceOrderDetailDatasourceIncludeService As List(Of ServiceOrderDetail)
    ''' <summary>
    ''' listado de los detalle que se agregan al combo para que se puedan incluir en otro servicio
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListServiceOrderDetailDatasourceIncludeService As List(Of ServiceOrderDetail)
        Get
            Return _listServiceOrderDetailDatasourceIncludeService
        End Get
        Set(value As List(Of ServiceOrderDetail))
            _listServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)(value.ToArray())
        End Set
    End Property

    ''' <summary>
    ''' listado del detalle de la orden de servicio para los tipos de intervenciones quirurgicas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceOrderDetailSurgicalIntervention As List(Of ServiceOrderDetail)
    ''' <summary>
    ''' listado de los detalle de la orden de servicio con los tipos de intervenciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListServiceOrderDetailSurgicalIntervention As List(Of ServiceOrderDetail)
        Get
            Return _listServiceOrderDetailSurgicalIntervention
        End Get
        Set(value As List(Of ServiceOrderDetail))
            _listServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)(value.ToArray())
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    Public Property Admission As String
        Get
            Return INDLblAdmission.Text
        End Get
        Set(value As String)
            INDLblAdmission.Text = value
        End Set
    End Property
    Public Property CenterAttentionCode As String
    Private Property FunctionalUnitCenterAttentionCode As String
    Public Property PatientDateBirth As String
        Get
            Return INDLblAge.Text
        End Get
        Set(value As String)
            INDLblAge.Text = Utils.AgeToString(CDate(value))
            _patientDate = CDate(value)
        End Set
    End Property
    ''' <summary>
    ''' nombre del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Dim _patient As String = String.Empty
    Public Property Patient As String
        Get
            Return _patient
        End Get
        Set(value As String)
            _patient = value
        End Set
    End Property
    ''' <summary>
    ''' genero del paciente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PatientGenus As String
        Get
            Return INDLblGenus.Text
        End Get
        Set(value As String)
            _patienGenus = value
            If value = 1 Then
                INDLblGenus.Text = ResourceManager.GetString("Male", MODULE_NAME)
            Else
                INDLblGenus.Text = ResourceManager.GetString("Female", MODULE_NAME)
            End If
        End Set
    End Property
    ''' <summary>
    ''' numero de la cama 
    ''' </summary>
    ''' <remarks></remarks>
    Public WriteOnly Property Stay As String
        Set(value As String)
            INDLblStay.Text = value
        End Set
    End Property

    Private Property IPSServiceXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleServiceSoatIss.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleServiceSoatIss.Properties.DataSource = value
        End Set
    End Property

    Private Property CostCenterXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSLeCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSLeCostCenter.Properties.DataSource = value
        End Set
    End Property

    Private Property PerformsHealthProfessionalXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlePerformsHealthProfessionalCode.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePerformsHealthProfessionalCode.Properties.DataSource = value
        End Set
    End Property

    Private Property PerformsHealthProfessionalRepositoryXPO As XPInstantFeedbackSource
        Get
            Return CType(INDRptSleHealthProfessional.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDRptSleHealthProfessional.DataSource = value
        End Set
    End Property

    Private Property CareGroupXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleCareGroup.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCareGroup.Properties.DataSource = value
        End Set
    End Property

    Private Property SoatIssXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleServiceSoatIss.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleServiceSoatIss.Properties.DataSource = value
        End Set
    End Property

    Property CupsXPO As LinqInstantFeedbackSource
        Get
            Return CType(INDSleServiceCups.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDSleServiceCups.Properties.DataSource = value
        End Set
    End Property

    Property PerformsFuntionalUnitXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSlePerformsFuntionalUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlePerformsFuntionalUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para activar o desactivar controles cuando se llenan y se obtiene la homologacion o el valor
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActivateControls
        Set(value)
            'INDSleCareGroup.Enabled = value
            INDGleServiceType.Enabled = value
            INDSleServiceCups.Enabled = value
            INDSleServiceSoatIss.Enabled = value
            INDDteDate.Enabled = value
            INDSlePerformsFuntionalUnit.Enabled = value
            INDSlePerformsHealthProfessionalCode.Enabled = value
            INDSlePerformsHealthProfessionalCode.Enabled = value
            INDGleSpecialtyPerformsHealthProfessional.Enabled = value
            INDGleSpecialtyPerformsHealthProfessional.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' proepiedad para activar los controles del grupo de comportamiento
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActivateControlsBehavior
        Set(value)
            INDTxtAuthorizationNumber.Enabled = value
            'INDGleLiquidationType.Enabled = value
            INDSleIncludeService.Enabled = value
            INDSePercent.Enabled = value
            INDGleSurchargeApply.Enabled = value
            INDTxtValue.Enabled = value
            INDSeIndividualDiscount.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para cargar los datos para editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ServiceOrderDetailEdit As ServiceOrderDetail
        Set(value As ServiceOrderDetail)
            serviceOrderDetail = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Public WriteOnly Property AdmissionDate As Date
        Set(value As Date)
            INDDteDate.Properties.MinValue = value
        End Set
    End Property

    Dim _thirdPartyId As Integer?
    WriteOnly Property ThirdPartyId As Integer?
        Set(value As Integer?)
            _thirdPartyId = value
        End Set
    End Property

    Dim _healthAdministratorId As Integer?
    WriteOnly Property HealthAdministratorId As Integer?
        Set(value As Integer?)
            _healthAdministratorId = value
        End Set
    End Property

    ''' <summary>
    ''' valor total unitario
    ''' </summary>
    ''' <returns></returns>
    Property TotalSalesPrice As Decimal
        Get
            Return INDTxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' cantidad
    ''' </summary>
    ''' <returns></returns>
    Property Quantity As Integer
        Get
            Return INDSeCount.EditValue
        End Get
        Set(value As Integer)
            INDSeCount.EditValue = value
        End Set
    End Property
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método que me muestra u oculta el grupo descripciones
    ''' </summary>
    Private Sub HideShowFieldsDescription()
        Dim careGroupXpo As Infrastructure.Data.Xpo.BillingRepository.CareGroupXpo = Nothing
        Dim cupsEntityXpo As Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo = Nothing

        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        INDsleDescription.Properties.DataSource = Nothing

        Using srvOrderModel As New MServiceOrder("")
            If INDSleCareGroup.EditValue IsNot Nothing Then
                careGroupXpo = srvOrderModel.CareGroupById(INDSleCareGroup.EditValue)
            End If

            If INDSleServiceCups.EditValue IsNot Nothing Then
                cupsEntityXpo = srvOrderModel.GetCupsEntityById(INDSleServiceCups.EditValue)
            End If
        End Using

        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If cupsEntityXpo IsNot Nothing AndAlso cupsEntityXpo.CUPSEntityContractDescriptionsXpo IsNot Nothing AndAlso cupsEntityXpo.CUPSEntityContractDescriptionsXpo.Count > 0 Then
            INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDBtnAddDetail.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Contiene la vista del folio padre que contiene los items a empaquetar
    ''' </summary>
    ''' <value>
    ''' The view to package items.
    ''' </value>
    Property ViewToPackageItems As DevExpress.XtraGrid.Views.Grid.GridView

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, Decimal)
        Return New Tuple(Of String, String, Decimal)(Patient, Format(_serviceValue, "c2"), 0)
    End Function

    ''' <summary>
    ''' limpia controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        serviceOrderDetail = Nothing
        ActivateControls = True
        ActivateControlsBehavior = False
        If cleaningControls = True Then
            'INDSleCareGroup.EditValue = Nothing
            'INDSleCareGroup.Properties.NullText = String.Empty
            INDGleServiceType.EditValue = Nothing
            INDGleServiceType.Properties.ReadOnly = False
            'INDGleServiceType.Enabled = False
            INDSleServiceSoatIss.EditValue = Nothing
            INDSleServiceSoatIss.Properties.NullText = String.Empty
            INDSleServiceSoatIss.Properties.ReadOnly = False
            INDSleServiceCups.EditValue = Nothing
            INDSleServiceCups.Properties.NullText = String.Empty
            INDSleServiceCups.Properties.ReadOnly = False
            'INDSleCareGroup.Focus()
        Else
            If INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleServiceSoatIss.EditValue = Nothing
                INDSleServiceSoatIss.Properties.NullText = String.Empty
                INDSleServiceSoatIss.Focus()
            Else
                INDSleServiceCups.EditValue = Nothing
                INDSleServiceCups.Properties.NullText = String.Empty
                INDSleServiceCups.Focus()
            End If
        End If

        INDsleContractPackage.EditValue = Nothing
        INDsleContractPackage.Properties.NullText = String.Empty

        Me.Quantity = 1
        INDSeCount.Properties.ReadOnly = True
        INDDteDate.EditValue = GetDateServer()
        INDDteDate.Properties.ReadOnly = False
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSLeCostCenter.EditValue = Nothing
        INDSLeCostCenter.Properties.NullText = String.Empty
        INDSLeCostCenter.Properties.ReadOnly = True
        INDSlePerformsHealthProfessionalCode.EditValue = Nothing
        INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = False
        INDSlePerformsHealthProfessionalCode.Properties.NullText = String.Empty
        INDTxtAuthorizationNumber.EditValue = Nothing
        INDTxtAuthorizationNumber.Properties.ReadOnly = False
        INDGleLiquidationType.EditValue = 1
        'INDGleLiquidationType.Properties.ReadOnly = True
        INDSleIncludeService.EditValue = Nothing
        INDSleIncludeService.Properties.ReadOnly = False
        INDSleIncludeService.Properties.NullText = String.Empty
        INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercent.EditValue = 0
        INDSePercent.Properties.ReadOnly = False
        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.TotalSalesPrice = 0
        INDTxtValue.Properties.ReadOnly = False
        INDGleSurchargeApply.EditValue = 0
        INDGleSurchargeApply.Properties.ReadOnly = False
        INDSeSurgeryNumber.EditValue = Nothing
        INDSeSurgeryNumber.Properties.ReadOnly = False
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDGleSurgicalInterventionType.EditValue = Nothing
        INDGleSurgicalInterventionType.Properties.ReadOnly = False
        listSurgicalProcedureService = Nothing
        INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSlePerformsFuntionalUnit.EditValue = Nothing
        INDSlePerformsFuntionalUnit.Properties.NullText = String.Empty
        INDSlePerformsFuntionalUnit.Properties.ReadOnly = False
        INDGleSpecialtyPerformsHealthProfessional.EditValue = Nothing
        INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.NullText = String.Empty
        _serviceValue = 0
        ctrTmp.PrintInfo()
        listServiceOrderDetailPopup = Nothing
        listHomologation = Nothing
        Me.BarraBotones.FilterDataSource = Nothing
        _editMode = False
        _flagHomologation = False
        INDBtnAddDetail.Text = ResourceManager.GetString("Add")

        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleDescription.Properties.DataSource = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        INDsleDescription.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles de los valores del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsErrorValueFields()
        serviceOrderDetail = Nothing
        ActivateControlsBehavior = False
        INDTxtAuthorizationNumber.Text = String.Empty
        INDTxtAuthorizationNumber.Properties.ReadOnly = False
        INDGleLiquidationType.EditValue = 1
        'INDGleLiquidationType.Properties.ReadOnly = True
        INDSleIncludeService.EditValue = Nothing
        INDSleIncludeService.Properties.ReadOnly = False
        INDSleIncludeService.Properties.NullText = String.Empty
        INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercent.EditValue = 0
        INDSePercent.Properties.ReadOnly = False
        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.TotalSalesPrice = 0
        INDTxtValue.Properties.ReadOnly = False
        INDGleSurchargeApply.EditValue = 0
        INDGleSurchargeApply.Properties.ReadOnly = False
        INDSeSurgeryNumber.EditValue = Nothing
        INDSeSurgeryNumber.Properties.ReadOnly = False
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDGleSurgicalInterventionType.EditValue = Nothing
        INDGleSurgicalInterventionType.Properties.ReadOnly = False
        listSurgicalProcedureService = Nothing
        INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _serviceValue = 0
        ctrTmp.PrintInfo()
        listServiceOrderDetailPopup = Nothing
        listHomologation = Nothing
        Me.BarraBotones.FilterDataSource = Nothing
    End Sub



    ''' <summary>
    ''' metodo para establecer los datasource a los combos con datos quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSourceCombo()
        INDGleServiceType.Properties.DataSource = ListServiceType
        INDGleLiquidationType.Properties.DataSource = ListLiquidationType
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
    End Sub

    ''' <summary>
    ''' metodo pra establecer las especialidades del medico en el combo
    ''' </summary>
    ''' <param name="healthProfessional"></param>
    ''' <param name="control"></param>
    ''' <remarks></remarks>
    Private Sub SetSpecialties(healthProfessional As HealthCareProfessionalXpo, control As GridLookUpEdit)
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
        control.Properties.DataSource = listSpecialty
        If listSpecialty.Count = 1 Then
            control.EditValue = listSpecialty(0).Item1
        Else
            control.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' metodo que recibe las homologaciones seleccionadas en el popup cuando un servicio tiene mas de una
    ''' </summary>
    ''' <param name="Senders"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnSetHomologation(Senders As Object, e As SetHomologationEventArgs)
        listHomologation = Nothing
        listHomologation = e.ListHomologations
        If listHomologation.Count > 1 Then
            ActivateControls = False
            _flagHomologation = True
        End If
        GetServiceValue()
    End Sub

    ''' <summary>
    ''' metodo para obtener las homologaciones del iss o soat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHomologationIssSoat()
        If INDSleCareGroup.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDSleServiceSoatIss.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDDteDate.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDSlePerformsFuntionalUnit.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDGleSpecialtyPerformsHealthProfessional.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        runCalculationValue = False
        listHomologation = Nothing
        Using model As New MServiceOrder(Me.Tag)
            AsyncLoader(True)
            Dim listHomologationTmp = model.ListCupsHomologationByIpsServiceId(INDSleServiceSoatIss.EditValue)
            If listHomologation Is Nothing Then
                listHomologation = New List(Of CupsHomologation)
            End If
            Dim errorsHomologation As New StringBuilder
            Dim manualType = 0
            Select Case INDGleServiceType.EditValue
                Case 1
                    manualType = 3 'soar
                Case 2
                    manualType = 1 'iss
            End Select
            Dim errorhomologation As New StringBuilder()
            For Each item In listHomologationTmp
                Dim result = model.GetHomologationCups(INDSleCareGroup.EditValue, item.CupsEntityId, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, INDSleServiceSoatIss.EditValue, manualType)
                If result.StateResult = False Then
                    errorsHomologation.AppendLine(result.Message)
                    Continue For
                End If
                If result.ObjectEmbbeded.Where(Function(x) x.Presentation = 3).ToList().Count = 0 Then
                    errorhomologation.AppendLine(String.Format("No se Encontraron Homologos para el item {0} para el grupo de atención {1}", result.ObjectEmbbeded(0).CodeNameCupsEntity, INDSleCareGroup.Text))
                    Continue For
                End If
                listHomologation.AddRange(result.ObjectEmbbeded.Where(Function(x) x.Presentation = 3).ToList())
            Next
            AsyncLoader(False)
            If errorhomologation.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorhomologation.ToString()
                CleanControlsErrorValueFields()
                Exit Sub
            End If

            If errorsHomologation.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsHomologation.ToString()
                CleanControlsErrorValueFields()
                Exit Sub
            End If
            If listHomologation.Count > 1 Then
                Dim formulario As New PopupHomologation
                formulario.OnlySelectedOne = True
                AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.ListHomologation = listHomologation
                Dim transParent As New FrmTransparent(formulario, False)
                transParent.ShowDialog(Me)
            End If

            runCalculationValue = True
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener las homologaciones que tiene el cups para saber que servicio ips se debe utilizar si SOAT o ISS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHomologationCups()
        ''valido que los campos esten llenos para poder ejecutar la consulta
        If INDSleCareGroup.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDSleServiceCups.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDDteDate.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDSlePerformsFuntionalUnit.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDGleSpecialtyPerformsHealthProfessional.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If
        If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue Is Nothing Then
            runCalculationValue = False
            Exit Sub
        End If

        listHomologation = Nothing
        runCalculationValue = False
        Using model As New MServiceOrder(Me.Tag)
            AsyncLoader(True)

            Dim TempDescriptionId As Integer? = Nothing
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue IsNot Nothing Then
                Dim info = model.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                TempDescriptionId = info.ContractDescriptionId.Id
            End If

            'consulto las homologaciones que tiene el cups
            Dim result = model.GetHomologationCups(INDSleCareGroup.EditValue, cupsEntity.Id, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, 0, 0, Nothing, TempDescriptionId)
            If result.StateResult = False Then
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                CleanControlsErrorValueFields()
                Exit Sub
            End If
            AsyncLoader(False)
            'If result.ObjectEmbbeded.Where(Function(x) x.Presentation = 3).ToList().Count = 0 Then
            '    Mensaje(EeventViewerImages.Advertencia) = String.Format("No se Encontraron Homologos para el item {0} para el grupo de atención {1}", result.ObjectEmbbeded(0).CodeNameCupsEntity, INDSleCareGroup.Text)
            '    CleanControlsErrorValueFields()
            '    Exit Sub
            'End If
            listHomologation = result.ObjectEmbbeded '.Where(Function(x) x.Presentation = 3).ToList()
            'si el listado de las homologacione es mayor a 1 se muestra un popup con el listado para que el usuario seleccione cuales quiere cobrar
            If listHomologation.Count > 1 Then
                Dim formulario As New PopupHomologation
                AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.ListHomologation = listHomologation
                Dim transParent As New FrmTransparent(formulario, False)
                transParent.ShowDialog(Me)
            End If
            'marco como verdadera la bandera para que se haga el calculo
            runCalculationValue = True
        End Using
    End Sub

    ''' <summary>
    ''' metodo que obtiene el manual tarifario para saber si se debe utilizar SOAT o ISS en la busqueda de homologaciones del CUPS
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GetRateManualType(RateManualId As Integer) As Task(Of Integer)
        Dim rateManual As RateManual
        Using model As New MRateManual(Me.Tag)
            Dim result = Await model.GetRateManualById(RateManualId)
            rateManual = result.ObjectEmbbeded
        End Using
        Return rateManual.Type
    End Function

    ''' <summary>
    ''' metodo que obtiene el manual tarifario para saber si se debe utilizar SOAT o ISS en la busqueda de homologaciones del CUPS
    ''' </summary>
    ''' <param name="RateManualId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function GetRateManual(RateManualId As Integer) As Task(Of RateManual)
        Dim rateManual As RateManual
        Using model As New MRateManual(Me.Tag)
            Dim result = Await model.GetRateManualById(RateManualId)
            rateManual = result.ObjectEmbbeded
        End Using
        Return rateManual
    End Function

    ''' <summary>
    ''' metodo para obtener el valor del servicio cuando se llenen los campos requeridos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetServiceValue()
        'valido que los campos requeridos esten diligenciados
        Dim serviceOrderDetail As New ServiceOrderDetail
        If INDSleCareGroup.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleServiceSoatIss.EditValue Is Nothing Then
                Exit Sub
            End If
        Else
            If INDSleServiceCups.EditValue Is Nothing Then
                Exit Sub
            End If
        End If
        If INDDteDate.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDSlePerformsFuntionalUnit.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDGleSpecialtyPerformsHealthProfessional.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue Is Nothing Then
            Exit Sub
        End If

        Using model As New MServiceOrder(Me.Tag)
            'consulto el valor del servicio con los parametros requeridos
            AsyncLoader(True)
            errorsGetValue = String.Empty

            Dim TempDescriptionId As Integer? = Nothing
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue IsNot Nothing Then
                Dim info = model.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                TempDescriptionId = info.ContractDescriptionId.Id
            End If

            Dim result = model.GetServiceValue(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), listHomologation, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id, Nothing, TempDescriptionId)
            If result.StateResult = False Then
                AsyncLoader(False)
                errorsGetValue = result.Message
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDTxtAuthorizationNumber.EditValue = Nothing
                INDGleLiquidationType.EditValue = 1
                INDGleSurchargeApply.EditValue = 0
                Me.TotalSalesPrice = 0
                INDSeIndividualDiscount.EditValue = 0
                ActivateControlsBehavior = False
                Exit Sub
            End If

            listServiceOrderDetailPopup = result.ObjectEmbbeded
            If listServiceOrderDetailPopup.Count > 1 Then
                'si el listado de los servicios es mayor a uno le asigno el listado al control de navegacion de la barra botones para poder cambiar de registro
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "CodeNameIpsService"}, New ColumnInfo With {.Caption = "CUPS", .FieldName = "CodeNameCups"}}.ToList()
                Me.BarraBotones.FilterDataSource = listServiceOrderDetailPopup
            Else
                'cargo los controles con el unico registro que se retorno

                If listServiceOrderDetailPopup Is Nothing OrElse Not listServiceOrderDetailPopup.Any() Then
                    AsyncLoader(False)
                    errorsGetValue = result.Message
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontró órden de servicio"
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTxtAuthorizationNumber.EditValue = Nothing
                    INDGleLiquidationType.EditValue = 1
                    INDGleSurchargeApply.EditValue = 0
                    Me.TotalSalesPrice = 0
                    INDSeIndividualDiscount.EditValue = 0
                    ActivateControlsBehavior = False
                    Exit Sub
                End If

                LoadControls(listServiceOrderDetailPopup(0))
            End If
            AsyncLoader(False)
            'activo los controles del grupo del grupo de comportamiento
            ActivateControlsBehavior = True
            'INDGleSurgicalInterventionType.Properties.Buttons(1).Visible = False
            'si el grupo de cirugia esta visible asigno el foco al campo del evento sino al numero de autorizacion
            INDTxtAuthorizationNumber.Focus()

        End Using
    End Sub

    ''' <summary>
    ''' metodo para cargar controles con el registro seleccionado
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Record As ServiceOrderDetail)
        serviceOrderDetail = Record
        With serviceOrderDetail
            'esta bandera se hace para que no se haga nada en los eventos editValueChanged de los controles mientras se asignan desde este metodo
            editValueChangeLoadControl = True
            INDSleCareGroup.EditValue = .CareGroupId
            .CodeNameCareGroup = INDSleCareGroup.Text
            .CodeNameFunctionalUnit = INDSlePerformsFuntionalUnit.Text
            If listHomologation IsNot Nothing AndAlso listHomologation.Count > 1 Then
                INDGleServiceType.EditValue = 3
                INDSleServiceCups.EditValue = .CUPSEntityId
                If CupsXPO Is Nothing Then
                    INDSleServiceCups.Properties.NullText = .CodeNameCups
                End If
            Else
                If INDGleServiceType.EditValue = 3 Then
                    If INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        INDSleServiceSoatIss.EditValue = .IPSServiceId
                    Else
                        INDSleServiceCups.EditValue = .CUPSEntityId
                    End If
                End If
            End If
            If _healthAdministratorId IsNot Nothing Then
                Using modelHealth As New MHealthAdministrator(Me.Tag)
                    Dim administrator = modelHealth.GetHealthAdministratorByIdSimple(_healthAdministratorId).ObjectEmbbeded
                    .HealthAdministratorId = _healthAdministratorId
                    .ThirdPartyId = administrator.ThirdPartyId
                End Using
            Else
                .ThirdPartyId = _thirdPartyId
            End If


            Me.Quantity = .InvoicedQuantity
            INDDteDate.EditValue = .ServiceDate
            INDSlePerformsFuntionalUnit.EditValue = .PerformsFunctionalUnitId
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSLeCostCenter.EditValue = .CostCenterId
            INDSLeCostCenter.Properties.NullText = .CodeNameCostCenter
            INDSlePerformsHealthProfessionalCode.EditValue = .PerformsHealthProfessionalCode
            INDGleSpecialtyPerformsHealthProfessional.EditValue = .PerformsProfessionalSpecialty
            'si se permite cambiar el valor del servicio activo o inactivo el campo
            If .AllowValueChange = True Then
                INDTxtValue.Properties.ReadOnly = False
                INDRpTxtValue.ReadOnly = False
            Else
                INDTxtValue.Properties.ReadOnly = True
                INDRpTxtValue.ReadOnly = True
            End If

            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.TotalSalesPrice = .SubTotalSalesPrice

            INDTxtAuthorizationNumber.EditValue = .AuthorizationNumber
            INDGleLiquidationType.EditValue = Nothing
            If .SettlementType = 0 Then
                INDGleLiquidationType.EditValue = 1
            Else
                INDGleLiquidationType.EditValue = .SettlementType
            End If

            INDSleIncludeService.EditValue = .IncludeServiceOrderDetailId
            INDSePercent.EditValue = .RecoveryRatio
            INDGleSurchargeApply.EditValue = .SurchargeApply
            INDSeIndividualDiscount.EditValue = .ThirdPartyDiscountPercentage

            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CUPSEntityContractDescriptionId = INDsleDescription.EditValue
                .ContractDescriptionCodeName = INDsleDescription.Text
            End If

            editValueChangeLoadControl = False
            BarraBotones.Focus()
            'obtengo el valor de todos los items para ponerlo en el control de usuario de la barra botones

            _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
            ctrTmp.PrintInfo()
        End With
    End Sub

    ''' <summary>
    ''' metodo para obtener el servicio ips de materiales de sutura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetIPSSutureMaterials(surgicalProcedureService As SurgicalProcedureService)
        Using modelIpsService As New MIPSService(Me.Tag)
            Dim ipsRightRoom = modelIpsService.GetIPSServiceByIdSimple(surgicalProcedureService.IPSServiceId).ObjectEmbbeded
            If ipsRightRoom.AssociatedMaterialIPSServiceId IsNot Nothing Then
                'consulto el ips para el material de sutura
                Dim ipsSutureMaterials = modelIpsService.GetIPSServiceByIdSimple(ipsRightRoom.AssociatedMaterialIPSServiceId).ObjectEmbbeded
                Dim procedureServiceTpm As New SurgicalProcedureService
                With procedureServiceTpm
                    .IPSServiceParentId = surgicalProcedureService.IPSServiceParentId
                    .IPSServiceId = ipsSutureMaterials.Id
                    .ServiceAmount = 1
                    .DefaultService = True
                    .ValueItemServiceOrderDetail = 0
                    .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                    .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                End With
                listSurgicalProcedureService.Add(procedureServiceTpm)
            End If
            Dim indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
            listServiceOrderDetailPopup.Remove(serviceOrderDetail)
            Using model As New MServiceOrder(Me.Tag)
                listSurgicalProcedureService.ForEach(Sub(x) x.ValueItemServiceOrderDetail = 0)
                listSurgicalProcedureService.ForEach(Sub(x) x.PerformsHealthProfessionalCode = Nothing)
                'obetengo todos los valores por defecto 
                Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                'consulto el valor enviando como parametro el listado de los valores por defecto y el detalle que se esta trabajando
                Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureDefault)
                If result.StateResult = True Then
                    serviceOrderDetail = result.ObjectEmbbeded
                    'asigno los nuevos valores al listado del detalle quirurgico
                    For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                        listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                        listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                    Next
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    'consulto los detalles del servicio ips cuando es quirurgico
                    listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)
                    Dim ipsSuture = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("RightRoom", "Contract") And x.DefaultService = True)
                    ipsRightRoom = modelIpsService.GetIPSServiceByIdSimple(ipsSuture.IPSServiceId).ObjectEmbbeded
                    If ipsRightRoom.AssociatedMaterialIPSServiceId IsNot Nothing Then
                        'consulto el ips para el material de sutura
                        Dim ipsSutureMaterials = modelIpsService.GetIPSServiceByIdSimple(ipsRightRoom.AssociatedMaterialIPSServiceId).ObjectEmbbeded
                        Dim procedureServiceTpm As New SurgicalProcedureService
                        With procedureServiceTpm
                            .IPSServiceParentId = serviceOrderDetail.IPSServiceId
                            .IPSServiceId = ipsSutureMaterials.Id
                            .ServiceAmount = 1
                            .DefaultService = True
                            .ValueItemServiceOrderDetail = 0
                            .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                            .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                        End With
                        listSurgicalProcedureService.Add(procedureServiceTpm)
                    End If
                    Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                    result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalDefault)

                    serviceOrderDetail = result.ObjectEmbbeded

                    'los detalles que estan como por defecto le asigno los valores de la entidad service order detail surgical que trae los valores calculado para cada item
                    'esto se hace porque el listado con el que se hace datasource a la rejilla es de otro tipo al que retorna el calculo del servicio
                    For i As Integer = 0 To listSurgicalDefault.Count - 1 Step 1
                        listSurgicalDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                        listSurgicalDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                    Next
                    INDGcSurgery.DataSource = listSurgicalProcedureService
                    INDGvSurgery.ExpandAllGroups()
                End If
            End Using
            'inserto el item en la misma posicion qe estaba antes de ser eliminado
            listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para colocar el valor del servicio en el control de usuario de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintServiceValue()
        If listServiceOrderDetailPopup Is Nothing Then
            _serviceValue = 0
        Else
            _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice * x.InvoicedQuantity)
        End If
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo para calcular el descuento del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetIndividualDiscount()
        'se pone esta bandera en true para que no se haga nada en el editValueChanged del TxtValue
        _changeValue = True
        If INDSeIndividualDiscount.EditValue > 0 Then
            Dim round = 1
            If serviceOrderDetail.RateManualId IsNot Nothing Then
                Using model As New MRateManual(Me.Tag)
                    round = model.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded.RoundService
                End Using
            End If
            Dim percent As Decimal
            'si el tipo de liquidacion es por un porcentaje de otro servicio o en el mismo
            If INDGleLiquidationType.EditValue = 2 Or INDGleLiquidationType.EditValue = 4 Then
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice

                percent = Math.Round(serviceOrderDetail.SubTotalSalesPrice * INDSeIndividualDiscount.EditValue / 100, 0)
            Else
                If serviceOrderDetail.AllowValueChange = False Then
                    'sino permite cambiar el valor siempre tomo el RateManualSalePrice para hacer los calculos
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.RateManualSalePrice
                Else
                    'si permite cambiar el valor tomo lo que este en el campo de valor para hacer los calculos
                    serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                End If
                percent = Math.Round(serviceOrderDetail.TotalSalesPrice * INDSeIndividualDiscount.EditValue / 100, 0)
            End If
            serviceOrderDetail.TotalSalesPrice = Utils.RoundValue(serviceOrderDetail.TotalSalesPrice - percent, round)
            serviceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue((serviceOrderDetail.SubTotalSalesPrice - percent) * serviceOrderDetail.InvoicedQuantity, round)
            serviceOrderDetail.ThirdPartyDiscountPercentage = INDSeIndividualDiscount.EditValue
            serviceOrderDetail.ThirdPartyDiscount = percent
        Else
            If serviceOrderDetail IsNot Nothing Then
                If INDGleLiquidationType.EditValue = 2 Or INDGleLiquidationType.EditValue = 4 Then
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                Else
                    serviceOrderDetail.TotalSalesPrice += serviceOrderDetail.ThirdPartyDiscount
                End If
                serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                serviceOrderDetail.ThirdPartyDiscount = 0
            End If
        End If
        _changeValue = False
    End Sub

    ''' <summary>
    ''' metodo para validar el registro antes de agregarlo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count > 1 Then

        Else
            If INDSleCareGroup.EditValue Is Nothing Then
                errors.AppendLine(INDLciCareGroup.Text + ResourceManager.GetString("Empty"))
            End If
            If INDGleServiceType.EditValue Is Nothing Then
                errors.AppendLine(INDLciServiceType.Text + ResourceManager.GetString("Empty"))
            End If
            If INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDSleServiceSoatIss.EditValue Is Nothing Then
                    errors.AppendLine(INDLciServiceSoatIss.Text + ResourceManager.GetString("Empty"))
                End If
            Else
                If INDSleServiceCups.EditValue Is Nothing Then
                    errors.AppendLine(INDLciServiceCups.Text + ResourceManager.GetString("Empty"))
                End If
            End If
            If INDDteDate.EditValue Is Nothing Then
                errors.AppendLine(INDLciDate.Text + ResourceManager.GetString("Empty"))
            End If
            If INDSlePerformsFuntionalUnit.EditValue Is Nothing Then
                errors.AppendLine(INDLciPerformsFuntionalUnit.Text + ResourceManager.GetString("Empty"))
            End If
            If INDSlePerformsHealthProfessionalCode.EditValue Is Nothing Then
                errors.AppendLine(INDLciPerformsHealthProfessionalCode.Text + ResourceManager.GetString("Empty"))
            End If

            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleDescription.EditValue Is Nothing Then
                    errors.AppendLine(INDlyItemDescription.Text + ResourceManager.GetString("Empty"))
                End If
            End If

            If INDTxtAuthorizationNumber.EditValue Is Nothing Then
                errors.AppendLine(INDLciAuthorizationNumber.Text + ResourceManager.GetString("Empty"))
            End If
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para cargar los controles cuando se va a editar un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControlsForEdit()
        INDSleIncludeService.Properties.DataSource = Nothing
        INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        INDBtnAddDetail.Text = ResourceManager.GetString("Edit")
        With serviceOrderDetail
            'asigno valores a los campos y los pongo readOnly
            INDSleCareGroup.EditValue = .CareGroupId
            INDSleCareGroup.Properties.NullText = .CodeNameCareGroup
            INDSleCareGroup.Properties.ReadOnly = True
            _flagHomologation = True
            INDGleServiceType.EditValue = 3
            _flagHomologation = False
            INDGleServiceType.Properties.ReadOnly = True
            INDSleServiceCups.EditValue = .CUPSEntityId
            INDSleServiceCups.Properties.NullText = .CodeNameCups
            INDSleServiceCups.Properties.ReadOnly = True
            Me.Quantity = .InvoicedQuantity
            INDSeCount.Properties.ReadOnly = True
            INDDteDate.EditValue = .ServiceDate
            INDDteDate.Properties.ReadOnly = True
            INDSlePerformsFuntionalUnit.Properties.NullText = .CodeNameFunctionalUnit
            INDSlePerformsFuntionalUnit.EditValue = .PerformsFunctionalUnitId
            'dependiendo del tipo de liquidacion que se hizo se habilitan los conmtroles
            '1 tipo de unidad - 2 Unidad funcional
            If .LiquidationType = 1 Or .LiquidationType = 3 Then
                INDSlePerformsFuntionalUnit.Properties.ReadOnly = True
            Else
                INDSlePerformsFuntionalUnit.Properties.ReadOnly = False
            End If
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSLeCostCenter.Properties.NullText = .CodeNameCostCenter
            '2 especialidad - si el tipo de liquidacion es 2 se pone readOnly el controls
            If .LiquidationType = 2 Then
                INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = True
                INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = True
            Else
                INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = False
                INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = False
            End If
            INDSlePerformsHealthProfessionalCode.EditValue = .PerformsHealthProfessionalCode
            INDGleSpecialtyPerformsHealthProfessional.EditValue = .PerformsProfessionalSpecialty
            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtAuthorizationNumber.EditValue = .AuthorizationNumber
            INDGleLiquidationType.EditValue = Nothing
            INDGleLiquidationType.EditValue = .SettlementType
            'INDGleLiquidationType.Properties.ReadOnly = True
            INDSleIncludeService.EditValue = .IncludeServiceOrderDetailId
            INDSleIncludeService.Properties.ReadOnly = True
            INDSePercent.EditValue = .RecordType
            INDSePercent.Properties.ReadOnly = True
            INDGleSurchargeApply.EditValue = .SurchargeApply
            INDGleSurchargeApply.Properties.ReadOnly = True
            Me.TotalSalesPrice = .TotalSalesPrice + .ThirdPartyDiscount
            INDTxtValue.Properties.ReadOnly = True
            INDSeIndividualDiscount.EditValue = .ThirdPartyDiscountPercentage
            INDSeIndividualDiscount.Properties.ReadOnly = True

            INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If .CUPSEntityContractDescriptionId IsNot Nothing AndAlso .CUPSEntityContractDescriptionId > 0 Then
                INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleDescription.EditValue = .CUPSEntityContractDescriptionId
                INDsleDescription.Text = .ContractDescriptionCodeName
            End If

            _serviceValue = .SubTotalSalesPrice
            ctrTmp.PrintInfo()
        End With
    End Sub

    ''' <summary>
    ''' metodo para mostrar los formulario en el evento buttonclik
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    Private Sub OpenPopupValue(sender As Object, e As CancelEventArgs)
        If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count > 1 Then
            INDGcHomologation.DataSource = Nothing
            INDGcHomologation.DataSource = listServiceOrderDetailPopup
        Else
            e.Cancel = True
        End If
    End Sub

    Private Function GetDetailsIncludedService() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MServiceOrder(Me.Tag)
                                             _listServiceOrderDetailDatasourceIncludeService.AddRange(model.ListServiceOrderDetailsByAdmissionNumber(Admission))
                                         End Using
                                     End Sub)
    End Function
#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        cupsEntity = Nothing
        listHomologation = Nothing
        runCalculationValue = Nothing
        listServiceOrderDetailPopup = Nothing
        serviceOrderDetail = Nothing
        _serviceValue = Nothing
        _patientDate = Nothing
        _patienGenus = Nothing
        listSurgicalProcedureService = Nothing
        _changeValue = Nothing
        _editMode = Nothing
        errorsGetValue = Nothing
        thirdParty = Nothing
        editValueChangeLoadControl = Nothing
        cleaningControls = Nothing
        _flagHomologation = Nothing
        healthProfessional = Nothing
    End Sub

    Private Async Sub FrmPopupServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'INDSleIncludeService.Properties.Buttons(1).Visible = False
        'INDSlePerformsHealthProfessionalCode.Properties.Buttons(1).Visible = False
        AsyncLoader(True)
        ctrTmp.PopupContainerControl = INDPccMoreInfoAdminssion
        ctrTmp.PopupContainerControlValue = INDPccHomologation
        AddHandler ctrTmp.OpenPopupValue, AddressOf OpenPopupValue
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        SetDataSourceCombo()
        ctrTmp.PrintInfo()

        Using model As New MServiceOrder(Me.Tag)
            CareGroupXPO = model.ListCareGroup()
        End Using

        Using model As New MServiceOrder(Me.Tag)
            PerformsHealthProfessionalXPO = model.ListHealthCareProfessional()
        End Using

        If _editMode = True Then
            LoadControlsForEdit()
        Else
            CleanControls()
        End If

        Await loadSettingBilling()
        INDSleCareGroup.Properties.ReadOnly = True
    End Sub

    Private Async Function loadSettingBilling() As Task
        If _settingBilling Is Nothing Then
            Using Model As New MCtrFolio()
                _settingBilling = (Await Model.GetSettingsBillingByIdUnitOperative(Me.OperativeUnitId)).ObjectEmbbeded
            End Using
        End If
        If _settingBilling IsNot Nothing AndAlso _settingBilling.ValidatePackaging Then
            INDGleServiceType.Properties.ReadOnly = True
            INDLciServiceType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciControlPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDGleServiceType.EditValue = 3
        End If
        AsyncLoader(False)
    End Function
#End Region

#Region "Activated"

    Public Overrides Sub AsyncLoader(State As Boolean)
        MyBase.AsyncLoader(State)
        INDBtnAddDetail.Enabled = Not State
    End Sub

    Private Sub FrmPopupServices_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDSleCareGroup.EditValue Is Nothing Then
            INDSleCareGroup.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"

    Dim forceClosing As Boolean = False
    Private Sub FrmPopupServices_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If INDSleCareGroup.EditValue IsNot Nothing And cleaningControls = True Then
        '    If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        e.Cancel = True
        '    End If
        'ElseIf INDSleServiceSoatIss.EditValue IsNot Nothing Or INDSleServiceCups.EditValue IsNot Nothing Then
        '    If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        e.Cancel = True
        '    End If
        'End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDsleContractPackage_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleContractPackage.QueryPopUp
        If INDsleContractPackage.Properties.DataSource Is Nothing Then
            INDsleContractPackage.Properties.DataSource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).ContractService.ListContractPackage(CareGroupParentId, True)
        End If
    End Sub


    Private Sub INDsleDescription_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDescription.QueryPopUp
        If INDsleDescription.Properties.DataSource Is Nothing AndAlso INDSleServiceCups.EditValue IsNot Nothing Then
            Using srvOrderModel As New MServiceOrder("")
                INDsleDescription.Properties.DataSource = srvOrderModel.ListContractDescriptionsByCupsEntityId(INDSleServiceCups.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDSLeCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeCostCenter.QueryPopUp
        If CostCenterXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                CostCenterXPO = model.GetCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If CareGroupXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                CareGroupXPO = model.ListCareGroup()
            End Using
        End If
    End Sub

    Private Sub INDSleServiceSoatIss_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleServiceSoatIss.QueryPopUp
        If SoatIssXPO Is Nothing AndAlso Not INDSleServiceSoatIss.ReadOnly Then
            Using model As New MServiceOrder(Me.Tag)
                If INDGleServiceType.EditValue = 2 Then
                    SoatIssXPO = model.ListIssServicesByCareGroupAndPresentation(INDSleCareGroup.EditValue, 3) 'Presentacion: Paquete
                Else
                    SoatIssXPO = model.ListSoatByCareGroupAndPresentation(INDSleCareGroup.EditValue, 3) 'Presentacion: Paquete
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleServiceCups_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleServiceCups.QueryPopUp
        If CupsXPO Is Nothing AndAlso Not INDSleServiceCups.ReadOnly Then
            Using model As New MServiceOrder(Me.Tag)
                CupsXPO = model.ListCUPS(INDSleCareGroup.EditValue)
            End Using
        End If
    End Sub

    Private Async Sub INDSleIncludeService_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleIncludeService.QueryPopUp
        If INDSleIncludeService.Properties.DataSource Is Nothing Then
            INDGvSleIncludedService.ShowLoadingPanel()
            Await GetDetailsIncludedService()
            INDGvSleIncludedService.HideLoadingPanel()
            'INDSleIncludeService.Properties.DataSource = Nothing
            INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService

        Else
            INDGvSleIncludedService.ShowLoadingPanel()
            'INDSleIncludeService.Properties.DataSource = Nothing

            INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService
            INDGvSleIncludedService.HideLoadingPanel()
        End If
    End Sub

    Private Sub INDSlePerformsFuntionalUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSlePerformsFuntionalUnit.QueryPopUp
        If PerformsFuntionalUnitXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                PerformsFuntionalUnitXPO = model.ListFunctionalUnit()
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDsleContractPackage_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleContractPackage.EditValueChanged
        If INDsleContractPackage.EditValue IsNot Nothing Then
            Dim package = DirectCast(INDsleContractPackage.GetSelectedObject(), ContractPackageXpo)

            If package IsNot Nothing Then
                'INDSleServiceCups.Properties.NullText = package.CUPSEntityId.CodeDescription
                'INDSleServiceCups.EditValue = package.CUPSEntityId.Id
                'INDSleServiceCups.Properties.ReadOnly = True

                ' 1 - soat, 2 - iss
                If package.IPSServiceId.ServiceManual <= 2 Then
                    INDGleServiceType.EditValue = 2
                ElseIf package.IPSServiceId.ServiceManual = 3 Then
                    INDGleServiceType.EditValue = 1
                End If

                INDSleServiceSoatIss.Properties.NullText = package.IPSServiceId.CodeName
                INDSleServiceSoatIss.EditValue = package.IPSServiceId.Id
                INDSleServiceSoatIss.Properties.ReadOnly = True
            End If
        End If
    End Sub


    Private Sub INDsleDescription_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDescription.EditValueChanged
        If INDsleDescription.EditValue IsNot Nothing AndAlso editValueChangeLoadControl = False Then
            GetHomologationCups()
            If runCalculationValue Then
                GetServiceValue()
            End If
        End If
    End Sub

    Private Sub INDSleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareGroup.EditValueChanged
        SoatIssXPO = Nothing
        CupsXPO = Nothing

        If editValueChangeLoadControl = False Then
            HideShowFieldsDescription()
        End If

        If INDSleCareGroup.EditValue IsNot Nothing Then
            ' si se esta editando me salgo del evento
            If _editMode = True Then
                Exit Sub
            End If
            'obtengo el grupo de atencion seleccionado
            Dim careGroupTmp As CareGroup
            Using model As New MCareGroup(Me.Tag)
                careGroupTmp = model.GetCareGroupByIdSimple(CareGroupParentId).ObjectEmbbeded 'DirectCast(DirectCast(INDGvSleCareGroup.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ContractCareGroupXpo)
            End Using

            INDGleServiceType.EditValue = careGroupTmp.DefaultManual
            INDGleServiceType.Enabled = True
            If careGroupTmp.DefaultManual <= 2 Then
                INDSleServiceSoatIss.Focus()
            Else
                INDSleServiceCups.Focus()
            End If
            'dependiendo del tipo de servicio consulto las homologaciones
            If INDGleServiceType.EditValue = 3 Then
                GetHomologationCups()
            Else
                GetHomologationIssSoat()
            End If
            ' esta bandera se activa en los metodos de consultar homologacione si todo se hizo bien
            If runCalculationValue Then
                GetServiceValue()
            End If
        Else
            INDGleServiceType.Enabled = False
        End If
    End Sub

    Private Sub INDGleServiceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleServiceType.EditValueChanged
        SoatIssXPO = Nothing
        CupsXPO = Nothing
        INDSleServiceSoatIss.EditValue = Nothing
        INDSleServiceSoatIss.Properties.NullText = String.Empty
        INDSleServiceSoatIss.Properties.ReadOnly = False
        INDSleServiceCups.EditValue = Nothing
        INDSleServiceCups.Properties.NullText = String.Empty
        INDSleServiceCups.Properties.ReadOnly = False
        If _flagHomologation = False Then
            CleanControlsErrorValueFields()
        End If
        If INDGleServiceType.EditValue IsNot Nothing Then
            If INDGleServiceType.EditValue <= 2 Then
                INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciServiceCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciServiceCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        Else
            INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciServiceCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDRpTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDRpTxtValue.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        'si la el editvalue se cambia desde el load controls me salgo del evento
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        Dim serviceOrderDetailSurgicalTmp = DirectCast(INDGvSurgery.GetFocusedRow(), SurgicalProcedureService)
        Dim value = DirectCast(sender, TextEdit)
        serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = serviceOrderDetailSurgicalTmp.IPSServiceId).FirstOrDefault().TotalSalesPrice = value.EditValue
        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
        serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
        GetIndividualDiscount()
        _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
        ctrTmp.PrintInfo()
    End Sub

    Private Async Sub INDSleServiceCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleServiceCups.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If INDSleServiceCups.EditValue IsNot Nothing Then
            'ActivateControls = True
            Using model As New MCupsEntity(Me.Tag)
                Dim result = Await model.GetCupsEntityById(INDSleServiceCups.EditValue)
                cupsEntity = result.ObjectEmbbeded
            End Using
        End If
        HideShowFieldsDescription()
        GetHomologationCups()
        If runCalculationValue Then
            GetServiceValue()
        End If
    End Sub

    Private Sub INDGleSpecialtyPerformsHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSpecialtyPerformsHealthProfessional.EditValueChanged
        If _editMode = False Then
            If INDGleServiceType.EditValue = 3 Then
                GetHomologationCups()
            Else
                GetHomologationIssSoat()
            End If
            If runCalculationValue Then
                GetServiceValue()
            End If
        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.PerformsProfessionalSpecialty = INDGleSpecialtyPerformsHealthProfessional.EditValue
            End If
        End If
    End Sub

    Private Sub INDSleServiceSoatIss_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleServiceSoatIss.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If INDSleServiceSoatIss.EditValue IsNot Nothing Then
            GetHomologationIssSoat()
        End If
        If runCalculationValue Then
            GetServiceValue()
        End If
    End Sub

    Private Sub INDDteDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDate.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If INDGleServiceType.EditValue = 3 Then
            GetHomologationCups()
        Else
            GetHomologationIssSoat()
        End If
        If runCalculationValue Then
            GetServiceValue()
        End If

    End Sub

    Private Sub INDSlePerformsFuntionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePerformsFuntionalUnit.EditValueChanged
        FunctionalUnitCenterAttentionCode = Nothing
        If INDSlePerformsFuntionalUnit.EditValue IsNot Nothing Then
            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                Dim functionalUnitTemp = modelServiceOrder.GetFunctionalUnitById(INDSlePerformsFuntionalUnit.EditValue)
                FunctionalUnitCenterAttentionCode = functionalUnitTemp.BranchOfficeId.Code
            End Using
        End If

        If _editMode = False Then
            If INDGleServiceType.EditValue = 3 Then
                GetHomologationCups()
            Else
                GetHomologationIssSoat()
            End If
            If runCalculationValue Then
                GetServiceValue()
            End If
        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.PerformsFunctionalUnitId = INDSlePerformsFuntionalUnit.EditValue
                serviceOrderDetail.CodeNameFunctionalUnit = IIf(INDSlePerformsFuntionalUnit.Text Is String.Empty, INDSlePerformsFuntionalUnit.Properties.NullText, INDSlePerformsFuntionalUnit.Text)
            End If
        End If
    End Sub

    Private Sub INDGleLiquidationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleLiquidationType.EditValueChanged
        If serviceOrderDetail IsNot Nothing Then
            serviceOrderDetail.SettlementType = INDGleLiquidationType.EditValue
            Select Case INDGleLiquidationType.EditValue
                Case 1 ' Por manual de tarifas (Defecto)
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If editValueChangeLoadControl = False Then
                        If serviceOrderDetail.AllowValueChange = True Then
                            INDTxtValue.Properties.ReadOnly = False
                        Else
                            INDTxtValue.Properties.ReadOnly = True
                        End If
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = True
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.RateManualSalePrice
                        If serviceOrderDetail.AllowValueChange = True Then
                            If serviceOrderDetail.Presentation = 2 Then
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                            Else
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                            End If
                        Else
                            serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.RateManualSalePrice
                        End If
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
                Case 2 ' % de otro servicio cargado
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If editValueChangeLoadControl = False Then
                        INDSleIncludeService.EditValue = Nothing
                        INDSePercent.Enabled = False
                        INDTxtValue.Properties.ReadOnly = True
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = False
                        serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.RateManualSalePrice
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
                Case 3 '100% incluido dentro de otro servicio (No se cobra nada)
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    If editValueChangeLoadControl = False Then
                        INDSleIncludeService.EditValue = Nothing
                        INDTxtValue.Properties.ReadOnly = True
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = False
                        serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = 0 'serviceOrderDetail.RateManualSalePrice
                        serviceOrderDetail.GrandTotalSalesPrice = 0
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
                Case 4 'porcentage del mismo servicio
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If editValueChangeLoadControl = False Then
                        INDSleIncludeService.EditValue = Nothing
                        INDSePercent.Enabled = True
                        INDTxtValue.Properties.ReadOnly = True
                        INDSePercent.EditValue = 100
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = True
                        'serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.RateManualSalePrice
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
            End Select
        End If

    End Sub

    Private Sub INDSleIncludeService_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleIncludeService.EditValueChanged
        If INDSleIncludeService.EditValue IsNot Nothing AndAlso INDSleIncludeService.EditValue IsNot String.Empty Then
            If editValueChangeLoadControl = False Then
                INDSePercent.Enabled = True
                Me.TotalSalesPrice = 0
                Dim percentTmp = INDSePercent.EditValue
                INDSePercent.EditValue = 0
                INDSePercent.EditValue = percentTmp
                INDSeIndividualDiscount.Enabled = True
                If INDGleLiquidationType.EditValue = 3 Or INDGleLiquidationType.EditValue = 2 Then
                    Dim serviceDetailTmp = DirectCast(INDGvSleIncludedService.GetFocusedRow, ServiceOrderDetail)
                    If serviceDetailTmp.Id > 0 Then
                        serviceOrderDetail.IncludeServiceOrderDetailId = serviceDetailTmp.Id
                    Else
                        serviceOrderDetail.ServiceOrderDetail2 = serviceDetailTmp
                    End If
                End If
            End If

        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.IncludeServiceOrderDetailId = Nothing
                serviceOrderDetail.ServiceOrderDetail2 = Nothing
                serviceOrderDetail.TotalSalesPrice = 0
                serviceOrderDetail.SubTotalSalesPrice = 0 'serviceOrderDetail.RateManualSalePrice
                serviceOrderDetail.RecoveryRatio = 0
                serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                serviceOrderDetail.ThirdPartyDiscount = 0

                INDSePercent.Enabled = False
                INDSePercent.EditValue = 0
                INDSeIndividualDiscount.EditValue = 0
                INDSeIndividualDiscount.Enabled = False
                _changeValue = True
                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                PrintServiceValue()
                _changeValue = False
            End If
        End If
    End Sub

    Private Sub INDSeIndividualDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeIndividualDiscount.EditValueChanged
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        GetIndividualDiscount()
        PrintServiceValue()
    End Sub

    Private Sub INDGleSurchargeApply_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSurchargeApply.EditValueChanged
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If serviceOrderDetail IsNot Nothing Then
            'obtengo el indice del item para luego agregarlo en la mismo posicion
            Dim indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
            listServiceOrderDetailPopup.Remove(serviceOrderDetail)
            serviceOrderDetail.SurchargeApply = INDGleSurchargeApply.EditValue
            Using model As New MServiceOrder(Me.Tag)
                serviceOrderDetail = model.GetServiceValueSurcharge(serviceOrderDetail)
            End Using
            If listSurgicalProcedureService IsNot Nothing Then
                'obtengo los items con valor por defecto
                Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                'le asigno los valores a los items por defecto que me retorno el metodo de obtener el valor con recargo
                For i As Integer = 0 To listSurgicalDefault.Count - 1 Step 1
                    listSurgicalDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                    listSurgicalDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                Next
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
            End If
            'inserto el registro que retorno el metodo en la mismo posicion que estaba
            listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
            _changeValue = True
            Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
            _changeValue = False
            GetIndividualDiscount()
            PrintServiceValue()
        End If
    End Sub

    Private Sub INDSeSurgeryNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeSurgeryNumber.EditValueChanged
        If _editMode = False Then
            If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.Presentation = 2 Then
                serviceOrderDetail.SurgeryNumber = CInt(INDSeSurgeryNumber.EditValue)
            End If
        End If
        Dim listEvent As New List(Of ServiceOrderDetail)
        If INDSeSurgeryNumber.EditValue IsNot Nothing Then
            If _listServiceOrderDetailSurgicalIntervention IsNot Nothing Then
                listEvent = _listServiceOrderDetailSurgicalIntervention.FindAll(Function(x) x.SurgeryNumber = INDSeSurgeryNumber.EditValue And x.Presentation = 2)
                INDGcEvents.DataSource = listEvent
                INDPceEvents.Text = listEvent.Count.ToString()
            End If
        End If
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = False Then
            If serviceOrderDetail IsNot Nothing Then
                If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count > 1 Then
                    serviceOrderDetail.SurgeryNumber = 0
                    Dim listEventTmp As New List(Of ServiceOrderDetail)(listEvent.ToArray())
                    listEventTmp.AddRange(listServiceOrderDetailPopup.FindAll(Function(x) x.SurgeryNumber = INDSeSurgeryNumber.EditValue And x.Presentation = 2))
                    If listEventTmp.Count > 0 Then
                        If listEventTmp(0).SurgicalInterventionType = 1 Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("BasicEvent", MODULE_NAME), INDSeSurgeryNumber.EditValue.ToString())
                            INDGleSurgicalInterventionType.EditValue = listEventTmp(0).SurgicalInterventionType
                            INDGleSurgicalInterventionType.Properties.ReadOnly = True
                            serviceOrderDetail.SurgeryNumber = CInt(INDSeSurgeryNumber.EditValue)
                            Exit Sub
                        Else
                            INDGleSurgicalInterventionType.EditValue = Nothing
                            INDGleSurgicalInterventionType.Properties.ReadOnly = False
                            serviceOrderDetail.IsFirstEvent = False
                        End If
                    Else
                        'INDGleSurgicalInterventionType.EditValue = Nothing
                        INDGleSurgicalInterventionType.Properties.ReadOnly = False
                        serviceOrderDetail.IsFirstEvent = True
                    End If
                    serviceOrderDetail.SurgeryNumber = CInt(INDSeSurgeryNumber.EditValue)
                Else
                    If listEvent.Count > 0 Then
                        If listEvent(0).SurgicalInterventionType = 1 Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("BasicEvent", MODULE_NAME), INDSeSurgeryNumber.EditValue.ToString())
                            INDGleSurgicalInterventionType.EditValue = listEvent(0).SurgicalInterventionType
                            INDGleSurgicalInterventionType.Properties.ReadOnly = True
                            Exit Sub
                        Else
                            'INDGleSurgicalInterventionType.EditValue = Nothing
                            INDGleSurgicalInterventionType.Properties.ReadOnly = False
                            serviceOrderDetail.IsFirstEvent = False
                        End If
                    Else
                        'INDGleSurgicalInterventionType.EditValue = Nothing
                        INDGleSurgicalInterventionType.Properties.ReadOnly = False
                        serviceOrderDetail.IsFirstEvent = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub INDGleSurgicalInterventionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSurgicalInterventionType.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If serviceOrderDetail IsNot Nothing Then
            serviceOrderDetail.SurgicalInterventionType = CInt(INDGleSurgicalInterventionType.EditValue)
            If editValueChangeLoadControl = False Then
                If INDGleSurgicalInterventionType.EditValue IsNot Nothing Then
                    Dim indexItem As Integer
                    If INDGleSurgicalInterventionType.EditValue = 9 Then
                        If serviceOrderDetail.RateManualId Is Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = "El servicio se liquido con tarifa fija y solo puede ser básico"
                            INDGleSurgicalInterventionType.EditValue = 1
                            Exit Sub
                        End If
                        'obtengo el item de materiales de sutura si existe
                        Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("SutureMaterials", "Contract"))
                        'si existe un item para materiales lo elimino
                        If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                            serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault())
                            listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                        End If
                        'consulto el manual de tarifas
                        Dim rateManual As RateManual
                        Using model As New MRateManual(Me.Tag)
                            rateManual = model.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded
                        End Using
                        Using model As New MIPSService(Me.Tag)
                            'obtengo el servicio ips para materiales de sutura no cruentos
                            Dim ipsSutureMaterials = model.GetIPSServiceByIdSimple(rateManual.MaterialNoBloodyIPSServiceId).ObjectEmbbeded
                            Using modelService As New MServiceOrder(Me.Tag)
                                Dim listHomologationTmp = modelService.ListCupsHomologationByIpsServiceId(ipsSutureMaterials.Id)
                                Dim result = modelService.GetServiceValue(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), listHomologationTmp, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id)
                                If result.StateResult = False Then
                                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                                    INDGleSurgicalInterventionType.EditValue = 1
                                    Exit Sub
                                End If
                                Dim procedureServiceTpm As New SurgicalProcedureService
                                With procedureServiceTpm
                                    .IPSServiceParentId = serviceOrderDetail.IPSServiceId
                                    .IPSServiceId = ipsSutureMaterials.Id
                                    .ServiceAmount = 1
                                    .DefaultService = True
                                    .ValueItemServiceOrderDetail = 0
                                    .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                                    .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                                End With
                                listSurgicalProcedureService.Add(procedureServiceTpm)

                                Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                                'asigno los valores a la entidad serviceOrderDetailSurgical que es donde se almacenan los valores de los detalles quirurgicos con valor por defecto
                                With serviceOrderDetailSurgical
                                    .CodeNameIpsService = result.ObjectEmbbeded(0).CodeNameIpsService
                                    .IPSServiceId = result.ObjectEmbbeded(0).IPSServiceId
                                    .InvoicedQuantity = 1
                                    .LiquidationPercentage = 0
                                    .TotalSalesPrice = result.ObjectEmbbeded(0).TotalSalesPrice
                                    .ClassServiceIps = ResourceManager.GetString("SutureMaterials", "Contract")
                                    .RateManualSalePrice = result.ObjectEmbbeded(0).RateManualSalePrice
                                    .CostValue = result.ObjectEmbbeded(0).CostValue
                                    .BillingConceptId = result.ObjectEmbbeded(0).BillingConceptId
                                    .CostCenterId = result.ObjectEmbbeded(0).CostCenterId
                                    .SurchargeApply = False
                                End With
                                serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)


                                Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("RightRoom", "Contract")).FirstOrDefault()
                                If detailRigthRoom IsNot Nothing Then
                                    detailRigthRoom.TotalSalesPrice = Utils.RoundValue(CDec(detailRigthRoom.RateManualSalePrice * rateManual.PercentageNoBloodyRoom / 100), rateManual.RoundService)
                                End If


                                'Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("RightRoom", "Contract")).FirstOrDefault()
                                'detailRigthRoom.TotalSalesPrice = detailRigthRoom.TotalSalesPrice * rateManual.PercentageNoBloodyRoom / 100
                                'detailRigthRoom.RateManualSalePrice =
                                serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                                serviceOrderDetail.RateManualSalePrice = serviceOrderDetail.SubTotalSalesPrice
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice

                                'obetengo todos los valores por defecto 
                                Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                                'asigno los nuevos valores al listado del detalle quirurgico
                                For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                                    listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                                    listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                                Next
                            End Using

                        End Using
                    Else
                        'cuando sea no cruento
                        'obtengo el item de materiales de sutura si existe
                        Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("SutureMaterials", "Contract"))
                        'si existe un item para materiales lo elimino
                        If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                            serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault())
                            listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                        End If
                        indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
                        listServiceOrderDetailPopup.Remove(serviceOrderDetail)
                        Using modelIpsService As New MIPSService(Me.Tag)
                            Using model As New MServiceOrder(Me.Tag)
                                listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)
                                Dim ipsSuture = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("RightRoom", "Contract") And x.DefaultService = True)
                                Dim ipsRightRoom = modelIpsService.GetIPSServiceByIdSimple(ipsSuture.IPSServiceId).ObjectEmbbeded
                                If ipsRightRoom.AssociatedMaterialIPSServiceId IsNot Nothing Then
                                    'consulto el ips para el material de sutura
                                    Dim ipsSutureMaterials = modelIpsService.GetIPSServiceByIdSimple(ipsRightRoom.AssociatedMaterialIPSServiceId).ObjectEmbbeded
                                    Dim procedureServiceTpm As New SurgicalProcedureService
                                    With procedureServiceTpm
                                        .IPSServiceParentId = serviceOrderDetail.IPSServiceId
                                        .IPSServiceId = ipsSutureMaterials.Id
                                        .ServiceAmount = 1
                                        .DefaultService = True
                                        .ValueItemServiceOrderDetail = 0
                                        .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                                        .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                                    End With
                                    listSurgicalProcedureService.Add(procedureServiceTpm)
                                End If
                                'vuelvo a poner el valor que se calculo porque pudo cambiar cuando es no cruento
                                Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("RightRoom", "Contract")).FirstOrDefault()
                                detailRigthRoom.TotalSalesPrice = detailRigthRoom.RateManualSalePrice
                                Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                                Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalDefault)
                                serviceOrderDetail = result.ObjectEmbbeded

                                'los detalles que estan como por defecto le asigno los valores de la entidad service order detail surgical que trae los valores calculado para cada item
                                'esto se hace porque el listado con el que se hace datasource a la rejilla es de otro tipo al que retorna el calculo del servicio
                                For i As Integer = 0 To listSurgicalDefault.Count - 1 Step 1
                                    listSurgicalDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                                    listSurgicalDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                                Next
                                INDGcSurgery.DataSource = listSurgicalProcedureService
                                INDGvSurgery.ExpandAllGroups()
                            End Using
                        End Using
                        'inserto el item en la misma posicion qe estaba antes de ser eliminado
                        listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                    End If

                    Dim indexRow = INDGvSurgery.FocusedRowHandle
                    INDGcSurgery.DataSource = Nothing
                    INDGcSurgery.DataSource = listSurgicalProcedureService
                    INDGvSurgery.ExpandAllGroups()
                    INDGvSurgery.FocusedRowHandle = indexRow
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                    Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                    GetIndividualDiscount()
                    _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
                    ctrTmp.PrintInfo()
                End If
            End If
        End If
    End Sub

    Private Sub INDTxtAuthorizationNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtAuthorizationNumber.EditValueChanged
        If serviceOrderDetail IsNot Nothing Then
            serviceOrderDetail.AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
        End If
    End Sub

    Private Sub INDSePercent_EditValueChanged(sender As Object, e As EventArgs) Handles INDSePercent.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If INDSePercent.EditValue > 0 Then
            If INDGleLiquidationType.EditValue = 4 Then ' si es incluido en el mismo servicio

                serviceOrderDetail.SubTotalSalesPrice = Utils.RoundValue(CDec(serviceOrderDetail.RateManualSalePrice * INDSePercent.EditValue / 100), 1)
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
            Else
                Dim serviceDetailTmp = DirectCast(INDGvSleIncludedService.GetFocusedRow, ServiceOrderDetail)
                serviceOrderDetail.SubTotalSalesPrice = Utils.RoundValue(CDec(serviceDetailTmp.TotalSalesPrice * INDSePercent.EditValue / 100), 1)
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice

                If serviceDetailTmp.Id > 0 Then
                    serviceOrderDetail.IncludeServiceOrderDetailId = serviceDetailTmp.Id
                Else
                    serviceOrderDetail.ServiceOrderDetail2 = serviceDetailTmp
                End If
            End If
        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.RateManualSalePrice
                serviceOrderDetail.RecoveryRatio = 0
                serviceOrderDetail.IncludeServiceOrderDetailId = Nothing
                serviceOrderDetail.ServiceOrderDetail2 = Nothing
                serviceOrderDetail.TotalSalesPrice = 0
                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                'INDSeIndividualDiscount.EditValue = 0
            End If
        End If
        GetIndividualDiscount()
        PrintServiceValue()
    End Sub

    Private Sub INDTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValue.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If serviceOrderDetail IsNot Nothing Then
            If INDTxtValue.Properties.ReadOnly = False And _changeValue = False Then
                Dim subTotal = serviceOrderDetail.RateManualSalePrice - Me.TotalSalesPrice
                serviceOrderDetail.SubTotalSalesPrice = IIf(subTotal > 0, subTotal, subTotal * -1)
                serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                GetIndividualDiscount()
                _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
                ctrTmp.PrintInfo()
            End If
        End If

    End Sub

    Private Sub INDSlePerformsHealthProfessionalCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePerformsHealthProfessionalCode.EditValueChanged
        If INDSlePerformsHealthProfessionalCode.EditValue IsNot Nothing Then
            SetSpecialties(healthProfessional, INDGleSpecialtyPerformsHealthProfessional)
        End If
    End Sub

    Private Sub INDGleSurgicalInterventionType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDGleSurgicalInterventionType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count > 1 Then
                If e.NewValue = 1 Then
                    For Each item In listServiceOrderDetailPopup
                        Dim listEvents As New List(Of ServiceOrderDetail)(_listServiceOrderDetailSurgicalIntervention.ToArray())
                        Dim listDetails As New List(Of ServiceOrderDetail)(listServiceOrderDetailPopup.ToArray())
                        listDetails.Remove(item)
                        listEvents.AddRange(listDetails)
                        Dim surgicalDetailBasic = listEvents.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber)
                        If surgicalDetailBasic IsNot Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = "En el evento " + (INDSeSurgeryNumber.EditValue).ToString() + " ya existen servicios y no se puede agregar uno basico"
                            e.Cancel = True
                            Exit Sub
                        End If
                    Next
                End If
            Else
                If _editMode = False Then
                    If e.NewValue = 1 Then
                        Dim surgicalDetailBasic = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.SurgeryNumber = INDSeSurgeryNumber.EditValue)
                        If surgicalDetailBasic IsNot Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = "En el evento " + (INDSeSurgeryNumber.EditValue).ToString() + " ya existen servicios y no se puede agregar uno basico"
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "RecordNavigationChangeEvent"
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        editValueChangeLoadControl = True
        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        editValueChangeLoadControl = False
        LoadControls(Record)
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDBtnAddDetail.Click
        If errorsGetValue IsNot Nothing AndAlso errorsGetValue.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorsGetValue
            Exit Sub
        End If
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If serviceOrderDetail Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Por favor cargue un servicio"
            Exit Sub
        End If
        If _editMode = False Then
            If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count > 1 Then
                listServiceOrderDetailPopup.ForEach(Sub(x) x.GrandTotalSalesPrice = x.TotalSalesPrice * x.InvoicedQuantity)
            Else
                'serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
                If serviceOrderDetail.Presentation = 2 Then
                    serviceOrderDetail.SurgeryNumber = CInt(INDSeSurgeryNumber.EditValue)
                End If
            End If
            '_listServiceOrderDetailSurgicalIntervention.AddRange(listServiceOrderDetailPopup)
            '_listServiceOrderDetailDatasourceIncludeService.AddRange(listServiceOrderDetailPopup)

            Dim args As New AddServiceEventArgs
            args.ListServiceOrderDetail = listServiceOrderDetailPopup
            args.EditMode = False
            args.ViewToPackageItems = ViewToPackageItems
            args.ContractPackageId = INDsleContractPackage.EditValue
            cleaningControls = False
            Me.AsyncLoader(True)
            RaiseEvent AddServiceOrderDetail(Me, args)
            'Me.Close()
            'INDSleIncludeService.Properties.DataSource = Nothing
            'INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService
            'CleanControls()
        Else
            Dim args As New AddServiceEventArgs
            args.ServiceOrderDetail = serviceOrderDetail
            args.EditMode = True
            cleaningControls = True
            RaiseEvent AddServiceOrderDetail(Me, args)
            CleanControls()
            'Me.Close()
        End If

    End Sub
#End Region

#Region "ShowingEditor"
    Private Sub INDGvSurgery_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvSurgery.ShowingEditor
        Dim surgicalProcedireService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
        If surgicalProcedireService.DefaultService = True And serviceOrderDetail.AllowValueChange = True Then
            If _editMode = True Then
                INDRpTxtValue.ReadOnly = True
            Else
                INDRpTxtValue.ReadOnly = False
            End If
        Else
            INDRpTxtValue.ReadOnly = True
        End If
        If surgicalProcedireService.ClassService = ResourceManager.GetString("None", "Contract") Or surgicalProcedireService.ClassService = ResourceManager.GetString("RightRoom", "Contract") Or surgicalProcedireService.ClassService = ResourceManager.GetString("SutureMaterials", "Contract") Or surgicalProcedireService.ClassService = ResourceManager.GetString("SurgicalInstrumentation", "Contract") Then
            INDRptSleHealthProfessional.ReadOnly = True
        Else
            If surgicalProcedireService.DefaultService Then
                If _editMode = True And serviceOrderDetail.LiquidationType = 2 Then
                    INDRptSleHealthProfessional.ReadOnly = True
                Else
                    INDRptSleHealthProfessional.ReadOnly = False
                End If
            Else
                INDRptSleHealthProfessional.ReadOnly = True
            End If
        End If
    End Sub
#End Region

#Region "EditValueChanging"

#Region "Evento cuando el valor por defecto se marcaba en un radio group"
    'Private Async Sub INDRptRgDefaulValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptRgDefaulValue.EditValueChanging
    '    If editValueChangeLoadControl = True Then
    '        Exit Sub
    '    End If
    '    Dim rowPosition = INDGvSurgery.FocusedRowHandle
    '    Dim surgicalProcedireService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
    '    'obtengo el item que debo remover del listado ya que el valor por defecto cambio
    '    Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
    '    'remuevo el item de la entidad del detalle quirurgico
    '    serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault())
    '    'obtengo los items por defecto de la clase seleccionada
    '    Dim listDefaultValue = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
    '    'los pongo todos en valor por defecto false
    '    listDefaultValue.ForEach(Sub(x) x.DefaultService = False)
    '    ' el nuevo item lo marco como por defecto
    '    surgicalProcedireService.DefaultService = True
    '    Dim indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
    '    listServiceOrderDetailPopup.Remove(serviceOrderDetail)
    '    Using model As New MServiceOrder(Me.Tag)
    '        listSurgicalProcedureService.ForEach(Sub(x) x.ValueItemServiceOrderDetail = 0)
    '        listSurgicalProcedureService.ForEach(Sub(x) x.PerformsHealthProfessionalCode = Nothing)
    '        'obetengo todos los valores por defecto 
    '        Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
    '        'consulto el valor enviando como parametro el listado de los valores por defecto y el detalle que se esta trabajando
    '        serviceOrderDetail = Await model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureDefault)
    '        'asigno los nuevos valores al listado del detalle quirurgico
    '        For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
    '            listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
    '            listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
    '        Next
    '    End Using
    '    'inserto el item en la misma posicion qe estaba antes de ser eliminado
    '    listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
    '    INDGcSurgery.DataSource = Nothing
    '    INDGcSurgery.DataSource = listSurgicalProcedureService
    '    INDGvSurgery.ExpandAllGroups()
    '    INDGvSurgery.FocusedRowHandle = rowPosition
    '    GetIndividualDiscount()
    '    _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
    '    ctrTmp.PrintInfo()
    'End Sub
#End Region


    Private Sub INDSlePerformsHealthProfessionalCode_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSlePerformsHealthProfessionalCode.EditValueChanging
        If e.NewValue IsNot Nothing Then
            INDGleSpecialtyPerformsHealthProfessional.Enabled = True
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
            If _editMode = True AndAlso serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.PerformsHealthProfessionalCode = e.NewValue
                serviceOrderDetail.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
            End If
        Else
            INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        End If
    End Sub

    Private Sub INDRptSleHealthProfessional_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSleHealthProfessional.EditValueChanging
        If serviceOrderDetail IsNot Nothing Then
            If e.NewValue IsNot Nothing Then
                Dim SurgicalProcedureService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
                Dim healthProfessional As HealthCareProfessionalXpo
                Using model As New MServiceOrder(Me.Tag)
                    Dim healthProfessionalTmp = model.GetCareProfessionalByCode(e.NewValue.ToString().Trim())
                    healthProfessional = healthProfessionalTmp(0)
                End Using
                Using model As New MThirdParty(Me.Tag)
                    Dim thirdPartyTmp = model.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                    If thirdPartyTmp.Id = 0 Then
                        'si el medico no esta creado como tercero en la BD no continua el proceso
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", MODULE_NAME), healthProfessional.CodeName)
                        e.Cancel = True
                        Exit Sub
                    End If

                    Dim professionalSelected = listSurgicalProcedureService.Find(Function(x) x.PerformsHealthProfessionalCode = e.NewValue)
                    If professionalSelected IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "Este profesional ya esta seleccionado como " + professionalSelected.ClassService
                        e.Cancel = True
                        Exit Sub
                    End If

                    If SurgicalProcedureService.ClassService = ResourceManager.GetString("Surgeon", "Contract") Then
                        'pongo el mismo medico que se selecciono como cirujano en el search de medico
                        INDSlePerformsHealthProfessionalCode.EditValue = e.NewValue
                    End If
                    'cambio el medico a la entidad SurgicalProcedureService para mostrar en la rejilla
                    SurgicalProcedureService.PerformsHealthProfessionalCode = e.NewValue
                    Dim surgicalDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = SurgicalProcedureService.IPSServiceId).FirstOrDefault()
                    'le asigno el medico a la entidad ServiceOrderDetailSurgical
                    surgicalDetail.PerformsHealthProfessionalCode = SurgicalProcedureService.PerformsHealthProfessionalCode
                    surgicalDetail.PerformsHealthProfessionalThirdPartyId = thirdPartyTmp.Id
                    If surgicalDetail.Id > 0 Then
                        surgicalDetail.MarkAsModified()
                    End If
                End Using
            End If
        End If
    End Sub

    Private Sub INDRptCheDefaultService_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptCheDefaultService.EditValueChanging
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        Dim rowPosition = INDGvSurgery.FocusedRowHandle
        Dim surgicalProcedireService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
        If surgicalProcedireService.ClassService = "Ayudante" Then
            If e.NewValue = False Then
                'si se va a desactivar un item verifico que no sea el unico que esta seleccionado
                Dim listSurgicalProcedureServiceTmp = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = "Ayudante" And x.DefaultService = True)
                If listSurgicalProcedureServiceTmp.Count = 1 Then
                    e.Cancel = True
                    Exit Sub
                Else
                    'disminuyo el valor del item que se deselecciono
                    serviceOrderDetail.SubTotalSalesPrice -= surgicalProcedireService.ValueItemServiceOrderDetail
                    serviceOrderDetail.RateManualSalePrice -= surgicalProcedireService.ValueItemServiceOrderDetail
                    serviceOrderDetail.TotalSalesPrice -= surgicalProcedireService.ValueItemServiceOrderDetail
                    serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedireService.IPSServiceId).FirstOrDefault())
                    surgicalProcedireService.ValueItemServiceOrderDetail = 0
                    surgicalProcedireService.DefaultService = False
                    surgicalProcedireService.PerformsHealthProfessionalCode = Nothing
                    INDGcSurgery.DataSource = Nothing
                    INDGcSurgery.DataSource = listSurgicalProcedureService
                    INDGvSurgery.ExpandAllGroups()
                    INDGvSurgery.FocusedRowHandle = rowPosition
                    GetIndividualDiscount()
                    _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
                    ctrTmp.PrintInfo()
                End If
            Else
                'si se va activar un item y la clase es ayudante
                surgicalProcedireService.DefaultService = True
                Dim indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
                listServiceOrderDetailPopup.Remove(serviceOrderDetail)
                Using model As New MServiceOrder(Me.Tag)
                    listSurgicalProcedureService.ForEach(Sub(x) x.ValueItemServiceOrderDetail = 0)
                    listSurgicalProcedureService.ForEach(Sub(x) x.PerformsHealthProfessionalCode = Nothing)
                    'obetengo todos los valores por defecto 
                    Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                    'consulto el valor enviando como parametro el listado de los valores por defecto y el detalle que se esta trabajando
                    Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureDefault)
                    If result.StateResult = True Then
                        serviceOrderDetail = result.ObjectEmbbeded
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    'asigno los nuevos valores al listado del detalle quirurgico
                    For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                        listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                        listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                    Next
                End Using
                'inserto el item en la misma posicion qe estaba antes de ser eliminado
                listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
                ctrTmp.PrintInfo()
            End If
        Else
            If e.NewValue = False Then
                e.Cancel = True
                Me.Cursor = Cursors.Default
                Exit Sub
            ElseIf surgicalProcedireService.ClassService = ResourceManager.GetString("RightRoom", "Contract") And surgicalProcedireService.ClassService <> ResourceManager.GetString("SutureMaterials", "Contract") And surgicalProcedireService.ClassService <> ResourceManager.GetString("RightRoom", "Contract") Then ' el item es derecho a sala
                'obtengo el item que debo remover del listado ya que el valor por defecto cambio
                Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                'obtengo el item de materiales de sutura si existe
                Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("SutureMaterials", "Contract"))
                'remuevo el item de la entidad del detalle quirurgico
                serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault())
                'si existe un item para materiales lo elimino
                If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                    serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault())
                    listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                End If
                'obtengo los items por defecto de la clase seleccionada
                Dim listDefaultValue = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                'los pongo todos en valor por defecto false
                listDefaultValue.ForEach(Sub(x) x.DefaultService = False)
                'obtengo los valores
                ' el nuevo item lo marco como por defecto
                surgicalProcedireService.DefaultService = True
                GetIPSSutureMaterials(surgicalProcedireService)
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
                ctrTmp.PrintInfo()
            Else
                'obtengo el item que debo remover del listado ya que el valor por defecto cambio
                Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                'remuevo el item de la entidad del detalle quirurgico
                serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault())
                'obtengo los items por defecto de la clase seleccionada
                Dim listDefaultValue = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                'los pongo todos en valor por defecto false
                listDefaultValue.ForEach(Sub(x) x.DefaultService = False)
                ' el nuevo item lo marco como por defecto
                surgicalProcedireService.DefaultService = True
                Dim indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
                listServiceOrderDetailPopup.Remove(serviceOrderDetail)
                Using model As New MServiceOrder(Me.Tag)
                    listSurgicalProcedureService.ForEach(Sub(x) x.ValueItemServiceOrderDetail = 0)
                    listSurgicalProcedureService.ForEach(Sub(x) x.PerformsHealthProfessionalCode = Nothing)
                    'obetengo todos los valores por defecto 
                    Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                    'consulto el valor enviando como parametro el listado de los valores por defecto y el detalle que se esta trabajando
                    Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureDefault)
                    If result.StateResult = True Then
                        serviceOrderDetail = result.ObjectEmbbeded
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    'asigno los nuevos valores al listado del detalle quirurgico
                    For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                        listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                        listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                    Next
                End Using
                'inserto el item en la misma posicion qe estaba antes de ser eliminado
                listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice)
                ctrTmp.PrintInfo()
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDGleSpecialtyPerformsHealthProfessional_KeyDown(sender As Object, e As KeyEventArgs) Handles INDGleSpecialtyPerformsHealthProfessional.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDTxtAuthorizationNumber.Focus()
        End If
    End Sub

    Private Sub INDSeSurgeryNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSeSurgeryNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDGleSurgicalInterventionType.Focus()
        End If
    End Sub

    Private Sub FrmPopupServices_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    Private Sub INDsleDescription_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDescription.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2096, Nothing, True)

        End If
    End Sub

    Private Sub INDSleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCareGroup
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDSleServiceSoatIss_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleServiceSoatIss.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmIPSService
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDSleServiceCups_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleServiceCups.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCupsEntity
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDSlePerformsFuntionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlePerformsFuntionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmFunctionalUnit
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub INDSLeCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCostCenter
                OpenFormDialog(form)
            End Using
        End If
    End Sub
#End Region
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
#End Region

End Class
