'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface ISequenseAccountingDRepository
    Inherits IRepository(Of GeneralLedgerSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As GeneralLedgerSequenceDetail
    Function GetSequenseDetailUpdatedById(id As Integer) As GeneralLedgerSequenceDetail


#End Region

#Region "Methods VoucherTypes"
    ''' <summary>
    ''' Crea una secuencia propia de sql desde la entidad JournalVoucherTypeConsecutive
    ''' </summary>
    ''' <param name="documentType">Objeto de tipo JournalVoucherTypeConsecutive </param>
    Function CreateSequenceForDocumentTypeAsync(documentType As JournalVoucherTypeConsecutive) As Task

    ''' <summary>
    ''' Crea todas las secuencias propias de sql desde la entidad JournalVoucherTypeConsecutive
    ''' </summary>
    ''' <param name="documentType">Objeto de tipo JournalVoucherTypeConsecutive </param>
    Function CreateSequencesForNewDocumentTypeAsync(documentType As JournalVoucherTypes) As Task

    ''' <summary>
    ''' Obtiene el current_value de una secuencia propia de sql 
    ''' </summary>
    ''' <param name="sequenceName">Nombre de la secuencia para consultar</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetCurrentSequenceValueAsync(sequenceName As String) As Task(Of Long)

    ''' <summary>
    ''' Elimina una secuencia propia de sql desde la entidad JournalVoucherTypeConsecutive
    ''' </summary>
    ''' <param name="documentType">Objeto de tipo JournalVoucherTypeConsecutive </param>
    Function DeleteSequenceForDocumentTypeAsync(documentType As JournalVoucherTypeConsecutive) As Task

    ''' <summary>
    ''' Elimina todas las secuencias propias de sql para un JournalVoucherType
    ''' </summary>
    ''' <param name="documentType">Objeto de tipo JournalVoucherTypeConsecutive </param>
    Function DeleteAllSequencesForDocumentTypeAsync(documentType As JournalVoucherTypes) As Task
#End Region

End Interface