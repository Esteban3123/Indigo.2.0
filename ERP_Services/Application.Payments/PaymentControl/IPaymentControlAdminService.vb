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

Public Interface IPaymentControlAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro de control de los documentos de pagos
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePaymentControl(ByVal paymentControl As PaymentsControl, ByVal audit As AuditMessage) As ActionResult(Of PaymentsControl)

    ''' <summary>
    ''' Elimina un registro de control de los documentos de pago
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePaymentControl(ByVal paymentControl As PaymentsControl, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un registro de control de los documentos de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentControlById(Id As Integer) As PaymentsControl

    ''' <summary>
    ''' Obtiene un registro de control de pago por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetPaymentControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As PaymentsControl

End Interface