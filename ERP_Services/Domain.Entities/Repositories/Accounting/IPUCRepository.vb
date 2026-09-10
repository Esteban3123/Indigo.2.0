'***********************************************************************
' Assembly         : Domain.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 02-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IPUCRepository
    Inherits IRepository(Of MainAccounts)

#Region "Functions"

    ''' <summary>
    ''' Gets the account by identifier includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Function GetAccountByIdIncludes(id As Integer, includes() As String) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountByCode(ByVal code As String, ByVal tracking As Boolean, Optional LegalBookId As Integer? = Nothing) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo y libro oficial
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <returns></returns>
    Function GetAccountByCodeAndLegalBookId(ByVal code As String, LegalBookId As Integer) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id">id.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountById(ByVal id As Integer, ByVal tracking As Boolean) As MainAccounts

    ''' <summary>
    ''' funcion para listar todas las cuentas
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAcounts() As List(Of MainAccounts)

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
    Function Sp_InsertPUC(ByVal accountLevelId As Nullable(Of Integer),
                          ByVal accountClassId As Nullable(Of Integer),
                          ByVal accountCode As String,
                          ByVal accountName As String,
                          ByVal parentId As Nullable(Of Integer),
                          ByVal handlesThird As Nullable(Of Boolean),
                          ByVal closeThird As Nullable(Of Boolean),
                          ByVal thirdId As Nullable(Of Integer),
                          ByVal reconcileAccount As Nullable(Of Boolean),
                          ByVal available As Nullable(Of Byte),
                          ByVal handlesCenter As Nullable(Of Boolean),
                          ByVal typeRetencion As Nullable(Of Byte),
                          ByVal accountActive As Nullable(Of Boolean),
                          ByVal allowMovement As Nullable(Of Boolean)) As Decimal

    ''' <summary>
    ''' Gets all child by identifier parente.
    ''' </summary>
    ''' <param name="idParent">The identifier parent.</param>
    ''' <returns></returns>
    Function GetAllChildByIdParente(ByVal idParent As Integer) As List(Of MainAccounts)

    ''' <summary>
    ''' Validates the account parent.
    ''' </summary>
    ''' <param name="codeAccount">The code account.</param>
    ''' <returns></returns>
    Function ValidateAccountParent(ByVal codeAccount As String, legalBookId As Integer) As MainAccounts

    ''' <summary>
    ''' Validates the account parent.
    ''' </summary>
    ''' <param name="codeAccount">The code account.</param>
    ''' <returns></returns>
    Function ValidateAccountParentByCodeAndLegalBookId(ByVal codeAccount As String, LegalBookId As Integer) As MainAccounts

    Function GetAccountByCodePOCO(code As String, tracking As Boolean) As MainAccounts

    Function GetListAccountByCodePOCO(listCode As List(Of String)) As List(Of MainAccounts)

    ''' <summary>
    ''' Valida que la clase contable no tenga asociada una cuenta contable
    ''' </summary>
    ''' <param name="idAccountClass"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateMainAccountByIdAccountClass(ByVal idAccountClass As Integer) As Boolean

    ''' <summary>
    ''' Valida si la cuenta contable tiene movimientos
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateMovementsOfMainAccount(ByVal MainAccountId As Integer) As Boolean

    ''' <summary>
    ''' Obtiene si maneja centro de costo la cuenta contable
    ''' </summary>
    ''' ''' <param name="idProfit"></param>
    ''' ''' <param name="idLost"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetHadleCostCenterByMainAccountId(idProfit As Integer?, idLost As Integer?) As MainAccounts


#End Region

End Interface