Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IPositionRollRepository
    Inherits IRepository(Of PositionRoll)

    ''' <summary>
    ''' obtiene el detalle de un archivo plano de bancos por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListPositionRollByRole(RoleID As Integer) As List(Of PositionRoll)

    ''' <summary>
    ''' Obtiene el Listado de PositionRoll
    ''' </summary>
    ''' <param name="RoleCode"></param>
    ''' <returns></returns>
    Function GetListPositionRollByRoleCode(RoleCode As String) As List(Of PositionRoll)
End Interface
