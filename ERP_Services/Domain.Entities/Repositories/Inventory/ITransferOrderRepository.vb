#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ITransferOrderRepository
    Inherits IRepository(Of TransferOrder)
    Inherits IRepositoryRollbackStrategy

    ''' <summary>
    ''' Obtiene una orden de traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransferOrderById(id As Integer) As TransferOrder

    ''' <summary>
    ''' Obtiene una orden de traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransferOrderByCode(code As String) As TransferOrder

    ''' <summary>
    ''' Guarda, Actualiza o Confirma una orden de traslado
    ''' </summary>
    ''' <param name="transferOrderXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveTransferOrder(transferOrderXml As String, userCode As String) As SP_SaveTransferOrder_Result

End Interface
