'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IContributorTypeRepository
    Inherits IRepository(Of ContributorType)

    ''' <summary>
    ''' Lista todos los tipos contribuyentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllContributorType() As List(Of ContributorType)

    ''' <summary>
    ''' Obtiene un tipo contribuyente
    ''' </summary>
    ''' <param name="code">Codigo del tipo contribuyente</param>
    ''' <returns>Tipo Contribuyente</returns>
    ''' <remarks></remarks>
    Function GetContributorType(ByVal code As String, Optional desatach As Boolean = True) As ContributorType

End Interface
