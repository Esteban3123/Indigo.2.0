'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-07-23
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
#End Region

Public Class FixedAssetDeteriorationIndicationRepository
    Inherits GenericRepository(Of DeteriorationIndications)
    Implements IFixedAssetDeteriorationIndicationRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="context">el contexto.</param>
    ''' 
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetDeteriorationIndicationByCode(Code As String) As DeteriorationIndications Implements IFixedAssetDeteriorationIndicationRepository.GetDeteriorationIndicationByCode
        If Code Is Nothing OrElse Code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim res = (From d As DeteriorationIndications In _context.DeteriorationIndications Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As DeteriorationIndications In Me._context.DeteriorationIndications.AsNoTracking() Where d.Code.Equals(Code.Trim()) Select d).FirstOrDefault()
            Return res
        Else
            Return New DeteriorationIndications()
        End If
    End Function

    Public Function ListAllDeteriorationIndications() As List(Of DeteriorationIndications) Implements IFixedAssetDeteriorationIndicationRepository.ListAllDeteriorationIndications
        Dim ListDeteriorationIndications = From e In _context.DeteriorationIndications
                                           Select e

        If ListDeteriorationIndications.Count() > 0 Then
            Return ListDeteriorationIndications.ToList()
        Else
            Return New List(Of DeteriorationIndications)
        End If
    End Function
End Class
