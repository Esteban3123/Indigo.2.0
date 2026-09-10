'***********************************************************************
' Assembly         : Domain.Seedwork
' Author           : Juan F. Tamayo
' Created          : 2014-01-15
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' define los servicios disponibles para todas las operaciones
''' con la entidad clase contable
''' </summary>
Public Interface IAccountClassAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene una clase contable
    ''' </summary>
    ''' <param name="code">Código de la clase contable</param>
    ''' <returns>Clase contable</returns>
    Function GetAccountClassByCode(ByVal code As String, Optional Tracking As Boolean = True) As MainAccountClasses

    ''' <summary>
    ''' Obtiene una clase contable
    ''' </summary>
    ''' <returns>Clase contable</returns>
    Function GetAccountClassById(ByVal id As Integer, Optional Tracking As Boolean = True) As MainAccountClasses

    ''' <summary>
    ''' Graba una una clase contable
    ''' </summary>
    ''' <param name="doc">Clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function SaveAccountClass(ByVal doc As MainAccountClasses, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of MainAccountClasses)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateAccountClass(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of MainAccountClasses)

    ''' <summary>
    ''' Elimina una clase contable
    ''' </summary>
    ''' <param name="doc">Clase contable</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function DeleteAccountClass(ByVal doc As MainAccountClasses, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Gets all account class.
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAccountClass() As List(Of MainAccountClasses)
#End Region

End Interface
