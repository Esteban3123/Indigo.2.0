'***********************************************************************
' Assembly         : DistributedService.Glosas
' Author           : Julian Cardozo
' Created          : 06-04-2013
'
' Last Modified By : Julian Cardozo
' Last Modified On : 06-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
#End Region

Partial Class PayrollService

#Region "Common"
    Public Function GetFieldsNULLUsers(TableName As String, session As SessionValues) As DataSet Implements IPayrollService.GetFieldsNULL
        Using AdminCommon As ICommonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonAdminService)()
            Return AdminCommon.ConsultarCamposNULL(TableName)
        End Using
    End Function
#End Region

End Class
