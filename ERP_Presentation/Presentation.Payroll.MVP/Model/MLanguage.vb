'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 25-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Realiza la conexion con los servicios del idioma
''' </summary>
Public Class MLanguage
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub


#Region "Methods"

    ''' <summary>
    ''' Obtiene el Idioma de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del Idioma</param>
    ''' <returns>el Idioma</returns>
    Public Function GetLanguage(ByVal code As String) As Language
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLanguage(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el Idioma de acuerdo al codigo de manera asincrona
    ''' </summary>
    ''' <param name="code">Codigo del Idioma</param>
    ''' <returns>El Idioma</returns>
    Public Async Function GetLanguageAsync(ByVal code As String) As Task(Of Language)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLanguageAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de Idiomas 
    ''' </summary>
    ''' <returns>Listado de Idiomas</returns>
    Public Function ListAllLanguages() As List(Of Language)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllLanguage(Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de Idiomas asincrono
    ''' </summary>
    ''' <returns>Listado de Idiomas</returns>
    Public Async Function ListAllLanguagesAsync() As Task(Of List(Of Language))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllLanguageAsync(Indigo)
    End Function

    ''' <summary>
    ''' Guarda los cambios del Idioma
    ''' </summary>
    ''' <param name="Language">lenguaje</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Function SaveLanguage(ByVal language As Language, idSequence As Long) As ActionResult(Of Language)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveLanguage(language, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Guarda los cambios del Idioma asincrono
    ''' </summary>
    ''' <param name="Language">lenguaje</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveLanguageAsync(ByVal language As Language, idSequence As Long) As Task(Of ActionResult(Of Language))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveLanguageAsync(language, Indigo, idSequence)
    End Function

    Public Async Function UpdateStateLanguageAsync(code As String, state As Boolean) As Task(Of ActionResult(Of Language))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.UpdateStateLanguageAsync(code, state, Indigo)
    End Function

    ''' <summary>
    ''' Borra el Idioma
    ''' </summary>
    ''' <param name="Language">lenguaje</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Function DeleteLanguage(ByVal language As Language) As ActionMessageResult(Of Language)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteLanguage(language, Indigo)
    End Function

    ''' <summary>
    ''' Borra el Idioma asincrono
    ''' </summary>
    ''' <param name="Language">lenguaje</param>
    ''' <returns>Si se realizo o no el borrado</returns>
    Public Async Function DeleteLanguageAsync(ByVal language As Language) As Task(Of ActionMessageResult(Of Language))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteLanguageAsync(language, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los campos nulos 
    ''' </summary>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Language", Indigo)
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
