'************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-10-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BillingNoteDetailRepository
    Inherits GenericRepository(Of BillingNoteDetail)
    Implements IBillingNoteDetailRepository

    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
