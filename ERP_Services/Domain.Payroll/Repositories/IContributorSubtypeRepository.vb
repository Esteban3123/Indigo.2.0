'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 05-01-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IContributorSubtypeRepository
    Inherits IRepository(Of ContributorSubtype)

    ''' <summary>
    ''' Lista todos los subtipis de cotizantes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllContributorSubtype() As List(Of ContributorSubtype)

    ''' <summary>
    ''' Obtiene por codigo el subtipo de cotizante
    ''' </summary>
    ''' <param name="Code">codigo </param>
    ''' <returns>Year</returns>
    ''' <remarks></remarks>
    Function GetContributorSubtypeByCode(ByVal Code As String, Optional tracking As Boolean = True) As ContributorSubtype

    ''' <summary>
    ''' Obtiene por id el subtipo de cotizante
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetContributorSubtypeById(ByVal Id As Integer, Optional tracking As Boolean = True) As ContributorSubtype

End Interface
