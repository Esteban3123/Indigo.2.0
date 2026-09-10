'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 09-07-2013
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

Public Class MAuthorizationConcept
    Inherits ModelBase
    Implements IDisposable
    Private Indigo As SessionValues = SessionValues.Instance


#Region "Properties"

    Public Shared TAG As String = "556"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
    End Sub

#End Region

#Region "methods"
    ''' <summary>
    ''' Obtener los conceptos autorizados filtrados por grupo
    ''' </summary>
    ''' <param name="GroupId">El id del grupo.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetAuthorizationConceptByGroupIdAsync(ByVal GroupId As Integer) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetAuthorizationConceptByGroupIdAsync(GroupId, Indigo)
    End Function

    ''' <summary>
    ''' Obtener los conceptos autorizados filtrados por Empleado
    ''' </summary>
    ''' <param name="EmployeeId">El id del empleado.</param>
    ''' <returns>El registro</returns>
    Public Async Function GetAuthorizationConceptByEmployeeIdAsync(ByVal EmployeeId As Integer) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetAuthorizationConceptByEmployeeIdAsync(EmployeeId, Indigo)
    End Function

    ''' <summary>
    ''' Graba un registro de autorizacion de conceptos
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveListAuthorizationConceptAsync(ByVal reg As List(Of AuthorizationConcept)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveListAuthorizationConceptAsync(reg, Indigo)
    End Function

    ''' <summary>
    ''' Graba una lista de autorizacion de conceptos
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el registro</returns>
    Public Async Function SaveAuthorizationConceptAsync(ByVal reg As Object) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveAuthorizationConceptAsync(reg, Indigo)
    End Function


    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el registro</returns>
    Public Async Function DeleteAuthorizationConceptAsync(ByVal reg As Object) As Task(Of ActionMessageResult(Of AuthorizationConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteAuthorizationConceptAsync(reg, Indigo)
    End Function

    Public Async Function ListAllAuthorizationConceptAsync() As Task(Of List(Of AuthorizationConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllAuthorizationConceptAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("AuthorizationConcept", Indigo)
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
