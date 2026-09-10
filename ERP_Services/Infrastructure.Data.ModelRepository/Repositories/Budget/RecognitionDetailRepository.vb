'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class RecognitionDetailRepository
    Inherits GenericRepository(Of RecognitionDetail)
    Implements IRecognitionDetailRepository


    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    

    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetRecognitionDetailById(Id As Integer) As RecognitionDetail Implements IRecognitionDetailRepository.GetRecognitionDetailById
        Return (From rd In _context.RecognitionDetail Where rd.Id = Id Select rd).FirstOrDefault()
    End Function
End Class
