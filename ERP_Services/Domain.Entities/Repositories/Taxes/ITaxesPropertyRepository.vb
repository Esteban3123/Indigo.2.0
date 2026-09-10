'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities


Public Interface ITaxesPropertyRepository
    Inherits IRepository(Of TaxesProperty)

    Function SaveLoadPlaneCollection(xml As String, user As String) As Entity.Core.Objects.ObjectResult(Of SP_SaveLoadPlaneCollection_Result)


    Function ValidateLoadPlaneCollection(xml As String) As Entity.Core.Objects.ObjectResult(Of SP_ValidateLoadPlaneCollection_Result)


    Function GetAllTaxedProperties() As List(Of TaxesProperty)

    ''' <summary>
    ''' Busca un taxesProperty atraves de su code
    ''' </summary>
    ''' <param name="code">Code del taxesProperty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTaxesPropertyByCode(code As String, Optional tracking As Boolean = True) As TaxesProperty
End Interface
