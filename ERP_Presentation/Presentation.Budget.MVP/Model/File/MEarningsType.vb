'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 09-04-2014
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
Public Class MEarningsType
    Inherits ModelBaseBudget
    Implements IDisposable
    Public Shared TAG As String = "202"

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
    ''' Obtiene un tipo de ingreso
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEarningsTypeAsync(code As String, validityId As Integer, type As Integer) As Task(Of RevenueType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetEarningsTypeAsync(code, validityId, type, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un tipo de ingreso by validity
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEarningsTypeByValidityAsync(code As String, ValidityId As String) As Task(Of RevenueType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetEarningsTypeByValidityAsync(code, ValidityId, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">La entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Public Async Function DeleteEarningsType(earningsType As RevenueType) As Task(Of Domain.Base.Entities.ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.DeleteEarningsTypeAsync(earningsType, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un tipo de ingreso
    ''' </summary>
    ''' <param name="earningsType">la entidad</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Async Function SaveEarningsType(earningsType As RevenueType, ByVal idSequense As Int64) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.RevenueType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.SaveEarningsTypeAsync(earningsType, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function


    Public Async Function ChangeState(ByVal code As String, validityId As Integer, type As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of RevenueType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.ChangeStateEarningsTypeAsync(code, validityId, type, state, Me._indigoSessionValues.AuditMessageWcf)
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
