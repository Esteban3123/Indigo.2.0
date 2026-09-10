'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IEconomicIndicatorRepository
    Inherits IRepository(Of EconomicIndicator)

#Region "Methods"
    ''' <summary>
    ''' Metodo para listar todos los 
    ''' </summary>
    ''' <returns></returns>
    Function GetAllEconomicIndicator() As List(Of EconomicIndicator)

    ''' <summary>
    ''' Metodo para obtener un indicador economico por año y mes
    ''' </summary>
    Function GetEconomicIndicator(ByVal year As String, ByVal month As String, Optional tracking As Boolean = True) As EconomicIndicator

    ''' <summary>
    ''' Metodo para obtener un indicador economico por codigo
    ''' </summary>
    Function GetEconomicIndicatorByCode(ByVal code As String, Optional tracking As Boolean = True) As EconomicIndicator
#End Region

End Interface
