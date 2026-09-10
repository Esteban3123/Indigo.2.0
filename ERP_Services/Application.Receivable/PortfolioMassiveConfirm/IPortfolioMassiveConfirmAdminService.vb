'***********************************************************************
' Assembly         : Application.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IPortfolioMassiveConfirmAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Metodo para confirmar un documentos del módulo de cuentas por cobrar
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmPortfolioDocument(processId As Integer, code As String, audit As AuditMessage, session As SessionValues) As ActionResult(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' metodo para confirmar documentos de cuentas por cobrar masivamente
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmPortfolioDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage, session As SessionValues) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface