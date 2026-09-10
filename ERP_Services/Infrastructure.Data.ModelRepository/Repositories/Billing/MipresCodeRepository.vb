'************************************************************
' Assembly         : Domain.Billing
' Author           : Diego A. Roldán
' Created          : 2021-09-24
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class MipresCodeRepository
    Inherits GenericRepository(Of MipresCode)
    Implements IMipresCodeRepository, Inject

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
