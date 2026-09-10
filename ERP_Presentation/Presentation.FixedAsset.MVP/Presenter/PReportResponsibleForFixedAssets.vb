Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class PReportResponsibleForFixedAssets

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IReportResponsibleForFixedAssets

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IReportResponsibleForFixedAssets)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para Cargar el data source de responsables
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionResponsible()
        View.ResponsibleXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListCollectionResponsible(Nothing)
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de catalogos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionItemCatalog()
        View.ItemCatalogXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListCollectionItemCatalog(Nothing)
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de artículos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionItem()
        View.ItemXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListCollectionItem(Nothing)
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de tipo de artículos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionItemType()
        View.ItemTypeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListCollectionItemType(Nothing)
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de ubicaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionLocation()
        View.LocationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListCollectionLocation(Nothing)
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source de activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionPhysicalAsset()
        View.PhysicalAssetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListCollectionPhysicalAsset(Nothing)
    End Sub

#End Region

End Class
