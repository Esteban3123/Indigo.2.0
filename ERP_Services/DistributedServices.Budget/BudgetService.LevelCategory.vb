'***********************************************************************
' Assembly         : DistributedServices.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Obtiene un nivel de rubro
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLevelCategory(ByVal level As String, audit As AuditMessage) As LevelCategory Implements IBudgetServiceLevelCategory.GetLevelCategory
        Using service As ILevelCategoryAdminService = Container.Current.Resolve(Of ILevelCategoryAdminService)()
            Return service.GetLevelCategory(level.Trim(), audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene todos los niveles de rubro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListLevelsCategory(audit As AuditMessage) As List(Of LevelCategory) Implements IBudgetServiceLevelCategory.ListLevelsCategory
        Using service As ILevelCategoryAdminService = Container.Current.Resolve(Of ILevelCategoryAdminService)()
            Return service.ListLevelsCategory(audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un nivel de rubro
    ''' </summary>
    ''' <param name="LevelCategory">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Function DeleteLevelCategory(LevelCategory As LevelCategory, audit As AuditMessage) As ActionResult Implements IBudgetServiceLevelCategory.DeleteLevelCategory
        Using service As ILevelCategoryAdminService = Container.Current.Resolve(Of ILevelCategoryAdminService)()
            Return service.DeleteLevelCategory(LevelCategory, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un nivel de rubro
    ''' </summary>
    ''' <param name="LevelsCategory">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Function SaveLevelCategory(LevelsCategory As List(Of LevelCategory), audit As AuditMessage) As ActionResult(Of List(Of LevelCategory)) Implements IBudgetServiceLevelCategory.SaveLevelCategory
        Using service As ILevelCategoryAdminService = Container.Current.Resolve(Of ILevelCategoryAdminService)()
            Return service.SaveLevelCategory(LevelsCategory, audit)
        End Using
    End Function

End Class