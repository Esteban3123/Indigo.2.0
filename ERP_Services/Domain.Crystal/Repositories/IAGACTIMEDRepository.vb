Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IAGACTIMEDRepository
    Inherits IRepository(Of AGACTIMED)

    Function GetListAGACTIMEDPOCO(listAGACTIMEDCode As List(Of String)) As List(Of AGACTIMED)

End Interface
