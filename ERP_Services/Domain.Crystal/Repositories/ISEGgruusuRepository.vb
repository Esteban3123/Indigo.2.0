Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface ISEGgruusuRepository
    Inherits IRepository(Of SEGgruusu)

    ''' <summary>
    ''' Funcion que retorna un objeto tipo grupo de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGrupoCrystal(ByVal code As String) As SEGgruusu

End Interface
