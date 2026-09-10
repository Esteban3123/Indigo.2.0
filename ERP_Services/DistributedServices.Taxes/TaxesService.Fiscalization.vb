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

    Public Function SP_ValidateTaxBase(ListData As List(Of String), Year As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.SP_ValidateTaxBase_Result)) Implements ITaxesServiceFiscalization.SP_ValidateTaxBase
        Using service As IFiscalizationAdminService = Container.Current.Resolve(Of IFiscalizationAdminService)()
            Return service.SP_ValidateTaxBase(ListData, Year)
        End Using
        'Return _fiscalizationAdminService.SP_ValidateTaxBase(ListData, Year)
    End Function

End Class
