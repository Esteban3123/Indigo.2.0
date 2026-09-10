Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface ITreasuryServiceVReportConsignmentTransfer

    ''' <summary>
    ''' obtiene un traslado o consignación por codigo
    ''' </summary>
    ''' <param name="code">codigo del traslado o consignación</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetVReportConsignmentTransferByCode(ByVal code As String) As VReportConsignmentTransfer

End Interface
