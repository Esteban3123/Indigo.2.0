'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 29-04-2013
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
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los parentescos
''' </summary>
Public Class MKinship
    Inherits ModelBase
    Implements IDisposable

    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtener un parentesco por su codigo
    ''' </summary>
    ''' <param name="code">El codigo del parentesco.</param>
    ''' <returns>El parentesco</returns>
    Public Function GetKinship(ByVal code As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetKinship(code, Indigo)
    End Function
    ''' <summary>
    ''' Obtener un parentesco por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del parentesco.</param>
    ''' <returns>La Profesion</returns>
    Public Async Function GetKinshipAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetKinshipAsync(code, Indigo)
    End Function
    ''' <summary>
    ''' Graba el parentesco
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el parentesco</returns>
    Public Function SaveKinship(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveKinship(reg, Indigo)
    End Function
    ''' <summary>
    ''' Graba el parentesco modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el parentesco</returns>
    Public Async Function SaveKinshipAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveKinshipAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Elimina el parentesco
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el parentesco</returns>
    Public Function DeleteKinship(ByVal reg As Kinship) As ActionMessageResult(Of Kinship)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteKinship(reg, Indigo)
    End Function
    ''' <summary>
    ''' Elimina el parentesco modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el parentesco</returns>
    Public Async Function DeleteKinshipAsync(ByVal reg As Kinship) As Task(Of ActionMessageResult(Of Kinship))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteKinshipAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Lista los parentescos
    ''' </summary>
    Public Function ListAllKinship() As List(Of Kinship)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllKinship(Indigo)
    End Function
    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("Kinship", Indigo)
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
