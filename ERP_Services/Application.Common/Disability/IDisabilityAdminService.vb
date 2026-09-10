'***********************************************************************
' Assembly         : Application.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 03-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDisabilityAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las Discapacidades
    ''' </summary>
    ''' <returns>Lista de Discapacidades</returns>
    ''' <remarks></remarks>
    Function ListAllDisability() As List(Of Disability)

    ''' <summary>
    ''' Elimina una discapacidad
    ''' </summary>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function DeleteDisability(ByVal disability As Disability, ByVal audit As AuditMessage) As ActionMessageResult(Of Disability)

    ''' <summary>
    ''' Graba o Actualiza una Discapacidad
    ''' </summary>
    ''' <param name="disability">Discapacidad</param>
    ''' <param name="audit">Objeto de Auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function SaveDisability(ByVal disability As Disability, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene una Discapacidad especifica
    ''' </summary>
    ''' <param name="code">Codigo Discapacidad</param>
    ''' <returns>Discapacidad</returns>
    ''' <remarks></remarks>
    Function GetDisability(ByVal code As String) As Disability

End Interface
