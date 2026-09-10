'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RequestParamFunctionalUnitRepository
    Inherits GenericRepository(Of RequestParamFuncionalUnit)
    Implements IRequestParamFunctionalUnitRepository, Inject

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
