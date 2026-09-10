'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports System.Text

Public Interface INationalSavingsFundAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Genera el Archivo Plano del Fondo Nacional del Ahorro
    ''' </summary>
    ''' <param name="PayrollDateLiquidated">Fecha de Liquidación de Nómina</param>
    ''' <param name="CompanyId">Id Company</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateNationalSavingsFundFile(PayrollDateLiquidated As Date, CompanyId As Integer, audit As Infrastructure.CrossCutting.Base.SessionValues) As ActionMessageResult(Of StringBuilder)
End Interface
