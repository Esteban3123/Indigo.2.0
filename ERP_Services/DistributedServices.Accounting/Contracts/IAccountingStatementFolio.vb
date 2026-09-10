'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IAccountingStatementFolio

#Region "Methods"
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStateStatementfolio(ByVal code As String, ByVal state As Boolean) As ActionResult(Of AttachedDeclarations)
    ''' <summary>
    ''' Saves the statement folio.
    ''' </summary>
    ''' <param name="statementfolio">The statementfolio.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveStatementFolio(ByVal statementfolio As AttachedDeclarations) As ActionResult(Of AttachedDeclarations)

    ''' <summary>
    ''' Deletes the statement folio.
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteStatementFolio(ByVal statementfolio As AttachedDeclarations) As ActionResult
    ''' <summary>
    ''' Gets the statement folio by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetStatementFolioByCode(ByVal code As String, Optional tracking As Boolean = True) As AttachedDeclarations

    ''' <summary>
    ''' Gets the statement folio by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetStatementFolioById(ByVal id As Integer, Optional tracking As Boolean = True) As AttachedDeclarations
#End Region

End Interface
