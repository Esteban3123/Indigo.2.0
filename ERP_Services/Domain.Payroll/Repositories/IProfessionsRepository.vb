'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IProfessionsRepository
    Inherits IRepository(Of Profession)

    ''' <summary>
    ''' Lista todas las profesiones
    ''' </summary>
    ''' <returns>Lista de profesiones</returns>
    ''' <remarks></remarks>
    Function ListAllProfessions() As List(Of Profession)

    ''' <summary>
    ''' Obtiene una profesion especifica
    ''' </summary>
    ''' <param name="code">Codigo de la profesion</param>
    ''' <returns>Profesion</returns>
    ''' <remarks></remarks>
    Function GetProfessions(ByVal code As String, Optional tracking As Boolean = True) As Profession

End Interface
