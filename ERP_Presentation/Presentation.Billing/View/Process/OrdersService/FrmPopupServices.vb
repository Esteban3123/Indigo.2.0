'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 6-11-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.Xpo
Imports Presentation.Billing.MVP
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.XtraEditors
Imports Presentation.Contract.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Payroll.MVP
Imports Presentation.Common.MVP
Imports Infrastructure.CrossCutting.Resources
Imports System.Drawing
Imports Presentation.Contract
Imports Presentation.Payroll
Imports Presentation.Common
Imports System.ComponentModel
Imports Domain.Crystal.Entities
Imports Presentation.Billing
Imports System.Globalization
Imports System.Threading

#End Region

Public Class FrmPopupServices

#Region "EVENTS"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        cupsEntity = Nothing
        listHomologation = Nothing
        runCalculationValue = Nothing
        listServiceOrderDetailPopup = Nothing
        listServiceOrderDetailPopupDelete = Nothing
        serviceOrderDetail = Nothing
        _serviceValue = Nothing
        _patientDate = Nothing
        _patienGenus = Nothing
        listSurgicalProcedureService = Nothing
        _changeValue = Nothing
        _editMode = Nothing
        errorsGetValue = Nothing
        errorsHomologation = Nothing
        thirdParty = Nothing
        editValueChangeLoadControl = Nothing
        cleaningControls = Nothing
        _flagHomologation = Nothing
        healthProfessional = Nothing
        _idTmp = Nothing
        _bgSOATISS = Nothing
        _bgGetServiceValue = Nothing
        _bgCUPS = Nothing
        srvOrderModel = Nothing
        _serviceDate = Nothing
        Me.TaxInclude = False
        Me.TaxedService = False
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando se de click en el boton de agregar y pasa el o los registros al formulario principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
#End Region

#Region "BUILDER"
    Sub New(Optional _isFormQuotation As Boolean = False)
        InitializeComponent()
        srvOrderModel = New MServiceOrder(Me.Tag)
        ctrTmp = New CtrServiceOrderInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = DockStyle.Fill
        ctrTmp.PopupContainerControl = INDPccMoreInfoAdminssion
        ctrTmp.PopupContainerControlValue = INDPccHomologation
        AddHandler ctrTmp.OpenPopupValue, AddressOf OpenPopupValue
        AdditionalControlPanel.Controls.Add(ctrTmp)
        INDGvSurgery.OptionsView.ShowAutoFilterRow = False
        IsFormQuotation = _isFormQuotation
    End Sub
#End Region

#Region "TUPLES"
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
    ''' representa el datasource de tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listLiquidationTypeQX As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListLiquidationTypeQX As List(Of Tuple(Of Byte, String))
        Get
            If _listLiquidationTypeQX Is Nothing Then
                _listLiquidationTypeQX = New List(Of Tuple(Of Byte, String))
                _listLiquidationTypeQX.Add(New Tuple(Of Byte, String)(1, "Manual de Tarifas"))
                _listLiquidationTypeQX.Add(New Tuple(Of Byte, String)(4, "% del mismo servicio"))
                _listLiquidationTypeQX.Add(New Tuple(Of Byte, String)(3, "Incluido el 100%"))
            End If
            Return _listLiquidationTypeQX
        End Get
    End Property

    ''' <summary>
    ''' data source para el search de si o no
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listYesNo As List(Of Tuple(Of Boolean, String))
    ReadOnly Property ListYesNo As List(Of Tuple(Of Boolean, String))
        Get
            If _listYesNo Is Nothing Then
                _listYesNo = New List(Of Tuple(Of Boolean, String))
                _listYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
                _listYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
            End If
            Return _listYesNo
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
    ''' Variable que me indica que el registro que se esta editando viene desde cotización
    ''' </summary>
    Private QuotationServiceOrderDetailId As Integer? = Nothing

    ''' <summary>
    ''' Permite saber si este popup se esta abriendo desde el form de cotizaciones
    ''' </summary>
    Private IsFormQuotation As Boolean

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
    Private listServiceOrderDetailPopup As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' agrega el item que se esta modificando si se cambian valores y se genera uno nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listServiceOrderDetailPopupDelete As ObjectList

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
    ''' Almacena el valor del servicio cuando viene desde una cotización, se utiliza para validar que el valor no haya cambiado
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceValueQuotation As Decimal = 0

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
    ''' almacena los errores obteniendo las homologaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim errorsHomologation As New StringBuilder
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
    ''' bandera para saber que el valor de los controles se cambio desde el metodo de cargar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private editTxtValueLoadControl As Boolean
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
    ''' <summary>
    ''' variable temporal para poner un id y poder seleccionar los servicios cuando no se han guardado
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idTmp As Integer = -1
    ''' <summary>
    ''' BackgroundWorker para obtener las homologaciones ISS y SOAT
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bgSOATISS As BackgroundWorker
    ''' <summary>
    ''' BackgroundWorker para obtener el valor del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bgGetServiceValue As BackgroundWorker
    ''' <summary>
    ''' BackgroundWorker para obtener las homologaciones CUPS
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bgCUPS As BackgroundWorker

    ''' <summary>
    ''' BackgroundWorker para obtener el valor del servicio desde rias
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bgGetServiceValueByRIAS As BackgroundWorker

    ''' <summary>
    ''' modelo
    ''' </summary>
    Dim srvOrderModel As MServiceOrder

    ''' <summary>
    ''' Código del cups para poder consultar los RIAS del search
    ''' </summary>
    Dim CupsCodeSearchRIAS As String

    ''' <summary>
    ''' Listado que sirve para comparar cantidades de RIAS
    ''' </summary>
    Public ListCompare As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Permite saber si se lanzó el proceso de GetServiceValue() desde el cambio de valor del search de RIAS
    ''' </summary>
    Dim IsSearchRias As Boolean = False

    ''' <summary>
    ''' Entidad de detalle de orden de servicio para el asyncrono de rias
    ''' </summary>
    Dim EntityTemp As ServiceOrderDetail

    ''' <summary>
    ''' Obtiene el id de la orden de servicio y me permite validar si muestro o no el control del estado en la parte superior
    ''' </summary>
    Public ServiceOrderId As Integer

    ''' <summary>
    ''' Obtiene el estado de la orden de servicio
    ''' </summary>
    Public ServiceOrderStatus As String

    ''' <summary>
    ''' Permite saber si se valida como obligatorio el no. de autorización
    ''' </summary>
    Private HandlesAuthorizationNumber As Boolean = False

    ''' <summary>
    ''' permite añadir control de autorizacion
    ''' </summary>
    Private AllowsAddAuthorizationControl As Boolean

    ''' <summary>
    ''' bandera si viene del dashboard de cotizaciones
    ''' </summary>
    Private _flagDasboardQuoted As Boolean

    ''' <summary>
    ''' lista de cups a filtrar si viene de dashboard de cotizaciones
    ''' </summary>
    Private _listCupsIds As List(Of Integer)

    ''' <summary>
    ''' Bandera Impuesto Incluido companysettings
    ''' </summary>
    Private _flagTaxInclude As Boolean

    ''' <summary>
    ''' variable que almacena el valor del porcentaje del impuesto del servicio
    ''' </summary>
    Private _taxPercent As Decimal

    ''' <summary>
    ''' listado del detalle de la orden de servicio para usar como datasource cuando se va a incluir en otro servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceOrderDetailDatasourceIncludeService As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' este listado se pasa para que los items que se eliminaron de la rejilla no aparezcan en el search de incluir a servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceOrderDetailDelete As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' listado del detalle de la orden de servicio para los tipos de intervenciones quirurgicas
    ''' </summary>
    ''' <remarks></remarks>
    Private _listServiceOrderDetailSurgicalIntervention As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' fecha 
    ''' </summary>
    Dim _serviceDate As DateTime

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    Dim _requestQuoteServices As Boolean

    ''' <summary>
    ''' codigo centro de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Property CenterAttentionCode As String

    ''' <summary>
    ''' codigo unicad funcional
    ''' </summary>
    ''' <returns></returns>
    Private Property FunctionalUnitCenterAttentionCode As String

    ''' <summary>
    ''' variable que define si un servicio IPS es gravado o no (impuestos)
    ''' </summary>
    Private _taxedService As Boolean

    ''' <summary>
    ''' valor neto
    ''' </summary>
    Private _totalSalesPrice As Decimal

    ''' <summary>
    ''' valor bruto
    ''' </summary>
    Private _grossValue As Decimal

    ''' <summary>
    ''' impuesto
    ''' </summary>
    Private _taxValue As Decimal
#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    Public WriteOnly Property RequestQuoteServices As Boolean
        Set(value As Boolean)
            _requestQuoteServices = value
        End Set
    End Property

    ''' <summary>
    ''' fecha del servicio
    ''' </summary>
    WriteOnly Property ServiceDate As DateTime
        Set(value As DateTime)
            _serviceDate = value
        End Set
    End Property

    ''' <summary>
    ''' este listado se pasa para que los items que se eliminaron de la rejilla no aparezcan en el search de incluir a servicio
    ''' </summary>
    ''' <remarks></remarks>
    WriteOnly Property ListServiceOrderDetailDelete As List(Of ServiceOrderDetail)
        Set(value As List(Of ServiceOrderDetail))
            _listServiceOrderDetailDelete = value
        End Set
    End Property

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
            If _listServiceOrderDetailDatasourceIncludeService.Count > 0 Then
                Dim itemsNews = _listServiceOrderDetailDatasourceIncludeService.FindAll(Function(x) x.Id = 0)
                If itemsNews.Count > 0 Then
                    _idTmp = (From a In itemsNews Where a.Id = 0 Select a.IdTmp).Min()
                    _idTmp -= 1
                End If
            End If
        End Set
    End Property

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

    Public Property PatientDateBirth As String
        Get
            Return INDLblAge.Text
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                INDLblAge.Text = Utils.AgeToString(CDate(value))
                _patientDate = CDate(value)
            End If
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
            If value IsNot Nothing Then
                _patienGenus = value
                If value = 1 Then
                    INDLblGenus.Text = ResourceManager.GetString("Male", MODULE_NAME)
                Else
                    INDLblGenus.Text = ResourceManager.GetString("Female", MODULE_NAME)
                End If
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

    ''' <summary>
    ''' # de autorizacion
    ''' </summary>
    Public WriteOnly Property AutorizationNumber As String
        Set(value As String)
            INDTxtAuthorizationNumber.EditValue = value
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
            LayoutControl1.BeginUpdate()
            INDSleCareGroup.Enabled = value
            INDGleServiceType.Enabled = value
            INDSleServiceCups.Enabled = value
            INDSleServiceSoatIss.Enabled = value
            INDDteDate.Enabled = value
            INDSlePerformsFuntionalUnit.Enabled = value
            INDSlePerformsHealthProfessionalCode.Enabled = value
            INDSlePerformsHealthProfessionalCode.Enabled = value
            INDGleSpecialtyPerformsHealthProfessional.Enabled = value
            INDGleSpecialtyPerformsHealthProfessional.Enabled = value
            LayoutControl1.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' proepiedad para activar los controles del grupo de comportamiento
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActivateControlsBehavior
        Set(value)
            LayoutControl1.BeginUpdate()
            INDTxtAuthorizationNumber.Enabled = value
            INDGleLiquidationType.Enabled = value
            INDSleIncludeService.Enabled = value
            INDSePercent.Enabled = value
            INDGleSurchargeApply.Enabled = value
            INDTxtValueTotal.Enabled = value
            INDTxtTaxValue.Enabled = value
            INDTxtGrossValue.Enabled = value
            INDSeIndividualDiscount.Enabled = If(value = True, If(BarraBotones.PermissionsForm.ContainsKey(115), True, False), False)
            LayoutControl1.EndUpdate()
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
            INDDteDate.Properties.MaxValue = value
        End Set
    End Property

    Public WriteOnly Property AdmissionDateMinValue As Date
        Set(value As Date)
            INDDteDate.Properties.MinValue = value
        End Set
    End Property

    Property ThirdPartyXPO As XPInstantFeedbackSource
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    Dim _careGroupAdmission As String
    WriteOnly Property CareGroupAdmission As String
        Set(value As String)
            _careGroupAdmission = value
        End Set
    End Property

    Dim _healthAdministratorCrystal As Integer
    WriteOnly Property HealthAdministratorCrystal As Integer
        Set(value As Integer)
            _healthAdministratorCrystal = value
        End Set
    End Property

    Property HealthAdministratorXPO As XPInstantFeedbackSource
        Get
            Return CType(INDsleHealthAdministrator.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthAdministrator.Properties.DataSource = value
        End Set
    End Property

    Dim _allowEditItem As Boolean
    WriteOnly Property AllowEditItem As Boolean
        Set(value As Boolean)
            _allowEditItem = value
        End Set
    End Property

    Public WriteOnly Property FlagDashboardQuoted As Boolean
        Set(value As Boolean)
            _flagDasboardQuoted = value
        End Set
    End Property

    Public WriteOnly Property ListCupsIds As List(Of Integer)
        Set(value As List(Of Integer))
            _listCupsIds = value
        End Set
    End Property

    ''' <summary>
    ''' Valor neto
    ''' </summary>
    ''' <returns></returns>
    Property TotalSalesPrice As Decimal
        Get
            Return _totalSalesPrice
        End Get
        Set(value As Decimal)
            _totalSalesPrice = value
            INDTxtValueTotal.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor Bruto
    ''' </summary>
    ''' <returns></returns>
    Property GrossValue As Decimal
        Get
            Return _grossValue
        End Get
        Set(value As Decimal)
            _grossValue = value
            INDTxtGrossValue.EditValue = value
        End Set
    End Property

    Property TaxValue As Decimal
        Get
            Return _taxValue
        End Get
        Set(value As Decimal)
            _taxValue = value
            INDTxtTaxValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el valor de la bandera de impuestos incluidos
    ''' </summary>
    WriteOnly Property TaxInclude As Boolean
        Set(value As Boolean)
            Me._flagTaxInclude = value
        End Set
    End Property

    ''' <summary>
    ''' propeidad que obtiene y establece el valor de la bandera si es un servicio agarvado, adicional oculta los control si no es gravado
    ''' </summary>
    ''' <returns></returns>
    Private Property TaxedService As Boolean
        Get
            Return _taxedService
        End Get
        Set(value As Boolean)
            _taxedService = value
            If Not value Then
                Me.HideTaxesControls()
            Else
                Me.ChangedTaxesControls(Me._flagTaxInclude)
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad que lee las cantidades del control
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property Quantity As Integer
        Get
            Return INDSeCount.EditValue
        End Get
    End Property

#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método que me muestra u oculta los grupos de rias y descripciones
    ''' </summary>
    Private Sub HideShowFieldsRIASAndDescription()
        Dim careGroupXpo As Infrastructure.Data.Xpo.BillingRepository.CareGroupXpo = Nothing
        Dim cupsEntityXpo As Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo = Nothing

        INDsleRIASCups.EditValue = Nothing
        INDsleRIASCups.Properties.NullText = String.Empty
        INDsleRIASCups.Properties.DataSource = Nothing
        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        INDsleDescription.Properties.DataSource = Nothing

        If INDSleCareGroup.EditValue IsNot Nothing Then
            careGroupXpo = srvOrderModel.CareGroupById(INDSleCareGroup.EditValue)
        End If

        If INDSleServiceCups.EditValue IsNot Nothing Then
            cupsEntityXpo = srvOrderModel.GetCupsEntityById(INDSleServiceCups.EditValue)
        End If

        INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If careGroupXpo IsNot Nothing AndAlso careGroupXpo.ApplyRIAS = True AndAlso cupsEntityXpo IsNot Nothing AndAlso cupsEntityXpo.ApplyRIAS = True Then
            INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDBtnAddDetail.Enabled = True
        End If

        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If cupsEntityXpo IsNot Nothing AndAlso cupsEntityXpo.CUPSEntityContractDescriptionsXpo IsNot Nothing AndAlso cupsEntityXpo.CUPSEntityContractDescriptionsXpo.Count > 0 Then
            If (From x In cupsEntityXpo.CUPSEntityContractDescriptionsXpo Where x.IsDelete = False).Count > 0 Then
                INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDBtnAddDetail.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' limpia los controles en modo edicion
    ''' </summary>
    Private Sub CleanControlsEditMode()
        LayoutControl1.BeginUpdate()
        serviceOrderDetail = Nothing
        ActivateControls = True
        ActivateControlsBehavior = False

        INDSleThirdParty.Properties.ReadOnly = True
        INDsleHealthAdministrator.Properties.ReadOnly = True

        INDSeCount.EditValue = 1
        INDSeCount.Properties.ReadOnly = False
        INDDteDate.EditValue = Nothing

        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSLeCostCenter.EditValue = Nothing
        INDSLeCostCenter.Properties.NullText = String.Empty
        INDSLeCostCenter.Properties.ReadOnly = True
        INDSlePerformsHealthProfessionalCode.EditValue = Nothing
        INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = False
        INDSlePerformsHealthProfessionalCode.Properties.NullText = String.Empty
        INDGcEvents.DataSource = Nothing
        INDPceEvents.Text = 0

        INDGleLiquidationType.EditValue = 1

        INDSleIncludeService.EditValue = Nothing

        INDSleIncludeService.Properties.NullText = String.Empty
        INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercent.EditValue = 0

        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.TotalSalesPrice = 0
        Me.GrossValue = 0
        Me.TaxValue = 0
        _taxPercent = 0

        INDGleSurchargeApply.EditValue = False

        INDSeSurgeryNumber.EditValue = Nothing

        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDGleSurgicalInterventionType.EditValue = Nothing

        listSurgicalProcedureService = Nothing
        INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDGleSpecialtyPerformsHealthProfessional.EditValue = Nothing
        INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.NullText = String.Empty

        listHomologation = Nothing
        Me.BarraBotones.FilterDataSource = Nothing

        _flagHomologation = False

        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="detail"></param>
    Private Sub AddSurgicalDetailRemove(detail As ServiceOrderDetailSurgical)
        detail.MarkAsDeleted()
        If Not serviceOrderDetail.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ServiceOrderDetailSurgical") Then
            Dim list As New ObjectList
            list.Add(detail)
            serviceOrderDetail.ChangeTracker.ObjectsRemovedFromCollectionProperties.Add("ServiceOrderDetailSurgical", list)
        Else
            serviceOrderDetail.ChangeTracker.ObjectsRemovedFromCollectionProperties("ServiceOrderDetailSurgical").Add(detail)
        End If
    End Sub

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, Decimal)
        Return New Tuple(Of String, String, Decimal)(Patient, Format(_serviceValue, "c2"), (Me.TaxValue * Quantity))
    End Function

    ''' <summary>
    ''' limpia controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
        QuotationServiceOrderDetailId = Nothing
        SetReadOnlyControlsForQuotation = False
        _serviceValueQuotation = 0
        serviceOrderDetail = Nothing
        ActivateControls = True
        ActivateControlsBehavior = False
        If cleaningControls = True Then
            INDSleCareGroup.EditValue = Nothing
            INDSleCareGroup.Properties.NullText = String.Empty
            INDSleCareGroup.Properties.ReadOnly = False
            INDSleThirdParty.EditValue = Nothing
            INDSleThirdParty.Properties.NullText = String.Empty
            INDSleThirdParty.Properties.ReadOnly = False
            INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDDteDate.EditValue = _serviceDate
            INDsleHealthAdministrator.EditValue = Nothing
            INDsleHealthAdministrator.Properties.NullText = String.Empty
            INDsleHealthAdministrator.Properties.ReadOnly = False
            INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleServiceType.EditValue = Nothing
            INDGleServiceType.Properties.ReadOnly = False
            INDGleServiceType.Enabled = False
            INDSleServiceSoatIss.EditValue = Nothing
            INDSleServiceSoatIss.Properties.NullText = String.Empty
            INDSleServiceSoatIss.Properties.ReadOnly = False
            INDSleServiceCups.EditValue = Nothing
            INDSleServiceCups.Properties.NullText = String.Empty
            INDSleServiceCups.Properties.ReadOnly = False
            INDSlePerformsFuntionalUnit.EditValue = Nothing
            INDSlePerformsFuntionalUnit.Properties.NullText = String.Empty
            INDSlePerformsFuntionalUnit.Properties.ReadOnly = False

            Me.CleanProfessional("es-CR" = SessionValues.Instance.Culture.Name)

            INDSleCareGroup.Focus()
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
            INDSleThirdParty.Properties.ReadOnly = True
            INDsleHealthAdministrator.Properties.ReadOnly = True

        End If
        INDSeCount.EditValue = 1
        INDSeCount.Properties.ReadOnly = False
        INDDteDate.Properties.ReadOnly = False
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSLeCostCenter.EditValue = Nothing
        INDSLeCostCenter.Properties.NullText = String.Empty
        INDSLeCostCenter.Properties.ReadOnly = True

        Me.CleanProfessional(Not ("es-CR" = SessionValues.Instance.Culture.Name))

        INDGcEvents.DataSource = Nothing
        INDPceEvents.Text = 0
        INDTxtAuthorizationNumber.Properties.ReadOnly = False
        INDGleLiquidationType.EditValue = 1
        INDGleLiquidationType.Properties.ReadOnly = False
        INDSleIncludeService.EditValue = Nothing
        INDSleIncludeService.Properties.ReadOnly = False
        INDSleIncludeService.Properties.NullText = String.Empty
        INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercent.EditValue = 0
        INDSePercent.Properties.ReadOnly = False
        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDTxtValueTotal.Properties.ReadOnly = False
        Me.TotalSalesPrice = 0
        Me.GrossValue = 0
        Me.TaxValue = 0
        _taxPercent = 0
        INDGleSurchargeApply.EditValue = False
        INDGleSurchargeApply.Properties.ReadOnly = False
        INDSeSurgeryNumber.EditValue = Nothing
        INDSeSurgeryNumber.Properties.ReadOnly = False
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDGleSurgicalInterventionType.EditValue = Nothing
        INDGleSurgicalInterventionType.Properties.ReadOnly = False
        listSurgicalProcedureService = Nothing
        INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never


        listServiceOrderDetailPopup = Nothing
        PrintServiceValue()
        listHomologation = Nothing
        Me.BarraBotones.FilterDataSource = Nothing
        _editMode = False
        _flagHomologation = False
        INDBtnAddDetail.Text = ResourceManager.GetString("Add")

        editValueChangeLoadControl = True

        INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleRIASCups.Properties.DataSource = Nothing
        INDsleRIASCups.Properties.NullText = String.Empty
        INDsleApplyRIAS.EditValue = Nothing
        INDsleRIASCups.EditValue = Nothing

        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleDescription.Properties.DataSource = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        INDsleDescription.EditValue = Nothing
        editValueChangeLoadControl = False
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' limpia controles de medico realizo y especilida de medico realizo de 
    ''' pendiendo de si es "es-CR" o "es-CO"
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanProfessional(flag As Boolean)

        If Not flag Then
            Exit Sub
        End If

        INDSlePerformsHealthProfessionalCode.EditValue = Nothing
        INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = False
        INDSlePerformsHealthProfessionalCode.Properties.NullText = String.Empty

        INDGleSpecialtyPerformsHealthProfessional.EditValue = Nothing
        INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.NullText = String.Empty

    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles de los valores del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsErrorValueFields(Optional cleanEntity As Boolean = True)
        LayoutControl1.BeginUpdate()

        If cleanEntity AndAlso Not _editMode Then
            serviceOrderDetail = Nothing
        End If

        ActivateControlsBehavior = False
        INDTxtAuthorizationNumber.Properties.ReadOnly = False
        INDGleLiquidationType.EditValue = 1
        INDGleLiquidationType.Properties.ReadOnly = False
        INDSleIncludeService.EditValue = Nothing
        INDSleIncludeService.Properties.ReadOnly = False
        INDSleIncludeService.Properties.NullText = String.Empty
        INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercent.EditValue = 0
        INDSePercent.Properties.ReadOnly = False
        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.TotalSalesPrice = 0
        Me.GrossValue = 0
        Me.TaxValue = 0
        INDTxtValueTotal.Properties.ReadOnly = False
        INDGleSurchargeApply.EditValue = False
        INDGleSurchargeApply.Properties.ReadOnly = False
        INDSeSurgeryNumber.EditValue = Nothing
        INDSeSurgeryNumber.Properties.ReadOnly = False
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDGleSurgicalInterventionType.EditValue = Nothing
        INDGleSurgicalInterventionType.Properties.ReadOnly = False
        listSurgicalProcedureService = Nothing
        INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        listServiceOrderDetailPopup = Nothing
        PrintServiceValue()
        listHomologation = Nothing
        Me.BarraBotones.FilterDataSource = Nothing
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' metodo para establecer los datasource a los combos con datos quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSourceCombo()
        INDGleServiceType.Properties.DataSource = ListServiceType
        INDGleLiquidationType.Properties.DataSource = ListLiquidationType
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDsleApplyRIAS.Properties.DataSource = ListYesNo
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
            _flagHomologation = True
        End If
    End Sub

    ''' <summary>
    ''' metodo para obtener las homologaciones del iss o soat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHomologationIssSoat()
        If INDSleCareGroup.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDSleServiceSoatIss.EditValue Is Nothing Then
            Exit Sub
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

        AsyncLoader(True)
        _bgSOATISS = New BackgroundWorker
        _bgSOATISS.WorkerSupportsCancellation = True
        AddHandler _bgSOATISS.DoWork, AddressOf _bgSOATISS_DoWork
        AddHandler _bgSOATISS.RunWorkerCompleted, AddressOf _bgSOATISS_RunWorkerCompleted
        _bgSOATISS.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' metodo para obtener las homologaciones que tiene el cups para saber que servicio ips se debe utilizar si SOAT o ISS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHomologationCups()
        If INDSleCareGroup.EditValue Is Nothing Then
            Exit Sub
        End If
        If INDSleServiceCups.EditValue Is Nothing Then
            Exit Sub
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
        If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If (INDsleApplyRIAS.EditValue = True AndAlso INDsleRIASCups.EditValue Is Nothing) OrElse String.IsNullOrEmpty(INDsleApplyRIAS.Text) Then
                Exit Sub
            End If
        End If
        If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue Is Nothing Then
            Exit Sub
        End If

        INDBtnAddDetail.Enabled = False
        AsyncLoader(True)
        _bgCUPS = New BackgroundWorker
        _bgCUPS.WorkerSupportsCancellation = True
        AddHandler _bgCUPS.DoWork, AddressOf _bgCUPS_DoWork
        AddHandler _bgCUPS.RunWorkerCompleted, AddressOf _bgCUPS_RunWorkerCompleted
        _bgCUPS.RunWorkerAsync()
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
        Using model As New MServiceOrder(Me.Tag)
            'consulto el valor del servicio con los parametros requeridos
            _bgGetServiceValue = New BackgroundWorker
            _bgGetServiceValue.WorkerSupportsCancellation = True
            AddHandler _bgGetServiceValue.DoWork, AddressOf _bgGetServiceValue_DoWork
            AddHandler _bgGetServiceValue.RunWorkerCompleted, AddressOf _bgGetServiceValue_RunWorkerCompleted
            _bgGetServiceValue.RunWorkerAsync()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener el valor del servicio cuando se llenen los campos requeridos desde rias
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetServiceValueByRIAS()
        Using model As New MServiceOrder(Me.Tag)
            _bgGetServiceValueByRIAS = New BackgroundWorker
            _bgGetServiceValueByRIAS.WorkerSupportsCancellation = True
            AddHandler _bgGetServiceValueByRIAS.DoWork, AddressOf _bgGetServiceValueByRIAS_DoWork
            AddHandler _bgGetServiceValueByRIAS.RunWorkerCompleted, AddressOf _bgGetServiceValueByRIAS_RunWorkerCompleted
            _bgGetServiceValueByRIAS.RunWorkerAsync()
        End Using
    End Sub

    ''' <summary>
    ''' metodo para cargar controles con el registro seleccionado
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Async Sub LoadControls(Record As ServiceOrderDetail)
        LayoutControl1.BeginUpdate()
        serviceOrderDetail = Record
        With serviceOrderDetail
            'esta bandera se hace para que no se haga nada en los eventos editValueChanged de los controles mientras se asignan desde este metodo
            editValueChangeLoadControl = True
            INDSleCareGroup.EditValue = .CareGroupId
            Me.TaxedService = .TaxedService
            Me._taxPercent = .TaxPercent
            Using model As New MCareGroup(Me.Tag)
                Dim careGroupTmp = Await model.GetCareGroupById(.CareGroupId)
                Select Case careGroupTmp.ObjectEmbbeded.CareGroupType
                    Case 1 ' EAPB Con contrato
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Using modelContract As New Presentation.Contract.MVP.MContract(Me.Tag)
                            Dim contract = Await modelContract.GetContractById(careGroupTmp.ObjectEmbbeded.ContractId)
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministrator = Await modelHealthAdministrator.GetHealthAdministratorById(contract.ObjectEmbbeded.HealthAdministratorId)
                                .ThirdPartyId = healthAdministrator.ObjectEmbbeded.ThirdPartyId
                                .HealthAdministratorId = healthAdministrator.ObjectEmbbeded.Id
                            End Using
                        End Using
                    Case 2, 4 'EAPB Sin Contrato y aseguradoras
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If INDsleHealthAdministrator.EditValue IsNot Nothing Then
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministrator = Await modelHealthAdministrator.GetHealthAdministratorById(INDsleHealthAdministrator.EditValue)
                                .ThirdPartyId = healthAdministrator.ObjectEmbbeded.ThirdPartyId
                                .HealthAdministratorId = healthAdministrator.ObjectEmbbeded.Id
                                .CodeNameHealthAdministrator = healthAdministrator.ObjectEmbbeded.Code + " - " + healthAdministrator.ObjectEmbbeded.Name
                            End Using
                        End If
                    Case 3 'Particulares
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        If INDSleThirdParty.EditValue IsNot Nothing Then
                            .ThirdPartyId = INDSleThirdParty.EditValue
                            .NitNameThirdParty = INDSleThirdParty.Text
                        End If
                End Select

                If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ApplyRIAS = INDsleApplyRIAS.EditValue
                    .RIASCupsId = Nothing
                    If INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        .RIASCupsId = INDsleRIASCups.EditValue
                        .RiasCupsDescription = INDsleRIASCups.Text
                    End If
                End If

                If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .CUPSEntityContractDescriptionId = INDsleDescription.EditValue
                    .ContractDescriptionCodeName = INDsleDescription.Text
                End If
            End Using

            .CodeNameCareGroup = INDSleCareGroup.Text
            .CodeNameFunctionalUnit = INDSlePerformsFuntionalUnit.Text
            If listHomologation IsNot Nothing AndAlso listHomologation.Count > 1 Then
                INDGleServiceType.EditValue = 3
                INDSleServiceCups.EditValue = .CUPSEntityId
                Using model As New MServiceOrder(Me.Tag)
                    CupsXPO = model.ListCUPS(INDSleCareGroup.EditValue)
                End Using
                INDSleServiceCups.Properties.NullText = .CodeNameCups

            Else
                Using model As New MServiceOrder(Me.Tag)
                    If INDGleServiceType.EditValue = 2 Then
                        SoatIssXPO = model.ListIssServicesByCareGroup(INDSleCareGroup.EditValue)
                    Else
                        SoatIssXPO = model.ListSoatByCareGroup(INDSleCareGroup.EditValue)
                    End If
                End Using
                'si se obtubo el valor como cups
                If INDGleServiceType.EditValue = 3 Then
                    If INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                        INDSleServiceSoatIss.EditValue = .IPSServiceId
                    Else
                        INDSleServiceCups.EditValue = .CUPSEntityId
                    End If
                End If
            End If
            INDSeCount.EditValue = .InvoicedQuantity
            INDDteDate.EditValue = .ServiceDate
            INDSlePerformsFuntionalUnit.EditValue = .PerformsFunctionalUnitId
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSLeCostCenter.EditValue = .CostCenterId
            INDSLeCostCenter.Properties.NullText = .CodeNameCostCenter

            'si se permite cambiar el valor del servicio activo o inactivo el campo
            If .AllowValueChange = True Then
                INDTxtValueTotal.Properties.ReadOnly = False
                INDRpTxtValue.ReadOnly = False
            Else
                INDTxtValueTotal.Properties.ReadOnly = True
                INDRpTxtValue.ReadOnly = True
            End If

            If .Presentation = 2 Then
                INDSeCount.EditValue = 1
                'si el servicio es quirurgico cargo los datos de la rejilla
                INDSeSurgeryNumber.EditValue = IIf(.SurgeryNumber = 0, Nothing, .SurgeryNumber)
                If .SurgeryNumber > 0 Then
                    If _listServiceOrderDetailSurgicalIntervention IsNot Nothing Then
                        'obtengo los detalles de la orden de servicio por el mismo evento que se esta seleccionndo y asigno el datasource para mostralo en un popup
                        Dim listEvent = _listServiceOrderDetailSurgicalIntervention.FindAll(Function(x) x.SurgeryNumber = .SurgeryNumber And x.Presentation = 2)
                        INDGcEvents.DataSource = listEvent
                        INDPceEvents.Text = listEvent.Count.ToString()
                    End If
                End If
                'si el servicio se iquida como SOAT cambio el datasource de las intervenciones quirurgicas para que aparezca la opcion de no cruento
                If .IsSOAT = True Then
                    INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionTypeNoBloody
                Else
                    INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
                End If
                INDGleSurgicalInterventionType.EditValue = .SurgicalInterventionType
                INDLciLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDGleLiquidationType.Properties.DataSource = ListLiquidationTypeQX
                INDGleLiquidationType.EditValue = .SettlementType
                Using model As New MServiceOrder(Me.Tag)
                    PerformsHealthProfessionalRepositoryXPO = model.ListHealthCareProfessional()
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'consulto los detalles del servicio ips cuando es quirurgico
                    listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)

                    'los detalles que estan como por defecto le asigno los valores de la entidad service order detail surgical que trae los valores calculado para cada item
                    'esto se hace porque el listado con el que se hace datasource a la rejilla es de otro tipo al que retorna el calculo del servicio
                    For Each itemSurgicalDetail In serviceOrderDetail.ServiceOrderDetailSurgical

                        Dim hasMaterial = listSurgicalProcedureService.Any(Function(x) x.ClassService = "Materiales Sutura")
                        Dim surgicalDefault = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = itemSurgicalDetail.IPSServiceId)
                        surgicalDefault.DefaultService = True

                        'si el item es derecho a sala busco el ips para materiales de sutura
                        If surgicalDefault.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper() AndAlso Not hasMaterial Then

                            surgicalDefault.ValueItemServiceOrderDetail = itemSurgicalDetail.TotalSalesPrice
                            surgicalDefault.PerformsHealthProfessionalCode = .PerformsHealthProfessionalCode
                            surgicalDefault.AllowValueChange = itemSurgicalDetail.AllowValueChange

                            If .SurgicalInterventionType Is Nothing Or .SurgicalInterventionType < 9 Then
                                GetIPSSutureMaterials(surgicalDefault)
                            Else
                                'si es cruento consulto el manual de tarifas para obtener el ips de materiales
                                Dim rateManual As RateManual
                                Using modelRateManual As New MRateManual(Me.Tag)
                                    rateManual = (Await modelRateManual.GetRateManualById(serviceOrderDetail.RateManualId)).ObjectEmbbeded
                                End Using
                                Dim ipsSutureMaterials As IPSService
                                Using modelIPS As New MIPSService(Me.Tag)
                                    ipsSutureMaterials = (Await modelIPS.GetIPSServiceById(rateManual.MaterialNoBloodyIPSServiceId)).ObjectEmbbeded
                                    Dim procedureServiceTpm As New SurgicalProcedureService
                                    With procedureServiceTpm
                                        .IPSServiceParentId = serviceOrderDetail.IPSServiceId
                                        .IPSServiceId = ipsSutureMaterials.Id
                                        .ServiceAmount = 1
                                        .DefaultService = True
                                        .ValueItemServiceOrderDetail = 0
                                        .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                                        .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                                        .AllowValueChange = itemSurgicalDetail.AllowValueChange
                                    End With
                                    listSurgicalProcedureService.Add(procedureServiceTpm)
                                End Using
                                'asigno valores al item materiales de sutura 
                                Dim detailSuturematerial = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper())
                                detailSuturematerial.ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = ipsSutureMaterials.Id).FirstOrDefault().TotalSalesPrice
                                detailSuturematerial.PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = ipsSutureMaterials.Id).FirstOrDefault().PerformsHealthProfessionalCode

                            End If

                        Else
                            If errorsGetValue.Length = 0 Then
                                surgicalDefault.ValueItemServiceOrderDetail = itemSurgicalDetail.TotalSalesPrice
                                surgicalDefault.PerformsHealthProfessionalCode = itemSurgicalDetail.PerformsHealthProfessionalCode
                                surgicalDefault.AllowValueChange = itemSurgicalDetail.AllowValueChange
                            End If
                        End If
                    Next
                    If errorsGetValue Is Nothing OrElse errorsGetValue.Length = 0 Then
                        INDGcSurgery.DataSource = listSurgicalProcedureService
                        INDGvSurgery.ExpandAllGroups()
                    End If
                End Using
            Else
                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDGleLiquidationType.Properties.DataSource = ListLiquidationType
                Me.TotalSalesPrice = .SubTotalSalesPrice
                Me.TaxValue = .TaxValue
                Me.GrossValue = .GrossValue
            End If

            INDSlePerformsHealthProfessionalCode.EditValue = .PerformsHealthProfessionalCode
            INDGleSpecialtyPerformsHealthProfessional.EditValue = .PerformsProfessionalSpecialty

            If errorsGetValue Is Nothing OrElse errorsGetValue.Length = 0 Then
                If .AuthorizationNumber IsNot Nothing Then
                    INDTxtAuthorizationNumber.EditValue = .AuthorizationNumber
                End If
                INDGleLiquidationType.EditValue = Nothing
                If .SettlementType = 0 Then
                    INDGleLiquidationType.EditValue = 1
                Else
                    INDGleLiquidationType.EditValue = .SettlementType
                End If

                INDSleIncludeService.EditValue = .IncludeServiceOrderDetailId
                INDSePercent.EditValue = .RecoveryRatio
                INDGleSurchargeApply.EditValue = .SurchargeApply
                If .Presentation = 2 Then
                    INDTxtValueTotal.Properties.ReadOnly = True
                End If
                INDSeIndividualDiscount.EditValue = .ThirdPartyDiscountPercentage
                editValueChangeLoadControl = False
                'obtengo el valor de todos los items para ponerlo en el control de usuario de la barra botones
                PrintServiceValue()
            End If

            .QuotationServiceOrderDetailId = QuotationServiceOrderDetailId
            If QuotationServiceOrderDetailId IsNot Nothing AndAlso QuotationServiceOrderDetailId > 0 Then
                SetReadOnlyControlsForQuotation = True
            End If
        End With
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' metodo para colocar el valor del servicio en el control de usuario de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintServiceValue()
        If listServiceOrderDetailPopup Is Nothing Then
            _serviceValue = 0
        Else
            _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.GrandTotalSalesPrice)
        End If
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo que obtiene los materiales de sutura cuando el tipo de intervencion quirurgica sea no cruento
    ''' </summary>
    ''' <param name="defaultValue">parametro para saber si se esta cambiando el valor por defecto</param>
    ''' <remarks></remarks>
    Private Sub GetIPSSutureMaterialsNoBloody(defaultValue As Boolean)
        'consulto el manual de tarifas
        Dim rateManual As RateManual
        Using model As New MRateManual(Me.Tag)
            rateManual = model.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded
        End Using
        Using model As New MIPSService(Me.Tag)
            Using modelService As New MServiceOrder(Me.Tag)

                'si no existe un derecho a sala cuando sea cruento obtengo de nuevo el item
                Dim existsRightRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper()).FirstOrDefault()
                If existsRightRoom Is Nothing Then
                    Dim resultRightRoom = modelService.GetServiceValue(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode),
                                                                       listHomologation.FindAll(Function(x) x.IPSServiceId = serviceOrderDetail.IPSServiceId And x.CupsEntityId = serviceOrderDetail.CUPSEntityId),
                                                                       INDSleCareGroup.EditValue,
                                                                       INDSlePerformsFuntionalUnit.EditValue,
                                                                       INDGleSpecialtyPerformsHealthProfessional.EditValue,
                                                                       INDDteDate.EditValue,
                                                                       _patienGenus,
                                                                       _patientDate,
                                                                       INDSeCount.EditValue,
                                                                       INDSlePerformsHealthProfessionalCode.EditValue,
                                                                       thirdParty.Id)
                    serviceOrderDetail.ServiceOrderDetailSurgical.Add(resultRightRoom.ObjectEmbbeded(0).ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper()).FirstOrDefault())
                    Dim rigthRoomDefault = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper())
                    rigthRoomDefault.DefaultService = True
                End If

                'consulto el grupo quirurgico del servicio cargado para saber si se cobran los materiales de sutura
                Dim surgicalGroup As SurgicalGroup = Nothing
                Using modelIPS As New MIPSService(Me.Tag)
                    Dim ipsRigthRoomTmp = modelIPS.GetIPSServiceByIdSimple(serviceOrderDetail.IPSServiceId)
                    Using modelSurgicalGroup As New MSurgicalGroup(Me.Tag)
                        surgicalGroup = modelSurgicalGroup.GetSurgicalGroupByIdSimple(ipsRigthRoomTmp.ObjectEmbbeded.SurgicalGroupId).ObjectEmbbeded
                    End Using
                End Using

                'obtengo el servicio ips para materiales de sutura no cruentos
                If surgicalGroup.MaterialsService > 0 Then
                    If rateManual.MaterialNoBloodyIPSServiceId Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("El manual de tarifas {0} no tiene parametrizado el Servicio IPS Material", rateManual.Code)
                        INDGleSurgicalInterventionType.EditValue = 1
                        Exit Sub
                    End If

                    Dim ipsSutureMaterials = model.GetIPSServiceByIdSimple(rateManual.MaterialNoBloodyIPSServiceId).ObjectEmbbeded
                    Dim listHomologationTmp = modelService.ListCupsHomologationByIpsServiceId(ipsSutureMaterials.Id)
                    Dim result = modelService.GetServiceValueByManual(serviceOrderDetail, ipsSutureMaterials.Id, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id)
                    If result.StateResult = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        INDGleSurgicalInterventionType.EditValue = 1
                        Exit Sub
                    End If
                    If result.ObjectEmbbeded.BillingConceptId Is Nothing Then
                        Dim message = "El servicio IPS " + ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name + " no tiene asignado concepto de facturación."
                        If ipsSutureMaterials.ServiceClass <> 6 Then
                            message &= " La clase del servicio no es Materiales Sutura."
                        End If
                        Mensaje(EeventViewerImages.Advertencia) = message
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
                        .AllowValueChange = serviceOrderDetail.AllowValueChange
                    End With
                    listSurgicalProcedureService.Add(procedureServiceTpm)

                    Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                    'asigno los valores a la entidad serviceOrderDetailSurgical que es donde se almacenan los valores de los detalles quirurgicos con valor por defecto
                    With serviceOrderDetailSurgical
                        .CodeNameIpsService = result.ObjectEmbbeded.CodeNameIpsService
                        .IPSServiceId = result.ObjectEmbbeded.IPSServiceId
                        .InvoicedQuantity = 1
                        .LiquidationPercentage = 0
                        .TotalSalesPrice = result.ObjectEmbbeded.TotalSalesPrice
                        .ClassServiceIps = ResourceManager.GetString("SutureMaterials", "Contract")
                        .RateManualSalePrice = result.ObjectEmbbeded.RateManualSalePrice
                        .CostValue = result.ObjectEmbbeded.CostValue
                        .BillingConceptId = result.ObjectEmbbeded.BillingConceptId
                        .CostCenterId = result.ObjectEmbbeded.CostCenterId
                        .SurchargeApply = False
                    End With
                    serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
                End If
                If defaultValue = False Then
                    Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper()).FirstOrDefault()
                    If detailRigthRoom IsNot Nothing Then
                        detailRigthRoom.TotalSalesPrice = Utils.RoundValue(CDec(detailRigthRoom.RateManualSalePrice * rateManual.PercentageNoBloodyRoom / 100), rateManual.RoundService)
                    End If
                End If
                serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                serviceOrderDetail.RateManualSalePrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.GrossValue = If(_flagTaxInclude, serviceOrderDetail.TotalSalesPrice / ((serviceOrderDetail.TaxPercent / 100.0F) + 1), serviceOrderDetail.TotalSalesPrice)

                'obetengo todos los valores por defecto 
                Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                'asigno los nuevos valores al listado del detalle quirurgico
                For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                    Dim surgicalDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault()
                    If surgicalDetail IsNot Nothing Then
                        listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = surgicalDetail.TotalSalesPrice
                        listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = surgicalDetail.PerformsHealthProfessionalCode
                    End If
                Next
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener el servicio ips de materiales de sutura
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetIPSSutureMaterials(surgicalProcedureService As SurgicalProcedureService)
        If INDGleSurgicalInterventionType.EditValue IsNot Nothing AndAlso INDGleSurgicalInterventionType.EditValue = 9 Then
            'esto se hace para obtener los materiales de sutura cuando el tipo de intervencion quirurgica sea no cruento 
            GetIPSSutureMaterialsNoBloody(False)
        Else
            Using modelIpsService As New MIPSService(Me.Tag)

                Dim query As IPSService
                Dim materialIpsServiceId As Integer?

                If surgicalProcedureService.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper() Then
                    query = modelIpsService.GetIPSServiceByIdSimple(surgicalProcedureService.IPSServiceId)?.ObjectEmbbeded
                    materialIpsServiceId = query?.Id
                Else
                    'se obtiene los materiales de sutura asociado al servicio IPS si tiene entonces, NO se consultan los materiales asociado a la sala
                    Dim resultIps = modelIpsService.GetMaterialIPSServiceByParentId(serviceOrderDetail.IPSServiceId, EClassService.SutureMaterials)?.ObjectEmbbeded
                    If Not resultIps?.IsNotNullAndAny() Then
                        query = modelIpsService.GetIPSServiceByIdSimple(surgicalProcedureService.IPSServiceId)?.ObjectEmbbeded
                        materialIpsServiceId = query?.AssociatedMaterialIPSServiceId
                        'se mandan a quitar todos los materiales de sutura para postular (si tiene)  solo el material asociado a la sala selecionada
                        Dim toRemove = listSurgicalProcedureService.FindAll(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper())
                        Me.RemoveItemMaterials(toRemove)
                    End If
                End If

                'si se encuentra el materia de sutura se procede agregarlo y consultar el valor
                If materialIpsServiceId.HasValue Then
                    'consulto el ips para el material de sutura
                    Dim ipsSutureMaterials = modelIpsService.GetIPSServiceByIdSimple(materialIpsServiceId).ObjectEmbbeded
                    Dim procedureServiceTpm As New SurgicalProcedureService
                    With procedureServiceTpm
                        .IPSServiceParentId = surgicalProcedureService.IPSServiceParentId
                        .IPSServiceId = ipsSutureMaterials?.Id
                        .ServiceAmount = 1
                        .DefaultService = True
                        .ValueItemServiceOrderDetail = 0
                        .CodeNameService = ipsSutureMaterials?.Code + " - " + ipsSutureMaterials?.Name
                        .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                        .AllowValueChange = surgicalProcedureService.AllowValueChange
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
                            Dim surgicalDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault()
                            If surgicalDetail IsNot Nothing Then
                                listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = surgicalDetail.TotalSalesPrice
                                listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = surgicalDetail.PerformsHealthProfessionalCode
                            End If
                        Next
                        'inserto el item en la misma posicion qe estaba antes de ser eliminado
                        listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                        serviceOrderDetail.RateManualSalePrice = If(Me._flagTaxInclude, serviceOrderDetail.TotalSalesPrice, serviceOrderDetail.GrossValue)
                        Me.GrossValue = serviceOrderDetail.GrossValue
                        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        Me.TaxValue = serviceOrderDetail.TaxValue
                    Else
                        If surgicalProcedureService.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper() Then
                            'volvemos a obtener el servicio por defecto de derecho a sala
                            GetIpsRightRoomDefaultValueError(result.Message, indexItem)
                        Else
                            'si existe un error informamos y limpiamos controles
                            errorsGetValue = result.Message
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDTxtAuthorizationNumber.EditValue = Nothing
                            INDGleLiquidationType.EditValue = 1
                            Me.TotalSalesPrice = 0
                            editValueChangeLoadControl = True
                            INDGleSurchargeApply.EditValue = False
                            INDSeIndividualDiscount.EditValue = 0
                            ActivateControlsBehavior = False
                            INDSleCareGroup.Focus()
                            editValueChangeLoadControl = False
                            Exit Sub
                        End If
                    End If
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo para volver a obtener el servicio de derecho a sala que estaba por defecto, cuando ocurre un error cambiando el servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetIpsRightRoomDefaultValueError(errorMessge As String, indexItem As Integer)
        Using modelIpsService As New MIPSService(Me.Tag)
            Using model As New MServiceOrder(Me.Tag)
                'consulto los detalles del servicio ips cuando es quirurgico
                listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)
                Dim ipsSuture = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper() And x.DefaultService = True)
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
                        .AllowValueChange = serviceOrderDetail.AllowValueChange
                    End With
                    listSurgicalProcedureService.Add(procedureServiceTpm)
                End If
                Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalDefault)
                If result.StateResult = False Then
                    'si existe un error informamos y limpiamos controles
                    errorsGetValue = result.Message
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.BarraBotones.FilterDataSource = Nothing
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTxtAuthorizationNumber.EditValue = Nothing
                    INDGleLiquidationType.EditValue = 1
                    Me.TotalSalesPrice = 0
                    editValueChangeLoadControl = True
                    INDGleSurchargeApply.EditValue = False
                    INDSeIndividualDiscount.EditValue = 0
                    ActivateControlsBehavior = False
                    INDSleCareGroup.Focus()
                    editValueChangeLoadControl = False
                    Exit Sub
                End If
                Mensaje(EeventViewerImages.Advertencia) = errorMessge
                serviceOrderDetail = result.ObjectEmbbeded
                'inserto el item en la misma posicion qe estaba antes de ser eliminado
                listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                'los detalles que estan como por defecto le asigno los valores de la entidad service order detail surgical que trae los valores calculado para cada item
                'esto se hace porque el listado con el que se hace datasource a la rejilla es de otro tipo al que retorna el calculo del servicio
                For i As Integer = 0 To listSurgicalDefault.Count - 1 Step 1
                    listSurgicalDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                    listSurgicalDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                Next
            End Using
        End Using


    End Sub

    ''' <summary>
    ''' metodo para calcular el descuento del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetIndividualDiscount()
        If serviceOrderDetail Is Nothing Then
            Exit Sub
        End If
        'se pone esta bandera en true para que no se haga nada en el editValueChanged del TxtValue

        Dim round = 0
        If serviceOrderDetail.RateManualId IsNot Nothing AndAlso (cupsEntity Is Nothing OrElse Not (cupsEntity.RIPSConcept = "12" OrElse cupsEntity.RIPSConcept = "13")) Then
            Using model As New MRateManual(Me.Tag)
                round = model.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded.RoundService
            End Using
        End If

        _changeValue = True
        If INDSeIndividualDiscount.EditValue > 0 Then

            Dim percent As Decimal
            'si el tipo de liquidacion es por un porcentaje de otro servicio o en el mismo
            If INDGleLiquidationType.EditValue = 2 Or INDGleLiquidationType.EditValue = 4 Then
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice

                percent = Math.Round(serviceOrderDetail.SubTotalSalesPrice * INDSeIndividualDiscount.EditValue / 100, 0)

                If serviceOrderDetail.Presentation = 2 Then
                    For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                        item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * INDSeIndividualDiscount.EditValue / 100, round)
                        Dim serviceTmp = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = item.IPSServiceId)
                        serviceTmp.ValueItemServiceOrderDetail = item.TotalSalesPrice
                    Next
                    INDGcSurgery.RefreshDataSource()
                End If

            Else
                If Not serviceOrderDetail.AllowValueChange Then
                    'sino permite cambiar el valor siempre tomo el RateManualSalePrice para hacer los calculos
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                Else
                    'si permite cambiar el valor tomo lo que este en el campo de valor para hacer los calculos
                    serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                End If
                percent = Utils.RoundValue((serviceOrderDetail.TotalSalesPrice * INDSeIndividualDiscount.EditValue) / 100, round)
            End If

            If serviceOrderDetail.TotalSalesPrice > round Then
                serviceOrderDetail.TotalSalesPrice = Utils.RoundValue(serviceOrderDetail.TotalSalesPrice - percent, round)
            End If

            If serviceOrderDetail.GrandTotalSalesPrice > round Then
                serviceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue((serviceOrderDetail.SubTotalSalesPrice - percent) * serviceOrderDetail.InvoicedQuantity, round)
            End If

            Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
            serviceOrderDetail.ThirdPartyDiscountPercentage = INDSeIndividualDiscount.EditValue
            serviceOrderDetail.ThirdPartyDiscount = percent
        Else
            If INDGleLiquidationType.EditValue = 2 Or INDGleLiquidationType.EditValue = 4 Then
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                If serviceOrderDetail.Presentation = 2 Then
                    For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                        item.TotalSalesPrice = Utils.RoundValue(item.RateManualSalePrice * INDSePercent.EditValue / 100, round)
                        Dim serviceTmp = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = item.IPSServiceId)
                        serviceTmp.ValueItemServiceOrderDetail = item.TotalSalesPrice
                    Next
                    INDGcSurgery.RefreshDataSource()
                End If
            Else
                If serviceOrderDetail IsNot Nothing Then
                    serviceOrderDetail.TotalSalesPrice += serviceOrderDetail.ThirdPartyDiscount
                End If
            End If
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity

                If serviceOrderDetail.TotalSalesPrice > round Then
                    serviceOrderDetail.TotalSalesPrice = Utils.RoundValue(serviceOrderDetail.TotalSalesPrice, round)
                End If

                If serviceOrderDetail.GrandTotalSalesPrice > round Then
                    serviceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(serviceOrderDetail.GrandTotalSalesPrice, round)
                End If

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
            For Each item In listServiceOrderDetailPopup
                Using model As New MCareGroup(Me.Tag)
                    Dim careGroupTmp = model.GetCareGroupByIdSimple(item.CareGroupId)
                    Select Case careGroupTmp.ObjectEmbbeded.CareGroupType
                        Case 2, 4
                            If item.ThirdPartyId Is Nothing Then
                                errors.AppendLine("Para el grupo de atención del item " + (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString() + " se debe seleccionar un tercero")
                            End If
                        Case 3
                            If item.HealthAdministratorId Is Nothing Then
                                errors.AppendLine("Para el grupo de atención del item " + (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString() + " se debe seleccionar una administradora de salud")
                            End If
                    End Select
                End Using

                If item.Presentation = 2 Then
                    If item.SurgicalInterventionType Is Nothing Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartEmpty", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), INDLciSurgicalInterventionType.Text))
                    End If
                    If item.SurgeryNumber = 0 Then
                        errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartEmpty", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), INDLciSurgeryNumber.Text))
                    End If
                    Dim listEvents As New List(Of ServiceOrderDetail)(_listServiceOrderDetailSurgicalIntervention.ToArray())
                    Dim listDetails As New List(Of ServiceOrderDetail)(listServiceOrderDetailPopup.ToArray())
                    listDetails.Remove(item)
                    listEvents.AddRange(listDetails)
                    Dim surgicalDetailBasic = listEvents.Find(Function(x) x.SurgicalInterventionType IsNot Nothing AndAlso x.SurgicalInterventionType = 1 And x.SurgeryNumber = item.SurgeryNumber)
                    If surgicalDetailBasic IsNot Nothing Then
                        If surgicalDetailBasic.SurgicalInterventionType = item.SurgicalInterventionType Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartEvent", MODULE_NAME), item.SurgeryNumber.ToString(), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString()))
                        End If
                    End If
                    For Each itemSurgical In item.ServiceOrderDetailSurgical
                        If itemSurgical.ClassServiceIps?.ToUpper() = ResourceManager.GetString("Surgeon", "Contract").ToUpper() AndAlso itemSurgical.PerformsHealthProfessionalThirdPartyId Is Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartClassService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), itemSurgical.ClassServiceIps))
                        End If
                        If itemSurgical.ClassServiceIps?.ToUpper() = ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper() AndAlso itemSurgical.PerformsHealthProfessionalThirdPartyId Is Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartClassService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), itemSurgical.ClassServiceIps))
                        End If
                        If itemSurgical.ClassServiceIps?.ToUpper() = ResourceManager.GetString("Assistant", "Contract").ToUpper() AndAlso itemSurgical.PerformsHealthProfessionalThirdPartyId Is Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartClassService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), itemSurgical.ClassServiceIps))
                        End If
                    Next
                    Select Case item.SettlementType
                        Case 2
                            If item.IncludeServiceOrderDetailId Is Nothing Then
                                errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartNotIncludedService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString()))
                            End If
                            If item.RecoveryRatio = 0 Then
                                errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartRecoveryRatio", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString()))
                            End If
                        Case 3
                            If item.IncludeServiceOrderDetailId Is Nothing Then
                                errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartNotIncludedService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString()))
                            End If
                    End Select

                End If
                If HandlesAuthorizationNumber AndAlso String.IsNullOrEmpty(RTrim(LTrim(item.AuthorizationNumber))) Then
                    errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartEmpty", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), INDLciAuthorizationNumber.Text))
                End If
            Next
        Else
            If INDSleCareGroup.EditValue Is Nothing Then
                errors.AppendLine(INDLciCareGroup.Text + ResourceManager.GetString("Empty"))
            End If

            If INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDSleThirdParty.EditValue Is Nothing Then
                    errors.AppendLine("Para el centro de atención actual se debe seleccionar un tercero")
                End If
            End If

            If INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleHealthAdministrator.EditValue = Nothing Then
                    errors.AppendLine("Para el centro de atención actual se debe seleccionar una administradora de salud")
                End If
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

            If INDDteDate.EditValue < INDDteDate.Properties.MinValue Then
                errors.AppendLine("La fecha del servicio no puede ser menor que la del ingreso")
            End If

            If INDSlePerformsFuntionalUnit.EditValue Is Nothing Then
                errors.AppendLine(INDLciPerformsFuntionalUnit.Text + ResourceManager.GetString("Empty"))
            End If

            If INDSlePerformsHealthProfessionalCode.EditValue Is Nothing Then
                errors.AppendLine(INDLciPerformsHealthProfessionalCode.Text + ResourceManager.GetString("Empty"))
            End If

            If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDlyItemApplyRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If INDsleApplyRIAS.Text = String.Empty Then
                        errors.AppendLine(INDlyItemApplyRIAS.Text + ResourceManager.GetString("Empty"))
                    End If
                End If
                If INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    If INDsleRIASCups.EditValue = Nothing Then
                        errors.AppendLine(INDlyItemRIASCups.Text + ResourceManager.GetString("Empty"))
                    End If
                End If
            End If
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleDescription.EditValue Is Nothing Then
                    errors.AppendLine(INDlyItemDescription.Text + ResourceManager.GetString("Empty"))
                End If
            End If

            If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDSeSurgeryNumber.EditValue Is Nothing Then
                    errors.AppendLine(INDLciSurgeryNumber.Text + ResourceManager.GetString("Empty"))
                End If
                If INDGleSurgicalInterventionType.EditValue Is Nothing Then
                    errors.AppendLine(INDLciSurgicalInterventionType.Text + ResourceManager.GetString("Empty"))
                End If
                If _editMode = False Then
                    If INDGleSurgicalInterventionType.EditValue = 1 Then
                        Dim surgicalDetailBasic = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.SurgicalInterventionType = 1 And x.SurgeryNumber = serviceOrderDetail.SurgeryNumber)
                        If surgicalDetailBasic IsNot Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("BasicEvent", MODULE_NAME), INDSeSurgeryNumber.EditValue.ToString()))
                        End If
                    End If
                End If
                Dim listSurgicalProcedureTmp = (From sps In listSurgicalProcedureService Where sps.DefaultService = True Select sps.ClassService).Distinct().ToList()
                For i As Integer = 0 To listSurgicalProcedureTmp.Count - 1 Step 1
                    'obtengo los items que se van a cobrar por la clase que estoy recorriendo
                    Dim listDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True And x.ClassService = listSurgicalProcedureTmp(i))

                    For Each itemClassService In listDefault
                        If itemClassService.ClassService?.ToUpper() = ResourceManager.GetString("Surgeon", "Contract").ToUpper() AndAlso itemClassService.PerformsHealthProfessionalCode Is Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("PerformsHealthProfessionalNotSelected", MODULE_NAME), itemClassService.ClassService))
                        ElseIf itemClassService.ClassService?.ToUpper() = ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper() AndAlso itemClassService.PerformsHealthProfessionalCode Is Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("PerformsHealthProfessionalNotSelected", MODULE_NAME), itemClassService.ClassService))
                        ElseIf itemClassService.ClassService?.ToUpper() = ResourceManager.GetString("Assistant", "Contract").ToUpper() AndAlso itemClassService.PerformsHealthProfessionalCode Is Nothing Then
                            errors.AppendLine(String.Format(ResourceManager.GetString("PerformsHealthProfessionalNotSelected", MODULE_NAME), itemClassService.ClassService))
                        End If
                    Next
                Next
            End If
            If HandlesAuthorizationNumber AndAlso String.IsNullOrEmpty(RTrim(LTrim(INDTxtAuthorizationNumber.EditValue))) Then
                errors.AppendLine(INDLciAuthorizationNumber.Text + ResourceManager.GetString("Empty"))
            End If
            Select Case INDGleLiquidationType.EditValue
                Case 2
                    If INDSleIncludeService.EditValue Is Nothing Then
                        errors.AppendLine(ResourceManager.GetString("ServiceIncludedNotSelected", MODULE_NAME))
                    End If
                    If INDSePercent.EditValue = 0 Then
                        errors.AppendLine(ResourceManager.GetString("Percentage", MODULE_NAME))
                    End If
                Case 3
                    If INDSleIncludeService.EditValue Is Nothing Then
                        errors.AppendLine(ResourceManager.GetString("ServiceIncludedNotSelected", MODULE_NAME))
                    End If
            End Select
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' metodo para cargar los controles cuando se va a editar un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadControlsForEdit()
        LayoutControl1.BeginUpdate()
        INDSleIncludeService.Properties.DataSource = Nothing
        If serviceOrderDetail.Id > 0 Then
            _listServiceOrderDetailDatasourceIncludeService.RemoveAll(Function(x) x.Id = serviceOrderDetail.Id)
        Else
            _listServiceOrderDetailDatasourceIncludeService.RemoveAll(Function(x) x.IdTmp = serviceOrderDetail.IdTmp)
        End If
        GetDetailsIncludedService()
        INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        INDBtnAddDetail.Text = ResourceManager.GetString("Edit")
        editValueChangeLoadControl = True
        With serviceOrderDetail
            'asigno valores a los campos y los pongo readOnly
            INDSleCareGroup.EditValue = .CareGroupId
            Me.TaxedService = .TaxedService
            Me._taxPercent = .TaxPercent
            Using model As New MCareGroup(Me.Tag)
                Dim careGroupTmp = model.GetCareGroupByIdSimple(.CareGroupId)

                Select Case careGroupTmp.ObjectEmbbeded.CareGroupType
                    Case 1
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Case 2, 4
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDsleHealthAdministrator.EditValue = .HealthAdministratorId
                        INDsleHealthAdministrator.Properties.NullText = .CodeNameHealthAdministrator
                        INDsleHealthAdministrator.Properties.ReadOnly = True
                    Case 3
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDSleThirdParty.EditValue = .ThirdPartyId
                        INDSleThirdParty.Properties.NullText = .NitNameThirdParty
                        INDSleThirdParty.Properties.ReadOnly = True
                End Select
            End Using
            INDSleCareGroup.Properties.NullText = .CodeNameCareGroup
            INDSleCareGroup.Properties.ReadOnly = True
            _flagHomologation = True
            INDGleServiceType.EditValue = .ServiceType
            _flagHomologation = False
            INDGleServiceType.Properties.ReadOnly = False
            INDSleServiceSoatIss.EditValue = .IPSServiceId
            INDSleServiceSoatIss.Properties.NullText = .CodeNameIpsService
            INDSleServiceCups.EditValue = .CUPSEntityId
            INDSleServiceCups.Properties.NullText = .CodeNameCups

            Using model As New MCupsEntity(Me.Tag)
                Dim resultCups = model.GetCupsEntityByIdSimple(INDSleServiceCups.EditValue)
                cupsEntity = resultCups.ObjectEmbbeded
            End Using

            'Si aplica a RIAS el servicio, se habilita el grupo
            If .ApplyRIAS IsNot Nothing Then
                CupsCodeSearchRIAS = cupsEntity.Code
                INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleApplyRIAS.EditValue = .ApplyRIAS

                'Si aplica a RIAS se llena el datasource de las rias
                If .ApplyRIAS Then
                    INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDsleRIASCups.Properties.DataSource = srvOrderModel.ListRIASCups(CupsCodeSearchRIAS)
                    INDsleRIASCups.EditValue = .RIASCupsId
                End If
            End If

            If .CUPSEntityContractDescriptionId IsNot Nothing Then
                INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleDescription.EditValue = .CUPSEntityContractDescriptionId
                INDsleDescription.Properties.NullText = .ContractDescriptionCodeName
            End If

            INDSeCount.EditValue = .InvoicedQuantity
            If .Presentation = 2 Then
                INDSeCount.Properties.ReadOnly = True
            Else
                INDSeCount.Properties.ReadOnly = False
            End If

            INDDteDate.EditValue = .ServiceDate
            INDSlePerformsFuntionalUnit.Properties.NullText = .CodeNameFunctionalUnit
            INDSlePerformsFuntionalUnit.EditValue = .PerformsFunctionalUnitId
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSLeCostCenter.Properties.NullText = .CodeNameCostCenter

            'si se permite cambiar el valor del servicio activo o inactivo el campo
            If .AllowValueChange = True Then
                INDTxtValueTotal.Properties.ReadOnly = False
                INDRpTxtValue.ReadOnly = False
            Else
                INDTxtValueTotal.Properties.ReadOnly = True
                INDRpTxtValue.ReadOnly = True
            End If

            INDSlePerformsHealthProfessionalCode.EditValue = .PerformsHealthProfessionalCode
            INDGleSpecialtyPerformsHealthProfessional.EditValue = .PerformsProfessionalSpecialty
            If .Presentation = 2 Then
                INDLciLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDGleLiquidationType.Properties.DataSource = ListLiquidationTypeQX
                'si el registro a editar es quirurgico
                Using model As New MServiceOrder(Me.Tag)
                    PerformsHealthProfessionalRepositoryXPO = model.ListHealthCareProfessional()
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)

                    'Verifico si hay material sutura dentro de la entidad principal
                    Dim sutureMaterials = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()).FirstOrDefault()

                    If .SurgicalInterventionType < 9 Then
                        If sutureMaterials IsNot Nothing Then
                            Using modelIpsService As New MIPSService(Me.Tag)
                                'consulto el ips para el material de sutura
                                Dim ipsSutureMaterials = modelIpsService.GetIPSServiceByIdSimple(sutureMaterials.IPSServiceId).ObjectEmbbeded
                                Dim procedureServiceTpm As New SurgicalProcedureService
                                With procedureServiceTpm
                                    .IPSServiceParentId = serviceOrderDetail.IPSServiceId
                                    .IPSServiceId = ipsSutureMaterials.Id
                                    .ServiceAmount = 1
                                    .DefaultService = True
                                    .ValueItemServiceOrderDetail = 0
                                    .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                                    .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                                    .AllowValueChange = serviceOrderDetail.AllowValueChange
                                End With
                                listSurgicalProcedureService.Add(procedureServiceTpm)
                            End Using
                        End If
                    Else
                        'si es cruento consulto el manual de tarifas para obtener el ips de materiales
                        If sutureMaterials IsNot Nothing Then
                            Dim rateManual As RateManual
                            Using modelRateManual As New MRateManual(Me.Tag)
                                rateManual = modelRateManual.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded
                            End Using
							Using modelIPS As New MIPSService(Me.Tag)
								If rateManual.MaterialNoBloodyIPSServiceId IsNot Nothing Then
									Dim ipsSutureMaterials = modelIPS.GetIPSServiceByIdSimple(rateManual.MaterialNoBloodyIPSServiceId).ObjectEmbbeded
									Dim procedureServiceTpm As New SurgicalProcedureService
									With procedureServiceTpm
										.IPSServiceParentId = serviceOrderDetail.IPSServiceId
										.IPSServiceId = ipsSutureMaterials.Id
										.ServiceAmount = 1
										.DefaultService = True
										.ValueItemServiceOrderDetail = 0
										.CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
										.ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
										.AllowValueChange = serviceOrderDetail.AllowValueChange
									End With
									listSurgicalProcedureService.Add(procedureServiceTpm)
								End If
							End Using
                        End If
                    End If

                    'consulto los detalles quirurgicos del servicio
                    If serviceOrderDetail.ServiceOrderDetailSurgical.Count = 0 Then
                        Dim listServiceDetailSurgical = model.ListSurgicalDetailByIdServiceOrderDetail(serviceOrderDetail.Id)
                        For Each item In listServiceDetailSurgical
                            serviceOrderDetail.ServiceOrderDetailSurgical.Add(item)
                        Next
                    End If

                    'asigno los valores al listado de los items por default y hago datasource
                    listSurgicalProcedureService.ForEach(Sub(x) x.DefaultService = False)
                    For Each itemDetailSurgical In serviceOrderDetail.ServiceOrderDetailSurgical
                        Dim surgicalDefault = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = itemDetailSurgical.IPSServiceId)
                        If surgicalDefault IsNot Nothing Then
                            surgicalDefault.ValueItemServiceOrderDetail = itemDetailSurgical.TotalSalesPrice
                            surgicalDefault.PerformsHealthProfessionalCode = itemDetailSurgical.PerformsHealthProfessionalCode
                            surgicalDefault.DefaultService = True
                            surgicalDefault.AllowValueChange = itemDetailSurgical.AllowValueChange
                        End If
                    Next

                    INDGcSurgery.DataSource = listSurgicalProcedureService
                    INDGvSurgery.ExpandAllGroups()
                End Using

                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSeSurgeryNumber.EditValue = .SurgeryNumber

                'si el servicio se iquida como SOAT cambio el datasource de las intervenciones quirurgicas para que aparezca la opcion de no cruento
                If .SurgicalInterventionType = 9 Then
                    INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionTypeNoBloody
                Else
                    If .ServiceType = 1 Then
                        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionTypeNoBloody
                    Else
                        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
                    End If

                End If
                INDGleSurgicalInterventionType.EditValue = .SurgicalInterventionType

                'consulto los detalle de la orden de servicio por el numero del evento del item a editar para mostrar cuantos eventos con el mismo numero existen
                If _listServiceOrderDetailSurgicalIntervention IsNot Nothing Then
                    Dim listEvent = _listServiceOrderDetailSurgicalIntervention.FindAll(Function(x) x.SurgeryNumber = .SurgeryNumber And x.Presentation = 2)
                    INDGcEvents.DataSource = listEvent
                    INDPceEvents.Text = listEvent.Count.ToString()
                End If
            Else
                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDGleLiquidationType.Properties.DataSource = ListLiquidationType
            End If
            INDTxtAuthorizationNumber.EditValue = .AuthorizationNumber
            INDGleLiquidationType.EditValue = .SettlementType

            If .ServiceOrderDetail3 IsNot Nothing Then
                INDSleIncludeService.EditValue = .ServiceOrderDetail3.IdTmp
            Else
                INDSleIncludeService.EditValue = .IncludeServiceOrderDetailId
            End If

            INDSePercent.EditValue = .RecoveryRatio
            INDGleSurchargeApply.EditValue = .SurchargeApply
            Me.TotalSalesPrice = .TotalSalesPrice
            Me.TaxValue = .TaxValue
            Me.GrossValue = .GrossValue
            INDSeIndividualDiscount.EditValue = .ThirdPartyDiscountPercentage
            _serviceValue = .GrandTotalSalesPrice
            _serviceValueQuotation = .GrandTotalSalesPrice
            ctrTmp.PrintInfo()

            QuotationServiceOrderDetailId = .QuotationServiceOrderDetailId
            If QuotationServiceOrderDetailId IsNot Nothing AndAlso QuotationServiceOrderDetailId > 0 Then
                SetReadOnlyControlsForQuotation = True
            End If
        End With
        editValueChangeLoadControl = False
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' Permite bloquear controles cuando se edita un registro que viene desde cotización
    ''' </summary>
    Private WriteOnly Property SetReadOnlyControlsForQuotation As Boolean
        Set(value As Boolean)
            INDSleCareGroup.Properties.ReadOnly = value
            INDSleThirdParty.Properties.ReadOnly = value
            INDsleHealthAdministrator.Properties.ReadOnly = value
            INDGleServiceType.Properties.ReadOnly = value
            INDSleServiceSoatIss.Properties.ReadOnly = value
            INDSleServiceCups.Properties.ReadOnly = value
            INDSeCount.Properties.ReadOnly = value
            INDsleApplyRIAS.Properties.ReadOnly = value
            INDsleRIASCups.Properties.ReadOnly = value
            INDsleDescription.Properties.ReadOnly = value
            INDSeSurgeryNumber.Properties.ReadOnly = value
            INDGleSurgicalInterventionType.Properties.ReadOnly = value
            INDGcSurgery.Enabled = Not value
            INDGleLiquidationType.Properties.ReadOnly = value
            INDSleIncludeService.Properties.ReadOnly = value
            INDSePercent.Properties.ReadOnly = value
            INDGleSurchargeApply.Properties.ReadOnly = value
            INDTxtValueTotal.Properties.ReadOnly = value
            INDSeIndividualDiscount.Properties.ReadOnly = value
        End Set
    End Property

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
											 Dim ListTmp As New List(Of ServiceOrderDetail)
											 ListTmp.AddRange(model.ListServiceOrderDetailsByAdmissionNumber(Admission))
											 If _listServiceOrderDetailDatasourceIncludeService Is Nothing Then
												 _listServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
											 End If
											 If ListTmp.Any() Then
												 For Each item In ListTmp
													 If Not _listServiceOrderDetailDatasourceIncludeService.Any(Function(x) x.Id = item.Id) Then
														 _listServiceOrderDetailDatasourceIncludeService.Add(item)
													 End If
												 Next
											 End If
											 If _listServiceOrderDetailDelete IsNot Nothing Then
												 For Each itemRemove In _listServiceOrderDetailDelete
													 _listServiceOrderDetailDatasourceIncludeService.RemoveAll(Function(x) x.IdTmp = itemRemove.IdTmp)
												 Next
											 End If

										 End Using
									 End Sub)
    End Function

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

    Private Sub OnLoadForm()
        LoadStatus()
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = ServiceOrderStatus
        Me.ChangedTaxesControls(Me._flagTaxInclude)

        'Si no esta guardada la orden de servicio se oculta el control del estado
        If ServiceOrderId = 0 Then
            BarraBotones.ControlHideStatus = False
        End If

        ctrTmp.PrintInfo()
        Task.Factory.StartNew(Sub()
                                  INDSlePerformsHealthProfessionalCode.BeginInvoke(Sub()
                                                                                       SetDataSourceCombo()
                                                                                       PerformsHealthProfessionalXPO = srvOrderModel.ListHealthCareProfessional()
                                                                                       INDDteDate.Properties.MaxValue = GetDateServer()
                                                                                       If _editMode = True Then
                                                                                           listServiceOrderDetailPopup = New List(Of ServiceOrderDetail)
                                                                                           listServiceOrderDetailPopup.Add(serviceOrderDetail)
                                                                                           LoadControlsForEdit()
                                                                                           If _allowEditItem = False Then
                                                                                               ReadOnlyControls(True)
                                                                                               INDBtnAddDetail.Enabled = False
                                                                                           End If
                                                                                       Else
                                                                                           cleaningControls = False
                                                                                           INDDteDate.EditValue = _serviceDate
                                                                                           CleanControls()
                                                                                           If _flagDasboardQuoted Then
                                                                                               INDGleServiceType.EditValue = 3
                                                                                               INDGleServiceType.ReadOnly = True
                                                                                           End If
                                                                                       End If
                                                                                   End Sub)
                              End Sub)
        Task.WaitAll()
    End Sub

    ''' <summary>
    ''' Método que valida si la parametrización en grupos de atención de los tipos de unidad me pide el número de autorización obligatorio
    ''' </summary>
    Private Sub ValidateAuthorizationNumber()
        'Se valida que esten diligenciados tanto el grupo de atención como la unidad funcional
        If INDSleCareGroup.EditValue IsNot Nothing AndAlso INDSlePerformsFuntionalUnit.EditValue IsNot Nothing Then

            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                'Se obtiene la unidad funcional para sacar el tipo de unidad
                Dim functionalUnitTemp = modelServiceOrder.GetFunctionalUnitById(INDSlePerformsFuntionalUnit.EditValue)

                'Se obtiene el tipo de unidad funcional
                Dim unitTypeFunctionalUnit = Utils.GetFunctionalUnitType(functionalUnitTemp.UnitType)

                'Se obtiene la parametrización del grupo de atención de los tipos de unidad para saber si el número de autorización es obligatorio
                Dim controlByTypeFunctionalUnit = modelServiceOrder.GetControlByTypeFunctionalUnit(INDSleCareGroup.EditValue, unitTypeFunctionalUnit)

                'Se valida que el número de autorización sea obligatoria
                INDLciAuthorizationNumber.AllowHide = True
                If controlByTypeFunctionalUnit IsNot Nothing AndAlso controlByTypeFunctionalUnit.MandatoryAuthorization Then
                    INDLciAuthorizationNumber.AllowHide = False
                End If
            End Using

        End If
    End Sub

    ''' <summary>
    ''' metodo que cambia el Label de los controles de valores
    ''' </summary>
    ''' <param name="_taxInclude"></param>
    Private Sub ChangedTaxesControls(_taxInclude As Boolean?)
        If _taxInclude Is Nothing Then
            Me.HideTaxesControls()
            Exit Sub
        End If
        INDLciTaxValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciGrossValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLciGrossValue.Text = String.Format("{0}", If(_taxInclude, "SubTotal", "Valor"))
        INDLciTxValueTotal.Text = String.Format("{0}", "Valor Unitario")
    End Sub

    ''' <summary>
    ''' Oculta los controles valor IVA y subTotal
    ''' </summary>
    Private Sub HideTaxesControls()
        INDLciTxValueTotal.Text = "Valor"
        INDLciTaxValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciGrossValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.TaxValue = 0
        Me.TotalSalesPrice = 0
        Me.GrossValue = 0
    End Sub

    ''' <summary>
    ''' metodo que establece el valor neto, bruto e impuesto
    ''' </summary>
    ''' <param name="taxInclude"></param>
    ''' <param name="totalSalesPrice"></param>
    Private Sub SetCalculateTaxValue(taxInclude As Boolean?, totalSalesPrice As Decimal, taxPercent As Decimal)
        editTxtValueLoadControl = True
        If taxInclude Is Nothing Then
            Me.HideTaxesControls()
            Exit Sub
        End If
        If taxInclude Then
            Me.TotalSalesPrice = totalSalesPrice
            Me.GrossValue = (totalSalesPrice / ((taxPercent / 100.0F) + 1))
            Me.TaxValue = If(taxPercent = 0, 0, (totalSalesPrice - Me.GrossValue))
            editTxtValueLoadControl = False
        Else
            Me.GrossValue = totalSalesPrice
            Me.TaxValue = (Me.GrossValue * (taxPercent / 100.0F))
            Me.TotalSalesPrice = Me.GrossValue + Me.TaxValue
            editTxtValueLoadControl = False
        End If
    End Sub

#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub FrmPopupServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDLciTaxValue.Text = $"Valor {ResourceManager.GetString("TaxName")}"
        OnLoadForm()
        'Se cargan los permisos del formulario ordenes de servicio
        BarraBotones.ActualizarPermisosBarra("755")
        INDSeIndividualDiscount.Enabled = If(BarraBotones.PermissionsForm.ContainsKey(115), True, False)
        AllowsAddAuthorizationControl = If(BarraBotones.PermissionsForm.ContainsKey(130), True, False)
    End Sub
#End Region

#Region "Shown"

    Private Sub FrmPopupServices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleCareGroup.Focus()
        If _careGroupAdmission IsNot Nothing Then
            If CInt(_careGroupAdmission) > 0 Then
                INDSleCareGroup.EditValue = CInt(_careGroupAdmission)
                Dim CareGroupCodeName = srvOrderModel.CareGroupById(CInt(_careGroupAdmission))
                INDSleCareGroup.Properties.NullText = CareGroupCodeName.CodeName
            End If
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmPopupServices_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If INDSleCareGroup.EditValue IsNot Nothing And cleaningControls = True Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        ElseIf INDSleServiceSoatIss.EditValue IsNot Nothing Or INDSleServiceCups.EditValue IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDescription.QueryPopUp
        If INDsleDescription.Properties.DataSource Is Nothing AndAlso INDSleServiceCups.EditValue IsNot Nothing Then
            INDsleDescription.Properties.DataSource = srvOrderModel.ListContractDescriptionsByCupsEntityId(INDSleServiceCups.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASCups_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIASCups.QueryPopUp
        If INDsleRIASCups.Properties.DataSource Is Nothing Then
            CupsCodeSearchRIAS = INDSleServiceCups.Text.Split(" - ")(0).Trim()
            INDsleRIASCups.Properties.DataSource = srvOrderModel.ListRIASCups(CupsCodeSearchRIAS)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSLeCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeCostCenter.QueryPopUp
        If INDSLeCostCenter.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CostCenterXPO Is Nothing Then
            CostCenterXPO = srvOrderModel.GetCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CareGroupXPO Is Nothing Then
            CareGroupXPO = srvOrderModel.ListCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleServiceSoatIss_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleServiceSoatIss.QueryPopUp
        If INDSleServiceSoatIss.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If SoatIssXPO Is Nothing Then
            If INDGleServiceType.EditValue = 2 Then
                SoatIssXPO = srvOrderModel.ListIssServicesByCareGroup(INDSleCareGroup.EditValue)
            Else
                SoatIssXPO = srvOrderModel.ListSoatByCareGroup(INDSleCareGroup.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleServiceCups_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleServiceCups.QueryPopUp
        If INDSleServiceCups.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CupsXPO Is Nothing Then
            CupsXPO = srvOrderModel.ListCUPS(INDSleCareGroup.EditValue, _listCupsIds)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleIncludeService_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleIncludeService.QueryPopUp
        INDGvSleIncludedService.ShowLoadingPanel()
        Await GetDetailsIncludedService()
        INDGvSleIncludedService.HideLoadingPanel()
        INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePerformsFuntionalUnit_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSlePerformsFuntionalUnit.QueryPopUp
        If INDSlePerformsFuntionalUnit.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If PerformsFuntionalUnitXPO Is Nothing Then
            PerformsFuntionalUnitXPO = srvOrderModel.ListFunctionalUnit()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ThirdPartyXPO Is Nothing Then
            ThirdPartyXPO = srvOrderModel.ListThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If HealthAdministratorXPO Is Nothing Then
            HealthAdministratorXPO = srvOrderModel.ListHealthAdministratorByStatus()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDescription.EditValueChanged
        If INDsleDescription.EditValue IsNot Nothing AndAlso editValueChangeLoadControl = False Then
            GetHomologationCups()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRIASCups.EditValueChanged
        If INDsleRIASCups.EditValue IsNot Nothing AndAlso editValueChangeLoadControl = False Then
            GetHomologationCups()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de si aplica a rias o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleApplyRIAS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyRIAS.EditValueChanged
        If editValueChangeLoadControl = False Then
            If INDsleApplyRIAS.EditValue <> Nothing Then
                If INDsleApplyRIAS.EditValue Then
                    INDlyItemRIASCups.HideControl(False)
                Else
                    serviceOrderDetail.RIASCupsId = Nothing
                    INDsleRIASCups.EditValue = Nothing
                    INDsleRIASCups.Properties.NullText = String.Empty
                    INDlyItemRIASCups.HideControl()
                End If
            Else
                INDsleRIASCups.EditValue = Nothing
                INDsleRIASCups.Properties.NullText = String.Empty
                INDlyItemRIASCups.HideControl()
                GetHomologationCups()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareGroup.EditValueChanged
        SoatIssXPO = Nothing
        CupsXPO = Nothing
        ThirdPartyXPO = Nothing
        HealthAdministratorXPO = Nothing

        If editValueChangeLoadControl = False Then
            HideShowFieldsRIASAndDescription()
        End If
        If INDSleCareGroup.EditValue IsNot Nothing Then

            'obtengo el grupo de atencion seleccionado 
            Dim careGroupTmp As Object
            ' Intenta obtener el objeto ContractCareGroupXpo de la fila seleccionada
            Dim focusedRow As Object = INDGvSleCareGroup.GetFocusedRow()
            If focusedRow IsNot Nothing AndAlso TypeOf focusedRow Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                Dim originalRow As Object = DirectCast(focusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                If TypeOf originalRow Is ContractCareGroupXpo Then
                    careGroupTmp = DirectCast(originalRow, ContractCareGroupXpo)
                End If
            End If
            ' Si no se obtuvo de la GridView, utiliza el modelo para obtenerlo por ID
            If careGroupTmp Is Nothing Then
                Using model As New Contract.MVP.MCareGroup(Me.Tag)
                    careGroupTmp = model.GetCareGroupByIdSimple(INDSleCareGroup.EditValue).ObjectEmbbeded
                End Using
            End If

            'Se valida si se requiere como obligatorio el no. de autorización dependiendo del grupo de atención
            HandlesAuthorizationNumber = False
            If careGroupTmp IsNot Nothing AndAlso careGroupTmp.AuthorizationRequired Then
                HandlesAuthorizationNumber = True
            End If

            Dim serviceOrderCareGruopType = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.CareGroupId = INDSleCareGroup.EditValue)

            Select Case careGroupTmp.CareGroupType
                Case 1 ' EAPB Con contrato
                    INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    If TypeOf careGroupTmp Is ContractCareGroupXpo Then
                        ' Cuando viene del grid XPO, ContractId es un objeto ContractXpo
                        Dim careGroupXpo = DirectCast(careGroupTmp, ContractCareGroupXpo)
                        INDsleHealthAdministrator.EditValue = careGroupXpo.ContractId.HealthAdministratorId.Id
                    Else
                        ' Cuando viene de una entidad de dominio, ContractId es un Integer
                        Using modelContract As New Presentation.Contract.MVP.MContract(Me.Tag)
                            Dim contract = modelContract.GetContractByIdSimple(careGroupTmp.ContractId).ObjectEmbbeded
                            INDsleHealthAdministrator.EditValue = contract.HealthAdministratorId
                        End Using
                    End If
                    Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                        INDSleThirdParty.EditValue = modelHealthAdministrator.GetHealthAdministratorByIdSimple(INDsleHealthAdministrator.EditValue).ObjectEmbbeded.ThirdPartyId
                    End Using
                Case 2, 4 'EAPB Sin Contrato y aseguradoras
                    INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If serviceOrderCareGruopType IsNot Nothing And serviceOrderCareGruopType?.HealthAdministratorId IsNot Nothing Then
                        INDsleHealthAdministrator.Properties.ReadOnly = True
                        INDsleHealthAdministrator.EditValue = serviceOrderCareGruopType.HealthAdministratorId
                        INDsleHealthAdministrator.Properties.NullText = serviceOrderCareGruopType.CodeNameHealthAdministrator
                    Else
                        Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                            Dim healthAdministratorTmp As HealthAdministrator = Nothing
                            If _healthAdministratorCrystal > 0 Then
                                healthAdministratorTmp = modelHealthAdministrator.GetHealthAdministratorByIdSimple(_healthAdministratorCrystal).ObjectEmbbeded
                            End If
                            If healthAdministratorTmp IsNot Nothing AndAlso healthAdministratorTmp.Id > 0 Then
                                INDsleHealthAdministrator.Properties.ReadOnly = False
                                INDsleHealthAdministrator.EditValue = healthAdministratorTmp.Id
                                INDsleHealthAdministrator.Properties.NullText = healthAdministratorTmp.Code + " - " + healthAdministratorTmp.Name
                            Else
                                INDsleHealthAdministrator.Properties.ReadOnly = False
                                INDsleHealthAdministrator.EditValue = Nothing
                                INDsleHealthAdministrator.Properties.NullText = Nothing
                            End If
                        End Using
                    End If

                    If INDsleHealthAdministrator.Properties.ReadOnly = False Then
                        If HealthAdministratorXPO Is Nothing Then
                            Using model As New MCtrFolio()
                                HealthAdministratorXPO = model.ListHealthAdministrator(careGroupTmp.EntityType)
                            End Using
                        End If
                    End If

                Case 3 'Particulares
                    INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    If serviceOrderCareGruopType IsNot Nothing Then
                        INDSleThirdParty.Properties.ReadOnly = True
                        INDSleThirdParty.EditValue = serviceOrderCareGruopType.ThirdPartyId
                        INDSleThirdParty.Properties.NullText = serviceOrderCareGruopType.NitNameThirdParty
                    Else
                        Using modelThird As New MThirdParty(Me.Tag)
                            Dim thirdTmp = modelThird.GetThirdParty(_patient.Split("-").ElementAt(0).Trim())
                            If thirdTmp IsNot Nothing AndAlso thirdTmp.Id > 0 Then
                                INDSleThirdParty.Properties.ReadOnly = False
                                INDSleThirdParty.EditValue = thirdTmp.Id
                                INDSleThirdParty.Properties.NullText = thirdTmp.Nit + " - " + thirdTmp.Name
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = "El paciente " + _patient + " no se encuentra creado como tercero en Indigo VIE"
                                INDSleCareGroup.EditValue = Nothing
                                INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                Exit Sub
                            End If

                        End Using

                    End If
            End Select

            If _editMode = True Then
                Exit Sub
            End If

            If careGroupTmp.DefaultManual > 0 Then
                INDGleServiceType.EditValue = careGroupTmp.DefaultManual
            Else
                INDGleServiceType.EditValue = 3
                careGroupTmp.DefaultManual = 3
            End If
            INDGleServiceType.Enabled = True

            If INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never And INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                If careGroupTmp.DefaultManual <= 2 Then
                    INDSleServiceSoatIss.Focus()
                Else
                    INDSleServiceCups.Focus()
                End If
            End If

            'dependiendo del tipo de servicio consulto las homologaciones
            If INDGleServiceType.EditValue = 3 Then
                GetHomologationCups()
            Else
                GetHomologationIssSoat()
            End If

        Else
            INDGleServiceType.Enabled = False
            INDGleServiceType.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.ServiceType = INDGleServiceType.EditValue
            End If
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRpTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDRpTxtValue.EditValueChanged
        'si la el editvalue se cambia desde el load controls me salgo del evento
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        Dim serviceOrderDetailSurgicalTmp = DirectCast(INDGvSurgery.GetFocusedRow(), SurgicalProcedureService)
        Dim value = DirectCast(sender, TextEdit)
        serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = serviceOrderDetailSurgicalTmp.IPSServiceId).FirstOrDefault().TotalSalesPrice = value.EditValue
        serviceOrderDetail.RateManualSalePrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
        Me.SetCalculateTaxValue(Me._flagTaxInclude, serviceOrderDetail.RateManualSalePrice, serviceOrderDetail.TaxPercent)
        serviceOrderDetail.GrossValue = Me.GrossValue
        serviceOrderDetail.TaxValue = Me.TaxValue
        serviceOrderDetail.SubTotalSalesPrice = Me.TotalSalesPrice
        serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
        INDTxtValueTotal.EditValue = serviceOrderDetail.TotalSalesPrice
        GetIndividualDiscount()
        PrintServiceValue()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleServiceCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleServiceCups.EditValueChanged
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
        HideShowFieldsRIASAndDescription()
        GetHomologationCups()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleSpecialtyPerformsHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSpecialtyPerformsHealthProfessional.EditValueChanged
        If editValueChangeLoadControl = False Then
            If INDGleServiceType.EditValue = 3 Then
                GetHomologationCups()
            Else
                GetHomologationIssSoat()
            End If
        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.PerformsProfessionalSpecialty = INDGleSpecialtyPerformsHealthProfessional.EditValue
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleServiceSoatIss_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleServiceSoatIss.EditValueChanged
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        GetHomologationIssSoat()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDteDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDate.EditValueChanged
        If editValueChangeLoadControl Then
            Exit Sub
        End If
        If INDGleServiceType.EditValue = 3 Then
            GetHomologationCups()
        Else
            GetHomologationIssSoat()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePerformsFuntionalUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePerformsFuntionalUnit.EditValueChanged
        FunctionalUnitCenterAttentionCode = Nothing
        If INDSlePerformsFuntionalUnit.EditValue IsNot Nothing Then
            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                Dim functionalUnitTemp = modelServiceOrder.GetFunctionalUnitById(INDSlePerformsFuntionalUnit.EditValue)
                FunctionalUnitCenterAttentionCode = functionalUnitTemp.BranchOfficeId.Code
            End Using
        End If

        If editValueChangeLoadControl = False Then
            If INDGleServiceType.EditValue = 3 Then
                GetHomologationCups()
            Else
                GetHomologationIssSoat()
            End If
        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.PerformsFunctionalUnitId = INDSlePerformsFuntionalUnit.EditValue
                serviceOrderDetail.CodeNameFunctionalUnit = IIf(INDSlePerformsFuntionalUnit.Text Is String.Empty, INDSlePerformsFuntionalUnit.Properties.NullText, INDSlePerformsFuntionalUnit.Text)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
                            INDTxtValueTotal.Properties.ReadOnly = False
                        Else
                            INDTxtValueTotal.Properties.ReadOnly = True
                        End If
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = If(BarraBotones.PermissionsForm.ContainsKey(115), True, False)
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                        If serviceOrderDetail.AllowValueChange = True Then
                            If serviceOrderDetail.Presentation = 2 Then
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                            Else
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                            End If
                        Else
                            serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                        End If

                        If serviceOrderDetail.Presentation = 2 Then
                            For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                                item.TotalSalesPrice = item.RateManualSalePrice
                                Dim serviceTmp = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = item.IPSServiceId)
                                serviceTmp.ValueItemServiceOrderDetail = item.TotalSalesPrice
                            Next
                            INDGcSurgery.RefreshDataSource()
                        End If

                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * INDSeCount.EditValue
                        _changeValue = True
                        Me.SetCalculateTaxValue(Me._flagTaxInclude, serviceOrderDetail.RateManualSalePrice, serviceOrderDetail.TaxPercent)
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
                        INDTxtValueTotal.Properties.ReadOnly = True
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = False
                        serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
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
                        INDTxtValueTotal.Properties.ReadOnly = True
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = False
                        serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = 0 'serviceOrderDetail.RateManualSalePrice
                        serviceOrderDetail.GrandTotalSalesPrice = 0
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        If serviceOrderDetail.Presentation = 2 Then
                            For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                                item.TotalSalesPrice = 0
                                Dim serviceTmp = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = item.IPSServiceId)
                                serviceTmp.ValueItemServiceOrderDetail = item.TotalSalesPrice
                            Next
                            INDGcSurgery.RefreshDataSource()
                        End If
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
                        INDTxtValueTotal.Properties.ReadOnly = True
                        INDSePercent.EditValue = 100
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = If(BarraBotones.PermissionsForm.ContainsKey(115), True, False)
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleIncludeService_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleIncludeService.EditValueChanged
        If INDSleIncludeService.EditValue IsNot Nothing AndAlso INDSleIncludeService.EditValue IsNot String.Empty Then
            If editValueChangeLoadControl = False Then
                INDSePercent.Enabled = True
                Me.TotalSalesPrice = 0
                Dim percentTmp = INDSePercent.EditValue
                INDSePercent.EditValue = 0
                INDSePercent.EditValue = percentTmp
                INDSeIndividualDiscount.Enabled = If(BarraBotones.PermissionsForm.ContainsKey(115), True, False)
                If INDGleLiquidationType.EditValue = 3 Or INDGleLiquidationType.EditValue = 2 Then
                    Dim serviceDetailTmp = DirectCast(INDGvSleIncludedService.GetFocusedRow, ServiceOrderDetail)
                    If serviceDetailTmp.Id > 0 Then
                        serviceOrderDetail.IncludeServiceOrderDetailId = serviceDetailTmp.Id
                    Else
                        serviceOrderDetail.ServiceOrderDetail3 = serviceDetailTmp
                    End If
                End If
            End If

        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.IncludeServiceOrderDetailId = Nothing
                serviceOrderDetail.ServiceOrderDetail3 = Nothing
                serviceOrderDetail.TotalSalesPrice = 0
                serviceOrderDetail.SubTotalSalesPrice = 0 'serviceOrderDetail.RateManualSalePrice
                serviceOrderDetail.RecoveryRatio = 0
                serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                serviceOrderDetail.ThirdPartyDiscount = 0
                serviceOrderDetail.GrandTotalSalesPrice = 0

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeIndividualDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeIndividualDiscount.EditValueChanged
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        GetIndividualDiscount()
        PrintServiceValue()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
                    listSurgicalDefault(i).AllowValueChange = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().AllowValueChange
                Next
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
            End If
            'inserto el registro que retorno el metodo en la mismo posicion que estaba
            listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
            _changeValue = True
            Me.SetCalculateTaxValue(True, serviceOrderDetail.TotalSalesPrice, serviceOrderDetail.TaxPercent)
            _changeValue = False
            GetIndividualDiscount()
            PrintServiceValue()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeSurgeryNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeSurgeryNumber.EditValueChanged
        If editValueChangeLoadControl = False Then
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
                            INDGleSurgicalInterventionType.Properties.ReadOnly = False
                            serviceOrderDetail.IsFirstEvent = False
                        End If
                    Else
                        INDGleSurgicalInterventionType.Properties.ReadOnly = False
                        serviceOrderDetail.IsFirstEvent = True
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
    Private Sub INDGleSurgicalInterventionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSurgicalInterventionType.EditValueChanged
        If serviceOrderDetail Is Nothing Then
            Return
        End If

        serviceOrderDetail.SurgicalInterventionType = CInt(INDGleSurgicalInterventionType.EditValue)

        Dim hasMaterial = listSurgicalProcedureService.Any(Function(x) x.ClassService = "Materiales Sutura")
        If hasMaterial And INDGleSurgicalInterventionType.EditValue <> 9 Then
            Return
        End If

        If editValueChangeLoadControl = False Then
            If INDGleSurgicalInterventionType.EditValue IsNot Nothing Then
                Dim indexItem As Integer
                If serviceOrderDetail.RateManualId Is Nothing AndAlso INDGleSurgicalInterventionType.EditValue <> 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se pueden llegar a calcular los porcentajes del procedimiento quirurgico, ya que no esta asociado un manual de tarifas."
                    INDGleSurgicalInterventionType.EditValue = 1
                    Exit Sub
                End If
                If INDGleSurgicalInterventionType.EditValue = 9 Then
                    'obtengo el item de materiales de sutura si existe
                    Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper())
                    'si existe un item para materiales lo elimino
                    If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                        Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault()
                        If surgicalDetailTmp IsNot Nothing Then
                            If surgicalDetailTmp.Id > 0 Then
                                AddSurgicalDetailRemove(surgicalDetailTmp)
                            Else
                                serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicalDetailTmp)
                            End If
                        End If
                        listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                    End If
                    'obtenemos el ips de material de sutura
                    GetIPSSutureMaterialsNoBloody(False)
                Else
                    'cuando sea normal
                    'obtengo el item de materiales de sutura si existe
                    Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper())
                    'si existe un item para materiales lo elimino
                    If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                        Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault()
                        If surgicalDetailTmp.Id > 0 Then
                            AddSurgicalDetailRemove(surgicalDetailTmp)
                        Else
                            serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicalDetailTmp)
                        End If
                        listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                    End If
                    indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
                    listServiceOrderDetailPopup.Remove(serviceOrderDetail)
                    Using modelIpsService As New MIPSService(Me.Tag)
                        Using model As New MServiceOrder(Me.Tag)
                            listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)
                            Dim ipsSuture = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract")?.ToUpper() And x.DefaultService = True)
                            If ipsSuture IsNot Nothing Then
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
                                        .AllowValueChange = serviceOrderDetail.AllowValueChange
                                    End With
                                    listSurgicalProcedureService.Add(procedureServiceTpm)
                                End If
                                'vuelvo a poner el valor que se calculo porque pudo cambiar cuando es no cruento
                                Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper()).FirstOrDefault()
                                If detailRigthRoom IsNot Nothing Then
                                    detailRigthRoom.TotalSalesPrice = detailRigthRoom.RateManualSalePrice
                                End If
                            End If
                            Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                            Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalDefault)
                            serviceOrderDetail = result.ObjectEmbbeded

                            'los detalles que estan como por defecto le asigno los valores de la entidad service order detail surgical que trae los valores calculado para cada item
                            'esto se hace porque el listado con el que se hace datasource a la rejilla es de otro tipo al que retorna el calculo del servicio
                            For i As Integer = 0 To listSurgicalDefault.Count - 1 Step 1
                                listSurgicalDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                                listSurgicalDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                                listSurgicalDefault(i).AllowValueChange = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().AllowValueChange
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

                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                Me.GrossValue = serviceOrderDetail.GrossValue
                Me.TaxValue = serviceOrderDetail.TaxValue
                GetIndividualDiscount()
                PrintServiceValue()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtAuthorizationNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtAuthorizationNumber.EditValueChanged
        If serviceOrderDetail IsNot Nothing Then
            serviceOrderDetail.AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSePercent_EditValueChanged(sender As Object, e As EventArgs) Handles INDSePercent.EditValueChanged
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If serviceOrderDetail Is Nothing Then
            Exit Sub
        End If
        Dim round = 1
        If serviceOrderDetail.RateManualId IsNot Nothing Then
            Using model As New MRateManual(Me.Tag)
                round = model.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded.RoundService
            End Using
        End If

        If INDSePercent.EditValue > 0 Then
            If INDGleLiquidationType.EditValue = 4 Then ' si es incluido en el mismo servicio

                If serviceOrderDetail.Presentation = 2 Then
                    For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                        item.TotalSalesPrice = Utils.RoundValue(CDec(item.RateManualSalePrice * INDSePercent.EditValue / 100), round)
                        Dim serviceTmp = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = item.IPSServiceId)
                        serviceTmp.ValueItemServiceOrderDetail = item.TotalSalesPrice
                    Next
                    INDGcSurgery.RefreshDataSource()
                    Dim subtotal = (From x In serviceOrderDetail.ServiceOrderDetailSurgical Select x.TotalSalesPrice).Sum()
                    Me.SetCalculateTaxValue(Me._flagTaxInclude, subtotal, serviceOrderDetail.TaxPercent)
                    serviceOrderDetail.SubTotalSalesPrice = Me.TotalSalesPrice
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                    serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
                    serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
                    Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                Else
                    serviceOrderDetail.SubTotalSalesPrice = Utils.RoundValue(CDec((serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue) * INDSePercent.EditValue / 100), round)
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                    serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
                    serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
                    Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                End If
                serviceOrderDetail.GrossValue = Me.GrossValue
                serviceOrderDetail.TaxValue = Me.TaxValue
            Else
                Dim serviceDetailTmp = DirectCast(INDGvSleIncludedService.GetFocusedRow, ServiceOrderDetail)
                If serviceDetailTmp Is Nothing Then
                    If serviceOrderDetail.IncludeServiceOrderDetailId IsNot Nothing AndAlso serviceOrderDetail.IncludeServiceOrderDetailId > 0 Then
                        serviceDetailTmp = _listServiceOrderDetailDatasourceIncludeService.Find(Function(x) x.IdTmp = serviceOrderDetail.IncludeServiceOrderDetailId)
                    Else
                        serviceDetailTmp = _listServiceOrderDetailDatasourceIncludeService.Find(Function(x) x.IPSServiceId = serviceOrderDetail.ServiceOrderDetail3.IPSServiceId And x.CUPSEntityId = serviceOrderDetail.ServiceOrderDetail3.CUPSEntityId)
                    End If

                End If
                serviceOrderDetail.SubTotalSalesPrice = Utils.RoundValue(CDec(serviceDetailTmp.TotalSalesPrice * INDSePercent.EditValue / 100), round)
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
				serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
				serviceOrderDetail.GrossValue = (serviceOrderDetail.TotalSalesPrice / ((serviceOrderDetail.TaxPercent / 100.0F) + 1))
				serviceOrderDetail.TaxValue = If(serviceOrderDetail.TaxPercent = 0, 0, (serviceOrderDetail.TotalSalesPrice - serviceOrderDetail.GrossValue))

				Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
				If serviceDetailTmp.Id > 0 Then
                    serviceOrderDetail.IncludeServiceOrderDetailId = serviceDetailTmp.Id
                Else
                    serviceOrderDetail.ServiceOrderDetail3 = serviceDetailTmp
                End If
            End If
        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                serviceOrderDetail.RecoveryRatio = 0
                serviceOrderDetail.IncludeServiceOrderDetailId = Nothing
                serviceOrderDetail.ServiceOrderDetail3 = Nothing
                serviceOrderDetail.TotalSalesPrice = 0
                serviceOrderDetail.GrandTotalSalesPrice = 0
                Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice

                If serviceOrderDetail.Presentation = 2 Then
                    For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                        item.TotalSalesPrice = item.RateManualSalePrice
                        Dim serviceTmp = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = item.IPSServiceId)
                        serviceTmp.ValueItemServiceOrderDetail = item.TotalSalesPrice
                    Next
                    INDGcSurgery.RefreshDataSource()
                End If
            End If
        End If
        If INDSeIndividualDiscount.EditValue > 0 Then
            GetIndividualDiscount()
        End If
        PrintServiceValue()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValueTotal.EditValueChanged
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If editTxtValueLoadControl = True Then
            Exit Sub
        End If
        If serviceOrderDetail IsNot Nothing AndAlso INDTxtValueTotal.EditValue IsNot Nothing Then
            If INDTxtValueTotal.Properties.ReadOnly = False AndAlso _changeValue = False Then
                Me.SetCalculateTaxValue(True, INDTxtValueTotal.EditValue, serviceOrderDetail.TaxPercent)
                serviceOrderDetail.SubTotalSalesPrice = Me.TotalSalesPrice
                serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                serviceOrderDetail.GrossValue = Me.GrossValue
                serviceOrderDetail.TaxValue = Me.TaxValue
                GetIndividualDiscount()
                PrintServiceValue()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePerformsHealthProfessionalCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePerformsHealthProfessionalCode.EditValueChanged
        If INDSlePerformsHealthProfessionalCode.EditValue IsNot Nothing Then
            SetSpecialties(healthProfessional, INDGleSpecialtyPerformsHealthProfessional)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleThirdParty.EditValueChanged
        If serviceOrderDetail IsNot Nothing AndAlso INDSleThirdParty.EditValue IsNot Nothing Then
            serviceOrderDetail.ThirdPartyId = INDSleThirdParty.EditValue
            If INDSleThirdParty.Text IsNot String.Empty Then
                serviceOrderDetail.NitNameThirdParty = INDSleThirdParty.Text
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleHealthAdministrator_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHealthAdministrator.EditValueChanged
        If serviceOrderDetail IsNot Nothing AndAlso INDsleHealthAdministrator.EditValue IsNot Nothing Then
            serviceOrderDetail.HealthAdministratorId = INDsleHealthAdministrator.EditValue
            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                Dim healthAdministrator = modelHealthAdministrator.GetHealthAdministratorByIdSimple(INDsleHealthAdministrator.EditValue)
                serviceOrderDetail.ThirdPartyId = healthAdministrator.ObjectEmbbeded.ThirdPartyId
                serviceOrderDetail.HealthAdministratorId = healthAdministrator.ObjectEmbbeded.Id
                serviceOrderDetail.CodeNameHealthAdministrator = healthAdministrator.ObjectEmbbeded.Code + " - " + healthAdministrator.ObjectEmbbeded.Name
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeCount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeCount.EditValueChanged
        If editValueChangeLoadControl Then
            Exit Sub
        End If
        If serviceOrderDetail IsNot Nothing Then
            serviceOrderDetail.InvoicedQuantity = INDSeCount.EditValue
            GetIndividualDiscount()
            PrintServiceValue()
        End If
    End Sub

#End Region

#Region "RecordNavigationChangeEvent"
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        editValueChangeLoadControl = True
        INDsleApplyRIAS.EditValue = Nothing
        INDsleRIASCups.EditValue = Nothing
        INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        editValueChangeLoadControl = False
        LoadControls(Record)
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' se habilita el seleccionado entero en la caja de texto del spinEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeCount_Click(sender As Object, e As EventArgs) Handles INDSeCount.Click
        INDSeCount.SelectAll()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDBtnAddDetail.Click
        If errorsHomologation.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorsHomologation.ToString()
            Exit Sub
        End If
        If errorsGetValue IsNot Nothing AndAlso errorsGetValue.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorsGetValue
            Exit Sub
        End If
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        'Variable que permite saber si se va a importar un detalle de cotización
        Dim processImportQuotation As Boolean = False

        'Se valida si los servicios necesitan cotización, siempre y cuando el registro no venga imoprtada desde una cotización
        If QuotationServiceOrderDetailId Is Nothing AndAlso listServiceOrderDetailPopup IsNot Nothing Then
            Dim messageQuoted = srvOrderModel.GetCUPSWithQuoted(listServiceOrderDetailPopup)
            If Not String.IsNullOrEmpty(messageQuoted) AndAlso IsFormQuotation = False Then
                If MessageIndigo.Show(messageQuoted, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    If Me._requestQuoteServices Then
                        Exit Sub
                    End If
                Else
                    processImportQuotation = True
                End If
            End If
        End If

        'Si el proceso viene desde una cotización se valida que el mismo valor que tiene la cotización lo tenga el registro a editar
        If QuotationServiceOrderDetailId IsNot Nothing AndAlso QuotationServiceOrderDetailId > 0 Then
            If _serviceValueQuotation <> _serviceValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor de la cotización " + _serviceValueQuotation.ToString() + " es diferente al valor del item " + _serviceValue.ToString()
                Exit Sub
            End If
        End If

        If processImportQuotation Then 'Si se ejecuta el proceso de importación de cotización
            QuotationProcess()
        Else 'Si se ejecuta el proceso normal de agregar o editar el detalle de la orden de servicio
            NormalProcess()
        End If
    End Sub

    ''' <summary>
    ''' Método que se ejecuta cuando el usuario va a importar la información de la cotización
    ''' </summary>
    Private Sub QuotationProcess()
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportQuotation()
            AddHandler Formulario.ImportEvent, AddressOf ImportInfo
            Formulario.PatientCode = Patient.Split(" - ")(0).ToString()
            Formulario.ListCupsEntityId = (From x In listServiceOrderDetailPopup Select x.CUPSEntityId.Value).ToList()
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1054
            Formulario.Height = 500
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que se ejecuta con el resultado de la importación
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddImportQuotationServiceOrderDetail)
        If e IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo.Count > 0 Then
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
                Exit For
            Next
            LoadControlsForEdit()
        End If
    End Sub

    ''' <summary>
    ''' Método que se ejecuta cuando no se realiza el proceso importación de cotización
    ''' </summary>
    Private Async Sub NormalProcess()
        If serviceOrderDetail Is Nothing Then
            Exit Sub
        End If

        If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count = 1 Then
            If serviceOrderDetail.Presentation = 2 Then
                serviceOrderDetail.SurgeryNumber = CInt(INDSeSurgeryNumber.EditValue)
            End If
        End If

        'Listado de parámetros para enviar a validar las rias
        Dim ListParameters As New List(Of Tuple(Of String, Integer, String, Integer, DateTime))

        For Each item In listServiceOrderDetailPopup
            If item.Id = 0 Then
                item.IdTmp = _idTmp
                _idTmp -= 1
            End If

            Dim DeleteIds As New ObjectList
            If listServiceOrderDetailPopupDelete IsNot Nothing AndAlso listServiceOrderDetailPopupDelete.Count > 0 Then
                listServiceOrderDetailPopupDelete.ForEach(Sub(x) DeleteIds.Add(x.Id))
            End If
            Dim ExcludeId = String.Join(",", DeleteIds)
            item.OperatingUnitId = BarraBotones.OperatingUnitValue
            item.PatientCode = Patient.Split(" - ")(0).ToString()
            item.AdmissionNumber = Admission
            item.ExcludeIds = ExcludeId

            'Se valida si se puede agregar el detalle de rias
            If item.ApplyRIAS IsNot Nothing AndAlso item.ApplyRIAS Then
                ListParameters.Add(New Tuple(Of String, Integer, String, Integer, DateTime)(_patient.Split(" - ")(0).Trim(), item.RIASCupsId, item.CodeNameCups.Split(" - ")(0).Trim(), item.InvoicedQuantity, item.ServiceDate))
            End If
        Next

        'Si hay datos en el listado de rias se envia al sp para validar
        If ListParameters.Count > 0 Then
            'Se envia las rias al sp para validar si se pueden agregar
            Dim resultValidateRIAS As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) = Await srvOrderModel.SP_RIAS_ValidacionCUPSRIAS(ListParameters)
            If resultValidateRIAS.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = resultValidateRIAS.Message
                Exit Sub
            End If

            'Mensajes de la validacion de las cantidades de rias
            Dim errorsValidateQuantityRias As New StringBuilder

            'Se validan las cantidades de las rias
            'Se recorre el resultado de las validaciones
            For Each item In resultValidateRIAS.ObjectEmbbeded
                'Se valida que si el codigoMensaje viene nulo es porque se escogió una rias que maneja una regla SIN REGLA
                If item.CodigoMensaje Is Nothing Then
                    Continue For
                End If

                'Cantidad para validar
                Dim quantity As Integer = 0

                'Se suma las cantidades que hay en la rejilla
                If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
                    quantity = (From x In ListCompare Where x.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso x.RIASCupsId = item.RiasCupsId Select x.InvoicedQuantity).Sum()
                End If

                'Se unifica las cantidades de la rejilla con la cantidad a agregar
                quantity += (From x In listServiceOrderDetailPopup Where x.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso x.RIASCupsId = item.RiasCupsId Select x.InvoicedQuantity).Sum()

                'Si la cantidad limite - la cantidad realizada es menor a la cantidad a agregar, se arroja mensaje error
                If (item.Frecuencia - item.cantidadRealizadas) < quantity Then
                    errorsValidateQuantityRias.AppendLine("No se puede agregar el cups " + item.NombreCUPS + " con la RIAS " + item.NombreRuta + " porque la cantidad es superior a la cantidad que se puede realizar(" + (item.Frecuencia - item.cantidadRealizadas).ToString() + ")")
                End If

                'Se actualiza el campo de cantidades realizadas
                listServiceOrderDetailPopup.ForEach(Sub(x)
                                                        If x.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso x.RIASCupsId = item.RiasCupsId Then
                                                            x.RealizedQuantity = item.cantidadRealizadas
                                                        Else
                                                            x.RealizedQuantity = 0
                                                        End If
                                                    End Sub)
            Next

            'Se retorna el error si lo hay
            If errorsValidateQuantityRias.ToString().Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsValidateQuantityRias.ToString()
                Exit Sub
            End If
        End If

        If Not serviceOrderDetail.IsServiceOrderDetailControlJustify Then
            Using model As New MServiceOrder(Me.Tag)
                Dim result = Await model.ValidateServiceOrderDetail(listServiceOrderDetailPopup)
                If Not result.StateResult Then
                    If Not AllowsAddAuthorizationControl Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        Exit Sub
                    End If

                    If MessageIndigo.Show("El número de la autorización asociada al servicio a agregar ya se encuentra registrada en el Control de Autorización de Servicios Detallados, desea Continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If

                    Using popUp As New FrmPopupServiceOrderDetailControl()
                        Dim transparent As New FrmTransparent(popUp, False)
                        If transparent.ShowDialog(Me) <> System.Windows.Forms.DialogResult.OK Then
                            Exit Sub
                        End If

                        serviceOrderDetail.IsServiceOrderDetailControlJustify = True
                        serviceOrderDetail.ServiceOrderDetailControlJustification = popUp.Justify
                    End Using
                End If
            End Using
        End If

        If _editMode = False Then
            Dim args As New AddServiceEventArgs
            args.ListServiceOrderDetail = listServiceOrderDetailPopup
            args.EditMode = False
            cleaningControls = False
            RaiseEvent AddServiceOrderDetail(Nothing, args)

            'Si se esta agregando se asignan los items al listado de comparación ya que este listado solo es enviado cuando le dan click en abrir form modal, y
            'al agregar uno nuevo el modal nunca se cierra por lo tanto toca actualizar el listado de comparación para validar las rias
            If ListCompare Is Nothing Then
                ListCompare = New List(Of ServiceOrderDetail)
            End If

            If listServiceOrderDetailPopup IsNot Nothing Then
                ListCompare.AddRange(listServiceOrderDetailPopup.Select(Function(x) x.CloneEntity()).Cast(Of ServiceOrderDetail).ToList())
            End If

            CleanControls()
        Else
            Dim args As New AddServiceEventArgs
            args.ListServiceOrderDetail = listServiceOrderDetailPopup
            args.ListServiceOrderDetailDelete = listServiceOrderDetailPopupDelete
            args.EditMode = True
            cleaningControls = True
            RaiseEvent AddServiceOrderDetail(Nothing, args)
            CleanControls()
            Me.Close()
        End If
    End Sub

#End Region

#Region "ShowingEditor"
    Private Sub INDGvSurgery_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvSurgery.ShowingEditor
        Dim surgicalProcedireService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
        If surgicalProcedireService.DefaultService = True And surgicalProcedireService.AllowValueChange = True Then

            INDRpTxtValue.ReadOnly = False

        Else
            INDRpTxtValue.ReadOnly = True
        End If
        If surgicalProcedireService.ClassService?.ToUpper() = ResourceManager.GetString("None", "Contract").ToUpper() _
            Or surgicalProcedireService.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper() _
            Or surgicalProcedireService.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper() _
            Or surgicalProcedireService.ClassService?.ToUpper() = ResourceManager.GetString("SurgicalInstrumentation", "Contract").ToUpper() Then
            INDRptSleHealthProfessional.ReadOnly = True
        Else
            If surgicalProcedireService.DefaultService Then

                INDRptSleHealthProfessional.ReadOnly = False

            Else
                INDRptSleHealthProfessional.ReadOnly = True
            End If
        End If
    End Sub
#End Region

#Region "EditValueChanging"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleSurgicalInterventionType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDGleSurgicalInterventionType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            If listServiceOrderDetailPopup IsNot Nothing AndAlso listServiceOrderDetailPopup.Count > 1 Then
                If e.NewValue = 1 Then
                    For Each item In listServiceOrderDetailPopup
                        Dim listEvents As New List(Of ServiceOrderDetail)(_listServiceOrderDetailSurgicalIntervention.ToArray())
                        Dim listDetails As New List(Of ServiceOrderDetail)(listServiceOrderDetailPopup.ToArray())
                        listDetails.Remove(item)
                        listEvents.AddRange(listDetails)
                        Dim surgicalDetailBasic = listEvents.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.IdTmp <> item.IdTmp)
                        If surgicalDetailBasic IsNot Nothing Then
                            If INDSeSurgeryNumber.EditValue Is Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un evento"
                                e.Cancel = True
                                Exit Sub
                            End If
                            Mensaje(EeventViewerImages.Advertencia) = "En el evento " + (INDSeSurgeryNumber.EditValue).ToString() + " ya existen servicios y no se puede agregar uno basico"
                            e.Cancel = True
                            Exit Sub
                        End If
                    Next
                End If
            Else
                If editValueChangeLoadControl = False Then
                    If e.NewValue = 1 Then
                        Dim surgicalDetailBasic = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.SurgeryNumber = INDSeSurgeryNumber.EditValue And x.IdTmp <> serviceOrderDetail.IdTmp)
                        If surgicalDetailBasic IsNot Nothing Then
                            If INDSeSurgeryNumber.EditValue IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = "En el evento " + (INDSeSurgeryNumber.EditValue).ToString() + " ya existen servicios y no se puede agregar uno basico"
                            End If
                            e.Cancel = True
                            Exit Sub
                        End If
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
    Private Sub INDSlePerformsHealthProfessionalCode_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSlePerformsHealthProfessionalCode.EditValueChanging
        If e.NewValue IsNot Nothing AndAlso e.NewValue IsNot String.Empty Then
            Dim codeProfessional = e.NewValue.ToString().Trim
            Using model As New MServiceOrder(Me.Tag)
                healthProfessional = model.GetCareProfessionalByCode(codeProfessional)?.FirstOrDefault()
            End Using
            If healthProfessional Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = $"No se encontro un profesional de la salud con código {codeProfessional}"
                e.Cancel = True
                Exit Sub
            End If
            Using model As New MThirdParty(Me.Tag)
                thirdParty = model.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                If thirdParty.Id = 0 Then
                    'si el medico no esta creado como tercero en la BD no continua el proceso
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", MODULE_NAME), healthProfessional.CodeName)
                    e.Cancel = True
                    Exit Sub
                End If
            End Using
            INDGleSpecialtyPerformsHealthProfessional.Enabled = True
            If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Dim professionalSelected = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() <> ResourceManager.GetString("Surgeon", "Contract").ToUpper() _
                                                                                        And x.PerformsHealthProfessionalCode = e.NewValue)
                If professionalSelected IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Este profesional ya esta seleccionado como " + professionalSelected.ClassService
                    e.Cancel = True
                    Exit Sub
                End If

                Dim rowIndex = INDGvSurgery.FocusedRowHandle
                'obtengo el registro de la clase cirujano y le pongo el mismo medico que se selecciona para mostrar en la rejilla
                Dim surgicalProcedure = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("Surgeon", "Contract").ToUpper() And x.DefaultService = True)
                surgicalProcedure.PerformsHealthProfessionalCode = e.NewValue
                Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedure.IPSServiceId).FirstOrDefault()
                'cambio el medico tambn a la entidad ServiceOrderDetailSurgical
                surgicalDetailTmp.PerformsHealthProfessionalCode = e.NewValue
                surgicalDetailTmp.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowIndex
            End If
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.PerformsHealthProfessionalCode = e.NewValue
                serviceOrderDetail.PerformsHealthProfessionalThirdPartyId = thirdParty.Id
            End If
        Else
            INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptSleHealthProfessional_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSleHealthProfessional.EditValueChanging
        If serviceOrderDetail IsNot Nothing Then
            If e.NewValue IsNot Nothing Then
                Dim SurgicalProcedureService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
                Dim healthProfessional As HealthCareProfessionalXpo
                Using model As New MServiceOrder(Me.Tag)
                    Dim healthProfessionalTmp = model.GetCareProfessionalByCode(e.NewValue.ToString().Trim())

                    If healthProfessionalTmp Is Nothing OrElse Not healthProfessionalTmp?.Any() Then
                        Mensaje(EeventViewerImages.Advertencia) = $"No se encontró el profesional de codigo {e.NewValue.ToString().Trim()}"
                        e.Cancel = True
                        Exit Sub
                    End If

                    healthProfessional = healthProfessionalTmp?.FirstOrDefault
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

                    If SurgicalProcedureService?.ClassService?.ToUpper() = ResourceManager.GetString("Surgeon", "Contract").ToUpper() Then
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptCheDefaultService_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptCheDefaultService.EditValueChanging
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        Me.Cursor = ChangeCursorIndigo()
        Dim rowPosition = INDGvSurgery.FocusedRowHandle
        Dim surgicalProcedireService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
        If surgicalProcedireService.ClassService = "Ayudante" Then
            If e.NewValue = False Then
                'si se va a desactivar un item verifico que no sea el unico que esta seleccionado                
                If listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True).Count = 1 Then
                    e.Cancel = True
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                'disminuyo el valor del item que se deselecciono                
                serviceOrderDetail.RateManualSalePrice -= surgicalProcedireService.ValueItemServiceOrderDetail
                Me.SetCalculateTaxValue(Me._flagTaxInclude, serviceOrderDetail.RateManualSalePrice, Me._taxPercent)
                serviceOrderDetail.SubTotalSalesPrice = Me.TotalSalesPrice
                serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                serviceOrderDetail.GrossValue = Me.GrossValue
                serviceOrderDetail.TaxValue = Me.TaxValue

                Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedireService.IPSServiceId).FirstOrDefault()
                If surgicalDetailTmp.Id > 0 Then
                    AddSurgicalDetailRemove(surgicalDetailTmp)
                Else
                    serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicalDetailTmp)
                End If
                surgicalProcedireService.ValueItemServiceOrderDetail = 0
                surgicalProcedireService.DefaultService = False
                surgicalProcedireService.PerformsHealthProfessionalCode = Nothing
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                PrintServiceValue()
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
                        Me.GrossValue = serviceOrderDetail.GrossValue
                        Me.TaxValue = serviceOrderDetail.TaxValue
                        Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    'asigno los nuevos valores al listado del detalle quirurgico
                    For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                        Dim surgicalDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault()
                        If surgicalDetail IsNot Nothing Then
                            listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = surgicalDetail.TotalSalesPrice
                            listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = surgicalDetail.PerformsHealthProfessionalCode
                        End If
                    Next
                End Using
                'inserto el item en la misma posicion qe estaba antes de ser eliminado
                listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                PrintServiceValue()
            End If
        Else
            If e.NewValue = False Then
                'valido que exista un item seleccionado
                If surgicalProcedireService.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper() Then
                    If listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True).Count = 2 Then
                        Dim suturematerials = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper() And x.DefaultService = True)
                        If suturematerials IsNot Nothing Then
                            e.Cancel = True
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                    'si es derecho a sala debe existir dos seleccionados porque cuando se desactiva el elimina los materiales de sutura
                    If listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True).Count = 1 Then
                        e.Cancel = True
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If

                    'si se esta quitando el derecho a sala elimino los materiales de sutura
                    Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))

                    If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                        'elimino del listado por de la rejilla el item de materiales
                        listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                        'cambio los valores para que se vean reflejados en el precio del servicio
                        serviceOrderDetail.SubTotalSalesPrice -= surgicalProcedureServiceSutureMaterialsRemove.ValueItemServiceOrderDetail
                        serviceOrderDetail.RateManualSalePrice -= surgicalProcedureServiceSutureMaterialsRemove.ValueItemServiceOrderDetail
                        serviceOrderDetail.TotalSalesPrice -= surgicalProcedureServiceSutureMaterialsRemove.ValueItemServiceOrderDetail
                        serviceOrderDetail.GrossValue -= surgicalProcedureServiceSutureMaterialsRemove.ValueItemServiceOrderDetail
                        'elimino del detalle quirurgico los meteriales de sutura
                        serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault())
                    End If
                Else
                    If listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True).Count = 1 Then
                        e.Cancel = True
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                End If

                serviceOrderDetail.RateManualSalePrice -= surgicalProcedireService.ValueItemServiceOrderDetail
                Me.SetCalculateTaxValue(Me._flagTaxInclude, serviceOrderDetail.RateManualSalePrice, Me._taxPercent)
                serviceOrderDetail.SubTotalSalesPrice = Me.TotalSalesPrice
                serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                serviceOrderDetail.GrossValue = Me.GrossValue
                serviceOrderDetail.TaxValue = Me.TaxValue

                Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedireService.IPSServiceId).FirstOrDefault()
                If surgicalDetailTmp?.Id > 0 Then
                    AddSurgicalDetailRemove(surgicalDetailTmp)
                Else
                    serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicalDetailTmp)
                End If

                surgicalProcedireService.ValueItemServiceOrderDetail = 0
                surgicalProcedireService.DefaultService = False
                surgicalProcedireService.PerformsHealthProfessionalCode = Nothing
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                PrintServiceValue()
            ElseIf surgicalProcedireService.ClassService?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract").ToUpper() Then ' el item es derecho a sala
                'obtengo el item que debo remover del listado ya que el valor por defecto cambio
                Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)

                'remuevo el item de la entidad del detalle quirurgico
                If surgicalProcedureServiceRemove IsNot Nothing Then
                    Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault()
                    If surgicalDetailTmp.Id > 0 Then
                        AddSurgicalDetailRemove(surgicalDetailTmp)
                    Else
                        serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicalDetailTmp)
                    End If
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
                PrintServiceValue()
            Else
                If surgicalProcedireService?.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper() Then ' si es materiales de sutura

                    Dim ipsSutureMaterials = listSurgicalProcedureService.Find(Function(x) x.ClassService?.ToUpper() = ResourceManager.GetString("SutureMaterials", "Contract").ToUpper() _
                                                                                   AndAlso x.IPSServiceId = surgicalProcedireService.IPSServiceId)

                    Dim listDefaultValue = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                    listDefaultValue.ForEach(Sub(x) x.DefaultService = False)
                    listSurgicalProcedureService.Remove(ipsSutureMaterials)
                    GetIPSSutureMaterials(ipsSutureMaterials)
                Else
                    'obtengo el item que debo remover del listado ya que el valor por defecto cambio
                    Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                    If surgicalProcedureServiceRemove IsNot Nothing Then
                        'remuevo el item de la entidad del detalle quirurgico
                        Dim surgicaldetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault()
                        If surgicaldetailTmp?.Id > 0 Then
                            AddSurgicalDetailRemove(surgicaldetailTmp)
                        Else
                            serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicaldetailTmp)
                        End If
                    End If

                    'obtengo los items por defecto de la clase seleccionada
                    Dim listDefaultValue = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                    'los pongo todos en valor por defecto false
                    listDefaultValue.ForEach(Sub(x) x.DefaultService = False)
                    ' el nuevo item lo marco como por defecto
                    surgicalProcedireService.DefaultService = True
                    'obtenemos la posicion del item
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
                            Me.GrossValue = serviceOrderDetail.GrossValue
                            Me.TaxValue = serviceOrderDetail.TaxValue
                            Me.TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                        'asigno los nuevos valores al listado del detalle quirurgico
                        For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                            Dim surgicalDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault()
                            If surgicalDetail IsNot Nothing Then
                                listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = surgicalDetail.TotalSalesPrice
                                listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = surgicalDetail.PerformsHealthProfessionalCode
                            End If
                        Next
                    End Using
                    'inserto el item en la misma posicion qe estaba antes de ser eliminado
                    listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)
                End If
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                PrintServiceValue()
            End If
        End If
        Me.Cursor = Cursors.Default
    End Sub

    ''' <summary>
    ''' Metodo que elimina los materiales de Sutura
    ''' </summary>
    ''' <param name="surgicalProcedureServiceSutureMaterialsRemove"></param>
    Private Sub RemoveItemMaterials(surgicalProcedureServiceSutureMaterialsRemove As List(Of SurgicalProcedureService))
        If Not surgicalProcedureServiceSutureMaterialsRemove.IsNotNullAndAny Then
            Exit Sub
        End If

        For Each item In surgicalProcedureServiceSutureMaterialsRemove
            Dim surgicalDetailTmp = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = item.IPSServiceId).FirstOrDefault()
            If surgicalDetailTmp IsNot Nothing Then
                If surgicalDetailTmp.Id > 0 Then
                    AddSurgicalDetailRemove(surgicalDetailTmp)
                Else
                    serviceOrderDetail.ServiceOrderDetailSurgical.Remove(surgicalDetailTmp)
                End If
            End If
            listSurgicalProcedureService.Remove(item)
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSePercent_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSePercent.EditValueChanging
		If INDGleLiquidationType.EditValue = 4 Then
			If e.NewValue = 0 Then
				Mensaje(EeventViewerImages.Advertencia) = "El porcentage a cobrar no puede ser 0"
				e.Cancel = True
			End If
		End If
	End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeCount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSeCount.EditValueChanging
        If e.NewValue Is String.Empty Then
            e.Cancel = True
        End If
        If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = "Cuando el servicio seleccionado es quirurgico la cantidad solo puede ser 1"
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleSpecialtyPerformsHealthProfessional_KeyDown(sender As Object, e As KeyEventArgs) Handles INDGleSpecialtyPerformsHealthProfessional.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si el grupo de rias esta habilitado se coloca el focus en el control de aplica a rias
                INDsleApplyRIAS.Focus()
            Else 'Si el grupo de rias esta oculto realiza el focus dependiendo de otro grupo
                If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDSeSurgeryNumber.Focus()
                Else
                    INDTxtAuthorizationNumber.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeSurgeryNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSeSurgeryNumber.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDGleSurgicalInterventionType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopupServices_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDescription.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2096, Nothing, True)

        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCareGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("985", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleServiceSoatIss_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleServiceSoatIss.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmIPSService
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleServiceCups_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleServiceCups.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCupsEntity
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePerformsFuntionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlePerformsFuntionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmFunctionalUnit
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSLeCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSLeCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmCostCenter
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                OpenFormDialog(formulario)
            End Using
        End If
    End Sub

#End Region

#Region "BackgroundWorker"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgSOATISS_DoWork(sender As Object, e As DoWorkEventArgs)
        Try
            listHomologation = Nothing
            Using model As New MServiceOrder(Me.Tag)

                Dim listHomologationTmp = model.ListCupsHomologationByIpsServiceId(INDSleServiceSoatIss.EditValue)
                If listHomologation Is Nothing Then
                    listHomologation = New List(Of CupsHomologation)
                End If
                errorsHomologation = New StringBuilder()
                Dim manualType = 0
                Select Case INDGleServiceType.EditValue
                    Case 1
                        manualType = 3 'soat
                    Case 2
                        manualType = 1 'iss
                End Select
                Dim listCups = (From i In listHomologationTmp Select i.CupsEntityId).ToList()
                Dim result = model.GetHomologationCupsByListCUPS(listCups, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, INDSleServiceSoatIss.EditValue, manualType)

                If result.StateResult = False Then
                    errorsHomologation.AppendLine(result.Message)
                Else
                    listHomologation = result.ObjectEmbbeded
                End If
                If errorsHomologation.Length > 0 Then
                    e.Cancel = True
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgSOATISS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsHomologation?.ToString()
            AsyncLoader(False)
            CleanControlsErrorValueFields()
        Else

            If listHomologation.Count > 1 Then
                Dim formulario As New PopupHomologation
                AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.ListHomologation = listHomologation
                Dim transParent As New FrmTransparent(formulario, False)
                transParent.ShowDialog(Me)
            End If

            GetServiceValue()
        End If
        INDBtnAddDetail.Enabled = True
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgCUPS_DoWork(sender As Object, e As DoWorkEventArgs)

        Try
            listHomologation = Nothing

            errorsHomologation = New StringBuilder()
            Using model As New MServiceOrder(Me.Tag)

                Dim TempRiasId As Integer? = Nothing
                Dim TempDescriptionId As Integer? = Nothing
                If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always _
                    AndAlso INDsleRIASCups.EditValue IsNot Nothing Then
                    Dim info = srvOrderModel.GetRiasCupsByRiasCupsId(INDsleRIASCups.EditValue)
                    TempRiasId = info.RiasId
                End If
                If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue IsNot Nothing Then
                    Dim info = srvOrderModel.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                    TempDescriptionId = info.ContractDescriptionId.Id
                End If

                'consulto las homologaciones que tiene el cups
                Dim result = model.GetHomologationCups(INDSleCareGroup.EditValue, cupsEntity.Id, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, 0, 0, TempRiasId, TempDescriptionId)
                If result.StateResult = False Then

                    errorsHomologation.AppendLine(result.Message)
                    e.Cancel = True
                    Exit Sub
                End If
                listHomologation = result.ObjectEmbbeded
            End Using
        Catch ex As Exception
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgCUPS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsHomologation?.ToString()
            _bgCUPS.CancelAsync()
            AsyncLoader(False)
            CleanControlsErrorValueFields(False)
        Else
            'si el listado de las homologacione es mayor a 1 se muestra un popup con el listado para que el usuario seleccione cuales quiere cobrar
            If listHomologation.Count > 1 Then
                Dim formulario As New PopupHomologation
                AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
                formulario.StartPosition = FormStartPosition.CenterParent
                formulario.ListHomologation = listHomologation
                Dim transParent As New FrmTransparent(formulario, False)
                transParent.ShowDialog(Me)
            End If
            GetServiceValue()
            ''marco como verdadera la bandera para que se haga el calculo
            'runCalculationValue = True
            INDBtnAddDetail.Enabled = True
        End If
    End Sub

    Private Sub _bgGetServiceValue_DoWork(sender As Object, e As DoWorkEventArgs)

        Try
            errorsGetValue = String.Empty
            Dim TempRiasId As Integer? = Nothing
            Dim TempDescriptionId As Integer? = Nothing
            If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always _
                    AndAlso INDsleRIASCups.EditValue IsNot Nothing Then
                Dim info = srvOrderModel.GetRiasCupsByRiasCupsId(INDsleRIASCups.EditValue)
                TempRiasId = info.RiasId
            End If
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue IsNot Nothing Then
                Dim info = srvOrderModel.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                TempDescriptionId = info.ContractDescriptionId.Id
            End If

            Using model As New MServiceOrder(Me.Tag)
                Dim result = model.GetServiceValue(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), listHomologation, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id, TempRiasId, TempDescriptionId)
                If result.StateResult = False Then
                    e.Cancel = True
                    errorsGetValue = result.Message

                    Exit Sub
                End If
                If _editMode Then
                    If serviceOrderDetail.Id > 0 Then
                        If listServiceOrderDetailPopupDelete Is Nothing Then
                            listServiceOrderDetailPopupDelete = New ObjectList
                        End If

                        While serviceOrderDetail.ServiceOrderDetailSurgical.Count > 0
                            AddSurgicalDetailRemove(serviceOrderDetail.ServiceOrderDetailSurgical(0))
                        End While
                        listServiceOrderDetailPopupDelete.Add(serviceOrderDetail)
                    End If
                    If _listServiceOrderDetailSurgicalIntervention IsNot Nothing AndAlso _listServiceOrderDetailSurgicalIntervention.Count > 0 Then
                        Dim detailDelete = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.IdTmp = serviceOrderDetail.IdTmp)
                        _listServiceOrderDetailSurgicalIntervention.Remove(detailDelete)
                    End If
                    If _listServiceOrderDetailDatasourceIncludeService IsNot Nothing AndAlso _listServiceOrderDetailDatasourceIncludeService.Count > 0 Then
                        Dim detailDelete = _listServiceOrderDetailDatasourceIncludeService.Find(Function(x) x.IdTmp = serviceOrderDetail.IdTmp)
                        _listServiceOrderDetailDatasourceIncludeService.Remove(detailDelete)
                    End If

                End If
                listServiceOrderDetailPopup = result.ObjectEmbbeded
            End Using
        Catch ex As Exception
            e.Cancel = True
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub _bgGetServiceValue_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsGetValue
            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtAuthorizationNumber.EditValue = Nothing
            INDGleLiquidationType.EditValue = 1
            INDGleSurchargeApply.EditValue = False
            Me.SetCalculateTaxValue(Me._flagTaxInclude, 0, 0)
            INDSeIndividualDiscount.EditValue = 0
            AsyncLoader(False)
            ActivateControlsBehavior = False
            _serviceValue = 0
            ctrTmp.PrintInfo()
        Else
            Dim _serviceType = INDGleServiceType.EditValue
            If _editMode Then
                CleanControlsEditMode()
            End If
            If listServiceOrderDetailPopup.Count > 1 Then
                'si el listado de los servicios es mayor a uno le asigno el listado al control de navegacion de la barra botones para poder cambiar de registro
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "CodeNameIpsService"}, New ColumnInfo With {.Caption = "CUPS", .FieldName = "CodeNameCups"}}.ToList()
                Me.BarraBotones.FilterDataSource = listServiceOrderDetailPopup
            Else
                'cargo los controles con el unico registro que se retorno
                LoadControls(listServiceOrderDetailPopup(0))
            End If
            If INDTxtAuthorizationNumber.EditValue IsNot Nothing Then
                serviceOrderDetail.AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
            End If
            If errorsGetValue.Length = 0 Then
                serviceOrderDetail.ServiceType = _serviceType
                AsyncLoader(False)
                If _flagHomologation Then
                    ActivateControls = False
                End If
                LayoutControl1.BeginUpdate()
                'activo los controles del grupo del grupo de comportamiento
                ActivateControlsBehavior = True
                'si el grupo de cirugia esta visible asigno el foco al campo del evento sino al numero de autorizacion
                If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDSeSurgeryNumber.Focus()
                End If
                LayoutControl1.EndUpdate()
            Else
                _bgGetServiceValue.CancelAsync()
                AsyncLoader(False)
                INDSleCareGroup.Focus()
            End If

            INDBtnAddDetail.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgGetServiceValueByRIAS_DoWork(sender As Object, e As DoWorkEventArgs)

        errorsGetValue = String.Empty

        'Variable para establecer el valor que se envía al servicio que calcula el valor del cups
        Dim TempRiasId As Integer? = Nothing
        If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Se consulta el id de la rias ya que el que retorna el control es el id de riasCups
            Dim info = srvOrderModel.GetRiasCupsByRiasCupsId(INDsleRIASCups.EditValue)
            TempRiasId = info.RiasId
        ElseIf INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            TempRiasId = 0
        End If

        Using model As New MServiceOrder(Me.Tag)
            Dim result = model.GetServiceValueByRIAS(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), serviceOrderDetail.CUPSEntityId, serviceOrderDetail.IPSServiceId, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id, TempRiasId)
            If result.StateResult = False Then
                e.Cancel = True
                errorsGetValue = result.Message
                Exit Sub
            End If
            If _editMode Then
                If serviceOrderDetail.Id > 0 Then
                    If listServiceOrderDetailPopupDelete Is Nothing Then
                        listServiceOrderDetailPopupDelete = New ObjectList
                    End If

                    While serviceOrderDetail.ServiceOrderDetailSurgical.Count > 0
                        AddSurgicalDetailRemove(serviceOrderDetail.ServiceOrderDetailSurgical(0))
                    End While
                    listServiceOrderDetailPopupDelete.Add(serviceOrderDetail.MarkAsDeleted())
                End If
                If _listServiceOrderDetailSurgicalIntervention IsNot Nothing AndAlso _listServiceOrderDetailSurgicalIntervention.Count > 0 Then
                    Dim detailDelete = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.IdTmp = serviceOrderDetail.IdTmp)
                    _listServiceOrderDetailSurgicalIntervention.Remove(detailDelete)
                End If
                If _listServiceOrderDetailDatasourceIncludeService IsNot Nothing AndAlso _listServiceOrderDetailDatasourceIncludeService.Count > 0 Then
                    Dim detailDelete = _listServiceOrderDetailDatasourceIncludeService.Find(Function(x) x.IdTmp = serviceOrderDetail.IdTmp)
                    _listServiceOrderDetailDatasourceIncludeService.Remove(detailDelete)
                End If

            End If

            EntityTemp = result.ObjectEmbbeded
            If listServiceOrderDetailPopup Is Nothing OrElse listServiceOrderDetailPopup.Count = 0 Then
                listServiceOrderDetailPopup = New List(Of ServiceOrderDetail)
                listServiceOrderDetailPopup.Add(result.ObjectEmbbeded)
            Else
                Dim indexItem = listServiceOrderDetailPopup.IndexOf(serviceOrderDetail)
                listServiceOrderDetailPopup.Remove(serviceOrderDetail)
                listServiceOrderDetailPopup.Insert(indexItem, EntityTemp)
            End If
        End Using

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgGetServiceValueByRIAS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsGetValue
            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtAuthorizationNumber.EditValue = Nothing
            INDGleLiquidationType.EditValue = 1
            INDGleSurchargeApply.EditValue = False
            Me.SetCalculateTaxValue(Me._flagTaxInclude, 0, 0)
            INDSeIndividualDiscount.EditValue = 0
            AsyncLoader(False)
            ActivateControlsBehavior = False
            _serviceValue = 0
            ctrTmp.PrintInfo()
        Else
            Dim _serviceType = INDGleServiceType.EditValue
            If _editMode Then
                CleanControlsEditMode()
            End If
            LoadControls(EntityTemp)
            If INDTxtAuthorizationNumber.EditValue IsNot Nothing Then
                serviceOrderDetail.AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
            End If
            If errorsGetValue.Length = 0 Then
                serviceOrderDetail.ServiceType = _serviceType
                AsyncLoader(False)
                If _flagHomologation Then
                    ActivateControls = False
                End If
                LayoutControl1.BeginUpdate()
                'activo los controles del grupo del grupo de comportamiento
                ActivateControlsBehavior = True
                'si el grupo de cirugia esta visible asigno el foco al campo del evento sino al numero de autorizacion
                If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDSeSurgeryNumber.Focus()
                Else
                    INDTxtAuthorizationNumber.Focus()
                End If
                LayoutControl1.EndUpdate()
            Else
                _bgGetServiceValue.CancelAsync()
                AsyncLoader(False)
                INDSleCareGroup.Focus()
            End If

            INDBtnAddDetail.Enabled = True
        End If
    End Sub

#End Region

#Region "Popup"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptSleHealthProfessional_Popup(sender As Object, e As EventArgs) Handles INDRptSleHealthProfessional.Popup
        Dim search = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim view = search.Properties.View
        Dim obj As SurgicalProcedureService = CType(INDGvSurgery.GetFocusedRow(), SurgicalProcedureService)
        Select Case obj.ClassService?.ToUpper()
            Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                view.ActiveFilterString = "MEDPERCIR = 1 Or MEDPERCIR = 4"
            Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                view.ActiveFilterString = "MEDPERCIR = 2 Or MEDPERCIR = 4"
            Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                view.ActiveFilterString = "MEDPERCIR <> 0"
        End Select
        view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub
#End Region
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleServiceSoatIss_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleServiceSoatIss.KeyDown, INDSleServiceCups.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DirectCast(sender, SearchLookUpEdit).EditValue Is Nothing Then
                e.SuppressKeyPress = True
            Else
                INDSeCount.Focus()
            End If
        End If
    End Sub

#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        cleaningControls = True
        CleanControls()
    End Sub

#End Region

End Class
