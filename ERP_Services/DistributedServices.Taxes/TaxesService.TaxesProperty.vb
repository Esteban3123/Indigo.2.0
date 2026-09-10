'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Taxes
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class TaxesService
    Public Function ValidateLoadPlaneCollection(data As List(Of String)) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of Integer, String, String, String))) Implements ITaxesServiceTaxesProperty.ValidateLoadPlaneCollection
        Using service As ITaxesPropertyAdminService = Container.Current.Resolve(Of ITaxesPropertyAdminService)()
            Return service.ValidateLoadPlaneCollection(data)
        End Using
        'Return _taxesPropertyAdminService.ValidateLoadPlaneCollection(data)
    End Function

    Public Function SaveLoadPlaneCollection(data As List(Of String), audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements ITaxesServiceTaxesProperty.SaveLoadPlaneCollection
        Using service As ITaxesPropertyAdminService = Container.Current.Resolve(Of ITaxesPropertyAdminService)()
            Return service.SaveLoadPlaneCollection(data, audit)
        End Using
        'Return _taxesPropertyAdminService.SaveLoadPlaneCollection(data, audit)
    End Function

    Public Function GetAllTaxedProperties() As List(Of Domain.Entities.TaxesProperty) Implements ITaxesServiceTaxesProperty.GetAllTaxedProperties
        Using service As ITaxesPropertyAdminService = Container.Current.Resolve(Of ITaxesPropertyAdminService)()
            Return service.GetAllTaxedProperties()
        End Using
        'Return _taxesPropertyAdminService.GetAllTaxedProperties()
    End Function

    ''' <summary>
    ''' Busca un TaxesProperty atraves de su code
    ''' </summary>
    ''' <param name="code">Code del TaxesProperty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxesPropertyByCode(code As String, session As SessionValues) As Domain.Entities.TaxesProperty Implements ITaxesServiceTaxesProperty.GetTaxesPropertyByCode
        Using service As ITaxesPropertyAdminService = Container.Current.Resolve(Of ITaxesPropertyAdminService)()
            Return service.GetTaxesPropertyByCode(code)
        End Using
        'Return _taxesPropertyAdminService.GetTaxesPropertyByCode(code)
    End Function
End Class
