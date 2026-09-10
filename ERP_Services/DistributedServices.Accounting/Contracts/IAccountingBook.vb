#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingBook

#Region "Methods"

    ''' <summary>
    ''' Guarda o Actualiza un libro
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveBook(ByVal Book As LegalBook) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Elimina un libro
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteBook(ByVal Book As LegalBook) As ActionResult

    ''' <summary>
    ''' Obtiene un libro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBookByCode(ByVal code As String) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Obtiene un libro por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetBookById(ByVal id As Integer) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Cambia el estado de la entidad de libro
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ChangeStateBook(ByVal code As String, ByVal state As Boolean) As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Valida si ya hay libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ValidateOfficialBook() As ActionResult(Of LegalBook)

    ''' <summary>
    ''' Metodo para ejecutar el cierre contable por libro
    ''' </summary>
    ''' <param name="legalBookId"></param>
    ''' <param name="year"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <param name="nitCompany"></param>
    ''' <param name="user"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function FiscalYearClose(legalBookId As Integer, year As Integer, operativeUnitId As Integer, nitCompany As String, user As String) As ActionResult(Of List(Of Tuple(Of String, Integer)))

#End Region

End Interface
