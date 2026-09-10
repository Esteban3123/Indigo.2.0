#Region "Imports"

Imports Domain.Base
#End Region

Public Interface IBankReconciliationAutomaticRepository
    Inherits IRepository(Of BankReconciliationAutomatic)

    ''' <summary>
    ''' Obtiene una conciliación por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationAutomaticById(id As Integer, Optional tracking As Boolean = True) As BankReconciliationAutomatic

    ''' <summary>
    ''' Obtiene una conciliación por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBankReconciliationAutomaticByCode(code As String, Optional tracking As Boolean = True) As BankReconciliationAutomatic

    ''' <summary>
    ''' Obtiene los extractos
    ''' </summary>
    ''' <param name="BankReconciliationAutomaticId"></param>
    ''' <returns></returns>
    Function GetListAssociationByReconciliationId(BankReconciliationAutomaticId As Integer) As List(Of BankReconciliationAutomaticAssociation)

    ''' <summary>
    ''' Obtiene los extractos en la pivot
    ''' </summary>
    ''' <param name="DetailId"></param>
    ''' <param name="DetailExtractId"></param>
    ''' <returns></returns>
    Function GetBankAssociation(DetailId As Integer, DetailExtractId As Integer) As BankReconciliationAutomaticAssociation

    ''' <summary>
    ''' Obtiene múltiples asociaciones de la tabla pivot de forma masiva (optimización N+1)
    ''' </summary>
    ''' <param name="pairs">Lista de tuplas (DetailId, ExtractId) a buscar</param>
    ''' <returns>Diccionario con clave "DetailId_ExtractId" y valor el Id de la asociación</returns>
    Function GetBankAssociationsBulk(pairs As List(Of Tuple(Of Integer, Integer))) As Dictionary(Of String, Integer)

    ''' <summary>
    ''' Obtienes las partidas pendientes por conciliar de la cuenta bancaria del segmento libro de bancos acorde al periodo
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Function GetPendingItemsBankBook(entityBankAccountId As Integer, documentDate As Date) As List(Of BankReconciliationAutomaticDetail)

    ''' <summary>
    ''' Obtienes las partidas pendientes por conciliar de la cuenta bancaria del segmento extracto bancario acorde al periodo
    ''' </summary>
    ''' <param name="entityBankAccountId"></param>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Function GetPendingItemsExtract(entityBankAccountId As Integer, documentDate As Date) As List(Of BankReconciliationAutomaticExtractDetail)

    ''' <summary>
    ''' Obtiene los detalles de una conciliación
    ''' </summary>
    ''' <param name="XmlCriterias">Criterios</param>
    ''' <returns></returns>
    Function SP_GetBankReconciliationAutomaticDetails(XmlCriterias As String) As List(Of SP_GetBankReconciliationAutomaticDetails_Result)

    ''' <summary>
    ''' Obtiene los extractos
    ''' </summary>
    ''' <param name="XmlCriterias"></param>
    ''' <returns></returns>
    Function SP_GetBankReconciliationAutomaticExtractDetails(XmlCriterias As String) As List(Of SP_GetBankReconciliationAutomaticExtractDetails_Result)
    ''' <summary>
    ''' Trae las reglas de reconocimiento en bancos
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <returns></returns>
    Function BankAutomaticRecognitionRules(entityId As Integer) As List(Of BankAutomaticRecognitionRules)
    ''' <summary>
    ''' Obtiene la descripcion de los extractos bancarios
    ''' </summary>
    ''' <param name="UploadBankStatementsDescription"></param>
    ''' <returns></returns>
    Function GetUploadBankStatementsDescriptionList(UploadBankStatementsDescription As List(Of Integer)) As List(Of UploadBankStatementsDetail)
    ''' <summary>
    ''' Obtiene lista de bancos
    ''' </summary>
    ''' <param name="bankList"></param>
    ''' <returns></returns>
    Function getbanksList(bankList As List(Of Integer)) As List(Of Bank)
    ''' <summary>
    ''' Obtiene los conceptos de notas
    ''' </summary>
    ''' <param name="noteConcepts"></param>
    ''' <returns></returns>
    Function getNoteconcepts(noteConcepts As List(Of Integer)) As List(Of NoteConcepts)
    ''' <summary>
    ''' Asigna el extendido del Periodo de la partida pendiente por Conciliar
    ''' </summary>
    ''' <param name="documentDate"></param>
    ''' <returns></returns>
    Function GetPeriodFromDate(documentDate As Date) As String
    ''' <summary>
    ''' Retorna los recibos de caja que han sido relacionados a las notas de gastos bancarios
    ''' </summary>
    ''' <param name="NoteIds"></param>
    ''' <returns></returns>
    Function GetListCashReceiptsBulk(noteIds As List(Of Integer)) As Dictionary(Of Integer, List(Of CashReceipts))

End Interface
