'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-12-2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MInteropCostSetting
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
    ''' Guarda un parámetro de costos
    ''' </summary>
    Public Async Function SaveInteropCostSetting(ByVal interopCostSetting As InteropCostSetting) As Task(Of ActionResult(Of InteropCostSetting))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.SaveInteropCostSettingAsync(interopCostSetting, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    Public Async Function DeleteInteropCostSetting(ByVal interopCostSetting As InteropCostSetting) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.DeleteInteropCostSettingAsync(interopCostSetting, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Public Function GetInteropCostSetting() As InteropCostSetting
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetInteropCostSetting()
    End Function

    Public Function HasMonthClosed() As Boolean
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.HasMonthClosed()
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    Public Async Function GetInteropCostSettingAsync() As Task(Of InteropCostSetting)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetInteropCostSettingAsync()
    End Function

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    Public Async Function GetInteropCostSettingById(id As Integer) As Task(Of InteropCostSetting)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInteropCost.GetInteropCostSettingByIdAsync(id)
    End Function

    Function ListDocumentTypes(status As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.InteropCostContainer).InteropCostService.ListDocumentTypes(status)
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