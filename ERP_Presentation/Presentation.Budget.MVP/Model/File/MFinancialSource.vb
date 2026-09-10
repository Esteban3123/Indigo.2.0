'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 02-04-2014
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
Public Class MFinancialSource
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "201"

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
    ''' Obtiene una fuente de financiacion por su codigo
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFinancialSourceAsync(ByVal code As String, validityId As Integer) As Task(Of FinancialSource)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetFinancialSourceAsync(code, validityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Eliminar una fuente de financiacion
    ''' </summary>
    ''' <param name="financialSource"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteFinancialSourceAsync(financialSource As FinancialSource) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteFinancialSourceAsync(financialSource, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guardar o Actualizar una fuente de finaciación
    ''' </summary>
    ''' <param name="financialSource"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    Public Async Function SaveFinancialSourceAsync(financialSource As FinancialSource, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.FinancialSource))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveFinancialSourceAsync(financialSource, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambiar el estado a le entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="validityId"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function ChangeState(ByVal code As String, validityId As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of Domain.Entities.FinancialSource))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStateFinancialSourceAsync(code, validityId, state, Me._indigoSessionValues.AuditMessageWcf)
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
