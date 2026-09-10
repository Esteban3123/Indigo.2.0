'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IKinshipRepository
    Inherits IRepository(Of Kinship)

    ''' <summary>
    ''' Obtiene todos los parentescos
    ''' </summary>
    ''' <returns>Lista de parentescos</returns>
    ''' <remarks></remarks>
    Function ListAllKinship() As List(Of Kinship)

    ''' <summary>
    ''' Obtiene un parentesco especifico
    ''' </summary>
    ''' <param name="code">Codigo del parentesco</param>
    ''' <returns>Parentesco</returns>
    ''' <remarks></remarks>
    Function GetKinship(ByVal code As String, Optional tracking As Boolean = True) As Kinship

End Interface
