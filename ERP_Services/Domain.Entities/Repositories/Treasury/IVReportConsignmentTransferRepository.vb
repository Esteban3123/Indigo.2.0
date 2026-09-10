Imports Domain.Base
Imports Domain.Entities

Public Interface IVReportConsignmentTransferRepository
    Inherits IRepository(Of VReportConsignmentTransfer)

    ''' <summary>
    ''' Obtiene un traslado o consignación por codigo
    ''' </summary>
    Function GetVReportConsignmentTransferByCode(ByVal Code As String) As VReportConsignmentTransfer

End Interface
