'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

Public Class MConfigurationServicesAmbulatory
    Implements IDisposable

#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetConfigurationServicesAmbulatoryById(ByVal id As Integer) As Task(Of ActionResult(Of ConfigurationServicesAmbulatory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.GetConfigurationServicesAmbulatoryByIdAsync(id)
    End Function

    Public Async Function SaveConfigurationServicesAmbulatory(ConfigurationServicesAmbulatory As ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds As List(Of Integer), ListPortfolioInventoryProductIds As List(Of Integer)) As Task(Of ActionResult(Of ConfigurationServicesAmbulatory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.SaveConfigurationServicesAmbulatoryAsync(ConfigurationServicesAmbulatory, ListPortfolioCUPSEntityIds, ListPortfolioInventoryProductIds, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function DeleteConfigurationServicesAmbulatory(ListIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAuthorization.DeleteConfigurationServicesAmbulatoryAsync(ListIds, Indigo.TransactionalContainer, Indigo.AuditMessageWcf)
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
