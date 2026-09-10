'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 05-08-2013
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
#End Region

''' <summary>
''' Esta clase tiene el modelo del patron MVP implementado en los tipos de equipos
''' </summary>
Public Class MEquipamentType
    Implements IDisposable
    Dim Indigo As SessionValues

#Region "Methods"

    ''' <summary>
    ''' Obtener un tipo de equipo por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo del parentesco.</param>
    ''' <returns>objeto tipo de equipo</returns>
    Public Async Function GetEquipamentTypeAsync(ByVal code As String) As Task(Of FixedAssetItemType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetEquipmentTypeAsync(Indigo.TransactionalContainer, code)
    End Function
    ''' <summary>
    ''' Graba el tipo de equipo modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el tipo de equipo</returns>
    Public Async Function SaveEquipamentTypeAsync(ByVal reg As List(Of FixedAssetItemType)) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveEquipmentTypeAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Graba el tipo de equipo modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se grabo el tipo de equipo</returns>
    Public Async Function SaveEquipamentTypeAsync(ByVal reg As FixedAssetItemType) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetItemTypeAsync(reg, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina el tipo de equipo modo asincrono
    ''' </summary>
    ''' <param name="reg">El registro.</param>
    ''' <returns>Un valor que indica si se elimino con exito el tipo de equipo</returns>
    Public Async Function DeleteEquipamentTypeAsync(ByVal reg As FixedAssetItemType) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteEquipmentTypeAsync(Indigo.TransactionalContainer, reg, Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Lista todos los tipos de equipos
    ''' </summary>
    Public Async Function ListAllEquipamentType() As Task(Of List(Of FixedAssetItemType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllEquipmentTypeAsync(Indigo.TransactionalContainer)
    End Function
    ''' <summary>
    ''' Lista los tipos de inventario 
    ''' </summary>
    Public Function ListAllInventoryType() As Task(Of List(Of FixedAssetInventoryType))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllFixedAssetInventoryTypeAsync()
    End Function
    ' ''' <summary>
    ' ''' Lista los campos nulos de la base de datos y permitir su customizacion
    ' ''' </summary>
    ' ''' <returns>Un conjuto de datos con los campos marcados como nulos en la base de datos</returns>
    'Public Function GetNullFields() As DataSet
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("EquipamentType", Indigo)
    'End Function

    ''' <summary>
    ''' Obtener un tipo de equipo por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="Id">El ID del parentesco.</param>
    ''' <returns>objeto tipo de equipo</returns>
    Public Async Function GetEquipamentTypeByIdAsync(ByVal Id As Integer) As Task(Of FixedAssetItemType)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetEquipmentTypeByIdAsync(Indigo.TransactionalContainer, Id)
    End Function

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.Indigo = SessionValues.Instance
    End Sub
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
