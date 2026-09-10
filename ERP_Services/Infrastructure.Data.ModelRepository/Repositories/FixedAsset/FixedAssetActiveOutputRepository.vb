'************************************************************
' Assembly         : Infrastructure.Data.FixedAssetRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/05/2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports System.Data.Entity.Infrastructure
Imports System.Text

#End Region

Public Class FixedAssetActiveOutputRepository
    Inherits GenericRepository(Of FixedAssetActiveOutput)
    Implements IFixedAssetActiveOutputRepository

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
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetActiveOutput(code As String) As FixedAssetActiveOutput Implements IFixedAssetActiveOutputRepository.GetFixedAssetActiveOutput
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d In Me._context.FixedAssetActiveOutput.Include("FixedAssetActiveOutputDetail") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.FixedAssetActiveOutputDetail IsNot Nothing AndAlso res.FixedAssetActiveOutputDetail.Count > 0 Then
                For Each item In res.FixedAssetActiveOutputDetail

                    If item.PhysicalAssetId IsNot Nothing Then
                        Dim physical = (From p In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetItem").Include("FixedAssetItem.FixedAssetItemCatalog").AsNoTracking Where p.Id = item.PhysicalAssetId Select p).FirstOrDefault
                        item.PhysicalAssetDescription = physical.Plate + " - " + physical.FixedAssetItem.Code + " - " + physical.FixedAssetItem.Description
                        item.HistoricalValue = physical.HistoricalValue
                        item.ClassificationFixedAsset = physical.FixedAssetItem.FixedAssetItemCatalog.Classification
                    End If

                    If item.PhysicalAssetPartsId IsNot Nothing Then
                        Dim physicalPart = (From p In _context.FixedAssetPhysicalAssetParts.AsNoTracking.Include("FixedAssetPartsAccesoriesConsumables").AsNoTracking Where p.Id = item.PhysicalAssetPartsId Select p).FirstOrDefault
                        item.PhysicalAssetPartDescription = physicalPart.FixedAssetPartsAccesoriesConsumables.Code + " - " + physicalPart.FixedAssetPartsAccesoriesConsumables.Name
                        item.HistoricalValue = physicalPart.HistoricalValue
                    End If

                    Dim mainAccount = (From m In _context.MainAccounts.AsNoTracking Where m.Id = item.MainAccountId Select m).FirstOrDefault
                    item.MainAccountNumberName = mainAccount.Number + " - " + mainAccount.Name

                    If item.ThirdPartyId IsNot Nothing Then
                        Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = item.ThirdPartyId Select t).FirstOrDefault
                        item.ThirdPartyNitName = thirdParty.Nit + " - " + thirdParty.Name
                    End If

                    If item.LowType > 0 Then
                        Dim LowType = (From x In _context.FixedAssetRetirementTypes.AsNoTracking Where x.Id = item.LowType Select x).FirstOrDefault
                        item.LowTypeName = LowType.Code + " - " + LowType.Name
                    End If

                Next
            End If

            res.OriginalValue = (From d In Me._context.FixedAssetActiveOutput.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetActiveOutput()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetActiveOutputById(Id As Integer) As FixedAssetActiveOutput Implements IFixedAssetActiveOutputRepository.GetFixedAssetActiveOutputById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d In Me._context.FixedAssetActiveOutput Where d.Id = Id Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d In Me._context.FixedAssetActiveOutput.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res
        Else
            Return New FixedAssetActiveOutput()
        End If
    End Function

    ''' <summary>
    ''' Obtiene el activo por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetById(Id As Integer, Optional IsTracking As Boolean = False) As FixedAssetPhysicalAsset Implements IFixedAssetActiveOutputRepository.GetFixedAssetPhysicalAssetById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        If IsTracking = False Then
            Return (From p In _context.FixedAssetPhysicalAsset.AsNoTracking.Include("FixedAssetPhysicalAssetDetailBook").AsNoTracking.Include("FixedAssetResponsible").AsNoTracking.Include("FixedAssetLocation").AsNoTracking.Include("FixedAssetLocation.FunctionalUnit").AsNoTracking.Include("FixedAssetItem").AsNoTracking.Include("FixedAssetItem.FixedAssetItemDetail").AsNoTracking.Include("FixedAssetItem.FixedAssetItemCatalog").AsNoTracking
                    Where p.Id = Id Select p).FirstOrDefault
        Else
            Return (From p In _context.FixedAssetPhysicalAsset.Include("FixedAssetPhysicalAssetDetailBook").Include("FixedAssetResponsible").Include("FixedAssetLocation").Include("FixedAssetLocation.FunctionalUnit").Include("FixedAssetItem").Include("FixedAssetItem.FixedAssetItemDetail").Include("FixedAssetItem.FixedAssetItemCatalog").Include("FixedAssetItem.FixedAssetItemCatalog.FixedAssetItemCatalogDetail")
                    Where p.Id = Id Select p).FirstOrDefault
        End If
    End Function

    ''' <summary>
    ''' Obtiene los ids de los libros de los cuales tenga permiso para realizar salidas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetListLegalBookIds() As List(Of Integer) Implements IFixedAssetActiveOutputRepository.GetListLegalBookIds
        Return (From x In _context.VieBot.AsNoTracking() Where x.Form = "FixedAssetActiveOutput" Select x.LegalBookId).ToList()
    End Function

    ''' <summary>
    ''' Obtiene parametro de contabilidad por id de la unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Public Function GetSettingGeneralLedgerByOperatingUnitId(OperatingUnitId As Integer) As GeneralLedgerSettings Implements IFixedAssetActiveOutputRepository.GetSettingGeneralLedgerByOperatingUnitId
        Return (From s In _context.GeneralLedgerSettings.AsNoTracking Where s.IdOperatingUnit = OperatingUnitId Select s).FirstOrDefault
    End Function

    ''' <summary>
    ''' Confirma la salida de activos
    ''' </summary>
    ''' <param name="FixedAssetActiveOutputXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveFixedAssetActiveOutput(FixedAssetActiveOutputXml As String, codeUser As String) As SP_ConfirmFixedAssetActiveOutput_Result Implements IFixedAssetActiveOutputRepository.SP_SaveFixedAssetActiveOutput
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmFixedAssetActiveOutput(FixedAssetActiveOutputXml, codeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Metodo que valida que el articulo no este dentro de otra salida confirmada
    ''' </summary>
    ''' <param name="ListDetail"></param>
    ''' <returns></returns>
    Public Function ValidateItemInOutput(ListDetail As List(Of FixedAssetActiveOutputDetail)) As String Implements IFixedAssetActiveOutputRepository.ValidateItemInOutput
        'Mensaje que se devuelve
        Dim stringBuilder As New StringBuilder

        'Listado para realizar la validación
        Dim ListIds = (From x In ListDetail
                       Where x.ChangeTracker.State <> ObjectState.Deleted AndAlso x.PhysicalAssetId IsNot Nothing
                       Select x.PhysicalAssetId).ToList()

        If ListIds IsNot Nothing AndAlso ListIds.Any Then

            Dim ListPhysical = (From detail In _context.FixedAssetActiveOutputDetail.AsNoTracking()
                                Join header In _context.FixedAssetActiveOutput.AsNoTracking() On header.Id Equals detail.FixedAssetActiveOutputId
                                Where ListIds.Contains(detail.PhysicalAssetId) And header.Status = 2
                                Select New OutputDetailWithHeaderDTO With {
                                .Detail = detail,
                                .Header = header
                    }).ToList()

            If ListPhysical IsNot Nothing AndAlso ListPhysical.Count > 0 Then
                ListPhysical.ForEach(Sub(x)
                                         Dim item = (From y In ListDetail Where y.PhysicalAssetId = x.Detail.PhysicalAssetId).FirstOrDefault()
                                         stringBuilder.AppendLine("El articulo " + item.PhysicalAssetDescription + " se encuentra en una salida ya confirmada (" + x.Header.Code + ")")
                                     End Sub)
            End If
        End If
        Return stringBuilder.ToString()
    End Function

End Class

Public Class OutputDetailWithHeaderDTO
    Public Property Detail As FixedAssetActiveOutputDetail
    Public Property Header As FixedAssetActiveOutput
End Class

