'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IStudyCenterRepository

    Inherits IRepository(Of StudyCenter)

    ''' <summary>
    ''' Lista Todos los Centros de Estudio
    ''' </summary>
    ''' <returns>Centros de Estudios</returns>
    ''' <remarks></remarks>
    Function ListAllStudyCenter() As List(Of StudyCenter)

    ''' <summary>
    ''' Obtiene un Centro de Estudio
    ''' </summary>
    ''' <param name="code">Código del Centro de Estudio</param>
    ''' <returns>Centro de Estudio</returns>
    ''' <remarks></remarks>
    Function GetStudyCenter(ByVal code As String, Optional tracking As Boolean = True) As StudyCenter

End Interface
