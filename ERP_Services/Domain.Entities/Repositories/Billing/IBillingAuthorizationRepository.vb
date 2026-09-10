'************************************************************
' Assembly         : Domain.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IBillingAuthorizationRepository
    Inherits IRepository(Of BillingAuthorization)

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListBillingAuthorizationByUserCode(ByVal userCode As String) As List(Of BillingAuthorization)

    ''' <summary>
    ''' Obtiene una autorización de factura por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetBillingAuthorizationById(ByVal Id As Integer, Optional tracking As Boolean = True) As BillingAuthorization

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetBillingAuthorizationByCode(ByVal code As String) As BillingAuthorization

End Interface
