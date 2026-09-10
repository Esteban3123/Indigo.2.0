'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
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
''' con la entidad GeneralLedgerIVA
''' </summary>
Public Interface IGeneralLedgerIVAAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' obtiene el iva por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGeneralLedgerIVAByCode(code As String, ByVal audit As AuditMessage) As GeneralLedgerIVA

    ''' <summary>
    ''' obtiene el iva por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGeneralLedgerIVAById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Guarda o actualiza un IVA
    ''' </summary>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveGeneralLedgerIVA(ByVal glIVA As GeneralLedgerIVA, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Actualiza el Estado de un IVA
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateGeneralLedgerIVA(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of GeneralLedgerIVA)

    ''' <summary>
    ''' Elimina un iva
    ''' </summary>
    ''' <param name="doc">Iva a eliminar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function DeleteGeneralLedgerIVA(ByVal doc As GeneralLedgerIVA, ByVal audit As AuditMessage) As ActionResult

#End Region

End Interface
