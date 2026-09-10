'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 27-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollStudyType

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns>Lista de tipos de estudio</returns>
    <OperationContract()> _
    Function ListAllStudyType(session As SessionValues) As List(Of StudyType)

    ''' <summary>
    ''' Elimina un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteStudyType(ByVal studyType As StudyType, session As SessionValues) As ActionMessageResult(Of StudyType)

    ''' <summary>
    ''' Guarda o edita un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveStudyType(ByVal studyType As StudyType, session As SessionValues) As Boolean

    ''' <summary>
    ''' Obtiene un tipo de estudio especifico
    ''' </summary>
    ''' <param name="code">Código de el tipo de estudio</param>
    ''' <returns> Tipo de estudio</returns>
    <OperationContract()> _
    Function GetStudyType(ByVal code As String, session As SessionValues) As StudyType

End Interface
