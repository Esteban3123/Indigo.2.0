'***********************************************************************
' Assembly         : Presentacion.Seguridad.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 03-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports System.Threading.Tasks
Imports Domain.Entities

#End Region

''' <summary>
''' Clase Modelo de el control de usuario de trazabilidad de la glosa
''' </summary>
Public Class MTraceabilityControl
    Implements IDisposable

#Region "Variables Generales"
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance
#End Region

#Region "Metodos - Funciones"
    ''' <summary>
    ''' Funcion que retorna el listado con la trazabilidad de la factura.
    ''' </summary>
    ''' <param name="Invoice">The invoice.</param>
    ''' <returns></returns>
    Public Async Function GetTraceability(ByVal Invoice As String, ByVal Entity As String, ByVal _idOperativeUnit As Integer) As Task(Of List(Of ControlParametersTime))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListControlParametersTimeAsync(Invoice, Entity, _indigoSessionValues, _idOperativeUnit)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
