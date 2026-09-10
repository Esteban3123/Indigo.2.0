'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities

Public Interface INationalSavingsFundDomain
    Inherits IDisposable

    ''' <summary>
    ''' Genera el archivo plano para Bancolombia
    ''' </summary>
    ''' <param name="PayrollLiquidation">liquidaciones de nomina</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchive(PayrollLiquidation As List(Of Liquidation), PayrollLiquidationAcumulated As List(Of Liquidation)) As StringBuilder
End Interface
