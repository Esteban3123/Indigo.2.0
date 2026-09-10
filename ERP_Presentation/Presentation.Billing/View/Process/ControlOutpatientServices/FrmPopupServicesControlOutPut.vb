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
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class FrmPopupServicesControlOutPut

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
    ''' almacena el valor del impuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _taxValue As Decimal = 0
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
    ''' BackgroundWorker para validar si se muestra o no el grupo de RIAS
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bgRiasGroup As BackgroundWorker

    ''' <summary>
    ''' BackgroundWorker para obtener el valor del servicio desde rias
    ''' </summary>
    ''' <remarks></remarks>
    Dim _bgGetServiceValueByRIAS As BackgroundWorker

    ''' <summary>
    ''' Código del cups para poder consultar los RIAS del search
    ''' </summary>
    Dim CupsCodeSearchRIAS As String

    ''' <summary>
    ''' Permite saber si ejecuta el codigo que esta dentro del cambio de valor de los search
    ''' </summary>
    Dim DontValueChangeSearch As Boolean = True

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
    ''' guarda los permisos del formulario desde donde se esta llamando
    ''' </summary>
    ''' <returns></returns>
    Public Property PermissionsForm As Dictionary(Of Integer, String)

    ''' <summary>
    ''' se establece bandera que define si que controles se pueden editar cuando se modifica una preliquidacion hecha por una cita
    ''' </summary>
    ''' <returns></returns>
    Public Property FlagModPreCita As Boolean = False

    ''' <summary>
    ''' Bandera Impuesto Incluido companysettings
    ''' </summary>
    Private _flagTaxInclude As Boolean

#End Region

#Region "PROPERTIES"

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
            INDTxtValue.Enabled = value
            INDSeIndividualDiscount.Enabled = value
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

    ''' <summary>
    ''' propiedad que establece el valor de la bandera de impuestos incluidos
    ''' </summary>
    WriteOnly Property TaxInclude As Boolean
        Set(value As Boolean)
            Me._flagTaxInclude = value
        End Set
    End Property

    ''' <summary>
    ''' valor unitario
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
    ''' valor del iva
    ''' </summary>
    ''' <returns></returns>
    Property TaxValue As Decimal
        Get
            Return INDTxtIVA.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIVA.EditValue = value
        End Set
    End Property



    ''' <summary>
    ''' valor del subtotal
    ''' </summary>
    ''' <returns></returns>
    Property GrossValue As Decimal
        Get
            Return INDTxtSubtotal.EditValue
        End Get
        Set(value As Decimal)
            INDTxtSubtotal.EditValue = value
        End Set
    End Property

#End Region

#Region "METHODS"

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, Decimal)
        Return New Tuple(Of String, String, Decimal)(Patient, Format(_serviceValue, "c2"), Format(_taxValue, "c2"))
    End Function

    ''' <summary>
    ''' se consulta el cups
    ''' </summary>
    ''' <param name="CupsId"></param>
    Private Sub GetCupsEntity(CupsId As Integer)
        If CupsId = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo Encontrar el Cups"
            Exit Sub
        End If
        Using model As New MCupsEntity(Me.Tag)
            Dim resultCups = model.GetCupsEntityByIdSimple(CupsId)
            cupsEntity = resultCups.ObjectEmbbeded
        End Using
    End Sub

    ''' <summary>
    ''' limpia controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        LayoutControl1.BeginUpdate()
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
        INDDteDate.EditValue = GetDateServer()
        INDDteDate.Properties.ReadOnly = False
        INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSLeCostCenter.EditValue = Nothing
        INDSLeCostCenter.Properties.NullText = String.Empty
        INDSLeCostCenter.Properties.ReadOnly = True
        INDSlePerformsHealthProfessionalCode.EditValue = Nothing
        INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = False
        INDSlePerformsHealthProfessionalCode.Properties.NullText = String.Empty
        INDGcEvents.DataSource = Nothing
        INDPceEvents.Text = 0
        'INDTxtAuthorizationNumber.EditValue = Nothing
        INDTxtAuthorizationNumber.Properties.ReadOnly = False
        INDGleLiquidationType.EditValue = 1
        INDGleLiquidationType.Properties.ReadOnly = False
        INDSleIncludeService.EditValue = Nothing
        INDSleIncludeService.Properties.ReadOnly = False
        INDSleIncludeService.Properties.NullText = String.Empty
        INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDSePercent.EditValue = 0D
        INDSePercent.Properties.ReadOnly = False
        INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        TotalSalesPrice = 0D
        INDTxtValue.Properties.ReadOnly = False
        INDGleSurchargeApply.EditValue = 0D
        INDGleSurchargeApply.Properties.ReadOnly = False
        INDSeSurgeryNumber.EditValue = Nothing
        INDSeSurgeryNumber.Properties.ReadOnly = False
        INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
        INDGleSurgicalInterventionType.EditValue = Nothing
        INDGleSurgicalInterventionType.Properties.ReadOnly = False
        listSurgicalProcedureService = Nothing
        INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDGleSpecialtyPerformsHealthProfessional.EditValue = Nothing
        INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = False
        INDGleSpecialtyPerformsHealthProfessional.Properties.NullText = String.Empty
        listServiceOrderDetailPopup = Nothing
        PrintServiceValue()
        listHomologation = Nothing
        Me.BarraBotones.FilterDataSource = Nothing
        _editMode = False
        _flagHomologation = False
        INDBtnAddDetail.Text = ResourceManager.GetString("Add")

        INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleRIASCups.Properties.DataSource = Nothing
        INDsleApplyRIAS.EditValue = Nothing
        INDsleRIASCups.EditValue = Nothing
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles de los valores del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsErrorValueFields()
        LayoutControl1.BeginUpdate()
        serviceOrderDetail = Nothing
        ActivateControlsBehavior = False
        'INDTxtAuthorizationNumber.Text = String.Empty
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
        TotalSalesPrice = 0
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
            'ActivateControls = False
            _flagHomologation = True
        End If
        'GetServiceValue()
    End Sub

    ''' <summary>
    ''' metodo para obtener las homologaciones del iss o soat
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHomologationIssSoat()
        'LayoutControl1.BeginUpdate()
        'runCalculationValue = False
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

        'listHomologation = Nothing
        'Using model As New MServiceOrder(Me.Tag)
        '    AsyncLoader(True)
        '    Dim listHomologationTmp = model.ListCupsHomologationByIpsServiceId(INDSleServiceSoatIss.EditValue)
        '    If listHomologation Is Nothing Then
        '        listHomologation = New List(Of CupsHomologation)
        '    End If
        '    errorsHomologation = New StringBuilder()
        '    Dim manualType = 0
        '    Select Case INDGleServiceType.EditValue
        '        Case 1
        '            manualType = 3 'soat
        '        Case 2
        '            manualType = 1 'iss
        '    End Select
        '    For Each item In listHomologationTmp
        '        Dim result = model.GetHomologationCups(INDSleCareGroup.EditValue, item.CupsEntityId, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, INDSleServiceSoatIss.EditValue, manualType)
        '        If result.StateResult = False Then
        '            errorsHomologation.AppendLine(result.Message)
        '            Continue For
        '        End If
        '        listHomologation.AddRange(result.ObjectEmbbeded)
        '    Next
        '    AsyncLoader(False)
        '    If errorsHomologation.Length > 0 Then
        '        Mensaje(EeventViewerImages.Advertencia) = errorsHomologation.ToString()
        '        CleanControlsErrorValueFields()
        '        Exit Sub
        '    End If
        '    If listHomologation.Count > 1 Then
        '        Dim formulario As New PopupHomologation
        '        'formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        '        'formulario.Text = "hola"
        '        AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
        '        formulario.StartPosition = FormStartPosition.CenterParent
        '        formulario.ListHomologation = listHomologation
        '        Dim transParent As New FrmTransparent(formulario, False)
        '        transParent.ShowDialog(Me)
        '    End If

        '    runCalculationValue = True
        'End Using
        ' LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' metodo para obtener las homologaciones que tiene el cups para saber que servicio ips se debe utilizar si SOAT o ISS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetHomologationCups()
        'LayoutControl1.BeginUpdate()
        ''valido que los campos esten llenos para poder ejecutar la consulta

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
        If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue Is Nothing Then
            Exit Sub
        End If
        AsyncLoader(True)
        _bgCUPS = New BackgroundWorker
        _bgCUPS.WorkerSupportsCancellation = True
        AddHandler _bgCUPS.DoWork, AddressOf _bgCUPS_DoWork
        AddHandler _bgCUPS.RunWorkerCompleted, AddressOf _bgCUPS_RunWorkerCompleted
        _bgCUPS.RunWorkerAsync()
        'listHomologation = Nothing

        'runCalculationValue = False
        'errorsHomologation = New StringBuilder()
        'Using model As New MServiceOrder(Me.Tag)
        '    AsyncLoader(True)
        '    'consulto las homologaciones que tiene el cups
        '    Dim result = model.GetHomologationCups(INDSleCareGroup.EditValue, cupsEntity.Id, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue)
        '    If result.StateResult = False Then
        '        AsyncLoader(False)
        '        errorsHomologation.AppendLine(result.Message)
        '        Mensaje(EeventViewerImages.Advertencia) = result.Message
        '        CleanControlsErrorValueFields()
        '        Exit Sub
        '    End If
        '    AsyncLoader(False)
        '    listHomologation = result.ObjectEmbbeded
        '    'si el listado de las homologacione es mayor a 1 se muestra un popup con el listado para que el usuario seleccione cuales quiere cobrar
        '    If listHomologation.Count > 1 Then
        '        Dim formulario As New PopupHomologation
        '        AddHandler formulario.SetHomologation, AddressOf ReturnSetHomologation
        '        formulario.StartPosition = FormStartPosition.CenterParent
        '        formulario.ListHomologation = listHomologation
        '        Dim transParent As New FrmTransparent(formulario, False)
        '        transParent.ShowDialog(Me)
        '    End If
        '    'marco como verdadera la bandera para que se haga el calculo
        '    runCalculationValue = True
        'End Using
        'LayoutControl1.EndUpdate()
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

        'LayoutControl1.BeginUpdate()
        'valido que los campos requeridos esten diligenciados
        'Dim serviceOrderDetail As New ServiceOrderDetail
        'If INDSleCareGroup.EditValue Is Nothing Then
        '    Exit Sub
        'End If
        'If INDLciServiceSoatIss.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
        '    If INDSleServiceSoatIss.EditValue Is Nothing Then
        '        Exit Sub
        '    End If
        'Else
        '    If INDSleServiceCups.EditValue Is Nothing Then
        '        Exit Sub
        '    End If
        'End If
        'If INDDteDate.EditValue Is Nothing Then
        '    Exit Sub
        'End If
        'If INDSlePerformsFuntionalUnit.EditValue Is Nothing Then
        '    Exit Sub
        'End If
        'If INDGleSpecialtyPerformsHealthProfessional.EditValue Is Nothing Then
        '    Exit Sub
        'End If
        Using model As New MServiceOrder(Me.Tag)
            'consulto el valor del servicio con los parametros requeridos


            _bgGetServiceValue = New BackgroundWorker
            _bgGetServiceValue.WorkerSupportsCancellation = True
            AddHandler _bgGetServiceValue.DoWork, AddressOf _bgGetServiceValue_DoWork
            AddHandler _bgGetServiceValue.RunWorkerCompleted, AddressOf _bgGetServiceValue_RunWorkerCompleted
            _bgGetServiceValue.RunWorkerAsync()


            'AsyncLoader(True)
            'errorsGetValue = String.Empty
            'Dim result = model.GetServiceValue(listHomologation, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id)
            'If result.StateResult = False Then
            '    AsyncLoader(False)
            '    errorsGetValue = result.Message
            '    Mensaje(EeventViewerImages.Advertencia) = result.Message
            '    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            '    INDTxtAuthorizationNumber.EditValue = Nothing
            '    INDGleLiquidationType.EditValue = 1
            '    INDGleSurchargeApply.EditValue = 0
            '    INDTxtValue.EditValue = 0
            '    INDSeIndividualDiscount.EditValue = 0
            '    ActivateControlsBehavior = False
            '    Exit Sub
            'End If

            'listServiceOrderDetailPopup = result.ObjectEmbbeded
            'If listServiceOrderDetailPopup.Count > 1 Then
            '    'si el listado de los servicios es mayor a uno le asigno el listado al control de navegacion de la barra botones para poder cambiar de registro
            '    Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "CodeNameIpsService"}, New ColumnInfo With {.Caption = "CUPS", .FieldName = "CodeNameCups"}}.ToList()
            '    Me.BarraBotones.FilterDataSource = listServiceOrderDetailPopup
            'Else
            '    'cargo los controles con el unico registro que se retorno
            '    LoadControls(listServiceOrderDetailPopup(0))
            'End If
            'If errorsGetValue.Length = 0 Then
            '    AsyncLoader(False)
            '    'activo los controles del grupo del grupo de comportamiento
            '    ActivateControlsBehavior = True
            '    'si el grupo de cirugia esta visible asigno el foco al campo del evento sino al numero de autorizacion
            '    If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            '        INDSeSurgeryNumber.Focus()
            '    Else
            '        INDTxtAuthorizationNumber.Focus()
            '    End If
            'End If
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

    Private Sub LoadControlsForLiquidation(Record As ServiceOrderDetail)
        LayoutControl1.BeginUpdate()
        INDSleCareGroup.Properties.NullText = Record.CodeNameCareGroup
        INDSlePerformsFuntionalUnit.Properties.NullText = Record.CodeNameFunctionalUnit
        INDSlePerformsHealthProfessionalCode.Properties.NullText = Record.CodeNameHealthProfessional
        INDSleCareGroup.Enabled = False
        INDGleServiceType.Enabled = False
        INDSleServiceCups.Enabled = False
        INDSeCount.Enabled = False
        INDDteDate.Enabled = False
        INDSlePerformsFuntionalUnit.Enabled = False
        INDSLeCostCenter.Enabled = False
        INDSlePerformsHealthProfessionalCode.Enabled = False
        INDGleSpecialtyPerformsHealthProfessional.Enabled = False
        Me.EditMode = True

        serviceOrderDetail = Record
        With serviceOrderDetail
            'esta bandera se hace para que no se haga nada en los eventos editValueChanged de los controles mientras se asignan desde este metodo
            editValueChangeLoadControl = True
            'asigno valores a los campos y los pongo readOnly
            INDSleCareGroup.EditValue = .CareGroupId
            Using model As New MCareGroup(Me.Tag)
                Dim careGroupTmp = model.GetCareGroupByIdSimple(.CareGroupId)
                .CodeNameCareGroup = String.Concat(careGroupTmp.ObjectEmbbeded.Code, " - ", careGroupTmp.ObjectEmbbeded.Name)
                Select Case careGroupTmp.ObjectEmbbeded.CareGroupType
                    Case 1 ' EAPB Con contrato
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Using modelContract As New Presentation.Contract.MVP.MContract(Me.Tag)
                            Dim contract = modelContract.GetContractByIdSimple(careGroupTmp.ObjectEmbbeded.ContractId).ObjectEmbbeded
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministrator = modelHealthAdministrator.GetHealthAdministratorByIdSimple(contract.HealthAdministratorId)
                                .ThirdPartyId = healthAdministrator.ObjectEmbbeded.ThirdPartyId
                                .HealthAdministratorId = healthAdministrator.ObjectEmbbeded.Id
                            End Using
                        End Using
                    Case 2, 4 'EAPB Sin Contrato y aseguradoras
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If INDsleHealthAdministrator.EditValue IsNot Nothing Then
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministrator = modelHealthAdministrator.GetHealthAdministratorByIdSimple(INDsleHealthAdministrator.EditValue)
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
                INDSleCareGroup.Properties.NullText = .CodeNameCareGroup
                INDSleCareGroup.Properties.ReadOnly = True
                _flagHomologation = True
                INDGleServiceType.EditValue = 3
                _flagHomologation = False
                If INDGleServiceType.EditValue <= 2 Then
                    INDSleServiceSoatIss.EditValue = .IPSServiceId
                Else
                    INDSleServiceCups.EditValue = .CUPSEntityId
                End If
                INDSleServiceCups.Enabled = True
                INDSleServiceCups.Properties.NullText = Record.CodeNameCups
                INDSleServiceSoatIss.Properties.NullText = Record.CodeNameIpsService
            End Using

        End With
        LayoutControl1.EndUpdate()
    End Sub

    ''' <summary>
    ''' Oculta o muestra el grupo de RIAS
    ''' </summary>
    Private Sub ShowHideGroupRIAS(_cupsCode As String)
        'Se ejecuta en asyncrono la validación
        _bgRiasGroup = New BackgroundWorker
        _bgRiasGroup.WorkerSupportsCancellation = True
        AddHandler _bgRiasGroup.DoWork, AddressOf _bgRiasGroup_DoWork
        AddHandler _bgRiasGroup.RunWorkerCompleted, AddressOf _bgRiasGroup_RunWorkerCompleted
        _bgRiasGroup.RunWorkerAsync(_cupsCode)
    End Sub

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgRiasGroup_DoWork(sender As Object, e As DoWorkEventArgs)
        'Se obtiene el código del cups
        CupsCodeSearchRIAS = CStr(e.Argument)

        Using srvOrderModel As New MServiceOrder(Me.Tag)
            'Se obtiene el cups por código
            Dim cupsEntityXpo = srvOrderModel.GetCupsEntityByCode(CupsCodeSearchRIAS)

            'Si no existe se cancela
            If cupsEntityXpo Is Nothing Then
                e.Cancel = True
                Exit Sub
            End If

            'Si el cups no aplica a RIAS se cancela
            If cupsEntityXpo.ApplyRIAS = Nothing OrElse cupsEntityXpo.ApplyRIAS = False Then
                e.Cancel = True
                Exit Sub
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub _bgRiasGroup_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        'Si se cancela la solicitud y se oculta el grupo de rias
        If e.Cancelled Then
            INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            _bgRiasGroup.CancelAsync()
            Exit Sub
        End If

        'Se habilita el grupo de RIAS
        INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        DontValueChangeSearch = False
        'Si hay datos para asignar a los controles
        If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.ApplyRIAS IsNot Nothing Then
            INDsleApplyRIAS.EditValue = serviceOrderDetail.ApplyRIAS
        End If
        If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.RIASCupsId IsNot Nothing Then
            INDsleRIASCups.EditValue = serviceOrderDetail.RIASCupsId
        End If
        DontValueChangeSearch = True
    End Sub

    ''' <summary>
    ''' metodo para cargar controles con el registro seleccionado
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub LoadControls(Record As ServiceOrderDetail)
        LayoutControl1.BeginUpdate()
        serviceOrderDetail = Record
        With serviceOrderDetail
            'esta bandera se hace para que no se haga nada en los eventos editValueChanged de los controles mientras se asignan desde este metodo
            editValueChangeLoadControl = True
            INDSleCareGroup.EditValue = .CareGroupId
            '' se visualiza o no los campos de iva
            If .TaxedService Then
                INDLySubtotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLySubtotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            Using model As New MCareGroup(Me.Tag)
                Dim careGroupTmp = model.GetCareGroupByIdSimple(.CareGroupId)
                Select Case careGroupTmp.ObjectEmbbeded.CareGroupType
                    Case 1 ' EAPB Con contrato
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Using modelContract As New Presentation.Contract.MVP.MContract(Me.Tag)
                            Dim contract = modelContract.GetContractByIdSimple(careGroupTmp.ObjectEmbbeded.ContractId).ObjectEmbbeded
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministrator = modelHealthAdministrator.GetHealthAdministratorByIdSimple(contract.HealthAdministratorId)
                                .ThirdPartyId = healthAdministrator.ObjectEmbbeded.ThirdPartyId
                                .HealthAdministratorId = healthAdministrator.ObjectEmbbeded.Id
                            End Using
                        End Using
                    Case 2, 4 'EAPB Sin Contrato y aseguradoras
                        INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        If INDsleHealthAdministrator.EditValue IsNot Nothing Then
                            Using modelHealthAdministrator As New MHealthAdministrator(Me.Tag)
                                Dim healthAdministrator = modelHealthAdministrator.GetHealthAdministratorByIdSimple(INDsleHealthAdministrator.EditValue)
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

                'Si el grupo de atención aplica a RIAS y el proceso no sea lanzado desde el search de rias
                If careGroupTmp.ObjectEmbbeded.ApplyRIAS IsNot Nothing AndAlso careGroupTmp.ObjectEmbbeded.ApplyRIAS AndAlso IsSearchRias = False Then
                    Dim _cupsCode As String = .CodeNameCups.Split(" - ")(0).Trim()
                    ShowHideGroupRIAS(_cupsCode)
                End If

                'Si esta variable llega en true es porque se lanzó el proceso desde el cambio de valor de rias 
                'y se actualizan los campos de rias porque el calculo de valor me retorna un nuevo serviceOrderDetail
                If IsSearchRias Then
                    .ApplyRIAS = INDsleApplyRIAS.EditValue
                    .RIASCupsId = INDsleRIASCups.EditValue
                End If

                'Se vuelve a dejar la variable de control del cambio de rias
                IsSearchRias = False
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
                INDTxtValue.Properties.ReadOnly = False
                INDRpTxtValue.ReadOnly = False
            Else
                INDTxtValue.Properties.ReadOnly = True
                INDRpTxtValue.ReadOnly = True
            End If
            If .Presentation = 2 Then
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
                INDLciLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Using model As New MServiceOrder(Me.Tag)
                    PerformsHealthProfessionalRepositoryXPO = model.ListHealthCareProfessional()
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'consulto los detalles del servicio ips cuando es quirurgico
                    listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)

                    'los detalles que estan como por defecto le asigno los valores de la entidad service order detail surgical que trae los valores calculado para cada item
                    'esto se hace porque el listado con el que se hace datasource a la rejilla es de otro tipo al que retorna el calculo del servicio
                    For Each itemSurgicalDetail In serviceOrderDetail.ServiceOrderDetailSurgical

                        Dim surgicalDefault = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = itemSurgicalDetail.IPSServiceId)
                        surgicalDefault.DefaultService = True
                        If FlagModPreCita Then
                            itemSurgicalDetail.ChangeTracker.ChangeTrackingEnabled = False
                        End If
						'si el item es derecho a sala busco el ips para materiales de sutura
						If surgicalDefault.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) Then
							If .SurgicalInterventionType Is Nothing Or .SurgicalInterventionType < 9 Then
								GetIPSSutureMaterials(surgicalDefault)
							Else
								'si es cruento consulto el manual de tarifas para obtener el ips de materiales
								Dim rateManual As RateManual
								Using modelRateManual As New MRateManual(Me.Tag)
									rateManual = modelRateManual.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded
								End Using
								Dim ipsSutureMaterials As IPSService
								Using modelIPS As New MIPSService(Me.Tag)
									ipsSutureMaterials = modelIPS.GetIPSServiceByIdSimple(rateManual.MaterialNoBloodyIPSServiceId).ObjectEmbbeded
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
									'listSurgicalDefault.Add(procedureServiceTpm)
								End Using
								surgicalDefault.ValueItemServiceOrderDetail = itemSurgicalDetail.TotalSalesPrice
								surgicalDefault.PerformsHealthProfessionalCode = .PerformsHealthProfessionalCode
								'asigno valores al item materiales de sutura 
								Dim detailSuturematerial = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))
								detailSuturematerial.ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = ipsSutureMaterials.Id).FirstOrDefault().TotalSalesPrice
								detailSuturematerial.PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = ipsSutureMaterials.Id).FirstOrDefault().PerformsHealthProfessionalCode

							End If
						Else
							If errorsGetValue.Length = 0 Then
                                surgicalDefault.ValueItemServiceOrderDetail = itemSurgicalDetail.TotalSalesPrice
                                surgicalDefault.PerformsHealthProfessionalCode = itemSurgicalDetail.PerformsHealthProfessionalCode
                            End If
                        End If
                    Next
                    If errorsGetValue.Length = 0 Then
                        INDGcSurgery.DataSource = listSurgicalProcedureService
                        INDGvSurgery.ExpandAllGroups()
                    End If
                End Using
            Else
                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                TotalSalesPrice = .SubTotalSalesPrice
                TaxValue = .TaxValue
                GrossValue = .GrossValue
            End If
            INDSlePerformsHealthProfessionalCode.EditValue = .PerformsHealthProfessionalCode
            INDGleSpecialtyPerformsHealthProfessional.EditValue = .PerformsProfessionalSpecialty
            If errorsGetValue.Length = 0 Then
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
                    INDTxtValue.Properties.ReadOnly = True
                End If
                INDSeIndividualDiscount.EditValue = .ThirdPartyDiscountPercentage
                editValueChangeLoadControl = False
                BarraBotones.Focus()
                'obtengo el valor de todos los items para ponerlo en el control de usuario de la barra botones

                PrintServiceValue()
            End If
            If FlagModPreCita Then
                .ChangeTracker.ChangeTrackingEnabled = False
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
            _serviceValue = listServiceOrderDetailPopup.Sum(Function(x) x.TotalSalesPrice * x.InvoicedQuantity)
            _taxValue = listServiceOrderDetailPopup.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
        End If
        ctrTmp.PrintInfo()
    End Sub

    ''' <summary>
    ''' metodo para opculta, mostrar o cambiar nombre de los labels relacionados con iva
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AllowAndChangeControls()
        If _flagTaxInclude Then
            INDLySubtotal.Text = "Subtotal"
        Else
            INDLySubtotal.Text = "Valor"
            INDLyTotalValue.Text = "Valor Total"
        End If

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
				Dim existsRightRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase)).FirstOrDefault()
				If existsRightRoom Is Nothing Then
					Dim resultRightRoom = modelService.GetServiceValue(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), listHomologation.FindAll(Function(x) x.IPSServiceId = serviceOrderDetail.IPSServiceId And x.CupsEntityId = serviceOrderDetail.CUPSEntityId), INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id)
					serviceOrderDetail.ServiceOrderDetailSurgical.Add(resultRightRoom.ObjectEmbbeded(0).ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase)).FirstOrDefault())
					Dim rigthRoomDefault = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase))
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
					Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase)).FirstOrDefault()
					If detailRigthRoom IsNot Nothing Then
						detailRigthRoom.TotalSalesPrice = Utils.RoundValue(CDec(detailRigthRoom.RateManualSalePrice * rateManual.PercentageNoBloodyRoom / 100), rateManual.RoundService)
					End If
				End If
				'detailRigthRoom.RateManualSalePrice =
				serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
				serviceOrderDetail.RateManualSalePrice = serviceOrderDetail.SubTotalSalesPrice
				serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
				serviceOrderDetail.GrossValue = serviceOrderDetail.SubTotalSalesPrice

				'obetengo todos los valores por defecto 
				Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
				'asigno los nuevos valores al listado del detalle quirurgico
				For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
					listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
					listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
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
            GetIPSSutureMaterialsNoBloody(True)
        Else
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
                        'inserto el item en la misma posicion qe estaba antes de ser eliminado
                        listServiceOrderDetailPopup.Insert(indexItem, serviceOrderDetail)

                    Else
						If surgicalProcedureService.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) Then
							'volvemos a obtener el servicio por defecto de derecho a sala
							GetIpsRightRoomDefaultValueError(result.Message, indexItem)
							AsyncLoader(False)
						Else
							'si existe un error informamos y limpiamos controles
							AsyncLoader(False)
                            errorsGetValue = result.Message
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            INDTxtAuthorizationNumber.EditValue = Nothing
                            INDGleLiquidationType.EditValue = 1
                            TotalSalesPrice = 0
                            editValueChangeLoadControl = True
                            INDGleSurchargeApply.EditValue = 0
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
				Dim ipsSuture = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) And x.DefaultService = True)
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
                Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
                Dim result = model.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalDefault)
                If result.StateResult = False Then
                    'si existe un error informamos y limpiamos controles
                    AsyncLoader(False)
                    errorsGetValue = result.Message
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDTxtAuthorizationNumber.EditValue = Nothing
                    INDGleLiquidationType.EditValue = 1
                    TotalSalesPrice = 0
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
        'se pone esta bandera en true para que no se haga nada en el editValueChanged del TxtValue
        _changeValue = True
        If INDSeIndividualDiscount.EditValue > 0 Then
            Dim round = 0
            If serviceOrderDetail.RateManualId IsNot Nothing Then
                Using model As New MRateManual(Me.Tag)
                    round = model.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded.RoundService
                End Using
            End If

            Dim percent As Decimal
            'si el tipo de liquidacion es por un porcentaje de otro servicio
            If INDGleLiquidationType.EditValue = 2 Or INDGleLiquidationType.EditValue = 4 Then
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice

                percent = Math.Round(serviceOrderDetail.SubTotalSalesPrice * INDSeIndividualDiscount.EditValue / 100, 2)
            Else
                If serviceOrderDetail.AllowValueChange = False Then
                    'sino permite cambiar el valor siempre tomo el RateManualSalePrice para hacer los calculos
                    serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                Else
                    'si permite cambiar el valor tomo lo que este en el campo de valor para hacer los calculos
                    serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                End If
                percent = Math.Round(serviceOrderDetail.TotalSalesPrice * INDSeIndividualDiscount.EditValue / 100, 2)
            End If
            serviceOrderDetail.TotalSalesPrice = Utils.RoundValue(serviceOrderDetail.TotalSalesPrice - percent, round)
            serviceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue((serviceOrderDetail.SubTotalSalesPrice - percent) * serviceOrderDetail.InvoicedQuantity, round)
            serviceOrderDetail.ThirdPartyDiscountPercentage = INDSeIndividualDiscount.EditValue
            serviceOrderDetail.ThirdPartyDiscount = percent
        Else
            If INDGleLiquidationType.EditValue = 2 Or INDGleLiquidationType.EditValue = 4 Then
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
            Else
                If serviceOrderDetail IsNot Nothing Then
                    serviceOrderDetail.TotalSalesPrice += serviceOrderDetail.ThirdPartyDiscount
                End If
            End If
            If serviceOrderDetail IsNot Nothing Then
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
						If itemSurgical.ClassServiceIps.Equals(ResourceManager.GetString("Surgeon", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso itemSurgical.PerformsHealthProfessionalThirdPartyId Is Nothing Then
							errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartClassService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), itemSurgical.ClassServiceIps))
						End If
						If itemSurgical.ClassServiceIps.Equals(ResourceManager.GetString("Anesthesiologist", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso itemSurgical.PerformsHealthProfessionalThirdPartyId Is Nothing Then
							errors.AppendLine(String.Format(ResourceManager.GetString("CounterpartClassService", MODULE_NAME), (listServiceOrderDetailPopup.IndexOf(item) + 1).ToString(), itemSurgical.ClassServiceIps))
						End If
						If itemSurgical.ClassServiceIps.Equals(ResourceManager.GetString("Assistant", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso itemSurgical.PerformsHealthProfessionalThirdPartyId Is Nothing Then
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
                If item.AuthorizationNumber Is Nothing Then
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
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleDescription.EditValue Is Nothing Then
                    errors.AppendLine(INDlygDescriptions.Text + ResourceManager.GetString("Empty"))
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
						If itemClassService.ClassService.Equals(ResourceManager.GetString("Surgeon", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso itemClassService.PerformsHealthProfessionalCode Is Nothing Then
							errors.AppendLine(String.Format(ResourceManager.GetString("PerformsHealthProfessionalNotSelected", MODULE_NAME), itemClassService.ClassService))
						ElseIf itemClassService.ClassService.Equals(ResourceManager.GetString("Anesthesiologist", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso itemClassService.PerformsHealthProfessionalCode Is Nothing Then
							errors.AppendLine(String.Format(ResourceManager.GetString("PerformsHealthProfessionalNotSelected", MODULE_NAME), itemClassService.ClassService))
						ElseIf itemClassService.ClassService.Equals(ResourceManager.GetString("Assistant", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso itemClassService.PerformsHealthProfessionalCode Is Nothing Then
							errors.AppendLine(String.Format(ResourceManager.GetString("PerformsHealthProfessionalNotSelected", MODULE_NAME), itemClassService.ClassService))
                        End If
                    Next
                Next
            End If
            If INDTxtAuthorizationNumber.EditValue Is Nothing Then
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
        INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        INDBtnAddDetail.Text = ResourceManager.GetString("Edit")
        editValueChangeLoadControl = FlagModPreCita

        If FlagModPreCita Then
            If listServiceOrderDetailPopup Is Nothing Then
                listServiceOrderDetailPopup = New List(Of ServiceOrderDetail)
            End If
            listServiceOrderDetailPopup.Add(serviceOrderDetail)
        End If

        With serviceOrderDetail
            'asigno valores a los campos y los pongo readOnly
            INDSleCareGroup.EditValue = .CareGroupId
            '' se visualiza o no los campos de iva
            If .TaxedService Then
                INDLySubtotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLyIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLySubtotal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyIva.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
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
            INDGleServiceType.EditValue = 3
            _flagHomologation = False
            INDGleServiceType.Properties.ReadOnly = True
            INDSleServiceCups.EditValue = .CUPSEntityId
            INDSleServiceCups.Properties.NullText = .CodeNameCups
            INDSleServiceCups.Properties.ReadOnly = Not FlagModPreCita
            GetCupsEntity(.CUPSEntityId)

            'Si aplica a RIAS el servicio, se habilita el grupo
            If .ApplyRIAS IsNot Nothing Then
                CupsCodeSearchRIAS = .CodeNameCups.Split(" - ")(0).Trim()
                INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleApplyRIAS.EditValue = .ApplyRIAS

                'Si aplica a RIAS se llena el datasource de las rias
                If .ApplyRIAS Then
                    Using srvOrderModel As New MServiceOrder(Me.Tag)
                        INDsleRIASCups.Properties.DataSource = srvOrderModel.ListRIASCups(CupsCodeSearchRIAS)
                        DontValueChangeSearch = False
                        INDsleRIASCups.EditValue = .RIASCupsId
                        DontValueChangeSearch = True
                    End Using
                End If
            End If
            If .CUPSEntityContractDescriptionId IsNot Nothing Then
                INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDsleDescription.EditValue = .CUPSEntityContractDescriptionId
                INDsleDescription.Properties.NullText = .ContractDescriptionCodeName
            End If
            INDSeCount.EditValue = .InvoicedQuantity
            INDSeCount.Properties.ReadOnly = IIf(FlagModPreCita, False, True)
            INDDteDate.EditValue = .ServiceDate
            INDDteDate.Properties.ReadOnly = True
            INDSlePerformsFuntionalUnit.Properties.NullText = .CodeNameFunctionalUnit
            INDSlePerformsFuntionalUnit.EditValue = .PerformsFunctionalUnitId
            'dependiendo del tipo de liquidacion que se hizo se habilitan los conmtroles
            '1 tipo de unidad - 2 Unidad funcional
            If (.LiquidationType = 1 Or .LiquidationType = 3) Then
                INDSlePerformsFuntionalUnit.Properties.ReadOnly = True
            Else
                INDSlePerformsFuntionalUnit.Properties.ReadOnly = FlagModPreCita
            End If
            INDLciCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDSLeCostCenter.Properties.NullText = .CodeNameCostCenter
            '2 especialidad - si el tipo de liquidacion es 2 se pone readOnly el controls
            If .LiquidationType = 2 Then
                INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = True
                INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = True
            Else
                INDSlePerformsHealthProfessionalCode.Properties.ReadOnly = FlagModPreCita
                INDGleSpecialtyPerformsHealthProfessional.Properties.ReadOnly = FlagModPreCita
            End If
            INDSlePerformsHealthProfessionalCode.EditValue = .PerformsHealthProfessionalCode
            INDGleSpecialtyPerformsHealthProfessional.EditValue = .PerformsProfessionalSpecialty
            If .Presentation = 2 Then

                'si el registro a editar es quirurgico
                Using model As New MServiceOrder(Me.Tag)
                    PerformsHealthProfessionalRepositoryXPO = model.ListHealthCareProfessional()
                    INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    listSurgicalProcedureService = model.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)
                    'si es no cruento consulto esl ips para poder obetener los materiales de sutura
                    If .SurgicalInterventionType < 9 Then
                        Using modelIpsService As New MIPSService(Me.Tag)
							Dim defaultRightRoom = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase))
							Dim ipsRightRoom = modelIpsService.GetIPSServiceByIdSimple(defaultRightRoom.IPSServiceId).ObjectEmbbeded
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
                        End Using
                    Else
                        'si es cruento consulto el manual de tarifas para obtener el ips de materiales
                        Dim rateManual As RateManual
                        Using modelRateManual As New MRateManual(Me.Tag)
                            rateManual = modelRateManual.GetRateManualByIdSimple(serviceOrderDetail.RateManualId).ObjectEmbbeded
                        End Using
                        Using modelIPS As New MIPSService(Me.Tag)
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
                            End With
                            listSurgicalProcedureService.Add(procedureServiceTpm)
                        End Using
                    End If


                    'consulto los detalles quirurgicos del servicio
                    If serviceOrderDetail.ServiceOrderDetailSurgical.Count = 0 Then
                        Dim listServiceDetailSurgical = model.ListSurgicalDetailByIdServiceOrderDetail(serviceOrderDetail.Id)
                        For Each item In listServiceDetailSurgical
                            serviceOrderDetail.ServiceOrderDetailSurgical.Add(item)
                        Next
                    End If
                    'asigno los valores al listado de los items por default y hago datasource

                    For Each itemDetailSurgical In serviceOrderDetail.ServiceOrderDetailSurgical
                        Dim surgicalDefault = listSurgicalProcedureService.Find(Function(x) x.IPSServiceId = itemDetailSurgical.IPSServiceId)
                        If surgicalDefault IsNot Nothing Then
                            surgicalDefault.ValueItemServiceOrderDetail = itemDetailSurgical.TotalSalesPrice
                            surgicalDefault.PerformsHealthProfessionalCode = itemDetailSurgical.PerformsHealthProfessionalCode
                            surgicalDefault.DefaultService = True
                        End If
                    Next

                    INDGcSurgery.DataSource = listSurgicalProcedureService
                    INDGvSurgery.ExpandAllGroups()
                End Using
                'el control del valor opor defecto se bloquea porque no se puede cambiar el item por defecto
                INDRptCheDefaultService.ReadOnly = True
                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDSeSurgeryNumber.EditValue = .SurgeryNumber
                INDSeSurgeryNumber.Properties.ReadOnly = True
                'si el servicio se iquida como SOAT cambio el datasource de las intervenciones quirurgicas para que aparezca la opcion de no cruento
                If .SurgicalInterventionType = 9 Then
                    INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionTypeNoBloody
                Else
                    INDGleSurgicalInterventionType.Properties.DataSource = ListSurgicalInterventionType
                End If
                INDGleSurgicalInterventionType.EditValue = .SurgicalInterventionType
                INDGleSurgicalInterventionType.Properties.ReadOnly = True
                'consulto los detalle de la orden de servicio por el numero del evento del item a editar para mostrar cuantos eventos con el mismo numero existen
                If _listServiceOrderDetailSurgicalIntervention IsNot Nothing Then
                    Dim listEvent = _listServiceOrderDetailSurgicalIntervention.FindAll(Function(x) x.SurgeryNumber = .SurgeryNumber And x.Presentation = 2)
                    INDGcEvents.DataSource = listEvent
                    INDPceEvents.Text = listEvent.Count.ToString()
                End If
            Else
                INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            INDTxtAuthorizationNumber.EditValue = .AuthorizationNumber
            INDGleLiquidationType.EditValue = Nothing
            INDGleLiquidationType.EditValue = .SettlementType
            INDGleLiquidationType.Properties.ReadOnly = True
            INDSleIncludeService.EditValue = .IncludeServiceOrderDetailId
            INDSleIncludeService.Properties.ReadOnly = True
            INDSePercent.EditValue = .RecordType
            INDSePercent.Properties.ReadOnly = True
            INDGleSurchargeApply.EditValue = .SurchargeApply
            INDGleSurchargeApply.Properties.ReadOnly = True
            Me.TotalSalesPrice = .TotalSalesPrice + .ThirdPartyDiscount
            TaxValue = .TaxValue
            GrossValue = .GrossValue
            INDTxtValue.Properties.ReadOnly = True
            INDSeIndividualDiscount.EditValue = .ThirdPartyDiscountPercentage
            INDSeIndividualDiscount.Properties.ReadOnly = True
            _serviceValue = .GrandTotalSalesPrice
            _taxValue = .TaxValue * INDSeCount.EditValue
            ctrTmp.PrintInfo()
        End With
        editValueChangeLoadControl = False
        LayoutControl1.EndUpdate()
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

    ''' <summary>
    ''' muestra u oculta el campo de descripcion relacionada del cups
    ''' </summary>
    Private Sub HideShowFieldsDescription()
        Dim cupsEntityXpo As Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo = Nothing

        INDsleDescription.EditValue = Nothing
        INDsleDescription.Properties.NullText = String.Empty
        INDsleDescription.Properties.DataSource = Nothing

        If INDSleServiceCups.EditValue IsNot Nothing Then
            Using srvOrderModel As New MServiceOrder(Me.Tag)
                cupsEntityXpo = srvOrderModel.GetCupsEntityById(INDSleServiceCups.EditValue)
            End Using
        End If

        INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If cupsEntityXpo IsNot Nothing AndAlso cupsEntityXpo.CUPSEntityContractDescriptionsXpo IsNot Nothing AndAlso cupsEntityXpo.CUPSEntityContractDescriptionsXpo.Count > 0 Then
            If (From x In cupsEntityXpo.CUPSEntityContractDescriptionsXpo Where x.IsDelete = False).Count > 0 Then
                INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDBtnAddDetail.Enabled = True
            End If
        End If
    End Sub

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
    End Sub


    Public Property FormOwnerName As String

    Private Sub FrmPopupServices_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'INDSleIncludeService.Properties.Buttons(1).Visible = False
        'INDSlePerformsHealthProfessionalCode.Properties.Buttons(1).Visible = False
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
            PerformsHealthProfessionalXPO = model.ListHealthCareProfessional()
            PerformsHealthProfessionalRepositoryXPO = model.ListHealthCareProfessional()
        End Using

        If Not String.IsNullOrEmpty(FormOwnerName) AndAlso FormOwnerName.Equals(GetType(CtrFolio).Name) Then
            errorsGetValue = String.Empty
            If listServiceOrderDetailPopup.Count > 1 Then
                'si el listado de los servicios es mayor a uno le asigno el listado al control de navegacion de la barra botones para poder cambiar de registro
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "CodeNameIpsService"}, New ColumnInfo With {.Caption = "CUPS", .FieldName = "CodeNameCups"}}.ToList()
                Me.BarraBotones.FilterDataSource = listServiceOrderDetailPopup
            Else
                'cargo los controles con el unico registro que se retorno
                LoadControlsForLiquidation(listServiceOrderDetailPopup(0))
            End If
            If errorsGetValue.Length = 0 Then
                AsyncLoader(False)
                'activo los controles del grupo del grupo de comportamiento
                ActivateControlsBehavior = True
                'si el grupo de cirugia esta visible asigno el foco al campo del evento sino al numero de autorizacion
                If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDSeSurgeryNumber.Focus()
                Else
                    INDTxtAuthorizationNumber.Focus()
                End If
            End If
            Exit Sub
        End If
        AllowAndChangeControls()
        If _editMode = True Then
            LoadControlsForEdit()
        Else
            CleanControls()
        End If
    End Sub
#End Region

#Region "Shown"

    Private Sub FrmPopupServices_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleCareGroup.Focus()
        If _careGroupAdmission IsNot Nothing Then
            Dim careGroupAdmissionTmp = _careGroupAdmission.Split(",")
            If CInt(careGroupAdmissionTmp.ElementAt(0)) > 0 Then
                INDSleCareGroup.EditValue = CInt(careGroupAdmissionTmp.ElementAt(0))
                INDSleCareGroup.Properties.NullText = careGroupAdmissionTmp.ElementAt(1)
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
    ''' consulta las descripciones relacionadas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDescription.QueryPopUp
        If INDsleDescription.Properties.DataSource Is Nothing AndAlso INDSleServiceCups.EditValue IsNot Nothing Then
            Using srvOrderModel As New MServiceOrder(Me.Tag)
                INDsleDescription.Properties.DataSource = srvOrderModel.ListContractDescriptionsByCupsEntityId(INDSleServiceCups.EditValue)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASCups_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIASCups.QueryPopUp
        If INDsleRIASCups.Properties.DataSource Is Nothing AndAlso CupsCodeSearchRIAS <> "" Then
            Using srvOrderModel As New MServiceOrder(Me.Tag)
                INDsleRIASCups.Properties.DataSource = srvOrderModel.ListRIASCups(CupsCodeSearchRIAS)
            End Using
        End If
    End Sub

    Private Sub INDRptSleHealthProfessional_Popup(sender As Object, e As EventArgs) Handles INDRptSleHealthProfessional.Popup
        Dim search = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim view = search.Properties.View
        Dim obj As SurgicalProcedureService = CType(INDGvSurgery.GetFocusedRow(), SurgicalProcedureService)
        Select Case obj.ClassService
            Case ResourceManager.GetString("Surgeon", "Contract")
                view.ActiveFilterString = "MEDPERCIR = 1 Or MEDPERCIR = 4"
            Case ResourceManager.GetString("Anesthesiologist", "Contract")
                view.ActiveFilterString = "MEDPERCIR = 2 Or MEDPERCIR = 4"
            Case ResourceManager.GetString("Assistant", "Contract")
                view.ActiveFilterString = "MEDPERCIR = 3"
        End Select
        view.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    Private Sub INDSLeCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSLeCostCenter.QueryPopUp
        If INDSLeCostCenter.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CostCenterXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                CostCenterXPO = model.GetCostCenter()
            End Using
        End If
    End Sub

    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CareGroupXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                CareGroupXPO = model.ListCareGroup()
            End Using
        End If
    End Sub

    Private Sub INDSleServiceSoatIss_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleServiceSoatIss.QueryPopUp
        If INDSleServiceSoatIss.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If SoatIssXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                If INDGleServiceType.EditValue = 2 Then
                    SoatIssXPO = model.ListIssServicesByCareGroupNotQx(INDSleCareGroup.EditValue)
                Else
                    SoatIssXPO = model.ListSoatByCareGroupNoQx(INDSleCareGroup.EditValue)
                End If
            End Using
        End If
    End Sub

    Private Sub INDSleServiceCups_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleServiceCups.QueryPopUp
        If INDSleServiceCups.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CupsXPO Is Nothing Then
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
        If INDSlePerformsFuntionalUnit.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If PerformsFuntionalUnitXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                PerformsFuntionalUnitXPO = model.ListFunctionalUnit()
            End Using
        End If
    End Sub

    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ThirdPartyXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                ThirdPartyXPO = model.ListThirdParty()
            End Using
        End If
    End Sub

    Private Sub INDsleHealthAdministrator_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleHealthAdministrator.QueryPopUp
        If INDsleHealthAdministrator.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If HealthAdministratorXPO Is Nothing Then
            Using model As New MServiceOrder(Me.Tag)
                HealthAdministratorXPO = model.ListHealthAdministratorByStatus()
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de aplica a RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleApplyRIAS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyRIAS.EditValueChanged
        If DontValueChangeSearch Then
            If serviceOrderDetail IsNot Nothing AndAlso INDsleApplyRIAS.EditValue <> Nothing Then
                serviceOrderDetail.ApplyRIAS = INDsleApplyRIAS.EditValue
                If INDsleApplyRIAS.EditValue Then
                    INDlyItemRIASCups.HideControl(False)
                Else
                    serviceOrderDetail.RIASCupsId = Nothing
                    INDsleRIASCups.EditValue = Nothing
                    INDsleRIASCups.Properties.NullText = String.Empty
                    INDlyItemRIASCups.HideControl()
                End If
            Else
                If serviceOrderDetail IsNot Nothing Then
                    serviceOrderDetail.ApplyRIAS = INDsleApplyRIAS.EditValue
                    serviceOrderDetail.RIASCupsId = Nothing
                End If
                INDsleRIASCups.EditValue = Nothing
                INDsleRIASCups.Properties.NullText = String.Empty
                INDlyItemRIASCups.HideControl()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIASCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRIASCups.EditValueChanged
        If serviceOrderDetail IsNot Nothing AndAlso INDsleRIASCups.EditValue IsNot Nothing AndAlso DontValueChangeSearch Then
            serviceOrderDetail.RIASCupsId = INDsleRIASCups.EditValue
            serviceOrderDetail.RiasCupsDescription = INDsleRIASCups.Text
            IsSearchRias = True
            AsyncLoader(True)
            GetServiceValueByRIAS()
        End If
    End Sub

    Private Sub INDSleCareGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCareGroup.EditValueChanged
        SoatIssXPO = Nothing
        CupsXPO = Nothing
        ThirdPartyXPO = Nothing
        HealthAdministratorXPO = Nothing
        If INDSleCareGroup.EditValue IsNot Nothing Then
            ' si se esta editando me salgo del evento
            If _editMode = True Then
                Exit Sub
            End If

            'obtengo el grupo de atencion seleccionado 
            Dim careGroupTmp As Object
            If CareGroupXPO Is Nothing Then
                Using model As New Contract.MVP.MCareGroup(Me.Tag)
                    careGroupTmp = model.GetCareGroupByIdSimple(INDSleCareGroup.EditValue).ObjectEmbbeded
                End Using
            Else
                careGroupTmp = DirectCast(DirectCast(INDGvSleCareGroup.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, ContractCareGroupXpo)
            End If

            Dim serviceOrderCareGruopType = _listServiceOrderDetailSurgicalIntervention.Find(Function(x) x.CareGroupId = INDSleCareGroup.EditValue)

            Select Case careGroupTmp.CareGroupType
                Case 1 ' EAPB Con contrato
                    INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 2, 4 'EAPB Sin Contrato y aseguradoras
                    INDLciThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciHealthAdministrator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If serviceOrderCareGruopType IsNot Nothing Then
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
        TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
        GetIndividualDiscount()
        PrintServiceValue()
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleServiceCups_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleServiceCups.EditValueChanged
        If _editMode = True AndAlso Not FlagModPreCita Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        If INDSleServiceCups.EditValue IsNot Nothing Then
            GetCupsEntity(CType(INDSleServiceCups.EditValue, Integer))
        End If
        HideShowFieldsDescription()
        GetHomologationCups()
    End Sub

    Private Sub INDGleSpecialtyPerformsHealthProfessional_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSpecialtyPerformsHealthProfessional.EditValueChanged
        If _editMode = False Then
            INDBtnAddDetail.Enabled = False
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

    Private Async Sub INDSleServiceSoatIss_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleServiceSoatIss.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        GetHomologationIssSoat()
    End Sub

    Private Sub INDDteDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDate.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If

        If INDGleServiceType.EditValue = 3 Then
            GetHomologationCups()
        Else
            GetHomologationIssSoat()
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
                    If _editMode = False Then
                        If serviceOrderDetail.AllowValueChange = True Then
                            INDTxtValue.Properties.ReadOnly = False
                        Else
                            INDTxtValue.Properties.ReadOnly = True
                        End If
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = True

                        Dim dictionarySetValues = Utils.SetValueSalesPrice(Me._flagTaxInclude, serviceOrderDetail.RateManualSalePrice, serviceOrderDetail.TaxPercent)
                        serviceOrderDetail.GrossValue = dictionarySetValues.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                        serviceOrderDetail.TaxValue = dictionarySetValues.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                        serviceOrderDetail.SubTotalSalesPrice = dictionarySetValues.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value

                        If serviceOrderDetail.AllowValueChange = True Then
                            If serviceOrderDetail.Presentation = 2 Then
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                            Else
                                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                            End If
                        Else
                            serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                        End If
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
                Case 2 ' % de otro servicio cargado
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If _editMode = False Then
                        INDSleIncludeService.EditValue = Nothing
                        INDSePercent.Enabled = False
                        INDTxtValue.Properties.ReadOnly = True
                        INDSePercent.EditValue = 0
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = False
                        serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
                Case 3 '100% incluido dentro de otro servicio (No se cobra nada)
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    If _editMode = False Then
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
                        TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
                Case 4 'porcentage del mismo servicio
                    INDLciIncludeService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDGleSurcharge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciIndividualDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If _editMode = False Then
                        INDSleIncludeService.EditValue = Nothing
                        INDSePercent.Enabled = True
                        INDTxtValue.Properties.ReadOnly = True
                        INDSePercent.EditValue = 100
                        INDSeIndividualDiscount.EditValue = 0
                        INDSeIndividualDiscount.Enabled = True
                        'serviceOrderDetail.TotalSalesPrice = 0
                        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                        serviceOrderDetail.RecoveryRatio = 0
                        serviceOrderDetail.ThirdPartyDiscountPercentage = 0
                        serviceOrderDetail.ThirdPartyDiscount = 0
                        _changeValue = True
                        TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                        PrintServiceValue()
                        _changeValue = False
                    End If
            End Select
        End If
    End Sub

    Private Sub INDSleIncludeService_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleIncludeService.EditValueChanged
        If INDSleIncludeService.EditValue IsNot Nothing Then
            If _editMode = False Then
                INDSePercent.Enabled = True
                TotalSalesPrice = 0
                INDSeIndividualDiscount.Enabled = True
                If INDGleLiquidationType.EditValue = 3 Then
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
                TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                PrintServiceValue()
                _changeValue = False
            End If
        End If
    End Sub

    Private Sub INDSeIndividualDiscount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeIndividualDiscount.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
        If editValueChangeLoadControl = True Then
            Exit Sub
        End If
        GetIndividualDiscount()
        PrintServiceValue()
    End Sub

    Private Sub INDGleSurchargeApply_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleSurchargeApply.EditValueChanged
        If _editMode = True Then
            Exit Sub
        End If
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
            TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
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
						Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))
						'si existe un item para materiales lo elimino
						If surgicalProcedureServiceSutureMaterialsRemove IsNot Nothing Then
                            serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceSutureMaterialsRemove.IPSServiceId).FirstOrDefault())
                            listSurgicalProcedureService.Remove(surgicalProcedureServiceSutureMaterialsRemove)
                        End If
                        'obtenemos el ips de material de sutura
                        GetIPSSutureMaterialsNoBloody(False)
                    Else
						'cuando sea normal
						'obtengo el item de materiales de sutura si existe
						Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))
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
								Dim ipsSuture = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) And x.DefaultService = True)
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
                                        End With
                                        listSurgicalProcedureService.Add(procedureServiceTpm)
                                    End If
									'vuelvo a poner el valor que se calculo porque pudo cambiar cuando es no cruento
									Dim detailRigthRoom = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase)).FirstOrDefault()
									detailRigthRoom.TotalSalesPrice = detailRigthRoom.RateManualSalePrice
                                End If
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
                    TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
                    GetIndividualDiscount()
                    PrintServiceValue()
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
                serviceOrderDetail.SubTotalSalesPrice = Utils.RoundValue(CDec((serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue) * INDSePercent.EditValue / 100), 1)
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
                TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
            Else
                Dim serviceDetailTmp = DirectCast(INDGvSleIncludedService.GetFocusedRow, ServiceOrderDetail)
                serviceOrderDetail.SubTotalSalesPrice = Utils.RoundValue(CDec(serviceDetailTmp.TotalSalesPrice * INDSePercent.EditValue / 100), 1)
                serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                serviceOrderDetail.RecoveryRatio = INDSePercent.EditValue
                TotalSalesPrice = serviceOrderDetail.TotalSalesPrice

                If serviceDetailTmp.Id > 0 Then
                    serviceOrderDetail.IncludeServiceOrderDetailId = serviceDetailTmp.Id
                Else
                    serviceOrderDetail.ServiceOrderDetail2 = serviceDetailTmp
                End If
            End If

        Else
            If serviceOrderDetail IsNot Nothing Then
                serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.GrossValue + serviceOrderDetail.TaxValue
                serviceOrderDetail.RecoveryRatio = 0
                serviceOrderDetail.IncludeServiceOrderDetailId = Nothing
                serviceOrderDetail.ServiceOrderDetail2 = Nothing
                serviceOrderDetail.TotalSalesPrice = 0
                TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
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
                Dim dictionaryTotals = Utils.SetValueSalesPrice(True, Me.TotalSalesPrice, serviceOrderDetail.TaxPercent)

                Dim subTotal = serviceOrderDetail.RateManualSalePrice - dictionaryTotals?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                serviceOrderDetail.TaxValue = dictionaryTotals?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                serviceOrderDetail.GrossValue = dictionaryTotals?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                serviceOrderDetail.SubTotalSalesPrice = IIf(subTotal > 0, subTotal, subTotal * -1)
                serviceOrderDetail.TotalSalesPrice = Me.TotalSalesPrice
                GetIndividualDiscount()
                PrintServiceValue()
            End If
        End If

    End Sub

    Private Sub INDSlePerformsHealthProfessionalCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePerformsHealthProfessionalCode.EditValueChanged
        If INDSlePerformsHealthProfessionalCode.EditValue IsNot Nothing Then
            SetSpecialties(healthProfessional, INDGleSpecialtyPerformsHealthProfessional)
        End If
    End Sub

    Private Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleThirdParty.EditValueChanged
        If serviceOrderDetail IsNot Nothing AndAlso INDSleThirdParty.EditValue IsNot Nothing Then
            serviceOrderDetail.ThirdPartyId = INDSleThirdParty.EditValue
            If INDSleThirdParty.Text IsNot String.Empty Then
                serviceOrderDetail.NitNameThirdParty = INDSleThirdParty.Text
            End If
        End If
    End Sub

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

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDescription_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDescription.EditValueChanged
        If INDsleDescription.EditValue IsNot Nothing AndAlso Not editValueChangeLoadControl Then
            GetHomologationCups()
        End If
    End Sub
#End Region

#Region "RecordNavigationChangeEvent"
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If Not String.IsNullOrEmpty(FormOwnerName) AndAlso FormOwnerName.Equals(GetType(CtrFolio).Name) Then
            LoadControlsForLiquidation(Record)
        Else
            DontValueChangeSearch = False
            INDsleApplyRIAS.EditValue = Nothing
            INDsleRIASCups.EditValue = Nothing
            INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            DontValueChangeSearch = True
            LoadControls(Record)
        End If
    End Sub
#End Region

#Region "Click"
    Private Async Sub INDBtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDBtnAddDetail.Click
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

        'Listado de parámetros para enviar a validar las rias
        Dim ListParameters As New List(Of Tuple(Of String, Integer, String, Integer, DateTime))

        If _editMode = False Then
            For Each item In listServiceOrderDetailPopup
                'Se valida si se puede agregar el detalle de rias
                If item.ApplyRIAS IsNot Nothing AndAlso item.ApplyRIAS Then
                    ListParameters.Add(New Tuple(Of String, Integer, String, Integer, DateTime)(_patient.Split(" - ")(0).Trim(), item.RIASCupsId, item.CodeNameCups.Split(" - ")(0).Trim(), item.InvoicedQuantity, item.ServiceDate))
                End If
            Next
        Else
            If serviceOrderDetail.ApplyRIAS IsNot Nothing AndAlso serviceOrderDetail.ApplyRIAS Then
                ListParameters.Add(New Tuple(Of String, Integer, String, Integer, DateTime)(_patient.Split(" - ")(0).Trim(), serviceOrderDetail.RIASCupsId, serviceOrderDetail.CodeNameCups.Split(" - ")(0).Trim(), serviceOrderDetail.InvoicedQuantity, serviceOrderDetail.ServiceDate))
            End If
        End If

        'Si hay datos en el listado de rias se envia al sp para validar
        If ListParameters.Count > 0 Then

            'Se envia las rias al sp para validar si se pueden agregar
            Dim resultValidateRIAS As ActionResult(Of List(Of SP_RIAS_ValidacionCUPSRIAS_Result)) = Nothing
            Using srvOrderModel As New MServiceOrder(Me.Tag)
                resultValidateRIAS = Await srvOrderModel.SP_RIAS_ValidacionCUPSRIAS(ListParameters)
            End Using
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
                If _editMode = False Then
                    quantity += (From x In listServiceOrderDetailPopup Where x.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso x.RIASCupsId = item.RiasCupsId Select x.InvoicedQuantity).Sum()
                Else
                    If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso serviceOrderDetail.RIASCupsId = item.RiasCupsId Then
                        quantity += serviceOrderDetail.InvoicedQuantity
                    End If
                End If

                'Si la cantidad limite - la cantidad realizada es menor a la cantidad a agregar, se arroja mensaje error
                If (item.Frecuencia - item.cantidadRealizadas) < quantity Then
                    errorsValidateQuantityRias.AppendLine("No se puede agregar el cups " + item.NombreCUPS + " con la RIAS " + item.NombreRuta + " porque la cantidad es superior a la cantidad que se puede realizar(" + (item.Frecuencia - item.cantidadRealizadas).ToString() + ")")
                End If

                'Se actualiza el campo de cantidades realizadas
                If _editMode = False Then
                    listServiceOrderDetailPopup.ForEach(Sub(x)
                                                            If x.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso x.RIASCupsId = item.RiasCupsId Then
                                                                x.RealizedQuantity = item.cantidadRealizadas
                                                            Else
                                                                x.RealizedQuantity = 0
                                                            End If
                                                        End Sub)
                Else
                    If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.CodeNameCups.Split(" - ")(0).Trim() = item.CupsCode AndAlso serviceOrderDetail.RIASCupsId = item.RiasCupsId Then
                        serviceOrderDetail.RealizedQuantity = item.cantidadRealizadas
                    End If
                End If
            Next

            'Se retorna el error si lo hay
            If errorsValidateQuantityRias.ToString().Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsValidateQuantityRias.ToString()
                Exit Sub
            End If

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

            For Each item In listServiceOrderDetailPopup
                item.IdTmp = _idTmp
                _idTmp -= 1
            Next

            Dim args As New AddServiceEventArgs
            args.ListServiceOrderDetail = listServiceOrderDetailPopup
            args.EditMode = False
            cleaningControls = False
            RaiseEvent AddServiceOrderDetail(Me, args)
            'INDSleIncludeService.Properties.DataSource = Nothing
            'INDSleIncludeService.Properties.DataSource = _listServiceOrderDetailDatasourceIncludeService

            'Si se esta agregando se asignan los items al listado de comparación ya que este listado solo es enviado cuando le dan click en abrir form modal, y
            'al agregar uno nuevo el modal nunca se cierra por lo tanto toca actualizar el listado de comparación para validar las rias
            If ListCompare Is Nothing Then
                ListCompare = New List(Of ServiceOrderDetail)
            End If
            ListCompare.AddRange(listServiceOrderDetailPopup.Select(Function(x) x.CloneEntity()).Cast(Of ServiceOrderDetail).ToList())

            CleanControls()
        Else
            Dim args As New AddServiceEventArgs

            args.ServiceOrderDetail = serviceOrderDetail
            args.EditMode = True
            cleaningControls = True
            RaiseEvent AddServiceOrderDetail(Me, args)
            CleanControls()
            Me.Close()
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
		Dim validServices = {
				ResourceManager.GetString("None", "Contract"),
				ResourceManager.GetString("RightRoom", "Contract"),
				ResourceManager.GetString("SutureMaterials", "Contract"),
				ResourceManager.GetString("SurgicalInstrumentation", "Contract")
			}
		If validServices.Any(Function(service) service.Equals(surgicalProcedireService.ClassService, StringComparison.OrdinalIgnoreCase)) Then
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

    Private Sub INDSlePerformsHealthProfessionalCode_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSlePerformsHealthProfessionalCode.EditValueChanging
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
            INDGleSpecialtyPerformsHealthProfessional.Enabled = True
            If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				Dim professionalSelected = listSurgicalProcedureService.Find(Function(x) Not x.ClassService.Equals(ResourceManager.GetString("Surgeon", "Contract"), StringComparison.OrdinalIgnoreCase) AndAlso x.PerformsHealthProfessionalCode = e.NewValue)
				If professionalSelected IsNot Nothing Then
					Mensaje(EeventViewerImages.Advertencia) = "Este profesional ya esta seleccionado como " + professionalSelected.ClassService
					e.Cancel = True
					Exit Sub
				End If

				Dim rowIndex = INDGvSurgery.FocusedRowHandle
				'obtengo el registro de la clase cirujano y le pongo el mismo medico que se selecciona para mostrar en la rejilla
				Dim surgicalProcedure = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("Surgeon", "Contract"), StringComparison.OrdinalIgnoreCase) And x.DefaultService = True)
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

					If SurgicalProcedureService.ClassService.Equals(ResourceManager.GetString("Surgeon", "Contract"), StringComparison.OrdinalIgnoreCase) Then
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
        Me.Cursor = ChangeCursorIndigo()
        Dim rowPosition = INDGvSurgery.FocusedRowHandle
        Dim surgicalProcedireService = DirectCast(INDGvSurgery.GetFocusedRow, SurgicalProcedureService)
        If surgicalProcedireService.ClassService = "Ayudante" Then
            If e.NewValue = False Then
                'si se va a desactivar un item verifico que no sea el unico que esta seleccionado
                'Dim listSurgicalProcedureServiceTmp = listSurgicalProcedureService.FindAll(Function(x) x.ClassService = "Ayudante" And x.DefaultService = True)
                'If listSurgicalProcedureServiceTmp.Count = 1 Then
                '    'e.Cancel = True
                '    'Exit Sub
                'Else
                'disminuyo el valor del item que se deselecciono
                serviceOrderDetail.SubTotalSalesPrice -= surgicalProcedireService.ValueItemServiceOrderDetail
                serviceOrderDetail.RateManualSalePrice -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.TotalSalesPrice -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.GrossValue -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedireService.IPSServiceId).FirstOrDefault())
                surgicalProcedireService.ValueItemServiceOrderDetail = 0
                surgicalProcedireService.DefaultService = False
                surgicalProcedireService.PerformsHealthProfessionalCode = Nothing
                INDGcSurgery.DataSource = Nothing
                INDGcSurgery.DataSource = listSurgicalProcedureService
                INDGvSurgery.ExpandAllGroups()
                INDGvSurgery.FocusedRowHandle = rowPosition
                GetIndividualDiscount()
                PrintServiceValue()
                'End If
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
                PrintServiceValue()
            End If
        Else
			If e.NewValue = False Then
				Dim excludedServices = {
					ResourceManager.GetString("Anesthesiologist", "Contract"),
					ResourceManager.GetString("SutureMaterials", "Contract"),
					ResourceManager.GetString("RightRoom", "Contract")
				}
				If Not excludedServices.Any(Function(service) service.Equals(surgicalProcedireService.ClassService, StringComparison.OrdinalIgnoreCase)) Then
					e.Cancel = True
					Me.Cursor = Cursors.Default
					Exit Sub
				End If
				If surgicalProcedireService.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) Then
					'si se esta quitando el derecho a sala elimino los materiales de sutura
					Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))
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
				serviceOrderDetail.SubTotalSalesPrice -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.RateManualSalePrice -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.TotalSalesPrice -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.GrossValue -= surgicalProcedireService.ValueItemServiceOrderDetail
				serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedireService.IPSServiceId).FirstOrDefault())
				surgicalProcedireService.ValueItemServiceOrderDetail = 0
				surgicalProcedireService.DefaultService = False
				surgicalProcedireService.PerformsHealthProfessionalCode = Nothing
				INDGcSurgery.DataSource = Nothing
				INDGcSurgery.DataSource = listSurgicalProcedureService
				INDGvSurgery.ExpandAllGroups()
				INDGvSurgery.FocusedRowHandle = rowPosition
				GetIndividualDiscount()
				PrintServiceValue()
			ElseIf surgicalProcedireService.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) Then ' el item es derecho a sala
				'obtengo el item que debo remover del listado ya que el valor por defecto cambio
				Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
				'obtengo el item de materiales de sutura si existe
				Dim surgicalProcedureServiceSutureMaterialsRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))
				'remuevo el item de la entidad del detalle quirurgico
				If surgicalProcedureServiceRemove IsNot Nothing Then
					serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault())
				End If
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
				PrintServiceValue()
			Else
				If surgicalProcedireService.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase) Then ' si es materiales de sutura
					'obtengo el ips de derecho a sala para saber la tarifa del ips de materiales que tiene asociado
					Dim ipsRigthRoom = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("RightRoom", "Contract"), StringComparison.OrdinalIgnoreCase) And x.DefaultService = True)
					Dim ipsSutureMaterials = listSurgicalProcedureService.Find(Function(x) x.ClassService.Equals(ResourceManager.GetString("SutureMaterials", "Contract"), StringComparison.OrdinalIgnoreCase))
					listSurgicalProcedureService.Remove(ipsSutureMaterials)
					GetIPSSutureMaterials(ipsRigthRoom)
				Else
					'obtengo el item que debo remover del listado ya que el valor por defecto cambio
					Dim surgicalProcedureServiceRemove = listSurgicalProcedureService.Find(Function(x) x.ClassService = surgicalProcedireService.ClassService And x.DefaultService = True)
                    If surgicalProcedureServiceRemove IsNot Nothing Then
                        'remuevo el item de la entidad del detalle quirurgico
                        serviceOrderDetail.ServiceOrderDetailSurgical.Remove(serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = surgicalProcedureServiceRemove.IPSServiceId).FirstOrDefault())
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

    Private Sub INDSePercent_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSePercent.EditValueChanging
        If INDGleLiquidationType.EditValue = 4 Then
            If e.NewValue = 0 Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "El porcentage a cobrar no puede ser 0"
            End If
        End If
    End Sub

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
    Private Sub INDGleSpecialtyPerformsHealthProfessional_KeyDown(sender As Object, e As KeyEventArgs) Handles INDGleSpecialtyPerformsHealthProfessional.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSeSurgeryNumber.Focus()
            Else
                INDTxtAuthorizationNumber.Focus()
            End If
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

    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                OpenFormDialog(formulario)
            End Using
        End If
    End Sub
#End Region

#Region "BackgroundWorker"
    Private Sub _bgSOATISS_DoWork(sender As Object, e As DoWorkEventArgs)
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
            For Each item In listHomologationTmp
                Dim result = model.GetHomologationCups(INDSleCareGroup.EditValue, item.CupsEntityId, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, INDSleServiceSoatIss.EditValue, manualType)
                If result.StateResult = False Then
                    errorsHomologation.AppendLine(result.Message)
                    Continue For
                End If
                listHomologation.AddRange(result.ObjectEmbbeded)
            Next
            If errorsHomologation.Length > 0 Then
                e.Cancel = True
                Exit Sub
            End If
        End Using
    End Sub

    Private Sub _bgSOATISS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsHomologation.ToString()
            AsyncLoader(False)
            CleanControlsErrorValueFields()
        Else

            If listHomologation.Count > 1 Then
                Dim formulario As New PopupHomologation
                'formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
                'formulario.Text = "hola"
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

    Private Sub _bgCUPS_DoWork(sender As Object, e As DoWorkEventArgs)
        listHomologation = Nothing

        errorsHomologation = New StringBuilder()
        Using model As New MServiceOrder(Me.Tag)
            Dim TempDescriptionId As Integer? = Nothing
            Dim TempRiasId As Integer? = Nothing
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue IsNot Nothing Then
                Dim info = model.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                TempDescriptionId = info.ContractDescriptionId.Id
            End If
            If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always _
                AndAlso INDsleRIASCups.EditValue IsNot Nothing Then
                Dim info = model.GetRiasCupsByRiasCupsId(INDsleRIASCups.EditValue)
                TempRiasId = info.RiasId
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
    End Sub

    Private Sub _bgCUPS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsHomologation.ToString()
            _bgCUPS.CancelAsync()
            AsyncLoader(False)
            CleanControlsErrorValueFields()
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

    Private Sub _bgGetServiceValueByRIAS_DoWork(sender As Object, e As DoWorkEventArgs)

        errorsGetValue = String.Empty

        'Variable para establecer el valor que se envía al servicio que calcula el valor del cups
        Dim TempRiasId As Integer? = Nothing
        If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Se consulta el id de la rias ya que el que retorna el control es el id de riasCups
            Dim info As ViewRIASCupsXpo
            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                info = modelServiceOrder.GetRiasCupsByRiasCupsId(INDsleRIASCups.EditValue)
            End Using
            TempRiasId = info.RiasId
        End If

        Using model As New MServiceOrder(Me.Tag)
            Dim result = model.GetServiceValueByRIAS(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), serviceOrderDetail.CUPSEntityId, serviceOrderDetail.IPSServiceId, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id, TempRiasId)
            If result.StateResult = False Then
                e.Cancel = True
                errorsGetValue = result.Message
                Exit Sub
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

    Private Sub _bgGetServiceValue_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsGetValue
            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtAuthorizationNumber.EditValue = Nothing
            INDGleLiquidationType.EditValue = 1
            INDGleSurchargeApply.EditValue = False
            TotalSalesPrice = 0
            INDSeIndividualDiscount.EditValue = 0
            AsyncLoader(False)
            ActivateControlsBehavior = False
        Else
            If listServiceOrderDetailPopup.Count > 1 Then
                'si el listado de los servicios es mayor a uno le asigno el listado al control de navegacion de la barra botones para poder cambiar de registro
                Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Servicio IPS", .FieldName = "CodeNameIpsService"}, New ColumnInfo With {.Caption = "CUPS", .FieldName = "CodeNameCups"}}.ToList()
                Me.BarraBotones.FilterDataSource = listServiceOrderDetailPopup
            Else
                'cargo los controles con el unico registro que se retorno
                LoadControls(listServiceOrderDetailPopup(0))
            End If
            If INDTxtAuthorizationNumber.EditValue IsNot Nothing Then
                'listServiceOrderDetailPopup(0).AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
                serviceOrderDetail.AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
            End If
            If errorsGetValue.Length = 0 Then

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
                'Exit Sub
                'bg.Dispose()
            End If
            'INDGleSurgicalInterventionType.Properties.Buttons(1).Visible = False
            INDBtnAddDetail.Enabled = True
        End If

    End Sub

    Private Sub _bgGetServiceValue_DoWork(sender As Object, e As DoWorkEventArgs)

        errorsGetValue = String.Empty

        'Variable para establecer el valor que se envía al servicio que calcula el valor del cups
        Dim TempRiasId As Integer? = Nothing
        If INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemRIASCups.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Se consulta el id de la rias ya que el que retorna el control es el id de riasCups
            Dim info As ViewRIASCupsXpo
            Using modelServiceOrder As New MServiceOrder(Me.Tag)
                info = modelServiceOrder.GetRiasCupsByRiasCupsId(INDsleRIASCups.EditValue)
            End Using
            TempRiasId = info.RiasId
        End If

        Using model As New MServiceOrder(Me.Tag)
            Dim TempDescriptionId As Integer? = Nothing
            If INDlygDescriptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleDescription.EditValue IsNot Nothing Then
                Dim info = model.GetCUPSEntityContractDescriptionById(INDsleDescription.EditValue)
                TempDescriptionId = info.ContractDescriptionId.Id
            End If
            Dim result = model.GetServiceValue(Admission, If(String.IsNullOrEmpty(CenterAttentionCode), FunctionalUnitCenterAttentionCode, CenterAttentionCode), listHomologation, INDSleCareGroup.EditValue, INDSlePerformsFuntionalUnit.EditValue, INDGleSpecialtyPerformsHealthProfessional.EditValue, INDDteDate.EditValue, _patienGenus, _patientDate, INDSeCount.EditValue, INDSlePerformsHealthProfessionalCode.EditValue, thirdParty.Id, TempRiasId, TempDescriptionId)
            If result.StateResult = False Then
                e.Cancel = True
                errorsGetValue = result.Message
                Exit Sub
            End If
            ' se verifica si el detalle de la orden del servicio tiene un id de cita o de hemocomponente
            If Not String.IsNullOrEmpty(serviceOrderDetail?.IdCita) OrElse serviceOrderDetail?.HemocomponentId <> 0 Then
                result.ObjectEmbbeded.ForEach(Sub(x)
                                                  x.IdCita = serviceOrderDetail?.IdCita
                                                  x.HemocomponentId = serviceOrderDetail?.HemocomponentId
                                              End Sub)

            End If
            listServiceOrderDetailPopup = result.ObjectEmbbeded
        End Using

    End Sub

    Private Sub _bgGetServiceValueByRIAS_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsGetValue
            INDLcgSurgery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDTxtAuthorizationNumber.EditValue = Nothing
            INDGleLiquidationType.EditValue = 1
            INDGleSurchargeApply.EditValue = False
            TotalSalesPrice = 0
            INDSeIndividualDiscount.EditValue = 0
            AsyncLoader(False)
            ActivateControlsBehavior = False
        Else
            LoadControls(EntityTemp)
            If INDTxtAuthorizationNumber.EditValue IsNot Nothing Then
                'listServiceOrderDetailPopup(0).AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
                serviceOrderDetail.AuthorizationNumber = INDTxtAuthorizationNumber.EditValue
            End If
            If errorsGetValue.Length = 0 Then

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
                'Exit Sub
                'bg.Dispose()
            End If
            'INDGleSurgicalInterventionType.Properties.Buttons(1).Visible = False
            INDBtnAddDetail.Enabled = True
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
