Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IBankFileDetailRepository
    Inherits IRepository(Of BankFileDetail)

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankFileDetailByBankFileId(id As Integer) As List(Of BankFileDetail)

End Interface
