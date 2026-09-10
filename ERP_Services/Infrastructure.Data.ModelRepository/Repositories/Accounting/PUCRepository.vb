'***********************************************************************
' Assembly         : Infrastructure.PUCRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region

Public Class PUCRepository
    Inherits GenericRepository(Of MainAccounts)
    Implements IPUCRepository, Inject

#Region "Fields"
    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork
#End Region

#Region "Builders"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de contabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountById(id As Integer, tracking As Boolean) As MainAccounts Implements IPUCRepository.GetAccountById

        If tracking = True Then
            Dim busqueda = (From e In _context.MainAccounts.Include("MainAccountLevels").Include("MainAccountClasses")
                            Where e.Id = id
                            Select e).FirstOrDefault()
            If busqueda IsNot Nothing Then
                busqueda.OriginalValue = (From d In _context.MainAccounts Where d.Id = id Select d).FirstOrDefault()
                busqueda.NumberName = busqueda.Number + " - " + busqueda.Name
                Return busqueda
            Else
                Return New MainAccounts
            End If
        Else
            Dim busqueda = (From e In _context.MainAccounts.AsNoTracking().Include("MainAccountLevels").AsNoTracking().Include("MainAccountClasses").AsNoTracking()
                            Where e.Id = id
                            Select e).FirstOrDefault()
            If busqueda IsNot Nothing Then
                busqueda.OriginalValue = (From d In _context.MainAccounts.AsNoTracking() Where d.Id = id Select d).FirstOrDefault()
                busqueda.NumberName = busqueda.Number + " - " + busqueda.Name
                Return busqueda
            Else
                Return New MainAccounts
            End If
        End If
    End Function

    Public Function GetAccountByIdIncludes(id As Integer, includes() As String) As MainAccounts Implements IPUCRepository.GetAccountByIdIncludes
        Dim query = _context.MainAccounts.AsNoTracking().Where(Function(o) o.Id = id).AsQueryable()
        If includes IsNot Nothing Then
            For Each include In includes
                query = query.Include(include).AsNoTracking()
            Next
        End If
        Return query.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountByCode(code As String, tracking As Boolean, Optional LegalBookId As Integer? = Nothing) As MainAccounts Implements IPUCRepository.GetAccountByCode
        If LegalBookId Is Nothing Then
            'Se consulta el libro oficial para pedir la cuenta contable
            LegalBookId = (From e In _context.LegalBook.AsNoTracking Where e.OfficialBook = True Select e.Id).FirstOrDefault
        End If

        If LegalBookId Is Nothing Then
            Return New MainAccounts
        End If

        If tracking = True Then
            Dim busqueda = From e In _context.MainAccounts.Include("MainAccountLevels").Include("MainAccountClasses")
                           Where e.Number = code And e.LegalBookId = LegalBookId
                           Select e
            If busqueda.Count > 0 Then
                busqueda.FirstOrDefault.OriginalValue = (From d In _context.MainAccounts Where d.Number = code And d.LegalBookId = LegalBookId Select d).FirstOrDefault()
                Return busqueda.FirstOrDefault()
            Else
                Return New MainAccounts
            End If
        Else
            Dim busqueda = From e In _context.MainAccounts.Include("MainAccountLevels").Include("MainAccountClasses")
                           Where e.Number = code And e.LegalBookId = LegalBookId
                           Select e
            If busqueda.Count > 0 Then
                busqueda.FirstOrDefault.OriginalValue = (From d In _context.MainAccounts.AsNoTracking() Where d.Number = code And d.LegalBookId = LegalBookId Select d).FirstOrDefault()
                Return busqueda.FirstOrDefault()
            Else
                Return New MainAccounts
            End If
        End If
    End Function

    Public Function GetAccountByCodePOCO(code As String, tracking As Boolean) As MainAccounts Implements IPUCRepository.GetAccountByCodePOCO
        Dim mainAccount As MainAccounts = Nothing

        Dim LegalBookId As Integer = (From e In _context.LegalBook.AsNoTracking Where e.OfficialBook = True Select e.Id).FirstOrDefault
        If Not (LegalBookId = Nothing) Then
            If tracking = True Then
                mainAccount = (From e In _context.MainAccounts
                               Where e.Number = code And e.LegalBookId = LegalBookId
                               Select e).FirstOrDefault()
            Else
                mainAccount = (From e In _context.MainAccounts
                               Where e.Number = code And e.LegalBookId = LegalBookId
                               Select e).FirstOrDefault()
            End If
        End If

        If mainAccount Is Nothing Then
            Return New MainAccounts()
        End If

        Return mainAccount
    End Function

    Function GetListAccountByCodePOCO(listCode As List(Of String)) As List(Of MainAccounts) Implements IPUCRepository.GetListAccountByCodePOCO
        If listCode Is Nothing OrElse listCode.Count = 0 Then
            Return New List(Of MainAccounts)
        End If
        Dim LegalBookId As Integer = (From e In _context.LegalBook.AsNoTracking Where e.OfficialBook = True Select e.Id).FirstOrDefault
        Return (From e In _context.MainAccounts.AsNoTracking() Where listCode.Contains(e.Number) And e.LegalBookId = LegalBookId Select e).ToList()
    End Function

    Function GetListAccountByIdPOCO(listId As List(Of Integer)) As List(Of MainAccounts) Implements IPUCRepository.GetListAccountByIdPOCO
        If listId Is Nothing OrElse listId.Count = 0 Then
            Return New List(Of MainAccounts)
        End If
        Return (From e In _context.MainAccounts.AsNoTracking()
                Where listId.Contains(e.Id)
                Select e).ToList()
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo y libro oficial
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <returns></returns>
    Public Function GetAccountByCodeAndLegalBookId(code As String, LegalBookId As Integer) As MainAccounts Implements IPUCRepository.GetAccountByCodeAndLegalBookId
        Dim busqueda = From e In _context.MainAccounts.Include("MainAccountLevels").Include("MainAccountClasses")
                       Where e.Number = code And e.LegalBookId = LegalBookId
                       Select e
        If busqueda.Count > 0 Then
            busqueda.FirstOrDefault.OriginalValue = (From d In _context.MainAccounts Where d.Number = code And d.LegalBookId = LegalBookId Select d).AsNoTracking.FirstOrDefault()
            Dim result = busqueda.FirstOrDefault()
            Dim hasMovements = (From jvd In _context.JournalVoucherDetails Where jvd.MainAccounts.Id = result.Id Select jvd).Count
            If hasMovements > 0 Then
                result.hasMovements = True
            Else
                result.hasMovements = False
            End If
            Return result
        Else
            Return New MainAccounts
        End If
    End Function

    ''' <summary>
    ''' funcion para listar todas las cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAcounts() As List(Of MainAccounts) Implements IPUCRepository.GetAllAcounts

        Dim list = From e In _context.MainAccounts.Include("MainAccountLevels").Include("MainAccountClasses")
                   Select e
        If list.Count > 0 Then
            Return list.ToList()
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' SP_s the insert puc.
    ''' </summary>
    ''' <param name="accountLevelId">The account level identifier.</param>
    ''' <param name="accountClassId">The account class identifier.</param>
    ''' <param name="accountCode">The account code.</param>
    ''' <param name="accountName">Name of the account.</param>
    ''' <param name="parentId">The parent identifier.</param>
    ''' <param name="handlesThird">The handles third.</param>
    ''' <param name="closeThird">The close third.</param>
    ''' <param name="thirdId">The third identifier.</param>
    ''' <param name="reconcileAccount">The reconcile account.</param>
    ''' <param name="available">The available.</param>
    ''' <param name="handlesAdjustmentAccount">The handles adjustment account.</param>
    ''' <param name="accountAdjustmentId">The account adjustment identifier.</param>
    ''' <param name="accountCorrectionId">The account correction identifier.</param>
    ''' <param name="handlesCenter">The handles center.</param>
    ''' <param name="typeRetencion">The type retencion.</param>
    ''' <param name="accountActive">The account active.</param>
    ''' <param name="idNodePrevious">The identifier node previous.</param>
    ''' <param name="level">The level.</param>
    ''' <param name="childCount">The child count.</param>
    ''' <param name="idAccountPrevious">The identifier account previous.</param>
    ''' <returns></returns>
    Public Function Sp_InsertPUC(accountLevelId As Integer?, accountClassId As Integer?, accountCode As String, accountName As String, parentId As Integer?, handlesThird As Boolean?, closeThird As Boolean?, thirdId As Integer?, reconcileAccount As Boolean?, available As Byte?, handlesCenter As Boolean?, typeRetencion As Byte?, accountActive As Boolean?, allowMovement As Boolean?) As Decimal Implements IPUCRepository.Sp_InsertPUC
        Return _context.SP_InsertPUC(accountLevelId, accountClassId, accountCode, accountName, parentId, handlesThird, closeThird, thirdId, reconcileAccount, available, handlesCenter, typeRetencion, allowMovement, accountActive).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Gets all child by identifier parente.
    ''' </summary>
    ''' <param name="idParent">The identifier parent.</param>
    ''' <returns></returns>
    Public Function GetAllChildByIdParente(idParent As Integer) As List(Of MainAccounts) Implements IPUCRepository.GetAllChildByIdParente
        Dim query = From e In _context.MainAccounts
                    Where e.IdParent = idParent
                    Select e

        If query.Count > 0 Then
            Return query.ToList()
        Else
            Return New List(Of MainAccounts)
        End If
    End Function

    ''' <summary>
    ''' Validates the account parent.
    ''' </summary>
    ''' <param name="codeAccount">The code account.</param>
    ''' <returns></returns>
    Public Function ValidateAccountParent(codeAccount As String, legalBookId As Integer) As MainAccounts Implements IPUCRepository.ValidateAccountParent
        Dim query = (From e In _context.MainAccounts.Include("GeneralLedgerBalance").Include("MainAccountLevels").Include("MainAccountClasses")
                     Where e.Number = codeAccount AndAlso e.LegalBookId = legalBookId
                     Select e).FirstOrDefault
        If query IsNot Nothing Then

            query.OriginalValue = (From e In _context.MainAccounts.AsNoTracking
                                   Where e.Number = codeAccount AndAlso e.LegalBookId = legalBookId
                                   Select e).FirstOrDefault

            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Validates the account parent.
    ''' </summary>
    ''' <param name="codeAccount">The code account.</param>
    ''' <returns></returns>
    Public Function ValidateAccountParentByCodeAndLegalBookId(codeAccount As String, LegalBookId As Integer) As MainAccounts Implements IPUCRepository.ValidateAccountParentByCodeAndLegalBookId
        Dim query = (From e In _context.MainAccounts
                     Where e.Number = codeAccount And e.LegalBookId = LegalBookId
                     Select e).FirstOrDefault
        If query IsNot Nothing Then

            query.OriginalValue = (From e In _context.MainAccounts.AsNoTracking
                                   Where e.Number = codeAccount
                                   Select e).FirstOrDefault

            Return query
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Valida que la clase contable no tenga asociada una cuenta contable
    ''' </summary>
    ''' <param name="idAccountClass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateMainAccountByIdAccountClass(idAccountClass As Integer) As Boolean Implements IPUCRepository.ValidateMainAccountByIdAccountClass
        Dim accounts = (From a In _context.MainAccounts.AsNoTracking Where a.IdAccountClass = idAccountClass).ToList
        If accounts.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Valida si la cuenta contable tiene movimientos
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateMovementsOfMainAccount(MainAccountId As Integer) As Boolean Implements IPUCRepository.ValidateMovementsOfMainAccount
        Dim result = (From r In _context.GeneralLedgerBalance.AsNoTracking Where r.IdMainAccount = MainAccountId Select r).ToList
        If result.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Obtiene si la cuenta contable maneja centro de costo
    ''' </summary>
    ''' <param name="idProfit"></param>
    ''' <param name="idLost"></param>
    ''' <returns></returns>
    Public Function GetHadleCostCenterByMainAccountId(idProfit As Integer?, idLost As Integer?) As MainAccounts Implements IPUCRepository.GetHadleCostCenterByMainAccountId

        Dim _MainAccountLevels = From e In _context.MainAccounts
                                 Where {idProfit, idLost}.Contains(e.Id) AndAlso e.HandlesCostCenter
                                 Select e

        Return _MainAccountLevels.FirstOrDefault
    End Function




#End Region

End Class