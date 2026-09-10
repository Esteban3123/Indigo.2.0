'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27-06-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICheckBlockRepository
    Inherits IRepository(Of CheckBlock)

    ''' <summary>
    ''' Obtener un registro de cheque bloqueado
    ''' </summary>
    ''' <returns></returns>
    Function GetCheckBlockById(ByVal Id As Integer) As CheckBlock

    ''' <summary>
    ''' Obtiene un cheque bloqueado por el id de la chequera y numero del cheque
    ''' </summary>
    ''' <returns></returns>
    Function GetCheckBlockByIdCheckBookAndNumber(ByVal IdCheckBook As Integer, ByVal checkNumber As Long) As CheckBlock

    Function ListCheckByCheckbookId(checkbookid As Integer) As List(Of CheckBlock)

End Interface
