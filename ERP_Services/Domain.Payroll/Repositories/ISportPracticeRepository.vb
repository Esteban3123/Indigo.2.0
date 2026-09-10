'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Juan Diego Díaz
' Created          : 30-08-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ISportPracticeRepository
    Inherits IRepository(Of SportPractice)

    ''' <summary>
    ''' Obtiene un Deporte Practicado
    ''' </summary>
    ''' <param name="code">Código del Deporte Practicado</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Deporte Practicado</returns>
    ''' <remarks></remarks>
    Function GetSportPractice(code As String, tracking As Boolean) As SportPractice

    ''' <summary>
    ''' Obtiene un Deporte Practicado por ID
    ''' </summary>
    ''' <param name="ID">Id del Deporte Practicado</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Deporte Practicado</returns>
    ''' <remarks></remarks>
    Function GetSportPracticeById(ID As Integer, tracking As Boolean) As SportPractice


End Interface
