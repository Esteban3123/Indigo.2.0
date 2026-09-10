'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ICancellationCheckRepository
    Inherits IRepository(Of CancellationChecks)

    ''' <summary>
    ''' Obtener un registro de cheque cancelado
    ''' </summary>
    ''' <param name="IdEntityAccount">The identifier entity account.</param>
    ''' <param name="CheckNumber">The check number.</param>
    ''' <returns></returns>
    Function GetCancellationCheckByEntityAccountAndCheckNumber(ByVal IdEntityAccount As Integer, ByVal CheckNumber As String) As CancellationChecks

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    ''' <param name="checkBookId">The check book identifier.</param>
    ''' <param name="CheckNumber">The check number.</param>
    ''' <returns></returns>
    Function GetCancellationCheckByCheckBookIdAndCheckNumber(ByVal checkBookId As Integer, ByVal CheckNumber As Long) As CancellationChecks

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCancellationCheckById(ByVal id As Integer) As CancellationChecks

End Interface
