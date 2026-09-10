'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2023-03-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AuditDataRepository

    Inherits GenericRepository(Of AuditData)
    Implements IAuditDataRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto de inventario
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de inventario
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

End Class
