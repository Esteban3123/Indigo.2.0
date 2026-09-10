'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Base
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
#End Region
Public Interface IStatementFolioAdminService
    Inherits IDisposable

#Region "Methods"
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateStatementFolio(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AttachedDeclarations)
    ''' <summary>
    ''' Saves the statement folio.
    ''' </summary>
    ''' <param name="statementfolio">The statementfolio.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveStatementFolio(ByVal statementfolio As AttachedDeclarations, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AttachedDeclarations)

    ''' <summary>
    ''' Deletes the statement folio.
    ''' </summary>
    ''' <param name="address">The address.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteStatementFolio(ByVal statementfolio As AttachedDeclarations, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Gets the statement folio by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetStatementFolioByCode(ByVal code As String, Optional tracking As Boolean = True) As AttachedDeclarations

    ''' <summary>
    ''' Gets the statement folio by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetStatementFolioById(ByVal id As Integer, Optional tracking As Boolean = True) As AttachedDeclarations
#End Region

End Interface
