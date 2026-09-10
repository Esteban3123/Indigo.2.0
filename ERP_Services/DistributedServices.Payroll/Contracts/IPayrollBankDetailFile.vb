Imports Domain.Base.Entities
Imports System.Text
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()> _
Public Interface IPayrollBankFileDetail

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBankFileDetailByBankFileId(id As Integer, session As SessionValues) As List(Of BankFileDetail)
    
End Interface
