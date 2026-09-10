Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IVReportConsignmentTransferAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene un traslado o consignacion por codigo
    ''' </summary>
    ''' <param name="Code">codigo del traslado o consignación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetVReportConsignmentTransferByCode(Code As String) As VReportConsignmentTransfer

End Interface
