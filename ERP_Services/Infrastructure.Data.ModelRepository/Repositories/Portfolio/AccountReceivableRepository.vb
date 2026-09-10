'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Carlos ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AccountReceivableRepository
    Inherits GenericRepository(Of AccountReceivable)
    Implements IAccountReceivableRepository

#Region "Context"
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builder"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' se usa para consultar la factura por ir para validar en las notas de cuentas por cobrar
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableById(idAccountReceivable As Integer) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableById
        Return (From ar In _context.AccountReceivable.Include("AccountReceivableShare").Include("AccountReceivableAccounting") Where ar.Id = idAccountReceivable Select ar).FirstOrDefault()
    End Function

    Public Function GetAccountReceivableByInvoiceNumber(invoiceNumber As String, idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer, idOperatingUnit As Integer) As List(Of AccountReceivable) Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceNumber
        Dim status = 2
        Dim balance = 0
        Dim colorAge As Integer = -1
        Dim listInvoice As New List(Of AccountReceivable)
        If idCostCenter = 0 Then
            listInvoice = (From ar In _context.AccountReceivable Join ara In _context.AccountReceivableAccounting On
                       ar.Id Equals ara.AccountReceivableId Where ar.InvoiceNumber = invoiceNumber And ar.Status = status And ar.ThirdPartyId = idThirdParty And ara.Balance > balance And ara.MainAccountId = idMainAccount
                           Select ar).ToList()
        Else
            listInvoice = (From ar In _context.AccountReceivable Join ara In _context.AccountReceivableAccounting On
                       ar.Id Equals ara.AccountReceivableId Where ar.InvoiceNumber = invoiceNumber And ar.Status = status And ar.ThirdPartyId = idThirdParty And ara.Balance > balance And ara.MainAccountId = idMainAccount And ara.CostCenterId = idCostCenter
                           Select ar).ToList()
        End If
        For Each item In listInvoice
            item.Balance = (From arc In _context.AccountReceivableAccounting.AsNoTracking() Where arc.AccountReceivableId = item.Id And arc.MainAccountId = idMainAccount Select arc).FirstOrDefault().Balance
            Dim setting = (From sp In _context.SettingPortfolio.AsNoTracking().Include("AgesPortfolio").AsNoTracking() Where sp.OperatingUnitId = idOperatingUnit Select sp).FirstOrDefault()
            Dim daysExpire As Integer = (CDate(item.ExpiredDate) - DateTime.Now).TotalDays
            If daysExpire > 0 Then
                colorAge = (From ap In setting.AgesPortfolio Where ap.InitialRange <= daysExpire And ap.EndRange >= daysExpire Select ap.Color).FirstOrDefault()
            End If
            item.Age = colorAge
        Next
        Return listInvoice
    End Function

    ''' <summary>
    ''' obtiene una factura por numero y por id del cliente
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="idCustomer"></param>
    ''' <returns></returns>
    Public Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceNumberAndCustomer
        Return (From ar In _context.AccountReceivable.AsNoTracking() Where ar.InvoiceNumber = invoiceNumber And ar.CustomerId = idCustomer Select ar).FirstOrDefault()
    End Function

    ''' <summary>
    ''' obtiene una factura por numero. Metodo usuado en la radicacion de cuentas de cobro. Glosas filtrando los tipos de factura Basica y Ley 100
    ''' </summary>
    ''' <param name="invoicenumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableByInvoiceNumberGloss(ByVal invoicenumber As String) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceNumberGloss
        Dim AccountReceivable = From e In _context.AccountReceivable.Include("MainAccounts").Include("Invoice").Include("AccountReceivableAccounting")
                 Where e.InvoiceNumber = invoicenumber And (e.AccountReceivableType = 1 Or e.AccountReceivableType = 2)
                 Select e
        If AccountReceivable.Count > 0 Then
            Dim tmpAccountReceivable = AccountReceivable.SingleOrDefault
            tmpAccountReceivable.OriginalValue = (From e In _context.AccountReceivable.AsNoTracking
         Where e.InvoiceNumber = invoicenumber And (e.AccountReceivableType = 1 Or e.AccountReceivableType = 2)
         Select e).SingleOrDefault
            Return tmpAccountReceivable
        Else
            Return Nothing
        End If
    End Function


    Public Function GetAccountReceivableByAdminssionNumberAndAccountReceivableType(invoiceNumber As String, AccountReceivableType As Integer) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByAdminssionNumberAndAccountReceivableType
        Return (From a In _context.AccountReceivable Where a.InvoiceNumber = invoiceNumber And a.AccountReceivableType = AccountReceivableType Select a).FirstOrDefault()
    End Function


    ''' <summary>
    ''' Obtiene el saldo de factura de cuentas por cobrar (GLOSAS)
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadBalance(invoiceNumber As String) As Decimal Implements IAccountReceivableRepository.LoadBalance
        Dim AccountReceivable = From e In _context.AccountReceivable
           Where e.InvoiceNumber = invoiceNumber And e.AccountReceivableType = 2
           Select e
        Dim tmpAccountReceivable = AccountReceivable.SingleOrDefault
        Return tmpAccountReceivable.Balance
    End Function

    ''' <summary>
    ''' obtiene una factura por tercero y numero
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="thirdPartyId"></param>
    ''' <returns></returns>
    Public Function GetAccountReceivableByInvoiceNumberAndThirdPartyId(invoiceNumber As String, ThirdPartyId As Integer) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceNumberAndThirdPartyId
        Dim status As Integer = 2
        Dim balance As Decimal = 0
        Return (From cr In _context.AccountReceivable.Include("AccountReceivableAccounting") Where cr.InvoiceNumber = invoiceNumber And cr.ThirdPartyId = ThirdPartyId And cr.Status = status And cr.Balance > balance).FirstOrDefault()
    End Function


    ''' <summary>
    ''' se usa para consultar Cuenta por Cobrar teniendo en cuenta el Código de la misma
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableByCode(ByVal code As String) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByCode
        Return (From ar In _context.AccountReceivable Where ar.Code = code Select ar).FirstOrDefault()
    End Function

    ''' <summary>
    ''' obtiene una Cuenta por Cobrar teniendo en cuenta el número de factura
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    Public Function GetAccountReceivableByInvoiceNumber(ByVal invoiceNumber As String) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceNumber
        If invoiceNumber Is Nothing OrElse invoiceNumber.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("invoiceNumber")
        End If
        Dim res = (From Ar As AccountReceivable In Me._context.AccountReceivable Where Ar.InvoiceNumber.Equals(invoiceNumber.Trim()) Select Ar).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From Ar As AccountReceivable In Me._context.AccountReceivable.AsNoTracking() Where Ar.InvoiceNumber.Equals(invoiceNumber.Trim()) Select Ar).FirstOrDefault()
            Return res(0)
        Else
            Return New AccountReceivable()
        End If
    End Function

    Public Function GetAccountReceivableByInvoiceNumberSimple(ByVal invoiceNumber As String) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceNumberSimple
        If invoiceNumber Is Nothing OrElse invoiceNumber.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("invoiceNumber")
        End If
        Return (From Ar As AccountReceivable In Me._context.AccountReceivable Where Ar.InvoiceNumber.Equals(invoiceNumber.Trim()) Select Ar).FirstOrDefault()
    End Function

    Public Function GetListAccountReceivable(ByVal listInvoiceNumber As List(Of String)) As List(Of AccountReceivable) Implements IAccountReceivableRepository.GetListAccountReceivable
        If listInvoiceNumber Is Nothing OrElse listInvoiceNumber.Count = 0 Then
            Return New List(Of AccountReceivable)
        End If
        Return (From Ar As AccountReceivable In Me._context.AccountReceivable.AsNoTracking() Where listInvoiceNumber.Contains(Ar.InvoiceNumber) Select Ar).ToList()
    End Function

    ''' <summary>
    ''' Gets the account receivable by invoice identifier.
    ''' </summary>
    ''' <param name="invoiceId"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">invoiceId</exception>
    Public Function GetAccountReceivableByInvoiceId(invoiceId As Integer) As List(Of AccountReceivable) Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceId
        If invoiceId = 0 Then
            Throw New ArgumentNullException("invoiceId")
        End If
        Dim query = (From Ar As AccountReceivable In Me._context.AccountReceivable.Include("AccountReceivableShare").Include("AccountReceivableAccounting") Where Ar.InvoiceId = invoiceId Select Ar).ToList()
        If query IsNot Nothing AndAlso query.Count > 0 Then
            For Each item In query
                item.OriginalValue = (From i In _context.AccountReceivable.AsNoTracking() Where i.Id = item.Id Select i).FirstOrDefault()
            Next
            Return query
        Else
            Return Nothing
        End If
    End Function


    ''' <summary>
    ''' metodo para establecer un rango de facturas para guardar
    ''' </summary>
    ''' <param name="listAccountReceivable"></param>
    ''' <returns></returns>
    Public Function SaveListAccountReceivable(listAccountReceivable As List(Of AccountReceivable)) As List(Of AccountReceivable) Implements IAccountReceivableRepository.SaveListAccountReceivable
        Return (_context.AccountReceivable.AddRange(listAccountReceivable).ToList())
    End Function

    Public Function GetAccountReceivableByInvoiceIdAndAccountReceivableType(invoiceId As Integer, accountReceivableType As Integer) As List(Of AccountReceivable) Implements IAccountReceivableRepository.GetAccountReceivableByInvoiceIdAndAccountReceivableType
        Return (From ar In _context.AccountReceivable Where ar.InvoiceId = invoiceId AndAlso ar.AccountReceivableType = accountReceivableType).ToList()
    End Function


    Public Function GetBillPortfolioNote(thirdPartyId As Integer, invoiceNumber As String, nature As Integer, Optional IsPrivate As Boolean = False, Optional MainAccountId As Integer = 0) As AccountReceivableAccounting Implements IAccountReceivableRepository.GetBillPortfolioNote
        Dim res As AccountReceivableAccounting = Nothing
        Dim accountReceivable = (From e In _context.AccountReceivable.AsNoTracking() Where e.InvoiceNumber = invoiceNumber Select e).FirstOrDefault()
        Dim idAcoount As Integer
        If accountReceivable IsNot Nothing Then
            If IsPrivate = False Then 'Si el tipo de compañia es publica

                If accountReceivable.PortfolioStatus >= 3 Then
                    idAcoount = accountReceivable.AccountRadicateId
                Else
                    idAcoount = accountReceivable.AccountWithoutRadicateId
                End If
                If nature = 1 Then 'debito
                    res = (From ara In _context.AccountReceivableAccounting Join ar In _context.AccountReceivable On ara.AccountReceivableId Equals ar.Id
                           Where ar.Status = 2 And ar.ThirdPartyId = thirdPartyId And ar.InvoiceNumber = invoiceNumber And ara.MainAccountId = idAcoount Select ara).FirstOrDefault()
                Else 'credito
                    res = (From ara In _context.AccountReceivableAccounting Join ar In _context.AccountReceivable On ara.AccountReceivableId Equals ar.Id
                           Where ar.Status = 2 And ar.ThirdPartyId = thirdPartyId And ar.InvoiceNumber = invoiceNumber And ara.Balance > 0 And ara.MainAccountId = idAcoount Select ara).FirstOrDefault()
                End If
                If res IsNot Nothing Then
                    res.MainAccountDescription = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    res.AccountReceivableShareIdTmp = (From ars In _context.AccountReceivableShare.AsNoTracking() Where ars.AccountReceivableId = res.AccountReceivableId Select ars).ToList().ElementAt(0).Id
                End If

            Else 'Si el tipo de compañia es privada
                If nature = 1 Then 'debito
                    res = (From ara In _context.AccountReceivableAccounting.Include("AccountReceivable")
                           Where ara.AccountReceivable.Status = 2 And ara.AccountReceivable.ThirdPartyId = thirdPartyId And ara.AccountReceivable.InvoiceNumber = invoiceNumber And ara.MainAccountId = MainAccountId Select ara).FirstOrDefault()
                Else 'credito
                    res = (From ara In _context.AccountReceivableAccounting.Include("AccountReceivable")
                           Where ara.AccountReceivable.Status = 2 And ara.AccountReceivable.ThirdPartyId = thirdPartyId And ara.AccountReceivable.InvoiceNumber = invoiceNumber And ara.Balance > 0 And ara.MainAccountId = MainAccountId Select ara).FirstOrDefault()
                End If
                If res IsNot Nothing Then
                    res.MainAccountDescription = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = res.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                    res.AccountReceivableShareIdTmp = (From ars In _context.AccountReceivableShare.AsNoTracking() Where ars.AccountReceivableId = res.AccountReceivableId Select ars).ToList().ElementAt(0).Id
                End If

            End If
        End If
        Return res
    End Function

    Public Function GetSharePortfolioNote(thirdPartyId As Integer, invoiceNumber As String, nature As Integer, shareNumber As Integer) As AccountReceivableShare Implements IAccountReceivableRepository.GetSharePortfolioNote
        Dim res As AccountReceivableShare
        If nature = 1 Then 'debito
            res = (From ars In _context.AccountReceivableShare Join ar In _context.AccountReceivable On ars.AccountReceivableId Equals ar.Id
                   Where ar.Status = 2 And ar.ThirdPartyId = thirdPartyId And ar.InvoiceNumber = invoiceNumber And ars.Number = shareNumber Select ars).FirstOrDefault()
        Else 'credito
            res = (From ars In _context.AccountReceivableShare Join ar In _context.AccountReceivable On ars.AccountReceivableId Equals ar.Id
                   Where ar.Status = 2 And ar.ThirdPartyId = thirdPartyId And ar.InvoiceNumber = invoiceNumber And ars.Number = shareNumber And ars.Balance > 0 Select ars).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            res.AccountReceivableAccountingIdTmp = (From ara In _context.AccountReceivableAccounting.AsNoTracking() Where ara.AccountReceivableId = res.AccountReceivableId Select ara).ToList().ElementAt(0).Id
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene una cuenta contable por el numero
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <returns></returns>
    Public Function GetMainAccountByNumber(Number As String) As MainAccounts Implements IAccountReceivableRepository.GetMainAccountByNumber
        Return (From m In _context.MainAccounts.AsNoTracking()
                Join l In _context.LegalBook.AsNoTracking() On l.Id Equals m.LegalBookId
                Where m.Number = Number And l.OfficialBook = True
                Select m).FirstOrDefault()
    End Function

    Public Function GetAccountByInvoiceNumberAndAccountReceivableType(invoiceNumber As String, accountReceivableType As Byte()) As AccountReceivable Implements IAccountReceivableRepository.GetAccountByInvoiceNumberAndAccountReceivableType
        Return (From ac In _context.AccountReceivable Where ac.InvoiceNumber.Equals(invoiceNumber) AndAlso accountReceivableType.Contains(ac.AccountReceivableType) Select ac).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el cliente por id del tercero
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    Public Function GetCustomerIdByThirdPartyId(ThirdPartyId As Integer) As Integer? Implements IAccountReceivableRepository.GetCustomerIdByThirdPartyId
        Dim customer = (From c In _context.Customer.AsNoTracking() Where c.ThirdPartyId = ThirdPartyId Select c).FirstOrDefault()
        If customer Is Nothing Then
            Return Nothing
        End If
        Return customer.Id
    End Function

    ''' <summary>
    ''' Obtiene el codigo cufe de una factura por el Id de la cuenta por cobrar
    ''' </summary>
    ''' <param name="AccountReceivableId"></param>
    ''' <returns></returns>
    Public Function GetCUFEByAccountReceivableId(AccountReceivableId As String) As String Implements IAccountReceivableRepository.GetCUFEByAccountReceivableId
        Return (From ar In _context.AccountReceivable.AsNoTracking()
                Join i In _context.Invoice
                        On ar.InvoiceId Equals i.Id
                Where ar.Id = AccountReceivableId Select i.CUFE).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene la cxc por id de AccountReceivableAccounting
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAccountReceivableByAccountReceivableAccountingId(id As Integer) As AccountReceivable Implements IAccountReceivableRepository.GetAccountReceivableByAccountReceivableAccountingId
        Dim query = (From x In _context.AccountReceivableAccounting.AsNoTracking().Include("AccountReceivable").AsNoTracking()
                     Where x.Id = id Select x.AccountReceivable)?.FirstOrDefault

        If query?.InvoiceId IsNot Nothing Then
            query.Invoice = (From x In _context.Invoice.AsNoTracking() Where x.Id = query.InvoiceId Select x)?.FirstOrDefault
        End If

        Return query
    End Function

End Class
