'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2023-03-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Obtiene los datos de liquidacion
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLiquidationDataByAdmissionNumber(ByVal _admissionNumber As String, ByVal audit As AuditMessage) As ActionResult(Of LiquidationData) Implements IBillingServiceLiquidationData.GetLiquidationDataByAdmissionNumber
        Using service As ILiquidationDataAdminService = Container.Current.Resolve(Of ILiquidationDataAdminService)()
            Return service.GetLiquidationDataByAdmissionNumber(_admissionNumber, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda los datos de liquidacion
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveLiquidationData(ByVal _liquidationData As LiquidationData, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of LiquidationData) Implements IBillingServiceLiquidationData.SaveLiquidationData
        Using service As ILiquidationDataAdminService = Container.Current.Resolve(Of ILiquidationDataAdminService)()
            Return service.SaveLiquidationData(_liquidationData, audit)
        End Using
    End Function

End Class
