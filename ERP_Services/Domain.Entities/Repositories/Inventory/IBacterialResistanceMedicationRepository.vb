'***********************************************************************
' Assembly         : Domain.Entities.Repositories.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 06-04-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base

Public Interface IBacterialResistanceMedicationRepository
    Inherits IRepository(Of BacterialResistanceMedication)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="statesBRM"></param>
    ''' <returns></returns>
    Function GetBacterialResistanceMedicationByStates(statesBRM As String) As List(Of Domain.Entities.BacterialResistanceMedication)

    ''' <summary>
    ''' Sp que se encarga de guardar medicamentos de resistencia bacteriana
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_SaveBacterialResistanceMedication(Xml As String, UserCode As String) As SP_SaveBacterialResistanceMedication_Result
End Interface
