'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2023-03-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceLiquidationData


    ''' <summary>
    ''' Obtiene una justificacion control especifico
    ''' </summary>
    ''' <param name="code">Codigo de la justificacion control</param>
    ''' <returns>Justificacion control</returns>
    <OperationContract()>
    Function GetLiquidationDataByAdmissionNumber(ByVal _admissionNumber As String, ByVal audit As AuditMessage) As ActionResult(Of LiquidationData)

    '''''' <summary>
    ''' graba una justificacion
    ''' </summary>
    ''' <param name="justificationControl">justificacion control</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveLiquidationData(ByVal _liquidationData As LiquidationData, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of LiquidationData)

End Interface
