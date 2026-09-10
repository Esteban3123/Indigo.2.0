#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IPharmaceuticalDispensingTransferRepository
    Inherits IRepository(Of PharmaceuticalDispensingTransfer)

    ''' <summary>
    ''' Obtiene el traslado por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingTransferById(id As Integer) As PharmaceuticalDispensingTransfer

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmaceuticalDispensingTransferByCode(code As String) As PharmaceuticalDispensingTransfer

    ''' <summary>
    ''' Guarda, Actualiza o Confirma un traslado
    ''' </summary>
    ''' <param name="pharmaceuticalDispensingTransferXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SavePharmaceuticalDispensingTransfer(pharmaceuticalDispensingTransferXml As String, userCode As String) As SP_SavePharmaceuticalDispensingTransfer_Result

End Interface
