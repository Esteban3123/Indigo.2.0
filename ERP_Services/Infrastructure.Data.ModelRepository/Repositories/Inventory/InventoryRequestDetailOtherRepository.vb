'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Andres Alarcon
' Created          : 03-05-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class InventoryRequestDetailOtherRepository
    Inherits GenericRepository(Of InventoryRequestDetailOther)
    Implements IInventoryRequestDetailOtherRepository

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

    ''' <summary>
    ''' Obtiene un detalle de solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryRequestDetailOtherById(id As Integer) As InventoryRequestDetailOther Implements IInventoryRequestDetailOtherRepository.GetInventoryRequestDetailOtherById
        Return (From ir In Me._context.InventoryRequestDetailOther Where ir.Id = id
                Select ir).FirstOrDefault
    End Function
End Class
