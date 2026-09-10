'************************************************************
' Assembly         : Domain.Accounting
' Author           : Pablo Alexander Salazar Sanchez
' Created          : 14/12/2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
Imports Domain.Base.Entities


#End Region


Public Interface IMainAccountLevelsRepository
    Inherits IRepository(Of MainAccountLevels)

    ''' <summary>
    ''' Obtiene todos los Niveles de cuntas contables
    ''' </summary>
    ''' <returns>Lista de los entidades</returns>
    ''' <remarks></remarks>
    Function GetAllMainAccountLevels() As List(Of MainAccountLevels)

    ''' <summary>
    ''' obtiene un Nivel de cuntas contables por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMainAccountLevelsByCode(code As String) As MainAccountLevels

    ''' <summary>
    ''' obtiene un Nivele de cuntas contables por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMainAccountLevelsById(id As Integer, Optional tracking As Boolean = True) As MainAccountLevels

    '''' <summary>
    '''' Valida que el digito de cuenta contable sea mayor al anterior, que el nivel tenga consecutivo para poder guardar
    '''' </summary>
    '''' <param name="mainAccountLevels"></param>
    '''' <returns></returns>
    'Function GetMainAccountLevelsByDigits(mainAccountLevels As MainAccountLevels) As List(Of MainAccountLevels)

End Interface
