'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Andrés Steven Rojas
' Created          : 31/10/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CardCollectionsRepository
    Inherits GenericRepository(Of CardCollections)
    Implements ICardCollectionsRepository

    ''' <summary>
    ''' Contexto de la base de datos
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtener un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetCardCollections(code As String) As CardCollections Implements ICardCollectionsRepository.GetCardCollections
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        
        Dim res = (From d As CardCollections In Me._context.CardCollections.Include("CardCollectionDetails").Include("EntityBankAccounts") 
                   Where d.Code.Equals(code.Trim()) 
                   Select d).FirstOrDefault
        
        If res IsNot Nothing Then
            res.OriginalValue = (From d As CardCollections In Me._context.CardCollections.AsNoTracking() 
                                Where d.Code.Equals(code.Trim()) 
                                Select d).SingleOrDefault()
            
            ' Obtener información de la cuenta bancaria
            If res.EntityBankAccounts IsNot Nothing Then
                Dim bankAccount = (From eba In _context.EntityBankAccounts.AsNoTracking()
                                  Where eba.Id = res.EntityBankAccountId
                                  Select eba).FirstOrDefault()
                If bankAccount IsNot Nothing Then
                    ' Aquí se puede agregar información adicional si es necesaria
                End If
            End If
            
            Return res
        Else
            Return New CardCollections()
        End If
    End Function

    ''' <summary>
    ''' Obtener un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCardCollectionsById(id As Integer) As CardCollections Implements ICardCollectionsRepository.GetCardCollectionsById
        Dim res = (From d As CardCollections In Me._context.CardCollections.Include("CardCollectionDetails").Include("EntityBankAccounts") 
                   Where d.Id = id 
                   Select d).ToList()
        
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As CardCollections In Me._context.CardCollections.AsNoTracking() 
                                   Where d.Id = id 
                                   Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New CardCollections()
        End If
    End Function
End Class


