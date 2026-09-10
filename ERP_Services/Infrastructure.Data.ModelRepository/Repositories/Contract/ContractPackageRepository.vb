'***********************************************************************
' Assembly         : Infrastructure.Data.ContractRepository
' Author           : Giovanny Plazas
' Created          : 24/08/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Public Class ContractPackageRepository
    Inherits GenericRepository(Of ContractPackage)
    Implements IContractPackageRepository

    ''' <summary>
    ''' Contexto 
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una entidad  por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractPackage(code As String) As ContractPackage Implements IContractPackageRepository.GetContractPackage
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As ContractPackage In Me._context.ContractPackage.Include("CUPSEntity").Include("ContractDescriptions").Include("IPSService").Include("ContractPackageProduct").Include("ContractPackageService")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then
            If res.ContractPackageProduct.Count > 0 Then
                For Each Detail In res.ContractPackageProduct
                    Dim ObjProduct = (From x In _context.InventoryProduct.AsNoTracking() Where x.Id = Detail.ProductId Select x).FirstOrDefault
                    Detail.ProductCodName = String.Join(" - ", ObjProduct.Code, ObjProduct.Name)

                    Detail.UnitValueTotal = (Detail.Quantity * Detail.UnitValue)
                Next
            End If

            If res.ContractPackageService.Count > 0 Then
                For Each Detail In res.ContractPackageService
                    Dim ObjCupsEntity = (From x In _context.CUPSEntity.AsNoTracking() Where x.Id = Detail.CUPSEntityId Select x).FirstOrDefault
                    Detail.CupsCodeName = String.Join(" - ", ObjCupsEntity.Code, ObjCupsEntity.Description)

                    Dim ObjContractDescription = (From x In _context.ContractDescriptions Where x.Id = Detail.ContractDescriptionId Select x).FirstOrDefault
                    If ObjContractDescription IsNot Nothing Then
                        Detail.ContractDescriptionName = String.Join(" - ", ObjContractDescription.Code, ObjContractDescription.Name)
                    Else
                        Detail.ContractDescriptionName = String.Empty
                    End If

                    Detail.UnitValueTotal = (Detail.Quantity * Detail.UnitValue)
                Next
            End If

            If res.ContractDescriptions IsNot Nothing Then
                res.ContractDescriptionName = String.Join(" - ", res.ContractDescriptions.Code, res.ContractDescriptions.Name)
            Else
                res.ContractDescriptionName = String.Empty
            End If

            res.CupsCodeName = String.Join(" - ", res.CUPSEntity.Code, res.CUPSEntity.Description)

            res.IPSServicesName = String.Join(" - ", res.IPSService.Code, res.IPSService.Name)


            res.OriginalValue = (From g In _context.ContractPackage.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New ContractPackage()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractPackageById(id As Integer, Optional ByVal tracking As Boolean = True) As ContractPackage Implements IContractPackageRepository.GetContractPackageById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As List(Of ContractPackage)
        If tracking Then
            res = (From d In Me._context.ContractPackage.Include("BillingConcept").AsNoTracking().Include("BillingConcept.BillingConceptAccount").AsNoTracking() Where d.Id = id Select d).ToList
        Else
            res = (From d In Me._context.ContractPackage.AsNoTracking().Include("CupsSubgroup").AsNoTracking().Include("BillingConcept").AsNoTracking().Include("BillingConcept.BillingConceptAccount").AsNoTracking() Where d.Id = id Select d).ToList
        End If
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As ContractPackage In Me._context.ContractPackage.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New ContractPackage()
        End If
    End Function

End Class
