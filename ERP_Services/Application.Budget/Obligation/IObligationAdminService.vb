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


Public Interface IObligationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una obligacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Obligation

    ''' <summary>
    ''' obtiene una obligacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationById(id As Integer) As Obligation

    ''' <summary>
    ''' metodo para guardar una obligacion
    ''' </summary>
    ''' <param name="Obligation"></param>
    ''' <param name="listObligationDetailDelete"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveObligation(Obligation As Obligation, listObligationDetailDelete As List(Of Integer), ByVal audit As AuditMessage) As ActionResult(Of Obligation)

End Interface
