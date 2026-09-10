Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface ICommonERPCommon

    ''' <summary>
    ''' consulta los campos NULL para customizacion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFieldsNULL(TableName As String, session As SessionValues) As DataSet


End Interface

