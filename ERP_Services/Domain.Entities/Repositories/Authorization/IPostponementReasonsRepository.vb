#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IPostponementReasonsRepository
    Inherits IRepository(Of PostponementReasons)

    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPostponementReasonsById(id As Integer) As PostponementReasons

    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPostponementReasonsByCode(code As String) As PostponementReasons

End Interface
