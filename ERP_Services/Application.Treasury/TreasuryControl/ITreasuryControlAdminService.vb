'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ITreasuryControlAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <param name="treasuryControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveTreasuryControl(ByVal treasuryControl As TreasuryControl, ByVal audit As AuditMessage, Optional ByVal witCommit As Boolean = True) As ActionResult(Of TreasuryControl)

    ''' <summary>
    ''' Elimina un registro de control de los documentos de tesoreria
    ''' </summary>
    ''' <param name="treasuryControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteTreasuryControl(ByVal treasuryControl As TreasuryControl, ByVal audit As AuditMessage, Optional ByVal witCommit As Boolean = True) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de tesoreria por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTreasuryControlById(Id As Integer) As TreasuryControl

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetTreasuryControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As TreasuryControl

End Interface