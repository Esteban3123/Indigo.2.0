'************************************************************
' Assembly         : Domain.Common
' Author           : Juan Diego Diaz
' Created          : 23-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Common.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio Consecutivos.
''' </summary>
Public Interface IConsecutiveRepository
    Inherits IRepository(Of Consecutive)

    ''' <summary>
    ''' Función que obtiene una lista de consecutivos.
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllConsecutives() As List(Of Consecutive)
    ''' <summary>
    ''' Consulta un consecutivo especifico segun código.
    ''' </summary>
    ''' <param name="code">el código del consecutivo</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetConsecutiveByCode(ByVal code As String) As Consecutive


End Interface
