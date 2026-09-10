'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 08-07-2013
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
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region

Public Class MRetention
    Inherits ModelBase
    Implements IDisposable

    Shared TAG As String = "545"
    Sub New()
        MyBase.New(TAG)
        Me.Indigo = SessionValues.Instance
    End Sub

#Region "methods"
    ''' <summary>
    ''' Obtener un registro por sucodigo modo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetRetentionAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetRetentionAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por sucodigo 
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Function GetRetention(ByVal code As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetRetention(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Function SaveRetention(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveRetention(reg, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveRetentionAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveRetentionAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Function DeleteRetention(ByVal reg As Object) As ActionMessageResult(Of Retention)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteRetention(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Async Function DeleteRetentionAsync(ByVal reg As Object) As Task(Of ActionMessageResult(Of Retention))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteRetentionAsync(reg, Indigo)
    End Function


    Public Function ListAllRetention() As List(Of Retention)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllRetention(Indigo)
    End Function

    Public Async Function GetConsecutive() As Task(Of Integer)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetConsecutiveRetention()
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Retention", Indigo)
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
