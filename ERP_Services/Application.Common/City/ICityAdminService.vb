'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 11-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las ciudades
    ''' </summary>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Function ListAllCity() As List(Of City)

    ''' <summary>
    ''' Elimina una ciudad
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function DeleteCity(ByVal city As City, ByVal audit As AuditMessage) As ActionMessageResult(Of City)

    ''' <summary>
    ''' Graba o Actualiza una ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <param name="audit">Objeto de Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function SaveCity(ByVal city As City, ByVal audit As AuditMessage) As ActionResult(Of City)

    ''' <summary>
    ''' Obtiene una ciudad en especifica
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Function GetCity(ByVal code As String) As City

    ''' <summary>
    ''' Retorna una ciudad especifica
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <param name="idDepartamento">Id del departamento</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Function GetCity(ByVal code As String, ByVal idDepartamento As Integer, audit As Infrastructure.CrossCutting.Base.AuditMessage) As City

    ''' <summary>
    ''' Metodo que lista todas las ciudades dependiendo del departanmento
    ''' </summary>
    ''' <param name="IdDepartment">Id del departamento</param>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    Function ListAllCitiesByIdDepartment(ByVal idDepartment As Integer) As List(Of City)

    ''' <summary>
    ''' Obtiene una ciudad especifica
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    Function GetCityById(ByVal idCity As Integer) As City


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStateCity(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of City)

End Interface
