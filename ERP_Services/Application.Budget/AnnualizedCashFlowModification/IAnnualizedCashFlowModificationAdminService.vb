'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowModificationAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene una modificacion del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowModificationByCode(code As String, type As Integer, audit As AuditMessage) As AnnualizedCashFlowModification
    ''' <summary>
    ''' obtiene una modificacion del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowModificationById(id As Integer) As AnnualizedCashFlowModification
    ''' <summary>
    ''' metodo para guardar una modificacion del pac
    ''' </summary>
    ''' <param name="AnnualizedCashFlowModification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAnnualizedCashFlowModification(AnnualizedCashFlowModification As AnnualizedCashFlowModification, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AnnualizedCashFlowModification)
End Interface
