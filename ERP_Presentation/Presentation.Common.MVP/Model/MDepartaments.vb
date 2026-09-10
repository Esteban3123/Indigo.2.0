'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 10-04-2013
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
Imports Domain.Entities
Imports Presentation.Base
Imports Domain.Base.Entities

#End Region
''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los Departamentos
''' </summary>
Public Class MDepartaments
    Inherits ModelBase
    Implements IDisposable

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(TAG As String)
        MyBase.New(TAG)
    End Sub
    Public Shared TAG As String = "510"
#Region "Methods"
    ''' <summary>
    ''' Obtener el listado de los paises
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCountry() As List(Of Country)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCountry(Indigo)
    End Function
    ''' <summary>
    ''' Obtener un Departamento por su codigo
    ''' </summary>
    ''' <param name="code">El codigo del nivel de cargo.</param>
    ''' <returns>El Nivel de cargo</returns>
    Public Async Function GetDepartmentAsync(ByVal code As String, ByVal IdCountry As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetDepartmentAsync(code, IdCountry, Indigo)
    End Function

    ''' <summary>
    ''' Obtener una lista de Departamentos por su pais
    ''' </summary>
    ''' <param name="IdCountry">El codigo del nivel de cargo.</param>
    Public Async Function GetDepartmentsAsync(ByVal IdCountry As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetDepartmentsAsync(IdCountry, Indigo)
    End Function

    ''' <summary>
    ''' Devuelve un departamento por id
    ''' </summary>
    ''' <param name="idCountry">id del departamento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetDepartmentByIdAsync(ByVal idDepartment As Integer) As Task(Of Department)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetDepartmentByIdAsync(idDepartment, Indigo)
    End Function

    ''' <summary>
    ''' Graba el Departamento
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el Departamento</returns>
    Public Async Function SaveDepartmentAsync(ByVal reg As Object) As Task(Of ActionResult(Of Department))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveDepartmentAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Elimina el Departamento
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el Departamento</returns>
    Public Async Function DeleteDepartmentAsync(ByVal reg As Object) As Task(Of ActionMessageResult(Of Department))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteDepartmentAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Lista los departamentos
    ''' </summary>
    Public Async Function ListAllDepartmentAsync() As Task(Of List(Of Department))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllDepartmentAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULL("Departments", Me.Indigo)
    End Function

    ''' <summary>
    ''' Cambia el estado del registro
    ''' </summary>
    ''' <returns>Un booleano</returns>
    Public Async Function ChangeState(ByVal code As String, ByVal idCountry As String, ByVal state As Boolean) As Task(Of ActionResult(Of Department))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ChangeStateDepartmentAsync(code, idCountry, state, Me.Indigo)
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
