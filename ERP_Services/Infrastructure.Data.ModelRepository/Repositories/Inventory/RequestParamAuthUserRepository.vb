'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Oscar stiven Astudillo.
' Created          : 2024-09-2'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RequestParamAuthUserRepository
    Inherits GenericRepository(Of RequestParamAuthUser)
    Implements IRequestParamAuthUserRepository, Inject

    ''' <summary>
    ''' Contexto de ParamAuthUserRepository
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de ParamAuthUserRepository
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
