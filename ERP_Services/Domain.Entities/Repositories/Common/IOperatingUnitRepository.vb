'***********************************************************************
' Assembly         : Domain.Common
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 30-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Public Interface IOperatingUnitRepository
    Inherits IRepository(Of OperatingUnit)

    ''' <summary>
    ''' obtiene una unidad operativa por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOpertatingUnitByCode(code As String) As OperatingUnit

    ''' <summary>
    ''' obtiene una unidad operativa por id
    ''' </summary>
    ''' <param name="id">id de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOperatingUnitById(id As Integer) As OperatingUnit

    ''' <summary>
    ''' Obtiene todas las unidades operativas
    ''' </summary>
    ''' <returns>Lista de unidades operativas</returns>
    ''' <remarks></remarks>
    Function ListAllOperatingUnit() As List(Of OperatingUnit)

End Interface
