Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IFixedAssetServices
    Inherits IDisposable

    Function CalculateDaysPendingDepreciate(AdquisitionDate As Date, LifeTime As Integer, UnitLifeTime As Byte) As Integer

End Interface
