#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class MaintenanceServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Fields"

#End Region

#Region "Builders"

    ' ''' <summary>
    ' ''' Incializa una nueva instancia de la clase
    ' ''' </summary>
    ' ''' <param name="Company">Empresa que se debe consultar</param>
    ' ''' <param name="endpoint">Punto de configuración del servicio</param>
    ' ''' <param name="remoteaddress">Dirección remota del servicio</param>
    'Public Sub New(Company As String, ByVal endpoint As String, ByVal remoteaddress As String)
    '    XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStoreEx(endpoint, remoteaddress, Company))
    'End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

    ''' <summary>
    ''' Consulta el listado de niveles de cargos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBranch() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Branch))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEquipmentType() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAsset_FixedAssetItemType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
        'Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_EquipmentType))
        'Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;EquipmentType", Nothing)
        'Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLocation() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Location))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;IdLocationType", Nothing)
        Return serverMode
    End Function

    Public Function ListMaintenanceAnulateReason() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_MaintenanceAnulateReason)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_MaintenanceAnulateReason))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListMaintenanceProtocol() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_MaintenanceProtocol)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_MaintenanceProtocol))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListViewMaintenanceProgramming(Optional filter As String = "") As XPInstantFeedbackSource
        'Dim session As New IndigoXPOSession(Of Maintenance_ViewMaintenanceProgramming)()
        'Dim classEntity = session.GetClassInfo(GetType(Maintenance_ViewMaintenanceProgramming))
        'Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        'Return serverMode
        Dim session As New IndigoXPOSession(Of Maintenance_ViewMaintenanceProgramming)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_ViewMaintenanceProgramming))

        Dim allFilter As String = "ProtocolCode Is Null Or (ProtocolCode Is Not Null And ProximoMantenimiento Is Not Null)"
        If filter IsNot Nothing AndAlso Not filter.Equals("") Then
            allFilter = filter ' & " And ProximoMantenimiento Is Not Null"
        End If
        Dim criteria = CriteriaOperator.Parse(allFilter)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = "Plate"
        Return serverMode
    End Function

    Public Function ListViewMaintenanceWithOutProgrammingCollection(filter As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_ViewMaintenanceWithOutProgramming)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_ViewMaintenanceWithOutProgramming))
        Dim criteria = CriteriaOperator.Parse(filter)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListViewMaintenanceProgrammingCollection(filter As String) As XPCollection(Of Maintenance_ViewMaintenanceProgramming)
        Dim session As New IndigoXPOSession(Of Maintenance_ViewMaintenanceProgramming)

        Dim allFilter As String = "ProtocolCode Is Null Or (ProtocolCode Is Not Null And ProximoMantenimiento Is Not Null)"
        If filter IsNot Nothing AndAlso Not filter.Equals("") Then
            allFilter = filter ' & " And ProximoMantenimiento Is Not Null"
        End If

        Dim criteria = CriteriaOperator.Parse(allFilter)
        Dim sortCollection As New SortingCollection()
        sortCollection.Add(New SortProperty("Plate", SortingDirection.Ascending))

        Dim collection As New XPCollection(Of Maintenance_ViewMaintenanceProgramming)(session, criteria)
        collection.Sorting = sortCollection
        Return collection
    End Function

    Public Function ListViewMaintenanceProgrammingCollectionOnlyWithOutProgramming(filter As String) As XPCollection(Of Maintenance_ViewMaintenanceProgramming)
        Dim session As New IndigoXPOSession(Of Maintenance_ViewMaintenanceProgramming)

        Dim allFilter As String = "ProtocolCode Is Null And ProximoMantenimiento Is Null"
        If filter IsNot Nothing AndAlso Not filter.Equals("") Then
            allFilter = filter ' & " And ProximoMantenimiento Is Not Null"
        End If

        Dim criteria = CriteriaOperator.Parse(allFilter)
        Dim sortCollection As New SortingCollection()
        sortCollection.Add(New SortProperty("Plate", SortingDirection.Ascending))

        Dim collection As New XPCollection(Of Maintenance_ViewMaintenanceProgramming)(session, criteria)
        collection.Sorting = sortCollection
        Return collection
    End Function

    Public Function ListViewMaintenanceProgrammingCollectionOnlyProgramed(filter As String) As XPCollection(Of Maintenance_ViewMaintenanceProgramming)
        Dim session As New IndigoXPOSession(Of Maintenance_ViewMaintenanceProgramming)

        Dim allFilter As String = "ProtocolCode Is Not Null And ProximoMantenimiento Is Not Null"
        If filter IsNot Nothing AndAlso Not filter.Equals("") Then
            allFilter = filter ' & " And ProximoMantenimiento Is Not Null"
        End If

        Dim criteria = CriteriaOperator.Parse(allFilter)
        Dim sortCollection As New SortingCollection()
        sortCollection.Add(New SortProperty("Plate", SortingDirection.Ascending))

        Dim collection As New XPCollection(Of Maintenance_ViewMaintenanceProgramming)(session, criteria)
        collection.Sorting = sortCollection
        Return collection
    End Function

    Public Function ListViewWorkOrderScheduledMaintenance(filter As String, Optional sort As String = "Plate") As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewWorkOrderScheduledMaintenanceXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewWorkOrderScheduledMaintenanceXpo))
        Dim criteria = CriteriaOperator.Parse(filter)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = sort

        Return serverMode
    End Function

    Public Function ListViewWorkOrderUnScheduledMaintenance(filter As String, Optional sort As String = "Plate") As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewWorkOrderUnScheduledMaintenanceXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewWorkOrderUnScheduledMaintenanceXpo))
        Dim criteria = CriteriaOperator.Parse(filter)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = sort

        Return serverMode
    End Function

    Public Function ListWorkOrders(filter As String, Optional sort As String = "Plate") As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_WorkOrder)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_WorkOrder))
        Dim criteria = CriteriaOperator.Parse(filter)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = sort

        Return serverMode
    End Function
    Public Function WorkOrders() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_WorkOrder)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_WorkOrder))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListViewPartAccesoryConsumables() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Maintenance_ViewPartAccesoryConsumables)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_ViewPartAccesoryConsumables))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function GetProtocolById(Id As Integer) As Maintenance_MaintenanceProtocol
        Dim filtroConsulta As String = "Id = " & Id
        Return Me.GetCollection(Of Maintenance_MaintenanceProtocol)(Nothing, filtroConsulta).FirstOrDefault
    End Function

    Public Function ListMaintenanceProtocolByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"State={status}")
        Dim session As New IndigoXPOSession(Of Maintenance_MaintenanceProtocol)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_MaintenanceProtocol))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListMaintenanceProtocolByStatusAndFixedAssetItemId(fixedAssetItemId As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"FixedAssetItemId.Id = {fixedAssetItemId} AND State={status}")
        Dim session As New IndigoXPOSession(Of Maintenance_MaintenanceProtocol)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_MaintenanceProtocol))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListTechnicalLogByState(state As Boolean) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"State={state}")
        Dim session As New IndigoXPOSession(Of Maintenance_TechnicalLog)()
        Dim classEntity = session.GetClassInfo(GetType(Maintenance_TechnicalLog))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de aseguradoras
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInsurance() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Insurance))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Nit;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de inventario
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInventoryType() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_InventoryType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSupplier(Optional SupplierId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = Nothing
        If SupplierId IsNot Nothing Then
            criteria = CriteriaOperator.Parse("Id=" & SupplierId)
        End If

        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Supplier))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Descripcion;CodeName;IdThirdParty.Name;IdThirdParty.Nit", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de proveedores
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Supplier))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Descripcion;CodeName;IdThirdParty.Name;IdThirdParty.Nit;Status;IdThirdParty.NitName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierType() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_SupplierType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetPartsAccesoriesConsumiblesByEquipmentType(equipmentType As Integer) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"FixedAsset_FixedAssetItemTypePartsAccesoriess[EquipmentTypeId.Id={equipmentType}]")
        Dim session As New IndigoXPOSession(Of FixedAsset_FixedAssetPartsAccesoriesConsumables)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAsset_FixedAssetPartsAccesoriesConsumables))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierTypeByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And ParentId is null")
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " and ParentId is not null And Maintenance_SupplierTypeCollection is null")
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_SupplierType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;ParentId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllSupplierTypeByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_SupplierType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status;ParentId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores por estado para el treelist
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierTypeData() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(Maintenance_SupplierType))
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores por estado para el treelist
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierTypeByStatusTreeList(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(Maintenance_SupplierType), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de proveedores por estado para el treelist
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierTypeByStatusTreeList2(supplierId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SupplierId=" & supplierId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(Maintenance_SupplierDetailType), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de Proveedores de Mantenimiento
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSupplierMaintenance() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_SupplierMaintenance))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    Public Function ListEquipmentFunction() As XPInstantFeedbackSource
        Dim sessionNew = New IndigoXPOSession(Of Maintenance_EquipmentFunction)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_EquipmentFunction))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListEquipmentHistory() As XPInstantFeedbackSource
        Dim sessionNew = New IndigoXPOSession(Of Maintenance_EquipmentHistory)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_EquipmentHistory))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListAllPhysicalRisk() As XPInstantFeedbackSource
        Dim sessionNew = New IndigoXPOSession(Of Maintenance_PhysicalRisk)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_PhysicalRisk))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListAllEquipmentRequirement() As XPInstantFeedbackSource
        Dim sessionNew = New IndigoXPOSession(Of Maintenance_EquipmentRequirement)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_EquipmentRequirement))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de polizas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPoliza() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Poliza))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de polizas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPolizaType() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_PolizaType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de accesorios
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAccesory() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Accessory))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    Public Function ListAccesoryByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"State={If(status, 1, 0)}")
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Accessory))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListAccesoriesByEquipmentType(equipmentType As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"State={status} And Maintenance_AccesoryDetails[IdEquipmentType.Id={equipmentType}]")
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Accessory))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de consumibles
    ''' </summary>
    ''' <returns></returns>
    Public Function GetConsumible() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Consumable))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    Public Function ListConsumibleByStatus(state As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"State={If(state, 1, 0)}")
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Consumable))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListAllConsumableByEquipmentType(equipmentType As Integer, state As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"State={state} And Maintenance_ConsumableDetails[IdEquipmentType.Id={equipmentType}]")
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Consumable))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de torres
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTower() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Tower))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de responsables
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaintenanceResponsible() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MaintenanceResponsibleXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MaintenanceResponsibleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId.Nit;ThirdPartyId.Name;CodeNitName;ResponsibleRoleName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de catalogos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaintenanceTools() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MaintenanceToolsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MaintenanceToolsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;AssociateAsset", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de fabricantes
    ''' </summary>
    ''' <returns></returns> 
    Public Function ListMaintenanceManufacturers() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MaintenanceManufacturersXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MaintenanceManufacturersXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de torres
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFloor() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Floor))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de area
    ''' </summary>
    ''' <returns></returns>
    Public Function GetArea() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Area))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de area
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRoom() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Room))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de equipo
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEquipment() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Equipment))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de parte
    ''' </summary>
    ''' <returns></returns>
    Public Function GetPart() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Part))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de recepcion de equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetEquipmentReception() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_EquipmentRegistration))
        'Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_EquipmentRegistration))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;Serial;LicensePlate;IdTrademark.Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las unidaded de medida
    ''' </summary>
    ''' <returns></returns>
    Public Function GetMeasurementUnit() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_MeasurementUnit))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las Registro tecnico
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTechnicalLog() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_TechnicalLog))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las Registro tecnico
    ''' </summary>
    ''' <returns></returns>
    Public Function GetResponsible() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Responsible))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las Registro tecnico
    ''' </summary>
    ''' <returns></returns>
    Public Function GetRegisterEquipment() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_EquipmentRegistration))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "LicensePlate;EquipmentName;Serial;InventoryNumber", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las Registro de marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBrand() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Brand))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta las Registro de marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTemplateEquipmentType() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_TemplateEquipmentType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetTrademark() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_Trademark))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    Public Function GetCostCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim classEntity = sessionNew.GetClassInfo(GetType(Maintenance_CostCenter))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListPartsAccesoriesConsumables() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_PartsAccesoriesConsumables))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListMaintenancePlan() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(Maintenance_MaintenancePlan))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllEquipment() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAsset_FixedAssetItemType))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de fallas a reportar
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaintenanceFailureRequest() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MaintenanceFailureRequestXpo)()
        Dim classEntity = session.GetClassInfo(GetType(MaintenanceFailureRequestXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DateFailure;Observation;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los contratos de mantenimiento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaintenanceContract() As XPInstantFeedbackSource
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(MaintenanceContractXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ContractTypeId.CodeName;DocumentDate;ContractNumber;InitialDate;EndDate;SupplierId.CodeName;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los catalogos de mantenimiento asociados al responsable
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaintenanceResponsibleItemCatalogByResponsible(responsibleId As Integer) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ResponsibleId = {responsibleId}")
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(ViewMaintenanceResponsibleItemCatalogXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de los catalogos de mantenimiento asociados al responsable
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMaintenanceResponsible(responsibleRole As Byte, itemCatalogFilters As String) As XPInstantFeedbackSource
        Dim filter As String = String.Format("ItemCatalogIds LIKE '%,{0},%'", itemCatalogFilters)
        If responsibleRole = 3 Then
            filter &= String.Format(" AND ResponsibleRole IN ({0})", "3,4,5")
        ElseIf responsibleRole = 2 Then
            filter &= String.Format(" AND ResponsibleRole IN ({0})", "2,3,4,5")
        ElseIf responsibleRole = 1 Then
            filter &= String.Format(" AND ResponsibleRole IN ({0})", "1,2,3,4,5")
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim classEntity = XpoDefault.Session.GetClassInfo(GetType(ViewMaintenanceResponsibleItemCatalogsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Function ListMaintenanceResponsibleByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MaintenanceResponsibleXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status={Status}")
        Dim classEntity = session.GetClassInfo(GetType(MaintenanceResponsibleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Function ListMaintenanceResponsibleByType(responsibleType As Integer, Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MaintenanceResponsibleXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ReponsibleTypeId = {responsibleType} And Status={Status}")
        Dim classEntity = session.GetClassInfo(GetType(MaintenanceResponsibleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notificaciones de la orden de trabajo
    ''' </summary>
    ''' <param name="workOrderId">Electronic Document Id.</param>
    ''' <returns></returns>
    Public Function ListWorkOrderNotificationsByWorkOrderId(workOrderId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewWorkOrderNotificationXpo)()
        Dim strCriteria = "WorkOrderId = " & workOrderId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewWorkOrderNotificationXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista todas las notificaciones de la orden de trabajo por documento origen
    ''' </summary>
    ''' <param name="entityName"></param>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Public Function ListWorkOrderNotificationsBySource(entityName As String, entityId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewWorkOrderNotificationXpo)()
        Dim strCriteria = "EntityName = '" & entityName & "' AND EntityId = " & entityId
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewWorkOrderNotificationXpo)), Nothing, criteria)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
