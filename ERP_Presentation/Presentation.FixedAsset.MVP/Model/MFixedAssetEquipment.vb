'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 03-09-2013
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
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en el funcional de equipos
''' </summary>
Public Class MFixedAssetEquipment
    Inherits MBlockRecordAndSequenceFixedAsset
    Implements IDisposable


    Dim Indigo As SessionValues
    Public Shared TAG As String = "572"
    Public Sub New()
        MyBase.New(TAG)
        Indigo = SessionValues.Instance
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Obtener una torre por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la torre</param>
    ''' <returns>objeto tipo de poliza</returns>
    Public Async Function GetEquipment(ByVal code As String) As Task(Of ActionResult(Of FixedAssetItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetEquipmentAsync(code)
    End Function
    ''' <summary>
    ''' Graba el objeto torre en modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo la torre</returns>
    Public Async Function SaveEquipmentAsync(ByVal reg As FixedAssetItem, idSequense As Int64) As Task(Of ActionResult(Of FixedAssetItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveEquipmentAsync(reg, Indigo, idSequense, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Elimina el objeto equipo modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito la torre</returns>
    Public Async Function DeleteEquipment(ByVal reg As FixedAssetItem) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteEquipmentAsync(reg, Indigo)
    End Function
    ''' <summary>
    ''' Lista todos los equipos
    ''' </summary>
    Public Async Function ListAllEquipment() As Task(Of List(Of FixedAssetItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllEquipmentAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los tipos de equipo por tipo de inventario seleccionado
    ''' </summary>
    Public Function ListEquipmentTypeByInventoryType(IdInventoryType As Integer) As List(Of FixedAssetEquipmentType)
        'Return IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.ListEquipmentTypeInventoryType(Indigo.TransactionalContainer, IdInventoryType)
    End Function

    ''' <summary>
    ''' Obtiene el ultimo costo de un articulo por cada moneda activa.
    ''' </summary>
    ''' <param name="id">ID del artículo</param>
    ''' <returns></returns>
    Public Async Function GetEquipmentLastCostPerCurrency(id As Integer) As Task(Of ActionResult(Of List(Of Tuple(Of Decimal, Currency))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetItemCostsPerCurrencyAsync(id)
    End Function


    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(Code As String, State As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of FixedAssetItem))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.Change_StateEquipmentAsync(Code, State, Indigo)
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
