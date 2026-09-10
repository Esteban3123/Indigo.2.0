
Imports Domain.Base
Imports Domain.Entities
Public Interface ISupplierBankAccountRepository
    Inherits IRepository(Of SupplierBankAccount)

    ''' <summary>
    ''' Obtiene las cuentas bancarias del proveedor
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSuppliersBankAccountByIdSupplier(id As Integer, Optional tracking As Boolean = True) As List(Of SupplierBankAccount)

End Interface
