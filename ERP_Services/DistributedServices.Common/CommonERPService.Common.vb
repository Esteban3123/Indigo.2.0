'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 17-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Common
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Class CommonERPService

    ''' <summary>
    ''' Metodo para traer los campos null
    ''' </summary>
    ''' <param name="TableName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFieldsNULLUsers(TableName As String, session As SessionValues) As DataSet Implements ICommonERPService.GetFieldsNULL
        Using AdminCommon As ICommonAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICommonAdminService)()
            Return AdminCommon.ConsultarCamposNULL(TableName)
        End Using
    End Function

End Class
