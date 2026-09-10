'***********************************************************************
' Assembly         : Application.Common
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ITaxExemptionsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza la exoneracion tributaria
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveTaxExemptions(ByVal EconomicActivity As TaxExemptions, ByVal audit As AuditMessage) As ActionResult(Of TaxExemptions)

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTaxExemptionsyById(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of TaxExemptions)

    ''' <summary>
    ''' Obtiene una exoneracion tributaria por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTaxExemptions(code As String, ByVal audit As AuditMessage) As ActionResult(Of TaxExemptions)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateTaxExemptions(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TaxExemptions)

End Interface
