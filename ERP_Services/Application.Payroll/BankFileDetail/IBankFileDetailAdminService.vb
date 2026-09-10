Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Text

Public Interface IBankFileDetailAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por Id
    ''' por código de usuario
    ''' </summary>
    ''' <param name="id">Identificador del registro</param>
    ''' <returns></returns>
    Function GetBankFileDetailByBankFileId(id As Integer) As List(Of BankFileDetail)

End Interface
