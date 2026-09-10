'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Jhossept K. Garay
' Created          : 19-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Imports System.Dynamic
Public Interface IConsecutiveRepository
    Inherits IRepository(Of INCONSECU)

    ''' <summary>
    ''' Obtiene el consecutivo de ingreso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsecutive(codigoConsecutivo As String) As INCONSECU

End Interface
