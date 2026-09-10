' ***********************************************************************
' Assembly         : Presentation.Reporter
' Author           : Juan Diego Diaz
' Created          : 2014-01-03
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

Imports System.Globalization
Imports DevExpress.XtraReports.UI
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
''' <summary>
''' Clase Utilidades
''' </summary>
Public Class UtilitiesReporter

    ''' <summary>
    ''' Estructura con parametros para Devoluciones Cabecera
    ''' </summary>
    Public Structure ParametrosReporteDevolutions
        Dim INDDocumentNumber As String
        Dim INDRadicatedConsecutive As String
    End Structure

    ''' <summary>
    ''' Estructura con parametros para Responsables
    ''' </summary>
    Public Structure ParametrosReporteResponsibles
        Dim CodeERP As String
    End Structure

    ''' <summary>
    ''' Inicializa la localización del reporte usando el método nativo ApplyLocalization de XtraReport
    ''' </summary>
    ''' <param name="report">Instancia del reporte al que se aplicará la localización</param>
    Public Shared Sub InitializeReportLocalization(report As XtraReport, session As SessionValues, optionModule As Integer)
        If optionModule = 1 Then ''Payroll
            Dim companySettings As PayrollSettingsXpo = XpoServiceEx.Instance(session.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
            If companySettings IsNot Nothing Then
                Dim culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
                culture.NumberFormat = companySettings.CurrencyId.Abbreviation.GetNumberFormat()
                report.ApplyLocalization(culture)
            End If
        End If
    End Sub

End Class


