Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IElectronicPayrollPaymentSupportRepository
    Inherits IRepository(Of ElectronicPayrollPaymentSupport)

    ''' <summary>
    ''' Obtiene un soporte de pago de nomina electronica usado por el id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetElectronicPayrollPaymentSupportById(ByVal Id As Integer, Optional tracking As Boolean = True) As ElectronicPayrollPaymentSupport

    ''' <summary>
    ''' funcion para obtener el soporte de pago por tercero y periodo
    ''' </summary>
    ''' <returns></returns>
    Function GetElectronicPayrollPaymentSupportByThirdPartyAndPeriod(ByVal thirdPartyId As Integer, ByVal year As Integer, ByVal month As Byte) As ElectronicPayrollPaymentSupport

    ''' <summary>
    ''' Obtiene informacion de un soporte de pago de nomina electronica
    ''' </summary>
    ''' <param name="id">id del soporte de pago de nomina electronica</param>
    ''' <returns>Factura</returns>
    Function GetElectronicPayrollPaymentSupport(ByVal id As Integer) As SP_GetElectronicPayrollPaymentSupport_Result

    ''' <summary>
    ''' Obtiene los detalles de un soporte de pago de nomina electronica
    ''' </summary>
    ''' <param name="id">id del soporte de pago de nomina electronica</param>
    ''' <returns>Factura</returns>
    Function GetElectronicPayrollPaymentSupportDetails(ByVal id As Integer) As List(Of SP_GetElectronicPayrollPaymentSupportDetails_Result)

End Interface
