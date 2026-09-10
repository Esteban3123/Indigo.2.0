'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-12-2014
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

Public Class MProductRate
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
    ''' <summary>
    ''' metodo asincrono para copiar y pegar o importar un archivo de excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetCopyPasteOrImportFileProductRateAsync(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of ProductRateDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetCopyPasteOrImportFileProductRateAsync(dataImportFile, dataCopyPaste)
    End Function

    ''' <summary>
    ''' metodo para copiar y pegar o importar un archivo de excel
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFileProductRate(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of ProductRateDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetCopyPasteOrImportFileProductRate(dataImportFile, dataCopyPaste)
    End Function

    ''' <summary>
    ''' Obtiene un cubrimiento de producto por codigo
    ''' </summary>
    Public Async Function GetProductTemplate(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of ProductRate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductTemplateAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un cubrimiento de producto por id
    ''' </summary>
    Public Async Function GetProductTemplateById(ByVal id As Integer) As Task(Of ProductRate)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductTemplateByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un cubrimiento de producto
    ''' </summary>
    Public Async Function UpdateStateProductTemplate(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ProductRate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateProductTemplateAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Saves the product template.
    ''' </summary>
    Public Async Function SaveProductTemplate(ByVal productTemplate As ProductRate, ByVal idSequense As Int64) As Task(Of ActionResult(Of ProductRate))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveProductTemplateAsync(productTemplate, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Deletes the product template.
    ''' </summary>
    ''' <param name="productTemplate">The product template.</param>
    ''' <returns></returns>
    Public Async Function DeleteProductTemplate(ByVal productTemplate As ProductRate) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteProductTemplateAsync(productTemplate, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
    ''' </summary>
    Public Async Function GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId As Integer, ProductId As Integer, ServiceDate As DateTime) As Task(Of ActionResult(Of ProductRateDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductRateDetailByCareGroupIdProductIdServiceDateAsync(CareGroupId, ProductId, ServiceDate)
    End Function

    Public Function GetProductRateDetailByCareGroupIdProductIdServiceDateSimple(CareGroupId As Integer, ProductId As Integer, ServiceDate As DateTime) As ActionResult(Of ProductRateDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId, ProductId, ServiceDate)
    End Function


    Public Async Function GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId As Integer, ProductId As List(Of Integer), ServiceDate As DateTime) As Task(Of List(Of ProductRateDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetListProductRateDetailByCareGroupIdProductIdServiceDateAsync(CareGroupId, ProductId, ServiceDate)
    End Function

    ''' <summary>
    ''' Obtiene la homologacion de cups
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="FunctionalUnitId"></param>
    ''' <param name="Specialty"></param>
    ''' <param name="ServiceDate"></param>
    ''' <param name="IPSServiceId"></param>
    ''' <param name="ManualType"></param>
    ''' <param name="RiasId"></param>
    ''' <param name="ContractDescriptionId"></param>
    ''' <returns></returns>
    Public Function GetHomologationCups(CareGroupId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, Optional IPSServiceId As Integer = 0, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetHomologationCups(CareGroupId, CupsId, FunctionalUnitId, Specialty, ServiceDate, IPSServiceId, ManualType, RiasId, ContractDescriptionId)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ProductId"></param>
    ''' <returns></returns>
    Public Async Function GetInventoryProduct(ProductId As List(Of Integer)) As Task(Of List(Of InventoryProduct))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductByIdsAsync(ProductId)
    End Function

    ''' <summary>
    ''' retorna el valor del producto independiente si es fija o por porcentaje
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Public Async Function GetProductRateDetailWithValue(CareGroupId As Integer, ProductId As Integer, ServiceDate As DateTime) As Task(Of ActionResult(Of ProductRateDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductRateDetailWithValueAsync(CareGroupId, ProductId, ServiceDate)
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