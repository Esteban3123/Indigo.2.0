'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 10-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
'Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base
Imports Domain.Base.Entities
#End Region

Public Class MTimeUnit
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="TAG"></param>
    ''' <remarks></remarks>
    Public Sub New(TAG As String)
        MyBase.New(TAG)
    End Sub

#Region "methods"
    ''' <summary>
    ''' Obtener un registro por sucodigo modo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetTimeUnitAsync(ByVal code As String) As Task(Of TimeUnit)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTimeUnitAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por sucodigo 
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Function GetTimeUnit(ByVal code As String) As TimeUnit
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTimeUnit(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Function SaveTimeUnit(ByVal reg As TimeUnit) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveTimeUnit(reg, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveTimeUnitAsync(ByVal reg As TimeUnit) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveTimeUnitAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Function DeleteTimeUnit(ByVal reg As TimeUnit) As ActionMessageResult(Of TimeUnit)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteTimeUnit(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Async Function DeleteTimeUnitAsync(ByVal reg As TimeUnit) As Task(Of ActionMessageResult(Of TimeUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteTimeUnitAsync(reg, Indigo)
    End Function


    Public Async Function ListAllPositionLevel() As Task(Of List(Of TimeUnit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllTimeUnitAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULL("TimeUnit", Me.Indigo)
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
