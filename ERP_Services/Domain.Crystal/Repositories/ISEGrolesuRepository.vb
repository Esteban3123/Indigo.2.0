Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface ISEGrolesuRepository
    Inherits IRepository(Of SEGrolesu)

    ''' <summary>
    ''' Funcion que retorna un objeto tipo rol de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRolCrystal(ByVal code As String) As SEGrolesu

End Interface
