'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PPackage

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IPackage

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues
#End Region

#Region "Builder"
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As IPackage)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequence() As Task
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequence = Await model.GetSequence()
        End Using
    End Function

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the Product.
    ''' </summary>
    Public Sub InitializeProduct(Optional ATCId As Integer? = Nothing, Optional ProductNPT As Boolean? = Nothing)
        Using Model As New MBusqueda
            Me._view.ProductDatasource = Model.ConsultarEntidades(eDataSource.ListInventoryProductByProductType, {5, ATCId, ProductNPT})
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the risk level.
    ''' </summary>
    Public Sub InitializeRiskLevel()
        Using Model As New MBusqueda
            Me._view.RiskLevelDatasource = Model.ConsultarEntidades(eDataSource.ListInventoryRiskLevel)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia los tipos de dosis unitarias
    ''' </summary>
    Public Sub InitializeUnitDoseType()
        Using Model As New MBusqueda
            Me._view.UnitDoseTypeDatasource = Model.ConsultarEntidades(eDataSource.ListUnitDoseType)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia los tipos de dosis unitarias
    ''' </summary>
    Public Function InitializeUnitDoseTypeByStatusAndClass() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListUnitDoseTypeByStatusAndClass()
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <returns></returns>
    Public Function GetStabilityTableDetailByATCId(atcId As Integer) As List(Of StabilityTableDetailXpo)
        Dim filter As String = "ATCId.Id = " & atcId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of StabilityTableDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el detalle de estabilidad que corresponde al atc
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <returns></returns>
    Public Function GetStabilityTableDetail(atcId As Integer) As StabilityTableDetailXpo
        Dim filter As String = "ATCId.Id = " & atcId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of StabilityTableDetailXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function InitializeNPT() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListNPT
    End Function

    ''' <summary>
    ''' Muestra el listado de temperatura de almacenamiento parametrizados
    ''' </summary>
    Public Sub InitializeStorageTemperature()
        _view.StorageTemperatureDatasource = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListStorageTemperature
    End Sub

    ''' <summary>
    ''' Initializes the measure unit.
    ''' </summary>
    Public Function InitializeMeasureUnitWeight() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Dim _filter As Object() = {1}
            Return Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Function

    ''' <summary>
    ''' Initializes the measure unit UnitType.
    ''' </summary>
    Public Function InitializeMeasureUnitWeights() As XPInstantFeedbackSource
        Dim _filter As New List(Of Integer) From {1, 2}
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListMeasureUnitByTypes(_filter)
    End Function

    ''' <summary>
    ''' Initializes the measure unit volumen.
    ''' </summary>
    Public Function InitializeMeasureUnitVolumen() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Dim _filter As Object() = {2}
            Return Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Function

    ''' <summary>
    ''' Initializes the generics or international measure unit.
    ''' </summary>
    Public Function InitializeMeasureUnitGeneric() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Dim _filter As Object() = {3, "unidad"}
            Return Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una unidad de medida por el id
    ''' </summary>
    ''' <param name="measureUnitId"></param>
    ''' <returns></returns>
    Public Function InitializeMeasureUnitPrepared(measureUnitId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListMeasureUnitById(measureUnitId)
    End Function

    ''' <summary>
    ''' Initializes the measure unit volumen.
    ''' </summary>
    Public Function InitializeMeasureUnitByType(formulationType As Integer?) As XPInstantFeedbackSource
        Dim type As Integer? = formulationType
        If formulationType IsNot Nothing AndAlso (formulationType = 3 OrElse formulationType = 4) Then
            type = 1
        End If

        If type IsNot Nothing Then
            Using Model As New MBusqueda
                Dim _filter As Object() = {type}
                Return Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
            End Using
        Else
            Using Model As New MBusqueda
                Return Model.ConsultarEntidades(eDataSource.ListMeasureUnit)
            End Using
        End If
    End Function

    ''' <summary>
    ''' funcion que retorna la consulta a la vista
    ''' </summary>
    ''' <param name="AGRUPAQUETE"></param>
    ''' <returns></returns>
    Public Function GetHCFARMEPDbyAGRUPAQUETE(AGRUPAQUETE As String) As List(Of ViewListHCFARMEPDtoConfirmationUnitDoseXpo)
        Dim filter As String = String.Format("AGRUPAQUETE = '{0}'", AGRUPAQUETE)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListHCFARMEPDtoConfirmationUnitDoseXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' obtiene el ATC
    ''' </summary>
    ''' <param name="AtcId"></param>
    ''' <returns></returns>
    Public Function GetATC(AtcId As Integer) As ATCXpo
        Dim filter As String = String.Format("Id = '{0}'", AtcId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of ATCXpo)(Nothing, filter).FirstOrDefault
    End Function

    ''' <summary>
    ''' funcion que retorna la consulta a la vista
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetComponentTypeNPT(Id As Integer) As List(Of ViewListComponentsNPTXpo)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListComponentsNPT(Id)
    End Function

    ''' <summary>
    ''' obtiene el ATC
    ''' </summary>
    ''' <param name="AtcId"></param>
    ''' <returns></returns>
    Public Function GetInventoryRiskByATCId(AtcId As Integer) As InventoryRiskLevelXpo
        Dim filter = String.Format("Id = '{0}'", AtcId)
        Dim atc = XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of ATCXpo)(Nothing, filter).FirstOrDefault

        filter = String.Format("Id = '{0}'", atc.InventoryRiskLevelId)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of InventoryRiskLevelXpo)(Nothing, filter).FirstOrDefault
    End Function

    Public Function GetATCByCode(code As String) As ATCXpo
        Dim filter As String = String.Format("Code = '{0}'", code)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of ATCXpo)(Nothing, filter).FirstOrDefault
    End Function

#End Region

End Class
