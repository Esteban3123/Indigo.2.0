'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IDefinitionRateRepository
    Inherits IRepository(Of DefinitionRate)

    ''' <summary>
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRate(code As String) As DefinitionRate

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefinitionRateById(id As Integer) As DefinitionRate

    ''' <summary>
    ''' Obtiene la lista de codigos de las definiciones de tarifas
    ''' </summary>
    ''' <param name="listSurgicalProcedureServiceId"></param>
    ''' <returns></returns>
    Function GetListDefinitionRateCode(listSurgicalProcedureServiceId As List(Of Integer)) As List(Of String)

End Interface