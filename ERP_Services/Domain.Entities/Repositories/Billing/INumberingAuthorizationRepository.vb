'************************************************************
' Assembly         : Domain.Billing
' Author           : Andres Alarcon
' Created          : 2022-08-02
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface INumberingAuthorizationRepository
    Inherits IRepository(Of NumberingAuthorization)

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListNumberingAuthorizationByUserCode(ByVal userCode As String) As List(Of NumberingAuthorization)

    ''' <summary>
    ''' Obtiene una autorización de factura por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetNumberingAuthorizationById(ByVal Id As Integer, Optional tracking As Boolean = True) As NumberingAuthorization

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetNumberingAuthorizationByCode(ByVal code As String) As NumberingAuthorization

End Interface
