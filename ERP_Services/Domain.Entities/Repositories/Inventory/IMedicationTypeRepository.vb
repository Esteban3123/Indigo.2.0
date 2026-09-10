Imports Domain.Base

Public Interface IMedicationTypeRepository
    Inherits IRepository(Of MedicationType)

#Region "Methods"

    ''' <summary>
    ''' Obtiene tipo de medicamento por código
    ''' </summary>
    ''' <param name="code">codigo</param>
    ''' <returns></returns>
    Function GetMedicationTypeByCode(ByVal code As String) As MedicationType


#End Region



End Interface
