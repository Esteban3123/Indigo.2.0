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
''' con la entidad tipo de documento
''' </summary>
Public Interface IDocumentTypeAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="code">Código del documento a consultar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Tipo de documento consultado</returns>
    Function GetDocumentType(ByVal code As String, ByVal audit As AuditMessage) As Task(Of JournalVoucherTypes)

    ''' <summary>
    ''' Graba un tipo de documento
    ''' </summary>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function SaveDocumentType(ByVal documentType As JournalVoucherTypes, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As Task(Of ActionResult(Of JournalVoucherTypes))

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateDocumentType(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As Task(Of ActionResult(Of JournalVoucherTypes))

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Function DeleteDocumentType(ByVal doc As JournalVoucherTypes, ByVal audit As AuditMessage) As Task(Of ActionResult)

    ''' <summary>
    ''' Gets the journal voucher by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetJournalVoucherById(ByVal id As Long, ByVal audit As AuditMessage) As ActionResult(Of JournalVoucherTypes)

#End Region

End Interface
