'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface INPTConfigurationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los registros
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Function ListAllNPTConfiguration(ByVal audit As AuditMessage) As List(Of NPTConfiguration)

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="NPTConfiguration">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveNPTConfiguration(ByVal NPTConfiguration As NPTConfiguration, ByVal audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration))

    ''' <summary>
    ''' elimina detalles
    ''' </summary>
    ''' <param name="ListNPTConfiguration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function DeleteNPTConfiguration(ByVal ListNPTConfiguration As List(Of NPTConfiguration), audit As AuditMessage) As ActionResult(Of List(Of NPTConfiguration))




End Interface