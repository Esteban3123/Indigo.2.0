Imports DevExpress.Xpo

Public Interface IReportComparativeCosts

#Region "Properties"

    ''' <summary>
    ''' Establece y obtiene el año inicial del rango inicial
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property InitialRangeYearStart As Integer

    ''' <summary>
    ''' Establece y obtiene el mes inicial del rango inicial
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property InitialRangeMonthStart As Integer

    ''' <summary>
    ''' Establece y obtiene el año final del rango inicial
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property InitialRangeYearEnd As Integer

    ''' <summary>
    ''' Establece y obtiene el mes final del rango inicial
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property InitialRangeMonthEnd As Integer

    ''' <summary>
    ''' Establece y obtiene el año inicial del rango final
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FinalRangeYearStart As Integer

    ''' <summary>
    ''' Establece y obtiene el mes inicial del rango final
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FinalRangeMonthStart As Integer

    ''' <summary>
    ''' Establece y obtiene el año final del rango final
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FinalRangeYearEnd As Integer

    ''' <summary>
    ''' Establece y obtiene el mes final del rango final
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FinalRangeMonthEnd As Integer

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
