'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 27-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class UpdateExpirationDateRepository
    Inherits GenericRepository(Of UpdateExpirationDate)
    Implements IUpdateExpirationDateRepository


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
