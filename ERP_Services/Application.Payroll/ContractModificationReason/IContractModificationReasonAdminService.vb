'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IContractModificationReasonAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteContractModificationReason(ByVal contractModificationReason As ContractModificationReason, ByVal audit As AuditMessage) As ActionMessageResult(Of ContractModificationReason)

    ''' <summary>
    ''' Almacena o Actualiza una razon de otro si
    ''' </summary>
    ''' <param name="contractModificationReason">Razon de otro si</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveContractModificationReason(ByVal contractModificationReason As ContractModificationReason, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene una razon de otro si por codigo
    ''' </summary>
    ''' <returns>razones de otro si</returns>
    ''' <remarks></remarks>
    Function GetContractModificationReason(ByVal code As String) As ContractModificationReason

End Interface
