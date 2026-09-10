'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-04-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel

#End Region

''' <summary>
''' Realiza la conexion con los servicios del grupo
''' </summary>
Public Class MOutstandingChecks
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Saves the outstanding checks.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function SaveOutstandingChecks(ByVal outstandingChecks As OutstandingChecks) As Task(Of ActionResult(Of OutstandingChecks))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveOutstandingChecksAsync(outstandingChecks, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Saves the outstanding checks.
    ''' </summary>
    ''' <returns></returns>
    Public Function SaveOutstandingChecksSimple(ByVal outstandingChecks As OutstandingChecks) As ActionResult(Of OutstandingChecks)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.SaveOutstandingChecks(outstandingChecks, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Deletes the outstanding checks.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeleteOutstandingChecks(ByVal outstandingChecks As OutstandingChecks) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.DeleteOutstandingChecksAsync(outstandingChecks, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetOutstandingChecksById(ByVal Id As Integer) As Task(Of OutstandingChecks)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetOutstandingChecksByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Obtiene el primer cheque que esta en espera
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetFirstOutstandingChecks(ByVal IdCheckBook As Integer) As Task(Of OutstandingChecks)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetFirstOutstandingChecksAsync(IdCheckBook)
    End Function

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    Public Async Function ListOutstandingChecksByIdCheckBook(ByVal IdCheckBook As Integer) As Task(Of List(Of OutstandingChecks))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.ListOutstandingChecksByIdCheckBookAsync(IdCheckBook)
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
