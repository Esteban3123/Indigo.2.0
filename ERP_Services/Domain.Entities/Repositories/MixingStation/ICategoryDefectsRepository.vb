'************************************************************
' Assembly         : Domain.Authorization
' Author           : Andres Alarcon
' Created          : 26/08/2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface ICategoryDefectsRepository
    Inherits IRepository(Of DefectClassificationGroup)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCategoryDefectsByCode(code As String) As DefectClassificationGroup
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCategoryDefectsById(id As Integer) As DefectClassificationGroup
End Interface
