'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Duván Albeiro Mejia Cortes
' Created          : 2022-01-13
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports System.ServiceModel

#End Region

''' <summary>
''' Clase que expone los metodos de servicios
''' </summary>
''' <remarks></remarks>
Public Class MPublicPolicy
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "2716"

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene una fuente de política pública por codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPublicPolicyAsync(ByVal code As String) As Task(Of ActionResult(Of PublicPolicy))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetPublicPolicyAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guardar o Actualizar una política pública
    ''' </summary>
    ''' <param name="publicPolicy"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Async Function SavePublicPolicyAsync(publicPolicy As PublicPolicy, ByVal idSequense As Int64) As Task(Of ActionResult(Of PublicPolicy))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SavePublicPolicyAsync(publicPolicy, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia estado una política pública
    ''' </summary>
    ''' <param name="publicPolicyId"></param>
    ''' <returns></returns>
    Public Async Function ChangeStatePublicPolicyAsync(publicPolicyId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStatePublicPolicyAsync(publicPolicyId, Me._indigoSessionValues.AuditMessageWcf)
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
