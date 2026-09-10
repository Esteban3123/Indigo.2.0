'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 05-07-2013
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
''' Realiza la conexion con los servicios del grupo
''' </summary>
Public Class MPensionaryType
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub


#Region "Methods"

    ''' <summary>
    ''' Obtiene el tipo de pensionado de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del tipo de pensionado</param>
    ''' <returns>Tipo de pensionado</returns>
    Public Async Function GetPensionaryTypeAsync(ByVal code As String) As Task(Of PensionaryType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPensionaryTypeAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del tipo de pensionado asincrono
    ''' </summary>
    ''' <param name="PensionaryType">Tipo de pensionado</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SavePensionaryTypeAsync(ByVal pensionaryType As PensionaryType) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SavePensionaryTypeAsync(pensionaryType, Indigo)
    End Function


    ''' <summary>
    ''' Borra tipo de pensionado asincrono
    ''' </summary>
    ''' <param name="PensionaryType">Tipo de pensionado</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeletePensionaryTypeAsync(ByVal pensionaryType As PensionaryType) As Task(Of ActionMessageResult(Of PensionaryType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeletePensionaryTypeAsync(pensionaryType, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de tipos de pensionado asincrono
    ''' </summary>
    ''' <returns>Listado de tipos de pensionado</returns>
    Public Async Function ListAllPensionaryTypeAsync() As Task(Of List(Of PensionaryType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllPensionaryTypeAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("PensionaryType", Indigo)
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
