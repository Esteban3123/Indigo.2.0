'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IJobBondingTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns>Tipos de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Function ListAllJobBondingType() As List(Of JobBondingType)

    ''' <summary>
    ''' Elimina un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteJobBondingType(ByVal jobBondingType As JobBondingType, ByVal audit As AuditMessage) As ActionMessageResult(Of JobBondingType)

    ''' <summary>
    ''' Almacena o Actualiza un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="jobBondingType">Tipo de Vinculación Laboral</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveJobBondingType(ByVal jobBondingType As JobBondingType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="code">Código del Tipo Vinculación Laboral</param>
    ''' <returns>Tipo de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Function GetJobBondingType(ByVal code As String) As JobBondingType

End Interface
