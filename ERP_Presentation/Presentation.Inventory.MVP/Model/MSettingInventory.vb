'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 30-01-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MSettingInventory
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag">Tag del funcional</param>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un parametro de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventorySettingsRegister(ByVal OperatingUnitId As Integer) As Task(Of ActionResult(Of SettingInventory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventorySettingsRegisterAsync(OperatingUnitId)
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro de parametros de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveSettingInventory(ByVal record As SettingInventory) As Task(Of ActionResult(Of SettingInventory))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveSettingInventoryAsync(record, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo Para validar Stock Por productos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ValidateStock(ByVal productId As Integer, ByVal OperatingUnitId As Integer, Optional warehouseId As Integer = 0, Optional quantity As Integer = 0, Optional movement As InventoryStaticServices.MovementType = 0) As Task(Of ActionResult(Of InventoryProduct))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ValidateStockAsync(productId, OperatingUnitId, warehouseId, quantity, movement)
    End Function

    ''' <summary>
    ''' Obtiene los parametros de inventario por unidad operativa
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingInventoryByOperatingUnitIdXpo(ByVal operatingUnitId As Integer) As XPCollection
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetSettingInventoryByOperatingUnitId(operatingUnitId)
    End Function

    ''' <summary>
    ''' Lista los almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllActiveWarehouse() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListAllActiveWarehouse(True, _sessionValues.UserIndigo)
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
