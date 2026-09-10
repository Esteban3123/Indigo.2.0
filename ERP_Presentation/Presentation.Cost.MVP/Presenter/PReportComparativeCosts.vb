Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class PReportComparativeCosts

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IReportComparativeCosts

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
    Public Sub New(ByRef iview As IReportComparativeCosts)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para obtener el nivel máximo de estructura organizacional
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetMaxLevelOrganizationalStructure()
        View.LevelMax = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.GetMaxLevelOrganizationalStructure()
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del clientes
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionCategories()
        View.CategoriesXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCollectionCategories(Nothing)
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del radicados
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCollectionProductionCenters()
        View.ProductionCentersXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCollectionProductionCenters(Nothing)
    End Sub

#End Region

End Class
