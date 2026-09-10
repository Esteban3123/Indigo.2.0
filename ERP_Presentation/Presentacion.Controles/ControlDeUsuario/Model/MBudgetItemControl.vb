'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
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
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Clase que expone los metodos de servicios
''' </summary>
''' <remarks></remarks>
Public Class MBudgetItemControl
    Implements IDisposable
    Public Shared TAG As String = "206"


    Protected _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Protected TagForm As String

#Region "Constructor"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        Me.TagForm = TAG
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Listado de rubros por vigencia
    ''' </summary>
    ''' <param name="ValidityId">Id de la vigencia</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListBudgetItemsByValidityAsync(ValidityId As String, ItemType As Byte) As Task(Of List(Of Category))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ListBudgetItemsByValidityAsync(ValidityId, ItemType, Me._indigoSessionValues.AuditMessageWcf)
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
