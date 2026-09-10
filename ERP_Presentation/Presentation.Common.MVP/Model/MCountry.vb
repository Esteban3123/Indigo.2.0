'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 10-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Base
Imports Domain.Base.Entities
#End Region

''' <summary>
''' Model de conexion con los servicios distribuidos de Pais
''' </summary>
Public Class MCountry
    Inherits ModelBase
    Implements IDisposable

   

    ''' <summary>
    ''' Contiene el tag del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared TAG As String = "511"


    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="TAG">tag del formulario</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal TAG As String)
        MyBase.New(TAG)
    End Sub
    ''' <summary>
    ''' Obtiene el pais por codigo
    ''' </summary>
    ''' <param name="code">Codigo del pais</param>
    ''' <returns>Country</returns>
    Public Function GetCountry(ByVal code As String) As Country
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCountry(code, indigo)
    End Function

    ''' <summary>
    ''' Obtiene el pais por codigo asincrono
    ''' </summary>
    ''' <param name="code">Codigo del pais</param>
    ''' <returns>Country</returns>
    Public Async Function GetCountryAsync(ByVal code As String) As Task(Of Country)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCountryAsync(code, indigo)
    End Function

    ''' <summary>
    ''' Guarda el pais
    ''' </summary>
    ''' <param name="country">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Function SaveCountry(ByVal country As Country, idSequence As Long) As ActionResult(Of Country)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveCountry(country, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Guarda el pais asincrono
    ''' </summary>
    ''' <param name="country">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Async Function SaveCountryAsync(ByVal country As Country, idSequence As Long) As Task(Of ActionResult(Of Country))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveCountryAsync(country, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Borra el pais seleccionado
    ''' </summary>
    ''' <param name="country">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Function DeleteCountry(ByVal country As Country) As ActionMessageResult(Of Country)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteCountry(country, Indigo)
    End Function

    ''' <summary>
    ''' Borra el pais seleccionado Asincrono
    ''' </summary>
    ''' <param name="country">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Async Function DeleteCountryAsync(ByVal country As Country) As Task(Of ActionMessageResult(Of Country))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteCountryAsync(country, Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los paises
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Function ListAllCountries() As List(Of Country)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCountry(indigo)
    End Function

    ''' <summary>
    ''' Lista todos los paises asincrono
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Async Function ListAllCountriesAsync() As Task(Of List(Of Country))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCountryAsync(indigo)
    End Function

    ''' <summary>
    ''' Lista todos los campos nulos
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Async Function GetFieldsNULLAsync() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Country", Me.indigo)
    End Function


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
