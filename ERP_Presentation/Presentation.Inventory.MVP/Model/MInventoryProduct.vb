'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 20-11-2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo
Imports System.Text
Imports Domain.Security.Entities

#End Region

Public Class MInventoryProduct
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
    ''' Gets the list admissions filter.
    ''' </summary>
    Function ListDiagnosticById(id As Integer) As XPCollection(Of DiagnosticXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListDiagnosticById(id)
    End Function

    Public Function GetProductTypeXpo(ProductTypeId As Integer) As ProductTypeXpo
        Dim filtroConsulta As String = "Id = " & ProductTypeId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ProductTypeXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    Public Function GetProductHierarchyXpo(ProductId As Integer) As List(Of ProductHierarchyXpo)
        Dim filtroConsulta As String = " HierarchyProductFinalId = " & ProductId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ProductHierarchyXpo)(Nothing, filtroConsulta)
    End Function

    Public Function GetProductHierarchyXpoById(ProductHierarchyId As Integer) As ProductHierarchyXpo
        Dim filtroConsulta As String = " Id = " & ProductHierarchyId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ProductHierarchyXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Gets the list admissions filter.
    ''' </summary>
    Function ListMeasureUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListNotMeasureUnitByType(4)
    End Function

    Function ValidateChangeSubgroup(productId As Integer) As Integer
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetKardexByProductId(productId)
    End Function

    Public Async Function GetProductByIdXpo(productId As Integer) As Task(Of InventoryProductXpo)
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetInventoryProductXpo(productId))
    End Function

    Function GetProductXpoByCode(code As String) As InventoryProductXpo
        Dim filter As String = "Code = '" & code & "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryProductXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Verifica que en el Inventario Fisico los productos tengan cantidades mayores a cero
    ''' </summary>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateChangeSubgroupPhysicalInventory(productId As Integer) As Integer
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetPhysicalInventoryByProductId(productId)
    End Function

    ''' <summary>
    ''' Obtiene un producto por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetInventoryProduct(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryProduct))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Loads the list hierarchy product.
    ''' </summary>
    ''' <param name="product">The product.</param>
    ''' <returns></returns>
    Public Async Function LoadListHierarchyProduct(ByVal product As InventoryProduct) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of ProductHierarchy)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.LoadListHierarchyProductAsync(product)
    End Function

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetInventoryProductById(ByVal id As Integer) As Task(Of InventoryProduct)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductByIdAsync(id)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdSimple(ByVal id As Integer) As InventoryProduct
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductById(id)
        End Using
    End Function

    ''' <summary>
    ''' Updates the state product.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function UpdateStateProduct(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of InventoryProduct))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.UpdateStateProductAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda un producto
    ''' </summary>
    ''' <param name="product">The product.</param>
    ''' <param name="idSequense">The identifier sequense.</param>
    ''' <returns></returns>
    Public Async Function SaveInventoryProduct(ByVal product As InventoryProduct, ByVal idSequense As Int64) As Task(Of ActionResult(Of InventoryProduct))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventoryProductAsync(product, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un producto
    ''' </summary>
    ''' <param name="product">The product.</param>
    ''' <returns></returns>
    Public Async Function DeleteInventoryProduct(ByVal product As InventoryProduct) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteInventoryProductAsync(product, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdSimpleToGroup(ByVal id As Integer) As InventoryProduct
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductByIdSimple(id)
        End Using
    End Function
    ''' <summary>
    ''' Obtiene un producto por codigo
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByCodeSimpleToGroup(ByVal code As String) As InventoryProduct
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductByCodeWithProducGroup(code)
        End Using
    End Function

    ''' <summary>
    ''' Genera el archivo plano de sismed de ventas
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateFileSismedVentas(DateStart As Date, DateEnd As Date) As Task(Of ActionResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GenerateFileSismedVentasAsync(DateStart, DateEnd, Me.Indigo)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de sismed de compras
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateFileSismedCompras(DateStart As Date, DateEnd As Date) As Task(Of ActionResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GenerateFileSismedComprasAsync(DateStart, DateEnd, Me.Indigo)
    End Function
    ''' <summary>
    ''' Genera el archivo plano de sismed de compras y ventas unificado Res 006
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <returns></returns>
    Public Async Function GenerateFileSismedRes006(DateStart As Date, DateEnd As Date) As Task(Of ActionResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GenerateFileSismedRes006Async(DateStart, DateEnd, Me.Indigo)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de SISDIS Circular 002
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <returns></returns>
    Public Async Function GenerateSISDIS002(DateStart As Date, DateEnd As Date) As Task(Of ActionResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GenerateSISDIS002Async(DateStart, DateEnd, Me.Indigo, Nothing)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de SISDIS Circular 015
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <returns></returns>
    Public Async Function GenerateSISDIS015(DateStart As Date, DateEnd As Date) As Task(Of ActionResult(Of StringBuilder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GenerateSISDIS015Async(DateStart, DateEnd, Me.Indigo)
    End Function

    ''' <summary>
    ''' Consultar los permisos que tiene el usuario en éste formulario
    ''' </summary>
    Public Function GetPermissions(ByVal tagForm As String) As List(Of PermissionUserToolbar)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.ListPermissionsUserToolbar(Infrastructure.CrossCutting.Base.SessionValues.Instance.UserIndigo, Infrastructure.CrossCutting.Base.SessionValues.Instance.UserRol, tagForm, Infrastructure.CrossCutting.Base.SessionValues.Instance)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="product"></param>
    ''' <returns></returns>
    Public Async Function ValidateMedicationTypeByProduct(product As InventoryProduct) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ValidateMedicationTypeByProductAsync(product)
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