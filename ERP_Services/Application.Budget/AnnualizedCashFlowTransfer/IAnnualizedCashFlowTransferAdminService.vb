'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 27-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowTransferAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene un traslado del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowTransferByCode(code As String, type As Integer, audit As AuditMessage) As AnnualizedCashFlowTransfer
    ''' <summary>
    ''' obtiene un traslado del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowTransferById(id As Integer) As AnnualizedCashFlowTransfer
    ''' <summary>
    ''' Guarda un traslado del pac
    ''' </summary>
    ''' <param name="annualizedCashFlowTransfer"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAnnualizedCashFlowTransfer(annualizedCashFlowTransfer As AnnualizedCashFlowTransfer, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AnnualizedCashFlowTransfer)

End Interface
