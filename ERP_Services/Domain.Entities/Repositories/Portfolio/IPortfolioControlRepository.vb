'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IPortfolioControlRepository
    Inherits IRepository(Of PortfolioControl)

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioControlById(Id As Integer) As PortfolioControl

    ''' <summary>
    ''' Obtiene un registro de control de tesoreria por consecutivo del documento
    ''' </summary>
    ''' <param name="DocumentNumber">The document number.</param>
    ''' <returns></returns>
    Function GetPortfolioControlByDocumentNumber(DocumentNumber As String, Optional DocumentType As Integer = 0) As PortfolioControl

End Interface
