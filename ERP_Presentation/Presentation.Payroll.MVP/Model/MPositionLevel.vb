'***********************************************************************
' Assembly         : Presentacion.Payroll.File.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 06-04-2013
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
Imports System.ServiceModel

#End Region

Public Class MPositionLevel
    Inherits ModelBase
    Implements IDisposable

    Public Shared TAG As String = "506"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="tag"></param>
    ''' <remarks></remarks>
    Sub New(tag As String)
        MyBase.New(tag)
    End Sub



#Region "methods"
    ''' <summary>
    ''' Obtener un nivel de cargo por sucodigo
    ''' </summary>
    ''' <param name="code">El codigo del nivel de cargo.</param>
    ''' <returns>El Nivel de cargo</returns>
    Public Async Function GetPositionLevelAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPositionLevelAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Graba el nivel de cargo
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el nivel de cargo</returns>
    Public Async Function SavePositionLevelAsync(ByVal reg As Object, ByVal idSequense As Int64) As Task(Of ActionResult(Of PositionLevel))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SavePositionLevelAsync(reg, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SavePositionLevelAsync(reg, Indigo, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina el nivel de cargo
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el nivel de cargo</returns>
    Public Async Function DeletePositionLevelAsync(ByVal reg As Object) As Task(Of ActionResult)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeletePositionLevelAsync(reg, Indigo)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeletePositionLevelAsync(reg, Indigo, Me.Indigo.AuditMessageWcf)
    End Function


    Public Function ListAllPositionLevel() As List(Of PositionLevel)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllPositionLevel(Indigo)
    End Function

    ''' <summary>
    ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ''' </summary>
    ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    Public Function GetNullFields() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("PositionLevel", Indigo)
    End Function


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of PositionLevel))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStatePositionLevelAsync(code, state, Indigo, Me.Indigo.AuditMessageWcf)
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
