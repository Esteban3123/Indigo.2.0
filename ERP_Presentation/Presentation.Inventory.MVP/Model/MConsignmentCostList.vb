'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-01-31
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.CloudAgent
Imports Presentation.Controls.MVP
#End Region


Public Class MConsignmentCostList
    Implements IDisposable


#Region "Fields"
    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private Indigo As SessionValues

    ''' <summary>
    ''' Tag del formulario
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
    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    Public Async Function SaveConsignmentCostList(ByVal consignmentCostList As ConsignmentCostList) As Task(Of ActionResult(Of ConsignmentCostList))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveConsignmentCostListAsync(consignmentCostList, Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene la informacion de las tablas consignment y detalle por proveedor mas la unidad operativa
    ''' </summary>
    Public Async Function GetConsignmentCostListBySupplierId(ByVal SupplierId As Integer, ByVal OperatingUnitId As Integer) As Task(Of ConsignmentCostList)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentCostListBySupplierIdAsync(SupplierId, OperatingUnitId)
    End Function
    ''' <summary>
    ''' Elimina encabezado y detalle
    ''' </summary>
    ''' <param name="consignmentCostList">The product template.</param>
    ''' <returns></returns>
    Public Async Function DeleteConsignmentCostList(ByVal consignmentCostList As ConsignmentCostList) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteConsignmentCostListAsync(consignmentCostList, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Importar un archivo de excel
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetConsignmentConsListDetailFromFile(DataImport As List(Of ImportFileRow)) As ActionResult(Of List(Of ConsignmentCostListDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetConsignmentConsListDetailFromFile(DataImport)
    End Function

    ''' <summary>
    ''' Importa lo obtenido del copiar y pegar 
    ''' </summary>
    ''' <param name="DataImport"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetConsignmentConsListDetailFromCopyandPaste(DataImport As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of ConsignmentCostListDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetProductFeeDetailFromCopyandPasteAsync(DataImport)
    End Function


    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    Public Async Function UpdateStateConsignmentCostList(ByVal Id As Integer, ByVal state As Boolean) As Task(Of ActionResult(Of ConsignmentCostList))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateConsignmentCostListAsync(Id, state, Me.Indigo.AuditMessageWcf)
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
