'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IRequirementTemplateRepository
    Inherits IRepository(Of RequirementTemplate)

    ''' <summary>
    ''' Obtiene una plantilla de requerimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRequirementTemplate(code As String) As RequirementTemplate

    ''' <summary>
    ''' Obtiene una plantilla de requerimiento por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRequirementTemplateById(id As Integer) As RequirementTemplate

End Interface
