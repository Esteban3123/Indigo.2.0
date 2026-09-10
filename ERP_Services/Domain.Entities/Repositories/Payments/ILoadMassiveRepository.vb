Imports Domain.Base

Public Interface ILoadMassiveRepository
    Inherits IRepository(Of LoadMassive)

    Function GetLoadMassive(ByVal code As String) As LoadMassive

    Function SP_SaveLoadMassive(XmlLoadMassive As String, XmlBills As String, XmlDetails As String) As List(Of SP_SaveLoadMassive_Result)

End Interface
