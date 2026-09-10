'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities
Public Interface IStudyTypeRepository
    Inherits IRepository(Of StudyType)

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllStudyType() As List(Of StudyType)

    ''' <summary>
    ''' Obtiene un tipo de estudio
    ''' </summary>
    ''' <param name="code">Codigo del tipo de estudio</param>
    ''' <returns>Tipo de estudio</returns>
    ''' <remarks></remarks>
    Function GetStudyType(ByVal code As String, Optional tracking As Boolean = True) As StudyType

End Interface
