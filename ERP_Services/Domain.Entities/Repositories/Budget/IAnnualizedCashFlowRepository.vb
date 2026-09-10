'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowRepository
    Inherits IRepository(Of AnnualizedCashFlow)

    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia y tipo
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowByValidityIdAndByCodeCategory(ValidityId As Integer, CodeCategory As String, type As Integer) As ActionResult(Of List(Of AnnualizedCashFlow))
    ''' <summary>
    ''' Obtener todos los registros de PAC inicial por vigencia 
    ''' </summary>
    ''' <param name="ValidityId"></param>
    Function GetAnnualizedCashFlowByValidityId(ValidityId As Integer) As List(Of AnnualizedCashFlow)
    ''' <summary>
    ''' Obtener un registro de PAC inicial por id 
    ''' </summary>
    ''' <param name="id"></param>
    Function GetAnnualizedCashFlowById(id As Integer) As AnnualizedCashFlow
    ''' <summary>
    ''' Obtiene un registro de pac inicial por id del rubro y el mes
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowByCategoryIdAndByMonth(categoryId As Integer, month As Integer) As AnnualizedCashFlow

End Interface
