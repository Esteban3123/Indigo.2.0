'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IPensionaryTypeRepository

    Inherits IRepository(Of PensionaryType)

    ''' <summary>
    ''' Lista todos los Tipos de Pensiones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPensionaryType() As List(Of PensionaryType)

    ''' <summary>
    ''' Obtiene un Tipo de Pensión especifico
    ''' </summary>
    ''' <param name="code">Codigo del Tipo de Pensión</param>
    ''' <returns>Tipo de Pensión</returns>
    ''' <remarks></remarks>
    Function GetPensionaryType(ByVal code As String, Optional tracking As Boolean = True) As PensionaryType

End Interface
