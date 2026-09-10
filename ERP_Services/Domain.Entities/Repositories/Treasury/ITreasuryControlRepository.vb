'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 13-08-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ITreasuryControlRepository
    Inherits IRepository(Of TreasuryControl)

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTreasuryControlById(Id As Integer) As TreasuryControl

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetTreasuryControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As TreasuryControl

End Interface
