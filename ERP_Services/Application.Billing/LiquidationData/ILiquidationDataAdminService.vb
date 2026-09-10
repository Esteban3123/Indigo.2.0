'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2023-03-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ILiquidationDataAdminService

    Inherits IDisposable

    ''' <summary>
    ''' Guarda los datos de la liquidacion
    ''' </summary>
    ''' <param name="liquidationData">datos de liquidacion</param>
    ''' <returns></returns>
    Function SaveLiquidationData(ByVal _liquidationData As LiquidationData, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of LiquidationData)

    ''' <summary>
    ''' consulta los datos de liquidacion
    ''' </summary>
    ''' <param name="admissionNumber">Numero del ingreso</param>
    ''' <returns></returns>
    Function GetLiquidationDataByAdmissionNumber(ByVal _admissionNumber As String, ByVal audit As AuditMessage) As ActionResult(Of LiquidationData)


End Interface
