'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 07-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.Text

#End Region

Public Class MNationalSavingsFund
    Inherits ModelBase
    Implements IDisposable

#Region "Construct"

    Shared TAG As String = ""

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Devuelve armado el archivo de banco, por codigo de archivo de banco, y todas las liquidaciones
    ''' </summary>
    ''' <param name="PayrollDateLiquidation">Fecha de la Liquidación de Nómina</param>
    ''' <param name="CompanyId">Id Compañía</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateNationalSavingsFundAsync(PayrollDateLiquidation As Date, ByVal CompanyId As Integer) As Task(Of ActionMessageResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GenerateNationalSavingsFundFileAsync(PayrollDateLiquidation, CompanyId, Indigo)
    End Function

    ''' <summary>
    ''' Lista todas las compañias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllCompany() As Task(Of List(Of Company))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllCompanyAsync(Indigo)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
