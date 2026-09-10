Imports Domain.Base
Imports Domain.Entities

Public Interface ILiquidationDataRepository
    Inherits IRepository(Of LiquidationData)

    ''' <summary>
    ''' obtiene los datos de liquidacion de la aseguradora por numero de ingreso
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <returns></returns>
    Function GetLiquidationDataByAdmissionNumber(_admissionNumber As String) As LiquidationData
End Interface
