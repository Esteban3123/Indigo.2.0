'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPensionaryTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los Tipos de Pensionados
    ''' </summary>
    ''' <returns>Lista de Tipos de Pensionados</returns>
    ''' <remarks></remarks>
    Function ListAllPensionaryType() As List(Of PensionaryType)

    ''' <summary>
    ''' Obtiene un Tipo de Pensionado especifico
    ''' </summary>
    ''' <param name="code">Codigo del Tipo de Pensionado</param>
    ''' <returns>Tipo del Pensionado</returns>
    ''' <remarks></remarks>
    Function GetPensionaryType(ByVal code As String) As PensionaryType

    ''' <summary>
    ''' Graba o Actualiza un Tipo de Pensionado
    ''' </summary>
    ''' <param name="pensionaryType">Tipo de Pensionado</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SavePensionaryType(ByVal pensionaryType As PensionaryType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un Tipo de Pensionado
    ''' </summary>
    ''' <param name="pensionaryType">Tipo de Pensionado</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePensionaryType(ByVal pensionaryType As PensionaryType, ByVal audit As AuditMessage) As ActionMessageResult(Of PensionaryType)

End Interface
