'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IAccountReceivableShareAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene la cuota de la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivableShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableShareById(idAccountReceivableShare As Integer) As AccountReceivableShare
    ''' <summary>
    ''' guarda una cuota
    ''' </summary>
    ''' <param name="AccountReceivableShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAccountReceivableShare(AccountReceivableShare As AccountReceivableShare, audit As AuditMessage) As ActionResult(Of AccountReceivableShare)

End Interface
