Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IFunctionalUnitResponsibleRepository

    Inherits IRepository(Of FunctionalUnitResponsible)

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListFunctionalUnitResponibleByUserId(UserID As Integer) As List(Of FunctionalUnitResponsible)

End Interface
