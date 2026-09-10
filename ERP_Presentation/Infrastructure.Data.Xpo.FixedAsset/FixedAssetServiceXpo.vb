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
Public Class FixedAssetServiceXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocation() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(FixedAssetFixedAssetLocationXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Lista todos las Ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocationData() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(FixedAssetFixedAssetLocationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipo de ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocationType() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(FixedAssetFixedAssetLocationTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;OrderLocation;State;StateName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipo de ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetCostCenter() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PayrollCostCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;State;StateName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipo de ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetMainAccount() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GeneralLedgerMainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;NumberName;HandlesThirdParty;HandlesCostCenter;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFixedAssetLocationTypeParent(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(FixedAssetFixedAssetLocationTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;OrderLocation,state", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Marcas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTrademark() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetTrademarkXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Codigo;Descripcion", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de Poliza
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPolizaType() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetPolizaTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de Poliza
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetPoliza() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetPolizaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function


    ''' <summary>
    ''' Consulta el listado de Catalogo de Equipos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedEquipmentCatalog() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetEquipmentCatalogXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Aseguradoras
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedInsurance() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetInsuranceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de tipos de Inventario
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetInventoryType() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetInventoryTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de Equipo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetEquipmentType() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetEquipmentTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de Tipos de REsponsable
    ''' </summary>
    ''' <returns></returns>
    Public Function ListFixedAssetResponsibleType() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetResponsibleTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetVinculationType() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetVinculationTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetResponsible() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetResponsibleXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdThirdParty.Nit;IdThirdParty.Name", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetEquipment() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetEquipmentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetInputRemission() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetRemissionEntranceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;RemisionDate;RemisionNumber", Nothing)
        Return serverMode
    End Function

    Public Function ListFixedAssetPartsAccesoriesConsumibles() As XPInstantFeedbackSource
        classEntity = XpoDefault.Session.GetClassInfo(GetType(FixedAssetPartsAccesoriesConsumablesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

#Region "Address"
    ''' <summary>
    ''' Lista todos las direcciones por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllAddressByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(AddressXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las direcciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllAddress() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AddressXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function
#End Region

#Region "Area"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllAreaByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(AreaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllArea() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AreaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function
#End Region

#Region "Group"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllGroupByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(GroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllGroup() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;IdBudgetEntry", Nothing)
        Return serverMode
    End Function
#End Region

#Region "Iva"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllIvaByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(IvaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllIva() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(IvaXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Percent;Rate", Nothing)
        Return serverMode
    End Function
#End Region

#Region "Deduction"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDeductionByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(DeductionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllDeduction() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DeductionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function
#End Region

#Region "ConceptsShuttle"

    ''' <summary>
    ''' Lista todos los conceptos de traslados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConceptShuttle() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ConceptsShuttleXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

#End Region

#Region "InflationAdjustment"
    ''' <summary>
    ''' Obtiene todos los ajustes de inflacion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllInflationAdjustment() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(InflationAdjustmentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;YearMonth;Percent", Nothing)
        Return serverMode
    End Function
#End Region

#Region "Productos"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllProductByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(ProductXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllProduct() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ProductXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function
#End Region

#Region "Classificacion"
    ''' <summary>
    ''' Lista todos las areas por un estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllClassificationByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(ClassificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las areas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllClassification() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ClassificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function
#End Region

#End Region

End Class