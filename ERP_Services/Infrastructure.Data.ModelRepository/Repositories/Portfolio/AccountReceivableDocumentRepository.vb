'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Juan Carlos Bermudez 
' Created          : 11-06-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class AccountReceivableDocumentRepository
    Inherits GenericRepository(Of AccountReceivableDocument)
    Implements IAccountReceivableDocumentRepository

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
    ''' Obtener un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableDocumentById(id As Integer) As AccountReceivableDocument Implements IAccountReceivableDocumentRepository.GetAccountReceivableDocumentById

        Dim res = (From ard In Me._context.AccountReceivableDocument.AsNoTracking().Include("AccountReceivableDocumentDetail").AsNoTracking() Where ard.Id = id Select ard).FirstOrDefault()
        If res IsNot Nothing Then

            Dim customer = (From c In Me._context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c).FirstOrDefault()
            res.DescriptionCustomer = customer.Nit & " - " & customer.Name

            Dim account = (From a In Me._context.MainAccounts.AsNoTracking() Where a.Id = res.MainAccountId Select a).FirstOrDefault()
            res.DescriptionAccount = account.Number & " - " & account.Name

            If res.CostCenterId IsNot Nothing Then
                Dim costCenter = (From cc In Me._context.CostCenter.AsNoTracking() Where cc.Id = res.CostCenterId Select cc).FirstOrDefault()
                res.DescriptionCostCenter = costCenter.Code & " - " & costCenter.Name
            End If

            Return res
        Else
            Return New AccountReceivableDocument
        End If
    End Function

    ''' <summary>
    ''' Obtener un documento de cuenta x cobrar por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableDocumentByCode(code As String) As AccountReceivableDocument Implements IAccountReceivableDocumentRepository.GetAccountReceivableDocumentByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From ard In Me._context.AccountReceivableDocument.Include("AccountReceivableDocumentDetail").Include("Currency") _
                       .Include("AccountReceivableDocumentDetail.AccountReceivableConcept") _
                       .Include("AccountReceivableDocumentDetail.AccountReceivableConcept.MainAccounts") Where ard.Code = code Select ard).FirstOrDefault()
        If res IsNot Nothing Then

            Dim customer = (From c In Me._context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c).FirstOrDefault()
            res.DescriptionCustomer = customer.Nit & " - " & customer.Name

            Dim account = (From a In Me._context.MainAccounts.AsNoTracking() Where a.Id = res.MainAccountId Select a).FirstOrDefault()
            res.DescriptionAccount = account.Number & " - " & account.Name

            If res.CostCenterId IsNot Nothing Then
                Dim costCenter = (From cc In Me._context.CostCenter.AsNoTracking() Where cc.Id = res.CostCenterId Select cc).FirstOrDefault()
                res.DescriptionCostCenter = costCenter.Code & " - " & costCenter.Name
            End If

            If res.AccountReceivableDocumentDetail IsNot Nothing AndAlso res.AccountReceivableDocumentDetail.Count > 0 Then
                Dim dictionaryAccountReceivableConcept As New Dictionary(Of Integer, String)
                Dim dictionaryThirdParty As New Dictionary(Of Integer, String)
                Dim dictionaryAccount As New Dictionary(Of Integer, String)
                Dim dictionaryCostCenter As New Dictionary(Of Integer, String)
                For Each item As AccountReceivableDocumentDetail In res.AccountReceivableDocumentDetail
                    'llenar el campo de code y nombre de concepto de cuenta x cobrar del detalle
                    If dictionaryAccountReceivableConcept.ContainsKey(item.AccountReceivableConceptId) Then
                        item.DescriptionAccountReceivableConcept = dictionaryAccountReceivableConcept(item.AccountReceivableConceptId)
                    Else
                        Dim accountReceivableConcept = (From c In Me._context.AccountReceivableConcept.AsNoTracking() Where c.Id = item.AccountReceivableConceptId Select c).FirstOrDefault
                        item.DescriptionAccountReceivableConcept = accountReceivableConcept.Code + " - " + accountReceivableConcept.Name
                        dictionaryAccountReceivableConcept.Add(item.AccountReceivableConceptId, item.DescriptionAccountReceivableConcept)
                    End If

                    'llenar el campo de code y nombre de cuenta del detalle
                    If dictionaryAccount.ContainsKey(item.MainAccountId) Then
                        item.DescriptionAccount = dictionaryAccount(item.MainAccountId)
                    Else
                        Dim mainAccount = (From ma In Me._context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select ma).FirstOrDefault
                        item.DescriptionAccount = mainAccount.Number + " - " + mainAccount.Name
                        dictionaryAccount.Add(item.MainAccountId, item.DescriptionAccount)
                    End If

                    'llenar el campo de code y nombre de tercero del detalle
                    If item.ThirdPartyId IsNot Nothing Then
                        If dictionaryThirdParty.ContainsKey(item.ThirdPartyId) Then
                            item.DescriptionThirdParty = dictionaryThirdParty(item.ThirdPartyId)
                        Else
                            Dim thirdParty = (From tp In Me._context.ThirdParty.AsNoTracking() Where tp.Id = item.ThirdPartyId Select tp).FirstOrDefault
                            item.DescriptionThirdParty = thirdParty.Nit + " - " + thirdParty.Name
                            dictionaryThirdParty.Add(item.ThirdPartyId, item.DescriptionThirdParty)
                        End If
                    End If

                    'llenar el campo de code y nombre de centro de costo del detalle
                    If item.CostCenterId IsNot Nothing Then
                        If dictionaryCostCenter.ContainsKey(item.CostCenterId) Then
                            item.DescriptionCostCenter = dictionaryCostCenter(item.CostCenterId)
                        Else
                            Dim costCenter = (From cc In Me._context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select cc).FirstOrDefault
                            item.DescriptionCostCenter = costCenter.Code + " - " + costCenter.Name
                            dictionaryCostCenter.Add(item.CostCenterId, item.DescriptionCostCenter)
                        End If
                    End If
                Next
            End If

            res.OriginalValue = (From ardo In _context.AccountReceivableDocument.AsNoTracking() Where ardo.Code = code Select ardo).FirstOrDefault()
            Return res
        Else
            Return New AccountReceivableDocument
        End If
    End Function

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivableDocumentMassiveConfirm(listDocuments As List(Of String)) As List(Of AccountReceivableDocument) Implements IAccountReceivableDocumentRepository.ListAccountReceivableDocumentMassiveConfirm
        Return (From cr In _context.AccountReceivableDocument.Include("AccountReceivableDocumentDetail") Where listDocuments.Contains(cr.Code) Select cr).ToList()
    End Function

    ''' <summary>
    ''' Guarda, Actualiza o Confirma un documento de cuenta por cobrar
    ''' </summary>
    ''' <param name="accountReceivableDocumentXml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveAccountReceivableDocument(accountReceivableDocumentXml As String, userCode As String) As SP_SaveAccountReceivableDocument_Result Implements IAccountReceivableDocumentRepository.SP_SaveAccountReceivableDocument
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAccountReceivableDocument(accountReceivableDocumentXml, userCode).SingleOrDefault()
    End Function

End Class
