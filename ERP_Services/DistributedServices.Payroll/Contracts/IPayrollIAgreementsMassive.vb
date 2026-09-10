'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 08/09/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

<ServiceContract()>
Public Interface IPayrollIAgreementsMassive

    <OperationContract()>
    Function SP_ImportFileAgreementsMassive(data As List(Of ImportFileRow), session As SessionValues) As ActionResult(Of List(Of SP_ImportFileAgreementsC_Result))
    <OperationContract()>
    Function SP_SaveAgreementsMassive(ListInfo As List(Of SP_ImportFileAgreementsC_Result), session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
