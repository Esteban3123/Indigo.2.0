'***********************************************************************
' Assembly         : Presentation.CloudAgent.eServicios
' Author           : WalterSierra
' Created          : 25-03-2011
'
' Last Modified By : WalterSierra
' Last Modified On : 25-03-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Enumeracion necesaria para definir el servicio utilizado.
''' </summary>
Public Enum eServicios As Integer
    ''' <summary>
    ''' Servicio Comunes.
    ''' </summary>
    Common = 0
    ''' <summary>
    ''' Servicio Seguridad
    ''' </summary>
    Security = 1
    ''' <summary>
    ''' Servicio Contratos
    ''' </summary>
    Contratos = 2
    ''' <summary>
    ''' Servicio Agendamiento
    ''' </summary>
    Agendamiento = 3
    ''' <summary>
    ''' Servicio Admisiones
    ''' </summary>
    Admisiones = 4
    ''' <summary>
    ''' Servicio Glosas
    ''' </summary>
    Glosas = 5
    ''' <summary>
    ''' Servicio Payroll
    ''' </summary>
    Payroll = 6
    ''' <summary>
    ''' Servicio CommonERP
    ''' </summary>
    CommonERP = 7
    ''' <summary>
    ''' Servicio de notificación de coordinación y evaluación
    ''' </summary>
    NotificationEvaluationService = 8
    ''' <summary>
    ''' Servicio de mantenimiento
    ''' </summary>
    Maintenance = 9
    ''' <summary>
    ''' Servicio del Sistema Documental
    ''' </summary>
    DocumentalSystem = 10
    ''' <summary>
    ''' Servicio de notificación de administración y mensajeria
    ''' </summary>
    NotificationManagementService = 11
    ''' <summary>
    ''' Servicio de indexación
    ''' </summary>
    Indexing = 12
    ''' <summary>
    ''' Servicio de contabilidad
    ''' </summary>
    Accounting = 13
    ''' <summary>
    ''' The treasury
    ''' </summary>
    Treasury = 14

    ''' <summary>
    ''' activos fijos
    ''' </summary>
    ''' <remarks></remarks>
    FixedAssets = 15
    ''' <summary>
    ''' Cartera
    ''' </summary>
    ''' <remarks></remarks>
    Portfolio = 16
    ''' <summary>
    ''' Pagos
    ''' </summary>
    ''' <remarks></remarks>
    Payments = 17
    ''' <summary>
    ''' Presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Budget = 18
    ''' <summary>
    ''' Inventario
    ''' </summary>
    ''' <remarks></remarks>
    Inventory = 19
    ''' <summary>
    ''' Contratos
    ''' </summary>
    ''' <remarks></remarks>
    Contract = 20
    ''' <summary>
    ''' Contratos
    ''' </summary>
    ''' <remarks></remarks>
    Billing = 21
    ''' <summary>
    ''' Costos
    ''' </summary>
    InteropCost = 22
    ''' <summary>
    ''' Honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    MedicalFees = 23

    ''' <summary>
    ''' indigo Crystal
    ''' </summary>
    ''' <remarks></remarks>
    Crystal = 24
    ''' <summary>
    ''' Costos
    ''' </summary>
    Cost = 25
    ''' <summary>
    ''' Servicio de Administración de archivos
    ''' </summary>
    FileManager

    Taxes = 27

    ''' <summary>
    ''' Indigo MixingStation
    ''' </summary>
    ''' <remarks></remarks>
    MixingStation = 28
    ''' <summary>
    ''' Servicio del esquema de autorizaciones
    ''' </summary>
    ''' <remarks></remarks>
    Authorization = 29

    ''' <summary>
    ''' indigo Security Default
    ''' </summary>
    ''' <remarks></remarks>
    SecurityDefault = 30

    ''' <summary>
    ''' Servicio del esquema de documentos electronicos
    ''' </summary>
    ''' <remarks></remarks>
    ElectronicDocuments = 31

    ''' <summary>
    ''' Servicio del esquema de documentos electronicos
    ''' </summary>
    ''' <remarks></remarks>
    Admissions = 32
    AccountManagement = 33
End Enum