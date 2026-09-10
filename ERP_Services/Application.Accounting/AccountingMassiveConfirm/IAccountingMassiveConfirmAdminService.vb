'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IAccountingMassiveConfirmAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Metodo para confirmar un documentos del módulo de Contabilidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmAccountingDocument(code As String, audit As AuditMessage) As ActionResult(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' metodo para confirmar documentos de tesoreria masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    Function ConfirmAccountingDocuments(listDocuments As List(Of String), audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface