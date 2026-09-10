'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 16-09-2014
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
Imports Domain.Maintenance.Entities
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

Public Class MTrademark
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetTrademarkAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetTrademarkByCodeAsync(code, Indigo)
    End Function
    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="Trademark">Trademark</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    Public Async Function SaveTrademarkAsync(ByVal Trademark As Trademark, idSequence As Long) As Task(Of ActionResult(Of Trademark))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.SaveTrademarkAsync(Trademark, Indigo, idSequence)
    End Function
    ''' <summary>
    ''' Elimina Marca modo asincrono
    ''' </summary>
    ''' <param name="Trademark">Trademark</param>
    ''' <returns>Un valor que indica si se elimino con exito la Marca</returns>
    Public Async Function DeleteTrademarkAsync(ByVal Trademark As Trademark) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.DeleteTrademarkAsync(Trademark, Indigo)
    End Function
    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllTrademarkAsync() As Task(Of List(Of Trademark))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListAllTrademarkAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Accessory", Indigo)
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
