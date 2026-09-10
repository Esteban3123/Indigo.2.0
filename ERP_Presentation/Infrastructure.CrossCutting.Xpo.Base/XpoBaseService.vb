' ***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Xpo.Base
' Author           : Juan Diego Diaz
' Created          : 2014-01-03
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering
Imports DistributedServices.Xpo.Base
Imports System.Configuration
Imports DevExpress.Xpo.DB
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo.DB.Helpers
Imports System.Xml
' Alias para evitar ambigüedad con XpoDefault de DistributedServices.Xpo
Imports DxXpo = DevExpress.Xpo

#End Region

''' <summary>
''' Manejador de conexiones XPO
''' </summary>
''' <seealso cref="System.IDisposable" />
Public Class XPODataLayerManager
    Implements IDisposable

#Region "Properties"
    Public Shared dataStoreProviderDictionary As New Dictionary(Of String, Tuple(Of DataCacheRoot, SimpleDataLayer, Dictionary(Of String, SimpleDataLayer)))()
    Public Shared Property Company As String
    Public Shared uriServiceEntitiesXpo As String
    Public Shared protocolServicesXpo As Protocol
    Public Shared endpointConfiguration As String
    Public Shared remoteAddress As String
    Private Shared _cacheRoot As DataCacheRoot
    Private _isCached As Boolean
    Private _newServices As Boolean
    Public Property DataLayer As IDataLayer
    Private Shared _instance As Dictionary(Of String, XPODataLayerManager) = New Dictionary(Of String, XPODataLayerManager)()
#End Region

    Public Shared ReadOnly Property Instance(company As String) As XPODataLayerManager
        Get
            '_instance = New Dictionary(Of String, XPODataLayerManager)()
            If String.IsNullOrEmpty(company) Then
                company = XPODataLayerManager.Company
            End If
            If company Is Nothing Then
                Return New XPODataLayerManager("")
            End If
            If Not _instance.ContainsKey(company) Then
                _instance.Add(company, New XPODataLayerManager(company))
            End If
            DxXpo.XpoDefault.DataLayer = _instance(company).DataLayer
            Return _instance(company)
        End Get
    End Property

    Public Sub Refresh()

    End Sub

    Public Sub New(company As String)
        'Dim cached As String = ConfigurationManager.AppSettings.Get("CachedEnable")
        '_isCached = Not String.IsNullOrEmpty(cached) AndAlso cached.ToLower().Equals("true")
        _isCached = ApplicationSetting.Instance.CachedEnable
        'Dim newServices As String = ConfigurationManager.AppSettings.Get("NewServices")
        '_newServices = Not String.IsNullOrEmpty(newServices) AndAlso newServices.ToLower().Equals("true")
        _newServices = ApplicationSetting.Instance.NewServices
        If Not _newServices Then
            Return
        End If
        ReadConfiguration()
        RefreshDataLayer(company)
    End Sub

    ''' <summary>
    ''' Referesca la configuración de la capa de datos
    ''' </summary>
    ''' <param name="company">Empresa a consultar</param>
    Private Shadows Sub RefreshDataLayer(ByVal company As String)
        If Not dataStoreProviderDictionary.ContainsKey(company) Then
            Dim dataStore = New WCFServiceDataStoreCache(endpointConfiguration, remoteAddress, company)
            If _isCached Then
                _cacheRoot = New DataCacheRoot(dataStore)
                _cacheRoot.Configure(New DataCacheConfiguration(DataCacheConfigurationCaching.All))
                DataLayer = New SimpleDataLayer(_cacheRoot)
            Else
                DataLayer = New SimpleDataLayer(dataStore)
            End If
            dataStoreProviderDictionary.Add(company, New Tuple(Of DataCacheRoot, SimpleDataLayer, Dictionary(Of String, SimpleDataLayer))(_cacheRoot, DataLayer, New Dictionary(Of String, SimpleDataLayer)()))
        Else
            _cacheRoot = dataStoreProviderDictionary(company).Item1
            DataLayer = dataStoreProviderDictionary(company).Item2
        End If
    End Sub

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Shared Shadows Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
        remoteAddress = GetRemoteAddress()
        endpointConfiguration = GetEndPoint()
    End Sub

    Private Shared Shadows Function GetEndPoint() As String
        Return $"{[Enum].GetName(GetType(Protocol), protocolServicesXpo)}_Endpoint_XPOGateCache"
    End Function

    Private Shared Shadows Function GetRemoteAddress() As String
        Return $"{uriServiceEntitiesXpo}XpoGate.svc/{[Enum].GetName(GetType(Protocol), protocolServicesXpo)}XPO"
    End Function

    Public Shared Sub ClearAllCache()
        'For Each item In _dictionaryCacheNode
        '    item.Value.Reset()
        'Next
        Dim svr As New WCFServiceClearCache($"{[Enum].GetName(GetType(Protocol), protocolServicesXpo)}_Endpoint_XpoGateClearCache", $"{ConfigurationFile.Instance.UrlXpoWebServer}XpoGate.svc")
        svr.ClearAllCache()
    End Sub

    Public Function ValidateCachedNode(Of T)() As IDataLayer
        If Not _newServices Then
            Return DxXpo.XpoDefault.DataLayer
        End If
        If Not _isCached Then
            DxXpo.XpoDefault.DataLayer = dataStoreProviderDictionary(Company).Item2 'DataLayer
        Else
            Dim cacheXml As New XmlDocument()
            cacheXml.LoadXml(My.Resources.ResourceXPO.IndigoCacheLevel)
            If cacheXml.SelectNodes("/XpoCacheData/CacheLevel").Count > 0 Then
                Dim tablename As String = String.Empty
                If GetType(T).CustomAttributes.Any() Then
                    tablename = ((GetType(T).CustomAttributes(0)).ConstructorArguments(0)).Value.ToString()
                Else
                    tablename = $"dbo.{GetType(T).Name}"
                End If
                Dim obj As XmlNode = cacheXml.SelectSingleNode($"/XpoCacheData/CacheLevel[XpoEntity='{tablename}']")
                If obj IsNot Nothing Then
                    If dataStoreProviderDictionary(Company).Item3.ContainsKey(obj.Attributes.Item(0).Value) Then
                        DxXpo.XpoDefault.DataLayer = dataStoreProviderDictionary(Company).Item3(obj.Attributes.Item(0).Value)
                    Else
                        'dataStoreProviderDictionary(Company).Item3.Add(obj.Attributes.Item(0).Value, Nothing)
                        'Dim node As New IndigoDataCacheNode(dataStoreProviderDictionary(Company).Item1)
                        'node.MaxCacheLatency = TimeSpan.FromSeconds(CDbl(obj.Attributes.Item(0).Value))
                        'node.ProcessCookie(DataCacheCookie.Empty)
                        'Dim layer As New SimpleDataLayer(node)

                        'dataStoreProviderDictionary(Company).Item3(obj.Attributes.Item(0).Value) = layer
                        'DxXpo.XpoDefault.DataLayer = layer
                    End If
                Else
                    DxXpo.XpoDefault.DataLayer = DataLayer
                End If
            Else
                DxXpo.XpoDefault.DataLayer = DataLayer
            End If
        End If
        Return DxXpo.XpoDefault.DataLayer
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                For Each i In _instance.Select(Function(o) o.Value)
                    i.Dispose()
                Next
            End If
            _instance = Nothing
        End If
        disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

''' <summary>
''' Crea la sesion dependiendo si maneja o no cache
''' </summary>
''' <typeparam name="T"></typeparam>
''' <seealso cref="DevExpress.Xpo.Session" />
Public Class IndigoXPOSession(Of T)
    Inherits Session

    Public Sub New()
        MyBase.New(XPODataLayerManager.Instance("").ValidateCachedNode(Of T)())
    End Sub

End Class

''' <summary>
''' Clase abstracta para funciones bases de los servicios XPO
''' </summary>
Public MustInherit Class XpoBaseService

    ''' <summary>
    ''' List entity by T class
    ''' </summary>
    ''' <returns></returns>
    Public Function ListXPInstantFeedbackSource(Of T As {XPLiteObject})(
            Optional filter As String = Nothing,
            Optional displayableProperties As String = Nothing,
            Optional sortField As String = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of T)()
        Dim criteria = CriteriaOperator.Parse(filter)
        Dim classEntity = session.GetClassInfo(GetType(T))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, displayableProperties, criteria)
        serverMode.DefaultSorting = sortField
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los centros de atención externos por permiso de usuario
    ''' </summary>
    ''' <returns></returns>
    Public Function Count(Of T As {XPLiteObject})(Optional strCriteria As String = Nothing) As Integer
        Dim session As New IndigoXPOSession(Of T)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Dim classEntity = session.GetClassInfo(GetType(T))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)

        Return CType(session.Evaluate(Of T)(New AggregateOperand("", Aggregate.Count), serverMode.FixedFilterCriteria), Integer)
    End Function
    ''' <summary>
    ''' Valida si existe un registro por el filtro
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Public Function Any(Of T As {XPLiteObject})(filter As String) As Boolean
        Dim res = GetXPOObject(Of T)(filter)
        Return res IsNot Nothing
    End Function
    ''' <summary>
    ''' Obtiene un objeto xpo
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="filter"></param>
    ''' <returns></returns>
    Public Function GetXPOObject(Of T As {XPLiteObject})(Optional filter As String = Nothing) As T
        Dim session As New IndigoXPOSession(Of T)()
        Return session.FindObject(Of T)(CriteriaOperator.Parse(filter))
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionAsList(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(DxXpo.XpoDefault.DataLayer, Fun, criteria)
        Return result
    End Function

    ''' <summary>
    ''' Función para cargar una colección y retornar una lista especifica de datos
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="DataLayerAux">DataLayer para la conexión a traves del servicio XPO</param>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function LoadCollection(Of T)(
                                        DataLayerAux As IDataLayer,
                                        Optional Fun As Func(Of T, Boolean) = Nothing,
                                        Optional criteria As String = Nothing,
                                        Optional session As Session = Nothing,
                                        Optional withSort As Boolean = True,
                                        Optional topRows As Integer? = Nothing,
                                        Optional propertyToOrderBy As String = Nothing
                                        ) As List(Of T)
        Dim sessionNew = Nothing
        If session IsNot Nothing Then
            sessionNew = session
        Else
            sessionNew = New Session(DataLayerAux)
        End If
        Dim classEntity As XPClassInfo = sessionNew.GetClassInfo(GetType(T))
        Dim ListObj = Nothing
        Using collection = New XPCollection(sessionNew, classEntity)
            If topRows IsNot Nothing Then
                collection.TopReturnedObjects = topRows
            End If
            If Fun Is Nothing Then
                If criteria Is Nothing Then
                    collection.Load()
                    ListObj = collection.OfType(Of T).ToList
                Else
                    collection.Criteria = CriteriaOperator.Parse(criteria)

                    If withSort Then
                        Dim sortCollection As SortingCollection = New SortingCollection()
                        sortCollection.Add(New SortProperty(If(String.IsNullOrEmpty(propertyToOrderBy), classEntity.KeyProperty.Name, propertyToOrderBy), DevExpress.Xpo.DB.SortingDirection.Descending))
                        collection.Sorting = sortCollection
                    End If

                    collection.Load()
                    ListObj = collection.OfType(Of T).ToList
                End If
            Else
                collection.Load()
                ListObj = collection.OfType(Of T).Where(Fun).ToList
            End If
        End Using
        Return ListObj
    End Function

    Public Function LoadView(Of T)(DataLayerAux As IDataLayer) As XPView
        Dim sessionNew = New Session(DataLayerAux)
        Dim classEntity As XPClassInfo = sessionNew.GetClassInfo(GetType(T))
        Dim propertiesView = classEntity.PersistentProperties
        Dim assocProperties = classEntity.AssociationListProperties
        Dim ObjProperties = classEntity.ObjectProperties
        Dim xpViewObj As XPView = New XPView(sessionNew, classEntity)
        For Each item In propertiesView
            Dim propertyRef As Metadata.ReflectionPropertyInfo = item
            xpViewObj.AddProperty(propertyRef.MappingField, propertyRef.Name)
        Next
        For Each itemAssoc In assocProperties
            Dim propertyRef As Metadata.ReflectionPropertyInfo = itemAssoc
            For Each itemAssocAux In propertyRef.CollectionElementType.PersistentProperties
                Dim propertyRefAux As Metadata.ReflectionPropertyInfo = itemAssocAux
                xpViewObj.AddProperty(propertyRef.Name & "." & propertyRefAux.MappingField, propertyRef.Name & "[" & propertyRefAux.Name & "]")
            Next
        Next
        For Each itemObj In ObjProperties
            Dim propertyRef As Metadata.ReflectionPropertyInfo = itemObj
            Dim ClassAux = propertyRef.StorageType
            Dim classEntityAux As XPClassInfo = sessionNew.GetClassInfo(ClassAux)
            Dim propertiesViewAux = classEntityAux.PersistentProperties
            For Each itemObjAux In propertiesViewAux
                Dim propertyRefAux As Metadata.ReflectionPropertyInfo = itemObjAux
                xpViewObj.AddProperty(propertyRef.Name & "." & propertyRefAux.MappingField, propertyRef.Name & "." & propertyRefAux.Name)
            Next
        Next
        Return xpViewObj
    End Function



#Region "XPCollection Methods"
    Public _objectType As Type

    Public Function GetXPCollection(Optional displayMembers As String = Nothing, Optional criteria As CriteriaOperator = Nothing) As XPCollection
        Dim _xpCollection = New XPCollection()
        CType(_xpCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        _xpCollection.DisplayableProperties = displayMembers
        CType(_xpCollection, System.ComponentModel.ISupportInitialize).EndInit()
        AddHandler _xpCollection.ResolveSession, AddressOf XPCollection_ResolveSession
        Return _xpCollection
    End Function

    Public Function GetXPCollectionEntity(Of T)(Optional displayMembers As String = Nothing, Optional criteria As CriteriaOperator = Nothing) As XPCollection(Of T)
        Dim _xpCollection = New XPCollection(Of T)
        CType(_xpCollection, System.ComponentModel.ISupportInitialize).BeginInit()
        _xpCollection.DisplayableProperties = displayMembers
        _objectType = GetType(T)
        CType(_xpCollection, System.ComponentModel.ISupportInitialize).EndInit()
        AddHandler _xpCollection.ResolveSession, AddressOf XPCollection_ResolveSession
        Return _xpCollection
    End Function

    Private Sub XPCollection_ResolveSession(sender As Object, e As DevExpress.Xpo.ResolveSessionEventArgs)
        ValidateDataLayer()
        Dim session As New Session(DxXpo.XpoDefault.DataLayer)
        e.Session = session
    End Sub
#End Region

#Region "XPInstantFeedbackSource Methods"
    Public WithEvents xpInstantFeedbackSource As XPInstantFeedbackSource

    Private Sub XPInstantFeedbackSource_DismissSession(ByVal sender As Object, ByVal e As ResolveSessionEventArgs) Handles xpInstantFeedbackSource.DismissSession
        Dim session1 As IDisposable = TryCast(e.Session, IDisposable)
        If session1 IsNot Nothing Then
            session1.Dispose()
            xpInstantFeedbackSource.Dispose()
        End If
    End Sub
    Private Sub XPInstantFeedbackSource_ResolveSession(ByVal sender As Object, ByVal e As ResolveSessionEventArgs) Handles xpInstantFeedbackSource.ResolveSession
        _objectType = CType(sender, XPInstantFeedbackSource).ObjectType
        ValidateDataLayer()
        Dim session As New Session(DxXpo.XpoDefault.DataLayer)
        e.Session = session
    End Sub
#End Region

    Public Sub ValidateDataLayer()

    End Sub

End Class
