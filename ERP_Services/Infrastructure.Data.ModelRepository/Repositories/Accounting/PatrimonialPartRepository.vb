'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PatrimonialPartRepository
    Inherits GenericRepository(Of Shareholding)
    Implements IPatrimonialPartRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una participación patrimonial por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetPatrimonialPart(code As String) As Shareholding Implements IPatrimonialPartRepository.GetPatrimonialPart
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As Shareholding In Me._context.Shareholding Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing Then
            res(0).OriginalValue = (From d As Shareholding In Me._context.Shareholding.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New Shareholding()
        End If
    End Function

End Class
