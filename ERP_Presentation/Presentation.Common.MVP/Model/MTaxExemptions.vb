'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Andres Alarcon
' Created          : 26/08/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class MTaxExemptions
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtiene una actividad economica
    ''' </summary>
    ''' <returns>Ciudad</returns>
    Public Async Function GetTaxExemptions(ByVal code As String) As Task(Of ActionResult(Of Domain.Entities.TaxExemptions))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTaxExemptionsAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una exoneracion tributaria
    ''' </summary>
    ''' <param name="TaxExemptions"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveTaxExemptions(ByVal TaxExemptions As Domain.Entities.TaxExemptions) As Task(Of ActionResult(Of Domain.Entities.TaxExemptions))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveTaxExemptionsAsync(TaxExemptions, Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.TaxExemptions))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ChangeStateTaxExemptionsAsync(code, state, Indigo)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
