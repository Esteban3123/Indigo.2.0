'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPaymentsNoteConceptRepository
    Inherits IRepository(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPaymentsNoteConcepts() As List(Of AccountPayableConceptNotes)

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNoteConcept(code As String, Optional tracking As Boolean = True) As AccountPayableConceptNotes

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPaymentNoteConceptById(id As String, Optional tracking As Boolean = True) As AccountPayableConceptNotes

End Interface
