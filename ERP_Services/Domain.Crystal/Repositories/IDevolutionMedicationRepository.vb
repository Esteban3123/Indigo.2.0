'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Carlos Cordoba
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IDevolutionMedicationRepository
    Inherits IRepository(Of HCDEVMEDC)

    ''' <summary>
    ''' obtiene una devolucion por consecutivo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDevolutionMedicationByConsecutive(consecutive As Integer) As HCDEVMEDC

    ''' <summary>
    ''' obtiene una devolucion por consecutivo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetViewDashBoardPharmacyDevolutionByConsecutive(consecutive As Integer) As ViewDashBoardPharmacyDevolution

End Interface
