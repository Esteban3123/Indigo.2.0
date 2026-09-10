'************************************************************
' Assembly         : Domain.Payments
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 2021-01-14
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IDocumentSupportRepository
    Inherits IRepository(Of DocumentSupport)

    ''' <summary>
    ''' Lista todas las resoluciones de facturación autorizadas
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListDocumentSupportByUserCode(ByVal userCode As String) As List(Of DocumentSupport)

    ''' <summary>
    ''' Obtiene una autorización de factura por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetDocumentSupportById(ByVal Id As Integer, Optional tracking As Boolean = True) As DocumentSupport

    ''' <summary>
    ''' Obtiene una autorización de factura por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetDocumentSupportByCode(ByVal code As String) As DocumentSupport

End Interface
