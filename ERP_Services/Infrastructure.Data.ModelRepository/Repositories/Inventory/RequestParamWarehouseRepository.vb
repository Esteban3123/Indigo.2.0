'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Oscar stiven Astudillo.
' Created          : 2024-01-18
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RequestParamWarehouseRepository
    Inherits GenericRepository(Of RequestParamWarehouse)
    Implements IRequestParamWarehouseRepository, Inject

    ''' <summary>
    ''' Contexto de RequestParamWarehouse
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de RequestParamWarehouse
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
