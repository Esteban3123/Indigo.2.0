Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IReligiousBeliefsRepository
    Inherits IRepository(Of ReligiousBeliefs)

    Function GetReligiousBeliefs(pCode As String, pTracking As Boolean) As ReligiousBeliefs

    Function GetReligiousBeliefsByID(pID As Integer, pTracking As Boolean) As ReligiousBeliefs

End Interface
