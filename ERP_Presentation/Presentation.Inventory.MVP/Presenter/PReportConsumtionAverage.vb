'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Johan Sebastian Carranza Ramos
' Created          : 01/10/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class PReportConsumtionAverage
    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub
    ''' <summary>
    ''' Funcion para consultar el Store procedure guardado en BD para traer el estadistico de ingresos.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function LoadDataSourceEntranceAverage(parameters As String, ByVal session As SessionValues) As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListAverageEntranceOrderAsync(parameters, session)
    End Function
End Class
