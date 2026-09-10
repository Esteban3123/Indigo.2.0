'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IOutstandingChecksRepository
    Inherits IRepository(Of OutstandingChecks)

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <returns></returns>
    Function GetOutstandingChecksById(ByVal Id As Integer) As OutstandingChecks

    ''' <summary>
    ''' Obtiene un cheque pendiente por id de la chequera y numero del cheque
    ''' </summary>
    ''' <returns></returns>
    Function GetOutstandingCheckByCheckBookIdAndCheckNumber(ByVal checkBookId As Integer, ByVal checkNumber As Long) As OutstandingChecks

    ''' <summary>
    ''' Obtiene el primer cheque que esta en espera por id de chequera
    ''' </summary>
    ''' <returns></returns>
    Function GetFirstOutstandingChecks(ByVal IdCheckBook As Integer) As OutstandingChecks

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    Function ListOutstandingChecksByIdCheckBook(ByVal IdCheckBook As Integer) As List(Of OutstandingChecks)

End Interface
