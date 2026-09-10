'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 03-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IEquipmentReception
    Inherits IcrudBase

#Region "properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de la Recepción de Equipos
    ''' </summary>
    Property CodeEquipmentReception As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre del equipo
    ''' </summary>
    Property NameEquipment As String
    ''' <summary>
    ''' Esta propiedad contiene id del tipo de inventario
    ''' </summary>
    Property IdTrademark As Integer
    ''' <summary>
    ''' Esta propiedad contiene id del tipo de equipo
    ''' </summary>
    Property IdEquipmentType As Integer
    ''' <summary>
    ''' Esta propiedad contiene id de la ubicacion del equipo
    ''' </summary>
    Property IdLocation As String
    ''' <summary>
    ''' Esta propiedad contiene el id del responsable 
    ''' </summary>
    Property IdResponsible As Integer
    ''' <summary>
    ''' Esta propiedad contiene la adquisicion
    ''' </summary>
    Property Acquisition As String
    ''' <summary>
    ''' Esta propiedad contiene otra adquisicion
    ''' </summary>
    Property OtherAcquisition As String
    ''' <summary>
    ''' Propiedad que carga todos los equipo
    ''' </summary>
    WriteOnly Property EquipmenteTypeDataSource As List(Of Object) 'EquipmentType)
    ''' <summary>
    ''' Establece la propiedad de tipos de inventarios
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property TypeInventoryDataSource As List(Of Object)
    ''' <summary>
    ''' Propiedad que carga todos las sucursales y sedes
    ''' </summary>
    WriteOnly Property LocationDataSource As List(Of Object)
    ''' <summary>
    ''' Propiedad que carga todos los responsablexs
    ''' </summary>
    WriteOnly Property SupplierDataSource As List(Of Object)
    ''' <summary>
    ''' Propiedad que carga todos los registros tecnicos
    ''' </summary>
    Property TechnicalLogDataSource As List(Of TechnicalLog)
    ''' <summary>
    ''' Propiedad que carga todos los registros tecnicos
    ''' </summary>
    Property MeasureUnitDataSource As List(Of MeasurementUnit)
    ''' <summary>
    ''' Propiedad que carga todos los responsables
    ''' </summary>
    WriteOnly Property ResponsibleDataSource As List(Of Responsible)
    ''' <summary>
    ''' Propiedad que carga todos las polizas
    ''' </summary>
    WriteOnly Property PolizaDataSource As List(Of Object)
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Esta propiedad contiene el estado del registro de equipo
    ''' </summary>
    Property StateEquipmentReceptions As Boolean
    ''' <summary>
    ''' Propiedad que cargar las funciones del equipo
    ''' </summary>
    WriteOnly Property EquipmentFunctionDataSource As List(Of EquipmentFunction)
    ''' <summary>
    ''' Propiedad que carga todos los antecedentes del equipo
    ''' </summary>
    WriteOnly Property EquipmentHistoryDataSource As List(Of EquipmentHistory)
    ''' <summary>
    ''' Propiedad que carga todos los requerimientos del equipo
    ''' </summary>
    WriteOnly Property EquipmentRequirementDataSource As List(Of EquipmentRequirement)
    ''' <summary>
    ''' Propiedad que carga todos los riesgos fisicos
    ''' </summary>
    WriteOnly Property PhysicalRiskDataSource As List(Of PhysicalRisk)
    ''' <summary>
    ''' Propiedad que carga todas las marcas
    ''' </summary>
    WriteOnly Property TrademarkDataSource As List(Of Object)

    ''' <summary>
    ''' Propiedad que carga todas las marcas
    ''' </summary>
    WriteOnly Property PartsAccesoriesConsumablesDataSource As List(Of Object)
    ''' <summary>
    ''' Propiedad que carga todos los equipos
    ''' </summary>
    ''' <remarks></remarks>
    WriteOnly Property EquipmentDataSource As List(Of Equipment)
    ''' <summary>
    ''' Id del Equipo
    ''' </summary>
    Property IdEquipment As Integer
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MaintenanceSequence

    WriteOnly Property MaintenanceParameters As MaintenanceParameter

#End Region

End Interface
