'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 02-07-2013
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
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

''' <summary>
''' Realiza la conexion con los servicios del grupo
''' </summary>
Public Class MWorkCenter
    Inherits ModelBase
    Implements IDisposable

#Region "Properties"

    Public Shared TAG As String = "539"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el centro de trabajo de acuerdo al codigo 
    ''' </summary>
    ''' <param name="code">Codigo del centro de trabajo</param>
    ''' <returns>centro de trabajo</returns>
    Public Async Function GetWorkCenterAsync(ByVal code As String) As Task(Of WorkCenter)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetWorkCenterAsync(code, Indigo)
    End Function


    ''' <summary>
    ''' Guarda los cambios del centro de trabajo asincrono
    ''' </summary>
    ''' <param name="WorkCenter">centro de trabajo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function SaveWorkCenterAsync(ByVal workCenter As WorkCenter) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveWorkCenterAsync(workCenter, Indigo)
    End Function


    ''' <summary>
    ''' Borra centro de trabajo asincrono
    ''' </summary>
    ''' <param name="WorkCenter">centro de trabajo</param>
    ''' <returns>Si se realizo o no el cambio</returns>
    Public Async Function DeleteWorkCenterAsync(ByVal workCenter As WorkCenter) As Task(Of ActionMessageResult(Of WorkCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteWorkCenterAsync(workCenter, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el listado de centro de trabajo asincrono
    ''' </summary>
    ''' <returns>Listado de centro de trabajo</returns>
    Public Async Function ListAllWorkCenterAsync() As Task(Of List(Of WorkCenter))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllWorkCenterAsync(Indigo)
    End Function


    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("WorkCenter", Indigo)
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
