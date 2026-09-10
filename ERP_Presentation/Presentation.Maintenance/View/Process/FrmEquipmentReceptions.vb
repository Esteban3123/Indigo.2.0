'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Julian Andres Cardozo Flores
' Created          : 03-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Maintenance.MVP

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista en el frontal de torres
''' </summary>
Public Class FrmEquipmentReceptions
    Implements IEquipmentReception

#Region "Propiedades Intefaz"
    Public Property FixedAssetPhysicalAssetId As Integer
    Public Event Saved()
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    Dim _datasourceunit As List(Of Tuple(Of String, String))
    Dim _datasourceInventoryType As List(Of Tuple(Of Integer, String))
    Dim ListEquipmentType As List(Of Object)

    ''' <summary>
    ''' Lista de registros invima
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListInvimaRegistration As List(Of EquipmentInvima)

    ''' <summary>
    ''' Lista de eliminados de registros invima
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteInvimaRegistration As List(Of EquipmentInvima)


    Public Property InvimaRegistration As EquipmentInvima


    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    ''' <remarks></remarks>
    Public Property FlagInvimaRegistration As Boolean


    ''' Obtiene el datasource de la unidad (meses,años)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DataSourceInventoryType As List(Of Tuple(Of Integer, String))
        Get
            If _datasourceInventoryType Is Nothing Then
                _datasourceInventoryType = New List(Of Tuple(Of Integer, String))
                _datasourceInventoryType.Add(New Tuple(Of Integer, String)("1", "Biomédico"))
                _datasourceInventoryType.Add(New Tuple(Of Integer, String)("2", "Infraestructura"))
                _datasourceInventoryType.Add(New Tuple(Of Integer, String)("3", "Industriales"))
                _datasourceInventoryType.Add(New Tuple(Of Integer, String)("4", "Muebles y Enseres"))
                _datasourceInventoryType.Add(New Tuple(Of Integer, String)("5", "Equipos de Cómputo y Comunicaciones"))
                _datasourceInventoryType.Add(New Tuple(Of Integer, String)("6", "Otros"))
            End If
            Return _datasourceInventoryType
        End Get
    End Property

    Dim _datasourceacquisition As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceAcquisition As List(Of Tuple(Of String, String))
        Get
            If _datasourceacquisition Is Nothing Then
                _datasourceacquisition = New List(Of Tuple(Of String, String))
                _datasourceacquisition.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("CompraDirecta", "Maintenance")))
                _datasourceacquisition.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("DonadoPorParticulares", "Maintenance")))
                _datasourceacquisition.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Leasing", "Maintenance")))
                _datasourceacquisition.Add(New Tuple(Of String, String)("4", ResourceManager.GetString("Comodato", "Maintenance")))
                _datasourceacquisition.Add(New Tuple(Of String, String)("5", ResourceManager.GetString("Outsourcing", "Maintenance")))
                _datasourceacquisition.Add(New Tuple(Of String, String)("6", ResourceManager.GetString("Otro", "Maintenance")))
            End If
            Return _datasourceacquisition
        End Get
    End Property

    Dim _datasourcefuentealimentacion As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceFuenteAlimentacion As List(Of Tuple(Of String, String))
        Get
            If _datasourcefuentealimentacion Is Nothing Then
                _datasourcefuentealimentacion = New List(Of Tuple(Of String, String))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Agua", "Maintenance")))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Aire", "Maintenance")))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Gas", "Maintenance")))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("4", ResourceManager.GetString("Vapor", "Maintenance")))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("5", ResourceManager.GetString("DerivadosDePetroleo", "Maintenance")))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("6", "Electricidad"))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("7", "Energia solar"))
                _datasourcefuentealimentacion.Add(New Tuple(Of String, String)("8", ResourceManager.GetString("Otro", "Maintenance")))
            End If
            Return _datasourcefuentealimentacion
        End Get
    End Property

    Dim _datasourceuse As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceUse As List(Of Tuple(Of String, String))
        Get
            If _datasourceuse Is Nothing Then
                _datasourceuse = New List(Of Tuple(Of String, String))
                _datasourceuse.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Medico", "Maintenance")))
                _datasourceuse.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Basico", "Maintenance")))
                _datasourceuse.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Apoyo", "Maintenance")))
            End If
            Return _datasourceuse
        End Get
    End Property

    Dim _datasourcerisk As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceRisk As List(Of Tuple(Of String, String))
        Get
            If _datasourcerisk Is Nothing Then
                _datasourcerisk = New List(Of Tuple(Of String, String))
                _datasourcerisk.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Alto", "Maintenance")))
                _datasourcerisk.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Medios", "Maintenance")))
                _datasourcerisk.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Bajo", "Maintenance")))
            End If
            Return _datasourcerisk
        End Get
    End Property

    Dim _datasourcetechnology As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceTechnology As List(Of Tuple(Of String, String))
        Get
            If _datasourcetechnology Is Nothing Then
                _datasourcetechnology = New List(Of Tuple(Of String, String))
                _datasourcetechnology.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Electrico", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Electronico", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Mecanico", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("4", ResourceManager.GetString("Electromecanico", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("5", ResourceManager.GetString("Hidraulico", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("6", ResourceManager.GetString("Neumatico", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("7", ResourceManager.GetString("Vapor", "Maintenance")))
                _datasourcetechnology.Add(New Tuple(Of String, String)("8", ResourceManager.GetString("Solar", "Maintenance")))

            End If
            Return _datasourcetechnology
        End Get
    End Property

    Dim _datasourcefrecuso As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceFrecUso As List(Of Tuple(Of String, String))
        Get
            If _datasourcefrecuso Is Nothing Then
                _datasourcefrecuso = New List(Of Tuple(Of String, String))
                _datasourcefrecuso.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("UsoBajo", "Maintenance")))
                _datasourcefrecuso.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("UsoModerado", "Maintenance")))
                _datasourcefrecuso.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("UsoContinuo", "Maintenance")))
            End If
            Return _datasourcefrecuso
        End Get
    End Property

    Dim _datasourcelocation As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceLocation As List(Of Tuple(Of String, String))
        Get
            If _datasourcelocation Is Nothing Then
                _datasourcelocation = New List(Of Tuple(Of String, String))
                _datasourcelocation.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Movil", "Maintenance")))
                _datasourcelocation.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Fijo", "Maintenance")))
            End If
            Return _datasourcelocation
        End Get
    End Property

    Dim _datasourceEquipmentType As List(Of Tuple(Of String, String))
    ReadOnly Property DatasourceEquipmentType As List(Of Tuple(Of String, String))
        Get
            If _datasourceEquipmentType Is Nothing Then
                _datasourceEquipmentType = New List(Of Tuple(Of String, String))
                _datasourceEquipmentType.Add(New Tuple(Of String, String)("1", ResourceManager.GetString("Piezas", "Maintenance")))
                _datasourceEquipmentType.Add(New Tuple(Of String, String)("2", ResourceManager.GetString("Accesorios", "Maintenance")))
                _datasourceEquipmentType.Add(New Tuple(Of String, String)("3", ResourceManager.GetString("Consumibles", "Maintenance")))
            End If
            Return _datasourceEquipmentType
        End Get
    End Property


    Public ReadOnly Property MyTag As Object Implements IEquipmentReception.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del equipo
    ''' </summary>
    Public Property NameEquipment As String Implements IEquipmentReception.NameEquipment
        Get
            Return INDtxtDescripcionEquipo.Text
        End Get
        Set(value As String)
            INDtxtDescripcionEquipo.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el codigo del tipo de inventrario
    ''' </summary>
    Public Property IdTrademark As Integer Implements IEquipmentReception.IdTrademark
        Get
            Return INDSlTrademark.EditValue
        End Get
        Set(value As Integer)
            INDSlTrademark.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el codigo del equipo
    ''' </summary>
    Public Property IdEquipmentType As Integer Implements IEquipmentReception.IdEquipmentType
        Get
            Return INDglEquipmentType.EditValue
        End Get
        Set(value As Integer)
            INDglEquipmentType.EditValue = value
            'INDglEquipmentType.Properties.PopupFormWidth = INDglEquipmentType.Width * 2
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la ubicacion
    ''' </summary>
    Private _IdLocation As String
    Public Property IdLocation As String Implements IEquipmentReception.IdLocation
        Get
            Return _IdLocation
        End Get
        Set(value As String)
            _IdLocation = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el codigo del responsable
    ''' </summary>
    Public Property IdResponsible As Integer Implements IEquipmentReception.IdResponsible
        Get
            Return INDglResponsible.EditValue
        End Get
        Set(value As Integer)
            INDglResponsible.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene la adquisicion  del equipo
    ''' </summary>
    Public Property Acquisition As String Implements IEquipmentReception.Acquisition
        Get
            Return INDgleAcquisition.EditValue
        End Get
        Set(value As String)
            INDgleAcquisition.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene otra adquisicion  del equipo
    ''' </summary>
    Public Property OtherAcquisition As String Implements IEquipmentReception.OtherAcquisition
        Get
            Return INDtxtOther.EditValue
        End Get
        Set(value As String)
            INDtxtOther.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad carga todos las sucursales
    ''' </summary>
    Public WriteOnly Property LocationDataSource As List(Of Object) Implements IEquipmentReception.LocationDataSource
        Set(value As List(Of Object))
            'INDglLocation.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene todos los equipos
    ''' </summary>
    Public WriteOnly Property EquipmenteDataSource As List(Of Object) Implements IEquipmentReception.EquipmenteTypeDataSource
        Set(value As List(Of Object))
            'INDglEquipmentType.Properties.DataSource = value
            ListEquipmentType = value
            'INDglEquipmentType.Properties.PopupFormWidth = INDglEquipmentType.Width * 2
        End Set
    End Property
    ''' <summary>
    ''' Establece la propiedad de tipos de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property TypeInventoryDataSource As List(Of Object) Implements IEquipmentReception.TypeInventoryDataSource
        Set(value As List(Of Object))
            'INDgleTypeInventory.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene todos los responsables
    ''' </summary>
    Public WriteOnly Property SupplierDataSource As List(Of Object) Implements IEquipmentReception.SupplierDataSource
        Set(value As List(Of Object))
            INDglVendedor.Properties.DataSource = value.Where(Function(X) X.Seller = True).ToList
            INDglResponsableGarantia.Properties.DataSource = value.Where(Function(X) X.ResponsibleWarranty = True).ToList
            INDglManufacter.Properties.DataSource = value.Where(Function(X) X.Manufacturer = True).ToList
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga todos las polizas
    ''' </summary>
    Public WriteOnly Property PolizaDataSource As List(Of Object) Implements IEquipmentReception.PolizaDataSource
        Set(value As List(Of Object))
            'INDglPolize.Properties.DataSource = value
            'INDglPolize.Properties.PopupFormWidth = INDglPolize.Width * 2
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga todos los responsables
    ''' </summary>
    Public WriteOnly Property ResponsibleDataSource As List(Of Domain.Entities.Responsible) Implements IEquipmentReception.ResponsibleDataSource
        Set(value As List(Of Domain.Entities.Responsible))
            'INDglResponsible.Properties.DataSource = value
            'INDglResponsible.Properties.PopupFormWidth = INDglResponsible.Width * 2
        End Set
    End Property

    Public Property TechnicalLogDataSource As List(Of Domain.Entities.TechnicalLog) Implements IEquipmentReception.TechnicalLogDataSource
        Get
            Return CType(INDglTechnicalLog.Properties.DataSource, List(Of Domain.Entities.TechnicalLog))
        End Get
        Set(value As List(Of Domain.Entities.TechnicalLog))
            INDglTechnicalLog.Properties.DataSource = value
            INDglTechnicalLog.Properties.PopupFormWidth = INDglTechnicalLog.Width * 2
        End Set


    End Property
    Public Property MeasureUnitDataSource As List(Of Domain.Entities.MeasurementUnit) Implements IEquipmentReception.MeasureUnitDataSource
        Get
            Return CType(INDRepositoryItemglTipoUnidad.DataSource, List(Of Domain.Entities.MeasurementUnit))
        End Get
        Set(value As List(Of Domain.Entities.MeasurementUnit))
            INDRepositoryItemglTipoUnidad.DataSource = value
        End Set
    End Property

    Public WriteOnly Property PartsAccesoriesConsumablesDataSource As List(Of Object) Implements IEquipmentReception.PartsAccesoriesConsumablesDataSource
        Set(value As List(Of Object))

            INDSlePartsAccesoriesConsumables.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As Domain.Entities.MaintenanceSequence Implements IEquipmentReception.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property CodeEquipmentReception As String Implements IEquipmentReception.CodeEquipmentReception
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Dim MaintenanceParameters As MaintenanceParameter

    Public Property IdEquipment As Integer Implements IEquipmentReception.IdEquipment

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Dim Equipment As List(Of Domain.Entities.Equipment)

    Dim ListTrademark As List(Of Object)

    Function EnabledControls(value As Boolean)
        LayoutControlGroup1.BeginUpdate()
        INDBteCode.Enabled = Not value
        INDgleTypeInventory.Enabled = value
        'INDtxtNumberInventory.Enabled = value
        INDSlEquipment.Enabled = value
        INDSlTrademark.Enabled = value
        'INDtxtBarCode.Enabled = value
        INDglEquipmentType.Enabled = value
        INDglLocation.Enabled = value
        INDtxtModelo.Enabled = value
        INDtxtSerie.Enabled = value
        INDtxtDescripcionEquipo.Enabled = value
        INDspLifeTime.Enabled = value
        INDgleUnit.Enabled = value
        INDglResponsible.Enabled = value

        INDgleAcquisition.Enabled = value
        INDtxtOther.Enabled = value
        INDglManufacter.Enabled = value
        INDglVendedor.Enabled = value
        INDglResponsableGarantia.Enabled = value
        INDtxtValorAdquisicion.Enabled = value
        INDdtFechaAdquisicion.Enabled = value
        INDdtFechaFabricacion.Enabled = value
        INDdtFechaInicioOperacion.Enabled = value
        INDdtFechaInstalacion.Enabled = value
        INDdtFechaVencimiento.Enabled = value
        INDglPolize.Enabled = value
        INDtxtNroFactura.Enabled = value

        INDgleFuenteAlimentacion.Enabled = value
        INDgleUso.Enabled = value
        'INDgleRiesgos.Enabled = value
        INDgleTecnologiaPredominante.Enabled = value
        INDglEquipmentFunction.Enabled = value
        INDgleFrequencyComputerUse.Enabled = value
        INDgleLocationType.Enabled = value
        INDglPhysicalRisk.Enabled = value
        INDglEquipmentRequirement.Enabled = value
        INDglEquipmentHistory.Enabled = value

        INDglTechnicalLog.Enabled = value
        INDgcRegistroTecnico.Enabled = value
        INDbtnAgregarRegistroTecnicio.Enabled = value
        'INDglMeasureUnit.Enabled = value

        INDSlePartsAccesoriesConsumables.Enabled = value
        INDbtnAgregarDetalle.Enabled = value
        INDgcDetail.Enabled = value
        INDGcMantenimientos.Enabled = value
        INDGcMantenimientoNoProgramado.Enabled = value
        INDGcWorkOrders.Enabled = value

        INDpopupManuales.Enabled = value
        INDPopupPlanos.Enabled = value
        INDPopupFichaTecnica.Enabled = value

        INDgcInvima.Enabled = value
        INDpceAddInventory.Enabled = value

        CtrFoto.Enabled = value
        BarraBotones.StatusRecordVisible = True
        LayoutControlGroup1.EndUpdate()
        If value Then
            INDgleTypeInventory.Focus()
        Else
            INDBteCode.Focus()
        End If
    End Function

    Public WriteOnly Property ActionsOnControls As Boolean Implements IEquipmentReception.ActionsOnControls
        Set(value As Boolean)

            EnabledControls(value)

        End Set
    End Property
    ''' <summary>
    ''' Esta propiedad contiene el estado del registro de equipo
    ''' </summary>
    Public Property StateEquipmentReceptions As Boolean Implements IEquipmentReception.StateEquipmentReceptions
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            BarraBotones.StatusRecord = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que cargar las funciones del equipo
    ''' </summary>
    Public WriteOnly Property EquipmentFunctionDataSource As List(Of Domain.Entities.EquipmentFunction) Implements IEquipmentReception.EquipmentFunctionDataSource
        Set(value As List(Of Domain.Entities.EquipmentFunction))
            INDglEquipmentFunction.Properties.DataSource = value
            INDglEquipmentFunction.Properties.PopupFormWidth = INDglEquipmentFunction.Width * 2
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga todos los antecedentes del equipo
    ''' </summary>
    Public WriteOnly Property EquipmentHistoryDataSource As List(Of Domain.Entities.EquipmentHistory) Implements IEquipmentReception.EquipmentHistoryDataSource
        Set(value As List(Of Domain.Entities.EquipmentHistory))
            INDglEquipmentHistory.Properties.DataSource = value
            INDglEquipmentHistory.Properties.PopupFormWidth = INDglEquipmentHistory.Width * 2
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga todos los requerimientos del equipo
    ''' </summary>
    Public WriteOnly Property EquipmentRequirementDataSource As List(Of Domain.Entities.EquipmentRequirement) Implements IEquipmentReception.EquipmentRequirementDataSource
        Set(value As List(Of Domain.Entities.EquipmentRequirement))
            INDglEquipmentRequirement.Properties.DataSource = value
            INDglEquipmentRequirement.Properties.PopupFormWidth = INDglEquipmentRequirement.Width * 2
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga todos los riesgos fisicos
    ''' </summary>
    Public WriteOnly Property PhysicalRiskDataSource As List(Of Domain.Entities.PhysicalRisk) Implements IEquipmentReception.PhysicalRiskDataSource
        Set(value As List(Of Domain.Entities.PhysicalRisk))
            INDglPhysicalRisk.Properties.DataSource = value
            INDglPhysicalRisk.Properties.PopupFormWidth = INDglPhysicalRisk.Width * 2
        End Set
    End Property

    Public WriteOnly Property MaintenanceParameters1 As MaintenanceParameter Implements IEquipmentReception.MaintenanceParameters
        Set(value As MaintenanceParameter)
            MaintenanceParameters = value
            'INDtxtDescripcionEquipo.Text = MaintenanceParameters.Description
        End Set
    End Property

#End Region

#Region "Variables Funcional y Load"

    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim EquipmentReception As Domain.Entities.EquipmentRegistration
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MEquipment
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PEquipmentReception
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que contiene la fecha del servidor
    ''' </summary>
    Dim ServerDate As Date
    ''' <summary>
    ''' Variable qpara acceder al modelo de las unidad nde medida
    ''' </summary>
    Dim ModelMeasureUnit As New MUnitMeasure
    'Dim ListTechnicalLog As New List(Of Domain.Entities.TechnicalLogDetail)
    Dim ListParts As New List(Of Object)
    Dim ListAccessory As New List(Of Domain.Entities.EquipmentReceptionAccesory)
    Dim ListConsumable As New List(Of Domain.Entities.EquipmentReceptionConsumable)
    Dim ListPartAccesoryConsumable As New List(Of Domain.Entities.EquipmentReceptionPartsAccesoriesConsumables)
    Dim ListDetail As New List(Of Object)
    'eliminacion de registros agregados
    Dim DeletedListTechnicalLog As New List(Of Domain.Entities.TechnicalLogDetail)
    Dim DeletedListPart As New List(Of Object)
    Dim DeletedListAccessory As New List(Of Domain.Entities.EquipmentReceptionAccesory)
    Dim DeletedListConsumable As New List(Of Domain.Entities.EquipmentReceptionConsumable)
    Dim DeleteManualList As New List(Of Domain.Entities.ManualDetail)
    Dim DeleteDrawingsList As New List(Of Domain.Entities.DrawingsDetail)
    Dim DeletedListPartAccesoryConsumable As New List(Of Domain.Entities.EquipmentReceptionPartsAccesoriesConsumables)
    Dim Delete As Boolean

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.MaintenanceSequence

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _datasourceunit = Nothing
        _datasourceInventoryType = Nothing
        ListEquipmentType = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        EquipmentReception = Nothing
        Model = Nothing
        Presenter = Nothing
        ServerDate = Nothing
        ModelMeasureUnit = Nothing
        ListParts = Nothing
        ListAccessory = Nothing
        ListConsumable = Nothing
        ListPartAccesoryConsumable = Nothing
        ListDetail = Nothing
        DeletedListTechnicalLog = Nothing
        DeletedListPart = Nothing
        DeletedListAccessory = Nothing
        DeletedListConsumable = Nothing
        DeleteManualList = Nothing
        DeleteDrawingsList = Nothing
        DeletedListPartAccesoryConsumable = Nothing
        Delete = Nothing
        ListInvimaRegistration = Nothing
        ListDeleteInvimaRegistration = Nothing
        FlagInvimaRegistration = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSpecificConcepts_Load(sender As Object, e As EventArgs) Handles Me.Load
        '  INDgleUnit.DataSource = DatasourceUnit
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        INDgleFuenteAlimentacion.Properties.DataSource = DatasourceFuenteAlimentacion
        INDgleUso.Properties.DataSource = DatasourceUse
        'INDgleRiesgos.Properties.DataSource = DatasourceRisk
        INDgleTecnologiaPredominante.Properties.DataSource = DatasourceTechnology
        INDgleFrequencyComputerUse.Properties.DataSource = DatasourceFrecUso
        INDgleLocationType.Properties.DataSource = DatasourceLocation
        'INDglePartsAccesoriesConsumables.Properties.DataSource = DatasourceEquipmentType
        'INDgleMeasurementUnit.Properties.DataSource = DatasourceUnit

        Me.LoadStatus()
        Presenter = New PEquipmentReception(Me)
        Presenter.Initializes()
        'Presenter.GetSequense()
        'Presenter.GetMaintenanceParameters()
        Deshacer()
        INDBteCode.Focus()
        CtrFoto.INDOrigen = 1
        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)

        INDdtFechaAdquisicion.Properties.MaxValue = Date.Now()
        INDdtFechaInstalacion.Properties.MaxValue = Date.Now()
        INDdtFechaInicioOperacion.Properties.MaxValue = Date.Now()
        INDdtFechaFabricacion.Properties.MaxValue = Date.Now()
        'INDgleTypeInventory.Properties.DataSource = DataSourceInventoryType
        indigo = SessionValues.Instance
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        indigo.AuditMessageWcf.Company = indigo.TransactionalContainer

        IndigoGridControl1.RefreshGrid(INDgcInvima)
        SetActionsGrid()
    End Sub

    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        AdditionalControlPanel.Visible = True
        AdditionalControlPanel.Controls.Add(CtrFoto)
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
#End Region

#Region "Methods"
    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}}

        IndigoGridViewInvima.SetListAcction(INDgvInvima, _listActions)
    End Sub
#End Region

#Region "CRUD Base"


    ' ''' <summary>
    ' ''' Metodo que se utiliza para seleccionar la ubicaciobn en la que se encuentra el equipo
    ' ''' </summary>
    'Private Sub RecorreNodos(Nodos As DevExpress.XtraTreeList.Nodes.TreeListNode)
    '    Dim Nodo As DevExpress.XtraTreeList.Nodes.TreeListNode = Nodos
    '    For i = 0 To Nodo.Nodes.Count - 1
    '        If Nodo.Nodes.Item(i).GetValue("Id") = IdLocation Then
    '            INDTreeListLocation.SetNodeCheckState(Nodo.Nodes.Item(i), CheckState.Checked)
    '            INDpcLocation.Text = Nodo.Nodes.Item(i).GetValue("CodeName").ToString.Trim()
    '            Exit For
    '        End If
    '        If Nodo.Nodes.Count > 0 Then
    '            Call RecorreNodos(Nodo.Nodes.Item(i))
    '        End If
    '    Next
    'End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeEquipmentReception) AndAlso Not String.IsNullOrWhiteSpace(CodeEquipmentReception) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MEquipmentReception()
                    AsyncLoader(True)
                    EquipmentReception = Await Model.GetEquipmentRegistrationCAsync(INDBteCode.Text.Trim)
                    INDLyCtrEquipmentReception.BeginUpdate()
                    If EquipmentReception IsNot Nothing AndAlso EquipmentReception.Id > 0 Then

                        FixedAssetPhysicalAssetId = EquipmentReception.FixedAssetPhysicalAssetId

                        Me.BarraBotones.StatusRecordVisible = True

                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(EquipmentReception.Id))
                        With EquipmentReception
                            LogicaBotonActualizar(True)

                            'cargamos los registros tecnicos 
                            INDgcRegistroTecnico.DataSource = .TechnicalLogDetail.ToList()

                            INDBteCode.Text = .Plate
                            'INDtxtBarCode.Text = .BarCode
                            INDgleTypeInventory.EditValue = .InventoryTypeName
                            'INDtxtNumberInventory.Text = .InventoryNumber
                            INDglEquipmentType.EditValue = .EquipmentTypeName 'esta amarrado al tipo de equipo
                            INDSlEquipment.EditValue = .ArticleName
                            INDSlTrademark.EditValue = .TrademarkName
                            INDtxtModelo.Text = .Model
                            INDtxtSerie.Text = .Serie
                            INDtxtDescripcionEquipo.Text = .ArticleName
                            INDspLifeTime.EditValue = .LifeTime
                            INDgleUnit.EditValue = .MeasurementUnit
                            INDglResponsible.EditValue = .ResponsibleName
                            INDglLocation.EditValue = .LocationName
                            INDgleAcquisition.EditValue = .AdquisitionTypeName
                            INDtxtNroFactura.EditValue = .NumberContractLeasing
                            INDglManufacter.EditValue = .IdManufacturer
                            INDglVendedor.EditValue = .IdSeller
                            'IdResponsible = .IdResponsible
                            'INDglResponsableGarantia.EditValue = .IdWarranty
                            INDdtFechaAdquisicion.EditValue = .PurchaseDate
                            INDtxtValorAdquisicion.EditValue = .PurchaseValue
                            INDtxtOther.Text = .Other
                            INDdtFechaInstalacion.EditValue = .InstallationDate
                            INDdtFechaInicioOperacion.EditValue = .InitialOperationDate
                            INDdtFechaVencimiento.EditValue = .WarrantyExpirationDate
                            INDdtFechaFabricacion.EditValue = .ManufactureDate
                            INDglPolize.EditValue = .PolizaName
                            'INDtxtNumberInventory.Text = .InventoryNumber
                            INDgleFuenteAlimentacion.EditValue = .FeedingSource
                            INDgleFrequencyComputerUse.EditValue = .FrequencyComputerUse
                            INDgleTecnologiaPredominante.EditValue = .PredominantTechnology
                            INDglEquipmentFunction.EditValue = .IdEquipmentFunction
                            INDgleLocationType.EditValue = .LocationTypeName
                            INDgleUso.EditValue = .Use
                            'INDgleRiesgos.EditValue = .Risks
                            INDglPhysicalRisk.EditValue = .IdPhysicalRisk
                            INDglEquipmentRequirement.EditValue = .IdEquipmentRequirement
                            INDglEquipmentHistory.EditValue = .IdEquipmentHistory

                            'nulltext
                            INDglManufacter.Properties.NullText = .ManufacturerName
                            INDglVendedor.Properties.NullText = .SellerName
                            INDglEquipmentFunction.Properties.NullText = .EquipmentFunctionName
                            INDglPhysicalRisk.Properties.NullText = .PhysicalRiskName
                            INDtxtOther.Text = .Other

                            Delete = True
                            'cargamos informcion del panel de informacion de apoyo tecnico*******
                            'manuales
                            For i = 0 To .ManualDetail.Count - 1
                                INDckManuales.SelectedIndex = .ManualDetail.Item(i).Manual
                                INDckManuales.Items(.ManualDetail.Item(i).Manual).CheckState = CheckState.Checked
                            Next
                            'planos
                            Dim Item As Integer
                            For i = 0 To .DrawingsDetail.Count - 1
                                Item = CInt(.DrawingsDetail.Item(i).Drawings.ToString.Trim)
                                INDckPlanos.Items(Item - 1).CheckState = CheckState.Checked
                            Next
                            Delete = False
                            'ficha tecnica 

                            If .TechnicalEquipmentSheet.Count > 0 Then
                                INDmemoDatosGenerales.Text = .TechnicalEquipmentSheet.Item(0).GeneralData
                                INDmemoCondiciones.Text = .TechnicalEquipmentSheet.Item(0).Terms
                                INDmemoDescripcionFuncionamiento.Text = .TechnicalEquipmentSheet.Item(0).OperationDescription
                                INDMemoPrecauciones.Text = .TechnicalEquipmentSheet.Item(0).HandlingPrecautions
                                INDmemoLimpieza.Text = .TechnicalEquipmentSheet.Item(0).Cleaning
                            End If
                            CtrFoto.Foto = .Photo
                            StateEquipmentReceptions = .State

                            ListInvimaRegistration = .EquipmentInvima.ToList
                        End With
                        INDgcInvima.DataSource = Nothing
                        INDgcInvima.DataSource = ListInvimaRegistration

                        RefreshDetails()

                        'Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.EquipmentReception.Plate)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = EquipmentReception.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(EquipmentReception.Id, Me.Tag.ToString(), Nothing, GetType(EquipmentRegistration).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True

                    ElseIf EquipmentReception IsNot Nothing Then
                        FixedAssetPhysicalAssetId = EquipmentReception.FixedAssetPhysicalAssetId
                        Me.BarraBotones.StatusRecordVisible = True
                        With EquipmentReception
                            INDBteCode.Text = .Plate
                            INDgleTypeInventory.EditValue = .InventoryTypeName
                            INDglEquipmentType.EditValue = .EquipmentTypeName 'esta amarrado al tipo de equipo
                            INDSlEquipment.EditValue = .ArticleName
                            INDSlTrademark.EditValue = .TrademarkName
                            INDtxtModelo.Text = .Model
                            INDtxtSerie.Text = .Serie
                            INDtxtDescripcionEquipo.Text = .ArticleName
                            INDspLifeTime.EditValue = .LifeTime
                            INDgleUnit.EditValue = .MeasurementUnit
                            INDglResponsible.EditValue = .ResponsibleName
                            INDglLocation.EditValue = .LocationName
                            INDgleAcquisition.EditValue = .AdquisitionTypeName
                            INDtxtNroFactura.EditValue = .NumberContractLeasing
                            INDglManufacter.EditValue = .IdManufacturer
                            INDglVendedor.EditValue = .IdSeller
                            'IdResponsible = .IdResponsible
                            INDdtFechaAdquisicion.EditValue = .PurchaseDate
                            INDtxtValorAdquisicion.EditValue = .PurchaseValue
                            INDtxtOther.Text = .Other
                            INDdtFechaInstalacion.EditValue = .InstallationDate
                            INDdtFechaInicioOperacion.EditValue = ValidateDateForDatabaseServer(.InitialOperationDate)
                            INDdtFechaVencimiento.EditValue = .WarrantyExpirationDate
                            INDdtFechaFabricacion.EditValue = .ManufactureDate
                            INDglPolize.EditValue = .PolizaName
                            INDgleFuenteAlimentacion.EditValue = .FeedingSource
                            INDgleFrequencyComputerUse.EditValue = .FrequencyComputerUse
                            INDgleTecnologiaPredominante.EditValue = .PredominantTechnology
                            INDglEquipmentFunction.EditValue = .IdEquipmentFunction
                            INDgleLocationType.EditValue = .LocationTypeName
                            INDgleUso.EditValue = .Use
                            INDglPhysicalRisk.EditValue = .IdPhysicalRisk
                            INDglEquipmentRequirement.EditValue = .IdEquipmentRequirement
                            CtrFoto.Foto = .Photo
                            StateEquipmentReceptions = True
                        End With
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEquipmentReception()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", "Maintenance")
                            CodeEquipmentReception = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If

                    'Ocultar informacion cuando no sea un biomedico
                    INDGrCaracteristicasTecnicas.HideControl(Not {1, 2, 3}.Contains(EquipmentReception.InventoryType))
                    loadtechnicalLogs(FixedAssetPhysicalAssetId)

                    ' Cargamos los protocolos
                    loadProtocols(EquipmentReception.FixedAssetItemId)
                    loadScheduledMaintenances(EquipmentReception.FixedAssetPhysicalAssetId)
                    loadUnScheduledMaintenances(EquipmentReception.FixedAssetPhysicalAssetId)
                    LoadWorkOrders(EquipmentReception.FixedAssetPhysicalAssetId)
                    INDLyCtrEquipmentReception.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Sub loadScheduledMaintenances(fixedAssetPhysicalAssetId As Integer)
        INDGcMantenimientos.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MaintenanceService.ListViewWorkOrderScheduledMaintenance($"[PhysicalAssetId]={fixedAssetPhysicalAssetId}", "ProgramDate DESC")
    End Sub

    Private Sub loadUnScheduledMaintenances(fixedAssetPhysicalAssetId As Integer)
        INDGcMantenimientoNoProgramado.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MaintenanceService.ListViewWorkOrderUnScheduledMaintenance($"[PhysicalAssetId]={fixedAssetPhysicalAssetId}", "ProgramDate DESC")
    End Sub

    Private Sub LoadWorkOrders(fixedAssetPhysicalAssetId As Integer)
        INDGcWorkOrders.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MaintenanceService.ListWorkOrders($"PhysicalAssetId.Id = {fixedAssetPhysicalAssetId}", "ProgramDate DESC")
    End Sub

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.EquipmentReception IsNot Nothing AndAlso Me.EquipmentReception.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                'DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo para obtener un documento indexado
    ''' </summary>
    Public Async Sub GetDocumentIndexed(TextSearch As String)
        Dim indexDocument As IndexedDocumentResultSet2 = Await Me._model.SearchIndexingDocument(TextSearch)
        Me._doc = indexDocument.Results.FirstOrDefault
    End Sub

    ''' <summary>
    ''' Metodo para eliminar un documento indexado
    ''' </summary>
    Public Async Function DeleteDocumentIndexed() As Threading.Tasks.Task
        Await Me._model.DeleteIndexingDocument(Me._doc)
    End Function

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Save() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        If EquipmentReception IsNot Nothing AndAlso EquipmentReception.TechnicalLogDetail IsNot Nothing AndAlso EquipmentReception.TechnicalLogDetail.Any() Then
            If EquipmentReception.TechnicalLogDetail.Any(Function(m) m.ValueMax < m.ValueMin) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor Mínimo no debe superar al valor Máximo"
                Exit Sub
            End If
        End If
        If EquipmentReception.TechnicalLogDetail.Where(Function(X) X.ValueMin = String.Empty).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe especificar el valor mínimo de las caracteristicas tecnicas agregadas"
            Exit Sub
        End If
        AssigningValues()
        'marcamos los objetos eliminados  ******
        If DeletedListTechnicalLog.Count > 0 Then
            EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeletedListTechnicalLog.Count - 1
                If DeletedListTechnicalLog.Item(i).Id <> 0 Then
                    DeletedListTechnicalLog.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    EquipmentReception.TechnicalLogDetail.Add(DeletedListTechnicalLog.Item(i))
                End If
            Next
        End If

        If DeletedListPartAccesoryConsumable.Count > 0 Then
            EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeletedListPartAccesoryConsumable.Count - 1
                If DeletedListPartAccesoryConsumable.Item(i).Id <> 0 Then
                    DeletedListPartAccesoryConsumable.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.Add(DeletedListPartAccesoryConsumable.Item(i))
                End If
            Next
        End If
        '*****************************
        If DeleteManualList.Count > 0 Then
            EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeleteManualList.Count - 1
                If DeleteManualList.Item(i).Id <> 0 Then
                    DeleteManualList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    EquipmentReception.ManualDetail.Add(DeleteManualList.Item(i))
                End If
            Next
        End If
        If DeleteDrawingsList.Count > 0 Then
            EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            For i As Integer = 0 To DeleteDrawingsList.Count - 1
                If DeleteDrawingsList.Item(i).Id <> 0 Then
                    DeleteDrawingsList.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    EquipmentReception.DrawingsDetail.Add(DeleteDrawingsList.Item(i))
                End If
            Next
        End If
        If EquipmentReception.Id > 0 Then
            EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
        End If
        Try
            Using Model As New MEquipmentReception
                AsyncLoader(True)
                Dim resultAction As ActionResult(Of Domain.Entities.EquipmentRegistration) = Await Model.SaveEquipmentRegistrationAsync(EquipmentReception, Me._idCurrentSequence)
                If resultAction.StateResult = True Then
                    EquipmentReception = resultAction.ObjectEmbbeded
                    If EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.EquipmentReception.Plate)
                    ElseIf EquipmentReception.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = "Se ha Actualizado correctamente el Equipo con el Código " + EquipmentReception.Plate
                        'Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    End If
                    AsyncLoader(False)

                    RaiseEvent Saved()

                    Deshacer()
                    'EnabledControls(False)
                    'CleanControls()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = resultAction.Message ' obtenerRecurso(ComunesContacteAdministrador)
                End If
            End Using
            'EnabledControls(False)
            'CleanControls()
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me.EquipmentReception IsNot Nothing AndAlso Me.EquipmentReception.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEquipmentReception()
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEquipmentRegistrationAsync(Me.EquipmentReception)
                        If result Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        End If


        'Try
        '    If EquipmentReception IsNot Nothing Then
        '        If EquipmentReception.Id > 0 Then
        '            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '                Using Model As New MEquipmentReception
        '                    If Await Model.DeleteEquipmentRegistrationAsync(EquipmentReception) = True Then
        '                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                        CleanControls()
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                    End If
        '                End Using
        '            End If
        '        Else
        '            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneTorre, Torres)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub
    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo() With {.Caption = "Placa", .FieldName = "Plate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                New ColumnInfo() With {.Caption = "Descripción", .FieldName = "ItemId.Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                New ColumnInfo() With {.Caption = "Serie", .FieldName = "Serie", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                New ColumnInfo() With {.Caption = "Ubicación", .FieldName = "LocationId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                New ColumnInfo() With {.Caption = "Marca", .FieldName = "IdTrademark.Descripcion", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}
            }.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset
            .ValorSolicitado = "Plate"
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
        INDBteCode.Text = ReturnValue
        FixedAssetPhysicalAssetId = ReturnObject.Id
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Private Sub loadProtocols(fixedAssetItemId)
        INDGcProtocol.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListMaintenanceProtocolByStatusAndFixedAssetItemId(fixedAssetItemId, True)
        INDGcProtocol.RefreshDataSource()
    End Sub

    Private Sub loadtechnicalLogs(fixedAssetPhysicalAssetId As Integer)
        'INDgcRegistroTecnico.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MaintenanceService.ListTechnicalLogByFixedAssetPhysicalId(fixedAssetPhysicalAssetId)
        CType(INDgcRegistroTecnico.MainView, DevExpress.XtraGrid.Views.Grid.GridView).ShowLoadingPanel()
        Task.Factory.StartNew(Async Sub()
                                  Using model As New MEquipmentReception()
                                      Dim res As List(Of TechnicalLogDetail) = Await model.ListTechnicalLogByFixedAssetPhysicalId(fixedAssetPhysicalAssetId, EquipmentReception.Id)
                                      If res IsNot Nothing AndAlso res.Any() Then
                                          For Each i In res
                                              EquipmentReception.TechnicalLogDetail.Add(i)
                                          Next
                                      End If
                                      Me.SafeInvoke(Sub()
                                                        INDgcRegistroTecnico.DataSource = res
                                                        CType(INDgcRegistroTecnico.MainView, DevExpress.XtraGrid.Views.Grid.GridView).HideLoadingPanel()
                                                    End Sub)

                                  End Using
                              End Sub)
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEquipmentReception()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public WriteOnly Property TrademarkDataSource As List(Of Object) Implements IEquipmentReception.TrademarkDataSource
        Set(value As List(Of Object))
            Throw New NotImplementedException()
        End Set
    End Property

    Public WriteOnly Property EquipmentDataSource As List(Of Equipment) Implements IEquipmentReception.EquipmentDataSource
        Set(value As List(Of Equipment))
            Throw New NotImplementedException()
        End Set
    End Property

#End Region

#Region "Metodos Funciones"
    ''' <summary>
    ''' Este método valida si la valor es una fecha mínima y aumenta el año para que sea valida para el servidor de base de datos
    ''' </summary>
    ''' <param name="_date"></param>
    Private Function ValidateDateForDatabaseServer(_date As DateTime) As DateTime
        If _date.Year < 1753 Then
            Dim minDateDbServer As New Date(1753, 1, 1)
            Return minDateDbServer
        Else
            Return _date
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Activo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "Inactivo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        LayoutControlGroup1.BeginUpdate()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        INDBteCode.Text = String.Empty
        INDglLocation.EditValue = Nothing
        'INDglInventoryType.EditValue = Nothing
        INDglEquipmentType.EditValue = Nothing
        INDtxtDescripcionEquipo.Text = String.Empty
        INDspLifeTime.EditValue = Nothing
        INDspLifeTime.Text = String.Empty
        INDgleUnit.EditValue = Nothing

        'INDtxtMarca.Text = String.Empty
        INDtxtModelo.Text = String.Empty
        INDtxtSerie.Text = String.Empty
        'INDtxtNroInventario.Text = String.Empty
        INDglResponsible.EditValue = Nothing
        INDglResponsible.Text = String.Empty
        'INDtxtBarCode.Text = String.Empty

        INDgleAcquisition.EditValue = Nothing
        INDtxtOther.Text = String.Empty
        INDglManufacter.EditValue = Nothing
        INDglVendedor.EditValue = Nothing
        INDglResponsableGarantia.EditValue = Nothing
        INDtxtValorAdquisicion.Text = String.Empty
        INDdtFechaAdquisicion.EditValue = Nothing
        INDdtFechaFabricacion.EditValue = Nothing
        INDdtFechaInicioOperacion.EditValue = Nothing
        INDdtFechaInstalacion.EditValue = Nothing
        INDdtFechaVencimiento.EditValue = Nothing
        INDglPolize.EditValue = Nothing
        INDtxtNroFactura.Text = String.Empty

        INDgleFuenteAlimentacion.EditValue = Nothing
        INDgleUso.EditValue = Nothing
        'INDgleRiesgos.EditValue = Nothing
        INDgleTecnologiaPredominante.EditValue = Nothing
        INDglEquipmentFunction.EditValue = Nothing
        INDgleFrequencyComputerUse.EditValue = Nothing
        INDgleLocationType.EditValue = Nothing
        INDglPhysicalRisk.EditValue = Nothing
        INDglPhysicalRisk.Text = String.Empty
        INDglEquipmentRequirement.EditValue = Nothing
        INDglEquipmentHistory.EditValue = Nothing

        INDgleTypeInventory.EditValue = Nothing
        'INDtxtNumberInventory.Text = String.Empty
        INDSlEquipment.EditValue = Nothing
        INDSlTrademark.EditValue = Nothing

        INDglTechnicalLog.EditValue = Nothing
        INDgcRegistroTecnico.DataSource = Nothing
        'INDglMeasureUnit.EditValue = Nothing

        INDSlePartsAccesoriesConsumables.EditValue = Nothing
        INDgcDetail.DataSource = Nothing
        INDGcMantenimientos.DataSource = Nothing
        INDGcMantenimientoNoProgramado.DataSource = Nothing
        INDGcWorkOrders.DataSource = Nothing

        Delete = True
        INDckManuales.UnCheckAll()
        INDckPlanos.UnCheckAll()
        Delete = False
        'limpiamos los listados
        DeleteManualList.Clear()
        DeleteDrawingsList.Clear()
        ListParts.Clear()
        ListAccessory.Clear()
        ListConsumable.Clear()
        ListPartAccesoryConsumable.Clear()
        ListInvimaRegistration = Nothing
        ListDeleteInvimaRegistration = Nothing
        INDgcInvima.DataSource = Nothing
        CleanControlsPopup()

        INDglManufacter.Properties.NullText = ""
        INDglVendedor.Properties.NullText = ""
        INDglEquipmentFunction.Properties.NullText = ""
        INDglPhysicalRisk.Properties.NullText = ""

        DeletedListTechnicalLog.Clear()
        'DeletedListPart.Clear()
        'DeletedListAccessory.Clear()
        'DeletedListConsumable.Clear()
        DeletedListPartAccesoryConsumable.Clear()

        INDmemoCondiciones.Text = String.Empty
        INDmemoDescripcionFuncionamiento.Text = String.Empty
        INDMemoPrecauciones.Text = String.Empty
        INDmemoLimpieza.Text = String.Empty
        INDmemoDatosGenerales.Text = String.Empty

        INDBteCode.Focus()


        CtrFoto.LimpiarControles()
        CtrFoto.SetImage = Presentation.Controls.CtrFoto.eImagen.Mantenimiento

        'ocultamos las rejillas
        'INDlyItemTechnicalLogDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'INDlyItemDetailEquipment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'ActionsOnControls = False
        EnabledControls(False)
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        Me.BarraBotones.ReassignOperatingUnit()

        Presenter.GetMaintenanceParameters()
        LayoutControlGroup1.EndUpdate()
    End Sub

    Private Sub CleanControlsPopup()
        FlagInvimaRegistration = False
        INDTxtInvimaDate.EditValue = Nothing
        INDTxtInvimaNumberRegister.EditValue = Nothing
        INDTxtExpirationDate.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        'validamos la cabecera
        If Object.Equals(INDglLocation.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        'If Object.Equals(INDglInventoryType.EditValue, Nothing) = True Then
        '    ValidateControls = False
        'End If
        If Object.Equals(INDglEquipmentType.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        If String.IsNullOrEmpty(INDtxtDescripcionEquipo.Text) = True Then
            ValidateControls = False
        End If
        If Object.Equals(INDspLifeTime.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        If Object.Equals(INDgleUnit.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        'If INDtxtMarca.Text = String.Empty Then
        '    Return False
        'End If
        If INDtxtSerie.Text = String.Empty Then
            Return False
        End If
        'If INDtxtNroInventario.Text = String.Empty Then
        '    Return False
        'End If
        If Object.Equals(INDglResponsible.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        If Object.Equals(INDgleAcquisition.EditValue, Nothing) = True Then
            ValidateControls = False
        End If
        If INDgleAcquisition.EditValue = "8" Then
            If INDtxtOther.Text = String.Empty Then
                ValidateControls = False
            End If
        End If
        If String.IsNullOrEmpty(INDglVendedor.EditValue) OrElse INDglVendedor.EditValue = 0 Then
            ValidateControls = False
        End If
        If String.IsNullOrEmpty(INDglManufacter.EditValue) OrElse INDglManufacter.EditValue = 0 Then
            Return False
        End If
        'If Object.Equals(INDglResponsableGarantia.EditValue, Nothing) = True Then
        '    Return False
        'End If
        If Object.Equals(INDtxtValorAdquisicion.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDdtFechaAdquisicion.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDdtFechaInstalacion.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDdtFechaInicioOperacion.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDdtFechaVencimiento.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDdtFechaFabricacion.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDglPolize.EditValue, Nothing) = True Then
            Return False
        End If
        'If INDtxtNroFactura.Text = String.Empty Then
        '    Return False
        'End If

        If INDGrCaracteristicasTecnicas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If Object.Equals(INDgleFuenteAlimentacion.EditValue, Nothing) = True Then
                Return False
            End If
            'If FlagTypeInventory = True Then
            '    'If Object.Equals(INDgleRiesgos.EditValue, Nothing) Then
            '    '    Return False
            '    'End If

            '    If Object.Equals(INDglEquipmentFunction.EditValue, Nothing) = True Then
            '        Return False
            '    End If
            'End If
            If Object.Equals(INDgleTecnologiaPredominante.EditValue, Nothing) = True Then
                Return False
            End If

            If Object.Equals(INDgleFrequencyComputerUse.EditValue, Nothing) = True Then
                Return False
            End If
            'If Object.Equals(INDgleLocationType.EditValue, Nothing) = True Then
            '    Return False
            'End If
        End If
    End Function
    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With EquipmentReception
            .FixedAssetPhysicalAssetId = FixedAssetPhysicalAssetId
            .Plate = INDBteCode.Text.Trim()
            .IdManufacturer = INDglManufacter.EditValue
            .IdSeller = INDglVendedor.EditValue
            .ManufactureDate = INDdtFechaFabricacion.EditValue
            .InstallationDate = INDdtFechaInstalacion.EditValue 'INDdtDateInstalation.EditValue
            .InitialOperationDate = INDdtFechaInicioOperacion.EditValue
            .WarrantyExpirationDate = INDdtFechaVencimiento.EditValue
            .LifeTime = INDspLifeTime.EditValue
            .MeasurementUnit = INDgleUnit.EditValue
            .FeedingSource = INDgleFuenteAlimentacion.EditValue
            .FrequencyComputerUse = INDgleFrequencyComputerUse.EditValue
            .PredominantTechnology = INDgleTecnologiaPredominante.EditValue
            .IdEquipmentFunction = INDglEquipmentFunction.EditValue
            .Use = INDgleUso.EditValue
            .IdPhysicalRisk = INDglPhysicalRisk.EditValue
            .IdEquipmentHistory = INDglEquipmentHistory.EditValue
            .IdEquipmentRequirement = INDglEquipmentRequirement.EditValue
            .Photo = CtrFoto.Foto
            .State = StateEquipmentReceptions

            .EquipmentInvima.Clear()
            If ListInvimaRegistration IsNot Nothing Then
                For Each itemdetail As EquipmentInvima In ListInvimaRegistration
                    .EquipmentInvima.Add(itemdetail)
                Next
            End If
            If ListDeleteInvimaRegistration IsNot Nothing Then
                For Each itemdetail As EquipmentInvima In ListDeleteInvimaRegistration
                    .EquipmentInvima.Add(itemdetail)
                Next

            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With


        If EquipmentReception.TechnicalLogDetail.Where(Function(m) m.ValueMax = 0 OrElse m.IdMeasurementUnit = 0).Any() Then
            While EquipmentReception.TechnicalLogDetail.Where(Function(m) m.ValueMax = 0 OrElse m.IdMeasurementUnit = 0).Any()
                EquipmentReception.TechnicalLogDetail.Where(Function(m) m.ValueMax = 0 OrElse m.IdMeasurementUnit = 0).FirstOrDefault().MarkAsDeleted()
            End While
        End If

        Dim technicalEquipmtenSheet As TechnicalEquipmentSheet = Nothing

        If EquipmentReception.TechnicalEquipmentSheet.IsNotNullAndAny() Then
            technicalEquipmtenSheet = EquipmentReception.TechnicalEquipmentSheet(0)
        Else
            technicalEquipmtenSheet = New TechnicalEquipmentSheet()
        End If

        technicalEquipmtenSheet.Terms = INDmemoCondiciones.Text
        technicalEquipmtenSheet.GeneralData = INDmemoDatosGenerales.Text
        technicalEquipmtenSheet.OperationDescription = INDmemoDescripcionFuncionamiento.Text
        technicalEquipmtenSheet.HandlingPrecautions = INDMemoPrecauciones.Text
        technicalEquipmtenSheet.Cleaning = INDmemoLimpieza.Text

        EquipmentReception.TechnicalEquipmentSheet.Add(technicalEquipmtenSheet)
    End Sub

#Region "ContexMenuActions"
    Private Sub IndigoGridViewInvima_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewInvima.ContexMenuActions, IndigoGridViewInvima.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditRegistrationInvima()
            Case "Remove"
                DeleteRegistrationInvima()
        End Select
    End Sub

    Private Sub EditRegistrationInvima()
        FlagInvimaRegistration = True
        InvimaRegistration = CType(INDgvInvima.GetFocusedRow, EquipmentInvima)
        With InvimaRegistration
            Me.INDTxtInvimaDate.EditValue = .DateInit
            Me.INDTxtInvimaNumberRegister.EditValue = .NumberRegister
            Me.INDTxtExpirationDate.EditValue = .ExpirationDate
            INDpceAddInventory.ShowPopup()
        End With
        INDTxtInvimaDate.Focus()
    End Sub



    Private Sub DeleteRegistrationInvima()
        Dim Ild As EquipmentInvima = CType(INDgvInvima.GetFocusedRow, EquipmentInvima)
        If Ild.Id <> 0 Then
            If ListDeleteInvimaRegistration Is Nothing Then
                ListDeleteInvimaRegistration = New List(Of EquipmentInvima)
            End If
            Ild.MarkAsDeleted()
            ListDeleteInvimaRegistration.Add(Ild)
        End If
        ListInvimaRegistration.Remove(Ild)
        INDgcInvima.DataSource = Nothing
        INDgcInvima.DataSource = ListInvimaRegistration
    End Sub


    Private Sub AddInvimaRegistration()
        If FlagInvimaRegistration = False Then
            Dim InvimaRegistration As New EquipmentInvima
            With InvimaRegistration
                .DateInit = INDTxtInvimaDate.EditValue
                .NumberRegister = INDTxtInvimaNumberRegister.EditValue
                .ExpirationDate = INDTxtExpirationDate.EditValue
                .State = True
            End With
            If ListInvimaRegistration Is Nothing Then
                ListInvimaRegistration = New List(Of EquipmentInvima)
            End If
            ListInvimaRegistration.Add(InvimaRegistration)

        Else
            With InvimaRegistration
                .DateInit = INDTxtInvimaDate.EditValue
                .NumberRegister = INDTxtInvimaNumberRegister.EditValue
                .ExpirationDate = INDTxtExpirationDate.EditValue
            End With
        End If

        INDgcInvima.DataSource = ListInvimaRegistration
        INDgcInvima.RefreshDataSource()
        CleanControlsPopup()
    End Sub



#End Region
    Private _partAccesoryConsumableSelected As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_ViewPartAccesoryConsumables

    Private Sub GridView9_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView9.RowClick
        Dim obj = DirectCast(INDSlePartsAccesoriesConsumables.Properties.View.GetRow(e.RowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
            _partAccesoryConsumableSelected = obj.OriginalRow
        End If
    End Sub


    Private _listDetails As New List(Of Object)()

    Public Sub RefreshDetails()
        _listDetails.Clear()
        _listDetails.AddRange(EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.Select(Function(m) New With {.Type = 1, .TypeName = "PARTES", .Code = m.PartCode, .Name = m.PartName, m.Id, .DocumentId = m.IdPartsAccesoriesConsumables}).ToList())
        _listDetails.AddRange(EquipmentReception.EquipmentReceptionAccesory.Select(Function(m) New With {.Type = 2, .TypeName = "ACCESORIOS", .Code = m.AccesoryCode, .Name = m.AccesoryName, m.Id, .DocumentId = m.IdAccesory}).ToList())
        _listDetails.AddRange(EquipmentReception.EquipmentReceptionConsumable.Select(Function(m) New With {.Type = 3, .TypeName = "CONSUMIBLES", .Code = m.ConsumableCode, .Name = m.ConsumableName, m.Id, .DocumentId = m.IdConsumable}).ToList())

        INDgcDetail.DataSource = _listDetails
        INDgcDetail.RefreshDataSource()
        CType(INDgcDetail.MainView, DevExpress.XtraGrid.Views.Grid.GridView).ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para agregar los registros de consumibles, accesorios y partes al detalle del ingreso del equipo INDgcRegistroTecnico
    ''' </summary>
    Private Sub AddDetail()
        If INDSlePartsAccesoriesConsumables.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Accesorio / Consumible"
            INDSlePartsAccesoriesConsumables.Focus()
            Return
        End If

        If _partAccesoryConsumableSelected.TypeDetail = 1 Then
            'Partes
        ElseIf _partAccesoryConsumableSelected.TypeDetail = 2 Then
            'Accesorios
            If _partAccesoryConsumableSelected IsNot Nothing Then
                If EquipmentReception.EquipmentReceptionAccesory.Any(Function(o) o.IdAccesory = _partAccesoryConsumableSelected.DocumentId) Then
                    Mensaje(EeventViewerImages.Advertencia) = "El Accesorio seleccionado ya se encuentra en el listado"
                    INDSlePartsAccesoriesConsumables.Focus()
                    Return
                End If
                EquipmentReception.EquipmentReceptionAccesory.Add(New EquipmentReceptionAccesory() With {.IdAccesory = _partAccesoryConsumableSelected.DocumentId, .AccesoryCode = _partAccesoryConsumableSelected.Code, .AccesoryName = _partAccesoryConsumableSelected.Name})
                'INDgcDetail.DataSource = EquipmentReception.EquipmentReceptionAccesory.ToList()
                'INDgcDetail.RefreshDataSource()
                RefreshDetails()

                INDSlePartsAccesoriesConsumables.EditValue = Nothing
                _partAccesoryConsumableSelected = Nothing
                INDSlePartsAccesoriesConsumables.Focus()
            End If
        ElseIf _partAccesoryConsumableSelected.TypeDetail = 3 Then
            'Consumibles
            If _partAccesoryConsumableSelected IsNot Nothing Then
                If EquipmentReception.EquipmentReceptionConsumable.Any(Function(o) o.IdConsumable = _partAccesoryConsumableSelected.DocumentId) Then
                    Mensaje(EeventViewerImages.Advertencia) = "El Consumible seleccionado ya se encuentra en el listado"
                    INDSlePartsAccesoriesConsumables.Focus()
                    Return
                End If
                EquipmentReception.EquipmentReceptionConsumable.Add(New EquipmentReceptionConsumable() With {.IdConsumable = _partAccesoryConsumableSelected.DocumentId, .ConsumableCode = _partAccesoryConsumableSelected.Code, .ConsumableName = _partAccesoryConsumableSelected.Name})


                RefreshDetails()

                'INDgcDetail.DataSource = EquipmentReception.EquipmentReceptionConsumable.ToList()
                'INDgcDetail.RefreshDataSource()

                INDSlePartsAccesoriesConsumables.EditValue = Nothing
                _partAccesoryConsumableSelected = Nothing
                INDSlePartsAccesoriesConsumables.Focus()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para eliminar los registros de consumibles, accesorios y partes del detalle de ingreso del equipo 
    ''' </summary>
    Private Sub DeleteItemDetail()
        Dim Ids As String = INDgcDetailView.GetFocusedRowCellValue("Id")
        Dim Code As String = INDgcDetailView.GetFocusedRowCellValue("Code")



        If EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables IsNot Nothing And EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.Count() > 0 Then
            DeletedListPartAccesoryConsumable.Add(EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.Where(Function(x) x.IdPartsAccesoriesConsumables = Ids).FirstOrDefault)
            EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.Remove(EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.Where(Function(x) x.IdPartsAccesoriesConsumables = Ids).SingleOrDefault)
            ListDetail.Remove(ListDetail.Where(Function(x) x.Code = Code).FirstOrDefault())
            INDgcDetail.DataSource = EquipmentReception.EquipmentReceptionPartsAccesoriesConsumables.ToList()
        Else
            INDgcDetail.DataSource = ListDetail.Remove(ListDetail.Where(Function(x) x.Code = Code).FirstOrDefault())
        End If



        INDgcDetail.RefreshDataSource()


    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para eliminar los registros tecnicos agregados al equipo 
    ''' </summary>
    Private Sub DeleteItemTecnicalLog()
        If INDgcRegistroTecnicoView.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        Dim Row = INDgcRegistroTecnicoView.FocusedRowHandle
        DeletedListTechnicalLog.Add(EquipmentReception.TechnicalLogDetail.Item(Row))
        EquipmentReception.TechnicalLogDetail.RemoveAt(Row)
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para obtener la fecha de servidor 
    ''' </summary>
    Private Sub GetServerDate()
        ServerDate = CloudAgent.IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para agregar una nueva caracteristica a los consumibles ,accesorios , y partes
    ''' </summary>
    Private Function AddFeatureAdditional() As Boolean

        AddFeatureAdditional = True
        'Validamos  los controles al agregar las caracteristicas adicionales
        If String.IsNullOrEmpty(INDtxtRegisterInvima.Text) = True Then
            Return False
        End If
        If String.IsNullOrEmpty(INDTxtSupplierReference.Text) = True Then
            Return False
        End If
        If Object.Equals(INDspLifetimeTime.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDgleMeasurementUnit.EditValue, Nothing) = True Then
            Return False
        End If
        If Object.Equals(INDdtDateInstalation.EditValue, Nothing) = True Then
            Return False
        End If

        Select Case INDgcDetailView.GetFocusedRowCellValue("Type")
            Case Is = "Piezas" 'piezas
                Dim Part = INDgcDetailView.GetFocusedRowCellValue("Id").ToString
                'EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.RegisterInvima = INDtxtRegisterInvima.Text.Trim
                'EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.SupplierReference = INDTxtSupplierReference.Text.Trim
                'EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.LifetimeTime = INDspLifetimeTime.EditValue
                'EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.MeasurementUnit = INDgleMeasurementUnit.EditValue
                'EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.InstallationDate = INDdtDateInstalation.EditValue
            Case "Accesorios" 'accesorios
                Dim Accesory = INDgcDetailView.GetFocusedRowCellValue("Id").ToString
                EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.RegisterInvima = INDtxtRegisterInvima.Text
                EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.SupplierReference = INDTxtSupplierReference.Text.Trim
                EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.LifetimeTime = INDspLifetimeTime.EditValue
                EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.MeasurementUnit = INDgleMeasurementUnit.EditValue
                EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.InstallationDate = INDdtDateInstalation.EditValue
            Case "Consumibles" 'consumibles
                Dim Consumible = INDgcDetailView.GetFocusedRowCellValue("Id").ToString
                EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.RegisterInvima = INDtxtRegisterInvima.Text
                EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.SupplierReference = INDTxtSupplierReference.Text.Trim
                EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.LifetimeTime = INDspLifetimeTime.EditValue
                EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.MeasurementUnit = INDgleMeasurementUnit.EditValue
                EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.InstallationDate = INDdtDateInstalation.EditValue
        End Select

        INDtxtRegisterInvima.Text = String.Empty
        INDTxtSupplierReference.Text = String.Empty
        INDspLifetimeTime.EditValue = Nothing
        INDgleMeasurementUnit.EditValue = Nothing
        INDdtDateInstalation.EditValue = Nothing

    End Function

    ''' <summary>
    ''' Metodo que se utiliza para mostrar la informacion de las caracteristicas que se han agreagado a las piezas , consumibles , y accesorios 
    ''' </summary>
    Private Sub ShowInformarionFeature()
        Select Case INDgcDetailView.GetFocusedRowCellValue("Type")
            Case Is = "Piezas" 'piezas
                Dim Part = INDgcDetailView.GetFocusedRowCellValue("Id").ToString
                'INDtxtRegisterInvima.Text = EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.RegisterInvima
                'INDTxtSupplierReference.Text = EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.SupplierReference
                'INDspLifetimeTime.EditValue = EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.LifetimeTime
                'INDgleMeasurementUnit.EditValue = EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.MeasurementUnit
                'INDdtDateInstalation.EditValue = EquipmentReception.EquipmentReceptionParte.Where(Function(x) x.Idpart = Part).Single.InstallationDate
            Case "Accesorios" 'accesorios
                Dim Accesory = INDgcDetailView.GetFocusedRowCellValue("Id").ToString
                INDtxtRegisterInvima.Text = EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.RegisterInvima
                INDTxtSupplierReference.Text = EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.SupplierReference
                INDspLifetimeTime.EditValue = EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.LifetimeTime
                INDgleMeasurementUnit.EditValue = EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.MeasurementUnit
                INDdtDateInstalation.EditValue = EquipmentReception.EquipmentReceptionAccesory.Where(Function(x) x.IdAccesory = Accesory).Single.InstallationDate
            Case "Consumibles" 'consumibles
                Dim Consumible = INDgcDetailView.GetFocusedRowCellValue("Id").ToString
                INDtxtRegisterInvima.Text = EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.RegisterInvima
                INDTxtSupplierReference.Text = EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.SupplierReference
                INDspLifetimeTime.EditValue = EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.LifetimeTime
                INDgleMeasurementUnit.EditValue = EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.MeasurementUnit
                INDdtDateInstalation.EditValue = EquipmentReception.EquipmentReceptionConsumable.Where(Function(x) x.IdConsumable = Consumible).Single.InstallationDate
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para cargar (piezas, consumibles , accesorios) en el gridllookup del evento editvalue del control Type
    ''' </summary>
    Private Async Sub LoadDetailEquipment()
        If INDglEquipmentType.Enabled = False Then
            Exit Sub
        End If
        If Object.Equals(INDglEquipmentType.EditValue, Nothing) = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione el Tipo de Equipo"
            INDSlePartsAccesoriesConsumables.EditValue = Nothing
            INDglEquipmentType.Focus()
            Exit Sub
        End If
    End Sub

    Private Async Function NewEquipmentReception() As Task
        Me.EquipmentReception = New Domain.Entities.EquipmentRegistration()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MaintenanceSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeEquipmentReception = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeEquipmentReception = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeEquipmentReception = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeEquipmentReception = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

#End Region

#Region "Eventos Funcional"

    Private Async Sub INDtxtConsecutivo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(CodeEquipmentReception.Trim()) Then
                Await Me.LoadControls()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un número de Placa"
            End If
            'If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            '    Exit Sub
            'End If
            'If Me._sequence.IsManual Then
            '    If Not String.IsNullOrEmpty(CodeEquipmentReception.Trim()) Then
            '        Await Me.LoadControls()
            '    Else
            '        Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            '    End If
            'Else
            '    If String.IsNullOrEmpty(CodeEquipmentReception) Then
            '        Await Me.NewEquipmentReception()
            '    Else
            '        Await Me.LoadControls()
            '    End If
            'End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDtxtConsecutivo_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub INDCbeAcquisition_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleAcquisition.EditValueChanged
        If INDgleAcquisition.EditValue = "6" Then
            INDlyItemOther.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDtxtOther.Focus()
        Else
            INDlyItemOther.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        If INDgleAcquisition.EditValue = "3" Then
            LayoutControlItem36.Text = "Nro Documento"
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub INDcbType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePartsAccesoriesConsumables.EditValueChanged, INDgleType.EditValueChanged
        If Object.Equals(INDSlePartsAccesoriesConsumables.EditValue, Nothing) = False Then
            LoadDetailEquipment()
        End If
    End Sub

    Private Sub INDcbLocationType_KeyDown(sender As Object, e As KeyEventArgs) Handles INDglEquipmentHistory.KeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            INDglTechnicalLog.Focus()
        End If
    End Sub
    Private Sub INDbtnAgregarRegistroTecnicio_Click(sender As Object, e As EventArgs) Handles INDbtnAgregarRegistroTecnicio.Click
        'AddTechnicalLog()
    End Sub

    Private Sub INDbtnAgregarDetalle_Click(sender As Object, e As EventArgs) Handles INDbtnAgregarDetalle.Click
        AddDetail()
    End Sub
    Private Sub INDgcDetail_EmbeddedNavigator_ButtonClick(sender As Object, e As NavigatorButtonClickEventArgs) Handles INDgcDetail.EmbeddedNavigator.ButtonClick
        If e.Button.ButtonType = NavigatorButtonType.Remove Then
            DeleteItemDetail()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAgregarRegistroInvima_Click(sender As Object, e As EventArgs) Handles INDbtnAddAccesory.Click
        'Si se esta guardando
        'If FlagInvimaRegistration = False Then
        '    Dim errors As New Text.StringBuilder
        '    If INDTxtInvimaDate.EditValue Is Nothing Then
        '        errors.AppendLine("Fecha Vacia")
        '    End If
        '    If INDTxtInvimaNumberRegister.EditValue Is Nothing Then
        '        errors.AppendLine("Numero de Registro Vacio")
        '    End If
        '    If INDTxtExpirationDate.EditValue Is Nothing Then
        '        errors.AppendLine("Fecha de Vencimiento Vacia")
        '    End If

        '    If errors.Length > 0 Then
        '        Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
        '        Exit Sub
        '    End If

        '    Dim InvimaRegistration As New EquipmentInvima
        '    With InvimaRegistration
        '        .Date = INDTxtInvimaDate.EditValue
        '        .NumberRegister = INDTxtInvimaNumberRegister.EditValue
        '        .ExpirationDate = INDTxtExpirationDate.EditValue
        '        .State = If(INDCbeStatus.SelectedIndex = 0, True, False)
        '    End With

        '    If ListInvimaRegistration Is Nothing Then
        '        ListInvimaRegistration = New List(Of EquipmentInvima)
        '    End If

        '    ListInvimaRegistration.Add(InvimaRegistration)


        'Else
        '    'With InvimaRegistration
        '    '    .Date = INDTxtInvimaDate.EditValue
        '    '    .NumberRegister = INDTxtInvimaNumberRegister.EditValue
        '    '    .ExpirationDate = INDTxtExpirationDate.EditValue
        '    '    .State = If(INDCbeStatus.SelectedIndex = 0, True, False)
        '    'End With
        'End If

        'INDgcInvima.DataSource = ListInvimaRegistration
        'INDgcInvima.RefreshDataSource()
        'CleanControlsPopup()
        If ValidateInvimaRegistration() Then
            AddInvimaRegistration()
        End If


    End Sub

    Private Function ValidateInvimaRegistration() As Boolean

        Dim errors As New Text.StringBuilder
        If INDTxtInvimaDate.EditValue Is Nothing Or INDTxtInvimaDate.Text = "" Then
            errors.AppendLine("Fecha Vacia")
        End If
        If INDTxtInvimaNumberRegister.EditValue Is Nothing Or INDTxtInvimaNumberRegister.Text = "" Then
            errors.AppendLine("Numero de Registro Vacio")
        End If
        If INDTxtExpirationDate.EditValue Is Nothing Or INDTxtExpirationDate.Text = "" Then
            errors.AppendLine("Fecha de Vencimiento Vacia")
        End If

        If errors.Length = 0 Then
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If
    End Function

    Private Async Sub INDglEquipmentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglEquipmentType.EditValueChanged
        ''If Object.Equals(INDglEquipmentType.EditValue, Nothing) = False And EquipmentReception.Id = 0 Then
        'INDgcDetail.DataSource = Nothing
        ''INDglDetalleEquipo.EditValue = Nothing
        ''ListDetail = New List(Of DetailEquipment)

        'AsyncLoader(True)
        'Using ModelEquipment As New MEquipment

        '    'Equipment = Await ModelEquipment.ListAllEquipment()
        '    INDSlEquipment.Properties.DataSource = Equipment.Where(Function(x) x.IdEquipmentType = IdEquipmentType).ToList()

        'End Using

        ''Using ModelEquipmentType As New MEquipamentType

        ''    If FlagNew = True Then

        ''        Using ModelTemplate As New MTemplateEquipmentType
        ''            Dim Template As Domain.Entities.TemplateEquipmentType = Await ModelTemplate.GetTemplateEquipmentTypeByEquipmentTypeIdAsync(INDglEquipmentType.EditValue)
        ''            With Template
        ''                If .Id > 0 Then
        ''                    INDmemoDatosGenerales.Text = .GeneralData
        ''                    INDmemoCondiciones.Text = .Terms
        ''                    INDmemoDescripcionFuncionamiento.Text = .OperationDescription
        ''                    INDmemoLimpieza.Text = .Cleaning
        ''                    INDMemoPrecauciones.Text = .HandlingPrecautions
        ''                End If
        ''            End With
        ''        End Using


        ''        ListDetail = New List(Of DetailEquipment)
        ''        Dim EquipmentType = Await ModelEquipmentType.GetEquipamentTypeByIdAsync(IdEquipmentType)
        ''        Dim ListObj = EquipmentType.EquipmentTypePartsAccesoriesConsumibles.ToList()
        ''        Dim ListPartsAccesoriesConsumables As New List(Of PartsAccesoriesConsumables)

        ''        If ListObj IsNot Nothing And ListObj.Count() > 0 Then
        ''            For i As Integer = 0 To ListObj.Count - 1
        ''                ListDetail.Add(New DetailEquipment With {.Id = ListObj.Item(i).PartsAccesoriesConsumables.Id, .Code = ListObj.Item(i).PartsAccesoriesConsumables.Code, .Name = ListObj.Item(i).PartsAccesoriesConsumables.Name, .Comment = ListObj.Item(i).PartsAccesoriesConsumables.Name, .Type = ""})
        ''                ListPartsAccesoriesConsumables.Add(ListObj.Item(i).PartsAccesoriesConsumables)
        ''            Next

        ''            INDgcDetail.DataSource = ListDetail
        ''            INDglePartsAccesoriesConsumables.Properties.DataSource = ListPartsAccesoriesConsumables
        ''        End If
        ''    Else
        ''        If ListDetail.Count = 0 Then
        ''            Dim EquipmentType = Await ModelEquipmentType.GetEquipamentTypeByIdAsync(IdEquipmentType)

        ''            If EquipmentType.EquipmentTypePartsAccesoriesConsumibles IsNot Nothing And EquipmentType.EquipmentTypePartsAccesoriesConsumibles.Count() > 0 Then

        ''                ListDetail = New List(Of DetailEquipment)

        ''                Dim ListObj = EquipmentType.EquipmentTypePartsAccesoriesConsumibles.ToList()
        ''                Dim ListPartsAccesoriesConsumables As New List(Of PartsAccesoriesConsumables)

        ''                If ListObj IsNot Nothing And ListObj.Count() > 0 Then
        ''                    For i As Integer = 0 To ListObj.Count - 1
        ''                        ListDetail.Add(New DetailEquipment With {.Id = ListObj.Item(i).PartsAccesoriesConsumables.Id, .Code = ListObj.Item(i).PartsAccesoriesConsumables.Code, .Name = ListObj.Item(i).PartsAccesoriesConsumables.Name, .Comment = ListObj.Item(i).PartsAccesoriesConsumables.Name, .Type = ""})
        ''                        ListPartsAccesoriesConsumables.Add(ListObj.Item(i).PartsAccesoriesConsumables)
        ''                    Next

        ''                    INDgcDetail.DataSource = ListDetail
        ''                    INDglePartsAccesoriesConsumables.Properties.DataSource = ListPartsAccesoriesConsumables
        ''                End If
        ''            End If
        ''        End If
        ''    End If

        ''    If ListTechnicalLog.Count = 0 Then
        ''        Dim EquipmentType = Await ModelEquipmentType.GetEquipamentTypeByIdAsync(IdEquipmentType)

        ''        If EquipmentType.EquipmentTypeTechnicalLog IsNot Nothing And EquipmentType.EquipmentTypeTechnicalLog.Count() > 0 Then

        ''            ListTechnicalLog = New List(Of Domain.Entities.TechnicalLogDetail)

        ''            Dim ListObj = EquipmentType.EquipmentTypeTechnicalLog

        ''            For i As Integer = 0 To ListObj.Count - 1
        ''                ListTechnicalLog.Add(New Domain.Entities.TechnicalLogDetail With {.IdTechnicalLog = ListObj.Item(i).TechnicalLogId, .ValueMin = 0, .ValueMax = 0, .Name = ListObj.Item(i).TechnicalLog.Name.Trim, .IdMeasurementUnit = 0, .Abbreviation = "".ToString})
        ''                'ListTechnicalLog.Add(New Domain.Entities.TechnicalLogDetail With {.IdTechnicalLog = ListObj.Item(i).TechnicalLogId, .Value = INDglTechnicalLogView.GetFocusedRowCellValue("Value"), .Name = INDglTechnicalLogView.GetFocusedRowCellValue("Name").ToString.Trim, .IdMeasurementUnit = INDglMeasureUnit.EditValue, .Abbreviation = INDglMeasureUnitView.GetFocusedRowCellValue("Abbreviation").ToString})
        ''            Next

        ''            INDgcRegistroTecnico.DataSource = ListTechnicalLog
        ''        End If
        ''    End If

        ''End Using

        'AsyncLoader(False)

        ''End If
    End Sub

    Private Sub INDglTechnicalLog_EditValueChanged(sender As Object, e As EventArgs) Handles INDglTechnicalLog.EditValueChanged
        'If Object.Equals(INDglTechnicalLog.EditValue, Nothing) = False Then
        '    Dim MeasurementUnitList As New List(Of Domain.Entities.MeasurementUnit)
        '    Dim List As List(Of Domain.Entities.TechnicalLog) = TechnicalLogDataSource.Where(Function(x) x.Id = INDglTechnicalLog.EditValue).ToList
        '    For i = 0 To List.Count - 1
        '        For j = 0 To List.Item(i).TechnicalLogMeasurementUnitDetail.Count - 1
        '            MeasurementUnitList.Add(List.Item(i).TechnicalLogMeasurementUnitDetail.Item(j).MeasurementUnit)
        '        Next
        '    Next
        '    INDglMeasureUnit.Properties.DataSource = MeasurementUnitList
        'End If
    End Sub

    Private Sub INDbtnAddFeature_Click(sender As Object, e As EventArgs) Handles INDbtnAddFeature.Click
        If AddFeatureAdditional() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        End If
    End Sub

    Private Sub RepositoryItemPopupContainerEdit1_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEdit1.QueryPopUp
        ShowInformarionFeature()
    End Sub

    'Private Sub INDTreeListLocation_AfterCheckNode(sender As Object, e As DevExpress.XtraTreeList.NodeEventArgs) Handles INDTreeListLocation.AfterCheckNode
    '    '  INDTreeListLocation.CollapseAll()
    '    IdLocation = e.Node.GetValue("Id").ToString.Trim()
    '    '  INDpcLocation.Text = e.Node.GetValue("CodeName").ToString.Trim()
    'End Sub


#Region "CloseUp"
    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceInvima_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddInventory.CloseUp
        If FlagInvimaRegistration = True Then
            CleanControlsPopup()
        End If
    End Sub
#End Region

#Region "Agregar Manuales y Planos"


    Private Sub INDckPlanos_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles INDckPlanos.ItemCheck
        If INDckPlanos.SelectedIndex >= 0 Then
            If EquipmentReception.DrawingsDetail.Where(Function(x) x.Drawings = e.Index + 1).Count = 0 Then
                EquipmentReception.DrawingsDetail.Add(New Domain.Entities.DrawingsDetail With {.Drawings = e.Index + 1})
            Else
                If Delete = False Then
                    DeleteDrawingsList.Add(EquipmentReception.DrawingsDetail.Where(Function(x) x.Drawings = e.Index + 1).SingleOrDefault)
                    EquipmentReception.DrawingsDetail.Remove(EquipmentReception.DrawingsDetail.Where(Function(x) x.Drawings = e.Index + 1).SingleOrDefault)
                End If
            End If
        End If
    End Sub
    Private Sub INDckOpcionesManuales_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles INDckManuales.ItemCheck
        If INDckManuales.SelectedIndex >= 0 Then
            If EquipmentReception.ManualDetail.Where(Function(x) x.Manual = e.Index + 1).Count = 0 Then
                EquipmentReception.ManualDetail.Add(New Domain.Entities.ManualDetail With {.Manual = e.Index + 1})
            Else
                If Delete = False Then
                    DeleteManualList.Add(EquipmentReception.ManualDetail.Where(Function(x) x.Manual = e.Index + 1).SingleOrDefault)
                    'If EquipmentReception.ManualDetail.Where(Function(x) x.Manual = e.Index + 1).Count > 0 Then
                    EquipmentReception.ManualDetail.Remove(EquipmentReception.ManualDetail.Where(Function(x) x.Manual = e.Index + 1).SingleOrDefault)
                    'End If
                End If
            End If
        End If
    End Sub



#End Region

#Region "Validaciones Fechas"

    Private Sub INDdtFechaCompra_Validating(sender As Object, e As CancelEventArgs) Handles INDdtFechaAdquisicion.Validating, INDdtFechaInstalacion.Validating, INDdtFechaInicioOperacion.Validating, INDdtFechaFabricacion.Validating
        If Object.Equals(INDdtFechaAdquisicion.EditValue, Nothing) = False Then
            Dim Obj As DateEdit = CType(sender, DateEdit)
            Dim MensajeMostrar As String = String.Empty
            Select Case Obj.Name
                Case Is = "INDdtFechaAdquisicion"
                    MensajeMostrar = "La fecha de adquisicion del equipo no puede ser mayor a la actual"
                Case Is = "INDdtFechaInstalacion"
                    MensajeMostrar = "La fecha de instalacion del equipo no puede ser mayor a la actual"
                Case Is = "INDdtFechaInicioOperacion"
                    MensajeMostrar = "La fecha de inicio de operacion del equipo no puede ser mayor a la actual"
                Case Is = "INDdtFechaFabricacion"
                    MensajeMostrar = "La fecha fabricacion del equipo no puede ser mayor a la actual"
            End Select
            GetServerDate()
            If Date.Compare(Obj.EditValue, ServerDate) > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = MensajeMostrar
                Obj.Focus()
                Exit Sub
            End If

            'Select Case Obj.Name
            '    Case Is = "INDdtFechaInstalacion"
            '        MensajeMostrar = "La fecha de instalacion no puede ser mayor a la fecha de adquisicion"
            '    Case Is = "INDdtFechaInicioOperacion"
            '        MensajeMostrar = "La fecha de inicio de operacion no puede ser mayor a la fecha de adquisicion"
            '    Case Is = "INDdtFechaFabricacion"
            '        MensajeMostrar = "La fecha de inicio de operacion no puede ser mayor a la fecha de adquisicion"
            'End Select
            'If Obj.Name <> INDdtFechaInstalacion.Name Then
            '    If Date.Compare(Obj.EditValue, INDdtFechaAdquisicion.EditValue) < 0 Then
            '        Mensaje(EeventViewerImages.Advertencia) = MensajeMostrar
            '        Obj.Focus()
            '        Exit Sub
            '    End If
            'End If

            'Select Case Obj.Name
            '    Case Is = "INDdtFechaInstalacion"
            '        MensajeMostrar = "La fecha de instalacion no puede ser mayor a la fecha de inicio operacion"
            '    Case Is = "INDdtFechaAdquisicion"
            '        MensajeMostrar = "La fecha de adquisicion no puede ser mayor a la fecha de inicio de operacion"
            '    Case Is = "INDdtFechaFabricacion"
            '        MensajeMostrar = "La fecha fabricacion no puede ser mayor a la fecha de inicio de operacion"
            'End Select
            'If Obj.Name <> INDdtFechaInicioOperacion.Name Then
            '    If Date.Compare(Obj.EditValue, INDdtFechaInicioOperacion.EditValue) < 0 Then
            '        Mensaje(EeventViewerImages.Advertencia) = MensajeMostrar
            '        Obj.Focus()
            '        Exit Sub
            '    End If
            'End If

            'Select Case Obj.Name
            '    Case Is = "INDdtFechaInstalacion"
            '        MensajeMostrar = "La fecha de instalacion no puede ser mayor a la fecha de fabricacion"
            '    Case Is = "INDdtFechaAdquisicion"
            '        MensajeMostrar = "La fecha de adquisicion no puede ser mayor a la fecha de inicio de operacion"
            '    Case Is = "INDdtFechaFabricacion"
            '        MensajeMostrar = "La fecha fabricacion no puede ser mayor a la fecha de inicio de operacion"
            'End Select
            'If Obj.Name <> INDdtFechaFabricacion.Name Then
            '    If Date.Compare(Obj.EditValue, INDdtFechaFabricacion.EditValue) < 0 Then
            '        Mensaje(EeventViewerImages.Advertencia) = MensajeMostrar
            '        Obj.Focus()
            '        Exit Sub
            '    End If
            'End If
        End If
    End Sub

#End Region

#Region "Menus Rejillas"

    Private Sub INDgcRegistroTecnicoView_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgcRegistroTecnicoView.PopupMenuShowing
        If e.HitInfo.InRow <> True Or e.Menu Is Nothing Then
            Exit Sub
        End If
        If INDgcDetailView.FocusedRowHandle < 0 Then
            Exit Sub
        End If
        If e.MenuType = DevExpress.XtraGrid.Views.Grid.GridMenuType.Row Then
            e.Menu.Items.Clear()

        End If
    End Sub

    Private Async Sub INDglSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDglManufacter.EditValueChanged
        If Object.Equals(INDglManufacter.EditValue, Nothing) = False Then
            Dim Info As String = String.Empty
            Using Model As New MEquipmentReception
                Dim Country As Domain.Entities.City = Await Model.GetCityCountry(CType(INDglManufacter, SearchLookUpEdit).Properties.View.GetFocusedRowCellValue("IdCity"))
                If Object.Equals(Country, Nothing) = False Then
                    Info = Country.Department.Country.Name
                End If
            End Using
            'Using ModelCity As New MCity(Me.Tag)
            '    Dim City = ModelCity.GetCityById(INDglManufacterView.GetFocusedRowCellValue("IdCity"))
            '    If Object.Equals(City, Nothing) = False Then
            '        Info = Info & " - "
            '    End If
            'End Using
            'INDtxtInfoFabricante.Text = Info
        Else
            'INDtxtInfoFabricante.Text = String.Empty
        End If
    End Sub


#End Region

#Region "ShowingEditor"

    Private Sub INDGvMantenimientos_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGvMantenimientos.ShowingEditor
        INDGvMantenimientos_PceNotification.PopupControl = Nothing
        GridView11_PceNotification.PopupControl = Nothing
        GridView12_PceNotification.PopupControl = Nothing
        INDGcWorkOrderNotification.DataSource = Nothing

        Dim view = CType(INDGvMantenimientos.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcWorkOrderNotification.DataSource = Me.Presenter.GetNotificationsBySource("MaintenancePlanProgramated", view.MaintenancePlanProgramatedId)
        INDGvMantenimientos_PceNotification.PopupControl = INDPccWorkOrderNotification
    End Sub

    Private Sub GridView11_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridView11.ShowingEditor
        INDGvMantenimientos_PceNotification.PopupControl = Nothing
        GridView11_PceNotification.PopupControl = Nothing
        GridView12_PceNotification.PopupControl = Nothing
        INDGcWorkOrderNotification.DataSource = Nothing

        Dim view = CType(GridView11.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcWorkOrderNotification.DataSource = Me.Presenter.GetNotificationsBySource("MaintenanceFailureRequest", view.FailureRequestDetailId)
        GridView11_PceNotification.PopupControl = INDPccWorkOrderNotification
    End Sub

    Private Sub GridView12_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridView12.ShowingEditor
        INDGvMantenimientos_PceNotification.PopupControl = Nothing
        GridView11_PceNotification.PopupControl = Nothing
        GridView12_PceNotification.PopupControl = Nothing
        INDGcWorkOrderNotification.DataSource = Nothing

        Dim view = CType(GridView12.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        INDGcWorkOrderNotification.DataSource = Me.Presenter.GetNotificationsByWorkOrderId(view.Id)
        GridView12_PceNotification.PopupControl = INDPccWorkOrderNotification
    End Sub

#End Region

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub
    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
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
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Save()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
        NewEquipmentReception()
        INDgleTypeInventory.Focus()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Save()
    End Sub

#End Region

    Private Sub INDRepositoryItemSpinEditValor_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepositoryItemSpinEditValorMin.EditValueChanging, INDRepositoryItemSpinEditValor.EditValueChanging
        Dim TechnicalLogID = INDgcRegistroTecnicoView.GetFocusedRowCellValue(INDColTechnicalLogId)
        Dim Value = e.NewValue
        'Dim obj = INDgcRegistroTecnicoView.GetRow(1)
        If EquipmentReception.TechnicalLogDetail.Where(Function(x) x.IdTechnicalLog = TechnicalLogID).Count > 0 Then
            Dim TechnicalLogDetailModified = EquipmentReception.TechnicalLogDetail.Where(Function(x) x.IdTechnicalLog = TechnicalLogID).FirstOrDefault()

            TechnicalLogDetailModified.ValueMin = Value

            EquipmentReception.TechnicalLogDetail.Remove(EquipmentReception.TechnicalLogDetail.Where(Function(x) x.IdTechnicalLog = TechnicalLogID).FirstOrDefault())
            EquipmentReception.TechnicalLogDetail.Add(TechnicalLogDetailModified)
        End If
    End Sub

    Private Sub INDRepositoryItemSpinEditValorMax_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepositoryItemSpinEditValorMax.EditValueChanging
        Dim TechnicalLogID = INDgcRegistroTecnicoView.GetFocusedRowCellValue(INDColTechnicalLogId)
        Dim Value = e.NewValue
        'Dim obj = INDgcRegistroTecnicoView.GetRow(1)
        If EquipmentReception.TechnicalLogDetail.Where(Function(x) x.IdTechnicalLog = TechnicalLogID).Count > 0 Then
            Dim TechnicalLogDetailModified = EquipmentReception.TechnicalLogDetail.Where(Function(x) x.IdTechnicalLog = TechnicalLogID).FirstOrDefault()

            TechnicalLogDetailModified.ValueMax = Value

            EquipmentReception.TechnicalLogDetail.Remove(EquipmentReception.TechnicalLogDetail.Where(Function(x) x.IdTechnicalLog = TechnicalLogID).FirstOrDefault())
            EquipmentReception.TechnicalLogDetail.Add(TechnicalLogDetailModified)
        End If
    End Sub

    Private Sub INDdtFechaFabricacion_EditValueChanged(sender As Object, e As EventArgs) Handles INDdtFechaFabricacion.EditValueChanged
        INDdtFechaAdquisicion.Properties.MinValue = INDdtFechaFabricacion.EditValue
        INDdtFechaInstalacion.Properties.MinValue = INDdtFechaFabricacion.EditValue
        INDdtFechaInicioOperacion.Properties.MinValue = INDdtFechaFabricacion.EditValue
        INDdtFechaVencimiento.Properties.MinValue = INDdtFechaFabricacion.EditValue

    End Sub

    Private Sub INDdtFechaAdquisicion_EditValueChanged(sender As Object, e As EventArgs) Handles INDdtFechaAdquisicion.EditValueChanged
        INDdtFechaInstalacion.Properties.MinValue = INDdtFechaAdquisicion.EditValue
        INDdtFechaInicioOperacion.Properties.MinValue = INDdtFechaAdquisicion.EditValue
    End Sub

    Private Sub RepItemButtonDelete_Click(sender As Object, e As EventArgs) Handles RepItemButtonDelete.Click
        DeleteItemDetail()
    End Sub

    Private Sub RepItemButtonDelete_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles RepItemButtonDelete.CustomDisplayText
        e.DisplayText = "Eliminar"
    End Sub
    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaData, Eform.InfoMetaData), Me.EquipmentReception.Plate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.EquipmentReception.Plate & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaDataTitle, Eform.InfoMetaData), Me.EquipmentReception.Plate),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaData, Eform.InfoMetaData), Me.EquipmentReception.Plate)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEmployeeMetaDataTitle, Eform.InfoMetaData), Me.EquipmentReception.Plate)
            Return Me._doc
        End If
    End Function

    Private Sub INDglManufacter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglManufacter.QueryPopUp
        If INDglManufacter.Properties.DataSource Is Nothing Then
            INDglManufacter.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.ListAllSupplierManufacturer()
        End If
    End Sub

    Private Sub INDglVendedor_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglVendedor.QueryPopUp
        If INDglVendedor.Properties.DataSource Is Nothing Then
            INDglVendedor.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.ListAllSupplierSeller()
        End If
    End Sub

    Private Sub INDglEquipmentFunction_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglEquipmentFunction.QueryPopUp
        If INDglEquipmentFunction.Properties.DataSource Is Nothing Then
            Using ModelEquipmentReception As New MEquipmentReception
                INDglEquipmentFunction.Properties.DataSource = ModelEquipmentReception.ListAllEquipmentFunction()
            End Using
        End If
    End Sub

    Private Sub INDglPhysicalRisk_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglPhysicalRisk.QueryPopUp
        If INDglPhysicalRisk.Properties.DataSource Is Nothing Then
            Using ModelEquipmentReception As New MEquipmentReception
                INDglPhysicalRisk.Properties.DataSource = ModelEquipmentReception.ListAllPhysicalRisk()
            End Using
        End If
    End Sub

    Private Sub INDglEquipmentRequirement_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglEquipmentRequirement.QueryPopUp
        If INDglEquipmentRequirement.Properties.DataSource Is Nothing Then
            Using ModelEquipmentReception As New MEquipmentReception
                INDglEquipmentRequirement.Properties.DataSource = ModelEquipmentReception.ListAllEquipmentRequirement()
            End Using
        End If
    End Sub

    Private Sub INDglEquipmentHistory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDglEquipmentHistory.QueryPopUp
        If INDglEquipmentHistory.Properties.DataSource Is Nothing Then
            Using ModelEquipmentReception As New MEquipmentReception
                INDglEquipmentHistory.Properties.DataSource = ModelEquipmentReception.ListAllEquipmentHistory()
            End Using
        End If
    End Sub

    Private Sub INDSlePartsAccesoriesConsumables_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePartsAccesoriesConsumables.QueryPopUp
        If INDSlePartsAccesoriesConsumables.Properties.DataSource Is Nothing Then
            Using ModelEquipmentReception As New MEquipmentReception
                INDSlePartsAccesoriesConsumables.Properties.DataSource = ModelEquipmentReception.ListViewPartAccesoryConsumables()
            End Using
        End If
    End Sub

    Private Sub INDPopupFichaTecnica_EditValueChanged(sender As Object, e As EventArgs) Handles INDPopupFichaTecnica.EditValueChanged

    End Sub
End Class


