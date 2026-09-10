'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Andres Alarcon
' Created          : 18-05-2023
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
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class MProductAndServiceFee
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' metodo para copiar y pegar en la rejilla
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetProductFeeDetailFromCopyandPaste(DataImport As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of ProductFeeDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SetProductFeeDetailFromCopyandPasteAsync(DataImport)
    End Function

    ''' <summary>
    ''' metodo para copiar y pegar en la rejilla
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetServicesFeeDetailFromCopyandPaste(DataImport As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of ServiceFeeDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SetServiceFeeDetailFromCopyandPasteAsync(DataImport)
    End Function

    ''' <summary>
    ''' metodo para importar un archivo de excel
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetProductFeeDetailFromFile(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of ProductFeeDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SetProductFeeDetailFromFile(DataImport)
    End Function

    ''' <summary>
    ''' metodo para importar un archivo de excel
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetServicesFeeDetailFromFile(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of ServiceFeeDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SetServiceFeeDetailFromFile(DataImport)
    End Function

    ''' <summary>
    ''' Obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    Public Async Function GetProductAndServiceFeeByCode(ByVal code As String) As Task(Of ProductAndServiceFee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetProductAndServiceFeeByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    Public Async Function GetProductAndServiceFeeById(ByVal Id As Integer) As Task(Of ProductAndServiceFee)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetProductAndServiceFeeByIdAsync(Id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Actualiza el estado de un registro de tarifas de productos y servicios
    ''' </summary>
    Public Async Function UpdateStateProductAndServiceFee(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ProductAndServiceFee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.UpdateStateProductAndServiceFeeAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un registro de tarifas de productos y servicios
    ''' </summary>
    Public Async Function SaveProductAndServiceFee(ByVal ProductAndServiceFee As ProductAndServiceFee) As Task(Of ActionResult(Of ProductAndServiceFee))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveProductAndServiceFeeAsync(ProductAndServiceFee, Me.Indigo.AuditMessageWcf)
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