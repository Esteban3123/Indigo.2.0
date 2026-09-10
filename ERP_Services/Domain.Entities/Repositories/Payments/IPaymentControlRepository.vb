'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IPaymentControlRepository
    Inherits IRepository(Of PaymentsControl)

    ''' <summary>
    ''' Obtiene un registro de control de pagos por id
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
