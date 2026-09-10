'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 06/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Controls.MVP

#End Region

Public Class MPackage
    Implements IDisposable

#Region "Fields"


    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Lista todos los paquetes
    ''' </summary>
    Public Function ListAllPackage() As List(Of Package)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllPackage(Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista todos los paquetes duplicados
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="packageDetailTmp"></param>
    ''' <returns></returns>
    Public Function ListDuplicatePackage(packageId As Integer, packageDetailTmp As List(Of Tuple(Of Byte, Integer))) As List(Of PackageDto)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListDuplicatePackage(packageId, packageDetailTmp, Me._sessionValues.AuditMessageWcf)
    End Function


    ''' <summary>
    ''' Lista todos los paquetes asincrono
    ''' </summary>
    Public Async Function ListAllPackageAsync() As Task(Of List(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllPackageAsync(Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetProductRateDetailPackageByPackageIdAsync(packageId As Integer) As Task(Of List(Of ProductRateDetailPackage))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductRateDetailPackageByPackageIdAsync(packageId)
    End Function

    ''' <summary>
    ''' Obtiene un paquete por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPackage(ByVal code As String) As ActionResult(Of Package)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetPackage(code, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Obtiene un paquete por código asincrono
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPackageAsync(ByVal code As String) As Task(Of ActionResult(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetPackageAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un paquete asociado a un proceso de producción
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProductionPackageAsync(ByVal Code As String) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetProductionPackageAsync(Code, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Obtiene un paquete por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPackageById(ByVal id As Integer) As ActionResult(Of Package)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetPackageById(id, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un paquete por id asincrono
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPackageByIdAsync(ByVal id As Integer) As Task(Of ActionResult(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetPackageByIdAsync(id, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Guarda o actualiza un paquete
    ''' </summary>
    ''' <param name="package"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SavePackage(ByVal package As Package, ByVal idSequence As Int64) As ActionResult(Of Package)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SavePackage(package, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Guarda o actualiza un paquete asincrono
    ''' </summary>
    ''' <param name="package"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePackageAsync(ByVal package As Package, ByVal idSequence As Int64) As Task(Of ActionResult(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SavePackageAsync(package, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Elimina un paquete
    ''' </summary>
    ''' <param name="package"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeletePackage(ByVal package As Package) As ActionResult
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeletePackage(package, Me._sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Elimina un paquete asincrono
    ''' </summary>
    ''' <param name="package"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePackageAsync(ByVal package As Package) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.DeletePackageAsync(package, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStatePackageAsync(code, state, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateAsync(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Package))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStatePackageAsync(code, state, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un producto por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetInventoryProductByIdSimple(ByVal id As Integer) As InventoryProduct
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Me._sessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._sessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductById(id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetUnitDoseTypeById(ByVal id As Integer) As Task(Of ActionResult(Of UnitDoseType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetUnitDoseTypeByIdAsync(id, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function ListInventoryProductByProductType(classProductType As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListInventoryProductByProductType(classProductType)
    End Function


    ''' <summary>
    ''' Initializes the los productos de tipo insumo para nutiriciones parenterales.
    ''' </summary>
    Public Function ListSuppliesForNpt()
        Dim filter As String = String.Format("ProductTypeId.Class= {0} AND SupplieId.IsParenteralNutritionSupply = {1}", 3, 1)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListInventoryProductByFilter(filter)
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
