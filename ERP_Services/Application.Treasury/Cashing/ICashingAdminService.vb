'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 28-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICashingAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro de cambio de cheque
    ''' </summary>
    Function SaveCashing(ByVal checkCashing As CheckCashing, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CheckCashing)

    ''' <summary>
    ''' Elimina un registro de cambio de cheque
    ''' </summary>
    Function DeleteCashing(ByVal checkCashing As CheckCashing, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCashing(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CheckCashing)

    ''' <summary>
    ''' Obtiene un registro de cambio de cheque por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCashingById(id As Integer, tracking As Boolean) As CheckCashing

End Interface