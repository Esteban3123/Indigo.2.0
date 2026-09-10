'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtener los rubros del PAC inicial 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, CodeCategory As String, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of Domain.Entities.AnnualizedCashFlow))

    ''' <summary>
    ''' Guarda o Actualiza el PAC inicial
    ''' </summary>
    ''' <param name="ListAnnualizedCashFlow">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveListAnnualizedCashFlowHeader(ListAnnualizedCashFlow As List(Of AnnualizedCashFlow), state As Integer, type As Integer, audit As AuditMessage) As ActionResult(Of List(Of Domain.Entities.AnnualizedCashFlow))
    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As List(Of AnnualizedCashFlow)
End Interface
