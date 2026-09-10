'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IAccountReceivableShareRepository
    Inherits IRepository(Of AccountReceivableShare)

    ''' <summary>
    ''' obtener la cuota de la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivableShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableShareById(idAccountReceivableShare As Integer) As AccountReceivableShare
    ''' <summary>
    ''' metodo para obtener las cuotas de una factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="idThirdParty"></param>
    ''' <param name="idMainAccount"></param>
    ''' <param name="idCostCenter"></param>
    ''' <param name="balance"></param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableShareByInvoiceNumber(invoiceNumber As String, idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer, Optional balance As Decimal = 0, Optional status As Integer = 2) As List(Of AccountReceivableShare)
    ''' <summary>
    ''' Obtiene la primer  cuota de una factura
    ''' </summary>
    ''' <param name="_AccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccountReceivableSharebyAccountId(ByVal _AccountReceivableId As Integer) As AccountReceivableShare
End Interface
