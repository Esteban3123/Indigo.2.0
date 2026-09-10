'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IBankRepository
    Inherits IRepository(Of Bank)

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllBank() As List(Of Bank)

    ''' <summary>
    ''' Obtiene un Banco especifico
    ''' </summary>
    ''' <param name="code">Codigo del Banco</param>
    ''' <returns>Banco</returns>
    ''' <remarks></remarks>
    Function GetBank(ByVal code As String, Optional tracking As Boolean = True) As Bank

    ''' <summary>
    ''' Obtiene un banco por el identificador
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetBankById(ByVal Id As Integer, Optional tracking As Boolean = True) As Bank

End Interface
