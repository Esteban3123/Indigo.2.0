'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ILevelCategoryAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtiene un nivel de rubro
    ''' </summary>
    ''' <param name="level">El nivel</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Function GetLevelCategory(level As String, audit As AuditMessage) As LevelCategory

    ''' <summary>
    ''' Obtiene los niveles de rubro
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Code Vacio</exception>
    Function ListLevelsCategory(audit As AuditMessage) As List(Of LevelCategory)


    ''' <summary>
    ''' Elimina un nivel de rubro
    ''' </summary>
    ''' <param name="LevelCategory">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteLevelCategory(LevelCategory As LevelCategory, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o Actualiza un nivel de rubro
    ''' </summary>
    ''' <param name="LevelCategory">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveLevelCategory(LevelsCategory As List(Of LevelCategory), audit As AuditMessage) As ActionResult(Of List(Of LevelCategory))

End Interface
