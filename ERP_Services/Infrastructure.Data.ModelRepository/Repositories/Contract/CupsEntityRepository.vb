'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Public Class CupsEntityRepository
    Inherits GenericRepository(Of CUPSEntity)
    Implements ICupsEntityRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsEntity(code As String) As CUPSEntity Implements ICupsEntityRepository.GetCupsEntity
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CUPSEntity In Me._context.CUPSEntity.Include("CUPSEntityPanelDetail.CUPSEntity1")
                   Where d.Code.Equals(code.Trim())
                   Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim cupsSubGroup = (From cs In _context.CupsSubgroup.AsNoTracking Where cs.Id = res.CUPSSubGroupId Select cs).FirstOrDefault
            res.CupsSubGroupDescription = cupsSubGroup.Code + " - " + cupsSubGroup.Name

            Dim ipsServiceGroup = (From isg In _context.BillingConcept.AsNoTracking Where isg.Id = res.BillingConceptId Select isg).FirstOrDefault
            res.IPSServiceGroupDescription = ipsServiceGroup.Code + " - " + ipsServiceGroup.Name

            Dim billingGroup = (From bg In _context.BillingGroup.AsNoTracking Where bg.Id = res.BillingGroupId Select bg).FirstOrDefault
            res.BillingGroupDescription = billingGroup.Code + " - " + billingGroup.Name

            If res.RIASBillingConceptId IsNot Nothing Then
                res.RIASBillingConceptCodeName = (From isg In _context.BillingConcept.AsNoTracking Where isg.Id = res.RIASBillingConceptId Select isg.Code + " - " + isg.Name).FirstOrDefault
            End If

            If res.RIASBillingGroupId IsNot Nothing Then
                res.RIASBillingGroupCodeName = (From bg In _context.BillingGroup.AsNoTracking Where bg.Id = res.RIASBillingGroupId Select bg.Code + " - " + bg.Name).FirstOrDefault
            End If

            If res?.CUPSEntityPanelDetail?.Any() Then
                res?.CUPSEntityPanelDetail?.ToList.ForEach(Sub(x)
                                                               x.CUPSCode = x.CUPSEntity1?.Code
                                                               x.CUPSName = x.CUPSEntity1?.Description
                                                           End Sub)
            End If

            res.OriginalValue = (From g In _context.CUPSEntity.AsNoTracking
                                 Where g.Code.Equals(code.Trim())
                                 Select g).FirstOrDefault

            Return res
        Else
            Return New CUPSEntity()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsEntityById(id As Integer, Optional ByVal tracking As Boolean = True) As CUPSEntity Implements ICupsEntityRepository.GetCupsEntityById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res As List(Of CUPSEntity)
        If tracking Then
            res = (From d In Me._context.CUPSEntity.Include("BillingConcept").AsNoTracking().Include("BillingConcept.BillingConceptAccount").AsNoTracking() Where d.Id = id Select d).ToList
        Else
            res = (From d In Me._context.CUPSEntity.AsNoTracking().Include("CupsSubgroup").AsNoTracking().Include("BillingConcept").AsNoTracking().Include("BillingConcept.BillingConceptAccount").AsNoTracking() Where d.Id = id Select d).ToList
        End If
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As CUPSEntity In Me._context.CUPSEntity.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New CUPSEntity()
        End If
    End Function

    ''' <summary>
    ''' Consulta la entidad cups por id con asNoTracking y sin agregados
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCupsEntityByIdSimple(id As Integer) As CUPSEntity Implements ICupsEntityRepository.GetCupsEntityByIdSimple
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From c In _context.CUPSEntity.AsNoTracking.Include("CupsSubgroup").AsNoTracking Where c.Id = id Select c).FirstOrDefault
        Return res
    End Function

    ''' <summary>
    ''' Gets the cups entity by identifier includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Public Function GetCupsEntityByIdIncludes(id As Integer, includes() As String) As CUPSEntity Implements ICupsEntityRepository.GetCupsEntityByIdIncludes
        Dim query = _context.CUPSEntity.AsNoTracking().Where(Function(o) o.Id = id).AsQueryable()
        If includes IsNot Nothing Then
            For Each inc In includes
                query = query.Include(inc).AsNoTracking()
            Next
        End If
        Return query.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene una lista de cups por codigos
    ''' </summary>
    ''' <param name="ListCode"></param>
    ''' <returns></returns>
    Public Function GetListCupsEntityBycodes(ListCode As List(Of String)) As List(Of CUPSEntity) Implements ICupsEntityRepository.GetListCupsEntityBycodes
        Dim res = (From i In _context.CUPSEntity.AsNoTracking() Where ListCode.Contains(i.Code) Select i).ToList()
        If res IsNot Nothing Then
            Return res
        Else
            Return New List(Of CUPSEntity)
        End If
    End Function

    ''' <summary>
    ''' Permite guardar y actualizar los cups
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveCupsEntity(Xml As String, CodeUser As String) As SP_SaveCupsEntity_Result Implements ICupsEntityRepository.SP_SaveCupsEntity
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCupsEntity(Xml, CodeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Valida la descripción antes de eliminarse
    ''' </summary>
    ''' <param name="CUPSEntityContractDescriptionId"></param>
    ''' <returns></returns>
    Public Function SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId As Integer) As SP_ValidateDescriptionsInCrystal_Result Implements ICupsEntityRepository.SP_ValidateDescriptionsInCrystal
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId).SingleOrDefault
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="quotationServiceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetQuotationServiceOrderDetailById(quotationServiceOrderDetailId As Integer) As QuotationServiceOrderDetail Implements ICupsEntityRepository.GetQuotationServiceOrderDetailById
        Return (From q In _context.QuotationServiceOrderDetail.AsNoTracking().Include("QuotationServiceOrderDetailSurgical").AsNoTracking()
                Where q.Id = quotationServiceOrderDetailId Select q).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Consulta el id de la descripción relacionada
    ''' </summary>
    ''' <param name="CupsEntityContractDescriptionId"></param>
    ''' <returns></returns>
    Public Function GetContractDescriptionIdByCupsEntityContractDescription(CupsEntityContractDescriptionId As Integer) As Integer? Implements ICupsEntityRepository.GetContractDescriptionIdByCupsEntityContractDescription
        Return (From x In _context.CUPSEntityContractDescriptions.AsNoTracking() Where x.Id = CupsEntityContractDescriptionId Select x.ContractDescriptionId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Valida el cups cuando se agrega una descripción
    ''' </summary>
    ''' <param name="CUPSEntityCode"></param>
    ''' <returns></returns>
    Public Function SP_ValidateCUPSInCrystal(CUPSEntityCode As String) As SP_ValidateCUPSInCrystal_Result Implements ICupsEntityRepository.SP_ValidateCUPSInCrystal
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ValidateCUPSInCrystal(CUPSEntityCode).SingleOrDefault
    End Function


    ''' <summary>
    ''' consulta la relacion entre CUPS y descripcion relacionada
    ''' </summary>
    ''' <param name="CupsCode"></param>
    ''' <param name="CodeDescription"></param>
    ''' <returns></returns>
    Public Function GetListCupsEntityContractDescription(CupsCode As String, CodeDescription As String) As List(Of CUPSEntityContractDescriptions) Implements ICupsEntityRepository.GetListCupsEntityContractDescription
        Dim res = (From x In _context.CUPSEntityContractDescriptions.AsNoTracking().Include("ContractDescriptions").Include("CUPSEntity") Where CupsCode = x.CUPSEntity.Code And CodeDescription = x.ContractDescriptions.Code Select x).ToList()
        If res Is Nothing Then
            Return New List(Of CUPSEntityContractDescriptions)
        Else
            Return res
        End If
    End Function

    ''' <summary>
    ''' Consulta el id de la descripción relacionada
    ''' </summary>
    ''' <param name="CupsCode"></param>
    ''' <returns></returns>
    Public Function GetCupsEntityWithContractDescriptions(CupsCode As String) As CUPSEntity Implements ICupsEntityRepository.GetCupsEntityWithContractDescriptions
        Dim cUPSEntity = (From ce In _context.CUPSEntity.AsNoTracking().
                        Include("CupsSubgroup").AsNoTracking().
                        Include("CupsSubgroup.CupsGroup").AsNoTracking().
                        Include("CUPSEntityContractDescriptions").AsNoTracking().
                        Include("CUPSEntityContractDescriptions.ContractDescriptions").AsNoTracking()
                          Where CupsCode = ce.Code
                          Select ce).FirstOrDefault

        If cUPSEntity Is Nothing Then
            cUPSEntity = New CUPSEntity
        End If

        Return cUPSEntity
    End Function

    Public Function GetListCupsEntityWithContractDescriptions(listCupsCode As List(Of String)) As List(Of CUPSEntity) Implements ICupsEntityRepository.GetListCupsEntityWithContractDescriptions
        If listCupsCode Is Nothing OrElse listCupsCode.Count = 0 Then
            Return New List(Of CUPSEntity)
        End If
        Return (From ce In _context.CUPSEntity.AsNoTracking().
                        Include("CUPSEntityContractDescriptions").AsNoTracking().
                        Include("CUPSEntityContractDescriptions.ContractDescriptions").AsNoTracking()
                Where listCupsCode.Contains(ce.Code)
                Select ce).ToList()
    End Function

End Class
