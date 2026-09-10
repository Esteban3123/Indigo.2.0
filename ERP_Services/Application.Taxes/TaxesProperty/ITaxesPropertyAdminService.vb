'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ITaxesPropertyAdminService
    Inherits IDisposable

    Function SaveLoadPlaneCollection(data As List(Of String), audit As AuditMessage) As ActionResult

    Function ValidateLoadPlaneCollection(data As List(Of String)) As ActionResult(Of List(Of Tuple(Of Integer, String, String, String)))

    Function GetAllTaxedProperties() As List(Of TaxesProperty)

    ''' <summary>
    ''' Busca una TaxesProperty atraves de su code
    ''' </summary>
    ''' <param name="code">Code del TaxesProperty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTaxesPropertyByCode(code As String) As TaxesProperty
End Interface
