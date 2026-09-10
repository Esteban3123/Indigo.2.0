'***********************************************************************
' Assembly         : Domain.Common
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Public Interface ITaxExemptionsRepository
    Inherits IRepository(Of TaxExemptions)

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por id
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTaxExemptionsById(id As Integer, Optional tracking As Boolean = True) As TaxExemptions

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por codigo
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTaxExemptions(code As String, Optional tracking As Boolean = True) As TaxExemptions

End Interface
