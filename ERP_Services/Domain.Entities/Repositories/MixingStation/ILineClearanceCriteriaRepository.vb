'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Andres Alarcon
' Created          : 21-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Public Interface ILineClearanceCriteriaRepository
    Inherits IRepository(Of LineClearanceCriteria)

    ''' <summary>
    ''' Obtiene un criterio de despeje de linea mediante el codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetLineClearanceCriteriaByCode(code As String, Optional tracking As Boolean = True) As LineClearanceCriteria

End Interface
