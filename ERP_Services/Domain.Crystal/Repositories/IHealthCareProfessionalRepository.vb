'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Jhossept K. Garay
' Created          : 11-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Interface IHealthCareProfessionalRepository
    Inherits IRepository(Of INPROFSAL)

    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProfessionalByCode(Code As String) As INPROFSAL

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSpecialityByCode(Code As String) As INESPECIA

    ''' <summary>
    ''' Obtener Usuario del HIS
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUserHIS(Code As String) As SEGusuaru
End Interface
