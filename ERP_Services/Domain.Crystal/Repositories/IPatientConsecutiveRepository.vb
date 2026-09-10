'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Jhossept K. Garay
' Created          : 26-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Interface IPatientConsecutiveRepository
    Inherits IRepository(Of INCONSEPA)

    ''' <summary>
    ''' Funcion para obtener el consecutivo segun tipo de población y documento
    ''' </summary>
    ''' <param name="PoblationType"></param>
    ''' <param name="Document"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsecutiveByPoblationTypeAndDocument(PoblationType As String, Document As String) As INCONSEPA
End Interface
