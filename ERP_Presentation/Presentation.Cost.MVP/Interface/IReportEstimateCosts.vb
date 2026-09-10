Imports DevExpress.Xpo

Public Interface IReportEstimateCosts

#Region "Properties"

    ''' <summary>
    ''' Establece y obtiene el año inicial
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property YearStart As Integer

    ''' <summary>
    ''' Establece y obtiene el mes inicial
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MonthStart As Integer

    ''' <summary>
    ''' Establece y obtiene el año final
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property YearEnd As Integer

    ''' <summary>
    ''' Establece y obtiene el mes final
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property MonthEnd As Integer

    ''' <summary>
    ''' Establece y obtiene el tipo de reporte
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property TypeReport As Integer

    ''' <summary>
    ''' Establece y obtiene el tipo de agrupacion
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property GroupBy As Integer

    ''' <summary>
    ''' Establece y obtiene el nivel máximo de la estructura organizacional
    ''' </summary>
    ''' <returns></returns>
    Property LevelMax As Integer

    ''' <summary>
    ''' Establece y obtiene el nivel de la estructura organizacional
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Level As Integer

    ''' <summary>
    ''' Establece y obtiene el tipo de informacion detallada a mostrar
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property SecondaryDetailType As Integer

    ''' <summary>
    ''' Establece y obtiene el tipo de visualizacion
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Visualization As Integer

    ''' <summary>
    ''' Establece y obtiene los tipos de centro de producción
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property CenterType As String

    ''' <summary>
    ''' Establece y obtiene las categorias de centros de producción
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property CategoryIds As String

    ''' <summary>
    ''' Establece y obtiene los centros de producción
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property ProductionCenterIds As String

    ''' <summary>
    ''' Establece y obtiene el tipo de informacion detallada a mostrar
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property DetailType As String

#End Region

#Region "XPO"

    '' <summary>
    '' Establece el datasource de categorias
    '' </summary>
    '' <value></value>
    '' <returns></returns>
    '' <remarks></remarks>
    Property CategoriesXpo As XPCollection(Of Infrastructure.Data.Xpo.CostRepository.CostProductionCenterCategoryXpo)

    '' <summary>
    '' Establece el datasource de centros de produccion
    '' </summary>
    '' <value></value>
    '' <returns></returns>
    '' <remarks></remarks>
    Property ProductionCentersXpo As XPCollection(Of Infrastructure.Data.Xpo.CostRepository.CostProductionCenterXpo)

#End Region

End Interface
