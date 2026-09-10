'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IBillingControlAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro de control de los documentos de facturacion
    ''' </summary>
    ''' <param name="billingControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveBillingControl(ByVal billingControl As BillingControl, ByVal audit As AuditMessage, Optional ByVal witCommit As Boolean = True) As ActionResult(Of BillingControl)

    ''' <summary>
    ''' Elimina un registro de control de los documentos de facturacion
    ''' </summary>
    ''' <param name="billingControl">The treasury control.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteBillingControl(ByVal billingControl As BillingControl, ByVal audit As AuditMessage, Optional ByVal witCommit As Boolean = True) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de facturacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingControlById(Id As Integer) As BillingControl

    ''' <summary>
    ''' Obtiene un registro de control de facturacion por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetBillingControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As BillingControl

End Interface
