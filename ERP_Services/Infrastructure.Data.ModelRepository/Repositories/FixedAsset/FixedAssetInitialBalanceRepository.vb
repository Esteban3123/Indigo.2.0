'************************************************************
' Assembly         : Infrastructure.Data.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Class FixedAssetInitialBalanceRepository
    Inherits GenericRepository(Of FixedAssetInitialBalance)
    Implements IFixedAssetInitialBalanceRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene un saldo inicial por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetInitialBalance(Code As String) As FixedAssetInitialBalance Implements IFixedAssetInitialBalanceRepository.GetFixedAssetInitialBalance
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d In Me._context.FixedAssetInitialBalance.Include("FixedAssetInitialBalanceItem").Include("FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook").Include("FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts").Include("FixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook")
                   Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.FixedAssetInitialBalanceItem IsNot Nothing And res.FixedAssetInitialBalanceItem.Count > 0 Then

                ' OPTIMIZACIÓN: Obtener todos los IDs únicos necesarios primero
                Dim itemIds = res.FixedAssetInitialBalanceItem.Select(Function(x) x.ItemId).Distinct().ToList()
                Dim locationIds = res.FixedAssetInitialBalanceItem.Select(Function(x) x.LocationId).Distinct().ToList()
                Dim responsibleIds = res.FixedAssetInitialBalanceItem.Select(Function(x) x.ResponsibleId).Distinct().ToList()
                Dim supplierIds = res.FixedAssetInitialBalanceItem.Select(Function(x) x.SupplierId).Distinct().ToList()
                Dim trademarkIds = res.FixedAssetInitialBalanceItem.Select(Function(x) x.TrademarkId).Distinct().ToList()
                Dim policyIds = res.FixedAssetInitialBalanceItem.Where(Function(x) x.PolicyId.HasValue).Select(Function(x) x.PolicyId.Value).Distinct().ToList()
                Dim statusAssetIds = res.FixedAssetInitialBalanceItem.Select(Function(x) x.StatusAssetId).Distinct().ToList()

                ' Obtener IDs de libros legales de los detalles
                Dim detailBookLegalBookIds As New List(Of Integer)()
                For Each item In res.FixedAssetInitialBalanceItem
                    If item.FixedAssetInitialBalanceItemDetailBook IsNot Nothing Then
                        detailBookLegalBookIds.AddRange(item.FixedAssetInitialBalanceItemDetailBook.Select(Function(x) x.LegalBookId))
                    End If
                    If item.FixedAssetInitialBalanceItemParts IsNot Nothing Then
                        For Each part In item.FixedAssetInitialBalanceItemParts
                            If part.FixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing Then
                                detailBookLegalBookIds.AddRange(part.FixedAssetInitialBalanceItemPartsDetailBook.Select(Function(x) x.LegalBookId))
                            End If
                        Next
                    End If
                Next
                Dim legalBookIds = detailBookLegalBookIds.Distinct().ToList()

                ' Obtener IDs de partes/accesorios
                Dim partIds As New List(Of Integer)()
                For Each item In res.FixedAssetInitialBalanceItem
                    If item.FixedAssetInitialBalanceItemParts IsNot Nothing Then
                        partIds.AddRange(item.FixedAssetInitialBalanceItemParts.Select(Function(x) x.PartAccesoriesConsumablesId))
                    End If
                Next
                Dim uniquePartIds = partIds.Distinct().ToList()

                'Cargar todas las entidades relacionadas en una sola consulta por tipo
                Dim itemsDict = (From c In _context.FixedAssetItem.AsNoTracking Where itemIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id)
                Dim locationsDict = (From c In _context.FixedAssetLocation.AsNoTracking Where locationIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id)
                Dim responsiblesDict = (From c In _context.FixedAssetResponsible.AsNoTracking Where responsibleIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id)
                Dim suppliersDict = (From c In _context.Supplier.AsNoTracking Where supplierIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id)
                Dim trademarksDict = (From c In _context.FixedAssetTrademark.AsNoTracking Where trademarkIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id)
                Dim policiesDict = If(policyIds.Count > 0, (From c In _context.FixedAssetPolicy.AsNoTracking Where policyIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id), New Dictionary(Of Integer, Object)())
                Dim statusAssetsDict = (From c In _context.FixedAssetStatusAsset.AsNoTracking Where statusAssetIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id)
                Dim legalBooksDict = If(legalBookIds.Count > 0, (From c In _context.LegalBook.AsNoTracking Where legalBookIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id), New Dictionary(Of Integer, Object)())
                Dim partsDict = If(uniquePartIds.Count > 0, (From c In _context.FixedAssetPartsAccesoriesConsumables.AsNoTracking Where uniquePartIds.Contains(c.Id) Select c).ToDictionary(Function(x) x.Id), New Dictionary(Of Integer, Object)())

                'Asignar valores usando lookups en memoria
                For Each ObjFixedAssetInitialBalanceItem As FixedAssetInitialBalanceItem In res.FixedAssetInitialBalanceItem

                    Dim ObjItem = itemsDict(ObjFixedAssetInitialBalanceItem.ItemId)
                    Dim ObjLocation = locationsDict(ObjFixedAssetInitialBalanceItem.LocationId)
                    Dim ObjResponsible = responsiblesDict(ObjFixedAssetInitialBalanceItem.ResponsibleId)
                    Dim ObjSupplier = suppliersDict(ObjFixedAssetInitialBalanceItem.SupplierId)
                    Dim ObjTrademark = trademarksDict(ObjFixedAssetInitialBalanceItem.TrademarkId)
                    Dim ObjStatusAsset = statusAssetsDict(ObjFixedAssetInitialBalanceItem.StatusAssetId)

                    ObjFixedAssetInitialBalanceItem.ItemCodeName = ObjItem.Code + " - " + ObjItem.Description
                    ObjFixedAssetInitialBalanceItem.LocationCodeName = ObjLocation.Code + " - " + ObjLocation.Name
                    ObjFixedAssetInitialBalanceItem.ResponsibleCodeName = ObjResponsible.Code + " - " + ObjResponsible.ThirdPartyName
                    ObjFixedAssetInitialBalanceItem.SupplierCodeName = ObjSupplier.Code + " - " + ObjSupplier.Name
                    ObjFixedAssetInitialBalanceItem.TrademarkCodeName = ObjTrademark.Code + " - " + ObjTrademark.Name
                    ObjFixedAssetInitialBalanceItem.StatusAssetCodeName = ObjStatusAsset.Code + " - " + ObjStatusAsset.Name

                    If ObjFixedAssetInitialBalanceItem.PolicyId.HasValue AndAlso policiesDict.ContainsKey(ObjFixedAssetInitialBalanceItem.PolicyId.Value) Then
                        Dim ObjPolicy = policiesDict(ObjFixedAssetInitialBalanceItem.PolicyId.Value)
                        ObjFixedAssetInitialBalanceItem.PolicyCodeName = ObjPolicy.Code + " - " + ObjPolicy.Name
                    End If

                    If ObjFixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook IsNot Nothing And ObjFixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook.Count > 0 Then
                        For Each ObjFixedAssetInitialBalanceItemDetailBook As FixedAssetInitialBalanceItemDetailBook In ObjFixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemDetailBook
                            If legalBooksDict.ContainsKey(ObjFixedAssetInitialBalanceItemDetailBook.LegalBookId) Then
                                Dim ObjLegalBook = legalBooksDict(ObjFixedAssetInitialBalanceItemDetailBook.LegalBookId)
                                ObjFixedAssetInitialBalanceItemDetailBook.LegalBookCodeName = ObjLegalBook.Code + " - " + ObjLegalBook.Name
                            End If
                        Next
                    End If

                    If ObjFixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts IsNot Nothing And ObjFixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts.Count > 0 Then
                        For Each ObjFixedAssetInitialBalanceItemParts As FixedAssetInitialBalanceItemParts In ObjFixedAssetInitialBalanceItem.FixedAssetInitialBalanceItemParts

                            If partsDict.ContainsKey(ObjFixedAssetInitialBalanceItemParts.PartAccesoriesConsumablesId) Then
                                Dim ObjPartAccesoriesConsumables = partsDict(ObjFixedAssetInitialBalanceItemParts.PartAccesoriesConsumablesId)
                                ObjFixedAssetInitialBalanceItemParts.PartAccesoriesConsumablesCodeName = ObjPartAccesoriesConsumables.Code + " - " + ObjPartAccesoriesConsumables.Name
                            End If

                            If ObjFixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing And ObjFixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook.Count() > 0 Then
                                For Each ObjFixedAssetInitialBalanceItemPartsDetailBook As FixedAssetInitialBalanceItemPartsDetailBook In ObjFixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook
                                    If legalBooksDict.ContainsKey(ObjFixedAssetInitialBalanceItemPartsDetailBook.LegalBookId) Then
                                        Dim ObjLegalBook = legalBooksDict(ObjFixedAssetInitialBalanceItemPartsDetailBook.LegalBookId)
                                        ObjFixedAssetInitialBalanceItemPartsDetailBook.LegalBookCodeName = ObjLegalBook.Code + " - " + ObjLegalBook.Name
                                    End If
                                Next
                            End If

                        Next
                    End If

                Next

            End If

            res.OriginalValue = (From d In Me._context.FixedAssetInitialBalance.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetInitialBalance()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un saldo inicial por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetInitialBalanceById(Id As Integer) As FixedAssetInitialBalance Implements IFixedAssetInitialBalanceRepository.GetFixedAssetInitialBalanceById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.FixedAssetInitialBalance Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.FixedAssetInitialBalance.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetInitialBalance()
        End If
    End Function

    ''' <summary>
    ''' Elimina todos los detalles del saldo inicial
    ''' </summary>
    ''' <param name="ListString"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveFixedAssetInitialBalance(ListString As List(Of String), codeUser As String) As SP_SaveFixedAssetInitialBalance_Result Implements IFixedAssetInitialBalanceRepository.SP_SaveFixedAssetInitialBalance
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveFixedAssetInitialBalance(ListString(0), ListString(1), ListString(2), ListString(3), codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Inserta en las tablas de physicalAsset cuando se confirma el saldo inicial
    ''' </summary>
    ''' <param name="FixedAssetInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConfirmFixedAssetInitialBalance(FixedAssetInitialBalanceId As Integer) As SP_ConfirmFixedAssetInitialBalance_Result Implements IFixedAssetInitialBalanceRepository.SP_ConfirmFixedAssetInitialBalance
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmFixedAssetInitialBalance(FixedAssetInitialBalanceId).SingleOrDefault
    End Function

    ''' <summary>
    ''' Copiar y pegar de saldos iniciales de activos fijos
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Public Function SP_CopyAndPasteFixedAssetInitialBalance(XmlObject As String) As List(Of SP_CopyAndPasteFixedAssetInitialBalance_Result) Implements IFixedAssetInitialBalanceRepository.SP_CopyAndPasteFixedAssetInitialBalance
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteFixedAssetInitialBalance(XmlObject).ToList()
    End Function

End Class
