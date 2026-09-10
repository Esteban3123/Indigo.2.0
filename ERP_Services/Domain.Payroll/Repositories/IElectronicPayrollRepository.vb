Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IElectronicPayrollRepository
    Inherits IRepository(Of ElectronicPayroll)

    ''' <summary>
    ''' Obtiene un documento electronico usado para la facturación electronica por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicPayrollById(ByVal Id As Integer, Optional tracking As Boolean = True) As ElectronicPayroll

    ''' <summary>
    ''' Función que obtiene un documento electrónico por el número de documento
    ''' </summary>
    ''' <param name="documentNumber"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetElectronicPayrollByDocumentNumber(ByVal documentNumber As Integer, Optional tracking As Boolean = True) As ElectronicPayroll

End Interface
