'***********************************************************************
' Assembly         : Domain.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/12/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface ICostDirectDistributionSecondaryRepository
    Inherits IRepository(Of CostDirectDistributionSecondary)

    ''' <summary>
    ''' Calcular distribución secundaria
    ''' </summary>
    ''' <param name="costDistributionSecondaryId"></param>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    Function CalculateDistributionSecondary(costDistributionSecondaryId As Integer, Year As Integer, Month As Integer) As List(Of SP_CalculateDistributionSecondary_Result)

    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCostDirectDistributionSecondary(ByVal code As String) As CostDirectDistributionSecondary

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCostDirectDistributionSecondaryById(id As Integer) As CostDirectDistributionSecondary

    ''' <summary>
    ''' Obtiene la cantidad de veces que esta el elemento de distribucion secundaria 
    ''' </summary>
    ''' <returns></returns>
    Function GetCountByCostDistributionSecondaryId(idDistributionSecundary As Integer, month As Integer, year As Integer) As Integer

    ''' <summary>
    ''' Actualiza el campo import en la tabla LogisticProductionCenterRecordDetail
    ''' </summary>
    ''' <param name="ObjectXml"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_UpdateFieldImportCost(ObjectXml As String) As SP_UpdateFieldImportCost_Result

    ''' <summary>
    ''' Guarda la obligacion
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <param name="ListDeleteXml"></param>
    ''' <param name="ListLogisticsProductionCenterDetailXml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveDistributionSecondary(EntityXml As String, ListDeleteXml As String, ListLogisticsProductionCenterDetailXml As String, CodeUser As String) As SP_SaveDistributionSecondary_Result

    ''' <summary>
    ''' Generar Distribuciones Secundarias
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_GenerateDistributionSecondary(Year As Integer, Month As Integer, OperatingUnitId As Integer, CodeUser As String) As SP_GenerateDistributionSecondary_Result

End Interface
