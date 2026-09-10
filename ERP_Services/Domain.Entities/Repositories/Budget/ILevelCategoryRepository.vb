'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ILevelCategoryRepository
    Inherits IRepository(Of LevelCategory)

    ''' <summary>
    ''' Obtiene un nivel de categoria por Nivel
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLevelCategory(level As String, Optional tracking As Boolean = True) As LevelCategory

    ''' <summary>
    ''' Obtiene todos los niveles de categoria
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLevelsCategory() As List(Of LevelCategory)

End Interface
