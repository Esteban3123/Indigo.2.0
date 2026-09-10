Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IPositionLevelRepository
    Inherits IRepository(Of PositionLevel)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPositionLevel() As List(Of PositionLevel)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPositionLevel(ByVal code As String, Optional tracking As Boolean = True) As PositionLevel

End Interface
