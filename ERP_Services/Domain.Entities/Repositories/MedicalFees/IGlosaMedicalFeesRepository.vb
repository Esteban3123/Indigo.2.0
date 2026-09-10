Imports Domain.Base

Public Interface IGlosaMedicalFeesRepository
    Inherits IRepository(Of GlosaMedicalFees)

    ''' <summary>
    ''' Obtiene todos los Registros
    ''' </summary>
    ''' <returns></returns>
    Function ListAllGlosaMedicalFees() As List(Of GlosaMedicalFees)

    ''' <summary>
    ''' Obtiene un GlosaMedicalFees por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGlosaMedicalFeesByCode(Code As String) As GlosaMedicalFees

    ''' <summary>
    ''' Obtiene las cuentas por pagar por Id de la tabla AccountPayable
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetAccountPayableById(Id As Integer) As AccountPayable

End Interface