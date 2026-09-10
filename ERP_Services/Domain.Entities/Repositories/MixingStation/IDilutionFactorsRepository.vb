'************************************************************
' Assembly         : Domain.MixingStation
' Author           : Giovanny Plazas L
' Created          : 02-09-2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports System.Threading.Tasks
Imports Domain.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del repositorio Consecutivos de lotes
''' </summary>
Public Interface IDilutionFactorsRepository
    Inherits IRepository(Of DilutionFactors)

    ''' <summary>
    ''' Valda si el medicamento cabecera ya existe en otro factor de dilucion
    ''' </summary>
    ''' <param name="DilutionFactor"></param>
    ''' <returns></returns>
    Function ValidateDuplicateDilutionFactorsAsync(DilutionFactor As DilutionFactors) As Task(Of String)

End Interface
