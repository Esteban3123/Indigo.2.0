'***********************************************************************
' Assembly         : Domain.Common
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/08/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IDistributionLinesRepository
    Inherits IRepository(Of DistributionLines)

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDistributionLinesById(id As Integer, Optional tracking As Boolean = True) As DistributionLines

    ''' <summary>
    ''' Obtiene una linea de distribucion por codigo
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDistributionLines(code As String, Optional tracking As Boolean = True) As DistributionLines

    ''' <summary>
    ''' Obtiene un tipo de proveedor por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSupplierTypeByCode(code As String) As SupplierType

    ''' <summary>
    ''' Obtiene una unidad de radicacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFilingUnitByCode(code As String) As FilingUnit

End Interface
