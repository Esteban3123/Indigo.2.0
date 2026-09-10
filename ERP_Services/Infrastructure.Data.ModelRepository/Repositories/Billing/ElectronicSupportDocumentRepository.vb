Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports System.Dynamic

Public Class ElectronicSupportDocumentRepository
    Inherits GenericRepository(Of ElectronicSupportDocument)
    Implements IElectronicSupportDocumentRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un objeto de la entidad por Id
    ''' </summary>
    ''' <param name="supporDocumentId"></param>
    ''' <returns></returns>
    Public Function GetDocumentSupportById(supporDocumentId As Integer) As ElectronicSupportDocument Implements IElectronicSupportDocumentRepository.GetDocumentSupportById
        Dim res As ElectronicSupportDocument
        res = (From esd As ElectronicSupportDocument In Me._context.ElectronicSupportDocument
               Where esd.Id = supporDocumentId
               Select esd).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ElectronicSupportDocument
        End If
    End Function

    Public Function GetElectronicSupportDocumentByDocumentOrigin(EntityId As Integer, EntityName As String) As ElectronicSupportDocument Implements IElectronicSupportDocumentRepository.GetElectronicSupportDocumentByDocumentOrigin
        Dim res As ElectronicSupportDocument
        res = (From esd As ElectronicSupportDocument In Me._context.ElectronicSupportDocument
               Where esd.EntityId = EntityId And esd.EntityName = EntityName
               Select esd).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New ElectronicSupportDocument
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de documentos para ser procesados
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDocumentSupportIdsByConfirmedStatus() As List(Of Object) Implements IElectronicSupportDocumentRepository.GetElectronicDocumentIds
        Dim list As New List(Of Object)
        Dim DocumentSupport = (From esd As ElectronicSupportDocument
                               In Me._context.ElectronicSupportDocument
                               Where esd.StatusElectronic <> 3 And esd.Status = True
                               Select New With {
                                   .EntityId = esd.Id,
                                   .Entity = "ElectronicSupportDocument"
                                   }).ToList()
        Dim AdjustmentNote = (From n As ElectronicSupportDocumentAdjustmentNote
                              In Me._context.ElectronicSupportDocumentAdjustmentNote
                              Where n.StatusElectronic <> 3 And n.Status = 2
                              Select New With {
                                    .EntityId = n.Id,
                                    .Entity = "ElectronicSupportDocumentAdjustmentNote"
                                  }).ToList()
        Dim union = DocumentSupport.Union(AdjustmentNote)
        For Each d In union
            list.Add(New ExpandoObject())
            list(list.Count - 1).EntityId = d.EntityId
            list(list.Count - 1).Entity = d.Entity
        Next
        If list IsNot Nothing AndAlso list.Count > 0 Then
            Return list
        Else
            Return New List(Of Object)
        End If
    End Function

    ''' <summary>
    ''' Se obtienen los detalles relacionados a un documento de soporte electrónico
    ''' </summary>
    ''' <param name="supporDocumentId"></param>
    ''' <returns></returns>
    Function GetDocumentSupportDetails(supporDocumentId As Integer) As List(Of SP_GetDocumentSupportDetailsById_Result) Implements IElectronicSupportDocumentRepository.GetDocumentSupportDetails
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetDocumentSupportDetailsById(supporDocumentId).ToList()
    End Function

    ''' <summary>
    ''' Se obtienen los metodos de pago del documento soporte
    ''' </summary>
    ''' <param name="supporDocumentId"></param>
    ''' <returns></returns>
    Function GetDocumentSupportPaymentMethods(supporDocumentId As Integer) As List(Of SP_GetPaymentMethodByDocumentSupportId_Result) Implements IElectronicSupportDocumentRepository.GetDocumentSupportPaymentMethods
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GetPaymentMethodByDocumentSupportId(supporDocumentId).ToList()
    End Function

    Function SP_GenerateElectronicSupportDocument(AccountsPayableXml As String, Optional AuthorizationResolutionId As Integer? = Nothing) As List(Of SP_GenerateElectronicSupportDocument_Result) Implements IElectronicSupportDocumentRepository.SP_GenerateElectronicSupportDocument
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateElectronicSupportDocument(AccountsPayableXml, AuthorizationResolutionId).ToList()
    End Function

#End Region

End Class
