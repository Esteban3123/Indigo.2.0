'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 10-04-2014
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
Public Class MPrivateBudgetItemsStructure
    Inherits ModelBaseBudget
    Implements IDisposable

#Region "Variables"

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    Dim TAG As String

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(_tag As String)
        MyBase.New(_tag)
        _indigoSessionValues = SessionValues.Instance
        Me.TAG = _tag
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPrivateBudgetItemsStructure(code As String) As Task(Of ActionResult(Of PrivateBudgetItemsStructure))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetPrivateBudgetItemsStructureAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPrivateBudgetItemsStructureById(id As String) As Task(Of ActionResult(Of PrivateBudgetItemsStructure))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetPrivateBudgetItemsStructureByIdAsync(id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina el registro
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Async Function DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeletePrivateBudgetItemsStructureAsync(PrivateBudgetItemsStructure, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un concepto
    ''' </summary>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SavePrivateBudgetItemsStructure(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure, ByVal idSequense As Int64) As Task(Of ActionResult(Of PrivateBudgetItemsStructure))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SavePrivateBudgetItemsStructureAsync(PrivateBudgetItemsStructure, idSequense, Me._indigoSessionValues.AuditMessageWcf)
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
