'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 2014-04-10
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface IStatementFolioRepository
    Inherits IRepository(Of AttachedDeclarations)

#Region "Functions"
    ''' <summary>
    ''' funcion para obtener el folio por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetStatementFoliobyCode(ByVal code As String, Optional tracking As Boolean = True) As AttachedDeclarations

    ''' <summary>
    ''' funcion para obtene rel folio por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetStatementFoliobyId(ByVal id As Integer, Optional tracking As Boolean = True) As AttachedDeclarations
#End Region
    
End Interface
