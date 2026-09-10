#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ITransferOrderDevolutionRepository
    Inherits IRepository(Of TransferOrderDevolution)
    Inherits IRepositoryRollbackStrategy

    ''' <summary>
    ''' Obtiene una devolución de orden de traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransferOrderDevolutionById(id As Integer) As TransferOrderDevolution

    ''' <summary>
    ''' Obtiene una devolucion de orden de traslado
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTransferOrderDevolutionByCode(code As String) As TransferOrderDevolution

    ''' <summary>
    ''' Guarda, Actualiza o Confirma una devolución de orden de traslado
    ''' </summary>
    ''' <param name="transferOrderDevolutionXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveTransferOrderDevolution(transferOrderDevolutionXml As String, userCode As String) As SP_SaveTransferOrderDevolution_Result

End Interface
