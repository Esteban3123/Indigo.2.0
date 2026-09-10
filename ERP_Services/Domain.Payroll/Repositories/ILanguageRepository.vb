'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface ILanguageRepository
    Inherits IRepository(Of Language)

    ''' <summary>
    ''' Obtiene todos los idiomas
    ''' </summary>
    ''' <returns>Lista de idiomas</returns>
    ''' <remarks></remarks>
    Function ListAllLanguage() As List(Of Language)

    ''' <summary>
    ''' Obtiene un idioma en especifico
    ''' </summary>
    ''' <param name="code">Codigo del idioma</param>
    ''' <returns>Idioma</returns>
    ''' <remarks></remarks>
    Function GetLanguage(ByVal code As String, Optional tracking As Boolean = True) As Language

End Interface
