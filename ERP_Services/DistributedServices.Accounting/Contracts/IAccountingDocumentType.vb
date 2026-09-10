#Region "Imports"

Imports System.ServiceModel
Imports System.Threading
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IAccountingDocumentType

#Region "Methods"

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="code">Código del documento a consultar</param>
    ''' <returns>Tipo de documento consultado</returns>
    <OperationContract()>
    Function GetDocumentType(ByVal code As String) As Task(Of JournalVoucherTypes)

    ''' <summary>
    ''' Graba un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function SaveDocumentType(ByVal doc As JournalVoucherTypes) As Task(Of ActionResult(Of JournalVoucherTypes))

    ''' <summary>
    ''' Updates the state
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateDocumentType(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of JournalVoucherTypes))

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function DeleteDocumentType(ByVal doc As JournalVoucherTypes) As Task(Of ActionResult)

    ''' <summary>
    ''' Gets the journal voucher by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetJournalVoucherById(ByVal id As Long) As ActionResult(Of JournalVoucherTypes)

#End Region

End Interface
