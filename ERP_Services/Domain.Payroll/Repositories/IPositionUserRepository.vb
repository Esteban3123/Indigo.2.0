Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IPositionUserRepository
    Inherits IRepository(Of PositionUser)

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListPositionUserByUserId(UserID As Integer) As List(Of PositionUser)
End Interface
