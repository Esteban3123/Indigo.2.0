'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
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
Public Class MLevelCategory
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "241"

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
    ''' Obtiene un nivel de rubro
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetLevelCategoryAsync(ByVal code As String) As Task(Of LevelCategory)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetLevelCategoryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un nivel de rubro
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListLevelsCategoryAsync() As Task(Of List(Of LevelCategory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ListLevelsCategoryAsync(Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Eliminar una fuente de financiacion
    ''' </summary>
    ''' <param name="LevelCategory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteLevelCategoryAsync(LevelCategory As LevelCategory) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteLevelCategoryAsync(LevelCategory, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gaurdar o Actualizar una fuente de financiacion
    ''' </summary>
    ''' <param name="LevelCategory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveLevelCategoryAsync(LevelsCategory As List(Of LevelCategory)) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.LevelCategory)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveLevelCategoryAsync(LevelsCategory, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetAllBudgetItemsByStateAsync() As Task(Of Integer)
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetAllBudgetItemsByStateAsync(True)
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
