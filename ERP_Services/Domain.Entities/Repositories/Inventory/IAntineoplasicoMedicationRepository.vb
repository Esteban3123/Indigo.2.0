'***********************************************************************
' Assembly         : Domain.Entities.Repositories.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 04-08-2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base

Public Interface IAntineoplasicoMedicationRepository
    Inherits IRepository(Of AntineoplasicoMedication)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="statesAPM"></param>
    ''' <returns></returns>
    Function GetAntineoplasicoMedicationByStates(statesAPM As String) As List(Of Domain.Entities.AntineoplasicoMedication)

    ''' <summary>
    ''' Sp que se encarga de guardar medicamentos de resistencia bacteriana
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_SaveAntineoplasicoMedication(Xml As String, UserCode As String) As SP_SaveAntineoplasicoMedication_Result
End Interface
