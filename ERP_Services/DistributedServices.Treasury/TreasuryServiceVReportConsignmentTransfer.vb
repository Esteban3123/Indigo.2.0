Imports Domain.Entities
Imports Application.Treasury
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class TreasuryService

    ''' <summary>
    ''' obtiene un traslado o consignación por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVReportConsignmentTransferByCode(code As String) As Domain.Entities.VReportConsignmentTransfer Implements ITreasuryServiceVReportConsignmentTransfer.GetVReportConsignmentTransferByCode
        Using service As IVReportConsignmentTransferAdminService = Container.Current.Resolve(Of IVReportConsignmentTransferAdminService)()
            Return service.GetVReportConsignmentTransferByCode(code)
        End Using
        'Return Me._vReportConsignmentTransfer.GetVReportConsignmentTransferByCode(code)
    End Function

End Class
