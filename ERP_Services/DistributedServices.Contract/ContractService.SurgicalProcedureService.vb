'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Contract
Imports Microsoft.Practices.Unity

Partial Class ContractService
    Public Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of Domain.Entities.SurgicalProcedureService) Implements IContractServiceSurgicalProcedureService.GetSurgicalProcedureServiceByIPSServiceId
        Using service As ISurgicalProcedureServiceAdminService = Container.Current.Resolve(Of ISurgicalProcedureServiceAdminService)()
            Return service.GetSurgicalProcedureServiceByIPSServiceId(idIPSService)
        End Using
        'Return _surgicalProcedureServiceAdminService.GetSurgicalProcedureServiceByIPSServiceId(idIPSService)
    End Function
End Class
