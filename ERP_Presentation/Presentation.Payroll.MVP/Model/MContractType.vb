'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
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
'Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Presentation.Base

#End Region

Public Class MContractType
    Inherits ModelBase
    Implements IDisposable
    Private Indigo As SessionValues = SessionValues.Instance

    Public Shared TAG As String = "541"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#Region "methods"
    ''' <summary>
    ''' Obtener un registro por sucodigo modo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetContractTypeAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractTypeAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por su id modo asincrono
    ''' </summary>
    ''' <param name="code">El id del registro.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetContractTypeByIdAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractTypeByIdAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtener un registro por sucodigo 
    ''' </summary>
    ''' <param name="code">El codigo del registro.</param>
    ''' <returns>El registro</returns>
    Public Function GetContractType(ByVal code As String) As Object
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContractType(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Function SaveContractType(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveContractType(reg, Indigo)
    End Function

    ''' <summary>
    ''' Graba el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveContractTypeAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveContractTypeAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Function DeleteContractType(ByVal reg As Object) As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteContractType(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Async Function DeleteContractTypeAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteContractTypeAsync(reg, Indigo)
    End Function


    Public Async Function ListAllContractTypeAsync() As Task(Of List(Of ContractType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllContractTypeAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("ContractType", Indigo)
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
