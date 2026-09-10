'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.FixedAsset
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class FixedAssetServiceXpoEx
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
    Public Function GetCountFixedAssetDepreciationDetailByFixedAssetPhysicalAssetDetailBookId(listId As List(Of Integer)) As Integer
        Dim session As New IndigoXPOSession(Of FixedAssetDepreciationDetailXpo)()
        Dim count As Integer = session.Evaluate(GetType(FixedAssetDepreciationDetailXpo), CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("FixedAssetPhysicalAssetDetailBookId.Id in(" & String.Join(",", listId) & ")"))
        Return count
    End Function

    ''' <summary>
    ''' Obtiene las depreciaciones por mes y año
    ''' </summary>
    ''' <param name="year"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDepreciationCostByYearAndMonth(year As Integer, month As Integer) As XPCollection(Of ViewListFixedAssetDepreciationDetailCostXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetDepreciationDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ClosingYear = " & year & " And ClosingMonth = " & month)
        Dim collect As XPCollection(Of ViewListFixedAssetDepreciationDetailCostXpo) = New XPCollection(Of ViewListFixedAssetDepreciationDetailCostXpo)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Obtiene las depreciaciones por mes y año
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetViewReportHistoricalDepreciation(filtro As String) As XPCollection(Of FixedAssetViewReportHistoricalDepreciationXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetViewReportHistoricalDepreciationXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of FixedAssetViewReportHistoricalDepreciationXpo) = New XPCollection(Of FixedAssetViewReportHistoricalDepreciationXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista los activos por mes y año
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPhysicalAssetByMonthAndYear(year As Integer, month As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListFixedAssetDepreciationDetailXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ClosingYear = " & year & " And ClosingMonth = " & month)
        Dim classEntity = session.GetClassInfo(GetType(ViewListFixedAssetDepreciationDetailXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocation() As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationXpo)()

        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(session, GetType(FixedAssetFixedAssetLocationXpo))
        Return collect
        'End Using
    End Function

    Public Function ListFixedAssetLocationCollection() As XPCollection(Of FixedAssetFixedAssetLocationXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationXpo)()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As New XPCollection(Of FixedAssetFixedAssetLocationXpo)(session)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocationData() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetLocationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetEntryDevolution() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEntryDevolutionXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEntryDevolutionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetChangePlate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetChangePlateXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetChangePlateXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;DocumentDate;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los ingresos de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetEntry() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEntryXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEntryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;EntryDate;EntryNumber;AdquisitionType;AdquisitionTypeName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los ingresos de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetEntryByStatus(Status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEntryXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEntryXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;EntryDate;EntryNumber;AdquisitionType;AdquisitionTypeName;StatusName;CodeSupplier;SupplierId.Id;SupplierId.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los traslados de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetTransfer() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetTransferXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetTransferXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;DocumentDate;StatusName;TransferType;TransferTypeName;SourceResponsibleId.CodeName;TargetResponsibleId.CodeName;SourceLocationId.CodeName;TargetLocationId.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los traslados de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetRetirementTypes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetRetirementTypesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetRetirementTypesXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;RecordReplacementValue", Nothing)
    End Function

    ''' <summary>
    ''' Lista los que Registran valor de reposición
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetRetirementTypesByStatus() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetRetirementTypesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetRetirementTypesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;RecordReplacementValue", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los traslados de activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetActiveOutput() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetActiveOutputXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetActiveOutputXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;DocumentDate;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipo de ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocationType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationTypeXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetLocationTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;OrderLocation;State;StateName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipo de ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetCostCenter() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PayrollCostCenterXpo)()

        Dim classEntity = session.GetClassInfo(GetType(PayrollCostCenterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;State;StateName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipo de ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetMainAccount() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralLedgerMainAccountsXpo)()

        Dim classEntity = session.GetClassInfo(GetType(GeneralLedgerMainAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;NumberName;HandlesThirdParty;HandlesCostCenter;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocationTypeParent(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationTypeXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetLocationTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;OrderLocation,state", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTrademark() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetTrademarkXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetTrademarkXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Bienes y Servicios
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetCatalogOfPropertyandServices() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetCatalogOfPropertyandServicesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetCatalogOfPropertyandServicesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion;CodeDescription", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTrademarkByState(State As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetTrademarkXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & State & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetTrademarkXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de Poliza
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPolizaType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPolizaTypeXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPolizaTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de Poliza
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPoliza() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPolizaXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPolizaXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPolizaByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPolizaXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPolizaXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los parametros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSettingFixedAssetByOperatingUnitId(ByVal operatingUnitId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetPolizaTypeXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("OperatingUnitId = " & operatingUnitId)
        Dim collect As XPCollection = New XPCollection(session, GetType(FixedAssetSettingFixedAssetReportXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedEquipmentCatalog() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentCatalogXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentCatalogXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedEquipmentCatalogByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentCatalogXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentCatalogXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status;CodeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos por Id
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedEquipmentCatalogById(Id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentCatalogXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & Id & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentCatalogXpo))
        Dim serverMode = New XPCollection(session, classEntity, criteria)
        Return serverMode
        'End Using
    End Function

    ''' <summary>
    ''' Lista todos los saldos iniciales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetInitialBalance() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetInitialBalanceXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetInitialBalanceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;StatusName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllEquipmentType() As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentTypeXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentTypeXpo))
        Dim serverMode = New XPCollection(session, classEntity)
        Return serverMode
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetLocationByStatus() As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationXpo)()

        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetLocationXpo))
        Dim serverMode = New XPCollection(session, classEntity)
        Return serverMode
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetLocationXPInstantFeedbackSource() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetLocationXpo)()

        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetLocationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene un articulo por id
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFixedAssetItemById(Id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & Id & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentXpo))
        Dim serverMode = New XPCollection(session, classEntity, criteria)
        Return serverMode
        'End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de Aseguradoras
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedInsurance() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetInsuranceXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetInsuranceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de Inventario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetInventoryType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetInventoryTypeXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetInventoryTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de Equipo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetEquipmentType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentTypeXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ParentId", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Funcion para consultar el tipo de inventario transportes
    ''' </summary>
    ''' <param name="status"></param>
    ''' <param name="inventoryType"></param>
    ''' <returns></returns>
    Public Function ListFixedAssetItemTypeByStatusAndInventoryType(status As Boolean, inventoryType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetItemTypeReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & " AND InventoryTypeId.Type = " & inventoryType & " And ParentId IS NOT NULL")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetItemTypeReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListFixedAssetPhysicalAssetByItemTypeId(itemTypeId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPhysicalAssetXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ItemId.ItemTypeId = " & itemTypeId)
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPhysicalAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListFixedAssetItemType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetItemTypeReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetItemTypeReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Propiedad Física
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPhysicalAsset() As XPInstantFeedbackSource
        ' Crear la sesión
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetPhysicalAssetXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetPhysicalAssetXpo))
        Dim criteria As CriteriaOperator = New BinaryOperator("ItemId.ItemCatalogId.Classification", 3, BinaryOperatorType.NotEqual)

        Dim serverMode = New XPInstantFeedbackSource(classEntity,
                                                 "Id;ItemId;Serie;Plate;LocationId;ResponsibleId;HistoricalValue;ItemId.Description;ItemId.Code;ItemId.Id;ItemId.CodeDescription;Model;Status;LocationId.CodeName;ResponsibleId.CodeNitName;StatusFull;StatusName;FixedAssetItemCodeNameWithPlate",
                                                 criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de cuentas de la Propiedad Física
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetVPhysicalAssetMainAccount() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetVPhysicalAssetMainAccountsReportXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetVPhysicalAssetMainAccountsReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;LegalBookId", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de cuentas de la Propiedad Física
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetMainAccountsByTypeAndByBook(type As Boolean, bookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetViewListAccountsUsedInFixedAssetXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type = " & type & " AND LegalBookId = " & bookId & " AND Status = 1")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetViewListAccountsUsedInFixedAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMainAccountsFixedAssetByStatusAndBookId(status As Boolean, bookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetFixedAssetVPhysicalAssetMainAccountsReportXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & " AND LegalBookId = " & bookId)
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetFixedAssetVPhysicalAssetMainAccountsReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los activos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllFixedAssetPhysicalAsset() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPhysicalAssetXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPhysicalAssetXpo))
        'Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;ItemId.CodeDescription;Serie;Plate;AssetDescription", Nothing)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los activos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllFixedAssetPhysicalAssetWithOutHigh() As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse($"HasHighTech={False}")
        Dim session As New IndigoXPOSession(Of FixedAssetPhysicalAssetXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPhysicalAssetXpo))
        'Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;ItemId.CodeDescription;Serie;Plate;AssetDescription", Nothing)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los activos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPhysicalAssetHasOutput() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPhysicalAssetXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("HasOutput = 0 AND NOT AdquisitionType IN (3, 8, 10)")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPhysicalAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;ItemId.CodeDescription;Serie;Plate;AssetDescription;HasOutput", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Propiedad Física
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPhysicalAssetParts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPhysicalAssetPartsXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPhysicalAssetPartsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;PartAccesoriesConsumiblesId.Code;PartAccesoriesConsumiblesId.Name;PhysicalAssetId.ItemId.Code;PhysicalAssetId.ItemId.Description;PartAccesoriesConsumiblesId.CodeName;PhysicalAssetId.ItemId.CodeDescription", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el Estado del Activo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetStatusAsset() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetStatusAssetXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetStatusAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CreationDate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de REsponsable
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetResponsibleType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetResponsibleTypeXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetResponsibleTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeDescription", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetVinculationType() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetVinculationTypeXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetVinculationTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetResponsible() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetResponsibleXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetResponsibleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId.Nit;ThirdPartyId.Name;CodeNitName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene el listado de responsables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetResponsibleByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetResponsibleXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetResponsibleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Function ListFixedAssetResponsibleByType(responsibleType As Integer, Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetResponsibleXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"ReponsibleTypeId = {responsibleType} And Status={Status}")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetResponsibleXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene el listado de responsables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetInsuranceByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetInsuranceXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetInsuranceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene el listado de responsables por estado
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetPolizaTypeByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPolizaTypeXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPolizaTypeXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListFixedAssetEquipment() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;LastCostItem;StatusName;Status", Nothing)
            Return serverMode
        End Using
    End Function

    Public Function ListFixedAssetEquipmentByStatus(Status As Boolean, Optional AdquisitionType As Integer = 0) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = ?", Status)

        If AdquisitionType <> 0 AndAlso AdquisitionType <> 1 Then
            Dim additionalCriteria As CriteriaOperator = CriteriaOperator.Parse("ItemCatalogId.Classification <> 3")
            criteria = CriteriaOperator.And(criteria, additionalCriteria)
        End If

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;LastCostItem;StatusName;Status;CodeDescription", criteria)
        Return serverMode
    End Function

    Public Function ListFixedAssetStatusAssetByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetStatusAssetXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetStatusAssetXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListFixedAssetInputRemission() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetRemissionEntranceXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetRemissionEntranceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;RemisionDate;RemisionNumber;StatusName;Status", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetRemissionEntranceBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetRemissionEntranceXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetRemissionEntranceXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SupplierDistributionLineId = " & SupplierDistributionLineId)
        Dim serverMode = New XPCollection(session, classEntity, criteria)
        Return serverMode
        'End Using
    End Function

    Public Function ListFixedAssetPartsAccesoriesConsumibles() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPartsAccesoriesConsumablesXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPartsAccesoriesConsumablesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetIngress() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetIngressXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetIngressXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetPurchaseOrder() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPurchaseOrderXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPurchaseOrderXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Detail;PurchaseOrderDate;DeliverDate;SupplierDistributionLineId.IdSupplier.CodeName;StatusName;TotalValue;CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetPurchaseOrderDetail() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetPurchaseOrderXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPurchaseOrderXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;PurchaseOrderDate;PurchaseOrderNumber", Nothing)
        Return serverMode
    End Function

    Public Function ListPurchaseOrderDetailBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of FixedAssetPurchaseOrderEquipmentXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetPurchaseOrderEquipmentXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdFixedAssetPurchaseOrder.IdSupplierDistributionLine=" & SupplierDistributionLineId & " AND IdFixedAssetPurchaseOrder.Status = 2 ")
        Dim serverMode = New XPCollection(session, classEntity, criteria)
        Return serverMode
        'End Using
    End Function

    Public Function ListFixedAssetEntryItemDetailForDevolution(FixedAssetEntryId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListFixedAssetEntryItemDetail)()

        Dim classEntity = session.GetClassInfo(GetType(ViewListFixedAssetEntryItemDetail))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("FixedAssetEntryId = " & FixedAssetEntryId)
        Dim serverMode = New XPCollection(session, classEntity, criteria)
        Return serverMode
        'End Using
    End Function

    Public Function ListFixedAssetEquipmentByEquipmentType(IdEquipmentType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetEquipmentXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetEquipmentXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ItemTypeId=" & IdEquipmentType)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    Public Function ListFixedAssetTransaction() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetTransactionXpo)()

        Dim classEntity = session.GetClassInfo(GetType(FixedAssetTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;InvoiceNumber;Value;CurrencyAbbreviation;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetReclassification() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)
            Dim classEntity = session.GetClassInfo(GetType(FixedAssetReclassificationXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ReclassificationType;ReclassificationTypeName;Status;StatusName", Nothing)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Consulta el listado de indicios de deterioro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDeteriorationIndications() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DeteriorationIndicationsXpo)()

        Dim classEntity = session.GetClassInfo(GetType(DeteriorationIndicationsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Rate;Status;CreationDate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de indicios de deterioro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDeteriorationIndicationsByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DeteriorationIndicationsXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & Status & "")
        Dim classEntity = session.GetClassInfo(GetType(DeteriorationIndicationsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

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
    ''' Funcion para obtener una colección de responsables
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionResponsible(ByVal filtro As String) As XPCollection(Of FixedAssetResponsibleReportXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetResponsibleReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of FixedAssetResponsibleReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Funcion para obtener una colección de catalogos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionItemCatalog(ByVal filtro As String) As XPCollection(Of FixedAssetItemCatalogReportXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetItemCatalogReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of FixedAssetItemCatalogReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de catalogos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCatalogItem() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetItemCatalogReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetItemCatalogReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;CodeDescription;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Funcion para obtener una colección de articulos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionItem(ByVal filtro As String) As XPCollection(Of FixedAssetItemReportXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetItemReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of FixedAssetItemReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Consulta el listado de articulos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListItem() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of FixedAssetItemReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(FixedAssetItemReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;StatusName;ItemCatalogIdDescription", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Funcion para obtener una colección de tipos de artículos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionItemType(ByVal filtro As String) As XPCollection(Of FixedAssetItemTypeReportXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetItemTypeReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of FixedAssetItemTypeReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Funcion para obtener una colección de ubicacion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionLocation(ByVal filtro As String) As XPCollection(Of FixedAssetLocationReportXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetLocationReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of FixedAssetLocationReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Funcion para obtener una colección de activos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionPhysicalAsset(ByVal filtro As String) As XPCollection(Of FixedAssetPhysicalAssetReportXpo)
        Dim session As New IndigoXPOSession(Of FixedAssetPhysicalAssetReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of FixedAssetPhysicalAssetReportXpo)(session, criteria)
    End Function

#Region "Address"
    ''' <summary>
    ''' Lista todos las direcciones por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllAddressByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(AddressXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las direcciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllAddress() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(AddressXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "Area"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllAreaByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(AreaXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllArea() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(AreaXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "Group"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllGroupByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(GroupXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", criteria)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllGroup() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(GroupXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;IdBudgetEntry", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "Iva"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllIvaByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(IvaXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllIva() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(IvaXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Percent", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "Deduction"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDeductionByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(DeductionXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDeduction() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(DeductionXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "ConceptsShuttle"

    ''' <summary>
    ''' Lista todos los conceptos de traslados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptShuttle() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(ConceptsShuttleXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
            Return serverMode
        End Using
    End Function

#End Region

#Region "InflationAdjustment"
    ''' <summary>
    ''' Obtiene todos los ajustes de inflacion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllInflationAdjustment() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(InflationAdjustmentXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;YearMonth;Percent", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "Productos"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllProductByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(ProductXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
            Return serverMode
        End Using
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllProduct() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(ProductXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

#Region "Classificacion"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllClassificationByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)
            Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
            Dim classEntity = session.GetClassInfo(GetType(ClassificationXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
            Return serverMode

        End Using
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllClassification() As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)

            Dim classEntity = session.GetClassInfo(GetType(ClassificationXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
            Return serverMode
        End Using
    End Function
#End Region

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