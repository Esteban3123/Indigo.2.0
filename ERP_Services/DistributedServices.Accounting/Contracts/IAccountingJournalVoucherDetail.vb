#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract>
Public Interface IAccountingJournalVoucherDetail

#Region "Methods"
    
    ''' <summary>
    ''' Obtiene los Detalles de un comprobantea contable
    ''' </summary>
    ''' <param name="journalVoucherId"></param>
    ''' <returns></returns>
    <OperationContract>
    Function GetJournalVoucherDetails(journalVoucherId As Integer) As ActionResult(Of List(Of JournalVoucherDetails))

#End Region

End Interface
