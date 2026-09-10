'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2015
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

Public Class MCopyBase
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' tag del funcional
    ''' </summary>
    Dim _tag As String

    ''' <summary>
    ''' Valores de session
    ''' </summary>
    Dim _indigoSessionValues As SessionValues

#End Region

#Region "Builder"

    Sub New(ByVal tag As String)
        Me._tag = tag
        _indigoSessionValues = SessionValues.Instance
        Me._indigoSessionValues.AuditMessageWcf.Functional = Me._tag
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda la copia de presupuesto
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Public Async Function SaveCopyBase(CopyBase As CopyBase) As Task(Of Domain.Base.Entities.ActionResult(Of CopyBase))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveCopyBaseAsync(CopyBase, Me._indigoSessionValues.AuditMessageWcf)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
